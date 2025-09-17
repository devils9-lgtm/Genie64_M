using System;
using System.Collections.Generic;
using System.Linq;

namespace 지니_64
{
    public static class Tab_Basic
    {
        public static void New_Buy(string strType, string itemCode, string condition)
        {
            bool ID_A = false;
            bool ID_B = false;
            bool ID_C = false;

            if (strType.Equals("I"))
            {
                if (Properties.Settings.Default.combo_new_or_A == 0) ID_A = true;
                if (Properties.Settings.Default.combo_new_or_B == 0 || Properties.Settings.Default.combo_new_or_B == 1) ID_B = true;
                if (Properties.Settings.Default.combo_new_or_C == 0 || Properties.Settings.Default.combo_new_or_C == 1) ID_C = true;
            }
            else if (strType.Equals("D"))
            {
                if (Properties.Settings.Default.combo_new_or_A == 1) ID_A = true;
                if (Properties.Settings.Default.combo_new_or_B == 2 || Properties.Settings.Default.combo_new_or_B == 3) ID_B = true;
                if (Properties.Settings.Default.combo_new_or_C == 2 || Properties.Settings.Default.combo_new_or_C == 3) ID_C = true;
            }

            if (Properties.Settings.Default.combo_new_condition_A.Equals(condition) && Properties.Settings.Default.CB_new_A)
            {
                NewCatch_A 확인 = Form1.NewCatch_List_A.Find(o => o.code.Equals(itemCode));
                if (확인 == null)
                {
                    if (ID_A) // 진입추가
                    {
                        NewCatch_A 추가 = new NewCatch_A(condition, itemCode, "진입", 0, DateTime.Now);
                        Form1.NewCatch_List_A.Add(추가);

                        if (Properties.Settings.Default.MTB_new_delay_A == 0) 매수검색식_A(추가);
                    }
                }
                else
                {
                    if (ID_A)
                    {
                        확인.state = "재진입";
                        확인.timer = 0;
                        if (Properties.Settings.Default.MTB_new_delay_A == 0) 매수검색식_A(확인);
                    }
                    else
                    {
                        확인.state = "이탈";
                        확인.timer = 0;
                    }
                }
            }

            if (Properties.Settings.Default.combo_new_condition_B.Equals(condition) && Properties.Settings.Default.CB_new_B)
            {
                NewCatch_B 확인 = Form1.NewCatch_List_B.Find(o => o.code.Equals(itemCode));
                if (확인 == null)
                {
                    if (ID_B)   // 진입추가
                    {
                        NewCatch_B 추가 = new NewCatch_B(condition, itemCode, "진입", 0, DateTime.Now);
                        Form1.NewCatch_List_B.Add(추가);
                        if (Properties.Settings.Default.MTB_new_delay_B == 0) 매수검색식_B(추가);
                    }
                }
                else
                {
                    if (ID_B)
                    {
                        확인.state = "재진입";
                        확인.timer = 0;
                        if (Properties.Settings.Default.MTB_new_delay_B == 0) 매수검색식_B(확인);
                    }
                    else
                    {
                        확인.state = "이탈";
                        확인.timer = 0;
                        Form1.form1.AnB_List.Remove(itemCode);
                        Form1.form1.AnBnC_List.Remove(itemCode);
                    }
                }
            }

            if (Properties.Settings.Default.combo_new_condition_C.Equals(condition) && Properties.Settings.Default.CB_new_C)
            {
                NewCatch_C 확인 = Form1.NewCatch_List_C.Find(o => o.code.Equals(itemCode));
                if (확인 == null)
                {
                    if (ID_C)  // 진입추가
                    {
                        NewCatch_C 추가 = new NewCatch_C(condition, itemCode, "진입", 0, DateTime.Now);
                        Form1.NewCatch_List_C.Add(추가);
                        if (Properties.Settings.Default.MTB_new_delay_C == 0) 매수검색식_C(추가);
                    }
                }
                else
                {
                    if (ID_C)
                    {
                        확인.state = "재진입";
                        확인.timer = 0;
                        if (Properties.Settings.Default.MTB_new_delay_C == 0) 매수검색식_C(확인);
                    }
                    else
                    {
                        확인.state = "이탈";
                        확인.timer = 0;
                        Form1.form1.AnB_List.Remove(itemCode);
                        Form1.form1.AnBnC_List.Remove(itemCode);
                    }
                }
            }
        }

