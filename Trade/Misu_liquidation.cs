using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 지니_64
{
    class Misu_liquidation
    {

        /////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        /////////////////////            미수정리 매매             ////////////////

        public static void 미수정리()
        {
            if (Properties.Settings.Default.CB_misu && Properties.Settings.Default.Combo_misu > 0)
            {
                string 시간 = Properties.Settings.Default.MT_misu_time.ToString();

                if (Properties.Settings.Default.MT_misu_time < 100000)
                {
                    시간 = "0" + 시간;
                }

                DateTime 정리시작 = DateTime.ParseExact(시간, "HHmmss", null);

                int 추가시간 = (int)(Form1.JumunItem_List.Count / 2.7);
                int 정리시간 = int.Parse(정리시작.AddSeconds(-(추가시간 + 10)).ToString("HHmmss"));

                if (Form1.timenow > 정리시간 && Form1.Acc[0].추정D2 < 0 )
                {
                    if (Form1.form1.미수정리_미수알림)
                    {
                        Form1.form1.미수정리_미수알림 = false;

                        Form1.form1.RB_sell_stop.Checked = true;
                        Form1.form1.RB_buy_stop.Checked = true;
                        Form1.form1.RB_sell_run.Checked = true;

                        Form1.Delay(500);

                        Form1.동작_Log(" ");
                        Form1.동작_Log("[미수금정리] " + Form1.form1.Combo_misu.Text + "기준 미수금을 정리 합니다. 미체결 주문이 일괄 취소 됩니다. 약 ' " + 추가시간 + " ' 초뒤 미수정리를 시작 합니다.");
                        Form1.동작_Log(" ");

                        Form1.알림창("[ 미수금정리 ]\n\n" + Form1.form1.Combo_misu.Text + "기준 미수금을 정리 합니다.\n\n미체결 주문이 일괄 취소 됩니다.\n\n약 ' " + 추가시간 + " ' 초뒤 미수정리를 시작 합니다.", 5, false);

                        Form1.form1.미체결일괄취소();

                        var MISU_Action = new Task(
                        () =>
                        {
                            for (int i = 추가시간; i > 0; i--)
                            {
                                if (i < 6)
                                {
                                    Form1.form1.미체결일괄취소();
                                }
                                Form1.Delay(1000);
                            }
                        });
                        MISU_Action.Start();
                    }
                }

                if (Form1.timenow > int.Parse(시간))
                {
                    if (Form1.JumunItem_List.Count <= 10)
                    {
                        Form1.추정예수금 = Form1.Acc[0].D2;
                        Form1.Acc[0].추정D2 = Form1.Acc[0].D2;

                        foreach (var jumun in Form1.JumunItem_List)
                        {
                            int 주문가격 = jumun.주문가격;
                            if (jumun.주문구분 == 0) 주문가격 = jumun.현재가;
                            Form1.추정예수금 = Form1.추정예수금 + (주문가격 * jumun.주문수량);
                        }

                        if (Form1.JumunItem_List.Count == 0)
                        {
                            foreach (var code in Form1.stockBalanceList.ToList())
                            {
                                Stockbalance 잔고 = code.Value;
                                if (잔고.주문가능수량 != 잔고.보유수량)
                                {
                                    Form1.동작_Log(" ");
                                    Form1.동작_Log("[주문가능 수량보정] 종목명: " + 잔고.종목명 + " 주문가능수량이 보유수량으로 정정됩니다. 보유수량: " + 잔고.보유수량 + " 주문가능수량: " + 잔고.주문가능수량);
                                    Form1.동작_Log(" ");

                                    // Form1.알림창("[ 주문가능 수량보정 ]\n\n종목명: " + 잔고.종목명 + "\n\n주문가능수량이 보유수량으로 정정됩니다.\n\n보유수량: " + 잔고.보유수량 + " 주문가능수량: " + 잔고.주문가능수량, 5, false);

                                    잔고.주문가능수량 = 잔고.보유수량;
                                }
                            }
                        }
                    }

                    if (Form1.Acc[0].추정D2 < 0 && !Form1.form1.미수정리_미수알림 && Form1.추정예수금 < 0)
                    {
                        double 비중 = Properties.Settings.Default.TB_misu_ratio;
                        double Value = Properties.Settings.Default.TB_misu_value;
                        int jumnun_구분 = Properties.Settings.Default.Combo_misu_jumnun;
                        int 취소시간 = Properties.Settings.Default.TB_misu_repeat_time;

                        Stockbalance 기준잔고1 = null;
                        Stockbalance 기준잔고2 = null;
                        Stockbalance 기준잔고3 = null;

                        if (Properties.Settings.Default.CB_ETF매입비제외)
                        {
                            Dictionary<string, Stockbalance> NewstockBalanceList = Form1.stockBalanceList.Where(item => item.Value.시장.Equals("E") && item.Value.매입금액 > 0).ToDictionary(item => item.Key, item => item.Value);
                            if (NewstockBalanceList.Count > 0)
                            {
                                jumnun_구분 = 0;
                                foreach (var code in NewstockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;
                                    if (Misu_liquidation.미수정리(잔고))
                                    {
                                        if (Form1.추정예수금 >= 0) break;
                                        주문(잔고);
                                    }
                                }
                            }
                            else
                            {
                                jumnun_구분 = Properties.Settings.Default.Combo_misu_jumnun;
                                Run();
                            }
                        }
                        else
                        {
                            Run();
                        }

                        void Run()
                        {
                            if (Properties.Settings.Default.Combo_misu.Equals(1))
                            {
                                Dictionary<string, Stockbalance> NewstockBalanceList = Form1.stockBalanceList.Where(item => item.Value.시장.Equals("E")).ToDictionary(item => item.Key, item => item.Value);
                                if (NewstockBalanceList.Count > 0)
                                {
                                    jumnun_구분 = 0;
                                    foreach (var code in NewstockBalanceList.ToList())
                                    {
                                        Stockbalance 잔고 = code.Value;
                                        if (Misu_liquidation.미수정리(잔고))
                                        {
                                            if (Form1.추정예수금 >= 0) break;
                                            주문(잔고);
                                        }
                                    }
                                }
                            }
                            else if (Properties.Settings.Default.Combo_misu.Equals(2))  //1:: 1.수익중 2.균등
                            {
                                long 계산금 = Form1.추정예수금;

                                foreach (var code in Form1.stockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;
                                    if (미수정리(잔고))
                                    {
                                        if (잔고.수익률 > 0)
                                        {
                                            int 주문가격 = Method.order_price(Value, jumnun_구분, 잔고.종목코드, 잔고.현재가); ;
                                            if (jumnun_구분.Equals(0)) 주문가격 = 잔고.현재가;

                                            계산금 = 계산금 + (주문가격 * 잔고.주문가능수량);
                                        }
                                    }
                                }

                                var NewstockBalanceList = from pair in Form1.stockBalanceList.ToList() orderby pair.Value.수익률 descending select pair;

                                foreach (var code in NewstockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;

                                    if (미수정리(잔고))
                                    {
                                        if (Form1.추정예수금 >= 0) break;

                                        if (잔고.수익률 > 0)
                                        {
                                            주문(잔고);
                                        }
                                        else
                                        {
                                            if (계산금 < 0)
                                            {
                                                if (계산금 + (_주문수량(잔고) * _주문가격(잔고)) > 0)
                                                {
                                                    JumunItem item = Form1.JumunItem_List.Find(o => o.종목코드.Equals(잔고.종목코드));
                                                    if (item == null) 주문(잔고);
                                                }
                                                else
                                                {
                                                    주문(잔고);
                                                }

                                                계산금 = 계산금 + (_주문가격(잔고) * _주문수량(잔고));
                                            }
                                        }
                                    }
                                }
                            }
                            else if (Properties.Settings.Default.Combo_misu.Equals(3))    //2:: 1.수익중 2.수익률
                            {
                                foreach (var code in Form1.stockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;
                                    if (미수정리(잔고))
                                    {
                                        if (기준잔고1 == null)
                                        {
                                            기준잔고1 = 잔고;
                                        }

                                        if (기준잔고1.수익률 < 잔고.수익률)
                                        {
                                            기준잔고1 = 잔고;
                                        }
                                    }
                                }

                                foreach (var code in Form1.stockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;
                                    if (미수정리(잔고))
                                    {
                                        if (기준잔고2 == null)
                                        {
                                            기준잔고2 = 잔고;
                                        }

                                        if (기준잔고2.수익률 < 잔고.수익률)
                                        {
                                            if (잔고.수익률 < 기준잔고1.수익률)
                                            {
                                                기준잔고2 = 잔고;
                                            }
                                        }
                                    }
                                }

                                foreach (var code in Form1.stockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;
                                    if (미수정리(잔고))
                                    {
                                        if (기준잔고3 == null)
                                        {
                                            기준잔고3 = 잔고;
                                        }

                                        if (기준잔고3.수익률 < 잔고.수익률)
                                        {
                                            if (잔고.수익률 < 기준잔고2.수익률)
                                            {
                                                if (잔고.수익률 < 기준잔고1.수익률)
                                                {
                                                    기준잔고3 = 잔고;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (Form1.추정예수금 < 0)
                                {
                                    주문(기준잔고1);
                                }
                                if (Form1.추정예수금 < 0)
                                {
                                    주문(기준잔고2);
                                }
                                if (Form1.추정예수금 < 0)
                                {
                                    주문(기준잔고3);
                                }
                            }
                            else if (Properties.Settings.Default.Combo_misu.Equals(4))   //3:: 1.손실중 2.손실률 
                            {
                                foreach (var code in Form1.stockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;
                                    if (미수정리(잔고))
                                    {
                                        if (기준잔고1 == null)
                                        {
                                            기준잔고1 = 잔고;
                                        }

                                        if (기준잔고1.수익률 > 잔고.수익률)
                                        {
                                            기준잔고1 = 잔고;
                                        }
                                    }
                                }

                                foreach (var code in Form1.stockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;
                                    if (미수정리(잔고))
                                    {
                                        if (기준잔고2 == null)
                                        {
                                            기준잔고2 = 잔고;
                                        }

                                        if (기준잔고2.수익률 > 잔고.수익률)
                                        {
                                            if (잔고.수익률 > 기준잔고1.수익률)
                                            {
                                                기준잔고2 = 잔고;
                                            }
                                        }
                                    }
                                }

                                foreach (var code in Form1.stockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;
                                    if (미수정리(잔고))
                                    {
                                        if (기준잔고3 == null)
                                        {
                                            기준잔고3 = 잔고;
                                        }

                                        if (기준잔고3.수익률 > 잔고.수익률)
                                        {
                                            if (잔고.수익률 > 기준잔고1.수익률)
                                            {
                                                if (잔고.수익률 > 기준잔고2.수익률)
                                                {
                                                    기준잔고3 = 잔고;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (Form1.추정예수금 < 0)
                                {
                                    주문(기준잔고1);
                                }
                                if (Form1.추정예수금 < 0)
                                {
                                    주문(기준잔고2);
                                }
                                if (Form1.추정예수금 < 0)
                                {
                                    주문(기준잔고3);
                                }
                            }
                            else if (Properties.Settings.Default.Combo_misu.Equals(5))  //4:: - 균등
                            {
                                foreach (var code in Form1.stockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;
                                    if (미수정리(잔고))
                                    {
                                        if (Form1.추정예수금 + (_주문수량(잔고) * _주문가격(잔고)) > 0)
                                        {
                                            JumunItem item = Form1.JumunItem_List.Find(o => o.종목코드.Equals(잔고.종목코드));
                                            if (item == null) 주문(잔고);
                                        }
                                        else
                                        {
                                            주문(잔고);
                                        }
                                    }
                                    if (Form1.추정예수금 >= 0) break;
                                }
                            }
                        }

                        int _주문가격(Stockbalance 잔고)
                        {
                            int result = Method.order_price(Value, jumnun_구분, 잔고.종목코드, 잔고.현재가);

                            if (jumnun_구분.Equals(0)) result = 잔고.현재가;

                            return result;
                        }

                        int _주문수량(Stockbalance 잔고)
                        {
                            int result = 0;

                            int 주문가격 = _주문가격(잔고);
                            int 기록_주문가격 = 주문가격;
                            if (jumnun_구분.Equals(0)) 기록_주문가격 = 잔고.현재가;

                            result = (int)Math.Truncate((double)비중 * (double)10000 / (double)기록_주문가격);

                            if (-Form1.추정예수금 < 비중 * 10000)
                            {
                                result = (int)Math.Ceiling((double)-Form1.추정예수금 / (double)기록_주문가격);
                            }

                            if (잔고.주문가능수량 < result) result = 잔고.주문가능수량;

                            return result;
                        }

                        void 주문(Stockbalance 잔고)
                        {
                            if (잔고 != null)
                            {
                                if (Form1.timenow >= (Form1.시장종료 - 1000))
                                {
                                    취소시간 = 660;
                                    jumnun_구분 = 0;
                                }

                                int 반복횟수 = 0;

                                string HogaGB = "00"; // 지정가
                                int 주문가격 = _주문가격(잔고);
                                int 기록_주문가격 = 주문가격;

                                if (jumnun_구분.Equals(0))
                                {
                                    HogaGB = "03"; // 시장가
                                    주문가격 = 0;
                                    기록_주문가격 = 잔고.현재가;
                                }

                                if (Form1.재시작)
                                    if (Method.매매확인_VI_모투가능확인(Form1.Market_Item_List[잔고.종목코드], 2))
                                    {
                                        int 주문수량 = _주문수량(잔고);

                                        if (주문수량 > 0)
                                        {
                                            if (Form1.추정예수금 < 0)
                                            {
                                                int ScreenNumber = GET.jumunScreen(잔고.종목코드);
                                                if (ScreenNumber == 1300)
                                                {
                                                    Method.주문초과알림(잔고.종목명);
                                                }
                                                else
                                                {
                                                    DataManagement.주문가능수업데이트(잔고, "매도", 주문수량, "매도주문");
                                                    Form1.추정예수금 = Form1.추정예수금 + (주문수량 * 기록_주문가격);

                                                    int Order번호 = GET.Order번호();
                                                    string 검색식 = "미수금정리";


                                                    JumunItem ItemList = new JumunItem(0, 0, ScreenNumber.ToString(), 잔고.종목코드, 잔고.종목명, "++", "---", 검색식, Value, jumnun_구분, 취소시간, 3, 반복횟수, "", 검색식, 주문수량, 기록_주문가격, 2, 비중, 0, 취소시간, 잔고.현재가, 잔고.등락율,
                                                                                       Form1.timenow, 주문수량, true, false, 0, Method.Find_Tik_Cap(잔고.현재가, 주문가격, 잔고.시장),
                                                                                       잔고.현재가, 잔고.수익률, false, 0, Order번호, 0, Form1.NXT_server); // 자동 매도 일때  주문추가 
                                                    Form1.JumunItem_List.Add(ItemList);

                                                    Form1.que_order(ScreenNumber.ToString(), 잔고.종목명, 2, 잔고.종목코드, 주문수량, 주문가격, HogaGB, "++", 검색식, Order번호);
                                                }
                                            }
                                        }
                                    }
                            }
                        }
                    }
                }
            }
        }

        public static bool 미수정리(Stockbalance 잔고)
        {
            bool result = true;

            if (잔고.종목상태.Contains("거래정지")) result = false;
            if (result && 잔고.종목상태.Contains("동시호가")) result = false;
            if (result && 잔고.종목상태.Contains("과열(VI)")) result = false;
            if (result && 잔고.종목상태.Contains("하한가")) result = false;
            if (result && !잔고.매매가능) result = false;
            if (result && 잔고.주문가능수량 <= 0) result = false;
            if (result && !GET.익절그룹("미수금정리").Contains(GET.그룹변환(잔고.매매그룹))) result = false;

            return result;
        }

        /////////////////////             미수정리 매매            ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////





    }
}
