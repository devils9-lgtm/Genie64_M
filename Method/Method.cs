using System;
using System.Collections.Generic;
using System.Linq;

namespace 지니_64
{
    public static class Method
    {

        public static string 매입비매수제한(int index)
        {
            string str = "";

            Form1.Acc[0].매입비 = Acc매입비();

            if (Form1.Acc[0].매입비 > Properties.Settings.Default.TB_계좌매입비_제한비중)
            {
                switch (index)
                {
                    case 0:
                        str = "신규금지";
                        break;
                    case 1:
                        str = "추매금지";
                        break;
                    case 2:
                        str = "신규&추매금지";
                        break;
                }
            }

            return str;
        }

        public static double Acc매입비()
        {
            long 매수진행금 = 0;
            List<JumunItem> 매수_List = Form1.JumunItem_List.FindAll(o => o.주문유형.Equals(1));
            if (매수_List.Count > 0)
            {
                for (int i = 0; i < 매수_List.Count; i++)
                {
                    매수진행금 = 매수진행금 + (매수_List[i].주문가격 * 매수_List[i].주문수량);
                }
            }

            long 투자원금 = Properties.Settings.Default.MT_principal;
            if (투자원금 < Form1.Acc[0].추정자산)
            {
                투자원금 = Form1.Acc[0].추정자산;
            }

            long 매입금 = 0;

            Dictionary<string, Stockbalance> stockBalanceList = Form1.stockBalanceList;   // 잔고 - 보유잔고리스트
            foreach (var code in stockBalanceList.Keys)
            {
                if (Properties.Settings.Default.CB_ETF매입비제외)
                {
                    if (!stockBalanceList[code].시장.Equals("E"))
                    {
                        매입금 = 매입금 + stockBalanceList[code].매입금액;
                    }
                }
                else
                {
                    매입금 = 매입금 + stockBalanceList[code].매입금액;
                }
            }

            return (double)(매입금 + 매수진행금) / 투자원금 * (double)100;
        }


        public static void 실시간시세등록(string Code)
        {
            if (!Form1.form1.실시간시세_List.Contains(Code))
            {
                string fid = "10;12;13;14;15;16;17;18;20;29;30;31;311;42;43;44;45;46;47;48;49;50;52;53;54;55;56;57;58;59;60;62;63;64;65;66;67;68;69;70;72;73;74;75;76;77;78;79;80";
                Form1.form1.axKHOpenAPI1.SetRealReg(GET.ScreenNum(), Code + "_AL", fid, "1");

                Form1.form1.실시간시세_List.Add(Code);
            }
        }

        public static bool 주문_매매계산(Stockbalance 잔고, bool 단위_기준금, double 매매손익, int 매매단위, string 유형)
        {
            bool 실행 = false;
            long 매수기준금 = Properties.Settings.Default.MT_buying_standard;

            double 손익 = 매매손익 * 10000;
            if (단위_기준금)
                손익 = 매매손익 / 100 * 매수기준금;

            if (유형.Equals("익절"))
            {
                if (잔고.수익률 > 0 && 잔고.예상손익 > 0)
                {
                    if (매매단위 == 0) // % 수익률 
                    {
                        if (매매손익 <= 잔고.수익률)
                        {
                            실행 = true;
                        }
                    }
                    else if (매매단위 == 1) // 평가손익금
                    {
                        if (손익 <= 잔고.평가손익)
                        {
                            실행 = true;
                        }
                    }
                    else if (매매단위 == 2) // 예상손익금
                    {
                        if (손익 <= 잔고.예상손익)
                        {
                            실행 = true;
                        }
                    }
                    else if (매매단위 == 3) // 기준수익 이상
                    {
                        if (손익 <= 잔고.기준수익률)
                        {
                            실행 = true;
                        }
                    }
                }
            }
            else
            {
                if (매매단위 == 0) // % 수익률 
                {
                    if (매매손익 >= 잔고.수익률)
                    {
                        실행 = true;
                    }
                }
                else if (매매단위 == 1) // 평가손익금
                {
                    if (손익 >= 잔고.평가손익)
                    {
                        실행 = true;
                    }
                }
                else if (매매단위 == 2) // 예상손익금
                {
                    if (손익 >= 잔고.예상손익)
                    {
                        실행 = true;
                    }
                }
                else if (매매단위 == 3) // 기준수익 이하
                {
                    if (손익 >= 잔고.기준수익률)
                    {
                        실행 = true;
                    }
                }
            }

            return 실행;
        }
        // 주문수량 계산 다시 해야함
        public static int 주문수량계산(Stockbalance 잔고, int 주문가, double 비중, int index, int 주문유형)
        {
            double 수량 = 0;
            long 기준금 = Properties.Settings.Default.MT_buying_standard;

            //   if (주문유형 == 2 && 잔고.주문가능수량 > 0) 주문가 = 잔고.현재가;

            if (index.Equals(0)) // 주
            {
                수량 = 비중;
            }
            else if (index.Equals(1))  //만원
            {
                수량 = Math.Truncate(비중 * 10000 / (double)주문가); // 버림
            }
            else if (index.Equals(2))  //% 기준
            {
                수량 = Math.Truncate(기준금 * 비중 / 100 / (double)주문가); // 버림
            }
            else if (index.Equals(3))  //% 보유수
            {
                수량 = Math.Truncate(잔고.매도기준 * 비중 / 100); // 올림
            }
            else if (index.Equals(4))  // 만원 / 평단가
            {
                수량 = Math.Truncate(비중 * 10000 / (double)잔고.평균단가); // 버림
            }
            else if (index.Equals(5))  //% 기준 / 평단가
            {
                수량 = Math.Truncate(기준금 * 비중 / 100 / (double)잔고.평균단가); // 버림
            }
            else if (index.Equals(6))  // 만원 / 기준가
            {
                수량 = Math.Truncate(비중 * 10000 / (double)잔고.기준가격); // 버림
            }
            else if (index.Equals(7))  //% 기준 / 기준가
            {
                수량 = Math.Truncate(기준금 * 비중 / 100 / (double)잔고.기준가격); // 버림
            }

            //    Console.WriteLine("주문수량 계산::index :" + index + " 비중:" + 비중 + " 주문가: " + 주문가 + " 주문수량 :"+ 수량+ " 잔고:"+잔고.종목명);

            return (int)수량;
        }

