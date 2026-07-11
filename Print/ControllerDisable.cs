using System.Windows.Forms;

namespace 지니64
{
    public class ControllerDisable
    {
        public static void Form_1_Disable()
        {
            Form1.form1.TB_starttime.Enabled = false;
            Form1.form1.TB_stoptime.Enabled = false;

            if (GenieConfig.CB_기본매매변경)
            {
                Form1.form1.TB_setjango.Enabled = true;
                Form1.form1.CB_계좌매입비_매수제한.Enabled = true;
                Form1.form1.TB_계좌매입비_제한비중.Enabled = true;
                Form1.form1.CBB_계좌매입비_제한선택.Enabled = true;
                Form1.form1.TB_계좌매입비_현비중.Enabled = true;
                Form1.form1.CB_잔고매입비_추매제한.Enabled = true;
                Form1.form1.TB_잔고매입비_추매제한.Enabled = true;
                Form1.form1.CB_misu.Enabled = true;
                Form1.form1.CB_misu.Enabled = true;
                Form1.form1.CB_misu.Enabled = true;
                Form1.form1.MT_misu_time.Enabled = true;
                Form1.form1.Combo_misu.Enabled = true;
                Form1.form1.TB_misu_ratio.Enabled = true;
                Form1.form1.TB_misu_value.Enabled = true;
                Form1.form1.Combo_misu_jumnun.Enabled = true;
                Form1.form1.TB_misu_repeat_time.Enabled = true;
                Form1.form1.label_계좌매입비1.Enabled = true;
                Form1.form1.label_계좌매입비2.Enabled = true;
                Form1.form1.label_잔고매입비.Enabled = true;
                Form1.form1.label_misu.Enabled = true;
            }
            else
            {
                Form1.form1.TB_setjango.Enabled = false;
                Form1.form1.CB_계좌매입비_매수제한.Enabled = false;
                Form1.form1.TB_계좌매입비_제한비중.Enabled = false;
                Form1.form1.CBB_계좌매입비_제한선택.Enabled = false;
                Form1.form1.TB_계좌매입비_현비중.Enabled = false;
                Form1.form1.CB_잔고매입비_추매제한.Enabled = false;
                Form1.form1.TB_잔고매입비_추매제한.Enabled = false;
                Form1.form1.CB_misu.Enabled = false;
                Form1.form1.CB_misu.Enabled = false;
                Form1.form1.CB_misu.Enabled = false;
                Form1.form1.MT_misu_time.Enabled = false;
                Form1.form1.Combo_misu.Enabled = false;
                Form1.form1.TB_misu_ratio.Enabled = false;
                Form1.form1.TB_misu_value.Enabled = false;
                Form1.form1.Combo_misu_jumnun.Enabled = false;
                Form1.form1.TB_misu_repeat_time.Enabled = false;
                Form1.form1.label_계좌매입비1.Enabled = false;
                Form1.form1.label_계좌매입비2.Enabled = false;
                Form1.form1.label_잔고매입비.Enabled = false;
                Form1.form1.label_misu.Enabled = false;
            }

            Form1.form1.TB_p_ratio_use.Enabled = false;
            Form1.form1.combo_p_ratio_UD.Enabled = false;
            Form1.form1.combo_p_ratio.Enabled = false;
            Form1.form1.TB_p_go_use.Enabled = false;
            Form1.form1.combo_p_go_UD.Enabled = false;
            Form1.form1.combo_p_go.Enabled = false;
            Form1.form1.TB_p_down_use.Enabled = false;
            Form1.form1.combo_p_down_UD.Enabled = false;
            Form1.form1.combo_p_down.Enabled = false;
            Form1.form1.TB_q_ratio_use.Enabled = false;
            Form1.form1.combo_q_ratio_UD.Enabled = false;
            Form1.form1.combo_q_ratio.Enabled = false;
            Form1.form1.TB_q_go_use.Enabled = false;
            Form1.form1.combo_q_go_UD.Enabled = false;
            Form1.form1.combo_q_go.Enabled = false;
            Form1.form1.TB_q_down_use.Enabled = false;
            Form1.form1.combo_q_down_UD.Enabled = false;
            Form1.form1.combo_q_down.Enabled = false;
            Form1.form1.CB_미체결취소.Enabled = false;
            Form1.form1.BT_미체결취소.Enabled = false;
            Form1.form1.BT_관심그룹변경.Enabled = false;
            Form1.form1.CBB_지수연동_신규.Enabled = false;
            Form1.form1.CBB_지수연동_추매.Enabled = false;
        }

