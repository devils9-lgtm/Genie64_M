using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니_64
{
    public class RealData_Management
    {

        public static void Stock_update(bool 예상체결, string itemcode, string Stock_현재가, string Stock_등락율, string 누적_거래량, string 누적_거래대금, string Stock_시가)
        {
            var Task = new Task(() =>
            {
                if (Form1.Market_Item_List.ContainsKey(itemcode))
                {
                    Market_Item Market = Form1.Market_Item_List[itemcode];
                    double.TryParse(Stock_등락율, out double 등락율);
                    int 현재가 = Math.Abs(int.Parse(Stock_현재가));
                    int 시가 = Math.Abs(int.Parse(Stock_시가));
                    int.TryParse(누적_거래량, out int 누적거래량);
                    int.TryParse(누적_거래대금, out int 누적거래대금);

                    if (예상체결)
                    {
                        Market.등락율 = 등락율;
                        if (Form1.stockBalanceList.TryGetValue(itemcode, out Stockbalance 잔고_))
                        {
                            잔고_.등락율 = 등락율;
                        }
                    }
                    else
                    {
                        Market.현재가 = 현재가;
                        Market.start_price = 시가;
                        Market.등락율 = 등락율;
                        Market.누적거래량 = 누적거래량;
                        Market.누적거래대금 = 누적거래대금;

                        if (Form1.stockBalanceList.TryGetValue(itemcode, out Stockbalance 잔고))
                        {
                            if (!잔고.전량매도)
                            {
                                double tax_ = Form1.TAX;
                                if (잔고.시장.Equals("E")) tax_ = 0;

                                int 잔고현재가 = 잔고.현재가;
                                int 보유수량 = 잔고.보유수량;
                                int 매입금액 = 잔고.평균단가 * 보유수량;

                                잔고.현재가 = (int)현재가;
                                잔고.등락율 = 등락율;
                                잔고.평가금액 = (long)(잔고.현재가 * 보유수량);

                                double 세금_수수료 = Math.Truncate(((double)잔고.평균단가 * (double)잔고.보유수량 * Form1.수수료) + ((double)잔고.현재가 * (double)잔고.보유수량 * Form1.수수료) + ((double)잔고.현재가 * (double)잔고.보유수량 * tax_));

                                잔고.평가손익 = 잔고.평가금액 - 매입금액 - (long)세금_수수료;

                                double 수익률 = Math.Truncate((((double)(현재가 - 잔고.평균단가) / 잔고.평균단가 * (double)100) - ((Form1.수수료 + Form1.수수료 + tax_) * 100)) * 100) / 100;
                                잔고.수익률 = 수익률;

                                if (Form1.form1.기준값변경)
                                {
                                    잔고.기준수익률 = Math.Truncate((((double)(현재가 - 잔고.기준가격) / 잔고.기준가격 * (double)100) - ((Form1.수수료 + Form1.수수료 + tax_) * 100)) * 100) / 100;
                                }

                                잔고.시작수익률 = Math.Truncate((((double)(현재가 - 잔고.시작가격) / 잔고.시작가격 * (double)100) - ((Form1.수수료 + Form1.수수료 + tax_) * 100)) * 100) / 100;
                                잔고.예상손익 = 잔고.평가손익 + 잔고.누적손익;

                                if (0 < 잔고.수익률 && 잔고.최고수익률 < 잔고.수익률)
                                {
                                    잔고.최고수익률 = 잔고.수익률;
                                }

                                if (잔고.수익률 < 0 && 잔고.최저수익률 > 잔고.수익률)
                                {
                                    잔고.최저수익률 = 잔고.수익률;
                                }

                                if (0 < 잔고.예상손익 && 잔고.최고예상손익금 < 잔고.예상손익)
                                {
                                    잔고.최고예상손익금 = 잔고.예상손익;
                                }

                                if (잔고.예상손익 < 0 && 잔고.최저예상손익금 > 잔고.예상손익)
                                {
                                    잔고.최저예상손익금 = 잔고.예상손익;
                                }

                                if (Properties.Settings.Default.CB_최종가업데이트)
                                {
                                    if (잔고.시작가격 < 잔고.현재가)
                                    {
                                        List<최종매입가> List = Form1.최종매입가_List.FindAll(o => o.종목코드.Equals(itemcode));
                                        if (List.Count == 7)
                                        {
                                            최종매입가 item_A = List.Find(o => o.위치.Equals("리밸_A") && o.번호 == 0); if (item_A.매입가 < 잔고.현재가) item_A.매입가 = 잔고.현재가;
                                            최종매입가 item_B = List.Find(o => o.위치.Equals("리밸_B") && o.번호 == 0); if (item_B.매입가 < 잔고.현재가) item_B.매입가 = 잔고.현재가;
                                            최종매입가 item_C = List.Find(o => o.위치.Equals("리밸_C") && o.번호 == 0); if (item_C.매입가 < 잔고.현재가) item_C.매입가 = 잔고.현재가;
                                            최종매입가 item_D = List.Find(o => o.위치.Equals("리밸_D") && o.번호 == 0); if (item_D.매입가 < 잔고.현재가) item_D.매입가 = 잔고.현재가;
                                            최종매입가 item_E = List.Find(o => o.위치.Equals("리밸_E") && o.번호 == 0); if (item_E.매입가 < 잔고.현재가) item_E.매입가 = 잔고.현재가;
                                            최종매입가 item_F = List.Find(o => o.위치.Equals("리밸_F") && o.번호 == 0); if (item_F.매입가 < 잔고.현재가) item_F.매입가 = 잔고.현재가;
                                            최종매입가 item_G = List.Find(o => o.위치.Equals("리밸_G") && o.번호 == 0); if (item_G.매입가 < 잔고.현재가) item_G.매입가 = 잔고.현재가;
                                        }
                                    }
                                }
                            }

                            MA.Mma_Record(잔고);
                            MA.Dma_Record(잔고);
                        }

                        List<JumunItem> code_list = Form1.JumunItem_List.FindAll(o => o.종목코드.Equals(itemcode));
                        for (int i = 0; i < code_list.Count; i++)
                        {
                            JumunItem Item = code_list[i];

                            Item.현재가 = 현재가;
                            Item.등락률 = 등락율;

                            if (Form1.stockBalanceList.TryGetValue(itemcode, out Stockbalance 잔고_))
                            {
                                Item.수익률 = 잔고_.수익률;
                            }

                            if (현재가 != Item.Tik_price)
                            {
                                Item.Tik_cap = Method.Find_Tik_Cap(Item.현재가, Item.주문가격, Form1.Market_Item_List[itemcode].Market);
                                Item.Tik_price = 현재가;
                            }
                        }

                        Order_Reserve.수동주문_주문가변화(Market);
                    }
                }
            });
            Form1.Real_data_Manager.RequestTrData(Task);
        }


        public static void Market_update(string itemcode, string Jisu_등락율, string Jisu_현재가, string Jisu_저가, string Jisu_고가)
        {
            var Task = new Task(() =>
            {
                double 현재가 = Math.Abs(double.Parse(Jisu_현재가));
                double 등락율 = double.Parse(Jisu_등락율);
                double 저가 = Math.Abs(double.Parse(Jisu_저가));
                double 고가 = Math.Abs(double.Parse(Jisu_고가));

                if (Jisu_현재가.Equals(Jisu_저가) && Jisu_저가.Equals(Jisu_고가))
                {
                    if (itemcode.Equals("001")) // 코스피 등락율
                    {
                        Form1.Acc[0].피_현재가 = 현재가;
                        Form1.Acc[0].피_등락률 = 등락율;

                        AVG_jisu_print("001", 현재가);
                    }

                    if (itemcode.Equals("101"))// 코스닥 등락율
                    {
                        Form1.Acc[0].닥_현재가 = 현재가;
                        Form1.Acc[0].닥_등락률 = 등락율;

                        AVG_jisu_print("101", 현재가);
                    }
                }
                else
                {
                    double 업종고가대비 = (Math.Truncate((double)(현재가 - 고가) / 고가 * (double)100 * 100)) / 100;
                    double 업종저가대비 = (Math.Truncate((double)(현재가 - 저가) / 저가 * (double)100 * 100)) / 100;

                    if (itemcode.Equals("001")) // 코스피 등락율
                    {
                        if (Form1.kospi_avg_min.Count > 0) Form1.kospi_avg_min[0].endprice = 현재가;
                        if (Form1.kospi_avg_day.Count > 0) Form1.kospi_avg_day[0].endprice = 현재가;

                        Form1.Acc[0].피_현재가 = 현재가;
                        Form1.Acc[0].피_등락률 = 등락율;
                        Form1.Acc[0].피_저가대비 = 업종저가대비;
                        Form1.Acc[0].피_고가대비 = 업종고가대비;

                        AVG_jisu_print("001", 현재가);
                    }

                    if (itemcode.Equals("101"))// 코스닥 등락율
                    {
                        if (Form1.kosdaq_avg_min.Count > 0) Form1.kosdaq_avg_min[0].endprice = 현재가;
                        if (Form1.kosdaq_avg_day.Count > 0) Form1.kosdaq_avg_day[0].endprice = 현재가;

                        Form1.Acc[0].닥_현재가 = 현재가;
                        Form1.Acc[0].닥_등락률 = 등락율;
                        Form1.Acc[0].닥_저가대비 = 업종저가대비;
                        Form1.Acc[0].닥_고가대비 = 업종고가대비;

                        AVG_jisu_print("101", 현재가);
                    }
                }
            });
            Form1.Real_data_Manager.RequestTrData(Task);
        }



        public static void AVG_jisu_print(string market, double now_price)
        {
            if (market.Equals("001")) //  코스피
            {
                avg_min(Form1.AVG_jisu[0], Form1.kospi_avg_min);
                avg_day(Form1.AVG_jisu[0], Form1.kospi_avg_day);
            }
            if (market.Equals("101")) // 코스닥
            {
                avg_min(Form1.AVG_jisu[1], Form1.kosdaq_avg_min);
                avg_day(Form1.AVG_jisu[1], Form1.kosdaq_avg_day);
            }

            void avg_min(AVG_jisu avg, List<AVG_price> list)
            {
                double sum = 0;
                var date = list.OrderByDescending(o => o.date).ToList();
                for (int i = 0; i < date.Count; i++)
                {
                    sum = sum + date[i].endprice;
                    if (i + 1 == 3) { avg.min_03 = avg_check(sum / (i + 1)); avg.check_min_03 = check(avg.use_min_03, avg.UD_min_03, avg.min_03); }
                    if (i + 1 == 5) { avg.min_05 = avg_check(sum / (i + 1)); avg.check_min_05 = check(avg.use_min_05, avg.UD_min_05, avg.min_05); }
                    if (i + 1 == 10) { avg.min_10 = avg_check(sum / (i + 1)); avg.check_min_10 = check(avg.use_min_10, avg.UD_min_10, avg.min_10); }
                    if (i + 1 == 20) { avg.min_20 = avg_check(sum / (i + 1)); avg.check_min_20 = check(avg.use_min_20, avg.UD_min_20, avg.min_20); }
                    if (i + 1 == 30) { avg.min_30 = avg_check(sum / (i + 1)); avg.check_min_30 = check(avg.use_min_30, avg.UD_min_30, avg.min_30); }
                    if (i + 1 == 60) { avg.min_60 = avg_check(sum / (i + 1)); avg.check_min_60 = check(avg.use_min_60, avg.UD_min_60, avg.min_60); }
                }
            }

            void avg_day(AVG_jisu avg, List<AVG_price> list)
            {
                double sum = 0;
                var date = list.OrderByDescending(o => o.date).ToList();
                for (int i = 0; i < date.Count; i++)
                {
                    sum = sum + date[i].endprice;
                    if (i + 1 == 3) { avg.day_03 = avg_check(sum / (i + 1)); avg.check_day_03 = check(avg.use_day_03, avg.UD_day_03, avg.day_03); }// Console.WriteLine("3::: " + sum / (i + 1) + " now_price : "+ now_price+ "   avg.day_03: " + avg.day_03); }  
                    if (i + 1 == 5) { avg.day_05 = avg_check(sum / (i + 1)); avg.check_day_05 = check(avg.use_day_05, avg.UD_day_05, avg.day_05);  }// Console.WriteLine("5::: " + sum / (i + 1) + " now_price : " + now_price + "   avg.day_05: " + avg.day_05); }
                    if (i + 1 == 10) { avg.day_10 = avg_check(sum / (i + 1)); avg.check_day_10 = check(avg.use_day_10, avg.UD_day_10, avg.day_10); }
                    if (i + 1 == 20) { avg.day_20 = avg_check(sum / (i + 1)); avg.check_day_20 = check(avg.use_day_20, avg.UD_day_20, avg.day_20); }
                    if (i + 1 == 40) { avg.day_40 = avg_check(sum / (i + 1)); avg.check_day_40 = check(avg.use_day_40, avg.UD_day_40, avg.day_40); }
                    if (i + 1 == 60) { avg.day_60 = avg_check(sum / (i + 1)); avg.check_day_60 = check(avg.use_day_60, avg.UD_day_60, avg.day_60); }
                }
            }

            bool avg_check(double avg_price)
            {
                if (avg_price < now_price)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            bool check(bool use, bool UD, bool check_)
            {
                if (use)
                {
                    if (UD) // 이상
                    {
                        if (check_)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (check_)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        public static void Market_fluctuate(string itemcode, string 누적거래대금, string 상한종목수, string 상승종목수, string 보합종목수, string 하한종목수, string 하락종목수)
        {
            var Task = new Task(() =>
            {
                Form1.form1.Invoke((MethodInvoker)delegate ()
                {
                    if (itemcode.Equals("001")) // 코스피 등락율
                    {
                        Form1.Acc[0].피_누적거래대금 = 누적거래대금;
                        Form1.Acc[0].피_상한종목수 = 상한종목수;
                        Form1.Acc[0].피_상승종목수 = 상승종목수;
                        Form1.Acc[0].피_보합종목수 = 보합종목수;
                        Form1.Acc[0].피_하한종목수 = 하한종목수;
                        Form1.Acc[0].피_하락종목수 = 하락종목수;

                        Form1.form1.LB_코스피대금.Text = "금:" + int.Parse(누적거래대금) / 100 + "억";
                        Form1.form1.LB_코스피상승.Text = "▲:" + 상한종목수 + " △:" + 상승종목수;
                        Form1.form1.LB_코스피보합.Text = "－:" + 보합종목수;
                        Form1.form1.LB_코스피하락.Text = "▽:" + 하락종목수 + " ▼:" + 하한종목수;

                    }
                    else if (itemcode.Equals("101"))// 코스닥 등락율
                    {
                        Form1.Acc[0].닥_누적거래대금 = 누적거래대금;
                        Form1.Acc[0].닥_상한종목수 = 상한종목수;
                        Form1.Acc[0].닥_상승종목수 = 상승종목수;
                        Form1.Acc[0].닥_보합종목수 = 보합종목수;
                        Form1.Acc[0].닥_하한종목수 = 하한종목수;
                        Form1.Acc[0].닥_하락종목수 = 하락종목수;

                        Form1.form1.LB_코스닥대금.Text = "금:" + int.Parse(누적거래대금) / 100 + "억";
                        Form1.form1.LB_코스닥상승.Text = "▲:" + 상한종목수 + " △:" + 상승종목수;
                        Form1.form1.LB_코스닥보합.Text = "－:" + 보합종목수;
                        Form1.form1.LB_코스닥하락.Text = "▽:" + 하락종목수 + " ▼:" + 하한종목수;
                        //↑▲△↓▼▽－


                    }
                });
            });
            Form1.Real_data_Manager.RequestTrData(Task);
        }

        public static void Real_Watch_update(string itemcode, string Real_등락율, string Real_현재가, string Real_시가, string Real_고가, string Real_저가, string 누적거래량, string Real_누적거래대금, string Real_거래대금증감, string 전일거래량대비, string 거래회전율, string 시가총액)
        {
            var Task = new Task(() =>
            {
                if (Form1.Watch_List.Count > 0)
                {
                    if (Form1.Market_Item_List.ContainsKey(itemcode))
                    {
                        double.TryParse(Real_등락율, out double 등락율);
                        double.TryParse(Real_현재가, out double 현재가);
                        double.TryParse(Real_누적거래대금, out double 누적거래대금);
                        int.TryParse(Real_시가, out int 시가);
                        int.TryParse(Real_고가, out int 고가);
                        int.TryParse(Real_저가, out int 저가);
                        double.TryParse(Real_거래대금증감, out double 거래대금증감);

                        Tab_Watch.Watch_update(itemcode, 등락율, Math.Abs(현재가), 누적거래량, 누적거래대금, Math.Abs(시가), Math.Abs(고가), Math.Abs(저가), 거래대금증감, 전일거래량대비, 거래회전율, 시가총액);
                    }
                }
            });
            Form1.Real_data_Manager.RequestTrData(Task);
        }

    }
}
