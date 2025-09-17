using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 지니_64.box;

namespace 지니_64
{
    class GridView_Print
    {
        public static void JanGo_dataGridView_print()// 잔고그리드뷰 그리기
        {
            try
            {
                if (Form1.form1.tab_잔고.TabPages[0].Contains(Form1.form1.JanGo_dataGridView))//Form1.form1.JanGo_dataGridView.Enabled.Equals(true) && Form1.form1.JanGo_dataGridView.Rows.Count > 0)
                {
                    //for (int a = Form1.stockBalanceList.Count - 1; a >= 0; a--)
                    //{
                    //    var code = Form1.stockBalanceList.ElementAt(a);
                    //    if (Form1.form1.JanGo_dataGridView.Rows.Count < Form1.stockBalanceList.Count) break;

                    //    Stockbalance 잔고 = code.Value;

                    //    for (int i = 0; i < Form1.form1.JanGo_dataGridView.Rows.Count; i++)
                    //    {
                    //        if (Form1.form1.JanGo_dataGridView["코드_잔고A", i].Value.ToString().Equals(잔고.종목코드))
                    //        {
                    //            if (잔고.재매수 > 0)
                    //            {
                    //                JangoRow_print(i, 잔고);
                    //            }
                    //            else
                    //            {
                    //                if (잔고.전량매도)
                    //                {
                    //                    List<JumunItem> Cansel_List = Form1.JumunItem_List.FindAll(o => o.종목코드.Equals(잔고.종목코드));
                    //                    for (int c = 0; c < Cansel_List.Count; c++)
                    //                    {
                    //                        JumunItem item = Cansel_List[c];
                    //                        if (item != null)
                    //                        {
                    //                            item.반복횟수 = 0;
                    //                            item.취소시간 = 0;
                    //                            item.취소timer = 0;
                    //                        }
                    //                    }

                    //                    DataManagement.신규매수개수확인(잔고.초기매수검색식, false); // 전량매도

                    //                    Form1.동작_Log(" ");
                    //                    Form1.동작_Log("[전량매도] " + 잔고.종목명 + " 수익금: " + 잔고.누적손익.ToString("N0") + " 이 전량 매도 되었습니다.");
                    //                    Form1.동작_Log(" ");

                    //                    Form1.Error_Log(" ");
                    //                    Form1.Error_Log(잔고.종목명 + " 이 전량 매도 되었습니다.");
                    //                    Form1.Error_Log(잔고.종목명 + " 금일등락율: " + 잔고.등락율 + " 수익률: " + Math.Round(잔고.수익률, 2) + " 최고수익률: " + Math.Round(잔고.최고수익률, 2) + " 최저수익률: " + Math.Round(잔고.최저수익률, 2));
                    //                    Form1.Error_Log(잔고.종목명 + " 금일수익금: " + Method.단위변환(잔고.금일수익금) +
                    //                                                 " 총수익금: " + Method.단위변환(잔고.누적손익) +
                    //                                                 " 최고예상손익금: " + Method.단위변환(잔고.최고예상손익금) +
                    //                                                 " 최저예상손익금: " + Method.단위변환(잔고.최저예상손익금) +
                    //                                                 " 금일매도금: " + Method.단위변환(잔고.금일매도금));

                    //                    Form1.Error_Log(잔고.종목명 + " 매수검색식: " + 잔고.초기매수검색식 + " 초기매수일: " + 잔고.초기매수일.ToString("yyyy/MM/dd") + " 거래일: " + 잔고.거래일);
                    //                    Form1.Error_Log(" ");

                    //                    Writing_Management.전량매도기록(Form1.today + ";" + 잔고.종목명 + ";" + 잔고.초기매수검색식 + ";" + 잔고.초기매수일.ToString("yyyy/MM/dd") + ";" + 잔고.거래일 + ";" +
                    //                                                   Method.단위변환(잔고.금일수익금) + ";" +
                    //                                                   Method.단위변환(잔고.누적손익) + ";" +
                    //                                                   Method.단위변환(잔고.최고예상손익금) + ";" +
                    //                                                   Method.단위변환(잔고.최저예상손익금) + ";" +
                    //                                                   Method.단위변환(잔고.금일매도금) + ";" + 잔고.등락율 + ";" + Math.Round(잔고.수익률, 2) + ";" + Math.Round(잔고.최고수익률, 2) + ";" + Math.Round(잔고.최저수익률, 2) + ";" +
                    //                                                   Method.단위변환(Form1.Acc[0].실현손익) + ";" +
                    //                                                   Method.단위변환(Form1.Acc[0].증가자산));

                    //                    if (Properties.Settings.Default.CB_텔레그램사용)
                    //                        TelegramMessenger.Telegram_Send("[전량매도] :: 종목: " + 잔고.종목명 + " 매수식: " + 잔고.초기매수검색식 + " 초기매수일: " + 잔고.초기매수일.ToString("yyyy/MM/dd") + " 거래일: " + 잔고.거래일 + " 수익률: " + 잔고.수익률 + " 금일수익금: " + Method.단위변환(잔고.금일수익금) + " 총수익금: " + Method.단위변환(잔고.누적손익) + " 계좌실현손익: " + Method.단위변환(Form1.Acc[0].실현손익) + " 증가자산: " + Method.단위변환(Form1.Acc[0].증가자산) + " D2예수금: " + Method.단위변환(Form1.Acc[0].추정D2));

                    //                    Form1.비프음("실행");

                    //                    if (!Properties.Settings.Default.CB_new_rebuy)
                    //                    {
                    //                        Form1.form1.실시간시세_List.Remove(잔고.종목코드);
                    //                        Form1.form1.axKHOpenAPI1.SetRealRemove("ALL", 잔고.종목코드);  // 모든 화면에서 종목 실시간 해지
                    //                    }

                    //                    재매수 Item = Form1.Rebuystock_List.Find(o => o.ItemCode.Contains(잔고.종목코드));
                    //                    if (Item != null)
                    //                    {
                    //                        string 구분 = "수익";
                    //                        if (잔고.금일수익금 < 0)
                    //                        {
                    //                            구분 = "손실";
                    //                        }

                    //                        Item.결과 = 구분;

                    //                        DataManagement.save_Rebuystock_List();
                    //                    }

                    //                    if (Form1.form1.주문예약_List.Count > 0)
                    //                    {
                    //                        int Num = 0;
                    //                        for (int n = Form1.form1.주문예약_List.Count - 1; n >= 0; n--)
                    //                        {
                    //                            if (Num >= Form1.form1.주문예약_List.Count) break;

                    //                            주문예약 item = Form1.form1.주문예약_List[Num];

                    //                            if (item.전량매도삭제)
                    //                            {
                    //                                if (item.종목코드.Equals(잔고.종목코드))
                    //                                {
                    //                                    Form1.form1.주문예약_List.Remove(item);
                    //                                }
                    //                            }
                    //                            else
                    //                            {
                    //                                Num++;
                    //                            }
                    //                        }
                    //                    }

                    //                    if (Form1.form1.CBB_최종가종목.Items.Contains(잔고.종목명))
                    //                    {
                    //                        Form1.form1.CBB_최종가종목.Items.Remove(잔고.종목명);
                    //                    }

                    //                    Console.WriteLine("############## [" + 잔고.종목명 + "] 잔고 전량 매도 ##############");

                    //                    if (Form1.form1.신규거부_List.Contains(잔고.종목명)) Form1.form1.신규거부_List.Remove(잔고.종목명);
                    //                    if (Form1.form1.추매거부_List.Contains(잔고.종목명)) Form1.form1.추매거부_List.Remove(잔고.종목명);
                    //                    if (Form1.form1.매도거부_List.Contains(잔고.종목명)) Form1.form1.매도거부_List.Remove(잔고.종목명);

                    //                    Form1.form1.JanGo_dataGridView.Rows.Remove(Form1.form1.JanGo_dataGridView.Rows[i]);
                    //                    Form1.form1.JanGo_dataGridView.CurrentCell = null;
                    //                    Form1.stockBalanceList.Remove(잔고.종목코드);

                    //                    DataManagement.save_jango();
                    //                    Tab_AccountManagement.감시주문동기화();
                    //                }
                    //            }
                    //        }
                    //    }
                    //}


                    // DataGridView 행을 Dictionary에 담아 O(1) 시간 복잡도로 빠르게 조회합니다.
                    var jangoRows = new Dictionary<string, DataGridViewRow>();
                    foreach (DataGridViewRow row in Form1.form1.JanGo_dataGridView.Rows)
                    {
                        if (row.Cells["코드_잔고A"].Value != null)
                        {
                            jangoRows[row.Cells["코드_잔고A"].Value.ToString()] = row;
                        }
                    }

                    Form1.form1.JanGo_dataGridView.SuspendLayout(); // 렌더링 중지
                    try
                    {
                        // stockBalanceList를 순회하며 각 항목을 처리합니다.

                        foreach (var stockBalanceEntry in Form1.stockBalanceList)
                        {
                            var jango = stockBalanceEntry.Value;

                            // Dictionary를 사용하여 JanGo_dataGridView에서 해당 행을 직접 찾습니다.
                            if (jangoRows.TryGetValue(jango.종목코드, out var jangoRow))
                            {
                                if (jango.재매수 > 0)
                                {
                                    JangoRow_print(jangoRow.Index, jango);
                                }
                                else if (jango.전량매도)
                                {
                                    // 전량 매도 시 필요한 모든 작업을 논리적으로 묶어서 처리합니다.

                                    // 1. 주문 항목 취소 및 초기화
                                    var cancelList = Form1.JumunItem_List.FindAll(o => o.종목코드.Equals(jango.종목코드));
                                    foreach (var item in cancelList)
                                    {
                                        if (item != null)
                                        {
                                            item.반복횟수 = 0;
                                            item.취소시간 = 0;
                                            item.취소timer = 0;
                                        }
                                    }
                                    DataManagement.신규매수개수확인(jango.초기매수검색식, false);

                                    // 2. 로그 기록 (문자열 보간 사용)
                                    string logMsg = $"[전량매도] {jango.종목명} 수익금: {jango.누적손익:N0}이 전량 매도 되었습니다.";
                                    string errorLogDetails = $"{jango.종목명} 금일등락율: {jango.등락율} 수익률: {Math.Round(jango.수익률, 2)} 최고수익률: {Math.Round(jango.최고수익률, 2)} 최저수익률: {Math.Round(jango.최저수익률, 2)}";
                                    string errorLogFinancials = $"{jango.종목명} 금일수익금: {Method.단위변환(jango.금일수익금)} 총수익금: {Method.단위변환(jango.누적손익)} 최고예상손익금: {Method.단위변환(jango.최고예상손익금)} 최저예상손익금: {Method.단위변환(jango.최저예상손익금)} 금일매도금: {Method.단위변환(jango.금일매도금)}";
                                    string errorLogMisc = $"{jango.종목명} 매수검색식: {jango.초기매수검색식} 초기매수일: {jango.초기매수일:yyyy/MM/dd} 거래일: {jango.거래일}";

                                    Form1.동작_Log($"\n{logMsg}\n");
                                    Form1.Error_Log($"\n{errorLogDetails}\n{errorLogFinancials}\n{errorLogMisc}\n");

                                    // 3. 전량 매도 기록 저장
                                    string recordData = string.Join(";",
                                        Form1.today, jango.종목명, jango.초기매수검색식, jango.초기매수일.ToString("yyyy/MM/dd"), jango.거래일,
                                        Method.단위변환(jango.금일수익금), Method.단위변환(jango.누적손익), Method.단위변환(jango.최고예상손익금),
                                        Method.단위변환(jango.최저예상손익금), Method.단위변환(jango.금일매도금), jango.등락율,
                                        Math.Round(jango.수익률, 2), Math.Round(jango.최고수익률, 2), Math.Round(jango.최저수익률, 2),
                                        Method.단위변환(Form1.Acc[0].실현손익), Method.단위변환(Form1.Acc[0].증가자산));
                                    Writing_Management.전량매도기록(recordData);

                                    // 4. 텔레그램 메시지 전송 (옵션)
                                    if (Properties.Settings.Default.CB_텔레그램사용)
                                    {
                                        string telegramMsg = $"[전량매도] :: 종목: {jango.종목명} 매수식: {jango.초기매수검색식} 초기매수일: {jango.초기매수일:yyyy/MM/dd} 거래일: {jango.거래일} 수익률: {jango.수익률} 금일수익금: {Method.단위변환(jango.금일수익금)} 총수익금: {Method.단위변환(jango.누적손익)} 계좌실현손익: {Method.단위변환(Form1.Acc[0].실현손익)} 증가자산: {Method.단위변환(Form1.Acc[0].증가자산)} D2예수금: {Method.단위변환(Form1.Acc[0].추정D2)}";
                                        TelegramMessenger.Telegram_Send(telegramMsg);
                                    }

                                    // 5. 비프음 재생 및 실시간 시세 해지
                                    Form1.비프음("실행");
                                    if (!Properties.Settings.Default.CB_new_rebuy)
                                    {
                                        Form1.form1.실시간시세_List.Remove(jango.종목코드);
                                        Form1.form1.axKHOpenAPI1.SetRealRemove("ALL", jango.종목코드);
                                    }

                                    // 6. 재매수 리스트 업데이트
                                    var rebuystockItem = Form1.Rebuystock_List.Find(o => o.ItemCode.Contains(jango.종목코드));
                                    if (rebuystockItem != null)
                                    {
                                        rebuystockItem.결과 = (jango.금일수익금 < 0) ? "손실" : "수익";
                                        DataManagement.save_Rebuystock_List();
                                    }

                                    // 7. 기타 리스트에서 항목 제거
                                    Form1.form1.주문예약_List.RemoveAll(item => item.전량매도삭제 && item.종목코드.Equals(jango.종목코드));
                                    Form1.form1.CBB_최종가종목.Items.Remove(jango.종목명);
                                    Form1.form1.신규거부_List.Remove(jango.종목명);
                                    Form1.form1.추매거부_List.Remove(jango.종목명);
                                    Form1.form1.매도거부_List.Remove(jango.종목명);

                                    Console.WriteLine($"############## [{jango.종목명}] 잔고 전량 매도 ##############");

                                    // 8. UI 및 데이터 업데이트
                                    Form1.form1.JanGo_dataGridView.Rows.Remove(jangoRow);
                                    Form1.form1.JanGo_dataGridView.CurrentCell = null;
                                    Form1.stockBalanceList.Remove(jango.종목코드);
                                    DataManagement.save_jango();
                                    Tab_AccountManagement.감시주문동기화();
                                }
                            }
                        }
                    }
                    finally
                    {
                        Form1.form1.JanGo_dataGridView.ResumeLayout(); // 렌더링 재개
                    }
                }
            }
            catch
            {
                Form1.Error_Log("[에러] 잔고표시 에러");
            }
        }