        public static void Form_Jisu_Disable()
        {
            box.Form_Jisu.form.BT_설정저장.Enabled = false;
            box.Form_Jisu.form.CB_지수이평사용_kospi.Enabled = false;
            box.Form_Jisu.form.CB_지수이평사용_kosdaq.Enabled = false;
            box.Form_Jisu.form.CB_use_kospi_min_03.Enabled = false;
            box.Form_Jisu.form.CB_use_kospi_min_05.Enabled = false;
            box.Form_Jisu.form.CB_use_kospi_min_10.Enabled = false;
            box.Form_Jisu.form.CB_use_kospi_min_20.Enabled = false;
            box.Form_Jisu.form.CB_use_kospi_min_30.Enabled = false;
            box.Form_Jisu.form.CB_use_kospi_min_60.Enabled = false;
            box.Form_Jisu.form.CB_use_kospi_day_03.Enabled = false;
            box.Form_Jisu.form.CB_use_kospi_day_05.Enabled = false;
            box.Form_Jisu.form.CB_use_kospi_day_10.Enabled = false;
            box.Form_Jisu.form.CB_use_kospi_day_20.Enabled = false;
            box.Form_Jisu.form.CB_use_kospi_day_40.Enabled = false;
            box.Form_Jisu.form.CB_use_kospi_day_60.Enabled = false;
            box.Form_Jisu.form.CB_use_kosdaq_min_03.Enabled = false;
            box.Form_Jisu.form.CB_use_kosdaq_min_05.Enabled = false;
            box.Form_Jisu.form.CB_use_kosdaq_min_10.Enabled = false;
            box.Form_Jisu.form.CB_use_kosdaq_min_20.Enabled = false;
            box.Form_Jisu.form.CB_use_kosdaq_min_30.Enabled = false;
            box.Form_Jisu.form.CB_use_kosdaq_min_60.Enabled = false;
            box.Form_Jisu.form.CB_use_kosdaq_day_03.Enabled = false;
            box.Form_Jisu.form.CB_use_kosdaq_day_05.Enabled = false;
            box.Form_Jisu.form.CB_use_kosdaq_day_10.Enabled = false;
            box.Form_Jisu.form.CB_use_kosdaq_day_20.Enabled = false;
            box.Form_Jisu.form.CB_use_kosdaq_day_40.Enabled = false;
            box.Form_Jisu.form.CB_use_kosdaq_day_60.Enabled = false;
            box.Form_Jisu.form.CB_UD_kospi_min_03.Enabled = false;
            box.Form_Jisu.form.CB_UD_kospi_min_05.Enabled = false;
            box.Form_Jisu.form.CB_UD_kospi_min_10.Enabled = false;
            box.Form_Jisu.form.CB_UD_kospi_min_20.Enabled = false;
            box.Form_Jisu.form.CB_UD_kospi_min_30.Enabled = false;
            box.Form_Jisu.form.CB_UD_kospi_min_60.Enabled = false;
            box.Form_Jisu.form.CB_UD_kospi_day_03.Enabled = false;
            box.Form_Jisu.form.CB_UD_kospi_day_05.Enabled = false;
            box.Form_Jisu.form.CB_UD_kospi_day_10.Enabled = false;
            box.Form_Jisu.form.CB_UD_kospi_day_20.Enabled = false;
            box.Form_Jisu.form.CB_UD_kospi_day_40.Enabled = false;
            box.Form_Jisu.form.CB_UD_kospi_day_60.Enabled = false;
            box.Form_Jisu.form.CB_UD_kosdaq_min_03.Enabled = false;
            box.Form_Jisu.form.CB_UD_kosdaq_min_05.Enabled = false;
            box.Form_Jisu.form.CB_UD_kosdaq_min_10.Enabled = false;
            box.Form_Jisu.form.CB_UD_kosdaq_min_20.Enabled = false;
            box.Form_Jisu.form.CB_UD_kosdaq_min_30.Enabled = false;
            box.Form_Jisu.form.CB_UD_kosdaq_min_60.Enabled = false;
            box.Form_Jisu.form.CB_UD_kosdaq_day_03.Enabled = false;
            box.Form_Jisu.form.CB_UD_kosdaq_day_05.Enabled = false;
            box.Form_Jisu.form.CB_UD_kosdaq_day_10.Enabled = false;
            box.Form_Jisu.form.CB_UD_kosdaq_day_20.Enabled = false;
            box.Form_Jisu.form.CB_UD_kosdaq_day_40.Enabled = false;
            box.Form_Jisu.form.CB_UD_kosdaq_day_60.Enabled = false;
        }

