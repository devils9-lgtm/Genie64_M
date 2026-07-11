using System;
using System.Collections.Generic;
using System.Linq;

namespace 지니64
{
    class Tab_Special
    {

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        /////////////////////            그룹지정 매매             ////////////////
        public static void 그룹지정매매(Stockbalance 잔고)
        {
            if (Form1.재시작) return;

            if (잔고.매매가능 )
            {

                int CBB_In_group = GenieConfig.CBB_In_group_A;
                int CBB_group_In_won = GenieConfig.CBB_group_In_won_A;
                double TB_group_In_won = GenieConfig.TB_group_In_won_A;
                int CBB_Out_group = GenieConfig.CBB_Out_group_A;

                int In_Group = GenieConfig.CBB_In_group_choice_A;
                int Out_Group = GenieConfig.CBB_Out_group_choice_A;
                double 매입금 = 0.01;
                double In_수익or예상금 = 잔고.수익률;
                double Out_수익or예상금 = 잔고.수익률;
                double ratio_In = GenieConfig.TB_In_group_ratio_A;
                bool updown_In = GenieConfig.CB_group_In_updown_A;
                double ratio_Out = GenieConfig.TB_Out_group_ratio_A;
                bool updown_Out = GenieConfig.CB_group_Out_updown_A;
                bool In_work = GenieConfig.CB_Group_In_work_A;
                bool Out_work = GenieConfig.CB_Group_Out_work_A;

                bool In_trading = GenieConfig.CB_Group_In_trading_A;
                double In_Value = GenieConfig.TB_group_In_value_A;
                int In_jumun = GenieConfig.CBB_group_In_jumun_A;
                int In_Time = GenieConfig.MTB_group_In_CanselTime_A;
                int In_repeat = GenieConfig.MTB_group_In_repeat_A;
                double In_ratio = GenieConfig.TB_group_In_ratio_A;
                int In_choice = GenieConfig.CBB_group_In_choice_A;

                bool Out_trading = GenieConfig.CB_Group_Out_trading_A;
                double Out_Value = GenieConfig.TB_group_Out_value_A;
                int Out_jumun = GenieConfig.CBB_group_out_jumun_A;
                int Out_Time = GenieConfig.MTB_group_out_CanselTime_A;
                int Out_repeat = GenieConfig.MTB_group_out_repeat_A;
                double Out_ratio = GenieConfig.TB_group_out_ratio_A;
                int Out_choice = GenieConfig.CBB_group_out_choice_A;
                bool 그룹동작 = 잔고.그룹지정_A;
                string location = "그룹지정_A";
                bool IN체크박스 = GenieConfig.CB_Group_In_work_A;
                bool OUT체크박스 = GenieConfig.CB_Group_Out_work_A;

                if (GenieConfig.CBB_In_group_A > 0)
                {
                    if (그룹동작) Run();
                }

                if (GenieConfig.CBB_In_group_B > 0)
                {
                    CBB_In_group = GenieConfig.CBB_In_group_B;
                    CBB_group_In_won = GenieConfig.CBB_group_In_won_B;
                    TB_group_In_won = GenieConfig.TB_group_In_won_B;
                    CBB_Out_group = GenieConfig.CBB_Out_group_B;

                    In_Group = GenieConfig.CBB_In_group_choice_B;
                    Out_Group = GenieConfig.CBB_Out_group_choice_B;

                    ratio_In = GenieConfig.TB_In_group_ratio_B;
                    updown_In = GenieConfig.CB_group_In_updown_B;
                    ratio_Out = GenieConfig.TB_Out_group_ratio_B;
                    updown_Out = GenieConfig.CB_group_Out_updown_B;

                    In_work = GenieConfig.CB_Group_In_work_B;
                    Out_work = GenieConfig.CB_Group_Out_work_B;

                    In_trading = GenieConfig.CB_Group_In_trading_B;
                    In_Value = GenieConfig.TB_group_In_value_B;
                    In_jumun = GenieConfig.CBB_group_In_jumun_B;
                    In_Time = GenieConfig.MTB_group_In_CanselTime_B;
                    In_repeat = GenieConfig.MTB_group_In_repeat_B;
                    In_ratio = GenieConfig.TB_group_In_ratio_B;
                    In_choice = GenieConfig.CBB_group_In_choice_B;

                    Out_trading = GenieConfig.CB_Group_Out_trading_B;
                    Out_Value = GenieConfig.TB_group_Out_value_B;
                    Out_jumun = GenieConfig.CBB_group_out_jumun_B;
                    Out_Time = GenieConfig.MTB_group_out_CanselTime_B;
                    Out_repeat = GenieConfig.MTB_group_out_repeat_B;
                    Out_ratio = GenieConfig.TB_group_out_ratio_B;
                    Out_choice = GenieConfig.CBB_group_out_choice_B;
                    location = "그룹지정_B";
                    IN체크박스 = GenieConfig.CB_Group_In_work_B;
                    OUT체크박스 = GenieConfig.CB_Group_Out_work_B;

                    그룹동작 = 잔고.그룹지정_B;
                    if (그룹동작) Run();
                }

                if (GenieConfig.CBB_In_group_C > 0)
                {
                    CBB_In_group = GenieConfig.CBB_In_group_C;
                    CBB_group_In_won = GenieConfig.CBB_group_In_won_C;
                    TB_group_In_won = GenieConfig.TB_group_In_won_C;
                    CBB_Out_group = GenieConfig.CBB_Out_group_C;

                    In_Group = GenieConfig.CBB_In_group_choice_C;
                    Out_Group = GenieConfig.CBB_Out_group_choice_C;
                    ratio_In = GenieConfig.TB_In_group_ratio_C;
                    updown_In = GenieConfig.CB_group_In_updown_C;
                    ratio_Out = GenieConfig.TB_Out_group_ratio_C;
                    updown_Out = GenieConfig.CB_group_Out_updown_C;

                    In_work = GenieConfig.CB_Group_In_work_C;
                    Out_work = GenieConfig.CB_Group_Out_work_C;

                    In_trading = GenieConfig.CB_Group_In_trading_C;
                    In_Value = GenieConfig.TB_group_In_value_C;
                    In_jumun = GenieConfig.CBB_group_In_jumun_C;
                    In_Time = GenieConfig.MTB_group_In_CanselTime_C;
                    In_repeat = GenieConfig.MTB_group_In_repeat_C;
                    In_ratio = GenieConfig.TB_group_In_ratio_C;
                    In_choice = GenieConfig.CBB_group_In_choice_C;

                    Out_trading = GenieConfig.CB_Group_Out_trading_C;
                    Out_Value = GenieConfig.TB_group_Out_value_C;
                    Out_jumun = GenieConfig.CBB_group_out_jumun_C;
                    Out_Time = GenieConfig.MTB_group_out_CanselTime_C;
                    Out_repeat = GenieConfig.MTB_group_out_repeat_C;
                    Out_ratio = GenieConfig.TB_group_out_ratio_C;
                    Out_choice = GenieConfig.CBB_group_out_choice_C;
                    location = "그룹지정_C";
                    IN체크박스 = GenieConfig.CB_Group_In_work_C;
                    OUT체크박스 = GenieConfig.CB_Group_Out_work_C;

                    그룹동작 = 잔고.그룹지정_C;
                    if (그룹동작) Run();
                }

                if (GenieConfig.CBB_In_group_D > 0)
                {
                    CBB_In_group = GenieConfig.CBB_In_group_D;
                    CBB_group_In_won = GenieConfig.CBB_group_In_won_D;
                    TB_group_In_won = GenieConfig.TB_group_In_won_D;
                    CBB_Out_group = GenieConfig.CBB_Out_group_D;

                    In_Group = GenieConfig.CBB_In_group_choice_D;
                    Out_Group = GenieConfig.CBB_Out_group_choice_D;
                    ratio_In = GenieConfig.TB_In_group_ratio_D;
                    updown_In = GenieConfig.CB_group_In_updown_D;
                    ratio_Out = GenieConfig.TB_Out_group_ratio_D;
                    updown_Out = GenieConfig.CB_group_Out_updown_D;

                    In_work = GenieConfig.CB_Group_In_work_D;
                    Out_work = GenieConfig.CB_Group_Out_work_D;

                    In_trading = GenieConfig.CB_Group_In_trading_D;
                    In_Value = GenieConfig.TB_group_In_value_D;
                    In_jumun = GenieConfig.CBB_group_In_jumun_D;
                    In_Time = GenieConfig.MTB_group_In_CanselTime_D;
                    In_repeat = GenieConfig.MTB_group_In_repeat_D;
                    In_ratio = GenieConfig.TB_group_In_ratio_D;
                    In_choice = GenieConfig.CBB_group_In_choice_D;

                    Out_trading = GenieConfig.CB_Group_Out_trading_D;
                    Out_Value = GenieConfig.TB_group_Out_value_D;
                    Out_jumun = GenieConfig.CBB_group_out_jumun_D;
                    Out_Time = GenieConfig.MTB_group_out_CanselTime_D;
                    Out_repeat = GenieConfig.MTB_group_out_repeat_D;
                    Out_ratio = GenieConfig.TB_group_out_ratio_D;
                    Out_choice = GenieConfig.CBB_group_out_choice_D;
                    location = "그룹지정_D";
                    IN체크박스 = GenieConfig.CB_Group_In_work_D;
                    OUT체크박스 = GenieConfig.CB_Group_Out_work_D;

                    그룹동작 = 잔고.그룹지정_D;
                    if (그룹동작) Run();
                }


                void Run()
                {
                    if (GenieConfig.CB_group_기준금)
                    {
                        double 매수기준금 = GenieConfig.MT_buying_standard;

                        if (CBB_In_group == 2) // 예상수익금
                        {
                            ratio_In = ratio_In / (double)100 * (double)매수기준금;
                            In_수익or예상금 = 잔고.예상손익;
                        }

                        if (CBB_group_In_won == 1)
                            매입금 = TB_group_In_won / (double)100 * (double)매수기준금;

                        if (CBB_Out_group > 0) // 매입금
                        {
                            ratio_Out = ratio_Out / (double)100 * (double)매수기준금;
                        }
                    }
                    else
                    {
                        if (CBB_In_group == 2) // 예상수익금
                        {
                            ratio_In *= 10000;
                            In_수익or예상금 = 잔고.예상손익;
                        }

                        if (CBB_group_In_won == 1)
                            매입금 = TB_group_In_won * 10000;

                        if (CBB_Out_group > 0) // 매입금
                        {
                            ratio_Out *= 10000;
                        }
                    }

                    if (CBB_Out_group == 1) // 매입금
                    {
                        Out_수익or예상금 = 잔고.매입금액;
                    }
                    else if (CBB_Out_group == 2) // 예상수익금
                    {
                        Out_수익or예상금 = 잔고.예상손익;
                    }

                    if (잔고.매입금액 > 매입금 || 잔고.매매그룹 == In_Group)
                    {
                        그룹지정(잔고, In_수익or예상금, Out_수익or예상금, 잔고.그룹지정_A, In_Group, Out_Group, In_work, ratio_In, updown_In, Out_work, ratio_Out, updown_Out,
                            In_trading, In_Value, In_jumun, In_Time, In_repeat, In_ratio, In_choice, Out_trading, Out_Value, Out_jumun, Out_Time, Out_repeat, Out_ratio, Out_choice, location, IN체크박스, OUT체크박스);
                    }
                }
            }
        }

