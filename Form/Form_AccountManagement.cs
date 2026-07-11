using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace 지니64
{
    public partial class Form_AccountManagement : Form
    {
        public static Form_AccountManagement form;
        public Form_AccountManagement()
        {
            form = this;
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer |
                        ControlStyles.UserPaint |
                        ControlStyles.AllPaintingInWmPaint |
                        ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
        }

        public void Form_AccountManagement_Load()
        {
            Form1.음소거 = true;

            if (Form1.Get.오전감시시간 < Form1.Get.TimeNow) LB_리밸매도시간오전.BackColor = Color.Tan;
            if (Form1.Get.오후감시시간 < Form1.Get.TimeNow) LB_리밸매도시간오후.BackColor = Color.Tan;

            CB_리밸TS_1.Checked = false;
            CB_리밸TS_2.Checked = false;
            CB_mma.Checked = false;
            panel_리밸TS_1.Hide();
            panel_리밸TS_2.Hide();
            panel_mma.Hide();

            combo_rebalance_use_condition_A.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_use_condition_A);
            combo_rebalance_use_condition_B.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_use_condition_B);
            combo_rebalance_use_condition_C.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_use_condition_C);
            combo_rebalance_use_condition_D.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_use_condition_D);
            combo_rebalance_use_condition_E.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_use_condition_E);
            combo_rebalance_use_condition_F.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_use_condition_F);
            combo_rebalance_use_condition_G.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_use_condition_G);
            CBB_Liquidation_use_condition_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_use_condition_A);
            CBB_Liquidation_use_condition_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_use_condition_B);
            CBB_Liquidation_use_condition_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_use_condition_C);

            CB_rebalance_A.Checked = GenieConfig.CB_rebalance_A;
            CB_rebalance_B.Checked = GenieConfig.CB_rebalance_B;
            CB_rebalance_C.Checked = GenieConfig.CB_rebalance_C;
            CB_rebalance_D.Checked = GenieConfig.CB_rebalance_D;
            CB_rebalance_E.Checked = GenieConfig.CB_rebalance_E;
            CB_rebalance_F.Checked = GenieConfig.CB_rebalance_F;
            CB_rebalance_G.Checked = GenieConfig.CB_rebalance_G;
            CB_Liquidation_A.Checked = GenieConfig.CB_Liquidation_A;
            CB_Liquidation_B.Checked = GenieConfig.CB_Liquidation_B;
            CB_Liquidation_C.Checked = GenieConfig.CB_Liquidation_C;

            CB_총매수금.Checked = GenieConfig.CB_총매수금;
            CB_일매수제한금.Checked = GenieConfig.CB_일매수제한금;
            CB_회수제한.Checked = GenieConfig.CB_회수제한;

            CB_cut_기준금.Checked = GenieConfig.CB_cut_기준금;
            CB_cut_A.Checked = GenieConfig.CB_cut_A;
            CB_cut_B.Checked = GenieConfig.CB_cut_B;
            CB_cut_C.Checked = GenieConfig.CB_cut_C;

            MTB_cut_time_A.Text = GenieConfig.MTB_cut_time_A.ToString();
            MTB_cut_time_B.Text = GenieConfig.MTB_cut_time_B.ToString();
            MTB_cut_time_C.Text = GenieConfig.MTB_cut_time_C.ToString();

            TB_cut_수익금1_A.Text = GenieConfig.TB_cut_수익금1_A.ToString();
            TB_cut_수익금1_B.Text = GenieConfig.TB_cut_수익금1_B.ToString();
            TB_cut_수익금1_C.Text = GenieConfig.TB_cut_수익금1_C.ToString();

            TB_cut_수익금2_A.Text = GenieConfig.TB_cut_수익금2_A.ToString();
            TB_cut_수익금2_B.Text = GenieConfig.TB_cut_수익금2_B.ToString();
            TB_cut_수익금2_C.Text = GenieConfig.TB_cut_수익금2_C.ToString();

            TB_cut_남길퍼_A.Text = GenieConfig.TB_cut_남길퍼_A.ToString();
            TB_cut_남길퍼_B.Text = GenieConfig.TB_cut_남길퍼_B.ToString();
            TB_cut_남길퍼_C.Text = GenieConfig.TB_cut_남길퍼_C.ToString();

            TB_cut_P_A.Text = GenieConfig.TB_cut_P_A.ToString();
            TB_cut_P_B.Text = GenieConfig.TB_cut_P_B.ToString();
            TB_cut_P_C.Text = GenieConfig.TB_cut_P_C.ToString();

            TB_cut_won_A.Text = GenieConfig.TB_cut_won_A.ToString();
            TB_cut_won_B.Text = GenieConfig.TB_cut_won_B.ToString();
            TB_cut_won_C.Text = GenieConfig.TB_cut_won_C.ToString();

            TB_cut_ratio_A.Text = GenieConfig.TB_cut_ratio_A.ToString();
            TB_cut_ratio_B.Text = GenieConfig.TB_cut_ratio_B.ToString();
            TB_cut_ratio_C.Text = GenieConfig.TB_cut_ratio_C.ToString();

            CBB_cut_gubun_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_cut_gubun_A);
            CBB_cut_gubun_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_cut_gubun_B);
            CBB_cut_gubun_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_cut_gubun_C);

            CBB_cut_jumun_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_cut_jumun_A);
            CBB_cut_jumun_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_cut_jumun_B);
            CBB_cut_jumun_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_cut_jumun_C);

            MTB_cut_cansel_time_A.Text = GenieConfig.MTB_cut_cansel_time_A.ToString();
            MTB_cut_cansel_time_B.Text = GenieConfig.MTB_cut_cansel_time_B.ToString();
            MTB_cut_cansel_time_C.Text = GenieConfig.MTB_cut_cansel_time_C.ToString();

            TB_cut_value_A.Text = GenieConfig.TB_cut_value_A.ToString();
            TB_cut_value_B.Text = GenieConfig.TB_cut_value_B.ToString();
            TB_cut_value_C.Text = GenieConfig.TB_cut_value_C.ToString();

            CB_cut_LB_A.Text = Form1.str.cut_LB_A;
            CB_cut_LB_B.Text = Form1.str.cut_LB_B;
            CB_cut_LB_C.Text = Form1.str.cut_LB_C;

            CB_rebalance_option_A.Checked = GenieConfig.CB_rebalance_option_A;
            CB_rebalance_option_B.Checked = GenieConfig.CB_rebalance_option_B;
            CB_rebalance_option_C.Checked = GenieConfig.CB_rebalance_option_C;
            CB_rebalance_option_D.Checked = GenieConfig.CB_rebalance_option_D;
            CB_rebalance_option_E.Checked = GenieConfig.CB_rebalance_option_E;
            CB_rebalance_option_F.Checked = GenieConfig.CB_rebalance_option_F;
            CB_rebalance_option_G.Checked = GenieConfig.CB_rebalance_option_G;

            CB_rebalance_기준금.Checked = GenieConfig.CB_rebalance_기준금;

            CBB_rebalance_1A.SelectedItem = GenieConfig.리밸매도기준1_A;
            CBB_rebalance_1B.SelectedItem = GenieConfig.리밸매도기준1_B;
            CBB_rebalance_1C.SelectedItem = GenieConfig.리밸매도기준1_C;
            CBB_rebalance_1D.SelectedItem = GenieConfig.리밸매도기준1_D;
            CBB_rebalance_1E.SelectedItem = GenieConfig.리밸매도기준1_E;
            CBB_rebalance_1F.SelectedItem = GenieConfig.리밸매도기준1_F;
            CBB_rebalance_1G.SelectedItem = GenieConfig.리밸매도기준1_G;
            CBB_rebalance_2A.SelectedItem = GenieConfig.리밸매도기준2_A;
            CBB_rebalance_2B.SelectedItem = GenieConfig.리밸매도기준2_B;
            CBB_rebalance_2C.SelectedItem = GenieConfig.리밸매도기준2_C;
            CBB_rebalance_2D.SelectedItem = GenieConfig.리밸매도기준2_D;
            CBB_rebalance_2E.SelectedItem = GenieConfig.리밸매도기준2_E;
            CBB_rebalance_2F.SelectedItem = GenieConfig.리밸매도기준2_F;
            CBB_rebalance_2G.SelectedItem = GenieConfig.리밸매도기준2_G;

            CBB_rebalance_1A.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_1A);
            CBB_rebalance_1B.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_1B);
            CBB_rebalance_1C.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_1C);
            CBB_rebalance_1D.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_1D);
            CBB_rebalance_1E.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_1E);
            CBB_rebalance_1F.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_1F);
            CBB_rebalance_1G.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_1G);
            CBB_rebalance_2A.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_2A);
            CBB_rebalance_2B.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_2B);
            CBB_rebalance_2C.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_2C);
            CBB_rebalance_2D.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_2D);
            CBB_rebalance_2E.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_2E);
            CBB_rebalance_2F.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_2F);
            CBB_rebalance_2G.SelectedIndex = GET.ComboBoxIndex(CBB_rebalance_2G);

            CBB_rebalance_DropDownClosed_(CBB_rebalance_1A);
            CBB_rebalance_DropDownClosed_(CBB_rebalance_1B);
            CBB_rebalance_DropDownClosed_(CBB_rebalance_1C);
            CBB_rebalance_DropDownClosed_(CBB_rebalance_1D);
            CBB_rebalance_DropDownClosed_(CBB_rebalance_1E);
            CBB_rebalance_DropDownClosed_(CBB_rebalance_1F);
            CBB_rebalance_DropDownClosed_(CBB_rebalance_1G);
            CBB_rebalance_DropDownClosed_(CBB_rebalance_2A);
            CBB_rebalance_DropDownClosed_(CBB_rebalance_2B);
            CBB_rebalance_DropDownClosed_(CBB_rebalance_2C);
            CBB_rebalance_DropDownClosed_(CBB_rebalance_2D);
            CBB_rebalance_DropDownClosed_(CBB_rebalance_2E);
            CBB_rebalance_DropDownClosed_(CBB_rebalance_2F);
            CBB_rebalance_DropDownClosed_(CBB_rebalance_2G);

            CB_rebalance_choice_A.Checked = GenieConfig.CB_rebalance_choice_A;
            CB_rebalance_choice_B.Checked = GenieConfig.CB_rebalance_choice_B;
            CB_rebalance_choice_C.Checked = GenieConfig.CB_rebalance_choice_C;
            CB_rebalance_choice_D.Checked = GenieConfig.CB_rebalance_choice_D;
            CB_rebalance_choice_E.Checked = GenieConfig.CB_rebalance_choice_E;
            CB_rebalance_choice_F.Checked = GenieConfig.CB_rebalance_choice_F;
            CB_rebalance_choice_G.Checked = GenieConfig.CB_rebalance_choice_G;

            combo_rebalance_suik_gubun_A.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_suik_gubun_A);
            combo_rebalance_suik_gubun_B.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_suik_gubun_B);
            combo_rebalance_suik_gubun_C.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_suik_gubun_C);
            combo_rebalance_suik_gubun_D.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_suik_gubun_D);
            combo_rebalance_suik_gubun_E.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_suik_gubun_E);
            combo_rebalance_suik_gubun_F.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_suik_gubun_F);
            combo_rebalance_suik_gubun_G.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_suik_gubun_G);

            combo_rebalance_sell_gubun_A.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_sell_gubun_A);
            combo_rebalance_sell_gubun_B.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_sell_gubun_B);
            combo_rebalance_sell_gubun_C.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_sell_gubun_C);
            combo_rebalance_sell_gubun_D.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_sell_gubun_D);
            combo_rebalance_sell_gubun_E.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_sell_gubun_E);
            combo_rebalance_sell_gubun_F.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_sell_gubun_F);
            combo_rebalance_sell_gubun_G.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_sell_gubun_G);

            combo_rebalance_maemae_gubun_A.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_maemae_gubun_A);
            combo_rebalance_maemae_gubun_B.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_maemae_gubun_B);
            combo_rebalance_maemae_gubun_C.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_maemae_gubun_C);
            combo_rebalance_maemae_gubun_D.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_maemae_gubun_D);
            combo_rebalance_maemae_gubun_E.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_maemae_gubun_E);
            combo_rebalance_maemae_gubun_F.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_maemae_gubun_F);
            combo_rebalance_maemae_gubun_G.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_maemae_gubun_G);

            combo_rebalance_jumun_A.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_jumun_A);
            combo_rebalance_jumun_B.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_jumun_B);
            combo_rebalance_jumun_C.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_jumun_C);
            combo_rebalance_jumun_D.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_jumun_D);
            combo_rebalance_jumun_E.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_jumun_E);
            combo_rebalance_jumun_F.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_jumun_F);
            combo_rebalance_jumun_G.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_jumun_G);

            MTB_rebalance_delay_A.Text = GenieConfig.MTB_rebalance_delay_A.ToString();
            MTB_rebalance_delay_B.Text = GenieConfig.MTB_rebalance_delay_B.ToString();
            MTB_rebalance_delay_C.Text = GenieConfig.MTB_rebalance_delay_C.ToString();
            MTB_rebalance_delay_D.Text = GenieConfig.MTB_rebalance_delay_D.ToString();
            MTB_rebalance_delay_E.Text = GenieConfig.MTB_rebalance_delay_E.ToString();
            MTB_rebalance_delay_F.Text = GenieConfig.MTB_rebalance_delay_F.ToString();
            MTB_rebalance_delay_G.Text = GenieConfig.MTB_rebalance_delay_G.ToString();

            TB_rebalance_suik_1_A.Text = GenieConfig.TB_rebalance_suik_1_A.ToString();
            TB_rebalance_suik_1_B.Text = GenieConfig.TB_rebalance_suik_1_B.ToString();
            TB_rebalance_suik_1_C.Text = GenieConfig.TB_rebalance_suik_1_C.ToString();
            TB_rebalance_suik_1_D.Text = GenieConfig.TB_rebalance_suik_1_D.ToString();
            TB_rebalance_suik_1_E.Text = GenieConfig.TB_rebalance_suik_1_E.ToString();
            TB_rebalance_suik_1_F.Text = GenieConfig.TB_rebalance_suik_1_F.ToString();
            TB_rebalance_suik_1_G.Text = GenieConfig.TB_rebalance_suik_1_G.ToString();

            TB_rebalance_suik_2_A.Text = GenieConfig.TB_rebalance_suik_2_A.ToString();
            TB_rebalance_suik_2_B.Text = GenieConfig.TB_rebalance_suik_2_B.ToString();
            TB_rebalance_suik_2_C.Text = GenieConfig.TB_rebalance_suik_2_C.ToString();
            TB_rebalance_suik_2_D.Text = GenieConfig.TB_rebalance_suik_2_D.ToString();
            TB_rebalance_suik_2_E.Text = GenieConfig.TB_rebalance_suik_2_E.ToString();
            TB_rebalance_suik_2_F.Text = GenieConfig.TB_rebalance_suik_2_F.ToString();
            TB_rebalance_suik_2_G.Text = GenieConfig.TB_rebalance_suik_2_G.ToString();

            TB_rebalance_sell_ratio_A.Text = GenieConfig.TB_rebalance_sell_ratio_A.ToString();
            TB_rebalance_sell_ratio_B.Text = GenieConfig.TB_rebalance_sell_ratio_B.ToString();
            TB_rebalance_sell_ratio_C.Text = GenieConfig.TB_rebalance_sell_ratio_C.ToString();
            TB_rebalance_sell_ratio_D.Text = GenieConfig.TB_rebalance_sell_ratio_D.ToString();
            TB_rebalance_sell_ratio_E.Text = GenieConfig.TB_rebalance_sell_ratio_E.ToString();
            TB_rebalance_sell_ratio_F.Text = GenieConfig.TB_rebalance_sell_ratio_F.ToString();
            TB_rebalance_sell_ratio_G.Text = GenieConfig.TB_rebalance_sell_ratio_G.ToString();

            TB_rebalance_maemae_1_A.Text = GenieConfig.TB_rebalance_maemae_1_A.ToString();
            TB_rebalance_maemae_1_B.Text = GenieConfig.TB_rebalance_maemae_1_B.ToString();
            TB_rebalance_maemae_1_C.Text = GenieConfig.TB_rebalance_maemae_1_C.ToString();
            TB_rebalance_maemae_1_D.Text = GenieConfig.TB_rebalance_maemae_1_D.ToString();
            TB_rebalance_maemae_1_E.Text = GenieConfig.TB_rebalance_maemae_1_E.ToString();
            TB_rebalance_maemae_1_F.Text = GenieConfig.TB_rebalance_maemae_1_F.ToString();
            TB_rebalance_maemae_1_G.Text = GenieConfig.TB_rebalance_maemae_1_G.ToString();

            TB_rebalance_maemae_2_A.Text = GenieConfig.TB_rebalance_maemae_2_A.ToString();
            TB_rebalance_maemae_2_B.Text = GenieConfig.TB_rebalance_maemae_2_B.ToString();
            TB_rebalance_maemae_2_C.Text = GenieConfig.TB_rebalance_maemae_2_C.ToString();
            TB_rebalance_maemae_2_D.Text = GenieConfig.TB_rebalance_maemae_2_D.ToString();
            TB_rebalance_maemae_2_E.Text = GenieConfig.TB_rebalance_maemae_2_E.ToString();
            TB_rebalance_maemae_2_F.Text = GenieConfig.TB_rebalance_maemae_2_F.ToString();
            TB_rebalance_maemae_2_G.Text = GenieConfig.TB_rebalance_maemae_2_G.ToString();

            MT_rebalance_repeat_time_A.Text = GenieConfig.MT_rebalance_repeat_time_A.ToString();
            MT_rebalance_repeat_time_B.Text = GenieConfig.MT_rebalance_repeat_time_B.ToString();
            MT_rebalance_repeat_time_C.Text = GenieConfig.MT_rebalance_repeat_time_C.ToString();
            MT_rebalance_repeat_time_D.Text = GenieConfig.MT_rebalance_repeat_time_D.ToString();
            MT_rebalance_repeat_time_E.Text = GenieConfig.MT_rebalance_repeat_time_E.ToString();
            MT_rebalance_repeat_time_F.Text = GenieConfig.MT_rebalance_repeat_time_F.ToString();
            MT_rebalance_repeat_time_G.Text = GenieConfig.MT_rebalance_repeat_time_G.ToString();

            MTB_rebalance_Cancel_time_A.Text = GenieConfig.MTB_rebalance_Cancel_time_A.ToString();
            MTB_rebalance_Cancel_time_B.Text = GenieConfig.MTB_rebalance_Cancel_time_B.ToString();
            MTB_rebalance_Cancel_time_C.Text = GenieConfig.MTB_rebalance_Cancel_time_C.ToString();
            MTB_rebalance_Cancel_time_D.Text = GenieConfig.MTB_rebalance_Cancel_time_D.ToString();
            MTB_rebalance_Cancel_time_E.Text = GenieConfig.MTB_rebalance_Cancel_time_E.ToString();
            MTB_rebalance_Cancel_time_F.Text = GenieConfig.MTB_rebalance_Cancel_time_F.ToString();
            MTB_rebalance_Cancel_time_G.Text = GenieConfig.MTB_rebalance_Cancel_time_G.ToString();

            TB_rebalance_sellratio1_A.Text = GenieConfig.TB_rebalance_sellratio1_A.ToString();
            TB_rebalance_sellratio1_B.Text = GenieConfig.TB_rebalance_sellratio1_B.ToString();
            TB_rebalance_sellratio1_C.Text = GenieConfig.TB_rebalance_sellratio1_C.ToString();
            TB_rebalance_sellratio1_D.Text = GenieConfig.TB_rebalance_sellratio1_D.ToString();
            TB_rebalance_sellratio1_E.Text = GenieConfig.TB_rebalance_sellratio1_E.ToString();
            TB_rebalance_sellratio1_F.Text = GenieConfig.TB_rebalance_sellratio1_F.ToString();
            TB_rebalance_sellratio1_G.Text = GenieConfig.TB_rebalance_sellratio1_G.ToString();

            TB_rebalance_감시_value_A.Text = GenieConfig.TB_rebalance_감시_value_A.ToString();
            TB_rebalance_감시_value_B.Text = GenieConfig.TB_rebalance_감시_value_B.ToString();
            TB_rebalance_감시_value_C.Text = GenieConfig.TB_rebalance_감시_value_C.ToString();
            TB_rebalance_감시_value_D.Text = GenieConfig.TB_rebalance_감시_value_D.ToString();
            TB_rebalance_감시_value_E.Text = GenieConfig.TB_rebalance_감시_value_E.ToString();
            TB_rebalance_감시_value_F.Text = GenieConfig.TB_rebalance_감시_value_F.ToString();
            TB_rebalance_감시_value_G.Text = GenieConfig.TB_rebalance_감시_value_G.ToString();

            combo_rebalance_감시_jumun_A.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_감시_jumun_A);
            combo_rebalance_감시_jumun_B.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_감시_jumun_B);
            combo_rebalance_감시_jumun_C.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_감시_jumun_C);
            combo_rebalance_감시_jumun_D.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_감시_jumun_D);
            combo_rebalance_감시_jumun_E.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_감시_jumun_E);
            combo_rebalance_감시_jumun_F.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_감시_jumun_F);
            combo_rebalance_감시_jumun_G.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_rebalance_감시_jumun_G);

            TB_rebalance_sellvolume1_A.Text = GenieConfig.TB_rebalance_sellvolume1_A.ToString();
            TB_rebalance_sellvolume1_B.Text = GenieConfig.TB_rebalance_sellvolume1_B.ToString();
            TB_rebalance_sellvolume1_C.Text = GenieConfig.TB_rebalance_sellvolume1_C.ToString();
            TB_rebalance_sellvolume1_D.Text = GenieConfig.TB_rebalance_sellvolume1_D.ToString();
            TB_rebalance_sellvolume1_E.Text = GenieConfig.TB_rebalance_sellvolume1_E.ToString();
            TB_rebalance_sellvolume1_F.Text = GenieConfig.TB_rebalance_sellvolume1_F.ToString();
            TB_rebalance_sellvolume1_G.Text = GenieConfig.TB_rebalance_sellvolume1_G.ToString();

            TB_rebalance_sellvolume2_A.Text = GenieConfig.TB_rebalance_sellvolume2_A.ToString();
            TB_rebalance_sellvolume2_B.Text = GenieConfig.TB_rebalance_sellvolume2_B.ToString();
            TB_rebalance_sellvolume2_C.Text = GenieConfig.TB_rebalance_sellvolume2_C.ToString();
            TB_rebalance_sellvolume2_D.Text = GenieConfig.TB_rebalance_sellvolume2_D.ToString();
            TB_rebalance_sellvolume2_E.Text = GenieConfig.TB_rebalance_sellvolume2_E.ToString();
            TB_rebalance_sellvolume2_F.Text = GenieConfig.TB_rebalance_sellvolume2_F.ToString();
            TB_rebalance_sellvolume2_G.Text = GenieConfig.TB_rebalance_sellvolume2_G.ToString();

            TB_rebalance_sellcancel1_A.Text = GenieConfig.TB_rebalance_sellcancel1_A.ToString();
            TB_rebalance_sellcancel1_B.Text = GenieConfig.TB_rebalance_sellcancel1_B.ToString();
            TB_rebalance_sellcancel1_C.Text = GenieConfig.TB_rebalance_sellcancel1_C.ToString();
            TB_rebalance_sellcancel1_D.Text = GenieConfig.TB_rebalance_sellcancel1_D.ToString();
            TB_rebalance_sellcancel1_E.Text = GenieConfig.TB_rebalance_sellcancel1_E.ToString();
            TB_rebalance_sellcancel1_F.Text = GenieConfig.TB_rebalance_sellcancel1_F.ToString();
            TB_rebalance_sellcancel1_G.Text = GenieConfig.TB_rebalance_sellcancel1_G.ToString();

            TB_rebalance_sellratio2_A.Text = GenieConfig.TB_rebalance_sellratio2_A.ToString();
            TB_rebalance_sellratio2_B.Text = GenieConfig.TB_rebalance_sellratio2_B.ToString();
            TB_rebalance_sellratio2_C.Text = GenieConfig.TB_rebalance_sellratio2_C.ToString();
            TB_rebalance_sellratio2_D.Text = GenieConfig.TB_rebalance_sellratio2_D.ToString();
            TB_rebalance_sellratio2_E.Text = GenieConfig.TB_rebalance_sellratio2_E.ToString();
            TB_rebalance_sellratio2_F.Text = GenieConfig.TB_rebalance_sellratio2_F.ToString();
            TB_rebalance_sellratio2_G.Text = GenieConfig.TB_rebalance_sellratio2_G.ToString();

            TB_rebalance_sellcancel2_A.Text = GenieConfig.TB_rebalance_sellcancel2_A.ToString();
            TB_rebalance_sellcancel2_B.Text = GenieConfig.TB_rebalance_sellcancel2_B.ToString();
            TB_rebalance_sellcancel2_C.Text = GenieConfig.TB_rebalance_sellcancel2_C.ToString();
            TB_rebalance_sellcancel2_D.Text = GenieConfig.TB_rebalance_sellcancel2_D.ToString();
            TB_rebalance_sellcancel2_E.Text = GenieConfig.TB_rebalance_sellcancel2_E.ToString();
            TB_rebalance_sellcancel2_F.Text = GenieConfig.TB_rebalance_sellcancel2_F.ToString();
            TB_rebalance_sellcancel2_G.Text = GenieConfig.TB_rebalance_sellcancel2_G.ToString();

            TB_rebalance_value_A.Text = GenieConfig.TB_rebalance_value_A.ToString();
            TB_rebalance_value_B.Text = GenieConfig.TB_rebalance_value_B.ToString();
            TB_rebalance_value_C.Text = GenieConfig.TB_rebalance_value_C.ToString();
            TB_rebalance_value_D.Text = GenieConfig.TB_rebalance_value_D.ToString();
            TB_rebalance_value_E.Text = GenieConfig.TB_rebalance_value_E.ToString();
            TB_rebalance_value_F.Text = GenieConfig.TB_rebalance_value_F.ToString();
            TB_rebalance_value_G.Text = GenieConfig.TB_rebalance_value_G.ToString();

            CBB_rebalance_Selltime_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_Selltime_A);
            CBB_rebalance_Selltime_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_Selltime_B);
            CBB_rebalance_Selltime_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_Selltime_C);
            CBB_rebalance_Selltime_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_Selltime_D);
            CBB_rebalance_Selltime_E.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_Selltime_E);
            CBB_rebalance_Selltime_F.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_Selltime_F);
            CBB_rebalance_Selltime_G.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_Selltime_G);

            MTB_rebalance_Selltime_오전.Text = GenieConfig.MTB_rebalance_Selltime_오전.ToString();
            MTB_rebalance_Selltime_오후.Text = GenieConfig.MTB_rebalance_Selltime_오후.ToString();

            MT_rebalance_starttime_A.Text = GenieConfig.MT_rebalance_starttime_A.ToString();
            MT_rebalance_starttime_B.Text = GenieConfig.MT_rebalance_starttime_B.ToString();
            MT_rebalance_starttime_C.Text = GenieConfig.MT_rebalance_starttime_C.ToString();
            MT_rebalance_starttime_D.Text = GenieConfig.MT_rebalance_starttime_D.ToString();
            MT_rebalance_starttime_E.Text = GenieConfig.MT_rebalance_starttime_E.ToString();
            MT_rebalance_starttime_F.Text = GenieConfig.MT_rebalance_starttime_F.ToString();
            MT_rebalance_starttime_G.Text = GenieConfig.MT_rebalance_starttime_G.ToString();

            MT_rebalance_stoptime_A.Text = GenieConfig.MT_rebalance_stoptime_A.ToString();
            MT_rebalance_stoptime_B.Text = GenieConfig.MT_rebalance_stoptime_B.ToString();
            MT_rebalance_stoptime_C.Text = GenieConfig.MT_rebalance_stoptime_C.ToString();
            MT_rebalance_stoptime_D.Text = GenieConfig.MT_rebalance_stoptime_D.ToString();
            MT_rebalance_stoptime_E.Text = GenieConfig.MT_rebalance_stoptime_E.ToString();
            MT_rebalance_stoptime_F.Text = GenieConfig.MT_rebalance_stoptime_F.ToString();
            MT_rebalance_stoptime_G.Text = GenieConfig.MT_rebalance_stoptime_G.ToString();

            TB_Rebalance_매입금_A.Text = GenieConfig.TB_Rebalance_매입금_A.ToString();
            TB_Rebalance_매입금_B.Text = GenieConfig.TB_Rebalance_매입금_B.ToString();
            TB_Rebalance_매입금_C.Text = GenieConfig.TB_Rebalance_매입금_C.ToString();
            TB_Rebalance_매입금_D.Text = GenieConfig.TB_Rebalance_매입금_D.ToString();
            TB_Rebalance_매입금_E.Text = GenieConfig.TB_Rebalance_매입금_E.ToString();
            TB_Rebalance_매입금_F.Text = GenieConfig.TB_Rebalance_매입금_F.ToString();
            TB_Rebalance_매입금_G.Text = GenieConfig.TB_Rebalance_매입금_G.ToString();

            TB_rebalance_누적거래량_A.Text = GenieConfig.TB_rebalance_누적거래량_A.ToString();
            TB_rebalance_누적거래량_B.Text = GenieConfig.TB_rebalance_누적거래량_B.ToString();
            TB_rebalance_누적거래량_C.Text = GenieConfig.TB_rebalance_누적거래량_C.ToString();
            TB_rebalance_누적거래량_D.Text = GenieConfig.TB_rebalance_누적거래량_D.ToString();
            TB_rebalance_누적거래량_E.Text = GenieConfig.TB_rebalance_누적거래량_E.ToString();
            TB_rebalance_누적거래량_F.Text = GenieConfig.TB_rebalance_누적거래량_F.ToString();
            TB_rebalance_누적거래량_G.Text = GenieConfig.TB_rebalance_누적거래량_G.ToString();

            TB_rebalance_누적거래대금_A.Text = GenieConfig.TB_rebalance_누적거래대금_A.ToString();
            TB_rebalance_누적거래대금_B.Text = GenieConfig.TB_rebalance_누적거래대금_B.ToString();
            TB_rebalance_누적거래대금_C.Text = GenieConfig.TB_rebalance_누적거래대금_C.ToString();
            TB_rebalance_누적거래대금_D.Text = GenieConfig.TB_rebalance_누적거래대금_D.ToString();
            TB_rebalance_누적거래대금_E.Text = GenieConfig.TB_rebalance_누적거래대금_E.ToString();
            TB_rebalance_누적거래대금_F.Text = GenieConfig.TB_rebalance_누적거래대금_F.ToString();
            TB_rebalance_누적거래대금_G.Text = GenieConfig.TB_rebalance_누적거래대금_G.ToString();

            TB_rebalance_MinMAPeriod1_A.Text = GenieConfig.TB_rebalance_MinMAPeriod1_A.ToString();
            TB_rebalance_MinMAPeriod1_B.Text = GenieConfig.TB_rebalance_MinMAPeriod1_B.ToString();
            TB_rebalance_MinMAPeriod1_C.Text = GenieConfig.TB_rebalance_MinMAPeriod1_C.ToString();
            TB_rebalance_MinMAPeriod1_D.Text = GenieConfig.TB_rebalance_MinMAPeriod1_D.ToString();
            TB_rebalance_MinMAPeriod1_E.Text = GenieConfig.TB_rebalance_MinMAPeriod1_E.ToString();
            TB_rebalance_MinMAPeriod1_F.Text = GenieConfig.TB_rebalance_MinMAPeriod1_F.ToString();
            TB_rebalance_MinMAPeriod1_G.Text = GenieConfig.TB_rebalance_MinMAPeriod1_G.ToString();

            CBB_rebalance_MinMAPeriod1_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_A);
            CBB_rebalance_MinMAPeriod1_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_B);
            CBB_rebalance_MinMAPeriod1_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_C);
            CBB_rebalance_MinMAPeriod1_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_D);
            CBB_rebalance_MinMAPeriod1_E.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_E);
            CBB_rebalance_MinMAPeriod1_F.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_F);
            CBB_rebalance_MinMAPeriod1_G.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_G);

            TB_rebalance_MinMAPeriod2_A.Text = GenieConfig.TB_rebalance_MinMAPeriod2_A.ToString();
            TB_rebalance_MinMAPeriod2_B.Text = GenieConfig.TB_rebalance_MinMAPeriod2_B.ToString();
            TB_rebalance_MinMAPeriod2_C.Text = GenieConfig.TB_rebalance_MinMAPeriod2_C.ToString();
            TB_rebalance_MinMAPeriod2_D.Text = GenieConfig.TB_rebalance_MinMAPeriod2_D.ToString();
            TB_rebalance_MinMAPeriod2_E.Text = GenieConfig.TB_rebalance_MinMAPeriod2_E.ToString();
            TB_rebalance_MinMAPeriod2_F.Text = GenieConfig.TB_rebalance_MinMAPeriod2_F.ToString();
            TB_rebalance_MinMAPeriod2_G.Text = GenieConfig.TB_rebalance_MinMAPeriod2_G.ToString();

            CBB_rebalance_MinMAPeriod2_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod2_A);
            CBB_rebalance_MinMAPeriod2_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod2_B);
            CBB_rebalance_MinMAPeriod2_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod2_C);
            CBB_rebalance_MinMAPeriod2_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod2_D);
            CBB_rebalance_MinMAPeriod2_E.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod2_E);
            CBB_rebalance_MinMAPeriod2_F.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod2_F);
            CBB_rebalance_MinMAPeriod2_G.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod2_G);

            CBB_rebalance_MinMAPeriod1_배열_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_배열_A);
            CBB_rebalance_MinMAPeriod1_배열_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_배열_B);
            CBB_rebalance_MinMAPeriod1_배열_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_배열_C);
            CBB_rebalance_MinMAPeriod1_배열_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_배열_D);
            CBB_rebalance_MinMAPeriod1_배열_E.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_배열_E);
            CBB_rebalance_MinMAPeriod1_배열_F.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_배열_F);
            CBB_rebalance_MinMAPeriod1_배열_G.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_MinMAPeriod1_배열_G);

            TB_rebalance_DayMAPeriod1_A.Text = GenieConfig.TB_rebalance_DayMAPeriod1_A.ToString();
            TB_rebalance_DayMAPeriod1_B.Text = GenieConfig.TB_rebalance_DayMAPeriod1_B.ToString();
            TB_rebalance_DayMAPeriod1_C.Text = GenieConfig.TB_rebalance_DayMAPeriod1_C.ToString();
            TB_rebalance_DayMAPeriod1_D.Text = GenieConfig.TB_rebalance_DayMAPeriod1_D.ToString();
            TB_rebalance_DayMAPeriod1_E.Text = GenieConfig.TB_rebalance_DayMAPeriod1_E.ToString();
            TB_rebalance_DayMAPeriod1_F.Text = GenieConfig.TB_rebalance_DayMAPeriod1_F.ToString();
            TB_rebalance_DayMAPeriod1_G.Text = GenieConfig.TB_rebalance_DayMAPeriod1_G.ToString();

            CBB_rebalance_DayMAPeriod1_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod1_A);
            CBB_rebalance_DayMAPeriod1_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod1_B);
            CBB_rebalance_DayMAPeriod1_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod1_C);
            CBB_rebalance_DayMAPeriod1_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod1_D);
            CBB_rebalance_DayMAPeriod1_E.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod1_E);
            CBB_rebalance_DayMAPeriod1_F.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod1_F);
            CBB_rebalance_DayMAPeriod1_G.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod1_G);

            TB_rebalance_DayMAPeriod2_A.Text = GenieConfig.TB_rebalance_DayMAPeriod2_A.ToString();
            TB_rebalance_DayMAPeriod2_B.Text = GenieConfig.TB_rebalance_DayMAPeriod2_B.ToString();
            TB_rebalance_DayMAPeriod2_C.Text = GenieConfig.TB_rebalance_DayMAPeriod2_C.ToString();
            TB_rebalance_DayMAPeriod2_D.Text = GenieConfig.TB_rebalance_DayMAPeriod2_D.ToString();
            TB_rebalance_DayMAPeriod2_E.Text = GenieConfig.TB_rebalance_DayMAPeriod2_E.ToString();
            TB_rebalance_DayMAPeriod2_F.Text = GenieConfig.TB_rebalance_DayMAPeriod2_F.ToString();
            TB_rebalance_DayMAPeriod2_G.Text = GenieConfig.TB_rebalance_DayMAPeriod2_G.ToString();

            CBB_rebalance_DayMAPeriod2_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod2_A);
            CBB_rebalance_DayMAPeriod2_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod2_B);
            CBB_rebalance_DayMAPeriod2_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod2_C);
            CBB_rebalance_DayMAPeriod2_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod2_D);
            CBB_rebalance_DayMAPeriod2_E.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod2_E);
            CBB_rebalance_DayMAPeriod2_F.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod2_F);
            CBB_rebalance_DayMAPeriod2_G.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod2_G);

            CBB_rebalance_DayMAPeriod_배열_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod_배열_A);
            CBB_rebalance_DayMAPeriod_배열_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod_배열_B);
            CBB_rebalance_DayMAPeriod_배열_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod_배열_C);
            CBB_rebalance_DayMAPeriod_배열_D.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod_배열_D);
            CBB_rebalance_DayMAPeriod_배열_E.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod_배열_E);
            CBB_rebalance_DayMAPeriod_배열_F.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod_배열_F);
            CBB_rebalance_DayMAPeriod_배열_G.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_rebalance_DayMAPeriod_배열_G);

            if (GenieConfig.CB_rebalance_매도체크_A) { CB_rebalance_option_A.Checked = false; CB_rebalance_option_A.Enabled = false; }
            if (GenieConfig.CB_rebalance_매도체크_B) { CB_rebalance_option_B.Checked = false; CB_rebalance_option_B.Enabled = false; }
            if (GenieConfig.CB_rebalance_매도체크_C) { CB_rebalance_option_C.Checked = false; CB_rebalance_option_C.Enabled = false; }
            if (GenieConfig.CB_rebalance_매도체크_D) { CB_rebalance_option_D.Checked = false; CB_rebalance_option_D.Enabled = false; }
            if (GenieConfig.CB_rebalance_매도체크_E) { CB_rebalance_option_E.Checked = false; CB_rebalance_option_E.Enabled = false; }
            if (GenieConfig.CB_rebalance_매도체크_F) { CB_rebalance_option_F.Checked = false; CB_rebalance_option_F.Enabled = false; }
            if (GenieConfig.CB_rebalance_매도체크_G) { CB_rebalance_option_G.Checked = false; CB_rebalance_option_G.Enabled = false; }

            CB_rebalance_매도체크_A.Checked = GenieConfig.CB_rebalance_매도체크_A;
            CB_rebalance_매도체크_B.Checked = GenieConfig.CB_rebalance_매도체크_B;
            CB_rebalance_매도체크_C.Checked = GenieConfig.CB_rebalance_매도체크_C;
            CB_rebalance_매도체크_D.Checked = GenieConfig.CB_rebalance_매도체크_D;
            CB_rebalance_매도체크_E.Checked = GenieConfig.CB_rebalance_매도체크_E;
            CB_rebalance_매도체크_F.Checked = GenieConfig.CB_rebalance_매도체크_F;
            CB_rebalance_매도체크_G.Checked = GenieConfig.CB_rebalance_매도체크_G;

            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_A);
            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_B);
            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_C);
            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_D);
            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_E);
            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_F);
            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_G);

            CB_Liquidation_기준금.Checked = GenieConfig.CB_Liquidation_기준금;
            CB_Liquidation_SellStop_A.Checked = GenieConfig.CB_Liquidation_SellStop_A;
            CB_Liquidation_SellStop_B.Checked = GenieConfig.CB_Liquidation_SellStop_B;
            CB_Liquidation_SellStop_C.Checked = GenieConfig.CB_Liquidation_SellStop_C;

            CB_Liquidation_강제매도_A.Checked = GenieConfig.CB_Liquidation_강제매도_A;
            CB_Liquidation_강제매도_B.Checked = GenieConfig.CB_Liquidation_강제매도_B;
            CB_Liquidation_강제매도_C.Checked = GenieConfig.CB_Liquidation_강제매도_C;

            CB_추매금지_Liquidation_A.Checked = GenieConfig.CB_추매금지_Liquidation_A;
            CB_추매금지_Liquidation_B.Checked = GenieConfig.CB_추매금지_Liquidation_B;
            CB_추매금지_Liquidation_C.Checked = GenieConfig.CB_추매금지_Liquidation_C;

            CB_수익보전_Liquidation_A.Checked = GenieConfig.CB_수익보전_Liquidation_A;
            CB_수익보전_Liquidation_B.Checked = GenieConfig.CB_수익보전_Liquidation_B;
            CB_수익보전_Liquidation_C.Checked = GenieConfig.CB_수익보전_Liquidation_C;

            CB_Liquidation_choice_A.Checked = GenieConfig.CB_Liquidation_choice_A;
            CB_Liquidation_choice_B.Checked = GenieConfig.CB_Liquidation_choice_B;
            CB_Liquidation_choice_C.Checked = GenieConfig.CB_Liquidation_choice_C;
            CBB_Liquidation_suik_gubun_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_suik_gubun_A);
            CBB_Liquidation_suik_gubun_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_suik_gubun_B);
            CBB_Liquidation_suik_gubun_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_suik_gubun_C);
            CBB_Liquidation_sell_gubun_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_sell_gubun_A);
            CBB_Liquidation_sell_gubun_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_sell_gubun_B);
            CBB_Liquidation_sell_gubun_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_sell_gubun_C);
            CBB_Liquidation_jumun_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_jumun_A);
            CBB_Liquidation_jumun_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_jumun_B);
            CBB_Liquidation_jumun_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_jumun_C);
            CBB_Liquidation_Cancel_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_Cancel_A);
            CBB_Liquidation_Cancel_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_Cancel_B);
            CBB_Liquidation_Cancel_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_Cancel_C);
            MTB_Liquidation_Starttime_A.Text = GenieConfig.MTB_Liquidation_Starttime_A.ToString();
            MTB_Liquidation_Starttime_B.Text = GenieConfig.MTB_Liquidation_Starttime_B.ToString();
            MTB_Liquidation_Starttime_C.Text = GenieConfig.MTB_Liquidation_Starttime_C.ToString();
            MTB_Liquidation_Stoptime_A.Text = GenieConfig.MTB_Liquidation_Stoptime_A.ToString();
            MTB_Liquidation_Stoptime_B.Text = GenieConfig.MTB_Liquidation_Stoptime_B.ToString();
            MTB_Liquidation_Stoptime_C.Text = GenieConfig.MTB_Liquidation_Stoptime_C.ToString();
            MTB_Liquidation_delay_A.Text = GenieConfig.MTB_Liquidation_delay_A.ToString();
            MTB_Liquidation_delay_B.Text = GenieConfig.MTB_Liquidation_delay_B.ToString();
            MTB_Liquidation_delay_C.Text = GenieConfig.MTB_Liquidation_delay_C.ToString();
            MTB_Liquidation_Cancel_time_A.Text = GenieConfig.MTB_Liquidation_Cancel_time_A.ToString();
            MTB_Liquidation_Cancel_time_B.Text = GenieConfig.MTB_Liquidation_Cancel_time_B.ToString();
            MTB_Liquidation_Cancel_time_C.Text = GenieConfig.MTB_Liquidation_Cancel_time_C.ToString();
            MTB_Liquidation_repeat_A.Text = GenieConfig.MTB_Liquidation_repeat_A.ToString();
            MTB_Liquidation_repeat_B.Text = GenieConfig.MTB_Liquidation_repeat_B.ToString();
            MTB_Liquidation_repeat_C.Text = GenieConfig.MTB_Liquidation_repeat_C.ToString();
            TB_Liquidation_sell_ratio_A.Text = GenieConfig.TB_Liquidation_sell_ratio_A.ToString();
            TB_Liquidation_sell_ratio_B.Text = GenieConfig.TB_Liquidation_sell_ratio_B.ToString();
            TB_Liquidation_sell_ratio_C.Text = GenieConfig.TB_Liquidation_sell_ratio_C.ToString();
            MT_Liquidation_repeat_time_A.Text = GenieConfig.MT_Liquidation_repeat_time_A.ToString();
            MT_Liquidation_repeat_time_B.Text = GenieConfig.MT_Liquidation_repeat_time_B.ToString();
            MT_Liquidation_repeat_time_C.Text = GenieConfig.MT_Liquidation_repeat_time_C.ToString();
            TB_Liquidation_suik_1_A.Text = GenieConfig.TB_Liquidation_suik_1_A.ToString();
            TB_Liquidation_suik_1_B.Text = GenieConfig.TB_Liquidation_suik_1_B.ToString();
            TB_Liquidation_suik_1_C.Text = GenieConfig.TB_Liquidation_suik_1_C.ToString();
            TB_Liquidation_suik_2_A.Text = GenieConfig.TB_Liquidation_suik_2_A.ToString();
            TB_Liquidation_suik_2_B.Text = GenieConfig.TB_Liquidation_suik_2_B.ToString();
            TB_Liquidation_suik_2_C.Text = GenieConfig.TB_Liquidation_suik_2_C.ToString();
            TB_Liquidation_maemae_1_A.Text = GenieConfig.TB_Liquidation_maemae_1_A.ToString();
            TB_Liquidation_maemae_1_B.Text = GenieConfig.TB_Liquidation_maemae_1_B.ToString();
            TB_Liquidation_maemae_1_C.Text = GenieConfig.TB_Liquidation_maemae_1_C.ToString();
            TB_Liquidation_maemae_2_A.Text = GenieConfig.TB_Liquidation_maemae_2_A.ToString();
            TB_Liquidation_maemae_2_B.Text = GenieConfig.TB_Liquidation_maemae_2_B.ToString();
            TB_Liquidation_maemae_2_C.Text = GenieConfig.TB_Liquidation_maemae_2_C.ToString();
            TB_Liquidation_value_A.Text = GenieConfig.TB_Liquidation_value_A.ToString();
            TB_Liquidation_value_B.Text = GenieConfig.TB_Liquidation_value_B.ToString();
            TB_Liquidation_value_C.Text = GenieConfig.TB_Liquidation_value_C.ToString();

            CB_매수기준.Checked = GenieConfig.CB_매수기준;
            CB_손익기준.Checked = GenieConfig.CB_손익기준;
            TB_손익비율.Text = GenieConfig.TB_손익비율.ToString();
            TB_매수비율.Text = GenieConfig.TB_매수비율.ToString();

            TB_분할간격_A.Text = GenieConfig.TB_분할간격_A.ToString();
            TB_분할간격_B.Text = GenieConfig.TB_분할간격_B.ToString();
            TB_분할횟수_A.Text = GenieConfig.TB_분할횟수_A.ToString();
            TB_분할횟수_B.Text = GenieConfig.TB_분할횟수_B.ToString();
            TB_분할간격_C.Text = GenieConfig.TB_분할간격_C.ToString();
            TB_분할횟수_C.Text = GenieConfig.TB_분할횟수_C.ToString();

            TB_잔고청산_매입금1_A.Text = GenieConfig.TB_잔고청산_매입금1_A.ToString();
            TB_잔고청산_매입금1_B.Text = GenieConfig.TB_잔고청산_매입금1_B.ToString();
            TB_잔고청산_매입금1_C.Text = GenieConfig.TB_잔고청산_매입금1_C.ToString();

            TB_잔고청산_매입금2_A.Text = GenieConfig.TB_잔고청산_매입금2_A.ToString();
            TB_잔고청산_매입금2_B.Text = GenieConfig.TB_잔고청산_매입금2_B.ToString();
            TB_잔고청산_매입금2_C.Text = GenieConfig.TB_잔고청산_매입금2_C.ToString();

            TB_총매수금.Text = GenieConfig.TB_총매수금.ToString("N0");
            TB_일매수제한금.Text = GenieConfig.TB_일매수제한금.ToString("N0");
            TB_회수제한.Text = GenieConfig.TB_회수제한.ToString();

            TB_리밸_추매주가이상.Text = GenieConfig.TB_리밸_추매주가이상.ToString("N0");
            TB_리밸_추매주가이하.Text = GenieConfig.TB_리밸_추매주가이하.ToString("N0");
            TB_리밸_추매등락률이상.Text = GenieConfig.TB_리밸_추매등락률이상.ToString();
            TB_리밸_추매등락률이하.Text = GenieConfig.TB_리밸_추매등락률이하.ToString();

            TB_매수기준.Text = int.Parse(GenieConfig.Today_매수기준금.Split('@')[0]).ToString("N0");
            TB_손익기준.Text = int.Parse(GenieConfig.Today_손익기준금.Split('@')[0]).ToString("N0");


            FormPrint.CBB_suik_DropDownClosed(CBB_Liquidation_suik_gubun_A);
            FormPrint.CBB_suik_DropDownClosed(CBB_Liquidation_suik_gubun_B);
            FormPrint.CBB_suik_DropDownClosed(CBB_Liquidation_suik_gubun_C);

            TB_Liquidation_MinMAPeriod_A.Text = GenieConfig.TB_Liquidation_MinMAPeriod_A.ToString();
            TB_Liquidation_MinMAPeriod_B.Text = GenieConfig.TB_Liquidation_MinMAPeriod_B.ToString();
            TB_Liquidation_MinMAPeriod_C.Text = GenieConfig.TB_Liquidation_MinMAPeriod_C.ToString();

            CBB_Liquidation_MinMAPeriod_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_MinMAPeriod_A);
            CBB_Liquidation_MinMAPeriod_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_MinMAPeriod_B);
            CBB_Liquidation_MinMAPeriod_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_MinMAPeriod_C);

            Form1.음소거 = GenieConfig.CB_음소거;

            if (!GenieConfig.CB_가이드매매)
            {
                form.combo_rebalance_use_condition_A.Enabled = true;
                form.combo_rebalance_use_condition_B.Enabled = true;
                form.combo_rebalance_use_condition_C.Enabled = true;
                form.combo_rebalance_use_condition_D.Enabled = true;
                form.combo_rebalance_use_condition_E.Enabled = true;
                form.combo_rebalance_use_condition_F.Enabled = true;
                form.combo_rebalance_use_condition_G.Enabled = true;
                form.리밸_A.Enabled = true;
                form.리밸_B.Enabled = true;
                form.리밸_C.Enabled = true;
                form.리밸_D.Enabled = true;
                form.리밸_E.Enabled = true;
                form.리밸_F.Enabled = true;
                form.리밸_G.Enabled = true;

                form.CBB_Liquidation_use_condition_A.Enabled = true;
                form.CBB_Liquidation_use_condition_B.Enabled = true;
                form.CBB_Liquidation_use_condition_C.Enabled = true;
                form.청산_A.Enabled = true;
                form.청산_B.Enabled = true;
                form.청산_C.Enabled = true;
            }
            else
            {
                ControllerDisable.Form_AccountManagement_Disable();
            }

            CB_Liquidation_TS_A.Checked = GenieConfig.CB_Liquidation_TS_A;
            CB_Liquidation_TS_B.Checked = GenieConfig.CB_Liquidation_TS_B;
            CB_Liquidation_TS_C.Checked = GenieConfig.CB_Liquidation_TS_C;

            TB_Liquidation_TS_down_A.Text = GenieConfig.TB_Liquidation_TS_down_A.ToString();
            TB_Liquidation_TS_down_B.Text = GenieConfig.TB_Liquidation_TS_down_B.ToString();
            TB_Liquidation_TS_down_C.Text = GenieConfig.TB_Liquidation_TS_down_C.ToString();

            TB_Liquidation_TS_MinMAPeriod_A.Text = GenieConfig.TB_Liquidation_TS_MinMAPeriod_A.ToString();
            TB_Liquidation_TS_MinMAPeriod_B.Text = GenieConfig.TB_Liquidation_TS_MinMAPeriod_B.ToString();
            TB_Liquidation_TS_MinMAPeriod_C.Text = GenieConfig.TB_Liquidation_TS_MinMAPeriod_C.ToString();

            CBB_Liquidation_TS_MinMAPeriod_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_TS_MinMAPeriod_A);
            CBB_Liquidation_TS_MinMAPeriod_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_TS_MinMAPeriod_B);
            CBB_Liquidation_TS_MinMAPeriod_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_TS_MinMAPeriod_C);

            TB_Liquidation_TS_DayMAPeriod_A.Text = GenieConfig.TB_Liquidation_TS_DayMAPeriod_A.ToString();
            TB_Liquidation_TS_DayMAPeriod_B.Text = GenieConfig.TB_Liquidation_TS_DayMAPeriod_B.ToString();
            TB_Liquidation_TS_DayMAPeriod_C.Text = GenieConfig.TB_Liquidation_TS_DayMAPeriod_C.ToString();

            CBB_Liquidation_TS_DayMAPeriod_A.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_TS_DayMAPeriod_A);
            CBB_Liquidation_TS_DayMAPeriod_B.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_TS_DayMAPeriod_B);
            CBB_Liquidation_TS_DayMAPeriod_C.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Liquidation_TS_DayMAPeriod_C);

            TB_rebalance_MinMAPeriod1_A.Enabled = false;
            TB_rebalance_MinMAPeriod1_B.Enabled = false;
            TB_rebalance_MinMAPeriod1_C.Enabled = false;
            TB_rebalance_MinMAPeriod1_D.Enabled = false;
            TB_rebalance_MinMAPeriod1_E.Enabled = false;
            TB_rebalance_MinMAPeriod1_F.Enabled = false;
            TB_rebalance_MinMAPeriod1_G.Enabled = false;
            CBB_rebalance_MinMAPeriod1_A.Enabled = false;
            CBB_rebalance_MinMAPeriod1_B.Enabled = false;
            CBB_rebalance_MinMAPeriod1_C.Enabled = false;
            CBB_rebalance_MinMAPeriod1_D.Enabled = false;
            CBB_rebalance_MinMAPeriod1_E.Enabled = false;
            CBB_rebalance_MinMAPeriod1_F.Enabled = false;
            CBB_rebalance_MinMAPeriod1_G.Enabled = false;
        }

        public static void 계좌관리_SAVE()
        {
            GenieConfig.CB_총매수금 = form.CB_총매수금.Checked;
            GenieConfig.CB_일매수제한금 = form.CB_일매수제한금.Checked;
            GenieConfig.CB_회수제한 = form.CB_회수제한.Checked;

            try
            {
                double.TryParse(form.TB_잔고청산_매입금1_A.Text, out double TB_잔고청산_매입금1_A);
                double.TryParse(form.TB_잔고청산_매입금1_B.Text, out double TB_잔고청산_매입금1_B);
                double.TryParse(form.TB_잔고청산_매입금1_C.Text, out double TB_잔고청산_매입금1_C);

                GenieConfig.TB_잔고청산_매입금1_A = Math.Abs(TB_잔고청산_매입금1_A);
                GenieConfig.TB_잔고청산_매입금1_B = Math.Abs(TB_잔고청산_매입금1_B);
                GenieConfig.TB_잔고청산_매입금1_C = Math.Abs(TB_잔고청산_매입금1_C);

                form.TB_잔고청산_매입금1_A.Text = GenieConfig.TB_잔고청산_매입금1_A.ToString();
                form.TB_잔고청산_매입금1_B.Text = GenieConfig.TB_잔고청산_매입금1_B.ToString();
                form.TB_잔고청산_매입금1_C.Text = GenieConfig.TB_잔고청산_매입금1_C.ToString();

                double.TryParse(form.TB_잔고청산_매입금2_A.Text, out double TB_잔고청산_매입금2_A);
                double.TryParse(form.TB_잔고청산_매입금2_B.Text, out double TB_잔고청산_매입금2_B);
                double.TryParse(form.TB_잔고청산_매입금2_C.Text, out double TB_잔고청산_매입금2_C);

                if (TB_잔고청산_매입금2_A == 0) TB_잔고청산_매입금2_A = 10000;
                if (TB_잔고청산_매입금2_B == 0) TB_잔고청산_매입금2_B = 10000;
                if (TB_잔고청산_매입금2_C == 0) TB_잔고청산_매입금2_C = 10000;

                GenieConfig.TB_잔고청산_매입금2_A = Math.Abs(TB_잔고청산_매입금2_A);
                GenieConfig.TB_잔고청산_매입금2_B = Math.Abs(TB_잔고청산_매입금2_B);
                GenieConfig.TB_잔고청산_매입금2_C = Math.Abs(TB_잔고청산_매입금2_C);

                form.TB_잔고청산_매입금2_A.Text = GenieConfig.TB_잔고청산_매입금2_A.ToString();
                form.TB_잔고청산_매입금2_B.Text = GenieConfig.TB_잔고청산_매입금2_B.ToString();
                form.TB_잔고청산_매입금2_C.Text = GenieConfig.TB_잔고청산_매입금2_C.ToString();

                // [비교 로직도 Setting 값으로 변경]
                if (GenieConfig.TB_잔고청산_매입금1_A != Math.Abs(TB_잔고청산_매입금1_A) || GenieConfig.TB_잔고청산_매입금2_A != Math.Abs(TB_잔고청산_매입금2_A))
                    foreach (var 잔고 in Form1.stockBalanceList.Values)
                    {
                        잔고.잔고청산_매입금_A = false;
                    }
                if (GenieConfig.TB_잔고청산_매입금1_B != Math.Abs(TB_잔고청산_매입금1_B) || GenieConfig.TB_잔고청산_매입금2_A != Math.Abs(TB_잔고청산_매입금2_A)) // [주의] 원본 코드에 매입금2_A 비교가 중복되어 보임 (의도된 것인지 확인 필요)
                    foreach (var 잔고 in Form1.stockBalanceList.Values)
                    {
                        잔고.잔고청산_매입금_B = false;
                    }
                if (GenieConfig.TB_잔고청산_매입금1_C != Math.Abs(TB_잔고청산_매입금1_C) || GenieConfig.TB_잔고청산_매입금2_A != Math.Abs(TB_잔고청산_매입금2_A))
                    foreach (var 잔고 in Form1.stockBalanceList.Values)
                    {
                        잔고.잔고청산_매입금_C = false;
                    }
            }
            catch (Exception e)
            {
                Form1.Console_print("잔고청산_저장 / 매입금 입력 오류 : " + e.Message); Log.에러기록("잔고청산_저장 /  매입금 입력 오류 : " + e.Message);
            }

            GenieConfig.CBB_rebalance_Selltime_A = GET.ComboBoxIndex(form.CBB_rebalance_Selltime_A);
            GenieConfig.CBB_rebalance_Selltime_B = GET.ComboBoxIndex(form.CBB_rebalance_Selltime_B);
            GenieConfig.CBB_rebalance_Selltime_C = GET.ComboBoxIndex(form.CBB_rebalance_Selltime_C);
            GenieConfig.CBB_rebalance_Selltime_D = GET.ComboBoxIndex(form.CBB_rebalance_Selltime_D);
            GenieConfig.CBB_rebalance_Selltime_E = GET.ComboBoxIndex(form.CBB_rebalance_Selltime_E);
            GenieConfig.CBB_rebalance_Selltime_F = GET.ComboBoxIndex(form.CBB_rebalance_Selltime_F);
            GenieConfig.CBB_rebalance_Selltime_G = GET.ComboBoxIndex(form.CBB_rebalance_Selltime_G);

            try
            {
                int.TryParse(form.MTB_rebalance_Selltime_오전.Text, out int 오전);
                int.TryParse(form.MTB_rebalance_Selltime_오후.Text, out int 오후);

                if (오전 < 080000) 오전 = 084500;
                if (오후 > 200000) 오후 = 152500;

                form.MTB_rebalance_Selltime_오전.Text = 오전.ToString();
                form.MTB_rebalance_Selltime_오후.Text = 오후.ToString();

                GenieConfig.MTB_rebalance_Selltime_오전 = 오전;
                GenieConfig.MTB_rebalance_Selltime_오후 = 오후;

                Form1.Get.오전감시시간 = 오전;
                Form1.Get.오후감시시간 = 오후;

                if (Form1.Get.오전감시시간 < Form1.Get.TimeNow) form.LB_리밸매도시간오전.BackColor = Color.Tan; else form.LB_리밸매도시간오전.BackColor = Color.Orange;
                if (Form1.Get.오후감시시간 < Form1.Get.TimeNow) form.LB_리밸매도시간오후.BackColor = Color.Tan; else form.LB_리밸매도시간오후.BackColor = Color.Orange;

            }
            catch
            {
                Form1.AutoClosingAlram("리밸런싱 매도시간 저장 에러", "에러알림", 10, "동작");
                Log.에러기록("리밸런싱 매도시간 저장 에러");
            }

            try
            {
                int.TryParse(form.MT_rebalance_starttime_A.Text, out int _rebalance_starttime_A);
                int.TryParse(form.MT_rebalance_starttime_B.Text, out int _rebalance_starttime_B);
                int.TryParse(form.MT_rebalance_starttime_C.Text, out int _rebalance_starttime_C);
                int.TryParse(form.MT_rebalance_starttime_D.Text, out int _rebalance_starttime_D);
                int.TryParse(form.MT_rebalance_starttime_E.Text, out int _rebalance_starttime_E);
                int.TryParse(form.MT_rebalance_starttime_F.Text, out int _rebalance_starttime_F);
                int.TryParse(form.MT_rebalance_starttime_G.Text, out int _rebalance_starttime_G);
                int.TryParse(form.MTB_Liquidation_Starttime_A.Text, out int _Liquidation_Starttime_A);
                int.TryParse(form.MTB_Liquidation_Starttime_B.Text, out int _Liquidation_Starttime_B);
                int.TryParse(form.MTB_Liquidation_Starttime_C.Text, out int _Liquidation_Starttime_C);

                int rebalance_starttime_A = GET.Start_stop_time(true, _rebalance_starttime_A);
                int rebalance_starttime_B = GET.Start_stop_time(true, _rebalance_starttime_B);
                int rebalance_starttime_C = GET.Start_stop_time(true, _rebalance_starttime_C);
                int rebalance_starttime_D = GET.Start_stop_time(true, _rebalance_starttime_D);
                int rebalance_starttime_E = GET.Start_stop_time(true, _rebalance_starttime_E);
                int rebalance_starttime_F = GET.Start_stop_time(true, _rebalance_starttime_F);
                int rebalance_starttime_G = GET.Start_stop_time(true, _rebalance_starttime_G);

                int Liquidation_Starttime_A = GET.Start_stop_time(true, _Liquidation_Starttime_A);
                int Liquidation_Starttime_B = GET.Start_stop_time(true, _Liquidation_Starttime_B);
                int Liquidation_Starttime_C = GET.Start_stop_time(true, _Liquidation_Starttime_C);

                GenieConfig.MT_rebalance_starttime_A = rebalance_starttime_A;
                GenieConfig.MT_rebalance_starttime_B = rebalance_starttime_B;
                GenieConfig.MT_rebalance_starttime_C = rebalance_starttime_C;
                GenieConfig.MT_rebalance_starttime_D = rebalance_starttime_D;
                GenieConfig.MT_rebalance_starttime_E = rebalance_starttime_E;
                GenieConfig.MT_rebalance_starttime_F = rebalance_starttime_F;
                GenieConfig.MT_rebalance_starttime_G = rebalance_starttime_G;
                GenieConfig.MTB_Liquidation_Starttime_A = Liquidation_Starttime_A;
                GenieConfig.MTB_Liquidation_Starttime_B = Liquidation_Starttime_B;
                GenieConfig.MTB_Liquidation_Starttime_C = Liquidation_Starttime_C;

                form.MT_rebalance_starttime_A.Text = rebalance_starttime_A.ToString();
                form.MT_rebalance_starttime_B.Text = rebalance_starttime_B.ToString();
                form.MT_rebalance_starttime_C.Text = rebalance_starttime_C.ToString();
                form.MT_rebalance_starttime_D.Text = rebalance_starttime_D.ToString();
                form.MT_rebalance_starttime_E.Text = rebalance_starttime_E.ToString();
                form.MT_rebalance_starttime_F.Text = rebalance_starttime_F.ToString();
                form.MT_rebalance_starttime_G.Text = rebalance_starttime_G.ToString();
                form.MTB_Liquidation_Starttime_A.Text = Liquidation_Starttime_A.ToString();
                form.MTB_Liquidation_Starttime_B.Text = Liquidation_Starttime_B.ToString();
                form.MTB_Liquidation_Starttime_C.Text = Liquidation_Starttime_C.ToString();


                int.TryParse(form.MT_rebalance_stoptime_A.Text, out int _rebalance_stoptime_A);
                int.TryParse(form.MT_rebalance_stoptime_B.Text, out int _rebalance_stoptime_B);
                int.TryParse(form.MT_rebalance_stoptime_C.Text, out int _rebalance_stoptime_C);
                int.TryParse(form.MT_rebalance_stoptime_D.Text, out int _rebalance_stoptime_D);
                int.TryParse(form.MT_rebalance_stoptime_E.Text, out int _rebalance_stoptime_E);
                int.TryParse(form.MT_rebalance_stoptime_F.Text, out int _rebalance_stoptime_F);
                int.TryParse(form.MT_rebalance_stoptime_G.Text, out int _rebalance_stoptime_G);
                int.TryParse(form.MTB_Liquidation_Stoptime_A.Text, out int _Liquidation_Stoptime_A);
                int.TryParse(form.MTB_Liquidation_Stoptime_B.Text, out int _Liquidation_Stoptime_B);
                int.TryParse(form.MTB_Liquidation_Stoptime_C.Text, out int _Liquidation_Stoptime_C);

                int rebalance_stoptime_A = GET.Start_stop_time(false, _rebalance_stoptime_A);
                int rebalance_stoptime_B = GET.Start_stop_time(false, _rebalance_stoptime_B);
                int rebalance_stoptime_C = GET.Start_stop_time(false, _rebalance_stoptime_C);
                int rebalance_stoptime_D = GET.Start_stop_time(false, _rebalance_stoptime_D);
                int rebalance_stoptime_E = GET.Start_stop_time(false, _rebalance_stoptime_E);
                int rebalance_stoptime_F = GET.Start_stop_time(false, _rebalance_stoptime_F);
                int rebalance_stoptime_G = GET.Start_stop_time(false, _rebalance_stoptime_G);
                int Liquidation_Stoptime_A = GET.Start_stop_time(false, _Liquidation_Stoptime_A);
                int Liquidation_Stoptime_B = GET.Start_stop_time(false, _Liquidation_Stoptime_B);
                int Liquidation_Stoptime_C = GET.Start_stop_time(false, _Liquidation_Stoptime_C);

                GenieConfig.MT_rebalance_stoptime_A = rebalance_stoptime_A;
                GenieConfig.MT_rebalance_stoptime_B = rebalance_stoptime_B;
                GenieConfig.MT_rebalance_stoptime_C = rebalance_stoptime_C;
                GenieConfig.MT_rebalance_stoptime_D = rebalance_stoptime_D;
                GenieConfig.MT_rebalance_stoptime_E = rebalance_stoptime_E;
                GenieConfig.MT_rebalance_stoptime_F = rebalance_stoptime_F;
                GenieConfig.MT_rebalance_stoptime_G = rebalance_stoptime_G;
                GenieConfig.MTB_Liquidation_Stoptime_A = Liquidation_Stoptime_A;
                GenieConfig.MTB_Liquidation_Stoptime_B = Liquidation_Stoptime_B;
                GenieConfig.MTB_Liquidation_Stoptime_C = Liquidation_Stoptime_C;

                form.MT_rebalance_stoptime_A.Text = rebalance_stoptime_A.ToString();
                form.MT_rebalance_stoptime_B.Text = rebalance_stoptime_B.ToString();
                form.MT_rebalance_stoptime_C.Text = rebalance_stoptime_C.ToString();
                form.MT_rebalance_stoptime_D.Text = rebalance_stoptime_D.ToString();
                form.MT_rebalance_stoptime_E.Text = rebalance_stoptime_E.ToString();
                form.MT_rebalance_stoptime_F.Text = rebalance_stoptime_F.ToString();
                form.MT_rebalance_stoptime_G.Text = rebalance_stoptime_G.ToString();
                form.MTB_Liquidation_Stoptime_A.Text = Liquidation_Stoptime_A.ToString();
                form.MTB_Liquidation_Stoptime_B.Text = Liquidation_Stoptime_B.ToString();
                form.MTB_Liquidation_Stoptime_C.Text = Liquidation_Stoptime_C.ToString();

            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 시간 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 시간 입력 오류 : " + e.Message);
            }
            
            try
            {
                int.TryParse(form.MTB_rebalance_delay_A.Text, out int _rebalance_delay_A);
                int.TryParse(form.MTB_rebalance_delay_B.Text, out int _rebalance_delay_B);
                int.TryParse(form.MTB_rebalance_delay_C.Text, out int _rebalance_delay_C);
                int.TryParse(form.MTB_rebalance_delay_D.Text, out int _rebalance_delay_D);
                int.TryParse(form.MTB_rebalance_delay_E.Text, out int _rebalance_delay_E);
                int.TryParse(form.MTB_rebalance_delay_F.Text, out int _rebalance_delay_F);
                int.TryParse(form.MTB_rebalance_delay_G.Text, out int _rebalance_delay_G);
                int.TryParse(form.MTB_Liquidation_delay_A.Text, out int _Liquidation_delay_A);
                int.TryParse(form.MTB_Liquidation_delay_B.Text, out int _Liquidation_delay_B);
                int.TryParse(form.MTB_Liquidation_delay_C.Text, out int _Liquidation_delay_C);

                GenieConfig.MTB_rebalance_delay_A = _rebalance_delay_A;
                GenieConfig.MTB_rebalance_delay_B = _rebalance_delay_B;
                GenieConfig.MTB_rebalance_delay_C = _rebalance_delay_C;
                GenieConfig.MTB_rebalance_delay_D = _rebalance_delay_D;
                GenieConfig.MTB_rebalance_delay_E = _rebalance_delay_E;
                GenieConfig.MTB_rebalance_delay_F = _rebalance_delay_F;
                GenieConfig.MTB_rebalance_delay_G = _rebalance_delay_G;
                GenieConfig.MTB_Liquidation_delay_A = _Liquidation_delay_A;
                GenieConfig.MTB_Liquidation_delay_B = _Liquidation_delay_B;
                GenieConfig.MTB_Liquidation_delay_C = _Liquidation_delay_C;

                form.MTB_rebalance_delay_A.Text = _rebalance_delay_A.ToString();
                form.MTB_rebalance_delay_B.Text = _rebalance_delay_B.ToString();
                form.MTB_rebalance_delay_C.Text = _rebalance_delay_C.ToString();
                form.MTB_rebalance_delay_D.Text = _rebalance_delay_D.ToString();
                form.MTB_rebalance_delay_E.Text = _rebalance_delay_E.ToString();
                form.MTB_rebalance_delay_F.Text = _rebalance_delay_F.ToString();
                form.MTB_rebalance_delay_G.Text = _rebalance_delay_G.ToString();
                form.MTB_Liquidation_delay_A.Text = _Liquidation_delay_A.ToString();
                form.MTB_Liquidation_delay_B.Text = _Liquidation_delay_B.ToString();
                form.MTB_Liquidation_delay_C.Text = _Liquidation_delay_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 유지 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 유지 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_rebalance_suik_1_A.Text, out double _rebalance_suik_1_A);
                double.TryParse(form.TB_rebalance_suik_1_B.Text, out double _rebalance_suik_1_B);
                double.TryParse(form.TB_rebalance_suik_1_C.Text, out double _rebalance_suik_1_C);
                double.TryParse(form.TB_rebalance_suik_1_D.Text, out double _rebalance_suik_1_D);
                double.TryParse(form.TB_rebalance_suik_1_E.Text, out double _rebalance_suik_1_E);
                double.TryParse(form.TB_rebalance_suik_1_F.Text, out double _rebalance_suik_1_F);
                double.TryParse(form.TB_rebalance_suik_1_G.Text, out double _rebalance_suik_1_G);
                double.TryParse(form.TB_Liquidation_suik_1_A.Text, out double _Liquidation_suik_1_A);
                double.TryParse(form.TB_Liquidation_suik_1_B.Text, out double _Liquidation_suik_1_B);
                double.TryParse(form.TB_Liquidation_suik_1_C.Text, out double _Liquidation_suik_1_C);

                GenieConfig.TB_rebalance_suik_1_A = _rebalance_suik_1_A;
                GenieConfig.TB_rebalance_suik_1_B = _rebalance_suik_1_B;
                GenieConfig.TB_rebalance_suik_1_C = _rebalance_suik_1_C;
                GenieConfig.TB_rebalance_suik_1_D = _rebalance_suik_1_D;
                GenieConfig.TB_rebalance_suik_1_E = _rebalance_suik_1_E;
                GenieConfig.TB_rebalance_suik_1_F = _rebalance_suik_1_F;
                GenieConfig.TB_rebalance_suik_1_G = _rebalance_suik_1_G;
                GenieConfig.TB_Liquidation_suik_1_A = _Liquidation_suik_1_A;
                GenieConfig.TB_Liquidation_suik_1_B = _Liquidation_suik_1_B;
                GenieConfig.TB_Liquidation_suik_1_C = _Liquidation_suik_1_C;

                form.TB_rebalance_suik_1_A.Text = _rebalance_suik_1_A.ToString();
                form.TB_rebalance_suik_1_B.Text = _rebalance_suik_1_B.ToString();
                form.TB_rebalance_suik_1_C.Text = _rebalance_suik_1_C.ToString();
                form.TB_rebalance_suik_1_D.Text = _rebalance_suik_1_D.ToString();
                form.TB_rebalance_suik_1_E.Text = _rebalance_suik_1_E.ToString();
                form.TB_rebalance_suik_1_F.Text = _rebalance_suik_1_F.ToString();
                form.TB_rebalance_suik_1_G.Text = _rebalance_suik_1_G.ToString();
                form.TB_Liquidation_suik_1_A.Text = _Liquidation_suik_1_A.ToString();
                form.TB_Liquidation_suik_1_B.Text = _Liquidation_suik_1_B.ToString();
                form.TB_Liquidation_suik_1_C.Text = _Liquidation_suik_1_C.ToString();


                double.TryParse(form.TB_rebalance_suik_2_A.Text, out double _rebalance_suik_2_A);
                double.TryParse(form.TB_rebalance_suik_2_B.Text, out double _rebalance_suik_2_B);
                double.TryParse(form.TB_rebalance_suik_2_C.Text, out double _rebalance_suik_2_C);
                double.TryParse(form.TB_rebalance_suik_2_D.Text, out double _rebalance_suik_2_D);
                double.TryParse(form.TB_rebalance_suik_2_E.Text, out double _rebalance_suik_2_E);
                double.TryParse(form.TB_rebalance_suik_2_F.Text, out double _rebalance_suik_2_F);
                double.TryParse(form.TB_rebalance_suik_2_G.Text, out double _rebalance_suik_2_G);
                double.TryParse(form.TB_Liquidation_suik_2_A.Text, out double _Liquidation_suik_2_A);
                double.TryParse(form.TB_Liquidation_suik_2_B.Text, out double _Liquidation_suik_2_B);
                double.TryParse(form.TB_Liquidation_suik_2_C.Text, out double _Liquidation_suik_2_C);

                GenieConfig.TB_rebalance_suik_2_A = _rebalance_suik_2_A;
                GenieConfig.TB_rebalance_suik_2_B = _rebalance_suik_2_B;
                GenieConfig.TB_rebalance_suik_2_C = _rebalance_suik_2_C;
                GenieConfig.TB_rebalance_suik_2_D = _rebalance_suik_2_D;
                GenieConfig.TB_rebalance_suik_2_E = _rebalance_suik_2_E;
                GenieConfig.TB_rebalance_suik_2_F = _rebalance_suik_2_F;
                GenieConfig.TB_rebalance_suik_2_G = _rebalance_suik_2_G;
                GenieConfig.TB_Liquidation_suik_2_A = _Liquidation_suik_2_A;
                GenieConfig.TB_Liquidation_suik_2_B = _Liquidation_suik_2_B;
                GenieConfig.TB_Liquidation_suik_2_C = _Liquidation_suik_2_C;

                form.TB_rebalance_suik_2_A.Text = _rebalance_suik_2_A.ToString();
                form.TB_rebalance_suik_2_B.Text = _rebalance_suik_2_B.ToString();
                form.TB_rebalance_suik_2_C.Text = _rebalance_suik_2_C.ToString();
                form.TB_rebalance_suik_2_D.Text = _rebalance_suik_2_D.ToString();
                form.TB_rebalance_suik_2_E.Text = _rebalance_suik_2_E.ToString();
                form.TB_rebalance_suik_2_F.Text = _rebalance_suik_2_F.ToString();
                form.TB_rebalance_suik_2_G.Text = _rebalance_suik_2_G.ToString();
                form.TB_Liquidation_suik_2_A.Text = _Liquidation_suik_2_A.ToString();
                form.TB_Liquidation_suik_2_B.Text = _Liquidation_suik_2_B.ToString();
                form.TB_Liquidation_suik_2_C.Text = _Liquidation_suik_2_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 수익범위 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 수익범위 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_rebalance_sell_ratio_A.Text, out double _rebalance_sell_ratio_A);
                double.TryParse(form.TB_rebalance_sell_ratio_B.Text, out double _rebalance_sell_ratio_B);
                double.TryParse(form.TB_rebalance_sell_ratio_C.Text, out double _rebalance_sell_ratio_C);
                double.TryParse(form.TB_rebalance_sell_ratio_D.Text, out double _rebalance_sell_ratio_D);
                double.TryParse(form.TB_rebalance_sell_ratio_E.Text, out double _rebalance_sell_ratio_E);
                double.TryParse(form.TB_rebalance_sell_ratio_F.Text, out double _rebalance_sell_ratio_F);
                double.TryParse(form.TB_rebalance_sell_ratio_G.Text, out double _rebalance_sell_ratio_G);
                double.TryParse(form.TB_Liquidation_sell_ratio_A.Text, out double _Liquidation_sell_ratio_A);
                double.TryParse(form.TB_Liquidation_sell_ratio_B.Text, out double _Liquidation_sell_ratio_B);
                double.TryParse(form.TB_Liquidation_sell_ratio_C.Text, out double _Liquidation_sell_ratio_C);

                if (_rebalance_sell_ratio_A == 0) _rebalance_sell_ratio_A = 1;
                if (_rebalance_sell_ratio_B == 0) _rebalance_sell_ratio_B = 1;
                if (_rebalance_sell_ratio_C == 0) _rebalance_sell_ratio_C = 1;
                if (_rebalance_sell_ratio_D == 0) _rebalance_sell_ratio_D = 1;
                if (_rebalance_sell_ratio_E == 0) _rebalance_sell_ratio_E = 1;
                if (_rebalance_sell_ratio_F == 0) _rebalance_sell_ratio_F = 1;
                if (_rebalance_sell_ratio_G == 0) _rebalance_sell_ratio_G = 1;
                if (_Liquidation_sell_ratio_A == 0) _Liquidation_sell_ratio_A = 1;
                if (_Liquidation_sell_ratio_B == 0) _Liquidation_sell_ratio_B = 1;
                if (_Liquidation_sell_ratio_C == 0) _Liquidation_sell_ratio_C = 1;

                GenieConfig.TB_rebalance_sell_ratio_A = Math.Abs(_rebalance_sell_ratio_A);
                GenieConfig.TB_rebalance_sell_ratio_B = Math.Abs(_rebalance_sell_ratio_B);
                GenieConfig.TB_rebalance_sell_ratio_C = Math.Abs(_rebalance_sell_ratio_C);
                GenieConfig.TB_rebalance_sell_ratio_D = Math.Abs(_rebalance_sell_ratio_D);
                GenieConfig.TB_rebalance_sell_ratio_E = Math.Abs(_rebalance_sell_ratio_E);
                GenieConfig.TB_rebalance_sell_ratio_F = Math.Abs(_rebalance_sell_ratio_F);
                GenieConfig.TB_rebalance_sell_ratio_G = Math.Abs(_rebalance_sell_ratio_G);
                GenieConfig.TB_Liquidation_sell_ratio_A = Math.Abs(_Liquidation_sell_ratio_A);
                GenieConfig.TB_Liquidation_sell_ratio_B = Math.Abs(_Liquidation_sell_ratio_B);
                GenieConfig.TB_Liquidation_sell_ratio_C = Math.Abs(_Liquidation_sell_ratio_C);

                form.TB_rebalance_sell_ratio_A.Text = GenieConfig.TB_rebalance_sell_ratio_A.ToString();
                form.TB_rebalance_sell_ratio_B.Text = GenieConfig.TB_rebalance_sell_ratio_B.ToString();
                form.TB_rebalance_sell_ratio_C.Text = GenieConfig.TB_rebalance_sell_ratio_C.ToString();
                form.TB_rebalance_sell_ratio_D.Text = GenieConfig.TB_rebalance_sell_ratio_D.ToString();
                form.TB_rebalance_sell_ratio_E.Text = GenieConfig.TB_rebalance_sell_ratio_E.ToString();
                form.TB_rebalance_sell_ratio_F.Text = GenieConfig.TB_rebalance_sell_ratio_F.ToString();
                form.TB_rebalance_sell_ratio_G.Text = GenieConfig.TB_rebalance_sell_ratio_G.ToString();
                form.TB_Liquidation_sell_ratio_A.Text = GenieConfig.TB_Liquidation_sell_ratio_A.ToString();
                form.TB_Liquidation_sell_ratio_B.Text = GenieConfig.TB_Liquidation_sell_ratio_B.ToString();
                form.TB_Liquidation_sell_ratio_C.Text = GenieConfig.TB_Liquidation_sell_ratio_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 매매비중 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 /  매매비중 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_rebalance_maemae_1_A.Text, out double _rebalance_maemae_1_A);
                double.TryParse(form.TB_rebalance_maemae_1_B.Text, out double _rebalance_maemae_1_B);
                double.TryParse(form.TB_rebalance_maemae_1_C.Text, out double _rebalance_maemae_1_C);
                double.TryParse(form.TB_rebalance_maemae_1_D.Text, out double _rebalance_maemae_1_D);
                double.TryParse(form.TB_rebalance_maemae_1_E.Text, out double _rebalance_maemae_1_E);
                double.TryParse(form.TB_rebalance_maemae_1_F.Text, out double _rebalance_maemae_1_F);
                double.TryParse(form.TB_rebalance_maemae_1_G.Text, out double _rebalance_maemae_1_G);
                double.TryParse(form.TB_Liquidation_maemae_1_A.Text, out double _Liquidation_maemae_1_A);
                double.TryParse(form.TB_Liquidation_maemae_1_B.Text, out double _Liquidation_maemae_1_B);
                double.TryParse(form.TB_Liquidation_maemae_1_C.Text, out double _Liquidation_maemae_1_C);

                GenieConfig.TB_rebalance_maemae_1_A = Math.Abs(_rebalance_maemae_1_A);
                GenieConfig.TB_rebalance_maemae_1_B = Math.Abs(_rebalance_maemae_1_B);
                GenieConfig.TB_rebalance_maemae_1_C = Math.Abs(_rebalance_maemae_1_C);
                GenieConfig.TB_rebalance_maemae_1_D = Math.Abs(_rebalance_maemae_1_D);
                GenieConfig.TB_rebalance_maemae_1_E = Math.Abs(_rebalance_maemae_1_E);
                GenieConfig.TB_rebalance_maemae_1_F = Math.Abs(_rebalance_maemae_1_F);
                GenieConfig.TB_rebalance_maemae_1_G = Math.Abs(_rebalance_maemae_1_G);
                GenieConfig.TB_Liquidation_maemae_1_A = Math.Abs(_Liquidation_maemae_1_A);
                GenieConfig.TB_Liquidation_maemae_1_B = Math.Abs(_Liquidation_maemae_1_B);
                GenieConfig.TB_Liquidation_maemae_1_C = Math.Abs(_Liquidation_maemae_1_C);

                form.TB_rebalance_maemae_1_A.Text = GenieConfig.TB_rebalance_maemae_1_A.ToString();
                form.TB_rebalance_maemae_1_B.Text = GenieConfig.TB_rebalance_maemae_1_B.ToString();
                form.TB_rebalance_maemae_1_C.Text = GenieConfig.TB_rebalance_maemae_1_C.ToString();
                form.TB_rebalance_maemae_1_D.Text = GenieConfig.TB_rebalance_maemae_1_D.ToString();
                form.TB_rebalance_maemae_1_E.Text = GenieConfig.TB_rebalance_maemae_1_E.ToString();
                form.TB_rebalance_maemae_1_F.Text = GenieConfig.TB_rebalance_maemae_1_F.ToString();
                form.TB_rebalance_maemae_1_G.Text = GenieConfig.TB_rebalance_maemae_1_G.ToString();
                form.TB_Liquidation_maemae_1_A.Text = GenieConfig.TB_Liquidation_maemae_1_A.ToString();
                form.TB_Liquidation_maemae_1_B.Text = GenieConfig.TB_Liquidation_maemae_1_B.ToString();
                form.TB_Liquidation_maemae_1_C.Text = GenieConfig.TB_Liquidation_maemae_1_C.ToString();


                double.TryParse(form.TB_rebalance_maemae_2_A.Text, out double _rebalance_maemae_2_A);
                double.TryParse(form.TB_rebalance_maemae_2_B.Text, out double _rebalance_maemae_2_B);
                double.TryParse(form.TB_rebalance_maemae_2_C.Text, out double _rebalance_maemae_2_C);
                double.TryParse(form.TB_rebalance_maemae_2_D.Text, out double _rebalance_maemae_2_D);
                double.TryParse(form.TB_rebalance_maemae_2_E.Text, out double _rebalance_maemae_2_E);
                double.TryParse(form.TB_rebalance_maemae_2_F.Text, out double _rebalance_maemae_2_F);
                double.TryParse(form.TB_rebalance_maemae_2_G.Text, out double _rebalance_maemae_2_G);
                double.TryParse(form.TB_Liquidation_maemae_2_A.Text, out double _Liquidation_maemae_2_A);
                double.TryParse(form.TB_Liquidation_maemae_2_B.Text, out double _Liquidation_maemae_2_B);
                double.TryParse(form.TB_Liquidation_maemae_2_C.Text, out double _Liquidation_maemae_2_C);

                if (_rebalance_maemae_2_A == 0) _rebalance_maemae_2_A = 100;
                if (_rebalance_maemae_2_B == 0) _rebalance_maemae_2_B = 100;
                if (_rebalance_maemae_2_C == 0) _rebalance_maemae_2_C = 100;
                if (_rebalance_maemae_2_D == 0) _rebalance_maemae_2_D = 100;
                if (_rebalance_maemae_2_E == 0) _rebalance_maemae_2_E = 100;
                if (_rebalance_maemae_2_F == 0) _rebalance_maemae_2_F = 100;
                if (_rebalance_maemae_2_G == 0) _rebalance_maemae_2_G = 100;
                if (_Liquidation_maemae_2_A == 0 || _Liquidation_maemae_2_A >= 100) _Liquidation_maemae_2_A = 100;
                if (_Liquidation_maemae_2_B == 0 || _Liquidation_maemae_2_B >= 100) _Liquidation_maemae_2_B = 100;
                if (_Liquidation_maemae_2_C == 0 || _Liquidation_maemae_2_C >= 100) _Liquidation_maemae_2_C = 100;

                GenieConfig.TB_rebalance_maemae_2_A = Math.Abs(_rebalance_maemae_2_A);
                GenieConfig.TB_rebalance_maemae_2_B = Math.Abs(_rebalance_maemae_2_B);
                GenieConfig.TB_rebalance_maemae_2_C = Math.Abs(_rebalance_maemae_2_C);
                GenieConfig.TB_rebalance_maemae_2_D = Math.Abs(_rebalance_maemae_2_D);
                GenieConfig.TB_rebalance_maemae_2_E = Math.Abs(_rebalance_maemae_2_E);
                GenieConfig.TB_rebalance_maemae_2_F = Math.Abs(_rebalance_maemae_2_F);
                GenieConfig.TB_rebalance_maemae_2_G = Math.Abs(_rebalance_maemae_2_G);
                GenieConfig.TB_Liquidation_maemae_2_A = Math.Abs(_Liquidation_maemae_2_A);
                GenieConfig.TB_Liquidation_maemae_2_B = Math.Abs(_Liquidation_maemae_2_B);
                GenieConfig.TB_Liquidation_maemae_2_C = Math.Abs(_Liquidation_maemae_2_C);

                form.TB_rebalance_maemae_2_A.Text = GenieConfig.TB_rebalance_maemae_2_A.ToString();
                form.TB_rebalance_maemae_2_B.Text = GenieConfig.TB_rebalance_maemae_2_B.ToString();
                form.TB_rebalance_maemae_2_C.Text = GenieConfig.TB_rebalance_maemae_2_C.ToString();
                form.TB_rebalance_maemae_2_D.Text = GenieConfig.TB_rebalance_maemae_2_D.ToString();
                form.TB_rebalance_maemae_2_E.Text = GenieConfig.TB_rebalance_maemae_2_E.ToString();
                form.TB_rebalance_maemae_2_F.Text = GenieConfig.TB_rebalance_maemae_2_F.ToString();
                form.TB_rebalance_maemae_2_G.Text = GenieConfig.TB_rebalance_maemae_2_G.ToString();
                form.TB_Liquidation_maemae_2_A.Text = GenieConfig.TB_Liquidation_maemae_2_A.ToString();
                form.TB_Liquidation_maemae_2_B.Text = GenieConfig.TB_Liquidation_maemae_2_B.ToString();
                form.TB_Liquidation_maemae_2_C.Text = GenieConfig.TB_Liquidation_maemae_2_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 매매범위 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 매매범위 입력 오류 : " + e.Message);
            }


            try
            {
                int.TryParse(form.MT_rebalance_repeat_time_A.Text, out int _rebalance_repeat_time_A);
                int.TryParse(form.MT_rebalance_repeat_time_B.Text, out int _rebalance_repeat_time_B);
                int.TryParse(form.MT_rebalance_repeat_time_C.Text, out int _rebalance_repeat_time_C);
                int.TryParse(form.MT_rebalance_repeat_time_D.Text, out int _rebalance_repeat_time_D);
                int.TryParse(form.MT_rebalance_repeat_time_E.Text, out int _rebalance_repeat_time_E);
                int.TryParse(form.MT_rebalance_repeat_time_F.Text, out int _rebalance_repeat_time_F);
                int.TryParse(form.MT_rebalance_repeat_time_G.Text, out int _rebalance_repeat_time_G);
                int.TryParse(form.MT_Liquidation_repeat_time_A.Text, out int _Liquidation_repeat_time_A);
                int.TryParse(form.MT_Liquidation_repeat_time_B.Text, out int _Liquidation_repeat_time_B);
                int.TryParse(form.MT_Liquidation_repeat_time_C.Text, out int _Liquidation_repeat_time_C);

                if (_rebalance_repeat_time_A == 0) _rebalance_repeat_time_A = 30;
                if (_rebalance_repeat_time_B == 0) _rebalance_repeat_time_B = 30;
                if (_rebalance_repeat_time_C == 0) _rebalance_repeat_time_C = 30;
                if (_rebalance_repeat_time_D == 0) _rebalance_repeat_time_D = 30;
                if (_rebalance_repeat_time_E == 0) _rebalance_repeat_time_E = 30;
                if (_rebalance_repeat_time_F == 0) _rebalance_repeat_time_F = 30;
                if (_rebalance_repeat_time_G == 0) _rebalance_repeat_time_G = 30;
                if (_Liquidation_repeat_time_A == 0) _Liquidation_repeat_time_A = 30;
                if (_Liquidation_repeat_time_B == 0) _Liquidation_repeat_time_B = 30;
                if (_Liquidation_repeat_time_C == 0) _Liquidation_repeat_time_C = 30;

                GenieConfig.MT_rebalance_repeat_time_A = _rebalance_repeat_time_A;
                GenieConfig.MT_rebalance_repeat_time_B = _rebalance_repeat_time_B;
                GenieConfig.MT_rebalance_repeat_time_C = _rebalance_repeat_time_C;
                GenieConfig.MT_rebalance_repeat_time_D = _rebalance_repeat_time_D;
                GenieConfig.MT_rebalance_repeat_time_E = _rebalance_repeat_time_E;
                GenieConfig.MT_rebalance_repeat_time_F = _rebalance_repeat_time_F;
                GenieConfig.MT_rebalance_repeat_time_G = _rebalance_repeat_time_G;
                GenieConfig.MT_Liquidation_repeat_time_A = _Liquidation_repeat_time_A;
                GenieConfig.MT_Liquidation_repeat_time_B = _Liquidation_repeat_time_B;
                GenieConfig.MT_Liquidation_repeat_time_C = _Liquidation_repeat_time_C;

                form.MT_rebalance_repeat_time_A.Text = _rebalance_repeat_time_A.ToString();
                form.MT_rebalance_repeat_time_B.Text = _rebalance_repeat_time_B.ToString();
                form.MT_rebalance_repeat_time_C.Text = _rebalance_repeat_time_C.ToString();
                form.MT_rebalance_repeat_time_D.Text = _rebalance_repeat_time_D.ToString();
                form.MT_rebalance_repeat_time_E.Text = _rebalance_repeat_time_E.ToString();
                form.MT_rebalance_repeat_time_F.Text = _rebalance_repeat_time_F.ToString();
                form.MT_rebalance_repeat_time_G.Text = _rebalance_repeat_time_G.ToString();
                form.MT_Liquidation_repeat_time_A.Text = _Liquidation_repeat_time_A.ToString();
                form.MT_Liquidation_repeat_time_B.Text = _Liquidation_repeat_time_B.ToString();
                form.MT_Liquidation_repeat_time_C.Text = _Liquidation_repeat_time_C.ToString();

                // [최적화] Dictionary로 리밸런싱 시간 매핑 (if문 10개 대체)
                var timeLimits = new Dictionary<string, int>()
                {
                    { "리밸_A", _rebalance_repeat_time_A },
                    { "리밸_B", _rebalance_repeat_time_B },
                    { "리밸_C", _rebalance_repeat_time_C },
                    { "리밸_D", _rebalance_repeat_time_D },
                    { "리밸_E", _rebalance_repeat_time_E },
                    { "리밸_F", _rebalance_repeat_time_F },
                    { "리밸_G", _rebalance_repeat_time_G },
                    { "청산_A", _Liquidation_repeat_time_A },
                    { "청산_B", _Liquidation_repeat_time_B },
                    { "청산_C", _Liquidation_repeat_time_C }
                };

                // Active_List(작업 중인 애들)만 돕니다. (Trading_Pool 아님!)
                foreach (var kvp in Form1.Active_List)
                {
                    Trading_item item = kvp.Key; // Key가 객체입니다.

                    // timeLimits에 설정된 제한시간 확인
                    if (timeLimits.TryGetValue(item.Location, out int limitTime))
                    {
                        // 현재 남은 시간이 제한시간보다 길면 -> 강제로 줄임
                        if (item.Timer > limitTime)
                        {
                            item.Timer = limitTime;
                            // 이렇게 바꾸면 위쪽 'while' 루프에서 item.Timer가 줄어든 것을 
                            // 다음 1초 틱에 바로 인지하고 빨리 끝내줍니다.
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 반복시간 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 반복시간 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_rebalance_value_A.Text, out double _rebalance_value_A);
                double.TryParse(form.TB_rebalance_value_B.Text, out double _rebalance_value_B);
                double.TryParse(form.TB_rebalance_value_C.Text, out double _rebalance_value_C);
                double.TryParse(form.TB_rebalance_value_D.Text, out double _rebalance_value_D);
                double.TryParse(form.TB_rebalance_value_E.Text, out double _rebalance_value_E);
                double.TryParse(form.TB_rebalance_value_F.Text, out double _rebalance_value_F);
                double.TryParse(form.TB_rebalance_value_G.Text, out double _rebalance_value_G);
                double.TryParse(form.TB_Liquidation_value_A.Text, out double _Liquidation_value_A);
                double.TryParse(form.TB_Liquidation_value_B.Text, out double _Liquidation_value_B);
                double.TryParse(form.TB_Liquidation_value_C.Text, out double _Liquidation_value_C);

                if (form.combo_rebalance_jumun_A.SelectedIndex < 2) _rebalance_value_A = 0;
                if (form.combo_rebalance_jumun_B.SelectedIndex < 2) _rebalance_value_B = 0;
                if (form.combo_rebalance_jumun_C.SelectedIndex < 2) _rebalance_value_C = 0;
                if (form.combo_rebalance_jumun_D.SelectedIndex < 2) _rebalance_value_D = 0;
                if (form.combo_rebalance_jumun_E.SelectedIndex < 2) _rebalance_value_E = 0;
                if (form.combo_rebalance_jumun_F.SelectedIndex < 2) _rebalance_value_F = 0;
                if (form.combo_rebalance_jumun_G.SelectedIndex < 2) _rebalance_value_G = 0;
                if (form.CBB_Liquidation_jumun_A.SelectedIndex < 2) _Liquidation_value_A = 0;
                if (form.CBB_Liquidation_jumun_B.SelectedIndex < 2) _Liquidation_value_B = 0;
                if (form.CBB_Liquidation_jumun_C.SelectedIndex < 2) _Liquidation_value_C = 0;

                GenieConfig.TB_rebalance_value_A = _rebalance_value_A;
                GenieConfig.TB_rebalance_value_B = _rebalance_value_B;
                GenieConfig.TB_rebalance_value_C = _rebalance_value_C;
                GenieConfig.TB_rebalance_value_D = _rebalance_value_D;
                GenieConfig.TB_rebalance_value_E = _rebalance_value_E;
                GenieConfig.TB_rebalance_value_F = _rebalance_value_F;
                GenieConfig.TB_rebalance_value_G = _rebalance_value_G;
                GenieConfig.TB_Liquidation_value_A = _Liquidation_value_A;
                GenieConfig.TB_Liquidation_value_B = _Liquidation_value_B;
                GenieConfig.TB_Liquidation_value_C = _Liquidation_value_C;

                form.TB_rebalance_value_A.Text = _rebalance_value_A.ToString();
                form.TB_rebalance_value_B.Text = _rebalance_value_B.ToString();
                form.TB_rebalance_value_C.Text = _rebalance_value_C.ToString();
                form.TB_rebalance_value_D.Text = _rebalance_value_D.ToString();
                form.TB_rebalance_value_E.Text = _rebalance_value_E.ToString();
                form.TB_rebalance_value_F.Text = _rebalance_value_F.ToString();
                form.TB_rebalance_value_G.Text = _rebalance_value_G.ToString();
                form.TB_Liquidation_value_A.Text = _Liquidation_value_A.ToString();
                form.TB_Liquidation_value_B.Text = _Liquidation_value_B.ToString();
                form.TB_Liquidation_value_C.Text = _Liquidation_value_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 매매비중 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 매매비중 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_rebalance_Cancel_time_A.Text, out int Cancel_time_A);
                int.TryParse(form.MTB_rebalance_Cancel_time_B.Text, out int Cancel_time_B);
                int.TryParse(form.MTB_rebalance_Cancel_time_C.Text, out int Cancel_time_C);
                int.TryParse(form.MTB_rebalance_Cancel_time_D.Text, out int Cancel_time_D);
                int.TryParse(form.MTB_rebalance_Cancel_time_E.Text, out int Cancel_time_E);
                int.TryParse(form.MTB_rebalance_Cancel_time_F.Text, out int Cancel_time_F);
                int.TryParse(form.MTB_rebalance_Cancel_time_G.Text, out int Cancel_time_G);
                int.TryParse(form.MTB_Liquidation_Cancel_time_A.Text, out int _Liquidation_Cancel_time_A);
                int.TryParse(form.MTB_Liquidation_Cancel_time_B.Text, out int _Liquidation_Cancel_time_B);
                int.TryParse(form.MTB_Liquidation_Cancel_time_C.Text, out int _Liquidation_Cancel_time_C);

                if (Cancel_time_A < 10) Cancel_time_A = 60;
                if (Cancel_time_B < 10) Cancel_time_B = 60;
                if (Cancel_time_C < 10) Cancel_time_C = 60;
                if (Cancel_time_D < 10) Cancel_time_D = 60;
                if (Cancel_time_E < 10) Cancel_time_E = 60;
                if (Cancel_time_F < 10) Cancel_time_F = 60;
                if (Cancel_time_G < 10) Cancel_time_G = 60;
                if (_Liquidation_Cancel_time_A < 10) _Liquidation_Cancel_time_A = 60;
                if (_Liquidation_Cancel_time_B < 10) _Liquidation_Cancel_time_B = 60;
                if (_Liquidation_Cancel_time_C < 10) _Liquidation_Cancel_time_C = 60;

                GenieConfig.MTB_rebalance_Cancel_time_A = Cancel_time_A;
                GenieConfig.MTB_rebalance_Cancel_time_B = Cancel_time_B;
                GenieConfig.MTB_rebalance_Cancel_time_C = Cancel_time_C;
                GenieConfig.MTB_rebalance_Cancel_time_D = Cancel_time_D;
                GenieConfig.MTB_rebalance_Cancel_time_E = Cancel_time_E;
                GenieConfig.MTB_rebalance_Cancel_time_F = Cancel_time_F;
                GenieConfig.MTB_rebalance_Cancel_time_G = Cancel_time_G;
                GenieConfig.MTB_Liquidation_Cancel_time_A = _Liquidation_Cancel_time_A;
                GenieConfig.MTB_Liquidation_Cancel_time_B = _Liquidation_Cancel_time_B;
                GenieConfig.MTB_Liquidation_Cancel_time_C = _Liquidation_Cancel_time_C;

                form.MTB_rebalance_Cancel_time_A.Text = Cancel_time_A.ToString();
                form.MTB_rebalance_Cancel_time_B.Text = Cancel_time_B.ToString();
                form.MTB_rebalance_Cancel_time_C.Text = Cancel_time_C.ToString();
                form.MTB_rebalance_Cancel_time_D.Text = Cancel_time_D.ToString();
                form.MTB_rebalance_Cancel_time_E.Text = Cancel_time_E.ToString();
                form.MTB_rebalance_Cancel_time_F.Text = Cancel_time_F.ToString();
                form.MTB_rebalance_Cancel_time_G.Text = Cancel_time_G.ToString();
                form.MTB_Liquidation_Cancel_time_A.Text = _Liquidation_Cancel_time_A.ToString();
                form.MTB_Liquidation_Cancel_time_B.Text = _Liquidation_Cancel_time_B.ToString();
                form.MTB_Liquidation_Cancel_time_C.Text = _Liquidation_Cancel_time_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 리벨 취소시간 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 리벨 취소시간 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_rebalance_감시_value_A.Text, out double 감시_value_A);
                double.TryParse(form.TB_rebalance_감시_value_B.Text, out double 감시_value_B);
                double.TryParse(form.TB_rebalance_감시_value_C.Text, out double 감시_value_C);
                double.TryParse(form.TB_rebalance_감시_value_D.Text, out double 감시_value_D);
                double.TryParse(form.TB_rebalance_감시_value_E.Text, out double 감시_value_E);
                double.TryParse(form.TB_rebalance_감시_value_F.Text, out double 감시_value_F);
                double.TryParse(form.TB_rebalance_감시_value_G.Text, out double 감시_value_G);

                GenieConfig.TB_rebalance_감시_value_A = 감시_value_A;
                GenieConfig.TB_rebalance_감시_value_B = 감시_value_B;
                GenieConfig.TB_rebalance_감시_value_C = 감시_value_C;
                GenieConfig.TB_rebalance_감시_value_D = 감시_value_D;
                GenieConfig.TB_rebalance_감시_value_E = 감시_value_E;
                GenieConfig.TB_rebalance_감시_value_F = 감시_value_F;
                GenieConfig.TB_rebalance_감시_value_G = 감시_value_G;

                form.TB_rebalance_감시_value_A.Text = 감시_value_A.ToString();
                form.TB_rebalance_감시_value_B.Text = 감시_value_B.ToString();
                form.TB_rebalance_감시_value_C.Text = 감시_value_C.ToString();
                form.TB_rebalance_감시_value_D.Text = 감시_value_D.ToString();
                form.TB_rebalance_감시_value_E.Text = 감시_value_E.ToString();
                form.TB_rebalance_감시_value_F.Text = 감시_value_F.ToString();
                form.TB_rebalance_감시_value_G.Text = 감시_value_G.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 리벨 감시 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 리벨 감시 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_Liquidation_repeat_A.Text, out int _Liquidation_repeat_A);
                int.TryParse(form.MTB_Liquidation_repeat_B.Text, out int _Liquidation_repeat_B);
                int.TryParse(form.MTB_Liquidation_repeat_C.Text, out int _Liquidation_repeat_C);

                if (form.CBB_Liquidation_Cancel_A.SelectedIndex == 0) _Liquidation_repeat_A = 0;
                if (form.CBB_Liquidation_Cancel_B.SelectedIndex == 0) _Liquidation_repeat_B = 0;
                if (form.CBB_Liquidation_Cancel_C.SelectedIndex == 0) _Liquidation_repeat_C = 0;

                GenieConfig.MTB_Liquidation_repeat_A = _Liquidation_repeat_A;
                GenieConfig.MTB_Liquidation_repeat_B = _Liquidation_repeat_B;
                GenieConfig.MTB_Liquidation_repeat_C = _Liquidation_repeat_C;

                form.MTB_Liquidation_repeat_A.Text = _Liquidation_repeat_A.ToString();
                form.MTB_Liquidation_repeat_B.Text = _Liquidation_repeat_B.ToString();
                form.MTB_Liquidation_repeat_C.Text = _Liquidation_repeat_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 청산반복 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 청산반복 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_rebalance_sellratio1_A.Text, out double sellratio1_A);
                double.TryParse(form.TB_rebalance_sellratio1_B.Text, out double sellratio1_B);
                double.TryParse(form.TB_rebalance_sellratio1_C.Text, out double sellratio1_C);
                double.TryParse(form.TB_rebalance_sellratio1_D.Text, out double sellratio1_D);
                double.TryParse(form.TB_rebalance_sellratio1_E.Text, out double sellratio1_E);
                double.TryParse(form.TB_rebalance_sellratio1_F.Text, out double sellratio1_F);
                double.TryParse(form.TB_rebalance_sellratio1_G.Text, out double sellratio1_G);

                // =================================================================
                // [+] 콤보박스 사용 중(SelectedIndex != 0)이면서 비중값이 0일 때, 1.5 또는 -1.5 자동 할당
                // =================================================================
                double 초기값_자동세팅(ComboBox 콤보박스, double 현재비중)
                {
                    if (콤보박스.SelectedIndex != 0 && 현재비중 == 0)
                    {
                        if (콤보박스.SelectedIndex >= 1 && 콤보박스.SelectedIndex <= 6) return 1;
                        if (콤보박스.SelectedIndex >= 7) return -1;
                    }
                    return 현재비중; // 조건에 안 맞으면 원래 읽어온 값 그대로 통과시킵니다.
                }

                sellratio1_A = 초기값_자동세팅(form.CBB_rebalance_1A, sellratio1_A);
                sellratio1_B = 초기값_자동세팅(form.CBB_rebalance_1B, sellratio1_B);
                sellratio1_C = 초기값_자동세팅(form.CBB_rebalance_1C, sellratio1_C);
                sellratio1_D = 초기값_자동세팅(form.CBB_rebalance_1D, sellratio1_D);
                sellratio1_E = 초기값_자동세팅(form.CBB_rebalance_1E, sellratio1_E);
                sellratio1_F = 초기값_자동세팅(form.CBB_rebalance_1F, sellratio1_F);
                sellratio1_G = 초기값_자동세팅(form.CBB_rebalance_1G, sellratio1_G);
                // =================================================================

                GenieConfig.TB_rebalance_sellratio1_A = sellratio1_A;
                GenieConfig.TB_rebalance_sellratio1_B = sellratio1_B;
                GenieConfig.TB_rebalance_sellratio1_C = sellratio1_C;
                GenieConfig.TB_rebalance_sellratio1_D = sellratio1_D;
                GenieConfig.TB_rebalance_sellratio1_E = sellratio1_E;
                GenieConfig.TB_rebalance_sellratio1_F = sellratio1_F;
                GenieConfig.TB_rebalance_sellratio1_G = sellratio1_G;

                form.TB_rebalance_sellratio1_A.Text = GenieConfig.TB_rebalance_sellratio1_A.ToString();
                form.TB_rebalance_sellratio1_B.Text = GenieConfig.TB_rebalance_sellratio1_B.ToString();
                form.TB_rebalance_sellratio1_C.Text = GenieConfig.TB_rebalance_sellratio1_C.ToString();
                form.TB_rebalance_sellratio1_D.Text = GenieConfig.TB_rebalance_sellratio1_D.ToString();
                form.TB_rebalance_sellratio1_E.Text = GenieConfig.TB_rebalance_sellratio1_E.ToString();
                form.TB_rebalance_sellratio1_F.Text = GenieConfig.TB_rebalance_sellratio1_F.ToString();
                form.TB_rebalance_sellratio1_G.Text = GenieConfig.TB_rebalance_sellratio1_G.ToString();

                double.TryParse(form.TB_rebalance_sellratio2_A.Text, out double sellratio2_A);
                double.TryParse(form.TB_rebalance_sellratio2_B.Text, out double sellratio2_B);
                double.TryParse(form.TB_rebalance_sellratio2_C.Text, out double sellratio2_C);
                double.TryParse(form.TB_rebalance_sellratio2_D.Text, out double sellratio2_D);
                double.TryParse(form.TB_rebalance_sellratio2_E.Text, out double sellratio2_E);
                double.TryParse(form.TB_rebalance_sellratio2_F.Text, out double sellratio2_F);
                double.TryParse(form.TB_rebalance_sellratio2_G.Text, out double sellratio2_G);

                GenieConfig.TB_rebalance_sellratio2_A = sellratio2_A;
                GenieConfig.TB_rebalance_sellratio2_B = sellratio2_B;
                GenieConfig.TB_rebalance_sellratio2_C = sellratio2_C;
                GenieConfig.TB_rebalance_sellratio2_D = sellratio2_D;
                GenieConfig.TB_rebalance_sellratio2_E = sellratio2_E;
                GenieConfig.TB_rebalance_sellratio2_F = sellratio2_F;
                GenieConfig.TB_rebalance_sellratio2_G = sellratio2_G;

                form.TB_rebalance_sellratio2_A.Text = GenieConfig.TB_rebalance_sellratio2_A.ToString();
                form.TB_rebalance_sellratio2_B.Text = GenieConfig.TB_rebalance_sellratio2_B.ToString();
                form.TB_rebalance_sellratio2_C.Text = GenieConfig.TB_rebalance_sellratio2_C.ToString();
                form.TB_rebalance_sellratio2_D.Text = GenieConfig.TB_rebalance_sellratio2_D.ToString();
                form.TB_rebalance_sellratio2_E.Text = GenieConfig.TB_rebalance_sellratio2_E.ToString();
                form.TB_rebalance_sellratio2_F.Text = GenieConfig.TB_rebalance_sellratio2_F.ToString();
                form.TB_rebalance_sellratio2_G.Text = GenieConfig.TB_rebalance_sellratio2_G.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 리벨 매도 수익율 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 리벨 매도 수익율 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_rebalance_sellvolume1_A.Text, out double sellvolume1_A);
                double.TryParse(form.TB_rebalance_sellvolume1_B.Text, out double sellvolume1_B);
                double.TryParse(form.TB_rebalance_sellvolume1_C.Text, out double sellvolume1_C);
                double.TryParse(form.TB_rebalance_sellvolume1_D.Text, out double sellvolume1_D);
                double.TryParse(form.TB_rebalance_sellvolume1_E.Text, out double sellvolume1_E);
                double.TryParse(form.TB_rebalance_sellvolume1_F.Text, out double sellvolume1_F);
                double.TryParse(form.TB_rebalance_sellvolume1_G.Text, out double sellvolume1_G);

                if (sellvolume1_A == 0) sellvolume1_A = 10;
                if (sellvolume1_B == 0) sellvolume1_B = 10;
                if (sellvolume1_C == 0) sellvolume1_C = 10;
                if (sellvolume1_D == 0) sellvolume1_D = 10;
                if (sellvolume1_E == 0) sellvolume1_E = 10;
                if (sellvolume1_F == 0) sellvolume1_F = 10;
                if (sellvolume1_G == 0) sellvolume1_G = 10;

                GenieConfig.TB_rebalance_sellvolume1_A = Math.Abs(sellvolume1_A);
                GenieConfig.TB_rebalance_sellvolume1_B = Math.Abs(sellvolume1_B);
                GenieConfig.TB_rebalance_sellvolume1_C = Math.Abs(sellvolume1_C);
                GenieConfig.TB_rebalance_sellvolume1_D = Math.Abs(sellvolume1_D);
                GenieConfig.TB_rebalance_sellvolume1_E = Math.Abs(sellvolume1_E);
                GenieConfig.TB_rebalance_sellvolume1_F = Math.Abs(sellvolume1_F);
                GenieConfig.TB_rebalance_sellvolume1_G = Math.Abs(sellvolume1_G);

                form.TB_rebalance_sellvolume1_A.Text = GenieConfig.TB_rebalance_sellvolume1_A.ToString();
                form.TB_rebalance_sellvolume1_B.Text = GenieConfig.TB_rebalance_sellvolume1_B.ToString();
                form.TB_rebalance_sellvolume1_C.Text = GenieConfig.TB_rebalance_sellvolume1_C.ToString();
                form.TB_rebalance_sellvolume1_D.Text = GenieConfig.TB_rebalance_sellvolume1_D.ToString();
                form.TB_rebalance_sellvolume1_E.Text = GenieConfig.TB_rebalance_sellvolume1_E.ToString();
                form.TB_rebalance_sellvolume1_F.Text = GenieConfig.TB_rebalance_sellvolume1_F.ToString();
                form.TB_rebalance_sellvolume1_G.Text = GenieConfig.TB_rebalance_sellvolume1_G.ToString();

                double.TryParse(form.TB_rebalance_sellvolume2_A.Text, out double sellvolume2_A);
                double.TryParse(form.TB_rebalance_sellvolume2_B.Text, out double sellvolume2_B);
                double.TryParse(form.TB_rebalance_sellvolume2_C.Text, out double sellvolume2_C);
                double.TryParse(form.TB_rebalance_sellvolume2_D.Text, out double sellvolume2_D);
                double.TryParse(form.TB_rebalance_sellvolume2_E.Text, out double sellvolume2_E);
                double.TryParse(form.TB_rebalance_sellvolume2_F.Text, out double sellvolume2_F);
                double.TryParse(form.TB_rebalance_sellvolume2_G.Text, out double sellvolume2_G);

                if (sellvolume2_A == 0) sellvolume2_A = 10;
                if (sellvolume2_B == 0) sellvolume2_B = 10;
                if (sellvolume2_C == 0) sellvolume2_C = 10;
                if (sellvolume2_D == 0) sellvolume2_D = 10;
                if (sellvolume2_E == 0) sellvolume2_E = 10;
                if (sellvolume2_F == 0) sellvolume2_F = 10;
                if (sellvolume2_G == 0) sellvolume2_G = 10;

                GenieConfig.TB_rebalance_sellvolume2_A = Math.Abs(sellvolume2_A);
                GenieConfig.TB_rebalance_sellvolume2_B = Math.Abs(sellvolume2_B);
                GenieConfig.TB_rebalance_sellvolume2_C = Math.Abs(sellvolume2_C);
                GenieConfig.TB_rebalance_sellvolume2_D = Math.Abs(sellvolume2_D);
                GenieConfig.TB_rebalance_sellvolume2_E = Math.Abs(sellvolume2_E);
                GenieConfig.TB_rebalance_sellvolume2_F = Math.Abs(sellvolume2_F);
                GenieConfig.TB_rebalance_sellvolume2_G = Math.Abs(sellvolume2_G);

                form.TB_rebalance_sellvolume2_A.Text = GenieConfig.TB_rebalance_sellvolume2_A.ToString();
                form.TB_rebalance_sellvolume2_B.Text = GenieConfig.TB_rebalance_sellvolume2_B.ToString();
                form.TB_rebalance_sellvolume2_C.Text = GenieConfig.TB_rebalance_sellvolume2_C.ToString();
                form.TB_rebalance_sellvolume2_D.Text = GenieConfig.TB_rebalance_sellvolume2_D.ToString();
                form.TB_rebalance_sellvolume2_E.Text = GenieConfig.TB_rebalance_sellvolume2_E.ToString();
                form.TB_rebalance_sellvolume2_F.Text = GenieConfig.TB_rebalance_sellvolume2_F.ToString();
                form.TB_rebalance_sellvolume2_G.Text = GenieConfig.TB_rebalance_sellvolume2_G.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 리벨 1차매도 비율 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 리벨 1차매도 비율 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_rebalance_sellcancel1_A.Text, out int sellcancel1_A);
                int.TryParse(form.TB_rebalance_sellcancel1_B.Text, out int sellcancel1_B);
                int.TryParse(form.TB_rebalance_sellcancel1_C.Text, out int sellcancel1_C);
                int.TryParse(form.TB_rebalance_sellcancel1_D.Text, out int sellcancel1_D);
                int.TryParse(form.TB_rebalance_sellcancel1_E.Text, out int sellcancel1_E);
                int.TryParse(form.TB_rebalance_sellcancel1_F.Text, out int sellcancel1_F);
                int.TryParse(form.TB_rebalance_sellcancel1_G.Text, out int sellcancel1_G);

                if (sellcancel1_A == 0) sellcancel1_A = 3600;
                if (sellcancel1_B == 0) sellcancel1_B = 3600;
                if (sellcancel1_C == 0) sellcancel1_C = 3600;
                if (sellcancel1_D == 0) sellcancel1_D = 3600;
                if (sellcancel1_E == 0) sellcancel1_E = 3600;
                if (sellcancel1_F == 0) sellcancel1_F = 3600;
                if (sellcancel1_G == 0) sellcancel1_G = 3600;

                GenieConfig.TB_rebalance_sellcancel1_A = Math.Abs(sellcancel1_A);
                GenieConfig.TB_rebalance_sellcancel1_B = Math.Abs(sellcancel1_B);
                GenieConfig.TB_rebalance_sellcancel1_C = Math.Abs(sellcancel1_C);
                GenieConfig.TB_rebalance_sellcancel1_D = Math.Abs(sellcancel1_D);
                GenieConfig.TB_rebalance_sellcancel1_E = Math.Abs(sellcancel1_E);
                GenieConfig.TB_rebalance_sellcancel1_F = Math.Abs(sellcancel1_F);
                GenieConfig.TB_rebalance_sellcancel1_G = Math.Abs(sellcancel1_G);

                form.TB_rebalance_sellcancel1_A.Text = GenieConfig.TB_rebalance_sellcancel1_A.ToString();
                form.TB_rebalance_sellcancel1_B.Text = GenieConfig.TB_rebalance_sellcancel1_B.ToString();
                form.TB_rebalance_sellcancel1_C.Text = GenieConfig.TB_rebalance_sellcancel1_C.ToString();
                form.TB_rebalance_sellcancel1_D.Text = GenieConfig.TB_rebalance_sellcancel1_D.ToString();
                form.TB_rebalance_sellcancel1_E.Text = GenieConfig.TB_rebalance_sellcancel1_E.ToString();
                form.TB_rebalance_sellcancel1_F.Text = GenieConfig.TB_rebalance_sellcancel1_F.ToString();
                form.TB_rebalance_sellcancel1_G.Text = GenieConfig.TB_rebalance_sellcancel1_G.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 리벨 1차매도 취소시간 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 리벨 1차매도 취소시간 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_rebalance_sellcancel2_A.Text, out int sellcancel2_A);
                int.TryParse(form.TB_rebalance_sellcancel2_B.Text, out int sellcancel2_B);
                int.TryParse(form.TB_rebalance_sellcancel2_C.Text, out int sellcancel2_C);
                int.TryParse(form.TB_rebalance_sellcancel2_D.Text, out int sellcancel2_D);
                int.TryParse(form.TB_rebalance_sellcancel2_E.Text, out int sellcancel2_E);
                int.TryParse(form.TB_rebalance_sellcancel2_F.Text, out int sellcancel2_F);
                int.TryParse(form.TB_rebalance_sellcancel2_G.Text, out int sellcancel2_G);

                if (sellcancel2_A == 0) sellcancel2_A = 3600;
                if (sellcancel2_B == 0) sellcancel2_B = 3600;
                if (sellcancel2_C == 0) sellcancel2_C = 3600;
                if (sellcancel2_D == 0) sellcancel2_D = 3600;
                if (sellcancel2_E == 0) sellcancel2_E = 3600;
                if (sellcancel2_F == 0) sellcancel2_F = 3600;
                if (sellcancel2_G == 0) sellcancel2_G = 3600;

                GenieConfig.TB_rebalance_sellcancel2_A = Math.Abs(sellcancel2_A);
                GenieConfig.TB_rebalance_sellcancel2_B = Math.Abs(sellcancel2_B);
                GenieConfig.TB_rebalance_sellcancel2_C = Math.Abs(sellcancel2_C);
                GenieConfig.TB_rebalance_sellcancel2_D = Math.Abs(sellcancel2_D);
                GenieConfig.TB_rebalance_sellcancel2_E = Math.Abs(sellcancel2_E);
                GenieConfig.TB_rebalance_sellcancel2_F = Math.Abs(sellcancel2_F);
                GenieConfig.TB_rebalance_sellcancel2_G = Math.Abs(sellcancel2_G);

                form.TB_rebalance_sellcancel2_A.Text = GenieConfig.TB_rebalance_sellcancel2_A.ToString();
                form.TB_rebalance_sellcancel2_B.Text = GenieConfig.TB_rebalance_sellcancel2_B.ToString();
                form.TB_rebalance_sellcancel2_C.Text = GenieConfig.TB_rebalance_sellcancel2_C.ToString();
                form.TB_rebalance_sellcancel2_D.Text = GenieConfig.TB_rebalance_sellcancel2_D.ToString();
                form.TB_rebalance_sellcancel2_E.Text = GenieConfig.TB_rebalance_sellcancel2_E.ToString();
                form.TB_rebalance_sellcancel2_F.Text = GenieConfig.TB_rebalance_sellcancel2_F.ToString();
                form.TB_rebalance_sellcancel2_G.Text = GenieConfig.TB_rebalance_sellcancel2_G.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 리벨 2차매도 취소시간 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 리벨 2차매도 취소시간 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_Rebalance_매입금_A.Text, out double TB_Rebalance_매입금_A);
                double.TryParse(form.TB_Rebalance_매입금_B.Text, out double TB_Rebalance_매입금_B);
                double.TryParse(form.TB_Rebalance_매입금_C.Text, out double TB_Rebalance_매입금_C);
                double.TryParse(form.TB_Rebalance_매입금_D.Text, out double TB_Rebalance_매입금_D);
                double.TryParse(form.TB_Rebalance_매입금_E.Text, out double TB_Rebalance_매입금_E);
                double.TryParse(form.TB_Rebalance_매입금_F.Text, out double TB_Rebalance_매입금_F);
                double.TryParse(form.TB_Rebalance_매입금_G.Text, out double TB_Rebalance_매입금_G);

                GenieConfig.TB_Rebalance_매입금_A = Math.Abs(TB_Rebalance_매입금_A);
                GenieConfig.TB_Rebalance_매입금_B = Math.Abs(TB_Rebalance_매입금_B);
                GenieConfig.TB_Rebalance_매입금_C = Math.Abs(TB_Rebalance_매입금_C);
                GenieConfig.TB_Rebalance_매입금_D = Math.Abs(TB_Rebalance_매입금_D);
                GenieConfig.TB_Rebalance_매입금_E = Math.Abs(TB_Rebalance_매입금_E);
                GenieConfig.TB_Rebalance_매입금_F = Math.Abs(TB_Rebalance_매입금_F);
                GenieConfig.TB_Rebalance_매입금_G = Math.Abs(TB_Rebalance_매입금_G);

                form.TB_Rebalance_매입금_A.Text = GenieConfig.TB_Rebalance_매입금_A.ToString();
                form.TB_Rebalance_매입금_B.Text = GenieConfig.TB_Rebalance_매입금_B.ToString();
                form.TB_Rebalance_매입금_C.Text = GenieConfig.TB_Rebalance_매입금_C.ToString();
                form.TB_Rebalance_매입금_D.Text = GenieConfig.TB_Rebalance_매입금_D.ToString();
                form.TB_Rebalance_매입금_E.Text = GenieConfig.TB_Rebalance_매입금_E.ToString();
                form.TB_Rebalance_매입금_F.Text = GenieConfig.TB_Rebalance_매입금_F.ToString();
                form.TB_Rebalance_매입금_G.Text = GenieConfig.TB_Rebalance_매입금_G.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 에러: " + e.Message); Log.에러기록("계좌관리_저장 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_rebalance_누적거래량_A.Text.Replace(",", ""), out int TB_rebalance_누적거래량_A);
                int.TryParse(form.TB_rebalance_누적거래량_B.Text.Replace(",", ""), out int TB_rebalance_누적거래량_B);
                int.TryParse(form.TB_rebalance_누적거래량_C.Text.Replace(",", ""), out int TB_rebalance_누적거래량_C);
                int.TryParse(form.TB_rebalance_누적거래량_D.Text.Replace(",", ""), out int TB_rebalance_누적거래량_D);
                int.TryParse(form.TB_rebalance_누적거래량_E.Text.Replace(",", ""), out int TB_rebalance_누적거래량_E);
                int.TryParse(form.TB_rebalance_누적거래량_F.Text.Replace(",", ""), out int TB_rebalance_누적거래량_F);
                int.TryParse(form.TB_rebalance_누적거래량_G.Text.Replace(",", ""), out int TB_rebalance_누적거래량_G);

                GenieConfig.TB_rebalance_누적거래량_A = TB_rebalance_누적거래량_A;
                GenieConfig.TB_rebalance_누적거래량_B = TB_rebalance_누적거래량_B;
                GenieConfig.TB_rebalance_누적거래량_C = TB_rebalance_누적거래량_C;
                GenieConfig.TB_rebalance_누적거래량_D = TB_rebalance_누적거래량_D;
                GenieConfig.TB_rebalance_누적거래량_E = TB_rebalance_누적거래량_E;
                GenieConfig.TB_rebalance_누적거래량_F = TB_rebalance_누적거래량_F;
                GenieConfig.TB_rebalance_누적거래량_G = TB_rebalance_누적거래량_G;

                form.TB_rebalance_누적거래량_A.Text = GenieConfig.TB_rebalance_누적거래량_A.ToString();
                form.TB_rebalance_누적거래량_B.Text = GenieConfig.TB_rebalance_누적거래량_B.ToString();
                form.TB_rebalance_누적거래량_C.Text = GenieConfig.TB_rebalance_누적거래량_C.ToString();
                form.TB_rebalance_누적거래량_D.Text = GenieConfig.TB_rebalance_누적거래량_D.ToString();
                form.TB_rebalance_누적거래량_E.Text = GenieConfig.TB_rebalance_누적거래량_E.ToString();
                form.TB_rebalance_누적거래량_F.Text = GenieConfig.TB_rebalance_누적거래량_F.ToString();
                form.TB_rebalance_누적거래량_G.Text = GenieConfig.TB_rebalance_누적거래량_G.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 에러: " + e.Message); Log.에러기록("계좌관리_저장 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_rebalance_누적거래대금_A.Text.Replace(",", ""), out int TB_rebalance_누적거래대금_A);
                int.TryParse(form.TB_rebalance_누적거래대금_B.Text.Replace(",", ""), out int TB_rebalance_누적거래대금_B);
                int.TryParse(form.TB_rebalance_누적거래대금_C.Text.Replace(",", ""), out int TB_rebalance_누적거래대금_C);
                int.TryParse(form.TB_rebalance_누적거래대금_D.Text.Replace(",", ""), out int TB_rebalance_누적거래대금_D);
                int.TryParse(form.TB_rebalance_누적거래대금_E.Text.Replace(",", ""), out int TB_rebalance_누적거래대금_E);
                int.TryParse(form.TB_rebalance_누적거래대금_F.Text.Replace(",", ""), out int TB_rebalance_누적거래대금_F);
                int.TryParse(form.TB_rebalance_누적거래대금_G.Text.Replace(",", ""), out int TB_rebalance_누적거래대금_G);

                GenieConfig.TB_rebalance_누적거래대금_A = TB_rebalance_누적거래대금_A;
                GenieConfig.TB_rebalance_누적거래대금_B = TB_rebalance_누적거래대금_B;
                GenieConfig.TB_rebalance_누적거래대금_C = TB_rebalance_누적거래대금_C;
                GenieConfig.TB_rebalance_누적거래대금_D = TB_rebalance_누적거래대금_D;
                GenieConfig.TB_rebalance_누적거래대금_E = TB_rebalance_누적거래대금_E;
                GenieConfig.TB_rebalance_누적거래대금_F = TB_rebalance_누적거래대금_F;
                GenieConfig.TB_rebalance_누적거래대금_G = TB_rebalance_누적거래대금_G;

                form.TB_rebalance_누적거래대금_A.Text = GenieConfig.TB_rebalance_누적거래대금_A.ToString();
                form.TB_rebalance_누적거래대금_B.Text = GenieConfig.TB_rebalance_누적거래대금_B.ToString();
                form.TB_rebalance_누적거래대금_C.Text = GenieConfig.TB_rebalance_누적거래대금_C.ToString();
                form.TB_rebalance_누적거래대금_D.Text = GenieConfig.TB_rebalance_누적거래대금_D.ToString();
                form.TB_rebalance_누적거래대금_E.Text = GenieConfig.TB_rebalance_누적거래대금_E.ToString();
                form.TB_rebalance_누적거래대금_F.Text = GenieConfig.TB_rebalance_누적거래대금_F.ToString();
                form.TB_rebalance_누적거래대금_G.Text = GenieConfig.TB_rebalance_누적거래대금_G.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 에러: " + e.Message); Log.에러기록("계좌관리_저장 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_rebalance_MinMAPeriod1_A.Text, out int TB_rebalance_MinMAPeriod1_A);
                int.TryParse(form.TB_rebalance_MinMAPeriod1_B.Text, out int TB_rebalance_MinMAPeriod1_B);
                int.TryParse(form.TB_rebalance_MinMAPeriod1_C.Text, out int TB_rebalance_MinMAPeriod1_C);
                int.TryParse(form.TB_rebalance_MinMAPeriod1_D.Text, out int TB_rebalance_MinMAPeriod1_D);
                int.TryParse(form.TB_rebalance_MinMAPeriod1_E.Text, out int TB_rebalance_MinMAPeriod1_E);
                int.TryParse(form.TB_rebalance_MinMAPeriod1_F.Text, out int TB_rebalance_MinMAPeriod1_F);
                int.TryParse(form.TB_rebalance_MinMAPeriod1_G.Text, out int TB_rebalance_MinMAPeriod1_G);

                if (TB_rebalance_MinMAPeriod1_A == 0) TB_rebalance_MinMAPeriod1_A = 3;
                if (TB_rebalance_MinMAPeriod1_B == 0) TB_rebalance_MinMAPeriod1_B = 3;
                if (TB_rebalance_MinMAPeriod1_C == 0) TB_rebalance_MinMAPeriod1_C = 3;
                if (TB_rebalance_MinMAPeriod1_D == 0) TB_rebalance_MinMAPeriod1_D = 3;
                if (TB_rebalance_MinMAPeriod1_E == 0) TB_rebalance_MinMAPeriod1_E = 3;
                if (TB_rebalance_MinMAPeriod1_F == 0) TB_rebalance_MinMAPeriod1_F = 3;
                if (TB_rebalance_MinMAPeriod1_G == 0) TB_rebalance_MinMAPeriod1_G = 3;

                if (TB_rebalance_MinMAPeriod1_A > 300) TB_rebalance_MinMAPeriod1_A = 300;
                if (TB_rebalance_MinMAPeriod1_B > 300) TB_rebalance_MinMAPeriod1_B = 300;
                if (TB_rebalance_MinMAPeriod1_C > 300) TB_rebalance_MinMAPeriod1_C = 300;
                if (TB_rebalance_MinMAPeriod1_D > 300) TB_rebalance_MinMAPeriod1_D = 300;
                if (TB_rebalance_MinMAPeriod1_E > 300) TB_rebalance_MinMAPeriod1_E = 300;
                if (TB_rebalance_MinMAPeriod1_F > 300) TB_rebalance_MinMAPeriod1_F = 300;
                if (TB_rebalance_MinMAPeriod1_G > 300) TB_rebalance_MinMAPeriod1_G = 300;

                GenieConfig.TB_rebalance_MinMAPeriod1_A = TB_rebalance_MinMAPeriod1_A;
                GenieConfig.TB_rebalance_MinMAPeriod1_B = TB_rebalance_MinMAPeriod1_B;
                GenieConfig.TB_rebalance_MinMAPeriod1_C = TB_rebalance_MinMAPeriod1_C;
                GenieConfig.TB_rebalance_MinMAPeriod1_D = TB_rebalance_MinMAPeriod1_D;
                GenieConfig.TB_rebalance_MinMAPeriod1_E = TB_rebalance_MinMAPeriod1_E;
                GenieConfig.TB_rebalance_MinMAPeriod1_F = TB_rebalance_MinMAPeriod1_F;
                GenieConfig.TB_rebalance_MinMAPeriod1_G = TB_rebalance_MinMAPeriod1_G;

                form.TB_rebalance_MinMAPeriod1_A.Text = GenieConfig.TB_rebalance_MinMAPeriod1_A.ToString();
                form.TB_rebalance_MinMAPeriod1_B.Text = GenieConfig.TB_rebalance_MinMAPeriod1_B.ToString();
                form.TB_rebalance_MinMAPeriod1_C.Text = GenieConfig.TB_rebalance_MinMAPeriod1_C.ToString();
                form.TB_rebalance_MinMAPeriod1_D.Text = GenieConfig.TB_rebalance_MinMAPeriod1_D.ToString();
                form.TB_rebalance_MinMAPeriod1_E.Text = GenieConfig.TB_rebalance_MinMAPeriod1_E.ToString();
                form.TB_rebalance_MinMAPeriod1_F.Text = GenieConfig.TB_rebalance_MinMAPeriod1_F.ToString();
                form.TB_rebalance_MinMAPeriod1_G.Text = GenieConfig.TB_rebalance_MinMAPeriod1_G.ToString();

                GenieConfig.CBB_rebalance_MinMAPeriod1_A = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_A);
                GenieConfig.CBB_rebalance_MinMAPeriod1_B = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_B);
                GenieConfig.CBB_rebalance_MinMAPeriod1_C = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_C);
                GenieConfig.CBB_rebalance_MinMAPeriod1_D = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_D);
                GenieConfig.CBB_rebalance_MinMAPeriod1_E = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_E);
                GenieConfig.CBB_rebalance_MinMAPeriod1_F = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_F);
                GenieConfig.CBB_rebalance_MinMAPeriod1_G = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_G);

                int.TryParse(form.TB_rebalance_MinMAPeriod2_A.Text, out int TB_rebalance_MinMAPeriod2_A);
                int.TryParse(form.TB_rebalance_MinMAPeriod2_B.Text, out int TB_rebalance_MinMAPeriod2_B);
                int.TryParse(form.TB_rebalance_MinMAPeriod2_C.Text, out int TB_rebalance_MinMAPeriod2_C);
                int.TryParse(form.TB_rebalance_MinMAPeriod2_D.Text, out int TB_rebalance_MinMAPeriod2_D);
                int.TryParse(form.TB_rebalance_MinMAPeriod2_E.Text, out int TB_rebalance_MinMAPeriod2_E);
                int.TryParse(form.TB_rebalance_MinMAPeriod2_F.Text, out int TB_rebalance_MinMAPeriod2_F);
                int.TryParse(form.TB_rebalance_MinMAPeriod2_G.Text, out int TB_rebalance_MinMAPeriod2_G);

                if (TB_rebalance_MinMAPeriod2_A == 0) TB_rebalance_MinMAPeriod2_A = 5;
                if (TB_rebalance_MinMAPeriod2_B == 0) TB_rebalance_MinMAPeriod2_B = 5;
                if (TB_rebalance_MinMAPeriod2_C == 0) TB_rebalance_MinMAPeriod2_C = 5;
                if (TB_rebalance_MinMAPeriod2_D == 0) TB_rebalance_MinMAPeriod2_D = 5;
                if (TB_rebalance_MinMAPeriod2_E == 0) TB_rebalance_MinMAPeriod2_E = 5;
                if (TB_rebalance_MinMAPeriod2_F == 0) TB_rebalance_MinMAPeriod2_F = 5;
                if (TB_rebalance_MinMAPeriod2_G == 0) TB_rebalance_MinMAPeriod2_G = 5;

                if (TB_rebalance_MinMAPeriod2_A > 300) TB_rebalance_MinMAPeriod2_A = 300;
                if (TB_rebalance_MinMAPeriod2_B > 300) TB_rebalance_MinMAPeriod2_B = 300;
                if (TB_rebalance_MinMAPeriod2_C > 300) TB_rebalance_MinMAPeriod2_C = 300;
                if (TB_rebalance_MinMAPeriod2_D > 300) TB_rebalance_MinMAPeriod2_D = 300;
                if (TB_rebalance_MinMAPeriod2_E > 300) TB_rebalance_MinMAPeriod2_E = 300;
                if (TB_rebalance_MinMAPeriod2_F > 300) TB_rebalance_MinMAPeriod2_F = 300;
                if (TB_rebalance_MinMAPeriod2_G > 300) TB_rebalance_MinMAPeriod2_G = 300;

                GenieConfig.TB_rebalance_MinMAPeriod2_A = TB_rebalance_MinMAPeriod2_A;
                GenieConfig.TB_rebalance_MinMAPeriod2_B = TB_rebalance_MinMAPeriod2_B;
                GenieConfig.TB_rebalance_MinMAPeriod2_C = TB_rebalance_MinMAPeriod2_C;
                GenieConfig.TB_rebalance_MinMAPeriod2_D = TB_rebalance_MinMAPeriod2_D;
                GenieConfig.TB_rebalance_MinMAPeriod2_E = TB_rebalance_MinMAPeriod2_E;
                GenieConfig.TB_rebalance_MinMAPeriod2_F = TB_rebalance_MinMAPeriod2_F;
                GenieConfig.TB_rebalance_MinMAPeriod2_G = TB_rebalance_MinMAPeriod2_G;

                form.TB_rebalance_MinMAPeriod2_A.Text = GenieConfig.TB_rebalance_MinMAPeriod2_A.ToString();
                form.TB_rebalance_MinMAPeriod2_B.Text = GenieConfig.TB_rebalance_MinMAPeriod2_B.ToString();
                form.TB_rebalance_MinMAPeriod2_C.Text = GenieConfig.TB_rebalance_MinMAPeriod2_C.ToString();
                form.TB_rebalance_MinMAPeriod2_D.Text = GenieConfig.TB_rebalance_MinMAPeriod2_D.ToString();
                form.TB_rebalance_MinMAPeriod2_E.Text = GenieConfig.TB_rebalance_MinMAPeriod2_E.ToString();
                form.TB_rebalance_MinMAPeriod2_F.Text = GenieConfig.TB_rebalance_MinMAPeriod2_F.ToString();
                form.TB_rebalance_MinMAPeriod2_G.Text = GenieConfig.TB_rebalance_MinMAPeriod2_G.ToString();

                GenieConfig.CBB_rebalance_MinMAPeriod2_A = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod2_A);
                GenieConfig.CBB_rebalance_MinMAPeriod2_B = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod2_B);
                GenieConfig.CBB_rebalance_MinMAPeriod2_C = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod2_C);
                GenieConfig.CBB_rebalance_MinMAPeriod2_D = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod2_D);
                GenieConfig.CBB_rebalance_MinMAPeriod2_E = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod2_E);
                GenieConfig.CBB_rebalance_MinMAPeriod2_F = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod2_F);
                GenieConfig.CBB_rebalance_MinMAPeriod2_G = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod2_G);

                GenieConfig.CBB_rebalance_MinMAPeriod1_배열_A = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_배열_A);
                GenieConfig.CBB_rebalance_MinMAPeriod1_배열_B = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_배열_B);
                GenieConfig.CBB_rebalance_MinMAPeriod1_배열_C = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_배열_C);
                GenieConfig.CBB_rebalance_MinMAPeriod1_배열_D = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_배열_D);
                GenieConfig.CBB_rebalance_MinMAPeriod1_배열_E = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_배열_E);
                GenieConfig.CBB_rebalance_MinMAPeriod1_배열_F = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_배열_F);
                GenieConfig.CBB_rebalance_MinMAPeriod1_배열_G = GET.ComboBoxIndex(form.CBB_rebalance_MinMAPeriod1_배열_G);

                int.TryParse(form.TB_rebalance_DayMAPeriod1_A.Text, out int TB_rebalance_DayMAPeriod1_A);
                int.TryParse(form.TB_rebalance_DayMAPeriod1_B.Text, out int TB_rebalance_DayMAPeriod1_B);
                int.TryParse(form.TB_rebalance_DayMAPeriod1_C.Text, out int TB_rebalance_DayMAPeriod1_C);
                int.TryParse(form.TB_rebalance_DayMAPeriod1_D.Text, out int TB_rebalance_DayMAPeriod1_D);
                int.TryParse(form.TB_rebalance_DayMAPeriod1_E.Text, out int TB_rebalance_DayMAPeriod1_E);
                int.TryParse(form.TB_rebalance_DayMAPeriod1_F.Text, out int TB_rebalance_DayMAPeriod1_F);
                int.TryParse(form.TB_rebalance_DayMAPeriod1_G.Text, out int TB_rebalance_DayMAPeriod1_G);

                if (TB_rebalance_DayMAPeriod1_A == 0) TB_rebalance_DayMAPeriod1_A = 5;
                if (TB_rebalance_DayMAPeriod1_B == 0) TB_rebalance_DayMAPeriod1_B = 5;
                if (TB_rebalance_DayMAPeriod1_C == 0) TB_rebalance_DayMAPeriod1_C = 5;
                if (TB_rebalance_DayMAPeriod1_D == 0) TB_rebalance_DayMAPeriod1_D = 5;
                if (TB_rebalance_DayMAPeriod1_E == 0) TB_rebalance_DayMAPeriod1_E = 5;
                if (TB_rebalance_DayMAPeriod1_F == 0) TB_rebalance_DayMAPeriod1_F = 5;
                if (TB_rebalance_DayMAPeriod1_G == 0) TB_rebalance_DayMAPeriod1_G = 5;

                if (TB_rebalance_DayMAPeriod1_A > 300) TB_rebalance_DayMAPeriod1_A = 300;
                if (TB_rebalance_DayMAPeriod1_B > 300) TB_rebalance_DayMAPeriod1_B = 300;
                if (TB_rebalance_DayMAPeriod1_C > 300) TB_rebalance_DayMAPeriod1_C = 300;
                if (TB_rebalance_DayMAPeriod1_D > 300) TB_rebalance_DayMAPeriod1_D = 300;
                if (TB_rebalance_DayMAPeriod1_E > 300) TB_rebalance_DayMAPeriod1_E = 300;
                if (TB_rebalance_DayMAPeriod1_F > 300) TB_rebalance_DayMAPeriod1_F = 300;
                if (TB_rebalance_DayMAPeriod1_G > 300) TB_rebalance_DayMAPeriod1_G = 300;

                GenieConfig.TB_rebalance_DayMAPeriod1_A = TB_rebalance_DayMAPeriod1_A;
                GenieConfig.TB_rebalance_DayMAPeriod1_B = TB_rebalance_DayMAPeriod1_B;
                GenieConfig.TB_rebalance_DayMAPeriod1_C = TB_rebalance_DayMAPeriod1_C;
                GenieConfig.TB_rebalance_DayMAPeriod1_D = TB_rebalance_DayMAPeriod1_D;
                GenieConfig.TB_rebalance_DayMAPeriod1_E = TB_rebalance_DayMAPeriod1_E;
                GenieConfig.TB_rebalance_DayMAPeriod1_F = TB_rebalance_DayMAPeriod1_F;
                GenieConfig.TB_rebalance_DayMAPeriod1_G = TB_rebalance_DayMAPeriod1_G;

                form.TB_rebalance_DayMAPeriod1_A.Text = GenieConfig.TB_rebalance_DayMAPeriod1_A.ToString();
                form.TB_rebalance_DayMAPeriod1_B.Text = GenieConfig.TB_rebalance_DayMAPeriod1_B.ToString();
                form.TB_rebalance_DayMAPeriod1_C.Text = GenieConfig.TB_rebalance_DayMAPeriod1_C.ToString();
                form.TB_rebalance_DayMAPeriod1_D.Text = GenieConfig.TB_rebalance_DayMAPeriod1_D.ToString();
                form.TB_rebalance_DayMAPeriod1_E.Text = GenieConfig.TB_rebalance_DayMAPeriod1_E.ToString();
                form.TB_rebalance_DayMAPeriod1_F.Text = GenieConfig.TB_rebalance_DayMAPeriod1_F.ToString();
                form.TB_rebalance_DayMAPeriod1_G.Text = GenieConfig.TB_rebalance_DayMAPeriod1_G.ToString();

                GenieConfig.CBB_rebalance_DayMAPeriod1_A = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod1_A);
                GenieConfig.CBB_rebalance_DayMAPeriod1_B = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod1_B);
                GenieConfig.CBB_rebalance_DayMAPeriod1_C = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod1_C);
                GenieConfig.CBB_rebalance_DayMAPeriod1_D = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod1_D);
                GenieConfig.CBB_rebalance_DayMAPeriod1_E = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod1_E);
                GenieConfig.CBB_rebalance_DayMAPeriod1_F = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod1_F);
                GenieConfig.CBB_rebalance_DayMAPeriod1_G = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod1_G);

                int.TryParse(form.TB_rebalance_DayMAPeriod2_A.Text, out int TB_rebalance_DayMAPeriod2_A);
                int.TryParse(form.TB_rebalance_DayMAPeriod2_B.Text, out int TB_rebalance_DayMAPeriod2_B);
                int.TryParse(form.TB_rebalance_DayMAPeriod2_C.Text, out int TB_rebalance_DayMAPeriod2_C);
                int.TryParse(form.TB_rebalance_DayMAPeriod2_D.Text, out int TB_rebalance_DayMAPeriod2_D);
                int.TryParse(form.TB_rebalance_DayMAPeriod2_E.Text, out int TB_rebalance_DayMAPeriod2_E);
                int.TryParse(form.TB_rebalance_DayMAPeriod2_F.Text, out int TB_rebalance_DayMAPeriod2_F);
                int.TryParse(form.TB_rebalance_DayMAPeriod2_G.Text, out int TB_rebalance_DayMAPeriod2_G);

                if (TB_rebalance_DayMAPeriod2_A == 0) TB_rebalance_DayMAPeriod2_A = 20;
                if (TB_rebalance_DayMAPeriod2_B == 0) TB_rebalance_DayMAPeriod2_B = 20;
                if (TB_rebalance_DayMAPeriod2_C == 0) TB_rebalance_DayMAPeriod2_C = 20;
                if (TB_rebalance_DayMAPeriod2_D == 0) TB_rebalance_DayMAPeriod2_D = 20;
                if (TB_rebalance_DayMAPeriod2_E == 0) TB_rebalance_DayMAPeriod2_E = 20;
                if (TB_rebalance_DayMAPeriod2_F == 0) TB_rebalance_DayMAPeriod2_F = 20;
                if (TB_rebalance_DayMAPeriod2_G == 0) TB_rebalance_DayMAPeriod2_G = 20;

                if (TB_rebalance_DayMAPeriod2_A > 300) TB_rebalance_DayMAPeriod2_A = 300;
                if (TB_rebalance_DayMAPeriod2_B > 300) TB_rebalance_DayMAPeriod2_B = 300;
                if (TB_rebalance_DayMAPeriod2_C > 300) TB_rebalance_DayMAPeriod2_C = 300;
                if (TB_rebalance_DayMAPeriod2_D > 300) TB_rebalance_DayMAPeriod2_D = 300;
                if (TB_rebalance_DayMAPeriod2_E > 300) TB_rebalance_DayMAPeriod2_E = 300;
                if (TB_rebalance_DayMAPeriod2_F > 300) TB_rebalance_DayMAPeriod2_F = 300;
                if (TB_rebalance_DayMAPeriod2_G > 300) TB_rebalance_DayMAPeriod2_G = 300;

                GenieConfig.TB_rebalance_DayMAPeriod2_A = TB_rebalance_DayMAPeriod2_A;
                GenieConfig.TB_rebalance_DayMAPeriod2_B = TB_rebalance_DayMAPeriod2_B;
                GenieConfig.TB_rebalance_DayMAPeriod2_C = TB_rebalance_DayMAPeriod2_C;
                GenieConfig.TB_rebalance_DayMAPeriod2_D = TB_rebalance_DayMAPeriod2_D;
                GenieConfig.TB_rebalance_DayMAPeriod2_E = TB_rebalance_DayMAPeriod2_E;
                GenieConfig.TB_rebalance_DayMAPeriod2_F = TB_rebalance_DayMAPeriod2_F;
                GenieConfig.TB_rebalance_DayMAPeriod2_G = TB_rebalance_DayMAPeriod2_G;

                form.TB_rebalance_DayMAPeriod2_A.Text = GenieConfig.TB_rebalance_DayMAPeriod2_A.ToString();
                form.TB_rebalance_DayMAPeriod2_B.Text = GenieConfig.TB_rebalance_DayMAPeriod2_B.ToString();
                form.TB_rebalance_DayMAPeriod2_C.Text = GenieConfig.TB_rebalance_DayMAPeriod2_C.ToString();
                form.TB_rebalance_DayMAPeriod2_D.Text = GenieConfig.TB_rebalance_DayMAPeriod2_D.ToString();
                form.TB_rebalance_DayMAPeriod2_E.Text = GenieConfig.TB_rebalance_DayMAPeriod2_E.ToString();
                form.TB_rebalance_DayMAPeriod2_F.Text = GenieConfig.TB_rebalance_DayMAPeriod2_F.ToString();
                form.TB_rebalance_DayMAPeriod2_G.Text = GenieConfig.TB_rebalance_DayMAPeriod2_G.ToString();

                GenieConfig.CBB_rebalance_DayMAPeriod2_A = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod2_A);
                GenieConfig.CBB_rebalance_DayMAPeriod2_B = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod2_B);
                GenieConfig.CBB_rebalance_DayMAPeriod2_C = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod2_C);
                GenieConfig.CBB_rebalance_DayMAPeriod2_D = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod2_D);
                GenieConfig.CBB_rebalance_DayMAPeriod2_E = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod2_E);
                GenieConfig.CBB_rebalance_DayMAPeriod2_F = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod2_F);
                GenieConfig.CBB_rebalance_DayMAPeriod2_G = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod2_G);

                GenieConfig.CBB_rebalance_DayMAPeriod_배열_A = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod_배열_A);
                GenieConfig.CBB_rebalance_DayMAPeriod_배열_B = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod_배열_B);
                GenieConfig.CBB_rebalance_DayMAPeriod_배열_C = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod_배열_C);
                GenieConfig.CBB_rebalance_DayMAPeriod_배열_D = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod_배열_D);
                GenieConfig.CBB_rebalance_DayMAPeriod_배열_E = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod_배열_E);
                GenieConfig.CBB_rebalance_DayMAPeriod_배열_F = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod_배열_F);
                GenieConfig.CBB_rebalance_DayMAPeriod_배열_G = GET.ComboBoxIndex(form.CBB_rebalance_DayMAPeriod_배열_G);
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 실현손익대비관리 시간입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 실현손익대비관리 시간입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_cut_time_A.Text, out int cut_time_A);
                int.TryParse(form.MTB_cut_time_B.Text, out int cut_time_B);
                int.TryParse(form.MTB_cut_time_C.Text, out int cut_time_C);

                if (cut_time_A == 0) cut_time_A = 151000;
                if (cut_time_B == 0) cut_time_B = 151000;
                if (cut_time_C == 0) cut_time_C = 151000;

                if (cut_time_A > 151900) cut_time_A = 151000;
                if (cut_time_B > 151900) cut_time_B = 151000;
                if (cut_time_C > 151900) cut_time_C = 151000;

                GenieConfig.MTB_cut_time_A = cut_time_A;
                GenieConfig.MTB_cut_time_B = cut_time_B;
                GenieConfig.MTB_cut_time_C = cut_time_C;

                form.MTB_cut_time_A.Text = cut_time_A.ToString();
                form.MTB_cut_time_B.Text = cut_time_B.ToString();
                form.MTB_cut_time_C.Text = cut_time_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 실현손익대비관리 시간입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 실현손익대비관리 시간입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_cut_수익금1_A.Text, out double cut_수익금1_A);
                double.TryParse(form.TB_cut_수익금1_B.Text, out double cut_수익금1_B);
                double.TryParse(form.TB_cut_수익금1_C.Text, out double cut_수익금1_C);

                if (cut_수익금1_A == 0) cut_수익금1_A = 10;
                if (cut_수익금1_B == 0) cut_수익금1_B = 20;
                if (cut_수익금1_C == 0) cut_수익금1_C = 30;

                GenieConfig.TB_cut_수익금1_A = Math.Abs(cut_수익금1_A);
                GenieConfig.TB_cut_수익금1_B = Math.Abs(cut_수익금1_B);
                GenieConfig.TB_cut_수익금1_C = Math.Abs(cut_수익금1_C);

                form.TB_cut_수익금1_A.Text = GenieConfig.TB_cut_수익금1_A.ToString();
                form.TB_cut_수익금1_B.Text = GenieConfig.TB_cut_수익금1_B.ToString();
                form.TB_cut_수익금1_C.Text = GenieConfig.TB_cut_수익금1_C.ToString();

                double.TryParse(form.TB_cut_수익금2_A.Text, out double cut_수익금2_A);
                double.TryParse(form.TB_cut_수익금2_B.Text, out double cut_수익금2_B);
                double.TryParse(form.TB_cut_수익금2_C.Text, out double cut_수익금2_C);

                if (cut_수익금2_A == 0) cut_수익금2_A = 20;
                if (cut_수익금2_B == 0) cut_수익금2_B = 30;
                if (cut_수익금2_C == 0) cut_수익금2_C = 100;

                GenieConfig.TB_cut_수익금2_A = Math.Abs(cut_수익금2_A);
                GenieConfig.TB_cut_수익금2_B = Math.Abs(cut_수익금2_B);
                GenieConfig.TB_cut_수익금2_C = Math.Abs(cut_수익금2_C);

                form.TB_cut_수익금2_A.Text = GenieConfig.TB_cut_수익금2_A.ToString();
                form.TB_cut_수익금2_B.Text = GenieConfig.TB_cut_수익금2_B.ToString();
                form.TB_cut_수익금2_C.Text = GenieConfig.TB_cut_수익금2_C.ToString();

                double.TryParse(form.TB_cut_남길퍼_A.Text, out double cut_남길퍼_A);
                double.TryParse(form.TB_cut_남길퍼_B.Text, out double cut_남길퍼_B);
                double.TryParse(form.TB_cut_남길퍼_C.Text, out double cut_남길퍼_C);

                if (cut_남길퍼_A == 0) cut_남길퍼_A = 0;
                if (cut_남길퍼_B == 0) cut_남길퍼_B = 0;
                if (cut_남길퍼_C == 0) cut_남길퍼_C = 0;

                GenieConfig.TB_cut_남길퍼_A = Math.Abs(cut_남길퍼_A);
                GenieConfig.TB_cut_남길퍼_B = Math.Abs(cut_남길퍼_B);
                GenieConfig.TB_cut_남길퍼_C = Math.Abs(cut_남길퍼_C);

                form.TB_cut_남길퍼_A.Text = GenieConfig.TB_cut_남길퍼_A.ToString();
                form.TB_cut_남길퍼_B.Text = GenieConfig.TB_cut_남길퍼_B.ToString();
                form.TB_cut_남길퍼_C.Text = GenieConfig.TB_cut_남길퍼_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 실현손익대비관리 수익금 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 실현손익대비관리 수익금 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_cut_P_A.Text, out double cut_P_A);
                double.TryParse(form.TB_cut_P_B.Text, out double cut_P_B);
                double.TryParse(form.TB_cut_P_C.Text, out double cut_P_C);

                if (cut_P_A == 0) cut_P_A = -30;
                if (cut_P_B == 0) cut_P_B = -30;
                if (cut_P_C == 0) cut_P_C = -30;

                GenieConfig.TB_cut_P_A = cut_P_A;
                GenieConfig.TB_cut_P_B = cut_P_B;
                GenieConfig.TB_cut_P_C = cut_P_C;

                form.TB_cut_P_A.Text = cut_P_A.ToString();
                form.TB_cut_P_B.Text = cut_P_B.ToString();
                form.TB_cut_P_C.Text = cut_P_C.ToString();

            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 수익률 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 수익률 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_cut_won_A.Text, out double won_A);
                double.TryParse(form.TB_cut_won_B.Text, out double won_B);
                double.TryParse(form.TB_cut_won_C.Text, out double won_C);

                GenieConfig.TB_cut_won_A = Math.Abs(won_A);
                GenieConfig.TB_cut_won_B = Math.Abs(won_B);
                GenieConfig.TB_cut_won_C = Math.Abs(won_C);

                form.TB_cut_won_A.Text = GenieConfig.TB_cut_won_A.ToString();
                form.TB_cut_won_B.Text = GenieConfig.TB_cut_won_B.ToString();
                form.TB_cut_won_C.Text = GenieConfig.TB_cut_won_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 매입금 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 매입금 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_cut_ratio_A.Text, out double ratio_A);
                double.TryParse(form.TB_cut_ratio_B.Text, out double ratio_B);
                double.TryParse(form.TB_cut_ratio_C.Text, out double ratio_C);

                if (ratio_A == 0) ratio_A = 10;
                if (ratio_B == 0) ratio_B = 10;
                if (ratio_C == 0) ratio_C = 10;

                GenieConfig.TB_cut_ratio_A = Math.Abs(ratio_A);
                GenieConfig.TB_cut_ratio_B = Math.Abs(ratio_B);
                GenieConfig.TB_cut_ratio_C = Math.Abs(ratio_C);

                form.TB_cut_ratio_A.Text = GenieConfig.TB_cut_ratio_A.ToString();
                form.TB_cut_ratio_B.Text = GenieConfig.TB_cut_ratio_B.ToString();
                form.TB_cut_ratio_C.Text = GenieConfig.TB_cut_ratio_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 매도비율 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 매도비율 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_cut_value_A.Text, out double cut_value_A);
                double.TryParse(form.TB_cut_value_B.Text, out double cut_value_B);
                double.TryParse(form.TB_cut_value_C.Text, out double cut_value_C);

                if (form.CBB_cut_jumun_A.SelectedIndex == 0 || form.CBB_cut_jumun_A.SelectedIndex == 1) cut_value_A = 0;
                if (form.CBB_cut_jumun_B.SelectedIndex == 0 || form.CBB_cut_jumun_B.SelectedIndex == 1) cut_value_B = 0;
                if (form.CBB_cut_jumun_C.SelectedIndex == 0 || form.CBB_cut_jumun_C.SelectedIndex == 1) cut_value_C = 0;

                GenieConfig.TB_cut_value_A = cut_value_A;
                GenieConfig.TB_cut_value_B = cut_value_B;
                GenieConfig.TB_cut_value_C = cut_value_C;

                form.TB_cut_value_A.Text = cut_value_A.ToString();
                form.TB_cut_value_B.Text = cut_value_B.ToString();
                form.TB_cut_value_C.Text = cut_value_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 매도비율 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 매도비율 입력 오류 : " + e.Message);
            }


            try
            {
                int.TryParse(form.MTB_cut_cansel_time_A.Text, out int time_A);
                int.TryParse(form.MTB_cut_cansel_time_B.Text, out int time_B);
                int.TryParse(form.MTB_cut_cansel_time_C.Text, out int time_C);

                if (time_A == 0) time_A = 30;
                if (time_B == 0) time_B = 30;
                if (time_C == 0) time_C = 30;

                GenieConfig.MTB_cut_cansel_time_A = time_A;
                GenieConfig.MTB_cut_cansel_time_B = time_B;
                GenieConfig.MTB_cut_cansel_time_C = time_C;

                form.MTB_cut_cansel_time_A.Text = time_A.ToString();
                form.MTB_cut_cansel_time_B.Text = time_B.ToString();
                form.MTB_cut_cansel_time_C.Text = time_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 취소시간 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 취소시간 입력 오류 : " + e.Message);
            }

            GenieConfig.CB_rebalance_매도체크_A = form.CB_rebalance_매도체크_A.Checked;
            GenieConfig.CB_rebalance_매도체크_B = form.CB_rebalance_매도체크_B.Checked;
            GenieConfig.CB_rebalance_매도체크_C = form.CB_rebalance_매도체크_C.Checked;
            GenieConfig.CB_rebalance_매도체크_D = form.CB_rebalance_매도체크_D.Checked;
            GenieConfig.CB_rebalance_매도체크_E = form.CB_rebalance_매도체크_E.Checked;
            GenieConfig.CB_rebalance_매도체크_F = form.CB_rebalance_매도체크_F.Checked;
            GenieConfig.CB_rebalance_매도체크_G = form.CB_rebalance_매도체크_G.Checked;

            if (form.combo_rebalance_use_condition_A.SelectedIndex == 0) { form.리밸_A.SelectedIndex = 0; Form1.위치별검색식리스트["리밸_A"].이름 = ""; }
            if (form.combo_rebalance_use_condition_B.SelectedIndex == 0) { form.리밸_B.SelectedIndex = 0; Form1.위치별검색식리스트["리밸_B"].이름 = ""; }
            if (form.combo_rebalance_use_condition_C.SelectedIndex == 0) { form.리밸_C.SelectedIndex = 0; Form1.위치별검색식리스트["리밸_C"].이름 = ""; }
            if (form.combo_rebalance_use_condition_D.SelectedIndex == 0) { form.리밸_D.SelectedIndex = 0; Form1.위치별검색식리스트["리밸_D"].이름 = ""; }
            if (form.combo_rebalance_use_condition_E.SelectedIndex == 0) { form.리밸_E.SelectedIndex = 0; Form1.위치별검색식리스트["리밸_E"].이름 = ""; }
            if (form.combo_rebalance_use_condition_F.SelectedIndex == 0) { form.리밸_F.SelectedIndex = 0; Form1.위치별검색식리스트["리밸_F"].이름 = ""; }
            if (form.combo_rebalance_use_condition_G.SelectedIndex == 0) { form.리밸_G.SelectedIndex = 0; Form1.위치별검색식리스트["리밸_G"].이름 = ""; }

            GenieConfig.combo_rebalance_use_condition_A = GET.ComboBoxIndex(form.combo_rebalance_use_condition_A);
            GenieConfig.combo_rebalance_use_condition_B = GET.ComboBoxIndex(form.combo_rebalance_use_condition_B);
            GenieConfig.combo_rebalance_use_condition_C = GET.ComboBoxIndex(form.combo_rebalance_use_condition_C);
            GenieConfig.combo_rebalance_use_condition_D = GET.ComboBoxIndex(form.combo_rebalance_use_condition_D);
            GenieConfig.combo_rebalance_use_condition_E = GET.ComboBoxIndex(form.combo_rebalance_use_condition_E);
            GenieConfig.combo_rebalance_use_condition_F = GET.ComboBoxIndex(form.combo_rebalance_use_condition_F);
            GenieConfig.combo_rebalance_use_condition_G = GET.ComboBoxIndex(form.combo_rebalance_use_condition_G);

            if (GenieConfig.combo_rebalance_use_condition_A == 0) Condition_Management.Catch_Stock_List_Clear("리밸_A");
            if (GenieConfig.combo_rebalance_use_condition_B == 0) Condition_Management.Catch_Stock_List_Clear("리밸_B");
            if (GenieConfig.combo_rebalance_use_condition_C == 0) Condition_Management.Catch_Stock_List_Clear("리밸_C");
            if (GenieConfig.combo_rebalance_use_condition_D == 0) Condition_Management.Catch_Stock_List_Clear("리밸_D");
            if (GenieConfig.combo_rebalance_use_condition_E == 0) Condition_Management.Catch_Stock_List_Clear("리밸_E");
            if (GenieConfig.combo_rebalance_use_condition_F == 0) Condition_Management.Catch_Stock_List_Clear("리밸_F");
            if (GenieConfig.combo_rebalance_use_condition_G == 0) Condition_Management.Catch_Stock_List_Clear("리밸_G");

            GenieConfig.CB_rebalance_A = form.CB_rebalance_A.Checked;
            GenieConfig.CB_rebalance_B = form.CB_rebalance_B.Checked;
            GenieConfig.CB_rebalance_C = form.CB_rebalance_C.Checked;
            GenieConfig.CB_rebalance_D = form.CB_rebalance_D.Checked;
            GenieConfig.CB_rebalance_E = form.CB_rebalance_E.Checked;
            GenieConfig.CB_rebalance_F = form.CB_rebalance_F.Checked;
            GenieConfig.CB_rebalance_G = form.CB_rebalance_G.Checked;

            if (form.CBB_Liquidation_use_condition_A.SelectedIndex == 0) { form.청산_A.SelectedIndex = 0; Form1.위치별검색식리스트["청산_A"].이름 = ""; }
            if (form.CBB_Liquidation_use_condition_B.SelectedIndex == 0) { form.청산_B.SelectedIndex = 0; Form1.위치별검색식리스트["청산_B"].이름 = ""; }
            if (form.CBB_Liquidation_use_condition_C.SelectedIndex == 0) { form.청산_C.SelectedIndex = 0; Form1.위치별검색식리스트["청산_C"].이름 = ""; }

            GenieConfig.CBB_Liquidation_use_condition_A = GET.ComboBoxIndex(form.CBB_Liquidation_use_condition_A);
            GenieConfig.CBB_Liquidation_use_condition_B = GET.ComboBoxIndex(form.CBB_Liquidation_use_condition_B);
            GenieConfig.CBB_Liquidation_use_condition_C = GET.ComboBoxIndex(form.CBB_Liquidation_use_condition_C);

            if (GenieConfig.CBB_Liquidation_use_condition_A == 0) Condition_Management.Catch_Stock_List_Clear("청산_A");
            if (GenieConfig.CBB_Liquidation_use_condition_B == 0) Condition_Management.Catch_Stock_List_Clear("청산_B");
            if (GenieConfig.CBB_Liquidation_use_condition_C == 0) Condition_Management.Catch_Stock_List_Clear("청산_C");

            GenieConfig.CB_Liquidation_A = form.CB_Liquidation_A.Checked;
            GenieConfig.CB_Liquidation_B = form.CB_Liquidation_B.Checked;
            GenieConfig.CB_Liquidation_C = form.CB_Liquidation_C.Checked;

            GenieConfig.combo_rebalance_suik_gubun_A = GET.ComboBoxIndex(form.combo_rebalance_suik_gubun_A);
            GenieConfig.combo_rebalance_suik_gubun_B = GET.ComboBoxIndex(form.combo_rebalance_suik_gubun_B);
            GenieConfig.combo_rebalance_suik_gubun_C = GET.ComboBoxIndex(form.combo_rebalance_suik_gubun_C);
            GenieConfig.combo_rebalance_suik_gubun_D = GET.ComboBoxIndex(form.combo_rebalance_suik_gubun_D);
            GenieConfig.combo_rebalance_suik_gubun_E = GET.ComboBoxIndex(form.combo_rebalance_suik_gubun_E);
            GenieConfig.combo_rebalance_suik_gubun_F = GET.ComboBoxIndex(form.combo_rebalance_suik_gubun_F);
            GenieConfig.combo_rebalance_suik_gubun_G = GET.ComboBoxIndex(form.combo_rebalance_suik_gubun_G);

            GenieConfig.combo_rebalance_sell_gubun_A = GET.ComboBoxIndex(form.combo_rebalance_sell_gubun_A);
            GenieConfig.combo_rebalance_sell_gubun_B = GET.ComboBoxIndex(form.combo_rebalance_sell_gubun_B);
            GenieConfig.combo_rebalance_sell_gubun_C = GET.ComboBoxIndex(form.combo_rebalance_sell_gubun_C);
            GenieConfig.combo_rebalance_sell_gubun_D = GET.ComboBoxIndex(form.combo_rebalance_sell_gubun_D);
            GenieConfig.combo_rebalance_sell_gubun_E = GET.ComboBoxIndex(form.combo_rebalance_sell_gubun_E);
            GenieConfig.combo_rebalance_sell_gubun_F = GET.ComboBoxIndex(form.combo_rebalance_sell_gubun_F);
            GenieConfig.combo_rebalance_sell_gubun_G = GET.ComboBoxIndex(form.combo_rebalance_sell_gubun_G);

            GenieConfig.combo_rebalance_maemae_gubun_A = GET.ComboBoxIndex(form.combo_rebalance_maemae_gubun_A);
            GenieConfig.combo_rebalance_maemae_gubun_B = GET.ComboBoxIndex(form.combo_rebalance_maemae_gubun_B);
            GenieConfig.combo_rebalance_maemae_gubun_C = GET.ComboBoxIndex(form.combo_rebalance_maemae_gubun_C);
            GenieConfig.combo_rebalance_maemae_gubun_D = GET.ComboBoxIndex(form.combo_rebalance_maemae_gubun_D);
            GenieConfig.combo_rebalance_maemae_gubun_E = GET.ComboBoxIndex(form.combo_rebalance_maemae_gubun_E);
            GenieConfig.combo_rebalance_maemae_gubun_F = GET.ComboBoxIndex(form.combo_rebalance_maemae_gubun_F);
            GenieConfig.combo_rebalance_maemae_gubun_G = GET.ComboBoxIndex(form.combo_rebalance_maemae_gubun_G);

            GenieConfig.combo_rebalance_jumun_A = GET.ComboBoxIndex(form.combo_rebalance_jumun_A);
            GenieConfig.combo_rebalance_jumun_B = GET.ComboBoxIndex(form.combo_rebalance_jumun_B);
            GenieConfig.combo_rebalance_jumun_C = GET.ComboBoxIndex(form.combo_rebalance_jumun_C);
            GenieConfig.combo_rebalance_jumun_D = GET.ComboBoxIndex(form.combo_rebalance_jumun_D);
            GenieConfig.combo_rebalance_jumun_E = GET.ComboBoxIndex(form.combo_rebalance_jumun_E);
            GenieConfig.combo_rebalance_jumun_F = GET.ComboBoxIndex(form.combo_rebalance_jumun_F);
            GenieConfig.combo_rebalance_jumun_G = GET.ComboBoxIndex(form.combo_rebalance_jumun_G);

            GenieConfig.CBB_cut_gubun_A = GET.ComboBoxIndex(form.CBB_cut_gubun_A);
            GenieConfig.CBB_cut_gubun_B = GET.ComboBoxIndex(form.CBB_cut_gubun_B);
            GenieConfig.CBB_cut_gubun_C = GET.ComboBoxIndex(form.CBB_cut_gubun_C);

            GenieConfig.CBB_cut_jumun_A = GET.ComboBoxIndex(form.CBB_cut_jumun_A);
            GenieConfig.CBB_cut_jumun_B = GET.ComboBoxIndex(form.CBB_cut_jumun_B);
            GenieConfig.CBB_cut_jumun_C = GET.ComboBoxIndex(form.CBB_cut_jumun_C);

            GenieConfig.combo_rebalance_감시_jumun_A = GET.ComboBoxIndex(form.combo_rebalance_감시_jumun_A);
            GenieConfig.combo_rebalance_감시_jumun_B = GET.ComboBoxIndex(form.combo_rebalance_감시_jumun_B);
            GenieConfig.combo_rebalance_감시_jumun_C = GET.ComboBoxIndex(form.combo_rebalance_감시_jumun_C);
            GenieConfig.combo_rebalance_감시_jumun_D = GET.ComboBoxIndex(form.combo_rebalance_감시_jumun_D);
            GenieConfig.combo_rebalance_감시_jumun_E = GET.ComboBoxIndex(form.combo_rebalance_감시_jumun_E);
            GenieConfig.combo_rebalance_감시_jumun_F = GET.ComboBoxIndex(form.combo_rebalance_감시_jumun_F);
            GenieConfig.combo_rebalance_감시_jumun_G = GET.ComboBoxIndex(form.combo_rebalance_감시_jumun_G);

            try
            {
                double.TryParse(form.TB_rebalance_감시_value_A.Text, out double 감시_value_A);
                double.TryParse(form.TB_rebalance_감시_value_B.Text, out double 감시_value_B);
                double.TryParse(form.TB_rebalance_감시_value_C.Text, out double 감시_value_C);
                double.TryParse(form.TB_rebalance_감시_value_D.Text, out double 감시_value_D);
                double.TryParse(form.TB_rebalance_감시_value_E.Text, out double 감시_value_E);
                double.TryParse(form.TB_rebalance_감시_value_F.Text, out double 감시_value_F);
                double.TryParse(form.TB_rebalance_감시_value_G.Text, out double 감시_value_G);

                if (form.combo_rebalance_감시_jumun_A.SelectedIndex < 3) 감시_value_A = 0;
                if (form.combo_rebalance_감시_jumun_B.SelectedIndex < 3) 감시_value_B = 0;
                if (form.combo_rebalance_감시_jumun_C.SelectedIndex < 3) 감시_value_C = 0;
                if (form.combo_rebalance_감시_jumun_D.SelectedIndex < 3) 감시_value_D = 0;
                if (form.combo_rebalance_감시_jumun_E.SelectedIndex < 3) 감시_value_E = 0;
                if (form.combo_rebalance_감시_jumun_F.SelectedIndex < 3) 감시_value_F = 0;
                if (form.combo_rebalance_감시_jumun_G.SelectedIndex < 3) 감시_value_G = 0;

                GenieConfig.TB_rebalance_감시_value_A = 감시_value_A;
                GenieConfig.TB_rebalance_감시_value_B = 감시_value_B;
                GenieConfig.TB_rebalance_감시_value_C = 감시_value_C;
                GenieConfig.TB_rebalance_감시_value_D = 감시_value_D;
                GenieConfig.TB_rebalance_감시_value_E = 감시_value_E;
                GenieConfig.TB_rebalance_감시_value_F = 감시_value_F;
                GenieConfig.TB_rebalance_감시_value_G = 감시_value_G;

                form.TB_rebalance_감시_value_A.Text = 감시_value_A.ToString();
                form.TB_rebalance_감시_value_B.Text = 감시_value_B.ToString();
                form.TB_rebalance_감시_value_C.Text = 감시_value_C.ToString();
                form.TB_rebalance_감시_value_D.Text = 감시_value_D.ToString();
                form.TB_rebalance_감시_value_E.Text = 감시_value_E.ToString();
                form.TB_rebalance_감시_value_F.Text = 감시_value_F.ToString();
                form.TB_rebalance_감시_value_G.Text = 감시_value_G.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 리벨 감시 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 리벨 감시 입력 오류 : " + e.Message);
            }

            GenieConfig.CBB_Liquidation_suik_gubun_A = GET.ComboBoxIndex(form.CBB_Liquidation_suik_gubun_A);
            GenieConfig.CBB_Liquidation_suik_gubun_B = GET.ComboBoxIndex(form.CBB_Liquidation_suik_gubun_B);
            GenieConfig.CBB_Liquidation_suik_gubun_C = GET.ComboBoxIndex(form.CBB_Liquidation_suik_gubun_C);
            GenieConfig.CBB_Liquidation_sell_gubun_A = GET.ComboBoxIndex(form.CBB_Liquidation_sell_gubun_A);
            GenieConfig.CBB_Liquidation_sell_gubun_B = GET.ComboBoxIndex(form.CBB_Liquidation_sell_gubun_B);
            GenieConfig.CBB_Liquidation_sell_gubun_C = GET.ComboBoxIndex(form.CBB_Liquidation_sell_gubun_C);

            GenieConfig.CBB_Liquidation_jumun_A = GET.ComboBoxIndex(form.CBB_Liquidation_jumun_A);
            GenieConfig.CBB_Liquidation_jumun_B = GET.ComboBoxIndex(form.CBB_Liquidation_jumun_B);
            GenieConfig.CBB_Liquidation_jumun_C = GET.ComboBoxIndex(form.CBB_Liquidation_jumun_C);

            GenieConfig.CBB_Liquidation_Cancel_A = GET.ComboBoxIndex(form.CBB_Liquidation_Cancel_A);
            GenieConfig.CBB_Liquidation_Cancel_B = GET.ComboBoxIndex(form.CBB_Liquidation_Cancel_B);
            GenieConfig.CBB_Liquidation_Cancel_C = GET.ComboBoxIndex(form.CBB_Liquidation_Cancel_C);

            try
            {
                double.TryParse(form.TB_손익비율.Text, out double 손익비율);
                double.TryParse(form.TB_매수비율.Text, out double 매수비율);

                if (손익비율 == 0) 손익비율 = 10;
                if (매수비율 == 0) 매수비율 = 10;

                GenieConfig.TB_손익비율 = Math.Abs(손익비율);
                GenieConfig.TB_매수비율 = Math.Abs(매수비율);

                form.TB_손익비율.Text = GenieConfig.TB_손익비율.ToString();
                form.TB_매수비율.Text = GenieConfig.TB_매수비율.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 기준금 비율 계산 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 기준금 비율 계산 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_분할간격_A.Text, out int 분할간격_A);
                int.TryParse(form.TB_분할간격_B.Text, out int 분할간격_B);
                int.TryParse(form.TB_분할간격_C.Text, out int 분할간격_C);

                if (분할간격_A == 0) 분할간격_A = 1;
                if (분할간격_B == 0) 분할간격_B = 1;
                if (분할간격_C == 0) 분할간격_C = 1;

                GenieConfig.TB_분할간격_A = 분할간격_A;
                GenieConfig.TB_분할간격_B = 분할간격_B;
                GenieConfig.TB_분할간격_C = 분할간격_C;

                form.TB_분할간격_A.Text = 분할간격_A.ToString();
                form.TB_분할간격_B.Text = 분할간격_B.ToString();
                form.TB_분할간격_C.Text = 분할간격_C.ToString();

                int.TryParse(form.TB_분할횟수_A.Text, out int 분할횟수_A);
                int.TryParse(form.TB_분할횟수_B.Text, out int 분할횟수_B);
                int.TryParse(form.TB_분할횟수_C.Text, out int 분할횟수_C);

                if (분할횟수_A == 0) 분할횟수_A = 1;
                if (분할횟수_B == 0) 분할횟수_B = 1;
                if (분할횟수_C == 0) 분할횟수_C = 1;

                GenieConfig.TB_분할횟수_A = Math.Abs(분할횟수_A);
                GenieConfig.TB_분할횟수_B = Math.Abs(분할횟수_B);
                GenieConfig.TB_분할횟수_C = Math.Abs(분할횟수_C);

                form.TB_분할횟수_A.Text = GenieConfig.TB_분할횟수_A.ToString();
                form.TB_분할횟수_B.Text = GenieConfig.TB_분할횟수_B.ToString();
                form.TB_분할횟수_C.Text = GenieConfig.TB_분할횟수_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 분할주문 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 분할주문 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_일매수제한금.Text.Replace(",", ""), out double 일매수제한금);
                double.TryParse(form.TB_총매수금.Text.Replace(",", ""), out double 총매수금);
                int.TryParse(form.TB_회수제한.Text, out int 회수제한);

                if (일매수제한금 == 0) 일매수제한금 = 10;
                if (총매수금 == 0) 총매수금 = 100;
                if (회수제한 == 0) 회수제한 = 5;

                GenieConfig.TB_일매수제한금 = 일매수제한금;
                GenieConfig.TB_총매수금 = 총매수금;
                GenieConfig.TB_회수제한 = 회수제한;

                form.TB_일매수제한금.Text = GenieConfig.TB_일매수제한금.ToString();
                form.TB_총매수금.Text = GenieConfig.TB_총매수금.ToString();
                form.TB_회수제한.Text = GenieConfig.TB_회수제한.ToString();
            }
            catch
            {
                Form1.AutoClosingAlram("일매수제한금 저장 에러", "에러알림", 10, "동작");
                Log.에러기록("일매수제한금 저장 에러");
            }

            try
            {
                int.TryParse(form.TB_리밸_추매주가이상.Text.Replace(",", ""), out int 추매주가이상);
                int.TryParse(form.TB_리밸_추매주가이하.Text.Replace(",", ""), out int 추매주가이하);

                if (추매주가이하 == 0) 추매주가이하 = 1000000;
                GenieConfig.TB_리밸_추매주가이상 = 추매주가이상;
                GenieConfig.TB_리밸_추매주가이하 = 추매주가이하;

                double.TryParse(form.TB_리밸_추매등락률이상.Text, out double 추매등락률이상);
                double.TryParse(form.TB_리밸_추매등락률이하.Text, out double 추매등락률이하);

                GenieConfig.TB_리밸_추매등락률이상 = 추매등락률이상;
                GenieConfig.TB_리밸_추매등락률이하 = 추매등락률이하;

                form.TB_리밸_추매주가이상.Text = GenieConfig.TB_리밸_추매주가이상.ToString();
                form.TB_리밸_추매주가이하.Text = GenieConfig.TB_리밸_추매주가이하.ToString();
                form.TB_리밸_추매등락률이상.Text = GenieConfig.TB_리밸_추매등락률이상.ToString();
                form.TB_리밸_추매등락률이하.Text = GenieConfig.TB_리밸_추매등락률이하.ToString();

            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 추매주가제한 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 추매주가제한 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_Liquidation_MinMAPeriod_A.Text, out int TB_Liquidation_MinMAPeriod_A);
                int.TryParse(form.TB_Liquidation_MinMAPeriod_B.Text, out int TB_Liquidation_MinMAPeriod_B);
                int.TryParse(form.TB_Liquidation_MinMAPeriod_C.Text, out int TB_Liquidation_MinMAPeriod_C);

                if (TB_Liquidation_MinMAPeriod_A == 0) TB_Liquidation_MinMAPeriod_A = 3;
                if (TB_Liquidation_MinMAPeriod_B == 0) TB_Liquidation_MinMAPeriod_B = 3;
                if (TB_Liquidation_MinMAPeriod_C == 0) TB_Liquidation_MinMAPeriod_C = 3;

                if (TB_Liquidation_MinMAPeriod_A > 60) TB_Liquidation_MinMAPeriod_A = 60;
                if (TB_Liquidation_MinMAPeriod_B > 60) TB_Liquidation_MinMAPeriod_B = 60;
                if (TB_Liquidation_MinMAPeriod_C > 60) TB_Liquidation_MinMAPeriod_C = 60;

                GenieConfig.TB_Liquidation_MinMAPeriod_A = TB_Liquidation_MinMAPeriod_A;
                GenieConfig.TB_Liquidation_MinMAPeriod_B = TB_Liquidation_MinMAPeriod_B;
                GenieConfig.TB_Liquidation_MinMAPeriod_C = TB_Liquidation_MinMAPeriod_C;

                form.TB_Liquidation_MinMAPeriod_A.Text = TB_Liquidation_MinMAPeriod_A.ToString();
                form.TB_Liquidation_MinMAPeriod_B.Text = TB_Liquidation_MinMAPeriod_B.ToString();
                form.TB_Liquidation_MinMAPeriod_C.Text = TB_Liquidation_MinMAPeriod_C.ToString();

                GenieConfig.CBB_Liquidation_MinMAPeriod_A = GET.ComboBoxIndex(form.CBB_Liquidation_MinMAPeriod_A);
                GenieConfig.CBB_Liquidation_MinMAPeriod_B = GET.ComboBoxIndex(form.CBB_Liquidation_MinMAPeriod_B);
                GenieConfig.CBB_Liquidation_MinMAPeriod_C = GET.ComboBoxIndex(form.CBB_Liquidation_MinMAPeriod_C);
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 이평 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 이평 입력 오류 : " + e.Message);
            }

            GenieConfig.CB_매수기준 = form.CB_매수기준.Checked;
            GenieConfig.CB_손익기준 = form.CB_손익기준.Checked;

            GenieConfig.CB_rebalance_기준금 = form.CB_rebalance_기준금.Checked;

            GenieConfig.CBB_rebalance_1_A = GET.ComboBoxIndex(form.CBB_rebalance_1A);
            GenieConfig.CBB_rebalance_1_B = GET.ComboBoxIndex(form.CBB_rebalance_1B);
            GenieConfig.CBB_rebalance_1_C = GET.ComboBoxIndex(form.CBB_rebalance_1C);
            GenieConfig.CBB_rebalance_1_D = GET.ComboBoxIndex(form.CBB_rebalance_1D);
            GenieConfig.CBB_rebalance_1_E = GET.ComboBoxIndex(form.CBB_rebalance_1E);
            GenieConfig.CBB_rebalance_1_F = GET.ComboBoxIndex(form.CBB_rebalance_1F);
            GenieConfig.CBB_rebalance_1_G = GET.ComboBoxIndex(form.CBB_rebalance_1G);

            GenieConfig.CBB_rebalance_2_A = GET.ComboBoxIndex(form.CBB_rebalance_2A);
            GenieConfig.CBB_rebalance_2_B = GET.ComboBoxIndex(form.CBB_rebalance_2B);
            GenieConfig.CBB_rebalance_2_C = GET.ComboBoxIndex(form.CBB_rebalance_2C);
            GenieConfig.CBB_rebalance_2_D = GET.ComboBoxIndex(form.CBB_rebalance_2D);
            GenieConfig.CBB_rebalance_2_E = GET.ComboBoxIndex(form.CBB_rebalance_2E);
            GenieConfig.CBB_rebalance_2_F = GET.ComboBoxIndex(form.CBB_rebalance_2F);
            GenieConfig.CBB_rebalance_2_G = GET.ComboBoxIndex(form.CBB_rebalance_2G);

            GenieConfig.리밸매도기준1_A = (form.CBB_rebalance_1A.SelectedIndex == -1) ? form.CBB_rebalance_1A.Items[0].ToString() : form.CBB_rebalance_1A.Text;
            GenieConfig.리밸매도기준1_B = (form.CBB_rebalance_1B.SelectedIndex == -1) ? form.CBB_rebalance_1B.Items[0].ToString() : form.CBB_rebalance_1B.Text;
            GenieConfig.리밸매도기준1_C = (form.CBB_rebalance_1C.SelectedIndex == -1) ? form.CBB_rebalance_1C.Items[0].ToString() : form.CBB_rebalance_1C.Text;
            GenieConfig.리밸매도기준1_D = (form.CBB_rebalance_1D.SelectedIndex == -1) ? form.CBB_rebalance_1D.Items[0].ToString() : form.CBB_rebalance_1D.Text;
            GenieConfig.리밸매도기준1_E = (form.CBB_rebalance_1E.SelectedIndex == -1) ? form.CBB_rebalance_1E.Items[0].ToString() : form.CBB_rebalance_1E.Text;
            GenieConfig.리밸매도기준1_F = (form.CBB_rebalance_1F.SelectedIndex == -1) ? form.CBB_rebalance_1F.Items[0].ToString() : form.CBB_rebalance_1F.Text;
            GenieConfig.리밸매도기준1_G = (form.CBB_rebalance_1G.SelectedIndex == -1) ? form.CBB_rebalance_1G.Items[0].ToString() : form.CBB_rebalance_1G.Text;

            GenieConfig.리밸매도기준2_A = (form.CBB_rebalance_2A.SelectedIndex == -1) ? form.CBB_rebalance_2A.Items[0].ToString() : form.CBB_rebalance_2A.Text;
            GenieConfig.리밸매도기준2_B = (form.CBB_rebalance_2B.SelectedIndex == -1) ? form.CBB_rebalance_2B.Items[0].ToString() : form.CBB_rebalance_2B.Text;
            GenieConfig.리밸매도기준2_C = (form.CBB_rebalance_2C.SelectedIndex == -1) ? form.CBB_rebalance_2C.Items[0].ToString() : form.CBB_rebalance_2C.Text;
            GenieConfig.리밸매도기준2_D = (form.CBB_rebalance_2D.SelectedIndex == -1) ? form.CBB_rebalance_2D.Items[0].ToString() : form.CBB_rebalance_2D.Text;
            GenieConfig.리밸매도기준2_E = (form.CBB_rebalance_2E.SelectedIndex == -1) ? form.CBB_rebalance_2E.Items[0].ToString() : form.CBB_rebalance_2E.Text;
            GenieConfig.리밸매도기준2_F = (form.CBB_rebalance_2F.SelectedIndex == -1) ? form.CBB_rebalance_2F.Items[0].ToString() : form.CBB_rebalance_2F.Text;
            GenieConfig.리밸매도기준2_G = (form.CBB_rebalance_2G.SelectedIndex == -1) ? form.CBB_rebalance_2G.Items[0].ToString() : form.CBB_rebalance_2G.Text;

            GenieConfig.CB_rebalance_choice_A = form.CB_rebalance_choice_A.Checked;
            GenieConfig.CB_rebalance_choice_B = form.CB_rebalance_choice_B.Checked;
            GenieConfig.CB_rebalance_choice_C = form.CB_rebalance_choice_C.Checked;
            GenieConfig.CB_rebalance_choice_D = form.CB_rebalance_choice_D.Checked;
            GenieConfig.CB_rebalance_choice_E = form.CB_rebalance_choice_E.Checked;
            GenieConfig.CB_rebalance_choice_F = form.CB_rebalance_choice_F.Checked;
            GenieConfig.CB_rebalance_choice_G = form.CB_rebalance_choice_G.Checked;

            GenieConfig.CB_cut_A = form.CB_cut_A.Checked;
            GenieConfig.CB_cut_B = form.CB_cut_B.Checked;
            GenieConfig.CB_cut_C = form.CB_cut_C.Checked;
            GenieConfig.CB_cut_기준금 = form.CB_cut_기준금.Checked;

            GenieConfig.CB_rebalance_option_A = form.CB_rebalance_option_A.Checked;
            GenieConfig.CB_rebalance_option_B = form.CB_rebalance_option_B.Checked;
            GenieConfig.CB_rebalance_option_C = form.CB_rebalance_option_C.Checked;
            GenieConfig.CB_rebalance_option_D = form.CB_rebalance_option_D.Checked;
            GenieConfig.CB_rebalance_option_E = form.CB_rebalance_option_E.Checked;
            GenieConfig.CB_rebalance_option_F = form.CB_rebalance_option_F.Checked;
            GenieConfig.CB_rebalance_option_G = form.CB_rebalance_option_G.Checked;

            GenieConfig.CB_Liquidation_SellStop_A = form.CB_Liquidation_SellStop_A.Checked;
            GenieConfig.CB_Liquidation_SellStop_B = form.CB_Liquidation_SellStop_B.Checked;
            GenieConfig.CB_Liquidation_SellStop_C = form.CB_Liquidation_SellStop_C.Checked;

            GenieConfig.CB_Liquidation_강제매도_A = form.CB_Liquidation_강제매도_A.Checked;
            GenieConfig.CB_Liquidation_강제매도_B = form.CB_Liquidation_강제매도_B.Checked;
            GenieConfig.CB_Liquidation_강제매도_C = form.CB_Liquidation_강제매도_C.Checked;

            GenieConfig.CB_추매금지_Liquidation_A = form.CB_추매금지_Liquidation_A.Checked;
            GenieConfig.CB_추매금지_Liquidation_B = form.CB_추매금지_Liquidation_B.Checked;
            GenieConfig.CB_추매금지_Liquidation_C = form.CB_추매금지_Liquidation_C.Checked;

            GenieConfig.CB_수익보전_Liquidation_A = form.CB_수익보전_Liquidation_A.Checked;
            GenieConfig.CB_수익보전_Liquidation_B = form.CB_수익보전_Liquidation_B.Checked;
            GenieConfig.CB_수익보전_Liquidation_C = form.CB_수익보전_Liquidation_C.Checked;

            GenieConfig.CB_Liquidation_choice_A = form.CB_Liquidation_choice_A.Checked;
            GenieConfig.CB_Liquidation_choice_B = form.CB_Liquidation_choice_B.Checked;
            GenieConfig.CB_Liquidation_choice_C = form.CB_Liquidation_choice_C.Checked;

            GenieConfig.CB_Liquidation_기준금 = form.CB_Liquidation_기준금.Checked;

            try
            {
                double.TryParse(form.TB_rebalance_TS_1차_down_A.Text, out double TB_rebalance_TS_1차_down_A);
                double.TryParse(form.TB_rebalance_TS_1차_down_B.Text, out double TB_rebalance_TS_1차_down_B);
                double.TryParse(form.TB_rebalance_TS_1차_down_C.Text, out double TB_rebalance_TS_1차_down_C);
                double.TryParse(form.TB_rebalance_TS_1차_down_D.Text, out double TB_rebalance_TS_1차_down_D);
                double.TryParse(form.TB_rebalance_TS_1차_down_E.Text, out double TB_rebalance_TS_1차_down_E);
                double.TryParse(form.TB_rebalance_TS_1차_down_F.Text, out double TB_rebalance_TS_1차_down_F);
                double.TryParse(form.TB_rebalance_TS_1차_down_G.Text, out double TB_rebalance_TS_1차_down_G);
                double.TryParse(form.TB_rebalance_TS_2차_down_A.Text, out double TB_rebalance_TS_2차_down_A);
                double.TryParse(form.TB_rebalance_TS_2차_down_B.Text, out double TB_rebalance_TS_2차_down_B);
                double.TryParse(form.TB_rebalance_TS_2차_down_C.Text, out double TB_rebalance_TS_2차_down_C);
                double.TryParse(form.TB_rebalance_TS_2차_down_D.Text, out double TB_rebalance_TS_2차_down_D);
                double.TryParse(form.TB_rebalance_TS_2차_down_E.Text, out double TB_rebalance_TS_2차_down_E);
                double.TryParse(form.TB_rebalance_TS_2차_down_F.Text, out double TB_rebalance_TS_2차_down_F);
                double.TryParse(form.TB_rebalance_TS_2차_down_G.Text, out double TB_rebalance_TS_2차_down_G);

                GenieConfig.TB_rebalance_TS_1차_down_A = TB_rebalance_TS_1차_down_A;
                GenieConfig.TB_rebalance_TS_1차_down_B = TB_rebalance_TS_1차_down_B;
                GenieConfig.TB_rebalance_TS_1차_down_C = TB_rebalance_TS_1차_down_C;
                GenieConfig.TB_rebalance_TS_1차_down_D = TB_rebalance_TS_1차_down_D;
                GenieConfig.TB_rebalance_TS_1차_down_E = TB_rebalance_TS_1차_down_E;
                GenieConfig.TB_rebalance_TS_1차_down_F = TB_rebalance_TS_1차_down_F;
                GenieConfig.TB_rebalance_TS_1차_down_G = TB_rebalance_TS_1차_down_G;
                GenieConfig.TB_rebalance_TS_2차_down_A = TB_rebalance_TS_2차_down_A;
                GenieConfig.TB_rebalance_TS_2차_down_B = TB_rebalance_TS_2차_down_B;
                GenieConfig.TB_rebalance_TS_2차_down_C = TB_rebalance_TS_2차_down_C;
                GenieConfig.TB_rebalance_TS_2차_down_D = TB_rebalance_TS_2차_down_D;
                GenieConfig.TB_rebalance_TS_2차_down_E = TB_rebalance_TS_2차_down_E;
                GenieConfig.TB_rebalance_TS_2차_down_F = TB_rebalance_TS_2차_down_F;
                GenieConfig.TB_rebalance_TS_2차_down_G = TB_rebalance_TS_2차_down_G;
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / TS 다운값 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / TS 다운값 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_rebalance_TS_1차_MinMAPeriod_A.Text, out int TB_rebalance_TS_1차_MinMAPeriod_A);
                int.TryParse(form.TB_rebalance_TS_1차_MinMAPeriod_B.Text, out int TB_rebalance_TS_1차_MinMAPeriod_B);
                int.TryParse(form.TB_rebalance_TS_1차_MinMAPeriod_C.Text, out int TB_rebalance_TS_1차_MinMAPeriod_C);
                int.TryParse(form.TB_rebalance_TS_1차_MinMAPeriod_D.Text, out int TB_rebalance_TS_1차_MinMAPeriod_D);
                int.TryParse(form.TB_rebalance_TS_1차_MinMAPeriod_E.Text, out int TB_rebalance_TS_1차_MinMAPeriod_E);
                int.TryParse(form.TB_rebalance_TS_1차_MinMAPeriod_F.Text, out int TB_rebalance_TS_1차_MinMAPeriod_F);
                int.TryParse(form.TB_rebalance_TS_1차_MinMAPeriod_G.Text, out int TB_rebalance_TS_1차_MinMAPeriod_G);
                int.TryParse(form.TB_rebalance_TS_2차_MinMAPeriod_A.Text, out int TB_rebalance_TS_2차_MinMAPeriod_A);
                int.TryParse(form.TB_rebalance_TS_2차_MinMAPeriod_B.Text, out int TB_rebalance_TS_2차_MinMAPeriod_B);
                int.TryParse(form.TB_rebalance_TS_2차_MinMAPeriod_C.Text, out int TB_rebalance_TS_2차_MinMAPeriod_C);
                int.TryParse(form.TB_rebalance_TS_2차_MinMAPeriod_D.Text, out int TB_rebalance_TS_2차_MinMAPeriod_D);
                int.TryParse(form.TB_rebalance_TS_2차_MinMAPeriod_E.Text, out int TB_rebalance_TS_2차_MinMAPeriod_E);
                int.TryParse(form.TB_rebalance_TS_2차_MinMAPeriod_F.Text, out int TB_rebalance_TS_2차_MinMAPeriod_F);
                int.TryParse(form.TB_rebalance_TS_2차_MinMAPeriod_G.Text, out int TB_rebalance_TS_2차_MinMAPeriod_G);

                if (TB_rebalance_TS_1차_MinMAPeriod_A == 0) TB_rebalance_TS_1차_MinMAPeriod_A = 5;
                if (TB_rebalance_TS_1차_MinMAPeriod_B == 0) TB_rebalance_TS_1차_MinMAPeriod_B = 5;
                if (TB_rebalance_TS_1차_MinMAPeriod_C == 0) TB_rebalance_TS_1차_MinMAPeriod_C = 5;
                if (TB_rebalance_TS_1차_MinMAPeriod_D == 0) TB_rebalance_TS_1차_MinMAPeriod_D = 5;
                if (TB_rebalance_TS_1차_MinMAPeriod_E == 0) TB_rebalance_TS_1차_MinMAPeriod_E = 5;
                if (TB_rebalance_TS_1차_MinMAPeriod_F == 0) TB_rebalance_TS_1차_MinMAPeriod_F = 5;
                if (TB_rebalance_TS_1차_MinMAPeriod_G == 0) TB_rebalance_TS_1차_MinMAPeriod_G = 5;
                if (TB_rebalance_TS_2차_MinMAPeriod_A == 0) TB_rebalance_TS_2차_MinMAPeriod_A = 5;
                if (TB_rebalance_TS_2차_MinMAPeriod_B == 0) TB_rebalance_TS_2차_MinMAPeriod_B = 5;
                if (TB_rebalance_TS_2차_MinMAPeriod_C == 0) TB_rebalance_TS_2차_MinMAPeriod_C = 5;
                if (TB_rebalance_TS_2차_MinMAPeriod_D == 0) TB_rebalance_TS_2차_MinMAPeriod_D = 5;
                if (TB_rebalance_TS_2차_MinMAPeriod_E == 0) TB_rebalance_TS_2차_MinMAPeriod_E = 5;
                if (TB_rebalance_TS_2차_MinMAPeriod_F == 0) TB_rebalance_TS_2차_MinMAPeriod_F = 5;
                if (TB_rebalance_TS_2차_MinMAPeriod_G == 0) TB_rebalance_TS_2차_MinMAPeriod_G = 5;

                if (TB_rebalance_TS_1차_MinMAPeriod_A > 300) TB_rebalance_TS_1차_MinMAPeriod_A = 300;
                if (TB_rebalance_TS_1차_MinMAPeriod_B > 300) TB_rebalance_TS_1차_MinMAPeriod_B = 300;
                if (TB_rebalance_TS_1차_MinMAPeriod_C > 300) TB_rebalance_TS_1차_MinMAPeriod_C = 300;
                if (TB_rebalance_TS_1차_MinMAPeriod_D > 300) TB_rebalance_TS_1차_MinMAPeriod_D = 300;
                if (TB_rebalance_TS_1차_MinMAPeriod_E > 300) TB_rebalance_TS_1차_MinMAPeriod_E = 300;
                if (TB_rebalance_TS_1차_MinMAPeriod_F > 300) TB_rebalance_TS_1차_MinMAPeriod_F = 300;
                if (TB_rebalance_TS_1차_MinMAPeriod_G > 300) TB_rebalance_TS_1차_MinMAPeriod_G = 300;
                if (TB_rebalance_TS_2차_MinMAPeriod_A > 300) TB_rebalance_TS_2차_MinMAPeriod_A = 300;
                if (TB_rebalance_TS_2차_MinMAPeriod_B > 300) TB_rebalance_TS_2차_MinMAPeriod_B = 300;
                if (TB_rebalance_TS_2차_MinMAPeriod_C > 300) TB_rebalance_TS_2차_MinMAPeriod_C = 300;
                if (TB_rebalance_TS_2차_MinMAPeriod_D > 300) TB_rebalance_TS_2차_MinMAPeriod_D = 300;
                if (TB_rebalance_TS_2차_MinMAPeriod_E > 300) TB_rebalance_TS_2차_MinMAPeriod_E = 300;
                if (TB_rebalance_TS_2차_MinMAPeriod_F > 300) TB_rebalance_TS_2차_MinMAPeriod_F = 300;
                if (TB_rebalance_TS_2차_MinMAPeriod_G > 300) TB_rebalance_TS_2차_MinMAPeriod_G = 300;

                GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_A = TB_rebalance_TS_1차_MinMAPeriod_A;
                GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_B = TB_rebalance_TS_1차_MinMAPeriod_B;
                GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_C = TB_rebalance_TS_1차_MinMAPeriod_C;
                GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_D = TB_rebalance_TS_1차_MinMAPeriod_D;
                GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_E = TB_rebalance_TS_1차_MinMAPeriod_E;
                GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_F = TB_rebalance_TS_1차_MinMAPeriod_F;
                GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_G = TB_rebalance_TS_1차_MinMAPeriod_G;
                GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_A = TB_rebalance_TS_2차_MinMAPeriod_A;
                GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_B = TB_rebalance_TS_2차_MinMAPeriod_B;
                GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_C = TB_rebalance_TS_2차_MinMAPeriod_C;
                GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_D = TB_rebalance_TS_2차_MinMAPeriod_D;
                GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_E = TB_rebalance_TS_2차_MinMAPeriod_E;
                GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_F = TB_rebalance_TS_2차_MinMAPeriod_F;
                GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_G = TB_rebalance_TS_2차_MinMAPeriod_G;

                form.TB_rebalance_TS_1차_MinMAPeriod_A.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_A.ToString();
                form.TB_rebalance_TS_1차_MinMAPeriod_B.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_B.ToString();
                form.TB_rebalance_TS_1차_MinMAPeriod_C.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_C.ToString();
                form.TB_rebalance_TS_1차_MinMAPeriod_D.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_D.ToString();
                form.TB_rebalance_TS_1차_MinMAPeriod_E.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_E.ToString();
                form.TB_rebalance_TS_1차_MinMAPeriod_F.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_F.ToString();
                form.TB_rebalance_TS_1차_MinMAPeriod_G.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_G.ToString();
                form.TB_rebalance_TS_2차_MinMAPeriod_A.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_A.ToString();
                form.TB_rebalance_TS_2차_MinMAPeriod_B.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_B.ToString();
                form.TB_rebalance_TS_2차_MinMAPeriod_C.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_C.ToString();
                form.TB_rebalance_TS_2차_MinMAPeriod_D.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_D.ToString();
                form.TB_rebalance_TS_2차_MinMAPeriod_E.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_E.ToString();
                form.TB_rebalance_TS_2차_MinMAPeriod_F.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_F.ToString();
                form.TB_rebalance_TS_2차_MinMAPeriod_G.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_G.ToString();

            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 리밸런싱 TS 분이평 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 리밸런싱 TS 분이평 입력 오류 : " + e.Message);
            }

            if (form.CBB_rebalance_1A.Text.Contains("손절")) form.CB_rebalance_TS_1차_A.Checked = false; GenieConfig.CB_rebalance_TS_1차_A = form.CB_rebalance_TS_1차_A.Checked;
            if (form.CBB_rebalance_1B.Text.Contains("손절")) form.CB_rebalance_TS_1차_B.Checked = false; GenieConfig.CB_rebalance_TS_1차_B = form.CB_rebalance_TS_1차_B.Checked;
            if (form.CBB_rebalance_1C.Text.Contains("손절")) form.CB_rebalance_TS_1차_C.Checked = false; GenieConfig.CB_rebalance_TS_1차_C = form.CB_rebalance_TS_1차_C.Checked;
            if (form.CBB_rebalance_1D.Text.Contains("손절")) form.CB_rebalance_TS_1차_D.Checked = false; GenieConfig.CB_rebalance_TS_1차_D = form.CB_rebalance_TS_1차_D.Checked;
            if (form.CBB_rebalance_1E.Text.Contains("손절")) form.CB_rebalance_TS_1차_E.Checked = false; GenieConfig.CB_rebalance_TS_1차_E = form.CB_rebalance_TS_1차_E.Checked;
            if (form.CBB_rebalance_1F.Text.Contains("손절")) form.CB_rebalance_TS_1차_F.Checked = false; GenieConfig.CB_rebalance_TS_1차_F = form.CB_rebalance_TS_1차_F.Checked;
            if (form.CBB_rebalance_1G.Text.Contains("손절")) form.CB_rebalance_TS_1차_G.Checked = false; GenieConfig.CB_rebalance_TS_1차_G = form.CB_rebalance_TS_1차_G.Checked;
            if (form.CBB_rebalance_2A.Text.Contains("손절")) form.CB_rebalance_TS_2차_A.Checked = false; GenieConfig.CB_rebalance_TS_2차_A = form.CB_rebalance_TS_2차_A.Checked;
            if (form.CBB_rebalance_2B.Text.Contains("손절")) form.CB_rebalance_TS_2차_B.Checked = false; GenieConfig.CB_rebalance_TS_2차_B = form.CB_rebalance_TS_2차_B.Checked;
            if (form.CBB_rebalance_2C.Text.Contains("손절")) form.CB_rebalance_TS_2차_C.Checked = false; GenieConfig.CB_rebalance_TS_2차_C = form.CB_rebalance_TS_2차_C.Checked;
            if (form.CBB_rebalance_2D.Text.Contains("손절")) form.CB_rebalance_TS_2차_D.Checked = false; GenieConfig.CB_rebalance_TS_2차_D = form.CB_rebalance_TS_2차_D.Checked;
            if (form.CBB_rebalance_2E.Text.Contains("손절")) form.CB_rebalance_TS_2차_E.Checked = false; GenieConfig.CB_rebalance_TS_2차_E = form.CB_rebalance_TS_2차_E.Checked;
            if (form.CBB_rebalance_2F.Text.Contains("손절")) form.CB_rebalance_TS_2차_F.Checked = false; GenieConfig.CB_rebalance_TS_2차_F = form.CB_rebalance_TS_2차_F.Checked;
            if (form.CBB_rebalance_2G.Text.Contains("손절")) form.CB_rebalance_TS_2차_G.Checked = false; GenieConfig.CB_rebalance_TS_2차_G = form.CB_rebalance_TS_2차_G.Checked;

            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_A = GET.ComboBoxIndex(form.CBB_rebalance_TS_1차_MinMAPeriod_A);
            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_B = GET.ComboBoxIndex(form.CBB_rebalance_TS_1차_MinMAPeriod_B);
            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_C = GET.ComboBoxIndex(form.CBB_rebalance_TS_1차_MinMAPeriod_C);
            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_D = GET.ComboBoxIndex(form.CBB_rebalance_TS_1차_MinMAPeriod_D);
            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_E = GET.ComboBoxIndex(form.CBB_rebalance_TS_1차_MinMAPeriod_E);
            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_F = GET.ComboBoxIndex(form.CBB_rebalance_TS_1차_MinMAPeriod_F);
            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_G = GET.ComboBoxIndex(form.CBB_rebalance_TS_1차_MinMAPeriod_G);
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_A = GET.ComboBoxIndex(form.CBB_rebalance_TS_2차_MinMAPeriod_A);
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_B = GET.ComboBoxIndex(form.CBB_rebalance_TS_2차_MinMAPeriod_B);
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_C = GET.ComboBoxIndex(form.CBB_rebalance_TS_2차_MinMAPeriod_C);
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_D = GET.ComboBoxIndex(form.CBB_rebalance_TS_2차_MinMAPeriod_D);
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_E = GET.ComboBoxIndex(form.CBB_rebalance_TS_2차_MinMAPeriod_E);
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_F = GET.ComboBoxIndex(form.CBB_rebalance_TS_2차_MinMAPeriod_F);
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_G = GET.ComboBoxIndex(form.CBB_rebalance_TS_2차_MinMAPeriod_G);

            try
            {
                double.TryParse(form.TB_Liquidation_TS_down_A.Text, out double TB_Liquidation_TS_down_A);
                double.TryParse(form.TB_Liquidation_TS_down_B.Text, out double TB_Liquidation_TS_down_B);
                double.TryParse(form.TB_Liquidation_TS_down_C.Text, out double TB_Liquidation_TS_down_C);

                GenieConfig.TB_Liquidation_TS_down_A = TB_Liquidation_TS_down_A;
                GenieConfig.TB_Liquidation_TS_down_B = TB_Liquidation_TS_down_B;
                GenieConfig.TB_Liquidation_TS_down_C = TB_Liquidation_TS_down_C;
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 청산 TS 다운값 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 청산 TS 다운값 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_Liquidation_TS_MinMAPeriod_A.Text, out int TB_Liquidation_TS_MinMAPeriod_A);
                int.TryParse(form.TB_Liquidation_TS_MinMAPeriod_B.Text, out int TB_Liquidation_TS_MinMAPeriod_B);
                int.TryParse(form.TB_Liquidation_TS_MinMAPeriod_C.Text, out int TB_Liquidation_TS_MinMAPeriod_C);

                if (TB_Liquidation_TS_MinMAPeriod_A == 0) TB_Liquidation_TS_MinMAPeriod_A = 3;
                if (TB_Liquidation_TS_MinMAPeriod_B == 0) TB_Liquidation_TS_MinMAPeriod_B = 3;
                if (TB_Liquidation_TS_MinMAPeriod_C == 0) TB_Liquidation_TS_MinMAPeriod_C = 3;

                if (TB_Liquidation_TS_MinMAPeriod_A > 300) TB_Liquidation_TS_MinMAPeriod_A = 300;
                if (TB_Liquidation_TS_MinMAPeriod_B > 300) TB_Liquidation_TS_MinMAPeriod_B = 300;
                if (TB_Liquidation_TS_MinMAPeriod_C > 300) TB_Liquidation_TS_MinMAPeriod_C = 300;

                GenieConfig.TB_Liquidation_TS_MinMAPeriod_A = TB_Liquidation_TS_MinMAPeriod_A;
                GenieConfig.TB_Liquidation_TS_MinMAPeriod_B = TB_Liquidation_TS_MinMAPeriod_B;
                GenieConfig.TB_Liquidation_TS_MinMAPeriod_C = TB_Liquidation_TS_MinMAPeriod_C;

                form.TB_Liquidation_TS_MinMAPeriod_A.Text = GenieConfig.TB_Liquidation_TS_MinMAPeriod_A.ToString();
                form.TB_Liquidation_TS_MinMAPeriod_B.Text = GenieConfig.TB_Liquidation_TS_MinMAPeriod_B.ToString();
                form.TB_Liquidation_TS_MinMAPeriod_C.Text = GenieConfig.TB_Liquidation_TS_MinMAPeriod_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 청산 TS 분이평 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 청산 TS 분이평 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_Liquidation_TS_DayMAPeriod_A.Text, out int TB_Liquidation_TS_DayMAPeriod_A);
                int.TryParse(form.TB_Liquidation_TS_DayMAPeriod_B.Text, out int TB_Liquidation_TS_DayMAPeriod_B);
                int.TryParse(form.TB_Liquidation_TS_DayMAPeriod_C.Text, out int TB_Liquidation_TS_DayMAPeriod_C);

                if (TB_Liquidation_TS_DayMAPeriod_A == 0) TB_Liquidation_TS_DayMAPeriod_A = 5;
                if (TB_Liquidation_TS_DayMAPeriod_B == 0) TB_Liquidation_TS_DayMAPeriod_B = 5;
                if (TB_Liquidation_TS_DayMAPeriod_C == 0) TB_Liquidation_TS_DayMAPeriod_C = 5;

                if (TB_Liquidation_TS_DayMAPeriod_A > 300) TB_Liquidation_TS_DayMAPeriod_A = 300;
                if (TB_Liquidation_TS_DayMAPeriod_B > 300) TB_Liquidation_TS_DayMAPeriod_B = 300;
                if (TB_Liquidation_TS_DayMAPeriod_C > 300) TB_Liquidation_TS_DayMAPeriod_C = 300;

                GenieConfig.TB_Liquidation_TS_DayMAPeriod_A = TB_Liquidation_TS_DayMAPeriod_A;
                GenieConfig.TB_Liquidation_TS_DayMAPeriod_B = TB_Liquidation_TS_DayMAPeriod_B;
                GenieConfig.TB_Liquidation_TS_DayMAPeriod_C = TB_Liquidation_TS_DayMAPeriod_C;

                form.TB_Liquidation_TS_DayMAPeriod_A.Text = GenieConfig.TB_Liquidation_TS_DayMAPeriod_A.ToString();
                form.TB_Liquidation_TS_DayMAPeriod_B.Text = GenieConfig.TB_Liquidation_TS_DayMAPeriod_B.ToString();
                form.TB_Liquidation_TS_DayMAPeriod_C.Text = GenieConfig.TB_Liquidation_TS_DayMAPeriod_C.ToString();
            }
            catch (Exception e)
            {
                Form1.Console_print("계좌관리_저장 / 청산 TS 일이평 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 청산 TS 일이평 입력 오류 : " + e.Message);
            }

            GenieConfig.CB_Liquidation_TS_A = form.CB_Liquidation_TS_A.Checked;
            GenieConfig.CB_Liquidation_TS_B = form.CB_Liquidation_TS_B.Checked;
            GenieConfig.CB_Liquidation_TS_C = form.CB_Liquidation_TS_C.Checked;

            GenieConfig.CBB_Liquidation_TS_MinMAPeriod_A = GET.ComboBoxIndex(form.CBB_Liquidation_TS_MinMAPeriod_A);
            GenieConfig.CBB_Liquidation_TS_MinMAPeriod_B = GET.ComboBoxIndex(form.CBB_Liquidation_TS_MinMAPeriod_B);
            GenieConfig.CBB_Liquidation_TS_MinMAPeriod_C = GET.ComboBoxIndex(form.CBB_Liquidation_TS_MinMAPeriod_C);
            GenieConfig.CBB_Liquidation_TS_DayMAPeriod_A = GET.ComboBoxIndex(form.CBB_Liquidation_TS_DayMAPeriod_A);
            GenieConfig.CBB_Liquidation_TS_DayMAPeriod_B = GET.ComboBoxIndex(form.CBB_Liquidation_TS_DayMAPeriod_B);
            GenieConfig.CBB_Liquidation_TS_DayMAPeriod_C = GET.ComboBoxIndex(form.CBB_Liquidation_TS_DayMAPeriod_C);


            MA.Get_MA();
        }

        private void Form_AccountManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                Form1.form1.CB_계좌관리.Checked = false;

                this.MinimumSize = new Size(0, 0); // 임시 해제
                this.MaximumSize = new Size(1936, 389);
                this.MinimumSize = new Size(1936, 389);
                this.Size = new Size(1936, 389);
            }
        }


        private void CB_레이아웃고정_계좌관리_CheckedChanged(object sender, EventArgs e)
        {
            GenieConfig.CB_레이아웃고정_계좌관리 = CB_레이아웃고정_계좌관리.Checked;

            if (!CB_레이아웃고정_계좌관리.Checked) LayoutChange.CBB_layout_SelectedIndex(-1);
            else LayoutChange.CBB_layout_SelectedIndex(GenieConfig.CBB_layout);
        }


        private void CB_rebalance_option_CheckedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);
            CheckBox CB = (sender as CheckBox);
            if (CB.Checked)
            {
                CB.Text = "단위체결 별";
                CB.ForeColor = Color.Red;
            }
            else
            {
                CB.Text = "전량체결 후";
                CB.ForeColor = Color.Blue;
            }
        }

        private static void 확인버튼클릭()
        {
            Form1.form1.CB_계좌관리.Checked = true;

            Form1.form1.panel_tab_주문.Size = new Size(820, 999);
            Form1.form1.tab_주문.SelectedIndex = 2;
            Form1.form1.tab_주문.Size = new Size(828, 1005);
            Form1.form1.tab_주문.BringToFront();
            Form1.form1.panel_tab_주문.BringToFront();

            Form1.form1.TP_동작상태.Controls.Add(Form1.form1.LB_JumunList);
            Form1.form1.LB_JumunList.Location = new Point(-1, 3);
            Form1.form1.LB_JumunList.Size = new Size(821, 975);

            Form1.form1.DGV_최종매입가View.Rows.Clear();
            Method.SortClear(Form1.form1.DGV_최종매입가View);
            Form1.form1.TP_동작상태.Controls.Remove(Form1.form1.DGV_최종매입가View);
            Form1.form1.panel_tab_주문.Controls.Remove(Form1.form1.Label_주문row);
            Form1.form1.panel_tab_주문.Controls.Remove(Form1.form1.TB_주문row);
            Form1.form1.panel_tab_주문.Controls.Remove(Form1.form1.CBB_최종가종목);
            Form1.form1.panel_tab_주문.Controls.Remove(Form1.form1.BT_종목업);
            Form1.form1.panel_tab_주문.Controls.Remove(Form1.form1.BT_종목다운);
        }

        private void BT_감시확인_Click(object sender, EventArgs e)
        {
            확인버튼클릭();
            this.ActiveControl = null;

            Form1.form1.TP_동작상태.Controls.Add(Form1.form1.LB_JumunList);
            Form1.form1.LB_JumunList.BringToFront();

            this.MaximumSize = new Size(1118, 389);
            this.MinimumSize = new Size(1118, 389);
            this.Size = new Size(1118, 389);

            Form1.form1.동작상태보기 = false;
            Form1.form1.일반주문확인 = true;
            Form1.form1.LB_JumunList.SelectionMode = SelectionMode.MultiExtended;

            Tab_AccountManagement.감시주문_확인("일반주문");
        }

        private void BT_감시삭제_Click_1(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            if (Form1.form1.LB_JumunList.SelectedItems.Count > 0)
            {
                Form1.MBC_sender = (sender as Button).Name;
                Form1.중요메세지("감시주문취소", "예약된 감시주문을 취소 하겠습니까?");
            }
        }

        private void BT_최종매입가감시주문_Click(object sender, EventArgs e)
        {
            확인버튼클릭();
            this.ActiveControl = null;

            this.MaximumSize = new Size(1118, 389);
            this.MinimumSize = new Size(1118, 389);
            this.Size = new Size(1118, 389);

            Form1.form1.동작상태보기 = false;
            Form1.form1.일반주문확인 = false;
            Form1.form1.LB_JumunList.SelectionMode = SelectionMode.MultiExtended;

            Tab_AccountManagement.감시주문_확인("최종주문");
        }


        private void BT_최종매입가_Click(object sender, EventArgs e)
        {
            확인버튼클릭();
            this.ActiveControl = null;

            Form1.form1.TP_동작상태.Controls.Remove(Form1.form1.LB_JumunList);
            Form1.form1.TP_동작상태.Controls.Add(Form1.form1.DGV_최종매입가View);

            Form1.form1.panel_tab_주문.Controls.Add(Form1.form1.CBB_최종가종목);
            Form1.form1.panel_tab_주문.Controls.Add(Form1.form1.BT_종목업);
            Form1.form1.panel_tab_주문.Controls.Add(Form1.form1.BT_종목다운);
            Form1.form1.CBB_최종가종목.BringToFront();
            Form1.form1.BT_종목업.BringToFront();
            Form1.form1.BT_종목다운.BringToFront();

            Form1.form1.DGV_최종매입가View.Location = new Point(-2, -2);
            Form1.form1.DGV_최종매입가View.Size = new Size(825, 980);

            this.MaximumSize = new Size(1118, 389);
            this.MinimumSize = new Size(1118, 389);
            this.Size = new Size(1118, 389);

            Form1.form1.동작상태보기 = false;
            Form1.form1.일반주문확인 = false;

            Form1.form1.DGV_최종매입가View.Rows.Clear();
            Method.SortClear(Form1.form1.DGV_최종매입가View);
            Form1.form1.CBB_최종가종목.Items.Clear();

            if (Form1.stockBalanceList.Count > 0)
            {
                var newList = from pair in Form1.stockBalanceList orderby pair.Value.초기매수일 descending select pair;
                foreach (var item in newList.ToList())
                {
                    Form1.form1.CBB_최종가종목.Items.Add(item.Value.종목명);
                }

                Form1.form1.CBB_최종가종목.SelectedIndex = 0; 
            }
        }

        private void CB_cut_CheckedChanged(object sender, EventArgs e)
        {
            // 1. 철벽 방어막 & 단일 캐스팅: 체크박스가 아니거나 글자가 2글자 미만이면 즉시 컷! (뻗음 방지)
            if (!(sender is CheckBox 클릭된_체크박스) || string.IsNullOrEmpty(클릭된_체크박스.Text) || 클릭된_체크박스.Text.Length < 2) return;

            // 2. 비프음 최적화: 삼항 연산자로 묶어서 한 줄로 깔끔하게 처리
            if (Form1.로딩완료)
            {
                Form1.비프음(클릭된_체크박스.Checked ? "체크" : "언체크");
            }

            // 3. 목표 텍스트 0.001초 만에 조립 (앞 2글자 + 기호)
            string 앞글자 = 클릭된_체크박스.Text[..2];
            string 목표_텍스트 = 앞글자 + (클릭된_체크박스.Checked ? "■" : "□");

            // 4. UI 렌더링 최적화: 진짜 다를 때만 덮어써서 화면 깜빡임 차단
            if (클릭된_체크박스.Text != 목표_텍스트)
            {
                클릭된_체크박스.Text = 목표_텍스트;
            }

            // 5. 로직 최적화: 체크 해제(false) 상태이고 폼이 열려있을 때만 실행
            if (!클릭된_체크박스.Checked && Form1.FormAccountManagement_Open)
            {
                // [최적화 핵심] 무거운 Equals 3연속 비교 대신, Switch문으로 단번에 목적지 점프!
                switch (클릭된_체크박스.Name)
                {
                    case "CB_cut_A":
                        Form1.form1.Cut_A = false;
                        Form1.str.cut_LB_A = "X";
                        GenieConfig.CB_cut_A = false;
                        // 다른 화면(form)의 UI를 건드릴 때도 똑같이 렌더링 최적화 적용!
                        if (form.CB_cut_LB_A.Text != "X") form.CB_cut_LB_A.Text = "X";
                        break;

                    case "CB_cut_B":
                        Form1.form1.Cut_B = false;
                        Form1.str.cut_LB_B = "X";
                        GenieConfig.CB_cut_B = false;
                        if (form.CB_cut_LB_B.Text != "X") form.CB_cut_LB_B.Text = "X";
                        break;

                    case "CB_cut_C":
                        Form1.form1.Cut_C = false;
                        Form1.str.cut_LB_C = "X";
                        GenieConfig.CB_cut_C = false;
                        if (form.CB_cut_LB_C.Text != "X") form.CB_cut_LB_C.Text = "X";
                        break;
                }
            }
        }

        private void CheckBox_Check_TEXT_Changed(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            // 1. 철벽 방어막: 체크박스가 아니거나 텍스트가 텅 비었으면 즉시 탈출 (프로그램 뻗음 방지)
            if (!(sender is CheckBox 클릭된_체크박스) || string.IsNullOrEmpty(클릭된_체크박스.Text)) return;

            // 2. 텍스트 안전 분리: 혹시 글자가 1글자밖에 없더라도 뻗지 않도록 길이 방어막 추가
            string 나머지_텍스트 = 클릭된_체크박스.Text.Length > 1 ? 클릭된_체크박스.Text[1..] : "";

            // 3. 목표 텍스트 조립 후, 다를 때만 UI 렌더링
            string 목표_텍스트 = (클릭된_체크박스.Checked ? "■" : "□") + 나머지_텍스트;
            if (클릭된_체크박스.Text != 목표_텍스트)
            {
                클릭된_체크박스.Text = 목표_텍스트;
            }

            // 4. [최적화 핵심] 위아래 6연속 Equals 비교를 단 1번의 Switch 문으로 압축!
            Color 목표_색상 = 클릭된_체크박스.ForeColor; // 기본값은 현재 색상 그대로 유지

            switch (클릭된_체크박스.Name)
            {
                case "CB_rebalance_기준금":
                    목표_색상 = 클릭된_체크박스.Checked ? Color.Crimson : Color.Black;
                    break;

                // 두 체크박스는 색상 변경 로직이 같으므로 하나로 묶음 (코드 다이어트)
                case "CB_Liquidation_기준금":
                case "CB_cut_기준금":
                    목표_색상 = 클릭된_체크박스.Checked ? Color.Red : Color.Black;
                    break;
            }

            // 5. 색상이 진짜로 바뀔 필요가 있을 때만 붓을 들어 칠합니다!
            if (클릭된_체크박스.ForeColor != 목표_색상)
            {
                클릭된_체크박스.ForeColor = 목표_색상;
            }
        }

        private void CheckBox_매도체크_CheckChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            // 1. 방어막: 체크박스가 아니면 즉시 컷! (프로그램 뻗음 방지)
            if (!(sender is CheckBox 클릭된_체크박스)) return;

            // 2. UI 렌더링 최적화: 현재 글자와 다를 때만 덮어써서 화면 깜빡임 방지
            string 목표_텍스트 = 클릭된_체크박스.Checked ? "■" : "□";
            if (클릭된_체크박스.Text != 목표_텍스트)
            {
                클릭된_체크박스.Text = 목표_텍스트;
            }

            CheckBox 연동된_옵션 = null;

            // 3. [최적화 핵심 1] 위아래 중복되던 14개의 Equals 대신, 
            // 단 1번의 Switch 문으로 내 짝꿍(옵션 체크박스)을 0.001초 만에 찾아냅니다!
            switch (클릭된_체크박스.Name)
            {
                case "CB_rebalance_매도체크_A": 연동된_옵션 = CB_rebalance_option_A; break;
                case "CB_rebalance_매도체크_B": 연동된_옵션 = CB_rebalance_option_B; break;
                case "CB_rebalance_매도체크_C": 연동된_옵션 = CB_rebalance_option_C; break;
                case "CB_rebalance_매도체크_D": 연동된_옵션 = CB_rebalance_option_D; break;
                case "CB_rebalance_매도체크_E": 연동된_옵션 = CB_rebalance_option_E; break;
                case "CB_rebalance_매도체크_F": 연동된_옵션 = CB_rebalance_option_F; break;
                case "CB_rebalance_매도체크_G": 연동된_옵션 = CB_rebalance_option_G; break;
            }

            // 짝꿍을 못 찾았다면 안전하게 탈출
            if (연동된_옵션 == null) return;

            // 4. [최적화 핵심 2] 내부 함수(Checked)를 지우고, 로직을 직관적으로 통합!
            if (클릭된_체크박스.Checked)
            {
                // 이미 비활성화 상태라면 알림창을 또 띄우거나 렌더링할 필요가 없습니다.
                if (연동된_옵션.Enabled)
                {
                    Form1.AutoClosingAlram("매도 체크 일때는 '전량체결 후 매도' 전용 입니다.", "[ 매도옵션알림 ]", 10, "동작");

                    if (연동된_옵션.Checked) 연동된_옵션.Checked = false;
                    연동된_옵션.Enabled = false;
                }
            }
            else
            {
                // 목표 상태와 다를 때만 활성화시켜 UI 부하를 줄입니다.
                if (!연동된_옵션.Enabled)
                {
                    연동된_옵션.Enabled = true;
                }
            }
        }


        private void AccountManagement_use_checked_chainge(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            // 1. 철벽 방어 및 캐스팅: 체크박스가 아니거나 글자가 2글자 미만이면 뻗음 방지!
            if (!(sender is CheckBox 클릭된_체크박스) || string.IsNullOrEmpty(클릭된_체크박스.Text) || 클릭된_체크박스.Text.Length < 2) return;

            // 2. 목표 텍스트 0.001초 만에 조립 (앞의 2글자만 떼어내고 네모 기호 붙이기)
            string 앞글자 = 클릭된_체크박스.Text[..2];
            string 목표_텍스트 = 앞글자 + (클릭된_체크박스.Checked ? "■" : "□");

            // 3. UI 렌더링 최적화: 현재 글자와 다를 때만 덮어써서 화면 깜빡임 방지
            if (클릭된_체크박스.Text != 목표_텍스트)
            {
                클릭된_체크박스.Text = 목표_텍스트;
            }

            if (클릭된_체크박스.Checked && Form1.FormAccountManagement_Open)
            {
                string 체크박스_이름 = 클릭된_체크박스.Name;

                switch (체크박스_이름)
                {
                    case "CB_rebalance_A": if (form.CBB_rebalance_1A.SelectedIndex == 0) { form.CBB_rebalance_1A.SelectedIndex = 1; form.TB_rebalance_sellratio1_A.Enabled = true; } if (int.TryParse(form.TB_rebalance_sellvolume1_A.Text, out int value_A) && value_A == 0) form.TB_rebalance_sellvolume1_A.Text = "50"; break;
                    case "CB_rebalance_B": if (form.CBB_rebalance_1B.SelectedIndex == 0) { form.CBB_rebalance_1B.SelectedIndex = 1; form.TB_rebalance_sellratio1_B.Enabled = true; } if (int.TryParse(form.TB_rebalance_sellvolume1_B.Text, out int value_B) && value_B == 0) form.TB_rebalance_sellvolume1_B.Text = "50"; break;
                    case "CB_rebalance_C": if (form.CBB_rebalance_1C.SelectedIndex == 0) { form.CBB_rebalance_1C.SelectedIndex = 1; form.TB_rebalance_sellratio1_C.Enabled = true; } if (int.TryParse(form.TB_rebalance_sellvolume1_C.Text, out int value_C) && value_C == 0) form.TB_rebalance_sellvolume1_C.Text = "50"; break;
                    case "CB_rebalance_D": if (form.CBB_rebalance_1D.SelectedIndex == 0) { form.CBB_rebalance_1D.SelectedIndex = 1; form.TB_rebalance_sellratio1_D.Enabled = true; } if (int.TryParse(form.TB_rebalance_sellvolume1_D.Text, out int value_D) && value_D == 0) form.TB_rebalance_sellvolume1_D.Text = "50"; break;
                    case "CB_rebalance_E": if (form.CBB_rebalance_1E.SelectedIndex == 0) { form.CBB_rebalance_1E.SelectedIndex = 1; form.TB_rebalance_sellratio1_E.Enabled = true; } if (int.TryParse(form.TB_rebalance_sellvolume1_E.Text, out int value_E) && value_E == 0) form.TB_rebalance_sellvolume1_E.Text = "50"; break;
                    case "CB_rebalance_F": if (form.CBB_rebalance_1F.SelectedIndex == 0) { form.CBB_rebalance_1F.SelectedIndex = 1; form.TB_rebalance_sellratio1_F.Enabled = true; } if (int.TryParse(form.TB_rebalance_sellvolume1_F.Text, out int value_F) && value_F == 0) form.TB_rebalance_sellvolume1_F.Text = "50"; break;
                    case "CB_rebalance_G": if (form.CBB_rebalance_1G.SelectedIndex == 0) { form.CBB_rebalance_1G.SelectedIndex = 1; form.TB_rebalance_sellratio1_G.Enabled = true; } if (int.TryParse(form.TB_rebalance_sellvolume1_G.Text, out int value_G) && value_G == 0) form.TB_rebalance_sellvolume1_G.Text = "50"; break;
                }
            }

            // 4. 로직 최적화: 체크 해제(false) 상태이고 폼이 열려있을 때만 실행
            if (!클릭된_체크박스.Checked && Form1.FormAccountManagement_Open)
            {
                string 체크박스_이름 = 클릭된_체크박스.Name;

                // [최적화 핵심 1] 무거운 Equals 10연속 검사 대신, Switch문으로 0.001초 만에 설정 변경!
                switch (체크박스_이름)
                {
                    case "CB_rebalance_A": GenieConfig.CB_rebalance_A = false; break;
                    case "CB_rebalance_B": GenieConfig.CB_rebalance_B = false; break;
                    case "CB_rebalance_C": GenieConfig.CB_rebalance_C = false; break;
                    case "CB_rebalance_D": GenieConfig.CB_rebalance_D = false; break;
                    case "CB_rebalance_E": GenieConfig.CB_rebalance_E = false; break;
                    case "CB_rebalance_F": GenieConfig.CB_rebalance_F = false; break;
                    case "CB_rebalance_G": GenieConfig.CB_rebalance_G = false; break;
                    case "CB_Liquidation_A": GenieConfig.CB_Liquidation_A = false; break;
                    case "CB_Liquidation_B": GenieConfig.CB_Liquidation_B = false; break;
                    case "CB_Liquidation_C": GenieConfig.CB_Liquidation_C = false; break;
                }

                // [최적화 핵심 2] 잔고 리스트를 순회할 때도 무거운 객체 비교 없이 Switch문으로 광속 점프!
                foreach (var 잔고 in Form1.stockBalanceList.Values)
                {
                    switch (체크박스_이름)
                    {
                        case "CB_rebalance_A": 잔고.리벨A = "A"; break;
                        case "CB_rebalance_B": 잔고.리벨B = "B"; break;
                        case "CB_rebalance_C": 잔고.리벨C = "C"; break;
                        case "CB_rebalance_D": 잔고.리벨D = "D"; break;
                        case "CB_rebalance_E": 잔고.리벨E = "E"; break;
                        case "CB_rebalance_F": 잔고.리벨F = "F"; break;
                        case "CB_rebalance_G": 잔고.리벨G = "G"; break;
                        case "CB_Liquidation_A": 잔고.청산A = "A"; break;
                        case "CB_Liquidation_B": 잔고.청산B = "B"; break;
                        case "CB_Liquidation_C": 잔고.청산C = "C"; break;
                    }
                }
            }
        }

        private void BT_계좌관리저장_Click(object sender, EventArgs e)
        {
            Form1.form1.Select();
            Form1.MBC_sender = (sender as Button).Name;
            Form1.중요메세지("계좌관리", "계좌관리 설정을 저장 하시겠습니까?");
        }

        private void BT_기준금적용_Click(object sender, EventArgs e)
        {
            Form1.form1.Select();
            if (sender.Equals(BT_매수기준금적용))
            {
                Form1.MBC_sender = (sender as Button).Name;
                if (CB_매수기준.Checked)
                    Form1.중요메세지("매수기준금 자동적용", "'매수기준금'을 (투자원금 + 누적실손)의 비율로 계산 하여 적용 하겠습니까?");
                else
                    Form1.중요메세지("매수기준금 기준금자동적용", "'매수기준금'을 투자원금의 비율로 초기화 하겠습니까?");
            }

            if (sender.Equals(BT_손익기준금적용))
            {
                Form1.MBC_sender = (sender as Button).Name;
                if (CB_손익기준.Checked)
                    Form1.중요메세지("손익기준금 자동적용", "'손익기준금' 을 (투자원금 + 누적실손)의 비율로 계산 하여 적용 하겠습니까?");
                else
                    Form1.중요메세지("손익기준금 자동적용", "'손익기준금' 을 투자원금의 비율로 초기화 하겠습니까?");
            }
        }

        private void CheckBox_TextChange_Choicechecked(object sender, EventArgs e)
        {
            FormPrint.TextChange_매매방법(sender);
        }

        private void CBB_DropDownClosed(object sender, EventArgs e)
        {
            Form1.비프음("체크");
        }

        private void CBB_rebalance_DropDownClosed(object sender, EventArgs e)
        {
            Form1.비프음("체크");
            if (!(sender is ComboBox 클릭된_콤보박스)) return;
            체크박스_연동_해제(클릭된_콤보박스);
            
            CBB_rebalance_DropDownClosed_(sender);
        }

        private void 체크박스_연동_해제(ComboBox 클릭된_콤보박스)
        {
            // 1. 방어막: 선택값이 0(사용 안 함)이 아니면 이 함수는 할 일이 없으므로 즉시 탈출
            if (클릭된_콤보박스.SelectedIndex != 0) return;

            // 2. 콤보박스 이름에 맞는 짝꿍 체크박스를 찾아서 렌더링 부하 없이 꺼줍니다.
            switch (클릭된_콤보박스.Name)
            {
                case "CBB_rebalance_1A":
                    if (CB_rebalance_A.Checked) CB_rebalance_A.Checked = false;
                    break;

                case "CBB_rebalance_1B":
                    if (CB_rebalance_B.Checked) CB_rebalance_B.Checked = false;
                    break;

                case "CBB_rebalance_1C":
                    if (CB_rebalance_C.Checked) CB_rebalance_C.Checked = false;
                    break;

                case "CBB_rebalance_1D":
                    if (CB_rebalance_D.Checked) CB_rebalance_D.Checked = false;
                    break;

                case "CBB_rebalance_1E":
                    if (CB_rebalance_E.Checked) CB_rebalance_E.Checked = false;
                    break;

                case "CBB_rebalance_1F":
                    if (CB_rebalance_F.Checked) CB_rebalance_F.Checked = false;
                    break;

                case "CBB_rebalance_1G":
                    if (CB_rebalance_G.Checked) CB_rebalance_G.Checked = false;
                    break;
            }
        }

        private void CBB_rebalance_DropDownClosed_(object sender)
        {
            // 1. 방어막: 콤보박스가 아니면 즉시 탈출
            if (!(sender is ComboBox 클릭된_콤보박스)) return;

            TextBox 비중입력1 = null;
            TextBox 비중입력2 = null;
            ComboBox 연동콤보박스2 = null;
            bool 그룹1_여부 = false; // 1그룹(1A~1G)인지 2그룹(2A~2G)인지 구분하는 마법의 스위치

            // 2. [최적화 핵심 1] 무거운 Equals 13연속 타격 대신, Name으로 0.001초 만에 짝꿍을 찾습니다.
            switch (클릭된_콤보박스.Name)
            {
                // --- 그룹 1 (1A ~ 1G) ---
                case "CBB_rebalance_1A": 비중입력1 = TB_rebalance_sellratio1_A; 비중입력2 = TB_rebalance_sellratio2_A; 연동콤보박스2 = CBB_rebalance_2A; 그룹1_여부 = true; break;
                case "CBB_rebalance_1B": 비중입력1 = TB_rebalance_sellratio1_B; 비중입력2 = TB_rebalance_sellratio2_B; 연동콤보박스2 = CBB_rebalance_2B; 그룹1_여부 = true; break;
                case "CBB_rebalance_1C": 비중입력1 = TB_rebalance_sellratio1_C; 비중입력2 = TB_rebalance_sellratio2_C; 연동콤보박스2 = CBB_rebalance_2C; 그룹1_여부 = true; break;
                case "CBB_rebalance_1D": 비중입력1 = TB_rebalance_sellratio1_D; 비중입력2 = TB_rebalance_sellratio2_D; 연동콤보박스2 = CBB_rebalance_2D; 그룹1_여부 = true; break; 
                case "CBB_rebalance_1E": 비중입력1 = TB_rebalance_sellratio1_E; 비중입력2 = TB_rebalance_sellratio2_E; 연동콤보박스2 = CBB_rebalance_2E; 그룹1_여부 = true; break;
                case "CBB_rebalance_1F": 비중입력1 = TB_rebalance_sellratio1_F; 비중입력2 = TB_rebalance_sellratio2_F; 연동콤보박스2 = CBB_rebalance_2F; 그룹1_여부 = true; break;
                case "CBB_rebalance_1G": 비중입력1 = TB_rebalance_sellratio1_G; 비중입력2 = TB_rebalance_sellratio2_G; 연동콤보박스2 = CBB_rebalance_2G; 그룹1_여부 = true; break;

                // --- 그룹 2 (2A ~ 2G) ---
                case "CBB_rebalance_2A": 비중입력1 = TB_rebalance_sellratio2_A; break;
                case "CBB_rebalance_2B": 비중입력1 = TB_rebalance_sellratio2_B; break;
                case "CBB_rebalance_2C": 비중입력1 = TB_rebalance_sellratio2_C; break;
                case "CBB_rebalance_2D": 비중입력1 = TB_rebalance_sellratio2_D; break;
                case "CBB_rebalance_2E": 비중입력1 = TB_rebalance_sellratio2_E; break;
                case "CBB_rebalance_2F": 비중입력1 = TB_rebalance_sellratio2_F; break;
                case "CBB_rebalance_2G": 비중입력1 = TB_rebalance_sellratio2_G; break;
            }

            // 짝꿍(비중입력1)을 못 찾았으면 안전하게 탈출
            if (비중입력1 == null) return;

            // 3. [최적화 핵심 2] 사용제한 1, 2 함수 통합 & 이중 렌더링 차단
            if (클릭된_콤보박스.SelectedIndex == 0)
            {
                if (비중입력1.Text != "0") 비중입력1.Text = "0";
                if (비중입력1.Enabled) 비중입력1.Enabled = false;

                if (그룹1_여부)
                {
                    if (비중입력2 != null && 비중입력2.Text != "0") 비중입력2.Text = "0";
                    if (비중입력2 != null && 비중입력2.Enabled) 비중입력2.Enabled = false;
                    if (연동콤보박스2 != null && 연동콤보박스2.SelectedIndex != 0) 연동콤보박스2.SelectedIndex = 0;
                    if (연동콤보박스2 != null && 연동콤보박스2.Enabled) 연동콤보박스2.Enabled = false;
                }
            }
            else
            {
                if (!비중입력1.Enabled) 비중입력1.Enabled = true;

                if (그룹1_여부)
                {
                    if (비중입력2 != null && !비중입력2.Enabled) 비중입력2.Enabled = true;
                    if (연동콤보박스2 != null && !연동콤보박스2.Enabled) 연동콤보박스2.Enabled = true;
                }
            }

            // 4. [최적화 핵심 3] 마이너스 부호 처리 (Substring 뻗음 완벽 방어)
            if (클릭된_콤보박스.SelectedIndex > 6)
            {
                if (!비중입력1.Text.StartsWith("-")) 비중입력1.Text = "-" + 비중입력1.Text;
            }
            else
            {
                if (비중입력1.Text.StartsWith("-"))
                {
                    // 글자가 "-" 한 개만 달랑 있을 때 뻗는 버그 방어
                    if (비중입력1.Text.Length > 1)
                        비중입력1.Text = 비중입력1.Text[1..];
                    else
                        비중입력1.Text = "0"; // 마이너스만 있으면 0으로 초기화
                }
            }
        }

        private void Combo_use_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Form1.FormAccountManagement_Open) Condition_Management.Combo_use_condition_SelectedIndexChanged(sender);
        }
        private void Combo_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Form1.FormAccountManagement_Open) Condition_Management.Combo_condition_SelectedIndexChanged(sender);
        }

        private void CBB_jumun_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrint.CBB_jumun_SelectedIndex(sender);
        }

        private void Combo_condition_MouseHover(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            toolTip1.SetToolTip(combo, combo.Text);
        }


        private void CBB_rebalance_suik_gubun_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.로딩완료) Form1.비프음("체크");

            FormPrint.CBB_suik_DropDownClosed(sender);
        }

        private void CBB_Liquidation_suik_gubun_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.로딩완료) Form1.비프음("체크");

            FormPrint.CBB_suik_DropDownClosed(sender);
        }

        private void Combo_Condition_Add(object sender, EventArgs e)
        {
            Condition_Management.Condition_Add(sender);
        }

        private void Combo_Condition_TextChanged(object sender, EventArgs e)
        {

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

            TextBox_0입력(sender);
        }

        private void TextBox_음수만입력_소수2자리제한(object sender, EventArgs e) // 사용
        {
            TextValue.TextBox_음수만입력_소수2자리제한(sender);
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

            TextBox TB = sender as TextBox;
            TextBox TB_1 = TB_rebalance_sellvolume1_A;
            CheckBox CB = CB_rebalance_option_A;
            ComboBox CBB = CBB_rebalance_2A;

            if (sender.Equals(TB_rebalance_sellvolume1_A)) volume();
            if (sender.Equals(TB_rebalance_sellvolume1_B)) { CB = CB_rebalance_option_B; CBB = CBB_rebalance_2B; volume(); }
            if (sender.Equals(TB_rebalance_sellvolume1_C)) { CB = CB_rebalance_option_C; CBB = CBB_rebalance_2C; volume(); }
            if (sender.Equals(TB_rebalance_sellvolume1_D)) { CB = CB_rebalance_option_D; CBB = CBB_rebalance_2D; volume(); }
            if (sender.Equals(TB_rebalance_sellvolume1_E)) { CB = CB_rebalance_option_E; CBB = CBB_rebalance_2E; volume(); }
            if (sender.Equals(TB_rebalance_sellvolume1_F)) { CB = CB_rebalance_option_F; CBB = CBB_rebalance_2F; volume(); }
            if (sender.Equals(TB_rebalance_sellvolume1_G)) { CB = CB_rebalance_option_G; CBB = CBB_rebalance_2G; volume(); }

            void volume()
            {
                int.TryParse(TB.Text, out int N);
                if (N >= 100)
                {
                    TB.Text = "100";

                    if (CBB.SelectedIndex > 0)
                    {

                        if (CB.Checked)
                        {
                            Form1.AutoClosingAlram("1차매도 비중이 100% 일떄는 '전량체결 후 매도' 전용 입니다. 1,2차 '감시'주문중 한쪽이 먼저 체결되면 1,2차 같이 삭제 됩니다.", "[ 매도옵션알림 ]", 10, "동작");
                            CB.Checked = false;
                        }

                        CB.Enabled = false;
                    }
                }
                else
                {
                    CB.Enabled = true;
                }
            }

            if (sender.Equals(TB_rebalance_sellvolume2_A)) { volume2(); }
            if (sender.Equals(TB_rebalance_sellvolume2_B)) { TB_1 = TB_rebalance_sellvolume1_B; volume2(); }
            if (sender.Equals(TB_rebalance_sellvolume2_C)) { TB_1 = TB_rebalance_sellvolume1_C; volume2(); }
            if (sender.Equals(TB_rebalance_sellvolume2_D)) { TB_1 = TB_rebalance_sellvolume1_D; volume2(); }
            if (sender.Equals(TB_rebalance_sellvolume2_E)) { TB_1 = TB_rebalance_sellvolume1_E; volume2(); }
            if (sender.Equals(TB_rebalance_sellvolume2_F)) { TB_1 = TB_rebalance_sellvolume1_F; volume2(); }
            if (sender.Equals(TB_rebalance_sellvolume2_G)) { TB_1 = TB_rebalance_sellvolume1_G; volume2(); }

            void volume2()
            {
                int.TryParse(TB_1.Text, out int N_1);

                if (N_1 == 100)
                {
                    int.TryParse(TB.Text, out int N);
                    if (N > 100) TB.Text = "100";
                }
                else
                {
                    int.TryParse(TB.Text, out int N);
                    if (N_1 + N > 100) TB.Text = (100 - N_1).ToString();
                }
            }
        }

        private void 양수실수_키프레스_(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, false); // textbox 에 양수 , 실수 숫자만 입력 받을수 있음
        }

        private void 양수음수실수_키프레스_(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, true); // textbox 에 양수, 음수 , 실수 숫자만 입력 받을수 있음
        }

        public void TextBox_0입력(object sender) //textbox 의 색표시  사용
        {
            int index = 0;
            TextBox textbox = (sender as TextBox);
            if (sender.Equals(TB_rebalance_suik_1_A)) index = combo_rebalance_suik_gubun_A.SelectedIndex;
            if (sender.Equals(TB_rebalance_suik_1_B)) index = combo_rebalance_suik_gubun_B.SelectedIndex;
            if (sender.Equals(TB_rebalance_suik_1_C)) index = combo_rebalance_suik_gubun_C.SelectedIndex;
            if (sender.Equals(TB_rebalance_suik_1_D)) index = combo_rebalance_suik_gubun_D.SelectedIndex;
            if (sender.Equals(TB_rebalance_suik_1_E)) index = combo_rebalance_suik_gubun_E.SelectedIndex;
            if (sender.Equals(TB_rebalance_suik_1_F)) index = combo_rebalance_suik_gubun_F.SelectedIndex;
            if (sender.Equals(TB_rebalance_suik_1_G)) index = combo_rebalance_suik_gubun_G.SelectedIndex;
            if (sender.Equals(TB_Liquidation_suik_1_A)) index = CBB_Liquidation_suik_gubun_A.SelectedIndex;
            if (sender.Equals(TB_Liquidation_suik_1_B)) index = CBB_Liquidation_suik_gubun_B.SelectedIndex;
            if (sender.Equals(TB_Liquidation_suik_1_C)) index = CBB_Liquidation_suik_gubun_C.SelectedIndex;

            textbox.Text = TextValue.TextBox_0입력(index, textbox.Text);
        }

        private void 콤보박스_전량체결후매도제한(object sender, EventArgs e)
        {
            CheckBox CB_;
            TextBox TB_;

            if (sender.Equals(CBB_rebalance_2A)) { CB_ = CB_rebalance_option_A; TB_ = TB_rebalance_sellvolume1_A; volume(CB_, TB_); }
            else if (sender.Equals(CBB_rebalance_2B)) { CB_ = CB_rebalance_option_B; TB_ = TB_rebalance_sellvolume1_B; volume(CB_, TB_); }
            else if (sender.Equals(CBB_rebalance_2C)) { CB_ = CB_rebalance_option_C; TB_ = TB_rebalance_sellvolume1_C; volume(CB_, TB_); }
            else if (sender.Equals(CBB_rebalance_2D)) { CB_ = CB_rebalance_option_D; TB_ = TB_rebalance_sellvolume1_D; volume(CB_, TB_); }
            else if (sender.Equals(CBB_rebalance_2E)) { CB_ = CB_rebalance_option_E; TB_ = TB_rebalance_sellvolume1_E; volume(CB_, TB_); }
            else if (sender.Equals(CBB_rebalance_2F)) { CB_ = CB_rebalance_option_F; TB_ = TB_rebalance_sellvolume1_F; volume(CB_, TB_); }
            else if (sender.Equals(CBB_rebalance_2G)) { CB_ = CB_rebalance_option_G; TB_ = TB_rebalance_sellvolume1_G; volume(CB_, TB_); }

            void volume(CheckBox CB, TextBox TB)
            {
                if ((sender as ComboBox).SelectedIndex > 0)
                {
                    int.TryParse(TB.Text, out int N);
                    if (N >= 100)
                    {
                        if (CB.Checked)
                        {
                            Form1.AutoClosingAlram("1차매도 비중이 100% 일떄는 '전량체결 후 매도' 전용 입니다.", "[ 매도옵션알림 ]", 10, "동작");
                            CB.Checked = false;
                        }

                        CB.Enabled = false;
                    }
                }
                else
                {
                    if (!GenieConfig.CB_가이드매매) CB.Enabled = true;
                }
            }
        }


        private void 숫자콤마넣기_TextChanged(object sender, EventArgs e)
        {
            TextValue.숫자콤마넣기_TextChanged(sender);
        }

        private void Combo_cancel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrint.Combo_cancel_SelectedIndexChanged(sender);
        }

        bool 소리1 = false;
        bool 소리2 = false;

        private void CB_리밸TS_1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                Form1.비프음("체크");


                CB_rebalance_TS_1차_A.Checked = GenieConfig.CB_rebalance_TS_1차_A;
                CB_rebalance_TS_1차_B.Checked = GenieConfig.CB_rebalance_TS_1차_B;
                CB_rebalance_TS_1차_C.Checked = GenieConfig.CB_rebalance_TS_1차_C;
                CB_rebalance_TS_1차_D.Checked = GenieConfig.CB_rebalance_TS_1차_D;
                CB_rebalance_TS_1차_E.Checked = GenieConfig.CB_rebalance_TS_1차_E;
                CB_rebalance_TS_1차_F.Checked = GenieConfig.CB_rebalance_TS_1차_F;
                CB_rebalance_TS_1차_G.Checked = GenieConfig.CB_rebalance_TS_1차_G;

                TB_rebalance_TS_1차_down_A.Text = GenieConfig.TB_rebalance_TS_1차_down_A.ToString();
                TB_rebalance_TS_1차_down_B.Text = GenieConfig.TB_rebalance_TS_1차_down_B.ToString();
                TB_rebalance_TS_1차_down_C.Text = GenieConfig.TB_rebalance_TS_1차_down_C.ToString();
                TB_rebalance_TS_1차_down_D.Text = GenieConfig.TB_rebalance_TS_1차_down_D.ToString();
                TB_rebalance_TS_1차_down_E.Text = GenieConfig.TB_rebalance_TS_1차_down_E.ToString();
                TB_rebalance_TS_1차_down_F.Text = GenieConfig.TB_rebalance_TS_1차_down_F.ToString();
                TB_rebalance_TS_1차_down_G.Text = GenieConfig.TB_rebalance_TS_1차_down_G.ToString();

                TB_rebalance_TS_1차_MinMAPeriod_A.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_A.ToString();
                TB_rebalance_TS_1차_MinMAPeriod_B.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_B.ToString();
                TB_rebalance_TS_1차_MinMAPeriod_C.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_C.ToString();
                TB_rebalance_TS_1차_MinMAPeriod_D.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_D.ToString();
                TB_rebalance_TS_1차_MinMAPeriod_E.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_E.ToString();
                TB_rebalance_TS_1차_MinMAPeriod_F.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_F.ToString();
                TB_rebalance_TS_1차_MinMAPeriod_G.Text = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_G.ToString();

                CBB_rebalance_TS_1차_MinMAPeriod_A.SelectedIndex = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_A;
                CBB_rebalance_TS_1차_MinMAPeriod_B.SelectedIndex = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_B;
                CBB_rebalance_TS_1차_MinMAPeriod_C.SelectedIndex = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_C;
                CBB_rebalance_TS_1차_MinMAPeriod_D.SelectedIndex = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_D;
                CBB_rebalance_TS_1차_MinMAPeriod_E.SelectedIndex = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_E;
                CBB_rebalance_TS_1차_MinMAPeriod_F.SelectedIndex = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_F;
                CBB_rebalance_TS_1차_MinMAPeriod_G.SelectedIndex = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_G;

                CB_리밸TS_2.Checked = false;
                panel_리밸TS_1.BringToFront();
                panel_리밸TS_1.Show();
                소리1 = true;
            }
            else
            {
                Form1.비프음("언체크");
                panel_리밸TS_1.Hide();
                소리1 = false;
            }
        }

        private void CB_리밸TS_2_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                Form1.비프음("체크");

                CB_rebalance_TS_2차_A.Checked = GenieConfig.CB_rebalance_TS_2차_A;
                CB_rebalance_TS_2차_B.Checked = GenieConfig.CB_rebalance_TS_2차_B;
                CB_rebalance_TS_2차_C.Checked = GenieConfig.CB_rebalance_TS_2차_C;
                CB_rebalance_TS_2차_D.Checked = GenieConfig.CB_rebalance_TS_2차_D;
                CB_rebalance_TS_2차_E.Checked = GenieConfig.CB_rebalance_TS_2차_E;
                CB_rebalance_TS_2차_F.Checked = GenieConfig.CB_rebalance_TS_2차_F;
                CB_rebalance_TS_2차_G.Checked = GenieConfig.CB_rebalance_TS_2차_G;

                TB_rebalance_TS_2차_down_A.Text = GenieConfig.TB_rebalance_TS_2차_down_A.ToString();
                TB_rebalance_TS_2차_down_B.Text = GenieConfig.TB_rebalance_TS_2차_down_B.ToString();
                TB_rebalance_TS_2차_down_C.Text = GenieConfig.TB_rebalance_TS_2차_down_C.ToString();
                TB_rebalance_TS_2차_down_D.Text = GenieConfig.TB_rebalance_TS_2차_down_D.ToString();
                TB_rebalance_TS_2차_down_E.Text = GenieConfig.TB_rebalance_TS_2차_down_E.ToString();
                TB_rebalance_TS_2차_down_F.Text = GenieConfig.TB_rebalance_TS_2차_down_F.ToString();
                TB_rebalance_TS_2차_down_G.Text = GenieConfig.TB_rebalance_TS_2차_down_G.ToString();

                TB_rebalance_TS_2차_MinMAPeriod_A.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_A.ToString();
                TB_rebalance_TS_2차_MinMAPeriod_B.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_B.ToString();
                TB_rebalance_TS_2차_MinMAPeriod_C.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_C.ToString();
                TB_rebalance_TS_2차_MinMAPeriod_D.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_D.ToString();
                TB_rebalance_TS_2차_MinMAPeriod_E.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_E.ToString();
                TB_rebalance_TS_2차_MinMAPeriod_F.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_F.ToString();
                TB_rebalance_TS_2차_MinMAPeriod_G.Text = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_G.ToString();

                CBB_rebalance_TS_2차_MinMAPeriod_A.SelectedIndex = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_A;
                CBB_rebalance_TS_2차_MinMAPeriod_B.SelectedIndex = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_B;
                CBB_rebalance_TS_2차_MinMAPeriod_C.SelectedIndex = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_C;
                CBB_rebalance_TS_2차_MinMAPeriod_D.SelectedIndex = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_D;
                CBB_rebalance_TS_2차_MinMAPeriod_E.SelectedIndex = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_E;
                CBB_rebalance_TS_2차_MinMAPeriod_F.SelectedIndex = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_F;
                CBB_rebalance_TS_2차_MinMAPeriod_G.SelectedIndex = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_G;

                CB_리밸TS_1.Checked = false;
                panel_리밸TS_2.BringToFront();
                panel_리밸TS_2.Show();
                소리2 = true;
            }
            else
            {
                Form1.비프음("언체크");
                panel_리밸TS_2.Hide();
                소리2 = false;
            }
        }

        private void CB_트레일링스탑1차_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);
            if (소리1) Form1.form1.체크박스_비프(sender);

            string text = CB.Text.Split(' ')[0];
            if (CB.Checked)
            {
                CB.Text = text + " 1차 TS run";
                CB.ForeColor = Color.Red;
            }
            else
            {
                CB.Text = text + " 1차 TS off";
                CB.ForeColor = Color.Black;
            }
        }

        private void CB_트레일링스탑2차_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);
            if (소리2) Form1.form1.체크박스_비프(sender);

            string text = CB.Text.Split(' ')[0];
            if (CB.Checked)
            {
                CB.Text = text + " 2차 TS run";
                CB.ForeColor = Color.Red;
            }
            else
            {
                CB.Text = text + " 2차 TS off";
                CB.ForeColor = Color.Black;
            }
        }

        private void CB_Liquidation_TS_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);
            if (Form1.FormAccountManagement_Open) Form1.form1.체크박스_비프(sender);

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
        }

        private void CB_mma_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_mma.Checked)
            {
                계좌관리_P.Controls.Add(panel_mma);
                panel_mma.BringToFront();
                panel_mma.Show();
                TB_rebalance_MinMAPeriod1_A.Enabled = true;
                TB_rebalance_MinMAPeriod1_B.Enabled = true;
                TB_rebalance_MinMAPeriod1_C.Enabled = true;
                TB_rebalance_MinMAPeriod1_D.Enabled = true;
                TB_rebalance_MinMAPeriod1_E.Enabled = true;
                TB_rebalance_MinMAPeriod1_F.Enabled = true;
                TB_rebalance_MinMAPeriod1_G.Enabled = true;
                CBB_rebalance_MinMAPeriod1_A.Enabled = true;
                CBB_rebalance_MinMAPeriod1_B.Enabled = true;
                CBB_rebalance_MinMAPeriod1_C.Enabled = true;
                CBB_rebalance_MinMAPeriod1_D.Enabled = true;
                CBB_rebalance_MinMAPeriod1_E.Enabled = true;
                CBB_rebalance_MinMAPeriod1_F.Enabled = true;
                CBB_rebalance_MinMAPeriod1_G.Enabled = true;

                Form1.비프음("체크");

                TB_rebalance_MinMAPeriod2_A.Text = GenieConfig.TB_rebalance_MinMAPeriod2_A.ToString();
                TB_rebalance_MinMAPeriod2_B.Text = GenieConfig.TB_rebalance_MinMAPeriod2_B.ToString();
                TB_rebalance_MinMAPeriod2_C.Text = GenieConfig.TB_rebalance_MinMAPeriod2_C.ToString();
                TB_rebalance_MinMAPeriod2_D.Text = GenieConfig.TB_rebalance_MinMAPeriod2_D.ToString();
                TB_rebalance_MinMAPeriod2_E.Text = GenieConfig.TB_rebalance_MinMAPeriod2_E.ToString();
                TB_rebalance_MinMAPeriod2_F.Text = GenieConfig.TB_rebalance_MinMAPeriod2_F.ToString();
                TB_rebalance_MinMAPeriod2_G.Text = GenieConfig.TB_rebalance_MinMAPeriod2_G.ToString();

                CBB_rebalance_MinMAPeriod2_A.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod2_A;
                CBB_rebalance_MinMAPeriod2_B.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod2_B;
                CBB_rebalance_MinMAPeriod2_C.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod2_C;
                CBB_rebalance_MinMAPeriod2_D.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod2_D;
                CBB_rebalance_MinMAPeriod2_E.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod2_E;
                CBB_rebalance_MinMAPeriod2_F.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod2_F;
                CBB_rebalance_MinMAPeriod2_G.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod2_G;

                CBB_rebalance_MinMAPeriod1_배열_A.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_A;
                CBB_rebalance_MinMAPeriod1_배열_B.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_B;
                CBB_rebalance_MinMAPeriod1_배열_C.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_C;
                CBB_rebalance_MinMAPeriod1_배열_D.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_D;
                CBB_rebalance_MinMAPeriod1_배열_E.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_E;
                CBB_rebalance_MinMAPeriod1_배열_F.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_F;
                CBB_rebalance_MinMAPeriod1_배열_G.SelectedIndex = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_G;

                TB_rebalance_DayMAPeriod1_A.Text = GenieConfig.TB_rebalance_DayMAPeriod1_A.ToString();
                TB_rebalance_DayMAPeriod1_B.Text = GenieConfig.TB_rebalance_DayMAPeriod1_B.ToString();
                TB_rebalance_DayMAPeriod1_C.Text = GenieConfig.TB_rebalance_DayMAPeriod1_C.ToString();
                TB_rebalance_DayMAPeriod1_D.Text = GenieConfig.TB_rebalance_DayMAPeriod1_D.ToString();
                TB_rebalance_DayMAPeriod1_E.Text = GenieConfig.TB_rebalance_DayMAPeriod1_E.ToString();
                TB_rebalance_DayMAPeriod1_F.Text = GenieConfig.TB_rebalance_DayMAPeriod1_F.ToString();
                TB_rebalance_DayMAPeriod1_G.Text = GenieConfig.TB_rebalance_DayMAPeriod1_G.ToString();

                CBB_rebalance_DayMAPeriod1_A.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod1_A;
                CBB_rebalance_DayMAPeriod1_B.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod1_B;
                CBB_rebalance_DayMAPeriod1_C.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod1_C;
                CBB_rebalance_DayMAPeriod1_D.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod1_D;
                CBB_rebalance_DayMAPeriod1_E.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod1_E;
                CBB_rebalance_DayMAPeriod1_F.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod1_F;
                CBB_rebalance_DayMAPeriod1_G.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod1_G;

                TB_rebalance_DayMAPeriod2_A.Text = GenieConfig.TB_rebalance_DayMAPeriod2_A.ToString();
                TB_rebalance_DayMAPeriod2_B.Text = GenieConfig.TB_rebalance_DayMAPeriod2_B.ToString();
                TB_rebalance_DayMAPeriod2_C.Text = GenieConfig.TB_rebalance_DayMAPeriod2_C.ToString();
                TB_rebalance_DayMAPeriod2_D.Text = GenieConfig.TB_rebalance_DayMAPeriod2_D.ToString();
                TB_rebalance_DayMAPeriod2_E.Text = GenieConfig.TB_rebalance_DayMAPeriod2_E.ToString();
                TB_rebalance_DayMAPeriod2_F.Text = GenieConfig.TB_rebalance_DayMAPeriod2_F.ToString();
                TB_rebalance_DayMAPeriod2_G.Text = GenieConfig.TB_rebalance_DayMAPeriod2_G.ToString();

                CBB_rebalance_DayMAPeriod2_A.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod2_A;
                CBB_rebalance_DayMAPeriod2_B.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod2_B;
                CBB_rebalance_DayMAPeriod2_C.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod2_C;
                CBB_rebalance_DayMAPeriod2_D.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod2_D;
                CBB_rebalance_DayMAPeriod2_E.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod2_E;
                CBB_rebalance_DayMAPeriod2_F.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod2_F;
                CBB_rebalance_DayMAPeriod2_G.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod2_G;

                CBB_rebalance_DayMAPeriod_배열_A.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod_배열_A;
                CBB_rebalance_DayMAPeriod_배열_B.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod_배열_B;
                CBB_rebalance_DayMAPeriod_배열_C.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod_배열_C;
                CBB_rebalance_DayMAPeriod_배열_D.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod_배열_D;
                CBB_rebalance_DayMAPeriod_배열_E.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod_배열_E;
                CBB_rebalance_DayMAPeriod_배열_F.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod_배열_F;
                CBB_rebalance_DayMAPeriod_배열_G.SelectedIndex = GenieConfig.CBB_rebalance_DayMAPeriod_배열_G;
            }
            else
            {
                계좌관리_P.Controls.Remove(panel_mma);
                TB_rebalance_MinMAPeriod1_A.Enabled = false;
                TB_rebalance_MinMAPeriod1_B.Enabled = false;
                TB_rebalance_MinMAPeriod1_C.Enabled = false;
                TB_rebalance_MinMAPeriod1_D.Enabled = false;
                TB_rebalance_MinMAPeriod1_E.Enabled = false;
                TB_rebalance_MinMAPeriod1_F.Enabled = false;
                TB_rebalance_MinMAPeriod1_G.Enabled = false;
                CBB_rebalance_MinMAPeriod1_A.Enabled = false;
                CBB_rebalance_MinMAPeriod1_B.Enabled = false;
                CBB_rebalance_MinMAPeriod1_C.Enabled = false;
                CBB_rebalance_MinMAPeriod1_D.Enabled = false;
                CBB_rebalance_MinMAPeriod1_E.Enabled = false;
                CBB_rebalance_MinMAPeriod1_F.Enabled = false;
                CBB_rebalance_MinMAPeriod1_G.Enabled = false;

                Form1.비프음("언체크");
            }
        }

        private void CBB_mma_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_A) && CBB_rebalance_MinMAPeriod1_A.SelectedIndex == 0) { CBB_rebalance_MinMAPeriod2_A.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_B) && CBB_rebalance_MinMAPeriod1_B.SelectedIndex == 0) { CBB_rebalance_MinMAPeriod2_B.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_C) && CBB_rebalance_MinMAPeriod1_C.SelectedIndex == 0) { CBB_rebalance_MinMAPeriod2_C.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_D) && CBB_rebalance_MinMAPeriod1_D.SelectedIndex == 0) { CBB_rebalance_MinMAPeriod2_D.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_E) && CBB_rebalance_MinMAPeriod1_E.SelectedIndex == 0) { CBB_rebalance_MinMAPeriod2_E.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_F) && CBB_rebalance_MinMAPeriod1_F.SelectedIndex == 0) { CBB_rebalance_MinMAPeriod2_F.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_G) && CBB_rebalance_MinMAPeriod1_G.SelectedIndex == 0) { CBB_rebalance_MinMAPeriod2_G.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_G.SelectedIndex = 0; }
        }

        private void CBB_mma2_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_rebalance_MinMAPeriod2_A) && (CBB_rebalance_MinMAPeriod1_A.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_A.SelectedIndex == 0)) { CBB_rebalance_MinMAPeriod2_A.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_MinMAPeriod2_B) && (CBB_rebalance_MinMAPeriod1_B.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_B.SelectedIndex == 0)) { CBB_rebalance_MinMAPeriod2_B.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_MinMAPeriod2_C) && (CBB_rebalance_MinMAPeriod1_C.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_C.SelectedIndex == 0)) { CBB_rebalance_MinMAPeriod2_C.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_MinMAPeriod2_D) && (CBB_rebalance_MinMAPeriod1_D.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_D.SelectedIndex == 0)) { CBB_rebalance_MinMAPeriod2_D.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_MinMAPeriod2_E) && (CBB_rebalance_MinMAPeriod1_E.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_E.SelectedIndex == 0)) { CBB_rebalance_MinMAPeriod2_E.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_MinMAPeriod2_F) && (CBB_rebalance_MinMAPeriod1_F.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_F.SelectedIndex == 0)) { CBB_rebalance_MinMAPeriod2_F.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_MinMAPeriod2_G) && (CBB_rebalance_MinMAPeriod1_G.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_G.SelectedIndex == 0)) { CBB_rebalance_MinMAPeriod2_G.SelectedIndex = 0; CBB_rebalance_MinMAPeriod1_배열_G.SelectedIndex = 0; }
        }

        private void CBB_mma_배열_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_배열_A) && (CBB_rebalance_MinMAPeriod1_A.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_A.SelectedIndex == 0)) CBB_rebalance_MinMAPeriod1_배열_A.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_배열_B) && (CBB_rebalance_MinMAPeriod1_B.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_B.SelectedIndex == 0)) CBB_rebalance_MinMAPeriod1_배열_B.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_배열_C) && (CBB_rebalance_MinMAPeriod1_C.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_C.SelectedIndex == 0)) CBB_rebalance_MinMAPeriod1_배열_C.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_배열_D) && (CBB_rebalance_MinMAPeriod1_D.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_D.SelectedIndex == 0)) CBB_rebalance_MinMAPeriod1_배열_D.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_배열_E) && (CBB_rebalance_MinMAPeriod1_E.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_E.SelectedIndex == 0)) CBB_rebalance_MinMAPeriod1_배열_E.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_배열_F) && (CBB_rebalance_MinMAPeriod1_F.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_F.SelectedIndex == 0)) CBB_rebalance_MinMAPeriod1_배열_F.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_MinMAPeriod1_배열_G) && (CBB_rebalance_MinMAPeriod1_G.SelectedIndex == 0 || CBB_rebalance_MinMAPeriod2_G.SelectedIndex == 0)) CBB_rebalance_MinMAPeriod1_배열_G.SelectedIndex = 0;
        }

        private void CBB_dma1_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_rebalance_DayMAPeriod1_A) && CBB_rebalance_DayMAPeriod1_A.SelectedIndex == 0) { CBB_rebalance_DayMAPeriod2_A.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_DayMAPeriod1_B) && CBB_rebalance_DayMAPeriod1_B.SelectedIndex == 0) { CBB_rebalance_DayMAPeriod2_B.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_DayMAPeriod1_C) && CBB_rebalance_DayMAPeriod1_C.SelectedIndex == 0) { CBB_rebalance_DayMAPeriod2_C.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_DayMAPeriod1_D) && CBB_rebalance_DayMAPeriod1_D.SelectedIndex == 0) { CBB_rebalance_DayMAPeriod2_D.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_DayMAPeriod1_E) && CBB_rebalance_DayMAPeriod1_E.SelectedIndex == 0) { CBB_rebalance_DayMAPeriod2_E.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_DayMAPeriod1_F) && CBB_rebalance_DayMAPeriod1_F.SelectedIndex == 0) { CBB_rebalance_DayMAPeriod2_F.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_DayMAPeriod1_G) && CBB_rebalance_DayMAPeriod1_G.SelectedIndex == 0) { CBB_rebalance_DayMAPeriod2_G.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_G.SelectedIndex = 0; }
        }
        private void CBB_dma2_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_rebalance_DayMAPeriod2_A) && (CBB_rebalance_DayMAPeriod1_A.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_A.SelectedIndex == 0)) { CBB_rebalance_DayMAPeriod2_A.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_DayMAPeriod2_B) && (CBB_rebalance_DayMAPeriod1_B.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_B.SelectedIndex == 0)) { CBB_rebalance_DayMAPeriod2_B.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_DayMAPeriod2_C) && (CBB_rebalance_DayMAPeriod1_C.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_C.SelectedIndex == 0)) { CBB_rebalance_DayMAPeriod2_C.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_DayMAPeriod2_D) && (CBB_rebalance_DayMAPeriod1_D.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_D.SelectedIndex == 0)) { CBB_rebalance_DayMAPeriod2_D.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_DayMAPeriod2_E) && (CBB_rebalance_DayMAPeriod1_E.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_E.SelectedIndex == 0)) { CBB_rebalance_DayMAPeriod2_E.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_DayMAPeriod2_F) && (CBB_rebalance_DayMAPeriod1_F.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_F.SelectedIndex == 0)) { CBB_rebalance_DayMAPeriod2_F.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_DayMAPeriod2_G) && (CBB_rebalance_DayMAPeriod1_G.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_G.SelectedIndex == 0)) { CBB_rebalance_DayMAPeriod2_G.SelectedIndex = 0; CBB_rebalance_DayMAPeriod_배열_G.SelectedIndex = 0; }
        }
        private void CBB_dma_배열_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_rebalance_DayMAPeriod_배열_A) && (CBB_rebalance_DayMAPeriod1_A.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_A.SelectedIndex == 0)) CBB_rebalance_DayMAPeriod_배열_A.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_DayMAPeriod_배열_B) && (CBB_rebalance_DayMAPeriod1_B.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_B.SelectedIndex == 0)) CBB_rebalance_DayMAPeriod_배열_B.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_DayMAPeriod_배열_C) && (CBB_rebalance_DayMAPeriod1_C.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_C.SelectedIndex == 0)) CBB_rebalance_DayMAPeriod_배열_C.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_DayMAPeriod_배열_D) && (CBB_rebalance_DayMAPeriod1_D.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_D.SelectedIndex == 0)) CBB_rebalance_DayMAPeriod_배열_D.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_DayMAPeriod_배열_E) && (CBB_rebalance_DayMAPeriod1_E.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_E.SelectedIndex == 0)) CBB_rebalance_DayMAPeriod_배열_E.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_DayMAPeriod_배열_F) && (CBB_rebalance_DayMAPeriod1_F.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_F.SelectedIndex == 0)) CBB_rebalance_DayMAPeriod_배열_F.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_DayMAPeriod_배열_G) && (CBB_rebalance_DayMAPeriod1_G.SelectedIndex == 0 || CBB_rebalance_DayMAPeriod2_G.SelectedIndex == 0)) CBB_rebalance_DayMAPeriod_배열_G.SelectedIndex = 0;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        private void MTB_cut_time_TextChanged(object sender, EventArgs e)
        {
            int.TryParse((sender as MaskedTextBox).Text, out int time);

            if ((sender as MaskedTextBox).Text.Length > 4 && time < Form1.Get.메인마켓시작 || time > Form1.Get.메인마켓종료)
            {
                Helper.알림창_멀티("실현손익 담보 손실매도","운영시간\n메인마켓 에서 가능합니다. 9:00:00 ~ 15:20:00 ", 20, false);

                (sender as MaskedTextBox).Text = (Form1.Get.메인마켓종료 - 2000).ToString();
            }
        }
    }
}
