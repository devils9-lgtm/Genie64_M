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
    internal class TR_차트
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task 차트(string token, object data, string tr_id, string cont_yn = "Y", string next_key = "")
        {
            string host = "https://api.kiwoom.com";
            if (GenieConfig.checkBox_Simulation) host = "https://mockapi.kiwoom.com";

            string endpoint = "/api/dostk/chart";
            string url = host + endpoint;

            // [핵심 1] 재요청에 필요한 변수를 try 블록 밖으로 꺼냅니다!
            string req_stk_cd = "";
            string req_inds_cd = "";

            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (JsonDocument reqDoc = JsonDocument.Parse(json))
                {
                    if (reqDoc.RootElement.TryGetProperty("stk_cd", out JsonElement val1)) req_stk_cd = val1.GetString();
                    if (reqDoc.RootElement.TryGetProperty("inds_cd", out JsonElement val2)) req_inds_cd = val2.GetString();
                }

                // === [수술 완료] 공용 헤더를 건드리지 않고, 전용 편지봉투를 만듭니다 ===
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    request.Headers.Add("cont-yn", cont_yn);
                    request.Headers.Add("next-key", next_key);
                    request.Headers.Add("api-id", tr_id);

                    request.Content = content;

                    // 바로 여기서 통신이 끊기면 catch 문으로 안전하게 점프합니다!
                    using (var response = await client.SendAsync(request))
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

                        // =================================================================
                        // [제1 방어선] HTTP 에러 및 HTML 반환 시 안전하게 재요청
                        // =================================================================
                        if (!response.IsSuccessStatusCode)
                        {
                            // [*] [수정됨] 기존 코드에 빠져있던 재요청 호출과 return을 추가했습니다.
                           // Form1.Console_print($"[HTTP 에러] 상태코드: {response.StatusCode} -> 차트 재요청 실행");
                            차트_재요청(tr_id, req_stk_cd, req_inds_cd, cont_yn, next_key);
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(responseBody))
                        {
                            Form1.Console_print("[심각] 차트 응답 본문이 비어있습니다 -> 차트 재요청 실행");
                            차트_재요청(tr_id, req_stk_cd, req_inds_cd, cont_yn, next_key);
                            return;
                        }

                        if (responseBody.TrimStart().StartsWith("<"))
                        {
                            Form1.Console_print($"[심각] 서버가 JSON 대신 HTML 페이지를 반환했습니다 -> 차트 재요청 실행");
                            차트_재요청(tr_id, req_stk_cd, req_inds_cd, cont_yn, next_key);
                            return;
                        }
                        // =================================================================

                        // =================================================================
                        // [핵심] JSON 파싱 후, TR별로 분리된 함수 호출
                        // =================================================================
                        try
                        {
                            // ==========================================================
                            // [*] 1. [긴급 검사] JSON 파싱 전, 토큰 만료 에러부터 가장 먼저 요격!
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

                                if (tr_id.Equals("ka10080"))
                                {
                                    처리_ka10080(root, req_stk_cd);
                                }
                                else if (tr_id.Equals("ka10081"))
                                {
                                    처리_ka10081(root, req_stk_cd);
                                }
                                else if (tr_id.Equals("ka20005"))
                                {
                                    처리_ka20005(root, req_inds_cd);
                                }
                                else if (tr_id.Equals("ka20006"))
                                {
                                    처리_ka20006(root, req_inds_cd);
                                }
                            }
                        }
                        catch (JsonException)
                        {
                            Form1.Console_print($"[JSON 파싱 실패] 서버 응답이 올바른 JSON 형식이 아닙니다 -> 차트 재요청 실행");
                            차트_재요청(tr_id, req_stk_cd, req_inds_cd, cont_yn, next_key);
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
                차트_재요청(tr_id, req_stk_cd, req_inds_cd, cont_yn, next_key);
            }
            catch (TaskCanceledException)
            {
                Form1.Console_print($"[타임아웃] 키움 서버 응답 지연 -> 3초 대기 후 재요청");
                await Task.Delay(3000);
                차트_재요청(tr_id, req_stk_cd, req_inds_cd, cont_yn, next_key);
            }
            catch (Exception error)
            {
                Form1.Console_print("[DEBUG] TR_차트 요청 중 치명적 에러: " + error.Message);
            }
        }

        internal static void 차트_재요청(string tr_id, string req_stk_cd, string req_inds_cd, string cont_yn, string next_key)
        {
          //  Form1.Console_print($"[재요청] 차트 통신 복구를 시도합니다. (TR: {tr_id})");

            // [핵심 디테일] 원본 함수에서 "_AL"을 다시 붙이므로, 여기서 꼬리표를 떼어내고 순수 코드(6자리)만 추출합니다.
            string pure_stk_cd = req_stk_cd.Split('_')[0];

            if (tr_id == "ka10080")
            {
                // 주식 분봉 차트
             TR_요청.   주식분봉차트조회요청(pure_stk_cd, true);
            }
            else if (tr_id == "ka10081")
            {
                // 주식 일봉 차트
                TR_요청.주식일봉차트조회요청(pure_stk_cd, true);
            }
            else if (tr_id == "ka20005")
            {
                // 업종 분봉 차트 (업종코드는 꼬리표가 없으므로 그대로 사용)
                TR_loding.업종분봉조회요청(req_inds_cd, true);
            }
            else if (tr_id == "ka20006")
            {
                // 업종 일봉 차트
                TR_loding.업종일봉조회요청(req_inds_cd, true);
            }
            else
            {
                Form1.Console_print($"[재요청 실패] 알 수 없는 차트 TR입니다 -> {tr_id}");
            }
        }

        // =========================================================================
        // [공용 헬퍼] 리턴 코드 성공(0) 여부 확인
        // =========================================================================
        private static bool IsReturnCodeSuccess(JsonElement rootEl)
        {
            if (rootEl.TryGetProperty("return_code", out JsonElement rc))
            {
                if (rc.ValueKind == JsonValueKind.String) return rc.GetString() == "0";
                if (rc.ValueKind == JsonValueKind.Number) return rc.GetInt32() == 0;
            }
            return false;
        }

        // =========================================================================
        // [함수 분리 1] 주식분봉차트조회요청 (ka10080)
        // =========================================================================
        private static void 처리_ka10080(JsonElement root, string req_stk_cd)
        {
            if (IsReturnCodeSuccess(root))
            {
                if (root.TryGetProperty("stk_min_pole_chart_qry", out JsonElement chartArray) &&
                    chartArray.ValueKind == JsonValueKind.Array &&
                    chartArray.GetArrayLength() > 0)
                {
                    string itemcode = "";
                    if (root.TryGetProperty("stk_cd", out JsonElement cdEl))
                        itemcode = cdEl.GetString().Split('_')[0];
                    else
                        itemcode = req_stk_cd.Split('_')[0]; // 응답에 없으면 요청 변수 활용

                        if (Form1.stockBalanceList.TryGetValue(itemcode, out Stockbalance 잔고))
                        {
                        
                        //Form1.Console_print("[주식분봉차트조회요청 ] ----------------------------------" + 잔고.종목명);
                         
                        StringBuilder sb = new StringBuilder();
                            int cart_count = 0;

                            foreach (JsonElement item in chartArray.EnumerateArray())
                            {
                                if (item.TryGetProperty("cur_prc", out JsonElement prcEl))
                                {
                                    if (double.TryParse(prcEl.GetString(), out double 현재가))
                                    {
                                        if (sb.Length > 0) sb.Append(";");
                                        sb.Append(Math.Abs(현재가));
                                    }
                                }
                                if (cart_count == 299) break;
                                cart_count++;
                            }
                            잔고.분_리스트 = sb.ToString();
                            MA.Get_Min_Moving_Average(잔고);
                        }
                }
                Form1.TR유량제한 = false;
            }
            else
            {
                Form1.TR유량제한 = true;
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                Form1.Console_print("주식분봉차트조회요청 Body error: " + JsonSerializer.Serialize(root, options));
                TR_요청.주식분봉차트조회요청(req_stk_cd.Split('_')[0], true);
            }
        }

        // =========================================================================
        // [함수 분리 2] 주식일봉차트조회요청 (ka10081)
        // =========================================================================
        private static void 처리_ka10081(JsonElement root, string req_stk_cd)
        {
            if (IsReturnCodeSuccess(root))
            {
                Form1.TR유량제한 = false;

                if (root.TryGetProperty("stk_dt_pole_chart_qry", out JsonElement chartArray) &&
                    chartArray.ValueKind == JsonValueKind.Array &&
                    chartArray.GetArrayLength() > 0)
                {
                    string rawCode = req_stk_cd;
                    if (root.TryGetProperty("stk_cd", out JsonElement cdEl)) rawCode = cdEl.GetString();
                    string itemcode = rawCode.Split('_')[0].Trim();

                    long 최고가120일 = 0;
                    long 오늘시가 = 0;
                    int 봉카운트 = 0;
                    bool 불량종목_감지됨 = false;

                    StringBuilder sb = new StringBuilder();

                    foreach (JsonElement item in chartArray.EnumerateArray())
                    {
                        if (item.TryGetProperty("cur_prc", out JsonElement prcEl))
                        {
                            if (double.TryParse(prcEl.GetString(), out double 현재가_double))
                            {
                                long 현재가_long = (long)Math.Abs(현재가_double);

                                if (봉카운트 == 0)
                                {
                                    if (item.TryGetProperty("open_pric", out JsonElement openEl))
                                        오늘시가 = (long)Math.Abs(double.Parse(openEl.GetString()));
                                }
                                else if (봉카운트 >= 1 && 봉카운트 <= 120)
                                {
                                    if (현재가_long > 최고가120일) 최고가120일 = 현재가_long;
                                }

                                if (봉카운트 < 10 && !불량종목_감지됨)
                                {
                                    long O = 0, H = 0, L = 0;
                                    if (item.TryGetProperty("open_pric", out JsonElement oEl)) O = (long)Math.Abs(double.Parse(oEl.GetString()));
                                    if (item.TryGetProperty("high_pric", out JsonElement hEl)) H = (long)Math.Abs(double.Parse(hEl.GetString()));
                                    if (item.TryGetProperty("low_pric", out JsonElement lEl)) L = (long)Math.Abs(double.Parse(lEl.GetString()));

                                    if (O != 0 && O == H && H == L && L == 현재가_long)
                                    {
                                        불량종목_감지됨 = true;
                                        Form1.Console_print($"[필터링] {itemcode} {봉카운트}일전 이상감지 -> 시:{O:N0} / 고:{H:N0} / 저:{L:N0} / 종:{현재가_long:N0}");
                                    }
                                }

                                if (sb.Length > 0) sb.Append(";");
                                sb.Append(현재가_long);
                            }
                        }

                        if (++봉카운트 > 299) break;
                    }

                    if (Ranking.통합랭킹딕셔너리.TryGetValue(itemcode, out var 랭킹종목))
                    {
                        랭킹종목.종가120일최고 = (int)최고가120일;
                        랭킹종목.시가 = (int)오늘시가;
                        랭킹종목.최근10일이내이상감지 = 불량종목_감지됨;
                    }

                    if (Form1.stockBalanceList.TryGetValue(itemcode, out Stockbalance 잔고))
                    {
                        잔고.일_리스트 = sb.ToString();
                        MA.Get_Day_Moving_Average(잔고);
                    }
                }
            }
            else
            {
                Form1.TR유량제한 = true;
                string rawCode = req_stk_cd;
                if (root.TryGetProperty("stk_cd", out JsonElement cdEl)) rawCode = cdEl.GetString();
                if (!string.IsNullOrEmpty(rawCode))
                    TR_요청.주식일봉차트조회요청(rawCode.Split('_')[0], true);
            }
        }

        // =========================================================================
        // [함수 분리 3] 업종분봉조회요청 (ka20005)
        // =========================================================================
        private static void 처리_ka20005(JsonElement root, string req_inds_cd)
        {
            string itemcode = req_inds_cd;
            if (root.TryGetProperty("inds_cd", out JsonElement cdEl)) itemcode = cdEl.GetString();

            if (IsReturnCodeSuccess(root))
            {
                if (root.TryGetProperty("inds_min_pole_qry", out JsonElement chartArray) &&
                    chartArray.ValueKind == JsonValueKind.Array &&
                    chartArray.GetArrayLength() > 0)
                {
                    foreach (JsonElement item in chartArray.EnumerateArray())
                    {
                        double 현재가 = 0;
                        if (item.TryGetProperty("cur_prc", out JsonElement prcEl))
                            double.TryParse(prcEl.GetString(), out 현재가);

                        long Time = 0;
                        if (item.TryGetProperty("cntr_tm", out JsonElement tmEl))
                            long.TryParse(tmEl.GetString().Trim(), out Time);

                        if (itemcode == "001")
                        {
                            if (Form1.kospi_min_count < Form1.kospi_avg_min.Length)
                            {
                                Form1.kospi_avg_min[Form1.kospi_min_count] = new AVG_price((Math.Abs(현재가) / 100), Time);
                                Form1.kospi_min_count++;
                                if (Form1.kospi_min_count == 59) break;
                            }
                        }
                        if (itemcode == "101")
                        {
                            if (Form1.kosdaq_min_count < Form1.kosdaq_avg_min.Length)
                            {
                                Form1.kosdaq_avg_min[Form1.kosdaq_min_count] = new AVG_price((Math.Abs(현재가) / 100), Time);
                                Form1.kosdaq_min_count++;
                                if (Form1.kosdaq_min_count == 59) break;
                            }
                        }
                    }
                    if (itemcode.Equals("001")) Form1.매매시작 = "Loding_06_코스피일봉조회요청";
                    if (itemcode.Equals("101")) Form1.매매시작 = "Loding_08_코스닥일봉조회요청";
                }
                Form1.TR유량제한 = false;
            }
            else
            {
                Form1.TR유량제한 = true;
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                Form1.Console_print("업종분봉조회요청 Body error: " + JsonSerializer.Serialize(root, options));
                TR_loding.업종분봉조회요청(itemcode, true);
            }
        }

        // =========================================================================
        // [함수 분리 4] 업종일봉조회요청 (ka20006)
        // =========================================================================
        private static void 처리_ka20006(JsonElement root, string req_inds_cd)
        {
            string itemcode = req_inds_cd;
            if (root.TryGetProperty("inds_cd", out JsonElement cdEl)) itemcode = cdEl.GetString();

            if (IsReturnCodeSuccess(root))
            {
                if (root.TryGetProperty("inds_dt_pole_qry", out JsonElement chartArray) &&
                    chartArray.ValueKind == JsonValueKind.Array &&
                    chartArray.GetArrayLength() > 0)
                {
                    foreach (JsonElement item in chartArray.EnumerateArray())
                    {
                        double 현재가 = 0;
                        if (item.TryGetProperty("cur_prc", out JsonElement prcEl))
                            double.TryParse(prcEl.GetString(), out 현재가);

                        long Time = 0;
                        if (item.TryGetProperty("dt", out JsonElement dtEl))
                            long.TryParse(dtEl.GetString().Trim(), out Time);

                        if (itemcode == "001")
                        {
                            if (Form1.kospi_day_count < Form1.kospi_avg_day.Length)
                            {
                                Form1.kospi_avg_day[Form1.kospi_day_count] = new AVG_price((Math.Abs(현재가) / 100), Time);
                                Form1.kospi_day_count++;
                                if (Form1.kospi_day_count == 59) break;
                            }
                        }
                        if (itemcode == "101")
                        {
                            if (Form1.kosdaq_day_count < Form1.kosdaq_avg_day.Length)
                            {
                                Form1.kosdaq_avg_day[Form1.kosdaq_day_count] = new AVG_price((Math.Abs(현재가) / 100), Time);
                                Form1.kosdaq_day_count++;
                                if (Form1.kosdaq_day_count == 59) break;
                            }
                        }
                    }

                    if (itemcode.Equals("101"))
                    {
                        RealData_Management.AVG_jisu_print("001", Form1.kospi_avg_min[0].Endprice);
                        RealData_Management.AVG_jisu_print("101", Form1.kosdaq_avg_min[0].Endprice);

                        if (!Form1.로딩완료)
                        {
                            Form1.매매시작 = "Loding_09_업종순매수요청";
                        }
                    }
                    else if (itemcode.Equals("001"))
                    {
                        Form1.매매시작 = "Loding_07_코스닥분봉조회요청";
                    }
                }
                Form1.TR유량제한 = false;
            }
            else
            {
                Form1.TR유량제한 = true;
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                Form1.Console_print("업종일봉조회요청 Body error: " + JsonSerializer.Serialize(root, options));
                TR_loding.업종일봉조회요청(itemcode, true);
            }
        }

        // =========================================================================
        // [공용 헬퍼] 차트 재요청 (에러 시 호출)
        // =========================================================================
        private static void 차트_재요청(string tr_id, string req_stk_cd, string req_inds_cd)
        {
            Form1.TR유량제한 = true;

            if (tr_id.Equals("ka10080"))
            {
                TR_요청.주식분봉차트조회요청(req_stk_cd.Split('_')[0], true);
            }
            else if (tr_id.Equals("ka10081"))
            {
                TR_요청.주식일봉차트조회요청(req_stk_cd.Split('_')[0], true);
            }
            else if (tr_id.Equals("ka20005"))
            {
                TR_loding.업종분봉조회요청(req_inds_cd, true);
            }
            else if (tr_id.Equals("ka20006"))
            {
                TR_loding.업종일봉조회요청(req_inds_cd, true);
            }
        }
    }
}
