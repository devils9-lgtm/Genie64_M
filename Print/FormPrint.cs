using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace 지니64
{
    public class FormPrint : Form1
    {
        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////         계좌정보 출력        ///////////////////////

       
        public static void acc_print() // 계좌정보 출력
        {
            long 손익률기준금 = GenieConfig.MT_sonik_price;

            // ==========================================================
            // 💡 [실시간 갱신 복구] TR 제한 방지를 위해 로컬 잔고를 직접 합산합니다.
            // ==========================================================
            long 실시간_매입금 = 0;
            long 실시간_평가금 = 0;
            long 실시간_평가손익 = 0;

       //     Console_print("\n=========== [🔄 실시간 잔고 로컬 합산 시작] ===========");

            foreach (var 잔고 in stockBalanceList.Values)
            {
                // 1. 개별 종목의 '현금 매수분 + 신용 매수분'을 먼저 계산합니다.
                long 종목별_매입금 = 잔고.매입금액 + 잔고.신용_매입금액;
                long 종목별_평가금 = 잔고.평가금액 + 잔고.신용_평가금액;
                long 종목별_평가손익 = 잔고.평가손익;

                // 2. 🖨️ [디버깅 출력] 합산된 개별 종목의 데이터를 콘솔에 예쁘게 찍어줍니다.
        //        Console_print($"  ㄴ [{잔고.종목명}] 매입금: {종목별_매입금:N0}원 | 평가금: {종목별_평가금:N0}원 | 평가손익: {종목별_평가손익:N0}원");

                // 3. 전체 계좌 총합 변수에 누적해서 더해줍니다.
                실시간_매입금 += 종목별_매입금;
                실시간_평가금 += 종목별_평가금;
                실시간_평가손익 += 종목별_평가손익;
            }

         //   Console_print($"[합산 완료] 총 매입금: {실시간_매입금:N0} | 총 평가금: {실시간_평가금:N0} | 총 평가손익: {실시간_평가손익:N0}");
        //    Console_print("==============================================================\n");

            // 합산된 실시간 데이터를 Form1.Acc 전역 변수에 덮어씁니다.
            Form1.Acc.매입금 = 실시간_매입금;
            Form1.Acc.평가금 = 실시간_평가금;
            Form1.Acc.평가손익 = 실시간_평가손익;
            // ==========================================================


            // 1. [수익률 계산] 기준금 대비 수익률 세팅
            if (손익률기준금 > 0)
            {
                Form1.Acc.평가손익률 = Math.Round((double)Form1.Acc.평가손익 / 손익률기준금 * 100, 2);
                Form1.Acc.실현손익률 = Math.Round((double)Form1.Acc.실현손익 / 손익률기준금 * 100, 2);
            }

            // 2. [UI 텍스트박스 업데이트]
            // 🔹 추정/증가자산 (실시간 평가금을 반영하고 싶다면 추정자산도 '예수금 + 실시간_평가금'으로 할 수 있습니다)
            Form1.form1.TB_추정자산.Text = Form1.Acc.추정자산.ToString("N0");
            Form1.form1.TB_증가자산.Text = Form1.Acc.증가자산.ToString("N0");

            // 🔹 D+2
            Form1.form1.TB_D2.Text = Form1.Acc.D2.ToString("N0");
            Form1.form1.TB_추정D2.Text = 신용계산.Get_Estimated_Max_Buying_Power().ToString("N0");

            // 🔹 매입금 / 평가금 (실시간 데이터 출력)
            Form1.form1.TB_매입금.Text = Form1.Acc.매입금.ToString("N0");
            Form1.form1.TB_평가금.Text = Form1.Acc.평가금.ToString("N0");

            // 🔹 손익금 / 손익률 (실시간 데이터 출력)
            Form1.form1.TB_평가손익금.Text = Form1.Acc.평가손익.ToString("N0");
            Form1.form1.TB_평가손익율.Text = Form1.Acc.평가손익률.ToString("N2");

            Form1.form1.TB_실현손익.Text = Form1.Acc.실현손익.ToString("N0");
            Form1.form1.TB_실현손익율.Text = Form1.Acc.실현손익률.ToString("N2");

            // 3. [시장 지수 (피/닥) 업데이트]
            Form1.form1.TB_P_ratio.Text = Form1.Acc.피_등락률.ToString("N2");
            Form1.form1.TB_p_down.Text = Form1.Acc.피_저가대비.ToString("N2");
            Form1.form1.TB_p_go.Text = Form1.Acc.피_고가대비.ToString("N2");

            Form1.form1.TB_q_ratio.Text = Form1.Acc.닥_등락률.ToString("N2");
            Form1.form1.TB_q_down.Text = Form1.Acc.닥_저가대비.ToString("N2");
            Form1.form1.TB_q_go.Text = Form1.Acc.닥_고가대비.ToString("N2");

            // 4. [기타 연동 및 UI 갱신]
            Jisu_linkage.지수업종별연동("코스피");
            Jisu_linkage.지수업종별연동("코스닥");

            Form1.Acc.매입비 = Method.Acc매입비();
            Form1.form1.TB_계좌매입비_현비중.Text = Math.Round(Form1.Acc.매입비, 2).ToString("N2");

            var sortedList = stockBalanceList.Where(pair => pair.Value.매매그룹 != 13).ToList();
            Form1.form1.TB_jango_count.Text = sortedList.Count.ToString();

            if (Form1.form1.CB_미니시계.Checked)
            {
                Form1.form1.label_date.Text = DateTime.Now.ToLongDateString();
            }

            box.Form_Jisu.form.Jisu_avg_Label_print();
            box.Form_Jisu_print.form.Jisu_avg_Label_print();
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

            // 1. 철벽 방어막: 체크박스가 아니면 즉시 컷!
            if (!(sender is CheckBox cb)) return;

            // 2. UI 렌더링 최적화: 삼항 연산자로 목표를 정하고 다를 때만 칠합니다.
            string targetText = cb.Checked ? "⇒" : "→";
            Color targetColor = cb.Checked ? Color.Red : Color.Blue;

            if (cb.Text != targetText) cb.Text = targetText;
            if (cb.ForeColor != targetColor) cb.ForeColor = targetColor;

            // [핵심 최적화 1] 체크가 해제(false)되는 상황이면 아래의 콤보박스 검사 로직을 아예 탈 필요가 없습니다!
            // 여기서 바로 퇴근(return)시켜서 CPU 점유율을 극단적으로 낮춥니다.
            if (!cb.Checked) return;

            ComboBox targetCbb = null;

            // [핵심 최적화 2] 27개의 무거운 Equals 객체 비교를 초고속 문자열 Switch 문으로 대체!
            if (Form1.FormRepeat_Open)
            {
                switch (cb.Name)
                {
                    case "CB_repeat_choice_A": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_A; break;
                    case "CB_repeat_choice_B": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_B; break;
                    case "CB_repeat_choice_C": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_C; break;
                    case "CB_repeat_choice_D": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_D; break;
                    case "CB_repeat_choice_E": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_E; break;
                    case "CB_repeat_choice_F": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_F; break;
                    case "CB_repeat_choice_G": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_G; break;
                    case "CB_repeat_choice_H": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_H; break;
                    case "CB_repeat_choice_I": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_I; break;
                    case "CB_repeat_choice_J": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_J; break;
                    case "CB_repeat_choice_K": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_K; break;
                    case "CB_repeat_choice_L": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_L; break;
                    case "CB_repeat_choice_M": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_M; break;
                    case "CB_repeat_choice_N": targetCbb = Form_Repeat.form.combo_repeat_suik_gubun_N; break;
                }
            }
            // 이미 찾았으면 불필요한 다음 폼 검사를 스킵하도록 최적화
            if (targetCbb == null && Form1.FormAccountManagement_Open)
            {
                switch (cb.Name)
                {
                    case "CB_rebalance_choice_A": targetCbb = Form_AccountManagement.form.combo_rebalance_suik_gubun_A; break;
                    case "CB_rebalance_choice_B": targetCbb = Form_AccountManagement.form.combo_rebalance_suik_gubun_B; break;
                    case "CB_rebalance_choice_C": targetCbb = Form_AccountManagement.form.combo_rebalance_suik_gubun_C; break;
                    case "CB_rebalance_choice_D": targetCbb = Form_AccountManagement.form.combo_rebalance_suik_gubun_D; break;
                    case "CB_rebalance_choice_E": targetCbb = Form_AccountManagement.form.combo_rebalance_suik_gubun_E; break;
                    case "CB_rebalance_choice_F": targetCbb = Form_AccountManagement.form.combo_rebalance_suik_gubun_F; break;
                    case "CB_rebalance_choice_G": targetCbb = Form_AccountManagement.form.combo_rebalance_suik_gubun_G; break;
                    case "CB_Liquidation_choice_A": targetCbb = Form_AccountManagement.form.CBB_Liquidation_suik_gubun_A; break;
                    case "CB_Liquidation_choice_B": targetCbb = Form_AccountManagement.form.CBB_Liquidation_suik_gubun_B; break;
                    case "CB_Liquidation_choice_C": targetCbb = Form_AccountManagement.form.CBB_Liquidation_suik_gubun_C; break;
                }
            }
            if (targetCbb == null && Form1.FormBasic_Open)
            {
                switch (cb.Name)
                {
                    case "CB_TimeSell_수익범위_choice_A": targetCbb = Form_Basic.form.CBB_TimeSell_수익구분_A; break;
                    case "CB_TimeSell_수익범위_choice_B": targetCbb = Form_Basic.form.CBB_TimeSell_수익구분_B; break;
                    case "CB_TimeSell_수익범위_choice_C": targetCbb = Form_Basic.form.CBB_TimeSell_수익구분_C; break;
                }
            }

            // 3. 유효성 검사 및 알림창 (루프 밖에서 딱 1번만 깔끔하게 실행)
            if (targetCbb != null && targetCbb.SelectedIndex > 3)
            {
                cb.Checked = false; // [히든 스킬] 이 코드가 실행되면 이벤트가 다시 호출되지만, 위쪽에 만들어둔 방어막(!cb.Checked) 덕분에 에러 없이 파란색 화살표로 예쁘게 되돌려놓고 즉시 종료됩니다!
                Helper.알림창_멀티("설정알림", "수익범위 '수익 + 예상손익금, 기준하(기준 + 수익률 이하), 기준상(기준 + 수익률 이상)'\n\n 은 ' → ' 전용옵션 입니다. ' ⇒ ' 는 사용할수 없습니다. ", 10, false);
            }
        }

        public static void CBB_jumun_SelectedIndex(object sender)
        {
            // 1. 방어막: 콤보박스가 아니면 즉시 탈출
            if (!(sender is ComboBox jumun_combo)) return;

            TextBox Ho_text = null;

            // 2. [최적화 핵심] 78개의 무거운 if문을 초고속 해시 탐색(switch)으로 변경!
            // (단 1번의 연산으로 목적지를 찾아가며, 폼이 Null일 때 뻗지 않도록 '?. ' 안전 연산자 적용)
            switch (jumun_combo.Name)
            {
                // --- Form_Basic ---
                case "combo_new_jumun_A": Ho_text = Form_Basic.form?.TB_new_value_A; break;
                case "combo_new_jumun_B": Ho_text = Form_Basic.form?.TB_new_value_B; break;
                case "combo_new_jumun_C": Ho_text = Form_Basic.form?.TB_new_value_C; break;

                case "combo_sell_jumun_A": Ho_text = Form_Basic.form?.TB_sell_value_A; break;
                case "combo_sell_jumun_B": Ho_text = Form_Basic.form?.TB_sell_value_B; break;
                case "combo_sell_jumun_C": Ho_text = Form_Basic.form?.TB_sell_value_C; break;
                case "combo_sell_jumun_D": Ho_text = Form_Basic.form?.TB_sell_value_D; break;
                case "combo_sell_jumun_E": Ho_text = Form_Basic.form?.TB_sell_value_E; break;
                case "combo_sell_jumun_F": Ho_text = Form_Basic.form?.TB_sell_value_F; break;

                case "combo_sell_time_jumun": Ho_text = Form_Basic.form?.TB_sell_time_value; break;
                case "combo_silson_jumun_W": Ho_text = Form_Basic.form?.TB_silson_value_W; break;
                case "combo_예상수익_jumun": Ho_text = Form_Basic.form?.TB_예상수익_value; break;
                case "combo_예상손실_jumun": Ho_text = Form_Basic.form?.TB_예상손실_value; break;

                case "CBB_TS_Jumun_1": Ho_text = Form_Basic.form?.TB_TS_Jumun_A; break;
                case "CBB_TS_Jumun_2": Ho_text = Form_Basic.form?.TB_TS_Jumun_B; break;
                case "CBB_TS_Jumun_3": Ho_text = Form_Basic.form?.TB_TS_Jumun_C; break;
                case "CBB_TS_Jumun_4": Ho_text = Form_Basic.form?.TB_TS_Jumun_D; break;
                case "CBB_TS_Jumun_5": Ho_text = Form_Basic.form?.TB_TS_Jumun_E; break;
                case "CBB_TS_Jumun_6": Ho_text = Form_Basic.form?.TB_TS_Jumun_F; break;
                case "CBB_TS_Jumun_7": Ho_text = Form_Basic.form?.TB_TS_Jumun_G; break;
                case "CBB_TS_Jumun_8": Ho_text = Form_Basic.form?.TB_TS_Jumun_H; break;
                case "CBB_TS_Jumun_9": Ho_text = Form_Basic.form?.TB_TS_Jumun_I; break;

                case "combo_ik_down_jumun_A": Ho_text = Form_Basic.form?.TB_ik_down_value_A; break;
                case "combo_ik_down_jumun_B": Ho_text = Form_Basic.form?.TB_ik_down_value_B; break;
                case "combo_ik_down_jumun_C": Ho_text = Form_Basic.form?.TB_ik_down_value_C; break;
                case "combo_ik_down_jumun_D": Ho_text = Form_Basic.form?.TB_ik_down_value_D; break;
                case "combo_ik_down_jumun_E": Ho_text = Form_Basic.form?.TB_ik_down_value_E; break;
                case "combo_ik_down_jumun_F": Ho_text = Form_Basic.form?.TB_ik_down_value_F; break;
                case "combo_ik_down_jumun_G": Ho_text = Form_Basic.form?.TB_ik_down_value_G; break;
                case "combo_ik_down_jumun_H": Ho_text = Form_Basic.form?.TB_ik_down_value_H; break;
                case "combo_ik_down_jumun_I": Ho_text = Form_Basic.form?.TB_ik_down_value_I; break;

                case "combo_ik_jumun_A": Ho_text = Form_Basic.form?.TB_ik_value_A; break;
                case "combo_ik_jumun_B": Ho_text = Form_Basic.form?.TB_ik_value_B; break;
                case "combo_ik_jumun_C": Ho_text = Form_Basic.form?.TB_ik_value_C; break;
                case "combo_ik_jumun_D": Ho_text = Form_Basic.form?.TB_ik_value_D; break;
                case "combo_ik_jumun_E": Ho_text = Form_Basic.form?.TB_ik_value_E; break;
                case "combo_ik_jumun_F": Ho_text = Form_Basic.form?.TB_ik_value_F; break;
                case "combo_ik_jumun_G": Ho_text = Form_Basic.form?.TB_ik_value_G; break;
                case "combo_ik_jumun_H": Ho_text = Form_Basic.form?.TB_ik_value_H; break;
                case "combo_ik_jumun_I": Ho_text = Form_Basic.form?.TB_ik_value_I; break;

                case "CBB_TimeSell_주문가격_A": Ho_text = Form_Basic.form?.TB_TimeSell_주문가격_A; break;
                case "CBB_TimeSell_주문가격_B": Ho_text = Form_Basic.form?.TB_TimeSell_주문가격_B; break;
                case "CBB_TimeSell_주문가격_C": Ho_text = Form_Basic.form?.TB_TimeSell_주문가격_C; break;

                // --- Form_Special ---
                case "CBB_group_In_jumun_A": Ho_text = Form_Special.form?.TB_group_In_value_A; break;
                case "CBB_group_In_jumun_B": Ho_text = Form_Special.form?.TB_group_In_value_B; break;
                case "CBB_group_In_jumun_C": Ho_text = Form_Special.form?.TB_group_In_value_C; break;
                case "CBB_group_In_jumun_D": Ho_text = Form_Special.form?.TB_group_In_value_D; break;

                case "CBB_group_out_jumun_A": Ho_text = Form_Special.form?.TB_group_Out_value_A; break;
                case "CBB_group_out_jumun_B": Ho_text = Form_Special.form?.TB_group_Out_value_B; break;
                case "CBB_group_out_jumun_C": Ho_text = Form_Special.form?.TB_group_Out_value_C; break;
                case "CBB_group_out_jumun_D": Ho_text = Form_Special.form?.TB_group_Out_value_D; break;

                case "CBB_day_Jumun_A": case "CBB_매매기간_Jumun_A": Ho_text = Form_Special.form?.TB_매매기간_value_A; break;
                case "CBB_day_Jumun_B": case "CBB_매매기간_Jumun_B": Ho_text = Form_Special.form?.TB_매매기간_value_B; break;
                case "CBB_day_Jumun_C": case "CBB_매매기간_Jumun_C": Ho_text = Form_Special.form?.TB_매매기간_value_C; break;
                case "CBB_day_Jumun_D": case "CBB_매매기간_Jumun_D": Ho_text = Form_Special.form?.TB_매매기간_value_D; break;
                case "CBB_day_Jumun_E": case "CBB_매매기간_Jumun_E": Ho_text = Form_Special.form?.TB_매매기간_value_E; break;
                case "CBB_day_Jumun_F": case "CBB_매매기간_Jumun_F": Ho_text = Form_Special.form?.TB_매매기간_value_F; break;

                // --- Form_Repeat ---
                case "combo_repeat_jumun_A": Ho_text = Form_Repeat.form?.TB_repeat_value_A; break;
                case "combo_repeat_jumun_B": Ho_text = Form_Repeat.form?.TB_repeat_value_B; break;
                case "combo_repeat_jumun_C": Ho_text = Form_Repeat.form?.TB_repeat_value_C; break;
                case "combo_repeat_jumun_D": Ho_text = Form_Repeat.form?.TB_repeat_value_D; break;
                case "combo_repeat_jumun_E": Ho_text = Form_Repeat.form?.TB_repeat_value_E; break;
                case "combo_repeat_jumun_F": Ho_text = Form_Repeat.form?.TB_repeat_value_F; break;
                case "combo_repeat_jumun_G": Ho_text = Form_Repeat.form?.TB_repeat_value_G; break;
                case "combo_repeat_jumun_H": Ho_text = Form_Repeat.form?.TB_repeat_value_H; break;
                case "combo_repeat_jumun_I": Ho_text = Form_Repeat.form?.TB_repeat_value_I; break;
                case "combo_repeat_jumun_J": Ho_text = Form_Repeat.form?.TB_repeat_value_J; break;
                case "combo_repeat_jumun_K": Ho_text = Form_Repeat.form?.TB_repeat_value_K; break;
                case "combo_repeat_jumun_L": Ho_text = Form_Repeat.form?.TB_repeat_value_L; break;
                case "combo_repeat_jumun_M": Ho_text = Form_Repeat.form?.TB_repeat_value_M; break;
                case "combo_repeat_jumun_N": Ho_text = Form_Repeat.form?.TB_repeat_value_N; break;

                // --- Form_AccountManagement ---
                case "combo_rebalance_jumun_A": Ho_text = Form_AccountManagement.form?.TB_rebalance_value_A; break;
                case "combo_rebalance_jumun_B": Ho_text = Form_AccountManagement.form?.TB_rebalance_value_B; break;
                case "combo_rebalance_jumun_C": Ho_text = Form_AccountManagement.form?.TB_rebalance_value_C; break;
                case "combo_rebalance_jumun_D": Ho_text = Form_AccountManagement.form?.TB_rebalance_value_D; break;
                case "combo_rebalance_jumun_E": Ho_text = Form_AccountManagement.form?.TB_rebalance_value_E; break;
                case "combo_rebalance_jumun_F": Ho_text = Form_AccountManagement.form?.TB_rebalance_value_F; break;
                case "combo_rebalance_jumun_G": Ho_text = Form_AccountManagement.form?.TB_rebalance_value_G; break;

                case "combo_rebalance_감시_jumun_A": Ho_text = Form_AccountManagement.form?.TB_rebalance_감시_value_A; break;
                case "combo_rebalance_감시_jumun_B": Ho_text = Form_AccountManagement.form?.TB_rebalance_감시_value_B; break;
                case "combo_rebalance_감시_jumun_C": Ho_text = Form_AccountManagement.form?.TB_rebalance_감시_value_C; break;
                case "combo_rebalance_감시_jumun_D": Ho_text = Form_AccountManagement.form?.TB_rebalance_감시_value_D; break;
                case "combo_rebalance_감시_jumun_E": Ho_text = Form_AccountManagement.form?.TB_rebalance_감시_value_E; break;
                case "combo_rebalance_감시_jumun_F": Ho_text = Form_AccountManagement.form?.TB_rebalance_감시_value_F; break;
                case "combo_rebalance_감시_jumun_G": Ho_text = Form_AccountManagement.form?.TB_rebalance_감시_value_G; break;

                case "CBB_cut_jumun_A": Ho_text = Form_AccountManagement.form?.TB_cut_value_A; break;
                case "CBB_cut_jumun_B": Ho_text = Form_AccountManagement.form?.TB_cut_value_B; break;
                case "CBB_cut_jumun_C": Ho_text = Form_AccountManagement.form?.TB_cut_value_C; break;

                case "CBB_Liquidation_jumun_A": Ho_text = Form_AccountManagement.form?.TB_Liquidation_value_A; break;
                case "CBB_Liquidation_jumun_B": Ho_text = Form_AccountManagement.form?.TB_Liquidation_value_B; break;
                case "CBB_Liquidation_jumun_C": Ho_text = Form_AccountManagement.form?.TB_Liquidation_value_C; break;

                // --- Form1 ---
                case "Combo_misu_jumnun": Ho_text = Form1.form1?.TB_misu_value; break;
                case "combo_jango_sell": Ho_text = Form1.form1?.TB_jango_sell_value; break;
            }

            // 3. 목적지(TextBox)를 못 찾았으면 안전하게 탈출
            if (Ho_text == null) return;

            // 4. [최적화 핵심] 활성화 여부를 먼저 판단하고, 값이 다를 때만 UI를 변경합니다.
            bool isEnabled = jumun_combo.Text.Contains("호가") || jumun_combo.Text.Contains("%");

            if (!isEnabled)
            {
                if (Ho_text.Text != "0") Ho_text.Text = "0";
                if (Ho_text.Enabled) Ho_text.Enabled = false;
            }
            else
            {
                if (!Ho_text.Enabled) Ho_text.Enabled = true;
            }
        }


        public static void Combo_cancel_SelectedIndexChanged(object sender)
        {
            // 1. 철벽 방어막: 보낸 객체가 ComboBox가 아니면 즉시 컷! (프로그램 뻗음 방지)
            if (!(sender is ComboBox Combo_gubun)) return;

            MaskedTextBox MTextBox = null;

            // 2. [최적화 핵심 1] Combo_gubun은 이미 위에서 찾았습니다!
            // 이제 무거운 Equals 대신, 이름(Name)을 Switch문으로 검색해 짝꿍인 MTextBox만 0.001초 만에 찾아옵니다.
            switch (Combo_gubun.Name)
            {
                // --- Form_Basic ---
                case "combo_new_cancel_buy_A": MTextBox = Form_Basic.form?.MTB_new_repeat_A; break;
                case "combo_new_cancel_buy_B": MTextBox = Form_Basic.form?.MTB_new_repeat_B; break;
                case "combo_new_cancel_buy_C": MTextBox = Form_Basic.form?.MTB_new_repeat_C; break;

                case "combo_ik_cancel_sell": MTextBox = Form_Basic.form?.MTB_ik_repeat; break;
                case "combo_ik_down_cancel_sell": MTextBox = Form_Basic.form?.MTB_ik_down_repeat; break;
                case "combo_sell_cancel_sell": MTextBox = Form_Basic.form?.MTB_sell_cancel_repeat; break;
                case "CBB_TS_cancel_sell": MTextBox = Form_Basic.form?.MTB_TS_repeat; break;

                // --- Form_Repeat ---
                case "combo_repeat_Cancel_A": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_A; break;
                case "combo_repeat_Cancel_B": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_B; break;
                case "combo_repeat_Cancel_C": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_C; break;
                case "combo_repeat_Cancel_D": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_D; break;
                case "combo_repeat_Cancel_E": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_E; break;
                case "combo_repeat_Cancel_F": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_F; break;
                case "combo_repeat_Cancel_G": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_G; break;
                case "combo_repeat_Cancel_H": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_H; break;
                case "combo_repeat_Cancel_I": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_I; break;
                case "combo_repeat_Cancel_J": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_J; break;
                case "combo_repeat_Cancel_K": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_K; break;
                case "combo_repeat_Cancel_L": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_L; break;
                case "combo_repeat_Cancel_M": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_M; break;
                case "combo_repeat_Cancel_N": MTextBox = Form_Repeat.form?.MTB_repeat_repeat_N; break;

                // --- Form_AccountManagement ---
                case "CBB_Liquidation_Cancel_A": MTextBox = Form_AccountManagement.form?.MTB_Liquidation_repeat_A; break;
                case "CBB_Liquidation_Cancel_B": MTextBox = Form_AccountManagement.form?.MTB_Liquidation_repeat_B; break;
                case "CBB_Liquidation_Cancel_C": MTextBox = Form_AccountManagement.form?.MTB_Liquidation_repeat_C; break;
            }

            // 3. 짝꿍(MaskedTextBox)을 못 찾았거나 폼이 안 열려있으면 안전하게 탈출
            if (MTextBox == null) return;

            // 4. [최적화 핵심 2] UI 깜빡임 방지: 값이 "진짜 다를 때만" 변경합니다.
            if (Combo_gubun.SelectedIndex < 3)
            {
                if (MTextBox.Text != "0") MTextBox.Text = "0";
                if (MTextBox.Enabled) MTextBox.Enabled = false;
            }
            else
            {
                if (!MTextBox.Enabled) MTextBox.Enabled = true;
            }
        }


        public static void CBB_suik_DropDownClosed(object sender)
        {
            // 1. 방어막 및 본인 확인: 콤보박스가 아니면 즉시 탈출!
            // (Combo_gubun은 더 이상 찾을 필요 없이 sender 그 자체입니다)
            if (!(sender is ComboBox Combo_gubun)) return;

            CheckBox check_choice = null;
            TextBox textBox_suik = null;

            // 2. [최적화 핵심 1] 30개의 무거운 if문을 초고속 해시 탐색(switch)으로 변경!
            // 이름(Name)을 기반으로 짝꿍 2명만 0.001초 만에 바로 찾아옵니다.
            switch (Combo_gubun.Name)
            {
                // --- Form_Repeat ---
                case "combo_repeat_suik_gubun_A": check_choice = Form_Repeat.form?.CB_repeat_choice_A; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_A; break;
                case "combo_repeat_suik_gubun_B": check_choice = Form_Repeat.form?.CB_repeat_choice_B; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_B; break;
                case "combo_repeat_suik_gubun_C": check_choice = Form_Repeat.form?.CB_repeat_choice_C; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_C; break;
                case "combo_repeat_suik_gubun_D": check_choice = Form_Repeat.form?.CB_repeat_choice_D; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_D; break;
                case "combo_repeat_suik_gubun_E": check_choice = Form_Repeat.form?.CB_repeat_choice_E; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_E; break;
                case "combo_repeat_suik_gubun_F": check_choice = Form_Repeat.form?.CB_repeat_choice_F; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_F; break;
                case "combo_repeat_suik_gubun_G": check_choice = Form_Repeat.form?.CB_repeat_choice_G; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_G; break;
                case "combo_repeat_suik_gubun_H": check_choice = Form_Repeat.form?.CB_repeat_choice_H; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_H; break;
                case "combo_repeat_suik_gubun_I": check_choice = Form_Repeat.form?.CB_repeat_choice_I; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_I; break;
                case "combo_repeat_suik_gubun_J": check_choice = Form_Repeat.form?.CB_repeat_choice_J; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_J; break;
                case "combo_repeat_suik_gubun_K": check_choice = Form_Repeat.form?.CB_repeat_choice_K; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_K; break;
                case "combo_repeat_suik_gubun_L": check_choice = Form_Repeat.form?.CB_repeat_choice_L; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_L; break;
                case "combo_repeat_suik_gubun_M": check_choice = Form_Repeat.form?.CB_repeat_choice_M; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_M; break;
                case "combo_repeat_suik_gubun_N": check_choice = Form_Repeat.form?.CB_repeat_choice_N; textBox_suik = Form_Repeat.form?.TB_repeat_suik_1_N; break;

                // --- Form_Basic ---
                case "CBB_TimeSell_수익구분_A": check_choice = Form_Basic.form?.CB_TimeSell_수익범위_choice_A; textBox_suik = Form_Basic.form?.TB_TimeSell_수익범위_1_A; break;
                case "CBB_TimeSell_수익구분_B": check_choice = Form_Basic.form?.CB_TimeSell_수익범위_choice_B; textBox_suik = Form_Basic.form?.TB_TimeSell_수익범위_1_B; break;
                case "CBB_TimeSell_수익구분_C": check_choice = Form_Basic.form?.CB_TimeSell_수익범위_choice_C; textBox_suik = Form_Basic.form?.TB_TimeSell_수익범위_1_C; break;

                // --- Form_AccountManagement ---
                case "combo_rebalance_suik_gubun_A": check_choice = Form_AccountManagement.form?.CB_rebalance_choice_A; textBox_suik = Form_AccountManagement.form?.TB_rebalance_suik_1_A; break;
                case "combo_rebalance_suik_gubun_B": check_choice = Form_AccountManagement.form?.CB_rebalance_choice_B; textBox_suik = Form_AccountManagement.form?.TB_rebalance_suik_1_B; break;
                case "combo_rebalance_suik_gubun_C": check_choice = Form_AccountManagement.form?.CB_rebalance_choice_C; textBox_suik = Form_AccountManagement.form?.TB_rebalance_suik_1_C; break;
                case "combo_rebalance_suik_gubun_D": check_choice = Form_AccountManagement.form?.CB_rebalance_choice_D; textBox_suik = Form_AccountManagement.form?.TB_rebalance_suik_1_D; break;
                case "combo_rebalance_suik_gubun_E": check_choice = Form_AccountManagement.form?.CB_rebalance_choice_E; textBox_suik = Form_AccountManagement.form?.TB_rebalance_suik_1_E; break;
                case "combo_rebalance_suik_gubun_F": check_choice = Form_AccountManagement.form?.CB_rebalance_choice_F; textBox_suik = Form_AccountManagement.form?.TB_rebalance_suik_1_F; break;
                case "combo_rebalance_suik_gubun_G": check_choice = Form_AccountManagement.form?.CB_rebalance_choice_G; textBox_suik = Form_AccountManagement.form?.TB_rebalance_suik_1_G; break;

                case "CBB_Liquidation_suik_gubun_A": check_choice = Form_AccountManagement.form?.CB_Liquidation_choice_A; textBox_suik = Form_AccountManagement.form?.TB_Liquidation_suik_1_A; break;
                case "CBB_Liquidation_suik_gubun_B": check_choice = Form_AccountManagement.form?.CB_Liquidation_choice_B; textBox_suik = Form_AccountManagement.form?.TB_Liquidation_suik_1_B; break;
                case "CBB_Liquidation_suik_gubun_C": check_choice = Form_AccountManagement.form?.CB_Liquidation_choice_C; textBox_suik = Form_AccountManagement.form?.TB_Liquidation_suik_1_C; break;
            }

            // 3. 짝꿍 컨트롤들을 못 찾았거나 화면이 안 열려있으면 안전하게 탈출
            if (check_choice == null || textBox_suik == null) return;

            // 4. [최적화 핵심 2] UI 깜빡임 방지: 값이 "진짜 다를 때만" 변경합니다.
            if (Combo_gubun.SelectedIndex > 3)
            {
                // 이미 체크 해제되어 있다면 무시
                if (check_choice.Checked) check_choice.Checked = false;
            }

            if (Combo_gubun.SelectedIndex > 4)
            {
                if (textBox_suik.Text != "0000") textBox_suik.Text = "0000";
                if (textBox_suik.Enabled) textBox_suik.Enabled = false;
            }
            else
            {
                // 가이드매매가 아니고 현재 비활성화 상태일 때만 활성화시킴
                if (!GenieConfig.CB_가이드매매 && !textBox_suik.Enabled)
                {
                    textBox_suik.Enabled = true;
                }
            }
        }


        public static void 동작상태()
        {
            Form_Basic.form.LB_신규매수횟수.Text = GenieConfig.신규횟수.ToString();
            Form_Basic.form.LB_잔고개수_신규A.Text = Get.신규개수A.ToString();
            Form_Basic.form.LB_잔고개수_신규B.Text = Get.신규개수B.ToString();
            Form_Basic.form.LB_잔고개수_신규C.Text = Get.신규개수C.ToString();

            if (Form1.server_알림.Contains("마켓") || Form1.server_알림.Contains("동시"))
            {
                if (Form1.FormBasic_Open)
                {
                    Form_Basic.form.LB_특정시간반복.Text = Get.time_Run_time.ToString();
                    Form_Basic.form.LB_실현손익반복.Text = Get.time_Run_silson_W.ToString();
                    Form_Basic.form.LB_예상손실_반복.Text = Get.time_Run_예상손실.ToString();
                    Form_Basic.form.LB_예상수익반복.Text = Get.time_Run_예상수익.ToString();
                }

                if (Form1.FormRepeat_Open)
                {
                    Form_Repeat.form.TT_Repeat_A.Text = Get.Repeat_time_A.ToString();
                    Form_Repeat.form.TT_Repeat_B.Text = Get.Repeat_time_B.ToString();
                    Form_Repeat.form.TT_Repeat_C.Text = Get.Repeat_time_C.ToString();
                    Form_Repeat.form.TT_Repeat_D.Text = Get.Repeat_time_D.ToString();
                    Form_Repeat.form.TT_Repeat_E.Text = Get.Repeat_time_E.ToString();
                    Form_Repeat.form.TT_Repeat_F.Text = Get.Repeat_time_F.ToString();
                    Form_Repeat.form.TT_Repeat_G.Text = Get.Repeat_time_G.ToString();
                    Form_Repeat.form.TT_Repeat_H.Text = Get.Repeat_time_H.ToString();
                    Form_Repeat.form.TT_Repeat_I.Text = Get.Repeat_time_I.ToString();
                    Form_Repeat.form.TT_Repeat_J.Text = Get.Repeat_time_J.ToString();
                    Form_Repeat.form.TT_Repeat_K.Text = Get.Repeat_time_K.ToString();
                    Form_Repeat.form.TT_Repeat_L.Text = Get.Repeat_time_L.ToString();
                    Form_Repeat.form.TT_Repeat_M.Text = Get.Repeat_time_M.ToString();
                    Form_Repeat.form.TT_Repeat_N.Text = Get.Repeat_time_N.ToString();
                }

                if (Form1.FormAccountManagement_Open)
                {
                    Form_AccountManagement.form.TT_rebalance_A.Text = Get.TT_rebalance_time_A.ToString();
                    Form_AccountManagement.form.TT_rebalance_B.Text = Get.TT_rebalance_time_B.ToString();
                    Form_AccountManagement.form.TT_rebalance_C.Text = Get.TT_rebalance_time_C.ToString();
                    Form_AccountManagement.form.TT_rebalance_D.Text = Get.TT_rebalance_time_D.ToString();
                    Form_AccountManagement.form.TT_rebalance_E.Text = Get.TT_rebalance_time_E.ToString();
                    Form_AccountManagement.form.TT_rebalance_F.Text = Get.TT_rebalance_time_F.ToString();
                    Form_AccountManagement.form.TT_rebalance_G.Text = Get.TT_rebalance_time_G.ToString();
                    Form_AccountManagement.form.TT_Liquidation_A.Text = Get.TT_Liqu_time_A.ToString();
                    Form_AccountManagement.form.TT_Liquidation_B.Text = Get.TT_Liqu_time_B.ToString();
                    Form_AccountManagement.form.TT_Liquidation_C.Text = Get.TT_Liqu_time_C.ToString();
                }
            }

            if (Form1.form1.동작상태보기 && Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("동작/감시 보기"))
            {
                // =================================================================
                // 1. [백그라운드 연산 구역] - 데이터 수집, 정렬, 문자열 조립 몽땅 미리 해둠
                // =================================================================
                if (!form1.동작실시간) return;

                // 데이터 복사 (충돌 방지)
                List<JumunItem> allItemsSnapshot = Form1.JumunItem_List.Values.ToList();

                // 통계 계산
                int totalCount = allItemsSnapshot.Count;
                int 주문중_Count = allItemsSnapshot.Count(o => o.주문번호.Contains("+"));
                int 주문완료_Count = totalCount - 주문중_Count;

                // 상단 타이틀 문자열 미리 조립
                string topInfo = $"JumunList Count: {totalCount} EA 주문완료: {주문완료_Count}EA 주문중: {주문중_Count}EA 참고용: {order_scheduler.QueueCount}";

                // 그룹화 및 정렬
                var groupedList = allItemsSnapshot
                    .GroupBy(o => o.종목코드)
                    .OrderBy(g => g.First().종목명)
                    .ToList(); // 그룹화된 결과도 메모리에 고정

                // [핵심 최적화] ListBox에 넣을 '문자열'들을 UI 밖에서 미리 다 만들어 둔다!
                List<string> displayItems = new List<string>();
                displayItems.Add(topInfo);
                displayItems.Add("");

                foreach (var group in groupedList)
                {
                    foreach (JumunItem jumun in group)
                    {
                        // 여기서 문자열 조립을 다 끝내서 UI 부하를 0으로 만듦
                        string timeStr = jumun.주문시간.ToString("00':'00':'00");
                        string line = $"{jumun.종목명} T-{timeStr} 현재가: {jumun.현재가} [{GET.매수매도str(jumun.매수매도)}]주문: {jumun.주문가격} 수: {jumun.주문수량} 반복: {jumun.반복횟수} 취소: {jumun.취소timer} 주문번호: {jumun.주문번호} 검색식: {jumun.검색식}";

                        displayItems.Add(line);
                    }
                    displayItems.Add(""); // 종목 간 빈 줄
                }

                // =================================================================
                // 2. [UI 업데이트 구역] - 완성된 텍스트 뭉치를 화면에 뿌리기만 함 (초경량)
                // =================================================================
                Helper.안전한_UI_업데이트(form1, () =>
                {
                    // 폼이 닫혀있으면 무시
                    if (form1 == null || form1.IsDisposed || !form1.IsHandleCreated) return;

                    form1.LB_JumunList.BeginUpdate();
                    form1.LB_JumunList.Items.Clear();

                    try
                    {
                        // 미리 만들어둔 문자열 리스트를 한 방에 쏙 집어넣음 (루프 돌며 Add 하는 것보다 AddRange가 훨씬 빠름!)
                        form1.LB_JumunList.Items.AddRange(displayItems.ToArray());
                    }
                    catch (Exception ex)
                    {
                        Form1.Console_print($"[동작/감시 보기 UI 에러] {ex.Message}");
                    }
                    finally
                    {
                        form1.LB_JumunList.EndUpdate();
                    }
                });
            }
        }


        public static void CB_개장일n수능일_Checked(string sender)
        {
            int 추가시간 = 10000;

            // [수능일 처리] (시작 시간 +1시간 AND 종료 시간 +1시간)
            if (sender.Equals("CB_수능일"))
            {
                if (Form1.CB수능일)
                {
                    if (Get.메인마켓시작 == 90000) Get.메인마켓시작 += 추가시간;
                    if (Get.메인마켓종료 == 153000) Get.메인마켓종료 += 추가시간;

                    // 1. 현재 설정 백업
                    GenieConfig.저장_시작시간 = Backup_StartTimes();
                    GenieConfig.저장_종료시간 = Backup_EndTimes();

                    // 2. 시간 추가
                    Adjust_StartTimes(추가시간);
                    Adjust_EndTimes(추가시간);
                }
                else
                {
                    // 3. (안전장치) 백업값이 없으면 현재값 저장
                    if (GenieConfig.저장_시작시간.Length < 10)
                        GenieConfig.저장_시작시간 = Backup_StartTimes();

                    if (GenieConfig.저장_종료시간.Length < 10)
                        GenieConfig.저장_종료시간 = Backup_EndTimes();

                    // 4. 원래 시간으로 복원
                    Restore_StartTimes(GenieConfig.저장_시작시간);
                    Restore_EndTimes(GenieConfig.저장_종료시간);

                    if (Get.시장시작 != 90000) Get.시장시작 -= 추가시간;
                    if (Get.시장종료 != 153000) Get.시장종료 -= 추가시간;

                    시작시간그리기();
                    종료시간그리기();
                }

                GenieConfig.CB_개장일 = Form1.CB개장일;
                GenieConfig.CB_수능일 = Form1.CB수능일;
            }
        }

        // 1. 시작 시간 백업 문자열 생성
        private static string Backup_StartTimes()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(GenieConfig.MT_starttime).Append("^");

            sb.Append(GenieConfig.MT_new_start_A).Append("^");
            sb.Append(GenieConfig.MT_new_start_B).Append("^");
            sb.Append(GenieConfig.MT_new_start_C).Append("^");
            sb.Append(GenieConfig.MT_sell_time_start).Append("^");
            sb.Append(GenieConfig.MT_silson_start_W).Append("^");
            sb.Append(GenieConfig.MT_예상수익_start).Append("^");
            sb.Append(GenieConfig.MT_예상손실_start).Append("^");

            sb.Append(GenieConfig.MT_repeat_time_start_A).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_start_B).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_start_C).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_start_D).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_start_E).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_start_F).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_start_G).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_start_H).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_start_I).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_start_J).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_start_K).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_start_L).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_start_M).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_start_N).Append("^");

            sb.Append(GenieConfig.MTB_예약주문_장전주문시간).Append("^");

            sb.Append(GenieConfig.MT_rebalance_starttime_A).Append("^");
            sb.Append(GenieConfig.MT_rebalance_starttime_B).Append("^");
            sb.Append(GenieConfig.MT_rebalance_starttime_C).Append("^");
            sb.Append(GenieConfig.MT_rebalance_starttime_D).Append("^");
            sb.Append(GenieConfig.MT_rebalance_starttime_E).Append("^");
            sb.Append(GenieConfig.MT_rebalance_starttime_F).Append("^");
            sb.Append(GenieConfig.MT_rebalance_starttime_G).Append("^");

            sb.Append(GenieConfig.MTB_Liquidation_Starttime_A).Append("^");
            sb.Append(GenieConfig.MTB_Liquidation_Starttime_B).Append("^");
            sb.Append(GenieConfig.MTB_Liquidation_Starttime_C).Append("^");

            sb.Append(GenieConfig.TB_TimeSell_start_A).Append("^");
            sb.Append(GenieConfig.TB_TimeSell_start_B).Append("^");
            sb.Append(GenieConfig.TB_TimeSell_start_C).Append("^");

            sb.Append(GenieConfig.MTB_rebalance_Selltime_오전);

            return sb.ToString();
        }

        // 2. 시작 시간 복원
        private static void Restore_StartTimes(string data)
        {
            if (string.IsNullOrEmpty(data)) return;
            string[] t = data.Split('^');

            try
            {
                GenieConfig.MT_starttime = int.Parse(t[0]);

                GenieConfig.MT_new_start_A = int.Parse(t[1]);
                GenieConfig.MT_new_start_B = int.Parse(t[2]);
                GenieConfig.MT_new_start_C = int.Parse(t[3]);
                GenieConfig.MT_sell_time_start = int.Parse(t[4]);
                GenieConfig.MT_silson_start_W = int.Parse(t[5]);
                GenieConfig.MT_예상수익_start = int.Parse(t[6]);
                GenieConfig.MT_예상손실_start = int.Parse(t[7]);

                GenieConfig.MT_repeat_time_start_A = int.Parse(t[8]);
                GenieConfig.MT_repeat_time_start_B = int.Parse(t[9]);
                GenieConfig.MT_repeat_time_start_C = int.Parse(t[10]);
                GenieConfig.MT_repeat_time_start_D = int.Parse(t[11]);
                GenieConfig.MT_repeat_time_start_E = int.Parse(t[12]);
                GenieConfig.MT_repeat_time_start_F = int.Parse(t[13]);
                GenieConfig.MT_repeat_time_start_G = int.Parse(t[14]);
                GenieConfig.MT_repeat_time_start_H = int.Parse(t[15]);
                GenieConfig.MT_repeat_time_start_I = int.Parse(t[16]);
                GenieConfig.MT_repeat_time_start_J = int.Parse(t[17]);
                GenieConfig.MT_repeat_time_start_K = int.Parse(t[18]);
                GenieConfig.MT_repeat_time_start_L = int.Parse(t[19]);
                GenieConfig.MT_repeat_time_start_M = int.Parse(t[20]);
                GenieConfig.MT_repeat_time_start_N = int.Parse(t[21]);

                GenieConfig.MTB_예약주문_장전주문시간 = int.Parse(t[22]);

                GenieConfig.MT_rebalance_starttime_A = int.Parse(t[23]);
                GenieConfig.MT_rebalance_starttime_B = int.Parse(t[24]);
                GenieConfig.MT_rebalance_starttime_C = int.Parse(t[25]);
                GenieConfig.MT_rebalance_starttime_D = int.Parse(t[26]);
                GenieConfig.MT_rebalance_starttime_E = int.Parse(t[27]);
                GenieConfig.MT_rebalance_starttime_F = int.Parse(t[28]);
                GenieConfig.MT_rebalance_starttime_G = int.Parse(t[29]);

                GenieConfig.MTB_Liquidation_Starttime_A = int.Parse(t[30]);
                GenieConfig.MTB_Liquidation_Starttime_B = int.Parse(t[31]);
                GenieConfig.MTB_Liquidation_Starttime_C = int.Parse(t[32]);

                // (조건부 복원이었던 부분도 원본 값으로 덮어씁니다)
                GenieConfig.TB_TimeSell_start_A = int.Parse(t[33]);
                GenieConfig.TB_TimeSell_start_B = int.Parse(t[34]);
                GenieConfig.TB_TimeSell_start_C = int.Parse(t[35]);

                GenieConfig.MTB_rebalance_Selltime_오전 = int.Parse(t[36]);
            }
            catch { Console_print("시작시간 복원 오류"); }
        }

        // 3. 시작 시간 전체 +1시간 적용
        private static void Adjust_StartTimes(int addTime)
        {
            // 시장 종료시간을 넘지 않을 때만 더하기
            if (GenieConfig.MT_starttime + addTime < Get.시장종료) GenieConfig.MT_starttime += addTime;

            if (GenieConfig.MT_new_start_A + addTime < Get.시장종료) GenieConfig.MT_new_start_A += addTime;
            if (GenieConfig.MT_new_start_B + addTime < Get.시장종료) GenieConfig.MT_new_start_B += addTime;
            if (GenieConfig.MT_new_start_C + addTime < Get.시장종료) GenieConfig.MT_new_start_C += addTime;
            if (GenieConfig.MT_sell_time_start + addTime < Get.시장종료) GenieConfig.MT_sell_time_start += addTime;
            if (GenieConfig.MT_silson_start_W + addTime < Get.시장종료) GenieConfig.MT_silson_start_W += addTime;
            if (GenieConfig.MT_예상수익_start + addTime < Get.시장종료) GenieConfig.MT_예상수익_start += addTime;
            if (GenieConfig.MT_예상손실_start + addTime < Get.시장종료) GenieConfig.MT_예상손실_start += addTime;

            if (GenieConfig.MT_repeat_time_start_A + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_A += addTime;
            if (GenieConfig.MT_repeat_time_start_B + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_B += addTime;
            if (GenieConfig.MT_repeat_time_start_C + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_C += addTime;
            if (GenieConfig.MT_repeat_time_start_D + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_D += addTime;
            if (GenieConfig.MT_repeat_time_start_E + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_E += addTime;
            if (GenieConfig.MT_repeat_time_start_F + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_F += addTime;
            if (GenieConfig.MT_repeat_time_start_G + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_G += addTime;
            if (GenieConfig.MT_repeat_time_start_H + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_H += addTime;
            if (GenieConfig.MT_repeat_time_start_I + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_I += addTime;
            if (GenieConfig.MT_repeat_time_start_J + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_J += addTime;
            if (GenieConfig.MT_repeat_time_start_K + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_K += addTime;
            if (GenieConfig.MT_repeat_time_start_L + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_L += addTime;
            if (GenieConfig.MT_repeat_time_start_M + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_M += addTime;
            if (GenieConfig.MT_repeat_time_start_N + addTime < Get.시장종료) GenieConfig.MT_repeat_time_start_N += addTime;

            if (GenieConfig.MTB_예약주문_장전주문시간 + addTime < Get.시장종료) GenieConfig.MTB_예약주문_장전주문시간 += addTime;

            if (GenieConfig.MT_rebalance_starttime_A + addTime < Get.시장종료) GenieConfig.MT_rebalance_starttime_A += addTime;
            if (GenieConfig.MT_rebalance_starttime_B + addTime < Get.시장종료) GenieConfig.MT_rebalance_starttime_B += addTime;
            if (GenieConfig.MT_rebalance_starttime_C + addTime < Get.시장종료) GenieConfig.MT_rebalance_starttime_C += addTime;
            if (GenieConfig.MT_rebalance_starttime_D + addTime < Get.시장종료) GenieConfig.MT_rebalance_starttime_D += addTime;
            if (GenieConfig.MT_rebalance_starttime_E + addTime < Get.시장종료) GenieConfig.MT_rebalance_starttime_E += addTime;
            if (GenieConfig.MT_rebalance_starttime_F + addTime < Get.시장종료) GenieConfig.MT_rebalance_starttime_F += addTime;
            if (GenieConfig.MT_rebalance_starttime_G + addTime < Get.시장종료) GenieConfig.MT_rebalance_starttime_G += addTime;

            if (GenieConfig.MTB_Liquidation_Starttime_A + addTime < Get.시장종료) GenieConfig.MTB_Liquidation_Starttime_A += addTime;
            if (GenieConfig.MTB_Liquidation_Starttime_B + addTime < Get.시장종료) GenieConfig.MTB_Liquidation_Starttime_B += addTime;
            if (GenieConfig.MTB_Liquidation_Starttime_C + addTime < Get.시장종료) GenieConfig.MTB_Liquidation_Starttime_C += addTime;

            if (GenieConfig.CBB_TimeSell_start_A == 0) if (GenieConfig.TB_TimeSell_start_A + addTime < Get.시장종료) GenieConfig.TB_TimeSell_start_A += addTime;
            if (GenieConfig.CBB_TimeSell_start_B == 0) if (GenieConfig.TB_TimeSell_start_B + addTime < Get.시장종료) GenieConfig.TB_TimeSell_start_B += addTime;
            if (GenieConfig.CBB_TimeSell_start_C == 0) if (GenieConfig.TB_TimeSell_start_C + addTime < Get.시장종료) GenieConfig.TB_TimeSell_start_C += addTime;

            if (GenieConfig.MTB_rebalance_Selltime_오전 + addTime < Get.시장종료) GenieConfig.MTB_rebalance_Selltime_오전 += addTime;

            // UI 갱신
            Console_print("시작시간체크 :: " + GenieConfig.MT_starttime);
            시작시간그리기();
        }

        // 4. 종료 시간 백업 문자열 생성
        private static string Backup_EndTimes()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(GenieConfig.MT_closetime).Append("^");
            sb.Append(GenieConfig.MT_stoptime).Append("^");

            sb.Append(GenieConfig.MT_new_end_A).Append("^");
            sb.Append(GenieConfig.MT_new_end_B).Append("^");
            sb.Append(GenieConfig.MT_new_end_C).Append("^");
            sb.Append(GenieConfig.MT_sell_time_end).Append("^");
            sb.Append(GenieConfig.MT_silson_end_W).Append("^");
            sb.Append(GenieConfig.MT_예상수익_end).Append("^");
            sb.Append(GenieConfig.MT_예상손실_end).Append("^");

            sb.Append(GenieConfig.MT_repeat_time_end_A).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_end_B).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_end_C).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_end_D).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_end_E).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_end_F).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_end_G).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_end_H).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_end_I).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_end_J).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_end_K).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_end_L).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_end_M).Append("^");
            sb.Append(GenieConfig.MT_repeat_time_end_N).Append("^");

            sb.Append(GenieConfig.MT_rebalance_stoptime_A).Append("^");
            sb.Append(GenieConfig.MT_rebalance_stoptime_B).Append("^");
            sb.Append(GenieConfig.MT_rebalance_stoptime_C).Append("^");
            sb.Append(GenieConfig.MT_rebalance_stoptime_D).Append("^");
            sb.Append(GenieConfig.MT_rebalance_stoptime_E).Append("^");
            sb.Append(GenieConfig.MT_rebalance_stoptime_F).Append("^");
            sb.Append(GenieConfig.MT_rebalance_stoptime_G).Append("^");

            sb.Append(GenieConfig.MTB_Liquidation_Stoptime_A).Append("^");
            sb.Append(GenieConfig.MTB_Liquidation_Stoptime_B).Append("^");
            sb.Append(GenieConfig.MTB_Liquidation_Stoptime_C).Append("^");

            sb.Append(GenieConfig.MTB_cut_time_A).Append("^");
            sb.Append(GenieConfig.MTB_cut_time_B).Append("^");
            sb.Append(GenieConfig.MTB_cut_time_C).Append("^");

            sb.Append(GenieConfig.MTB_예약주문_종가주문시간).Append("^");
            sb.Append(GenieConfig.MT_misu_time).Append("^");
            sb.Append(GenieConfig.MTB_rebalance_Selltime_오후);

            return sb.ToString();
        }

        // 5. 종료 시간 복원
        private static void Restore_EndTimes(string data)
        {
            if (string.IsNullOrEmpty(data)) return;
            string[] t = data.Split('^');

            try
            {
                GenieConfig.MT_closetime = int.Parse(t[0]);
                GenieConfig.MT_stoptime = int.Parse(t[1]);

                GenieConfig.MT_new_end_A = int.Parse(t[2]);
                GenieConfig.MT_new_end_B = int.Parse(t[3]);
                GenieConfig.MT_new_end_C = int.Parse(t[4]);
                GenieConfig.MT_sell_time_end = int.Parse(t[5]);
                GenieConfig.MT_silson_end_W = int.Parse(t[6]);
                GenieConfig.MT_예상수익_end = int.Parse(t[7]);
                GenieConfig.MT_예상손실_end = int.Parse(t[8]);

                GenieConfig.MT_repeat_time_end_A = int.Parse(t[9]);
                GenieConfig.MT_repeat_time_end_B = int.Parse(t[10]);
                GenieConfig.MT_repeat_time_end_C = int.Parse(t[11]);
                GenieConfig.MT_repeat_time_end_D = int.Parse(t[12]);
                GenieConfig.MT_repeat_time_end_E = int.Parse(t[13]);
                GenieConfig.MT_repeat_time_end_F = int.Parse(t[14]);
                GenieConfig.MT_repeat_time_end_G = int.Parse(t[15]);
                GenieConfig.MT_repeat_time_end_H = int.Parse(t[16]);
                GenieConfig.MT_repeat_time_end_I = int.Parse(t[17]);
                GenieConfig.MT_repeat_time_end_J = int.Parse(t[18]);
                GenieConfig.MT_repeat_time_end_K = int.Parse(t[19]);
                GenieConfig.MT_repeat_time_end_L = int.Parse(t[20]);
                GenieConfig.MT_repeat_time_end_M = int.Parse(t[21]);
                GenieConfig.MT_repeat_time_end_N = int.Parse(t[22]);

                GenieConfig.MT_rebalance_stoptime_A = int.Parse(t[23]);
                GenieConfig.MT_rebalance_stoptime_B = int.Parse(t[24]);
                GenieConfig.MT_rebalance_stoptime_C = int.Parse(t[25]);
                GenieConfig.MT_rebalance_stoptime_D = int.Parse(t[26]);
                GenieConfig.MT_rebalance_stoptime_E = int.Parse(t[27]);
                GenieConfig.MT_rebalance_stoptime_F = int.Parse(t[28]);
                GenieConfig.MT_rebalance_stoptime_G = int.Parse(t[29]);

                GenieConfig.MTB_Liquidation_Stoptime_A = int.Parse(t[30]);
                GenieConfig.MTB_Liquidation_Stoptime_B = int.Parse(t[31]);
                GenieConfig.MTB_Liquidation_Stoptime_C = int.Parse(t[32]);

                GenieConfig.MTB_cut_time_A = int.Parse(t[33]);
                GenieConfig.MTB_cut_time_B = int.Parse(t[34]);
                GenieConfig.MTB_cut_time_C = int.Parse(t[35]);

                GenieConfig.MTB_예약주문_종가주문시간 = int.Parse(t[36]);
                GenieConfig.MT_misu_time = int.Parse(t[37]);
                GenieConfig.MTB_rebalance_Selltime_오후 = int.Parse(t[38]);
            }
            catch { Console_print("종료시간 복원 오류"); }
        }

        // 6. 종료 시간 전체 +1시간 적용
        private static void Adjust_EndTimes(int addTime)
        {
            GenieConfig.MT_closetime += addTime;
            GenieConfig.MT_stoptime += addTime;

            GenieConfig.MT_new_end_A += addTime;
            GenieConfig.MT_new_end_B += addTime;
            GenieConfig.MT_new_end_C += addTime;
            GenieConfig.MT_sell_time_end += addTime;
            GenieConfig.MT_silson_end_W += addTime;
            GenieConfig.MT_예상수익_end += addTime;
            GenieConfig.MT_예상손실_end += addTime;

            GenieConfig.MT_repeat_time_end_A += addTime;
            GenieConfig.MT_repeat_time_end_B += addTime;
            GenieConfig.MT_repeat_time_end_C += addTime;
            GenieConfig.MT_repeat_time_end_D += addTime;
            GenieConfig.MT_repeat_time_end_E += addTime;
            GenieConfig.MT_repeat_time_end_F += addTime;
            GenieConfig.MT_repeat_time_end_G += addTime;
            GenieConfig.MT_repeat_time_end_H += addTime;
            GenieConfig.MT_repeat_time_end_I += addTime;
            GenieConfig.MT_repeat_time_end_J += addTime;
            GenieConfig.MT_repeat_time_end_K += addTime;
            GenieConfig.MT_repeat_time_end_L += addTime;
            GenieConfig.MT_repeat_time_end_M += addTime;
            GenieConfig.MT_repeat_time_end_N += addTime;

            GenieConfig.MT_rebalance_stoptime_A += addTime;
            GenieConfig.MT_rebalance_stoptime_B += addTime;
            GenieConfig.MT_rebalance_stoptime_C += addTime;
            GenieConfig.MT_rebalance_stoptime_D += addTime;
            GenieConfig.MT_rebalance_stoptime_E += addTime;
            GenieConfig.MT_rebalance_stoptime_F += addTime;
            GenieConfig.MT_rebalance_stoptime_G += addTime;

            GenieConfig.MTB_Liquidation_Stoptime_A += addTime;
            GenieConfig.MTB_Liquidation_Stoptime_B += addTime;
            GenieConfig.MTB_Liquidation_Stoptime_C += addTime;

            GenieConfig.MTB_cut_time_A += addTime;
            GenieConfig.MTB_cut_time_B += addTime;
            GenieConfig.MTB_cut_time_C += addTime;

            GenieConfig.MTB_예약주문_종가주문시간 += addTime;
            GenieConfig.MT_misu_time += addTime;
            GenieConfig.MTB_rebalance_Selltime_오후 += addTime;

            종료시간그리기();
        }

        // 7. 화면 그리기 (UI 갱신)
        static void 시작시간그리기()
        {
            Form1.form1.TB_starttime.Text = GenieConfig.MT_starttime.ToString();
        }

        static void 종료시간그리기()
        {
            Form1.form1.TB_closetime.Text = GenieConfig.MT_closetime.ToString();
            Form1.form1.TB_stoptime.Text = GenieConfig.MT_stoptime.ToString();
            Form1.form1.MT_misu_time.Text = GenieConfig.MT_misu_time.ToString();
        }


        public static void 잔고표시내역()
        {
            // [1] 리밸런싱 최종매입가 설정 (Setting.accmgr, Setting.function)
            if (GenieConfig.combo_rebalance_suik_gubun_A != 7 || (!GenieConfig.CB_rebalance_A && GenieConfig.combo_rebalance_suik_gubun_A == 7)) GenieConfig.CB_최종매입가_A = false;
            if (GenieConfig.combo_rebalance_suik_gubun_B != 7 || (!GenieConfig.CB_rebalance_B && GenieConfig.combo_rebalance_suik_gubun_B == 7)) GenieConfig.CB_최종매입가_B = false;
            if (GenieConfig.combo_rebalance_suik_gubun_C != 7 || (!GenieConfig.CB_rebalance_C && GenieConfig.combo_rebalance_suik_gubun_C == 7)) GenieConfig.CB_최종매입가_C = false;
            if (GenieConfig.combo_rebalance_suik_gubun_D != 7 || (!GenieConfig.CB_rebalance_D && GenieConfig.combo_rebalance_suik_gubun_D == 7)) GenieConfig.CB_최종매입가_D = false;
            if (GenieConfig.combo_rebalance_suik_gubun_E != 7 || (!GenieConfig.CB_rebalance_E && GenieConfig.combo_rebalance_suik_gubun_E == 7)) GenieConfig.CB_최종매입가_E = false;
            if (GenieConfig.combo_rebalance_suik_gubun_F != 7 || (!GenieConfig.CB_rebalance_F && GenieConfig.combo_rebalance_suik_gubun_F == 7)) GenieConfig.CB_최종매입가_F = false;
            if (GenieConfig.combo_rebalance_suik_gubun_G != 7 || (!GenieConfig.CB_rebalance_G && GenieConfig.combo_rebalance_suik_gubun_G == 7)) GenieConfig.CB_최종매입가_G = false;

            // [2] 익회 모니터 (Setting.basic -> Setting.function)
            if (!GenieConfig.CB_ik_one_A && !GenieConfig.CB_ik_one_B && !GenieConfig.CB_ik_one_C && !GenieConfig.CB_ik_one_D && !GenieConfig.CB_ik_one_E &&
                !GenieConfig.CB_ik_one_F && !GenieConfig.CB_ik_one_G && !GenieConfig.CB_ik_one_H && !GenieConfig.CB_ik_one_I) GenieConfig.CB_익회모니터 = false;

            // [3] 익절 모니터 (Setting.basic -> Setting.function)
            if (!GenieConfig.CB_ik_A && !GenieConfig.CB_ik_B && !GenieConfig.CB_ik_C && !GenieConfig.CB_ik_D &&
                !GenieConfig.CB_ik_E && !GenieConfig.CB_ik_F && !GenieConfig.CB_ik_G && !GenieConfig.CB_ik_H && !GenieConfig.CB_ik_I &&
                !GenieConfig.CB_TS_A && !GenieConfig.CB_TS_B && !GenieConfig.CB_TS_C && !GenieConfig.CB_TS_D && !GenieConfig.CB_TS_E &&
                !GenieConfig.CB_TS_F && !GenieConfig.CB_TS_G && !GenieConfig.CB_TS_H && !GenieConfig.CB_TS_I) GenieConfig.CB_익절모니터 = false;

            // [4] 보전 모니터 (Setting.basic -> Setting.function)
            if (!GenieConfig.CB_ik_down_A && !GenieConfig.CB_ik_down_B && !GenieConfig.CB_ik_down_C && !GenieConfig.CB_ik_down_D &&
                !GenieConfig.CB_ik_down_E && !GenieConfig.CB_ik_down_F && !GenieConfig.CB_ik_down_G && !GenieConfig.CB_ik_down_H && !GenieConfig.CB_ik_down_I) GenieConfig.CB_보전모니터 = false;

            // [5] 손절 모니터 (Setting.basic -> Setting.function)
            if (!GenieConfig.CB_sell_use_A && !GenieConfig.CB_sell_use_B &&
                !GenieConfig.CB_sell_use_C && !GenieConfig.CB_sell_use_D && !GenieConfig.CB_sell_use_E && !GenieConfig.CB_sell_use_F) GenieConfig.CB_손절모니터 = false;

            // [6] 시간 청산 범위 (Setting.special)
            bool 모니터 = GenieConfig.CB_시간청산범위;

            if ((GenieConfig.CB_TimeSell_A && GenieConfig.CB_TimeSell_수익범위_choice_A) ||
                (GenieConfig.CB_TimeSell_B && GenieConfig.CB_TimeSell_수익범위_choice_B) ||
                (GenieConfig.CB_TimeSell_C && GenieConfig.CB_TimeSell_수익범위_choice_C)) GenieConfig.CB_시간청산범위 = 모니터;
            else GenieConfig.CB_시간청산범위 = false;

            if ((!GenieConfig.CB_TimeSell_A && !GenieConfig.CB_TimeSell_B && !GenieConfig.CB_TimeSell_C) ||
                (!GenieConfig.CB_TimeSell_수익범위_choice_A && !GenieConfig.CB_TimeSell_수익범위_choice_B && !GenieConfig.CB_TimeSell_수익범위_choice_C)) GenieConfig.CB_시간청산범위 = false;

            // [7] 잔고 청산 범위 (Setting.accmgr)
            모니터 = GenieConfig.CB_잔고청산범위;
            if ((GenieConfig.CB_Liquidation_A && GenieConfig.CB_Liquidation_choice_A) ||
                (GenieConfig.CB_Liquidation_B && GenieConfig.CB_Liquidation_choice_B) ||
                (GenieConfig.CB_Liquidation_C && GenieConfig.CB_Liquidation_choice_C)) GenieConfig.CB_잔고청산범위 = 모니터;
            else GenieConfig.CB_잔고청산범위 = false;

            if ((!GenieConfig.CB_Liquidation_A && !GenieConfig.CB_Liquidation_B && !GenieConfig.CB_Liquidation_C) ||
                (!GenieConfig.CB_Liquidation_choice_A && !GenieConfig.CB_Liquidation_choice_B && !GenieConfig.CB_Liquidation_choice_C)) GenieConfig.CB_잔고청산범위 = false;

            // [8] 반복 매매 범위 (Setting.repeat)
            모니터 = GenieConfig.CB_반복매매범위;
            if ((GenieConfig.CB_repeat_use_A && GenieConfig.CB_repeat_choice_A) ||
                (GenieConfig.CB_repeat_use_B && GenieConfig.CB_repeat_choice_B) ||
                (GenieConfig.CB_repeat_use_C && GenieConfig.CB_repeat_choice_C) ||
                (GenieConfig.CB_repeat_use_D && GenieConfig.CB_repeat_choice_D) ||
                (GenieConfig.CB_repeat_use_E && GenieConfig.CB_repeat_choice_E) ||
                (GenieConfig.CB_repeat_use_F && GenieConfig.CB_repeat_choice_F) ||
                (GenieConfig.CB_repeat_use_G && GenieConfig.CB_repeat_choice_G) ||
                (GenieConfig.CB_repeat_use_H && GenieConfig.CB_repeat_choice_H) ||
                (GenieConfig.CB_repeat_use_I && GenieConfig.CB_repeat_choice_I) ||
                (GenieConfig.CB_repeat_use_J && GenieConfig.CB_repeat_choice_J) ||
                (GenieConfig.CB_repeat_use_K && GenieConfig.CB_repeat_choice_K) ||
                (GenieConfig.CB_repeat_use_L && GenieConfig.CB_repeat_choice_L) ||
                (GenieConfig.CB_repeat_use_M && GenieConfig.CB_repeat_choice_M) ||
                (GenieConfig.CB_repeat_use_N && GenieConfig.CB_repeat_choice_N)) GenieConfig.CB_반복매매범위 = 모니터;
            else GenieConfig.CB_반복매매범위 = false;

            if ((!GenieConfig.CB_repeat_use_A &&
                    !GenieConfig.CB_repeat_use_B &&
                    !GenieConfig.CB_repeat_use_C &&
                    !GenieConfig.CB_repeat_use_D &&
                    !GenieConfig.CB_repeat_use_E &&
                    !GenieConfig.CB_repeat_use_F &&
                    !GenieConfig.CB_repeat_use_G &&
                    !GenieConfig.CB_repeat_use_H &&
                    !GenieConfig.CB_repeat_use_I &&
                    !GenieConfig.CB_repeat_use_J &&
                    !GenieConfig.CB_repeat_use_K &&
                    !GenieConfig.CB_repeat_use_L &&
                    !GenieConfig.CB_repeat_use_M &&
                    !GenieConfig.CB_repeat_use_N) ||
                (!GenieConfig.CB_repeat_choice_A &&
                    !GenieConfig.CB_repeat_choice_B &&
                    !GenieConfig.CB_repeat_choice_C &&
                    !GenieConfig.CB_repeat_choice_D &&
                    !GenieConfig.CB_repeat_choice_E &&
                    !GenieConfig.CB_repeat_choice_F &&
                    !GenieConfig.CB_repeat_choice_G &&
                    !GenieConfig.CB_repeat_choice_H &&
                    !GenieConfig.CB_repeat_choice_I &&
                    !GenieConfig.CB_repeat_choice_J &&
                    !GenieConfig.CB_repeat_choice_K &&
                    !GenieConfig.CB_repeat_choice_L &&
                    !GenieConfig.CB_repeat_choice_M &&
                    !GenieConfig.CB_repeat_choice_N)) GenieConfig.CB_반복매매범위 = false;

            // [9] 리밸런싱 범위 (Setting.accmgr)
            모니터 = GenieConfig.CB_리밸런싱범위;
            if ((GenieConfig.CB_rebalance_A && GenieConfig.CB_rebalance_choice_A) ||
                (GenieConfig.CB_rebalance_B && GenieConfig.CB_rebalance_choice_B) ||
                (GenieConfig.CB_rebalance_C && GenieConfig.CB_rebalance_choice_C) ||
                (GenieConfig.CB_rebalance_D && GenieConfig.CB_rebalance_choice_D) ||
                (GenieConfig.CB_rebalance_E && GenieConfig.CB_rebalance_choice_E) ||
                (GenieConfig.CB_rebalance_F && GenieConfig.CB_rebalance_choice_F) ||
                (GenieConfig.CB_rebalance_G && GenieConfig.CB_rebalance_choice_G)) GenieConfig.CB_리밸런싱범위 = 모니터;
            else GenieConfig.CB_리밸런싱범위 = false;

            if ((!GenieConfig.CB_rebalance_A &&
                    !GenieConfig.CB_rebalance_B &&
                    !GenieConfig.CB_rebalance_C &&
                    !GenieConfig.CB_rebalance_D &&
                    !GenieConfig.CB_rebalance_E &&
                    !GenieConfig.CB_rebalance_F &&
                    !GenieConfig.CB_rebalance_G) ||
                (!GenieConfig.CB_rebalance_choice_A &&
                    !GenieConfig.CB_rebalance_choice_B &&
                    !GenieConfig.CB_rebalance_choice_C &&
                    !GenieConfig.CB_rebalance_choice_D &&
                    !GenieConfig.CB_rebalance_choice_E &&
                    !GenieConfig.CB_rebalance_choice_F &&
                    !GenieConfig.CB_rebalance_choice_G)) GenieConfig.CB_리밸런싱범위 = false;

            // [10] 잔고 청산 범위 재확인 (Setting.accmgr)
            모니터 = GenieConfig.CB_잔고청산범위;
            if ((GenieConfig.CB_Liquidation_A && GenieConfig.CB_Liquidation_choice_A) ||
                (GenieConfig.CB_Liquidation_B && GenieConfig.CB_Liquidation_choice_B) ||
                (GenieConfig.CB_Liquidation_C && GenieConfig.CB_Liquidation_choice_C)) GenieConfig.CB_잔고청산범위 = 모니터;
            else GenieConfig.CB_잔고청산범위 = false;

            if ((!GenieConfig.CB_Liquidation_A && !GenieConfig.CB_Liquidation_B && !GenieConfig.CB_Liquidation_C) ||
                (!GenieConfig.CB_Liquidation_choice_A && !GenieConfig.CB_Liquidation_choice_B && !GenieConfig.CB_Liquidation_choice_C)) GenieConfig.CB_잔고청산범위 = false;

            // [11] 컬럼 추가
            Form_Function.칼람추가();
        }
    }
}
