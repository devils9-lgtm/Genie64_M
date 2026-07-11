using Microsoft.Office.Core;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using 지니64.RESTAPI;

namespace 지니64
{
    public partial class Form_Special : Form
    {
        public static Form_Special form;
        public Form_Special()
        {
            form = this;
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer |
                           ControlStyles.UserPaint |
                           ControlStyles.AllPaintingInWmPaint |
                           ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
        }

        public void Form_Special_Load()
        {
            if (Form1.내아이디)
            {
                panel_자금관리.Show();
            }
            else
            {
                panel_자금관리.Hide();
                GenieConfig.CB_자금관리 = false;
            }

            typeof(ListBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, LB_예약리스트, new object[] { true });

            Form1.음소거 = true;

            CB_자금관리.Checked = GenieConfig.CB_자금관리;
            label_관리종목코드.Text = GenieConfig.label_관리종목코드;
            TB_자금관리_유지현금.Text = GenieConfig.TB_자금관리_유지현금.ToString();

            RB_매수.Checked = GenieConfig.수동주문_RB_매수;
            RB_매도.Checked = !GenieConfig.수동주문_RB_매수;
            CB_수동신용.Checked = GenieConfig.CB_신용_주문사용;

            CB_매매기간_TS.Checked = false;
            panel_매매기간_TS.Hide();

            combo_수동주문_choice.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_수동주문_choice);

            MTB_수동주문_cansel_time.Text = GenieConfig.MTB_수동주문_cansel_time.ToString();
            MTB_수동주문_repeat.Text = GenieConfig.MTB_수동주문_repeat.ToString();
            TB_수동주문_ratio.Text = GenieConfig.TB_수동주문_ratio.ToString();

