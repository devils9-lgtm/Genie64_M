using System;
using System.Threading.Tasks;



namespace 지니_64
{


    public class GET
    {
        public static void 신규매수방법()
        {
            if (Properties.Settings.Default.CB_new_A)
            {
                if (!Properties.Settings.Default.CB_new_B)
                {
                    if (!Properties.Settings.Default.CB_new_C)
                    {
                        Form1.form1.신규매수방법 = "A";
                    }
                }
            }

            if (!Properties.Settings.Default.CB_new_A)
            {
                if ((Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 0) || (Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 2))
                {
                    if (!Properties.Settings.Default.CB_new_C)
                    {
                        Form1.form1.신규매수방법 = "B";
                    }
                }
            }

            if (!Properties.Settings.Default.CB_new_A)
            {
                if (!Properties.Settings.Default.CB_new_B)
                {
                    if ((Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_or_C == 0) || (Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_or_C == 2))
                    {
                        Form1.form1.신규매수방법 = "C";
                    }
                }
            }

            if (Properties.Settings.Default.CB_new_A)
            {
                if ((Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 0) || (Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 2))
                {
                    if (!Properties.Settings.Default.CB_new_C)
                    {
                        Form1.form1.신규매수방법 = "AoB";
                    }
                }
            }

            if (Properties.Settings.Default.CB_new_A)
            {
                if (!Properties.Settings.Default.CB_new_B)
                {
                    if ((Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_or_C == 0) || (Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_or_C == 2))
                    {
                        Form1.form1.신규매수방법 = "AoC";
                    }
                }
            }

            if (!Properties.Settings.Default.CB_new_A)
            {
                if ((Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 0) || (Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 2))
                {
                    if ((Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_or_C == 0) || (Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_or_C == 2))
                    {
                        Form1.form1.신규매수방법 = "BoC";
                    }
                }
            }

            if (Properties.Settings.Default.CB_new_A)
            {
                if ((Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 0) || (Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 2))
                {
                    if ((Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_or_C == 0) || (Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_or_C == 2))
                    {
                        Form1.form1.신규매수방법 = "AoBoC";
                    }
                }
            }

            if (Properties.Settings.Default.CB_new_A)
            {
                if ((Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 1) || (Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 3))
                {
                    if (!Properties.Settings.Default.CB_new_C)
                    {
                        Form1.form1.신규매수방법 = "AnB";
                    }
                }
            }

            if (Properties.Settings.Default.CB_new_A)
            {
                if ((Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 1) || (Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 3))
                {
                    if ((Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_or_C == 1) || (Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_or_C == 3))
                    {
                        Form1.form1.신규매수방법 = "AnBnC";
                    }
                }
            }

            if (Properties.Settings.Default.CB_new_A)
            {
                if ((Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 1) || (Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_or_B == 3))
                {
                    if ((Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_or_C == 0) || (Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_or_C == 2))
                    {
                        Form1.form1.신규매수방법 = "AnBoC";
                    }
                }
            }
        }

        public static string 주문유형(int 주문유형)
        {
            string 주문 = "";

            switch (주문유형)
            {
                case 1:
                    주문 = "매수";
                    break;

                case 2:
                    주문 = "매도";
                    break;

                case 3:
                    주문 = "수취소";
                    break;

                case 4:
                    주문 = "도취소";
                    break;

            }
            return 주문;
        }



        public static string ScreenNum() // 1101 ~ 1299
        {
            Form1.form1.스크린Num++;
            if (Form1.form1.스크린Num > 1299) Form1.form1.스크린Num = 1100;

            int 스크린넘버 = Form1.form1.스크린Num;
            return 스크린넘버.ToString();
        }

        public static int start_stop_time(bool start, int time)
        {
            int result = time;

            if (start)
            {
                if (Form1.NXT)
                {
                    if (result < 80000) result = 80000;
                    if (result > 200000) result = 80000;
                }
                else
                {
                    if (result < 90000) result = 90000;
                    if (result > 153000) result = 90000;
                }
            }
            else
            {
                if (Form1.NXT)
                {
                    if (result < 80000) result = 200000;
                    if (result > 200000) result = 200000;
                }
                else
                {
                    if (result < 85000) result = 152500;
                    if (result > 153000) result = 152500;
                }
            }

            return result;
        }