        public static int 최대매수금_주문수량계산(Stockbalance 잔고, int 주문수량)
        {
            if (Properties.Settings.Default.CB_총매수금)
            {
                long 총매수금 = (long)(Properties.Settings.Default.MT_buying_standard * Properties.Settings.Default.TB_총매수금 / 100);

                if (총매수금 < (잔고.현재가 * 주문수량) + 잔고.매입금액)
                {
                    for (int i = 주문수량; i > -1; i--)
                    {
                        if (총매수금 > (잔고.현재가 * i) + 잔고.매입금액)
                        {
                            주문수량 = i;
                            break;
                        }
                    }
                }
            }

            return 주문수량;
        }

        public static void 매입금매매제한(Stockbalance 잔고)
        {
            if (Properties.Settings.Default.CB_총매수금)
            {
                long 총매수금 = (long)(Properties.Settings.Default.MT_buying_standard * Properties.Settings.Default.TB_총매수금 / 100);

                if (총매수금 < (잔고.현재가 + 잔고.매입금액))
                {
                    잔고.매수제한 = false;
                }
                else
                {
                    if (Properties.Settings.Default.CB_회수제한)
                    {
                        if (Properties.Settings.Default.TB_회수제한 < 잔고.매수횟수)
                            잔고.매수제한 = false;
                        else
                            잔고.매수제한 = true;
                    }
                    else
                    {
                        잔고.매수제한 = true;
                    }
                }
            }
            else
            {
                if (Properties.Settings.Default.CB_회수제한)
                {
                    if (Properties.Settings.Default.TB_회수제한 < 잔고.매수횟수)
                        잔고.매수제한 = false;
                    else
                        잔고.매수제한 = true;
                }
                else
                {
                    잔고.매수제한 = true;
                }
            }

            if (잔고.매수제한 && Properties.Settings.Default.CB_일매수제한금)
            {
                long 제한금 = (long)(Properties.Settings.Default.MT_buying_standard * Properties.Settings.Default.TB_일매수제한금 / 100);

                if (제한금 < (잔고.금일매수금 - 잔고.금일매도금))
                {
                    잔고.매수제한 = false;
                }
                else
                {
                    if (Properties.Settings.Default.CB_회수제한)
                    {
                        if (Properties.Settings.Default.TB_회수제한 > 잔고.매수횟수)
                            잔고.매수제한 = true;
                    }
                    else
                    {
                        잔고.매수제한 = true;
                    }
                }
            }
        }

        public static int order_price(double 주문가_value, int 거래구분, string ItemCode, int 현재가)
        {
            현재가 = 호가맞추기(현재가, Form1.Market_Item_List[ItemCode].Market);

            int after_order = 0;
            string 상하 = "상한가";
            if (주문가_value < 0) 상하 = "하한가";

            if (거래구분.Equals(1)) // 현재가
            {
                after_order = 현재가;
            }
            else if (거래구분.Equals(2)) // 호가
            {
                after_order = Hoga_Calculus(ItemCode, 현재가, 주문가_value);
            }
            else if (거래구분.Equals(3)) // %
            {
                after_order = int.Parse(주문가계산(ItemCode, 주문가_value, 현재가, 현재가, 상하).Split('&')[0]);
            }
            else
            {
                after_order = 현재가;
            }

            return after_order;
        }

