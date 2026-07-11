using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace 지니64
{
    public partial class Form_Repeat : Form
    {
        public static Form_Repeat form;
        public Form_Repeat()
        {
            form = this;
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer |
                            ControlStyles.UserPaint |
                            ControlStyles.AllPaintingInWmPaint |
                            ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
        }

        public void Form_Repeat_Load()
        {
            Form1.음소거 = true;

            CB_repeat_kind_A.Checked = GenieConfig.CB_repeat_kind_A;
            CB_repeat_kind_C.Checked = GenieConfig.CB_repeat_kind_C;
            CB_repeat_kind_B.Checked = GenieConfig.CB_repeat_kind_B;
            CB_repeat_kind_D.Checked = GenieConfig.CB_repeat_kind_D;
            CB_repeat_kind_E.Checked = GenieConfig.CB_repeat_kind_E;
            CB_repeat_kind_F.Checked = GenieConfig.CB_repeat_kind_F;
            CB_repeat_kind_G.Checked = GenieConfig.CB_repeat_kind_G;
            CB_repeat_kind_H.Checked = GenieConfig.CB_repeat_kind_H;
            CB_repeat_kind_I.Checked = GenieConfig.CB_repeat_kind_I;
            CB_repeat_kind_J.Checked = GenieConfig.CB_repeat_kind_J;
            CB_repeat_kind_K.Checked = GenieConfig.CB_repeat_kind_K;
            CB_repeat_kind_L.Checked = GenieConfig.CB_repeat_kind_L;
            CB_repeat_kind_M.Checked = GenieConfig.CB_repeat_kind_M;
            CB_repeat_kind_N.Checked = GenieConfig.CB_repeat_kind_N;

            combo_repeat_jumun_A.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_jumun_A);
            combo_repeat_jumun_B.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_jumun_B);
            combo_repeat_jumun_C.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_jumun_C);
            combo_repeat_jumun_D.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_jumun_D);
            combo_repeat_jumun_E.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_jumun_E);
            combo_repeat_jumun_F.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_jumun_F);
            combo_repeat_jumun_G.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_jumun_G);
            combo_repeat_jumun_H.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_jumun_H);
            combo_repeat_jumun_I.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_jumun_I);
            combo_repeat_jumun_J.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_jumun_J);
            combo_repeat_jumun_K.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_jumun_K);
            combo_repeat_jumun_L.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_jumun_L);
            combo_repeat_jumun_M.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_jumun_M);
            combo_repeat_jumun_N.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_repeat_jumun_N);

            MT_repeat_time_start_A.Text = GenieConfig.MT_repeat_time_start_A.ToString();
            MT_repeat_time_start_B.Text = GenieConfig.MT_repeat_time_start_B.ToString();
            MT_repeat_time_start_C.Text = GenieConfig.MT_repeat_time_start_C.ToString();
            MT_repeat_time_start_D.Text = GenieConfig.MT_repeat_time_start_D.ToString();
            MT_repeat_time_start_E.Text = GenieConfig.MT_repeat_time_start_E.ToString();
            MT_repeat_time_start_F.Text = GenieConfig.MT_repeat_time_start_F.ToString();
            MT_repeat_time_start_G.Text = GenieConfig.MT_repeat_time_start_G.ToString();
            MT_repeat_time_start_H.Text = GenieConfig.MT_repeat_time_start_H.ToString();
            MT_repeat_time_start_I.Text = GenieConfig.MT_repeat_time_start_I.ToString();
            MT_repeat_time_start_J.Text = GenieConfig.MT_repeat_time_start_J.ToString();
            MT_repeat_time_start_K.Text = GenieConfig.MT_repeat_time_start_K.ToString();
            MT_repeat_time_start_L.Text = GenieConfig.MT_repeat_time_start_L.ToString();
            MT_repeat_time_start_M.Text = GenieConfig.MT_repeat_time_start_M.ToString();
            MT_repeat_time_start_N.Text = GenieConfig.MT_repeat_time_start_N.ToString();

            MT_repeat_time_end_A.Text = GenieConfig.MT_repeat_time_end_A.ToString();
            MT_repeat_time_end_B.Text = GenieConfig.MT_repeat_time_end_B.ToString();
            MT_repeat_time_end_C.Text = GenieConfig.MT_repeat_time_end_C.ToString();
            MT_repeat_time_end_D.Text = GenieConfig.MT_repeat_time_end_D.ToString();
            MT_repeat_time_end_E.Text = GenieConfig.MT_repeat_time_end_E.ToString();
            MT_repeat_time_end_F.Text = GenieConfig.MT_repeat_time_end_F.ToString();
            MT_repeat_time_end_G.Text = GenieConfig.MT_repeat_time_end_G.ToString();
            MT_repeat_time_end_H.Text = GenieConfig.MT_repeat_time_end_H.ToString();
            MT_repeat_time_end_I.Text = GenieConfig.MT_repeat_time_end_I.ToString();
            MT_repeat_time_end_J.Text = GenieConfig.MT_repeat_time_end_J.ToString();
            MT_repeat_time_end_K.Text = GenieConfig.MT_repeat_time_end_K.ToString();
            MT_repeat_time_end_L.Text = GenieConfig.MT_repeat_time_end_L.ToString();
            MT_repeat_time_end_M.Text = GenieConfig.MT_repeat_time_end_M.ToString();
            MT_repeat_time_end_N.Text = GenieConfig.MT_repeat_time_end_N.ToString();

            MT_repeat_repeat_time_A.Text = GenieConfig.MT_repeat_repeat_time_A.ToString();
            MT_repeat_repeat_time_B.Text = GenieConfig.MT_repeat_repeat_time_B.ToString();
            MT_repeat_repeat_time_C.Text = GenieConfig.MT_repeat_repeat_time_C.ToString();
            MT_repeat_repeat_time_D.Text = GenieConfig.MT_repeat_repeat_time_D.ToString();
            MT_repeat_repeat_time_E.Text = GenieConfig.MT_repeat_repeat_time_E.ToString();
            MT_repeat_repeat_time_F.Text = GenieConfig.MT_repeat_repeat_time_F.ToString();
            MT_repeat_repeat_time_G.Text = GenieConfig.MT_repeat_repeat_time_G.ToString();
            MT_repeat_repeat_time_H.Text = GenieConfig.MT_repeat_repeat_time_H.ToString();
            MT_repeat_repeat_time_I.Text = GenieConfig.MT_repeat_repeat_time_I.ToString();
            MT_repeat_repeat_time_J.Text = GenieConfig.MT_repeat_repeat_time_J.ToString();
            MT_repeat_repeat_time_K.Text = GenieConfig.MT_repeat_repeat_time_K.ToString();
            MT_repeat_repeat_time_L.Text = GenieConfig.MT_repeat_repeat_time_L.ToString();
            MT_repeat_repeat_time_M.Text = GenieConfig.MT_repeat_repeat_time_M.ToString();
            MT_repeat_repeat_time_N.Text = GenieConfig.MT_repeat_repeat_time_N.ToString();

            TB_repeat_suik_1_A.Text = GenieConfig.TB_repeat_suik_1_A.ToString();
            TB_repeat_suik_1_B.Text = GenieConfig.TB_repeat_suik_1_B.ToString();
            TB_repeat_suik_1_C.Text = GenieConfig.TB_repeat_suik_1_C.ToString();
            TB_repeat_suik_1_D.Text = GenieConfig.TB_repeat_suik_1_D.ToString();
            TB_repeat_suik_1_E.Text = GenieConfig.TB_repeat_suik_1_E.ToString();
            TB_repeat_suik_1_F.Text = GenieConfig.TB_repeat_suik_1_F.ToString();
            TB_repeat_suik_1_G.Text = GenieConfig.TB_repeat_suik_1_G.ToString();
            TB_repeat_suik_1_H.Text = GenieConfig.TB_repeat_suik_1_H.ToString();
            TB_repeat_suik_1_I.Text = GenieConfig.TB_repeat_suik_1_I.ToString();
            TB_repeat_suik_1_J.Text = GenieConfig.TB_repeat_suik_1_J.ToString();
            TB_repeat_suik_1_K.Text = GenieConfig.TB_repeat_suik_1_K.ToString();
            TB_repeat_suik_1_L.Text = GenieConfig.TB_repeat_suik_1_L.ToString();
            TB_repeat_suik_1_M.Text = GenieConfig.TB_repeat_suik_1_M.ToString();
            TB_repeat_suik_1_N.Text = GenieConfig.TB_repeat_suik_1_N.ToString();

            CB_repeat_choice_A.Checked = GenieConfig.CB_repeat_choice_A;
            CB_repeat_choice_B.Checked = GenieConfig.CB_repeat_choice_B;
            CB_repeat_choice_C.Checked = GenieConfig.CB_repeat_choice_C;
            CB_repeat_choice_D.Checked = GenieConfig.CB_repeat_choice_D;
            CB_repeat_choice_E.Checked = GenieConfig.CB_repeat_choice_E;
            CB_repeat_choice_F.Checked = GenieConfig.CB_repeat_choice_F;
            CB_repeat_choice_G.Checked = GenieConfig.CB_repeat_choice_G;
            CB_repeat_choice_H.Checked = GenieConfig.CB_repeat_choice_H;
            CB_repeat_choice_I.Checked = GenieConfig.CB_repeat_choice_I;
            CB_repeat_choice_J.Checked = GenieConfig.CB_repeat_choice_J;
            CB_repeat_choice_K.Checked = GenieConfig.CB_repeat_choice_K;
            CB_repeat_choice_L.Checked = GenieConfig.CB_repeat_choice_L;
            CB_repeat_choice_M.Checked = GenieConfig.CB_repeat_choice_M;
            CB_repeat_choice_N.Checked = GenieConfig.CB_repeat_choice_N;
            CB_Repeat_기준금.Checked = GenieConfig.CB_Repeat_기준금;

            TB_repeat_suik_2_A.Text = GenieConfig.TB_repeat_suik_2_A.ToString();
            TB_repeat_suik_2_B.Text = GenieConfig.TB_repeat_suik_2_B.ToString();
            TB_repeat_suik_2_C.Text = GenieConfig.TB_repeat_suik_2_C.ToString();
            TB_repeat_suik_2_D.Text = GenieConfig.TB_repeat_suik_2_D.ToString();
            TB_repeat_suik_2_E.Text = GenieConfig.TB_repeat_suik_2_E.ToString();
            TB_repeat_suik_2_F.Text = GenieConfig.TB_repeat_suik_2_F.ToString();
            TB_repeat_suik_2_G.Text = GenieConfig.TB_repeat_suik_2_G.ToString();
            TB_repeat_suik_2_H.Text = GenieConfig.TB_repeat_suik_2_H.ToString();
            TB_repeat_suik_2_I.Text = GenieConfig.TB_repeat_suik_2_I.ToString();
            TB_repeat_suik_2_J.Text = GenieConfig.TB_repeat_suik_2_J.ToString();
            TB_repeat_suik_2_K.Text = GenieConfig.TB_repeat_suik_2_K.ToString();
            TB_repeat_suik_2_L.Text = GenieConfig.TB_repeat_suik_2_L.ToString();
            TB_repeat_suik_2_M.Text = GenieConfig.TB_repeat_suik_2_M.ToString();
            TB_repeat_suik_2_N.Text = GenieConfig.TB_repeat_suik_2_N.ToString();

            TB_repeat_value_A.Text = GenieConfig.TB_repeat_value_A.ToString();
            TB_repeat_value_B.Text = GenieConfig.TB_repeat_value_B.ToString();
            TB_repeat_value_C.Text = GenieConfig.TB_repeat_value_C.ToString();
            TB_repeat_value_D.Text = GenieConfig.TB_repeat_value_D.ToString();
            TB_repeat_value_E.Text = GenieConfig.TB_repeat_value_E.ToString();
            TB_repeat_value_F.Text = GenieConfig.TB_repeat_value_F.ToString();
            TB_repeat_value_G.Text = GenieConfig.TB_repeat_value_G.ToString();
            TB_repeat_value_H.Text = GenieConfig.TB_repeat_value_H.ToString();
            TB_repeat_value_I.Text = GenieConfig.TB_repeat_value_I.ToString();
            TB_repeat_value_J.Text = GenieConfig.TB_repeat_value_J.ToString();
            TB_repeat_value_K.Text = GenieConfig.TB_repeat_value_K.ToString();
            TB_repeat_value_L.Text = GenieConfig.TB_repeat_value_L.ToString();
            TB_repeat_value_M.Text = GenieConfig.TB_repeat_value_M.ToString();
            TB_repeat_value_N.Text = GenieConfig.TB_repeat_value_N.ToString();

            combo_repeat_suik_gubun_A.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_A);
            combo_repeat_suik_gubun_B.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_B);
            combo_repeat_suik_gubun_C.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_C);
            combo_repeat_suik_gubun_D.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_D);
            combo_repeat_suik_gubun_E.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_E);
            combo_repeat_suik_gubun_F.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_F);
            combo_repeat_suik_gubun_G.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_G);
            combo_repeat_suik_gubun_H.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_H);
            combo_repeat_suik_gubun_I.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_I);
            combo_repeat_suik_gubun_J.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_J);
            combo_repeat_suik_gubun_K.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_K);
            combo_repeat_suik_gubun_L.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_L);
            combo_repeat_suik_gubun_M.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_M);
            combo_repeat_suik_gubun_N.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_repeat_suik_gubun_N);

            TB_repeat_sell_ratio_A.Text = GenieConfig.TB_repeat_sell_ratio_A.ToString();
            TB_repeat_sell_ratio_B.Text = GenieConfig.TB_repeat_sell_ratio_B.ToString();
            TB_repeat_sell_ratio_C.Text = GenieConfig.TB_repeat_sell_ratio_C.ToString();
            TB_repeat_sell_ratio_D.Text = GenieConfig.TB_repeat_sell_ratio_D.ToString();
            TB_repeat_sell_ratio_E.Text = GenieConfig.TB_repeat_sell_ratio_E.ToString();
            TB_repeat_sell_ratio_F.Text = GenieConfig.TB_repeat_sell_ratio_F.ToString();
            TB_repeat_sell_ratio_G.Text = GenieConfig.TB_repeat_sell_ratio_G.ToString();
            TB_repeat_sell_ratio_H.Text = GenieConfig.TB_repeat_sell_ratio_H.ToString();
            TB_repeat_sell_ratio_I.Text = GenieConfig.TB_repeat_sell_ratio_I.ToString();
            TB_repeat_sell_ratio_J.Text = GenieConfig.TB_repeat_sell_ratio_J.ToString();
            TB_repeat_sell_ratio_K.Text = GenieConfig.TB_repeat_sell_ratio_K.ToString();
            TB_repeat_sell_ratio_L.Text = GenieConfig.TB_repeat_sell_ratio_L.ToString();
            TB_repeat_sell_ratio_M.Text = GenieConfig.TB_repeat_sell_ratio_M.ToString();
            TB_repeat_sell_ratio_N.Text = GenieConfig.TB_repeat_sell_ratio_N.ToString();

            combo_repeat_sell_gubun_A.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_A);
            combo_repeat_sell_gubun_B.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_B);
            combo_repeat_sell_gubun_C.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_C);
            combo_repeat_sell_gubun_D.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_D);
            combo_repeat_sell_gubun_E.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_E);
            combo_repeat_sell_gubun_F.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_F);
            combo_repeat_sell_gubun_G.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_G);
            combo_repeat_sell_gubun_H.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_H);
            combo_repeat_sell_gubun_I.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_I);
            combo_repeat_sell_gubun_J.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_J);
            combo_repeat_sell_gubun_K.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_K);
            combo_repeat_sell_gubun_L.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_L);
            combo_repeat_sell_gubun_M.SelectedIndex =GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_M);
            combo_repeat_sell_gubun_N.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_repeat_sell_gubun_N);

            TB_Repeat_maemae_1_A.Text = GenieConfig.TB_Repeat_maemae_1_A.ToString();
            TB_Repeat_maemae_1_B.Text = GenieConfig.TB_Repeat_maemae_1_B.ToString();
            TB_Repeat_maemae_1_C.Text = GenieConfig.TB_Repeat_maemae_1_C.ToString();
            TB_Repeat_maemae_1_D.Text = GenieConfig.TB_Repeat_maemae_1_D.ToString();
            TB_Repeat_maemae_1_E.Text = GenieConfig.TB_Repeat_maemae_1_E.ToString();
            TB_Repeat_maemae_1_F.Text = GenieConfig.TB_Repeat_maemae_1_F.ToString();
            TB_Repeat_maemae_1_G.Text = GenieConfig.TB_Repeat_maemae_1_G.ToString();
            TB_Repeat_maemae_1_H.Text = GenieConfig.TB_Repeat_maemae_1_H.ToString();
            TB_Repeat_maemae_1_I.Text = GenieConfig.TB_Repeat_maemae_1_I.ToString();
            TB_Repeat_maemae_1_J.Text = GenieConfig.TB_Repeat_maemae_1_J.ToString();
            TB_Repeat_maemae_1_K.Text = GenieConfig.TB_Repeat_maemae_1_K.ToString();
            TB_Repeat_maemae_1_L.Text = GenieConfig.TB_Repeat_maemae_1_L.ToString();
            TB_Repeat_maemae_1_M.Text = GenieConfig.TB_Repeat_maemae_1_M.ToString();
            TB_Repeat_maemae_1_N.Text = GenieConfig.TB_Repeat_maemae_1_N.ToString();

            TB_Repeat_maemae_2_A.Text = GenieConfig.TB_Repeat_maemae_2_A.ToString();
            TB_Repeat_maemae_2_B.Text = GenieConfig.TB_Repeat_maemae_2_B.ToString();
            TB_Repeat_maemae_2_C.Text = GenieConfig.TB_Repeat_maemae_2_C.ToString();
            TB_Repeat_maemae_2_D.Text = GenieConfig.TB_Repeat_maemae_2_D.ToString();
            TB_Repeat_maemae_2_E.Text = GenieConfig.TB_Repeat_maemae_2_E.ToString();
            TB_Repeat_maemae_2_F.Text = GenieConfig.TB_Repeat_maemae_2_F.ToString();
            TB_Repeat_maemae_2_G.Text = GenieConfig.TB_Repeat_maemae_2_G.ToString();
            TB_Repeat_maemae_2_H.Text = GenieConfig.TB_Repeat_maemae_2_H.ToString();
            TB_Repeat_maemae_2_I.Text = GenieConfig.TB_Repeat_maemae_2_I.ToString();
            TB_Repeat_maemae_2_J.Text = GenieConfig.TB_Repeat_maemae_2_J.ToString();
            TB_Repeat_maemae_2_K.Text = GenieConfig.TB_Repeat_maemae_2_K.ToString();
            TB_Repeat_maemae_2_L.Text = GenieConfig.TB_Repeat_maemae_2_L.ToString();
            TB_Repeat_maemae_2_M.Text = GenieConfig.TB_Repeat_maemae_2_M.ToString();
            TB_Repeat_maemae_2_N.Text = GenieConfig.TB_Repeat_maemae_2_N.ToString();

            combo_Repeat_maemae_gubun_A.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_Repeat_maemae_gubun_A);
            combo_Repeat_maemae_gubun_B.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_Repeat_maemae_gubun_B);
            combo_Repeat_maemae_gubun_C.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_Repeat_maemae_gubun_C);
            combo_Repeat_maemae_gubun_D.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_Repeat_maemae_gubun_D);
            combo_Repeat_maemae_gubun_E.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_Repeat_maemae_gubun_E);
            combo_Repeat_maemae_gubun_F.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_Repeat_maemae_gubun_F);
            combo_Repeat_maemae_gubun_G.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_Repeat_maemae_gubun_G);
            combo_Repeat_maemae_gubun_H.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_Repeat_maemae_gubun_H);
            combo_Repeat_maemae_gubun_I.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_Repeat_maemae_gubun_I);
            combo_Repeat_maemae_gubun_J.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_Repeat_maemae_gubun_J);
            combo_Repeat_maemae_gubun_K.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_Repeat_maemae_gubun_K);
            combo_Repeat_maemae_gubun_L.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_Repeat_maemae_gubun_L);
            combo_Repeat_maemae_gubun_M.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_Repeat_maemae_gubun_M);
            combo_Repeat_maemae_gubun_N.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_Repeat_maemae_gubun_N);

            MTB_repeat_repeat_A.Text = GenieConfig.MTB_repeat_repeat_A.ToString();
            MTB_repeat_repeat_B.Text = GenieConfig.MTB_repeat_repeat_B.ToString();
            MTB_repeat_repeat_C.Text = GenieConfig.MTB_repeat_repeat_C.ToString();
            MTB_repeat_repeat_D.Text = GenieConfig.MTB_repeat_repeat_D.ToString();
            MTB_repeat_repeat_E.Text = GenieConfig.MTB_repeat_repeat_E.ToString();
            MTB_repeat_repeat_F.Text = GenieConfig.MTB_repeat_repeat_F.ToString();
            MTB_repeat_repeat_G.Text = GenieConfig.MTB_repeat_repeat_G.ToString();
            MTB_repeat_repeat_H.Text = GenieConfig.MTB_repeat_repeat_H.ToString();
            MTB_repeat_repeat_I.Text = GenieConfig.MTB_repeat_repeat_I.ToString();
            MTB_repeat_repeat_J.Text = GenieConfig.MTB_repeat_repeat_J.ToString();
            MTB_repeat_repeat_K.Text = GenieConfig.MTB_repeat_repeat_K.ToString();
            MTB_repeat_repeat_L.Text = GenieConfig.MTB_repeat_repeat_L.ToString();
            MTB_repeat_repeat_M.Text = GenieConfig.MTB_repeat_repeat_M.ToString();
            MTB_repeat_repeat_N.Text = GenieConfig.MTB_repeat_repeat_N.ToString();

            MTB_repeat_Cancel_time_A.Text = GenieConfig.MTB_repeat_Cancel_time_A.ToString();
            MTB_repeat_Cancel_time_B.Text = GenieConfig.MTB_repeat_Cancel_time_B.ToString();
            MTB_repeat_Cancel_time_C.Text = GenieConfig.MTB_repeat_Cancel_time_C.ToString();
            MTB_repeat_Cancel_time_D.Text = GenieConfig.MTB_repeat_Cancel_time_D.ToString();
            MTB_repeat_Cancel_time_E.Text = GenieConfig.MTB_repeat_Cancel_time_E.ToString();
            MTB_repeat_Cancel_time_F.Text = GenieConfig.MTB_repeat_Cancel_time_F.ToString();
            MTB_repeat_Cancel_time_G.Text = GenieConfig.MTB_repeat_Cancel_time_G.ToString();
            MTB_repeat_Cancel_time_H.Text = GenieConfig.MTB_repeat_Cancel_time_H.ToString();
            MTB_repeat_Cancel_time_I.Text = GenieConfig.MTB_repeat_Cancel_time_I.ToString();
            MTB_repeat_Cancel_time_J.Text = GenieConfig.MTB_repeat_Cancel_time_J.ToString();
            MTB_repeat_Cancel_time_K.Text = GenieConfig.MTB_repeat_Cancel_time_K.ToString();
            MTB_repeat_Cancel_time_L.Text = GenieConfig.MTB_repeat_Cancel_time_L.ToString();
            MTB_repeat_Cancel_time_M.Text = GenieConfig.MTB_repeat_Cancel_time_M.ToString();
            MTB_repeat_Cancel_time_N.Text = GenieConfig.MTB_repeat_Cancel_time_N.ToString();

            combo_repeat_Cancel_A.SelectedIndex = GenieConfig.combo_repeat_Cancel_A;
            combo_repeat_Cancel_B.SelectedIndex = GenieConfig.combo_repeat_Cancel_B;
            combo_repeat_Cancel_C.SelectedIndex = GenieConfig.combo_repeat_Cancel_C;
            combo_repeat_Cancel_D.SelectedIndex = GenieConfig.combo_repeat_Cancel_D;
            combo_repeat_Cancel_E.SelectedIndex = GenieConfig.combo_repeat_Cancel_E;
            combo_repeat_Cancel_F.SelectedIndex = GenieConfig.combo_repeat_Cancel_F;
            combo_repeat_Cancel_G.SelectedIndex = GenieConfig.combo_repeat_Cancel_G;
            combo_repeat_Cancel_H.SelectedIndex = GenieConfig.combo_repeat_Cancel_H;
            combo_repeat_Cancel_I.SelectedIndex = GenieConfig.combo_repeat_Cancel_I;
            combo_repeat_Cancel_J.SelectedIndex = GenieConfig.combo_repeat_Cancel_J;
            combo_repeat_Cancel_K.SelectedIndex = GenieConfig.combo_repeat_Cancel_K;
            combo_repeat_Cancel_L.SelectedIndex = GenieConfig.combo_repeat_Cancel_L;
            combo_repeat_Cancel_M.SelectedIndex = GenieConfig.combo_repeat_Cancel_M;
            combo_repeat_Cancel_N.SelectedIndex = GenieConfig.combo_repeat_Cancel_N;

            MTB_repeat_delay_A.Text = GenieConfig.MTB_repeat_delay_A.ToString();
            MTB_repeat_delay_B.Text = GenieConfig.MTB_repeat_delay_B.ToString();
            MTB_repeat_delay_C.Text = GenieConfig.MTB_repeat_delay_C.ToString();
            MTB_repeat_delay_D.Text = GenieConfig.MTB_repeat_delay_D.ToString();
            MTB_repeat_delay_E.Text = GenieConfig.MTB_repeat_delay_E.ToString();
            MTB_repeat_delay_F.Text = GenieConfig.MTB_repeat_delay_F.ToString();
            MTB_repeat_delay_G.Text = GenieConfig.MTB_repeat_delay_G.ToString();
            MTB_repeat_delay_H.Text = GenieConfig.MTB_repeat_delay_H.ToString();
            MTB_repeat_delay_I.Text = GenieConfig.MTB_repeat_delay_I.ToString();
            MTB_repeat_delay_J.Text = GenieConfig.MTB_repeat_delay_J.ToString();
            MTB_repeat_delay_K.Text = GenieConfig.MTB_repeat_delay_K.ToString();
            MTB_repeat_delay_L.Text = GenieConfig.MTB_repeat_delay_L.ToString();
            MTB_repeat_delay_M.Text = GenieConfig.MTB_repeat_delay_M.ToString();
            MTB_repeat_delay_N.Text = GenieConfig.MTB_repeat_delay_N.ToString();

            CB_repeat_use_A.Checked = GenieConfig.CB_repeat_use_A;
            CB_repeat_use_B.Checked = GenieConfig.CB_repeat_use_B;
            CB_repeat_use_C.Checked = GenieConfig.CB_repeat_use_C;
            CB_repeat_use_D.Checked = GenieConfig.CB_repeat_use_D;
            CB_repeat_use_E.Checked = GenieConfig.CB_repeat_use_E;
            CB_repeat_use_F.Checked = GenieConfig.CB_repeat_use_F;
            CB_repeat_use_G.Checked = GenieConfig.CB_repeat_use_G;
            CB_repeat_use_H.Checked = GenieConfig.CB_repeat_use_H;
            CB_repeat_use_I.Checked = GenieConfig.CB_repeat_use_I;
            CB_repeat_use_J.Checked = GenieConfig.CB_repeat_use_J;
            CB_repeat_use_K.Checked = GenieConfig.CB_repeat_use_K;
            CB_repeat_use_L.Checked = GenieConfig.CB_repeat_use_L;
            CB_repeat_use_M.Checked = GenieConfig.CB_repeat_use_M;
            CB_repeat_use_N.Checked = GenieConfig.CB_repeat_use_N;

            combo_repeat_use_condition_A.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_repeat_use_condition_A);
            combo_repeat_use_condition_B.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_repeat_use_condition_B);
            combo_repeat_use_condition_C.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_repeat_use_condition_C);
            combo_repeat_use_condition_D.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_repeat_use_condition_D);
            combo_repeat_use_condition_E.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_repeat_use_condition_E);
            combo_repeat_use_condition_F.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_repeat_use_condition_F);
            combo_repeat_use_condition_G.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_repeat_use_condition_G);
            combo_repeat_use_condition_H.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_repeat_use_condition_H);
            combo_repeat_use_condition_I.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_repeat_use_condition_I);
            combo_repeat_use_condition_J.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_repeat_use_condition_J);
            combo_repeat_use_condition_K.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_repeat_use_condition_K);
            combo_repeat_use_condition_L.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_repeat_use_condition_L);
            combo_repeat_use_condition_M.SelectedIndex =GET.GenieCombobox( GenieConfig.combo_repeat_use_condition_M);
            combo_repeat_use_condition_N.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_repeat_use_condition_N);

            TB_Repeat_매입금_A.Text = GenieConfig.TB_Repeat_매입금_A.ToString();
            TB_Repeat_매입금_B.Text = GenieConfig.TB_Repeat_매입금_B.ToString();
            TB_Repeat_매입금_C.Text = GenieConfig.TB_Repeat_매입금_C.ToString();
            TB_Repeat_매입금_D.Text = GenieConfig.TB_Repeat_매입금_D.ToString();
            TB_Repeat_매입금_E.Text = GenieConfig.TB_Repeat_매입금_E.ToString();
            TB_Repeat_매입금_F.Text = GenieConfig.TB_Repeat_매입금_F.ToString();
            TB_Repeat_매입금_G.Text = GenieConfig.TB_Repeat_매입금_G.ToString();
            TB_Repeat_매입금_H.Text = GenieConfig.TB_Repeat_매입금_H.ToString();
            TB_Repeat_매입금_I.Text = GenieConfig.TB_Repeat_매입금_I.ToString();
            TB_Repeat_매입금_J.Text = GenieConfig.TB_Repeat_매입금_J.ToString();
            TB_Repeat_매입금_K.Text = GenieConfig.TB_Repeat_매입금_K.ToString();
            TB_Repeat_매입금_L.Text = GenieConfig.TB_Repeat_매입금_L.ToString();
            TB_Repeat_매입금_M.Text = GenieConfig.TB_Repeat_매입금_M.ToString();
            TB_Repeat_매입금_N.Text = GenieConfig.TB_Repeat_매입금_N.ToString();

            TB_repeat_누적거래량_A.Text = GenieConfig.TB_repeat_누적거래량_A.ToString();
            TB_repeat_누적거래량_B.Text = GenieConfig.TB_repeat_누적거래량_B.ToString();
            TB_repeat_누적거래량_C.Text = GenieConfig.TB_repeat_누적거래량_C.ToString();
            TB_repeat_누적거래량_D.Text = GenieConfig.TB_repeat_누적거래량_D.ToString();
            TB_repeat_누적거래량_E.Text = GenieConfig.TB_repeat_누적거래량_E.ToString();
            TB_repeat_누적거래량_F.Text = GenieConfig.TB_repeat_누적거래량_F.ToString();
            TB_repeat_누적거래량_G.Text = GenieConfig.TB_repeat_누적거래량_G.ToString();
            TB_repeat_누적거래량_H.Text = GenieConfig.TB_repeat_누적거래량_H.ToString();
            TB_repeat_누적거래량_I.Text = GenieConfig.TB_repeat_누적거래량_I.ToString();
            TB_repeat_누적거래량_J.Text = GenieConfig.TB_repeat_누적거래량_J.ToString();
            TB_repeat_누적거래량_K.Text = GenieConfig.TB_repeat_누적거래량_K.ToString();
            TB_repeat_누적거래량_L.Text = GenieConfig.TB_repeat_누적거래량_L.ToString();
            TB_repeat_누적거래량_M.Text = GenieConfig.TB_repeat_누적거래량_M.ToString();
            TB_repeat_누적거래량_N.Text = GenieConfig.TB_repeat_누적거래량_N.ToString();

            TB_repeat_누적거래대금_A.Text = GenieConfig.TB_repeat_누적거래대금_A.ToString();
            TB_repeat_누적거래대금_B.Text = GenieConfig.TB_repeat_누적거래대금_B.ToString();
            TB_repeat_누적거래대금_C.Text = GenieConfig.TB_repeat_누적거래대금_C.ToString();
            TB_repeat_누적거래대금_D.Text = GenieConfig.TB_repeat_누적거래대금_D.ToString();
            TB_repeat_누적거래대금_E.Text = GenieConfig.TB_repeat_누적거래대금_E.ToString();
            TB_repeat_누적거래대금_F.Text = GenieConfig.TB_repeat_누적거래대금_F.ToString();
            TB_repeat_누적거래대금_G.Text = GenieConfig.TB_repeat_누적거래대금_G.ToString();
            TB_repeat_누적거래대금_H.Text = GenieConfig.TB_repeat_누적거래대금_H.ToString();
            TB_repeat_누적거래대금_I.Text = GenieConfig.TB_repeat_누적거래대금_I.ToString();
            TB_repeat_누적거래대금_J.Text = GenieConfig.TB_repeat_누적거래대금_J.ToString();
            TB_repeat_누적거래대금_K.Text = GenieConfig.TB_repeat_누적거래대금_K.ToString();
            TB_repeat_누적거래대금_L.Text = GenieConfig.TB_repeat_누적거래대금_L.ToString();
            TB_repeat_누적거래대금_M.Text = GenieConfig.TB_repeat_누적거래대금_M.ToString();
            TB_repeat_누적거래대금_N.Text = GenieConfig.TB_repeat_누적거래대금_N.ToString();

            TB_repeat_MinMAPeriod1_A.Text = GenieConfig.TB_repeat_MinMAPeriod1_A.ToString();
            TB_repeat_MinMAPeriod1_B.Text = GenieConfig.TB_repeat_MinMAPeriod1_B.ToString();
            TB_repeat_MinMAPeriod1_C.Text = GenieConfig.TB_repeat_MinMAPeriod1_C.ToString();
            TB_repeat_MinMAPeriod1_D.Text = GenieConfig.TB_repeat_MinMAPeriod1_D.ToString();
            TB_repeat_MinMAPeriod1_E.Text = GenieConfig.TB_repeat_MinMAPeriod1_E.ToString();
            TB_repeat_MinMAPeriod1_F.Text = GenieConfig.TB_repeat_MinMAPeriod1_F.ToString();
            TB_repeat_MinMAPeriod1_G.Text = GenieConfig.TB_repeat_MinMAPeriod1_G.ToString();
            TB_repeat_MinMAPeriod1_H.Text = GenieConfig.TB_repeat_MinMAPeriod1_H.ToString();
            TB_repeat_MinMAPeriod1_I.Text = GenieConfig.TB_repeat_MinMAPeriod1_I.ToString();
            TB_repeat_MinMAPeriod1_J.Text = GenieConfig.TB_repeat_MinMAPeriod1_J.ToString();
            TB_repeat_MinMAPeriod1_K.Text = GenieConfig.TB_repeat_MinMAPeriod1_K.ToString();
            TB_repeat_MinMAPeriod1_L.Text = GenieConfig.TB_repeat_MinMAPeriod1_L.ToString();
            TB_repeat_MinMAPeriod1_M.Text = GenieConfig.TB_repeat_MinMAPeriod1_M.ToString();
            TB_repeat_MinMAPeriod1_N.Text = GenieConfig.TB_repeat_MinMAPeriod1_N.ToString();

            CBB_repeat_MinMAPeriod1_A.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_A);
            CBB_repeat_MinMAPeriod1_B.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_B);
            CBB_repeat_MinMAPeriod1_C.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_C);
            CBB_repeat_MinMAPeriod1_D.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_D);
            CBB_repeat_MinMAPeriod1_E.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_E);
            CBB_repeat_MinMAPeriod1_F.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_F);
            CBB_repeat_MinMAPeriod1_G.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_G);
            CBB_repeat_MinMAPeriod1_H.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_H);
            CBB_repeat_MinMAPeriod1_I.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_I);
            CBB_repeat_MinMAPeriod1_J.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_J);
            CBB_repeat_MinMAPeriod1_K.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_K);
            CBB_repeat_MinMAPeriod1_L.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_L);
            CBB_repeat_MinMAPeriod1_M.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_M);
            CBB_repeat_MinMAPeriod1_N.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_N);

            TB_repeat_MinMAPeriod2_A.Text = GenieConfig.TB_repeat_MinMAPeriod2_A.ToString();
            TB_repeat_MinMAPeriod2_B.Text = GenieConfig.TB_repeat_MinMAPeriod2_B.ToString();
            TB_repeat_MinMAPeriod2_C.Text = GenieConfig.TB_repeat_MinMAPeriod2_C.ToString();
            TB_repeat_MinMAPeriod2_D.Text = GenieConfig.TB_repeat_MinMAPeriod2_D.ToString();
            TB_repeat_MinMAPeriod2_E.Text = GenieConfig.TB_repeat_MinMAPeriod2_E.ToString();
            TB_repeat_MinMAPeriod2_F.Text = GenieConfig.TB_repeat_MinMAPeriod2_F.ToString();
            TB_repeat_MinMAPeriod2_G.Text = GenieConfig.TB_repeat_MinMAPeriod2_G.ToString();
            TB_repeat_MinMAPeriod2_H.Text = GenieConfig.TB_repeat_MinMAPeriod2_H.ToString();
            TB_repeat_MinMAPeriod2_I.Text = GenieConfig.TB_repeat_MinMAPeriod2_I.ToString();
            TB_repeat_MinMAPeriod2_J.Text = GenieConfig.TB_repeat_MinMAPeriod2_J.ToString();
            TB_repeat_MinMAPeriod2_K.Text = GenieConfig.TB_repeat_MinMAPeriod2_K.ToString();
            TB_repeat_MinMAPeriod2_L.Text = GenieConfig.TB_repeat_MinMAPeriod2_L.ToString();
            TB_repeat_MinMAPeriod2_M.Text = GenieConfig.TB_repeat_MinMAPeriod2_M.ToString();
            TB_repeat_MinMAPeriod2_N.Text = GenieConfig.TB_repeat_MinMAPeriod2_N.ToString();

            CBB_repeat_MinMAPeriod2_A.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_A);
            CBB_repeat_MinMAPeriod2_B.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_B);
            CBB_repeat_MinMAPeriod2_C.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_C);
            CBB_repeat_MinMAPeriod2_D.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_D);
            CBB_repeat_MinMAPeriod2_E.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_E);
            CBB_repeat_MinMAPeriod2_F.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_F);
            CBB_repeat_MinMAPeriod2_G.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_G);
            CBB_repeat_MinMAPeriod2_H.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_H);
            CBB_repeat_MinMAPeriod2_I.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_I);
            CBB_repeat_MinMAPeriod2_J.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_J);
            CBB_repeat_MinMAPeriod2_K.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_K);
            CBB_repeat_MinMAPeriod2_L.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_L);
            CBB_repeat_MinMAPeriod2_M.SelectedIndex =GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_M);
            CBB_repeat_MinMAPeriod2_N.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod2_N);

            CBB_repeat_MinMAPeriod1_배열_A.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_MinMAPeriod1_배열_A);
            CBB_repeat_MinMAPeriod1_배열_B.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_MinMAPeriod1_배열_B);
            CBB_repeat_MinMAPeriod1_배열_C.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_MinMAPeriod1_배열_C);
            CBB_repeat_MinMAPeriod1_배열_D.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_MinMAPeriod1_배열_D);
            CBB_repeat_MinMAPeriod1_배열_E.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_MinMAPeriod1_배열_E);
            CBB_repeat_MinMAPeriod1_배열_F.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_MinMAPeriod1_배열_F);
            CBB_repeat_MinMAPeriod1_배열_G.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_MinMAPeriod1_배열_G);
            CBB_repeat_MinMAPeriod1_배열_H.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_MinMAPeriod1_배열_H);
            CBB_repeat_MinMAPeriod1_배열_I.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_MinMAPeriod1_배열_I);
            CBB_repeat_MinMAPeriod1_배열_J.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_MinMAPeriod1_배열_J);
            CBB_repeat_MinMAPeriod1_배열_K.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_MinMAPeriod1_배열_K);
            CBB_repeat_MinMAPeriod1_배열_L.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_MinMAPeriod1_배열_L);
            CBB_repeat_MinMAPeriod1_배열_M.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_MinMAPeriod1_배열_M);
            CBB_repeat_MinMAPeriod1_배열_N.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_repeat_MinMAPeriod1_배열_N);

            TB_repeat_DayMAPeriod1_A.Text = GenieConfig.TB_repeat_DayMAPeriod1_A.ToString();
            TB_repeat_DayMAPeriod1_B.Text = GenieConfig.TB_repeat_DayMAPeriod1_B.ToString();
            TB_repeat_DayMAPeriod1_C.Text = GenieConfig.TB_repeat_DayMAPeriod1_C.ToString();
            TB_repeat_DayMAPeriod1_D.Text = GenieConfig.TB_repeat_DayMAPeriod1_D.ToString();
            TB_repeat_DayMAPeriod1_E.Text = GenieConfig.TB_repeat_DayMAPeriod1_E.ToString();
            TB_repeat_DayMAPeriod1_F.Text = GenieConfig.TB_repeat_DayMAPeriod1_F.ToString();
            TB_repeat_DayMAPeriod1_G.Text = GenieConfig.TB_repeat_DayMAPeriod1_G.ToString();
            TB_repeat_DayMAPeriod1_H.Text = GenieConfig.TB_repeat_DayMAPeriod1_H.ToString();
            TB_repeat_DayMAPeriod1_I.Text = GenieConfig.TB_repeat_DayMAPeriod1_I.ToString();
            TB_repeat_DayMAPeriod1_J.Text = GenieConfig.TB_repeat_DayMAPeriod1_J.ToString();
            TB_repeat_DayMAPeriod1_K.Text = GenieConfig.TB_repeat_DayMAPeriod1_K.ToString();
            TB_repeat_DayMAPeriod1_L.Text = GenieConfig.TB_repeat_DayMAPeriod1_L.ToString();
            TB_repeat_DayMAPeriod1_M.Text = GenieConfig.TB_repeat_DayMAPeriod1_M.ToString();
            TB_repeat_DayMAPeriod1_N.Text = GenieConfig.TB_repeat_DayMAPeriod1_N.ToString();

            CBB_repeat_DayMAPeriod1_A.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod1_A);
            CBB_repeat_DayMAPeriod1_B.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod1_B);
            CBB_repeat_DayMAPeriod1_C.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod1_C);
            CBB_repeat_DayMAPeriod1_D.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod1_D);
            CBB_repeat_DayMAPeriod1_E.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod1_E);
            CBB_repeat_DayMAPeriod1_F.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod1_F);
            CBB_repeat_DayMAPeriod1_G.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod1_G);
            CBB_repeat_DayMAPeriod1_H.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod1_H);
            CBB_repeat_DayMAPeriod1_I.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod1_I);
            CBB_repeat_DayMAPeriod1_J.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod1_J);
            CBB_repeat_DayMAPeriod1_K.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod1_K);
            CBB_repeat_DayMAPeriod1_L.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod1_L);
            CBB_repeat_DayMAPeriod1_M.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod1_M);
            CBB_repeat_DayMAPeriod1_N.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_repeat_DayMAPeriod1_N);

            TB_repeat_DayMAPeriod2_A.Text = GenieConfig.TB_repeat_DayMAPeriod2_A.ToString();
            TB_repeat_DayMAPeriod2_B.Text = GenieConfig.TB_repeat_DayMAPeriod2_B.ToString();
            TB_repeat_DayMAPeriod2_C.Text = GenieConfig.TB_repeat_DayMAPeriod2_C.ToString();
            TB_repeat_DayMAPeriod2_D.Text = GenieConfig.TB_repeat_DayMAPeriod2_D.ToString();
            TB_repeat_DayMAPeriod2_E.Text = GenieConfig.TB_repeat_DayMAPeriod2_E.ToString();
            TB_repeat_DayMAPeriod2_F.Text = GenieConfig.TB_repeat_DayMAPeriod2_F.ToString();
            TB_repeat_DayMAPeriod2_G.Text = GenieConfig.TB_repeat_DayMAPeriod2_G.ToString();
            TB_repeat_DayMAPeriod2_H.Text = GenieConfig.TB_repeat_DayMAPeriod2_H.ToString();
            TB_repeat_DayMAPeriod2_I.Text = GenieConfig.TB_repeat_DayMAPeriod2_I.ToString();
            TB_repeat_DayMAPeriod2_J.Text = GenieConfig.TB_repeat_DayMAPeriod2_J.ToString();
            TB_repeat_DayMAPeriod2_K.Text = GenieConfig.TB_repeat_DayMAPeriod2_K.ToString();
            TB_repeat_DayMAPeriod2_L.Text = GenieConfig.TB_repeat_DayMAPeriod2_L.ToString();
            TB_repeat_DayMAPeriod2_M.Text = GenieConfig.TB_repeat_DayMAPeriod2_M.ToString();
            TB_repeat_DayMAPeriod2_N.Text = GenieConfig.TB_repeat_DayMAPeriod2_N.ToString();

            CBB_repeat_DayMAPeriod2_A.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod2_A);
            CBB_repeat_DayMAPeriod2_B.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod2_B);
            CBB_repeat_DayMAPeriod2_C.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod2_C);
            CBB_repeat_DayMAPeriod2_D.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod2_D);
            CBB_repeat_DayMAPeriod2_E.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod2_E);
            CBB_repeat_DayMAPeriod2_F.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod2_F);
            CBB_repeat_DayMAPeriod2_G.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod2_G);
            CBB_repeat_DayMAPeriod2_H.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod2_H);
            CBB_repeat_DayMAPeriod2_I.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod2_I);
            CBB_repeat_DayMAPeriod2_J.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod2_J);
            CBB_repeat_DayMAPeriod2_K.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod2_K);
            CBB_repeat_DayMAPeriod2_L.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod2_L);
            CBB_repeat_DayMAPeriod2_M.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod2_M);
            CBB_repeat_DayMAPeriod2_N.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_repeat_DayMAPeriod2_N);

            CBB_repeat_DayMAPeriod_배열_A.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod_배열_A);
            CBB_repeat_DayMAPeriod_배열_B.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod_배열_B);
            CBB_repeat_DayMAPeriod_배열_C.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod_배열_C);
            CBB_repeat_DayMAPeriod_배열_D.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod_배열_D);
            CBB_repeat_DayMAPeriod_배열_E.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod_배열_E);
            CBB_repeat_DayMAPeriod_배열_F.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod_배열_F);
            CBB_repeat_DayMAPeriod_배열_G.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod_배열_G);
            CBB_repeat_DayMAPeriod_배열_H.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod_배열_H);
            CBB_repeat_DayMAPeriod_배열_I.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod_배열_I);
            CBB_repeat_DayMAPeriod_배열_J.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod_배열_J);
            CBB_repeat_DayMAPeriod_배열_K.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod_배열_K);
            CBB_repeat_DayMAPeriod_배열_L.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod_배열_L);
            CBB_repeat_DayMAPeriod_배열_M.SelectedIndex =GET.GenieCombobox( GenieConfig.CBB_repeat_DayMAPeriod_배열_M);
            CBB_repeat_DayMAPeriod_배열_N.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_repeat_DayMAPeriod_배열_N);

            TB_반복_추매주가이상.Text = GenieConfig.TB_반복_추매주가이상.ToString("N0");
            TB_반복_추매주가이하.Text = GenieConfig.TB_반복_추매주가이하.ToString("N0");
            TB_반복_추매등락률이상.Text = GenieConfig.TB_반복_추매등락률이상.ToString();
            TB_반복_추매등락률이하.Text = GenieConfig.TB_반복_추매등락률이하.ToString();

            if (!GenieConfig.CB_가이드매매)
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

                form.반복_A.Enabled = true;
                form.반복_B.Enabled = true;
                form.반복_C.Enabled = true;
                form.반복_D.Enabled = true;
                form.반복_E.Enabled = true;
                form.반복_F.Enabled = true;
                form.반복_G.Enabled = true;
                form.반복_H.Enabled = true;
                form.반복_I.Enabled = true;
                form.반복_J.Enabled = true;
                form.반복_K.Enabled = true;
                form.반복_L.Enabled = true;
                form.반복_M.Enabled = true;
                form.반복_N.Enabled = true;
            }
            else
            {
                ControllerDisable.Form_Repeat_Disable();
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

            this.ActiveControl = 반복_N;
            Form1.음소거 = GenieConfig.CB_음소거;

        }

        public static void 반복매매_저장() // 반복매매 저장
        {
            ComboBox combo_jumun = form.combo_repeat_jumun_A;
            ComboBox combo_Cancel = form.combo_repeat_Cancel_A;
            MaskedTextBox MTB = form.MTB_repeat_repeat_A;
            TextBox TB = form.TB_repeat_value_A;
            cancel_sell_reset();

            combo_jumun = form.combo_repeat_jumun_B;
            combo_Cancel = form.combo_repeat_Cancel_B;
            MTB = form.MTB_repeat_repeat_B;
            TB = form.TB_repeat_value_B;
            cancel_sell_reset();

            combo_jumun = form.combo_repeat_jumun_C;
            combo_Cancel = form.combo_repeat_Cancel_C;
            MTB = form.MTB_repeat_repeat_C;
            TB = form.TB_repeat_value_C;
            cancel_sell_reset();

            combo_jumun = form.combo_repeat_jumun_D;
            combo_Cancel = form.combo_repeat_Cancel_D;
            MTB = form.MTB_repeat_repeat_D;
            TB = form.TB_repeat_value_D;
            cancel_sell_reset();

            combo_jumun = form.combo_repeat_jumun_E;
            combo_Cancel = form.combo_repeat_Cancel_E;
            MTB = form.MTB_repeat_repeat_E;
            TB = form.TB_repeat_value_E;
            cancel_sell_reset();

            combo_jumun = form.combo_repeat_jumun_F;
            combo_Cancel = form.combo_repeat_Cancel_F;
            MTB = form.MTB_repeat_repeat_F;
            TB = form.TB_repeat_value_F;
            cancel_sell_reset();

            combo_jumun = form.combo_repeat_jumun_G;
            combo_Cancel = form.combo_repeat_Cancel_G;
            MTB = form.MTB_repeat_repeat_G;
            TB = form.TB_repeat_value_G;
            cancel_sell_reset();

            combo_jumun = form.combo_repeat_jumun_H;
            combo_Cancel = form.combo_repeat_Cancel_H;
            MTB = form.MTB_repeat_repeat_H;
            TB = form.TB_repeat_value_H;
            cancel_sell_reset();

            combo_jumun = form.combo_repeat_jumun_I;
            combo_Cancel = form.combo_repeat_Cancel_I;
            MTB = form.MTB_repeat_repeat_I;
            TB = form.TB_repeat_value_I;
            cancel_sell_reset();

            combo_jumun = form.combo_repeat_jumun_J;
            combo_Cancel = form.combo_repeat_Cancel_J;
            MTB = form.MTB_repeat_repeat_J;
            TB = form.TB_repeat_value_J;
            cancel_sell_reset();

            combo_jumun = form.combo_repeat_jumun_K;
            combo_Cancel = form.combo_repeat_Cancel_K;
            MTB = form.MTB_repeat_repeat_K;
            TB = form.TB_repeat_value_K;
            cancel_sell_reset();

            combo_jumun = form.combo_repeat_jumun_L;
            combo_Cancel = form.combo_repeat_Cancel_L;
            MTB = form.MTB_repeat_repeat_L;
            TB = form.TB_repeat_value_L;
            cancel_sell_reset();

            combo_jumun = form.combo_repeat_jumun_M;
            combo_Cancel = form.combo_repeat_Cancel_M;
            MTB = form.MTB_repeat_repeat_M;
            TB = form.TB_repeat_value_M;
            cancel_sell_reset();

            combo_jumun = form.combo_repeat_jumun_N;
            combo_Cancel = form.combo_repeat_Cancel_N;
            MTB = form.MTB_repeat_repeat_N;
            TB = form.TB_repeat_value_N;
            cancel_sell_reset();

            void cancel_sell_reset()
            {
                if (combo_jumun.SelectedIndex > 3)
                {
                    combo_Cancel.SelectedIndex = 0;
                    MTB.Text = "0";
                    TB.Text = "0";
                }
            }

            try
            {
                GenieConfig.combo_repeat_jumun_A = GET.ComboBoxIndex(form.combo_repeat_jumun_A);
                GenieConfig.combo_repeat_jumun_B = GET.ComboBoxIndex(form.combo_repeat_jumun_B);
                GenieConfig.combo_repeat_jumun_C = GET.ComboBoxIndex(form.combo_repeat_jumun_C);
                GenieConfig.combo_repeat_jumun_D = GET.ComboBoxIndex(form.combo_repeat_jumun_D);
                GenieConfig.combo_repeat_jumun_E = GET.ComboBoxIndex(form.combo_repeat_jumun_E);
                GenieConfig.combo_repeat_jumun_F = GET.ComboBoxIndex(form.combo_repeat_jumun_F);
                GenieConfig.combo_repeat_jumun_G = GET.ComboBoxIndex(form.combo_repeat_jumun_G);
                GenieConfig.combo_repeat_jumun_H = GET.ComboBoxIndex(form.combo_repeat_jumun_H);
                GenieConfig.combo_repeat_jumun_I = GET.ComboBoxIndex(form.combo_repeat_jumun_I);
                GenieConfig.combo_repeat_jumun_J = GET.ComboBoxIndex(form.combo_repeat_jumun_J);
                GenieConfig.combo_repeat_jumun_K = GET.ComboBoxIndex(form.combo_repeat_jumun_K);
                GenieConfig.combo_repeat_jumun_L = GET.ComboBoxIndex(form.combo_repeat_jumun_L);
                GenieConfig.combo_repeat_jumun_M = GET.ComboBoxIndex(form.combo_repeat_jumun_M);
                GenieConfig.combo_repeat_jumun_N = GET.ComboBoxIndex(form.combo_repeat_jumun_N);
            }
            catch (Exception e)
            {
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
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

                int start_A = GET.Start_stop_time(true, _start_A);
                int start_B = GET.Start_stop_time(true, _start_B);
                int start_C = GET.Start_stop_time(true, _start_C);
                int start_D = GET.Start_stop_time(true, _start_D);
                int start_E = GET.Start_stop_time(true, _start_E);
                int start_F = GET.Start_stop_time(true, _start_F);
                int start_G = GET.Start_stop_time(true, _start_G);
                int start_H = GET.Start_stop_time(true, _start_H);
                int start_I = GET.Start_stop_time(true, _start_I);
                int start_J = GET.Start_stop_time(true, _start_J);
                int start_K = GET.Start_stop_time(true, _start_K);
                int start_L = GET.Start_stop_time(true, _start_L);
                int start_M = GET.Start_stop_time(true, _start_M);
                int start_N = GET.Start_stop_time(true, _start_N);

                GenieConfig.MT_repeat_time_start_A = start_A;
                GenieConfig.MT_repeat_time_start_B = start_B;
                GenieConfig.MT_repeat_time_start_C = start_C;
                GenieConfig.MT_repeat_time_start_D = start_D;
                GenieConfig.MT_repeat_time_start_E = start_E;
                GenieConfig.MT_repeat_time_start_F = start_F;
                GenieConfig.MT_repeat_time_start_G = start_G;
                GenieConfig.MT_repeat_time_start_H = start_H;
                GenieConfig.MT_repeat_time_start_I = start_I;
                GenieConfig.MT_repeat_time_start_J = start_J;
                GenieConfig.MT_repeat_time_start_K = start_K;
                GenieConfig.MT_repeat_time_start_L = start_L;
                GenieConfig.MT_repeat_time_start_M = start_M;
                GenieConfig.MT_repeat_time_start_N = start_N;

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
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
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

                int end_A = GET.Start_stop_time(false, _end_A);
                int end_B = GET.Start_stop_time(false, _end_B);
                int end_C = GET.Start_stop_time(false, _end_C);
                int end_D = GET.Start_stop_time(false, _end_D);
                int end_E = GET.Start_stop_time(false, _end_E);
                int end_F = GET.Start_stop_time(false, _end_F);
                int end_G = GET.Start_stop_time(false, _end_G);
                int end_H = GET.Start_stop_time(false, _end_H);
                int end_I = GET.Start_stop_time(false, _end_I);
                int end_J = GET.Start_stop_time(false, _end_J);
                int end_K = GET.Start_stop_time(false, _end_K);
                int end_L = GET.Start_stop_time(false, _end_L);
                int end_M = GET.Start_stop_time(false, _end_M);
                int end_N = GET.Start_stop_time(false, _end_N);

                GenieConfig.MT_repeat_time_end_A = end_A;
                GenieConfig.MT_repeat_time_end_B = end_B;
                GenieConfig.MT_repeat_time_end_C = end_C;
                GenieConfig.MT_repeat_time_end_D = end_D;
                GenieConfig.MT_repeat_time_end_E = end_E;
                GenieConfig.MT_repeat_time_end_F = end_F;
                GenieConfig.MT_repeat_time_end_G = end_G;
                GenieConfig.MT_repeat_time_end_H = end_H;
                GenieConfig.MT_repeat_time_end_I = end_I;
                GenieConfig.MT_repeat_time_end_J = end_J;
                GenieConfig.MT_repeat_time_end_K = end_K;
                GenieConfig.MT_repeat_time_end_L = end_L;
                GenieConfig.MT_repeat_time_end_M = end_M;
                GenieConfig.MT_repeat_time_end_N = end_N;

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
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
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

                GenieConfig.MTB_repeat_delay_A = delay_A;
                GenieConfig.MTB_repeat_delay_B = delay_B;
                GenieConfig.MTB_repeat_delay_C = delay_C;
                GenieConfig.MTB_repeat_delay_D = delay_D;
                GenieConfig.MTB_repeat_delay_E = delay_E;
                GenieConfig.MTB_repeat_delay_F = delay_F;
                GenieConfig.MTB_repeat_delay_G = delay_G;
                GenieConfig.MTB_repeat_delay_H = delay_H;
                GenieConfig.MTB_repeat_delay_I = delay_I;
                GenieConfig.MTB_repeat_delay_J = delay_J;
                GenieConfig.MTB_repeat_delay_K = delay_K;
                GenieConfig.MTB_repeat_delay_L = delay_L;
                GenieConfig.MTB_repeat_delay_M = delay_M;
                GenieConfig.MTB_repeat_delay_N = delay_N;

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
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
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

                GenieConfig.TB_repeat_suik_1_A = suik_1_A;
                GenieConfig.TB_repeat_suik_1_B = suik_1_B;
                GenieConfig.TB_repeat_suik_1_C = suik_1_C;
                GenieConfig.TB_repeat_suik_1_D = suik_1_D;
                GenieConfig.TB_repeat_suik_1_E = suik_1_E;
                GenieConfig.TB_repeat_suik_1_F = suik_1_F;
                GenieConfig.TB_repeat_suik_1_G = suik_1_G;
                GenieConfig.TB_repeat_suik_1_H = suik_1_H;
                GenieConfig.TB_repeat_suik_1_I = suik_1_I;
                GenieConfig.TB_repeat_suik_1_J = suik_1_J;
                GenieConfig.TB_repeat_suik_1_K = suik_1_K;
                GenieConfig.TB_repeat_suik_1_L = suik_1_L;
                GenieConfig.TB_repeat_suik_1_M = suik_1_M;
                GenieConfig.TB_repeat_suik_1_N = suik_1_N;

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
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
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

                GenieConfig.TB_repeat_suik_2_A = suik_2_A;
                GenieConfig.TB_repeat_suik_2_B = suik_2_B;
                GenieConfig.TB_repeat_suik_2_C = suik_2_C;
                GenieConfig.TB_repeat_suik_2_D = suik_2_D;
                GenieConfig.TB_repeat_suik_2_E = suik_2_E;
                GenieConfig.TB_repeat_suik_2_F = suik_2_F;
                GenieConfig.TB_repeat_suik_2_G = suik_2_G;
                GenieConfig.TB_repeat_suik_2_H = suik_2_H;
                GenieConfig.TB_repeat_suik_2_I = suik_2_I;
                GenieConfig.TB_repeat_suik_2_J = suik_2_J;
                GenieConfig.TB_repeat_suik_2_K = suik_2_K;
                GenieConfig.TB_repeat_suik_2_L = suik_2_L;
                GenieConfig.TB_repeat_suik_2_M = suik_2_M;
                GenieConfig.TB_repeat_suik_2_N = suik_2_N;

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
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
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

                GenieConfig.TB_repeat_sell_ratio_A = Math.Abs(Ratio_A);
                GenieConfig.TB_repeat_sell_ratio_B = Math.Abs(Ratio_B);
                GenieConfig.TB_repeat_sell_ratio_C = Math.Abs(Ratio_C);
                GenieConfig.TB_repeat_sell_ratio_D = Math.Abs(Ratio_D);
                GenieConfig.TB_repeat_sell_ratio_E = Math.Abs(Ratio_E);
                GenieConfig.TB_repeat_sell_ratio_F = Math.Abs(Ratio_F);
                GenieConfig.TB_repeat_sell_ratio_G = Math.Abs(Ratio_G);
                GenieConfig.TB_repeat_sell_ratio_H = Math.Abs(Ratio_H);
                GenieConfig.TB_repeat_sell_ratio_I = Math.Abs(Ratio_I);
                GenieConfig.TB_repeat_sell_ratio_J = Math.Abs(Ratio_J);
                GenieConfig.TB_repeat_sell_ratio_K = Math.Abs(Ratio_K);
                GenieConfig.TB_repeat_sell_ratio_L = Math.Abs(Ratio_L);
                GenieConfig.TB_repeat_sell_ratio_M = Math.Abs(Ratio_M);
                GenieConfig.TB_repeat_sell_ratio_N = Math.Abs(Ratio_N);

                form.TB_repeat_sell_ratio_A.Text = GenieConfig.TB_repeat_sell_ratio_A.ToString();
                form.TB_repeat_sell_ratio_B.Text = GenieConfig.TB_repeat_sell_ratio_B.ToString();
                form.TB_repeat_sell_ratio_C.Text = GenieConfig.TB_repeat_sell_ratio_C.ToString();
                form.TB_repeat_sell_ratio_D.Text = GenieConfig.TB_repeat_sell_ratio_D.ToString();
                form.TB_repeat_sell_ratio_E.Text = GenieConfig.TB_repeat_sell_ratio_E.ToString();
                form.TB_repeat_sell_ratio_F.Text = GenieConfig.TB_repeat_sell_ratio_F.ToString();
                form.TB_repeat_sell_ratio_G.Text = GenieConfig.TB_repeat_sell_ratio_G.ToString();
                form.TB_repeat_sell_ratio_H.Text = GenieConfig.TB_repeat_sell_ratio_H.ToString();
                form.TB_repeat_sell_ratio_I.Text = GenieConfig.TB_repeat_sell_ratio_I.ToString();
                form.TB_repeat_sell_ratio_J.Text = GenieConfig.TB_repeat_sell_ratio_J.ToString();
                form.TB_repeat_sell_ratio_K.Text = GenieConfig.TB_repeat_sell_ratio_K.ToString();
                form.TB_repeat_sell_ratio_L.Text = GenieConfig.TB_repeat_sell_ratio_L.ToString();
                form.TB_repeat_sell_ratio_M.Text = GenieConfig.TB_repeat_sell_ratio_M.ToString();
                form.TB_repeat_sell_ratio_N.Text = GenieConfig.TB_repeat_sell_ratio_N.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_Repeat_매입금_A.Text, out double Repeat_매입금_A);
                double.TryParse(form.TB_Repeat_매입금_B.Text, out double Repeat_매입금_B);
                double.TryParse(form.TB_Repeat_매입금_C.Text, out double Repeat_매입금_C);
                double.TryParse(form.TB_Repeat_매입금_D.Text, out double Repeat_매입금_D);
                double.TryParse(form.TB_Repeat_매입금_E.Text, out double Repeat_매입금_E);
                double.TryParse(form.TB_Repeat_매입금_F.Text, out double Repeat_매입금_F);
                double.TryParse(form.TB_Repeat_매입금_G.Text, out double Repeat_매입금_G);
                double.TryParse(form.TB_Repeat_매입금_H.Text, out double Repeat_매입금_H);
                double.TryParse(form.TB_Repeat_매입금_I.Text, out double Repeat_매입금_I);
                double.TryParse(form.TB_Repeat_매입금_J.Text, out double Repeat_매입금_J);
                double.TryParse(form.TB_Repeat_매입금_K.Text, out double Repeat_매입금_K);
                double.TryParse(form.TB_Repeat_매입금_L.Text, out double Repeat_매입금_L);
                double.TryParse(form.TB_Repeat_매입금_M.Text, out double Repeat_매입금_M);
                double.TryParse(form.TB_Repeat_매입금_N.Text, out double Repeat_매입금_N);

                GenieConfig.TB_Repeat_매입금_A = Math.Abs(Repeat_매입금_A);
                GenieConfig.TB_Repeat_매입금_B = Math.Abs(Repeat_매입금_B);
                GenieConfig.TB_Repeat_매입금_C = Math.Abs(Repeat_매입금_C);
                GenieConfig.TB_Repeat_매입금_D = Math.Abs(Repeat_매입금_D);
                GenieConfig.TB_Repeat_매입금_E = Math.Abs(Repeat_매입금_E);
                GenieConfig.TB_Repeat_매입금_F = Math.Abs(Repeat_매입금_F);
                GenieConfig.TB_Repeat_매입금_G = Math.Abs(Repeat_매입금_G);
                GenieConfig.TB_Repeat_매입금_H = Math.Abs(Repeat_매입금_H);
                GenieConfig.TB_Repeat_매입금_I = Math.Abs(Repeat_매입금_I);
                GenieConfig.TB_Repeat_매입금_J = Math.Abs(Repeat_매입금_J);
                GenieConfig.TB_Repeat_매입금_K = Math.Abs(Repeat_매입금_K);
                GenieConfig.TB_Repeat_매입금_L = Math.Abs(Repeat_매입금_L);
                GenieConfig.TB_Repeat_매입금_M = Math.Abs(Repeat_매입금_M);
                GenieConfig.TB_Repeat_매입금_N = Math.Abs(Repeat_매입금_N);

                form.TB_Repeat_매입금_A.Text = GenieConfig.TB_Repeat_매입금_A.ToString();
                form.TB_Repeat_매입금_B.Text = GenieConfig.TB_Repeat_매입금_B.ToString();
                form.TB_Repeat_매입금_C.Text = GenieConfig.TB_Repeat_매입금_C.ToString();
                form.TB_Repeat_매입금_D.Text = GenieConfig.TB_Repeat_매입금_D.ToString();
                form.TB_Repeat_매입금_E.Text = GenieConfig.TB_Repeat_매입금_E.ToString();
                form.TB_Repeat_매입금_F.Text = GenieConfig.TB_Repeat_매입금_F.ToString();
                form.TB_Repeat_매입금_G.Text = GenieConfig.TB_Repeat_매입금_G.ToString();
                form.TB_Repeat_매입금_H.Text = GenieConfig.TB_Repeat_매입금_H.ToString();
                form.TB_Repeat_매입금_I.Text = GenieConfig.TB_Repeat_매입금_I.ToString();
                form.TB_Repeat_매입금_J.Text = GenieConfig.TB_Repeat_매입금_J.ToString();
                form.TB_Repeat_매입금_K.Text = GenieConfig.TB_Repeat_매입금_K.ToString();
                form.TB_Repeat_매입금_L.Text = GenieConfig.TB_Repeat_매입금_L.ToString();
                form.TB_Repeat_매입금_M.Text = GenieConfig.TB_Repeat_매입금_M.ToString();
                form.TB_Repeat_매입금_N.Text = GenieConfig.TB_Repeat_매입금_N.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                double.TryParse(form.TB_Repeat_maemae_1_A.Text, out double maemae_1_A);
                double.TryParse(form.TB_Repeat_maemae_1_B.Text, out double maemae_1_B);
                double.TryParse(form.TB_Repeat_maemae_1_C.Text, out double maemae_1_C);
                double.TryParse(form.TB_Repeat_maemae_1_D.Text, out double maemae_1_D);
                double.TryParse(form.TB_Repeat_maemae_1_E.Text, out double maemae_1_E);
                double.TryParse(form.TB_Repeat_maemae_1_F.Text, out double maemae_1_F);
                double.TryParse(form.TB_Repeat_maemae_1_G.Text, out double maemae_1_G);
                double.TryParse(form.TB_Repeat_maemae_1_H.Text, out double maemae_1_H);
                double.TryParse(form.TB_Repeat_maemae_1_I.Text, out double maemae_1_I);
                double.TryParse(form.TB_Repeat_maemae_1_J.Text, out double maemae_1_J);
                double.TryParse(form.TB_Repeat_maemae_1_K.Text, out double maemae_1_K);
                double.TryParse(form.TB_Repeat_maemae_1_L.Text, out double maemae_1_L);
                double.TryParse(form.TB_Repeat_maemae_1_M.Text, out double maemae_1_M);
                double.TryParse(form.TB_Repeat_maemae_1_N.Text, out double maemae_1_N);

                GenieConfig.TB_Repeat_maemae_1_A = Math.Abs(maemae_1_A);
                GenieConfig.TB_Repeat_maemae_1_B = Math.Abs(maemae_1_B);
                GenieConfig.TB_Repeat_maemae_1_C = Math.Abs(maemae_1_C);
                GenieConfig.TB_Repeat_maemae_1_D = Math.Abs(maemae_1_D);
                GenieConfig.TB_Repeat_maemae_1_E = Math.Abs(maemae_1_E);
                GenieConfig.TB_Repeat_maemae_1_F = Math.Abs(maemae_1_F);
                GenieConfig.TB_Repeat_maemae_1_G = Math.Abs(maemae_1_G);
                GenieConfig.TB_Repeat_maemae_1_H = Math.Abs(maemae_1_H);
                GenieConfig.TB_Repeat_maemae_1_I = Math.Abs(maemae_1_I);
                GenieConfig.TB_Repeat_maemae_1_J = Math.Abs(maemae_1_J);
                GenieConfig.TB_Repeat_maemae_1_K = Math.Abs(maemae_1_K);
                GenieConfig.TB_Repeat_maemae_1_L = Math.Abs(maemae_1_L);
                GenieConfig.TB_Repeat_maemae_1_M = Math.Abs(maemae_1_M);
                GenieConfig.TB_Repeat_maemae_1_N = Math.Abs(maemae_1_N);

                form.TB_Repeat_maemae_1_A.Text = GenieConfig.TB_Repeat_maemae_1_A.ToString();
                form.TB_Repeat_maemae_1_B.Text = GenieConfig.TB_Repeat_maemae_1_B.ToString();
                form.TB_Repeat_maemae_1_C.Text = GenieConfig.TB_Repeat_maemae_1_C.ToString();
                form.TB_Repeat_maemae_1_D.Text = GenieConfig.TB_Repeat_maemae_1_D.ToString();
                form.TB_Repeat_maemae_1_E.Text = GenieConfig.TB_Repeat_maemae_1_E.ToString();
                form.TB_Repeat_maemae_1_F.Text = GenieConfig.TB_Repeat_maemae_1_F.ToString();
                form.TB_Repeat_maemae_1_G.Text = GenieConfig.TB_Repeat_maemae_1_G.ToString();
                form.TB_Repeat_maemae_1_H.Text = GenieConfig.TB_Repeat_maemae_1_H.ToString();
                form.TB_Repeat_maemae_1_I.Text = GenieConfig.TB_Repeat_maemae_1_I.ToString();
                form.TB_Repeat_maemae_1_J.Text = GenieConfig.TB_Repeat_maemae_1_J.ToString();
                form.TB_Repeat_maemae_1_K.Text = GenieConfig.TB_Repeat_maemae_1_K.ToString();
                form.TB_Repeat_maemae_1_L.Text = GenieConfig.TB_Repeat_maemae_1_L.ToString();
                form.TB_Repeat_maemae_1_M.Text = GenieConfig.TB_Repeat_maemae_1_M.ToString();
                form.TB_Repeat_maemae_1_N.Text = GenieConfig.TB_Repeat_maemae_1_N.ToString();

                double.TryParse(form.TB_Repeat_maemae_2_A.Text, out double maemae_2_A);
                double.TryParse(form.TB_Repeat_maemae_2_B.Text, out double maemae_2_B);
                double.TryParse(form.TB_Repeat_maemae_2_C.Text, out double maemae_2_C);
                double.TryParse(form.TB_Repeat_maemae_2_D.Text, out double maemae_2_D);
                double.TryParse(form.TB_Repeat_maemae_2_E.Text, out double maemae_2_E);
                double.TryParse(form.TB_Repeat_maemae_2_F.Text, out double maemae_2_F);
                double.TryParse(form.TB_Repeat_maemae_2_G.Text, out double maemae_2_G);
                double.TryParse(form.TB_Repeat_maemae_2_H.Text, out double maemae_2_H);
                double.TryParse(form.TB_Repeat_maemae_2_I.Text, out double maemae_2_I);
                double.TryParse(form.TB_Repeat_maemae_2_J.Text, out double maemae_2_J);
                double.TryParse(form.TB_Repeat_maemae_2_K.Text, out double maemae_2_K);
                double.TryParse(form.TB_Repeat_maemae_2_L.Text, out double maemae_2_L);
                double.TryParse(form.TB_Repeat_maemae_2_M.Text, out double maemae_2_M);
                double.TryParse(form.TB_Repeat_maemae_2_N.Text, out double maemae_2_N);

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

                GenieConfig.TB_Repeat_maemae_2_A = Math.Abs(maemae_2_A);
                GenieConfig.TB_Repeat_maemae_2_B = Math.Abs(maemae_2_B);
                GenieConfig.TB_Repeat_maemae_2_C = Math.Abs(maemae_2_C);
                GenieConfig.TB_Repeat_maemae_2_D = Math.Abs(maemae_2_D);
                GenieConfig.TB_Repeat_maemae_2_E = Math.Abs(maemae_2_E);
                GenieConfig.TB_Repeat_maemae_2_F = Math.Abs(maemae_2_F);
                GenieConfig.TB_Repeat_maemae_2_G = Math.Abs(maemae_2_G);
                GenieConfig.TB_Repeat_maemae_2_H = Math.Abs(maemae_2_H);
                GenieConfig.TB_Repeat_maemae_2_I = Math.Abs(maemae_2_I);
                GenieConfig.TB_Repeat_maemae_2_J = Math.Abs(maemae_2_J);
                GenieConfig.TB_Repeat_maemae_2_K = Math.Abs(maemae_2_K);
                GenieConfig.TB_Repeat_maemae_2_L = Math.Abs(maemae_2_L);
                GenieConfig.TB_Repeat_maemae_2_M = Math.Abs(maemae_2_M);
                GenieConfig.TB_Repeat_maemae_2_N = Math.Abs(maemae_2_N);

                form.TB_Repeat_maemae_2_A.Text = GenieConfig.TB_Repeat_maemae_2_A.ToString();
                form.TB_Repeat_maemae_2_B.Text = GenieConfig.TB_Repeat_maemae_2_B.ToString();
                form.TB_Repeat_maemae_2_C.Text = GenieConfig.TB_Repeat_maemae_2_C.ToString();
                form.TB_Repeat_maemae_2_D.Text = GenieConfig.TB_Repeat_maemae_2_D.ToString();
                form.TB_Repeat_maemae_2_E.Text = GenieConfig.TB_Repeat_maemae_2_E.ToString();
                form.TB_Repeat_maemae_2_F.Text = GenieConfig.TB_Repeat_maemae_2_F.ToString();
                form.TB_Repeat_maemae_2_G.Text = GenieConfig.TB_Repeat_maemae_2_G.ToString();
                form.TB_Repeat_maemae_2_H.Text = GenieConfig.TB_Repeat_maemae_2_H.ToString();
                form.TB_Repeat_maemae_2_I.Text = GenieConfig.TB_Repeat_maemae_2_I.ToString();
                form.TB_Repeat_maemae_2_J.Text = GenieConfig.TB_Repeat_maemae_2_J.ToString();
                form.TB_Repeat_maemae_2_K.Text = GenieConfig.TB_Repeat_maemae_2_K.ToString();
                form.TB_Repeat_maemae_2_L.Text = GenieConfig.TB_Repeat_maemae_2_L.ToString();
                form.TB_Repeat_maemae_2_M.Text = GenieConfig.TB_Repeat_maemae_2_M.ToString();
                form.TB_Repeat_maemae_2_N.Text = GenieConfig.TB_Repeat_maemae_2_N.ToString();

            }
            catch (Exception e)
            {
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
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

                GenieConfig.MT_repeat_repeat_time_A = repeat_time_A;
                GenieConfig.MT_repeat_repeat_time_B = repeat_time_B;
                GenieConfig.MT_repeat_repeat_time_C = repeat_time_C;
                GenieConfig.MT_repeat_repeat_time_D = repeat_time_D;
                GenieConfig.MT_repeat_repeat_time_E = repeat_time_E;
                GenieConfig.MT_repeat_repeat_time_F = repeat_time_F;
                GenieConfig.MT_repeat_repeat_time_G = repeat_time_G;
                GenieConfig.MT_repeat_repeat_time_H = repeat_time_H;
                GenieConfig.MT_repeat_repeat_time_I = repeat_time_I;
                GenieConfig.MT_repeat_repeat_time_J = repeat_time_J;
                GenieConfig.MT_repeat_repeat_time_K = repeat_time_K;
                GenieConfig.MT_repeat_repeat_time_L = repeat_time_L;
                GenieConfig.MT_repeat_repeat_time_M = repeat_time_M;
                GenieConfig.MT_repeat_repeat_time_N = repeat_time_N;

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

                // [최적화 로직 적용] 딕셔너리로 값을 묶어 처리 속도 향상
                var timeLimits = new Dictionary<string, int>()
                {
                    { "반복_A", repeat_time_A },
                    { "반복_B", repeat_time_B },
                    { "반복_C", repeat_time_C },
                    { "반복_D", repeat_time_D },
                    { "반복_E", repeat_time_E },
                    { "반복_F", repeat_time_F },
                    { "반복_G", repeat_time_G },
                    { "반복_H", repeat_time_H },
                    { "반복_I", repeat_time_I },
                    { "반복_J", repeat_time_J },
                    { "반복_K", repeat_time_K },
                    { "반복_L", repeat_time_L },
                    { "반복_M", repeat_time_M },
                    { "반복_N", repeat_time_N }
                };

                //foreach (var item in Form1.Trading_Pool)
                //{
                //    if (item.Timer <= 0) continue;

                //    if (timeLimits.TryGetValue(item.Location, out int limitTime))
                //    {
                //        if (item.Timer > limitTime)
                //        {
                //            item.Timer = limitTime;
                //        }
                //    }
                //}

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
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
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

                // [최적화] 긴 if문 대신 콤보박스 인덱스 체크 함수 사용 권장 (여기선 원본 로직 유지하며 변환)
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_A.SelectedIndex)) value_A = 0;
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_B.SelectedIndex)) value_B = 0;
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_C.SelectedIndex)) value_C = 0;
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_D.SelectedIndex)) value_D = 0;
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_E.SelectedIndex)) value_E = 0;
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_F.SelectedIndex)) value_F = 0;
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_G.SelectedIndex)) value_G = 0;
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_H.SelectedIndex)) value_H = 0;
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_I.SelectedIndex)) value_I = 0;
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_J.SelectedIndex)) value_J = 0;
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_K.SelectedIndex)) value_K = 0;
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_L.SelectedIndex)) value_L = 0;
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_M.SelectedIndex)) value_M = 0;
                if (new[] { 0, 1, 4, 5, 6 }.Contains(form.combo_repeat_jumun_N.SelectedIndex)) value_N = 0;

                GenieConfig.TB_repeat_value_A = value_A;
                GenieConfig.TB_repeat_value_B = value_B;
                GenieConfig.TB_repeat_value_C = value_C;
                GenieConfig.TB_repeat_value_D = value_D;
                GenieConfig.TB_repeat_value_E = value_E;
                GenieConfig.TB_repeat_value_F = value_F;
                GenieConfig.TB_repeat_value_G = value_G;
                GenieConfig.TB_repeat_value_H = value_H;
                GenieConfig.TB_repeat_value_I = value_I;
                GenieConfig.TB_repeat_value_J = value_J;
                GenieConfig.TB_repeat_value_K = value_K;
                GenieConfig.TB_repeat_value_L = value_L;
                GenieConfig.TB_repeat_value_M = value_M;
                GenieConfig.TB_repeat_value_N = value_N;

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
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
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

                GenieConfig.MTB_repeat_Cancel_time_A = time_A;
                GenieConfig.MTB_repeat_Cancel_time_B = time_B;
                GenieConfig.MTB_repeat_Cancel_time_C = time_C;
                GenieConfig.MTB_repeat_Cancel_time_D = time_D;
                GenieConfig.MTB_repeat_Cancel_time_E = time_E;
                GenieConfig.MTB_repeat_Cancel_time_F = time_F;
                GenieConfig.MTB_repeat_Cancel_time_G = time_G;
                GenieConfig.MTB_repeat_Cancel_time_H = time_H;
                GenieConfig.MTB_repeat_Cancel_time_I = time_I;
                GenieConfig.MTB_repeat_Cancel_time_J = time_J;
                GenieConfig.MTB_repeat_Cancel_time_K = time_K;
                GenieConfig.MTB_repeat_Cancel_time_L = time_L;
                GenieConfig.MTB_repeat_Cancel_time_M = time_M;
                GenieConfig.MTB_repeat_Cancel_time_N = time_N;

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
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
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

                GenieConfig.MTB_repeat_repeat_A = Repeat_A;
                GenieConfig.MTB_repeat_repeat_B = Repeat_B;
                GenieConfig.MTB_repeat_repeat_C = Repeat_C;
                GenieConfig.MTB_repeat_repeat_D = Repeat_D;
                GenieConfig.MTB_repeat_repeat_E = Repeat_E;
                GenieConfig.MTB_repeat_repeat_F = Repeat_F;
                GenieConfig.MTB_repeat_repeat_G = Repeat_G;
                GenieConfig.MTB_repeat_repeat_H = Repeat_H;
                GenieConfig.MTB_repeat_repeat_I = Repeat_I;
                GenieConfig.MTB_repeat_repeat_J = Repeat_J;
                GenieConfig.MTB_repeat_repeat_K = Repeat_K;
                GenieConfig.MTB_repeat_repeat_L = Repeat_L;
                GenieConfig.MTB_repeat_repeat_M = Repeat_M;
                GenieConfig.MTB_repeat_repeat_N = Repeat_N;

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
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                if (Form1.로딩완료)
                {
                    if (form.combo_repeat_use_condition_A.SelectedIndex == 0) { form.반복_A.SelectedIndex = 0; Form1.위치별검색식리스트["반복_A"].이름 = ""; }
                    if (form.combo_repeat_use_condition_B.SelectedIndex == 0) { form.반복_B.SelectedIndex = 0; Form1.위치별검색식리스트["반복_B"].이름 = ""; }
                    if (form.combo_repeat_use_condition_C.SelectedIndex == 0) { form.반복_C.SelectedIndex = 0; Form1.위치별검색식리스트["반복_C"].이름 = ""; }
                    if (form.combo_repeat_use_condition_D.SelectedIndex == 0) { form.반복_D.SelectedIndex = 0; Form1.위치별검색식리스트["반복_D"].이름 = ""; }
                    if (form.combo_repeat_use_condition_E.SelectedIndex == 0) { form.반복_E.SelectedIndex = 0; Form1.위치별검색식리스트["반복_E"].이름 = ""; }
                    if (form.combo_repeat_use_condition_F.SelectedIndex == 0) { form.반복_F.SelectedIndex = 0; Form1.위치별검색식리스트["반복_F"].이름 = ""; }
                    if (form.combo_repeat_use_condition_G.SelectedIndex == 0) { form.반복_G.SelectedIndex = 0; Form1.위치별검색식리스트["반복_G"].이름 = ""; }
                    if (form.combo_repeat_use_condition_H.SelectedIndex == 0) { form.반복_H.SelectedIndex = 0; Form1.위치별검색식리스트["반복_H"].이름 = ""; }
                    if (form.combo_repeat_use_condition_I.SelectedIndex == 0) { form.반복_I.SelectedIndex = 0; Form1.위치별검색식리스트["반복_I"].이름 = ""; }
                    if (form.combo_repeat_use_condition_J.SelectedIndex == 0) { form.반복_J.SelectedIndex = 0; Form1.위치별검색식리스트["반복_J"].이름 = ""; }
                    if (form.combo_repeat_use_condition_K.SelectedIndex == 0) { form.반복_K.SelectedIndex = 0; Form1.위치별검색식리스트["반복_K"].이름 = ""; }
                    if (form.combo_repeat_use_condition_L.SelectedIndex == 0) { form.반복_L.SelectedIndex = 0; Form1.위치별검색식리스트["반복_L"].이름 = ""; }
                    if (form.combo_repeat_use_condition_M.SelectedIndex == 0) { form.반복_M.SelectedIndex = 0; Form1.위치별검색식리스트["반복_M"].이름 = ""; }
                    if (form.combo_repeat_use_condition_N.SelectedIndex == 0) { form.반복_N.SelectedIndex = 0; Form1.위치별검색식리스트["반복_N"].이름 = ""; }

                    GenieConfig.combo_repeat_use_condition_A = GET.ComboBoxIndex(form.combo_repeat_use_condition_A);
                    GenieConfig.combo_repeat_use_condition_B = GET.ComboBoxIndex(form.combo_repeat_use_condition_B);
                    GenieConfig.combo_repeat_use_condition_C = GET.ComboBoxIndex(form.combo_repeat_use_condition_C);
                    GenieConfig.combo_repeat_use_condition_D = GET.ComboBoxIndex(form.combo_repeat_use_condition_D);
                    GenieConfig.combo_repeat_use_condition_E = GET.ComboBoxIndex(form.combo_repeat_use_condition_E);
                    GenieConfig.combo_repeat_use_condition_F = GET.ComboBoxIndex(form.combo_repeat_use_condition_F);
                    GenieConfig.combo_repeat_use_condition_G = GET.ComboBoxIndex(form.combo_repeat_use_condition_G);
                    GenieConfig.combo_repeat_use_condition_H = GET.ComboBoxIndex(form.combo_repeat_use_condition_H);
                    GenieConfig.combo_repeat_use_condition_I = GET.ComboBoxIndex(form.combo_repeat_use_condition_I);
                    GenieConfig.combo_repeat_use_condition_J = GET.ComboBoxIndex(form.combo_repeat_use_condition_J);
                    GenieConfig.combo_repeat_use_condition_K = GET.ComboBoxIndex(form.combo_repeat_use_condition_K);
                    GenieConfig.combo_repeat_use_condition_L = GET.ComboBoxIndex(form.combo_repeat_use_condition_L);
                    GenieConfig.combo_repeat_use_condition_M = GET.ComboBoxIndex(form.combo_repeat_use_condition_M);
                    GenieConfig.combo_repeat_use_condition_N = GET.ComboBoxIndex(form.combo_repeat_use_condition_N);

                    if (GenieConfig.combo_repeat_use_condition_A == 0) Condition_Management.Catch_Stock_List_Clear("반복_A");
                    if (GenieConfig.combo_repeat_use_condition_B == 0) Condition_Management.Catch_Stock_List_Clear("반복_B");
                    if (GenieConfig.combo_repeat_use_condition_C == 0) Condition_Management.Catch_Stock_List_Clear("반복_C");
                    if (GenieConfig.combo_repeat_use_condition_D == 0) Condition_Management.Catch_Stock_List_Clear("반복_D");
                    if (GenieConfig.combo_repeat_use_condition_E == 0) Condition_Management.Catch_Stock_List_Clear("반복_E");
                    if (GenieConfig.combo_repeat_use_condition_F == 0) Condition_Management.Catch_Stock_List_Clear("반복_F");
                    if (GenieConfig.combo_repeat_use_condition_G == 0) Condition_Management.Catch_Stock_List_Clear("반복_G");
                    if (GenieConfig.combo_repeat_use_condition_H == 0) Condition_Management.Catch_Stock_List_Clear("반복_H");
                    if (GenieConfig.combo_repeat_use_condition_I == 0) Condition_Management.Catch_Stock_List_Clear("반복_I");
                    if (GenieConfig.combo_repeat_use_condition_J == 0) Condition_Management.Catch_Stock_List_Clear("반복_J");
                    if (GenieConfig.combo_repeat_use_condition_K == 0) Condition_Management.Catch_Stock_List_Clear("반복_K");
                    if (GenieConfig.combo_repeat_use_condition_L == 0) Condition_Management.Catch_Stock_List_Clear("반복_L");
                    if (GenieConfig.combo_repeat_use_condition_M == 0) Condition_Management.Catch_Stock_List_Clear("반복_M");
                    if (GenieConfig.combo_repeat_use_condition_N == 0) Condition_Management.Catch_Stock_List_Clear("반복_N");

                    GenieConfig.CB_repeat_use_A = form.CB_repeat_use_A.Checked;
                    GenieConfig.CB_repeat_use_B = form.CB_repeat_use_B.Checked;
                    GenieConfig.CB_repeat_use_C = form.CB_repeat_use_C.Checked;
                    GenieConfig.CB_repeat_use_D = form.CB_repeat_use_D.Checked;
                    GenieConfig.CB_repeat_use_E = form.CB_repeat_use_E.Checked;
                    GenieConfig.CB_repeat_use_F = form.CB_repeat_use_F.Checked;
                    GenieConfig.CB_repeat_use_G = form.CB_repeat_use_G.Checked;
                    GenieConfig.CB_repeat_use_H = form.CB_repeat_use_H.Checked;
                    GenieConfig.CB_repeat_use_I = form.CB_repeat_use_I.Checked;
                    GenieConfig.CB_repeat_use_J = form.CB_repeat_use_J.Checked;
                    GenieConfig.CB_repeat_use_K = form.CB_repeat_use_K.Checked;
                    GenieConfig.CB_repeat_use_L = form.CB_repeat_use_L.Checked;
                    GenieConfig.CB_repeat_use_M = form.CB_repeat_use_M.Checked;
                    GenieConfig.CB_repeat_use_N = form.CB_repeat_use_N.Checked;
                }
            }
            catch (Exception e)
            {
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
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

                GenieConfig.TB_repeat_누적거래량_A = TB_repeat_누적거래량_A;
                GenieConfig.TB_repeat_누적거래량_B = TB_repeat_누적거래량_B;
                GenieConfig.TB_repeat_누적거래량_C = TB_repeat_누적거래량_C;
                GenieConfig.TB_repeat_누적거래량_D = TB_repeat_누적거래량_D;
                GenieConfig.TB_repeat_누적거래량_E = TB_repeat_누적거래량_E;
                GenieConfig.TB_repeat_누적거래량_F = TB_repeat_누적거래량_F;
                GenieConfig.TB_repeat_누적거래량_G = TB_repeat_누적거래량_G;
                GenieConfig.TB_repeat_누적거래량_H = TB_repeat_누적거래량_H;
                GenieConfig.TB_repeat_누적거래량_I = TB_repeat_누적거래량_I;
                GenieConfig.TB_repeat_누적거래량_J = TB_repeat_누적거래량_J;
                GenieConfig.TB_repeat_누적거래량_K = TB_repeat_누적거래량_K;
                GenieConfig.TB_repeat_누적거래량_L = TB_repeat_누적거래량_L;
                GenieConfig.TB_repeat_누적거래량_M = TB_repeat_누적거래량_M;
                GenieConfig.TB_repeat_누적거래량_N = TB_repeat_누적거래량_N;

                form.TB_repeat_누적거래량_A.Text = GenieConfig.TB_repeat_누적거래량_A.ToString();
                form.TB_repeat_누적거래량_B.Text = GenieConfig.TB_repeat_누적거래량_B.ToString();
                form.TB_repeat_누적거래량_C.Text = GenieConfig.TB_repeat_누적거래량_C.ToString();
                form.TB_repeat_누적거래량_D.Text = GenieConfig.TB_repeat_누적거래량_D.ToString();
                form.TB_repeat_누적거래량_E.Text = GenieConfig.TB_repeat_누적거래량_E.ToString();
                form.TB_repeat_누적거래량_F.Text = GenieConfig.TB_repeat_누적거래량_F.ToString();
                form.TB_repeat_누적거래량_G.Text = GenieConfig.TB_repeat_누적거래량_G.ToString();
                form.TB_repeat_누적거래량_H.Text = GenieConfig.TB_repeat_누적거래량_H.ToString();
                form.TB_repeat_누적거래량_I.Text = GenieConfig.TB_repeat_누적거래량_I.ToString();
                form.TB_repeat_누적거래량_J.Text = GenieConfig.TB_repeat_누적거래량_J.ToString();
                form.TB_repeat_누적거래량_K.Text = GenieConfig.TB_repeat_누적거래량_K.ToString();
                form.TB_repeat_누적거래량_L.Text = GenieConfig.TB_repeat_누적거래량_L.ToString();
                form.TB_repeat_누적거래량_M.Text = GenieConfig.TB_repeat_누적거래량_M.ToString();
                form.TB_repeat_누적거래량_N.Text = GenieConfig.TB_repeat_누적거래량_N.ToString();

            }
            catch (Exception e)
            {
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
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

                GenieConfig.TB_repeat_누적거래대금_A = TB_repeat_누적거래대금_A;
                GenieConfig.TB_repeat_누적거래대금_B = TB_repeat_누적거래대금_B;
                GenieConfig.TB_repeat_누적거래대금_C = TB_repeat_누적거래대금_C;
                GenieConfig.TB_repeat_누적거래대금_D = TB_repeat_누적거래대금_D;
                GenieConfig.TB_repeat_누적거래대금_E = TB_repeat_누적거래대금_E;
                GenieConfig.TB_repeat_누적거래대금_F = TB_repeat_누적거래대금_F;
                GenieConfig.TB_repeat_누적거래대금_G = TB_repeat_누적거래대금_G;
                GenieConfig.TB_repeat_누적거래대금_H = TB_repeat_누적거래대금_H;
                GenieConfig.TB_repeat_누적거래대금_I = TB_repeat_누적거래대금_I;
                GenieConfig.TB_repeat_누적거래대금_J = TB_repeat_누적거래대금_J;
                GenieConfig.TB_repeat_누적거래대금_K = TB_repeat_누적거래대금_K;
                GenieConfig.TB_repeat_누적거래대금_L = TB_repeat_누적거래대금_L;
                GenieConfig.TB_repeat_누적거래대금_M = TB_repeat_누적거래대금_M;
                GenieConfig.TB_repeat_누적거래대금_N = TB_repeat_누적거래대금_N;

                form.TB_repeat_누적거래대금_A.Text = GenieConfig.TB_repeat_누적거래대금_A.ToString();
                form.TB_repeat_누적거래대금_B.Text = GenieConfig.TB_repeat_누적거래대금_B.ToString();
                form.TB_repeat_누적거래대금_C.Text = GenieConfig.TB_repeat_누적거래대금_C.ToString();
                form.TB_repeat_누적거래대금_D.Text = GenieConfig.TB_repeat_누적거래대금_D.ToString();
                form.TB_repeat_누적거래대금_E.Text = GenieConfig.TB_repeat_누적거래대금_E.ToString();
                form.TB_repeat_누적거래대금_F.Text = GenieConfig.TB_repeat_누적거래대금_F.ToString();
                form.TB_repeat_누적거래대금_G.Text = GenieConfig.TB_repeat_누적거래대금_G.ToString();
                form.TB_repeat_누적거래대금_H.Text = GenieConfig.TB_repeat_누적거래대금_H.ToString();
                form.TB_repeat_누적거래대금_I.Text = GenieConfig.TB_repeat_누적거래대금_I.ToString();
                form.TB_repeat_누적거래대금_J.Text = GenieConfig.TB_repeat_누적거래대금_J.ToString();
                form.TB_repeat_누적거래대금_K.Text = GenieConfig.TB_repeat_누적거래대금_K.ToString();
                form.TB_repeat_누적거래대금_L.Text = GenieConfig.TB_repeat_누적거래대금_L.ToString();
                form.TB_repeat_누적거래대금_M.Text = GenieConfig.TB_repeat_누적거래대금_M.ToString();
                form.TB_repeat_누적거래대금_N.Text = GenieConfig.TB_repeat_누적거래대금_N.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                //분이평

                int.TryParse(form.TB_repeat_MinMAPeriod1_A.Text, out int TB_repeat_MinMAPeriod1_A);
                int.TryParse(form.TB_repeat_MinMAPeriod1_B.Text, out int TB_repeat_MinMAPeriod1_B);
                int.TryParse(form.TB_repeat_MinMAPeriod1_C.Text, out int TB_repeat_MinMAPeriod1_C);
                int.TryParse(form.TB_repeat_MinMAPeriod1_D.Text, out int TB_repeat_MinMAPeriod1_D);
                int.TryParse(form.TB_repeat_MinMAPeriod1_E.Text, out int TB_repeat_MinMAPeriod1_E);
                int.TryParse(form.TB_repeat_MinMAPeriod1_F.Text, out int TB_repeat_MinMAPeriod1_F);
                int.TryParse(form.TB_repeat_MinMAPeriod1_G.Text, out int TB_repeat_MinMAPeriod1_G);
                int.TryParse(form.TB_repeat_MinMAPeriod1_H.Text, out int TB_repeat_MinMAPeriod1_H);
                int.TryParse(form.TB_repeat_MinMAPeriod1_I.Text, out int TB_repeat_MinMAPeriod1_I);
                int.TryParse(form.TB_repeat_MinMAPeriod1_J.Text, out int TB_repeat_MinMAPeriod1_J);
                int.TryParse(form.TB_repeat_MinMAPeriod1_K.Text, out int TB_repeat_MinMAPeriod1_K);
                int.TryParse(form.TB_repeat_MinMAPeriod1_L.Text, out int TB_repeat_MinMAPeriod1_L);
                int.TryParse(form.TB_repeat_MinMAPeriod1_M.Text, out int TB_repeat_MinMAPeriod1_M);
                int.TryParse(form.TB_repeat_MinMAPeriod1_N.Text, out int TB_repeat_MinMAPeriod1_N);

                if (TB_repeat_MinMAPeriod1_A == 0) TB_repeat_MinMAPeriod1_A = 3;
                if (TB_repeat_MinMAPeriod1_B == 0) TB_repeat_MinMAPeriod1_B = 3;
                if (TB_repeat_MinMAPeriod1_C == 0) TB_repeat_MinMAPeriod1_C = 3;
                if (TB_repeat_MinMAPeriod1_D == 0) TB_repeat_MinMAPeriod1_D = 3;
                if (TB_repeat_MinMAPeriod1_E == 0) TB_repeat_MinMAPeriod1_E = 3;
                if (TB_repeat_MinMAPeriod1_F == 0) TB_repeat_MinMAPeriod1_F = 3;
                if (TB_repeat_MinMAPeriod1_G == 0) TB_repeat_MinMAPeriod1_G = 3;
                if (TB_repeat_MinMAPeriod1_H == 0) TB_repeat_MinMAPeriod1_H = 3;
                if (TB_repeat_MinMAPeriod1_I == 0) TB_repeat_MinMAPeriod1_I = 3;
                if (TB_repeat_MinMAPeriod1_J == 0) TB_repeat_MinMAPeriod1_J = 3;
                if (TB_repeat_MinMAPeriod1_K == 0) TB_repeat_MinMAPeriod1_K = 3;
                if (TB_repeat_MinMAPeriod1_L == 0) TB_repeat_MinMAPeriod1_L = 3;
                if (TB_repeat_MinMAPeriod1_M == 0) TB_repeat_MinMAPeriod1_M = 3;
                if (TB_repeat_MinMAPeriod1_N == 0) TB_repeat_MinMAPeriod1_N = 3;

                if (TB_repeat_MinMAPeriod1_A > 300) TB_repeat_MinMAPeriod1_A = 300;
                if (TB_repeat_MinMAPeriod1_B > 300) TB_repeat_MinMAPeriod1_B = 300;
                if (TB_repeat_MinMAPeriod1_C > 300) TB_repeat_MinMAPeriod1_C = 300;
                if (TB_repeat_MinMAPeriod1_D > 300) TB_repeat_MinMAPeriod1_D = 300;
                if (TB_repeat_MinMAPeriod1_E > 300) TB_repeat_MinMAPeriod1_E = 300;
                if (TB_repeat_MinMAPeriod1_F > 300) TB_repeat_MinMAPeriod1_F = 300;
                if (TB_repeat_MinMAPeriod1_G > 300) TB_repeat_MinMAPeriod1_G = 300;
                if (TB_repeat_MinMAPeriod1_H > 300) TB_repeat_MinMAPeriod1_H = 300;
                if (TB_repeat_MinMAPeriod1_I > 300) TB_repeat_MinMAPeriod1_I = 300;
                if (TB_repeat_MinMAPeriod1_J > 300) TB_repeat_MinMAPeriod1_J = 300;
                if (TB_repeat_MinMAPeriod1_K > 300) TB_repeat_MinMAPeriod1_K = 300;
                if (TB_repeat_MinMAPeriod1_L > 300) TB_repeat_MinMAPeriod1_L = 300;
                if (TB_repeat_MinMAPeriod1_M > 300) TB_repeat_MinMAPeriod1_M = 300;
                if (TB_repeat_MinMAPeriod1_N > 300) TB_repeat_MinMAPeriod1_N = 300;

                GenieConfig.TB_repeat_MinMAPeriod1_A = TB_repeat_MinMAPeriod1_A;
                GenieConfig.TB_repeat_MinMAPeriod1_B = TB_repeat_MinMAPeriod1_B;
                GenieConfig.TB_repeat_MinMAPeriod1_C = TB_repeat_MinMAPeriod1_C;
                GenieConfig.TB_repeat_MinMAPeriod1_D = TB_repeat_MinMAPeriod1_D;
                GenieConfig.TB_repeat_MinMAPeriod1_E = TB_repeat_MinMAPeriod1_E;
                GenieConfig.TB_repeat_MinMAPeriod1_F = TB_repeat_MinMAPeriod1_F;
                GenieConfig.TB_repeat_MinMAPeriod1_G = TB_repeat_MinMAPeriod1_G;
                GenieConfig.TB_repeat_MinMAPeriod1_H = TB_repeat_MinMAPeriod1_H;
                GenieConfig.TB_repeat_MinMAPeriod1_I = TB_repeat_MinMAPeriod1_I;
                GenieConfig.TB_repeat_MinMAPeriod1_J = TB_repeat_MinMAPeriod1_J;
                GenieConfig.TB_repeat_MinMAPeriod1_K = TB_repeat_MinMAPeriod1_K;
                GenieConfig.TB_repeat_MinMAPeriod1_L = TB_repeat_MinMAPeriod1_L;
                GenieConfig.TB_repeat_MinMAPeriod1_M = TB_repeat_MinMAPeriod1_M;
                GenieConfig.TB_repeat_MinMAPeriod1_N = TB_repeat_MinMAPeriod1_N;

                form.TB_repeat_MinMAPeriod1_A.Text = TB_repeat_MinMAPeriod1_A.ToString();
                form.TB_repeat_MinMAPeriod1_B.Text = TB_repeat_MinMAPeriod1_B.ToString();
                form.TB_repeat_MinMAPeriod1_C.Text = TB_repeat_MinMAPeriod1_C.ToString();
                form.TB_repeat_MinMAPeriod1_D.Text = TB_repeat_MinMAPeriod1_D.ToString();
                form.TB_repeat_MinMAPeriod1_E.Text = TB_repeat_MinMAPeriod1_E.ToString();
                form.TB_repeat_MinMAPeriod1_F.Text = TB_repeat_MinMAPeriod1_F.ToString();
                form.TB_repeat_MinMAPeriod1_G.Text = TB_repeat_MinMAPeriod1_G.ToString();
                form.TB_repeat_MinMAPeriod1_H.Text = TB_repeat_MinMAPeriod1_H.ToString();
                form.TB_repeat_MinMAPeriod1_I.Text = TB_repeat_MinMAPeriod1_I.ToString();
                form.TB_repeat_MinMAPeriod1_J.Text = TB_repeat_MinMAPeriod1_J.ToString();
                form.TB_repeat_MinMAPeriod1_K.Text = TB_repeat_MinMAPeriod1_K.ToString();
                form.TB_repeat_MinMAPeriod1_L.Text = TB_repeat_MinMAPeriod1_L.ToString();
                form.TB_repeat_MinMAPeriod1_M.Text = TB_repeat_MinMAPeriod1_M.ToString();
                form.TB_repeat_MinMAPeriod1_N.Text = TB_repeat_MinMAPeriod1_N.ToString();

                GenieConfig.CBB_repeat_MinMAPeriod1_A = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_A);
                GenieConfig.CBB_repeat_MinMAPeriod1_B = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_B);
                GenieConfig.CBB_repeat_MinMAPeriod1_C = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_C);
                GenieConfig.CBB_repeat_MinMAPeriod1_D = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_D);
                GenieConfig.CBB_repeat_MinMAPeriod1_E = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_E);
                GenieConfig.CBB_repeat_MinMAPeriod1_F = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_F);
                GenieConfig.CBB_repeat_MinMAPeriod1_G = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_G);
                GenieConfig.CBB_repeat_MinMAPeriod1_H = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_H);
                GenieConfig.CBB_repeat_MinMAPeriod1_I = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_I);
                GenieConfig.CBB_repeat_MinMAPeriod1_J = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_J);
                GenieConfig.CBB_repeat_MinMAPeriod1_K = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_K);
                GenieConfig.CBB_repeat_MinMAPeriod1_L = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_L);
                GenieConfig.CBB_repeat_MinMAPeriod1_M = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_M);
                GenieConfig.CBB_repeat_MinMAPeriod1_N = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_N);

                int.TryParse(form.TB_repeat_MinMAPeriod2_A.Text, out int TB_repeat_MinMAPeriod2_A);
                int.TryParse(form.TB_repeat_MinMAPeriod2_B.Text, out int TB_repeat_MinMAPeriod2_B);
                int.TryParse(form.TB_repeat_MinMAPeriod2_C.Text, out int TB_repeat_MinMAPeriod2_C);
                int.TryParse(form.TB_repeat_MinMAPeriod2_D.Text, out int TB_repeat_MinMAPeriod2_D);
                int.TryParse(form.TB_repeat_MinMAPeriod2_E.Text, out int TB_repeat_MinMAPeriod2_E);
                int.TryParse(form.TB_repeat_MinMAPeriod2_F.Text, out int TB_repeat_MinMAPeriod2_F);
                int.TryParse(form.TB_repeat_MinMAPeriod2_G.Text, out int TB_repeat_MinMAPeriod2_G);
                int.TryParse(form.TB_repeat_MinMAPeriod2_H.Text, out int TB_repeat_MinMAPeriod2_H);
                int.TryParse(form.TB_repeat_MinMAPeriod2_I.Text, out int TB_repeat_MinMAPeriod2_I);
                int.TryParse(form.TB_repeat_MinMAPeriod2_J.Text, out int TB_repeat_MinMAPeriod2_J);
                int.TryParse(form.TB_repeat_MinMAPeriod2_K.Text, out int TB_repeat_MinMAPeriod2_K);
                int.TryParse(form.TB_repeat_MinMAPeriod2_L.Text, out int TB_repeat_MinMAPeriod2_L);
                int.TryParse(form.TB_repeat_MinMAPeriod2_M.Text, out int TB_repeat_MinMAPeriod2_M);
                int.TryParse(form.TB_repeat_MinMAPeriod2_N.Text, out int TB_repeat_MinMAPeriod2_N);

                if (TB_repeat_MinMAPeriod2_A == 0) TB_repeat_MinMAPeriod2_A = 5;
                if (TB_repeat_MinMAPeriod2_B == 0) TB_repeat_MinMAPeriod2_B = 5;
                if (TB_repeat_MinMAPeriod2_C == 0) TB_repeat_MinMAPeriod2_C = 5;
                if (TB_repeat_MinMAPeriod2_D == 0) TB_repeat_MinMAPeriod2_D = 5;
                if (TB_repeat_MinMAPeriod2_E == 0) TB_repeat_MinMAPeriod2_E = 5;
                if (TB_repeat_MinMAPeriod2_F == 0) TB_repeat_MinMAPeriod2_F = 5;
                if (TB_repeat_MinMAPeriod2_G == 0) TB_repeat_MinMAPeriod2_G = 5;
                if (TB_repeat_MinMAPeriod2_H == 0) TB_repeat_MinMAPeriod2_H = 5;
                if (TB_repeat_MinMAPeriod2_I == 0) TB_repeat_MinMAPeriod2_I = 5;
                if (TB_repeat_MinMAPeriod2_J == 0) TB_repeat_MinMAPeriod2_J = 5;
                if (TB_repeat_MinMAPeriod2_K == 0) TB_repeat_MinMAPeriod2_K = 5;
                if (TB_repeat_MinMAPeriod2_L == 0) TB_repeat_MinMAPeriod2_L = 5;
                if (TB_repeat_MinMAPeriod2_M == 0) TB_repeat_MinMAPeriod2_M = 5;
                if (TB_repeat_MinMAPeriod2_N == 0) TB_repeat_MinMAPeriod2_N = 5;

                if (TB_repeat_MinMAPeriod2_A > 300) TB_repeat_MinMAPeriod2_A = 300;
                if (TB_repeat_MinMAPeriod2_B > 300) TB_repeat_MinMAPeriod2_B = 300;
                if (TB_repeat_MinMAPeriod2_C > 300) TB_repeat_MinMAPeriod2_C = 300;
                if (TB_repeat_MinMAPeriod2_D > 300) TB_repeat_MinMAPeriod2_D = 300;
                if (TB_repeat_MinMAPeriod2_E > 300) TB_repeat_MinMAPeriod2_E = 300;
                if (TB_repeat_MinMAPeriod2_F > 300) TB_repeat_MinMAPeriod2_F = 300;
                if (TB_repeat_MinMAPeriod2_G > 300) TB_repeat_MinMAPeriod2_G = 300;
                if (TB_repeat_MinMAPeriod2_H > 300) TB_repeat_MinMAPeriod2_H = 300;
                if (TB_repeat_MinMAPeriod2_I > 300) TB_repeat_MinMAPeriod2_I = 300;
                if (TB_repeat_MinMAPeriod2_J > 300) TB_repeat_MinMAPeriod2_J = 300;
                if (TB_repeat_MinMAPeriod2_K > 300) TB_repeat_MinMAPeriod2_K = 300;
                if (TB_repeat_MinMAPeriod2_L > 300) TB_repeat_MinMAPeriod2_L = 300;
                if (TB_repeat_MinMAPeriod2_M > 300) TB_repeat_MinMAPeriod2_M = 300;
                if (TB_repeat_MinMAPeriod2_N > 300) TB_repeat_MinMAPeriod2_N = 300;

                GenieConfig.TB_repeat_MinMAPeriod2_A = TB_repeat_MinMAPeriod2_A;
                GenieConfig.TB_repeat_MinMAPeriod2_B = TB_repeat_MinMAPeriod2_B;
                GenieConfig.TB_repeat_MinMAPeriod2_C = TB_repeat_MinMAPeriod2_C;
                GenieConfig.TB_repeat_MinMAPeriod2_D = TB_repeat_MinMAPeriod2_D;
                GenieConfig.TB_repeat_MinMAPeriod2_E = TB_repeat_MinMAPeriod2_E;
                GenieConfig.TB_repeat_MinMAPeriod2_F = TB_repeat_MinMAPeriod2_F;
                GenieConfig.TB_repeat_MinMAPeriod2_G = TB_repeat_MinMAPeriod2_G;
                GenieConfig.TB_repeat_MinMAPeriod2_H = TB_repeat_MinMAPeriod2_H;
                GenieConfig.TB_repeat_MinMAPeriod2_I = TB_repeat_MinMAPeriod2_I;
                GenieConfig.TB_repeat_MinMAPeriod2_J = TB_repeat_MinMAPeriod2_J;
                GenieConfig.TB_repeat_MinMAPeriod2_K = TB_repeat_MinMAPeriod2_K;
                GenieConfig.TB_repeat_MinMAPeriod2_L = TB_repeat_MinMAPeriod2_L;
                GenieConfig.TB_repeat_MinMAPeriod2_M = TB_repeat_MinMAPeriod2_M;
                GenieConfig.TB_repeat_MinMAPeriod2_N = TB_repeat_MinMAPeriod2_N;

                form.TB_repeat_MinMAPeriod2_A.Text = TB_repeat_MinMAPeriod2_A.ToString();
                form.TB_repeat_MinMAPeriod2_B.Text = TB_repeat_MinMAPeriod2_B.ToString();
                form.TB_repeat_MinMAPeriod2_C.Text = TB_repeat_MinMAPeriod2_C.ToString();
                form.TB_repeat_MinMAPeriod2_D.Text = TB_repeat_MinMAPeriod2_D.ToString();
                form.TB_repeat_MinMAPeriod2_E.Text = TB_repeat_MinMAPeriod2_E.ToString();
                form.TB_repeat_MinMAPeriod2_F.Text = TB_repeat_MinMAPeriod2_F.ToString();
                form.TB_repeat_MinMAPeriod2_G.Text = TB_repeat_MinMAPeriod2_G.ToString();
                form.TB_repeat_MinMAPeriod2_H.Text = TB_repeat_MinMAPeriod2_H.ToString();
                form.TB_repeat_MinMAPeriod2_I.Text = TB_repeat_MinMAPeriod2_I.ToString();
                form.TB_repeat_MinMAPeriod2_J.Text = TB_repeat_MinMAPeriod2_J.ToString();
                form.TB_repeat_MinMAPeriod2_K.Text = TB_repeat_MinMAPeriod2_K.ToString();
                form.TB_repeat_MinMAPeriod2_L.Text = TB_repeat_MinMAPeriod2_L.ToString();
                form.TB_repeat_MinMAPeriod2_M.Text = TB_repeat_MinMAPeriod2_M.ToString();
                form.TB_repeat_MinMAPeriod2_N.Text = TB_repeat_MinMAPeriod2_N.ToString();

                GenieConfig.CBB_repeat_MinMAPeriod2_A = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_A); 
                GenieConfig.CBB_repeat_MinMAPeriod2_B = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_B); 
                GenieConfig.CBB_repeat_MinMAPeriod2_C = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_C); 
                GenieConfig.CBB_repeat_MinMAPeriod2_D = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_D); 
                GenieConfig.CBB_repeat_MinMAPeriod2_E = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_E); 
                GenieConfig.CBB_repeat_MinMAPeriod2_F = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_F); 
                GenieConfig.CBB_repeat_MinMAPeriod2_G = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_G); 
                GenieConfig.CBB_repeat_MinMAPeriod2_H = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_H); 
                GenieConfig.CBB_repeat_MinMAPeriod2_I = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_I); 
                GenieConfig.CBB_repeat_MinMAPeriod2_J = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_J); 
                GenieConfig.CBB_repeat_MinMAPeriod2_K = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_K); 
                GenieConfig.CBB_repeat_MinMAPeriod2_L = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_L); 
                GenieConfig.CBB_repeat_MinMAPeriod2_M = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_M); 
                GenieConfig.CBB_repeat_MinMAPeriod2_N = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod2_N); 

                GenieConfig.CBB_repeat_MinMAPeriod1_배열_A = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_A);
                GenieConfig.CBB_repeat_MinMAPeriod1_배열_B = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_B);
                GenieConfig.CBB_repeat_MinMAPeriod1_배열_C = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_C);
                GenieConfig.CBB_repeat_MinMAPeriod1_배열_D = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_D);
                GenieConfig.CBB_repeat_MinMAPeriod1_배열_E = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_E);
                GenieConfig.CBB_repeat_MinMAPeriod1_배열_F = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_F);
                GenieConfig.CBB_repeat_MinMAPeriod1_배열_G = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_G);
                GenieConfig.CBB_repeat_MinMAPeriod1_배열_H = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_H);
                GenieConfig.CBB_repeat_MinMAPeriod1_배열_I = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_I);
                GenieConfig.CBB_repeat_MinMAPeriod1_배열_J = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_J);
                GenieConfig.CBB_repeat_MinMAPeriod1_배열_K = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_K);
                GenieConfig.CBB_repeat_MinMAPeriod1_배열_L = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_L);
                GenieConfig.CBB_repeat_MinMAPeriod1_배열_M = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_M);
                GenieConfig.CBB_repeat_MinMAPeriod1_배열_N = GET.ComboBoxIndex(form.CBB_repeat_MinMAPeriod1_배열_N);

                //일이평

                int.TryParse(form.TB_repeat_DayMAPeriod1_A.Text, out int TB_repeat_DayMAPeriod1_A);
                int.TryParse(form.TB_repeat_DayMAPeriod1_B.Text, out int TB_repeat_DayMAPeriod1_B);
                int.TryParse(form.TB_repeat_DayMAPeriod1_C.Text, out int TB_repeat_DayMAPeriod1_C);
                int.TryParse(form.TB_repeat_DayMAPeriod1_D.Text, out int TB_repeat_DayMAPeriod1_D);
                int.TryParse(form.TB_repeat_DayMAPeriod1_E.Text, out int TB_repeat_DayMAPeriod1_E);
                int.TryParse(form.TB_repeat_DayMAPeriod1_F.Text, out int TB_repeat_DayMAPeriod1_F);
                int.TryParse(form.TB_repeat_DayMAPeriod1_G.Text, out int TB_repeat_DayMAPeriod1_G);
                int.TryParse(form.TB_repeat_DayMAPeriod1_H.Text, out int TB_repeat_DayMAPeriod1_H);
                int.TryParse(form.TB_repeat_DayMAPeriod1_I.Text, out int TB_repeat_DayMAPeriod1_I);
                int.TryParse(form.TB_repeat_DayMAPeriod1_J.Text, out int TB_repeat_DayMAPeriod1_J);
                int.TryParse(form.TB_repeat_DayMAPeriod1_K.Text, out int TB_repeat_DayMAPeriod1_K);
                int.TryParse(form.TB_repeat_DayMAPeriod1_L.Text, out int TB_repeat_DayMAPeriod1_L);
                int.TryParse(form.TB_repeat_DayMAPeriod1_M.Text, out int TB_repeat_DayMAPeriod1_M);
                int.TryParse(form.TB_repeat_DayMAPeriod1_N.Text, out int TB_repeat_DayMAPeriod1_N);

                if (TB_repeat_DayMAPeriod1_A == 0) TB_repeat_DayMAPeriod1_A = 5;
                if (TB_repeat_DayMAPeriod1_B == 0) TB_repeat_DayMAPeriod1_B = 5;
                if (TB_repeat_DayMAPeriod1_C == 0) TB_repeat_DayMAPeriod1_C = 5;
                if (TB_repeat_DayMAPeriod1_D == 0) TB_repeat_DayMAPeriod1_D = 5;
                if (TB_repeat_DayMAPeriod1_E == 0) TB_repeat_DayMAPeriod1_E = 5;
                if (TB_repeat_DayMAPeriod1_F == 0) TB_repeat_DayMAPeriod1_F = 5;
                if (TB_repeat_DayMAPeriod1_G == 0) TB_repeat_DayMAPeriod1_G = 5;
                if (TB_repeat_DayMAPeriod1_H == 0) TB_repeat_DayMAPeriod1_H = 5;
                if (TB_repeat_DayMAPeriod1_I == 0) TB_repeat_DayMAPeriod1_I = 5;
                if (TB_repeat_DayMAPeriod1_J == 0) TB_repeat_DayMAPeriod1_J = 5;
                if (TB_repeat_DayMAPeriod1_K == 0) TB_repeat_DayMAPeriod1_K = 5;
                if (TB_repeat_DayMAPeriod1_L == 0) TB_repeat_DayMAPeriod1_L = 5;
                if (TB_repeat_DayMAPeriod1_M == 0) TB_repeat_DayMAPeriod1_M = 5;
                if (TB_repeat_DayMAPeriod1_N == 0) TB_repeat_DayMAPeriod1_N = 5;

                if (TB_repeat_DayMAPeriod1_A > 300) TB_repeat_DayMAPeriod1_A = 300;
                if (TB_repeat_DayMAPeriod1_B > 300) TB_repeat_DayMAPeriod1_B = 300;
                if (TB_repeat_DayMAPeriod1_C > 300) TB_repeat_DayMAPeriod1_C = 300;
                if (TB_repeat_DayMAPeriod1_D > 300) TB_repeat_DayMAPeriod1_D = 300;
                if (TB_repeat_DayMAPeriod1_E > 300) TB_repeat_DayMAPeriod1_E = 300;
                if (TB_repeat_DayMAPeriod1_F > 300) TB_repeat_DayMAPeriod1_F = 300;
                if (TB_repeat_DayMAPeriod1_G > 300) TB_repeat_DayMAPeriod1_G = 300;
                if (TB_repeat_DayMAPeriod1_H > 300) TB_repeat_DayMAPeriod1_H = 300;
                if (TB_repeat_DayMAPeriod1_I > 300) TB_repeat_DayMAPeriod1_I = 300;
                if (TB_repeat_DayMAPeriod1_J > 300) TB_repeat_DayMAPeriod1_J = 300;
                if (TB_repeat_DayMAPeriod1_K > 300) TB_repeat_DayMAPeriod1_K = 300;
                if (TB_repeat_DayMAPeriod1_L > 300) TB_repeat_DayMAPeriod1_L = 300;
                if (TB_repeat_DayMAPeriod1_M > 300) TB_repeat_DayMAPeriod1_M = 300;
                if (TB_repeat_DayMAPeriod1_N > 300) TB_repeat_DayMAPeriod1_N = 300;

                GenieConfig.TB_repeat_DayMAPeriod1_A = TB_repeat_DayMAPeriod1_A;
                GenieConfig.TB_repeat_DayMAPeriod1_B = TB_repeat_DayMAPeriod1_B;
                GenieConfig.TB_repeat_DayMAPeriod1_C = TB_repeat_DayMAPeriod1_C;
                GenieConfig.TB_repeat_DayMAPeriod1_D = TB_repeat_DayMAPeriod1_D;
                GenieConfig.TB_repeat_DayMAPeriod1_E = TB_repeat_DayMAPeriod1_E;
                GenieConfig.TB_repeat_DayMAPeriod1_F = TB_repeat_DayMAPeriod1_F;
                GenieConfig.TB_repeat_DayMAPeriod1_G = TB_repeat_DayMAPeriod1_G;
                GenieConfig.TB_repeat_DayMAPeriod1_H = TB_repeat_DayMAPeriod1_H;
                GenieConfig.TB_repeat_DayMAPeriod1_I = TB_repeat_DayMAPeriod1_I;
                GenieConfig.TB_repeat_DayMAPeriod1_J = TB_repeat_DayMAPeriod1_J;
                GenieConfig.TB_repeat_DayMAPeriod1_K = TB_repeat_DayMAPeriod1_K;
                GenieConfig.TB_repeat_DayMAPeriod1_L = TB_repeat_DayMAPeriod1_L;
                GenieConfig.TB_repeat_DayMAPeriod1_M = TB_repeat_DayMAPeriod1_M;
                GenieConfig.TB_repeat_DayMAPeriod1_N = TB_repeat_DayMAPeriod1_N;

                form.TB_repeat_DayMAPeriod1_A.Text = TB_repeat_DayMAPeriod1_A.ToString();
                form.TB_repeat_DayMAPeriod1_B.Text = TB_repeat_DayMAPeriod1_B.ToString();
                form.TB_repeat_DayMAPeriod1_C.Text = TB_repeat_DayMAPeriod1_C.ToString();
                form.TB_repeat_DayMAPeriod1_D.Text = TB_repeat_DayMAPeriod1_D.ToString();
                form.TB_repeat_DayMAPeriod1_E.Text = TB_repeat_DayMAPeriod1_E.ToString();
                form.TB_repeat_DayMAPeriod1_F.Text = TB_repeat_DayMAPeriod1_F.ToString();
                form.TB_repeat_DayMAPeriod1_G.Text = TB_repeat_DayMAPeriod1_G.ToString();
                form.TB_repeat_DayMAPeriod1_H.Text = TB_repeat_DayMAPeriod1_H.ToString();
                form.TB_repeat_DayMAPeriod1_I.Text = TB_repeat_DayMAPeriod1_I.ToString();
                form.TB_repeat_DayMAPeriod1_J.Text = TB_repeat_DayMAPeriod1_J.ToString();
                form.TB_repeat_DayMAPeriod1_K.Text = TB_repeat_DayMAPeriod1_K.ToString();
                form.TB_repeat_DayMAPeriod1_L.Text = TB_repeat_DayMAPeriod1_L.ToString();
                form.TB_repeat_DayMAPeriod1_M.Text = TB_repeat_DayMAPeriod1_M.ToString();
                form.TB_repeat_DayMAPeriod1_N.Text = TB_repeat_DayMAPeriod1_N.ToString();

                GenieConfig.CBB_repeat_DayMAPeriod1_A = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_A);
                GenieConfig.CBB_repeat_DayMAPeriod1_B = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_B);
                GenieConfig.CBB_repeat_DayMAPeriod1_C = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_C);
                GenieConfig.CBB_repeat_DayMAPeriod1_D = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_D);
                GenieConfig.CBB_repeat_DayMAPeriod1_E = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_E);
                GenieConfig.CBB_repeat_DayMAPeriod1_F = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_F);
                GenieConfig.CBB_repeat_DayMAPeriod1_G = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_G);
                GenieConfig.CBB_repeat_DayMAPeriod1_H = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_H);
                GenieConfig.CBB_repeat_DayMAPeriod1_I = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_I);
                GenieConfig.CBB_repeat_DayMAPeriod1_J = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_J);
                GenieConfig.CBB_repeat_DayMAPeriod1_K = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_K);
                GenieConfig.CBB_repeat_DayMAPeriod1_L = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_L);
                GenieConfig.CBB_repeat_DayMAPeriod1_M = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_M);
                GenieConfig.CBB_repeat_DayMAPeriod1_N = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod1_N);

                int.TryParse(form.TB_repeat_DayMAPeriod2_A.Text, out int TB_repeat_DayMAPeriod2_A);
                int.TryParse(form.TB_repeat_DayMAPeriod2_B.Text, out int TB_repeat_DayMAPeriod2_B);
                int.TryParse(form.TB_repeat_DayMAPeriod2_C.Text, out int TB_repeat_DayMAPeriod2_C);
                int.TryParse(form.TB_repeat_DayMAPeriod2_D.Text, out int TB_repeat_DayMAPeriod2_D);
                int.TryParse(form.TB_repeat_DayMAPeriod2_E.Text, out int TB_repeat_DayMAPeriod2_E);
                int.TryParse(form.TB_repeat_DayMAPeriod2_F.Text, out int TB_repeat_DayMAPeriod2_F);
                int.TryParse(form.TB_repeat_DayMAPeriod2_G.Text, out int TB_repeat_DayMAPeriod2_G);
                int.TryParse(form.TB_repeat_DayMAPeriod2_H.Text, out int TB_repeat_DayMAPeriod2_H);
                int.TryParse(form.TB_repeat_DayMAPeriod2_I.Text, out int TB_repeat_DayMAPeriod2_I);
                int.TryParse(form.TB_repeat_DayMAPeriod2_J.Text, out int TB_repeat_DayMAPeriod2_J);
                int.TryParse(form.TB_repeat_DayMAPeriod2_K.Text, out int TB_repeat_DayMAPeriod2_K);
                int.TryParse(form.TB_repeat_DayMAPeriod2_L.Text, out int TB_repeat_DayMAPeriod2_L);
                int.TryParse(form.TB_repeat_DayMAPeriod2_M.Text, out int TB_repeat_DayMAPeriod2_M);
                int.TryParse(form.TB_repeat_DayMAPeriod2_N.Text, out int TB_repeat_DayMAPeriod2_N);

                if (TB_repeat_DayMAPeriod2_A == 0) TB_repeat_DayMAPeriod2_A = 20;
                if (TB_repeat_DayMAPeriod2_B == 0) TB_repeat_DayMAPeriod2_B = 20;
                if (TB_repeat_DayMAPeriod2_C == 0) TB_repeat_DayMAPeriod2_C = 20;
                if (TB_repeat_DayMAPeriod2_D == 0) TB_repeat_DayMAPeriod2_D = 20;
                if (TB_repeat_DayMAPeriod2_E == 0) TB_repeat_DayMAPeriod2_E = 20;
                if (TB_repeat_DayMAPeriod2_F == 0) TB_repeat_DayMAPeriod2_F = 20;
                if (TB_repeat_DayMAPeriod2_G == 0) TB_repeat_DayMAPeriod2_G = 20;
                if (TB_repeat_DayMAPeriod2_H == 0) TB_repeat_DayMAPeriod2_H = 20;
                if (TB_repeat_DayMAPeriod2_I == 0) TB_repeat_DayMAPeriod2_I = 20;
                if (TB_repeat_DayMAPeriod2_J == 0) TB_repeat_DayMAPeriod2_J = 20;
                if (TB_repeat_DayMAPeriod2_K == 0) TB_repeat_DayMAPeriod2_K = 20;
                if (TB_repeat_DayMAPeriod2_L == 0) TB_repeat_DayMAPeriod2_L = 20;
                if (TB_repeat_DayMAPeriod2_M == 0) TB_repeat_DayMAPeriod2_M = 20;
                if (TB_repeat_DayMAPeriod2_N == 0) TB_repeat_DayMAPeriod2_N = 20;

                if (TB_repeat_DayMAPeriod2_A > 300) TB_repeat_DayMAPeriod2_A = 300;
                if (TB_repeat_DayMAPeriod2_B > 300) TB_repeat_DayMAPeriod2_B = 300;
                if (TB_repeat_DayMAPeriod2_C > 300) TB_repeat_DayMAPeriod2_C = 300;
                if (TB_repeat_DayMAPeriod2_D > 300) TB_repeat_DayMAPeriod2_D = 300;
                if (TB_repeat_DayMAPeriod2_E > 300) TB_repeat_DayMAPeriod2_E = 300;
                if (TB_repeat_DayMAPeriod2_F > 300) TB_repeat_DayMAPeriod2_F = 300;
                if (TB_repeat_DayMAPeriod2_G > 300) TB_repeat_DayMAPeriod2_G = 300;
                if (TB_repeat_DayMAPeriod2_H > 300) TB_repeat_DayMAPeriod2_H = 300;
                if (TB_repeat_DayMAPeriod2_I > 300) TB_repeat_DayMAPeriod2_I = 300;
                if (TB_repeat_DayMAPeriod2_J > 300) TB_repeat_DayMAPeriod2_J = 300;
                if (TB_repeat_DayMAPeriod2_K > 300) TB_repeat_DayMAPeriod2_K = 300;
                if (TB_repeat_DayMAPeriod2_L > 300) TB_repeat_DayMAPeriod2_L = 300;
                if (TB_repeat_DayMAPeriod2_M > 300) TB_repeat_DayMAPeriod2_M = 300;
                if (TB_repeat_DayMAPeriod2_N > 300) TB_repeat_DayMAPeriod2_N = 300;

                GenieConfig.TB_repeat_DayMAPeriod2_A = TB_repeat_DayMAPeriod2_A;
                GenieConfig.TB_repeat_DayMAPeriod2_B = TB_repeat_DayMAPeriod2_B;
                GenieConfig.TB_repeat_DayMAPeriod2_C = TB_repeat_DayMAPeriod2_C;
                GenieConfig.TB_repeat_DayMAPeriod2_D = TB_repeat_DayMAPeriod2_D;
                GenieConfig.TB_repeat_DayMAPeriod2_E = TB_repeat_DayMAPeriod2_E;
                GenieConfig.TB_repeat_DayMAPeriod2_F = TB_repeat_DayMAPeriod2_F;
                GenieConfig.TB_repeat_DayMAPeriod2_G = TB_repeat_DayMAPeriod2_G;
                GenieConfig.TB_repeat_DayMAPeriod2_H = TB_repeat_DayMAPeriod2_H;
                GenieConfig.TB_repeat_DayMAPeriod2_I = TB_repeat_DayMAPeriod2_I;
                GenieConfig.TB_repeat_DayMAPeriod2_J = TB_repeat_DayMAPeriod2_J;
                GenieConfig.TB_repeat_DayMAPeriod2_K = TB_repeat_DayMAPeriod2_K;
                GenieConfig.TB_repeat_DayMAPeriod2_L = TB_repeat_DayMAPeriod2_L;
                GenieConfig.TB_repeat_DayMAPeriod2_M = TB_repeat_DayMAPeriod2_M;
                GenieConfig.TB_repeat_DayMAPeriod2_N = TB_repeat_DayMAPeriod2_N;

                form.TB_repeat_DayMAPeriod2_A.Text = TB_repeat_DayMAPeriod2_A.ToString();
                form.TB_repeat_DayMAPeriod2_B.Text = TB_repeat_DayMAPeriod2_B.ToString();
                form.TB_repeat_DayMAPeriod2_C.Text = TB_repeat_DayMAPeriod2_C.ToString();
                form.TB_repeat_DayMAPeriod2_D.Text = TB_repeat_DayMAPeriod2_D.ToString();
                form.TB_repeat_DayMAPeriod2_E.Text = TB_repeat_DayMAPeriod2_E.ToString();
                form.TB_repeat_DayMAPeriod2_F.Text = TB_repeat_DayMAPeriod2_F.ToString();
                form.TB_repeat_DayMAPeriod2_G.Text = TB_repeat_DayMAPeriod2_G.ToString();
                form.TB_repeat_DayMAPeriod2_H.Text = TB_repeat_DayMAPeriod2_H.ToString();
                form.TB_repeat_DayMAPeriod2_I.Text = TB_repeat_DayMAPeriod2_I.ToString();
                form.TB_repeat_DayMAPeriod2_J.Text = TB_repeat_DayMAPeriod2_J.ToString();
                form.TB_repeat_DayMAPeriod2_K.Text = TB_repeat_DayMAPeriod2_K.ToString();
                form.TB_repeat_DayMAPeriod2_L.Text = TB_repeat_DayMAPeriod2_L.ToString();
                form.TB_repeat_DayMAPeriod2_M.Text = TB_repeat_DayMAPeriod2_M.ToString();
                form.TB_repeat_DayMAPeriod2_N.Text = TB_repeat_DayMAPeriod2_N.ToString();

                GenieConfig.CBB_repeat_DayMAPeriod2_A = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_A); 
                GenieConfig.CBB_repeat_DayMAPeriod2_B = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_B); 
                GenieConfig.CBB_repeat_DayMAPeriod2_C = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_C); 
                GenieConfig.CBB_repeat_DayMAPeriod2_D = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_D); 
                GenieConfig.CBB_repeat_DayMAPeriod2_E = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_E); 
                GenieConfig.CBB_repeat_DayMAPeriod2_F = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_F); 
                GenieConfig.CBB_repeat_DayMAPeriod2_G = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_G); 
                GenieConfig.CBB_repeat_DayMAPeriod2_H = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_H); 
                GenieConfig.CBB_repeat_DayMAPeriod2_I = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_I); 
                GenieConfig.CBB_repeat_DayMAPeriod2_J = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_J); 
                GenieConfig.CBB_repeat_DayMAPeriod2_K = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_K); 
                GenieConfig.CBB_repeat_DayMAPeriod2_L = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_L); 
                GenieConfig.CBB_repeat_DayMAPeriod2_M = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_M); 
                GenieConfig.CBB_repeat_DayMAPeriod2_N = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod2_N); 

                GenieConfig.CBB_repeat_DayMAPeriod_배열_A = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_A); 
                GenieConfig.CBB_repeat_DayMAPeriod_배열_B = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_B); 
                GenieConfig.CBB_repeat_DayMAPeriod_배열_C = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_C); 
                GenieConfig.CBB_repeat_DayMAPeriod_배열_D = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_D); 
                GenieConfig.CBB_repeat_DayMAPeriod_배열_E = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_E); 
                GenieConfig.CBB_repeat_DayMAPeriod_배열_F = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_F); 
                GenieConfig.CBB_repeat_DayMAPeriod_배열_G = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_G); 
                GenieConfig.CBB_repeat_DayMAPeriod_배열_H = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_H); 
                GenieConfig.CBB_repeat_DayMAPeriod_배열_I = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_I); 
                GenieConfig.CBB_repeat_DayMAPeriod_배열_J = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_J); 
                GenieConfig.CBB_repeat_DayMAPeriod_배열_K = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_K); 
                GenieConfig.CBB_repeat_DayMAPeriod_배열_L = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_L); 
                GenieConfig.CBB_repeat_DayMAPeriod_배열_M = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_M); 
                GenieConfig.CBB_repeat_DayMAPeriod_배열_N = GET.ComboBoxIndex(form.CBB_repeat_DayMAPeriod_배열_N); 

                MA.Get_MA();
            }
            catch (Exception e)
            {
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                GenieConfig.combo_repeat_Cancel_A = GET.ComboBoxIndex(form.combo_repeat_Cancel_A);
                GenieConfig.combo_repeat_Cancel_B = GET.ComboBoxIndex(form.combo_repeat_Cancel_B);
                GenieConfig.combo_repeat_Cancel_C = GET.ComboBoxIndex(form.combo_repeat_Cancel_C);
                GenieConfig.combo_repeat_Cancel_D = GET.ComboBoxIndex(form.combo_repeat_Cancel_D);
                GenieConfig.combo_repeat_Cancel_E = GET.ComboBoxIndex(form.combo_repeat_Cancel_E);
                GenieConfig.combo_repeat_Cancel_F = GET.ComboBoxIndex(form.combo_repeat_Cancel_F);
                GenieConfig.combo_repeat_Cancel_G = GET.ComboBoxIndex(form.combo_repeat_Cancel_G);
                GenieConfig.combo_repeat_Cancel_H = GET.ComboBoxIndex(form.combo_repeat_Cancel_H);
                GenieConfig.combo_repeat_Cancel_I = GET.ComboBoxIndex(form.combo_repeat_Cancel_I);
                GenieConfig.combo_repeat_Cancel_J = GET.ComboBoxIndex(form.combo_repeat_Cancel_J);
                GenieConfig.combo_repeat_Cancel_K = GET.ComboBoxIndex(form.combo_repeat_Cancel_K);
                GenieConfig.combo_repeat_Cancel_L = GET.ComboBoxIndex(form.combo_repeat_Cancel_L);
                GenieConfig.combo_repeat_Cancel_M = GET.ComboBoxIndex(form.combo_repeat_Cancel_M);
                GenieConfig.combo_repeat_Cancel_N = GET.ComboBoxIndex(form.combo_repeat_Cancel_N);

                GenieConfig.combo_repeat_suik_gubun_A = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_A);  
                GenieConfig.combo_repeat_suik_gubun_B = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_B ); 
                GenieConfig.combo_repeat_suik_gubun_C = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_C ); 
                GenieConfig.combo_repeat_suik_gubun_D = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_D ); 
                GenieConfig.combo_repeat_suik_gubun_E = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_E ); 
                GenieConfig.combo_repeat_suik_gubun_F = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_F ); 
                GenieConfig.combo_repeat_suik_gubun_G = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_G ); 
                GenieConfig.combo_repeat_suik_gubun_H = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_H ); 
                GenieConfig.combo_repeat_suik_gubun_I = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_I ); 
                GenieConfig.combo_repeat_suik_gubun_J = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_J ); 
                GenieConfig.combo_repeat_suik_gubun_K = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_K ); 
                GenieConfig.combo_repeat_suik_gubun_L = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_L ); 
                GenieConfig.combo_repeat_suik_gubun_M = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_M ); 
                GenieConfig.combo_repeat_suik_gubun_N = GET.ComboBoxIndex(form.combo_repeat_suik_gubun_N ); 

                GenieConfig.combo_repeat_sell_gubun_A = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_A);
                GenieConfig.combo_repeat_sell_gubun_B = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_B);
                GenieConfig.combo_repeat_sell_gubun_C = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_C);
                GenieConfig.combo_repeat_sell_gubun_D = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_D);
                GenieConfig.combo_repeat_sell_gubun_E = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_E);
                GenieConfig.combo_repeat_sell_gubun_F = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_F);
                GenieConfig.combo_repeat_sell_gubun_G = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_G);
                GenieConfig.combo_repeat_sell_gubun_H = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_H);
                GenieConfig.combo_repeat_sell_gubun_I = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_I);
                GenieConfig.combo_repeat_sell_gubun_J = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_J);
                GenieConfig.combo_repeat_sell_gubun_K = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_K);
                GenieConfig.combo_repeat_sell_gubun_L = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_L);
                GenieConfig.combo_repeat_sell_gubun_M = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_M);
                GenieConfig.combo_repeat_sell_gubun_N = GET.ComboBoxIndex(form.combo_repeat_sell_gubun_N);

                GenieConfig.combo_Repeat_maemae_gubun_A = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_A);
                GenieConfig.combo_Repeat_maemae_gubun_B = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_B);
                GenieConfig.combo_Repeat_maemae_gubun_C = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_C);
                GenieConfig.combo_Repeat_maemae_gubun_D = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_D);
                GenieConfig.combo_Repeat_maemae_gubun_E = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_E);
                GenieConfig.combo_Repeat_maemae_gubun_F = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_F);
                GenieConfig.combo_Repeat_maemae_gubun_G = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_G);
                GenieConfig.combo_Repeat_maemae_gubun_H = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_H);
                GenieConfig.combo_Repeat_maemae_gubun_I = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_I);
                GenieConfig.combo_Repeat_maemae_gubun_J = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_J);
                GenieConfig.combo_Repeat_maemae_gubun_K = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_K);
                GenieConfig.combo_Repeat_maemae_gubun_L = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_L);
                GenieConfig.combo_Repeat_maemae_gubun_M = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_M);
                GenieConfig.combo_Repeat_maemae_gubun_N = GET.ComboBoxIndex(form.combo_Repeat_maemae_gubun_N);
            }
            catch (Exception e)
            {
                 Form1.Console_print("반복매매_저장 에러: " + e.Message); Log.에러기록("반복매매_저장 에러: " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_반복_추매주가이상.Text.Replace(",", ""), out int 추매주가이상);
                int.TryParse(form.TB_반복_추매주가이하.Text.Replace(",", ""), out int 추매주가이하);

                if (추매주가이하 == 0) 추매주가이하 = 1000000;
                GenieConfig.TB_반복_추매주가이상 = 추매주가이상;
                GenieConfig.TB_반복_추매주가이하 = 추매주가이하;

                double.TryParse(form.TB_반복_추매등락률이상.Text, out double 추매등락률이상);
                double.TryParse(form.TB_반복_추매등락률이하.Text, out double 추매등락률이하);

                GenieConfig.TB_반복_추매등락률이상 = 추매등락률이상;
                GenieConfig.TB_반복_추매등락률이하 = 추매등락률이하;

                form.TB_반복_추매주가이상.Text = GenieConfig.TB_반복_추매주가이상.ToString();
                form.TB_반복_추매주가이하.Text = GenieConfig.TB_반복_추매주가이하.ToString();
                form.TB_반복_추매등락률이상.Text = GenieConfig.TB_반복_추매등락률이상.ToString();
                form.TB_반복_추매등락률이하.Text = GenieConfig.TB_반복_추매등락률이하.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("계좌관리_저장 / 추매주가제한 입력 오류 : " + e.Message); Log.에러기록("계좌관리_저장 / 추매주가제한 입력 오류 : " + e.Message);
            }

            GenieConfig.CB_Repeat_기준금 = form.CB_Repeat_기준금.Checked;

            GenieConfig.CB_repeat_kind_A = form.CB_repeat_kind_A.Checked;
            GenieConfig.CB_repeat_kind_B = form.CB_repeat_kind_B.Checked;
            GenieConfig.CB_repeat_kind_C = form.CB_repeat_kind_C.Checked;
            GenieConfig.CB_repeat_kind_D = form.CB_repeat_kind_D.Checked;
            GenieConfig.CB_repeat_kind_E = form.CB_repeat_kind_E.Checked;
            GenieConfig.CB_repeat_kind_F = form.CB_repeat_kind_F.Checked;
            GenieConfig.CB_repeat_kind_G = form.CB_repeat_kind_G.Checked;
            GenieConfig.CB_repeat_kind_H = form.CB_repeat_kind_H.Checked;
            GenieConfig.CB_repeat_kind_I = form.CB_repeat_kind_I.Checked;
            GenieConfig.CB_repeat_kind_J = form.CB_repeat_kind_J.Checked;
            GenieConfig.CB_repeat_kind_K = form.CB_repeat_kind_K.Checked;
            GenieConfig.CB_repeat_kind_L = form.CB_repeat_kind_L.Checked;
            GenieConfig.CB_repeat_kind_M = form.CB_repeat_kind_M.Checked;
            GenieConfig.CB_repeat_kind_N = form.CB_repeat_kind_N.Checked;

            GenieConfig.CB_repeat_choice_A = form.CB_repeat_choice_A.Checked;
            GenieConfig.CB_repeat_choice_B = form.CB_repeat_choice_B.Checked;
            GenieConfig.CB_repeat_choice_C = form.CB_repeat_choice_C.Checked;
            GenieConfig.CB_repeat_choice_D = form.CB_repeat_choice_D.Checked;
            GenieConfig.CB_repeat_choice_E = form.CB_repeat_choice_E.Checked;
            GenieConfig.CB_repeat_choice_F = form.CB_repeat_choice_F.Checked;
            GenieConfig.CB_repeat_choice_G = form.CB_repeat_choice_G.Checked;
            GenieConfig.CB_repeat_choice_H = form.CB_repeat_choice_H.Checked;
            GenieConfig.CB_repeat_choice_I = form.CB_repeat_choice_I.Checked;
            GenieConfig.CB_repeat_choice_J = form.CB_repeat_choice_J.Checked;
            GenieConfig.CB_repeat_choice_K = form.CB_repeat_choice_K.Checked;
            GenieConfig.CB_repeat_choice_L = form.CB_repeat_choice_L.Checked;
            GenieConfig.CB_repeat_choice_M = form.CB_repeat_choice_M.Checked;
            GenieConfig.CB_repeat_choice_N = form.CB_repeat_choice_N.Checked;
        }

        private void Form_Repeat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                Form1.form1.CB_반복매매.Checked = false;
            }
        }

        private void CB_레이아웃고정_반복매매_CheckedChanged(object sender, EventArgs e)
        {
            GenieConfig.CB_레이아웃고정_반복매매 = CB_레이아웃고정_반복매매.Checked;

            if (!CB_레이아웃고정_반복매매.Checked) LayoutChange.CBB_layout_SelectedIndex(-1);
            else LayoutChange.CBB_layout_SelectedIndex(GenieConfig.CBB_layout);
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

            // 1. 방어막 및 단일 캐스팅: 체크박스가 아니거나 텍스트가 2글자 미만이면 뻗음 방지
            if (!(sender is CheckBox cb) || string.IsNullOrEmpty(cb.Text) || cb.Text.Length < 2) return;

            // 2. UI 렌더링 최적화: 삼항 연산자로 목표 텍스트를 만들고, 다를 때만 덮어씀
            string prefix = cb.Text.Substring(0, 2);
            string targetText = prefix + (cb.Checked ? "■" : "□");

            if (cb.Text != targetText)
            {
                cb.Text = targetText;
            }

            // 3. 로직 최적화: 체크 해제(false) 상태이고 폼이 열려있을 때만 실행
            if (!cb.Checked && Form1.FormRepeat_Open)
            {
                // [최적화 핵심 1] 무거운 14연속 if문 대신, switch문을 써서 0.001초 만에 목적지로 점프합니다.
                // 환경설정(GenieConfig) 업데이트는 굳이 잔고 루프를 탈 필요가 없으므로 밖에서 딱 1번만 실행합니다.
                switch (cb.Name)
                {
                    case "CB_repeat_use_A": GenieConfig.CB_repeat_use_A = false; break;
                    case "CB_repeat_use_B": GenieConfig.CB_repeat_use_B = false; break;
                    case "CB_repeat_use_C": GenieConfig.CB_repeat_use_C = false; break;
                    case "CB_repeat_use_D": GenieConfig.CB_repeat_use_D = false; break;
                    case "CB_repeat_use_E": GenieConfig.CB_repeat_use_E = false; break;
                    case "CB_repeat_use_F": GenieConfig.CB_repeat_use_F = false; break;
                    case "CB_repeat_use_G": GenieConfig.CB_repeat_use_G = false; break;
                    case "CB_repeat_use_H": GenieConfig.CB_repeat_use_H = false; break;
                    case "CB_repeat_use_I": GenieConfig.CB_repeat_use_I = false; break;
                    case "CB_repeat_use_J": GenieConfig.CB_repeat_use_J = false; break;
                    case "CB_repeat_use_K": GenieConfig.CB_repeat_use_K = false; break;
                    case "CB_repeat_use_L": GenieConfig.CB_repeat_use_L = false; break;
                    case "CB_repeat_use_M": GenieConfig.CB_repeat_use_M = false; break;
                    case "CB_repeat_use_N": GenieConfig.CB_repeat_use_N = false; break;
                }

                // [최적화 핵심 2] 잔고 리스트를 순회할 때도 switch문으로 연산 낭비를 0으로 만듭니다.
                foreach (var 잔고 in Form1.stockBalanceList.Values)
                {
                    switch (cb.Name)
                    {
                        case "CB_repeat_use_A": 잔고.반복A = "A"; break;
                        case "CB_repeat_use_B": 잔고.반복B = "B"; break;
                        case "CB_repeat_use_C": 잔고.반복C = "C"; break;
                        case "CB_repeat_use_D": 잔고.반복D = "D"; break;
                        case "CB_repeat_use_E": 잔고.반복E = "E"; break;
                        case "CB_repeat_use_F": 잔고.반복F = "F"; break;
                        case "CB_repeat_use_G": 잔고.반복G = "G"; break;
                        case "CB_repeat_use_H": 잔고.반복H = "H"; break;
                        case "CB_repeat_use_I": 잔고.반복I = "I"; break;
                        case "CB_repeat_use_J": 잔고.반복J = "J"; break;
                        case "CB_repeat_use_K": 잔고.반복K = "K"; break;
                        case "CB_repeat_use_L": 잔고.반복L = "L"; break;
                        case "CB_repeat_use_M": 잔고.반복M = "M"; break;
                        case "CB_repeat_use_N": 잔고.반복N = "N"; break;
                    }
                }
            }
        }

        private void Repeat_CB_CheckedChanged(object sender, EventArgs e)
        {
            // 1. 철벽 방어막: 체크박스가 아니면 즉시 탈출 (프로그램 뻗음 방지)
            if (!(sender is CheckBox cb)) return;

            // 2. 삼항 연산자로 바꿀 목표를 0.001초 만에 결정
            string targetText = cb.Checked ? "매수" : "매도";
            Color targetColor = cb.Checked ? Color.Red : Color.Blue;

            // 3. UI 렌더링 최적화: 현재 상태와 "진짜로 다를 때만" 화면을 다시 그린다!
            if (cb.Text != targetText) cb.Text = targetText;
            if (cb.ForeColor != targetColor) cb.ForeColor = targetColor;

            // 4. 폼이 열려있을 때 추가 로직 실행
            if (Form1.FormRepeat_Open)
            {
                FormPrint.CBB_반복매매_매도비중_Selected(sender);

                // [최적화 핵심] 14개의 if문을 switch문으로 묶어서 '조종할 대상'을 한 번만 찾습니다.
                CheckBox targetUseCb = null;

                switch (cb.Name)
                {
                    case "CB_repeat_kind_A": targetUseCb = CB_repeat_use_A; break;
                    case "CB_repeat_kind_B": targetUseCb = CB_repeat_use_B; break;
                    case "CB_repeat_kind_C": targetUseCb = CB_repeat_use_C; break;
                    case "CB_repeat_kind_D": targetUseCb = CB_repeat_use_D; break;
                    case "CB_repeat_kind_E": targetUseCb = CB_repeat_use_E; break;
                    case "CB_repeat_kind_F": targetUseCb = CB_repeat_use_F; break;
                    case "CB_repeat_kind_G": targetUseCb = CB_repeat_use_G; break;
                    case "CB_repeat_kind_H": targetUseCb = CB_repeat_use_H; break;
                    case "CB_repeat_kind_I": targetUseCb = CB_repeat_use_I; break;
                    case "CB_repeat_kind_J": targetUseCb = CB_repeat_use_J; break;
                    case "CB_repeat_kind_K": targetUseCb = CB_repeat_use_K; break;
                    case "CB_repeat_kind_L": targetUseCb = CB_repeat_use_L; break;
                    case "CB_repeat_kind_M": targetUseCb = CB_repeat_use_M; break;
                    case "CB_repeat_kind_N": targetUseCb = CB_repeat_use_N; break;
                }

                // 대상을 찾았다면, 똑같은 로직(체크해제 or 비프음)을 딱 한 번만 깔끔하게 실행합니다!
                if (targetUseCb != null)
                {
                    if (targetUseCb.Checked)
                    {
                        targetUseCb.Checked = false;
                    }
                    else
                    {
                        Form1.form1.체크박스_비프(sender);
                    }
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
            // 1. 철벽 방어막: 체크박스가 아니거나 텍스트가 텅 비었으면 즉시 탈출 (프로그램 뻗음 방지)
            if (!(sender is CheckBox cb) || string.IsNullOrEmpty(cb.Text)) return;

            // 2. 비프음 재생 (삼항 연산자로 1줄 압축)
            if (Form1.로딩완료)
            {
                Form1.비프음(cb.Checked ? "체크" : "언체크");
            }

            // 3. 텍스트 분리: 맨 앞 1글자(기존 네모)를 제외한 나머지 텍스트 추출
            // (만약 체크박스 이름이 1글자밖에 없더라도 뻗지 않도록 길이 방어막 추가)
            string remainText = cb.Text.Length > 1 ? cb.Text[1..] : "";

            // 4. 목표 텍스트 0.001초 만에 조립
            string targetText = (cb.Checked ? "■" : "□") + remainText;

            // [최적화 핵심 1] 현재 텍스트와 목표 텍스트가 "다를 때만" 화면을 다시 그린다!
            if (cb.Text != targetText)
            {
                cb.Text = targetText;
            }

            // 5. 특정 체크박스(CB_Repeat_기준금) 전용 색상 변경 로직
            if (cb == CB_Repeat_기준금)
            {
                Color targetColor = cb.Checked ? Color.Crimson : Color.Black;

                // [최적화 핵심 2] 현재 색상과 목표 색상이 "다를 때만" 붓을 든다!
                if (cb.ForeColor != targetColor)
                {
                    cb.ForeColor = targetColor;
                }
            }
        }

        private void Combo_use_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Form1.FormRepeat_Open) Condition_Management.Combo_use_condition_SelectedIndexChanged(sender);
        }

        private void Combo_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Form1.FormRepeat_Open) Condition_Management.Combo_condition_SelectedIndexChanged(sender);
        }

        private void CBB_jumun_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrint.CBB_jumun_SelectedIndex(sender);
        }

        private void Combo_Condition_Add(object sender, EventArgs e)
        {
            Condition_Management.Condition_Add(sender);
        }

        private void Combo_condition_MouseHover(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            toolTip1.SetToolTip(combo, combo.Text);
        }

        private void CBB_repeat_suik_gubun_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.FormRepeat_Open) Form1.비프음("체크");

            FormPrint.CBB_suik_DropDownClosed(sender);
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

        private void 양수음수소수_키프레스_(object sender, KeyPressEventArgs e) // 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, true); // textbox 에 양수, 음수 , 소수  숫자만 입력 받을수 있음 
        }

        private void 양수소수_키프레스_(object sender, KeyPressEventArgs e)// 사용
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

        private void Combo_cancel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrint.Combo_cancel_SelectedIndexChanged(sender);
        }

        private void 숫자콤마넣기_TextChanged(object sender, EventArgs e)
        {
            TextValue.숫자콤마넣기_TextChanged(sender);
        }

        private void 양수실수_키프레스_(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, false); // textbox 에 양수 , 실수 숫자만 입력 받을수 있음
        }

        private void CBB_mma_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_repeat_MinMAPeriod1_A) && CBB_repeat_MinMAPeriod1_A.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_A.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod1_B) && CBB_repeat_MinMAPeriod1_B.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_B.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod1_C) && CBB_repeat_MinMAPeriod1_C.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_C.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod1_D) && CBB_repeat_MinMAPeriod1_D.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_D.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod1_E) && CBB_repeat_MinMAPeriod1_E.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_E.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod1_F) && CBB_repeat_MinMAPeriod1_F.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_F.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod1_G) && CBB_repeat_MinMAPeriod1_G.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_G.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_G.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod1_H) && CBB_repeat_MinMAPeriod1_H.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_H.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_H.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod1_I) && CBB_repeat_MinMAPeriod1_I.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_I.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_I.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod1_J) && CBB_repeat_MinMAPeriod1_J.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_J.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_J.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod1_K) && CBB_repeat_MinMAPeriod1_K.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_K.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_K.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod1_L) && CBB_repeat_MinMAPeriod1_L.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_L.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_L.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod1_M) && CBB_repeat_MinMAPeriod1_M.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_M.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_M.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod1_N) && CBB_repeat_MinMAPeriod1_N.SelectedIndex == 0) { CBB_repeat_MinMAPeriod2_N.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_N.SelectedIndex = 0; }
        }

        private void CBB_mma2_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_repeat_MinMAPeriod2_A) && (CBB_repeat_MinMAPeriod1_A.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_A.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_A.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod2_B) && (CBB_repeat_MinMAPeriod1_B.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_B.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_B.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod2_C) && (CBB_repeat_MinMAPeriod1_C.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_C.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_C.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod2_D) && (CBB_repeat_MinMAPeriod1_D.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_D.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_D.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod2_E) && (CBB_repeat_MinMAPeriod1_E.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_E.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_E.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod2_F) && (CBB_repeat_MinMAPeriod1_F.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_F.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_F.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod2_G) && (CBB_repeat_MinMAPeriod1_G.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_G.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_G.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_G.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod2_H) && (CBB_repeat_MinMAPeriod1_H.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_H.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_H.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_H.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod2_I) && (CBB_repeat_MinMAPeriod1_I.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_I.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_I.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_I.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod2_J) && (CBB_repeat_MinMAPeriod1_J.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_J.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_J.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_J.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod2_K) && (CBB_repeat_MinMAPeriod1_K.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_K.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_K.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_K.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod2_L) && (CBB_repeat_MinMAPeriod1_L.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_L.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_L.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_L.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod2_M) && (CBB_repeat_MinMAPeriod1_M.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_M.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_M.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_M.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_MinMAPeriod2_N) && (CBB_repeat_MinMAPeriod1_N.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_N.SelectedIndex == 0)) { CBB_repeat_MinMAPeriod2_N.SelectedIndex = 0; CBB_repeat_MinMAPeriod1_배열_N.SelectedIndex = 0; }
        }

        private void CBB_mma_배열_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_A) && (CBB_repeat_MinMAPeriod1_A.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_A.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_A.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_B) && (CBB_repeat_MinMAPeriod1_B.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_B.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_B.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_C) && (CBB_repeat_MinMAPeriod1_C.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_C.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_C.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_D) && (CBB_repeat_MinMAPeriod1_D.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_D.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_D.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_E) && (CBB_repeat_MinMAPeriod1_E.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_E.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_E.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_F) && (CBB_repeat_MinMAPeriod1_F.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_F.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_F.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_G) && (CBB_repeat_MinMAPeriod1_G.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_G.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_G.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_H) && (CBB_repeat_MinMAPeriod1_H.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_H.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_H.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_I) && (CBB_repeat_MinMAPeriod1_I.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_I.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_I.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_J) && (CBB_repeat_MinMAPeriod1_J.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_J.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_J.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_K) && (CBB_repeat_MinMAPeriod1_K.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_K.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_K.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_L) && (CBB_repeat_MinMAPeriod1_L.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_L.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_L.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_M) && (CBB_repeat_MinMAPeriod1_M.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_M.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_M.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_MinMAPeriod1_배열_N) && (CBB_repeat_MinMAPeriod1_N.SelectedIndex == 0 || CBB_repeat_MinMAPeriod2_N.SelectedIndex == 0)) CBB_repeat_MinMAPeriod1_배열_N.SelectedIndex = 0;
        }

        private void CBB_dma1_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_repeat_DayMAPeriod1_A) && CBB_repeat_DayMAPeriod1_A.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_A.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod1_B) && CBB_repeat_DayMAPeriod1_B.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_B.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod1_C) && CBB_repeat_DayMAPeriod1_C.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_C.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod1_D) && CBB_repeat_DayMAPeriod1_D.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_D.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod1_E) && CBB_repeat_DayMAPeriod1_E.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_E.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod1_F) && CBB_repeat_DayMAPeriod1_F.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_F.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod1_G) && CBB_repeat_DayMAPeriod1_G.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_G.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_G.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod1_H) && CBB_repeat_DayMAPeriod1_H.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_H.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_H.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod1_I) && CBB_repeat_DayMAPeriod1_I.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_I.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_I.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod1_J) && CBB_repeat_DayMAPeriod1_J.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_J.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_J.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod1_K) && CBB_repeat_DayMAPeriod1_K.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_K.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_K.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod1_L) && CBB_repeat_DayMAPeriod1_L.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_L.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_L.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod1_M) && CBB_repeat_DayMAPeriod1_M.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_M.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_M.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod1_N) && CBB_repeat_DayMAPeriod1_N.SelectedIndex == 0) { CBB_repeat_DayMAPeriod2_N.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_N.SelectedIndex = 0; }
        }
        private void CBB_dma2_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_repeat_DayMAPeriod2_A) && (CBB_repeat_DayMAPeriod1_A.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_A.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_A.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_A.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod2_B) && (CBB_repeat_DayMAPeriod1_B.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_B.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_B.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_B.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod2_C) && (CBB_repeat_DayMAPeriod1_C.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_C.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_C.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_C.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod2_D) && (CBB_repeat_DayMAPeriod1_D.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_D.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_D.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_D.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod2_E) && (CBB_repeat_DayMAPeriod1_E.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_E.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_E.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_E.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod2_F) && (CBB_repeat_DayMAPeriod1_F.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_F.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_F.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_F.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod2_G) && (CBB_repeat_DayMAPeriod1_G.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_G.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_G.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_G.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod2_H) && (CBB_repeat_DayMAPeriod1_H.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_H.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_H.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_H.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod2_I) && (CBB_repeat_DayMAPeriod1_I.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_I.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_I.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_I.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod2_J) && (CBB_repeat_DayMAPeriod1_J.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_J.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_J.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_J.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod2_K) && (CBB_repeat_DayMAPeriod1_K.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_K.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_K.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_K.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod2_L) && (CBB_repeat_DayMAPeriod1_L.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_L.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_L.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_L.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod2_M) && (CBB_repeat_DayMAPeriod1_M.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_M.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_M.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_M.SelectedIndex = 0; }
            if (sender.Equals(CBB_repeat_DayMAPeriod2_N) && (CBB_repeat_DayMAPeriod1_N.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_N.SelectedIndex == 0)) { CBB_repeat_DayMAPeriod2_N.SelectedIndex = 0; CBB_repeat_DayMAPeriod_배열_N.SelectedIndex = 0; }
        }
        private void CBB_dma_배열_DropDownClosed(object sender, EventArgs e)
        {
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_A) && (CBB_repeat_DayMAPeriod1_A.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_A.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_A.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_B) && (CBB_repeat_DayMAPeriod1_B.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_B.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_B.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_C) && (CBB_repeat_DayMAPeriod1_C.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_C.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_C.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_D) && (CBB_repeat_DayMAPeriod1_D.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_D.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_D.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_E) && (CBB_repeat_DayMAPeriod1_E.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_E.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_E.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_F) && (CBB_repeat_DayMAPeriod1_F.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_F.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_F.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_G) && (CBB_repeat_DayMAPeriod1_G.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_G.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_G.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_H) && (CBB_repeat_DayMAPeriod1_H.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_H.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_H.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_I) && (CBB_repeat_DayMAPeriod1_I.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_I.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_I.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_J) && (CBB_repeat_DayMAPeriod1_J.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_J.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_J.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_K) && (CBB_repeat_DayMAPeriod1_K.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_K.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_K.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_L) && (CBB_repeat_DayMAPeriod1_L.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_L.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_L.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_M) && (CBB_repeat_DayMAPeriod1_M.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_M.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_M.SelectedIndex = 0;
            if (sender.Equals(CBB_repeat_DayMAPeriod_배열_N) && (CBB_repeat_DayMAPeriod1_N.SelectedIndex == 0 || CBB_repeat_DayMAPeriod2_N.SelectedIndex == 0)) CBB_repeat_DayMAPeriod_배열_N.SelectedIndex = 0;
        }
    }
}
