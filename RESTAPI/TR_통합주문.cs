using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64.RESTAPI
{
    internal class TR_통합주문
    {
        private static readonly HttpClient client = new HttpClient();

        private static async Task 통합주문(JumunItem jumun, string token, object data, string tr_id, string cont_yn = "N", string next_key = "")
        {
           
            // 1. [분기] tr_id를 보고 현금주문인지 신용주문인지 자동 판별
            bool isCreditOrder = tr_id.Equals("kt10006") || tr_id.Equals("kt10007") || tr_id.Equals("kt10009");

            // 2. 요청할 API URL 세팅
            string host = GenieConfig.checkBox_Simulation ? "https://mockapi.kiwoom.com" : "https://api.kiwoom.com";
            string endpoint = isCreditOrder ? "/api/dostk/crdordr" : "/api/dostk/ordr";
            string url = host + endpoint;

            try
            {
                // 4. HTTP POST 요청 및 데이터 백업 파싱
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // 재요청 및 로그를 위한 변수
                string req_dmst_stex_tp = "", req_stk_cd = "", req_ord_qty = "", req_ord_uv = "", req_trde_tp = "", req_cond_uv = "";
                string req_crd_deal_tp = "", req_crd_loan_dt = "";

                using (JsonDocument reqDoc = JsonDocument.Parse(json))
                {
                    JsonElement root = reqDoc.RootElement;
                    if (root.TryGetProperty("dmst_stex_tp", out JsonElement val1)) req_dmst_stex_tp = val1.GetString();
                    if (root.TryGetProperty("stk_cd", out JsonElement val2)) req_stk_cd = val2.GetString();
                    if (root.TryGetProperty("ord_qty", out JsonElement val3)) req_ord_qty = val3.GetString();
                    if (root.TryGetProperty("ord_uv", out JsonElement val4)) req_ord_uv = val4.GetString();
                    if (root.TryGetProperty("trde_tp", out JsonElement val5)) req_trde_tp = val5.GetString();
                    if (root.TryGetProperty("cond_uv", out JsonElement val6)) req_cond_uv = val6.GetString();

                    // 신용 전용 변수 (없으면 빈값 유지)
                    if (root.TryGetProperty("crd_deal_tp", out JsonElement val7)) req_crd_deal_tp = val7.GetString();
                    if (root.TryGetProperty("crd_loan_dt", out JsonElement val8)) req_crd_loan_dt = val8.GetString();
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

                    using (var response = await client.SendAsync(request))
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

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

                        // 5. 응답 결과 파싱
                        using (JsonDocument doc = JsonDocument.Parse(responseBody))
                        {
                            JsonElement root = doc.RootElement;
                            bool isSuccess = false;
                            string returnCodeStr = "";

                            if (root.TryGetProperty("return_code", out JsonElement retCode))
                            {
                                Form1.Get.주문_S++; Form1.Get.주문_M++; Form1.Get.주문_H++;

                                if (retCode.ValueKind == JsonValueKind.String)
                                {
                                    returnCodeStr = retCode.GetString();
                                    isSuccess = (returnCodeStr == "0");
                                }
                                else if (retCode.ValueKind == JsonValueKind.Number)
                                {
                                    returnCodeStr = retCode.ToString();
                                    isSuccess = (retCode.GetInt32() == 0);
                                }
                            }

                            string returnMsg = "";
                            if (root.TryGetProperty("return_msg", out JsonElement msgEl))
                                returnMsg = msgEl.GetString();

                            // ==========================================================
                            // [성공 처리] 주문번호 업데이트
                            // ==========================================================
                            if (isSuccess)
                            {
                                Form1.주문유량제한 = false;
                                // 매수/매도 TR일 경우에만 주문번호 갱신 (현금 00/01, 신용 06/07)
                                if (tr_id.Equals("kt10000") || tr_id.Equals("kt10001") || tr_id.Equals("kt10006") || tr_id.Equals("kt10007"))
                                {
                                    string 주문번호 = "";
                                    if (root.TryGetProperty("ord_no", out JsonElement ordNo))
                                    {
                                        주문번호 = ordNo.GetString();
                                    }

                                    string 매수매도 = "취소";
                                    if (jumun.매수매도 == 1) 매수매도 = "매수";
                                    else if (jumun.매수매도 == 2) 매수매도 = "매도";

                                    jumun = Jumun.UpdateKey(주문번호, jumun.Screennum, jumun.종목코드, jumun);
                                    string 로그 = $"[@@@ 주문성공] {jumun.종목명} 식: {jumun.검색식} 주문:{매수매도} 수량:{jumun.주문수량} 가격:{jumun.주문가격} 시장가:{jumun.시장가구분} 신용주문:{(jumun.신용주문 ? "신용" : "현금")} ScrNo:{jumun.Screennum}";
                                    Log.키움로그(로그, false);
                                }
                            }
                            // ==========================================================
                            // [실패 처리] 에러 및 재주문 통합 로직
                            // ==========================================================
                            else
                            {
                                string orderTypeStr = isCreditOrder ? "신용" : "현금";

                                // (1) 매수증거금 부족 ("20")
                                if (returnCodeStr == "20")
                                {
                                    if (returnMsg.Contains("매수증거금이"))
                                    {
                                        if (Form1.매수증거금)
                                        {
                                            Log.에러기록($"[키움서버 알림] {returnMsg} 종목명: {jumun.종목명} 주문이 취소되었습니다.");
                                            Helper.알림창_멀티("키움서버 알림", "매수증거금이 부족합니다. 최대 5분간 매수주문이 일시정지됩니다.", 10, false);

                                            // 실제 장부 업데이트
                                            Form1.Acc.추정D2 = Form1.Acc.D2;

                                            Form1.매수증거금 = false;
                                        }
                                    }

                                    if (!returnMsg.Contains("정정/취소할") && !returnMsg.Contains("매수증거금이"))
                                    {
                                     //   Helper.알림창_멀티("에러알림", $"{returnMsg}\n종목명: {jumun.종목명} 주문이 취소되었습니다.", 10, false);
                                        Log.에러기록($"종목명: {jumun.종목명} 주문취소 되었습니다. {returnMsg} ");
                                    }

                                    if (returnMsg.Contains("융자한도 초과종목"))
                                    {
                                        Form1.Market_Item_List[jumun.종목코드].신용가능 = false;
                                        Form1.stockBalanceList[jumun.종목코드].종목상태 = GET.Jango_state(jumun.종목코드);
                                    }

                                    Form1.Console_print($"\n\n[{orderTypeStr}주문 키움서버 알림] {returnMsg}\n종목명: {jumun.종목명} 주문이 취소되었습니다.");
                                    Form1.Console_print($"[order {orderTypeStr}] 시장: {req_dmst_stex_tp} | 종목: {req_stk_cd} | 수량: {req_ord_qty} | 가격: {req_ord_uv}");

                                    // 자물쇠 역할을 할 전용 객체 또는 리스트 자체를 사용해 잠급니다.
                                    lock (Form1.form1.Rebal_Sell_List)
                                    {
                                        // 1. 잠긴 상태에서 안전하게 위치를 찾습니다.
                                        int 삭제할인덱스 = Form1.form1.Rebal_Sell_List.FindIndex(o => o.종목코드 == jumun.종목코드 && o.Screennum == jumun.Screennum);

                                        // 2. 찾았을 경우 즉시 지웁니다. (이 과정이 끝날 때까지 다른 스레드는 접근 불가 대기 상태가 됩니다)
                                        if (삭제할인덱스 >= 0)
                                        {
                                            Form1.form1.Rebal_Sell_List.RemoveAt(삭제할인덱스);
                                        }
                                    }

                                    if (returnMsg.Contains("KRX주문이 아닙니다"))
                                    {
                                        Form1.Console_print($"NXT 주문취소");
                                        jumun.NXT = true;
                                        jumun.취소timer = 5;
                                        jumun.주문취소 = true;
                                        return;
                                    }

                                    if (returnMsg.Contains("NXT주문이 아닙니다"))
                                    {
                                        Form1.Console_print($"KRX 주문취소");

                                        jumun.NXT = false;
                                        jumun.취소timer = 5;
                                        jumun.주문취소 = true;
                                        return;
                                    }

                                    _ = Helper.미체결내역동기화(false);
                                    Jumun.ExecuteDelete(jumun);
                                    return;
                                }

                                // (2) 유량 제한 ("5") - 스위치(switch)문으로 완벽한 분기 처리
                                if (returnCodeStr == "5" && returnMsg.Contains("허용된 요청 개수를 초과"))
                                {
                                    Form1.주문유량제한 = true;

                                    switch (tr_id)
                                    {
                                        // 현금 재주문
                                        case "kt10000": 주식_매수(jumun, req_dmst_stex_tp, req_stk_cd, req_ord_qty, req_ord_uv, req_trde_tp, true); break;
                                        case "kt10001": 주식_매도(jumun, req_dmst_stex_tp, req_stk_cd, req_ord_qty, req_ord_uv, req_trde_tp, true); break;
                                        case "kt10003": 주식_취소(jumun, req_dmst_stex_tp, jumun.주문번호, req_stk_cd, true); break;

                                        // 신용 재주문
                                        case "kt10006": 신용_매수(jumun, req_dmst_stex_tp, req_stk_cd, req_ord_qty, req_ord_uv, req_trde_tp, true); break;
                                        case "kt10007": 신용_매도(jumun, req_dmst_stex_tp, req_stk_cd, req_ord_qty, req_ord_uv, req_trde_tp, req_crd_deal_tp, req_crd_loan_dt, true); break;
                                        case "kt10009": 신용_취소(jumun, req_dmst_stex_tp, jumun.주문번호, req_stk_cd, true); break;
                                    }
                                    return;
                                }

                                // (3) 입력값 형식 오류 ("2")
                                if (returnCodeStr == "2")
                                {
                                    Jumun.ExecuteDelete(jumun);
                                    return;
                                }

                                // (4) 그 외 기타 에러
                                Form1.Console_print($"################# [TR_주문 {orderTypeStr}주문 에러] #################");
                                Form1.Console_print($"[TR_{orderTypeStr}주문] return_code : {returnCodeStr}");
                                Form1.Console_print($"[TR_{orderTypeStr}주문] return_msg : {returnMsg}");
                                Form1.Console_print($"[order {orderTypeStr}] 시장 : {req_dmst_stex_tp}");
                                Form1.Console_print($"[order {orderTypeStr}] 종목코드 : {req_stk_cd}");
                                Form1.Console_print($"[order {orderTypeStr}] 주문수량 : {req_ord_qty}");
                                Form1.Console_print($"[order {orderTypeStr}] 주문가격 : {req_ord_uv}");
                                Form1.Console_print($"[order {orderTypeStr}] 매수매도 : {req_trde_tp}");
                                if (isCreditOrder) Form1.Console_print($"[order 신용] 대출일 : {req_crd_loan_dt}");

                                Log.키움로그($"[TR_{orderTypeStr}주문] return_msg : {returnMsg}", false);

                            }
                        }
                    } // using response 끝
                } // using request 끝
            }
            catch (Exception error)
            {
                Form1.Console_print("통합주문 요청 실패: " + error.Message);
            }
        }

        // =========================================================================
        // 💰 [현금 주문 래퍼 함수들]
        // =========================================================================
        internal static void 주식_매수(JumunItem jumun, string 거래소, string stk_cd, string ord_qty, string ord_uv, string trde_tp, bool Priority)
        {
            if (Priority) Form1.order_scheduler.EnqueuePriorityRequest(stk_cd + "_매수", 요청);
            else Form1.order_scheduler.EnqueueRequest(stk_cd + "_매수", 요청);

            async Task 요청()
            {
                try
                {
                    string MY_ACCESS_TOKEN = Form1.API_token;
                    var paramsData = new { dmst_stex_tp = 거래소, stk_cd, ord_qty, ord_uv, trde_tp, cond_uv = "" };
                    await 통합주문(jumun, MY_ACCESS_TOKEN, paramsData, "kt10000");
                }
                catch (Exception ex) { Form1.Console_print("주식_매수주문 요청 실패: " + ex.Message); }
            }
        }

        internal static void 주식_매도(JumunItem jumun, string 거래소, string stk_cd, string ord_qty, string ord_uv, string trde_tp, bool Priority)
        {
            if (Priority) Form1.order_scheduler.EnqueuePriorityRequest(stk_cd + "_매도", 요청);
            else Form1.order_scheduler.EnqueueRequest(stk_cd + "_매도", 요청);

            async Task 요청()
            {
                try
                {
                    string MY_ACCESS_TOKEN = Form1.API_token;
                    var paramsData = new { dmst_stex_tp = 거래소, stk_cd, ord_qty, ord_uv, trde_tp, cond_uv = "" };
                    await 통합주문(jumun, MY_ACCESS_TOKEN, paramsData, "kt10001");
                }
                catch (Exception ex) { Form1.Console_print("주식_매도주문 요청 실패: " + ex.Message); }
            }
        }

        internal static void 주식_취소(JumunItem jumun, string 거래소, string orig_ord_no, string stk_cd, bool Priority)
        {
            if (Priority) Form1.order_scheduler.EnqueuePriorityRequest(stk_cd + "_취소", 요청);
            else Form1.order_scheduler.EnqueueRequest(stk_cd + "_취소", 요청);

            async Task 요청()
            {
                try
                {
                    string MY_ACCESS_TOKEN = Form1.API_token;
                    var paramsData = new { dmst_stex_tp = 거래소, orig_ord_no, stk_cd, cncl_qty = "0" };

                    await 통합주문(jumun, MY_ACCESS_TOKEN, paramsData, "kt10003");
                }
                catch (Exception ex) { Form1.Console_print("주식_취소주문 요청 실패: " + ex.Message); }
            }
        }

        // =========================================================================
        // 💳 [신용 주문 래퍼 함수들]
        // =========================================================================
        internal static void 신용_매수(JumunItem jumun, string 거래소, string stk_cd, string ord_qty, string ord_uv, string trde_tp, bool Priority)
        {
            if (Priority) Form1.order_scheduler.EnqueuePriorityRequest(stk_cd + "_신용매수", 요청);
            else Form1.order_scheduler.EnqueueRequest(stk_cd + "_신용매수", 요청);

            async Task 요청()
            {
                try
                {
                    string MY_ACCESS_TOKEN = Form1.API_token;
                    var paramsData = new { dmst_stex_tp = 거래소, stk_cd, ord_qty, ord_uv, trde_tp, cond_uv = "" };
                    await 통합주문(jumun, MY_ACCESS_TOKEN, paramsData, "kt10006");
                }
                catch (Exception ex) { Form1.Console_print("신용_매수주문 요청 실패: " + ex.Message); }
            }
        }

        internal static void 신용_매도(JumunItem jumun, string 거래소, string stk_cd, string ord_qty, string ord_uv, string trde_tp, string crd_deal_tp, string crd_loan_dt, bool Priority)
        {
            if (Priority) Form1.order_scheduler.EnqueuePriorityRequest(stk_cd + "_신용매도", 요청);
            else Form1.order_scheduler.EnqueueRequest(stk_cd + "_신용매도", 요청);

            async Task 요청()
            {
                try
                {
                    string MY_ACCESS_TOKEN = Form1.API_token;
                    var paramsData = new { dmst_stex_tp = 거래소, stk_cd, ord_qty, ord_uv, trde_tp, crd_deal_tp, crd_loan_dt, cond_uv = "" };
                    await 통합주문(jumun, MY_ACCESS_TOKEN, paramsData, "kt10007");
                }
                catch (Exception ex) { Form1.Console_print("신용_매도주문 요청 실패: " + ex.Message); }
            }
        }

        internal static void 신용_취소(JumunItem jumun, string 거래소, string orig_ord_no, string stk_cd, bool Priority)
        {
            if (Priority) Form1.order_scheduler.EnqueuePriorityRequest(stk_cd + "_신용취소", 요청);
            else Form1.order_scheduler.EnqueueRequest(stk_cd + "_신용취소", 요청);

            async Task 요청()
            {
                try
                {
                    string MY_ACCESS_TOKEN = Form1.API_token;
                    var paramsData = new { dmst_stex_tp = 거래소, orig_ord_no, stk_cd, cncl_qty = "0" };

                    await 통합주문(jumun, MY_ACCESS_TOKEN, paramsData, "kt10009");
                }
                catch (Exception ex) { Form1.Console_print("신용_취소주문 요청 실패: " + ex.Message); }
            }
        }
    }
}