        public static string 주문가계산(string code, double ratio, int standard, int NowPrice, string 상하)
        {
            string para = "";
            string Market = Form1.Market_Item_List[code].Market;

            int 주문가_계산 = 호가맞추기(standard, Market);

            if (ratio < 0) // 하한가 // 매수
            {
                for (int i = -1; i < 1; i--)
                {
                    int 주문가 = 주문가_계산 - GetHoga(주문가_계산, Market);

                    주문가_계산 = 주문가;

                    double 주문비 = ((double)주문가 - (double)standard) / (double)standard * (double)100;

                    if (상하.Equals("하한가"))
                    {
                        int 하한가 = 상한가_하한가_구하기("하", code);
                        if (주문가 <= 하한가)
                        {
                            주문가 = 하한가;

                            주문비 = Math.Round(((double)주문가 - (double)NowPrice) / (double)NowPrice * 100, 2);
                            para = 주문가 + "&" + 주문비 + "&" + i;
                            break;
                        }
                    }

                    if (주문비 <= ratio)
                    {
                        주문비 = Math.Round(((double)주문가 - (double)NowPrice) / (double)NowPrice * 100, 2);
                        para = 주문가 + "&" + 주문비 + "&" + i;
                        break;
                    }
                }
            }
            else if (ratio > 0)// 상한가 // 매도
            {
                for (int i = 1; i > -1; i++)
                {
                    int 주문가 = 주문가_계산 + GetHoga(주문가_계산, Market);

                    주문가_계산 = 주문가;

                    double 주문비 = ((double)주문가 - (double)standard) / (double)standard * (double)100;

                    if (상하.Equals("상한가"))
                    {
                        int 상한_가 = 상한가_하한가_구하기("상", code);
                        if (주문가 >= 상한_가)
                        {
                            주문가 = 상한_가;
                            주문비 = Math.Round(((double)주문가 - (double)NowPrice) / (double)NowPrice * 100, 2);
                            para = 주문가 + "&" + 주문비 + "&" + i;
                            break;
                        }
                    }

                    if (주문비 >= ratio)
                    {
                        주문비 = Math.Round(((double)주문가 - (double)NowPrice) / (double)NowPrice * 100, 2);

                        para = 주문가 + "&" + 주문비 + "&" + i;
                        break;
                    }
                }
            }
            else if (ratio == 0)
            {
                int 주문가 = 예약order_price(0, standard, Market);
                int 상한_가 = 상한가_하한가_구하기("상", code);
                int 하한_가 = 상한가_하한가_구하기("하", code);

                if (주문가 >= 상한_가)
                {
                    주문가 = 상한_가;
                }
                else if (주문가 <= 하한_가)
                {
                    주문가 = 하한_가;
                }

                double 주문비 = Math.Round(((double)주문가 - (double)NowPrice) / (double)NowPrice * 100, 2);

                para = 주문가 + "&" + 주문비 + "&" + 0;
            }
            return para;
        }

        public static string 감시주문가계산(string code, double ratio, int standard, int NowPrice)
        {
            string para = "";
            string Market = Form1.Market_Item_List[code].Market;
            int 주문가_계산 = 호가맞추기(standard, Market);

            if (ratio < 0) // 하한가 // 매수
            {
                for (int i = -1; i < 1; i--)
                {
                    int 주문가 = 주문가_계산 - GetHoga(주문가_계산, Market);

                    주문가_계산 = 주문가;

                    double 주문비 = ((double)주문가 - (double)standard) / (double)standard * (double)100;

                    if (주문비 <= ratio)
                    {
                        para = 주문가 + "&" + Math.Round(주문비, 2) + "&" + i;
                        break;
                    }
                }
            }
            else if (ratio > 0)// 상한가 // 매도
            {
                for (int i = 1; i > -1; i++)
                {
                    int 주문가 = 주문가_계산 + GetHoga(주문가_계산, Market);

                    주문가_계산 = 주문가;

                    double 주문비 = ((double)주문가 - (double)standard) / (double)standard * (double)100;

                    if (주문비 >= ratio)
                    {
                        para = 주문가 + "&" + Math.Round(주문비, 2) + "&" + i;
                        break;
                    }
                }
            }
            else if (ratio == 0)
            {
                int 주문가 = 예약order_price(0, standard, Market);
                double 주문비 = Math.Round(((double)주문가 - (double)NowPrice) / (double)NowPrice * 100, 2);
                para = 주문가 + "&" + 주문비 + "&" + 0;
            }
            return para;
        }

        public static int 예약order_price(int index, int 기준가, string Market)
        {
            int re_wont = 기준가;
            if (index > 0)
            {
                for (int i = 0; i < index; i++) // 10
                {
                    int 계산 = re_wont;
                    re_wont = 계산 + GetHoga(계산, Market);
                }
            }
            else if (index < 0)
            {
                for (int i = 0; i > index; i--)  // -10
                {

                    int 계산 = re_wont;
                    re_wont = 계산 - GetHoga(계산, Market);
                }
            }
            else
            {
                if (index == 0)
                {
                    return 기준가;
                }
            }
            return re_wont;
        }

