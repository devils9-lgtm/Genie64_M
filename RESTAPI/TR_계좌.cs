using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Text.Json; // System.Text.Json 사용
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using 지니64.box;

namespace 지니64
{
    internal class TR_계좌 : Form1
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

        public static async Task 계좌(string token, object data, string tr_, string cont_yn, string next_key)
        {
            string host = "https://api.kiwoom.com"; // 실전투자
            if (GenieConfig.checkBox_Simulation) host = "https://mockapi.kiwoom.com"; // 모의투자

            string endpoint = "/api/dostk/acnt";
            string url = host + endpoint;

            string tr_id = tr_.Split('_')[0];
            string where = tr_.Split('_')[1];

            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // [수술 완료] 공용 client의 헤더를 건드리지 않고, 독립적인 개별 요청(HttpRequestMessage) 객체를 생성합니다.
                // 스레드 충돌을 원천 차단하여 통신 에러를 방지합니다.
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    request.Headers.Add("cont-yn", cont_yn);
                    request.Headers.Add("next-key", next_key);
                    request.Headers.Add("api-id", tr_id);

                    request.Content = content;

                    // PostAsync 대신 SendAsync를 사용하여 개별 편지봉투를 전송합니다.
                    using (var response = await client.SendAsync(request))
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

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

                            if (tr_id.Equals("kt00004"))
                            {
                                Parse_kt00004_계좌평가현황요청(root, ref cont_yn, ref next_key);
                            }
                            else if (tr_id.Equals("ka10074") && where.Equals("일자별실현손익요청통계"))
                            {
                                Parse_ka10074_일자별실현손익통계(root, tr_, ref cont_yn, ref next_key);
                            }
                            else if (tr_id.Equals("ka10074") && where.Equals("실현손익요청"))
                            {
                                Parse_ka10074_실현손익요청(root, ref cont_yn, ref next_key);
                            }
                            else if (tr_id.Equals("ka10072"))
                            {
                                Parse_ka10072_당일실현손익요청(root, tr_, where, response, ref cont_yn, ref next_key);
                            }
                            else if (tr_id.Equals("kt00018"))
                            {
                                Parse_kt00018_계좌평가잔고내역(root, response, ref cont_yn, ref next_key);
                            }
                            else if (tr_id.Equals("ka10075"))
                            {
                                Parse_ka10075_미체결요청(root, response, ref cont_yn, ref next_key);
                            }
                            else if (tr_id.Equals("ka10085"))
                            {
                                Parse_ka10085_계좌수익률요청(root, response, ref cont_yn, ref next_key);
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Form1.Console_print("요청 실패: " + error.Message);
            }
        }