            CBB_group_In_jumun_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_In_jumun_A);
            MTB_group_In_CanselTime_A.Text = GenieConfig.MTB_group_In_CanselTime_A.ToString();
            MTB_group_In_repeat_A.Text = GenieConfig.MTB_group_In_repeat_A.ToString();
            TB_group_In_ratio_A.Text = GenieConfig.TB_group_In_ratio_A.ToString();
            CBB_group_In_choice_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_In_choice_A);
            CBB_group_out_jumun_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_out_jumun_A);
            MTB_group_out_CanselTime_A.Text = GenieConfig.MTB_group_out_CanselTime_A.ToString();
            MTB_group_out_repeat_A.Text = GenieConfig.MTB_group_out_repeat_A.ToString();
            TB_group_out_ratio_A.Text = GenieConfig.TB_group_out_ratio_A.ToString();
            CBB_group_out_choice_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_out_choice_A);
            TB_group_Out_value_A.Text = GenieConfig.TB_group_Out_value_A.ToString();

            CBB_group_In_jumun_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_In_jumun_B);
            MTB_group_In_CanselTime_B.Text = GenieConfig.MTB_group_In_CanselTime_B.ToString();
            MTB_group_In_repeat_B.Text = GenieConfig.MTB_group_In_repeat_B.ToString();
            TB_group_In_ratio_B.Text = GenieConfig.TB_group_In_ratio_B.ToString();
            CBB_group_In_choice_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_In_choice_B);
            CBB_group_out_jumun_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_out_jumun_B);
            MTB_group_out_CanselTime_B.Text = GenieConfig.MTB_group_out_CanselTime_B.ToString();
            MTB_group_out_repeat_B.Text = GenieConfig.MTB_group_out_repeat_B.ToString();
            TB_group_out_ratio_B.Text = GenieConfig.TB_group_out_ratio_B.ToString();
            CBB_group_out_choice_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_out_choice_B);
            TB_group_Out_value_B.Text = GenieConfig.TB_group_Out_value_B.ToString();

            CBB_group_In_jumun_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_In_jumun_C);
            MTB_group_In_CanselTime_C.Text = GenieConfig.MTB_group_In_CanselTime_C.ToString();
            MTB_group_In_repeat_C.Text = GenieConfig.MTB_group_In_repeat_C.ToString();
            TB_group_In_ratio_C.Text = GenieConfig.TB_group_In_ratio_C.ToString();
            CBB_group_In_choice_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_In_choice_C);
            CBB_group_out_jumun_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_out_jumun_C);
            MTB_group_out_CanselTime_C.Text = GenieConfig.MTB_group_out_CanselTime_C.ToString();
            MTB_group_out_repeat_C.Text = GenieConfig.MTB_group_out_repeat_C.ToString();
            TB_group_out_ratio_C.Text = GenieConfig.TB_group_out_ratio_C.ToString();
            CBB_group_out_choice_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_out_choice_C);
            TB_group_Out_value_C.Text = GenieConfig.TB_group_Out_value_C.ToString();

            CBB_group_In_jumun_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_In_jumun_D);
            MTB_group_In_CanselTime_D.Text = GenieConfig.MTB_group_In_CanselTime_D.ToString();
            MTB_group_In_repeat_D.Text = GenieConfig.MTB_group_In_repeat_D.ToString();
            TB_group_In_ratio_D.Text = GenieConfig.TB_group_In_ratio_D.ToString();
            CBB_group_In_choice_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_In_choice_D);
            CBB_group_out_jumun_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_out_jumun_D);
            MTB_group_out_CanselTime_D.Text = GenieConfig.MTB_group_out_CanselTime_D.ToString();
            MTB_group_out_repeat_D.Text = GenieConfig.MTB_group_out_repeat_D.ToString();
            TB_group_out_ratio_D.Text = GenieConfig.TB_group_out_ratio_D.ToString();
            CBB_group_out_choice_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_out_choice_D);
            TB_group_Out_value_D.Text = GenieConfig.TB_group_Out_value_D.ToString();
            TB_group_out_ratio_D.Text = GenieConfig.TB_group_out_ratio_D.ToString();

            CBB_In_group_A.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_In_group_A);
            CBB_In_group_B.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_In_group_B);
            CBB_In_group_C.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_In_group_C);
            CBB_In_group_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_In_group_D);

            combo_신규그룹_A.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_신규그룹_A);
            combo_신규그룹_B.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_신규그룹_B);
            combo_신규그룹_C.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_신규그룹_C);

            CB_group_기준금.Checked = GenieConfig.CB_group_기준금;

            CBB_I_group_choice_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_In_group_choice_A);
            CBB_I_group_choice_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_In_group_choice_B);
            CBB_I_group_choice_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_In_group_choice_C);
            CBB_I_group_choice_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_In_group_choice_D);

            CBB_O_group_choice_A.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_Out_group_choice_A);
            CBB_O_group_choice_B.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_Out_group_choice_B);
            CBB_O_group_choice_C.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_Out_group_choice_C);
            CBB_O_group_choice_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Out_group_choice_D);

            CBB_Out_group_A.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_Out_group_A);
            CBB_Out_group_B.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_Out_group_B);
            CBB_Out_group_C.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_Out_group_C);
            CBB_Out_group_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Out_group_D);

            TB_In_group_ratio_A.Text = GenieConfig.TB_In_group_ratio_A.ToString();
            TB_In_group_ratio_B.Text = GenieConfig.TB_In_group_ratio_B.ToString();
            TB_In_group_ratio_C.Text = GenieConfig.TB_In_group_ratio_C.ToString();
            TB_In_group_ratio_D.Text = GenieConfig.TB_In_group_ratio_D.ToString();

            TB_Out_group_ratio_A.Text = GenieConfig.TB_Out_group_ratio_A.ToString();
            TB_Out_group_ratio_B.Text = GenieConfig.TB_Out_group_ratio_B.ToString();
            TB_Out_group_ratio_C.Text = GenieConfig.TB_Out_group_ratio_C.ToString();
            TB_Out_group_ratio_D.Text = GenieConfig.TB_Out_group_ratio_D.ToString();

            CB_Group_In_work_A.Checked = GenieConfig.CB_Group_In_work_A;
            CB_Group_In_work_B.Checked = GenieConfig.CB_Group_In_work_B;
            CB_Group_In_work_C.Checked = GenieConfig.CB_Group_In_work_C;
            CB_Group_In_work_D.Checked = GenieConfig.CB_Group_In_work_D;

            CB_Group_Out_work_A.Checked = GenieConfig.CB_Group_Out_work_A;
            CB_Group_Out_work_B.Checked = GenieConfig.CB_Group_Out_work_B;
            CB_Group_Out_work_C.Checked = GenieConfig.CB_Group_Out_work_C;
            CB_Group_Out_work_D.Checked = GenieConfig.CB_Group_Out_work_D;

            CB_Group_In_trading_A.Checked = GenieConfig.CB_Group_In_trading_A;
            CB_Group_In_trading_B.Checked = GenieConfig.CB_Group_In_trading_B;
            CB_Group_In_trading_C.Checked = GenieConfig.CB_Group_In_trading_C;
            CB_Group_In_trading_D.Checked = GenieConfig.CB_Group_In_trading_D;

            CB_Group_Out_trading_A.Checked = GenieConfig.CB_Group_Out_trading_A;
            CB_Group_Out_trading_B.Checked = GenieConfig.CB_Group_Out_trading_B;
            CB_Group_Out_trading_C.Checked = GenieConfig.CB_Group_Out_trading_C;
            CB_Group_Out_trading_D.Checked = GenieConfig.CB_Group_Out_trading_D;

            CB_group_In_updown_A.Checked = GenieConfig.CB_group_In_updown_A;
            CB_group_In_updown_B.Checked = GenieConfig.CB_group_In_updown_B;
            CB_group_In_updown_C.Checked = GenieConfig.CB_group_In_updown_C;
            CB_group_In_updown_D.Checked = GenieConfig.CB_group_In_updown_D;

            CB_group_Out_updown_A.Checked = GenieConfig.CB_group_Out_updown_A;
            CB_group_Out_updown_B.Checked = GenieConfig.CB_group_Out_updown_B;
            CB_group_Out_updown_C.Checked = GenieConfig.CB_group_Out_updown_C;
            CB_group_Out_updown_D.Checked = GenieConfig.CB_group_Out_updown_D;

            CBB_group_In_won_A.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_group_In_won_A);
            CBB_group_In_won_B.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_group_In_won_B);
            CBB_group_In_won_C.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_group_In_won_C);
            CBB_group_In_won_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_group_In_won_D);

            TB_group_In_won_A.Text = GenieConfig.TB_group_In_won_A.ToString();
            TB_group_In_won_B.Text = GenieConfig.TB_group_In_won_B.ToString();
            TB_group_In_won_C.Text = GenieConfig.TB_group_In_won_C.ToString();
            TB_group_In_won_D.Text = GenieConfig.TB_group_In_won_D.ToString();

            TB_group_In_value_A.Text = GenieConfig.TB_group_In_value_A.ToString();
            TB_group_In_value_B.Text = GenieConfig.TB_group_In_value_B.ToString();
            TB_group_In_value_C.Text = GenieConfig.TB_group_In_value_C.ToString();
            TB_group_In_value_D.Text = GenieConfig.TB_group_In_value_D.ToString();

            TB_TAX.Text = (Form1.TAX * 100).ToString();
            TB_sil_commission.Text = GenieConfig.TB_sil_commission.ToString();
            TB_mo_commission.Text = GenieConfig.TB_mo_commission.ToString();

            TB_예약주문_장전매수호가.Text = GenieConfig.TB_예약주문_장전매수호가.ToString();
            TB_예약주문_장전매수비중.Text = GenieConfig.TB_예약주문_장전매수비중.ToString();
            CBB_예약주문_장전매수선택.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_예약주문_장전매수선택);
            TB_예약주문_장전매도호가.Text = GenieConfig.TB_예약주문_장전매도호가.ToString();
            TB_예약주문_장전매도비중.Text = GenieConfig.TB_예약주문_장전매도비중.ToString();
            CBB_예약주문_장전매도선택.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_예약주문_장전매도선택);
            TB_예약주문_종가매수호가.Text = GenieConfig.TB_예약주문_종가매수호가.ToString();
            TB_예약주문_종가매수비중.Text = GenieConfig.TB_예약주문_종가매수비중.ToString();
            CBB_예약주문_종가매수선택.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_예약주문_종가매수선택);
            TB_예약주문_종가매도호가.Text = GenieConfig.TB_예약주문_종가매도호가.ToString();
            TB_예약주문_종가매도비중.Text = GenieConfig.TB_예약주문_종가매도비중.ToString();
            CBB_예약주문_종가매도선택.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_예약주문_종가매도선택);

            CB_예약주문_장전체결삭제.Checked = GenieConfig.CB_예약주문_장전체결삭제;
            CB_예약주문_장전전량매도삭제.Checked = GenieConfig.CB_예약주문_장전전량매도삭제;

            CB_예약주문_종가체결삭제.Checked = GenieConfig.CB_예약주문_종가체결삭제;
            CB_예약주문_종가전량매도삭제.Checked = GenieConfig.CB_예약주문_종가전량매도삭제;

            CBB_예약주문_예약종류.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_예약주문_예약종류);
            TB_예약주문_주문비.Text = GenieConfig.TB_예약주문_주문비.ToString();

            TB_수동주문_주문비.Text = GenieConfig.TB_수동주문_주문비.ToString();

            CB_매매기간_기준금.Checked = GenieConfig.CB_매매기간_기준금;

            CBB_매매기간_주문시간_A.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_주문시간_A);
            CBB_매매기간_주문시간_B.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_주문시간_B);
            CBB_매매기간_주문시간_C.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_주문시간_C);
            CBB_매매기간_주문시간_D.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_주문시간_D);
            CBB_매매기간_주문시간_E.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_주문시간_E);
            CBB_매매기간_주문시간_F.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_매매기간_주문시간_F);

            CBB_매매기간_choice_A.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_choice_A);
            CBB_매매기간_choice_B.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_choice_B);
            CBB_매매기간_choice_C.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_choice_C);
            CBB_매매기간_choice_D.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_choice_D);
            CBB_매매기간_choice_E.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_choice_E);
            CBB_매매기간_choice_F.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_매매기간_choice_F);

            CBB_매매기간_Jumun_A.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_Jumun_A);
            CBB_매매기간_Jumun_B.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_Jumun_B);
            CBB_매매기간_Jumun_C.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_Jumun_C);
            CBB_매매기간_Jumun_D.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_Jumun_D);
            CBB_매매기간_Jumun_E.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_Jumun_E);
            CBB_매매기간_Jumun_F.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_매매기간_Jumun_F);

            CBB_매매기간_trading_A.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_매매기간_trading_A);
            CBB_매매기간_trading_B.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_매매기간_trading_B);
            CBB_매매기간_trading_C.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_매매기간_trading_C);
            CBB_매매기간_trading_D.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_매매기간_trading_D);
            CBB_매매기간_trading_E.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_매매기간_trading_E);
            CBB_매매기간_trading_F.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_매매기간_trading_F);

            CBB_매매기간_기준_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_매매기간_기준_A);
            CBB_매매기간_기준_B.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_기준_B);
            CBB_매매기간_기준_C.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_기준_C);
            CBB_매매기간_기준_D.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_기준_D);
            CBB_매매기간_기준_E.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_매매기간_기준_E);
            CBB_매매기간_기준_F.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_매매기간_기준_F);

            MTB_매매기간_기간_A.Text = GenieConfig.MTB_매매기간_기간_A.ToString();
            MTB_매매기간_기간_B.Text = GenieConfig.MTB_매매기간_기간_B.ToString();
            MTB_매매기간_기간_C.Text = GenieConfig.MTB_매매기간_기간_C.ToString();
            MTB_매매기간_기간_D.Text = GenieConfig.MTB_매매기간_기간_D.ToString();
            MTB_매매기간_기간_E.Text = GenieConfig.MTB_매매기간_기간_E.ToString();
            MTB_매매기간_기간_F.Text = GenieConfig.MTB_매매기간_기간_F.ToString();

            TB_매매기간_ratio_A.Text = GenieConfig.TB_매매기간_ratio_A.ToString();
            TB_매매기간_ratio_B.Text = GenieConfig.TB_매매기간_ratio_B.ToString();
            TB_매매기간_ratio_C.Text = GenieConfig.TB_매매기간_ratio_C.ToString();
            TB_매매기간_ratio_D.Text = GenieConfig.TB_매매기간_ratio_D.ToString();
            TB_매매기간_ratio_E.Text = GenieConfig.TB_매매기간_ratio_E.ToString();
            TB_매매기간_ratio_F.Text = GenieConfig.TB_매매기간_ratio_F.ToString();

            TB_매매기간_기준_A.Text = GenieConfig.TB_매매기간_기준_A.ToString();
            TB_매매기간_기준_B.Text = GenieConfig.TB_매매기간_기준_B.ToString();
            TB_매매기간_기준_C.Text = GenieConfig.TB_매매기간_기준_C.ToString();
            TB_매매기간_기준_D.Text = GenieConfig.TB_매매기간_기준_D.ToString();
            TB_매매기간_기준_E.Text = GenieConfig.TB_매매기간_기준_E.ToString();
            TB_매매기간_기준_F.Text = GenieConfig.TB_매매기간_기준_F.ToString();

            TB_매매기간_value_A.Text = GenieConfig.TB_매매기간_value_A.ToString();
            TB_매매기간_value_B.Text = GenieConfig.TB_매매기간_value_B.ToString();
            TB_매매기간_value_C.Text = GenieConfig.TB_매매기간_value_C.ToString();
            TB_매매기간_value_D.Text = GenieConfig.TB_매매기간_value_D.ToString();
            TB_매매기간_value_E.Text = GenieConfig.TB_매매기간_value_E.ToString();
            TB_매매기간_value_F.Text = GenieConfig.TB_매매기간_value_F.ToString();

            CB_매매기간_오전.Checked = GenieConfig.CB_매매기간_오전;
            CB_매매기간_오후.Checked = GenieConfig.CB_매매기간_오후;
            TB_매매기간_오전주문시간.Text = GenieConfig.TB_매매기간_오전주문시간.ToString();
            TB_매매기간_오후주문시간.Text = GenieConfig.TB_매매기간_오후주문시간.ToString();

            TB_매매기간_취소시간_A.Text = GenieConfig.TB_매매기간_취소시간_A.ToString();
            TB_매매기간_취소시간_B.Text = GenieConfig.TB_매매기간_취소시간_B.ToString();
            TB_매매기간_취소시간_C.Text = GenieConfig.TB_매매기간_취소시간_C.ToString();
            TB_매매기간_취소시간_D.Text = GenieConfig.TB_매매기간_취소시간_D.ToString();
            TB_매매기간_취소시간_E.Text = GenieConfig.TB_매매기간_취소시간_E.ToString();
            TB_매매기간_취소시간_F.Text = GenieConfig.TB_매매기간_취소시간_F.ToString();

            CB_매매기간_강제매도_A.Checked = GenieConfig.CB_매매기간_강제매도_A;
            CB_매매기간_강제매도_B.Checked = GenieConfig.CB_매매기간_강제매도_B;
            CB_매매기간_강제매도_C.Checked = GenieConfig.CB_매매기간_강제매도_C;
            CB_매매기간_강제매도_D.Checked = GenieConfig.CB_매매기간_강제매도_D;
            CB_매매기간_강제매도_E.Checked = GenieConfig.CB_매매기간_강제매도_E;
            CB_매매기간_강제매도_F.Checked = GenieConfig.CB_매매기간_강제매도_F;

            CB_매매기간_수익보전_A.Checked = GenieConfig.CB_매매기간_수익보전_A;
            CB_매매기간_수익보전_B.Checked = GenieConfig.CB_매매기간_수익보전_B;
            CB_매매기간_수익보전_C.Checked = GenieConfig.CB_매매기간_수익보전_C;
            CB_매매기간_수익보전_D.Checked = GenieConfig.CB_매매기간_수익보전_D;
            CB_매매기간_수익보전_E.Checked = GenieConfig.CB_매매기간_수익보전_E;
            CB_매매기간_수익보전_F.Checked = GenieConfig.CB_매매기간_수익보전_F;

            CB_예약주문_장전.Checked = GenieConfig.CB_예약주문_장전;
            CB_예약주문_종가.Checked = GenieConfig.CB_예약주문_종가;

            MTB_예약주문_장전주문시간.Text = GenieConfig.MTB_예약주문_장전주문시간.ToString();
            MTB_예약주문_종가주문시간.Text = GenieConfig.MTB_예약주문_종가주문시간.ToString();

            CB_수동주문_시장가.Checked = GenieConfig.CB_수동주문_시장가;

            CB_예약주문_장전연동.Checked = GenieConfig.CB_예약주문_장전연동;
            CB_예약주문_종가연동.Checked = GenieConfig.CB_예약주문_종가연동;

            CB_수동주문_주문가고정.Checked = GenieConfig.CB_수동주문_주문가고정;
            CB_예약주문_주문가고정.Checked = GenieConfig.CB_예약주문_주문가고정;
            TB_수동주문_tick.Text = GenieConfig.TB_수동주문_tick.ToString();

            if (GenieConfig.checkBox_Simulation)
            {
                CB_수동주문_중간가.Enabled = false;
                GenieConfig.CB_수동주문_중간가 = false;
            }

            CB_개장일.Checked = GenieConfig.CB_개장일;
            CB_수능일.Checked = GenieConfig.CB_수능일;

            CB_개장일.Text = "개장일   " + Form1.str.개장일[..2] + " 월 " + Form1.str.개장일.Substring(2, 2) + " 일";
            CB_수능일.Text = "수능일   " + Form1.str.수능일[..2] + " 월 " + Form1.str.수능일.Substring(2, 2) + " 일";

            Form1.음소거 = GenieConfig.CB_음소거;
            this.ActiveControl = CBB_예약주문_예약종류;

            if (GenieConfig.CB_가이드매매) ControllerDisable.Form_Special_Disable();
        }

        public static void 특수매매_저장()
        {
            try
            {
                // UI의 값을 설정 변수에 저장
                GenieConfig.CB_자금관리 = form.CB_자금관리.Checked;
                GenieConfig.label_관리종목코드 = form.label_관리종목코드.Text;

                // 텍스트 상자에서 쉼표(,) 및 공백을 제거하여 순수 숫자 문자열 추출
                string 유지현금_문자열 = form.TB_자금관리_유지현금.Text.Replace(",", "").Trim();

                // long 타입으로 안전하게 변환하여 저장 (에러 방지)
                if (long.TryParse(유지현금_문자열, out long 변환된_유지현금))
                {
                    GenieConfig.TB_자금관리_유지현금 = 변환된_유지현금;
                }
                else
                {
                    // 입력값이 비어있거나 숫자가 아닌 문자가 들어갔을 경우의 안전장치
                    GenieConfig.TB_자금관리_유지현금 = 0;
                }
            }
            catch (Exception e)
            {
                Form1.Console_print("특수매매_저장 자금관리: " + e.Message);
                Log.에러기록("특수매매_저장 자금관리 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_sil_commission.Text, out double sil_commission);
                double.TryParse(form.TB_mo_commission.Text, out double mo_commission);

                // [핵심 방어] 사용자가 0이나 빈칸을 입력했을 때의 기본값 세팅 (퍼센트 단위 그대로 입력)
                if (sil_commission == 0) sil_commission = 0.015; // 실투자 수수료 0.015%
                if (mo_commission == 0) mo_commission = 0.35;    // 모의투자 수수료 0.35%

                // 화면(UI)과 설정(GenieConfig)에 값을 저장합니다. (여기에는 0.015, 0.35가 저장됨)
                GenieConfig.TB_sil_commission = sil_commission;
                GenieConfig.TB_mo_commission = mo_commission;
                form.TB_sil_commission.Text = sil_commission.ToString();
                form.TB_mo_commission.Text = mo_commission.ToString();

                // [완벽한 비율 환산] 연산용 수수료에는 반드시 100을 나누어 순수 비율(비례상수)로 변환!
                // 예: 0.015 / 100 = 0.00015
                if (Form1.server.Equals("모의투자"))
                {
                    Form1.수수료 = GenieConfig.TB_mo_commission / 100.0;
                }
                else
                {
                    Form1.수수료 = GenieConfig.TB_sil_commission / 100.0;
                }
            }
            catch (Exception e)
            {
                Form1.Console_print("특수매매_저장 세금,수수료 에러: " + e.Message); Log.에러기록("특수매매_저장 세금,수수료 에러: " + e.Message);
            }

            try
            {
                GenieConfig.combo_신규그룹_A = GET.ComboBoxIndex(form.combo_신규그룹_A);
                GenieConfig.combo_신규그룹_B = GET.ComboBoxIndex(form.combo_신규그룹_B);
                GenieConfig.combo_신규그룹_C = GET.ComboBoxIndex(form.combo_신규그룹_C);
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 에러: " + e.Message); Log.에러기록("특수매매_저장 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_In_group_ratio_A.Text, out double In_group_ratio_A);
                double.TryParse(form.TB_In_group_ratio_B.Text, out double In_group_ratio_B);
                double.TryParse(form.TB_In_group_ratio_C.Text, out double In_group_ratio_C);
                double.TryParse(form.TB_In_group_ratio_D.Text, out double In_group_ratio_D);

                GenieConfig.TB_In_group_ratio_A = In_group_ratio_A;
                GenieConfig.TB_In_group_ratio_B = In_group_ratio_B;
                GenieConfig.TB_In_group_ratio_C = In_group_ratio_C;
                GenieConfig.TB_In_group_ratio_D = In_group_ratio_D;

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

                GenieConfig.TB_Out_group_ratio_A = Out_group_ratio_A;
                GenieConfig.TB_Out_group_ratio_B = Out_group_ratio_B;
                GenieConfig.TB_Out_group_ratio_C = Out_group_ratio_C;
                GenieConfig.TB_Out_group_ratio_D = Out_group_ratio_D;

                form.TB_Out_group_ratio_A.Text = Out_group_ratio_A.ToString();
                form.TB_Out_group_ratio_B.Text = Out_group_ratio_B.ToString();
                form.TB_Out_group_ratio_C.Text = Out_group_ratio_C.ToString();
                form.TB_Out_group_ratio_D.Text = Out_group_ratio_D.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 조건별그룹지정 에러: " + e.Message); Log.에러기록("특수매매_저장 조건별그룹지정 에러: " + e.Message);
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

                GenieConfig.TB_group_In_won_A = Math.Abs(In_won_A);
                GenieConfig.TB_group_In_won_B = Math.Abs(In_won_B);
                GenieConfig.TB_group_In_won_C = Math.Abs(In_won_C);
                GenieConfig.TB_group_In_won_D = Math.Abs(In_won_D);

                form.TB_group_In_won_A.Text = GenieConfig.TB_group_In_won_A.ToString();
                form.TB_group_In_won_B.Text = GenieConfig.TB_group_In_won_B.ToString();
                form.TB_group_In_won_C.Text = GenieConfig.TB_group_In_won_C.ToString();
                form.TB_group_In_won_D.Text = GenieConfig.TB_group_In_won_D.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 조건별그룹지정 에러: " + e.Message); Log.에러기록("특수매매_저장 조건별그룹지정 에러: " + e.Message);
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

                GenieConfig.TB_group_In_ratio_A = Math.Abs(group_In_ratio_A);
                GenieConfig.TB_group_In_ratio_B = Math.Abs(group_In_ratio_B);
                GenieConfig.TB_group_In_ratio_C = Math.Abs(group_In_ratio_C);
                GenieConfig.TB_group_In_ratio_D = Math.Abs(group_In_ratio_D);

                form.TB_group_In_ratio_A.Text = GenieConfig.TB_group_In_ratio_A.ToString();
                form.TB_group_In_ratio_B.Text = GenieConfig.TB_group_In_ratio_B.ToString();
                form.TB_group_In_ratio_C.Text = GenieConfig.TB_group_In_ratio_C.ToString();
                form.TB_group_In_ratio_D.Text = GenieConfig.TB_group_In_ratio_D.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 조건별그룹지정 에러: " + e.Message); Log.에러기록("특수매매_저장 조건별그룹지정 에러: " + e.Message);
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

                GenieConfig.TB_group_In_value_A = group_In_value_A;
                GenieConfig.TB_group_In_value_B = group_In_value_B;
                GenieConfig.TB_group_In_value_C = group_In_value_C;
                GenieConfig.TB_group_In_value_D = group_In_value_D;

                form.TB_group_In_value_A.Text = group_In_value_A.ToString();
                form.TB_group_In_value_B.Text = group_In_value_B.ToString();
                form.TB_group_In_value_C.Text = group_In_value_C.ToString();
                form.TB_group_In_value_D.Text = group_In_value_D.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 조건별그룹지정 에러: " + e.Message); Log.에러기록("특수매매_저장 조건별그룹지정 에러: " + e.Message);
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

                GenieConfig.MTB_group_In_CanselTime_A = group_In_CanselTime_A;
                GenieConfig.MTB_group_In_CanselTime_B = group_In_CanselTime_B;
                GenieConfig.MTB_group_In_CanselTime_C = group_In_CanselTime_C;
                GenieConfig.MTB_group_In_CanselTime_D = group_In_CanselTime_D;

                form.MTB_group_In_CanselTime_A.Text = group_In_CanselTime_A.ToString();
                form.MTB_group_In_CanselTime_B.Text = group_In_CanselTime_B.ToString();
                form.MTB_group_In_CanselTime_C.Text = group_In_CanselTime_C.ToString();
                form.MTB_group_In_CanselTime_D.Text = group_In_CanselTime_D.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 조건별그룹지정 에러: " + e.Message); Log.에러기록("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_group_In_repeat_A.Text, out int group_In_repeat_A);
                int.TryParse(form.MTB_group_In_repeat_B.Text, out int group_In_repeat_B);
                int.TryParse(form.MTB_group_In_repeat_C.Text, out int group_In_repeat_C);
                int.TryParse(form.MTB_group_In_repeat_D.Text, out int group_In_repeat_D);

                GenieConfig.MTB_group_In_repeat_A = group_In_repeat_A;
                GenieConfig.MTB_group_In_repeat_B = group_In_repeat_B;
                GenieConfig.MTB_group_In_repeat_C = group_In_repeat_C;
                GenieConfig.MTB_group_In_repeat_D = group_In_repeat_D;

                form.MTB_group_In_repeat_A.Text = group_In_repeat_A.ToString();
                form.MTB_group_In_repeat_B.Text = group_In_repeat_B.ToString();
                form.MTB_group_In_repeat_C.Text = group_In_repeat_C.ToString();
                form.MTB_group_In_repeat_D.Text = group_In_repeat_D.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 조건별그룹지정 에러: " + e.Message); Log.에러기록("특수매매_저장 조건별그룹지정 에러: " + e.Message);
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

                GenieConfig.TB_group_out_ratio_A = Math.Abs(group_out_ratio_A);
                GenieConfig.TB_group_out_ratio_B = Math.Abs(group_out_ratio_B);
                GenieConfig.TB_group_out_ratio_C = Math.Abs(group_out_ratio_C);
                GenieConfig.TB_group_out_ratio_D = Math.Abs(group_out_ratio_D);

                form.TB_group_out_ratio_A.Text = GenieConfig.TB_group_out_ratio_A.ToString();
                form.TB_group_out_ratio_B.Text = GenieConfig.TB_group_out_ratio_B.ToString();
                form.TB_group_out_ratio_C.Text = GenieConfig.TB_group_out_ratio_C.ToString();
                form.TB_group_out_ratio_D.Text = GenieConfig.TB_group_out_ratio_D.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 조건별그룹지정 에러: " + e.Message); Log.에러기록("특수매매_저장 조건별그룹지정 에러: " + e.Message);
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

                GenieConfig.TB_group_Out_value_A = group_Out_value_A;
                GenieConfig.TB_group_Out_value_B = group_Out_value_B;
                GenieConfig.TB_group_Out_value_C = group_Out_value_C;
                GenieConfig.TB_group_Out_value_D = group_Out_value_D;

                form.TB_group_Out_value_A.Text = group_Out_value_A.ToString();
                form.TB_group_Out_value_B.Text = group_Out_value_B.ToString();
                form.TB_group_Out_value_C.Text = group_Out_value_C.ToString();
                form.TB_group_Out_value_D.Text = group_Out_value_D.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 조건별그룹지정 에러: " + e.Message); Log.에러기록("특수매매_저장 조건별그룹지정 에러: " + e.Message);
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

                GenieConfig.MTB_group_out_CanselTime_A = group_out_CanselTime_A;
                GenieConfig.MTB_group_out_CanselTime_B = group_out_CanselTime_B;
                GenieConfig.MTB_group_out_CanselTime_C = group_out_CanselTime_C;
                GenieConfig.MTB_group_out_CanselTime_D = group_out_CanselTime_D;

                form.MTB_group_out_CanselTime_A.Text = group_out_CanselTime_A.ToString();
                form.MTB_group_out_CanselTime_B.Text = group_out_CanselTime_B.ToString();
                form.MTB_group_out_CanselTime_C.Text = group_out_CanselTime_C.ToString();
                form.MTB_group_out_CanselTime_D.Text = group_out_CanselTime_D.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 조건별그룹지정 에러: " + e.Message); Log.에러기록("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_group_out_repeat_A.Text, out int group_out_repeat_A);
                int.TryParse(form.MTB_group_out_repeat_B.Text, out int group_out_repeat_B);
                int.TryParse(form.MTB_group_out_repeat_C.Text, out int group_out_repeat_C);
                int.TryParse(form.MTB_group_out_repeat_D.Text, out int group_out_repeat_D);

                GenieConfig.MTB_group_out_repeat_A = group_out_repeat_A;
                GenieConfig.MTB_group_out_repeat_B = group_out_repeat_B;
                GenieConfig.MTB_group_out_repeat_C = group_out_repeat_C;
                GenieConfig.MTB_group_out_repeat_D = group_out_repeat_D;

                form.MTB_group_out_repeat_A.Text = group_out_repeat_A.ToString();
                form.MTB_group_out_repeat_B.Text = group_out_repeat_B.ToString();
                form.MTB_group_out_repeat_C.Text = group_out_repeat_C.ToString();
                form.MTB_group_out_repeat_D.Text = group_out_repeat_D.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 조건별그룹지정 에러: " + e.Message); Log.에러기록("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }

            try
            {
                GenieConfig.CBB_group_In_won_A = GET.ComboBoxIndex(form.CBB_group_In_won_A);
                GenieConfig.CBB_group_In_won_B = GET.ComboBoxIndex(form.CBB_group_In_won_B);
                GenieConfig.CBB_group_In_won_C = GET.ComboBoxIndex(form.CBB_group_In_won_C);
                GenieConfig.CBB_group_In_won_D = GET.ComboBoxIndex(form.CBB_group_In_won_D);

                GenieConfig.CBB_In_group_choice_A = GET.ComboBoxIndex(form.CBB_I_group_choice_A);
                GenieConfig.CBB_In_group_choice_B = GET.ComboBoxIndex(form.CBB_I_group_choice_B);
                GenieConfig.CBB_In_group_choice_C = GET.ComboBoxIndex(form.CBB_I_group_choice_C);
                GenieConfig.CBB_In_group_choice_D = GET.ComboBoxIndex(form.CBB_I_group_choice_D);

                GenieConfig.CBB_Out_group_choice_A = GET.ComboBoxIndex(form.CBB_O_group_choice_A);
                GenieConfig.CBB_Out_group_choice_B = GET.ComboBoxIndex(form.CBB_O_group_choice_B);
                GenieConfig.CBB_Out_group_choice_C = GET.ComboBoxIndex(form.CBB_O_group_choice_C);
                GenieConfig.CBB_Out_group_choice_D = GET.ComboBoxIndex(form.CBB_O_group_choice_D);

                GenieConfig.CBB_Out_group_A = GET.ComboBoxIndex(form.CBB_Out_group_A);
                GenieConfig.CBB_Out_group_B = GET.ComboBoxIndex(form.CBB_Out_group_B);
                GenieConfig.CBB_Out_group_C = GET.ComboBoxIndex(form.CBB_Out_group_C);
                GenieConfig.CBB_Out_group_D = GET.ComboBoxIndex(form.CBB_Out_group_D);

                GenieConfig.CBB_group_In_jumun_A = GET.ComboBoxIndex(form.CBB_group_In_jumun_A);
                GenieConfig.CBB_group_In_choice_A = GET.ComboBoxIndex(form.CBB_group_In_choice_A);
                GenieConfig.CBB_group_out_jumun_A = GET.ComboBoxIndex(form.CBB_group_out_jumun_A);
                GenieConfig.CBB_group_out_choice_A = GET.ComboBoxIndex(form.CBB_group_out_choice_A);

                GenieConfig.CBB_group_In_jumun_B = GET.ComboBoxIndex(form.CBB_group_In_jumun_B);
                GenieConfig.CBB_group_In_choice_B = GET.ComboBoxIndex(form.CBB_group_In_choice_B);
                GenieConfig.CBB_group_out_jumun_B = GET.ComboBoxIndex(form.CBB_group_out_jumun_B);
                GenieConfig.CBB_group_out_choice_B = GET.ComboBoxIndex(form.CBB_group_out_choice_B);

                GenieConfig.CBB_group_In_jumun_C = GET.ComboBoxIndex(form.CBB_group_In_jumun_C);
                GenieConfig.CBB_group_In_choice_C = GET.ComboBoxIndex(form.CBB_group_In_choice_C);
                GenieConfig.CBB_group_out_jumun_C = GET.ComboBoxIndex(form.CBB_group_out_jumun_C);
                GenieConfig.CBB_group_out_choice_C = GET.ComboBoxIndex(form.CBB_group_out_choice_C);

                GenieConfig.CBB_group_In_jumun_D = GET.ComboBoxIndex(form.CBB_group_In_jumun_D);
                GenieConfig.CBB_group_In_choice_D = GET.ComboBoxIndex(form.CBB_group_In_choice_D);
                GenieConfig.CBB_group_out_jumun_D = GET.ComboBoxIndex(form.CBB_group_out_jumun_D);
                GenieConfig.CBB_group_out_choice_D = GET.ComboBoxIndex(form.CBB_group_out_choice_D);

                GenieConfig.CBB_In_group_A = GET.ComboBoxIndex(form.CBB_In_group_A);
                GenieConfig.CBB_In_group_B = GET.ComboBoxIndex(form.CBB_In_group_B);
                GenieConfig.CBB_In_group_C = GET.ComboBoxIndex(form.CBB_In_group_C);
                GenieConfig.CBB_In_group_D = GET.ComboBoxIndex(form.CBB_In_group_D);
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 조건별그룹지정 에러: " + e.Message); Log.에러기록("특수매매_저장 조건별그룹지정 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_예약주문_장전매수호가.Text, out int 장전매수);
                int.TryParse(form.TB_예약주문_장전매도호가.Text, out int 장전매도);
                int.TryParse(form.TB_예약주문_종가매수호가.Text, out int 종가매수);
                int.TryParse(form.TB_예약주문_종가매도호가.Text, out int 종가매도);

                GenieConfig.TB_예약주문_장전매수호가 = 장전매수;
                GenieConfig.TB_예약주문_장전매도호가 = 장전매도;
                GenieConfig.TB_예약주문_종가매수호가 = 종가매수;
                GenieConfig.TB_예약주문_종가매도호가 = 종가매도;

                form.TB_예약주문_장전매수호가.Text = 장전매수.ToString();
                form.TB_예약주문_장전매도호가.Text = 장전매도.ToString();
                form.TB_예약주문_종가매수호가.Text = 종가매수.ToString();
                form.TB_예약주문_종가매도호가.Text = 종가매도.ToString();

                double.TryParse(form.TB_예약주문_장전매수비중.Text, out double 장전매수비중);
                double.TryParse(form.TB_예약주문_장전매도비중.Text, out double 장전매도비중);
                double.TryParse(form.TB_예약주문_종가매수비중.Text, out double 종가매수비중);
                double.TryParse(form.TB_예약주문_종가매도비중.Text, out double 종가매도비중);

                GenieConfig.TB_예약주문_장전매수비중 = Math.Abs(장전매수비중);
                GenieConfig.TB_예약주문_장전매도비중 = Math.Abs(장전매도비중);
                GenieConfig.TB_예약주문_종가매수비중 = Math.Abs(종가매수비중);
                GenieConfig.TB_예약주문_종가매도비중 = Math.Abs(종가매도비중);

                form.TB_예약주문_장전매수비중.Text = GenieConfig.TB_예약주문_장전매수비중.ToString();
                form.TB_예약주문_장전매도비중.Text = GenieConfig.TB_예약주문_장전매도비중.ToString();
                form.TB_예약주문_종가매수비중.Text = GenieConfig.TB_예약주문_종가매수비중.ToString();
                form.TB_예약주문_종가매도비중.Text = GenieConfig.TB_예약주문_종가매도비중.ToString();

                GenieConfig.CBB_예약주문_장전매수선택 = GET.ComboBoxIndex(form.CBB_예약주문_장전매수선택);
                GenieConfig.CBB_예약주문_장전매도선택 = GET.ComboBoxIndex(form.CBB_예약주문_장전매도선택);
                GenieConfig.CBB_예약주문_종가매수선택 = GET.ComboBoxIndex(form.CBB_예약주문_종가매수선택);
                GenieConfig.CBB_예약주문_종가매도선택 = GET.ComboBoxIndex(form.CBB_예약주문_종가매도선택);


                double.TryParse(form.TB_예약주문_주문비.Text, out double 주문_예약);
                GenieConfig.TB_예약주문_주문비 = 주문_예약;
                form.TB_예약주문_주문비.Text = 주문_예약.ToString();

                GenieConfig.CBB_예약주문_예약종류 = form.CBB_예약주문_예약종류.SelectedIndex;
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 주문예약 에러: " + e.Message); Log.에러기록("특수매매_저장 주문예약 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_예약주문_장전주문시간.Text, out int 장전주문시간);
                int.TryParse(form.MTB_예약주문_종가주문시간.Text, out int 종가주문시간);

                if (GenieConfig.CB_NXT)
                {
                    if (장전주문시간 < 80000 || 120000 < 장전주문시간) 장전주문시간 = 80000;
                    if (120000 > 종가주문시간 || 종가주문시간 > 200000) 종가주문시간 = 195500;
                }
                else
                {
                    if (장전주문시간 < 84000 || 120000 < 장전주문시간) 장전주문시간 = 85000;
                    if (120000 > 종가주문시간 || 종가주문시간 > 153000) 종가주문시간 = 151500;
                }

                GenieConfig.MTB_예약주문_장전주문시간 = 장전주문시간;
                GenieConfig.MTB_예약주문_종가주문시간 = 종가주문시간;

                form.MTB_예약주문_장전주문시간.Text = 장전주문시간.ToString();
                form.MTB_예약주문_종가주문시간.Text = 종가주문시간.ToString();

                if (GenieConfig.MTB_예약주문_장전주문시간 > Form1.Get.TimeNow) Form1.예약주문_장전 = true;
                if (GenieConfig.MTB_예약주문_종가주문시간 > Form1.Get.TimeNow) Form1.예약주문_종가 = true;
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 장전,종가 주문시간 에러: " + e.Message); Log.에러기록("특수매매_저장 장전,종가 주문시간 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_매매기간_오전주문시간.Text, out int 오전주문시간);
                int.TryParse(form.TB_매매기간_오후주문시간.Text, out int 오후주문시간);

                if (GenieConfig.CB_NXT)
                {
                    if (오전주문시간 < 080100 || 120000 < 오전주문시간) 오전주문시간 = 080100;
                    if (120000 > 오후주문시간 || 오후주문시간 > 200000) 오후주문시간 = 195000;
                }
                else
                {
                    if (오전주문시간 < 090100 || 120000 < 오전주문시간) 오전주문시간 = 090100;
                    if (120000 > 오후주문시간 || 오후주문시간 > 152000) 오후주문시간 = 151000;
                }

                GenieConfig.TB_매매기간_오전주문시간 = 오전주문시간;
                GenieConfig.TB_매매기간_오후주문시간 = 오후주문시간;

                form.TB_매매기간_오전주문시간.Text = 오전주문시간.ToString();
                form.TB_매매기간_오후주문시간.Text = 오후주문시간.ToString();

                if (GenieConfig.TB_매매기간_오전주문시간 > Form1.Get.TimeNow) Form1.매매기간_오전 = true;
                if (GenieConfig.TB_매매기간_오후주문시간 > Form1.Get.TimeNow) Form1.매매기간_오후 = true;
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 장전,종가 주문시간 에러: " + e.Message); Log.에러기록("특수매매_저장 장전,종가 주문시간 에러: " + e.Message);
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

                GenieConfig.MTB_매매기간_기간_A = 기간_A;
                GenieConfig.MTB_매매기간_기간_B = 기간_B;
                GenieConfig.MTB_매매기간_기간_C = 기간_C;
                GenieConfig.MTB_매매기간_기간_D = 기간_D;
                GenieConfig.MTB_매매기간_기간_E = 기간_E;
                GenieConfig.MTB_매매기간_기간_F = 기간_F;

                form.MTB_매매기간_기간_A.Text = 기간_A.ToString();
                form.MTB_매매기간_기간_B.Text = 기간_B.ToString();
                form.MTB_매매기간_기간_C.Text = 기간_C.ToString();
                form.MTB_매매기간_기간_D.Text = 기간_D.ToString();
                form.MTB_매매기간_기간_E.Text = 기간_E.ToString();
                form.MTB_매매기간_기간_F.Text = 기간_F.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 매매기간 주문 에러: " + e.Message); Log.에러기록("특수매매_저장 매매기간 주문 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_매매기간_기준_A.Text, out double day_기준_A);
                double.TryParse(form.TB_매매기간_기준_B.Text, out double day_기준_B);
                double.TryParse(form.TB_매매기간_기준_C.Text, out double day_기준_C);
                double.TryParse(form.TB_매매기간_기준_D.Text, out double day_기준_D);
                double.TryParse(form.TB_매매기간_기준_E.Text, out double day_기준_E);
                double.TryParse(form.TB_매매기간_기준_F.Text, out double day_기준_F);

                GenieConfig.TB_매매기간_기준_A = day_기준_A;
                GenieConfig.TB_매매기간_기준_B = day_기준_B;
                GenieConfig.TB_매매기간_기준_C = day_기준_C;
                GenieConfig.TB_매매기간_기준_D = day_기준_D;
                GenieConfig.TB_매매기간_기준_E = day_기준_E;
                GenieConfig.TB_매매기간_기준_F = day_기준_F;

                form.TB_매매기간_기준_A.Text = day_기준_A.ToString();
                form.TB_매매기간_기준_B.Text = day_기준_B.ToString();
                form.TB_매매기간_기준_C.Text = day_기준_C.ToString();
                form.TB_매매기간_기준_D.Text = day_기준_D.ToString();
                form.TB_매매기간_기준_E.Text = day_기준_E.ToString();
                form.TB_매매기간_기준_F.Text = day_기준_F.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 매매기간 주문 에러: " + e.Message); Log.에러기록("특수매매_저장 매매기간 주문 에러: " + e.Message);
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

                GenieConfig.TB_매매기간_ratio_A = Math.Abs(Ratio_A);
                GenieConfig.TB_매매기간_ratio_B = Math.Abs(Ratio_B);
                GenieConfig.TB_매매기간_ratio_C = Math.Abs(Ratio_C);
                GenieConfig.TB_매매기간_ratio_D = Math.Abs(Ratio_D);
                GenieConfig.TB_매매기간_ratio_E = Math.Abs(Ratio_E);
                GenieConfig.TB_매매기간_ratio_F = Math.Abs(Ratio_F);

                form.TB_매매기간_ratio_A.Text = GenieConfig.TB_매매기간_ratio_A.ToString();
                form.TB_매매기간_ratio_B.Text = GenieConfig.TB_매매기간_ratio_B.ToString();
                form.TB_매매기간_ratio_C.Text = GenieConfig.TB_매매기간_ratio_C.ToString();
                form.TB_매매기간_ratio_D.Text = GenieConfig.TB_매매기간_ratio_D.ToString();
                form.TB_매매기간_ratio_E.Text = GenieConfig.TB_매매기간_ratio_E.ToString();
                form.TB_매매기간_ratio_F.Text = GenieConfig.TB_매매기간_ratio_F.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 매매기간 주문 에러: " + e.Message); Log.에러기록("특수매매_저장 매매기간 주문 에러: " + e.Message);
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

                GenieConfig.TB_매매기간_value_A = day_value_A;
                GenieConfig.TB_매매기간_value_B = day_value_B;
                GenieConfig.TB_매매기간_value_C = day_value_C;
                GenieConfig.TB_매매기간_value_D = day_value_D;
                GenieConfig.TB_매매기간_value_E = day_value_E;
                GenieConfig.TB_매매기간_value_F = day_value_F;

                form.TB_매매기간_value_A.Text = day_value_A.ToString();
                form.TB_매매기간_value_B.Text = day_value_B.ToString();
                form.TB_매매기간_value_C.Text = day_value_C.ToString();
                form.TB_매매기간_value_D.Text = day_value_D.ToString();
                form.TB_매매기간_value_E.Text = day_value_E.ToString();
                form.TB_매매기간_value_F.Text = day_value_F.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 매매기간 주문 에러: " + e.Message); Log.에러기록("특수매매_저장 매매기간 주문 에러: " + e.Message);
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

                GenieConfig.TB_매매기간_취소시간_A = 취소시간_A;
                GenieConfig.TB_매매기간_취소시간_B = 취소시간_B;
                GenieConfig.TB_매매기간_취소시간_C = 취소시간_C;
                GenieConfig.TB_매매기간_취소시간_D = 취소시간_D;
                GenieConfig.TB_매매기간_취소시간_E = 취소시간_E;
                GenieConfig.TB_매매기간_취소시간_F = 취소시간_F;

                form.TB_매매기간_취소시간_A.Text = 취소시간_A.ToString();
                form.TB_매매기간_취소시간_B.Text = 취소시간_B.ToString();
                form.TB_매매기간_취소시간_C.Text = 취소시간_C.ToString();
                form.TB_매매기간_취소시간_D.Text = 취소시간_D.ToString();
                form.TB_매매기간_취소시간_E.Text = 취소시간_E.ToString();
                form.TB_매매기간_취소시간_F.Text = 취소시간_F.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 취소시간 입력 에러: " + e.Message); Log.에러기록("특수매매_저장 취소시간 입력 에러");
            }

            GenieConfig.CB_매매기간_오전 = form.CB_매매기간_오전.Checked;
            GenieConfig.CB_매매기간_오후 = form.CB_매매기간_오후.Checked;

            GenieConfig.CB_매매기간_강제매도_A = form.CB_매매기간_강제매도_A.Checked;
            GenieConfig.CB_매매기간_강제매도_B = form.CB_매매기간_강제매도_B.Checked;
            GenieConfig.CB_매매기간_강제매도_C = form.CB_매매기간_강제매도_C.Checked;
            GenieConfig.CB_매매기간_강제매도_D = form.CB_매매기간_강제매도_D.Checked;
            GenieConfig.CB_매매기간_강제매도_E = form.CB_매매기간_강제매도_E.Checked;
            GenieConfig.CB_매매기간_강제매도_F = form.CB_매매기간_강제매도_F.Checked;

            GenieConfig.CB_매매기간_수익보전_A = form.CB_매매기간_수익보전_A.Checked;
            GenieConfig.CB_매매기간_수익보전_B = form.CB_매매기간_수익보전_B.Checked;
            GenieConfig.CB_매매기간_수익보전_C = form.CB_매매기간_수익보전_C.Checked;
            GenieConfig.CB_매매기간_수익보전_D = form.CB_매매기간_수익보전_D.Checked;
            GenieConfig.CB_매매기간_수익보전_E = form.CB_매매기간_수익보전_E.Checked;
            GenieConfig.CB_매매기간_수익보전_F = form.CB_매매기간_수익보전_F.Checked;

            GenieConfig.CBB_매매기간_trading_A = GET.ComboBoxIndex(form.CBB_매매기간_trading_A);
            GenieConfig.CBB_매매기간_trading_B = GET.ComboBoxIndex(form.CBB_매매기간_trading_B);
            GenieConfig.CBB_매매기간_trading_C = GET.ComboBoxIndex(form.CBB_매매기간_trading_C);
            GenieConfig.CBB_매매기간_trading_D = GET.ComboBoxIndex(form.CBB_매매기간_trading_D);
            GenieConfig.CBB_매매기간_trading_E = GET.ComboBoxIndex(form.CBB_매매기간_trading_E);
            GenieConfig.CBB_매매기간_trading_F = GET.ComboBoxIndex(form.CBB_매매기간_trading_F);

            GenieConfig.CBB_매매기간_Jumun_A = GET.ComboBoxIndex(form.CBB_매매기간_Jumun_A);
            GenieConfig.CBB_매매기간_Jumun_B = GET.ComboBoxIndex(form.CBB_매매기간_Jumun_B);
            GenieConfig.CBB_매매기간_Jumun_C = GET.ComboBoxIndex(form.CBB_매매기간_Jumun_C);
            GenieConfig.CBB_매매기간_Jumun_D = GET.ComboBoxIndex(form.CBB_매매기간_Jumun_D);
            GenieConfig.CBB_매매기간_Jumun_E = GET.ComboBoxIndex(form.CBB_매매기간_Jumun_E);
            GenieConfig.CBB_매매기간_Jumun_F = GET.ComboBoxIndex(form.CBB_매매기간_Jumun_F);

            GenieConfig.CBB_매매기간_choice_A = GET.ComboBoxIndex(form.CBB_매매기간_choice_A);
            GenieConfig.CBB_매매기간_choice_B = GET.ComboBoxIndex(form.CBB_매매기간_choice_B);
            GenieConfig.CBB_매매기간_choice_C = GET.ComboBoxIndex(form.CBB_매매기간_choice_C);
            GenieConfig.CBB_매매기간_choice_D = GET.ComboBoxIndex(form.CBB_매매기간_choice_D);
            GenieConfig.CBB_매매기간_choice_E = GET.ComboBoxIndex(form.CBB_매매기간_choice_E);
            GenieConfig.CBB_매매기간_choice_F = GET.ComboBoxIndex(form.CBB_매매기간_choice_F);

            GenieConfig.CBB_매매기간_기준_A = GET.ComboBoxIndex(form.CBB_매매기간_기준_A);
            GenieConfig.CBB_매매기간_기준_B = GET.ComboBoxIndex(form.CBB_매매기간_기준_B);
            GenieConfig.CBB_매매기간_기준_C = GET.ComboBoxIndex(form.CBB_매매기간_기준_C);
            GenieConfig.CBB_매매기간_기준_D = GET.ComboBoxIndex(form.CBB_매매기간_기준_D);
            GenieConfig.CBB_매매기간_기준_E = GET.ComboBoxIndex(form.CBB_매매기간_기준_E);
            GenieConfig.CBB_매매기간_기준_F = GET.ComboBoxIndex(form.CBB_매매기간_기준_F);

            GenieConfig.CB_group_기준금 = form.CB_group_기준금.Checked;
            GenieConfig.CB_매매기간_기준금 = form.CB_매매기간_기준금.Checked;

            GenieConfig.CB_Group_In_work_A = form.CB_Group_In_work_A.Checked;
            GenieConfig.CB_Group_In_work_B = form.CB_Group_In_work_B.Checked;
            GenieConfig.CB_Group_In_work_C = form.CB_Group_In_work_C.Checked;
            GenieConfig.CB_Group_In_work_D = form.CB_Group_In_work_D.Checked;
            GenieConfig.CB_Group_Out_work_A = form.CB_Group_Out_work_A.Checked;
            GenieConfig.CB_Group_Out_work_B = form.CB_Group_Out_work_B.Checked;
            GenieConfig.CB_Group_Out_work_C = form.CB_Group_Out_work_C.Checked;
            GenieConfig.CB_Group_Out_work_D = form.CB_Group_Out_work_D.Checked;

            GenieConfig.CB_Group_In_trading_A = form.CB_Group_In_trading_A.Checked;
            GenieConfig.CB_Group_In_trading_B = form.CB_Group_In_trading_B.Checked;
            GenieConfig.CB_Group_In_trading_C = form.CB_Group_In_trading_C.Checked;
            GenieConfig.CB_Group_In_trading_D = form.CB_Group_In_trading_D.Checked;
            GenieConfig.CB_Group_Out_trading_A = form.CB_Group_Out_trading_A.Checked;
            GenieConfig.CB_Group_Out_trading_B = form.CB_Group_Out_trading_B.Checked;
            GenieConfig.CB_Group_Out_trading_C = form.CB_Group_Out_trading_C.Checked;
            GenieConfig.CB_Group_Out_trading_D = form.CB_Group_Out_trading_D.Checked;

            GenieConfig.CB_group_In_updown_A = form.CB_group_In_updown_A.Checked;
            GenieConfig.CB_group_In_updown_B = form.CB_group_In_updown_B.Checked;
            GenieConfig.CB_group_In_updown_C = form.CB_group_In_updown_C.Checked;
            GenieConfig.CB_group_In_updown_D = form.CB_group_In_updown_D.Checked;
            GenieConfig.CB_group_Out_updown_A = form.CB_group_Out_updown_A.Checked;
            GenieConfig.CB_group_Out_updown_B = form.CB_group_Out_updown_B.Checked;
            GenieConfig.CB_group_Out_updown_C = form.CB_group_Out_updown_C.Checked;
            GenieConfig.CB_group_Out_updown_D = form.CB_group_Out_updown_D.Checked;

            GenieConfig.CB_예약주문_장전체결삭제 = form.CB_예약주문_장전체결삭제.Checked;
            GenieConfig.CB_예약주문_장전전량매도삭제 = form.CB_예약주문_장전전량매도삭제.Checked;
            GenieConfig.CB_예약주문_종가체결삭제 = form.CB_예약주문_종가체결삭제.Checked;
            GenieConfig.CB_예약주문_종가전량매도삭제 = form.CB_예약주문_종가전량매도삭제.Checked;

            GenieConfig.CB_예약주문_장전연동 = form.CB_예약주문_장전연동.Checked;
            GenieConfig.CB_예약주문_종가연동 = form.CB_예약주문_종가연동.Checked;
            GenieConfig.CB_예약주문_주문가고정 = form.CB_예약주문_주문가고정.Checked;

            GenieConfig.CBB_매매기간_주문시간_A = GET.ComboBoxIndex(form.CBB_매매기간_주문시간_A);
            GenieConfig.CBB_매매기간_주문시간_B = GET.ComboBoxIndex(form.CBB_매매기간_주문시간_B);
            GenieConfig.CBB_매매기간_주문시간_C = GET.ComboBoxIndex(form.CBB_매매기간_주문시간_C);
            GenieConfig.CBB_매매기간_주문시간_D = GET.ComboBoxIndex(form.CBB_매매기간_주문시간_D);
            GenieConfig.CBB_매매기간_주문시간_E = GET.ComboBoxIndex(form.CBB_매매기간_주문시간_E);
            GenieConfig.CBB_매매기간_주문시간_F = GET.ComboBoxIndex(form.CBB_매매기간_주문시간_F);

            GenieConfig.CB_예약주문_장전 = form.CB_예약주문_장전.Checked;
            GenieConfig.CB_예약주문_종가 = form.CB_예약주문_종가.Checked;

            try
            {
                double.TryParse(form.TB_매매기간_TS_down_A.Text, out double TB_매매기간_TS_down_A);
                double.TryParse(form.TB_매매기간_TS_down_B.Text, out double TB_매매기간_TS_down_B);
                double.TryParse(form.TB_매매기간_TS_down_C.Text, out double TB_매매기간_TS_down_C);
                double.TryParse(form.TB_매매기간_TS_down_D.Text, out double TB_매매기간_TS_down_D);
                double.TryParse(form.TB_매매기간_TS_down_E.Text, out double TB_매매기간_TS_down_E);
                double.TryParse(form.TB_매매기간_TS_down_F.Text, out double TB_매매기간_TS_down_F);

                GenieConfig.TB_매매기간_TS_down_A = TB_매매기간_TS_down_A;
                GenieConfig.TB_매매기간_TS_down_B = TB_매매기간_TS_down_B;
                GenieConfig.TB_매매기간_TS_down_C = TB_매매기간_TS_down_C;
                GenieConfig.TB_매매기간_TS_down_D = TB_매매기간_TS_down_D;
                GenieConfig.TB_매매기간_TS_down_E = TB_매매기간_TS_down_E;
                GenieConfig.TB_매매기간_TS_down_F = TB_매매기간_TS_down_F;
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 / 매매기간 TS 다운값 입력 오류 : " + e.Message); Log.에러기록("특수매매_저장 / 매매기간 TS 다운값 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_매매기간_TS_MinMAPeriod_A.Text, out int TB_매매기간_TS_MinMAPeriod_A);
                int.TryParse(form.TB_매매기간_TS_MinMAPeriod_B.Text, out int TB_매매기간_TS_MinMAPeriod_B);
                int.TryParse(form.TB_매매기간_TS_MinMAPeriod_C.Text, out int TB_매매기간_TS_MinMAPeriod_C);
                int.TryParse(form.TB_매매기간_TS_MinMAPeriod_D.Text, out int TB_매매기간_TS_MinMAPeriod_D);
                int.TryParse(form.TB_매매기간_TS_MinMAPeriod_E.Text, out int TB_매매기간_TS_MinMAPeriod_E);
                int.TryParse(form.TB_매매기간_TS_MinMAPeriod_F.Text, out int TB_매매기간_TS_MinMAPeriod_F);

                if (TB_매매기간_TS_MinMAPeriod_A == 0) TB_매매기간_TS_MinMAPeriod_A = 1;
                if (TB_매매기간_TS_MinMAPeriod_B == 0) TB_매매기간_TS_MinMAPeriod_B = 1;
                if (TB_매매기간_TS_MinMAPeriod_C == 0) TB_매매기간_TS_MinMAPeriod_C = 1;
                if (TB_매매기간_TS_MinMAPeriod_D == 0) TB_매매기간_TS_MinMAPeriod_D = 1;
                if (TB_매매기간_TS_MinMAPeriod_E == 0) TB_매매기간_TS_MinMAPeriod_E = 1;
                if (TB_매매기간_TS_MinMAPeriod_F == 0) TB_매매기간_TS_MinMAPeriod_F = 1;

                if (TB_매매기간_TS_MinMAPeriod_A > 60) TB_매매기간_TS_MinMAPeriod_A = 60;
                if (TB_매매기간_TS_MinMAPeriod_B > 60) TB_매매기간_TS_MinMAPeriod_B = 60;
                if (TB_매매기간_TS_MinMAPeriod_C > 60) TB_매매기간_TS_MinMAPeriod_C = 60;
                if (TB_매매기간_TS_MinMAPeriod_D > 60) TB_매매기간_TS_MinMAPeriod_D = 60;
                if (TB_매매기간_TS_MinMAPeriod_E > 60) TB_매매기간_TS_MinMAPeriod_E = 60;
                if (TB_매매기간_TS_MinMAPeriod_F > 60) TB_매매기간_TS_MinMAPeriod_F = 60;

                GenieConfig.TB_매매기간_TS_MinMAPeriod_A = TB_매매기간_TS_MinMAPeriod_A;
                GenieConfig.TB_매매기간_TS_MinMAPeriod_B = TB_매매기간_TS_MinMAPeriod_B;
                GenieConfig.TB_매매기간_TS_MinMAPeriod_C = TB_매매기간_TS_MinMAPeriod_C;
                GenieConfig.TB_매매기간_TS_MinMAPeriod_D = TB_매매기간_TS_MinMAPeriod_D;
                GenieConfig.TB_매매기간_TS_MinMAPeriod_E = TB_매매기간_TS_MinMAPeriod_E;
                GenieConfig.TB_매매기간_TS_MinMAPeriod_F = TB_매매기간_TS_MinMAPeriod_F;

                form.TB_매매기간_TS_MinMAPeriod_A.Text = TB_매매기간_TS_MinMAPeriod_A.ToString();
                form.TB_매매기간_TS_MinMAPeriod_B.Text = TB_매매기간_TS_MinMAPeriod_B.ToString();
                form.TB_매매기간_TS_MinMAPeriod_C.Text = TB_매매기간_TS_MinMAPeriod_C.ToString();
                form.TB_매매기간_TS_MinMAPeriod_D.Text = TB_매매기간_TS_MinMAPeriod_D.ToString();
                form.TB_매매기간_TS_MinMAPeriod_E.Text = TB_매매기간_TS_MinMAPeriod_E.ToString();
                form.TB_매매기간_TS_MinMAPeriod_F.Text = TB_매매기간_TS_MinMAPeriod_F.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 / 매매기간 TS 다운값 입력 오류 : " + e.Message); Log.에러기록("특수매매_저장 / 매매기간 TS 다운값 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_매매기간_TS_DayMAPeriod_A.Text, out int TB_매매기간_TS_DayMAPeriod_A);
                int.TryParse(form.TB_매매기간_TS_DayMAPeriod_B.Text, out int TB_매매기간_TS_DayMAPeriod_B);
                int.TryParse(form.TB_매매기간_TS_DayMAPeriod_C.Text, out int TB_매매기간_TS_DayMAPeriod_C);
                int.TryParse(form.TB_매매기간_TS_DayMAPeriod_D.Text, out int TB_매매기간_TS_DayMAPeriod_D);
                int.TryParse(form.TB_매매기간_TS_DayMAPeriod_E.Text, out int TB_매매기간_TS_DayMAPeriod_E);
                int.TryParse(form.TB_매매기간_TS_DayMAPeriod_F.Text, out int TB_매매기간_TS_DayMAPeriod_F);

                if (TB_매매기간_TS_DayMAPeriod_A == 0) TB_매매기간_TS_DayMAPeriod_A = 1;
                if (TB_매매기간_TS_DayMAPeriod_B == 0) TB_매매기간_TS_DayMAPeriod_B = 1;
                if (TB_매매기간_TS_DayMAPeriod_C == 0) TB_매매기간_TS_DayMAPeriod_C = 1;
                if (TB_매매기간_TS_DayMAPeriod_D == 0) TB_매매기간_TS_DayMAPeriod_D = 1;
                if (TB_매매기간_TS_DayMAPeriod_E == 0) TB_매매기간_TS_DayMAPeriod_E = 1;
                if (TB_매매기간_TS_DayMAPeriod_F == 0) TB_매매기간_TS_DayMAPeriod_F = 1;

                if (TB_매매기간_TS_DayMAPeriod_A > 60) TB_매매기간_TS_DayMAPeriod_A = 60;
                if (TB_매매기간_TS_DayMAPeriod_B > 60) TB_매매기간_TS_DayMAPeriod_B = 60;
                if (TB_매매기간_TS_DayMAPeriod_C > 60) TB_매매기간_TS_DayMAPeriod_C = 60;
                if (TB_매매기간_TS_DayMAPeriod_D > 60) TB_매매기간_TS_DayMAPeriod_D = 60;
                if (TB_매매기간_TS_DayMAPeriod_E > 60) TB_매매기간_TS_DayMAPeriod_E = 60;
                if (TB_매매기간_TS_DayMAPeriod_F > 60) TB_매매기간_TS_DayMAPeriod_F = 60;

                GenieConfig.TB_매매기간_TS_DayMAPeriod_A = TB_매매기간_TS_DayMAPeriod_A;
                GenieConfig.TB_매매기간_TS_DayMAPeriod_B = TB_매매기간_TS_DayMAPeriod_B;
                GenieConfig.TB_매매기간_TS_DayMAPeriod_C = TB_매매기간_TS_DayMAPeriod_C;
                GenieConfig.TB_매매기간_TS_DayMAPeriod_D = TB_매매기간_TS_DayMAPeriod_D;
                GenieConfig.TB_매매기간_TS_DayMAPeriod_E = TB_매매기간_TS_DayMAPeriod_E;
                GenieConfig.TB_매매기간_TS_DayMAPeriod_F = TB_매매기간_TS_DayMAPeriod_F;

                form.TB_매매기간_TS_DayMAPeriod_A.Text = TB_매매기간_TS_DayMAPeriod_A.ToString();
                form.TB_매매기간_TS_DayMAPeriod_B.Text = TB_매매기간_TS_DayMAPeriod_B.ToString();
                form.TB_매매기간_TS_DayMAPeriod_C.Text = TB_매매기간_TS_DayMAPeriod_C.ToString();
                form.TB_매매기간_TS_DayMAPeriod_D.Text = TB_매매기간_TS_DayMAPeriod_D.ToString();
                form.TB_매매기간_TS_DayMAPeriod_E.Text = TB_매매기간_TS_DayMAPeriod_E.ToString();
                form.TB_매매기간_TS_DayMAPeriod_F.Text = TB_매매기간_TS_DayMAPeriod_F.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("특수매매_저장 / 매매기간 TS 다운값 입력 오류 : " + e.Message); Log.에러기록("특수매매_저장 / 매매기간 TS 다운값 입력 오류 : " + e.Message);
            }

            if (form.CBB_매매기간_trading_A.SelectedIndex != 2) { form.CB_매매기간_TS_A.Checked = false; }
            if (form.CBB_매매기간_trading_B.SelectedIndex != 2) { form.CB_매매기간_TS_B.Checked = false; }
            if (form.CBB_매매기간_trading_C.SelectedIndex != 2) { form.CB_매매기간_TS_C.Checked = false; }
            if (form.CBB_매매기간_trading_D.SelectedIndex != 2) { form.CB_매매기간_TS_D.Checked = false; }
            if (form.CBB_매매기간_trading_E.SelectedIndex != 2) { form.CB_매매기간_TS_E.Checked = false; }
            if (form.CBB_매매기간_trading_F.SelectedIndex != 2) { form.CB_매매기간_TS_F.Checked = false; }

            GenieConfig.CB_매매기간_TS_A = form.CB_매매기간_TS_A.Checked;
            GenieConfig.CB_매매기간_TS_B = form.CB_매매기간_TS_B.Checked;
            GenieConfig.CB_매매기간_TS_C = form.CB_매매기간_TS_C.Checked;
            GenieConfig.CB_매매기간_TS_D = form.CB_매매기간_TS_D.Checked;
            GenieConfig.CB_매매기간_TS_E = form.CB_매매기간_TS_E.Checked;
            GenieConfig.CB_매매기간_TS_F = form.CB_매매기간_TS_F.Checked;

            GenieConfig.CBB_매매기간_TS_MinMAPeriod_A = GET.ComboBoxIndex(form.CBB_매매기간_TS_MinMAPeriod_A);
            GenieConfig.CBB_매매기간_TS_MinMAPeriod_B = GET.ComboBoxIndex(form.CBB_매매기간_TS_MinMAPeriod_B);
            GenieConfig.CBB_매매기간_TS_MinMAPeriod_C = GET.ComboBoxIndex(form.CBB_매매기간_TS_MinMAPeriod_C);
            GenieConfig.CBB_매매기간_TS_MinMAPeriod_D = GET.ComboBoxIndex(form.CBB_매매기간_TS_MinMAPeriod_D);
            GenieConfig.CBB_매매기간_TS_MinMAPeriod_E = GET.ComboBoxIndex(form.CBB_매매기간_TS_MinMAPeriod_E);
            GenieConfig.CBB_매매기간_TS_MinMAPeriod_F = GET.ComboBoxIndex(form.CBB_매매기간_TS_MinMAPeriod_F);

            GenieConfig.CBB_매매기간_TS_DayMAPeriod_A = GET.ComboBoxIndex(form.CBB_매매기간_TS_DayMAPeriod_A);
            GenieConfig.CBB_매매기간_TS_DayMAPeriod_B = GET.ComboBoxIndex(form.CBB_매매기간_TS_DayMAPeriod_B);
            GenieConfig.CBB_매매기간_TS_DayMAPeriod_C = GET.ComboBoxIndex(form.CBB_매매기간_TS_DayMAPeriod_C);
            GenieConfig.CBB_매매기간_TS_DayMAPeriod_D = GET.ComboBoxIndex(form.CBB_매매기간_TS_DayMAPeriod_D);
            GenieConfig.CBB_매매기간_TS_DayMAPeriod_E = GET.ComboBoxIndex(form.CBB_매매기간_TS_DayMAPeriod_E);
            GenieConfig.CBB_매매기간_TS_DayMAPeriod_F = GET.ComboBoxIndex(form.CBB_매매기간_TS_DayMAPeriod_F);


            MA.Get_MA();
        }

        private void Form_Special_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                Form1.form1.CB_특수매매.Checked = false;
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
            GenieConfig.CB_레이아웃고정_특수매매 = CB_레이아웃고정_특수매매.Checked;

            if (!CB_레이아웃고정_특수매매.Checked) LayoutChange.CBB_layout_SelectedIndex(-1);
            else LayoutChange.CBB_layout_SelectedIndex(GenieConfig.CBB_layout);
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
            if (Form1.FormSpecial_Open) Order_Reserve.CBB_예약종류_SelectedIndexChanged();
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

        private void Combo_수동주문_choice_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.로딩완료) Form1.비프음("체크");
            Order_Reserve.Combo_수동주문_choice_DropDownClosed();
        }

        private void BT_수동주문_실행_Click(object sender, EventArgs e) // 잔고 수동매매 
        {
            this.ActiveControl = null;
            Order_Reserve.BT_수동주문_실행_Click();
        }

        private void LB_예약리스트_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Order_Reserve.LB_예약리스트_Click();
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
                    if (CBB_매매기간_trading_A.SelectedIndex == 0) GenieConfig.CBB_매매기간_trading_A = 0;
                    if (CBB_매매기간_trading_A.SelectedIndex == 1 && CBB_매매기간_choice_A.SelectedIndex > 3)
                    {
                        CBB_매매기간_choice_A.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_매매기간_choice_B) || sender.Equals(CBB_매매기간_trading_B))
                {
                    if (CBB_매매기간_trading_B.SelectedIndex == 0) GenieConfig.CBB_매매기간_trading_B = 0;
                    if (CBB_매매기간_trading_B.SelectedIndex == 1 && CBB_매매기간_choice_B.SelectedIndex > 3)
                    {
                        CBB_매매기간_choice_B.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_매매기간_choice_C) || sender.Equals(CBB_매매기간_trading_C))
                {
                    if (CBB_매매기간_trading_C.SelectedIndex == 0) GenieConfig.CBB_매매기간_trading_C = 0;
                    if (CBB_매매기간_trading_C.SelectedIndex == 1 && CBB_매매기간_choice_C.SelectedIndex > 3)
                    {
                        CBB_매매기간_choice_C.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_매매기간_choice_D) || sender.Equals(CBB_매매기간_trading_D))
                {
                    if (CBB_매매기간_trading_D.SelectedIndex == 0) GenieConfig.CBB_매매기간_trading_D = 0;
                    if (CBB_매매기간_trading_D.SelectedIndex == 1 && CBB_매매기간_choice_D.SelectedIndex > 3)
                    {
                        CBB_매매기간_choice_D.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_매매기간_choice_E) || sender.Equals(CBB_매매기간_trading_E))
                {
                    if (CBB_매매기간_trading_E.SelectedIndex == 0) GenieConfig.CBB_매매기간_trading_E = 0;
                    if (CBB_매매기간_trading_E.SelectedIndex == 1 && CBB_매매기간_choice_E.SelectedIndex > 3)
                    {
                        CBB_매매기간_choice_E.SelectedIndex = 2;
                        매도비중알림();
                    }
                }
                if (sender.Equals(CBB_매매기간_choice_F) || sender.Equals(CBB_매매기간_trading_F))
                {
                    if (CBB_매매기간_trading_F.SelectedIndex == 0) GenieConfig.CBB_매매기간_trading_F = 0;
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
                        GenieConfig.CB_예약주문_장전 = false;
                        Form1.예약주문_장전 = true;
                    }
                }

                if (sender.Equals(CB_예약주문_종가))
                {
                    if (!CB_예약주문_종가.Checked)
                    {
                        GenieConfig.CB_예약주문_종가 = false;
                        Form1.예약주문_종가 = true;
                    }
                }

                if (sender.Equals(CB_매매기간_오전))
                {
                    if (!CB_매매기간_오전.Checked)
                    {
                        GenieConfig.CB_매매기간_오전 = false;
                        Form1.매매기간_오전 = true;
                    }
                }

                if (sender.Equals(CB_매매기간_오후))
                {
                    if (!CB_매매기간_오후.Checked)
                    {
                        GenieConfig.CB_매매기간_오후 = false;
                        Form1.매매기간_오후 = true;
                    }
                }
            }
        }

        private void 매매일_기준_콤보박스(object sender, EventArgs _)
        {
            // [최적화 1] 조기 리턴 (Early Exit)
            // 조건이 안 맞으면 아예 밑에 코드를 실행도 안 하고 끝내버림
            if (!Form1.FormSpecial_Open) return;

            // [최적화 2] 안전한 형변환
            if (!(sender is ComboBox cb)) return;

            // [최적화 3] 불필요한 연산 방지
            // 선택된 인덱스가 2 또는 3이 아니면 바로 종료 (뒤에 긴 if문들 검사할 필요 없음)
            int idx = cb.SelectedIndex;
            if (idx != 2 && idx != 3) return;

            // [최적화 4] 짝꿍 텍스트박스 찾기 (if-else if 구조)
            // sender를 한 번만 확인해서 딱 맞는 텍스트박스를 찾음
            TextBox targetTB = null;

            if (cb == CBB_매매기간_기준_A) targetTB = TB_매매기간_기준_A;
            else if (cb == CBB_매매기간_기준_B) targetTB = TB_매매기간_기준_B;
            else if (cb == CBB_매매기간_기준_C) targetTB = TB_매매기간_기준_C;
            else if (cb == CBB_매매기간_기준_D) targetTB = TB_매매기간_기준_D;
            else if (cb == CBB_매매기간_기준_E) targetTB = TB_매매기간_기준_E;
            else if (cb == CBB_매매기간_기준_F) targetTB = TB_매매기간_기준_F;

            // [최적화 5] 텍스트 변경
            // targetTB가 존재하고, 마이너스(-)로 시작할 때만 자름
            if (targetTB != null && targetTB.Text.StartsWith("-"))
            {
                targetTB.Text = targetTB.Text[1..];
            }
        }

        /////////////////////           매매일기준 매매            ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        private void CheckBox_Checked_Changed(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            // 1. 철벽 방어막: 체크박스가 아니면 즉시 탈출 (프로그램 뻗음 방지)
            if (!(sender is CheckBox cb)) return;

            // 2. 삼항 연산자로 바꿀 목표 텍스트를 0.001초 만에 결정
            string targetText = cb.Checked ? "■" : "□";

            // 3. UI 렌더링 최적화: 현재 텍스트와 목표 텍스트가 "다를 때만" 화면에 그린다!
            if (cb.Text != targetText)
            {
                cb.Text = targetText;
            }
        }

        private void Once_CheckedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            // 1. 철벽 방어막: 체크박스가 아니면 즉시 탈출
            if (!(sender is CheckBox cb)) return;

            // 2. 삼항 연산자로 바꿀 목표 색상을 0.001초 만에 결정
            Color targetColor = cb.Checked ? Color.Red : Color.Black;

            // 3. UI 렌더링 최적화: 현재 색상과 목표 색상이 "다를 때만" 붓을 들어 칠한다!
            if (cb.ForeColor != targetColor)
            {
                cb.ForeColor = targetColor;
            }
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

        private void 양수음수소수_키프레스_(object sender, KeyPressEventArgs e) // 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, true); // textbox 에 양수, 음수 , 소수  숫자만 입력 받을수 있음 
        }

        private void 양수소수_키프레스_(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, false); // textbox 에 양수 , 소수 숫자만 입력 받을수 있음
        }

        private void TextBox_양실수만(object sender, EventArgs e)
        {
            TextValue.TextBox_양실수만(sender);
        }

        private void 양수실수_키프레스_(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, false); // textbox 에 양수 , 실수 숫자만 입력 받을수 있음
        }

        private void 양수음수실수_키프레스_(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, true); // textbox 에 양수, 음수 , 실수 숫자만 입력 받을수 있음
        }

        bool 소리 = false;
        private void CB_매매기간_TS_set_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                CB_매매기간_TS_A.Checked = GenieConfig.CB_매매기간_TS_A;
                CB_매매기간_TS_B.Checked = GenieConfig.CB_매매기간_TS_B;
                CB_매매기간_TS_C.Checked = GenieConfig.CB_매매기간_TS_C;
                CB_매매기간_TS_D.Checked = GenieConfig.CB_매매기간_TS_D;
                CB_매매기간_TS_E.Checked = GenieConfig.CB_매매기간_TS_E;
                CB_매매기간_TS_F.Checked = GenieConfig.CB_매매기간_TS_F;

                TB_매매기간_TS_down_A.Text = GenieConfig.TB_매매기간_TS_down_A.ToString();
                TB_매매기간_TS_down_B.Text = GenieConfig.TB_매매기간_TS_down_B.ToString();
                TB_매매기간_TS_down_C.Text = GenieConfig.TB_매매기간_TS_down_C.ToString();
                TB_매매기간_TS_down_D.Text = GenieConfig.TB_매매기간_TS_down_D.ToString();
                TB_매매기간_TS_down_E.Text = GenieConfig.TB_매매기간_TS_down_E.ToString();
                TB_매매기간_TS_down_F.Text = GenieConfig.TB_매매기간_TS_down_F.ToString();

                TB_매매기간_TS_MinMAPeriod_A.Text = GenieConfig.TB_매매기간_TS_MinMAPeriod_A.ToString();
                TB_매매기간_TS_MinMAPeriod_B.Text = GenieConfig.TB_매매기간_TS_MinMAPeriod_B.ToString();
                TB_매매기간_TS_MinMAPeriod_C.Text = GenieConfig.TB_매매기간_TS_MinMAPeriod_C.ToString();
                TB_매매기간_TS_MinMAPeriod_D.Text = GenieConfig.TB_매매기간_TS_MinMAPeriod_D.ToString();
                TB_매매기간_TS_MinMAPeriod_E.Text = GenieConfig.TB_매매기간_TS_MinMAPeriod_E.ToString();
                TB_매매기간_TS_MinMAPeriod_F.Text = GenieConfig.TB_매매기간_TS_MinMAPeriod_F.ToString();

                CBB_매매기간_TS_MinMAPeriod_A.SelectedIndex = GenieConfig.CBB_매매기간_TS_MinMAPeriod_A;
                CBB_매매기간_TS_MinMAPeriod_B.SelectedIndex = GenieConfig.CBB_매매기간_TS_MinMAPeriod_B;
                CBB_매매기간_TS_MinMAPeriod_C.SelectedIndex = GenieConfig.CBB_매매기간_TS_MinMAPeriod_C;
                CBB_매매기간_TS_MinMAPeriod_D.SelectedIndex = GenieConfig.CBB_매매기간_TS_MinMAPeriod_D;
                CBB_매매기간_TS_MinMAPeriod_E.SelectedIndex = GenieConfig.CBB_매매기간_TS_MinMAPeriod_E;
                CBB_매매기간_TS_MinMAPeriod_F.SelectedIndex = GenieConfig.CBB_매매기간_TS_MinMAPeriod_F;

                TB_매매기간_TS_DayMAPeriod_A.Text = GenieConfig.TB_매매기간_TS_DayMAPeriod_A.ToString();
                TB_매매기간_TS_DayMAPeriod_B.Text = GenieConfig.TB_매매기간_TS_DayMAPeriod_B.ToString();
                TB_매매기간_TS_DayMAPeriod_C.Text = GenieConfig.TB_매매기간_TS_DayMAPeriod_C.ToString();
                TB_매매기간_TS_DayMAPeriod_D.Text = GenieConfig.TB_매매기간_TS_DayMAPeriod_D.ToString();
                TB_매매기간_TS_DayMAPeriod_E.Text = GenieConfig.TB_매매기간_TS_DayMAPeriod_E.ToString();
                TB_매매기간_TS_DayMAPeriod_F.Text = GenieConfig.TB_매매기간_TS_DayMAPeriod_F.ToString();

                CBB_매매기간_TS_DayMAPeriod_A.SelectedIndex = GenieConfig.CBB_매매기간_TS_DayMAPeriod_A;
                CBB_매매기간_TS_DayMAPeriod_B.SelectedIndex = GenieConfig.CBB_매매기간_TS_DayMAPeriod_B;
                CBB_매매기간_TS_DayMAPeriod_C.SelectedIndex = GenieConfig.CBB_매매기간_TS_DayMAPeriod_C;
                CBB_매매기간_TS_DayMAPeriod_D.SelectedIndex = GenieConfig.CBB_매매기간_TS_DayMAPeriod_D;
                CBB_매매기간_TS_DayMAPeriod_E.SelectedIndex = GenieConfig.CBB_매매기간_TS_DayMAPeriod_E;
                CBB_매매기간_TS_DayMAPeriod_F.SelectedIndex = GenieConfig.CBB_매매기간_TS_DayMAPeriod_F;

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

        private void CB_수동주문_중간가_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                form.CB_수동주문_시장가.Checked = false;
            }
        }

        private void CB_수동주문_시장가_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                form.CB_수동주문_중간가.Checked = false;
            }
        }

        private void TB_자금관리_유지현금_TextChanged(object sender, EventArgs e)
        {
            TextValue.숫자콤마넣기_TextChanged(sender);
        }

        private void TB_자금관리종목_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // [최적화 1] 엔터키 입력 시 윈도우 기본 경고음 제거 및 불필요한 이벤트 전파 차단
                // (입력 지연을 없애고 체감 반응 속도를 높여줍니다)
                e.Handled = true;
                e.SuppressKeyPress = true;

                TextBox tb = sender as TextBox;
                if (tb == null) return;

                string 입력된_종목명 = tb.Text.Trim();
                if (string.IsNullOrEmpty(입력된_종목명)) return;

                // [최적화 2] 찾은_종목 이라는 중간 임시 변수 생성을 제거하여 메모리 절약
                foreach (var item in Form1.Market_Item_List.Values)
                {
                    // [최적화 3] CPU를 가장 적게 쓰는 '초고속 문자열 비교 (Ordinal)' 적용
                    // == 연산자보다 내부적으로 바이트 단위 비교를 수행하여 훨씬 빠릅니다.
                    if (string.Equals(item.종목명, 입력된_종목명, StringComparison.Ordinal))
                    {
                        // 값을 찾자마자 UI에 꽂아넣고 즉시 탈출!
                        form.label_관리종목코드.Text = item.종목코드;

                        info.요청(item.종목코드, "info_자금관리", "", false);
                        break;
                    }
                }
            }
        }

        private void CB_자금관리_CheckedChanged(object sender, EventArgs e)
        {
            // 1. 안전한 형변환 및 null 체크
            CheckBox CB = sender as CheckBox;
            if (CB == null) return;

            Form1.form1.체크박스_비프(sender);

            // 2. 바꿀 모양 결정 (체크 여부에 따라 네모 모양 선택)
            char 변경할문자 = CB.Checked ? '■' : '□';

            // 3. [최적화 핵심] 텍스트가 1글자 이상 존재하고, 첫 번째 글자가 목표 문자와 다를 때만 UI 업데이트!
            if (CB.Text.Length > 0 && CB.Text[0] != 변경할문자)
            {
                // 문자열 전체를 자르고 붙이는 대신, 배열로 펼쳐서 맨 앞(인덱스 0) 딱 1개만 바꾼 뒤 합칩니다. (초고속 연산)
                char[] 텍스트배열 = CB.Text.ToCharArray();
                텍스트배열[0] = 변경할문자;

                CB.Text = new string(텍스트배열);
            }
        }
    }
}
