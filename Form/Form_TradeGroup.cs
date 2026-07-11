using System;
using System.Drawing;
using System.Windows.Forms;

namespace 지니64
{
    public partial class Form_TradeGroup : Form
    {
        public static Form_TradeGroup form;

        public Form_TradeGroup()
        {
            form = this;

            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
        }

        public void Form_TradeGroup_Load()
        {
            Form1.음소거 = true;

            CB_IK_group_A.Checked = GenieConfig.CB_IK_group_A;
            CB_IK_group_B.Checked = GenieConfig.CB_IK_group_B;
            CB_IK_group_C.Checked = GenieConfig.CB_IK_group_C;
            CB_IK_group_D.Checked = GenieConfig.CB_IK_group_D;
            CB_IK_group_E.Checked = GenieConfig.CB_IK_group_E;
            CB_IK_group_F.Checked = GenieConfig.CB_IK_group_F;
            CB_IK_group_G.Checked = GenieConfig.CB_IK_group_G;
            CB_IK_group_H.Checked = GenieConfig.CB_IK_group_H;
            CB_IK_group_I.Checked = GenieConfig.CB_IK_group_I;
            CB_IK_group_J.Checked = GenieConfig.CB_IK_group_J;
            CB_IK_group_K.Checked = GenieConfig.CB_IK_group_K;
            CB_IK_group_L.Checked = GenieConfig.CB_IK_group_L;

            CB_손절_A.Checked = GenieConfig.CB_손절_A;
            CB_손절_B.Checked = GenieConfig.CB_손절_B;
            CB_손절_C.Checked = GenieConfig.CB_손절_C;
            CB_손절_D.Checked = GenieConfig.CB_손절_D;
            CB_손절_E.Checked = GenieConfig.CB_손절_E;
            CB_손절_F.Checked = GenieConfig.CB_손절_F;
            CB_손절_G.Checked = GenieConfig.CB_손절_G;
            CB_손절_H.Checked = GenieConfig.CB_손절_H;
            CB_손절_I.Checked = GenieConfig.CB_손절_I;
            CB_손절_J.Checked = GenieConfig.CB_손절_J;
            CB_손절_K.Checked = GenieConfig.CB_손절_K;
            CB_손절_L.Checked = GenieConfig.CB_손절_L;

            CB_특정시간_A.Checked = GenieConfig.CB_특정시간_계좌청산_A;
            CB_특정시간_B.Checked = GenieConfig.CB_특정시간_계좌청산_B;
            CB_특정시간_C.Checked = GenieConfig.CB_특정시간_계좌청산_C;
            CB_특정시간_D.Checked = GenieConfig.CB_특정시간_계좌청산_D;
            CB_특정시간_E.Checked = GenieConfig.CB_특정시간_계좌청산_E;
            CB_특정시간_F.Checked = GenieConfig.CB_특정시간_계좌청산_F;
            CB_특정시간_G.Checked = GenieConfig.CB_특정시간_계좌청산_G;
            CB_특정시간_H.Checked = GenieConfig.CB_특정시간_계좌청산_H;
            CB_특정시간_I.Checked = GenieConfig.CB_특정시간_계좌청산_I;
            CB_특정시간_J.Checked = GenieConfig.CB_특정시간_계좌청산_J;
            CB_특정시간_K.Checked = GenieConfig.CB_특정시간_계좌청산_K;
            CB_특정시간_L.Checked = GenieConfig.CB_특정시간_계좌청산_L;

            CB_실현손익_A.Checked = GenieConfig.CB_실현손익_계좌청산_A;
            CB_실현손익_B.Checked = GenieConfig.CB_실현손익_계좌청산_B;
            CB_실현손익_C.Checked = GenieConfig.CB_실현손익_계좌청산_C;
            CB_실현손익_D.Checked = GenieConfig.CB_실현손익_계좌청산_D;
            CB_실현손익_E.Checked = GenieConfig.CB_실현손익_계좌청산_E;
            CB_실현손익_F.Checked = GenieConfig.CB_실현손익_계좌청산_F;
            CB_실현손익_G.Checked = GenieConfig.CB_실현손익_계좌청산_G;
            CB_실현손익_H.Checked = GenieConfig.CB_실현손익_계좌청산_H;
            CB_실현손익_I.Checked = GenieConfig.CB_실현손익_계좌청산_I;
            CB_실현손익_J.Checked = GenieConfig.CB_실현손익_계좌청산_J;
            CB_실현손익_K.Checked = GenieConfig.CB_실현손익_계좌청산_K;
            CB_실현손익_L.Checked = GenieConfig.CB_실현손익_계좌청산_L;

            CB_예상손실_A.Checked = GenieConfig.CB_예상손실_계좌청산_A;
            CB_예상손실_B.Checked = GenieConfig.CB_예상손실_계좌청산_B;
            CB_예상손실_C.Checked = GenieConfig.CB_예상손실_계좌청산_C;
            CB_예상손실_D.Checked = GenieConfig.CB_예상손실_계좌청산_D;
            CB_예상손실_E.Checked = GenieConfig.CB_예상손실_계좌청산_E;
            CB_예상손실_F.Checked = GenieConfig.CB_예상손실_계좌청산_F;
            CB_예상손실_G.Checked = GenieConfig.CB_예상손실_계좌청산_G;
            CB_예상손실_H.Checked = GenieConfig.CB_예상손실_계좌청산_H;
            CB_예상손실_I.Checked = GenieConfig.CB_예상손실_계좌청산_I;
            CB_예상손실_J.Checked = GenieConfig.CB_예상손실_계좌청산_J;
            CB_예상손실_K.Checked = GenieConfig.CB_예상손실_계좌청산_K;
            CB_예상손실_L.Checked = GenieConfig.CB_예상손실_계좌청산_L;

            CB_예상수익_A.Checked = GenieConfig.CB_예상수익_계좌청산_A;
            CB_예상수익_B.Checked = GenieConfig.CB_예상수익_계좌청산_B;
            CB_예상수익_C.Checked = GenieConfig.CB_예상수익_계좌청산_C;
            CB_예상수익_D.Checked = GenieConfig.CB_예상수익_계좌청산_D;
            CB_예상수익_E.Checked = GenieConfig.CB_예상수익_계좌청산_E;
            CB_예상수익_F.Checked = GenieConfig.CB_예상수익_계좌청산_F;
            CB_예상수익_G.Checked = GenieConfig.CB_예상수익_계좌청산_G;
            CB_예상수익_H.Checked = GenieConfig.CB_예상수익_계좌청산_H;
            CB_예상수익_I.Checked = GenieConfig.CB_예상수익_계좌청산_I;
            CB_예상수익_J.Checked = GenieConfig.CB_예상수익_계좌청산_J;
            CB_예상수익_K.Checked = GenieConfig.CB_예상수익_계좌청산_K;
            CB_예상수익_L.Checked = GenieConfig.CB_예상수익_계좌청산_L;

            CB_Repeat_A_A.Checked = GenieConfig.CB_Repeat_A_A;
            CB_Repeat_A_B.Checked = GenieConfig.CB_Repeat_A_B;
            CB_Repeat_A_C.Checked = GenieConfig.CB_Repeat_A_C;
            CB_Repeat_A_D.Checked = GenieConfig.CB_Repeat_A_D;
            CB_Repeat_A_E.Checked = GenieConfig.CB_Repeat_A_E;
            CB_Repeat_A_F.Checked = GenieConfig.CB_Repeat_A_F;
            CB_Repeat_A_G.Checked = GenieConfig.CB_Repeat_A_G;
            CB_Repeat_A_H.Checked = GenieConfig.CB_Repeat_A_H;
            CB_Repeat_A_I.Checked = GenieConfig.CB_Repeat_A_I;
            CB_Repeat_A_J.Checked = GenieConfig.CB_Repeat_A_J;
            CB_Repeat_A_K.Checked = GenieConfig.CB_Repeat_A_K;
            CB_Repeat_A_L.Checked = GenieConfig.CB_Repeat_A_L;

            CB_Repeat_B_A.Checked = GenieConfig.CB_Repeat_B_A;
            CB_Repeat_B_B.Checked = GenieConfig.CB_Repeat_B_B;
            CB_Repeat_B_C.Checked = GenieConfig.CB_Repeat_B_C;
            CB_Repeat_B_D.Checked = GenieConfig.CB_Repeat_B_D;
            CB_Repeat_B_E.Checked = GenieConfig.CB_Repeat_B_E;
            CB_Repeat_B_F.Checked = GenieConfig.CB_Repeat_B_F;
            CB_Repeat_B_G.Checked = GenieConfig.CB_Repeat_B_G;
            CB_Repeat_B_H.Checked = GenieConfig.CB_Repeat_B_H;
            CB_Repeat_B_I.Checked = GenieConfig.CB_Repeat_B_I;
            CB_Repeat_B_J.Checked = GenieConfig.CB_Repeat_B_J;
            CB_Repeat_B_K.Checked = GenieConfig.CB_Repeat_B_K;
            CB_Repeat_B_L.Checked = GenieConfig.CB_Repeat_B_L;

            CB_Repeat_C_A.Checked = GenieConfig.CB_Repeat_C_A;
            CB_Repeat_C_B.Checked = GenieConfig.CB_Repeat_C_B;
            CB_Repeat_C_C.Checked = GenieConfig.CB_Repeat_C_C;
            CB_Repeat_C_D.Checked = GenieConfig.CB_Repeat_C_D;
            CB_Repeat_C_E.Checked = GenieConfig.CB_Repeat_C_E;
            CB_Repeat_C_F.Checked = GenieConfig.CB_Repeat_C_F;
            CB_Repeat_C_G.Checked = GenieConfig.CB_Repeat_C_G;
            CB_Repeat_C_H.Checked = GenieConfig.CB_Repeat_C_H;
            CB_Repeat_C_I.Checked = GenieConfig.CB_Repeat_C_I;
            CB_Repeat_C_J.Checked = GenieConfig.CB_Repeat_C_J;
            CB_Repeat_C_K.Checked = GenieConfig.CB_Repeat_C_K;
            CB_Repeat_C_L.Checked = GenieConfig.CB_Repeat_C_L;

            CB_Repeat_D_A.Checked = GenieConfig.CB_Repeat_D_A;
            CB_Repeat_D_B.Checked = GenieConfig.CB_Repeat_D_B;
            CB_Repeat_D_C.Checked = GenieConfig.CB_Repeat_D_C;
            CB_Repeat_D_D.Checked = GenieConfig.CB_Repeat_D_D;
            CB_Repeat_D_E.Checked = GenieConfig.CB_Repeat_D_E;
            CB_Repeat_D_F.Checked = GenieConfig.CB_Repeat_D_F;
            CB_Repeat_D_G.Checked = GenieConfig.CB_Repeat_D_G;
            CB_Repeat_D_H.Checked = GenieConfig.CB_Repeat_D_H;
            CB_Repeat_D_I.Checked = GenieConfig.CB_Repeat_D_I;
            CB_Repeat_D_J.Checked = GenieConfig.CB_Repeat_D_J;
            CB_Repeat_D_K.Checked = GenieConfig.CB_Repeat_D_K;
            CB_Repeat_D_L.Checked = GenieConfig.CB_Repeat_D_L;

            CB_Repeat_E_A.Checked = GenieConfig.CB_Repeat_E_A;
            CB_Repeat_E_B.Checked = GenieConfig.CB_Repeat_E_B;
            CB_Repeat_E_C.Checked = GenieConfig.CB_Repeat_E_C;
            CB_Repeat_E_D.Checked = GenieConfig.CB_Repeat_E_D;
            CB_Repeat_E_E.Checked = GenieConfig.CB_Repeat_E_E;
            CB_Repeat_E_F.Checked = GenieConfig.CB_Repeat_E_F;
            CB_Repeat_E_G.Checked = GenieConfig.CB_Repeat_E_G;
            CB_Repeat_E_H.Checked = GenieConfig.CB_Repeat_E_H;
            CB_Repeat_E_I.Checked = GenieConfig.CB_Repeat_E_I;
            CB_Repeat_E_J.Checked = GenieConfig.CB_Repeat_E_J;
            CB_Repeat_E_K.Checked = GenieConfig.CB_Repeat_E_K;
            CB_Repeat_E_L.Checked = GenieConfig.CB_Repeat_E_L;

            CB_Repeat_F_A.Checked = GenieConfig.CB_Repeat_F_A;
            CB_Repeat_F_B.Checked = GenieConfig.CB_Repeat_F_B;
            CB_Repeat_F_C.Checked = GenieConfig.CB_Repeat_F_C;
            CB_Repeat_F_D.Checked = GenieConfig.CB_Repeat_F_D;
            CB_Repeat_F_E.Checked = GenieConfig.CB_Repeat_F_E;
            CB_Repeat_F_F.Checked = GenieConfig.CB_Repeat_F_F;
            CB_Repeat_F_G.Checked = GenieConfig.CB_Repeat_F_G;
            CB_Repeat_F_H.Checked = GenieConfig.CB_Repeat_F_H;
            CB_Repeat_F_I.Checked = GenieConfig.CB_Repeat_F_I;
            CB_Repeat_F_J.Checked = GenieConfig.CB_Repeat_F_J;
            CB_Repeat_F_K.Checked = GenieConfig.CB_Repeat_F_K;
            CB_Repeat_F_L.Checked = GenieConfig.CB_Repeat_F_L;

            CB_Repeat_G_A.Checked = GenieConfig.CB_Repeat_G_A;
            CB_Repeat_G_B.Checked = GenieConfig.CB_Repeat_G_B;
            CB_Repeat_G_C.Checked = GenieConfig.CB_Repeat_G_C;
            CB_Repeat_G_D.Checked = GenieConfig.CB_Repeat_G_D;
            CB_Repeat_G_E.Checked = GenieConfig.CB_Repeat_G_E;
            CB_Repeat_G_F.Checked = GenieConfig.CB_Repeat_G_F;
            CB_Repeat_G_G.Checked = GenieConfig.CB_Repeat_G_G;
            CB_Repeat_G_H.Checked = GenieConfig.CB_Repeat_G_H;
            CB_Repeat_G_I.Checked = GenieConfig.CB_Repeat_G_I;
            CB_Repeat_G_J.Checked = GenieConfig.CB_Repeat_G_J;
            CB_Repeat_G_K.Checked = GenieConfig.CB_Repeat_G_K;
            CB_Repeat_G_L.Checked = GenieConfig.CB_Repeat_G_L;

            CB_Repeat_H_A.Checked = GenieConfig.CB_Repeat_H_A;
            CB_Repeat_H_B.Checked = GenieConfig.CB_Repeat_H_B;
            CB_Repeat_H_C.Checked = GenieConfig.CB_Repeat_H_C;
            CB_Repeat_H_D.Checked = GenieConfig.CB_Repeat_H_D;
            CB_Repeat_H_E.Checked = GenieConfig.CB_Repeat_H_E;
            CB_Repeat_H_F.Checked = GenieConfig.CB_Repeat_H_F;
            CB_Repeat_H_G.Checked = GenieConfig.CB_Repeat_H_G;
            CB_Repeat_H_H.Checked = GenieConfig.CB_Repeat_H_H;
            CB_Repeat_H_I.Checked = GenieConfig.CB_Repeat_H_I;
            CB_Repeat_H_J.Checked = GenieConfig.CB_Repeat_H_J;
            CB_Repeat_H_K.Checked = GenieConfig.CB_Repeat_H_K;
            CB_Repeat_H_L.Checked = GenieConfig.CB_Repeat_H_L;

            CB_Repeat_I_A.Checked = GenieConfig.CB_Repeat_I_A;
            CB_Repeat_I_B.Checked = GenieConfig.CB_Repeat_I_B;
            CB_Repeat_I_C.Checked = GenieConfig.CB_Repeat_I_C;
            CB_Repeat_I_D.Checked = GenieConfig.CB_Repeat_I_D;
            CB_Repeat_I_E.Checked = GenieConfig.CB_Repeat_I_E;
            CB_Repeat_I_F.Checked = GenieConfig.CB_Repeat_I_F;
            CB_Repeat_I_G.Checked = GenieConfig.CB_Repeat_I_G;
            CB_Repeat_I_H.Checked = GenieConfig.CB_Repeat_I_H;
            CB_Repeat_I_I.Checked = GenieConfig.CB_Repeat_I_I;
            CB_Repeat_I_J.Checked = GenieConfig.CB_Repeat_I_J;
            CB_Repeat_I_K.Checked = GenieConfig.CB_Repeat_I_K;
            CB_Repeat_I_L.Checked = GenieConfig.CB_Repeat_I_L;

            CB_Repeat_J_A.Checked = GenieConfig.CB_Repeat_J_A;
            CB_Repeat_J_B.Checked = GenieConfig.CB_Repeat_J_B;
            CB_Repeat_J_C.Checked = GenieConfig.CB_Repeat_J_C;
            CB_Repeat_J_D.Checked = GenieConfig.CB_Repeat_J_D;
            CB_Repeat_J_E.Checked = GenieConfig.CB_Repeat_J_E;
            CB_Repeat_J_F.Checked = GenieConfig.CB_Repeat_J_F;
            CB_Repeat_J_G.Checked = GenieConfig.CB_Repeat_J_G;
            CB_Repeat_J_H.Checked = GenieConfig.CB_Repeat_J_H;
            CB_Repeat_J_I.Checked = GenieConfig.CB_Repeat_J_I;
            CB_Repeat_J_J.Checked = GenieConfig.CB_Repeat_J_J;
            CB_Repeat_J_K.Checked = GenieConfig.CB_Repeat_J_K;
            CB_Repeat_J_L.Checked = GenieConfig.CB_Repeat_J_L;

            CB_Repeat_K_A.Checked = GenieConfig.CB_Repeat_K_A;
            CB_Repeat_K_B.Checked = GenieConfig.CB_Repeat_K_B;
            CB_Repeat_K_C.Checked = GenieConfig.CB_Repeat_K_C;
            CB_Repeat_K_D.Checked = GenieConfig.CB_Repeat_K_D;
            CB_Repeat_K_E.Checked = GenieConfig.CB_Repeat_K_E;
            CB_Repeat_K_F.Checked = GenieConfig.CB_Repeat_K_F;
            CB_Repeat_K_G.Checked = GenieConfig.CB_Repeat_K_G;
            CB_Repeat_K_H.Checked = GenieConfig.CB_Repeat_K_H;
            CB_Repeat_K_I.Checked = GenieConfig.CB_Repeat_K_I;
            CB_Repeat_K_J.Checked = GenieConfig.CB_Repeat_K_J;
            CB_Repeat_K_K.Checked = GenieConfig.CB_Repeat_K_K;
            CB_Repeat_K_L.Checked = GenieConfig.CB_Repeat_K_L;

            CB_Repeat_L_A.Checked = GenieConfig.CB_Repeat_L_A;
            CB_Repeat_L_B.Checked = GenieConfig.CB_Repeat_L_B;
            CB_Repeat_L_C.Checked = GenieConfig.CB_Repeat_L_C;
            CB_Repeat_L_D.Checked = GenieConfig.CB_Repeat_L_D;
            CB_Repeat_L_E.Checked = GenieConfig.CB_Repeat_L_E;
            CB_Repeat_L_F.Checked = GenieConfig.CB_Repeat_L_F;
            CB_Repeat_L_G.Checked = GenieConfig.CB_Repeat_L_G;
            CB_Repeat_L_H.Checked = GenieConfig.CB_Repeat_L_H;
            CB_Repeat_L_I.Checked = GenieConfig.CB_Repeat_L_I;
            CB_Repeat_L_J.Checked = GenieConfig.CB_Repeat_L_J;
            CB_Repeat_L_K.Checked = GenieConfig.CB_Repeat_L_K;
            CB_Repeat_L_L.Checked = GenieConfig.CB_Repeat_L_L;

            CB_Repeat_M_A.Checked = GenieConfig.CB_Repeat_M_A;
            CB_Repeat_M_B.Checked = GenieConfig.CB_Repeat_M_B;
            CB_Repeat_M_C.Checked = GenieConfig.CB_Repeat_M_C;
            CB_Repeat_M_D.Checked = GenieConfig.CB_Repeat_M_D;
            CB_Repeat_M_E.Checked = GenieConfig.CB_Repeat_M_E;
            CB_Repeat_M_F.Checked = GenieConfig.CB_Repeat_M_F;
            CB_Repeat_M_G.Checked = GenieConfig.CB_Repeat_M_G;
            CB_Repeat_M_H.Checked = GenieConfig.CB_Repeat_M_H;
            CB_Repeat_M_I.Checked = GenieConfig.CB_Repeat_M_I;
            CB_Repeat_M_J.Checked = GenieConfig.CB_Repeat_M_J;
            CB_Repeat_M_K.Checked = GenieConfig.CB_Repeat_M_K;
            CB_Repeat_M_L.Checked = GenieConfig.CB_Repeat_M_L;

            CB_Repeat_N_A.Checked = GenieConfig.CB_Repeat_N_A;
            CB_Repeat_N_B.Checked = GenieConfig.CB_Repeat_N_B;
            CB_Repeat_N_C.Checked = GenieConfig.CB_Repeat_N_C;
            CB_Repeat_N_D.Checked = GenieConfig.CB_Repeat_N_D;
            CB_Repeat_N_E.Checked = GenieConfig.CB_Repeat_N_E;
            CB_Repeat_N_F.Checked = GenieConfig.CB_Repeat_N_F;
            CB_Repeat_N_G.Checked = GenieConfig.CB_Repeat_N_G;
            CB_Repeat_N_H.Checked = GenieConfig.CB_Repeat_N_H;
            CB_Repeat_N_I.Checked = GenieConfig.CB_Repeat_N_I;
            CB_Repeat_N_J.Checked = GenieConfig.CB_Repeat_N_J;
            CB_Repeat_N_K.Checked = GenieConfig.CB_Repeat_N_K;
            CB_Repeat_N_L.Checked = GenieConfig.CB_Repeat_N_L;

            CB_day_A_A.Checked = GenieConfig.CB_day_A_A;
            CB_day_A_B.Checked = GenieConfig.CB_day_A_B;
            CB_day_A_C.Checked = GenieConfig.CB_day_A_C;
            CB_day_A_D.Checked = GenieConfig.CB_day_A_D;
            CB_day_A_E.Checked = GenieConfig.CB_day_A_E;
            CB_day_A_F.Checked = GenieConfig.CB_day_A_F;
            CB_day_A_G.Checked = GenieConfig.CB_day_A_G;
            CB_day_A_H.Checked = GenieConfig.CB_day_A_H;
            CB_day_A_I.Checked = GenieConfig.CB_day_A_I;
            CB_day_A_J.Checked = GenieConfig.CB_day_A_J;
            CB_day_A_K.Checked = GenieConfig.CB_day_A_K;
            CB_day_A_L.Checked = GenieConfig.CB_day_A_L;

            CB_day_B_A.Checked = GenieConfig.CB_day_B_A;
            CB_day_B_B.Checked = GenieConfig.CB_day_B_B;
            CB_day_B_C.Checked = GenieConfig.CB_day_B_C;
            CB_day_B_D.Checked = GenieConfig.CB_day_B_D;
            CB_day_B_E.Checked = GenieConfig.CB_day_B_E;
            CB_day_B_F.Checked = GenieConfig.CB_day_B_F;
            CB_day_B_G.Checked = GenieConfig.CB_day_B_G;
            CB_day_B_H.Checked = GenieConfig.CB_day_B_H;
            CB_day_B_I.Checked = GenieConfig.CB_day_B_I;
            CB_day_B_J.Checked = GenieConfig.CB_day_B_J;
            CB_day_B_K.Checked = GenieConfig.CB_day_B_K;
            CB_day_B_L.Checked = GenieConfig.CB_day_B_L;

            CB_day_C_A.Checked = GenieConfig.CB_day_C_A;
            CB_day_C_B.Checked = GenieConfig.CB_day_C_B;
            CB_day_C_C.Checked = GenieConfig.CB_day_C_C;
            CB_day_C_D.Checked = GenieConfig.CB_day_C_D;
            CB_day_C_E.Checked = GenieConfig.CB_day_C_E;
            CB_day_C_F.Checked = GenieConfig.CB_day_C_F;
            CB_day_C_G.Checked = GenieConfig.CB_day_C_G;
            CB_day_C_H.Checked = GenieConfig.CB_day_C_H;
            CB_day_C_I.Checked = GenieConfig.CB_day_C_I;
            CB_day_C_J.Checked = GenieConfig.CB_day_C_J;
            CB_day_C_K.Checked = GenieConfig.CB_day_C_K;
            CB_day_C_L.Checked = GenieConfig.CB_day_C_L;

            CB_day_D_A.Checked = GenieConfig.CB_day_D_A;
            CB_day_D_B.Checked = GenieConfig.CB_day_D_B;
            CB_day_D_C.Checked = GenieConfig.CB_day_D_C;
            CB_day_D_D.Checked = GenieConfig.CB_day_D_D;
            CB_day_D_E.Checked = GenieConfig.CB_day_D_E;
            CB_day_D_F.Checked = GenieConfig.CB_day_D_F;
            CB_day_D_G.Checked = GenieConfig.CB_day_D_G;
            CB_day_D_H.Checked = GenieConfig.CB_day_D_H;
            CB_day_D_I.Checked = GenieConfig.CB_day_D_I;
            CB_day_D_J.Checked = GenieConfig.CB_day_D_J;
            CB_day_D_K.Checked = GenieConfig.CB_day_D_K;
            CB_day_D_L.Checked = GenieConfig.CB_day_D_L;

            CB_day_E_A.Checked = GenieConfig.CB_day_E_A;
            CB_day_E_B.Checked = GenieConfig.CB_day_E_B;
            CB_day_E_C.Checked = GenieConfig.CB_day_E_C;
            CB_day_E_D.Checked = GenieConfig.CB_day_E_D;
            CB_day_E_E.Checked = GenieConfig.CB_day_E_E;
            CB_day_E_F.Checked = GenieConfig.CB_day_E_F;
            CB_day_E_G.Checked = GenieConfig.CB_day_E_G;
            CB_day_E_H.Checked = GenieConfig.CB_day_E_H;
            CB_day_E_I.Checked = GenieConfig.CB_day_E_I;
            CB_day_E_J.Checked = GenieConfig.CB_day_E_J;
            CB_day_E_K.Checked = GenieConfig.CB_day_E_K;
            CB_day_E_L.Checked = GenieConfig.CB_day_E_L;

            CB_day_F_A.Checked = GenieConfig.CB_day_F_A;
            CB_day_F_B.Checked = GenieConfig.CB_day_F_B;
            CB_day_F_C.Checked = GenieConfig.CB_day_F_C;
            CB_day_F_D.Checked = GenieConfig.CB_day_F_D;
            CB_day_F_E.Checked = GenieConfig.CB_day_F_E;
            CB_day_F_F.Checked = GenieConfig.CB_day_F_F;
            CB_day_F_G.Checked = GenieConfig.CB_day_F_G;
            CB_day_F_H.Checked = GenieConfig.CB_day_F_H;
            CB_day_F_I.Checked = GenieConfig.CB_day_F_I;
            CB_day_F_J.Checked = GenieConfig.CB_day_F_J;
            CB_day_F_K.Checked = GenieConfig.CB_day_F_K;
            CB_day_F_L.Checked = GenieConfig.CB_day_F_L;

            CB_Cut_A_A.Checked = GenieConfig.CB_Cut_A_A;
            CB_Cut_A_B.Checked = GenieConfig.CB_Cut_A_B;
            CB_Cut_A_C.Checked = GenieConfig.CB_Cut_A_C;
            CB_Cut_A_D.Checked = GenieConfig.CB_Cut_A_D;
            CB_Cut_A_E.Checked = GenieConfig.CB_Cut_A_E;
            CB_Cut_A_F.Checked = GenieConfig.CB_Cut_A_F;
            CB_Cut_A_G.Checked = GenieConfig.CB_Cut_A_G;
            CB_Cut_A_H.Checked = GenieConfig.CB_Cut_A_H;
            CB_Cut_A_I.Checked = GenieConfig.CB_Cut_A_I;
            CB_Cut_A_J.Checked = GenieConfig.CB_Cut_A_J;
            CB_Cut_A_K.Checked = GenieConfig.CB_Cut_A_K;
            CB_Cut_A_L.Checked = GenieConfig.CB_Cut_A_L;

            CB_Cut_B_A.Checked = GenieConfig.CB_Cut_B_A;
            CB_Cut_B_B.Checked = GenieConfig.CB_Cut_B_B;
            CB_Cut_B_C.Checked = GenieConfig.CB_Cut_B_C;
            CB_Cut_B_D.Checked = GenieConfig.CB_Cut_B_D;
            CB_Cut_B_E.Checked = GenieConfig.CB_Cut_B_E;
            CB_Cut_B_F.Checked = GenieConfig.CB_Cut_B_F;
            CB_Cut_B_G.Checked = GenieConfig.CB_Cut_B_G;
            CB_Cut_B_H.Checked = GenieConfig.CB_Cut_B_H;
            CB_Cut_B_I.Checked = GenieConfig.CB_Cut_B_I;
            CB_Cut_B_J.Checked = GenieConfig.CB_Cut_B_J;
            CB_Cut_B_K.Checked = GenieConfig.CB_Cut_B_K;
            CB_Cut_B_L.Checked = GenieConfig.CB_Cut_B_L;

            CB_Cut_C_A.Checked = GenieConfig.CB_Cut_C_A;
            CB_Cut_C_B.Checked = GenieConfig.CB_Cut_C_B;
            CB_Cut_C_C.Checked = GenieConfig.CB_Cut_C_C;
            CB_Cut_C_D.Checked = GenieConfig.CB_Cut_C_D;
            CB_Cut_C_E.Checked = GenieConfig.CB_Cut_C_E;
            CB_Cut_C_F.Checked = GenieConfig.CB_Cut_C_F;
            CB_Cut_C_G.Checked = GenieConfig.CB_Cut_C_G;
            CB_Cut_C_H.Checked = GenieConfig.CB_Cut_C_H;
            CB_Cut_C_I.Checked = GenieConfig.CB_Cut_C_I;
            CB_Cut_C_J.Checked = GenieConfig.CB_Cut_C_J;
            CB_Cut_C_K.Checked = GenieConfig.CB_Cut_C_K;
            CB_Cut_C_L.Checked = GenieConfig.CB_Cut_C_L;

            CB_미수금정리_A.Checked = GenieConfig.CB_미수금정리_A;
            CB_미수금정리_B.Checked = GenieConfig.CB_미수금정리_B;
            CB_미수금정리_C.Checked = GenieConfig.CB_미수금정리_C;
            CB_미수금정리_D.Checked = GenieConfig.CB_미수금정리_D;
            CB_미수금정리_E.Checked = GenieConfig.CB_미수금정리_E;
            CB_미수금정리_F.Checked = GenieConfig.CB_미수금정리_F;
            CB_미수금정리_G.Checked = GenieConfig.CB_미수금정리_G;
            CB_미수금정리_H.Checked = GenieConfig.CB_미수금정리_H;
            CB_미수금정리_I.Checked = GenieConfig.CB_미수금정리_I;
            CB_미수금정리_J.Checked = GenieConfig.CB_미수금정리_J;
            CB_미수금정리_K.Checked = GenieConfig.CB_미수금정리_K;
            CB_미수금정리_L.Checked = GenieConfig.CB_미수금정리_L;

            CB_rebalance_A_A.Checked = GenieConfig.CB_rebalance_A_A;
            CB_rebalance_A_B.Checked = GenieConfig.CB_rebalance_A_B;
            CB_rebalance_A_C.Checked = GenieConfig.CB_rebalance_A_C;
            CB_rebalance_A_D.Checked = GenieConfig.CB_rebalance_A_D;
            CB_rebalance_A_E.Checked = GenieConfig.CB_rebalance_A_E;
            CB_rebalance_A_F.Checked = GenieConfig.CB_rebalance_A_F;
            CB_rebalance_A_G.Checked = GenieConfig.CB_rebalance_A_G;
            CB_rebalance_A_H.Checked = GenieConfig.CB_rebalance_A_H;
            CB_rebalance_A_I.Checked = GenieConfig.CB_rebalance_A_I;
            CB_rebalance_A_J.Checked = GenieConfig.CB_rebalance_A_J;
            CB_rebalance_A_K.Checked = GenieConfig.CB_rebalance_A_K;
            CB_rebalance_A_L.Checked = GenieConfig.CB_rebalance_A_L;

            CB_rebalance_B_A.Checked = GenieConfig.CB_rebalance_B_A;
            CB_rebalance_B_B.Checked = GenieConfig.CB_rebalance_B_B;
            CB_rebalance_B_C.Checked = GenieConfig.CB_rebalance_B_C;
            CB_rebalance_B_D.Checked = GenieConfig.CB_rebalance_B_D;
            CB_rebalance_B_E.Checked = GenieConfig.CB_rebalance_B_E;
            CB_rebalance_B_F.Checked = GenieConfig.CB_rebalance_B_F;
            CB_rebalance_B_G.Checked = GenieConfig.CB_rebalance_B_G;
            CB_rebalance_B_H.Checked = GenieConfig.CB_rebalance_B_H;
            CB_rebalance_B_I.Checked = GenieConfig.CB_rebalance_B_I;
            CB_rebalance_B_J.Checked = GenieConfig.CB_rebalance_B_J;
            CB_rebalance_B_K.Checked = GenieConfig.CB_rebalance_B_K;
            CB_rebalance_B_L.Checked = GenieConfig.CB_rebalance_B_L;

            CB_rebalance_C_A.Checked = GenieConfig.CB_rebalance_C_A;
            CB_rebalance_C_B.Checked = GenieConfig.CB_rebalance_C_B;
            CB_rebalance_C_C.Checked = GenieConfig.CB_rebalance_C_C;
            CB_rebalance_C_D.Checked = GenieConfig.CB_rebalance_C_D;
            CB_rebalance_C_E.Checked = GenieConfig.CB_rebalance_C_E;
            CB_rebalance_C_F.Checked = GenieConfig.CB_rebalance_C_F;
            CB_rebalance_C_G.Checked = GenieConfig.CB_rebalance_C_G;
            CB_rebalance_C_H.Checked = GenieConfig.CB_rebalance_C_H;
            CB_rebalance_C_I.Checked = GenieConfig.CB_rebalance_C_I;
            CB_rebalance_C_J.Checked = GenieConfig.CB_rebalance_C_J;
            CB_rebalance_C_K.Checked = GenieConfig.CB_rebalance_C_K;
            CB_rebalance_C_L.Checked = GenieConfig.CB_rebalance_C_L;

            CB_rebalance_D_A.Checked = GenieConfig.CB_rebalance_D_A;
            CB_rebalance_D_B.Checked = GenieConfig.CB_rebalance_D_B;
            CB_rebalance_D_C.Checked = GenieConfig.CB_rebalance_D_C;
            CB_rebalance_D_D.Checked = GenieConfig.CB_rebalance_D_D;
            CB_rebalance_D_E.Checked = GenieConfig.CB_rebalance_D_E;
            CB_rebalance_D_F.Checked = GenieConfig.CB_rebalance_D_F;
            CB_rebalance_D_G.Checked = GenieConfig.CB_rebalance_D_G;
            CB_rebalance_D_H.Checked = GenieConfig.CB_rebalance_D_H;
            CB_rebalance_D_I.Checked = GenieConfig.CB_rebalance_D_I;
            CB_rebalance_D_J.Checked = GenieConfig.CB_rebalance_D_J;
            CB_rebalance_D_K.Checked = GenieConfig.CB_rebalance_D_K;
            CB_rebalance_D_L.Checked = GenieConfig.CB_rebalance_D_L;

            CB_rebalance_E_A.Checked = GenieConfig.CB_rebalance_E_A;
            CB_rebalance_E_B.Checked = GenieConfig.CB_rebalance_E_B;
            CB_rebalance_E_C.Checked = GenieConfig.CB_rebalance_E_C;
            CB_rebalance_E_D.Checked = GenieConfig.CB_rebalance_E_D;
            CB_rebalance_E_E.Checked = GenieConfig.CB_rebalance_E_E;
            CB_rebalance_E_F.Checked = GenieConfig.CB_rebalance_E_F;
            CB_rebalance_E_G.Checked = GenieConfig.CB_rebalance_E_G;
            CB_rebalance_E_H.Checked = GenieConfig.CB_rebalance_E_H;
            CB_rebalance_E_I.Checked = GenieConfig.CB_rebalance_E_I;
            CB_rebalance_E_J.Checked = GenieConfig.CB_rebalance_E_J;
            CB_rebalance_E_K.Checked = GenieConfig.CB_rebalance_E_K;
            CB_rebalance_E_L.Checked = GenieConfig.CB_rebalance_E_L;

            CB_rebalance_F_A.Checked = GenieConfig.CB_rebalance_F_A;
            CB_rebalance_F_B.Checked = GenieConfig.CB_rebalance_F_B;
            CB_rebalance_F_C.Checked = GenieConfig.CB_rebalance_F_C;
            CB_rebalance_F_D.Checked = GenieConfig.CB_rebalance_F_D;
            CB_rebalance_F_E.Checked = GenieConfig.CB_rebalance_F_E;
            CB_rebalance_F_F.Checked = GenieConfig.CB_rebalance_F_F;
            CB_rebalance_F_G.Checked = GenieConfig.CB_rebalance_F_G;
            CB_rebalance_F_H.Checked = GenieConfig.CB_rebalance_F_H;
            CB_rebalance_F_I.Checked = GenieConfig.CB_rebalance_F_I;
            CB_rebalance_F_J.Checked = GenieConfig.CB_rebalance_F_J;
            CB_rebalance_F_K.Checked = GenieConfig.CB_rebalance_F_K;
            CB_rebalance_F_L.Checked = GenieConfig.CB_rebalance_F_L;

            CB_rebalance_G_A.Checked = GenieConfig.CB_rebalance_G_A;
            CB_rebalance_G_B.Checked = GenieConfig.CB_rebalance_G_B;
            CB_rebalance_G_C.Checked = GenieConfig.CB_rebalance_G_C;
            CB_rebalance_G_D.Checked = GenieConfig.CB_rebalance_G_D;
            CB_rebalance_G_E.Checked = GenieConfig.CB_rebalance_G_E;
            CB_rebalance_G_F.Checked = GenieConfig.CB_rebalance_G_F;
            CB_rebalance_G_G.Checked = GenieConfig.CB_rebalance_G_G;
            CB_rebalance_G_H.Checked = GenieConfig.CB_rebalance_G_H;
            CB_rebalance_G_I.Checked = GenieConfig.CB_rebalance_G_I;
            CB_rebalance_G_J.Checked = GenieConfig.CB_rebalance_G_J;
            CB_rebalance_G_K.Checked = GenieConfig.CB_rebalance_G_K;
            CB_rebalance_G_L.Checked = GenieConfig.CB_rebalance_G_L;

            CB_Liquidation_A_A.Checked = GenieConfig.CB_Liquidation_A_A;
            CB_Liquidation_A_B.Checked = GenieConfig.CB_Liquidation_A_B;
            CB_Liquidation_A_C.Checked = GenieConfig.CB_Liquidation_A_C;
            CB_Liquidation_A_D.Checked = GenieConfig.CB_Liquidation_A_D;
            CB_Liquidation_A_E.Checked = GenieConfig.CB_Liquidation_A_E;
            CB_Liquidation_A_F.Checked = GenieConfig.CB_Liquidation_A_F;
            CB_Liquidation_A_G.Checked = GenieConfig.CB_Liquidation_A_G;
            CB_Liquidation_A_H.Checked = GenieConfig.CB_Liquidation_A_H;
            CB_Liquidation_A_I.Checked = GenieConfig.CB_Liquidation_A_I;
            CB_Liquidation_A_J.Checked = GenieConfig.CB_Liquidation_A_J;
            CB_Liquidation_A_K.Checked = GenieConfig.CB_Liquidation_A_K;
            CB_Liquidation_A_L.Checked = GenieConfig.CB_Liquidation_A_L;

            CB_Liquidation_B_A.Checked = GenieConfig.CB_Liquidation_B_A;
            CB_Liquidation_B_B.Checked = GenieConfig.CB_Liquidation_B_B;
            CB_Liquidation_B_C.Checked = GenieConfig.CB_Liquidation_B_C;
            CB_Liquidation_B_D.Checked = GenieConfig.CB_Liquidation_B_D;
            CB_Liquidation_B_E.Checked = GenieConfig.CB_Liquidation_B_E;
            CB_Liquidation_B_F.Checked = GenieConfig.CB_Liquidation_B_F;
            CB_Liquidation_B_G.Checked = GenieConfig.CB_Liquidation_B_G;
            CB_Liquidation_B_H.Checked = GenieConfig.CB_Liquidation_B_H;
            CB_Liquidation_B_I.Checked = GenieConfig.CB_Liquidation_B_I;
            CB_Liquidation_B_J.Checked = GenieConfig.CB_Liquidation_B_J;
            CB_Liquidation_B_K.Checked = GenieConfig.CB_Liquidation_B_K;
            CB_Liquidation_B_L.Checked = GenieConfig.CB_Liquidation_B_L;

            CB_Liquidation_C_A.Checked = GenieConfig.CB_Liquidation_C_A;
            CB_Liquidation_C_B.Checked = GenieConfig.CB_Liquidation_C_B;
            CB_Liquidation_C_C.Checked = GenieConfig.CB_Liquidation_C_C;
            CB_Liquidation_C_D.Checked = GenieConfig.CB_Liquidation_C_D;
            CB_Liquidation_C_E.Checked = GenieConfig.CB_Liquidation_C_E;
            CB_Liquidation_C_F.Checked = GenieConfig.CB_Liquidation_C_F;
            CB_Liquidation_C_G.Checked = GenieConfig.CB_Liquidation_C_G;
            CB_Liquidation_C_H.Checked = GenieConfig.CB_Liquidation_C_H;
            CB_Liquidation_C_I.Checked = GenieConfig.CB_Liquidation_C_I;
            CB_Liquidation_C_J.Checked = GenieConfig.CB_Liquidation_C_J;
            CB_Liquidation_C_K.Checked = GenieConfig.CB_Liquidation_C_K;
            CB_Liquidation_C_L.Checked = GenieConfig.CB_Liquidation_C_L;

            form.CB_시간청산A_A.Checked = GenieConfig.CB_시간청산A_A;
            form.CB_시간청산A_B.Checked = GenieConfig.CB_시간청산A_B;
            form.CB_시간청산A_C.Checked = GenieConfig.CB_시간청산A_C;
            form.CB_시간청산A_D.Checked = GenieConfig.CB_시간청산A_D;
            form.CB_시간청산A_E.Checked = GenieConfig.CB_시간청산A_E;
            form.CB_시간청산A_F.Checked = GenieConfig.CB_시간청산A_F;
            form.CB_시간청산A_G.Checked = GenieConfig.CB_시간청산A_G;
            form.CB_시간청산A_H.Checked = GenieConfig.CB_시간청산A_H;
            form.CB_시간청산A_I.Checked = GenieConfig.CB_시간청산A_I;
            form.CB_시간청산A_J.Checked = GenieConfig.CB_시간청산A_J;
            form.CB_시간청산A_K.Checked = GenieConfig.CB_시간청산A_K;
            form.CB_시간청산A_L.Checked = GenieConfig.CB_시간청산A_L;

            form.CB_시간청산B_A.Checked = GenieConfig.CB_시간청산B_A;
            form.CB_시간청산B_B.Checked = GenieConfig.CB_시간청산B_B;
            form.CB_시간청산B_C.Checked = GenieConfig.CB_시간청산B_C;
            form.CB_시간청산B_D.Checked = GenieConfig.CB_시간청산B_D;
            form.CB_시간청산B_E.Checked = GenieConfig.CB_시간청산B_E;
            form.CB_시간청산B_F.Checked = GenieConfig.CB_시간청산B_F;
            form.CB_시간청산B_G.Checked = GenieConfig.CB_시간청산B_G;
            form.CB_시간청산B_H.Checked = GenieConfig.CB_시간청산B_H;
            form.CB_시간청산B_I.Checked = GenieConfig.CB_시간청산B_I;
            form.CB_시간청산B_J.Checked = GenieConfig.CB_시간청산B_J;
            form.CB_시간청산B_K.Checked = GenieConfig.CB_시간청산B_K;
            form.CB_시간청산B_L.Checked = GenieConfig.CB_시간청산B_L;

            form.CB_시간청산C_A.Checked = GenieConfig.CB_시간청산C_A;
            form.CB_시간청산C_B.Checked = GenieConfig.CB_시간청산C_B;
            form.CB_시간청산C_C.Checked = GenieConfig.CB_시간청산C_C;
            form.CB_시간청산C_D.Checked = GenieConfig.CB_시간청산C_D;
            form.CB_시간청산C_E.Checked = GenieConfig.CB_시간청산C_E;
            form.CB_시간청산C_F.Checked = GenieConfig.CB_시간청산C_F;
            form.CB_시간청산C_G.Checked = GenieConfig.CB_시간청산C_G;
            form.CB_시간청산C_H.Checked = GenieConfig.CB_시간청산C_H;
            form.CB_시간청산C_I.Checked = GenieConfig.CB_시간청산C_I;
            form.CB_시간청산C_J.Checked = GenieConfig.CB_시간청산C_J;
            form.CB_시간청산C_K.Checked = GenieConfig.CB_시간청산C_K;
            form.CB_시간청산C_L.Checked = GenieConfig.CB_시간청산C_L;

            Form1.음소거 = GenieConfig.CB_음소거;

            if (GenieConfig.CB_가이드매매) ControllerDisable.Form_TradeGroup_Disable();
        }