        // =================================================================================
        // 2. ka10074 (일자별실현손익요청_통계)
        // =================================================================================
        public static void Parse_ka10074_일자별실현손익통계(JsonElement root, string tr_, ref string cont_yn, ref string next_key)
        {
            Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                Form1.form1.CBB_통계.SelectedIndex = -1;
                Form1.form1.TB_월통계기준.Text = GenieConfig.TB_월통계기준.ToString("N0");
            });

            string returnCode = GetSafeString(root, "return_code");
            if (returnCode == "0")
            {
                string 총매수금액 = GetSafeString(root, "tot_buy_amt").Trim();
                string 총매도금액 = GetSafeString(root, "tot_sell_amt").Trim();
                string 실현손익 = GetSafeString(root, "rlzt_pl").Trim();
                string 매매수수료 = GetSafeString(root, "trde_cmsn").Trim();
                string 매매세금 = GetSafeString(root, "trde_tax").Trim();

                Form1.form1.매매내역_List.Add(new TradeLog("총합", 총매수금액, 총매도금액, 실현손익, 매매수수료, 매매세금));

                if (root.TryGetProperty("dt_rlzt_pl", out JsonElement listArray) && listArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement item in listArray.EnumerateArray())
                    {
                        string 일자 = GetSafeString(item, "dt").Trim();
                        string 매수금액 = GetSafeString(item, "buy_amt").Trim();
                        string 매도금액 = GetSafeString(item, "sell_amt").Trim();
                        string 당일매도손익 = GetSafeString(item, "tdy_sel_pl").Trim();
                        string 당일매매수수료 = GetSafeString(item, "tdy_trde_cmsn").Trim();
                        string 당일매매세금 = GetSafeString(item, "tdy_trde_tax").Trim();

                        Form1.form1.매매내역_List.Add(new TradeLog(일자, 매수금액, 매도금액, 당일매도손익, 당일매매수수료, 당일매매세금));
                    }
                }

                Helper.안전한_UI_업데이트(Form1.form1, () => { Form1.form1.CBB_통계.SelectedIndex = 0; });
                Form1.키움_TR유량제한 = false;
            }
            else
            {
                Form1.키움_TR유량제한 = true;
                TR_요청.일자별실현손익요청_통계(cont_yn, next_key, true);
            }
        }

        // =================================================================================
        // 3. ka10074 (실현손익요청)
        // =================================================================================
        public static void Parse_ka10074_실현손익요청(JsonElement root, ref string cont_yn, ref string next_key)
        {
            string returnCode = GetSafeString(root, "return_code");
            if (returnCode == "0")
            {
                string 실현손익_ = GetSafeString(root, "rlzt_pl").Trim();
                double.TryParse(실현손익_, out double 실현손익Result);

                long 손익률기준금 = GenieConfig.MT_sonik_price;
                double 매도손익률 = 실현손익Result != 0 ? 실현손익Result / (double)손익률기준금 * 100 : 0;
                double 실현손익률 = Math.Round(매도손익률, 2);

                Form1.Acc.실현손익 = (long)실현손익Result;
                Form1.Acc.실현손익률 = 실현손익률;

                if (!Form1.로딩완료)
                {
                    Log.동작기록(GenieConfig.textBox_계좌번호 + " 실현손익요청 조회 완료");
                    Console_print(GenieConfig.textBox_계좌번호 + " 실현손익요청 조회 완료");
                    Form1.매매시작 = "Loding_14_계좌평가잔고내역요청"; 
                }
                Form1.키움_TR유량제한 = false;
            }
            else
            {
                Form1.키움_TR유량제한 = true;
                TR_요청.실현손익요청(cont_yn, next_key, true);
            }
        }

        // =================================================================================
        // 4. ka10072 (당일실현손익요청 및 통계)
        // =================================================================================
        public static void Parse_ka10072_당일실현손익요청(JsonElement root, string tr_, string where, HttpResponseMessage response, ref string cont_yn, ref string next_key)
        {
            Helper.안전한_UI_업데이트(Form1.form1, () => { Form1.form1.CBB_통계.SelectedIndex = -1; });
            string returnCode = GetSafeString(root, "return_code");
            double 실현손익 = 0;

            if (returnCode == "0")
            {
                if (root.TryGetProperty("dt_stk_div_rlzt_pl", out JsonElement listArray) && listArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement item in listArray.EnumerateArray())
                    {
                        string 종목명 = GetSafeString(item, "stk_nm").Trim();
                        int 체결량_ = Helper.StringToInt(GetSafeString(item, "cntr_qty"));
                        double 매입단가_ = Helper.StringToDouble(GetSafeString(item, "buy_uv"));
                        double 체결가_ = Helper.StringToDouble(GetSafeString(item, "cntr_pric"));
                        string 손익율 = GetSafeString(item, "pl_rt").Trim();
                        string 당일매매수수료 = GetSafeString(item, "tdy_trde_cmsn").Trim();
                        string 당일매매세금 = GetSafeString(item, "tdy_trde_tax").Trim();
                        double.TryParse(GetSafeString(item, "tdy_sel_pl_1"), out double 당일매도손익);

                        double 매수금액 = (매입단가_ * (double)체결량_);
                        double 매도금액 = (체결가_ * (double)체결량_);

                        if (where == "일자별종목별실현손익요청통계")
                        {
                            Form1.form1.기준일매매내역_List.Add(new TradeLog(종목명, 매수금액, 매도금액, Math.Truncate(당일매도손익), 당일매매수수료, 당일매매세금, 체결가_.ToString(), 손익율));
                        }

                        if (where == "당일실현손익요청")
                        {
                            실현손익 += 당일매도손익;
                            long 손익률기준금 = GenieConfig.MT_sonik_price;
                            double 매도손익률 = 실현손익 != 0 ? 실현손익 / (double)손익률기준금 * 100 : 0;

                            Form1.Acc.실현손익 = (long)실현손익;
                            Form1.Acc.실현손익률 = Math.Round(매도손익률, 2);
                        }
                    }
                }

                if (response.Headers.TryGetValues("cont-yn", out var contYnValues)) cont_yn = contYnValues.FirstOrDefault();
                if (response.Headers.TryGetValues("next-key", out var nextKeyValues)) next_key = nextKeyValues.FirstOrDefault();

                if (cont_yn == "Y")
                {
                    Form1.키움_TR유량제한 = true;
                    if (where == "당일실현손익요청") TR_요청.당일실현손익요청(cont_yn, next_key, true);
                    else TR_요청.일자별종목별실현손익요청_통계(cont_yn, next_key, tr_.Split('_')[2], true);
                    return;
                }

                if (cont_yn == "N")
                {
                    Form1.키움_TR유량제한 = false;
                    if (where == "당일실현손익요청" && !Form1.매매시작.Equals("매매시작"))
                    {
                        Log.동작기록(GenieConfig.textBox_계좌번호 + " 당일실현손익요청 조회 완료");
                        Console_print(GenieConfig.textBox_계좌번호 + " 당일실현손익요청 조회 완료");
                        Form1.매매시작 = "계좌평가잔고내역요청_8";
                    }
                    Helper.안전한_UI_업데이트(Form1.form1, () =>
                    {
                        if (where == "일자별종목별실현손익요청통계" && Form1.form1.tab_잔고.SelectedIndex == 2)
                        {
                            Console_print(GenieConfig.textBox_계좌번호 + " 일자별종목별실현손익요청통계 조회 완료");
                            Statistical_chart.Print_기준일매매일지();
                        }
                    });
                    실현손익 = 0;
                }
            }
            else
            {
                Form1.키움_TR유량제한 = true;
                Console_print("return_code: " + returnCode);
                Console_print("return_message: " + GetSafeString(root, "return_msg"));
                Console_print($"실패 tr_ [{tr_}]");
                Console_print($"실현손익_통계 조회 실패 재요청 [{where}]");

                if (where == "당일실현손익요청") TR_요청.당일실현손익요청(cont_yn, next_key, true);
                else TR_요청.일자별종목별실현손익요청_통계(cont_yn, next_key, tr_.Split('_')[2], true);
            }
        }

        // =================================================================================
        // 6. ka10075 (미체결요청)
        // =================================================================================
        public static void Parse_ka10075_미체결요청(JsonElement root, HttpResponseMessage response, ref string cont_yn, ref string next_key)
        {
            string returnCode = GetSafeString(root, "return_code");

            if (returnCode == "0")
            {
                // [+] [지니 최적화] 데이터 유무와 상관없이 헤더 값(cont_yn)부터 무조건 뽑아냅니다.
                if (response.Headers.TryGetValues("cont-yn", out var contYnValues)) cont_yn = contYnValues.FirstOrDefault();
                if (response.Headers.TryGetValues("next-key", out var nextKeyValues)) next_key = nextKeyValues.FirstOrDefault();

                // 데이터 파싱 (미체결 건수가 1건이라도 있을 때만 실행됨)
                if (root.TryGetProperty("oso", out JsonElement listArray) && listArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement item in listArray.EnumerateArray())
                    {
                        string itemcode = GetSafeString(item, "stk_cd");

                        if (Form1.Market_Item_List.TryGetValue(itemcode, out Market_Item Market))
                        {
                            string 종목명 = GetSafeString(item, "stk_nm").Trim();
                            string 주문번호 = GetSafeString(item, "ord_no").Trim();
                            int 주문수량 = Helper.StringToInt(GetSafeString(item, "ord_qty"));
                            int 주문가격 = Helper.StringToInt(GetSafeString(item, "ord_pric"));
                            int 미체결수량 = Helper.StringToInt(GetSafeString(item, "oso_qty"));
                            int 주문시간 = Helper.StringToInt(GetSafeString(item, "tm"));

                            string 매수매도str = GetSafeString(item, "io_tp_nm").Substring(1);
                            string 시장가 = GetSafeString(item, "trde_tp").Trim();

                            int 매수매도 = (매수매도str.Contains("매수")) ? 1 : 2;
                            int 시장가구분 = (시장가.Equals("시장가")) ? 0 : 1;

                            int 현재가 = Math.Abs(Helper.StringToInt(GetSafeString(item, "cur_prc")));
                            Market.현재가 = 현재가;

                            if (Form1.JumunItem_List.TryGetValue(주문번호, out JumunItem jumun))
                            {
                                jumun.종목명 = 종목명;
                                jumun.주문가격 = 주문가격;
                                jumun.현재가 = 현재가;
                                jumun.주문시간 = 주문시간;
                                jumun.미체결량 = 미체결수량;
                                jumun.주문동기화 = false;
                                jumun.Tik_price = 현재가;
                                jumun.Tik_cap = Method.Find_Tik_Cap(현재가, 주문가격, Market.Market);
                                jumun.매수매도 = 매수매도;
                                jumun.주문취소 = true;
                            }
                            else
                            {
                                int tik_cap = Method.Find_Tik_Cap(현재가, 주문가격, Market.Market);
                                int OrderN = GET.Order번호();

                                jumun = new JumunItem
                                {
                                    Deletetimer = 0,
                                    Screennum = GET.ScreenNum(),
                                    종목코드 = itemcode,
                                    종목명 = 종목명,
                                    주문번호 = 주문번호,
                                    원주문번호 = "---",
                                    검색식 = "리스트'누락'",
                                    주문값 = 0,
                                    시장가구분 = 시장가구분,
                                    취소시간 = 5,
                                    취소N주문 = 0,
                                    반복횟수 = 0,
                                    비고 = "",
                                    Pos = "TR_미체결조회",
                                    주문수량 = 주문수량,
                                    주문가격 = 주문가격,
                                    매수매도 = 매수매도,
                                    비중 = 0,
                                    비중단위 = 0,
                                    취소timer = 5,
                                    현재가 = 현재가,
                                    등락률 = Market.등락율,
                                    주문시간 = 주문시간,
                                    미체결량 = 미체결수량,
                                    주문취소 = true,
                                    가동전 = true,
                                    Tik_cap = tik_cap,
                                    Tik_price = 현재가,
                                    수익률 = 0,
                                    주문동기화 = false,
                                    감시번호 = 0,
                                    Order번호 = OrderN,
                                    수익구분 = 0,
                                    NXT = NXT_server,
                                    주문시간_Ticks = DateTime.Now.Ticks
                                };

                                Jumun.Add누락주문(jumun);
                                Log.동작기록("미체결 확인:: " + 종목명 + " 주문: " + 매수매도str + " 주문수량: " + 주문수량 + " 미체결수량: " + 미체결수량 + " 주문번호: " + 주문번호);
                            }
                            REG.실시간시세등록(itemcode);
                        }
                    }
                }

                // ==========================================================
                // [분기점] 연속 조회 여부에 따른 처리
                // ==========================================================
                if (cont_yn == "Y")
                {
                    Form1.키움_TR유량제한 = true;
                    TR_요청.미체결요청(cont_yn, next_key, true);
                    return; // 다음 페이지가 있으므로 락을 유지한 채로 리턴
                }
            
                // 다음 페이지가 없거나(N) 값이 없으면(단건) 최종 완료 처리
                if (cont_yn == "N" || string.IsNullOrEmpty(cont_yn))
                {
                    Form1.키움_TR유량제한 = false;

                    // ==========================================================
                    // [장부 동기화 상태 확인 로그] 삭제 전 모든 리스트 점검
                    // ==========================================================
                    Console_print("=== [장부 동기화 검증 로그] ===");
                    foreach (var 주문데이터 in Form1.JumunItem_List)
                    {
                        // 주문데이터.Key는 실제 딕셔너리에 저장된 진짜 열쇠입니다.
                        Console_print($"-> 종목: {주문데이터.Value.종목명} | 장부키: {주문데이터.Key} | 주문번호: {주문데이터.Value.주문번호} | 동기화상태(true면 삭제): {주문데이터.Value.주문동기화}");
                    }
                    Console_print("================================");


                    // ==========================================================
                    // [버그 수정 및 최적화] 삭제 안 되던 문제 해결
                    // .Values만 가져오지 않고, KeyValuePair 전체를 가져와서 정확한 'Key' 값으로 지웁니다.
                    // ==========================================================
                    var 삭제할_주문리스트 = Form1.JumunItem_List.Where(kvp => kvp.Value.주문동기화).ToList();

                    foreach (var 타겟 in 삭제할_주문리스트)
                    {
                        // 타겟주문.주문번호가 아닌, 딕셔너리의 고유 'Key' 값으로 삭제 요청
                        bool 삭제성공 = Form1.JumunItem_List.TryRemove(타겟.Key, out _);

                        if (삭제성공)
                        {
                            Console_print($"[+] 동기화 삭제 완료 >> 종목명: {타겟.Value.종목명} (장부키: {타겟.Key})");
                        }
                        else
                        {
                            Console_print($"[-] 삭제 실패(키 찾을수없음) >> 종목명: {타겟.Value.종목명} (장부키: {타겟.Key})");
                        }
                    }

                    // [지니 최적화 핵심] 실제 모든 파싱과 삭제가 끝났습니다! 여기서 락을 풀어줍니다.
                    Interlocked.Exchange(ref Helper._미체결동기화_진행중, 0);

                    if (!Form1.로딩완료)
                    {
                        foreach (var 개별주문 in Form1.JumunItem_List.Values)
                        {
                            if (GridView_Print.Find_OutstandingStock(개별주문))
                            {
                                GridView_Print.Outstanding_insert(개별주문, 0);
                            }
                        }

                        Console_print(GenieConfig.textBox_계좌번호 + " 주문접수내역 조회완료");
                        Log.동작기록(GenieConfig.textBox_계좌번호 + " 주문접수내역 조회완료");
                        Form1.매매시작 = "Loding_12_계좌평가현황요청";
                    }
                }
            }
            else
            {
                Form1.키움_TR유량제한 = true;
                TR_요청.미체결요청(cont_yn, next_key, true);
            }
        }

        public static void Parse_kt00004_계좌평가현황요청(JsonElement root, ref string cont_yn, ref string next_key)
        {
            string returnCode = GetSafeString(root, "return_code").Trim();
            if (returnCode == "0")
            {
                // =====================================================================
                // [서버 원본 데이터 덤프 로그] - 불필요한 0(제로패딩) 제거하여 깔끔하게 출력
                // =====================================================================
                Form1.Console_print("\n=========== [kt00004 계좌평가현황 원본 수신 데이터] ===========");
                //Form1.Console_print($"계좌명(acnt_nm): {GetSafeString(root, "acnt_nm").Trim()}");
                //Form1.Console_print($"지점명(brch_nm): {GetSafeString(root, "brch_nm").Trim()}");
                //Form1.Console_print($"예수금(entr): {Helper.StringToLong(GetSafeString(root, "entr")):N0}");
                //Form1.Console_print($"D+2추정예수금(d2_entra): {Helper.StringToLong(GetSafeString(root, "d2_entra")):N0}");
                //Form1.Console_print($"유가잔고평가액(tot_est_amt): {Helper.StringToLong(GetSafeString(root, "tot_est_amt")):N0}");
                //Form1.Console_print($"예탁자산평가액(aset_evlt_amt): {Helper.StringToLong(GetSafeString(root, "aset_evlt_amt")):N0}");
                //Form1.Console_print($"총매입금액(tot_pur_amt): {Helper.StringToLong(GetSafeString(root, "tot_pur_amt")):N0}");
                //Form1.Console_print($"Parse_kt00004_계좌평가현황요청 (prsm_dpst_aset_amt): {Helper.StringToLong(GetSafeString(root, "prsm_dpst_aset_amt")):N0}");
                //Form1.Console_print($"매도담보대출금(tot_grnt_sella): {Helper.StringToLong(GetSafeString(root, "tot_grnt_sella")):N0}");
                //Form1.Console_print($"당일투자원금(tdy_lspft_amt): {Helper.StringToLong(GetSafeString(root, "tdy_lspft_amt")):N0}");
                //Form1.Console_print($"당월투자원금(invt_bsamt): {Helper.StringToLong(GetSafeString(root, "invt_bsamt")):N0}");
                //Form1.Console_print($"누적투자원금(lspft_amt): {Helper.StringToLong(GetSafeString(root, "lspft_amt")):N0}");
                //Form1.Console_print($"당일투자손익(tdy_lspft): {Helper.StringToLong(GetSafeString(root, "tdy_lspft")):N0}");
                //Form1.Console_print($"당월투자손익(lspft2): {Helper.StringToLong(GetSafeString(root, "lspft2")):N0}");
                //Form1.Console_print($"누적투자손익(lspft): {Helper.StringToLong(GetSafeString(root, "lspft")):N0}");

                //// 수익률은 소수점이 있으므로 double로 파싱하여 출력
                //double.TryParse(GetSafeString(root, "tdy_lspft_rt").Trim(), out double tdy_rt);
                //double.TryParse(GetSafeString(root, "lspft_ratio").Trim(), out double lspft_rt2);
                //double.TryParse(GetSafeString(root, "lspft_rt").Trim(), out double lspft_rt);
                //Form1.Console_print($"당일손익율(tdy_lspft_rt): {tdy_rt}%");
                //Form1.Console_print($"당월손익율(lspft_ratio): {lspft_rt2}%");
                //Form1.Console_print($"누적손익율(lspft_rt): {lspft_rt}%");

                // 1. [데이터 추출]
                long 당일예수금 = Helper.StringToLong(GetSafeString(root, "entr").Trim());
                long D2예수금 = Helper.StringToLong(GetSafeString(root, "d2_entra").Trim());
                long 유가잔고평가액 = Helper.StringToLong(GetSafeString(root, "tot_est_amt").Trim());
                long 총매입금액 = Helper.StringToLong(GetSafeString(root, "tot_pur_amt").Trim());

                // [+] 서버에서 내려준 추정예탁자산을 그대로 가져옵니다.
                long 서버제공_추정자산 = Helper.StringToLong(GetSafeString(root, "prsm_dpst_aset_amt").Trim());

                // ---------------------------------------------------------
                // [+] [수동 계산 로직] 신용상세리스트 이자 합산 및 매도제비용 동시 산출
                // ---------------------------------------------------------
                long 총_신용이자 = 0;
                long 총_예상매도제비용 = 0;

                double 주식_제비용율 = Form1.TAX + Form1.수수료;
                double ETF_제비용율 = Form1.수수료;

                //Form1.Console_print($"[kt00004] 당일예수금: {당일예수금:N0}");
                //Form1.Console_print($"[kt00004] D2예수금: {D2예수금:N0}");
                //Form1.Console_print($"[kt00004] 유가잔고평가액: {유가잔고평가액:N0}");
                //Form1.Console_print($"[kt00004] 총매입금액: {총매입금액:N0}");
                //Form1.Console_print($"[kt00004] 서버제공_추정자산: {서버제공_추정자산:N0}");

                lock (Form1.stockBalanceList)
                {
                    foreach (var item in Form1.stockBalanceList.Values)
                    {
                        // 1. 신용이자 합산
                        long 종목별_이자합계 = 0;
                        if (item.신용상세리스트 != null && item.신용상세리스트.Count > 0)
                        {
                            foreach (var 상세 in item.신용상세리스트)
                            {
                                종목별_이자합계 += 상세.신용이자;
                            }
                        }
                        총_신용이자 += 종목별_이자합계;

                        // 2. 매도제비용(세금+수수료) 합산
                        long 종목평가금 = item.평가금액;
                        long 개별_제비용 = 0;

                        if (!string.IsNullOrEmpty(item.시장) && item.시장 == "E")
                        {
                            개별_제비용 = (long)(종목평가금 * ETF_제비용율);
                        }
                        else
                        {
                            개별_제비용 = (long)(종목평가금 * 주식_제비용율);
                        }
                        총_예상매도제비용 += 개별_제비용;
                    }
                }

                // 최종 순자산: 서버 값 - 총 신용이자 - 총 예상매도제비용
                long 추정예탁자산 = 서버제공_추정자산 - 총_신용이자 - 총_예상매도제비용;
                Form1.Console_print($"[순자산 계산] 완료 - 총 신용이자 차감: {총_신용이자:N0} | 예상매도제비용 차감: {총_예상매도제비용:N0}");
                Form1.Console_print($"Parse_kt00004_계좌평가현황요청 (최종 보수적 순자산): {추정예탁자산:N0}");
                Form1.Console_print("==================================");

                // 2. [Account 클래스 업데이트]
                long 투자원금 = GenieConfig.MT_principal;
                Form1.Acc.투자원금 = 투자원금;
                Form1.Acc.당일예수금 = 당일예수금;

                // ==========================================================
                // [새로운 정밀 동기화 로직] - 진단 및 차감액 계산
                // ==========================================================
                long 기존_D2 = Form1.Acc.D2;
                long 기존_추정D2 = Form1.Acc.추정D2;
                long 매수대기_묶인돈 = 0;

                lock (Form1.JumunItem_List)
                {
                    foreach (var jumun in Form1.JumunItem_List.Values)
                    {
                        if (jumun.매수매도 == 1) // 1 = 매수 주문
                        {
                            long 주문총액 = (long)jumun.주문가격 * jumun.주문수량;
                            double 적용증거금률 = 1.0;

                            if (Form1.Market_Item_List.TryGetValue(jumun.종목코드, out var mItem))
                            {
                                if (jumun.신용주문)
                                {
                                    적용증거금률 = Math.Max(0.45, mItem.증거금률);
                                }
                            }
                            매수대기_묶인돈 += (long)(주문총액 * 적용증거금률);
                        }
                    }
                }

                // 실제 장부 업데이트
                Form1.Acc.D2 = D2예수금;
                Form1.Acc.추정D2 = D2예수금 - 매수대기_묶인돈;

                // 기타 자산 정보 갱신
                Form1.Acc.추정자산 = 추정예탁자산;
                Form1.Acc.매입금 = 총매입금액;
                Form1.Acc.평가금 = 유가잔고평가액;

                Form1.Acc.증가자산 = 추정예탁자산 - 투자원금;
                Form1.Acc.평가손익 = 유가잔고평가액 - 총매입금액;

                if (총매입금액 > 0)
                {
                    Form1.Acc.평가손익률 = Math.Round(((double)Form1.Acc.평가손익 / 총매입금액) * 100, 2);
                }

                // [기존 UI 업데이트 로직]
                if (Form1.로딩완료)
                {
                    if (!Form1.매수증거금)
                    {
                        Form1.AutoClosingAlram("매수 가능합니다. 예수금 확인후 주문하세요.", "매수가능 알림", 5, "동작");
                        Form1.매수증거금 = true;
                        foreach (var Market in Form1.Market_Item_List.Values) Market.매수증거금알림 = true;
                    }

                    if (Form1.form1.JanGo_dataGridView != null && !Form1.form1.JanGo_dataGridView.IsDisposed)
                    {
                        if (Get.TimeNow < GenieConfig.MT_misu_time && GenieConfig.CB_misu || !GenieConfig.CB_misu)
                        {
                            Helper.안전한_UI_업데이트(Form1.form1, () =>
                            {
                                Form1.form1.JanGo_dataGridView.SuspendLayout();
                                Form1.form1.JanGo_dataGridView.Rows.Clear();
                                Method.SortClear(Form1.form1.JanGo_dataGridView);

                                List<Stockbalance> 정렬된잔고리스트 = new List<Stockbalance>(Form1.stockBalanceList.Values);
                                정렬된잔고리스트.Sort((a, b) => b.초기매수일.CompareTo(a.초기매수일));

                                foreach (Stockbalance 잔고 in 정렬된잔고리스트)
                                {
                                    int rowIndex = Form1.form1.JanGo_dataGridView.Rows.Add();
                                    Form1.form1.JanGo_dataGridView.Rows[rowIndex].Cells["코드_잔고"].Value = 잔고.종목코드;
                                    홀딩잔고.JangoRow_print(rowIndex, 잔고);
                                }
                                Form1.form1.JanGo_dataGridView.ClearSelection();
                                Form1.form1.JanGo_dataGridView.ResumeLayout();
                            });
                        }
                    }
                }
                else
                {
                    Helper.안전한_UI_업데이트(form1, () =>
                    {
                        FormPrint.acc_print();
                    });
                }

                if (Form1.매매시작 == "계좌평가현황요청")
                {
                    Log.동작기록(GenieConfig.textBox_계좌번호 + " 예수금 조회 완료");
                    Form1.Console_print(GenieConfig.textBox_계좌번호 + " 예수금 조회 완료");
                    Form1.매매시작 = "Loding_13_일자별실현손익요청";
                }
                Form1.키움_TR유량제한 = false;
            }
            else
            {
                Form1.키움_TR유량제한 = true;
                TR_요청.계좌평가현황요청("Y", "", true);
            }
        }
        // =================================================================================
        // 5. kt00018 (계좌평가잔고내역요청)
        // =================================================================================
        public static void Parse_kt00018_계좌평가잔고내역(JsonElement root, HttpResponseMessage response, ref string cont_yn, ref string next_key)
        {
            string returnCode = GetSafeString(root, "return_code");
            if (returnCode == "0")
            {
                if (root.TryGetProperty("acnt_evlt_remn_indv_tot", out JsonElement listArray) && listArray.ValueKind == JsonValueKind.Array)
                {
                    // ==========================================================
                    // 💡 [추가] 계좌 전체 총합 데이터 (루프 밖 root에서 파싱 권장)
                    // ==========================================================
                    // (이 부분은 foreach 루프 바깥 상단에 위치하는 것이 좋습니다)
                    long 총매입금액 = Helper.StringToLong(GetSafeString(root, "tot_pur_amt"));
                    long 총평가금액 = Helper.StringToLong(GetSafeString(root, "tot_evlt_amt"));
                    long 총평가손익 = Helper.StringToLong(GetSafeString(root, "tot_evlt_pl"));
                    string 총수익률 = GetSafeString(root, "tot_prft_rt");
                    long 추정예탁자산 = Helper.StringToLong(GetSafeString(root, "prsm_dpst_aset_amt"));
                    long 총대출금 = Helper.StringToLong(GetSafeString(root, "tot_loan_amt"));
                    long 총융자금액 = Helper.StringToLong(GetSafeString(root, "tot_crd_loan_amt"));
                    long 총대주금액 = Helper.StringToLong(GetSafeString(root, "tot_crd_ls_amt"));

                    // ★ [신규 계산 로직] 내 현금 흐름 파악하기
                    long 현금매수금 = 총매입금액 - 총융자금액;
                    long 남은현금 = 추정예탁자산 - 총평가금액 + 총융자금액;

                    // ==========================================================
                    // 🚀 [Acc 연동] Form1.Acc 전역 변수에 데이터 매핑 및 계산
                    // ==========================================================
                    Form1.Acc.투자원금 = GenieConfig.MT_principal; // 설정된 초기 투자원금 가져오기
                    Form1.Acc.매입금 = 총매입금액;
                    Form1.Acc.평가금 = 총평가금액;
                    Form1.Acc.평가손익 = 총평가손익;
                  //  Form1.Acc.추정자산 = 추정예탁자산;
                    double.TryParse(총수익률, out Form1.Acc.평가손익률);

                    // 증가자산 및 증가율 자동 계산
                  //  Form1.Acc.증가자산 = Form1.Acc.추정자산 - Form1.Acc.투자원금;
                    // ==========================================================

                    Console_print($"\n[Parse_kt00018_계좌평가잔고내역 요약] 총매입:{총매입금액:N0} | 총평가:{총평가금액:N0} | 총손익:{총평가손익:N0} |\n" +
                                  $" 총수익률:{총수익률}% | 추정예탁:{추정예탁자산:N0} | 총대출금:{총대출금:N0} |\n" +
                                  $" 총융자:{총융자금액:N0} | 총대주금액:{총대주금액:N0}");

                    // 콘솔에 눈에 확 띄게 출력!
                    Console_print($"[자금 분석] 내 현금매수금: {현금매수금:N0}원 | 계좌 남은 현금: {남은현금:N0}원");
                    Console_print($"[자산 증감] 투자원금: {Form1.Acc.투자원금:N0}원 | 증가자산: {Form1.Acc.증가자산:N0}원 ");
                    // ==========================================================

                    // ==========================================================
                    // 💡 [분리 바구니 생성]
                    // ==========================================================
                    List<string> 기존잔고_추적 = new List<string>();
                    List<string> 신규잔고_추적 = new List<string>();

                    // 🔄 엄청나게 짧아진 핵심 루프!
                    foreach (JsonElement item in listArray.EnumerateArray())
                    {
                        string 종목코드 = GetSafeString(item, "stk_cd")[1..].ToString();

                        if (Form1.Market_Item_List.TryGetValue(종목코드, out Market_Item Market))
                        {
                            로딩잔고.업데이트_및_신규추가(item, 종목코드, Market, 기존잔고_추적, 신규잔고_추적);
                        }
                    }

                    // ==========================================================
                    // 📊 [분리 결과 리포트]
                    // ==========================================================
                    string 기존목록 = 기존잔고_추적.Count > 0 ? string.Join(", ", 기존잔고_추적) : "없음";
                    string 신규목록 = 신규잔고_추적.Count > 0 ? string.Join(", ", 신규잔고_추적) : "없음";
                
                    Console_print($"\n[잔고 로딩 완료]");
                    Console_print($"  기존 유지 ({기존잔고_추적.Count}건): {기존목록}");
                    Console_print($"  신규 추가 ({신규잔고_추적.Count}건): {신규목록}\n");

                    if (response.Headers.TryGetValues("cont-yn", out var contYnValues)) cont_yn = contYnValues.FirstOrDefault();
                    if (response.Headers.TryGetValues("next-key", out var nextKeyValues)) next_key = nextKeyValues.FirstOrDefault();

                    if (cont_yn == "Y")
                    {
                        Form1.키움_TR유량제한 = true;
                        TR_요청.계좌평가잔고내역요청(cont_yn, next_key, true);
                        return;
                    }

                    if (cont_yn == "N")
                    {
                        Form1.키움_TR유량제한 = false;

                        if (!Form1.로딩완료)
                        {
                            Form1.키움_TR유량제한 = false;

                            Log.동작기록(GenieConfig.textBox_계좌번호 + " 계좌평가잔고내역 조회 완료");
                            Console_print(GenieConfig.textBox_계좌번호 + " 계좌평가잔고내역 조회 완료");
                            Form1.매매시작 = "Loding_15_계좌수익률요청";
                        }
                        else
                        {
                            TR_요청.계좌수익률요청("Y", "", false);
                        }
                    }
                }
            }
            else
            {
                Form1.키움_TR유량제한 = true;
                TR_요청.계좌평가잔고내역요청(cont_yn, next_key, true);
            }
        }


        public static void Parse_ka10085_계좌수익률요청(JsonElement root, HttpResponseMessage response, ref string cont_yn, ref string next_key)
        {

            Console_print($"\n============================================================\n");
            Console_print($"Parse_ka10085_계좌수익률요청");

            if (response.Headers.TryGetValues("cont-yn", out var contYnValues)) cont_yn = contYnValues.FirstOrDefault();
            if (response.Headers.TryGetValues("next-key", out var nextKeyValues)) next_key = nextKeyValues.FirstOrDefault();

            string returnCode = GetSafeString(root, "return_code");
            string returnMsg = GetSafeString(root, "return_msg");
            if (returnCode == "0")
            {
                //   Console_print($"\n=========== [ka10085 계좌수익률요청 수신 성공] ===========");
                HashSet<string> 처리된종목코드 = new HashSet<string>();

                if (root.TryGetProperty("acnt_prft_rt", out JsonElement listArray) && listArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement item in listArray.EnumerateArray())
                    {
                        // 1. [파싱] 데이터 추출
                        string 종목코드 = GetSafeString(item, "stk_cd");
                        string keyJongmok = 종목코드.Length > 0 && char.IsLetter(종목코드[0]) ? 종목코드.Substring(1) : 종목코드;

                        string 신용구분 = GetSafeString(item, "crd_tp");
                        string 대출일 = GetSafeString(item, "loan_dt");
                        string 만기일 = GetSafeString(item, "expr_dt");

                        // 🛑 [중요] 합계(Total) 데이터 필터링
                        if (대출일 == "99991231" || 신용구분 == "99")
                        {
                            continue;
                        }

                        // 2. 메인 잔고 찾기
                        if (Form1.stockBalanceList.TryGetValue(keyJongmok, out Stockbalance 잔고))
                        {
                            // 💡 [초기화] 종목별 첫 데이터일 때 누적 변수 초기화
                            if (!처리된종목코드.Contains(keyJongmok))
                            {
                                잔고.신용상세리스트.Clear();
                                잔고.신용_보유수량 = 0;
                                잔고.신용_주문가능수량 = 0;
                                잔고.신용_매입금액 = 0;
                                잔고.신용_평가금액 = 0;
                                잔고.신용_이자합계 = 0;
                                잔고.신용_평가손익 = 0;
                                처리된종목코드.Add(keyJongmok);
                            }

                            // ==========================================================
                            // 🔍 [RAW 데이터 추출 및 출력] 서버 원본 데이터 확인용
                            // ==========================================================
                            string raw_일자 = GetSafeString(item, "dt");
                            string raw_현재가 = GetSafeString(item, "cur_prc");
                            string raw_매입가 = GetSafeString(item, "pur_pric");
                            string raw_매입금액 = GetSafeString(item, "pur_amt");
                            string raw_보유수량 = GetSafeString(item, "rmnd_qty");
                            string raw_당일매도손익 = GetSafeString(item, "tdy_sel_pl");
                            string raw_당일수수료 = GetSafeString(item, "tdy_trde_cmsn");
                            string raw_당일세금 = GetSafeString(item, "tdy_trde_tax");
                            string raw_결제잔고 = GetSafeString(item, "setl_remn");
                            string raw_청산가능 = GetSafeString(item, "clrn_alow_qty");
                            string raw_신용금액 = GetSafeString(item, "crd_amt");
                            string raw_신용이자 = GetSafeString(item, "crd_int");

                            //string rawLog = $" [RAW] {잔고.종목명}({종목코드}) | 일자:[{raw_일자}] | 현재가:[{raw_현재가}] | 매입가:[{raw_매입가}] | " +
                            //                $"매입금:[{raw_매입금액}] | 수량:[{raw_보유수량}] | 매도손익:[{raw_당일매도손익}] | " +
                            //                $"수수료:[{raw_당일수수료}] | 세금:[{raw_당일세금}] | 신용구분:[{신용구분}] | " +
                            //                $"대출일:[{대출일}] | 결제잔고:[{raw_결제잔고}] | 청산가능:[{raw_청산가능}] | " +
                            //                $"신용금액:[{raw_신용금액}] | 신용이자:[{raw_신용이자}] | 만기일:[{만기일}]";
                            //Console_print(rawLog);

                            // ==========================================================
                            // 🧮 [공통 비용 및 HTS 정밀 계산]
                            // ==========================================================
                            // 현재가 앞의 '-' 기호를 제거하기 위해 Math.Abs 적용
                            int 현재가 = Math.Abs(Helper.StringToInt(raw_현재가));
                            int 수량 = Helper.StringToInt(raw_보유수량);
                            long 매입금액 = Helper.StringToLong(raw_매입금액);
                            int 평균단가 = Helper.StringToInt(raw_매입가);
                            int 청산가능 = Helper.StringToInt(raw_청산가능);
                            long 신용이자 = Helper.StringToLong(raw_신용이자);

                            long 현재_평가금액 = (long)현재가 * 수량;
                            double 현재세금율 = (잔고.시장 == "E") ? 0 : Form1.TAX; // ETF는 세금 0

                            // 매수/매도 수수료 10원 미만 절사 (HTS 방식 동일 적용)
                            long 항목_매수수수료 = (long)((매입금액 * Form1.수수료) / 10) * 10;
                            long 항목_매도수수료 = (long)((현재_평가금액 * Form1.수수료) / 10) * 10;
                            long 항목_제세금 = (long)(현재_평가금액 * 현재세금율);

                            // 해당 줄(Row)의 최종 순손익 계산
                            long 항목_평가손익 = (현재_평가금액 - 매입금액) - 항목_매수수수료 - 항목_매도수수료 - 항목_제세금 - 신용이자;


                            // ==========================================================
                            // 🔹 Case A: 현금 잔고 처리 ("00")
                            // ==========================================================
                            //잔고.보유수량 = 0;
                            //잔고.주문가능수량 = 0;

                            if (신용구분 == "00")
                            {
                                //잔고.보유수량 = 수량;
                                //잔고.주문가능수량 = 청산가능;
                                //잔고.매입금액 = 매입금액;
                                //잔고.평가금액 = 현재_평가금액;
                                //잔고.평균단가 = 평균단가;
                                
                             //   Console_print($"로딩완료 => {Form1.로딩완료} [현금잔고 갱신완료] {잔고.종목명} |  수량:{수량} 보유수량:{잔고.보유수량} | 청산가능:{청산가능} 주문가능수량:{잔고.주문가능수량}");
                            }
                            // ==========================================================
                            // 🔹 Case B: 신용 잔고 처리 ("03" 등)
                            // ==========================================================
                            else
                            {
                                // 1. 상세 영수증 객체 생성 (평가손익, 수익률 추가됨)
                                신용상세 상세영수증 = new 신용상세
                                {
                                    일자 = raw_일자,
                                    종목코드 = 종목코드,
                                    종목명 = 잔고.종목명,
                                    신용구분 = 신용구분,
                                    대출일 = 대출일,
                                    만기일 = 만기일,
                                    현재가 = 현재가,
                                    매입가 = 평균단가,
                                    보유수량 = 수량,
                                    청산가능수량 = 청산가능,
                                    결제잔고 = Helper.StringToInt(raw_결제잔고),
                                    매입금액 = 매입금액,
                                    신용금액 = Helper.StringToLong(raw_신용금액),
                                    신용이자 = 신용이자,
                                    당일매도손익 = Helper.StringToLong(raw_당일매도손익),
                                    당일매매수수료 = Helper.StringToLong(raw_당일수수료),
                                    당일매매세금 = Helper.StringToLong(raw_당일세금),
                                    평가손익 = 항목_평가손익,
                                };

                                잔고.신용상세리스트.Add(상세영수증);

                                // 2. 전체 신용 변수에 누적 합산 (HTS 방식)
                                잔고.신용_보유수량 += 수량;
                                잔고.신용_주문가능수량 += 청산가능;
                                잔고.신용_매입금액 += 매입금액;
                                잔고.신용_이자합계 += 신용이자;
                                잔고.신용_평가금액 += 현재_평가금액;
                                잔고.신용_평가손익 += 항목_평가손익;

                                // 전체 수익률 갱신
                                if (잔고.신용_매입금액 > 0)
                                {
                                    잔고.신용_평균단가 = (int)(잔고.신용_매입금액 / 잔고.신용_보유수량);
                                    잔고.신용_수익률 = Math.Round(((double)잔고.신용_평가손익 / 잔고.신용_매입금액) * 100, 2);
                                }
                            }

                            if (Form1.로딩완료)
                            {
                                if (Form1.잔고보정Dict.ContainsKey(종목코드))
                                {
                                    string 알림메시지 = $"{잔고.종목명} 보유수량:{잔고.보유수량} 주문가능수량:{잔고.주문가능수량}\n신용_보유수량:{잔고.신용_보유수량} 신용_주문가능수량:{잔고.신용_주문가능수량}";
                                    string 로그메시지1 = $"[잔고보정완료] 종목:{잔고.종목명} 보유수량:{잔고.보유수량} 주문가능수량:{잔고.주문가능수량} 으로 보정되었습니다.";
                                    string 로그메시지2 = $"[신용_잔고보정완료] 종목:{잔고.종목명} 신용_보유수량:{잔고.신용_보유수량} 신용_주문가능수량:{잔고.신용_주문가능수량} 으로 보정되었습니다.";

                                    Helper.알림창_멀티("잔고보정", 알림메시지, 5, false);
                                    Log.동작기록(로그메시지1);
                                    Log.동작기록(로그메시지2);

                                    Form1.잔고보정Dict.TryRemove(종목코드, out _);
                                }
                            }
                        }
                    }
                }

                foreach (var 잔고 in stockBalanceList.Values)
                {
                    잔고.수익률 = 신용계산.Get_Real_Profit_Rate(잔고, out long 실질평가손익);
                    잔고.평가손익 = 실질평가손익;
                    잔고.예상손익 = 잔고.평가손익 + 잔고.누적손익;

                    // 최고 수익률 기록
                    if (double.IsInfinity(잔고.수익률) || 잔고.최고수익률 < 잔고.수익률)
                    {
                        잔고.최고수익률 = 잔고.수익률;
                    }
                }

                if (cont_yn == "Y")
                {
                    Form1.키움_TR유량제한 = true;
                    TR_요청.계좌수익률요청(cont_yn, next_key, true);

                    Console_print($"[잔고갱신 추가요청] cont_yn: {cont_yn} next_key: {next_key}");
                    return;
                }

                if (cont_yn == "N")
                {
                    Form1.키움_TR유량제한 = false;

                    //foreach (var 잔고 in Form1.stockBalanceList.Values)
                    //{
                    //    Console_print($" [신용잔고 갱신완료] {잔고.종목명} | 신용 보유수량:{잔고.신용_보유수량} | 신용 주문가능수량:{잔고.신용_주문가능수량}");
                    //}

                    if (!Form1.로딩완료)
                    {
                        Helper.안전한_UI_업데이트(Form1.form1, () =>
                        {
                            Form1.form1.JanGo_dataGridView.Rows.Clear();
                            Method.SortClear(Form1.form1.JanGo_dataGridView);
                            // 정렬된 잔고 리스트 순회
                            var sortedStockBalances = from pair in Form1.stockBalanceList orderby pair.Value.초기매수일 descending select pair;
                            foreach (var code in sortedStockBalances)
                            {
                                Stockbalance balance = code.Value;
                                Method.매입금매매제한(balance);

                                if ((balance.보유수량 + balance.신용_보유수량) == 0)
                                {
                                    Form1.stockBalanceList.TryRemove(balance.종목코드, out _);
                                }
                                else
                                {
                                    if (balance.보유수량 + balance.신용_보유수량 > 0)
                                    {
                                        if (!Form1.Rebuystock_List.Exists(o => o.Itemcode.Equals(balance.종목코드)))
                                        {
                                            Form1.Rebuystock_List.Add(new 재매수(balance.종목코드, balance.종목명, ""));
                                        }
                                    }

                                    // 그리드뷰 행 추가
                                    Form1.form1.JanGo_dataGridView.Rows.Add();
                                    int rowIndex = Form1.form1.JanGo_dataGridView.Rows.Count - 1;
                                    Form1.form1.JanGo_dataGridView["코드_잔고", rowIndex].Value = balance.종목코드;

                                    // 실제 UI 업데이트 함수 호출
                                    홀딩잔고.JangoRow_print(rowIndex, balance);
                                }
                            }

                            Form1.form1.JanGo_dataGridView.ClearSelection();
                            var sortedList = Form1.stockBalanceList.Where(pair => pair.Value.매매그룹 != 13).ToList();
                            Form1.form1.TB_jango_count.Text = sortedList.Count.ToString();
                        });

                        Log.동작기록(GenieConfig.textBox_계좌번호 + " 계좌수익률요청 조회 완료");
                        Console_print(GenieConfig.textBox_계좌번호 + " 계좌수익률요청 조회 완료");
                        
                        if(매매시작 == "계좌수익률요청")
                        {
                            TelegramMessenger.Telegram_alram("logIn_user");
                            Trade_tasks.잔고_매매();
                            Form1.매매시작 = "Load_completion";

                            TR_요청.신용융자가능종목요청("N", "", false);
                        }

                        //    Form1.매매시작 = "Loding_16_신용융자가능종목요청";
                    }
                    else
                    {
                        Form1.잔고보정요청 = true;
                        _ = Helper.미체결내역동기화(false);
                    }
                }
            }
            else
            {
                Form1.키움_TR유량제한 = true;
                TR_요청.계좌수익률요청(cont_yn, next_key, true);
                Console_print($"[계좌수익률요청 실패] 코드: {returnCode} | 메시지: {returnMsg}");
            }
        }



  
    }
}