        public static void JangoRow_print(int i, Stockbalance 잔고)
        {
          
            string 매매_가능 = "F";
                if (잔고.매매가능) 매매_가능 = "T";

                Form1.form1.JanGo_dataGridView["선택_잔고A", i].Value = 잔고.선택;

                DataGridViewComboBoxCell cbx = (DataGridViewComboBoxCell)Form1.form1.JanGo_dataGridView.Rows[i].Cells["그룹_잔고A"];
                cbx.Value = cbx.Items[잔고.매매그룹].ToString();

                Form1.form1.JanGo_dataGridView["평가금액_잔고A", i].Value = 잔고.평가금액;
                Form1.form1.JanGo_dataGridView["평가손익_잔고A", i].Value = 잔고.평가손익;
                Form1.form1.JanGo_dataGridView["누적손익_잔고A", i].Value = 잔고.누적손익;
                Form1.form1.JanGo_dataGridView["예상손익_잔고A", i].Value = 잔고.예상손익;
                Form1.form1.JanGo_dataGridView["수익률_잔고A", i].Value = 잔고.수익률;
                Form1.form1.JanGo_dataGridView["금일수익금_잔고A", i].Value = 잔고.금일수익금;

                if (Form1.form1.수익금or수익률)
                {
                    Form1.form1.JanGo_dataGridView["최고수익률_잔고A", i].Value = 잔고.최고예상손익금.ToString("###,###");
                    Form1.form1.JanGo_dataGridView["최저수익률_잔고A", i].Value = 잔고.최저예상손익금.ToString("###,###");
                }
                else
                {
                    Form1.form1.JanGo_dataGridView["최고수익률_잔고A", i].Value = 잔고.최고수익률;
                    Form1.form1.JanGo_dataGridView["최저수익률_잔고A", i].Value = 잔고.최저수익률;
                }

                if (잔고.시장.Equals("R"))
                {
                    잔고.시장 = Form1.Market_Item_List[잔고.종목코드].Market;
                }

                Form1.form1.JanGo_dataGridView["종목명_잔고A", i].Value = 잔고.종목명;
                Form1.form1.JanGo_dataGridView["시장구분_잔고A", i].Value = 잔고.시장;
                Form1.form1.JanGo_dataGridView["종목상태_잔고A", i].Value = 잔고.종목상태 + "｜" + 매매_가능;
                Form1.form1.JanGo_dataGridView["현재가_잔고A", i].Value = 잔고.현재가;
                Form1.form1.JanGo_dataGridView["등락율_잔고A", i].Value = 잔고.등락율;
                Form1.form1.JanGo_dataGridView["평균단가_잔고A", i].Value = 잔고.평균단가;
                Form1.form1.JanGo_dataGridView["보유수량_잔고A", i].Value = 잔고.보유수량;
                Form1.form1.JanGo_dataGridView["매입금액_잔고A", i].Value = 잔고.매입금액;
                Form1.form1.JanGo_dataGridView["금일매수금_잔고A", i].Value = 잔고.금일매수금;
                Form1.form1.JanGo_dataGridView["금일매도금_잔고A", i].Value = 잔고.금일매도금;
                Form1.form1.JanGo_dataGridView["매수횟수_잔고A", i].Value = 잔고.매수횟수;
                Form1.form1.JanGo_dataGridView["매도횟수_잔고A", i].Value = 잔고.매도횟수;
                Form1.form1.JanGo_dataGridView["거래일_잔고A", i].Value = 잔고.거래일;
                Form1.form1.JanGo_dataGridView["주문가능수량_잔고A", i].Value = 잔고.주문가능수량;
                Form1.form1.JanGo_dataGridView["보유비중_잔고A", i].Value = 잔고.보유비중;

                Form1.form1.JanGo_dataGridView["전일매수량_잔고A", i].Value = 잔고.전일매수량;
                Form1.form1.JanGo_dataGridView["전일매도량_잔고A", i].Value = 잔고.전일매도량;
                Form1.form1.JanGo_dataGridView["초기매수일_잔고A", i].Value = 잔고.초기매수일.ToString("yyyy/MM/dd");
                Form1.form1.JanGo_dataGridView["초기매수검색식", i].Value = 잔고.초기매수검색식;
                Form1.form1.JanGo_dataGridView["재매수_잔고A", i].Value = 잔고.재매수;
                Form1.form1.JanGo_dataGridView["추가매수일_잔고A", i].Value = 잔고.매수일;

                if (Properties.Settings.Default.CB_시작가격보기) { Form1.form1.JanGo_dataGridView["시작가", i].Value = 잔고.시작가격; Form1.form1.JanGo_dataGridView["시작%", i].Value = 매입가_수익률(잔고.시작가격); }
                if (Properties.Settings.Default.CB_기준가격보기)
                {
                    if (Form1.form1.기준값변경)
                    {
                        Form1.form1.JanGo_dataGridView["기준가", i].Value = 잔고.기준가격;
                        Form1.form1.JanGo_dataGridView["기준%", i].Value = 잔고.기준수익률;
                    }
                }

                if (Properties.Settings.Default.CB_최종매입가_A) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_A") && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); Form1.form1.JanGo_dataGridView["차수_A", i].Value = list.Count - 1; Form1.form1.JanGo_dataGridView["최종가_A", i].Value = 정렬_List[0].매입가; Form1.form1.JanGo_dataGridView["수익률_A", i].Value = 매입가_수익률(정렬_List[0].매입가); }
                if (Properties.Settings.Default.CB_최종매입가_B) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_B") && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); Form1.form1.JanGo_dataGridView["차수_B", i].Value = list.Count - 1; Form1.form1.JanGo_dataGridView["최종가_B", i].Value = 정렬_List[0].매입가; Form1.form1.JanGo_dataGridView["수익률_B", i].Value = 매입가_수익률(정렬_List[0].매입가); }
                if (Properties.Settings.Default.CB_최종매입가_C) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_C") && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); Form1.form1.JanGo_dataGridView["차수_C", i].Value = list.Count - 1; Form1.form1.JanGo_dataGridView["최종가_C", i].Value = 정렬_List[0].매입가; Form1.form1.JanGo_dataGridView["수익률_C", i].Value = 매입가_수익률(정렬_List[0].매입가); }
                if (Properties.Settings.Default.CB_최종매입가_D) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_D") && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); Form1.form1.JanGo_dataGridView["차수_D", i].Value = list.Count - 1; Form1.form1.JanGo_dataGridView["최종가_D", i].Value = 정렬_List[0].매입가; Form1.form1.JanGo_dataGridView["수익률_D", i].Value = 매입가_수익률(정렬_List[0].매입가); }
                if (Properties.Settings.Default.CB_최종매입가_E) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_E") && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); Form1.form1.JanGo_dataGridView["차수_E", i].Value = list.Count - 1; Form1.form1.JanGo_dataGridView["최종가_E", i].Value = 정렬_List[0].매입가; Form1.form1.JanGo_dataGridView["수익률_E", i].Value = 매입가_수익률(정렬_List[0].매입가); }
                if (Properties.Settings.Default.CB_최종매입가_F) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_F") && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); Form1.form1.JanGo_dataGridView["차수_F", i].Value = list.Count - 1; Form1.form1.JanGo_dataGridView["최종가_F", i].Value = 정렬_List[0].매입가; Form1.form1.JanGo_dataGridView["수익률_F", i].Value = 매입가_수익률(정렬_List[0].매입가); }
                if (Properties.Settings.Default.CB_최종매입가_G) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_G") && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); Form1.form1.JanGo_dataGridView["차수_G", i].Value = list.Count - 1; Form1.form1.JanGo_dataGridView["최종가_G", i].Value = 정렬_List[0].매입가; Form1.form1.JanGo_dataGridView["수익률_G", i].Value = 매입가_수익률(정렬_List[0].매입가); }

                if (Properties.Settings.Default.CB_익회모니터) Form1.form1.JanGo_dataGridView["일회모니터", i].Value = GET.전광판현황(잔고, "일회");
                if (Properties.Settings.Default.CB_익절모니터) Form1.form1.JanGo_dataGridView["익절 & 트레일링", i].Value = GET.전광판현황(잔고, "익절");
                if (Properties.Settings.Default.CB_보전모니터) Form1.form1.JanGo_dataGridView["보전모니터", i].Value = GET.전광판현황(잔고, "보전");
                if (Properties.Settings.Default.CB_손절모니터) Form1.form1.JanGo_dataGridView["손절모니터", i].Value = GET.전광판현황(잔고, "손절");
                if (Properties.Settings.Default.CB_시간청산범위) Form1.form1.JanGo_dataGridView["시간청산", i].Value = GET.전광판현황(잔고, "시간청산");
                if (Properties.Settings.Default.CB_반복매매범위) Form1.form1.JanGo_dataGridView["반복매매 범위모니터", i].Value = GET.전광판현황(잔고, "반복");
                if (Properties.Settings.Default.CB_리밸런싱범위) Form1.form1.JanGo_dataGridView["리밸런싱", i].Value = GET.전광판현황(잔고, "리밸");
                if (Properties.Settings.Default.CB_잔고청산범위) Form1.form1.JanGo_dataGridView["잔고청산", i].Value = GET.전광판현황(잔고, "잔고청산");

                if (Properties.Settings.Default.CB_매도X) Form1.form1.JanGo_dataGridView["매도_정지", i].Value = 잔고.매도정지;
                if (Properties.Settings.Default.CB_추매X) Form1.form1.JanGo_dataGridView["추매_정지", i].Value = 잔고.추매정지;
                if (Properties.Settings.Default.CB_잔고청산_A) Form1.form1.JanGo_dataGridView["잔고청산_A", i].Value = 잔고.잔고청산_A;
                if (Properties.Settings.Default.CB_잔고청산_B) Form1.form1.JanGo_dataGridView["잔고청산_B", i].Value = 잔고.잔고청산_B;
                if (Properties.Settings.Default.CB_잔고청산_C) Form1.form1.JanGo_dataGridView["잔고청산_C", i].Value = 잔고.잔고청산_C;

                double 매입가_수익률(int 매입가)
                {
                    double tax_ = Form1.TAX;
                    if (잔고.시장.Equals("E")) tax_ = 0;

                    return Math.Truncate((((double)(잔고.현재가 - 매입가) / 매입가 * (double)100) - ((Form1.수수료 + Form1.수수료 + tax_) * 100)) * 100) / 100;
                }

                if (Form1.form1.CBB_최종가종목.Text.Equals(잔고.종목명))
                {
                    DataGridView DGV = Form1.form1.최종매입가View;
                    if (DGV.Rows.Count > 0)
                    {
                        List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.종목코드.Equals(잔고.종목코드));
                        DGVupdata("리밸_A", 2);
                        DGVupdata("리밸_B", 4);
                        DGVupdata("리밸_C", 6);
                        DGVupdata("리밸_D", 8);
                        DGVupdata("리밸_E", 10);
                        DGVupdata("리밸_F", 12);
                        DGVupdata("리밸_G", 14);

                        void DGVupdata(string pos, int A)
                        {
                            List<최종매입가> list_A = list.FindAll(o => o.위치.Equals(pos));

                            if (list_A.Count > DGV.Rows.Count)
                            {
                                최종매입가view();
                            }
                            else
                            {
                                for (int a = 0; a < list_A.Count; a++)
                                {
                                    if (list_A[a] != null)
                                    {
                                        DGV[A, a].Value = 매입가_수익률(list_A[a].매입가).ToString("N2");
                                    }
                                }
                            }
                        }
                    }
                }
        
        }

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////           주문그리드뷰           ///////////////////