        public static string 기준가고정_비(int 기준가, int 현재가, string Market)
        {
            string re = "";

            if (기준가 < 현재가)
            {
                int 주문가_계산 = 현재가;

                for (int i = -1; i < 0; i--)
                {
                    int 계산 = 주문가_계산 - GetHoga(주문가_계산, Market);
                    주문가_계산 = 계산;

                    if (기준가 >= 계산)
                    {
                        double 주문비 = Math.Round(((double)기준가 - 현재가) / 기준가 * 100, 2);

                        re = 주문비 + "&" + i;
                        break;
                    }
                }
            }
            else if (기준가 > 현재가)
            {
                int 주문가_계산 = 현재가;

                for (int i = 1; i > 0; i++)
                {
                    int 계산 = 주문가_계산 + GetHoga(주문가_계산, Market);
                    주문가_계산 = 계산;

                    if (기준가 <= 계산)
                    {
                        double 주문비 = Math.Round(((double)기준가 - 현재가) / 기준가 * 100, 2);

                        re = 주문비 + "&" + i;
                        break;
                    }
                }
            }
            else if (기준가 == 현재가)
            {
                re = 0.00 + "&" + 0;
            }

            return re;
        }

        public static int GetHoga(int 기준값, string Market) //호가 구하기 
        {
            int Tick = 1;

            if (Market.Equals("E"))
            {
                Tick = 5;
                if (기준값 < 2000) Tick = 1;
                return Tick;
            }
            else
            {
                Tick = 1000;
                if (기준값 < 500000) Tick = 500;
                if (기준값 < 200000) Tick = 100;
                if (기준값 < 50000) Tick = 50;
                if (기준값 < 20000) Tick = 10;
                if (기준값 < 5000) Tick = 5;
                if (기준값 < 2000) Tick = 1;

                return Tick;
            }
        }

        public static int 호가맞추기(int 기준값, string Market)
        {
            int 주문가 = 기준값;
            int Tick = GetHoga(기준값, Market);

            if (기준값 > 1000)
            {
                for (int i = 0; i > -1; i++)
                {
                    if (기준값 % Tick == 0)
                    {
                        주문가 = 기준값;
                        break;
                    }
                    else
                    {
                        기준값++;
                    }
                }
            }
            return 주문가;
        }

        public static int 호가변환(double 주문비율, string Code, int 호가기준가, int 현재가, string 마켓, string 검색식)// 상한가 하한가 기준 계산. 
        {
            int 호가 = 0;
            string 상하 = "하한가";

            if (검색식.Contains("매도"))
            {
                상하 = "상한가";
            }


            string para = 주문가계산(Code, 주문비율, 호가기준가, 현재가, 상하);
            호가 = int.Parse(para.Split('&')[2]);
            return 호가;
        }


        public static int Hoga_Calculus(string ItemCode, int now_price, double Hoga)  // 호가 계산  마켓 , 현재가 , 호가계산  리턴 => 현재값 * 호가
        {
            int 전일종가 = int.Parse(Form1.form1.axKHOpenAPI1.GetMasterLastPrice(ItemCode));
            string Market = Form1.Market_Item_List[ItemCode].Market;

            int re_wont = now_price;//   + (hoga * wont);

            if (Hoga == 0)
            {
                return re_wont;
            }
            else
            {
                if (Hoga > 0)
                {
                    for (int i = 0; i < Hoga; i++)
                    {
                        int 계산 = re_wont;
                        re_wont = 계산 + GetHoga(계산, Market);
                    }
                }
                else
                {
                    for (int i = 0; i < int.Parse(Hoga.ToString().Substring(1)); i++)
                    {
                        int 계산 = re_wont;
                        re_wont = 계산 - GetHoga(계산, Market);
                    }
                }
            }

            return 상한가_하한가_주문가(ItemCode, re_wont);
        }


        public static int 상한가_하한가_주문가(string code, int 주문가)
        {
            int 전일종가 = int.Parse(Form1.form1.axKHOpenAPI1.GetMasterLastPrice(code));
            string Market = Form1.Market_Item_List[code].Market;
            int nResult = 0;
            int nMod = 0;
            int nnMod = 0;


            if (주문가 <= 하한가())
            {
                return 하한가();
            }
            else
            {
                if (주문가 >= 상한가())
                {
                    return 상한가();
                }
                else
                {
                    return 주문가;
                }
            }

            int 상한가() // 상한가 구하기 
            {
                nResult = (int)(전일종가 * 0.3);

                if ((nResult + 전일종가) > 1000)
                {
                    nMod = nResult % GetHoga(전일종가, Market);
                    nResult -= nMod;
                    nResult = (int)전일종가 + nResult;
                    nnMod = nResult % GetHoga(nResult, Market);
                    nResult = nResult -= nnMod;
                }
                return nResult;
            }

            int 하한가() // 하한가 구하기 
            {
                nResult = (int)(전일종가 * 0.3);

                if ((nResult + 전일종가) > 1000)
                {
                    nMod = nResult % GetHoga(전일종가, Market);
                    nResult -= nMod;
                    nResult = (int)전일종가 - nResult;
                    nnMod = nResult % GetHoga(nResult, Market);
                    nResult = nResult -= nnMod;
                }
                return nResult;
            }
        }