        public static void 그룹지정(Stockbalance 잔고, double In_수익or예상금, double Out_수익or예상금, bool In_check, int In_Group, int Out_Group, bool In_work, double ratio_In, bool updown_In, bool Out_work, double ratio_Out, bool updown_Out,
            bool In_trading, double In_주문값, int In_매수매도, int In_CanselTime, int In_repeat, double In_비중, int In_비중선택, bool Out_trading, double Out_주문값, int Out_매수매도, int Out_CanselTime, int Out_repeat, double Out_비중, int Out_비중선택, string location, bool IN체크박스, bool OUT체크박스)
        {
            string condition;
            int 매수매도 = 2;

            if (In_check)
            {
                if (잔고.매매그룹 != In_Group)
                {
                    if (updown_In) // 이상 
                    {
                        if (ratio_In <= In_수익or예상금)
                        {
                            진입동작();
                        }
                    }
                    else // 이하 
                    {
                        if (ratio_In >= In_수익or예상금)
                        {
                            진입동작();
                        }
                    }
                }
            }

            if (잔고.매매그룹 == In_Group && 잔고.매매그룹 != Out_Group) // 해제
            {
                if (updown_Out)  // 이상 
                {
                    if (ratio_Out <= Out_수익or예상금)
                    {
                        해제동작();
                    }
                }
                else
                {
                    if (ratio_Out >= Out_수익or예상금)
                    {
                        해제동작();
                    }
                }
            }

            void 진입동작()
            {
                if (Out_work)
                {
                    if (updown_Out)  // 이상 
                    {
                        if (ratio_Out > Out_수익or예상금)
                        {
                            Run();
                        }
                        else
                        {
                            False_message();
                        }
                    }
                    else
                    {
                        if (ratio_Out < Out_수익or예상금)
                        {
                            Run();
                        }
                        else
                        {
                            False_message();
                        }
                    }

                    void False_message()
                    {
                        if (IN체크박스)
                        {
                            Form1.AutoClosingAlram("종목: " + 잔고.종목명 + "  " + location + "의 매도 기준값이 충돌 하여 Group IN 매매가 정지 됩니다. 매도 기준값 설정을 수정후 지니64를 재시작 하면 정상동작 합니다.", "[설정충돌]", 30, "에러");

                            Log.동작기록("[설정충돌] 매도 기준값 설정을 수정후 지니64를 재시작 하면 정상동작 합니다.");
                            Log.동작기록("[설정충돌] " + 잔고.종목명 + " " + location + "의 매도 기준값이 충돌 하여 Group IN 매매가 정지 됩니다.");

                            switch (location)
                            {
                                case "그룹지정_A":
                                    GenieConfig.CB_Group_In_work_A = false;
                                    break;
                                case "그룹지정_B":
                                    GenieConfig.CB_Group_In_work_B = false;
                                    break;
                                case "그룹지정_C":
                                    GenieConfig.CB_Group_In_work_C = false;
                                    break;
                                case "그룹지정_D":
                                    GenieConfig.CB_Group_In_work_D = false;
                                    break;
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
                    if (In_work) //사용유무
                    {
                        condition = location + "_IN";

                        if (In_trading) // 매수
                        {
                            if (Helper.NXT_매수매도_금지(true)) return;
                            매수매도 = 1;

                            if (!Form1.추가매수정지 && Form1.form1.매수_ON && 잔고.매수제한)
                            {
                                if (Method.매입비추매제한(잔고.보유비중) && !잔고.추매정지)
                                {
                                    if (Jisu_linkage.업종별지수연동("추매", Form1.Market_Item_List[잔고.종목코드]))
                                    {
                                        if (GenieConfig.CB_계좌매입비_매수제한)// 매입비
                                        {
                                            if (!Method.매입비매수제한(GenieConfig.CBB_계좌매입비_제한선택).Contains("추매"))
                                            {
                                                ExecuteTrade.잔고주문_오더(잔고, condition, 매수매도, In_비중, In_비중선택, In_주문값, In_매수매도, In_CanselTime, 0, In_repeat, "", location, 0, false, 0);

                                                잔고.매매그룹 = In_Group;
                                            }
                                        }
                                        else
                                        {
                                            ExecuteTrade.잔고주문_오더(잔고, condition, 매수매도, In_비중, In_비중선택, In_주문값, In_매수매도, In_CanselTime, 0, In_repeat, "", location, 0, false, 0);
                                            잔고.매매그룹 = In_Group;
                                        }
                                    }
                                }
                            }
                        }
                        else // 매도
                        {
                            if (Helper.NXT_매수매도_금지(false))
                            {
                                if (잔고.수익률 < 0.3) return;
                            }

                            if (Form1.form1.매도_ON && !잔고.매도정지)
                            {
                                ExecuteTrade.잔고주문_오더(잔고, condition, 매수매도, In_비중, In_비중선택, In_주문값, In_매수매도, In_CanselTime, 0, In_repeat, "", location, 0, false, 0);
                                잔고.매매그룹 = In_Group;
                            }
                        }
                    }
                    else
                    {
                        잔고.매매그룹 = In_Group;
                    }
                }
            }

            void 해제동작()
            {

                if (In_work)
                {
                    if (updown_In)  // 이상 
                    {
                        if (ratio_In > In_수익or예상금)
                        {
                            Run();
                        }
                        else
                        {
                            False_message();
                        }
                    }
                    else
                    {
                        if (ratio_In < In_수익or예상금)
                        {
                            Run();
                        }
                        else
                        {
                            False_message();
                        }
                    }

                    void False_message()
                    {
                        if (OUT체크박스)
                        {
                            // 1. 알림창 출력 (가독성 확보 및 띄어쓰기 교정)
                            Helper.알림창_멀티("설정충돌", $"종목: {잔고.종목명} {location}의 매수 기준값이 충돌 하여 Group OUT 매매가 정지 됩니다. \n\n매수 기준값 설정을 수정 후 지니64를 재시작 하면 정상 동작 합니다.", 30, false);

                            // 2. 동작 기록 로그
                            Log.동작기록("[설정충돌] 매수 기준값 설정을 수정 후 지니64를 재시작 하면 정상 동작 합니다.");
                            Log.동작기록($"[설정충돌] {잔고.종목명} {location}의 매수 기준값이 충돌 하여 Group OUT 매매가 정지 됩니다.");

                            // 3. 에러 기록 로그
                            Log.에러기록($"[설정충돌] {잔고.종목명} {location}의 매수 기준값이 충돌 하여 Group OUT 매매가 정지 됩니다.");

                            switch (location)
                            {
                                case "그룹지정_A":
                                    GenieConfig.CB_Group_Out_work_A = false;
                                    break;
                                case "그룹지정_B":
                                    GenieConfig.CB_Group_Out_work_B = false;
                                    break;
                                case "그룹지정_C":
                                    GenieConfig.CB_Group_Out_work_C = false;
                                    break;
                                case "그룹지정_D":
                                    GenieConfig.CB_Group_Out_work_D = false;
                                    break;
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
                    if (Out_work)   //사용유무
                    {
                        condition = location + "_OUT";
                        if (Out_trading) // 매수
                        {
                            if (Helper.NXT_매수매도_금지(true)) return;
                            매수매도 = 1;

                            if (Form1.form1.매수_ON && 잔고.매수제한 && !Form1.추가매수정지)
                            {
                                if (Method.매입비추매제한(잔고.보유비중) && !잔고.추매정지)
                                {
                                    if (Jisu_linkage.업종별지수연동("추매", Form1.Market_Item_List[잔고.종목코드]))
                                    {
                                        if (GenieConfig.CB_계좌매입비_매수제한)// 매입비
                                        {
                                            if (!Method.매입비매수제한(GenieConfig.CBB_계좌매입비_제한선택).Contains("추매"))
                                            {
                                                ExecuteTrade.잔고주문_오더(잔고, condition, 매수매도, Out_비중, Out_비중선택, Out_주문값, Out_매수매도, Out_CanselTime, 0, Out_repeat, "", location, 0, false, 0);
                                                잔고.매매그룹 = Out_Group;
                                                switch_false();
                                            }
                                        }
                                        else
                                        {
                                            ExecuteTrade.잔고주문_오더(잔고, condition, 매수매도, Out_비중, Out_비중선택, Out_주문값, Out_매수매도, Out_CanselTime, 0, Out_repeat, "", location, 0, false, 0);
                                            잔고.매매그룹 = Out_Group;
                                            switch_false();
                                        }
                                    }
                                }
                            }
                        }
                        else // 매도
                        {
                            if (Helper.NXT_매수매도_금지(false))
                            {
                                if (잔고.수익률 < 0.3) return;
                            }

                            if (Form1.form1.RB_sell_run.Checked && !잔고.매도정지)
                            {
                                ExecuteTrade.잔고주문_오더(잔고, condition, 매수매도, Out_비중, Out_비중선택, Out_주문값, Out_매수매도, Out_CanselTime, 0, Out_repeat, "", location, 0, false, 0);
                                잔고.매매그룹 = Out_Group;
                                switch_false();
                            }
                        }
                    }
                    else
                    {
                        잔고.매매그룹 = Out_Group;
                        switch_false();
                    }
                }
            }

            void switch_false()
            {
                switch (location)
                {
                    case "그룹지정_A":
                        잔고.그룹지정_A = false;
                        break;
                    case "그룹지정_B":
                        잔고.그룹지정_B = false;
                        break;
                    case "그룹지정_C":
                        잔고.그룹지정_C = false;
                        break;
                    case "그룹지정_D":
                        잔고.그룹지정_D = false;
                        break;
                }
            }



        }
        /////////////////////            그룹지정 매매             ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ///


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        /////////////////////           매매일기준 매매            ////////////////

        public static void 매매일_기준거래(Stockbalance 잔고, string 매매일_주문시간)
        {
            if (Helper.NXT_매수매도_금지(false))
            {
                if (잔고.수익률 < 0.3) return;
            }

            double 매수기준금 = GenieConfig.MT_buying_standard;

            int 매수n매도 = GenieConfig.CBB_매매기간_trading_A;
            int 거래일 = GenieConfig.MTB_매매기간_기간_A;
            double 기준금 = GenieConfig.TB_매매기간_기준_A * 10000;
            if (GenieConfig.CB_매매기간_기준금) 기준금 = GenieConfig.TB_매매기간_기준_A / 100 * 매수기준금;
            double 수익률 = GenieConfig.TB_매매기간_기준_A;
            int 매매유형 = GenieConfig.CBB_매매기간_기준_A;
            double 비중 = GenieConfig.TB_매매기간_ratio_A;
            int 비중단위 = GenieConfig.CBB_매매기간_choice_A;
            double 주문값 = GenieConfig.TB_매매기간_value_A;
            int 매수매도 = GenieConfig.CBB_매매기간_Jumun_A;
            int 주문시간 = GenieConfig.CBB_매매기간_주문시간_A;
            int 취소시간 = GenieConfig.TB_매매기간_취소시간_A;
            bool 강제매도 = GenieConfig.CB_매매기간_강제매도_A;
            bool 수익보전 = GenieConfig.CB_매매기간_수익보전_A;
            string 그룹 = "Day_A";
            string location = "_A";

            int TS_high = 잔고.매매기간_TS_high_A;
            bool TS = GenieConfig.CB_매매기간_TS_A;
            double TS_down = GenieConfig.TB_매매기간_TS_down_A;

            MAPeriod mma = Form1.Min_ma_list[잔고.종목코드];
            MAPeriod dma = Form1.Day_ma_list[잔고.종목코드];
            double TS_min_ma = mma.매매기간_TS_MAValue_A;
            double TS_day_ma = dma.매매기간_TS_MAValue_A;
            int CBB_TS_mma = GenieConfig.CBB_매매기간_TS_MinMAPeriod_A;
            int CBB_TS_dma = GenieConfig.CBB_매매기간_TS_DayMAPeriod_A;

            if (매수n매도 > 0 && int.Parse(잔고.거래일) >= 거래일)
            {
                주문전달();
            }

            if (GenieConfig.CBB_매매기간_trading_B > 0)
            {
                매수n매도 = GenieConfig.CBB_매매기간_trading_B;
                if (매수n매도 > 0)
                {
                    거래일 = GenieConfig.MTB_매매기간_기간_B;
                    if (int.Parse(잔고.거래일) >= 거래일)
                    {
                        기준금 = GenieConfig.TB_매매기간_기준_B * 10000;
                        if (GenieConfig.CB_매매기간_기준금) 기준금 = GenieConfig.TB_매매기간_기준_B / 100 * 매수기준금;
                        수익률 = GenieConfig.TB_매매기간_기준_B;
                        매매유형 = GenieConfig.CBB_매매기간_기준_B;
                        비중 = GenieConfig.TB_매매기간_ratio_B;
                        비중단위 = GenieConfig.CBB_매매기간_choice_B;
                        주문값 = GenieConfig.TB_매매기간_value_B;
                        매수매도 = GenieConfig.CBB_매매기간_Jumun_B;
                        주문시간 = GenieConfig.CBB_매매기간_주문시간_B;
                        취소시간 = GenieConfig.TB_매매기간_취소시간_B;
                        강제매도 = GenieConfig.CB_매매기간_강제매도_B;
                        수익보전 = GenieConfig.CB_매매기간_수익보전_B;
                        그룹 = "Day_B";
                        location = "_B";

                        TS_high = 잔고.매매기간_TS_high_B;
                        TS = GenieConfig.CB_매매기간_TS_B;
                        TS_down = GenieConfig.TB_매매기간_TS_down_B;
                        TS_min_ma = mma.매매기간_TS_MAValue_B;
                        TS_day_ma = dma.매매기간_TS_MAValue_B;
                        CBB_TS_mma = GenieConfig.CBB_매매기간_TS_MinMAPeriod_B;
                        CBB_TS_dma = GenieConfig.CBB_매매기간_TS_DayMAPeriod_B;

                        주문전달();
                    }
                }
            }

            if (GenieConfig.CBB_매매기간_trading_C > 0)
            {
                매수n매도 = GenieConfig.CBB_매매기간_trading_C;
                if (매수n매도 > 0)
                {
                    거래일 = GenieConfig.MTB_매매기간_기간_C;
                    if (int.Parse(잔고.거래일) >= 거래일)
                    {
                        기준금 = GenieConfig.TB_매매기간_기준_C * 10000;
                        if (GenieConfig.CB_매매기간_기준금) 기준금 = GenieConfig.TB_매매기간_기준_C / 100 * 매수기준금;
                        수익률 = GenieConfig.TB_매매기간_기준_C;
                        매매유형 = GenieConfig.CBB_매매기간_기준_C;
                        비중 = GenieConfig.TB_매매기간_ratio_C;
                        비중단위 = GenieConfig.CBB_매매기간_choice_C;
                        주문값 = GenieConfig.TB_매매기간_value_C;
                        매수매도 = GenieConfig.CBB_매매기간_Jumun_C;
                        주문시간 = GenieConfig.CBB_매매기간_주문시간_C;
                        취소시간 = GenieConfig.TB_매매기간_취소시간_C;
                        강제매도 = GenieConfig.CB_매매기간_강제매도_C;
                        수익보전 = GenieConfig.CB_매매기간_수익보전_C;
                        그룹 = "Day_C";
                        location = "_C";

                        TS_high = 잔고.매매기간_TS_high_C;
                        TS = GenieConfig.CB_매매기간_TS_C;
                        TS_down = GenieConfig.TB_매매기간_TS_down_C;
                        TS_min_ma = mma.매매기간_TS_MAValue_C;
                        TS_day_ma = dma.매매기간_TS_MAValue_C;
                        CBB_TS_mma = GenieConfig.CBB_매매기간_TS_MinMAPeriod_C;
                        CBB_TS_dma = GenieConfig.CBB_매매기간_TS_DayMAPeriod_C;

                        주문전달();
                    }
                }
            }

            if (GenieConfig.CBB_매매기간_trading_D > 0)
            {
                매수n매도 = GenieConfig.CBB_매매기간_trading_D;
                if (매수n매도 > 0)
                {
                    거래일 = GenieConfig.MTB_매매기간_기간_D;
                    if (int.Parse(잔고.거래일) >= 거래일)
                    {
                        기준금 = GenieConfig.TB_매매기간_기준_D * 10000;
                        if (GenieConfig.CB_매매기간_기준금) 기준금 = GenieConfig.TB_매매기간_기준_D / 100 * 매수기준금;
                        수익률 = GenieConfig.TB_매매기간_기준_D;
                        매매유형 = GenieConfig.CBB_매매기간_기준_D;
                        비중 = GenieConfig.TB_매매기간_ratio_D;
                        비중단위 = GenieConfig.CBB_매매기간_choice_D;
                        주문값 = GenieConfig.TB_매매기간_value_D;
                        매수매도 = GenieConfig.CBB_매매기간_Jumun_D;
                        주문시간 = GenieConfig.CBB_매매기간_주문시간_D;
                        취소시간 = GenieConfig.TB_매매기간_취소시간_D;
                        강제매도 = GenieConfig.CB_매매기간_강제매도_D;
                        수익보전 = GenieConfig.CB_매매기간_수익보전_D;
                        그룹 = "Day_D";
                        location = "_D";

                        TS_high = 잔고.매매기간_TS_high_D;
                        TS = GenieConfig.CB_매매기간_TS_D;
                        TS_down = GenieConfig.TB_매매기간_TS_down_D;
                        TS_min_ma = mma.매매기간_TS_MAValue_D;
                        TS_day_ma = dma.매매기간_TS_MAValue_D;
                        CBB_TS_mma = GenieConfig.CBB_매매기간_TS_MinMAPeriod_D;
                        CBB_TS_dma = GenieConfig.CBB_매매기간_TS_DayMAPeriod_D;

                        주문전달();
                    }
                }
            }

            if (GenieConfig.CBB_매매기간_trading_E > 0)
            {
                매수n매도 = GenieConfig.CBB_매매기간_trading_E;
                if (매수n매도 > 0)
                {
                    거래일 = GenieConfig.MTB_매매기간_기간_E;
                    if (int.Parse(잔고.거래일) >= 거래일)
                    {
                        기준금 = GenieConfig.TB_매매기간_기준_E * 10000;
                        if (GenieConfig.CB_매매기간_기준금) 기준금 = GenieConfig.TB_매매기간_기준_E / 100 * 매수기준금;
                        수익률 = GenieConfig.TB_매매기간_기준_E;
                        매매유형 = GenieConfig.CBB_매매기간_기준_E;
                        비중 = GenieConfig.TB_매매기간_ratio_E;
                        비중단위 = GenieConfig.CBB_매매기간_choice_E;
                        주문값 = GenieConfig.TB_매매기간_value_E;
                        매수매도 = GenieConfig.CBB_매매기간_Jumun_E;
                        주문시간 = GenieConfig.CBB_매매기간_주문시간_E;
                        취소시간 = GenieConfig.TB_매매기간_취소시간_E;
                        강제매도 = GenieConfig.CB_매매기간_강제매도_E;
                        수익보전 = GenieConfig.CB_매매기간_수익보전_E;
                        그룹 = "Day_E";
                        location = "_E";

                        TS_high = 잔고.매매기간_TS_high_E;
                        TS = GenieConfig.CB_매매기간_TS_E;
                        TS_down = GenieConfig.TB_매매기간_TS_down_E;
                        TS_min_ma = mma.매매기간_TS_MAValue_E;
                        TS_day_ma = dma.매매기간_TS_MAValue_E;
                        CBB_TS_mma = GenieConfig.CBB_매매기간_TS_MinMAPeriod_E;
                        CBB_TS_dma = GenieConfig.CBB_매매기간_TS_DayMAPeriod_E;

                        주문전달();
                    }
                }
            }


            if (GenieConfig.CBB_매매기간_trading_F > 0)
            {
                매수n매도 = GenieConfig.CBB_매매기간_trading_F;
                if (매수n매도 > 0)
                {
                    거래일 = GenieConfig.MTB_매매기간_기간_F;
                    if (int.Parse(잔고.거래일) >= 거래일)
                    {
                        기준금 = GenieConfig.TB_매매기간_기준_F * 10000;
                        if (GenieConfig.CB_매매기간_기준금) 기준금 = GenieConfig.TB_매매기간_기준_F / 100 * 매수기준금;
                        수익률 = GenieConfig.TB_매매기간_기준_F;
                        매매유형 = GenieConfig.CBB_매매기간_기준_F;
                        비중 = GenieConfig.TB_매매기간_ratio_F;
                        비중단위 = GenieConfig.CBB_매매기간_choice_F;
                        주문값 = GenieConfig.TB_매매기간_value_F;
                        매수매도 = GenieConfig.CBB_매매기간_Jumun_F;
                        주문시간 = GenieConfig.CBB_매매기간_주문시간_F;
                        취소시간 = GenieConfig.TB_매매기간_취소시간_F;
                        강제매도 = GenieConfig.CB_매매기간_강제매도_F;
                        수익보전 = GenieConfig.CB_매매기간_수익보전_F;
                        그룹 = "Day_F";
                        location = "_F";

                        TS_high = 잔고.매매기간_TS_high_F;
                        TS = GenieConfig.CB_매매기간_TS_F;
                        TS_down = GenieConfig.TB_매매기간_TS_down_F;
                        TS_min_ma = mma.매매기간_TS_MAValue_F;
                        TS_day_ma = dma.매매기간_TS_MAValue_F;
                        CBB_TS_mma = GenieConfig.CBB_매매기간_TS_MinMAPeriod_F;
                        CBB_TS_dma = GenieConfig.CBB_매매기간_TS_DayMAPeriod_F;

                        주문전달();
                    }
                }
            }


            void 주문전달()
            {
                if (Form1.server_알림.Contains("마켓") || Form1.server_알림.Contains("동시"))
                {
                    if (매매일_주문시간.Equals("오전") && 주문시간 == 0) 전달();
                    if (매매일_주문시간.Equals("오후") && 주문시간 == 1) 전달();
                    if (매매일_주문시간.Equals("리얼") && 주문시간 == 2)
                    {
                        if (GET.총주문가능수량(잔고) > 0 && Method.매매진입_가능여부(잔고.종목코드, "매매기간_" + GET.매수매도str(매수n매도) + location))
                        {
                            전달();
                        }
                    }
                }

                void 전달()
                {
                    if (!잔고.종목상태.Contains("거래정지") && !잔고.종목상태.Contains("동시호가"))
                    {
                        if (TS)
                        {

                            //double 주문비1 = ((double)잔고.현재가 - (double)TS_high) / (double)TS_high * (double)100;
                            //Console_print("TS_high :[" + TS_high + "] 주문비 :[" + 주문비1 + "] (TS_down >= 주문비) : " + (TS_down >= 주문비1));

                            if (TS_high == 0)
                            {
                                // Console_print("############  잔고청산 2222 + " + " 잔고.거래일: " + int.Parse(잔고.거래일) + " 거래일" + 거래일 + " 잔고: " + 잔고.종목명);
                                Check();
                            }
                            else
                            {

                                if (TS_high < 잔고.현재가)
                                {
                                    switch (location)
                                    {
                                        case "_A": 잔고.매매기간_TS_high_A = 잔고.현재가; break;
                                        case "_B": 잔고.매매기간_TS_high_B = 잔고.현재가; break;
                                        case "_C": 잔고.매매기간_TS_high_C = 잔고.현재가; break;
                                        case "_D": 잔고.매매기간_TS_high_D = 잔고.현재가; break;
                                        case "_E": 잔고.매매기간_TS_high_E = 잔고.현재가; break;
                                        case "_F": 잔고.매매기간_TS_high_F = 잔고.현재가; break;
                                    }
                                }
                                else
                                {
                                    double 주문비 = ((double)잔고.현재가 - (double)TS_high) / (double)TS_high * (double)100;
                                    if (TS_down >= 주문비)
                                    {
                                        if (MA.Get_TS_이평(잔고, CBB_TS_mma, TS_min_ma) && MA.Get_TS_이평(잔고, CBB_TS_dma, TS_day_ma))
                                        {
                                            if (수익보전)
                                            {
                                                if (잔고.수익률 > 0 && 잔고.예상손익 > 0) Run(" TS");
                                            }
                                            else
                                            {
                                                Run(" TS");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Check();
                        }

                        void Check()
                        {
                            bool 주문 = false;

                            if (매매유형 == 0 && 수익률 <= 잔고.수익률) 주문 = true;   // 수익률 이상
                            if (매매유형 == 1 && 수익률 >= 잔고.수익률) 주문 = true;   // 수익률 이하
                            if (매매유형 == 2 && 기준금 <= 잔고.매입금액) 주문 = true; // 매입금 이상
                            if (매매유형 == 3 && 기준금 >= 잔고.매입금액) 주문 = true; // 매입금 이하
                            if (매매유형 == 4 && 기준금 <= 잔고.예상손익) 주문 = true; // 예상손익 이상
                            if (매매유형 == 5 && 기준금 >= 잔고.예상손익) 주문 = true; // 예상손익 이하
                            if (수익보전)
                            {
                                if (잔고.수익률 < 0 || 잔고.예상손익 < 0) 주문 = false;
                            }

                            if (주문)
                            {
                                if (GET.익절그룹(그룹).Contains(GET.그룹변환(잔고.매매그룹)) && Tab_InterestGroup.관심그룹확인(그룹, 잔고.종목코드))
                                {
                                    if (매수n매도 == 1)
                                    {
                                        if (!Form1.추가매수정지 && Form1.form1.매수_ON)
                                        {
                                            bool 기존주문_존재여부 = Form1.JumunItem_List.Values.Any(개별주문 =>
                                                개별주문.종목코드 == 잔고.종목코드 && 개별주문.검색식.Contains("매매기간_"));

                                            // 결과: 기존주문_존재여부가 false일 때만(존재하지 않을 때만) 신규 주문을 진행합니다.
                                            if (!기존주문_존재여부)
                                            {
                                                ExecuteTrade.잔고주문_오더(잔고, "매매기간_" + GET.매수매도str(매수n매도) + location, 매수n매도, 비중, 비중단위, 주문값, 매수매도, 취소시간, 0, 0, "", "매매기간" + location, 0, false, 0);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Form1.form1.RB_sell_run.Checked && (!잔고.매도정지 || 강제매도))
                                        {
                                            if (TS)
                                            {
                                                if (TS_high == 0)
                                                {
                                                    switch (location)
                                                    {
                                                        case "_A": 잔고.매매기간_TS_high_A = 잔고.현재가; break;
                                                        case "_B": 잔고.매매기간_TS_high_B = 잔고.현재가; break;
                                                        case "_C": 잔고.매매기간_TS_high_C = 잔고.현재가; break;
                                                        case "_D": 잔고.매매기간_TS_high_D = 잔고.현재가; break;
                                                        case "_E": 잔고.매매기간_TS_high_E = 잔고.현재가; break;
                                                        case "_F": 잔고.매매기간_TS_high_F = 잔고.현재가; break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (매매일_주문시간.Equals("리얼"))
                                                {
                                                    Run("TS");
                                                }
                                                else
                                                {
                                                    if (Method.매매진입_가능여부(잔고.종목코드, "매매기간_" + GET.매수매도str(매수n매도) + location))
                                                    {
                                                        ExecuteTrade.잔고주문_오더(잔고, "매매기간_" + GET.매수매도str(매수n매도) + location, 매수n매도, 비중, 비중단위, 주문값, 매수매도, 취소시간, 0, 0, "", "매매기간" + location, 0, true, 0);
                                                    }
                                                }
                                            }


                                        }
                                    }
                                }
                            }
                        }
                    }

                    void Run(string _TS)
                    {
                        List<JumunItem> 취소대상_주문리스트 = Form1.JumunItem_List.Values
                            .Where(개별주문 => 개별주문.종목코드 == 잔고.종목코드 && !개별주문.검색식.Contains("매매기간_"))
                            .ToList();

                        if (취소대상_주문리스트.Count > 0)
                        {
                            잔고.매매가능 = false;

                            foreach (JumunItem 취소할주문 in 취소대상_주문리스트)
                            {
                                취소할주문.반복횟수 = 0;
                                취소할주문.취소시간 = 0;
                                취소할주문.취소timer = 0;
                                취소할주문.비고 = "매매기간_미체결일괄 '취소'";
                            }
                        }
                        else
                        {
                            잔고.매매가능 = true;
                            if (Method.매매진입_가능여부(잔고.종목코드, "매매기간_" + GET.매수매도str(매수n매도) + location + _TS))
                            {
                                ExecuteTrade.잔고주문_오더(잔고, "매매기간_" + GET.매수매도str(매수n매도) + location + _TS, 매수n매도, 비중, 비중단위, 주문값, 매수매도, 취소시간, 0, 0, "", "매매기간" + location, 0, true, 0);
                            }
                        }
                    }
                }
            }
        }

        /////////////////////           매매일기준 매매            ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
    }
}