        public static int jumuncount = 1;
        public static void DGV_Jumun(string 주문시간, string 접수내역, string 종목명, int 현재가, string 주문유형, string 거래구분, int 주문가, int 수량, string 코드, string 비고, int 취소대기, int 취소N주문, string 검색식, string 주문번호, double Value, int 주문, int 반복)
        {
            string 기록_검색식 = 검색식;
            JumunItem JumunItem = Form1.JumunItem_List.Find(o => o.주문번호.Equals(주문번호));

            double 수익률 = 0;
            if (JumunItem != null)
            {
                수익률 = JumunItem.수익률;
            }

            var _Action = new Task(
            () =>
            {
                Form_JuMun.form.Invoke((MethodInvoker)delegate ()
                {
                    if (Properties.Settings.Default.TB_주문row > 0)
                    {
                        Form_JuMun.form.JuMun_dataGridView.Rows.Insert(0);

                        Form_JuMun.form.JuMun_dataGridView["Num_주문A", 0].Value = jumuncount;
                        Form_JuMun.form.JuMun_dataGridView["주문시간_주문A", 0].Value = 주문시간;
                        Form_JuMun.form.JuMun_dataGridView["검색식_주문A", 0].Value = 기록_검색식;
                        Form_JuMun.form.JuMun_dataGridView["접수내역_주문A", 0].Value = 접수내역;
                        Form_JuMun.form.JuMun_dataGridView["종목명_주문A", 0].Value = 종목명;
                        Form_JuMun.form.JuMun_dataGridView["P현재가_주문A", 0].Value = 현재가;
                        Form_JuMun.form.JuMun_dataGridView["주문유형_주문A", 0].Value = 주문유형;  // 매수n매도
                        Form_JuMun.form.JuMun_dataGridView["거래구분_주문A", 0].Value = 거래구분;  // 시장가n보통가
                        Form_JuMun.form.JuMun_dataGridView["수익률_주문A", 0].Value = Math.Round(수익률, 2);
                        Form_JuMun.form.JuMun_dataGridView["주문가_주문A", 0].Value = 주문가;
                        Form_JuMun.form.JuMun_dataGridView["수량_주문A", 0].Value = 수량;
                        Form_JuMun.form.JuMun_dataGridView["적용금액_주문A", 0].Value = Method.적용금액계산(주문가, 수량, 현재가);
                        Form_JuMun.form.JuMun_dataGridView["코드_주문A", 0].Value = 코드;
                        Form_JuMun.form.JuMun_dataGridView["취소대기_주문A", 0].Value = 취소대기 + "초";
                        Form_JuMun.form.JuMun_dataGridView["취소N주문_주문A", 0].Value = GET.cansel_order_string(취소N주문);
                        Form_JuMun.form.JuMun_dataGridView["P등락률_주문A", 0].Value = Form1.Market_Item_List[코드].등락율;
                        Form_JuMun.form.JuMun_dataGridView["주문번호_주문A", 0].Value = 주문번호;
                        Form_JuMun.form.JuMun_dataGridView["주문_주문A", 0].Value = GET.After_order_string(주문, Value);
                        Form_JuMun.form.JuMun_dataGridView["반복_주문A", 0].Value = 반복 + "회";
                        Form_JuMun.form.JuMun_dataGridView["비고_주문A", 0].Value = 비고;

                        Form_JuMun.form.JuMun_dataGridView.CurrentCell = null;
                    }

                    Writing_Management.주문기록(jumuncount + ";" + 주문유형 + ";" + 주문시간 + ";" + 종목명 + ";" + 기록_검색식 + ";" + Form1.Market_Item_List[코드].등락율 + ";" + 현재가 + ";" + 주문가 + ";" + 수량 + ";" + Method.적용금액계산(주문가, 수량, 현재가) + ";" + Math.Round(수익률, 2) + ";" + GET.After_order_string(주문, Value)
                            + ";" + 반복 + "회" + ";" + GET.cansel_order_string(취소N주문) + ";" + 거래구분 + ";" + 취소대기 + "초" + ";" + 코드 + ";" + 주문번호 + ";" + 접수내역 + ";" + 비고);

                    jumuncount++;
                });
            });
            _Action.Start();


        }