        public static void Form_Basic_Disable()
        {
            if (GenieConfig.CB_기본매매변경)
            {
                Form_Basic.form.BT_기본매매저장.Enabled = true;

                Form_Basic.form.CB_new_A.Enabled = true;
                Form_Basic.form.CB_new_B.Enabled = true;
                Form_Basic.form.CB_new_C.Enabled = true;
                Form_Basic.form.combo_new_or_A.Enabled = true;
                Form_Basic.form.combo_new_or_B.Enabled = true;
                Form_Basic.form.combo_new_or_C.Enabled = true;
                Form_Basic.form.신규_A.Enabled = true;
                Form_Basic.form.신규_B.Enabled = true;
                Form_Basic.form.신규_C.Enabled = true;
                Form_Basic.form.CB_TS_A.Enabled = true;
                Form_Basic.form.CB_TS_B.Enabled = true;
                Form_Basic.form.CB_TS_C.Enabled = true;
                Form_Basic.form.CB_TS_D.Enabled = true;
                Form_Basic.form.CB_TS_E.Enabled = true;
                Form_Basic.form.CB_TS_F.Enabled = true;
                Form_Basic.form.CB_TS_G.Enabled = true;
                Form_Basic.form.CB_TS_H.Enabled = true;
                Form_Basic.form.CB_TS_I.Enabled = true;
            }
            else
            {
                Form_Basic.form.BT_기본매매저장.Enabled = false;

                Form_Basic.form.CB_new_A.Enabled = false;
                Form_Basic.form.CB_new_B.Enabled = false;
                Form_Basic.form.CB_new_C.Enabled = false;
                Form_Basic.form.combo_new_or_A.Enabled = false;
                Form_Basic.form.combo_new_or_B.Enabled = false;
                Form_Basic.form.combo_new_or_C.Enabled = false;
                Form_Basic.form.신규_A.Enabled = false;
                Form_Basic.form.신규_B.Enabled = false;
                Form_Basic.form.신규_C.Enabled = false;
                Form_Basic.form.CB_TS_A.Enabled = false;
                Form_Basic.form.CB_TS_B.Enabled = false;
                Form_Basic.form.CB_TS_C.Enabled = false;
                Form_Basic.form.CB_TS_D.Enabled = false;
                Form_Basic.form.CB_TS_E.Enabled = false;
                Form_Basic.form.CB_TS_F.Enabled = false;
                Form_Basic.form.CB_TS_G.Enabled = false;
                Form_Basic.form.CB_TS_H.Enabled = false;
                Form_Basic.form.CB_TS_I.Enabled = false;
            }
        }

