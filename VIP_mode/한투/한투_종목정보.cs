using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json; // System.Text.Json 사용
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 지니64.box;

namespace 지니64
{
    internal class TR_종목정보_한투 : Form1
    {
        private static readonly HttpClient client = new HttpClient();

        // 헬퍼: 안전하게 string 가져오기 (숫자/문자 모두 처리)
        private static string GetSafeString(JsonElement element, string key)
        {
            if (element.TryGetProperty(key, out JsonElement prop))
            {
                if (prop.ValueKind == JsonValueKind.String) return prop.GetString();
                if (prop.ValueKind == JsonValueKind.Number) return prop.ToString();
                return prop.ToString();
            }
            return "";
        }

        // =================================================================================
        // 1. 한국투자증권 전용 GET 요청 메서드 (종목/순위/신용 조회계열은 모두 GET 사용)
        // =================================================================================
        public static async Task 종목정보_한투(string token, string appKey, string appSecret, Dictionary<string, string> queryParams, string tr_, string tr_cont = "")
        {
            string host = "https://openapi.koreainvestment.com:9443"; // 실전투자
            if (GenieConfig.checkBox_Simulation) host = "https://openapivts.koreainvestment.com:29443"; // 모의투자

            string tr_id = tr_.Split('_')[0];
            string endpoint = "";

            // KIS API 엔드포인트 분기 (키움 TR -> 한투 TR 매핑)
            if (tr_id == "CTPF1002R") endpoint = "/uapi/domestic-stock/v1/quotations/search-stock-info"; // 주식기본조회 (키움 ka10100 대응)
            else if (tr_id == "HHMCM000100C0") endpoint = "/uapi/domestic-stock/v1/ranking/hts-top-view"; // HTS조회상위20종목 (키움 ka00198 대응)
            else if (tr_id == "FHPST04770000") endpoint = "/uapi/domestic-stock/v1/quotations/credit-by-company"; // 당사 신용가능종목 (키움 kt20016 대응)

            // GET 쿼리 파라미터 조합
            string queryString = "";
            if (queryParams != null && queryParams.Count > 0)
            {
                queryString = "?" + string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            }

            string url = host + endpoint + queryString;

            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    request.Headers.Add("appkey", appKey);
                    request.Headers.Add("appsecret", appSecret);
                    request.Headers.Add("tr_id", tr_id);

                    if (!string.IsNullOrEmpty(tr_cont))
                        request.Headers.Add("tr_cont", tr_cont); // N, Y, M, F 처리

                    using (var response = await client.SendAsync(request))
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

                        // =================================================================
                        // [제1 방어선] 토큰 만료 에러 요격 (한투는 msg_cd EGV00003 등 반환)
                        // =================================================================
                        if (responseBody.Contains("EGV00003") || responseBody.Contains("유효하지 않은 Token"))
                        {
                            Form1.Console_print(">> [REST API] 한국투자증권 접근 토큰이 만료되었습니다!");
                            Log.에러기록("[토큰 만료] 서버로부터 토큰 만료 에러 수신. 안전 종료를 시도합니다.");

                            Form1.중복접속 = false; // 단순 토큰 만료이므로 중복 접속은 아님을 명시
                            Helper.안전종료하기();
                            return;
                        }

                        if (!response.IsSuccessStatusCode)
                        {
                            종목정보_재요청_한투(tr_id, queryParams, tr_cont);
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(responseBody) || responseBody.TrimStart().StartsWith("<"))
                        {
                            Form1.Console_print("[심각] 서버 응답이 비어있거나 HTML입니다 -> 종목정보 재요청 실행");
                            종목정보_재요청_한투(tr_id, queryParams, tr_cont);
                            return;
                        }

                        try
                        {
                            using (JsonDocument doc = JsonDocument.Parse(responseBody))
                            {
                                JsonElement root = doc.RootElement;
                                string rt_cd = GetSafeString(root, "rt_cd"); // 0: 성공

                                // 한투 연속조회 키 (다음 페이지 조회용)
                                string ctx_fk = response.Headers.TryGetValues("tr_cont", out var c_yn) ? c_yn.FirstOrDefault() : "";
                                string out_ctx_fk = GetSafeString(root, "ctx_area_fk100");
                                string out_ctx_nk = GetSafeString(root, "ctx_area_nk100");

                                // =================================================================
                                // [핵심] JSON 파싱 후, TR별로 분리된 함수로 넘겨주기!
                                // =================================================================
                                if (tr_id.Equals("CTPF1002R"))
                                {
                                    string req_stk_cd = queryParams != null && queryParams.ContainsKey("PDNO") ? queryParams["PDNO"] : "";
                                    처리_CTPF1002R_주식기본조회(root, req_stk_cd);
                                }
                                else if (tr_id.Equals("HHMCM000100C0"))
                                {
                                    처리_HHMCM000100C0_조회상위20(root);
                                }
                                else if (tr_id.Equals("FHPST04770000"))
                                {
                                    처리_FHPST04770000_신용가능종목(root, response, ref ctx_fk, out_ctx_fk, out_ctx_nk);
                                }
                                // ※ 한투는 키움의 'ka10099'(시장 전체 종목리스트) API가 없습니다.
                                // 대신 KIS Developers에서 제공하는 '종목마스터 파일(.mst)'을 다운받아 로컬에서 파싱하는 방식을 사용해야 합니다.
                            }
                        }
                        catch (JsonException)
                        {
                            Form1.Console_print($"[JSON 파싱 실패] 서버 응답이 올바른 JSON 형식이 아닙니다 -> 종목정보 재요청 실행");
                            종목정보_재요청_한투(tr_id, queryParams, tr_cont);
                        }
                    }
                }
            }
            // =================================================================
            // [제2 방어선] 서버 무응답(SocketException) 또는 타임아웃 방어막!
            // =================================================================
            catch (HttpRequestException)
            {
                Form1.Console_print($"[네트워크 끊김] 한투 서버 무응답 (SocketException) -> 3초 대기 후 재요청");
                await Task.Delay(3000);
                종목정보_재요청_한투(tr_id, queryParams, tr_cont);
            }
            catch (TaskCanceledException)
            {
                Form1.Console_print($"[타임아웃] 한투 서버 응답 지연 -> 3초 대기 후 재요청");
                await Task.Delay(3000);
                종목정보_재요청_한투(tr_id, queryParams, tr_cont);
            }
            catch (Exception error)
            {
                Form1.Console_print("[DEBUG] TR_종목정보 요청 중 치명적 에러: " + error.Message);
            }
        }

        internal static void 종목정보_재요청_한투(string tr_id, Dictionary<string, string> queryParams, string tr_cont)
        {
            if (tr_id == "CTPF1002R")
            {
                string req_stk_cd = queryParams.ContainsKey("PDNO") ? queryParams["PDNO"] : "";
           //     TR_요청.한투_종목정보조회(req_stk_cd, true); // (TR_요청 클래스에 선언된 한투 전용 메서드 호출)
            }
            else if (tr_id == "HHMCM000100C0")
            {
              //  Rankinginfo.한투_실시간종목조회순위(true);
            }
            else if (tr_id == "FHPST04770000")
            {
           //     TR_요청.한투_신용융자가능종목요청(tr_cont, "", "", true);
            }
            else
            {
                Form1.Console_print($"[재요청 실패] 알 수 없는 종목정보 TR입니다 -> {tr_id}");
            }
        }


        // =========================================================================
        // 개별 TR 파싱 처리 함수들 (키움 -> 한국투자증권 규격 매핑)
        // =========================================================================

        // 키움 ka10100 -> 한투 CTPF1002R (주식기본조회)
        private static void 처리_CTPF1002R_주식기본조회(JsonElement root, string req_stk_cd)
        {
            string returnCode = GetSafeString(root, "rt_cd");

            if (returnCode == "0")
            {
                if (root.TryGetProperty("output", out JsonElement output))
                {
                    string itemcode = GetSafeString(output, "pdno"); // 상품번호(종목코드)
                    string name = GetSafeString(output, "prdt_name"); // 상품명
                    string lastPrice = GetSafeString(output, "stck_prdy_clpr"); // 주식전일종가
                    string marketName = GetSafeString(output, "prdt_clsf_name"); // 코스닥/코스피 등
                    string iscd_stat = GetSafeString(output, "iscd_stat_cls_code"); // 00:정상, 51:관리, 57:증거금100% 등

                    Form1.Console_print("-------------종목정보 조회------------------ ");
                    Form1.Console_print("[종목정보리스트] 종목코드 : " + itemcode);
                    Form1.Console_print("[종목정보리스트] 종목명 : " + name);
                    Form1.Console_print("[종목정보리스트] 전일종가 : " + lastPrice);
                    Form1.Console_print("[종목정보리스트] 종목상태코드 : " + iscd_stat);
                    Form1.Console_print("[종목정보리스트] 시장명 : " + marketName);

                    // 한투의 상태코드를 지니64의 범용 텍스트 상태로 변환
                    string state = "정상";
                    if (iscd_stat == "51") state = "관리종목";
                    else if (iscd_stat == "57") state = "증거금100%";

                    if (Form1.Market_Item_List.ContainsKey(itemcode))
                    {
                        Form1.Market_Item_List[itemcode].state = state;
                    }

                    if (Form1.stockBalanceList.TryGetValue(itemcode, out Stockbalance 잔고))
                    {
                        잔고.매매가능 = true;
                        잔고.잔고청산 = true;
                        잔고.종목상태 = GET.Jango_state(잔고.종목코드);

                        // NXT(넥스트레이드) 가능 여부 로직 (한투는 기본 야간/주간/NXT 여부를 별도 판단)
                        if (!Form1.NXT_list.Contains(itemcode))
                        {
                            Form1.NXT_list.Add(itemcode);
                        }
                    }
                    Form1.한투_TR유량제한 = false;

                    // =========================================================================
                    // 📝 [로그 기록] '제주반도체'일 때만 상세 정보 출력 (기존 로직 유지)
                    // =========================================================================
                    if (name == "제주반도체")
                    {
                        Form1.Console_print($"[제주반도체 분석] 상태: {state} ");
                    }
                    if (name == "JB금융지주")
                    {
                        Form1.Console_print($"[JB금융지주 분석] 상태: {state}");
                    }
                    // =========================================================================
                }
            }
            else
            {
                Form1.한투_TR유량제한 = true;
                Form1.Console_print($"종목정보 조회 실패 ({req_stk_cd}): {GetSafeString(root, "msg1")}");
             //   TR_요청.한투_종목정보조회(req_stk_cd, true); // 재요청
            }
        }

        // 키움 ka00198 -> 한투 HHMCM000100C0 (HTS조회상위 20종목)
        private static void 처리_HHMCM000100C0_조회상위20(JsonElement root)
        {
            string returnCode = GetSafeString(root, "rt_cd");

            if (returnCode == "0")
            {
                Form1.한투_TR유량제한 = false;
                if (root.TryGetProperty("output", out JsonElement listElement) && listElement.ValueKind == JsonValueKind.Array)
                {
                    int 출력갯수 = 0;
                    foreach (JsonElement item in listElement.EnumerateArray())
                    {
                        if (++출력갯수 > 5) break; // 기존 코드처럼 상위 5개만 추출

                        string 종목코드 = GetSafeString(item, "mksc_shrn_iscd").Trim(); // 유가증권단축종목코드

                        // 한투 해당 API는 이름과 현재가를 안 주므로 기존 Market_Item_List에서 매핑
                        string 종목명 = "";
                        int 현재가 = 0;
                        if (Form1.Market_Item_List.TryGetValue(종목코드, out var mItem))
                        {
                            종목명 = mItem.종목명;
                            현재가 = mItem.현재가;
                        }

                        var 종목 = Ranking.통합랭킹딕셔너리.GetOrAdd(종목코드, k => new 랭킹종목정보
                        {
                            종목코드 = 종목코드,
                            종목명 = 종목명
                        });

                        종목.조회순위 = 출력갯수; // 순서대로 랭크 부여
                        if (현재가 > 0) 종목.현재가 = 현재가;

                        if (string.IsNullOrEmpty(종목.시장))
                        {
                            if (Form1.Market_Item_List.TryGetValue(종목코드, out var itemInfo))
                            {
                                종목.시장 = itemInfo.Market;
                                if (종목.전일종가 == 0) 종목.전일종가 = itemInfo.Last_price;
                            }
                        }
                    }
                    Ranking.응답완료카운트++;
                }
            }
            else
            {
                Form1.Console_print($"실시간종목조회순위 실패: {GetSafeString(root, "msg1")}");
                Form1.한투_TR유량제한 = true;
             //   Rankinginfo.한투_실시간종목조회순위(Form1.한투_TR유량제한); // 재요청
            }
        }


        // 키움 kt20016 -> 한투 FHPST04770000 (당사 신용가능종목)
        static int conut = 40;
        static DateTime 마지막로그시간 = DateTime.MinValue;

        private static void 처리_FHPST04770000_신용가능종목(JsonElement root, HttpResponseMessage response, ref string cont_yn, string ctx_fk, string ctx_nk)
        {
            string returnCode = GetSafeString(root, "rt_cd");

            if (returnCode == "0") // 정상 처리 완료
            {
                Form1.한투_TR유량제한 = false;

                if (root.TryGetProperty("output", out JsonElement stkList) && stkList.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement stk in stkList.EnumerateArray())
                    {
                        string 종목코드 = GetSafeString(stk, "stck_shrn_iscd").Trim();
                        string 신용율 = GetSafeString(stk, "crdt_rate");

                        // 한투의 해당 API는 "당사가 선정한 신용가능 종목 리스트"만 반환하므로 리스트에 있으면 무조건 신용 가능.
                        // (키움처럼 초과여부를 하나하나 확인해서 끄는 방식이 아니라 리스트에 존재함을 확인)
                        if (Form1.Market_Item_List.TryGetValue(종목코드, out var targetItem))
                        {
                            targetItem.신용가능 = true;
                        }
                    }
                }

                // 한투 연속조회 헤더 추출 (M, F 등)
                if (response.Headers.TryGetValues("tr_cont", out var contYnValues))
                    cont_yn = contYnValues.FirstOrDefault() ?? "N";

                if (Helper.IsDebugMode) cont_yn = "N";

                // 다음 페이지가 있는 경우 (한투는 M 혹은 F가 연속조회 값임)
                if (cont_yn == "M" || cont_yn == "F" || cont_yn == "Y")
                {
                    // 현재 시간과 마지막 로그 시간을 비교해서 5초 이상 차이가 나면 출력
                    if ((DateTime.Now - 마지막로그시간).TotalSeconds >= 5)
                    {
                        Log.동작기록($"신용융자 가능종목 요청 중... (현재 {conut}페이지 남음)");
                        Form1.Console_print($"신용융자 가능종목 요청 중... (현재 {conut}페이지 남음)");
                        마지막로그시간 = DateTime.Now;
                    }

                    Form1.한투_TR유량제한 = true;
                    // 다음페이지 조회 시 tr_cont에 N 대신 넘겨받은 M, F 값 세팅 및 ctx_fk, ctx_nk 전송
                  //  TR_요청.한투_신용융자가능종목요청(cont_yn, ctx_fk, ctx_nk, true);

                    conut--;
                    return;
                }

                // 조회가 최종적으로 끝났을 경우 (D, E, N 등)
                if (cont_yn == "D" || cont_yn == "E" || cont_yn == "N" || string.IsNullOrEmpty(cont_yn))
                {
                    conut = 0;
                    마지막로그시간 = DateTime.MinValue;

                    Form1.Console_print("신용가능여부 조회 완료 - " + Form1.server + "    " + DateTime.Now.ToString("HH:mm:ss.fff"));
                    Log.동작기록("신용가능여부 조회 완료 - " + Form1.server);

                    로그인완료();
                }
            }
            else // 오류 발생 또는 TR 과부하
            {
                Form1.Console_print($"신용가능여부 실패 (코드:{returnCode}): {GetSafeString(root, "msg1")}");

                // 모의투자 미지원 에러인 경우 KIS 모의투자 서버 응답 분기 처리
                if (GetSafeString(root, "msg1").Contains("모의투자에서는"))
                {
                    Form1.Console_print($"[알림] 한투 모의투자에서는 신용가능종목 조회가 미지원됩니다. 시뮬레이션 모드에서는 이 TR을 건너뛰고 다음 단계로 진행합니다.");
                    로그인완료();
                }
                else
                {
                    Form1.한투_TR유량제한 = true;
                 //   TR_요청.한투_신용융자가능종목요청(cont_yn, ctx_fk, ctx_nk, true);
                }
            }

            void 로그인완료()
            {
                TelegramMessenger.Telegram_alram("logIn_user");
                Trade_tasks.잔고_매매();
                Form1.매매시작 = "Load_completion";
            }
        }
    }
}