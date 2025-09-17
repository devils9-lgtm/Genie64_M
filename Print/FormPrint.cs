using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace 지니_64
{
    public static class FormPrint
    {
        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////         계좌정보 출력        ///////////////////////

        public static void acc_print() // 계좌정보 출력
        {
            long 손익률기준금 = Properties.Settings.Default.MT_sonik_price;
            long 투자원금 = Properties.Settings.Default.MT_principal; //투자원금 
            long 매입금 = 0;
            long 평가금 = 0;
            long 평가손익금 = 0;
            int 세금 = 0;
            int 수수료 = 0;

            Dictionary<string, Stockbalance> stockBalanceList = Form1.stockBalanceList;   // 잔고 - 보유잔고리스트
            foreach (var 코드 in stockBalanceList.Keys)
            {
                매입금 = 매입금 + stockBalanceList[코드].매입금액;
                평가금 = 평가금 + stockBalanceList[코드].평가금액;
                평가손익금 = 평가손익금 + stockBalanceList[코드].평가손익;
                세금 = 세금 + stockBalanceList[코드].세금;
                수수료 = 수수료 + stockBalanceList[코드].수수료;
            }

            Form1.Acc[0].추정자산 = 매입금 + 평가손익금 + Form1.Acc[0].D2;
            Form1.Acc[0].증가자산 = Form1.Acc[0].추정자산 - 투자원금;
            Form1.Acc[0].매입금 = 매입금;
            Form1.Acc[0].평가금 = 평가금;
            Form1.Acc[0].평가손익 = 평가손익금;
            Form1.Acc[0].평가손익률 = Math.Round((double)평가손익금 / 손익률기준금 * 100, 2);
            Form1.Acc[0].실현손익률 = Math.Round((double)Form1.Acc[0].실현손익 / 손익률기준금 * 100, 2);

            Form1.form1.TB_증가자산.Text = Form1.Acc[0].증가자산.ToString("N0");
            Form1.form1.TB_추정자산.Text = Form1.Acc[0].추정자산.ToString("N0");
            Form1.form1.TB_D2.Text = Form1.Acc[0].D2.ToString("N0");
            Form1.form1.TB_추정D2.Text = Form1.Acc[0].추정D2.ToString("N0");
            Form1.form1.TB_매입금.Text = Form1.Acc[0].매입금.ToString("N0");
            Form1.form1.TB_평가금.Text = Form1.Acc[0].평가금.ToString("N0");
            Form1.form1.TB_평가손익금.Text = Form1.Acc[0].평가손익.ToString("N0");
            Form1.form1.TB_평가손익율.Text = Form1.Acc[0].평가손익률.ToString("N2");
            Form1.form1.TB_실현손익.Text = Form1.Acc[0].실현손익.ToString("N0");
            Form1.form1.TB_실현손익율.Text = Form1.Acc[0].실현손익률.ToString("N2");
            Form1.form1.TB_P_ratio.Text = Form1.Acc[0].피_등락률.ToString("N2");
            Form1.form1.TB_p_down.Text = Form1.Acc[0].피_저가대비.ToString("N2");
            Form1.form1.TB_p_go.Text = Form1.Acc[0].피_고가대비.ToString("N2");
            Form1.form1.TB_q_ratio.Text = Form1.Acc[0].닥_등락률.ToString("N2");
            Form1.form1.TB_q_down.Text = Form1.Acc[0].닥_저가대비.ToString("N2");
            Form1.form1.TB_q_go.Text = Form1.Acc[0].닥_고가대비.ToString("N2");

            Jisu_linkage.지수업종별연동("코스피");
            Jisu_linkage.지수업종별연동("코스닥");

            Form1.Acc[0].매입비 = Method.Acc매입비();

            Form1.form1.TB_계좌매입비_현비중.Text = Math.Round(Form1.Acc[0].매입비, 2).ToString("N2");                                     // 계좌매입비 
            Form1.form1.TB_jango_count.Text = Form1.stockBalanceList.Count.ToString();                                          // 잔고 카운트

            if (Form1.form1.CB_미니시계.Checked)
            {
                Form1.form1.label_date.Text = DateTime.Now.ToLongDateString();
            }

            box.Form_Jisu.form.Jisu_avg_Label_print();




        }


        ///////////////////////         계좌정보 출력        ///////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        public static void CBB_반복매매_매도비중_Selected(object sender)
        {
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_A) || sender.Equals(Form_Repeat.form.CB_repeat_kind_A))
            {
                if (Form_Repeat.form.CB_repeat_kind_A.Checked && Form_Repeat.form.combo_repeat_sell_gubun_A.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_A.SelectedIndex = 2;
                    매도비중알림();
                }
            }
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_B) || sender.Equals(Form_Repeat.form.CB_repeat_kind_B))
            {
                if (Form_Repeat.form.CB_repeat_kind_B.Checked && Form_Repeat.form.combo_repeat_sell_gubun_B.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_B.SelectedIndex = 2;
                    매도비중알림();
                }
            }
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_C) || sender.Equals(Form_Repeat.form.CB_repeat_kind_C))
            {
                if (Form_Repeat.form.CB_repeat_kind_C.Checked && Form_Repeat.form.combo_repeat_sell_gubun_C.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_C.SelectedIndex = 2;
                    매도비중알림();
                }
            }
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_D) || sender.Equals(Form_Repeat.form.CB_repeat_kind_D))
            {
                if (Form_Repeat.form.CB_repeat_kind_D.Checked && Form_Repeat.form.combo_repeat_sell_gubun_D.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_D.SelectedIndex = 2;
                    매도비중알림();
                }
            }
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_E) || sender.Equals(Form_Repeat.form.CB_repeat_kind_E))
            {
                if (Form_Repeat.form.CB_repeat_kind_E.Checked && Form_Repeat.form.combo_repeat_sell_gubun_E.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_E.SelectedIndex = 2;
                    매도비중알림();
                }
            }
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_F) || sender.Equals(Form_Repeat.form.CB_repeat_kind_F))
            {
                if (Form_Repeat.form.CB_repeat_kind_F.Checked && Form_Repeat.form.combo_repeat_sell_gubun_F.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_F.SelectedIndex = 2;
                    매도비중알림();
                }
            }
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_G) || sender.Equals(Form_Repeat.form.CB_repeat_kind_G))
            {
                if (Form_Repeat.form.CB_repeat_kind_G.Checked && Form_Repeat.form.combo_repeat_sell_gubun_G.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_G.SelectedIndex = 2;
                    매도비중알림();
                }
            }
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_H) || sender.Equals(Form_Repeat.form.CB_repeat_kind_H))
            {
                if (Form_Repeat.form.CB_repeat_kind_H.Checked && Form_Repeat.form.combo_repeat_sell_gubun_H.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_H.SelectedIndex = 2;
                    매도비중알림();
                }
            }
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_I) || sender.Equals(Form_Repeat.form.CB_repeat_kind_I))
            {
                if (Form_Repeat.form.CB_repeat_kind_I.Checked && Form_Repeat.form.combo_repeat_sell_gubun_I.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_I.SelectedIndex = 2;
                    매도비중알림();
                }
            }
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_J) || sender.Equals(Form_Repeat.form.CB_repeat_kind_J))
            {
                if (Form_Repeat.form.CB_repeat_kind_J.Checked && Form_Repeat.form.combo_repeat_sell_gubun_J.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_J.SelectedIndex = 2;
                    매도비중알림();
                }
            }
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_K) || sender.Equals(Form_Repeat.form.CB_repeat_kind_K))
            {
                if (Form_Repeat.form.CB_repeat_kind_K.Checked && Form_Repeat.form.combo_repeat_sell_gubun_K.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_K.SelectedIndex = 2;
                    매도비중알림();
                }
            }
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_L) || sender.Equals(Form_Repeat.form.CB_repeat_kind_L))
            {
                if (Form_Repeat.form.CB_repeat_kind_L.Checked && Form_Repeat.form.combo_repeat_sell_gubun_L.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_L.SelectedIndex = 2;
                    매도비중알림();
                }
            }
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_M) || sender.Equals(Form_Repeat.form.CB_repeat_kind_M))
            {
                if (Form_Repeat.form.CB_repeat_kind_M.Checked && Form_Repeat.form.combo_repeat_sell_gubun_M.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_M.SelectedIndex = 2;
                    매도비중알림();
                }
            }
            if (sender.Equals(Form_Repeat.form.combo_repeat_sell_gubun_N) || sender.Equals(Form_Repeat.form.CB_repeat_kind_N))
            {
                if (Form_Repeat.form.CB_repeat_kind_N.Checked && Form_Repeat.form.combo_repeat_sell_gubun_N.SelectedIndex > 3)
                {
                    Form_Repeat.form.combo_repeat_sell_gubun_N.SelectedIndex = 2;
                    매도비중알림();
                }
            }


            void 매도비중알림()
            {
                Form1.AutoClosingAlram("'매수' 는 [ 만원/평균단가, 만원/기준가, 기준금/평균단가, 기준금/기준가 ] 를 사용할수 없습니다. [ 기준금 ] 으로 변경됩니다. 설정을 확인하기 바랍니다.", "비중알림", 10, "에러");
            }
        }


        public static void TextChange_매매방법(object sender)
        {
            Form1.form1.체크박스_비프(sender);

            CheckBox CB_choice = (sender as CheckBox);
            if (CB_choice.Checked)
            {
                CB_choice.Text = "⇒";
                CB_choice.ForeColor = Color.Red;
            }
            else
            {
                CB_choice.Text = "→";
                CB_choice.ForeColor = Color.Blue;
            }

            ComboBox CBB_suik_gubun = null;

            if (Form1.FormRepeat_Open)
            {
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_A)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_A;
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_B)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_B;
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_C)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_C;
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_D)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_D;
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_E)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_E;
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_F)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_F;
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_G)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_G;
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_H)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_H;
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_I)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_I;
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_J)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_J;
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_K)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_K;
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_L)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_L;
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_M)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_M;
                if (sender.Equals(Form_Repeat.form.CB_repeat_choice_N)) CBB_suik_gubun = Form_Repeat.form.combo_repeat_suik_gubun_N;
                메세지알림();
            }

            if (Form1.FormAccountManagement_Open)
            {
                if (sender.Equals(Form_AccountManagement.form.CB_rebalance_choice_A)) CBB_suik_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_A;
                if (sender.Equals(Form_AccountManagement.form.CB_rebalance_choice_B)) CBB_suik_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_B;
                if (sender.Equals(Form_AccountManagement.form.CB_rebalance_choice_C)) CBB_suik_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_C;
                if (sender.Equals(Form_AccountManagement.form.CB_rebalance_choice_D)) CBB_suik_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_D;
                if (sender.Equals(Form_AccountManagement.form.CB_rebalance_choice_E)) CBB_suik_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_E;
                if (sender.Equals(Form_AccountManagement.form.CB_rebalance_choice_F)) CBB_suik_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_F;
                if (sender.Equals(Form_AccountManagement.form.CB_rebalance_choice_G)) CBB_suik_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_G;
                if (sender.Equals(Form_AccountManagement.form.CB_Liquidation_choice_A)) CBB_suik_gubun = Form_AccountManagement.form.CBB_Liquidation_suik_gubun_A;
                if (sender.Equals(Form_AccountManagement.form.CB_Liquidation_choice_B)) CBB_suik_gubun = Form_AccountManagement.form.CBB_Liquidation_suik_gubun_B;
                if (sender.Equals(Form_AccountManagement.form.CB_Liquidation_choice_C)) CBB_suik_gubun = Form_AccountManagement.form.CBB_Liquidation_suik_gubun_C;
                메세지알림();
            }

            if (Form1.FormBasic_Open)
            {
                if (sender.Equals(Form_Basic.form.CB_TimeSell_수익범위_choice_A)) CBB_suik_gubun = Form_Basic.form.CBB_TimeSell_수익구분_A;
                if (sender.Equals(Form_Basic.form.CB_TimeSell_수익범위_choice_B)) CBB_suik_gubun = Form_Basic.form.CBB_TimeSell_수익구분_B;
                if (sender.Equals(Form_Basic.form.CB_TimeSell_수익범위_choice_C)) CBB_suik_gubun = Form_Basic.form.CBB_TimeSell_수익구분_C;
                메세지알림();
            }

            void 메세지알림()
            {
                if (CBB_suik_gubun != null && CB_choice != null)
                    if (CB_choice.Checked && (CBB_suik_gubun.SelectedIndex > 3))
                    {
                        CB_choice.Checked = false;
                        Form1.알림창("[ 설정알림 ]\n\n수익범위 '수익 + 예상손익금, 기준하(기준 + 수익률 이하), 기준상(기준 + 수익률 이상)'\n\n 은 ' → ' 전용옵션 입니다. ' ⇒ ' 는 사용할수 없습니다. ", 10, false);
                    }
            }
        }

        public static void CBB_jumun_SelectedIndex(object sender)
        {
            ComboBox jumun_combo = sender as ComboBox;
            TextBox Ho_text = null;

            if (jumun_combo.Name.Equals("combo_new_jumun_A")) Ho_text = Form_Basic.form.TB_new_value_A;
            if (jumun_combo.Name.Equals("combo_new_jumun_B")) Ho_text = Form_Basic.form.TB_new_value_B;
            if (jumun_combo.Name.Equals("combo_new_jumun_C")) Ho_text = Form_Basic.form.TB_new_value_C;

            if (jumun_combo.Name.Equals("combo_sell_jumun_A")) Ho_text = Form_Basic.form.TB_sell_value_A;
            if (jumun_combo.Name.Equals("combo_sell_jumun_B")) Ho_text = Form_Basic.form.TB_sell_value_B;
            if (jumun_combo.Name.Equals("combo_sell_jumun_C")) Ho_text = Form_Basic.form.TB_sell_value_C;
            if (jumun_combo.Name.Equals("combo_sell_jumun_D")) Ho_text = Form_Basic.form.TB_sell_value_D;
            if (jumun_combo.Name.Equals("combo_sell_jumun_E")) Ho_text = Form_Basic.form.TB_sell_value_E;
            if (jumun_combo.Name.Equals("combo_sell_jumun_F")) Ho_text = Form_Basic.form.TB_sell_value_F;

            if (jumun_combo.Name.Equals("combo_sell_time_jumun")) Ho_text = Form_Basic.form.TB_sell_time_value;
            if (jumun_combo.Name.Equals("combo_silson_jumun_W")) Ho_text = Form_Basic.form.TB_silson_value_W;
            if (jumun_combo.Name.Equals("combo_예상수익_jumun")) Ho_text = Form_Basic.form.TB_예상수익_value;
            if (jumun_combo.Name.Equals("combo_예상손실_jumun")) Ho_text = Form_Basic.form.TB_예상손실_value;

            if (jumun_combo.Name.Equals("CBB_TS_Jumun_1")) Ho_text = Form_Basic.form.TB_TS_Jumun_A;
            if (jumun_combo.Name.Equals("CBB_TS_Jumun_2")) Ho_text = Form_Basic.form.TB_TS_Jumun_B;
            if (jumun_combo.Name.Equals("CBB_TS_Jumun_3")) Ho_text = Form_Basic.form.TB_TS_Jumun_C;
            if (jumun_combo.Name.Equals("CBB_TS_Jumun_4")) Ho_text = Form_Basic.form.TB_TS_Jumun_D;
            if (jumun_combo.Name.Equals("CBB_TS_Jumun_5")) Ho_text = Form_Basic.form.TB_TS_Jumun_E;
            if (jumun_combo.Name.Equals("CBB_TS_Jumun_6")) Ho_text = Form_Basic.form.TB_TS_Jumun_F;
            if (jumun_combo.Name.Equals("CBB_TS_Jumun_7")) Ho_text = Form_Basic.form.TB_TS_Jumun_G;
            if (jumun_combo.Name.Equals("CBB_TS_Jumun_8")) Ho_text = Form_Basic.form.TB_TS_Jumun_H;
            if (jumun_combo.Name.Equals("CBB_TS_Jumun_9")) Ho_text = Form_Basic.form.TB_TS_Jumun_I;

            if (jumun_combo.Name.Equals("combo_ik_down_jumun_A")) Ho_text = Form_Basic.form.TB_ik_down_value_A;
            if (jumun_combo.Name.Equals("combo_ik_down_jumun_B")) Ho_text = Form_Basic.form.TB_ik_down_value_B;
            if (jumun_combo.Name.Equals("combo_ik_down_jumun_C")) Ho_text = Form_Basic.form.TB_ik_down_value_C;
            if (jumun_combo.Name.Equals("combo_ik_down_jumun_D")) Ho_text = Form_Basic.form.TB_ik_down_value_D;
            if (jumun_combo.Name.Equals("combo_ik_down_jumun_E")) Ho_text = Form_Basic.form.TB_ik_down_value_E;
            if (jumun_combo.Name.Equals("combo_ik_down_jumun_F")) Ho_text = Form_Basic.form.TB_ik_down_value_F;
            if (jumun_combo.Name.Equals("combo_ik_down_jumun_G")) Ho_text = Form_Basic.form.TB_ik_down_value_G;
            if (jumun_combo.Name.Equals("combo_ik_down_jumun_H")) Ho_text = Form_Basic.form.TB_ik_down_value_H;
            if (jumun_combo.Name.Equals("combo_ik_down_jumun_I")) Ho_text = Form_Basic.form.TB_ik_down_value_I;

            if (jumun_combo.Name.Equals("combo_ik_jumun_A")) Ho_text = Form_Basic.form.TB_ik_value_A;
            if (jumun_combo.Name.Equals("combo_ik_jumun_B")) Ho_text = Form_Basic.form.TB_ik_value_B;
            if (jumun_combo.Name.Equals("combo_ik_jumun_C")) Ho_text = Form_Basic.form.TB_ik_value_C;
            if (jumun_combo.Name.Equals("combo_ik_jumun_D")) Ho_text = Form_Basic.form.TB_ik_value_D;
            if (jumun_combo.Name.Equals("combo_ik_jumun_E")) Ho_text = Form_Basic.form.TB_ik_value_E;
            if (jumun_combo.Name.Equals("combo_ik_jumun_F")) Ho_text = Form_Basic.form.TB_ik_value_F;
            if (jumun_combo.Name.Equals("combo_ik_jumun_G")) Ho_text = Form_Basic.form.TB_ik_value_G;
            if (jumun_combo.Name.Equals("combo_ik_jumun_H")) Ho_text = Form_Basic.form.TB_ik_value_H;
            if (jumun_combo.Name.Equals("combo_ik_jumun_I")) Ho_text = Form_Basic.form.TB_ik_value_I;

            if (jumun_combo.Name.Equals("CBB_TimeSell_주문가격_A")) Ho_text = Form_Basic.form.TB_TimeSell_주문가격_A;
            if (jumun_combo.Name.Equals("CBB_TimeSell_주문가격_B")) Ho_text = Form_Basic.form.TB_TimeSell_주문가격_B;
            if (jumun_combo.Name.Equals("CBB_TimeSell_주문가격_C")) Ho_text = Form_Basic.form.TB_TimeSell_주문가격_C;

            if (jumun_combo.Name.Equals("CBB_group_In_jumun_A")) Ho_text = Form_Special.form.TB_group_In_value_A;
            if (jumun_combo.Name.Equals("CBB_group_In_jumun_B")) Ho_text = Form_Special.form.TB_group_In_value_B;
            if (jumun_combo.Name.Equals("CBB_group_In_jumun_C")) Ho_text = Form_Special.form.TB_group_In_value_C;
            if (jumun_combo.Name.Equals("CBB_group_In_jumun_D")) Ho_text = Form_Special.form.TB_group_In_value_D;

            if (jumun_combo.Name.Equals("CBB_group_out_jumun_A")) Ho_text = Form_Special.form.TB_group_Out_value_A;
            if (jumun_combo.Name.Equals("CBB_group_out_jumun_B")) Ho_text = Form_Special.form.TB_group_Out_value_B;
            if (jumun_combo.Name.Equals("CBB_group_out_jumun_C")) Ho_text = Form_Special.form.TB_group_Out_value_C;
            if (jumun_combo.Name.Equals("CBB_group_out_jumun_D")) Ho_text = Form_Special.form.TB_group_Out_value_D;

            if (jumun_combo.Name.Equals("CBB_day_Jumun_A")) Ho_text = Form_Special.form.TB_매매기간_value_A;
            if (jumun_combo.Name.Equals("CBB_day_Jumun_B")) Ho_text = Form_Special.form.TB_매매기간_value_B;
            if (jumun_combo.Name.Equals("CBB_day_Jumun_C")) Ho_text = Form_Special.form.TB_매매기간_value_C;
            if (jumun_combo.Name.Equals("CBB_day_Jumun_D")) Ho_text = Form_Special.form.TB_매매기간_value_D;
            if (jumun_combo.Name.Equals("CBB_day_Jumun_E")) Ho_text = Form_Special.form.TB_매매기간_value_E;
            if (jumun_combo.Name.Equals("CBB_day_Jumun_F")) Ho_text = Form_Special.form.TB_매매기간_value_F;

            if (jumun_combo.Name.Equals("combo_repeat_jumun_A")) Ho_text = Form_Repeat.form.TB_repeat_value_A;
            if (jumun_combo.Name.Equals("combo_repeat_jumun_B")) Ho_text = Form_Repeat.form.TB_repeat_value_B;
            if (jumun_combo.Name.Equals("combo_repeat_jumun_C")) Ho_text = Form_Repeat.form.TB_repeat_value_C;
            if (jumun_combo.Name.Equals("combo_repeat_jumun_D")) Ho_text = Form_Repeat.form.TB_repeat_value_D;
            if (jumun_combo.Name.Equals("combo_repeat_jumun_E")) Ho_text = Form_Repeat.form.TB_repeat_value_E;
            if (jumun_combo.Name.Equals("combo_repeat_jumun_F")) Ho_text = Form_Repeat.form.TB_repeat_value_F;
            if (jumun_combo.Name.Equals("combo_repeat_jumun_G")) Ho_text = Form_Repeat.form.TB_repeat_value_G;
            if (jumun_combo.Name.Equals("combo_repeat_jumun_H")) Ho_text = Form_Repeat.form.TB_repeat_value_H;
            if (jumun_combo.Name.Equals("combo_repeat_jumun_I")) Ho_text = Form_Repeat.form.TB_repeat_value_I;
            if (jumun_combo.Name.Equals("combo_repeat_jumun_J")) Ho_text = Form_Repeat.form.TB_repeat_value_J;
            if (jumun_combo.Name.Equals("combo_repeat_jumun_K")) Ho_text = Form_Repeat.form.TB_repeat_value_K;
            if (jumun_combo.Name.Equals("combo_repeat_jumun_L")) Ho_text = Form_Repeat.form.TB_repeat_value_L;
            if (jumun_combo.Name.Equals("combo_repeat_jumun_M")) Ho_text = Form_Repeat.form.TB_repeat_value_M;
            if (jumun_combo.Name.Equals("combo_repeat_jumun_N")) Ho_text = Form_Repeat.form.TB_repeat_value_N;

            if (jumun_combo.Name.Equals("combo_rebalance_jumun_A")) Ho_text = Form_AccountManagement.form.TB_rebalance_value_A;
            if (jumun_combo.Name.Equals("combo_rebalance_jumun_B")) Ho_text = Form_AccountManagement.form.TB_rebalance_value_B;
            if (jumun_combo.Name.Equals("combo_rebalance_jumun_C")) Ho_text = Form_AccountManagement.form.TB_rebalance_value_C;
            if (jumun_combo.Name.Equals("combo_rebalance_jumun_D")) Ho_text = Form_AccountManagement.form.TB_rebalance_value_D;
            if (jumun_combo.Name.Equals("combo_rebalance_jumun_E")) Ho_text = Form_AccountManagement.form.TB_rebalance_value_E;
            if (jumun_combo.Name.Equals("combo_rebalance_jumun_F")) Ho_text = Form_AccountManagement.form.TB_rebalance_value_F;
            if (jumun_combo.Name.Equals("combo_rebalance_jumun_G")) Ho_text = Form_AccountManagement.form.TB_rebalance_value_G;

            if (jumun_combo.Name.Equals("combo_rebalance_감시_jumun_A")) Ho_text = Form_AccountManagement.form.TB_rebalance_감시_value_A;
            if (jumun_combo.Name.Equals("combo_rebalance_감시_jumun_B")) Ho_text = Form_AccountManagement.form.TB_rebalance_감시_value_B;
            if (jumun_combo.Name.Equals("combo_rebalance_감시_jumun_C")) Ho_text = Form_AccountManagement.form.TB_rebalance_감시_value_C;
            if (jumun_combo.Name.Equals("combo_rebalance_감시_jumun_D")) Ho_text = Form_AccountManagement.form.TB_rebalance_감시_value_D;
            if (jumun_combo.Name.Equals("combo_rebalance_감시_jumun_E")) Ho_text = Form_AccountManagement.form.TB_rebalance_감시_value_E;
            if (jumun_combo.Name.Equals("combo_rebalance_감시_jumun_F")) Ho_text = Form_AccountManagement.form.TB_rebalance_감시_value_F;
            if (jumun_combo.Name.Equals("combo_rebalance_감시_jumun_G")) Ho_text = Form_AccountManagement.form.TB_rebalance_감시_value_G;

            if (jumun_combo.Name.Equals("CBB_cut_jumun_A")) Ho_text = Form_AccountManagement.form.TB_cut_value_A;
            if (jumun_combo.Name.Equals("CBB_cut_jumun_B")) Ho_text = Form_AccountManagement.form.TB_cut_value_B;
            if (jumun_combo.Name.Equals("CBB_cut_jumun_C")) Ho_text = Form_AccountManagement.form.TB_cut_value_C;

            if (jumun_combo.Name.Equals("CBB_Liquidation_jumun_A")) Ho_text = Form_AccountManagement.form.TB_Liquidation_value_A;
            if (jumun_combo.Name.Equals("CBB_Liquidation_jumun_B")) Ho_text = Form_AccountManagement.form.TB_Liquidation_value_B;
            if (jumun_combo.Name.Equals("CBB_Liquidation_jumun_C")) Ho_text = Form_AccountManagement.form.TB_Liquidation_value_C;

            if (jumun_combo.Name.Equals("CBB_매매기간_Jumun_A")) Ho_text = Form_Special.form.TB_매매기간_value_A;
            if (jumun_combo.Name.Equals("CBB_매매기간_Jumun_B")) Ho_text = Form_Special.form.TB_매매기간_value_B;
            if (jumun_combo.Name.Equals("CBB_매매기간_Jumun_C")) Ho_text = Form_Special.form.TB_매매기간_value_C;
            if (jumun_combo.Name.Equals("CBB_매매기간_Jumun_D")) Ho_text = Form_Special.form.TB_매매기간_value_D;
            if (jumun_combo.Name.Equals("CBB_매매기간_Jumun_E")) Ho_text = Form_Special.form.TB_매매기간_value_E;
            if (jumun_combo.Name.Equals("CBB_매매기간_Jumun_F")) Ho_text = Form_Special.form.TB_매매기간_value_F;

            if (jumun_combo.Name.Equals("Combo_misu_jumnun")) Ho_text = Form1.form1.TB_misu_value;
            if (jumun_combo.Name.Equals("combo_jango_sell")) Ho_text = Form1.form1.TB_jango_sell_value;

            if (jumun_combo != null && Ho_text != null)
                if (!jumun_combo.Text.Contains("호가") && !jumun_combo.Text.Contains("%"))
                {
                    Ho_text.Text = "0";
                    Ho_text.Enabled = false;
                }
                else
                {
                    Ho_text.Enabled = true;
                }
        }

        public static void combo_cancel_SelectedIndexChanged(object sender)
        {
            ComboBox Combo_gubun = Form_Basic.form.combo_new_cancel_buy_A;
            MaskedTextBox MTextBox = Form_Basic.form.MTB_new_repeat_A;


            if (sender.Equals(Form_Basic.form.combo_new_cancel_buy_A)) { Combo_gubun = Form_Basic.form.combo_new_cancel_buy_A; MTextBox = Form_Basic.form.MTB_new_repeat_A; }
            if (sender.Equals(Form_Basic.form.combo_new_cancel_buy_B)) { Combo_gubun = Form_Basic.form.combo_new_cancel_buy_B; MTextBox = Form_Basic.form.MTB_new_repeat_B; }
            if (sender.Equals(Form_Basic.form.combo_new_cancel_buy_C)) { Combo_gubun = Form_Basic.form.combo_new_cancel_buy_C; MTextBox = Form_Basic.form.MTB_new_repeat_C; }

            if (sender.Equals(Form_Basic.form.combo_ik_cancel_sell)) { Combo_gubun = Form_Basic.form.combo_ik_cancel_sell; MTextBox = Form_Basic.form.MTB_ik_repeat; }
            if (sender.Equals(Form_Basic.form.combo_ik_down_cancel_sell)) { Combo_gubun = Form_Basic.form.combo_ik_down_cancel_sell; MTextBox = Form_Basic.form.MTB_ik_down_repeat; }
            if (sender.Equals(Form_Basic.form.combo_sell_cancel_sell)) { Combo_gubun = Form_Basic.form.combo_sell_cancel_sell; MTextBox = Form_Basic.form.MTB_sell_cancel_repeat; }
            if (sender.Equals(Form_Basic.form.CBB_TS_cancel_sell)) { Combo_gubun = Form_Basic.form.CBB_TS_cancel_sell; MTextBox = Form_Basic.form.MTB_TS_repeat; }

            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_A)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_A; MTextBox = Form_Repeat.form.MTB_repeat_repeat_A; }
            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_B)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_B; MTextBox = Form_Repeat.form.MTB_repeat_repeat_B; }
            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_C)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_C; MTextBox = Form_Repeat.form.MTB_repeat_repeat_C; }
            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_D)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_D; MTextBox = Form_Repeat.form.MTB_repeat_repeat_D; }
            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_E)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_E; MTextBox = Form_Repeat.form.MTB_repeat_repeat_E; }
            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_F)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_F; MTextBox = Form_Repeat.form.MTB_repeat_repeat_F; }
            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_G)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_G; MTextBox = Form_Repeat.form.MTB_repeat_repeat_G; }
            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_H)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_H; MTextBox = Form_Repeat.form.MTB_repeat_repeat_H; }
            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_I)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_I; MTextBox = Form_Repeat.form.MTB_repeat_repeat_I; }
            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_J)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_J; MTextBox = Form_Repeat.form.MTB_repeat_repeat_J; }
            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_K)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_K; MTextBox = Form_Repeat.form.MTB_repeat_repeat_K; }
            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_L)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_L; MTextBox = Form_Repeat.form.MTB_repeat_repeat_L; }
            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_M)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_M; MTextBox = Form_Repeat.form.MTB_repeat_repeat_M; }
            if (sender.Equals(Form_Repeat.form.combo_repeat_Cancel_N)) { Combo_gubun = Form_Repeat.form.combo_repeat_Cancel_N; MTextBox = Form_Repeat.form.MTB_repeat_repeat_N; }

            if (sender.Equals(Form_AccountManagement.form.CBB_Liquidation_Cancel_A)) { Combo_gubun = Form_AccountManagement.form.CBB_Liquidation_Cancel_A; MTextBox = Form_AccountManagement.form.MTB_Liquidation_repeat_A; }
            if (sender.Equals(Form_AccountManagement.form.CBB_Liquidation_Cancel_B)) { Combo_gubun = Form_AccountManagement.form.CBB_Liquidation_Cancel_B; MTextBox = Form_AccountManagement.form.MTB_Liquidation_repeat_B; }
            if (sender.Equals(Form_AccountManagement.form.CBB_Liquidation_Cancel_C)) { Combo_gubun = Form_AccountManagement.form.CBB_Liquidation_Cancel_C; MTextBox = Form_AccountManagement.form.MTB_Liquidation_repeat_C; }

            if (Combo_gubun.SelectedIndex < 3)
            {
                MTextBox.Text = "0";
                MTextBox.Enabled = false;
            }
            else
            {
                MTextBox.Enabled = true;
            }
        }

        public static void CBB_suik_DropDownClosed(object sender)
        {
            ComboBox Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_A;
            CheckBox check_choice = Form_Repeat.form.CB_repeat_choice_A;
            TextBox textBox_suik = Form_Repeat.form.TB_repeat_suik_1_A;

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_A))
            { }

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_B))
            {
                Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_B;
                check_choice = Form_Repeat.form.CB_repeat_choice_B;
                textBox_suik = Form_Repeat.form.TB_repeat_suik_1_B;
            }

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_C))
            {
                Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_C;
                check_choice = Form_Repeat.form.CB_repeat_choice_C;
                textBox_suik = Form_Repeat.form.TB_repeat_suik_1_C;
            }

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_D))
            {
                Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_D;
                check_choice = Form_Repeat.form.CB_repeat_choice_D;
                textBox_suik = Form_Repeat.form.TB_repeat_suik_1_D;
            }

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_E))
            {
                Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_E;
                check_choice = Form_Repeat.form.CB_repeat_choice_E;
                textBox_suik = Form_Repeat.form.TB_repeat_suik_1_E;
            }

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_F))
            {
                Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_F;
                check_choice = Form_Repeat.form.CB_repeat_choice_F;
                textBox_suik = Form_Repeat.form.TB_repeat_suik_1_F;
            }

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_G))
            {
                Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_G;
                check_choice = Form_Repeat.form.CB_repeat_choice_G;
                textBox_suik = Form_Repeat.form.TB_repeat_suik_1_G;
            }

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_H))
            {
                Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_H;
                check_choice = Form_Repeat.form.CB_repeat_choice_H;
                textBox_suik = Form_Repeat.form.TB_repeat_suik_1_H;
            }

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_I))
            {
                Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_I;
                check_choice = Form_Repeat.form.CB_repeat_choice_I;
                textBox_suik = Form_Repeat.form.TB_repeat_suik_1_I;
            }

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_J))
            {
                Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_J;
                check_choice = Form_Repeat.form.CB_repeat_choice_J;
                textBox_suik = Form_Repeat.form.TB_repeat_suik_1_J;
            }

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_K))
            {
                Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_K;
                check_choice = Form_Repeat.form.CB_repeat_choice_K;
                textBox_suik = Form_Repeat.form.TB_repeat_suik_1_K;
            }

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_L))
            {
                Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_L;
                check_choice = Form_Repeat.form.CB_repeat_choice_L;
                textBox_suik = Form_Repeat.form.TB_repeat_suik_1_L;
            }

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_M))
            {
                Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_M;
                check_choice = Form_Repeat.form.CB_repeat_choice_M;
                textBox_suik = Form_Repeat.form.TB_repeat_suik_1_M;
            }

            if (sender.Equals(Form_Repeat.form.combo_repeat_suik_gubun_N))
            {
                Combo_gubun = Form_Repeat.form.combo_repeat_suik_gubun_N;
                check_choice = Form_Repeat.form.CB_repeat_choice_N;
                textBox_suik = Form_Repeat.form.TB_repeat_suik_1_N;
            }

            if (sender.Equals(Form_Basic.form.CBB_TimeSell_수익구분_A))
            {
                Combo_gubun = Form_Basic.form.CBB_TimeSell_수익구분_A;
                check_choice = Form_Basic.form.CB_TimeSell_수익범위_choice_A;
                textBox_suik = Form_Basic.form.TB_TimeSell_수익범위_1_A;
            }

            if (sender.Equals(Form_Basic.form.CBB_TimeSell_수익구분_B))
            {
                Combo_gubun = Form_Basic.form.CBB_TimeSell_수익구분_B;
                check_choice = Form_Basic.form.CB_TimeSell_수익범위_choice_B;
                textBox_suik = Form_Basic.form.TB_TimeSell_수익범위_1_B;
            }

            if (sender.Equals(Form_Basic.form.CBB_TimeSell_수익구분_C))
            {
                Combo_gubun = Form_Basic.form.CBB_TimeSell_수익구분_C;
                check_choice = Form_Basic.form.CB_TimeSell_수익범위_choice_C;
                textBox_suik = Form_Basic.form.TB_TimeSell_수익범위_1_C;
            }

            if (sender.Equals(Form_AccountManagement.form.combo_rebalance_suik_gubun_A))
            {
                Combo_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_A;
                check_choice = Form_AccountManagement.form.CB_rebalance_choice_A;
                textBox_suik = Form_AccountManagement.form.TB_rebalance_suik_1_A;
            }

            if (sender.Equals(Form_AccountManagement.form.combo_rebalance_suik_gubun_B))
            {
                Combo_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_B;
                check_choice = Form_AccountManagement.form.CB_rebalance_choice_B;
                textBox_suik = Form_AccountManagement.form.TB_rebalance_suik_1_B;
            }

            if (sender.Equals(Form_AccountManagement.form.combo_rebalance_suik_gubun_C))
            {
                Combo_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_C;
                check_choice = Form_AccountManagement.form.CB_rebalance_choice_C;
                textBox_suik = Form_AccountManagement.form.TB_rebalance_suik_1_C;
            }

            if (sender.Equals(Form_AccountManagement.form.combo_rebalance_suik_gubun_D))
            {
                Combo_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_D;
                check_choice = Form_AccountManagement.form.CB_rebalance_choice_D;
                textBox_suik = Form_AccountManagement.form.TB_rebalance_suik_1_D;
            }
            if (sender.Equals(Form_AccountManagement.form.combo_rebalance_suik_gubun_E))
            {
                Combo_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_E;
                check_choice = Form_AccountManagement.form.CB_rebalance_choice_E;
                textBox_suik = Form_AccountManagement.form.TB_rebalance_suik_1_E;
            }

            if (sender.Equals(Form_AccountManagement.form.combo_rebalance_suik_gubun_F))
            {
                Combo_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_F;
                check_choice = Form_AccountManagement.form.CB_rebalance_choice_F;
                textBox_suik = Form_AccountManagement.form.TB_rebalance_suik_1_F;
            }

            if (sender.Equals(Form_AccountManagement.form.combo_rebalance_suik_gubun_G))
            {
                Combo_gubun = Form_AccountManagement.form.combo_rebalance_suik_gubun_G;
                check_choice = Form_AccountManagement.form.CB_rebalance_choice_G;
                textBox_suik = Form_AccountManagement.form.TB_rebalance_suik_1_G;
            }

            if (sender.Equals(Form_AccountManagement.form.CBB_Liquidation_suik_gubun_A))
            {
                Combo_gubun = Form_AccountManagement.form.CBB_Liquidation_suik_gubun_A;
                check_choice = Form_AccountManagement.form.CB_Liquidation_choice_A;
                textBox_suik = Form_AccountManagement.form.TB_Liquidation_suik_1_A;
            }

            if (sender.Equals(Form_AccountManagement.form.CBB_Liquidation_suik_gubun_B))
            {
                Combo_gubun = Form_AccountManagement.form.CBB_Liquidation_suik_gubun_B;
                check_choice = Form_AccountManagement.form.CB_Liquidation_choice_B;
                textBox_suik = Form_AccountManagement.form.TB_Liquidation_suik_1_B;
            }
            if (sender.Equals(Form_AccountManagement.form.CBB_Liquidation_suik_gubun_C))
            {
                Combo_gubun = Form_AccountManagement.form.CBB_Liquidation_suik_gubun_C;
                check_choice = Form_AccountManagement.form.CB_Liquidation_choice_C;
                textBox_suik = Form_AccountManagement.form.TB_Liquidation_suik_1_C;
            }
            //평가수익률
            //평가손익금
            //예상손익금
            //주가등락률 3
            //수익률 + 예상 4
            //기준이하(기준 + 수익률 이하)
            //기준이상(기준 + 수익률 이상)

            if (Combo_gubun.SelectedIndex > 3)
            {
                check_choice.Checked = false;
            }

            if (Combo_gubun.SelectedIndex > 4)
            {
                textBox_suik.Text = "0000";
                textBox_suik.Enabled = false;
            }
            else
            {
                if (!Properties.Settings.Default.CB_가이드매매) textBox_suik.Enabled = true;
            }

        }


        public static void 동작상태()
        {
            if (Form1.server_알림.Contains("마켓") || Form1.server_알림.Contains("동시"))
            {
                if (Form1.FormBasic_Open)
                {
                    Form_Basic.form.LB_특정시간반복.Text = Form1.time_Run_time.ToString();
                    Form_Basic.form.LB_실현손익반복.Text = Form1.time_Run_silson_W.ToString();
                    Form_Basic.form.LB_예상손실_반복.Text = Form1.time_Run_예상손실.ToString();
                    Form_Basic.form.LB_예상수익반복.Text = Form1.time_Run_예상수익.ToString();
                    Form_Basic.form.LB_신규매수횟수.Text = Properties.Settings.Default.신규횟수.ToString();

                    Form_Basic.form.LB_잔고개수_신규A.Text = Form1.신규개수A.ToString();
                    Form_Basic.form.LB_잔고개수_신규B.Text = Form1.신규개수B.ToString();
                    Form_Basic.form.LB_잔고개수_신규C.Text = Form1.신규개수C.ToString();
                }

                if (Form1.FormRepeat_Open)
                {
                    Form_Repeat.form.TT_Repeat_A.Text = Form1.Repeat_time_A.ToString();
                    Form_Repeat.form.TT_Repeat_B.Text = Form1.Repeat_time_B.ToString();
                    Form_Repeat.form.TT_Repeat_C.Text = Form1.Repeat_time_C.ToString();
                    Form_Repeat.form.TT_Repeat_D.Text = Form1.Repeat_time_D.ToString();
                    Form_Repeat.form.TT_Repeat_E.Text = Form1.Repeat_time_E.ToString();
                    Form_Repeat.form.TT_Repeat_F.Text = Form1.Repeat_time_F.ToString();
                    Form_Repeat.form.TT_Repeat_G.Text = Form1.Repeat_time_G.ToString();
                    Form_Repeat.form.TT_Repeat_H.Text = Form1.Repeat_time_H.ToString();
                    Form_Repeat.form.TT_Repeat_I.Text = Form1.Repeat_time_I.ToString();
                    Form_Repeat.form.TT_Repeat_J.Text = Form1.Repeat_time_J.ToString();
                    Form_Repeat.form.TT_Repeat_K.Text = Form1.Repeat_time_K.ToString();
                    Form_Repeat.form.TT_Repeat_L.Text = Form1.Repeat_time_L.ToString();
                    Form_Repeat.form.TT_Repeat_M.Text = Form1.Repeat_time_M.ToString();
                    Form_Repeat.form.TT_Repeat_N.Text = Form1.Repeat_time_N.ToString();
                }

                if (Form1.FormAccountManagement_Open)
                {
                    Form_AccountManagement.form.TT_rebalance_A.Text = Form1.TT_rebalance_time_A.ToString();
                    Form_AccountManagement.form.TT_rebalance_B.Text = Form1.TT_rebalance_time_B.ToString();
                    Form_AccountManagement.form.TT_rebalance_C.Text = Form1.TT_rebalance_time_C.ToString();
                    Form_AccountManagement.form.TT_rebalance_D.Text = Form1.TT_rebalance_time_D.ToString();
                    Form_AccountManagement.form.TT_rebalance_E.Text = Form1.TT_rebalance_time_E.ToString();
                    Form_AccountManagement.form.TT_rebalance_F.Text = Form1.TT_rebalance_time_F.ToString();
                    Form_AccountManagement.form.TT_rebalance_G.Text = Form1.TT_rebalance_time_G.ToString();
                    Form_AccountManagement.form.TT_Liquidation_A.Text = Form1.TT_Liqu_time_A.ToString();
                    Form_AccountManagement.form.TT_Liquidation_B.Text = Form1.TT_Liqu_time_B.ToString();
                    Form_AccountManagement.form.TT_Liquidation_C.Text = Form1.TT_Liqu_time_C.ToString();
                }
            }

            if (Form1.form1.동작상태보기 && Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("동작/감시 보기"))
            {
                if (Form1.form1.동작실시간)
                {
                    Form1.form1.LB_JumunList.BeginUpdate();

                    Form1.form1.LB_JumunList.Items.Clear();
                    List<JumunItem> 주문중 = Form1.JumunItem_List.FindAll(o => o.주문번호.Contains("+"));

                    Form1.form1.LB_JumunList.Items.Add("JumunList Count: " + Form1.JumunItem_List.Count + " EA  주문완료: " + (Form1.JumunItem_List.Count - 주문중.Count) + "EA  주문중: " + 주문중.Count + "EA" + " 참고용: " + Form1.Order_list.Count);
                    Form1.form1.LB_JumunList.Items.Add("");

                    List<JumunItem> List_ = Form1.JumunItem_List.OrderBy(n => n.종목명).ToList();

                    for (int i = 0; i < List_.Count; i++)
                    {
                        JumunItem 주문 = List_[i];

                        if (중복확인(주문.종목명))
                        {
                            List<JumunItem> 보기_List = Form1.JumunItem_List.FindAll(o => o.종목코드.Equals(주문.종목코드));

                            for (int m = 0; m < 보기_List.Count; m++)
                            {
                                JumunItem JumunItem = 보기_List[m];

                                Form1.form1.LB_JumunList.Items.Add(JumunItem.종목명 + " T-" + JumunItem.주문시간.ToString("##:##:##") + " Order: " + JumunItem.Order_count + " 현재가: " + JumunItem.현재가 + " [" + GET.주문유형(JumunItem.주문유형) + "]주문: " + JumunItem.주문가격 + " 수: " + JumunItem.주문수량 + " 반복: " + JumunItem.반복횟수 + " 취소: " + JumunItem.취소timer + " 주문번호: " + JumunItem.주문번호 + " 검색식: " + JumunItem.검색식);
                            }
                            Form1.form1.LB_JumunList.Items.Add("");
                        }
                    }

                    Form1.form1.LB_JumunList.EndUpdate();

                    bool 중복확인(string 종목)
                    {
                        bool 확인 = true;

                        for (int n = 0; n < Form1.form1.LB_JumunList.Items.Count; n++)
                        {
                            if (Form1.form1.LB_JumunList.Items[n].ToString().Contains(종목))
                            {
                                확인 = false;
                            }
                        }

                        return 확인;
                    }
                }
            }
        }

        public static void CB_개장일n수능일_Checked(string sender)
        {
            int 시간 = 10000;

            if (sender.Equals("CB_개장일"))
            {
                if (Form1.CB개장일)
                {
                    if (Form1.시장시작 == 90000)
                        Form1.시장시작 = Form1.시장시작 + 시간;

                    Properties.Settings.Default.저장_시작시간 = Properties.Settings.Default.MT_starttime.ToString() + "^" + Properties.Settings.Default.MT_new_start_A.ToString() + "^" + Properties.Settings.Default.MT_new_start_B.ToString() + "^" + Properties.Settings.Default.MT_new_start_C.ToString() + "^" +
                                                               Properties.Settings.Default.MT_sell_time_start.ToString() + "^" + Properties.Settings.Default.MT_silson_start_W.ToString() + "^" + Properties.Settings.Default.MT_예상수익_start.ToString() + "^" + Properties.Settings.Default.MT_예상손실_start.ToString() + "^" +
                                                               Properties.Settings.Default.MT_repeat_time_start_A.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_B.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_C.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_D.ToString() + "^" +
                                                               Properties.Settings.Default.MT_repeat_time_start_E.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_F.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_G.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_H.ToString() + "^" +
                                                               Properties.Settings.Default.MT_repeat_time_start_I.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_J.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_K.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_L.ToString() + "^" +
                                                               Properties.Settings.Default.MT_repeat_time_start_M.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_N.ToString() + "^" + Properties.Settings.Default.MTB_예약주문_장전주문시간.ToString() + "^" +
                                                               Properties.Settings.Default.MT_rebalance_starttime_A.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_B.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_C.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_D.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_E.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_F.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_G.ToString() + "^" +
                                                               Properties.Settings.Default.MTB_Liquidation_Starttime_A.ToString() + "^" + Properties.Settings.Default.MTB_Liquidation_Starttime_B.ToString() + "^" + Properties.Settings.Default.MTB_Liquidation_Starttime_C.ToString() + "^" + Properties.Settings.Default.TB_TimeSell_start_A.ToString() + "^" + Properties.Settings.Default.TB_TimeSell_start_B.ToString() + "^" + Properties.Settings.Default.TB_TimeSell_start_C.ToString() + "^" + Properties.Settings.Default.MTB_rebalance_Selltime_오전;


                    시작시간_체크();
                }
                else
                {

                    if (Properties.Settings.Default.저장_시작시간.Length < 10)
                    {
                        Properties.Settings.Default.저장_시작시간 = Properties.Settings.Default.MT_starttime.ToString() + "^" + Properties.Settings.Default.MT_new_start_A.ToString() + "^" + Properties.Settings.Default.MT_new_start_B.ToString() + "^" + Properties.Settings.Default.MT_new_start_C.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_sell_time_start.ToString() + "^" + Properties.Settings.Default.MT_silson_start_W.ToString() + "^" + Properties.Settings.Default.MT_예상수익_start.ToString() + "^" + Properties.Settings.Default.MT_예상손실_start.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_repeat_time_start_A.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_B.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_C.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_D.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_repeat_time_start_E.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_F.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_G.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_H.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_repeat_time_start_I.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_J.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_K.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_L.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_repeat_time_start_M.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_N.ToString() + "^" + Properties.Settings.Default.MTB_예약주문_장전주문시간.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_rebalance_starttime_A.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_B.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_C.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_D.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_E.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_F.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_G.ToString() + "^" +
                                                                   Properties.Settings.Default.MTB_Liquidation_Starttime_A.ToString() + "^" + Properties.Settings.Default.MTB_Liquidation_Starttime_B.ToString() + "^" + Properties.Settings.Default.MTB_Liquidation_Starttime_C.ToString() + "^" + Properties.Settings.Default.TB_TimeSell_start_A.ToString() + "^" + Properties.Settings.Default.TB_TimeSell_start_B.ToString() + "^" + Properties.Settings.Default.TB_TimeSell_start_C.ToString() + "^" + Properties.Settings.Default.MTB_rebalance_Selltime_오전;
                    }

                    시작시간_해제();
                }
            }

            if (sender.Equals("CB_수능일"))
            {
                if (Form1.CB수능일)
                {
                    if (Form1.시장시작 == 90000)
                        Form1.시장시작 = Form1.시장시작 + 시간;

                    if (Form1.시장종료 == 153000)
                        Form1.시장종료 = Form1.시장종료 + 시간;

                    Properties.Settings.Default.저장_시작시간 = Properties.Settings.Default.MT_starttime.ToString() + "^" + Properties.Settings.Default.MT_new_start_A.ToString() + "^" + Properties.Settings.Default.MT_new_start_B.ToString() + "^" + Properties.Settings.Default.MT_new_start_C.ToString() + "^" +
                                                               Properties.Settings.Default.MT_sell_time_start.ToString() + "^" + Properties.Settings.Default.MT_silson_start_W.ToString() + "^" + Properties.Settings.Default.MT_예상수익_start.ToString() + "^" + Properties.Settings.Default.MT_예상손실_start.ToString() + "^" +
                                                               Properties.Settings.Default.MT_repeat_time_start_A.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_B.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_C.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_D.ToString() + "^" +
                                                               Properties.Settings.Default.MT_repeat_time_start_E.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_F.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_G.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_H.ToString() + "^" +
                                                               Properties.Settings.Default.MT_repeat_time_start_I.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_J.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_K.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_L.ToString() + "^" +
                                                               Properties.Settings.Default.MT_repeat_time_start_M.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_N.ToString() + "^" + Properties.Settings.Default.MTB_예약주문_장전주문시간.ToString() + "^" +
                                                               Properties.Settings.Default.MT_rebalance_starttime_A.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_B.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_C.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_D.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_E.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_F.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_G.ToString() + "^" +
                                                               Properties.Settings.Default.MTB_Liquidation_Starttime_A.ToString() + "^" + Properties.Settings.Default.MTB_Liquidation_Starttime_B.ToString() + "^" + Properties.Settings.Default.MTB_Liquidation_Starttime_C.ToString() + "^" + Properties.Settings.Default.TB_TimeSell_start_A.ToString() + "^" + Properties.Settings.Default.TB_TimeSell_start_B.ToString() + "^" + Properties.Settings.Default.TB_TimeSell_start_C.ToString() + "^" + Properties.Settings.Default.MTB_rebalance_Selltime_오전;

                    Properties.Settings.Default.저장_종료시간 = Properties.Settings.Default.MT_closetime.ToString() + "^" + Properties.Settings.Default.MT_stoptime.ToString() + "^" +
                                                               Properties.Settings.Default.MT_new_end_A.ToString() + "^" + Properties.Settings.Default.MT_new_end_B.ToString() + "^" + Properties.Settings.Default.MT_new_end_C.ToString() + "^" +
                                                               Properties.Settings.Default.MT_sell_time_end.ToString() + "^" + Properties.Settings.Default.MT_silson_end_W.ToString() + "^" + Properties.Settings.Default.MT_예상수익_end.ToString() + "^" + Properties.Settings.Default.MT_예상손실_end.ToString() + "^" +
                                                               Properties.Settings.Default.MT_repeat_time_end_A.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_B.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_C.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_D.ToString() + "^" +
                                                               Properties.Settings.Default.MT_repeat_time_end_E.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_F.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_G.ToString() + "^" +
                                                               Properties.Settings.Default.MT_repeat_time_end_H.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_I.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_J.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_K.ToString() + "^" +
                                                               Properties.Settings.Default.MT_repeat_time_end_L.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_M.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_N.ToString() + "^" +
                                                               Properties.Settings.Default.MT_rebalance_stoptime_A.ToString() + "^" + Properties.Settings.Default.MT_rebalance_stoptime_B.ToString() + "^" + Properties.Settings.Default.MT_rebalance_stoptime_C.ToString() + "^" + Properties.Settings.Default.MT_rebalance_stoptime_D.ToString() + "^" + Properties.Settings.Default.MT_rebalance_stoptime_E.ToString() + "^" + Properties.Settings.Default.MT_rebalance_stoptime_F.ToString() + "^" + Properties.Settings.Default.MT_rebalance_stoptime_G.ToString() + "^" +
                                                               Properties.Settings.Default.MTB_Liquidation_Stoptime_A.ToString() + "^" + Properties.Settings.Default.MTB_Liquidation_Stoptime_B.ToString() + "^" + Properties.Settings.Default.MTB_Liquidation_Stoptime_C.ToString() + "^" + Properties.Settings.Default.MTB_cut_time_A.ToString() + "^" + Properties.Settings.Default.MTB_cut_time_B.ToString() + "^" + Properties.Settings.Default.MTB_cut_time_C.ToString() + "^" +
                                                               Properties.Settings.Default.MTB_예약주문_종가주문시간.ToString() + "^" + Properties.Settings.Default.MT_misu_time.ToString() + "^" + Properties.Settings.Default.MTB_rebalance_Selltime_오후;

                    시작시간_체크();
                    종료시간_체크();
                }
                else
                {

                    if (Properties.Settings.Default.저장_시작시간.Length < 10)
                    {
                        Properties.Settings.Default.저장_시작시간 = Properties.Settings.Default.MT_starttime.ToString() + "^" + Properties.Settings.Default.MT_new_start_A.ToString() + "^" + Properties.Settings.Default.MT_new_start_B.ToString() + "^" + Properties.Settings.Default.MT_new_start_C.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_sell_time_start.ToString() + "^" + Properties.Settings.Default.MT_silson_start_W.ToString() + "^" + Properties.Settings.Default.MT_예상수익_start.ToString() + "^" + Properties.Settings.Default.MT_예상손실_start.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_repeat_time_start_A.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_B.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_C.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_D.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_repeat_time_start_E.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_F.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_G.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_H.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_repeat_time_start_I.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_J.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_K.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_L.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_repeat_time_start_M.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_start_N.ToString() + "^" + Properties.Settings.Default.MTB_예약주문_장전주문시간.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_rebalance_starttime_A.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_B.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_C.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_D.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_E.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_F.ToString() + "^" + Properties.Settings.Default.MT_rebalance_starttime_G.ToString() + "^" +
                                                                   Properties.Settings.Default.MTB_Liquidation_Starttime_A.ToString() + "^" + Properties.Settings.Default.MTB_Liquidation_Starttime_B.ToString() + "^" + Properties.Settings.Default.MTB_Liquidation_Starttime_C.ToString() + "^" + Properties.Settings.Default.TB_TimeSell_start_A.ToString() + "^" + Properties.Settings.Default.TB_TimeSell_start_B.ToString() + "^" + Properties.Settings.Default.TB_TimeSell_start_C.ToString() + "^" + Properties.Settings.Default.MTB_rebalance_Selltime_오전;
                    }

                    if (Properties.Settings.Default.저장_종료시간.Length < 10)
                    {
                        Properties.Settings.Default.저장_종료시간 = Properties.Settings.Default.MT_closetime.ToString() + "^" + Properties.Settings.Default.MT_stoptime.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_new_end_A.ToString() + "^" + Properties.Settings.Default.MT_new_end_B.ToString() + "^" + Properties.Settings.Default.MT_new_end_C.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_sell_time_end.ToString() + "^" + Properties.Settings.Default.MT_silson_end_W.ToString() + "^" + Properties.Settings.Default.MT_예상수익_end.ToString() + "^" + Properties.Settings.Default.MT_예상손실_end.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_repeat_time_end_A.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_B.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_C.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_D.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_repeat_time_end_E.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_F.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_G.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_repeat_time_end_H.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_I.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_J.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_K.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_repeat_time_end_L.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_M.ToString() + "^" + Properties.Settings.Default.MT_repeat_time_end_N.ToString() + "^" +
                                                                   Properties.Settings.Default.MT_rebalance_stoptime_A.ToString() + "^" + Properties.Settings.Default.MT_rebalance_stoptime_B.ToString() + "^" + Properties.Settings.Default.MT_rebalance_stoptime_C.ToString() + "^" + Properties.Settings.Default.MT_rebalance_stoptime_D.ToString() + "^" + Properties.Settings.Default.MT_rebalance_stoptime_E.ToString() + "^" + Properties.Settings.Default.MT_rebalance_stoptime_F.ToString() + "^" + Properties.Settings.Default.MT_rebalance_stoptime_G.ToString() + "^" +
                                                                   Properties.Settings.Default.MTB_Liquidation_Stoptime_A.ToString() + "^" + Properties.Settings.Default.MTB_Liquidation_Stoptime_B.ToString() + "^" + Properties.Settings.Default.MTB_Liquidation_Stoptime_C.ToString() + "^" + Properties.Settings.Default.MTB_cut_time_A.ToString() + "^" + Properties.Settings.Default.MTB_cut_time_B.ToString() + "^" + Properties.Settings.Default.MTB_cut_time_C.ToString() + "^" +
                                                                   Properties.Settings.Default.MTB_예약주문_종가주문시간.ToString() + "^" + Properties.Settings.Default.MT_misu_time.ToString() + "^" + Properties.Settings.Default.MTB_rebalance_Selltime_오후;
                    }

                    시작시간_해제();
                    종료시간_해제();
                }
            }

            void 시작시간_체크()
            {
                if (Properties.Settings.Default.MT_starttime + 시간 < Form1.시장종료) Properties.Settings.Default.MT_starttime = Properties.Settings.Default.MT_starttime + 시간;
                if (Properties.Settings.Default.MT_new_start_A + 시간 < Form1.시장종료) Properties.Settings.Default.MT_new_start_A = Properties.Settings.Default.MT_new_start_A + 시간;
                if (Properties.Settings.Default.MT_new_start_B + 시간 < Form1.시장종료) Properties.Settings.Default.MT_new_start_B = Properties.Settings.Default.MT_new_start_B + 시간;
                if (Properties.Settings.Default.MT_new_start_C + 시간 < Form1.시장종료) Properties.Settings.Default.MT_new_start_C = Properties.Settings.Default.MT_new_start_C + 시간;
                if (Properties.Settings.Default.MT_sell_time_start + 시간 < Form1.시장종료) Properties.Settings.Default.MT_sell_time_start = Properties.Settings.Default.MT_sell_time_start + 시간;
                if (Properties.Settings.Default.MT_silson_start_W + 시간 < Form1.시장종료) Properties.Settings.Default.MT_silson_start_W = Properties.Settings.Default.MT_silson_start_W + 시간;
                if (Properties.Settings.Default.MT_예상수익_start + 시간 < Form1.시장종료) Properties.Settings.Default.MT_예상수익_start = Properties.Settings.Default.MT_예상수익_start + 시간;
                if (Properties.Settings.Default.MT_예상손실_start + 시간 < Form1.시장종료) Properties.Settings.Default.MT_예상손실_start = Properties.Settings.Default.MT_예상손실_start + 시간;

                if (Properties.Settings.Default.MT_repeat_time_start_A + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_A = Properties.Settings.Default.MT_repeat_time_start_A + 시간;
                if (Properties.Settings.Default.MT_repeat_time_start_B + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_B = Properties.Settings.Default.MT_repeat_time_start_B + 시간;
                if (Properties.Settings.Default.MT_repeat_time_start_C + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_C = Properties.Settings.Default.MT_repeat_time_start_C + 시간;
                if (Properties.Settings.Default.MT_repeat_time_start_D + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_D = Properties.Settings.Default.MT_repeat_time_start_D + 시간;
                if (Properties.Settings.Default.MT_repeat_time_start_E + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_E = Properties.Settings.Default.MT_repeat_time_start_E + 시간;
                if (Properties.Settings.Default.MT_repeat_time_start_F + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_F = Properties.Settings.Default.MT_repeat_time_start_F + 시간;
                if (Properties.Settings.Default.MT_repeat_time_start_G + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_G = Properties.Settings.Default.MT_repeat_time_start_G + 시간;
                if (Properties.Settings.Default.MT_repeat_time_start_H + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_H = Properties.Settings.Default.MT_repeat_time_start_H + 시간;
                if (Properties.Settings.Default.MT_repeat_time_start_I + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_I = Properties.Settings.Default.MT_repeat_time_start_I + 시간;
                if (Properties.Settings.Default.MT_repeat_time_start_J + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_J = Properties.Settings.Default.MT_repeat_time_start_J + 시간;
                if (Properties.Settings.Default.MT_repeat_time_start_K + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_K = Properties.Settings.Default.MT_repeat_time_start_K + 시간;
                if (Properties.Settings.Default.MT_repeat_time_start_L + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_L = Properties.Settings.Default.MT_repeat_time_start_L + 시간;
                if (Properties.Settings.Default.MT_repeat_time_start_M + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_M = Properties.Settings.Default.MT_repeat_time_start_M + 시간;
                if (Properties.Settings.Default.MT_repeat_time_start_N + 시간 < Form1.시장종료) Properties.Settings.Default.MT_repeat_time_start_N = Properties.Settings.Default.MT_repeat_time_start_N + 시간;
                if (Properties.Settings.Default.MTB_예약주문_장전주문시간 + 시간 < Form1.시장종료) Properties.Settings.Default.MTB_예약주문_장전주문시간 = Properties.Settings.Default.MTB_예약주문_장전주문시간 + 시간;

                if (Properties.Settings.Default.MT_rebalance_starttime_A + 시간 < Form1.시장종료) Properties.Settings.Default.MT_rebalance_starttime_A = Properties.Settings.Default.MT_rebalance_starttime_A + 시간;
                if (Properties.Settings.Default.MT_rebalance_starttime_B + 시간 < Form1.시장종료) Properties.Settings.Default.MT_rebalance_starttime_B = Properties.Settings.Default.MT_rebalance_starttime_B + 시간;
                if (Properties.Settings.Default.MT_rebalance_starttime_C + 시간 < Form1.시장종료) Properties.Settings.Default.MT_rebalance_starttime_C = Properties.Settings.Default.MT_rebalance_starttime_C + 시간;
                if (Properties.Settings.Default.MT_rebalance_starttime_D + 시간 < Form1.시장종료) Properties.Settings.Default.MT_rebalance_starttime_D = Properties.Settings.Default.MT_rebalance_starttime_D + 시간;
                if (Properties.Settings.Default.MT_rebalance_starttime_E + 시간 < Form1.시장종료) Properties.Settings.Default.MT_rebalance_starttime_E = Properties.Settings.Default.MT_rebalance_starttime_E + 시간;
                if (Properties.Settings.Default.MT_rebalance_starttime_F + 시간 < Form1.시장종료) Properties.Settings.Default.MT_rebalance_starttime_F = Properties.Settings.Default.MT_rebalance_starttime_F + 시간;
                if (Properties.Settings.Default.MT_rebalance_starttime_G + 시간 < Form1.시장종료) Properties.Settings.Default.MT_rebalance_starttime_G = Properties.Settings.Default.MT_rebalance_starttime_G + 시간;

                if (Properties.Settings.Default.MTB_Liquidation_Starttime_A + 시간 < Form1.시장종료) Properties.Settings.Default.MTB_Liquidation_Starttime_A = Properties.Settings.Default.MTB_Liquidation_Starttime_A + 시간;
                if (Properties.Settings.Default.MTB_Liquidation_Starttime_B + 시간 < Form1.시장종료) Properties.Settings.Default.MTB_Liquidation_Starttime_B = Properties.Settings.Default.MTB_Liquidation_Starttime_B + 시간;
                if (Properties.Settings.Default.MTB_Liquidation_Starttime_C + 시간 < Form1.시장종료) Properties.Settings.Default.MTB_Liquidation_Starttime_C = Properties.Settings.Default.MTB_Liquidation_Starttime_C + 시간;

                if (Properties.Settings.Default.CBB_TimeSell_start_A == 0) if (Properties.Settings.Default.TB_TimeSell_start_A + 시간 < Form1.시장종료) Properties.Settings.Default.TB_TimeSell_start_A = Properties.Settings.Default.TB_TimeSell_start_A + 시간;
                if (Properties.Settings.Default.CBB_TimeSell_start_B == 0) if (Properties.Settings.Default.TB_TimeSell_start_B + 시간 < Form1.시장종료) Properties.Settings.Default.TB_TimeSell_start_B = Properties.Settings.Default.TB_TimeSell_start_B + 시간;
                if (Properties.Settings.Default.CBB_TimeSell_start_C == 0) if (Properties.Settings.Default.TB_TimeSell_start_C + 시간 < Form1.시장종료) Properties.Settings.Default.TB_TimeSell_start_C = Properties.Settings.Default.TB_TimeSell_start_C + 시간;

                if (Properties.Settings.Default.MTB_rebalance_Selltime_오전 + 시간 < Form1.시장종료) Properties.Settings.Default.MTB_rebalance_Selltime_오전 = Properties.Settings.Default.MTB_rebalance_Selltime_오전 + 시간;

                Properties.Settings.Default.CB_개장일 = Form1.CB개장일;
                Properties.Settings.Default.CB_수능일 = Form1.CB수능일;

                Console.WriteLine("시작시간체크 시작시간체크 시작시간체크 :: " + Properties.Settings.Default.MT_starttime);
                시작시간그리기();
            }

            void 시작시간_해제()
            {
                if (Form1.시장시작 != 90000)
                    Form1.시장시작 = Form1.시장시작 - 시간;

                string[] 시작시간 = Properties.Settings.Default.저장_시작시간.Split('^');

                Properties.Settings.Default.MT_starttime = int.Parse(시작시간[0]);
                Properties.Settings.Default.MT_new_start_A = int.Parse(시작시간[1]);
                Properties.Settings.Default.MT_new_start_B = int.Parse(시작시간[2]);
                Properties.Settings.Default.MT_new_start_C = int.Parse(시작시간[3]);
                Properties.Settings.Default.MT_sell_time_start = int.Parse(시작시간[4]);
                Properties.Settings.Default.MT_silson_start_W = int.Parse(시작시간[5]);
                Properties.Settings.Default.MT_예상수익_start = int.Parse(시작시간[6]);
                Properties.Settings.Default.MT_예상손실_start = int.Parse(시작시간[7]);

                Properties.Settings.Default.MT_repeat_time_start_A = int.Parse(시작시간[8]);
                Properties.Settings.Default.MT_repeat_time_start_B = int.Parse(시작시간[9]);
                Properties.Settings.Default.MT_repeat_time_start_C = int.Parse(시작시간[10]);
                Properties.Settings.Default.MT_repeat_time_start_D = int.Parse(시작시간[11]);
                Properties.Settings.Default.MT_repeat_time_start_E = int.Parse(시작시간[12]);
                Properties.Settings.Default.MT_repeat_time_start_F = int.Parse(시작시간[13]);
                Properties.Settings.Default.MT_repeat_time_start_G = int.Parse(시작시간[14]);
                Properties.Settings.Default.MT_repeat_time_start_H = int.Parse(시작시간[15]);
                Properties.Settings.Default.MT_repeat_time_start_I = int.Parse(시작시간[16]);
                Properties.Settings.Default.MT_repeat_time_start_J = int.Parse(시작시간[17]);
                Properties.Settings.Default.MT_repeat_time_start_K = int.Parse(시작시간[18]);
                Properties.Settings.Default.MT_repeat_time_start_L = int.Parse(시작시간[19]);
                Properties.Settings.Default.MT_repeat_time_start_M = int.Parse(시작시간[20]);
                Properties.Settings.Default.MT_repeat_time_start_N = int.Parse(시작시간[21]);
                Properties.Settings.Default.MTB_예약주문_장전주문시간 = int.Parse(시작시간[22]);

                Properties.Settings.Default.MT_rebalance_starttime_A = int.Parse(시작시간[23]);
                Properties.Settings.Default.MT_rebalance_starttime_B = int.Parse(시작시간[24]);
                Properties.Settings.Default.MT_rebalance_starttime_C = int.Parse(시작시간[25]);
                Properties.Settings.Default.MT_rebalance_starttime_D = int.Parse(시작시간[26]);
                Properties.Settings.Default.MT_rebalance_starttime_E = int.Parse(시작시간[27]);
                Properties.Settings.Default.MT_rebalance_starttime_F = int.Parse(시작시간[28]);
                Properties.Settings.Default.MT_rebalance_starttime_G = int.Parse(시작시간[29]);

                Properties.Settings.Default.MTB_Liquidation_Starttime_A = int.Parse(시작시간[30]);
                Properties.Settings.Default.MTB_Liquidation_Starttime_B = int.Parse(시작시간[31]);
                Properties.Settings.Default.MTB_Liquidation_Starttime_C = int.Parse(시작시간[32]);

                if (Properties.Settings.Default.CBB_TimeSell_start_A == 0) Properties.Settings.Default.TB_TimeSell_start_A = int.Parse(시작시간[33]);
                if (Properties.Settings.Default.CBB_TimeSell_start_B == 0) Properties.Settings.Default.TB_TimeSell_start_B = int.Parse(시작시간[34]);
                if (Properties.Settings.Default.CBB_TimeSell_start_C == 0) Properties.Settings.Default.TB_TimeSell_start_C = int.Parse(시작시간[35]);

                Properties.Settings.Default.MTB_rebalance_Selltime_오전 = int.Parse(시작시간[36]);

                Properties.Settings.Default.CB_개장일 = Form1.CB개장일;
                Properties.Settings.Default.CB_수능일 = Form1.CB수능일;

                시작시간그리기();
            }

            void 종료시간_체크()
            {
                Properties.Settings.Default.MT_closetime = Properties.Settings.Default.MT_closetime + 시간;
                Properties.Settings.Default.MT_stoptime = Properties.Settings.Default.MT_stoptime + 시간;

                Properties.Settings.Default.MT_new_end_A = Properties.Settings.Default.MT_new_end_A + 시간;
                Properties.Settings.Default.MT_new_end_B = Properties.Settings.Default.MT_new_end_B + 시간;
                Properties.Settings.Default.MT_new_end_C = Properties.Settings.Default.MT_new_end_C + 시간;

                Properties.Settings.Default.MT_sell_time_end = Properties.Settings.Default.MT_sell_time_end + 시간;
                Properties.Settings.Default.MT_silson_end_W = Properties.Settings.Default.MT_silson_end_W + 시간;
                Properties.Settings.Default.MT_예상수익_end = Properties.Settings.Default.MT_예상수익_end + 시간;
                Properties.Settings.Default.MT_예상손실_end = Properties.Settings.Default.MT_예상손실_end + 시간;

                Properties.Settings.Default.MT_repeat_time_end_A = Properties.Settings.Default.MT_repeat_time_end_A + 시간;
                Properties.Settings.Default.MT_repeat_time_end_B = Properties.Settings.Default.MT_repeat_time_end_B + 시간;
                Properties.Settings.Default.MT_repeat_time_end_C = Properties.Settings.Default.MT_repeat_time_end_C + 시간;
                Properties.Settings.Default.MT_repeat_time_end_D = Properties.Settings.Default.MT_repeat_time_end_D + 시간;
                Properties.Settings.Default.MT_repeat_time_end_E = Properties.Settings.Default.MT_repeat_time_end_E + 시간;
                Properties.Settings.Default.MT_repeat_time_end_F = Properties.Settings.Default.MT_repeat_time_end_F + 시간;
                Properties.Settings.Default.MT_repeat_time_end_G = Properties.Settings.Default.MT_repeat_time_end_G + 시간;
                Properties.Settings.Default.MT_repeat_time_end_H = Properties.Settings.Default.MT_repeat_time_end_H + 시간;
                Properties.Settings.Default.MT_repeat_time_end_I = Properties.Settings.Default.MT_repeat_time_end_I + 시간;
                Properties.Settings.Default.MT_repeat_time_end_J = Properties.Settings.Default.MT_repeat_time_end_J + 시간;
                Properties.Settings.Default.MT_repeat_time_end_K = Properties.Settings.Default.MT_repeat_time_end_K + 시간;
                Properties.Settings.Default.MT_repeat_time_end_L = Properties.Settings.Default.MT_repeat_time_end_L + 시간;
                Properties.Settings.Default.MT_repeat_time_end_M = Properties.Settings.Default.MT_repeat_time_end_M + 시간;
                Properties.Settings.Default.MT_repeat_time_end_N = Properties.Settings.Default.MT_repeat_time_end_N + 시간;

                Properties.Settings.Default.MT_rebalance_stoptime_A = Properties.Settings.Default.MT_rebalance_stoptime_A + 시간;
                Properties.Settings.Default.MT_rebalance_stoptime_B = Properties.Settings.Default.MT_rebalance_stoptime_B + 시간;
                Properties.Settings.Default.MT_rebalance_stoptime_C = Properties.Settings.Default.MT_rebalance_stoptime_C + 시간;
                Properties.Settings.Default.MT_rebalance_stoptime_D = Properties.Settings.Default.MT_rebalance_stoptime_D + 시간;
                Properties.Settings.Default.MT_rebalance_stoptime_E = Properties.Settings.Default.MT_rebalance_stoptime_E + 시간;
                Properties.Settings.Default.MT_rebalance_stoptime_F = Properties.Settings.Default.MT_rebalance_stoptime_F + 시간;
                Properties.Settings.Default.MT_rebalance_stoptime_G = Properties.Settings.Default.MT_rebalance_stoptime_G + 시간;

                Properties.Settings.Default.MTB_Liquidation_Stoptime_A = Properties.Settings.Default.MTB_Liquidation_Stoptime_A + 시간;
                Properties.Settings.Default.MTB_Liquidation_Stoptime_B = Properties.Settings.Default.MTB_Liquidation_Stoptime_B + 시간;
                Properties.Settings.Default.MTB_Liquidation_Stoptime_C = Properties.Settings.Default.MTB_Liquidation_Stoptime_C + 시간;

                Properties.Settings.Default.MTB_cut_time_A = Properties.Settings.Default.MTB_cut_time_A + 시간;
                Properties.Settings.Default.MTB_cut_time_B = Properties.Settings.Default.MTB_cut_time_B + 시간;
                Properties.Settings.Default.MTB_cut_time_C = Properties.Settings.Default.MTB_cut_time_C + 시간;

                Properties.Settings.Default.MTB_예약주문_종가주문시간 = Properties.Settings.Default.MTB_예약주문_종가주문시간 + 시간;

                Properties.Settings.Default.MT_misu_time = Properties.Settings.Default.MT_misu_time + 시간;

                Properties.Settings.Default.MTB_rebalance_Selltime_오후 = Properties.Settings.Default.MTB_rebalance_Selltime_오후 + 시간;

                Properties.Settings.Default.CB_수능일 = Form1.CB수능일;

                종료시간그리기();
            }

            void 종료시간_해제()
            {
                if (Form1.시장종료 != 153000)
                    Form1.시장종료 = Form1.시장종료 - 시간;

                string[] 종료시간 = Properties.Settings.Default.저장_종료시간.Split('^');

                Properties.Settings.Default.MT_closetime = int.Parse(종료시간[0]);
                Properties.Settings.Default.MT_stoptime = int.Parse(종료시간[1]);

                Properties.Settings.Default.MT_new_end_A = int.Parse(종료시간[2]);
                Properties.Settings.Default.MT_new_end_B = int.Parse(종료시간[3]);
                Properties.Settings.Default.MT_new_end_C = int.Parse(종료시간[4]);

                Properties.Settings.Default.MT_sell_time_end = int.Parse(종료시간[5]);
                Properties.Settings.Default.MT_silson_end_W = int.Parse(종료시간[6]);
                Properties.Settings.Default.MT_예상수익_end = int.Parse(종료시간[7]);
                Properties.Settings.Default.MT_예상손실_end = int.Parse(종료시간[8]);

                Properties.Settings.Default.MT_repeat_time_end_A = int.Parse(종료시간[9]);
                Properties.Settings.Default.MT_repeat_time_end_B = int.Parse(종료시간[10]);
                Properties.Settings.Default.MT_repeat_time_end_C = int.Parse(종료시간[11]);
                Properties.Settings.Default.MT_repeat_time_end_D = int.Parse(종료시간[12]);
                Properties.Settings.Default.MT_repeat_time_end_E = int.Parse(종료시간[13]);
                Properties.Settings.Default.MT_repeat_time_end_F = int.Parse(종료시간[14]);
                Properties.Settings.Default.MT_repeat_time_end_G = int.Parse(종료시간[15]);
                Properties.Settings.Default.MT_repeat_time_end_H = int.Parse(종료시간[16]);
                Properties.Settings.Default.MT_repeat_time_end_I = int.Parse(종료시간[17]);
                Properties.Settings.Default.MT_repeat_time_end_J = int.Parse(종료시간[18]);
                Properties.Settings.Default.MT_repeat_time_end_K = int.Parse(종료시간[19]);
                Properties.Settings.Default.MT_repeat_time_end_L = int.Parse(종료시간[20]);
                Properties.Settings.Default.MT_repeat_time_end_M = int.Parse(종료시간[21]);
                Properties.Settings.Default.MT_repeat_time_end_N = int.Parse(종료시간[22]);

                Properties.Settings.Default.MT_rebalance_stoptime_A = int.Parse(종료시간[23]);
                Properties.Settings.Default.MT_rebalance_stoptime_B = int.Parse(종료시간[24]);
                Properties.Settings.Default.MT_rebalance_stoptime_C = int.Parse(종료시간[25]);
                Properties.Settings.Default.MT_rebalance_stoptime_D = int.Parse(종료시간[26]);
                Properties.Settings.Default.MT_rebalance_stoptime_E = int.Parse(종료시간[27]);
                Properties.Settings.Default.MT_rebalance_stoptime_F = int.Parse(종료시간[28]);
                Properties.Settings.Default.MT_rebalance_stoptime_G = int.Parse(종료시간[29]);

                Properties.Settings.Default.MTB_Liquidation_Stoptime_A = int.Parse(종료시간[30]);
                Properties.Settings.Default.MTB_Liquidation_Stoptime_B = int.Parse(종료시간[31]);
                Properties.Settings.Default.MTB_Liquidation_Stoptime_C = int.Parse(종료시간[32]);

                Properties.Settings.Default.MTB_cut_time_A = int.Parse(종료시간[33]);
                Properties.Settings.Default.MTB_cut_time_B = int.Parse(종료시간[34]);
                Properties.Settings.Default.MTB_cut_time_C = int.Parse(종료시간[35]);

                Properties.Settings.Default.MTB_예약주문_종가주문시간 = int.Parse(종료시간[36]);

                Properties.Settings.Default.MT_misu_time = int.Parse(종료시간[37]);
                Properties.Settings.Default.MTB_rebalance_Selltime_오후 = int.Parse(종료시간[38]);

                Properties.Settings.Default.CB_수능일 = Form1.CB수능일;

                종료시간그리기();
            }

            void 시작시간그리기()
            {
                Console.WriteLine("시작시간그리기시작시간그리기시작시간그리기시작시간그리기");
                Form1.form1.TB_starttime.Text = Properties.Settings.Default.MT_starttime.ToString();
            }

            void 종료시간그리기()
            {
                Form1.form1.TB_closetime.Text = Properties.Settings.Default.MT_closetime.ToString();
                Form1.form1.TB_stoptime.Text = Properties.Settings.Default.MT_stoptime.ToString();
                Form1.form1.MT_misu_time.Text = Properties.Settings.Default.MT_misu_time.ToString();
            }
        }


        public static void 잔고표시내역()
        {
            if (Properties.Settings.Default.combo_rebalance_suik_gubun_A != 7 || (!Properties.Settings.Default.CB_rebalance_A && Properties.Settings.Default.combo_rebalance_suik_gubun_A == 7)) Properties.Settings.Default.CB_최종매입가_A = false;
            if (Properties.Settings.Default.combo_rebalance_suik_gubun_B != 7 || (!Properties.Settings.Default.CB_rebalance_B && Properties.Settings.Default.combo_rebalance_suik_gubun_B == 7)) Properties.Settings.Default.CB_최종매입가_B = false;
            if (Properties.Settings.Default.combo_rebalance_suik_gubun_C != 7 || (!Properties.Settings.Default.CB_rebalance_C && Properties.Settings.Default.combo_rebalance_suik_gubun_C == 7)) Properties.Settings.Default.CB_최종매입가_C = false;
            if (Properties.Settings.Default.combo_rebalance_suik_gubun_D != 7 || (!Properties.Settings.Default.CB_rebalance_D && Properties.Settings.Default.combo_rebalance_suik_gubun_D == 7)) Properties.Settings.Default.CB_최종매입가_D = false;
            if (Properties.Settings.Default.combo_rebalance_suik_gubun_E != 7 || (!Properties.Settings.Default.CB_rebalance_E && Properties.Settings.Default.combo_rebalance_suik_gubun_E == 7)) Properties.Settings.Default.CB_최종매입가_E = false;
            if (Properties.Settings.Default.combo_rebalance_suik_gubun_F != 7 || (!Properties.Settings.Default.CB_rebalance_F && Properties.Settings.Default.combo_rebalance_suik_gubun_F == 7)) Properties.Settings.Default.CB_최종매입가_F = false;
            if (Properties.Settings.Default.combo_rebalance_suik_gubun_G != 7 || (!Properties.Settings.Default.CB_rebalance_G && Properties.Settings.Default.combo_rebalance_suik_gubun_G == 7)) Properties.Settings.Default.CB_최종매입가_G = false;

            if (!Properties.Settings.Default.CB_ik_one_A && !Properties.Settings.Default.CB_ik_one_B && !Properties.Settings.Default.CB_ik_one_C && !Properties.Settings.Default.CB_ik_one_D && !Properties.Settings.Default.CB_ik_one_E &&
                !Properties.Settings.Default.CB_ik_one_F && !Properties.Settings.Default.CB_ik_one_G && !Properties.Settings.Default.CB_ik_one_H && !Properties.Settings.Default.CB_ik_one_I) Properties.Settings.Default.CB_익회모니터 = false;

            if (!Properties.Settings.Default.CB_ik_A && !Properties.Settings.Default.CB_ik_B && !Properties.Settings.Default.CB_ik_C && !Properties.Settings.Default.CB_ik_D &&
                !Properties.Settings.Default.CB_ik_E && !Properties.Settings.Default.CB_ik_F && !Properties.Settings.Default.CB_ik_G && !Properties.Settings.Default.CB_ik_H && !Properties.Settings.Default.CB_ik_I &&
                !Properties.Settings.Default.CB_TS_A && !Properties.Settings.Default.CB_TS_B && !Properties.Settings.Default.CB_TS_C && !Properties.Settings.Default.CB_TS_D && !Properties.Settings.Default.CB_TS_E &&
                !Properties.Settings.Default.CB_TS_F && !Properties.Settings.Default.CB_TS_G && !Properties.Settings.Default.CB_TS_H && !Properties.Settings.Default.CB_TS_I) Properties.Settings.Default.CB_익절모니터 = false;

            if (!Properties.Settings.Default.CB_ik_down_A && !Properties.Settings.Default.CB_ik_down_B && !Properties.Settings.Default.CB_ik_down_C && !Properties.Settings.Default.CB_ik_down_D &&
                !Properties.Settings.Default.CB_ik_down_E && !Properties.Settings.Default.CB_ik_down_F && !Properties.Settings.Default.CB_ik_down_G && !Properties.Settings.Default.CB_ik_down_H && !Properties.Settings.Default.CB_ik_down_I) Properties.Settings.Default.CB_보전모니터 = false;

            if (!Properties.Settings.Default.CB_sell_use_A && !Properties.Settings.Default.CB_sell_use_B &&
                !Properties.Settings.Default.CB_sell_use_C && !Properties.Settings.Default.CB_sell_use_D && !Properties.Settings.Default.CB_sell_use_E && !Properties.Settings.Default.CB_sell_use_F) Properties.Settings.Default.CB_손절모니터 = false;

            bool 모니터 = Properties.Settings.Default.CB_시간청산범위;

            if ((Properties.Settings.Default.CB_TimeSell_A && Properties.Settings.Default.CB_TimeSell_수익범위_choice_A) ||
                (Properties.Settings.Default.CB_TimeSell_B && Properties.Settings.Default.CB_TimeSell_수익범위_choice_B) ||
                (Properties.Settings.Default.CB_TimeSell_C && Properties.Settings.Default.CB_TimeSell_수익범위_choice_C)) Properties.Settings.Default.CB_시간청산범위 = 모니터;
            else Properties.Settings.Default.CB_시간청산범위 = false;

            if ((!Properties.Settings.Default.CB_TimeSell_A && !Properties.Settings.Default.CB_TimeSell_B && !Properties.Settings.Default.CB_TimeSell_C) ||
                (!Properties.Settings.Default.CB_TimeSell_수익범위_choice_A && !Properties.Settings.Default.CB_TimeSell_수익범위_choice_B && !Properties.Settings.Default.CB_TimeSell_수익범위_choice_C)) Properties.Settings.Default.CB_시간청산범위 = false;

            모니터 = Properties.Settings.Default.CB_잔고청산범위;
            if ((Properties.Settings.Default.CB_Liquidation_A && Properties.Settings.Default.CB_Liquidation_choice_A) ||
                (Properties.Settings.Default.CB_Liquidation_B && Properties.Settings.Default.CB_Liquidation_choice_B) ||
                (Properties.Settings.Default.CB_Liquidation_C && Properties.Settings.Default.CB_Liquidation_choice_C)) Properties.Settings.Default.CB_잔고청산범위 = 모니터;
            else Properties.Settings.Default.CB_잔고청산범위 = false;

            if ((!Properties.Settings.Default.CB_Liquidation_A && !Properties.Settings.Default.CB_Liquidation_B && !Properties.Settings.Default.CB_Liquidation_C) ||
               (!Properties.Settings.Default.CB_Liquidation_choice_A && !Properties.Settings.Default.CB_Liquidation_choice_B && !Properties.Settings.Default.CB_Liquidation_choice_C)) Properties.Settings.Default.CB_잔고청산범위 = false;

            모니터 = Properties.Settings.Default.CB_반복매매범위;
            if ((Properties.Settings.Default.CB_repeat_use_A && Properties.Settings.Default.CB_repeat_choice_A) ||
                (Properties.Settings.Default.CB_repeat_use_B && Properties.Settings.Default.CB_repeat_choice_B) ||
                (Properties.Settings.Default.CB_repeat_use_C && Properties.Settings.Default.CB_repeat_choice_C) ||
                (Properties.Settings.Default.CB_repeat_use_D && Properties.Settings.Default.CB_repeat_choice_D) ||
                (Properties.Settings.Default.CB_repeat_use_E && Properties.Settings.Default.CB_repeat_choice_E) ||
                (Properties.Settings.Default.CB_repeat_use_F && Properties.Settings.Default.CB_repeat_choice_F) ||
                (Properties.Settings.Default.CB_repeat_use_G && Properties.Settings.Default.CB_repeat_choice_G) ||
                (Properties.Settings.Default.CB_repeat_use_H && Properties.Settings.Default.CB_repeat_choice_H) ||
                (Properties.Settings.Default.CB_repeat_use_I && Properties.Settings.Default.CB_repeat_choice_I) ||
                (Properties.Settings.Default.CB_repeat_use_J && Properties.Settings.Default.CB_repeat_choice_J) ||
                (Properties.Settings.Default.CB_repeat_use_K && Properties.Settings.Default.CB_repeat_choice_K) ||
                (Properties.Settings.Default.CB_repeat_use_L && Properties.Settings.Default.CB_repeat_choice_L) ||
                (Properties.Settings.Default.CB_repeat_use_M && Properties.Settings.Default.CB_repeat_choice_M) ||
                (Properties.Settings.Default.CB_repeat_use_N && Properties.Settings.Default.CB_repeat_choice_N)) Properties.Settings.Default.CB_반복매매범위 = 모니터;
            else Properties.Settings.Default.CB_반복매매범위 = false;

            if ((!Properties.Settings.Default.CB_repeat_use_A &&
                 !Properties.Settings.Default.CB_repeat_use_B &&
                 !Properties.Settings.Default.CB_repeat_use_C &&
                 !Properties.Settings.Default.CB_repeat_use_D &&
                 !Properties.Settings.Default.CB_repeat_use_E &&
                 !Properties.Settings.Default.CB_repeat_use_F &&
                 !Properties.Settings.Default.CB_repeat_use_G &&
                 !Properties.Settings.Default.CB_repeat_use_H &&
                 !Properties.Settings.Default.CB_repeat_use_I &&
                 !Properties.Settings.Default.CB_repeat_use_J &&
                 !Properties.Settings.Default.CB_repeat_use_K &&
                 !Properties.Settings.Default.CB_repeat_use_L &&
                 !Properties.Settings.Default.CB_repeat_use_M &&
                 !Properties.Settings.Default.CB_repeat_use_N) ||
                (!Properties.Settings.Default.CB_repeat_choice_A &&
                 !Properties.Settings.Default.CB_repeat_choice_B &&
                 !Properties.Settings.Default.CB_repeat_choice_C &&
                 !Properties.Settings.Default.CB_repeat_choice_D &&
                 !Properties.Settings.Default.CB_repeat_choice_E &&
                 !Properties.Settings.Default.CB_repeat_choice_F &&
                 !Properties.Settings.Default.CB_repeat_choice_G &&
                 !Properties.Settings.Default.CB_repeat_choice_H &&
                 !Properties.Settings.Default.CB_repeat_choice_I &&
                 !Properties.Settings.Default.CB_repeat_choice_J &&
                 !Properties.Settings.Default.CB_repeat_choice_K &&
                 !Properties.Settings.Default.CB_repeat_choice_L &&
                 !Properties.Settings.Default.CB_repeat_choice_M &&
                 !Properties.Settings.Default.CB_repeat_choice_N)) Properties.Settings.Default.CB_반복매매범위 = false;

            모니터 = Properties.Settings.Default.CB_반복매매범위;
            if ((Properties.Settings.Default.CB_rebalance_A && Properties.Settings.Default.CB_rebalance_choice_A) ||
                (Properties.Settings.Default.CB_rebalance_B && Properties.Settings.Default.CB_rebalance_choice_B) ||
                (Properties.Settings.Default.CB_rebalance_C && Properties.Settings.Default.CB_rebalance_choice_C) ||
                (Properties.Settings.Default.CB_rebalance_D && Properties.Settings.Default.CB_rebalance_choice_D) ||
                (Properties.Settings.Default.CB_rebalance_E && Properties.Settings.Default.CB_rebalance_choice_E) ||
                (Properties.Settings.Default.CB_rebalance_F && Properties.Settings.Default.CB_rebalance_choice_F) ||
                (Properties.Settings.Default.CB_rebalance_G && Properties.Settings.Default.CB_rebalance_choice_G)) Properties.Settings.Default.CB_리밸런싱범위 = 모니터;
            else Properties.Settings.Default.CB_리밸런싱범위 = false;

            if ((!Properties.Settings.Default.CB_rebalance_A &&
                 !Properties.Settings.Default.CB_rebalance_B &&
                 !Properties.Settings.Default.CB_rebalance_C &&
                 !Properties.Settings.Default.CB_rebalance_D &&
                 !Properties.Settings.Default.CB_rebalance_E &&
                 !Properties.Settings.Default.CB_rebalance_F &&
                 !Properties.Settings.Default.CB_rebalance_G) ||
               (!Properties.Settings.Default.CB_rebalance_choice_A &&
                !Properties.Settings.Default.CB_rebalance_choice_B &&
                !Properties.Settings.Default.CB_rebalance_choice_C &&
                !Properties.Settings.Default.CB_rebalance_choice_D &&
                !Properties.Settings.Default.CB_rebalance_choice_E &&
                !Properties.Settings.Default.CB_rebalance_choice_F &&
                !Properties.Settings.Default.CB_rebalance_choice_G)) Properties.Settings.Default.CB_리밸런싱범위 = false;

            모니터 = Properties.Settings.Default.CB_잔고청산범위;
            if ((Properties.Settings.Default.CB_Liquidation_A && Properties.Settings.Default.CB_Liquidation_choice_A) ||
                (Properties.Settings.Default.CB_Liquidation_B && Properties.Settings.Default.CB_Liquidation_choice_B) ||
                (Properties.Settings.Default.CB_Liquidation_C && Properties.Settings.Default.CB_Liquidation_choice_C)) Properties.Settings.Default.CB_잔고청산범위 = 모니터;
            else Properties.Settings.Default.CB_잔고청산범위 = false;

            if ((!Properties.Settings.Default.CB_Liquidation_A && !Properties.Settings.Default.CB_Liquidation_B && !Properties.Settings.Default.CB_Liquidation_C) ||
               (!Properties.Settings.Default.CB_Liquidation_choice_A && !Properties.Settings.Default.CB_Liquidation_choice_B && !Properties.Settings.Default.CB_Liquidation_choice_C)) Properties.Settings.Default.CB_잔고청산범위 = false;

            Form_Function.칼람추가();
        }
    }
}