        public static void 매매그룹설정_저장()
        {
            // ---------------------------------------------------------
            // 그룹별 매매 설정 저장 (Setting.tradegroup 사용)
            // ---------------------------------------------------------

            GenieConfig.CB_IK_group_A = form.CB_IK_group_A.Checked;
            GenieConfig.CB_IK_group_B = form.CB_IK_group_B.Checked;
            GenieConfig.CB_IK_group_C = form.CB_IK_group_C.Checked;
            GenieConfig.CB_IK_group_D = form.CB_IK_group_D.Checked;
            GenieConfig.CB_IK_group_E = form.CB_IK_group_E.Checked;
            GenieConfig.CB_IK_group_F = form.CB_IK_group_F.Checked;
            GenieConfig.CB_IK_group_G = form.CB_IK_group_G.Checked;
            GenieConfig.CB_IK_group_H = form.CB_IK_group_H.Checked;
            GenieConfig.CB_IK_group_I = form.CB_IK_group_I.Checked;
            GenieConfig.CB_IK_group_J = form.CB_IK_group_J.Checked;
            GenieConfig.CB_IK_group_K = form.CB_IK_group_K.Checked;
            GenieConfig.CB_IK_group_L = form.CB_IK_group_L.Checked;

            GenieConfig.CB_손절_A = form.CB_손절_A.Checked;
            GenieConfig.CB_손절_B = form.CB_손절_B.Checked;
            GenieConfig.CB_손절_C = form.CB_손절_C.Checked;
            GenieConfig.CB_손절_D = form.CB_손절_D.Checked;
            GenieConfig.CB_손절_E = form.CB_손절_E.Checked;
            GenieConfig.CB_손절_F = form.CB_손절_F.Checked;
            GenieConfig.CB_손절_G = form.CB_손절_G.Checked;
            GenieConfig.CB_손절_H = form.CB_손절_H.Checked;
            GenieConfig.CB_손절_I = form.CB_손절_I.Checked;
            GenieConfig.CB_손절_J = form.CB_손절_J.Checked;
            GenieConfig.CB_손절_K = form.CB_손절_K.Checked;
            GenieConfig.CB_손절_L = form.CB_손절_L.Checked;

            GenieConfig.CB_특정시간_계좌청산_A = form.CB_특정시간_A.Checked;
            GenieConfig.CB_특정시간_계좌청산_B = form.CB_특정시간_B.Checked;
            GenieConfig.CB_특정시간_계좌청산_C = form.CB_특정시간_C.Checked;
            GenieConfig.CB_특정시간_계좌청산_D = form.CB_특정시간_D.Checked;
            GenieConfig.CB_특정시간_계좌청산_E = form.CB_특정시간_E.Checked;
            GenieConfig.CB_특정시간_계좌청산_F = form.CB_특정시간_F.Checked;
            GenieConfig.CB_특정시간_계좌청산_G = form.CB_특정시간_G.Checked;
            GenieConfig.CB_특정시간_계좌청산_H = form.CB_특정시간_H.Checked;
            GenieConfig.CB_특정시간_계좌청산_I = form.CB_특정시간_I.Checked;
            GenieConfig.CB_특정시간_계좌청산_J = form.CB_특정시간_J.Checked;
            GenieConfig.CB_특정시간_계좌청산_K = form.CB_특정시간_K.Checked;
            GenieConfig.CB_특정시간_계좌청산_L = form.CB_특정시간_L.Checked;

            GenieConfig.CB_실현손익_계좌청산_A = form.CB_실현손익_A.Checked;
            GenieConfig.CB_실현손익_계좌청산_B = form.CB_실현손익_B.Checked;
            GenieConfig.CB_실현손익_계좌청산_C = form.CB_실현손익_C.Checked;
            GenieConfig.CB_실현손익_계좌청산_D = form.CB_실현손익_D.Checked;
            GenieConfig.CB_실현손익_계좌청산_E = form.CB_실현손익_E.Checked;
            GenieConfig.CB_실현손익_계좌청산_F = form.CB_실현손익_F.Checked;
            GenieConfig.CB_실현손익_계좌청산_G = form.CB_실현손익_G.Checked;
            GenieConfig.CB_실현손익_계좌청산_H = form.CB_실현손익_H.Checked;
            GenieConfig.CB_실현손익_계좌청산_I = form.CB_실현손익_I.Checked;
            GenieConfig.CB_실현손익_계좌청산_J = form.CB_실현손익_J.Checked;
            GenieConfig.CB_실현손익_계좌청산_K = form.CB_실현손익_K.Checked;
            GenieConfig.CB_실현손익_계좌청산_L = form.CB_실현손익_L.Checked;

            GenieConfig.CB_예상손실_계좌청산_A = form.CB_예상손실_A.Checked;
            GenieConfig.CB_예상손실_계좌청산_B = form.CB_예상손실_B.Checked;
            GenieConfig.CB_예상손실_계좌청산_C = form.CB_예상손실_C.Checked;
            GenieConfig.CB_예상손실_계좌청산_D = form.CB_예상손실_D.Checked;
            GenieConfig.CB_예상손실_계좌청산_E = form.CB_예상손실_E.Checked;
            GenieConfig.CB_예상손실_계좌청산_F = form.CB_예상손실_F.Checked;
            GenieConfig.CB_예상손실_계좌청산_G = form.CB_예상손실_G.Checked;
            GenieConfig.CB_예상손실_계좌청산_H = form.CB_예상손실_H.Checked;
            GenieConfig.CB_예상손실_계좌청산_I = form.CB_예상손실_I.Checked;
            GenieConfig.CB_예상손실_계좌청산_J = form.CB_예상손실_J.Checked;
            GenieConfig.CB_예상손실_계좌청산_K = form.CB_예상손실_K.Checked;
            GenieConfig.CB_예상손실_계좌청산_L = form.CB_예상손실_L.Checked;

            GenieConfig.CB_예상수익_계좌청산_A = form.CB_예상수익_A.Checked;
            GenieConfig.CB_예상수익_계좌청산_B = form.CB_예상수익_B.Checked;
            GenieConfig.CB_예상수익_계좌청산_C = form.CB_예상수익_C.Checked;
            GenieConfig.CB_예상수익_계좌청산_D = form.CB_예상수익_D.Checked;
            GenieConfig.CB_예상수익_계좌청산_E = form.CB_예상수익_E.Checked;
            GenieConfig.CB_예상수익_계좌청산_F = form.CB_예상수익_F.Checked;
            GenieConfig.CB_예상수익_계좌청산_G = form.CB_예상수익_G.Checked;
            GenieConfig.CB_예상수익_계좌청산_H = form.CB_예상수익_H.Checked;
            GenieConfig.CB_예상수익_계좌청산_I = form.CB_예상수익_I.Checked;
            GenieConfig.CB_예상수익_계좌청산_J = form.CB_예상수익_J.Checked;
            GenieConfig.CB_예상수익_계좌청산_K = form.CB_예상수익_K.Checked;
            GenieConfig.CB_예상수익_계좌청산_L = form.CB_예상수익_L.Checked;

            GenieConfig.CB_Repeat_A_A = form.CB_Repeat_A_A.Checked;
            GenieConfig.CB_Repeat_A_B = form.CB_Repeat_A_B.Checked;
            GenieConfig.CB_Repeat_A_C = form.CB_Repeat_A_C.Checked;
            GenieConfig.CB_Repeat_A_D = form.CB_Repeat_A_D.Checked;
            GenieConfig.CB_Repeat_A_E = form.CB_Repeat_A_E.Checked;
            GenieConfig.CB_Repeat_A_F = form.CB_Repeat_A_F.Checked;
            GenieConfig.CB_Repeat_A_G = form.CB_Repeat_A_G.Checked;
            GenieConfig.CB_Repeat_A_H = form.CB_Repeat_A_H.Checked;
            GenieConfig.CB_Repeat_A_I = form.CB_Repeat_A_I.Checked;
            GenieConfig.CB_Repeat_A_J = form.CB_Repeat_A_J.Checked;
            GenieConfig.CB_Repeat_A_K = form.CB_Repeat_A_K.Checked;
            GenieConfig.CB_Repeat_A_L = form.CB_Repeat_A_L.Checked;

            GenieConfig.CB_Repeat_B_A = form.CB_Repeat_B_A.Checked;
            GenieConfig.CB_Repeat_B_B = form.CB_Repeat_B_B.Checked;
            GenieConfig.CB_Repeat_B_C = form.CB_Repeat_B_C.Checked;
            GenieConfig.CB_Repeat_B_D = form.CB_Repeat_B_D.Checked;
            GenieConfig.CB_Repeat_B_E = form.CB_Repeat_B_E.Checked;
            GenieConfig.CB_Repeat_B_F = form.CB_Repeat_B_F.Checked;
            GenieConfig.CB_Repeat_B_G = form.CB_Repeat_B_G.Checked;
            GenieConfig.CB_Repeat_B_H = form.CB_Repeat_B_H.Checked;
            GenieConfig.CB_Repeat_B_I = form.CB_Repeat_B_I.Checked;
            GenieConfig.CB_Repeat_B_J = form.CB_Repeat_B_J.Checked;
            GenieConfig.CB_Repeat_B_K = form.CB_Repeat_B_K.Checked;
            GenieConfig.CB_Repeat_B_L = form.CB_Repeat_B_L.Checked;

            GenieConfig.CB_Repeat_C_A = form.CB_Repeat_C_A.Checked;
            GenieConfig.CB_Repeat_C_B = form.CB_Repeat_C_B.Checked;
            GenieConfig.CB_Repeat_C_C = form.CB_Repeat_C_C.Checked;
            GenieConfig.CB_Repeat_C_D = form.CB_Repeat_C_D.Checked;
            GenieConfig.CB_Repeat_C_E = form.CB_Repeat_C_E.Checked;
            GenieConfig.CB_Repeat_C_F = form.CB_Repeat_C_F.Checked;
            GenieConfig.CB_Repeat_C_G = form.CB_Repeat_C_G.Checked;
            GenieConfig.CB_Repeat_C_H = form.CB_Repeat_C_H.Checked;
            GenieConfig.CB_Repeat_C_I = form.CB_Repeat_C_I.Checked;
            GenieConfig.CB_Repeat_C_J = form.CB_Repeat_C_J.Checked;
            GenieConfig.CB_Repeat_C_K = form.CB_Repeat_C_K.Checked;
            GenieConfig.CB_Repeat_C_L = form.CB_Repeat_C_L.Checked;

            GenieConfig.CB_Repeat_D_A = form.CB_Repeat_D_A.Checked;
            GenieConfig.CB_Repeat_D_B = form.CB_Repeat_D_B.Checked;
            GenieConfig.CB_Repeat_D_C = form.CB_Repeat_D_C.Checked;
            GenieConfig.CB_Repeat_D_D = form.CB_Repeat_D_D.Checked;
            GenieConfig.CB_Repeat_D_E = form.CB_Repeat_D_E.Checked;
            GenieConfig.CB_Repeat_D_F = form.CB_Repeat_D_F.Checked;
            GenieConfig.CB_Repeat_D_G = form.CB_Repeat_D_G.Checked;
            GenieConfig.CB_Repeat_D_H = form.CB_Repeat_D_H.Checked;
            GenieConfig.CB_Repeat_D_I = form.CB_Repeat_D_I.Checked;
            GenieConfig.CB_Repeat_D_J = form.CB_Repeat_D_J.Checked;
            GenieConfig.CB_Repeat_D_K = form.CB_Repeat_D_K.Checked;
            GenieConfig.CB_Repeat_D_L = form.CB_Repeat_D_L.Checked;

            GenieConfig.CB_Repeat_E_A = form.CB_Repeat_E_A.Checked;
            GenieConfig.CB_Repeat_E_B = form.CB_Repeat_E_B.Checked;
            GenieConfig.CB_Repeat_E_C = form.CB_Repeat_E_C.Checked;
            GenieConfig.CB_Repeat_E_D = form.CB_Repeat_E_D.Checked;
            GenieConfig.CB_Repeat_E_E = form.CB_Repeat_E_E.Checked;
            GenieConfig.CB_Repeat_E_F = form.CB_Repeat_E_F.Checked;
            GenieConfig.CB_Repeat_E_G = form.CB_Repeat_E_G.Checked;
            GenieConfig.CB_Repeat_E_H = form.CB_Repeat_E_H.Checked;
            GenieConfig.CB_Repeat_E_I = form.CB_Repeat_E_I.Checked;
            GenieConfig.CB_Repeat_E_J = form.CB_Repeat_E_J.Checked;
            GenieConfig.CB_Repeat_E_K = form.CB_Repeat_E_K.Checked;
            GenieConfig.CB_Repeat_E_L = form.CB_Repeat_E_L.Checked;

            GenieConfig.CB_Repeat_F_A = form.CB_Repeat_F_A.Checked;
            GenieConfig.CB_Repeat_F_B = form.CB_Repeat_F_B.Checked;
            GenieConfig.CB_Repeat_F_C = form.CB_Repeat_F_C.Checked;
            GenieConfig.CB_Repeat_F_D = form.CB_Repeat_F_D.Checked;
            GenieConfig.CB_Repeat_F_E = form.CB_Repeat_F_E.Checked;
            GenieConfig.CB_Repeat_F_F = form.CB_Repeat_F_F.Checked;
            GenieConfig.CB_Repeat_F_G = form.CB_Repeat_F_G.Checked;
            GenieConfig.CB_Repeat_F_H = form.CB_Repeat_F_H.Checked;
            GenieConfig.CB_Repeat_F_I = form.CB_Repeat_F_I.Checked;
            GenieConfig.CB_Repeat_F_J = form.CB_Repeat_F_J.Checked;
            GenieConfig.CB_Repeat_F_K = form.CB_Repeat_F_K.Checked;
            GenieConfig.CB_Repeat_F_L = form.CB_Repeat_F_L.Checked;

            GenieConfig.CB_Repeat_G_A = form.CB_Repeat_G_A.Checked;
            GenieConfig.CB_Repeat_G_B = form.CB_Repeat_G_B.Checked;
            GenieConfig.CB_Repeat_G_C = form.CB_Repeat_G_C.Checked;
            GenieConfig.CB_Repeat_G_D = form.CB_Repeat_G_D.Checked;
            GenieConfig.CB_Repeat_G_E = form.CB_Repeat_G_E.Checked;
            GenieConfig.CB_Repeat_G_F = form.CB_Repeat_G_F.Checked;
            GenieConfig.CB_Repeat_G_G = form.CB_Repeat_G_G.Checked;
            GenieConfig.CB_Repeat_G_H = form.CB_Repeat_G_H.Checked;
            GenieConfig.CB_Repeat_G_I = form.CB_Repeat_G_I.Checked;
            GenieConfig.CB_Repeat_G_J = form.CB_Repeat_G_J.Checked;
            GenieConfig.CB_Repeat_G_K = form.CB_Repeat_G_K.Checked;
            GenieConfig.CB_Repeat_G_L = form.CB_Repeat_G_L.Checked;

            GenieConfig.CB_Repeat_H_A = form.CB_Repeat_H_A.Checked;
            GenieConfig.CB_Repeat_H_B = form.CB_Repeat_H_B.Checked;
            GenieConfig.CB_Repeat_H_C = form.CB_Repeat_H_C.Checked;
            GenieConfig.CB_Repeat_H_D = form.CB_Repeat_H_D.Checked;
            GenieConfig.CB_Repeat_H_E = form.CB_Repeat_H_E.Checked;
            GenieConfig.CB_Repeat_H_F = form.CB_Repeat_H_F.Checked;
            GenieConfig.CB_Repeat_H_G = form.CB_Repeat_H_G.Checked;
            GenieConfig.CB_Repeat_H_H = form.CB_Repeat_H_H.Checked;
            GenieConfig.CB_Repeat_H_I = form.CB_Repeat_H_I.Checked;
            GenieConfig.CB_Repeat_H_J = form.CB_Repeat_H_J.Checked;
            GenieConfig.CB_Repeat_H_K = form.CB_Repeat_H_K.Checked;
            GenieConfig.CB_Repeat_H_L = form.CB_Repeat_H_L.Checked;

            GenieConfig.CB_Repeat_I_A = form.CB_Repeat_I_A.Checked;
            GenieConfig.CB_Repeat_I_B = form.CB_Repeat_I_B.Checked;
            GenieConfig.CB_Repeat_I_C = form.CB_Repeat_I_C.Checked;
            GenieConfig.CB_Repeat_I_D = form.CB_Repeat_I_D.Checked;
            GenieConfig.CB_Repeat_I_E = form.CB_Repeat_I_E.Checked;
            GenieConfig.CB_Repeat_I_F = form.CB_Repeat_I_F.Checked;
            GenieConfig.CB_Repeat_I_G = form.CB_Repeat_I_G.Checked;
            GenieConfig.CB_Repeat_I_H = form.CB_Repeat_I_H.Checked;
            GenieConfig.CB_Repeat_I_I = form.CB_Repeat_I_I.Checked;
            GenieConfig.CB_Repeat_I_J = form.CB_Repeat_I_J.Checked;
            GenieConfig.CB_Repeat_I_K = form.CB_Repeat_I_K.Checked;
            GenieConfig.CB_Repeat_I_L = form.CB_Repeat_I_L.Checked;

            GenieConfig.CB_Repeat_J_A = form.CB_Repeat_J_A.Checked;
            GenieConfig.CB_Repeat_J_B = form.CB_Repeat_J_B.Checked;
            GenieConfig.CB_Repeat_J_C = form.CB_Repeat_J_C.Checked;
            GenieConfig.CB_Repeat_J_D = form.CB_Repeat_J_D.Checked;
            GenieConfig.CB_Repeat_J_E = form.CB_Repeat_J_E.Checked;
            GenieConfig.CB_Repeat_J_F = form.CB_Repeat_J_F.Checked;
            GenieConfig.CB_Repeat_J_G = form.CB_Repeat_J_G.Checked;
            GenieConfig.CB_Repeat_J_H = form.CB_Repeat_J_H.Checked;
            GenieConfig.CB_Repeat_J_I = form.CB_Repeat_J_I.Checked;
            GenieConfig.CB_Repeat_J_J = form.CB_Repeat_J_J.Checked;
            GenieConfig.CB_Repeat_J_K = form.CB_Repeat_J_K.Checked;
            GenieConfig.CB_Repeat_J_L = form.CB_Repeat_J_L.Checked;

            GenieConfig.CB_Repeat_K_A = form.CB_Repeat_K_A.Checked;
            GenieConfig.CB_Repeat_K_B = form.CB_Repeat_K_B.Checked;
            GenieConfig.CB_Repeat_K_C = form.CB_Repeat_K_C.Checked;
            GenieConfig.CB_Repeat_K_D = form.CB_Repeat_K_D.Checked;
            GenieConfig.CB_Repeat_K_E = form.CB_Repeat_K_E.Checked;
            GenieConfig.CB_Repeat_K_F = form.CB_Repeat_K_F.Checked;
            GenieConfig.CB_Repeat_K_G = form.CB_Repeat_K_G.Checked;
            GenieConfig.CB_Repeat_K_H = form.CB_Repeat_K_H.Checked;
            GenieConfig.CB_Repeat_K_I = form.CB_Repeat_K_I.Checked;
            GenieConfig.CB_Repeat_K_J = form.CB_Repeat_K_J.Checked;
            GenieConfig.CB_Repeat_K_K = form.CB_Repeat_K_K.Checked;
            GenieConfig.CB_Repeat_K_L = form.CB_Repeat_K_L.Checked;

            GenieConfig.CB_Repeat_L_A = form.CB_Repeat_L_A.Checked;
            GenieConfig.CB_Repeat_L_B = form.CB_Repeat_L_B.Checked;
            GenieConfig.CB_Repeat_L_C = form.CB_Repeat_L_C.Checked;
            GenieConfig.CB_Repeat_L_D = form.CB_Repeat_L_D.Checked;
            GenieConfig.CB_Repeat_L_E = form.CB_Repeat_L_E.Checked;
            GenieConfig.CB_Repeat_L_F = form.CB_Repeat_L_F.Checked;
            GenieConfig.CB_Repeat_L_G = form.CB_Repeat_L_G.Checked;
            GenieConfig.CB_Repeat_L_H = form.CB_Repeat_L_H.Checked;
            GenieConfig.CB_Repeat_L_I = form.CB_Repeat_L_I.Checked;
            GenieConfig.CB_Repeat_L_J = form.CB_Repeat_L_J.Checked;
            GenieConfig.CB_Repeat_L_K = form.CB_Repeat_L_K.Checked;
            GenieConfig.CB_Repeat_L_L = form.CB_Repeat_L_L.Checked;

            GenieConfig.CB_Repeat_M_A = form.CB_Repeat_M_A.Checked;
            GenieConfig.CB_Repeat_M_B = form.CB_Repeat_M_B.Checked;
            GenieConfig.CB_Repeat_M_C = form.CB_Repeat_M_C.Checked;
            GenieConfig.CB_Repeat_M_D = form.CB_Repeat_M_D.Checked;
            GenieConfig.CB_Repeat_M_E = form.CB_Repeat_M_E.Checked;
            GenieConfig.CB_Repeat_M_F = form.CB_Repeat_M_F.Checked;
            GenieConfig.CB_Repeat_M_G = form.CB_Repeat_M_G.Checked;
            GenieConfig.CB_Repeat_M_H = form.CB_Repeat_M_H.Checked;
            GenieConfig.CB_Repeat_M_I = form.CB_Repeat_M_I.Checked;
            GenieConfig.CB_Repeat_M_J = form.CB_Repeat_M_J.Checked;
            GenieConfig.CB_Repeat_M_K = form.CB_Repeat_M_K.Checked;
            GenieConfig.CB_Repeat_M_L = form.CB_Repeat_M_L.Checked;

            GenieConfig.CB_Repeat_N_A = form.CB_Repeat_N_A.Checked;
            GenieConfig.CB_Repeat_N_B = form.CB_Repeat_N_B.Checked;
            GenieConfig.CB_Repeat_N_C = form.CB_Repeat_N_C.Checked;
            GenieConfig.CB_Repeat_N_D = form.CB_Repeat_N_D.Checked;
            GenieConfig.CB_Repeat_N_E = form.CB_Repeat_N_E.Checked;
            GenieConfig.CB_Repeat_N_F = form.CB_Repeat_N_F.Checked;
            GenieConfig.CB_Repeat_N_G = form.CB_Repeat_N_G.Checked;
            GenieConfig.CB_Repeat_N_H = form.CB_Repeat_N_H.Checked;
            GenieConfig.CB_Repeat_N_I = form.CB_Repeat_N_I.Checked;
            GenieConfig.CB_Repeat_N_J = form.CB_Repeat_N_J.Checked;
            GenieConfig.CB_Repeat_N_K = form.CB_Repeat_N_K.Checked;
            GenieConfig.CB_Repeat_N_L = form.CB_Repeat_N_L.Checked;

            GenieConfig.CB_day_A_A = form.CB_day_A_A.Checked;
            GenieConfig.CB_day_A_B = form.CB_day_A_B.Checked;
            GenieConfig.CB_day_A_C = form.CB_day_A_C.Checked;
            GenieConfig.CB_day_A_D = form.CB_day_A_D.Checked;
            GenieConfig.CB_day_A_E = form.CB_day_A_E.Checked;
            GenieConfig.CB_day_A_F = form.CB_day_A_F.Checked;
            GenieConfig.CB_day_A_G = form.CB_day_A_G.Checked;
            GenieConfig.CB_day_A_H = form.CB_day_A_H.Checked;
            GenieConfig.CB_day_A_I = form.CB_day_A_I.Checked;
            GenieConfig.CB_day_A_J = form.CB_day_A_J.Checked;
            GenieConfig.CB_day_A_K = form.CB_day_A_K.Checked;
            GenieConfig.CB_day_A_L = form.CB_day_A_L.Checked;

            GenieConfig.CB_day_B_A = form.CB_day_B_A.Checked;
            GenieConfig.CB_day_B_B = form.CB_day_B_B.Checked;
            GenieConfig.CB_day_B_C = form.CB_day_B_C.Checked;
            GenieConfig.CB_day_B_D = form.CB_day_B_D.Checked;
            GenieConfig.CB_day_B_E = form.CB_day_B_E.Checked;
            GenieConfig.CB_day_B_F = form.CB_day_B_F.Checked;
            GenieConfig.CB_day_B_G = form.CB_day_B_G.Checked;
            GenieConfig.CB_day_B_H = form.CB_day_B_H.Checked;
            GenieConfig.CB_day_B_I = form.CB_day_B_I.Checked;
            GenieConfig.CB_day_B_J = form.CB_day_B_J.Checked;
            GenieConfig.CB_day_B_K = form.CB_day_B_K.Checked;
            GenieConfig.CB_day_B_L = form.CB_day_B_L.Checked;

            GenieConfig.CB_day_C_A = form.CB_day_C_A.Checked;
            GenieConfig.CB_day_C_B = form.CB_day_C_B.Checked;
            GenieConfig.CB_day_C_C = form.CB_day_C_C.Checked;
            GenieConfig.CB_day_C_D = form.CB_day_C_D.Checked;
            GenieConfig.CB_day_C_E = form.CB_day_C_E.Checked;
            GenieConfig.CB_day_C_F = form.CB_day_C_F.Checked;
            GenieConfig.CB_day_C_G = form.CB_day_C_G.Checked;
            GenieConfig.CB_day_C_H = form.CB_day_C_H.Checked;
            GenieConfig.CB_day_C_I = form.CB_day_C_I.Checked;
            GenieConfig.CB_day_C_J = form.CB_day_C_J.Checked;
            GenieConfig.CB_day_C_K = form.CB_day_C_K.Checked;
            GenieConfig.CB_day_C_L = form.CB_day_C_L.Checked;

            GenieConfig.CB_day_D_A = form.CB_day_D_A.Checked;
            GenieConfig.CB_day_D_B = form.CB_day_D_B.Checked;
            GenieConfig.CB_day_D_C = form.CB_day_D_C.Checked;
            GenieConfig.CB_day_D_D = form.CB_day_D_D.Checked;
            GenieConfig.CB_day_D_E = form.CB_day_D_E.Checked;
            GenieConfig.CB_day_D_F = form.CB_day_D_F.Checked;
            GenieConfig.CB_day_D_G = form.CB_day_D_G.Checked;
            GenieConfig.CB_day_D_H = form.CB_day_D_H.Checked;
            GenieConfig.CB_day_D_I = form.CB_day_D_I.Checked;
            GenieConfig.CB_day_D_J = form.CB_day_D_J.Checked;
            GenieConfig.CB_day_D_K = form.CB_day_D_K.Checked;
            GenieConfig.CB_day_D_L = form.CB_day_D_L.Checked;

            GenieConfig.CB_day_E_A = form.CB_day_E_A.Checked;
            GenieConfig.CB_day_E_B = form.CB_day_E_B.Checked;
            GenieConfig.CB_day_E_C = form.CB_day_E_C.Checked;
            GenieConfig.CB_day_E_D = form.CB_day_E_D.Checked;
            GenieConfig.CB_day_E_E = form.CB_day_E_E.Checked;
            GenieConfig.CB_day_E_F = form.CB_day_E_F.Checked;
            GenieConfig.CB_day_E_G = form.CB_day_E_G.Checked;
            GenieConfig.CB_day_E_H = form.CB_day_E_H.Checked;
            GenieConfig.CB_day_E_I = form.CB_day_E_I.Checked;
            GenieConfig.CB_day_E_J = form.CB_day_E_J.Checked;
            GenieConfig.CB_day_E_K = form.CB_day_E_K.Checked;
            GenieConfig.CB_day_E_L = form.CB_day_E_L.Checked;

            GenieConfig.CB_day_F_A = form.CB_day_F_A.Checked;
            GenieConfig.CB_day_F_B = form.CB_day_F_B.Checked;
            GenieConfig.CB_day_F_C = form.CB_day_F_C.Checked;
            GenieConfig.CB_day_F_D = form.CB_day_F_D.Checked;
            GenieConfig.CB_day_F_E = form.CB_day_F_E.Checked;
            GenieConfig.CB_day_F_F = form.CB_day_F_F.Checked;
            GenieConfig.CB_day_F_G = form.CB_day_F_G.Checked;
            GenieConfig.CB_day_F_H = form.CB_day_F_H.Checked;
            GenieConfig.CB_day_F_I = form.CB_day_F_I.Checked;
            GenieConfig.CB_day_F_J = form.CB_day_F_J.Checked;
            GenieConfig.CB_day_F_K = form.CB_day_F_K.Checked;
            GenieConfig.CB_day_F_L = form.CB_day_F_L.Checked;

            GenieConfig.CB_Cut_A_A = form.CB_Cut_A_A.Checked;
            GenieConfig.CB_Cut_A_B = form.CB_Cut_A_B.Checked;
            GenieConfig.CB_Cut_A_C = form.CB_Cut_A_C.Checked;
            GenieConfig.CB_Cut_A_D = form.CB_Cut_A_D.Checked;
            GenieConfig.CB_Cut_A_E = form.CB_Cut_A_E.Checked;
            GenieConfig.CB_Cut_A_F = form.CB_Cut_A_F.Checked;
            GenieConfig.CB_Cut_A_G = form.CB_Cut_A_G.Checked;
            GenieConfig.CB_Cut_A_H = form.CB_Cut_A_H.Checked;
            GenieConfig.CB_Cut_A_I = form.CB_Cut_A_I.Checked;
            GenieConfig.CB_Cut_A_J = form.CB_Cut_A_J.Checked;
            GenieConfig.CB_Cut_A_K = form.CB_Cut_A_K.Checked;
            GenieConfig.CB_Cut_A_L = form.CB_Cut_A_L.Checked;

            GenieConfig.CB_Cut_B_A = form.CB_Cut_B_A.Checked;
            GenieConfig.CB_Cut_B_B = form.CB_Cut_B_B.Checked;
            GenieConfig.CB_Cut_B_C = form.CB_Cut_B_C.Checked;
            GenieConfig.CB_Cut_B_D = form.CB_Cut_B_D.Checked;
            GenieConfig.CB_Cut_B_E = form.CB_Cut_B_E.Checked;
            GenieConfig.CB_Cut_B_F = form.CB_Cut_B_F.Checked;
            GenieConfig.CB_Cut_B_G = form.CB_Cut_B_G.Checked;
            GenieConfig.CB_Cut_B_H = form.CB_Cut_B_H.Checked;
            GenieConfig.CB_Cut_B_I = form.CB_Cut_B_I.Checked;
            GenieConfig.CB_Cut_B_J = form.CB_Cut_B_J.Checked;
            GenieConfig.CB_Cut_B_K = form.CB_Cut_B_K.Checked;
            GenieConfig.CB_Cut_B_L = form.CB_Cut_B_L.Checked;

            GenieConfig.CB_Cut_C_A = form.CB_Cut_C_A.Checked;
            GenieConfig.CB_Cut_C_B = form.CB_Cut_C_B.Checked;
            GenieConfig.CB_Cut_C_C = form.CB_Cut_C_C.Checked;
            GenieConfig.CB_Cut_C_D = form.CB_Cut_C_D.Checked;
            GenieConfig.CB_Cut_C_E = form.CB_Cut_C_E.Checked;
            GenieConfig.CB_Cut_C_F = form.CB_Cut_C_F.Checked;
            GenieConfig.CB_Cut_C_G = form.CB_Cut_C_G.Checked;
            GenieConfig.CB_Cut_C_H = form.CB_Cut_C_H.Checked;
            GenieConfig.CB_Cut_C_I = form.CB_Cut_C_I.Checked;
            GenieConfig.CB_Cut_C_J = form.CB_Cut_C_J.Checked;
            GenieConfig.CB_Cut_C_K = form.CB_Cut_C_K.Checked;
            GenieConfig.CB_Cut_C_L = form.CB_Cut_C_L.Checked;

            GenieConfig.CB_rebalance_A_A = form.CB_rebalance_A_A.Checked;
            GenieConfig.CB_rebalance_A_B = form.CB_rebalance_A_B.Checked;
            GenieConfig.CB_rebalance_A_C = form.CB_rebalance_A_C.Checked;
            GenieConfig.CB_rebalance_A_D = form.CB_rebalance_A_D.Checked;
            GenieConfig.CB_rebalance_A_E = form.CB_rebalance_A_E.Checked;
            GenieConfig.CB_rebalance_A_F = form.CB_rebalance_A_F.Checked;
            GenieConfig.CB_rebalance_A_G = form.CB_rebalance_A_G.Checked;
            GenieConfig.CB_rebalance_A_H = form.CB_rebalance_A_H.Checked;
            GenieConfig.CB_rebalance_A_I = form.CB_rebalance_A_I.Checked;
            GenieConfig.CB_rebalance_A_J = form.CB_rebalance_A_J.Checked;
            GenieConfig.CB_rebalance_A_K = form.CB_rebalance_A_K.Checked;
            GenieConfig.CB_rebalance_A_L = form.CB_rebalance_A_L.Checked;

            GenieConfig.CB_rebalance_B_A = form.CB_rebalance_B_A.Checked;
            GenieConfig.CB_rebalance_B_B = form.CB_rebalance_B_B.Checked;
            GenieConfig.CB_rebalance_B_C = form.CB_rebalance_B_C.Checked;
            GenieConfig.CB_rebalance_B_D = form.CB_rebalance_B_D.Checked;
            GenieConfig.CB_rebalance_B_E = form.CB_rebalance_B_E.Checked;
            GenieConfig.CB_rebalance_B_F = form.CB_rebalance_B_F.Checked;
            GenieConfig.CB_rebalance_B_G = form.CB_rebalance_B_G.Checked;
            GenieConfig.CB_rebalance_B_H = form.CB_rebalance_B_H.Checked;
            GenieConfig.CB_rebalance_B_I = form.CB_rebalance_B_I.Checked;
            GenieConfig.CB_rebalance_B_J = form.CB_rebalance_B_J.Checked;
            GenieConfig.CB_rebalance_B_K = form.CB_rebalance_B_K.Checked;
            GenieConfig.CB_rebalance_B_L = form.CB_rebalance_B_L.Checked;

            GenieConfig.CB_rebalance_C_A = form.CB_rebalance_C_A.Checked;
            GenieConfig.CB_rebalance_C_B = form.CB_rebalance_C_B.Checked;
            GenieConfig.CB_rebalance_C_C = form.CB_rebalance_C_C.Checked;
            GenieConfig.CB_rebalance_C_D = form.CB_rebalance_C_D.Checked;
            GenieConfig.CB_rebalance_C_E = form.CB_rebalance_C_E.Checked;
            GenieConfig.CB_rebalance_C_F = form.CB_rebalance_C_F.Checked;
            GenieConfig.CB_rebalance_C_G = form.CB_rebalance_C_G.Checked;
            GenieConfig.CB_rebalance_C_H = form.CB_rebalance_C_H.Checked;
            GenieConfig.CB_rebalance_C_I = form.CB_rebalance_C_I.Checked;
            GenieConfig.CB_rebalance_C_J = form.CB_rebalance_C_J.Checked;
            GenieConfig.CB_rebalance_C_K = form.CB_rebalance_C_K.Checked;
            GenieConfig.CB_rebalance_C_L = form.CB_rebalance_C_L.Checked;

            GenieConfig.CB_rebalance_D_A = form.CB_rebalance_D_A.Checked;
            GenieConfig.CB_rebalance_D_B = form.CB_rebalance_D_B.Checked;
            GenieConfig.CB_rebalance_D_C = form.CB_rebalance_D_C.Checked;
            GenieConfig.CB_rebalance_D_D = form.CB_rebalance_D_D.Checked;
            GenieConfig.CB_rebalance_D_E = form.CB_rebalance_D_E.Checked;
            GenieConfig.CB_rebalance_D_F = form.CB_rebalance_D_F.Checked;
            GenieConfig.CB_rebalance_D_G = form.CB_rebalance_D_G.Checked;
            GenieConfig.CB_rebalance_D_H = form.CB_rebalance_D_H.Checked;
            GenieConfig.CB_rebalance_D_I = form.CB_rebalance_D_I.Checked;
            GenieConfig.CB_rebalance_D_J = form.CB_rebalance_D_J.Checked;
            GenieConfig.CB_rebalance_D_K = form.CB_rebalance_D_K.Checked;
            GenieConfig.CB_rebalance_D_L = form.CB_rebalance_D_L.Checked;

            GenieConfig.CB_rebalance_E_A = form.CB_rebalance_E_A.Checked;
            GenieConfig.CB_rebalance_E_B = form.CB_rebalance_E_B.Checked;
            GenieConfig.CB_rebalance_E_C = form.CB_rebalance_E_C.Checked;
            GenieConfig.CB_rebalance_E_D = form.CB_rebalance_E_D.Checked;
            GenieConfig.CB_rebalance_E_E = form.CB_rebalance_E_E.Checked;
            GenieConfig.CB_rebalance_E_F = form.CB_rebalance_E_F.Checked;
            GenieConfig.CB_rebalance_E_G = form.CB_rebalance_E_G.Checked;
            GenieConfig.CB_rebalance_E_H = form.CB_rebalance_E_H.Checked;
            GenieConfig.CB_rebalance_E_I = form.CB_rebalance_E_I.Checked;
            GenieConfig.CB_rebalance_E_J = form.CB_rebalance_E_J.Checked;
            GenieConfig.CB_rebalance_E_K = form.CB_rebalance_E_K.Checked;
            GenieConfig.CB_rebalance_E_L = form.CB_rebalance_E_L.Checked;

            GenieConfig.CB_rebalance_F_A = form.CB_rebalance_F_A.Checked;
            GenieConfig.CB_rebalance_F_B = form.CB_rebalance_F_B.Checked;
            GenieConfig.CB_rebalance_F_C = form.CB_rebalance_F_C.Checked;
            GenieConfig.CB_rebalance_F_D = form.CB_rebalance_F_D.Checked;
            GenieConfig.CB_rebalance_F_E = form.CB_rebalance_F_E.Checked;
            GenieConfig.CB_rebalance_F_F = form.CB_rebalance_F_F.Checked;
            GenieConfig.CB_rebalance_F_G = form.CB_rebalance_F_G.Checked;
            GenieConfig.CB_rebalance_F_H = form.CB_rebalance_F_H.Checked;
            GenieConfig.CB_rebalance_F_I = form.CB_rebalance_F_I.Checked;
            GenieConfig.CB_rebalance_F_J = form.CB_rebalance_F_J.Checked;
            GenieConfig.CB_rebalance_F_K = form.CB_rebalance_F_K.Checked;
            GenieConfig.CB_rebalance_F_L = form.CB_rebalance_F_L.Checked;

            GenieConfig.CB_rebalance_G_A = form.CB_rebalance_G_A.Checked;
            GenieConfig.CB_rebalance_G_B = form.CB_rebalance_G_B.Checked;
            GenieConfig.CB_rebalance_G_C = form.CB_rebalance_G_C.Checked;
            GenieConfig.CB_rebalance_G_D = form.CB_rebalance_G_D.Checked;
            GenieConfig.CB_rebalance_G_E = form.CB_rebalance_G_E.Checked;
            GenieConfig.CB_rebalance_G_F = form.CB_rebalance_G_F.Checked;
            GenieConfig.CB_rebalance_G_G = form.CB_rebalance_G_G.Checked;
            GenieConfig.CB_rebalance_G_H = form.CB_rebalance_G_H.Checked;
            GenieConfig.CB_rebalance_G_I = form.CB_rebalance_G_I.Checked;
            GenieConfig.CB_rebalance_G_J = form.CB_rebalance_G_J.Checked;
            GenieConfig.CB_rebalance_G_K = form.CB_rebalance_G_K.Checked;
            GenieConfig.CB_rebalance_G_L = form.CB_rebalance_G_L.Checked;

            GenieConfig.CB_Liquidation_A_A = form.CB_Liquidation_A_A.Checked;
            GenieConfig.CB_Liquidation_A_B = form.CB_Liquidation_A_B.Checked;
            GenieConfig.CB_Liquidation_A_C = form.CB_Liquidation_A_C.Checked;
            GenieConfig.CB_Liquidation_A_D = form.CB_Liquidation_A_D.Checked;
            GenieConfig.CB_Liquidation_A_E = form.CB_Liquidation_A_E.Checked;
            GenieConfig.CB_Liquidation_A_F = form.CB_Liquidation_A_F.Checked;
            GenieConfig.CB_Liquidation_A_G = form.CB_Liquidation_A_G.Checked;
            GenieConfig.CB_Liquidation_A_H = form.CB_Liquidation_A_H.Checked;
            GenieConfig.CB_Liquidation_A_I = form.CB_Liquidation_A_I.Checked;
            GenieConfig.CB_Liquidation_A_J = form.CB_Liquidation_A_J.Checked;
            GenieConfig.CB_Liquidation_A_K = form.CB_Liquidation_A_K.Checked;
            GenieConfig.CB_Liquidation_A_L = form.CB_Liquidation_A_L.Checked;

            GenieConfig.CB_Liquidation_B_A = form.CB_Liquidation_B_A.Checked;
            GenieConfig.CB_Liquidation_B_B = form.CB_Liquidation_B_B.Checked;
            GenieConfig.CB_Liquidation_B_C = form.CB_Liquidation_B_C.Checked;
            GenieConfig.CB_Liquidation_B_D = form.CB_Liquidation_B_D.Checked;
            GenieConfig.CB_Liquidation_B_E = form.CB_Liquidation_B_E.Checked;
            GenieConfig.CB_Liquidation_B_F = form.CB_Liquidation_B_F.Checked;
            GenieConfig.CB_Liquidation_B_G = form.CB_Liquidation_B_G.Checked;
            GenieConfig.CB_Liquidation_B_H = form.CB_Liquidation_B_H.Checked;
            GenieConfig.CB_Liquidation_B_I = form.CB_Liquidation_B_I.Checked;
            GenieConfig.CB_Liquidation_B_J = form.CB_Liquidation_B_J.Checked;
            GenieConfig.CB_Liquidation_B_K = form.CB_Liquidation_B_K.Checked;
            GenieConfig.CB_Liquidation_B_L = form.CB_Liquidation_B_L.Checked;

            GenieConfig.CB_Liquidation_C_A = form.CB_Liquidation_C_A.Checked;
            GenieConfig.CB_Liquidation_C_B = form.CB_Liquidation_C_B.Checked;
            GenieConfig.CB_Liquidation_C_C = form.CB_Liquidation_C_C.Checked;
            GenieConfig.CB_Liquidation_C_D = form.CB_Liquidation_C_D.Checked;
            GenieConfig.CB_Liquidation_C_E = form.CB_Liquidation_C_E.Checked;
            GenieConfig.CB_Liquidation_C_F = form.CB_Liquidation_C_F.Checked;
            GenieConfig.CB_Liquidation_C_G = form.CB_Liquidation_C_G.Checked;
            GenieConfig.CB_Liquidation_C_H = form.CB_Liquidation_C_H.Checked;
            GenieConfig.CB_Liquidation_C_I = form.CB_Liquidation_C_I.Checked;
            GenieConfig.CB_Liquidation_C_J = form.CB_Liquidation_C_J.Checked;
            GenieConfig.CB_Liquidation_C_K = form.CB_Liquidation_C_K.Checked;
            GenieConfig.CB_Liquidation_C_L = form.CB_Liquidation_C_L.Checked;

            GenieConfig.CB_미수금정리_A = form.CB_미수금정리_A.Checked;
            GenieConfig.CB_미수금정리_B = form.CB_미수금정리_B.Checked;
            GenieConfig.CB_미수금정리_C = form.CB_미수금정리_C.Checked;
            GenieConfig.CB_미수금정리_D = form.CB_미수금정리_D.Checked;
            GenieConfig.CB_미수금정리_E = form.CB_미수금정리_E.Checked;
            GenieConfig.CB_미수금정리_F = form.CB_미수금정리_F.Checked;
            GenieConfig.CB_미수금정리_G = form.CB_미수금정리_G.Checked;
            GenieConfig.CB_미수금정리_H = form.CB_미수금정리_H.Checked;
            GenieConfig.CB_미수금정리_I = form.CB_미수금정리_I.Checked;
            GenieConfig.CB_미수금정리_J = form.CB_미수금정리_J.Checked;
            GenieConfig.CB_미수금정리_K = form.CB_미수금정리_K.Checked;
            GenieConfig.CB_미수금정리_L = form.CB_미수금정리_L.Checked;

            GenieConfig.CB_시간청산A_A = form.CB_시간청산A_A.Checked;
            GenieConfig.CB_시간청산A_B = form.CB_시간청산A_B.Checked;
            GenieConfig.CB_시간청산A_C = form.CB_시간청산A_C.Checked;
            GenieConfig.CB_시간청산A_D = form.CB_시간청산A_D.Checked;
            GenieConfig.CB_시간청산A_E = form.CB_시간청산A_E.Checked;
            GenieConfig.CB_시간청산A_F = form.CB_시간청산A_F.Checked;
            GenieConfig.CB_시간청산A_G = form.CB_시간청산A_G.Checked;
            GenieConfig.CB_시간청산A_H = form.CB_시간청산A_H.Checked;
            GenieConfig.CB_시간청산A_I = form.CB_시간청산A_I.Checked;
            GenieConfig.CB_시간청산A_J = form.CB_시간청산A_J.Checked;
            GenieConfig.CB_시간청산A_K = form.CB_시간청산A_K.Checked;
            GenieConfig.CB_시간청산A_L = form.CB_시간청산A_L.Checked;

            GenieConfig.CB_시간청산B_A = form.CB_시간청산B_A.Checked;
            GenieConfig.CB_시간청산B_B = form.CB_시간청산B_B.Checked;
            GenieConfig.CB_시간청산B_C = form.CB_시간청산B_C.Checked;
            GenieConfig.CB_시간청산B_D = form.CB_시간청산B_D.Checked;
            GenieConfig.CB_시간청산B_E = form.CB_시간청산B_E.Checked;
            GenieConfig.CB_시간청산B_F = form.CB_시간청산B_F.Checked;
            GenieConfig.CB_시간청산B_G = form.CB_시간청산B_G.Checked;
            GenieConfig.CB_시간청산B_H = form.CB_시간청산B_H.Checked;
            GenieConfig.CB_시간청산B_I = form.CB_시간청산B_I.Checked;
            GenieConfig.CB_시간청산B_J = form.CB_시간청산B_J.Checked;
            GenieConfig.CB_시간청산B_K = form.CB_시간청산B_K.Checked;
            GenieConfig.CB_시간청산B_L = form.CB_시간청산B_L.Checked;

            GenieConfig.CB_시간청산C_A = form.CB_시간청산C_A.Checked;
            GenieConfig.CB_시간청산C_B = form.CB_시간청산C_B.Checked;
            GenieConfig.CB_시간청산C_C = form.CB_시간청산C_C.Checked;
            GenieConfig.CB_시간청산C_D = form.CB_시간청산C_D.Checked;
            GenieConfig.CB_시간청산C_E = form.CB_시간청산C_E.Checked;
            GenieConfig.CB_시간청산C_F = form.CB_시간청산C_F.Checked;
            GenieConfig.CB_시간청산C_G = form.CB_시간청산C_G.Checked;
            GenieConfig.CB_시간청산C_H = form.CB_시간청산C_H.Checked;
            GenieConfig.CB_시간청산C_I = form.CB_시간청산C_I.Checked;
            GenieConfig.CB_시간청산C_J = form.CB_시간청산C_J.Checked;
            GenieConfig.CB_시간청산C_K = form.CB_시간청산C_K.Checked;
            GenieConfig.CB_시간청산C_L = form.CB_시간청산C_L.Checked;

        }