        //////////////////////           주문그리드뷰           ///////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////       미체결 그리드뷰 그리기        ////////////////

        public static void OUT_DGV_print()// 미체결그리드뷰 그리기
        {
            int Max_row = 0;
            int.TryParse(Form_Outstanding.form.TB_미체결row.Text, out int Max);
            Max_row = Max;

            //if (Max_row > 0 && Form1.form1.미체결종목_index < 1)
            //{
            //    int row = 0;

            //    if (Form_Outstanding.form.Outstanding_DataGridView.Rows.Count > Max_row && Form1.JumunItem_List.Count > Max_row)
            //    {
            //        row = 0;
            //        Form_Outstanding.form.Outstanding_DataGridView.Rows.Clear();

            //        while (true) // 감추기
            //        {
            //            if (Form_Outstanding.form.Outstanding_DataGridView.Rows.Count >= Max_row || Form1.JumunItem_List.Count <= Max_row) break;

            //            List<JumunItem> 오름차순_List = Form1.JumunItem_List.OrderBy(o => Math.Abs(o.Tik_cap)).ToList();
            //            List<JumunItem> 주문번호정렬_List = 오름차순_List.FindAll(o => !o.주문번호.Contains("+"));

            //            for (int i = 0; i < 주문번호정렬_List.Count; i++)
            //            {
            //                if (Form_Outstanding.form.Outstanding_DataGridView.Rows.Count >= Max_row || Form1.JumunItem_List.Count <= Max_row) break;

            //                JumunItem insert_item = 주문번호정렬_List[i];

            //                if (find_OutstandingStock(insert_item))
            //                {
            //                    Outstanding_insert(insert_item, row);
            //                    row++;
            //                }
            //            }

            //            if (주문번호정렬_List.Count <= Max_row) break;
            //        }
            //    }

            //    if (Form_Outstanding.form.Outstanding_DataGridView.Rows.Count < Max_row && Form1.JumunItem_List.Count > 0)
            //    {
            //        if (Form_Outstanding.form.Outstanding_DataGridView.Rows.Count != Form1.JumunItem_List.Count)
            //        {
            //            row = 0;
            //            Form_Outstanding.form.Outstanding_DataGridView.Rows.Clear();

            //            while (true) // 넣기
            //            {
            //                if ((Form1.JumunItem_List.Count >= Max_row && Form_Outstanding.form.Outstanding_DataGridView.Rows.Count >= Max_row) ||
            //                    (Form1.JumunItem_List.Count <= Max_row && Form1.JumunItem_List.Count == Form_Outstanding.form.Outstanding_DataGridView.Rows.Count)) break;

            //                List<JumunItem> 오름차순_List = Form1.JumunItem_List.OrderBy(o => Math.Abs(o.Tik_cap)).ToList();
            //                List<JumunItem> 주문번호정렬_List = 오름차순_List.FindAll(o => !o.주문번호.Contains("+"));

            //                for (int i = 0; i < 주문번호정렬_List.Count; i++)
            //                {
            //                    if ((Form1.JumunItem_List.Count >= Max_row && Form_Outstanding.form.Outstanding_DataGridView.Rows.Count >= Max_row) ||
            //                       (Form1.JumunItem_List.Count <= Max_row && Form1.JumunItem_List.Count == Form_Outstanding.form.Outstanding_DataGridView.Rows.Count)) break;

            //                    JumunItem insert_item = 주문번호정렬_List[i];

            //                    if (find_OutstandingStock(insert_item))
            //                    {
            //                        Outstanding_insert(insert_item, row);
            //                        row++;
            //                    }
            //                }

            //                if (주문번호정렬_List.Count <= Max_row) break;
            //            }
            //        }
            //    }
            //}

            if (Max_row > 0 && Form1.form1.미체결종목_index < 1)
            {
                // 정렬된 주문 목록을 한 번만 생성하여 재활용
                List<JumunItem> sortedList = Form1.JumunItem_List
                    .Where(o => !o.주문번호.Contains("+")) // '+'가 포함되지 않은 항목만 필터링
                    .OrderBy(o => Math.Abs(o.Tik_cap)) // Tik_cap의 절댓값으로 오름차순 정렬
                    .ToList();

                int displayCount = Math.Min(Max_row, sortedList.Count);

                // 데이터그리드뷰를 한 번만 초기화
                Form_Outstanding.form.Outstanding_DataGridView.Rows.Clear();

                // 필요한 만큼의 항목만 추가
                for (int i = 0; i < displayCount; i++)
                {
                    JumunItem item = sortedList[i];
                    if (find_OutstandingStock(item))
                    {
                        Outstanding_insert(item, Form_Outstanding.form.Outstanding_DataGridView.Rows.Count);
                    }
                }
            }

            if (Form1.form1.미체결종목_index > 0 && Form_Outstanding.form.Outstanding_DataGridView.Rows.Count == 0)
            {
                Form_Outstanding.form.CBB_미체결종목.SelectedIndex = 0;
            }

            foreach (DataGridViewRow row in Form_Outstanding.form.Outstanding_DataGridView.Rows)
            {
                if (row.Cells["검색식_미체결"].Value.ToString().Equals("취소") || row.Cells["검색식_미체결"].Value.ToString().Equals("체결"))
                {
                    Form_Outstanding.form.Outstanding_DataGridView.Rows.Remove(row);
                    Form_Outstanding.form.Outstanding_DataGridView.CurrentCell = null;
                }
                else
                {
                    JumunItem JumunItem = Form1.JumunItem_List.Find(o => o.주문번호.Equals(row.Cells["주문번호_미체결"].Value.ToString()));
                    if (JumunItem != null)
                    {
                        row.Cells["검색식_미체결"].Value = JumunItem.검색식;
                        row.Cells["취소대기_미체결"].Value = JumunItem.취소timer + "초";
                        row.Cells["반복_미체결"].Value = JumunItem.반복횟수 + " 회";
                        row.Cells["미체결량_미체결"].Value = JumunItem.미체결량;
                        row.Cells["현재가_미체결"].Value = JumunItem.현재가;
                        row.Cells["등락률_미체결"].Value = JumunItem.등락률;
                        row.Cells["수익률_미체결"].Value = JumunItem.수익률;
                        row.Cells["주문_미체결"].Value = GET.After_order_string(JumunItem.주문구분, JumunItem.주문값);
                        row.Cells["틱차이_미체결"].Value = JumunItem.Tik_cap + " 틱";
                        row.Cells["주문가격_미체결"].Value = JumunItem.주문가격;
                    }
                    else
                    {
                        if (row.Cells["취소대기_미체결"].Value.ToString().Equals("0초"))
                            row.Cells["검색식_미체결"].Value = "취소";
                        else
                            row.Cells["검색식_미체결"].Value = "체결";
                    }
                }
            }

            return;
        }



