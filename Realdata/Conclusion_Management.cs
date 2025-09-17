using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 지니_64.box;

namespace 지니_64
{
    public class Conclusion_Management
    {
        public static void 잔고업데이트(Stockbalance 잔고, int 현재가, int 보유수량, int 평균단가, long 매입금액, long 실현손익)
        {
            Form1.Acc[0].실현손익 = 실현손익;

            if (보유수량 == 0) 평균단가 = 잔고.평균단가;
            잔고.현재가 = 현재가;
            잔고.보유수량 = 보유수량;
            잔고.평균단가 = 평균단가;
            잔고.매입금액 = 매입금액;

            잔고.보유비중 = Math.Round((double)잔고.매입금액 / (double)Properties.Settings.Default.MT_buying_standard * 100, 2);

            if (잔고.매도기준update)
            {
                잔고.매도기준 = 보유수량;
            }
            else
            {
                if (잔고.매도기준 < 잔고.보유수량) 잔고.매도기준 = 잔고.보유수량;
            }

            double tax_ = Form1.TAX;
            if (잔고.시장.Equals("E")) tax_ = 0;

            int 잔고수수료 = (int)Math.Round((잔고.현재가 * 잔고.보유수량 * Form1.수수료 / 10) * 10);
            int 잔고세금 = (int)Math.Round((잔고.현재가 * 잔고.보유수량 * tax_ / 10) * 10);

            잔고.수수료 = 잔고수수료;
            잔고.세금 = 잔고세금;

            if (잔고.보유수량 == 0)
            {
                잔고.전량매도 = true;
            }
        }

        public static void 체결잔고_취소기록(string 화면번호, string 코드b, string 종목명b, int 현재가b, string 주문유형b, string 거래구분b, int 주문수량b, string 원주문번호)
        {
            JumunItem JumunItem = Form1.JumunItem_List.Find(o => o.주문번호.Equals(원주문번호));
            if (JumunItem != null)
            {
                GridView_Print.DGV_Jumun(DateTime.Now.ToString("HH:mm:ss"), "성공", 종목명b, 현재가b, 주문유형b, 거래구분b, JumunItem.주문가격, 주문수량b, 코드b, JumunItem.비고, 0, 0, JumunItem.검색식, 원주문번호, 0, 1, 0);

                if (JumunItem.비고.Contains("'취소'") || (int.Parse(화면번호) < 1100 || 1299 < int.Parse(화면번호)))
                {
                    Jumun_remove(JumunItem);
                }

                재주문(JumunItem, 코드b, 종목명b, 주문수량b, 주문유형b.Substring(1));  //(JumunItem, 원주문번호, 코드b, 종목명b, 주문수량b, 화면번호, "재주문", 주문유형b.Substring(1), 주문가격b);
            }

            Tab_AccountManagement.취소_리밸주문(화면번호, 코드b, 주문유형b, 주문수량b);

            if (Form1.form1.CBscalping) Tab_Basic.취소_스켈핑주문(화면번호, 코드b);
        }

