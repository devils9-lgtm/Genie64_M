using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace 지니_64
{
    public partial class Form_Basic : Form
    {
        public static Form_Basic form;
        public Form_Basic()
        {
            form = this;
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }


        public void Form_Basic_Load()
        {
            Form1.음소거 = true;
            CB_scalping.Checked = Properties.Settings.Default.CB_scalping;
            CB_신규횟수제한.Checked = Properties.Settings.Default.CB_신규횟수제한;
            LB_신규매수횟수.Text = Properties.Settings.Default.신규횟수.ToString();
            TB_신규횟수제한.Text = Properties.Settings.Default.TB_신규횟수제한.ToString();

            string 신규검색식 = Properties.Settings.Default.신규검색식;
            combo_new_condition_A.Items.Add(신규검색식.Split('^')[0]); combo_new_condition_A.Text = 신규검색식.Split('^')[0];
            combo_new_condition_B.Items.Add(신규검색식.Split('^')[1]); combo_new_condition_B.Text = 신규검색식.Split('^')[1];
            combo_new_condition_C.Items.Add(신규검색식.Split('^')[2]); combo_new_condition_C.Text = 신규검색식.Split('^')[2];

            combo_new_choice_A.SelectedIndex = Properties.Settings.Default.combo_new_choice_A;
            combo_new_choice_B.SelectedIndex = Properties.Settings.Default.combo_new_choice_B;
            combo_new_choice_C.SelectedIndex = Properties.Settings.Default.combo_new_choice_C;

            CB_new_A.Checked = Properties.Settings.Default.CB_new_A;
            CB_new_B.Checked = Properties.Settings.Default.CB_new_B;
            CB_new_C.Checked = Properties.Settings.Default.CB_new_C;

            MTB_new_delay_A.Text = Properties.Settings.Default.MTB_new_delay_A.ToString();
            MTB_new_delay_B.Text = Properties.Settings.Default.MTB_new_delay_B.ToString();
            MTB_new_delay_C.Text = Properties.Settings.Default.MTB_new_delay_C.ToString();

            TB_new_value_A.Text = Properties.Settings.Default.TB_new_value_A.ToString();
            TB_new_value_B.Text = Properties.Settings.Default.TB_new_value_B.ToString();
            TB_new_value_C.Text = Properties.Settings.Default.TB_new_value_C.ToString();

            CB_new_recatch_A.Checked = Properties.Settings.Default.CB_new_recatch_A;
            CB_new_recatch_B.Checked = Properties.Settings.Default.CB_new_recatch_B;
            CB_new_recatch_C.Checked = Properties.Settings.Default.CB_new_recatch_C;

            MTB_ik_repeat.Text = Properties.Settings.Default.MTB_ik_repeat.ToString();
            MTB_new_repeat_A.Text = Properties.Settings.Default.MTB_new_repeat_A.ToString();
            MTB_new_repeat_B.Text = Properties.Settings.Default.MTB_new_repeat_B.ToString();
            MTB_new_repeat_C.Text = Properties.Settings.Default.MTB_new_repeat_C.ToString();
            MT_new_start_A.Text = Properties.Settings.Default.MT_new_start_A.ToString();
            MT_new_start_B.Text = Properties.Settings.Default.MT_new_start_B.ToString();
            MT_new_start_C.Text = Properties.Settings.Default.MT_new_start_C.ToString();

            MT_new_end_A.Text = Properties.Settings.Default.MT_new_end_A.ToString();
            MT_new_end_B.Text = Properties.Settings.Default.MT_new_end_B.ToString();
            MT_new_end_C.Text = Properties.Settings.Default.MT_new_end_C.ToString();

            MT_new_ratio_A.Text = Properties.Settings.Default.MT_new_ratio_A.ToString();
            MT_new_ratio_B.Text = Properties.Settings.Default.MT_new_ratio_B.ToString();
            MT_new_ratio_C.Text = Properties.Settings.Default.MT_new_ratio_C.ToString();

            CB_ik_A.Checked = Properties.Settings.Default.CB_ik_A;
            CB_ik_B.Checked = Properties.Settings.Default.CB_ik_B;
            CB_ik_C.Checked = Properties.Settings.Default.CB_ik_C;
            CB_ik_D.Checked = Properties.Settings.Default.CB_ik_D;
            CB_ik_E.Checked = Properties.Settings.Default.CB_ik_E;
            CB_ik_F.Checked = Properties.Settings.Default.CB_ik_F;
            CB_ik_G.Checked = Properties.Settings.Default.CB_ik_G;
            CB_ik_H.Checked = Properties.Settings.Default.CB_ik_H;
            CB_ik_I.Checked = Properties.Settings.Default.CB_ik_I;

            CB_ik_down_A.Checked = Properties.Settings.Default.CB_ik_down_A;
            CB_ik_down_B.Checked = Properties.Settings.Default.CB_ik_down_B;
            CB_ik_down_C.Checked = Properties.Settings.Default.CB_ik_down_C;
            CB_ik_down_D.Checked = Properties.Settings.Default.CB_ik_down_D;
            CB_ik_down_E.Checked = Properties.Settings.Default.CB_ik_down_E;
            CB_ik_down_F.Checked = Properties.Settings.Default.CB_ik_down_F;
            CB_ik_down_G.Checked = Properties.Settings.Default.CB_ik_down_G;
            CB_ik_down_H.Checked = Properties.Settings.Default.CB_ik_down_H;
            CB_ik_down_I.Checked = Properties.Settings.Default.CB_ik_down_I;

            TB_ik_son_A.Text = Properties.Settings.Default.TB_ik_son_A.ToString();
            TB_ik_son_B.Text = Properties.Settings.Default.TB_ik_son_B.ToString();
            TB_ik_son_C.Text = Properties.Settings.Default.TB_ik_son_C.ToString();
            TB_ik_son_D.Text = Properties.Settings.Default.TB_ik_son_D.ToString();
            TB_ik_son_E.Text = Properties.Settings.Default.TB_ik_son_E.ToString();
            TB_ik_son_F.Text = Properties.Settings.Default.TB_ik_son_F.ToString();
            TB_ik_son_G.Text = Properties.Settings.Default.TB_ik_son_G.ToString();
            TB_ik_son_H.Text = Properties.Settings.Default.TB_ik_son_H.ToString();
            TB_ik_son_I.Text = Properties.Settings.Default.TB_ik_son_I.ToString();

            TB_ik_value_A.Text = Properties.Settings.Default.TB_ik_value_A.ToString();
            TB_ik_value_B.Text = Properties.Settings.Default.TB_ik_value_B.ToString();
            TB_ik_value_C.Text = Properties.Settings.Default.TB_ik_value_C.ToString();
            TB_ik_value_D.Text = Properties.Settings.Default.TB_ik_value_D.ToString();
            TB_ik_value_E.Text = Properties.Settings.Default.TB_ik_value_E.ToString();
            TB_ik_value_F.Text = Properties.Settings.Default.TB_ik_value_F.ToString();
            TB_ik_value_G.Text = Properties.Settings.Default.TB_ik_value_G.ToString();
            TB_ik_value_H.Text = Properties.Settings.Default.TB_ik_value_H.ToString();
            TB_ik_value_I.Text = Properties.Settings.Default.TB_ik_value_I.ToString();

            TB_ik_son_ratio_A.Text = Properties.Settings.Default.TB_ik_son_ratio_A.ToString();
            TB_ik_son_ratio_B.Text = Properties.Settings.Default.TB_ik_son_ratio_B.ToString();
            TB_ik_son_ratio_C.Text = Properties.Settings.Default.TB_ik_son_ratio_C.ToString();
            TB_ik_son_ratio_D.Text = Properties.Settings.Default.TB_ik_son_ratio_D.ToString();
            TB_ik_son_ratio_E.Text = Properties.Settings.Default.TB_ik_son_ratio_E.ToString();
            TB_ik_son_ratio_F.Text = Properties.Settings.Default.TB_ik_son_ratio_F.ToString();
            TB_ik_son_ratio_G.Text = Properties.Settings.Default.TB_ik_son_ratio_G.ToString();
            TB_ik_son_ratio_H.Text = Properties.Settings.Default.TB_ik_son_ratio_H.ToString();
            TB_ik_son_ratio_I.Text = Properties.Settings.Default.TB_ik_son_ratio_I.ToString();

            TB_ik_down_A.Text = Properties.Settings.Default.TB_ik_down_A.ToString();
            TB_ik_down_B.Text = Properties.Settings.Default.TB_ik_down_B.ToString();
            TB_ik_down_C.Text = Properties.Settings.Default.TB_ik_down_C.ToString();
            TB_ik_down_D.Text = Properties.Settings.Default.TB_ik_down_D.ToString();
            TB_ik_down_E.Text = Properties.Settings.Default.TB_ik_down_E.ToString();
            TB_ik_down_F.Text = Properties.Settings.Default.TB_ik_down_F.ToString();
            TB_ik_down_G.Text = Properties.Settings.Default.TB_ik_down_G.ToString();
            TB_ik_down_H.Text = Properties.Settings.Default.TB_ik_down_H.ToString();
            TB_ik_down_I.Text = Properties.Settings.Default.TB_ik_down_I.ToString();

            TB_ik_down_ratio_A.Text = Properties.Settings.Default.TB_ik_down_ratio_A.ToString();
            TB_ik_down_ratio_B.Text = Properties.Settings.Default.TB_ik_down_ratio_B.ToString();
            TB_ik_down_ratio_C.Text = Properties.Settings.Default.TB_ik_down_ratio_C.ToString();
            TB_ik_down_ratio_D.Text = Properties.Settings.Default.TB_ik_down_ratio_D.ToString();
            TB_ik_down_ratio_E.Text = Properties.Settings.Default.TB_ik_down_ratio_E.ToString();
            TB_ik_down_ratio_F.Text = Properties.Settings.Default.TB_ik_down_ratio_F.ToString();
            TB_ik_down_ratio_G.Text = Properties.Settings.Default.TB_ik_down_ratio_G.ToString();
            TB_ik_down_ratio_H.Text = Properties.Settings.Default.TB_ik_down_ratio_H.ToString();
            TB_ik_down_ratio_I.Text = Properties.Settings.Default.TB_ik_down_ratio_I.ToString();

            TB_ik_down_value_A.Text = Properties.Settings.Default.TB_ik_down_value_A.ToString();
            TB_ik_down_value_B.Text = Properties.Settings.Default.TB_ik_down_value_B.ToString();
            TB_ik_down_value_C.Text = Properties.Settings.Default.TB_ik_down_value_C.ToString();
            TB_ik_down_value_D.Text = Properties.Settings.Default.TB_ik_down_value_D.ToString();
            TB_ik_down_value_E.Text = Properties.Settings.Default.TB_ik_down_value_E.ToString();
            TB_ik_down_value_F.Text = Properties.Settings.Default.TB_ik_down_value_F.ToString();
            TB_ik_down_value_G.Text = Properties.Settings.Default.TB_ik_down_value_G.ToString();
            TB_ik_down_value_H.Text = Properties.Settings.Default.TB_ik_down_value_H.ToString();
            TB_ik_down_value_I.Text = Properties.Settings.Default.TB_ik_down_value_I.ToString();

            combo_ik_A.SelectedIndex = Properties.Settings.Default.combo_ik_A;
            combo_ik_B.SelectedIndex = Properties.Settings.Default.combo_ik_B;
            combo_ik_C.SelectedIndex = Properties.Settings.Default.combo_ik_C;
            combo_ik_D.SelectedIndex = Properties.Settings.Default.combo_ik_D;
            combo_ik_E.SelectedIndex = Properties.Settings.Default.combo_ik_E;
            combo_ik_F.SelectedIndex = Properties.Settings.Default.combo_ik_F;
            combo_ik_G.SelectedIndex = Properties.Settings.Default.combo_ik_G;
            combo_ik_H.SelectedIndex = Properties.Settings.Default.combo_ik_H;
            combo_ik_I.SelectedIndex = Properties.Settings.Default.combo_ik_I;

            combo_ik_ratio_A.SelectedIndex = Properties.Settings.Default.combo_ik_ratio_A;
            combo_ik_ratio_B.SelectedIndex = Properties.Settings.Default.combo_ik_ratio_B;
            combo_ik_ratio_C.SelectedIndex = Properties.Settings.Default.combo_ik_ratio_C;
            combo_ik_ratio_D.SelectedIndex = Properties.Settings.Default.combo_ik_ratio_D;
            combo_ik_ratio_E.SelectedIndex = Properties.Settings.Default.combo_ik_ratio_E;
            combo_ik_ratio_F.SelectedIndex = Properties.Settings.Default.combo_ik_ratio_F;
            combo_ik_ratio_G.SelectedIndex = Properties.Settings.Default.combo_ik_ratio_G;
            combo_ik_ratio_H.SelectedIndex = Properties.Settings.Default.combo_ik_ratio_H;
            combo_ik_ratio_I.SelectedIndex = Properties.Settings.Default.combo_ik_ratio_I;

            combo_ik_down_A.SelectedIndex = Properties.Settings.Default.combo_ik_down_A;
            combo_ik_down_B.SelectedIndex = Properties.Settings.Default.combo_ik_down_B;
            combo_ik_down_C.SelectedIndex = Properties.Settings.Default.combo_ik_down_C;
            combo_ik_down_D.SelectedIndex = Properties.Settings.Default.combo_ik_down_D;
            combo_ik_down_E.SelectedIndex = Properties.Settings.Default.combo_ik_down_E;
            combo_ik_down_F.SelectedIndex = Properties.Settings.Default.combo_ik_down_F;
            combo_ik_down_G.SelectedIndex = Properties.Settings.Default.combo_ik_down_G;
            combo_ik_down_H.SelectedIndex = Properties.Settings.Default.combo_ik_down_H;
            combo_ik_down_I.SelectedIndex = Properties.Settings.Default.combo_ik_down_I;

            combo_ik_down_ratio_A.SelectedIndex = Properties.Settings.Default.combo_ik_down_ratio_A;
            combo_ik_down_ratio_B.SelectedIndex = Properties.Settings.Default.combo_ik_down_ratio_B;
            combo_ik_down_ratio_C.SelectedIndex = Properties.Settings.Default.combo_ik_down_ratio_C;
            combo_ik_down_ratio_D.SelectedIndex = Properties.Settings.Default.combo_ik_down_ratio_D;
            combo_ik_down_ratio_E.SelectedIndex = Properties.Settings.Default.combo_ik_down_ratio_E;
            combo_ik_down_ratio_F.SelectedIndex = Properties.Settings.Default.combo_ik_down_ratio_F;
            combo_ik_down_ratio_G.SelectedIndex = Properties.Settings.Default.combo_ik_down_ratio_G;
            combo_ik_down_ratio_H.SelectedIndex = Properties.Settings.Default.combo_ik_down_ratio_H;
            combo_ik_down_ratio_I.SelectedIndex = Properties.Settings.Default.combo_ik_down_ratio_I;

            CB_sell_time_use.Checked = Properties.Settings.Default.CB_sell_time_use;
            CB_sell_time_overlap.Checked = Properties.Settings.Default.CB_sell_time_overlap;
            CB_sell_time_Buystop.Checked = Properties.Settings.Default.CB_sell_time_Buystop;
            CB_sell_time_잔량취소.Checked = Properties.Settings.Default.CB_sell_time_잔량취소;
            combo_sell_time_gubun.SelectedIndex = Properties.Settings.Default.combo_sell_time_gubun;
            combo_sell_time_jumun.SelectedIndex = Properties.Settings.Default.combo_sell_time_jumun;
            MT_sell_time_start.Text = Properties.Settings.Default.MT_sell_time_start.ToString();
            MT_sell_time_end.Text = Properties.Settings.Default.MT_sell_time_end.ToString();
            MT_sell_time_repeat.Text = Properties.Settings.Default.MT_sell_time_repeat.ToString();
            MT_sell_time_CanselTime.Text = Properties.Settings.Default.MT_sell_time_cansel.ToString();
            TB_sell_time_trade_1.Text = Properties.Settings.Default.TB_sell_time_trade_1.ToString();
            TB_sell_time_trade_2.Text = Properties.Settings.Default.TB_sell_time_trade_2.ToString();
            TB_sell_time_ratio.Text = Properties.Settings.Default.TB_sell_time_ratio.ToString();
            TB_sell_time_ik_1.Text = Properties.Settings.Default.TB_sell_time_ik_1.ToString();
            TB_sell_time_ik_2.Text = Properties.Settings.Default.TB_sell_time_ik_2.ToString();
            TB_sell_time_매입금.Text = Properties.Settings.Default.TB_sell_time_매입금.ToString();
            CBB_sell_time_choice.SelectedIndex = Properties.Settings.Default.CBB_sell_time_choice;

            TB_지수연동이하.Text = Properties.Settings.Default.TB_지수연동이하.ToString();
            TB_지수연동이상.Text = Properties.Settings.Default.TB_지수연동이상.ToString();
            combo_지수연동이하.SelectedIndex = Properties.Settings.Default.combo_지수연동이하;
            combo_지수연동이상.SelectedIndex = Properties.Settings.Default.combo_지수연동이상;
            CB_지수연동범위사용.Checked = Properties.Settings.Default.CB_지수연동범위사용;

            CB_silson_use_W.Checked = Properties.Settings.Default.CB_silson_use_W;
            CB_silson_overlap_W.Checked = Properties.Settings.Default.CB_silson_overlap_W;
            CB_silson_Buystop_W.Checked = Properties.Settings.Default.CB_silson_Buystop_W;
            CB_silson_잔량취소.Checked = Properties.Settings.Default.CB_silson_잔량취소;
            CB_silson_choice_W.Checked = Properties.Settings.Default.CB_silson_choice_W;

            combo_silson_gubun_W.SelectedIndex = Properties.Settings.Default.combo_silson_gubun_W;
            combo_silson_jumun_W.SelectedIndex = Properties.Settings.Default.combo_silson_jumun_W;

            CB_silson_청산기준.Checked = Properties.Settings.Default.CB_silson_청산기준;

            MT_silson_start_W.Text = Properties.Settings.Default.MT_silson_start_W.ToString();
            MT_silson_end_W.Text = Properties.Settings.Default.MT_silson_end_W.ToString();
            TB_silson_ik_W_1.Text = Properties.Settings.Default.TB_silson_ik_W_1.ToString();
            TB_silson_ik_W_2.Text = Properties.Settings.Default.TB_silson_ik_W_2.ToString();
            TB_silson_trade_W_1.Text = Properties.Settings.Default.TB_silson_trade_W_1.ToString();
            TB_silson_trade_W_2.Text = Properties.Settings.Default.TB_silson_trade_W_2.ToString();
            TB_silson_ratio_W.Text = Properties.Settings.Default.TB_silson_ratio_W.ToString();
            MT_silson_end_W.Text = Properties.Settings.Default.MT_silson_end_W.ToString();
            MT_silson_repeat_W.Text = Properties.Settings.Default.MT_silson_repeat_W.ToString();
            MT_silson_CancelTime.Text = Properties.Settings.Default.MT_silson_cancel_W.ToString();
            TB_silson_매입금_W.Text = Properties.Settings.Default.TB_silson_매입금_W.ToString();
            TB_silson_value_W.Text = Properties.Settings.Default.TB_silson_value_W.ToString();

            CB_지수연동청산.Checked = Properties.Settings.Default.CB_지수연동청산;

            CB_예상수익사용.Checked = Properties.Settings.Default.CB_예상수익사용;
            CB_예상수익_overlap.Checked = Properties.Settings.Default.CB_예상수익_overlap;
            CB_예상수익_Buystop.Checked = Properties.Settings.Default.CB_예상수익_Buystop;
            CB_예상수익_잔량취소.Checked = Properties.Settings.Default.CB_예상수익_잔량취소;
            CB_예상수익_choice.Checked = Properties.Settings.Default.CB_예상수익_choice;
            CB_예상수익_청산기준.Checked = Properties.Settings.Default.CB_예상수익_청산기준;
            combo_예상수익_gubun.SelectedIndex = Properties.Settings.Default.combo_예상수익_gubun;
            combo_예상수익_jumun.SelectedIndex = Properties.Settings.Default.combo_예상수익_jumun;

            TB_예상수익_ik_1.Text = Properties.Settings.Default.TB_예상수익_ik_1.ToString();
            TB_예상수익_ik_2.Text = Properties.Settings.Default.TB_예상수익_ik_2.ToString();
            TB_예상수익_trade_1.Text = Properties.Settings.Default.TB_예상수익_trade_1.ToString();
            TB_예상수익_trade_2.Text = Properties.Settings.Default.TB_예상수익_trade_2.ToString();
            TB_예상수익_ratio.Text = Properties.Settings.Default.TB_예상수익_ratio.ToString();
            MT_예상수익_start.Text = Properties.Settings.Default.MT_예상수익_start.ToString();
            MT_예상수익_end.Text = Properties.Settings.Default.MT_예상수익_end.ToString();
            MT_예상수익_repeat.Text = Properties.Settings.Default.MT_예상수익_repeat.ToString();
            MT_예상수익_CancelTime.Text = Properties.Settings.Default.MT_예상수익_cancel.ToString();
            TB_예상수익_매입금.Text = Properties.Settings.Default.TB_예상수익_매입금.ToString();
            TB_예상수익_value.Text = Properties.Settings.Default.TB_예상수익_value.ToString();

            CB_예상수익TS.Checked = Properties.Settings.Default.CB_예상수익TS;
            CB_예상수익TS_시작.Checked = Properties.Settings.Default.CB_예상수익TS_시작;

            if (CB_예상수익TS.Checked)
            {
                CB_예상수익사용.Text = "■";
                CB_예상수익사용.Checked = Properties.Settings.Default.CB_예상수익사용;
                CB_예상수익사용.Enabled = false;
            }

            TB_예상수익TS_상승값.Text = Properties.Settings.Default.TB_예상수익TS_상승값.ToString();
            TB_예상수익TS_하락값.Text = Properties.Settings.Default.TB_예상수익TS_하락값.ToString();

            CB_예상손실_use.Checked = Properties.Settings.Default.CB_예상손실_use;
            CB_예상손실_overlap.Checked = Properties.Settings.Default.CB_예상손실_overlap;
            CB_예상손실_Buystop.Checked = Properties.Settings.Default.CB_예상손실_Buystop;
            CB_예상손실_잔량취소.Checked = Properties.Settings.Default.CB_예상손실_잔량취소;
            CB_예상손실_choice.Checked = Properties.Settings.Default.CB_예상손실_choice;
            CB_예상손실_청산기준.Checked = Properties.Settings.Default.CB_예상손실_청산기준;
            combo_예상손실_gubun.SelectedIndex = Properties.Settings.Default.combo_예상손실_gubun;
            combo_예상손실_jumun.SelectedIndex = Properties.Settings.Default.combo_예상손실_jumun;

            TB_예상손실_ik_1.Text = Properties.Settings.Default.TB_예상손실_ik_1.ToString();
            TB_예상손실_ik_2.Text = Properties.Settings.Default.TB_예상손실_ik_2.ToString();
            TB_예상손실_trade_1.Text = Properties.Settings.Default.TB_예상손실_trade_1.ToString();
            TB_예상손실_trade_2.Text = Properties.Settings.Default.TB_예상손실_trade_2.ToString();
            TB_예상손실_ratio.Text = Properties.Settings.Default.TB_예상손실_ratio.ToString();
            MT_예상손실_start.Text = Properties.Settings.Default.MT_예상손실_start.ToString();
            MT_예상손실_end.Text = Properties.Settings.Default.MT_예상손실_end.ToString();
            MT_예상손실_repeat.Text = Properties.Settings.Default.MT_예상손실_repeat.ToString();
            MT_예상손실_CanceTime.Text = Properties.Settings.Default.MT_예상손실_CanceTime.ToString();
            TB_예상손실_매입.Text = Properties.Settings.Default.TB_예상손실_매입.ToString();
            TB_예상손실_value.Text = Properties.Settings.Default.TB_예상손실_value.ToString();

            CB_TimeSell_A.Checked = Properties.Settings.Default.CB_TimeSell_A;
            CB_TimeSell_B.Checked = Properties.Settings.Default.CB_TimeSell_B;
            CB_TimeSell_C.Checked = Properties.Settings.Default.CB_TimeSell_C;

            CB_TimeSell_수익범위_choice_A.Checked = Properties.Settings.Default.CB_TimeSell_수익범위_choice_A;
            CB_TimeSell_수익범위_choice_B.Checked = Properties.Settings.Default.CB_TimeSell_수익범위_choice_B;
            CB_TimeSell_수익범위_choice_C.Checked = Properties.Settings.Default.CB_TimeSell_수익범위_choice_C;

            CBB_TimeSell_start_A.SelectedIndex = Properties.Settings.Default.CBB_TimeSell_start_A;
            CBB_TimeSell_start_B.SelectedIndex = Properties.Settings.Default.CBB_TimeSell_start_B;
            CBB_TimeSell_start_C.SelectedIndex = Properties.Settings.Default.CBB_TimeSell_start_C;
            CBB_TimeSell_TC_DropDownClosed_(CBB_TimeSell_start_A);
            CBB_TimeSell_TC_DropDownClosed_(CBB_TimeSell_start_B);
            CBB_TimeSell_TC_DropDownClosed_(CBB_TimeSell_start_C);

            CBB_TimeSell_주문가격_A.SelectedIndex = Properties.Settings.Default.CBB_TimeSell_주문가격_A;
            CBB_TimeSell_주문가격_B.SelectedIndex = Properties.Settings.Default.CBB_TimeSell_주문가격_B;
            CBB_TimeSell_주문가격_C.SelectedIndex = Properties.Settings.Default.CBB_TimeSell_주문가격_C;

            CBB_TimeSell_수익구분_A.SelectedIndex = Properties.Settings.Default.CBB_TimeSell_수익구분_A;
            CBB_TimeSell_수익구분_B.SelectedIndex = Properties.Settings.Default.CBB_TimeSell_수익구분_B;
            CBB_TimeSell_수익구분_C.SelectedIndex = Properties.Settings.Default.CBB_TimeSell_수익구분_C;
            CBB_TimeSell_매도비중_A.SelectedIndex = Properties.Settings.Default.CBB_TimeSell_매도비중_A;
            CBB_TimeSell_매도비중_B.SelectedIndex = Properties.Settings.Default.CBB_TimeSell_매도비중_B;
            CBB_TimeSell_매도비중_C.SelectedIndex = Properties.Settings.Default.CBB_TimeSell_매도비중_C;

            TB_TimeSell_start_A.Text = Properties.Settings.Default.TB_TimeSell_start_A.ToString();
            TB_TimeSell_start_B.Text = Properties.Settings.Default.TB_TimeSell_start_B.ToString();
            TB_TimeSell_start_C.Text = Properties.Settings.Default.TB_TimeSell_start_C.ToString();
            MT_TimeSell_반복간격_A.Text = Properties.Settings.Default.MT_TimeSell_반복간격_A.ToString();
            MT_TimeSell_반복간격_B.Text = Properties.Settings.Default.MT_TimeSell_반복간격_B.ToString();
            MT_TimeSell_반복간격_C.Text = Properties.Settings.Default.MT_TimeSell_반복간격_C.ToString();
            MT_TimeSell_취소간격_A.Text = Properties.Settings.Default.MT_TimeSell_취소간격_A.ToString();
            MT_TimeSell_취소간격_B.Text = Properties.Settings.Default.MT_TimeSell_취소간격_B.ToString();
            MT_TimeSell_취소간격_C.Text = Properties.Settings.Default.MT_TimeSell_취소간격_C.ToString();

            TB_TimeSell_매매범위_1_A.Text = Properties.Settings.Default.TB_TimeSell_매매범위_1_A.ToString();
            TB_TimeSell_매매범위_1_B.Text = Properties.Settings.Default.TB_TimeSell_매매범위_1_B.ToString();
            TB_TimeSell_매매범위_1_C.Text = Properties.Settings.Default.TB_TimeSell_매매범위_1_C.ToString();
            TB_TimeSell_매매범위_2_A.Text = Properties.Settings.Default.TB_TimeSell_매매범위_2_A.ToString();
            TB_TimeSell_매매범위_2_B.Text = Properties.Settings.Default.TB_TimeSell_매매범위_2_B.ToString();
            TB_TimeSell_매매범위_2_C.Text = Properties.Settings.Default.TB_TimeSell_매매범위_2_C.ToString();
            TB_TimeSell_매도비중_A.Text = Properties.Settings.Default.TB_TimeSell_매도비중_A.ToString();
            TB_TimeSell_매도비중_B.Text = Properties.Settings.Default.TB_TimeSell_매도비중_B.ToString();
            TB_TimeSell_매도비중_C.Text = Properties.Settings.Default.TB_TimeSell_매도비중_C.ToString();
            TB_TimeSell_주문가격_A.Text = Properties.Settings.Default.TB_TimeSell_주문가격_A.ToString();
            TB_TimeSell_주문가격_B.Text = Properties.Settings.Default.TB_TimeSell_주문가격_B.ToString();
            TB_TimeSell_주문가격_C.Text = Properties.Settings.Default.TB_TimeSell_주문가격_C.ToString();
            TB_TimeSell_수익범위_1_A.Text = Properties.Settings.Default.TB_TimeSell_수익범위_1_A.ToString();
            TB_TimeSell_수익범위_1_B.Text = Properties.Settings.Default.TB_TimeSell_수익범위_1_B.ToString();
            TB_TimeSell_수익범위_1_C.Text = Properties.Settings.Default.TB_TimeSell_수익범위_1_C.ToString();
            TB_TimeSell_수익범위_2_A.Text = Properties.Settings.Default.TB_TimeSell_수익범위_2_A.ToString();
            TB_TimeSell_수익범위_2_B.Text = Properties.Settings.Default.TB_TimeSell_수익범위_2_B.ToString();
            TB_TimeSell_수익범위_2_C.Text = Properties.Settings.Default.TB_TimeSell_수익범위_2_C.ToString();
            TB_TimeSell_매입금1_A.Text = Properties.Settings.Default.TB_TimeSell_매입금1_A.ToString();
            TB_TimeSell_매입금1_B.Text = Properties.Settings.Default.TB_TimeSell_매입금1_B.ToString();
            TB_TimeSell_매입금1_C.Text = Properties.Settings.Default.TB_TimeSell_매입금1_C.ToString();
            TB_TimeSell_매입금2_A.Text = Properties.Settings.Default.TB_TimeSell_매입금2_A.ToString();
            TB_TimeSell_매입금2_B.Text = Properties.Settings.Default.TB_TimeSell_매입금2_B.ToString();
            TB_TimeSell_매입금2_C.Text = Properties.Settings.Default.TB_TimeSell_매입금2_C.ToString();

            TB_TimeSell_거래일_A.Text = Properties.Settings.Default.TB_TimeSell_거래일_A.ToString();
            TB_TimeSell_거래일_B.Text = Properties.Settings.Default.TB_TimeSell_거래일_B.ToString();
            TB_TimeSell_거래일_C.Text = Properties.Settings.Default.TB_TimeSell_거래일_C.ToString();

            CB_sell_use_A.Checked = Properties.Settings.Default.CB_sell_use_A;
            CB_sell_use_B.Checked = Properties.Settings.Default.CB_sell_use_B;
            CB_sell_use_C.Checked = Properties.Settings.Default.CB_sell_use_C;
            CB_sell_use_D.Checked = Properties.Settings.Default.CB_sell_use_D;
            CB_sell_use_E.Checked = Properties.Settings.Default.CB_sell_use_E;
            CB_sell_use_F.Checked = Properties.Settings.Default.CB_sell_use_F;

            MTB_sell_starttime.Text = Properties.Settings.Default.MTB_sell_starttime.ToString();
            MTB_sell_endtime.Text = Properties.Settings.Default.MTB_sell_endtime.ToString();

            TB_sell_son_A.Text = Properties.Settings.Default.TB_sell_son_A.ToString();
            TB_sell_son_B.Text = Properties.Settings.Default.TB_sell_son_B.ToString();
            TB_sell_son_C.Text = Properties.Settings.Default.TB_sell_son_C.ToString();
            TB_sell_son_D.Text = Properties.Settings.Default.TB_sell_son_D.ToString();
            TB_sell_son_E.Text = Properties.Settings.Default.TB_sell_son_E.ToString();
            TB_sell_son_F.Text = Properties.Settings.Default.TB_sell_son_F.ToString();

            combo_sell_son_A.SelectedIndex = Properties.Settings.Default.combo_sell_son_A;
            combo_sell_son_B.SelectedIndex = Properties.Settings.Default.combo_sell_son_B;
            combo_sell_son_C.SelectedIndex = Properties.Settings.Default.combo_sell_son_C;
            combo_sell_son_D.SelectedIndex = Properties.Settings.Default.combo_sell_son_D;
            combo_sell_son_E.SelectedIndex = Properties.Settings.Default.combo_sell_son_E;
            combo_sell_son_F.SelectedIndex = Properties.Settings.Default.combo_sell_son_F;

            TB_sell_ratio_A.Text = Properties.Settings.Default.TB_sell_ratio_A.ToString();
            TB_sell_ratio_B.Text = Properties.Settings.Default.TB_sell_ratio_B.ToString();
            TB_sell_ratio_C.Text = Properties.Settings.Default.TB_sell_ratio_C.ToString();
            TB_sell_ratio_D.Text = Properties.Settings.Default.TB_sell_ratio_D.ToString();
            TB_sell_ratio_E.Text = Properties.Settings.Default.TB_sell_ratio_E.ToString();
            TB_sell_ratio_F.Text = Properties.Settings.Default.TB_sell_ratio_F.ToString();

            TB_sell_value_A.Text = Properties.Settings.Default.TB_sell_value_A.ToString();
            TB_sell_value_B.Text = Properties.Settings.Default.TB_sell_value_B.ToString();
            TB_sell_value_C.Text = Properties.Settings.Default.TB_sell_value_C.ToString();
            TB_sell_value_D.Text = Properties.Settings.Default.TB_sell_value_D.ToString();
            TB_sell_value_E.Text = Properties.Settings.Default.TB_sell_value_E.ToString();
            TB_sell_value_F.Text = Properties.Settings.Default.TB_sell_value_F.ToString();

            combo_sell_ratio_A.SelectedIndex = Properties.Settings.Default.combo_sell_ratio_A;
            combo_sell_ratio_B.SelectedIndex = Properties.Settings.Default.combo_sell_ratio_B;
            combo_sell_ratio_C.SelectedIndex = Properties.Settings.Default.combo_sell_ratio_C;

            combo_sell_ratio_D.SelectedIndex = Properties.Settings.Default.combo_sell_ratio_D;
            combo_sell_ratio_E.SelectedIndex = Properties.Settings.Default.combo_sell_ratio_E;
            combo_sell_ratio_F.SelectedIndex = Properties.Settings.Default.combo_sell_ratio_F;

            CB_ik_one_A.Checked = Properties.Settings.Default.CB_ik_one_A;
            CB_ik_one_B.Checked = Properties.Settings.Default.CB_ik_one_B;
            CB_ik_one_C.Checked = Properties.Settings.Default.CB_ik_one_C;
            CB_ik_one_D.Checked = Properties.Settings.Default.CB_ik_one_D;
            CB_ik_one_E.Checked = Properties.Settings.Default.CB_ik_one_E;
            CB_ik_one_F.Checked = Properties.Settings.Default.CB_ik_one_F;
            CB_ik_one_G.Checked = Properties.Settings.Default.CB_ik_one_G;
            CB_ik_one_H.Checked = Properties.Settings.Default.CB_ik_one_H;
            CB_ik_one_I.Checked = Properties.Settings.Default.CB_ik_one_I;

            combo_ik_jumun_A.SelectedIndex = Properties.Settings.Default.combo_ik_jumun_A;
            combo_ik_jumun_B.SelectedIndex = Properties.Settings.Default.combo_ik_jumun_B;
            combo_ik_jumun_C.SelectedIndex = Properties.Settings.Default.combo_ik_jumun_C;
            combo_ik_jumun_D.SelectedIndex = Properties.Settings.Default.combo_ik_jumun_D;
            combo_ik_jumun_E.SelectedIndex = Properties.Settings.Default.combo_ik_jumun_E;
            combo_ik_jumun_F.SelectedIndex = Properties.Settings.Default.combo_ik_jumun_F;
            combo_ik_jumun_G.SelectedIndex = Properties.Settings.Default.combo_ik_jumun_G;
            combo_ik_jumun_H.SelectedIndex = Properties.Settings.Default.combo_ik_jumun_H;
            combo_ik_jumun_I.SelectedIndex = Properties.Settings.Default.combo_ik_jumun_I;

            combo_ik_down_jumun_A.SelectedIndex = Properties.Settings.Default.combo_ik_down_jumun_A;
            combo_ik_down_jumun_B.SelectedIndex = Properties.Settings.Default.combo_ik_down_jumun_B;
            combo_ik_down_jumun_C.SelectedIndex = Properties.Settings.Default.combo_ik_down_jumun_C;
            combo_ik_down_jumun_D.SelectedIndex = Properties.Settings.Default.combo_ik_down_jumun_D;
            combo_ik_down_jumun_E.SelectedIndex = Properties.Settings.Default.combo_ik_down_jumun_E;
            combo_ik_down_jumun_F.SelectedIndex = Properties.Settings.Default.combo_ik_down_jumun_F;
            combo_ik_down_jumun_G.SelectedIndex = Properties.Settings.Default.combo_ik_down_jumun_G;
            combo_ik_down_jumun_H.SelectedIndex = Properties.Settings.Default.combo_ik_down_jumun_H;
            combo_ik_down_jumun_I.SelectedIndex = Properties.Settings.Default.combo_ik_down_jumun_I;

            combo_new_jumun_A.SelectedIndex = Properties.Settings.Default.combo_new_jumun_A;
            combo_new_jumun_B.SelectedIndex = Properties.Settings.Default.combo_new_jumun_B;
            combo_new_jumun_C.SelectedIndex = Properties.Settings.Default.combo_new_jumun_C;

            CB_new_rebuy.Checked = Properties.Settings.Default.CB_new_rebuy;

            MTB_추가매수딜레이.Text = Properties.Settings.Default.MTB_추가매수딜레이.ToString();
            MTB_new_rebuytime.Text = Properties.Settings.Default.MTB_new_rebuytime.ToString();
            MTB_new_canceltime_A.Text = Properties.Settings.Default.MTB_new_canceltime_A.ToString();
            MTB_new_canceltime_B.Text = Properties.Settings.Default.MTB_new_canceltime_B.ToString();
            MTB_new_canceltime_C.Text = Properties.Settings.Default.MTB_new_canceltime_C.ToString();

            combo_sell_jumun_A.SelectedIndex = Properties.Settings.Default.combo_sell_jumun_A;
            combo_sell_jumun_B.SelectedIndex = Properties.Settings.Default.combo_sell_jumun_B;
            combo_sell_jumun_C.SelectedIndex = Properties.Settings.Default.combo_sell_jumun_C;
            combo_sell_jumun_D.SelectedIndex = Properties.Settings.Default.combo_sell_jumun_D;
            combo_sell_jumun_E.SelectedIndex = Properties.Settings.Default.combo_sell_jumun_E;
            combo_sell_jumun_F.SelectedIndex = Properties.Settings.Default.combo_sell_jumun_F;

            MTB_sell_cancel_time.Text = Properties.Settings.Default.MTB_sell_cancel_time.ToString();
            MTB_sell_cancel_repeat.Text = Properties.Settings.Default.MTB_sell_cancel_repeat.ToString();
            combo_sell_cancel_sell.SelectedIndex = Properties.Settings.Default.combo_sell_cancel_sell;

            CB_ik_down_CancelOrder.Checked = Properties.Settings.Default.CB_ik_down_CancelOrder;
            CB_ik_down_기준금.Checked = Properties.Settings.Default.CB_ik_down_기준금;

            MTB_ik_canceltime.Text = Properties.Settings.Default.MTB_ik_canceltime.ToString();

            combo_new_or_A.SelectedIndex = Properties.Settings.Default.combo_new_or_A;
            combo_new_or_B.SelectedIndex = Properties.Settings.Default.combo_new_or_B;
            combo_new_or_C.SelectedIndex = Properties.Settings.Default.combo_new_or_C;

            combo_new_cancel_buy_A.SelectedIndex = Properties.Settings.Default.combo_new_cancel_buy_A;
            combo_new_cancel_buy_B.SelectedIndex = Properties.Settings.Default.combo_new_cancel_buy_B;
            combo_new_cancel_buy_C.SelectedIndex = Properties.Settings.Default.combo_new_cancel_buy_C;

            combo_ik_cancel_sell.SelectedIndex = Properties.Settings.Default.combo_ik_cancel_sell;
            CB_sell_CancelOrder.Checked = Properties.Settings.Default.CB_sell_CancelOrder;
            CB_sell_기준금.Checked = Properties.Settings.Default.CB_sell_기준금;

            CBB_ik_CancelOrder.SelectedIndex = Properties.Settings.Default.CBB_ik_CancelOrder;
            CB_ik_기준금.Checked = Properties.Settings.Default.CB_ik_기준금;

            MTB_ik_down_canceltime.Text = Properties.Settings.Default.MTB_ik_down_canceltime.ToString();
            MTB_ik_down_repeat.Text = Properties.Settings.Default.MTB_ik_down_repeat.ToString();
            combo_ik_down_cancel_sell.SelectedIndex = Properties.Settings.Default.combo_ik_down_cancel_sell;

            CB_scalping_A.Checked = Properties.Settings.Default.CB_scalping_A;
            CB_scalping_B.Checked = Properties.Settings.Default.CB_scalping_B;
            CB_scalping_C.Checked = Properties.Settings.Default.CB_scalping_C;

            CB_scalping_A_1.Checked = Properties.Settings.Default.CB_scalping_A_1;
            CB_scalping_A_2.Checked = Properties.Settings.Default.CB_scalping_A_2;
            CB_scalping_A_3.Checked = Properties.Settings.Default.CB_scalping_A_3;
            CB_scalping_A_4.Checked = Properties.Settings.Default.CB_scalping_A_4;
            CB_scalping_A_5.Checked = Properties.Settings.Default.CB_scalping_A_5;
            CB_scalping_A_6.Checked = Properties.Settings.Default.CB_scalping_A_6;
            CB_scalping_A_7.Checked = Properties.Settings.Default.CB_scalping_A_7;
            CB_scalping_A_8.Checked = Properties.Settings.Default.CB_scalping_A_8;
            CB_scalping_A_9.Checked = Properties.Settings.Default.CB_scalping_A_9;

            CB_scalping_B_1.Checked = Properties.Settings.Default.CB_scalping_B_1;
            CB_scalping_B_2.Checked = Properties.Settings.Default.CB_scalping_B_2;
            CB_scalping_B_3.Checked = Properties.Settings.Default.CB_scalping_B_3;
            CB_scalping_B_4.Checked = Properties.Settings.Default.CB_scalping_B_4;
            CB_scalping_B_5.Checked = Properties.Settings.Default.CB_scalping_B_5;
            CB_scalping_B_6.Checked = Properties.Settings.Default.CB_scalping_B_6;
            CB_scalping_B_7.Checked = Properties.Settings.Default.CB_scalping_B_7;
            CB_scalping_B_8.Checked = Properties.Settings.Default.CB_scalping_B_8;
            CB_scalping_B_9.Checked = Properties.Settings.Default.CB_scalping_B_9;

            CB_scalping_C_1.Checked = Properties.Settings.Default.CB_scalping_C_1;
            CB_scalping_C_2.Checked = Properties.Settings.Default.CB_scalping_C_2;
            CB_scalping_C_3.Checked = Properties.Settings.Default.CB_scalping_C_3;
            CB_scalping_C_4.Checked = Properties.Settings.Default.CB_scalping_C_4;
            CB_scalping_C_5.Checked = Properties.Settings.Default.CB_scalping_C_5;
            CB_scalping_C_6.Checked = Properties.Settings.Default.CB_scalping_C_6;
            CB_scalping_C_7.Checked = Properties.Settings.Default.CB_scalping_C_7;
            CB_scalping_C_8.Checked = Properties.Settings.Default.CB_scalping_C_8;
            CB_scalping_C_9.Checked = Properties.Settings.Default.CB_scalping_C_9;

            TB_신규등락률이상.Text = Properties.Settings.Default.TB_신규등락률이상.ToString();
            TB_신규등락률이하.Text = Properties.Settings.Default.TB_신규등락률이하.ToString();
            TB_신규주가이상.Text = Properties.Settings.Default.TB_신규주가이상.ToString("N0");
            TB_신규주가이하.Text = Properties.Settings.Default.TB_신규주가이하.ToString("N0");

            TB_TS_upper_A.Text = Properties.Settings.Default.TB_TS_upper_A.ToString();
            TB_TS_upper_B.Text = Properties.Settings.Default.TB_TS_upper_B.ToString();
            TB_TS_upper_C.Text = Properties.Settings.Default.TB_TS_upper_C.ToString();
            TB_TS_upper_D.Text = Properties.Settings.Default.TB_TS_upper_D.ToString();
            TB_TS_upper_E.Text = Properties.Settings.Default.TB_TS_upper_E.ToString();
            TB_TS_upper_F.Text = Properties.Settings.Default.TB_TS_upper_F.ToString();
            TB_TS_upper_G.Text = Properties.Settings.Default.TB_TS_upper_G.ToString();
            TB_TS_upper_H.Text = Properties.Settings.Default.TB_TS_upper_H.ToString();
            TB_TS_upper_I.Text = Properties.Settings.Default.TB_TS_upper_I.ToString();

            TB_TS_down_A.Text = Properties.Settings.Default.TB_TS_down_A.ToString();
            TB_TS_down_B.Text = Properties.Settings.Default.TB_TS_down_B.ToString();
            TB_TS_down_C.Text = Properties.Settings.Default.TB_TS_down_C.ToString();
            TB_TS_down_D.Text = Properties.Settings.Default.TB_TS_down_D.ToString();
            TB_TS_down_E.Text = Properties.Settings.Default.TB_TS_down_E.ToString();
            TB_TS_down_F.Text = Properties.Settings.Default.TB_TS_down_F.ToString();
            TB_TS_down_G.Text = Properties.Settings.Default.TB_TS_down_G.ToString();
            TB_TS_down_H.Text = Properties.Settings.Default.TB_TS_down_H.ToString();
            TB_TS_down_I.Text = Properties.Settings.Default.TB_TS_down_I.ToString();

            CBB_TS_upper_A.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_A;
            CBB_TS_upper_B.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_B;
            CBB_TS_upper_C.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_C;
            CBB_TS_upper_D.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_D;
            CBB_TS_upper_E.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_E;
            CBB_TS_upper_F.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_F;
            CBB_TS_upper_G.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_G;
            CBB_TS_upper_H.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_H;
            CBB_TS_upper_I.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_I;

            CBB_TS_down_A.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_A;
            CBB_TS_down_B.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_B;
            CBB_TS_down_C.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_C;
            CBB_TS_down_D.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_D;
            CBB_TS_down_E.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_E;
            CBB_TS_down_F.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_F;
            CBB_TS_down_G.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_G;
            CBB_TS_down_H.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_H;
            CBB_TS_down_I.SelectedIndex = Properties.Settings.Default.CBB_TS_upper_I;

            CB_TS_손실제한.Checked = Properties.Settings.Default.CB_TS_손실제한;
            CB_TS_A.Checked = Properties.Settings.Default.CB_TS_A;
            CB_TS_B.Checked = Properties.Settings.Default.CB_TS_B;
            CB_TS_C.Checked = Properties.Settings.Default.CB_TS_C;
            CB_TS_D.Checked = Properties.Settings.Default.CB_TS_D;
            CB_TS_E.Checked = Properties.Settings.Default.CB_TS_E;
            CB_TS_F.Checked = Properties.Settings.Default.CB_TS_F;
            CB_TS_G.Checked = Properties.Settings.Default.CB_TS_G;
            CB_TS_H.Checked = Properties.Settings.Default.CB_TS_H;
            CB_TS_I.Checked = Properties.Settings.Default.CB_TS_I;

            TB_TS_ratio_A.Text = Properties.Settings.Default.TB_TS_ratio_A.ToString();
            TB_TS_ratio_B.Text = Properties.Settings.Default.TB_TS_ratio_B.ToString();
            TB_TS_ratio_C.Text = Properties.Settings.Default.TB_TS_ratio_C.ToString();
            TB_TS_ratio_D.Text = Properties.Settings.Default.TB_TS_ratio_D.ToString();
            TB_TS_ratio_E.Text = Properties.Settings.Default.TB_TS_ratio_E.ToString();
            TB_TS_ratio_F.Text = Properties.Settings.Default.TB_TS_ratio_F.ToString();
            TB_TS_ratio_G.Text = Properties.Settings.Default.TB_TS_ratio_G.ToString();
            TB_TS_ratio_H.Text = Properties.Settings.Default.TB_TS_ratio_H.ToString();
            TB_TS_ratio_I.Text = Properties.Settings.Default.TB_TS_ratio_I.ToString();

            TB_TS_Jumun_A.Text = Properties.Settings.Default.TB_TS_Jumun_A.ToString();
            TB_TS_Jumun_B.Text = Properties.Settings.Default.TB_TS_Jumun_B.ToString();
            TB_TS_Jumun_C.Text = Properties.Settings.Default.TB_TS_Jumun_C.ToString();
            TB_TS_Jumun_D.Text = Properties.Settings.Default.TB_TS_Jumun_D.ToString();
            TB_TS_Jumun_E.Text = Properties.Settings.Default.TB_TS_Jumun_E.ToString();
            TB_TS_Jumun_F.Text = Properties.Settings.Default.TB_TS_Jumun_F.ToString();
            TB_TS_Jumun_G.Text = Properties.Settings.Default.TB_TS_Jumun_G.ToString();
            TB_TS_Jumun_H.Text = Properties.Settings.Default.TB_TS_Jumun_H.ToString();
            TB_TS_Jumun_I.Text = Properties.Settings.Default.TB_TS_Jumun_I.ToString();

            CBB_TS_ratio_A.SelectedIndex = Properties.Settings.Default.CBB_TS_ratio_A;
            CBB_TS_ratio_B.SelectedIndex = Properties.Settings.Default.CBB_TS_ratio_B;
            CBB_TS_ratio_C.SelectedIndex = Properties.Settings.Default.CBB_TS_ratio_C;
            CBB_TS_ratio_D.SelectedIndex = Properties.Settings.Default.CBB_TS_ratio_D;
            CBB_TS_ratio_E.SelectedIndex = Properties.Settings.Default.CBB_TS_ratio_E;
            CBB_TS_ratio_F.SelectedIndex = Properties.Settings.Default.CBB_TS_ratio_F;
            CBB_TS_ratio_G.SelectedIndex = Properties.Settings.Default.CBB_TS_ratio_G;
            CBB_TS_ratio_H.SelectedIndex = Properties.Settings.Default.CBB_TS_ratio_H;
            CBB_TS_ratio_I.SelectedIndex = Properties.Settings.Default.CBB_TS_ratio_I;

            CBB_TS_Jumun_A.SelectedIndex = Properties.Settings.Default.CBB_TS_Jumun_A;
            CBB_TS_Jumun_B.SelectedIndex = Properties.Settings.Default.CBB_TS_Jumun_B;
            CBB_TS_Jumun_C.SelectedIndex = Properties.Settings.Default.CBB_TS_Jumun_C;
            CBB_TS_Jumun_D.SelectedIndex = Properties.Settings.Default.CBB_TS_Jumun_D;
            CBB_TS_Jumun_E.SelectedIndex = Properties.Settings.Default.CBB_TS_Jumun_E;
            CBB_TS_Jumun_F.SelectedIndex = Properties.Settings.Default.CBB_TS_Jumun_F;
            CBB_TS_Jumun_G.SelectedIndex = Properties.Settings.Default.CBB_TS_Jumun_G;
            CBB_TS_Jumun_H.SelectedIndex = Properties.Settings.Default.CBB_TS_Jumun_H;
            CBB_TS_Jumun_I.SelectedIndex = Properties.Settings.Default.CBB_TS_Jumun_I;

            MTB_TS_canceltime.Text = Properties.Settings.Default.MTB_TS_canceltime.ToString();
            CBB_TS_cancel_sell.SelectedIndex = Properties.Settings.Default.CBB_TS_cancel_sell;
            MTB_TS_repeat.Text = Properties.Settings.Default.MTB_TS_repeat.ToString();
            CB_TS_취소후.Checked = Properties.Settings.Default.CB_TS_취소후;
            CB_TS_기준금.Checked = Properties.Settings.Default.CB_TS_기준금;

            TB_잔고개수_신규A.Text = Properties.Settings.Default.TB_잔고개수_신규A.ToString();
            TB_잔고개수_신규B.Text = Properties.Settings.Default.TB_잔고개수_신규B.ToString();
            TB_잔고개수_신규C.Text = Properties.Settings.Default.TB_잔고개수_신규C.ToString();

            LB_잔고개수_신규A.Text = Form1.신규개수A.ToString();
            LB_잔고개수_신규B.Text = Form1.신규개수B.ToString();
            LB_잔고개수_신규C.Text = Form1.신규개수C.ToString();

            TB_Limit_New_A.Text = Properties.Settings.Default.TB_Limit_New_A.ToString();
            TB_Limit_New_B.Text = Properties.Settings.Default.TB_Limit_New_B.ToString();
            TB_Limit_New_C.Text = Properties.Settings.Default.TB_Limit_New_C.ToString();

            CB_익절재매수A.Checked = Properties.Settings.Default.CB_익절재매수A;
            CB_익절재매수B.Checked = Properties.Settings.Default.CB_익절재매수B;
            CB_익절재매수C.Checked = Properties.Settings.Default.CB_익절재매수C;

            CB_TimeSell_기준금.Checked = Properties.Settings.Default.CB_TimeSell_기준금;

            if (Form1.로딩완료)
            {
                if (!Properties.Settings.Default.CB_가이드매매 || Properties.Settings.Default.CB_기본매매변경)
                {
                    Form_Basic.form.combo_new_or_A.Enabled = true;
                    Form_Basic.form.combo_new_or_B.Enabled = true;
                    Form_Basic.form.combo_new_or_C.Enabled = true;
                    Form_Basic.form.combo_new_condition_A.Enabled = true;
                    Form_Basic.form.combo_new_condition_B.Enabled = true;
                    Form_Basic.form.combo_new_condition_C.Enabled = true;
                }
            }

            if (CBB_TS_cancel_sell.SelectedIndex == -1) CBB_TS_cancel_sell.SelectedIndex = 0;
            if (CBB_TS_upper_A.SelectedIndex == -1) CBB_TS_upper_A.SelectedIndex = 0; if (CBB_TS_down_A.SelectedIndex == -1) CBB_TS_down_A.SelectedIndex = 0; if (CBB_TS_ratio_A.SelectedIndex == -1) CBB_TS_ratio_A.SelectedIndex = 0; if (CBB_TS_Jumun_A.SelectedIndex == -1) CBB_TS_Jumun_A.SelectedIndex = 1;
            if (CBB_TS_upper_B.SelectedIndex == -1) CBB_TS_upper_B.SelectedIndex = 0; if (CBB_TS_down_B.SelectedIndex == -1) CBB_TS_down_B.SelectedIndex = 0; if (CBB_TS_ratio_B.SelectedIndex == -1) CBB_TS_ratio_B.SelectedIndex = 0; if (CBB_TS_Jumun_B.SelectedIndex == -1) CBB_TS_Jumun_B.SelectedIndex = 1;
            if (CBB_TS_upper_C.SelectedIndex == -1) CBB_TS_upper_C.SelectedIndex = 0; if (CBB_TS_down_C.SelectedIndex == -1) CBB_TS_down_C.SelectedIndex = 0; if (CBB_TS_ratio_C.SelectedIndex == -1) CBB_TS_ratio_C.SelectedIndex = 0; if (CBB_TS_Jumun_C.SelectedIndex == -1) CBB_TS_Jumun_C.SelectedIndex = 1;
            if (CBB_TS_upper_D.SelectedIndex == -1) CBB_TS_upper_D.SelectedIndex = 0; if (CBB_TS_down_D.SelectedIndex == -1) CBB_TS_down_D.SelectedIndex = 0; if (CBB_TS_ratio_D.SelectedIndex == -1) CBB_TS_ratio_D.SelectedIndex = 0; if (CBB_TS_Jumun_D.SelectedIndex == -1) CBB_TS_Jumun_D.SelectedIndex = 1;
            if (CBB_TS_upper_E.SelectedIndex == -1) CBB_TS_upper_E.SelectedIndex = 0; if (CBB_TS_down_E.SelectedIndex == -1) CBB_TS_down_E.SelectedIndex = 0; if (CBB_TS_ratio_E.SelectedIndex == -1) CBB_TS_ratio_E.SelectedIndex = 0; if (CBB_TS_Jumun_E.SelectedIndex == -1) CBB_TS_Jumun_E.SelectedIndex = 1;
            if (CBB_TS_upper_F.SelectedIndex == -1) CBB_TS_upper_F.SelectedIndex = 0; if (CBB_TS_down_F.SelectedIndex == -1) CBB_TS_down_F.SelectedIndex = 0; if (CBB_TS_ratio_F.SelectedIndex == -1) CBB_TS_ratio_F.SelectedIndex = 0; if (CBB_TS_Jumun_F.SelectedIndex == -1) CBB_TS_Jumun_F.SelectedIndex = 1;
            if (CBB_TS_upper_G.SelectedIndex == -1) CBB_TS_upper_G.SelectedIndex = 0; if (CBB_TS_down_G.SelectedIndex == -1) CBB_TS_down_G.SelectedIndex = 0; if (CBB_TS_ratio_G.SelectedIndex == -1) CBB_TS_ratio_G.SelectedIndex = 0; if (CBB_TS_Jumun_G.SelectedIndex == -1) CBB_TS_Jumun_G.SelectedIndex = 1;
            if (CBB_TS_upper_H.SelectedIndex == -1) CBB_TS_upper_H.SelectedIndex = 0; if (CBB_TS_down_H.SelectedIndex == -1) CBB_TS_down_H.SelectedIndex = 0; if (CBB_TS_ratio_H.SelectedIndex == -1) CBB_TS_ratio_H.SelectedIndex = 0; if (CBB_TS_Jumun_H.SelectedIndex == -1) CBB_TS_Jumun_H.SelectedIndex = 1;
            if (CBB_TS_upper_I.SelectedIndex == -1) CBB_TS_upper_I.SelectedIndex = 0; if (CBB_TS_down_I.SelectedIndex == -1) CBB_TS_down_I.SelectedIndex = 0; if (CBB_TS_ratio_I.SelectedIndex == -1) CBB_TS_ratio_I.SelectedIndex = 0; if (CBB_TS_Jumun_I.SelectedIndex == -1) CBB_TS_Jumun_I.SelectedIndex = 1;


            FormPrint.CBB_suik_DropDownClosed(CBB_TimeSell_수익구분_A);
            FormPrint.CBB_suik_DropDownClosed(CBB_TimeSell_수익구분_B);
            FormPrint.CBB_suik_DropDownClosed(CBB_TimeSell_수익구분_C);
            this.ActiveControl = combo_new_cancel_buy_A;
            Form1.음소거 = Properties.Settings.Default.CB_음소거;

            if (Properties.Settings.Default.CB_가이드매매) ControllerDisable.Form_Basic_Disable();
        }

        public static void 기본매매_save()  // 기본설정 저장
        {
            Dictionary<string, Stockbalance> stockBalanceList = Form1.stockBalanceList;   // 잔고 - 보유잔고리스트

            ComboBox combo_jumun = form.combo_new_jumun_A;
            ComboBox combo_cancel = form.combo_new_cancel_buy_A;
            MaskedTextBox MTB = form.MTB_new_repeat_A;
            TextBox TB = form.TB_new_value_A;
            값변경();

            combo_jumun = form.combo_new_jumun_B;
            combo_cancel = form.combo_new_cancel_buy_B;
            MTB = form.MTB_new_repeat_B;
            TB = form.TB_new_value_B;
            값변경();

            combo_jumun = form.combo_new_jumun_C;
            combo_cancel = form.combo_new_cancel_buy_C;
            MTB = form.MTB_new_repeat_C;
            TB = form.TB_new_value_C;
            값변경();

            void 값변경()
            {
                if (combo_jumun.SelectedIndex > 3)
                {
                    combo_cancel.SelectedIndex = 0;
                    MTB.Text = "0";
                    TB.Text = "0";
                }

                if (combo_jumun.SelectedIndex < 2)
                {
                    TB.Text = "0";
                }
            }

            void 저장에러()
            {
                Console.WriteLine("기본설정 저장 에러 ");
                Form1.Error_Log("기본설정 저장 에러 ");
            }

            //    << ---신규매수
            try
            {
                int.TryParse(form.MTB_new_repeat_A.Text, out int new_repeat_A);
                int.TryParse(form.MTB_new_repeat_B.Text, out int new_repeat_B);
                int.TryParse(form.MTB_new_repeat_C.Text, out int new_repeat_C);

                int.TryParse(form.MTB_new_delay_A.Text, out int TB_new_delay_A);
                int.TryParse(form.MTB_new_delay_B.Text, out int TB_new_delay_B);
                int.TryParse(form.MTB_new_delay_C.Text, out int TB_new_delay_C);

                int.TryParse(form.MTB_new_canceltime_A.Text, out int time_A);
                int.TryParse(form.MTB_new_canceltime_B.Text, out int time_B);
                int.TryParse(form.MTB_new_canceltime_C.Text, out int time_C);

                if (time_A < 10) time_A = 60;
                if (time_B < 10) time_B = 60;
                if (time_C < 10) time_C = 60;

                if (form.combo_new_cancel_buy_A.SelectedIndex < 3) new_repeat_A = 0;
                if (form.combo_new_cancel_buy_B.SelectedIndex < 3) new_repeat_B = 0;
                if (form.combo_new_cancel_buy_C.SelectedIndex < 3) new_repeat_C = 0;

                Properties.Settings.Default.MTB_new_repeat_A = new_repeat_A;
                Properties.Settings.Default.MTB_new_repeat_B = new_repeat_B;
                Properties.Settings.Default.MTB_new_repeat_C = new_repeat_C;

                Properties.Settings.Default.MTB_new_delay_A = TB_new_delay_A;
                Properties.Settings.Default.MTB_new_delay_B = TB_new_delay_B;
                Properties.Settings.Default.MTB_new_delay_C = TB_new_delay_C;

                Properties.Settings.Default.MTB_new_canceltime_A = time_A;
                Properties.Settings.Default.MTB_new_canceltime_B = time_B;
                Properties.Settings.Default.MTB_new_canceltime_C = time_C;

                form.MTB_new_canceltime_A.Text = time_A.ToString();
                form.MTB_new_canceltime_B.Text = time_B.ToString();
                form.MTB_new_canceltime_C.Text = time_C.ToString();

                form.MTB_new_repeat_A.Text = new_repeat_A.ToString();
                form.MTB_new_repeat_B.Text = new_repeat_B.ToString();
                form.MTB_new_repeat_C.Text = new_repeat_C.ToString();

                form.MTB_new_delay_A.Text = TB_new_delay_A.ToString();
                form.MTB_new_delay_B.Text = TB_new_delay_B.ToString();
                form.MTB_new_delay_C.Text = TB_new_delay_C.ToString();
            }
            catch
            {
                저장에러();
            }

            try
            {
                int.TryParse(form.MT_new_start_A.Text, out int _new_start_A);
                int.TryParse(form.MT_new_start_B.Text, out int _new_start_B);
                int.TryParse(form.MT_new_start_C.Text, out int _new_start_C);

                int new_start_A = GET.start_stop_time(true, _new_start_A);
                int new_start_B = GET.start_stop_time(true, _new_start_B);
                int new_start_C = GET.start_stop_time(true, _new_start_C);

                Properties.Settings.Default.MT_new_start_A = new_start_A;
                Properties.Settings.Default.MT_new_start_B = new_start_B;
                Properties.Settings.Default.MT_new_start_C = new_start_C;
                form.MT_new_start_A.Text = new_start_A.ToString();
                form.MT_new_start_B.Text = new_start_B.ToString();
                form.MT_new_start_C.Text = new_start_C.ToString();

                int.TryParse(form.MT_new_end_A.Text, out int _new_end_A);
                int.TryParse(form.MT_new_end_B.Text, out int _new_end_B);
                int.TryParse(form.MT_new_end_C.Text, out int _new_end_C);

                int new_end_A = GET.start_stop_time(false, _new_end_A);
                int new_end_B = GET.start_stop_time(false, _new_end_B);
                int new_end_C = GET.start_stop_time(false, _new_end_C);

                Properties.Settings.Default.MT_new_end_A = new_end_A;
                Properties.Settings.Default.MT_new_end_B = new_end_B;
                Properties.Settings.Default.MT_new_end_C = new_end_C;
                form.MT_new_end_A.Text = new_end_A.ToString();
                form.MT_new_end_B.Text = new_end_B.ToString();
                form.MT_new_end_C.Text = new_end_C.ToString();

                if (Properties.Settings.Default.MT_new_start_A >= Form1.timenow) Form1.신규매수신호_A = true;
                if (Properties.Settings.Default.MT_new_start_B >= Form1.timenow) Form1.신규매수신호_B = true;
                if (Properties.Settings.Default.MT_new_start_C >= Form1.timenow) Form1.신규매수신호_C = true;

                double.TryParse(form.MT_new_ratio_A.Text, out double new_Ratio_A);
                double.TryParse(form.MT_new_ratio_B.Text, out double new_Ratio_B);
                double.TryParse(form.MT_new_ratio_C.Text, out double new_Ratio_C);
                double.TryParse(form.TB_new_value_A.Text, out double new_value_A);
                double.TryParse(form.TB_new_value_B.Text, out double new_value_B);
                double.TryParse(form.TB_new_value_C.Text, out double new_value_C);

                if (new_Ratio_A == 0) new_Ratio_A = 1;
                if (new_Ratio_B == 0) new_Ratio_B = 1;
                if (new_Ratio_C == 0) new_Ratio_C = 1;

                Properties.Settings.Default.MT_new_ratio_A = Math.Abs(new_Ratio_A);
                Properties.Settings.Default.MT_new_ratio_B = Math.Abs(new_Ratio_B);
                Properties.Settings.Default.MT_new_ratio_C = Math.Abs(new_Ratio_C);
                Properties.Settings.Default.TB_new_value_A = new_value_A;
                Properties.Settings.Default.TB_new_value_B = new_value_B;
                Properties.Settings.Default.TB_new_value_C = new_value_C;

                form.MT_new_ratio_A.Text = Properties.Settings.Default.MT_new_ratio_A.ToString();
                form.MT_new_ratio_B.Text = Properties.Settings.Default.MT_new_ratio_B.ToString();
                form.MT_new_ratio_C.Text = Properties.Settings.Default.MT_new_ratio_C.ToString();
                form.TB_new_value_A.Text = Properties.Settings.Default.TB_new_value_A.ToString();
                form.TB_new_value_B.Text = Properties.Settings.Default.TB_new_value_B.ToString();
                form.TB_new_value_C.Text = Properties.Settings.Default.TB_new_value_C.ToString();
            }
            catch
            {
                저장에러();
            }

            Properties.Settings.Default.combo_new_or_A = form.combo_new_or_A.SelectedIndex;
            Properties.Settings.Default.combo_new_or_B = form.combo_new_or_B.SelectedIndex;
            Properties.Settings.Default.combo_new_or_C = form.combo_new_or_C.SelectedIndex;

            Properties.Settings.Default.combo_new_choice_A = form.combo_new_choice_A.SelectedIndex;
            Properties.Settings.Default.combo_new_choice_B = form.combo_new_choice_B.SelectedIndex;
            Properties.Settings.Default.combo_new_choice_C = form.combo_new_choice_C.SelectedIndex;

            Properties.Settings.Default.combo_new_jumun_A = form.combo_new_jumun_A.SelectedIndex;
            Properties.Settings.Default.combo_new_jumun_B = form.combo_new_jumun_B.SelectedIndex;
            Properties.Settings.Default.combo_new_jumun_C = form.combo_new_jumun_C.SelectedIndex;

            Properties.Settings.Default.combo_ik_jumun_A = form.combo_ik_jumun_A.SelectedIndex;
            Properties.Settings.Default.combo_ik_jumun_B = form.combo_ik_jumun_B.SelectedIndex;
            Properties.Settings.Default.combo_ik_jumun_C = form.combo_ik_jumun_C.SelectedIndex;
            Properties.Settings.Default.combo_ik_jumun_D = form.combo_ik_jumun_D.SelectedIndex;
            Properties.Settings.Default.combo_ik_jumun_E = form.combo_ik_jumun_E.SelectedIndex;
            Properties.Settings.Default.combo_ik_jumun_F = form.combo_ik_jumun_F.SelectedIndex;
            Properties.Settings.Default.combo_ik_jumun_G = form.combo_ik_jumun_G.SelectedIndex;
            Properties.Settings.Default.combo_ik_jumun_H = form.combo_ik_jumun_H.SelectedIndex;
            Properties.Settings.Default.combo_ik_jumun_I = form.combo_ik_jumun_I.SelectedIndex;


            //<-- 익절 저장
            try
            {

                int.TryParse(form.MTB_ik_canceltime.Text, out int ik_canceltime);
                if (ik_canceltime < 10) ik_canceltime = 60;
                int.TryParse(form.MTB_ik_repeat.Text, out int TB_ik_repeat);

                if (form.combo_ik_cancel_sell.SelectedIndex < 3) TB_ik_repeat = 0;

                Properties.Settings.Default.MTB_ik_canceltime = ik_canceltime;
                Properties.Settings.Default.MTB_ik_repeat = TB_ik_repeat;

                form.MTB_ik_repeat.Text = TB_ik_repeat.ToString();
                form.MTB_ik_canceltime.Text = ik_canceltime.ToString();

                Properties.Settings.Default.combo_new_cancel_buy_A = form.combo_new_cancel_buy_A.SelectedIndex;
                Properties.Settings.Default.combo_new_cancel_buy_B = form.combo_new_cancel_buy_B.SelectedIndex;
                Properties.Settings.Default.combo_new_cancel_buy_B = form.combo_new_cancel_buy_C.SelectedIndex;
                Properties.Settings.Default.combo_ik_cancel_sell = form.combo_ik_cancel_sell.SelectedIndex;
            }
            catch
            {
                저장에러();
            }

            try
            {
                double.TryParse(form.TB_ik_son_A.Text, out double ik_son_A);
                double.TryParse(form.TB_ik_son_B.Text, out double ik_son_B);
                double.TryParse(form.TB_ik_son_C.Text, out double ik_son_C);
                double.TryParse(form.TB_ik_son_D.Text, out double ik_son_D);
                double.TryParse(form.TB_ik_son_E.Text, out double ik_son_E);
                double.TryParse(form.TB_ik_son_F.Text, out double ik_son_F);
                double.TryParse(form.TB_ik_son_G.Text, out double ik_son_G);
                double.TryParse(form.TB_ik_son_H.Text, out double ik_son_H);
                double.TryParse(form.TB_ik_son_I.Text, out double ik_son_I);

                Properties.Settings.Default.TB_ik_son_A = Math.Abs(ik_son_A);
                Properties.Settings.Default.TB_ik_son_B = Math.Abs(ik_son_B);
                Properties.Settings.Default.TB_ik_son_C = Math.Abs(ik_son_C);
                Properties.Settings.Default.TB_ik_son_D = Math.Abs(ik_son_D);
                Properties.Settings.Default.TB_ik_son_E = Math.Abs(ik_son_E);
                Properties.Settings.Default.TB_ik_son_F = Math.Abs(ik_son_F);
                Properties.Settings.Default.TB_ik_son_G = Math.Abs(ik_son_G);
                Properties.Settings.Default.TB_ik_son_H = Math.Abs(ik_son_H);
                Properties.Settings.Default.TB_ik_son_I = Math.Abs(ik_son_I);

                form.TB_ik_son_A.Text = Properties.Settings.Default.TB_ik_son_A.ToString();
                form.TB_ik_son_B.Text = Properties.Settings.Default.TB_ik_son_B.ToString();
                form.TB_ik_son_C.Text = Properties.Settings.Default.TB_ik_son_C.ToString();
                form.TB_ik_son_D.Text = Properties.Settings.Default.TB_ik_son_D.ToString();
                form.TB_ik_son_E.Text = Properties.Settings.Default.TB_ik_son_E.ToString();
                form.TB_ik_son_F.Text = Properties.Settings.Default.TB_ik_son_F.ToString();
                form.TB_ik_son_G.Text = Properties.Settings.Default.TB_ik_son_G.ToString();
                form.TB_ik_son_H.Text = Properties.Settings.Default.TB_ik_son_H.ToString();
                form.TB_ik_son_I.Text = Properties.Settings.Default.TB_ik_son_I.ToString();

            }
            catch
            {
                저장에러();
            }

            try
            {
                double.TryParse(form.TB_ik_value_A.Text, out double ik_value_A);
                double.TryParse(form.TB_ik_value_B.Text, out double ik_value_B);
                double.TryParse(form.TB_ik_value_C.Text, out double ik_value_C);
                double.TryParse(form.TB_ik_value_D.Text, out double ik_value_D);
                double.TryParse(form.TB_ik_value_E.Text, out double ik_value_E);
                double.TryParse(form.TB_ik_value_F.Text, out double ik_value_F);
                double.TryParse(form.TB_ik_value_G.Text, out double ik_value_G);
                double.TryParse(form.TB_ik_value_H.Text, out double ik_value_H);
                double.TryParse(form.TB_ik_value_I.Text, out double ik_value_I);

                Properties.Settings.Default.TB_ik_value_A = ik_value_A;
                Properties.Settings.Default.TB_ik_value_B = ik_value_B;
                Properties.Settings.Default.TB_ik_value_C = ik_value_C;
                Properties.Settings.Default.TB_ik_value_D = ik_value_D;
                Properties.Settings.Default.TB_ik_value_E = ik_value_E;
                Properties.Settings.Default.TB_ik_value_F = ik_value_F;
                Properties.Settings.Default.TB_ik_value_G = ik_value_G;
                Properties.Settings.Default.TB_ik_value_H = ik_value_H;
                Properties.Settings.Default.TB_ik_value_I = ik_value_I;

                form.TB_ik_value_A.Text = ik_value_A.ToString();
                form.TB_ik_value_B.Text = ik_value_B.ToString();
                form.TB_ik_value_C.Text = ik_value_C.ToString();
                form.TB_ik_value_D.Text = ik_value_D.ToString();
                form.TB_ik_value_E.Text = ik_value_E.ToString();
                form.TB_ik_value_F.Text = ik_value_F.ToString();
                form.TB_ik_value_G.Text = ik_value_G.ToString();
                form.TB_ik_value_H.Text = ik_value_H.ToString();
                form.TB_ik_value_I.Text = ik_value_I.ToString();

                double.TryParse(form.TB_ik_son_ratio_A.Text, out double ik_son_ratio_A);
                double.TryParse(form.TB_ik_son_ratio_B.Text, out double ik_son_ratio_B);
                double.TryParse(form.TB_ik_son_ratio_C.Text, out double ik_son_ratio_C);
                double.TryParse(form.TB_ik_son_ratio_D.Text, out double ik_son_ratio_D);
                double.TryParse(form.TB_ik_son_ratio_E.Text, out double ik_son_ratio_E);
                double.TryParse(form.TB_ik_son_ratio_F.Text, out double ik_son_ratio_F);
                double.TryParse(form.TB_ik_son_ratio_G.Text, out double ik_son_ratio_G);
                double.TryParse(form.TB_ik_son_ratio_H.Text, out double ik_son_ratio_H);
                double.TryParse(form.TB_ik_son_ratio_I.Text, out double ik_son_ratio_I);

                if (ik_son_ratio_A == 0) ik_son_ratio_A = 1;
                if (ik_son_ratio_B == 0) ik_son_ratio_B = 1;
                if (ik_son_ratio_C == 0) ik_son_ratio_C = 1;
                if (ik_son_ratio_D == 0) ik_son_ratio_D = 1;
                if (ik_son_ratio_E == 0) ik_son_ratio_E = 1;
                if (ik_son_ratio_F == 0) ik_son_ratio_F = 1;
                if (ik_son_ratio_G == 0) ik_son_ratio_G = 1;
                if (ik_son_ratio_H == 0) ik_son_ratio_H = 1;
                if (ik_son_ratio_I == 0) ik_son_ratio_I = 1;


                Properties.Settings.Default.TB_ik_son_ratio_A = Math.Abs(ik_son_ratio_A);
                Properties.Settings.Default.TB_ik_son_ratio_B = Math.Abs(ik_son_ratio_B);
                Properties.Settings.Default.TB_ik_son_ratio_C = Math.Abs(ik_son_ratio_C);
                Properties.Settings.Default.TB_ik_son_ratio_D = Math.Abs(ik_son_ratio_D);
                Properties.Settings.Default.TB_ik_son_ratio_E = Math.Abs(ik_son_ratio_E);
                Properties.Settings.Default.TB_ik_son_ratio_F = Math.Abs(ik_son_ratio_F);
                Properties.Settings.Default.TB_ik_son_ratio_G = Math.Abs(ik_son_ratio_G);
                Properties.Settings.Default.TB_ik_son_ratio_H = Math.Abs(ik_son_ratio_H);
                Properties.Settings.Default.TB_ik_son_ratio_I = Math.Abs(ik_son_ratio_I);

                form.TB_ik_son_ratio_A.Text = Properties.Settings.Default.TB_ik_son_ratio_A.ToString();
                form.TB_ik_son_ratio_B.Text = Properties.Settings.Default.TB_ik_son_ratio_B.ToString();
                form.TB_ik_son_ratio_C.Text = Properties.Settings.Default.TB_ik_son_ratio_C.ToString();
                form.TB_ik_son_ratio_D.Text = Properties.Settings.Default.TB_ik_son_ratio_D.ToString();
                form.TB_ik_son_ratio_E.Text = Properties.Settings.Default.TB_ik_son_ratio_E.ToString();
                form.TB_ik_son_ratio_F.Text = Properties.Settings.Default.TB_ik_son_ratio_F.ToString();
                form.TB_ik_son_ratio_G.Text = Properties.Settings.Default.TB_ik_son_ratio_G.ToString();
                form.TB_ik_son_ratio_H.Text = Properties.Settings.Default.TB_ik_son_ratio_H.ToString();
                form.TB_ik_son_ratio_I.Text = Properties.Settings.Default.TB_ik_son_ratio_I.ToString();

                Properties.Settings.Default.combo_ik_A = form.combo_ik_A.SelectedIndex;
                Properties.Settings.Default.combo_ik_B = form.combo_ik_B.SelectedIndex;
                Properties.Settings.Default.combo_ik_C = form.combo_ik_C.SelectedIndex;
                Properties.Settings.Default.combo_ik_D = form.combo_ik_D.SelectedIndex;
                Properties.Settings.Default.combo_ik_E = form.combo_ik_E.SelectedIndex;
                Properties.Settings.Default.combo_ik_F = form.combo_ik_F.SelectedIndex;
                Properties.Settings.Default.combo_ik_G = form.combo_ik_G.SelectedIndex;
                Properties.Settings.Default.combo_ik_H = form.combo_ik_H.SelectedIndex;
                Properties.Settings.Default.combo_ik_I = form.combo_ik_I.SelectedIndex;

                Properties.Settings.Default.combo_ik_ratio_A = form.combo_ik_ratio_A.SelectedIndex;
                Properties.Settings.Default.combo_ik_ratio_B = form.combo_ik_ratio_B.SelectedIndex;
                Properties.Settings.Default.combo_ik_ratio_C = form.combo_ik_ratio_C.SelectedIndex;
                Properties.Settings.Default.combo_ik_ratio_D = form.combo_ik_ratio_D.SelectedIndex;
                Properties.Settings.Default.combo_ik_ratio_E = form.combo_ik_ratio_E.SelectedIndex;
                Properties.Settings.Default.combo_ik_ratio_F = form.combo_ik_ratio_F.SelectedIndex;
                Properties.Settings.Default.combo_ik_ratio_G = form.combo_ik_ratio_G.SelectedIndex;
                Properties.Settings.Default.combo_ik_ratio_H = form.combo_ik_ratio_H.SelectedIndex;
                Properties.Settings.Default.combo_ik_ratio_I = form.combo_ik_ratio_I.SelectedIndex;
            }
            catch
            {
                저장에러();
            }

            try
            {
                int.TryParse(form.MTB_ik_down_canceltime.Text, out int time);
                if (time < 10) time = 60;
                int.TryParse(form.MTB_ik_down_repeat.Text, out int ik_down_repeat);

                if (form.combo_ik_down_cancel_sell.SelectedIndex < 3) ik_down_repeat = 0;

                Properties.Settings.Default.MTB_ik_down_canceltime = time;
                Properties.Settings.Default.MTB_ik_down_repeat = ik_down_repeat;

                form.MTB_ik_down_canceltime.Text = time.ToString();
                form.MTB_ik_down_repeat.Text = ik_down_repeat.ToString();

                double.TryParse(form.TB_ik_down_A.Text, out double ik_down_A);
                double.TryParse(form.TB_ik_down_B.Text, out double ik_down_B);
                double.TryParse(form.TB_ik_down_C.Text, out double ik_down_C);
                double.TryParse(form.TB_ik_down_D.Text, out double ik_down_D);
                double.TryParse(form.TB_ik_down_E.Text, out double ik_down_E);
                double.TryParse(form.TB_ik_down_F.Text, out double ik_down_F);
                double.TryParse(form.TB_ik_down_G.Text, out double ik_down_G);
                double.TryParse(form.TB_ik_down_H.Text, out double ik_down_H);
                double.TryParse(form.TB_ik_down_I.Text, out double ik_down_I);

                Properties.Settings.Default.TB_ik_down_A = Math.Abs(ik_down_A);
                Properties.Settings.Default.TB_ik_down_B = Math.Abs(ik_down_B);
                Properties.Settings.Default.TB_ik_down_C = Math.Abs(ik_down_C);
                Properties.Settings.Default.TB_ik_down_D = Math.Abs(ik_down_D);
                Properties.Settings.Default.TB_ik_down_E = Math.Abs(ik_down_E);
                Properties.Settings.Default.TB_ik_down_F = Math.Abs(ik_down_F);
                Properties.Settings.Default.TB_ik_down_G = Math.Abs(ik_down_G);
                Properties.Settings.Default.TB_ik_down_H = Math.Abs(ik_down_H);
                Properties.Settings.Default.TB_ik_down_I = Math.Abs(ik_down_I);

                form.TB_ik_down_A.Text = Properties.Settings.Default.TB_ik_down_A.ToString();
                form.TB_ik_down_B.Text = Properties.Settings.Default.TB_ik_down_B.ToString();
                form.TB_ik_down_C.Text = Properties.Settings.Default.TB_ik_down_C.ToString();
                form.TB_ik_down_D.Text = Properties.Settings.Default.TB_ik_down_D.ToString();
                form.TB_ik_down_E.Text = Properties.Settings.Default.TB_ik_down_E.ToString();
                form.TB_ik_down_F.Text = Properties.Settings.Default.TB_ik_down_F.ToString();
                form.TB_ik_down_G.Text = Properties.Settings.Default.TB_ik_down_G.ToString();
                form.TB_ik_down_H.Text = Properties.Settings.Default.TB_ik_down_H.ToString();
                form.TB_ik_down_I.Text = Properties.Settings.Default.TB_ik_down_I.ToString();

                double.TryParse(form.TB_ik_down_ratio_A.Text, out double ik_down_ratio_A);
                double.TryParse(form.TB_ik_down_ratio_B.Text, out double ik_down_ratio_B);
                double.TryParse(form.TB_ik_down_ratio_C.Text, out double ik_down_ratio_C);
                double.TryParse(form.TB_ik_down_ratio_D.Text, out double ik_down_ratio_D);
                double.TryParse(form.TB_ik_down_ratio_E.Text, out double ik_down_ratio_E);
                double.TryParse(form.TB_ik_down_ratio_F.Text, out double ik_down_ratio_F);
                double.TryParse(form.TB_ik_down_ratio_G.Text, out double ik_down_ratio_G);
                double.TryParse(form.TB_ik_down_ratio_H.Text, out double ik_down_ratio_H);
                double.TryParse(form.TB_ik_down_ratio_I.Text, out double ik_down_ratio_I);

                if (ik_down_ratio_A == 0) ik_down_ratio_A = 1;
                if (ik_down_ratio_B == 0) ik_down_ratio_B = 1;
                if (ik_down_ratio_C == 0) ik_down_ratio_C = 1;
                if (ik_down_ratio_D == 0) ik_down_ratio_D = 1;
                if (ik_down_ratio_E == 0) ik_down_ratio_E = 1;
                if (ik_down_ratio_F == 0) ik_down_ratio_F = 1;
                if (ik_down_ratio_G == 0) ik_down_ratio_G = 1;
                if (ik_down_ratio_H == 0) ik_down_ratio_H = 1;
                if (ik_down_ratio_I == 0) ik_down_ratio_I = 1;

                Properties.Settings.Default.TB_ik_down_ratio_A = Math.Abs(ik_down_ratio_A);
                Properties.Settings.Default.TB_ik_down_ratio_B = Math.Abs(ik_down_ratio_B);
                Properties.Settings.Default.TB_ik_down_ratio_C = Math.Abs(ik_down_ratio_C);
                Properties.Settings.Default.TB_ik_down_ratio_D = Math.Abs(ik_down_ratio_D);
                Properties.Settings.Default.TB_ik_down_ratio_E = Math.Abs(ik_down_ratio_E);
                Properties.Settings.Default.TB_ik_down_ratio_F = Math.Abs(ik_down_ratio_F);
                Properties.Settings.Default.TB_ik_down_ratio_G = Math.Abs(ik_down_ratio_G);
                Properties.Settings.Default.TB_ik_down_ratio_H = Math.Abs(ik_down_ratio_H);
                Properties.Settings.Default.TB_ik_down_ratio_I = Math.Abs(ik_down_ratio_I);

                form.TB_ik_down_ratio_A.Text = Properties.Settings.Default.TB_ik_down_ratio_A.ToString();
                form.TB_ik_down_ratio_B.Text = Properties.Settings.Default.TB_ik_down_ratio_B.ToString();
                form.TB_ik_down_ratio_C.Text = Properties.Settings.Default.TB_ik_down_ratio_C.ToString();
                form.TB_ik_down_ratio_D.Text = Properties.Settings.Default.TB_ik_down_ratio_D.ToString();
                form.TB_ik_down_ratio_E.Text = Properties.Settings.Default.TB_ik_down_ratio_E.ToString();
                form.TB_ik_down_ratio_F.Text = Properties.Settings.Default.TB_ik_down_ratio_F.ToString();
                form.TB_ik_down_ratio_G.Text = Properties.Settings.Default.TB_ik_down_ratio_G.ToString();
                form.TB_ik_down_ratio_H.Text = Properties.Settings.Default.TB_ik_down_ratio_H.ToString();
                form.TB_ik_down_ratio_I.Text = Properties.Settings.Default.TB_ik_down_ratio_I.ToString();

                double.TryParse(form.TB_ik_down_value_A.Text, out double ik_down_value_A);
                double.TryParse(form.TB_ik_down_value_B.Text, out double ik_down_value_B);
                double.TryParse(form.TB_ik_down_value_C.Text, out double ik_down_value_C);
                double.TryParse(form.TB_ik_down_value_D.Text, out double ik_down_value_D);
                double.TryParse(form.TB_ik_down_value_E.Text, out double ik_down_value_E);
                double.TryParse(form.TB_ik_down_value_F.Text, out double ik_down_value_F);
                double.TryParse(form.TB_ik_down_value_G.Text, out double ik_down_value_G);
                double.TryParse(form.TB_ik_down_value_H.Text, out double ik_down_value_H);
                double.TryParse(form.TB_ik_down_value_I.Text, out double ik_down_value_I);

                if (form.combo_ik_down_jumun_A.SelectedIndex == 0 || form.combo_ik_down_jumun_A.SelectedIndex == 1) ik_down_value_A = 0;
                if (form.combo_ik_down_jumun_B.SelectedIndex == 0 || form.combo_ik_down_jumun_B.SelectedIndex == 1) ik_down_value_B = 0;
                if (form.combo_ik_down_jumun_C.SelectedIndex == 0 || form.combo_ik_down_jumun_C.SelectedIndex == 1) ik_down_value_C = 0;
                if (form.combo_ik_down_jumun_D.SelectedIndex == 0 || form.combo_ik_down_jumun_D.SelectedIndex == 1) ik_down_value_D = 0;
                if (form.combo_ik_down_jumun_E.SelectedIndex == 0 || form.combo_ik_down_jumun_E.SelectedIndex == 1) ik_down_value_E = 0;
                if (form.combo_ik_down_jumun_F.SelectedIndex == 0 || form.combo_ik_down_jumun_F.SelectedIndex == 1) ik_down_value_F = 0;
                if (form.combo_ik_down_jumun_G.SelectedIndex == 0 || form.combo_ik_down_jumun_G.SelectedIndex == 1) ik_down_value_G = 0;
                if (form.combo_ik_down_jumun_H.SelectedIndex == 0 || form.combo_ik_down_jumun_H.SelectedIndex == 1) ik_down_value_H = 0;
                if (form.combo_ik_down_jumun_I.SelectedIndex == 0 || form.combo_ik_down_jumun_I.SelectedIndex == 1) ik_down_value_I = 0;

                Properties.Settings.Default.TB_ik_down_value_A = ik_down_value_A;
                Properties.Settings.Default.TB_ik_down_value_B = ik_down_value_B;
                Properties.Settings.Default.TB_ik_down_value_C = ik_down_value_C;
                Properties.Settings.Default.TB_ik_down_value_D = ik_down_value_D;
                Properties.Settings.Default.TB_ik_down_value_E = ik_down_value_E;
                Properties.Settings.Default.TB_ik_down_value_F = ik_down_value_F;
                Properties.Settings.Default.TB_ik_down_value_G = ik_down_value_G;
                Properties.Settings.Default.TB_ik_down_value_H = ik_down_value_H;
                Properties.Settings.Default.TB_ik_down_value_I = ik_down_value_I;

                form.TB_ik_down_value_A.Text = ik_down_value_A.ToString();
                form.TB_ik_down_value_B.Text = ik_down_value_B.ToString();
                form.TB_ik_down_value_C.Text = ik_down_value_C.ToString();
                form.TB_ik_down_value_D.Text = ik_down_value_D.ToString();
                form.TB_ik_down_value_E.Text = ik_down_value_E.ToString();
                form.TB_ik_down_value_F.Text = ik_down_value_F.ToString();
                form.TB_ik_down_value_G.Text = ik_down_value_G.ToString();
                form.TB_ik_down_value_H.Text = ik_down_value_H.ToString();
                form.TB_ik_down_value_I.Text = ik_down_value_I.ToString();
            }
            catch
            {
                저장에러();
            }

            try
            {
                Properties.Settings.Default.combo_ik_down_cancel_sell = form.combo_ik_down_cancel_sell.SelectedIndex;

                Properties.Settings.Default.combo_ik_down_A = form.combo_ik_down_A.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_B = form.combo_ik_down_B.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_C = form.combo_ik_down_C.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_D = form.combo_ik_down_D.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_E = form.combo_ik_down_E.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_F = form.combo_ik_down_F.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_G = form.combo_ik_down_G.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_H = form.combo_ik_down_H.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_I = form.combo_ik_down_I.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_ratio_A = form.combo_ik_down_ratio_A.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_ratio_B = form.combo_ik_down_ratio_B.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_ratio_C = form.combo_ik_down_ratio_C.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_ratio_D = form.combo_ik_down_ratio_D.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_ratio_E = form.combo_ik_down_ratio_E.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_ratio_F = form.combo_ik_down_ratio_F.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_ratio_G = form.combo_ik_down_ratio_G.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_ratio_H = form.combo_ik_down_ratio_H.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_ratio_I = form.combo_ik_down_ratio_I.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_jumun_A = form.combo_ik_down_jumun_A.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_jumun_B = form.combo_ik_down_jumun_B.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_jumun_C = form.combo_ik_down_jumun_C.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_jumun_D = form.combo_ik_down_jumun_D.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_jumun_E = form.combo_ik_down_jumun_E.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_jumun_F = form.combo_ik_down_jumun_F.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_jumun_G = form.combo_ik_down_jumun_G.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_jumun_H = form.combo_ik_down_jumun_H.SelectedIndex;
                Properties.Settings.Default.combo_ik_down_jumun_I = form.combo_ik_down_jumun_I.SelectedIndex;
            }
            catch
            {
                저장에러();
            }



            // 계좌청산
            try
            {
                int.TryParse(form.MT_sell_time_start.Text, out int sell_time_start);
                int.TryParse(form.MT_sell_time_end.Text, out int sell_time_end);
                int.TryParse(form.MT_sell_time_repeat.Text, out int sell_time_repeat);
                int.TryParse(form.MT_sell_time_CanselTime.Text, out int sell_time_CanselTime);

                if (sell_time_start == 0) sell_time_start = 150000;
                if (sell_time_end == 0) sell_time_end = 152000;
                if (sell_time_repeat == 0) sell_time_repeat = 1;
                if (sell_time_CanselTime < 10) sell_time_CanselTime = 60;

                Properties.Settings.Default.MT_sell_time_start = sell_time_start;
                Properties.Settings.Default.MT_sell_time_end = sell_time_end;
                Properties.Settings.Default.MT_sell_time_repeat = sell_time_repeat;
                Properties.Settings.Default.MT_sell_time_cansel = sell_time_CanselTime;

                form.MT_sell_time_start.Text = sell_time_start.ToString();
                form.MT_sell_time_end.Text = sell_time_end.ToString();
                form.MT_sell_time_repeat.Text = sell_time_repeat.ToString();
                form.MT_sell_time_CanselTime.Text = sell_time_CanselTime.ToString();

                if (Form1.time_Run_time > sell_time_repeat) Form1.time_Run_time = sell_time_repeat;

                double.TryParse(form.TB_sell_time_trade_1.Text, out double sell_time_trade_1);
                double.TryParse(form.TB_sell_time_trade_2.Text, out double sell_time_trade_2);
                double.TryParse(form.TB_sell_time_ratio.Text, out double sell_time_ratio);
                double.TryParse(form.TB_sell_time_매입금.Text, out double sell_time_매입금);
                double.TryParse(form.TB_sell_time_value.Text, out double sell_time_value);
                double.TryParse(form.TB_sell_time_ik_1.Text, out double sell_time_ik_1);
                double.TryParse(form.TB_sell_time_ik_2.Text, out double sell_time_ik_2);

                if (sell_time_trade_1 == 0) sell_time_trade_1 = 0;
                if (sell_time_trade_2 == 0) sell_time_trade_2 = 100;
                if (sell_time_ratio == 0) sell_time_ratio = 1;
                if (sell_time_ik_1 == 0) sell_time_ik_1 = -100;
                if (sell_time_ik_2 == 0) sell_time_ik_2 = 100;

                if (form.combo_sell_time_jumun.SelectedIndex == 0 || form.combo_sell_time_jumun.SelectedIndex == 1) sell_time_value = 0;

                Properties.Settings.Default.TB_sell_time_trade_1 = Math.Abs(sell_time_trade_1);
                Properties.Settings.Default.TB_sell_time_trade_2 = Math.Abs(sell_time_trade_2);
                Properties.Settings.Default.TB_sell_time_ratio = Math.Abs(sell_time_ratio);
                Properties.Settings.Default.TB_sell_time_매입금 = Math.Abs(sell_time_매입금);
                Properties.Settings.Default.TB_sell_time_value = sell_time_value;
                Properties.Settings.Default.TB_sell_time_ik_1 = sell_time_ik_1;
                Properties.Settings.Default.TB_sell_time_ik_2 = sell_time_ik_2;

                form.TB_sell_time_trade_1.Text = Properties.Settings.Default.TB_sell_time_trade_1.ToString();
                form.TB_sell_time_trade_2.Text = Properties.Settings.Default.TB_sell_time_trade_2.ToString();
                form.TB_sell_time_ratio.Text = Properties.Settings.Default.TB_sell_time_ratio.ToString();
                form.TB_sell_time_매입금.Text = Properties.Settings.Default.TB_sell_time_매입금.ToString();
                form.TB_sell_time_value.Text = sell_time_value.ToString();
                form.TB_sell_time_ik_1.Text = sell_time_ik_1.ToString();
                form.TB_sell_time_ik_2.Text = sell_time_ik_2.ToString();

                Properties.Settings.Default.combo_sell_time_jumun = form.combo_sell_time_jumun.SelectedIndex;
                Properties.Settings.Default.combo_sell_time_gubun = form.combo_sell_time_gubun.SelectedIndex;
                Properties.Settings.Default.CBB_sell_time_choice = form.CBB_sell_time_choice.SelectedIndex;
            }
            catch
            {
                저장에러();
            }


            try
            {
                int.TryParse(form.MT_silson_start_W.Text, out int start);
                int.TryParse(form.MT_silson_end_W.Text, out int end);
                int.TryParse(form.MT_silson_repeat_W.Text, out int repeat);
                int.TryParse(form.MT_silson_CancelTime.Text, out int canseltime);

                if (start == 0) start = 090000;
                if (end == 0) end = 152000;
                if (repeat == 0) repeat = 1;
                if (canseltime == 0) canseltime = 30;

                Properties.Settings.Default.MT_silson_start_W = start;
                Properties.Settings.Default.MT_silson_end_W = end;
                Properties.Settings.Default.MT_silson_repeat_W = repeat;
                Properties.Settings.Default.MT_silson_cancel_W = canseltime;

                form.MT_silson_start_W.Text = start.ToString();
                form.MT_silson_end_W.Text = end.ToString();
                form.MT_silson_repeat_W.Text = repeat.ToString();
                form.MT_silson_CancelTime.Text = canseltime.ToString();

                if (Form1.time_Run_silson_W > repeat) Form1.time_Run_silson_W = repeat;

                double.TryParse(form.TB_silson_trade_W_1.Text, out double trade_1);
                double.TryParse(form.TB_silson_trade_W_2.Text, out double trade_2);
                double.TryParse(form.TB_silson_ratio_W.Text, out double ratio);
                double.TryParse(form.TB_silson_매입금_W.Text, out double 매입금);
                double.TryParse(form.TB_silson_value_W.Text, out double value);
                double.TryParse(form.TB_silson_ik_W_1.Text, out double ik_1);
                double.TryParse(form.TB_silson_ik_W_2.Text, out double ik_2);

                if (form.combo_silson_jumun_W.SelectedIndex == 0 || form.combo_silson_jumun_W.SelectedIndex == 1) value = 0;

                if (trade_1 == 0) trade_1 = 0;
                if (trade_2 == 0) trade_2 = 100;
                if (ratio == 0) ratio = 1;
                if (ik_1 == 0) ik_1 = 0;
                if (ik_2 == 0) ik_2 = 100;

                Properties.Settings.Default.TB_silson_trade_W_1 = Math.Abs(trade_1);
                Properties.Settings.Default.TB_silson_trade_W_2 = Math.Abs(trade_2);
                Properties.Settings.Default.TB_silson_ratio_W = Math.Abs(ratio);
                Properties.Settings.Default.TB_silson_매입금_W = Math.Abs(매입금);
                Properties.Settings.Default.TB_silson_value_W = value;
                Properties.Settings.Default.TB_silson_ik_W_1 = ik_1;
                Properties.Settings.Default.TB_silson_ik_W_2 = ik_2;

                form.TB_silson_trade_W_1.Text = Properties.Settings.Default.TB_silson_trade_W_1.ToString();
                form.TB_silson_trade_W_2.Text = Properties.Settings.Default.TB_silson_trade_W_2.ToString();
                form.TB_silson_ratio_W.Text = Properties.Settings.Default.TB_silson_ratio_W.ToString();
                form.TB_silson_매입금_W.Text = Properties.Settings.Default.TB_silson_매입금_W.ToString();
                form.TB_silson_value_W.Text = value.ToString();
                form.TB_silson_ik_W_1.Text = ik_1.ToString();
                form.TB_silson_ik_W_2.Text = ik_2.ToString();

                Properties.Settings.Default.combo_silson_gubun_W = form.combo_silson_gubun_W.SelectedIndex;
                Properties.Settings.Default.combo_silson_jumun_W = form.combo_silson_jumun_W.SelectedIndex;
            }
            catch
            {
                저장에러();
            }

            try
            {
                int.TryParse(form.MT_예상손실_start.Text, out int start);
                int.TryParse(form.MT_예상손실_end.Text, out int end);
                int.TryParse(form.MT_예상손실_repeat.Text, out int repeat);
                int.TryParse(form.MT_예상손실_CanceTime.Text, out int canseltime);

                if (start == 0) start = 090000;
                if (end == 0) end = 152000;
                if (repeat == 0) repeat = 1;
                if (canseltime < 10) canseltime = 60;

                Properties.Settings.Default.MT_예상손실_start = start;
                Properties.Settings.Default.MT_예상손실_end = end;
                Properties.Settings.Default.MT_예상손실_repeat = repeat;
                Properties.Settings.Default.MT_예상손실_CanceTime = canseltime;

                form.MT_예상손실_start.Text = start.ToString();
                form.MT_예상손실_end.Text = end.ToString();
                form.MT_예상손실_repeat.Text = repeat.ToString();
                form.MT_예상손실_CanceTime.Text = canseltime.ToString();

                if (Form1.time_Run_예상손실 > repeat) Form1.time_Run_예상손실 = repeat;

                double.TryParse(form.TB_예상손실_trade_1.Text, out double trade_1);
                double.TryParse(form.TB_예상손실_trade_2.Text, out double trade_2);
                double.TryParse(form.TB_예상손실_ratio.Text, out double ratio);
                double.TryParse(form.TB_예상손실_매입.Text, out double 매입금);
                double.TryParse(form.TB_예상손실_value.Text, out double value);
                double.TryParse(form.TB_예상손실_ik_1.Text, out double ik_1);
                double.TryParse(form.TB_예상손실_ik_2.Text, out double ik_2);

                if (form.combo_예상손실_jumun.SelectedIndex == 0 || form.combo_예상손실_jumun.SelectedIndex == 1) value = 0;

                if (trade_1 == 0) trade_1 = 0;
                if (trade_2 == 0) trade_2 = 100;
                if (ratio == 0) ratio = 1;
                if (ik_1 == 0) ik_1 = 0;
                if (ik_2 == 0) ik_2 = 100;

                Properties.Settings.Default.TB_예상손실_trade_1 = Math.Abs(trade_1);
                Properties.Settings.Default.TB_예상손실_trade_2 = Math.Abs(trade_2);
                Properties.Settings.Default.TB_예상손실_ratio = Math.Abs(ratio);
                Properties.Settings.Default.TB_예상손실_매입 = Math.Abs(매입금);
                Properties.Settings.Default.TB_예상손실_value = value;
                Properties.Settings.Default.TB_예상손실_ik_1 = ik_1;
                Properties.Settings.Default.TB_예상손실_ik_2 = ik_2;

                form.TB_예상손실_trade_1.Text = Properties.Settings.Default.TB_예상손실_trade_1.ToString();
                form.TB_예상손실_trade_2.Text = Properties.Settings.Default.TB_예상손실_trade_2.ToString();
                form.TB_예상손실_ratio.Text = Properties.Settings.Default.TB_예상손실_ratio.ToString();
                form.TB_예상손실_매입.Text = Properties.Settings.Default.TB_예상손실_매입.ToString();
                form.TB_예상손실_value.Text = value.ToString();
                form.TB_예상손실_ik_1.Text = ik_1.ToString();
                form.TB_예상손실_ik_2.Text = ik_2.ToString();

                double.TryParse(form.TB_지수연동이하.Text, out double 지수연동이하);
                double.TryParse(form.TB_지수연동이상.Text, out double 지수연동이상);

                Properties.Settings.Default.TB_지수연동이하 = 지수연동이하;
                Properties.Settings.Default.TB_지수연동이상 = 지수연동이상;

                form.TB_지수연동이하.Text = 지수연동이하.ToString();
                form.TB_지수연동이상.Text = 지수연동이상.ToString();

                Properties.Settings.Default.combo_예상손실_gubun = form.combo_예상손실_gubun.SelectedIndex;
                Properties.Settings.Default.combo_예상손실_jumun = form.combo_예상손실_jumun.SelectedIndex;
                Properties.Settings.Default.combo_지수연동이하 = form.combo_지수연동이하.SelectedIndex;
                Properties.Settings.Default.combo_지수연동이상 = form.combo_지수연동이상.SelectedIndex;
                Properties.Settings.Default.CB_지수연동범위사용 = form.CB_지수연동범위사용.Checked;

            }
            catch
            {
                저장에러();
            }


            try
            {
                int.TryParse(form.MT_예상수익_start.Text, out int start);
                int.TryParse(form.MT_예상수익_end.Text, out int end);
                int.TryParse(form.MT_예상수익_repeat.Text, out int repeat);
                int.TryParse(form.MT_예상수익_CancelTime.Text, out int canseltime);

                if (start == 0) start = 090000;
                if (end == 0) end = 152000;
                if (repeat == 0) repeat = 1;
                if (canseltime < 10) canseltime = 60;

                Properties.Settings.Default.MT_예상수익_start = start;
                Properties.Settings.Default.MT_예상수익_end = end;
                Properties.Settings.Default.MT_예상수익_repeat = repeat;
                Properties.Settings.Default.MT_예상수익_cancel = canseltime;

                form.MT_예상수익_start.Text = start.ToString();
                form.MT_예상수익_end.Text = end.ToString();
                form.MT_예상수익_repeat.Text = repeat.ToString();
                form.MT_예상수익_CancelTime.Text = canseltime.ToString();

                if (Form1.time_Run_예상수익 > repeat) Form1.time_Run_예상수익 = repeat;

                double.TryParse(form.TB_예상수익_trade_1.Text, out double trade_1);
                double.TryParse(form.TB_예상수익_trade_2.Text, out double trade_2);
                double.TryParse(form.TB_예상수익_ratio.Text, out double ratio);
                double.TryParse(form.TB_예상수익_value.Text, out double value);
                double.TryParse(form.TB_예상수익_ik_1.Text, out double ik_1);
                double.TryParse(form.TB_예상수익_ik_2.Text, out double ik_2);
                double.TryParse(form.TB_예상수익_매입금.Text, out double 매입금);

                if (form.combo_예상수익_jumun.SelectedIndex == 0 || form.combo_예상수익_jumun.SelectedIndex == 1) value = 0;

                if (trade_1 == 0) trade_1 = 0;
                if (trade_2 == 0) trade_2 = 100;
                if (ratio == 0) ratio = 1;
                if (ik_1 == 0) ik_1 = 0;
                if (ik_2 == 0) ik_2 = 100;

                Properties.Settings.Default.TB_예상수익_trade_1 = Math.Abs(trade_1);
                Properties.Settings.Default.TB_예상수익_trade_2 = Math.Abs(trade_2);
                Properties.Settings.Default.TB_예상수익_ratio = Math.Abs(ratio);
                Properties.Settings.Default.TB_예상수익_매입금 = Math.Abs(매입금);
                Properties.Settings.Default.TB_예상수익_value = value;
                Properties.Settings.Default.TB_예상수익_ik_1 = ik_1;
                Properties.Settings.Default.TB_예상수익_ik_2 = ik_2;

                form.TB_예상수익_trade_1.Text = Properties.Settings.Default.TB_예상수익_trade_1.ToString();
                form.TB_예상수익_trade_2.Text = Properties.Settings.Default.TB_예상수익_trade_2.ToString();
                form.TB_예상수익_ratio.Text = Properties.Settings.Default.TB_예상수익_ratio.ToString();
                form.TB_예상수익_매입금.Text = Properties.Settings.Default.TB_예상수익_매입금.ToString();
                form.TB_예상수익_value.Text = value.ToString();
                form.TB_예상수익_ik_1.Text = ik_1.ToString();
                form.TB_예상수익_ik_2.Text = ik_2.ToString();

                Properties.Settings.Default.combo_예상수익_gubun = form.combo_예상수익_gubun.SelectedIndex;
                Properties.Settings.Default.combo_예상수익_jumun = form.combo_예상수익_jumun.SelectedIndex;
                Properties.Settings.Default.CB_예상수익TS = form.CB_예상수익TS.Checked;
                Properties.Settings.Default.CB_예상수익TS_시작 = form.CB_예상수익TS_시작.Checked;

                double.TryParse(form.TB_예상수익TS_상승값.Text, out double TS_상승값);
                double.TryParse(form.TB_예상수익TS_하락값.Text, out double TS_하락값);

                if (TS_상승값 == 0) TS_상승값 = 10;
                if (TS_하락값 == 0) TS_하락값 = -5;

                Properties.Settings.Default.TB_예상수익TS_상승값 = TS_상승값;
                Properties.Settings.Default.TB_예상수익TS_하락값 = TS_하락값;

                form.TB_예상수익TS_상승값.Text = Properties.Settings.Default.TB_예상수익TS_상승값.ToString();
                form.TB_예상수익TS_하락값.Text = Properties.Settings.Default.TB_예상수익TS_하락값.ToString();
            }
            catch
            {
                저장에러();
            }

            // 손실매도 << --- 
            try
            {
                int.TryParse(form.MTB_sell_cancel_time.Text, out int cancel_time);
                int.TryParse(form.MTB_sell_cancel_repeat.Text, out int repeat);
                if (cancel_time < 10) cancel_time = 60;

                if (form.combo_sell_cancel_sell.SelectedIndex < 3) repeat = 0;

                Properties.Settings.Default.MTB_sell_cancel_time = cancel_time;
                Properties.Settings.Default.MTB_sell_cancel_repeat = repeat;

                Properties.Settings.Default.combo_sell_cancel_sell = form.combo_sell_cancel_sell.SelectedIndex;

                form.MTB_sell_cancel_time.Text = cancel_time.ToString();
                form.MTB_sell_cancel_repeat.Text = repeat.ToString();

                int.TryParse(form.MTB_sell_starttime.Text, out int start);
                int.TryParse(form.MTB_sell_endtime.Text, out int end);

                if (start == 0) start = 090000;
                if (end == 0) end = 152000;

                Properties.Settings.Default.MTB_sell_starttime = start;
                Properties.Settings.Default.MTB_sell_endtime = end;

                form.MTB_sell_starttime.Text = start.ToString();
                form.MTB_sell_endtime.Text = end.ToString();
            }
            catch
            {
                저장에러();
            }

            try
            {
                double.TryParse(form.TB_sell_son_A.Text, out double son_A);
                double.TryParse(form.TB_sell_son_B.Text, out double son_B);
                double.TryParse(form.TB_sell_son_C.Text, out double son_C);
                double.TryParse(form.TB_sell_son_D.Text, out double son_D);
                double.TryParse(form.TB_sell_son_E.Text, out double son_E);
                double.TryParse(form.TB_sell_son_F.Text, out double son_F);

                if (son_A == 0) son_A = -1;
                if (son_B == 0) son_B = -2;
                if (son_C == 0) son_C = -3;
                if (son_D == 0) son_D = -4;
                if (son_E == 0) son_E = -5;
                if (son_F == 0) son_F = -6;

                Properties.Settings.Default.TB_sell_son_A = son_A;
                Properties.Settings.Default.TB_sell_son_B = son_B;
                Properties.Settings.Default.TB_sell_son_C = son_C;
                Properties.Settings.Default.TB_sell_son_D = son_D;
                Properties.Settings.Default.TB_sell_son_E = son_E;
                Properties.Settings.Default.TB_sell_son_F = son_F;

                form.TB_sell_son_A.Text = son_A.ToString();
                form.TB_sell_son_B.Text = son_B.ToString();
                form.TB_sell_son_C.Text = son_C.ToString();
                form.TB_sell_son_D.Text = son_D.ToString();
                form.TB_sell_son_E.Text = son_E.ToString();
                form.TB_sell_son_F.Text = son_F.ToString();

                double.TryParse(form.TB_sell_ratio_A.Text, out double ratio_A);
                double.TryParse(form.TB_sell_ratio_B.Text, out double ratio_B);
                double.TryParse(form.TB_sell_ratio_C.Text, out double ratio_C);
                double.TryParse(form.TB_sell_ratio_D.Text, out double ratio_D);
                double.TryParse(form.TB_sell_ratio_E.Text, out double ratio_E);
                double.TryParse(form.TB_sell_ratio_F.Text, out double ratio_F);

                if (ratio_A == 0) ratio_A = 1;
                if (ratio_B == 0) ratio_B = 1;
                if (ratio_C == 0) ratio_C = 1;
                if (ratio_D == 0) ratio_D = 1;
                if (ratio_E == 0) ratio_E = 1;
                if (ratio_F == 0) ratio_F = 1;

                Properties.Settings.Default.TB_sell_ratio_A = ratio_A;
                Properties.Settings.Default.TB_sell_ratio_B = ratio_B;
                Properties.Settings.Default.TB_sell_ratio_C = ratio_C;
                Properties.Settings.Default.TB_sell_ratio_D = ratio_D;
                Properties.Settings.Default.TB_sell_ratio_E = ratio_E;
                Properties.Settings.Default.TB_sell_ratio_F = ratio_F;

                form.TB_sell_ratio_A.Text = ratio_A.ToString();
                form.TB_sell_ratio_B.Text = ratio_B.ToString();
                form.TB_sell_ratio_C.Text = ratio_C.ToString();
                form.TB_sell_ratio_D.Text = ratio_D.ToString();
                form.TB_sell_ratio_E.Text = ratio_E.ToString();
                form.TB_sell_ratio_F.Text = ratio_F.ToString();

                double.TryParse(form.TB_sell_value_A.Text, out double value_A);
                double.TryParse(form.TB_sell_value_B.Text, out double value_B);
                double.TryParse(form.TB_sell_value_C.Text, out double value_C);
                double.TryParse(form.TB_sell_value_D.Text, out double value_D);
                double.TryParse(form.TB_sell_value_E.Text, out double value_E);
                double.TryParse(form.TB_sell_value_F.Text, out double value_F);

                Properties.Settings.Default.TB_sell_value_A = value_A;
                Properties.Settings.Default.TB_sell_value_B = value_B;
                Properties.Settings.Default.TB_sell_value_C = value_C;
                Properties.Settings.Default.TB_sell_value_D = value_D;
                Properties.Settings.Default.TB_sell_value_E = value_E;
                Properties.Settings.Default.TB_sell_value_F = value_F;

                if (form.combo_sell_jumun_A.SelectedIndex == 0 || form.combo_sell_jumun_A.SelectedIndex == 1) value_A = 0;
                if (form.combo_sell_jumun_B.SelectedIndex == 0 || form.combo_sell_jumun_B.SelectedIndex == 1) value_B = 0;
                if (form.combo_sell_jumun_C.SelectedIndex == 0 || form.combo_sell_jumun_C.SelectedIndex == 1) value_C = 0;
                if (form.combo_sell_jumun_D.SelectedIndex == 0 || form.combo_sell_jumun_D.SelectedIndex == 1) value_D = 0;
                if (form.combo_sell_jumun_E.SelectedIndex == 0 || form.combo_sell_jumun_E.SelectedIndex == 1) value_E = 0;
                if (form.combo_sell_jumun_F.SelectedIndex == 0 || form.combo_sell_jumun_F.SelectedIndex == 1) value_F = 0;

                form.TB_sell_value_A.Text = value_A.ToString();
                form.TB_sell_value_B.Text = value_B.ToString();
                form.TB_sell_value_C.Text = value_C.ToString();
                form.TB_sell_value_D.Text = value_D.ToString();
                form.TB_sell_value_E.Text = value_E.ToString();
                form.TB_sell_value_F.Text = value_F.ToString();
            }
            catch
            {
                저장에러();
            }

            try
            {
                int.TryParse(form.TB_신규주가이상.Text.Replace(",", ""), out int TB_신규주가이상);
                int.TryParse(form.TB_신규주가이하.Text.Replace(",", ""), out int TB_신규주가이하);
                if (TB_신규주가이하 == 0) TB_신규주가이하 = 1000000;
                Properties.Settings.Default.TB_신규주가이상 = TB_신규주가이상;
                Properties.Settings.Default.TB_신규주가이하 = TB_신규주가이하;

                double.TryParse(form.TB_신규등락률이상.Text, out double TB_신규등락률이상);
                double.TryParse(form.TB_신규등락률이하.Text, out double TB_신규등락률이하);
                Properties.Settings.Default.TB_신규등락률이상 = TB_신규등락률이상;
                Properties.Settings.Default.TB_신규등락률이하 = TB_신규등락률이하;

                form.TB_신규주가이상.Text = Properties.Settings.Default.TB_신규주가이상.ToString();
                form.TB_신규주가이하.Text = Properties.Settings.Default.TB_신규주가이하.ToString();
                form.TB_신규등락률이상.Text = Properties.Settings.Default.TB_신규등락률이상.ToString();
                form.TB_신규등락률이하.Text = Properties.Settings.Default.TB_신규등락률이하.ToString();

                Properties.Settings.Default.CB_new_rebuy = form.CB_new_rebuy.Checked;

                int.TryParse(form.MTB_new_rebuytime.Text, out int Rebuytime);
                int.TryParse(form.MTB_추가매수딜레이.Text, out int 추가매수딜레이);

                if (Rebuytime < 10) Rebuytime = 10;
                if (추가매수딜레이 < 10) 추가매수딜레이 = 10;

                Properties.Settings.Default.MTB_new_rebuytime = Rebuytime;
                Properties.Settings.Default.MTB_추가매수딜레이 = 추가매수딜레이;

                form.MTB_new_rebuytime.Text = Rebuytime.ToString();
                form.MTB_추가매수딜레이.Text = 추가매수딜레이.ToString();

                if (form.CB_new_rebuy.Checked)
                {
                    foreach (var 잔고 in stockBalanceList.ToList())
                    {
                        잔고.Value.재매수 = Properties.Settings.Default.MTB_new_rebuytime;
                    }
                }
            }
            catch
            {
                저장에러();
            }

            try
            {
                int.TryParse(form.TB_TimeSell_start_A.Text, out int TB_TimeSell_start_A);
                int.TryParse(form.TB_TimeSell_start_B.Text, out int TB_TimeSell_start_B);
                int.TryParse(form.TB_TimeSell_start_C.Text, out int TB_TimeSell_start_C);
                int.TryParse(form.MT_TimeSell_반복간격_A.Text, out int MT_TimeSell_반복간격_A);
                int.TryParse(form.MT_TimeSell_반복간격_B.Text, out int MT_TimeSell_반복간격_B);
                int.TryParse(form.MT_TimeSell_반복간격_C.Text, out int MT_TimeSell_반복간격_C);
                int.TryParse(form.MT_TimeSell_취소간격_A.Text, out int MT_TimeSell_취소간격_A);
                int.TryParse(form.MT_TimeSell_취소간격_B.Text, out int MT_TimeSell_취소간격_B);
                int.TryParse(form.MT_TimeSell_취소간격_C.Text, out int MT_TimeSell_취소간격_C);

                if (TB_TimeSell_start_A == 0) TB_TimeSell_start_A = 60;
                if (TB_TimeSell_start_B == 0) TB_TimeSell_start_B = 60;
                if (TB_TimeSell_start_C == 0) TB_TimeSell_start_C = 60;
                if (MT_TimeSell_반복간격_A == 0) MT_TimeSell_반복간격_A = 1;
                if (MT_TimeSell_반복간격_B == 0) MT_TimeSell_반복간격_B = 1;
                if (MT_TimeSell_반복간격_C == 0) MT_TimeSell_반복간격_C = 1;
                if (MT_TimeSell_취소간격_A < 10) MT_TimeSell_취소간격_A = 60;
                if (MT_TimeSell_취소간격_B < 10) MT_TimeSell_취소간격_B = 60;
                if (MT_TimeSell_취소간격_C < 10) MT_TimeSell_취소간격_C = 60;

                if (form.CBB_TimeSell_start_A.SelectedIndex == 0 && TB_TimeSell_start_A < 090000) TB_TimeSell_start_A = 090100;
                if (form.CBB_TimeSell_start_B.SelectedIndex == 0 && TB_TimeSell_start_B < 090000) TB_TimeSell_start_B = 090100;
                if (form.CBB_TimeSell_start_C.SelectedIndex == 0 && TB_TimeSell_start_C < 090000) TB_TimeSell_start_C = 090100;

                Properties.Settings.Default.TB_TimeSell_start_A = Math.Abs(TB_TimeSell_start_A);
                Properties.Settings.Default.TB_TimeSell_start_B = Math.Abs(TB_TimeSell_start_B);
                Properties.Settings.Default.TB_TimeSell_start_C = Math.Abs(TB_TimeSell_start_C);
                Properties.Settings.Default.MT_TimeSell_반복간격_A = MT_TimeSell_반복간격_A;
                Properties.Settings.Default.MT_TimeSell_반복간격_B = MT_TimeSell_반복간격_B;
                Properties.Settings.Default.MT_TimeSell_반복간격_C = MT_TimeSell_반복간격_C;
                Properties.Settings.Default.MT_TimeSell_취소간격_A = MT_TimeSell_취소간격_A;
                Properties.Settings.Default.MT_TimeSell_취소간격_B = MT_TimeSell_취소간격_B;
                Properties.Settings.Default.MT_TimeSell_취소간격_C = MT_TimeSell_취소간격_C;

                form.TB_TimeSell_start_A.Text = Properties.Settings.Default.TB_TimeSell_start_A.ToString();
                form.TB_TimeSell_start_B.Text = Properties.Settings.Default.TB_TimeSell_start_B.ToString();
                form.TB_TimeSell_start_C.Text = Properties.Settings.Default.TB_TimeSell_start_C.ToString();
                form.MT_TimeSell_반복간격_A.Text = Properties.Settings.Default.MT_TimeSell_반복간격_A.ToString();
                form.MT_TimeSell_반복간격_B.Text = Properties.Settings.Default.MT_TimeSell_반복간격_B.ToString();
                form.MT_TimeSell_반복간격_C.Text = Properties.Settings.Default.MT_TimeSell_반복간격_C.ToString();
                form.MT_TimeSell_취소간격_A.Text = Properties.Settings.Default.MT_TimeSell_취소간격_A.ToString();
                form.MT_TimeSell_취소간격_B.Text = Properties.Settings.Default.MT_TimeSell_취소간격_B.ToString();
                form.MT_TimeSell_취소간격_C.Text = Properties.Settings.Default.MT_TimeSell_취소간격_C.ToString();

                double.TryParse(form.TB_TimeSell_매매범위_1_A.Text, out double TB_TimeSell_매매범위_1_A);
                double.TryParse(form.TB_TimeSell_매매범위_1_B.Text, out double TB_TimeSell_매매범위_1_B);
                double.TryParse(form.TB_TimeSell_매매범위_1_C.Text, out double TB_TimeSell_매매범위_1_C);
                double.TryParse(form.TB_TimeSell_매매범위_2_A.Text, out double TB_TimeSell_매매범위_2_A);
                double.TryParse(form.TB_TimeSell_매매범위_2_B.Text, out double TB_TimeSell_매매범위_2_B);
                double.TryParse(form.TB_TimeSell_매매범위_2_C.Text, out double TB_TimeSell_매매범위_2_C);
                if (TB_TimeSell_매매범위_2_A == 0 || TB_TimeSell_매매범위_2_A >= 100) TB_TimeSell_매매범위_2_A = 100;
                if (TB_TimeSell_매매범위_2_B == 0 || TB_TimeSell_매매범위_2_B >= 100) TB_TimeSell_매매범위_2_B = 100;
                if (TB_TimeSell_매매범위_2_C == 0 || TB_TimeSell_매매범위_2_C >= 100) TB_TimeSell_매매범위_2_C = 100;
                Properties.Settings.Default.TB_TimeSell_매매범위_1_A = Math.Abs(TB_TimeSell_매매범위_1_A);
                Properties.Settings.Default.TB_TimeSell_매매범위_1_B = Math.Abs(TB_TimeSell_매매범위_1_B);
                Properties.Settings.Default.TB_TimeSell_매매범위_1_C = Math.Abs(TB_TimeSell_매매범위_1_C);
                Properties.Settings.Default.TB_TimeSell_매매범위_2_A = Math.Abs(TB_TimeSell_매매범위_2_A);
                Properties.Settings.Default.TB_TimeSell_매매범위_2_B = Math.Abs(TB_TimeSell_매매범위_2_B);
                Properties.Settings.Default.TB_TimeSell_매매범위_2_C = Math.Abs(TB_TimeSell_매매범위_2_C);
                form.TB_TimeSell_매매범위_1_A.Text = Properties.Settings.Default.TB_TimeSell_매매범위_1_A.ToString();
                form.TB_TimeSell_매매범위_1_B.Text = Properties.Settings.Default.TB_TimeSell_매매범위_1_B.ToString();
                form.TB_TimeSell_매매범위_1_C.Text = Properties.Settings.Default.TB_TimeSell_매매범위_1_C.ToString();
                form.TB_TimeSell_매매범위_2_A.Text = Properties.Settings.Default.TB_TimeSell_매매범위_2_A.ToString();
                form.TB_TimeSell_매매범위_2_B.Text = Properties.Settings.Default.TB_TimeSell_매매범위_2_B.ToString();
                form.TB_TimeSell_매매범위_2_C.Text = Properties.Settings.Default.TB_TimeSell_매매범위_2_C.ToString();

                double.TryParse(form.TB_TimeSell_매도비중_A.Text, out double TB_TimeSell_매도비중_A);
                double.TryParse(form.TB_TimeSell_매도비중_B.Text, out double TB_TimeSell_매도비중_B);
                double.TryParse(form.TB_TimeSell_매도비중_C.Text, out double TB_TimeSell_매도비중_C);
                if (TB_TimeSell_매도비중_A == 0) TB_TimeSell_매도비중_A = 1;
                if (TB_TimeSell_매도비중_B == 0) TB_TimeSell_매도비중_B = 1;
                if (TB_TimeSell_매도비중_C == 0) TB_TimeSell_매도비중_C = 1;
                Properties.Settings.Default.TB_TimeSell_매도비중_A = Math.Abs(TB_TimeSell_매도비중_A);
                Properties.Settings.Default.TB_TimeSell_매도비중_B = Math.Abs(TB_TimeSell_매도비중_B);
                Properties.Settings.Default.TB_TimeSell_매도비중_C = Math.Abs(TB_TimeSell_매도비중_C);
                form.TB_TimeSell_매도비중_A.Text = Properties.Settings.Default.TB_TimeSell_매도비중_A.ToString();
                form.TB_TimeSell_매도비중_B.Text = Properties.Settings.Default.TB_TimeSell_매도비중_B.ToString();
                form.TB_TimeSell_매도비중_C.Text = Properties.Settings.Default.TB_TimeSell_매도비중_C.ToString();


                double.TryParse(form.TB_TimeSell_수익범위_1_A.Text, out double TB_TimeSell_수익범위_1_A);
                double.TryParse(form.TB_TimeSell_수익범위_1_B.Text, out double TB_TimeSell_수익범위_1_B);
                double.TryParse(form.TB_TimeSell_수익범위_1_C.Text, out double TB_TimeSell_수익범위_1_C);
                double.TryParse(form.TB_TimeSell_수익범위_2_A.Text, out double TB_TimeSell_수익범위_2_A);
                double.TryParse(form.TB_TimeSell_수익범위_2_B.Text, out double TB_TimeSell_수익범위_2_B);
                double.TryParse(form.TB_TimeSell_수익범위_2_C.Text, out double TB_TimeSell_수익범위_2_C);
                Properties.Settings.Default.TB_TimeSell_수익범위_1_A = TB_TimeSell_수익범위_1_A;
                Properties.Settings.Default.TB_TimeSell_수익범위_1_B = TB_TimeSell_수익범위_1_B;
                Properties.Settings.Default.TB_TimeSell_수익범위_1_C = TB_TimeSell_수익범위_1_C;
                Properties.Settings.Default.TB_TimeSell_수익범위_2_A = TB_TimeSell_수익범위_2_A;
                Properties.Settings.Default.TB_TimeSell_수익범위_2_B = TB_TimeSell_수익범위_2_B;
                Properties.Settings.Default.TB_TimeSell_수익범위_2_C = TB_TimeSell_수익범위_2_C;
                form.TB_TimeSell_수익범위_1_A.Text = Properties.Settings.Default.TB_TimeSell_수익범위_1_A.ToString();
                form.TB_TimeSell_수익범위_1_B.Text = Properties.Settings.Default.TB_TimeSell_수익범위_1_B.ToString();
                form.TB_TimeSell_수익범위_1_C.Text = Properties.Settings.Default.TB_TimeSell_수익범위_1_C.ToString();
                form.TB_TimeSell_수익범위_2_A.Text = Properties.Settings.Default.TB_TimeSell_수익범위_2_A.ToString();
                form.TB_TimeSell_수익범위_2_B.Text = Properties.Settings.Default.TB_TimeSell_수익범위_2_B.ToString();
                form.TB_TimeSell_수익범위_2_C.Text = Properties.Settings.Default.TB_TimeSell_수익범위_2_C.ToString();

                double.TryParse(form.TB_TimeSell_매입금1_A.Text, out double TB_TimeSell_매입금1_A);
                double.TryParse(form.TB_TimeSell_매입금1_B.Text, out double TB_TimeSell_매입금1_B);
                double.TryParse(form.TB_TimeSell_매입금1_C.Text, out double TB_TimeSell_매입금1_C);
                if (TB_TimeSell_매입금1_A < 0) TB_TimeSell_매입금1_A = 0;
                if (TB_TimeSell_매입금1_B < 0) TB_TimeSell_매입금1_B = 0;
                if (TB_TimeSell_매입금1_C < 0) TB_TimeSell_매입금1_C = 0;
                Properties.Settings.Default.TB_TimeSell_매입금1_A = Math.Abs(TB_TimeSell_매입금1_A);
                Properties.Settings.Default.TB_TimeSell_매입금1_B = Math.Abs(TB_TimeSell_매입금1_B);
                Properties.Settings.Default.TB_TimeSell_매입금1_C = Math.Abs(TB_TimeSell_매입금1_C);
                form.TB_TimeSell_매입금1_A.Text = Math.Abs(TB_TimeSell_매입금1_A).ToString();
                form.TB_TimeSell_매입금1_B.Text = Math.Abs(TB_TimeSell_매입금1_B).ToString();
                form.TB_TimeSell_매입금1_C.Text = Math.Abs(TB_TimeSell_매입금1_C).ToString();

                double.TryParse(form.TB_TimeSell_매입금2_A.Text, out double TB_TimeSell_매입금2_A);
                double.TryParse(form.TB_TimeSell_매입금2_B.Text, out double TB_TimeSell_매입금2_B);
                double.TryParse(form.TB_TimeSell_매입금2_C.Text, out double TB_TimeSell_매입금2_C);
                if (TB_TimeSell_매입금2_A == 0) TB_TimeSell_매입금2_A = 10000;
                if (TB_TimeSell_매입금2_B == 0) TB_TimeSell_매입금2_B = 10000;
                if (TB_TimeSell_매입금2_C == 0) TB_TimeSell_매입금2_C = 10000;
                Properties.Settings.Default.TB_TimeSell_매입금2_A = Math.Abs(TB_TimeSell_매입금2_A);
                Properties.Settings.Default.TB_TimeSell_매입금2_B = Math.Abs(TB_TimeSell_매입금2_B);
                Properties.Settings.Default.TB_TimeSell_매입금2_C = Math.Abs(TB_TimeSell_매입금2_C);
                form.TB_TimeSell_매입금2_A.Text = Math.Abs(TB_TimeSell_매입금2_A).ToString();
                form.TB_TimeSell_매입금2_B.Text = Math.Abs(TB_TimeSell_매입금2_B).ToString();
                form.TB_TimeSell_매입금2_C.Text = Math.Abs(TB_TimeSell_매입금2_C).ToString();

                double.TryParse(form.TB_TimeSell_주문가격_A.Text, out double TB_TimeSell_주문가격_A);
                double.TryParse(form.TB_TimeSell_주문가격_B.Text, out double TB_TimeSell_주문가격_B);
                double.TryParse(form.TB_TimeSell_주문가격_C.Text, out double TB_TimeSell_주문가격_C);
                if (form.CBB_TimeSell_주문가격_A.SelectedIndex == 0 || form.CBB_TimeSell_주문가격_A.SelectedIndex == 1) TB_TimeSell_주문가격_A = 0;
                if (form.CBB_TimeSell_주문가격_B.SelectedIndex == 0 || form.CBB_TimeSell_주문가격_B.SelectedIndex == 1) TB_TimeSell_주문가격_B = 0;
                if (form.CBB_TimeSell_주문가격_C.SelectedIndex == 0 || form.CBB_TimeSell_주문가격_C.SelectedIndex == 1) TB_TimeSell_주문가격_C = 0;
                Properties.Settings.Default.TB_TimeSell_주문가격_A = TB_TimeSell_주문가격_A;
                Properties.Settings.Default.TB_TimeSell_주문가격_B = TB_TimeSell_주문가격_B;
                Properties.Settings.Default.TB_TimeSell_주문가격_C = TB_TimeSell_주문가격_C;
                form.TB_TimeSell_주문가격_A.Text = Properties.Settings.Default.TB_TimeSell_주문가격_A.ToString();
                form.TB_TimeSell_주문가격_B.Text = Properties.Settings.Default.TB_TimeSell_주문가격_B.ToString();
                form.TB_TimeSell_주문가격_C.Text = Properties.Settings.Default.TB_TimeSell_주문가격_C.ToString();

                Properties.Settings.Default.CBB_TimeSell_start_A = form.CBB_TimeSell_start_A.SelectedIndex;
                Properties.Settings.Default.CBB_TimeSell_start_B = form.CBB_TimeSell_start_B.SelectedIndex;
                Properties.Settings.Default.CBB_TimeSell_start_C = form.CBB_TimeSell_start_C.SelectedIndex;
                Properties.Settings.Default.CBB_TimeSell_주문가격_A = form.CBB_TimeSell_주문가격_A.SelectedIndex;
                Properties.Settings.Default.CBB_TimeSell_주문가격_B = form.CBB_TimeSell_주문가격_B.SelectedIndex;
                Properties.Settings.Default.CBB_TimeSell_주문가격_C = form.CBB_TimeSell_주문가격_C.SelectedIndex;
                Properties.Settings.Default.CBB_TimeSell_수익구분_A = form.CBB_TimeSell_수익구분_A.SelectedIndex;
                Properties.Settings.Default.CBB_TimeSell_수익구분_B = form.CBB_TimeSell_수익구분_B.SelectedIndex;
                Properties.Settings.Default.CBB_TimeSell_수익구분_C = form.CBB_TimeSell_수익구분_C.SelectedIndex;
                Properties.Settings.Default.CBB_TimeSell_매도비중_A = form.CBB_TimeSell_매도비중_A.SelectedIndex;
                Properties.Settings.Default.CBB_TimeSell_매도비중_B = form.CBB_TimeSell_매도비중_B.SelectedIndex;
                Properties.Settings.Default.CBB_TimeSell_매도비중_C = form.CBB_TimeSell_매도비중_C.SelectedIndex;

                int.TryParse(form.TB_TimeSell_거래일_A.Text, out int 거래일A);
                int.TryParse(form.TB_TimeSell_거래일_B.Text, out int 거래일B);
                int.TryParse(form.TB_TimeSell_거래일_C.Text, out int 거래일C);
                if (form.CBB_TimeSell_start_A.SelectedIndex == 1) 거래일A = 0;
                if (form.CBB_TimeSell_start_B.SelectedIndex == 1) 거래일B = 0;
                if (form.CBB_TimeSell_start_C.SelectedIndex == 1) 거래일C = 0;
                Properties.Settings.Default.TB_TimeSell_거래일_A = Math.Abs(거래일A);
                Properties.Settings.Default.TB_TimeSell_거래일_B = Math.Abs(거래일A);
                Properties.Settings.Default.TB_TimeSell_거래일_C = Math.Abs(거래일A);
                form.TB_TimeSell_거래일_A.Text = Properties.Settings.Default.TB_TimeSell_거래일_A.ToString();
                form.TB_TimeSell_거래일_B.Text = Properties.Settings.Default.TB_TimeSell_거래일_B.ToString();
                form.TB_TimeSell_거래일_C.Text = Properties.Settings.Default.TB_TimeSell_거래일_C.ToString();
            }
            catch
            {
                저장에러();
            }


            try
            {
                int.TryParse(form.TB_Limit_New_A.Text, out int Limit_New_A);
                int.TryParse(form.TB_Limit_New_B.Text, out int Limit_New_B);
                int.TryParse(form.TB_Limit_New_C.Text, out int Limit_New_C);

                if (Limit_New_A == 0) Limit_New_A = 60;
                if (Limit_New_B == 0) Limit_New_B = 60;
                if (Limit_New_C == 0) Limit_New_C = 60;

                Properties.Settings.Default.TB_Limit_New_A = Math.Abs(Limit_New_A);
                Properties.Settings.Default.TB_Limit_New_B = Math.Abs(Limit_New_B);
                Properties.Settings.Default.TB_Limit_New_C = Math.Abs(Limit_New_C);

                form.TB_Limit_New_A.Text = Properties.Settings.Default.TB_Limit_New_A.ToString();
                form.TB_Limit_New_B.Text = Properties.Settings.Default.TB_Limit_New_B.ToString();
                form.TB_Limit_New_C.Text = Properties.Settings.Default.TB_Limit_New_C.ToString();
            }
            catch
            {
                저장에러();
            }

            try
            {
                int.TryParse(form.TB_잔고개수_신규A.Text, out int 신규A);
                int.TryParse(form.TB_잔고개수_신규B.Text, out int 신규B);
                int.TryParse(form.TB_잔고개수_신규C.Text, out int 신규C);

                if (신규A > 100) 신규A = 100;
                if (신규B > 100) 신규B = 100;
                if (신규C > 100) 신규C = 100;

                Properties.Settings.Default.TB_잔고개수_신규A = Math.Abs(신규A);
                Properties.Settings.Default.TB_잔고개수_신규B = Math.Abs(신규B);
                Properties.Settings.Default.TB_잔고개수_신규C = Math.Abs(신규C);

                form.TB_잔고개수_신규A.Text = Properties.Settings.Default.TB_잔고개수_신규A.ToString();
                form.TB_잔고개수_신규B.Text = Properties.Settings.Default.TB_잔고개수_신규B.ToString();
                form.TB_잔고개수_신규C.Text = Properties.Settings.Default.TB_잔고개수_신규C.ToString();
            }
            catch
            {
                저장에러();
            }

            try
            {
                int.TryParse(form.MTB_TS_canceltime.Text, out int TS_canceltime);
                if (TS_canceltime < 10) TS_canceltime = 60;
                int.TryParse(form.MTB_TS_repeat.Text, out int TS_repeat);

                Properties.Settings.Default.MTB_TS_canceltime = TS_canceltime;
                Properties.Settings.Default.MTB_TS_repeat = TS_repeat;

                form.MTB_TS_canceltime.Text = TS_canceltime.ToString();
                form.MTB_TS_repeat.Text = TS_repeat.ToString();

                Properties.Settings.Default.CBB_TS_cancel_sell = form.CBB_TS_cancel_sell.SelectedIndex;
            }
            catch
            {
                저장에러();
            }

            try
            {
                double.TryParse(form.TB_TS_upper_A.Text, out double TS_upper_1);
                double.TryParse(form.TB_TS_upper_B.Text, out double TS_upper_2);
                double.TryParse(form.TB_TS_upper_C.Text, out double TS_upper_3);
                double.TryParse(form.TB_TS_upper_D.Text, out double TS_upper_4);
                double.TryParse(form.TB_TS_upper_E.Text, out double TS_upper_5);
                double.TryParse(form.TB_TS_upper_F.Text, out double TS_upper_6);
                double.TryParse(form.TB_TS_upper_G.Text, out double TS_upper_7);
                double.TryParse(form.TB_TS_upper_H.Text, out double TS_upper_8);
                double.TryParse(form.TB_TS_upper_I.Text, out double TS_upper_9);

                if (TS_upper_1 == 0) TS_upper_1 = 3;
                if (TS_upper_2 == 0) TS_upper_2 = 3;
                if (TS_upper_3 == 0) TS_upper_3 = 3;
                if (TS_upper_4 == 0) TS_upper_4 = 3;
                if (TS_upper_5 == 0) TS_upper_5 = 3;
                if (TS_upper_6 == 0) TS_upper_6 = 3;
                if (TS_upper_7 == 0) TS_upper_7 = 3;
                if (TS_upper_8 == 0) TS_upper_8 = 3;
                if (TS_upper_9 == 0) TS_upper_9 = 3;

                Properties.Settings.Default.TB_TS_upper_A = Math.Abs(TS_upper_1);
                Properties.Settings.Default.TB_TS_upper_B = Math.Abs(TS_upper_2);
                Properties.Settings.Default.TB_TS_upper_C = Math.Abs(TS_upper_3);
                Properties.Settings.Default.TB_TS_upper_D = Math.Abs(TS_upper_4);
                Properties.Settings.Default.TB_TS_upper_E = Math.Abs(TS_upper_5);
                Properties.Settings.Default.TB_TS_upper_F = Math.Abs(TS_upper_6);
                Properties.Settings.Default.TB_TS_upper_G = Math.Abs(TS_upper_7);
                Properties.Settings.Default.TB_TS_upper_H = Math.Abs(TS_upper_8);
                Properties.Settings.Default.TB_TS_upper_I = Math.Abs(TS_upper_9);

                form.TB_TS_upper_A.Text = Properties.Settings.Default.TB_TS_upper_A.ToString();
                form.TB_TS_upper_B.Text = Properties.Settings.Default.TB_TS_upper_B.ToString();
                form.TB_TS_upper_C.Text = Properties.Settings.Default.TB_TS_upper_C.ToString();
                form.TB_TS_upper_D.Text = Properties.Settings.Default.TB_TS_upper_D.ToString();
                form.TB_TS_upper_E.Text = Properties.Settings.Default.TB_TS_upper_E.ToString();
                form.TB_TS_upper_F.Text = Properties.Settings.Default.TB_TS_upper_F.ToString();
                form.TB_TS_upper_G.Text = Properties.Settings.Default.TB_TS_upper_G.ToString();
                form.TB_TS_upper_H.Text = Properties.Settings.Default.TB_TS_upper_H.ToString();
                form.TB_TS_upper_I.Text = Properties.Settings.Default.TB_TS_upper_I.ToString();
            }
            catch
            {
                저장에러();
            }

            try
            {
                double.TryParse(form.TB_TS_down_A.Text, out double TS_down_1);
                double.TryParse(form.TB_TS_down_B.Text, out double TS_down_2);
                double.TryParse(form.TB_TS_down_C.Text, out double TS_down_3);
                double.TryParse(form.TB_TS_down_D.Text, out double TS_down_4);
                double.TryParse(form.TB_TS_down_E.Text, out double TS_down_5);
                double.TryParse(form.TB_TS_down_F.Text, out double TS_down_6);
                double.TryParse(form.TB_TS_down_G.Text, out double TS_down_7);
                double.TryParse(form.TB_TS_down_H.Text, out double TS_down_8);
                double.TryParse(form.TB_TS_down_I.Text, out double TS_down_9);

                if (TS_down_1 == 0) TS_down_1 = -2;
                if (TS_down_2 == 0) TS_down_2 = -2;
                if (TS_down_3 == 0) TS_down_3 = -2;
                if (TS_down_4 == 0) TS_down_4 = -2;
                if (TS_down_5 == 0) TS_down_5 = -2;
                if (TS_down_6 == 0) TS_down_6 = -2;
                if (TS_down_7 == 0) TS_down_7 = -2;
                if (TS_down_8 == 0) TS_down_8 = -2;
                if (TS_down_9 == 0) TS_down_9 = -2;

                Properties.Settings.Default.TB_TS_down_A = TS_down_1;
                Properties.Settings.Default.TB_TS_down_B = TS_down_2;
                Properties.Settings.Default.TB_TS_down_C = TS_down_3;
                Properties.Settings.Default.TB_TS_down_D = TS_down_4;
                Properties.Settings.Default.TB_TS_down_E = TS_down_5;
                Properties.Settings.Default.TB_TS_down_F = TS_down_6;
                Properties.Settings.Default.TB_TS_down_G = TS_down_7;
                Properties.Settings.Default.TB_TS_down_H = TS_down_8;
                Properties.Settings.Default.TB_TS_down_I = TS_down_9;

                form.TB_TS_down_A.Text = Properties.Settings.Default.TB_TS_down_A.ToString();
                form.TB_TS_down_B.Text = Properties.Settings.Default.TB_TS_down_B.ToString();
                form.TB_TS_down_C.Text = Properties.Settings.Default.TB_TS_down_C.ToString();
                form.TB_TS_down_D.Text = Properties.Settings.Default.TB_TS_down_D.ToString();
                form.TB_TS_down_E.Text = Properties.Settings.Default.TB_TS_down_E.ToString();
                form.TB_TS_down_F.Text = Properties.Settings.Default.TB_TS_down_F.ToString();
                form.TB_TS_down_G.Text = Properties.Settings.Default.TB_TS_down_G.ToString();
                form.TB_TS_down_H.Text = Properties.Settings.Default.TB_TS_down_H.ToString();
                form.TB_TS_down_I.Text = Properties.Settings.Default.TB_TS_down_I.ToString();
            }
            catch
            {
                저장에러();
            }

            try
            {
                double.TryParse(form.TB_TS_ratio_A.Text, out double TS_ratio_1);
                double.TryParse(form.TB_TS_ratio_B.Text, out double TS_ratio_2);
                double.TryParse(form.TB_TS_ratio_C.Text, out double TS_ratio_3);
                double.TryParse(form.TB_TS_ratio_D.Text, out double TS_ratio_4);
                double.TryParse(form.TB_TS_ratio_E.Text, out double TS_ratio_5);
                double.TryParse(form.TB_TS_ratio_F.Text, out double TS_ratio_6);
                double.TryParse(form.TB_TS_ratio_G.Text, out double TS_ratio_7);
                double.TryParse(form.TB_TS_ratio_H.Text, out double TS_ratio_8);
                double.TryParse(form.TB_TS_ratio_I.Text, out double TS_ratio_9);

                if (TS_ratio_1 == 0) TS_ratio_1 = 1;
                if (TS_ratio_2 == 0) TS_ratio_2 = 1;
                if (TS_ratio_3 == 0) TS_ratio_3 = 1;
                if (TS_ratio_4 == 0) TS_ratio_4 = 1;
                if (TS_ratio_5 == 0) TS_ratio_5 = 1;
                if (TS_ratio_6 == 0) TS_ratio_6 = 1;
                if (TS_ratio_7 == 0) TS_ratio_7 = 1;
                if (TS_ratio_8 == 0) TS_ratio_8 = 1;
                if (TS_ratio_9 == 0) TS_ratio_9 = 1;

                Properties.Settings.Default.TB_TS_ratio_A = Math.Abs(TS_ratio_1);
                Properties.Settings.Default.TB_TS_ratio_B = Math.Abs(TS_ratio_2);
                Properties.Settings.Default.TB_TS_ratio_C = Math.Abs(TS_ratio_3);
                Properties.Settings.Default.TB_TS_ratio_D = Math.Abs(TS_ratio_4);
                Properties.Settings.Default.TB_TS_ratio_E = Math.Abs(TS_ratio_5);
                Properties.Settings.Default.TB_TS_ratio_F = Math.Abs(TS_ratio_6);
                Properties.Settings.Default.TB_TS_ratio_G = Math.Abs(TS_ratio_7);
                Properties.Settings.Default.TB_TS_ratio_H = Math.Abs(TS_ratio_8);
                Properties.Settings.Default.TB_TS_ratio_I = Math.Abs(TS_ratio_9);

                form.TB_TS_ratio_A.Text = Properties.Settings.Default.TB_TS_ratio_A.ToString();
                form.TB_TS_ratio_B.Text = Properties.Settings.Default.TB_TS_ratio_B.ToString();
                form.TB_TS_ratio_C.Text = Properties.Settings.Default.TB_TS_ratio_C.ToString();
                form.TB_TS_ratio_D.Text = Properties.Settings.Default.TB_TS_ratio_D.ToString();
                form.TB_TS_ratio_E.Text = Properties.Settings.Default.TB_TS_ratio_E.ToString();
                form.TB_TS_ratio_F.Text = Properties.Settings.Default.TB_TS_ratio_F.ToString();
                form.TB_TS_ratio_G.Text = Properties.Settings.Default.TB_TS_ratio_G.ToString();
                form.TB_TS_ratio_H.Text = Properties.Settings.Default.TB_TS_ratio_H.ToString();
                form.TB_TS_ratio_I.Text = Properties.Settings.Default.TB_TS_ratio_I.ToString();
            }
            catch
            {
                저장에러();
            }

            try
            {
                double.TryParse(form.TB_TS_Jumun_A.Text, out double TS_Jumun_1);
                double.TryParse(form.TB_TS_Jumun_B.Text, out double TS_Jumun_2);
                double.TryParse(form.TB_TS_Jumun_C.Text, out double TS_Jumun_3);
                double.TryParse(form.TB_TS_Jumun_D.Text, out double TS_Jumun_4);
                double.TryParse(form.TB_TS_Jumun_E.Text, out double TS_Jumun_5);
                double.TryParse(form.TB_TS_Jumun_F.Text, out double TS_Jumun_6);
                double.TryParse(form.TB_TS_Jumun_G.Text, out double TS_Jumun_7);
                double.TryParse(form.TB_TS_Jumun_H.Text, out double TS_Jumun_8);
                double.TryParse(form.TB_TS_Jumun_I.Text, out double TS_Jumun_9);

                if (form.CBB_TS_Jumun_A.SelectedIndex == 0 || form.CBB_TS_Jumun_A.SelectedIndex == 1) TS_Jumun_1 = 0;
                if (form.CBB_TS_Jumun_B.SelectedIndex == 0 || form.CBB_TS_Jumun_B.SelectedIndex == 1) TS_Jumun_2 = 0;
                if (form.CBB_TS_Jumun_C.SelectedIndex == 0 || form.CBB_TS_Jumun_C.SelectedIndex == 1) TS_Jumun_3 = 0;
                if (form.CBB_TS_Jumun_D.SelectedIndex == 0 || form.CBB_TS_Jumun_D.SelectedIndex == 1) TS_Jumun_4 = 0;
                if (form.CBB_TS_Jumun_E.SelectedIndex == 0 || form.CBB_TS_Jumun_E.SelectedIndex == 1) TS_Jumun_5 = 0;
                if (form.CBB_TS_Jumun_F.SelectedIndex == 0 || form.CBB_TS_Jumun_F.SelectedIndex == 1) TS_Jumun_6 = 0;
                if (form.CBB_TS_Jumun_G.SelectedIndex == 0 || form.CBB_TS_Jumun_G.SelectedIndex == 1) TS_Jumun_7 = 0;
                if (form.CBB_TS_Jumun_H.SelectedIndex == 0 || form.CBB_TS_Jumun_H.SelectedIndex == 1) TS_Jumun_8 = 0;
                if (form.CBB_TS_Jumun_I.SelectedIndex == 0 || form.CBB_TS_Jumun_I.SelectedIndex == 1) TS_Jumun_9 = 0;

                Properties.Settings.Default.TB_TS_Jumun_A = TS_Jumun_1;
                Properties.Settings.Default.TB_TS_Jumun_B = TS_Jumun_2;
                Properties.Settings.Default.TB_TS_Jumun_C = TS_Jumun_3;
                Properties.Settings.Default.TB_TS_Jumun_D = TS_Jumun_4;
                Properties.Settings.Default.TB_TS_Jumun_E = TS_Jumun_5;
                Properties.Settings.Default.TB_TS_Jumun_F = TS_Jumun_6;
                Properties.Settings.Default.TB_TS_Jumun_G = TS_Jumun_7;
                Properties.Settings.Default.TB_TS_Jumun_H = TS_Jumun_8;
                Properties.Settings.Default.TB_TS_Jumun_I = TS_Jumun_9;

                form.TB_TS_Jumun_A.Text = TS_Jumun_1.ToString();
                form.TB_TS_Jumun_B.Text = TS_Jumun_2.ToString();
                form.TB_TS_Jumun_C.Text = TS_Jumun_3.ToString();
                form.TB_TS_Jumun_D.Text = TS_Jumun_4.ToString();
                form.TB_TS_Jumun_E.Text = TS_Jumun_5.ToString();
                form.TB_TS_Jumun_F.Text = TS_Jumun_6.ToString();
                form.TB_TS_Jumun_G.Text = TS_Jumun_7.ToString();
                form.TB_TS_Jumun_H.Text = TS_Jumun_8.ToString();
                form.TB_TS_Jumun_I.Text = TS_Jumun_9.ToString();
            }
            catch
            {
                저장에러();
            }

            Properties.Settings.Default.CB_TS_취소후 = form.CB_TS_취소후.Checked;
            Properties.Settings.Default.CB_TS_기준금 = form.CB_TS_기준금.Checked;

            Properties.Settings.Default.CB_TS_A = form.CB_TS_A.Checked;
            Properties.Settings.Default.CB_TS_B = form.CB_TS_B.Checked;
            Properties.Settings.Default.CB_TS_C = form.CB_TS_C.Checked;
            Properties.Settings.Default.CB_TS_D = form.CB_TS_D.Checked;
            Properties.Settings.Default.CB_TS_E = form.CB_TS_E.Checked;
            Properties.Settings.Default.CB_TS_F = form.CB_TS_F.Checked;
            Properties.Settings.Default.CB_TS_G = form.CB_TS_G.Checked;
            Properties.Settings.Default.CB_TS_H = form.CB_TS_H.Checked;
            Properties.Settings.Default.CB_TS_I = form.CB_TS_I.Checked;

            Properties.Settings.Default.CBB_TS_upper_A = form.CBB_TS_upper_A.SelectedIndex;
            Properties.Settings.Default.CBB_TS_upper_B = form.CBB_TS_upper_B.SelectedIndex;
            Properties.Settings.Default.CBB_TS_upper_C = form.CBB_TS_upper_C.SelectedIndex;
            Properties.Settings.Default.CBB_TS_upper_D = form.CBB_TS_upper_D.SelectedIndex;
            Properties.Settings.Default.CBB_TS_upper_E = form.CBB_TS_upper_E.SelectedIndex;
            Properties.Settings.Default.CBB_TS_upper_F = form.CBB_TS_upper_F.SelectedIndex;
            Properties.Settings.Default.CBB_TS_upper_G = form.CBB_TS_upper_G.SelectedIndex;
            Properties.Settings.Default.CBB_TS_upper_H = form.CBB_TS_upper_H.SelectedIndex;
            Properties.Settings.Default.CBB_TS_upper_I = form.CBB_TS_upper_I.SelectedIndex;

            Properties.Settings.Default.CBB_TS_ratio_A = form.CBB_TS_ratio_A.SelectedIndex;
            Properties.Settings.Default.CBB_TS_ratio_B = form.CBB_TS_ratio_B.SelectedIndex;
            Properties.Settings.Default.CBB_TS_ratio_C = form.CBB_TS_ratio_C.SelectedIndex;
            Properties.Settings.Default.CBB_TS_ratio_D = form.CBB_TS_ratio_D.SelectedIndex;
            Properties.Settings.Default.CBB_TS_ratio_E = form.CBB_TS_ratio_E.SelectedIndex;
            Properties.Settings.Default.CBB_TS_ratio_F = form.CBB_TS_ratio_F.SelectedIndex;
            Properties.Settings.Default.CBB_TS_ratio_G = form.CBB_TS_ratio_G.SelectedIndex;
            Properties.Settings.Default.CBB_TS_ratio_H = form.CBB_TS_ratio_H.SelectedIndex;
            Properties.Settings.Default.CBB_TS_ratio_I = form.CBB_TS_ratio_I.SelectedIndex;

            Properties.Settings.Default.CBB_TS_Jumun_A = form.CBB_TS_Jumun_A.SelectedIndex;
            Properties.Settings.Default.CBB_TS_Jumun_B = form.CBB_TS_Jumun_B.SelectedIndex;
            Properties.Settings.Default.CBB_TS_Jumun_C = form.CBB_TS_Jumun_C.SelectedIndex;
            Properties.Settings.Default.CBB_TS_Jumun_D = form.CBB_TS_Jumun_D.SelectedIndex;
            Properties.Settings.Default.CBB_TS_Jumun_E = form.CBB_TS_Jumun_E.SelectedIndex;
            Properties.Settings.Default.CBB_TS_Jumun_F = form.CBB_TS_Jumun_F.SelectedIndex;
            Properties.Settings.Default.CBB_TS_Jumun_G = form.CBB_TS_Jumun_G.SelectedIndex;
            Properties.Settings.Default.CBB_TS_Jumun_H = form.CBB_TS_Jumun_H.SelectedIndex;
            Properties.Settings.Default.CBB_TS_Jumun_I = form.CBB_TS_Jumun_I.SelectedIndex;

            Properties.Settings.Default.CB_TS_손실제한 = form.CB_TS_손실제한.Checked;

            try
            {
                double.TryParse(form.TB_ik_son_ratio_A.Text, out double ik_son_ratio_A);
                double.TryParse(form.TB_ik_son_ratio_B.Text, out double ik_son_ratio_B);
                double.TryParse(form.TB_ik_son_ratio_C.Text, out double ik_son_ratio_C);
                double.TryParse(form.TB_ik_son_ratio_D.Text, out double ik_son_ratio_D);
                double.TryParse(form.TB_ik_son_ratio_E.Text, out double ik_son_ratio_E);
                double.TryParse(form.TB_ik_son_ratio_F.Text, out double ik_son_ratio_F);
                double.TryParse(form.TB_ik_son_ratio_G.Text, out double ik_son_ratio_G);
                double.TryParse(form.TB_ik_son_ratio_H.Text, out double ik_son_ratio_H);
                double.TryParse(form.TB_ik_son_ratio_I.Text, out double ik_son_ratio_I);

                if (ik_son_ratio_A == 0) ik_son_ratio_A = 1;
                if (ik_son_ratio_B == 0) ik_son_ratio_B = 1;
                if (ik_son_ratio_C == 0) ik_son_ratio_C = 1;
                if (ik_son_ratio_D == 0) ik_son_ratio_D = 1;
                if (ik_son_ratio_E == 0) ik_son_ratio_E = 1;
                if (ik_son_ratio_F == 0) ik_son_ratio_F = 1;
                if (ik_son_ratio_G == 0) ik_son_ratio_G = 1;
                if (ik_son_ratio_H == 0) ik_son_ratio_H = 1;
                if (ik_son_ratio_I == 0) ik_son_ratio_I = 1;


                Properties.Settings.Default.TB_ik_son_ratio_A = ik_son_ratio_A;
                Properties.Settings.Default.TB_ik_son_ratio_B = ik_son_ratio_B;
                Properties.Settings.Default.TB_ik_son_ratio_C = ik_son_ratio_C;
                Properties.Settings.Default.TB_ik_son_ratio_D = ik_son_ratio_D;
                Properties.Settings.Default.TB_ik_son_ratio_E = ik_son_ratio_E;
                Properties.Settings.Default.TB_ik_son_ratio_F = ik_son_ratio_F;
                Properties.Settings.Default.TB_ik_son_ratio_G = ik_son_ratio_G;
                Properties.Settings.Default.TB_ik_son_ratio_H = ik_son_ratio_H;
                Properties.Settings.Default.TB_ik_son_ratio_I = ik_son_ratio_I;

                form.TB_ik_son_ratio_A.Text = ik_son_ratio_A.ToString();
                form.TB_ik_son_ratio_B.Text = ik_son_ratio_B.ToString();
                form.TB_ik_son_ratio_C.Text = ik_son_ratio_C.ToString();
                form.TB_ik_son_ratio_D.Text = ik_son_ratio_D.ToString();
                form.TB_ik_son_ratio_E.Text = ik_son_ratio_E.ToString();
                form.TB_ik_son_ratio_F.Text = ik_son_ratio_F.ToString();
                form.TB_ik_son_ratio_G.Text = ik_son_ratio_G.ToString();
                form.TB_ik_son_ratio_H.Text = ik_son_ratio_H.ToString();
                form.TB_ik_son_ratio_I.Text = ik_son_ratio_I.ToString();

                double.TryParse(form.TB_ik_son_A.Text, out double ik_son_A);
                double.TryParse(form.TB_ik_son_B.Text, out double ik_son_B);
                double.TryParse(form.TB_ik_son_C.Text, out double ik_son_C);
                double.TryParse(form.TB_ik_son_D.Text, out double ik_son_D);
                double.TryParse(form.TB_ik_son_E.Text, out double ik_son_E);
                double.TryParse(form.TB_ik_son_F.Text, out double ik_son_F);
                double.TryParse(form.TB_ik_son_G.Text, out double ik_son_G);
                double.TryParse(form.TB_ik_son_H.Text, out double ik_son_H);
                double.TryParse(form.TB_ik_son_I.Text, out double ik_son_I);

                Properties.Settings.Default.TB_ik_son_A = ik_son_A;
                Properties.Settings.Default.TB_ik_son_B = ik_son_B;
                Properties.Settings.Default.TB_ik_son_C = ik_son_C;
                Properties.Settings.Default.TB_ik_son_D = ik_son_D;
                Properties.Settings.Default.TB_ik_son_E = ik_son_E;
                Properties.Settings.Default.TB_ik_son_F = ik_son_F;
                Properties.Settings.Default.TB_ik_son_G = ik_son_G;
                Properties.Settings.Default.TB_ik_son_H = ik_son_H;
                Properties.Settings.Default.TB_ik_son_I = ik_son_I;

                form.TB_ik_son_A.Text = ik_son_A.ToString();
                form.TB_ik_son_B.Text = ik_son_B.ToString();
                form.TB_ik_son_C.Text = ik_son_C.ToString();
                form.TB_ik_son_D.Text = ik_son_D.ToString();
                form.TB_ik_son_E.Text = ik_son_E.ToString();
                form.TB_ik_son_F.Text = ik_son_F.ToString();
                form.TB_ik_son_G.Text = ik_son_G.ToString();
                form.TB_ik_son_H.Text = ik_son_H.ToString();
                form.TB_ik_son_I.Text = ik_son_I.ToString();

                int.TryParse(form.TB_신규횟수제한.Text, out int 횟수);
                Properties.Settings.Default.TB_신규횟수제한 = Math.Abs(횟수);
            }
            catch
            {
                저장에러();
            }

            Properties.Settings.Default.CB_익절재매수A = form.CB_익절재매수A.Checked;
            Properties.Settings.Default.CB_익절재매수B = form.CB_익절재매수B.Checked;
            Properties.Settings.Default.CB_익절재매수C = form.CB_익절재매수C.Checked;

            foreach (var code in stockBalanceList.ToList())
            {
                Stockbalance 잔고 = code.Value;
                if (잔고.시간청산반복_A > Properties.Settings.Default.MT_TimeSell_반복간격_A) 잔고.시간청산반복_A = Properties.Settings.Default.MT_TimeSell_반복간격_A;
                if (잔고.시간청산반복_B > Properties.Settings.Default.MT_TimeSell_반복간격_B) 잔고.시간청산반복_B = Properties.Settings.Default.MT_TimeSell_반복간격_B;
                if (잔고.시간청산반복_C > Properties.Settings.Default.MT_TimeSell_반복간격_C) 잔고.시간청산반복_C = Properties.Settings.Default.MT_TimeSell_반복간격_C;
            }

            Properties.Settings.Default.CB_TimeSell_기준금 = form.CB_TimeSell_기준금.Checked;

            Properties.Settings.Default.CB_TimeSell_A = form.CB_TimeSell_A.Checked;
            Properties.Settings.Default.CB_TimeSell_B = form.CB_TimeSell_B.Checked;
            Properties.Settings.Default.CB_TimeSell_C = form.CB_TimeSell_C.Checked;

            Properties.Settings.Default.CB_TimeSell_수익범위_choice_A = form.CB_TimeSell_수익범위_choice_A.Checked;
            Properties.Settings.Default.CB_TimeSell_수익범위_choice_B = form.CB_TimeSell_수익범위_choice_B.Checked;
            Properties.Settings.Default.CB_TimeSell_수익범위_choice_C = form.CB_TimeSell_수익범위_choice_C.Checked;

            Properties.Settings.Default.CB_scalping = form.CB_scalping.Checked;
            Properties.Settings.Default.CB_scalping_A = form.CB_scalping_A.Checked;
            Properties.Settings.Default.CB_scalping_B = form.CB_scalping_B.Checked;
            Properties.Settings.Default.CB_scalping_C = form.CB_scalping_C.Checked;

            Properties.Settings.Default.CB_scalping_A_1 = form.CB_scalping_A_1.Checked;
            Properties.Settings.Default.CB_scalping_A_2 = form.CB_scalping_A_2.Checked;
            Properties.Settings.Default.CB_scalping_A_3 = form.CB_scalping_A_3.Checked;
            Properties.Settings.Default.CB_scalping_A_4 = form.CB_scalping_A_4.Checked;
            Properties.Settings.Default.CB_scalping_A_5 = form.CB_scalping_A_5.Checked;
            Properties.Settings.Default.CB_scalping_A_6 = form.CB_scalping_A_6.Checked;
            Properties.Settings.Default.CB_scalping_A_7 = form.CB_scalping_A_7.Checked;
            Properties.Settings.Default.CB_scalping_A_8 = form.CB_scalping_A_8.Checked;
            Properties.Settings.Default.CB_scalping_A_9 = form.CB_scalping_A_9.Checked;

            Properties.Settings.Default.CB_scalping_B_1 = form.CB_scalping_B_1.Checked;
            Properties.Settings.Default.CB_scalping_B_2 = form.CB_scalping_B_2.Checked;
            Properties.Settings.Default.CB_scalping_B_3 = form.CB_scalping_B_3.Checked;
            Properties.Settings.Default.CB_scalping_B_4 = form.CB_scalping_B_4.Checked;
            Properties.Settings.Default.CB_scalping_B_5 = form.CB_scalping_B_5.Checked;
            Properties.Settings.Default.CB_scalping_B_6 = form.CB_scalping_B_6.Checked;
            Properties.Settings.Default.CB_scalping_B_7 = form.CB_scalping_B_7.Checked;
            Properties.Settings.Default.CB_scalping_B_8 = form.CB_scalping_B_8.Checked;
            Properties.Settings.Default.CB_scalping_B_9 = form.CB_scalping_B_9.Checked;

            Properties.Settings.Default.CB_scalping_C_1 = form.CB_scalping_C_1.Checked;
            Properties.Settings.Default.CB_scalping_C_2 = form.CB_scalping_C_2.Checked;
            Properties.Settings.Default.CB_scalping_C_3 = form.CB_scalping_C_3.Checked;
            Properties.Settings.Default.CB_scalping_C_4 = form.CB_scalping_C_4.Checked;
            Properties.Settings.Default.CB_scalping_C_5 = form.CB_scalping_C_5.Checked;
            Properties.Settings.Default.CB_scalping_C_6 = form.CB_scalping_C_6.Checked;
            Properties.Settings.Default.CB_scalping_C_7 = form.CB_scalping_C_7.Checked;
            Properties.Settings.Default.CB_scalping_C_8 = form.CB_scalping_C_8.Checked;
            Properties.Settings.Default.CB_scalping_C_9 = form.CB_scalping_C_9.Checked;

            Properties.Settings.Default.combo_sell_son_A = form.combo_sell_son_A.SelectedIndex;
            Properties.Settings.Default.combo_sell_son_B = form.combo_sell_son_B.SelectedIndex;
            Properties.Settings.Default.combo_sell_son_C = form.combo_sell_son_C.SelectedIndex;
            Properties.Settings.Default.combo_sell_son_D = form.combo_sell_son_D.SelectedIndex;
            Properties.Settings.Default.combo_sell_son_E = form.combo_sell_son_E.SelectedIndex;
            Properties.Settings.Default.combo_sell_son_F = form.combo_sell_son_F.SelectedIndex;

            Properties.Settings.Default.combo_sell_ratio_A = form.combo_sell_ratio_A.SelectedIndex;
            Properties.Settings.Default.combo_sell_ratio_B = form.combo_sell_ratio_B.SelectedIndex;
            Properties.Settings.Default.combo_sell_ratio_C = form.combo_sell_ratio_C.SelectedIndex;
            Properties.Settings.Default.combo_sell_ratio_D = form.combo_sell_ratio_D.SelectedIndex;
            Properties.Settings.Default.combo_sell_ratio_E = form.combo_sell_ratio_E.SelectedIndex;
            Properties.Settings.Default.combo_sell_ratio_F = form.combo_sell_ratio_F.SelectedIndex;

            Properties.Settings.Default.combo_sell_jumun_A = form.combo_sell_jumun_A.SelectedIndex;
            Properties.Settings.Default.combo_sell_jumun_B = form.combo_sell_jumun_B.SelectedIndex;
            Properties.Settings.Default.combo_sell_jumun_C = form.combo_sell_jumun_C.SelectedIndex;
            Properties.Settings.Default.combo_sell_jumun_D = form.combo_sell_jumun_D.SelectedIndex;
            Properties.Settings.Default.combo_sell_jumun_E = form.combo_sell_jumun_E.SelectedIndex;
            Properties.Settings.Default.combo_sell_jumun_F = form.combo_sell_jumun_F.SelectedIndex;

            if (Form1.form1.account_comboBox.SelectedIndex > -1)
            {
                Properties.Settings.Default.CB_new_A = form.CB_new_A.Checked;
                Properties.Settings.Default.CB_new_B = form.CB_new_B.Checked;
                Properties.Settings.Default.CB_new_C = form.CB_new_C.Checked;
            }

            Properties.Settings.Default.CB_new_recatch_A = form.CB_new_recatch_A.Checked;
            Properties.Settings.Default.CB_new_recatch_B = form.CB_new_recatch_B.Checked;
            Properties.Settings.Default.CB_new_recatch_C = form.CB_new_recatch_C.Checked;

            Properties.Settings.Default.CBB_ik_CancelOrder = form.CBB_ik_CancelOrder.SelectedIndex;

            Properties.Settings.Default.CB_ik_기준금 = form.CB_ik_기준금.Checked;

            Properties.Settings.Default.CB_ik_down_CancelOrder = form.CB_ik_down_CancelOrder.Checked;
            Properties.Settings.Default.CB_ik_down_기준금 = form.CB_ik_down_기준금.Checked;

            Properties.Settings.Default.CB_silson_choice_W = form.CB_silson_choice_W.Checked;
            Properties.Settings.Default.CB_silson_청산기준 = form.CB_silson_청산기준.Checked;
            Properties.Settings.Default.CB_예상수익_choice = form.CB_예상수익_choice.Checked;
            Properties.Settings.Default.CB_예상손실_choice = form.CB_예상손실_choice.Checked;
            Properties.Settings.Default.CB_예상손실_청산기준 = form.CB_예상손실_청산기준.Checked;
            Properties.Settings.Default.CB_예상수익_청산기준 = form.CB_예상수익_청산기준.Checked;

            Properties.Settings.Default.CB_ik_one_A = form.CB_ik_one_A.Checked;
            Properties.Settings.Default.CB_ik_one_B = form.CB_ik_one_B.Checked;
            Properties.Settings.Default.CB_ik_one_C = form.CB_ik_one_C.Checked;
            Properties.Settings.Default.CB_ik_one_D = form.CB_ik_one_D.Checked;
            Properties.Settings.Default.CB_ik_one_E = form.CB_ik_one_E.Checked;
            Properties.Settings.Default.CB_ik_one_F = form.CB_ik_one_F.Checked;
            Properties.Settings.Default.CB_ik_one_G = form.CB_ik_one_G.Checked;
            Properties.Settings.Default.CB_ik_one_H = form.CB_ik_one_H.Checked;
            Properties.Settings.Default.CB_ik_one_I = form.CB_ik_one_I.Checked;

            Properties.Settings.Default.CB_ik_A = form.CB_ik_A.Checked;
            Properties.Settings.Default.CB_ik_B = form.CB_ik_B.Checked;
            Properties.Settings.Default.CB_ik_C = form.CB_ik_C.Checked;
            Properties.Settings.Default.CB_ik_D = form.CB_ik_D.Checked;
            Properties.Settings.Default.CB_ik_E = form.CB_ik_E.Checked;
            Properties.Settings.Default.CB_ik_F = form.CB_ik_F.Checked;
            Properties.Settings.Default.CB_ik_G = form.CB_ik_G.Checked;
            Properties.Settings.Default.CB_ik_H = form.CB_ik_H.Checked;
            Properties.Settings.Default.CB_ik_I = form.CB_ik_I.Checked;

            Properties.Settings.Default.CB_ik_down_A = form.CB_ik_down_A.Checked;
            Properties.Settings.Default.CB_ik_down_B = form.CB_ik_down_B.Checked;
            Properties.Settings.Default.CB_ik_down_C = form.CB_ik_down_C.Checked;
            Properties.Settings.Default.CB_ik_down_D = form.CB_ik_down_D.Checked;
            Properties.Settings.Default.CB_ik_down_E = form.CB_ik_down_E.Checked;
            Properties.Settings.Default.CB_ik_down_F = form.CB_ik_down_F.Checked;
            Properties.Settings.Default.CB_ik_down_G = form.CB_ik_down_G.Checked;
            Properties.Settings.Default.CB_ik_down_H = form.CB_ik_down_H.Checked;
            Properties.Settings.Default.CB_ik_down_I = form.CB_ik_down_I.Checked;

            Properties.Settings.Default.CB_sell_time_overlap = form.CB_sell_time_overlap.Checked;
            Properties.Settings.Default.CB_sell_time_use = form.CB_sell_time_use.Checked;
            Properties.Settings.Default.CB_sell_time_Buystop = form.CB_sell_time_Buystop.Checked;
            Properties.Settings.Default.CB_sell_time_잔량취소 = form.CB_sell_time_잔량취소.Checked;

            Properties.Settings.Default.CB_sell_CancelOrder = form.CB_sell_CancelOrder.Checked;
            Properties.Settings.Default.CB_sell_기준금 = form.CB_sell_기준금.Checked;

            Properties.Settings.Default.CB_sell_use_A = form.CB_sell_use_A.Checked;
            Properties.Settings.Default.CB_sell_use_B = form.CB_sell_use_B.Checked;
            Properties.Settings.Default.CB_sell_use_C = form.CB_sell_use_C.Checked;
            Properties.Settings.Default.CB_sell_use_D = form.CB_sell_use_D.Checked;
            Properties.Settings.Default.CB_sell_use_E = form.CB_sell_use_E.Checked;
            Properties.Settings.Default.CB_sell_use_F = form.CB_sell_use_F.Checked;

            Properties.Settings.Default.CB_silson_overlap_W = form.CB_silson_overlap_W.Checked;
            Properties.Settings.Default.CB_silson_use_W = form.CB_silson_use_W.Checked;
            Properties.Settings.Default.CB_silson_Buystop_W = form.CB_silson_Buystop_W.Checked;
            Properties.Settings.Default.CB_silson_잔량취소 = form.CB_silson_잔량취소.Checked;

            Properties.Settings.Default.CB_예상수익_overlap = form.CB_예상수익_overlap.Checked;
            Properties.Settings.Default.CB_예상수익사용 = form.CB_예상수익사용.Checked;
            Properties.Settings.Default.CB_예상수익_Buystop = form.CB_예상수익_Buystop.Checked;
            Properties.Settings.Default.CB_예상수익_잔량취소 = form.CB_예상수익_잔량취소.Checked;

            Properties.Settings.Default.CB_예상손실_use = form.CB_예상손실_use.Checked;
            Properties.Settings.Default.CB_예상손실_overlap = form.CB_예상손실_overlap.Checked;
            Properties.Settings.Default.CB_예상손실_Buystop = form.CB_예상손실_Buystop.Checked;
            Properties.Settings.Default.CB_예상손실_잔량취소 = form.CB_예상손실_잔량취소.Checked;

            Properties.Settings.Default.CB_지수연동청산 = form.CB_지수연동청산.Checked;
            Properties.Settings.Default.CB_신규횟수제한 = form.CB_신규횟수제한.Checked;

            Form1.form1.CBscalping = Properties.Settings.Default.CB_scalping;
            GET.신규매수방법();
        }


        private void Form_Basic_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
                Form1.form1.CB_기본매매.Checked = false;
                Form1.FormBasic_Open = false;
                Properties.Settings.Default.CB_레이아웃고정_기본매매 = form.CB_레이아웃고정_기본매매.Checked;
                Properties.Settings.Default.CB_기본매매_시작위치저장 = form.CB_기본매매_시작위치저장.Checked;
                if (Properties.Settings.Default.CB_기본매매_시작위치저장) Properties.Settings.Default.WindowLocation = form.Location;

                e.Cancel = true;
                Hide();
            }
        }


        private void CB_레이아웃고정_기본매매_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CB_레이아웃고정_기본매매 = CB_레이아웃고정_기본매매.Checked;

            if (!CB_레이아웃고정_기본매매.Checked) LayoutChange.CBB_layout_SelectedIndex(-1);
            else LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
        }


        private void 계좌청산_만원n기준_CheckedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            CheckBox CB = (sender as CheckBox);
            if (CB.Checked)
            {
                CB.Text = "기준";
                CB.ForeColor = Color.Crimson;
            }
            else
            {
                CB.Text = "만원";
                CB.ForeColor = Color.Black;
            }

            if (sender.Equals(CB_예상수익_청산기준))
            {
                CB_예상수익TS_시작.Checked = CB_예상수익_청산기준.Checked;
            }
        }

        private void 계좌청산_checked_chainge(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);

            Form1.form1.체크박스_비프(sender);
            if (CB.Checked)
            {
                CB.Text = "■";
                if (sender.Equals(CB_TimeSell_A) || sender.Equals(CB_TimeSell_B) || sender.Equals(CB_TimeSell_C)) Form1.잔고시간_ON = true;
            }
            else
            {
                CB.Text = "□";

                if (sender.Equals(CB_sell_time_use) || sender.Equals(CB_silson_use_W) || sender.Equals(CB_예상수익사용) || sender.Equals(CB_예상손실_use))
                {
                    if (Form1.Acc.Count > 0)
                    {
                        switch (CB.Name.ToString())
                        {
                            case "CB_sell_time_use":
                                Form1.Acc[0].시간청산 = "시간청산";
                                Form1.Run_time = true;
                                break;
                            case "CB_silson_use_W":
                                Form1.Acc[0].실현손익금청산 = "실현손익금청산";
                                Form1.Run_silson_W = true;
                                Form1.매입금_silson_W = false;
                                Form1.Run_silson_trading = false;
                                break;
                            case "CB_예상손실_use":
                                Form1.Acc[0].예상손실청산 = "예상손실청산";
                                Form1.Run_예상손실 = true;
                                Form1.매입금_예상손실 = false;
                                Form1.Run_예상손실_trading = false;
                                break;
                            case "CB_예상수익사용":
                                Form1.Acc[0].예상수익청산 = "예상수익청산";
                                Form1.Run_예상수익 = true;
                                Form1.매입금_예상손익 = false;
                                Form1.Run_예상수익_trading = false;
                                break;
                        }
                    }
                }

                if (sender.Equals(CB_TimeSell_A) || sender.Equals(CB_TimeSell_B) || sender.Equals(CB_TimeSell_C))
                    if (!CB_TimeSell_A.Checked && !CB_TimeSell_B.Checked && !CB_TimeSell_C.Checked) Form1.잔고시간_ON = false;

                if (sender.Equals(CB_sell_time_use)) Properties.Settings.Default.CB_sell_time_use = false;
                if (sender.Equals(CB_silson_use_W)) Properties.Settings.Default.CB_silson_use_W = false;
                if (sender.Equals(CB_예상수익사용)) Properties.Settings.Default.CB_예상수익사용 = false;
                if (sender.Equals(CB_예상손실_use)) Properties.Settings.Default.CB_예상손실_use = false;
                if (sender.Equals(CB_TimeSell_A)) Properties.Settings.Default.CB_TimeSell_A = false;
                if (sender.Equals(CB_TimeSell_B)) Properties.Settings.Default.CB_TimeSell_B = false;
                if (sender.Equals(CB_TimeSell_C)) Properties.Settings.Default.CB_TimeSell_C = false;
            }
        }


        private void CheckBox_Checked_Changed(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);
            CheckBox CB = (sender as CheckBox);
            if (CB.Checked)
            {
                CB.Text = "■";
            }
            else
            {
                CB.Text = "□";

                if (Form1.FormBasic_Open)
                {
                    if (sender.Equals(CB_ik_A)) Properties.Settings.Default.CB_ik_A = false;
                    if (sender.Equals(CB_ik_B)) Properties.Settings.Default.CB_ik_B = false;
                    if (sender.Equals(CB_ik_C)) Properties.Settings.Default.CB_ik_C = false;
                    if (sender.Equals(CB_ik_D)) Properties.Settings.Default.CB_ik_D = false;
                    if (sender.Equals(CB_ik_E)) Properties.Settings.Default.CB_ik_E = false;
                    if (sender.Equals(CB_ik_F)) Properties.Settings.Default.CB_ik_F = false;
                    if (sender.Equals(CB_ik_G)) Properties.Settings.Default.CB_ik_G = false;
                    if (sender.Equals(CB_ik_H)) Properties.Settings.Default.CB_ik_H = false;
                    if (sender.Equals(CB_ik_I)) Properties.Settings.Default.CB_ik_I = false;

                    if (sender.Equals(CB_ik_one_A)) Properties.Settings.Default.CB_ik_one_A = false;
                    if (sender.Equals(CB_ik_one_B)) Properties.Settings.Default.CB_ik_one_B = false;
                    if (sender.Equals(CB_ik_one_C)) Properties.Settings.Default.CB_ik_one_C = false;
                    if (sender.Equals(CB_ik_one_D)) Properties.Settings.Default.CB_ik_one_D = false;
                    if (sender.Equals(CB_ik_one_E)) Properties.Settings.Default.CB_ik_one_E = false;
                    if (sender.Equals(CB_ik_one_F)) Properties.Settings.Default.CB_ik_one_F = false;
                    if (sender.Equals(CB_ik_one_G)) Properties.Settings.Default.CB_ik_one_G = false;
                    if (sender.Equals(CB_ik_one_H)) Properties.Settings.Default.CB_ik_one_H = false;
                    if (sender.Equals(CB_ik_one_I)) Properties.Settings.Default.CB_ik_one_I = false;

                    if (sender.Equals(CB_ik_down_A)) Properties.Settings.Default.CB_ik_down_A = false;
                    if (sender.Equals(CB_ik_down_B)) Properties.Settings.Default.CB_ik_down_B = false;
                    if (sender.Equals(CB_ik_down_C)) Properties.Settings.Default.CB_ik_down_C = false;
                    if (sender.Equals(CB_ik_down_D)) Properties.Settings.Default.CB_ik_down_D = false;
                    if (sender.Equals(CB_ik_down_E)) Properties.Settings.Default.CB_ik_down_E = false;
                    if (sender.Equals(CB_ik_down_F)) Properties.Settings.Default.CB_ik_down_F = false;
                    if (sender.Equals(CB_ik_down_G)) Properties.Settings.Default.CB_ik_down_G = false;
                    if (sender.Equals(CB_ik_down_H)) Properties.Settings.Default.CB_ik_down_H = false;
                    if (sender.Equals(CB_ik_down_I)) Properties.Settings.Default.CB_ik_down_I = false;
                }
            }
        }

        private void CheckBox_Check_TEXT_Changed(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);
            CheckBox CB = (sender as CheckBox);
            string text = CB.Text.Substring(1);
            string 지수연동 = CB.Text;
            if (CB.Checked)
            {
                CB.Text = "■" + text;

                if (sender.Equals(CB_지수연동범위사용)) CB.Text = 지수연동.Substring(0, CB.Text.Length - 1) + "■";
                if (sender.Equals(CB_지수연동청산)) Form1.순매수조회 = true;

                if (sender.Equals(CB_ik_기준금)) CB.ForeColor = Color.Red;
                if (sender.Equals(CB_ik_down_기준금)) CB.ForeColor = Color.Red;
                if (sender.Equals(CB_sell_기준금)) CB.ForeColor = Color.Red;
                if (sender.Equals(CB_TimeSell_기준금)) CB.ForeColor = Color.Red;
                if (sender.Equals(CB_TS_기준금)) CB.ForeColor = Color.Crimson;

                if (sender.Equals(CB_익절재매수A)) CB_new_recatch_A.Checked = true;
                if (sender.Equals(CB_익절재매수B)) CB_new_recatch_B.Checked = true;
                if (sender.Equals(CB_익절재매수C)) CB_new_recatch_C.Checked = true;

            }
            else
            {
                CB.Text = "□" + text;

                if (sender.Equals(CB_지수연동범위사용))
                {
                    CB.Text = 지수연동.Substring(0, CB.Text.Length - 1) + "□";
                }

                if (sender.Equals(CB_지수연동청산)) Form1.순매수조회변경();

                if (sender.Equals(CB_ik_기준금)) CB.ForeColor = Color.Black;
                if (sender.Equals(CB_ik_down_기준금)) CB.ForeColor = Color.Black;
                if (sender.Equals(CB_sell_기준금)) CB.ForeColor = Color.Black;
                if (sender.Equals(CB_TimeSell_기준금)) CB.ForeColor = Color.Black;
                if (sender.Equals(CB_TS_기준금)) CB.ForeColor = Color.Black;

                if (sender.Equals(CB_익절재매수A)) CB_new_recatch_A.Checked = false;
                if (sender.Equals(CB_익절재매수B)) CB_new_recatch_B.Checked = false;
                if (sender.Equals(CB_익절재매수C)) CB_new_recatch_C.Checked = false;

                if (sender.Equals(CB_sell_use_A)) Properties.Settings.Default.CB_sell_use_A = false;
                if (sender.Equals(CB_sell_use_B)) Properties.Settings.Default.CB_sell_use_B = false;
                if (sender.Equals(CB_sell_use_C)) Properties.Settings.Default.CB_sell_use_C = false;
                if (sender.Equals(CB_sell_use_D)) Properties.Settings.Default.CB_sell_use_D = false;
                if (sender.Equals(CB_sell_use_E)) Properties.Settings.Default.CB_sell_use_E = false;
                if (sender.Equals(CB_sell_use_F)) Properties.Settings.Default.CB_sell_use_F = false;

                if (sender.Equals(CB_TimeSell_A)) Properties.Settings.Default.CB_TimeSell_A = false;
                if (sender.Equals(CB_TimeSell_B)) Properties.Settings.Default.CB_TimeSell_B = false;
                if (sender.Equals(CB_TimeSell_C)) Properties.Settings.Default.CB_TimeSell_C = false;

                foreach (var 잔고 in Form1.stockBalanceList.ToList())
                {
                    if (sender.Equals(CB_TimeSell_A)) 잔고.Value.시간청산동작_A = "A";
                    if (sender.Equals(CB_TimeSell_B)) 잔고.Value.시간청산동작_B = "B";
                    if (sender.Equals(CB_TimeSell_C)) 잔고.Value.시간청산동작_C = "C";
                }
            }
        }

        private void NewCheckBox_Check_재진입_Changed(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);
            CheckBox CB = (sender as CheckBox);
            string text_a = CB.Text.Substring(0, 3);
            string text_b = CB.Text.Substring(4);

            if (CB.Checked)
            {
                CB.Text = text_a + "■" + text_b;
            }
            else
            {
                CB.Text = text_a + "□" + text_b;
            }
        }

        private void Text_Zero(object sender, EventArgs e)
        {
            TextBox TB = sender as TextBox;

            if (CB_scalping.Checked)
            {
                if (!TB.Text.Equals("0"))
                    TB.Text = "0";
            }

            TextValue.TextBox_빨파검_소수2자리제한(sender); //textbox 의 색표시  사용
        }


        private void combo_new_or_SelectedIndexChanged(object sender, EventArgs e) // 신규매수 콤보박스 동작 제한
        {
            if (Form1.FormBasic_Open)
            {
                if (sender.Equals(combo_new_or_A))
                {
                    CB_new_A.Checked = false;
                    Form1.NewCatch_List_A.Clear();
                }

                if (sender.Equals(combo_new_or_B))
                {
                    int indexer = Properties.Settings.Default.combo_new_or_B;

                    if (CB_new_B.Checked && (indexer.Equals(1) || indexer.Equals(3)) && (combo_new_or_B.SelectedIndex == 0 || combo_new_or_B.SelectedIndex == 2))
                    {
                        CB_new_A.Checked = false;
                    }

                    if ((indexer.Equals(1) || indexer.Equals(3)) && (combo_new_or_B.SelectedIndex == 0 || combo_new_or_B.SelectedIndex == 2))
                    {
                        if (combo_new_or_C.SelectedIndex.Equals(1))
                        {
                            combo_new_or_C.SelectedIndex = 0;
                        }
                        else if (combo_new_or_C.SelectedIndex.Equals(3))
                        {
                            combo_new_or_C.SelectedIndex = 2;
                        }
                    }

                    if (combo_new_or_C.SelectedIndex == 1 || combo_new_or_C.SelectedIndex == 3)
                    {
                        CB_new_A.Checked = false;
                        CB_new_C.Checked = false;
                    }

                    CB_new_B.Checked = false;
                    Form1.NewCatch_List_B.Clear();
                }

                if (sender.Equals(combo_new_or_C))
                {
                    int indexer = Properties.Settings.Default.combo_new_or_C;
                    int id = Properties.Settings.Default.combo_new_or_B;

                    if ((indexer.Equals(0) || indexer.Equals(2)) && (combo_new_or_C.SelectedIndex == 1 || combo_new_or_C.SelectedIndex == 3))
                    {
                        if (combo_new_or_B.SelectedIndex.Equals(0))
                        {
                            combo_new_or_B.SelectedIndex = 1;
                        }
                        else if (combo_new_or_B.SelectedIndex.Equals(2))
                        {
                            combo_new_or_B.SelectedIndex = 3;
                        }
                    }

                    if (CB_new_B.Checked && (id.Equals(0) || id.Equals(2)) && (combo_new_or_B.SelectedIndex == 1 || combo_new_or_B.SelectedIndex == 3))
                    {
                        CB_new_A.Checked = false;
                        CB_new_B.Checked = false;
                    }

                    CB_new_C.Checked = false;
                    Form1.NewCatch_List_C.Clear();
                }
            }
        }

        private void CB_new_rebuy_CheckedChanged(object sender, EventArgs e) //재매수 체크박스 변화 
        {
            Form1.form1.체크박스_비프(sender);
            CheckBox CB = sender as CheckBox;
            string Text = CB.Text.Substring(1);

            if ((sender as CheckBox).Checked)
            {
                CB.Text = "■" + Text;

            }
            else
            {
                CB.Text = "□" + Text;
            }

            if (Form1.FormBasic_Open)
            {
                foreach (var code in Form1.stockBalanceList.ToList())
                {
                    Stockbalance 잔고 = code.Value;
                    if (CB_new_rebuy.Checked)
                    {
                        잔고.재매수 = Properties.Settings.Default.MTB_new_rebuytime;
                    }
                    else
                    {
                        잔고.재매수 = 10;
                    }
                }
            }
        }

        private void CBB_jumun_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrint.CBB_jumun_SelectedIndex(sender);
        }

        private void CB_예상수익TS_CheckedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);
            CheckBox CB = (sender as CheckBox);

            if (sender.Equals(CB_예상수익TS))
            {
                string text = CB.Text.Substring(1);
                if (CB.Checked)
                {
                    CB.Text = "■" + text;

                    CB_예상수익사용.Enabled = false;
                    CB_예상수익사용.Checked = true;

                    TB_예상수익_ik_1.Text = "-100";
                    TB_예상수익_ik_2.Text = "100";

                    TB_예상수익_ik_1.Enabled = false;
                    CB_예상수익_choice.Enabled = false;
                    TB_예상수익_ik_2.Enabled = false;
                }
                else
                {
                    CB.Text = "□" + text;
                    CB_예상수익사용.Enabled = true;

                    TB_예상수익_ik_1.Enabled = true;
                    CB_예상수익_choice.Enabled = true;
                    TB_예상수익_ik_2.Enabled = true;

                    Properties.Settings.Default.CB_예상수익TS = false;
                    Properties.Settings.Default.CB_예상수익사용 = false;
                    CB_예상수익사용.Checked = false;
                }
            }

            if (sender.Equals(CB_예상수익TS_시작))
            {
                string text = CB.Text.Substring(2);
                if (CB.Checked)
                {
                    CB_예상수익_청산기준.Checked = true;
                    CB.Text = "기준" + text;
                    CB.ForeColor = Color.Crimson;
                    LB_예상수익TS.Text = "기준 하락시 청산";
                    LB_예상수익TS.ForeColor = Color.Crimson;
                }
                else
                {
                    CB_예상수익_청산기준.Checked = false;
                    CB.Text = "만원" + text;
                    CB.ForeColor = Color.Black;
                    LB_예상수익TS.Text = "만원 하락시 청산";
                    LB_예상수익TS.ForeColor = Color.Black;
                }
            }
        }

        private void CBB_TimeSell_TC_DropDownClosed(object sender, EventArgs e)
        {
            CBB_TimeSell_TC_DropDownClosed_(sender);
        }

        private void CBB_TimeSell_TC_DropDownClosed_(object sender)
        {
            ComboBox CBB = CBB_TimeSell_start_A;
            TextBox TB = TB_TimeSell_start_A;
            TextBox 거래일 = TB_TimeSell_거래일_A;

            if (sender.Equals(CBB_TimeSell_start_B))
            {
                CBB = CBB_TimeSell_start_B;
                TB = TB_TimeSell_start_B;
                거래일 = TB_TimeSell_거래일_B;
            }

            if (sender.Equals(CBB_TimeSell_start_C))
            {
                CBB = CBB_TimeSell_start_C;
                TB = TB_TimeSell_start_C;
                거래일 = TB_TimeSell_거래일_C;
            }

            if (CBB.SelectedIndex == 0)
            {
                TB.Text = "090500";
                거래일.Enabled = true;
            }
            else
            {
                TB.Text = "600";
                거래일.Enabled = false;
                거래일.Text = "0";
            }
        }


        private void combo_condition_MouseHover(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            toolTip1.SetToolTip(combo, combo.Text);
        }

        private void BT_기본매매저장_Click(object sender, EventArgs e)
        {
            Form1.form1.Select();
            Form1.MBC_sender = (sender as Button).Name;
            Form1.중요메세지("기본매매", "기본매매 설정을 저장 하시겠습니까?");
        }

        private void CBB_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.FormBasic_Open)
            {
                Form1.비프음("체크");

                if (CB_scalping.Checked)
                {
                    if (sender.Equals(combo_ik_A)) combo_ik_A.SelectedIndex = 0;
                    if (sender.Equals(combo_ik_B)) combo_ik_B.SelectedIndex = 0;
                    if (sender.Equals(combo_ik_C)) combo_ik_C.SelectedIndex = 0;
                    if (sender.Equals(combo_ik_D)) combo_ik_D.SelectedIndex = 0;
                    if (sender.Equals(combo_ik_E)) combo_ik_E.SelectedIndex = 0;
                    if (sender.Equals(combo_ik_F)) combo_ik_F.SelectedIndex = 0;
                    if (sender.Equals(combo_ik_G)) combo_ik_G.SelectedIndex = 0;
                    if (sender.Equals(combo_ik_H)) combo_ik_H.SelectedIndex = 0;
                    if (sender.Equals(combo_ik_I)) combo_ik_I.SelectedIndex = 0;
                    if (sender.Equals(combo_ik_ratio_A)) combo_ik_ratio_A.SelectedIndex = 3;
                    if (sender.Equals(combo_ik_ratio_B)) combo_ik_ratio_B.SelectedIndex = 3;
                    if (sender.Equals(combo_ik_ratio_C)) combo_ik_ratio_C.SelectedIndex = 3;
                    if (sender.Equals(combo_ik_ratio_D)) combo_ik_ratio_D.SelectedIndex = 3;
                    if (sender.Equals(combo_ik_ratio_E)) combo_ik_ratio_E.SelectedIndex = 3;
                    if (sender.Equals(combo_ik_ratio_F)) combo_ik_ratio_F.SelectedIndex = 3;
                    if (sender.Equals(combo_ik_ratio_G)) combo_ik_ratio_G.SelectedIndex = 3;
                    if (sender.Equals(combo_ik_ratio_H)) combo_ik_ratio_H.SelectedIndex = 3;
                    if (sender.Equals(combo_ik_ratio_I)) combo_ik_ratio_I.SelectedIndex = 3;
                    if (sender.Equals(combo_ik_jumun_A)) combo_ik_jumun_A.SelectedIndex = -1;
                    if (sender.Equals(combo_ik_jumun_B)) combo_ik_jumun_B.SelectedIndex = -1;
                    if (sender.Equals(combo_ik_jumun_C)) combo_ik_jumun_C.SelectedIndex = -1;
                    if (sender.Equals(combo_ik_jumun_D)) combo_ik_jumun_D.SelectedIndex = -1;
                    if (sender.Equals(combo_ik_jumun_E)) combo_ik_jumun_E.SelectedIndex = -1;
                    if (sender.Equals(combo_ik_jumun_F)) combo_ik_jumun_F.SelectedIndex = -1;
                    if (sender.Equals(combo_ik_jumun_G)) combo_ik_jumun_G.SelectedIndex = -1;
                    if (sender.Equals(combo_ik_jumun_H)) combo_ik_jumun_H.SelectedIndex = -1;
                    if (sender.Equals(combo_ik_jumun_I)) combo_ik_jumun_I.SelectedIndex = -1;
                }

                if (sender.Equals(CBB_TS_upper_A)) { CBB_TS_down_A.SelectedIndex = CBB_TS_upper_A.SelectedIndex; }
                if (sender.Equals(CBB_TS_upper_B)) { CBB_TS_down_B.SelectedIndex = CBB_TS_upper_B.SelectedIndex; }
                if (sender.Equals(CBB_TS_upper_C)) { CBB_TS_down_C.SelectedIndex = CBB_TS_upper_C.SelectedIndex; }
                if (sender.Equals(CBB_TS_upper_D)) { CBB_TS_down_D.SelectedIndex = CBB_TS_upper_D.SelectedIndex; }
                if (sender.Equals(CBB_TS_upper_E)) { CBB_TS_down_E.SelectedIndex = CBB_TS_upper_E.SelectedIndex; }
                if (sender.Equals(CBB_TS_upper_F)) { CBB_TS_down_F.SelectedIndex = CBB_TS_upper_F.SelectedIndex; }
                if (sender.Equals(CBB_TS_upper_G)) { CBB_TS_down_G.SelectedIndex = CBB_TS_upper_G.SelectedIndex; }
                if (sender.Equals(CBB_TS_upper_H)) { CBB_TS_down_H.SelectedIndex = CBB_TS_upper_H.SelectedIndex; }
                if (sender.Equals(CBB_TS_upper_I)) { CBB_TS_down_I.SelectedIndex = CBB_TS_upper_I.SelectedIndex; }
            }
        }

        private void CBB_TimeSell_수익구분_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.FormBasic_Open) Form1.비프음("체크");

            FormPrint.CBB_suik_DropDownClosed(sender);
        }

        //I.OR
        //I.AND
        //D.OR
        //D.AND
        private void CB_condition_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);
            if (CB.Checked) CB.Text = "■";
            else CB.Text = "□";

            if (Form1.FormBasic_Open)
            {
                if (sender.Equals(CB_new_A))
                {
                    if (CB_new_A.Checked)
                    {
                        if (!CB_new_B.Checked && !CB_new_C.Checked) Run();
                        else if (CB_new_B.Checked && (combo_new_or_B.SelectedIndex == 0 || combo_new_or_B.SelectedIndex == 2) && CB_new_C.Checked && (combo_new_or_C.SelectedIndex == 0 || combo_new_or_C.SelectedIndex == 2))
                        {
                            Run();
                        }
                        else if (CB_new_B.Checked && (combo_new_or_B.SelectedIndex == 0 || combo_new_or_B.SelectedIndex == 2))
                        {
                            Run();
                        }
                    }
                    else
                    {
                        if (!CB_new_B.Checked && !CB_new_C.Checked)
                        {
                            Run();
                        }
                        else if (CB_new_B.Checked && (combo_new_or_B.SelectedIndex == 0 || combo_new_or_B.SelectedIndex == 2) && CB_new_C.Checked && (combo_new_or_C.SelectedIndex == 0 || combo_new_or_C.SelectedIndex == 2))
                        {
                            Run();
                        }
                        else if (CB_new_B.Checked && (combo_new_or_B.SelectedIndex == 0 || combo_new_or_B.SelectedIndex == 2))//B - OR
                        {
                            Run();
                        }
                        else if (CB_new_B.Checked && (combo_new_or_B.SelectedIndex == 1 || combo_new_or_B.SelectedIndex == 3) && CB_new_C.Checked && (combo_new_or_C.SelectedIndex == 1 || combo_new_or_C.SelectedIndex == 3))// B - AND
                        {
                            Form1.알림창("[ 신규매수알림 ]\n\nAND 사용 시에는 신규매수B 와 신규매수C 를 먼저 정지 하세요.", 10, false);
                            CB_new_A.Checked = true;
                        }
                        else if (CB_new_B.Checked && (combo_new_or_B.SelectedIndex == 1 || combo_new_or_B.SelectedIndex == 3))
                        {
                            Form1.알림창("[ 신규매수알림 ]\n\nAND 사용 시에는 신규매수B 와 신규매수C 를 먼저 정지 하세요.", 10, false);
                            CB_new_A.Checked = true;
                        }
                    }
                }

                if (sender.Equals(CB_new_B))
                {
                    if (combo_new_or_B.SelectedIndex == 1 || combo_new_or_B.SelectedIndex == 3) //AND
                    {
                        if (CB_new_B.Checked)
                        {
                            if (CB_new_A.Checked)
                            {
                                if (!CB_new_C.Checked) Run();
                                else if (CB_new_C.Checked && (combo_new_or_C.SelectedIndex == 0 || combo_new_or_C.SelectedIndex == 2)) Run();
                            }
                            else
                            {
                                Form1.알림창("[ 신규매수알림 ]\n\nAND 사용 시에는 신규매수A 를 먼저 실행해 주세요.", 10, false);
                                CB_new_B.Checked = false;
                            }
                        }
                        else
                        {
                            if (combo_new_or_C.SelectedIndex == 1 || combo_new_or_C.SelectedIndex == 3)
                            {
                                if (CB_new_C.Checked)
                                {
                                    CB_new_B.Checked = true;
                                    Form1.알림창("[ 신규매수알림 ]\n\nAND 사용 시에는 신규매수C 를 먼저 정지 하세요.", 10, false);
                                }
                                else
                                {
                                    if (CB_new_A.Checked)
                                    {
                                        if (Form1.form1.RB_buy_run.Checked)
                                        {
                                            Form1.form1.RB_buy_run.Checked = false;
                                            Form1.알림창("[ 신규매수알림 ]\n\n매수가 정지 됩니다.", 10, false);
                                        }
                                        Run();
                                    }
                                }
                            }
                            else
                            {
                                Run();
                            }
                        }
                    }
                    else
                    {
                        Run();
                    }
                }

                if (sender.Equals(CB_new_C))
                {
                    if (combo_new_or_C.SelectedIndex == 1 || combo_new_or_C.SelectedIndex == 3) //AND
                    {
                        if (CB_new_C.Checked)
                        {
                            if (!CB_new_A.Checked || !CB_new_B.Checked)
                            {
                                Form1.알림창("[ 신규매수알림 ]\n\nAND 사용 시에는 신규매수A 와 신규매수B 를 먼저 실행해 주세요.", 10, false);
                                CB_new_C.Checked = false;
                                return;
                            }
                            else
                            {
                                if (CB_new_A.Checked && CB_new_B.Checked) Run();
                            }
                        }
                        else
                        {
                            if (CB_new_B.Checked)
                            {
                                if (Form1.form1.RB_buy_run.Checked)
                                {
                                    Form1.form1.RB_buy_run.Checked = false;
                                    Form1.알림창("[ 신규매수알림 ]\n\n매수가 정지 됩니다.", 10, false);
                                }
                                Run();
                            }
                        }
                    }
                    else
                    {
                        Run();
                    }
                }

                void Run()
                {
                    Condition_Management.CB_condition_CheckedChanged(sender);
                    Form1.시장가탐색 = Condition_Management.시장가대금탐색();

                    if (!CB.Checked)
                    {
                        if (sender.Equals(CB_new_A)) Properties.Settings.Default.CB_new_A = false;
                        if (sender.Equals(CB_new_B)) Properties.Settings.Default.CB_new_B = false;
                        if (sender.Equals(CB_new_C)) Properties.Settings.Default.CB_new_C = false;
                    }
                }
            }
        }

        private void combo_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Form1.FormBasic_Open) Condition_Management.combo_condition_SelectedIndexChanged(sender);
        }

        private void combo_Condition_Add(object sender, EventArgs e)
        {
            Condition_Management.Condition_Add(sender);
        }

        private void combo_Condition_TextChanged(object sender, EventArgs e)
        {
            Condition_Management.Condition_TextChanged(sender);
        }

        private void TextChange_매매방법(object sender, EventArgs e)
        {
            FormPrint.TextChange_매매방법(sender);
        }

        private void TextBox_소수자리제한(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_소수자리제한(sender);
        }

        private void TextBox_양수소수자리제한(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_양수소수자리제한(sender);
        }

        private void TextBox_빨파검(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_빨파검(sender);
        }

        public void TextBox_빨파검_소수2자리제한(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_빨파검_소수2자리제한(sender);
        }

        private void TextBox_음수만입력_소수2자리제한(object sender, EventArgs e) // 사용
        {
            TextValue.TextBox_음수만입력_소수2자리제한(sender);
        }

        private void _양수음수소수_키프레스(object sender, KeyPressEventArgs e) // 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, true); // textbox 에 양수, 음수 , 소수  숫자만 입력 받을수 있음 
        }

        private void _양수소수_키프레스(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, false); // textbox 에 양수 , 소수 숫자만 입력 받을수 있음
        }

        private void 숫자콤마넣기_TextChanged(object sender, EventArgs e)
        {
            TextValue.숫자콤마넣기_TextChanged(sender);
        }

        private void TextBox_양실수만(object sender, EventArgs e)
        {
            TextValue.TextBox_양실수만(sender);
        }

        private void _양수실수_키프레스(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, false); // textbox 에 양수 , 실수 숫자만 입력 받을수 있음
        }

        private void _양수음수실수_키프레스(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, true); // textbox 에 양수, 음수 , 실수 숫자만 입력 받을수 있음
        }



        ///////////////////////////////////////////////////////////////////////////////////////////
        private void CB_트레일링스탑_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);
            Form1.form1.체크박스_비프(sender);

            string text = CB.Text.Split(' ')[0];
            if (CB.Checked)
            {
                CB.Text = text + " run";
                CB.ForeColor = Color.Red;

                if (sender.Equals(CB_TS_A)) { CB_ik_down_A.Checked = false; CB_ik_down_A.Enabled = false; CB_ik_one_A.Checked = false; CB_ik_A.Checked = false; CB_ik_one_A.Enabled = false; CB_ik_A.Enabled = false; combo_ik_jumun_A.Enabled = false; CB_ik_one_A.Text = "T"; CB_ik_A.Text = "S"; }
                if (sender.Equals(CB_TS_B)) { CB_ik_down_B.Checked = false; CB_ik_down_B.Enabled = false; CB_ik_one_B.Checked = false; CB_ik_B.Checked = false; CB_ik_one_B.Enabled = false; CB_ik_B.Enabled = false; combo_ik_jumun_B.Enabled = false; CB_ik_one_B.Text = "T"; CB_ik_B.Text = "S"; }
                if (sender.Equals(CB_TS_C)) { CB_ik_down_C.Checked = false; CB_ik_down_C.Enabled = false; CB_ik_one_C.Checked = false; CB_ik_C.Checked = false; CB_ik_one_C.Enabled = false; CB_ik_C.Enabled = false; combo_ik_jumun_C.Enabled = false; CB_ik_one_C.Text = "T"; CB_ik_C.Text = "S"; }
                if (sender.Equals(CB_TS_D)) { CB_ik_down_D.Checked = false; CB_ik_down_D.Enabled = false; CB_ik_one_D.Checked = false; CB_ik_D.Checked = false; CB_ik_one_D.Enabled = false; CB_ik_D.Enabled = false; combo_ik_jumun_D.Enabled = false; CB_ik_one_D.Text = "T"; CB_ik_D.Text = "S"; }
                if (sender.Equals(CB_TS_E)) { CB_ik_down_E.Checked = false; CB_ik_down_E.Enabled = false; CB_ik_one_E.Checked = false; CB_ik_E.Checked = false; CB_ik_one_E.Enabled = false; CB_ik_E.Enabled = false; combo_ik_jumun_E.Enabled = false; CB_ik_one_E.Text = "T"; CB_ik_E.Text = "S"; }
                if (sender.Equals(CB_TS_F)) { CB_ik_down_F.Checked = false; CB_ik_down_F.Enabled = false; CB_ik_one_F.Checked = false; CB_ik_F.Checked = false; CB_ik_one_F.Enabled = false; CB_ik_F.Enabled = false; combo_ik_jumun_F.Enabled = false; CB_ik_one_F.Text = "T"; CB_ik_F.Text = "S"; }
                if (sender.Equals(CB_TS_G)) { CB_ik_down_G.Checked = false; CB_ik_down_G.Enabled = false; CB_ik_one_G.Checked = false; CB_ik_G.Checked = false; CB_ik_one_G.Enabled = false; CB_ik_G.Enabled = false; combo_ik_jumun_G.Enabled = false; CB_ik_one_G.Text = "T"; CB_ik_G.Text = "S"; }
                if (sender.Equals(CB_TS_H)) { CB_ik_down_H.Checked = false; CB_ik_down_H.Enabled = false; CB_ik_one_H.Checked = false; CB_ik_H.Checked = false; CB_ik_one_H.Enabled = false; CB_ik_H.Enabled = false; combo_ik_jumun_H.Enabled = false; CB_ik_one_H.Text = "T"; CB_ik_H.Text = "S"; }
                if (sender.Equals(CB_TS_I)) { CB_ik_down_I.Checked = false; CB_ik_down_I.Enabled = false; CB_ik_one_I.Checked = false; CB_ik_I.Checked = false; CB_ik_one_I.Enabled = false; CB_ik_I.Enabled = false; combo_ik_jumun_I.Enabled = false; CB_ik_one_I.Text = "T"; CB_ik_I.Text = "S"; }
            }
            else
            {
                CB.Text = text + " off";
                CB.ForeColor = Color.Black;

                if (sender.Equals(CB_TS_A)) { CB_ik_one_A.Text = "□"; CB_ik_A.Text = "□"; Properties.Settings.Default.CB_TS_A = false; 트레일링off("TS_1"); Properties.Settings.Default.CB_ik_down_A = false; CB_ik_down_A.Enabled = true; CB_ik_one_A.Enabled = true; CB_ik_A.Enabled = true; combo_ik_jumun_A.Enabled = true; }
                if (sender.Equals(CB_TS_B)) { CB_ik_one_B.Text = "□"; CB_ik_B.Text = "□"; Properties.Settings.Default.CB_TS_B = false; 트레일링off("TS_2"); Properties.Settings.Default.CB_ik_down_B = false; CB_ik_down_B.Enabled = true; CB_ik_one_B.Enabled = true; CB_ik_B.Enabled = true; combo_ik_jumun_B.Enabled = true; }
                if (sender.Equals(CB_TS_C)) { CB_ik_one_C.Text = "□"; CB_ik_C.Text = "□"; Properties.Settings.Default.CB_TS_C = false; 트레일링off("TS_3"); Properties.Settings.Default.CB_ik_down_C = false; CB_ik_down_C.Enabled = true; CB_ik_one_C.Enabled = true; CB_ik_C.Enabled = true; combo_ik_jumun_C.Enabled = true; }
                if (sender.Equals(CB_TS_D)) { CB_ik_one_D.Text = "□"; CB_ik_D.Text = "□"; Properties.Settings.Default.CB_TS_D = false; 트레일링off("TS_4"); Properties.Settings.Default.CB_ik_down_D = false; CB_ik_down_D.Enabled = true; CB_ik_one_D.Enabled = true; CB_ik_D.Enabled = true; combo_ik_jumun_D.Enabled = true; }
                if (sender.Equals(CB_TS_E)) { CB_ik_one_E.Text = "□"; CB_ik_E.Text = "□"; Properties.Settings.Default.CB_TS_E = false; 트레일링off("TS_5"); Properties.Settings.Default.CB_ik_down_E = false; CB_ik_down_E.Enabled = true; CB_ik_one_E.Enabled = true; CB_ik_E.Enabled = true; combo_ik_jumun_E.Enabled = true; }
                if (sender.Equals(CB_TS_F)) { CB_ik_one_F.Text = "□"; CB_ik_F.Text = "□"; Properties.Settings.Default.CB_TS_F = false; 트레일링off("TS_6"); Properties.Settings.Default.CB_ik_down_F = false; CB_ik_down_F.Enabled = true; CB_ik_one_F.Enabled = true; CB_ik_F.Enabled = true; combo_ik_jumun_F.Enabled = true; }
                if (sender.Equals(CB_TS_G)) { CB_ik_one_G.Text = "□"; CB_ik_G.Text = "□"; Properties.Settings.Default.CB_TS_G = false; 트레일링off("TS_7"); Properties.Settings.Default.CB_ik_down_G = false; CB_ik_down_G.Enabled = true; CB_ik_one_G.Enabled = true; CB_ik_G.Enabled = true; combo_ik_jumun_G.Enabled = true; }
                if (sender.Equals(CB_TS_H)) { CB_ik_one_H.Text = "□"; CB_ik_H.Text = "□"; Properties.Settings.Default.CB_TS_H = false; 트레일링off("TS_8"); Properties.Settings.Default.CB_ik_down_H = false; CB_ik_down_H.Enabled = true; CB_ik_one_H.Enabled = true; CB_ik_H.Enabled = true; combo_ik_jumun_H.Enabled = true; }
                if (sender.Equals(CB_TS_I)) { CB_ik_one_I.Text = "□"; CB_ik_I.Text = "□"; Properties.Settings.Default.CB_TS_I = false; 트레일링off("TS_9"); Properties.Settings.Default.CB_ik_down_I = false; CB_ik_down_I.Enabled = true; CB_ik_one_I.Enabled = true; CB_ik_I.Enabled = true; combo_ik_jumun_I.Enabled = true; }
            }

            void 트레일링off(string location)
            {
                foreach (var 잔고 in Form1.stockBalanceList.ToList())
                {
                    if (location.Equals("TS_1")) 잔고.Value.TS_1 = true;
                    if (location.Equals("TS_2")) 잔고.Value.TS_2 = true;
                    if (location.Equals("TS_3")) 잔고.Value.TS_3 = true;
                    if (location.Equals("TS_4")) 잔고.Value.TS_4 = true;
                    if (location.Equals("TS_5")) 잔고.Value.TS_5 = true;
                    if (location.Equals("TS_6")) 잔고.Value.TS_6 = true;
                    if (location.Equals("TS_7")) 잔고.Value.TS_7 = true;
                    if (location.Equals("TS_8")) 잔고.Value.TS_8 = true;
                    if (location.Equals("TS_9")) 잔고.Value.TS_9 = true;

                    if (!CB_TS_A.Checked && !CB_TS_B.Checked && !CB_TS_C.Checked && !CB_TS_D.Checked && !CB_TS_E.Checked &&
                        !CB_TS_F.Checked && !CB_TS_G.Checked && !CB_TS_H.Checked && !CB_TS_I.Checked) 잔고.Value.트레일링값 = "0@0@0@0";
                }
            }
        }

        private void CB_scalping_Run_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);
            Form1.form1.체크박스_비프(sender);

            string text = CB.Text.Substring(1);

            if (CB.Checked)
            {
                CB.ForeColor = Color.Red;
                CB.Text = "■" + text;
            }
            else
            {
                CB.ForeColor = Color.Black;
                CB.Text = "□" + text;

                if (sender.Equals(CB_scalping_A)) Properties.Settings.Default.CB_scalping_A = false;
                if (sender.Equals(CB_scalping_B)) Properties.Settings.Default.CB_scalping_B = false;
                if (sender.Equals(CB_scalping_C)) Properties.Settings.Default.CB_scalping_C = false;
            }
        }

        private void CB_scalping_CheckedChanged_1(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);
            if (CB_scalping.Checked)
            {
                Form1.알림창("[ 설정알림 ]\n\n체결즉시 매도주문 넣기는\n\n트레일링 스탑과 같이 사용할수 없습니다.", 20, false);

                CheckBox CB = CB_TS_A;

                if (CB_TS_A.Checked) { CB = CB_TS_A; CB.Checked = false; OFF("A off"); }
                if (CB_TS_B.Checked) { CB = CB_TS_B; CB.Checked = false; OFF("B off"); }
                if (CB_TS_C.Checked) { CB = CB_TS_C; CB.Checked = false; OFF("C off"); }
                if (CB_TS_D.Checked) { CB = CB_TS_D; CB.Checked = false; OFF("D off"); }
                if (CB_TS_E.Checked) { CB = CB_TS_E; CB.Checked = false; OFF("E off"); }
                if (CB_TS_F.Checked) { CB = CB_TS_F; CB.Checked = false; OFF("F off"); }
                if (CB_TS_G.Checked) { CB = CB_TS_G; CB.Checked = false; OFF("G off"); }
                if (CB_TS_H.Checked) { CB = CB_TS_H; CB.Checked = false; OFF("H off"); }
                if (CB_TS_I.Checked) { CB = CB_TS_I; CB.Checked = false; OFF("I off"); }
                if (CB_ik_one_A.Checked) { CB = CB_ik_one_A; CB.Checked = false; }
                if (CB_ik_one_B.Checked) { CB = CB_ik_one_B; CB.Checked = false; }
                if (CB_ik_one_C.Checked) { CB = CB_ik_one_C; CB.Checked = false; }
                if (CB_ik_one_D.Checked) { CB = CB_ik_one_D; CB.Checked = false; }
                if (CB_ik_one_E.Checked) { CB = CB_ik_one_E; CB.Checked = false; }
                if (CB_ik_one_F.Checked) { CB = CB_ik_one_F; CB.Checked = false; }
                if (CB_ik_one_G.Checked) { CB = CB_ik_one_G; CB.Checked = false; }
                if (CB_ik_one_H.Checked) { CB = CB_ik_one_H; CB.Checked = false; }
                if (CB_ik_one_I.Checked) { CB = CB_ik_one_I; CB.Checked = false; }

                void OFF(string text)
                {
                    CB.Checked = false;
                    CB.ForeColor = Color.Black;
                    CB.Text = text;
                }

                CB_TS_A.Enabled = false;
                CB_TS_B.Enabled = false;
                CB_TS_C.Enabled = false;
                CB_TS_D.Enabled = false;
                CB_TS_E.Enabled = false;
                CB_TS_F.Enabled = false;
                CB_TS_G.Enabled = false;
                CB_TS_H.Enabled = false;
                CB_TS_I.Enabled = false;

                form.CB_ik_one_A.Checked = false;
                form.CB_ik_one_B.Checked = false;
                form.CB_ik_one_C.Checked = false;
                form.CB_ik_one_D.Checked = false;
                form.CB_ik_one_E.Checked = false;
                form.CB_ik_one_F.Checked = false;
                form.CB_ik_one_G.Checked = false;
                form.CB_ik_one_H.Checked = false;
                form.CB_ik_one_I.Checked = false;

                form.combo_ik_A.SelectedIndex = 0;
                form.combo_ik_B.SelectedIndex = 0;
                form.combo_ik_C.SelectedIndex = 0;
                form.combo_ik_D.SelectedIndex = 0;
                form.combo_ik_E.SelectedIndex = 0;
                form.combo_ik_F.SelectedIndex = 0;
                form.combo_ik_G.SelectedIndex = 0;
                form.combo_ik_H.SelectedIndex = 0;
                form.combo_ik_I.SelectedIndex = 0;

                form.combo_ik_ratio_A.SelectedIndex = 3;
                form.combo_ik_ratio_B.SelectedIndex = 3;
                form.combo_ik_ratio_C.SelectedIndex = 3;
                form.combo_ik_ratio_D.SelectedIndex = 3;
                form.combo_ik_ratio_E.SelectedIndex = 3;
                form.combo_ik_ratio_F.SelectedIndex = 3;
                form.combo_ik_ratio_G.SelectedIndex = 3;
                form.combo_ik_ratio_H.SelectedIndex = 3;
                form.combo_ik_ratio_I.SelectedIndex = 3;

                form.CB_ik_one_A.Enabled = false;
                form.CB_ik_one_B.Enabled = false;
                form.CB_ik_one_C.Enabled = false;
                form.CB_ik_one_D.Enabled = false;
                form.CB_ik_one_E.Enabled = false;
                form.CB_ik_one_F.Enabled = false;
                form.CB_ik_one_G.Enabled = false;
                form.CB_ik_one_H.Enabled = false;
                form.CB_ik_one_I.Enabled = false;

                form.combo_ik_A.Enabled = false;
                form.combo_ik_B.Enabled = false;
                form.combo_ik_C.Enabled = false;
                form.combo_ik_D.Enabled = false;
                form.combo_ik_E.Enabled = false;
                form.combo_ik_F.Enabled = false;
                form.combo_ik_G.Enabled = false;
                form.combo_ik_H.Enabled = false;
                form.combo_ik_I.Enabled = false;

                form.combo_ik_ratio_A.Enabled = false;
                form.combo_ik_ratio_B.Enabled = false;
                form.combo_ik_ratio_C.Enabled = false;
                form.combo_ik_ratio_D.Enabled = false;
                form.combo_ik_ratio_E.Enabled = false;
                form.combo_ik_ratio_F.Enabled = false;
                form.combo_ik_ratio_G.Enabled = false;
                form.combo_ik_ratio_H.Enabled = false;
                form.combo_ik_ratio_I.Enabled = false;
            }
            else
            {
                CB_TS_A.Enabled = true;
                CB_TS_B.Enabled = true;
                CB_TS_C.Enabled = true;
                CB_TS_D.Enabled = true;
                CB_TS_E.Enabled = true;
                CB_TS_F.Enabled = true;
                CB_TS_G.Enabled = true;
                CB_TS_H.Enabled = true;
                CB_TS_I.Enabled = true;

                CB_ik_one_A.Enabled = true;
                CB_ik_one_B.Enabled = true;
                CB_ik_one_C.Enabled = true;
                CB_ik_one_D.Enabled = true;
                CB_ik_one_E.Enabled = true;
                CB_ik_one_F.Enabled = true;
                CB_ik_one_G.Enabled = true;
                CB_ik_one_H.Enabled = true;
                CB_ik_one_I.Enabled = true;

                combo_ik_A.Enabled = true;
                combo_ik_B.Enabled = true;
                combo_ik_C.Enabled = true;
                combo_ik_D.Enabled = true;
                combo_ik_E.Enabled = true;
                combo_ik_F.Enabled = true;
                combo_ik_G.Enabled = true;
                combo_ik_H.Enabled = true;
                combo_ik_I.Enabled = true;

                combo_ik_ratio_A.Enabled = true;
                combo_ik_ratio_B.Enabled = true;
                combo_ik_ratio_C.Enabled = true;
                combo_ik_ratio_D.Enabled = true;
                combo_ik_ratio_E.Enabled = true;
                combo_ik_ratio_F.Enabled = true;
                combo_ik_ratio_G.Enabled = true;
                combo_ik_ratio_H.Enabled = true;
                combo_ik_ratio_I.Enabled = true;
            }
        }

        private void CB_scalping_CheckedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);
            CheckBox CB = (sender as CheckBox);
            if (CB.Checked)
            {
                CB.ForeColor = Color.Red;
            }
            else
            {
                CB.ForeColor = Color.Black;
            }


            if (Form1.FormBasic_Open)
            {
                if (sender.Equals(Form_Basic.form.CB_scalping_A_9))
                {
                    if (Form_Basic.form.CB_scalping_A_9.Checked)
                    {
                        Form_Basic.form.CB_scalping_A_1.Checked = true;
                        Form_Basic.form.CB_scalping_A_2.Checked = true;
                        Form_Basic.form.CB_scalping_A_3.Checked = true;
                        Form_Basic.form.CB_scalping_A_4.Checked = true;
                        Form_Basic.form.CB_scalping_A_5.Checked = true;
                        Form_Basic.form.CB_scalping_A_6.Checked = true;
                        Form_Basic.form.CB_scalping_A_7.Checked = true;
                        Form_Basic.form.CB_scalping_A_8.Checked = true;
                        Check_I();
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_A_8))
                {
                    if (Form_Basic.form.CB_scalping_A_8.Checked)
                    {
                        Form_Basic.form.CB_scalping_A_1.Checked = true;
                        Form_Basic.form.CB_scalping_A_2.Checked = true;
                        Form_Basic.form.CB_scalping_A_3.Checked = true;
                        Form_Basic.form.CB_scalping_A_4.Checked = true;
                        Form_Basic.form.CB_scalping_A_5.Checked = true;
                        Form_Basic.form.CB_scalping_A_6.Checked = true;
                        Form_Basic.form.CB_scalping_A_7.Checked = true;
                        Check_H();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_A_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_A_7))
                {
                    if (Form_Basic.form.CB_scalping_A_7.Checked)
                    {
                        Form_Basic.form.CB_scalping_A_1.Checked = true;
                        Form_Basic.form.CB_scalping_A_2.Checked = true;
                        Form_Basic.form.CB_scalping_A_3.Checked = true;
                        Form_Basic.form.CB_scalping_A_4.Checked = true;
                        Form_Basic.form.CB_scalping_A_5.Checked = true;
                        Form_Basic.form.CB_scalping_A_6.Checked = true;
                        Check_G();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_A_8.Checked = false;
                        Form_Basic.form.CB_scalping_A_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_A_6))
                {
                    if (Form_Basic.form.CB_scalping_A_6.Checked)
                    {
                        Form_Basic.form.CB_scalping_A_1.Checked = true;
                        Form_Basic.form.CB_scalping_A_2.Checked = true;
                        Form_Basic.form.CB_scalping_A_3.Checked = true;
                        Form_Basic.form.CB_scalping_A_4.Checked = true;
                        Form_Basic.form.CB_scalping_A_5.Checked = true;
                        Check_F();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_A_7.Checked = false;
                        Form_Basic.form.CB_scalping_A_8.Checked = false;
                        Form_Basic.form.CB_scalping_A_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_A_5))
                {
                    if (Form_Basic.form.CB_scalping_A_5.Checked)
                    {
                        Form_Basic.form.CB_scalping_A_1.Checked = true;
                        Form_Basic.form.CB_scalping_A_2.Checked = true;
                        Form_Basic.form.CB_scalping_A_3.Checked = true;
                        Form_Basic.form.CB_scalping_A_4.Checked = true;
                        Check_E();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_A_6.Checked = false;
                        Form_Basic.form.CB_scalping_A_7.Checked = false;
                        Form_Basic.form.CB_scalping_A_8.Checked = false;
                        Form_Basic.form.CB_scalping_A_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_A_4))
                {
                    if (Form_Basic.form.CB_scalping_A_4.Checked)
                    {
                        Form_Basic.form.CB_scalping_A_1.Checked = true;
                        Form_Basic.form.CB_scalping_A_2.Checked = true;
                        Form_Basic.form.CB_scalping_A_3.Checked = true;
                        Check_D();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_A_5.Checked = false;
                        Form_Basic.form.CB_scalping_A_6.Checked = false;
                        Form_Basic.form.CB_scalping_A_7.Checked = false;
                        Form_Basic.form.CB_scalping_A_8.Checked = false;
                        Form_Basic.form.CB_scalping_A_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_A_3))
                {
                    if (Form_Basic.form.CB_scalping_A_3.Checked)
                    {
                        Form_Basic.form.CB_scalping_A_1.Checked = true;
                        Form_Basic.form.CB_scalping_A_2.Checked = true;
                        Check_C();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_A_4.Checked = false;
                        Form_Basic.form.CB_scalping_A_5.Checked = false;
                        Form_Basic.form.CB_scalping_A_6.Checked = false;
                        Form_Basic.form.CB_scalping_A_7.Checked = false;
                        Form_Basic.form.CB_scalping_A_8.Checked = false;
                        Form_Basic.form.CB_scalping_A_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_A_2))
                {
                    if (Form_Basic.form.CB_scalping_A_2.Checked)
                    {
                        Form_Basic.form.CB_scalping_A_1.Checked = true;
                        Check_B();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_A_3.Checked = false;
                        Form_Basic.form.CB_scalping_A_4.Checked = false;
                        Form_Basic.form.CB_scalping_A_5.Checked = false;
                        Form_Basic.form.CB_scalping_A_6.Checked = false;
                        Form_Basic.form.CB_scalping_A_7.Checked = false;
                        Form_Basic.form.CB_scalping_A_8.Checked = false;
                        Form_Basic.form.CB_scalping_A_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_A_1))
                {
                    if (Form_Basic.form.CB_scalping_A_1.Checked)
                    {
                        Check_A();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_A_2.Checked = false;
                        Form_Basic.form.CB_scalping_A_3.Checked = false;
                        Form_Basic.form.CB_scalping_A_4.Checked = false;
                        Form_Basic.form.CB_scalping_A_5.Checked = false;
                        Form_Basic.form.CB_scalping_A_6.Checked = false;
                        Form_Basic.form.CB_scalping_A_7.Checked = false;
                        Form_Basic.form.CB_scalping_A_8.Checked = false;
                        Form_Basic.form.CB_scalping_A_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_B_9))
                {
                    if (Form_Basic.form.CB_scalping_B_9.Checked)
                    {
                        Form_Basic.form.CB_scalping_B_1.Checked = true;
                        Form_Basic.form.CB_scalping_B_2.Checked = true;
                        Form_Basic.form.CB_scalping_B_3.Checked = true;
                        Form_Basic.form.CB_scalping_B_4.Checked = true;
                        Form_Basic.form.CB_scalping_B_5.Checked = true;
                        Form_Basic.form.CB_scalping_B_6.Checked = true;
                        Form_Basic.form.CB_scalping_B_7.Checked = true;
                        Form_Basic.form.CB_scalping_B_8.Checked = true;
                        Check_I();
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_B_8))
                {
                    if (Form_Basic.form.CB_scalping_B_8.Checked)
                    {
                        Form_Basic.form.CB_scalping_B_1.Checked = true;
                        Form_Basic.form.CB_scalping_B_2.Checked = true;
                        Form_Basic.form.CB_scalping_B_3.Checked = true;
                        Form_Basic.form.CB_scalping_B_4.Checked = true;
                        Form_Basic.form.CB_scalping_B_5.Checked = true;
                        Form_Basic.form.CB_scalping_B_6.Checked = true;
                        Form_Basic.form.CB_scalping_B_7.Checked = true;
                        Check_H();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_B_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_B_7))
                {
                    if (Form_Basic.form.CB_scalping_B_7.Checked)
                    {
                        Form_Basic.form.CB_scalping_B_1.Checked = true;
                        Form_Basic.form.CB_scalping_B_2.Checked = true;
                        Form_Basic.form.CB_scalping_B_3.Checked = true;
                        Form_Basic.form.CB_scalping_B_4.Checked = true;
                        Form_Basic.form.CB_scalping_B_5.Checked = true;
                        Form_Basic.form.CB_scalping_B_6.Checked = true;
                        Check_G();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_B_8.Checked = false;
                        Form_Basic.form.CB_scalping_B_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_B_6))
                {
                    if (Form_Basic.form.CB_scalping_B_6.Checked)
                    {
                        Form_Basic.form.CB_scalping_B_1.Checked = true;
                        Form_Basic.form.CB_scalping_B_2.Checked = true;
                        Form_Basic.form.CB_scalping_B_3.Checked = true;
                        Form_Basic.form.CB_scalping_B_4.Checked = true;
                        Form_Basic.form.CB_scalping_B_5.Checked = true;
                        Check_F();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_B_7.Checked = false;
                        Form_Basic.form.CB_scalping_B_8.Checked = false;
                        Form_Basic.form.CB_scalping_B_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_B_5))
                {
                    if (Form_Basic.form.CB_scalping_B_5.Checked)
                    {
                        Form_Basic.form.CB_scalping_B_1.Checked = true;
                        Form_Basic.form.CB_scalping_B_2.Checked = true;
                        Form_Basic.form.CB_scalping_B_3.Checked = true;
                        Form_Basic.form.CB_scalping_B_4.Checked = true;
                        Check_E();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_B_6.Checked = false;
                        Form_Basic.form.CB_scalping_B_7.Checked = false;
                        Form_Basic.form.CB_scalping_B_8.Checked = false;
                        Form_Basic.form.CB_scalping_B_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_B_4))
                {
                    if (Form_Basic.form.CB_scalping_B_4.Checked)
                    {
                        Form_Basic.form.CB_scalping_B_1.Checked = true;
                        Form_Basic.form.CB_scalping_B_2.Checked = true;
                        Form_Basic.form.CB_scalping_B_3.Checked = true;
                        Check_D();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_B_5.Checked = false;
                        Form_Basic.form.CB_scalping_B_6.Checked = false;
                        Form_Basic.form.CB_scalping_B_7.Checked = false;
                        Form_Basic.form.CB_scalping_B_8.Checked = false;
                        Form_Basic.form.CB_scalping_B_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_B_3))
                {
                    if (Form_Basic.form.CB_scalping_B_3.Checked)
                    {
                        Form_Basic.form.CB_scalping_B_1.Checked = true;
                        Form_Basic.form.CB_scalping_B_2.Checked = true;
                        Check_C();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_B_4.Checked = false;
                        Form_Basic.form.CB_scalping_B_5.Checked = false;
                        Form_Basic.form.CB_scalping_B_6.Checked = false;
                        Form_Basic.form.CB_scalping_B_7.Checked = false;
                        Form_Basic.form.CB_scalping_B_8.Checked = false;
                        Form_Basic.form.CB_scalping_B_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_B_2))
                {
                    if (Form_Basic.form.CB_scalping_B_2.Checked)
                    {
                        Form_Basic.form.CB_scalping_B_1.Checked = true;
                        Check_B();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_B_3.Checked = false;
                        Form_Basic.form.CB_scalping_B_4.Checked = false;
                        Form_Basic.form.CB_scalping_B_5.Checked = false;
                        Form_Basic.form.CB_scalping_B_6.Checked = false;
                        Form_Basic.form.CB_scalping_B_7.Checked = false;
                        Form_Basic.form.CB_scalping_B_8.Checked = false;
                        Form_Basic.form.CB_scalping_B_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_B_1))
                {
                    if (Form_Basic.form.CB_scalping_B_1.Checked)
                    {
                        Check_A();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_B_2.Checked = false;
                        Form_Basic.form.CB_scalping_B_3.Checked = false;
                        Form_Basic.form.CB_scalping_B_4.Checked = false;
                        Form_Basic.form.CB_scalping_B_5.Checked = false;
                        Form_Basic.form.CB_scalping_B_6.Checked = false;
                        Form_Basic.form.CB_scalping_B_7.Checked = false;
                        Form_Basic.form.CB_scalping_B_8.Checked = false;
                        Form_Basic.form.CB_scalping_B_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_C_9))
                {
                    if (Form_Basic.form.CB_scalping_C_9.Checked)
                    {
                        Form_Basic.form.CB_scalping_C_1.Checked = true;
                        Form_Basic.form.CB_scalping_C_2.Checked = true;
                        Form_Basic.form.CB_scalping_C_3.Checked = true;
                        Form_Basic.form.CB_scalping_C_4.Checked = true;
                        Form_Basic.form.CB_scalping_C_5.Checked = true;
                        Form_Basic.form.CB_scalping_C_6.Checked = true;
                        Form_Basic.form.CB_scalping_C_7.Checked = true;
                        Form_Basic.form.CB_scalping_C_8.Checked = true;
                        Check_I();
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_C_8))
                {
                    if (Form_Basic.form.CB_scalping_C_8.Checked)
                    {
                        Form_Basic.form.CB_scalping_C_1.Checked = true;
                        Form_Basic.form.CB_scalping_C_2.Checked = true;
                        Form_Basic.form.CB_scalping_C_3.Checked = true;
                        Form_Basic.form.CB_scalping_C_4.Checked = true;
                        Form_Basic.form.CB_scalping_C_5.Checked = true;
                        Form_Basic.form.CB_scalping_C_6.Checked = true;
                        Form_Basic.form.CB_scalping_C_7.Checked = true;
                        Check_H();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_C_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_C_7))
                {
                    if (Form_Basic.form.CB_scalping_C_7.Checked)
                    {
                        Form_Basic.form.CB_scalping_C_1.Checked = true;
                        Form_Basic.form.CB_scalping_C_2.Checked = true;
                        Form_Basic.form.CB_scalping_C_3.Checked = true;
                        Form_Basic.form.CB_scalping_C_4.Checked = true;
                        Form_Basic.form.CB_scalping_C_5.Checked = true;
                        Form_Basic.form.CB_scalping_C_6.Checked = true;
                        Check_G();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_C_8.Checked = false;
                        Form_Basic.form.CB_scalping_C_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_C_6))
                {
                    if (Form_Basic.form.CB_scalping_C_6.Checked)
                    {
                        Form_Basic.form.CB_scalping_C_1.Checked = true;
                        Form_Basic.form.CB_scalping_C_2.Checked = true;
                        Form_Basic.form.CB_scalping_C_3.Checked = true;
                        Form_Basic.form.CB_scalping_C_4.Checked = true;
                        Form_Basic.form.CB_scalping_C_5.Checked = true;
                        Check_F();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_C_7.Checked = false;
                        Form_Basic.form.CB_scalping_C_8.Checked = false;
                        Form_Basic.form.CB_scalping_C_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_C_5))
                {
                    if (Form_Basic.form.CB_scalping_C_5.Checked)
                    {
                        Form_Basic.form.CB_scalping_C_1.Checked = true;
                        Form_Basic.form.CB_scalping_C_2.Checked = true;
                        Form_Basic.form.CB_scalping_C_3.Checked = true;
                        Form_Basic.form.CB_scalping_C_4.Checked = true;
                        Check_E();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_C_6.Checked = false;
                        Form_Basic.form.CB_scalping_C_7.Checked = false;
                        Form_Basic.form.CB_scalping_C_8.Checked = false;
                        Form_Basic.form.CB_scalping_C_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_C_4))
                {
                    if (Form_Basic.form.CB_scalping_C_4.Checked)
                    {
                        Form_Basic.form.CB_scalping_C_1.Checked = true;
                        Form_Basic.form.CB_scalping_C_2.Checked = true;
                        Form_Basic.form.CB_scalping_C_3.Checked = true;
                        Check_D();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_C_5.Checked = false;
                        Form_Basic.form.CB_scalping_C_6.Checked = false;
                        Form_Basic.form.CB_scalping_C_7.Checked = false;
                        Form_Basic.form.CB_scalping_C_8.Checked = false;
                        Form_Basic.form.CB_scalping_C_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_C_3))
                {
                    if (Form_Basic.form.CB_scalping_C_3.Checked)
                    {
                        Form_Basic.form.CB_scalping_C_1.Checked = true;
                        Form_Basic.form.CB_scalping_C_2.Checked = true;
                        Check_C();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_C_4.Checked = false;
                        Form_Basic.form.CB_scalping_C_5.Checked = false;
                        Form_Basic.form.CB_scalping_C_6.Checked = false;
                        Form_Basic.form.CB_scalping_C_7.Checked = false;
                        Form_Basic.form.CB_scalping_C_8.Checked = false;
                        Form_Basic.form.CB_scalping_C_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_C_2))
                {
                    if (Form_Basic.form.CB_scalping_C_2.Checked)
                    {
                        Form_Basic.form.CB_scalping_C_1.Checked = true;
                        Check_B();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_C_3.Checked = false;
                        Form_Basic.form.CB_scalping_C_4.Checked = false;
                        Form_Basic.form.CB_scalping_C_5.Checked = false;
                        Form_Basic.form.CB_scalping_C_6.Checked = false;
                        Form_Basic.form.CB_scalping_C_7.Checked = false;
                        Form_Basic.form.CB_scalping_C_8.Checked = false;
                        Form_Basic.form.CB_scalping_C_9.Checked = false;
                    }
                }

                if (sender.Equals(Form_Basic.form.CB_scalping_C_1))
                {
                    if (Form_Basic.form.CB_scalping_C_1.Checked)
                    {
                        Check_A();
                    }
                    else
                    {
                        Form_Basic.form.CB_scalping_C_2.Checked = false;
                        Form_Basic.form.CB_scalping_C_3.Checked = false;
                        Form_Basic.form.CB_scalping_C_4.Checked = false;
                        Form_Basic.form.CB_scalping_C_5.Checked = false;
                        Form_Basic.form.CB_scalping_C_6.Checked = false;
                        Form_Basic.form.CB_scalping_C_7.Checked = false;
                        Form_Basic.form.CB_scalping_C_8.Checked = false;
                        Form_Basic.form.CB_scalping_C_9.Checked = false;
                    }
                }

                void Check_A()
                {
                    if (CB_scalping.Checked)
                        if (!CB_ik_A.Checked) CB_ik_A.Checked = true;
                }
                void Check_B()
                {
                    if (CB_scalping.Checked)
                    {
                        if (!CB_ik_A.Checked) CB_ik_A.Checked = true;
                        if (!CB_ik_B.Checked) CB_ik_B.Checked = true;
                    }
                }
                void Check_C()
                {
                    if (CB_scalping.Checked)
                    {
                        if (!CB_ik_A.Checked) CB_ik_A.Checked = true;
                        if (!CB_ik_B.Checked) CB_ik_B.Checked = true;
                        if (!CB_ik_C.Checked) CB_ik_C.Checked = true;
                    }
                }
                void Check_D()
                {
                    if (CB_scalping.Checked)
                    {
                        if (!CB_ik_A.Checked) CB_ik_A.Checked = true;
                        if (!CB_ik_B.Checked) CB_ik_B.Checked = true;
                        if (!CB_ik_C.Checked) CB_ik_C.Checked = true;
                        if (!CB_ik_D.Checked) CB_ik_D.Checked = true;
                    }
                }
                void Check_E()
                {
                    if (CB_scalping.Checked)
                    {
                        if (!CB_ik_A.Checked) CB_ik_A.Checked = true;
                        if (!CB_ik_B.Checked) CB_ik_B.Checked = true;
                        if (!CB_ik_C.Checked) CB_ik_C.Checked = true;
                        if (!CB_ik_D.Checked) CB_ik_D.Checked = true;
                        if (!CB_ik_E.Checked) CB_ik_E.Checked = true;
                    }
                }
                void Check_F()
                {
                    if (CB_scalping.Checked)
                    {
                        if (!CB_ik_A.Checked) CB_ik_A.Checked = true;
                        if (!CB_ik_B.Checked) CB_ik_B.Checked = true;
                        if (!CB_ik_C.Checked) CB_ik_C.Checked = true;
                        if (!CB_ik_D.Checked) CB_ik_D.Checked = true;
                        if (!CB_ik_E.Checked) CB_ik_E.Checked = true;
                        if (!CB_ik_F.Checked) CB_ik_F.Checked = true;
                    }
                }
                void Check_G()
                {
                    if (CB_scalping.Checked)
                    {
                        if (!CB_ik_A.Checked) CB_ik_A.Checked = true;
                        if (!CB_ik_B.Checked) CB_ik_B.Checked = true;
                        if (!CB_ik_C.Checked) CB_ik_C.Checked = true;
                        if (!CB_ik_D.Checked) CB_ik_D.Checked = true;
                        if (!CB_ik_E.Checked) CB_ik_E.Checked = true;
                        if (!CB_ik_F.Checked) CB_ik_F.Checked = true;
                        if (!CB_ik_G.Checked) CB_ik_G.Checked = true;
                    }
                }
                void Check_H()
                {
                    if (CB_scalping.Checked)
                    {
                        if (!CB_ik_A.Checked) CB_ik_A.Checked = true;
                        if (!CB_ik_B.Checked) CB_ik_B.Checked = true;
                        if (!CB_ik_C.Checked) CB_ik_C.Checked = true;
                        if (!CB_ik_D.Checked) CB_ik_D.Checked = true;
                        if (!CB_ik_E.Checked) CB_ik_E.Checked = true;
                        if (!CB_ik_F.Checked) CB_ik_F.Checked = true;
                        if (!CB_ik_G.Checked) CB_ik_G.Checked = true;
                        if (!CB_ik_H.Checked) CB_ik_H.Checked = true;
                    }
                }
                void Check_I()
                {
                    if (CB_scalping.Checked)
                    {
                        if (!CB_ik_A.Checked) CB_ik_A.Checked = true;
                        if (!CB_ik_B.Checked) CB_ik_B.Checked = true;
                        if (!CB_ik_C.Checked) CB_ik_C.Checked = true;
                        if (!CB_ik_D.Checked) CB_ik_D.Checked = true;
                        if (!CB_ik_E.Checked) CB_ik_E.Checked = true;
                        if (!CB_ik_F.Checked) CB_ik_F.Checked = true;
                        if (!CB_ik_G.Checked) CB_ik_G.Checked = true;
                        if (!CB_ik_H.Checked) CB_ik_H.Checked = true;
                        if (!CB_ik_I.Checked) CB_ik_I.Checked = true;
                    }
                }
            }
        }

        private void combo_cancel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrint.combo_cancel_SelectedIndexChanged(sender);
        }
    }
}