        private void Form_TradeGroup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                Form1.form1.CB_매매그룹.Checked = false;
            }
        }

        private void CB_레이아웃고정_매매그룹_CheckedChanged(object sender, EventArgs e)
        {
            GenieConfig.CB_레이아웃고정_매매그룹 = CB_레이아웃고정_매매그룹.Checked;

            if (!CB_레이아웃고정_매매그룹.Checked) LayoutChange.CBB_layout_SelectedIndex(-1);
            else LayoutChange.CBB_layout_SelectedIndex(GenieConfig.CBB_layout);
        }
        private void BT_ALL_check_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Group_allcheck(sender, true);
        }

        private void BT_ALL_Unchecked_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Group_allcheck(sender, false);
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            // 비프음 소리 내는 함수는 그대로 실행
            Form1.form1.체크박스_비프(sender);

            // 1. 방어막: 보낸 놈이 CheckBox가 아니거나, 텍스트가 2글자 미만이면 즉시 탈출! (프로그램 뻗음 방지)
            if (!(sender is CheckBox cb) || string.IsNullOrEmpty(cb.Text) || cb.Text.Length < 2) return;

            // 맨 앞 2글자만 안전하게 잘라냄 (예: "매수사용" -> "매수")
            string prefix = cb.Text[..2];

            string targetText;
            Color targetColor;

            // 2. 바꿀 목표(Text, Color)를 미리 정해둠
            if (cb.Checked)
            {
                targetText = prefix + "사용";
                targetColor = Color.Red;
            }
            else
            {
                targetText = prefix + "- ";
                targetColor = Color.Black;
            }

            // 3. [최적화 핵심] 목표값과 현재값이 '진짜로 다를 때만' 화면을 다시 그린다!
            if (cb.Text != targetText)
            {
                cb.Text = targetText;
            }

            if (cb.ForeColor != targetColor)
            {
                cb.ForeColor = targetColor;
            }
        }

        private void Group_allcheck(object sender, bool Check)
        {
            if ((sender as Button).Name.ToString().Contains("BT_ik_all"))
            {
                CB_IK_group_A.Checked = Check;
                CB_IK_group_B.Checked = Check;
                CB_IK_group_C.Checked = Check;
                CB_IK_group_D.Checked = Check;
                CB_IK_group_E.Checked = Check;
                CB_IK_group_F.Checked = Check;
                CB_IK_group_G.Checked = Check;
                CB_IK_group_H.Checked = Check;
                CB_IK_group_I.Checked = Check;
                CB_IK_group_J.Checked = Check;
                CB_IK_group_K.Checked = Check;
                CB_IK_group_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("BT_sell_all"))
            {
                CB_손절_A.Checked = Check;
                CB_손절_B.Checked = Check;
                CB_손절_C.Checked = Check;
                CB_손절_D.Checked = Check;
                CB_손절_E.Checked = Check;
                CB_손절_F.Checked = Check;
                CB_손절_G.Checked = Check;
                CB_손절_H.Checked = Check;
                CB_손절_I.Checked = Check;
                CB_손절_J.Checked = Check;
                CB_손절_K.Checked = Check;
                CB_손절_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("특정시간"))
            {
                CB_특정시간_A.Checked = Check;
                CB_특정시간_B.Checked = Check;
                CB_특정시간_C.Checked = Check;
                CB_특정시간_D.Checked = Check;
                CB_특정시간_E.Checked = Check;
                CB_특정시간_F.Checked = Check;
                CB_특정시간_G.Checked = Check;
                CB_특정시간_H.Checked = Check;
                CB_특정시간_I.Checked = Check;
                CB_특정시간_J.Checked = Check;
                CB_특정시간_K.Checked = Check;
                CB_특정시간_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("실현손익"))
            {
                CB_실현손익_A.Checked = Check;
                CB_실현손익_B.Checked = Check;
                CB_실현손익_C.Checked = Check;
                CB_실현손익_D.Checked = Check;
                CB_실현손익_E.Checked = Check;
                CB_실현손익_F.Checked = Check;
                CB_실현손익_G.Checked = Check;
                CB_실현손익_H.Checked = Check;
                CB_실현손익_I.Checked = Check;
                CB_실현손익_J.Checked = Check;
                CB_실현손익_K.Checked = Check;
                CB_실현손익_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("예상손실"))
            {
                CB_예상손실_A.Checked = Check;
                CB_예상손실_B.Checked = Check;
                CB_예상손실_C.Checked = Check;
                CB_예상손실_D.Checked = Check;
                CB_예상손실_E.Checked = Check;
                CB_예상손실_F.Checked = Check;
                CB_예상손실_G.Checked = Check;
                CB_예상손실_H.Checked = Check;
                CB_예상손실_I.Checked = Check;
                CB_예상손실_J.Checked = Check;
                CB_예상손실_K.Checked = Check;
                CB_예상손실_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("예상수익"))
            {
                CB_예상수익_A.Checked = Check;
                CB_예상수익_B.Checked = Check;
                CB_예상수익_C.Checked = Check;
                CB_예상수익_D.Checked = Check;
                CB_예상수익_E.Checked = Check;
                CB_예상수익_F.Checked = Check;
                CB_예상수익_G.Checked = Check;
                CB_예상수익_H.Checked = Check;
                CB_예상수익_I.Checked = Check;
                CB_예상수익_J.Checked = Check;
                CB_예상수익_K.Checked = Check;
                CB_예상수익_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("시간청산_A"))
            {
                CB_시간청산A_A.Checked = Check;
                CB_시간청산A_B.Checked = Check;
                CB_시간청산A_C.Checked = Check;
                CB_시간청산A_D.Checked = Check;
                CB_시간청산A_E.Checked = Check;
                CB_시간청산A_F.Checked = Check;
                CB_시간청산A_G.Checked = Check;
                CB_시간청산A_H.Checked = Check;
                CB_시간청산A_I.Checked = Check;
                CB_시간청산A_J.Checked = Check;
                CB_시간청산A_K.Checked = Check;
                CB_시간청산A_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("시간청산_B"))
            {
                CB_시간청산B_A.Checked = Check;
                CB_시간청산B_B.Checked = Check;
                CB_시간청산B_C.Checked = Check;
                CB_시간청산B_D.Checked = Check;
                CB_시간청산B_E.Checked = Check;
                CB_시간청산B_F.Checked = Check;
                CB_시간청산B_G.Checked = Check;
                CB_시간청산B_H.Checked = Check;
                CB_시간청산B_I.Checked = Check;
                CB_시간청산B_J.Checked = Check;
                CB_시간청산B_K.Checked = Check;
                CB_시간청산B_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("시간청산_C"))
            {
                CB_시간청산C_A.Checked = Check;
                CB_시간청산C_B.Checked = Check;
                CB_시간청산C_C.Checked = Check;
                CB_시간청산C_D.Checked = Check;
                CB_시간청산C_E.Checked = Check;
                CB_시간청산C_F.Checked = Check;
                CB_시간청산C_G.Checked = Check;
                CB_시간청산C_H.Checked = Check;
                CB_시간청산C_I.Checked = Check;
                CB_시간청산C_J.Checked = Check;
                CB_시간청산C_K.Checked = Check;
                CB_시간청산C_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("_Repeat_A"))
            {
                CB_Repeat_A_A.Checked = Check;
                CB_Repeat_A_B.Checked = Check;
                CB_Repeat_A_C.Checked = Check;
                CB_Repeat_A_D.Checked = Check;
                CB_Repeat_A_E.Checked = Check;
                CB_Repeat_A_F.Checked = Check;
                CB_Repeat_A_G.Checked = Check;
                CB_Repeat_A_H.Checked = Check;
                CB_Repeat_A_I.Checked = Check;
                CB_Repeat_A_J.Checked = Check;
                CB_Repeat_A_K.Checked = Check;
                CB_Repeat_A_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_Repeat_B"))
            {
                CB_Repeat_B_A.Checked = Check;
                CB_Repeat_B_B.Checked = Check;
                CB_Repeat_B_C.Checked = Check;
                CB_Repeat_B_D.Checked = Check;
                CB_Repeat_B_E.Checked = Check;
                CB_Repeat_B_F.Checked = Check;
                CB_Repeat_B_G.Checked = Check;
                CB_Repeat_B_H.Checked = Check;
                CB_Repeat_B_I.Checked = Check;
                CB_Repeat_B_J.Checked = Check;
                CB_Repeat_B_K.Checked = Check;
                CB_Repeat_B_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_Repeat_C"))
            {
                CB_Repeat_C_A.Checked = Check;
                CB_Repeat_C_B.Checked = Check;
                CB_Repeat_C_C.Checked = Check;
                CB_Repeat_C_D.Checked = Check;
                CB_Repeat_C_E.Checked = Check;
                CB_Repeat_C_F.Checked = Check;
                CB_Repeat_C_G.Checked = Check;
                CB_Repeat_C_H.Checked = Check;
                CB_Repeat_C_I.Checked = Check;
                CB_Repeat_C_J.Checked = Check;
                CB_Repeat_C_K.Checked = Check;
                CB_Repeat_C_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_Repeat_D"))
            {
                CB_Repeat_D_A.Checked = Check;
                CB_Repeat_D_B.Checked = Check;
                CB_Repeat_D_C.Checked = Check;
                CB_Repeat_D_D.Checked = Check;
                CB_Repeat_D_E.Checked = Check;
                CB_Repeat_D_F.Checked = Check;
                CB_Repeat_D_G.Checked = Check;
                CB_Repeat_D_H.Checked = Check;
                CB_Repeat_D_I.Checked = Check;
                CB_Repeat_D_J.Checked = Check;
                CB_Repeat_D_K.Checked = Check;
                CB_Repeat_D_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_Repeat_E"))
            {
                CB_Repeat_E_A.Checked = Check;
                CB_Repeat_E_B.Checked = Check;
                CB_Repeat_E_C.Checked = Check;
                CB_Repeat_E_D.Checked = Check;
                CB_Repeat_E_E.Checked = Check;
                CB_Repeat_E_F.Checked = Check;
                CB_Repeat_E_G.Checked = Check;
                CB_Repeat_E_H.Checked = Check;
                CB_Repeat_E_I.Checked = Check;
                CB_Repeat_E_J.Checked = Check;
                CB_Repeat_E_K.Checked = Check;
                CB_Repeat_E_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_Repeat_F"))
            {
                CB_Repeat_F_A.Checked = Check;
                CB_Repeat_F_B.Checked = Check;
                CB_Repeat_F_C.Checked = Check;
                CB_Repeat_F_D.Checked = Check;
                CB_Repeat_F_E.Checked = Check;
                CB_Repeat_F_F.Checked = Check;
                CB_Repeat_F_G.Checked = Check;
                CB_Repeat_F_H.Checked = Check;
                CB_Repeat_F_I.Checked = Check;
                CB_Repeat_F_J.Checked = Check;
                CB_Repeat_F_K.Checked = Check;
                CB_Repeat_F_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_Repeat_G"))
            {
                CB_Repeat_G_A.Checked = Check;
                CB_Repeat_G_B.Checked = Check;
                CB_Repeat_G_C.Checked = Check;
                CB_Repeat_G_D.Checked = Check;
                CB_Repeat_G_E.Checked = Check;
                CB_Repeat_G_F.Checked = Check;
                CB_Repeat_G_G.Checked = Check;
                CB_Repeat_G_H.Checked = Check;
                CB_Repeat_G_I.Checked = Check;
                CB_Repeat_G_J.Checked = Check;
                CB_Repeat_G_K.Checked = Check;
                CB_Repeat_G_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_Repeat_H"))
            {
                CB_Repeat_H_A.Checked = Check;
                CB_Repeat_H_B.Checked = Check;
                CB_Repeat_H_C.Checked = Check;
                CB_Repeat_H_D.Checked = Check;
                CB_Repeat_H_E.Checked = Check;
                CB_Repeat_H_F.Checked = Check;
                CB_Repeat_H_G.Checked = Check;
                CB_Repeat_H_H.Checked = Check;
                CB_Repeat_H_I.Checked = Check;
                CB_Repeat_H_J.Checked = Check;
                CB_Repeat_H_K.Checked = Check;
                CB_Repeat_H_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_Repeat_I"))
            {
                CB_Repeat_I_A.Checked = Check;
                CB_Repeat_I_B.Checked = Check;
                CB_Repeat_I_C.Checked = Check;
                CB_Repeat_I_D.Checked = Check;
                CB_Repeat_I_E.Checked = Check;
                CB_Repeat_I_F.Checked = Check;
                CB_Repeat_I_G.Checked = Check;
                CB_Repeat_I_H.Checked = Check;
                CB_Repeat_I_I.Checked = Check;
                CB_Repeat_I_J.Checked = Check;
                CB_Repeat_I_K.Checked = Check;
                CB_Repeat_I_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_Repeat_J"))
            {
                CB_Repeat_J_A.Checked = Check;
                CB_Repeat_J_B.Checked = Check;
                CB_Repeat_J_C.Checked = Check;
                CB_Repeat_J_D.Checked = Check;
                CB_Repeat_J_E.Checked = Check;
                CB_Repeat_J_F.Checked = Check;
                CB_Repeat_J_G.Checked = Check;
                CB_Repeat_J_H.Checked = Check;
                CB_Repeat_J_I.Checked = Check;
                CB_Repeat_J_J.Checked = Check;
                CB_Repeat_J_K.Checked = Check;
                CB_Repeat_J_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_Repeat_K"))
            {
                CB_Repeat_K_A.Checked = Check;
                CB_Repeat_K_B.Checked = Check;
                CB_Repeat_K_C.Checked = Check;
                CB_Repeat_K_D.Checked = Check;
                CB_Repeat_K_E.Checked = Check;
                CB_Repeat_K_F.Checked = Check;
                CB_Repeat_K_G.Checked = Check;
                CB_Repeat_K_H.Checked = Check;
                CB_Repeat_K_I.Checked = Check;
                CB_Repeat_K_J.Checked = Check;
                CB_Repeat_K_K.Checked = Check;
                CB_Repeat_K_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_Repeat_L"))
            {
                CB_Repeat_L_A.Checked = Check;
                CB_Repeat_L_B.Checked = Check;
                CB_Repeat_L_C.Checked = Check;
                CB_Repeat_L_D.Checked = Check;
                CB_Repeat_L_E.Checked = Check;
                CB_Repeat_L_F.Checked = Check;
                CB_Repeat_L_G.Checked = Check;
                CB_Repeat_L_H.Checked = Check;
                CB_Repeat_L_I.Checked = Check;
                CB_Repeat_L_J.Checked = Check;
                CB_Repeat_L_K.Checked = Check;
                CB_Repeat_L_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_Repeat_M"))
            {
                CB_Repeat_M_A.Checked = Check;
                CB_Repeat_M_B.Checked = Check;
                CB_Repeat_M_C.Checked = Check;
                CB_Repeat_M_D.Checked = Check;
                CB_Repeat_M_E.Checked = Check;
                CB_Repeat_M_F.Checked = Check;
                CB_Repeat_M_G.Checked = Check;
                CB_Repeat_M_H.Checked = Check;
                CB_Repeat_M_I.Checked = Check;
                CB_Repeat_M_J.Checked = Check;
                CB_Repeat_M_K.Checked = Check;
                CB_Repeat_M_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_Repeat_N"))
            {
                CB_Repeat_N_A.Checked = Check;
                CB_Repeat_N_B.Checked = Check;
                CB_Repeat_N_C.Checked = Check;
                CB_Repeat_N_D.Checked = Check;
                CB_Repeat_N_E.Checked = Check;
                CB_Repeat_N_F.Checked = Check;
                CB_Repeat_N_G.Checked = Check;
                CB_Repeat_N_H.Checked = Check;
                CB_Repeat_N_I.Checked = Check;
                CB_Repeat_N_J.Checked = Check;
                CB_Repeat_N_K.Checked = Check;
                CB_Repeat_N_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("_rebalance_A"))
            {
                CB_rebalance_A_A.Checked = Check;
                CB_rebalance_A_B.Checked = Check;
                CB_rebalance_A_C.Checked = Check;
                CB_rebalance_A_D.Checked = Check;
                CB_rebalance_A_E.Checked = Check;
                CB_rebalance_A_F.Checked = Check;
                CB_rebalance_A_G.Checked = Check;
                CB_rebalance_A_H.Checked = Check;
                CB_rebalance_A_I.Checked = Check;
                CB_rebalance_A_J.Checked = Check;
                CB_rebalance_A_K.Checked = Check;
                CB_rebalance_A_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("_rebalance_B"))
            {
                CB_rebalance_B_A.Checked = Check;
                CB_rebalance_B_B.Checked = Check;
                CB_rebalance_B_C.Checked = Check;
                CB_rebalance_B_D.Checked = Check;
                CB_rebalance_B_E.Checked = Check;
                CB_rebalance_B_F.Checked = Check;
                CB_rebalance_B_G.Checked = Check;
                CB_rebalance_B_H.Checked = Check;
                CB_rebalance_B_I.Checked = Check;
                CB_rebalance_B_J.Checked = Check;
                CB_rebalance_B_K.Checked = Check;
                CB_rebalance_B_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("_rebalance_C"))
            {
                CB_rebalance_C_A.Checked = Check;
                CB_rebalance_C_B.Checked = Check;
                CB_rebalance_C_C.Checked = Check;
                CB_rebalance_C_D.Checked = Check;
                CB_rebalance_C_E.Checked = Check;
                CB_rebalance_C_F.Checked = Check;
                CB_rebalance_C_G.Checked = Check;
                CB_rebalance_C_H.Checked = Check;
                CB_rebalance_C_I.Checked = Check;
                CB_rebalance_C_J.Checked = Check;
                CB_rebalance_C_K.Checked = Check;
                CB_rebalance_C_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("_rebalance_D"))
            {
                CB_rebalance_D_A.Checked = Check;
                CB_rebalance_D_B.Checked = Check;
                CB_rebalance_D_C.Checked = Check;
                CB_rebalance_D_D.Checked = Check;
                CB_rebalance_D_E.Checked = Check;
                CB_rebalance_D_F.Checked = Check;
                CB_rebalance_D_G.Checked = Check;
                CB_rebalance_D_H.Checked = Check;
                CB_rebalance_D_I.Checked = Check;
                CB_rebalance_D_J.Checked = Check;
                CB_rebalance_D_K.Checked = Check;
                CB_rebalance_D_L.Checked = Check;
            }


            if ((sender as Button).Name.ToString().Contains("_rebalance_E"))
            {
                CB_rebalance_E_A.Checked = Check;
                CB_rebalance_E_B.Checked = Check;
                CB_rebalance_E_C.Checked = Check;
                CB_rebalance_E_D.Checked = Check;
                CB_rebalance_E_E.Checked = Check;
                CB_rebalance_E_F.Checked = Check;
                CB_rebalance_E_G.Checked = Check;
                CB_rebalance_E_H.Checked = Check;
                CB_rebalance_E_I.Checked = Check;
                CB_rebalance_E_J.Checked = Check;
                CB_rebalance_E_K.Checked = Check;
                CB_rebalance_E_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("_rebalance_F"))
            {
                CB_rebalance_F_A.Checked = Check;
                CB_rebalance_F_B.Checked = Check;
                CB_rebalance_F_C.Checked = Check;
                CB_rebalance_F_D.Checked = Check;
                CB_rebalance_F_E.Checked = Check;
                CB_rebalance_F_F.Checked = Check;
                CB_rebalance_F_G.Checked = Check;
                CB_rebalance_F_H.Checked = Check;
                CB_rebalance_F_I.Checked = Check;
                CB_rebalance_F_J.Checked = Check;
                CB_rebalance_F_K.Checked = Check;
                CB_rebalance_F_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("_rebalance_G"))
            {
                CB_rebalance_G_A.Checked = Check;
                CB_rebalance_G_B.Checked = Check;
                CB_rebalance_G_C.Checked = Check;
                CB_rebalance_G_D.Checked = Check;
                CB_rebalance_G_E.Checked = Check;
                CB_rebalance_G_F.Checked = Check;
                CB_rebalance_G_G.Checked = Check;
                CB_rebalance_G_H.Checked = Check;
                CB_rebalance_G_I.Checked = Check;
                CB_rebalance_G_J.Checked = Check;
                CB_rebalance_G_K.Checked = Check;
                CB_rebalance_G_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("_Liquidation_A"))
            {
                CB_Liquidation_A_A.Checked = Check;
                CB_Liquidation_A_B.Checked = Check;
                CB_Liquidation_A_C.Checked = Check;
                CB_Liquidation_A_D.Checked = Check;
                CB_Liquidation_A_E.Checked = Check;
                CB_Liquidation_A_F.Checked = Check;
                CB_Liquidation_A_G.Checked = Check;
                CB_Liquidation_A_H.Checked = Check;
                CB_Liquidation_A_I.Checked = Check;
                CB_Liquidation_A_J.Checked = Check;
                CB_Liquidation_A_K.Checked = Check;
                CB_Liquidation_A_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("_Liquidation_B"))
            {
                CB_Liquidation_B_A.Checked = Check;
                CB_Liquidation_B_B.Checked = Check;
                CB_Liquidation_B_C.Checked = Check;
                CB_Liquidation_B_D.Checked = Check;
                CB_Liquidation_B_E.Checked = Check;
                CB_Liquidation_B_F.Checked = Check;
                CB_Liquidation_B_G.Checked = Check;
                CB_Liquidation_B_H.Checked = Check;
                CB_Liquidation_B_I.Checked = Check;
                CB_Liquidation_B_J.Checked = Check;
                CB_Liquidation_B_K.Checked = Check;
                CB_Liquidation_B_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("_Liquidation_C"))
            {
                CB_Liquidation_C_A.Checked = Check;
                CB_Liquidation_C_B.Checked = Check;
                CB_Liquidation_C_C.Checked = Check;
                CB_Liquidation_C_D.Checked = Check;
                CB_Liquidation_C_E.Checked = Check;
                CB_Liquidation_C_F.Checked = Check;
                CB_Liquidation_C_G.Checked = Check;
                CB_Liquidation_C_H.Checked = Check;
                CB_Liquidation_C_I.Checked = Check;
                CB_Liquidation_C_J.Checked = Check;
                CB_Liquidation_C_K.Checked = Check;
                CB_Liquidation_C_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("_day_A"))
            {
                CB_day_A_A.Checked = Check;
                CB_day_A_B.Checked = Check;
                CB_day_A_C.Checked = Check;
                CB_day_A_D.Checked = Check;
                CB_day_A_E.Checked = Check;
                CB_day_A_F.Checked = Check;
                CB_day_A_G.Checked = Check;
                CB_day_A_H.Checked = Check;
                CB_day_A_I.Checked = Check;
                CB_day_A_J.Checked = Check;
                CB_day_A_K.Checked = Check;
                CB_day_A_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_day_B"))
            {
                CB_day_B_A.Checked = Check;
                CB_day_B_B.Checked = Check;
                CB_day_B_C.Checked = Check;
                CB_day_B_D.Checked = Check;
                CB_day_B_E.Checked = Check;
                CB_day_B_F.Checked = Check;
                CB_day_B_G.Checked = Check;
                CB_day_B_H.Checked = Check;
                CB_day_B_I.Checked = Check;
                CB_day_B_J.Checked = Check;
                CB_day_B_K.Checked = Check;
                CB_day_B_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_day_C"))
            {
                CB_day_C_A.Checked = Check;
                CB_day_C_B.Checked = Check;
                CB_day_C_C.Checked = Check;
                CB_day_C_D.Checked = Check;
                CB_day_C_E.Checked = Check;
                CB_day_C_F.Checked = Check;
                CB_day_C_G.Checked = Check;
                CB_day_C_H.Checked = Check;
                CB_day_C_I.Checked = Check;
                CB_day_C_J.Checked = Check;
                CB_day_C_K.Checked = Check;
                CB_day_C_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_day_D"))
            {
                CB_day_D_A.Checked = Check;
                CB_day_D_B.Checked = Check;
                CB_day_D_C.Checked = Check;
                CB_day_D_D.Checked = Check;
                CB_day_D_E.Checked = Check;
                CB_day_D_F.Checked = Check;
                CB_day_D_G.Checked = Check;
                CB_day_D_H.Checked = Check;
                CB_day_D_I.Checked = Check;
                CB_day_D_J.Checked = Check;
                CB_day_D_K.Checked = Check;
                CB_day_D_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_day_E"))
            {
                CB_day_E_A.Checked = Check;
                CB_day_E_B.Checked = Check;
                CB_day_E_C.Checked = Check;
                CB_day_E_D.Checked = Check;
                CB_day_E_E.Checked = Check;
                CB_day_E_F.Checked = Check;
                CB_day_E_G.Checked = Check;
                CB_day_E_H.Checked = Check;
                CB_day_E_I.Checked = Check;
                CB_day_E_J.Checked = Check;
                CB_day_E_K.Checked = Check;
                CB_day_E_L.Checked = Check;
            }
            if ((sender as Button).Name.ToString().Contains("_day_F"))
            {
                CB_day_F_A.Checked = Check;
                CB_day_F_B.Checked = Check;
                CB_day_F_C.Checked = Check;
                CB_day_F_D.Checked = Check;
                CB_day_F_E.Checked = Check;
                CB_day_F_F.Checked = Check;
                CB_day_F_G.Checked = Check;
                CB_day_F_H.Checked = Check;
                CB_day_F_I.Checked = Check;
                CB_day_F_J.Checked = Check;
                CB_day_F_K.Checked = Check;
                CB_day_F_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("손익담보_A"))
            {
                CB_Cut_A_A.Checked = Check;
                CB_Cut_A_B.Checked = Check;
                CB_Cut_A_C.Checked = Check;
                CB_Cut_A_D.Checked = Check;
                CB_Cut_A_E.Checked = Check;
                CB_Cut_A_F.Checked = Check;
                CB_Cut_A_G.Checked = Check;
                CB_Cut_A_H.Checked = Check;
                CB_Cut_A_I.Checked = Check;
                CB_Cut_A_J.Checked = Check;
                CB_Cut_A_K.Checked = Check;
                CB_Cut_A_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("손익담보_B"))
            {
                CB_Cut_B_A.Checked = Check;
                CB_Cut_B_B.Checked = Check;
                CB_Cut_B_C.Checked = Check;
                CB_Cut_B_D.Checked = Check;
                CB_Cut_B_E.Checked = Check;
                CB_Cut_B_F.Checked = Check;
                CB_Cut_B_G.Checked = Check;
                CB_Cut_B_H.Checked = Check;
                CB_Cut_B_I.Checked = Check;
                CB_Cut_B_J.Checked = Check;
                CB_Cut_B_K.Checked = Check;
                CB_Cut_B_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("손익담보_C"))
            {
                CB_Cut_C_A.Checked = Check;
                CB_Cut_C_B.Checked = Check;
                CB_Cut_C_C.Checked = Check;
                CB_Cut_C_D.Checked = Check;
                CB_Cut_C_E.Checked = Check;
                CB_Cut_C_F.Checked = Check;
                CB_Cut_C_G.Checked = Check;
                CB_Cut_C_H.Checked = Check;
                CB_Cut_C_I.Checked = Check;
                CB_Cut_C_J.Checked = Check;
                CB_Cut_C_K.Checked = Check;
                CB_Cut_C_L.Checked = Check;
            }

            if ((sender as Button).Name.ToString().Contains("BT_미수금정리"))
            {
                CB_미수금정리_A.Checked = Check;
                CB_미수금정리_B.Checked = Check;
                CB_미수금정리_C.Checked = Check;
                CB_미수금정리_D.Checked = Check;
                CB_미수금정리_E.Checked = Check;
                CB_미수금정리_F.Checked = Check;
                CB_미수금정리_G.Checked = Check;
                CB_미수금정리_H.Checked = Check;
                CB_미수금정리_I.Checked = Check;
                CB_미수금정리_J.Checked = Check;
                CB_미수금정리_K.Checked = Check;
                CB_미수금정리_L.Checked = Check;
            }
        }

        private void BT_매매그룹저장_Click(object sender, EventArgs e)
        {
            Form1.form1.Select();
            Form1.MBC_sender = (sender as Button).Name;
            Form1.중요메세지("매매그룹", "매매그룹 설정 을 저장 하시겠습니까?");
        }


    }
}