        public static bool find_OutstandingStock(JumunItem code)
        {
            bool ok = true;

            if (!code.주문번호.Contains("+"))
            {
                foreach (DataGridViewRow row in Form_Outstanding.form.Outstanding_DataGridView.Rows)
                {
                    if (row.Cells["주문번호_미체결"].Value.ToString().Equals(code.주문번호))
                    {
                        ok = false;
                        break;
                    }
                }
            }
            else
            {
                ok = false;
            }

            return ok;
        }

        public static void Outstanding_insert(JumunItem Item, int row)
        {
            Form_Outstanding.form.Invoke((MethodInvoker)delegate ()
            {
                if (!Item.주문번호.Contains("+"))
                {
                    Form_Outstanding.form.Outstanding_DataGridView.Rows.Insert(row);
                    Form_Outstanding.form.Outstanding_DataGridView["수익률_미체결", row].Value = Item.수익률;
                    Form_Outstanding.form.Outstanding_DataGridView["주문시간_미체결", row].Value = Item.주문시간.ToString("##:##:##");  //  <--- 
                    Form_Outstanding.form.Outstanding_DataGridView["검색식_미체결", row].Value = Item.검색식;
                    Form_Outstanding.form.Outstanding_DataGridView["주문번호_미체결", row].Value = Item.주문번호;
                    Form_Outstanding.form.Outstanding_DataGridView["종목코드_미체결", row].Value = Item.종목코드;
                    Form_Outstanding.form.Outstanding_DataGridView["종목명_미체결", row].Value = Item.종목명;
                    Form_Outstanding.form.Outstanding_DataGridView["주문유형_미체결", row].Value = 주문유형(Item.주문유형);
                    Form_Outstanding.form.Outstanding_DataGridView["거래구분_미체결", row].Value = GET.거래구분_to한글(Item.주문구분);
                    Form_Outstanding.form.Outstanding_DataGridView["취소대기_미체결", row].Value = Item.취소timer + "초";
                    Form_Outstanding.form.Outstanding_DataGridView["주문_미체결", row].Value = GET.After_order_string(Item.주문구분, Item.주문값);
                    Form_Outstanding.form.Outstanding_DataGridView["취소N거래구분_미체결", row].Value = GET.cansel_order_string(Item.취소N주문);
                    Form_Outstanding.form.Outstanding_DataGridView["주문수량_미체결", row].Value = Item.주문수량;
                    Form_Outstanding.form.Outstanding_DataGridView["미체결량_미체결", row].Value = Item.미체결량;
                    Form_Outstanding.form.Outstanding_DataGridView["현재가_미체결", row].Value = Item.현재가;
                    Form_Outstanding.form.Outstanding_DataGridView["등락률_미체결", row].Value = Item.등락률;
                    Form_Outstanding.form.Outstanding_DataGridView["반복_미체결", row].Value = Item.반복횟수 + " 회";
                    Form_Outstanding.form.Outstanding_DataGridView["주문가격_미체결", row].Value = Item.주문가격;
                    Form_Outstanding.form.Outstanding_DataGridView["적용금액_미체결", row].Value = Method.적용금액계산(Item.주문가격, Item.주문수량, Item.현재가);
                    Form_Outstanding.form.Outstanding_DataGridView["틱차이_미체결", row].Value = Item.Tik_cap + " 틱";

                    Form_Outstanding.form.Outstanding_DataGridView.CurrentCell = null;
                }
            });

            string 주문유형(int num)
            {
                string 유형 = "";
                if (num == 1) 유형 = "매수";
                if (num == 2) 유형 = "매도";
                if (num == 3) 유형 = "수취";
                if (num == 4) 유형 = "도취";
                return 유형;
            }
        }

