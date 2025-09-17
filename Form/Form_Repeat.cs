using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace 지니_64
{
    public partial class Form_Repeat : Form
    {
        public static Form_Repeat form;
        public Form_Repeat()
        {
            form = this;
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void Form_Repeat_Load()
        {
            Form1.음소거 = true;

            CB_repeat_kind_A.Checked = Properties.Settings.Default.CB_repeat_kind_A;
            CB_repeat_kind_C.Checked = Properties.Settings.Default.CB_repeat_kind_C;
            CB_repeat_kind_B.Checked = Properties.Settings.Default.CB_repeat_kind_B;
            CB_repeat_kind_D.Checked = Properties.Settings.Default.CB_repeat_kind_D;
            CB_repeat_kind_E.Checked = Properties.Settings.Default.CB_repeat_kind_E;
            CB_repeat_kind_F.Checked = Properties.Settings.Default.CB_repeat_kind_F;
            CB_repeat_kind_G.Checked = Properties.Settings.Default.CB_repeat_kind_G;
            CB_repeat_kind_H.Checked = Properties.Settings.Default.CB_repeat_kind_H;
            CB_repeat_kind_I.Checked = Properties.Settings.Default.CB_repeat_kind_I;
            CB_repeat_kind_J.Checked = Properties.Settings.Default.CB_repeat_kind_J;
            CB_repeat_kind_K.Checked = Properties.Settings.Default.CB_repeat_kind_K;
            CB_repeat_kind_L.Checked = Properties.Settings.Default.CB_repeat_kind_L;
            CB_repeat_kind_M.Checked = Properties.Settings.Default.CB_repeat_kind_M;
            CB_repeat_kind_N.Checked = Properties.Settings.Default.CB_repeat_kind_N;

            combo_repeat_jumun_A.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_A;
            combo_repeat_jumun_B.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_B;
            combo_repeat_jumun_C.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_C;
            combo_repeat_jumun_D.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_D;
            combo_repeat_jumun_E.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_E;
            combo_repeat_jumun_F.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_F;
            combo_repeat_jumun_G.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_G;
            combo_repeat_jumun_H.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_H;
            combo_repeat_jumun_I.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_I;
            combo_repeat_jumun_J.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_J;
            combo_repeat_jumun_K.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_K;
            combo_repeat_jumun_L.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_L;
            combo_repeat_jumun_M.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_M;
            combo_repeat_jumun_N.SelectedIndex = Properties.Settings.Default.combo_repeat_jumun_N;

            MT_repeat_time_start_A.Text = Properties.Settings.Default.MT_repeat_time_start_A.ToString();
            MT_repeat_time_start_B.Text = Properties.Settings.Default.MT_repeat_time_start_B.ToString();
            MT_repeat_time_start_C.Text = Properties.Settings.Default.MT_repeat_time_start_C.ToString();
            MT_repeat_time_start_D.Text = Properties.Settings.Default.MT_repeat_time_start_D.ToString();
            MT_repeat_time_start_E.Text = Properties.Settings.Default.MT_repeat_time_start_E.ToString();
            MT_repeat_time_start_F.Text = Properties.Settings.Default.MT_repeat_time_start_F.ToString();
            MT_repeat_time_start_G.Text = Properties.Settings.Default.MT_repeat_time_start_G.ToString();
            MT_repeat_time_start_H.Text = Properties.Settings.Default.MT_repeat_time_start_H.ToString();
            MT_repeat_time_start_I.Text = Properties.Settings.Default.MT_repeat_time_start_I.ToString();
            MT_repeat_time_start_J.Text = Properties.Settings.Default.MT_repeat_time_start_J.ToString();
            MT_repeat_time_start_K.Text = Properties.Settings.Default.MT_repeat_time_start_K.ToString();
            MT_repeat_time_start_L.Text = Properties.Settings.Default.MT_repeat_time_start_L.ToString();
            MT_repeat_time_start_M.Text = Properties.Settings.Default.MT_repeat_time_start_M.ToString();
            MT_repeat_time_start_N.Text = Properties.Settings.Default.MT_repeat_time_start_N.ToString();

            MT_repeat_time_end_A.Text = Properties.Settings.Default.MT_repeat_time_end_A.ToString();
            MT_repeat_time_end_B.Text = Properties.Settings.Default.MT_repeat_time_end_B.ToString();
            MT_repeat_time_end_C.Text = Properties.Settings.Default.MT_repeat_time_end_C.ToString();
            MT_repeat_time_end_D.Text = Properties.Settings.Default.MT_repeat_time_end_D.ToString();
            MT_repeat_time_end_E.Text = Properties.Settings.Default.MT_repeat_time_end_E.ToString();
            MT_repeat_time_end_F.Text = Properties.Settings.Default.MT_repeat_time_end_F.ToString();
            MT_repeat_time_end_G.Text = Properties.Settings.Default.MT_repeat_time_end_G.ToString();
            MT_repeat_time_end_H.Text = Properties.Settings.Default.MT_repeat_time_end_H.ToString();
            MT_repeat_time_end_I.Text = Properties.Settings.Default.MT_repeat_time_end_I.ToString();
            MT_repeat_time_end_J.Text = Properties.Settings.Default.MT_repeat_time_end_J.ToString();
            MT_repeat_time_end_K.Text = Properties.Settings.Default.MT_repeat_time_end_K.ToString();
            MT_repeat_time_end_L.Text = Properties.Settings.Default.MT_repeat_time_end_L.ToString();
            MT_repeat_time_end_M.Text = Properties.Settings.Default.MT_repeat_time_end_M.ToString();
            MT_repeat_time_end_N.Text = Properties.Settings.Default.MT_repeat_time_end_N.ToString();

            MT_repeat_repeat_time_A.Text = Properties.Settings.Default.MT_repeat_repeat_time_A.ToString();
            MT_repeat_repeat_time_B.Text = Properties.Settings.Default.MT_repeat_repeat_time_B.ToString();
            MT_repeat_repeat_time_C.Text = Properties.Settings.Default.MT_repeat_repeat_time_C.ToString();
            MT_repeat_repeat_time_D.Text = Properties.Settings.Default.MT_repeat_repeat_time_D.ToString();
            MT_repeat_repeat_time_E.Text = Properties.Settings.Default.MT_repeat_repeat_time_E.ToString();
            MT_repeat_repeat_time_F.Text = Properties.Settings.Default.MT_repeat_repeat_time_F.ToString();
            MT_repeat_repeat_time_G.Text = Properties.Settings.Default.MT_repeat_repeat_time_G.ToString();
            MT_repeat_repeat_time_H.Text = Properties.Settings.Default.MT_repeat_repeat_time_H.ToString();
            MT_repeat_repeat_time_I.Text = Properties.Settings.Default.MT_repeat_repeat_time_I.ToString();
            MT_repeat_repeat_time_J.Text = Properties.Settings.Default.MT_repeat_repeat_time_J.ToString();
            MT_repeat_repeat_time_K.Text = Properties.Settings.Default.MT_repeat_repeat_time_K.ToString();
            MT_repeat_repeat_time_L.Text = Properties.Settings.Default.MT_repeat_repeat_time_L.ToString();
            MT_repeat_repeat_time_M.Text = Properties.Settings.Default.MT_repeat_repeat_time_M.ToString();
            MT_repeat_repeat_time_N.Text = Properties.Settings.Default.MT_repeat_repeat_time_N.ToString();

            TB_repeat_suik_1_A.Text = Properties.Settings.Default.TB_repeat_suik_1_A.ToString();
            TB_repeat_suik_1_B.Text = Properties.Settings.Default.TB_repeat_suik_1_B.ToString();
            TB_repeat_suik_1_C.Text = Properties.Settings.Default.TB_repeat_suik_1_C.ToString();
            TB_repeat_suik_1_D.Text = Properties.Settings.Default.TB_repeat_suik_1_D.ToString();
            TB_repeat_suik_1_E.Text = Properties.Settings.Default.TB_repeat_suik_1_E.ToString();
            TB_repeat_suik_1_F.Text = Properties.Settings.Default.TB_repeat_suik_1_F.ToString();
            TB_repeat_suik_1_G.Text = Properties.Settings.Default.TB_repeat_suik_1_G.ToString();
            TB_repeat_suik_1_H.Text = Properties.Settings.Default.TB_repeat_suik_1_H.ToString();
            TB_repeat_suik_1_I.Text = Properties.Settings.Default.TB_repeat_suik_1_I.ToString();
            TB_repeat_suik_1_J.Text = Properties.Settings.Default.TB_repeat_suik_1_J.ToString();
            TB_repeat_suik_1_K.Text = Properties.Settings.Default.TB_repeat_suik_1_K.ToString();
            TB_repeat_suik_1_L.Text = Properties.Settings.Default.TB_repeat_suik_1_L.ToString();
            TB_repeat_suik_1_M.Text = Properties.Settings.Default.TB_repeat_suik_1_M.ToString();
            TB_repeat_suik_1_N.Text = Properties.Settings.Default.TB_repeat_suik_1_N.ToString();

            CB_repeat_choice_A.Checked = Properties.Settings.Default.CB_repeat_choice_A;
            CB_repeat_choice_B.Checked = Properties.Settings.Default.CB_repeat_choice_B;
            CB_repeat_choice_C.Checked = Properties.Settings.Default.CB_repeat_choice_C;
            CB_repeat_choice_D.Checked = Properties.Settings.Default.CB_repeat_choice_D;
            CB_repeat_choice_E.Checked = Properties.Settings.Default.CB_repeat_choice_E;
            CB_repeat_choice_F.Checked = Properties.Settings.Default.CB_repeat_choice_F;
            CB_repeat_choice_G.Checked = Properties.Settings.Default.CB_repeat_choice_G;
            CB_repeat_choice_H.Checked = Properties.Settings.Default.CB_repeat_choice_H;
            CB_repeat_choice_I.Checked = Properties.Settings.Default.CB_repeat_choice_I;
            CB_repeat_choice_J.Checked = Properties.Settings.Default.CB_repeat_choice_J;
            CB_repeat_choice_K.Checked = Properties.Settings.Default.CB_repeat_choice_K;
            CB_repeat_choice_L.Checked = Properties.Settings.Default.CB_repeat_choice_L;
            CB_repeat_choice_M.Checked = Properties.Settings.Default.CB_repeat_choice_M;
            CB_repeat_choice_N.Checked = Properties.Settings.Default.CB_repeat_choice_N;
            CB_Repeat_기준금.Checked = Properties.Settings.Default.CB_Repeat_기준금;

            TB_repeat_suik_2_A.Text = Properties.Settings.Default.TB_repeat_suik_2_A.ToString();
            TB_repeat_suik_2_B.Text = Properties.Settings.Default.TB_repeat_suik_2_B.ToString();
            TB_repeat_suik_2_C.Text = Properties.Settings.Default.TB_repeat_suik_2_C.ToString();
            TB_repeat_suik_2_D.Text = Properties.Settings.Default.TB_repeat_suik_2_D.ToString();
            TB_repeat_suik_2_E.Text = Properties.Settings.Default.TB_repeat_suik_2_E.ToString();
            TB_repeat_suik_2_F.Text = Properties.Settings.Default.TB_repeat_suik_2_F.ToString();
            TB_repeat_suik_2_G.Text = Properties.Settings.Default.TB_repeat_suik_2_G.ToString();
            TB_repeat_suik_2_H.Text = Properties.Settings.Default.TB_repeat_suik_2_H.ToString();
            TB_repeat_suik_2_I.Text = Properties.Settings.Default.TB_repeat_suik_2_I.ToString();
            TB_repeat_suik_2_J.Text = Properties.Settings.Default.TB_repeat_suik_2_J.ToString();
            TB_repeat_suik_2_K.Text = Properties.Settings.Default.TB_repeat_suik_2_K.ToString();
            TB_repeat_suik_2_L.Text = Properties.Settings.Default.TB_repeat_suik_2_L.ToString();
            TB_repeat_suik_2_M.Text = Properties.Settings.Default.TB_repeat_suik_2_M.ToString();
            TB_repeat_suik_2_N.Text = Properties.Settings.Default.TB_repeat_suik_2_N.ToString();

            TB_repeat_value_A.Text = Properties.Settings.Default.TB_repeat_value_A.ToString();
            TB_repeat_value_B.Text = Properties.Settings.Default.TB_repeat_value_B.ToString();
            TB_repeat_value_C.Text = Properties.Settings.Default.TB_repeat_value_C.ToString();
            TB_repeat_value_D.Text = Properties.Settings.Default.TB_repeat_value_D.ToString();
            TB_repeat_value_E.Text = Properties.Settings.Default.TB_repeat_value_E.ToString();
            TB_repeat_value_F.Text = Properties.Settings.Default.TB_repeat_value_F.ToString();
            TB_repeat_value_G.Text = Properties.Settings.Default.TB_repeat_value_G.ToString();
            TB_repeat_value_H.Text = Properties.Settings.Default.TB_repeat_value_H.ToString();
            TB_repeat_value_I.Text = Properties.Settings.Default.TB_repeat_value_I.ToString();
            TB_repeat_value_J.Text = Properties.Settings.Default.TB_repeat_value_J.ToString();
            TB_repeat_value_K.Text = Properties.Settings.Default.TB_repeat_value_K.ToString();
            TB_repeat_value_L.Text = Properties.Settings.Default.TB_repeat_value_L.ToString();
            TB_repeat_value_M.Text = Properties.Settings.Default.TB_repeat_value_M.ToString();
            TB_repeat_value_N.Text = Properties.Settings.Default.TB_repeat_value_N.ToString();

            combo_repeat_suik_gubun_A.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_A;
            combo_repeat_suik_gubun_B.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_B;
            combo_repeat_suik_gubun_C.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_C;
            combo_repeat_suik_gubun_D.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_D;
            combo_repeat_suik_gubun_E.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_E;
            combo_repeat_suik_gubun_F.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_F;
            combo_repeat_suik_gubun_G.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_G;
            combo_repeat_suik_gubun_H.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_H;
            combo_repeat_suik_gubun_I.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_I;
            combo_repeat_suik_gubun_J.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_J;
            combo_repeat_suik_gubun_K.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_K;
            combo_repeat_suik_gubun_L.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_L;
            combo_repeat_suik_gubun_M.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_M;
            combo_repeat_suik_gubun_N.SelectedIndex = Properties.Settings.Default.combo_repeat_suik_gubun_N;

            TB_repeat_sell_ratio_A.Text = Properties.Settings.Default.TB_repeat_sell_ratio_A.ToString();
            TB_repeat_sell_ratio_B.Text = Properties.Settings.Default.TB_repeat_sell_ratio_B.ToString();
            TB_repeat_sell_ratio_C.Text = Properties.Settings.Default.TB_repeat_sell_ratio_C.ToString();
            TB_repeat_sell_ratio_D.Text = Properties.Settings.Default.TB_repeat_sell_ratio_D.ToString();
            TB_repeat_sell_ratio_E.Text = Properties.Settings.Default.TB_repeat_sell_ratio_E.ToString();
            TB_repeat_sell_ratio_F.Text = Properties.Settings.Default.TB_repeat_sell_ratio_F.ToString();
            TB_repeat_sell_ratio_G.Text = Properties.Settings.Default.TB_repeat_sell_ratio_G.ToString();
            TB_repeat_sell_ratio_H.Text = Properties.Settings.Default.TB_repeat_sell_ratio_H.ToString();
            TB_repeat_sell_ratio_I.Text = Properties.Settings.Default.TB_repeat_sell_ratio_I.ToString();
            TB_repeat_sell_ratio_J.Text = Properties.Settings.Default.TB_repeat_sell_ratio_J.ToString();
            TB_repeat_sell_ratio_K.Text = Properties.Settings.Default.TB_repeat_sell_ratio_K.ToString();
            TB_repeat_sell_ratio_L.Text = Properties.Settings.Default.TB_repeat_sell_ratio_L.ToString();
            TB_repeat_sell_ratio_M.Text = Properties.Settings.Default.TB_repeat_sell_ratio_M.ToString();
            TB_repeat_sell_ratio_N.Text = Properties.Settings.Default.TB_repeat_sell_ratio_N.ToString();

            combo_repeat_sell_gubun_A.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_A;
            combo_repeat_sell_gubun_B.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_B;
            combo_repeat_sell_gubun_C.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_C;
            combo_repeat_sell_gubun_D.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_D;
            combo_repeat_sell_gubun_E.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_E;
            combo_repeat_sell_gubun_F.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_F;
            combo_repeat_sell_gubun_G.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_G;
            combo_repeat_sell_gubun_H.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_H;
            combo_repeat_sell_gubun_I.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_I;
            combo_repeat_sell_gubun_J.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_J;
            combo_repeat_sell_gubun_K.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_K;
            combo_repeat_sell_gubun_L.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_L;
            combo_repeat_sell_gubun_M.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_M;
            combo_repeat_sell_gubun_N.SelectedIndex = Properties.Settings.Default.combo_repeat_sell_gubun_N;

            TB_repeat_maemae_1_A.Text = Properties.Settings.Default.TB_repeat_maemae_1_A.ToString();
            TB_repeat_maemae_1_B.Text = Properties.Settings.Default.TB_repeat_maemae_1_B.ToString();
            TB_repeat_maemae_1_C.Text = Properties.Settings.Default.TB_repeat_maemae_1_C.ToString();
            TB_repeat_maemae_1_D.Text = Properties.Settings.Default.TB_repeat_maemae_1_D.ToString();
            TB_repeat_maemae_1_E.Text = Properties.Settings.Default.TB_repeat_maemae_1_E.ToString();
            TB_repeat_maemae_1_F.Text = Properties.Settings.Default.TB_repeat_maemae_1_F.ToString();
            TB_repeat_maemae_1_G.Text = Properties.Settings.Default.TB_repeat_maemae_1_G.ToString();
            TB_repeat_maemae_1_H.Text = Properties.Settings.Default.TB_repeat_maemae_1_H.ToString();
            TB_repeat_maemae_1_I.Text = Properties.Settings.Default.TB_repeat_maemae_1_I.ToString();
            TB_repeat_maemae_1_J.Text = Properties.Settings.Default.TB_repeat_maemae_1_J.ToString();
            TB_repeat_maemae_1_K.Text = Properties.Settings.Default.TB_repeat_maemae_1_K.ToString();
            TB_repeat_maemae_1_L.Text = Properties.Settings.Default.TB_repeat_maemae_1_L.ToString();
            TB_repeat_maemae_1_M.Text = Properties.Settings.Default.TB_repeat_maemae_1_M.ToString();
            TB_repeat_maemae_1_N.Text = Properties.Settings.Default.TB_repeat_maemae_1_N.ToString();

            TB_repeat_maemae_2_A.Text = Properties.Settings.Default.TB_repeat_maemae_2_A.ToString();
            TB_repeat_maemae_2_B.Text = Properties.Settings.Default.TB_repeat_maemae_2_B.ToString();
            TB_repeat_maemae_2_C.Text = Properties.Settings.Default.TB_repeat_maemae_2_C.ToString();
            TB_repeat_maemae_2_D.Text = Properties.Settings.Default.TB_repeat_maemae_2_D.ToString();
            TB_repeat_maemae_2_E.Text = Properties.Settings.Default.TB_repeat_maemae_2_E.ToString();
            TB_repeat_maemae_2_F.Text = Properties.Settings.Default.TB_repeat_maemae_2_F.ToString();
            TB_repeat_maemae_2_G.Text = Properties.Settings.Default.TB_repeat_maemae_2_G.ToString();
            TB_repeat_maemae_2_H.Text = Properties.Settings.Default.TB_repeat_maemae_2_H.ToString();
            TB_repeat_maemae_2_I.Text = Properties.Settings.Default.TB_repeat_maemae_2_I.ToString();
            TB_repeat_maemae_2_J.Text = Properties.Settings.Default.TB_repeat_maemae_2_J.ToString();
            TB_repeat_maemae_2_K.Text = Properties.Settings.Default.TB_repeat_maemae_2_K.ToString();
            TB_repeat_maemae_2_L.Text = Properties.Settings.Default.TB_repeat_maemae_2_L.ToString();
            TB_repeat_maemae_2_M.Text = Properties.Settings.Default.TB_repeat_maemae_2_M.ToString();
            TB_repeat_maemae_2_N.Text = Properties.Settings.Default.TB_repeat_maemae_2_N.ToString();

            combo_repeat_maemae_gubun_A.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_A;
            combo_repeat_maemae_gubun_B.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_B;
            combo_repeat_maemae_gubun_C.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_C;
            combo_repeat_maemae_gubun_D.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_D;
            combo_repeat_maemae_gubun_E.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_E;
            combo_repeat_maemae_gubun_F.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_F;
            combo_repeat_maemae_gubun_G.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_G;
            combo_repeat_maemae_gubun_H.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_H;
            combo_repeat_maemae_gubun_I.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_I;
            combo_repeat_maemae_gubun_J.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_J;
            combo_repeat_maemae_gubun_K.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_K;
            combo_repeat_maemae_gubun_L.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_L;
            combo_repeat_maemae_gubun_M.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_M;
            combo_repeat_maemae_gubun_N.SelectedIndex = Properties.Settings.Default.combo_repeat_maemae_gubun_N;

            MTB_repeat_repeat_A.Text = Properties.Settings.Default.MTB_repeat_repeat_A.ToString();
            MTB_repeat_repeat_B.Text = Properties.Settings.Default.MTB_repeat_repeat_B.ToString();
            MTB_repeat_repeat_C.Text = Properties.Settings.Default.MTB_repeat_repeat_C.ToString();
            MTB_repeat_repeat_D.Text = Properties.Settings.Default.MTB_repeat_repeat_D.ToString();
            MTB_repeat_repeat_E.Text = Properties.Settings.Default.MTB_repeat_repeat_E.ToString();
            MTB_repeat_repeat_F.Text = Properties.Settings.Default.MTB_repeat_repeat_F.ToString();
            MTB_repeat_repeat_G.Text = Properties.Settings.Default.MTB_repeat_repeat_G.ToString();
            MTB_repeat_repeat_H.Text = Properties.Settings.Default.MTB_repeat_repeat_H.ToString();
            MTB_repeat_repeat_I.Text = Properties.Settings.Default.MTB_repeat_repeat_I.ToString();
            MTB_repeat_repeat_J.Text = Properties.Settings.Default.MTB_repeat_repeat_J.ToString();
            MTB_repeat_repeat_K.Text = Properties.Settings.Default.MTB_repeat_repeat_K.ToString();
            MTB_repeat_repeat_L.Text = Properties.Settings.Default.MTB_repeat_repeat_L.ToString();
            MTB_repeat_repeat_M.Text = Properties.Settings.Default.MTB_repeat_repeat_M.ToString();
            MTB_repeat_repeat_N.Text = Properties.Settings.Default.MTB_repeat_repeat_N.ToString();

            MTB_repeat_Cancel_time_A.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_A.ToString();
            MTB_repeat_Cancel_time_B.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_B.ToString();
            MTB_repeat_Cancel_time_C.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_C.ToString();
            MTB_repeat_Cancel_time_D.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_D.ToString();
            MTB_repeat_Cancel_time_E.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_E.ToString();
            MTB_repeat_Cancel_time_F.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_F.ToString();
            MTB_repeat_Cancel_time_G.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_G.ToString();
            MTB_repeat_Cancel_time_H.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_H.ToString();
            MTB_repeat_Cancel_time_I.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_I.ToString();
            MTB_repeat_Cancel_time_J.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_J.ToString();
            MTB_repeat_Cancel_time_K.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_K.ToString();
            MTB_repeat_Cancel_time_L.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_L.ToString();
            MTB_repeat_Cancel_time_M.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_M.ToString();
            MTB_repeat_Cancel_time_N.Text = Properties.Settings.Default.MTB_repeat_Cancel_time_N.ToString();

            combo_repeat_Cancel_A.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_A;
            combo_repeat_Cancel_B.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_B;
            combo_repeat_Cancel_C.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_C;
            combo_repeat_Cancel_D.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_D;
            combo_repeat_Cancel_E.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_E;
            combo_repeat_Cancel_F.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_F;
            combo_repeat_Cancel_G.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_G;
            combo_repeat_Cancel_H.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_H;
            combo_repeat_Cancel_I.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_I;
            combo_repeat_Cancel_J.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_J;
            combo_repeat_Cancel_K.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_K;
            combo_repeat_Cancel_L.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_L;
            combo_repeat_Cancel_M.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_M;
            combo_repeat_Cancel_N.SelectedIndex = Properties.Settings.Default.combo_repeat_Cancel_N;

            MTB_repeat_delay_A.Text = Properties.Settings.Default.MTB_repeat_delay_A.ToString();
            MTB_repeat_delay_B.Text = Properties.Settings.Default.MTB_repeat_delay_B.ToString();
            MTB_repeat_delay_C.Text = Properties.Settings.Default.MTB_repeat_delay_C.ToString();
            MTB_repeat_delay_D.Text = Properties.Settings.Default.MTB_repeat_delay_D.ToString();
            MTB_repeat_delay_E.Text = Properties.Settings.Default.MTB_repeat_delay_E.ToString();
            MTB_repeat_delay_F.Text = Properties.Settings.Default.MTB_repeat_delay_F.ToString();
            MTB_repeat_delay_G.Text = Properties.Settings.Default.MTB_repeat_delay_G.ToString();
            MTB_repeat_delay_H.Text = Properties.Settings.Default.MTB_repeat_delay_H.ToString();
            MTB_repeat_delay_I.Text = Properties.Settings.Default.MTB_repeat_delay_I.ToString();
            MTB_repeat_delay_J.Text = Properties.Settings.Default.MTB_repeat_delay_J.ToString();
            MTB_repeat_delay_K.Text = Properties.Settings.Default.MTB_repeat_delay_K.ToString();
            MTB_repeat_delay_L.Text = Properties.Settings.Default.MTB_repeat_delay_L.ToString();
            MTB_repeat_delay_M.Text = Properties.Settings.Default.MTB_repeat_delay_M.ToString();
            MTB_repeat_delay_N.Text = Properties.Settings.Default.MTB_repeat_delay_N.ToString();

            CB_repeat_use_A.Checked = Properties.Settings.Default.CB_repeat_use_A;
            CB_repeat_use_B.Checked = Properties.Settings.Default.CB_repeat_use_B;
            CB_repeat_use_C.Checked = Properties.Settings.Default.CB_repeat_use_C;
            CB_repeat_use_D.Checked = Properties.Settings.Default.CB_repeat_use_D;
            CB_repeat_use_E.Checked = Properties.Settings.Default.CB_repeat_use_E;
            CB_repeat_use_F.Checked = Properties.Settings.Default.CB_repeat_use_F;
            CB_repeat_use_G.Checked = Properties.Settings.Default.CB_repeat_use_G;
            CB_repeat_use_H.Checked = Properties.Settings.Default.CB_repeat_use_H;
            CB_repeat_use_I.Checked = Properties.Settings.Default.CB_repeat_use_I;
            CB_repeat_use_J.Checked = Properties.Settings.Default.CB_repeat_use_J;
            CB_repeat_use_K.Checked = Properties.Settings.Default.CB_repeat_use_K;
            CB_repeat_use_L.Checked = Properties.Settings.Default.CB_repeat_use_L;
            CB_repeat_use_M.Checked = Properties.Settings.Default.CB_repeat_use_M;
            CB_repeat_use_N.Checked = Properties.Settings.Default.CB_repeat_use_N;

            combo_repeat_use_condition_A.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_A;
            combo_repeat_use_condition_B.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_B;
            combo_repeat_use_condition_C.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_C;
            combo_repeat_use_condition_D.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_D;
            combo_repeat_use_condition_E.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_E;
            combo_repeat_use_condition_F.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_F;
            combo_repeat_use_condition_G.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_G;
            combo_repeat_use_condition_H.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_H;
            combo_repeat_use_condition_I.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_I;
            combo_repeat_use_condition_J.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_J;
            combo_repeat_use_condition_K.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_K;
            combo_repeat_use_condition_L.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_L;
            combo_repeat_use_condition_M.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_M;
            combo_repeat_use_condition_N.SelectedIndex = Properties.Settings.Default.combo_repeat_use_condition_N;

            string 반복매매검색식 = Properties.Settings.Default.반복매매검색식;

            combo_repeat_condition_A.Items.Add(반복매매검색식.Split('^')[0]); combo_repeat_condition_A.Text = 반복매매검색식.Split('^')[0];
            combo_repeat_condition_B.Items.Add(반복매매검색식.Split('^')[1]); combo_repeat_condition_B.Text = 반복매매검색식.Split('^')[1];
            combo_repeat_condition_C.Items.Add(반복매매검색식.Split('^')[2]); combo_repeat_condition_C.Text = 반복매매검색식.Split('^')[2];
            combo_repeat_condition_D.Items.Add(반복매매검색식.Split('^')[3]); combo_repeat_condition_D.Text = 반복매매검색식.Split('^')[3];
            combo_repeat_condition_E.Items.Add(반복매매검색식.Split('^')[4]); combo_repeat_condition_E.Text = 반복매매검색식.Split('^')[4];
            combo_repeat_condition_F.Items.Add(반복매매검색식.Split('^')[5]); combo_repeat_condition_F.Text = 반복매매검색식.Split('^')[5];
            combo_repeat_condition_G.Items.Add(반복매매검색식.Split('^')[6]); combo_repeat_condition_G.Text = 반복매매검색식.Split('^')[6];
            combo_repeat_condition_H.Items.Add(반복매매검색식.Split('^')[7]); combo_repeat_condition_H.Text = 반복매매검색식.Split('^')[7];
            combo_repeat_condition_I.Items.Add(반복매매검색식.Split('^')[8]); combo_repeat_condition_I.Text = 반복매매검색식.Split('^')[8];
            combo_repeat_condition_J.Items.Add(반복매매검색식.Split('^')[9]); combo_repeat_condition_J.Text = 반복매매검색식.Split('^')[9];
            combo_repeat_condition_K.Items.Add(반복매매검색식.Split('^')[10]); combo_repeat_condition_K.Text = 반복매매검색식.Split('^')[10];
            combo_repeat_condition_L.Items.Add(반복매매검색식.Split('^')[11]); combo_repeat_condition_L.Text = 반복매매검색식.Split('^')[11];
            combo_repeat_condition_M.Items.Add(반복매매검색식.Split('^')[12]); combo_repeat_condition_M.Text = 반복매매검색식.Split('^')[12];
            combo_repeat_condition_N.Items.Add(반복매매검색식.Split('^')[13]); combo_repeat_condition_N.Text = 반복매매검색식.Split('^')[13];

            TB_repeat_매입금_A.Text = Properties.Settings.Default.TB_repeat_매입금_A.ToString();
            TB_repeat_매입금_B.Text = Properties.Settings.Default.TB_repeat_매입금_B.ToString();
            TB_repeat_매입금_C.Text = Properties.Settings.Default.TB_repeat_매입금_C.ToString();
            TB_repeat_매입금_D.Text = Properties.Settings.Default.TB_repeat_매입금_D.ToString();
            TB_repeat_매입금_E.Text = Properties.Settings.Default.TB_repeat_매입금_E.ToString();
            TB_repeat_매입금_F.Text = Properties.Settings.Default.TB_repeat_매입금_F.ToString();
            TB_repeat_매입금_G.Text = Properties.Settings.Default.TB_repeat_매입금_G.ToString();
            TB_repeat_매입금_H.Text = Properties.Settings.Default.TB_repeat_매입금_H.ToString();
            TB_repeat_매입금_I.Text = Properties.Settings.Default.TB_repeat_매입금_I.ToString();
            TB_repeat_매입금_J.Text = Properties.Settings.Default.TB_repeat_매입금_J.ToString();
            TB_repeat_매입금_K.Text = Properties.Settings.Default.TB_repeat_매입금_K.ToString();
            TB_repeat_매입금_L.Text = Properties.Settings.Default.TB_repeat_매입금_L.ToString();
            TB_repeat_매입금_M.Text = Properties.Settings.Default.TB_repeat_매입금_M.ToString();
            TB_repeat_매입금_N.Text = Properties.Settings.Default.TB_repeat_매입금_N.ToString();

            TB_repeat_누적거래량_A.Text = Properties.Settings.Default.TB_repeat_누적거래량_A.ToString();
            TB_repeat_누적거래량_B.Text = Properties.Settings.Default.TB_repeat_누적거래량_B.ToString();
            TB_repeat_누적거래량_C.Text = Properties.Settings.Default.TB_repeat_누적거래량_C.ToString();
            TB_repeat_누적거래량_D.Text = Properties.Settings.Default.TB_repeat_누적거래량_D.ToString();
            TB_repeat_누적거래량_E.Text = Properties.Settings.Default.TB_repeat_누적거래량_E.ToString();
            TB_repeat_누적거래량_F.Text = Properties.Settings.Default.TB_repeat_누적거래량_F.ToString();
            TB_repeat_누적거래량_G.Text = Properties.Settings.Default.TB_repeat_누적거래량_G.ToString();
            TB_repeat_누적거래량_H.Text = Properties.Settings.Default.TB_repeat_누적거래량_H.ToString();
            TB_repeat_누적거래량_I.Text = Properties.Settings.Default.TB_repeat_누적거래량_I.ToString();
            TB_repeat_누적거래량_J.Text = Properties.Settings.Default.TB_repeat_누적거래량_J.ToString();
            TB_repeat_누적거래량_K.Text = Properties.Settings.Default.TB_repeat_누적거래량_K.ToString();
            TB_repeat_누적거래량_L.Text = Properties.Settings.Default.TB_repeat_누적거래량_L.ToString();
            TB_repeat_누적거래량_M.Text = Properties.Settings.Default.TB_repeat_누적거래량_M.ToString();
            TB_repeat_누적거래량_N.Text = Properties.Settings.Default.TB_repeat_누적거래량_N.ToString();

            TB_repeat_누적거래대금_A.Text = Properties.Settings.Default.TB_repeat_누적거래대금_A.ToString();
            TB_repeat_누적거래대금_B.Text = Properties.Settings.Default.TB_repeat_누적거래대금_B.ToString();
            TB_repeat_누적거래대금_C.Text = Properties.Settings.Default.TB_repeat_누적거래대금_C.ToString();
            TB_repeat_누적거래대금_D.Text = Properties.Settings.Default.TB_repeat_누적거래대금_D.ToString();
            TB_repeat_누적거래대금_E.Text = Properties.Settings.Default.TB_repeat_누적거래대금_E.ToString();
            TB_repeat_누적거래대금_F.Text = Properties.Settings.Default.TB_repeat_누적거래대금_F.ToString();
            TB_repeat_누적거래대금_G.Text = Properties.Settings.Default.TB_repeat_누적거래대금_G.ToString();
            TB_repeat_누적거래대금_H.Text = Properties.Settings.Default.TB_repeat_누적거래대금_H.ToString();
            TB_repeat_누적거래대금_I.Text = Properties.Settings.Default.TB_repeat_누적거래대금_I.ToString();
            TB_repeat_누적거래대금_J.Text = Properties.Settings.Default.TB_repeat_누적거래대금_J.ToString();
            TB_repeat_누적거래대금_K.Text = Properties.Settings.Default.TB_repeat_누적거래대금_K.ToString();
            TB_repeat_누적거래대금_L.Text = Properties.Settings.Default.TB_repeat_누적거래대금_L.ToString();
            TB_repeat_누적거래대금_M.Text = Properties.Settings.Default.TB_repeat_누적거래대금_M.ToString();
            TB_repeat_누적거래대금_N.Text = Properties.Settings.Default.TB_repeat_누적거래대금_N.ToString();

            TB_repeat_mma_A.Text = Properties.Settings.Default.TB_repeat_mma_A.ToString();
            TB_repeat_mma_B.Text = Properties.Settings.Default.TB_repeat_mma_B.ToString();
            TB_repeat_mma_C.Text = Properties.Settings.Default.TB_repeat_mma_C.ToString();
            TB_repeat_mma_D.Text = Properties.Settings.Default.TB_repeat_mma_D.ToString();
            TB_repeat_mma_E.Text = Properties.Settings.Default.TB_repeat_mma_E.ToString();
            TB_repeat_mma_F.Text = Properties.Settings.Default.TB_repeat_mma_F.ToString();
            TB_repeat_mma_G.Text = Properties.Settings.Default.TB_repeat_mma_G.ToString();
            TB_repeat_mma_H.Text = Properties.Settings.Default.TB_repeat_mma_H.ToString();
            TB_repeat_mma_I.Text = Properties.Settings.Default.TB_repeat_mma_I.ToString();
            TB_repeat_mma_J.Text = Properties.Settings.Default.TB_repeat_mma_J.ToString();
            TB_repeat_mma_K.Text = Properties.Settings.Default.TB_repeat_mma_K.ToString();
            TB_repeat_mma_L.Text = Properties.Settings.Default.TB_repeat_mma_L.ToString();
            TB_repeat_mma_M.Text = Properties.Settings.Default.TB_repeat_mma_M.ToString();
            TB_repeat_mma_N.Text = Properties.Settings.Default.TB_repeat_mma_N.ToString();

            CBB_repeat_mma_A.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_A;
            CBB_repeat_mma_B.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_B;
            CBB_repeat_mma_C.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_C;
            CBB_repeat_mma_D.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_D;
            CBB_repeat_mma_E.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_E;
            CBB_repeat_mma_F.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_F;
            CBB_repeat_mma_G.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_G;
            CBB_repeat_mma_H.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_H;
            CBB_repeat_mma_I.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_I;
            CBB_repeat_mma_J.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_J;
            CBB_repeat_mma_K.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_K;
            CBB_repeat_mma_L.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_L;
            CBB_repeat_mma_M.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_M;
            CBB_repeat_mma_N.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_N;

            TB_repeat_mma2_A.Text = Properties.Settings.Default.TB_repeat_mma2_A.ToString();
            TB_repeat_mma2_B.Text = Properties.Settings.Default.TB_repeat_mma2_B.ToString();
            TB_repeat_mma2_C.Text = Properties.Settings.Default.TB_repeat_mma2_C.ToString();
            TB_repeat_mma2_D.Text = Properties.Settings.Default.TB_repeat_mma2_D.ToString();
            TB_repeat_mma2_E.Text = Properties.Settings.Default.TB_repeat_mma2_E.ToString();
            TB_repeat_mma2_F.Text = Properties.Settings.Default.TB_repeat_mma2_F.ToString();
            TB_repeat_mma2_G.Text = Properties.Settings.Default.TB_repeat_mma2_G.ToString();
            TB_repeat_mma2_H.Text = Properties.Settings.Default.TB_repeat_mma2_H.ToString();
            TB_repeat_mma2_I.Text = Properties.Settings.Default.TB_repeat_mma2_I.ToString();
            TB_repeat_mma2_J.Text = Properties.Settings.Default.TB_repeat_mma2_J.ToString();
            TB_repeat_mma2_K.Text = Properties.Settings.Default.TB_repeat_mma2_K.ToString();
            TB_repeat_mma2_L.Text = Properties.Settings.Default.TB_repeat_mma2_L.ToString();
            TB_repeat_mma2_M.Text = Properties.Settings.Default.TB_repeat_mma2_M.ToString();
            TB_repeat_mma2_N.Text = Properties.Settings.Default.TB_repeat_mma2_N.ToString();

            CBB_repeat_mma2_A.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_A;
            CBB_repeat_mma2_B.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_B;
            CBB_repeat_mma2_C.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_C;
            CBB_repeat_mma2_D.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_D;
            CBB_repeat_mma2_E.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_E;
            CBB_repeat_mma2_F.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_F;
            CBB_repeat_mma2_G.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_G;
            CBB_repeat_mma2_H.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_H;
            CBB_repeat_mma2_I.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_I;
            CBB_repeat_mma2_J.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_J;
            CBB_repeat_mma2_K.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_K;
            CBB_repeat_mma2_L.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_L;
            CBB_repeat_mma2_M.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_M;
            CBB_repeat_mma2_N.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma2_N;

            CBB_repeat_mma_배열_A.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_A;
            CBB_repeat_mma_배열_B.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_B;
            CBB_repeat_mma_배열_C.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_C;
            CBB_repeat_mma_배열_D.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_D;
            CBB_repeat_mma_배열_E.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_E;
            CBB_repeat_mma_배열_F.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_F;
            CBB_repeat_mma_배열_G.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_G;
            CBB_repeat_mma_배열_H.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_H;
            CBB_repeat_mma_배열_I.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_I;
            CBB_repeat_mma_배열_J.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_J;
            CBB_repeat_mma_배열_K.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_K;
            CBB_repeat_mma_배열_L.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_L;
            CBB_repeat_mma_배열_M.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_M;
            CBB_repeat_mma_배열_N.SelectedIndex = Properties.Settings.Default.CBB_repeat_mma_배열_N;

            TB_repeat_dma1_A.Text = Properties.Settings.Default.TB_repeat_dma1_A.ToString();
            TB_repeat_dma1_B.Text = Properties.Settings.Default.TB_repeat_dma1_B.ToString();
            TB_repeat_dma1_C.Text = Properties.Settings.Default.TB_repeat_dma1_C.ToString();
            TB_repeat_dma1_D.Text = Properties.Settings.Default.TB_repeat_dma1_D.ToString();
            TB_repeat_dma1_E.Text = Properties.Settings.Default.TB_repeat_dma1_E.ToString();
            TB_repeat_dma1_F.Text = Properties.Settings.Default.TB_repeat_dma1_F.ToString();
            TB_repeat_dma1_G.Text = Properties.Settings.Default.TB_repeat_dma1_G.ToString();
            TB_repeat_dma1_H.Text = Properties.Settings.Default.TB_repeat_dma1_H.ToString();
            TB_repeat_dma1_I.Text = Properties.Settings.Default.TB_repeat_dma1_I.ToString();
            TB_repeat_dma1_J.Text = Properties.Settings.Default.TB_repeat_dma1_J.ToString();
            TB_repeat_dma1_K.Text = Properties.Settings.Default.TB_repeat_dma1_K.ToString();
            TB_repeat_dma1_L.Text = Properties.Settings.Default.TB_repeat_dma1_L.ToString();
            TB_repeat_dma1_M.Text = Properties.Settings.Default.TB_repeat_dma1_M.ToString();
            TB_repeat_dma1_N.Text = Properties.Settings.Default.TB_repeat_dma1_N.ToString();

            CBB_repeat_dma1_A.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_A;
            CBB_repeat_dma1_B.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_B;
            CBB_repeat_dma1_C.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_C;
            CBB_repeat_dma1_D.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_D;
            CBB_repeat_dma1_E.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_E;
            CBB_repeat_dma1_F.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_F;
            CBB_repeat_dma1_G.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_G;
            CBB_repeat_dma1_H.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_H;
            CBB_repeat_dma1_I.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_I;
            CBB_repeat_dma1_J.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_J;
            CBB_repeat_dma1_K.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_K;
            CBB_repeat_dma1_L.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_L;
            CBB_repeat_dma1_M.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_M;
            CBB_repeat_dma1_N.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma1_N;

            TB_repeat_dma2_A.Text = Properties.Settings.Default.TB_repeat_dma2_A.ToString();
            TB_repeat_dma2_B.Text = Properties.Settings.Default.TB_repeat_dma2_B.ToString();
            TB_repeat_dma2_C.Text = Properties.Settings.Default.TB_repeat_dma2_C.ToString();
            TB_repeat_dma2_D.Text = Properties.Settings.Default.TB_repeat_dma2_D.ToString();
            TB_repeat_dma2_E.Text = Properties.Settings.Default.TB_repeat_dma2_E.ToString();
            TB_repeat_dma2_F.Text = Properties.Settings.Default.TB_repeat_dma2_F.ToString();
            TB_repeat_dma2_G.Text = Properties.Settings.Default.TB_repeat_dma2_G.ToString();
            TB_repeat_dma2_H.Text = Properties.Settings.Default.TB_repeat_dma2_H.ToString();
            TB_repeat_dma2_I.Text = Properties.Settings.Default.TB_repeat_dma2_I.ToString();
            TB_repeat_dma2_J.Text = Properties.Settings.Default.TB_repeat_dma2_J.ToString();
            TB_repeat_dma2_K.Text = Properties.Settings.Default.TB_repeat_dma2_K.ToString();
            TB_repeat_dma2_L.Text = Properties.Settings.Default.TB_repeat_dma2_L.ToString();
            TB_repeat_dma2_M.Text = Properties.Settings.Default.TB_repeat_dma2_M.ToString();
            TB_repeat_dma2_N.Text = Properties.Settings.Default.TB_repeat_dma2_N.ToString();

            CBB_repeat_dma2_A.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_A;
            CBB_repeat_dma2_B.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_B;
            CBB_repeat_dma2_C.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_C;
            CBB_repeat_dma2_D.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_D;
            CBB_repeat_dma2_E.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_E;
            CBB_repeat_dma2_F.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_F;
            CBB_repeat_dma2_G.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_G;
            CBB_repeat_dma2_H.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_H;
            CBB_repeat_dma2_I.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_I;
            CBB_repeat_dma2_J.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_J;
            CBB_repeat_dma2_K.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_K;
            CBB_repeat_dma2_L.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_L;
            CBB_repeat_dma2_M.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_M;
            CBB_repeat_dma2_N.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma2_N;

            CBB_repeat_dma_배열_A.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_A;
            CBB_repeat_dma_배열_B.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_B;
            CBB_repeat_dma_배열_C.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_C;
            CBB_repeat_dma_배열_D.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_D;
            CBB_repeat_dma_배열_E.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_E;
            CBB_repeat_dma_배열_F.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_F;
            CBB_repeat_dma_배열_G.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_G;
            CBB_repeat_dma_배열_H.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_H;
            CBB_repeat_dma_배열_I.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_I;
            CBB_repeat_dma_배열_J.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_J;
            CBB_repeat_dma_배열_K.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_K;
            CBB_repeat_dma_배열_L.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_L;
            CBB_repeat_dma_배열_M.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_M;
            CBB_repeat_dma_배열_N.SelectedIndex = Properties.Settings.Default.CBB_repeat_dma_배열_N;



            if (Form1.로딩완료)
            {
                if (!Properties.Settings.Default.CB_가이드매매)
                {
                    form.combo_repeat_use_condition_A.Enabled = true;
                    form.combo_repeat_use_condition_B.Enabled = true;
                    form.combo_repeat_use_condition_C.Enabled = true;
                    form.combo_repeat_use_condition_D.Enabled = true;
                    form.combo_repeat_use_condition_E.Enabled = true;
                    form.combo_repeat_use_condition_F.Enabled = true;
                    form.combo_repeat_use_condition_G.Enabled = true;
                    form.combo_repeat_use_condition_H.Enabled = true;
                    form.combo_repeat_use_condition_I.Enabled = true;
                    form.combo_repeat_use_condition_J.Enabled = true;
                    form.combo_repeat_use_condition_K.Enabled = true;
                    form.combo_repeat_use_condition_L.Enabled = true;
                    form.combo_repeat_use_condition_M.Enabled = true;
                    form.combo_repeat_use_condition_N.Enabled = true;

                    form.combo_repeat_condition_A.Enabled = true;
                    form.combo_repeat_condition_B.Enabled = true;
                    form.combo_repeat_condition_C.Enabled = true;
                    form.combo_repeat_condition_D.Enabled = true;
                    form.combo_repeat_condition_E.Enabled = true;
                    form.combo_repeat_condition_F.Enabled = true;
                    form.combo_repeat_condition_G.Enabled = true;
                    form.combo_repeat_condition_H.Enabled = true;
                    form.combo_repeat_condition_I.Enabled = true;
                    form.combo_repeat_condition_J.Enabled = true;
                    form.combo_repeat_condition_K.Enabled = true;
                    form.combo_repeat_condition_L.Enabled = true;
                    form.combo_repeat_condition_M.Enabled = true;
                    form.combo_repeat_condition_N.Enabled = true;
                }
                else
                {
                    ControllerDisable.Form_Repeat_Disable();
                }
            }


            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_A);
            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_B);
            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_C);
            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_D);
            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_E);
            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_F);
            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_G);
            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_H);
            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_I);
            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_J);
            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_K);
            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_L);
            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_M);
            FormPrint.CBB_suik_DropDownClosed(combo_repeat_suik_gubun_N);

            this.ActiveControl = combo_repeat_condition_N;
            Form1.음소거 = Properties.Settings.Default.CB_음소거;

        }

        public static void 가이드확인_반복매매저장()
        {
            if (Properties.Settings.Default.CB_가이드매매)
            {
                try
                {
                    if (Form1.form1.account_comboBox.SelectedIndex > -1)
                    {
                        if (form.combo_repeat_use_condition_A.SelectedIndex == 0) form.combo_repeat_condition_A.SelectedItem = "";
                        if (form.combo_repeat_use_condition_B.SelectedIndex == 0) form.combo_repeat_condition_B.SelectedItem = "";
                        if (form.combo_repeat_use_condition_C.SelectedIndex == 0) form.combo_repeat_condition_C.SelectedItem = "";
                        if (form.combo_repeat_use_condition_D.SelectedIndex == 0) form.combo_repeat_condition_D.SelectedItem = "";
                        if (form.combo_repeat_use_condition_E.SelectedIndex == 0) form.combo_repeat_condition_E.SelectedItem = "";
                        if (form.combo_repeat_use_condition_F.SelectedIndex == 0) form.combo_repeat_condition_F.SelectedItem = "";
                        if (form.combo_repeat_use_condition_G.SelectedIndex == 0) form.combo_repeat_condition_G.SelectedItem = "";
                        if (form.combo_repeat_use_condition_H.SelectedIndex == 0) form.combo_repeat_condition_H.SelectedItem = "";
                        if (form.combo_repeat_use_condition_I.SelectedIndex == 0) form.combo_repeat_condition_I.SelectedItem = "";
                        if (form.combo_repeat_use_condition_J.SelectedIndex == 0) form.combo_repeat_condition_J.SelectedItem = "";
                        if (form.combo_repeat_use_condition_K.SelectedIndex == 0) form.combo_repeat_condition_K.SelectedItem = "";
                        if (form.combo_repeat_use_condition_L.SelectedIndex == 0) form.combo_repeat_condition_L.SelectedItem = "";
                        if (form.combo_repeat_use_condition_M.SelectedIndex == 0) form.combo_repeat_condition_M.SelectedItem = "";
                        if (form.combo_repeat_use_condition_N.SelectedIndex == 0) form.combo_repeat_condition_N.SelectedItem = "";

                        Properties.Settings.Default.combo_repeat_use_condition_A = form.combo_repeat_use_condition_A.SelectedIndex;
                        Properties.Settings.Default.combo_repeat_use_condition_B = form.combo_repeat_use_condition_B.SelectedIndex;
                        Properties.Settings.Default.combo_repeat_use_condition_C = form.combo_repeat_use_condition_C.SelectedIndex;
                        Properties.Settings.Default.combo_repeat_use_condition_D = form.combo_repeat_use_condition_D.SelectedIndex;
                        Properties.Settings.Default.combo_repeat_use_condition_E = form.combo_repeat_use_condition_E.SelectedIndex;
                        Properties.Settings.Default.combo_repeat_use_condition_F = form.combo_repeat_use_condition_F.SelectedIndex;
                        Properties.Settings.Default.combo_repeat_use_condition_G = form.combo_repeat_use_condition_G.SelectedIndex;
                        Properties.Settings.Default.combo_repeat_use_condition_H = form.combo_repeat_use_condition_H.SelectedIndex;
                        Properties.Settings.Default.combo_repeat_use_condition_I = form.combo_repeat_use_condition_I.SelectedIndex;
                        Properties.Settings.Default.combo_repeat_use_condition_J = form.combo_repeat_use_condition_J.SelectedIndex;
                        Properties.Settings.Default.combo_repeat_use_condition_K = form.combo_repeat_use_condition_K.SelectedIndex;
                        Properties.Settings.Default.combo_repeat_use_condition_L = form.combo_repeat_use_condition_L.SelectedIndex;
                        Properties.Settings.Default.combo_repeat_use_condition_M = form.combo_repeat_use_condition_M.SelectedIndex;
                        Properties.Settings.Default.combo_repeat_use_condition_N = form.combo_repeat_use_condition_N.SelectedIndex;

                        if (Properties.Settings.Default.combo_repeat_use_condition_A == 0) Form1.Repeat_condition_List_A.Clear();
                        if (Properties.Settings.Default.combo_repeat_use_condition_B == 0) Form1.Repeat_condition_List_B.Clear();
                        if (Properties.Settings.Default.combo_repeat_use_condition_C == 0) Form1.Repeat_condition_List_C.Clear();
                        if (Properties.Settings.Default.combo_repeat_use_condition_D == 0) Form1.Repeat_condition_List_D.Clear();
                        if (Properties.Settings.Default.combo_repeat_use_condition_E == 0) Form1.Repeat_condition_List_E.Clear();
                        if (Properties.Settings.Default.combo_repeat_use_condition_F == 0) Form1.Repeat_condition_List_F.Clear();
                        if (Properties.Settings.Default.combo_repeat_use_condition_G == 0) Form1.Repeat_condition_List_G.Clear();
                        if (Properties.Settings.Default.combo_repeat_use_condition_H == 0) Form1.Repeat_condition_List_H.Clear();
                        if (Properties.Settings.Default.combo_repeat_use_condition_I == 0) Form1.Repeat_condition_List_I.Clear();
                        if (Properties.Settings.Default.combo_repeat_use_condition_J == 0) Form1.Repeat_condition_List_J.Clear();
                        if (Properties.Settings.Default.combo_repeat_use_condition_K == 0) Form1.Repeat_condition_List_K.Clear();
                        if (Properties.Settings.Default.combo_repeat_use_condition_L == 0) Form1.Repeat_condition_List_L.Clear();
                        if (Properties.Settings.Default.combo_repeat_use_condition_M == 0) Form1.Repeat_condition_List_M.Clear();
                        if (Properties.Settings.Default.combo_repeat_use_condition_N == 0) Form1.Repeat_condition_List_N.Clear();

                        Properties.Settings.Default.CB_repeat_use_A = form.CB_repeat_use_A.Checked;
                        Properties.Settings.Default.CB_repeat_use_B = form.CB_repeat_use_B.Checked;
                        Properties.Settings.Default.CB_repeat_use_C = form.CB_repeat_use_C.Checked;
                        Properties.Settings.Default.CB_repeat_use_D = form.CB_repeat_use_D.Checked;
                        Properties.Settings.Default.CB_repeat_use_E = form.CB_repeat_use_E.Checked;
                        Properties.Settings.Default.CB_repeat_use_F = form.CB_repeat_use_F.Checked;
                        Properties.Settings.Default.CB_repeat_use_G = form.CB_repeat_use_G.Checked;
                        Properties.Settings.Default.CB_repeat_use_H = form.CB_repeat_use_H.Checked;
                        Properties.Settings.Default.CB_repeat_use_I = form.CB_repeat_use_I.Checked;
                        Properties.Settings.Default.CB_repeat_use_J = form.CB_repeat_use_J.Checked;
                        Properties.Settings.Default.CB_repeat_use_K = form.CB_repeat_use_K.Checked;
                        Properties.Settings.Default.CB_repeat_use_L = form.CB_repeat_use_L.Checked;
                        Properties.Settings.Default.CB_repeat_use_M = form.CB_repeat_use_M.Checked;
                        Properties.Settings.Default.CB_repeat_use_N = form.CB_repeat_use_N.Checked;

                        Console.WriteLine("반복매매 저장 - 가이드매매일때");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
                }
            }
            else
            {
                반복매매_저장();
            }
        }

        public static void 반복매매_저장() // 반복매매 저장
        {
            ComboBox combo_Jumun = form.combo_repeat_jumun_A;
            ComboBox combo_Cancel = form.combo_repeat_Cancel_A;
            MaskedTextBox MTB = form.MTB_repeat_repeat_A;
            TextBox TB = form.TB_repeat_value_A;
            cancel_sell_reset();

            combo_Jumun = form.combo_repeat_jumun_B;
            combo_Cancel = form.combo_repeat_Cancel_B;
            MTB = form.MTB_repeat_repeat_B;
            TB = form.TB_repeat_value_B;
            cancel_sell_reset();

            combo_Jumun = form.combo_repeat_jumun_C;
            combo_Cancel = form.combo_repeat_Cancel_C;
            MTB = form.MTB_repeat_repeat_C;
            TB = form.TB_repeat_value_C;
            cancel_sell_reset();

            combo_Jumun = form.combo_repeat_jumun_D;
            combo_Cancel = form.combo_repeat_Cancel_D;
            MTB = form.MTB_repeat_repeat_D;
            TB = form.TB_repeat_value_D;
            cancel_sell_reset();

            combo_Jumun = form.combo_repeat_jumun_E;
            combo_Cancel = form.combo_repeat_Cancel_E;
            MTB = form.MTB_repeat_repeat_E;
            TB = form.TB_repeat_value_E;
            cancel_sell_reset();

            combo_Jumun = form.combo_repeat_jumun_F;
            combo_Cancel = form.combo_repeat_Cancel_F;
            MTB = form.MTB_repeat_repeat_F;
            TB = form.TB_repeat_value_F;
            cancel_sell_reset();

            combo_Jumun = form.combo_repeat_jumun_G;
            combo_Cancel = form.combo_repeat_Cancel_G;
            MTB = form.MTB_repeat_repeat_G;
            TB = form.TB_repeat_value_G;
            cancel_sell_reset();

            combo_Jumun = form.combo_repeat_jumun_H;
            combo_Cancel = form.combo_repeat_Cancel_H;
            MTB = form.MTB_repeat_repeat_H;
            TB = form.TB_repeat_value_H;
            cancel_sell_reset();

            combo_Jumun = form.combo_repeat_jumun_I;
            combo_Cancel = form.combo_repeat_Cancel_I;
            MTB = form.MTB_repeat_repeat_I;
            TB = form.TB_repeat_value_I;
            cancel_sell_reset();

            combo_Jumun = form.combo_repeat_jumun_J;
            combo_Cancel = form.combo_repeat_Cancel_J;
            MTB = form.MTB_repeat_repeat_J;
            TB = form.TB_repeat_value_J;
            cancel_sell_reset();

            combo_Jumun = form.combo_repeat_jumun_K;
            combo_Cancel = form.combo_repeat_Cancel_K;
            MTB = form.MTB_repeat_repeat_K;
            TB = form.TB_repeat_value_K;
            cancel_sell_reset();

            combo_Jumun = form.combo_repeat_jumun_L;
            combo_Cancel = form.combo_repeat_Cancel_L;
            MTB = form.MTB_repeat_repeat_L;
            TB = form.TB_repeat_value_L;
            cancel_sell_reset();

            combo_Jumun = form.combo_repeat_jumun_M;
            combo_Cancel = form.combo_repeat_Cancel_M;
            MTB = form.MTB_repeat_repeat_M;
            TB = form.TB_repeat_value_M;
            cancel_sell_reset();

            combo_Jumun = form.combo_repeat_jumun_N;
            combo_Cancel = form.combo_repeat_Cancel_N;
            MTB = form.MTB_repeat_repeat_N;
            TB = form.TB_repeat_value_N;
            cancel_sell_reset();

            void cancel_sell_reset()
            {
                if (combo_Jumun.SelectedIndex > 3)
                {
                    combo_Cancel.SelectedIndex = 0;
                    MTB.Text = "0";
                    TB.Text = "0";
                }
            }

            try
            {
                Properties.Settings.Default.combo_repeat_jumun_A = form.combo_repeat_jumun_A.SelectedIndex;
                Properties.Settings.Default.combo_repeat_jumun_B = form.combo_repeat_jumun_B.SelectedIndex;
                Properties.Settings.Default.combo_repeat_jumun_C = form.combo_repeat_jumun_C.SelectedIndex;
                Properties.Settings.Default.combo_repeat_jumun_D = form.combo_repeat_jumun_D.SelectedIndex;
                Properties.Settings.Default.combo_repeat_jumun_E = form.combo_repeat_jumun_E.SelectedIndex;
                Properties.Settings.Default.combo_repeat_jumun_F = form.combo_repeat_jumun_F.SelectedIndex;
                Properties.Settings.Default.combo_repeat_jumun_G = form.combo_repeat_jumun_G.SelectedIndex;
                Properties.Settings.Default.combo_repeat_jumun_H = form.combo_repeat_jumun_H.SelectedIndex;
                Properties.Settings.Default.combo_repeat_jumun_I = form.combo_repeat_jumun_I.SelectedIndex;
                Properties.Settings.Default.combo_repeat_jumun_J = form.combo_repeat_jumun_J.SelectedIndex;
                Properties.Settings.Default.combo_repeat_jumun_K = form.combo_repeat_jumun_K.SelectedIndex;
                Properties.Settings.Default.combo_repeat_jumun_L = form.combo_repeat_jumun_L.SelectedIndex;
                Properties.Settings.Default.combo_repeat_jumun_M = form.combo_repeat_jumun_M.SelectedIndex;
                Properties.Settings.Default.combo_repeat_jumun_N = form.combo_repeat_jumun_N.SelectedIndex;
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MT_repeat_time_start_A.Text, out int _start_A);
                int.TryParse(form.MT_repeat_time_start_B.Text, out int _start_B);
                int.TryParse(form.MT_repeat_time_start_C.Text, out int _start_C);
                int.TryParse(form.MT_repeat_time_start_D.Text, out int _start_D);
                int.TryParse(form.MT_repeat_time_start_E.Text, out int _start_E);
                int.TryParse(form.MT_repeat_time_start_F.Text, out int _start_F);
                int.TryParse(form.MT_repeat_time_start_G.Text, out int _start_G);
                int.TryParse(form.MT_repeat_time_start_H.Text, out int _start_H);
                int.TryParse(form.MT_repeat_time_start_I.Text, out int _start_I);
                int.TryParse(form.MT_repeat_time_start_J.Text, out int _start_J);
                int.TryParse(form.MT_repeat_time_start_K.Text, out int _start_K);
                int.TryParse(form.MT_repeat_time_start_L.Text, out int _start_L);
                int.TryParse(form.MT_repeat_time_start_M.Text, out int _start_M);
                int.TryParse(form.MT_repeat_time_start_N.Text, out int _start_N);

                int start_A = GET.start_stop_time(true, _start_A);
                int start_B = GET.start_stop_time(true, _start_B);
                int start_C = GET.start_stop_time(true, _start_C);
                int start_D = GET.start_stop_time(true, _start_D);
                int start_E = GET.start_stop_time(true, _start_E);
                int start_F = GET.start_stop_time(true, _start_F);
                int start_G = GET.start_stop_time(true, _start_G);
                int start_H = GET.start_stop_time(true, _start_H);
                int start_I = GET.start_stop_time(true, _start_I);
                int start_J = GET.start_stop_time(true, _start_J);
                int start_K = GET.start_stop_time(true, _start_K);
                int start_L = GET.start_stop_time(true, _start_L);
                int start_M = GET.start_stop_time(true, _start_M);
                int start_N = GET.start_stop_time(true, _start_N);

                Properties.Settings.Default.MT_repeat_time_start_A = start_A;
                Properties.Settings.Default.MT_repeat_time_start_B = start_B;
                Properties.Settings.Default.MT_repeat_time_start_C = start_C;
                Properties.Settings.Default.MT_repeat_time_start_D = start_D;
                Properties.Settings.Default.MT_repeat_time_start_E = start_E;
                Properties.Settings.Default.MT_repeat_time_start_F = start_F;
                Properties.Settings.Default.MT_repeat_time_start_G = start_G;
                Properties.Settings.Default.MT_repeat_time_start_H = start_H;
                Properties.Settings.Default.MT_repeat_time_start_I = start_I;
                Properties.Settings.Default.MT_repeat_time_start_J = start_J;
                Properties.Settings.Default.MT_repeat_time_start_K = start_K;
                Properties.Settings.Default.MT_repeat_time_start_L = start_L;
                Properties.Settings.Default.MT_repeat_time_start_M = start_M;
                Properties.Settings.Default.MT_repeat_time_start_N = start_N;

                form.MT_repeat_time_start_A.Text = start_A.ToString();
                form.MT_repeat_time_start_B.Text = start_B.ToString();
                form.MT_repeat_time_start_C.Text = start_C.ToString();
                form.MT_repeat_time_start_D.Text = start_D.ToString();
                form.MT_repeat_time_start_E.Text = start_E.ToString();
                form.MT_repeat_time_start_F.Text = start_F.ToString();
                form.MT_repeat_time_start_G.Text = start_G.ToString();
                form.MT_repeat_time_start_H.Text = start_H.ToString();
                form.MT_repeat_time_start_I.Text = start_I.ToString();
                form.MT_repeat_time_start_J.Text = start_J.ToString();
                form.MT_repeat_time_start_K.Text = start_K.ToString();
                form.MT_repeat_time_start_L.Text = start_L.ToString();
                form.MT_repeat_time_start_M.Text = start_M.ToString();
                form.MT_repeat_time_start_N.Text = start_N.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }
            try
            {
                int.TryParse(form.MT_repeat_time_end_A.Text, out int _end_A);
                int.TryParse(form.MT_repeat_time_end_B.Text, out int _end_B);
                int.TryParse(form.MT_repeat_time_end_C.Text, out int _end_C);
                int.TryParse(form.MT_repeat_time_end_D.Text, out int _end_D);
                int.TryParse(form.MT_repeat_time_end_E.Text, out int _end_E);
                int.TryParse(form.MT_repeat_time_end_F.Text, out int _end_F);
                int.TryParse(form.MT_repeat_time_end_G.Text, out int _end_G);
                int.TryParse(form.MT_repeat_time_end_H.Text, out int _end_H);
                int.TryParse(form.MT_repeat_time_end_I.Text, out int _end_I);
                int.TryParse(form.MT_repeat_time_end_J.Text, out int _end_J);
                int.TryParse(form.MT_repeat_time_end_K.Text, out int _end_K);
                int.TryParse(form.MT_repeat_time_end_L.Text, out int _end_L);
                int.TryParse(form.MT_repeat_time_end_M.Text, out int _end_M);
                int.TryParse(form.MT_repeat_time_end_N.Text, out int _end_N);

                int end_A = GET.start_stop_time(false, _end_A);
                int end_B = GET.start_stop_time(false, _end_B);
                int end_C = GET.start_stop_time(false, _end_C);
                int end_D = GET.start_stop_time(false, _end_D);
                int end_E = GET.start_stop_time(false, _end_E);
                int end_F = GET.start_stop_time(false, _end_F);
                int end_G = GET.start_stop_time(false, _end_G);
                int end_H = GET.start_stop_time(false, _end_H);
                int end_I = GET.start_stop_time(false, _end_I);
                int end_J = GET.start_stop_time(false, _end_J);
                int end_K = GET.start_stop_time(false, _end_K);
                int end_L = GET.start_stop_time(false, _end_L);
                int end_M = GET.start_stop_time(false, _end_M);
                int end_N = GET.start_stop_time(false, _end_N);

                Properties.Settings.Default.MT_repeat_time_end_A = end_A;
                Properties.Settings.Default.MT_repeat_time_end_B = end_B;
                Properties.Settings.Default.MT_repeat_time_end_C = end_C;
                Properties.Settings.Default.MT_repeat_time_end_D = end_D;
                Properties.Settings.Default.MT_repeat_time_end_E = end_E;
                Properties.Settings.Default.MT_repeat_time_end_F = end_F;
                Properties.Settings.Default.MT_repeat_time_end_G = end_G;
                Properties.Settings.Default.MT_repeat_time_end_H = end_H;
                Properties.Settings.Default.MT_repeat_time_end_I = end_I;
                Properties.Settings.Default.MT_repeat_time_end_J = end_J;
                Properties.Settings.Default.MT_repeat_time_end_K = end_K;
                Properties.Settings.Default.MT_repeat_time_end_L = end_L;
                Properties.Settings.Default.MT_repeat_time_end_M = end_M;
                Properties.Settings.Default.MT_repeat_time_end_N = end_N;

                form.MT_repeat_time_end_A.Text = end_A.ToString();
                form.MT_repeat_time_end_B.Text = end_B.ToString();
                form.MT_repeat_time_end_C.Text = end_C.ToString();
                form.MT_repeat_time_end_D.Text = end_D.ToString();
                form.MT_repeat_time_end_E.Text = end_E.ToString();
                form.MT_repeat_time_end_F.Text = end_F.ToString();
                form.MT_repeat_time_end_G.Text = end_G.ToString();
                form.MT_repeat_time_end_H.Text = end_H.ToString();
                form.MT_repeat_time_end_I.Text = end_I.ToString();
                form.MT_repeat_time_end_J.Text = end_J.ToString();
                form.MT_repeat_time_end_K.Text = end_K.ToString();
                form.MT_repeat_time_end_L.Text = end_L.ToString();
                form.MT_repeat_time_end_M.Text = end_M.ToString();
                form.MT_repeat_time_end_N.Text = end_N.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_repeat_delay_A.Text, out int delay_A);
                int.TryParse(form.MTB_repeat_delay_B.Text, out int delay_B);
                int.TryParse(form.MTB_repeat_delay_C.Text, out int delay_C);
                int.TryParse(form.MTB_repeat_delay_D.Text, out int delay_D);
                int.TryParse(form.MTB_repeat_delay_E.Text, out int delay_E);
                int.TryParse(form.MTB_repeat_delay_F.Text, out int delay_F);
                int.TryParse(form.MTB_repeat_delay_G.Text, out int delay_G);
                int.TryParse(form.MTB_repeat_delay_H.Text, out int delay_H);
                int.TryParse(form.MTB_repeat_delay_I.Text, out int delay_I);
                int.TryParse(form.MTB_repeat_delay_J.Text, out int delay_J);
                int.TryParse(form.MTB_repeat_delay_K.Text, out int delay_K);
                int.TryParse(form.MTB_repeat_delay_L.Text, out int delay_L);
                int.TryParse(form.MTB_repeat_delay_M.Text, out int delay_M);
                int.TryParse(form.MTB_repeat_delay_N.Text, out int delay_N);

                Properties.Settings.Default.MTB_repeat_delay_A = delay_A;
                Properties.Settings.Default.MTB_repeat_delay_B = delay_B;
                Properties.Settings.Default.MTB_repeat_delay_C = delay_C;
                Properties.Settings.Default.MTB_repeat_delay_D = delay_D;
                Properties.Settings.Default.MTB_repeat_delay_E = delay_E;
                Properties.Settings.Default.MTB_repeat_delay_F = delay_F;
                Properties.Settings.Default.MTB_repeat_delay_G = delay_G;
                Properties.Settings.Default.MTB_repeat_delay_H = delay_H;
                Properties.Settings.Default.MTB_repeat_delay_I = delay_I;
                Properties.Settings.Default.MTB_repeat_delay_J = delay_J;
                Properties.Settings.Default.MTB_repeat_delay_K = delay_K;
                Properties.Settings.Default.MTB_repeat_delay_L = delay_L;
                Properties.Settings.Default.MTB_repeat_delay_M = delay_M;
                Properties.Settings.Default.MTB_repeat_delay_N = delay_N;

                form.MTB_repeat_delay_A.Text = delay_A.ToString();
                form.MTB_repeat_delay_B.Text = delay_B.ToString();
                form.MTB_repeat_delay_C.Text = delay_C.ToString();
                form.MTB_repeat_delay_D.Text = delay_D.ToString();
                form.MTB_repeat_delay_E.Text = delay_E.ToString();
                form.MTB_repeat_delay_F.Text = delay_F.ToString();
                form.MTB_repeat_delay_G.Text = delay_G.ToString();
                form.MTB_repeat_delay_H.Text = delay_H.ToString();
                form.MTB_repeat_delay_I.Text = delay_I.ToString();
                form.MTB_repeat_delay_J.Text = delay_J.ToString();
                form.MTB_repeat_delay_K.Text = delay_K.ToString();
                form.MTB_repeat_delay_L.Text = delay_L.ToString();
                form.MTB_repeat_delay_M.Text = delay_M.ToString();
                form.MTB_repeat_delay_N.Text = delay_N.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_repeat_suik_1_A.Text, out double suik_1_A);
                double.TryParse(form.TB_repeat_suik_1_B.Text, out double suik_1_B);
                double.TryParse(form.TB_repeat_suik_1_C.Text, out double suik_1_C);
                double.TryParse(form.TB_repeat_suik_1_D.Text, out double suik_1_D);
                double.TryParse(form.TB_repeat_suik_1_E.Text, out double suik_1_E);
                double.TryParse(form.TB_repeat_suik_1_F.Text, out double suik_1_F);
                double.TryParse(form.TB_repeat_suik_1_G.Text, out double suik_1_G);
                double.TryParse(form.TB_repeat_suik_1_H.Text, out double suik_1_H);
                double.TryParse(form.TB_repeat_suik_1_I.Text, out double suik_1_I);
                double.TryParse(form.TB_repeat_suik_1_J.Text, out double suik_1_J);
                double.TryParse(form.TB_repeat_suik_1_K.Text, out double suik_1_K);
                double.TryParse(form.TB_repeat_suik_1_L.Text, out double suik_1_L);
                double.TryParse(form.TB_repeat_suik_1_M.Text, out double suik_1_M);
                double.TryParse(form.TB_repeat_suik_1_N.Text, out double suik_1_N);

                Properties.Settings.Default.TB_repeat_suik_1_A = suik_1_A;
                Properties.Settings.Default.TB_repeat_suik_1_B = suik_1_B;
                Properties.Settings.Default.TB_repeat_suik_1_C = suik_1_C;
                Properties.Settings.Default.TB_repeat_suik_1_D = suik_1_D;
                Properties.Settings.Default.TB_repeat_suik_1_E = suik_1_E;
                Properties.Settings.Default.TB_repeat_suik_1_F = suik_1_F;
                Properties.Settings.Default.TB_repeat_suik_1_G = suik_1_G;
                Properties.Settings.Default.TB_repeat_suik_1_H = suik_1_H;
                Properties.Settings.Default.TB_repeat_suik_1_I = suik_1_I;
                Properties.Settings.Default.TB_repeat_suik_1_J = suik_1_J;
                Properties.Settings.Default.TB_repeat_suik_1_K = suik_1_K;
                Properties.Settings.Default.TB_repeat_suik_1_L = suik_1_L;
                Properties.Settings.Default.TB_repeat_suik_1_M = suik_1_M;
                Properties.Settings.Default.TB_repeat_suik_1_N = suik_1_N;

                form.TB_repeat_suik_1_A.Text = suik_1_A.ToString();
                form.TB_repeat_suik_1_B.Text = suik_1_B.ToString();
                form.TB_repeat_suik_1_C.Text = suik_1_C.ToString();
                form.TB_repeat_suik_1_D.Text = suik_1_D.ToString();
                form.TB_repeat_suik_1_E.Text = suik_1_E.ToString();
                form.TB_repeat_suik_1_F.Text = suik_1_F.ToString();
                form.TB_repeat_suik_1_G.Text = suik_1_G.ToString();
                form.TB_repeat_suik_1_H.Text = suik_1_H.ToString();
                form.TB_repeat_suik_1_I.Text = suik_1_I.ToString();
                form.TB_repeat_suik_1_J.Text = suik_1_J.ToString();
                form.TB_repeat_suik_1_K.Text = suik_1_K.ToString();
                form.TB_repeat_suik_1_L.Text = suik_1_L.ToString();
                form.TB_repeat_suik_1_M.Text = suik_1_M.ToString();
                form.TB_repeat_suik_1_N.Text = suik_1_N.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_repeat_suik_2_A.Text, out double suik_2_A);
                double.TryParse(form.TB_repeat_suik_2_B.Text, out double suik_2_B);
                double.TryParse(form.TB_repeat_suik_2_C.Text, out double suik_2_C);
                double.TryParse(form.TB_repeat_suik_2_D.Text, out double suik_2_D);
                double.TryParse(form.TB_repeat_suik_2_E.Text, out double suik_2_E);
                double.TryParse(form.TB_repeat_suik_2_F.Text, out double suik_2_F);
                double.TryParse(form.TB_repeat_suik_2_G.Text, out double suik_2_G);
                double.TryParse(form.TB_repeat_suik_2_H.Text, out double suik_2_H);
                double.TryParse(form.TB_repeat_suik_2_I.Text, out double suik_2_I);
                double.TryParse(form.TB_repeat_suik_2_J.Text, out double suik_2_J);
                double.TryParse(form.TB_repeat_suik_2_K.Text, out double suik_2_K);
                double.TryParse(form.TB_repeat_suik_2_L.Text, out double suik_2_L);
                double.TryParse(form.TB_repeat_suik_2_M.Text, out double suik_2_M);
                double.TryParse(form.TB_repeat_suik_2_N.Text, out double suik_2_N);

                Properties.Settings.Default.TB_repeat_suik_2_A = suik_2_A;
                Properties.Settings.Default.TB_repeat_suik_2_B = suik_2_B;
                Properties.Settings.Default.TB_repeat_suik_2_C = suik_2_C;
                Properties.Settings.Default.TB_repeat_suik_2_D = suik_2_D;
                Properties.Settings.Default.TB_repeat_suik_2_E = suik_2_E;
                Properties.Settings.Default.TB_repeat_suik_2_F = suik_2_F;
                Properties.Settings.Default.TB_repeat_suik_2_G = suik_2_G;
                Properties.Settings.Default.TB_repeat_suik_2_H = suik_2_H;
                Properties.Settings.Default.TB_repeat_suik_2_I = suik_2_I;
                Properties.Settings.Default.TB_repeat_suik_2_J = suik_2_J;
                Properties.Settings.Default.TB_repeat_suik_2_K = suik_2_K;
                Properties.Settings.Default.TB_repeat_suik_2_L = suik_2_L;
                Properties.Settings.Default.TB_repeat_suik_2_M = suik_2_M;
                Properties.Settings.Default.TB_repeat_suik_2_N = suik_2_N;

                form.TB_repeat_suik_2_A.Text = suik_2_A.ToString();
                form.TB_repeat_suik_2_B.Text = suik_2_B.ToString();
                form.TB_repeat_suik_2_C.Text = suik_2_C.ToString();
                form.TB_repeat_suik_2_D.Text = suik_2_D.ToString();
                form.TB_repeat_suik_2_E.Text = suik_2_E.ToString();
                form.TB_repeat_suik_2_F.Text = suik_2_F.ToString();
                form.TB_repeat_suik_2_G.Text = suik_2_G.ToString();
                form.TB_repeat_suik_2_H.Text = suik_2_H.ToString();
                form.TB_repeat_suik_2_I.Text = suik_2_I.ToString();
                form.TB_repeat_suik_2_J.Text = suik_2_J.ToString();
                form.TB_repeat_suik_2_K.Text = suik_2_K.ToString();
                form.TB_repeat_suik_2_L.Text = suik_2_L.ToString();
                form.TB_repeat_suik_2_M.Text = suik_2_M.ToString();
                form.TB_repeat_suik_2_N.Text = suik_2_N.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_repeat_sell_ratio_A.Text, out double Ratio_A);
                double.TryParse(form.TB_repeat_sell_ratio_B.Text, out double Ratio_B);
                double.TryParse(form.TB_repeat_sell_ratio_C.Text, out double Ratio_C);
                double.TryParse(form.TB_repeat_sell_ratio_D.Text, out double Ratio_D);
                double.TryParse(form.TB_repeat_sell_ratio_E.Text, out double Ratio_E);
                double.TryParse(form.TB_repeat_sell_ratio_F.Text, out double Ratio_F);
                double.TryParse(form.TB_repeat_sell_ratio_G.Text, out double Ratio_G);
                double.TryParse(form.TB_repeat_sell_ratio_H.Text, out double Ratio_H);
                double.TryParse(form.TB_repeat_sell_ratio_I.Text, out double Ratio_I);
                double.TryParse(form.TB_repeat_sell_ratio_J.Text, out double Ratio_J);
                double.TryParse(form.TB_repeat_sell_ratio_K.Text, out double Ratio_K);
                double.TryParse(form.TB_repeat_sell_ratio_L.Text, out double Ratio_L);
                double.TryParse(form.TB_repeat_sell_ratio_M.Text, out double Ratio_M);
                double.TryParse(form.TB_repeat_sell_ratio_N.Text, out double Ratio_N);

                if (Ratio_A == 0) Ratio_A = 1;
                if (Ratio_B == 0) Ratio_B = 1;
                if (Ratio_C == 0) Ratio_C = 1;
                if (Ratio_D == 0) Ratio_D = 1;
                if (Ratio_E == 0) Ratio_E = 1;
                if (Ratio_F == 0) Ratio_F = 1;
                if (Ratio_G == 0) Ratio_G = 1;
                if (Ratio_H == 0) Ratio_H = 1;
                if (Ratio_I == 0) Ratio_I = 1;
                if (Ratio_J == 0) Ratio_J = 1;
                if (Ratio_K == 0) Ratio_K = 1;
                if (Ratio_L == 0) Ratio_L = 1;
                if (Ratio_M == 0) Ratio_M = 1;
                if (Ratio_N == 0) Ratio_N = 1;

                Properties.Settings.Default.TB_repeat_sell_ratio_A = Math.Abs(Ratio_A);
                Properties.Settings.Default.TB_repeat_sell_ratio_B = Math.Abs(Ratio_B);
                Properties.Settings.Default.TB_repeat_sell_ratio_C = Math.Abs(Ratio_C);
                Properties.Settings.Default.TB_repeat_sell_ratio_D = Math.Abs(Ratio_D);
                Properties.Settings.Default.TB_repeat_sell_ratio_E = Math.Abs(Ratio_E);
                Properties.Settings.Default.TB_repeat_sell_ratio_F = Math.Abs(Ratio_F);
                Properties.Settings.Default.TB_repeat_sell_ratio_G = Math.Abs(Ratio_G);
                Properties.Settings.Default.TB_repeat_sell_ratio_H = Math.Abs(Ratio_H);
                Properties.Settings.Default.TB_repeat_sell_ratio_I = Math.Abs(Ratio_I);
                Properties.Settings.Default.TB_repeat_sell_ratio_J = Math.Abs(Ratio_J);
                Properties.Settings.Default.TB_repeat_sell_ratio_K = Math.Abs(Ratio_K);
                Properties.Settings.Default.TB_repeat_sell_ratio_L = Math.Abs(Ratio_L);
                Properties.Settings.Default.TB_repeat_sell_ratio_M = Math.Abs(Ratio_M);
                Properties.Settings.Default.TB_repeat_sell_ratio_N = Math.Abs(Ratio_N);

                form.TB_repeat_sell_ratio_A.Text = Properties.Settings.Default.TB_repeat_sell_ratio_A.ToString();
                form.TB_repeat_sell_ratio_B.Text = Properties.Settings.Default.TB_repeat_sell_ratio_B.ToString();
                form.TB_repeat_sell_ratio_C.Text = Properties.Settings.Default.TB_repeat_sell_ratio_C.ToString();
                form.TB_repeat_sell_ratio_D.Text = Properties.Settings.Default.TB_repeat_sell_ratio_D.ToString();
                form.TB_repeat_sell_ratio_E.Text = Properties.Settings.Default.TB_repeat_sell_ratio_E.ToString();
                form.TB_repeat_sell_ratio_F.Text = Properties.Settings.Default.TB_repeat_sell_ratio_F.ToString();
                form.TB_repeat_sell_ratio_G.Text = Properties.Settings.Default.TB_repeat_sell_ratio_G.ToString();
                form.TB_repeat_sell_ratio_H.Text = Properties.Settings.Default.TB_repeat_sell_ratio_H.ToString();
                form.TB_repeat_sell_ratio_I.Text = Properties.Settings.Default.TB_repeat_sell_ratio_I.ToString();
                form.TB_repeat_sell_ratio_J.Text = Properties.Settings.Default.TB_repeat_sell_ratio_J.ToString();
                form.TB_repeat_sell_ratio_K.Text = Properties.Settings.Default.TB_repeat_sell_ratio_K.ToString();
                form.TB_repeat_sell_ratio_L.Text = Properties.Settings.Default.TB_repeat_sell_ratio_L.ToString();
                form.TB_repeat_sell_ratio_M.Text = Properties.Settings.Default.TB_repeat_sell_ratio_M.ToString();
                form.TB_repeat_sell_ratio_N.Text = Properties.Settings.Default.TB_repeat_sell_ratio_N.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_repeat_매입금_A.Text, out double repeat_매입금_A);
                double.TryParse(form.TB_repeat_매입금_B.Text, out double repeat_매입금_B);
                double.TryParse(form.TB_repeat_매입금_C.Text, out double repeat_매입금_C);
                double.TryParse(form.TB_repeat_매입금_D.Text, out double repeat_매입금_D);
                double.TryParse(form.TB_repeat_매입금_E.Text, out double repeat_매입금_E);
                double.TryParse(form.TB_repeat_매입금_F.Text, out double repeat_매입금_F);
                double.TryParse(form.TB_repeat_매입금_G.Text, out double repeat_매입금_G);
                double.TryParse(form.TB_repeat_매입금_H.Text, out double repeat_매입금_H);
                double.TryParse(form.TB_repeat_매입금_I.Text, out double repeat_매입금_I);
                double.TryParse(form.TB_repeat_매입금_J.Text, out double repeat_매입금_J);
                double.TryParse(form.TB_repeat_매입금_K.Text, out double repeat_매입금_K);
                double.TryParse(form.TB_repeat_매입금_L.Text, out double repeat_매입금_L);
                double.TryParse(form.TB_repeat_매입금_M.Text, out double repeat_매입금_M);
                double.TryParse(form.TB_repeat_매입금_N.Text, out double repeat_매입금_N);

                Properties.Settings.Default.TB_repeat_매입금_A = Math.Abs(repeat_매입금_A);
                Properties.Settings.Default.TB_repeat_매입금_B = Math.Abs(repeat_매입금_B);
                Properties.Settings.Default.TB_repeat_매입금_C = Math.Abs(repeat_매입금_C);
                Properties.Settings.Default.TB_repeat_매입금_D = Math.Abs(repeat_매입금_D);
                Properties.Settings.Default.TB_repeat_매입금_E = Math.Abs(repeat_매입금_E);
                Properties.Settings.Default.TB_repeat_매입금_F = Math.Abs(repeat_매입금_F);
                Properties.Settings.Default.TB_repeat_매입금_G = Math.Abs(repeat_매입금_G);
                Properties.Settings.Default.TB_repeat_매입금_H = Math.Abs(repeat_매입금_H);
                Properties.Settings.Default.TB_repeat_매입금_I = Math.Abs(repeat_매입금_I);
                Properties.Settings.Default.TB_repeat_매입금_J = Math.Abs(repeat_매입금_J);
                Properties.Settings.Default.TB_repeat_매입금_K = Math.Abs(repeat_매입금_K);
                Properties.Settings.Default.TB_repeat_매입금_L = Math.Abs(repeat_매입금_L);
                Properties.Settings.Default.TB_repeat_매입금_M = Math.Abs(repeat_매입금_M);
                Properties.Settings.Default.TB_repeat_매입금_N = Math.Abs(repeat_매입금_N);

                form.TB_repeat_매입금_A.Text = Properties.Settings.Default.TB_repeat_매입금_A.ToString();
                form.TB_repeat_매입금_B.Text = Properties.Settings.Default.TB_repeat_매입금_B.ToString();
                form.TB_repeat_매입금_C.Text = Properties.Settings.Default.TB_repeat_매입금_C.ToString();
                form.TB_repeat_매입금_D.Text = Properties.Settings.Default.TB_repeat_매입금_D.ToString();
                form.TB_repeat_매입금_E.Text = Properties.Settings.Default.TB_repeat_매입금_E.ToString();
                form.TB_repeat_매입금_F.Text = Properties.Settings.Default.TB_repeat_매입금_F.ToString();
                form.TB_repeat_매입금_G.Text = Properties.Settings.Default.TB_repeat_매입금_G.ToString();
                form.TB_repeat_매입금_H.Text = Properties.Settings.Default.TB_repeat_매입금_H.ToString();
                form.TB_repeat_매입금_I.Text = Properties.Settings.Default.TB_repeat_매입금_I.ToString();
                form.TB_repeat_매입금_J.Text = Properties.Settings.Default.TB_repeat_매입금_J.ToString();
                form.TB_repeat_매입금_K.Text = Properties.Settings.Default.TB_repeat_매입금_K.ToString();
                form.TB_repeat_매입금_L.Text = Properties.Settings.Default.TB_repeat_매입금_L.ToString();
                form.TB_repeat_매입금_M.Text = Properties.Settings.Default.TB_repeat_매입금_M.ToString();
                form.TB_repeat_매입금_N.Text = Properties.Settings.Default.TB_repeat_매입금_N.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_repeat_maemae_1_A.Text, out double maemae_1_A);
                double.TryParse(form.TB_repeat_maemae_1_B.Text, out double maemae_1_B);
                double.TryParse(form.TB_repeat_maemae_1_C.Text, out double maemae_1_C);
                double.TryParse(form.TB_repeat_maemae_1_D.Text, out double maemae_1_D);
                double.TryParse(form.TB_repeat_maemae_1_E.Text, out double maemae_1_E);
                double.TryParse(form.TB_repeat_maemae_1_F.Text, out double maemae_1_F);
                double.TryParse(form.TB_repeat_maemae_1_G.Text, out double maemae_1_G);
                double.TryParse(form.TB_repeat_maemae_1_H.Text, out double maemae_1_H);
                double.TryParse(form.TB_repeat_maemae_1_I.Text, out double maemae_1_I);
                double.TryParse(form.TB_repeat_maemae_1_J.Text, out double maemae_1_J);
                double.TryParse(form.TB_repeat_maemae_1_K.Text, out double maemae_1_K);
                double.TryParse(form.TB_repeat_maemae_1_L.Text, out double maemae_1_L);
                double.TryParse(form.TB_repeat_maemae_1_M.Text, out double maemae_1_M);
                double.TryParse(form.TB_repeat_maemae_1_N.Text, out double maemae_1_N);

                Properties.Settings.Default.TB_repeat_maemae_1_A = Math.Abs(maemae_1_A);
                Properties.Settings.Default.TB_repeat_maemae_1_B = Math.Abs(maemae_1_B);
                Properties.Settings.Default.TB_repeat_maemae_1_C = Math.Abs(maemae_1_C);
                Properties.Settings.Default.TB_repeat_maemae_1_D = Math.Abs(maemae_1_D);
                Properties.Settings.Default.TB_repeat_maemae_1_E = Math.Abs(maemae_1_E);
                Properties.Settings.Default.TB_repeat_maemae_1_F = Math.Abs(maemae_1_F);
                Properties.Settings.Default.TB_repeat_maemae_1_G = Math.Abs(maemae_1_G);
                Properties.Settings.Default.TB_repeat_maemae_1_H = Math.Abs(maemae_1_H);
                Properties.Settings.Default.TB_repeat_maemae_1_I = Math.Abs(maemae_1_I);
                Properties.Settings.Default.TB_repeat_maemae_1_J = Math.Abs(maemae_1_J);
                Properties.Settings.Default.TB_repeat_maemae_1_K = Math.Abs(maemae_1_K);
                Properties.Settings.Default.TB_repeat_maemae_1_L = Math.Abs(maemae_1_L);
                Properties.Settings.Default.TB_repeat_maemae_1_M = Math.Abs(maemae_1_M);
                Properties.Settings.Default.TB_repeat_maemae_1_N = Math.Abs(maemae_1_N);

                form.TB_repeat_maemae_1_A.Text = Properties.Settings.Default.TB_repeat_maemae_1_A.ToString();
                form.TB_repeat_maemae_1_B.Text = Properties.Settings.Default.TB_repeat_maemae_1_B.ToString();
                form.TB_repeat_maemae_1_C.Text = Properties.Settings.Default.TB_repeat_maemae_1_C.ToString();
                form.TB_repeat_maemae_1_D.Text = Properties.Settings.Default.TB_repeat_maemae_1_D.ToString();
                form.TB_repeat_maemae_1_E.Text = Properties.Settings.Default.TB_repeat_maemae_1_E.ToString();
                form.TB_repeat_maemae_1_F.Text = Properties.Settings.Default.TB_repeat_maemae_1_F.ToString();
                form.TB_repeat_maemae_1_G.Text = Properties.Settings.Default.TB_repeat_maemae_1_G.ToString();
                form.TB_repeat_maemae_1_H.Text = Properties.Settings.Default.TB_repeat_maemae_1_H.ToString();
                form.TB_repeat_maemae_1_I.Text = Properties.Settings.Default.TB_repeat_maemae_1_I.ToString();
                form.TB_repeat_maemae_1_J.Text = Properties.Settings.Default.TB_repeat_maemae_1_J.ToString();
                form.TB_repeat_maemae_1_K.Text = Properties.Settings.Default.TB_repeat_maemae_1_K.ToString();
                form.TB_repeat_maemae_1_L.Text = Properties.Settings.Default.TB_repeat_maemae_1_L.ToString();
                form.TB_repeat_maemae_1_M.Text = Properties.Settings.Default.TB_repeat_maemae_1_M.ToString();
                form.TB_repeat_maemae_1_N.Text = Properties.Settings.Default.TB_repeat_maemae_1_N.ToString();

                double.TryParse(form.TB_repeat_maemae_2_A.Text, out double maemae_2_A);
                double.TryParse(form.TB_repeat_maemae_2_B.Text, out double maemae_2_B);
                double.TryParse(form.TB_repeat_maemae_2_C.Text, out double maemae_2_C);
                double.TryParse(form.TB_repeat_maemae_2_D.Text, out double maemae_2_D);
                double.TryParse(form.TB_repeat_maemae_2_E.Text, out double maemae_2_E);
                double.TryParse(form.TB_repeat_maemae_2_F.Text, out double maemae_2_F);
                double.TryParse(form.TB_repeat_maemae_2_G.Text, out double maemae_2_G);
                double.TryParse(form.TB_repeat_maemae_2_H.Text, out double maemae_2_H);
                double.TryParse(form.TB_repeat_maemae_2_I.Text, out double maemae_2_I);
                double.TryParse(form.TB_repeat_maemae_2_J.Text, out double maemae_2_J);
                double.TryParse(form.TB_repeat_maemae_2_K.Text, out double maemae_2_K);
                double.TryParse(form.TB_repeat_maemae_2_L.Text, out double maemae_2_L);
                double.TryParse(form.TB_repeat_maemae_2_M.Text, out double maemae_2_M);
                double.TryParse(form.TB_repeat_maemae_2_N.Text, out double maemae_2_N);

                if (maemae_2_A == 0) maemae_2_A = 100;
                if (maemae_2_B == 0) maemae_2_B = 100;
                if (maemae_2_C == 0) maemae_2_C = 100;
                if (maemae_2_D == 0) maemae_2_D = 100;
                if (maemae_2_E == 0) maemae_2_E = 100;
                if (maemae_2_F == 0) maemae_2_F = 100;
                if (maemae_2_G == 0) maemae_2_G = 100;
                if (maemae_2_H == 0) maemae_2_H = 100;
                if (maemae_2_I == 0) maemae_2_I = 100;
                if (maemae_2_J == 0) maemae_2_J = 100;
                if (maemae_2_K == 0) maemae_2_K = 100;
                if (maemae_2_L == 0) maemae_2_L = 100;
                if (maemae_2_M == 0) maemae_2_M = 100;
                if (maemae_2_N == 0) maemae_2_N = 100;

                Properties.Settings.Default.TB_repeat_maemae_2_A = Math.Abs(maemae_2_A);
                Properties.Settings.Default.TB_repeat_maemae_2_B = Math.Abs(maemae_2_B);
                Properties.Settings.Default.TB_repeat_maemae_2_C = Math.Abs(maemae_2_C);
                Properties.Settings.Default.TB_repeat_maemae_2_D = Math.Abs(maemae_2_D);
                Properties.Settings.Default.TB_repeat_maemae_2_E = Math.Abs(maemae_2_E);
                Properties.Settings.Default.TB_repeat_maemae_2_F = Math.Abs(maemae_2_F);
                Properties.Settings.Default.TB_repeat_maemae_2_G = Math.Abs(maemae_2_G);
                Properties.Settings.Default.TB_repeat_maemae_2_H = Math.Abs(maemae_2_H);
                Properties.Settings.Default.TB_repeat_maemae_2_I = Math.Abs(maemae_2_I);
                Properties.Settings.Default.TB_repeat_maemae_2_J = Math.Abs(maemae_2_J);
                Properties.Settings.Default.TB_repeat_maemae_2_K = Math.Abs(maemae_2_K);
                Properties.Settings.Default.TB_repeat_maemae_2_L = Math.Abs(maemae_2_L);
                Properties.Settings.Default.TB_repeat_maemae_2_M = Math.Abs(maemae_2_M);
                Properties.Settings.Default.TB_repeat_maemae_2_N = Math.Abs(maemae_2_N);

                form.TB_repeat_maemae_2_A.Text = Properties.Settings.Default.TB_repeat_maemae_2_A.ToString();
                form.TB_repeat_maemae_2_B.Text = Properties.Settings.Default.TB_repeat_maemae_2_B.ToString();
                form.TB_repeat_maemae_2_C.Text = Properties.Settings.Default.TB_repeat_maemae_2_C.ToString();
                form.TB_repeat_maemae_2_D.Text = Properties.Settings.Default.TB_repeat_maemae_2_D.ToString();
                form.TB_repeat_maemae_2_E.Text = Properties.Settings.Default.TB_repeat_maemae_2_E.ToString();
                form.TB_repeat_maemae_2_F.Text = Properties.Settings.Default.TB_repeat_maemae_2_F.ToString();
                form.TB_repeat_maemae_2_G.Text = Properties.Settings.Default.TB_repeat_maemae_2_G.ToString();
                form.TB_repeat_maemae_2_H.Text = Properties.Settings.Default.TB_repeat_maemae_2_H.ToString();
                form.TB_repeat_maemae_2_I.Text = Properties.Settings.Default.TB_repeat_maemae_2_I.ToString();
                form.TB_repeat_maemae_2_J.Text = Properties.Settings.Default.TB_repeat_maemae_2_J.ToString();
                form.TB_repeat_maemae_2_K.Text = Properties.Settings.Default.TB_repeat_maemae_2_K.ToString();
                form.TB_repeat_maemae_2_L.Text = Properties.Settings.Default.TB_repeat_maemae_2_L.ToString();
                form.TB_repeat_maemae_2_M.Text = Properties.Settings.Default.TB_repeat_maemae_2_M.ToString();
                form.TB_repeat_maemae_2_N.Text = Properties.Settings.Default.TB_repeat_maemae_2_N.ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MT_repeat_repeat_time_A.Text, out int repeat_time_A);
                int.TryParse(form.MT_repeat_repeat_time_B.Text, out int repeat_time_B);
                int.TryParse(form.MT_repeat_repeat_time_C.Text, out int repeat_time_C);
                int.TryParse(form.MT_repeat_repeat_time_D.Text, out int repeat_time_D);
                int.TryParse(form.MT_repeat_repeat_time_E.Text, out int repeat_time_E);
                int.TryParse(form.MT_repeat_repeat_time_F.Text, out int repeat_time_F);
                int.TryParse(form.MT_repeat_repeat_time_G.Text, out int repeat_time_G);
                int.TryParse(form.MT_repeat_repeat_time_H.Text, out int repeat_time_H);
                int.TryParse(form.MT_repeat_repeat_time_I.Text, out int repeat_time_I);
                int.TryParse(form.MT_repeat_repeat_time_J.Text, out int repeat_time_J);
                int.TryParse(form.MT_repeat_repeat_time_K.Text, out int repeat_time_K);
                int.TryParse(form.MT_repeat_repeat_time_L.Text, out int repeat_time_L);
                int.TryParse(form.MT_repeat_repeat_time_M.Text, out int repeat_time_M);
                int.TryParse(form.MT_repeat_repeat_time_N.Text, out int repeat_time_N);

                if (repeat_time_A == 0) repeat_time_A = 5;
                if (repeat_time_B == 0) repeat_time_B = 5;
                if (repeat_time_C == 0) repeat_time_C = 5;
                if (repeat_time_D == 0) repeat_time_D = 5;
                if (repeat_time_E == 0) repeat_time_E = 5;
                if (repeat_time_F == 0) repeat_time_F = 5;
                if (repeat_time_G == 0) repeat_time_G = 5;
                if (repeat_time_H == 0) repeat_time_H = 5;
                if (repeat_time_I == 0) repeat_time_I = 5;
                if (repeat_time_J == 0) repeat_time_J = 5;
                if (repeat_time_K == 0) repeat_time_K = 5;
                if (repeat_time_L == 0) repeat_time_L = 5;
                if (repeat_time_M == 0) repeat_time_M = 5;
                if (repeat_time_N == 0) repeat_time_N = 5;

                Properties.Settings.Default.MT_repeat_repeat_time_A = repeat_time_A;
                Properties.Settings.Default.MT_repeat_repeat_time_B = repeat_time_B;
                Properties.Settings.Default.MT_repeat_repeat_time_C = repeat_time_C;
                Properties.Settings.Default.MT_repeat_repeat_time_D = repeat_time_D;
                Properties.Settings.Default.MT_repeat_repeat_time_E = repeat_time_E;
                Properties.Settings.Default.MT_repeat_repeat_time_F = repeat_time_F;
                Properties.Settings.Default.MT_repeat_repeat_time_G = repeat_time_G;
                Properties.Settings.Default.MT_repeat_repeat_time_H = repeat_time_H;
                Properties.Settings.Default.MT_repeat_repeat_time_I = repeat_time_I;
                Properties.Settings.Default.MT_repeat_repeat_time_J = repeat_time_J;
                Properties.Settings.Default.MT_repeat_repeat_time_K = repeat_time_K;
                Properties.Settings.Default.MT_repeat_repeat_time_L = repeat_time_L;
                Properties.Settings.Default.MT_repeat_repeat_time_M = repeat_time_M;
                Properties.Settings.Default.MT_repeat_repeat_time_N = repeat_time_N;

                form.MT_repeat_repeat_time_A.Text = repeat_time_A.ToString();
                form.MT_repeat_repeat_time_B.Text = repeat_time_B.ToString();
                form.MT_repeat_repeat_time_C.Text = repeat_time_C.ToString();
                form.MT_repeat_repeat_time_D.Text = repeat_time_D.ToString();
                form.MT_repeat_repeat_time_E.Text = repeat_time_E.ToString();
                form.MT_repeat_repeat_time_F.Text = repeat_time_F.ToString();
                form.MT_repeat_repeat_time_G.Text = repeat_time_G.ToString();
                form.MT_repeat_repeat_time_H.Text = repeat_time_H.ToString();
                form.MT_repeat_repeat_time_I.Text = repeat_time_I.ToString();
                form.MT_repeat_repeat_time_J.Text = repeat_time_J.ToString();
                form.MT_repeat_repeat_time_K.Text = repeat_time_K.ToString();
                form.MT_repeat_repeat_time_L.Text = repeat_time_L.ToString();
                form.MT_repeat_repeat_time_M.Text = repeat_time_M.ToString();
                form.MT_repeat_repeat_time_N.Text = repeat_time_N.ToString();

                foreach (var item in Form1.Trading_Item_List.ToList())
                {
                    if (item.timer > 0)
                    {
                        if (item.location.Equals("반복_A") && item.timer > repeat_time_A) item.timer = repeat_time_A;
                        if (item.location.Equals("반복_B") && item.timer > repeat_time_B) item.timer = repeat_time_B;
                        if (item.location.Equals("반복_C") && item.timer > repeat_time_C) item.timer = repeat_time_C;
                        if (item.location.Equals("반복_D") && item.timer > repeat_time_D) item.timer = repeat_time_D;
                        if (item.location.Equals("반복_E") && item.timer > repeat_time_E) item.timer = repeat_time_E;
                        if (item.location.Equals("반복_F") && item.timer > repeat_time_F) item.timer = repeat_time_F;
                        if (item.location.Equals("반복_G") && item.timer > repeat_time_G) item.timer = repeat_time_G;
                        if (item.location.Equals("반복_H") && item.timer > repeat_time_H) item.timer = repeat_time_H;
                        if (item.location.Equals("반복_I") && item.timer > repeat_time_I) item.timer = repeat_time_I;
                        if (item.location.Equals("반복_J") && item.timer > repeat_time_J) item.timer = repeat_time_J;
                        if (item.location.Equals("반복_K") && item.timer > repeat_time_K) item.timer = repeat_time_K;
                        if (item.location.Equals("반복_L") && item.timer > repeat_time_L) item.timer = repeat_time_L;
                        if (item.location.Equals("반복_M") && item.timer > repeat_time_M) item.timer = repeat_time_M;
                        if (item.location.Equals("반복_N") && item.timer > repeat_time_N) item.timer = repeat_time_N;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }


            try
            {
                int.TryParse(form.TB_repeat_value_A.Text, out int value_A);
                int.TryParse(form.TB_repeat_value_B.Text, out int value_B);
                int.TryParse(form.TB_repeat_value_C.Text, out int value_C);
                int.TryParse(form.TB_repeat_value_D.Text, out int value_D);
                int.TryParse(form.TB_repeat_value_E.Text, out int value_E);
                int.TryParse(form.TB_repeat_value_F.Text, out int value_F);
                int.TryParse(form.TB_repeat_value_G.Text, out int value_G);
                int.TryParse(form.TB_repeat_value_H.Text, out int value_H);
                int.TryParse(form.TB_repeat_value_I.Text, out int value_I);
                int.TryParse(form.TB_repeat_value_J.Text, out int value_J);
                int.TryParse(form.TB_repeat_value_K.Text, out int value_K);
                int.TryParse(form.TB_repeat_value_L.Text, out int value_L);
                int.TryParse(form.TB_repeat_value_M.Text, out int value_M);
                int.TryParse(form.TB_repeat_value_N.Text, out int value_N);

                if (form.combo_repeat_jumun_A.SelectedIndex == 0 || form.combo_repeat_jumun_A.SelectedIndex == 1 || form.combo_repeat_jumun_A.SelectedIndex == 4 || form.combo_repeat_jumun_A.SelectedIndex == 5 || form.combo_repeat_jumun_A.SelectedIndex == 6) value_A = 0;
                if (form.combo_repeat_jumun_B.SelectedIndex == 0 || form.combo_repeat_jumun_B.SelectedIndex == 1 || form.combo_repeat_jumun_B.SelectedIndex == 4 || form.combo_repeat_jumun_B.SelectedIndex == 5 || form.combo_repeat_jumun_B.SelectedIndex == 6) value_B = 0;
                if (form.combo_repeat_jumun_C.SelectedIndex == 0 || form.combo_repeat_jumun_C.SelectedIndex == 1 || form.combo_repeat_jumun_C.SelectedIndex == 4 || form.combo_repeat_jumun_C.SelectedIndex == 5 || form.combo_repeat_jumun_C.SelectedIndex == 6) value_C = 0;
                if (form.combo_repeat_jumun_D.SelectedIndex == 0 || form.combo_repeat_jumun_D.SelectedIndex == 1 || form.combo_repeat_jumun_D.SelectedIndex == 4 || form.combo_repeat_jumun_D.SelectedIndex == 5 || form.combo_repeat_jumun_D.SelectedIndex == 6) value_D = 0;
                if (form.combo_repeat_jumun_E.SelectedIndex == 0 || form.combo_repeat_jumun_E.SelectedIndex == 1 || form.combo_repeat_jumun_E.SelectedIndex == 4 || form.combo_repeat_jumun_E.SelectedIndex == 5 || form.combo_repeat_jumun_E.SelectedIndex == 6) value_E = 0;
                if (form.combo_repeat_jumun_F.SelectedIndex == 0 || form.combo_repeat_jumun_F.SelectedIndex == 1 || form.combo_repeat_jumun_F.SelectedIndex == 4 || form.combo_repeat_jumun_F.SelectedIndex == 5 || form.combo_repeat_jumun_F.SelectedIndex == 6) value_F = 0;
                if (form.combo_repeat_jumun_G.SelectedIndex == 0 || form.combo_repeat_jumun_G.SelectedIndex == 1 || form.combo_repeat_jumun_G.SelectedIndex == 4 || form.combo_repeat_jumun_G.SelectedIndex == 5 || form.combo_repeat_jumun_G.SelectedIndex == 6) value_G = 0;
                if (form.combo_repeat_jumun_H.SelectedIndex == 0 || form.combo_repeat_jumun_H.SelectedIndex == 1 || form.combo_repeat_jumun_H.SelectedIndex == 4 || form.combo_repeat_jumun_H.SelectedIndex == 5 || form.combo_repeat_jumun_H.SelectedIndex == 6) value_H = 0;
                if (form.combo_repeat_jumun_I.SelectedIndex == 0 || form.combo_repeat_jumun_I.SelectedIndex == 1 || form.combo_repeat_jumun_I.SelectedIndex == 4 || form.combo_repeat_jumun_I.SelectedIndex == 5 || form.combo_repeat_jumun_I.SelectedIndex == 6) value_I = 0;
                if (form.combo_repeat_jumun_J.SelectedIndex == 0 || form.combo_repeat_jumun_J.SelectedIndex == 1 || form.combo_repeat_jumun_J.SelectedIndex == 4 || form.combo_repeat_jumun_J.SelectedIndex == 5 || form.combo_repeat_jumun_J.SelectedIndex == 6) value_J = 0;
                if (form.combo_repeat_jumun_K.SelectedIndex == 0 || form.combo_repeat_jumun_K.SelectedIndex == 1 || form.combo_repeat_jumun_K.SelectedIndex == 4 || form.combo_repeat_jumun_K.SelectedIndex == 5 || form.combo_repeat_jumun_K.SelectedIndex == 6) value_K = 0;
                if (form.combo_repeat_jumun_L.SelectedIndex == 0 || form.combo_repeat_jumun_L.SelectedIndex == 1 || form.combo_repeat_jumun_L.SelectedIndex == 4 || form.combo_repeat_jumun_L.SelectedIndex == 5 || form.combo_repeat_jumun_L.SelectedIndex == 6) value_L = 0;
                if (form.combo_repeat_jumun_M.SelectedIndex == 0 || form.combo_repeat_jumun_M.SelectedIndex == 1 || form.combo_repeat_jumun_M.SelectedIndex == 4 || form.combo_repeat_jumun_M.SelectedIndex == 5 || form.combo_repeat_jumun_M.SelectedIndex == 6) value_M = 0;
                if (form.combo_repeat_jumun_N.SelectedIndex == 0 || form.combo_repeat_jumun_N.SelectedIndex == 1 || form.combo_repeat_jumun_N.SelectedIndex == 4 || form.combo_repeat_jumun_N.SelectedIndex == 5 || form.combo_repeat_jumun_N.SelectedIndex == 6) value_N = 0;

                Properties.Settings.Default.TB_repeat_value_A = value_A;
                Properties.Settings.Default.TB_repeat_value_B = value_B;
                Properties.Settings.Default.TB_repeat_value_C = value_C;
                Properties.Settings.Default.TB_repeat_value_D = value_D;
                Properties.Settings.Default.TB_repeat_value_E = value_E;
                Properties.Settings.Default.TB_repeat_value_F = value_F;
                Properties.Settings.Default.TB_repeat_value_G = value_G;
                Properties.Settings.Default.TB_repeat_value_H = value_H;
                Properties.Settings.Default.TB_repeat_value_I = value_I;
                Properties.Settings.Default.TB_repeat_value_J = value_J;
                Properties.Settings.Default.TB_repeat_value_K = value_K;
                Properties.Settings.Default.TB_repeat_value_L = value_L;
                Properties.Settings.Default.TB_repeat_value_M = value_M;
                Properties.Settings.Default.TB_repeat_value_N = value_N;

                form.TB_repeat_value_A.Text = value_A.ToString();
                form.TB_repeat_value_B.Text = value_B.ToString();
                form.TB_repeat_value_C.Text = value_C.ToString();
                form.TB_repeat_value_D.Text = value_D.ToString();
                form.TB_repeat_value_E.Text = value_E.ToString();
                form.TB_repeat_value_F.Text = value_F.ToString();
                form.TB_repeat_value_G.Text = value_G.ToString();
                form.TB_repeat_value_H.Text = value_H.ToString();
                form.TB_repeat_value_I.Text = value_I.ToString();
                form.TB_repeat_value_J.Text = value_J.ToString();
                form.TB_repeat_value_K.Text = value_K.ToString();
                form.TB_repeat_value_L.Text = value_L.ToString();
                form.TB_repeat_value_M.Text = value_M.ToString();
                form.TB_repeat_value_N.Text = value_N.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_repeat_Cancel_time_A.Text, out int time_A);
                int.TryParse(form.MTB_repeat_Cancel_time_B.Text, out int time_B);
                int.TryParse(form.MTB_repeat_Cancel_time_C.Text, out int time_C);
                int.TryParse(form.MTB_repeat_Cancel_time_D.Text, out int time_D);
                int.TryParse(form.MTB_repeat_Cancel_time_E.Text, out int time_E);
                int.TryParse(form.MTB_repeat_Cancel_time_F.Text, out int time_F);
                int.TryParse(form.MTB_repeat_Cancel_time_G.Text, out int time_G);
                int.TryParse(form.MTB_repeat_Cancel_time_H.Text, out int time_H);
                int.TryParse(form.MTB_repeat_Cancel_time_I.Text, out int time_I);
                int.TryParse(form.MTB_repeat_Cancel_time_J.Text, out int time_J);
                int.TryParse(form.MTB_repeat_Cancel_time_K.Text, out int time_K);
                int.TryParse(form.MTB_repeat_Cancel_time_L.Text, out int time_L);
                int.TryParse(form.MTB_repeat_Cancel_time_M.Text, out int time_M);
                int.TryParse(form.MTB_repeat_Cancel_time_N.Text, out int time_N);

                if (time_A < 10) time_A = 60;
                if (time_B < 10) time_B = 60;
                if (time_C < 10) time_C = 60;
                if (time_D < 10) time_D = 60;
                if (time_E < 10) time_E = 60;
                if (time_F < 10) time_F = 60;
                if (time_G < 10) time_G = 60;
                if (time_H < 10) time_H = 60;
                if (time_I < 10) time_I = 60;
                if (time_J < 10) time_J = 60;
                if (time_K < 10) time_K = 60;
                if (time_L < 10) time_L = 60;
                if (time_M < 10) time_M = 60;
                if (time_N < 10) time_N = 60;

                Properties.Settings.Default.MTB_repeat_Cancel_time_A = time_A;
                Properties.Settings.Default.MTB_repeat_Cancel_time_B = time_B;
                Properties.Settings.Default.MTB_repeat_Cancel_time_C = time_C;
                Properties.Settings.Default.MTB_repeat_Cancel_time_D = time_D;
                Properties.Settings.Default.MTB_repeat_Cancel_time_E = time_E;
                Properties.Settings.Default.MTB_repeat_Cancel_time_F = time_F;
                Properties.Settings.Default.MTB_repeat_Cancel_time_G = time_G;
                Properties.Settings.Default.MTB_repeat_Cancel_time_H = time_H;
                Properties.Settings.Default.MTB_repeat_Cancel_time_I = time_I;
                Properties.Settings.Default.MTB_repeat_Cancel_time_J = time_J;
                Properties.Settings.Default.MTB_repeat_Cancel_time_K = time_K;
                Properties.Settings.Default.MTB_repeat_Cancel_time_L = time_L;
                Properties.Settings.Default.MTB_repeat_Cancel_time_M = time_M;
                Properties.Settings.Default.MTB_repeat_Cancel_time_N = time_N;

                form.MTB_repeat_Cancel_time_A.Text = time_A.ToString();
                form.MTB_repeat_Cancel_time_B.Text = time_B.ToString();
                form.MTB_repeat_Cancel_time_C.Text = time_C.ToString();
                form.MTB_repeat_Cancel_time_D.Text = time_D.ToString();
                form.MTB_repeat_Cancel_time_E.Text = time_E.ToString();
                form.MTB_repeat_Cancel_time_F.Text = time_F.ToString();
                form.MTB_repeat_Cancel_time_G.Text = time_G.ToString();
                form.MTB_repeat_Cancel_time_H.Text = time_H.ToString();
                form.MTB_repeat_Cancel_time_I.Text = time_I.ToString();
                form.MTB_repeat_Cancel_time_J.Text = time_J.ToString();
                form.MTB_repeat_Cancel_time_K.Text = time_K.ToString();
                form.MTB_repeat_Cancel_time_L.Text = time_L.ToString();
                form.MTB_repeat_Cancel_time_M.Text = time_M.ToString();
                form.MTB_repeat_Cancel_time_N.Text = time_N.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.MTB_repeat_repeat_A.Text, out int Repeat_A);
                int.TryParse(form.MTB_repeat_repeat_B.Text, out int Repeat_B);
                int.TryParse(form.MTB_repeat_repeat_C.Text, out int Repeat_C);
                int.TryParse(form.MTB_repeat_repeat_D.Text, out int Repeat_D);
                int.TryParse(form.MTB_repeat_repeat_E.Text, out int Repeat_E);
                int.TryParse(form.MTB_repeat_repeat_F.Text, out int Repeat_F);
                int.TryParse(form.MTB_repeat_repeat_G.Text, out int Repeat_G);
                int.TryParse(form.MTB_repeat_repeat_H.Text, out int Repeat_H);
                int.TryParse(form.MTB_repeat_repeat_I.Text, out int Repeat_I);
                int.TryParse(form.MTB_repeat_repeat_J.Text, out int Repeat_J);
                int.TryParse(form.MTB_repeat_repeat_K.Text, out int Repeat_K);
                int.TryParse(form.MTB_repeat_repeat_L.Text, out int Repeat_L);
                int.TryParse(form.MTB_repeat_repeat_M.Text, out int Repeat_M);
                int.TryParse(form.MTB_repeat_repeat_N.Text, out int Repeat_N);

                if (form.combo_repeat_Cancel_A.SelectedIndex == 0) Repeat_A = 0;
                if (form.combo_repeat_Cancel_B.SelectedIndex == 0) Repeat_B = 0;
                if (form.combo_repeat_Cancel_C.SelectedIndex == 0) Repeat_C = 0;
                if (form.combo_repeat_Cancel_D.SelectedIndex == 0) Repeat_D = 0;
                if (form.combo_repeat_Cancel_E.SelectedIndex == 0) Repeat_E = 0;
                if (form.combo_repeat_Cancel_F.SelectedIndex == 0) Repeat_F = 0;
                if (form.combo_repeat_Cancel_G.SelectedIndex == 0) Repeat_G = 0;
                if (form.combo_repeat_Cancel_H.SelectedIndex == 0) Repeat_H = 0;
                if (form.combo_repeat_Cancel_I.SelectedIndex == 0) Repeat_I = 0;
                if (form.combo_repeat_Cancel_J.SelectedIndex == 0) Repeat_J = 0;
                if (form.combo_repeat_Cancel_K.SelectedIndex == 0) Repeat_K = 0;
                if (form.combo_repeat_Cancel_L.SelectedIndex == 0) Repeat_L = 0;
                if (form.combo_repeat_Cancel_M.SelectedIndex == 0) Repeat_M = 0;
                if (form.combo_repeat_Cancel_N.SelectedIndex == 0) Repeat_N = 0;

                Properties.Settings.Default.MTB_repeat_repeat_A = Repeat_A;
                Properties.Settings.Default.MTB_repeat_repeat_B = Repeat_B;
                Properties.Settings.Default.MTB_repeat_repeat_C = Repeat_C;
                Properties.Settings.Default.MTB_repeat_repeat_D = Repeat_D;
                Properties.Settings.Default.MTB_repeat_repeat_E = Repeat_E;
                Properties.Settings.Default.MTB_repeat_repeat_F = Repeat_F;
                Properties.Settings.Default.MTB_repeat_repeat_G = Repeat_G;
                Properties.Settings.Default.MTB_repeat_repeat_H = Repeat_H;
                Properties.Settings.Default.MTB_repeat_repeat_I = Repeat_I;
                Properties.Settings.Default.MTB_repeat_repeat_J = Repeat_J;
                Properties.Settings.Default.MTB_repeat_repeat_K = Repeat_K;
                Properties.Settings.Default.MTB_repeat_repeat_L = Repeat_L;
                Properties.Settings.Default.MTB_repeat_repeat_M = Repeat_M;
                Properties.Settings.Default.MTB_repeat_repeat_N = Repeat_N;

                form.MTB_repeat_repeat_A.Text = Repeat_A.ToString();
                form.MTB_repeat_repeat_B.Text = Repeat_B.ToString();
                form.MTB_repeat_repeat_C.Text = Repeat_C.ToString();
                form.MTB_repeat_repeat_D.Text = Repeat_D.ToString();
                form.MTB_repeat_repeat_E.Text = Repeat_E.ToString();
                form.MTB_repeat_repeat_F.Text = Repeat_F.ToString();
                form.MTB_repeat_repeat_G.Text = Repeat_G.ToString();
                form.MTB_repeat_repeat_H.Text = Repeat_H.ToString();
                form.MTB_repeat_repeat_I.Text = Repeat_I.ToString();
                form.MTB_repeat_repeat_J.Text = Repeat_J.ToString();
                form.MTB_repeat_repeat_K.Text = Repeat_K.ToString();
                form.MTB_repeat_repeat_L.Text = Repeat_L.ToString();
                form.MTB_repeat_repeat_M.Text = Repeat_M.ToString();
                form.MTB_repeat_repeat_N.Text = Repeat_N.ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                if (Form1.form1.account_comboBox.SelectedIndex > -1)
                {
                    if (form.combo_repeat_use_condition_A.SelectedIndex == 0) form.combo_repeat_condition_A.SelectedItem = "";
                    if (form.combo_repeat_use_condition_B.SelectedIndex == 0) form.combo_repeat_condition_B.SelectedItem = "";
                    if (form.combo_repeat_use_condition_C.SelectedIndex == 0) form.combo_repeat_condition_C.SelectedItem = "";
                    if (form.combo_repeat_use_condition_D.SelectedIndex == 0) form.combo_repeat_condition_D.SelectedItem = "";
                    if (form.combo_repeat_use_condition_E.SelectedIndex == 0) form.combo_repeat_condition_E.SelectedItem = "";
                    if (form.combo_repeat_use_condition_F.SelectedIndex == 0) form.combo_repeat_condition_F.SelectedItem = "";
                    if (form.combo_repeat_use_condition_G.SelectedIndex == 0) form.combo_repeat_condition_G.SelectedItem = "";
                    if (form.combo_repeat_use_condition_H.SelectedIndex == 0) form.combo_repeat_condition_H.SelectedItem = "";
                    if (form.combo_repeat_use_condition_I.SelectedIndex == 0) form.combo_repeat_condition_I.SelectedItem = "";
                    if (form.combo_repeat_use_condition_J.SelectedIndex == 0) form.combo_repeat_condition_J.SelectedItem = "";
                    if (form.combo_repeat_use_condition_K.SelectedIndex == 0) form.combo_repeat_condition_K.SelectedItem = "";
                    if (form.combo_repeat_use_condition_L.SelectedIndex == 0) form.combo_repeat_condition_L.SelectedItem = "";
                    if (form.combo_repeat_use_condition_M.SelectedIndex == 0) form.combo_repeat_condition_M.SelectedItem = "";
                    if (form.combo_repeat_use_condition_N.SelectedIndex == 0) form.combo_repeat_condition_N.SelectedItem = "";

                    Properties.Settings.Default.combo_repeat_use_condition_A = form.combo_repeat_use_condition_A.SelectedIndex;
                    Properties.Settings.Default.combo_repeat_use_condition_B = form.combo_repeat_use_condition_B.SelectedIndex;
                    Properties.Settings.Default.combo_repeat_use_condition_C = form.combo_repeat_use_condition_C.SelectedIndex;
                    Properties.Settings.Default.combo_repeat_use_condition_D = form.combo_repeat_use_condition_D.SelectedIndex;
                    Properties.Settings.Default.combo_repeat_use_condition_E = form.combo_repeat_use_condition_E.SelectedIndex;
                    Properties.Settings.Default.combo_repeat_use_condition_F = form.combo_repeat_use_condition_F.SelectedIndex;
                    Properties.Settings.Default.combo_repeat_use_condition_G = form.combo_repeat_use_condition_G.SelectedIndex;
                    Properties.Settings.Default.combo_repeat_use_condition_H = form.combo_repeat_use_condition_H.SelectedIndex;
                    Properties.Settings.Default.combo_repeat_use_condition_I = form.combo_repeat_use_condition_I.SelectedIndex;
                    Properties.Settings.Default.combo_repeat_use_condition_J = form.combo_repeat_use_condition_J.SelectedIndex;
                    Properties.Settings.Default.combo_repeat_use_condition_K = form.combo_repeat_use_condition_K.SelectedIndex;
                    Properties.Settings.Default.combo_repeat_use_condition_L = form.combo_repeat_use_condition_L.SelectedIndex;
                    Properties.Settings.Default.combo_repeat_use_condition_M = form.combo_repeat_use_condition_M.SelectedIndex;
                    Properties.Settings.Default.combo_repeat_use_condition_N = form.combo_repeat_use_condition_N.SelectedIndex;

                    if (Properties.Settings.Default.combo_repeat_use_condition_A == 0) Form1.Repeat_condition_List_A.Clear();
                    if (Properties.Settings.Default.combo_repeat_use_condition_B == 0) Form1.Repeat_condition_List_B.Clear();
                    if (Properties.Settings.Default.combo_repeat_use_condition_C == 0) Form1.Repeat_condition_List_C.Clear();
                    if (Properties.Settings.Default.combo_repeat_use_condition_D == 0) Form1.Repeat_condition_List_D.Clear();
                    if (Properties.Settings.Default.combo_repeat_use_condition_E == 0) Form1.Repeat_condition_List_E.Clear();
                    if (Properties.Settings.Default.combo_repeat_use_condition_F == 0) Form1.Repeat_condition_List_F.Clear();
                    if (Properties.Settings.Default.combo_repeat_use_condition_G == 0) Form1.Repeat_condition_List_G.Clear();
                    if (Properties.Settings.Default.combo_repeat_use_condition_H == 0) Form1.Repeat_condition_List_H.Clear();
                    if (Properties.Settings.Default.combo_repeat_use_condition_I == 0) Form1.Repeat_condition_List_I.Clear();
                    if (Properties.Settings.Default.combo_repeat_use_condition_J == 0) Form1.Repeat_condition_List_J.Clear();
                    if (Properties.Settings.Default.combo_repeat_use_condition_K == 0) Form1.Repeat_condition_List_K.Clear();
                    if (Properties.Settings.Default.combo_repeat_use_condition_L == 0) Form1.Repeat_condition_List_L.Clear();
                    if (Properties.Settings.Default.combo_repeat_use_condition_M == 0) Form1.Repeat_condition_List_M.Clear();
                    if (Properties.Settings.Default.combo_repeat_use_condition_N == 0) Form1.Repeat_condition_List_N.Clear();

                    Properties.Settings.Default.CB_repeat_use_A = form.CB_repeat_use_A.Checked;
                    Properties.Settings.Default.CB_repeat_use_B = form.CB_repeat_use_B.Checked;
                    Properties.Settings.Default.CB_repeat_use_C = form.CB_repeat_use_C.Checked;
                    Properties.Settings.Default.CB_repeat_use_D = form.CB_repeat_use_D.Checked;
                    Properties.Settings.Default.CB_repeat_use_E = form.CB_repeat_use_E.Checked;
                    Properties.Settings.Default.CB_repeat_use_F = form.CB_repeat_use_F.Checked;
                    Properties.Settings.Default.CB_repeat_use_G = form.CB_repeat_use_G.Checked;
                    Properties.Settings.Default.CB_repeat_use_H = form.CB_repeat_use_H.Checked;
                    Properties.Settings.Default.CB_repeat_use_I = form.CB_repeat_use_I.Checked;
                    Properties.Settings.Default.CB_repeat_use_J = form.CB_repeat_use_J.Checked;
                    Properties.Settings.Default.CB_repeat_use_K = form.CB_repeat_use_K.Checked;
                    Properties.Settings.Default.CB_repeat_use_L = form.CB_repeat_use_L.Checked;
                    Properties.Settings.Default.CB_repeat_use_M = form.CB_repeat_use_M.Checked;
                    Properties.Settings.Default.CB_repeat_use_N = form.CB_repeat_use_N.Checked;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_repeat_누적거래량_A.Text.Replace(",", ""), out int TB_repeat_누적거래량_A);
                int.TryParse(form.TB_repeat_누적거래량_B.Text.Replace(",", ""), out int TB_repeat_누적거래량_B);
                int.TryParse(form.TB_repeat_누적거래량_C.Text.Replace(",", ""), out int TB_repeat_누적거래량_C);
                int.TryParse(form.TB_repeat_누적거래량_D.Text.Replace(",", ""), out int TB_repeat_누적거래량_D);
                int.TryParse(form.TB_repeat_누적거래량_E.Text.Replace(",", ""), out int TB_repeat_누적거래량_E);
                int.TryParse(form.TB_repeat_누적거래량_F.Text.Replace(",", ""), out int TB_repeat_누적거래량_F);
                int.TryParse(form.TB_repeat_누적거래량_G.Text.Replace(",", ""), out int TB_repeat_누적거래량_G);
                int.TryParse(form.TB_repeat_누적거래량_H.Text.Replace(",", ""), out int TB_repeat_누적거래량_H);
                int.TryParse(form.TB_repeat_누적거래량_I.Text.Replace(",", ""), out int TB_repeat_누적거래량_I);
                int.TryParse(form.TB_repeat_누적거래량_J.Text.Replace(",", ""), out int TB_repeat_누적거래량_J);
                int.TryParse(form.TB_repeat_누적거래량_K.Text.Replace(",", ""), out int TB_repeat_누적거래량_K);
                int.TryParse(form.TB_repeat_누적거래량_L.Text.Replace(",", ""), out int TB_repeat_누적거래량_L);
                int.TryParse(form.TB_repeat_누적거래량_M.Text.Replace(",", ""), out int TB_repeat_누적거래량_M);
                int.TryParse(form.TB_repeat_누적거래량_N.Text.Replace(",", ""), out int TB_repeat_누적거래량_N);

                Properties.Settings.Default.TB_repeat_누적거래량_A = TB_repeat_누적거래량_A;
                Properties.Settings.Default.TB_repeat_누적거래량_B = TB_repeat_누적거래량_B;
                Properties.Settings.Default.TB_repeat_누적거래량_C = TB_repeat_누적거래량_C;
                Properties.Settings.Default.TB_repeat_누적거래량_D = TB_repeat_누적거래량_D;
                Properties.Settings.Default.TB_repeat_누적거래량_E = TB_repeat_누적거래량_E;
                Properties.Settings.Default.TB_repeat_누적거래량_F = TB_repeat_누적거래량_F;
                Properties.Settings.Default.TB_repeat_누적거래량_G = TB_repeat_누적거래량_G;
                Properties.Settings.Default.TB_repeat_누적거래량_H = TB_repeat_누적거래량_H;
                Properties.Settings.Default.TB_repeat_누적거래량_I = TB_repeat_누적거래량_I;
                Properties.Settings.Default.TB_repeat_누적거래량_J = TB_repeat_누적거래량_J;
                Properties.Settings.Default.TB_repeat_누적거래량_K = TB_repeat_누적거래량_K;
                Properties.Settings.Default.TB_repeat_누적거래량_L = TB_repeat_누적거래량_L;
                Properties.Settings.Default.TB_repeat_누적거래량_M = TB_repeat_누적거래량_M;
                Properties.Settings.Default.TB_repeat_누적거래량_N = TB_repeat_누적거래량_N;

                form.TB_repeat_누적거래량_A.Text = Properties.Settings.Default.TB_repeat_누적거래량_A.ToString();
                form.TB_repeat_누적거래량_B.Text = Properties.Settings.Default.TB_repeat_누적거래량_B.ToString();
                form.TB_repeat_누적거래량_C.Text = Properties.Settings.Default.TB_repeat_누적거래량_C.ToString();
                form.TB_repeat_누적거래량_D.Text = Properties.Settings.Default.TB_repeat_누적거래량_D.ToString();
                form.TB_repeat_누적거래량_E.Text = Properties.Settings.Default.TB_repeat_누적거래량_E.ToString();
                form.TB_repeat_누적거래량_F.Text = Properties.Settings.Default.TB_repeat_누적거래량_F.ToString();
                form.TB_repeat_누적거래량_G.Text = Properties.Settings.Default.TB_repeat_누적거래량_G.ToString();
                form.TB_repeat_누적거래량_H.Text = Properties.Settings.Default.TB_repeat_누적거래량_H.ToString();
                form.TB_repeat_누적거래량_I.Text = Properties.Settings.Default.TB_repeat_누적거래량_I.ToString();
                form.TB_repeat_누적거래량_J.Text = Properties.Settings.Default.TB_repeat_누적거래량_J.ToString();
                form.TB_repeat_누적거래량_K.Text = Properties.Settings.Default.TB_repeat_누적거래량_K.ToString();
                form.TB_repeat_누적거래량_L.Text = Properties.Settings.Default.TB_repeat_누적거래량_L.ToString();
                form.TB_repeat_누적거래량_M.Text = Properties.Settings.Default.TB_repeat_누적거래량_M.ToString();
                form.TB_repeat_누적거래량_N.Text = Properties.Settings.Default.TB_repeat_누적거래량_N.ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_repeat_누적거래대금_A.Text.Replace(",", ""), out int TB_repeat_누적거래대금_A);
                int.TryParse(form.TB_repeat_누적거래대금_B.Text.Replace(",", ""), out int TB_repeat_누적거래대금_B);
                int.TryParse(form.TB_repeat_누적거래대금_C.Text.Replace(",", ""), out int TB_repeat_누적거래대금_C);
                int.TryParse(form.TB_repeat_누적거래대금_D.Text.Replace(",", ""), out int TB_repeat_누적거래대금_D);
                int.TryParse(form.TB_repeat_누적거래대금_E.Text.Replace(",", ""), out int TB_repeat_누적거래대금_E);
                int.TryParse(form.TB_repeat_누적거래대금_F.Text.Replace(",", ""), out int TB_repeat_누적거래대금_F);
                int.TryParse(form.TB_repeat_누적거래대금_G.Text.Replace(",", ""), out int TB_repeat_누적거래대금_G);
                int.TryParse(form.TB_repeat_누적거래대금_H.Text.Replace(",", ""), out int TB_repeat_누적거래대금_H);
                int.TryParse(form.TB_repeat_누적거래대금_I.Text.Replace(",", ""), out int TB_repeat_누적거래대금_I);
                int.TryParse(form.TB_repeat_누적거래대금_J.Text.Replace(",", ""), out int TB_repeat_누적거래대금_J);
                int.TryParse(form.TB_repeat_누적거래대금_K.Text.Replace(",", ""), out int TB_repeat_누적거래대금_K);
                int.TryParse(form.TB_repeat_누적거래대금_L.Text.Replace(",", ""), out int TB_repeat_누적거래대금_L);
                int.TryParse(form.TB_repeat_누적거래대금_M.Text.Replace(",", ""), out int TB_repeat_누적거래대금_M);
                int.TryParse(form.TB_repeat_누적거래대금_N.Text.Replace(",", ""), out int TB_repeat_누적거래대금_N);

                Properties.Settings.Default.TB_repeat_누적거래대금_A = TB_repeat_누적거래대금_A;
                Properties.Settings.Default.TB_repeat_누적거래대금_B = TB_repeat_누적거래대금_B;
                Properties.Settings.Default.TB_repeat_누적거래대금_C = TB_repeat_누적거래대금_C;
                Properties.Settings.Default.TB_repeat_누적거래대금_D = TB_repeat_누적거래대금_D;
                Properties.Settings.Default.TB_repeat_누적거래대금_E = TB_repeat_누적거래대금_E;
                Properties.Settings.Default.TB_repeat_누적거래대금_F = TB_repeat_누적거래대금_F;
                Properties.Settings.Default.TB_repeat_누적거래대금_G = TB_repeat_누적거래대금_G;
                Properties.Settings.Default.TB_repeat_누적거래대금_H = TB_repeat_누적거래대금_H;
                Properties.Settings.Default.TB_repeat_누적거래대금_I = TB_repeat_누적거래대금_I;
                Properties.Settings.Default.TB_repeat_누적거래대금_J = TB_repeat_누적거래대금_J;
                Properties.Settings.Default.TB_repeat_누적거래대금_K = TB_repeat_누적거래대금_K;
                Properties.Settings.Default.TB_repeat_누적거래대금_L = TB_repeat_누적거래대금_L;
                Properties.Settings.Default.TB_repeat_누적거래대금_M = TB_repeat_누적거래대금_M;
                Properties.Settings.Default.TB_repeat_누적거래대금_N = TB_repeat_누적거래대금_N;

                form.TB_repeat_누적거래대금_A.Text = Properties.Settings.Default.TB_repeat_누적거래대금_A.ToString();
                form.TB_repeat_누적거래대금_B.Text = Properties.Settings.Default.TB_repeat_누적거래대금_B.ToString();
                form.TB_repeat_누적거래대금_C.Text = Properties.Settings.Default.TB_repeat_누적거래대금_C.ToString();
                form.TB_repeat_누적거래대금_D.Text = Properties.Settings.Default.TB_repeat_누적거래대금_D.ToString();
                form.TB_repeat_누적거래대금_E.Text = Properties.Settings.Default.TB_repeat_누적거래대금_E.ToString();
                form.TB_repeat_누적거래대금_F.Text = Properties.Settings.Default.TB_repeat_누적거래대금_F.ToString();
                form.TB_repeat_누적거래대금_G.Text = Properties.Settings.Default.TB_repeat_누적거래대금_G.ToString();
                form.TB_repeat_누적거래대금_H.Text = Properties.Settings.Default.TB_repeat_누적거래대금_H.ToString();
                form.TB_repeat_누적거래대금_I.Text = Properties.Settings.Default.TB_repeat_누적거래대금_I.ToString();
                form.TB_repeat_누적거래대금_J.Text = Properties.Settings.Default.TB_repeat_누적거래대금_J.ToString();
                form.TB_repeat_누적거래대금_K.Text = Properties.Settings.Default.TB_repeat_누적거래대금_K.ToString();
                form.TB_repeat_누적거래대금_L.Text = Properties.Settings.Default.TB_repeat_누적거래대금_L.ToString();
                form.TB_repeat_누적거래대금_M.Text = Properties.Settings.Default.TB_repeat_누적거래대금_M.ToString();
                form.TB_repeat_누적거래대금_N.Text = Properties.Settings.Default.TB_repeat_누적거래대금_N.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                //분이평

                int.TryParse(form.TB_repeat_mma_A.Text, out int TB_repeat_mma_A);
                int.TryParse(form.TB_repeat_mma_B.Text, out int TB_repeat_mma_B);
                int.TryParse(form.TB_repeat_mma_C.Text, out int TB_repeat_mma_C);
                int.TryParse(form.TB_repeat_mma_D.Text, out int TB_repeat_mma_D);
                int.TryParse(form.TB_repeat_mma_E.Text, out int TB_repeat_mma_E);
                int.TryParse(form.TB_repeat_mma_F.Text, out int TB_repeat_mma_F);
                int.TryParse(form.TB_repeat_mma_G.Text, out int TB_repeat_mma_G);
                int.TryParse(form.TB_repeat_mma_H.Text, out int TB_repeat_mma_H);
                int.TryParse(form.TB_repeat_mma_I.Text, out int TB_repeat_mma_I);
                int.TryParse(form.TB_repeat_mma_J.Text, out int TB_repeat_mma_J);
                int.TryParse(form.TB_repeat_mma_K.Text, out int TB_repeat_mma_K);
                int.TryParse(form.TB_repeat_mma_L.Text, out int TB_repeat_mma_L);
                int.TryParse(form.TB_repeat_mma_M.Text, out int TB_repeat_mma_M);
                int.TryParse(form.TB_repeat_mma_N.Text, out int TB_repeat_mma_N);

                if (TB_repeat_mma_A == 0) TB_repeat_mma_A = 3;
                if (TB_repeat_mma_B == 0) TB_repeat_mma_B = 3;
                if (TB_repeat_mma_C == 0) TB_repeat_mma_C = 3;
                if (TB_repeat_mma_D == 0) TB_repeat_mma_D = 3;
                if (TB_repeat_mma_E == 0) TB_repeat_mma_E = 3;
                if (TB_repeat_mma_F == 0) TB_repeat_mma_F = 3;
                if (TB_repeat_mma_G == 0) TB_repeat_mma_G = 3;
                if (TB_repeat_mma_H == 0) TB_repeat_mma_H = 3;
                if (TB_repeat_mma_I == 0) TB_repeat_mma_I = 3;
                if (TB_repeat_mma_J == 0) TB_repeat_mma_J = 3;
                if (TB_repeat_mma_K == 0) TB_repeat_mma_K = 3;
                if (TB_repeat_mma_L == 0) TB_repeat_mma_L = 3;
                if (TB_repeat_mma_M == 0) TB_repeat_mma_M = 3;
                if (TB_repeat_mma_N == 0) TB_repeat_mma_N = 3;

                if (TB_repeat_mma_A > 300) TB_repeat_mma_A = 300;
                if (TB_repeat_mma_B > 300) TB_repeat_mma_B = 300;
                if (TB_repeat_mma_C > 300) TB_repeat_mma_C = 300;
                if (TB_repeat_mma_D > 300) TB_repeat_mma_D = 300;
                if (TB_repeat_mma_E > 300) TB_repeat_mma_E = 300;
                if (TB_repeat_mma_F > 300) TB_repeat_mma_F = 300;
                if (TB_repeat_mma_G > 300) TB_repeat_mma_G = 300;
                if (TB_repeat_mma_H > 300) TB_repeat_mma_H = 300;
                if (TB_repeat_mma_I > 300) TB_repeat_mma_I = 300;
                if (TB_repeat_mma_J > 300) TB_repeat_mma_J = 300;
                if (TB_repeat_mma_K > 300) TB_repeat_mma_K = 300;
                if (TB_repeat_mma_L > 300) TB_repeat_mma_L = 300;
                if (TB_repeat_mma_M > 300) TB_repeat_mma_M = 300;
                if (TB_repeat_mma_N > 300) TB_repeat_mma_N = 300;

                Properties.Settings.Default.TB_repeat_mma_A = TB_repeat_mma_A;
                Properties.Settings.Default.TB_repeat_mma_B = TB_repeat_mma_B;
                Properties.Settings.Default.TB_repeat_mma_C = TB_repeat_mma_C;
                Properties.Settings.Default.TB_repeat_mma_D = TB_repeat_mma_D;
                Properties.Settings.Default.TB_repeat_mma_E = TB_repeat_mma_E;
                Properties.Settings.Default.TB_repeat_mma_F = TB_repeat_mma_F;
                Properties.Settings.Default.TB_repeat_mma_G = TB_repeat_mma_G;
                Properties.Settings.Default.TB_repeat_mma_H = TB_repeat_mma_H;
                Properties.Settings.Default.TB_repeat_mma_I = TB_repeat_mma_I;
                Properties.Settings.Default.TB_repeat_mma_J = TB_repeat_mma_J;
                Properties.Settings.Default.TB_repeat_mma_K = TB_repeat_mma_K;
                Properties.Settings.Default.TB_repeat_mma_L = TB_repeat_mma_L;
                Properties.Settings.Default.TB_repeat_mma_M = TB_repeat_mma_M;
                Properties.Settings.Default.TB_repeat_mma_N = TB_repeat_mma_N;

                form.TB_repeat_mma_A.Text = TB_repeat_mma_A.ToString();
                form.TB_repeat_mma_B.Text = TB_repeat_mma_B.ToString();
                form.TB_repeat_mma_C.Text = TB_repeat_mma_C.ToString();
                form.TB_repeat_mma_D.Text = TB_repeat_mma_D.ToString();
                form.TB_repeat_mma_E.Text = TB_repeat_mma_E.ToString();
                form.TB_repeat_mma_F.Text = TB_repeat_mma_F.ToString();
                form.TB_repeat_mma_G.Text = TB_repeat_mma_G.ToString();
                form.TB_repeat_mma_H.Text = TB_repeat_mma_H.ToString();
                form.TB_repeat_mma_I.Text = TB_repeat_mma_I.ToString();
                form.TB_repeat_mma_J.Text = TB_repeat_mma_J.ToString();
                form.TB_repeat_mma_K.Text = TB_repeat_mma_K.ToString();
                form.TB_repeat_mma_L.Text = TB_repeat_mma_L.ToString();
                form.TB_repeat_mma_M.Text = TB_repeat_mma_M.ToString();
                form.TB_repeat_mma_N.Text = TB_repeat_mma_N.ToString();

                Properties.Settings.Default.CBB_repeat_mma_A = form.CBB_repeat_mma_A.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_B = form.CBB_repeat_mma_B.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_C = form.CBB_repeat_mma_C.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_D = form.CBB_repeat_mma_D.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_E = form.CBB_repeat_mma_E.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_F = form.CBB_repeat_mma_F.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_G = form.CBB_repeat_mma_G.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_H = form.CBB_repeat_mma_H.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_I = form.CBB_repeat_mma_I.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_J = form.CBB_repeat_mma_J.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_K = form.CBB_repeat_mma_K.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_L = form.CBB_repeat_mma_L.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_M = form.CBB_repeat_mma_M.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_N = form.CBB_repeat_mma_N.SelectedIndex;

                int.TryParse(form.TB_repeat_mma2_A.Text, out int TB_repeat_mma2_A);
                int.TryParse(form.TB_repeat_mma2_B.Text, out int TB_repeat_mma2_B);
                int.TryParse(form.TB_repeat_mma2_C.Text, out int TB_repeat_mma2_C);
                int.TryParse(form.TB_repeat_mma2_D.Text, out int TB_repeat_mma2_D);
                int.TryParse(form.TB_repeat_mma2_E.Text, out int TB_repeat_mma2_E);
                int.TryParse(form.TB_repeat_mma2_F.Text, out int TB_repeat_mma2_F);
                int.TryParse(form.TB_repeat_mma2_G.Text, out int TB_repeat_mma2_G);
                int.TryParse(form.TB_repeat_mma2_H.Text, out int TB_repeat_mma2_H);
                int.TryParse(form.TB_repeat_mma2_I.Text, out int TB_repeat_mma2_I);
                int.TryParse(form.TB_repeat_mma2_J.Text, out int TB_repeat_mma2_J);
                int.TryParse(form.TB_repeat_mma2_K.Text, out int TB_repeat_mma2_K);
                int.TryParse(form.TB_repeat_mma2_L.Text, out int TB_repeat_mma2_L);
                int.TryParse(form.TB_repeat_mma2_M.Text, out int TB_repeat_mma2_M);
                int.TryParse(form.TB_repeat_mma2_N.Text, out int TB_repeat_mma2_N);

                if (TB_repeat_mma2_A == 0) TB_repeat_mma2_A = 5;
                if (TB_repeat_mma2_B == 0) TB_repeat_mma2_B = 5;
                if (TB_repeat_mma2_C == 0) TB_repeat_mma2_C = 5;
                if (TB_repeat_mma2_D == 0) TB_repeat_mma2_D = 5;
                if (TB_repeat_mma2_E == 0) TB_repeat_mma2_E = 5;
                if (TB_repeat_mma2_F == 0) TB_repeat_mma2_F = 5;
                if (TB_repeat_mma2_G == 0) TB_repeat_mma2_G = 5;
                if (TB_repeat_mma2_H == 0) TB_repeat_mma2_H = 5;
                if (TB_repeat_mma2_I == 0) TB_repeat_mma2_I = 5;
                if (TB_repeat_mma2_J == 0) TB_repeat_mma2_J = 5;
                if (TB_repeat_mma2_K == 0) TB_repeat_mma2_K = 5;
                if (TB_repeat_mma2_L == 0) TB_repeat_mma2_L = 5;
                if (TB_repeat_mma2_M == 0) TB_repeat_mma2_M = 5;
                if (TB_repeat_mma2_N == 0) TB_repeat_mma2_N = 5;

                if (TB_repeat_mma2_A > 300) TB_repeat_mma2_A = 300;
                if (TB_repeat_mma2_B > 300) TB_repeat_mma2_B = 300;
                if (TB_repeat_mma2_C > 300) TB_repeat_mma2_C = 300;
                if (TB_repeat_mma2_D > 300) TB_repeat_mma2_D = 300;
                if (TB_repeat_mma2_E > 300) TB_repeat_mma2_E = 300;
                if (TB_repeat_mma2_F > 300) TB_repeat_mma2_F = 300;
                if (TB_repeat_mma2_G > 300) TB_repeat_mma2_G = 300;
                if (TB_repeat_mma2_H > 300) TB_repeat_mma2_H = 300;
                if (TB_repeat_mma2_I > 300) TB_repeat_mma2_I = 300;
                if (TB_repeat_mma2_J > 300) TB_repeat_mma2_J = 300;
                if (TB_repeat_mma2_K > 300) TB_repeat_mma2_K = 300;
                if (TB_repeat_mma2_L > 300) TB_repeat_mma2_L = 300;
                if (TB_repeat_mma2_M > 300) TB_repeat_mma2_M = 300;
                if (TB_repeat_mma2_N > 300) TB_repeat_mma2_N = 300;

                Properties.Settings.Default.TB_repeat_mma2_A = TB_repeat_mma2_A;
                Properties.Settings.Default.TB_repeat_mma2_B = TB_repeat_mma2_B;
                Properties.Settings.Default.TB_repeat_mma2_C = TB_repeat_mma2_C;
                Properties.Settings.Default.TB_repeat_mma2_D = TB_repeat_mma2_D;
                Properties.Settings.Default.TB_repeat_mma2_E = TB_repeat_mma2_E;
                Properties.Settings.Default.TB_repeat_mma2_F = TB_repeat_mma2_F;
                Properties.Settings.Default.TB_repeat_mma2_G = TB_repeat_mma2_G;
                Properties.Settings.Default.TB_repeat_mma2_H = TB_repeat_mma2_H;
                Properties.Settings.Default.TB_repeat_mma2_I = TB_repeat_mma2_I;
                Properties.Settings.Default.TB_repeat_mma2_J = TB_repeat_mma2_J;
                Properties.Settings.Default.TB_repeat_mma2_K = TB_repeat_mma2_K;
                Properties.Settings.Default.TB_repeat_mma2_L = TB_repeat_mma2_L;
                Properties.Settings.Default.TB_repeat_mma2_M = TB_repeat_mma2_M;
                Properties.Settings.Default.TB_repeat_mma2_N = TB_repeat_mma2_N;

                form.TB_repeat_mma2_A.Text = TB_repeat_mma2_A.ToString();
                form.TB_repeat_mma2_B.Text = TB_repeat_mma2_B.ToString();
                form.TB_repeat_mma2_C.Text = TB_repeat_mma2_C.ToString();
                form.TB_repeat_mma2_D.Text = TB_repeat_mma2_D.ToString();
                form.TB_repeat_mma2_E.Text = TB_repeat_mma2_E.ToString();
                form.TB_repeat_mma2_F.Text = TB_repeat_mma2_F.ToString();
                form.TB_repeat_mma2_G.Text = TB_repeat_mma2_G.ToString();
                form.TB_repeat_mma2_H.Text = TB_repeat_mma2_H.ToString();
                form.TB_repeat_mma2_I.Text = TB_repeat_mma2_I.ToString();
                form.TB_repeat_mma2_J.Text = TB_repeat_mma2_J.ToString();
                form.TB_repeat_mma2_K.Text = TB_repeat_mma2_K.ToString();
                form.TB_repeat_mma2_L.Text = TB_repeat_mma2_L.ToString();
                form.TB_repeat_mma2_M.Text = TB_repeat_mma2_M.ToString();
                form.TB_repeat_mma2_N.Text = TB_repeat_mma2_N.ToString();

                Properties.Settings.Default.CBB_repeat_mma2_A = form.CBB_repeat_mma2_A.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma2_B = form.CBB_repeat_mma2_B.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma2_C = form.CBB_repeat_mma2_C.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma2_D = form.CBB_repeat_mma2_D.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma2_E = form.CBB_repeat_mma2_E.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma2_F = form.CBB_repeat_mma2_F.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma2_G = form.CBB_repeat_mma2_G.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma2_H = form.CBB_repeat_mma2_H.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma2_I = form.CBB_repeat_mma2_I.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma2_J = form.CBB_repeat_mma2_J.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma2_K = form.CBB_repeat_mma2_K.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma2_L = form.CBB_repeat_mma2_L.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma2_M = form.CBB_repeat_mma2_M.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma2_N = form.CBB_repeat_mma2_N.SelectedIndex;

                Properties.Settings.Default.CBB_repeat_mma_배열_A = form.CBB_repeat_mma_배열_A.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_배열_B = form.CBB_repeat_mma_배열_B.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_배열_C = form.CBB_repeat_mma_배열_C.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_배열_D = form.CBB_repeat_mma_배열_D.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_배열_E = form.CBB_repeat_mma_배열_E.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_배열_F = form.CBB_repeat_mma_배열_F.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_배열_G = form.CBB_repeat_mma_배열_G.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_배열_H = form.CBB_repeat_mma_배열_H.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_배열_I = form.CBB_repeat_mma_배열_I.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_배열_J = form.CBB_repeat_mma_배열_J.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_배열_K = form.CBB_repeat_mma_배열_K.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_배열_L = form.CBB_repeat_mma_배열_L.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_배열_M = form.CBB_repeat_mma_배열_M.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_mma_배열_N = form.CBB_repeat_mma_배열_N.SelectedIndex;

                //일이평

                int.TryParse(form.TB_repeat_dma1_A.Text, out int TB_repeat_dma1_A);
                int.TryParse(form.TB_repeat_dma1_B.Text, out int TB_repeat_dma1_B);
                int.TryParse(form.TB_repeat_dma1_C.Text, out int TB_repeat_dma1_C);
                int.TryParse(form.TB_repeat_dma1_D.Text, out int TB_repeat_dma1_D);
                int.TryParse(form.TB_repeat_dma1_E.Text, out int TB_repeat_dma1_E);
                int.TryParse(form.TB_repeat_dma1_F.Text, out int TB_repeat_dma1_F);
                int.TryParse(form.TB_repeat_dma1_G.Text, out int TB_repeat_dma1_G);
                int.TryParse(form.TB_repeat_dma1_H.Text, out int TB_repeat_dma1_H);
                int.TryParse(form.TB_repeat_dma1_I.Text, out int TB_repeat_dma1_I);
                int.TryParse(form.TB_repeat_dma1_J.Text, out int TB_repeat_dma1_J);
                int.TryParse(form.TB_repeat_dma1_K.Text, out int TB_repeat_dma1_K);
                int.TryParse(form.TB_repeat_dma1_L.Text, out int TB_repeat_dma1_L);
                int.TryParse(form.TB_repeat_dma1_M.Text, out int TB_repeat_dma1_M);
                int.TryParse(form.TB_repeat_dma1_N.Text, out int TB_repeat_dma1_N);

                if (TB_repeat_dma1_A == 0) TB_repeat_dma1_A = 5;
                if (TB_repeat_dma1_B == 0) TB_repeat_dma1_B = 5;
                if (TB_repeat_dma1_C == 0) TB_repeat_dma1_C = 5;
                if (TB_repeat_dma1_D == 0) TB_repeat_dma1_D = 5;
                if (TB_repeat_dma1_E == 0) TB_repeat_dma1_E = 5;
                if (TB_repeat_dma1_F == 0) TB_repeat_dma1_F = 5;
                if (TB_repeat_dma1_G == 0) TB_repeat_dma1_G = 5;
                if (TB_repeat_dma1_H == 0) TB_repeat_dma1_H = 5;
                if (TB_repeat_dma1_I == 0) TB_repeat_dma1_I = 5;
                if (TB_repeat_dma1_J == 0) TB_repeat_dma1_J = 5;
                if (TB_repeat_dma1_K == 0) TB_repeat_dma1_K = 5;
                if (TB_repeat_dma1_L == 0) TB_repeat_dma1_L = 5;
                if (TB_repeat_dma1_M == 0) TB_repeat_dma1_M = 5;
                if (TB_repeat_dma1_N == 0) TB_repeat_dma1_N = 5;

                if (TB_repeat_dma1_A > 300) TB_repeat_dma1_A = 300;
                if (TB_repeat_dma1_B > 300) TB_repeat_dma1_B = 300;
                if (TB_repeat_dma1_C > 300) TB_repeat_dma1_C = 300;
                if (TB_repeat_dma1_D > 300) TB_repeat_dma1_D = 300;
                if (TB_repeat_dma1_E > 300) TB_repeat_dma1_E = 300;
                if (TB_repeat_dma1_F > 300) TB_repeat_dma1_F = 300;
                if (TB_repeat_dma1_G > 300) TB_repeat_dma1_G = 300;
                if (TB_repeat_dma1_H > 300) TB_repeat_dma1_H = 300;
                if (TB_repeat_dma1_I > 300) TB_repeat_dma1_I = 300;
                if (TB_repeat_dma1_J > 300) TB_repeat_dma1_J = 300;
                if (TB_repeat_dma1_K > 300) TB_repeat_dma1_K = 300;
                if (TB_repeat_dma1_L > 300) TB_repeat_dma1_L = 300;
                if (TB_repeat_dma1_M > 300) TB_repeat_dma1_M = 300;
                if (TB_repeat_dma1_N > 300) TB_repeat_dma1_N = 300;

                Properties.Settings.Default.TB_repeat_dma1_A = TB_repeat_dma1_A;
                Properties.Settings.Default.TB_repeat_dma1_B = TB_repeat_dma1_B;
                Properties.Settings.Default.TB_repeat_dma1_C = TB_repeat_dma1_C;
                Properties.Settings.Default.TB_repeat_dma1_D = TB_repeat_dma1_D;
                Properties.Settings.Default.TB_repeat_dma1_E = TB_repeat_dma1_E;
                Properties.Settings.Default.TB_repeat_dma1_F = TB_repeat_dma1_F;
                Properties.Settings.Default.TB_repeat_dma1_G = TB_repeat_dma1_G;
                Properties.Settings.Default.TB_repeat_dma1_H = TB_repeat_dma1_H;
                Properties.Settings.Default.TB_repeat_dma1_I = TB_repeat_dma1_I;
                Properties.Settings.Default.TB_repeat_dma1_J = TB_repeat_dma1_J;
                Properties.Settings.Default.TB_repeat_dma1_K = TB_repeat_dma1_K;
                Properties.Settings.Default.TB_repeat_dma1_L = TB_repeat_dma1_L;
                Properties.Settings.Default.TB_repeat_dma1_M = TB_repeat_dma1_M;
                Properties.Settings.Default.TB_repeat_dma1_N = TB_repeat_dma1_N;

                form.TB_repeat_dma1_A.Text = TB_repeat_dma1_A.ToString();
                form.TB_repeat_dma1_B.Text = TB_repeat_dma1_B.ToString();
                form.TB_repeat_dma1_C.Text = TB_repeat_dma1_C.ToString();
                form.TB_repeat_dma1_D.Text = TB_repeat_dma1_D.ToString();
                form.TB_repeat_dma1_E.Text = TB_repeat_dma1_E.ToString();
                form.TB_repeat_dma1_F.Text = TB_repeat_dma1_F.ToString();
                form.TB_repeat_dma1_G.Text = TB_repeat_dma1_G.ToString();
                form.TB_repeat_dma1_H.Text = TB_repeat_dma1_H.ToString();
                form.TB_repeat_dma1_I.Text = TB_repeat_dma1_I.ToString();
                form.TB_repeat_dma1_J.Text = TB_repeat_dma1_J.ToString();
                form.TB_repeat_dma1_K.Text = TB_repeat_dma1_K.ToString();
                form.TB_repeat_dma1_L.Text = TB_repeat_dma1_L.ToString();
                form.TB_repeat_dma1_M.Text = TB_repeat_dma1_M.ToString();
                form.TB_repeat_dma1_N.Text = TB_repeat_dma1_N.ToString();

                Properties.Settings.Default.CBB_repeat_dma1_A = form.CBB_repeat_dma1_A.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma1_B = form.CBB_repeat_dma1_B.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma1_C = form.CBB_repeat_dma1_C.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma1_D = form.CBB_repeat_dma1_D.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma1_E = form.CBB_repeat_dma1_E.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma1_F = form.CBB_repeat_dma1_F.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma1_G = form.CBB_repeat_dma1_G.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma1_H = form.CBB_repeat_dma1_H.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma1_I = form.CBB_repeat_dma1_I.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma1_J = form.CBB_repeat_dma1_J.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma1_K = form.CBB_repeat_dma1_K.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma1_L = form.CBB_repeat_dma1_L.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma1_M = form.CBB_repeat_dma1_M.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma1_N = form.CBB_repeat_dma1_N.SelectedIndex;

                int.TryParse(form.TB_repeat_dma2_A.Text, out int TB_repeat_dma2_A);
                int.TryParse(form.TB_repeat_dma2_B.Text, out int TB_repeat_dma2_B);
                int.TryParse(form.TB_repeat_dma2_C.Text, out int TB_repeat_dma2_C);
                int.TryParse(form.TB_repeat_dma2_D.Text, out int TB_repeat_dma2_D);
                int.TryParse(form.TB_repeat_dma2_E.Text, out int TB_repeat_dma2_E);
                int.TryParse(form.TB_repeat_dma2_F.Text, out int TB_repeat_dma2_F);
                int.TryParse(form.TB_repeat_dma2_G.Text, out int TB_repeat_dma2_G);
                int.TryParse(form.TB_repeat_dma2_H.Text, out int TB_repeat_dma2_H);
                int.TryParse(form.TB_repeat_dma2_I.Text, out int TB_repeat_dma2_I);
                int.TryParse(form.TB_repeat_dma2_J.Text, out int TB_repeat_dma2_J);
                int.TryParse(form.TB_repeat_dma2_K.Text, out int TB_repeat_dma2_K);
                int.TryParse(form.TB_repeat_dma2_L.Text, out int TB_repeat_dma2_L);
                int.TryParse(form.TB_repeat_dma2_M.Text, out int TB_repeat_dma2_M);
                int.TryParse(form.TB_repeat_dma2_N.Text, out int TB_repeat_dma2_N);

                if (TB_repeat_dma2_A == 0) TB_repeat_dma2_A = 20;
                if (TB_repeat_dma2_B == 0) TB_repeat_dma2_B = 20;
                if (TB_repeat_dma2_C == 0) TB_repeat_dma2_C = 20;
                if (TB_repeat_dma2_D == 0) TB_repeat_dma2_D = 20;
                if (TB_repeat_dma2_E == 0) TB_repeat_dma2_E = 20;
                if (TB_repeat_dma2_F == 0) TB_repeat_dma2_F = 20;
                if (TB_repeat_dma2_G == 0) TB_repeat_dma2_G = 20;
                if (TB_repeat_dma2_H == 0) TB_repeat_dma2_H = 20;
                if (TB_repeat_dma2_I == 0) TB_repeat_dma2_I = 20;
                if (TB_repeat_dma2_J == 0) TB_repeat_dma2_J = 20;
                if (TB_repeat_dma2_K == 0) TB_repeat_dma2_K = 20;
                if (TB_repeat_dma2_L == 0) TB_repeat_dma2_L = 20;
                if (TB_repeat_dma2_M == 0) TB_repeat_dma2_M = 20;
                if (TB_repeat_dma2_N == 0) TB_repeat_dma2_N = 20;

                if (TB_repeat_dma2_A > 300) TB_repeat_dma2_A = 300;
                if (TB_repeat_dma2_B > 300) TB_repeat_dma2_B = 300;
                if (TB_repeat_dma2_C > 300) TB_repeat_dma2_C = 300;
                if (TB_repeat_dma2_D > 300) TB_repeat_dma2_D = 300;
                if (TB_repeat_dma2_E > 300) TB_repeat_dma2_E = 300;
                if (TB_repeat_dma2_F > 300) TB_repeat_dma2_F = 300;
                if (TB_repeat_dma2_G > 300) TB_repeat_dma2_G = 300;
                if (TB_repeat_dma2_H > 300) TB_repeat_dma2_H = 300;
                if (TB_repeat_dma2_I > 300) TB_repeat_dma2_I = 300;
                if (TB_repeat_dma2_J > 300) TB_repeat_dma2_J = 300;
                if (TB_repeat_dma2_K > 300) TB_repeat_dma2_K = 300;
                if (TB_repeat_dma2_L > 300) TB_repeat_dma2_L = 300;
                if (TB_repeat_dma2_M > 300) TB_repeat_dma2_M = 300;
                if (TB_repeat_dma2_N > 300) TB_repeat_dma2_N = 300;

                Properties.Settings.Default.TB_repeat_dma2_A = TB_repeat_dma2_A;
                Properties.Settings.Default.TB_repeat_dma2_B = TB_repeat_dma2_B;
                Properties.Settings.Default.TB_repeat_dma2_C = TB_repeat_dma2_C;
                Properties.Settings.Default.TB_repeat_dma2_D = TB_repeat_dma2_D;
                Properties.Settings.Default.TB_repeat_dma2_E = TB_repeat_dma2_E;
                Properties.Settings.Default.TB_repeat_dma2_F = TB_repeat_dma2_F;
                Properties.Settings.Default.TB_repeat_dma2_G = TB_repeat_dma2_G;
                Properties.Settings.Default.TB_repeat_dma2_H = TB_repeat_dma2_H;
                Properties.Settings.Default.TB_repeat_dma2_I = TB_repeat_dma2_I;
                Properties.Settings.Default.TB_repeat_dma2_J = TB_repeat_dma2_J;
                Properties.Settings.Default.TB_repeat_dma2_K = TB_repeat_dma2_K;
                Properties.Settings.Default.TB_repeat_dma2_L = TB_repeat_dma2_L;
                Properties.Settings.Default.TB_repeat_dma2_M = TB_repeat_dma2_M;
                Properties.Settings.Default.TB_repeat_dma2_N = TB_repeat_dma2_N;

                form.TB_repeat_dma2_A.Text = TB_repeat_dma2_A.ToString();
                form.TB_repeat_dma2_B.Text = TB_repeat_dma2_B.ToString();
                form.TB_repeat_dma2_C.Text = TB_repeat_dma2_C.ToString();
                form.TB_repeat_dma2_D.Text = TB_repeat_dma2_D.ToString();
                form.TB_repeat_dma2_E.Text = TB_repeat_dma2_E.ToString();
                form.TB_repeat_dma2_F.Text = TB_repeat_dma2_F.ToString();
                form.TB_repeat_dma2_G.Text = TB_repeat_dma2_G.ToString();
                form.TB_repeat_dma2_H.Text = TB_repeat_dma2_H.ToString();
                form.TB_repeat_dma2_I.Text = TB_repeat_dma2_I.ToString();
                form.TB_repeat_dma2_J.Text = TB_repeat_dma2_J.ToString();
                form.TB_repeat_dma2_K.Text = TB_repeat_dma2_K.ToString();
                form.TB_repeat_dma2_L.Text = TB_repeat_dma2_L.ToString();
                form.TB_repeat_dma2_M.Text = TB_repeat_dma2_M.ToString();
                form.TB_repeat_dma2_N.Text = TB_repeat_dma2_N.ToString();

                Properties.Settings.Default.CBB_repeat_dma2_A = form.CBB_repeat_dma2_A.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma2_B = form.CBB_repeat_dma2_B.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma2_C = form.CBB_repeat_dma2_C.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma2_D = form.CBB_repeat_dma2_D.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma2_E = form.CBB_repeat_dma2_E.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma2_F = form.CBB_repeat_dma2_F.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma2_G = form.CBB_repeat_dma2_G.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma2_H = form.CBB_repeat_dma2_H.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma2_I = form.CBB_repeat_dma2_I.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma2_J = form.CBB_repeat_dma2_J.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma2_K = form.CBB_repeat_dma2_K.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma2_L = form.CBB_repeat_dma2_L.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma2_M = form.CBB_repeat_dma2_M.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma2_N = form.CBB_repeat_dma2_N.SelectedIndex;

                Properties.Settings.Default.CBB_repeat_dma_배열_A = form.CBB_repeat_dma_배열_A.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma_배열_B = form.CBB_repeat_dma_배열_B.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma_배열_C = form.CBB_repeat_dma_배열_C.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma_배열_D = form.CBB_repeat_dma_배열_D.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma_배열_E = form.CBB_repeat_dma_배열_E.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma_배열_F = form.CBB_repeat_dma_배열_F.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma_배열_G = form.CBB_repeat_dma_배열_G.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma_배열_H = form.CBB_repeat_dma_배열_H.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma_배열_I = form.CBB_repeat_dma_배열_I.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma_배열_J = form.CBB_repeat_dma_배열_J.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma_배열_K = form.CBB_repeat_dma_배열_K.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma_배열_L = form.CBB_repeat_dma_배열_L.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma_배열_M = form.CBB_repeat_dma_배열_M.SelectedIndex;
                Properties.Settings.Default.CBB_repeat_dma_배열_N = form.CBB_repeat_dma_배열_N.SelectedIndex;

                MA.Get_MA();
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                Properties.Settings.Default.combo_repeat_Cancel_A = form.combo_repeat_Cancel_A.SelectedIndex;
                Properties.Settings.Default.combo_repeat_Cancel_B = form.combo_repeat_Cancel_B.SelectedIndex;
                Properties.Settings.Default.combo_repeat_Cancel_C = form.combo_repeat_Cancel_C.SelectedIndex;
                Properties.Settings.Default.combo_repeat_Cancel_D = form.combo_repeat_Cancel_D.SelectedIndex;
                Properties.Settings.Default.combo_repeat_Cancel_E = form.combo_repeat_Cancel_E.SelectedIndex;
                Properties.Settings.Default.combo_repeat_Cancel_F = form.combo_repeat_Cancel_F.SelectedIndex;
                Properties.Settings.Default.combo_repeat_Cancel_G = form.combo_repeat_Cancel_G.SelectedIndex;
                Properties.Settings.Default.combo_repeat_Cancel_H = form.combo_repeat_Cancel_H.SelectedIndex;
                Properties.Settings.Default.combo_repeat_Cancel_I = form.combo_repeat_Cancel_I.SelectedIndex;
                Properties.Settings.Default.combo_repeat_Cancel_J = form.combo_repeat_Cancel_J.SelectedIndex;
                Properties.Settings.Default.combo_repeat_Cancel_K = form.combo_repeat_Cancel_K.SelectedIndex;
                Properties.Settings.Default.combo_repeat_Cancel_L = form.combo_repeat_Cancel_L.SelectedIndex;
                Properties.Settings.Default.combo_repeat_Cancel_M = form.combo_repeat_Cancel_M.SelectedIndex;
                Properties.Settings.Default.combo_repeat_Cancel_N = form.combo_repeat_Cancel_N.SelectedIndex;

                Properties.Settings.Default.combo_repeat_suik_gubun_A = form.combo_repeat_suik_gubun_A.SelectedIndex;
                Properties.Settings.Default.combo_repeat_suik_gubun_B = form.combo_repeat_suik_gubun_B.SelectedIndex;
                Properties.Settings.Default.combo_repeat_suik_gubun_C = form.combo_repeat_suik_gubun_C.SelectedIndex;
                Properties.Settings.Default.combo_repeat_suik_gubun_D = form.combo_repeat_suik_gubun_D.SelectedIndex;
                Properties.Settings.Default.combo_repeat_suik_gubun_E = form.combo_repeat_suik_gubun_E.SelectedIndex;
                Properties.Settings.Default.combo_repeat_suik_gubun_F = form.combo_repeat_suik_gubun_F.SelectedIndex;
                Properties.Settings.Default.combo_repeat_suik_gubun_G = form.combo_repeat_suik_gubun_G.SelectedIndex;
                Properties.Settings.Default.combo_repeat_suik_gubun_H = form.combo_repeat_suik_gubun_H.SelectedIndex;
                Properties.Settings.Default.combo_repeat_suik_gubun_I = form.combo_repeat_suik_gubun_I.SelectedIndex;
                Properties.Settings.Default.combo_repeat_suik_gubun_J = form.combo_repeat_suik_gubun_J.SelectedIndex;
                Properties.Settings.Default.combo_repeat_suik_gubun_K = form.combo_repeat_suik_gubun_K.SelectedIndex;
                Properties.Settings.Default.combo_repeat_suik_gubun_L = form.combo_repeat_suik_gubun_L.SelectedIndex;
                Properties.Settings.Default.combo_repeat_suik_gubun_M = form.combo_repeat_suik_gubun_M.SelectedIndex;
                Properties.Settings.Default.combo_repeat_suik_gubun_N = form.combo_repeat_suik_gubun_N.SelectedIndex;

                Properties.Settings.Default.combo_repeat_sell_gubun_A = form.combo_repeat_sell_gubun_A.SelectedIndex;
                Properties.Settings.Default.combo_repeat_sell_gubun_B = form.combo_repeat_sell_gubun_B.SelectedIndex;
                Properties.Settings.Default.combo_repeat_sell_gubun_C = form.combo_repeat_sell_gubun_C.SelectedIndex;
                Properties.Settings.Default.combo_repeat_sell_gubun_D = form.combo_repeat_sell_gubun_D.SelectedIndex;
                Properties.Settings.Default.combo_repeat_sell_gubun_E = form.combo_repeat_sell_gubun_E.SelectedIndex;
                Properties.Settings.Default.combo_repeat_sell_gubun_F = form.combo_repeat_sell_gubun_F.SelectedIndex;
                Properties.Settings.Default.combo_repeat_sell_gubun_G = form.combo_repeat_sell_gubun_G.SelectedIndex;
                Properties.Settings.Default.combo_repeat_sell_gubun_H = form.combo_repeat_sell_gubun_H.SelectedIndex;
                Properties.Settings.Default.combo_repeat_sell_gubun_I = form.combo_repeat_sell_gubun_I.SelectedIndex;
                Properties.Settings.Default.combo_repeat_sell_gubun_J = form.combo_repeat_sell_gubun_J.SelectedIndex;
                Properties.Settings.Default.combo_repeat_sell_gubun_K = form.combo_repeat_sell_gubun_K.SelectedIndex;
                Properties.Settings.Default.combo_repeat_sell_gubun_L = form.combo_repeat_sell_gubun_L.SelectedIndex;
                Properties.Settings.Default.combo_repeat_sell_gubun_M = form.combo_repeat_sell_gubun_M.SelectedIndex;
                Properties.Settings.Default.combo_repeat_sell_gubun_N = form.combo_repeat_sell_gubun_N.SelectedIndex;

                Properties.Settings.Default.combo_repeat_maemae_gubun_A = form.combo_repeat_maemae_gubun_A.SelectedIndex;
                Properties.Settings.Default.combo_repeat_maemae_gubun_B = form.combo_repeat_maemae_gubun_B.SelectedIndex;
                Properties.Settings.Default.combo_repeat_maemae_gubun_C = form.combo_repeat_maemae_gubun_C.SelectedIndex;
                Properties.Settings.Default.combo_repeat_maemae_gubun_D = form.combo_repeat_maemae_gubun_D.SelectedIndex;
                Properties.Settings.Default.combo_repeat_maemae_gubun_E = form.combo_repeat_maemae_gubun_E.SelectedIndex;
                Properties.Settings.Default.combo_repeat_maemae_gubun_F = form.combo_repeat_maemae_gubun_F.SelectedIndex;
                Properties.Settings.Default.combo_repeat_maemae_gubun_G = form.combo_repeat_maemae_gubun_G.SelectedIndex;
                Properties.Settings.Default.combo_repeat_maemae_gubun_H = form.combo_repeat_maemae_gubun_H.SelectedIndex;
                Properties.Settings.Default.combo_repeat_maemae_gubun_I = form.combo_repeat_maemae_gubun_I.SelectedIndex;
                Properties.Settings.Default.combo_repeat_maemae_gubun_J = form.combo_repeat_maemae_gubun_J.SelectedIndex;
                Properties.Settings.Default.combo_repeat_maemae_gubun_K = form.combo_repeat_maemae_gubun_K.SelectedIndex;
                Properties.Settings.Default.combo_repeat_maemae_gubun_L = form.combo_repeat_maemae_gubun_L.SelectedIndex;
                Properties.Settings.Default.combo_repeat_maemae_gubun_M = form.combo_repeat_maemae_gubun_M.SelectedIndex;
                Properties.Settings.Default.combo_repeat_maemae_gubun_N = form.combo_repeat_maemae_gubun_N.SelectedIndex;
            }
            catch (Exception e)
            {
                Console.WriteLine("반복매매_저장 에러: " + e.Message); Form1.Error_Log("반복매매_저장 에러: " + e.Message);
            }

            Properties.Settings.Default.CB_Repeat_기준금 = form.CB_Repeat_기준금.Checked;

            Properties.Settings.Default.CB_repeat_kind_A = form.CB_repeat_kind_A.Checked;
            Properties.Settings.Default.CB_repeat_kind_B = form.CB_repeat_kind_B.Checked;
            Properties.Settings.Default.CB_repeat_kind_C = form.CB_repeat_kind_C.Checked;
            Properties.Settings.Default.CB_repeat_kind_D = form.CB_repeat_kind_D.Checked;
            Properties.Settings.Default.CB_repeat_kind_E = form.CB_repeat_kind_E.Checked;
            Properties.Settings.Default.CB_repeat_kind_F = form.CB_repeat_kind_F.Checked;
            Properties.Settings.Default.CB_repeat_kind_G = form.CB_repeat_kind_G.Checked;
            Properties.Settings.Default.CB_repeat_kind_H = form.CB_repeat_kind_H.Checked;
            Properties.Settings.Default.CB_repeat_kind_I = form.CB_repeat_kind_I.Checked;
            Properties.Settings.Default.CB_repeat_kind_J = form.CB_repeat_kind_J.Checked;
            Properties.Settings.Default.CB_repeat_kind_K = form.CB_repeat_kind_K.Checked;
            Properties.Settings.Default.CB_repeat_kind_L = form.CB_repeat_kind_L.Checked;
            Properties.Settings.Default.CB_repeat_kind_M = form.CB_repeat_kind_M.Checked;
            Properties.Settings.Default.CB_repeat_kind_N = form.CB_repeat_kind_N.Checked;

            Properties.Settings.Default.CB_repeat_choice_A = form.CB_repeat_choice_A.Checked;
            Properties.Settings.Default.CB_repeat_choice_B = form.CB_repeat_choice_B.Checked;
            Properties.Settings.Default.CB_repeat_choice_C = form.CB_repeat_choice_C.Checked;
            Properties.Settings.Default.CB_repeat_choice_D = form.CB_repeat_choice_D.Checked;
            Properties.Settings.Default.CB_repeat_choice_E = form.CB_repeat_choice_E.Checked;
            Properties.Settings.Default.CB_repeat_choice_F = form.CB_repeat_choice_F.Checked;
            Properties.Settings.Default.CB_repeat_choice_G = form.CB_repeat_choice_G.Checked;
            Properties.Settings.Default.CB_repeat_choice_H = form.CB_repeat_choice_H.Checked;
            Properties.Settings.Default.CB_repeat_choice_I = form.CB_repeat_choice_I.Checked;
            Properties.Settings.Default.CB_repeat_choice_J = form.CB_repeat_choice_J.Checked;
            Properties.Settings.Default.CB_repeat_choice_K = form.CB_repeat_choice_K.Checked;
            Properties.Settings.Default.CB_repeat_choice_L = form.CB_repeat_choice_L.Checked;
            Properties.Settings.Default.CB_repeat_choice_M = form.CB_repeat_choice_M.Checked;
            Properties.Settings.Default.CB_repeat_choice_N = form.CB_repeat_choice_N.Checked;
        }

        private void Form_Repeat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Properties.Settings.Default.CB_레이아웃고정_반복매매 = CB_레이아웃고정_반복매매.Checked;
                Properties.Settings.Default.CB_반복매매_시작위치저장 = form.CB_반복매매_시작위치저장.Checked;
                if (Properties.Settings.Default.CB_반복매매_시작위치저장) Properties.Settings.Default.WindowLocation = form.Location;

                LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
                Form1.form1.CB_반복매매.Checked = false;
                Form1.FormRepeat_Open = false;

                e.Cancel = true;
                Hide();
            }
        }

        private void CB_레이아웃고정_반복매매_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CB_레이아웃고정_반복매매 = CB_레이아웃고정_반복매매.Checked;

            if (!CB_레이아웃고정_반복매매.Checked) LayoutChange.CBB_layout_SelectedIndex(-1);
            else LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
        }

        //////////////////////////////// 특수설정 저장 ////////////////////////////////   
        private void BT_반복매매저장_Click(object sender, EventArgs e)
        {
            Form1.form1.Select();
            Form1.MBC_sender = (sender as Button).Name;
            Form1.중요메세지("반복매매", "반복매매 설정을 저장 하시겠습니까?");
        }

        private void Repeat_use_checked_chainge(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);
            CheckBox CB = sender as CheckBox;
            string Text = CB.Text.Substring(0, 2);

            if ((sender as CheckBox).Checked)
            {
                CB.Text = Text + "■";
            }
            else
            {
                CB.Text = Text + "□";

                if (Form1.FormRepeat_Open)
                {
                    foreach (var code in Form1.stockBalanceList.ToList())
                    {
                        if (CB.Name.Equals("CB_repeat_use_A")) code.Value.반복A = "A";
                        if (CB.Name.Equals("CB_repeat_use_B")) code.Value.반복B = "B";
                        if (CB.Name.Equals("CB_repeat_use_C")) code.Value.반복C = "C";
                        if (CB.Name.Equals("CB_repeat_use_D")) code.Value.반복D = "D";
                        if (CB.Name.Equals("CB_repeat_use_E")) code.Value.반복E = "E";
                        if (CB.Name.Equals("CB_repeat_use_F")) code.Value.반복F = "F";
                        if (CB.Name.Equals("CB_repeat_use_G")) code.Value.반복G = "G";
                        if (CB.Name.Equals("CB_repeat_use_H")) code.Value.반복H = "H";
                        if (CB.Name.Equals("CB_repeat_use_I")) code.Value.반복I = "I";
                        if (CB.Name.Equals("CB_repeat_use_J")) code.Value.반복J = "J";
                        if (CB.Name.Equals("CB_repeat_use_K")) code.Value.반복K = "K";
                        if (CB.Name.Equals("CB_repeat_use_L")) code.Value.반복L = "L";
                        if (CB.Name.Equals("CB_repeat_use_M")) code.Value.반복M = "M";
                        if (CB.Name.Equals("CB_repeat_use_N")) code.Value.반복N = "N";
                    }

                    if (CB.Name.Equals("CB_repeat_use_A")) Properties.Settings.Default.CB_repeat_use_A = false;
                    if (CB.Name.Equals("CB_repeat_use_B")) Properties.Settings.Default.CB_repeat_use_B = false;
                    if (CB.Name.Equals("CB_repeat_use_C")) Properties.Settings.Default.CB_repeat_use_C = false;
                    if (CB.Name.Equals("CB_repeat_use_D")) Properties.Settings.Default.CB_repeat_use_D = false;
                    if (CB.Name.Equals("CB_repeat_use_E")) Properties.Settings.Default.CB_repeat_use_E = false;
                    if (CB.Name.Equals("CB_repeat_use_F")) Properties.Settings.Default.CB_repeat_use_F = false;
                    if (CB.Name.Equals("CB_repeat_use_G")) Properties.Settings.Default.CB_repeat_use_G = false;
                    if (CB.Name.Equals("CB_repeat_use_H")) Properties.Settings.Default.CB_repeat_use_H = false;
                    if (CB.Name.Equals("CB_repeat_use_I")) Properties.Settings.Default.CB_repeat_use_I = false;
                    if (CB.Name.Equals("CB_repeat_use_J")) Properties.Settings.Default.CB_repeat_use_J = false;
                    if (CB.Name.Equals("CB_repeat_use_K")) Properties.Settings.Default.CB_repeat_use_K = false;
                    if (CB.Name.Equals("CB_repeat_use_L")) Properties.Settings.Default.CB_repeat_use_L = false;
                    if (CB.Name.Equals("CB_repeat_use_M")) Properties.Settings.Default.CB_repeat_use_M = false;
                    if (CB.Name.Equals("CB_repeat_use_N")) Properties.Settings.Default.CB_repeat_use_N = false;
                }
            }

            if (Form1.FormRepeat_Open) Form1.시장가탐색 = Condition_Management.시장가대금탐색();
        }

        private void Repeat_CB_CheckedChanged(object sender, EventArgs e)
        {
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

            if (Form1.FormRepeat_Open)
            {
                FormPrint.CBB_반복매매_매도비중_Selected(sender);

                if (sender.Equals(CB_repeat_kind_A)) if (CB_repeat_use_A.Checked) { CB_repeat_use_A.Checked = false; } else 비프음();
                if (sender.Equals(CB_repeat_kind_B)) if (CB_repeat_use_B.Checked) { CB_repeat_use_B.Checked = false; } else 비프음();
                if (sender.Equals(CB_repeat_kind_C)) if (CB_repeat_use_C.Checked) { CB_repeat_use_C.Checked = false; } else 비프음();
                if (sender.Equals(CB_repeat_kind_D)) if (CB_repeat_use_D.Checked) { CB_repeat_use_D.Checked = false; } else 비프음();
                if (sender.Equals(CB_repeat_kind_E)) if (CB_repeat_use_E.Checked) { CB_repeat_use_E.Checked = false; } else 비프음();
                if (sender.Equals(CB_repeat_kind_F)) if (CB_repeat_use_F.Checked) { CB_repeat_use_F.Checked = false; } else 비프음();
                if (sender.Equals(CB_repeat_kind_G)) if (CB_repeat_use_G.Checked) { CB_repeat_use_G.Checked = false; } else 비프음();
                if (sender.Equals(CB_repeat_kind_H)) if (CB_repeat_use_H.Checked) { CB_repeat_use_H.Checked = false; } else 비프음();
                if (sender.Equals(CB_repeat_kind_I)) if (CB_repeat_use_I.Checked) { CB_repeat_use_I.Checked = false; } else 비프음();
                if (sender.Equals(CB_repeat_kind_J)) if (CB_repeat_use_J.Checked) { CB_repeat_use_J.Checked = false; } else 비프음();
                if (sender.Equals(CB_repeat_kind_K)) if (CB_repeat_use_K.Checked) { CB_repeat_use_K.Checked = false; } else 비프음();
                if (sender.Equals(CB_repeat_kind_L)) if (CB_repeat_use_L.Checked) { CB_repeat_use_L.Checked = false; } else 비프음();
                if (sender.Equals(CB_repeat_kind_M)) if (CB_repeat_use_M.Checked) { CB_repeat_use_M.Checked = false; } else 비프음();
                if (sender.Equals(CB_repeat_kind_N)) if (CB_repeat_use_N.Checked) { CB_repeat_use_N.Checked = false; } else 비프음();

                void 비프음()
                {
                    Form1.form1.체크박스_비프(sender);
                }
            }
        }

        private void CheckBox_TextChange_Choicechecked(object sender, EventArgs e)
        {
            FormPrint.TextChange_매매방법(sender);
        }


        private void CBB_repeat_주문비중_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.로딩완료)
            {
                Form1.비프음("체크");
            }

            FormPrint.CBB_반복매매_매도비중_Selected(sender);
        }

        private void CheckBox_Check_TEXT_Changed(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);
            string text = CB.Text.Substring(1);
            if (CB.Checked)
            {
                if (Form1.로딩완료) Form1.비프음("체크");
                CB.Text = "■" + text;
                if (sender.Equals(CB_Repeat_기준금)) CB.ForeColor = Color.Crimson;
            }
            else
            {
                if (Form1.로딩완료) Form1.비프음("언체크");
                CB.Text = "□" + text;
                if (sender.Equals(CB_Repeat_기준금)) CB.ForeColor = Color.Black;
            }
        }

        private void combo_use_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Form1.FormRepeat_Open) Condition_Management.combo_use_condition_SelectedIndexChanged(sender);
        }

        private void combo_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Form1.FormRepeat_Open) Condition_Management.combo_condition_SelectedIndexChanged(sender);
        }

        private void CBB_jumun_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrint.CBB_jumun_SelectedIndex(sender);
        }


        private void combo_Condition_Add(object sender, EventArgs e)
        {
            Condition_Management.Condition_Add(sender);
        }

        private void combo_condition_MouseHover(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            toolTip1.SetToolTip(combo, combo.Text);
        }

        private void CBB_repeat_suik_gubun_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.FormRepeat_Open) Form1.비프음("체크");

            FormPrint.CBB_suik_DropDownClosed(sender);
        }

        private void combo_Condition_TextChanged(object sender, EventArgs e)
        {
            Condition_Management.Condition_TextChanged(sender);
        }

        private void CBB_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.FormRepeat_Open) Form1.비프음("체크");
        }
        private void TextBox_양실수만(object sender, EventArgs e)
        {
            TextValue.TextBox_양실수만(sender);
        }
        private void TextBox_소수자리제한(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_소수자리제한(sender);
        }

        public void TextBox_빨파검_소수2자리제한(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_빨파검_소수2자리제한(sender);
            TextBox_0입력(sender);
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

        public void TextBox_0입력(object sender) //textbox 의 색표시  사용
        {
            int index = 0;
            TextBox textbox = (sender as TextBox);
            if (sender.Equals(TB_repeat_suik_1_A)) index = combo_repeat_suik_gubun_A.SelectedIndex;
            if (sender.Equals(TB_repeat_suik_1_B)) index = combo_repeat_suik_gubun_B.SelectedIndex;
            if (sender.Equals(TB_repeat_suik_1_C)) index = combo_repeat_suik_gubun_C.SelectedIndex;
            if (sender.Equals(TB_repeat_suik_1_D)) index = combo_repeat_suik_gubun_D.SelectedIndex;
            if (sender.Equals(TB_repeat_suik_1_E)) index = combo_repeat_suik_gubun_E.SelectedIndex;
            if (sender.Equals(TB_repeat_suik_1_F)) index = combo_repeat_suik_gubun_F.SelectedIndex;
            if (sender.Equals(TB_repeat_suik_1_G)) index = combo_repeat_suik_gubun_G.SelectedIndex;
            if (sender.Equals(TB_repeat_suik_1_H)) index = combo_repeat_suik_gubun_H.SelectedIndex;
            if (sender.Equals(TB_repeat_suik_1_I)) index = combo_repeat_suik_gubun_I.SelectedIndex;
            if (sender.Equals(TB_repeat_suik_1_J)) index = combo_repeat_suik_gubun_J.SelectedIndex;
            if (sender.Equals(TB_repeat_suik_1_K)) index = combo_repeat_suik_gubun_K.SelectedIndex;
            if (sender.Equals(TB_repeat_suik_1_L)) index = combo_repeat_suik_gubun_L.SelectedIndex;
            if (sender.Equals(TB_repeat_suik_1_M)) index = combo_repeat_suik_gubun_M.SelectedIndex;
            if (sender.Equals(TB_repeat_suik_1_N)) index = combo_repeat_suik_gubun_N.SelectedIndex;

            textbox.Text = TextValue.TextBox_0입력(index, textbox.Text);
        }

        private void combo_cancel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrint.combo_cancel_SelectedIndexChanged(sender);
        }

        private void 숫자콤마넣기_TextChanged(object sender, EventArgs e)
        {
            TextValue.숫자콤마넣기_TextChanged(sender);
        }

        private void _양수실수_키프레스(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, false); // textbox 에 양수 , 실수 숫자만 입력 받을수 있음
        }

        private void CBB_mma_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_repeat_mma_A) && CBB_repeat_mma_A.SelectedIndex == 0) { CBB_repeat_mma2_A.SelectedIndex = 0; CBB_repeat_mma_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma_B) && CBB_repeat_mma_B.SelectedIndex == 0) { CBB_repeat_mma2_B.SelectedIndex = 0; CBB_repeat_mma_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma_C) && CBB_repeat_mma_C.SelectedIndex == 0) { CBB_repeat_mma2_C.SelectedIndex = 0; CBB_repeat_mma_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma_D) && CBB_repeat_mma_D.SelectedIndex == 0) { CBB_repeat_mma2_D.SelectedIndex = 0; CBB_repeat_mma_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma_E) && CBB_repeat_mma_E.SelectedIndex == 0) { CBB_repeat_mma2_E.SelectedIndex = 0; CBB_repeat_mma_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma_F) && CBB_repeat_mma_F.SelectedIndex == 0) { CBB_repeat_mma2_F.SelectedIndex = 0; CBB_repeat_mma_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma_G) && CBB_repeat_mma_G.SelectedIndex == 0) { CBB_repeat_mma2_G.SelectedIndex = 0; CBB_repeat_mma_배열_G.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma_H) && CBB_repeat_mma_H.SelectedIndex == 0) { CBB_repeat_mma2_H.SelectedIndex = 0; CBB_repeat_mma_배열_H.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma_I) && CBB_repeat_mma_I.SelectedIndex == 0) { CBB_repeat_mma2_I.SelectedIndex = 0; CBB_repeat_mma_배열_I.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma_J) && CBB_repeat_mma_J.SelectedIndex == 0) { CBB_repeat_mma2_J.SelectedIndex = 0; CBB_repeat_mma_배열_J.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma_K) && CBB_repeat_mma_K.SelectedIndex == 0) { CBB_repeat_mma2_K.SelectedIndex = 0; CBB_repeat_mma_배열_K.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma_L) && CBB_repeat_mma_L.SelectedIndex == 0) { CBB_repeat_mma2_L.SelectedIndex = 0; CBB_repeat_mma_배열_L.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma_M) && CBB_repeat_mma_M.SelectedIndex == 0) { CBB_repeat_mma2_M.SelectedIndex = 0; CBB_repeat_mma_배열_M.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma_N) && CBB_repeat_mma_N.SelectedIndex == 0) { CBB_repeat_mma2_N.SelectedIndex = 0; CBB_repeat_mma_배열_N.SelectedIndex = 0; }
        }

        private void CBB_mma2_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_repeat_mma2_A) && (CBB_repeat_mma_A.SelectedIndex == 0 || CBB_repeat_mma2_A.SelectedIndex == 0)) { CBB_repeat_mma2_A.SelectedIndex = 0; CBB_repeat_mma_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma2_B) && (CBB_repeat_mma_B.SelectedIndex == 0 || CBB_repeat_mma2_B.SelectedIndex == 0)) { CBB_repeat_mma2_B.SelectedIndex = 0; CBB_repeat_mma_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma2_C) && (CBB_repeat_mma_C.SelectedIndex == 0 || CBB_repeat_mma2_C.SelectedIndex == 0)) { CBB_repeat_mma2_C.SelectedIndex = 0; CBB_repeat_mma_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma2_D) && (CBB_repeat_mma_D.SelectedIndex == 0 || CBB_repeat_mma2_D.SelectedIndex == 0)) { CBB_repeat_mma2_D.SelectedIndex = 0; CBB_repeat_mma_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma2_E) && (CBB_repeat_mma_E.SelectedIndex == 0 || CBB_repeat_mma2_E.SelectedIndex == 0)) { CBB_repeat_mma2_E.SelectedIndex = 0; CBB_repeat_mma_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma2_F) && (CBB_repeat_mma_F.SelectedIndex == 0 || CBB_repeat_mma2_F.SelectedIndex == 0)) { CBB_repeat_mma2_F.SelectedIndex = 0; CBB_repeat_mma_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma2_G) && (CBB_repeat_mma_G.SelectedIndex == 0 || CBB_repeat_mma2_G.SelectedIndex == 0)) { CBB_repeat_mma2_G.SelectedIndex = 0; CBB_repeat_mma_배열_G.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma2_H) && (CBB_repeat_mma_H.SelectedIndex == 0 || CBB_repeat_mma2_H.SelectedIndex == 0)) { CBB_repeat_mma2_H.SelectedIndex = 0; CBB_repeat_mma_배열_H.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma2_I) && (CBB_repeat_mma_I.SelectedIndex == 0 || CBB_repeat_mma2_I.SelectedIndex == 0)) { CBB_repeat_mma2_I.SelectedIndex = 0; CBB_repeat_mma_배열_I.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma2_J) && (CBB_repeat_mma_J.SelectedIndex == 0 || CBB_repeat_mma2_J.SelectedIndex == 0)) { CBB_repeat_mma2_J.SelectedIndex = 0; CBB_repeat_mma_배열_J.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma2_K) && (CBB_repeat_mma_K.SelectedIndex == 0 || CBB_repeat_mma2_K.SelectedIndex == 0)) { CBB_repeat_mma2_K.SelectedIndex = 0; CBB_repeat_mma_배열_K.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma2_L) && (CBB_repeat_mma_L.SelectedIndex == 0 || CBB_repeat_mma2_L.SelectedIndex == 0)) { CBB_repeat_mma2_L.SelectedIndex = 0; CBB_repeat_mma_배열_L.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma2_M) && (CBB_repeat_mma_M.SelectedIndex == 0 || CBB_repeat_mma2_M.SelectedIndex == 0)) { CBB_repeat_mma2_M.SelectedIndex = 0; CBB_repeat_mma_배열_M.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_mma2_N) && (CBB_repeat_mma_N.SelectedIndex == 0 || CBB_repeat_mma2_N.SelectedIndex == 0)) { CBB_repeat_mma2_N.SelectedIndex = 0; CBB_repeat_mma_배열_N.SelectedIndex = 0; }
        }

        private void CBB_mma_배열_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_repeat_mma_배열_A) && (CBB_repeat_mma_A.SelectedIndex == 0 || CBB_repeat_mma2_A.SelectedIndex == 0)) CBB_repeat_mma_배열_A.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_mma_배열_B) && (CBB_repeat_mma_B.SelectedIndex == 0 || CBB_repeat_mma2_B.SelectedIndex == 0)) CBB_repeat_mma_배열_B.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_mma_배열_C) && (CBB_repeat_mma_C.SelectedIndex == 0 || CBB_repeat_mma2_C.SelectedIndex == 0)) CBB_repeat_mma_배열_C.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_mma_배열_D) && (CBB_repeat_mma_D.SelectedIndex == 0 || CBB_repeat_mma2_D.SelectedIndex == 0)) CBB_repeat_mma_배열_D.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_mma_배열_E) && (CBB_repeat_mma_E.SelectedIndex == 0 || CBB_repeat_mma2_E.SelectedIndex == 0)) CBB_repeat_mma_배열_E.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_mma_배열_F) && (CBB_repeat_mma_F.SelectedIndex == 0 || CBB_repeat_mma2_F.SelectedIndex == 0)) CBB_repeat_mma_배열_F.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_mma_배열_G) && (CBB_repeat_mma_G.SelectedIndex == 0 || CBB_repeat_mma2_G.SelectedIndex == 0)) CBB_repeat_mma_배열_G.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_mma_배열_H) && (CBB_repeat_mma_H.SelectedIndex == 0 || CBB_repeat_mma2_H.SelectedIndex == 0)) CBB_repeat_mma_배열_H.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_mma_배열_I) && (CBB_repeat_mma_I.SelectedIndex == 0 || CBB_repeat_mma2_I.SelectedIndex == 0)) CBB_repeat_mma_배열_I.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_mma_배열_J) && (CBB_repeat_mma_J.SelectedIndex == 0 || CBB_repeat_mma2_J.SelectedIndex == 0)) CBB_repeat_mma_배열_J.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_mma_배열_K) && (CBB_repeat_mma_K.SelectedIndex == 0 || CBB_repeat_mma2_K.SelectedIndex == 0)) CBB_repeat_mma_배열_K.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_mma_배열_L) && (CBB_repeat_mma_L.SelectedIndex == 0 || CBB_repeat_mma2_L.SelectedIndex == 0)) CBB_repeat_mma_배열_L.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_mma_배열_M) && (CBB_repeat_mma_M.SelectedIndex == 0 || CBB_repeat_mma2_M.SelectedIndex == 0)) CBB_repeat_mma_배열_M.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_mma_배열_N) && (CBB_repeat_mma_N.SelectedIndex == 0 || CBB_repeat_mma2_N.SelectedIndex == 0)) CBB_repeat_mma_배열_N.SelectedIndex = 0;
        }

        private void CBB_dma1_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_repeat_dma1_A) && CBB_repeat_dma1_A.SelectedIndex == 0) { CBB_repeat_dma2_A.SelectedIndex = 0; CBB_repeat_dma_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma1_B) && CBB_repeat_dma1_B.SelectedIndex == 0) { CBB_repeat_dma2_B.SelectedIndex = 0; CBB_repeat_dma_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma1_C) && CBB_repeat_dma1_C.SelectedIndex == 0) { CBB_repeat_dma2_C.SelectedIndex = 0; CBB_repeat_dma_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma1_D) && CBB_repeat_dma1_D.SelectedIndex == 0) { CBB_repeat_dma2_D.SelectedIndex = 0; CBB_repeat_dma_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma1_E) && CBB_repeat_dma1_E.SelectedIndex == 0) { CBB_repeat_dma2_E.SelectedIndex = 0; CBB_repeat_dma_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma1_F) && CBB_repeat_dma1_F.SelectedIndex == 0) { CBB_repeat_dma2_F.SelectedIndex = 0; CBB_repeat_dma_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma1_G) && CBB_repeat_dma1_G.SelectedIndex == 0) { CBB_repeat_dma2_G.SelectedIndex = 0; CBB_repeat_dma_배열_G.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma1_H) && CBB_repeat_dma1_H.SelectedIndex == 0) { CBB_repeat_dma2_H.SelectedIndex = 0; CBB_repeat_dma_배열_H.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma1_I) && CBB_repeat_dma1_I.SelectedIndex == 0) { CBB_repeat_dma2_I.SelectedIndex = 0; CBB_repeat_dma_배열_I.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma1_J) && CBB_repeat_dma1_J.SelectedIndex == 0) { CBB_repeat_dma2_J.SelectedIndex = 0; CBB_repeat_dma_배열_J.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma1_K) && CBB_repeat_dma1_K.SelectedIndex == 0) { CBB_repeat_dma2_K.SelectedIndex = 0; CBB_repeat_dma_배열_K.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma1_L) && CBB_repeat_dma1_L.SelectedIndex == 0) { CBB_repeat_dma2_L.SelectedIndex = 0; CBB_repeat_dma_배열_L.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma1_M) && CBB_repeat_dma1_M.SelectedIndex == 0) { CBB_repeat_dma2_M.SelectedIndex = 0; CBB_repeat_dma_배열_M.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma1_N) && CBB_repeat_dma1_N.SelectedIndex == 0) { CBB_repeat_dma2_N.SelectedIndex = 0; CBB_repeat_dma_배열_N.SelectedIndex = 0; }
        }
        private void CBB_dma2_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_repeat_dma2_A) && (CBB_repeat_dma1_A.SelectedIndex == 0 || CBB_repeat_dma2_A.SelectedIndex == 0)) { CBB_repeat_dma2_A.SelectedIndex = 0; CBB_repeat_dma_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma2_B) && (CBB_repeat_dma1_B.SelectedIndex == 0 || CBB_repeat_dma2_B.SelectedIndex == 0)) { CBB_repeat_dma2_B.SelectedIndex = 0; CBB_repeat_dma_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma2_C) && (CBB_repeat_dma1_C.SelectedIndex == 0 || CBB_repeat_dma2_C.SelectedIndex == 0)) { CBB_repeat_dma2_C.SelectedIndex = 0; CBB_repeat_dma_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma2_D) && (CBB_repeat_dma1_D.SelectedIndex == 0 || CBB_repeat_dma2_D.SelectedIndex == 0)) { CBB_repeat_dma2_D.SelectedIndex = 0; CBB_repeat_dma_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma2_E) && (CBB_repeat_dma1_E.SelectedIndex == 0 || CBB_repeat_dma2_E.SelectedIndex == 0)) { CBB_repeat_dma2_E.SelectedIndex = 0; CBB_repeat_dma_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma2_F) && (CBB_repeat_dma1_F.SelectedIndex == 0 || CBB_repeat_dma2_F.SelectedIndex == 0)) { CBB_repeat_dma2_F.SelectedIndex = 0; CBB_repeat_dma_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma2_G) && (CBB_repeat_dma1_G.SelectedIndex == 0 || CBB_repeat_dma2_G.SelectedIndex == 0)) { CBB_repeat_dma2_G.SelectedIndex = 0; CBB_repeat_dma_배열_G.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma2_H) && (CBB_repeat_dma1_H.SelectedIndex == 0 || CBB_repeat_dma2_H.SelectedIndex == 0)) { CBB_repeat_dma2_H.SelectedIndex = 0; CBB_repeat_dma_배열_H.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma2_I) && (CBB_repeat_dma1_I.SelectedIndex == 0 || CBB_repeat_dma2_I.SelectedIndex == 0)) { CBB_repeat_dma2_I.SelectedIndex = 0; CBB_repeat_dma_배열_I.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma2_J) && (CBB_repeat_dma1_J.SelectedIndex == 0 || CBB_repeat_dma2_J.SelectedIndex == 0)) { CBB_repeat_dma2_J.SelectedIndex = 0; CBB_repeat_dma_배열_J.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma2_K) && (CBB_repeat_dma1_K.SelectedIndex == 0 || CBB_repeat_dma2_K.SelectedIndex == 0)) { CBB_repeat_dma2_K.SelectedIndex = 0; CBB_repeat_dma_배열_K.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma2_L) && (CBB_repeat_dma1_L.SelectedIndex == 0 || CBB_repeat_dma2_L.SelectedIndex == 0)) { CBB_repeat_dma2_L.SelectedIndex = 0; CBB_repeat_dma_배열_L.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma2_M) && (CBB_repeat_dma1_M.SelectedIndex == 0 || CBB_repeat_dma2_M.SelectedIndex == 0)) { CBB_repeat_dma2_M.SelectedIndex = 0; CBB_repeat_dma_배열_M.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_dma2_N) && (CBB_repeat_dma1_N.SelectedIndex == 0 || CBB_repeat_dma2_N.SelectedIndex == 0)) { CBB_repeat_dma2_N.SelectedIndex = 0; CBB_repeat_dma_배열_N.SelectedIndex = 0; }
        }
        private void CBB_dma_배열_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_repeat_dma_배열_A) && (CBB_repeat_dma1_A.SelectedIndex == 0 || CBB_repeat_dma2_A.SelectedIndex == 0)) CBB_repeat_dma_배열_A.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_dma_배열_B) && (CBB_repeat_dma1_B.SelectedIndex == 0 || CBB_repeat_dma2_B.SelectedIndex == 0)) CBB_repeat_dma_배열_B.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_dma_배열_C) && (CBB_repeat_dma1_C.SelectedIndex == 0 || CBB_repeat_dma2_C.SelectedIndex == 0)) CBB_repeat_dma_배열_C.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_dma_배열_D) && (CBB_repeat_dma1_D.SelectedIndex == 0 || CBB_repeat_dma2_D.SelectedIndex == 0)) CBB_repeat_dma_배열_D.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_dma_배열_E) && (CBB_repeat_dma1_E.SelectedIndex == 0 || CBB_repeat_dma2_E.SelectedIndex == 0)) CBB_repeat_dma_배열_E.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_dma_배열_F) && (CBB_repeat_dma1_F.SelectedIndex == 0 || CBB_repeat_dma2_F.SelectedIndex == 0)) CBB_repeat_dma_배열_F.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_dma_배열_G) && (CBB_repeat_dma1_G.SelectedIndex == 0 || CBB_repeat_dma2_G.SelectedIndex == 0)) CBB_repeat_dma_배열_G.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_dma_배열_H) && (CBB_repeat_dma1_H.SelectedIndex == 0 || CBB_repeat_dma2_H.SelectedIndex == 0)) CBB_repeat_dma_배열_H.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_dma_배열_I) && (CBB_repeat_dma1_I.SelectedIndex == 0 || CBB_repeat_dma2_I.SelectedIndex == 0)) CBB_repeat_dma_배열_I.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_dma_배열_J) && (CBB_repeat_dma1_J.SelectedIndex == 0 || CBB_repeat_dma2_J.SelectedIndex == 0)) CBB_repeat_dma_배열_J.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_dma_배열_K) && (CBB_repeat_dma1_K.SelectedIndex == 0 || CBB_repeat_dma2_K.SelectedIndex == 0)) CBB_repeat_dma_배열_K.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_dma_배열_L) && (CBB_repeat_dma1_L.SelectedIndex == 0 || CBB_repeat_dma2_L.SelectedIndex == 0)) CBB_repeat_dma_배열_L.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_dma_배열_M) && (CBB_repeat_dma1_M.SelectedIndex == 0 || CBB_repeat_dma2_M.SelectedIndex == 0)) CBB_repeat_dma_배열_M.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_dma_배열_N) && (CBB_repeat_dma1_N.SelectedIndex == 0 || CBB_repeat_dma2_N.SelectedIndex == 0)) CBB_repeat_dma_배열_N.SelectedIndex = 0;
        }

    }
}