        private static void 재주문(JumunItem JumunItem, string 코드, string 종목명c, int 주문수량c, string 주문유형)
        {
            string Market = Form1.Market_Item_List[코드].Market;
            string 거래구분 = "00";
            if (JumunItem.취소N주문 == 1 || JumunItem.주문구분 == 0) // 시장가 
                거래구분 = "03";

            if (JumunItem.주문유형 == 3)
            {
                JumunItem.주문유형 = 1;
            }
            else if (JumunItem.주문유형 == 4)
            {
                JumunItem.주문유형 = 2;
            }

            if (JumunItem.비고.Contains("미체결"))
            {
                if (JumunItem.취소N주문.Equals(0))    //후 취소
                {
                    if (JumunItem.검색식.Contains("신규_"))
                    {
                        if (JumunItem.주문수량 == JumunItem.미체결량)
                        {
                            Properties.Settings.Default.신규횟수--;
                            Form1.신규count--;
                        }
                    }

                    주문삭제();
                }
                else if (JumunItem.취소N주문.Equals(1))   //후 시장가
                {
                    if (JumunItem.반복횟수 > 0)
                    {
                        JumunItem.반복횟수--;

                        JumunItem.비고 = "미체결 시장가";

                        JumunItem.주문번호 = "+++++";
                        JumunItem.Order_count = 0;
                        JumunItem.주문가격 = JumunItem.현재가;
                        JumunItem.Tik_cap = Method.Find_Tik_Cap(JumunItem.현재가, JumunItem.현재가, Market);
                        JumunItem.주문구분 = 0;
                        JumunItem.주문값 = 0;

                        거래구분 = "03";

                        Form1.que_order(JumunItem.screennum, 종목명c, JumunItem.주문유형, 코드, 주문수량c, JumunItem.현재가, 거래구분, "+++++", "취소후주문", JumunItem.Order번호);
                        Form1.동작_Log("[재주문] " + 종목명c + " 을 " + GET.거래구분변환(거래구분) + " 주문 가격: 시장가 주문수량: " + 주문수량c + " 으로 재주문합니다.");
                    }
                    else
                    {
                        주문삭제();
                    }
                }
                else if (JumunItem.취소N주문.Equals(2))   //후 현재가
                {
                    if (JumunItem.반복횟수 > 0)
                    {
                        JumunItem.반복횟수--;

                        JumunItem.비고 = "미체결 현재가";

                        JumunItem.주문번호 = "+++++";
                        JumunItem.Order_count = 0;
                        JumunItem.주문가격 = JumunItem.현재가;
                        JumunItem.Tik_cap = Method.Find_Tik_Cap(JumunItem.현재가, JumunItem.현재가, Market);
                        JumunItem.주문구분 = 1;
                        JumunItem.주문값 = 0;

                        Form1.que_order(JumunItem.screennum, 종목명c, JumunItem.주문유형, 코드, 주문수량c, JumunItem.현재가, 거래구분, "+++++", "취소후주문", JumunItem.Order번호);
                        Form1.동작_Log("[재주문] " + 종목명c + " 을 " + GET.거래구분변환(거래구분) + " 주문 가격: 현재가 주문수량: " + 주문수량c + " 으로 재주문합니다.");
                    }
                    else
                    {
                        주문삭제();
                    }
                }
                else if (JumunItem.취소N주문.Equals(3))   //후 재주문
                {
                    if (JumunItem.반복횟수 > 0)
                    {
                        JumunItem.반복횟수--;

                        JumunItem.비고 = "미체결 재주문";

                        JumunItem.주문번호 = "+++++";
                        JumunItem.Order_count = 0;
                        int 주문가격 = Method.order_price(JumunItem.주문값, JumunItem.주문구분, 코드, JumunItem.현재가);
                        JumunItem.Tik_cap = Method.Find_Tik_Cap(JumunItem.현재가, 주문가격, Market);

                        Form1.que_order(JumunItem.screennum, 종목명c, JumunItem.주문유형, 코드, 주문수량c, 주문가격, 거래구분, "+++++", "취소후주문", JumunItem.Order번호);
                        Form1.동작_Log("[재주문] " + 종목명c + " 을 " + GET.거래구분변환(거래구분) + " 주문 가격: " + 주문가격 + " 주문수량: " + 주문수량c + " 으로 재주문합니다.");
                    }
                    else
                    {
                        주문삭제();
                    }
                }
            }
            else
            {
                주문삭제();
            }

            void 주문삭제()
            {
                long 전_추정예수금 = Form1.추정예수금;

                if (!JumunItem.가동전)
                {
                    DataManagement.예수금업데이트(주문유형, JumunItem.주문가격, 주문수량c, "취소", JumunItem.종목코드);

                    if (Form1.timenow > Properties.Settings.Default.MT_misu_time && Properties.Settings.Default.CB_misu && Properties.Settings.Default.Combo_misu != 0)
                    {
                        if (JumunItem.주문유형.Equals(2))
                        {
                            Form1.추정예수금 = 전_추정예수금 - (JumunItem.주문가격 * 주문수량c);
                        }
                    }
                }

                if (Form1.stockBalanceList.TryGetValue(코드, out Stockbalance 잔고))
                {
                    DataManagement.주문가능수업데이트(잔고, 주문유형, 주문수량c, "취소");
                }

                Form1.동작_Log("[주문취소] " + 종목명c + " 을 " + GET.거래구분변환(거래구분) + " 주문 가격: " + Method.order_price(JumunItem.주문값, JumunItem.주문구분, 코드, JumunItem.현재가) + " 주문수량: " + 주문수량c + " 주문 취소 합니다.");

                Jumun_remove(JumunItem);
            }
        }

