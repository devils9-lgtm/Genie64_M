using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json; // System.Text.Json 사용
using System.Threading.Tasks;

namespace 지니64
{
    internal class TR_시세
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

        public static async Task 시세(string token, object data, string tr_id, string cont_yn = "N", string next_key = "")
        {
            // 1. 요청할 API URL
            string host = "https://api.kiwoom.com"; // 실전투자
            if (GenieConfig.checkBox_Simulation) host = "https://mockapi.kiwoom.com"; // 모의투자

            string endpoint = "/api/dostk/mrkcond";
            string url = host + endpoint;

            try
            {
                // 3. HTTP POST 요청 및 요청 데이터 백업
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // 재요청을 위해 요청 파라미터 미리 파싱
                string req_mrkt_tp = "";
                string req_stk_cd = "";

                using (JsonDocument reqDoc = JsonDocument.Parse(json))
                {
                    JsonElement reqRoot = reqDoc.RootElement;
                    req_mrkt_tp = GetSafeString(reqRoot, "mrkt_tp");
                    req_stk_cd = GetSafeString(reqRoot, "stk_cd");
                }

                // ==========================================================
                // [수술 완료] 공용 헤더(client.DefaultRequestHeaders)를 건드리지 않고, 
                // 이 요청만을 위한 전용 편지봉투(HttpRequestMessage)를 만듭니다!
                // ==========================================================
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    request.Headers.Add("cont-yn", cont_yn);
                    request.Headers.Add("next-key", next_key);
                    request.Headers.Add("api-id", tr_id); // TR명

                    request.Content = content;

                    // PostAsync 대신 SendAsync를 사용하여 개별 편지봉투를 안전하게 전송합니다.
                    using (var response = await client.SendAsync(request))
                    {
                        // 응답 본문 읽기
                        var responseBody = await response.Content.ReadAsStringAsync();

                        // =================================================================
                        // [지니 에러 방어] HTTP 에러 및 HTML 반환 시 안전하게 중단 및 재요청
                        // =================================================================
                        //if (!response.IsSuccessStatusCode)
                        //{
                        //    Form1.Console_print($"[HTTP 에러] 상태코드: {response.StatusCode} TR_id: {tr_id} -> 시세 재요청 실행");
                        //}

                        if (string.IsNullOrWhiteSpace(responseBody) || responseBody.TrimStart().StartsWith("<"))
                        {
                            Form1.Console_print($"[심각] 서버가 JSON 대신 HTML을 반환했습니다. (서버오류) -> 시세 재요청 실행");

                            // HTML 에러 시 즉시 재요청 (무한 루프 방지를 위해 TR_요청 내부에서 관리 필요)
                            시세_재요청(tr_id, req_mrkt_tp, req_stk_cd);
                            return;
                        }

                        try
                        {
                            // ==========================================================
                            // 💡 1. [긴급 검사] JSON 파싱 전, 토큰 만료 에러부터 가장 먼저 요격!
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

                                if (tr_id.Equals("ka90005"))
                                {
                                    처리_ka90005(root, req_mrkt_tp);
                                }
                                else if (tr_id.Equals("ka10004"))
                                {
                                    처리_ka10004(root, req_stk_cd);
                                }
                            }
                        }
                        catch (JsonException)
                        {
                            Form1.Console_print($"[JSON 파싱 실패] 서버 응답이 올바른 JSON 형식이 아닙니다. -> 시세 재요청 실행");
                            시세_재요청(tr_id, req_mrkt_tp, req_stk_cd);
                        }
                    } // using response 끝
                } // using request 끝
            }
            catch (Exception error)
            {
                Form1.Console_print("시세 요청 실패: " + error.Message);
            }
        }

        private static void 처리_ka90005(JsonElement root, string req_mrkt_tp)
        {
            string returnCode = GetSafeString(root, "return_code").Trim();

            if (returnCode == "0")
            {
                if (root.TryGetProperty("prm_trde_trnsn", out JsonElement trendArray) && trendArray.ValueKind == JsonValueKind.Array)
                {
                    if (trendArray.GetArrayLength() > 0)
                    {
                        JsonElement firstItem = trendArray[0];

                        // 1. 원본 문자열 추출
                        string 원본_문자열 = GetSafeString(firstItem, "all_netprps");

                        // 2. 파싱 방해꾼 제거 (공백 제거, 콤마 제거, --를 -로 변경)
                        string 정제된_문자열 = 원본_문자열.Trim().Replace(",", "").Replace("--", "-");

                        // 3. 변환
                        if (int.TryParse(정제된_문자열, out int 프로그램))
                        {
                            if (req_mrkt_tp == "P001_AL01")
                            {
                                Form1.Acc.피_프로그램 = 프로그램 / 100;
                                Jisu_linkage.지수업종별연동("코스피");
                                TR_요청.코스닥_프로그램매매추이요청시간대별(false);
                            }
                            else if (req_mrkt_tp == "P101_AL02")
                            {
                                Form1.Acc.닥_프로그램 = 프로그램 / 100;
                                Jisu_linkage.지수업종별연동("코스닥");

                                if (!Form1.로딩완료) Form1.매매시작 = "Loding_11_미체결요청";
                            }
                        }
                        else
                        {
                            Form1.Console_print($"[오류] 파싱 실패. 정제된 데이터: '{정제된_문자열}'");
                        }
                    }
                    else
                    {
                        if (req_mrkt_tp == "P001_AL01")
                        {
                            Form1.Acc.피_프로그램 = 0; // 0으로 안전하게 세팅
                            TR_요청.코스닥_프로그램매매추이요청시간대별(false); // 멈추지 않고 릴레이 이어가기
                        }
                        else if (req_mrkt_tp == "P101_AL02")
                        {
                            Form1.Acc.닥_프로그램 = 0;
                            if (!Form1.로딩완료) Form1.매매시작 = "Loding_11_미체결요청"; // 로딩 릴레이 이어가기
                        }
                    }
                }
                Form1.TR유량제한 = false;
            }
            else
            {
                Form1.TR유량제한 = true;

                if (req_mrkt_tp == "P001_AL01") TR_요청.코스피_프로그램매매추이요청시간대별(true);
                if (req_mrkt_tp == "P101_AL02") TR_요청.코스닥_프로그램매매추이요청시간대별(true);
            }
        }

        private static void 처리_ka10004(JsonElement root, string req_stk_cd)
        {
            string itemcode = req_stk_cd.Split('_')[0];
            string returnCode = GetSafeString(root, "return_code").Trim();

            // 💡 [추가] 서버에서 보내주는 오류 메시지를 추출합니다.
            string returnMsg = GetSafeString(root, "return_msg").Trim();

            if (returnCode == "0")
            {
                if (root.TryGetProperty("stk_cd", out JsonElement stkCdEl))
                {
                    GET.StockState(itemcode,
                                   GetSafeString(root, "buy_1th_pre_req"),     // 매수1잔량 (마켓종료 판단 보조)
                                   GetSafeString(root, "buy_4th_pre_req"),     // 매수4잔량 
                                   GetSafeString(root, "buy_5th_pre_req"),     // 매수5잔량
                                   GetSafeString(root, "buy_6th_pre_req"),     // 매수6잔량
                                   GetSafeString(root, "buy_7th_pre_req"),     // 매수7잔량
                                   GetSafeString(root, "sel_1th_pre_req"),     // 매도1잔량
                                   GetSafeString(root, "sel_4th_pre_req"),     // 매도4잔량
                                   GetSafeString(root, "sel_5th_pre_req"),     // 매도5잔량
                                   GetSafeString(root, "sel_6th_pre_req"),     // 매도6잔량
                                   GetSafeString(root, "sel_7th_pre_req")      // 매도7잔량
                               );
                }
                Form1.TR유량제한 = false;
            }
            else
            {
                Form1.TR유량제한 = true;

                // 💡 [안전장치] 만약 딕셔너리에 없는 이상한 종목코드가 들어와도 프로그램이 뻗지 않게 보호합니다.
                string 종목명 = "알수없음";
                if (Form1.Market_Item_List.TryGetValue(itemcode, out var item))
                {
                    종목명 = item.종목명;
                }

                // ==========================================================
                // 💡 [강화된 오류 로그] 호가요청 실패 원인 상세 분석
                // ==========================================================
                Form1.Console_print($"[오류 ka10004] 주식호가요청 실패!");
                Form1.Console_print($"   ▶ 요청종목: {종목명}({itemcode})");
                Form1.Console_print($"   ▶ 오류코드: [{returnCode}]");
                Form1.Console_print($"   ▶ 원인메시지: {returnMsg}");
                Form1.Console_print($"   ▶ 조치: 재요청 진행 중...");

                TR_요청.주식호가요청(itemcode, true);
            }
        }

        // =========================================================================
        // [함수 분리 3] 시세 재요청 헬퍼 함수
        // =========================================================================
        private static void 시세_재요청(string tr_id, string req_mrkt_tp, string req_stk_cd)
        {
            Form1.TR유량제한 = true;

            if (tr_id.Equals("ka90005"))
            {
                if (req_mrkt_tp == "P001_AL01") TR_요청.코스피_프로그램매매추이요청시간대별(true);
                if (req_mrkt_tp == "P101_AL02") TR_요청.코스닥_프로그램매매추이요청시간대별(true);
            }
            else if (tr_id.Equals("ka10004"))
            {
                string itemcode = req_stk_cd.Split('_')[0];
                TR_요청.주식호가요청(itemcode, true);
            }
        }
    }
}