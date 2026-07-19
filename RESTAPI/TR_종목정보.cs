using Microsoft.Office.Interop.Excel;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json; // System.Text.Json 사용
using System.Threading.Tasks;
using 지니64.RESTAPI;

namespace 지니64
{
    internal class TR_종목정보
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

        public static async Task 종목정보(string token, object data, string tr_id, string cont_yn = "N", string next_key = "")
        {
            // 1. 요청할 API URL
            string host = "https://api.kiwoom.com";
            if (GenieConfig.checkBox_Simulation) host = "https://mockapi.kiwoom.com";

            string endpoint = "/api/dostk/stkinfo";
            string url = host + endpoint;

            // [핵심 1] 재요청에 필요한 파라미터 변수를 try 블록 밖으로 꺼냅니다!
            string req_stk_cd = "";
            string req_mrkt_tp = "";

            try
            {
                // 3. HTTP POST 요청
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (JsonDocument reqDoc = JsonDocument.Parse(json))
                {
                    JsonElement reqRoot = reqDoc.RootElement;
                    req_stk_cd = GetSafeString(reqRoot, "stk_cd");
                    req_mrkt_tp = GetSafeString(reqRoot, "mrkt_tp");
                }

                // === [수술 완료] 공용 헤더를 건드리지 않고, 이 요청만을 위한 전용 편지봉투를 만듭니다 ===
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    request.Headers.Add("cont-yn", cont_yn);
                    request.Headers.Add("next-key", next_key);
                    request.Headers.Add("api-id", tr_id);

                    request.Content = content;

                    // 통신 단절(SocketException) 시 여기서 바로 catch문으로 점프합니다!
                    using (var response = await client.SendAsync(request))
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

                        // =================================================================
                        // [제1 방어선] HTTP 에러 및 HTML 반환 시 안전하게 재요청
                        // =================================================================
                        if (!response.IsSuccessStatusCode)
                        {
                          //  Form1.Console_print($"[HTTP 에러] 상태코드: {response.StatusCode} -> 종목정보 재요청 실행");
                            종목정보_재요청(tr_id, req_stk_cd, req_mrkt_tp, cont_yn, next_key);
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(responseBody))
                        {
                            Form1.Console_print("[심각] 서버 응답 본문이 비어있습니다 -> 종목정보 재요청 실행");
                            종목정보_재요청(tr_id, req_stk_cd, req_mrkt_tp, cont_yn, next_key);
                            return;
                        }

                        if (responseBody.TrimStart().StartsWith("<"))
                        {
                            Form1.Console_print($"[심각] 서버가 JSON 대신 HTML 페이지를 반환했습니다 -> 종목정보 재요청 실행");
                            종목정보_재요청(tr_id, req_stk_cd, req_mrkt_tp, cont_yn, next_key);
                            return;
                        }

                        // =================================================================
                        // [핵심] JSON 파싱 후, TR별로 분리된 함수로 넘겨주기!
                        // =================================================================
                        try
                        {
                            // ==========================================================
                            // 1. [긴급 검사] JSON 파싱 전, 토큰 만료 에러부터 가장 먼저 요격!
                            // ==========================================================
                            if (responseBody.Contains("8005") && responseBody.Contains("유효하지 않습니다"))
                            {
                                Form1.Console_print(">> [REST API] 접근 토큰이 만료되었습니다! (에러 8005)");
                                Log.에러기록("[토큰 만료] 서버로부터 8005 에러 수신. 안전 종료를 시도합니다.");

                                Form1.중복접속 = false; // 단순 토큰 만료이므로 중복 접속은 아님을 명시
                                Helper.안전종료하기();
                                return; // 에러 메시지이므로 더 이상 아래(JSON 파싱)로 내려가지 않고 즉시 컷!
                            }

                            using (JsonDocument doc = JsonDocument.Parse(responseBody))
                            {
                                JsonElement root = doc.RootElement;

                                if (tr_id.Equals("ka10100"))
                                {
                                    처리_ka10100(root, req_stk_cd);
                                }
                                else if (tr_id.Equals("ka10099"))
                                {
                                    처리_ka10099(root, req_mrkt_tp);
                                }
                                else if (tr_id.Equals("ka00198"))
                                {
                                    처리_ka00198(root);
                                }
                                else if (tr_id.Equals("kt20016"))
                                {
                                    처리_kt20016(root, response, ref cont_yn, ref next_key);
                                }
                            }
                        }
                        catch (JsonException)
                        {
                            Form1.Console_print($"[JSON 파싱 실패] 서버 응답이 올바른 JSON 형식이 아닙니다 -> 종목정보 재요청 실행");
                            종목정보_재요청(tr_id, req_stk_cd, req_mrkt_tp, cont_yn, next_key);
                        }
                    } // using response 끝
                } // using request 끝
            }
            // =================================================================
            // [제2 방어선] 키움 서버 응답 없음(SocketException) 또는 타임아웃 방어막!
            // =================================================================
            catch (HttpRequestException)
            {
                Form1.Console_print($"[네트워크 끊김] 키움 서버 무응답 (SocketException) -> 3초 대기 후 재요청");
                await Task.Delay(3000); // 3초 숨고르기
                종목정보_재요청(tr_id, req_stk_cd, req_mrkt_tp, cont_yn, next_key);
            }
            catch (TaskCanceledException)
            {
                Form1.Console_print($"[타임아웃] 키움 서버 응답 지연 -> 3초 대기 후 재요청");
                await Task.Delay(3000);
                종목정보_재요청(tr_id, req_stk_cd, req_mrkt_tp, cont_yn, next_key);
            }
            catch (Exception error)
            {
                Form1.Console_print("[DEBUG] TR_종목정보 요청 중 치명적 에러: " + error.Message);
            }
        }

        internal static void 종목정보_재요청(string tr_id, string req_stk_cd, string req_mrkt_tp, string cont_yn, string next_key)
        {
         //   Form1.Console_print($"[재요청] 종목정보 통신 복구를 시도합니다. (TR: {tr_id})");

            if (tr_id == "ka10100")
            {
                // 단일 종목 정보 조회 (종목코드 필요)
               TR_요청.종목정보조회(req_stk_cd, true);
            }
            else if (tr_id == "ka10099")
            {
                // 시장별 리스트 조회 (시장구분코드 필요)
                TR_loding.종목정보리스트(req_mrkt_tp, true);
            }
            else if (tr_id == "ka00198")
            {
                // 실시간 조회 순위 (파라미터 없이 바로 호출)
                Rankinginfo.실시간종목조회순위(true);
            }
            else if (tr_id == "kt20016")
            {
                // 신용융자 가능 종목 (페이징 데이터 유지하여 호출)
                TR_요청.신용융자가능종목요청(cont_yn, next_key, true);
            }
            else
            {
                Form1.Console_print($"[재요청 실패] 알 수 없는 종목정보 TR입니다 -> {tr_id}");
            }
        }

        // =========================================================================
        // 아래부터는 밖으로 빼낸 개별 TR 처리 함수들입니다.
        // =========================================================================

        private static void 처리_ka10100(JsonElement root, string req_stk_cd)
        {
            string returnCode = GetSafeString(root, "return_code");

            if (returnCode == "0")
            {
                Form1.Console_print("-------------종목정보 조회------------------ ");
                Form1.Console_print("[종목정보리스트] 종목코드 : " + GetSafeString(root, "code"));
                Form1.Console_print("[종목정보리스트] 종목명 : " + GetSafeString(root, "name"));
                Form1.Console_print("[종목정보리스트] 전일종가 : " + GetSafeString(root, "lastPrice"));
                Form1.Console_print("[종목정보리스트] 종목상태 : " + GetSafeString(root, "state"));
                Form1.Console_print("[종목정보리스트] 시장명 : " + GetSafeString(root, "marketName"));
                Form1.Console_print("[종목정보리스트] NXT가능여부 : " + GetSafeString(root, "nxtEnable"));

            
                string itemcode = GetSafeString(root, "code");
                string state = GetSafeString(root, "state");
                string nxtEnable = GetSafeString(root, "nxtEnable");

                if (Form1.Market_Item_List.ContainsKey(itemcode))
                {
                    Form1.Market_Item_List[itemcode].state = state;
                }

                if (Form1.stockBalanceList.TryGetValue(itemcode, out Stockbalance 잔고))
                {
                    잔고.매매가능 = true;
                    잔고.잔고청산 = true;
                    잔고.종목상태 = GET.Jango_state(잔고.종목코드);

                    if (nxtEnable == "Y" && !Form1.NXT_list.Contains(itemcode))
                    {
                        Form1.NXT_list.Add(itemcode);
                    }
                }
                Form1.키움_TR유량제한 = false;

            }
            else
            {
                Form1.키움_TR유량제한 = true;
                Form1.Console_print($"종목정보 조회 실패 ({req_stk_cd}): {GetSafeString(root, "return_msg")}");
                TR_요청.종목정보조회(req_stk_cd, true); // 재요청
            }
        }

        private static void 처리_ka10099(JsonElement root, string req_mrkt_tp)
        {
            string returnCode = GetSafeString(root, "return_code");

            if (returnCode == "0")
            {
                if (root.TryGetProperty("list", out JsonElement listArray) && listArray.ValueKind == JsonValueKind.Array)
                {
                    string 시장 = "";
                    foreach (JsonElement item in listArray.EnumerateArray())
                    {
                        string marketName = GetSafeString(item, "marketName");
                        시장 = marketName;

                        if (marketName == "코스닥" || marketName == "거래소" || marketName == "ETF")
                        {
                            string code = GetSafeString(item, "code");
                            string name = GetSafeString(item, "name");
                            string lastPrice = GetSafeString(item, "lastPrice");
                            string state = GetSafeString(item, "state");
                            string orderWarning = GetSafeString(item, "orderWarning");
                            string nxtEnable = GetSafeString(item, "nxtEnable");

                            Market_load.Loading(code, name, lastPrice, state, marketName, orderWarning, nxtEnable);
                        }
                    }

                    if (시장 == "거래소")
                    {
                        Form1.Console_print("종목정보 리스트 불러오기 완료 - " + 시장 + " - " + Form1.server + "    " + DateTime.Now.ToString("HH:mm:ss.fff"));
                        Form1.매매시작 = "Loding_02_코스닥리스트요청";
                    }
                    if (시장 == "코스닥")
                    {
                        Form1.Console_print("종목정보 리스트 불러오기 완료 - " + 시장 + " - " + Form1.server + "    " + DateTime.Now.ToString("HH:mm:ss.fff"));
                        Log.동작기록("종목정보 리스트 불러오기 완료 - " + Form1.server);
                        Form1.매매시작 = "Loding_03_코스피현재가요청";
                    }
                    Form1.키움_TR유량제한 = false;
                }
            }
            else
            {
                Form1.키움_TR유량제한 = true;
                Form1.Console_print($"종목정보리스트 조회 실패: {GetSafeString(root, "return_msg")}");
                TR_loding.종목정보리스트(req_mrkt_tp, true); // 재요청
            }
        }

        private static void 처리_ka00198(JsonElement root)
        {
            string returnCode = GetSafeString(root, "return_code");

            if (returnCode == "0")
            {
                Form1.키움_TR유량제한 = false;
                if (root.TryGetProperty("item_inq_rank", out JsonElement listElement) && listElement.ValueKind == JsonValueKind.Array)
                {
                    int 출력갯수 = 0;
                    foreach (JsonElement item in listElement.EnumerateArray())
                    {
                        if (++출력갯수 > 5) break;

                        string 종목코드 = GetSafeString(item, "stk_cd").Trim();
                        string 종목명 = GetSafeString(item, "stk_nm").Trim();
                        string 현재순위 = GetSafeString(item, "bigd_rank");
                        string 현재가_str = GetSafeString(item, "cur_prc");
                        int.TryParse(현재가_str, out int 현재가);

                        var 종목 = Ranking.통합랭킹딕셔너리.GetOrAdd(종목코드, k => new 랭킹종목정보
                        {
                            종목코드 = 종목코드,
                            종목명 = 종목명
                        });

                        int rank = int.Parse(현재순위);
                        종목.조회순위 = rank;
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
                Form1.Console_print($"실시간종목조회순위 실패: {GetSafeString(root, "return_msg")}");
                Form1.키움_TR유량제한 = true;
                Rankinginfo.실시간종목조회순위(Form1.키움_TR유량제한); // 재요청
            }
        }

       static int conut = 40;
        static DateTime 마지막로그시간 = DateTime.MinValue;
        // 🌟 파라미터에 string req_stk_cd 추가!
        private static void 처리_kt20016(JsonElement root, HttpResponseMessage response, ref string cont_yn, ref string next_key)
        {
            string returnCode = GetSafeString(root, "return_code");

            if (returnCode == "0") // 정상 처리 완료
            {
                Form1.키움_TR유량제한 = false;
                
                if (root.TryGetProperty("crd_loan_pos_stk", out JsonElement stkList) && stkList.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement stk in stkList.EnumerateArray())
                    {
                        // 명세서에 있는 모든 항목 추출
                        string 종목코드 = GetSafeString(stk, "stk_cd").TrimStart('A');
                        string 한도초과여부 = GetSafeString(stk, "crd_limit_over_yn");

                        if (한도초과여부 == "Y")
                        {
                            if (Form1.Market_Item_List.TryGetValue(종목코드, out var targetItem))
                            {
                                targetItem.신용가능 = false;

                                string 한도초과텍스트 = GetSafeString(stk, "crd_limit_over_txt");
                            //    Form1.Console_print($"[{종목코드}] {targetItem.종목명} | 보증금율:{targetItem.state} | 한도초과:{한도초과여부} ({한도초과텍스트})");
                            }
                        }
                    }
                }

                // 🌟 [수정 1] 아빠님의 원래 정확한 헤더 이름으로 원상 복구!
                try
                {
                    if (response.Headers.TryGetValues("cont-yn", out var contYnValues))
                        cont_yn = contYnValues.FirstOrDefault() ?? "N";

                    if (response.Headers.TryGetValues("next-key", out var nextKeyValues))
                        next_key = nextKeyValues.FirstOrDefault() ?? "";
                }
                catch (Exception ex)
                {
                    Form1.Console_print($"[KT20016] 헤더 파싱 오류: {ex.Message}");
                    cont_yn = "N";
                }

                if (Helper.IsDebugMode) cont_yn = "N";

                // 연속 조회가 필요한 경우
                if (cont_yn == "Y")
                {
                    // 현재 시간과 마지막 로그 시간을 비교해서 5초(5000밀리초) 이상 차이가 나면 출력
                    if ((DateTime.Now - 마지막로그시간).TotalSeconds >= 5)
                    {
                   //     Log.동작기록($"신용융자 가능종목 요청 중... (현재 {conut}페이지 남음)");
                        Form1.Console_print($"(백그라운드 실행) 신용융자 가능종목 요청 중... (현재 {conut}페이지 남음)");

                        // 출력했으니 마지막 로그 시간을 지금 시간으로 갱신
                        마지막로그시간 = DateTime.Now;
                    }

                    Form1.키움_TR유량제한 = true;
                    TR_요청.신용융자가능종목요청(cont_yn, next_key, false);

                    conut--;
                    return;
                }

                if (cont_yn == "N")
                {
                    // 조회가 끝났으므로 다음 조회를 위해 변수 초기화
                    conut = 0;
                    마지막로그시간 = DateTime.MinValue;

                    Form1.Console_print("(백그라운드 실행) 신용가능여부 조회 완료 - " + Form1.server + "    " + DateTime.Now.ToString("HH:mm:ss.fff"));
                  //  Log.동작기록("신용가능여부 조회 완료 - " + Form1.server);

                  //  로그인완료();
				}
            }
            else // 오류 발생 또는 TR 과부하
            {
                Form1.Console_print($"(백그라운드 실행) 신용가능여부 실패 (코드:{returnCode}): {GetSafeString(root, "return_msg")}");
                
                if(returnCode == "5") // TR 과부하로 인한 실패
                {
                    Form1.키움_TR유량제한 = true;
                    TR_요청.신용융자가능종목요청(cont_yn, next_key, false);
                }
                else if (returnCode == "20") 
                {
                    if(GetSafeString(root, "return_msg").Contains("모의투자에서는 해당업무가 제공되지 않습니다."))
                    {
                        Form1.Console_print($"[알림] 모의투자에서는 신용가능여부 조회가 제공되지 않습니다. 시뮬레이션 모드에서는 이 TR을 건너뛰고 다음 단계로 진행합니다.");

                     //   로그인완료();
                    }
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