        public static void 체결잔고_주문기록(string screennum, string 코드a, string 종목명a, int 현재가a, string 주문유형a, string 거래구분a, int 주문가격a, int 주문수량a, string 주문번호a, int 주문N체결시간, int 미체결량)
        {
            if (int.Parse(screennum) < 1100 || 1299 < int.Parse(screennum))
            {
                string 검색식 = "HTS주문";   // HTS 주문으로 매수, 매도      # 주문 아이템에 등록 해주어야 한다.
                int OrderN = GET.Order번호();

                if (!주문유형a.Contains("취소"))
                {
                    int 유형 = 2;
                    if (주문유형a.Contains("매수"))
                    {
                        유형 = 1;
                        DataManagement.예수금업데이트(주문유형a, 주문가격a, 주문수량a, "주문", 코드a);
                    }
                    else
                    {
                        DataManagement.주문가능수업데이트(Form1.stockBalanceList[코드a], "매도", 주문수량a, "매도주문");
                    }
                    string Market = Form1.Market_Item_List[코드a].Market;
                    int tik_price = 현재가a;
                    int tik_cap = Method.Find_Tik_Cap(tik_price, 주문가격a, Market);
                    double 수익률 = 0;


                    JumunItem JumunItem = new JumunItem(0, 0, screennum, 코드a, 종목명a, 주문번호a, "---", 검색식, 0, 1, 99999, 0, 0, "", "HTS주문접수", 주문수량a, 주문가격a, 유형, 0, 0, 99999, 현재가a, Form1.Market_Item_List[코드a].등락율, 주문N체결시간,
                                                            미체결량, true, false, 0, tik_cap, tik_price, 수익률, false, 0, OrderN, 0, Form1.NXT_server);
                    Form1.JumunItem_List.Add(JumunItem);

                    GridView_Print.Outstanding_insert(JumunItem, 0); // 수동주문
                }

                GridView_Print.DGV_Jumun(DateTime.Now.ToString("HH:mm:ss"), "성공", 종목명a, 현재가a, 주문유형a, 거래구분a, 주문가격a, 주문수량a, 코드a, " ", 99999, 0, 검색식, 주문번호a, 0, 1, 0);

                long 주문금액 = 주문가격a * 주문수량a;
                if (주문가격a == 0)
                    주문금액 = 현재가a * 주문수량a;

                Form1.동작_Log("");
                Form1.동작_Log("[수동접수알림] 종목명: " + 종목명a + " 주문유형: " + 주문유형a + "\n주문가격: " + 주문가격a.ToString("N0") + " 주문수량: " + 주문수량a.ToString("N0") + " 주문금액: " + 주문금액.ToString("N0") + "\nHTS 주문접수되었습니다.");
                Form1.동작_Log("");

                Form1.알림창("[ 수동접수알림 ]\n\n종목명: " + 종목명a + " 주문유형: " + 주문유형a + "\n\n주문가격: " + 주문가격a.ToString("N0") + " 주문수량: " + 주문수량a.ToString("N0") + " 주문금액: " + 주문금액.ToString("N0") + "\n\nHTS 주문접수되었습니다.", 5, false);

                DataManagement.SAVE_주문리스트();
            }
            else
            {
                if (!주문유형a.Contains("취소"))
                {
                    JumunItem JumunItem = Form1.JumunItem_List.Find(o => o.screennum.Equals(screennum) && o.종목코드.Equals(코드a));
                    if (JumunItem != null)
                    {
                        string 주문번호 = "";

                        if (!JumunItem.원주문번호.Contains("-"))
                        {
                            주문번호 = JumunItem.원주문번호;

                            var _Action = new Task(
                            () =>
                            {
                                for (int n = 0; n < Form_Outstanding.form.Outstanding_DataGridView.RowCount; n++)
                                {
                                    if (Form_Outstanding.form.Outstanding_DataGridView["주문번호_미체결", n].Value != null)
                                    {
                                        if (Form_Outstanding.form.Outstanding_DataGridView["주문번호_미체결", n].Value.Equals(주문번호))
                                        {
                                            Form_Outstanding.form.Outstanding_DataGridView["주문번호_미체결", n].Value = 주문번호a;
                                        }
                                    }
                                }
                            });
                            _Action.Start();
                        }

                        if (JumunItem.비고.Contains("미체결일괄"))
                        {
                            JumunItem.취소timer = 0;
                            JumunItem.취소시간 = 0;
                        }

                        JumunItem.주문취소 = true;
                        JumunItem.주문번호 = 주문번호a;
                        JumunItem.주문가격 = 주문가격a;
                        JumunItem.주문수량 = 주문수량a;
                        JumunItem.등락률 = JumunItem.등락률;
                        JumunItem.주문시간 = 주문N체결시간;
                        JumunItem.미체결량 = 미체결량;
                        JumunItem.취소timer = JumunItem.취소시간;

                        if (JumunItem.검색식.Contains("[감시]") || JumunItem.검색식.Contains("매도감시"))
                        {
                            감시주문 감시 = null;
                            if (JumunItem.감시번호 > 0)
                            {
                                감시 = Form1.감시주문_List.Find(o => o.감시번호.Equals(JumunItem.감시번호));
                            }
                            else 감시 = Form1.감시주문_List.Find(o => o.원주문번호.Equals(JumunItem.원주문번호));

                            if (감시 != null) 감시.원주문번호 = 주문번호a;
                            DataManagement.리밸_감시_List_기록();
                        }
                        else
                        {
                            if (JumunItem.원주문번호.Contains("-"))
                            {
                                GridView_Print.Outstanding_insert(JumunItem, 0); // 자동주문
                            }

                            JumunItem.원주문번호 = 주문번호a;
                        }


                        GridView_Print.DGV_Jumun(DateTime.Now.ToString("HH:mm:ss"), "성공", 종목명a, 현재가a, 주문유형a, 거래구분a, 주문가격a, 주문수량a, 코드a,
                        JumunItem.비고, JumunItem.취소시간, JumunItem.취소N주문, JumunItem.검색식, 주문번호a, JumunItem.주문값, JumunItem.주문구분, JumunItem.반복횟수);

                        DataManagement.SAVE_주문리스트();
                    }

                    주문예약 item = Form1.form1.주문예약_List.Find(o => o.스크린번호.Equals(screennum) && o.종목코드.Equals(코드a));
                    if (item != null)
                    {
                        item.주문번호 = 주문번호a;
                    }
                }
            }
        }