        //////////////////////       미체결 그리드뷰 그리기        ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        public static void 최종매입가view()
        {
            Form1.form1.최종매입가View.Rows.Clear();
            DataGridView DGV = Form1.form1.최종매입가View;
            Stockbalance 잔고 = null;
            string ItemCode = "";
            foreach (var item in Form1.stockBalanceList.ToList())
            {
                if (item.Value.종목명.Equals(Form1.form1.CBB_최종가종목.Text))
                {
                    ItemCode = item.Value.종목코드;
                    잔고 = item.Value;
                }
            }

            List<최종매입가> list = Form1.최종매입가_List.ToList().FindAll(o => o.종목코드.Equals(ItemCode));
            DGVinsert("리밸_A", 1, 2);
            DGVinsert("리밸_B", 3, 4);
            DGVinsert("리밸_C", 5, 6);
            DGVinsert("리밸_D", 7, 8);
            DGVinsert("리밸_E", 9, 10);
            DGVinsert("리밸_F", 11, 12);
            DGVinsert("리밸_G", 13, 14);

            void DGVinsert(string pos, int A, int B)
            {
                List<최종매입가> list_A = list.FindAll(o => o.위치.Equals(pos));
                for (int i = 0; i < list_A.Count; i++)
                {
                    if (Row_add(i)) DGV.Rows.Add();

                    DGV[0, i].Value = i;
                    DGV[A, i].Value = list_A[i].매입가.ToString("N0");
                    DGV[B, i].Value = 매입가_수익률(list_A[i].매입가).ToString("N2");
                }
            }

            bool Row_add(int index)
            {
                bool result = true;
                for (int n = 0; n < DGV.RowCount; n++)
                {
                    if (DGV[0, n].Value.Equals(index))
                    {
                        result = false;
                    }
                }

                return result;
            }

            double 매입가_수익률(int 매입가)
            {
                double tax_ = Form1.TAX;
                if (잔고.시장.Equals("E")) tax_ = 0;

                return Math.Truncate((((double)(잔고.현재가 - 매입가) / 매입가 * (double)100) - ((Form1.수수료 + Form1.수수료 + tax_) * 100)) * 100) / 100;
            }

            DGV.ClearSelection();
        }
    }
}
