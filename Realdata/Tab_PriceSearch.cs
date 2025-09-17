using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace 지니_64
{
    class Tab_PriceSearch
    {
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////                 시장가대금 탐색                    /////////////////

        public static void Stock_search_거래대금(string itemcode, string search_현재가, string search_체결시간, string 거래량_구분, string search_누적거래대금)
        {
            var Task = new Task(() =>
            {
                if (Form1.Market_Item_List.ContainsKey(itemcode))
                {
                    Market_Item Market = Form1.Market_Item_List[itemcode];

                    string 거래구분 = "매수";
                    if (거래량_구분.Contains("-")) 거래구분 = "매도";
                    string search_거래량 = 거래량_구분.Substring(1);

                    int.TryParse(search_현재가, out int _현재가);
                    int 현재가 = Math.Abs(_현재가);
                    long.TryParse(search_거래량, out long 거래량);
                    int.TryParse(search_체결시간, out int 체결시간);
                    long.TryParse(search_누적거래대금, out long 누적거래대금);
                    int.TryParse(search_체결시간.Substring(search_체결시간.Length - 4, 2), out int 분);

                    if (Market.minute != 분)
                    {
                        Market.minute = 분;
                        Market.S_minute = 현재가;
                    }

                    if (Market.Buy_현재가_A == 0) Market.Buy_현재가_A = 현재가;
                    if (Market.Buy_현재가_B == 0) Market.Buy_현재가_B = 현재가;
                    if (Market.Sell_현재가 == 0) Market.Sell_현재가 = 현재가;

                    bool 봉확인(int 분봉, int 일봉)
                    {
                        bool result = 일봉확인();

                        if (분봉 == 1) // 1분봉양봉 일때 확인
                        {
                            if (현재가 > Market.S_minute)
                                result = 일봉확인();
                        }
                        else if (분봉 == 2) // 1분봉음봉 일때 확인
                        {
                            if (현재가 < Market.S_minute)
                                result = 일봉확인();
                        }

                        bool 일봉확인()
                        {
                            bool 확인 = true;

                            if (일봉 == 1) // 시가양
                            {
                                if (현재가 < Market.start_price) 확인 = false;
                            }
                            else if (일봉 == 2) // 시가음
                            {
                                if (현재가 > Market.start_price) 확인 = false;
                            }
                            else if (일봉 == 3) // 종가양
                            {
                                if (현재가 < Market.Last_price) 확인 = false;
                            }
                            else if (일봉 == 4) // 종가음
                            {
                                if (현재가 > Market.Last_price) 확인 = false;
                            }

                            return 확인;
                        }

                        return result;
                    }

                    if (Properties.Settings.Default.TB_accumulate_Price <= 누적거래대금)
                    {
                        if (Properties.Settings.Default.CB_매수탐색A)
                        {
                            if (봉확인(Properties.Settings.Default.CBB_Buy_A_분봉, Properties.Settings.Default.CBB_Buy_A_일봉))
                            {
                                if (Market.수시간_A + Properties.Settings.Default.TB_Buy_A_기준초 < 체결시간)
                                {
                                    Market.수대금_A = 0;
                                    Market.수시간_A = 체결시간;
                                    Market.Buy_start_A = 현재가;

                                    Market.Buy_상승카운터_A = 0;
                                    Market.Buy_하락카운터_A = 0;
                                }

                                if (거래구분.Equals("매수"))
                                {
                                    Market.수대금_A = Market.수대금_A + (현재가 * 거래량);
                                    Market.Buy_End_A = 현재가;

                                    // 상승 카운터 
                                    if (Market.Buy_현재가_A < 현재가)
                                    {
                                        Market.Buy_상승카운터_A++;
                                        if (Market.Buy_하락카운터_A > 0) Market.Buy_하락카운터_A--;
                                    }
                                    else if (Market.Buy_현재가_A > 현재가)
                                    {
                                        if (Market.Buy_상승카운터_A > 0) Market.Buy_상승카운터_A--;
                                        Market.Buy_하락카운터_A++;
                                    }

                                    Market.Buy_현재가_A = 현재가;
                                }

                                double 시장가대금 = Properties.Settings.Default.TB_Buy_A_탐색대금_6 * (double)10000;

                                if (현재가 <= Properties.Settings.Default.TB_Buy_A_탐색주가_5) 시장가대금 = Properties.Settings.Default.TB_Buy_A_탐색대금_5 * 10000;
                                if (현재가 <= Properties.Settings.Default.TB_Buy_A_탐색주가_4) 시장가대금 = Properties.Settings.Default.TB_Buy_A_탐색대금_4 * 10000;
                                if (현재가 <= Properties.Settings.Default.TB_Buy_A_탐색주가_3) 시장가대금 = Properties.Settings.Default.TB_Buy_A_탐색대금_3 * 10000;
                                if (현재가 <= Properties.Settings.Default.TB_Buy_A_탐색주가_2) 시장가대금 = Properties.Settings.Default.TB_Buy_A_탐색대금_2 * 10000;
                                if (현재가 <= Properties.Settings.Default.TB_Buy_A_탐색주가_1) 시장가대금 = Properties.Settings.Default.TB_Buy_A_탐색대금_1 * 10000;

                                double MS_rate = 1 + (Properties.Settings.Default.TB_Buy_A_탐색rate / 100);

                                int 상승값 = Properties.Settings.Default.TB_Buy_상승카운터_A;
                                bool 상승옵션 = Properties.Settings.Default.CB_Buy_상승옵션_A;
                                int Market상승값 = Market.Buy_상승카운터_A;
                                int 하락값 = Properties.Settings.Default.TB_Buy_하락카운터_A;
                                bool 하락옵션 = Properties.Settings.Default.CB_Buy_하락옵션_A;
                                int Market하락값 = Market.Buy_하락카운터_A;

                                if (틱계산(상승값, 상승옵션, Market상승값, 하락값, 하락옵션, Market하락값))
                                {
                                    if (Market.수대금_A >= 시장가대금)
                                    {
                                        if ((Double)(Market.Buy_start_A * MS_rate) <= (Double)Market.Buy_End_A)
                                        {
                                            if (Market.매수가능_A && Market.재매수_A == 0)
                                            {
                                                bool New = false;
                                                if (Properties.Settings.Default.CB_new_A && Properties.Settings.Default.combo_new_condition_A.Equals(Properties.Settings.Default.TB_매수탐색A)) New = true;
                                                if (Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_condition_B.Equals(Properties.Settings.Default.TB_매수탐색A)) New = true;
                                                if (Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_condition_C.Equals(Properties.Settings.Default.TB_매수탐색A)) New = true;

                                                검색진입(itemcode, Properties.Settings.Default.MTB_M_반복, Properties.Settings.Default.TB_매수탐색A, New);

                                                Market.재매수_A = Properties.Settings.Default.MTB_M_반복;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Properties.Settings.Default.CB_매수탐색B)
                        {
                            if (봉확인(Properties.Settings.Default.CBB_Buy_B_분봉, Properties.Settings.Default.CBB_Buy_B_일봉))
                            {
                                if (Market.수시간_B + Properties.Settings.Default.TB_Buy_B_기준초 < 체결시간)
                                {
                                    Market.수대금_B = 0;
                                    Market.수시간_B = 체결시간;
                                    Market.Buy_start_B = 현재가;
                                }

                                if (거래구분.Equals("매수"))
                                {
                                    Market.수대금_B = Market.수대금_B + (현재가 * 거래량);
                                    Market.Buy_End_B = 현재가;

                                    // 상승 카운터 
                                    if (Market.Buy_현재가_B < 현재가)
                                    {
                                        Market.Buy_상승카운터_B++;
                                        if (Market.Buy_하락카운터_B > 0) Market.Buy_하락카운터_B--;
                                    }
                                    else if (Market.Buy_현재가_B > 현재가)
                                    {
                                        if (Market.Buy_상승카운터_B > 0) Market.Buy_상승카운터_B--;
                                        Market.Buy_하락카운터_B++;
                                    }

                                    Market.Buy_현재가_B = 현재가;
                                }

                                double 시장가대금 = Properties.Settings.Default.TB_Buy_B_탐색대금_6 * (double)10000;

                                if (현재가 <= Properties.Settings.Default.TB_Buy_B_탐색주가_5) 시장가대금 = Properties.Settings.Default.TB_Buy_B_탐색대금_5 * 10000;
                                if (현재가 <= Properties.Settings.Default.TB_Buy_B_탐색주가_4) 시장가대금 = Properties.Settings.Default.TB_Buy_B_탐색대금_4 * 10000;
                                if (현재가 <= Properties.Settings.Default.TB_Buy_B_탐색주가_3) 시장가대금 = Properties.Settings.Default.TB_Buy_B_탐색대금_3 * 10000;
                                if (현재가 <= Properties.Settings.Default.TB_Buy_B_탐색주가_2) 시장가대금 = Properties.Settings.Default.TB_Buy_B_탐색대금_2 * 10000;
                                if (현재가 <= Properties.Settings.Default.TB_Buy_B_탐색주가_1) 시장가대금 = Properties.Settings.Default.TB_Buy_B_탐색대금_1 * 10000;

                                double MS_rate = 1 + (Properties.Settings.Default.TB_Buy_B_탐색rate / 100);

                                int 상승값 = Properties.Settings.Default.TB_Buy_상승카운터_B;
                                bool 상승옵션 = Properties.Settings.Default.CB_Buy_상승옵션_B;
                                int Market상승값 = Market.Buy_상승카운터_B;
                                int 하락값 = Properties.Settings.Default.TB_Buy_하락카운터_B;
                                bool 하락옵션 = Properties.Settings.Default.CB_Buy_하락옵션_B;
                                int Market하락값 = Market.Buy_하락카운터_B;

                                if (틱계산(상승값, 상승옵션, Market상승값, 하락값, 하락옵션, Market하락값))
                                {
                                    if (Market.수대금_B >= 시장가대금)
                                    {
                                        if ((Double)(Market.Buy_start_B * MS_rate) <= (Double)Market.Buy_End_B)
                                        {
                                            if (Market.매수가능_B && Market.재매수_B == 0)
                                            {
                                                bool New = false;
                                                if (Properties.Settings.Default.CB_new_A && Properties.Settings.Default.combo_new_condition_A.Equals(Properties.Settings.Default.TB_매수탐색B)) New = true;
                                                if (Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_condition_B.Equals(Properties.Settings.Default.TB_매수탐색B)) New = true;
                                                if (Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_condition_C.Equals(Properties.Settings.Default.TB_매수탐색B)) New = true;

                                                검색진입(itemcode, Properties.Settings.Default.MTB_M_반복_2, Properties.Settings.Default.TB_매수탐색B, New);

                                                Market.재매수_B = Properties.Settings.Default.MTB_M_반복_2;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (Properties.Settings.Default.CB_매도탐색)
                    {
                        Dictionary<string, Stockbalance> stockBalanceList = Form1.stockBalanceList;   // 잔고 - 보유잔고리스트
                        if (stockBalanceList.ContainsKey(itemcode))
                        {
                            if (봉확인(Properties.Settings.Default.CBB_Sell_탐색_분봉, Properties.Settings.Default.CBB_Sell_탐색_일봉))
                            {
                                if (Market.도시간 + Properties.Settings.Default.TB_Sell_기준초 < 체결시간)
                                {
                                    Market.도대금 = 0;
                                    Market.도시간 = 체결시간;
                                    Market.Sell_start = 현재가;
                                }

                                if (거래구분.Equals("매도"))
                                {
                                    Market.도대금 = Market.도대금 + (현재가 * 거래량);
                                    Market.Sell_End = 현재가;

                                    // 상승 카운터 
                                    if (Market.Sell_현재가 < 현재가)
                                    {
                                        Market.Sell_상승카운터++;
                                        if (Market.Sell_하락카운터 > 0) Market.Sell_하락카운터--;
                                    }
                                    else if (Market.Sell_현재가 > 현재가)
                                    {
                                        if (Market.Sell_상승카운터 > 0) Market.Sell_상승카운터--;
                                        Market.Sell_하락카운터++;
                                    }

                                    Market.Sell_현재가 = 현재가;
                                }

                                double 시장가대금 = Properties.Settings.Default.TB_Sell_탐색대금_6 * (double)10000;

                                if (현재가 <= Properties.Settings.Default.TB_Sell_탐색주가_6) 시장가대금 = Properties.Settings.Default.TB_Sell_탐색대금_6 * 10000;
                                if (현재가 <= Properties.Settings.Default.TB_Sell_탐색주가_5) 시장가대금 = Properties.Settings.Default.TB_Sell_탐색대금_5 * 10000;
                                if (현재가 <= Properties.Settings.Default.TB_Sell_탐색주가_4) 시장가대금 = Properties.Settings.Default.TB_Sell_탐색대금_4 * 10000;
                                if (현재가 <= Properties.Settings.Default.TB_Sell_탐색주가_3) 시장가대금 = Properties.Settings.Default.TB_Sell_탐색대금_3 * 10000;
                                if (현재가 <= Properties.Settings.Default.TB_Sell_탐색주가_2) 시장가대금 = Properties.Settings.Default.TB_Sell_탐색대금_2 * 10000;
                                if (현재가 <= Properties.Settings.Default.TB_Sell_탐색주가_1) 시장가대금 = Properties.Settings.Default.TB_Sell_탐색대금_1 * 10000;

                                double MS_rate = 1 + (Properties.Settings.Default.TB_Sell_탐색rate / 100);

                                int 상승값 = Properties.Settings.Default.TB_Sell_상승카운터;
                                bool 상승옵션 = Properties.Settings.Default.CB_Sell_상승옵션;
                                int Market상승값 = Market.Sell_상승카운터;
                                int 하락값 = Properties.Settings.Default.TB_Sell_하락카운터;
                                bool 하락옵션 = Properties.Settings.Default.CB_Sell_하락옵션;
                                int Market하락값 = Market.Sell_하락카운터;

                                if (틱계산(상승값, 상승옵션, Market상승값, 하락값, 하락옵션, Market하락값))
                                {
                                    if (Market.도대금 >= 시장가대금)
                                    {
                                        if ((Double)(Market.Sell_start * MS_rate) >= (Double)Market.Sell_End)
                                        {
                                            검색진입(itemcode, 5, Properties.Settings.Default.TB_매도탐색, false);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    void 검색진입(string itemCode, int 타이머, string 검색식, bool 신규)
                    {
                        검색이탈 탐색 = Form1.검색이탈_LIST.Find(o => o.코드_검색식.Equals(itemCode + "^" + 검색식));

                        if (탐색 == null)
                        {
                            검색이탈 ADD = new 검색이탈(타이머, itemCode + "^" + 검색식, 신규);
                            Form1.검색이탈_LIST.Add(ADD);
                        }
                        else
                        {
                            탐색.타이머 = 타이머;
                        }

                        string time = DateTime.Now.ToString("HH:mm:ss");

                        if (신규) Tab_Basic.New_Buy("I", itemCode, 검색식);
                        Tab_Watch.Watch_In_Out("I", itemCode, 검색식, time);
                        Tab_Repeat.Repeat_condition("I", itemCode, 검색식);
                        Tab_AccountManagement.Rebalancing_condition("I", itemCode, 검색식);
                        Tab_AccountManagement.Liquidation_condition("I", itemCode, 검색식);

                        Form1.form1.SearchView_add("I", itemCode, 검색식, time);
                    }

                    bool 틱계산(int 상승값, bool 상승옵션, int Market상승값, int 하락값, bool 하락옵션, int Market하락값)
                    {
                        bool result = false;

                        if (상승옵션) //이상
                        {
                            if (상승값 <= Market상승값) result = true;
                        }
                        else //이하
                        {
                            if (상승값 >= Market상승값) result = true;
                        }

                        if (result)
                        {
                            if (하락옵션) //이상
                            {
                                if (하락값 <= Market하락값) result = true;
                            }
                            else //이하
                            {
                                if (하락값 >= Market하락값) result = true;
                            }
                        }

                        return result;
                    }
                }
            });
            Form1.Real_price_Manager.RequestTrData(Task);
        }


        public static void Stock_search_호가별대금(string Itemcode, string 매도호가2, string 매도호가3, string 매도호가4, string 매도호가5, string 매도호가6, string 매도호가7, string 매도호가8, string 매도호가9, string 매도호가10,
                                                     string 매수호가2, string 매수호가3, string 매수호가4, string 매수호가5, string 매수호가6, string 매수호가7, string 매수호가8, string 매수호가9, string 매수호가10,
                                                       string 매도호가수량2, string 매도호가수량3, string 매도호가수량4, string 매도호가수량5, string 매도호가수량6, string 매도호가수량7, string 매도호가수량8, string 매도호가수량9, string 매도호가수량10,
                                                     string 매수호가수량2, string 매수호가수량3, string 매수호가수량4, string 매수호가수량5, string 매수호가수량6, string 매수호가수량7, string 매수호가수량8, string 매수호가수량9, string 매수호가수량10)
        {
            var Task = new Task(() =>
            {
                if (Form1.Market_Item_List.ContainsKey(Itemcode))
                {
                    Market_Item Market = Form1.Market_Item_List[Itemcode];

                    int.TryParse(매도호가2, out int 매도2);
                    int.TryParse(매도호가3, out int 매도3);
                    int.TryParse(매도호가4, out int 매도4);
                    int.TryParse(매도호가5, out int 매도5);
                    int.TryParse(매도호가6, out int 매도6);
                    int.TryParse(매도호가7, out int 매도7);
                    int.TryParse(매도호가8, out int 매도8);
                    int.TryParse(매도호가9, out int 매도9);
                    int.TryParse(매도호가10, out int 매도10);

                    int.TryParse(매도호가수량2, out int 도수량2);
                    int.TryParse(매도호가수량3, out int 도수량3);
                    int.TryParse(매도호가수량4, out int 도수량4);
                    int.TryParse(매도호가수량5, out int 도수량5);
                    int.TryParse(매도호가수량6, out int 도수량6);
                    int.TryParse(매도호가수량7, out int 도수량7);
                    int.TryParse(매도호가수량8, out int 도수량8);
                    int.TryParse(매도호가수량9, out int 도수량9);
                    int.TryParse(매도호가수량10, out int 도수량10);

                    int.TryParse(매수호가2, out int 매수2);
                    int.TryParse(매수호가3, out int 매수3);
                    int.TryParse(매수호가4, out int 매수4);
                    int.TryParse(매수호가5, out int 매수5);
                    int.TryParse(매수호가6, out int 매수6);
                    int.TryParse(매수호가7, out int 매수7);
                    int.TryParse(매수호가8, out int 매수8);
                    int.TryParse(매수호가9, out int 매수9);
                    int.TryParse(매수호가10, out int 매수10);

                    int.TryParse(매수호가수량2, out int 수수량2);
                    int.TryParse(매수호가수량3, out int 수수량3);
                    int.TryParse(매수호가수량4, out int 수수량4);
                    int.TryParse(매수호가수량5, out int 수수량5);
                    int.TryParse(매수호가수량6, out int 수수량6);
                    int.TryParse(매수호가수량7, out int 수수량7);
                    int.TryParse(매수호가수량8, out int 수수량8);
                    int.TryParse(매수호가수량9, out int 수수량9);
                    int.TryParse(매수호가수량10, out int 수수량10);

                    long 도2 = Math.Abs(매도2 * 도수량2);
                    long 도3 = Math.Abs(매도3 * 도수량3);
                    long 도4 = Math.Abs(매도4 * 도수량4);
                    long 도5 = Math.Abs(매도5 * 도수량5);
                    long 도6 = Math.Abs(매도6 * 도수량6);
                    long 도7 = Math.Abs(매도7 * 도수량7);
                    long 도8 = Math.Abs(매도8 * 도수량8);
                    long 도9 = Math.Abs(매도9 * 도수량9);
                    long 도10 = Math.Abs(매도10 * 도수량10);

                    long 수2 = Math.Abs(매수2 * 수수량2);
                    long 수3 = Math.Abs(매수3 * 수수량3);
                    long 수4 = Math.Abs(매수4 * 수수량4);
                    long 수5 = Math.Abs(매수5 * 수수량5);
                    long 수6 = Math.Abs(매수6 * 수수량6);
                    long 수7 = Math.Abs(매수7 * 수수량7);
                    long 수8 = Math.Abs(매수8 * 수수량8);
                    long 수9 = Math.Abs(매수9 * 수수량9);
                    long 수10 = Math.Abs(매수10 * 수수량10);

                    long 매도 = 도2 + 도3 + 도4 + 도5 + 도6 + 도7 + 도8 + 도9 + 도10;
                    long 매수 = 수2 + 수3 + 수4 + 수5 + 수6 + 수7 + 수8 + 수9 + 수10;

                    if (Properties.Settings.Default.CB_매수탐색A)
                    {
                        Market.매수가능_A = true;

                        long 매도호가별대금 = (long)(Properties.Settings.Default.TB_M_매도호가별대금 * 1000000);
                        long 매도호가합대금 = (long)(Properties.Settings.Default.TB_M_매도호가합대금 * 1000000);

                        long 매수호가별대금 = (long)(Properties.Settings.Default.TB_M_매수호가별대금 * 1000000);
                        long 매수호가합대금 = (long)(Properties.Settings.Default.TB_M_매수호가합대금 * 1000000);

                        if (매도 < 매도호가합대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 매수 < 매수호가합대금) Market.매수가능_A = false;

                        if (Market.매수가능_A && 도2 < 매도호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 도3 < 매도호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 도4 < 매도호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 도5 < 매도호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 도6 < 매도호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 도7 < 매도호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 도8 < 매도호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 도9 < 매도호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 도10 < 매도호가별대금) Market.매수가능_A = false;

                        if (Market.매수가능_A && 수2 < 매수호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 수3 < 매수호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 수4 < 매수호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 수5 < 매수호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 수6 < 매수호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 수7 < 매수호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 수8 < 매수호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 수9 < 매수호가별대금) Market.매수가능_A = false;
                        if (Market.매수가능_A && 수10 < 매수호가별대금) Market.매수가능_A = false;

                        if (Market.매수가능_B && Properties.Settings.Default.CBB_M_잔량 == 0)
                        {
                            if (매도 < 매수)
                            {
                                Market.매수가능_A = false;
                            }
                        }

                        if (Market.매수가능_B && Properties.Settings.Default.CBB_M_잔량 == 1)
                        {
                            if (매도 > 매수)
                            {
                                Market.매수가능_A = false;
                            }
                        }
                    }

                    if (Properties.Settings.Default.CB_매수탐색B)
                    {
                        Market.매수가능_B = true;

                        long 매도호가별대금 = (long)(Properties.Settings.Default.TB_M_매도호가별대금_2 * 1000000);
                        long 매도호가합대금 = (long)(Properties.Settings.Default.TB_M_매도호가합대금_2 * 1000000);

                        long 매수호가별대금 = (long)(Properties.Settings.Default.TB_M_매수호가별대금_2 * 1000000);
                        long 매수호가합대금 = (long)(Properties.Settings.Default.TB_M_매수호가합대금_2 * 1000000);

                        if (매도 < 매도호가합대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 매수 < 매수호가합대금) Market.매수가능_B = false;

                        if (Market.매수가능_B && 도2 < 매도호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 도3 < 매도호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 도4 < 매도호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 도5 < 매도호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 도6 < 매도호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 도7 < 매도호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 도8 < 매도호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 도9 < 매도호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 도10 < 매도호가별대금) Market.매수가능_B = false;

                        if (Market.매수가능_B && 수2 < 매수호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 수3 < 매수호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 수4 < 매수호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 수5 < 매수호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 수6 < 매수호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 수7 < 매수호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 수8 < 매수호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 수9 < 매수호가별대금) Market.매수가능_B = false;
                        if (Market.매수가능_B && 수10 < 매수호가별대금) Market.매수가능_B = false;

                        if (Market.매수가능_B && Properties.Settings.Default.CBB_M_잔량_2 == 0)
                        {
                            if (매도 < 매수)
                            {
                                Market.매수가능_B = false;
                            }
                        }

                        if (Market.매수가능_B && Properties.Settings.Default.CBB_M_잔량_2 == 1)
                        {
                            if (매도 > 매수)
                            {
                                Market.매수가능_B = false;
                            }
                        }
                    }
                }
            });
            Form1.Real_Hoga_Manager.RequestTrData(Task);
        }
    }
}