        public static void 체결잔고_체결기록(string 체_주문번호, string 코드, string 체_종목명, int 체_단위체결가, int 체_체결가, int 체_단위체결량, int 체_체결량, int 체_매매세금, int 체_매매수수료, int 체_주문수량, int 체_주문N체결시간,
                         string 체_주문유형, string 체_거래구분, int 체_현재가, int 체_미체결량, string 화면번호)
        {
            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                Stockbalance 잔고 = null;
                JumunItem JumunItem = Form1.JumunItem_List.Find(o => o.주문번호.Equals(체_주문번호));

                double 체_등락률 = 0;
                double 수익률 = 0;
                string 체_검색식 = "";
                if (JumunItem != null) 체_검색식 = JumunItem.검색식;

                if (Form1.stockBalanceList.TryGetValue(코드, out Stockbalance 잔고_))
                {
                    잔고 = 잔고_;
                    체_등락률 = 잔고.등락율;
                    수익률 = 잔고.수익률;
                }
                else
                {
                    체_등락률 = Form1.Market_Item_List[코드].등락율;
                }

                if (Form1.Conclusion_List.Contains(체_주문번호))
                {
                    if (JumunItem != null)//미체결그리드뷰 미체결량 업데이트
                    {
                        if (JumunItem.주문번호.Equals(체_주문번호))
                        {
                            JumunItem.미체결량 = 체_미체결량;
                        }
                    }

                    if (잔고 != null)
                    {
                        if (체_주문유형.Equals("+매수"))
                        {
                            int 주문가능수량 = 잔고.주문가능수량;
                            잔고.주문가능수량 = 주문가능수량 + 체_단위체결량;
                            잔고.금일매수금 = 잔고.금일매수금 + (체_단위체결가 * 체_단위체결량);
                        }
                        else
                        {
                            잔고.금일매도금 = 잔고.금일매도금 + (체_단위체결량 * 체_단위체결가);

                            long 실현손익 = ((체_단위체결가 - 잔고.평균단가) * 체_단위체결량) - 체_매매수수료 - 체_매매세금;
                            잔고.금일수익금 = 잔고.금일수익금 + 실현손익;

                            long 누적손익 = 잔고.누적손익;
                            잔고.누적손익 = 누적손익 + 실현손익;
                            잔고.예상손익 = 잔고.평가손익 + 잔고.누적손익;
                        }

                        잔고.세금 = 잔고.세금 + 체_매매세금;
                        잔고.수수료 = 잔고.수수료 + 체_매매수수료;
                    }

                    DataManagement.예수금업데이트(체_주문유형, 체_단위체결가, 체_단위체결량, "체결", 코드);

                    var _Action = new Task(
                    () =>
                    {
                        Form_Conclusion.form.Invoke((MethodInvoker)delegate ()
                        {
                            for (int i = 0; i < Form_Conclusion.form.Conclusion_DataGridView.RowCount; i++)
                            {
                                if (Form_Conclusion.form.Conclusion_DataGridView["주문번호_체결", i].Value.Equals(체_주문번호))
                                {
                                    Form_Conclusion.form.Conclusion_DataGridView["누적체결량_체결", i].Value = 체_체결량;
                                    Form_Conclusion.form.Conclusion_DataGridView["체결가_체결", i].Value = 체_체결가;
                                    Form_Conclusion.form.Conclusion_DataGridView["누적금액_체결", 0].Value = 체_체결가 * 체_체결량;
                                }
                            }
                        });
                    });
                    _Action.Start();


                    체결 종목 = Form1.체결기록list.Find(o => o.주문번호.Equals(체_주문번호));
                    if (종목 != null)
                    {
                        종목.체결량 = 체_체결량.ToString();
                    }

                    주문예약 예약종목 = Form1.form1.주문예약_List.Find(o => o.주문번호.Equals(체_주문번호));  //예약주문 미체결량 업데이트
                    if (예약종목 != null)
                    {
                        예약종목.체결수량 = 예약종목.체결수량 + 체_단위체결량;
                    }

                    Form1.동작_Log("[체결알림] '" + 체_주문유형.Substring(1) + "' " + 체_종목명 + " 등락률: " + 체_등락률 + " 수익률: " + 수익률 + " 체결가: " + 체_체결가.ToString("N0") + " 체결량: " + 체_단위체결량.ToString("N0") + " 미체결량: " + 체_미체결량.ToString("N0") + " 체결금액: " + (체_체결가 * 체_체결량).ToString("N0"));

                    체결삭제(체_현재가, 체_검색식);
                    예약체결(예약종목);
                }
                else
                {
                    if (체_체결량 > 0)
                    {
                        Form1.Conclusion_List.Add(체_주문번호);
                        DataManagement.예수금업데이트(체_주문유형, 체_체결가, 체_체결량, "체결", 코드);

                        if (잔고 != null)
                        {
                            수익률 = Math.Round(잔고.수익률, 2);
                            long 금일매수금 = 잔고.금일매수금;
                            int 주문가능수량 = 잔고.주문가능수량;

                            if (체_주문유형.Equals("+매수"))
                            {
                                if (!잔고.매수일.Equals(Form1.today))
                                {
                                    잔고.매수일 = Form1.today;
                                }

                                잔고.금일매수금 = 금일매수금 + (체_체결량 * 체_체결가);
                                잔고.주문가능수량 = 주문가능수량 + 체_체결량;
                                잔고.매수횟수++;
                            }
                            else
                            {
                                if (!잔고.매도일.Equals(Form1.today)) 잔고.매도일 = Form1.today;

                                잔고.금일매도금 = 잔고.금일매도금 + (체_체결량 * 체_체결가);
                                잔고.매도횟수++;

                                long 실현손익 = ((체_체결가 - 잔고.평균단가) * 체_체결량) - 체_매매수수료 - 체_매매세금;
                                잔고.금일수익금 = 잔고.금일수익금 + 실현손익;

                                long 누적손익 = 잔고.누적손익;
                                잔고.누적손익 = 누적손익 + 실현손익;
                                잔고.예상손익 = 잔고.평가손익 + 잔고.누적손익;
                            }

                            잔고.세금 = 잔고.세금 + 체_매매세금;
                            잔고.수수료 = 잔고.수수료 + 체_매매수수료;

                            if (JumunItem != null)
                            {
                                JumunItem.미체결량 = 체_미체결량;

                                if (JumunItem.수익구분 == 7)
                                {
                                    List<최종매입가> 매입가_List = Form1.최종매입가_List.FindAll(o => o.종목코드.Equals(코드));
                                    if (JumunItem.location.Equals("리밸_A")) { List<최종매입가> list = 매입가_List.FindAll(o => o.위치.Equals("리밸_A")); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); Form1.최종매입가_List.Add(new 최종매입가(코드, "리밸_A", 번호정렬[0].번호 + 1, 체_체결가)); }
                                    if (JumunItem.location.Equals("리밸_B")) { List<최종매입가> list = 매입가_List.FindAll(o => o.위치.Equals("리밸_B")); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); Form1.최종매입가_List.Add(new 최종매입가(코드, "리밸_B", 번호정렬[0].번호 + 1, 체_체결가)); }
                                    if (JumunItem.location.Equals("리밸_C")) { List<최종매입가> list = 매입가_List.FindAll(o => o.위치.Equals("리밸_C")); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); Form1.최종매입가_List.Add(new 최종매입가(코드, "리밸_C", 번호정렬[0].번호 + 1, 체_체결가)); }
                                    if (JumunItem.location.Equals("리밸_D")) { List<최종매입가> list = 매입가_List.FindAll(o => o.위치.Equals("리밸_D")); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); Form1.최종매입가_List.Add(new 최종매입가(코드, "리밸_D", 번호정렬[0].번호 + 1, 체_체결가)); }
                                    if (JumunItem.location.Equals("리밸_E")) { List<최종매입가> list = 매입가_List.FindAll(o => o.위치.Equals("리밸_E")); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); Form1.최종매입가_List.Add(new 최종매입가(코드, "리밸_E", 번호정렬[0].번호 + 1, 체_체결가)); }
                                    if (JumunItem.location.Equals("리밸_F")) { List<최종매입가> list = 매입가_List.FindAll(o => o.위치.Equals("리밸_F")); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); Form1.최종매입가_List.Add(new 최종매입가(코드, "리밸_F", 번호정렬[0].번호 + 1, 체_체결가)); }
                                    if (JumunItem.location.Equals("리밸_G")) { List<최종매입가> list = 매입가_List.FindAll(o => o.위치.Equals("리밸_G")); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); Form1.최종매입가_List.Add(new 최종매입가(코드, "리밸_G", 번호정렬[0].번호 + 1, 체_체결가)); }

