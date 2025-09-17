using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace 지니_64
{
    public partial class Form_Special : Form
    {
        public static Form_Special form;
        public Form_Special()
        {
            form = this;
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void Form_Special_Load()
        {
            typeof(ListBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, LB_예약리스트, new object[] { true });

            Form1.음소거 = true;
            RB_매수.Checked = Properties.Settings.Default.수동주문_RB_매수;
            RB_매도.Checked = !Properties.Settings.Default.수동주문_RB_매수;

            CB_매매기간_TS.Checked = false;
            panel_매매기간_TS.Hide();

            combo_수동주문_choice.SelectedIndex = Properties.Settings.Default.combo_수동주문_choice;

            MTB_수동주문_cansel_time.Text = Properties.Settings.Default.MTB_수동주문_cansel_time.ToString();
            MTB_수동주문_repeat.Text = Properties.Settings.Default.MTB_수동주문_repeat.ToString();
            TB_수동주문_ratio.Text = Properties.Settings.Default.TB_수동주문_ratio.ToString();

            CBB_group_In_jumun_A.SelectedIndex = Properties.Settings.Default.CBB_group_In_jumun_A;
            MTB_group_In_CanselTime_A.Text = Properties.Settings.Default.MTB_group_In_CanselTime_A.ToString();
            MTB_group_In_repeat_A.Text = Properties.Settings.Default.MTB_group_In_repeat_A.ToString();
            TB_group_In_ratio_A.Text = Properties.Settings.Default.TB_group_In_ratio_A.ToString();
            CBB_group_In_choice_A.SelectedIndex = Properties.Settings.Default.CBB_group_In_choice_A;
            CBB_group_out_jumun_A.SelectedIndex = Properties.Settings.Default.CBB_group_out_jumun_A;
            MTB_group_out_CanselTime_A.Text = Properties.Settings.Default.MTB_group_out_CanselTime_A.ToString();
            MTB_group_out_repeat_A.Text = Properties.Settings.Default.MTB_group_out_repeat_A.ToString();
            TB_group_out_ratio_A.Text = Properties.Settings.Default.TB_group_out_ratio_A.ToString();
            CBB_group_out_choice_A.SelectedIndex = Properties.Settings.Default.CBB_group_out_choice_A;
            TB_group_Out_value_A.Text = Properties.Settings.Default.TB_group_Out_value_A.ToString();

            CBB_group_In_jumun_B.SelectedIndex = Properties.Settings.Default.CBB_group_In_jumun_B;
            MTB_group_In_CanselTime_B.Text = Properties.Settings.Default.MTB_group_In_CanselTime_B.ToString();
            MTB_group_In_repeat_B.Text = Properties.Settings.Default.MTB_group_In_repeat_B.ToString();
            TB_group_In_ratio_B.Text = Properties.Settings.Default.TB_group_In_ratio_B.ToString();
            CBB_group_In_choice_B.SelectedIndex = Properties.Settings.Default.CBB_group_In_choice_B;
            CBB_group_out_jumun_B.SelectedIndex = Properties.Settings.Default.CBB_group_out_jumun_B;
            MTB_group_out_CanselTime_B.Text = Properties.Settings.Default.MTB_group_out_CanselTime_B.ToString();
            MTB_group_out_repeat_B.Text = Properties.Settings.Default.MTB_group_out_repeat_B.ToString();
            TB_group_out_ratio_B.Text = Properties.Settings.Default.TB_group_out_ratio_B.ToString();
            CBB_group_out_choice_B.SelectedIndex = Properties.Settings.Default.CBB_group_out_choice_B;
            TB_group_Out_value_B.Text = Properties.Settings.Default.TB_group_Out_value_B.ToString();

            CBB_group_In_jumun_C.SelectedIndex = Properties.Settings.Default.CBB_group_In_jumun_C;
            MTB_group_In_CanselTime_C.Text = Properties.Settings.Default.MTB_group_In_CanselTime_C.ToString();
            MTB_group_In_repeat_C.Text = Properties.Settings.Default.MTB_group_In_repeat_C.ToString();
            TB_group_In_ratio_C.Text = Properties.Settings.Default.TB_group_In_ratio_C.ToString();
            CBB_group_In_choice_C.SelectedIndex = Properties.Settings.Default.CBB_group_In_choice_C;
            CBB_group_out_jumun_C.SelectedIndex = Properties.Settings.Default.CBB_group_out_jumun_C;
            MTB_group_out_CanselTime_C.Text = Properties.Settings.Default.MTB_group_out_CanselTime_C.ToString();
            MTB_group_out_repeat_C.Text = Properties.Settings.Default.MTB_group_out_repeat_C.ToString();
            TB_group_out_ratio_C.Text = Properties.Settings.Default.TB_group_out_ratio_C.ToString();
            CBB_group_out_choice_C.SelectedIndex = Properties.Settings.Default.CBB_group_out_choice_C;
            TB_group_Out_value_C.Text = Properties.Settings.Default.TB_group_Out_value_C.ToString();

            CBB_group_In_jumun_D.SelectedIndex = Properties.Settings.Default.CBB_group_In_jumun_D;
            MTB_group_In_CanselTime_D.Text = Properties.Settings.Default.MTB_group_In_CanselTime_D.ToString();
            MTB_group_In_repeat_D.Text = Properties.Settings.Default.MTB_group_In_repeat_D.ToString();
            TB_group_In_ratio_D.Text = Properties.Settings.Default.TB_group_In_ratio_D.ToString();
            CBB_group_In_choice_D.SelectedIndex = Properties.Settings.Default.CBB_group_In_choice_D;
            CBB_group_out_jumun_D.SelectedIndex = Properties.Settings.Default.CBB_group_out_jumun_D;
            MTB_group_out_CanselTime_D.Text = Properties.Settings.Default.MTB_group_out_CanselTime_D.ToString();
            MTB_group_out_repeat_D.Text = Properties.Settings.Default.MTB_group_out_repeat_D.ToString();
            TB_group_out_ratio_D.Text = Properties.Settings.Default.TB_group_out_ratio_D.ToString();
            CBB_group_out_choice_D.SelectedIndex = Properties.Settings.Default.CBB_group_out_choice_D;
            TB_group_Out_value_D.Text = Properties.Settings.Default.TB_group_Out_value_D.ToString();
            TB_group_out_ratio_D.Text = Properties.Settings.Default.TB_group_out_ratio_D.ToString();

            CBB_In_group_A.SelectedIndex = Properties.Settings.Default.CBB_In_group_A;
            CBB_In_group_B.SelectedIndex = Properties.Settings.Default.CBB_In_group_B;
            CBB_In_group_C.SelectedIndex = Properties.Settings.Default.CBB_In_group_C;
            CBB_In_group_D.SelectedIndex = Properties.Settings.Default.CBB_In_group_D;

            combo_신규그룹_A.SelectedIndex = Properties.Settings.Default.combo_신규그룹_A;
            combo_신규그룹_B.SelectedIndex = Properties.Settings.Default.combo_신규그룹_B;
            combo_신규그룹_C.SelectedIndex = Properties.Settings.Default.combo_신규그룹_C;

            CB_group_기준금.Checked = Properties.Settings.Default.CB_group_기준금;

            CBB_I_group_choice_A.SelectedIndex = Properties.Settings.Default.CBB_In_group_choice_A;
            CBB_I_group_choice_B.SelectedIndex = Properties.Settings.Default.CBB_In_group_choice_B;
            CBB_I_group_choice_C.SelectedIndex = Properties.Settings.Default.CBB_In_group_choice_C;
            CBB_I_group_choice_D.SelectedIndex = Properties.Settings.Default.CBB_In_group_choice_D;

            CBB_O_group_choice_A.SelectedIndex = Properties.Settings.Default.CBB_Out_group_choice_A;
            CBB_O_group_choice_B.SelectedIndex = Properties.Settings.Default.CBB_Out_group_choice_B;
            CBB_O_group_choice_C.SelectedIndex = Properties.Settings.Default.CBB_Out_group_choice_C;
            CBB_O_group_choice_D.SelectedIndex = Properties.Settings.Default.CBB_Out_group_choice_D;

            CBB_Out_group_A.SelectedIndex = Properties.Settings.Default.CBB_Out_group_A;
            CBB_Out_group_B.SelectedIndex = Properties.Settings.Default.CBB_Out_group_B;
            CBB_Out_group_C.SelectedIndex = Properties.Settings.Default.CBB_Out_group_C;
            CBB_Out_group_D.SelectedIndex = Properties.Settings.Default.CBB_Out_group_D;

            TB_In_group_ratio_A.Text = Properties.Settings.Default.TB_In_group_ratio_A.ToString();
            TB_In_group_ratio_B.Text = Properties.Settings.Default.TB_In_group_ratio_B.ToString();
            TB_In_group_ratio_C.Text = Properties.Settings.Default.TB_In_group_ratio_C.ToString();
            TB_In_group_ratio_D.Text = Properties.Settings.Default.TB_In_group_ratio_D.ToString();

            TB_Out_group_ratio_A.Text = Properties.Settings.Default.TB_Out_group_ratio_A.ToString();
            TB_Out_group_ratio_B.Text = Properties.Settings.Default.TB_Out_group_ratio_B.ToString();
            TB_Out_group_ratio_C.Text = Properties.Settings.Default.TB_Out_group_ratio_C.ToString();
            TB_Out_group_ratio_D.Text = Properties.Settings.Default.TB_Out_group_ratio_D.ToString();

            CB_Group_In_work_A.Checked = Properties.Settings.Default.CB_Group_In_work_A;
            CB_Group_In_work_B.Checked = Properties.Settings.Default.CB_Group_In_work_B;
            CB_Group_In_work_C.Checked = Properties.Settings.Default.CB_Group_In_work_C;
            CB_Group_In_work_D.Checked = Properties.Settings.Default.CB_Group_In_work_D;

            CB_Group_Out_work_A.Checked = Properties.Settings.Default.CB_Group_Out_work_A;
            CB_Group_Out_work_B.Checked = Properties.Settings.Default.CB_Group_Out_work_B;
            CB_Group_Out_work_C.Checked = Properties.Settings.Default.CB_Group_Out_work_C;
            CB_Group_Out_work_D.Checked = Properties.Settings.Default.CB_Group_Out_work_D;

            CB_Group_In_trading_A.Checked = Properties.Settings.Default.CB_Group_In_trading_A;
            CB_Group_In_trading_B.Checked = Properties.Settings.Default.CB_Group_In_trading_B;
            CB_Group_In_trading_C.Checked = Properties.Settings.Default.CB_Group_In_trading_C;
            CB_Group_In_trading_D.Checked = Properties.Settings.Default.CB_Group_In_trading_D;

            CB_Group_Out_trading_A.Checked = Properties.Settings.Default.CB_Group_Out_trading_A;
            CB_Group_Out_trading_B.Checked = Properties.Settings.Default.CB_Group_Out_trading_B;
            CB_Group_Out_trading_C.Checked = Properties.Settings.Default.CB_Group_Out_trading_C;
            CB_Group_Out_trading_D.Checked = Properties.Settings.Default.CB_Group_Out_trading_D;

            CB_group_In_updown_A.Checked = Properties.Settings.Default.CB_group_In_updown_A;
            CB_group_In_updown_B.Checked = Properties.Settings.Default.CB_group_In_updown_B;
            CB_group_In_updown_C.Checked = Properties.Settings.Default.CB_group_In_updown_C;
            CB_group_In_updown_D.Checked = Properties.Settings.Default.CB_group_In_updown_D;

            CB_group_Out_updown_A.Checked = Properties.Settings.Default.CB_group_Out_updown_A;
            CB_group_Out_updown_B.Checked = Properties.Settings.Default.CB_group_Out_updown_B;
            CB_group_Out_updown_C.Checked = Properties.Settings.Default.CB_group_Out_updown_C;
            CB_group_Out_updown_D.Checked = Properties.Settings.Default.CB_group_Out_updown_D;

            CBB_group_In_won_A.SelectedIndex = Properties.Settings.Default.CBB_group_In_won_A;
            CBB_group_In_won_B.SelectedIndex = Properties.Settings.Default.CBB_group_In_won_B;
            CBB_group_In_won_C.SelectedIndex = Properties.Settings.Default.CBB_group_In_won_C;
            CBB_group_In_won_D.SelectedIndex = Properties.Settings.Default.CBB_group_In_won_D;

            TB_group_In_won_A.Text = Properties.Settings.Default.TB_group_In_won_A.ToString();
            TB_group_In_won_B.Text = Properties.Settings.Default.TB_group_In_won_B.ToString();
            TB_group_In_won_C.Text = Properties.Settings.Default.TB_group_In_won_C.ToString();
            TB_group_In_won_D.Text = Properties.Settings.Default.TB_group_In_won_D.ToString();

            TB_group_In_value_A.Text = Properties.Settings.Default.TB_group_In_value_A.ToString();
            TB_group_In_value_B.Text = Properties.Settings.Default.TB_group_In_value_B.ToString();
            TB_group_In_value_C.Text = Properties.Settings.Default.TB_group_In_value_C.ToString();
            TB_group_In_value_D.Text = Properties.Settings.Default.TB_group_In_value_D.ToString();

            TB_TAX.Text = (Form1.TAX * 100).ToString();
            TB_sil_commission.Text = Properties.Settings.Default.TB_sil_commission.ToString();
            TB_mo_commission.Text = Properties.Settings.Default.TB_mo_commission.ToString();

            TB_예약주문_장전매수호가.Text = Properties.Settings.Default.TB_예약주문_장전매수호가.ToString();
            TB_예약주문_장전매수비중.Text = Properties.Settings.Default.TB_예약주문_장전매수비중.ToString();
            CBB_예약주문_장전매수선택.SelectedIndex = Properties.Settings.Default.CBB_예약주문_장전매수선택;
            TB_예약주문_장전매도호가.Text = Properties.Settings.Default.TB_예약주문_장전매도호가.ToString();
            TB_예약주문_장전매도비중.Text = Properties.Settings.Default.TB_예약주문_장전매도비중.ToString();
            CBB_예약주문_장전매도선택.SelectedIndex = Properties.Settings.Default.CBB_예약주문_장전매도선택;
            TB_예약주문_종가매수호가.Text = Properties.Settings.Default.TB_예약주문_종가매수호가.ToString();
            TB_예약주문_종가매수비중.Text = Properties.Settings.Default.TB_예약주문_종가매수비중.ToString();
            CBB_예약주문_종가매수선택.SelectedIndex = Properties.Settings.Default.CBB_예약주문_종가매수선택;
            TB_예약주문_종가매도호가.Text = Properties.Settings.Default.TB_예약주문_종가매도호가.ToString();
            TB_예약주문_종가매도비중.Text = Properties.Settings.Default.TB_예약주문_종가매도비중.ToString();
            CBB_예약주문_종가매도선택.SelectedIndex = Properties.Settings.Default.CBB_예약주문_종가매도선택;

            CB_예약주문_장전체결삭제.Checked = Properties.Settings.Default.CB_예약주문_장전체결삭제;
            CB_예약주문_장전전량매도삭제.Checked = Properties.Settings.Default.CB_예약주문_장전전량매도삭제;

            CB_예약주문_종가체결삭제.Checked = Properties.Settings.Default.CB_예약주문_종가체결삭제;
            CB_예약주문_종가전량매도삭제.Checked = Properties.Settings.Default.CB_예약주문_종가전량매도삭제;

            CBB_예약주문_예약종류.SelectedIndex = Properties.Settings.Default.CBB_예약주문_예약종류;
            TB_예약주문_주문비.Text = Properties.Settings.Default.TB_예약주문_주문비.ToString();

            TB_수동주문_주문비.Text = Properties.Settings.Default.TB_수동주문_주문비.ToString();

            CB_매매기간_기준금.Checked = Properties.Settings.Default.CB_매매기간_기준금;

            CBB_매매기간_주문시간_A.SelectedIndex = Properties.Settings.Default.CBB_매매기간_주문시간_A;
            CBB_매매기간_주문시간_B.SelectedIndex = Properties.Settings.Default.CBB_매매기간_주문시간_B;
            CBB_매매기간_주문시간_C.SelectedIndex = Properties.Settings.Default.CBB_매매기간_주문시간_C;
            CBB_매매기간_주문시간_D.SelectedIndex = Properties.Settings.Default.CBB_매매기간_주문시간_D;
            CBB_매매기간_주문시간_E.SelectedIndex = Properties.Settings.Default.CBB_매매기간_주문시간_E;
            CBB_매매기간_주문시간_F.SelectedIndex = Properties.Settings.Default.CBB_매매기간_주문시간_F;

            CBB_매매기간_choice_A.SelectedIndex = Properties.Settings.Default.CBB_매매기간_choice_A;
            CBB_매매기간_choice_B.SelectedIndex = Properties.Settings.Default.CBB_매매기간_choice_B;
            CBB_매매기간_choice_C.SelectedIndex = Properties.Settings.Default.CBB_매매기간_choice_C;
            CBB_매매기간_choice_D.SelectedIndex = Properties.Settings.Default.CBB_매매기간_choice_D;
            CBB_매매기간_choice_E.SelectedIndex = Properties.Settings.Default.CBB_매매기간_choice_E;
            CBB_매매기간_choice_F.SelectedIndex = Properties.Settings.Default.CBB_매매기간_choice_F;

            CBB_매매기간_Jumun_A.SelectedIndex = Properties.Settings.Default.CBB_매매기간_Jumun_A;
            CBB_매매기간_Jumun_B.SelectedIndex = Properties.Settings.Default.CBB_매매기간_Jumun_B;
            CBB_매매기간_Jumun_C.SelectedIndex = Properties.Settings.Default.CBB_매매기간_Jumun_C;
            CBB_매매기간_Jumun_D.SelectedIndex = Properties.Settings.Default.CBB_매매기간_Jumun_D;
            CBB_매매기간_Jumun_E.SelectedIndex = Properties.Settings.Default.CBB_매매기간_Jumun_E;
            CBB_매매기간_Jumun_F.SelectedIndex = Properties.Settings.Default.CBB_매매기간_Jumun_F;

            CBB_매매기간_trading_A.SelectedIndex = Properties.Settings.Default.CBB_매매기간_trading_A;
            CBB_매매기간_trading_B.SelectedIndex = Properties.Settings.Default.CBB_매매기간_trading_B;
            CBB_매매기간_trading_C.SelectedIndex = Properties.Settings.Default.CBB_매매기간_trading_C;
            CBB_매매기간_trading_D.SelectedIndex = Properties.Settings.Default.CBB_매매기간_trading_D;
            CBB_매매기간_trading_E.SelectedIndex = Properties.Settings.Default.CBB_매매기간_trading_E;
            CBB_매매기간_trading_F.SelectedIndex = Properties.Settings.Default.CBB_매매기간_trading_F;

            CBB_매매기간_기준_A.SelectedIndex = Properties.Settings.Default.CBB_매매기간_기준_A;
            CBB_매매기간_기준_B.SelectedIndex = Properties.Settings.Default.CBB_매매기간_기준_B;
            CBB_매매기간_기준_C.SelectedIndex = Properties.Settings.Default.CBB_매매기간_기준_C;
            CBB_매매기간_기준_D.SelectedIndex = Properties.Settings.Default.CBB_매매기간_기준_D;
            CBB_매매기간_기준_E.SelectedIndex = Properties.Settings.Default.CBB_매매기간_기준_E;
            CBB_매매기간_기준_F.SelectedIndex = Properties.Settings.Default.CBB_매매기간_기준_F;

            MTB_매매기간_기간_A.Text = Properties.Settings.Default.MTB_매매기간_기간_A.ToString();
            MTB_매매기간_기간_B.Text = Properties.Settings.Default.MTB_매매기간_기간_B.ToString();
            MTB_매매기간_기간_C.Text = Properties.Settings.Default.MTB_매매기간_기간_C.ToString();
            MTB_매매기간_기간_D.Text = Properties.Settings.Default.MTB_매매기간_기간_D.ToString();
            MTB_매매기간_기간_E.Text = Properties.Settings.Default.MTB_매매기간_기간_E.ToString();
            MTB_매매기간_기간_F.Text = Properties.Settings.Default.MTB_매매기간_기간_F.ToString();

            TB_매매기간_ratio_A.Text = Properties.Settings.Default.TB_매매기간_ratio_A.ToString();
            TB_매매기간_ratio_B.Text = Properties.Settings.Default.TB_매매기간_ratio_B.ToString();
            TB_매매기간_ratio_C.Text = Properties.Settings.Default.TB_매매기간_ratio_C.ToString();
            TB_매매기간_ratio_D.Text = Properties.Settings.Default.TB_매매기간_ratio_D.ToString();
            TB_매매기간_ratio_E.Text = Properties.Settings.Default.TB_매매기간_ratio_E.ToString();
            TB_매매기간_ratio_F.Text = Properties.Settings.Default.TB_매매기간_ratio_F.ToString();

            TB_매매기간_기준_A.Text = Properties.Settings.Default.TB_매매기간_기준_A.ToString();
            TB_매매기간_기준_B.Text = Properties.Settings.Default.TB_매매기간_기준_B.ToString();
            TB_매매기간_기준_C.Text = Properties.Settings.Default.TB_매매기간_기준_C.ToString();
            TB_매매기간_기준_D.Text = Properties.Settings.Default.TB_매매기간_기준_D.ToString();
            TB_매매기간_기준_E.Text = Properties.Settings.Default.TB_매매기간_기준_E.ToString();
            TB_매매기간_기준_F.Text = Properties.Settings.Default.TB_매매기간_기준_F.ToString();

            TB_매매기간_value_A.Text = Properties.Settings.Default.TB_매매기간_value_A.ToString();
            TB_매매기간_value_B.Text = Properties.Settings.Default.TB_매매기간_value_B.ToString();
            TB_매매기간_value_C.Text = Properties.Settings.Default.TB_매매기간_value_C.ToString();
            TB_매매기간_value_D.Text = Properties.Settings.Default.TB_매매기간_value_D.ToString();
            TB_매매기간_value_E.Text = Properties.Settings.Default.TB_매매기간_value_E.ToString();
            TB_매매기간_value_F.Text = Properties.Settings.Default.TB_매매기간_value_F.ToString();

            CB_매매기간_오전.Checked = Properties.Settings.Default.CB_매매기간_오전;
            CB_매매기간_오후.Checked = Properties.Settings.Default.CB_매매기간_오후;
            TB_매매기간_오전주문시간.Text = Properties.Settings.Default.TB_매매기간_오전주문시간.ToString();
            TB_매매기간_오후주문시간.Text = Properties.Settings.Default.TB_매매기간_오후주문시간.ToString();

            TB_매매기간_취소시간_A.Text = Properties.Settings.Default.TB_매매기간_취소시간_A.ToString();
            TB_매매기간_취소시간_B.Text = Properties.Settings.Default.TB_매매기간_취소시간_B.ToString();
            TB_매매기간_취소시간_C.Text = Properties.Settings.Default.TB_매매기간_취소시간_C.ToString();
            TB_매매기간_취소시간_D.Text = Properties.Settings.Default.TB_매매기간_취소시간_D.ToString();
            TB_매매기간_취소시간_E.Text = Properties.Settings.Default.TB_매매기간_취소시간_E.ToString();
            TB_매매기간_취소시간_F.Text = Properties.Settings.Default.TB_매매기간_취소시간_F.ToString();

            CB_매매기간_강제매도_A.Checked = Properties.Settings.Default.CB_매매기간_강제매도_A;
            CB_매매기간_강제매도_B.Checked = Properties.Settings.Default.CB_매매기간_강제매도_B;
            CB_매매기간_강제매도_C.Checked = Properties.Settings.Default.CB_매매기간_강제매도_C;
            CB_매매기간_강제매도_D.Checked = Properties.Settings.Default.CB_매매기간_강제매도_D;
            CB_매매기간_강제매도_E.Checked = Properties.Settings.Default.CB_매매기간_강제매도_E;
            CB_매매기간_강제매도_F.Checked = Properties.Settings.Default.CB_매매기간_강제매도_F;

            CB_매매기간_수익보전_A.Checked = Properties.Settings.Default.CB_매매기간_수익보전_A;
            CB_매매기간_수익보전_B.Checked = Properties.Settings.Default.CB_매매기간_수익보전_B;
            CB_매매기간_수익보전_C.Checked = Properties.Settings.Default.CB_매매기간_수익보전_C;
            CB_매매기간_수익보전_D.Checked = Properties.Settings.Default.CB_매매기간_수익보전_D;
            CB_매매기간_수익보전_E.Checked = Properties.Settings.Default.CB_매매기간_수익보전_E;
            CB_매매기간_수익보전_F.Checked = Properties.Settings.Default.CB_매매기간_수익보전_F;

            CB_예약주문_장전.Checked = Properties.Settings.Default.CB_예약주문_장전;
            CB_예약주문_종가.Checked = Properties.Settings.Default.CB_예약주문_종가;

            MTB_예약주문_장전주문시간.Text = Properties.Settings.Default.MTB_예약주문_장전주문시간.ToString();
            MTB_예약주문_종가주문시간.Text = Properties.Settings.Default.MTB_예약주문_종가주문시간.ToString();

            CB_수동주문_시장가.Checked = Properties.Settings.Default.CB_수동주문_시장가;

            CB_예약주문_장전연동.Checked = Properties.Settings.Default.CB_예약주문_장전연동;
            CB_예약주문_종가연동.Checked = Properties.Settings.Default.CB_예약주문_종가연동;

            CB_수동주문_주문가고정.Checked = Properties.Settings.Default.CB_수동주문_주문가고정;
            CB_예약주문_주문가고정.Checked = Properties.Settings.Default.CB_예약주문_주문가고정;
            TB_수동주문_tick.Text = Properties.Settings.Default.TB_수동주문_tick.ToString();

            CB_개장일.Checked = Properties.Settings.Default.CB_개장일;
            CB_수능일.Checked = Properties.Settings.Default.CB_수능일;

            CB_개장일.Text = "개장일   " + Form1.form1.개장일.Substring(0, 2) + " 월 " + Form1.form1.개장일.Substring(2, 2) + " 일";
            CB_수능일.Text = "수능일   " + Form1.form1.수능일.Substring(0, 2) + " 월 " + Form1.form1.수능일.Substring(2, 2) + " 일";

            Form1.음소거 = Properties.Settings.Default.CB_음소거;
            this.ActiveControl = CBB_예약주문_예약종류;

            if (Properties.Settings.Default.CB_가이드매매) ControllerDisable.Form_Special_Disable();
        }

        public static void 특수매매_저장()
        {
            try
            {
                double.TryParse(form.TB_sil_commission.Text, out double sil_commission);
                double.TryParse(form.TB_mo_commission.Text, out double mo_commission);

                if (sil_commission == 0) sil_commission = 0.015;
                if (mo_commission == 0) mo_commission = 0.35;

                Properties.Settings.Default.TB_sil_commission = sil_commission;
                Properties.Settings.Default.TB_mo_commission = mo_commission;

                form.TB_sil_commission.Text = sil_commission.ToString();
                form.TB_mo_commission.Text = mo_commission.ToString();

                if (Form1.server.Equals("모의투자")) // 모의, 실투 구분 하기 
                {
                    Form1.수수료 = Properties.Settings.Default.TB_mo_commission / (double)100;
                }
                else
                {
                    Form1.수수료 = Properties.Settings.Default.TB_sil_commission / (double)100;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 세금,수수료 에러: " + e.Message); Form1.Error_Log("특수매매_저장 세금,수수료 에러: " + e.Message);
            }



            try
            {
                Properties.Settings.Default.combo_신규그룹_A = form.combo_신규그룹_A.SelectedIndex;
                Properties.Settings.Default.combo_신규그룹_B = form.combo_신규그룹_B.SelectedIndex;
                Properties.Settings.Default.combo_신규그룹_C = form.combo_신규그룹_C.SelectedIndex;
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 에러: " + e.Message); Form1.Error_Log("특수매매_저장 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_In_group_ratio_A.Text, out double In_group_ratio_A);
                double.TryParse(form.TB_In_group_ratio_B.Text, out double In_group_ratio_B);
                double.TryParse(form.TB_In_group_ratio_C.Text, out double In_group_ratio_C);
                double.TryParse(form.TB_In_group_ratio_D.Text, out double In_group_ratio_D);

                Properties.Settings.Default.TB_In_group_ratio_A = In_group_ratio_A;
                Properties.Settings.Default.TB_In_group_ratio_B = In_group_ratio_B;
                Properties.Settings.Default.TB_In_group_ratio_C = In_group_ratio_C;
                Properties.Settings.Default.TB_In_group_ratio_D = In_group_ratio_D;

                form.TB_In_group_ratio_A.Text = In_group_ratio_A.ToString();
                form.TB_In_group_ratio_B.Text = In_group_ratio_B.ToString();
                form.TB_In_group_ratio_C.Text = In_group_ratio_C.ToString();
                form.TB_In_group_ratio_D.Text = In_group_ratio_D.ToString();

                double.TryParse(form.TB_Out_group_ratio_A.Text, out double Out_group_ratio_A);
                double.TryParse(form.TB_Out_group_ratio_B.Text, out double Out_group_ratio_B);
                double.TryParse(form.TB_Out_group_ratio_C.Text, out double Out_group_ratio_C);
                double.TryParse(form.TB_Out_group_ratio_D.Text, out double Out_group_ratio_D);

                if (In_group_ratio_A == Out_group_ratio_A) Out_group_ratio_A = In_group_ratio_A + 1;
                if (In_group_ratio_B == Out_group_ratio_B) Out_group_ratio_B = In_group_ratio_B + 1;
                if (In_group_ratio_C == Out_group_ratio_C) Out_group_ratio_C = In_group_ratio_C + 1;
                if (In_group_ratio_D == Out_group_ratio_D) Out_group_ratio_D = In_group_ratio_D + 1;

                Properties.Settings.Default.TB_Out_group_ratio_A = Out_group_ratio_A;
                Properties.Settings.Default.TB_Out_group_ratio_B = Out_group_ratio_B;
                Properties.Settings.Default.TB_Out_group_ratio_C = Out_group_ratio_C;
                Properties.Settings.Default.TB_Out_group_ratio_D = Out_group_ratio_D;

                form.TB_Out_group_ratio_A.Text = Out_group_ratio_A.ToString();
                form.TB_Out_group_ratio_B.Text = Out_group_ratio_B.ToString();
                form.TB_Out_group_ratio_C.Text = Out_group_ratio_C.ToString();
                form.TB_Out_group_ratio_D.Text = Out_group_ratio_D.ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 조건별그룹지정 에러: " + e.Message); Form1.Error_Log("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_group_In_won_A.Text, out double In_won_A);
                double.TryParse(form.TB_group_In_won_B.Text, out double In_won_B);
                double.TryParse(form.TB_group_In_won_C.Text, out double In_won_C);
                double.TryParse(form.TB_group_In_won_D.Text, out double In_won_D);

                if (In_won_A == 0) In_won_A = 100;
                if (In_won_B == 0) In_won_B = 100;
                if (In_won_C == 0) In_won_C = 100;
                if (In_won_D == 0) In_won_D = 100;

                Properties.Settings.Default.TB_group_In_won_A = Math.Abs(In_won_A);
                Properties.Settings.Default.TB_group_In_won_B = Math.Abs(In_won_B);
                Properties.Settings.Default.TB_group_In_won_C = Math.Abs(In_won_C);
                Properties.Settings.Default.TB_group_In_won_D = Math.Abs(In_won_D);

                form.TB_group_In_won_A.Text = Properties.Settings.Default.TB_group_In_won_A.ToString();
                form.TB_group_In_won_B.Text = Properties.Settings.Default.TB_group_In_won_B.ToString();
                form.TB_group_In_won_C.Text = Properties.Settings.Default.TB_group_In_won_C.ToString();
                form.TB_group_In_won_D.Text = Properties.Settings.Default.TB_group_In_won_D.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 조건별그룹지정 에러: " + e.Message); Form1.Error_Log("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_group_In_ratio_A.Text, out double group_In_ratio_A);
                double.TryParse(form.TB_group_In_ratio_B.Text, out double group_In_ratio_B);
                double.TryParse(form.TB_group_In_ratio_C.Text, out double group_In_ratio_C);
                double.TryParse(form.TB_group_In_ratio_D.Text, out double group_In_ratio_D);

                if (group_In_ratio_A == 0) group_In_ratio_A = 1;
                if (group_In_ratio_B == 0) group_In_ratio_B = 1;
                if (group_In_ratio_C == 0) group_In_ratio_C = 1;
                if (group_In_ratio_D == 0) group_In_ratio_D = 1;

                Properties.Settings.Default.TB_group_In_ratio_A = Math.Abs(group_In_ratio_A);
                Properties.Settings.Default.TB_group_In_ratio_B = Math.Abs(group_In_ratio_B);
                Properties.Settings.Default.TB_group_In_ratio_C = Math.Abs(group_In_ratio_C);
                Properties.Settings.Default.TB_group_In_ratio_D = Math.Abs(group_In_ratio_D);

                form.TB_group_In_ratio_A.Text = Properties.Settings.Default.TB_group_In_ratio_A.ToString();
                form.TB_group_In_ratio_B.Text = Properties.Settings.Default.TB_group_In_ratio_B.ToString();
                form.TB_group_In_ratio_C.Text = Properties.Settings.Default.TB_group_In_ratio_C.ToString();
                form.TB_group_In_ratio_D.Text = Properties.Settings.Default.TB_group_In_ratio_D.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 조건별그룹지정 에러: " + e.Message); Form1.Error_Log("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_group_In_value_A.Text, out double group_In_value_A);
                double.TryParse(form.TB_group_In_value_B.Text, out double group_In_value_B);
                double.TryParse(form.TB_group_In_value_C.Text, out double group_In_value_C);
                double.TryParse(form.TB_group_In_value_D.Text, out double group_In_value_D);

                if (form.CBB_group_In_jumun_A.SelectedIndex == 0 || form.CBB_group_In_jumun_A.SelectedIndex == 1) group_In_value_A = 0;
                if (form.CBB_group_In_jumun_B.SelectedIndex == 0 || form.CBB_group_In_jumun_B.SelectedIndex == 1) group_In_value_B = 0;
                if (form.CBB_group_In_jumun_C.SelectedIndex == 0 || form.CBB_group_In_jumun_C.SelectedIndex == 1) group_In_value_C = 0;
                if (form.CBB_group_In_jumun_D.SelectedIndex == 0 || form.CBB_group_In_jumun_D.SelectedIndex == 1) group_In_value_D = 0;

                Properties.Settings.Default.TB_group_In_value_A = group_In_value_A;
                Properties.Settings.Default.TB_group_In_value_B = group_In_value_B;
                Properties.Settings.Default.TB_group_In_value_C = group_In_value_C;
                Properties.Settings.Default.TB_group_In_value_D = group_In_value_D;

                form.TB_group_In_value_A.Text = group_In_value_A.ToString();
                form.TB_group_In_value_B.Text = group_In_value_B.ToString();
                form.TB_group_In_value_C.Text = group_In_value_C.ToString();
                form.TB_group_In_value_D.Text = group_In_value_D.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 조건별그룹지정 에러: " + e.Message); Form1.Error_Log("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_group_In_CanselTime_A.Text, out int group_In_CanselTime_A);
                int.TryParse(form.MTB_group_In_CanselTime_B.Text, out int group_In_CanselTime_B);
                int.TryParse(form.MTB_group_In_CanselTime_C.Text, out int group_In_CanselTime_C);
                int.TryParse(form.MTB_group_In_CanselTime_D.Text, out int group_In_CanselTime_D);

                if (group_In_CanselTime_A == 0) group_In_CanselTime_A = 30;
                if (group_In_CanselTime_B == 0) group_In_CanselTime_B = 30;
                if (group_In_CanselTime_C == 0) group_In_CanselTime_C = 30;
                if (group_In_CanselTime_D == 0) group_In_CanselTime_D = 30;

                Properties.Settings.Default.MTB_group_In_CanselTime_A = group_In_CanselTime_A;
                Properties.Settings.Default.MTB_group_In_CanselTime_B = group_In_CanselTime_B;
                Properties.Settings.Default.MTB_group_In_CanselTime_C = group_In_CanselTime_C;
                Properties.Settings.Default.MTB_group_In_CanselTime_D = group_In_CanselTime_D;

                form.MTB_group_In_CanselTime_A.Text = group_In_CanselTime_A.ToString();
                form.MTB_group_In_CanselTime_B.Text = group_In_CanselTime_B.ToString();
                form.MTB_group_In_CanselTime_C.Text = group_In_CanselTime_C.ToString();
                form.MTB_group_In_CanselTime_D.Text = group_In_CanselTime_D.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 조건별그룹지정 에러: " + e.Message); Form1.Error_Log("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_group_In_repeat_A.Text, out int group_In_repeat_A);
                int.TryParse(form.MTB_group_In_repeat_B.Text, out int group_In_repeat_B);
                int.TryParse(form.MTB_group_In_repeat_C.Text, out int group_In_repeat_C);
                int.TryParse(form.MTB_group_In_repeat_D.Text, out int group_In_repeat_D);

                Properties.Settings.Default.MTB_group_In_repeat_A = group_In_repeat_A;
                Properties.Settings.Default.MTB_group_In_repeat_B = group_In_repeat_B;
                Properties.Settings.Default.MTB_group_In_repeat_C = group_In_repeat_C;
                Properties.Settings.Default.MTB_group_In_repeat_D = group_In_repeat_D;

                form.MTB_group_In_repeat_A.Text = group_In_repeat_A.ToString();
                form.MTB_group_In_repeat_B.Text = group_In_repeat_B.ToString();
                form.MTB_group_In_repeat_C.Text = group_In_repeat_C.ToString();
                form.MTB_group_In_repeat_D.Text = group_In_repeat_D.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 조건별그룹지정 에러: " + e.Message); Form1.Error_Log("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }


            try
            {
                double.TryParse(form.TB_group_out_ratio_A.Text, out double group_out_ratio_A);
                double.TryParse(form.TB_group_out_ratio_B.Text, out double group_out_ratio_B);
                double.TryParse(form.TB_group_out_ratio_C.Text, out double group_out_ratio_C);
                double.TryParse(form.TB_group_out_ratio_D.Text, out double group_out_ratio_D);

                if (group_out_ratio_A == 0) group_out_ratio_A = 1;
                if (group_out_ratio_B == 0) group_out_ratio_B = 1;
                if (group_out_ratio_C == 0) group_out_ratio_C = 1;
                if (group_out_ratio_D == 0) group_out_ratio_D = 1;

                Properties.Settings.Default.TB_group_out_ratio_A = Math.Abs(group_out_ratio_A);
                Properties.Settings.Default.TB_group_out_ratio_B = Math.Abs(group_out_ratio_B);
                Properties.Settings.Default.TB_group_out_ratio_C = Math.Abs(group_out_ratio_C);
                Properties.Settings.Default.TB_group_out_ratio_D = Math.Abs(group_out_ratio_D);

                form.TB_group_out_ratio_A.Text = Properties.Settings.Default.TB_group_out_ratio_A.ToString();
                form.TB_group_out_ratio_B.Text = Properties.Settings.Default.TB_group_out_ratio_B.ToString();
                form.TB_group_out_ratio_C.Text = Properties.Settings.Default.TB_group_out_ratio_C.ToString();
                form.TB_group_out_ratio_D.Text = Properties.Settings.Default.TB_group_out_ratio_D.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 조건별그룹지정 에러: " + e.Message); Form1.Error_Log("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_group_Out_value_A.Text, out double group_Out_value_A);
                double.TryParse(form.TB_group_Out_value_B.Text, out double group_Out_value_B);
                double.TryParse(form.TB_group_Out_value_C.Text, out double group_Out_value_C);
                double.TryParse(form.TB_group_Out_value_D.Text, out double group_Out_value_D);

                if (form.CBB_group_out_jumun_A.SelectedIndex == 0 || form.CBB_group_out_jumun_A.SelectedIndex == 1) group_Out_value_A = 0;
                if (form.CBB_group_out_jumun_B.SelectedIndex == 0 || form.CBB_group_out_jumun_B.SelectedIndex == 1) group_Out_value_B = 0;
                if (form.CBB_group_out_jumun_C.SelectedIndex == 0 || form.CBB_group_out_jumun_C.SelectedIndex == 1) group_Out_value_C = 0;
                if (form.CBB_group_out_jumun_D.SelectedIndex == 0 || form.CBB_group_out_jumun_D.SelectedIndex == 1) group_Out_value_D = 0;

                Properties.Settings.Default.TB_group_Out_value_A = group_Out_value_A;
                Properties.Settings.Default.TB_group_Out_value_B = group_Out_value_B;
                Properties.Settings.Default.TB_group_Out_value_C = group_Out_value_C;
                Properties.Settings.Default.TB_group_Out_value_D = group_Out_value_D;

                form.TB_group_Out_value_A.Text = group_Out_value_A.ToString();
                form.TB_group_Out_value_B.Text = group_Out_value_B.ToString();
                form.TB_group_Out_value_C.Text = group_Out_value_C.ToString();
                form.TB_group_Out_value_D.Text = group_Out_value_D.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 조건별그룹지정 에러: " + e.Message); Form1.Error_Log("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_group_out_CanselTime_A.Text, out int group_out_CanselTime_A);
                int.TryParse(form.MTB_group_out_CanselTime_B.Text, out int group_out_CanselTime_B);
                int.TryParse(form.MTB_group_out_CanselTime_C.Text, out int group_out_CanselTime_C);
                int.TryParse(form.MTB_group_out_CanselTime_D.Text, out int group_out_CanselTime_D);

                if (group_out_CanselTime_A == 0) group_out_CanselTime_A = 30;
                if (group_out_CanselTime_B == 0) group_out_CanselTime_B = 30;
                if (group_out_CanselTime_C == 0) group_out_CanselTime_C = 30;
                if (group_out_CanselTime_D == 0) group_out_CanselTime_D = 30;

                Properties.Settings.Default.MTB_group_out_CanselTime_A = group_out_CanselTime_A;
                Properties.Settings.Default.MTB_group_out_CanselTime_B = group_out_CanselTime_B;
                Properties.Settings.Default.MTB_group_out_CanselTime_C = group_out_CanselTime_C;
                Properties.Settings.Default.MTB_group_out_CanselTime_D = group_out_CanselTime_D;

                form.MTB_group_out_CanselTime_A.Text = group_out_CanselTime_A.ToString();
                form.MTB_group_out_CanselTime_B.Text = group_out_CanselTime_B.ToString();
                form.MTB_group_out_CanselTime_C.Text = group_out_CanselTime_C.ToString();
                form.MTB_group_out_CanselTime_D.Text = group_out_CanselTime_D.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 조건별그룹지정 에러: " + e.Message); Form1.Error_Log("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_group_out_repeat_A.Text, out int group_out_repeat_A);
                int.TryParse(form.MTB_group_out_repeat_B.Text, out int group_out_repeat_B);
                int.TryParse(form.MTB_group_out_repeat_C.Text, out int group_out_repeat_C);
                int.TryParse(form.MTB_group_out_repeat_D.Text, out int group_out_repeat_D);

                Properties.Settings.Default.MTB_group_out_repeat_A = group_out_repeat_A;
                Properties.Settings.Default.MTB_group_out_repeat_B = group_out_repeat_B;
                Properties.Settings.Default.MTB_group_out_repeat_C = group_out_repeat_C;
                Properties.Settings.Default.MTB_group_out_repeat_D = group_out_repeat_D;

                form.MTB_group_out_repeat_A.Text = group_out_repeat_A.ToString();
                form.MTB_group_out_repeat_B.Text = group_out_repeat_B.ToString();
                form.MTB_group_out_repeat_C.Text = group_out_repeat_C.ToString();
                form.MTB_group_out_repeat_D.Text = group_out_repeat_D.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 조건별그룹지정 에러: " + e.Message); Form1.Error_Log("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }

            try
            {
                Properties.Settings.Default.CBB_group_In_won_A = form.CBB_group_In_won_A.SelectedIndex;
                Properties.Settings.Default.CBB_group_In_won_B = form.CBB_group_In_won_B.SelectedIndex;
                Properties.Settings.Default.CBB_group_In_won_C = form.CBB_group_In_won_C.SelectedIndex;
                Properties.Settings.Default.CBB_group_In_won_D = form.CBB_group_In_won_D.SelectedIndex;

                Properties.Settings.Default.CBB_In_group_choice_A = form.CBB_I_group_choice_A.SelectedIndex;
                Properties.Settings.Default.CBB_In_group_choice_B = form.CBB_I_group_choice_B.SelectedIndex;
                Properties.Settings.Default.CBB_In_group_choice_C = form.CBB_I_group_choice_C.SelectedIndex;
                Properties.Settings.Default.CBB_In_group_choice_D = form.CBB_I_group_choice_D.SelectedIndex;

                Properties.Settings.Default.CBB_Out_group_choice_A = form.CBB_O_group_choice_A.SelectedIndex;
                Properties.Settings.Default.CBB_Out_group_choice_B = form.CBB_O_group_choice_B.SelectedIndex;
                Properties.Settings.Default.CBB_Out_group_choice_C = form.CBB_O_group_choice_C.SelectedIndex;
                Properties.Settings.Default.CBB_Out_group_choice_D = form.CBB_O_group_choice_D.SelectedIndex;

                Properties.Settings.Default.CBB_Out_group_A = form.CBB_Out_group_A.SelectedIndex;
                Properties.Settings.Default.CBB_Out_group_B = form.CBB_Out_group_B.SelectedIndex;
                Properties.Settings.Default.CBB_Out_group_C = form.CBB_Out_group_C.SelectedIndex;
                Properties.Settings.Default.CBB_Out_group_D = form.CBB_Out_group_D.SelectedIndex;

                Properties.Settings.Default.CBB_group_In_jumun_A = form.CBB_group_In_jumun_A.SelectedIndex;
                Properties.Settings.Default.CBB_group_In_choice_A = form.CBB_group_In_choice_A.SelectedIndex;
                Properties.Settings.Default.CBB_group_out_jumun_A = form.CBB_group_out_jumun_A.SelectedIndex;
                Properties.Settings.Default.CBB_group_out_choice_A = form.CBB_group_out_choice_A.SelectedIndex;

                Properties.Settings.Default.CBB_group_In_jumun_B = form.CBB_group_In_jumun_B.SelectedIndex;
                Properties.Settings.Default.CBB_group_In_choice_B = form.CBB_group_In_choice_B.SelectedIndex;
                Properties.Settings.Default.CBB_group_out_jumun_B = form.CBB_group_out_jumun_B.SelectedIndex;
                Properties.Settings.Default.CBB_group_out_choice_B = form.CBB_group_out_choice_B.SelectedIndex;

                Properties.Settings.Default.CBB_group_In_jumun_C = form.CBB_group_In_jumun_C.SelectedIndex;
                Properties.Settings.Default.CBB_group_In_choice_C = form.CBB_group_In_choice_C.SelectedIndex;
                Properties.Settings.Default.CBB_group_out_jumun_C = form.CBB_group_out_jumun_C.SelectedIndex;
                Properties.Settings.Default.CBB_group_out_choice_C = form.CBB_group_out_choice_C.SelectedIndex;

                Properties.Settings.Default.CBB_group_In_jumun_D = form.CBB_group_In_jumun_D.SelectedIndex;
                Properties.Settings.Default.CBB_group_In_choice_D = form.CBB_group_In_choice_D.SelectedIndex;
                Properties.Settings.Default.CBB_group_out_jumun_D = form.CBB_group_out_jumun_D.SelectedIndex;
                Properties.Settings.Default.CBB_group_out_choice_D = form.CBB_group_out_choice_D.SelectedIndex;

                Properties.Settings.Default.CBB_In_group_A = form.CBB_In_group_A.SelectedIndex;
                Properties.Settings.Default.CBB_In_group_B = form.CBB_In_group_B.SelectedIndex;
                Properties.Settings.Default.CBB_In_group_C = form.CBB_In_group_C.SelectedIndex;
                Properties.Settings.Default.CBB_In_group_D = form.CBB_In_group_D.SelectedIndex;
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 조건별그룹지정 에러: " + e.Message); Form1.Error_Log("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }


            try
            {
                int.TryParse(form.TB_예약주문_장전매수호가.Text, out int 장전매수);
                int.TryParse(form.TB_예약주문_장전매도호가.Text, out int 장전매도);
                int.TryParse(form.TB_예약주문_종가매수호가.Text, out int 종가매수);
                int.TryParse(form.TB_예약주문_종가매도호가.Text, out int 종가매도);

                Properties.Settings.Default.TB_예약주문_장전매수호가 = 장전매수;
                Properties.Settings.Default.TB_예약주문_장전매도호가 = 장전매도;
                Properties.Settings.Default.TB_예약주문_종가매수호가 = 종가매수;
                Properties.Settings.Default.TB_예약주문_종가매도호가 = 종가매도;

                form.TB_예약주문_장전매수호가.Text = 장전매수.ToString();
                form.TB_예약주문_장전매도호가.Text = 장전매도.ToString();
                form.TB_예약주문_종가매수호가.Text = 종가매수.ToString();
                form.TB_예약주문_종가매도호가.Text = 종가매도.ToString();

                double.TryParse(form.TB_예약주문_장전매수비중.Text, out double 장전매수비중);
                double.TryParse(form.TB_예약주문_장전매도비중.Text, out double 장전매도비중);
                double.TryParse(form.TB_예약주문_종가매수비중.Text, out double 종가매수비중);
                double.TryParse(form.TB_예약주문_종가매도비중.Text, out double 종가매도비중);

                Properties.Settings.Default.TB_예약주문_장전매수비중 = Math.Abs(장전매수비중);
                Properties.Settings.Default.TB_예약주문_장전매도비중 = Math.Abs(장전매도비중);
                Properties.Settings.Default.TB_예약주문_종가매수비중 = Math.Abs(종가매수비중);
                Properties.Settings.Default.TB_예약주문_종가매도비중 = Math.Abs(종가매도비중);

                form.TB_예약주문_장전매수비중.Text = Properties.Settings.Default.TB_예약주문_장전매수비중.ToString();
                form.TB_예약주문_장전매도비중.Text = Properties.Settings.Default.TB_예약주문_장전매도비중.ToString();
                form.TB_예약주문_종가매수비중.Text = Properties.Settings.Default.TB_예약주문_종가매수비중.ToString();
                form.TB_예약주문_종가매도비중.Text = Properties.Settings.Default.TB_예약주문_종가매도비중.ToString();

                Properties.Settings.Default.CBB_예약주문_장전매수선택 = form.CBB_예약주문_장전매수선택.SelectedIndex;
                Properties.Settings.Default.CBB_예약주문_장전매도선택 = form.CBB_예약주문_장전매도선택.SelectedIndex;
                Properties.Settings.Default.CBB_예약주문_종가매수선택 = form.CBB_예약주문_종가매수선택.SelectedIndex;
                Properties.Settings.Default.CBB_예약주문_종가매도선택 = form.CBB_예약주문_종가매도선택.SelectedIndex;


                double.TryParse(form.TB_예약주문_주문비.Text, out double 주문_예약);
                Properties.Settings.Default.TB_예약주문_주문비 = 주문_예약;
                form.TB_예약주문_주문비.Text = 주문_예약.ToString();

                Properties.Settings.Default.CBB_예약주문_예약종류 = form.CBB_예약주문_예약종류.SelectedIndex;
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 주문예약 에러: " + e.Message); Form1.Error_Log("특수매매_저장 주문예약 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_예약주문_장전주문시간.Text, out int 장전주문시간);
                int.TryParse(form.MTB_예약주문_종가주문시간.Text, out int 종가주문시간);

                if (Form1.NXT)
                {
                    if (장전주문시간 < 80000 || 120000 < 장전주문시간) 장전주문시간 = 80000;
                    if (120000 > 종가주문시간 || 종가주문시간 > 200000) 종가주문시간 = 195500;
                }
                else
                {
                    if (장전주문시간 < 84000 || 120000 < 장전주문시간) 장전주문시간 = 85000;
                    if (120000 > 종가주문시간 || 종가주문시간 > 153000) 종가주문시간 = 151500;
                }

                Properties.Settings.Default.MTB_예약주문_장전주문시간 = 장전주문시간;
                Properties.Settings.Default.MTB_예약주문_종가주문시간 = 종가주문시간;

                form.MTB_예약주문_장전주문시간.Text = 장전주문시간.ToString();
                form.MTB_예약주문_종가주문시간.Text = 종가주문시간.ToString();

                if (Properties.Settings.Default.MTB_예약주문_장전주문시간 > Form1.timenow) Form1.예약주문_장전 = true;
                if (Properties.Settings.Default.MTB_예약주문_종가주문시간 > Form1.timenow) Form1.예약주문_종가 = true;


            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 장전,종가 주문시간 에러: " + e.Message); Form1.Error_Log("특수매매_저장 장전,종가 주문시간 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_매매기간_오전주문시간.Text, out int 오전주문시간);
                int.TryParse(form.TB_매매기간_오후주문시간.Text, out int 오후주문시간);

                if (Form1.NXT)
                {
                    if (오전주문시간 < 080100 || 120000 < 오전주문시간) 오전주문시간 = 080100;
                    if (120000 > 오후주문시간 || 오후주문시간 > 200000) 오후주문시간 = 195000;
                }
                else
                {
                    if (오전주문시간 < 090100 || 120000 < 오전주문시간) 오전주문시간 = 090100;
                    if (120000 > 오후주문시간 || 오후주문시간 > 152000) 오후주문시간 = 151000;
                }


                Properties.Settings.Default.TB_매매기간_오전주문시간 = 오전주문시간;
                Properties.Settings.Default.TB_매매기간_오후주문시간 = 오후주문시간;

                form.TB_매매기간_오전주문시간.Text = 오전주문시간.ToString();
                form.TB_매매기간_오후주문시간.Text = 오후주문시간.ToString();

                if (Properties.Settings.Default.TB_매매기간_오전주문시간 > Form1.timenow) Form1.매매기간_오전 = true;
                if (Properties.Settings.Default.TB_매매기간_오후주문시간 > Form1.timenow) Form1.매매기간_오후 = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 장전,종가 주문시간 에러: " + e.Message); Form1.Error_Log("특수매매_저장 장전,종가 주문시간 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_매매기간_기간_A.Text, out int 기간_A);
                int.TryParse(form.MTB_매매기간_기간_B.Text, out int 기간_B);
                int.TryParse(form.MTB_매매기간_기간_C.Text, out int 기간_C);
                int.TryParse(form.MTB_매매기간_기간_D.Text, out int 기간_D);
                int.TryParse(form.MTB_매매기간_기간_E.Text, out int 기간_E);
                int.TryParse(form.MTB_매매기간_기간_F.Text, out int 기간_F);

                if (기간_A == 0) 기간_A = 1;
                if (기간_B == 0) 기간_B = 2;
                if (기간_C == 0) 기간_C = 3;
                if (기간_D == 0) 기간_D = 4;
                if (기간_E == 0) 기간_E = 5;
                if (기간_F == 0) 기간_F = 6;

                Properties.Settings.Default.MTB_매매기간_기간_A = 기간_A;
                Properties.Settings.Default.MTB_매매기간_기간_B = 기간_B;
                Properties.Settings.Default.MTB_매매기간_기간_C = 기간_C;
                Properties.Settings.Default.MTB_매매기간_기간_D = 기간_D;
                Properties.Settings.Default.MTB_매매기간_기간_E = 기간_E;
                Properties.Settings.Default.MTB_매매기간_기간_F = 기간_F;

                form.MTB_매매기간_기간_A.Text = 기간_A.ToString();
                form.MTB_매매기간_기간_B.Text = 기간_B.ToString();
                form.MTB_매매기간_기간_C.Text = 기간_C.ToString();
                form.MTB_매매기간_기간_D.Text = 기간_D.ToString();
                form.MTB_매매기간_기간_E.Text = 기간_E.ToString();
                form.MTB_매매기간_기간_F.Text = 기간_F.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 매매기간 주문 에러: " + e.Message); Form1.Error_Log("특수매매_저장 매매기간 주문 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_매매기간_기준_A.Text, out double day_기준_A);
                double.TryParse(form.TB_매매기간_기준_B.Text, out double day_기준_B);
                double.TryParse(form.TB_매매기간_기준_C.Text, out double day_기준_C);
                double.TryParse(form.TB_매매기간_기준_D.Text, out double day_기준_D);
                double.TryParse(form.TB_매매기간_기준_E.Text, out double day_기준_E);
                double.TryParse(form.TB_매매기간_기준_F.Text, out double day_기준_F);

                Properties.Settings.Default.TB_매매기간_기준_A = day_기준_A;
                Properties.Settings.Default.TB_매매기간_기준_B = day_기준_B;
                Properties.Settings.Default.TB_매매기간_기준_C = day_기준_C;
                Properties.Settings.Default.TB_매매기간_기준_D = day_기준_D;
                Properties.Settings.Default.TB_매매기간_기준_E = day_기준_E;
                Properties.Settings.Default.TB_매매기간_기준_F = day_기준_F;

                form.TB_매매기간_기준_A.Text = day_기준_A.ToString();
                form.TB_매매기간_기준_B.Text = day_기준_B.ToString();
                form.TB_매매기간_기준_C.Text = day_기준_C.ToString();
                form.TB_매매기간_기준_D.Text = day_기준_D.ToString();
                form.TB_매매기간_기준_E.Text = day_기준_E.ToString();
                form.TB_매매기간_기준_F.Text = day_기준_F.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 매매기간 주문 에러: " + e.Message); Form1.Error_Log("특수매매_저장 매매기간 주문 에러: " + e.Message);
            }


            try
            {
                double.TryParse(form.TB_매매기간_ratio_A.Text, out double Ratio_A);
                double.TryParse(form.TB_매매기간_ratio_B.Text, out double Ratio_B);
                double.TryParse(form.TB_매매기간_ratio_C.Text, out double Ratio_C);
                double.TryParse(form.TB_매매기간_ratio_D.Text, out double Ratio_D);
                double.TryParse(form.TB_매매기간_ratio_E.Text, out double Ratio_E);
                double.TryParse(form.TB_매매기간_ratio_F.Text, out double Ratio_F);

                if (Ratio_A == 0) Ratio_A = 1;
                if (Ratio_B == 0) Ratio_B = 1;
                if (Ratio_C == 0) Ratio_C = 1;
                if (Ratio_D == 0) Ratio_D = 1;
                if (Ratio_E == 0) Ratio_E = 1;
                if (Ratio_F == 0) Ratio_F = 1;

                Properties.Settings.Default.TB_매매기간_ratio_A = Math.Abs(Ratio_A);
                Properties.Settings.Default.TB_매매기간_ratio_B = Math.Abs(Ratio_B);
                Properties.Settings.Default.TB_매매기간_ratio_C = Math.Abs(Ratio_C);
                Properties.Settings.Default.TB_매매기간_ratio_D = Math.Abs(Ratio_D);
                Properties.Settings.Default.TB_매매기간_ratio_E = Math.Abs(Ratio_E);
                Properties.Settings.Default.TB_매매기간_ratio_F = Math.Abs(Ratio_F);

                form.TB_매매기간_ratio_A.Text = Properties.Settings.Default.TB_매매기간_ratio_A.ToString();
                form.TB_매매기간_ratio_B.Text = Properties.Settings.Default.TB_매매기간_ratio_B.ToString();
                form.TB_매매기간_ratio_C.Text = Properties.Settings.Default.TB_매매기간_ratio_C.ToString();
                form.TB_매매기간_ratio_D.Text = Properties.Settings.Default.TB_매매기간_ratio_D.ToString();
                form.TB_매매기간_ratio_E.Text = Properties.Settings.Default.TB_매매기간_ratio_E.ToString();
                form.TB_매매기간_ratio_F.Text = Properties.Settings.Default.TB_매매기간_ratio_F.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 매매기간 주문 에러: " + e.Message); Form1.Error_Log("특수매매_저장 매매기간 주문 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_매매기간_value_A.Text, out double day_value_A);
                double.TryParse(form.TB_매매기간_value_B.Text, out double day_value_B);
                double.TryParse(form.TB_매매기간_value_C.Text, out double day_value_C);
                double.TryParse(form.TB_매매기간_value_D.Text, out double day_value_D);
                double.TryParse(form.TB_매매기간_value_E.Text, out double day_value_E);
                double.TryParse(form.TB_매매기간_value_F.Text, out double day_value_F);

                if (form.CBB_매매기간_Jumun_A.SelectedIndex == 0 || form.CBB_매매기간_Jumun_A.SelectedIndex == 1) day_value_A = 0;
                if (form.CBB_매매기간_Jumun_B.SelectedIndex == 0 || form.CBB_매매기간_Jumun_B.SelectedIndex == 1) day_value_B = 0;
                if (form.CBB_매매기간_Jumun_C.SelectedIndex == 0 || form.CBB_매매기간_Jumun_C.SelectedIndex == 1) day_value_C = 0;
                if (form.CBB_매매기간_Jumun_D.SelectedIndex == 0 || form.CBB_매매기간_Jumun_D.SelectedIndex == 1) day_value_D = 0;
                if (form.CBB_매매기간_Jumun_E.SelectedIndex == 0 || form.CBB_매매기간_Jumun_E.SelectedIndex == 1) day_value_E = 0;
                if (form.CBB_매매기간_Jumun_F.SelectedIndex == 0 || form.CBB_매매기간_Jumun_F.SelectedIndex == 1) day_value_F = 0;

                Properties.Settings.Default.TB_매매기간_value_A = day_value_A;
                Properties.Settings.Default.TB_매매기간_value_B = day_value_B;
                Properties.Settings.Default.TB_매매기간_value_C = day_value_C;
                Properties.Settings.Default.TB_매매기간_value_D = day_value_D;
                Properties.Settings.Default.TB_매매기간_value_E = day_value_E;
                Properties.Settings.Default.TB_매매기간_value_F = day_value_F;

                form.TB_매매기간_value_A.Text = day_value_A.ToString();
                form.TB_매매기간_value_B.Text = day_value_B.ToString();
                form.TB_매매기간_value_C.Text = day_value_C.ToString();
                form.TB_매매기간_value_D.Text = day_value_D.ToString();
                form.TB_매매기간_value_E.Text = day_value_E.ToString();
                form.TB_매매기간_value_F.Text = day_value_F.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 매매기간 주문 에러: " + e.Message); Form1.Error_Log("특수매매_저장 매매기간 주문 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_매매기간_취소시간_A.Text, out int 취소시간_A);
                int.TryParse(form.TB_매매기간_취소시간_B.Text, out int 취소시간_B);
                int.TryParse(form.TB_매매기간_취소시간_C.Text, out int 취소시간_C);
                int.TryParse(form.TB_매매기간_취소시간_D.Text, out int 취소시간_D);
                int.TryParse(form.TB_매매기간_취소시간_E.Text, out int 취소시간_E);
                int.TryParse(form.TB_매매기간_취소시간_F.Text, out int 취소시간_F);

                if (취소시간_A == 0) 취소시간_A = 600;
                if (취소시간_B == 0) 취소시간_B = 600;
                if (취소시간_C == 0) 취소시간_C = 600;
                if (취소시간_D == 0) 취소시간_D = 600;
                if (취소시간_E == 0) 취소시간_E = 600;
                if (취소시간_F == 0) 취소시간_F = 600;

                Properties.Settings.Default.TB_매매기간_취소시간_A = 취소시간_A;
                Properties.Settings.Default.TB_매매기간_취소시간_B = 취소시간_B;
                Properties.Settings.Default.TB_매매기간_취소시간_C = 취소시간_C;
                Properties.Settings.Default.TB_매매기간_취소시간_D = 취소시간_D;
                Properties.Settings.Default.TB_매매기간_취소시간_E = 취소시간_E;
                Properties.Settings.Default.TB_매매기간_취소시간_F = 취소시간_F;

                form.TB_매매기간_취소시간_A.Text = 취소시간_A.ToString();
                form.TB_매매기간_취소시간_B.Text = 취소시간_B.ToString();
                form.TB_매매기간_취소시간_C.Text = 취소시간_C.ToString();
                form.TB_매매기간_취소시간_D.Text = 취소시간_D.ToString();
                form.TB_매매기간_취소시간_E.Text = 취소시간_E.ToString();
                form.TB_매매기간_취소시간_F.Text = 취소시간_F.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 취소시간 입력 에러: " + e.Message); Form1.Error_Log("특수매매_저장 취소시간 입력 에러");
            }

            Properties.Settings.Default.CB_매매기간_오전 = form.CB_매매기간_오전.Checked;
            Properties.Settings.Default.CB_매매기간_오후 = form.CB_매매기간_오후.Checked;

            Properties.Settings.Default.CB_매매기간_강제매도_A = form.CB_매매기간_강제매도_A.Checked;
            Properties.Settings.Default.CB_매매기간_강제매도_B = form.CB_매매기간_강제매도_B.Checked;
            Properties.Settings.Default.CB_매매기간_강제매도_C = form.CB_매매기간_강제매도_C.Checked;
            Properties.Settings.Default.CB_매매기간_강제매도_D = form.CB_매매기간_강제매도_D.Checked;
            Properties.Settings.Default.CB_매매기간_강제매도_E = form.CB_매매기간_강제매도_E.Checked;
            Properties.Settings.Default.CB_매매기간_강제매도_F = form.CB_매매기간_강제매도_F.Checked;

            Properties.Settings.Default.CB_매매기간_수익보전_A = form.CB_매매기간_수익보전_A.Checked;
            Properties.Settings.Default.CB_매매기간_수익보전_B = form.CB_매매기간_수익보전_B.Checked;
            Properties.Settings.Default.CB_매매기간_수익보전_C = form.CB_매매기간_수익보전_C.Checked;
            Properties.Settings.Default.CB_매매기간_수익보전_D = form.CB_매매기간_수익보전_D.Checked;
            Properties.Settings.Default.CB_매매기간_수익보전_E = form.CB_매매기간_수익보전_E.Checked;
            Properties.Settings.Default.CB_매매기간_수익보전_F = form.CB_매매기간_수익보전_F.Checked;

            Properties.Settings.Default.CBB_매매기간_trading_A = form.CBB_매매기간_trading_A.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_trading_B = form.CBB_매매기간_trading_B.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_trading_C = form.CBB_매매기간_trading_C.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_trading_D = form.CBB_매매기간_trading_D.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_trading_E = form.CBB_매매기간_trading_E.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_trading_F = form.CBB_매매기간_trading_F.SelectedIndex;


            Properties.Settings.Default.CBB_매매기간_Jumun_A = form.CBB_매매기간_Jumun_A.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_Jumun_B = form.CBB_매매기간_Jumun_B.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_Jumun_C = form.CBB_매매기간_Jumun_C.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_Jumun_D = form.CBB_매매기간_Jumun_D.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_Jumun_E = form.CBB_매매기간_Jumun_E.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_Jumun_F = form.CBB_매매기간_Jumun_F.SelectedIndex;

            Properties.Settings.Default.CBB_매매기간_choice_A = form.CBB_매매기간_choice_A.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_choice_B = form.CBB_매매기간_choice_B.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_choice_C = form.CBB_매매기간_choice_C.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_choice_D = form.CBB_매매기간_choice_D.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_choice_E = form.CBB_매매기간_choice_E.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_choice_F = form.CBB_매매기간_choice_F.SelectedIndex;

            Properties.Settings.Default.CBB_매매기간_기준_A = form.CBB_매매기간_기준_A.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_기준_B = form.CBB_매매기간_기준_B.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_기준_C = form.CBB_매매기간_기준_C.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_기준_D = form.CBB_매매기간_기준_D.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_기준_E = form.CBB_매매기간_기준_E.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_기준_F = form.CBB_매매기간_기준_F.SelectedIndex;

            Properties.Settings.Default.CB_group_기준금 = form.CB_group_기준금.Checked;
            Properties.Settings.Default.CB_매매기간_기준금 = form.CB_매매기간_기준금.Checked;
            Properties.Settings.Default.CB_Group_In_work_A = form.CB_Group_In_work_A.Checked;
            Properties.Settings.Default.CB_Group_In_work_B = form.CB_Group_In_work_B.Checked;
            Properties.Settings.Default.CB_Group_In_work_C = form.CB_Group_In_work_C.Checked;
            Properties.Settings.Default.CB_Group_In_work_D = form.CB_Group_In_work_D.Checked;
            Properties.Settings.Default.CB_Group_Out_work_A = form.CB_Group_Out_work_A.Checked;
            Properties.Settings.Default.CB_Group_Out_work_B = form.CB_Group_Out_work_B.Checked;
            Properties.Settings.Default.CB_Group_Out_work_C = form.CB_Group_Out_work_C.Checked;
            Properties.Settings.Default.CB_Group_Out_work_D = form.CB_Group_Out_work_D.Checked;
            Properties.Settings.Default.CB_Group_In_trading_A = form.CB_Group_In_trading_A.Checked;
            Properties.Settings.Default.CB_Group_In_trading_B = form.CB_Group_In_trading_B.Checked;
            Properties.Settings.Default.CB_Group_In_trading_C = form.CB_Group_In_trading_C.Checked;
            Properties.Settings.Default.CB_Group_In_trading_D = form.CB_Group_In_trading_D.Checked;
            Properties.Settings.Default.CB_Group_Out_trading_A = form.CB_Group_Out_trading_A.Checked;
            Properties.Settings.Default.CB_Group_Out_trading_B = form.CB_Group_Out_trading_B.Checked;
            Properties.Settings.Default.CB_Group_Out_trading_C = form.CB_Group_Out_trading_C.Checked;
            Properties.Settings.Default.CB_Group_Out_trading_D = form.CB_Group_Out_trading_D.Checked;
            Properties.Settings.Default.CB_group_In_updown_A = form.CB_group_In_updown_A.Checked;
            Properties.Settings.Default.CB_group_In_updown_B = form.CB_group_In_updown_B.Checked;
            Properties.Settings.Default.CB_group_In_updown_C = form.CB_group_In_updown_C.Checked;
            Properties.Settings.Default.CB_group_In_updown_D = form.CB_group_In_updown_D.Checked;
            Properties.Settings.Default.CB_group_Out_updown_A = form.CB_group_Out_updown_A.Checked;
            Properties.Settings.Default.CB_group_Out_updown_B = form.CB_group_Out_updown_B.Checked;
            Properties.Settings.Default.CB_group_Out_updown_C = form.CB_group_Out_updown_C.Checked;
            Properties.Settings.Default.CB_group_Out_updown_D = form.CB_group_Out_updown_D.Checked;

            Properties.Settings.Default.CB_예약주문_장전체결삭제 = form.CB_예약주문_장전체결삭제.Checked;
            Properties.Settings.Default.CB_예약주문_장전전량매도삭제 = form.CB_예약주문_장전전량매도삭제.Checked;
            Properties.Settings.Default.CB_예약주문_종가체결삭제 = form.CB_예약주문_종가체결삭제.Checked;
            Properties.Settings.Default.CB_예약주문_종가전량매도삭제 = form.CB_예약주문_종가전량매도삭제.Checked;


            Properties.Settings.Default.CB_예약주문_장전연동 = form.CB_예약주문_장전연동.Checked;
            Properties.Settings.Default.CB_예약주문_종가연동 = form.CB_예약주문_종가연동.Checked;
            Properties.Settings.Default.CB_예약주문_주문가고정 = form.CB_예약주문_주문가고정.Checked;

            Properties.Settings.Default.CBB_매매기간_주문시간_A = form.CBB_매매기간_주문시간_A.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_주문시간_B = form.CBB_매매기간_주문시간_B.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_주문시간_C = form.CBB_매매기간_주문시간_C.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_주문시간_D = form.CBB_매매기간_주문시간_D.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_주문시간_E = form.CBB_매매기간_주문시간_E.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_주문시간_F = form.CBB_매매기간_주문시간_F.SelectedIndex;
            Properties.Settings.Default.CB_예약주문_장전 = form.CB_예약주문_장전.Checked;
            Properties.Settings.Default.CB_예약주문_종가 = form.CB_예약주문_종가.Checked;

            try
            {
                double.TryParse(form.TB_매매기간_TS_down_A.Text, out double TB_매매기간_TS_down_A);
                double.TryParse(form.TB_매매기간_TS_down_B.Text, out double TB_매매기간_TS_down_B);
                double.TryParse(form.TB_매매기간_TS_down_C.Text, out double TB_매매기간_TS_down_C);
                double.TryParse(form.TB_매매기간_TS_down_D.Text, out double TB_매매기간_TS_down_D);
                double.TryParse(form.TB_매매기간_TS_down_E.Text, out double TB_매매기간_TS_down_E);
                double.TryParse(form.TB_매매기간_TS_down_F.Text, out double TB_매매기간_TS_down_F);

                Properties.Settings.Default.TB_매매기간_TS_down_A = TB_매매기간_TS_down_A;
                Properties.Settings.Default.TB_매매기간_TS_down_B = TB_매매기간_TS_down_B;
                Properties.Settings.Default.TB_매매기간_TS_down_C = TB_매매기간_TS_down_C;
                Properties.Settings.Default.TB_매매기간_TS_down_D = TB_매매기간_TS_down_D;
                Properties.Settings.Default.TB_매매기간_TS_down_E = TB_매매기간_TS_down_E;
                Properties.Settings.Default.TB_매매기간_TS_down_F = TB_매매기간_TS_down_F;
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 / 매매기간 TS 다운값 입력 오류 : " + e.Message); Form1.Error_Log("특수매매_저장 / 매매기간 TS 다운값 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_매매기간_TS_mma_A.Text, out int TB_매매기간_TS_mma_A);
                int.TryParse(form.TB_매매기간_TS_mma_B.Text, out int TB_매매기간_TS_mma_B);
                int.TryParse(form.TB_매매기간_TS_mma_C.Text, out int TB_매매기간_TS_mma_C);
                int.TryParse(form.TB_매매기간_TS_mma_D.Text, out int TB_매매기간_TS_mma_D);
                int.TryParse(form.TB_매매기간_TS_mma_E.Text, out int TB_매매기간_TS_mma_E);
                int.TryParse(form.TB_매매기간_TS_mma_F.Text, out int TB_매매기간_TS_mma_F);

                if (TB_매매기간_TS_mma_A == 0) TB_매매기간_TS_mma_A = 1;
                if (TB_매매기간_TS_mma_B == 0) TB_매매기간_TS_mma_B = 1;
                if (TB_매매기간_TS_mma_C == 0) TB_매매기간_TS_mma_C = 1;
                if (TB_매매기간_TS_mma_D == 0) TB_매매기간_TS_mma_D = 1;
                if (TB_매매기간_TS_mma_E == 0) TB_매매기간_TS_mma_E = 1;
                if (TB_매매기간_TS_mma_F == 0) TB_매매기간_TS_mma_F = 1;

                if (TB_매매기간_TS_mma_A > 60) TB_매매기간_TS_mma_A = 60;
                if (TB_매매기간_TS_mma_B > 60) TB_매매기간_TS_mma_B = 60;
                if (TB_매매기간_TS_mma_C > 60) TB_매매기간_TS_mma_C = 60;
                if (TB_매매기간_TS_mma_D > 60) TB_매매기간_TS_mma_D = 60;
                if (TB_매매기간_TS_mma_E > 60) TB_매매기간_TS_mma_E = 60;
                if (TB_매매기간_TS_mma_F > 60) TB_매매기간_TS_mma_F = 60;

                Properties.Settings.Default.TB_매매기간_TS_mma_A = TB_매매기간_TS_mma_A;
                Properties.Settings.Default.TB_매매기간_TS_mma_B = TB_매매기간_TS_mma_B;
                Properties.Settings.Default.TB_매매기간_TS_mma_C = TB_매매기간_TS_mma_C;
                Properties.Settings.Default.TB_매매기간_TS_mma_D = TB_매매기간_TS_mma_D;
                Properties.Settings.Default.TB_매매기간_TS_mma_E = TB_매매기간_TS_mma_E;
                Properties.Settings.Default.TB_매매기간_TS_mma_F = TB_매매기간_TS_mma_F;

                form.TB_매매기간_TS_mma_A.Text = TB_매매기간_TS_mma_A.ToString();
                form.TB_매매기간_TS_mma_B.Text = TB_매매기간_TS_mma_B.ToString();
                form.TB_매매기간_TS_mma_C.Text = TB_매매기간_TS_mma_C.ToString();
                form.TB_매매기간_TS_mma_D.Text = TB_매매기간_TS_mma_D.ToString();
                form.TB_매매기간_TS_mma_E.Text = TB_매매기간_TS_mma_E.ToString();
                form.TB_매매기간_TS_mma_F.Text = TB_매매기간_TS_mma_F.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 / 매매기간 TS 다운값 입력 오류 : " + e.Message); Form1.Error_Log("특수매매_저장 / 매매기간 TS 다운값 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_매매기간_TS_dma_A.Text, out int TB_매매기간_TS_dma_A);
                int.TryParse(form.TB_매매기간_TS_dma_B.Text, out int TB_매매기간_TS_dma_B);
                int.TryParse(form.TB_매매기간_TS_dma_C.Text, out int TB_매매기간_TS_dma_C);
                int.TryParse(form.TB_매매기간_TS_dma_D.Text, out int TB_매매기간_TS_dma_D);
                int.TryParse(form.TB_매매기간_TS_dma_E.Text, out int TB_매매기간_TS_dma_E);
                int.TryParse(form.TB_매매기간_TS_dma_F.Text, out int TB_매매기간_TS_dma_F);

                if (TB_매매기간_TS_dma_A == 0) TB_매매기간_TS_dma_A = 1;
                if (TB_매매기간_TS_dma_B == 0) TB_매매기간_TS_dma_B = 1;
                if (TB_매매기간_TS_dma_C == 0) TB_매매기간_TS_dma_C = 1;
                if (TB_매매기간_TS_dma_D == 0) TB_매매기간_TS_dma_D = 1;
                if (TB_매매기간_TS_dma_E == 0) TB_매매기간_TS_dma_E = 1;
                if (TB_매매기간_TS_dma_F == 0) TB_매매기간_TS_dma_F = 1;

                if (TB_매매기간_TS_dma_A > 60) TB_매매기간_TS_dma_A = 60;
                if (TB_매매기간_TS_dma_B > 60) TB_매매기간_TS_dma_B = 60;
                if (TB_매매기간_TS_dma_C > 60) TB_매매기간_TS_dma_C = 60;
                if (TB_매매기간_TS_dma_D > 60) TB_매매기간_TS_dma_D = 60;
                if (TB_매매기간_TS_dma_E > 60) TB_매매기간_TS_dma_E = 60;
                if (TB_매매기간_TS_dma_F > 60) TB_매매기간_TS_dma_F = 60;

                Properties.Settings.Default.TB_매매기간_TS_dma_A = TB_매매기간_TS_dma_A;
                Properties.Settings.Default.TB_매매기간_TS_dma_B = TB_매매기간_TS_dma_B;
                Properties.Settings.Default.TB_매매기간_TS_dma_C = TB_매매기간_TS_dma_C;
                Properties.Settings.Default.TB_매매기간_TS_dma_D = TB_매매기간_TS_dma_D;
                Properties.Settings.Default.TB_매매기간_TS_dma_E = TB_매매기간_TS_dma_E;
                Properties.Settings.Default.TB_매매기간_TS_dma_F = TB_매매기간_TS_dma_F;

                form.TB_매매기간_TS_dma_A.Text = TB_매매기간_TS_dma_A.ToString();
                form.TB_매매기간_TS_dma_B.Text = TB_매매기간_TS_dma_B.ToString();
                form.TB_매매기간_TS_dma_C.Text = TB_매매기간_TS_dma_C.ToString();
                form.TB_매매기간_TS_dma_D.Text = TB_매매기간_TS_dma_D.ToString();
                form.TB_매매기간_TS_dma_E.Text = TB_매매기간_TS_dma_E.ToString();
                form.TB_매매기간_TS_dma_F.Text = TB_매매기간_TS_dma_F.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("특수매매_저장 / 매매기간 TS 다운값 입력 오류 : " + e.Message); Form1.Error_Log("특수매매_저장 / 매매기간 TS 다운값 입력 오류 : " + e.Message);
            }

            if (form.CBB_매매기간_trading_A.SelectedIndex != 2) { form.CB_매매기간_TS_A.Checked = false; }
            if (form.CBB_매매기간_trading_B.SelectedIndex != 2) { form.CB_매매기간_TS_B.Checked = false; }
            if (form.CBB_매매기간_trading_C.SelectedIndex != 2) { form.CB_매매기간_TS_C.Checked = false; }
            if (form.CBB_매매기간_trading_D.SelectedIndex != 2) { form.CB_매매기간_TS_D.Checked = false; }
            if (form.CBB_매매기간_trading_E.SelectedIndex != 2) { form.CB_매매기간_TS_E.Checked = false; }
            if (form.CBB_매매기간_trading_F.SelectedIndex != 2) { form.CB_매매기간_TS_F.Checked = false; }

            Properties.Settings.Default.CB_매매기간_TS_A = form.CB_매매기간_TS_A.Checked;
            Properties.Settings.Default.CB_매매기간_TS_B = form.CB_매매기간_TS_B.Checked;
            Properties.Settings.Default.CB_매매기간_TS_C = form.CB_매매기간_TS_C.Checked;
            Properties.Settings.Default.CB_매매기간_TS_D = form.CB_매매기간_TS_D.Checked;
            Properties.Settings.Default.CB_매매기간_TS_E = form.CB_매매기간_TS_E.Checked;
            Properties.Settings.Default.CB_매매기간_TS_F = form.CB_매매기간_TS_F.Checked;

            Properties.Settings.Default.CBB_매매기간_TS_mma_A = form.CBB_매매기간_TS_mma_A.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_TS_mma_B = form.CBB_매매기간_TS_mma_B.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_TS_mma_C = form.CBB_매매기간_TS_mma_C.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_TS_mma_D = form.CBB_매매기간_TS_mma_D.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_TS_mma_E = form.CBB_매매기간_TS_mma_E.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_TS_mma_F = form.CBB_매매기간_TS_mma_F.SelectedIndex;

            Properties.Settings.Default.CBB_매매기간_TS_dma_A = form.CBB_매매기간_TS_dma_A.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_TS_dma_B = form.CBB_매매기간_TS_dma_B.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_TS_dma_C = form.CBB_매매기간_TS_dma_C.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_TS_dma_D = form.CBB_매매기간_TS_dma_D.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_TS_dma_E = form.CBB_매매기간_TS_dma_E.SelectedIndex;
            Properties.Settings.Default.CBB_매매기간_TS_dma_F = form.CBB_매매기간_TS_dma_F.SelectedIndex;


            MA.Get_MA();
        }

        private void Form_Special_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Properties.Settings.Default.CB_레이아웃고정_특수매매 = CB_레이아웃고정_특수매매.Checked;
                Properties.Settings.Default.CB_특수매매_시작위치저장 = form.CB_특수매매_시작위치저장.Checked;
                if (Properties.Settings.Default.CB_특수매매_시작위치저장) Properties.Settings.Default.WindowLocation = form.Location; LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
                Form1.form1.CB_특수매매.Checked = false;
                Form1.FormSpecial_Open = false;

                e.Cancel = true;
                Hide();
            }
        }

        private void BT_특수매매저장_Click(object sender, EventArgs e)
        {
            Form1.form1.Select();
            Form1.MBC_sender = (sender as Button).Name;
            Form1.중요메세지("특수매매", "특수매매 설정을 저장 하시겠습니까?");
        }

        private void CB_레이아웃고정_특수매매_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CB_레이아웃고정_특수매매 = CB_레이아웃고정_특수매매.Checked;

            if (!CB_레이아웃고정_특수매매.Checked) LayoutChange.CBB_layout_SelectedIndex(-1);
            else LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
        }

        private void CB_매수매도_Changed(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            CheckBox CB = (sender as CheckBox);
            if (CB.Checked)
            {
                CB.Text = "매수";
                CB.ForeColor = Color.Red;
            }
            else
            {
                CB.Text = "매도";
                CB.ForeColor = Color.Blue;
            }
        }

        private void CB_IN_CheckedChanged(object sender, EventArgs e)
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
        }

        private void CB_OUT_checkedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            CheckBox CB = (sender as CheckBox);
            if (CB.Checked)
            {
                CB.ForeColor = Color.Blue;
            }
            else
            {
                CB.ForeColor = Color.Black;
            }
        }

        private void CB_group_In_updown_CheckedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            CheckBox CB = (sender as CheckBox);
            if (CB.Checked)
            {
                CB.Text = "이상";
                CB.ForeColor = Color.Red;
            }
            else
            {
                CB.Text = "이하";
                CB.ForeColor = Color.Blue;
            }
        }

        private void CB_group_OUT_updown_CheckedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            CheckBox CB = (sender as CheckBox);
            if (CB.Checked)
            {
                CB.Text = "이상 해제";
                CB.ForeColor = Color.Red;
            }
            else
            {
                CB.Text = "이하 해제";
                CB.ForeColor = Color.Blue;
            }
        }

        private void CBB_예약종류_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Form1.FormSpecial_Open) Order_Reserve.CBB_예약종류_SelectedIndexChanged(sender);
        }

        private void BT_Click_예약주문추가_(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Form1.비프음("체크");
            Order_Reserve.BT_Click_예약주문추가_(sender);
        }

        private void BT_Click_예약삭제(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Form1.비프음("체크");
            Order_Reserve.BT_Click_예약삭제(sender);
        }

        private void TB_주문비_예약_TextChanged(object sender, EventArgs e)
        {
            Order_Reserve.TB_주문비_예약_TextChanged();
        }

        private void 수동주문틱계산Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Order_Reserve.수동주문틱계산(sender);
            }
        }

        private void 예약주문호가계산Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Order_Reserve.예약주문호가계산(sender);
            }
        }

        private void 종목선택Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Order_Reserve.종목선택(sender);
            }
        }

        private void 주문비계산Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Order_Reserve.주문비계산(sender);
            }
        }

        private void 주문가계산Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Order_Reserve.주문가계산(sender);
            }
        }

        private void RB_매수_CheckedChanged(object sender, EventArgs e)
        {
            if (Form1.FormSpecial_Open)
                Order_Reserve.RB_매수_CheckedChanged(sender);
        }

        private void combo_수동주문_choice_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.로딩완료) Form1.비프음("체크");
            Order_Reserve.combo_수동주문_choice_DropDownClosed(sender);
        }

        private void BT_수동주문_실행_Click(object sender, EventArgs e) // 잔고 수동매매 
        {
            this.ActiveControl = null;
            Order_Reserve.BT_수동주문_실행_Click(sender);
        }

        private void LB_예약리스트_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Order_Reserve.LB_예약리스트_Click(sender);
        }


        private void CBB_group_In_out_choice_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.FormSpecial_Open)
            {
                Form1.비프음("체크");

                if (sender.Equals(CBB_group_In_choice_A) || sender.Equals(CB_Group_In_trading_A))
                {
                    if (CB_Group_In_trading_A.Checked && CBB_group_In_choice_A.SelectedIndex > 3)
                    {
                        CBB_group_In_choice_A.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_group_In_choice_B) || sender.Equals(CB_Group_In_trading_B))
                {
                    if (CB_Group_In_trading_B.Checked && CBB_group_In_choice_B.SelectedIndex > 3)
                    {
                        CBB_group_In_choice_B.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_group_In_choice_C) || sender.Equals(CB_Group_In_trading_C))
                {
                    if (CB_Group_In_trading_C.Checked && CBB_group_In_choice_C.SelectedIndex > 3)
                    {
                        CBB_group_In_choice_C.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_group_In_choice_D) || sender.Equals(CB_Group_In_trading_D))
                {
                    if (CB_Group_In_trading_D.Checked && CBB_group_In_choice_D.SelectedIndex > 3)
                    {
                        CBB_group_In_choice_D.SelectedIndex = 2;
                        매도비중알림();
                    }
                }

                if (sender.Equals(CBB_group_out_choice_A) || sender.Equals(CB_Group_Out_trading_A))
                {
                    if (CB_Group_Out_trading_A.Checked && CBB_group_out_choice_A.SelectedIndex > 3)
                    {
                        CBB_group_out_choice_A.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_group_out_choice_B) || sender.Equals(CB_Group_Out_trading_B))
                {
                    if (CB_Group_Out_trading_B.Checked && CBB_group_out_choice_B.SelectedIndex > 3)
                    {
                        CBB_group_out_choice_B.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_group_out_choice_C) || sender.Equals(CB_Group_Out_trading_C))
                {
                    if (CB_Group_Out_trading_C.Checked && CBB_group_out_choice_C.SelectedIndex > 3)
                    {
                        CBB_group_out_choice_C.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_group_out_choice_D) || sender.Equals(CB_Group_Out_trading_D))
                {
                    if (CB_Group_Out_trading_D.Checked && CBB_group_out_choice_D.SelectedIndex > 3)
                    {
                        CBB_group_out_choice_D.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
            }
        }

        private void CBB_day_choice_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.FormSpecial_Open)
            {
                Form1.비프음("체크");

                if (sender.Equals(CBB_매매기간_choice_A) || sender.Equals(CBB_매매기간_trading_A))
                {
                    if (CBB_매매기간_trading_A.SelectedIndex == 0) Properties.Settings.Default.CBB_매매기간_trading_A = 0;
                    if (CBB_매매기간_trading_A.SelectedIndex == 1 && CBB_매매기간_choice_A.SelectedIndex > 3)
                    {
                        CBB_매매기간_choice_A.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_매매기간_choice_B) || sender.Equals(CBB_매매기간_trading_B))
                {
                    if (CBB_매매기간_trading_B.SelectedIndex == 0) Properties.Settings.Default.CBB_매매기간_trading_B = 0;
                    if (CBB_매매기간_trading_B.SelectedIndex == 1 && CBB_매매기간_choice_B.SelectedIndex > 3)
                    {
                        CBB_매매기간_choice_B.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_매매기간_choice_C) || sender.Equals(CBB_매매기간_trading_C))
                {
                    if (CBB_매매기간_trading_C.SelectedIndex == 0) Properties.Settings.Default.CBB_매매기간_trading_C = 0;
                    if (CBB_매매기간_trading_C.SelectedIndex == 1 && CBB_매매기간_choice_C.SelectedIndex > 3)
                    {
                        CBB_매매기간_choice_C.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_매매기간_choice_D) || sender.Equals(CBB_매매기간_trading_D))
                {
                    if (CBB_매매기간_trading_D.SelectedIndex == 0) Properties.Settings.Default.CBB_매매기간_trading_D = 0;
                    if (CBB_매매기간_trading_D.SelectedIndex == 1 && CBB_매매기간_choice_D.SelectedIndex > 3)
                    {
                        CBB_매매기간_choice_D.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_매매기간_choice_E) || sender.Equals(CBB_매매기간_trading_E))
                {
                    if (CBB_매매기간_trading_E.SelectedIndex == 0) Properties.Settings.Default.CBB_매매기간_trading_E = 0;
                    if (CBB_매매기간_trading_E.SelectedIndex == 1 && CBB_매매기간_choice_E.SelectedIndex > 3)
                    {
                        CBB_매매기간_choice_E.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_매매기간_choice_F) || sender.Equals(CBB_매매기간_trading_F))
                {
                    if (CBB_매매기간_trading_F.SelectedIndex == 0) Properties.Settings.Default.CBB_매매기간_trading_F = 0;
                    if (CBB_매매기간_trading_F.SelectedIndex == 1 && CBB_매매기간_choice_F.SelectedIndex > 3)
                    {
                        CBB_매매기간_choice_F.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
            }
        }

        private void CBB_jumun_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrint.CBB_jumun_SelectedIndex(sender);
        }


        public static void 매도비중알림()
        {
            Form1.AutoClosingAlram("'매수' 는 [ 만원/평균단가, 만원/기준가, 기준금/평균단가, 기준금/기준가 ] 를 사용할수 없습니다. [ 기준금 ] 으로 변경됩니다. 설정을 확인하기 바랍니다.", "비중알림", 10, "에러");
        }


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////               매매일기준 매매            ////////////////

        private void CB_장전_CheckedChanged(object sender, EventArgs e)
        {
            if (Form1.FormSpecial_Open)
            {
                Form1.form1.체크박스_비프(sender);

                if (sender.Equals(CB_예약주문_장전))
                {
                    if (!CB_예약주문_장전.Checked)
                    {
                        Properties.Settings.Default.CB_예약주문_장전 = false;
                        Form1.예약주문_장전 = true;
                    }
                }

                if (sender.Equals(CB_예약주문_종가))
                {
                    if (!CB_예약주문_종가.Checked)
                    {
                        Properties.Settings.Default.CB_예약주문_종가 = false;
                        Form1.예약주문_종가 = true;
                    }
                }

                if (sender.Equals(CB_매매기간_오전))
                {
                    if (!CB_매매기간_오전.Checked)
                    {
                        Properties.Settings.Default.CB_매매기간_오전 = false;
                        Form1.매매기간_오전 = true;
                    }
                }

                if (sender.Equals(CB_매매기간_오후))
                {
                    if (!CB_매매기간_오후.Checked)
                    {
                        Properties.Settings.Default.CB_매매기간_오후 = false;
                        Form1.매매기간_오후 = true;
                    }
                }
            }
        }

        private void 매매일_기준_콤보박스(object sender, EventArgs eventArgs)
        {
            if (Form1.FormSpecial_Open)
            {
                if (sender.Equals(CBB_매매기간_기준_A))
                    if (CBB_매매기간_기준_A.SelectedIndex.Equals(2) || CBB_매매기간_기준_A.SelectedIndex.Equals(3))
                        if (TB_매매기간_기준_A.Text.StartsWith("-"))
                            TB_매매기간_기준_A.Text = TB_매매기간_기준_A.Text.Substring(1);

                if (sender.Equals(CBB_매매기간_기준_B))
                    if (CBB_매매기간_기준_B.SelectedIndex.Equals(2) || CBB_매매기간_기준_B.SelectedIndex.Equals(3))
                        if (TB_매매기간_기준_B.Text.StartsWith("-"))
                            TB_매매기간_기준_B.Text = TB_매매기간_기준_B.Text.Substring(1);

                if (sender.Equals(CBB_매매기간_기준_C))
                    if (CBB_매매기간_기준_C.SelectedIndex.Equals(2) || CBB_매매기간_기준_C.SelectedIndex.Equals(3))
                        if (TB_매매기간_기준_C.Text.StartsWith("-"))
                            TB_매매기간_기준_C.Text = TB_매매기간_기준_C.Text.Substring(1);

                if (sender.Equals(CBB_매매기간_기준_D))
                    if (CBB_매매기간_기준_D.SelectedIndex.Equals(2) || CBB_매매기간_기준_D.SelectedIndex.Equals(3))
                        if (TB_매매기간_기준_D.Text.StartsWith("-"))
                            TB_매매기간_기준_D.Text = TB_매매기간_기준_D.Text.Substring(1);

                if (sender.Equals(CBB_매매기간_기준_E))
                    if (CBB_매매기간_기준_E.SelectedIndex.Equals(2) || CBB_매매기간_기준_E.SelectedIndex.Equals(3))
                        if (TB_매매기간_기준_E.Text.StartsWith("-"))
                            TB_매매기간_기준_E.Text = TB_매매기간_기준_E.Text.Substring(1);

                if (sender.Equals(CBB_매매기간_기준_F))
                    if (CBB_매매기간_기준_F.SelectedIndex.Equals(2) || CBB_매매기간_기준_F.SelectedIndex.Equals(3))
                        if (TB_매매기간_기준_F.Text.StartsWith("-"))
                            TB_매매기간_기준_F.Text = TB_매매기간_기준_F.Text.Substring(1);

            }
        }

        /////////////////////           매매일기준 매매            ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

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
            }
        }

        private void Once_CheckedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            CheckBox CB = (sender as CheckBox);
            if (CB.Checked)
                CB.ForeColor = Color.Red;
            else
                CB.ForeColor = Color.Black;
        }

        private void 버튼음_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.FormSpecial_Open) Form1.비프음("체크");
        }

        private void TextBox_소수자리제한(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_소수자리제한(sender);
        }

        private void TextBox_빨파검(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_빨파검(sender);
        }

        public void TextBox_빨파검_소수2자리제한(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_빨파검_소수2자리제한(sender);
        }
        private void TextBox_양수소수자리제한(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_양수소수자리제한(sender);
        }

        private void _양수음수소수_키프레스(object sender, KeyPressEventArgs e) // 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, true); // textbox 에 양수, 음수 , 소수  숫자만 입력 받을수 있음 
        }

        private void _양수소수_키프레스(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, false); // textbox 에 양수 , 소수 숫자만 입력 받을수 있음
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

        bool 소리 = false;
        private void CB_매매기간_TS_set_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                CB_매매기간_TS_A.Checked = Properties.Settings.Default.CB_매매기간_TS_A;
                CB_매매기간_TS_B.Checked = Properties.Settings.Default.CB_매매기간_TS_B;
                CB_매매기간_TS_C.Checked = Properties.Settings.Default.CB_매매기간_TS_C;
                CB_매매기간_TS_D.Checked = Properties.Settings.Default.CB_매매기간_TS_D;
                CB_매매기간_TS_E.Checked = Properties.Settings.Default.CB_매매기간_TS_E;
                CB_매매기간_TS_F.Checked = Properties.Settings.Default.CB_매매기간_TS_F;

                TB_매매기간_TS_down_A.Text = Properties.Settings.Default.TB_매매기간_TS_down_A.ToString();
                TB_매매기간_TS_down_B.Text = Properties.Settings.Default.TB_매매기간_TS_down_B.ToString();
                TB_매매기간_TS_down_C.Text = Properties.Settings.Default.TB_매매기간_TS_down_C.ToString();
                TB_매매기간_TS_down_D.Text = Properties.Settings.Default.TB_매매기간_TS_down_D.ToString();
                TB_매매기간_TS_down_E.Text = Properties.Settings.Default.TB_매매기간_TS_down_E.ToString();
                TB_매매기간_TS_down_F.Text = Properties.Settings.Default.TB_매매기간_TS_down_F.ToString();

                TB_매매기간_TS_mma_A.Text = Properties.Settings.Default.TB_매매기간_TS_mma_A.ToString();
                TB_매매기간_TS_mma_B.Text = Properties.Settings.Default.TB_매매기간_TS_mma_B.ToString();
                TB_매매기간_TS_mma_C.Text = Properties.Settings.Default.TB_매매기간_TS_mma_C.ToString();
                TB_매매기간_TS_mma_D.Text = Properties.Settings.Default.TB_매매기간_TS_mma_D.ToString();
                TB_매매기간_TS_mma_E.Text = Properties.Settings.Default.TB_매매기간_TS_mma_E.ToString();
                TB_매매기간_TS_mma_F.Text = Properties.Settings.Default.TB_매매기간_TS_mma_F.ToString();

                CBB_매매기간_TS_mma_A.SelectedIndex = Properties.Settings.Default.CBB_매매기간_TS_mma_A;
                CBB_매매기간_TS_mma_B.SelectedIndex = Properties.Settings.Default.CBB_매매기간_TS_mma_B;
                CBB_매매기간_TS_mma_C.SelectedIndex = Properties.Settings.Default.CBB_매매기간_TS_mma_C;
                CBB_매매기간_TS_mma_D.SelectedIndex = Properties.Settings.Default.CBB_매매기간_TS_mma_D;
                CBB_매매기간_TS_mma_E.SelectedIndex = Properties.Settings.Default.CBB_매매기간_TS_mma_E;
                CBB_매매기간_TS_mma_F.SelectedIndex = Properties.Settings.Default.CBB_매매기간_TS_mma_F;

                TB_매매기간_TS_dma_A.Text = Properties.Settings.Default.TB_매매기간_TS_dma_A.ToString();
                TB_매매기간_TS_dma_B.Text = Properties.Settings.Default.TB_매매기간_TS_dma_B.ToString();
                TB_매매기간_TS_dma_C.Text = Properties.Settings.Default.TB_매매기간_TS_dma_C.ToString();
                TB_매매기간_TS_dma_D.Text = Properties.Settings.Default.TB_매매기간_TS_dma_D.ToString();
                TB_매매기간_TS_dma_E.Text = Properties.Settings.Default.TB_매매기간_TS_dma_E.ToString();
                TB_매매기간_TS_dma_F.Text = Properties.Settings.Default.TB_매매기간_TS_dma_F.ToString();

                CBB_매매기간_TS_dma_A.SelectedIndex = Properties.Settings.Default.CBB_매매기간_TS_dma_A;
                CBB_매매기간_TS_dma_B.SelectedIndex = Properties.Settings.Default.CBB_매매기간_TS_dma_B;
                CBB_매매기간_TS_dma_C.SelectedIndex = Properties.Settings.Default.CBB_매매기간_TS_dma_C;
                CBB_매매기간_TS_dma_D.SelectedIndex = Properties.Settings.Default.CBB_매매기간_TS_dma_D;
                CBB_매매기간_TS_dma_E.SelectedIndex = Properties.Settings.Default.CBB_매매기간_TS_dma_E;
                CBB_매매기간_TS_dma_F.SelectedIndex = Properties.Settings.Default.CBB_매매기간_TS_dma_F;

                panel_매매기간_TS.BringToFront();
                panel_매매기간_TS.Show();

                소리 = true;
            }
            else
            {
                panel_매매기간_TS.Hide();

                소리 = false;
            }

            if (Form1.FormSpecial_Open) Form1.form1.체크박스_비프(sender);
        }

        private void CB_매매기간_TS_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);

            string text = CB.Text.Split(' ')[0];
            if (CB.Checked)
            {
                CB.Text = text + " TS run";
                CB.ForeColor = Color.Red;
            }
            else
            {
                CB.Text = text + " TS off";
                CB.ForeColor = Color.Black;
            }
            if (소리) Form1.form1.체크박스_비프(sender);
        }
    }
}
