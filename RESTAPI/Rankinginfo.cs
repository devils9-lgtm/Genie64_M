using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using 지니64.RESTAPI;

namespace 지니64
{
    public class Rankinginfo
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

        public static async Task 순위정보(string token, object data, string tr_id, string cont_yn = "N", string next_key = "")
        {
            // 1. 요청할 API URL
            string host = "https://api.kiwoom.com"; // 실전투자
            if (GenieConfig.checkBox_Simulation) host = "https://mockapi.kiwoom.com"; // 모의투자

            string endpoint = "/api/dostk/rkinfo";
            string url = host + endpoint;

            // [핵심 1] 재요청에 필요한 변수를 try 블록 밖으로 꺼냅니다!
            string req_mrkt_tp = "";
            string req_stk_cd = "";

            try
            {
                // 3. HTTP POST 요청 데이터 준비
                var json = System.Text.Json.JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (System.Text.Json.JsonDocument reqDoc = System.Text.Json.JsonDocument.Parse(json))
                {
                    System.Text.Json.JsonElement reqRoot = reqDoc.RootElement;
                    req_mrkt_tp = GetSafeString(reqRoot, "mrkt_tp");
                    req_stk_cd = GetSafeString(reqRoot, "stk_cd");
                }

                // ==========================================================
                // [수술 완료] 공용 헤더를 건드리지 않고, 전용 편지봉투를 만듭니다!
                // ==========================================================
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    request.Headers.Add("cont-yn", cont_yn);
                    request.Headers.Add("next-key", next_key);
                    request.Headers.Add("api-id", tr_id); // TR명

                    request.Content = content;

                    // 통신 단절 시 여기서 바로 catch문으로 점프합니다! (PostAsync -> SendAsync 로 변경)
                    using (var response = await client.SendAsync(request))
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

                        // =================================================================
                        // [제1 방어선] HTTP 에러 및 HTML 반환 시 안전하게 재요청
                        // =================================================================
                        if (!response.IsSuccessStatusCode)
                        {
                          //  Form1.Console_print($"[HTTP 에러] 상태코드: {response.StatusCode} -> 순위정보 재요청 실행");
                            순위정보_재요청(tr_id, req_mrkt_tp, req_stk_cd); // 실제 사용하시는 재요청 함수명으로 맞춰주세요
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(responseBody) || responseBody.TrimStart().StartsWith("<"))
                        {
                            Form1.Console_print($"[심각] 서버가 JSON 대신 HTML 페이지를 반환했습니다 -> 순위정보 재요청 실행");
                            순위정보_재요청(tr_id, req_mrkt_tp, req_stk_cd);
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

                            using (System.Text.Json.JsonDocument doc = System.Text.Json.JsonDocument.Parse(responseBody))
                            {
                                System.Text.Json.JsonElement root = doc.RootElement;

                                if (tr_id.Equals("ka10027"))
                                {
                                    처리_ka10027(root);
                                }
                                else if (tr_id.Equals("ka10032"))
                                {
                                    처리_ka10032(root);
                                }
                            }
                        }
                        catch (System.Text.Json.JsonException)
                        {
                            Form1.Console_print($"[JSON 파싱 실패] 서버 응답이 올바른 JSON 형식이 아닙니다 -> 순위정보 재요청 실행");
                            순위정보_재요청(tr_id, req_mrkt_tp, req_stk_cd);
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
                순위정보_재요청(tr_id, req_mrkt_tp, req_stk_cd);
            }
            catch (TaskCanceledException)
            {
                Form1.Console_print($"[타임아웃] 키움 서버 응답 지연 -> 3초 대기 후 재요청");
                await Task.Delay(3000);
                순위정보_재요청(tr_id, req_mrkt_tp, req_stk_cd);
            }
            catch (Exception error)
            {
                Form1.Console_print("[DEBUG] TR_순위정보 요청 중 치명적 에러: " + error.Message);
            }
        }

        internal static void 순위정보_재요청(string tr_id, string req_mrkt_tp, string req_stk_cd)
        {
           // Form1.Console_print($"[재요청] 순위정보 통신 복구를 시도합니다. (TR: {tr_id})");

            // 1. 에러가 났던 TR 아이디를 확인하고, 원래 있던 요청 함수를 최우선 순위(true)로 다시 부릅니다.
            if (tr_id == "ka10027")
            {
                전일대비등락률상위요청(true);
            }
            else if (tr_id == "ka10032")
            {
                거래대금상위요청(true);
            }
            else
            {
                Form1.Console_print($"[재요청 실패] 알 수 없는 순위정보 TR입니다 -> {tr_id}");
            }
        }

        // =========================================================================
        // 아래부터는 밖으로 빼낸 개별 TR 처리 함수들입니다.
        // =========================================================================

        private static void 처리_ka10027(JsonElement root)
        {
            string returnCode = GetSafeString(root, "return_code");
            string return_msg = GetSafeString(root, "return_msg");

            if (returnCode == "0")
            {
                Form1.키움_TR유량제한 = false;

                if (root.TryGetProperty("pred_pre_flu_rt_upper", out JsonElement listElement) && listElement.ValueKind == JsonValueKind.Array)
                {
                    int 출력갯수 = 0;

                    foreach (JsonElement item in listElement.EnumerateArray())
                    {
                        if (++출력갯수 > 100) break;

                        string 원본코드 = GetSafeString(item, "stk_cd");
                        string 종목코드 = 원본코드.Trim();

                        // _AL 제거
                        int underBarIndex = 원본코드.IndexOf('_');
                        if (underBarIndex > 0)
                        {
                            종목코드 = 원본코드.Substring(0, underBarIndex);
                        }

                        string 종목명 = GetSafeString(item, "stk_nm");
                        string 현재가 = GetSafeString(item, "cur_prc");
                        string 전일대비 = GetSafeString(item, "pred_pre");
                        string 등락률 = GetSafeString(item, "flu_rt");

                        // [리스트에 담기]
                        Ranking.등락률상위_List.Add(new 랭킹종목정보
                        {
                            종목코드 = 종목코드,
                            종목명 = 종목명,
                            현재가 = int.Parse(현재가),
                            등락률 = double.Parse(등락률),
                            거래대금 = 0
                        });

                        // [딕셔너리 업데이트]
                        var 종목 = Ranking.통합랭킹딕셔너리.GetOrAdd(종목코드, k => new 랭킹종목정보
                        {
                            종목코드 = 종목코드,
                            종목명 = 종목명
                        });

                        종목.데이터갱신(
                            현재가,
                            전일대비,
                            등락률,
                            "0",
                            0,
                            false
                        );
                    }
                    Ranking.응답완료카운트++;
                }
            }
            else
            {
                Form1.Console_print($"[오류 ka10027] 조회 실패 msg: {return_msg}");
                Form1.키움_TR유량제한 = true;
                // 재요청 로직이 있다면 여기서 호출
            }
        }

        private static void 처리_ka10032(JsonElement root)
        {
            string returnCode = GetSafeString(root, "return_code");
            string return_msg = GetSafeString(root, "return_msg");

            if (returnCode == "0")
            {
                Form1.키움_TR유량제한 = false;

                if (root.TryGetProperty("trde_prica_upper", out JsonElement listElement) && listElement.ValueKind == JsonValueKind.Array)
                {
                    int 출력갯수 = 0;

                    foreach (JsonElement item in listElement.EnumerateArray())
                    {
                        if (++출력갯수 > 35) break;

                        string 종목코드 = GetSafeString(item, "stk_cd").Trim();
                        string 종목명 = GetSafeString(item, "stk_nm");
                        string 현재가 = GetSafeString(item, "cur_prc");
                        string 등락률 = GetSafeString(item, "flu_rt");
                        string 거래대금 = GetSafeString(item, "trde_prica");
                        string 현재순위 = GetSafeString(item, "now_rank");

                        // [리스트에 담기]
                        Ranking.거래대금상위_List.Add(new 랭킹종목정보
                        {
                            종목코드 = 종목코드,
                            종목명 = 종목명,
                            현재가 = int.Parse(현재가),
                            등락률 = double.Parse(등락률),
                            거래대금 = long.Parse(거래대금)
                        });

                        // [딕셔너리 업데이트]
                        var 종목 = Ranking.통합랭킹딕셔너리.GetOrAdd(종목코드, k => new 랭킹종목정보
                        {
                            종목코드 = 종목코드,
                            종목명 = 종목명
                        });

                        종목.데이터갱신(
                            현재가,
                            GetSafeString(item, "pred_pre"),
                            "0",
                            거래대금,
                            int.Parse(현재순위),
                            true
                        );
                    }
                    Ranking.응답완료카운트++;
                }
            }
            else
            {
                Form1.Console_print($"[오류 ka10032] 조회 실패 msg: {return_msg}");
                Form1.키움_TR유량제한 = true;
                // 재요청 로직
            }
        }




        internal static void 전일대비등락률상위요청(bool Priority)
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10027", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10027", 요청);
            }

            static async Task 요청()
            {
                try
                {
                    // 1. 토큰 설정
                    string MY_ACCESS_TOKEN = Form1.API_token; // 접근 토큰

                    // 2. 요청 데이터
                    var paramsData = new
                    {
                        mrkt_tp = "000", // # 시장구분 000:전체, 001:코스피, 101:코스닥
                        sort_tp = "1", //# 정렬구분 1:상승률, 2:상승폭, 3:하락률, 4:하락폭, 5:보합
                        trde_qty_cnd = "0000", // # 거래량조건 0000:전체조회, 0010:만주이상, 0050:5만주이상, 0100:10만주이상, 0150:15만주이상, 0200:20만주이상, 0300:30만주이상, 0500:50만주이상, 1000:백만주이상
                        stk_cnd = "16", //# 종목조건 0:전체조회, 1:관리종목제외, 4:우선주+관리주제외, 3:우선주제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기, 11:정리매매종목제외, 12:증50만보기, 13:증60만보기, 14:ETF제외, 15:스펙제외, 16:ETF+ETN제외
                        crd_cnd = "0", //# 신용조건 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 7:신용융자E군, 9:신용융자전체
                        updown_incls = "1", //# 상하한포함 0:불 포함, 1:포함
                        pric_cnd = "0", //# 가격조건 0:전체조회, 1:1천원미만, 2:1천원~2천원, 3:2천원~5천원, 4:5천원~1만원, 5:1만원이상, 8:1천원이상, 10: 1만원미만
                        trde_prica_cnd = "0", //# 거래대금조건 0:전체조회, 3:3천만원이상, 5:5천만원이상, 10:1억원이상, 30:3억원이상, 50:5억원이상, 100:10억원이상, 300:30억원이상, 500:50억원이상, 1000:100억원이상, 3000:300억원이상, 5000:500억원이상
                        stex_tp = "1" //# 거래소구분 1:KRX, 2:NXT 3.통합
                    };

                    // 3. API 실행
                    await 순위정보(MY_ACCESS_TOKEN, paramsData, "ka10027");
                }
                catch (Exception ex)
                {
                    Form1.Console_print("전일대비등락률상위요청 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 거래대금상위요청(bool Priority)
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10032", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10032", 요청);
            }

            static async Task 요청()
            {
                try
                {
                    // 1. 토큰 설정
                    string MY_ACCESS_TOKEN = Form1.API_token; // 접근 토큰

                    // 2. 요청 데이터
                    var paramsData = new
                    {
                        mrkt_tp = "000",// # 시장구분 000:전체, 001:코스피, 101:코스닥
                        mang_stk_incls = "0",// # 관리종목포함 0:관리종목 미포함, 1:관리종목 포함
                        stex_tp = "1" //# 거래소구분 1:KRX, 2:NXT 3.통합
                    };

                    // 3. API 실행
                    await 순위정보(MY_ACCESS_TOKEN, paramsData, "ka10032");
                }
                catch (Exception ex)
                {
                    Form1.Console_print("거래대금상위요청 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 실시간종목조회순위(bool Priority)
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka00198", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka00198", 요청);
            }

            static async Task 요청()
            {
                try
                {
                    // 1. 토큰 설정
                    string MY_ACCESS_TOKEN = Form1.API_token; // 접근 토큰

                    // 2. 요청 데이터
                    var paramsData = new
                    {
                        qry_tp = "5" //# 구분 1:1분, 2:10분, 3:1시간, 4:당일 누적, 5:30초
                    };

                    // 3. API 실행
                    await TR_종목정보.종목정보(MY_ACCESS_TOKEN, paramsData, "ka00198", "N", "");
                }
                catch (Exception ex)
                {
                    Form1.Console_print("실시간종목조회순위 요청 실패: " + ex.Message);
                }
            }
        }


    }
}