                                    DataManagement.최종매입가_저장();
                                }
                            }
                        }
                        else
                        {
                            if (체_주문유형.Equals("+매수"))
                            {
                                if (Form1.Market_Item_List.ContainsKey(코드))
                                {
                                    DataManagement.New_StockBalance(코드, 체_종목명, 체_현재가, 체_체결가, 체_체결량, 체_검색식, 체_매매세금, 체_매매수수료);
                                    Form1.NewBuyWrite_List.Add(체_주문번호 + ";" + 코드);
                                    Form1.신규count--;
                                }
                                else
                                {
                                    Form1.알림창("[ 매매종목안내 ]\n\n코스피, 코스닥 종목만 매매가능합니다.", 20, false);
                                }
                            }
                        }

                        var _Action = new Task(
                        () =>
                        {
                            Form_Conclusion.form.Invoke((MethodInvoker)delegate ()
                            {
                                int.TryParse(Form1.form1.TB_체결row.Text, out int rowcount);
                                if (rowcount > 0)
                                {
                                    Form_Conclusion.form.Conclusion_DataGridView.Rows.Insert(0);
                                    Form_Conclusion.form.Conclusion_DataGridView["Num_체결", 0].Value = Form1.con_num;
                                    Form_Conclusion.form.Conclusion_DataGridView["체결시간_체결", 0].Value = 체_주문N체결시간.ToString("##:##:##");
                                    Form_Conclusion.form.Conclusion_DataGridView["종목명_체결", 0].Value = 체_종목명;
                                    Form_Conclusion.form.Conclusion_DataGridView["검색식_체결", 0].Value = 체_검색식;
                                    Form_Conclusion.form.Conclusion_DataGridView["주문유형_체결", 0].Value = 체_주문유형.Substring(1);
                                    Form_Conclusion.form.Conclusion_DataGridView["거래구분_체결", 0].Value = 체_거래구분;
                                    Form_Conclusion.form.Conclusion_DataGridView["체결가_체결", 0].Value = 체_체결가;
                                    Form_Conclusion.form.Conclusion_DataGridView["주문수량_체결", 0].Value = 체_주문수량;
                                    Form_Conclusion.form.Conclusion_DataGridView["누적체결량_체결", 0].Value = 체_체결량;
                                    Form_Conclusion.form.Conclusion_DataGridView["P현재가_체결", 0].Value = 체_현재가;
                                    Form_Conclusion.form.Conclusion_DataGridView["P등락률_체결", 0].Value = 체_등락률;
                                    Form_Conclusion.form.Conclusion_DataGridView["주문번호_체결", 0].Value = 체_주문번호;
                                    Form_Conclusion.form.Conclusion_DataGridView["코드_체결", 0].Value = 코드;
                                    Form_Conclusion.form.Conclusion_DataGridView["누적금액_체결", 0].Value = 체_체결가 * 체_체결량;
                                    Form_Conclusion.form.Conclusion_DataGridView.ClearSelection();
                                }

                                if (Properties.Settings.Default.TB_체결row > 0) Form_Conclusion.form.Conclusion_DataGridView["수익률_체결", 0].Value = 수익률;

                                체결 add = new 체결(Form1.con_num.ToString(), 체_주문N체결시간.ToString("##:##:##"), 체_종목명, 체_검색식, 체_주문유형.Substring(1), 체_거래구분, 수익률.ToString(), 체_체결가.ToString(), 체_주문수량.ToString(), 체_체결량.ToString(), 체_현재가.ToString(), 체_등락률.ToString(), 체_주문번호, 코드);
                                Form1.체결기록list.Add(add);

                                주문예약 예약종목 = Form1.form1.주문예약_List.Find(o => o.주문번호.Equals(체_주문번호));  //예약주문 미체결량 업데이트
                                if (예약종목 != null) 예약종목.체결수량 = 체_체결량;

                                Form1.동작_Log("[체결알림] '" + 체_주문유형.Substring(1) + "' " + 체_종목명 + " 등락률: " + 체_등락률 + " 수익률: " + 수익률 + " 체결가: " + 체_체결가.ToString("N0") + " 체결량: " + 체_체결량.ToString("N0") + " 미체결량: " + 체_미체결량.ToString("N0") + " 체결금액: " + (체_체결가 * 체_체결량).ToString("N0"));

                                체결삭제(체_현재가, 체_검색식);
                                예약체결(예약종목);
                                Form1.con_num++;
                            });
                        });
                        _Action.Start();
                    }
                }

                void 체결삭제(int 현재가, string 검색식)
                {
                    if (체_주문수량 == 체_체결량)
                    {
                        string 금일수익금 = "0";
                        string 누적수익금 = "0";
                        long 매입금 = 체_체결가 * 체_체결량;

                        if (잔고 != null)
                        {
                            금일수익금 = 잔고.금일수익금.ToString("N0");
                            누적수익금 = 잔고.누적손익.ToString("N0");

                            if (체_주문유형.Substring(1).Equals("매수"))
                            {
                                매입금 = 잔고.매입금액 + 매입금;
                            }
                            else
                            {
                                매입금 = 잔고.매입금액 - 매입금;
                                if (매입금 < 0 || 매입금 < 잔고.현재가) 매입금 = 0;
                            }

                            bool 텔알림 = false;

                            long 실현손익 = Form1.Acc[0].실현손익;
                            if (체_주문유형.Substring(1).Equals("매수"))
                            {
                                if (Properties.Settings.Default.CB_매수알림) 텔알림 = true;
                            }
                            else
                            {
                                실현손익 = 실현손익 + ((체_체결가 * 체_체결량) - (잔고.평균단가 * 체_체결량)) - (체_매매세금 + 체_매매수수료);
                                if (Properties.Settings.Default.CB_매도알림) 텔알림 = true;
                            }

                            if (텔알림)
                            {
                                TelegramMessenger.Telegram_Send("[체결알림 - " + 체_주문유형.Substring(1) + "<" + 체_종목명 + ">] : " + 검색식 +
                                    " : 체결가: " + 체_체결가.ToString("N0") +
                                    " 체결량: " + 체_체결량.ToString("N0") +
                                    " 체결금: " + Method.단위변환(체_체결가 * 체_체결량) +
                                    " 총매입: " + Method.단위변환(매입금) +
                                    " 등락률: " + 체_등락률 +
                                    " 수익률: " + 수익률 +
                                    " 금일수익: " + Method.단위변환(double.Parse(금일수익금)) +
                                    " 누적수익: " + Method.단위변환(double.Parse(누적수익금)) +
                                    " <계좌-실현손익: " + Method.단위변환(Form1.Acc[0].실현손익) +
                                    " D+2: " + Method.단위변환(Form1.Acc[0].D2) +
                                    " 총매입: " + Method.단위변환(Form1.Acc[0].매입금) + ">"
                                    );
                            }
                        }

                        if (JumunItem != null) Jumun_remove(JumunItem);
                        Form1.Conclusion_List.Remove(체_주문번호);
                        Form1.Conclusion_remove_List.Add(체_주문번호);
                        if (Form1.Conclusion_remove_List.Count.Equals(500)) Form1.Conclusion_remove_List.Remove(Form1.Conclusion_remove_List[0]);

                        감시주문 감시 = Form1.감시주문_List.Find(o => o.원주문번호.Equals(체_주문번호));
                        DataManagement.감시주문삭제(감시, "주문체결");
                    }
                }

                void 예약체결(주문예약 예약종목)
                {
                    if (예약종목 != null)
                    {
                        if (예약종목.주문수량 == 예약종목.체결수량)
                        {
                            if (예약종목.체결완료삭제)
                            {
                                Form1.알림창("[ 예약주문 체결삭제 ]\n\n종목명: " + 예약종목.종목명 + " 등록일: " + 예약종목.등록일 + " 주문가격: " + 예약종목.주문가.ToString("N0") + " 주문수량: " + 예약종목.주문수량.ToString("N0") + "\n\n체결수량: " + 예약종목.체결수량.ToString("N0") + " 매매금액: " + (예약종목.주문가 * 예약종목.체결수량).ToString("N0"), 5, false);

                                Form1.동작_Log(" ");
                                Form1.동작_Log("[예약주문 체결삭제] 종목명: " + 예약종목.종목명 + " 등록일: " + 예약종목.등록일 + " 주문가격: " + 예약종목.주문가.ToString("N0") + " 주문수량: " + 예약종목.주문수량.ToString("N0") + " 체결수량: " + 예약종목.체결수량.ToString("N0") + " 매매금액: " + (예약종목.주문가 * 예약종목.체결수량).ToString("N0"));
                                Form1.동작_Log(" ");
                                Console.WriteLine("[예약주문 체결삭제] 종목명: " + 예약종목.종목명 + " 등록일: " + 예약종목.등록일 + " 주문가격: " + 예약종목.주문가.ToString("N0") + " 주문수량: " + 예약종목.주문수량.ToString("N0") + " 체결수량: " + 예약종목.체결수량.ToString("N0") + " 매매금액: " + (예약종목.주문가 * 예약종목.체결수량).ToString("N0"));

                                Form1.form1.주문예약_List.Remove(예약종목);

                                Order_Reserve.save_주문예약();
                            }
                        }

                        Order_Reserve.save_주문예약();
                    }
                }

                if (체_주문유형.Equals("+매수"))
                {
                    if (Form1.form1.CBscalping)
                        Tab_Basic.스캘핑주문(코드, 체_체결가, 체_단위체결량, 체_미체결량, 화면번호);

                    Tab_AccountManagement.리밸매도(코드, 체_체결가, 체_단위체결량, 체_체결량, 체_주문수량, 화면번호);
                }
                else
                {
                    if (Form1.form1.CBscalping &&
                    ((Properties.Settings.Default.CB_scalping_A && Properties.Settings.Default.CB_new_A) ||
                     (Properties.Settings.Default.CB_scalping_B && Properties.Settings.Default.CB_new_B) ||
                     (Properties.Settings.Default.CB_scalping_C && Properties.Settings.Default.CB_new_C))) Tab_Basic.스켈핑차수조정(체_주문번호);
                }
            });
        }

        public static void Jumun_remove(JumunItem JumunItem)
        {
            if (JumunItem.미체결량 > 0)
            {
                if (Form1.stockBalanceList.TryGetValue(JumunItem.종목코드, out Stockbalance 잔고))
                {
                    if (JumunItem.검색식.Contains("수익금손절"))
                    {
                        int 단위손절금 = 잔고.현재가 - 잔고.평균단가;
                        Form1.실현손익_예상 = Form1.실현손익_예상 - (JumunItem.미체결량 * 단위손절금);
                    }
                }
            }

            JumunItem.검색식 = "삭제";

            for (int i = 0; i < Form_Outstanding.form.Outstanding_DataGridView.RowCount; i++)
            {
                if (Form_Outstanding.form.Outstanding_DataGridView["주문번호_미체결", i].Value.ToString().Equals(JumunItem.주문번호))
                {
                    Form_Outstanding.form.Outstanding_DataGridView["주문번호_미체결", i].Value = "삭제";
                }
            }

            Form1.JumunItem_List.Remove(JumunItem);
        }

    }
}