        public static void Form_Repeat_Disable()
        {
            Form_Repeat.form.BT_반복매매저장.Enabled = false;

            Form_Repeat.form.CB_repeat_use_A.Enabled = false;
            Form_Repeat.form.CB_repeat_use_B.Enabled = false;
            Form_Repeat.form.CB_repeat_use_C.Enabled = false;
            Form_Repeat.form.CB_repeat_use_D.Enabled = false;
            Form_Repeat.form.CB_repeat_use_E.Enabled = false;
            Form_Repeat.form.CB_repeat_use_F.Enabled = false;
            Form_Repeat.form.CB_repeat_use_G.Enabled = false;
            Form_Repeat.form.CB_repeat_use_H.Enabled = false;
            Form_Repeat.form.CB_repeat_use_I.Enabled = false;
            Form_Repeat.form.CB_repeat_use_J.Enabled = false;
            Form_Repeat.form.CB_repeat_use_K.Enabled = false;
            Form_Repeat.form.CB_repeat_use_L.Enabled = false;
            Form_Repeat.form.CB_repeat_use_M.Enabled = false;
            Form_Repeat.form.CB_repeat_use_N.Enabled = false;

            Form_Repeat.form.combo_repeat_use_condition_A.Enabled = false;
            Form_Repeat.form.combo_repeat_use_condition_B.Enabled = false;
            Form_Repeat.form.combo_repeat_use_condition_C.Enabled = false;
            Form_Repeat.form.combo_repeat_use_condition_D.Enabled = false;
            Form_Repeat.form.combo_repeat_use_condition_E.Enabled = false;
            Form_Repeat.form.combo_repeat_use_condition_F.Enabled = false;
            Form_Repeat.form.combo_repeat_use_condition_G.Enabled = false;
            Form_Repeat.form.combo_repeat_use_condition_H.Enabled = false;
            Form_Repeat.form.combo_repeat_use_condition_I.Enabled = false;
            Form_Repeat.form.combo_repeat_use_condition_J.Enabled = false;
            Form_Repeat.form.combo_repeat_use_condition_K.Enabled = false;
            Form_Repeat.form.combo_repeat_use_condition_L.Enabled = false;
            Form_Repeat.form.combo_repeat_use_condition_M.Enabled = false;
            Form_Repeat.form.combo_repeat_use_condition_N.Enabled = false;

            Form_Repeat.form.반복_A.Enabled = false;
            Form_Repeat.form.반복_B.Enabled = false;
            Form_Repeat.form.반복_C.Enabled = false;
            Form_Repeat.form.반복_D.Enabled = false;
            Form_Repeat.form.반복_E.Enabled = false;
            Form_Repeat.form.반복_F.Enabled = false;
            Form_Repeat.form.반복_G.Enabled = false;
            Form_Repeat.form.반복_H.Enabled = false;
            Form_Repeat.form.반복_I.Enabled = false;
            Form_Repeat.form.반복_J.Enabled = false;
            Form_Repeat.form.반복_K.Enabled = false;
            Form_Repeat.form.반복_L.Enabled = false;
            Form_Repeat.form.반복_M.Enabled = false;
            Form_Repeat.form.반복_N.Enabled = false;

            Form_Repeat.form.CB_repeat_kind_A.Enabled = false;
            Form_Repeat.form.CB_repeat_kind_B.Enabled = false;
            Form_Repeat.form.CB_repeat_kind_C.Enabled = false;
            Form_Repeat.form.CB_repeat_kind_D.Enabled = false;
            Form_Repeat.form.CB_repeat_kind_E.Enabled = false;
            Form_Repeat.form.CB_repeat_kind_F.Enabled = false;
            Form_Repeat.form.CB_repeat_kind_G.Enabled = false;
            Form_Repeat.form.CB_repeat_kind_H.Enabled = false;
            Form_Repeat.form.CB_repeat_kind_I.Enabled = false;
            Form_Repeat.form.CB_repeat_kind_J.Enabled = false;
            Form_Repeat.form.CB_repeat_kind_K.Enabled = false;
            Form_Repeat.form.CB_repeat_kind_L.Enabled = false;
            Form_Repeat.form.CB_repeat_kind_M.Enabled = false;
            Form_Repeat.form.CB_repeat_kind_N.Enabled = false;

        }