        public static int 상한가_하한가_구하기(string 상하, string code)
        {
            int 전일종가 = int.Parse(Form1.form1.axKHOpenAPI1.GetMasterLastPrice(code));
            string Market = Form1.Market_Item_List[code].Market;
            int nResult = 0;
            int nMod = 0;
            int nnMod = 0;

            switch (상하)
            {
                case "상":
                    nResult = (int)(전일종가 * 0.3);
                    nMod = nResult % GetHoga(전일종가, Market);
                    nResult -= nMod;
                    nResult = (int)전일종가 + nResult;
                    nnMod = nResult % GetHoga(nResult, Market);
                    nResult = nResult -= nnMod;
                    break;
                case "하":
                    nResult = (int)(전일종가 * 0.3);
                    nMod = nResult % GetHoga(전일종가, Market);
                    nResult -= nMod;
                    nResult = (int)전일종가 - nResult;
                    nnMod = nResult % GetHoga(nResult, Market);
                    nResult = nResult -= nnMod;
                    break;
            }
            return nResult;
        }

        public static bool RunTime(int start, int end)
        {
            bool on = false;

            if (start <= Form1.timenow && Form1.timenow <= end)
            {
                on = true;
            }
            return on;
        }

        //0 수익률(%)
        //1 평가손익금
        //2 예상손익금
        //3 등락율(%)
        //4 수익률 + 예상
        //5 하기준(기준 + 수익률 이하)
        //6 상기준(기준 + 수익률 이상)
        //7 하최종(최종매입가 이하)