        public static void StockState(string itemcode, string 호가매수2, string 호가매수4, string 호가매도2, string 호가매도4)
        {
            var Task = new Task(() =>
            {
                if (Form1.Market_Item_List.ContainsKey(itemcode))
                {
                    Market_Item Market = Form1.Market_Item_List[itemcode];

                    int.TryParse(호가매수2, out int 매수2);
                    int.TryParse(호가매수4, out int 매수4);
                    int.TryParse(호가매도2, out int 매도2);
                    int.TryParse(호가매도4, out int 매도4);

                    try
                    {
                        if (Form1.stockBalanceList.TryGetValue(itemcode, out Stockbalance 잔고))
                        {
                            if (!잔고.매매가능 && !잔고.종목상태.Contains("거래정지"))
                            {
                                if (Form1.server_알림.Equals("메인마켓"))
                                {
                                    if (잔고.종목상태.Contains("시세로딩") || 잔고.종목상태.Contains("마켓대기") || 잔고.종목상태.Contains("장전동시"))
                                    {
                                        if (매수4 != 0 || 매도4 != 0 || (Form1.timenow >= Form1.메인마켓시작 + 200))
                                        {
                                            잔고.매매가능 = true;
                                            잔고.잔고청산 = true;
                                            잔고.종목상태 = Jango_state(잔고.종목코드);
                                        }
                                    }
                                }
                            }

                            if (!잔고.종목상태.Contains("시세로딩") && !잔고.종목상태.Contains("마켓대기") & !잔고.종목상태.Contains("장전동시"))
                            {
                                if (매수4 == 0 && 매도4 == 0)
                                {
                                    if (Form1.timenow < Form1.메인마켓종료)
                                    {
                                        if (Form1.server_알림.Equals("동시호가"))
                                        {
                                            잔고.종목상태 = "동시호가";
                                        }
                                        else
                                        {
                                            if (Form1.메인마켓시작 + 10 <= Form1.timenow) 잔고.종목상태 = "과열(VI)";
                                        }
                                    }
                                }
                                else
                                {
                                    if (매도2 == 0)
                                    {
                                        잔고.종목상태 = "상한가";
                                    }
                                    else
                                    {
                                        if (매수2 == 0)
                                        {
                                            잔고.종목상태 = "하한가";
                                        }
                                        else
                                        {
                                            if (잔고.종목상태.Equals("과열(VI)") || 잔고.종목상태.Equals("상한가") || 잔고.종목상태.Equals("하한가"))
                                            {
                                                if (Form1.NXT_server)
                                                {
                                                    NXT nxt = Form1.NXT_list.Find(o => o.code.Equals(잔고.종목코드));
                                                    if (nxt == null)
                                                    {
                                                        잔고.종목상태 = "마켓종료";
                                                    }
                                                    else
                                                    {
                                                        잔고.종목상태 = Jango_state(itemcode);
                                                    }
                                                }
                                                else
                                                {
                                                    잔고.종목상태 = Jango_state(itemcode);
                                                }
                                            }
                                        }
                                    }

                                    if (매도2 == 0 && 매수2 == 0) // 장시작전
                                    {
                                        잔고.종목상태 = "마켓종료";
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        Form1.Error_Log("호가에러:: " + Market.종목명 + " 잔고에 없습니다.");
                    }

                    try
                    {
                        if (매수4 == 0 && 매도4 == 0)
                        {
                            if (Form1.server_알림.Equals("장전종시")) // 10분전 동시효과
                            {
                                Market.과열 = "장전동시";
                            }
                            else
                            {
                                if (Form1.timenow < Form1.메인마켓종료)
                                {
                                    if (Form1.server_알림.Equals("동시호가"))
                                    {
                                        Market.과열 = "동시호가";
                                    }
                                    else
                                    {
                                        if (Form1.메인마켓시작 + 10 <= Form1.timenow) Market.과열 = "과열(VI)";
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (매도2 == 0)
                            {
                                Market.과열 = "상한가";
                            }
                            else
                            {
                                if (매수2 == 0)
                                {
                                    Market.과열 = "하한가";
                                }
                                else
                                {
                                    Market.과열 = "정상";
                                }
                            }
                        }
                    }
                    catch
                    {
                        Form1.Error_Log("호가에러:: " + Market.종목명 + " 종목상태 확인에러.");
                    }
                }
            });
            Form1.Real_data_Manager.RequestTrData(Task);
        }

        public static int Order번호()
        {
            int Order번호 = 0;

            for (int i = 0; i > -1; i++)
            {
                JumunItem result = Form1.JumunItem_List.Find(o => o.Order번호 == i);
                if (result == null)
                {
                    Order번호 = i;
                    break;
                }
            }

            return Order번호;
        }

        public static int jumunScreen(string 종목코드)
        {
            int screennum = 0;

            for (int i = 1100; i <= 1300; i++)
            {
                JumunItem result = Form1.JumunItem_List.Find(o => o.종목코드.Equals(종목코드) && o.screennum.Equals(i.ToString()));
                if (result == null)
                {
                    screennum = i;
                    break;
                }
            }
            return screennum;
        }

        public static string Jango_state(string code) // 종목상태 요청
        {
            string 리턴 = "";
            string 상태 = Form1.form1.axKHOpenAPI1.GetMasterStockState(code);

            string nxt가능 = "N";
            NXT nxt = Form1.NXT_list.Find(o => o.code.Equals(code));
            if (nxt == null) nxt가능 = "X";

            int 증거금률 = int.Parse(상태.Substring(3, 2));
            if (증거금률.Equals(10))
            {
                증거금률 = 100;
            }

            string 신 = "불";
            if (상태.Contains("신용가능"))
            {
                신 = "신";
            }

            리턴 = nxt가능 + "｜" + 신 + "｜" + 증거금률;

            if (상태.Contains("거래정지"))
            {
                리턴 = "거래정지";
            }

            return 리턴;
        }

        public static string 거래구분_to한글(int 거래구분)
        {
            string re거래구분 = "지정가";

            if (거래구분.Equals(0))
            {
                re거래구분 = "시장가";
            }

            return re거래구분;
        }

        public static string 거래구분변환(string 구분)
        {
            string 거래_구분 = "";

            if (구분.Equals("03"))
            {
                거래_구분 = "시장가";
            }
            else
            {
                거래_구분 = "지정가";
            }

            return 거래_구분;
        }

        public static string cansel_order_string(int index)
        {
            string after_order = "";

            switch (index)
            {
                case 0:
                    after_order = "주문취소";
                    break;

                case 1:
                    after_order = "C 시장가";
                    break;

                case 2:
                    after_order = "C 현재가";
                    break;
                case 3:
                    after_order = "C 재주문";
                    break;
            }
            return after_order;
        }

        public static string After_order_string(int index, double Value)
        {
            string after_order = "";

            switch (index)
            {
                case 0:
                    after_order = "시장가";
                    break;
                case 1:
                    after_order = "현재가";
                    break;
                case 2:
                    after_order = Value + " 틱";
                    break;
                case 3:
                    after_order = Value + " %";
                    break;
                case 4:
                    after_order = "A 분할";
                    break;
                case 5:
                    after_order = "B 분할";
                    break;
                case 6:
                    after_order = "C 분할";
                    break;
            }

            return after_order;
        }

        public static string 전광판현황(Stockbalance 잔고, string 위치)
        {
            string result = 사용("A") + "." + 사용("B") + "." + 사용("C") + "." + 사용("D") + "." + 사용("E") + "." + 사용("F") + "." + 사용("G") + "." + 사용("H") + "." + 사용("I");

            if (위치.Equals("손절"))
            {
                result = 사용("A") + "." + 사용("B") + "." + 사용("C") + "." + 사용("D") + "." + 사용("E") + "." + 사용("F");
            }
            else if (위치.Equals("리밸"))
            {
                result = 사용("A") + "." + 사용("B") + "." + 사용("C") + "." + 사용("D") + "." + 사용("E") + "." + 사용("F") + "." + 사용("G");
            }
            else if (위치.Equals("반복"))
            {
                result = 사용("A") + "." + 사용("B") + "." + 사용("C") + "." + 사용("D") + "." + 사용("E") + "." + 사용("F") + "." + 사용("G") + "." + 사용("H") + "." + 사용("I") + "." + 사용("J") + "." + 사용("K") + "." + 사용("L") + "." + 사용("M") + "." + 사용("N");
            }
            else if (위치.Equals("잔고청산"))
            {
                result = 사용("A") + "." + 사용("B") + "." + 사용("C");
            }
            else if (위치.Equals("시간청산"))
            {
                result = 사용("A") + "." + 사용("B") + "." + 사용("C");
            }

            string 사용(string LC)
            {
                string str_ = "-";
                string today = DateTime.Now.ToString("yyyy/MM/dd");

                switch (LC)
                {
                    case "A":
                        if (위치.Equals("익절") && (!잔고.익절A || !잔고.TS_1)) str_ = "A";
                        if (위치.Equals("보전") && 잔고.보전A.Equals("3")) str_ = "A";
                        if (위치.Equals("일회") && 잔고.일회A.Contains(today)) str_ = "A";
                        if (위치.Equals("손절") && !잔고.손절A) str_ = "A";
                        if (위치.Equals("리밸") && 잔고.리벨A.Equals("X")) str_ = "A";
                        if (위치.Equals("반복") && 잔고.반복A.Equals("X")) str_ = "A";
                        if (위치.Equals("잔고청산") && 잔고.청산A.Equals("X")) str_ = "A";
                        if (위치.Equals("시간청산") && 잔고.시간청산동작_A.Equals("X")) str_ = "A";
                        break;
                    case "B":
                        if (위치.Equals("익절") && (!잔고.익절B || !잔고.TS_2)) str_ = "B";
                        if (위치.Equals("보전") && 잔고.보전B.Equals("3")) str_ = "B";
                        if (위치.Equals("일회") && 잔고.일회B.Contains(today)) str_ = "B";
                        if (위치.Equals("손절") && !잔고.손절B) str_ = "B";
                        if (위치.Equals("리밸") && 잔고.리벨B.Equals("X")) str_ = "B";
                        if (위치.Equals("반복") && 잔고.반복B.Equals("X")) str_ = "B";
                        if (위치.Equals("잔고청산") && 잔고.청산B.Equals("X")) str_ = "B";
                        if (위치.Equals("시간청산") && 잔고.시간청산동작_B.Equals("X")) str_ = "B";
                        break;
                    case "C":
                        if (위치.Equals("익절") && (!잔고.익절C || !잔고.TS_3)) str_ = "C";
                        if (위치.Equals("보전") && 잔고.보전C.Equals("3")) str_ = "C";
                        if (위치.Equals("일회") && 잔고.일회C.Contains(today)) str_ = "C";
                        if (위치.Equals("손절") && !잔고.손절C) str_ = "C";
                        if (위치.Equals("리밸") && 잔고.리벨C.Equals("X")) str_ = "C";
                        if (위치.Equals("반복") && 잔고.반복C.Equals("X")) str_ = "C";
                        if (위치.Equals("잔고청산") && 잔고.청산C.Equals("X")) str_ = "C";
                        if (위치.Equals("시간청산") && 잔고.시간청산동작_C.Equals("X")) str_ = "C";
                        break;
                    case "D":
                        if (위치.Equals("익절") && (!잔고.익절D || !잔고.TS_4)) str_ = "D";
                        if (위치.Equals("보전") && 잔고.보전D.Equals("3")) str_ = "D";
                        if (위치.Equals("일회") && 잔고.일회D.Contains(today)) str_ = "D";
                        if (위치.Equals("손절") && !잔고.손절D) str_ = "D";
                        if (위치.Equals("리밸") && 잔고.리벨D.Equals("X")) str_ = "D";
                        if (위치.Equals("반복") && 잔고.반복D.Equals("X")) str_ = "D";
                        break;
                    case "E":
                        if (위치.Equals("익절") && (!잔고.익절E || !잔고.TS_5)) str_ = "E";
                        if (위치.Equals("보전") && 잔고.보전E.Equals("3")) str_ = "E";
                        if (위치.Equals("일회") && 잔고.일회E.Contains(today)) str_ = "E";
                        if (위치.Equals("손절") && !잔고.손절E) str_ = "E";
                        if (위치.Equals("리밸") && 잔고.리벨E.Equals("X")) str_ = "E";
                        if (위치.Equals("반복") && 잔고.반복E.Equals("X")) str_ = "E";
                        break;
                    case "F":
                        if (위치.Equals("익절") && (!잔고.익절F || !잔고.TS_6)) str_ = "F";
                        if (위치.Equals("보전") && 잔고.보전F.Equals("3")) str_ = "F";
                        if (위치.Equals("일회") && 잔고.일회F.Contains(today)) str_ = "F";
                        if (위치.Equals("손절") && !잔고.손절F) str_ = "F";
                        if (위치.Equals("리밸") && 잔고.리벨F.Equals("X")) str_ = "F";
                        if (위치.Equals("반복") && 잔고.반복F.Equals("X")) str_ = "F";
                        break;
                    case "G":
                        if (위치.Equals("익절") && (!잔고.익절G || !잔고.TS_7)) str_ = "G";
                        if (위치.Equals("보전") && 잔고.보전G.Equals("3")) str_ = "G";
                        if (위치.Equals("일회") && 잔고.일회G.Contains(today)) str_ = "G";
                        if (위치.Equals("리밸") && 잔고.리벨G.Equals("X")) str_ = "G";
                        if (위치.Equals("반복") && 잔고.반복G.Equals("X")) str_ = "G";
                        break;
                    case "H":
                        if (위치.Equals("익절") && (!잔고.익절H || !잔고.TS_8)) str_ = "H";
                        if (위치.Equals("보전") && 잔고.보전H.Equals("3")) str_ = "H";
                        if (위치.Equals("일회") && 잔고.일회H.Contains(today)) str_ = "H";
                        if (위치.Equals("반복") && 잔고.반복H.Equals("X")) str_ = "H";
                        break;
                    case "I":
                        if (위치.Equals("익절") && (!잔고.익절I || !잔고.TS_9)) str_ = "I";
                        if (위치.Equals("보전") && 잔고.보전I.Equals("3")) str_ = "I";
                        if (위치.Equals("일회") && 잔고.일회I.Contains(today)) str_ = "I";
                        if (위치.Equals("반복") && 잔고.반복I.Equals("X")) str_ = "I";
                        break;
                    case "J":
                        if (위치.Equals("반복") && 잔고.반복J.Equals("X")) str_ = "J";
                        break;
                    case "K":
                        if (위치.Equals("반복") && 잔고.반복K.Equals("X")) str_ = "K";
                        break;
                    case "L":
                        if (위치.Equals("반복") && 잔고.반복L.Equals("X")) str_ = "L";
                        break;
                    case "M":
                        if (위치.Equals("반복") && 잔고.반복M.Equals("X")) str_ = "M";
                        break;
                    case "N":
                        if (위치.Equals("반복") && 잔고.반복N.Equals("X")) str_ = "N";
                        break;
                }

                return str_;
            }

            return result;
        }
        public static string 요일가져오기()
        {
            string 요일 = "";
            DateTime nowDt = DateTime.Now;

            if (nowDt.DayOfWeek == DayOfWeek.Monday)
                요일 = "월요일";
            else if (nowDt.DayOfWeek == DayOfWeek.Tuesday)
                요일 = "화요일";
            else if (nowDt.DayOfWeek == DayOfWeek.Wednesday)
                요일 = "수요일";
            else if (nowDt.DayOfWeek == DayOfWeek.Thursday)
                요일 = "목요일";
            else if (nowDt.DayOfWeek == DayOfWeek.Friday)
                요일 = "금요일";
            else if (nowDt.DayOfWeek == DayOfWeek.Saturday)
                요일 = "토요일";
            else if (nowDt.DayOfWeek == DayOfWeek.Sunday)
                요일 = "일요일";
            return 요일;
        }


        public static string 익절그룹(string 위치)
        {
            string 그룹 = "0";

            switch (위치)
            {
                case "미수금정리":
                    if (Properties.Settings.Default.CB_미수금정리_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_미수금정리_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_미수금정리_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_미수금정리_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_미수금정리_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_미수금정리_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_미수금정리_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_미수금정리_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_미수금정리_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_미수금정리_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_미수금정리_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_미수금정리_L) 그룹 = 그룹 + "L";
                    break;

                case "익절":
                    if (Properties.Settings.Default.CB_IK_group_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_IK_group_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_IK_group_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_IK_group_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_IK_group_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_IK_group_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_IK_group_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_IK_group_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_IK_group_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_IK_group_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_IK_group_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_IK_group_L) 그룹 = 그룹 + "L";
                    break;

                case "손절":
                    if (Properties.Settings.Default.CB_손절_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_손절_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_손절_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_손절_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_손절_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_손절_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_손절_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_손절_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_손절_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_손절_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_손절_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_손절_L) 그룹 = 그룹 + "L";
                    break;

                case "특정시간_청산":
                    if (Properties.Settings.Default.CB_특정시간_계좌청산_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_특정시간_계좌청산_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_특정시간_계좌청산_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_특정시간_계좌청산_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_특정시간_계좌청산_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_특정시간_계좌청산_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_특정시간_계좌청산_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_특정시간_계좌청산_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_특정시간_계좌청산_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_특정시간_계좌청산_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_특정시간_계좌청산_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_특정시간_계좌청산_L) 그룹 = 그룹 + "L";
                    break;

                case "실현손익_청산":
                    if (Properties.Settings.Default.CB_실현손익_계좌청산_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_실현손익_계좌청산_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_실현손익_계좌청산_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_실현손익_계좌청산_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_실현손익_계좌청산_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_실현손익_계좌청산_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_실현손익_계좌청산_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_실현손익_계좌청산_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_실현손익_계좌청산_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_실현손익_계좌청산_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_실현손익_계좌청산_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_실현손익_계좌청산_L) 그룹 = 그룹 + "L";
                    break;

                case "예상손실_청산":
                    if (Properties.Settings.Default.CB_예상손실_계좌청산_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_예상손실_계좌청산_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_예상손실_계좌청산_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_예상손실_계좌청산_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_예상손실_계좌청산_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_예상손실_계좌청산_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_예상손실_계좌청산_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_예상손실_계좌청산_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_예상손실_계좌청산_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_예상손실_계좌청산_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_예상손실_계좌청산_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_예상손실_계좌청산_L) 그룹 = 그룹 + "L";
                    break;

                case "예상수익_청산":
                    if (Properties.Settings.Default.CB_예상수익_계좌청산_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_예상수익_계좌청산_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_예상수익_계좌청산_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_예상수익_계좌청산_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_예상수익_계좌청산_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_예상수익_계좌청산_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_예상수익_계좌청산_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_예상수익_계좌청산_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_예상수익_계좌청산_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_예상수익_계좌청산_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_예상수익_계좌청산_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_예상수익_계좌청산_L) 그룹 = 그룹 + "L";
                    break;

                case "잔고시간청산_A":
                    if (Properties.Settings.Default.CB_시간청산A_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_시간청산A_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_시간청산A_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_시간청산A_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_시간청산A_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_시간청산A_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_시간청산A_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_시간청산A_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_시간청산A_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_시간청산A_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_시간청산A_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_시간청산A_L) 그룹 = 그룹 + "L";
                    break;

                case "잔고시간청산_B":
                    if (Properties.Settings.Default.CB_시간청산B_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_시간청산B_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_시간청산B_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_시간청산B_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_시간청산B_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_시간청산B_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_시간청산B_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_시간청산B_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_시간청산B_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_시간청산B_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_시간청산B_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_시간청산B_L) 그룹 = 그룹 + "L";
                    break;

                case "잔고시간청산_C":
                    if (Properties.Settings.Default.CB_시간청산C_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_시간청산C_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_시간청산C_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_시간청산C_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_시간청산C_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_시간청산C_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_시간청산C_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_시간청산C_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_시간청산C_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_시간청산C_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_시간청산C_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_시간청산C_L) 그룹 = 그룹 + "L";
                    break;

                case "반복_A":
                    if (Properties.Settings.Default.CB_Repeat_A_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_A_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_A_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_A_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_A_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_A_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_A_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_A_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_A_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_A_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_A_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_A_L) 그룹 = 그룹 + "L";
                    break;

                case "반복_B":
                    if (Properties.Settings.Default.CB_Repeat_B_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_B_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_B_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_B_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_B_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_B_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_B_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_B_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_B_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_B_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_B_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_B_L) 그룹 = 그룹 + "L";
                    break;

                case "반복_C":
                    if (Properties.Settings.Default.CB_Repeat_C_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_C_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_C_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_C_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_C_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_C_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_C_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_C_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_C_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_C_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_C_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_C_L) 그룹 = 그룹 + "L";
                    break;


                case "반복_D":
                    if (Properties.Settings.Default.CB_Repeat_D_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_D_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_D_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_D_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_D_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_D_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_D_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_D_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_D_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_D_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_D_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_D_L) 그룹 = 그룹 + "L";
                    break;

                case "반복_E":
                    if (Properties.Settings.Default.CB_Repeat_E_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_E_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_E_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_E_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_E_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_E_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_E_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_E_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_E_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_E_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_E_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_E_L) 그룹 = 그룹 + "L";
                    break;

                case "반복_F":
                    if (Properties.Settings.Default.CB_Repeat_F_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_F_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_F_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_F_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_F_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_F_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_F_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_F_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_F_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_F_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_F_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_F_L) 그룹 = 그룹 + "L";
                    break;

                case "반복_G":
                    if (Properties.Settings.Default.CB_Repeat_G_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_G_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_G_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_G_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_G_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_G_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_G_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_G_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_G_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_G_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_G_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_G_L) 그룹 = 그룹 + "L";
                    break;

                case "반복_H":
                    if (Properties.Settings.Default.CB_Repeat_H_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_H_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_H_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_H_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_H_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_H_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_H_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_H_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_H_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_H_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_H_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_H_L) 그룹 = 그룹 + "L";
                    break;

                case "반복_I":
                    if (Properties.Settings.Default.CB_Repeat_I_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_I_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_I_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_I_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_I_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_I_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_I_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_I_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_I_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_I_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_I_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_I_L) 그룹 = 그룹 + "L";
                    break;

                case "반복_J":
                    if (Properties.Settings.Default.CB_Repeat_J_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_J_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_J_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_J_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_J_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_J_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_J_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_J_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_J_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_J_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_J_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_J_L) 그룹 = 그룹 + "L";
                    break;

                case "반복_K":
                    if (Properties.Settings.Default.CB_Repeat_K_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_K_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_K_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_K_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_K_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_K_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_K_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_K_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_K_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_K_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_K_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_K_L) 그룹 = 그룹 + "L";
                    break;

                case "반복_L":
                    if (Properties.Settings.Default.CB_Repeat_L_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_L_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_L_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_L_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_L_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_L_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_L_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_L_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_L_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_L_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_L_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_L_L) 그룹 = 그룹 + "L";
                    break;

                case "반복_M":
                    if (Properties.Settings.Default.CB_Repeat_M_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_M_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_M_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_M_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_M_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_M_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_M_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_M_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_M_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_M_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_M_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_M_L) 그룹 = 그룹 + "L";
                    break;

                case "반복_N":
                    if (Properties.Settings.Default.CB_Repeat_N_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Repeat_N_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Repeat_N_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Repeat_N_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Repeat_N_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Repeat_N_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Repeat_N_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Repeat_N_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Repeat_N_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Repeat_N_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Repeat_N_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Repeat_N_L) 그룹 = 그룹 + "L";
                    break;

                case "Day_A":
                    if (Properties.Settings.Default.CB_day_A_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_day_A_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_day_A_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_day_A_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_day_A_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_day_A_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_day_A_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_day_A_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_day_A_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_day_A_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_day_A_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_day_A_L) 그룹 = 그룹 + "L";
                    break;

                case "Day_B":
                    if (Properties.Settings.Default.CB_day_B_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_day_B_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_day_B_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_day_B_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_day_B_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_day_B_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_day_B_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_day_B_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_day_B_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_day_B_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_day_B_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_day_B_L) 그룹 = 그룹 + "L";
                    break;

                case "Day_C":
                    if (Properties.Settings.Default.CB_day_C_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_day_C_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_day_C_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_day_C_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_day_C_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_day_C_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_day_C_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_day_C_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_day_C_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_day_C_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_day_C_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_day_C_L) 그룹 = 그룹 + "L";
                    break;

                case "Day_D":
                    if (Properties.Settings.Default.CB_day_D_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_day_D_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_day_D_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_day_D_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_day_D_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_day_D_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_day_D_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_day_D_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_day_D_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_day_D_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_day_D_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_day_D_L) 그룹 = 그룹 + "L";
                    break;

                case "Day_E":
                    if (Properties.Settings.Default.CB_day_E_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_day_E_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_day_E_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_day_E_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_day_E_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_day_E_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_day_E_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_day_E_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_day_E_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_day_E_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_day_E_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_day_E_L) 그룹 = 그룹 + "L";
                    break;

                case "Day_F":
                    if (Properties.Settings.Default.CB_day_F_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_day_F_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_day_F_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_day_F_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_day_F_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_day_F_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_day_F_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_day_F_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_day_F_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_day_F_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_day_F_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_day_F_L) 그룹 = 그룹 + "L";
                    break;

                case "Cut_A":
                    if (Properties.Settings.Default.CB_Cut_A_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Cut_A_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Cut_A_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Cut_A_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Cut_A_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Cut_A_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Cut_A_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Cut_A_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Cut_A_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Cut_A_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Cut_A_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Cut_A_L) 그룹 = 그룹 + "L";
                    break;

                case "Cut_B":
                    if (Properties.Settings.Default.CB_Cut_B_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Cut_B_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Cut_B_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Cut_B_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Cut_B_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Cut_B_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Cut_B_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Cut_B_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Cut_B_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Cut_B_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Cut_B_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Cut_B_L) 그룹 = 그룹 + "L";
                    break;

                case "Cut_C":
                    if (Properties.Settings.Default.CB_Cut_C_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Cut_C_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Cut_C_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Cut_C_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Cut_C_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Cut_C_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Cut_C_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Cut_C_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Cut_C_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Cut_C_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Cut_C_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Cut_C_L) 그룹 = 그룹 + "L";
                    break;

                case "리밸_A":
                    if (Properties.Settings.Default.CB_rebalance_A_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_rebalance_A_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_rebalance_A_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_rebalance_A_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_rebalance_A_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_rebalance_A_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_rebalance_A_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_rebalance_A_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_rebalance_A_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_rebalance_A_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_rebalance_A_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_rebalance_A_L) 그룹 = 그룹 + "L";
                    break;

                case "리밸_B":
                    if (Properties.Settings.Default.CB_rebalance_B_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_rebalance_B_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_rebalance_B_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_rebalance_B_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_rebalance_B_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_rebalance_B_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_rebalance_B_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_rebalance_B_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_rebalance_B_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_rebalance_B_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_rebalance_B_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_rebalance_B_L) 그룹 = 그룹 + "L";
                    break;

                case "리밸_C":
                    if (Properties.Settings.Default.CB_rebalance_C_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_rebalance_C_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_rebalance_C_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_rebalance_C_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_rebalance_C_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_rebalance_C_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_rebalance_C_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_rebalance_C_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_rebalance_C_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_rebalance_C_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_rebalance_C_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_rebalance_C_L) 그룹 = 그룹 + "L";
                    break;

                case "리밸_D":
                    if (Properties.Settings.Default.CB_rebalance_D_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_rebalance_D_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_rebalance_D_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_rebalance_D_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_rebalance_D_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_rebalance_D_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_rebalance_D_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_rebalance_D_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_rebalance_D_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_rebalance_D_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_rebalance_D_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_rebalance_D_L) 그룹 = 그룹 + "L";
                    break;

                case "리밸_E":
                    if (Properties.Settings.Default.CB_rebalance_E_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_rebalance_E_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_rebalance_E_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_rebalance_E_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_rebalance_E_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_rebalance_E_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_rebalance_E_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_rebalance_E_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_rebalance_E_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_rebalance_E_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_rebalance_E_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_rebalance_E_L) 그룹 = 그룹 + "L";
                    break;

                case "리밸_F":
                    if (Properties.Settings.Default.CB_rebalance_F_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_rebalance_F_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_rebalance_F_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_rebalance_F_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_rebalance_F_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_rebalance_F_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_rebalance_F_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_rebalance_F_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_rebalance_F_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_rebalance_F_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_rebalance_F_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_rebalance_F_L) 그룹 = 그룹 + "L";
                    break;

                case "리밸_G":
                    if (Properties.Settings.Default.CB_rebalance_G_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_rebalance_G_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_rebalance_G_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_rebalance_G_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_rebalance_G_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_rebalance_G_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_rebalance_G_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_rebalance_G_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_rebalance_G_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_rebalance_G_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_rebalance_G_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_rebalance_G_L) 그룹 = 그룹 + "L";
                    break;

                case "잔고청산_A":
                    if (Properties.Settings.Default.CB_Liquidation_A_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Liquidation_A_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Liquidation_A_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Liquidation_A_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Liquidation_A_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Liquidation_A_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Liquidation_A_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Liquidation_A_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Liquidation_A_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Liquidation_A_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Liquidation_A_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Liquidation_A_L) 그룹 = 그룹 + "L";
                    break;

                case "잔고청산_B":
                    if (Properties.Settings.Default.CB_Liquidation_B_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Liquidation_B_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Liquidation_B_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Liquidation_B_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Liquidation_B_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Liquidation_B_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Liquidation_B_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Liquidation_B_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Liquidation_B_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Liquidation_B_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Liquidation_B_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Liquidation_B_L) 그룹 = 그룹 + "L";
                    break;

                case "잔고청산_C":
                    if (Properties.Settings.Default.CB_Liquidation_C_A) 그룹 = 그룹 + "A";
                    if (Properties.Settings.Default.CB_Liquidation_C_B) 그룹 = 그룹 + "B";
                    if (Properties.Settings.Default.CB_Liquidation_C_C) 그룹 = 그룹 + "C";
                    if (Properties.Settings.Default.CB_Liquidation_C_D) 그룹 = 그룹 + "D";
                    if (Properties.Settings.Default.CB_Liquidation_C_E) 그룹 = 그룹 + "E";
                    if (Properties.Settings.Default.CB_Liquidation_C_F) 그룹 = 그룹 + "F";
                    if (Properties.Settings.Default.CB_Liquidation_C_G) 그룹 = 그룹 + "G";
                    if (Properties.Settings.Default.CB_Liquidation_C_H) 그룹 = 그룹 + "H";
                    if (Properties.Settings.Default.CB_Liquidation_C_I) 그룹 = 그룹 + "I";
                    if (Properties.Settings.Default.CB_Liquidation_C_J) 그룹 = 그룹 + "J";
                    if (Properties.Settings.Default.CB_Liquidation_C_K) 그룹 = 그룹 + "K";
                    if (Properties.Settings.Default.CB_Liquidation_C_L) 그룹 = 그룹 + "L";
                    break;
            }

            return 그룹;
        }


        public static string 그룹변환(int num)
        {
            string 그룹 = "0";

            switch (num)
            {
                case 1:
                    그룹 = "A";
                    break;
                case 2:
                    그룹 = "B";
                    break;
                case 3:
                    그룹 = "C";
                    break;
                case 4:
                    그룹 = "D";
                    break;
                case 5:
                    그룹 = "E";
                    break;
                case 6:
                    그룹 = "F";
                    break;
                case 7:
                    그룹 = "G";
                    break;
                case 8:
                    그룹 = "H";
                    break;
                case 9:
                    그룹 = "I";
                    break;
                case 10:
                    그룹 = "J";
                    break;
                case 11:
                    그룹 = "K";
                    break;
                case 12:
                    그룹 = "L";
                    break;
            }
            return 그룹;
        }


        public static string 오류코드(int orderResult)
        {
            string result = "";

            switch (orderResult)
            {
                case 1:
                    result = "정상처리";
                    break;
                case 0:
                    result = "정상처리";
                    break;
                case -10:
                    result = "실패";
                    break;
                case -11:
                    result = "조건번호 없슴";
                    break;
                case -12:
                    result = "조건번호와 조건식 불일치";
                    break;
                case -13:
                    result = "조건검색 조회요청 초과";
                    break;
                case -100:
                    result = "사용자정보교환 실패";
                    break;
                case -101:
                    result = "서버 접속 실패";
                    break;
                case -102:
                    result = "버전처리 실패";
                    break;
                case -103:
                    result = "개인방화벽 실패";
                    break;
                case -104:
                    result = "메모리 보호실패";
                    break;
                case -105:
                    result = "함수입력값 오류";
                    break;
                case -106:
                    result = "통신연결 종료";
                    break;
                case -107:
                    result = "보안모듈 오류";
                    break;
                case -108:
                    result = "공인인증 로그인 필요";
                    break;
                case -200:
                    result = "시세조회 과부하";
                    break;
                case -201:
                    result = "전문작성 초기화 실패.";
                    break;
                case -202:
                    result = "전문작성 입력값 오류.";
                    break;
                case -203:
                    result = "데이터 없음.";
                    break;
                case -204:
                    result = "조회가능한 종목수 초과. 한번에 조회 가능한 종목개수는 최대 100종목.";
                    break;
                case -205:
                    result = "데이터 수신 실패";
                    break;
                case -206:
                    result = "조회가능한 FID수 초과. 한번에 조회 가능한 FID개수는 최대 100개.";
                    break;
                case -207:
                    result = "실시간 해제오류";
                    break;
                case -209:
                    result = "시세조회제한";
                    break;
                case -300:
                    result = "입력값 오류";
                    break;
                case -301:
                    result = "계좌비밀번호 없음.";
                    break;
                case -302:
                    result = "타인계좌 사용오류.";
                    break;
                case -303:
                    result = "주문가격이 주문착오 금액기준 초과.";
                    break;
                case -304:
                    result = "주문가격이 주문착오 금액기준 초과.";
                    break;
                case -305:
                    result = "주문수량이 총발행주수의 1% 초과오류.";
                    break;
                case -306:
                    result = "주문수량은 총발행주수의 3% 초과오류.";
                    break;
                case -307:
                    result = "주문전송 실패";
                    break;
                case -308:
                    result = "주문전송 과부하";
                    break;
                case -309:
                    result = "주문수량 300계약 초과.";
                    break;
                case -310:
                    result = "주문수량 500계약 초과.";
                    break;
                case -311:
                    result = "주문전송제한 과부하";
                    break;
                case -340:
                    result = "계좌정보 없음.";
                    break;
                case -500:
                    result = "종목코드 없음.";
                    break;
            }
            return result;
        }



    }












}