        public static void Form_AccountManagement_Disable()
        {
            Form_AccountManagement.form.BT_계좌관리저장.Enabled = false;

            Form_AccountManagement.form.CB_rebalance_A.Enabled = false;
            Form_AccountManagement.form.CB_rebalance_B.Enabled = false;
            Form_AccountManagement.form.CB_rebalance_C.Enabled = false;
            Form_AccountManagement.form.CB_rebalance_D.Enabled = false;
            Form_AccountManagement.form.CB_rebalance_E.Enabled = false;
            Form_AccountManagement.form.CB_rebalance_F.Enabled = false;
            Form_AccountManagement.form.CB_rebalance_G.Enabled = false;

            Form_AccountManagement.form.combo_rebalance_use_condition_A.Enabled = false;
            Form_AccountManagement.form.combo_rebalance_use_condition_B.Enabled = false;
            Form_AccountManagement.form.combo_rebalance_use_condition_C.Enabled = false;
            Form_AccountManagement.form.combo_rebalance_use_condition_D.Enabled = false;
            Form_AccountManagement.form.combo_rebalance_use_condition_E.Enabled = false;
            Form_AccountManagement.form.combo_rebalance_use_condition_F.Enabled = false;
            Form_AccountManagement.form.combo_rebalance_use_condition_G.Enabled = false;

            Form_AccountManagement.form.리밸_A.Enabled = false;
            Form_AccountManagement.form.리밸_B.Enabled = false;
            Form_AccountManagement.form.리밸_C.Enabled = false;
            Form_AccountManagement.form.리밸_D.Enabled = false;
            Form_AccountManagement.form.리밸_E.Enabled = false;
            Form_AccountManagement.form.리밸_F.Enabled = false;
            Form_AccountManagement.form.리밸_G.Enabled = false;

            Form_AccountManagement.form.CB_Liquidation_A.Enabled = false;
            Form_AccountManagement.form.CB_Liquidation_B.Enabled = false;
            Form_AccountManagement.form.CB_Liquidation_C.Enabled = false;

            Form_AccountManagement.form.CBB_Liquidation_use_condition_A.Enabled = false;
            Form_AccountManagement.form.CBB_Liquidation_use_condition_B.Enabled = false;
            Form_AccountManagement.form.CBB_Liquidation_use_condition_C.Enabled = false;

            Form_AccountManagement.form.청산_A.Enabled = false;
            Form_AccountManagement.form.청산_B.Enabled = false;
            Form_AccountManagement.form.청산_C.Enabled = false;

            Form_AccountManagement.form.CB_cut_A.Enabled = false;
            Form_AccountManagement.form.CB_cut_B.Enabled = false;
            Form_AccountManagement.form.CB_cut_C.Enabled = false;

            if (GenieConfig.CB_기본매매변경)
            {
                Form_AccountManagement.form.TB_매수비율.Enabled = true;
                Form_AccountManagement.form.TB_손익비율.Enabled = true;
            }
            else
            {
                Form_AccountManagement.form.TB_매수비율.Enabled = false;
                Form_AccountManagement.form.TB_손익비율.Enabled = false;
            }
        }

        public static void Form_Special_Disable()
        {
            Form_Special.form.BT_특수매매저장.Enabled = false;
            Form_Special.form.CBB_매매기간_trading_A.Enabled = false;
            Form_Special.form.CBB_매매기간_trading_B.Enabled = false;
            Form_Special.form.CBB_매매기간_trading_C.Enabled = false;
            Form_Special.form.CBB_매매기간_trading_D.Enabled = false;
            Form_Special.form.CBB_매매기간_trading_E.Enabled = false;
            Form_Special.form.CBB_매매기간_trading_F.Enabled = false;
        }

        public static void Form_TradeGroup_Disable()
        {
            Form_TradeGroup.form.BT_매매그룹저장.Enabled = false;

            Form_TradeGroup.form.BT_ik_allcheck.Enabled = false;
            Form_TradeGroup.form.BT_ik_all_Uncheck.Enabled = false;

            Form_TradeGroup.form.BT_sell_allcheck.Enabled = false;
            Form_TradeGroup.form.BT_sell_all_Uncheck.Enabled = false;

            Form_TradeGroup.form.BT_ALL_check_Repeat_A.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Repeat_B.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Repeat_C.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Repeat_D.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Repeat_E.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Repeat_F.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Repeat_G.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Repeat_H.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Repeat_I.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Repeat_J.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Repeat_K.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Repeat_L.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Repeat_M.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Repeat_N.Enabled = false;

            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_A.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_B.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_C.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_D.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_E.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_F.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_G.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_H.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_I.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_J.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_K.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_L.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_M.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Repeat_N.Enabled = false;

            Form_TradeGroup.form.BT_특정시간_allcheck.Enabled = false;
            Form_TradeGroup.form.BT_특정시간_all_Uncheck.Enabled = false;
            Form_TradeGroup.form.BT_실현손익_allcheck.Enabled = false;
            Form_TradeGroup.form.BT_실현손익_all_Uncheck.Enabled = false;
            Form_TradeGroup.form.BT_예상손실_allcheck.Enabled = false;
            Form_TradeGroup.form.BT_예상손실_all_Uncheck.Enabled = false;
            Form_TradeGroup.form.BT_예상수익_allcheck.Enabled = false;
            Form_TradeGroup.form.BT_예상수익_all_Uncheck.Enabled = false;
            Form_TradeGroup.form.BT_allcheck_시간청산_A.Enabled = false;
            Form_TradeGroup.form.BT_allcheck_시간청산_B.Enabled = false;
            Form_TradeGroup.form.BT_allcheck_시간청산_C.Enabled = false;
            Form_TradeGroup.form.BT_all_Uncheck_시간청산_A.Enabled = false;
            Form_TradeGroup.form.BT_all_Uncheck_시간청산_B.Enabled = false;
            Form_TradeGroup.form.BT_all_Uncheck_시간청산_C.Enabled = false;

            Form_TradeGroup.form.BT_미수금정리_allcheck.Enabled = false;
            Form_TradeGroup.form.BT_미수금정리_all_Uncheck.Enabled = false;

            Form_TradeGroup.form.BT_allcheck_손익담보_A.Enabled = false;
            Form_TradeGroup.form.BT_allcheck_손익담보_B.Enabled = false;
            Form_TradeGroup.form.BT_allcheck_손익담보_C.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_rebalance_A.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_rebalance_B.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_rebalance_C.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_rebalance_D.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_rebalance_E.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_rebalance_F.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_rebalance_G.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Liquidation_A.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Liquidation_B.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_Liquidation_C.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_day_A.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_day_B.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_day_C.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_day_D.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_day_E.Enabled = false;
            Form_TradeGroup.form.BT_ALL_check_day_F.Enabled = false;

            Form_TradeGroup.form.BT_all_Uncheck_손익담보_A.Enabled = false;
            Form_TradeGroup.form.BT_all_Uncheck_손익담보_B.Enabled = false;
            Form_TradeGroup.form.BT_all_Uncheck_손익담보_C.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_rebalance_A.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_rebalance_B.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_rebalance_C.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_rebalance_D.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_rebalance_E.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_rebalance_F.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_rebalance_G.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Liquidation_A.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Liquidation_B.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_Liquidation_C.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_day_A.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_day_B.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_day_C.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_day_D.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_day_E.Enabled = false;
            Form_TradeGroup.form.BT_ALL_Uncheck_day_F.Enabled = false;
        }