        public static bool 수익범위(bool 매수도, bool 단위_기준금, Stockbalance 잔고, double low, double height, int gubun, string location)
        {
            bool on = false;
            long 매수기준금 = Properties.Settings.Default.MT_buying_standard;

            if (gubun == 0) // 수익률
            {
                if (low <= 잔고.수익률 && 잔고.수익률 <= height)
                {
                    on = true;
                }
            }
            else if (gubun == 3) // 등락률
            {
                if (low <= 잔고.등락율 && 잔고.등락율 <= height)
                {
                    on = true;
                }
            }
            else if (gubun == 4) // 수익 + 예상손익
            {
                if (low <= 잔고.수익률)
                {
                    double 예상손익 = height * 10000;
                    if (단위_기준금) 예상손익 = 매수기준금 * (double)height / 100;

                    if (예상손익 <= 잔고.예상손익)
                    {
                        on = true;
                    }
                }
            }
            else if (gubun == 5) // 기준 하
            {
                if (height >= 잔고.기준수익률)
                {
                    on = true;
                }
            }
            else if (gubun == 6) // 기준 상
            {
                if (height <= 잔고.기준수익률)
                {
                    on = true;
                }
            }
            else if (gubun == 7) // 하 최종
            {
                int 단가 = 0;

                if (location.Equals("리밸_A")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals(location) && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); 단가 = 번호정렬[0].매입가; }
                if (location.Equals("리밸_B")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals(location) && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); 단가 = 번호정렬[0].매입가; }
                if (location.Equals("리밸_C")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals(location) && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); 단가 = 번호정렬[0].매입가; }
                if (location.Equals("리밸_D")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals(location) && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); 단가 = 번호정렬[0].매입가; }
                if (location.Equals("리밸_E")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals(location) && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); 단가 = 번호정렬[0].매입가; }
                if (location.Equals("리밸_F")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals(location) && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); 단가 = 번호정렬[0].매입가; }
                if (location.Equals("리밸_G")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals(location) && o.종목코드.Equals(잔고.종목코드)); List<최종매입가> 번호정렬 = list.OrderByDescending(o => o.번호).ToList(); 단가 = 번호정렬[0].매입가; }

                double tax_ = Form1.TAX;
                if (잔고.시장.Equals("E")) tax_ = 0;

                double 수익률 = Math.Truncate((((double)(잔고.현재가 - 단가) / 단가 * (double)100) - ((Form1.수수료 + Form1.수수료 + tax_) * 100)) * 100) / 100;

                if (수익률 <= height)
                {
                    on = true;
                }
            }
            else
            {
                if (단위_기준금)
                {
                    low = 매수기준금 * (double)low / 100;
                    height = 매수기준금 * (double)height / 100;
                }
                else
                {
                    low = low * 10000;
                    height = height * 10000;
                }

                if (gubun == 1) // 평가손익금 
                {
                    if (low <= 잔고.평가손익 && 잔고.평가손익 <= height)
                    {
                        on = true;
                    }
                }
                else if (gubun == 2) // 예상손익금 
                {
                    if (매수도) // 매수
                    {
                        if (low <= 잔고.예상손익 && 잔고.예상손익 <= height)
                        {
                            on = true;
                        }
                    }
                    else // 매도
                    {
                        if (low <= 잔고.예상손익 && 잔고.예상손익 <= height && 잔고.예상손익 > 0 && 잔고.수익률 > 0)
                        {
                            on = true;
                        }
                    }
                }
            }

            return on;
        }


        public static bool 시간매도수익범위(int index, double low, double height, long 손익금, double 수익률)
        {
            bool on = false;
            if (index == 0)
            {
                if (low <= 수익률 && 수익률 <= height)
                {
                    on = true;
                }
            }
            else
            {
                long 매수기준금 = Properties.Settings.Default.MT_buying_standard;

                if (index == 1)
                {
                    low = low * 10000;
                    height = height * 10000;
                }
                else
                {
                    low = 매수기준금 * low / 100;
                    height = 매수기준금 * height / 100;
                }

                if (low <= 손익금 && 손익금 <= height)
                {
                    on = true;
                }
            }

            return on;
        }


        public static bool 지수연동_범위(bool Low, int index, double value, long 손익금, double 수익률)
        {
            bool on = false;

            if (index == 0)
            {
                if (Low)
                {
                    if (value <= 수익률)
                    {
                        on = true;
                    }
                }
                else
                {
                    if (value >= 수익률)
                    {
                        on = true;
                    }
                }
            }
            else
            {
                long 매수기준금 = Properties.Settings.Default.MT_buying_standard;

                if (index == 1)
                {
                    value = value * 10000;
                }
                else
                {
                    value = 매수기준금 * value / 100;
                }

                if (Low)
                {
                    if (value <= 손익금)
                    {
                        on = true;
                    }
                }
                else
                {
                    if (value >= 손익금)
                    {
                        on = true;
                    }
                }
            }

            return on;
        }



        public static bool 계좌매도수익범위(bool 단위_기준금, long 손익금, double low, double height)
        {
            bool on = false;
            long 매수기준금 = Properties.Settings.Default.MT_buying_standard;

            if (단위_기준금)
            {
                low = 매수기준금 * low / 100;
                height = 매수기준금 * height / 100;
            }
            else
            {
                low = low * 10000;
                height = height * 10000;
            }

            if (low <= 손익금 && 손익금 <= height)
            {
                on = true;
            }

            return on;
        }

        public static string 계좌매도조건범위(bool 단위_기준금, long 손익금, double low, double height, string 청산, string 위치)
        {
            string on = 청산;

            long 매수기준금 = Properties.Settings.Default.MT_buying_standard;

            if (단위_기준금)
            {
                low = 매수기준금 * low / 100;
                height = 매수기준금 * height / 100;
            }
            else
            {
                low = low * 10000;
                height = height * 10000;
            }

            if (!청산.Equals("＊"))
            {
                if (손익금 <= low)
                {
                    on = "＊";
                }
            }
            else
            {
                if (height <= 손익금)
                {
                    on = 위치;
                }
            }

            return on;
        }

        //0 평가수익률
        //1 평가손익금
        //2 예상손익금
        //3 주가등락률
        //4 수익률 + 예상
        //5 기하(기준 + 수익률 이하)
        //6 기상(기준 + 수익률 이상)

        public static string 조건범위(bool 단위_기준금, double 등락율, double 수익률, long 평가손익, double 예상손익, double low, double height, int gubun, string 상태, string 위치, bool 매수)
        {
            string result = 상태;
            long 매수기준금 = Properties.Settings.Default.MT_buying_standard;

            if (매수)
            {
                if (gubun.Equals(0)) //  수익률
                {
                    if (!상태.Equals("X")) if (수익률 <= low) result = "X";
                    if (상태.Equals("X")) if (height <= 수익률) result = 위치;
                }
                else if (gubun.Equals(3)) //  등락율
                {
                    if (!상태.Equals("X")) if (등락율 <= low) result = "X";
                    if (상태.Equals("X")) if (height <= 등락율) result = 위치;
                }
                else
                {
                    if (단위_기준금)
                    {
                        low = 매수기준금 * low / 100;
                        height = 매수기준금 * height / 100;
                    }
                    else
                    {
                        low = low * 10000;
                        height = height * 10000;
                    }

                    if (gubun.Equals(1)) // 평가기준금
                    {
                        if (!상태.Equals("X")) if (평가손익 <= low) result = "X";
                        if (상태.Equals("X")) if (height <= 평가손익) result = 위치;
                    }
                    else if (gubun.Equals(2)) // 예상수익금
                    {
                        if (!상태.Equals("X")) if (예상손익 <= low) result = "X";
                        if (상태.Equals("X")) if (height <= 예상손익) result = 위치;
                    }
                }
            }
            else //매도
            {
                if (gubun.Equals(0)) //  수익률
                {
                    if (!상태.Equals("X")) if (low <= 수익률) result = "X";
                    if (상태.Equals("X")) if (height >= 수익률) result = 위치;
                }
                else if (gubun.Equals(3)) //  등락율
                {
                    if (!상태.Equals("X")) if (low <= 등락율) result = "X";
                    if (상태.Equals("X")) if (height >= 등락율) result = 위치;
                }
                else
                {
                    if (단위_기준금)
                    {
                        low = 매수기준금 * low / 100;
                        height = 매수기준금 * height / 100;
                    }
                    else
                    {
                        low = low * 10000;
                        height = height * 10000;
                    }

                    if (gubun.Equals(1)) // 평가기준금
                    {
                        if (!상태.Equals("X")) if (low <= 평가손익) result = "X";
                        if (상태.Equals("X")) if (height >= 평가손익) result = 위치;
                    }
                    else if (gubun.Equals(2)) // 예상수익금
                    {
                        if (!상태.Equals("X")) if (low <= 예상손익) result = "X";
                        if (상태.Equals("X")) if (height >= 예상손익) result = 위치;
                    }
                }
            }
            return result;
        }


        public static bool 매매범위(double low, double height, int gubun, long 매입금)
        {
            bool on = false;
            long 기준금 = Properties.Settings.Default.MT_buying_standard;

            switch (gubun)
            {
                case 0: // 만원

                    if (low * 10000 <= 매입금 && 매입금 <= height * 10000)
                    {
                        on = true;
                    }
                    break;

                case 1: // 기준금(%)

                    if ((low / 100 * 기준금) <= 매입금 && 매입금 <= (height / 100 * 기준금))
                    {
                        on = true;
                    }
                    break;
            }
            return on;
        }


        public static bool 매입비추매제한(double 잔고비중)
        {
            bool on = true;
            if (Properties.Settings.Default.CB_잔고매입비_추매제한)
            {
                on = false;

                if (잔고비중 < Properties.Settings.Default.TB_잔고매입비_추매제한) on = true;
            }

            return on;
        }

        public static long 적용금액계산(int 주문가격, int 주문수량, int 현재가)
        {
            if (주문가격 == 0)
                주문가격 = 현재가;

            long 적용금액 = 주문가격 * 주문수량;

            return 적용금액;
        }


        public static int Find_Tik_Cap(int 현재가, int 주문가격, string Market)
        {
            int result = 0;
            int 기준가 = 현재가;

            if (주문가격 > 0)
            {
                if (주문가격 < 현재가)
                {
                    for (int i = 0; i < 1; i--)
                    {
                        if (기준가 <= 주문가격)
                        {
                            result = i;
                            break;
                        }
                        else
                        {
                            기준가 = 기준가 - GetHoga(기준가, Market);
                        }
                    }
                }
                else
                {
                    if (주문가격 > 현재가)
                    {
                        for (int i = 0; i > -1; i++)
                        {
                            if (기준가 >= 주문가격)
                            {
                                result = i;
                                break;
                            }
                            else
                            {
                                기준가 = 기준가 + GetHoga(기준가, Market);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static bool 매매중복체크(string itemCode, string condition)
        {
            bool on = true;

            if (Form1.JumunItem_List.Count > 0)
            {
                JumunItem JumunItem = Form1.JumunItem_List.Find(o => o.검색식.Equals(condition) && o.종목코드.Equals(itemCode));
                if (JumunItem != null)
                {
                    on = false;
                }
            }

            return on;
        }

        public static void 주문초과알림(string 종목명)
        {
            if (!Form1.form1.주문초과종목명.Equals(종목명))
            {
                Form1.form1.주문초과종목명 = 종목명;
                Form1.알림창("[ 주문초과 ]\n\n" + 종목명 + " 의 주문이 200건을 넘어 주문할수 없습니다.", 20, false);
            }
        }

        public static bool 청산주문_매매범위(Stockbalance 잔고, double 범위_1, double 범위_2)
        {
            if (잔고.주문가능수량 <= (범위_2 / 100 * 잔고.매도기준))
            {
                if ((범위_1 / 100 * 잔고.매도기준) <= 잔고.주문가능수량)
                {
                    return true;
                }
            }

            return false;
        }

        public static int 청산주문_매매범위_주문수량계산(Stockbalance 잔고, int 청산주문수량, double 범위_1)
        {
            double 주문수량 = 청산주문수량;

            if ((범위_1 / 100 * 잔고.매도기준) <= 잔고.주문가능수량)
            {
                if (범위_1 == 0)
                {
                    if (주문수량 > 잔고.주문가능수량) 주문수량 = 잔고.주문가능수량;
                }
                else
                {
                    double 범위_1수량 = Math.Ceiling(범위_1 / 100 * 잔고.매도기준);

                    if (범위_1수량 > (잔고.주문가능수량 - 주문수량))
                    {
                        주문수량 = 잔고.주문가능수량 - 범위_1수량;

                        if (Math.Ceiling(범위_1 / 100 * 잔고.매도기준) == 1) 주문수량 = 0;
                    }
                }
            }
            else
            {
                주문수량 = 0;
            }

            return (int)주문수량;
        }



        public static bool 추매가능_Check(Stockbalance 잔고, bool 매수)
        {
            bool result = true;

            if (매수)
            {
                string str = "";
                string 시장 = "코스피";
                if (잔고.시장.Equals("D")) 시장 = "코스닥";

                if (Properties.Settings.Default.TB_추매주가이상 <= 잔고.현재가 && 잔고.현재가 <= Properties.Settings.Default.TB_추매주가이하)
                {
                    if (Properties.Settings.Default.TB_추매등락률이상 <= 잔고.등락율 && 잔고.등락율 <= Properties.Settings.Default.TB_추매등락률이하)
                    {

                    }
                    else
                    {
                        result = false;
                        str = "[추매제한 '주가 등락률'] " + 시장 + "ㆍ" + 잔고.종목명 + "ㆍ등락률(" + 잔고.등락율 + "%) 제한 등락률(" + Properties.Settings.Default.TB_추매등락률이상 + " ~ " + Properties.Settings.Default.TB_추매등락률이하 + ")";
                    }
                }
                else
                {
                    result = false;
                    str = "[추매제한 '주가 범위'] " + 시장 + "ㆍ" + 잔고.종목명 + "ㆍ주가(" + 잔고.현재가.ToString("N0") + ")이 제한 주가(" + Properties.Settings.Default.TB_추매주가이상.ToString("N0") + " ~ " + Properties.Settings.Default.TB_추매주가이하.ToString("N0") + ")";
                }

                if (!result)
                {
                    Tab_Basic.매매거부_메세지출력(잔고.종목명, str);
                }
            }

            return result;
        }




        public static bool 매매확인_VI_모투가능확인(Market_Item Market, int 주문유형)
        {
            bool result = true;
            string 비고 = "";
            int 현재가 = Market.현재가;
            string 종목명 = Market.종목명;
            string 종목상태 = Market.과열;

            if (Form1.server.Equals("모의투자"))
            {
                if (현재가 < 1000)
                {
                    result = false;

                    비고 = "[모투 제한] 종목: " + 종목명 + " 현재가: " + 현재가 + " 키움 정책상 1000원 미만 종목은 모의투자가 제한됩니다.";

                    if (Market.매매알림_모투제한)
                    {
                        Market.매매알림_모투제한 = false;
                        Form1.Error_Log(비고);
                    }
                }
                else
                {
                    if (!Market.매매알림_모투제한)
                    {
                        Market.매매알림_모투제한 = true;
                    }
                }
            }

            if (result)
            {
                if (종목상태.Contains("상한가"))
                {
                    if (Properties.Settings.Default.CB_상매수취소)
                    {
                        if (주문유형 == 1)
                        {
                            result = false;

                            비고 = "[매수 제한] 종목: " + 종목명 + " 현재가: " + 현재가 + " '상한가' 매수취소 됩니다.";

                            if (Market.매매알림_상한가)
                            {
                                Market.매매알림_상한가 = false;
                                Form1.Error_Log(비고);
                            }
                        }
                    }
                }
                else
                {
                    if (!Market.매매알림_상한가)
                    {
                        Market.매매알림_상한가 = true;
                    }
                }
            }

            if (result)
            {
                if (종목상태.Contains("하한가"))
                {
                    if (Properties.Settings.Default.CB_하매도취소)
                    {
                        if (주문유형 == 2)
                        {
                            result = false;

                            비고 = "[매도 제한] 종목: " + 종목명 + " 현재가: " + 현재가 + " '하한가' 매도취소 됩니다.";

                            if (Market.매매알림_하한가)
                            {
                                Market.매매알림_하한가 = false;
                                Form1.Error_Log(비고);
                            }
                        }
                    }
                }
                else
                {
                    if (!Market.매매알림_하한가)
                    {
                        Market.매매알림_하한가 = true;
                    }
                }
            }

            if (result)
            {
                if (주문유형 == 1)
                {
                    if (종목상태.Contains("과열(VI)"))
                    {
                        if (Properties.Settings.Default.CB_VI매수취소)
                        {
                            result = false;

                            비고 = "[매수 제한] 종목: " + 종목명 + " 현재가: " + 현재가 + " '과열(VI)' 매수취소 됩니다.";

                            if (Market.매매알림_vI매수)
                            {
                                Market.매매알림_vI매수 = false;
                                Form1.Error_Log(비고);
                            }
                        }
                    }
                    else
                    {
                        if (!Market.매매알림_vI매수)
                        {
                            Market.매매알림_vI매수 = true;
                        }
                    }
                }

                if (주문유형 == 2)
                {
                    if (종목상태.Contains("과열(VI)"))
                    {
                        if (Properties.Settings.Default.CB_VI매도취소)
                        {
                            result = false;

                            비고 = "[매도 제한] 종목: " + 종목명 + " 현재가: " + 현재가 + " '과열(VI)' 매도취소 됩니다.";

                            if (Market.매매알림_VI매도)
                            {
                                Market.매매알림_VI매도 = false;
                                Form1.Error_Log(비고);
                            }
                        }
                    }
                    else
                    {
                        if (!Market.매매알림_VI매도)
                        {
                            Market.매매알림_VI매도 = true;
                        }
                    }

                }
            }

            return result;
        }


        public static string 단위변환(double 단가)
        {
            string resut = 단가.ToString("N0");

            if (Math.Abs(단가) > 999999)
            {
                resut = Math.Round((double)(단가 / 10000), 0).ToString("N0") + "만";
            }

            return resut;
        }




    }
}