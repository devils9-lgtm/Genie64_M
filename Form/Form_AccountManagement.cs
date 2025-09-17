using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace 지니_64
{
    public partial class Form_AccountManagement : Form
    {
        public static Form_AccountManagement form;
        public Form_AccountManagement()
        {
            form = this;
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void Form_AccountManagement_Load()
        {
            Form1.음소거 = true;

            if (Form1.form1.오전감시시간 < Form1.timenow) LB_리밸매도시간오전.BackColor = Color.Tan;
            if (Form1.form1.오후감시시간 < Form1.timenow) LB_리밸매도시간오후.BackColor = Color.Tan;

            CB_리밸TS_1.Checked = false;
            CB_리밸TS_2.Checked = false;
            CB_mma.Checked = false;
            panel_리밸TS_1.Hide();
            panel_리밸TS_2.Hide();
            panel_mma.Hide();

            string 계좌관리검색식 = Properties.Settings.Default.계좌관리검색식;

            combo_rebalance_condition_A.Items.Add(계좌관리검색식.Split('^')[0]); combo_rebalance_condition_A.Text = 계좌관리검색식.Split('^')[0];
            combo_rebalance_condition_B.Items.Add(계좌관리검색식.Split('^')[1]); combo_rebalance_condition_B.Text = 계좌관리검색식.Split('^')[1];
            combo_rebalance_condition_C.Items.Add(계좌관리검색식.Split('^')[2]); combo_rebalance_condition_C.Text = 계좌관리검색식.Split('^')[2];
            combo_rebalance_condition_D.Items.Add(계좌관리검색식.Split('^')[3]); combo_rebalance_condition_D.Text = 계좌관리검색식.Split('^')[3];
            combo_rebalance_condition_E.Items.Add(계좌관리검색식.Split('^')[4]); combo_rebalance_condition_E.Text = 계좌관리검색식.Split('^')[4];
            combo_rebalance_condition_F.Items.Add(계좌관리검색식.Split('^')[5]); combo_rebalance_condition_F.Text = 계좌관리검색식.Split('^')[5];
            combo_rebalance_condition_G.Items.Add(계좌관리검색식.Split('^')[6]); combo_rebalance_condition_G.Text = 계좌관리검색식.Split('^')[6];
            CBB_Liquidation_condition_A.Items.Add(계좌관리검색식.Split('^')[7]); CBB_Liquidation_condition_A.Text = 계좌관리검색식.Split('^')[7];
            CBB_Liquidation_condition_B.Items.Add(계좌관리검색식.Split('^')[8]); CBB_Liquidation_condition_B.Text = 계좌관리검색식.Split('^')[8];
            CBB_Liquidation_condition_C.Items.Add(계좌관리검색식.Split('^')[9]); CBB_Liquidation_condition_C.Text = 계좌관리검색식.Split('^')[9];

            combo_rebalance_use_condition_A.SelectedIndex = Properties.Settings.Default.combo_rebalance_use_condition_A;
            combo_rebalance_use_condition_B.SelectedIndex = Properties.Settings.Default.combo_rebalance_use_condition_B;
            combo_rebalance_use_condition_C.SelectedIndex = Properties.Settings.Default.combo_rebalance_use_condition_C;
            combo_rebalance_use_condition_D.SelectedIndex = Properties.Settings.Default.combo_rebalance_use_condition_D;
            combo_rebalance_use_condition_E.SelectedIndex = Properties.Settings.Default.combo_rebalance_use_condition_E;
            combo_rebalance_use_condition_F.SelectedIndex = Properties.Settings.Default.combo_rebalance_use_condition_F;
            combo_rebalance_use_condition_G.SelectedIndex = Properties.Settings.Default.combo_rebalance_use_condition_G;
            CBB_Liquidation_use_condition_A.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_use_condition_A;
            CBB_Liquidation_use_condition_B.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_use_condition_B;
            CBB_Liquidation_use_condition_C.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_use_condition_C;

            CB_rebalance_A.Checked = Properties.Settings.Default.CB_rebalance_A;
            CB_rebalance_B.Checked = Properties.Settings.Default.CB_rebalance_B;
            CB_rebalance_C.Checked = Properties.Settings.Default.CB_rebalance_C;
            CB_rebalance_D.Checked = Properties.Settings.Default.CB_rebalance_D;
            CB_rebalance_E.Checked = Properties.Settings.Default.CB_rebalance_E;
            CB_rebalance_F.Checked = Properties.Settings.Default.CB_rebalance_F;
            CB_rebalance_G.Checked = Properties.Settings.Default.CB_rebalance_G;
            CB_Liquidation_A.Checked = Properties.Settings.Default.CB_Liquidation_A;
            CB_Liquidation_B.Checked = Properties.Settings.Default.CB_Liquidation_B;
            CB_Liquidation_C.Checked = Properties.Settings.Default.CB_Liquidation_C;

            CB_총매수금.Checked = Properties.Settings.Default.CB_총매수금;
            CB_일매수제한금.Checked = Properties.Settings.Default.CB_일매수제한금;
            CB_회수제한.Checked = Properties.Settings.Default.CB_회수제한;

            CB_cut_기준금.Checked = Properties.Settings.Default.CB_cut_기준금;
            CB_cut_A.Checked = Properties.Settings.Default.CB_cut_A;
            CB_cut_B.Checked = Properties.Settings.Default.CB_cut_B;
            CB_cut_C.Checked = Properties.Settings.Default.CB_cut_C;

            MTB_cut_time_A.Text = Properties.Settings.Default.MTB_cut_time_A.ToString();
            MTB_cut_time_B.Text = Properties.Settings.Default.MTB_cut_time_B.ToString();
            MTB_cut_time_C.Text = Properties.Settings.Default.MTB_cut_time_C.ToString();

            TB_cut_수익금1_A.Text = Properties.Settings.Default.TB_cut_수익금1_A.ToString();
            TB_cut_수익금1_B.Text = Properties.Settings.Default.TB_cut_수익금1_B.ToString();
            TB_cut_수익금1_C.Text = Properties.Settings.Default.TB_cut_수익금1_C.ToString();

            TB_cut_수익금2_A.Text = Properties.Settings.Default.TB_cut_수익금2_A.ToString();
            TB_cut_수익금2_B.Text = Properties.Settings.Default.TB_cut_수익금2_B.ToString();
            TB_cut_수익금2_C.Text = Properties.Settings.Default.TB_cut_수익금2_C.ToString();

            TB_cut_남길퍼_A.Text = Properties.Settings.Default.TB_cut_남길퍼_A.ToString();
            TB_cut_남길퍼_B.Text = Properties.Settings.Default.TB_cut_남길퍼_B.ToString();
            TB_cut_남길퍼_C.Text = Properties.Settings.Default.TB_cut_남길퍼_C.ToString();

            TB_cut_P_A.Text = Properties.Settings.Default.TB_cut_P_A.ToString();
            TB_cut_P_B.Text = Properties.Settings.Default.TB_cut_P_B.ToString();
            TB_cut_P_C.Text = Properties.Settings.Default.TB_cut_P_C.ToString();

            TB_cut_won_A.Text = Properties.Settings.Default.TB_cut_won_A.ToString();
            TB_cut_won_B.Text = Properties.Settings.Default.TB_cut_won_B.ToString();
            TB_cut_won_C.Text = Properties.Settings.Default.TB_cut_won_C.ToString();

            TB_cut_ratio_A.Text = Properties.Settings.Default.TB_cut_ratio_A.ToString();
            TB_cut_ratio_B.Text = Properties.Settings.Default.TB_cut_ratio_B.ToString();
            TB_cut_ratio_C.Text = Properties.Settings.Default.TB_cut_ratio_C.ToString();

            CBB_cut_gubun_A.SelectedIndex = Properties.Settings.Default.CBB_cut_gubun_A;
            CBB_cut_gubun_B.SelectedIndex = Properties.Settings.Default.CBB_cut_gubun_B;
            CBB_cut_gubun_C.SelectedIndex = Properties.Settings.Default.CBB_cut_gubun_C;

            CBB_cut_jumun_A.SelectedIndex = Properties.Settings.Default.CBB_cut_jumun_A;
            CBB_cut_jumun_B.SelectedIndex = Properties.Settings.Default.CBB_cut_jumun_B;
            CBB_cut_jumun_C.SelectedIndex = Properties.Settings.Default.CBB_cut_jumun_C;

            MTB_cut_cansel_time_A.Text = Properties.Settings.Default.MTB_cut_cansel_time_A.ToString();
            MTB_cut_cansel_time_B.Text = Properties.Settings.Default.MTB_cut_cansel_time_B.ToString();
            MTB_cut_cansel_time_C.Text = Properties.Settings.Default.MTB_cut_cansel_time_C.ToString();

            TB_cut_value_A.Text = Properties.Settings.Default.TB_cut_value_A.ToString();
            TB_cut_value_B.Text = Properties.Settings.Default.TB_cut_value_B.ToString();
            TB_cut_value_C.Text = Properties.Settings.Default.TB_cut_value_C.ToString();

            CB_cut_LB_A.Text = Form1.form1.cut_LB_A;
            CB_cut_LB_B.Text = Form1.form1.cut_LB_B;
            CB_cut_LB_C.Text = Form1.form1.cut_LB_C;

            CB_rebalance_option_A.Checked = Properties.Settings.Default.CB_rebalance_option_A;
            CB_rebalance_option_B.Checked = Properties.Settings.Default.CB_rebalance_option_B;
            CB_rebalance_option_C.Checked = Properties.Settings.Default.CB_rebalance_option_C;
            CB_rebalance_option_D.Checked = Properties.Settings.Default.CB_rebalance_option_D;
            CB_rebalance_option_E.Checked = Properties.Settings.Default.CB_rebalance_option_E;
            CB_rebalance_option_F.Checked = Properties.Settings.Default.CB_rebalance_option_F;
            CB_rebalance_option_G.Checked = Properties.Settings.Default.CB_rebalance_option_G;

            CB_rebalance_기준금.Checked = Properties.Settings.Default.CB_rebalance_기준금;

            CBB_rebalance_1A.SelectedItem = Properties.Settings.Default.리밸매도기준1_A;
            CBB_rebalance_1B.SelectedItem = Properties.Settings.Default.리밸매도기준1_B;
            CBB_rebalance_1C.SelectedItem = Properties.Settings.Default.리밸매도기준1_C;
            CBB_rebalance_1D.SelectedItem = Properties.Settings.Default.리밸매도기준1_D;
            CBB_rebalance_1E.SelectedItem = Properties.Settings.Default.리밸매도기준1_E;
            CBB_rebalance_1F.SelectedItem = Properties.Settings.Default.리밸매도기준1_F;
            CBB_rebalance_1G.SelectedItem = Properties.Settings.Default.리밸매도기준1_G;
            CBB_rebalance_2A.SelectedItem = Properties.Settings.Default.리밸매도기준2_A;
            CBB_rebalance_2B.SelectedItem = Properties.Settings.Default.리밸매도기준2_B;
            CBB_rebalance_2C.SelectedItem = Properties.Settings.Default.리밸매도기준2_C;
            CBB_rebalance_2D.SelectedItem = Properties.Settings.Default.리밸매도기준2_D;
            CBB_rebalance_2E.SelectedItem = Properties.Settings.Default.리밸매도기준2_E;
            CBB_rebalance_2F.SelectedItem = Properties.Settings.Default.리밸매도기준2_F;
            CBB_rebalance_2G.SelectedItem = Properties.Settings.Default.리밸매도기준2_G;

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

            CB_rebalance_choice_A.Checked = Properties.Settings.Default.CB_rebalance_choice_A;
            CB_rebalance_choice_B.Checked = Properties.Settings.Default.CB_rebalance_choice_B;
            CB_rebalance_choice_C.Checked = Properties.Settings.Default.CB_rebalance_choice_C;
            CB_rebalance_choice_D.Checked = Properties.Settings.Default.CB_rebalance_choice_D;
            CB_rebalance_choice_E.Checked = Properties.Settings.Default.CB_rebalance_choice_E;
            CB_rebalance_choice_F.Checked = Properties.Settings.Default.CB_rebalance_choice_F;
            CB_rebalance_choice_G.Checked = Properties.Settings.Default.CB_rebalance_choice_G;

            combo_rebalance_suik_gubun_A.SelectedIndex = Properties.Settings.Default.combo_rebalance_suik_gubun_A;
            combo_rebalance_suik_gubun_B.SelectedIndex = Properties.Settings.Default.combo_rebalance_suik_gubun_B;
            combo_rebalance_suik_gubun_C.SelectedIndex = Properties.Settings.Default.combo_rebalance_suik_gubun_C;
            combo_rebalance_suik_gubun_D.SelectedIndex = Properties.Settings.Default.combo_rebalance_suik_gubun_D;
            combo_rebalance_suik_gubun_E.SelectedIndex = Properties.Settings.Default.combo_rebalance_suik_gubun_E;
            combo_rebalance_suik_gubun_F.SelectedIndex = Properties.Settings.Default.combo_rebalance_suik_gubun_F;
            combo_rebalance_suik_gubun_G.SelectedIndex = Properties.Settings.Default.combo_rebalance_suik_gubun_G;

            combo_rebalance_sell_gubun_A.SelectedIndex = Properties.Settings.Default.combo_rebalance_sell_gubun_A;
            combo_rebalance_sell_gubun_B.SelectedIndex = Properties.Settings.Default.combo_rebalance_sell_gubun_B;
            combo_rebalance_sell_gubun_C.SelectedIndex = Properties.Settings.Default.combo_rebalance_sell_gubun_C;
            combo_rebalance_sell_gubun_D.SelectedIndex = Properties.Settings.Default.combo_rebalance_sell_gubun_D;
            combo_rebalance_sell_gubun_E.SelectedIndex = Properties.Settings.Default.combo_rebalance_sell_gubun_E;
            combo_rebalance_sell_gubun_F.SelectedIndex = Properties.Settings.Default.combo_rebalance_sell_gubun_F;
            combo_rebalance_sell_gubun_G.SelectedIndex = Properties.Settings.Default.combo_rebalance_sell_gubun_G;

            combo_rebalance_maemae_gubun_A.SelectedIndex = Properties.Settings.Default.combo_rebalance_maemae_gubun_A;
            combo_rebalance_maemae_gubun_B.SelectedIndex = Properties.Settings.Default.combo_rebalance_maemae_gubun_B;
            combo_rebalance_maemae_gubun_C.SelectedIndex = Properties.Settings.Default.combo_rebalance_maemae_gubun_C;
            combo_rebalance_maemae_gubun_D.SelectedIndex = Properties.Settings.Default.combo_rebalance_maemae_gubun_D;
            combo_rebalance_maemae_gubun_E.SelectedIndex = Properties.Settings.Default.combo_rebalance_maemae_gubun_E;
            combo_rebalance_maemae_gubun_F.SelectedIndex = Properties.Settings.Default.combo_rebalance_maemae_gubun_F;
            combo_rebalance_maemae_gubun_G.SelectedIndex = Properties.Settings.Default.combo_rebalance_maemae_gubun_G;

            combo_rebalance_jumun_A.SelectedIndex = Properties.Settings.Default.combo_rebalance_jumun_A;
            combo_rebalance_jumun_B.SelectedIndex = Properties.Settings.Default.combo_rebalance_jumun_B;
            combo_rebalance_jumun_C.SelectedIndex = Properties.Settings.Default.combo_rebalance_jumun_C;
            combo_rebalance_jumun_D.SelectedIndex = Properties.Settings.Default.combo_rebalance_jumun_D;
            combo_rebalance_jumun_E.SelectedIndex = Properties.Settings.Default.combo_rebalance_jumun_E;
            combo_rebalance_jumun_F.SelectedIndex = Properties.Settings.Default.combo_rebalance_jumun_F;
            combo_rebalance_jumun_G.SelectedIndex = Properties.Settings.Default.combo_rebalance_jumun_G;

            MTB_rebalance_delay_A.Text = Properties.Settings.Default.MTB_rebalance_delay_A.ToString();
            MTB_rebalance_delay_B.Text = Properties.Settings.Default.MTB_rebalance_delay_B.ToString();
            MTB_rebalance_delay_C.Text = Properties.Settings.Default.MTB_rebalance_delay_C.ToString();
            MTB_rebalance_delay_D.Text = Properties.Settings.Default.MTB_rebalance_delay_D.ToString();
            MTB_rebalance_delay_E.Text = Properties.Settings.Default.MTB_rebalance_delay_E.ToString();
            MTB_rebalance_delay_F.Text = Properties.Settings.Default.MTB_rebalance_delay_F.ToString();
            MTB_rebalance_delay_G.Text = Properties.Settings.Default.MTB_rebalance_delay_G.ToString();

            TB_rebalance_suik_1_A.Text = Properties.Settings.Default.TB_rebalance_suik_1_A.ToString();
            TB_rebalance_suik_1_B.Text = Properties.Settings.Default.TB_rebalance_suik_1_B.ToString();
            TB_rebalance_suik_1_C.Text = Properties.Settings.Default.TB_rebalance_suik_1_C.ToString();
            TB_rebalance_suik_1_D.Text = Properties.Settings.Default.TB_rebalance_suik_1_D.ToString();
            TB_rebalance_suik_1_E.Text = Properties.Settings.Default.TB_rebalance_suik_1_E.ToString();
            TB_rebalance_suik_1_F.Text = Properties.Settings.Default.TB_rebalance_suik_1_F.ToString();
            TB_rebalance_suik_1_G.Text = Properties.Settings.Default.TB_rebalance_suik_1_G.ToString();

            TB_rebalance_suik_2_A.Text = Properties.Settings.Default.TB_rebalance_suik_2_A.ToString();
            TB_rebalance_suik_2_B.Text = Properties.Settings.Default.TB_rebalance_suik_2_B.ToString();
            TB_rebalance_suik_2_C.Text = Properties.Settings.Default.TB_rebalance_suik_2_C.ToString();
            TB_rebalance_suik_2_D.Text = Properties.Settings.Default.TB_rebalance_suik_2_D.ToString();
            TB_rebalance_suik_2_E.Text = Properties.Settings.Default.TB_rebalance_suik_2_E.ToString();
            TB_rebalance_suik_2_F.Text = Properties.Settings.Default.TB_rebalance_suik_2_F.ToString();
            TB_rebalance_suik_2_G.Text = Properties.Settings.Default.TB_rebalance_suik_2_G.ToString();

            TB_rebalance_sell_ratio_A.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_A.ToString();
            TB_rebalance_sell_ratio_B.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_B.ToString();
            TB_rebalance_sell_ratio_C.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_C.ToString();
            TB_rebalance_sell_ratio_D.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_D.ToString();
            TB_rebalance_sell_ratio_E.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_E.ToString();
            TB_rebalance_sell_ratio_F.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_F.ToString();
            TB_rebalance_sell_ratio_G.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_G.ToString();

            TB_rebalance_maemae_1_A.Text = Properties.Settings.Default.TB_rebalance_maemae_1_A.ToString();
            TB_rebalance_maemae_1_B.Text = Properties.Settings.Default.TB_rebalance_maemae_1_B.ToString();
            TB_rebalance_maemae_1_C.Text = Properties.Settings.Default.TB_rebalance_maemae_1_C.ToString();
            TB_rebalance_maemae_1_D.Text = Properties.Settings.Default.TB_rebalance_maemae_1_D.ToString();
            TB_rebalance_maemae_1_E.Text = Properties.Settings.Default.TB_rebalance_maemae_1_E.ToString();
            TB_rebalance_maemae_1_F.Text = Properties.Settings.Default.TB_rebalance_maemae_1_F.ToString();
            TB_rebalance_maemae_1_G.Text = Properties.Settings.Default.TB_rebalance_maemae_1_G.ToString();

            TB_rebalance_maemae_2_A.Text = Properties.Settings.Default.TB_rebalance_maemae_2_A.ToString();
            TB_rebalance_maemae_2_B.Text = Properties.Settings.Default.TB_rebalance_maemae_2_B.ToString();
            TB_rebalance_maemae_2_C.Text = Properties.Settings.Default.TB_rebalance_maemae_2_C.ToString();
            TB_rebalance_maemae_2_D.Text = Properties.Settings.Default.TB_rebalance_maemae_2_D.ToString();
            TB_rebalance_maemae_2_E.Text = Properties.Settings.Default.TB_rebalance_maemae_2_E.ToString();
            TB_rebalance_maemae_2_F.Text = Properties.Settings.Default.TB_rebalance_maemae_2_F.ToString();
            TB_rebalance_maemae_2_G.Text = Properties.Settings.Default.TB_rebalance_maemae_2_G.ToString();

            MT_rebalance_repeat_time_A.Text = Properties.Settings.Default.MT_rebalance_repeat_time_A.ToString();
            MT_rebalance_repeat_time_B.Text = Properties.Settings.Default.MT_rebalance_repeat_time_B.ToString();
            MT_rebalance_repeat_time_C.Text = Properties.Settings.Default.MT_rebalance_repeat_time_C.ToString();
            MT_rebalance_repeat_time_D.Text = Properties.Settings.Default.MT_rebalance_repeat_time_D.ToString();
            MT_rebalance_repeat_time_E.Text = Properties.Settings.Default.MT_rebalance_repeat_time_E.ToString();
            MT_rebalance_repeat_time_F.Text = Properties.Settings.Default.MT_rebalance_repeat_time_F.ToString();
            MT_rebalance_repeat_time_G.Text = Properties.Settings.Default.MT_rebalance_repeat_time_G.ToString();

            MTB_rebalance_Cancel_time_A.Text = Properties.Settings.Default.MTB_rebalance_Cancel_time_A.ToString();
            MTB_rebalance_Cancel_time_B.Text = Properties.Settings.Default.MTB_rebalance_Cancel_time_B.ToString();
            MTB_rebalance_Cancel_time_C.Text = Properties.Settings.Default.MTB_rebalance_Cancel_time_C.ToString();
            MTB_rebalance_Cancel_time_D.Text = Properties.Settings.Default.MTB_rebalance_Cancel_time_D.ToString();
            MTB_rebalance_Cancel_time_E.Text = Properties.Settings.Default.MTB_rebalance_Cancel_time_E.ToString();
            MTB_rebalance_Cancel_time_F.Text = Properties.Settings.Default.MTB_rebalance_Cancel_time_F.ToString();
            MTB_rebalance_Cancel_time_G.Text = Properties.Settings.Default.MTB_rebalance_Cancel_time_G.ToString();

            TB_rebalance_sellratio1_A.Text = Properties.Settings.Default.TB_rebalance_sellratio1_A.ToString();
            TB_rebalance_sellratio1_B.Text = Properties.Settings.Default.TB_rebalance_sellratio1_B.ToString();
            TB_rebalance_sellratio1_C.Text = Properties.Settings.Default.TB_rebalance_sellratio1_C.ToString();
            TB_rebalance_sellratio1_D.Text = Properties.Settings.Default.TB_rebalance_sellratio1_D.ToString();
            TB_rebalance_sellratio1_E.Text = Properties.Settings.Default.TB_rebalance_sellratio1_E.ToString();
            TB_rebalance_sellratio1_F.Text = Properties.Settings.Default.TB_rebalance_sellratio1_F.ToString();
            TB_rebalance_sellratio1_G.Text = Properties.Settings.Default.TB_rebalance_sellratio1_G.ToString();

            TB_rebalance_감시_value_A.Text = Properties.Settings.Default.TB_rebalance_감시_value_A.ToString();
            TB_rebalance_감시_value_B.Text = Properties.Settings.Default.TB_rebalance_감시_value_B.ToString();
            TB_rebalance_감시_value_C.Text = Properties.Settings.Default.TB_rebalance_감시_value_C.ToString();
            TB_rebalance_감시_value_D.Text = Properties.Settings.Default.TB_rebalance_감시_value_D.ToString();
            TB_rebalance_감시_value_E.Text = Properties.Settings.Default.TB_rebalance_감시_value_E.ToString();
            TB_rebalance_감시_value_F.Text = Properties.Settings.Default.TB_rebalance_감시_value_F.ToString();
            TB_rebalance_감시_value_G.Text = Properties.Settings.Default.TB_rebalance_감시_value_G.ToString();

            combo_rebalance_감시_jumun_A.SelectedIndex = Properties.Settings.Default.combo_rebalance_감시_jumun_A;
            combo_rebalance_감시_jumun_B.SelectedIndex = Properties.Settings.Default.combo_rebalance_감시_jumun_B;
            combo_rebalance_감시_jumun_C.SelectedIndex = Properties.Settings.Default.combo_rebalance_감시_jumun_C;
            combo_rebalance_감시_jumun_D.SelectedIndex = Properties.Settings.Default.combo_rebalance_감시_jumun_D;
            combo_rebalance_감시_jumun_E.SelectedIndex = Properties.Settings.Default.combo_rebalance_감시_jumun_E;
            combo_rebalance_감시_jumun_F.SelectedIndex = Properties.Settings.Default.combo_rebalance_감시_jumun_F;
            combo_rebalance_감시_jumun_G.SelectedIndex = Properties.Settings.Default.combo_rebalance_감시_jumun_G;

            TB_rebalance_sellvolume1_A.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_A.ToString();
            TB_rebalance_sellvolume1_B.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_B.ToString();
            TB_rebalance_sellvolume1_C.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_C.ToString();
            TB_rebalance_sellvolume1_D.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_D.ToString();
            TB_rebalance_sellvolume1_E.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_E.ToString();
            TB_rebalance_sellvolume1_F.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_F.ToString();
            TB_rebalance_sellvolume1_G.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_G.ToString();

            TB_rebalance_sellvolume2_A.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_A.ToString();
            TB_rebalance_sellvolume2_B.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_B.ToString();
            TB_rebalance_sellvolume2_C.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_C.ToString();
            TB_rebalance_sellvolume2_D.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_D.ToString();
            TB_rebalance_sellvolume2_E.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_E.ToString();
            TB_rebalance_sellvolume2_F.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_F.ToString();
            TB_rebalance_sellvolume2_G.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_G.ToString();

            TB_rebalance_sellcancel1_A.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_A.ToString();
            TB_rebalance_sellcancel1_B.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_B.ToString();
            TB_rebalance_sellcancel1_C.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_C.ToString();
            TB_rebalance_sellcancel1_D.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_D.ToString();
            TB_rebalance_sellcancel1_E.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_E.ToString();
            TB_rebalance_sellcancel1_F.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_F.ToString();
            TB_rebalance_sellcancel1_G.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_G.ToString();

            TB_rebalance_sellratio2_A.Text = Properties.Settings.Default.TB_rebalance_sellratio2_A.ToString();
            TB_rebalance_sellratio2_B.Text = Properties.Settings.Default.TB_rebalance_sellratio2_B.ToString();
            TB_rebalance_sellratio2_C.Text = Properties.Settings.Default.TB_rebalance_sellratio2_C.ToString();
            TB_rebalance_sellratio2_D.Text = Properties.Settings.Default.TB_rebalance_sellratio2_D.ToString();
            TB_rebalance_sellratio2_E.Text = Properties.Settings.Default.TB_rebalance_sellratio2_E.ToString();
            TB_rebalance_sellratio2_F.Text = Properties.Settings.Default.TB_rebalance_sellratio2_F.ToString();
            TB_rebalance_sellratio2_G.Text = Properties.Settings.Default.TB_rebalance_sellratio2_G.ToString();

            TB_rebalance_sellcancel2_A.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_A.ToString();
            TB_rebalance_sellcancel2_B.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_B.ToString();
            TB_rebalance_sellcancel2_C.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_C.ToString();
            TB_rebalance_sellcancel2_D.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_D.ToString();
            TB_rebalance_sellcancel2_E.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_E.ToString();
            TB_rebalance_sellcancel2_F.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_F.ToString();
            TB_rebalance_sellcancel2_G.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_G.ToString();

            TB_rebalance_value_A.Text = Properties.Settings.Default.TB_rebalance_value_A.ToString();
            TB_rebalance_value_B.Text = Properties.Settings.Default.TB_rebalance_value_B.ToString();
            TB_rebalance_value_C.Text = Properties.Settings.Default.TB_rebalance_value_C.ToString();
            TB_rebalance_value_D.Text = Properties.Settings.Default.TB_rebalance_value_D.ToString();
            TB_rebalance_value_E.Text = Properties.Settings.Default.TB_rebalance_value_E.ToString();
            TB_rebalance_value_F.Text = Properties.Settings.Default.TB_rebalance_value_F.ToString();
            TB_rebalance_value_G.Text = Properties.Settings.Default.TB_rebalance_value_G.ToString();

            CBB_rebalance_Selltime_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_Selltime_A;
            CBB_rebalance_Selltime_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_Selltime_B;
            CBB_rebalance_Selltime_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_Selltime_C;
            CBB_rebalance_Selltime_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_Selltime_D;
            CBB_rebalance_Selltime_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_Selltime_E;
            CBB_rebalance_Selltime_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_Selltime_F;
            CBB_rebalance_Selltime_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_Selltime_G;

            MTB_rebalance_Selltime_오전.Text = Properties.Settings.Default.MTB_rebalance_Selltime_오전.ToString();
            MTB_rebalance_Selltime_오후.Text = Properties.Settings.Default.MTB_rebalance_Selltime_오후.ToString();

            MT_rebalance_starttime_A.Text = Properties.Settings.Default.MT_rebalance_starttime_A.ToString();
            MT_rebalance_starttime_B.Text = Properties.Settings.Default.MT_rebalance_starttime_B.ToString();
            MT_rebalance_starttime_C.Text = Properties.Settings.Default.MT_rebalance_starttime_C.ToString();
            MT_rebalance_starttime_D.Text = Properties.Settings.Default.MT_rebalance_starttime_D.ToString();
            MT_rebalance_starttime_E.Text = Properties.Settings.Default.MT_rebalance_starttime_E.ToString();
            MT_rebalance_starttime_F.Text = Properties.Settings.Default.MT_rebalance_starttime_F.ToString();
            MT_rebalance_starttime_G.Text = Properties.Settings.Default.MT_rebalance_starttime_G.ToString();

            MT_rebalance_stoptime_A.Text = Properties.Settings.Default.MT_rebalance_stoptime_A.ToString();
            MT_rebalance_stoptime_B.Text = Properties.Settings.Default.MT_rebalance_stoptime_B.ToString();
            MT_rebalance_stoptime_C.Text = Properties.Settings.Default.MT_rebalance_stoptime_C.ToString();
            MT_rebalance_stoptime_D.Text = Properties.Settings.Default.MT_rebalance_stoptime_D.ToString();
            MT_rebalance_stoptime_E.Text = Properties.Settings.Default.MT_rebalance_stoptime_E.ToString();
            MT_rebalance_stoptime_F.Text = Properties.Settings.Default.MT_rebalance_stoptime_F.ToString();
            MT_rebalance_stoptime_G.Text = Properties.Settings.Default.MT_rebalance_stoptime_G.ToString();

            TB_rebalance_매입금_A.Text = Properties.Settings.Default.TB_rebalance_매입금_A.ToString();
            TB_rebalance_매입금_B.Text = Properties.Settings.Default.TB_rebalance_매입금_B.ToString();
            TB_rebalance_매입금_C.Text = Properties.Settings.Default.TB_rebalance_매입금_C.ToString();
            TB_rebalance_매입금_D.Text = Properties.Settings.Default.TB_rebalance_매입금_D.ToString();
            TB_rebalance_매입금_E.Text = Properties.Settings.Default.TB_rebalance_매입금_E.ToString();
            TB_rebalance_매입금_F.Text = Properties.Settings.Default.TB_rebalance_매입금_F.ToString();
            TB_rebalance_매입금_G.Text = Properties.Settings.Default.TB_rebalance_매입금_G.ToString();

            TB_rebalance_누적거래량_A.Text = Properties.Settings.Default.TB_rebalance_누적거래량_A.ToString();
            TB_rebalance_누적거래량_B.Text = Properties.Settings.Default.TB_rebalance_누적거래량_B.ToString();
            TB_rebalance_누적거래량_C.Text = Properties.Settings.Default.TB_rebalance_누적거래량_C.ToString();
            TB_rebalance_누적거래량_D.Text = Properties.Settings.Default.TB_rebalance_누적거래량_D.ToString();
            TB_rebalance_누적거래량_E.Text = Properties.Settings.Default.TB_rebalance_누적거래량_E.ToString();
            TB_rebalance_누적거래량_F.Text = Properties.Settings.Default.TB_rebalance_누적거래량_F.ToString();
            TB_rebalance_누적거래량_G.Text = Properties.Settings.Default.TB_rebalance_누적거래량_G.ToString();

            TB_rebalance_누적거래대금_A.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_A.ToString();
            TB_rebalance_누적거래대금_B.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_B.ToString();
            TB_rebalance_누적거래대금_C.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_C.ToString();
            TB_rebalance_누적거래대금_D.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_D.ToString();
            TB_rebalance_누적거래대금_E.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_E.ToString();
            TB_rebalance_누적거래대금_F.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_F.ToString();
            TB_rebalance_누적거래대금_G.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_G.ToString();

            TB_rebalance_mma_A.Text = Properties.Settings.Default.TB_rebalance_mma_A.ToString();
            TB_rebalance_mma_B.Text = Properties.Settings.Default.TB_rebalance_mma_B.ToString();
            TB_rebalance_mma_C.Text = Properties.Settings.Default.TB_rebalance_mma_C.ToString();
            TB_rebalance_mma_D.Text = Properties.Settings.Default.TB_rebalance_mma_D.ToString();
            TB_rebalance_mma_E.Text = Properties.Settings.Default.TB_rebalance_mma_E.ToString();
            TB_rebalance_mma_F.Text = Properties.Settings.Default.TB_rebalance_mma_F.ToString();
            TB_rebalance_mma_G.Text = Properties.Settings.Default.TB_rebalance_mma_G.ToString();

            CBB_rebalance_mma_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_A;
            CBB_rebalance_mma_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_B;
            CBB_rebalance_mma_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_C;
            CBB_rebalance_mma_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_D;
            CBB_rebalance_mma_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_E;
            CBB_rebalance_mma_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_F;
            CBB_rebalance_mma_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_G;

            TB_rebalance_mma2_A.Text = Properties.Settings.Default.TB_rebalance_mma2_A.ToString();
            TB_rebalance_mma2_B.Text = Properties.Settings.Default.TB_rebalance_mma2_B.ToString();
            TB_rebalance_mma2_C.Text = Properties.Settings.Default.TB_rebalance_mma2_C.ToString();
            TB_rebalance_mma2_D.Text = Properties.Settings.Default.TB_rebalance_mma2_D.ToString();
            TB_rebalance_mma2_E.Text = Properties.Settings.Default.TB_rebalance_mma2_E.ToString();
            TB_rebalance_mma2_F.Text = Properties.Settings.Default.TB_rebalance_mma2_F.ToString();
            TB_rebalance_mma2_G.Text = Properties.Settings.Default.TB_rebalance_mma2_G.ToString();

            CBB_rebalance_mma2_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_A;
            CBB_rebalance_mma2_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_B;
            CBB_rebalance_mma2_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_C;
            CBB_rebalance_mma2_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_D;
            CBB_rebalance_mma2_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_E;
            CBB_rebalance_mma2_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_F;
            CBB_rebalance_mma2_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_G;

            CBB_rebalance_mma_배열_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_A;
            CBB_rebalance_mma_배열_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_B;
            CBB_rebalance_mma_배열_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_C;
            CBB_rebalance_mma_배열_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_D;
            CBB_rebalance_mma_배열_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_E;
            CBB_rebalance_mma_배열_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_F;
            CBB_rebalance_mma_배열_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_G;

            TB_rebalance_dma1_A.Text = Properties.Settings.Default.TB_rebalance_dma1_A.ToString();
            TB_rebalance_dma1_B.Text = Properties.Settings.Default.TB_rebalance_dma1_B.ToString();
            TB_rebalance_dma1_C.Text = Properties.Settings.Default.TB_rebalance_dma1_C.ToString();
            TB_rebalance_dma1_D.Text = Properties.Settings.Default.TB_rebalance_dma1_D.ToString();
            TB_rebalance_dma1_E.Text = Properties.Settings.Default.TB_rebalance_dma1_E.ToString();
            TB_rebalance_dma1_F.Text = Properties.Settings.Default.TB_rebalance_dma1_F.ToString();
            TB_rebalance_dma1_G.Text = Properties.Settings.Default.TB_rebalance_dma1_G.ToString();

            CBB_rebalance_dma1_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_A;
            CBB_rebalance_dma1_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_B;
            CBB_rebalance_dma1_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_C;
            CBB_rebalance_dma1_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_D;
            CBB_rebalance_dma1_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_E;
            CBB_rebalance_dma1_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_F;
            CBB_rebalance_dma1_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_G;

            TB_rebalance_dma2_A.Text = Properties.Settings.Default.TB_rebalance_dma2_A.ToString();
            TB_rebalance_dma2_B.Text = Properties.Settings.Default.TB_rebalance_dma2_B.ToString();
            TB_rebalance_dma2_C.Text = Properties.Settings.Default.TB_rebalance_dma2_C.ToString();
            TB_rebalance_dma2_D.Text = Properties.Settings.Default.TB_rebalance_dma2_D.ToString();
            TB_rebalance_dma2_E.Text = Properties.Settings.Default.TB_rebalance_dma2_E.ToString();
            TB_rebalance_dma2_F.Text = Properties.Settings.Default.TB_rebalance_dma2_F.ToString();
            TB_rebalance_dma2_G.Text = Properties.Settings.Default.TB_rebalance_dma2_G.ToString();

            CBB_rebalance_dma2_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_A;
            CBB_rebalance_dma2_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_B;
            CBB_rebalance_dma2_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_C;
            CBB_rebalance_dma2_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_D;
            CBB_rebalance_dma2_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_E;
            CBB_rebalance_dma2_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_F;
            CBB_rebalance_dma2_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_G;

            CBB_rebalance_dma_배열_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_A;
            CBB_rebalance_dma_배열_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_B;
            CBB_rebalance_dma_배열_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_C;
            CBB_rebalance_dma_배열_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_D;
            CBB_rebalance_dma_배열_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_E;
            CBB_rebalance_dma_배열_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_F;
            CBB_rebalance_dma_배열_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_G;

            if (Properties.Settings.Default.CB_rebalance_매도체크_A) { CB_rebalance_option_A.Checked = false; CB_rebalance_option_A.Enabled = false; }
            if (Properties.Settings.Default.CB_rebalance_매도체크_B) { CB_rebalance_option_B.Checked = false; CB_rebalance_option_B.Enabled = false; }
            if (Properties.Settings.Default.CB_rebalance_매도체크_C) { CB_rebalance_option_C.Checked = false; CB_rebalance_option_C.Enabled = false; }
            if (Properties.Settings.Default.CB_rebalance_매도체크_D) { CB_rebalance_option_D.Checked = false; CB_rebalance_option_D.Enabled = false; }
            if (Properties.Settings.Default.CB_rebalance_매도체크_E) { CB_rebalance_option_E.Checked = false; CB_rebalance_option_E.Enabled = false; }
            if (Properties.Settings.Default.CB_rebalance_매도체크_F) { CB_rebalance_option_F.Checked = false; CB_rebalance_option_F.Enabled = false; }
            if (Properties.Settings.Default.CB_rebalance_매도체크_G) { CB_rebalance_option_G.Checked = false; CB_rebalance_option_G.Enabled = false; }

            CB_rebalance_매도체크_A.Checked = Properties.Settings.Default.CB_rebalance_매도체크_A;
            CB_rebalance_매도체크_B.Checked = Properties.Settings.Default.CB_rebalance_매도체크_B;
            CB_rebalance_매도체크_C.Checked = Properties.Settings.Default.CB_rebalance_매도체크_C;
            CB_rebalance_매도체크_D.Checked = Properties.Settings.Default.CB_rebalance_매도체크_D;
            CB_rebalance_매도체크_E.Checked = Properties.Settings.Default.CB_rebalance_매도체크_E;
            CB_rebalance_매도체크_F.Checked = Properties.Settings.Default.CB_rebalance_매도체크_F;
            CB_rebalance_매도체크_G.Checked = Properties.Settings.Default.CB_rebalance_매도체크_G;

            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_A);
            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_B);
            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_C);
            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_D);
            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_E);
            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_F);
            FormPrint.CBB_suik_DropDownClosed(combo_rebalance_suik_gubun_G);

            CB_Liquidation_기준금.Checked = Properties.Settings.Default.CB_Liquidation_기준금;
            CB_Liquidation_SellStop_A.Checked = Properties.Settings.Default.CB_Liquidation_SellStop_A;
            CB_Liquidation_SellStop_B.Checked = Properties.Settings.Default.CB_Liquidation_SellStop_B;
            CB_Liquidation_SellStop_C.Checked = Properties.Settings.Default.CB_Liquidation_SellStop_C;

            CB_Liquidation_강제매도_A.Checked = Properties.Settings.Default.CB_Liquidation_강제매도_A;
            CB_Liquidation_강제매도_B.Checked = Properties.Settings.Default.CB_Liquidation_강제매도_B;
            CB_Liquidation_강제매도_C.Checked = Properties.Settings.Default.CB_Liquidation_강제매도_C;

            CB_추매금지_Liquidation_A.Checked = Properties.Settings.Default.CB_추매금지_Liquidation_A;
            CB_추매금지_Liquidation_B.Checked = Properties.Settings.Default.CB_추매금지_Liquidation_B;
            CB_추매금지_Liquidation_C.Checked = Properties.Settings.Default.CB_추매금지_Liquidation_C;

            CB_수익보전_Liquidation_A.Checked = Properties.Settings.Default.CB_수익보전_Liquidation_A;
            CB_수익보전_Liquidation_B.Checked = Properties.Settings.Default.CB_수익보전_Liquidation_B;
            CB_수익보전_Liquidation_C.Checked = Properties.Settings.Default.CB_수익보전_Liquidation_C;

            CB_Liquidation_choice_A.Checked = Properties.Settings.Default.CB_Liquidation_choice_A;
            CB_Liquidation_choice_B.Checked = Properties.Settings.Default.CB_Liquidation_choice_B;
            CB_Liquidation_choice_C.Checked = Properties.Settings.Default.CB_Liquidation_choice_C;
            CBB_Liquidation_suik_gubun_A.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_suik_gubun_A;
            CBB_Liquidation_suik_gubun_B.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_suik_gubun_B;
            CBB_Liquidation_suik_gubun_C.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_suik_gubun_C;
            CBB_Liquidation_sell_gubun_A.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_sell_gubun_A;
            CBB_Liquidation_sell_gubun_B.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_sell_gubun_B;
            CBB_Liquidation_sell_gubun_C.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_sell_gubun_C;
            CBB_Liquidation_jumun_A.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_jumun_A;
            CBB_Liquidation_jumun_B.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_jumun_B;
            CBB_Liquidation_jumun_C.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_jumun_C;
            CBB_Liquidation_Cancel_A.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_Cancel_A;
            CBB_Liquidation_Cancel_B.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_Cancel_B;
            CBB_Liquidation_Cancel_C.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_Cancel_C;
            MTB_Liquidation_Starttime_A.Text = Properties.Settings.Default.MTB_Liquidation_Starttime_A.ToString();
            MTB_Liquidation_Starttime_B.Text = Properties.Settings.Default.MTB_Liquidation_Starttime_B.ToString();
            MTB_Liquidation_Starttime_C.Text = Properties.Settings.Default.MTB_Liquidation_Starttime_C.ToString();
            MTB_Liquidation_Stoptime_A.Text = Properties.Settings.Default.MTB_Liquidation_Stoptime_A.ToString();
            MTB_Liquidation_Stoptime_B.Text = Properties.Settings.Default.MTB_Liquidation_Stoptime_B.ToString();
            MTB_Liquidation_Stoptime_C.Text = Properties.Settings.Default.MTB_Liquidation_Stoptime_C.ToString();
            MTB_Liquidation_delay_A.Text = Properties.Settings.Default.MTB_Liquidation_delay_A.ToString();
            MTB_Liquidation_delay_B.Text = Properties.Settings.Default.MTB_Liquidation_delay_B.ToString();
            MTB_Liquidation_delay_C.Text = Properties.Settings.Default.MTB_Liquidation_delay_C.ToString();
            MTB_Liquidation_Cancel_time_A.Text = Properties.Settings.Default.MTB_Liquidation_Cancel_time_A.ToString();
            MTB_Liquidation_Cancel_time_B.Text = Properties.Settings.Default.MTB_Liquidation_Cancel_time_B.ToString();
            MTB_Liquidation_Cancel_time_C.Text = Properties.Settings.Default.MTB_Liquidation_Cancel_time_C.ToString();
            MTB_Liquidation_repeat_A.Text = Properties.Settings.Default.MTB_Liquidation_repeat_A.ToString();
            MTB_Liquidation_repeat_B.Text = Properties.Settings.Default.MTB_Liquidation_repeat_B.ToString();
            MTB_Liquidation_repeat_C.Text = Properties.Settings.Default.MTB_Liquidation_repeat_C.ToString();
            TB_Liquidation_sell_ratio_A.Text = Properties.Settings.Default.TB_Liquidation_sell_ratio_A.ToString();
            TB_Liquidation_sell_ratio_B.Text = Properties.Settings.Default.TB_Liquidation_sell_ratio_B.ToString();
            TB_Liquidation_sell_ratio_C.Text = Properties.Settings.Default.TB_Liquidation_sell_ratio_C.ToString();
            MT_Liquidation_repeat_time_A.Text = Properties.Settings.Default.MT_Liquidation_repeat_time_A.ToString();
            MT_Liquidation_repeat_time_B.Text = Properties.Settings.Default.MT_Liquidation_repeat_time_B.ToString();
            MT_Liquidation_repeat_time_C.Text = Properties.Settings.Default.MT_Liquidation_repeat_time_C.ToString();
            TB_Liquidation_suik_1_A.Text = Properties.Settings.Default.TB_Liquidation_suik_1_A.ToString();
            TB_Liquidation_suik_1_B.Text = Properties.Settings.Default.TB_Liquidation_suik_1_B.ToString();
            TB_Liquidation_suik_1_C.Text = Properties.Settings.Default.TB_Liquidation_suik_1_C.ToString();
            TB_Liquidation_suik_2_A.Text = Properties.Settings.Default.TB_Liquidation_suik_2_A.ToString();
            TB_Liquidation_suik_2_B.Text = Properties.Settings.Default.TB_Liquidation_suik_2_B.ToString();
            TB_Liquidation_suik_2_C.Text = Properties.Settings.Default.TB_Liquidation_suik_2_C.ToString();
            TB_Liquidation_maemae_1_A.Text = Properties.Settings.Default.TB_Liquidation_maemae_1_A.ToString();
            TB_Liquidation_maemae_1_B.Text = Properties.Settings.Default.TB_Liquidation_maemae_1_B.ToString();
            TB_Liquidation_maemae_1_C.Text = Properties.Settings.Default.TB_Liquidation_maemae_1_C.ToString();
            TB_Liquidation_maemae_2_A.Text = Properties.Settings.Default.TB_Liquidation_maemae_2_A.ToString();
            TB_Liquidation_maemae_2_B.Text = Properties.Settings.Default.TB_Liquidation_maemae_2_B.ToString();
            TB_Liquidation_maemae_2_C.Text = Properties.Settings.Default.TB_Liquidation_maemae_2_C.ToString();
            TB_Liquidation_value_A.Text = Properties.Settings.Default.TB_Liquidation_value_A.ToString();
            TB_Liquidation_value_B.Text = Properties.Settings.Default.TB_Liquidation_value_B.ToString();
            TB_Liquidation_value_C.Text = Properties.Settings.Default.TB_Liquidation_value_C.ToString();

            CB_매수기준.Checked = Properties.Settings.Default.CB_매수기준;
            CB_손익기준.Checked = Properties.Settings.Default.CB_손익기준;
            TB_손익비율.Text = Properties.Settings.Default.TB_손익비율.ToString();
            TB_매수비율.Text = Properties.Settings.Default.TB_매수비율.ToString();

            TB_분할간격_A.Text = Properties.Settings.Default.TB_분할간격_A.ToString();
            TB_분할간격_B.Text = Properties.Settings.Default.TB_분할간격_B.ToString();
            TB_분할횟수_A.Text = Properties.Settings.Default.TB_분할횟수_A.ToString();
            TB_분할횟수_B.Text = Properties.Settings.Default.TB_분할횟수_B.ToString();
            TB_분할간격_C.Text = Properties.Settings.Default.TB_분할간격_C.ToString();
            TB_분할횟수_C.Text = Properties.Settings.Default.TB_분할횟수_C.ToString();

            TB_잔고청산_매입금1_A.Text = Properties.Settings.Default.TB_잔고청산_매입금1_A.ToString();
            TB_잔고청산_매입금1_B.Text = Properties.Settings.Default.TB_잔고청산_매입금1_B.ToString();
            TB_잔고청산_매입금1_C.Text = Properties.Settings.Default.TB_잔고청산_매입금1_C.ToString();

            TB_잔고청산_매입금2_A.Text = Properties.Settings.Default.TB_잔고청산_매입금2_A.ToString();
            TB_잔고청산_매입금2_B.Text = Properties.Settings.Default.TB_잔고청산_매입금2_B.ToString();
            TB_잔고청산_매입금2_C.Text = Properties.Settings.Default.TB_잔고청산_매입금2_C.ToString();

            TB_총매수금.Text = Properties.Settings.Default.TB_총매수금.ToString("N0");
            TB_일매수제한금.Text = Properties.Settings.Default.TB_일매수제한금.ToString("N0");
            TB_회수제한.Text = Properties.Settings.Default.TB_회수제한.ToString();

            TB_추매주가이상.Text = Properties.Settings.Default.TB_추매주가이상.ToString("N0");
            TB_추매주가이하.Text = Properties.Settings.Default.TB_추매주가이하.ToString("N0");
            TB_추매등락률이상.Text = Properties.Settings.Default.TB_추매등락률이상.ToString();
            TB_추매등락률이하.Text = Properties.Settings.Default.TB_추매등락률이하.ToString();

            TB_매수기준.Text = int.Parse(Properties.Settings.Default.Today_매수기준금.Split('@')[0]).ToString("N0");
            TB_손익기준.Text = int.Parse(Properties.Settings.Default.Today_손익기준금.Split('@')[0]).ToString("N0");


            FormPrint.CBB_suik_DropDownClosed(CBB_Liquidation_suik_gubun_A);
            FormPrint.CBB_suik_DropDownClosed(CBB_Liquidation_suik_gubun_B);
            FormPrint.CBB_suik_DropDownClosed(CBB_Liquidation_suik_gubun_C);

            TB_Liquidation_mma_A.Text = Properties.Settings.Default.TB_Liquidation_mma_A.ToString();
            TB_Liquidation_mma_B.Text = Properties.Settings.Default.TB_Liquidation_mma_B.ToString();
            TB_Liquidation_mma_C.Text = Properties.Settings.Default.TB_Liquidation_mma_C.ToString();

            CBB_Liquidation_mma_A.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_mma_A;
            CBB_Liquidation_mma_B.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_mma_B;
            CBB_Liquidation_mma_C.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_mma_C;

            Form1.음소거 = Properties.Settings.Default.CB_음소거;

            if (Form1.로딩완료)
            {
                if (!Properties.Settings.Default.CB_가이드매매)
                {
                    form.combo_rebalance_use_condition_A.Enabled = true;
                    form.combo_rebalance_use_condition_B.Enabled = true;
                    form.combo_rebalance_use_condition_C.Enabled = true;
                    form.combo_rebalance_use_condition_D.Enabled = true;
                    form.combo_rebalance_use_condition_E.Enabled = true;
                    form.combo_rebalance_use_condition_F.Enabled = true;
                    form.combo_rebalance_use_condition_G.Enabled = true;
                    form.combo_rebalance_condition_A.Enabled = true;
                    form.combo_rebalance_condition_B.Enabled = true;
                    form.combo_rebalance_condition_C.Enabled = true;
                    form.combo_rebalance_condition_D.Enabled = true;
                    form.combo_rebalance_condition_E.Enabled = true;
                    form.combo_rebalance_condition_F.Enabled = true;
                    form.combo_rebalance_condition_G.Enabled = true;

                    form.CBB_Liquidation_use_condition_A.Enabled = true;
                    form.CBB_Liquidation_use_condition_B.Enabled = true;
                    form.CBB_Liquidation_use_condition_C.Enabled = true;
                    form.CBB_Liquidation_condition_A.Enabled = true;
                    form.CBB_Liquidation_condition_B.Enabled = true;
                    form.CBB_Liquidation_condition_C.Enabled = true;
                }
                else
                {
                    ControllerDisable.Form_AccountManagement_Disable();
                }
            }

            CB_Liquidation_TS_A.Checked = Properties.Settings.Default.CB_Liquidation_TS_A;
            CB_Liquidation_TS_B.Checked = Properties.Settings.Default.CB_Liquidation_TS_B;
            CB_Liquidation_TS_C.Checked = Properties.Settings.Default.CB_Liquidation_TS_C;

            TB_Liquidation_TS_down_A.Text = Properties.Settings.Default.TB_Liquidation_TS_down_A.ToString();
            TB_Liquidation_TS_down_B.Text = Properties.Settings.Default.TB_Liquidation_TS_down_B.ToString();
            TB_Liquidation_TS_down_C.Text = Properties.Settings.Default.TB_Liquidation_TS_down_C.ToString();

            TB_Liquidation_TS_mma_A.Text = Properties.Settings.Default.TB_Liquidation_TS_mma_A.ToString();
            TB_Liquidation_TS_mma_B.Text = Properties.Settings.Default.TB_Liquidation_TS_mma_B.ToString();
            TB_Liquidation_TS_mma_C.Text = Properties.Settings.Default.TB_Liquidation_TS_mma_C.ToString();

            CBB_Liquidation_TS_mma_A.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_TS_mma_A;
            CBB_Liquidation_TS_mma_B.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_TS_mma_B;
            CBB_Liquidation_TS_mma_C.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_TS_mma_C;

            TB_Liquidation_TS_dma_A.Text = Properties.Settings.Default.TB_Liquidation_TS_dma_A.ToString();
            TB_Liquidation_TS_dma_B.Text = Properties.Settings.Default.TB_Liquidation_TS_dma_B.ToString();
            TB_Liquidation_TS_dma_C.Text = Properties.Settings.Default.TB_Liquidation_TS_dma_C.ToString();

            CBB_Liquidation_TS_dma_A.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_TS_dma_A;
            CBB_Liquidation_TS_dma_B.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_TS_dma_B;
            CBB_Liquidation_TS_dma_C.SelectedIndex = Properties.Settings.Default.CBB_Liquidation_TS_dma_C;

        }

        public static void 계좌관리_SAVE()
        {
            Properties.Settings.Default.CB_총매수금 = form.CB_총매수금.Checked;
            Properties.Settings.Default.CB_일매수제한금 = form.CB_일매수제한금.Checked;
            Properties.Settings.Default.CB_회수제한 = form.CB_회수제한.Checked;
            Properties.Settings.Default.CBB_SearchCondition = Form1.form1.CBB_SearchCondition.Text;

            try
            {
                double.TryParse(form.TB_잔고청산_매입금1_A.Text, out double TB_잔고청산_매입금1_A);
                double.TryParse(form.TB_잔고청산_매입금1_B.Text, out double TB_잔고청산_매입금1_B);
                double.TryParse(form.TB_잔고청산_매입금1_C.Text, out double TB_잔고청산_매입금1_C);

                Properties.Settings.Default.TB_잔고청산_매입금1_A = Math.Abs(TB_잔고청산_매입금1_A);
                Properties.Settings.Default.TB_잔고청산_매입금1_B = Math.Abs(TB_잔고청산_매입금1_B);
                Properties.Settings.Default.TB_잔고청산_매입금1_C = Math.Abs(TB_잔고청산_매입금1_C);

                form.TB_잔고청산_매입금1_A.Text = Properties.Settings.Default.TB_잔고청산_매입금1_A.ToString();
                form.TB_잔고청산_매입금1_B.Text = Properties.Settings.Default.TB_잔고청산_매입금1_B.ToString();
                form.TB_잔고청산_매입금1_C.Text = Properties.Settings.Default.TB_잔고청산_매입금1_C.ToString();

                double.TryParse(form.TB_잔고청산_매입금2_A.Text, out double TB_잔고청산_매입금2_A);
                double.TryParse(form.TB_잔고청산_매입금2_B.Text, out double TB_잔고청산_매입금2_B);
                double.TryParse(form.TB_잔고청산_매입금2_C.Text, out double TB_잔고청산_매입금2_C);

                if (TB_잔고청산_매입금2_A == 0) TB_잔고청산_매입금2_A = 10000;
                if (TB_잔고청산_매입금2_B == 0) TB_잔고청산_매입금2_B = 10000;
                if (TB_잔고청산_매입금2_C == 0) TB_잔고청산_매입금2_C = 10000;

                Properties.Settings.Default.TB_잔고청산_매입금2_A = Math.Abs(TB_잔고청산_매입금2_A);
                Properties.Settings.Default.TB_잔고청산_매입금2_B = Math.Abs(TB_잔고청산_매입금2_B);
                Properties.Settings.Default.TB_잔고청산_매입금2_C = Math.Abs(TB_잔고청산_매입금2_C);

                form.TB_잔고청산_매입금2_A.Text = Properties.Settings.Default.TB_잔고청산_매입금2_A.ToString();
                form.TB_잔고청산_매입금2_B.Text = Properties.Settings.Default.TB_잔고청산_매입금2_B.ToString();
                form.TB_잔고청산_매입금2_C.Text = Properties.Settings.Default.TB_잔고청산_매입금2_C.ToString();

                if (Properties.Settings.Default.TB_잔고청산_매입금1_A != Math.Abs(TB_잔고청산_매입금1_A) || Properties.Settings.Default.TB_잔고청산_매입금2_A != Math.Abs(TB_잔고청산_매입금2_A))
                    foreach (var code in Form1.stockBalanceList.ToList())
                    {
                        code.Value.잔고청산_매입금_A = false;
                    }
                if (Properties.Settings.Default.TB_잔고청산_매입금1_B != Math.Abs(TB_잔고청산_매입금1_B) || Properties.Settings.Default.TB_잔고청산_매입금2_A != Math.Abs(TB_잔고청산_매입금2_A))
                    foreach (var code in Form1.stockBalanceList.ToList())
                    {
                        code.Value.잔고청산_매입금_B = false;
                    }
                if (Properties.Settings.Default.TB_잔고청산_매입금1_C != Math.Abs(TB_잔고청산_매입금1_C) || Properties.Settings.Default.TB_잔고청산_매입금2_A != Math.Abs(TB_잔고청산_매입금2_A))
                    foreach (var code in Form1.stockBalanceList.ToList())
                    {
                        code.Value.잔고청산_매입금_C = false;
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine("잔고청산_저장 / 매입금 입력 오류 : " + e.Message); Form1.Error_Log("잔고청산_저장 /  매입금 입력 오류 : " + e.Message);
            }

            Properties.Settings.Default.CBB_rebalance_Selltime_A = form.CBB_rebalance_Selltime_A.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_Selltime_B = form.CBB_rebalance_Selltime_B.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_Selltime_C = form.CBB_rebalance_Selltime_C.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_Selltime_D = form.CBB_rebalance_Selltime_D.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_Selltime_E = form.CBB_rebalance_Selltime_E.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_Selltime_F = form.CBB_rebalance_Selltime_F.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_Selltime_G = form.CBB_rebalance_Selltime_G.SelectedIndex;

            try
            {
                int.TryParse(form.MTB_rebalance_Selltime_오전.Text, out int 오전);
                int.TryParse(form.MTB_rebalance_Selltime_오후.Text, out int 오후);

                if (오전 < 080000) 오전 = 084500;
                if (오후 > 200000) 오후 = 152500;

                form.MTB_rebalance_Selltime_오전.Text = 오전.ToString();
                form.MTB_rebalance_Selltime_오후.Text = 오후.ToString();

                Properties.Settings.Default.MTB_rebalance_Selltime_오전 = 오전;
                Properties.Settings.Default.MTB_rebalance_Selltime_오후 = 오후;

                Form1.form1.오전감시시간 = 오전;
                Form1.form1.오후감시시간 = 오후;

                if (Form1.form1.오전감시시간 < Form1.timenow) form.LB_리밸매도시간오전.BackColor = Color.Tan; else form.LB_리밸매도시간오전.BackColor = Color.Orange;
                if (Form1.form1.오후감시시간 < Form1.timenow) form.LB_리밸매도시간오후.BackColor = Color.Tan; else form.LB_리밸매도시간오후.BackColor = Color.Orange;

            }
            catch
            {
                Form1.AutoClosingAlram("리밸런싱 매도시간 저장 에러", "에러알림", 10, "동작");
                Form1.Error_Log("리밸런싱 매도시간 저장 에러");
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

                int rebalance_starttime_A = GET.start_stop_time(true, _rebalance_starttime_A);
                int rebalance_starttime_B = GET.start_stop_time(true, _rebalance_starttime_B);
                int rebalance_starttime_C = GET.start_stop_time(true, _rebalance_starttime_C);
                int rebalance_starttime_D = GET.start_stop_time(true, _rebalance_starttime_D);
                int rebalance_starttime_E = GET.start_stop_time(true, _rebalance_starttime_E);
                int rebalance_starttime_F = GET.start_stop_time(true, _rebalance_starttime_F);
                int rebalance_starttime_G = GET.start_stop_time(true, _rebalance_starttime_G);

                int Liquidation_Starttime_A = GET.start_stop_time(true, _Liquidation_Starttime_A);
                int Liquidation_Starttime_B = GET.start_stop_time(true, _Liquidation_Starttime_B);
                int Liquidation_Starttime_C = GET.start_stop_time(true, _Liquidation_Starttime_C);

                Properties.Settings.Default.MT_rebalance_starttime_A = rebalance_starttime_A;
                Properties.Settings.Default.MT_rebalance_starttime_B = rebalance_starttime_B;
                Properties.Settings.Default.MT_rebalance_starttime_C = rebalance_starttime_C;
                Properties.Settings.Default.MT_rebalance_starttime_D = rebalance_starttime_D;
                Properties.Settings.Default.MT_rebalance_starttime_E = rebalance_starttime_E;
                Properties.Settings.Default.MT_rebalance_starttime_F = rebalance_starttime_F;
                Properties.Settings.Default.MT_rebalance_starttime_G = rebalance_starttime_G;
                Properties.Settings.Default.MTB_Liquidation_Starttime_A = Liquidation_Starttime_A;
                Properties.Settings.Default.MTB_Liquidation_Starttime_B = Liquidation_Starttime_B;
                Properties.Settings.Default.MTB_Liquidation_Starttime_C = Liquidation_Starttime_C;

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

                int rebalance_stoptime_A = GET.start_stop_time(false, _rebalance_stoptime_A);
                int rebalance_stoptime_B = GET.start_stop_time(false, _rebalance_stoptime_B);
                int rebalance_stoptime_C = GET.start_stop_time(false, _rebalance_stoptime_C);
                int rebalance_stoptime_D = GET.start_stop_time(false, _rebalance_stoptime_D);
                int rebalance_stoptime_E = GET.start_stop_time(false, _rebalance_stoptime_E);
                int rebalance_stoptime_F = GET.start_stop_time(false, _rebalance_stoptime_F);
                int rebalance_stoptime_G = GET.start_stop_time(false, _rebalance_stoptime_G);
                int Liquidation_Stoptime_A = GET.start_stop_time(false, _Liquidation_Stoptime_A);
                int Liquidation_Stoptime_B = GET.start_stop_time(false, _Liquidation_Stoptime_B);
                int Liquidation_Stoptime_C = GET.start_stop_time(false, _Liquidation_Stoptime_C);

                Properties.Settings.Default.MT_rebalance_stoptime_A = rebalance_stoptime_A;
                Properties.Settings.Default.MT_rebalance_stoptime_B = rebalance_stoptime_B;
                Properties.Settings.Default.MT_rebalance_stoptime_C = rebalance_stoptime_C;
                Properties.Settings.Default.MT_rebalance_stoptime_D = rebalance_stoptime_D;
                Properties.Settings.Default.MT_rebalance_stoptime_E = rebalance_stoptime_E;
                Properties.Settings.Default.MT_rebalance_stoptime_F = rebalance_stoptime_F;
                Properties.Settings.Default.MT_rebalance_stoptime_G = rebalance_stoptime_G;
                Properties.Settings.Default.MTB_Liquidation_Stoptime_A = Liquidation_Stoptime_A;
                Properties.Settings.Default.MTB_Liquidation_Stoptime_B = Liquidation_Stoptime_B;
                Properties.Settings.Default.MTB_Liquidation_Stoptime_C = Liquidation_Stoptime_C;

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
                Console.WriteLine("계좌관리_저장 / 시간 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 시간 입력 오류 : " + e.Message);
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

                Properties.Settings.Default.MTB_rebalance_delay_A = _rebalance_delay_A;
                Properties.Settings.Default.MTB_rebalance_delay_B = _rebalance_delay_B;
                Properties.Settings.Default.MTB_rebalance_delay_C = _rebalance_delay_C;
                Properties.Settings.Default.MTB_rebalance_delay_D = _rebalance_delay_D;
                Properties.Settings.Default.MTB_rebalance_delay_E = _rebalance_delay_E;
                Properties.Settings.Default.MTB_rebalance_delay_F = _rebalance_delay_F;
                Properties.Settings.Default.MTB_rebalance_delay_G = _rebalance_delay_G;
                Properties.Settings.Default.MTB_Liquidation_delay_A = _Liquidation_delay_A;
                Properties.Settings.Default.MTB_Liquidation_delay_B = _Liquidation_delay_B;
                Properties.Settings.Default.MTB_Liquidation_delay_C = _Liquidation_delay_C;

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
                Console.WriteLine("계좌관리_저장 / 유지 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 유지 입력 오류 : " + e.Message);
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

                Properties.Settings.Default.TB_rebalance_suik_1_A = _rebalance_suik_1_A;
                Properties.Settings.Default.TB_rebalance_suik_1_B = _rebalance_suik_1_B;
                Properties.Settings.Default.TB_rebalance_suik_1_C = _rebalance_suik_1_C;
                Properties.Settings.Default.TB_rebalance_suik_1_D = _rebalance_suik_1_D;
                Properties.Settings.Default.TB_rebalance_suik_1_E = _rebalance_suik_1_E;
                Properties.Settings.Default.TB_rebalance_suik_1_F = _rebalance_suik_1_F;
                Properties.Settings.Default.TB_rebalance_suik_1_G = _rebalance_suik_1_G;
                Properties.Settings.Default.TB_Liquidation_suik_1_A = _Liquidation_suik_1_A;
                Properties.Settings.Default.TB_Liquidation_suik_1_B = _Liquidation_suik_1_B;
                Properties.Settings.Default.TB_Liquidation_suik_1_C = _Liquidation_suik_1_C;

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

                Properties.Settings.Default.TB_rebalance_suik_2_A = _rebalance_suik_2_A;
                Properties.Settings.Default.TB_rebalance_suik_2_B = _rebalance_suik_2_B;
                Properties.Settings.Default.TB_rebalance_suik_2_C = _rebalance_suik_2_C;
                Properties.Settings.Default.TB_rebalance_suik_2_D = _rebalance_suik_2_D;
                Properties.Settings.Default.TB_rebalance_suik_2_E = _rebalance_suik_2_E;
                Properties.Settings.Default.TB_rebalance_suik_2_F = _rebalance_suik_2_F;
                Properties.Settings.Default.TB_rebalance_suik_2_G = _rebalance_suik_2_G;
                Properties.Settings.Default.TB_Liquidation_suik_2_A = _Liquidation_suik_2_A;
                Properties.Settings.Default.TB_Liquidation_suik_2_B = _Liquidation_suik_2_B;
                Properties.Settings.Default.TB_Liquidation_suik_2_C = _Liquidation_suik_2_C;

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
                Console.WriteLine("계좌관리_저장 / 수익범위 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 수익범위 입력 오류 : " + e.Message);
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

                Properties.Settings.Default.TB_rebalance_sell_ratio_A = Math.Abs(_rebalance_sell_ratio_A);
                Properties.Settings.Default.TB_rebalance_sell_ratio_B = Math.Abs(_rebalance_sell_ratio_B);
                Properties.Settings.Default.TB_rebalance_sell_ratio_C = Math.Abs(_rebalance_sell_ratio_C);
                Properties.Settings.Default.TB_rebalance_sell_ratio_D = Math.Abs(_rebalance_sell_ratio_D);
                Properties.Settings.Default.TB_rebalance_sell_ratio_E = Math.Abs(_rebalance_sell_ratio_E);
                Properties.Settings.Default.TB_rebalance_sell_ratio_F = Math.Abs(_rebalance_sell_ratio_F);
                Properties.Settings.Default.TB_rebalance_sell_ratio_G = Math.Abs(_rebalance_sell_ratio_G);
                Properties.Settings.Default.TB_Liquidation_sell_ratio_A = Math.Abs(_Liquidation_sell_ratio_A);
                Properties.Settings.Default.TB_Liquidation_sell_ratio_B = Math.Abs(_Liquidation_sell_ratio_B);
                Properties.Settings.Default.TB_Liquidation_sell_ratio_C = Math.Abs(_Liquidation_sell_ratio_C);

                form.TB_rebalance_sell_ratio_A.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_A.ToString();
                form.TB_rebalance_sell_ratio_B.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_B.ToString();
                form.TB_rebalance_sell_ratio_C.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_C.ToString();
                form.TB_rebalance_sell_ratio_D.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_D.ToString();
                form.TB_rebalance_sell_ratio_E.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_E.ToString();
                form.TB_rebalance_sell_ratio_F.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_F.ToString();
                form.TB_rebalance_sell_ratio_G.Text = Properties.Settings.Default.TB_rebalance_sell_ratio_G.ToString();
                form.TB_Liquidation_sell_ratio_A.Text = Properties.Settings.Default.TB_Liquidation_sell_ratio_A.ToString();
                form.TB_Liquidation_sell_ratio_B.Text = Properties.Settings.Default.TB_Liquidation_sell_ratio_B.ToString();
                form.TB_Liquidation_sell_ratio_C.Text = Properties.Settings.Default.TB_Liquidation_sell_ratio_C.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 매매비중 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 /  매매비중 입력 오류 : " + e.Message);
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

                Properties.Settings.Default.TB_rebalance_maemae_1_A = Math.Abs(_rebalance_maemae_1_A);
                Properties.Settings.Default.TB_rebalance_maemae_1_B = Math.Abs(_rebalance_maemae_1_B);
                Properties.Settings.Default.TB_rebalance_maemae_1_C = Math.Abs(_rebalance_maemae_1_C);
                Properties.Settings.Default.TB_rebalance_maemae_1_D = Math.Abs(_rebalance_maemae_1_D);
                Properties.Settings.Default.TB_rebalance_maemae_1_E = Math.Abs(_rebalance_maemae_1_E);
                Properties.Settings.Default.TB_rebalance_maemae_1_F = Math.Abs(_rebalance_maemae_1_F);
                Properties.Settings.Default.TB_rebalance_maemae_1_G = Math.Abs(_rebalance_maemae_1_G);
                Properties.Settings.Default.TB_Liquidation_maemae_1_A = Math.Abs(_Liquidation_maemae_1_A);
                Properties.Settings.Default.TB_Liquidation_maemae_1_B = Math.Abs(_Liquidation_maemae_1_B);
                Properties.Settings.Default.TB_Liquidation_maemae_1_C = Math.Abs(_Liquidation_maemae_1_C);

                form.TB_rebalance_maemae_1_A.Text = Properties.Settings.Default.TB_rebalance_maemae_1_A.ToString();
                form.TB_rebalance_maemae_1_B.Text = Properties.Settings.Default.TB_rebalance_maemae_1_B.ToString();
                form.TB_rebalance_maemae_1_C.Text = Properties.Settings.Default.TB_rebalance_maemae_1_C.ToString();
                form.TB_rebalance_maemae_1_D.Text = Properties.Settings.Default.TB_rebalance_maemae_1_D.ToString();
                form.TB_rebalance_maemae_1_E.Text = Properties.Settings.Default.TB_rebalance_maemae_1_E.ToString();
                form.TB_rebalance_maemae_1_F.Text = Properties.Settings.Default.TB_rebalance_maemae_1_F.ToString();
                form.TB_rebalance_maemae_1_G.Text = Properties.Settings.Default.TB_rebalance_maemae_1_G.ToString();
                form.TB_Liquidation_maemae_1_A.Text = Properties.Settings.Default.TB_Liquidation_maemae_1_A.ToString();
                form.TB_Liquidation_maemae_1_B.Text = Properties.Settings.Default.TB_Liquidation_maemae_1_B.ToString();
                form.TB_Liquidation_maemae_1_C.Text = Properties.Settings.Default.TB_Liquidation_maemae_1_C.ToString();


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

                Properties.Settings.Default.TB_rebalance_maemae_2_A = Math.Abs(_rebalance_maemae_2_A);
                Properties.Settings.Default.TB_rebalance_maemae_2_B = Math.Abs(_rebalance_maemae_2_B);
                Properties.Settings.Default.TB_rebalance_maemae_2_C = Math.Abs(_rebalance_maemae_2_C);
                Properties.Settings.Default.TB_rebalance_maemae_2_D = Math.Abs(_rebalance_maemae_2_D);
                Properties.Settings.Default.TB_rebalance_maemae_2_E = Math.Abs(_rebalance_maemae_2_E);
                Properties.Settings.Default.TB_rebalance_maemae_2_F = Math.Abs(_rebalance_maemae_2_F);
                Properties.Settings.Default.TB_rebalance_maemae_2_G = Math.Abs(_rebalance_maemae_2_G);
                Properties.Settings.Default.TB_Liquidation_maemae_2_A = Math.Abs(_Liquidation_maemae_2_A);
                Properties.Settings.Default.TB_Liquidation_maemae_2_B = Math.Abs(_Liquidation_maemae_2_B);
                Properties.Settings.Default.TB_Liquidation_maemae_2_C = Math.Abs(_Liquidation_maemae_2_C);

                form.TB_rebalance_maemae_2_A.Text = Properties.Settings.Default.TB_rebalance_maemae_2_A.ToString();
                form.TB_rebalance_maemae_2_B.Text = Properties.Settings.Default.TB_rebalance_maemae_2_B.ToString();
                form.TB_rebalance_maemae_2_C.Text = Properties.Settings.Default.TB_rebalance_maemae_2_C.ToString();
                form.TB_rebalance_maemae_2_D.Text = Properties.Settings.Default.TB_rebalance_maemae_2_D.ToString();
                form.TB_rebalance_maemae_2_E.Text = Properties.Settings.Default.TB_rebalance_maemae_2_E.ToString();
                form.TB_rebalance_maemae_2_F.Text = Properties.Settings.Default.TB_rebalance_maemae_2_F.ToString();
                form.TB_rebalance_maemae_2_G.Text = Properties.Settings.Default.TB_rebalance_maemae_2_G.ToString();
                form.TB_Liquidation_maemae_2_A.Text = Properties.Settings.Default.TB_Liquidation_maemae_2_A.ToString();
                form.TB_Liquidation_maemae_2_B.Text = Properties.Settings.Default.TB_Liquidation_maemae_2_B.ToString();
                form.TB_Liquidation_maemae_2_C.Text = Properties.Settings.Default.TB_Liquidation_maemae_2_C.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 매매범위 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 매매범위 입력 오류 : " + e.Message);
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

                Properties.Settings.Default.MT_rebalance_repeat_time_A = _rebalance_repeat_time_A;
                Properties.Settings.Default.MT_rebalance_repeat_time_B = _rebalance_repeat_time_B;
                Properties.Settings.Default.MT_rebalance_repeat_time_C = _rebalance_repeat_time_C;
                Properties.Settings.Default.MT_rebalance_repeat_time_D = _rebalance_repeat_time_D;
                Properties.Settings.Default.MT_rebalance_repeat_time_E = _rebalance_repeat_time_E;
                Properties.Settings.Default.MT_rebalance_repeat_time_F = _rebalance_repeat_time_F;
                Properties.Settings.Default.MT_rebalance_repeat_time_G = _rebalance_repeat_time_G;
                Properties.Settings.Default.MT_Liquidation_repeat_time_A = _Liquidation_repeat_time_A;
                Properties.Settings.Default.MT_Liquidation_repeat_time_B = _Liquidation_repeat_time_B;
                Properties.Settings.Default.MT_Liquidation_repeat_time_C = _Liquidation_repeat_time_C;

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

                foreach (var item in Form1.Trading_Item_List.ToList())
                {
                    if (item.timer > 0)
                    {
                        if (item.location.Equals("리밸_A") && item.timer > _rebalance_repeat_time_A) item.timer = _rebalance_repeat_time_A;
                        if (item.location.Equals("리밸_B") && item.timer > _rebalance_repeat_time_B) item.timer = _rebalance_repeat_time_B;
                        if (item.location.Equals("리밸_C") && item.timer > _rebalance_repeat_time_C) item.timer = _rebalance_repeat_time_C;
                        if (item.location.Equals("리밸_D") && item.timer > _rebalance_repeat_time_D) item.timer = _rebalance_repeat_time_D;
                        if (item.location.Equals("리밸_E") && item.timer > _rebalance_repeat_time_E) item.timer = _rebalance_repeat_time_E;
                        if (item.location.Equals("리밸_F") && item.timer > _rebalance_repeat_time_F) item.timer = _rebalance_repeat_time_F;
                        if (item.location.Equals("리밸_G") && item.timer > _rebalance_repeat_time_G) item.timer = _rebalance_repeat_time_G;
                        if (item.location.Equals("청산_A") && item.timer > _Liquidation_repeat_time_A) item.timer = _Liquidation_repeat_time_A;
                        if (item.location.Equals("청산_B") && item.timer > _Liquidation_repeat_time_B) item.timer = _Liquidation_repeat_time_B;
                        if (item.location.Equals("청산_C") && item.timer > _Liquidation_repeat_time_C) item.timer = _Liquidation_repeat_time_C;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 반복시간 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 반복시간 입력 오류 : " + e.Message);
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

                Properties.Settings.Default.TB_rebalance_value_A = _rebalance_value_A;
                Properties.Settings.Default.TB_rebalance_value_B = _rebalance_value_B;
                Properties.Settings.Default.TB_rebalance_value_C = _rebalance_value_C;
                Properties.Settings.Default.TB_rebalance_value_D = _rebalance_value_D;
                Properties.Settings.Default.TB_rebalance_value_E = _rebalance_value_E;
                Properties.Settings.Default.TB_rebalance_value_F = _rebalance_value_F;
                Properties.Settings.Default.TB_rebalance_value_G = _rebalance_value_G;
                Properties.Settings.Default.TB_Liquidation_value_A = _Liquidation_value_A;
                Properties.Settings.Default.TB_Liquidation_value_B = _Liquidation_value_B;
                Properties.Settings.Default.TB_Liquidation_value_C = _Liquidation_value_C;

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
                Console.WriteLine("계좌관리_저장 / 매매비중 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 매매비중 입력 오류 : " + e.Message);
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

                Properties.Settings.Default.MTB_rebalance_Cancel_time_A = Cancel_time_A;
                Properties.Settings.Default.MTB_rebalance_Cancel_time_B = Cancel_time_B;
                Properties.Settings.Default.MTB_rebalance_Cancel_time_C = Cancel_time_C;
                Properties.Settings.Default.MTB_rebalance_Cancel_time_D = Cancel_time_D;
                Properties.Settings.Default.MTB_rebalance_Cancel_time_E = Cancel_time_E;
                Properties.Settings.Default.MTB_rebalance_Cancel_time_F = Cancel_time_F;
                Properties.Settings.Default.MTB_rebalance_Cancel_time_G = Cancel_time_G;
                Properties.Settings.Default.MTB_Liquidation_Cancel_time_A = _Liquidation_Cancel_time_A;
                Properties.Settings.Default.MTB_Liquidation_Cancel_time_B = _Liquidation_Cancel_time_B;
                Properties.Settings.Default.MTB_Liquidation_Cancel_time_C = _Liquidation_Cancel_time_C;

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
                Console.WriteLine("계좌관리_저장 / 리벨 취소시간 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 리벨 취소시간 입력 오류 : " + e.Message);
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

                Properties.Settings.Default.TB_rebalance_감시_value_A = 감시_value_A;
                Properties.Settings.Default.TB_rebalance_감시_value_B = 감시_value_B;
                Properties.Settings.Default.TB_rebalance_감시_value_C = 감시_value_C;
                Properties.Settings.Default.TB_rebalance_감시_value_D = 감시_value_D;
                Properties.Settings.Default.TB_rebalance_감시_value_E = 감시_value_E;
                Properties.Settings.Default.TB_rebalance_감시_value_F = 감시_value_F;
                Properties.Settings.Default.TB_rebalance_감시_value_G = 감시_value_G;

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
                Console.WriteLine("계좌관리_저장 / 리벨 감시 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 리벨 감시 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_Liquidation_repeat_A.Text, out int _Liquidation_repeat_A);
                int.TryParse(form.MTB_Liquidation_repeat_B.Text, out int _Liquidation_repeat_B);
                int.TryParse(form.MTB_Liquidation_repeat_C.Text, out int _Liquidation_repeat_C);

                if (form.CBB_Liquidation_Cancel_A.SelectedIndex == 0) _Liquidation_repeat_A = 0;
                if (form.CBB_Liquidation_Cancel_B.SelectedIndex == 0) _Liquidation_repeat_B = 0;
                if (form.CBB_Liquidation_Cancel_C.SelectedIndex == 0) _Liquidation_repeat_C = 0;

                Properties.Settings.Default.MTB_Liquidation_repeat_A = _Liquidation_repeat_A;
                Properties.Settings.Default.MTB_Liquidation_repeat_B = _Liquidation_repeat_B;
                Properties.Settings.Default.MTB_Liquidation_repeat_C = _Liquidation_repeat_C;

                form.MTB_Liquidation_repeat_A.Text = _Liquidation_repeat_A.ToString();
                form.MTB_Liquidation_repeat_B.Text = _Liquidation_repeat_B.ToString();
                form.MTB_Liquidation_repeat_C.Text = _Liquidation_repeat_C.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 청산반복 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 청산반복 입력 오류 : " + e.Message);
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

                Properties.Settings.Default.TB_rebalance_sellratio1_A = sellratio1_A;
                Properties.Settings.Default.TB_rebalance_sellratio1_B = sellratio1_B;
                Properties.Settings.Default.TB_rebalance_sellratio1_C = sellratio1_C;
                Properties.Settings.Default.TB_rebalance_sellratio1_D = sellratio1_D;
                Properties.Settings.Default.TB_rebalance_sellratio1_E = sellratio1_E;
                Properties.Settings.Default.TB_rebalance_sellratio1_F = sellratio1_F;
                Properties.Settings.Default.TB_rebalance_sellratio1_G = sellratio1_G;

                form.TB_rebalance_sellratio1_A.Text = Properties.Settings.Default.TB_rebalance_sellratio1_A.ToString();
                form.TB_rebalance_sellratio1_B.Text = Properties.Settings.Default.TB_rebalance_sellratio1_B.ToString();
                form.TB_rebalance_sellratio1_C.Text = Properties.Settings.Default.TB_rebalance_sellratio1_C.ToString();
                form.TB_rebalance_sellratio1_D.Text = Properties.Settings.Default.TB_rebalance_sellratio1_D.ToString();
                form.TB_rebalance_sellratio1_E.Text = Properties.Settings.Default.TB_rebalance_sellratio1_E.ToString();
                form.TB_rebalance_sellratio1_F.Text = Properties.Settings.Default.TB_rebalance_sellratio1_F.ToString();
                form.TB_rebalance_sellratio1_G.Text = Properties.Settings.Default.TB_rebalance_sellratio1_G.ToString();

                double.TryParse(form.TB_rebalance_sellratio2_A.Text, out double sellratio2_A);
                double.TryParse(form.TB_rebalance_sellratio2_B.Text, out double sellratio2_B);
                double.TryParse(form.TB_rebalance_sellratio2_C.Text, out double sellratio2_C);
                double.TryParse(form.TB_rebalance_sellratio2_D.Text, out double sellratio2_D);
                double.TryParse(form.TB_rebalance_sellratio2_E.Text, out double sellratio2_E);
                double.TryParse(form.TB_rebalance_sellratio2_F.Text, out double sellratio2_F);
                double.TryParse(form.TB_rebalance_sellratio2_G.Text, out double sellratio2_G);

                Properties.Settings.Default.TB_rebalance_sellratio2_A = sellratio2_A;
                Properties.Settings.Default.TB_rebalance_sellratio2_B = sellratio2_B;
                Properties.Settings.Default.TB_rebalance_sellratio2_C = sellratio2_C;
                Properties.Settings.Default.TB_rebalance_sellratio2_D = sellratio2_D;
                Properties.Settings.Default.TB_rebalance_sellratio2_E = sellratio2_E;
                Properties.Settings.Default.TB_rebalance_sellratio2_F = sellratio2_F;
                Properties.Settings.Default.TB_rebalance_sellratio2_G = sellratio2_G;

                form.TB_rebalance_sellratio2_A.Text = Properties.Settings.Default.TB_rebalance_sellratio2_A.ToString();
                form.TB_rebalance_sellratio2_B.Text = Properties.Settings.Default.TB_rebalance_sellratio2_B.ToString();
                form.TB_rebalance_sellratio2_C.Text = Properties.Settings.Default.TB_rebalance_sellratio2_C.ToString();
                form.TB_rebalance_sellratio2_D.Text = Properties.Settings.Default.TB_rebalance_sellratio2_D.ToString();
                form.TB_rebalance_sellratio2_E.Text = Properties.Settings.Default.TB_rebalance_sellratio2_E.ToString();
                form.TB_rebalance_sellratio2_F.Text = Properties.Settings.Default.TB_rebalance_sellratio2_F.ToString();
                form.TB_rebalance_sellratio2_G.Text = Properties.Settings.Default.TB_rebalance_sellratio2_G.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 리벨 매도 수익율 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 리벨 매도 수익율 입력 오류 : " + e.Message);
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

                Properties.Settings.Default.TB_rebalance_sellvolume1_A = Math.Abs(sellvolume1_A);
                Properties.Settings.Default.TB_rebalance_sellvolume1_B = Math.Abs(sellvolume1_B);
                Properties.Settings.Default.TB_rebalance_sellvolume1_C = Math.Abs(sellvolume1_C);
                Properties.Settings.Default.TB_rebalance_sellvolume1_D = Math.Abs(sellvolume1_D);
                Properties.Settings.Default.TB_rebalance_sellvolume1_E = Math.Abs(sellvolume1_E);
                Properties.Settings.Default.TB_rebalance_sellvolume1_F = Math.Abs(sellvolume1_F);
                Properties.Settings.Default.TB_rebalance_sellvolume1_G = Math.Abs(sellvolume1_G);

                form.TB_rebalance_sellvolume1_A.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_A.ToString();
                form.TB_rebalance_sellvolume1_B.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_B.ToString();
                form.TB_rebalance_sellvolume1_C.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_C.ToString();
                form.TB_rebalance_sellvolume1_D.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_D.ToString();
                form.TB_rebalance_sellvolume1_E.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_E.ToString();
                form.TB_rebalance_sellvolume1_F.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_F.ToString();
                form.TB_rebalance_sellvolume1_G.Text = Properties.Settings.Default.TB_rebalance_sellvolume1_G.ToString();

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

                Properties.Settings.Default.TB_rebalance_sellvolume2_A = Math.Abs(sellvolume2_A);
                Properties.Settings.Default.TB_rebalance_sellvolume2_B = Math.Abs(sellvolume2_B);
                Properties.Settings.Default.TB_rebalance_sellvolume2_C = Math.Abs(sellvolume2_C);
                Properties.Settings.Default.TB_rebalance_sellvolume2_D = Math.Abs(sellvolume2_D);
                Properties.Settings.Default.TB_rebalance_sellvolume2_E = Math.Abs(sellvolume2_E);
                Properties.Settings.Default.TB_rebalance_sellvolume2_F = Math.Abs(sellvolume2_F);
                Properties.Settings.Default.TB_rebalance_sellvolume2_G = Math.Abs(sellvolume2_G);

                form.TB_rebalance_sellvolume2_A.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_A.ToString();
                form.TB_rebalance_sellvolume2_B.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_B.ToString();
                form.TB_rebalance_sellvolume2_C.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_C.ToString();
                form.TB_rebalance_sellvolume2_D.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_D.ToString();
                form.TB_rebalance_sellvolume2_E.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_E.ToString();
                form.TB_rebalance_sellvolume2_F.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_F.ToString();
                form.TB_rebalance_sellvolume2_G.Text = Properties.Settings.Default.TB_rebalance_sellvolume2_G.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 리벨 1차매도 비율 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 리벨 1차매도 비율 입력 오류 : " + e.Message);
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

                Properties.Settings.Default.TB_rebalance_sellcancel1_A = Math.Abs(sellcancel1_A);
                Properties.Settings.Default.TB_rebalance_sellcancel1_B = Math.Abs(sellcancel1_B);
                Properties.Settings.Default.TB_rebalance_sellcancel1_C = Math.Abs(sellcancel1_C);
                Properties.Settings.Default.TB_rebalance_sellcancel1_D = Math.Abs(sellcancel1_D);
                Properties.Settings.Default.TB_rebalance_sellcancel1_E = Math.Abs(sellcancel1_E);
                Properties.Settings.Default.TB_rebalance_sellcancel1_F = Math.Abs(sellcancel1_F);
                Properties.Settings.Default.TB_rebalance_sellcancel1_G = Math.Abs(sellcancel1_G);

                form.TB_rebalance_sellcancel1_A.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_A.ToString();
                form.TB_rebalance_sellcancel1_B.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_B.ToString();
                form.TB_rebalance_sellcancel1_C.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_C.ToString();
                form.TB_rebalance_sellcancel1_D.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_D.ToString();
                form.TB_rebalance_sellcancel1_E.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_E.ToString();
                form.TB_rebalance_sellcancel1_F.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_F.ToString();
                form.TB_rebalance_sellcancel1_G.Text = Properties.Settings.Default.TB_rebalance_sellcancel1_G.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 리벨 1차매도 취소시간 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 리벨 1차매도 취소시간 입력 오류 : " + e.Message);
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

                Properties.Settings.Default.TB_rebalance_sellcancel2_A = Math.Abs(sellcancel2_A);
                Properties.Settings.Default.TB_rebalance_sellcancel2_B = Math.Abs(sellcancel2_B);
                Properties.Settings.Default.TB_rebalance_sellcancel2_C = Math.Abs(sellcancel2_C);
                Properties.Settings.Default.TB_rebalance_sellcancel2_D = Math.Abs(sellcancel2_D);
                Properties.Settings.Default.TB_rebalance_sellcancel2_E = Math.Abs(sellcancel2_E);
                Properties.Settings.Default.TB_rebalance_sellcancel2_F = Math.Abs(sellcancel2_F);
                Properties.Settings.Default.TB_rebalance_sellcancel2_G = Math.Abs(sellcancel2_G);

                form.TB_rebalance_sellcancel2_A.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_A.ToString();
                form.TB_rebalance_sellcancel2_B.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_B.ToString();
                form.TB_rebalance_sellcancel2_C.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_C.ToString();
                form.TB_rebalance_sellcancel2_D.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_D.ToString();
                form.TB_rebalance_sellcancel2_E.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_E.ToString();
                form.TB_rebalance_sellcancel2_F.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_F.ToString();
                form.TB_rebalance_sellcancel2_G.Text = Properties.Settings.Default.TB_rebalance_sellcancel2_G.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 리벨 2차매도 취소시간 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 리벨 2차매도 취소시간 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_rebalance_매입금_A.Text, out double TB_rebalance_매입금_A);
                double.TryParse(form.TB_rebalance_매입금_B.Text, out double TB_rebalance_매입금_B);
                double.TryParse(form.TB_rebalance_매입금_C.Text, out double TB_rebalance_매입금_C);
                double.TryParse(form.TB_rebalance_매입금_D.Text, out double TB_rebalance_매입금_D);
                double.TryParse(form.TB_rebalance_매입금_E.Text, out double TB_rebalance_매입금_E);
                double.TryParse(form.TB_rebalance_매입금_F.Text, out double TB_rebalance_매입금_F);
                double.TryParse(form.TB_rebalance_매입금_G.Text, out double TB_rebalance_매입금_G);

                Properties.Settings.Default.TB_rebalance_매입금_A = Math.Abs(TB_rebalance_매입금_A);
                Properties.Settings.Default.TB_rebalance_매입금_B = Math.Abs(TB_rebalance_매입금_B);
                Properties.Settings.Default.TB_rebalance_매입금_C = Math.Abs(TB_rebalance_매입금_C);
                Properties.Settings.Default.TB_rebalance_매입금_D = Math.Abs(TB_rebalance_매입금_D);
                Properties.Settings.Default.TB_rebalance_매입금_E = Math.Abs(TB_rebalance_매입금_E);
                Properties.Settings.Default.TB_rebalance_매입금_F = Math.Abs(TB_rebalance_매입금_F);
                Properties.Settings.Default.TB_rebalance_매입금_G = Math.Abs(TB_rebalance_매입금_G);

                form.TB_rebalance_매입금_A.Text = Properties.Settings.Default.TB_rebalance_매입금_A.ToString();
                form.TB_rebalance_매입금_B.Text = Properties.Settings.Default.TB_rebalance_매입금_B.ToString();
                form.TB_rebalance_매입금_C.Text = Properties.Settings.Default.TB_rebalance_매입금_C.ToString();
                form.TB_rebalance_매입금_D.Text = Properties.Settings.Default.TB_rebalance_매입금_D.ToString();
                form.TB_rebalance_매입금_E.Text = Properties.Settings.Default.TB_rebalance_매입금_E.ToString();
                form.TB_rebalance_매입금_F.Text = Properties.Settings.Default.TB_rebalance_매입금_F.ToString();
                form.TB_rebalance_매입금_G.Text = Properties.Settings.Default.TB_rebalance_매입금_G.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 에러: " + e.Message); Form1.Error_Log("계좌관리_저장 에러: " + e.Message);
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

                Properties.Settings.Default.TB_rebalance_누적거래량_A = TB_rebalance_누적거래량_A;
                Properties.Settings.Default.TB_rebalance_누적거래량_B = TB_rebalance_누적거래량_B;
                Properties.Settings.Default.TB_rebalance_누적거래량_C = TB_rebalance_누적거래량_C;
                Properties.Settings.Default.TB_rebalance_누적거래량_D = TB_rebalance_누적거래량_D;
                Properties.Settings.Default.TB_rebalance_누적거래량_E = TB_rebalance_누적거래량_E;
                Properties.Settings.Default.TB_rebalance_누적거래량_F = TB_rebalance_누적거래량_F;
                Properties.Settings.Default.TB_rebalance_누적거래량_G = TB_rebalance_누적거래량_G;

                form.TB_rebalance_누적거래량_A.Text = Properties.Settings.Default.TB_rebalance_누적거래량_A.ToString();
                form.TB_rebalance_누적거래량_B.Text = Properties.Settings.Default.TB_rebalance_누적거래량_B.ToString();
                form.TB_rebalance_누적거래량_C.Text = Properties.Settings.Default.TB_rebalance_누적거래량_C.ToString();
                form.TB_rebalance_누적거래량_D.Text = Properties.Settings.Default.TB_rebalance_누적거래량_D.ToString();
                form.TB_rebalance_누적거래량_E.Text = Properties.Settings.Default.TB_rebalance_누적거래량_E.ToString();
                form.TB_rebalance_누적거래량_F.Text = Properties.Settings.Default.TB_rebalance_누적거래량_F.ToString();
                form.TB_rebalance_누적거래량_G.Text = Properties.Settings.Default.TB_rebalance_누적거래량_G.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 에러: " + e.Message); Form1.Error_Log("계좌관리_저장 에러: " + e.Message);
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

                Properties.Settings.Default.TB_rebalance_누적거래대금_A = TB_rebalance_누적거래대금_A;
                Properties.Settings.Default.TB_rebalance_누적거래대금_B = TB_rebalance_누적거래대금_B;
                Properties.Settings.Default.TB_rebalance_누적거래대금_C = TB_rebalance_누적거래대금_C;
                Properties.Settings.Default.TB_rebalance_누적거래대금_D = TB_rebalance_누적거래대금_D;
                Properties.Settings.Default.TB_rebalance_누적거래대금_E = TB_rebalance_누적거래대금_E;
                Properties.Settings.Default.TB_rebalance_누적거래대금_F = TB_rebalance_누적거래대금_F;
                Properties.Settings.Default.TB_rebalance_누적거래대금_G = TB_rebalance_누적거래대금_G;

                form.TB_rebalance_누적거래대금_A.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_A.ToString();
                form.TB_rebalance_누적거래대금_B.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_B.ToString();
                form.TB_rebalance_누적거래대금_C.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_C.ToString();
                form.TB_rebalance_누적거래대금_D.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_D.ToString();
                form.TB_rebalance_누적거래대금_E.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_E.ToString();
                form.TB_rebalance_누적거래대금_F.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_F.ToString();
                form.TB_rebalance_누적거래대금_G.Text = Properties.Settings.Default.TB_rebalance_누적거래대금_G.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 에러: " + e.Message); Form1.Error_Log("계좌관리_저장 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_rebalance_mma_A.Text, out int TB_rebalance_mma_A);
                int.TryParse(form.TB_rebalance_mma_B.Text, out int TB_rebalance_mma_B);
                int.TryParse(form.TB_rebalance_mma_C.Text, out int TB_rebalance_mma_C);
                int.TryParse(form.TB_rebalance_mma_D.Text, out int TB_rebalance_mma_D);
                int.TryParse(form.TB_rebalance_mma_E.Text, out int TB_rebalance_mma_E);
                int.TryParse(form.TB_rebalance_mma_F.Text, out int TB_rebalance_mma_F);
                int.TryParse(form.TB_rebalance_mma_G.Text, out int TB_rebalance_mma_G);

                if (TB_rebalance_mma_A == 0) TB_rebalance_mma_A = 3;
                if (TB_rebalance_mma_B == 0) TB_rebalance_mma_B = 3;
                if (TB_rebalance_mma_C == 0) TB_rebalance_mma_C = 3;
                if (TB_rebalance_mma_D == 0) TB_rebalance_mma_D = 3;
                if (TB_rebalance_mma_E == 0) TB_rebalance_mma_E = 3;
                if (TB_rebalance_mma_F == 0) TB_rebalance_mma_F = 3;
                if (TB_rebalance_mma_G == 0) TB_rebalance_mma_G = 3;

                if (TB_rebalance_mma_A > 300) TB_rebalance_mma_A = 300;
                if (TB_rebalance_mma_B > 300) TB_rebalance_mma_B = 300;
                if (TB_rebalance_mma_C > 300) TB_rebalance_mma_C = 300;
                if (TB_rebalance_mma_D > 300) TB_rebalance_mma_D = 300;
                if (TB_rebalance_mma_E > 300) TB_rebalance_mma_E = 300;
                if (TB_rebalance_mma_F > 300) TB_rebalance_mma_F = 300;
                if (TB_rebalance_mma_G > 300) TB_rebalance_mma_G = 300;

                Properties.Settings.Default.TB_rebalance_mma_A = TB_rebalance_mma_A;
                Properties.Settings.Default.TB_rebalance_mma_B = TB_rebalance_mma_B;
                Properties.Settings.Default.TB_rebalance_mma_C = TB_rebalance_mma_C;
                Properties.Settings.Default.TB_rebalance_mma_D = TB_rebalance_mma_D;
                Properties.Settings.Default.TB_rebalance_mma_E = TB_rebalance_mma_E;
                Properties.Settings.Default.TB_rebalance_mma_F = TB_rebalance_mma_F;
                Properties.Settings.Default.TB_rebalance_mma_G = TB_rebalance_mma_G;

                form.TB_rebalance_mma_A.Text = TB_rebalance_mma_A.ToString();
                form.TB_rebalance_mma_B.Text = TB_rebalance_mma_B.ToString();
                form.TB_rebalance_mma_C.Text = TB_rebalance_mma_C.ToString();
                form.TB_rebalance_mma_D.Text = TB_rebalance_mma_D.ToString();
                form.TB_rebalance_mma_E.Text = TB_rebalance_mma_E.ToString();
                form.TB_rebalance_mma_F.Text = TB_rebalance_mma_F.ToString();
                form.TB_rebalance_mma_G.Text = TB_rebalance_mma_G.ToString();

                Properties.Settings.Default.CBB_rebalance_mma_A = form.CBB_rebalance_mma_A.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma_B = form.CBB_rebalance_mma_B.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma_C = form.CBB_rebalance_mma_C.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma_D = form.CBB_rebalance_mma_D.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma_E = form.CBB_rebalance_mma_E.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma_F = form.CBB_rebalance_mma_F.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma_G = form.CBB_rebalance_mma_G.SelectedIndex;

                int.TryParse(form.TB_rebalance_mma2_A.Text, out int TB_rebalance_mma2_A);
                int.TryParse(form.TB_rebalance_mma2_B.Text, out int TB_rebalance_mma2_B);
                int.TryParse(form.TB_rebalance_mma2_C.Text, out int TB_rebalance_mma2_C);
                int.TryParse(form.TB_rebalance_mma2_D.Text, out int TB_rebalance_mma2_D);
                int.TryParse(form.TB_rebalance_mma2_E.Text, out int TB_rebalance_mma2_E);
                int.TryParse(form.TB_rebalance_mma2_F.Text, out int TB_rebalance_mma2_F);
                int.TryParse(form.TB_rebalance_mma2_G.Text, out int TB_rebalance_mma2_G);

                if (TB_rebalance_mma2_A == 0) TB_rebalance_mma2_A = 5;
                if (TB_rebalance_mma2_B == 0) TB_rebalance_mma2_B = 5;
                if (TB_rebalance_mma2_C == 0) TB_rebalance_mma2_C = 5;
                if (TB_rebalance_mma2_D == 0) TB_rebalance_mma2_D = 5;
                if (TB_rebalance_mma2_E == 0) TB_rebalance_mma2_E = 5;
                if (TB_rebalance_mma2_F == 0) TB_rebalance_mma2_F = 5;
                if (TB_rebalance_mma2_G == 0) TB_rebalance_mma2_G = 5;

                if (TB_rebalance_mma2_A > 300) TB_rebalance_mma2_A = 300;
                if (TB_rebalance_mma2_B > 300) TB_rebalance_mma2_B = 300;
                if (TB_rebalance_mma2_C > 300) TB_rebalance_mma2_C = 300;
                if (TB_rebalance_mma2_D > 300) TB_rebalance_mma2_D = 300;
                if (TB_rebalance_mma2_E > 300) TB_rebalance_mma2_E = 300;
                if (TB_rebalance_mma2_F > 300) TB_rebalance_mma2_F = 300;
                if (TB_rebalance_mma2_G > 300) TB_rebalance_mma2_G = 300;

                Properties.Settings.Default.TB_rebalance_mma2_A = TB_rebalance_mma2_A;
                Properties.Settings.Default.TB_rebalance_mma2_B = TB_rebalance_mma2_B;
                Properties.Settings.Default.TB_rebalance_mma2_C = TB_rebalance_mma2_C;
                Properties.Settings.Default.TB_rebalance_mma2_D = TB_rebalance_mma2_D;
                Properties.Settings.Default.TB_rebalance_mma2_E = TB_rebalance_mma2_E;
                Properties.Settings.Default.TB_rebalance_mma2_F = TB_rebalance_mma2_F;
                Properties.Settings.Default.TB_rebalance_mma2_G = TB_rebalance_mma2_G;

                form.TB_rebalance_mma2_A.Text = TB_rebalance_mma2_A.ToString();
                form.TB_rebalance_mma2_B.Text = TB_rebalance_mma2_B.ToString();
                form.TB_rebalance_mma2_C.Text = TB_rebalance_mma2_C.ToString();
                form.TB_rebalance_mma2_D.Text = TB_rebalance_mma2_D.ToString();
                form.TB_rebalance_mma2_E.Text = TB_rebalance_mma2_E.ToString();
                form.TB_rebalance_mma2_F.Text = TB_rebalance_mma2_F.ToString();
                form.TB_rebalance_mma2_G.Text = TB_rebalance_mma2_G.ToString();

                Properties.Settings.Default.CBB_rebalance_mma2_A = form.CBB_rebalance_mma2_A.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma2_B = form.CBB_rebalance_mma2_B.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma2_C = form.CBB_rebalance_mma2_C.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma2_D = form.CBB_rebalance_mma2_D.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma2_E = form.CBB_rebalance_mma2_E.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma2_F = form.CBB_rebalance_mma2_F.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma2_G = form.CBB_rebalance_mma2_G.SelectedIndex;

                Properties.Settings.Default.CBB_rebalance_mma_배열_A = form.CBB_rebalance_mma_배열_A.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma_배열_B = form.CBB_rebalance_mma_배열_B.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma_배열_C = form.CBB_rebalance_mma_배열_C.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma_배열_D = form.CBB_rebalance_mma_배열_D.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma_배열_E = form.CBB_rebalance_mma_배열_E.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma_배열_F = form.CBB_rebalance_mma_배열_F.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_mma_배열_G = form.CBB_rebalance_mma_배열_G.SelectedIndex;

                int.TryParse(form.TB_rebalance_dma1_A.Text, out int TB_rebalance_dma1_A);
                int.TryParse(form.TB_rebalance_dma1_B.Text, out int TB_rebalance_dma1_B);
                int.TryParse(form.TB_rebalance_dma1_C.Text, out int TB_rebalance_dma1_C);
                int.TryParse(form.TB_rebalance_dma1_D.Text, out int TB_rebalance_dma1_D);
                int.TryParse(form.TB_rebalance_dma1_E.Text, out int TB_rebalance_dma1_E);
                int.TryParse(form.TB_rebalance_dma1_F.Text, out int TB_rebalance_dma1_F);
                int.TryParse(form.TB_rebalance_dma1_G.Text, out int TB_rebalance_dma1_G);

                if (TB_rebalance_dma1_A == 0) TB_rebalance_dma1_A = 5;
                if (TB_rebalance_dma1_B == 0) TB_rebalance_dma1_B = 5;
                if (TB_rebalance_dma1_C == 0) TB_rebalance_dma1_C = 5;
                if (TB_rebalance_dma1_D == 0) TB_rebalance_dma1_D = 5;
                if (TB_rebalance_dma1_E == 0) TB_rebalance_dma1_E = 5;
                if (TB_rebalance_dma1_F == 0) TB_rebalance_dma1_F = 5;
                if (TB_rebalance_dma1_G == 0) TB_rebalance_dma1_G = 5;

                if (TB_rebalance_dma1_A > 300) TB_rebalance_dma1_A = 300;
                if (TB_rebalance_dma1_B > 300) TB_rebalance_dma1_B = 300;
                if (TB_rebalance_dma1_C > 300) TB_rebalance_dma1_C = 300;
                if (TB_rebalance_dma1_D > 300) TB_rebalance_dma1_D = 300;
                if (TB_rebalance_dma1_E > 300) TB_rebalance_dma1_E = 300;
                if (TB_rebalance_dma1_F > 300) TB_rebalance_dma1_F = 300;
                if (TB_rebalance_dma1_G > 300) TB_rebalance_dma1_G = 300;

                Properties.Settings.Default.TB_rebalance_dma1_A = TB_rebalance_dma1_A;
                Properties.Settings.Default.TB_rebalance_dma1_B = TB_rebalance_dma1_B;
                Properties.Settings.Default.TB_rebalance_dma1_C = TB_rebalance_dma1_C;
                Properties.Settings.Default.TB_rebalance_dma1_D = TB_rebalance_dma1_D;
                Properties.Settings.Default.TB_rebalance_dma1_E = TB_rebalance_dma1_E;
                Properties.Settings.Default.TB_rebalance_dma1_F = TB_rebalance_dma1_F;
                Properties.Settings.Default.TB_rebalance_dma1_G = TB_rebalance_dma1_G;

                form.TB_rebalance_dma1_A.Text = TB_rebalance_dma1_A.ToString();
                form.TB_rebalance_dma1_B.Text = TB_rebalance_dma1_B.ToString();
                form.TB_rebalance_dma1_C.Text = TB_rebalance_dma1_C.ToString();
                form.TB_rebalance_dma1_D.Text = TB_rebalance_dma1_D.ToString();
                form.TB_rebalance_dma1_E.Text = TB_rebalance_dma1_E.ToString();
                form.TB_rebalance_dma1_F.Text = TB_rebalance_dma1_F.ToString();
                form.TB_rebalance_dma1_G.Text = TB_rebalance_dma1_G.ToString();

                Properties.Settings.Default.CBB_rebalance_dma1_A = form.CBB_rebalance_dma1_A.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma1_B = form.CBB_rebalance_dma1_B.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma1_C = form.CBB_rebalance_dma1_C.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma1_D = form.CBB_rebalance_dma1_D.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma1_E = form.CBB_rebalance_dma1_E.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma1_F = form.CBB_rebalance_dma1_F.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma1_G = form.CBB_rebalance_dma1_G.SelectedIndex;

                int.TryParse(form.TB_rebalance_dma2_A.Text, out int TB_rebalance_dma2_A);
                int.TryParse(form.TB_rebalance_dma2_B.Text, out int TB_rebalance_dma2_B);
                int.TryParse(form.TB_rebalance_dma2_C.Text, out int TB_rebalance_dma2_C);
                int.TryParse(form.TB_rebalance_dma2_D.Text, out int TB_rebalance_dma2_D);
                int.TryParse(form.TB_rebalance_dma2_E.Text, out int TB_rebalance_dma2_E);
                int.TryParse(form.TB_rebalance_dma2_F.Text, out int TB_rebalance_dma2_F);
                int.TryParse(form.TB_rebalance_dma2_G.Text, out int TB_rebalance_dma2_G);

                if (TB_rebalance_dma2_A == 0) TB_rebalance_dma2_A = 20;
                if (TB_rebalance_dma2_B == 0) TB_rebalance_dma2_B = 20;
                if (TB_rebalance_dma2_C == 0) TB_rebalance_dma2_C = 20;
                if (TB_rebalance_dma2_D == 0) TB_rebalance_dma2_D = 20;
                if (TB_rebalance_dma2_E == 0) TB_rebalance_dma2_E = 20;
                if (TB_rebalance_dma2_F == 0) TB_rebalance_dma2_F = 20;
                if (TB_rebalance_dma2_G == 0) TB_rebalance_dma2_G = 20;

                if (TB_rebalance_dma2_A > 300) TB_rebalance_dma2_A = 300;
                if (TB_rebalance_dma2_B > 300) TB_rebalance_dma2_B = 300;
                if (TB_rebalance_dma2_C > 300) TB_rebalance_dma2_C = 300;
                if (TB_rebalance_dma2_D > 300) TB_rebalance_dma2_D = 300;
                if (TB_rebalance_dma2_E > 300) TB_rebalance_dma2_E = 300;
                if (TB_rebalance_dma2_F > 300) TB_rebalance_dma2_F = 300;
                if (TB_rebalance_dma2_G > 300) TB_rebalance_dma2_G = 300;

                Properties.Settings.Default.TB_rebalance_dma2_A = TB_rebalance_dma2_A;
                Properties.Settings.Default.TB_rebalance_dma2_B = TB_rebalance_dma2_B;
                Properties.Settings.Default.TB_rebalance_dma2_C = TB_rebalance_dma2_C;
                Properties.Settings.Default.TB_rebalance_dma2_D = TB_rebalance_dma2_D;
                Properties.Settings.Default.TB_rebalance_dma2_E = TB_rebalance_dma2_E;
                Properties.Settings.Default.TB_rebalance_dma2_F = TB_rebalance_dma2_F;
                Properties.Settings.Default.TB_rebalance_dma2_G = TB_rebalance_dma2_G;

                form.TB_rebalance_dma2_A.Text = TB_rebalance_dma2_A.ToString();
                form.TB_rebalance_dma2_B.Text = TB_rebalance_dma2_B.ToString();
                form.TB_rebalance_dma2_C.Text = TB_rebalance_dma2_C.ToString();
                form.TB_rebalance_dma2_D.Text = TB_rebalance_dma2_D.ToString();
                form.TB_rebalance_dma2_E.Text = TB_rebalance_dma2_E.ToString();
                form.TB_rebalance_dma2_F.Text = TB_rebalance_dma2_F.ToString();
                form.TB_rebalance_dma2_G.Text = TB_rebalance_dma2_G.ToString();

                Properties.Settings.Default.CBB_rebalance_dma2_A = form.CBB_rebalance_dma2_A.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma2_B = form.CBB_rebalance_dma2_B.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma2_C = form.CBB_rebalance_dma2_C.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma2_D = form.CBB_rebalance_dma2_D.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma2_E = form.CBB_rebalance_dma2_E.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma2_F = form.CBB_rebalance_dma2_F.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma2_G = form.CBB_rebalance_dma2_G.SelectedIndex;

                Properties.Settings.Default.CBB_rebalance_dma_배열_A = form.CBB_rebalance_dma_배열_A.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma_배열_B = form.CBB_rebalance_dma_배열_B.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma_배열_C = form.CBB_rebalance_dma_배열_C.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma_배열_D = form.CBB_rebalance_dma_배열_D.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma_배열_E = form.CBB_rebalance_dma_배열_E.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma_배열_F = form.CBB_rebalance_dma_배열_F.SelectedIndex;
                Properties.Settings.Default.CBB_rebalance_dma_배열_G = form.CBB_rebalance_dma_배열_G.SelectedIndex;


            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 실현손익대비관리 시간입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 실현손익대비관리 시간입력 오류 : " + e.Message);
            }


            try
            {
                int.TryParse(form.MTB_cut_time_A.Text, out int cut_time_A);
                int.TryParse(form.MTB_cut_time_B.Text, out int cut_time_B);
                int.TryParse(form.MTB_cut_time_C.Text, out int cut_time_C);

                if (cut_time_A == 0) cut_time_A = 150000;
                if (cut_time_B == 0) cut_time_B = 150000;
                if (cut_time_C == 0) cut_time_C = 150000;

                Properties.Settings.Default.MTB_cut_time_A = cut_time_A;
                Properties.Settings.Default.MTB_cut_time_B = cut_time_B;
                Properties.Settings.Default.MTB_cut_time_C = cut_time_C;

                form.MTB_cut_time_A.Text = cut_time_A.ToString();
                form.MTB_cut_time_B.Text = cut_time_B.ToString();
                form.MTB_cut_time_C.Text = cut_time_C.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 실현손익대비관리 시간입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 실현손익대비관리 시간입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_cut_수익금1_A.Text, out double cut_수익금1_A);
                double.TryParse(form.TB_cut_수익금1_B.Text, out double cut_수익금1_B);
                double.TryParse(form.TB_cut_수익금1_C.Text, out double cut_수익금1_C);

                if (cut_수익금1_A == 0) cut_수익금1_A = 10;
                if (cut_수익금1_B == 0) cut_수익금1_B = 20;
                if (cut_수익금1_C == 0) cut_수익금1_C = 30;

                Properties.Settings.Default.TB_cut_수익금1_A = Math.Abs(cut_수익금1_A);
                Properties.Settings.Default.TB_cut_수익금1_B = Math.Abs(cut_수익금1_B);
                Properties.Settings.Default.TB_cut_수익금1_C = Math.Abs(cut_수익금1_C);

                form.TB_cut_수익금1_A.Text = Properties.Settings.Default.TB_cut_수익금1_A.ToString();
                form.TB_cut_수익금1_B.Text = Properties.Settings.Default.TB_cut_수익금1_B.ToString();
                form.TB_cut_수익금1_C.Text = Properties.Settings.Default.TB_cut_수익금1_C.ToString();

                double.TryParse(form.TB_cut_수익금2_A.Text, out double cut_수익금2_A);
                double.TryParse(form.TB_cut_수익금2_B.Text, out double cut_수익금2_B);
                double.TryParse(form.TB_cut_수익금2_C.Text, out double cut_수익금2_C);

                if (cut_수익금2_A == 0) cut_수익금2_A = 20;
                if (cut_수익금2_B == 0) cut_수익금2_B = 30;
                if (cut_수익금2_C == 0) cut_수익금2_C = 100;

                Properties.Settings.Default.TB_cut_수익금2_A = Math.Abs(cut_수익금2_A);
                Properties.Settings.Default.TB_cut_수익금2_B = Math.Abs(cut_수익금2_B);
                Properties.Settings.Default.TB_cut_수익금2_C = Math.Abs(cut_수익금2_C);

                form.TB_cut_수익금2_A.Text = Properties.Settings.Default.TB_cut_수익금2_A.ToString();
                form.TB_cut_수익금2_B.Text = Properties.Settings.Default.TB_cut_수익금2_B.ToString();
                form.TB_cut_수익금2_C.Text = Properties.Settings.Default.TB_cut_수익금2_C.ToString();

                double.TryParse(form.TB_cut_남길퍼_A.Text, out double cut_남길퍼_A);
                double.TryParse(form.TB_cut_남길퍼_B.Text, out double cut_남길퍼_B);
                double.TryParse(form.TB_cut_남길퍼_C.Text, out double cut_남길퍼_C);

                if (cut_남길퍼_A == 0) cut_남길퍼_A = 0;
                if (cut_남길퍼_B == 0) cut_남길퍼_B = 0;
                if (cut_남길퍼_C == 0) cut_남길퍼_C = 0;

                Properties.Settings.Default.TB_cut_남길퍼_A = Math.Abs(cut_남길퍼_A);
                Properties.Settings.Default.TB_cut_남길퍼_B = Math.Abs(cut_남길퍼_B);
                Properties.Settings.Default.TB_cut_남길퍼_C = Math.Abs(cut_남길퍼_C);

                form.TB_cut_남길퍼_A.Text = Properties.Settings.Default.TB_cut_남길퍼_A.ToString();
                form.TB_cut_남길퍼_B.Text = Properties.Settings.Default.TB_cut_남길퍼_B.ToString();
                form.TB_cut_남길퍼_C.Text = Properties.Settings.Default.TB_cut_남길퍼_C.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 실현손익대비관리 수익금 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 실현손익대비관리 수익금 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_cut_P_A.Text, out double cut_P_A);
                double.TryParse(form.TB_cut_P_B.Text, out double cut_P_B);
                double.TryParse(form.TB_cut_P_C.Text, out double cut_P_C);

                if (cut_P_A == 0) cut_P_A = -30;
                if (cut_P_B == 0) cut_P_B = -30;
                if (cut_P_C == 0) cut_P_C = -30;

                Properties.Settings.Default.TB_cut_P_A = cut_P_A;
                Properties.Settings.Default.TB_cut_P_B = cut_P_B;
                Properties.Settings.Default.TB_cut_P_C = cut_P_C;

                form.TB_cut_P_A.Text = cut_P_A.ToString();
                form.TB_cut_P_B.Text = cut_P_B.ToString();
                form.TB_cut_P_C.Text = cut_P_C.ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 수익률 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 수익률 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_cut_won_A.Text, out int won_A);
                int.TryParse(form.TB_cut_won_B.Text, out int won_B);
                int.TryParse(form.TB_cut_won_C.Text, out int won_C);

                Properties.Settings.Default.TB_cut_won_A = Math.Abs(won_A);
                Properties.Settings.Default.TB_cut_won_B = Math.Abs(won_B);
                Properties.Settings.Default.TB_cut_won_C = Math.Abs(won_C);

                form.TB_cut_won_A.Text = Properties.Settings.Default.TB_cut_won_A.ToString();
                form.TB_cut_won_B.Text = Properties.Settings.Default.TB_cut_won_B.ToString();
                form.TB_cut_won_C.Text = Properties.Settings.Default.TB_cut_won_C.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 매입금 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 매입금 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_cut_ratio_A.Text, out double ratio_A);
                double.TryParse(form.TB_cut_ratio_B.Text, out double ratio_B);
                double.TryParse(form.TB_cut_ratio_C.Text, out double ratio_C);

                if (ratio_A == 0) ratio_A = 10;
                if (ratio_B == 0) ratio_B = 10;
                if (ratio_C == 0) ratio_C = 10;

                Properties.Settings.Default.TB_cut_ratio_A = Math.Abs(ratio_A);
                Properties.Settings.Default.TB_cut_ratio_B = Math.Abs(ratio_B);
                Properties.Settings.Default.TB_cut_ratio_C = Math.Abs(ratio_C);

                form.TB_cut_ratio_A.Text = Properties.Settings.Default.TB_cut_ratio_A.ToString();
                form.TB_cut_ratio_B.Text = Properties.Settings.Default.TB_cut_ratio_B.ToString();
                form.TB_cut_ratio_C.Text = Properties.Settings.Default.TB_cut_ratio_C.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 매도비율 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 매도비율 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_cut_value_A.Text, out double cut_value_A);
                double.TryParse(form.TB_cut_value_B.Text, out double cut_value_B);
                double.TryParse(form.TB_cut_value_C.Text, out double cut_value_C);

                if (form.CBB_cut_jumun_A.SelectedIndex == 0 || form.CBB_cut_jumun_A.SelectedIndex == 1) cut_value_A = 0;
                if (form.CBB_cut_jumun_B.SelectedIndex == 0 || form.CBB_cut_jumun_B.SelectedIndex == 1) cut_value_B = 0;
                if (form.CBB_cut_jumun_C.SelectedIndex == 0 || form.CBB_cut_jumun_C.SelectedIndex == 1) cut_value_C = 0;

                Properties.Settings.Default.TB_cut_value_A = cut_value_A;
                Properties.Settings.Default.TB_cut_value_B = cut_value_B;
                Properties.Settings.Default.TB_cut_value_C = cut_value_C;

                form.TB_cut_value_A.Text = cut_value_A.ToString();
                form.TB_cut_value_B.Text = cut_value_B.ToString();
                form.TB_cut_value_C.Text = cut_value_C.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 매도비율 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 매도비율 입력 오류 : " + e.Message);
            }


            try
            {
                int.TryParse(form.MTB_cut_cansel_time_A.Text, out int time_A);
                int.TryParse(form.MTB_cut_cansel_time_B.Text, out int time_B);
                int.TryParse(form.MTB_cut_cansel_time_C.Text, out int time_C);

                if (time_A == 0) time_A = 30;
                if (time_B == 0) time_B = 30;
                if (time_C == 0) time_C = 30;

                Properties.Settings.Default.MTB_cut_cansel_time_A = time_A;
                Properties.Settings.Default.MTB_cut_cansel_time_B = time_B;
                Properties.Settings.Default.MTB_cut_cansel_time_C = time_C;

                form.MTB_cut_cansel_time_A.Text = time_A.ToString();
                form.MTB_cut_cansel_time_B.Text = time_B.ToString();
                form.MTB_cut_cansel_time_C.Text = time_C.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 취소시간 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 취소시간 입력 오류 : " + e.Message);
            }

            Properties.Settings.Default.CB_rebalance_매도체크_A = form.CB_rebalance_매도체크_A.Checked;
            Properties.Settings.Default.CB_rebalance_매도체크_B = form.CB_rebalance_매도체크_B.Checked;
            Properties.Settings.Default.CB_rebalance_매도체크_C = form.CB_rebalance_매도체크_C.Checked;
            Properties.Settings.Default.CB_rebalance_매도체크_D = form.CB_rebalance_매도체크_D.Checked;
            Properties.Settings.Default.CB_rebalance_매도체크_E = form.CB_rebalance_매도체크_E.Checked;
            Properties.Settings.Default.CB_rebalance_매도체크_F = form.CB_rebalance_매도체크_F.Checked;
            Properties.Settings.Default.CB_rebalance_매도체크_G = form.CB_rebalance_매도체크_G.Checked;

            if (Form1.form1.account_comboBox.SelectedIndex > -1)
            {
                if (form.combo_rebalance_use_condition_A.SelectedIndex == 0) form.combo_rebalance_condition_A.SelectedItem = "";
                if (form.combo_rebalance_use_condition_B.SelectedIndex == 0) form.combo_rebalance_condition_B.SelectedItem = "";
                if (form.combo_rebalance_use_condition_C.SelectedIndex == 0) form.combo_rebalance_condition_C.SelectedItem = "";
                if (form.combo_rebalance_use_condition_D.SelectedIndex == 0) form.combo_rebalance_condition_D.SelectedItem = "";
                if (form.combo_rebalance_use_condition_E.SelectedIndex == 0) form.combo_rebalance_condition_E.SelectedItem = "";
                if (form.combo_rebalance_use_condition_F.SelectedIndex == 0) form.combo_rebalance_condition_F.SelectedItem = "";
                if (form.combo_rebalance_use_condition_G.SelectedIndex == 0) form.combo_rebalance_condition_G.SelectedItem = "";

                Properties.Settings.Default.combo_rebalance_use_condition_A = form.combo_rebalance_use_condition_A.SelectedIndex;
                Properties.Settings.Default.combo_rebalance_use_condition_B = form.combo_rebalance_use_condition_B.SelectedIndex;
                Properties.Settings.Default.combo_rebalance_use_condition_C = form.combo_rebalance_use_condition_C.SelectedIndex;
                Properties.Settings.Default.combo_rebalance_use_condition_D = form.combo_rebalance_use_condition_D.SelectedIndex;
                Properties.Settings.Default.combo_rebalance_use_condition_E = form.combo_rebalance_use_condition_E.SelectedIndex;
                Properties.Settings.Default.combo_rebalance_use_condition_F = form.combo_rebalance_use_condition_F.SelectedIndex;
                Properties.Settings.Default.combo_rebalance_use_condition_G = form.combo_rebalance_use_condition_G.SelectedIndex;

                if (Properties.Settings.Default.combo_rebalance_use_condition_A == 0) Form1.Rebal_condition_List_A.Clear();
                if (Properties.Settings.Default.combo_rebalance_use_condition_B == 0) Form1.Rebal_condition_List_B.Clear();
                if (Properties.Settings.Default.combo_rebalance_use_condition_C == 0) Form1.Rebal_condition_List_C.Clear();
                if (Properties.Settings.Default.combo_rebalance_use_condition_D == 0) Form1.Rebal_condition_List_D.Clear();
                if (Properties.Settings.Default.combo_rebalance_use_condition_E == 0) Form1.Rebal_condition_List_E.Clear();
                if (Properties.Settings.Default.combo_rebalance_use_condition_F == 0) Form1.Rebal_condition_List_F.Clear();
                if (Properties.Settings.Default.combo_rebalance_use_condition_G == 0) Form1.Rebal_condition_List_G.Clear();

                Properties.Settings.Default.CB_rebalance_A = form.CB_rebalance_A.Checked;
                Properties.Settings.Default.CB_rebalance_B = form.CB_rebalance_B.Checked;
                Properties.Settings.Default.CB_rebalance_C = form.CB_rebalance_C.Checked;
                Properties.Settings.Default.CB_rebalance_D = form.CB_rebalance_D.Checked;
                Properties.Settings.Default.CB_rebalance_E = form.CB_rebalance_E.Checked;
                Properties.Settings.Default.CB_rebalance_F = form.CB_rebalance_F.Checked;
                Properties.Settings.Default.CB_rebalance_G = form.CB_rebalance_G.Checked;

                if (form.CBB_Liquidation_use_condition_A.SelectedIndex == 0) form.CBB_Liquidation_condition_A.SelectedItem = "";
                if (form.CBB_Liquidation_use_condition_B.SelectedIndex == 0) form.CBB_Liquidation_condition_B.SelectedItem = "";
                if (form.CBB_Liquidation_use_condition_C.SelectedIndex == 0) form.CBB_Liquidation_condition_C.SelectedItem = "";

                Properties.Settings.Default.CBB_Liquidation_use_condition_A = form.CBB_Liquidation_use_condition_A.SelectedIndex;
                Properties.Settings.Default.CBB_Liquidation_use_condition_B = form.CBB_Liquidation_use_condition_B.SelectedIndex;
                Properties.Settings.Default.CBB_Liquidation_use_condition_C = form.CBB_Liquidation_use_condition_C.SelectedIndex;

                if (Properties.Settings.Default.CBB_Liquidation_use_condition_A == 0) Form1.Liquidation_condition_List_A.Clear();
                if (Properties.Settings.Default.CBB_Liquidation_use_condition_B == 0) Form1.Liquidation_condition_List_B.Clear();
                if (Properties.Settings.Default.CBB_Liquidation_use_condition_C == 0) Form1.Liquidation_condition_List_C.Clear();

                Properties.Settings.Default.CB_Liquidation_A = form.CB_Liquidation_A.Checked;
                Properties.Settings.Default.CB_Liquidation_B = form.CB_Liquidation_B.Checked;
                Properties.Settings.Default.CB_Liquidation_C = form.CB_Liquidation_C.Checked;
            }

            Properties.Settings.Default.combo_rebalance_suik_gubun_A = form.combo_rebalance_suik_gubun_A.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_suik_gubun_B = form.combo_rebalance_suik_gubun_B.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_suik_gubun_C = form.combo_rebalance_suik_gubun_C.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_suik_gubun_D = form.combo_rebalance_suik_gubun_D.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_suik_gubun_E = form.combo_rebalance_suik_gubun_E.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_suik_gubun_F = form.combo_rebalance_suik_gubun_F.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_suik_gubun_G = form.combo_rebalance_suik_gubun_G.SelectedIndex;

            Properties.Settings.Default.combo_rebalance_sell_gubun_A = form.combo_rebalance_sell_gubun_A.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_sell_gubun_B = form.combo_rebalance_sell_gubun_B.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_sell_gubun_C = form.combo_rebalance_sell_gubun_C.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_sell_gubun_D = form.combo_rebalance_sell_gubun_D.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_sell_gubun_E = form.combo_rebalance_sell_gubun_E.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_sell_gubun_F = form.combo_rebalance_sell_gubun_F.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_sell_gubun_G = form.combo_rebalance_sell_gubun_G.SelectedIndex;

            Properties.Settings.Default.combo_rebalance_maemae_gubun_A = form.combo_rebalance_maemae_gubun_A.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_maemae_gubun_B = form.combo_rebalance_maemae_gubun_B.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_maemae_gubun_C = form.combo_rebalance_maemae_gubun_C.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_maemae_gubun_D = form.combo_rebalance_maemae_gubun_D.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_maemae_gubun_E = form.combo_rebalance_maemae_gubun_E.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_maemae_gubun_F = form.combo_rebalance_maemae_gubun_F.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_maemae_gubun_G = form.combo_rebalance_maemae_gubun_G.SelectedIndex;

            Properties.Settings.Default.combo_rebalance_jumun_A = form.combo_rebalance_jumun_A.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_jumun_B = form.combo_rebalance_jumun_B.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_jumun_C = form.combo_rebalance_jumun_C.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_jumun_D = form.combo_rebalance_jumun_D.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_jumun_E = form.combo_rebalance_jumun_E.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_jumun_F = form.combo_rebalance_jumun_F.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_jumun_G = form.combo_rebalance_jumun_G.SelectedIndex;

            Properties.Settings.Default.CBB_cut_gubun_A = form.CBB_cut_gubun_A.SelectedIndex;
            Properties.Settings.Default.CBB_cut_gubun_B = form.CBB_cut_gubun_B.SelectedIndex;
            Properties.Settings.Default.CBB_cut_gubun_C = form.CBB_cut_gubun_C.SelectedIndex;

            Properties.Settings.Default.CBB_cut_jumun_A = form.CBB_cut_jumun_A.SelectedIndex;
            Properties.Settings.Default.CBB_cut_jumun_B = form.CBB_cut_jumun_B.SelectedIndex;
            Properties.Settings.Default.CBB_cut_jumun_C = form.CBB_cut_jumun_C.SelectedIndex;

            Properties.Settings.Default.combo_rebalance_감시_jumun_A = form.combo_rebalance_감시_jumun_A.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_감시_jumun_B = form.combo_rebalance_감시_jumun_B.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_감시_jumun_C = form.combo_rebalance_감시_jumun_C.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_감시_jumun_D = form.combo_rebalance_감시_jumun_D.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_감시_jumun_E = form.combo_rebalance_감시_jumun_E.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_감시_jumun_F = form.combo_rebalance_감시_jumun_F.SelectedIndex;
            Properties.Settings.Default.combo_rebalance_감시_jumun_G = form.combo_rebalance_감시_jumun_G.SelectedIndex;

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

                Properties.Settings.Default.TB_rebalance_감시_value_A = 감시_value_A;
                Properties.Settings.Default.TB_rebalance_감시_value_B = 감시_value_B;
                Properties.Settings.Default.TB_rebalance_감시_value_C = 감시_value_C;
                Properties.Settings.Default.TB_rebalance_감시_value_D = 감시_value_D;
                Properties.Settings.Default.TB_rebalance_감시_value_E = 감시_value_E;
                Properties.Settings.Default.TB_rebalance_감시_value_F = 감시_value_F;
                Properties.Settings.Default.TB_rebalance_감시_value_G = 감시_value_G;

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
                Console.WriteLine("계좌관리_저장 / 리벨 감시 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 리벨 감시 입력 오류 : " + e.Message);
            }

            Properties.Settings.Default.CBB_Liquidation_suik_gubun_A = form.CBB_Liquidation_suik_gubun_A.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_suik_gubun_B = form.CBB_Liquidation_suik_gubun_B.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_suik_gubun_C = form.CBB_Liquidation_suik_gubun_C.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_sell_gubun_A = form.CBB_Liquidation_sell_gubun_A.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_sell_gubun_B = form.CBB_Liquidation_sell_gubun_B.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_sell_gubun_C = form.CBB_Liquidation_sell_gubun_C.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_jumun_A = form.CBB_Liquidation_jumun_A.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_jumun_B = form.CBB_Liquidation_jumun_B.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_jumun_C = form.CBB_Liquidation_jumun_C.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_Cancel_A = form.CBB_Liquidation_Cancel_A.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_Cancel_B = form.CBB_Liquidation_Cancel_B.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_Cancel_C = form.CBB_Liquidation_Cancel_C.SelectedIndex;

            try
            {
                double.TryParse(form.TB_손익비율.Text, out double 손익비율);
                double.TryParse(form.TB_매수비율.Text, out double 매수비율);

                if (손익비율 == 0) 손익비율 = 10;
                if (매수비율 == 0) 매수비율 = 10;

                Properties.Settings.Default.TB_손익비율 = Math.Abs(손익비율);
                Properties.Settings.Default.TB_매수비율 = Math.Abs(매수비율);

                form.TB_손익비율.Text = Properties.Settings.Default.TB_손익비율.ToString();
                form.TB_매수비율.Text = Properties.Settings.Default.TB_매수비율.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 기준금 비율 계산 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 기준금 비율 계산 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_분할간격_A.Text, out int 분할간격_A);
                int.TryParse(form.TB_분할간격_B.Text, out int 분할간격_B);
                int.TryParse(form.TB_분할간격_C.Text, out int 분할간격_C);

                if (분할간격_A == 0) 분할간격_A = 1;
                if (분할간격_B == 0) 분할간격_B = 1;
                if (분할간격_C == 0) 분할간격_C = 1;

                Properties.Settings.Default.TB_분할간격_A = 분할간격_A;
                Properties.Settings.Default.TB_분할간격_B = 분할간격_B;
                Properties.Settings.Default.TB_분할간격_C = 분할간격_C;

                form.TB_분할간격_A.Text = 분할간격_A.ToString();
                form.TB_분할간격_B.Text = 분할간격_B.ToString();
                form.TB_분할간격_C.Text = 분할간격_C.ToString();

                int.TryParse(form.TB_분할횟수_A.Text, out int 분할횟수_A);
                int.TryParse(form.TB_분할횟수_B.Text, out int 분할횟수_B);
                int.TryParse(form.TB_분할횟수_C.Text, out int 분할횟수_C);

                if (분할횟수_A == 0) 분할횟수_A = 1;
                if (분할횟수_B == 0) 분할횟수_B = 1;
                if (분할횟수_C == 0) 분할횟수_C = 1;

                Properties.Settings.Default.TB_분할횟수_A = Math.Abs(분할횟수_A);
                Properties.Settings.Default.TB_분할횟수_B = Math.Abs(분할횟수_B);
                Properties.Settings.Default.TB_분할횟수_C = Math.Abs(분할횟수_C);

                form.TB_분할횟수_A.Text = Properties.Settings.Default.TB_분할횟수_A.ToString();
                form.TB_분할횟수_B.Text = Properties.Settings.Default.TB_분할횟수_B.ToString();
                form.TB_분할횟수_C.Text = Properties.Settings.Default.TB_분할횟수_C.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 분할주문 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 분할주문 입력 오류 : " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_일매수제한금.Text.Replace(",", ""), out double 일매수제한금);
                double.TryParse(form.TB_총매수금.Text.Replace(",", ""), out double 총매수금);
                int.TryParse(form.TB_회수제한.Text, out int 회수제한);

                if (일매수제한금 == 0) 일매수제한금 = 10;
                if (총매수금 == 0) 총매수금 = 100;
                if (회수제한 == 0) 회수제한 = 5;

                Properties.Settings.Default.TB_일매수제한금 = 일매수제한금;
                Properties.Settings.Default.TB_총매수금 = 총매수금;
                Properties.Settings.Default.TB_회수제한 = 회수제한;

                form.TB_일매수제한금.Text = Properties.Settings.Default.TB_일매수제한금.ToString();
                form.TB_총매수금.Text = Properties.Settings.Default.TB_총매수금.ToString();
                form.TB_회수제한.Text = Properties.Settings.Default.TB_회수제한.ToString();
            }
            catch
            {
                Form1.AutoClosingAlram("일매수제한금 저장 에러", "에러알림", 10, "동작");
                Form1.Error_Log("일매수제한금 저장 에러");
            }

            try
            {
                int.TryParse(form.TB_추매주가이상.Text.Replace(",", ""), out int 추매주가이상);
                int.TryParse(form.TB_추매주가이하.Text.Replace(",", ""), out int 추매주가이하);

                if (추매주가이하 == 0) 추매주가이하 = 1000000;
                Properties.Settings.Default.TB_추매주가이상 = 추매주가이상;
                Properties.Settings.Default.TB_추매주가이하 = 추매주가이하;

                double.TryParse(form.TB_추매등락률이상.Text, out double 추매등락률이상);
                double.TryParse(form.TB_추매등락률이하.Text, out double 추매등락률이하);

                Properties.Settings.Default.TB_추매등락률이상 = 추매등락률이상;
                Properties.Settings.Default.TB_추매등락률이하 = 추매등락률이하;

                form.TB_추매주가이상.Text = Properties.Settings.Default.TB_추매주가이상.ToString();
                form.TB_추매주가이하.Text = Properties.Settings.Default.TB_추매주가이하.ToString();
                form.TB_추매등락률이상.Text = Properties.Settings.Default.TB_추매등락률이상.ToString();
                form.TB_추매등락률이하.Text = Properties.Settings.Default.TB_추매등락률이하.ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 추매주가제한 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 추매주가제한 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_Liquidation_mma_A.Text, out int TB_Liquidation_mma_A);
                int.TryParse(form.TB_Liquidation_mma_B.Text, out int TB_Liquidation_mma_B);
                int.TryParse(form.TB_Liquidation_mma_C.Text, out int TB_Liquidation_mma_C);

                if (TB_Liquidation_mma_A == 0) TB_Liquidation_mma_A = 3;
                if (TB_Liquidation_mma_B == 0) TB_Liquidation_mma_B = 3;
                if (TB_Liquidation_mma_C == 0) TB_Liquidation_mma_C = 3;

                if (TB_Liquidation_mma_A > 60) TB_Liquidation_mma_A = 60;
                if (TB_Liquidation_mma_B > 60) TB_Liquidation_mma_B = 60;
                if (TB_Liquidation_mma_C > 60) TB_Liquidation_mma_C = 60;

                Properties.Settings.Default.TB_Liquidation_mma_A = TB_Liquidation_mma_A;
                Properties.Settings.Default.TB_Liquidation_mma_B = TB_Liquidation_mma_B;
                Properties.Settings.Default.TB_Liquidation_mma_C = TB_Liquidation_mma_C;

                form.TB_Liquidation_mma_A.Text = TB_Liquidation_mma_A.ToString();
                form.TB_Liquidation_mma_B.Text = TB_Liquidation_mma_B.ToString();
                form.TB_Liquidation_mma_C.Text = TB_Liquidation_mma_C.ToString();

                Properties.Settings.Default.CBB_Liquidation_mma_A = form.CBB_Liquidation_mma_A.SelectedIndex;
                Properties.Settings.Default.CBB_Liquidation_mma_B = form.CBB_Liquidation_mma_B.SelectedIndex;
                Properties.Settings.Default.CBB_Liquidation_mma_C = form.CBB_Liquidation_mma_C.SelectedIndex;
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 이평 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 이평 입력 오류 : " + e.Message);
            }



            Properties.Settings.Default.CB_매수기준 = form.CB_매수기준.Checked;
            Properties.Settings.Default.CB_손익기준 = form.CB_손익기준.Checked;

            Properties.Settings.Default.CB_rebalance_기준금 = form.CB_rebalance_기준금.Checked;

            Properties.Settings.Default.CBB_rebalance_1_A = form.CBB_rebalance_1A.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_1_B = form.CBB_rebalance_1B.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_1_C = form.CBB_rebalance_1C.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_1_D = form.CBB_rebalance_1D.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_1_E = form.CBB_rebalance_1E.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_1_F = form.CBB_rebalance_1F.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_1_G = form.CBB_rebalance_1G.SelectedIndex;

            Properties.Settings.Default.CBB_rebalance_2_A = form.CBB_rebalance_2A.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_2_B = form.CBB_rebalance_2B.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_2_C = form.CBB_rebalance_2C.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_2_D = form.CBB_rebalance_2D.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_2_E = form.CBB_rebalance_2E.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_2_F = form.CBB_rebalance_2F.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_2_G = form.CBB_rebalance_2G.SelectedIndex;

            Properties.Settings.Default.리밸매도기준1_A = form.CBB_rebalance_1A.Text;
            Properties.Settings.Default.리밸매도기준1_B = form.CBB_rebalance_1B.Text;
            Properties.Settings.Default.리밸매도기준1_C = form.CBB_rebalance_1C.Text;
            Properties.Settings.Default.리밸매도기준1_D = form.CBB_rebalance_1D.Text;
            Properties.Settings.Default.리밸매도기준1_E = form.CBB_rebalance_1E.Text;
            Properties.Settings.Default.리밸매도기준1_F = form.CBB_rebalance_1F.Text;
            Properties.Settings.Default.리밸매도기준1_G = form.CBB_rebalance_1G.Text;

            Properties.Settings.Default.리밸매도기준2_A = form.CBB_rebalance_2A.Text;
            Properties.Settings.Default.리밸매도기준2_B = form.CBB_rebalance_2B.Text;
            Properties.Settings.Default.리밸매도기준2_C = form.CBB_rebalance_2C.Text;
            Properties.Settings.Default.리밸매도기준2_D = form.CBB_rebalance_2D.Text;
            Properties.Settings.Default.리밸매도기준2_E = form.CBB_rebalance_2E.Text;
            Properties.Settings.Default.리밸매도기준2_F = form.CBB_rebalance_2F.Text;
            Properties.Settings.Default.리밸매도기준2_G = form.CBB_rebalance_2G.Text;

            Properties.Settings.Default.CB_rebalance_choice_A = form.CB_rebalance_choice_A.Checked;
            Properties.Settings.Default.CB_rebalance_choice_B = form.CB_rebalance_choice_B.Checked;
            Properties.Settings.Default.CB_rebalance_choice_C = form.CB_rebalance_choice_C.Checked;
            Properties.Settings.Default.CB_rebalance_choice_D = form.CB_rebalance_choice_D.Checked;
            Properties.Settings.Default.CB_rebalance_choice_E = form.CB_rebalance_choice_E.Checked;
            Properties.Settings.Default.CB_rebalance_choice_F = form.CB_rebalance_choice_F.Checked;
            Properties.Settings.Default.CB_rebalance_choice_G = form.CB_rebalance_choice_G.Checked;

            Properties.Settings.Default.CB_cut_A = form.CB_cut_A.Checked;
            Properties.Settings.Default.CB_cut_B = form.CB_cut_B.Checked;
            Properties.Settings.Default.CB_cut_C = form.CB_cut_C.Checked;
            Properties.Settings.Default.CB_cut_기준금 = form.CB_cut_기준금.Checked;

            Properties.Settings.Default.CB_rebalance_option_A = form.CB_rebalance_option_A.Checked;
            Properties.Settings.Default.CB_rebalance_option_B = form.CB_rebalance_option_B.Checked;
            Properties.Settings.Default.CB_rebalance_option_C = form.CB_rebalance_option_C.Checked;
            Properties.Settings.Default.CB_rebalance_option_D = form.CB_rebalance_option_D.Checked;
            Properties.Settings.Default.CB_rebalance_option_E = form.CB_rebalance_option_E.Checked;
            Properties.Settings.Default.CB_rebalance_option_F = form.CB_rebalance_option_F.Checked;
            Properties.Settings.Default.CB_rebalance_option_G = form.CB_rebalance_option_G.Checked;

            Properties.Settings.Default.CB_Liquidation_SellStop_A = form.CB_Liquidation_SellStop_A.Checked;
            Properties.Settings.Default.CB_Liquidation_SellStop_B = form.CB_Liquidation_SellStop_B.Checked;
            Properties.Settings.Default.CB_Liquidation_SellStop_C = form.CB_Liquidation_SellStop_C.Checked;

            Properties.Settings.Default.CB_Liquidation_강제매도_A = form.CB_Liquidation_강제매도_A.Checked;
            Properties.Settings.Default.CB_Liquidation_강제매도_B = form.CB_Liquidation_강제매도_B.Checked;
            Properties.Settings.Default.CB_Liquidation_강제매도_C = form.CB_Liquidation_강제매도_C.Checked;

            Properties.Settings.Default.CB_추매금지_Liquidation_A = form.CB_추매금지_Liquidation_A.Checked;
            Properties.Settings.Default.CB_추매금지_Liquidation_B = form.CB_추매금지_Liquidation_B.Checked;
            Properties.Settings.Default.CB_추매금지_Liquidation_C = form.CB_추매금지_Liquidation_C.Checked;

            Properties.Settings.Default.CB_수익보전_Liquidation_A = form.CB_수익보전_Liquidation_A.Checked;
            Properties.Settings.Default.CB_수익보전_Liquidation_B = form.CB_수익보전_Liquidation_B.Checked;
            Properties.Settings.Default.CB_수익보전_Liquidation_C = form.CB_수익보전_Liquidation_C.Checked;

            Properties.Settings.Default.CB_Liquidation_choice_A = form.CB_Liquidation_choice_A.Checked;
            Properties.Settings.Default.CB_Liquidation_choice_B = form.CB_Liquidation_choice_B.Checked;
            Properties.Settings.Default.CB_Liquidation_choice_C = form.CB_Liquidation_choice_C.Checked;

            Properties.Settings.Default.CB_Liquidation_기준금 = form.CB_Liquidation_기준금.Checked;

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

                Properties.Settings.Default.TB_rebalance_TS_1차_down_A = TB_rebalance_TS_1차_down_A;
                Properties.Settings.Default.TB_rebalance_TS_1차_down_B = TB_rebalance_TS_1차_down_B;
                Properties.Settings.Default.TB_rebalance_TS_1차_down_C = TB_rebalance_TS_1차_down_C;
                Properties.Settings.Default.TB_rebalance_TS_1차_down_D = TB_rebalance_TS_1차_down_D;
                Properties.Settings.Default.TB_rebalance_TS_1차_down_E = TB_rebalance_TS_1차_down_E;
                Properties.Settings.Default.TB_rebalance_TS_1차_down_F = TB_rebalance_TS_1차_down_F;
                Properties.Settings.Default.TB_rebalance_TS_1차_down_G = TB_rebalance_TS_1차_down_G;
                Properties.Settings.Default.TB_rebalance_TS_2차_down_A = TB_rebalance_TS_2차_down_A;
                Properties.Settings.Default.TB_rebalance_TS_2차_down_B = TB_rebalance_TS_2차_down_B;
                Properties.Settings.Default.TB_rebalance_TS_2차_down_C = TB_rebalance_TS_2차_down_C;
                Properties.Settings.Default.TB_rebalance_TS_2차_down_D = TB_rebalance_TS_2차_down_D;
                Properties.Settings.Default.TB_rebalance_TS_2차_down_E = TB_rebalance_TS_2차_down_E;
                Properties.Settings.Default.TB_rebalance_TS_2차_down_F = TB_rebalance_TS_2차_down_F;
                Properties.Settings.Default.TB_rebalance_TS_2차_down_G = TB_rebalance_TS_2차_down_G;
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / TS 다운값 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / TS 다운값 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_rebalance_TS_1차_mma_A.Text, out int TB_rebalance_TS_1차_mma_A);
                int.TryParse(form.TB_rebalance_TS_1차_mma_B.Text, out int TB_rebalance_TS_1차_mma_B);
                int.TryParse(form.TB_rebalance_TS_1차_mma_C.Text, out int TB_rebalance_TS_1차_mma_C);
                int.TryParse(form.TB_rebalance_TS_1차_mma_D.Text, out int TB_rebalance_TS_1차_mma_D);
                int.TryParse(form.TB_rebalance_TS_1차_mma_E.Text, out int TB_rebalance_TS_1차_mma_E);
                int.TryParse(form.TB_rebalance_TS_1차_mma_F.Text, out int TB_rebalance_TS_1차_mma_F);
                int.TryParse(form.TB_rebalance_TS_1차_mma_G.Text, out int TB_rebalance_TS_1차_mma_G);
                int.TryParse(form.TB_rebalance_TS_2차_mma_A.Text, out int TB_rebalance_TS_2차_mma_A);
                int.TryParse(form.TB_rebalance_TS_2차_mma_B.Text, out int TB_rebalance_TS_2차_mma_B);
                int.TryParse(form.TB_rebalance_TS_2차_mma_C.Text, out int TB_rebalance_TS_2차_mma_C);
                int.TryParse(form.TB_rebalance_TS_2차_mma_D.Text, out int TB_rebalance_TS_2차_mma_D);
                int.TryParse(form.TB_rebalance_TS_2차_mma_E.Text, out int TB_rebalance_TS_2차_mma_E);
                int.TryParse(form.TB_rebalance_TS_2차_mma_F.Text, out int TB_rebalance_TS_2차_mma_F);
                int.TryParse(form.TB_rebalance_TS_2차_mma_G.Text, out int TB_rebalance_TS_2차_mma_G);

                if (TB_rebalance_TS_1차_mma_A == 0) TB_rebalance_TS_1차_mma_A = 5;
                if (TB_rebalance_TS_1차_mma_B == 0) TB_rebalance_TS_1차_mma_B = 5;
                if (TB_rebalance_TS_1차_mma_C == 0) TB_rebalance_TS_1차_mma_C = 5;
                if (TB_rebalance_TS_1차_mma_D == 0) TB_rebalance_TS_1차_mma_D = 5;
                if (TB_rebalance_TS_1차_mma_E == 0) TB_rebalance_TS_1차_mma_E = 5;
                if (TB_rebalance_TS_1차_mma_F == 0) TB_rebalance_TS_1차_mma_F = 5;
                if (TB_rebalance_TS_1차_mma_G == 0) TB_rebalance_TS_1차_mma_G = 5;
                if (TB_rebalance_TS_2차_mma_A == 0) TB_rebalance_TS_2차_mma_A = 5;
                if (TB_rebalance_TS_2차_mma_B == 0) TB_rebalance_TS_2차_mma_B = 5;
                if (TB_rebalance_TS_2차_mma_C == 0) TB_rebalance_TS_2차_mma_C = 5;
                if (TB_rebalance_TS_2차_mma_D == 0) TB_rebalance_TS_2차_mma_D = 5;
                if (TB_rebalance_TS_2차_mma_E == 0) TB_rebalance_TS_2차_mma_E = 5;
                if (TB_rebalance_TS_2차_mma_F == 0) TB_rebalance_TS_2차_mma_F = 5;
                if (TB_rebalance_TS_2차_mma_G == 0) TB_rebalance_TS_2차_mma_G = 5;

                if (TB_rebalance_TS_1차_mma_A > 300) TB_rebalance_TS_1차_mma_A = 300;
                if (TB_rebalance_TS_1차_mma_B > 300) TB_rebalance_TS_1차_mma_B = 300;
                if (TB_rebalance_TS_1차_mma_C > 300) TB_rebalance_TS_1차_mma_C = 300;
                if (TB_rebalance_TS_1차_mma_D > 300) TB_rebalance_TS_1차_mma_D = 300;
                if (TB_rebalance_TS_1차_mma_E > 300) TB_rebalance_TS_1차_mma_E = 300;
                if (TB_rebalance_TS_1차_mma_F > 300) TB_rebalance_TS_1차_mma_F = 300;
                if (TB_rebalance_TS_1차_mma_G > 300) TB_rebalance_TS_1차_mma_G = 300;
                if (TB_rebalance_TS_2차_mma_A > 300) TB_rebalance_TS_2차_mma_A = 300;
                if (TB_rebalance_TS_2차_mma_B > 300) TB_rebalance_TS_2차_mma_B = 300;
                if (TB_rebalance_TS_2차_mma_C > 300) TB_rebalance_TS_2차_mma_C = 300;
                if (TB_rebalance_TS_2차_mma_D > 300) TB_rebalance_TS_2차_mma_D = 300;
                if (TB_rebalance_TS_2차_mma_E > 300) TB_rebalance_TS_2차_mma_E = 300;
                if (TB_rebalance_TS_2차_mma_F > 300) TB_rebalance_TS_2차_mma_F = 300;
                if (TB_rebalance_TS_2차_mma_G > 300) TB_rebalance_TS_2차_mma_G = 300;

                Properties.Settings.Default.TB_rebalance_TS_1차_mma_A = TB_rebalance_TS_1차_mma_A;
                Properties.Settings.Default.TB_rebalance_TS_1차_mma_B = TB_rebalance_TS_1차_mma_B;
                Properties.Settings.Default.TB_rebalance_TS_1차_mma_C = TB_rebalance_TS_1차_mma_C;
                Properties.Settings.Default.TB_rebalance_TS_1차_mma_D = TB_rebalance_TS_1차_mma_D;
                Properties.Settings.Default.TB_rebalance_TS_1차_mma_E = TB_rebalance_TS_1차_mma_E;
                Properties.Settings.Default.TB_rebalance_TS_1차_mma_F = TB_rebalance_TS_1차_mma_F;
                Properties.Settings.Default.TB_rebalance_TS_1차_mma_G = TB_rebalance_TS_1차_mma_G;
                Properties.Settings.Default.TB_rebalance_TS_2차_mma_A = TB_rebalance_TS_2차_mma_A;
                Properties.Settings.Default.TB_rebalance_TS_2차_mma_B = TB_rebalance_TS_2차_mma_B;
                Properties.Settings.Default.TB_rebalance_TS_2차_mma_C = TB_rebalance_TS_2차_mma_C;
                Properties.Settings.Default.TB_rebalance_TS_2차_mma_D = TB_rebalance_TS_2차_mma_D;
                Properties.Settings.Default.TB_rebalance_TS_2차_mma_E = TB_rebalance_TS_2차_mma_E;
                Properties.Settings.Default.TB_rebalance_TS_2차_mma_F = TB_rebalance_TS_2차_mma_F;
                Properties.Settings.Default.TB_rebalance_TS_2차_mma_G = TB_rebalance_TS_2차_mma_G;

                form.TB_rebalance_TS_1차_mma_A.Text = TB_rebalance_TS_1차_mma_A.ToString();
                form.TB_rebalance_TS_1차_mma_B.Text = TB_rebalance_TS_1차_mma_B.ToString();
                form.TB_rebalance_TS_1차_mma_C.Text = TB_rebalance_TS_1차_mma_C.ToString();
                form.TB_rebalance_TS_1차_mma_D.Text = TB_rebalance_TS_1차_mma_D.ToString();
                form.TB_rebalance_TS_1차_mma_E.Text = TB_rebalance_TS_1차_mma_E.ToString();
                form.TB_rebalance_TS_1차_mma_F.Text = TB_rebalance_TS_1차_mma_F.ToString();
                form.TB_rebalance_TS_1차_mma_G.Text = TB_rebalance_TS_1차_mma_G.ToString();
                form.TB_rebalance_TS_2차_mma_A.Text = TB_rebalance_TS_2차_mma_A.ToString();
                form.TB_rebalance_TS_2차_mma_B.Text = TB_rebalance_TS_2차_mma_B.ToString();
                form.TB_rebalance_TS_2차_mma_C.Text = TB_rebalance_TS_2차_mma_C.ToString();
                form.TB_rebalance_TS_2차_mma_D.Text = TB_rebalance_TS_2차_mma_D.ToString();
                form.TB_rebalance_TS_2차_mma_E.Text = TB_rebalance_TS_2차_mma_E.ToString();
                form.TB_rebalance_TS_2차_mma_F.Text = TB_rebalance_TS_2차_mma_F.ToString();
                form.TB_rebalance_TS_2차_mma_G.Text = TB_rebalance_TS_2차_mma_G.ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 리밸런싱 TS 분이평 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 리밸런싱 TS 분이평 입력 오류 : " + e.Message);
            }

            if (form.CBB_rebalance_1A.Text.Contains("손절")) form.CB_rebalance_TS_1차_A.Checked = false; Properties.Settings.Default.CB_rebalance_TS_1차_A = form.CB_rebalance_TS_1차_A.Checked;
            if (form.CBB_rebalance_1B.Text.Contains("손절")) form.CB_rebalance_TS_1차_B.Checked = false; Properties.Settings.Default.CB_rebalance_TS_1차_B = form.CB_rebalance_TS_1차_B.Checked;
            if (form.CBB_rebalance_1C.Text.Contains("손절")) form.CB_rebalance_TS_1차_C.Checked = false; Properties.Settings.Default.CB_rebalance_TS_1차_C = form.CB_rebalance_TS_1차_C.Checked;
            if (form.CBB_rebalance_1D.Text.Contains("손절")) form.CB_rebalance_TS_1차_D.Checked = false; Properties.Settings.Default.CB_rebalance_TS_1차_D = form.CB_rebalance_TS_1차_D.Checked;
            if (form.CBB_rebalance_1E.Text.Contains("손절")) form.CB_rebalance_TS_1차_E.Checked = false; Properties.Settings.Default.CB_rebalance_TS_1차_E = form.CB_rebalance_TS_1차_E.Checked;
            if (form.CBB_rebalance_1F.Text.Contains("손절")) form.CB_rebalance_TS_1차_F.Checked = false; Properties.Settings.Default.CB_rebalance_TS_1차_F = form.CB_rebalance_TS_1차_F.Checked;
            if (form.CBB_rebalance_1G.Text.Contains("손절")) form.CB_rebalance_TS_1차_G.Checked = false; Properties.Settings.Default.CB_rebalance_TS_1차_G = form.CB_rebalance_TS_1차_G.Checked;
            if (form.CBB_rebalance_2A.Text.Contains("손절")) form.CB_rebalance_TS_2차_A.Checked = false; Properties.Settings.Default.CB_rebalance_TS_2차_A = form.CB_rebalance_TS_2차_A.Checked;
            if (form.CBB_rebalance_2B.Text.Contains("손절")) form.CB_rebalance_TS_2차_B.Checked = false; Properties.Settings.Default.CB_rebalance_TS_2차_B = form.CB_rebalance_TS_2차_B.Checked;
            if (form.CBB_rebalance_2C.Text.Contains("손절")) form.CB_rebalance_TS_2차_C.Checked = false; Properties.Settings.Default.CB_rebalance_TS_2차_C = form.CB_rebalance_TS_2차_C.Checked;
            if (form.CBB_rebalance_2D.Text.Contains("손절")) form.CB_rebalance_TS_2차_D.Checked = false; Properties.Settings.Default.CB_rebalance_TS_2차_D = form.CB_rebalance_TS_2차_D.Checked;
            if (form.CBB_rebalance_2E.Text.Contains("손절")) form.CB_rebalance_TS_2차_E.Checked = false; Properties.Settings.Default.CB_rebalance_TS_2차_E = form.CB_rebalance_TS_2차_E.Checked;
            if (form.CBB_rebalance_2F.Text.Contains("손절")) form.CB_rebalance_TS_2차_F.Checked = false; Properties.Settings.Default.CB_rebalance_TS_2차_F = form.CB_rebalance_TS_2차_F.Checked;
            if (form.CBB_rebalance_2G.Text.Contains("손절")) form.CB_rebalance_TS_2차_G.Checked = false; Properties.Settings.Default.CB_rebalance_TS_2차_G = form.CB_rebalance_TS_2차_G.Checked;

            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_A = form.CBB_rebalance_TS_1차_mma_A.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_B = form.CBB_rebalance_TS_1차_mma_B.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_C = form.CBB_rebalance_TS_1차_mma_C.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_D = form.CBB_rebalance_TS_1차_mma_D.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_E = form.CBB_rebalance_TS_1차_mma_E.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_F = form.CBB_rebalance_TS_1차_mma_F.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_G = form.CBB_rebalance_TS_1차_mma_G.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_A = form.CBB_rebalance_TS_2차_mma_A.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_B = form.CBB_rebalance_TS_2차_mma_B.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_C = form.CBB_rebalance_TS_2차_mma_C.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_D = form.CBB_rebalance_TS_2차_mma_D.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_E = form.CBB_rebalance_TS_2차_mma_E.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_F = form.CBB_rebalance_TS_2차_mma_F.SelectedIndex;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_G = form.CBB_rebalance_TS_2차_mma_G.SelectedIndex;

            try
            {
                double.TryParse(form.TB_Liquidation_TS_down_A.Text, out double TB_Liquidation_TS_down_A);
                double.TryParse(form.TB_Liquidation_TS_down_B.Text, out double TB_Liquidation_TS_down_B);
                double.TryParse(form.TB_Liquidation_TS_down_C.Text, out double TB_Liquidation_TS_down_C);

                Properties.Settings.Default.TB_Liquidation_TS_down_A = TB_Liquidation_TS_down_A;
                Properties.Settings.Default.TB_Liquidation_TS_down_B = TB_Liquidation_TS_down_B;
                Properties.Settings.Default.TB_Liquidation_TS_down_C = TB_Liquidation_TS_down_C;
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 청산 TS 다운값 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 청산 TS 다운값 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_Liquidation_TS_mma_A.Text, out int TB_Liquidation_TS_mma_A);
                int.TryParse(form.TB_Liquidation_TS_mma_B.Text, out int TB_Liquidation_TS_mma_B);
                int.TryParse(form.TB_Liquidation_TS_mma_C.Text, out int TB_Liquidation_TS_mma_C);

                if (TB_Liquidation_TS_mma_A == 0) TB_Liquidation_TS_mma_A = 3;
                if (TB_Liquidation_TS_mma_B == 0) TB_Liquidation_TS_mma_B = 3;
                if (TB_Liquidation_TS_mma_C == 0) TB_Liquidation_TS_mma_C = 3;

                if (TB_Liquidation_TS_mma_A > 300) TB_Liquidation_TS_mma_A = 300;
                if (TB_Liquidation_TS_mma_B > 300) TB_Liquidation_TS_mma_B = 300;
                if (TB_Liquidation_TS_mma_C > 300) TB_Liquidation_TS_mma_C = 300;

                Properties.Settings.Default.TB_Liquidation_TS_mma_A = TB_Liquidation_TS_mma_A;
                Properties.Settings.Default.TB_Liquidation_TS_mma_B = TB_Liquidation_TS_mma_B;
                Properties.Settings.Default.TB_Liquidation_TS_mma_C = TB_Liquidation_TS_mma_C;

                form.TB_Liquidation_TS_mma_A.Text = TB_Liquidation_TS_mma_A.ToString();
                form.TB_Liquidation_TS_mma_B.Text = TB_Liquidation_TS_mma_B.ToString();
                form.TB_Liquidation_TS_mma_C.Text = TB_Liquidation_TS_mma_C.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 청산 TS 분이평 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 청산 TS 분이평 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_Liquidation_TS_dma_A.Text, out int TB_Liquidation_TS_dma_A);
                int.TryParse(form.TB_Liquidation_TS_dma_B.Text, out int TB_Liquidation_TS_dma_B);
                int.TryParse(form.TB_Liquidation_TS_dma_C.Text, out int TB_Liquidation_TS_dma_C);

                if (TB_Liquidation_TS_dma_A == 0) TB_Liquidation_TS_dma_A = 5;
                if (TB_Liquidation_TS_dma_B == 0) TB_Liquidation_TS_dma_B = 5;
                if (TB_Liquidation_TS_dma_C == 0) TB_Liquidation_TS_dma_C = 5;

                if (TB_Liquidation_TS_dma_A > 300) TB_Liquidation_TS_dma_A = 300;
                if (TB_Liquidation_TS_dma_B > 300) TB_Liquidation_TS_dma_B = 300;
                if (TB_Liquidation_TS_dma_C > 300) TB_Liquidation_TS_dma_C = 300;

                Properties.Settings.Default.TB_Liquidation_TS_dma_A = TB_Liquidation_TS_dma_A;
                Properties.Settings.Default.TB_Liquidation_TS_dma_B = TB_Liquidation_TS_dma_B;
                Properties.Settings.Default.TB_Liquidation_TS_dma_C = TB_Liquidation_TS_dma_C;

                form.TB_Liquidation_TS_dma_A.Text = TB_Liquidation_TS_dma_A.ToString();
                form.TB_Liquidation_TS_dma_B.Text = TB_Liquidation_TS_dma_B.ToString();
                form.TB_Liquidation_TS_dma_C.Text = TB_Liquidation_TS_dma_C.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("계좌관리_저장 / 청산 TS 일이평 입력 오류 : " + e.Message); Form1.Error_Log("계좌관리_저장 / 청산 TS 일이평 입력 오류 : " + e.Message);
            }

            Properties.Settings.Default.CB_Liquidation_TS_A = form.CB_Liquidation_TS_A.Checked;
            Properties.Settings.Default.CB_Liquidation_TS_B = form.CB_Liquidation_TS_B.Checked;
            Properties.Settings.Default.CB_Liquidation_TS_C = form.CB_Liquidation_TS_C.Checked;

            Properties.Settings.Default.CBB_Liquidation_TS_mma_A = form.CBB_Liquidation_TS_mma_A.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_TS_mma_B = form.CBB_Liquidation_TS_mma_B.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_TS_mma_C = form.CBB_Liquidation_TS_mma_C.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_TS_dma_A = form.CBB_Liquidation_TS_dma_A.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_TS_dma_B = form.CBB_Liquidation_TS_dma_B.SelectedIndex;
            Properties.Settings.Default.CBB_Liquidation_TS_dma_C = form.CBB_Liquidation_TS_dma_C.SelectedIndex;


            MA.Get_MA();
        }

        private void Form_AccountManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Properties.Settings.Default.CB_레이아웃고정_계좌관리 = CB_레이아웃고정_계좌관리.Checked;
                Properties.Settings.Default.CB_계좌관리_시작위치저장 = form.CB_계좌관리_시작위치저장.Checked;
                if (Properties.Settings.Default.CB_계좌관리_시작위치저장) Properties.Settings.Default.WindowLocation = form.Location;
                LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
                Form1.form1.CB_계좌관리.Checked = false;
                Form1.FormAccountManagement_Open = false;

                e.Cancel = true;
                Hide();
            }
        }


        private void CB_레이아웃고정_계좌관리_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CB_레이아웃고정_계좌관리 = CB_레이아웃고정_계좌관리.Checked;

            if (!CB_레이아웃고정_계좌관리.Checked) LayoutChange.CBB_layout_SelectedIndex(-1);
            else LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
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

        private void BT_감시확인_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;

            Form1.form1.CBB_layout.SelectedIndex = 0;
            Form1.form1.CB_계좌관리.Checked = true;

            Form1.form1.panel_주문.Size = new Size(820, 999);
            Form1.form1.tab_주문.SelectedIndex = 2;
            Form1.form1.tab_주문.Size = new Size(828, 1005);
            Form1.form1.tab_주문.BringToFront();
            Form1.form1.panel_주문.BringToFront();

            Form1.form1.LB_JumunList.Location = new Point(-1, 3);
            Form1.form1.LB_JumunList.Size = new Size(821, 975);

            Form1.form1.최종매입가View.Hide();
            Form1.form1.최종매입가View.SendToBack();
            Form1.form1.최종매입가View.Rows.Clear();
            Form1.form1.CBB_최종가종목.Hide();
            Form1.form1.BT_종목업.Hide();
            Form1.form1.BT_종목다운.Hide();
            Form1.form1.CBB_최종가종목.SendToBack();
            Form1.form1.BT_종목업.SendToBack();
            Form1.form1.BT_종목다운.SendToBack();

            Form1.form1.LB_JumunList.Show();
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
            this.ActiveControl = null;

            Form1.form1.CBB_layout.SelectedIndex = 0;
            Form1.form1.CB_계좌관리.Checked = true;

            Form1.form1.panel_주문.Size = new Size(820, 999);
            Form1.form1.tab_주문.SelectedIndex = 2;
            Form1.form1.tab_주문.Size = new Size(828, 1005);
            Form1.form1.tab_주문.BringToFront();
            Form1.form1.panel_주문.BringToFront();

            Form1.form1.LB_JumunList.Location = new Point(-1, 3);
            Form1.form1.LB_JumunList.Size = new Size(821, 975);

            Form1.form1.최종매입가View.Hide();
            Form1.form1.최종매입가View.SendToBack();
            Form1.form1.최종매입가View.Rows.Clear();
            Form1.form1.CBB_최종가종목.Hide();
            Form1.form1.BT_종목업.Hide();
            Form1.form1.BT_종목다운.Hide();
            Form1.form1.CBB_최종가종목.SendToBack();
            Form1.form1.BT_종목업.SendToBack();
            Form1.form1.BT_종목다운.SendToBack();


            Form1.form1.LB_JumunList.Show();
            Form1.form1.LB_JumunList.BringToFront();

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
            this.ActiveControl = null;

            Form1.form1.CBB_layout.SelectedIndex = 0;
            Form1.form1.CB_계좌관리.Checked = true;

            Form1.form1.panel_주문.Size = new Size(820, 999);
            Form1.form1.tab_주문.SelectedIndex = 2;
            Form1.form1.tab_주문.Size = new Size(828, 1005);
            Form1.form1.tab_주문.BringToFront();
            Form1.form1.panel_주문.BringToFront();

            Form1.form1.LB_JumunList.Hide();
            Form1.form1.LB_JumunList.SendToBack();
            Form1.form1.최종매입가View.Show();
            Form1.form1.최종매입가View.BringToFront();
            Form1.form1.CBB_최종가종목.Show();
            Form1.form1.BT_종목업.Show();
            Form1.form1.BT_종목다운.Show();
            Form1.form1.CBB_최종가종목.BringToFront();
            Form1.form1.BT_종목업.BringToFront();
            Form1.form1.BT_종목다운.BringToFront();
            Form1.form1.CBB_최종가종목.Items.Clear();

            Form1.form1.최종매입가View.Location = new Point(-2, -2);
            Form1.form1.최종매입가View.Size = new Size(825, 980);

            this.MaximumSize = new Size(1118, 389);
            this.MinimumSize = new Size(1118, 389);
            this.Size = new Size(1118, 389);

            Form1.form1.동작상태보기 = false;
            Form1.form1.일반주문확인 = false;

            Form1.form1.최종매입가View.Rows.Clear();
            if (Form1.stockBalanceList.ToList().Count > 0)
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
            CheckBox CB = sender as CheckBox;
            string Text = CB.Text.Substring(0, 2);

            if ((sender as CheckBox).Checked)
            {
                if (Form1.로딩완료) Form1.비프음("체크");
                CB.Text = Text + "■";

            }
            else
            {
                if (Form1.로딩완료) Form1.비프음("언체크");
                CB.Text = Text + "□";

                if (Form1.FormAccountManagement_Open)
                {
                    if (sender.Equals(CB_cut_A))
                    {
                        Form1.form1.Cut_A = false;
                        Form1.form1.cut_LB_A = "X";
                        form.CB_cut_LB_A.Text = "X";
                        Properties.Settings.Default.CB_cut_A = false;
                    }

                    if (sender.Equals(CB_cut_B))
                    {
                        Form1.form1.Cut_B = false;
                        Form1.form1.cut_LB_B = "X";
                        form.CB_cut_LB_B.Text = "X";
                        Properties.Settings.Default.CB_cut_B = false;
                    }

                    if (sender.Equals(CB_cut_C))
                    {
                        Form1.form1.Cut_C = false;
                        Form1.form1.cut_LB_C = "X";
                        form.CB_cut_LB_C.Text = "X";
                        Properties.Settings.Default.CB_cut_C = false;
                    }
                }
            }
        }

        private void CheckBox_Check_TEXT_Changed(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);
            Form1.form1.체크박스_비프(sender);

            string text = CB.Text.Substring(1);
            if (CB.Checked)
            {
                CB.Text = "■" + text;
                if (sender.Equals(CB_rebalance_기준금)) CB.ForeColor = Color.Crimson;
                if (sender.Equals(CB_Liquidation_기준금)) CB.ForeColor = Color.Red;
                if (sender.Equals(CB_cut_기준금)) CB.ForeColor = Color.Red;
            }
            else
            {
                CB.Text = "□" + text;
                if (sender.Equals(CB_rebalance_기준금)) CB.ForeColor = Color.Black;
                if (sender.Equals(CB_Liquidation_기준금)) CB.ForeColor = Color.Black;
                if (sender.Equals(CB_cut_기준금)) CB.ForeColor = Color.Black;
            }
        }

        private void CheckBox_매도체크_CheckChanged(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);
            CheckBox CB_option = CB_rebalance_option_A;
            Form1.form1.체크박스_비프(sender);

            string text = CB.Text.Substring(1);
            if (CB.Checked)
            {
                CB.Text = "■";
                if (sender.Equals(CB_rebalance_매도체크_A)) { CB_option = CB_rebalance_option_A; Checked(); }
                if (sender.Equals(CB_rebalance_매도체크_B)) { CB_option = CB_rebalance_option_B; Checked(); }
                if (sender.Equals(CB_rebalance_매도체크_C)) { CB_option = CB_rebalance_option_C; Checked(); }
                if (sender.Equals(CB_rebalance_매도체크_D)) { CB_option = CB_rebalance_option_D; Checked(); }
                if (sender.Equals(CB_rebalance_매도체크_E)) { CB_option = CB_rebalance_option_E; Checked(); }
                if (sender.Equals(CB_rebalance_매도체크_F)) { CB_option = CB_rebalance_option_F; Checked(); }
                if (sender.Equals(CB_rebalance_매도체크_G)) { CB_option = CB_rebalance_option_G; Checked(); }
            }
            else
            {
                CB.Text = "□";
                if (sender.Equals(CB_rebalance_매도체크_A)) { CB_option = CB_rebalance_option_A; Checked(); }
                if (sender.Equals(CB_rebalance_매도체크_B)) { CB_option = CB_rebalance_option_B; Checked(); }
                if (sender.Equals(CB_rebalance_매도체크_C)) { CB_option = CB_rebalance_option_C; Checked(); }
                if (sender.Equals(CB_rebalance_매도체크_D)) { CB_option = CB_rebalance_option_D; Checked(); }
                if (sender.Equals(CB_rebalance_매도체크_E)) { CB_option = CB_rebalance_option_E; Checked(); }
                if (sender.Equals(CB_rebalance_매도체크_F)) { CB_option = CB_rebalance_option_F; Checked(); }
                if (sender.Equals(CB_rebalance_매도체크_G)) { CB_option = CB_rebalance_option_G; Checked(); }
            }

            void Checked()
            {
                if (CB.Checked)
                {
                    if (CB_option.Enabled)
                    {
                        Form1.AutoClosingAlram("매도 체크 일떄는 '전량체결 후 매도' 전용 입니다.", "[ 매도옵션알림 ]", 10, "동작");
                        CB_option.Checked = false;
                        CB_option.Enabled = false;
                    }
                }
                else
                {
                    CB_option.Enabled = true;
                }
            }

        }


        private void AccountManagement_use_checked_chainge(object sender, EventArgs e)
        {
            CheckBox CB = sender as CheckBox;
            Form1.form1.체크박스_비프(sender);

            string Text = CB.Text.Substring(0, 2);
            if ((sender as CheckBox).Checked)
            {
                CB.Text = Text + "■";
            }
            else
            {
                CB.Text = Text + "□";

                if (Form1.FormAccountManagement_Open)
                {
                    foreach (var code in Form1.stockBalanceList.ToList())
                    {
                        if (sender.Equals(CB_rebalance_A)) code.Value.리벨A = "A";
                        if (sender.Equals(CB_rebalance_B)) code.Value.리벨B = "B";
                        if (sender.Equals(CB_rebalance_C)) code.Value.리벨C = "C";
                        if (sender.Equals(CB_rebalance_D)) code.Value.리벨D = "D";
                        if (sender.Equals(CB_rebalance_E)) code.Value.리벨E = "E";
                        if (sender.Equals(CB_rebalance_F)) code.Value.리벨F = "F";
                        if (sender.Equals(CB_rebalance_G)) code.Value.리벨G = "G";
                        if (sender.Equals(CB_Liquidation_A)) code.Value.청산A = "A";
                        if (sender.Equals(CB_Liquidation_B)) code.Value.청산B = "B";
                        if (sender.Equals(CB_Liquidation_C)) code.Value.청산C = "C";
                    }

                    if (sender.Equals(CB_rebalance_A)) Properties.Settings.Default.CB_rebalance_A = false;
                    if (sender.Equals(CB_rebalance_B)) Properties.Settings.Default.CB_rebalance_B = false;
                    if (sender.Equals(CB_rebalance_C)) Properties.Settings.Default.CB_rebalance_C = false;
                    if (sender.Equals(CB_rebalance_D)) Properties.Settings.Default.CB_rebalance_D = false;
                    if (sender.Equals(CB_rebalance_E)) Properties.Settings.Default.CB_rebalance_E = false;
                    if (sender.Equals(CB_rebalance_F)) Properties.Settings.Default.CB_rebalance_F = false;
                    if (sender.Equals(CB_rebalance_G)) Properties.Settings.Default.CB_rebalance_G = false;
                    if (sender.Equals(CB_Liquidation_A)) Properties.Settings.Default.CB_Liquidation_A = false;
                    if (sender.Equals(CB_Liquidation_B)) Properties.Settings.Default.CB_Liquidation_B = false;
                    if (sender.Equals(CB_Liquidation_C)) Properties.Settings.Default.CB_Liquidation_C = false;

                    Form1.시장가탐색 = Condition_Management.시장가대금탐색();
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
            CBB_rebalance_DropDownClosed_(sender);
        }

        private void CBB_rebalance_DropDownClosed_(object sender)
        {
            TextBox TB1 = TB_rebalance_sellratio1_A;
            TextBox TB2 = TB_rebalance_sellratio2_A;
            ComboBox CBB1 = CBB_rebalance_1A;
            ComboBox CBB2 = CBB_rebalance_2A;

            if (sender.Equals(CBB_rebalance_1A)) { TB1 = TB_rebalance_sellratio1_A; CBB1 = CBB_rebalance_1A; TB2 = TB_rebalance_sellratio2_A; CBB2 = CBB_rebalance_2A; 사용제한(); }
            if (sender.Equals(CBB_rebalance_1B)) { TB1 = TB_rebalance_sellratio1_B; CBB1 = CBB_rebalance_1B; TB2 = TB_rebalance_sellratio2_B; CBB2 = CBB_rebalance_2B; 사용제한(); }
            if (sender.Equals(CBB_rebalance_1C)) { TB1 = TB_rebalance_sellratio1_C; CBB1 = CBB_rebalance_1C; TB2 = TB_rebalance_sellratio2_C; CBB2 = CBB_rebalance_2C; 사용제한(); }
            if (sender.Equals(CBB_rebalance_1D)) { TB1 = TB_rebalance_sellratio1_D; CBB1 = CBB_rebalance_1D; TB2 = TB_rebalance_sellratio2_D; CBB2 = CBB_rebalance_2D; 사용제한(); }
            if (sender.Equals(CBB_rebalance_1E)) { TB1 = TB_rebalance_sellratio1_E; CBB1 = CBB_rebalance_1E; TB2 = TB_rebalance_sellratio2_E; CBB2 = CBB_rebalance_2E; 사용제한(); }
            if (sender.Equals(CBB_rebalance_1F)) { TB1 = TB_rebalance_sellratio1_F; CBB1 = CBB_rebalance_1F; TB2 = TB_rebalance_sellratio2_F; CBB2 = CBB_rebalance_2F; 사용제한(); }
            if (sender.Equals(CBB_rebalance_1G)) { TB1 = TB_rebalance_sellratio1_G; CBB1 = CBB_rebalance_1G; TB2 = TB_rebalance_sellratio2_G; CBB2 = CBB_rebalance_2G; 사용제한(); }

            if (sender.Equals(CBB_rebalance_2A)) { TB1 = TB_rebalance_sellratio2_A; CBB1 = CBB_rebalance_2A; 사용제한2(); }
            if (sender.Equals(CBB_rebalance_2B)) { TB1 = TB_rebalance_sellratio2_B; CBB1 = CBB_rebalance_2B; 사용제한2(); }
            if (sender.Equals(CBB_rebalance_2C)) { TB1 = TB_rebalance_sellratio2_C; CBB1 = CBB_rebalance_2C; 사용제한2(); }
            if (sender.Equals(CBB_rebalance_2D)) { TB1 = TB_rebalance_sellratio2_D; CBB1 = CBB_rebalance_2D; 사용제한2(); }
            if (sender.Equals(CBB_rebalance_2E)) { TB1 = TB_rebalance_sellratio2_E; CBB1 = CBB_rebalance_2E; 사용제한2(); }
            if (sender.Equals(CBB_rebalance_2F)) { TB1 = TB_rebalance_sellratio2_F; CBB1 = CBB_rebalance_2F; 사용제한2(); }
            if (sender.Equals(CBB_rebalance_2G)) { TB1 = TB_rebalance_sellratio2_G; CBB1 = CBB_rebalance_2G; 사용제한2(); }

            if (CBB1.SelectedIndex > 6)
            {
                if (!TB1.Text.StartsWith("-")) TB1.Text = "-" + TB1.Text;

            }
            else
            {
                if (TB1.Text.StartsWith("-")) TB1.Text = TB1.Text.Substring(1);
            }

            void 사용제한()
            {
                if (CBB1.SelectedIndex == 0)
                {
                    TB1.Text = "0";
                    TB2.Text = "0";
                    TB1.Enabled = false;
                    TB2.Enabled = false;
                    CBB2.SelectedIndex = 0;
                    CBB2.Enabled = false;
                }
                else
                {
                    TB1.Enabled = true;
                    TB2.Enabled = true;
                    CBB2.Enabled = true;
                }
            }

            void 사용제한2()
            {
                if (CBB1.SelectedIndex == 0)
                {
                    TB1.Text = "0";
                    TB1.Enabled = false;
                }
                else
                {
                    TB1.Enabled = true;
                }
            }
        }

        private void combo_use_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Form1.FormAccountManagement_Open) Condition_Management.combo_use_condition_SelectedIndexChanged(sender);
        }
        private void combo_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Form1.FormAccountManagement_Open) Condition_Management.combo_condition_SelectedIndexChanged(sender);
        }

        private void CBB_jumun_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrint.CBB_jumun_SelectedIndex(sender);
        }

        private void combo_condition_MouseHover(object sender, EventArgs e)
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

        private void combo_Condition_Add(object sender, EventArgs e)
        {
            Condition_Management.Condition_Add(sender);
        }

        private void combo_Condition_TextChanged(object sender, EventArgs e)
        {
            Condition_Management.Condition_TextChanged(sender);
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

        private void _양수실수_키프레스(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, false); // textbox 에 양수 , 실수 숫자만 입력 받을수 있음
        }

        private void _양수음수실수_키프레스(object sender, KeyPressEventArgs e)// 사용
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
            CheckBox CB = CB_rebalance_option_A;
            TextBox TB_1 = TB_rebalance_sellvolume1_A;

            if (sender.Equals(CBB_rebalance_2A)) { CB = CB_rebalance_option_A; TB_1 = TB_rebalance_sellvolume1_A; volume(); }
            if (sender.Equals(CBB_rebalance_2B)) { CB = CB_rebalance_option_B; TB_1 = TB_rebalance_sellvolume1_B; volume(); }
            if (sender.Equals(CBB_rebalance_2C)) { CB = CB_rebalance_option_C; TB_1 = TB_rebalance_sellvolume1_C; volume(); }
            if (sender.Equals(CBB_rebalance_2D)) { CB = CB_rebalance_option_D; TB_1 = TB_rebalance_sellvolume1_D; volume(); }
            if (sender.Equals(CBB_rebalance_2E)) { CB = CB_rebalance_option_E; TB_1 = TB_rebalance_sellvolume1_E; volume(); }
            if (sender.Equals(CBB_rebalance_2F)) { CB = CB_rebalance_option_F; TB_1 = TB_rebalance_sellvolume1_F; volume(); }
            if (sender.Equals(CBB_rebalance_2G)) { CB = CB_rebalance_option_G; TB_1 = TB_rebalance_sellvolume1_G; volume(); }

            void volume()
            {
                if ((sender as ComboBox).SelectedIndex > 0)
                {
                    int.TryParse(TB_1.Text, out int N);
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
                    if (!Properties.Settings.Default.CB_가이드매매) CB.Enabled = true;
                }
            }
        }

        private void 숫자콤마넣기_TextChanged(object sender, EventArgs e)
        {
            TextValue.숫자콤마넣기_TextChanged(sender);
        }

        private void combo_cancel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrint.combo_cancel_SelectedIndexChanged(sender);
        }

        bool 소리1 = false;
        bool 소리2 = false;

        private void CB_리밸TS_1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                Form1.비프음("체크");


                CB_rebalance_TS_1차_A.Checked = Properties.Settings.Default.CB_rebalance_TS_1차_A;
                CB_rebalance_TS_1차_B.Checked = Properties.Settings.Default.CB_rebalance_TS_1차_B;
                CB_rebalance_TS_1차_C.Checked = Properties.Settings.Default.CB_rebalance_TS_1차_C;
                CB_rebalance_TS_1차_D.Checked = Properties.Settings.Default.CB_rebalance_TS_1차_D;
                CB_rebalance_TS_1차_E.Checked = Properties.Settings.Default.CB_rebalance_TS_1차_E;
                CB_rebalance_TS_1차_F.Checked = Properties.Settings.Default.CB_rebalance_TS_1차_F;
                CB_rebalance_TS_1차_G.Checked = Properties.Settings.Default.CB_rebalance_TS_1차_G;

                TB_rebalance_TS_1차_down_A.Text = Properties.Settings.Default.TB_rebalance_TS_1차_down_A.ToString();
                TB_rebalance_TS_1차_down_B.Text = Properties.Settings.Default.TB_rebalance_TS_1차_down_B.ToString();
                TB_rebalance_TS_1차_down_C.Text = Properties.Settings.Default.TB_rebalance_TS_1차_down_C.ToString();
                TB_rebalance_TS_1차_down_D.Text = Properties.Settings.Default.TB_rebalance_TS_1차_down_D.ToString();
                TB_rebalance_TS_1차_down_E.Text = Properties.Settings.Default.TB_rebalance_TS_1차_down_E.ToString();
                TB_rebalance_TS_1차_down_F.Text = Properties.Settings.Default.TB_rebalance_TS_1차_down_F.ToString();
                TB_rebalance_TS_1차_down_G.Text = Properties.Settings.Default.TB_rebalance_TS_1차_down_G.ToString();

                TB_rebalance_TS_1차_mma_A.Text = Properties.Settings.Default.TB_rebalance_TS_1차_mma_A.ToString();
                TB_rebalance_TS_1차_mma_B.Text = Properties.Settings.Default.TB_rebalance_TS_1차_mma_B.ToString();
                TB_rebalance_TS_1차_mma_C.Text = Properties.Settings.Default.TB_rebalance_TS_1차_mma_C.ToString();
                TB_rebalance_TS_1차_mma_D.Text = Properties.Settings.Default.TB_rebalance_TS_1차_mma_D.ToString();
                TB_rebalance_TS_1차_mma_E.Text = Properties.Settings.Default.TB_rebalance_TS_1차_mma_E.ToString();
                TB_rebalance_TS_1차_mma_F.Text = Properties.Settings.Default.TB_rebalance_TS_1차_mma_F.ToString();
                TB_rebalance_TS_1차_mma_G.Text = Properties.Settings.Default.TB_rebalance_TS_1차_mma_G.ToString();

                CBB_rebalance_TS_1차_mma_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_A;
                CBB_rebalance_TS_1차_mma_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_B;
                CBB_rebalance_TS_1차_mma_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_C;
                CBB_rebalance_TS_1차_mma_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_D;
                CBB_rebalance_TS_1차_mma_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_E;
                CBB_rebalance_TS_1차_mma_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_F;
                CBB_rebalance_TS_1차_mma_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_1차_mma_G;

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

                CB_rebalance_TS_2차_A.Checked = Properties.Settings.Default.CB_rebalance_TS_2차_A;
                CB_rebalance_TS_2차_B.Checked = Properties.Settings.Default.CB_rebalance_TS_2차_B;
                CB_rebalance_TS_2차_C.Checked = Properties.Settings.Default.CB_rebalance_TS_2차_C;
                CB_rebalance_TS_2차_D.Checked = Properties.Settings.Default.CB_rebalance_TS_2차_D;
                CB_rebalance_TS_2차_E.Checked = Properties.Settings.Default.CB_rebalance_TS_2차_E;
                CB_rebalance_TS_2차_F.Checked = Properties.Settings.Default.CB_rebalance_TS_2차_F;
                CB_rebalance_TS_2차_G.Checked = Properties.Settings.Default.CB_rebalance_TS_2차_G;

                TB_rebalance_TS_2차_down_A.Text = Properties.Settings.Default.TB_rebalance_TS_2차_down_A.ToString();
                TB_rebalance_TS_2차_down_B.Text = Properties.Settings.Default.TB_rebalance_TS_2차_down_B.ToString();
                TB_rebalance_TS_2차_down_C.Text = Properties.Settings.Default.TB_rebalance_TS_2차_down_C.ToString();
                TB_rebalance_TS_2차_down_D.Text = Properties.Settings.Default.TB_rebalance_TS_2차_down_D.ToString();
                TB_rebalance_TS_2차_down_E.Text = Properties.Settings.Default.TB_rebalance_TS_2차_down_E.ToString();
                TB_rebalance_TS_2차_down_F.Text = Properties.Settings.Default.TB_rebalance_TS_2차_down_F.ToString();
                TB_rebalance_TS_2차_down_G.Text = Properties.Settings.Default.TB_rebalance_TS_2차_down_G.ToString();

                TB_rebalance_TS_2차_mma_A.Text = Properties.Settings.Default.TB_rebalance_TS_2차_mma_A.ToString();
                TB_rebalance_TS_2차_mma_B.Text = Properties.Settings.Default.TB_rebalance_TS_2차_mma_B.ToString();
                TB_rebalance_TS_2차_mma_C.Text = Properties.Settings.Default.TB_rebalance_TS_2차_mma_C.ToString();
                TB_rebalance_TS_2차_mma_D.Text = Properties.Settings.Default.TB_rebalance_TS_2차_mma_D.ToString();
                TB_rebalance_TS_2차_mma_E.Text = Properties.Settings.Default.TB_rebalance_TS_2차_mma_E.ToString();
                TB_rebalance_TS_2차_mma_F.Text = Properties.Settings.Default.TB_rebalance_TS_2차_mma_F.ToString();
                TB_rebalance_TS_2차_mma_G.Text = Properties.Settings.Default.TB_rebalance_TS_2차_mma_G.ToString();

                CBB_rebalance_TS_2차_mma_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_A;
                CBB_rebalance_TS_2차_mma_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_B;
                CBB_rebalance_TS_2차_mma_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_C;
                CBB_rebalance_TS_2차_mma_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_D;
                CBB_rebalance_TS_2차_mma_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_E;
                CBB_rebalance_TS_2차_mma_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_F;
                CBB_rebalance_TS_2차_mma_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_TS_2차_mma_G;

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
                panel_mma.BringToFront();
                panel_mma.Show();
                Form1.비프음("체크");

                TB_rebalance_mma2_A.Text = Properties.Settings.Default.TB_rebalance_mma2_A.ToString();
                TB_rebalance_mma2_B.Text = Properties.Settings.Default.TB_rebalance_mma2_B.ToString();
                TB_rebalance_mma2_C.Text = Properties.Settings.Default.TB_rebalance_mma2_C.ToString();
                TB_rebalance_mma2_D.Text = Properties.Settings.Default.TB_rebalance_mma2_D.ToString();
                TB_rebalance_mma2_E.Text = Properties.Settings.Default.TB_rebalance_mma2_E.ToString();
                TB_rebalance_mma2_F.Text = Properties.Settings.Default.TB_rebalance_mma2_F.ToString();
                TB_rebalance_mma2_G.Text = Properties.Settings.Default.TB_rebalance_mma2_G.ToString();

                CBB_rebalance_mma2_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_A;
                CBB_rebalance_mma2_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_B;
                CBB_rebalance_mma2_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_C;
                CBB_rebalance_mma2_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_D;
                CBB_rebalance_mma2_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_E;
                CBB_rebalance_mma2_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_F;
                CBB_rebalance_mma2_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma2_G;

                CBB_rebalance_mma_배열_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_A;
                CBB_rebalance_mma_배열_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_B;
                CBB_rebalance_mma_배열_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_C;
                CBB_rebalance_mma_배열_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_D;
                CBB_rebalance_mma_배열_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_E;
                CBB_rebalance_mma_배열_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_F;
                CBB_rebalance_mma_배열_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_mma_배열_G;

                TB_rebalance_dma1_A.Text = Properties.Settings.Default.TB_rebalance_dma1_A.ToString();
                TB_rebalance_dma1_B.Text = Properties.Settings.Default.TB_rebalance_dma1_B.ToString();
                TB_rebalance_dma1_C.Text = Properties.Settings.Default.TB_rebalance_dma1_C.ToString();
                TB_rebalance_dma1_D.Text = Properties.Settings.Default.TB_rebalance_dma1_D.ToString();
                TB_rebalance_dma1_E.Text = Properties.Settings.Default.TB_rebalance_dma1_E.ToString();
                TB_rebalance_dma1_F.Text = Properties.Settings.Default.TB_rebalance_dma1_F.ToString();
                TB_rebalance_dma1_G.Text = Properties.Settings.Default.TB_rebalance_dma1_G.ToString();

                CBB_rebalance_dma1_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_A;
                CBB_rebalance_dma1_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_B;
                CBB_rebalance_dma1_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_C;
                CBB_rebalance_dma1_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_D;
                CBB_rebalance_dma1_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_E;
                CBB_rebalance_dma1_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_F;
                CBB_rebalance_dma1_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma1_G;

                TB_rebalance_dma2_A.Text = Properties.Settings.Default.TB_rebalance_dma2_A.ToString();
                TB_rebalance_dma2_B.Text = Properties.Settings.Default.TB_rebalance_dma2_B.ToString();
                TB_rebalance_dma2_C.Text = Properties.Settings.Default.TB_rebalance_dma2_C.ToString();
                TB_rebalance_dma2_D.Text = Properties.Settings.Default.TB_rebalance_dma2_D.ToString();
                TB_rebalance_dma2_E.Text = Properties.Settings.Default.TB_rebalance_dma2_E.ToString();
                TB_rebalance_dma2_F.Text = Properties.Settings.Default.TB_rebalance_dma2_F.ToString();
                TB_rebalance_dma2_G.Text = Properties.Settings.Default.TB_rebalance_dma2_G.ToString();

                CBB_rebalance_dma2_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_A;
                CBB_rebalance_dma2_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_B;
                CBB_rebalance_dma2_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_C;
                CBB_rebalance_dma2_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_D;
                CBB_rebalance_dma2_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_E;
                CBB_rebalance_dma2_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_F;
                CBB_rebalance_dma2_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma2_G;

                CBB_rebalance_dma_배열_A.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_A;
                CBB_rebalance_dma_배열_B.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_B;
                CBB_rebalance_dma_배열_C.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_C;
                CBB_rebalance_dma_배열_D.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_D;
                CBB_rebalance_dma_배열_E.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_E;
                CBB_rebalance_dma_배열_F.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_F;
                CBB_rebalance_dma_배열_G.SelectedIndex = Properties.Settings.Default.CBB_rebalance_dma_배열_G;
            }
            else
            {
                panel_mma.SendToBack();
                panel_mma.Hide();
                Form1.비프음("언체크");
            }
        }

        private void CBB_mma_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_rebalance_mma_A) && CBB_rebalance_mma_A.SelectedIndex == 0) { CBB_rebalance_mma2_A.SelectedIndex = 0; CBB_rebalance_mma_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_mma_B) && CBB_rebalance_mma_B.SelectedIndex == 0) { CBB_rebalance_mma2_B.SelectedIndex = 0; CBB_rebalance_mma_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_mma_C) && CBB_rebalance_mma_C.SelectedIndex == 0) { CBB_rebalance_mma2_C.SelectedIndex = 0; CBB_rebalance_mma_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_mma_D) && CBB_rebalance_mma_D.SelectedIndex == 0) { CBB_rebalance_mma2_D.SelectedIndex = 0; CBB_rebalance_mma_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_mma_E) && CBB_rebalance_mma_E.SelectedIndex == 0) { CBB_rebalance_mma2_E.SelectedIndex = 0; CBB_rebalance_mma_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_mma_F) && CBB_rebalance_mma_F.SelectedIndex == 0) { CBB_rebalance_mma2_F.SelectedIndex = 0; CBB_rebalance_mma_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_mma_G) && CBB_rebalance_mma_G.SelectedIndex == 0) { CBB_rebalance_mma2_G.SelectedIndex = 0; CBB_rebalance_mma_배열_G.SelectedIndex = 0; }
        }

        private void CBB_mma2_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_rebalance_mma2_A) && (CBB_rebalance_mma_A.SelectedIndex == 0 || CBB_rebalance_mma2_A.SelectedIndex == 0)) { CBB_rebalance_mma2_A.SelectedIndex = 0; CBB_rebalance_mma_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_mma2_B) && (CBB_rebalance_mma_B.SelectedIndex == 0 || CBB_rebalance_mma2_B.SelectedIndex == 0)) { CBB_rebalance_mma2_B.SelectedIndex = 0; CBB_rebalance_mma_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_mma2_C) && (CBB_rebalance_mma_C.SelectedIndex == 0 || CBB_rebalance_mma2_C.SelectedIndex == 0)) { CBB_rebalance_mma2_C.SelectedIndex = 0; CBB_rebalance_mma_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_mma2_D) && (CBB_rebalance_mma_D.SelectedIndex == 0 || CBB_rebalance_mma2_D.SelectedIndex == 0)) { CBB_rebalance_mma2_D.SelectedIndex = 0; CBB_rebalance_mma_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_mma2_E) && (CBB_rebalance_mma_E.SelectedIndex == 0 || CBB_rebalance_mma2_E.SelectedIndex == 0)) { CBB_rebalance_mma2_E.SelectedIndex = 0; CBB_rebalance_mma_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_mma2_F) && (CBB_rebalance_mma_F.SelectedIndex == 0 || CBB_rebalance_mma2_F.SelectedIndex == 0)) { CBB_rebalance_mma2_F.SelectedIndex = 0; CBB_rebalance_mma_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_mma2_G) && (CBB_rebalance_mma_G.SelectedIndex == 0 || CBB_rebalance_mma2_G.SelectedIndex == 0)) { CBB_rebalance_mma2_G.SelectedIndex = 0; CBB_rebalance_mma_배열_G.SelectedIndex = 0; }
        }

        private void CBB_mma_배열_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_rebalance_mma_배열_A) && (CBB_rebalance_mma_A.SelectedIndex == 0 || CBB_rebalance_mma2_A.SelectedIndex == 0)) CBB_rebalance_mma_배열_A.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_mma_배열_B) && (CBB_rebalance_mma_B.SelectedIndex == 0 || CBB_rebalance_mma2_B.SelectedIndex == 0)) CBB_rebalance_mma_배열_B.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_mma_배열_C) && (CBB_rebalance_mma_C.SelectedIndex == 0 || CBB_rebalance_mma2_C.SelectedIndex == 0)) CBB_rebalance_mma_배열_C.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_mma_배열_D) && (CBB_rebalance_mma_D.SelectedIndex == 0 || CBB_rebalance_mma2_D.SelectedIndex == 0)) CBB_rebalance_mma_배열_D.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_mma_배열_E) && (CBB_rebalance_mma_E.SelectedIndex == 0 || CBB_rebalance_mma2_E.SelectedIndex == 0)) CBB_rebalance_mma_배열_E.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_mma_배열_F) && (CBB_rebalance_mma_F.SelectedIndex == 0 || CBB_rebalance_mma2_F.SelectedIndex == 0)) CBB_rebalance_mma_배열_F.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_mma_배열_G) && (CBB_rebalance_mma_G.SelectedIndex == 0 || CBB_rebalance_mma2_G.SelectedIndex == 0)) CBB_rebalance_mma_배열_G.SelectedIndex = 0;
        }

        private void CBB_dma1_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_rebalance_dma1_A) && CBB_rebalance_dma1_A.SelectedIndex == 0) { CBB_rebalance_dma2_A.SelectedIndex = 0; CBB_rebalance_dma_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_dma1_B) && CBB_rebalance_dma1_B.SelectedIndex == 0) { CBB_rebalance_dma2_B.SelectedIndex = 0; CBB_rebalance_dma_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_dma1_C) && CBB_rebalance_dma1_C.SelectedIndex == 0) { CBB_rebalance_dma2_C.SelectedIndex = 0; CBB_rebalance_dma_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_dma1_D) && CBB_rebalance_dma1_D.SelectedIndex == 0) { CBB_rebalance_dma2_D.SelectedIndex = 0; CBB_rebalance_dma_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_dma1_E) && CBB_rebalance_dma1_E.SelectedIndex == 0) { CBB_rebalance_dma2_E.SelectedIndex = 0; CBB_rebalance_dma_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_dma1_F) && CBB_rebalance_dma1_F.SelectedIndex == 0) { CBB_rebalance_dma2_F.SelectedIndex = 0; CBB_rebalance_dma_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_dma1_G) && CBB_rebalance_dma1_G.SelectedIndex == 0) { CBB_rebalance_dma2_G.SelectedIndex = 0; CBB_rebalance_dma_배열_G.SelectedIndex = 0; }
        }
        private void CBB_dma2_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_rebalance_dma2_A) && (CBB_rebalance_dma1_A.SelectedIndex == 0 || CBB_rebalance_dma2_A.SelectedIndex == 0)) { CBB_rebalance_dma2_A.SelectedIndex = 0; CBB_rebalance_dma_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_dma2_B) && (CBB_rebalance_dma1_B.SelectedIndex == 0 || CBB_rebalance_dma2_B.SelectedIndex == 0)) { CBB_rebalance_dma2_B.SelectedIndex = 0; CBB_rebalance_dma_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_dma2_C) && (CBB_rebalance_dma1_C.SelectedIndex == 0 || CBB_rebalance_dma2_C.SelectedIndex == 0)) { CBB_rebalance_dma2_C.SelectedIndex = 0; CBB_rebalance_dma_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_dma2_D) && (CBB_rebalance_dma1_D.SelectedIndex == 0 || CBB_rebalance_dma2_D.SelectedIndex == 0)) { CBB_rebalance_dma2_D.SelectedIndex = 0; CBB_rebalance_dma_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_dma2_E) && (CBB_rebalance_dma1_E.SelectedIndex == 0 || CBB_rebalance_dma2_E.SelectedIndex == 0)) { CBB_rebalance_dma2_E.SelectedIndex = 0; CBB_rebalance_dma_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_dma2_F) && (CBB_rebalance_dma1_F.SelectedIndex == 0 || CBB_rebalance_dma2_F.SelectedIndex == 0)) { CBB_rebalance_dma2_F.SelectedIndex = 0; CBB_rebalance_dma_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_rebalance_dma2_G) && (CBB_rebalance_dma1_G.SelectedIndex == 0 || CBB_rebalance_dma2_G.SelectedIndex == 0)) { CBB_rebalance_dma2_G.SelectedIndex = 0; CBB_rebalance_dma_배열_G.SelectedIndex = 0; }
        }
        private void CBB_dma_배열_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_rebalance_dma_배열_A) && (CBB_rebalance_dma1_A.SelectedIndex == 0 || CBB_rebalance_dma2_A.SelectedIndex == 0)) CBB_rebalance_dma_배열_A.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_dma_배열_B) && (CBB_rebalance_dma1_B.SelectedIndex == 0 || CBB_rebalance_dma2_B.SelectedIndex == 0)) CBB_rebalance_dma_배열_B.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_dma_배열_C) && (CBB_rebalance_dma1_C.SelectedIndex == 0 || CBB_rebalance_dma2_C.SelectedIndex == 0)) CBB_rebalance_dma_배열_C.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_dma_배열_D) && (CBB_rebalance_dma1_D.SelectedIndex == 0 || CBB_rebalance_dma2_D.SelectedIndex == 0)) CBB_rebalance_dma_배열_D.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_dma_배열_E) && (CBB_rebalance_dma1_E.SelectedIndex == 0 || CBB_rebalance_dma2_E.SelectedIndex == 0)) CBB_rebalance_dma_배열_E.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_dma_배열_F) && (CBB_rebalance_dma1_F.SelectedIndex == 0 || CBB_rebalance_dma2_F.SelectedIndex == 0)) CBB_rebalance_dma_배열_F.SelectedIndex = 0;
            if (sender.Equals(CBB_rebalance_dma_배열_G) && (CBB_rebalance_dma1_G.SelectedIndex == 0 || CBB_rebalance_dma2_G.SelectedIndex == 0)) CBB_rebalance_dma_배열_G.SelectedIndex = 0;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

    }
}