        public static void Form_PriceSearch_Disable()
        {
            Form_PriceSearch.form.BT_대금탐색저장.Enabled = false;

            Form_PriceSearch.form.CB_매수탐색A.Enabled = false;
            Form_PriceSearch.form.CB_매수탐색B.Enabled = false;
            Form_PriceSearch.form.CB_매도탐색.Enabled = false;
        }

        public static void Form_Function_Disable()
        {
            if (GenieConfig.CB_기본매매변경)
            {
                Form_Function.form.CB_편입추가.Enabled = true;
                Form_Function.form.CB_최종가업데이트.Enabled = true;
                Form_Function.form.CB_신규매수정지.Enabled = true;
                Form_Function.form.CB_추가매수정지.Enabled = true;
                Form_Function.form.CB_VI매수취소.Enabled = true;
                Form_Function.form.CB_VI매도취소.Enabled = true;
                Form_Function.form.CB_상매수취소.Enabled = true;
                Form_Function.form.CB_하매도취소.Enabled = true;
                Form_Function.form.CB_상전량청산.Enabled = true;
                Form_Function.form.CB_하전량청산.Enabled = true;
                Form_Function.form.CB_중간가주문.Enabled = true;
                Form_Function.form.BT_가이드매매.Enabled = true;
                Form_Function.form.CB_NXT.Enabled = true;
                Form_Function.form.CB_NXT_매수금지.Enabled = true;
                Form_Function.form.CB_NXT_손실제한.Enabled = true;
            }
            else
            {
                Form_Function.form.CB_편입추가.Enabled = false;
                Form_Function.form.CB_최종가업데이트.Enabled = false;
                Form_Function.form.CB_신규매수정지.Enabled = false;
                Form_Function.form.CB_추가매수정지.Enabled = false;
                Form_Function.form.CB_VI매수취소.Enabled = false;
                Form_Function.form.CB_VI매도취소.Enabled = false;
                Form_Function.form.CB_상매수취소.Enabled = false;
                Form_Function.form.CB_하매도취소.Enabled = false;
                Form_Function.form.CB_상전량청산.Enabled = false;
                Form_Function.form.CB_하전량청산.Enabled = false;
                Form_Function.form.CB_중간가주문.Enabled = false;
                Form_Function.form.BT_가이드매매.Enabled = false;

                Form_Function.form.CB_NXT.Enabled = false;
                Form_Function.form.CB_NXT_매수금지.Enabled = false;
                Form_Function.form.CB_NXT_손실제한.Enabled = false;
            }
        }

    }
}
