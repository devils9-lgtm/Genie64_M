using System.Collections.Generic;

namespace 지니_64
{
    class Tab_Repeat
    {
        public static void Repeat_condition(string ST_ID, string itemcode, string condition)
        {
            bool ID_A = false;
            bool ID_B = false;
            bool ID_C = false;
            bool ID_D = false;
            bool ID_E = false;
            bool ID_F = false;
            bool ID_G = false;
            bool ID_H = false;
            bool ID_I = false;
            bool ID_J = false;
            bool ID_K = false;
            bool ID_L = false;
            bool ID_M = false;
            bool ID_N = false;

            if (ST_ID.Equals("I"))
            {
                if (Properties.Settings.Default.combo_repeat_use_condition_A.Equals(1)) ID_A = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_B.Equals(1)) ID_B = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_C.Equals(1)) ID_C = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_D.Equals(1)) ID_D = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_E.Equals(1)) ID_E = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_F.Equals(1)) ID_F = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_G.Equals(1)) ID_G = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_H.Equals(1)) ID_H = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_I.Equals(1)) ID_I = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_J.Equals(1)) ID_J = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_K.Equals(1)) ID_K = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_L.Equals(1)) ID_L = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_M.Equals(1)) ID_M = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_N.Equals(1)) ID_N = true;
            }
            else if (ST_ID.Equals("D"))
            {
                if (Properties.Settings.Default.combo_repeat_use_condition_A.Equals(2)) ID_A = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_B.Equals(2)) ID_B = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_C.Equals(2)) ID_C = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_D.Equals(2)) ID_D = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_E.Equals(2)) ID_E = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_F.Equals(2)) ID_F = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_G.Equals(2)) ID_G = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_H.Equals(2)) ID_H = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_I.Equals(2)) ID_I = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_J.Equals(2)) ID_J = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_K.Equals(2)) ID_K = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_L.Equals(2)) ID_L = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_M.Equals(2)) ID_M = true;
                if (Properties.Settings.Default.combo_repeat_use_condition_N.Equals(2)) ID_N = true;
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_A) && Properties.Settings.Default.combo_repeat_use_condition_A > 0)
            {
                Repeat_condition_A 종목 = Form1.Repeat_condition_List_A.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_A)
                    {
                        Repeat_condition_A 추가 = new Repeat_condition_A(itemcode, 0);
                        Form1.Repeat_condition_List_A.Add(추가);
                    }
                }
                else
                {
                    if (!ID_A)
                    {
                        Repeat_condition_A 삭제 = Form1.Repeat_condition_List_A.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_A.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_B) && Properties.Settings.Default.combo_repeat_use_condition_B > 0)
            {
                Repeat_condition_B 종목 = Form1.Repeat_condition_List_B.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_B)
                    {
                        Repeat_condition_B 추가 = new Repeat_condition_B(itemcode, 0);
                        Form1.Repeat_condition_List_B.Add(추가);
                    }
                }
                else
                {
                    if (!ID_B)
                    {
                        Repeat_condition_B 삭제 = Form1.Repeat_condition_List_B.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_B.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_C) && Properties.Settings.Default.combo_repeat_use_condition_C > 0)
            {
                Repeat_condition_C 종목 = Form1.Repeat_condition_List_C.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_C)
                    {
                        Repeat_condition_C 추가 = new Repeat_condition_C(itemcode, 0);
                        Form1.Repeat_condition_List_C.Add(추가);
                    }
                }
                else
                {
                    if (!ID_C)
                    {
                        Repeat_condition_C 삭제 = Form1.Repeat_condition_List_C.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_C.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_D) && Properties.Settings.Default.combo_repeat_use_condition_D > 0)
            {
                Repeat_condition_D 종목 = Form1.Repeat_condition_List_D.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_D)
                    {
                        Repeat_condition_D 추가 = new Repeat_condition_D(itemcode, 0);
                        Form1.Repeat_condition_List_D.Add(추가);
                    }
                }
                else
                {
                    if (!ID_D)
                    {
                        Repeat_condition_D 삭제 = Form1.Repeat_condition_List_D.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_D.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_E) && Properties.Settings.Default.combo_repeat_use_condition_E > 0)
            {
                Repeat_condition_E 종목 = Form1.Repeat_condition_List_E.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_E)
                    {
                        Repeat_condition_E 추가 = new Repeat_condition_E(itemcode, 0);
                        Form1.Repeat_condition_List_E.Add(추가);
                    }
                }
                else
                {
                    if (!ID_E)
                    {
                        Repeat_condition_E 삭제 = Form1.Repeat_condition_List_E.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_E.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_F) && Properties.Settings.Default.combo_repeat_use_condition_F > 0)
            {
                Repeat_condition_F 종목 = Form1.Repeat_condition_List_F.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_F)
                    {
                        Repeat_condition_F 추가 = new Repeat_condition_F(itemcode, 0);
                        Form1.Repeat_condition_List_F.Add(추가);
                    }
                }
                else
                {
                    if (!ID_F)
                    {
                        Repeat_condition_F 삭제 = Form1.Repeat_condition_List_F.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_F.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_G) && Properties.Settings.Default.combo_repeat_use_condition_G > 0)
            {
                Repeat_condition_G 종목 = Form1.Repeat_condition_List_G.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_G)
                    {
                        Repeat_condition_G 추가 = new Repeat_condition_G(itemcode, 0);
                        Form1.Repeat_condition_List_G.Add(추가);
                    }
                }
                else
                {
                    if (!ID_G)
                    {
                        Repeat_condition_G 삭제 = Form1.Repeat_condition_List_G.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_G.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_H) && Properties.Settings.Default.combo_repeat_use_condition_H > 0)
            {
                Repeat_condition_H 종목 = Form1.Repeat_condition_List_H.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_H)
                    {
                        Repeat_condition_H 추가 = new Repeat_condition_H(itemcode, 0);
                        Form1.Repeat_condition_List_H.Add(추가);
                    }
                }
                else
                {
                    if (!ID_H)
                    {
                        Repeat_condition_H 삭제 = Form1.Repeat_condition_List_H.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_H.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_I) && Properties.Settings.Default.combo_repeat_use_condition_I > 0)
            {
                Repeat_condition_I 종목 = Form1.Repeat_condition_List_I.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_I)
                    {
                        Repeat_condition_I 추가 = new Repeat_condition_I(itemcode, 0);
                        Form1.Repeat_condition_List_I.Add(추가);
                    }
                }
                else
                {
                    if (!ID_I)
                    {
                        Repeat_condition_I 삭제 = Form1.Repeat_condition_List_I.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_I.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_J) && Properties.Settings.Default.combo_repeat_use_condition_J > 0)
            {
                Repeat_condition_J 종목 = Form1.Repeat_condition_List_J.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_J)
                    {
                        Repeat_condition_J 추가 = new Repeat_condition_J(itemcode, 0);
                        Form1.Repeat_condition_List_J.Add(추가);
                    }
                }
                else
                {
                    if (!ID_J)
                    {
                        Repeat_condition_J 삭제 = Form1.Repeat_condition_List_J.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_J.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_K) && Properties.Settings.Default.combo_repeat_use_condition_K > 0)
            {
                Repeat_condition_K 종목 = Form1.Repeat_condition_List_K.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_K)
                    {
                        Repeat_condition_K 추가 = new Repeat_condition_K(itemcode, 0);
                        Form1.Repeat_condition_List_K.Add(추가);
                    }
                }
                else
                {
                    if (!ID_K)
                    {
                        Repeat_condition_K 삭제 = Form1.Repeat_condition_List_K.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_K.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_L) && Properties.Settings.Default.combo_repeat_use_condition_L > 0)
            {
                Repeat_condition_L 종목 = Form1.Repeat_condition_List_L.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_L)
                    {
                        Repeat_condition_L 추가 = new Repeat_condition_L(itemcode, 0);
                        Form1.Repeat_condition_List_L.Add(추가);
                    }
                }
                else
                {
                    if (!ID_L)
                    {
                        Repeat_condition_L 삭제 = Form1.Repeat_condition_List_L.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_L.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_M) && Properties.Settings.Default.combo_repeat_use_condition_M > 0)
            {
                Repeat_condition_M 종목 = Form1.Repeat_condition_List_M.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_M)
                    {
                        Repeat_condition_M 추가 = new Repeat_condition_M(itemcode, 0);
                        Form1.Repeat_condition_List_M.Add(추가);
                    }
                }
                else
                {
                    if (!ID_M)
                    {
                        Repeat_condition_M 삭제 = Form1.Repeat_condition_List_M.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_M.Remove(삭제);
                        }
                    }
                }
            }

            if (condition.Equals(Properties.Settings.Default.combo_repeat_condition_N) && Properties.Settings.Default.combo_repeat_use_condition_N > 0)
            {
                Repeat_condition_N 종목 = Form1.Repeat_condition_List_N.Find(o => o.code.Equals(itemcode));
                if (종목 == null)
                {
                    if (ID_N)
                    {
                        Repeat_condition_N 추가 = new Repeat_condition_N(itemcode, 0);
                        Form1.Repeat_condition_List_N.Add(추가);
                    }
                }
                else
                {
                    if (!ID_N)
                    {
                        Repeat_condition_N 삭제 = Form1.Repeat_condition_List_N.Find(o => o.code.Equals(itemcode));
                        if (삭제 != null)
                        {
                            Form1.Repeat_condition_List_N.Remove(삭제);
                        }
                    }
                }
            }
        }

        public static void 반복매매_USE(Stockbalance 잔고)
        {
            if (잔고.주문가능수량 > 0 && Form1.재시작 && 잔고.매매가능)
            {
                Market_Item market_Item = Form1.Market_Item_List[잔고.종목코드];

                long 매수기준금 = Properties.Settings.Default.MT_buying_standard;
                long 매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_A / 100 * 매수기준금);

                int start = Properties.Settings.Default.MT_repeat_time_start_A;
                int end = Properties.Settings.Default.MT_repeat_time_end_A;
                bool 매수도 = Properties.Settings.Default.CB_repeat_kind_A;
                int Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_A;
                double Value = Properties.Settings.Default.TB_repeat_value_A;
                int jumun = Properties.Settings.Default.combo_repeat_jumun_A;
                double suik_low = Properties.Settings.Default.TB_repeat_suik_1_A;
                double suik_height = Properties.Settings.Default.TB_repeat_suik_2_A;
                bool suik_choice = Properties.Settings.Default.CB_repeat_choice_A;
                int suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_A;
                double ratio = Properties.Settings.Default.TB_repeat_sell_ratio_A;
                int gubun = Properties.Settings.Default.combo_repeat_sell_gubun_A;
                double maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_A;
                double maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_A;
                int maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_A;
                string group = GET.익절그룹("반복_A");
                int 취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_A;
                int 취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_A;
                int 반복 = Properties.Settings.Default.MTB_repeat_repeat_A;
                int 누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_A;
                int 누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_A;

                ma min_mma = Form1.Min_ma_list[잔고.종목코드];
                ma day_mma = Form1.Day_ma_list[잔고.종목코드];
                int CBB_mma = Properties.Settings.Default.CBB_repeat_mma_A;
                int CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_A;
                int CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_A;
                double min_mma1 = min_mma.repeat_ma1_A;
                double min_mma2 = min_mma.repeat_ma2_A;

                int CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_A;
                int CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_A;
                int CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_A;
                double day_mma1 = day_mma.repeat_ma1_A;
                double day_mma2 = day_mma.repeat_ma2_A;

                string location = "반복_A";
                string 검색식 = Properties.Settings.Default.combo_repeat_condition_A;
                if (Repeat_use == 0) 검색식 = "";

                if (Properties.Settings.Default.CB_repeat_use_A && 잔고.가동_반복A && Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                {
                    if (Method.RunTime(start, end))
                    {
                        if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                        {
                            if (잔고.매입금액 >= 매입금)
                            {
                                if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                {
                                    if (Repeat_use > 0) // 검색식 사용 
                                    {
                                        Repeat_condition_A Item = Form1.Repeat_condition_List_A.Find(o => o.code.Equals(잔고.종목코드));
                                        if (Item != null)
                                        {
                                            if (Properties.Settings.Default.MTB_repeat_delay_A <= Item.timer)
                                            {
                                                if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                 gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_A))
                                                {
                                                    Form1.Repeat_condition_List_A.Remove(Item);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_repeat_use_B && 잔고.가동_반복B)
                {
                    location = "반복_B";
                    검색식 = Properties.Settings.Default.combo_repeat_condition_B;
                    매수도 = Properties.Settings.Default.CB_repeat_kind_B;
                    Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_B;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_repeat_time_start_B;
                        end = Properties.Settings.Default.MT_repeat_time_end_B;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_repeat_mma_B;
                            CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_B;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_B;
                            min_mma1 = min_mma.repeat_ma1_B;
                            min_mma2 = min_mma.repeat_ma2_B;

                            CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_B;
                            CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_B;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_B;
                            day_mma1 = day_mma.repeat_ma1_B;
                            day_mma2 = day_mma.repeat_ma2_B;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_B / 100 * 매수기준금);

                                if (잔고.매입금액 >= 매입금)
                                {
                                    누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_B;
                                    누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_B;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_repeat_value_B;
                                        jumun = Properties.Settings.Default.combo_repeat_jumun_B;
                                        suik_low = Properties.Settings.Default.TB_repeat_suik_1_B;
                                        suik_height = Properties.Settings.Default.TB_repeat_suik_2_B;
                                        suik_choice = Properties.Settings.Default.CB_repeat_choice_B;
                                        suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_B;
                                        ratio = Properties.Settings.Default.TB_repeat_sell_ratio_B;
                                        gubun = Properties.Settings.Default.combo_repeat_sell_gubun_B;
                                        maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_B;
                                        maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_B;
                                        maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_B;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_B;
                                        취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_B;
                                        반복 = Properties.Settings.Default.MTB_repeat_repeat_B;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Repeat_condition_B Item = Form1.Repeat_condition_List_B.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_repeat_delay_B <= Item.timer)
                                                {
                                                    if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_B))
                                                    {
                                                        Form1.Repeat_condition_List_B.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_repeat_use_C && 잔고.가동_반복C)
                {
                    location = "반복_C";
                    검색식 = Properties.Settings.Default.combo_repeat_condition_C;
                    매수도 = Properties.Settings.Default.CB_repeat_kind_C;
                    Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_C;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_repeat_time_start_C;
                        end = Properties.Settings.Default.MT_repeat_time_end_C;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_repeat_mma_C;
                            CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_C;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_C;
                            min_mma1 = min_mma.repeat_ma1_C;
                            min_mma2 = min_mma.repeat_ma2_C;

                            CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_C;
                            CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_C;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_C;
                            day_mma1 = day_mma.repeat_ma1_C;
                            day_mma2 = day_mma.repeat_ma2_C;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_C / 100 * 매수기준금);

                                if (잔고.매입금액 >= 매입금)
                                {
                                    누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_C;
                                    누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_C;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_repeat_value_C;
                                        jumun = Properties.Settings.Default.combo_repeat_jumun_C;
                                        suik_low = Properties.Settings.Default.TB_repeat_suik_1_C;
                                        suik_height = Properties.Settings.Default.TB_repeat_suik_2_C;
                                        suik_choice = Properties.Settings.Default.CB_repeat_choice_C;
                                        suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_C;
                                        ratio = Properties.Settings.Default.TB_repeat_sell_ratio_C;
                                        gubun = Properties.Settings.Default.combo_repeat_sell_gubun_C;
                                        maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_C;
                                        maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_C;
                                        maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_C;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_C;
                                        취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_C;
                                        반복 = Properties.Settings.Default.MTB_repeat_repeat_C;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Repeat_condition_C Item = Form1.Repeat_condition_List_C.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_repeat_delay_C <= Item.timer)
                                                {
                                                    if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_C))
                                                    {
                                                        Form1.Repeat_condition_List_C.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_repeat_use_D && 잔고.가동_반복D)
                {
                    location = "반복_D";
                    검색식 = Properties.Settings.Default.combo_repeat_condition_D;
                    매수도 = Properties.Settings.Default.CB_repeat_kind_D;
                    Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_D;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_repeat_time_start_D;
                        end = Properties.Settings.Default.MT_repeat_time_end_D;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_repeat_mma_D;
                            CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_D;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_D;
                            min_mma1 = min_mma.repeat_ma1_D;
                            min_mma2 = min_mma.repeat_ma2_D;

                            CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_D;
                            CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_D;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_D;
                            day_mma1 = day_mma.repeat_ma1_D;
                            day_mma2 = day_mma.repeat_ma2_D;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_D / 100 * 매수기준금);

                                if (잔고.매입금액 >= 매입금)
                                {
                                    누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_D;
                                    누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_D;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_repeat_value_D;
                                        jumun = Properties.Settings.Default.combo_repeat_jumun_D;
                                        suik_low = Properties.Settings.Default.TB_repeat_suik_1_D;
                                        suik_height = Properties.Settings.Default.TB_repeat_suik_2_D;
                                        suik_choice = Properties.Settings.Default.CB_repeat_choice_D;
                                        suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_D;
                                        ratio = Properties.Settings.Default.TB_repeat_sell_ratio_D;
                                        gubun = Properties.Settings.Default.combo_repeat_sell_gubun_D;
                                        maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_D;
                                        maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_D;
                                        maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_D;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_D;
                                        취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_D;
                                        반복 = Properties.Settings.Default.MTB_repeat_repeat_D;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Repeat_condition_D Item = Form1.Repeat_condition_List_D.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_repeat_delay_D <= Item.timer)
                                                {
                                                    if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_D))
                                                    {
                                                        Form1.Repeat_condition_List_D.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_repeat_use_E && 잔고.가동_반복E)
                {
                    location = "반복_E";
                    검색식 = Properties.Settings.Default.combo_repeat_condition_E;
                    매수도 = Properties.Settings.Default.CB_repeat_kind_E;
                    Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_E;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_repeat_time_start_E;
                        end = Properties.Settings.Default.MT_repeat_time_end_E;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_repeat_mma_E;
                            CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_E;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_E;
                            min_mma1 = min_mma.repeat_ma1_E;
                            min_mma2 = min_mma.repeat_ma2_E;

                            CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_E;
                            CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_E;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_E;
                            day_mma1 = day_mma.repeat_ma1_E;
                            day_mma2 = day_mma.repeat_ma2_E;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_E / 100 * 매수기준금);

                                if (잔고.매입금액 >= 매입금)
                                {
                                    누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_E;
                                    누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_E;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_repeat_value_E;
                                        jumun = Properties.Settings.Default.combo_repeat_jumun_E;
                                        suik_low = Properties.Settings.Default.TB_repeat_suik_1_E;
                                        suik_height = Properties.Settings.Default.TB_repeat_suik_2_E;
                                        suik_choice = Properties.Settings.Default.CB_repeat_choice_E;
                                        suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_E;
                                        ratio = Properties.Settings.Default.TB_repeat_sell_ratio_E;
                                        gubun = Properties.Settings.Default.combo_repeat_sell_gubun_E;
                                        maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_E;
                                        maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_E;
                                        maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_E;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_E;
                                        취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_E;
                                        반복 = Properties.Settings.Default.MTB_repeat_repeat_E;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Repeat_condition_E Item = Form1.Repeat_condition_List_E.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_repeat_delay_E <= Item.timer)
                                                {
                                                    if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_E))
                                                    {
                                                        Form1.Repeat_condition_List_E.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_repeat_use_F && 잔고.가동_반복F)
                {
                    location = "반복_F";
                    검색식 = Properties.Settings.Default.combo_repeat_condition_F;
                    매수도 = Properties.Settings.Default.CB_repeat_kind_F;
                    Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_F;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_repeat_time_start_F;
                        end = Properties.Settings.Default.MT_repeat_time_end_F;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_repeat_mma_F;
                            CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_F;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_F;
                            min_mma1 = min_mma.repeat_ma1_F;
                            min_mma2 = min_mma.repeat_ma2_F;

                            CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_F;
                            CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_F;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_F;
                            day_mma1 = day_mma.repeat_ma1_F;
                            day_mma2 = day_mma.repeat_ma2_F;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_F / 100 * 매수기준금);

                                if (잔고.매입금액 >= 매입금)
                                {
                                    누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_F;
                                    누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_F;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_repeat_value_F;
                                        jumun = Properties.Settings.Default.combo_repeat_jumun_F;
                                        suik_low = Properties.Settings.Default.TB_repeat_suik_1_F;
                                        suik_height = Properties.Settings.Default.TB_repeat_suik_2_F;
                                        suik_choice = Properties.Settings.Default.CB_repeat_choice_F;
                                        suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_F;
                                        ratio = Properties.Settings.Default.TB_repeat_sell_ratio_F;
                                        gubun = Properties.Settings.Default.combo_repeat_sell_gubun_F;
                                        maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_F;
                                        maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_F;
                                        maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_F;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_F;
                                        취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_F;
                                        반복 = Properties.Settings.Default.MTB_repeat_repeat_F;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Repeat_condition_F Item = Form1.Repeat_condition_List_F.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_repeat_delay_F <= Item.timer)
                                                {
                                                    if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_F))
                                                    {
                                                        Form1.Repeat_condition_List_F.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_repeat_use_G && 잔고.가동_반복G)
                {
                    location = "반복_G";
                    검색식 = Properties.Settings.Default.combo_repeat_condition_G;
                    매수도 = Properties.Settings.Default.CB_repeat_kind_G;
                    Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_G;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_repeat_time_start_G;
                        end = Properties.Settings.Default.MT_repeat_time_end_G;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_repeat_mma_G;
                            CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_G;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_G;
                            min_mma1 = min_mma.repeat_ma1_G;
                            min_mma2 = min_mma.repeat_ma2_G;

                            CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_G;
                            CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_G;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_G;
                            day_mma1 = day_mma.repeat_ma1_G;
                            day_mma2 = day_mma.repeat_ma2_G;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_G / 100 * 매수기준금);

                                if (잔고.매입금액 >= 매입금)
                                {
                                    누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_G;
                                    누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_G;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_repeat_value_G;
                                        jumun = Properties.Settings.Default.combo_repeat_jumun_G;
                                        suik_low = Properties.Settings.Default.TB_repeat_suik_1_G;
                                        suik_height = Properties.Settings.Default.TB_repeat_suik_2_G;
                                        suik_choice = Properties.Settings.Default.CB_repeat_choice_G;
                                        suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_G;
                                        ratio = Properties.Settings.Default.TB_repeat_sell_ratio_G;
                                        gubun = Properties.Settings.Default.combo_repeat_sell_gubun_G;
                                        maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_G;
                                        maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_G;
                                        maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_G;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_G;
                                        취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_G;
                                        반복 = Properties.Settings.Default.MTB_repeat_repeat_G;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Repeat_condition_G Item = Form1.Repeat_condition_List_G.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_repeat_delay_G <= Item.timer)
                                                {
                                                    if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_G))
                                                    {
                                                        Form1.Repeat_condition_List_G.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_repeat_use_H && 잔고.가동_반복H)
                {
                    location = "반복_H";
                    검색식 = Properties.Settings.Default.combo_repeat_condition_H;
                    매수도 = Properties.Settings.Default.CB_repeat_kind_H;
                    Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_H;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_repeat_time_start_H;
                        end = Properties.Settings.Default.MT_repeat_time_end_H;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_repeat_mma_H;
                            CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_H;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_H;
                            min_mma1 = min_mma.repeat_ma1_H;
                            min_mma2 = min_mma.repeat_ma2_H;

                            CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_H;
                            CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_H;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_H;
                            day_mma1 = day_mma.repeat_ma1_H;
                            day_mma2 = day_mma.repeat_ma2_H;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_H / 100 * 매수기준금);

                                if (잔고.매입금액 >= 매입금)
                                {
                                    누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_H;
                                    누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_H;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_repeat_value_H;
                                        jumun = Properties.Settings.Default.combo_repeat_jumun_H;
                                        suik_low = Properties.Settings.Default.TB_repeat_suik_1_H;
                                        suik_height = Properties.Settings.Default.TB_repeat_suik_2_H;
                                        suik_choice = Properties.Settings.Default.CB_repeat_choice_H;
                                        suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_H;
                                        ratio = Properties.Settings.Default.TB_repeat_sell_ratio_H;
                                        gubun = Properties.Settings.Default.combo_repeat_sell_gubun_H;
                                        maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_H;
                                        maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_H;
                                        maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_H;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_H;
                                        취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_H;
                                        반복 = Properties.Settings.Default.MTB_repeat_repeat_H;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Repeat_condition_H Item = Form1.Repeat_condition_List_H.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_repeat_delay_H <= Item.timer)
                                                {
                                                    if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_H))
                                                    {
                                                        Form1.Repeat_condition_List_H.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_repeat_use_I && 잔고.가동_반복I)
                {
                    location = "반복_I";
                    검색식 = Properties.Settings.Default.combo_repeat_condition_I;
                    매수도 = Properties.Settings.Default.CB_repeat_kind_I;
                    Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_I;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_repeat_time_start_I;
                        end = Properties.Settings.Default.MT_repeat_time_end_I;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_repeat_mma_I;
                            CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_I;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_I;
                            min_mma1 = min_mma.repeat_ma1_I;
                            min_mma2 = min_mma.repeat_ma2_I;

                            CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_I;
                            CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_I;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_I;
                            day_mma1 = day_mma.repeat_ma1_I;
                            day_mma2 = day_mma.repeat_ma2_I;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_I / 100 * 매수기준금);

                                if (잔고.매입금액 >= 매입금)
                                {
                                    누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_I;
                                    누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_I;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_repeat_value_I;
                                        jumun = Properties.Settings.Default.combo_repeat_jumun_I;
                                        suik_low = Properties.Settings.Default.TB_repeat_suik_1_I;
                                        suik_height = Properties.Settings.Default.TB_repeat_suik_2_I;
                                        suik_choice = Properties.Settings.Default.CB_repeat_choice_I;
                                        suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_I;
                                        ratio = Properties.Settings.Default.TB_repeat_sell_ratio_I;
                                        gubun = Properties.Settings.Default.combo_repeat_sell_gubun_I;
                                        maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_I;
                                        maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_I;
                                        maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_I;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_I;
                                        취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_I;
                                        반복 = Properties.Settings.Default.MTB_repeat_repeat_I;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Repeat_condition_I Item = Form1.Repeat_condition_List_I.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_repeat_delay_I <= Item.timer)
                                                {
                                                    if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_I))
                                                    {
                                                        Form1.Repeat_condition_List_I.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_repeat_use_J && 잔고.가동_반복J)
                {
                    location = "반복_J";
                    검색식 = Properties.Settings.Default.combo_repeat_condition_J;
                    매수도 = Properties.Settings.Default.CB_repeat_kind_J;
                    Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_J;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_repeat_time_start_J;
                        end = Properties.Settings.Default.MT_repeat_time_end_J;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_repeat_mma_J;
                            CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_J;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_J;
                            min_mma1 = min_mma.repeat_ma1_J;
                            min_mma2 = min_mma.repeat_ma2_J;

                            CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_J;
                            CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_J;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_J;
                            day_mma1 = day_mma.repeat_ma1_J;
                            day_mma2 = day_mma.repeat_ma2_J;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_J / 100 * 매수기준금);

                                if (잔고.매입금액 >= 매입금)
                                {
                                    누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_J;
                                    누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_J;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_repeat_value_J;
                                        jumun = Properties.Settings.Default.combo_repeat_jumun_J;
                                        suik_low = Properties.Settings.Default.TB_repeat_suik_1_J;
                                        suik_height = Properties.Settings.Default.TB_repeat_suik_2_J;
                                        suik_choice = Properties.Settings.Default.CB_repeat_choice_J;
                                        suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_J;
                                        ratio = Properties.Settings.Default.TB_repeat_sell_ratio_J;
                                        gubun = Properties.Settings.Default.combo_repeat_sell_gubun_J;
                                        maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_J;
                                        maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_J;
                                        maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_J;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_J;
                                        취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_J;
                                        반복 = Properties.Settings.Default.MTB_repeat_repeat_J;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Repeat_condition_J Item = Form1.Repeat_condition_List_J.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_repeat_delay_J <= Item.timer)
                                                {
                                                    if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_J))
                                                    {
                                                        Form1.Repeat_condition_List_J.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_repeat_use_K && 잔고.가동_반복K)
                {
                    location = "반복_K";
                    검색식 = Properties.Settings.Default.combo_repeat_condition_K;
                    매수도 = Properties.Settings.Default.CB_repeat_kind_K;
                    Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_K;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_repeat_time_start_K;
                        end = Properties.Settings.Default.MT_repeat_time_end_K;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_repeat_mma_K;
                            CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_K;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_K;
                            min_mma1 = min_mma.repeat_ma1_K;
                            min_mma2 = min_mma.repeat_ma2_K;

                            CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_K;
                            CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_K;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_K;
                            day_mma1 = day_mma.repeat_ma1_K;
                            day_mma2 = day_mma.repeat_ma2_K;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_K / 100 * 매수기준금);

                                if (잔고.매입금액 >= 매입금)
                                {
                                    누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_K;
                                    누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_K;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_repeat_value_K;
                                        jumun = Properties.Settings.Default.combo_repeat_jumun_K;
                                        suik_low = Properties.Settings.Default.TB_repeat_suik_1_K;
                                        suik_height = Properties.Settings.Default.TB_repeat_suik_2_K;
                                        suik_choice = Properties.Settings.Default.CB_repeat_choice_K;
                                        suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_K;
                                        ratio = Properties.Settings.Default.TB_repeat_sell_ratio_K;
                                        gubun = Properties.Settings.Default.combo_repeat_sell_gubun_K;
                                        maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_K;
                                        maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_K;
                                        maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_K;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_K;
                                        취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_K;
                                        반복 = Properties.Settings.Default.MTB_repeat_repeat_K;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Repeat_condition_K Item = Form1.Repeat_condition_List_K.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_repeat_delay_K <= Item.timer)
                                                {
                                                    if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_K))
                                                    {
                                                        Form1.Repeat_condition_List_K.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_repeat_use_L && 잔고.가동_반복L)
                {
                    location = "반복_L";
                    검색식 = Properties.Settings.Default.combo_repeat_condition_L;
                    매수도 = Properties.Settings.Default.CB_repeat_kind_L;
                    Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_L;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_repeat_time_start_L;
                        end = Properties.Settings.Default.MT_repeat_time_end_L;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_repeat_mma_L;
                            CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_L;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_L;
                            min_mma1 = min_mma.repeat_ma1_L;
                            min_mma2 = min_mma.repeat_ma2_L;

                            CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_L;
                            CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_L;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_L;
                            day_mma1 = day_mma.repeat_ma1_L;
                            day_mma2 = day_mma.repeat_ma2_L;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_L / 100 * 매수기준금);

                                if (잔고.매입금액 >= 매입금)
                                {
                                    누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_L;
                                    누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_L;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_repeat_value_L;
                                        jumun = Properties.Settings.Default.combo_repeat_jumun_L;
                                        suik_low = Properties.Settings.Default.TB_repeat_suik_1_L;
                                        suik_height = Properties.Settings.Default.TB_repeat_suik_2_L;
                                        suik_choice = Properties.Settings.Default.CB_repeat_choice_L;
                                        suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_L;
                                        ratio = Properties.Settings.Default.TB_repeat_sell_ratio_L;
                                        gubun = Properties.Settings.Default.combo_repeat_sell_gubun_L;
                                        maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_L;
                                        maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_L;
                                        maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_L;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_L;
                                        취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_L;
                                        반복 = Properties.Settings.Default.MTB_repeat_repeat_L;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Repeat_condition_L Item = Form1.Repeat_condition_List_L.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_repeat_delay_L <= Item.timer)
                                                {
                                                    if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_L))
                                                    {
                                                        Form1.Repeat_condition_List_L.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_repeat_use_M && 잔고.가동_반복M)
                {
                    location = "반복_M";
                    검색식 = Properties.Settings.Default.combo_repeat_condition_M;
                    매수도 = Properties.Settings.Default.CB_repeat_kind_M;
                    Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_M;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_repeat_time_start_M;
                        end = Properties.Settings.Default.MT_repeat_time_end_M;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_repeat_mma_M;
                            CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_M;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_M;
                            min_mma1 = min_mma.repeat_ma1_M;
                            min_mma2 = min_mma.repeat_ma2_M;

                            CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_M;
                            CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_M;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_M;
                            day_mma1 = day_mma.repeat_ma1_M;
                            day_mma2 = day_mma.repeat_ma2_M;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_M / 100 * 매수기준금);

                                if (잔고.매입금액 >= 매입금)
                                {
                                    누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_M;
                                    누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_M;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_repeat_value_M;
                                        jumun = Properties.Settings.Default.combo_repeat_jumun_M;
                                        suik_low = Properties.Settings.Default.TB_repeat_suik_1_M;
                                        suik_height = Properties.Settings.Default.TB_repeat_suik_2_M;
                                        suik_choice = Properties.Settings.Default.CB_repeat_choice_M;
                                        suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_M;
                                        ratio = Properties.Settings.Default.TB_repeat_sell_ratio_M;
                                        gubun = Properties.Settings.Default.combo_repeat_sell_gubun_M;
                                        maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_M;
                                        maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_M;
                                        maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_M;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_M;
                                        취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_M;
                                        반복 = Properties.Settings.Default.MTB_repeat_repeat_M;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Repeat_condition_M Item = Form1.Repeat_condition_List_M.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_repeat_delay_M <= Item.timer)
                                                {
                                                    if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_M))
                                                    {
                                                        Form1.Repeat_condition_List_M.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Properties.Settings.Default.CB_repeat_use_N && 잔고.가동_반복N)
                {
                    location = "반복_N";
                    검색식 = Properties.Settings.Default.combo_repeat_condition_N;
                    매수도 = Properties.Settings.Default.CB_repeat_kind_N;
                    Repeat_use = Properties.Settings.Default.combo_repeat_use_condition_N;
                    if (Repeat_use == 0) 검색식 = "";

                    if (Method.추매가능_Check(잔고, 매수도) && Method.매매중복체크(잔고.종목코드, location + " [" + 검색식 + "]"))
                    {
                        start = Properties.Settings.Default.MT_repeat_time_start_N;
                        end = Properties.Settings.Default.MT_repeat_time_end_N;

                        if (Method.RunTime(start, end))
                        {
                            CBB_mma = Properties.Settings.Default.CBB_repeat_mma_N;
                            CBB_mma2 = Properties.Settings.Default.CBB_repeat_mma2_N;
                            CBB_mma_배열 = Properties.Settings.Default.CBB_repeat_mma_배열_N;
                            min_mma1 = min_mma.repeat_ma1_N;
                            min_mma2 = min_mma.repeat_ma2_N;

                            CBB_dma1 = Properties.Settings.Default.CBB_repeat_dma1_N;
                            CBB_dma2 = Properties.Settings.Default.CBB_repeat_dma2_N;
                            CBB_dma_배열 = Properties.Settings.Default.CBB_repeat_dma_배열_N;
                            day_mma1 = day_mma.repeat_ma1_N;
                            day_mma2 = day_mma.repeat_ma2_N;

                            if (MA.Get_이평(잔고, CBB_mma, CBB_mma2, CBB_mma_배열, min_mma1, min_mma2) && MA.Get_이평(잔고, CBB_dma1, CBB_dma2, CBB_dma_배열, day_mma1, day_mma2))
                            {
                                매입금 = (long)(Properties.Settings.Default.TB_repeat_매입금_N / 100 * 매수기준금);

                                if (잔고.매입금액 >= 매입금)
                                {
                                    누적거래량 = Properties.Settings.Default.TB_repeat_누적거래량_N;
                                    누적거래대금 = Properties.Settings.Default.TB_repeat_누적거래대금_N;

                                    if (market_Item.누적거래량 >= 누적거래량 && market_Item.누적거래대금 >= 누적거래대금)
                                    {
                                        Value = Properties.Settings.Default.TB_repeat_value_N;
                                        jumun = Properties.Settings.Default.combo_repeat_jumun_N;
                                        suik_low = Properties.Settings.Default.TB_repeat_suik_1_N;
                                        suik_height = Properties.Settings.Default.TB_repeat_suik_2_N;
                                        suik_choice = Properties.Settings.Default.CB_repeat_choice_N;
                                        suik_gubun = Properties.Settings.Default.combo_repeat_suik_gubun_N;
                                        ratio = Properties.Settings.Default.TB_repeat_sell_ratio_N;
                                        gubun = Properties.Settings.Default.combo_repeat_sell_gubun_N;
                                        maemae_low = Properties.Settings.Default.TB_repeat_maemae_1_N;
                                        maemae_height = Properties.Settings.Default.TB_repeat_maemae_2_N;
                                        maemae_gubun = Properties.Settings.Default.combo_repeat_maemae_gubun_N;
                                        group = GET.익절그룹(location);
                                        취소시간 = Properties.Settings.Default.MTB_repeat_Cancel_time_N;
                                        취소N주문 = Properties.Settings.Default.combo_repeat_Cancel_N;
                                        반복 = Properties.Settings.Default.MTB_repeat_repeat_N;

                                        if (Repeat_use > 0) // 검색식 사용 
                                        {
                                            Repeat_condition_N Item = Form1.Repeat_condition_List_N.Find(o => o.code.Equals(잔고.종목코드));
                                            if (Item != null)
                                            {
                                                if (Properties.Settings.Default.MTB_repeat_delay_N <= Item.timer)
                                                {
                                                    if (반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio,
                                                     gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, Properties.Settings.Default.combo_repeat_condition_N))
                                                    {
                                                        Form1.Repeat_condition_List_N.Remove(Item);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            반복매매(잔고, 매수도, Value, jumun, suik_low, suik_height, suik_choice, suik_gubun, ratio, gubun, maemae_low, maemae_height, maemae_gubun, group, 취소시간, 취소N주문, 반복, location, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static bool 반복매매(Stockbalance 잔고, bool 매수도, double 주문값, int 주문구분, double suik_low, double suik_height, bool suik_choice, int suik_gubun, double 비중,
                                    int 비중단위, double maemae_low, double maemae_height, int maemae_gubun, string 매매그룹, int 취소시간, int 취소N주문, int 반복시간, string location, string 검색식)
        {
            bool result = false;


            if (매매그룹.Contains(GET.그룹변환(잔고.매매그룹)) && Tab_InterestGroup.관심그룹확인(location, 잔고.종목코드))
            {
                bool 단위_기준금 = Properties.Settings.Default.CB_Repeat_기준금;

                if (location.Contains("리밸_"))
                    단위_기준금 = Properties.Settings.Default.CB_rebalance_기준금;

                bool 반복매매Run = true;

                List<JumunItem> item = Form1.JumunItem_List.FindAll(o => o.종목코드.Equals(잔고.종목코드));
                if (item.Count > 0)
                {
                    for (int n = 0; n < item.Count; n++)
                    {
                        if (item[n].location.Equals("신규매수"))
                        {
                            반복매매Run = false;
                        }
                    }
                }

                if (반복매매Run)
                {
                    if (Method.매입비추매제한(잔고.보유비중))
                    {
                        //0 수익률(%)
                        //1 평가손익금
                        //2 예상손익금
                        //3 등락율(%)
                        //4 수익률 + 예상
                        //5 하기준(기준 + 수익률 이하)
                        //6 상기준(기준 + 수익률 이상)
                        //7 하최종(최종매입가 이하)

                        if (!suik_choice) // →
                        {
                            if (Method.수익범위(매수도, 단위_기준금, 잔고, suik_low, suik_height, suik_gubun, location))
                            {
                                매매진행();
                            }
                        }
                        else // ⇒
                        {
                            switch (location)
                            {
                                case "반복_A":
                                    잔고.반복A = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복A, "A", 매수도);
                                    if (잔고.반복A.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "반복_B":
                                    잔고.반복B = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복B, "B", 매수도);
                                    if (잔고.반복B.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "반복_C":
                                    잔고.반복C = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복C, "C", 매수도);
                                    if (잔고.반복C.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "반복_D":
                                    잔고.반복D = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복D, "D", 매수도);
                                    if (잔고.반복D.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "반복_E":
                                    잔고.반복E = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복E, "E", 매수도);
                                    if (잔고.반복E.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "반복_F":
                                    잔고.반복F = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복F, "F", 매수도);
                                    if (잔고.반복F.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "반복_G":
                                    잔고.반복G = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복G, "G", 매수도);
                                    if (잔고.반복G.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "반복_H":
                                    잔고.반복H = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복H, "H", 매수도);
                                    if (잔고.반복H.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "반복_I":
                                    잔고.반복I = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복I, "I", 매수도);
                                    if (잔고.반복I.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "반복_J":
                                    잔고.반복J = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복J, "J", 매수도);
                                    if (잔고.반복J.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "반복_K":
                                    잔고.반복K = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복K, "K", 매수도);
                                    if (잔고.반복K.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "반복_L":
                                    잔고.반복L = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복L, "L", 매수도);
                                    if (잔고.반복L.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "반복_M":
                                    잔고.반복M = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복M, "M", 매수도);
                                    if (잔고.반복M.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "반복_N":
                                    잔고.반복N = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복N, "N", 매수도);
                                    if (잔고.반복N.Equals("X"))
                                    {
                                        매매진행();
                                    }
                                    break;

                                case "리밸_A":
                                    잔고.리벨A = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨A, "A", 매수도);
                                    if (잔고.리벨A.Equals("X"))
                                    {
                                        매매진행();
                                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_A.Text = "O";
                                    }
                                    break;

                                case "리밸_B":
                                    잔고.리벨B = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨B, "B", 매수도);
                                    if (잔고.리벨B.Equals("X"))
                                    {
                                        매매진행();
                                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_B.Text = "O";
                                    }
                                    break;

                                case "리밸_C":
                                    잔고.리벨C = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨C, "C", 매수도);
                                    if (잔고.리벨C.Equals("X"))
                                    {
                                        매매진행();
                                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_C.Text = "O";
                                    }
                                    break;
                                case "리밸_D":
                                    잔고.리벨D = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨D, "D", 매수도);
                                    if (잔고.리벨D.Equals("X"))
                                    {
                                        매매진행();
                                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_D.Text = "O";
                                    }
                                    break;
                                case "리밸_E":
                                    잔고.리벨E = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨E, "E", 매수도);
                                    if (잔고.리벨E.Equals("X"))
                                    {
                                        매매진행();
                                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_E.Text = "O";
                                    }
                                    break;
                                case "리밸_F":
                                    잔고.리벨F = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨F, "F", 매수도);
                                    if (잔고.리벨F.Equals("X"))
                                    {
                                        매매진행();
                                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_F.Text = "O";
                                    }
                                    break;
                                case "리밸_G":
                                    잔고.리벨G = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨G, "G", 매수도);
                                    if (잔고.리벨G.Equals("X"))
                                    {
                                        매매진행();
                                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_G.Text = "O";
                                    }
                                    break;
                            }
                        }
                    }

                    void 매매진행()
                    {
                        if (Method.매매범위(maemae_low, maemae_height, maemae_gubun, 잔고.매입금액))
                        {
                            int 주문가 = Method.order_price(주문값, 주문구분, 잔고.종목코드, 잔고.현재가); // 주문가격
                            int 예_주문가 = 주문가;
                            int 주문유형 = 2;

                            if (주문구분 == 0) // 시장가
                                예_주문가 = 잔고.현재가;

                            if (매수도) 주문유형 = 1;
                            int 주문수량 = Method.주문수량계산(잔고, 예_주문가, 비중, 비중단위, 주문유형);

                            if (주문수량 < 1)
                            {
                                주문수량 = 1;
                            }

                            if (잔고.주문가능수량 > 0)
                            {

                                if (매수도) // 추가매수
                                {
                                    if (!Form1.추가매수정지 && Form1.form1.RB_buy_run.Checked)
                                    {
                                        if (잔고.추매딜레이 == 0 && !잔고.추매정지)
                                        {
                                            if (Method.매입비추매제한(잔고.보유비중))
                                            {
                                                if (Jisu_linkage.업종별지수연동("추매", Form1.Market_Item_List[잔고.종목코드]))
                                                {
                                                    if (Properties.Settings.Default.CB_계좌매입비_매수제한) // 매입비
                                                    {
                                                        if (!Method.매입비매수제한(Properties.Settings.Default.CBB_계좌매입비_제한선택).Contains("추매"))
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

                                            void 주문전달()
                                            {
                                                if (잔고.매수제한)
                                                {
                                                    if (주문구분 < 4)
                                                    {
                                                        if (Form1.form1.잔고주문_오더(잔고, location + " [" + 검색식 + "]", 1, 비중, 비중단위, 주문값, 주문구분, 취소시간, 취소N주문, 반복시간, "", location, suik_gubun, false, 0))
                                                        {
                                                            반복가동();
                                                            result = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        주문수량 = Method.최대매수금_주문수량계산(잔고, 주문수량);

                                                        if (주문수량 > 0)
                                                        {
                                                            if (Tab_AccountManagement.분할주문(location, 1, 주문구분, 잔고.종목코드, 잔고.종목명, 주문수량, 잔고.현재가, location + " [" + 검색식 + "]", 취소시간))
                                                            {
                                                                반복가동();
                                                                result = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Form1.Error_Log("[매수 제한]최대 매수금 초과 종목: " + 잔고.종목명 + " 주문가격: " + 예_주문가 + " 주문수량: " + 주문수량 + " 주문금액: " + (예_주문가 * 주문수량));
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else // 매도 
                                {
                                    if (Form1.form1.RB_sell_run.Checked && !잔고.매도정지)
                                    {
                                        if (잔고.주문가능수량 >= 주문수량)
                                        {
                                            if (주문구분 < 4)
                                            {
                                                if (Form1.form1.잔고주문_오더(잔고, location + " [" + 검색식 + "]", 2, 비중, 비중단위, 주문값, 주문구분, 취소시간, 취소N주문, 반복시간, "", location, 0, false, 0))
                                                {
                                                    반복가동();
                                                    result = true;
                                                }
                                            }
                                            else
                                            {
                                                if (Tab_AccountManagement.분할주문(location, 2, 주문구분, 잔고.종목코드, 잔고.종목명, 주문수량, 잔고.현재가, location + " [" + 검색식 + "]", 취소시간))
                                                {
                                                    반복가동();
                                                    result = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        void 반복가동()
                        {
                            switch (location)
                            {
                                case "반복_A":
                                    trading_item Repeat_A = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_A);
                                    Form1.Trading_Item_List.Add(Repeat_A);
                                    Form1.Repeat_time_A = Properties.Settings.Default.MT_repeat_repeat_time_A;
                                    잔고.가동_반복A = false;
                                    break;

                                case "반복_B":
                                    trading_item Repeat_B = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_B);
                                    Form1.Trading_Item_List.Add(Repeat_B);
                                    Form1.Repeat_time_B = Properties.Settings.Default.MT_repeat_repeat_time_B;
                                    잔고.가동_반복B = false;
                                    break;

                                case "반복_C":
                                    trading_item Repeat_C = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_C);
                                    Form1.Trading_Item_List.Add(Repeat_C);
                                    Form1.Repeat_time_C = Properties.Settings.Default.MT_repeat_repeat_time_C;
                                    잔고.가동_반복C = false;
                                    break;

                                case "반복_D":
                                    trading_item Repeat_D = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_D);
                                    Form1.Trading_Item_List.Add(Repeat_D);
                                    Form1.Repeat_time_D = Properties.Settings.Default.MT_repeat_repeat_time_D;
                                    잔고.가동_반복D = false;
                                    break;

                                case "반복_E":
                                    trading_item Repeat_E = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_E);
                                    Form1.Trading_Item_List.Add(Repeat_E);
                                    Form1.Repeat_time_E = Properties.Settings.Default.MT_repeat_repeat_time_E;
                                    잔고.가동_반복E = false;
                                    break;

                                case "반복_F":
                                    trading_item Repeat_F = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_F);
                                    Form1.Trading_Item_List.Add(Repeat_F);
                                    Form1.Repeat_time_F = Properties.Settings.Default.MT_repeat_repeat_time_F;
                                    잔고.가동_반복F = false;
                                    break;

                                case "반복_G":
                                    trading_item Repeat_G = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_G);
                                    Form1.Trading_Item_List.Add(Repeat_G);
                                    Form1.Repeat_time_G = Properties.Settings.Default.MT_repeat_repeat_time_G;
                                    잔고.가동_반복G = false;
                                    break;

                                case "반복_H":
                                    trading_item Repeat_H = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_H);
                                    Form1.Trading_Item_List.Add(Repeat_H);
                                    Form1.Repeat_time_H = Properties.Settings.Default.MT_repeat_repeat_time_H;
                                    잔고.가동_반복H = false;
                                    break;

                                case "반복_I":
                                    trading_item Repeat_I = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_I);
                                    Form1.Trading_Item_List.Add(Repeat_I);
                                    Form1.Repeat_time_I = Properties.Settings.Default.MT_repeat_repeat_time_I;
                                    잔고.가동_반복I = false;
                                    break;

                                case "반복_J":
                                    trading_item Repeat_J = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_J);
                                    Form1.Trading_Item_List.Add(Repeat_J);
                                    Form1.Repeat_time_J = Properties.Settings.Default.MT_repeat_repeat_time_J;
                                    잔고.가동_반복J = false;
                                    break;

                                case "반복_K":
                                    trading_item Repeat_K = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_K);
                                    Form1.Trading_Item_List.Add(Repeat_K);
                                    Form1.Repeat_time_K = Properties.Settings.Default.MT_repeat_repeat_time_K;
                                    잔고.가동_반복K = false;
                                    break;

                                case "반복_L":
                                    trading_item Repeat_L = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_L);
                                    Form1.Trading_Item_List.Add(Repeat_L);
                                    Form1.Repeat_time_L = Properties.Settings.Default.MT_repeat_repeat_time_L;
                                    잔고.가동_반복L = false;
                                    break;

                                case "반복_M":
                                    trading_item Repeat_M = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_M);
                                    Form1.Trading_Item_List.Add(Repeat_M);
                                    Form1.Repeat_time_M = Properties.Settings.Default.MT_repeat_repeat_time_M;
                                    잔고.가동_반복M = false;
                                    break;

                                case "반복_N":
                                    trading_item Repeat_N = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_repeat_repeat_time_N);
                                    Form1.Trading_Item_List.Add(Repeat_N);
                                    Form1.Repeat_time_N = Properties.Settings.Default.MT_repeat_repeat_time_N;
                                    잔고.가동_반복N = false;
                                    break;

                                case "리밸_A":
                                    trading_item _rebalance_A = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_rebalance_repeat_time_A);
                                    Form1.Trading_Item_List.Add(_rebalance_A);
                                    Form1.TT_rebalance_time_A = Properties.Settings.Default.MT_rebalance_repeat_time_A;
                                    잔고.가동_리밸A = false;
                                    break;

                                case "리밸_B":
                                    trading_item _rebalance_B = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_rebalance_repeat_time_B);
                                    Form1.Trading_Item_List.Add(_rebalance_B);
                                    Form1.TT_rebalance_time_B = Properties.Settings.Default.MT_rebalance_repeat_time_B;
                                    잔고.가동_리밸B = false;
                                    break;
                                case "리밸_C":
                                    trading_item _rebalance_C = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_rebalance_repeat_time_C);
                                    Form1.Trading_Item_List.Add(_rebalance_C);
                                    Form1.TT_rebalance_time_C = Properties.Settings.Default.MT_rebalance_repeat_time_C;
                                    잔고.가동_리밸C = false;
                                    break;
                                case "리밸_D":
                                    trading_item _rebalance_D = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_rebalance_repeat_time_D);
                                    Form1.Trading_Item_List.Add(_rebalance_D);
                                    Form1.TT_rebalance_time_D = Properties.Settings.Default.MT_rebalance_repeat_time_D;
                                    잔고.가동_리밸D = false;
                                    break;
                                case "리밸_E":
                                    trading_item _rebalance_E = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_rebalance_repeat_time_E);
                                    Form1.Trading_Item_List.Add(_rebalance_E);
                                    Form1.TT_rebalance_time_E = Properties.Settings.Default.MT_rebalance_repeat_time_E;
                                    잔고.가동_리밸E = false;
                                    break;
                                case "리밸_F":
                                    trading_item _rebalance_F = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_rebalance_repeat_time_F);
                                    Form1.Trading_Item_List.Add(_rebalance_F);
                                    Form1.TT_rebalance_time_F = Properties.Settings.Default.MT_rebalance_repeat_time_F;
                                    잔고.가동_리밸F = false;
                                    break;
                                case "리밸_G":
                                    trading_item _rebalance_G = new trading_item(잔고.종목코드, location, Properties.Settings.Default.MT_rebalance_repeat_time_G);
                                    Form1.Trading_Item_List.Add(_rebalance_G);
                                    Form1.TT_rebalance_time_G = Properties.Settings.Default.MT_rebalance_repeat_time_G;
                                    잔고.가동_리밸G = false;
                                    break;
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