        public static bool 신규매수진행(Market_Item Market, string 검색식, string 위치)
        {
            bool result = true;

            if (!Form1.신규매수정지)
            {
                if (Form1.신규count + Form1.stockBalanceList.Count < Properties.Settings.Default.TB_setjango)
                {
                    if (Properties.Settings.Default.CB_신규횟수제한)
                    {
                        if (Properties.Settings.Default.신규횟수 >= Properties.Settings.Default.TB_신규횟수제한)
                        {
                            거부메세지("횟수제한");
                            result = false;
                        }
                    }

                    int 주문개수()
                    {
                        List<JumunItem> list = Form1.JumunItem_List.FindAll(o => o.검색식.Contains(위치));
                        return list.Count;
                    }

                    if (result && 위치.Equals("신규_A")) { if (Form1.신규개수A + 주문개수() >= Properties.Settings.Default.TB_잔고개수_신규A) { 거부메세지("신규개수"); result = false; } }
                    if (result && 위치.Equals("신규_B")) { if (Form1.신규개수B + 주문개수() >= Properties.Settings.Default.TB_잔고개수_신규B) { 거부메세지("신규개수"); result = false; } }
                    if (result && 위치.Equals("신규_C")) { if (Form1.신규개수C + 주문개수() >= Properties.Settings.Default.TB_잔고개수_신규C) { 거부메세지("신규개수"); result = false; } }

                    if (result && Properties.Settings.Default.CB_계좌매입비_매수제한)
                    {
                        if (Method.매입비매수제한(Properties.Settings.Default.CBB_계좌매입비_제한선택).Contains("신규"))
                        {
                            거부메세지("매입비매수제한");
                            result = false;
                        }
                    }

                    if (result && !Jisu_linkage.업종별지수연동("신규", Market))
                    {
                        거부메세지("업종별지수연동");
                        result = false;
                    }
                }
                else
                {
                    거부메세지("잔고수");
                    result = false;
                }
            }
            else
            {
                거부메세지("신규매수정지");
                result = false;
            }

            void 거부메세지(string 신호)
            {
                string 시장 = "코스피";
                if (Market.Market.Equals("D")) 시장 = "코스닥";
                string message = "";

                if (신호.Equals("횟수제한")) message = ("[신규매수 제한 '매수횟수'] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                else if (신호.Equals("신규개수")) message = ("[신규매수 제한 '조건별 잔고제한'] " + 위치 + " -> " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                else if (신호.Equals("잔고수")) message = ("[신규매수 제한 '최대잔고'] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                else if (신호.Equals("매입비매수제한")) message = ("[신규매수 제한 '매입비매수제한'] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                else if (신호.Equals("업종별지수연동")) message = ("[신규매수 제한 '업종별지수연동'] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                else if (신호.Equals("신규매수정지")) message = ("[신규매수 제한 '신규매수정지'] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);

                매매거부_메세지출력(Market.종목명, message);
            }

            return result;
        }

        public static void 매수검색식_A(NewCatch_A 종목)
        {
            string Code = 종목.code;
            string 검색식 = 종목.condition;

            if (!Form1.stockBalanceList.ContainsKey(Code)) //잔고에 없을때
            {
                if (Properties.Settings.Default.CB_new_recatch_A) //재진입 포함 매수 
                {
                    if (종목.state.Contains("진입"))
                    {
                        New_buying(검색식, Code, 0, 종목, null, null);
                    }
                }
                else
                {
                    if (종목.state.Equals("진입")) //최초 진입시에만 매수 
                    {
                        New_buying(검색식, Code, 0, 종목, null, null);
                    }
                }
            }
        }

        public static void 매수검색식_B(NewCatch_B 종목)
        {
            string Code = 종목.code;
            string 검색식 = 종목.condition;
            if (!Form1.stockBalanceList.ContainsKey(Code)) //잔고에 없을때
            {
                if (Properties.Settings.Default.CB_new_recatch_B) //재진입 포함 매수 
                {
                    if (종목.state.Contains("진입"))
                    {
                        New_buying(검색식, Code, 0, null, 종목, null);
                    }
                }
                else
                {
                    if (종목.state.Equals("진입")) //최초 진입시에만 매수 
                    {
                        New_buying(검색식, Code, 0, null, 종목, null);
                    }
                }
            }
        }

        public static void 매수검색식_C(NewCatch_C 종목)
        {
            string Code = 종목.code;
            string 검색식 = 종목.condition;
            if (!Form1.stockBalanceList.ContainsKey(Code)) //잔고에 없을때
            {
                if (Properties.Settings.Default.CB_new_recatch_C) //재진입 포함 매수 
                {
                    if (종목.state.Contains("진입"))
                    {
                        New_buying(검색식, Code, 0, null, null, 종목);
                    }
                }
                else
                {
                    if (종목.state.Equals("진입")) //최초 진입시에만 매수 
                    {
                        New_buying(검색식, Code, 0, null, null, 종목);
                    }
                }
            }
        }

        public static void New_buying(string condition, string itemCode, int nowprice, NewCatch_A 종목A, NewCatch_B 종목B, NewCatch_C 종목C) //신규매수 종목&가동조건 전달
        {
            Market_Item Market = Form1.Market_Item_List[itemCode];

            if (Form1.form1.RB_buy_run.Checked)
            {
                if (Form1.form1.신규매수방법.Equals("AnB"))
                {
                    if (Properties.Settings.Default.combo_new_condition_B.Equals(condition) && !Form1.form1.AnB_List.Contains(itemCode))
                    {
                        Form1.form1.AnB_List.Add(itemCode);
                    }

                    if (Form1.form1.AnB_List.Contains(itemCode) && Properties.Settings.Default.combo_new_condition_A.Equals(condition))
                    {
                        _1_중복확인_매수();
                    }
                }
                else if (Form1.form1.신규매수방법.Equals("AnBnC"))
                {
                    if (Properties.Settings.Default.combo_new_condition_B.Equals(condition) && !Form1.form1.AnB_List.Contains(itemCode))
                    {
                        Form1.form1.AnB_List.Add(itemCode);
                    }
                    if (Properties.Settings.Default.combo_new_condition_C.Equals(condition) && !Form1.form1.AnBnC_List.Contains(itemCode))
                    {
                        Form1.form1.AnBnC_List.Add(itemCode);
                    }

                    if (Form1.form1.AnB_List.Contains(itemCode) && Form1.form1.AnBnC_List.Contains(itemCode) && Properties.Settings.Default.combo_new_condition_A.Equals(condition))
                    {
                        _1_중복확인_매수();
                    }
                }
                else if (Form1.form1.신규매수방법.Equals("AnBoC"))
                {
                    if (Properties.Settings.Default.combo_new_condition_B.Equals(condition))
                    {
                        Form1.form1.AnB_List.Add(itemCode);
                    }
                    if (Form1.form1.AnB_List.Contains(itemCode) && Properties.Settings.Default.combo_new_condition_A.Equals(condition))
                    {
                        _1_중복확인_매수();
                    }
                    if (Properties.Settings.Default.combo_new_condition_C.Equals(condition) && Properties.Settings.Default.combo_new_or_C == 0)
                    {
                        _1_중복확인_매수();
                    }
                }
                else // 그외의것 A B C AoB BoC AoBoC
                {
                    _1_중복확인_매수();
                }

                void _1_중복확인_매수()
                {
                    bool 횟수제한()
                    {
                        bool result = true;

                        if (Form1.신규count + Form1.stockBalanceList.Count >= Properties.Settings.Default.TB_setjango) result = false;

                        if (Properties.Settings.Default.CB_신규횟수제한 && result)
                        {
                            if (Properties.Settings.Default.신규횟수 >= Properties.Settings.Default.TB_신규횟수제한)
                            {
                                result = false;
                            }
                        }
                        return result;
                    }

                    if (횟수제한())
                    {
                        JumunItem Item_Check = Form1.JumunItem_List.Find(o => o.종목코드.Equals(itemCode) && o.검색식.Equals(condition));
                        if (Item_Check == null) //매수진행 종목이 없다 
                        {
                            if (!Form1.stockBalanceList.ContainsKey(itemCode))
                            {
                                if (Properties.Settings.Default.CB_new_rebuy)
                                {
                                    _2_a_매수();
                                }
                                else
                                {
                                    재매수 Item = Form1.Rebuystock_List.Find(o => o.ItemCode.Equals(itemCode));
                                    if (Item == null)
                                    {
                                        _2_a_매수();
                                    }
                                }
                            }

                            void _2_a_매수()
                            {
                                int 현재가 = nowprice;
                                string name = Market.종목명;

                                if (Market.현재가 > 0)
                                {
                                    현재가 = Market.현재가;
                                }

                                int start_time = Properties.Settings.Default.MT_new_start_A;
                                int end_time = Properties.Settings.Default.MT_new_end_A;
                                double value = Properties.Settings.Default.TB_new_value_A;
                                int Jumun = Properties.Settings.Default.combo_new_jumun_A;
                                int Choice = Properties.Settings.Default.combo_new_choice_A;
                                double ratio = Properties.Settings.Default.MT_new_ratio_A;
                                string 위치 = "신규_A";
                                bool Result = false;

                                if (Properties.Settings.Default.CB_new_A && Properties.Settings.Default.combo_new_condition_A.Equals(condition))
                                {
                                    if (신규매수진행(Market, condition, 위치))
                                    {
                                        if (Tab_InterestGroup.관심그룹확인(위치, itemCode))
                                        {
                                            if (종목A.CatchTime.AddSeconds(Properties.Settings.Default.TB_Limit_New_A) > DateTime.Now) Result = true;
                                        }
                                    }
                                }

                                if (Properties.Settings.Default.CB_new_B && Properties.Settings.Default.combo_new_condition_B.Equals(condition))
                                {
                                    위치 = "신규_B";

                                    if (신규매수진행(Market, condition, 위치))
                                    {
                                        start_time = Properties.Settings.Default.MT_new_start_B;
                                        end_time = Properties.Settings.Default.MT_new_end_B;
                                        value = Properties.Settings.Default.TB_new_value_B;
                                        Jumun = Properties.Settings.Default.combo_new_jumun_B;
                                        Choice = Properties.Settings.Default.combo_new_choice_B;
                                        ratio = Properties.Settings.Default.MT_new_ratio_B;

                                        if (Tab_InterestGroup.관심그룹확인(위치, itemCode))
                                        {
                                            if (종목B.CatchTime.AddSeconds(Properties.Settings.Default.TB_Limit_New_B) > DateTime.Now) Result = true;
                                        }
                                    }
                                }

                                if (Properties.Settings.Default.CB_new_C && Properties.Settings.Default.combo_new_condition_C.Equals(condition))
                                {
                                    위치 = "신규_C";

                                    if (신규매수진행(Market, condition, 위치))
                                    {
                                        start_time = Properties.Settings.Default.MT_new_start_C;
                                        end_time = Properties.Settings.Default.MT_new_end_C;
                                        value = Properties.Settings.Default.TB_new_value_C;
                                        Jumun = Properties.Settings.Default.combo_new_jumun_C;
                                        Choice = Properties.Settings.Default.combo_new_choice_C;
                                        ratio = Properties.Settings.Default.MT_new_ratio_C;

                                        if (Tab_InterestGroup.관심그룹확인(위치, itemCode))
                                        {
                                            if (종목C.CatchTime.AddSeconds(Properties.Settings.Default.TB_Limit_New_C) > DateTime.Now) Result = true;
                                        }
                                    }
                                }

                                if (Result)
                                {
                                    string 검색식 = 위치 + " [" + condition + "]";

                                    if (Method.RunTime(start_time, end_time))
                                    {
                                        int 취소시간 = Properties.Settings.Default.MTB_new_canceltime_A;
                                        int 취소N주문 = Properties.Settings.Default.combo_new_cancel_buy_A;
                                        int 반복횟수 = Properties.Settings.Default.MTB_new_repeat_A;

                                        if (Properties.Settings.Default.CB_new_B && 검색식.Contains(Properties.Settings.Default.combo_new_condition_B))
                                        {
                                            취소시간 = Properties.Settings.Default.MTB_new_canceltime_B;
                                            취소N주문 = Properties.Settings.Default.combo_new_cancel_buy_B;
                                            반복횟수 = Properties.Settings.Default.MTB_new_repeat_B;
                                        }

                                        if (Properties.Settings.Default.CB_new_C && 검색식.Contains(Properties.Settings.Default.combo_new_condition_C))
                                        {
                                            취소시간 = Properties.Settings.Default.MTB_new_canceltime_C;
                                            취소N주문 = Properties.Settings.Default.combo_new_cancel_buy_C;
                                            반복횟수 = Properties.Settings.Default.MTB_new_repeat_C;
                                        }

                                        int ScreenNumber = GET.jumunScreen(itemCode);
                                        if (ScreenNumber == 1300)
                                        {
                                            Method.주문초과알림(name);
                                        }
                                        else
                                        {
                                            if (현재가 > 1)
                                            {
                                                if (Method.매매확인_VI_모투가능확인(Market, 1))
                                                {
                                                    신규매수실행(itemCode, name, 검색식, ScreenNumber.ToString(), value, Jumun, Choice, ratio, 현재가, 취소시간, 취소N주문, 반복횟수);
                                                }
                                            }
                                            else
                                            {
                                                신규조회 중복 = Form1.신규조회_List.Find(o => o.ItemCode.Equals(itemCode));
                                                if (중복 == null)
                                                {
                                                    string para = "NEWBUYopt10001^" + 검색식 + "^" + name + "^" + itemCode + "^" + ScreenNumber + "^" + value + "^" + Jumun + "^" + Choice + "^" + ratio + "^" + 취소시간 + "^" + 취소N주문 + "^" + 반복횟수;
                                                    신규조회 Add = new 신규조회(itemCode, para, 0, 검색식);
                                                    Form1.신규조회_List.Add(Add);
                                                    Form1.TR_catch_Item_List.Enqueue(itemCode + ";" + para);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string 시장 = "코스피";
                                        if (Market.Market.Equals("D")) 시장 = "코스닥";

                                        매매거부_메세지출력(name, "[신규매수 제한 '가동시간'] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        public static void 신규매수실행(string ItemCode, string 종목명, string 검색식, string 화면번호, double 주문값, int 주문구분, int 비중선택, double 주문비중, int 현재가, int 취소시간, int 취소N주문, int 반복횟수) //신규매수 주문넣기
        {
            string 시장 = "코스피";
            if (Form1.Market_Item_List[ItemCode].Market.Equals("D")) 시장 = "코스닥";

            if (Properties.Settings.Default.TB_신규주가이상 <= Form1.Market_Item_List[ItemCode].현재가 && Form1.Market_Item_List[ItemCode].현재가 <= Properties.Settings.Default.TB_신규주가이하)
            {
                if (Properties.Settings.Default.TB_신규등락률이상 <= Form1.Market_Item_List[ItemCode].등락율 && Form1.Market_Item_List[ItemCode].등락율 <= Properties.Settings.Default.TB_신규등락률이하)
                {
                    Dictionary<string, Stockbalance> stockBalanceList = Form1.stockBalanceList;   // 잔고 - 보유잔고리스트

                    string HogaGB = "";
                    int 주문가 = 0;
                    int 주문수량 = 0;
                    int 예_주문가 = 0; //예수금 계산 가격


                    if (주문구분 == 0) //  [거래구분]
                    {
                        HogaGB = "03"; // 03 : 시장가
                        주문수량 = Method.주문수량계산(null, 현재가, 주문비중, 비중선택, 1);
                        예_주문가 = 현재가;
                    }
                    else
                    {
                        HogaGB = "00"; // 0 : 지정가
                        주문가 = Method.order_price(주문값, 주문구분, ItemCode, 현재가);

                        주문수량 = Method.주문수량계산(null, 주문가, 주문비중, 비중선택, 1);
                        예_주문가 = 주문가;
                    }

                    if (예수금확인())
                    {
                        if (신규주문중복확인(ItemCode, 검색식))
                        {
                            bool 매매제한()
                            {
                                bool result = true;

                                if (Form1.신규count + Form1.stockBalanceList.Count >= Properties.Settings.Default.TB_setjango)
                                {
                                    매매거부_메세지출력(종목명, "[신규매수 제한 '최대잔고'] " + 시장 + "ㆍ" + 종목명 + "ㆍ" + 검색식);
                                    result = false;
                                }

                                if (Properties.Settings.Default.CB_신규횟수제한 && result)
                                {
                                    if (Properties.Settings.Default.신규횟수 >= Properties.Settings.Default.TB_신규횟수제한)
                                    {
                                        매매거부_메세지출력(종목명, "[신규매수 제한 '매수횟수'] " + 시장 + "ㆍ" + 종목명 + "ㆍ" + 검색식);
                                        result = false;
                                    }
                                }

                                JumunItem 미체결Item = Form1.JumunItem_List.Find(o => o.종목코드.Equals(ItemCode));
                                if (미체결Item != null && 미체결Item.검색식.Contains("신규_"))
                                {
                                    //   Console.WriteLine("중복종목 :: " + Form1.Market_Item_List[ItemCode].Name);
                                    result = false;
                                }

                                return result;
                            }

                            if (매매제한())
                            {
                                Properties.Settings.Default.신규횟수++;
                                Form1.신규count++;

                                if (주문구분 < 4)
                                {
                                    int Order번호 = GET.Order번호();

                                    JumunItem ItemList = new JumunItem(0, 0, 화면번호, ItemCode, 종목명, "++", "---", 검색식, 주문값, 주문구분, 취소시간, 취소N주문, 반복횟수, "", "신규매수", 주문수량, 예_주문가, 1, 주문비중, 비중선택, 취소시간, 현재가, 0, Form1.timenow,
                                    주문수량, true, false, 0, Method.Find_Tik_Cap(현재가, 예_주문가, Form1.Market_Item_List[ItemCode].Market), 현재가, 0, false, 0, Order번호, 0, Form1.NXT_server); // 자동 주문추가 
                                    Form1.JumunItem_List.Add(ItemList);

                                    DataManagement.예수금업데이트("매수", 예_주문가, 주문수량, "주문", ItemCode);
                                    Form1.que_order(화면번호, 종목명, 1, ItemCode, 주문수량, 주문가, HogaGB, "++", 검색식, Order번호);

                                    스켈핑등록(검색식, 화면번호, ItemCode, 주문수량);
                                }
                                else
                                {
                                    Tab_AccountManagement.분할주문("신규매수", 1, 주문구분, ItemCode, 종목명, 주문수량, 현재가, 검색식, 취소시간);
                                }

                                if (검색식.Contains("신규_A"))
                                {
                                    NewCatch_A Itam = Form1.NewCatch_List_A.Find(o => o.code.Equals(ItemCode));

                                    if (Itam != null)
                                    {
                                        Itam.state = "이탈";
                                        Itam.timer = 0;
                                    }
                                }
                                else if (검색식.Contains("신규_B"))
                                {
                                    NewCatch_B Itam = Form1.NewCatch_List_B.Find(o => o.code.Equals(ItemCode));

                                    if (Itam != null)
                                    {
                                        Itam.state = "이탈";
                                        Itam.timer = 0;
                                    }
                                }
                                else
                                {
                                    NewCatch_C Itam = Form1.NewCatch_List_C.Find(o => o.code.Equals(ItemCode));

                                    if (Itam != null)
                                    {
                                        Itam.state = "이탈";
                                        Itam.timer = 0;
                                    }
                                }
                            }
                        }
                    }

                    bool 예수금확인()
                    {
                        bool result = false;

                        if (주문수량 >= 1)
                        {
                            if (Properties.Settings.Default.CB_misu)
                            {
                                if (Form1.매수증거금)
                                    result = true;
                                else
                                    result = false;
                            }
                            else
                            {
                                for (int i = 0; i > -1; i++)
                                {
                                    if (Form1.Acc[0].추정D2 < 예_주문가)
                                    {
                                        if (Form1.Market_Item_List[ItemCode].매수증거금알림)
                                        {
                                            Form1.Market_Item_List[ItemCode].매수증거금알림 = false;

                                            매매거부_메세지출력(종목명, "[신규매수 제한 '예수금부족'] " + 시장 + "ㆍ" + 종목명 + "ㆍ" + 검색식);
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        if (Form1.Acc[0].추정D2 > 예_주문가 * (주문수량 - i))
                                        {
                                            if ((주문수량 - i) > 0)
                                            {
                                                주문수량 = 주문수량 - i;
                                                result = true;
                                            }
                                            else
                                            {
                                                if (Form1.Market_Item_List[ItemCode].매수증거금알림)
                                                {
                                                    Form1.Market_Item_List[ItemCode].매수증거금알림 = false;

                                                    매매거부_메세지출력(종목명, "[신규매수 제한 '예수금부족'] " + 시장 + "ㆍ" + 종목명 + "ㆍ" + 검색식);

                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Form1.Market_Item_List[ItemCode].매수증거금알림)
                            {
                                Form1.Market_Item_List[ItemCode].매수증거금알림 = false;

                                매매거부_메세지출력(종목명, "[신규매수 제한 '매수설정금'] " + 시장 + "ㆍ" + 종목명 + "ㆍ" + 검색식);
                            }
                        }

                        return result;
                    }
                }
                else
                {
                    매매거부_메세지출력(종목명, "[신규매수 제한 '등락률범위'] " + 시장 + "ㆍ" + 종목명 + "ㆍ" + 검색식 + " 등락률(" + Form1.Market_Item_List[ItemCode].등락율 + "%) 제한 등락률(" + Properties.Settings.Default.TB_신규등락률이상 + " ~ " + Properties.Settings.Default.TB_신규등락률이하 + ")");
                }
            }
            else
            {
                매매거부_메세지출력(종목명, "[신규매수 제한 '주가범위'] " + 시장 + "ㆍ" + 종목명 + "ㆍ" + 검색식 + " 주가(" + Form1.Market_Item_List[ItemCode].현재가 + ")이 제한 주가(" + Properties.Settings.Default.TB_신규주가이상 + " ~ " + Properties.Settings.Default.TB_신규주가이하 + ")");
            }
        }

        public static void 매매거부_메세지출력(string 종목명, string 메세지)
        {
            if (!Form1.form1.신규거부_List.Contains(종목명))
            {
                Form1.Error_Log("");
                Form1.Error_Log(메세지);
                Form1.Error_Log("");
                Form1.동작_Log("");
                Form1.동작_Log(메세지);
                Form1.동작_Log("");

                Form1.form1.신규거부_List.Add(종목명);
            }
        }

        public static bool 신규주문중복확인(string 코드, string 검색식)
        {
            bool result = false;

            bool CB_recatch = Properties.Settings.Default.CB_new_recatch_A;
            bool CB_익절재매수 = Properties.Settings.Default.CB_익절재매수A;
            string New_condition = Properties.Settings.Default.combo_new_condition_A;

            if (검색식.Contains("신규_B"))
            {
                CB_recatch = Properties.Settings.Default.CB_new_recatch_B;
                CB_익절재매수 = Properties.Settings.Default.CB_익절재매수B;
                New_condition = Properties.Settings.Default.combo_new_condition_B;
            }
            else if (검색식.Contains("신규_C"))
            {
                CB_recatch = Properties.Settings.Default.CB_new_recatch_C;
                CB_익절재매수 = Properties.Settings.Default.CB_익절재매수C;
                New_condition = Properties.Settings.Default.combo_new_condition_C;
            }

            string NewBuyItem = Form1.form1.NewBuyItem_List.Find(o => o.Equals(코드));

            if (NewBuyItem == null)
            {
                Form1.form1.NewBuyItem_List.Add(코드);
                result = true;
            }
            else
            {
                if (CB_recatch)
                {
                    result = true;

                    if (CB_익절재매수)
                    {
                        재매수 종목 = Form1.Rebuystock_List.Find(o => o.ItemCode.Equals(코드));
                        if (종목 != null)
                        {
                            if (종목.결과.Equals("수익"))
                                result = true;
                            else
                                result = false;
                        }
                    }
                }
            }

            return result;
        }


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////       잔고익절 메소드모음         ////////////////

        public static void 잔고_익절(Stockbalance 잔고)
        {
            string sender = "CB_ik_one_A";
            bool 일회 = false;
            double 익절값 = Properties.Settings.Default.TB_ik_son_A;
            int 익절단위 = Properties.Settings.Default.combo_ik_A;
            double 익절비중 = Properties.Settings.Default.TB_ik_son_ratio_A;
            int 익절비중단위 = Properties.Settings.Default.combo_ik_ratio_A;
            double 주문값 = Properties.Settings.Default.TB_ik_value_A;
            int 주문구분 = Properties.Settings.Default.combo_ik_jumun_A;

            if (잔고.매매가능 && 잔고.주문가능수량 > 0 && Form1.재시작 && !잔고.매도정지)
            {
                if (Properties.Settings.Default.CB_ik_A)
                {
                    if (잔고.익절A)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (Properties.Settings.Default.CB_ik_one_A && 잔고.일회A.Equals("on"))
                        {
                            일회 = true;
                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (Properties.Settings.Default.CB_ik_B)
                {
                    sender = "CB_ik_one_B";
                    익절값 = Properties.Settings.Default.TB_ik_son_B;
                    익절단위 = Properties.Settings.Default.combo_ik_B;
                    익절비중 = Properties.Settings.Default.TB_ik_son_ratio_B;
                    익절비중단위 = Properties.Settings.Default.combo_ik_ratio_B;
                    주문값 = Properties.Settings.Default.TB_ik_value_B;
                    주문구분 = Properties.Settings.Default.combo_ik_jumun_B;

                    if (잔고.익절B)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (Properties.Settings.Default.CB_ik_one_B && 잔고.일회B.Equals("on"))
                        {
                            일회 = true;
                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (Properties.Settings.Default.CB_ik_C)
                {
                    sender = "CB_ik_one_C";
                    익절값 = Properties.Settings.Default.TB_ik_son_C;
                    익절단위 = Properties.Settings.Default.combo_ik_C;
                    익절비중 = Properties.Settings.Default.TB_ik_son_ratio_C;
                    익절비중단위 = Properties.Settings.Default.combo_ik_ratio_C;
                    주문값 = Properties.Settings.Default.TB_ik_value_C;
                    주문구분 = Properties.Settings.Default.combo_ik_jumun_C;

                    if (잔고.익절C)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (Properties.Settings.Default.CB_ik_one_C && 잔고.일회C.Equals("on"))
                        {
                            일회 = true;
                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (Properties.Settings.Default.CB_ik_D)
                {
                    sender = "CB_ik_one_D";
                    익절값 = Properties.Settings.Default.TB_ik_son_D;
                    익절단위 = Properties.Settings.Default.combo_ik_D;
                    익절비중 = Properties.Settings.Default.TB_ik_son_ratio_D;
                    익절비중단위 = Properties.Settings.Default.combo_ik_ratio_D;
                    주문값 = Properties.Settings.Default.TB_ik_value_D;
                    주문구분 = Properties.Settings.Default.combo_ik_jumun_D;

                    if (잔고.익절D)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (Properties.Settings.Default.CB_ik_one_D && 잔고.일회D.Equals("on"))
                        {
                            일회 = true;

                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (Properties.Settings.Default.CB_ik_E)
                {
                    sender = "CB_ik_one_E";
                    익절값 = Properties.Settings.Default.TB_ik_son_E;
                    익절단위 = Properties.Settings.Default.combo_ik_E;
                    익절비중 = Properties.Settings.Default.TB_ik_son_ratio_E;
                    익절비중단위 = Properties.Settings.Default.combo_ik_ratio_E;
                    주문값 = Properties.Settings.Default.TB_ik_value_E;
                    주문구분 = Properties.Settings.Default.combo_ik_jumun_E;

                    if (잔고.익절E)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (Properties.Settings.Default.CB_ik_one_E && 잔고.일회E.Equals("on"))
                        {
                            일회 = true;

                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (Properties.Settings.Default.CB_ik_F)
                {
                    sender = "CB_ik_one_F";
                    익절값 = Properties.Settings.Default.TB_ik_son_F;
                    익절단위 = Properties.Settings.Default.combo_ik_F;
                    익절비중 = Properties.Settings.Default.TB_ik_son_ratio_F;
                    익절비중단위 = Properties.Settings.Default.combo_ik_ratio_F;
                    주문값 = Properties.Settings.Default.TB_ik_value_F;
                    주문구분 = Properties.Settings.Default.combo_ik_jumun_F;

                    if (잔고.익절F)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (Properties.Settings.Default.CB_ik_one_F && 잔고.일회F.Equals("on"))
                        {
                            일회 = true;

                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (Properties.Settings.Default.CB_ik_G)
                {
                    sender = "CB_ik_one_G";
                    익절값 = Properties.Settings.Default.TB_ik_son_G;
                    익절단위 = Properties.Settings.Default.combo_ik_G;
                    익절비중 = Properties.Settings.Default.TB_ik_son_ratio_G;
                    익절비중단위 = Properties.Settings.Default.combo_ik_ratio_G;
                    주문값 = Properties.Settings.Default.TB_ik_value_G;
                    주문구분 = Properties.Settings.Default.combo_ik_jumun_G;

                    if (잔고.익절G)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (Properties.Settings.Default.CB_ik_one_G && 잔고.일회G.Equals("on"))
                        {
                            일회 = true;

                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (Properties.Settings.Default.CB_ik_H)
                {
                    sender = "CB_ik_one_H";
                    익절값 = Properties.Settings.Default.TB_ik_son_H;
                    익절단위 = Properties.Settings.Default.combo_ik_H;
                    익절비중 = Properties.Settings.Default.TB_ik_son_ratio_H;
                    익절비중단위 = Properties.Settings.Default.combo_ik_ratio_H;
                    주문값 = Properties.Settings.Default.TB_ik_value_H;
                    주문구분 = Properties.Settings.Default.combo_ik_jumun_H;

                    if (잔고.익절H)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (Properties.Settings.Default.CB_ik_one_H && 잔고.일회H.Equals("on"))
                        {
                            일회 = true;

                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (Properties.Settings.Default.CB_ik_I)
                {
                    sender = "CB_ik_one_I";
                    익절값 = Properties.Settings.Default.TB_ik_son_I;
                    익절단위 = Properties.Settings.Default.combo_ik_I;
                    익절비중 = Properties.Settings.Default.TB_ik_son_ratio_I;
                    익절비중단위 = Properties.Settings.Default.combo_ik_ratio_I;
                    주문값 = Properties.Settings.Default.TB_ik_value_I;
                    주문구분 = Properties.Settings.Default.combo_ik_jumun_I;

                    if (잔고.익절I)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (Properties.Settings.Default.CB_ik_one_I && 잔고.일회I.Equals("on"))
                        {
                            일회 = true;

                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 주문구분, 익절비중단위, 익절비중, sender);
                        }
                    }
                }
            }
        }

        public static void 익절주문전달(Stockbalance 잔고, bool 일회, double 익절손익, int 익절손익단위, double Value, int 주문, int 익절비중단위, double 익절비중, string sender)
        {
            if (GET.익절그룹("익절").Contains(GET.그룹변환(잔고.매매그룹)))
            {
                bool 단위_기준금 = Properties.Settings.Default.CB_ik_기준금;

                long 매수기준금 = Properties.Settings.Default.MT_buying_standard;

                if (Method.주문_매매계산(잔고, 단위_기준금, 익절손익, 익절손익단위, "익절"))
                {
                    int 취소시간 = Properties.Settings.Default.MTB_ik_canceltime;
                    int 취소N주문 = Properties.Settings.Default.combo_ik_cancel_sell;
                    int 반복 = Properties.Settings.Default.MTB_ik_repeat;

                    if (Properties.Settings.Default.CBB_ik_CancelOrder > 0) // 취소사용 index > 0
                    {
                        List<JumunItem> 취소주문 = Form1.JumunItem_List.FindAll(o => o.종목코드.Equals(잔고.종목코드));
                        List<JumunItem> 취소번호 = null;

                        int index = Properties.Settings.Default.CBB_ik_CancelOrder;

                        if (index == 1) // 전부취소
                        {
                            취소번호 = 취소주문;
                        }
                        else if (index == 2) // 매수취소
                        {
                            취소번호 = 취소주문.FindAll(o => o.주문유형 == 1);
                        }
                        else if (index == 3) // 매도취소
                        {
                            취소번호 = 취소주문.FindAll(o => o.주문유형 == 2);
                        }

                        if (취소번호.Count > 0)
                        {
                            if (잔고.매매가능)
                            {
                                잔고.매매가능 = false;

                                for (int n = 0; n < 취소번호.Count; n++)
                                {
                                    if (취소번호[n].취소timer > 0)
                                    {
                                        취소번호[n].취소timer = 0;
                                        취소번호[n].취소시간 = 0;
                                        취소번호[n].반복횟수 = 0;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Order 취소종목 = Form1.Order_list.Find(o => o.종목코드.Equals(잔고.종목코드));
                            if (취소종목 == null)
                            {
                                if (일회)
                                {
                                    if (익절N보전_sendorder(잔고, Value, 주문, 익절비중단위, 익절비중, "C익절(일회)_" + 일회차수(false), 취소시간, 취소N주문, 반복))
                                        일회차수(false);
                                }
                                else
                                {
                                    string 차수 = 일회차수(true);
                                    익절N보전_sendorder(잔고, Value, 주문, 익절비중단위, 익절비중, "C익절_" + 차수, 취소시간, 취소N주문, 반복);
                                }
                                잔고.매매가능 = true;
                            }
                        }
                    }
                    else
                    {
                        if (잔고.매매가능)
                        {
                            if (일회)
                            {
                                if (익절N보전_sendorder(잔고, Value, 주문, 익절비중단위, 익절비중, "익절(일회)_" + 일회차수(false), 취소시간, 취소N주문, 반복))
                                    일회차수(false);
                            }
                            else
                            {
                                string 차수 = 일회차수(true);
                                익절N보전_sendorder(잔고, Value, 주문, 익절비중단위, 익절비중, "익절_" + 차수, 취소시간, 취소N주문, 반복);
                            }
                        }
                    }

                    string 일회차수(bool 보전)
                    {
                        string 차수 = "0";
                        switch (sender)
                        {
                            case "CB_ik_one_A":
                                잔고.익절A = false;
                                if (보전) 잔고.보전A = "1";
                                잔고.일회A = "off " + Form1.today;
                                차수 = "A";
                                break;
                            case "CB_ik_one_B":
                                잔고.익절B = false;
                                if (보전) 잔고.보전B = "1";
                                잔고.일회B = "off " + Form1.today;
                                차수 = "B";
                                break;
                            case "CB_ik_one_C":
                                잔고.익절C = false;
                                if (보전) 잔고.보전C = "1";
                                잔고.일회C = "off " + Form1.today;
                                차수 = "C";
                                break;
                            case "CB_ik_one_D":
                                잔고.익절D = false;
                                if (보전) 잔고.보전D = "1";
                                잔고.일회D = "off " + Form1.today;
                                차수 = "D";
                                break;
                            case "CB_ik_one_E":
                                잔고.익절E = false;
                                if (보전) 잔고.보전E = "1";
                                잔고.일회E = "off " + Form1.today;
                                차수 = "E";
                                break;
                            case "CB_ik_one_F":
                                잔고.익절F = false;
                                if (보전) 잔고.보전F = "1";
                                잔고.일회F = "off " + Form1.today;
                                차수 = "F";
                                break;
                            case "CB_ik_one_G":
                                잔고.익절G = false;
                                if (보전) 잔고.보전G = "1";
                                잔고.일회G = "off " + Form1.today;
                                차수 = "G";
                                break;
                            case "CB_ik_one_H":
                                잔고.익절H = false;
                                if (보전) 잔고.보전H = "1";
                                잔고.일회H = "off " + Form1.today;
                                차수 = "H";
                                break;
                            case "CB_ik_one_I":
                                잔고.익절I = false;
                                if (보전) 잔고.보전I = "1";
                                잔고.일회I = "off " + Form1.today;
                                차수 = "I";
                                break;
                        }
                        return 차수;
                    }
                }
            }
        }


        public static void 잔고_보전(Stockbalance 잔고)
        {
            string 보전차수 = "A";
            double 보전손익 = Properties.Settings.Default.TB_ik_down_A;
            int 보전손익단위 = Properties.Settings.Default.combo_ik_down_A;
            int 보전비중단위 = Properties.Settings.Default.combo_ik_down_ratio_A;
            double 보전비중 = Properties.Settings.Default.TB_ik_down_ratio_A;
            double Value = Properties.Settings.Default.TB_ik_down_value_A;
            int 주문 = Properties.Settings.Default.combo_ik_down_jumun_A;
            bool 단위_기준금 = Properties.Settings.Default.CB_ik_down_기준금;

            string 매매그룹 = GET.그룹변환(잔고.매매그룹);
            bool result = false;

            if (잔고.매매가능 && 잔고.주문가능수량 > 0 && Form1.재시작 && !잔고.매도정지)
            {
                if (Properties.Settings.Default.CB_ik_down_A && 잔고.보전A.Equals("1"))
                {
                    result = true;
                }

                if (Properties.Settings.Default.CB_ik_down_B && 잔고.보전B.Equals("1"))
                {
                    보전손익 = Properties.Settings.Default.TB_ik_down_B;
                    보전손익단위 = Properties.Settings.Default.combo_ik_down_B;
                    보전비중 = Properties.Settings.Default.TB_ik_down_ratio_B;
                    보전비중단위 = Properties.Settings.Default.combo_ik_down_ratio_B;
                    보전차수 = "B";
                    Value = Properties.Settings.Default.TB_ik_down_value_B;
                    주문 = Properties.Settings.Default.combo_ik_down_jumun_B;
                    result = true;
                }
                if (Properties.Settings.Default.CB_ik_down_C && 잔고.보전C.Equals("1"))
                {
                    보전손익 = Properties.Settings.Default.TB_ik_down_C;
                    보전손익단위 = Properties.Settings.Default.combo_ik_down_C;
                    보전비중 = Properties.Settings.Default.TB_ik_down_ratio_C;
                    보전비중단위 = Properties.Settings.Default.combo_ik_down_ratio_C;
                    보전차수 = "C";
                    Value = Properties.Settings.Default.TB_ik_down_value_C;
                    주문 = Properties.Settings.Default.combo_ik_down_jumun_C;
                    result = true;
                }
                if (Properties.Settings.Default.CB_ik_down_D && 잔고.보전D.Equals("1"))
                {
                    보전손익 = Properties.Settings.Default.TB_ik_down_D;
                    보전손익단위 = Properties.Settings.Default.combo_ik_down_D;
                    보전비중 = Properties.Settings.Default.TB_ik_down_ratio_D;
                    보전비중단위 = Properties.Settings.Default.combo_ik_down_ratio_D;
                    보전차수 = "D";
                    Value = Properties.Settings.Default.TB_ik_down_value_D;
                    주문 = Properties.Settings.Default.combo_ik_down_jumun_D;
                    result = true;
                }
                if (Properties.Settings.Default.CB_ik_down_E && 잔고.보전E.Equals("1"))
                {
                    보전손익 = Properties.Settings.Default.TB_ik_down_E;
                    보전손익단위 = Properties.Settings.Default.combo_ik_down_E;
                    보전비중 = Properties.Settings.Default.TB_ik_down_ratio_E;
                    보전비중단위 = Properties.Settings.Default.combo_ik_down_ratio_E;
                    보전차수 = "E";
                    Value = Properties.Settings.Default.TB_ik_down_value_E;
                    주문 = Properties.Settings.Default.combo_ik_down_jumun_E;
                    result = true;
                }
                if (Properties.Settings.Default.CB_ik_down_F && 잔고.보전F.Equals("1"))
                {
                    보전손익 = Properties.Settings.Default.TB_ik_down_F;
                    보전손익단위 = Properties.Settings.Default.combo_ik_down_F;
                    보전비중 = Properties.Settings.Default.TB_ik_down_ratio_F;
                    보전비중단위 = Properties.Settings.Default.combo_ik_down_ratio_F;
                    보전차수 = "F";
                    Value = Properties.Settings.Default.TB_ik_down_value_F;
                    주문 = Properties.Settings.Default.combo_ik_down_jumun_F;
                    result = true;
                }
                if (Properties.Settings.Default.CB_ik_down_G && 잔고.보전G.Equals("1"))
                {
                    보전손익 = Properties.Settings.Default.TB_ik_down_G;
                    보전손익단위 = Properties.Settings.Default.combo_ik_down_G;
                    보전비중 = Properties.Settings.Default.TB_ik_down_ratio_G;
                    보전비중단위 = Properties.Settings.Default.combo_ik_down_ratio_G;
                    보전차수 = "G";
                    Value = Properties.Settings.Default.TB_ik_down_value_G;
                    주문 = Properties.Settings.Default.combo_ik_down_jumun_G;
                    result = true;
                }
                if (Properties.Settings.Default.CB_ik_down_H && 잔고.보전H.Equals("1"))
                {
                    보전손익 = Properties.Settings.Default.TB_ik_down_H;
                    보전손익단위 = Properties.Settings.Default.combo_ik_down_H;
                    보전비중 = Properties.Settings.Default.TB_ik_down_ratio_H;
                    보전비중단위 = Properties.Settings.Default.combo_ik_down_ratio_H;
                    보전차수 = "H";
                    Value = Properties.Settings.Default.TB_ik_down_value_H;
                    주문 = Properties.Settings.Default.combo_ik_down_jumun_H;
                    result = true;
                }
                if (Properties.Settings.Default.CB_ik_down_I && 잔고.보전I.Equals("1"))
                {
                    보전손익 = Properties.Settings.Default.TB_ik_down_I;
                    보전손익단위 = Properties.Settings.Default.combo_ik_down_I;
                    보전비중 = Properties.Settings.Default.TB_ik_down_ratio_I;
                    보전비중단위 = Properties.Settings.Default.combo_ik_down_ratio_I;
                    보전차수 = "I";
                    Value = Properties.Settings.Default.TB_ik_down_value_I;
                    주문 = Properties.Settings.Default.combo_ik_down_jumun_I;
                    result = true;
                }

                if (result)
                {
                    if (GET.익절그룹("익절").Contains(매매그룹))
                    {
                        보전주문전달(잔고, 보전손익, 단위_기준금, 보전손익단위, 보전차수, 보전비중단위, 보전비중, Value, 주문);
                    }
                }
            }
        }

        public static void 보전주문전달(Stockbalance 잔고, double 보전손익, bool 단위_기준금, int 보전손익단위, string 보전차수, int 보전비중단위, double 보전비중, double Value, int 주문)
        {
            if (Method.주문_매매계산(잔고, 단위_기준금, 보전손익, 보전손익단위, "보전"))
            {
                int 취소시간 = Properties.Settings.Default.MTB_ik_down_canceltime;
                int 취소N주문 = Properties.Settings.Default.combo_ik_down_cancel_sell;
                int 반복 = Properties.Settings.Default.MTB_ik_down_repeat;

                if (Properties.Settings.Default.CB_ik_down_CancelOrder)
                {
                    List<JumunItem> 취소번호 = Form1.JumunItem_List.FindAll(o => o.종목코드.Equals(잔고.종목코드));
                    if (취소번호.Count > 0)
                    {
                        if (잔고.매매가능)
                        {
                            잔고.매매가능 = false;

                            for (int n = 0; n < 취소번호.Count; n++)
                            {
                                if (취소번호[n].취소timer > 0)
                                {
                                    취소번호[n].취소timer = 0;
                                    취소번호[n].취소시간 = 0;
                                    취소번호[n].반복횟수 = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        Order 취소종목 = Form1.Order_list.Find(o => o.종목코드.Equals(잔고.종목코드));
                        if (취소종목 == null)
                        {
                            if (익절N보전_sendorder(잔고, Value, 주문, 보전비중단위, 보전비중, "C보전_" + 보전차수, 취소시간, 취소N주문, 반복))
                                보전조정(보전차수);

                            잔고.매매가능 = true;
                        }
                    }
                }
                else
                {
                    if (잔고.매매가능)
                    {
                        if (익절N보전_sendorder(잔고, Value, 주문, 보전비중단위, 보전비중, "보전_" + 보전차수, 취소시간, 취소N주문, 반복))
                            보전조정(보전차수);
                    }
                }
            }

            void 보전조정(string 차수)
            {
                switch (차수)
                {
                    case "A":
                        잔고.보전A = "3";
                        break;
                    case "B":
                        잔고.보전B = "3";
                        break;
                    case "C":
                        잔고.보전C = "3";
                        break;
                    case "D":
                        잔고.보전D = "3";
                        break;
                    case "E":
                        잔고.보전E = "3";
                        break;
                    case "F":
                        잔고.보전F = "3";
                        break;
                    case "G":
                        잔고.보전G = "3";
                        break;
                    case "H":
                        잔고.보전H = "3";
                        break;
                    case "I":
                        잔고.보전I = "3";
                        break;
                }
            }
        }

        public static bool 익절N보전_sendorder(Stockbalance 잔고, double 주문값, int 주문구분, int 비중단위, double 비중, string 검색식, int 취소시간, int 취소N주문, int 반복)
        {
            bool 매매확인 = false;

            if (Form1.form1.잔고주문_오더(잔고, 검색식, 2, 비중, 비중단위, 주문값, 주문구분, 취소시간, 취소N주문, 반복, "", 검색식, 0, false, 0))
            {
                매매확인 = true;
            }

            return 매매확인;
        }

        ////////////////////////       잔고익절 메소드모음         ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////





        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////       잔고손절 메소드모음         ////////////////

        public static void 잔고_손실매도(Stockbalance 잔고)
        {
            string 손절차수 = "A";
            double 손절손익 = Properties.Settings.Default.TB_sell_son_A;
            int 손절손익단위 = Properties.Settings.Default.combo_sell_son_A;
            double 손절비중 = Properties.Settings.Default.TB_sell_ratio_A;
            int 손절비중단위 = Properties.Settings.Default.combo_sell_ratio_A;
            double Value = Properties.Settings.Default.TB_sell_value_A;
            int 주문 = Properties.Settings.Default.combo_sell_jumun_A;
            bool 단위_기준금 = Properties.Settings.Default.CB_sell_기준금;

            string 매매그룹 = GET.그룹변환(잔고.매매그룹);
            if (잔고.매매가능 && 잔고.주문가능수량 > 0 && Form1.재시작 && !잔고.매도정지)
            {
                if (GET.익절그룹("손절").Contains(매매그룹))
                {
                    int start = Properties.Settings.Default.MTB_sell_starttime;
                    int end = Properties.Settings.Default.MTB_sell_endtime;

                    if (Properties.Settings.Default.CB_sell_use_A && 잔고.손절A)
                    {
                        손절주문전달(잔고, start, end, 손절차수, 손절손익, 단위_기준금, 손절손익단위, 손절비중, 손절비중단위, Value, 주문);
                    }
                    if (Properties.Settings.Default.CB_sell_use_B && 잔고.손절B)
                    {
                        손절손익 = Properties.Settings.Default.TB_sell_son_B;
                        손절손익단위 = Properties.Settings.Default.combo_sell_son_B;
                        손절비중 = Properties.Settings.Default.TB_sell_ratio_B;
                        Value = Properties.Settings.Default.TB_sell_value_B;
                        주문 = Properties.Settings.Default.combo_sell_jumun_B;
                        손절비중단위 = Properties.Settings.Default.combo_sell_ratio_B;
                        손절차수 = "B";

                        손절주문전달(잔고, start, end, 손절차수, 손절손익, 단위_기준금, 손절손익단위, 손절비중, 손절비중단위, Value, 주문);
                    }
                    if (Properties.Settings.Default.CB_sell_use_C && 잔고.손절C)
                    {
                        손절손익 = Properties.Settings.Default.TB_sell_son_C;
                        손절손익단위 = Properties.Settings.Default.combo_sell_son_C;
                        손절비중 = Properties.Settings.Default.TB_sell_ratio_C;
                        Value = Properties.Settings.Default.TB_sell_value_C;
                        주문 = Properties.Settings.Default.combo_sell_jumun_C;
                        손절비중단위 = Properties.Settings.Default.combo_sell_ratio_B;
                        손절차수 = "C";

                        손절주문전달(잔고, start, end, 손절차수, 손절손익, 단위_기준금, 손절손익단위, 손절비중, 손절비중단위, Value, 주문);
                    }
                    if (Properties.Settings.Default.CB_sell_use_D && 잔고.손절D)
                    {
                        손절손익 = Properties.Settings.Default.TB_sell_son_D;
                        손절손익단위 = Properties.Settings.Default.combo_sell_son_D;
                        손절비중 = Properties.Settings.Default.TB_sell_ratio_D;
                        Value = Properties.Settings.Default.TB_sell_value_D;
                        주문 = Properties.Settings.Default.combo_sell_jumun_D;
                        손절비중단위 = Properties.Settings.Default.combo_sell_ratio_D;
                        손절차수 = "D";

                        손절주문전달(잔고, start, end, 손절차수, 손절손익, 단위_기준금, 손절손익단위, 손절비중, 손절비중단위, Value, 주문);
                    }
                    if (Properties.Settings.Default.CB_sell_use_E && 잔고.손절E)
                    {
                        손절손익 = Properties.Settings.Default.TB_sell_son_E;
                        손절손익단위 = Properties.Settings.Default.combo_sell_son_E;
                        손절비중 = Properties.Settings.Default.TB_sell_ratio_E;
                        Value = Properties.Settings.Default.TB_sell_value_E;
                        주문 = Properties.Settings.Default.combo_sell_jumun_E;
                        손절비중단위 = Properties.Settings.Default.combo_sell_ratio_E;
                        손절차수 = "E";

                        손절주문전달(잔고, start, end, 손절차수, 손절손익, 단위_기준금, 손절손익단위, 손절비중, 손절비중단위, Value, 주문);
                    }
                    if (Properties.Settings.Default.CB_sell_use_F && 잔고.손절F)
                    {
                        손절손익 = Properties.Settings.Default.TB_sell_son_F;
                        손절손익단위 = Properties.Settings.Default.combo_sell_son_F;
                        손절비중 = Properties.Settings.Default.TB_sell_ratio_F;
                        Value = Properties.Settings.Default.TB_sell_value_F;
                        주문 = Properties.Settings.Default.combo_sell_jumun_F;
                        손절비중단위 = Properties.Settings.Default.combo_sell_ratio_F;
                        손절차수 = "F";

                        손절주문전달(잔고, start, end, 손절차수, 손절손익, 단위_기준금, 손절손익단위, 손절비중, 손절비중단위, Value, 주문);
                    }



                }
            }
        }

        public static void 손절주문전달(Stockbalance 잔고, int start, int end, string 손절차수, double 손절손익, bool 단위_기준금, int 손절손익단위, double 손절비중, int 손절비중단위, double 주문값, int 주문구분)
        {
            if (손절손익 <= 0)
            {
                if (Method.RunTime(start, end))
                {
                    if (Method.주문_매매계산(잔고, 단위_기준금, 손절손익, 손절손익단위, "손절"))
                    {
                        if (Properties.Settings.Default.CB_sell_CancelOrder)
                        {
                            List<JumunItem> 취소번호 = Form1.JumunItem_List.FindAll(o => o.종목코드.Equals(잔고.종목코드));
                            if (취소번호.Count > 0)
                            {
                                if (잔고.매매가능)
                                {
                                    잔고.매매가능 = false;

                                    for (int i = 0; i < 취소번호.Count; i++)
                                    {
                                        취소번호[i].취소timer = 0;
                                        취소번호[i].취소시간 = 0;
                                        취소번호[i].반복횟수 = 0;
                                    }
                                }
                            }
                            else
                            {
                                Order 취소종목 = Form1.Order_list.Find(o => o.종목코드.Equals(잔고.종목코드));
                                if (취소종목 == null)
                                {
                                    손절_sendorder();

                                    잔고.매매가능 = true;
                                }
                            }
                        }
                        else
                        {
                            if (잔고.매매가능)
                            {
                                손절_sendorder();
                            }
                        }
                    }
                }

                void 손절_sendorder() // 손절 에 따른 주문 정보 받기 for 기본설정 
                {
                    string 검색식 = "손절_" + 손절차수;
                    int 취소시간 = Properties.Settings.Default.MTB_sell_cancel_time;
                    int 취소N주문 = Properties.Settings.Default.combo_sell_cancel_sell;
                    int 반복 = Properties.Settings.Default.MTB_sell_cancel_repeat;

                    if (Form1.form1.잔고주문_오더(잔고, 검색식, 2, 손절비중, 손절비중단위, 주문값, 주문구분, 취소시간, 취소N주문, 반복, "", 검색식, 0, false, 0))
                    {
                        switch (검색식)
                        {
                            case "손절_A":
                                잔고.손절A = false;
                                break;
                            case "손절_B":
                                잔고.손절B = false;
                                break;
                            case "손절_C":
                                잔고.손절C = false;
                                break;
                            case "손절_D":
                                잔고.손절D = false;
                                break;
                            case "손절_E":
                                잔고.손절E = false;
                                break;
                            case "손절_F":
                                잔고.손절F = false;
                                break;
                        }
                    }
                }
            }
        }

        ////////////////////////       잔고손절 메소드모음         ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        /////////////////////              계좌 매도               ////////////////


        public static void 계좌청산()
        {
            Account 계좌 = Form1.Acc[0];

            long 매수기준금 = Properties.Settings.Default.MT_buying_standard;
            int 취소시간 = Properties.Settings.Default.MT_sell_time_cansel;

            double Value = Properties.Settings.Default.TB_sell_time_value;
            int jumun = Properties.Settings.Default.combo_sell_time_jumun;
            double ratio = Properties.Settings.Default.TB_sell_time_ratio;
            int gubun = Properties.Settings.Default.combo_sell_time_gubun;
            double trade_1 = Properties.Settings.Default.TB_sell_time_trade_1;
            double trade_2 = Properties.Settings.Default.TB_sell_time_trade_2;
            bool overlap = Properties.Settings.Default.CB_sell_time_overlap;
            bool Buy_stop = Properties.Settings.Default.CB_sell_time_Buystop;
            bool 매매취소 = Properties.Settings.Default.CB_sell_time_잔량취소;
            string position = "특정시간_청산";
            double low = Properties.Settings.Default.TB_silson_ik_W_1;
            double height = Properties.Settings.Default.TB_silson_ik_W_2;
            long 매입금 = (long)(Properties.Settings.Default.TB_sell_time_매입금 / 100 * 매수기준금);

            if (Properties.Settings.Default.CB_sell_time_use)
            {
                if (!Form1.매입금_Run_time && 계좌.매입금 >= 매입금)
                {
                    Form1.매입금_Run_time = true;
                }

                if (Method.RunTime(Properties.Settings.Default.MT_sell_time_start, Properties.Settings.Default.MT_sell_time_end) && Form1.Run_time && Form1.매입금_Run_time)
                {
                    if (Form1.stockBalanceList.Count > 0)
                    {
                        Form1.time_Run_silson_W = Properties.Settings.Default.MT_silson_repeat_W;
                        Form1.time_Run_예상수익 = Properties.Settings.Default.MT_예상수익_repeat;
                        Form1.time_Run_예상손실 = Properties.Settings.Default.MT_예상손실_repeat;

                        Acc_Sell_Run(취소시간, Value, jumun, ratio, gubun, trade_1, trade_2, position, overlap, Buy_stop, 매매취소, Form1.일괄취소_time, true, Properties.Settings.Default.TB_sell_time_ik_1, Properties.Settings.Default.TB_sell_time_ik_2);
                    }
                }
            }

            if (Properties.Settings.Default.CB_silson_use_W)
            {
                매입금 = (long)(Properties.Settings.Default.TB_silson_매입금_W / 100 * 매수기준금);

                if (!Form1.매입금_silson_W && 계좌.매입금 >= 매입금)
                {
                    Form1.매입금_silson_W = true;
                }

                if (Method.RunTime(Properties.Settings.Default.MT_silson_start_W, Properties.Settings.Default.MT_silson_end_W) && Form1.Run_silson_W && Form1.매입금_silson_W)
                {
                    취소시간 = Properties.Settings.Default.MT_silson_cancel_W;
                    Value = Properties.Settings.Default.TB_silson_value_W;
                    jumun = Properties.Settings.Default.combo_silson_jumun_W;
                    ratio = Properties.Settings.Default.TB_silson_ratio_W;
                    gubun = Properties.Settings.Default.combo_silson_gubun_W;
                    trade_1 = Properties.Settings.Default.TB_silson_trade_W_1;
                    trade_2 = Properties.Settings.Default.TB_silson_trade_W_2;
                    overlap = Properties.Settings.Default.CB_silson_overlap_W;
                    Buy_stop = Properties.Settings.Default.CB_silson_Buystop_W;
                    매매취소 = Properties.Settings.Default.CB_silson_잔량취소;
                    position = "실현손익_청산";

                    if (Form1.Run_silson_trading)
                    {
                        Acc_Sell_Run(취소시간, Value, jumun, ratio, gubun, trade_1, trade_2, position, overlap, Buy_stop, 매매취소, Form1.일괄취소_silson_W, false, 0, 0);
                    }
                    else
                    {
                        if (Properties.Settings.Default.CB_silson_choice_W) //  "⇒"
                        {
                            계좌.실현손익금청산 = Method.계좌매도조건범위(Properties.Settings.Default.CB_silson_청산기준, 계좌.실현손익, low, height, 계좌.실현손익금청산, "실현손익금청산");

                            if (계좌.실현손익금청산.Equals("＊"))
                            {
                                Form1.Run_silson_trading = true;
                            }
                        }
                        else //  "→";
                        {
                            if (Method.계좌매도수익범위(Properties.Settings.Default.CB_silson_청산기준, 계좌.실현손익, low, height))
                            {
                                Form1.Run_silson_trading = true;
                            }
                        }
                    }
                }
            }

            if (Properties.Settings.Default.CB_예상손실_use)
            {
                매입금 = (long)(Properties.Settings.Default.TB_예상손실_매입 / 100 * 매수기준금);

                if (!Form1.매입금_예상손실 && 계좌.매입금 >= 매입금)
                {
                    Form1.매입금_예상손실 = true;
                }

                if (Method.RunTime(Properties.Settings.Default.MT_예상손실_start, Properties.Settings.Default.MT_예상손실_end) && Form1.Run_예상손실 && Form1.매입금_예상손실)
                {
                    취소시간 = Properties.Settings.Default.MT_예상손실_CanceTime;
                    Value = Properties.Settings.Default.TB_예상손실_value;
                    jumun = Properties.Settings.Default.combo_예상손실_jumun;
                    ratio = Properties.Settings.Default.TB_예상손실_ratio;
                    gubun = Properties.Settings.Default.combo_예상손실_gubun;
                    trade_1 = Properties.Settings.Default.TB_예상손실_trade_1;
                    trade_2 = Properties.Settings.Default.TB_예상손실_trade_2;
                    overlap = Properties.Settings.Default.CB_예상손실_overlap;
                    Buy_stop = Properties.Settings.Default.CB_예상손실_Buystop;
                    매매취소 = Properties.Settings.Default.CB_예상손실_잔량취소;
                    position = "예상손실_청산";
                    low = Properties.Settings.Default.TB_예상손실_ik_1;
                    height = Properties.Settings.Default.TB_예상손실_ik_2;

                    if (Form1.Run_예상손실_trading)
                    {
                        Acc_Sell_Run(취소시간, Value, jumun, ratio, gubun, trade_1, trade_2, position, overlap, Buy_stop, 매매취소, Form1.일괄취소_예상손실, false, 0, 0);
                    }
                    else
                    {
                        if (Properties.Settings.Default.CB_예상손실_choice) // "⇒"
                        {
                            계좌.예상손실청산 = Method.계좌매도조건범위(Properties.Settings.Default.CB_예상손실_청산기준, 계좌.평가손익 + 계좌.실현손익, low, height, 계좌.예상손실청산, "예상손실청산");

                            if (계좌.예상손실청산.Equals("＊"))
                            {
                                Form1.Run_예상손실_trading = true;
                            }
                        }
                        else // "→";
                        {
                            if (Method.계좌매도수익범위(Properties.Settings.Default.CB_예상손실_청산기준, 계좌.평가손익 + 계좌.실현손익, low, height))
                            {
                                Form1.Run_예상손실_trading = true;
                            }
                        }
                    }
                }
            }

            if (Properties.Settings.Default.CB_예상수익사용)
            {
                매입금 = (long)(Properties.Settings.Default.TB_예상수익_매입금 / 100 * 매수기준금);

                if (!Form1.매입금_예상손익 && 계좌.매입금 >= 매입금)
                {
                    Form1.매입금_예상손익 = true;
                }

                if (계좌.평가손익 + 계좌.실현손익 > Form1.form1.예상수익_TS)
                    Form1.form1.예상수익_TS = 계좌.평가손익 + 계좌.실현손익;

                if (Method.RunTime(Properties.Settings.Default.MT_예상수익_start, Properties.Settings.Default.MT_예상수익_end) && Form1.Run_예상수익 && Form1.매입금_예상손익)
                {
                    취소시간 = Properties.Settings.Default.MT_예상수익_cancel;
                    Value = Properties.Settings.Default.TB_예상수익_value;
                    jumun = Properties.Settings.Default.combo_예상수익_jumun;
                    ratio = Properties.Settings.Default.TB_예상수익_ratio;
                    gubun = Properties.Settings.Default.combo_예상수익_gubun;
                    trade_1 = Properties.Settings.Default.TB_예상수익_trade_1;
                    trade_2 = Properties.Settings.Default.TB_예상수익_trade_2;
                    overlap = Properties.Settings.Default.CB_예상수익_overlap;
                    Buy_stop = Properties.Settings.Default.CB_예상수익_Buystop;
                    매매취소 = Properties.Settings.Default.CB_예상수익_잔량취소; //전체취소
                    position = "예상수익_청산";
                    low = Properties.Settings.Default.TB_예상수익_ik_1;
                    height = Properties.Settings.Default.TB_예상수익_ik_2;

                    if (Form1.Run_예상수익_trading)
                    {
                        Acc_Sell_Run(취소시간, Value, jumun, ratio, gubun, trade_1, trade_2, position, overlap, Buy_stop, 매매취소, Form1.일괄취소_예상수익, false, 0, 0);
                    }
                    else
                    {
                        if (Properties.Settings.Default.CB_예상수익TS)
                        {
                            Console.WriteLine("예상수익_TS :: " + Form1.form1.예상수익_TS);

                            double 상승값 = Properties.Settings.Default.TB_예상수익TS_상승값 * 10000;
                            double 하락값 = Properties.Settings.Default.TB_예상수익TS_하락값 * 10000;

                            if (Properties.Settings.Default.CB_예상수익TS_시작)
                            {
                                상승값 = 매수기준금 * Properties.Settings.Default.TB_예상수익TS_상승값 / 100;
                                하락값 = 매수기준금 * Properties.Settings.Default.TB_예상수익TS_하락값 / 100;
                            }

                            if (Form1.form1.예상수익_TS > 상승값)
                            {
                                Console.WriteLine("1111111111111111 :: " + Form1.form1.예상수익_TS);

                                if (계좌.평가손익 + 계좌.실현손익 < Form1.form1.예상수익_TS + 하락값)
                                {
                                    Console.WriteLine("222222222222222 :: " + Form1.form1.예상수익_TS);

                                    Form1.form1.청산검색식 = "예상수익_청산[TS]";
                                    Form1.Run_예상수익_trading = true;
                                }
                            }
                        }
                        else
                        {
                            Form1.form1.청산검색식 = "";

                            if (Properties.Settings.Default.CB_예상수익_choice) // "⇒"
                            {
                                계좌.예상수익청산 = Method.계좌매도조건범위(Properties.Settings.Default.CB_예상수익_청산기준, 계좌.평가손익 + 계좌.실현손익, low, height, 계좌.예상수익청산, "예상수익청산");

                                if (계좌.예상수익청산.Equals("＊"))
                                {
                                    Form1.Run_예상수익_trading = true;
                                }
                            }
                            else // "→";
                            {
                                if (Method.계좌매도수익범위(Properties.Settings.Default.CB_예상수익_청산기준, 계좌.평가손익 + 계좌.실현손익, low, height))
                                {
                                    Form1.Run_예상수익_trading = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void Acc_Sell_Run(int 취소시간, double 주문값, int 주문구분, double 비중, int 비중선택, double trade_1, double trade_2, string position, bool overlap, bool Buy_stop, bool 매매취소, bool 일괄취소, bool Time_sell, double low_ratio, double hight_ratio)
        {
            Dictionary<string, Stockbalance> stockBalanceList = Form1.stockBalanceList;   // 잔고 - 보유잔고리스트

            foreach (var code in stockBalanceList.ToList())
            {
                Stockbalance 잔고 = code.Value;
                string 매매그룹 = GET.그룹변환(잔고.매매그룹);

                if (!잔고.종목상태.Contains("거래정지") && !잔고.종목상태.Contains("동시호가"))
                    if (GET.익절그룹(position).Contains(매매그룹) && 잔고.매매가능 && 잔고.주문가능수량 > 0 && Form1.재시작)
                    {
                        if (Method.청산주문_매매범위(잔고, trade_1, trade_2))
                        {
                            if (Time_sell)
                            {
                                if (Method.시간매도수익범위(Properties.Settings.Default.CBB_sell_time_choice, low_ratio, hight_ratio, 잔고.평가손익, 잔고.수익률))
                                {
                                    주문전달();
                                }
                            }
                            else
                            {
                                if (position.Equals("예상손실_청산"))
                                {
                                    if (Properties.Settings.Default.CB_지수연동청산)
                                    {
                                        bool Run = false;

                                        if (Form1.Acc[0].피_외인 < 0 && Form1.Acc[0].피_기관 < 0 && Form1.Acc[0].피_프로그램 < 0) Run = true;

                                        if (잔고.시장.Equals("D"))
                                        {
                                            if (Form1.Acc[0].닥_외인 < 0 && Form1.Acc[0].닥_기관 < 0 && Form1.Acc[0].닥_프로그램 < 0) Run = true;
                                        }

                                        if (Run)
                                        {
                                            if (Properties.Settings.Default.CB_지수연동범위사용)
                                            {
                                                if (Method.지수연동_범위(true, Properties.Settings.Default.combo_지수연동이하, Properties.Settings.Default.TB_지수연동이하, 잔고.평가손익, 잔고.수익률))
                                                { 주문전달(); }

                                                if (Method.지수연동_범위(false, Properties.Settings.Default.combo_지수연동이상, Properties.Settings.Default.TB_지수연동이상, 잔고.평가손익, 잔고.수익률))
                                                { 주문전달(); }
                                            }
                                            else
                                            {
                                                주문전달();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        주문전달();
                                    }
                                }
                                else
                                {
                                    주문전달();
                                }
                            }
                        }
                    }

                void 주문전달()
                {
                    if (Form1.form1.RB_buy_run.Checked || Form1.form1.RB_sell_run.Checked)
                    {
                        if (Buy_stop)
                        {
                            if (Form1.form1.RB_buy_run.Checked)
                            {
                                Form1.form1.RB_sell_stop.Checked = true;
                                Form1.form1.RB_buy_stop.Checked = true;
                                Form1.form1.RB_sell_run.Checked = true;
                            }
                        }

                        if (일괄취소 && 매매취소)
                        {
                            일괄취소가동();
                            Form1.form1.미체결일괄취소();
                        }
                    }

                    if (Buy_stop)
                    {
                        List<JumunItem> 매수_List = Form1.JumunItem_List.FindAll(o => o.주문유형 == 1);
                        if (매수_List.Count > 0)
                        {
                            for (int n = 0; n < 매수_List.Count; n++)
                            {
                                if (!매수_List[n].주문번호.Contains("+") && 매수_List[n].취소timer > 0)
                                {
                                    매수_List[n].반복횟수 = 0;
                                    매수_List[n].취소시간 = 0;
                                    매수_List[n].취소timer = 0;

                                    매수_List[n].비고 = "계좌청산_매수취소 '취소'";
                                }
                            }
                        }
                    }

                    string 검색식 = position;
                    if (position.Equals("예상수익_청산")) if (!Form1.form1.청산검색식.Equals("")) 검색식 = Form1.form1.청산검색식;

                    if (overlap)
                    {
                        while (true)
                        {
                            bool off = true;
                            if (Method.청산주문_매매범위(잔고, trade_1, trade_2))
                            {
                                if (Form1.form1.잔고주문_오더(잔고, 검색식, 2, 비중, 비중선택, 주문값, 주문구분, 취소시간, 0, 0, "계좌청산", position, 0, true, 0))
                                {
                                    계좌매도가동(잔고);
                                    off = false;
                                }
                            }
                            if (off) break;
                        }
                    }
                    else
                    {
                        if (Method.매매중복체크(잔고.종목코드, 검색식))
                        {
                            if (Form1.form1.잔고주문_오더(잔고, 검색식, 2, 비중, 비중선택, 주문값, 주문구분, 취소시간, 0, 0, "계좌청산", position, 0, true, 0))
                                계좌매도가동(잔고);
                        }
                    }
                }
            }

            void 계좌매도가동(Stockbalance 잔고)
            {
                잔고.매도기준update = false;

                switch (position)
                {
                    case "특정시간_청산":
                        Form1.Run_time = false;
                        Form1.time_Run_time = Properties.Settings.Default.MT_sell_time_repeat;
                        break;

                    case "실현손익_청산":
                        Form1.Run_silson_W = false;
                        Form1.time_Run_silson_W = Properties.Settings.Default.MT_silson_repeat_W;
                        break;

                    case "예상손실_청산":
                        Form1.Run_예상손실 = false;
                        Form1.time_Run_예상손실 = Properties.Settings.Default.MT_예상손실_repeat;
                        break;

                    case "예상수익_청산":
                        Form1.Run_예상수익 = false;
                        Form1.time_Run_예상수익 = Properties.Settings.Default.MT_예상수익_repeat;
                        break;
                }
            }

            void 일괄취소가동()
            {
                switch (position)
                {
                    case "특정시간_청산":
                        Form1.일괄취소_time = false;
                        break;

                    case "실현손익_청산":
                        Form1.일괄취소_silson_W = false;
                        break;

                    case "예상손실_청산":
                        Form1.일괄취소_예상손실 = false;
                        break;

                    case "예상수익_청산":
                        Form1.일괄취소_예상수익 = false;
                        break;
                }
            }
        }

        /////////////////////              계좌 매도               ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        /////////////////////              시간 청산               ////////////////


        public static void TimeSell(Stockbalance 잔고)
        {
            if (잔고.잔고청산 && Form1.재시작 && !잔고.매도정지)
            {
                long 매수기준금 = Properties.Settings.Default.MT_buying_standard;
                bool 단위_기준금 = Properties.Settings.Default.CB_TimeSell_기준금;
                int StartTime = Properties.Settings.Default.TB_TimeSell_start_A;
                int CBB_TimeSell_start = Properties.Settings.Default.CBB_TimeSell_start_A;
                int 거래일 = Properties.Settings.Default.TB_TimeSell_거래일_A;
                double 수익범위_1 = Properties.Settings.Default.TB_TimeSell_수익범위_1_A;
                double 수익범위_2 = Properties.Settings.Default.TB_TimeSell_수익범위_2_A;
                int 수익범위_구분 = Properties.Settings.Default.CBB_TimeSell_수익구분_A;
                double 매도비중 = Properties.Settings.Default.TB_TimeSell_매도비중_A;
                int CBB_매도비중 = Properties.Settings.Default.CBB_TimeSell_매도비중_A;
                double 매매범위_1 = Properties.Settings.Default.TB_TimeSell_매매범위_1_A;
                double 매매범위_2 = Properties.Settings.Default.TB_TimeSell_매매범위_2_A;
                double TB_주문가격 = Properties.Settings.Default.TB_TimeSell_주문가격_A;
                int CBB_주문가격 = Properties.Settings.Default.CBB_TimeSell_주문가격_A;
                bool 수익범위_choice = Properties.Settings.Default.CB_TimeSell_수익범위_choice_A;
                int 취소간격 = Properties.Settings.Default.MT_TimeSell_취소간격_A;
                string position = "잔고시간청산_A";

                double 매입금1 = Properties.Settings.Default.TB_TimeSell_매입금1_A / 100 * 매수기준금;
                double 매입금2 = Properties.Settings.Default.TB_TimeSell_매입금2_A / 100 * 매수기준금;

                if (Properties.Settings.Default.CB_TimeSell_A)
                {
                    if (!잔고.TimeSell_매입금_A && 매입금1 <= 잔고.매입금액 && 잔고.매입금액 <= 매입금2) if (매매검사()) 잔고.TimeSell_매입금_A = true;
                    if (잔고.TimeSell_매입금_A && 잔고.시간청산반복_A == 0 && 매매검사()) 주문();
                    if (잔고.시간청산반복_A > 0) 잔고.시간청산반복_A--;
                }

                if (Properties.Settings.Default.CB_TimeSell_B)
                {
                    StartTime = Properties.Settings.Default.TB_TimeSell_start_B;
                    CBB_TimeSell_start = Properties.Settings.Default.CBB_TimeSell_start_B;
                    거래일 = Properties.Settings.Default.TB_TimeSell_거래일_B;
                    수익범위_1 = Properties.Settings.Default.TB_TimeSell_수익범위_1_B;
                    수익범위_2 = Properties.Settings.Default.TB_TimeSell_수익범위_2_B;
                    수익범위_구분 = Properties.Settings.Default.CBB_TimeSell_수익구분_B;
                    매도비중 = Properties.Settings.Default.TB_TimeSell_매도비중_B;
                    CBB_매도비중 = Properties.Settings.Default.CBB_TimeSell_매도비중_B;
                    매매범위_1 = Properties.Settings.Default.TB_TimeSell_매매범위_1_B;
                    매매범위_2 = Properties.Settings.Default.TB_TimeSell_매매범위_2_B;
                    TB_주문가격 = Properties.Settings.Default.TB_TimeSell_주문가격_B;
                    CBB_주문가격 = Properties.Settings.Default.CBB_TimeSell_주문가격_B;
                    수익범위_choice = Properties.Settings.Default.CB_TimeSell_수익범위_choice_B;
                    취소간격 = Properties.Settings.Default.MT_TimeSell_취소간격_B;
                    position = "잔고시간청산_B";

                    매입금1 = Properties.Settings.Default.TB_TimeSell_매입금1_B / 100 * 매수기준금;
                    매입금2 = Properties.Settings.Default.TB_TimeSell_매입금2_B / 100 * 매수기준금;

                    if (!잔고.TimeSell_매입금_B && 매입금1 <= 잔고.매입금액 && 잔고.매입금액 <= 매입금2) if (매매검사()) 잔고.TimeSell_매입금_B = true;
                    if (잔고.TimeSell_매입금_B && 잔고.시간청산반복_B == 0 && 매매검사()) 주문();
                    if (잔고.시간청산반복_B > 0) 잔고.시간청산반복_B--;
                }

                if (Properties.Settings.Default.CB_TimeSell_C)
                {
                    StartTime = Properties.Settings.Default.TB_TimeSell_start_C;
                    CBB_TimeSell_start = Properties.Settings.Default.CBB_TimeSell_start_C;
                    거래일 = Properties.Settings.Default.TB_TimeSell_거래일_C;
                    수익범위_1 = Properties.Settings.Default.TB_TimeSell_수익범위_1_C;
                    수익범위_2 = Properties.Settings.Default.TB_TimeSell_수익범위_2_C;
                    수익범위_구분 = Properties.Settings.Default.CBB_TimeSell_수익구분_C;
                    매도비중 = Properties.Settings.Default.TB_TimeSell_매도비중_C;
                    CBB_매도비중 = Properties.Settings.Default.CBB_TimeSell_매도비중_C;
                    매매범위_1 = Properties.Settings.Default.TB_TimeSell_매매범위_1_C;
                    매매범위_2 = Properties.Settings.Default.TB_TimeSell_매매범위_2_C;

                    TB_주문가격 = Properties.Settings.Default.TB_TimeSell_주문가격_C;
                    CBB_주문가격 = Properties.Settings.Default.CBB_TimeSell_주문가격_C;
                    수익범위_choice = Properties.Settings.Default.CB_TimeSell_수익범위_choice_C;
                    취소간격 = Properties.Settings.Default.MT_TimeSell_취소간격_C;
                    position = "잔고시간청산_C";

                    매입금1 = Properties.Settings.Default.TB_TimeSell_매입금1_C / 100 * 매수기준금;
                    매입금2 = Properties.Settings.Default.TB_TimeSell_매입금2_C / 100 * 매수기준금;

                    if (!잔고.TimeSell_매입금_C && 매입금1 <= 잔고.매입금액 && 잔고.매입금액 <= 매입금2) if (매매검사()) 잔고.TimeSell_매입금_C = true;
                    if (잔고.TimeSell_매입금_C && 잔고.시간청산반복_C == 0 && 매매검사()) 주문();
                    if (잔고.시간청산반복_C > 0) 잔고.시간청산반복_C--;
                }

                bool 매매검사()
                {
                    bool result = false;

                    if (CBB_TimeSell_start == 0)
                    {
                        if (Form1.timenow > StartTime) 검사();

                    }
                    else
                    {
                        if (잔고.초기매수일.AddSeconds(StartTime) < DateTime.Now) 검사();
                    }

                    void 검사()
                    {
                        TimeSpan 매매일 = DateTime.Now.Date - 잔고.초기매수일.Date;
                        if (매매일.Days <= 거래일)
                        {
                            string 매매그룹 = GET.그룹변환(잔고.매매그룹);
                            if (GET.익절그룹(position).Contains(매매그룹))
                            {
                                if (!수익범위_choice) // →
                                {
                                    if (Method.수익범위(false, 단위_기준금, 잔고, 수익범위_1, 수익범위_2, 수익범위_구분, position))
                                    {
                                        result = true;

                                    }
                                }
                                else // ⇒
                                {
                                    switch (position)
                                    {
                                        case "잔고시간청산_A":
                                            잔고.시간청산동작_A = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, 수익범위_1, 수익범위_2, 수익범위_구분, 잔고.시간청산동작_A, "A", false);
                                            if (잔고.시간청산동작_A.Equals("X"))
                                            {
                                                result = true;
                                            }
                                            break;

                                        case "잔고시간청산_B":
                                            잔고.시간청산동작_B = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, 수익범위_1, 수익범위_2, 수익범위_구분, 잔고.시간청산동작_B, "B", false);
                                            if (잔고.시간청산동작_B.Equals("X"))
                                            {
                                                result = true;
                                            }
                                            break;

                                        case "잔고시간청산_C":
                                            잔고.시간청산동작_C = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, 수익범위_1, 수익범위_2, 수익범위_구분, 잔고.시간청산동작_C, "C", false);
                                            if (잔고.시간청산동작_C.Equals("X"))
                                            {
                                                result = true;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    return result;
                }

                void 주문()
                {
                    if (Method.청산주문_매매범위(잔고, 매매범위_1, 매매범위_2))
                    {
                        List<JumunItem> item = Form1.JumunItem_List.ToList().FindAll(o => o.종목코드.Equals(잔고.종목코드) && !o.검색식.Contains("잔고시간청산"));

                        if (item.Count > 0)
                        {
                            잔고.매매가능 = false;

                            for (int i = 0; i < item.ToList().Count; i++)
                            {
                                JumunItem JumunItem = item[i];

                                if (JumunItem != null)
                                {
                                    JumunItem.반복횟수 = 0;
                                    JumunItem.취소시간 = 0;
                                    JumunItem.취소timer = 0;

                                    JumunItem.비고 = "잔고 시간청산_미체결 '취소'";
                                }
                            }
                        }
                        else
                        {
                            잔고.매도기준update = false;
                            잔고.매도기준 = 잔고.보유수량;

                            TimeSell_Run();
                            잔고.매매가능 = true;
                        }
                    }

                    void TimeSell_Run()
                    {
                        while (true)
                        {
                            bool off = true;
                            if (Method.청산주문_매매범위(잔고, 매매범위_1, 매매범위_2))
                            {
                                if (Form1.form1.잔고주문_오더(잔고, position, 2, 매도비중, CBB_매도비중, TB_주문가격, CBB_주문가격, 취소간격, 0, 0, "", position, 0, true, 매매범위_1))
                                {
                                    주문_완료();
                                    off = false;
                                }
                            }

                            if (off) break;
                        }

                        void 주문_완료()
                        {
                            switch (position)
                            {
                                case "잔고시간청산_A":
                                    잔고.시간청산반복_A = Properties.Settings.Default.MT_TimeSell_반복간격_A;
                                    break;
                                case "잔고시간청산_B":
                                    잔고.시간청산반복_B = Properties.Settings.Default.MT_TimeSell_반복간격_B;
                                    break;
                                case "잔고시간청산_C":
                                    잔고.시간청산반복_C = Properties.Settings.Default.MT_TimeSell_반복간격_C;
                                    break;
                            }
                        }
                    }
                }
            }
        }



        /////////////////////              시간 청산               ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////            상,하 전량매도               ////////////////

        public static void 상하_전량매도(Stockbalance 잔고)
        {
            if (잔고.매매가능 && Form1.재시작 && !잔고.매도정지)
            {
                if (Properties.Settings.Default.CB_상전량청산 && 잔고.종목상태.Contains("상한가") && 잔고.수익률 > 0) 매매주문("상한_전량청산");
                if (Properties.Settings.Default.CB_하전량청산 && 잔고.종목상태.Contains("하한가")) 매매주문("하한_전량청산");

                void 매매주문(string 검색식)
                {
                    List<JumunItem> item = Form1.JumunItem_List.ToList().FindAll(o => o.종목코드.Equals(잔고.종목코드));
                    JumunItem 주문 = item.Find(o => !o.검색식.Contains(검색식));
                    if (주문 != null)
                    {
                        for (int i = 0; i < item.ToList().Count; i++)
                        {
                            JumunItem JumunItem = item[i];
                            if (JumunItem != null && !JumunItem.검색식.Contains(검색식))
                            {
                                JumunItem.반복횟수 = 0;
                                JumunItem.취소시간 = 0;
                                JumunItem.취소timer = 0;

                                JumunItem.비고 = 검색식 + "_미체결일괄'취소'";
                            }
                        }

                        잔고.매매가능 = false;
                    }
                    else
                    {
                        if (잔고.주문가능수량 > 0)
                        {
                            Form1.form1.잔고주문_오더(잔고, 검색식, 2, 100, 3, 0, 0, 3600, 0, 0, "", 검색식, 0, false, 0);
                        }
                    }
                }
            }
        }


        //////////////////            상,하 전량매도               ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////





        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ///////////////                스캘핑 주문                ////////////////

        public static void 스켈핑등록(string 검색식, string 화면번호, string 코드, int 주문수량)
        {
            if (Properties.Settings.Default.CB_scalping)
            {
                if (Properties.Settings.Default.CB_scalping_A)
                {
                    int 수량A = 0; if (Properties.Settings.Default.CB_ik_A && Properties.Settings.Default.CB_scalping_A_1) 수량A = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_A / 100);
                    int 수량B = 0; if (Properties.Settings.Default.CB_ik_B && Properties.Settings.Default.CB_scalping_A_2) 수량B = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_B / 100);
                    int 수량C = 0; if (Properties.Settings.Default.CB_ik_C && Properties.Settings.Default.CB_scalping_A_3) 수량C = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_C / 100);
                    int 수량D = 0; if (Properties.Settings.Default.CB_ik_D && Properties.Settings.Default.CB_scalping_A_4) 수량D = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_D / 100);
                    int 수량E = 0; if (Properties.Settings.Default.CB_ik_E && Properties.Settings.Default.CB_scalping_A_5) 수량E = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_E / 100);
                    int 수량F = 0; if (Properties.Settings.Default.CB_ik_F && Properties.Settings.Default.CB_scalping_A_6) 수량F = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_F / 100);
                    int 수량G = 0; if (Properties.Settings.Default.CB_ik_G && Properties.Settings.Default.CB_scalping_A_7) 수량G = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_G / 100);
                    int 수량H = 0; if (Properties.Settings.Default.CB_ik_H && Properties.Settings.Default.CB_scalping_A_8) 수량H = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_H / 100);
                    int 수량I = 0; if (Properties.Settings.Default.CB_ik_I && Properties.Settings.Default.CB_scalping_A_9) 수량I = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_I / 100);

                    Scalping 스켈등록 = new Scalping(검색식, 코드, 화면번호, 수량A, 수량B, 수량C, 수량D, 수량E, 수량F, 수량G, 수량H, 수량I);
                    Form1.form1.Scalping_List.Add(스켈등록);
                }

                if (Properties.Settings.Default.CB_scalping_B)
                {
                    int 수량A = 0; if (Properties.Settings.Default.CB_ik_A && Properties.Settings.Default.CB_scalping_B_1) 수량A = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_A / 100);
                    int 수량B = 0; if (Properties.Settings.Default.CB_ik_B && Properties.Settings.Default.CB_scalping_B_2) 수량B = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_B / 100);
                    int 수량C = 0; if (Properties.Settings.Default.CB_ik_C && Properties.Settings.Default.CB_scalping_B_3) 수량C = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_C / 100);
                    int 수량D = 0; if (Properties.Settings.Default.CB_ik_D && Properties.Settings.Default.CB_scalping_B_4) 수량D = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_D / 100);
                    int 수량E = 0; if (Properties.Settings.Default.CB_ik_E && Properties.Settings.Default.CB_scalping_B_5) 수량E = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_E / 100);
                    int 수량F = 0; if (Properties.Settings.Default.CB_ik_F && Properties.Settings.Default.CB_scalping_B_6) 수량F = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_F / 100);
                    int 수량G = 0; if (Properties.Settings.Default.CB_ik_G && Properties.Settings.Default.CB_scalping_B_7) 수량G = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_G / 100);
                    int 수량H = 0; if (Properties.Settings.Default.CB_ik_H && Properties.Settings.Default.CB_scalping_B_8) 수량H = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_H / 100);
                    int 수량I = 0; if (Properties.Settings.Default.CB_ik_I && Properties.Settings.Default.CB_scalping_B_9) 수량I = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_I / 100);

                    Scalping 스켈등록 = new Scalping(검색식, 코드, 화면번호, 수량A, 수량B, 수량C, 수량D, 수량E, 수량F, 수량G, 수량H, 수량I);
                    Form1.form1.Scalping_List.Add(스켈등록);
                }

                if (Properties.Settings.Default.CB_scalping_C)
                {
                    int 수량A = 0; if (Properties.Settings.Default.CB_ik_A && Properties.Settings.Default.CB_scalping_C_1) 수량A = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_A / 100);
                    int 수량B = 0; if (Properties.Settings.Default.CB_ik_B && Properties.Settings.Default.CB_scalping_C_2) 수량B = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_B / 100);
                    int 수량C = 0; if (Properties.Settings.Default.CB_ik_C && Properties.Settings.Default.CB_scalping_C_3) 수량C = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_C / 100);
                    int 수량D = 0; if (Properties.Settings.Default.CB_ik_D && Properties.Settings.Default.CB_scalping_C_4) 수량D = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_D / 100);
                    int 수량E = 0; if (Properties.Settings.Default.CB_ik_E && Properties.Settings.Default.CB_scalping_C_5) 수량E = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_E / 100);
                    int 수량F = 0; if (Properties.Settings.Default.CB_ik_F && Properties.Settings.Default.CB_scalping_C_6) 수량F = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_F / 100);
                    int 수량G = 0; if (Properties.Settings.Default.CB_ik_G && Properties.Settings.Default.CB_scalping_C_7) 수량G = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_G / 100);
                    int 수량H = 0; if (Properties.Settings.Default.CB_ik_H && Properties.Settings.Default.CB_scalping_C_8) 수량H = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_H / 100);
                    int 수량I = 0; if (Properties.Settings.Default.CB_ik_I && Properties.Settings.Default.CB_scalping_C_9) 수량I = (int)Math.Ceiling(주문수량 * Properties.Settings.Default.TB_ik_son_ratio_I / 100);

                    Scalping 스켈등록 = new Scalping(검색식, 코드, 화면번호, 수량A, 수량B, 수량C, 수량D, 수량E, 수량F, 수량G, 수량H, 수량I);
                    Form1.form1.Scalping_List.Add(스켈등록);
                }
            }
        }

        public static void 스캘핑주문(string 코드, int 체_체결가, int 체_단위체결량, int 체_미체결량, string 화면번호)
        {
            if (Properties.Settings.Default.CB_scalping)
            {
                Dictionary<string, Stockbalance> stockBalanceList = Form1.stockBalanceList;   // 잔고 - 보유잔고리스트
                if (stockBalanceList.TryGetValue(코드, out Stockbalance 잔고))
                {
                    string find검색식()
                    {
                        string 검색식 = "신규_A";
                        if (잔고.초기매수검색식.Contains("신규_B"))
                        {
                            검색식 = "신규_B";
                        }
                        if (잔고.초기매수검색식.Contains("신규_C"))
                        {
                            검색식 = "신규_C";
                        }
                        return 검색식;
                    }

                    Scalping Item = Form1.form1.Scalping_List.Find(o => o.코드.Equals(코드) && o.화면번호.Equals(화면번호));
                    if (Item != null)
                    {
                        if (Properties.Settings.Default.CB_scalping)
                        {
                            if (Properties.Settings.Default.CB_new_A && Item.검색식.Contains(Properties.Settings.Default.combo_new_condition_A))
                            {
                                스켈주문();
                            }
                            if (Properties.Settings.Default.CB_new_B && Item.검색식.Contains(Properties.Settings.Default.combo_new_condition_B))
                            {
                                스켈주문();
                            }
                            if (Properties.Settings.Default.CB_new_C && Item.검색식.Contains(Properties.Settings.Default.combo_new_condition_C))

                                스켈주문();
                        }
                    }

                    void 스켈주문()
                    {
                        int 주문수량 = 체_단위체결량;
                        double 주문비 = 0;
                        string 검색식 = "";

                        if (Item.수량A > 0)
                        {
                            if (체_단위체결량 >= Item.수량A)
                            {
                                주문수량 = Item.수량A;
                            }
                            Item.수량A = Item.수량A - 주문수량;
                            검색식 = find검색식() + " 스켈익절_A";

                            주문비 = Properties.Settings.Default.TB_ik_son_A;

                            주문오더();

                            체_단위체결량 = 체_단위체결량 - 주문수량;

                            if (체_단위체결량 > 0)
                            {
                                B주문();
                            }
                            else
                            {
                                삭제();
                            }
                        }
                        else
                        {
                            B주문();
                        }

                        void B주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량B > 0)
                            {
                                if (체_단위체결량 >= Item.수량B)
                                {
                                    주문수량 = Item.수량B;
                                }
                                Item.수량B = Item.수량B - 주문수량;
                                검색식 = find검색식() + " 스켈익절_B";
                                주문비 = Properties.Settings.Default.TB_ik_son_B;

                                주문오더();

                                체_단위체결량 = 체_단위체결량 - 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    C주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                C주문();
                            }
                        }

                        void C주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량C > 0)
                            {
                                if (체_단위체결량 >= Item.수량C)
                                {
                                    주문수량 = Item.수량C;
                                }
                                Item.수량C = Item.수량C - 주문수량;
                                검색식 = find검색식() + " 스켈익절_C";
                                주문비 = Properties.Settings.Default.TB_ik_son_C;

                                주문오더();

                                체_단위체결량 = 체_단위체결량 - 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    D주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                D주문();
                            }
                        }

                        void D주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량D > 0)
                            {
                                if (체_단위체결량 >= Item.수량D)
                                {
                                    주문수량 = Item.수량D;
                                }
                                Item.수량D = Item.수량D - 주문수량;
                                검색식 = find검색식() + " 스켈익절_D";
                                주문비 = Properties.Settings.Default.TB_ik_son_D;

                                주문오더();

                                체_단위체결량 = 체_단위체결량 - 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    E주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                E주문();
                            }
                        }

                        void E주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량E > 0)
                            {
                                if (체_단위체결량 >= Item.수량E)
                                {
                                    주문수량 = Item.수량E;
                                }
                                Item.수량E = Item.수량E - 주문수량;
                                검색식 = find검색식() + " 스켈익절_E";
                                주문비 = Properties.Settings.Default.TB_ik_son_E;

                                주문오더();

                                체_단위체결량 = 체_단위체결량 - 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    F주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                F주문();
                            }
                        }

                        void F주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량F > 0)
                            {
                                if (체_단위체결량 >= Item.수량F)
                                {
                                    주문수량 = Item.수량F;
                                }
                                Item.수량F = Item.수량F - 주문수량;
                                검색식 = find검색식() + " 스켈익절_F";
                                주문비 = Properties.Settings.Default.TB_ik_son_F;

                                주문오더();

                                체_단위체결량 = 체_단위체결량 - 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    G주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                G주문();
                            }
                        }

                        void G주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량G > 0)
                            {
                                if (체_단위체결량 >= Item.수량G)
                                {
                                    주문수량 = Item.수량G;
                                }
                                Item.수량G = Item.수량G - 주문수량;
                                검색식 = find검색식() + " 스켈익절_G";
                                주문비 = Properties.Settings.Default.TB_ik_son_G;

                                주문오더();

                                체_단위체결량 = 체_단위체결량 - 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    H주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                H주문();
                            }
                        }

                        void H주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량H > 0)
                            {
                                if (체_단위체결량 >= Item.수량H)
                                {
                                    주문수량 = Item.수량H;
                                }
                                Item.수량H = Item.수량H - 주문수량;
                                검색식 = find검색식() + " 스켈익절_H";
                                주문비 = Properties.Settings.Default.TB_ik_son_H;

                                주문오더();

                                체_단위체결량 = 체_단위체결량 - 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    I주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                I주문();
                            }
                        }

                        void I주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량I > 0)
                            {
                                if (체_단위체결량 >= Item.수량I)
                                {
                                    주문수량 = Item.수량I;
                                }
                                Item.수량I = Item.수량I - 주문수량;
                                검색식 = find검색식() + " 스켈익절_I";
                                주문비 = Properties.Settings.Default.TB_ik_son_I;

                                주문오더();

                                삭제();
                            }
                            else
                            {
                                삭제();
                            }
                        }

                        void 주문오더()
                        {
                            double tax_ = Form1.TAX;
                            if (잔고.시장.Equals("E")) tax_ = 0;

                            int 주문가 = int.Parse(Method.주문가계산(코드, 주문비 + (tax_ * 100) + (Form1.수수료 * 100 * 2), 체_체결가, 잔고.현재가, "상한가").Split('&')[0]);
                            스캘핑_오더(잔고, 주문수량, 주문가, 검색식);
                        }

                        void 삭제()
                        {
                            if (체_미체결량 == 0)
                            {
                                Form1.form1.Scalping_List.Remove(Item);
                            }
                        }
                    }
                }
            }
        }


        public static void 스캘핑_오더(Stockbalance 잔고, int 주문수량, int 주문가격, string 검색식)
        {
            int 취소시간 = Properties.Settings.Default.MTB_ik_canceltime;

            string HogaGB = "00"; // 지정가
            int 주문유형 = 2;

            if (잔고.주문가능수량 > 0)
            {
                if (잔고.주문가능수량 < 주문수량)
                {
                    주문수량 = 0;
                }

                if (주문수량 > 0)
                {
                    if (Method.매매확인_VI_모투가능확인(Form1.Market_Item_List[잔고.종목코드], 주문유형))
                    {
                        int ScreenNumber = GET.jumunScreen(잔고.종목코드);
                        if (ScreenNumber == 1300)
                        {
                            Method.주문초과알림(잔고.종목명);
                        }
                        else
                        {
                            DataManagement.주문가능수업데이트(잔고, "매도", 주문수량, "매도주문");
                            int Order번호 = GET.Order번호();


                            JumunItem ItemList = new JumunItem(0, 0, ScreenNumber.ToString(), 잔고.종목코드, 잔고.종목명, "+++", "---", 검색식, 0, 1, 취소시간, 0, 0, "", 검색식, 주문수량, 주문가격, 주문유형, 0, 0, 취소시간, 0, 0,
                                                               Form1.timenow, 주문수량, true, false, 0, Method.Find_Tik_Cap(잔고.현재가, 주문가격, 잔고.시장),
                                                               잔고.현재가, 잔고.수익률, false, 0, Order번호, 0, Form1.NXT_server); // 자동 매도 일때  주문추가 
                            Form1.JumunItem_List.Add(ItemList);

                            Form1.que_order(ScreenNumber.ToString(), 잔고.종목명, 주문유형, 잔고.종목코드, 주문수량, 주문가격, HogaGB, "+++", 검색식, Order번호);
                        }
                    }
                }

                Form1.form1.금액알림 = true;
            }
            else
            {
                if (Form1.form1.금액알림)
                {
                    Form1.form1.금액알림 = false;

                    Form1.AutoClosingAlram("[스켈핑 주문불가] 종목명:" + 잔고.종목명 + "주문가능 수량이 없어 ' 매도 '주문 할수 없습니다.", "주문불가", 10, "에러");
                }
            }
        }

        public static void 취소_스켈핑주문(string 화면번호, string 코드b)
        {
            Scalping 종목 = Form1.form1.Scalping_List.Find(o => o.코드.Equals(코드b) && o.화면번호.Equals(화면번호));
            if (종목 != null)
            {
                Form1.form1.Scalping_List.Remove(종목);
            }
        }

        public static void 스켈핑차수조정(string 주문번호)
        {
            Dictionary<string, Stockbalance> stockBalanceList = Form1.stockBalanceList;   // 잔고 - 보유잔고리스트
            JumunItem JumunItem = Form1.JumunItem_List.Find(o => o.주문번호.Equals(주문번호));
            if (JumunItem != null)
            {
                if (stockBalanceList.TryGetValue(JumunItem.종목코드, out Stockbalance 잔고))
                {
                    if (JumunItem.검색식.Equals("스켈핑_익절_A") && 잔고.익절A) { 잔고.익절A = false; 잔고.보전A = "1"; }
                    if (JumunItem.검색식.Equals("스켈핑_익절_B") && 잔고.익절B) { 잔고.익절B = false; 잔고.보전B = "1"; }
                    if (JumunItem.검색식.Equals("스켈핑_익절_C") && 잔고.익절C) { 잔고.익절C = false; 잔고.보전C = "1"; }
                    if (JumunItem.검색식.Equals("스켈핑_익절_D") && 잔고.익절D) { 잔고.익절D = false; 잔고.보전D = "1"; }
                    if (JumunItem.검색식.Equals("스켈핑_익절_E") && 잔고.익절E) { 잔고.익절E = false; 잔고.보전E = "1"; }
                    if (JumunItem.검색식.Equals("스켈핑_익절_F") && 잔고.익절F) { 잔고.익절F = false; 잔고.보전F = "1"; }
                    if (JumunItem.검색식.Equals("스켈핑_익절_G") && 잔고.익절G) { 잔고.익절G = false; 잔고.보전G = "1"; }
                    if (JumunItem.검색식.Equals("스켈핑_익절_H") && 잔고.익절H) { 잔고.익절H = false; 잔고.보전H = "1"; }
                    if (JumunItem.검색식.Equals("스켈핑_익절_I") && 잔고.익절I) { 잔고.익절I = false; 잔고.보전I = "1"; }
                }
            }
        }

        ///////////////                스캘핑 주문                ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        public static void 트레일링_값갱신(Stockbalance 잔고)
        {
            if (잔고.트레일링값.Split('@').Length < 4) 잔고.트레일링값 = 잔고.트레일링값 + "@0";

            double.TryParse(잔고.트레일링값.Split('@')[0], out double 수익률);
            long.TryParse(잔고.트레일링값.Split('@')[1], out long 평가손익);
            long.TryParse(잔고.트레일링값.Split('@')[2], out long 예상손익);
            double.TryParse(잔고.트레일링값.Split('@')[3], out double 기준수익률);

            if (잔고.수익률 > 수익률) 수익률 = 잔고.수익률;
            if (잔고.평가손익 > 평가손익) 평가손익 = 잔고.평가손익;
            if (잔고.예상손익 > 예상손익) 예상손익 = 잔고.예상손익;
            if (잔고.기준수익률 > 기준수익률) 기준수익률 = 잔고.기준수익률;

            잔고.트레일링값 = 수익률 + "@" + 평가손익 + "@" + 예상손익 + "@" + 기준수익률;
            잔고.예상손익 = 잔고.누적손익 + 잔고.평가손익;
        }

        public static double 트레일값(Stockbalance 잔고, int index)
        {
            double.TryParse(잔고.트레일링값.Split('@')[index], out double result);
            return result;
        }

        public static double 트레일링스탑_구분(Stockbalance 잔고, int index)
        {
            double value = 0;
            switch (index)
            {
                case 0:
                    value = 잔고.수익률;
                    break;
                case 1:
                    value = 잔고.평가손익;
                    break;
                case 2:
                    value = 잔고.예상손익;
                    break;
                case 3:
                    value = 잔고.기준수익률;
                    break;
            }

            return value;
        }

        public static void 트레일링스탑(Stockbalance 잔고)
        {
            if (!잔고.매도정지)
            {
                bool 기준금 = Properties.Settings.Default.CB_TS_기준금;

                int CBB_index = Properties.Settings.Default.CBB_TS_upper_A;
                double 잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                double upper_value = 기준금_변환(Properties.Settings.Default.TB_TS_upper_A, CBB_index, 기준금);
                double down_value = 기준금_변환(Properties.Settings.Default.TB_TS_down_A, CBB_index, 기준금);
                double 트레일값_ = 트레일값(잔고, CBB_index);
                double 익절비중 = Properties.Settings.Default.TB_TS_ratio_A;
                int 익절비중단위 = Properties.Settings.Default.CBB_TS_ratio_A;
                double 주문_Value = Properties.Settings.Default.TB_TS_Jumun_A;
                int 주문 = Properties.Settings.Default.CBB_TS_Jumun_A;
                string 검색식 = "트레일링스탑_A";

                if (잔고.TS_1 && Properties.Settings.Default.CB_TS_A)
                {
                    if (트레일링스탑()) 잔고.TS_1 = false;
                }
                if (잔고.TS_2 && Properties.Settings.Default.CB_TS_B)
                {
                    CBB_index = Properties.Settings.Default.CBB_TS_upper_B;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(Properties.Settings.Default.TB_TS_upper_B, CBB_index, 기준금);
                    down_value = 기준금_변환(Properties.Settings.Default.TB_TS_down_B, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = Properties.Settings.Default.TB_TS_ratio_B;
                    익절비중단위 = Properties.Settings.Default.CBB_TS_ratio_B;
                    주문_Value = Properties.Settings.Default.TB_TS_Jumun_B;
                    주문 = Properties.Settings.Default.CBB_TS_Jumun_B;
                    검색식 = "트레일링스탑_B";

                    if (트레일링스탑()) 잔고.TS_2 = false;
                }
                if (잔고.TS_3 && Properties.Settings.Default.CB_TS_C)
                {
                    CBB_index = Properties.Settings.Default.CBB_TS_upper_C;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(Properties.Settings.Default.TB_TS_upper_C, CBB_index, 기준금);
                    down_value = 기준금_변환(Properties.Settings.Default.TB_TS_down_C, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = Properties.Settings.Default.TB_TS_ratio_C;
                    익절비중단위 = Properties.Settings.Default.CBB_TS_ratio_C;
                    주문_Value = Properties.Settings.Default.TB_TS_Jumun_C;
                    주문 = Properties.Settings.Default.CBB_TS_Jumun_C;
                    검색식 = "트레일링스탑_C";

                    if (트레일링스탑()) 잔고.TS_3 = false;
                }
                if (잔고.TS_4 && Properties.Settings.Default.CB_TS_D)
                {
                    CBB_index = Properties.Settings.Default.CBB_TS_upper_D;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(Properties.Settings.Default.TB_TS_upper_D, CBB_index, 기준금);
                    down_value = 기준금_변환(Properties.Settings.Default.TB_TS_down_D, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = Properties.Settings.Default.TB_TS_ratio_D;
                    익절비중단위 = Properties.Settings.Default.CBB_TS_ratio_D;
                    주문_Value = Properties.Settings.Default.TB_TS_Jumun_D;
                    주문 = Properties.Settings.Default.CBB_TS_Jumun_D;
                    검색식 = "트레일링스탑_D";

                    if (트레일링스탑()) 잔고.TS_4 = false;
                }
                if (잔고.TS_5 && Properties.Settings.Default.CB_TS_E)
                {
                    CBB_index = Properties.Settings.Default.CBB_TS_upper_E;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(Properties.Settings.Default.TB_TS_upper_E, CBB_index, 기준금);
                    down_value = 기준금_변환(Properties.Settings.Default.TB_TS_down_E, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = Properties.Settings.Default.TB_TS_ratio_E;
                    익절비중단위 = Properties.Settings.Default.CBB_TS_ratio_E;
                    주문_Value = Properties.Settings.Default.TB_TS_Jumun_E;
                    주문 = Properties.Settings.Default.CBB_TS_Jumun_E;
                    검색식 = "트레일링스탑_E";

                    if (트레일링스탑()) 잔고.TS_5 = false;
                }
                if (잔고.TS_6 && Properties.Settings.Default.CB_TS_F)
                {
                    CBB_index = Properties.Settings.Default.CBB_TS_upper_F;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(Properties.Settings.Default.TB_TS_upper_F, CBB_index, 기준금);
                    down_value = 기준금_변환(Properties.Settings.Default.TB_TS_down_F, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = Properties.Settings.Default.TB_TS_ratio_F;
                    익절비중단위 = Properties.Settings.Default.CBB_TS_ratio_F;
                    주문_Value = Properties.Settings.Default.TB_TS_Jumun_F;
                    주문 = Properties.Settings.Default.CBB_TS_Jumun_F;
                    검색식 = "트레일링스탑_F";

                    if (트레일링스탑()) 잔고.TS_6 = false;
                }
                if (잔고.TS_7 && Properties.Settings.Default.CB_TS_G)
                {
                    CBB_index = Properties.Settings.Default.CBB_TS_upper_G;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(Properties.Settings.Default.TB_TS_upper_G, CBB_index, 기준금);
                    down_value = 기준금_변환(Properties.Settings.Default.TB_TS_down_G, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = Properties.Settings.Default.TB_TS_ratio_G;
                    익절비중단위 = Properties.Settings.Default.CBB_TS_ratio_G;
                    주문_Value = Properties.Settings.Default.TB_TS_Jumun_G;
                    주문 = Properties.Settings.Default.CBB_TS_Jumun_G;
                    검색식 = "트레일링스탑_G";

                    if (트레일링스탑()) 잔고.TS_7 = false;
                }
                if (잔고.TS_8 && Properties.Settings.Default.CB_TS_H)
                {
                    CBB_index = Properties.Settings.Default.CBB_TS_upper_H;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(Properties.Settings.Default.TB_TS_upper_H, CBB_index, 기준금);
                    down_value = 기준금_변환(Properties.Settings.Default.TB_TS_down_H, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = Properties.Settings.Default.TB_TS_ratio_H;
                    익절비중단위 = Properties.Settings.Default.CBB_TS_ratio_H;
                    주문_Value = Properties.Settings.Default.TB_TS_Jumun_H;
                    주문 = Properties.Settings.Default.CBB_TS_Jumun_H;
                    검색식 = "트레일링스탑_H";

                    if (트레일링스탑()) 잔고.TS_8 = false;
                }
                if (잔고.TS_9 && Properties.Settings.Default.CB_TS_I)
                {
                    CBB_index = Properties.Settings.Default.CBB_TS_upper_I;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(Properties.Settings.Default.TB_TS_upper_I, CBB_index, 기준금);
                    down_value = 기준금_변환(Properties.Settings.Default.TB_TS_down_I, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = Properties.Settings.Default.TB_TS_ratio_I;
                    익절비중단위 = Properties.Settings.Default.CBB_TS_ratio_I;
                    주문_Value = Properties.Settings.Default.TB_TS_Jumun_I;
                    주문 = Properties.Settings.Default.CBB_TS_Jumun_I;
                    검색식 = "트레일링스탑_I";

                    if (트레일링스탑()) 잔고.TS_9 = false;
                }

                bool 트레일링스탑()
                {
                    bool Run = false;
                    트레일링_값갱신(잔고);

                    if (트레일값_ > upper_value)
                    {
                        if (트레일값_ + down_value >= 잔고평가값)
                        {
                            if (Properties.Settings.Default.CB_TS_손실제한)
                            {
                                if (잔고.수익률 > 0 && 잔고.예상손익 > 0)
                                {
                                    TS_주문();
                                }
                                else
                                {
                                    bool true_ = true;
                                    if (검색식.Contains("A")) { true_ = 잔고.TS_알림_A; 잔고.TS_알림_A = false; }
                                    if (검색식.Contains("B")) { true_ = 잔고.TS_알림_B; 잔고.TS_알림_B = false; }
                                    if (검색식.Contains("C")) { true_ = 잔고.TS_알림_C; 잔고.TS_알림_C = false; }
                                    if (검색식.Contains("D")) { true_ = 잔고.TS_알림_D; 잔고.TS_알림_D = false; }
                                    if (검색식.Contains("E")) { true_ = 잔고.TS_알림_E; 잔고.TS_알림_E = false; }
                                    if (검색식.Contains("F")) { true_ = 잔고.TS_알림_F; 잔고.TS_알림_F = false; }
                                    if (검색식.Contains("G")) { true_ = 잔고.TS_알림_G; 잔고.TS_알림_G = false; }
                                    if (검색식.Contains("H")) { true_ = 잔고.TS_알림_H; 잔고.TS_알림_H = false; }
                                    if (검색식.Contains("I")) { true_ = 잔고.TS_알림_I; 잔고.TS_알림_I = false; }

                                    if (true_)
                                    {
                                        GridView_Print.DGV_Jumun(DateTime.Now.ToString("HH:mm:ss"), "취소", 잔고.종목명, 잔고.현재가, GET.주문유형(2), "차수조정", 잔고.현재가, 0, 잔고.종목코드, "손실제한으로 TS 취소 됩니다. ", 0, 0, 검색식 + "_취소", "-", 주문_Value, 주문, 0);
                                    }
                                }
                            }
                            else
                            {
                                TS_주문();
                            }

                            void TS_주문()
                            {
                                int 취소시간 = Properties.Settings.Default.MTB_TS_canceltime;
                                int 취소N주문 = Properties.Settings.Default.CBB_TS_cancel_sell;
                                int 반복 = Properties.Settings.Default.MTB_TS_repeat;

                                if (Properties.Settings.Default.CB_TS_취소후)
                                {
                                    List<JumunItem> 취소번호 = Form1.JumunItem_List.FindAll(o => o.종목코드.Equals(잔고.종목코드));

                                    if (취소번호.Count > 0)
                                    {
                                        if (잔고.매매가능)
                                        {
                                            잔고.매매가능 = false;

                                            for (int n = 0; n < 취소번호.Count; n++)
                                            {
                                                if (취소번호[n].취소timer > 0)
                                                {
                                                    취소번호[n].취소timer = 0;
                                                    취소번호[n].취소시간 = 0;
                                                    취소번호[n].반복횟수 = 0;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Order 취소종목 = Form1.Order_list.Find(o => o.종목코드.Equals(잔고.종목코드));
                                        if (취소종목 == null)
                                        {
                                            익절N보전_sendorder(잔고, 주문_Value, 주문, 익절비중단위, 익절비중, "C" + 검색식, 취소시간, 취소N주문, 반복);
                                            Run = true;
                                            잔고.매매가능 = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (잔고.매매가능)
                                    {
                                        익절N보전_sendorder(잔고, 주문_Value, 주문, 익절비중단위, 익절비중, 검색식, 취소시간, 취소N주문, 반복);
                                        Run = true;
                                    }
                                }
                            }
                        }
                    }

                    return Run;
                }
            }
        }

        public static double 기준금_변환(double value, int index, bool 기준금)
        {
            long 매수기준금 = Properties.Settings.Default.MT_buying_standard;

            if (index != 0)
            {
                if (기준금)
                {
                    value = (value / 100 * 매수기준금);
                }
                else
                {
                    value = value * 10000;
                }
            }
            return value;
        }

        ///////////////                트레일링 스탑               ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////





    }
}
