using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace 지니_64
{
    class Tab_AccountManagement
    {
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ///////////////            계좌 리밸런싱 관리              ////////////////

        public static void Rebalancing_condition(string ST_ID, string itemcode, string condition)
        {
            bool ID_A = false;
            bool ID_B = false;
            bool ID_C = false;
            bool ID_D = false;
            bool ID_E = false;
            bool ID_F = false;
            bool ID_G = false;

            if (ST_ID.Equals("I"))
            {
                if (Properties.Settings.Default.combo_rebalance_use_condition_A == 1) ID_A = true;
                if (Properties.Settings.Default.combo_rebalance_use_condition_B == 1) ID_B = true;
                if (Properties.Settings.Default.combo_rebalance_use_condition_C == 1) ID_C = true;
                if (Properties.Settings.Default.combo_rebalance_use_condition_D == 1) ID_D = true;
                if (Properties.Settings.Default.combo_rebalance_use_condition_E == 1) ID_E = true;
                if (Properties.Settings.Default.combo_rebalance_use_condition_F == 1) ID_F = true;
                if (Properties.Settings.Default.combo_rebalance_use_condition_G == 1) ID_G = true;
            }
            else if (ST_ID.Equals("D"))
            {
                if (Properties.Settings.Default.combo_rebalance_use_condition_A == 2) ID_A = true;
                if (Properties.Settings.Default.combo_rebalance_use_condition_B == 2) ID_B = true;
                if (Properties.Settings.Default.combo_rebalance_use_condition_C == 2) ID_C = true;
                if (Properties.Settings.Default.combo_rebalance_use_condition_D == 2) ID_D = true;
                if (Properties.Settings.Default.combo_rebalance_use_condition_E == 2) ID_E = true;
                if (Properties.Settings.Default.combo_rebalance_use_condition_F == 2) ID_F = true;
                if (Properties.Settings.Default.combo_rebalance_use_condition_G == 2) ID_G = true;
            }

            if (condition.Equals(Properties.Settings.Default.combo_rebalance_condition_A) && Properties.Settings.Default.combo_rebalance_use_condition_A > 0)
            {
                Rebal_condition_A 종목 = Form1.Rebal_condition_List_A.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_A)
                    {
                        Rebal_condition_A 추가 = new Rebal_condition_A(itemcode, 0);
                        Form1.Rebal_condition_List_A.Add(추가);
                    }
                }
                else
                {
                    if (!ID_A)
                    {
                        Rebal_condition_A 삭제 = Form1.Rebal_condition_List_A.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Rebal_condition_List_A.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_rebalance_condition_B) && Properties.Settings.Default.combo_rebalance_use_condition_B > 0)
            {
                Rebal_condition_B 종목 = Form1.Rebal_condition_List_B.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_B) //진입 사용
                    {
                        Rebal_condition_B 추가 = new Rebal_condition_B(itemcode, 0);
                        Form1.Rebal_condition_List_B.Add(추가);
                    }
                }
                else
                {
                    if (!ID_B)
                    {
                        Rebal_condition_B 삭제 = Form1.Rebal_condition_List_B.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Rebal_condition_List_B.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_rebalance_condition_C) && Properties.Settings.Default.combo_rebalance_use_condition_C > 0)
            {
                Rebal_condition_C 종목 = Form1.Rebal_condition_List_C.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_C) //진입 사용
                    {
                        Rebal_condition_C 추가 = new Rebal_condition_C(itemcode, 0);
                        Form1.Rebal_condition_List_C.Add(추가);
                    }
                }
                else
                {
                    if (!ID_C)
                    {
                        Rebal_condition_C 삭제 = Form1.Rebal_condition_List_C.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Rebal_condition_List_C.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_rebalance_condition_D) && Properties.Settings.Default.combo_rebalance_use_condition_D > 0)
            {
                Rebal_condition_D 종목 = Form1.Rebal_condition_List_D.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_D) //진입 사용
                    {
                        Rebal_condition_D 추가 = new Rebal_condition_D(itemcode, 0);
                        Form1.Rebal_condition_List_D.Add(추가);
                    }
                }
                else
                {
                    if (!ID_D)
                    {
                        Rebal_condition_D 삭제 = Form1.Rebal_condition_List_D.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Rebal_condition_List_D.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_rebalance_condition_E) && Properties.Settings.Default.combo_rebalance_use_condition_E > 0)
            {
                Rebal_condition_E 종목 = Form1.Rebal_condition_List_E.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_E) //진입 사용
                    {
                        Rebal_condition_E 추가 = new Rebal_condition_E(itemcode, 0);
                        Form1.Rebal_condition_List_E.Add(추가);
                    }
                }
                else
                {
                    if (!ID_E)
                    {
                        Rebal_condition_E 삭제 = Form1.Rebal_condition_List_E.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Rebal_condition_List_E.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_rebalance_condition_F) && Properties.Settings.Default.combo_rebalance_use_condition_F > 0)
            {
                Rebal_condition_F 종목 = Form1.Rebal_condition_List_F.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_F) //진입 사용
                    {
                        Rebal_condition_F 추가 = new Rebal_condition_F(itemcode, 0);
                        Form1.Rebal_condition_List_F.Add(추가);
                    }
                }
                else
                {
                    if (!ID_F)
                    {
                        Rebal_condition_F 삭제 = Form1.Rebal_condition_List_F.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Rebal_condition_List_F.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_rebalance_condition_G) && Properties.Settings.Default.combo_rebalance_use_condition_G > 0)
            {
                Rebal_condition_G 종목 = Form1.Rebal_condition_List_G.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_G) //진입 사용
                    {
                        Rebal_condition_G 추가 = new Rebal_condition_G(itemcode, 0);
                        Form1.Rebal_condition_List_G.Add(추가);
                    }
                }
                else
                {
                    if (!ID_G)
                    {
                        Rebal_condition_G 삭제 = Form1.Rebal_condition_List_G.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Rebal_condition_List_G.Remove(삭제);
                        }
                    }
                }
            }
        }

        private static bool 감시리스트체크(string Code, string 위치, bool 매도체크) // 감시주문이 매도 체결 되었는지 체크후 매수 주문합니다.
        {
            bool result = true;

            if (매도체크) // 주문이 있을때 false
            {
                List<감시주문> 종목감시_LIST = Form1.감시주문_List.FindAll(o => o.종목코드.Equals(Code));
                if (종목감시_LIST.Count > 0)
                {
                    감시주문 감시 = 종목감시_LIST.Find(o => o.검색식.Contains(위치));

                    if (감시 != null) result = false;
                }
            }

            return result;
        }


        public static void Rebalancing_USE(Stockbalance 잔고)
        {
            if (잔고.주문가능수량 > 0 && Form1.재시작 && 잔고.매매가능)
            {
                Market_Item market_Item = Form1.Market_Item_List[잔고.종목코드];
                long 매수기준금 = Properties.Settings.Default.MT_buying_standard;
                long 매입금 = (long)(Properties.Settings.Default.TB_rebalance_매입금_A / 100 * 매수기준금);

                int start = Properties.Settings.Default.MT_rebalance_starttime_A;
                int end = Properties.Settings.Default.MT_rebalance_stoptime_A;
                bool 매수도 = true; //매수
                int Repeat_use = Properties.Settings.Default.combo_rebalance_use_condition_A;
                double Value = Properties.Settings.Default.TB_rebalance_value_A;
                int jumun = Properties.Settings.Default.combo_rebalance_jumun_A;
                double suik_low = Properties.Settings.Default.TB_rebalance_suik_1_A;
                double suik_height = Properties.Settings.Default.TB_rebalance_suik_2_A;
                bool suik_choice = Properties.Settings.Default.CB_rebalance_choice_A;
                int suik_gubun = Properties.Settings.Default.combo_rebalance_suik_gubun_A;
                double ratio = Properties.Settings.Default.TB_rebalance_sell_ratio_A;
                int gubun = Properties.Settings.Default.combo_rebalance_sell_gubun_A;
                double maemae_low = Properties.Settings.Default.TB_rebalance_maemae_1_A;
                double maemae_height = Properties.Settings.Default.TB_rebalance_maemae_2_A;
                int maemae_gubun = Properties.Settings.Default.combo_rebalance_maemae_gubun_A;
                int 취소N주문 = 0;
                int 취소시간 = Properties.Settings.Default.MTB_rebalance_Cancel_time_A;
                int 반복 = 0;
                int 누적거래량 = Properties.Settings.Default.TB_rebalance_누적거래량_A;
                int 누적거래대금 = Properties.Settings.Default.TB_rebalance_누적거래대금_A;
                bool 매도체크 = Properties.Settings.Default.CB_rebalance_매도체크_A;

                ma min_mma = Form1.Min_ma_list[잔고.종목코드];
                ma day_mma = Form1.Day_ma_list[잔고.종목코드];
                int CBB_mma = Properties.Settings.Default.CBB_rebalance_mma_A;
                int CBB_mma2 = Properties.Settings.Default.CBB_rebalance_mma2_A;
                int CBB_mma_배열 = Properties.Settings.Default.CBB_rebalance_mma_배열_A;
                double min_mma1 = min_mma.Rebal_ma1_A;
                double min_mma2 = min_mma.Rebal_ma2_A;

                int CBB_dma1 = Properties.Settings.Default.CBB_rebalance_dma1_A;
                int CBB_dma2 = Properties.Settings.Default.CBB_rebalance_dma2_A;
                int CBB_dma_배열 = Properties.Settings.Default.CBB_rebalance_dma_배열_A;
                double day_mma1 = day_mma.Rebal_ma1_A;
                double day_mma2 = day_mma.Rebal_ma2_A;

                string location = "리밸_A";
                string group = GET.익절그룹(location);
                string 검색식 = Properties.Settings.Default.combo_rebalance_condition_A;
                if (Repeat_use == 0) 검색식 = "";

                if (Properties.Settings.Default.CB_rebalance_A && 잔고.가동_리밸A && Method.추매가능_Check(잔고, true) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                {
                    if (Method.RunTime(start, end))
                    {
                        if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                        {
                            if (잔고.매입금액 >= 매입금 && 감시리스트체크(잔고.종목코드, location, 매도체크))
                            {
                                if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                {
                                    if (Repeat_use > 0) // 검색식 사용 
                                    {
                                        Rebal_condition_A Item = Form1.Rebal_condition_List_A.Find(o => o.code.Equals(잔고.종목코드));
                                        if (Item != null)
                                        {
                                            if (Properties.Settings.Default.MTB_rebalance_delay_A <= Item.timer)
                                            {
                                                if (Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                 gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식))
                                                {
                                                    Form1.Rebal_condition_List_A.Remove(Item);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식);
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_rebalance_B && 잔고.가동_리밸B)
                {
                    location = "리밸_B";
                    검색식 = Properties.Settings.Default.combo_rebalance_condition_B;
                    Repeat_use = Properties.Settings.Default.combo_rebalance_use_condition_B;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, true) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_rebalance_starttime_B;
                        end = Properties.Settings.Default.MT_rebalance_stoptime_B;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_rebalance_mma_B;
                            CBB_mma2 = Properties.Settings.Default.CBB_rebalance_mma2_B;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_rebalance_mma_배열_B;
                            min_mma1 = min_mma.Rebal_ma1_B;
                            min_mma2 = min_mma.Rebal_ma2_B;
                            CBB_dma1 = Properties.Settings.Default.CBB_rebalance_dma1_B;
                            CBB_dma2 = Properties.Settings.Default.CBB_rebalance_dma2_B;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_rebalance_dma_배열_B;
                            day_mma1 = day_mma.Rebal_ma1_B;
                            day_mma2 = day_mma.Rebal_ma2_B;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_rebalance_매입금_B / 100 * 매수기준금);
                                매도체크 = Properties.Settings.Default.CB_rebalance_매도체크_B;

                                if (잔고.매입금액 >= 매입금 && 감시리스트체크(잔고.종목코드, location, 매도체크))
                                {
                                    누적거래량 = Properties.Settings.Default.TB_rebalance_누적거래량_B;
                                    누적거래대금 = Properties.Settings.Default.TB_rebalance_누적거래대금_B;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_rebalance_value_B;
                                        jumun = Properties.Settings.Default.combo_rebalance_jumun_B;
                                        suik_low = Properties.Settings.Default.TB_rebalance_suik_1_B;
                                        suik_height = Properties.Settings.Default.TB_rebalance_suik_2_B;
                                        suik_choice = Properties.Settings.Default.CB_rebalance_choice_B;
                                        suik_gubun = Properties.Settings.Default.combo_rebalance_suik_gubun_B;
                                        ratio = Properties.Settings.Default.TB_rebalance_sell_ratio_B;
                                        gubun = Properties.Settings.Default.combo_rebalance_sell_gubun_B;
                                        maemae_low = Properties.Settings.Default.TB_rebalance_maemae_1_B;
                                        maemae_height = Properties.Settings.Default.TB_rebalance_maemae_2_B;
                                        maemae_gubun = Properties.Settings.Default.combo_rebalance_maemae_gubun_B;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_rebalance_Cancel_time_B;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Rebal_condition_B Item = Form1.Rebal_condition_List_B.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_rebalance_delay_B <= Item.timer)
                                                {
                                                    if (Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식))
                                                    {
                                                        Form1.Rebal_condition_List_B.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_rebalance_C && 잔고.가동_리밸C)
                {
                    location = "리밸_C";
                    검색식 = Properties.Settings.Default.combo_rebalance_condition_C;
                    Repeat_use = Properties.Settings.Default.combo_rebalance_use_condition_C;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, true) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_rebalance_starttime_C;
                        end = Properties.Settings.Default.MT_rebalance_stoptime_C;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_rebalance_mma_C;
                            CBB_mma2 = Properties.Settings.Default.CBB_rebalance_mma2_C;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_rebalance_mma_배열_C;
                            min_mma1 = min_mma.Rebal_ma1_C;
                            min_mma2 = min_mma.Rebal_ma2_C;
                            CBB_dma1 = Properties.Settings.Default.CBB_rebalance_dma1_C;
                            CBB_dma2 = Properties.Settings.Default.CBB_rebalance_dma2_C;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_rebalance_dma_배열_C;
                            day_mma1 = day_mma.Rebal_ma1_C;
                            day_mma2 = day_mma.Rebal_ma2_C;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_rebalance_매입금_C / 100 * 매수기준금);
                                매도체크 = Properties.Settings.Default.CB_rebalance_매도체크_C;

                                if (잔고.매입금액 >= 매입금 && 감시리스트체크(잔고.종목코드, location, 매도체크))
                                {
                                    누적거래량 = Properties.Settings.Default.TB_rebalance_누적거래량_C;
                                    누적거래대금 = Properties.Settings.Default.TB_rebalance_누적거래대금_C;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Repeat_use = Properties.Settings.Default.combo_rebalance_use_condition_C;
                                        Value = Properties.Settings.Default.TB_rebalance_value_C;
                                        jumun = Properties.Settings.Default.combo_rebalance_jumun_C;
                                        suik_low = Properties.Settings.Default.TB_rebalance_suik_1_C;
                                        suik_height = Properties.Settings.Default.TB_rebalance_suik_2_C;
                                        suik_choice = Properties.Settings.Default.CB_rebalance_choice_C;
                                        suik_gubun = Properties.Settings.Default.combo_rebalance_suik_gubun_C;
                                        ratio = Properties.Settings.Default.TB_rebalance_sell_ratio_C;
                                        gubun = Properties.Settings.Default.combo_rebalance_sell_gubun_C;
                                        maemae_low = Properties.Settings.Default.TB_rebalance_maemae_1_C;
                                        maemae_height = Properties.Settings.Default.TB_rebalance_maemae_2_C;
                                        maemae_gubun = Properties.Settings.Default.combo_rebalance_maemae_gubun_C;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_rebalance_Cancel_time_C;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Rebal_condition_C Item = Form1.Rebal_condition_List_C.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_rebalance_delay_C <= Item.timer)
                                                {
                                                    if (Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식))
                                                    {
                                                        Form1.Rebal_condition_List_C.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_rebalance_D && 잔고.가동_리밸D)
                {
                    location = "리밸_D";
                    검색식 = Properties.Settings.Default.combo_rebalance_condition_D;
                    Repeat_use = Properties.Settings.Default.combo_rebalance_use_condition_D;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, true) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_rebalance_starttime_D;
                        end = Properties.Settings.Default.MT_rebalance_stoptime_D;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_rebalance_mma_D;
                            CBB_mma2 = Properties.Settings.Default.CBB_rebalance_mma2_D;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_rebalance_mma_배열_D;
                            min_mma1 = min_mma.Rebal_ma1_D;
                            min_mma2 = min_mma.Rebal_ma2_D;
                            CBB_dma1 = Properties.Settings.Default.CBB_rebalance_dma1_D;
                            CBB_dma2 = Properties.Settings.Default.CBB_rebalance_dma2_D;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_rebalance_dma_배열_D;
                            day_mma1 = day_mma.Rebal_ma1_D;
                            day_mma2 = day_mma.Rebal_ma2_D;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_rebalance_매입금_D / 100 * 매수기준금);
                                매도체크 = Properties.Settings.Default.CB_rebalance_매도체크_D;

                                if (잔고.매입금액 >= 매입금 && 감시리스트체크(잔고.종목코드, location, 매도체크))
                                {
                                    누적거래량 = Properties.Settings.Default.TB_rebalance_누적거래량_D;
                                    누적거래대금 = Properties.Settings.Default.TB_rebalance_누적거래대금_D;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Repeat_use = Properties.Settings.Default.combo_rebalance_use_condition_D;
                                        Value = Properties.Settings.Default.TB_rebalance_value_D;
                                        jumun = Properties.Settings.Default.combo_rebalance_jumun_D;
                                        suik_low = Properties.Settings.Default.TB_rebalance_suik_1_D;
                                        suik_height = Properties.Settings.Default.TB_rebalance_suik_2_D;
                                        suik_choice = Properties.Settings.Default.CB_rebalance_choice_D;
                                        suik_gubun = Properties.Settings.Default.combo_rebalance_suik_gubun_D;
                                        ratio = Properties.Settings.Default.TB_rebalance_sell_ratio_D;
                                        gubun = Properties.Settings.Default.combo_rebalance_sell_gubun_D;
                                        maemae_low = Properties.Settings.Default.TB_rebalance_maemae_1_D;
                                        maemae_height = Properties.Settings.Default.TB_rebalance_maemae_2_D;
                                        maemae_gubun = Properties.Settings.Default.combo_rebalance_maemae_gubun_D;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_rebalance_Cancel_time_D;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Rebal_condition_D Item = Form1.Rebal_condition_List_D.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_rebalance_delay_D <= Item.timer)
                                                {
                                                    if (Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식))
                                                    {
                                                        Form1.Rebal_condition_List_D.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_rebalance_E && 잔고.가동_리밸E)
                {
                    location = "리밸_E";
                    검색식 = Properties.Settings.Default.combo_rebalance_condition_E;
                    Repeat_use = Properties.Settings.Default.combo_rebalance_use_condition_E;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, true) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_rebalance_starttime_E;
                        end = Properties.Settings.Default.MT_rebalance_stoptime_E;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_rebalance_mma_E;
                            CBB_mma2 = Properties.Settings.Default.CBB_rebalance_mma2_E;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_rebalance_mma_배열_E;
                            min_mma1 = min_mma.Rebal_ma1_E;
                            min_mma2 = min_mma.Rebal_ma2_E;
                            CBB_dma1 = Properties.Settings.Default.CBB_rebalance_dma1_E;
                            CBB_dma2 = Properties.Settings.Default.CBB_rebalance_dma2_E;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_rebalance_dma_배열_E;
                            day_mma1 = day_mma.Rebal_ma1_E;
                            day_mma2 = day_mma.Rebal_ma2_E;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_rebalance_매입금_E / 100 * 매수기준금);
                                매도체크 = Properties.Settings.Default.CB_rebalance_매도체크_E;

                                if (잔고.매입금액 >= 매입금 && 감시리스트체크(잔고.종목코드, location, 매도체크))
                                {
                                    누적거래량 = Properties.Settings.Default.TB_rebalance_누적거래량_E;
                                    누적거래대금 = Properties.Settings.Default.TB_rebalance_누적거래대금_E;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Repeat_use = Properties.Settings.Default.combo_rebalance_use_condition_E;
                                        Value = Properties.Settings.Default.TB_rebalance_value_E;
                                        jumun = Properties.Settings.Default.combo_rebalance_jumun_E;
                                        suik_low = Properties.Settings.Default.TB_rebalance_suik_1_E;
                                        suik_height = Properties.Settings.Default.TB_rebalance_suik_2_E;
                                        suik_choice = Properties.Settings.Default.CB_rebalance_choice_E;
                                        suik_gubun = Properties.Settings.Default.combo_rebalance_suik_gubun_E;
                                        ratio = Properties.Settings.Default.TB_rebalance_sell_ratio_E;
                                        gubun = Properties.Settings.Default.combo_rebalance_sell_gubun_E;
                                        maemae_low = Properties.Settings.Default.TB_rebalance_maemae_1_E;
                                        maemae_height = Properties.Settings.Default.TB_rebalance_maemae_2_E;
                                        maemae_gubun = Properties.Settings.Default.combo_rebalance_maemae_gubun_E;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_rebalance_Cancel_time_E;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Rebal_condition_E Item = Form1.Rebal_condition_List_E.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_rebalance_delay_E <= Item.timer)
                                                {
                                                    if (Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식))
                                                    {
                                                        Form1.Rebal_condition_List_E.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_rebalance_F && 잔고.가동_리밸F)
                {
                    location = "리밸_F";
                    검색식 = Properties.Settings.Default.combo_rebalance_condition_F;
                    Repeat_use = Properties.Settings.Default.combo_rebalance_use_condition_F;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, true) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_rebalance_starttime_F;
                        end = Properties.Settings.Default.MT_rebalance_stoptime_F;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_rebalance_mma_F;
                            CBB_mma2 = Properties.Settings.Default.CBB_rebalance_mma2_F;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_rebalance_mma_배열_F;
                            min_mma1 = min_mma.Rebal_ma1_F;
                            min_mma2 = min_mma.Rebal_ma2_F;
                            CBB_dma1 = Properties.Settings.Default.CBB_rebalance_dma1_F;
                            CBB_dma2 = Properties.Settings.Default.CBB_rebalance_dma2_F;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_rebalance_dma_배열_F;
                            day_mma1 = day_mma.Rebal_ma1_F;
                            day_mma2 = day_mma.Rebal_ma2_F;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_rebalance_매입금_F / 100 * 매수기준금);
                                매도체크 = Properties.Settings.Default.CB_rebalance_매도체크_F;

                                if (잔고.매입금액 >= 매입금 && 감시리스트체크(잔고.종목코드, location, 매도체크))
                                {
                                    누적거래량 = Properties.Settings.Default.TB_rebalance_누적거래량_F;
                                    누적거래대금 = Properties.Settings.Default.TB_rebalance_누적거래대금_F;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Repeat_use = Properties.Settings.Default.combo_rebalance_use_condition_F;
                                        Value = Properties.Settings.Default.TB_rebalance_value_F;
                                        jumun = Properties.Settings.Default.combo_rebalance_jumun_F;
                                        suik_low = Properties.Settings.Default.TB_rebalance_suik_1_F;
                                        suik_height = Properties.Settings.Default.TB_rebalance_suik_2_F;
                                        suik_choice = Properties.Settings.Default.CB_rebalance_choice_F;
                                        suik_gubun = Properties.Settings.Default.combo_rebalance_suik_gubun_F;
                                        ratio = Properties.Settings.Default.TB_rebalance_sell_ratio_F;
                                        gubun = Properties.Settings.Default.combo_rebalance_sell_gubun_F;
                                        maemae_low = Properties.Settings.Default.TB_rebalance_maemae_1_F;
                                        maemae_height = Properties.Settings.Default.TB_rebalance_maemae_2_F;
                                        maemae_gubun = Properties.Settings.Default.combo_rebalance_maemae_gubun_F;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_rebalance_Cancel_time_F;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Rebal_condition_F Item = Form1.Rebal_condition_List_F.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_rebalance_delay_F <= Item.timer)
                                                {
                                                    if (Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식))
                                                    {
                                                        Form1.Rebal_condition_List_F.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_rebalance_G && 잔고.가동_리밸G)
                {
                    location = "리밸_G";
                    검색식 = Properties.Settings.Default.combo_rebalance_condition_G;
                    Repeat_use = Properties.Settings.Default.combo_rebalance_use_condition_G;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, true) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_rebalance_starttime_G;
                        end = Properties.Settings.Default.MT_rebalance_stoptime_G;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_rebalance_mma_G;
                            CBB_mma2 = Properties.Settings.Default.CBB_rebalance_mma2_G;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_rebalance_mma_배열_G;
                            min_mma1 = min_mma.Rebal_ma1_G;
                            min_mma2 = min_mma.Rebal_ma2_G;
                            CBB_dma1 = Properties.Settings.Default.CBB_rebalance_dma1_G;
                            CBB_dma2 = Properties.Settings.Default.CBB_rebalance_dma2_G;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_rebalance_dma_배열_G;
                            day_mma1 = day_mma.Rebal_ma1_G;
                            day_mma2 = day_mma.Rebal_ma2_G;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_rebalance_매입금_G / 100 * 매수기준금);
                                매도체크 = Properties.Settings.Default.CB_rebalance_매도체크_G;

                                if (잔고.매입금액 >= 매입금 && 감시리스트체크(잔고.종목코드, location, 매도체크))
                                {
                                    누적거래량 = Properties.Settings.Default.TB_rebalance_누적거래량_G;
                                    누적거래대금 = Properties.Settings.Default.TB_rebalance_누적거래대금_G;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Repeat_use = Properties.Settings.Default.combo_rebalance_use_condition_G;
                                        Value = Properties.Settings.Default.TB_rebalance_value_G;
                                        jumun = Properties.Settings.Default.combo_rebalance_jumun_G;
                                        suik_low = Properties.Settings.Default.TB_rebalance_suik_1_G;
                                        suik_height = Properties.Settings.Default.TB_rebalance_suik_2_G;
                                        suik_choice = Properties.Settings.Default.CB_rebalance_choice_G;
                                        suik_gubun = Properties.Settings.Default.combo_rebalance_suik_gubun_G;
                                        ratio = Properties.Settings.Default.TB_rebalance_sell_ratio_G;
                                        gubun = Properties.Settings.Default.combo_rebalance_sell_gubun_G;
                                        maemae_low = Properties.Settings.Default.TB_rebalance_maemae_1_G;
                                        maemae_height = Properties.Settings.Default.TB_rebalance_maemae_2_G;
                                        maemae_gubun = Properties.Settings.Default.combo_rebalance_maemae_gubun_G;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_rebalance_Cancel_time_G;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Rebal_condition_G Item = Form1.Rebal_condition_List_G.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_rebalance_delay_G <= Item.timer)
                                                {
                                                    if (Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식))
                                                    {
                                                        Form1.Rebal_condition_List_G.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Tab_Repeat.반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, 검색식);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void 리밸등록(string 주문번호, string screennum, string 코드, string location, int 매수수량, int 주문가격, int 수익구분)
        {
            if (location.Contains("리밸"))
            {
                if (!location.Contains("[감시]") && !location.Contains("매도감시"))
                {
                    string 리밸매도기준_1 = Properties.Settings.Default.리밸매도기준1_A;
                    string 리밸매도기준_2 = Properties.Settings.Default.리밸매도기준2_A;
                    double 비중_1 = Properties.Settings.Default.TB_rebalance_sellvolume1_A;
                    double 비중_2 = Properties.Settings.Default.TB_rebalance_sellvolume2_A;
                    bool result = false;

                    if (location.Contains("리밸_A"))
                    {
                        result = true;
                    }

                    if (location.Contains("리밸_B"))
                    {
                        리밸매도기준_1 = Properties.Settings.Default.리밸매도기준1_B;
                        리밸매도기준_2 = Properties.Settings.Default.리밸매도기준2_B;
                        비중_1 = Properties.Settings.Default.TB_rebalance_sellvolume1_B;
                        비중_2 = Properties.Settings.Default.TB_rebalance_sellvolume2_B;
                        result = true;
                    }

                    if (location.Contains("리밸_C"))
                    {
                        리밸매도기준_1 = Properties.Settings.Default.리밸매도기준1_C;
                        리밸매도기준_2 = Properties.Settings.Default.리밸매도기준2_C;
                        비중_1 = Properties.Settings.Default.TB_rebalance_sellvolume1_C;
                        비중_2 = Properties.Settings.Default.TB_rebalance_sellvolume2_C;
                        result = true;
                    }

                    if (location.Contains("리밸_D"))
                    {
                        리밸매도기준_1 = Properties.Settings.Default.리밸매도기준1_D;
                        리밸매도기준_2 = Properties.Settings.Default.리밸매도기준2_D;
                        비중_1 = Properties.Settings.Default.TB_rebalance_sellvolume1_D;
                        비중_2 = Properties.Settings.Default.TB_rebalance_sellvolume2_D;
                        result = true;
                    }

                    if (location.Contains("리밸_E"))
                    {
                        리밸매도기준_1 = Properties.Settings.Default.리밸매도기준1_E;
                        리밸매도기준_2 = Properties.Settings.Default.리밸매도기준2_E;
                        비중_1 = Properties.Settings.Default.TB_rebalance_sellvolume1_E;
                        비중_2 = Properties.Settings.Default.TB_rebalance_sellvolume2_E;
                        result = true;
                    }

                    if (location.Contains("리밸_F"))
                    {
                        리밸매도기준_1 = Properties.Settings.Default.리밸매도기준1_F;
                        리밸매도기준_2 = Properties.Settings.Default.리밸매도기준2_F;
                        비중_1 = Properties.Settings.Default.TB_rebalance_sellvolume1_F;
                        비중_2 = Properties.Settings.Default.TB_rebalance_sellvolume2_F;
                        result = true;
                    }

                    if (location.Contains("리밸_G"))
                    {
                        리밸매도기준_1 = Properties.Settings.Default.리밸매도기준1_G;
                        리밸매도기준_2 = Properties.Settings.Default.리밸매도기준2_G;
                        비중_1 = Properties.Settings.Default.TB_rebalance_sellvolume1_G;
                        비중_2 = Properties.Settings.Default.TB_rebalance_sellvolume2_G;
                        result = true;
                    }

                    if (result)
                    {
                        int 수량_1 = 0;
                        int 수량_2 = 0;

                        if (!리밸매도기준_1.Equals("(    X    )"))
                        {
                            if (비중_1 == 100) 수량_1 = 매수수량;
                            else
                            {
                                수량_1 = (int)Math.Truncate(매수수량 * 비중_1 / 100); //버림
                            }

                            if (!리밸매도기준_2.Equals("(    X    )"))
                            {
                                if (비중_2 == 100) 수량_2 = 매수수량;
                                else
                                {
                                    if (비중_1 + 비중_2 == 100) 수량_2 = 매수수량 - 수량_1;
                                    else
                                    {
                                        수량_2 = (int)Math.Truncate(매수수량 * 비중_2 / 100); //버림
                                    }
                                }
                            }

                            Rebal_Sell _rebalance_List = new Rebal_Sell(location, 코드, screennum, 주문번호, 수량_1, 수량_2, 주문가격, 수익구분);
                            Form1.form1.Rebal_Sell_List.Add(_rebalance_List);
                        }
                    }
                }
            }
        }

        public static void 리밸매도(string 코드, int 체_체결가, int 체_단위체결량, int 체_체결량, int 체_주문수량, string 화면번호)
        {
            Rebal_Sell Rebal = Form1.form1.Rebal_Sell_List.Find(o => o.code.Equals(코드) && o.screennum.Equals(화면번호));
            if (Rebal != null)
            {
                int 주문수량 = 0;
                bool 단위체결 = Properties.Settings.Default.CB_rebalance_option_A;
                double A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_A;

                if (Rebal.location.Contains("리밸_A"))
                { }

                if (Rebal.location.Contains("리밸_B"))
                {
                    단위체결 = Properties.Settings.Default.CB_rebalance_option_B;
                    A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_B;
                }

                if (Rebal.location.Contains("리밸_C"))
                {
                    단위체결 = Properties.Settings.Default.CB_rebalance_option_C;
                    A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_C;
                }

                if (Rebal.location.Contains("리밸_D"))
                {
                    단위체결 = Properties.Settings.Default.CB_rebalance_option_D;
                    A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_D;
                }

                if (Rebal.location.Contains("리밸_E"))
                {
                    단위체결 = Properties.Settings.Default.CB_rebalance_option_E;
                    A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_E;
                }

                if (Rebal.location.Contains("리밸_F"))
                {
                    단위체결 = Properties.Settings.Default.CB_rebalance_option_F;
                    A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_F;
                }

                if (Rebal.location.Contains("리밸_G"))
                {
                    단위체결 = Properties.Settings.Default.CB_rebalance_option_G;
                    A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_G;
                }

                if (단위체결) // 단위체결 후 주문
                {
                    if (Rebal._1차수량 > 체_단위체결량 && Rebal._1차수량 > 0)
                    {
                        주문수량 = 체_단위체결량;
                        Rebal._1차수량 = Rebal._1차수량 - 체_단위체결량;

                        리밸_오더(코드, 주문수량, 체_체결가, Rebal.location + "_1차", 0, Rebal.수익구분);
                    }
                    else
                    {
                        주문수량 = Rebal._1차수량;
                        체_단위체결량 = 체_단위체결량 - Rebal._1차수량;
                        Rebal._1차수량 = 0;

                        if (주문수량 > 0) 리밸_오더(코드, 주문수량, 체_체결가, Rebal.location + "_1차", 0, Rebal.수익구분);

                        if (Rebal._1차수량 == 0 && Rebal._2차수량 > 0)
                        {
                            if (Rebal._2차수량 > 체_단위체결량)
                            {
                                주문수량 = 체_단위체결량;
                                Rebal._2차수량 = Rebal._2차수량 - 체_단위체결량;

                                if (주문수량 > 0) 리밸_오더(코드, 주문수량, 체_체결가, Rebal.location + "_2차", 0, Rebal.수익구분);
                            }
                            else
                            {
                                주문수량 = Rebal._2차수량;
                                체_단위체결량 = 체_단위체결량 - Rebal._2차수량;
                                Rebal._2차수량 = 0;

                                if (주문수량 > 0) 리밸_오더(코드, 주문수량, 체_체결가, Rebal.location + "_2차", 0, Rebal.수익구분);
                                Form1.form1.Rebal_Sell_List.Remove(Rebal);
                            }
                        }
                        else
                        {
                            Form1.form1.Rebal_Sell_List.Remove(Rebal);
                        }
                    }
                }
                else // 전량체결 후 주문
                {
                    if (A차수비중 == 100)
                    {
                        if (체_주문수량 == 체_체결량 && 체_체결량 >= Rebal._1차수량)
                        {
                            int 연동감시번호 = GET_연동감시번호();

                            리밸_오더(코드, Rebal._1차수량, 체_체결가, Rebal.location + "_1차", 연동감시번호, Rebal.수익구분);
                            리밸_오더(코드, Rebal._2차수량, 체_체결가, Rebal.location + "_2차", 연동감시번호, Rebal.수익구분);

                            Form1.form1.Rebal_Sell_List.Remove(Rebal);
                        }
                    }
                    else
                    {
                        if (체_주문수량 == 체_체결량 && 체_체결량 >= (Rebal._1차수량 + Rebal._2차수량))
                        {
                            리밸_오더(코드, Rebal._1차수량, 체_체결가, Rebal.location + "_1차", 0, Rebal.수익구분);

                            if (Rebal._2차수량 > 0)
                            {
                                리밸_오더(코드, Rebal._2차수량, 체_체결가, Rebal.location + "_2차", 0, Rebal.수익구분);
                            }

                            Form1.form1.Rebal_Sell_List.Remove(Rebal);
                        }
                    }
                }
            }
        }

        public static void 취소_리밸주문(string 화면번호, string 코드, string 주문유형b, int 주문수량b)
        {
            if (주문유형b.Equals("매수취소"))
            {
                Rebal_Sell Rebal = Form1.form1.Rebal_Sell_List.Find(o => o.code.Equals(코드) && o.screennum.Equals(화면번호));
                if (Rebal != null)
                {
                    double A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_A;

                    if (Rebal.location.Contains("리밸_A")) A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_A;
                    if (Rebal.location.Contains("리밸_B")) A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_B;
                    if (Rebal.location.Contains("리밸_C")) A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_C;
                    if (Rebal.location.Contains("리밸_D")) A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_D;
                    if (Rebal.location.Contains("리밸_E")) A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_E;
                    if (Rebal.location.Contains("리밸_F")) A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_F;
                    if (Rebal.location.Contains("리밸_G")) A차수비중 = Properties.Settings.Default.TB_rebalance_sellvolume1_G;

                    int 체결량 = Rebal._1차수량 + Rebal._2차수량 - 주문수량b;
                    if (A차수비중 == 100) 체결량 = Rebal._1차수량 - 주문수량b;

                    if (체결량 > 0)
                    {
                        int 주문수량 = Rebal._1차수량;

                        if (Rebal._1차수량 > 0)
                        {
                            if (체결량 < Rebal._1차수량) 주문수량 = 체결량;

                            if (A차수비중 == 100)
                            {
                                int 연동감시번호 = GET_연동감시번호();

                                리밸_오더(코드, 주문수량, Rebal.체결가격, Rebal.location + "_1차", 연동감시번호, Rebal.수익구분);
                                리밸_오더(코드, 주문수량, Rebal.체결가격, Rebal.location + "_2차", 연동감시번호, Rebal.수익구분);

                            }
                            else
                            {
                                리밸_오더(코드, 주문수량, Rebal.체결가격, Rebal.location + "_1차", 0, Rebal.수익구분);

                                if (Rebal._2차수량 > 0 && 체결량 > Rebal._1차수량)
                                {
                                    주문수량 = 체결량 - Rebal._1차수량;

                                    리밸_오더(코드, 주문수량, Rebal.체결가격, Rebal.location + "_2차", 0, Rebal.수익구분);
                                }
                            }
                        }
                    }

                    Form1.form1.Rebal_Sell_List.Remove(Rebal);
                }
            }
        }

        public static void 리밸_오더(string 종목코드, int 주문수량, int 주문체결가격, string 검색식, int 연동감시번호, int 수익구분)
        {
            Dictionary<string, Stockbalance> stockBalanceList = Form1.stockBalanceList;   // 잔고 - 보유잔고리스트
            if (stockBalanceList.TryGetValue(종목코드, out Stockbalance 잔고))
            {
                if (잔고.주문가능수량 > 0)
                {
                    bool 단위_기준 = Properties.Settings.Default.CB_rebalance_기준금;
                    double 감시_주문값 = Properties.Settings.Default.TB_rebalance_감시_value_A;
                    int 감시_주문구분 = Properties.Settings.Default.combo_rebalance_감시_jumun_A;
                    int TimeChoice = Properties.Settings.Default.CBB_rebalance_Selltime_A;
                    double 차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio1_A;
                    int 차수주문구분 = 100;
                    string 리밸매도기준 = Properties.Settings.Default.리밸매도기준1_A;
                    int 취소 = Properties.Settings.Default.TB_rebalance_sellcancel1_A;

                    bool CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_1차_A;
                    double TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_1차_down_A;
                    int TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_1차_mma_A;
                    int CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_A;

                    if (검색식.Contains("_2차"))
                    {
                        차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio2_A;
                        리밸매도기준 = Properties.Settings.Default.리밸매도기준2_A;
                        취소 = Properties.Settings.Default.TB_rebalance_sellcancel2_A;
                        CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_2차_A;
                        TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_2차_down_A;
                        TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_2차_mma_A;
                        CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_A;
                    }

                    if (검색식.Contains("리밸_B"))
                    {
                        감시_주문값 = Properties.Settings.Default.TB_rebalance_감시_value_B;
                        감시_주문구분 = Properties.Settings.Default.combo_rebalance_감시_jumun_B;
                        TimeChoice = Properties.Settings.Default.CBB_rebalance_Selltime_B;
                        차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio1_B;
                        리밸매도기준 = Properties.Settings.Default.리밸매도기준1_B;
                        취소 = Properties.Settings.Default.TB_rebalance_sellcancel1_B;
                        CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_1차_B;
                        TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_1차_down_B;
                        TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_1차_mma_B;
                        CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_B;

                        if (검색식.Contains("_2차"))
                        {
                            차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio2_B;
                            리밸매도기준 = Properties.Settings.Default.리밸매도기준2_B;
                            취소 = Properties.Settings.Default.TB_rebalance_sellcancel2_B;
                            CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_2차_B;
                            TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_2차_down_B;
                            TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_2차_mma_B;
                            CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_B;
                        }
                    }

                    if (검색식.Contains("리밸_C"))
                    {
                        감시_주문값 = Properties.Settings.Default.TB_rebalance_감시_value_C;
                        감시_주문구분 = Properties.Settings.Default.combo_rebalance_감시_jumun_C;
                        TimeChoice = Properties.Settings.Default.CBB_rebalance_Selltime_C;
                        차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio1_C;
                        리밸매도기준 = Properties.Settings.Default.리밸매도기준1_C;
                        취소 = Properties.Settings.Default.TB_rebalance_sellcancel1_C;
                        CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_1차_C;
                        TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_1차_down_C;
                        TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_1차_mma_C;
                        CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_C;

                        if (검색식.Contains("_2차"))
                        {
                            차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio2_C;
                            리밸매도기준 = Properties.Settings.Default.리밸매도기준2_C;
                            취소 = Properties.Settings.Default.TB_rebalance_sellcancel2_C;
                            CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_2차_C;
                            TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_2차_down_C;
                            TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_2차_mma_C;
                            CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_C;
                        }
                    }

                    if (검색식.Contains("리밸_D"))
                    {
                        감시_주문값 = Properties.Settings.Default.TB_rebalance_감시_value_D;
                        감시_주문구분 = Properties.Settings.Default.combo_rebalance_감시_jumun_D;
                        TimeChoice = Properties.Settings.Default.CBB_rebalance_Selltime_D;
                        차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio1_D;
                        리밸매도기준 = Properties.Settings.Default.리밸매도기준1_D;
                        취소 = Properties.Settings.Default.TB_rebalance_sellcancel1_D;
                        CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_1차_D;
                        TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_1차_down_D;
                        TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_1차_mma_D;
                        CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_D;

                        if (검색식.Contains("_2차"))
                        {
                            차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio2_D;
                            리밸매도기준 = Properties.Settings.Default.리밸매도기준2_D;
                            취소 = Properties.Settings.Default.TB_rebalance_sellcancel2_D;
                            CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_2차_D;
                            TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_2차_down_D;
                            TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_2차_mma_D;
                            CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_D;
                        }
                    }

                    if (검색식.Contains("리밸_E"))
                    {
                        감시_주문값 = Properties.Settings.Default.TB_rebalance_감시_value_E;
                        감시_주문구분 = Properties.Settings.Default.combo_rebalance_감시_jumun_E;
                        TimeChoice = Properties.Settings.Default.CBB_rebalance_Selltime_E;
                        차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio1_E;
                        리밸매도기준 = Properties.Settings.Default.리밸매도기준1_E;
                        취소 = Properties.Settings.Default.TB_rebalance_sellcancel1_E;
                        CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_1차_E;
                        TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_1차_down_E;
                        TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_1차_mma_E;
                        CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_E;

                        if (검색식.Contains("_2차"))
                        {
                            차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio2_E;
                            리밸매도기준 = Properties.Settings.Default.리밸매도기준2_E;
                            취소 = Properties.Settings.Default.TB_rebalance_sellcancel2_E;
                            CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_2차_E;
                            TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_2차_down_E;
                            TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_2차_mma_E;
                            CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_E;
                        }
                    }

                    if (검색식.Contains("리밸_F"))
                    {
                        감시_주문값 = Properties.Settings.Default.TB_rebalance_감시_value_F;
                        감시_주문구분 = Properties.Settings.Default.combo_rebalance_감시_jumun_F;
                        TimeChoice = Properties.Settings.Default.CBB_rebalance_Selltime_F;
                        차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio1_F;
                        리밸매도기준 = Properties.Settings.Default.리밸매도기준1_F;
                        취소 = Properties.Settings.Default.TB_rebalance_sellcancel1_F;
                        CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_1차_F;
                        TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_1차_down_F;
                        TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_1차_mma_F;
                        CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_F;

                        if (검색식.Contains("_2차"))
                        {
                            차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio2_F;
                            리밸매도기준 = Properties.Settings.Default.리밸매도기준2_F;
                            취소 = Properties.Settings.Default.TB_rebalance_sellcancel2_F;
                            CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_2차_F;
                            TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_2차_down_F;
                            TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_2차_mma_F;
                            CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_F;
                        }
                    }

                    if (검색식.Contains("리밸_G"))
                    {
                        감시_주문값 = Properties.Settings.Default.TB_rebalance_감시_value_G;
                        감시_주문구분 = Properties.Settings.Default.combo_rebalance_감시_jumun_G;
                        TimeChoice = Properties.Settings.Default.CBB_rebalance_Selltime_G;
                        차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio1_G;
                        리밸매도기준 = Properties.Settings.Default.리밸매도기준1_G;
                        취소 = Properties.Settings.Default.TB_rebalance_sellcancel1_G;
                        CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_1차_G;
                        TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_1차_down_G;
                        TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_1차_mma_G;
                        CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_G;

                        if (검색식.Contains("_2차"))
                        {
                            차수주문값 = Properties.Settings.Default.TB_rebalance_sellratio2_G;
                            리밸매도기준 = Properties.Settings.Default.리밸매도기준2_G;
                            취소 = Properties.Settings.Default.TB_rebalance_sellcancel2_G;
                            CB_rebalance_TS = Properties.Settings.Default.CB_rebalance_TS_2차_G;
                            TB_rebalance_TS_down = Properties.Settings.Default.TB_rebalance_TS_2차_down_G;
                            TB_rebalance_TS_이평 = Properties.Settings.Default.TB_rebalance_TS_2차_mma_G;
                            CBB_rebalance_TS_이평 = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_G;
                        }
                    }

                    int 최종번호 = 0;
                    if (수익구분 == 7)
                    {
                        if (검색식.Contains("리밸_A")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_A") && o.종목코드.Equals(종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); 최종번호 = 정렬_List[0].번호; }
                        if (검색식.Contains("리밸_B")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_B") && o.종목코드.Equals(종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); 최종번호 = 정렬_List[0].번호; }
                        if (검색식.Contains("리밸_C")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_C") && o.종목코드.Equals(종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); 최종번호 = 정렬_List[0].번호; }
                        if (검색식.Contains("리밸_D")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_D") && o.종목코드.Equals(종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); 최종번호 = 정렬_List[0].번호; }
                        if (검색식.Contains("리밸_E")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_E") && o.종목코드.Equals(종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); 최종번호 = 정렬_List[0].번호; }
                        if (검색식.Contains("리밸_F")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_F") && o.종목코드.Equals(종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); 최종번호 = 정렬_List[0].번호; }
                        if (검색식.Contains("리밸_G")) { List<최종매입가> list = Form1.최종매입가_List.FindAll(o => o.위치.Equals("리밸_G") && o.종목코드.Equals(종목코드)); List<최종매입가> 정렬_List = list.OrderByDescending(o => o.번호).ToList(); 최종번호 = 정렬_List[0].번호; }
                        최종번호 = 최종번호++;
                    }

                    //(X)  0
                    //매도수익률       1
                    //평가수익률       2
                    //기준수익률       3
                    //평가손익금       4
                    //예상손익금       5
                    //이익수익률       6 이익수익률 
                    //손절매도수익률   7
                    //손절평가수익률   8
                    //손절기준수익률   9

                    string 주문시간 = "Real";
                    if (TimeChoice == 1) 주문시간 = "AM";
                    else if (TimeChoice == 2) 주문시간 = "PM";

                    주문체결가격 = Method.호가맞추기(주문체결가격, Form1.Market_Item_List[종목코드].Market);
                    int 감시번호 = GET_감시번호();
                    int 감시주문가격 = 0; //Method.order_price(감시_주문값, 감시_주문구분, 잔고.종목코드, 감시가격);
                    int 손절주문가격 = 0;

                    double tax_ = Form1.TAX;
                    if (잔고.시장.Equals("E")) tax_ = 0;

                    if (리밸매도기준.Equals("매도수익률")) 감시주문가격 = int.Parse(Method.감시주문가계산(종목코드, 차수주문값 + (tax_ * 100) + (Form1.수수료 * 100 * 2), 주문체결가격, 잔고.현재가).Split('&')[0]);    //매도수익률
                    if (리밸매도기준.Equals("이익수익률")) 감시주문가격 = int.Parse(Method.감시주문가계산(종목코드, 차수주문값 + (tax_ * 100) + (Form1.수수료 * 100 * 2), 주문체결가격, 잔고.현재가).Split('&')[0]);    //이익수익률

                    if (리밸매도기준.Equals("손절매도수익률")) 손절주문가격 = int.Parse(Method.감시주문가계산(종목코드, 차수주문값, 주문체결가격, 잔고.현재가).Split('&')[0]);    //손절매도수익률
                    if (리밸매도기준.Equals("평가수익률")) if (0 < 잔고.수익률) 차수주문값 = 차수주문값 + 잔고.수익률;    //잔고수익률
                    if (리밸매도기준.Equals("기준수익률")) if (0 < 잔고.기준수익률) 차수주문값 = 차수주문값 + 잔고.기준수익률;    //기준수익률

                    if (리밸매도기준.Equals("평가손익금"))
                    {
                        if (0 < 잔고.평가손익)
                        {
                            if (단위_기준) // 매수기준금
                            {
                                long 매수기준금 = Properties.Settings.Default.MT_buying_standard;
                                차수주문값 = 차수주문값 + (Math.Round((double)잔고.평가손익 / 매수기준금 * 100, 2));
                            }
                            else // 만원
                            {
                                차수주문값 = 차수주문값 + (Math.Round((double)잔고.평가손익 / 10000, 2));
                            }
                        }
                    }

                    if (리밸매도기준.Equals("예상손익금"))
                    {
                        if (0 < 잔고.예상손익)
                        {
                            if (단위_기준) // 매수기준금
                            {
                                long 매수기준금 = Properties.Settings.Default.MT_buying_standard;
                                차수주문값 = 차수주문값 + (Math.Round((double)잔고.예상손익 / 매수기준금 * 100, 2));
                            }
                            else // 만원
                            {
                                차수주문값 = 차수주문값 + (Math.Round((double)잔고.예상손익 / 10000, 2));
                            }
                        }
                    }

                    감시주문 감시추가 = new 감시주문(종목코드, 주문수량, 주문체결가격, 감시주문가격, 손절주문가격, 감시_주문값, 감시_주문구분, "+++", 검색식 + " [감시]", 잔고.종목명, Form1.today, "", 감시번호, 연동감시번호,
                                                   주문시간, 단위_기준, 차수주문값, 차수주문구분, 취소, 수익구분, 최종번호, 리밸매도기준, CB_rebalance_TS, 0, TB_rebalance_TS_down, TB_rebalance_TS_이평, CBB_rebalance_TS_이평);
                    Form1.감시주문_List.Add(감시추가);

                    DataManagement.리밸_감시_List_기록();
                }
                else
                {
                    if (Form1.form1.금액알림)
                    {
                        Form1.form1.금액알림 = false;
                        Form1.AutoClosingAlram("[리밸런싱 주문불가] 종목명:" + 잔고.종목명 + "주문가능 수량이 없어 ' 매도 '주문 할수 없습니다.", "주문불가", 10, "에러");
                    }
                }
            }
        }

        public static void 리밸감시_감시중(Stockbalance 잔고)
        {
            if (잔고.매매가능 && !잔고.매도정지)
            {
                List<감시주문> 종목감시_LIST = Form1.감시주문_List.FindAll(o => o.종목코드.Equals(잔고.종목코드));
                if (종목감시_LIST.Count > 0)
                {
                    감시주문("Real");

                    if (Form1.form1.오전감시 && Form1.form1.오전감시시간 < Form1.timenow)
                    {
                        Form1.form1.오전감시 = false;
                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_리밸매도시간오전.BackColor = Color.Tan;

                        감시주문("AM");
                    }

                    if (Form1.form1.오후감시 && Form1.form1.오후감시시간 < Form1.timenow)
                    {
                        Form1.form1.오후감시 = false;
                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_리밸매도시간오후.BackColor = Color.Tan;

                        감시주문("PM");
                    }

                    void 감시주문(string 주문시간)
                    {
                        List<감시주문> 감시_LIST = 종목감시_LIST.FindAll(o => o.주문시간.Equals(주문시간));
                        if (감시_LIST.Count > 0)
                        {
                            for (int i = 0; i < 감시_LIST.Count; i++)
                            {
                                감시주문 Item = 감시_LIST[i];

                                bool 중복검사 = true;
                                if (!Item.원주문번호.Equals("+++"))
                                {
                                    JumunItem item = Form1.JumunItem_List.Find(o => o.주문번호.Equals(Item.원주문번호));
                                    if (item != null) 중복검사 = false;
                                }

                                if (중복검사 && Item.TS_high == 0)
                                {
                                    if (Item.감시주문가격 > 0) // 1(매도수익률) && 6(이익수익률) # 수익중일때
                                    {
                                        if (Item.주문수량 > 0)
                                        {
                                            if (Item.감시_주문구분 == 0) //시장가
                                            {
                                                if (Item.리밸매도기준.Equals("이익수익률"))
                                                {
                                                    if (잔고.수익률 > 0.3) if (잔고.현재가 >= Item.감시주문가격) 주문전달(잔고, Item, "");
                                                }
                                                else
                                                {
                                                    if (잔고.현재가 >= Item.감시주문가격) 주문전달(잔고, Item, "");
                                                }
                                            }
                                            else
                                            {
                                                if (Item.감시주문가격 < Method.상한가_하한가_구하기("상", 잔고.종목코드))
                                                {
                                                    if (Item.TS)
                                                    {
                                                        if (잔고.현재가 > Item.감시주문가격) 주문전달(잔고, Item, "high");
                                                    }
                                                    else
                                                    {
                                                        int Tik_cap = Method.Find_Tik_Cap(잔고.현재가, Item.감시주문가격, 잔고.시장);
                                                        if (Tik_cap <= 5)
                                                        {
                                                            if (Item.리밸매도기준.Equals("이익수익률"))
                                                            {
                                                                if (잔고.수익률 > 0.3) 주문전달(잔고, Item, "");
                                                            }
                                                            else
                                                            {
                                                                주문전달(잔고, Item, "");
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (잔고.현재가 >= Item.감시주문가격)
                                            {
                                                DataManagement.감시주문삭제(Item, "주문체결");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Item.주문수량 > 0)
                                        {
                                            if (Item.감시_주문구분 == 0) //시장가
                                            {
                                                if (리밸감시_수익계산(잔고, Item)) 주문전달(잔고, Item, "");
                                            }
                                            else
                                            {
                                                if (Item.TS && !Item.리밸매도기준.Contains("손절"))
                                                {
                                                    if (리밸감시_수익계산(잔고, Item)) 주문전달(잔고, Item, "high");
                                                }
                                                else
                                                {
                                                    if (!감시주문_주문가계산(잔고, Item).Equals("주문불가"))
                                                    {
                                                        if (미리주문_수익계산(잔고, Item)) 주문전달(잔고, Item, "");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (리밸감시_수익계산(잔고, Item))
                                            {
                                                DataManagement.감시주문삭제(Item, "주문체결");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                리밸감시_TS_감시(잔고);
            }
        }

        public static void 리밸감시_TS_감시(Stockbalance 잔고)
        {
            List<감시주문> 종목감시_LIST = Form1.감시주문_List.FindAll(o => o.종목코드.Equals(잔고.종목코드));
            if (종목감시_LIST.Count > 0)
            {
                for (int i = 0; i < 종목감시_LIST.Count; i++)
                {
                    감시주문 Item = 종목감시_LIST[i];

                    if (!Item.TS)
                    {
                        Item.TS_high = 0;
                    }

                    if (Item.TS && Item.TS_high != 0)
                    {
                        if (Item.TS_high < 잔고.현재가) Item.TS_high = 잔고.현재가;

                        double 주문비 = ((double)잔고.현재가 - (double)Item.TS_high) / (double)Item.TS_high * (double)100;
                        if (Item.TS_down >= 주문비)
                        {
                            bool 중복검사 = true;
                            if (!Item.원주문번호.Equals("+++"))
                            {
                                JumunItem item = Form1.JumunItem_List.Find(o => o.주문번호.Equals(Item.원주문번호));
                                if (item != null) 중복검사 = false;
                            }

                            if (중복검사)
                            {
                                ma mma = Form1.Min_ma_list[잔고.종목코드];
                                double min_mma = mma.Rebal_TS_ma_1차_A;

                                if (Item.검색식.Contains("리밸_B")) min_mma = mma.Rebal_TS_ma_1차_B;
                                if (Item.검색식.Contains("리밸_C")) min_mma = mma.Rebal_TS_ma_1차_C;
                                if (Item.검색식.Contains("리밸_D")) min_mma = mma.Rebal_TS_ma_1차_D;
                                if (Item.검색식.Contains("리밸_E")) min_mma = mma.Rebal_TS_ma_1차_E;
                                if (Item.검색식.Contains("리밸_F")) min_mma = mma.Rebal_TS_ma_1차_F;
                                if (Item.검색식.Contains("리밸_G")) min_mma = mma.Rebal_TS_ma_1차_G;
                                if (Item.검색식.Contains("리밸_A_2차")) min_mma = mma.Rebal_TS_ma_2차_A;
                                if (Item.검색식.Contains("리밸_B_2차")) min_mma = mma.Rebal_TS_ma_2차_B;
                                if (Item.검색식.Contains("리밸_C_2차")) min_mma = mma.Rebal_TS_ma_2차_C;
                                if (Item.검색식.Contains("리밸_D_2차")) min_mma = mma.Rebal_TS_ma_2차_D;
                                if (Item.검색식.Contains("리밸_E_2차")) min_mma = mma.Rebal_TS_ma_2차_E;
                                if (Item.검색식.Contains("리밸_F_2차")) min_mma = mma.Rebal_TS_ma_2차_F;
                                if (Item.검색식.Contains("리밸_G_2차")) min_mma = mma.Rebal_TS_ma_2차_G;

                                if (MA.Get_TS_이평(잔고, Item.CBB_TS_이평, min_mma))
                                {
                                    int Tik_cap = Method.Find_Tik_Cap(잔고.현재가, Item.감시주문가격, 잔고.시장);
                                    if (Tik_cap <= 15)
                                    {
                                        if (Item.리밸매도기준.Equals("이익수익률"))
                                        {
                                            if (잔고.수익률 > 0.3) if (잔고.현재가 > Item.감시주문가격) 주문전달(잔고, Item, " TS");
                                        }
                                        else
                                        {
                                            주문전달(잔고, Item, " TS");
                                        }
                                    }
                                    else
                                    {
                                        Item.TS_high = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void 주문전달(Stockbalance 잔고, 감시주문 감시, string TS)
        {
            if (감시.주문체결가격 == 0)
            {
                Form1.감시주문_List.Remove(감시);
            }
            else
            {
                if (TS.Equals("high"))
                {
                    감시.TS_high = 잔고.현재가;
                }
                else
                {
                    if (잔고.주문가능수량 >= 감시.주문수량)
                    {
                        int 감시가격 = 감시.감시주문가격;

                        if (감시.감시주문가격 == 0)
                        {
                            if (감시.리밸매도기준.Equals("평가수익률") || 감시.리밸매도기준.Equals("기준수익률")) 감시가격 = int.Parse(감시주문_주문가계산(잔고, 감시));
                            else 감시가격 = 잔고.현재가;
                        }

                        int 주문가격 = Method.order_price(감시.감시_주문값, 감시.감시_주문구분, 잔고.종목코드, 감시가격);

                        string 비고 = "";
                        string 거래구분 = "00";

                        int 취소시간 = 감시.취소시간;
                        int 기록_주문가격 = 주문가격;

                        if (감시.감시_주문구분 == 0) //  [거래구분]
                        {
                            거래구분 = "03"; // 03 : 시장가
                            주문가격 = 0;
                        }

                        if (Method.매매확인_VI_모투가능확인(Form1.Market_Item_List[잔고.종목코드], 2))
                        {
                            int ScreenNumber = GET.jumunScreen(잔고.종목코드);
                            if (ScreenNumber == 1300)
                            {
                                Method.주문초과알림(잔고.종목명);
                            }
                            else
                            {
                                int Order번호 = GET.Order번호();


                                JumunItem ItemList = new JumunItem(0, 0, ScreenNumber.ToString(), 잔고.종목코드, 잔고.종목명, 감시.원주문번호, "---", 감시.검색식 + TS, 감시.감시_주문값, 감시.감시_주문구분, 취소시간, 0, 0, 비고, 감시.검색식, 감시.주문수량, 기록_주문가격, 2, 1, 0, 취소시간, 잔고.현재가, 0,
                                                                   Form1.timenow, 감시.주문수량, true, false, 0, Method.Find_Tik_Cap(잔고.현재가, 주문가격, 잔고.시장),
                                                                   잔고.현재가, 잔고.수익률, false, 감시.감시번호, Order번호, 0, Form1.NXT_server); // 자동 매도 일때  주문추가 
                                Form1.JumunItem_List.Add(ItemList);

                                DataManagement.주문가능수업데이트(잔고, "매도", 감시.주문수량, "매도주문");
                                Form1.que_order(ScreenNumber.ToString(), 잔고.종목명, 2, 잔고.종목코드, 감시.주문수량, 주문가격, 거래구분, "+++", 감시.검색식 + TS, Order번호);
                            }
                        }
                    }
                    else
                    {
                        if (잔고.보유수량 < 감시.주문수량)
                        {
                            DataManagement.감시주문삭제(감시, "주문정리");
                        }
                    }
                }
            }
        }


        public static string 감시주문_주문가계산(Stockbalance 잔고, 감시주문 감시)
        {
            string para = "";

            if (감시.리밸매도기준.Equals("평가수익률"))
            {
                if (감시.차수주문값 <= 잔고.수익률)
                {
                    para = 잔고.현재가.ToString();
                }
                else
                {
                    계산();
                }
            }
            else if (감시.리밸매도기준.Equals("기준수익률"))
            {
                if (감시.차수주문값 <= 잔고.기준수익률)
                {
                    para = 잔고.현재가.ToString();
                }
                else
                {
                    계산();
                }
            }
            else
            {
                para = 잔고.현재가.ToString();
            }

            void 계산()
            {
                int 주문가_계산 = Method.호가맞추기(잔고.현재가, 잔고.시장);
                double 조정값 = 0.5;
                if (잔고.현재가 >= 20000) 조정값 = 1;

                for (int i = 1; i > -1; i++)
                {
                    int 주문가 = 주문가_계산 + Method.GetHoga(주문가_계산, 잔고.시장);

                    주문가_계산 = 주문가;

                    double 주문비 = ((double)주문가 - (double)잔고.현재가) / (double)잔고.현재가 * (double)100;

                    if (주문비 >= 조정값)
                    {
                        para = 주문가.ToString();
                        break;
                    }
                }

                int 상한_가 = Method.상한가_하한가_구하기("상", 잔고.종목코드);
                if (int.Parse(para) > 상한_가) para = "주문불가";
            }

            return para;
        }

        public static bool 리밸감시_수익계산(Stockbalance 잔고, 감시주문 Item)
        {
            bool on = false;

            if (Item.리밸매도기준.Equals("평가수익률"))
            {
                if (Item.차수주문값 <= 잔고.수익률)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("기준수익률"))
            {
                if (Item.차수주문값 <= 잔고.기준수익률)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("손절매도수익률"))
            {
                if (Item.손절주문가격 >= 잔고.현재가)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("손절평가수익률"))
            {
                if (Item.차수주문값 >= 잔고.수익률)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("손절기준수익률"))
            {
                if (Item.차수주문값 >= 잔고.기준수익률)
                {
                    on = true;
                }
            }
            else
            {
                long 매수기준금 = Properties.Settings.Default.MT_buying_standard;
                double 차수주문값 = Item.차수주문값 * 10000;
                if (Item.단위_기준) 차수주문값 = 매수기준금 * (double)Item.차수주문값 / 100;

                if (Item.리밸매도기준.Equals("평가손익금"))
                {
                    if (차수주문값 <= 잔고.평가손익)
                    {
                        on = true;
                    }
                }
                else if (Item.리밸매도기준.Equals("예상손익금"))
                {
                    if (차수주문값 <= 잔고.예상손익 && 잔고.평가손익 > 0 && 잔고.수익률 > 0)
                    {
                        on = true;
                    }
                }
            }
            return on;
        }

        public static bool 미리주문_수익계산(Stockbalance 잔고, 감시주문 Item)
        {
            bool on = false;
            double 조정값 = -0.5;
            if (잔고.현재가 >= 20000) 조정값 = -1;

            if (Item.리밸매도기준.Equals("평가수익률"))
            {
                if (Item.차수주문값 + 조정값 <= 잔고.수익률)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("기준수익률"))
            {
                if (Item.차수주문값 + 조정값 <= 잔고.기준수익률)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("손절매도수익률"))
            {
                if (Item.손절주문가격 >= 잔고.현재가)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("손절평가수익률"))
            {
                if (Item.차수주문값 >= 잔고.수익률)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("손절기준수익률"))
            {
                if (Item.차수주문값 >= 잔고.기준수익률)
                {
                    on = true;
                }
            }
            else
            {
                long 매수기준금 = Properties.Settings.Default.MT_buying_standard;
                double 차수주문값 = Item.차수주문값 * 10000;
                if (Item.단위_기준) 차수주문값 = 매수기준금 * (double)Item.차수주문값 / 100;

                if (Item.리밸매도기준.Equals("평가손익금"))
                {
                    if (차수주문값 <= 잔고.평가손익)
                    {
                        on = true;
                    }
                }
                else if (Item.리밸매도기준.Equals("예상손익금"))
                {
                    if (차수주문값 <= 잔고.예상손익 && 잔고.평가손익 > 0 && 잔고.수익률 > 0)
                    {
                        on = true;
                    }
                }
            }
            return on;
        }

        public static void 최종주문가_매도체결(감시주문 감시)
        {
            if (감시.수익구분 == 7)
            {
                if (감시.검색식.Contains("리밸_A")) { 최종매입가 item = Form1.최종매입가_List.Find(o => o.위치.Equals("리밸_A") && o.종목코드.Equals(감시.종목코드) && o.번호.Equals(감시.최종번호)); if (item != null) Form1.최종매입가_List.Remove(item); }
                if (감시.검색식.Contains("리밸_B")) { 최종매입가 item = Form1.최종매입가_List.Find(o => o.위치.Equals("리밸_B") && o.종목코드.Equals(감시.종목코드) && o.번호.Equals(감시.최종번호)); if (item != null) Form1.최종매입가_List.Remove(item); }
                if (감시.검색식.Contains("리밸_C")) { 최종매입가 item = Form1.최종매입가_List.Find(o => o.위치.Equals("리밸_C") && o.종목코드.Equals(감시.종목코드) && o.번호.Equals(감시.최종번호)); if (item != null) Form1.최종매입가_List.Remove(item); }
                if (감시.검색식.Contains("리밸_D")) { 최종매입가 item = Form1.최종매입가_List.Find(o => o.위치.Equals("리밸_D") && o.종목코드.Equals(감시.종목코드) && o.번호.Equals(감시.최종번호)); if (item != null) Form1.최종매입가_List.Remove(item); }
                if (감시.검색식.Contains("리밸_E")) { 최종매입가 item = Form1.최종매입가_List.Find(o => o.위치.Equals("리밸_E") && o.종목코드.Equals(감시.종목코드) && o.번호.Equals(감시.최종번호)); if (item != null) Form1.최종매입가_List.Remove(item); }
                if (감시.검색식.Contains("리밸_F")) { 최종매입가 item = Form1.최종매입가_List.Find(o => o.위치.Equals("리밸_F") && o.종목코드.Equals(감시.종목코드) && o.번호.Equals(감시.최종번호)); if (item != null) Form1.최종매입가_List.Remove(item); }
                if (감시.검색식.Contains("리밸_G")) { 최종매입가 item = Form1.최종매입가_List.Find(o => o.위치.Equals("리밸_G") && o.종목코드.Equals(감시.종목코드) && o.번호.Equals(감시.최종번호)); if (item != null) Form1.최종매입가_List.Remove(item); }

                DataManagement.최종매입가_저장();
            }
        }

        public static void 감시주문동기화()
        {
            Dictionary<string, Stockbalance> stockBalanceList = Form1.stockBalanceList;   // 잔고 - 보유잔고리스트

            if (stockBalanceList.Count > 0)
            {
                if (Form1.최종매입가_List.ToList().Count > 0)
                {
                    foreach (var item in Form1.최종매입가_List.ToList())
                    {
                        if (!Form1.stockBalanceList.ContainsKey(item.종목코드))
                        {
                            Form1.최종매입가_List.Remove(item);
                        }
                    }
                }
                else
                {
                    foreach (var item in Form1.stockBalanceList.ToList())
                    {
                        Stockbalance 잔고 = item.Value;

                        Form1.최종매입가_List.Add(new 최종매입가(잔고.종목코드, "리밸_A", 0, 잔고.시작가격));
                        Form1.최종매입가_List.Add(new 최종매입가(잔고.종목코드, "리밸_B", 0, 잔고.시작가격));
                        Form1.최종매입가_List.Add(new 최종매입가(잔고.종목코드, "리밸_C", 0, 잔고.시작가격));
                        Form1.최종매입가_List.Add(new 최종매입가(잔고.종목코드, "리밸_D", 0, 잔고.시작가격));
                        Form1.최종매입가_List.Add(new 최종매입가(잔고.종목코드, "리밸_E", 0, 잔고.시작가격));
                        Form1.최종매입가_List.Add(new 최종매입가(잔고.종목코드, "리밸_F", 0, 잔고.시작가격));
                        Form1.최종매입가_List.Add(new 최종매입가(잔고.종목코드, "리밸_G", 0, 잔고.시작가격));
                    }
                }

                DataManagement.최종매입가_저장();

                foreach (var item in Form1.감시주문_List.ToList())
                {
                    if (!stockBalanceList.ContainsKey(item.종목코드))
                        Form1.감시주문_List.Remove(item);
                }

                DataManagement.리밸_감시_List_기록();
            }

            List<주문예약> 신규_List = Form1.form1.주문예약_List.FindAll(o => o.검색식.Contains("신규'매수'"));

            foreach (var item in 신규_List.ToList())
            {
                if (!stockBalanceList.ContainsKey(item.종목코드))
                {
                    Method.실시간시세등록(item.종목코드);
                }
                else
                {
                    주문예약 예약 = item;
                    Form1.form1.주문예약_List.Remove(예약);
                }
            }

            Order_Reserve.save_주문예약();
        }

        public static void 감시주문_확인(string POS)
        {
            Form1.form1.LB_JumunList.BeginUpdate();
            Form1.form1.LB_JumunList.Items.Clear();

            List<감시주문> 최종_List = Form1.감시주문_List.FindAll(o => o.수익구분 == 7);
            Form1.form1.LB_JumunList.Items.Add(" 리밸런싱관리 감시주문 총개수: " + Form1.감시주문_List.Count + " EA  일반감시: " + (Form1.감시주문_List.Count - 최종_List.Count) + " EA   최종매입가 감시주문: " + 최종_List.Count + " EA");
            if (Form1.form1.일반주문확인) Form1.form1.LB_JumunList.Items.Add("");
            else
            {
                Form1.form1.LB_JumunList.Items.Add(" ### 최종매입가 감시주문 (참조. 식 별로 차수가 나누어지며 순번이 낮을수록 앞 차수 입니다. 차수는 무한대로 늘어 납니다.) ###");
                Form1.form1.LB_JumunList.Items.Add("");
            }

            List<감시주문> 감시주문_List = Form1.감시주문_List.OrderBy(i => i.종목명).ToList();

            for (int i = 0; i < 감시주문_List.Count; i++)
            {
                감시주문 주문 = 감시주문_List[i];

                if (POS.Equals("일반주문"))
                {
                    if (중복확인(주문.종목명))
                    {
                        List<감시주문> List = 감시주문_List.FindAll(o => o.종목명.Equals(주문.종목명) && o.수익구분 != 7);
                        if (List.Count > 0)
                        {
                            Form1.form1.LB_JumunList.Items.Add(" ---------------------------------------------------- " + 주문.종목명 + " ----------------------------------------------------- ");
                            Form1.form1.LB_JumunList.Items.Add("");
                            long 매수기준금 = Properties.Settings.Default.MT_buying_standard;

                            List<감시주문> List_주문구분1 = List.FindAll(o => o.리밸매도기준.Equals("매도수익률"));
                            List<감시주문> List_주문구분2 = List.FindAll(o => o.리밸매도기준.Equals("평가수익률"));
                            List<감시주문> List_주문구분3 = List.FindAll(o => o.리밸매도기준.Equals("기준수익률"));
                            List<감시주문> List_주문구분4 = List.FindAll(o => o.리밸매도기준.Equals("평가손익금"));
                            List<감시주문> List_주문구분5 = List.FindAll(o => o.리밸매도기준.Equals("예상손익금"));
                            List<감시주문> List_주문구분6 = List.FindAll(o => o.리밸매도기준.Equals("이익수익률"));
                            List<감시주문> List_주문구분7 = List.FindAll(o => o.리밸매도기준.Equals("손절매도수익률"));
                            List<감시주문> List_주문구분8 = List.FindAll(o => o.리밸매도기준.Equals("손절평가수익률"));
                            List<감시주문> List_주문구분9 = List.FindAll(o => o.리밸매도기준.Equals("손절기준수익률"));

                            List<감시주문> List_주문구분1정렬 = List_주문구분1.OrderBy(n => n.감시주문가격).ToList();
                            List<감시주문> List_주문구분2정렬 = List_주문구분2.OrderBy(n => n.차수주문값).ToList(); // 평가수익률
                            List<감시주문> List_주문구분3정렬 = List_주문구분3.OrderBy(n => n.차수주문값).ToList(); // 기준수익률
                            List<감시주문> List_주문구분4정렬 = List_주문구분4.OrderBy(n => n.차수주문값).ToList(); // 평가수익금
                            List<감시주문> List_주문구분5정렬 = List_주문구분5.OrderBy(n => n.차수주문값).ToList(); // 예상수익금
                            List<감시주문> List_주문구분6정렬 = List_주문구분6.OrderBy(n => n.감시주문가격).ToList(); //이익수익률
                            List<감시주문> List_주문구분7정렬 = List_주문구분7.OrderByDescending(n => n.손절주문가격).ToList();
                            List<감시주문> List_주문구분8정렬 = List_주문구분8.OrderByDescending(n => n.차수주문값).ToList(); //손절평가수익률
                            List<감시주문> List_주문구분9정렬 = List_주문구분9.OrderByDescending(n => n.차수주문값).ToList(); //손절기준수익률

                            for (int m = 0; m < List_주문구분1정렬.Count; m++)
                            {
                                감시주문 확인 = List_주문구분1정렬[m];

                                string 감시값 = "감시값: " + 확인.감시주문가격.ToString("N0");
                                기록(확인.감시일, 확인.검색식, 감시값, 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }

                            for (int m = 0; m < List_주문구분6정렬.Count; m++)
                            {
                                감시주문 확인 = List_주문구분6정렬[m];

                                string 감시값 = "이익감시값: " + 확인.감시주문가격.ToString("N0");
                                기록(확인.감시일, 확인.검색식, 감시값, 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }

                            for (int m = 0; m < List_주문구분7정렬.Count; m++)
                            {
                                감시주문 확인 = List_주문구분7정렬[m];
                                string 감시값 = "손절값: " + 확인.손절주문가격.ToString("N0");
                                기록(확인.감시일, 확인.검색식, 감시값, 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }

                            for (int m = 0; m < List_주문구분4정렬.Count; m++)
                            {
                                감시주문 확인 = List_주문구분4정렬[m];

                                double Velue = 확인.차수주문값;
                                if (확인.단위_기준) Velue = 매수기준금 * (double)확인.차수주문값 / 100 / 10000;

                                string 감시값 = Velue.ToString("N2") + " 만(평가손익)";

                                기록(확인.감시일, 확인.검색식, 감시값, 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }

                            for (int m = 0; m < List_주문구분5정렬.Count; m++)
                            {
                                감시주문 확인 = List_주문구분5정렬[m];

                                double Velue = 확인.차수주문값;
                                if (확인.단위_기준) Velue = 매수기준금 * (double)확인.차수주문값 / 100 / 10000;
                                string 감시값 = Velue.ToString("N2") + " 만(예상손익)";

                                기록(확인.감시일, 확인.검색식, 감시값, 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }

                            for (int m = 0; m < List_주문구분2정렬.Count; m++)
                            {
                                감시주문 확인 = List_주문구분2정렬[m];
                                if (확인.수익구분 != 7)
                                {
                                    string 감시값 = 확인.차수주문값.ToString() + "%(평가수익률)";
                                    기록(확인.감시일, 확인.검색식, 감시값, 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                                }
                            }

                            for (int m = 0; m < List_주문구분3정렬.Count; m++)
                            {
                                감시주문 확인 = List_주문구분3정렬[m];
                                string 감시값 = 확인.차수주문값.ToString() + "%(기준수익률)";
                                기록(확인.감시일, 확인.검색식, 감시값, 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }

                            for (int m = 0; m < List_주문구분8정렬.Count; m++)
                            {
                                감시주문 확인 = List_주문구분8정렬[m];
                                string 감시값 = 확인.차수주문값.ToString() + "%(손절평가수익률)";
                                기록(확인.감시일, 확인.검색식, 감시값, 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }

                            for (int m = 0; m < List_주문구분9정렬.Count; m++)
                            {
                                감시주문 확인 = List_주문구분9정렬[m];
                                string 감시값 = 확인.차수주문값.ToString() + "%(손절기준수익률)";
                                기록(확인.감시일, 확인.검색식, 감시값, 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }

                            Form1.form1.LB_JumunList.Items.Add("");
                        }

                        void 기록(string 일, string 식, string 값, string 주문수, string 원주문번호, string 주문_, string 연동, string 감시번호, string TS, string TS_high)
                        {
                            Form1.form1.LB_JumunList.Items.Add(" 감시일: " + 일 + " 식: " + 식 + " 주문: " + 주문_ + " " + 값 + " = " + 주문수 + " 연동: " + 연동 + " 감시번호: " + 감시번호 + " TS: " + TS + " TS_higt: " + TS_high + " 주 주문번호:" + 원주문번호);
                        }
                    }
                }
                else
                {
                    if (중복확인(주문.종목명))
                    {
                        List<감시주문> List = 감시주문_List.FindAll(o => o.종목명.Equals(주문.종목명) && o.수익구분 == 7);
                        if (List.Count > 0)
                        {
                            Form1.form1.LB_JumunList.Items.Add(" ---------------------------------------------------- " + 주문.종목명 + " ----------------------------------------------------- ");
                            Form1.form1.LB_JumunList.Items.Add("");
                            List<감시주문> List_A_1차 = List.FindAll(o => o.검색식.Contains("리밸_A_1차"));
                            List<감시주문> List_B_1차 = List.FindAll(o => o.검색식.Contains("리밸_B_1차"));
                            List<감시주문> List_C_1차 = List.FindAll(o => o.검색식.Contains("리밸_C_1차"));
                            List<감시주문> List_D_1차 = List.FindAll(o => o.검색식.Contains("리밸_D_1차"));
                            List<감시주문> List_E_1차 = List.FindAll(o => o.검색식.Contains("리밸_E_1차"));
                            List<감시주문> List_F_1차 = List.FindAll(o => o.검색식.Contains("리밸_F_1차"));
                            List<감시주문> List_G_1차 = List.FindAll(o => o.검색식.Contains("리밸_G_1차"));

                            List<감시주문> List_A_2차 = List.FindAll(o => o.검색식.Contains("리밸_A_2차"));
                            List<감시주문> List_B_2차 = List.FindAll(o => o.검색식.Contains("리밸_B_2차"));
                            List<감시주문> List_C_2차 = List.FindAll(o => o.검색식.Contains("리밸_C_2차"));
                            List<감시주문> List_D_2차 = List.FindAll(o => o.검색식.Contains("리밸_D_2차"));
                            List<감시주문> List_E_2차 = List.FindAll(o => o.검색식.Contains("리밸_E_2차"));
                            List<감시주문> List_F_2차 = List.FindAll(o => o.검색식.Contains("리밸_F_2차"));
                            List<감시주문> List_G_2차 = List.FindAll(o => o.검색식.Contains("리밸_G_2차"));

                            List<감시주문> List_A_1차_정렬 = List_A_1차.OrderBy(n => n.최종번호).ToList();
                            List<감시주문> List_B_1차_정렬 = List_B_1차.OrderBy(n => n.최종번호).ToList();
                            List<감시주문> List_C_1차_정렬 = List_C_1차.OrderBy(n => n.최종번호).ToList();
                            List<감시주문> List_D_1차_정렬 = List_D_1차.OrderBy(n => n.최종번호).ToList();
                            List<감시주문> List_E_1차_정렬 = List_E_1차.OrderBy(n => n.최종번호).ToList();
                            List<감시주문> List_F_1차_정렬 = List_F_1차.OrderBy(n => n.최종번호).ToList();
                            List<감시주문> List_G_1차_정렬 = List_G_1차.OrderBy(n => n.최종번호).ToList();

                            List<감시주문> List_A_2차_정렬 = List_A_2차.OrderBy(n => n.최종번호).ToList();
                            List<감시주문> List_B_2차_정렬 = List_B_2차.OrderBy(n => n.최종번호).ToList();
                            List<감시주문> List_C_2차_정렬 = List_C_2차.OrderBy(n => n.최종번호).ToList();
                            List<감시주문> List_D_2차_정렬 = List_D_2차.OrderBy(n => n.최종번호).ToList();
                            List<감시주문> List_E_2차_정렬 = List_E_2차.OrderBy(n => n.최종번호).ToList();
                            List<감시주문> List_F_2차_정렬 = List_F_2차.OrderBy(n => n.최종번호).ToList();
                            List<감시주문> List_G_2차_정렬 = List_G_2차.OrderBy(n => n.최종번호).ToList();

                            for (int m = 0; m < List_A_1차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_A_1차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            for (int m = 0; m < List_A_2차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_A_2차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            if (List_A_1차_정렬.Count > 0 || List_A_2차_정렬.Count > 0) Form1.form1.LB_JumunList.Items.Add("");

                            for (int m = 0; m < List_B_1차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_B_1차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            for (int m = 0; m < List_B_2차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_B_2차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            if (List_B_1차_정렬.Count > 0 || List_B_2차_정렬.Count > 0) Form1.form1.LB_JumunList.Items.Add("");

                            for (int m = 0; m < List_C_1차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_C_1차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            for (int m = 0; m < List_C_2차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_C_2차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            if (List_C_1차_정렬.Count > 0 || List_C_2차_정렬.Count > 0) Form1.form1.LB_JumunList.Items.Add("");

                            for (int m = 0; m < List_D_1차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_D_1차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            for (int m = 0; m < List_D_2차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_D_2차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            if (List_D_1차_정렬.Count > 0 || List_D_2차_정렬.Count > 0) Form1.form1.LB_JumunList.Items.Add("");

                            for (int m = 0; m < List_E_1차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_E_1차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            for (int m = 0; m < List_E_2차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_E_2차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            if (List_E_1차_정렬.Count > 0 || List_E_2차_정렬.Count > 0) Form1.form1.LB_JumunList.Items.Add("");

                            for (int m = 0; m < List_F_1차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_F_1차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            for (int m = 0; m < List_F_2차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_F_2차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            if (List_F_1차_정렬.Count > 0 || List_F_2차_정렬.Count > 0) Form1.form1.LB_JumunList.Items.Add("");

                            for (int m = 0; m < List_G_1차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_G_1차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            for (int m = 0; m < List_G_2차_정렬.Count; m++)
                            {
                                감시주문 확인 = List_G_2차_정렬[m];
                                최종기록(확인.감시일, 확인.검색식, 감시값확인(확인), 확인.최종번호.ToString(), 확인.주문수량.ToString("N0"), 확인.원주문번호, 확인.주문시간, 확인.연동감시번호.ToString(), 확인.감시번호.ToString(), 확인.TS.ToString(), 확인.TS_high.ToString());
                            }
                            if (List_G_1차_정렬.Count > 0 || List_G_2차_정렬.Count > 0) Form1.form1.LB_JumunList.Items.Add("");
                        }

                        void 최종기록(string 일, string 식, string 값, string 최종번호, string 주문수, string 원주문번호, string 주문_, string 연동, string 감시번호, string TS, string TS_high)
                        {
                            Form1.form1.LB_JumunList.Items.Add(" 감시일: " + 일 + " 식: " + 식 + " 순번:" + 최종번호 + " 주문: " + 주문_ + " " + 값 + " = " + 주문수 + " 연동: " + 연동 + " 감시번호: " + 감시번호 + " TS: " + TS + " TS_higt: " + TS_high + " 주 주문번호:" + 원주문번호);
                        }

                        string 감시값확인(감시주문 감시Item)
                        {
                            string 감시값 = "";

                            long 매수기준금 = Properties.Settings.Default.MT_buying_standard;
                            double Velue = 감시Item.차수주문값;
                            if (감시Item.단위_기준) Velue = 매수기준금 * (double)감시Item.차수주문값 / 100 / 10000;

                            if (감시Item.리밸매도기준.Equals("매도수익률")) 감시값 = "감시가격:" + 감시Item.감시주문가격;
                            if (감시Item.리밸매도기준.Equals("평가수익률")) 감시값 = 감시Item.차수주문값.ToString() + "%(평가수익률)";
                            if (감시Item.리밸매도기준.Equals("기준수익률")) 감시값 = 감시Item.차수주문값.ToString() + "%(기준수익률)";
                            if (감시Item.리밸매도기준.Equals("평가손익금")) 감시값 = Velue.ToString("N2") + " 만(평가손익)";
                            if (감시Item.리밸매도기준.Equals("예상손익금")) 감시값 = Velue.ToString("N2") + " 만(예상손익)";
                            if (감시Item.리밸매도기준.Equals("이익수익률")) 감시값 = "이익감시가격:" + 감시Item.감시주문가격;
                            if (감시Item.리밸매도기준.Equals("손절매도수익률")) 감시값 = "손절주문가격:" + 감시Item.손절주문가격;
                            if (감시Item.리밸매도기준.Equals("손절평가수익률")) 감시값 = 감시Item.차수주문값.ToString() + "%(손절평가수익률)";
                            if (감시Item.리밸매도기준.Equals("손절기준수익률")) 감시값 = 감시Item.차수주문값.ToString() + "%(손절기준수익률)";

                            return 감시값;
                        }
                    }
                }
            }

            Form1.form1.LB_JumunList.EndUpdate();

            bool 중복확인(string 종목)
            {
                bool 확인 = true;
                int 개수 = 0;

                for (int n = 0; n < Form1.form1.LB_JumunList.Items.Count; n++)
                {
                    if (Form1.form1.LB_JumunList.Items[n].ToString().Contains(종목))
                    {
                        개수++;
                    }
                }

                if (개수 > 0)
                {
                    확인 = false;
                }
                return 확인;
            }
        }

        private static int GET_감시번호()
        {
            int 감시번호 = 1;

            for (int i = 1; i > 0; i++)
            {
                감시주문 result = Form1.감시주문_List.Find(o => o.감시번호 == i);
                if (result == null)
                {
                    감시번호 = i;
                    break;
                }
            }

            return 감시번호;
        }

        private static int GET_연동감시번호()
        {
            int 감시번호 = 0;

            for (int i = 1; i > 0; i++)
            {
                감시주문 result = Form1.감시주문_List.Find(o => o.연동감시번호 == i);
                if (result == null)
                {
                    감시번호 = i;
                    break;
                }
            }

            return 감시번호;
        }

        ///////////////            계좌 리밸런싱 관리              ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////
        ///////////////                잔고청산                   ////////////////

        public static void Liquidation_condition(string ST_ID, string itemcode, string condition)
        {
            bool ID_A = false;
            bool ID_B = false;
            bool ID_C = false;

            if (ST_ID.Equals("I"))
            {
                if (Properties.Settings.Default.CBB_Liquidation_use_condition_A == 1) ID_A = true;
                if (Properties.Settings.Default.CBB_Liquidation_use_condition_B == 1) ID_B = true;
                if (Properties.Settings.Default.CBB_Liquidation_use_condition_C == 1) ID_C = true;
            }
            else if (ST_ID.Equals("D"))
            {
                if (Properties.Settings.Default.CBB_Liquidation_use_condition_A == 2) ID_A = true;
                if (Properties.Settings.Default.CBB_Liquidation_use_condition_B == 2) ID_B = true;
                if (Properties.Settings.Default.CBB_Liquidation_use_condition_C == 2) ID_C = true;
            }

            if (condition.Equals(Properties.Settings.Default.CBB_Liquidation_condition_A) && Properties.Settings.Default.CBB_Liquidation_use_condition_A > 0)
            {
                Liquidation_condition_A 종목 = Form1.Liquidation_condition_List_A.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_A)
                    {
                        Liquidation_condition_A 추가 = new Liquidation_condition_A(itemcode, 0);
                        Form1.Liquidation_condition_List_A.Add(추가);
                    }
                }
                else
                {
                    if (!ID_A)
                    {
                        Liquidation_condition_A 삭제 = Form1.Liquidation_condition_List_A.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Liquidation_condition_List_A.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.CBB_Liquidation_condition_B) && Properties.Settings.Default.CBB_Liquidation_use_condition_B > 0)
            {
                Liquidation_condition_B 종목 = Form1.Liquidation_condition_List_B.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_B)
                    {
                        Liquidation_condition_B 추가 = new Liquidation_condition_B(itemcode, 0);
                        Form1.Liquidation_condition_List_B.Add(추가);
                    }
                }
                else
                {
                    if (!ID_B)
                    {
                        Liquidation_condition_B 삭제 = Form1.Liquidation_condition_List_B.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Liquidation_condition_List_B.Remove(삭제);
                        }
                    }
                }
            }
            if (condition.Equals(Properties.Settings.Default.CBB_Liquidation_condition_C) && Properties.Settings.Default.CBB_Liquidation_use_condition_C > 0)
            {
                Liquidation_condition_C 종목 = Form1.Liquidation_condition_List_C.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_C)
                    {
                        Liquidation_condition_C 추가 = new Liquidation_condition_C(itemcode, 0);
                        Form1.Liquidation_condition_List_C.Add(추가);
                    }
                }
                else
                {
                    if (!ID_C)
                    {
                        Liquidation_condition_C 삭제 = Form1.Liquidation_condition_List_C.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Liquidation_condition_List_C.Remove(삭제);
                        }
                    }
                }
            }
        }

        public static void Liquidation_USE(Stockbalance 잔고)
        {
            if (잔고.잔고청산 && Form1.재시작)
            {
                long 매수기준금 = Properties.Settings.Default.MT_buying_standard;
                double 매입금1 = Properties.Settings.Default.TB_잔고청산_매입금1_A / 100 * 매수기준금;
                double 매입금2 = Properties.Settings.Default.TB_잔고청산_매입금2_A / 100 * 매수기준금;

                int start = Properties.Settings.Default.MTB_Liquidation_Starttime_A;
                int end = Properties.Settings.Default.MTB_Liquidation_Stoptime_A;
                int Repeat_use = Properties.Settings.Default.CBB_Liquidation_use_condition_A;
                double 주문값 = Properties.Settings.Default.TB_Liquidation_value_A;
                int 주문구분 = Properties.Settings.Default.CBB_Liquidation_jumun_A;
                double suik_low = Properties.Settings.Default.TB_Liquidation_suik_1_A;
                double suik_height = Properties.Settings.Default.TB_Liquidation_suik_2_A;
                bool suik_choice = Properties.Settings.Default.CB_Liquidation_choice_A;
                int suik_gubun = Properties.Settings.Default.CBB_Liquidation_suik_gubun_A;
                double 비중 = Properties.Settings.Default.TB_Liquidation_sell_ratio_A;
                int 비중단위 = Properties.Settings.Default.CBB_Liquidation_sell_gubun_A;
                double maemae_low = Properties.Settings.Default.TB_Liquidation_maemae_1_A;
                double maemae_height = Properties.Settings.Default.TB_Liquidation_maemae_2_A;
                string group = GET.익절그룹("잔고청산_A");
                int 취소N주문 = Properties.Settings.Default.CBB_Liquidation_Cancel_A;
                int 취소시간 = Properties.Settings.Default.MTB_Liquidation_Cancel_time_A;
                int 반복시간 = Properties.Settings.Default.MTB_Liquidation_repeat_A;
                bool 단위_기준금 = Properties.Settings.Default.CB_Liquidation_기준금;
                bool 추매금지 = Properties.Settings.Default.CB_추매금지_Liquidation_A;
                bool 수익보전 = Properties.Settings.Default.CB_수익보전_Liquidation_A;
                bool 매도정지 = Properties.Settings.Default.CB_Liquidation_SellStop_A;
                bool 강제매도 = Properties.Settings.Default.CB_Liquidation_강제매도_A;
                bool TS = Properties.Settings.Default.CB_Liquidation_TS_A;
                int TS_high = 잔고.잔고청산_TS_high_A;
                double TS_down = Properties.Settings.Default.TB_Liquidation_TS_down_A;

                ma mma = Form1.Min_ma_list[잔고.종목코드];
                ma dma = Form1.Day_ma_list[잔고.종목코드];

                double min_mma = mma.Liquidation_ma_A;
                int CBB_이평 = Properties.Settings.Default.CBB_Liquidation_mma_A;

                double TS_min_ma = mma.Liquidation_TS_ma_A;
                double TS_day_ma = dma.Liquidation_TS_ma_A;
                int CBB_TS_mma = Properties.Settings.Default.CBB_Liquidation_TS_mma_A;
                int CBB_TS_dma = Properties.Settings.Default.CBB_Liquidation_TS_dma_A;

                string location = "잔고청산_A";
                string 검색식 = "";
                if (Properties.Settings.Default.CBB_Liquidation_use_condition_A > 0)
                    검색식 = Properties.Settings.Default.CBB_Liquidation_condition_A;

                if (Properties.Settings.Default.CB_Liquidation_A && 잔고.가동_청산A && !잔고.잔고청산_A)
                {
                    if (!잔고.매도정지 || 강제매도)
                    {
                        if (Method.RunTime(start, end))
                        {
                            if (TS)
                            {
                                if (TS_high == 0)
                                {
                                    Run();
                                }
                                else
                                {
                                    if (잔고.잔고청산_TS_high_A < 잔고.현재가)
                                    {
                                        잔고.잔고청산_TS_high_A = 잔고.현재가;
                                    }
                                    else
                                    {
                                        if (수익보전)
                                        {
                                            if (잔고.수익률 > 0 && 잔고.예상손익 > 0) TS_check();
                                        }
                                        else
                                        {
                                            TS_check();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Run();
                            }

                            void Run()
                            {
                                if (!잔고.잔고청산_매입금_A && 매입금1 <= 잔고.매입금액 && 잔고.매입금액 <= 매입금2) if (검사()) 잔고.잔고청산_매입금_A = true;

                                if (잔고.잔고청산_매입금_A && MA.Get_TS_이평(잔고, CBB_이평, min_mma))
                                {
                                    if (Repeat_use > 0) // 검색식 사용 
                                    {
                                        Liquidation_condition_A Item = Form1.Liquidation_condition_List_A.Find(o => o.code.Equals(잔고.종목코드));
                                        if (Item != null)
                                        {
                                            if (Properties.Settings.Default.MTB_Liquidation_delay_A <= Item.timer)
                                            {
                                                if (검사()) 매매진행();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (검사()) 매매진행();
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_Liquidation_B && 잔고.가동_청산B && !잔고.잔고청산_B)
                {
                    start = Properties.Settings.Default.MTB_Liquidation_Starttime_B;
                    end = Properties.Settings.Default.MTB_Liquidation_Stoptime_B;

                    if (Method.RunTime(start, end))
                    {
                        강제매도 = Properties.Settings.Default.CB_Liquidation_강제매도_B;
                        if (!잔고.매도정지 || 강제매도)
                        {
                            Repeat_use = Properties.Settings.Default.CBB_Liquidation_use_condition_B;
                            주문값 = Properties.Settings.Default.TB_Liquidation_value_B;
                            주문구분 = Properties.Settings.Default.CBB_Liquidation_jumun_B;
                            suik_low = Properties.Settings.Default.TB_Liquidation_suik_1_B;
                            suik_height = Properties.Settings.Default.TB_Liquidation_suik_2_B;
                            suik_choice = Properties.Settings.Default.CB_Liquidation_choice_B;
                            suik_gubun = Properties.Settings.Default.CBB_Liquidation_suik_gubun_B;
                            비중 = Properties.Settings.Default.TB_Liquidation_sell_ratio_B;
                            비중단위 = Properties.Settings.Default.CBB_Liquidation_sell_gubun_B;
                            maemae_low = Properties.Settings.Default.TB_Liquidation_maemae_1_B;
                            maemae_height = Properties.Settings.Default.TB_Liquidation_maemae_2_B;
                            group = GET.익절그룹("잔고청산_B");
                            취소N주문 = Properties.Settings.Default.CBB_Liquidation_Cancel_B;
                            취소시간 = Properties.Settings.Default.MTB_Liquidation_Cancel_time_B;
                            반복시간 = Properties.Settings.Default.MTB_Liquidation_repeat_B;
                            추매금지 = Properties.Settings.Default.CB_추매금지_Liquidation_B;
                            수익보전 = Properties.Settings.Default.CB_수익보전_Liquidation_B;
                            매도정지 = Properties.Settings.Default.CB_Liquidation_SellStop_B;
                            TS = Properties.Settings.Default.CB_Liquidation_TS_B;
                            TS_down = Properties.Settings.Default.TB_Liquidation_TS_down_B;

                            min_mma = mma.Liquidation_ma_A;
                            CBB_이평 = Properties.Settings.Default.CBB_Liquidation_mma_B;

                            TS_min_ma = mma.Liquidation_TS_ma_B;
                            TS_day_ma = dma.Liquidation_TS_ma_B;
                            CBB_TS_mma = Properties.Settings.Default.CBB_Liquidation_TS_mma_B;
                            CBB_TS_dma = Properties.Settings.Default.CBB_Liquidation_TS_dma_B;

                            TS_high = 잔고.잔고청산_TS_high_B;
                            location = "잔고청산_B";
                            if (Properties.Settings.Default.CBB_Liquidation_use_condition_B > 0)
                                검색식 = Properties.Settings.Default.CBB_Liquidation_condition_B;

                            if (TS)
                            {
                                if (TS_high == 0)
                                {
                                    Run();
                                }
                                else
                                {
                                    if (잔고.잔고청산_TS_high_B < 잔고.현재가)
                                    {
                                        잔고.잔고청산_TS_high_B = 잔고.현재가;
                                    }
                                    else
                                    {
                                        if (수익보전)
                                        {
                                            if (잔고.수익률 > 0 && 잔고.예상손익 > 0) TS_check();
                                        }
                                        else
                                        {
                                            TS_check();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Run();
                            }

                            void Run()
                            {
                                매입금1 = Properties.Settings.Default.TB_잔고청산_매입금1_B / 100 * 매수기준금;
                                매입금2 = Properties.Settings.Default.TB_잔고청산_매입금2_B / 100 * 매수기준금;

                                if (!잔고.잔고청산_매입금_B && 매입금1 <= 잔고.매입금액 && 잔고.매입금액 <= 매입금2) if (검사()) 잔고.잔고청산_매입금_B = true;

                                if (잔고.잔고청산_매입금_B && MA.Get_TS_이평(잔고, CBB_이평, min_mma))
                                {
                                    if (Repeat_use > 0) // 검색식 사용 
                                    {
                                        Liquidation_condition_B Item = Form1.Liquidation_condition_List_B.Find(o => o.code.Equals(잔고.종목코드));
                                        if (Item != null)
                                        {
                                            if (Properties.Settings.Default.MTB_Liquidation_delay_B <= Item.timer)
                                            {
                                                if (검사()) 매매진행();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (검사()) 매매진행();
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_Liquidation_C && 잔고.가동_청산C && !잔고.잔고청산_C)
                {
                    start = Properties.Settings.Default.MTB_Liquidation_Starttime_C;
                    end = Properties.Settings.Default.MTB_Liquidation_Stoptime_C;

                    if (Method.RunTime(start, end))
                    {
                        강제매도 = Properties.Settings.Default.CB_Liquidation_강제매도_C;
                        if (!잔고.매도정지 || 강제매도)
                        {
                            Repeat_use = Properties.Settings.Default.CBB_Liquidation_use_condition_C;
                            주문값 = Properties.Settings.Default.TB_Liquidation_value_C;
                            주문구분 = Properties.Settings.Default.CBB_Liquidation_jumun_C;
                            suik_low = Properties.Settings.Default.TB_Liquidation_suik_1_C;
                            suik_height = Properties.Settings.Default.TB_Liquidation_suik_2_C;
                            suik_choice = Properties.Settings.Default.CB_Liquidation_choice_C;
                            suik_gubun = Properties.Settings.Default.CBB_Liquidation_suik_gubun_C;
                            비중 = Properties.Settings.Default.TB_Liquidation_sell_ratio_C;
                            비중단위 = Properties.Settings.Default.CBB_Liquidation_sell_gubun_C;
                            maemae_low = Properties.Settings.Default.TB_Liquidation_maemae_1_C;
                            maemae_height = Properties.Settings.Default.TB_Liquidation_maemae_2_C;
                            group = GET.익절그룹("잔고청산_C");
                            취소N주문 = Properties.Settings.Default.CBB_Liquidation_Cancel_C;
                            취소시간 = Properties.Settings.Default.MTB_Liquidation_Cancel_time_C;
                            반복시간 = Properties.Settings.Default.MTB_Liquidation_repeat_C;
                            추매금지 = Properties.Settings.Default.CB_추매금지_Liquidation_C;
                            수익보전 = Properties.Settings.Default.CB_수익보전_Liquidation_C;
                            매도정지 = Properties.Settings.Default.CB_Liquidation_SellStop_C;
                            TS = Properties.Settings.Default.CB_Liquidation_TS_C;
                            TS_down = Properties.Settings.Default.TB_Liquidation_TS_down_C;

                            min_mma = mma.Liquidation_ma_C;
                            CBB_이평 = Properties.Settings.Default.CBB_Liquidation_mma_C;

                            TS_min_ma = mma.Liquidation_TS_ma_C;
                            TS_day_ma = dma.Liquidation_TS_ma_C;
                            CBB_TS_mma = Properties.Settings.Default.CBB_Liquidation_TS_mma_C;
                            CBB_TS_dma = Properties.Settings.Default.CBB_Liquidation_TS_dma_C;

                            TS_high = 잔고.잔고청산_TS_high_C;

                            location = "잔고청산_C";

                            if (Properties.Settings.Default.CBB_Liquidation_use_condition_C > 0)
                                검색식 = Properties.Settings.Default.CBB_Liquidation_condition_C;

                            if (TS)
                            {
                                if (TS_high == 0)
                                {
                                    Run();
                                }
                                else
                                {
                                    if (잔고.잔고청산_TS_high_C < 잔고.현재가)
                                    {
                                        잔고.잔고청산_TS_high_C = 잔고.현재가;
                                    }
                                    else
                                    {
                                        if (수익보전)
                                        {
                                            if (잔고.수익률 > 0 && 잔고.예상손익 > 0) TS_check();
                                        }
                                        else
                                        {
                                            TS_check();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Run();
                            }

                            void Run()
                            {
                                매입금1 = Properties.Settings.Default.TB_잔고청산_매입금1_C / 100 * 매수기준금;
                                매입금2 = Properties.Settings.Default.TB_잔고청산_매입금2_C / 100 * 매수기준금;

                                if (!잔고.잔고청산_매입금_C && 매입금1 <= 잔고.매입금액 && 잔고.매입금액 <= 매입금2) if (검사()) 잔고.잔고청산_매입금_C = true;

                                if (잔고.잔고청산_매입금_C && MA.Get_TS_이평(잔고, CBB_이평, min_mma))
                                {
                                    if (Repeat_use > 0) // 검색식 사용 
                                    {
                                        Liquidation_condition_C Item = Form1.Liquidation_condition_List_C.Find(o => o.code.Equals(잔고.종목코드));
                                        if (Item != null)
                                        {
                                            if (Properties.Settings.Default.MTB_Liquidation_delay_C <= Item.timer)
                                            {
                                                if (검사()) 매매진행();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (검사()) 매매진행();
                                    }
                                }
                            }
                        }
                    }
                }

                bool 검사()
                {
                    bool result = false;

                    if (group.Contains(GET.그룹변환(잔고.매매그룹)) && Tab_InterestGroup.관심그룹확인(location, 잔고.종목코드))
                    {
                        if (!suik_choice) // →
                        {
                            if (Method.수익범위(false, 단위_기준금, 잔고, suik_low, suik_height, suik_gubun, location))
                            {
                                result = true;
                            }
                        }
                        else // ⇒
                        {
                            switch (location)
                            {
                                case "잔고청산_A":
                                    잔고.청산A = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.청산A, "A", false);
                                    if (잔고.청산A.Equals("X"))
                                    {
                                        result = true;
                                    }
                                    break;

                                case "잔고청산_B":
                                    잔고.청산B = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.청산B, "B", false);
                                    if (잔고.청산B.Equals("X"))
                                    {
                                        result = true;
                                    }
                                    break;
                                case "잔고청산_C":
                                    잔고.청산C = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.청산C, "C", false);
                                    if (잔고.청산C.Equals("X"))
                                    {
                                        result = true;
                                    }
                                    break;
                            }
                        }
                    }

                    return result;
                }

                void 매매진행()
                {
                    if (강제매도) 잔고.매도기준update = true;
                    if (Method.청산주문_매매범위(잔고, maemae_low, maemae_height))
                    {
                        if (수익보전)
                        {
                            if (잔고.수익률 > 0 && 잔고.예상손익 > 0) 매매진행_();
                        }
                        else
                        {
                            매매진행_();
                        }

                        void 매매진행_()
                        {
                            if (!잔고.추매정지 && 추매금지) 잔고.추매정지 = true;

                            if (잔고.추매정지)
                            {
                                if (!Form1.form1.추매거부_List.Contains(잔고.종목명))
                                {
                                    Form1.동작_Log("[잔고 추매금지] " + location + " 동작하여 " + 잔고.종목명 + " 추가매수 가 정지 됩니다. ");
                                    Form1.Error_Log(" ");
                                    Form1.Error_Log("[잔고 추매금지] " + location + " 동작하여 " + 잔고.종목명 + " 추가매수 정지 됩니다. ");
                                    Form1.Error_Log(" ");

                                    Form1.form1.추매거부_List.Add(잔고.종목명);
                                }
                            }

                            if (TS)
                            {
                                switch (location)
                                {
                                    case "잔고청산_A":
                                        잔고.잔고청산_TS_high_A = 잔고.현재가;
                                        break;
                                    case "잔고청산_B":
                                        잔고.잔고청산_TS_high_B = 잔고.현재가;
                                        break;
                                    case "잔고청산_C":
                                        잔고.잔고청산_TS_high_C = 잔고.현재가;
                                        break;
                                }
                            }
                            else
                            {
                                List<JumunItem> item = Form1.JumunItem_List.ToList().FindAll(o => o.종목코드.Equals(잔고.종목코드) && !o.검색식.Contains("잔고청산"));
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

                                            JumunItem.비고 = "잔고청산_미체결일괄 '취소'";
                                        }
                                    }
                                }
                                else
                                {
                                    잔고.매도기준update = false;
                                    잔고.매도기준 = 잔고.보유수량;

                                    Order_Run("");

                                    잔고.매매가능 = true;
                                    if (location.Equals("잔고청산_A")) 잔고.잔고청산_A = true;
                                    if (location.Equals("잔고청산_B")) 잔고.잔고청산_B = true;
                                    if (location.Equals("잔고청산_C")) 잔고.잔고청산_C = true;

                                    if (매도정지)
                                    {
                                        잔고.매도정지 = true;

                                        if (!Form1.form1.매도거부_List.Contains(잔고.종목명))
                                        {
                                            Form1.동작_Log("[잔고 매도정지] " + location + " 동작하여 " + 잔고.종목명 + " 매도가 정지 됩니다. ");
                                            Form1.Error_Log(" ");
                                            Form1.Error_Log("[잔고 매도정지] " + location + " 동작하여 " + 잔고.종목명 + " 매도가 정지 됩니다. ");
                                            Form1.Error_Log(" ");

                                            Form1.form1.매도거부_List.Add(잔고.종목명);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                void TS_check()
                {
                    double 주문비 = ((double)잔고.현재가 - (double)TS_high) / (double)TS_high * (double)100;
                    if (TS_down >= 주문비)
                    {
                        if (MA.Get_TS_이평(잔고, CBB_TS_mma, TS_min_ma) && MA.Get_TS_이평(잔고, CBB_TS_dma, TS_day_ma))
                        {
                            int 주문가 = Method.order_price(주문값, 주문구분, 잔고.종목코드, 잔고.현재가); // 주문가격
                            int Tik_cap = Method.Find_Tik_Cap(잔고.현재가, 주문가, 잔고.시장);
                            if (Tik_cap <= 15)
                            {
                                List<JumunItem> item = Form1.JumunItem_List.ToList().FindAll(o => o.종목코드.Equals(잔고.종목코드) && !o.검색식.Contains("잔고청산"));
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

                                            JumunItem.비고 = "잔고청산_미체결일괄 '취소'";
                                        }
                                    }
                                }
                                else
                                {
                                    잔고.매도기준update = false;
                                    잔고.매도기준 = 잔고.보유수량;

                                    Order_Run(" TS");

                                    잔고.매매가능 = true;
                                    if (location.Equals("잔고청산_A")) 잔고.잔고청산_A = true;
                                    if (location.Equals("잔고청산_B")) 잔고.잔고청산_B = true;
                                    if (location.Equals("잔고청산_C")) 잔고.잔고청산_C = true;
                                }
                            }
                            else
                            {
                                switch (location)
                                {
                                    case "잔고청산_A":
                                        잔고.잔고청산_TS_high_A = 0;
                                        break;
                                    case "잔고청산_B":
                                        잔고.잔고청산_TS_high_B = 0;
                                        break;
                                    case "잔고청산_C":
                                        잔고.잔고청산_TS_high_C = 0;
                                        break;
                                }
                            }
                        }
                    }
                }

                void Order_Run(string _TS)
                {
                    int 주문가 = Method.order_price(주문값, 주문구분, 잔고.종목코드, 잔고.현재가); // 주문가격
                    int 예_주문가 = 주문가;

                    if (주문구분 == 0) 예_주문가 = 잔고.현재가;

                    int 주문수량 = Method.주문수량계산(잔고, 예_주문가, 비중, 비중단위, 2);

                    while (true)
                    {
                        bool off = true;

                        if (Method.청산주문_매매범위(잔고, maemae_low, maemae_height))
                        {
                            if (주문구분 < 4)
                            {
                                if (Form1.form1.잔고주문_오더(잔고, location + " [" + 검색식 + "]" + _TS, 2, 비중, 비중단위, 주문값, 주문구분, 취소시간, 취소N주문, 반복시간, "", location, suik_gubun, true, maemae_low))
                                {
                                    청산가동();
                                    off = false;
                                }
                            }
                            else
                            {
                                if (분할주문(location, 2, 주문구분, 잔고.종목코드, 잔고.종목명, 주문수량, 잔고.현재가, location + " [" + 검색식 + "]", 취소시간))
                                {
                                    청산가동();
                                    off = false;
                                }
                            }
                        }

                        if (off) break;
                    }

                    void 청산가동()
                    {
                        switch (location)
                        {
                            case "청산_A":
                                trading_item 청산_A = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_Liquidation_repeat_time_A);
                                Form1.Trading_Item_List.Add(청산_A);
                                잔고.가동_청산A = false;

                                Form1.TT_Liqu_time_A = Properties.Settings.Default.MT_Liquidation_repeat_time_A;
                                Liquidation_condition_A 이탈_A = Form1.Liquidation_condition_List_A.Find(o => o.code.Equals(잔고.종목코드));
                                if (이탈_A != null)
                                {
                                    Form1.Liquidation_condition_List_A.Remove(이탈_A);
                                }
                                break;

                            case "청산_B":
                                trading_item 청산_B = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_Liquidation_repeat_time_B);
                                Form1.Trading_Item_List.Add(청산_B);
                                잔고.가동_청산B = false;

                                Form1.TT_Liqu_time_B = Properties.Settings.Default.MT_Liquidation_repeat_time_B;
                                Liquidation_condition_B 이탈_B = Form1.Liquidation_condition_List_B.Find(o => o.code.Equals(잔고.종목코드));
                                if (이탈_B != null)
                                {
                                    Form1.Liquidation_condition_List_B.Remove(이탈_B);
                                }
                                break;
                            case "청산_C":
                                trading_item 청산_C = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_Liquidation_repeat_time_C);
                                Form1.Trading_Item_List.Add(청산_C);
                                잔고.가동_청산C = false;

                                Form1.TT_Liqu_time_C = Properties.Settings.Default.MT_Liquidation_repeat_time_C;
                                Liquidation_condition_C 이탈_C = Form1.Liquidation_condition_List_C.Find(o => o.code.Equals(잔고.종목코드));
                                if (이탈_C != null)
                                {
                                    Form1.Liquidation_condition_List_C.Remove(이탈_C);
                                }
                                break;
                        }
                    }
                }
            }
        }


        ///////////////               잔고 자동청산                ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////           수익금기준손절 매매             ////////////////

        public static void 수익금기준손절_주문취소(string 검색식)
        {
            List<JumunItem> cansel_jumun = Form1.JumunItem_List.FindAll(o => o.검색식.Equals(검색식));

            for (int i = 0; i < cansel_jumun.Count; i++)
            {
                JumunItem item = cansel_jumun[i];

                item.반복횟수 = 0;
                item.취소시간 = 0;
                item.취소timer = 0;
            }
        }

        public static void 수익금기준손절()
        {
            long 매수기준금 = Properties.Settings.Default.MT_buying_standard;
            int Cut_time = Properties.Settings.Default.MTB_cut_time_A;
            double cut_수익금1 = Properties.Settings.Default.TB_cut_수익금1_A * 10000;
            double cut_수익금2 = Properties.Settings.Default.TB_cut_수익금2_A * 10000;
            double Cut_매입금 = Properties.Settings.Default.TB_cut_won_A * 10000;

            if (Properties.Settings.Default.CB_cut_기준금)
            {
                cut_수익금1 = Properties.Settings.Default.TB_cut_수익금1_A / 100 * 매수기준금;
                cut_수익금2 = Properties.Settings.Default.TB_cut_수익금2_A / 100 * 매수기준금;
                Cut_매입금 = Properties.Settings.Default.TB_cut_won_A / 100 * 매수기준금;
            }

            double Cut_수익율 = Properties.Settings.Default.TB_cut_P_A;
            double Cut_비중 = Properties.Settings.Default.TB_cut_ratio_A;
            int Cut_비중선택 = Properties.Settings.Default.CBB_cut_gubun_A;
            double Cut_주문값 = Properties.Settings.Default.TB_cut_value_A;
            int Cut_주문구분 = Properties.Settings.Default.CBB_cut_jumun_A;
            int Cut_취소시간 = Properties.Settings.Default.MTB_cut_cansel_time_A;
            string 익절그룹 = "Cut_A";
            string 검색식 = "수익금손절_A";

            if (!Form1.form1.Cut_A && !Form1.form1.Cut_B && !Form1.form1.Cut_C &&
                 Form1.form1.cut_LB_A.Equals("X") && Form1.form1.cut_LB_B.Equals("X") && Form1.form1.cut_LB_C.Equals("X"))
            {
                Form1.실현손익_시작 = Form1.Acc[0].실현손익;
                Form1.실현손익_예상 = Form1.Acc[0].실현손익;
            }

            if (Properties.Settings.Default.CB_cut_A)
            {
                if (Form1.timenow >= Cut_time)
                {
                    if (cut_수익금1 <= Form1.실현손익_시작 && Form1.실현손익_시작 < cut_수익금2 && Form1.form1.cut_LB_A.Equals("X"))
                    {
                        Form1.form1.Cut_남길금액_A = Form1.실현손익_예상 * Properties.Settings.Default.TB_cut_남길퍼_A / 100;
                        Form1.form1.Cut_A = true;
                        Form1.form1.cut_LB_A = "O";
                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.CB_cut_LB_A.Text = "O";
                    }

                    if (Form1.form1.Cut_A)
                    {
                        if (Form1.실현손익_예상 > Form1.form1.Cut_남길금액_A) 주문실행();

                        if (Form1.Acc[0].실현손익 <= Form1.form1.Cut_남길금액_A)
                        {
                            수익금기준손절_주문취소("수익금손절_A");
                            Form1.form1.Cut_A = false;
                            매도재개();
                        }
                    }
                }
            }

            if (Properties.Settings.Default.CB_cut_B)
            {
                Cut_time = Properties.Settings.Default.MTB_cut_time_B;
                cut_수익금1 = Properties.Settings.Default.TB_cut_수익금1_B * 10000;
                cut_수익금2 = Properties.Settings.Default.TB_cut_수익금2_B * 10000;
                Cut_매입금 = Properties.Settings.Default.TB_cut_won_B * 10000;

                if (Properties.Settings.Default.CB_cut_기준금)
                {
                    cut_수익금1 = Properties.Settings.Default.TB_cut_수익금1_B / 100 * 매수기준금;
                    cut_수익금2 = Properties.Settings.Default.TB_cut_수익금2_B / 100 * 매수기준금;
                    Cut_매입금 = Properties.Settings.Default.TB_cut_won_B / 100 * 매수기준금;
                }
                Cut_수익율 = Properties.Settings.Default.TB_cut_P_B;
                Cut_비중 = Properties.Settings.Default.TB_cut_ratio_B;
                Cut_비중선택 = Properties.Settings.Default.CBB_cut_gubun_B;
                Cut_주문값 = Properties.Settings.Default.TB_cut_value_B;
                Cut_주문구분 = Properties.Settings.Default.CBB_cut_jumun_B;
                Cut_취소시간 = Properties.Settings.Default.MTB_cut_cansel_time_B;
                익절그룹 = "Cut_B";
                검색식 = "수익금손절_B";

                if (Form1.timenow >= Cut_time)
                {
                    if (cut_수익금1 <= Form1.실현손익_시작 && Form1.실현손익_시작 < cut_수익금2 && Form1.form1.cut_LB_B.Equals("X"))
                    {
                        Form1.form1.Cut_남길금액_B = Form1.실현손익_예상 * Properties.Settings.Default.TB_cut_남길퍼_B / 100;
                        Form1.form1.Cut_B = true;
                        Form1.form1.cut_LB_B = "O";
                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.CB_cut_LB_B.Text = "O";
                    }

                    if (Form1.form1.Cut_B)
                    {
                        if (Form1.실현손익_예상 > Form1.form1.Cut_남길금액_B) 주문실행();

                        if (Form1.Acc[0].실현손익 <= Form1.form1.Cut_남길금액_B)
                        {
                            수익금기준손절_주문취소("수익금손절_B");
                            Form1.form1.Cut_B = false;
                            매도재개();
                        }
                    }
                }
            }

            if (Properties.Settings.Default.CB_cut_C)
            {
                Cut_time = Properties.Settings.Default.MTB_cut_time_C;
                cut_수익금1 = Properties.Settings.Default.TB_cut_수익금1_C * 10000;
                cut_수익금2 = Properties.Settings.Default.TB_cut_수익금2_C * 10000;
                Cut_매입금 = Properties.Settings.Default.TB_cut_won_C * 10000;

                if (Properties.Settings.Default.CB_cut_기준금)
                {
                    cut_수익금1 = Properties.Settings.Default.TB_cut_수익금1_C / 100 * 매수기준금;
                    cut_수익금2 = Properties.Settings.Default.TB_cut_수익금2_C / 100 * 매수기준금;
                    Cut_매입금 = Properties.Settings.Default.TB_cut_won_C / 100 * 매수기준금;
                }
                Cut_수익율 = Properties.Settings.Default.TB_cut_P_C;
                Cut_비중 = Properties.Settings.Default.TB_cut_ratio_C;
                Cut_비중선택 = Properties.Settings.Default.CBB_cut_gubun_C;
                Cut_주문값 = Properties.Settings.Default.TB_cut_value_C;
                Cut_주문구분 = Properties.Settings.Default.CBB_cut_jumun_C;
                Cut_취소시간 = Properties.Settings.Default.MTB_cut_cansel_time_C;
                익절그룹 = "Cut_C";
                검색식 = "수익금손절_C";

                if (Form1.timenow >= Cut_time)
                {
                    if (cut_수익금1 <= Form1.실현손익_시작 && Form1.실현손익_시작 < cut_수익금2 && Form1.form1.cut_LB_C.Equals("X"))
                    {
                        Form1.form1.Cut_남길금액_C = Form1.실현손익_예상 * Properties.Settings.Default.TB_cut_남길퍼_C / 100;
                        Form1.form1.Cut_C = true;
                        Form1.form1.cut_LB_C = "O";
                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.CB_cut_LB_C.Text = "O";
                    }

                    if (Form1.form1.Cut_C)
                    {
                        if (Form1.실현손익_예상 > Form1.form1.Cut_남길금액_C) 주문실행();

                        if (Form1.Acc[0].실현손익 <= Form1.form1.Cut_남길금액_C)
                        {
                            수익금기준손절_주문취소("수익금손절_C");
                            Form1.form1.Cut_C = false;
                            매도재개();
                        }
                    }
                }
            }

            void 주문실행()
            {
                foreach (var code in Form1.stockBalanceList.ToList())
                {
                    Stockbalance 잔고 = code.Value;

                    if (잔고.매매가능 && Form1.재시작)
                    {
                        string 잔고그룹 = GET.그룹변환(잔고.매매그룹);

                        if (잔고.수익률 <= Cut_수익율 && 잔고.매입금액 >= Cut_매입금)
                        {
                            if (GET.익절그룹(익절그룹).Contains(잔고그룹) && Method.추매가능_Check(잔고, false) && Method.매매중복체크(잔고.종목코드, 검색식))
                            {
                                잔고.매도정지 = true;

                                List<JumunItem> Item = Form1.JumunItem_List.FindAll(o => !o.검색식.Contains("수익금손절") && o.종목코드.Equals(잔고.종목코드));

                                if (Item.Count > 0)
                                {
                                    for (int i = 0; i < Item.Count; i++)
                                    {
                                        JumunItem JumunItem = Item[i];
                                        if (JumunItem != null)
                                        {
                                            JumunItem.반복횟수 = 0;
                                            JumunItem.취소시간 = 0;
                                            JumunItem.취소timer = 0;

                                            JumunItem.비고 = "수익금손절_미체결일괄 '취소'";
                                        }
                                    }
                                }

                                Form1.form1.잔고주문_오더(잔고, 검색식, 2, Cut_비중, Cut_비중선택, Cut_주문값, Cut_주문구분, Cut_취소시간, 0, 0, 검색식, 검색식, 0, false, 0);
                            }
                        }
                    }
                }
            }

            void 매도재개()
            {
                foreach (var code in Form1.stockBalanceList.ToList())
                {
                    Stockbalance 잔고 = code.Value;

                    if (잔고.매매가능 && Form1.재시작 && 잔고.매도정지)
                    {
                        string 잔고그룹 = GET.그룹변환(잔고.매매그룹);

                        if (잔고.수익률 <= Cut_수익율)
                        {
                            if (GET.익절그룹(익절그룹).Contains(잔고그룹))
                            {
                                잔고.매도정지 = false;
                            }
                        }
                    }
                }
            }
        }

        ////////////////           수익금기준손절 매매             ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////



        public static bool 분할주문(string location, int 매수매도, int 구분, string _코드, string _종목명, int _주문수량, int _현재가, string _검색식, int 취소시간)
        {
            bool 분할주문_가능 = false;

            int 분할간격 = Properties.Settings.Default.TB_분할간격_A;
            int 분할횟수 = Properties.Settings.Default.TB_분할횟수_A;

            if (구분 == 5)
            {
                분할간격 = Properties.Settings.Default.TB_분할간격_B;
                분할횟수 = Properties.Settings.Default.TB_분할횟수_B;
            }

            if (구분 == 6)
            {
                분할간격 = Properties.Settings.Default.TB_분할간격_C;
                분할횟수 = Properties.Settings.Default.TB_분할횟수_C;
            }

            int 주문호가 = 0;
            int 분할수량 = (int)(Math.Ceiling((double)_주문수량 / (double)분할횟수));

            for (int i = 0; i < 분할횟수; i++)
            {
                if (_주문수량 >= 분할수량)
                {
                    _주문수량 = _주문수량 - 분할수량;
                }
                else if (_주문수량 < 분할수량)
                {
                    if (_주문수량 > 0)
                    {
                        분할수량 = _주문수량;
                        _주문수량 = _주문수량 - 분할수량;
                    }
                    else
                    {
                        break;
                    }
                }

                if (Method.매매확인_VI_모투가능확인(Form1.Market_Item_List[_코드], 매수매도))
                {
                    int ScreenNumber = GET.jumunScreen(_코드);
                    if (ScreenNumber == 1300)
                    {
                        Method.주문초과알림(_종목명);
                    }
                    else
                    {
                        int 분할가격 = Method.Hoga_Calculus(_코드, _현재가, 주문호가);
                        int Order번호 = GET.Order번호();

                        if (매수매도 == 1)
                        {
                            DataManagement.예수금업데이트("매수", 분할가격, 분할수량, "주문", _코드);
                        }
                        else
                        {
                            Stockbalance 잔고 = Form1.stockBalanceList[_코드];

                            if (잔고.주문가능수량 > 0)
                            {
                                if (잔고.주문가능수량 < 분할수량)
                                {
                                    분할수량 = 잔고.주문가능수량;
                                }

                                DataManagement.주문가능수업데이트(잔고, "매도", 분할수량, "매도주문");
                            }
                        }

                        if (분할수량 > 0)
                        {
                            string 검색식 = "분할주문";
                            주문호가 = 주문호가 + 분할간격;

                            JumunItem ItemList = new JumunItem(0, 0, ScreenNumber.ToString(), _코드, _종목명, "++", "---", _검색식, 0, 구분, 취소시간, 0, 0, 검색식, location, 분할수량, 분할가격, 매수매도, 0, 0, 취소시간, _현재가, 0, Form1.timenow,
                                                                 0, true, false, 0, 0, 0, 0, false, 0, Order번호, 0, Form1.NXT_server); // 자동 주문추가 
                            Form1.JumunItem_List.Add(ItemList);

                            Form1.que_order(ScreenNumber.ToString(), _종목명, 매수매도, _코드, 분할수량, 분할가격, "00", "+++", _검색식, Order번호);

                            분할주문_가능 = true;
                        }
                    }
                }
            }

            return 분할주문_가능;
        }
    }
}
