using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 지니64.box;

namespace 지니64
{
    internal class TR_LS증권_계좌 : Form1
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
            return "0";
        }

        // [수정 완료] 파라미터는 원본 그대로 유지합니다.
        public static async Task LS_계좌(string token, object data, string tr_, string cont_yn, string next_key)
        {
            string host = "https://openapi.ls-sec.co.kr:8080";
            string endpoint = "/stock/accno";
            string url = host + endpoint;

            string tr_id = tr_.Split('_')[0];
            string where = tr_.Split('_')[1];

            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // LS증권 전용 헤더 세팅 (LS는 보통 appkey를 헤더에 요구하지 않음, 토큰으로 인증)
                    request.Headers.Add("tr_cd", tr_id);
                    request.Headers.Add("tr_cont", cont_yn);

                    if (!string.IsNullOrEmpty(next_key))
                        request.Headers.Add("tr_cont_key", next_key);

                    request.Content = content;

                    using (var response = await client.SendAsync(request))
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

                        // 1. [긴급 검사] 토큰 만료 요격
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                            (responseBody.Contains("토큰") && responseBody.Contains("유효하지")))
                        {
                            Form1.Console_print(">> [LS증권 REST API] 접근 토큰이 만료되었습니다!");
                            Log.에러기록("[LS 토큰 만료] 서버로부터 401 에러 수신. 안전 종료를 시도합니다.");

                            Form1.중복접속 = false;
                            Helper.안전종료하기();
                            return;
                        }

                        using (JsonDocument doc = JsonDocument.Parse(responseBody))
                        {
                            JsonElement root = doc.RootElement;

                            if (tr_id.Equals("CSPAQ12200") || tr_id.Equals("t0424"))
                            {
                                Parse_LS_계좌평가현황요청(root, ref cont_yn, ref next_key);
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Form1.Console_print("[LS증권] 요청 실패: " + error.Message);
            }
        }

        public static void Parse_LS_계좌평가현황요청(JsonElement root, ref string cont_yn, ref string next_key)
        {
            string 응답코드 = GetSafeString(root, "rsp_cd").Trim();

            if (응답코드 == "00000" || 응답코드 == "0" || string.IsNullOrEmpty(응답코드))
            {
                Form1.Console_print("\n=========== [LS증권 계좌평가현황 원본 수신 데이터] ===========");

                long 당일예수금 = Helper.StringToLong(GetSafeString(root, "Dpstamt").Trim());
                long D2예수금 = Helper.StringToLong(GetSafeString(root, "D2AsetWe").Trim());
                long 유가잔고평가액 = Helper.StringToLong(GetSafeString(root, "TotEvluAmt").Trim());
                long 총매입금액 = Helper.StringToLong(GetSafeString(root, "TotPchsAmt").Trim());
                long 서버제공_추정자산 = Helper.StringToLong(GetSafeString(root, "BalEvluAmt").Trim());

                long 총_신용이자 = 0;
                long 총_예상매도제비용 = 0;

                double 주식_제비용율 = Form1.TAX + Form1.수수료;
                double ETF_제비용율 = Form1.수수료;

                lock (Form1.stockBalanceList)
                {
                    foreach (var 보유항목 in Form1.stockBalanceList.Values)
                    {
                        long 종목별_이자합계 = 0;
                        if (보유항목.신용상세리스트 != null && 보유항목.신용상세리스트.Count > 0)
                        {
                            foreach (var 상세내역 in 보유항목.신용상세리스트)
                            {
                                종목별_이자합계 += 상세내역.신용이자;
                            }
                        }
                        총_신용이자 += 종목별_이자합계;

                        long 종목평가금 = 보유항목.평가금액;
                        long 개별_제비용 = 0;

                        if (!string.IsNullOrEmpty(보유항목.시장) && 보유항목.시장 == "E")
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

                long 추정예탁자산 = 서버제공_추정자산 - 총_신용이자 - 총_예상매도제비용;
                Form1.Console_print($"[LS증권 순자산 계산] 완료 - 신용이자 차감: {총_신용이자:N0} | 예상제비용 차감: {총_예상매도제비용:N0}");
                Form1.Console_print($"Parse_LS_계좌평가현황요청 (최종 보수적 순자산): {추정예탁자산:N0}");
                Form1.Console_print("==================================");

                //long 투자원금 = GenieConfig.MT_principal;
                ////Form1.Acc.투자원금 = 투자원금;
                ////Form1.Acc.당일예수금 = 당일예수금;
                //Form1.Console_print($"[LS증권 D2] {투자원금:N0}");
                //Form1.Console_print($"[LS증권 D2] {당일예수금:N0}");

                long 매수대기_묶인돈 = 0;

                lock (Form1.JumunItem_List)
                {
                    foreach (var 개별주문 in Form1.JumunItem_List.Values)
                    {
                        if (개별주문.매수매도 == 1)
                        {
                            long 주문총액 = (long)개별주문.주문가격 * 개별주문.주문수량;
                            double 적용증거금률 = 1.0;

                            if (Form1.Market_Item_List.TryGetValue(개별주문.종목코드, out var 시장데이터))
                            {
                                if (개별주문.신용주문)
                                {
                                    적용증거금률 = Math.Max(0.45, 시장데이터.증거금률);
                                }
                            }
                            매수대기_묶인돈 += (long)(주문총액 * 적용증거금률);
                        }
                    }
                }

                Form1.Console_print($"[LS증권 D2] {D2예수금:N0}");
                Form1.Console_print($"[LS증권 추정D2] {(D2예수금 - 매수대기_묶인돈):N0}");
                Form1.Console_print($"[LS증권 추정자산] {추정예탁자산:N0}");
                Form1.Console_print($"[LS증권 매입금] {총매입금액:N0}");
                Form1.Console_print($"[LS증권 평가금] {유가잔고평가액:N0}");
          //      Form1.Console_print($"[LS증권 증가자산] {(추정예탁자산 - 투자원금):N0}");
                Form1.Console_print($"[LS증권 평가손익] {(유가잔고평가액 - 총매입금액):N0}");


                //Form1.Acc.D2 = D2예수금;
                //Form1.Acc.추정D2 = D2예수금 - 매수대기_묶인돈;
                //Form1.Acc.추정자산 = 추정예탁자산;
                //Form1.Acc.매입금 = 총매입금액;
                //Form1.Acc.평가금 = 유가잔고평가액;
                //Form1.Acc.증가자산 = 추정예탁자산 - 투자원금;
                //Form1.Acc.평가손익 = 유가잔고평가액 - 총매입금액;

                //if (총매입금액 > 0)
                //{
                //    Form1.Acc.평가손익률 = Math.Round(((double)Form1.Acc.평가손익 / 총매입금액) * 100, 2);
                //}

                //if (Form1.로딩완료)
                //{
                //    if (!Form1.매수증거금)
                //    {
                //        Form1.AutoClosingAlram("매수 가능합니다. 예수금 확인후 주문하세요.", "매수가능 알림", 5, "동작");
                //        Form1.매수증거금 = true;
                //        foreach (var 시장데이터 in Form1.Market_Item_List.Values) 시장데이터.매수증거금알림 = true;
                //    }

                //    if (Form1.form1.JanGo_dataGridView != null && !Form1.form1.JanGo_dataGridView.IsDisposed)
                //    {
                //        if (Get.TimeNow < GenieConfig.MT_misu_time && GenieConfig.CB_misu || !GenieConfig.CB_misu)
                //        {
                //            Helper.안전한_UI_업데이트(Form1.form1, () =>
                //            {
                //                Form1.form1.JanGo_dataGridView.SuspendLayout();
                //                Form1.form1.JanGo_dataGridView.Rows.Clear();
                //                Method.SortClear(Form1.form1.JanGo_dataGridView);

                //                List<Stockbalance> 정렬된잔고리스트 = new List<Stockbalance>(Form1.stockBalanceList.Values);
                //                정렬된잔고리스트.Sort((a, b) => b.초기매수일.CompareTo(a.초기매수일));

                //                foreach (Stockbalance 잔고데이터 in 정렬된잔고리스트)
                //                {
                //                    int 행인덱스 = Form1.form1.JanGo_dataGridView.Rows.Add();
                //                    Form1.form1.JanGo_dataGridView.Rows[행인덱스].Cells["코드_잔고"].Value = 잔고데이터.종목코드;
                //                    홀딩잔고.JangoRow_print(행인덱스, 잔고데이터);
                //                }
                //                Form1.form1.JanGo_dataGridView.ClearSelection();
                //                Form1.form1.JanGo_dataGridView.ResumeLayout();
                //            });
                //        }
                //    }
                //}
                //else
                //{
                //    Helper.안전한_UI_업데이트(form1, () =>
                //    {
                //        FormPrint.acc_print();
                //    });
                //}

                Form1.LS_TR유량제한 = false;
            }
            else
            {
                Form1.LS_TR유량제한 = true;
                TR_요청.계좌평가현황요청("Y", "", true);
            }
        }
    }
}