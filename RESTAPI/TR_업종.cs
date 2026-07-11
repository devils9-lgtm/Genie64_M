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
    internal class TR_업종
    {
        private static readonly HttpClient client = new HttpClient();

        // 헬퍼: 안전하게 string 가져오기
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

        public static async Task 업종(string token, object data, string tr_id, string cont_yn = "N", string next_key = "")
        {
            string host = "https://api.kiwoom.com";
            if (GenieConfig.checkBox_Simulation) host = "https://mockapi.kiwoom.com";

            string endpoint = "/api/dostk/sect";
            string url = host + endpoint;

            // [핵심 1] 재요청에 필요한 변수를 try 블록 밖으로 꺼냅니다!
            string req_inds_cd = "";
            string req_mrkt_tp = "";

            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (JsonDocument reqDoc = JsonDocument.Parse(json))
                {
                    JsonElement reqRoot = reqDoc.RootElement;
                    req_inds_cd = GetSafeString(reqRoot, "inds_cd");
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

                    // 바로 여기서 SocketException이 터지면 즉시 아래 catch 문으로 점프합니다!
                    using (var response = await client.SendAsync(request))
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

                        // =================================================================
                        // [제1 방어선] HTTP 에러 및 HTML 반환 시 안전하게 재요청
                        // =================================================================
                        if (!response.IsSuccessStatusCode)
                        {
                        //    Form1.Console_print($"[HTTP 에러] 상태코드: {response.StatusCode} -> 업종 재요청 실행");
                            업종_재요청(tr_id, req_inds_cd, req_mrkt_tp);
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(responseBody))
                        {
                            Form1.Console_print("[심각] 서버 응답 본문이 비어있습니다 -> 업종 재요청 실행");
                            업종_재요청(tr_id, req_inds_cd, req_mrkt_tp);
                            return;
                        }

                        if (responseBody.TrimStart().StartsWith("<"))
                        {
                            Form1.Console_print($"[심각] 서버가 JSON 대신 HTML 페이지를 반환했습니다 -> 업종 재요청 실행");
                            업종_재요청(tr_id, req_inds_cd, req_mrkt_tp);
                            return;
                        }

                        // =================================================================
                        // [핵심] JSON 파싱 후, TR별로 분리된 함수 호출
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

                                if (tr_id.Equals("ka20001"))
                                {
                                    처리_ka20001(root, req_inds_cd);
                                }
                                else if (tr_id.Equals("ka10051"))
                                {
                                    처리_ka10051(root, req_mrkt_tp);
                                }
                            }
                        }
                        catch (JsonException)
                        {
                            Form1.Console_print($"[JSON 파싱 실패] 서버 응답이 올바른 JSON 형식이 아닙니다 -> 업종 재요청 실행");
                            업종_재요청(tr_id, req_inds_cd, req_mrkt_tp);
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
                업종_재요청(tr_id, req_inds_cd, req_mrkt_tp);
            }
            catch (TaskCanceledException)
            {
                Form1.Console_print($"[타임아웃] 키움 서버 응답 지연 -> 3초 대기 후 재요청");
                await Task.Delay(3000);
                업종_재요청(tr_id, req_inds_cd, req_mrkt_tp);
            }
            catch (Exception error)
            {
                Form1.Console_print("[DEBUG] TR_업종 요청 중 치명적 에러: " + error.Message);
            }
        }
        // =========================================================================
        // [함수 분리 1] 업종현재가 (ka20001) 처리
        // =========================================================================
        private static void 처리_ka20001(JsonElement root, string req_inds_cd)
        {
            string inds_cd = !string.IsNullOrEmpty(req_inds_cd) ? req_inds_cd : GetSafeString(root, "inds_cd");
            string returnCode = GetSafeString(root, "return_code");

            if (returnCode == "0")
            {
                if (root.TryGetProperty("cur_prc", out JsonElement curPrcEl))
                {
                    string 지수등락율 = GetSafeString(root, "flu_rt").Trim();
                    string 지수현재가 = GetSafeString(root, "cur_prc").Trim();
                    string 지수고가 = GetSafeString(root, "high_pric").Trim();
                    string 지수저가 = GetSafeString(root, "low_pric").Trim();
                    string 누적거래대금 = GetSafeString(root, "trde_prica").Trim();

                    if (지수저가.Equals("-0.00")) 지수저가 = 지수현재가;
                    if (지수고가.Equals("-0.00")) 지수고가 = 지수현재가;

                    if (inds_cd == "001") // 코스피
                    {
                        RealData_Management.Market_update("001", 지수등락율, 지수현재가, 지수저가, 지수고가);
                        RealData_Management.Market_fluctuate("001", 누적거래대금, GetSafeString(root, "upl"), GetSafeString(root, "rising"), GetSafeString(root, "stdns"), GetSafeString(root, "fall"), GetSafeString(root, "lst"));
                        Log.동작기록("코스피 시세 확인.");
                        Form1.매매시작 = "Loding_04_코스닥현재가요청";
                    }
                    else if (inds_cd == "101") // 코스닥
                    {
                        RealData_Management.Market_update("101", 지수등락율, 지수현재가, 지수저가, 지수고가);
                        RealData_Management.Market_fluctuate("101", 누적거래대금, GetSafeString(root, "upl"), GetSafeString(root, "rising"), GetSafeString(root, "stdns"), GetSafeString(root, "fall"), GetSafeString(root, "lst"));
                        Log.동작기록("코스닥 시세 확인.");
                        Form1.매매시작 = "Loding_05_코스피분봉조회요청";
                    }
                    Form1.TR유량제한 = false;
                }
                else
                {
                    Form1.Console_print("[DEBUG] 'cur_prc' 키가 응답에 없습니다.");
                }
            }
            else
            {
                string msg = GetSafeString(root, "return_msg");
                Form1.Console_print($"[DEBUG] ka20001 실패. 메세지: {msg}");

                Form1.TR유량제한 = true;
                if (inds_cd == "001") TR_loding.코스피현재가요청(true);
                if (inds_cd == "101") TR_loding.코스닥현재가요청(true);
            }
        }

        // =========================================================================
        // [함수 분리 2] 투자자순매수 (ka10051) 처리
        // =========================================================================
        private static void 처리_ka10051(JsonElement root, string req_mrkt_tp)
        {
            string returnCode = GetSafeString(root, "return_code");

            if (returnCode == "0")
            {
                if (root.TryGetProperty("inds_netprps", out JsonElement netPrpsArray) && netPrpsArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement item in netPrpsArray.EnumerateArray())
                    {
                        string inds_cd = GetSafeString(item, "inds_cd");

                        if (inds_cd.Equals("001_AL") || inds_cd.Equals("101_AL"))
                        {
                            int 외국인순매수 = int.Parse(GetSafeString(item, "frgnr_netprps")); //frgnr_netprps
                            int 기관계순매수 = int.Parse(GetSafeString(item, "orgn_netprps"));

                            if (inds_cd.Equals("001_AL"))
                            {
                                Form1.Acc.피_외인 = 외국인순매수;
                                Form1.Acc.피_기관 = 기관계순매수;
                                Jisu_linkage.지수업종별연동("코스피");
                                TR_요청.코스닥투자자순매수요청(false);
                            }
                            else if (inds_cd.Equals("101_AL"))
                            {
                                Form1.Acc.닥_외인 = 외국인순매수;
                                Form1.Acc.닥_기관 = 기관계순매수;
                                Jisu_linkage.지수업종별연동("코스닥");
                                if (!Form1.로딩완료) Form1.매매시작 = "Loding_10_프로그램순매수요청";
                            }
                        }
                    }
                    Form1.TR유량제한 = false;
                }
                else
                {
                    Form1.Console_print("[DEBUG] inds_netprps 배열이 없거나 비어있습니다.");
                }
            }
            else
            {
                string msg = GetSafeString(root, "return_msg");
                Form1.Console_print($"[DEBUG] ka10051 실패. 메세지: {msg}");

                Form1.TR유량제한 = true;
                if (req_mrkt_tp == "0") TR_요청.코스피투자자순매수요청(true);
                if (req_mrkt_tp == "1") TR_요청.코스닥투자자순매수요청(true);
            }
        }

        // =========================================================================
        // [함수 분리 3] 업종 재요청 헬퍼 함수
        // =========================================================================
        private static void 업종_재요청(string tr_id, string req_inds_cd, string req_mrkt_tp)
        {
            Form1.TR유량제한 = true;

            if (tr_id.Equals("ka20001"))
            {
                if (req_inds_cd == "001") TR_loding.코스피현재가요청(true);
                if (req_inds_cd == "101") TR_loding.코스닥현재가요청(true);
            }
            else if (tr_id.Equals("ka10051"))
            {
                if (req_mrkt_tp == "0") TR_요청.코스피투자자순매수요청(true);
                if (req_mrkt_tp == "1") TR_요청.코스닥투자자순매수요청(true);
            }
        }
    }
}