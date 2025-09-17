using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니_64
{
    public class Guide
    {
        static string[] 신규_A;
        static string[] 신규_B;
        static string[] 신규_C;

        static string[] 반복_A;
        static string[] 반복_B;
        static string[] 반복_C;
        static string[] 반복_D;
        static string[] 반복_E;
        static string[] 반복_F;
        static string[] 반복_G;
        static string[] 반복_H;
        static string[] 반복_I;
        static string[] 반복_J;
        static string[] 반복_K;
        static string[] 반복_L;
        static string[] 반복_M;
        static string[] 반복_N;

        static string[] 리밸_A;
        static string[] 리밸_B;
        static string[] 리밸_C;
        static string[] 리밸_D;
        static string[] 리밸_E;
        static string[] 리밸_F;
        static string[] 리밸_G;

        static string[] 청산_A;
        static string[] 청산_B;
        static string[] 청산_C;

        public static void 공휴일계산()
        {
            string 고정휴장일 = "0101;0301;0501;0505;0606;0815;1003;1009;1225;1231;";
            string 설날 = "0128;0129;0130;";
            string 추석 = "1005;1006;1007;1008;";
            string 부처님오신날 = "0505;";
            string 대체휴일 = "0303;0506;1008;";
            string 공휴일 = 고정휴장일 + 설날 + 추석 + 부처님오신날 + 대체휴일;

            if (공휴일.Contains(DateTime.Now.ToString("MMdd;"))) Form1.공휴일 = true;
        }

        public static void GuideLoding()
        {
            if (Properties.Settings.Default.CB_가이드매매)
            {
                Console.WriteLine("####################### 가이드매매 로딩 #######################");

                Form1.form1.CB_Jisu_avgset.Enabled = false;
                Form1.form1.CB_기본매매.Enabled = false;
                Form1.form1.CB_반복매매.Enabled = false;
                Form1.form1.CB_계좌관리.Enabled = false;
                Form1.form1.CB_특수매매.Enabled = false;
                Form1.form1.CB_매매그룹.Enabled = false;
                Form1.form1.CB_대금탐색.Enabled = false;
                Form1.form1.CB_기능설정.Enabled = false;
                Form1.form1.CB_Jisu_avgset.Enabled = false;

                패널감추기_신규매수();
                패널감추기_특수매매();

                ControllerDisable.Form_1_Disable();
            }
        }

        public static void 가이드매매설정로딩()
        {
            Form1.음소거 = true;

            if (리턴(Properties.Settings.Default.combo_new_condition_A) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_new_condition_A), "가이드로딩 신규_A");
            if (리턴(Properties.Settings.Default.combo_new_condition_B) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_new_condition_B), "가이드로딩 신규_B");
            if (리턴(Properties.Settings.Default.combo_new_condition_C) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_new_condition_C), "가이드로딩 신규_C");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_A) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_A), "가이드로딩 반복_A");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_B) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_B), "가이드로딩 반복_B");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_C) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_C), "가이드로딩 반복_C");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_D) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_D), "가이드로딩 반복_D");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_E) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_E), "가이드로딩 반복_E");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_F) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_F), "가이드로딩 반복_F");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_G) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_G), "가이드로딩 반복_G");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_H) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_H), "가이드로딩 반복_H");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_I) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_I), "가이드로딩 반복_I");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_J) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_J), "가이드로딩 반복_J");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_K) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_K), "가이드로딩 반복_K");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_L) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_L), "가이드로딩 반복_L");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_M) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_M), "가이드로딩 반복_M");
            if (리턴(Properties.Settings.Default.combo_repeat_condition_N) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_repeat_condition_N), "가이드로딩 반복_N");
            if (리턴(Properties.Settings.Default.combo_rebalance_condition_A) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_rebalance_condition_A), "가이드로딩 리밸_A");
            if (리턴(Properties.Settings.Default.combo_rebalance_condition_B) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_rebalance_condition_B), "가이드로딩 리밸_B");
            if (리턴(Properties.Settings.Default.combo_rebalance_condition_C) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_rebalance_condition_C), "가이드로딩 리밸_C");
            if (리턴(Properties.Settings.Default.combo_rebalance_condition_D) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_rebalance_condition_D), "가이드로딩 리밸_D");
            if (리턴(Properties.Settings.Default.combo_rebalance_condition_E) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_rebalance_condition_E), "가이드로딩 리밸_E");
            if (리턴(Properties.Settings.Default.combo_rebalance_condition_F) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_rebalance_condition_F), "가이드로딩 리밸_F");
            if (리턴(Properties.Settings.Default.combo_rebalance_condition_G) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.combo_rebalance_condition_G), "가이드로딩 리밸_G");
            if (리턴(Properties.Settings.Default.CBB_Liquidation_condition_A) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.CBB_Liquidation_condition_A), "가이드로딩 청산_A");
            if (리턴(Properties.Settings.Default.CBB_Liquidation_condition_B) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.CBB_Liquidation_condition_B), "가이드로딩 청산_B");
            if (리턴(Properties.Settings.Default.CBB_Liquidation_condition_C) != null) Condition_Management.Stop_Monitoring(리턴(Properties.Settings.Default.CBB_Liquidation_condition_C), "가이드로딩 청산_C");

            Condition 리턴(string 검색식)
            {
                Condition result = Form1.form1.ConditionList.Find(o => o.name.Equals(검색식));
                return result;
            }

            계좌설정();
            기본매매설정();
            대금탐색설정();
            반복매매설정();
            계좌관리설정();
            매매그룹설정();
            특수설정();
            기능설정();

            가이드검색식로딩();

            Form1.음소거 = Properties.Settings.Default.CB_음소거;
            Load_condition_textprint();
        }

        private static void 가이드검색식로딩()
        {
            Properties.Settings.Default.신규검색식 = Properties.Settings.Default.combo_new_condition_A + "^" + Properties.Settings.Default.combo_new_condition_B + "^" + Properties.Settings.Default.combo_new_condition_C;

            Properties.Settings.Default.반복매매검색식 = Properties.Settings.Default.combo_repeat_condition_A + "^" + Properties.Settings.Default.combo_repeat_condition_B + "^" + Properties.Settings.Default.combo_repeat_condition_C + "^" +
                                                        Properties.Settings.Default.combo_repeat_condition_D + "^" + Properties.Settings.Default.combo_repeat_condition_E + "^" + Properties.Settings.Default.combo_repeat_condition_F + "^" +
                                                        Properties.Settings.Default.combo_repeat_condition_G + "^" + Properties.Settings.Default.combo_repeat_condition_H + "^" + Properties.Settings.Default.combo_repeat_condition_I + "^" +
                                                        Properties.Settings.Default.combo_repeat_condition_J + "^" + Properties.Settings.Default.combo_repeat_condition_K + "^" + Properties.Settings.Default.combo_repeat_condition_L + "^" +
                                                        Properties.Settings.Default.combo_repeat_condition_M + "^" + Properties.Settings.Default.combo_repeat_condition_N;

            Properties.Settings.Default.계좌관리검색식 = Properties.Settings.Default.combo_rebalance_condition_A + "^" + Properties.Settings.Default.combo_rebalance_condition_B + "^" + Properties.Settings.Default.combo_rebalance_condition_C + "^" +
                                                        Properties.Settings.Default.combo_rebalance_condition_D + "^" + Properties.Settings.Default.combo_rebalance_condition_E + "^" + Properties.Settings.Default.combo_rebalance_condition_F + "^" +
                                                        Properties.Settings.Default.combo_rebalance_condition_G + "^" + Properties.Settings.Default.CBB_Liquidation_condition_A + "^" + Properties.Settings.Default.CBB_Liquidation_condition_B + "^" +
                                                        Properties.Settings.Default.CBB_Liquidation_condition_C;

            Properties.Settings.Default.와치검색식 = Properties.Settings.Default.combo_watch_condition_AA + "^" + Properties.Settings.Default.combo_watch_condition_BB + "^" + Properties.Settings.Default.combo_watch_condition_CC + "^" +
                                                    Properties.Settings.Default.combo_watch_condition_DD;

            Condition_Management.Condition_save();   // 검색식 저장
            Condition_Management.Condition_DataLoad(Properties.Settings.Default.select_account);
            Form1.form1.가이드검색식확인 = true;
        }

        public static void 계좌설정()
        {
            계좌설정_(80000, 200000, 20, true, 65, 0, false, 50, 11, 1);
            계좌설정_코스피(-1.5, 0, 3, -1.5, 0, 3, 0.1, 0, 3);
            계좌설정_코스닥(-1.5, 0, 3, -1.5, 0, 3, 0.1, 0, 3);
            계좌설정_미수사용(true, 151500, 5, 10, 0, 1, 30);
            계좌설정_이평선(true, true,
                           true, true, false, false, false, false, true, true, false, false, false, false,
                           true, true, false, false, false, false, true, true, false, false, false, false,
                           true, true,
                           true, true, false, false, false, false, true, true, false, false, false, false,
                           true, true, false, false, false, false, true, true, false, false, false, false);
        }
        public static void 기본매매설정()
        {
            기본매매_신규횟수제한(true, 6);
            기본매매_new_A(true, 0, 3600, 0, 0, true, 0, "종목선정", 100000, 152000, 1, 0, 0, 1);
            기본매매_new_B(false, 0, 3600, 0, 0, false, 0, "", 100000, 152000, 1, 0, 0, 1);
            기본매매_new_C(false, 0, 3600, 0, 0, false, 0, "", 100000, 152000, 1, 0, 0, 1);
            기본매매_신규매수조건(3000, 50000, -10, 0, 180, true, 180);
            기본매매_신규매수_세부조건(20, 10, 10, 360000, 360000, 360000, false, false, false);

            기본매매_트레일링스탑_조건(true, true, true, 60, 1, 0);
            기본매매_트레일링스탑_A(false, 10, 2, -3, 25, 3, 0, 0);
            기본매매_트레일링스탑_B(false, 10, 2, -4, 50, 3, 0, 0);
            기본매매_트레일링스탑_C(false, 10, 2, -5, 100, 3, 0, 0);
            기본매매_트레일링스탑_D(false, 10, 2, -5.2, 100, 3, 0, 0);
            기본매매_트레일링스탑_E(false, 10, 2, -5.4, 100, 3, 0, 0);
            기본매매_트레일링스탑_F(false, 10, 2, -5.6, 100, 3, 0, 0);
            기본매매_트레일링스탑_G(false, 10, 2, -2, 1, 3, 0, 0);
            기본매매_트레일링스탑_H(false, 10, 2, -2, 1, 3, 0, 0);
            기본매매_트레일링스탑_I(false, 10, 2, -2, 1, 3, 0, 0);
        }

        private static void 반복매매설정()
        {
            반복기준금(true);
            반복매매_A(true, 150500, 151500, true, 0, "", 0, 0, 0, 1000, 3, 1, 5, 1, 1, 5, 1, 10, 1, 1, -100, false, 100, 0, 1, 2, 0, 1000, 1, 1800, 0, 0, 360, 0, 0);
            반복매매_B(false, 80000, 200000, true, 0, "", 0, 0, 0, 0, 3, 0, 5, 0, 0, 5, 0, 10, 0, 0, 0, false, 100, 0, 1, 0, 0, 100, 0, 60, 0, 0, 180, 0, 0);
            반복매매_C(false, 80000, 200000, true, 0, "", 0, 0, 0, 0, 3, 0, 5, 0, 0, 5, 0, 10, 0, 0, 0, false, 100, 0, 1, 0, 0, 100, 0, 60, 0, 0, 180, 0, 0);
            반복매매_D(false, 80000, 200000, true, 0, "", 0, 0, 0, 0, 3, 0, 5, 0, 0, 5, 0, 10, 0, 0, 0, false, 100, 0, 1, 0, 0, 100, 0, 60, 0, 0, 180, 0, 0);
            반복매매_E(false, 80000, 200000, true, 0, "", 0, 0, 0, 0, 3, 0, 5, 0, 0, 5, 0, 10, 0, 0, 0, false, 100, 0, 1, 0, 0, 100, 0, 60, 0, 0, 180, 0, 0);
            반복매매_F(false, 80000, 200000, true, 0, "", 0, 0, 0, 0, 3, 0, 5, 0, 0, 5, 0, 10, 0, 0, 0, false, 100, 0, 1, 0, 0, 100, 0, 60, 0, 0, 180, 0, 0);
            반복매매_G(false, 80000, 200000, true, 0, "", 0, 0, 0, 0, 3, 0, 5, 0, 0, 5, 0, 10, 0, 0, 0, false, 100, 0, 1, 0, 0, 100, 0, 60, 0, 0, 180, 0, 0);
            반복매매_H(false, 80000, 200000, true, 0, "", 0, 0, 0, 0, 3, 0, 5, 0, 0, 5, 0, 10, 0, 0, 0, false, 100, 0, 1, 0, 0, 100, 0, 60, 0, 0, 180, 0, 0);
            반복매매_I(false, 80000, 200000, true, 0, "", 0, 0, 0, 0, 3, 0, 5, 0, 0, 5, 0, 10, 0, 0, 0, false, 100, 0, 1, 0, 0, 100, 0, 60, 0, 0, 180, 0, 0);
            반복매매_J(false, 80000, 200000, true, 0, "", 0, 0, 0, 0, 3, 0, 5, 0, 0, 5, 0, 10, 0, 0, 0, false, 100, 0, 1, 0, 0, 100, 0, 60, 0, 0, 180, 0, 0);
            반복매매_K(false, 80000, 200000, true, 0, "", 0, 0, 0, 0, 3, 0, 5, 0, 0, 5, 0, 10, 0, 0, 0, false, 100, 0, 1, 0, 0, 100, 0, 60, 0, 0, 180, 0, 0);
            반복매매_L(false, 80000, 200000, true, 0, "", 0, 0, 0, 0, 3, 0, 5, 0, 0, 5, 0, 10, 0, 0, 0, false, 100, 0, 1, 0, 0, 100, 0, 60, 0, 0, 180, 0, 0);
            반복매매_M(false, 80000, 200000, true, 0, "", 0, 0, 0, 0, 3, 0, 5, 0, 0, 5, 0, 10, 0, 0, 0, false, 100, 0, 1, 0, 0, 100, 0, 60, 0, 0, 180, 0, 0);
            반복매매_N(false, 80000, 200000, true, 0, "", 0, 0, 0, 0, 3, 0, 5, 0, 0, 5, 0, 10, 0, 0, 0, false, 100, 0, 1, 0, 0, 100, 0, 60, 0, 0, 180, 0, 0);
        }

        private static void 계좌관리설정()
        {
            계좌관리_추매조건(false, 1000, true, 20, false, 5, 1000, 100000, -0.3, 22);
            계좌관리_분할주문(-2, -1, 1, 4, 5, 3);
            계좌관리_기준비율관리(false, 14, false, 14);
            계좌관리_감시주문시간n기준금(90300, 151000, true, true, true);

            계좌관리_리밸런싱_A(true, 91000, 151000, 0, "", 0, 0, 0, 1000, 3, 1, 5, 1, 1, 5, 1, 10, 1, 1, 0, false, 100, 0, 5, 2, 0, 1000, 1, 5, 0, 1, 60, false, 1.5, "매도수익률", 100, 3600, -5, "손절매도수익률", 50, 3600, true, 0, 0, 1, false, -0.6, 3, 2, false, -2, 3, 2);
            계좌관리_리밸런싱_B(true, 91000, 151000, 1, "수탐A", 0, 0, 100000, 10000, 3, 1, 5, 1, 1, 5, 1, 10, 1, 1, -100, false, 100, 0, 5, 2, 0, 1000, 1, 50, 0, 1, 60, false, 1.5, "매도수익률", 100, 3600, -5, "손절매도수익률", 50, 3600, true, 0, 0, 1, true, -0.6, 3, 2, false, -2, 3, 2);
            계좌관리_리밸런싱_C(true, 91000, 151000, 1, "수탐B", 0, 0, 100000, 10000, 3, 1, 5, 1, 1, 5, 1, 10, 1, 1, 0, false, 100, 0, 2, 2, 0, 1000, 1, 50, 0, 1, 60, false, 1.5, "매도수익률", 100, 3600, -5, "손절매도수익률", 50, 3600, true, 0, 0, 1, true, -0.6, 3, 2, false, -2, 3, 2);
            계좌관리_리밸런싱_D(true, 91000, 151000, 1, "수탐B", 0, 0, 100000, 10000, 3, 1, 5, 1, 1, 5, 1, 10, 1, 1, 0, false, 100, 0, 2, 2, 0, 1000, 1, 50, 0, 1, 60, false, 1.2, "매도수익률", 100, 3600, -5, "손절매도수익률", 100, 3600, false, 0, 0, 1, false, -0.6, 3, 2, false, -2, 3, 2);
            계좌관리_리밸런싱_E(true, 130000, 151500, 0, "", 0, 0, 1000, 1000, 3, 1, 5, 1, 1, 5, 1, 10, 1, 1, 0, false, -3, 7, 4, 2, 0, 1000, 1, 5, 0, 1, 180, false, 3, "매도수익률", 100, 3600, 0, "(    X    )", 10, 3600, false, 0, 0, 1, true, -1, 3, 2, false, -2, 3, 2);
            계좌관리_리밸런싱_F(true, 130000, 151500, 0, "", 0, 0, 1000, 1000, 3, 1, 5, 1, 1, 5, 1, 10, 1, 1, 0, false, -6, 7, 4, 2, 0, 1000, 1, 5, 0, 1, 180, false, 6, "매도수익률", 100, 3600, 0, "(    X    )", 1, 3600, false, 0, 0, 1, true, -1, 3, 2, false, -2, 3, 2);
            계좌관리_리밸런싱_G(true, 130000, 151500, 0, "", 0, 0, 1000, 1000, 3, 1, 5, 1, 1, 5, 1, 10, 1, 1, 0, false, -9, 7, 4, 2, 0, 1000, 1, 5, 0, 1, 180, false, 9, "매도수익률", 100, 3600, 0, "(    X    )", 10, 3600, false, 0, 0, 1, true, -1, 3, 2, false, -2, 3, 2);

            계좌관리_잔고청산_A(true, 80000, 200000, 0, "", 0, 0, 10000, 30, 1, 1, false, 100, 2, 20, 3, 0, 100, 5, 0, 1, 30, 0, 0, false, true, true, true, true, -3, 5, 2, 3, 2);
            계좌관리_잔고청산_B(true, 80000, 200000, 0, "", 0, 0, 10000, 30, 1, 10, false, 0, 4, 20, 3, 50, 100, 5, 0, 1, 30, 0, 0, false, false, true, true, true, -3, 5, 2, 3, 2);
            계좌관리_잔고청산_C(true, 80000, 200000, 0, "", 0, 0, 10000, 30, 1, 15, false, 0, 4, 20, 3, 0, 100, 5, 0, 1, 30, 0, 0, false, false, true, true, true, -3, 5, 2, 3, 2);
            계좌관리_실현손익담보손실매도_A(true, 193000, 2, 100, 50, 50, -20, 5, 2, 0, 1, 60);
            계좌관리_실현손익담보손실매도_B(false, 193000, 2, 100, 50, 50, -20, 5, 2, 0, 1, 60);
            계좌관리_실현손익담보손실매도_C(false, 193000, 5, 100, 50, 50, -20, 5, 2, 0, 1, 60);
        }

        private static void 특수설정()
        {
            특수설정_신규그룹(3, 2, 1);
            특수설정_신규그룹(true, false, 90300, false, 150000);

            특수설정_매매기간주문_A(2, 7, 2, 2, 4, 50, 3, 0, 1, 60, true, true, true, -3, 5, 2, 3, 2);
            특수설정_매매기간주문_B(2, 14, 2, 1, 4, 50, 3, 0, 1, 60, true, true, true, -3, 5, 2, 3, 2);
            특수설정_매매기간주문_C(2, 21, 2, 0.5, 4, 50, 3, 0, 1, 60, true, true, true, -3, 5, 2, 3, 2);
            특수설정_매매기간주문_D(2, 30, 2, 2.5, 4, 50, 3, 0, 1, 60, true, true, true, -3, 5, 2, 3, 2);
            특수설정_매매기간주문_E(2, 60, 2, 2.5, 4, 50, 3, 0, 1, 60, true, true, true, -3, 5, 2, 3, 2);
            특수설정_매매기간주문_F(2, 10, 2, 3, 4, 50, 3, 0, 1, 60, true, true, true, -3, 5, 2, 3, 2);
        }
        private static void 매매그룹설정()
        {
            매매그룹설정_익절(true, true, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_손절(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복A(true, true, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복B(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복C(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복D(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복E(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복F(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복G(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복H(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복I(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복J(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복K(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복L(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복M(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_반복N(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_리밸_A(true, true, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_리밸_B(true, true, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_리밸_C(true, true, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_리밸_D(true, true, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_리밸_E(true, true, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_리밸_F(true, true, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_리밸_G(true, true, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_청산_A(true, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_청산_B(false, true, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_청산_C(false, true, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_기간매도_A(true, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_기간매도_B(true, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_기간매도_C(true, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_기간매도_D(false, true, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_기간매도_E(false, false, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_기간매도_F(true, true, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_미수금정리(true, true, true, false, true, false, false, false, false, false, false, false);
            매매그룹설정_손익담보매도_A(true, true, true, false, false, false, false, false, false, false, false, false);
            매매그룹설정_손익담보매도_B(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_손익담보매도_C(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_계좌청산_특정시간(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_계좌청산_실현손익(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_계좌청산_예상손실(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_계좌청산_예상수익(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_시간청산_A(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_시간청산_B(false, false, false, false, false, false, false, false, false, false, false, false);
            매매그룹설정_시간청산_C(false, false, false, false, false, false, false, false, false, false, false, false);
        }
        private static void 대금탐색설정()
        {
            대금탐색_누적거래대금(1000);
            대금탐색_매수_A(0, 10, 100, 10, 100, true, "수탐A", 1, 0, 2, true, 1, false, 5000, 20000, 50000, 200000, 500000, 500000, 5000, 5000, 5000, 10000, 20000, 30000, 10, 1, 1);
            대금탐색_매수_B(1, 10, 100, 10, 100, true, "수탐B", 1, 0, 2, true, 1, false, 5000, 20000, 50000, 200000, 500000, 500000, 5000, 5000, 5000, 10000, 20000, 30000, 10, 1, 1);
            대금탐색_매도(false, "매탐", 2, 1, 2, true, 1, true, 5000, 500000, 200000, 50000, 20000, 500000, 3000, 30000, 20000, 10000, 5000, 30000, 1, 1);
        }
        private static void 기능설정()
        {
            기능설정값(false, true, false, false, true, false, true, false, false, false, true, true);
        }
        private static void 패널감추기_신규매수()
        {
            Form_Basic.form.P_계좌매매.Hide();
            Form_Basic.form.P_손실청산.Hide();
            Form_Basic.form.P_익절매도.Hide();
            Form_Basic.form.P_손실청산.Hide();
            Form_Basic.form.P_잔고시간청산.Hide();
            Form_Basic.form.P_체결즉시.Hide();

            Form_Basic.form.P_진입조건.Location = new System.Drawing.Point(430, 141);
            Form_Basic.form.lb_TS.Location = new System.Drawing.Point(-1, 120);
            Form_Basic.form.P_트레일링스탑.Location = new System.Drawing.Point(-1, 141);
            Form_Basic.form.P_트레일링스탑.SendToBack();

            Form_Basic.form.label_신규취소_A.Enabled = false;
            Form_Basic.form.label_신규취소_B.Enabled = false;
            Form_Basic.form.label_신규취소_C.Enabled = false;

            Form_Basic.form.CB_ik_A.Checked = false;
            Form_Basic.form.CB_ik_B.Checked = false;
            Form_Basic.form.CB_ik_C.Checked = false;
            Form_Basic.form.CB_ik_D.Checked = false;
            Form_Basic.form.CB_ik_E.Checked = false;
            Form_Basic.form.CB_ik_F.Checked = false;
            Form_Basic.form.CB_ik_G.Checked = false;
            Form_Basic.form.CB_ik_H.Checked = false;
            Form_Basic.form.CB_ik_I.Checked = false;

            Properties.Settings.Default.CB_ik_A = false;
            Properties.Settings.Default.CB_ik_B = false;
            Properties.Settings.Default.CB_ik_C = false;
            Properties.Settings.Default.CB_ik_D = false;
            Properties.Settings.Default.CB_ik_E = false;
            Properties.Settings.Default.CB_ik_F = false;
            Properties.Settings.Default.CB_ik_G = false;
            Properties.Settings.Default.CB_ik_H = false;
            Properties.Settings.Default.CB_ik_I = false;

            Form_Basic.form.CB_ik_one_A.Checked = false;
            Form_Basic.form.CB_ik_one_B.Checked = false;
            Form_Basic.form.CB_ik_one_C.Checked = false;
            Form_Basic.form.CB_ik_one_D.Checked = false;
            Form_Basic.form.CB_ik_one_E.Checked = false;
            Form_Basic.form.CB_ik_one_F.Checked = false;
            Form_Basic.form.CB_ik_one_G.Checked = false;
            Form_Basic.form.CB_ik_one_H.Checked = false;
            Form_Basic.form.CB_ik_one_I.Checked = false;

            Properties.Settings.Default.CB_ik_one_A = false;
            Properties.Settings.Default.CB_ik_one_B = false;
            Properties.Settings.Default.CB_ik_one_C = false;
            Properties.Settings.Default.CB_ik_one_D = false;
            Properties.Settings.Default.CB_ik_one_E = false;
            Properties.Settings.Default.CB_ik_one_F = false;
            Properties.Settings.Default.CB_ik_one_G = false;
            Properties.Settings.Default.CB_ik_one_H = false;
            Properties.Settings.Default.CB_ik_one_I = false;

            Form_Basic.form.CB_sell_use_A.Checked = false;
            Form_Basic.form.CB_sell_use_B.Checked = false;
            Form_Basic.form.CB_sell_use_C.Checked = false;
            Form_Basic.form.CB_sell_use_D.Checked = false;
            Form_Basic.form.CB_sell_use_E.Checked = false;
            Form_Basic.form.CB_sell_use_F.Checked = false;

            Properties.Settings.Default.CB_sell_use_A = false;
            Properties.Settings.Default.CB_sell_use_B = false;
            Properties.Settings.Default.CB_sell_use_C = false;
            Properties.Settings.Default.CB_sell_use_D = false;
            Properties.Settings.Default.CB_sell_use_E = false;
            Properties.Settings.Default.CB_sell_use_F = false;

            Form_Basic.form.CB_sell_time_use.Checked = false;
            Form_Basic.form.CB_silson_use_W.Checked = false;
            Form_Basic.form.CB_예상손실_use.Checked = false;
            Form_Basic.form.CB_예상수익사용.Checked = false;

            Properties.Settings.Default.CB_sell_time_use = false;
            Properties.Settings.Default.CB_silson_use_W = false;
            Properties.Settings.Default.CB_예상손실_use = false;
            Properties.Settings.Default.CB_예상수익사용 = false;

            Form_Basic.form.CB_scalping.Checked = false;

            Properties.Settings.Default.CB_scalping = false;

            Form_Basic.form.CB_scalping_A.Checked = false;
            Form_Basic.form.CB_scalping_B.Checked = false;
            Form_Basic.form.CB_scalping_C.Checked = false;

            Properties.Settings.Default.CB_scalping_A = false;
            Properties.Settings.Default.CB_scalping_B = false;
            Properties.Settings.Default.CB_scalping_C = false;

            Form_Basic.form.CB_TimeSell_A.Checked = false;
            Form_Basic.form.CB_TimeSell_B.Checked = false;
            Form_Basic.form.CB_TimeSell_C.Checked = false;

            Properties.Settings.Default.CB_TimeSell_A = false;
            Properties.Settings.Default.CB_TimeSell_B = false;
            Properties.Settings.Default.CB_TimeSell_C = false;
        }
        private static void 패널감추기_특수매매()
        {
            Form_Special.form.P_조건별매매그룹지정.Hide();
            Form_Special.form.LB_예약리스트.Hide();
            Form_Special.form.P_예약주문.Hide();

            Form_Special.form.P_매매기간주문.Location = new System.Drawing.Point(1, 131);

            Form_Special.form.CBB_In_group_A.SelectedIndex = 0;
            Form_Special.form.CBB_In_group_B.SelectedIndex = 0;
            Form_Special.form.CBB_In_group_C.SelectedIndex = 0;
            Form_Special.form.CBB_In_group_D.SelectedIndex = 0;

            Properties.Settings.Default.CBB_In_group_A = 0;
            Properties.Settings.Default.CBB_In_group_B = 0;
            Properties.Settings.Default.CBB_In_group_C = 0;
            Properties.Settings.Default.CBB_In_group_D = 0;
        }

        ///////////////////////       계좌설정
        private static void 계좌설정_(int 시작시간, int 종료시간, int 최대잔고, bool 매수제한, double 매수제한매입비, int 매수사용법, bool 추매제한, double 추매제한매입비, int 지수연동_신규, int 지수연동_추매)
        {
            Form1.form1.CB_미체결취소.Checked = false;

            Form1.form1.TB_starttime.Text = 시작시간.ToString();
            Form1.form1.TB_stoptime.Text = 종료시간.ToString();
            Form1.form1.CBB_지수연동_신규.SelectedIndex = 지수연동_신규;
            Form1.form1.CBB_지수연동_추매.SelectedIndex = 지수연동_추매;

            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Form1.form1.TB_setjango.Text = 최대잔고.ToString();
                Form1.form1.CB_계좌매입비_매수제한.Checked = 매수제한;
                Form1.form1.TB_계좌매입비_제한비중.Text = 매수제한매입비.ToString();
                Form1.form1.CBB_계좌매입비_제한선택.SelectedIndex = 매수사용법;
                Form1.form1.CB_잔고매입비_추매제한.Checked = 추매제한;
                Form1.form1.TB_잔고매입비_추매제한.Text = 추매제한매입비.ToString();

                Properties.Settings.Default.TB_setjango = 최대잔고;
                Properties.Settings.Default.CB_계좌매입비_매수제한 = 매수제한;
                Properties.Settings.Default.TB_계좌매입비_제한비중 = 매수제한매입비;
                Properties.Settings.Default.CBB_계좌매입비_제한선택 = 매수사용법;
                Properties.Settings.Default.CB_잔고매입비_추매제한 = 추매제한;
                Properties.Settings.Default.TB_잔고매입비_추매제한 = 추매제한매입비;
            }

            Properties.Settings.Default.CB_미체결취소 = false;
            Properties.Settings.Default.MT_starttime = 시작시간;
            Properties.Settings.Default.MT_stoptime = 종료시간;
            Properties.Settings.Default.CBB_지수연동_신규 = 지수연동_신규;
            Properties.Settings.Default.CBB_지수연동_추매 = 지수연동_추매;
        }
        private static void 계좌설정_코스피(double 등락률, int 등락률상하, int 등락률사용법, double 고가대비등락률, int 고가대비상하, int 고가대비사용법, double 저가대비등락률, int 저가대비상하, int 저가대비사용법)
        {
            Form1.form1.TB_p_ratio_use.Text = 등락률.ToString();
            Form1.form1.combo_p_ratio_UD.SelectedIndex = 등락률상하;
            Form1.form1.combo_p_ratio.SelectedIndex = 등락률사용법;
            Form1.form1.TB_p_go_use.Text = 고가대비등락률.ToString();
            Form1.form1.combo_p_go_UD.SelectedIndex = 고가대비상하;
            Form1.form1.combo_p_go.SelectedIndex = 고가대비사용법;
            Form1.form1.TB_p_down_use.Text = 저가대비등락률.ToString();
            Form1.form1.combo_p_down_UD.SelectedIndex = 저가대비상하;
            Form1.form1.combo_p_down.SelectedIndex = 저가대비사용법;

            Properties.Settings.Default.TB_p_ratio_use = 등락률;
            Properties.Settings.Default.combo_p_ratio_UD = 등락률상하;
            Properties.Settings.Default.combo_p_ratio = 등락률사용법;
            Properties.Settings.Default.TB_p_go_use = 고가대비등락률;
            Properties.Settings.Default.combo_p_go_UD = 고가대비상하;
            Properties.Settings.Default.combo_p_go = 고가대비사용법;
            Properties.Settings.Default.TB_p_down_use = 저가대비등락률;
            Properties.Settings.Default.combo_p_down_UD = 저가대비상하;
            Properties.Settings.Default.combo_p_down = 저가대비사용법;
        }
        private static void 계좌설정_코스닥(double 등락률, int 등락률상하, int 등락률사용법, double 고가대비등락률, int 고가대비상하, int 고가대비사용법, double 저가대비등락률, int 저가대비상하, int 저가대비사용법)
        {
            Form1.form1.TB_q_ratio_use.Text = 등락률.ToString();
            Form1.form1.combo_q_ratio_UD.SelectedIndex = 등락률상하;
            Form1.form1.combo_q_ratio.SelectedIndex = 등락률사용법;
            Form1.form1.TB_q_go_use.Text = 고가대비등락률.ToString();
            Form1.form1.combo_q_go_UD.SelectedIndex = 고가대비상하;
            Form1.form1.combo_q_go.SelectedIndex = 고가대비사용법;
            Form1.form1.TB_q_down_use.Text = 저가대비등락률.ToString();
            Form1.form1.combo_q_down_UD.SelectedIndex = 저가대비상하;
            Form1.form1.combo_q_down.SelectedIndex = 저가대비사용법;

            Properties.Settings.Default.TB_q_ratio_use = 등락률;
            Properties.Settings.Default.combo_q_ratio_UD = 등락률상하;
            Properties.Settings.Default.combo_q_ratio = 등락률사용법;
            Properties.Settings.Default.TB_q_go_use = 고가대비등락률;
            Properties.Settings.Default.combo_q_go_UD = 고가대비상하;
            Properties.Settings.Default.combo_q_go = 고가대비사용법;
            Properties.Settings.Default.TB_q_down_use = 저가대비등락률;
            Properties.Settings.Default.combo_q_down_UD = 저가대비상하;
            Properties.Settings.Default.combo_q_down = 저가대비사용법;
        }
        private static void 계좌설정_미수사용(bool 사용, int 정리시간, int 사용법, int 회당주문금액, int 주문값, int 주문방법, int 반복시간)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Form1.form1.CB_misu.Checked = 사용;
                Form1.form1.MT_misu_time.Text = 정리시간.ToString();
                Form1.form1.Combo_misu.SelectedIndex = 사용법;
                Form1.form1.TB_misu_ratio.Text = 회당주문금액.ToString();
                Form1.form1.TB_misu_value.Text = 주문값.ToString();
                Form1.form1.Combo_misu_jumnun.SelectedIndex = 주문방법;
                Form1.form1.TB_misu_repeat_time.Text = 반복시간.ToString();

                Properties.Settings.Default.CB_misu = 사용;
                Properties.Settings.Default.MT_misu_time = 정리시간;
                Properties.Settings.Default.Combo_misu = 사용법;
                Properties.Settings.Default.TB_misu_ratio = 회당주문금액;
                Properties.Settings.Default.TB_misu_value = 주문값;
                Properties.Settings.Default.Combo_misu_jumnun = 주문방법;
                Properties.Settings.Default.TB_misu_repeat_time = 반복시간;
            }
        }

        private static void 계좌설정_이평선(bool kospi_new_stop, bool kospi_add_stop,
            bool use_kospi_min_03, bool use_kospi_min_05, bool use_kospi_min_10, bool use_kospi_min_20, bool use_kospi_min_30, bool use_kospi_min_60,
            bool use_kospi_day_03, bool use_kospi_day_05, bool use_kospi_day_10, bool use_kospi_day_20, bool use_kospi_day_40, bool use_kospi_day_60,
            bool UD_kospi_min_03, bool UD_kospi_min_05, bool UD_kospi_min_10, bool UD_kospi_min_20, bool UD_kospi_min_30, bool UD_kospi_min_60,
            bool UD_kospi_day_03, bool UD_kospi_day_05, bool UD_kospi_day_10, bool UD_kospi_day_20, bool UD_kospi_day_40, bool UD_kospi_day_60,
            bool kosdaq_new_stop, bool kosdaq_add_stop,
            bool use_kosdaq_min_03, bool use_kosdaq_min_05, bool use_kosdaq_min_10, bool use_kosdaq_min_20, bool use_kosdaq_min_30, bool use_kosdaq_min_60,
            bool use_kosdaq_day_03, bool use_kosdaq_day_05, bool use_kosdaq_day_10, bool use_kosdaq_day_20, bool use_kosdaq_day_40, bool use_kosdaq_day_60,
            bool UD_kosdaq_min_03, bool UD_kosdaq_min_05, bool UD_kosdaq_min_10, bool UD_kosdaq_min_20, bool UD_kosdaq_min_30, bool UD_kosdaq_min_60,
            bool UD_kosdaq_day_03, bool UD_kosdaq_day_05, bool UD_kosdaq_day_10, bool UD_kosdaq_day_20, bool UD_kosdaq_day_40, bool UD_kosdaq_day_60)
        {
            Properties.Settings.Default.CB_kospi_new_stop = kospi_new_stop;
            Properties.Settings.Default.CB_kospi_add_stop = kospi_add_stop;
            Properties.Settings.Default.CB_use_kospi_min_03 = use_kospi_min_03;
            Properties.Settings.Default.CB_use_kospi_min_05 = use_kospi_min_05;
            Properties.Settings.Default.CB_use_kospi_min_10 = use_kospi_min_10;
            Properties.Settings.Default.CB_use_kospi_min_20 = use_kospi_min_20;
            Properties.Settings.Default.CB_use_kospi_min_30 = use_kospi_min_30;
            Properties.Settings.Default.CB_use_kospi_min_60 = use_kospi_min_60;
            Properties.Settings.Default.CB_use_kospi_day_03 = use_kospi_day_03;
            Properties.Settings.Default.CB_use_kospi_day_05 = use_kospi_day_05;
            Properties.Settings.Default.CB_use_kospi_day_10 = use_kospi_day_10;
            Properties.Settings.Default.CB_use_kospi_day_20 = use_kospi_day_20;
            Properties.Settings.Default.CB_use_kospi_day_40 = use_kospi_day_40;
            Properties.Settings.Default.CB_use_kospi_day_60 = use_kospi_day_60;
            Properties.Settings.Default.CB_UD_kospi_min_03 = UD_kospi_min_03;
            Properties.Settings.Default.CB_UD_kospi_min_05 = UD_kospi_min_05;
            Properties.Settings.Default.CB_UD_kospi_min_10 = UD_kospi_min_10;
            Properties.Settings.Default.CB_UD_kospi_min_20 = UD_kospi_min_20;
            Properties.Settings.Default.CB_UD_kospi_min_30 = UD_kospi_min_30;
            Properties.Settings.Default.CB_UD_kospi_min_60 = UD_kospi_min_60;
            Properties.Settings.Default.CB_UD_kospi_day_03 = UD_kospi_day_03;
            Properties.Settings.Default.CB_UD_kospi_day_05 = UD_kospi_day_05;
            Properties.Settings.Default.CB_UD_kospi_day_10 = UD_kospi_day_10;
            Properties.Settings.Default.CB_UD_kospi_day_20 = UD_kospi_day_20;
            Properties.Settings.Default.CB_UD_kospi_day_40 = UD_kospi_day_40;
            Properties.Settings.Default.CB_UD_kospi_day_60 = UD_kospi_day_60;

            Form1.AVG_jisu[0].new_stop = kospi_new_stop;
            Form1.AVG_jisu[0].add_stop = kospi_add_stop;
            Form1.AVG_jisu[0].use_min_03 = use_kospi_min_03;
            Form1.AVG_jisu[0].use_min_05 = use_kospi_min_05;
            Form1.AVG_jisu[0].use_min_10 = use_kospi_min_10;
            Form1.AVG_jisu[0].use_min_20 = use_kospi_min_20;
            Form1.AVG_jisu[0].use_min_30 = use_kospi_min_30;
            Form1.AVG_jisu[0].use_min_60 = use_kospi_min_60;
            Form1.AVG_jisu[0].use_day_03 = use_kospi_day_03;
            Form1.AVG_jisu[0].use_day_05 = use_kospi_day_05;
            Form1.AVG_jisu[0].use_day_10 = use_kospi_day_10;
            Form1.AVG_jisu[0].use_day_20 = use_kospi_day_20;
            Form1.AVG_jisu[0].use_day_40 = use_kospi_day_40;
            Form1.AVG_jisu[0].use_day_60 = use_kospi_day_60;
            Form1.AVG_jisu[0].UD_min_03 = UD_kospi_min_03;
            Form1.AVG_jisu[0].UD_min_05 = UD_kospi_min_05;
            Form1.AVG_jisu[0].UD_min_10 = UD_kospi_min_10;
            Form1.AVG_jisu[0].UD_min_20 = UD_kospi_min_20;
            Form1.AVG_jisu[0].UD_min_30 = UD_kospi_min_30;
            Form1.AVG_jisu[0].UD_min_60 = UD_kospi_min_60;
            Form1.AVG_jisu[0].UD_day_03 = UD_kospi_day_03;
            Form1.AVG_jisu[0].UD_day_05 = UD_kospi_day_05;
            Form1.AVG_jisu[0].UD_day_10 = UD_kospi_day_10;
            Form1.AVG_jisu[0].UD_day_20 = UD_kospi_day_20;
            Form1.AVG_jisu[0].UD_day_40 = UD_kospi_day_40;
            Form1.AVG_jisu[0].UD_day_60 = UD_kospi_day_60;

            Properties.Settings.Default.CB_kosdaq_new_stop = kosdaq_new_stop;
            Properties.Settings.Default.CB_kosdaq_add_stop = kosdaq_add_stop;
            Properties.Settings.Default.CB_use_kosdaq_min_03 = use_kosdaq_min_03;
            Properties.Settings.Default.CB_use_kosdaq_min_05 = use_kosdaq_min_05;
            Properties.Settings.Default.CB_use_kosdaq_min_10 = use_kosdaq_min_10;
            Properties.Settings.Default.CB_use_kosdaq_min_20 = use_kosdaq_min_20;
            Properties.Settings.Default.CB_use_kosdaq_min_30 = use_kosdaq_min_30;
            Properties.Settings.Default.CB_use_kosdaq_min_60 = use_kosdaq_min_60;
            Properties.Settings.Default.CB_use_kosdaq_day_03 = use_kosdaq_day_03;
            Properties.Settings.Default.CB_use_kosdaq_day_05 = use_kosdaq_day_05;
            Properties.Settings.Default.CB_use_kosdaq_day_10 = use_kosdaq_day_10;
            Properties.Settings.Default.CB_use_kosdaq_day_20 = use_kosdaq_day_20;
            Properties.Settings.Default.CB_use_kosdaq_day_40 = use_kosdaq_day_40;
            Properties.Settings.Default.CB_use_kosdaq_day_60 = use_kosdaq_day_60;
            Properties.Settings.Default.CB_UD_kosdaq_min_03 = UD_kosdaq_min_03;
            Properties.Settings.Default.CB_UD_kosdaq_min_05 = UD_kosdaq_min_05;
            Properties.Settings.Default.CB_UD_kosdaq_min_10 = UD_kosdaq_min_10;
            Properties.Settings.Default.CB_UD_kosdaq_min_20 = UD_kosdaq_min_20;
            Properties.Settings.Default.CB_UD_kosdaq_min_30 = UD_kosdaq_min_30;
            Properties.Settings.Default.CB_UD_kosdaq_min_60 = UD_kosdaq_min_60;
            Properties.Settings.Default.CB_UD_kosdaq_day_03 = UD_kosdaq_day_03;
            Properties.Settings.Default.CB_UD_kosdaq_day_05 = UD_kosdaq_day_05;
            Properties.Settings.Default.CB_UD_kosdaq_day_10 = UD_kosdaq_day_10;
            Properties.Settings.Default.CB_UD_kosdaq_day_20 = UD_kosdaq_day_20;
            Properties.Settings.Default.CB_UD_kosdaq_day_40 = UD_kosdaq_day_40;
            Properties.Settings.Default.CB_UD_kosdaq_day_60 = UD_kosdaq_day_60;

            Form1.AVG_jisu[1].new_stop = kosdaq_new_stop;
            Form1.AVG_jisu[1].add_stop = kosdaq_add_stop;
            Form1.AVG_jisu[1].use_min_03 = use_kosdaq_min_03;
            Form1.AVG_jisu[1].use_min_05 = use_kosdaq_min_05;
            Form1.AVG_jisu[1].use_min_10 = use_kosdaq_min_10;
            Form1.AVG_jisu[1].use_min_20 = use_kosdaq_min_20;
            Form1.AVG_jisu[1].use_min_30 = use_kosdaq_min_30;
            Form1.AVG_jisu[1].use_min_60 = use_kosdaq_min_60;
            Form1.AVG_jisu[1].use_day_03 = use_kosdaq_day_03;
            Form1.AVG_jisu[1].use_day_05 = use_kosdaq_day_05;
            Form1.AVG_jisu[1].use_day_10 = use_kosdaq_day_10;
            Form1.AVG_jisu[1].use_day_20 = use_kosdaq_day_20;
            Form1.AVG_jisu[1].use_day_40 = use_kosdaq_day_40;
            Form1.AVG_jisu[1].use_day_60 = use_kosdaq_day_60;
            Form1.AVG_jisu[1].UD_min_03 = UD_kosdaq_min_03;
            Form1.AVG_jisu[1].UD_min_05 = UD_kosdaq_min_05;
            Form1.AVG_jisu[1].UD_min_10 = UD_kosdaq_min_10;
            Form1.AVG_jisu[1].UD_min_20 = UD_kosdaq_min_20;
            Form1.AVG_jisu[1].UD_min_30 = UD_kosdaq_min_30;
            Form1.AVG_jisu[1].UD_min_60 = UD_kosdaq_min_60;
            Form1.AVG_jisu[1].UD_day_03 = UD_kosdaq_day_03;
            Form1.AVG_jisu[1].UD_day_05 = UD_kosdaq_day_05;
            Form1.AVG_jisu[1].UD_day_10 = UD_kosdaq_day_10;
            Form1.AVG_jisu[1].UD_day_20 = UD_kosdaq_day_20;
            Form1.AVG_jisu[1].UD_day_40 = UD_kosdaq_day_40;
            Form1.AVG_jisu[1].UD_day_60 = UD_kosdaq_day_60;
        }

        ///////////////////////       기본매매
        private static void 기본매매_신규횟수제한(bool 사용, int 횟수제한)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.TB_신규횟수제한 = 횟수제한;
                Properties.Settings.Default.CB_신규횟수제한 = 사용;
            }
        }

        private static void 기본매매_new_A(bool 재진입, int 유지, int 취소시간, int 취소후, int 반복횟수, bool 사용, int 검색식사용법, string 검색식, int 시작시간, int 종료시간, double 비중, int 비중방법, int 주문값, int 주문방법)
        {
            신규_A = new string[] { 사용.ToString(), 검색식사용법.ToString(), 검색식 };
            검색식_유무확인(사용, "신규매수_A", 검색식);

            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_new_recatch_A = 재진입;
                Properties.Settings.Default.MTB_new_delay_A = 유지;
                Properties.Settings.Default.MTB_new_canceltime_A = 취소시간;
                Properties.Settings.Default.combo_new_cancel_buy_A = 취소후;
                Properties.Settings.Default.MTB_new_repeat_A = 반복횟수;
                Properties.Settings.Default.CB_new_A = 사용;
                Properties.Settings.Default.combo_new_or_A = 검색식사용법;
                Properties.Settings.Default.combo_new_condition_A = 검색식;
                Properties.Settings.Default.MT_new_start_A = 시작시간;
                Properties.Settings.Default.MT_new_end_A = 종료시간;
                Properties.Settings.Default.combo_new_choice_A = 비중방법;
                Properties.Settings.Default.TB_new_value_A = 주문값;
                Properties.Settings.Default.combo_new_jumun_A = 주문방법;
                Properties.Settings.Default.MT_new_ratio_A = 비중;
            }
        }
        private static void 기본매매_new_B(bool 재진입, int 유지, int 취소시간, int 취소후, int 반복횟수, bool 사용, int 검색식사용법, string 검색식, int 시작시간, int 종료시간, double 비중, int 비중방법, int 주문값, int 주문방법)
        {
            신규_B = new string[] { 사용.ToString(), 검색식사용법.ToString(), 검색식 };
            검색식_유무확인(사용, "신규매수_B", 검색식);

            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_new_recatch_B = 재진입;
                Properties.Settings.Default.MTB_new_delay_B = 유지;
                Properties.Settings.Default.MTB_new_canceltime_B = 취소시간;
                Properties.Settings.Default.combo_new_cancel_buy_B = 취소후;
                Properties.Settings.Default.MTB_new_repeat_B = 반복횟수;
                Properties.Settings.Default.CB_new_B = 사용;
                Properties.Settings.Default.combo_new_or_B = 검색식사용법;
                Properties.Settings.Default.combo_new_condition_B = 검색식;
                Properties.Settings.Default.MT_new_start_B = 시작시간;
                Properties.Settings.Default.MT_new_end_B = 종료시간;
                Properties.Settings.Default.combo_new_choice_B = 비중방법;
                Properties.Settings.Default.TB_new_value_B = 주문값;
                Properties.Settings.Default.combo_new_jumun_B = 주문방법;
                Properties.Settings.Default.MT_new_ratio_B = 비중;
            }
        }
        private static void 기본매매_new_C(bool 재진입, int 유지, int 취소시간, int 취소후, int 반복횟수, bool 사용, int 검색식사용법, string 검색식, int 시작시간, int 종료시간, double 비중, int 비중방법, int 주문값, int 주문방법)
        {
            신규_C = new string[] { 사용.ToString(), 검색식사용법.ToString(), 검색식 };
            검색식_유무확인(사용, "신규매수_C", 검색식);

            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_new_recatch_C = 재진입;
                Properties.Settings.Default.MTB_new_delay_C = 유지;
                Properties.Settings.Default.MTB_new_canceltime_C = 취소시간;
                Properties.Settings.Default.combo_new_cancel_buy_C = 취소후;
                Properties.Settings.Default.MTB_new_repeat_C = 반복횟수;
                Properties.Settings.Default.CB_new_C = 사용;
                Properties.Settings.Default.combo_new_or_C = 검색식사용법;
                Properties.Settings.Default.combo_new_condition_C = 검색식;
                Properties.Settings.Default.MT_new_start_C = 시작시간;
                Properties.Settings.Default.MT_new_end_C = 종료시간;
                Properties.Settings.Default.combo_new_choice_C = 비중방법;
                Properties.Settings.Default.TB_new_value_C = 주문값;
                Properties.Settings.Default.combo_new_jumun_C = 주문방법;
                Properties.Settings.Default.MT_new_ratio_C = 비중;
            }
        }
        private static void 기본매매_신규매수조건(int 신규주가이상, int 신규주가이하, double 신규등락률이상, double 신규등락률이하, int 추가매수딜레이, bool 재매수, int 재매수_지연시간)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.TB_신규주가이상 = 신규주가이상;
                Properties.Settings.Default.TB_신규주가이하 = 신규주가이하;
                Properties.Settings.Default.TB_신규등락률이상 = 신규등락률이상;
                Properties.Settings.Default.TB_신규등락률이하 = 신규등락률이하;
                Properties.Settings.Default.MTB_추가매수딜레이 = 추가매수딜레이;
                Properties.Settings.Default.CB_new_rebuy = 재매수;
                Properties.Settings.Default.MTB_new_rebuytime = 재매수_지연시간;
            }
        }
        private static void 기본매매_신규매수_세부조건(int 잔고개수_신규A, int 잔고개수_신규B, int 잔고개수_신규C, int 재진입제한_A, int 재진입제한_B, int 재진입제한_C, bool 익절재매수A, bool 익절재매수B, bool 익절재매수C)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.TB_잔고개수_신규A = 잔고개수_신규A;
                Properties.Settings.Default.TB_잔고개수_신규B = 잔고개수_신규B;
                Properties.Settings.Default.TB_잔고개수_신규C = 잔고개수_신규C;
                Properties.Settings.Default.TB_Limit_New_A = 재진입제한_A;
                Properties.Settings.Default.TB_Limit_New_B = 재진입제한_B;
                Properties.Settings.Default.TB_Limit_New_C = 재진입제한_C;
                Properties.Settings.Default.CB_익절재매수A = 익절재매수A;
                Properties.Settings.Default.CB_익절재매수B = 익절재매수B;
                Properties.Settings.Default.CB_익절재매수C = 익절재매수C;
            }
        }

        private static void 기본매매_트레일링스탑_조건(bool 손실제한, bool 취소후, bool 기준금, int canceltime, int cancel_sell, int repeat)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_TS_손실제한 = 손실제한;
                Properties.Settings.Default.CB_TS_취소후 = 취소후;
                Properties.Settings.Default.CB_TS_기준금 = 기준금;
                Properties.Settings.Default.MTB_TS_canceltime = canceltime;
                Properties.Settings.Default.CBB_TS_cancel_sell = cancel_sell;
                Properties.Settings.Default.MTB_TS_repeat = repeat;
            }
        }

        private static void 기본매매_트레일링스탑_A(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_TS_A = CB_TS;
                Properties.Settings.Default.TB_TS_upper_A = TB_TS_upper;
                Properties.Settings.Default.CBB_TS_upper_A = CBB_TS_upper;
                Properties.Settings.Default.TB_TS_down_A = TB_TS_down;
                Properties.Settings.Default.TB_TS_ratio_A = TB_TS_ratio;
                Properties.Settings.Default.CBB_TS_ratio_A = CBB_TS_ratio;
                Properties.Settings.Default.TB_TS_Jumun_A = TB_TS_Jumun;
                Properties.Settings.Default.CBB_TS_Jumun_A = CBB_TS_Jumun;
            }
        }
        private static void 기본매매_트레일링스탑_B(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_TS_B = CB_TS;
                Properties.Settings.Default.TB_TS_upper_B = TB_TS_upper;
                Properties.Settings.Default.CBB_TS_upper_B = CBB_TS_upper;
                Properties.Settings.Default.TB_TS_down_B = TB_TS_down;
                Properties.Settings.Default.TB_TS_ratio_B = TB_TS_ratio;
                Properties.Settings.Default.CBB_TS_ratio_B = CBB_TS_ratio;
                Properties.Settings.Default.TB_TS_Jumun_B = TB_TS_Jumun;
                Properties.Settings.Default.CBB_TS_Jumun_B = CBB_TS_Jumun;
            }
        }
        private static void 기본매매_트레일링스탑_C(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_TS_C = CB_TS;
                Properties.Settings.Default.TB_TS_upper_C = TB_TS_upper;
                Properties.Settings.Default.CBB_TS_upper_C = CBB_TS_upper;
                Properties.Settings.Default.TB_TS_down_C = TB_TS_down;
                Properties.Settings.Default.TB_TS_ratio_C = TB_TS_ratio;
                Properties.Settings.Default.CBB_TS_ratio_C = CBB_TS_ratio;
                Properties.Settings.Default.TB_TS_Jumun_C = TB_TS_Jumun;
                Properties.Settings.Default.CBB_TS_Jumun_C = CBB_TS_Jumun;
            }
        }
        private static void 기본매매_트레일링스탑_D(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_TS_D = CB_TS;
                Properties.Settings.Default.TB_TS_upper_D = TB_TS_upper;
                Properties.Settings.Default.CBB_TS_upper_D = CBB_TS_upper;
                Properties.Settings.Default.TB_TS_down_D = TB_TS_down;
                Properties.Settings.Default.TB_TS_ratio_D = TB_TS_ratio;
                Properties.Settings.Default.CBB_TS_ratio_D = CBB_TS_ratio;
                Properties.Settings.Default.TB_TS_Jumun_D = TB_TS_Jumun;
                Properties.Settings.Default.CBB_TS_Jumun_D = CBB_TS_Jumun;
            }
        }
        private static void 기본매매_트레일링스탑_E(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_TS_E = CB_TS;
                Properties.Settings.Default.TB_TS_upper_E = TB_TS_upper;
                Properties.Settings.Default.CBB_TS_upper_E = CBB_TS_upper;
                Properties.Settings.Default.TB_TS_down_E = TB_TS_down;
                Properties.Settings.Default.TB_TS_ratio_E = TB_TS_ratio;
                Properties.Settings.Default.CBB_TS_ratio_E = CBB_TS_ratio;
                Properties.Settings.Default.TB_TS_Jumun_E = TB_TS_Jumun;
                Properties.Settings.Default.CBB_TS_Jumun_E = CBB_TS_Jumun;
            }
        }
        private static void 기본매매_트레일링스탑_F(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_TS_F = CB_TS;
                Properties.Settings.Default.TB_TS_upper_F = TB_TS_upper;
                Properties.Settings.Default.CBB_TS_upper_F = CBB_TS_upper;
                Properties.Settings.Default.TB_TS_down_F = TB_TS_down;
                Properties.Settings.Default.TB_TS_ratio_F = TB_TS_ratio;
                Properties.Settings.Default.CBB_TS_ratio_F = CBB_TS_ratio;
                Properties.Settings.Default.TB_TS_Jumun_F = TB_TS_Jumun;
                Properties.Settings.Default.CBB_TS_Jumun_F = CBB_TS_Jumun;
            }
        }
        private static void 기본매매_트레일링스탑_G(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_TS_G = CB_TS;
                Properties.Settings.Default.TB_TS_upper_G = TB_TS_upper;
                Properties.Settings.Default.CBB_TS_upper_G = CBB_TS_upper;
                Properties.Settings.Default.TB_TS_down_G = TB_TS_down;
                Properties.Settings.Default.TB_TS_ratio_G = TB_TS_ratio;
                Properties.Settings.Default.CBB_TS_ratio_G = CBB_TS_ratio;
                Properties.Settings.Default.TB_TS_Jumun_G = TB_TS_Jumun;
                Properties.Settings.Default.CBB_TS_Jumun_G = CBB_TS_Jumun;
            }
        }
        private static void 기본매매_트레일링스탑_H(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_TS_H = CB_TS;
                Properties.Settings.Default.TB_TS_upper_H = TB_TS_upper;
                Properties.Settings.Default.CBB_TS_upper_H = CBB_TS_upper;
                Properties.Settings.Default.TB_TS_down_H = TB_TS_down;
                Properties.Settings.Default.TB_TS_ratio_H = TB_TS_ratio;
                Properties.Settings.Default.CBB_TS_ratio_H = CBB_TS_ratio;
                Properties.Settings.Default.TB_TS_Jumun_H = TB_TS_Jumun;
                Properties.Settings.Default.CBB_TS_Jumun_H = CBB_TS_Jumun;
            }
        }
        private static void 기본매매_트레일링스탑_I(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_TS_I = CB_TS;
                Properties.Settings.Default.TB_TS_upper_I = TB_TS_upper;
                Properties.Settings.Default.CBB_TS_upper_I = CBB_TS_upper;
                Properties.Settings.Default.TB_TS_down_I = TB_TS_down;
                Properties.Settings.Default.TB_TS_ratio_I = TB_TS_ratio;
                Properties.Settings.Default.CBB_TS_ratio_I = CBB_TS_ratio;
                Properties.Settings.Default.TB_TS_Jumun_I = TB_TS_Jumun;
                Properties.Settings.Default.CBB_TS_Jumun_I = CBB_TS_Jumun;
            }
        }
        ///////////////////////       반복매매

        private static void 반복기준금(bool 기준금)
        {
            Properties.Settings.Default.CB_Repeat_기준금 = 기준금;
        }

        private static void 반복매매_A(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                        double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                        int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_A = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_A", 검색식);

            Properties.Settings.Default.CB_repeat_use_A = 사용;
            Properties.Settings.Default.MT_repeat_time_start_A = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_A = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_A = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_A = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_A = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_A = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_A = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_A = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_A = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_A = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_A = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_A = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_A = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_A = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_A = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_A = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_A = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_A = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_A = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_A = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_A = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_A = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_A = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_A = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_A = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_A = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_A = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_A = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_A = 반복시간;
            Properties.Settings.Default.TB_repeat_value_A = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_A = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_A = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_A = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_A = 재주문;
        }
        private static void 반복매매_B(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                     double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                     int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_B = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_B", 검색식);

            Properties.Settings.Default.CB_repeat_use_B = 사용;
            Properties.Settings.Default.MT_repeat_time_start_B = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_B = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_B = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_B = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_B = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_B = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_B = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_B = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_B = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_B = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_B = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_B = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_B = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_B = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_B = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_B = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_B = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_B = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_B = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_B = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_B = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_B = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_B = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_B = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_B = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_B = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_B = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_B = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_B = 반복시간;
            Properties.Settings.Default.TB_repeat_value_B = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_B = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_B = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_B = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_B = 재주문;
        }
        private static void 반복매매_C(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                    double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                    int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_C = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_C", 검색식);

            Properties.Settings.Default.CB_repeat_use_C = 사용;
            Properties.Settings.Default.MT_repeat_time_start_C = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_C = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_C = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_C = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_C = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_C = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_C = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_C = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_C = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_C = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_C = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_C = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_C = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_C = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_C = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_C = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_C = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_C = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_C = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_C = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_C = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_C = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_C = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_C = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_C = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_C = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_C = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_C = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_C = 반복시간;
            Properties.Settings.Default.TB_repeat_value_C = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_C = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_C = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_C = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_C = 재주문;
        }

        private static void 반복매매_D(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                    double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                    int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_D = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_D", 검색식);

            Properties.Settings.Default.CB_repeat_use_D = 사용;
            Properties.Settings.Default.MT_repeat_time_start_D = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_D = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_D = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_D = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_D = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_D = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_D = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_D = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_D = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_D = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_D = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_D = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_D = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_D = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_D = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_D = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_D = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_D = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_D = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_D = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_D = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_D = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_D = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_D = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_D = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_D = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_D = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_D = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_D = 반복시간;
            Properties.Settings.Default.TB_repeat_value_D = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_D = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_D = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_D = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_D = 재주문;
        }
        private static void 반복매매_E(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                    double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                    int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_E = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_E", 검색식);

            Properties.Settings.Default.CB_repeat_use_E = 사용;
            Properties.Settings.Default.MT_repeat_time_start_E = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_E = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_E = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_E = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_E = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_E = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_E = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_E = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_E = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_E = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_E = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_E = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_E = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_E = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_E = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_E = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_E = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_E = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_E = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_E = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_E = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_E = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_E = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_E = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_E = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_E = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_E = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_E = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_E = 반복시간;
            Properties.Settings.Default.TB_repeat_value_E = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_E = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_E = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_E = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_E = 재주문;
        }
        private static void 반복매매_F(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                    double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                    int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_F = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_F", 검색식);

            Properties.Settings.Default.CB_repeat_use_F = 사용;
            Properties.Settings.Default.MT_repeat_time_start_F = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_F = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_F = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_F = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_F = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_F = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_F = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_F = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_F = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_F = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_F = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_F = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_F = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_F = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_F = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_F = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_F = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_F = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_F = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_F = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_F = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_F = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_F = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_F = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_F = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_F = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_F = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_F = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_F = 반복시간;
            Properties.Settings.Default.TB_repeat_value_F = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_F = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_F = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_F = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_F = 재주문;
        }
        private static void 반복매매_G(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                    double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                    int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_G = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_G", 검색식);

            Properties.Settings.Default.CB_repeat_use_G = 사용;
            Properties.Settings.Default.MT_repeat_time_start_G = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_G = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_G = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_G = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_G = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_G = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_G = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_G = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_G = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_G = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_G = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_G = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_G = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_G = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_G = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_G = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_G = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_G = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_G = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_G = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_G = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_G = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_G = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_G = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_G = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_G = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_G = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_G = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_G = 반복시간;
            Properties.Settings.Default.TB_repeat_value_G = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_G = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_G = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_G = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_G = 재주문;
        }
        private static void 반복매매_H(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                    double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                    int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_H = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_H", 검색식);

            Properties.Settings.Default.CB_repeat_use_H = 사용;
            Properties.Settings.Default.MT_repeat_time_start_H = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_H = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_H = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_H = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_H = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_H = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_H = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_H = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_H = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_H = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_H = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_H = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_H = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_H = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_H = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_H = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_H = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_H = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_H = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_H = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_H = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_H = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_H = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_H = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_H = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_H = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_H = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_H = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_H = 반복시간;
            Properties.Settings.Default.TB_repeat_value_H = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_H = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_H = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_H = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_H = 재주문;
        }
        private static void 반복매매_I(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                    double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                    int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_I = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_I", 검색식);

            Properties.Settings.Default.CB_repeat_use_I = 사용;
            Properties.Settings.Default.MT_repeat_time_start_I = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_I = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_I = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_I = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_I = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_I = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_I = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_I = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_I = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_I = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_I = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_I = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_I = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_I = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_I = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_I = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_I = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_I = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_I = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_I = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_I = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_I = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_I = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_I = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_I = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_I = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_I = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_I = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_I = 반복시간;
            Properties.Settings.Default.TB_repeat_value_I = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_I = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_I = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_I = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_I = 재주문;
        }
        private static void 반복매매_J(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                    double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                    int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_J = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_J", 검색식);

            Properties.Settings.Default.CB_repeat_use_J = 사용;
            Properties.Settings.Default.MT_repeat_time_start_J = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_J = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_J = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_J = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_J = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_J = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_J = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_J = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_J = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_J = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_J = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_J = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_J = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_J = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_J = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_J = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_J = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_J = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_J = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_J = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_J = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_J = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_J = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_J = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_J = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_J = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_J = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_J = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_J = 반복시간;
            Properties.Settings.Default.TB_repeat_value_J = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_J = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_J = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_J = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_J = 재주문;
        }
        private static void 반복매매_K(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                    double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                    int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_K = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_K", 검색식);

            Properties.Settings.Default.CB_repeat_use_K = 사용;
            Properties.Settings.Default.MT_repeat_time_start_K = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_K = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_K = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_K = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_K = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_K = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_K = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_K = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_K = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_K = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_K = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_K = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_K = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_K = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_K = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_K = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_K = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_K = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_K = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_K = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_K = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_K = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_K = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_K = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_K = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_K = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_K = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_K = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_K = 반복시간;
            Properties.Settings.Default.TB_repeat_value_K = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_K = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_K = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_K = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_K = 재주문;
        }
        private static void 반복매매_L(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                    double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                    int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_L = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_L", 검색식);

            Properties.Settings.Default.CB_repeat_use_L = 사용;
            Properties.Settings.Default.MT_repeat_time_start_L = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_L = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_L = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_L = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_L = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_L = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_L = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_L = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_L = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_L = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_L = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_L = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_L = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_L = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_L = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_L = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_L = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_L = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_L = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_L = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_L = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_L = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_L = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_L = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_L = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_L = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_L = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_L = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_L = 반복시간;
            Properties.Settings.Default.TB_repeat_value_L = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_L = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_L = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_L = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_L = 재주문;
        }
        private static void 반복매매_M(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                    double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                    int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_M = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_M", 검색식);

            Properties.Settings.Default.CB_repeat_use_M = 사용;
            Properties.Settings.Default.MT_repeat_time_start_M = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_M = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_M = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_M = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_M = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_M = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_M = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_M = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_M = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_M = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_M = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_M = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_M = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_M = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_M = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_M = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_M = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_M = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_M = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_M = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_M = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_M = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_M = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_M = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_M = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_M = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_M = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_M = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_M = 반복시간;
            Properties.Settings.Default.TB_repeat_value_M = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_M = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_M = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_M = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_M = 재주문;
        }
        private static void 반복매매_N(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                    double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                    int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취n주문, int 재주문)
        {
            반복_N = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "반복매매_N", 검색식);

            Properties.Settings.Default.CB_repeat_use_N = 사용;
            Properties.Settings.Default.MT_repeat_time_start_N = 시작시간;
            Properties.Settings.Default.MT_repeat_time_end_N = 종료시간;
            Properties.Settings.Default.CB_repeat_kind_N = 매매종류;
            Properties.Settings.Default.combo_repeat_use_condition_N = 검색식사용;
            Properties.Settings.Default.combo_repeat_condition_N = 검색식;
            Properties.Settings.Default.MTB_repeat_delay_N = 검색유지시간;
            Properties.Settings.Default.TB_repeat_매입금_N = 매입금;
            Properties.Settings.Default.TB_repeat_누적거래량_N = 누적거래량;
            Properties.Settings.Default.TB_repeat_누적거래대금_N = 누적거래대금;

            Properties.Settings.Default.TB_repeat_mma_N = TB_mma1;
            Properties.Settings.Default.CBB_repeat_mma_N = CBB_mma1;
            Properties.Settings.Default.TB_repeat_mma2_N = TB_mma2;
            Properties.Settings.Default.CBB_repeat_mma2_N = CBB_mma2;
            Properties.Settings.Default.CBB_repeat_mma_배열_N = CBB_mma_배열;
            Properties.Settings.Default.TB_repeat_dma1_N = TB_dma1;
            Properties.Settings.Default.CBB_repeat_dma1_N = CBB_dma1;
            Properties.Settings.Default.TB_repeat_dma2_N = TB_dma2;
            Properties.Settings.Default.CBB_repeat_dma2_N = CBB_dma2;
            Properties.Settings.Default.CBB_repeat_dma_배열_N = CBB_dma_배열;

            Properties.Settings.Default.TB_repeat_suik_1_N = 수익범위1;
            Properties.Settings.Default.CB_repeat_choice_N = 수익범위선택;
            Properties.Settings.Default.TB_repeat_suik_2_N = 수익범위2;
            Properties.Settings.Default.combo_repeat_suik_gubun_N = 수익구분;
            Properties.Settings.Default.TB_repeat_sell_ratio_N = 매수비중;
            Properties.Settings.Default.combo_repeat_sell_gubun_N = 매수구분;
            Properties.Settings.Default.TB_repeat_maemae_1_N = 매매범위1;
            Properties.Settings.Default.TB_repeat_maemae_2_N = 매매범위2;
            Properties.Settings.Default.combo_repeat_maemae_gubun_N = 매매범위기준;
            Properties.Settings.Default.MT_repeat_repeat_time_N = 반복시간;
            Properties.Settings.Default.TB_repeat_value_N = 주문가격;
            Properties.Settings.Default.combo_repeat_jumun_N = 주문구분;
            Properties.Settings.Default.MTB_repeat_Cancel_time_N = 취소시간;
            Properties.Settings.Default.combo_repeat_Cancel_N = 취n주문;
            Properties.Settings.Default.MTB_repeat_repeat_N = 재주문;
        }

        ///////////////////////       계좌관리
        private static void 계좌관리_추매조건(bool CB_총매수금, double 종목최대매수금, bool CB_일매수제한금, double 일매수제한금, bool CB_회수제한, int 회수제한,
                                            int 추매주가이상, int 추매주가이하, double 추매등락률이상, double 추매등락률이하)
        {
            Properties.Settings.Default.CB_총매수금 = CB_총매수금;
            Properties.Settings.Default.TB_총매수금 = 종목최대매수금;
            Properties.Settings.Default.CB_일매수제한금 = CB_일매수제한금;
            Properties.Settings.Default.TB_일매수제한금 = 일매수제한금;
            Properties.Settings.Default.CB_회수제한 = CB_회수제한;
            Properties.Settings.Default.TB_회수제한 = 회수제한;
            Properties.Settings.Default.TB_추매주가이상 = 추매주가이상;
            Properties.Settings.Default.TB_추매주가이하 = 추매주가이하;
            Properties.Settings.Default.TB_추매등락률이상 = 추매등락률이상;
            Properties.Settings.Default.TB_추매등락률이하 = 추매등락률이하;
        }
        private static void 계좌관리_분할주문(int 분할간격_A, int 분할횟수_A, int 분할간격_B, int 분할횟수_B, int 분할간격_C, int 분할횟수_C)
        {
            Properties.Settings.Default.TB_분할간격_A = 분할간격_A;
            Properties.Settings.Default.TB_분할간격_B = 분할간격_B;
            Properties.Settings.Default.TB_분할간격_C = 분할간격_C;
            Properties.Settings.Default.TB_분할횟수_A = 분할횟수_A;
            Properties.Settings.Default.TB_분할횟수_B = 분할횟수_B;
            Properties.Settings.Default.TB_분할횟수_C = 분할횟수_C;
        }
        private static void 계좌관리_기준비율관리(bool CB_매수기준, int TB_매수비율, bool CB_손익기준, int TB_손익비율)
        {
            Properties.Settings.Default.CB_매수기준 = CB_매수기준;
            Properties.Settings.Default.Today_매수기준금 = Properties.Settings.Default.MT_principal + "@" + Properties.Settings.Default.MT_principal;
            Properties.Settings.Default.CB_손익기준 = CB_손익기준;
            Properties.Settings.Default.Today_손익기준금 = Properties.Settings.Default.MT_principal + "@" + Properties.Settings.Default.MT_principal;

            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.TB_매수비율 = TB_매수비율;
                Properties.Settings.Default.TB_손익비율 = TB_손익비율;
            }
        }
        private static void 계좌관리_감시주문시간n기준금(int Selltime_오전, int Selltime_오후, bool rebalance_기준금, bool cut_기준금, bool Liquidation_기준금)
        {
            Properties.Settings.Default.MTB_rebalance_Selltime_오전 = Selltime_오전;
            Properties.Settings.Default.MTB_rebalance_Selltime_오후 = Selltime_오후;
            Properties.Settings.Default.CB_rebalance_기준금 = rebalance_기준금;
            Properties.Settings.Default.CB_cut_기준금 = cut_기준금;
            Properties.Settings.Default.CB_Liquidation_기준금 = Liquidation_기준금;
        }
        private static void 계좌관리_리밸런싱_A(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                              double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                              int 반복시간, double 주문가격, int 주문구분, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
                                              double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시주문구분,
                                              bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_A = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "리밸런싱_A", 검색식);

            Properties.Settings.Default.CB_rebalance_A = 사용;
            Properties.Settings.Default.MT_rebalance_starttime_A = 시작시간;
            Properties.Settings.Default.MT_rebalance_stoptime_A = 종료시간;
            Properties.Settings.Default.combo_rebalance_use_condition_A = 검색식사용;
            Properties.Settings.Default.combo_rebalance_condition_A = 검색식;
            Properties.Settings.Default.MTB_rebalance_delay_A = 검색유지시간;
            Properties.Settings.Default.TB_rebalance_매입금_A = 매입금;
            Properties.Settings.Default.TB_rebalance_누적거래량_A = 누적거래량;
            Properties.Settings.Default.TB_rebalance_누적거래대금_A = 누적거래대금;

            Properties.Settings.Default.TB_rebalance_mma_A = TB_mma1;
            Properties.Settings.Default.CBB_rebalance_mma_A = CBB_mma1;
            Properties.Settings.Default.TB_rebalance_mma2_A = TB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma2_A = CBB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma_배열_A = CBB_mma_배열;
            Properties.Settings.Default.TB_rebalance_dma1_A = TB_dma1;
            Properties.Settings.Default.CBB_rebalance_dma1_A = CBB_dma1;
            Properties.Settings.Default.TB_rebalance_dma2_A = TB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma2_A = CBB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma_배열_A = CBB_dma_배열;

            Properties.Settings.Default.TB_rebalance_suik_1_A = 수익범위1;
            Properties.Settings.Default.CB_rebalance_choice_A = 수익범위선택;
            Properties.Settings.Default.TB_rebalance_suik_2_A = 수익범위2;
            Properties.Settings.Default.combo_rebalance_suik_gubun_A = 수익구분;
            Properties.Settings.Default.TB_rebalance_sell_ratio_A = 매수비중;
            Properties.Settings.Default.combo_rebalance_sell_gubun_A = 매수구분;
            Properties.Settings.Default.TB_rebalance_maemae_1_A = 매매범위1;
            Properties.Settings.Default.TB_rebalance_maemae_2_A = 매매범위2;
            Properties.Settings.Default.combo_rebalance_maemae_gubun_A = 매매범위기준;
            Properties.Settings.Default.MT_rebalance_repeat_time_A = 반복시간;
            Properties.Settings.Default.TB_rebalance_value_A = 주문가격;
            Properties.Settings.Default.combo_rebalance_jumun_A = 주문구분;
            Properties.Settings.Default.MTB_rebalance_Cancel_time_A = 취소시간;

            Properties.Settings.Default.CB_rebalance_option_A = 감시시점;
            Properties.Settings.Default.TB_rebalance_sellratio1_A = 주문조건_1차;
            Properties.Settings.Default.리밸매도기준1_A = 주문조건선택_1차;
            Properties.Settings.Default.TB_rebalance_sellvolume1_A = 매도비중_1차;
            Properties.Settings.Default.TB_rebalance_sellcancel1_A = 취소시간_1차;
            Properties.Settings.Default.TB_rebalance_sellratio2_A = 주문조건_2차;
            Properties.Settings.Default.리밸매도기준2_A = 주문조건선택_2차;
            Properties.Settings.Default.TB_rebalance_sellvolume2_A = 매도비중_2차;
            Properties.Settings.Default.TB_rebalance_sellcancel2_A = 취소시간_2차;
            Properties.Settings.Default.CB_rebalance_매도체크_A = 매도체크;
            Properties.Settings.Default.CBB_rebalance_Selltime_A = 감시주문시간;
            Properties.Settings.Default.TB_rebalance_감시_value_A = 감시주문값;
            Properties.Settings.Default.combo_rebalance_감시_jumun_A = 감시주문구분;

            Properties.Settings.Default.CB_rebalance_TS_1차_A = TS_1차;
            Properties.Settings.Default.TB_rebalance_TS_1차_down_A = TS_1차_down;
            Properties.Settings.Default.TB_rebalance_TS_1차_mma_A = TB_1차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_A = CBB_1차_이평;
            Properties.Settings.Default.CB_rebalance_TS_2차_A = TS_2차;
            Properties.Settings.Default.TB_rebalance_TS_2차_down_A = TS_2차_down;
            Properties.Settings.Default.TB_rebalance_TS_2차_mma_A = TB_2차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_A = CBB_2차_이평;
        }
        private static void 계좌관리_리밸런싱_B(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                              double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                              int 반복시간, double 주문가격, int 주문구분, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
                                              double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시주문구분,
                                              bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_B = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "리밸런싱_B", 검색식);

            Properties.Settings.Default.CB_rebalance_B = 사용;
            Properties.Settings.Default.MT_rebalance_starttime_B = 시작시간;
            Properties.Settings.Default.MT_rebalance_stoptime_B = 종료시간;
            Properties.Settings.Default.combo_rebalance_use_condition_B = 검색식사용;
            Properties.Settings.Default.combo_rebalance_condition_B = 검색식;
            Properties.Settings.Default.MTB_rebalance_delay_B = 검색유지시간;
            Properties.Settings.Default.TB_rebalance_매입금_B = 매입금;
            Properties.Settings.Default.TB_rebalance_누적거래량_B = 누적거래량;
            Properties.Settings.Default.TB_rebalance_누적거래대금_B = 누적거래대금;

            Properties.Settings.Default.TB_rebalance_mma_B = TB_mma1;
            Properties.Settings.Default.CBB_rebalance_mma_B = CBB_mma1;
            Properties.Settings.Default.TB_rebalance_mma2_B = TB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma2_B = CBB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma_배열_B = CBB_mma_배열;
            Properties.Settings.Default.TB_rebalance_dma1_B = TB_dma1;
            Properties.Settings.Default.CBB_rebalance_dma1_B = CBB_dma1;
            Properties.Settings.Default.TB_rebalance_dma2_B = TB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma2_B = CBB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma_배열_B = CBB_dma_배열;

            Properties.Settings.Default.TB_rebalance_suik_1_B = 수익범위1;
            Properties.Settings.Default.CB_rebalance_choice_B = 수익범위선택;
            Properties.Settings.Default.TB_rebalance_suik_2_B = 수익범위2;
            Properties.Settings.Default.combo_rebalance_suik_gubun_B = 수익구분;
            Properties.Settings.Default.TB_rebalance_sell_ratio_B = 매수비중;
            Properties.Settings.Default.combo_rebalance_sell_gubun_B = 매수구분;
            Properties.Settings.Default.TB_rebalance_maemae_1_B = 매매범위1;
            Properties.Settings.Default.TB_rebalance_maemae_2_B = 매매범위2;
            Properties.Settings.Default.combo_rebalance_maemae_gubun_B = 매매범위기준;
            Properties.Settings.Default.MT_rebalance_repeat_time_B = 반복시간;
            Properties.Settings.Default.TB_rebalance_value_B = 주문가격;
            Properties.Settings.Default.combo_rebalance_jumun_B = 주문구분;
            Properties.Settings.Default.MTB_rebalance_Cancel_time_B = 취소시간;

            Properties.Settings.Default.CB_rebalance_option_B = 감시시점;
            Properties.Settings.Default.TB_rebalance_sellratio1_B = 주문조건_1차;
            Properties.Settings.Default.리밸매도기준1_B = 주문조건선택_1차;
            Properties.Settings.Default.TB_rebalance_sellvolume1_B = 매도비중_1차;
            Properties.Settings.Default.TB_rebalance_sellcancel1_B = 취소시간_1차;
            Properties.Settings.Default.TB_rebalance_sellratio2_B = 주문조건_2차;
            Properties.Settings.Default.리밸매도기준2_B = 주문조건선택_2차;
            Properties.Settings.Default.TB_rebalance_sellvolume2_B = 매도비중_2차;
            Properties.Settings.Default.TB_rebalance_sellcancel2_B = 취소시간_2차;
            Properties.Settings.Default.CB_rebalance_매도체크_B = 매도체크;
            Properties.Settings.Default.CBB_rebalance_Selltime_B = 감시주문시간;
            Properties.Settings.Default.TB_rebalance_감시_value_B = 감시주문값;
            Properties.Settings.Default.combo_rebalance_감시_jumun_B = 감시주문구분;

            Properties.Settings.Default.CB_rebalance_TS_1차_B = TS_1차;
            Properties.Settings.Default.TB_rebalance_TS_1차_down_B = TS_1차_down;
            Properties.Settings.Default.TB_rebalance_TS_1차_mma_B = TB_1차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_B = CBB_1차_이평;
            Properties.Settings.Default.CB_rebalance_TS_2차_B = TS_2차;
            Properties.Settings.Default.TB_rebalance_TS_2차_down_B = TS_2차_down;
            Properties.Settings.Default.TB_rebalance_TS_2차_mma_B = TB_2차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_B = CBB_2차_이평;
        }
        private static void 계좌관리_리밸런싱_C(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                              double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                              int 반복시간, double 주문가격, int 주문구분, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
                                              double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시주문구분,
                                              bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_C = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "리밸런싱_C", 검색식);

            Properties.Settings.Default.CB_rebalance_C = 사용;
            Properties.Settings.Default.MT_rebalance_starttime_C = 시작시간;
            Properties.Settings.Default.MT_rebalance_stoptime_C = 종료시간;
            Properties.Settings.Default.combo_rebalance_use_condition_C = 검색식사용;
            Properties.Settings.Default.combo_rebalance_condition_C = 검색식;
            Properties.Settings.Default.MTB_rebalance_delay_C = 검색유지시간;
            Properties.Settings.Default.TB_rebalance_매입금_C = 매입금;
            Properties.Settings.Default.TB_rebalance_누적거래량_C = 누적거래량;
            Properties.Settings.Default.TB_rebalance_누적거래대금_C = 누적거래대금;

            Properties.Settings.Default.TB_rebalance_mma_C = TB_mma1;
            Properties.Settings.Default.CBB_rebalance_mma_C = CBB_mma1;
            Properties.Settings.Default.TB_rebalance_mma2_C = TB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma2_C = CBB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma_배열_C = CBB_mma_배열;
            Properties.Settings.Default.TB_rebalance_dma1_C = TB_dma1;
            Properties.Settings.Default.CBB_rebalance_dma1_C = CBB_dma1;
            Properties.Settings.Default.TB_rebalance_dma2_C = TB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma2_C = CBB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma_배열_C = CBB_dma_배열;

            Properties.Settings.Default.TB_rebalance_suik_1_C = 수익범위1;
            Properties.Settings.Default.CB_rebalance_choice_C = 수익범위선택;
            Properties.Settings.Default.TB_rebalance_suik_2_C = 수익범위2;
            Properties.Settings.Default.combo_rebalance_suik_gubun_C = 수익구분;
            Properties.Settings.Default.TB_rebalance_sell_ratio_C = 매수비중;
            Properties.Settings.Default.combo_rebalance_sell_gubun_C = 매수구분;
            Properties.Settings.Default.TB_rebalance_maemae_1_C = 매매범위1;
            Properties.Settings.Default.TB_rebalance_maemae_2_C = 매매범위2;
            Properties.Settings.Default.combo_rebalance_maemae_gubun_C = 매매범위기준;
            Properties.Settings.Default.MT_rebalance_repeat_time_C = 반복시간;
            Properties.Settings.Default.TB_rebalance_value_C = 주문가격;
            Properties.Settings.Default.combo_rebalance_jumun_C = 주문구분;
            Properties.Settings.Default.MTB_rebalance_Cancel_time_C = 취소시간;

            Properties.Settings.Default.CB_rebalance_option_C = 감시시점;
            Properties.Settings.Default.TB_rebalance_sellratio1_C = 주문조건_1차;
            Properties.Settings.Default.리밸매도기준1_C = 주문조건선택_1차;
            Properties.Settings.Default.TB_rebalance_sellvolume1_C = 매도비중_1차;
            Properties.Settings.Default.TB_rebalance_sellcancel1_C = 취소시간_1차;
            Properties.Settings.Default.TB_rebalance_sellratio2_C = 주문조건_2차;
            Properties.Settings.Default.리밸매도기준2_C = 주문조건선택_2차;
            Properties.Settings.Default.TB_rebalance_sellvolume2_C = 매도비중_2차;
            Properties.Settings.Default.TB_rebalance_sellcancel2_C = 취소시간_2차;
            Properties.Settings.Default.CB_rebalance_매도체크_C = 매도체크;
            Properties.Settings.Default.CBB_rebalance_Selltime_C = 감시주문시간;
            Properties.Settings.Default.TB_rebalance_감시_value_C = 감시주문값;
            Properties.Settings.Default.combo_rebalance_감시_jumun_C = 감시주문구분;

            Properties.Settings.Default.CB_rebalance_TS_1차_C = TS_1차;
            Properties.Settings.Default.TB_rebalance_TS_1차_down_C = TS_1차_down;
            Properties.Settings.Default.TB_rebalance_TS_1차_mma_C = TB_1차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_C = CBB_1차_이평;
            Properties.Settings.Default.CB_rebalance_TS_2차_C = TS_2차;
            Properties.Settings.Default.TB_rebalance_TS_2차_down_C = TS_2차_down;
            Properties.Settings.Default.TB_rebalance_TS_2차_mma_C = TB_2차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_C = CBB_2차_이평;
        }
        private static void 계좌관리_리밸런싱_D(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                              double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                              int 반복시간, double 주문가격, int 주문구분, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
                                              double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시주문구분,
                                              bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_D = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "리밸런싱_D", 검색식);

            Properties.Settings.Default.CB_rebalance_D = 사용;
            Properties.Settings.Default.MT_rebalance_starttime_D = 시작시간;
            Properties.Settings.Default.MT_rebalance_stoptime_D = 종료시간;
            Properties.Settings.Default.combo_rebalance_use_condition_D = 검색식사용;
            Properties.Settings.Default.combo_rebalance_condition_D = 검색식;
            Properties.Settings.Default.MTB_rebalance_delay_D = 검색유지시간;
            Properties.Settings.Default.TB_rebalance_매입금_D = 매입금;
            Properties.Settings.Default.TB_rebalance_누적거래량_D = 누적거래량;
            Properties.Settings.Default.TB_rebalance_누적거래대금_D = 누적거래대금;

            Properties.Settings.Default.TB_rebalance_mma_D = TB_mma1;
            Properties.Settings.Default.CBB_rebalance_mma_D = CBB_mma1;
            Properties.Settings.Default.TB_rebalance_mma2_D = TB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma2_D = CBB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma_배열_D = CBB_mma_배열;
            Properties.Settings.Default.TB_rebalance_dma1_D = TB_dma1;
            Properties.Settings.Default.CBB_rebalance_dma1_D = CBB_dma1;
            Properties.Settings.Default.TB_rebalance_dma2_D = TB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma2_D = CBB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma_배열_D = CBB_dma_배열;

            Properties.Settings.Default.TB_rebalance_suik_1_D = 수익범위1;
            Properties.Settings.Default.CB_rebalance_choice_D = 수익범위선택;
            Properties.Settings.Default.TB_rebalance_suik_2_D = 수익범위2;
            Properties.Settings.Default.combo_rebalance_suik_gubun_D = 수익구분;
            Properties.Settings.Default.TB_rebalance_sell_ratio_D = 매수비중;
            Properties.Settings.Default.combo_rebalance_sell_gubun_D = 매수구분;
            Properties.Settings.Default.TB_rebalance_maemae_1_D = 매매범위1;
            Properties.Settings.Default.TB_rebalance_maemae_2_D = 매매범위2;
            Properties.Settings.Default.combo_rebalance_maemae_gubun_D = 매매범위기준;
            Properties.Settings.Default.MT_rebalance_repeat_time_D = 반복시간;
            Properties.Settings.Default.TB_rebalance_value_D = 주문가격;
            Properties.Settings.Default.combo_rebalance_jumun_D = 주문구분;
            Properties.Settings.Default.MTB_rebalance_Cancel_time_D = 취소시간;

            Properties.Settings.Default.CB_rebalance_option_D = 감시시점;
            Properties.Settings.Default.TB_rebalance_sellratio1_D = 주문조건_1차;
            Properties.Settings.Default.리밸매도기준1_D = 주문조건선택_1차;
            Properties.Settings.Default.TB_rebalance_sellvolume1_D = 매도비중_1차;
            Properties.Settings.Default.TB_rebalance_sellcancel1_D = 취소시간_1차;
            Properties.Settings.Default.TB_rebalance_sellratio2_D = 주문조건_2차;
            Properties.Settings.Default.리밸매도기준2_D = 주문조건선택_2차;
            Properties.Settings.Default.TB_rebalance_sellvolume2_D = 매도비중_2차;
            Properties.Settings.Default.TB_rebalance_sellcancel2_D = 취소시간_2차;
            Properties.Settings.Default.CB_rebalance_매도체크_D = 매도체크;
            Properties.Settings.Default.CBB_rebalance_Selltime_D = 감시주문시간;
            Properties.Settings.Default.TB_rebalance_감시_value_D = 감시주문값;
            Properties.Settings.Default.combo_rebalance_감시_jumun_D = 감시주문구분;

            Properties.Settings.Default.CB_rebalance_TS_1차_D = TS_1차;
            Properties.Settings.Default.TB_rebalance_TS_1차_down_D = TS_1차_down;
            Properties.Settings.Default.TB_rebalance_TS_1차_mma_D = TB_1차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_D = CBB_1차_이평;
            Properties.Settings.Default.CB_rebalance_TS_2차_D = TS_2차;
            Properties.Settings.Default.TB_rebalance_TS_2차_down_D = TS_2차_down;
            Properties.Settings.Default.TB_rebalance_TS_2차_mma_D = TB_2차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_D = CBB_2차_이평;
        }
        private static void 계좌관리_리밸런싱_E(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                              double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                              int 반복시간, double 주문가격, int 주문구분, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
                                              double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시주문구분,
                                              bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_E = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "리밸런싱_E", 검색식);

            Properties.Settings.Default.CB_rebalance_E = 사용;
            Properties.Settings.Default.MT_rebalance_starttime_E = 시작시간;
            Properties.Settings.Default.MT_rebalance_stoptime_E = 종료시간;
            Properties.Settings.Default.combo_rebalance_use_condition_E = 검색식사용;
            Properties.Settings.Default.combo_rebalance_condition_E = 검색식;
            Properties.Settings.Default.MTB_rebalance_delay_E = 검색유지시간;
            Properties.Settings.Default.TB_rebalance_매입금_E = 매입금;
            Properties.Settings.Default.TB_rebalance_누적거래량_E = 누적거래량;
            Properties.Settings.Default.TB_rebalance_누적거래대금_E = 누적거래대금;

            Properties.Settings.Default.TB_rebalance_mma_E = TB_mma1;
            Properties.Settings.Default.CBB_rebalance_mma_E = CBB_mma1;
            Properties.Settings.Default.TB_rebalance_mma2_E = TB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma2_E = CBB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma_배열_E = CBB_mma_배열;
            Properties.Settings.Default.TB_rebalance_dma1_E = TB_dma1;
            Properties.Settings.Default.CBB_rebalance_dma1_E = CBB_dma1;
            Properties.Settings.Default.TB_rebalance_dma2_E = TB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma2_E = CBB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma_배열_E = CBB_dma_배열;

            Properties.Settings.Default.TB_rebalance_suik_1_E = 수익범위1;
            Properties.Settings.Default.CB_rebalance_choice_E = 수익범위선택;
            Properties.Settings.Default.TB_rebalance_suik_2_E = 수익범위2;
            Properties.Settings.Default.combo_rebalance_suik_gubun_E = 수익구분;
            Properties.Settings.Default.TB_rebalance_sell_ratio_E = 매수비중;
            Properties.Settings.Default.combo_rebalance_sell_gubun_E = 매수구분;
            Properties.Settings.Default.TB_rebalance_maemae_1_E = 매매범위1;
            Properties.Settings.Default.TB_rebalance_maemae_2_E = 매매범위2;
            Properties.Settings.Default.combo_rebalance_maemae_gubun_E = 매매범위기준;
            Properties.Settings.Default.MT_rebalance_repeat_time_E = 반복시간;
            Properties.Settings.Default.TB_rebalance_value_E = 주문가격;
            Properties.Settings.Default.combo_rebalance_jumun_E = 주문구분;
            Properties.Settings.Default.MTB_rebalance_Cancel_time_E = 취소시간;

            Properties.Settings.Default.CB_rebalance_option_E = 감시시점;
            Properties.Settings.Default.TB_rebalance_sellratio1_E = 주문조건_1차;
            Properties.Settings.Default.리밸매도기준1_E = 주문조건선택_1차;
            Properties.Settings.Default.TB_rebalance_sellvolume1_E = 매도비중_1차;
            Properties.Settings.Default.TB_rebalance_sellcancel1_E = 취소시간_1차;
            Properties.Settings.Default.TB_rebalance_sellratio2_E = 주문조건_2차;
            Properties.Settings.Default.리밸매도기준2_E = 주문조건선택_2차;
            Properties.Settings.Default.TB_rebalance_sellvolume2_E = 매도비중_2차;
            Properties.Settings.Default.TB_rebalance_sellcancel2_E = 취소시간_2차;
            Properties.Settings.Default.CB_rebalance_매도체크_E = 매도체크;
            Properties.Settings.Default.CBB_rebalance_Selltime_E = 감시주문시간;
            Properties.Settings.Default.TB_rebalance_감시_value_E = 감시주문값;
            Properties.Settings.Default.combo_rebalance_감시_jumun_E = 감시주문구분;

            Properties.Settings.Default.CB_rebalance_TS_1차_E = TS_1차;
            Properties.Settings.Default.TB_rebalance_TS_1차_down_E = TS_1차_down;
            Properties.Settings.Default.TB_rebalance_TS_1차_mma_E = TB_1차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_E = CBB_1차_이평;
            Properties.Settings.Default.CB_rebalance_TS_2차_E = TS_2차;
            Properties.Settings.Default.TB_rebalance_TS_2차_down_E = TS_2차_down;
            Properties.Settings.Default.TB_rebalance_TS_2차_mma_E = TB_2차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_E = CBB_2차_이평;
        }
        private static void 계좌관리_리밸런싱_F(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                              double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                              int 반복시간, double 주문가격, int 주문구분, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
                                              double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시주문구분,
                                              bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_F = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "리밸런싱_F", 검색식);

            Properties.Settings.Default.CB_rebalance_F = 사용;
            Properties.Settings.Default.MT_rebalance_starttime_F = 시작시간;
            Properties.Settings.Default.MT_rebalance_stoptime_F = 종료시간;
            Properties.Settings.Default.combo_rebalance_use_condition_F = 검색식사용;
            Properties.Settings.Default.combo_rebalance_condition_F = 검색식;
            Properties.Settings.Default.MTB_rebalance_delay_F = 검색유지시간;
            Properties.Settings.Default.TB_rebalance_매입금_F = 매입금;
            Properties.Settings.Default.TB_rebalance_누적거래량_F = 누적거래량;
            Properties.Settings.Default.TB_rebalance_누적거래대금_F = 누적거래대금;

            Properties.Settings.Default.TB_rebalance_mma_F = TB_mma1;
            Properties.Settings.Default.CBB_rebalance_mma_F = CBB_mma1;
            Properties.Settings.Default.TB_rebalance_mma2_F = TB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma2_F = CBB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma_배열_F = CBB_mma_배열;
            Properties.Settings.Default.TB_rebalance_dma1_F = TB_dma1;
            Properties.Settings.Default.CBB_rebalance_dma1_F = CBB_dma1;
            Properties.Settings.Default.TB_rebalance_dma2_F = TB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma2_F = CBB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma_배열_F = CBB_dma_배열;

            Properties.Settings.Default.TB_rebalance_suik_1_F = 수익범위1;
            Properties.Settings.Default.CB_rebalance_choice_F = 수익범위선택;
            Properties.Settings.Default.TB_rebalance_suik_2_F = 수익범위2;
            Properties.Settings.Default.combo_rebalance_suik_gubun_F = 수익구분;
            Properties.Settings.Default.TB_rebalance_sell_ratio_F = 매수비중;
            Properties.Settings.Default.combo_rebalance_sell_gubun_F = 매수구분;
            Properties.Settings.Default.TB_rebalance_maemae_1_F = 매매범위1;
            Properties.Settings.Default.TB_rebalance_maemae_2_F = 매매범위2;
            Properties.Settings.Default.combo_rebalance_maemae_gubun_F = 매매범위기준;
            Properties.Settings.Default.MT_rebalance_repeat_time_F = 반복시간;
            Properties.Settings.Default.TB_rebalance_value_F = 주문가격;
            Properties.Settings.Default.combo_rebalance_jumun_F = 주문구분;
            Properties.Settings.Default.MTB_rebalance_Cancel_time_F = 취소시간;

            Properties.Settings.Default.CB_rebalance_option_F = 감시시점;
            Properties.Settings.Default.TB_rebalance_sellratio1_F = 주문조건_1차;
            Properties.Settings.Default.리밸매도기준1_F = 주문조건선택_1차;
            Properties.Settings.Default.TB_rebalance_sellvolume1_F = 매도비중_1차;
            Properties.Settings.Default.TB_rebalance_sellcancel1_F = 취소시간_1차;
            Properties.Settings.Default.TB_rebalance_sellratio2_F = 주문조건_2차;
            Properties.Settings.Default.리밸매도기준2_F = 주문조건선택_2차;
            Properties.Settings.Default.TB_rebalance_sellvolume2_F = 매도비중_2차;
            Properties.Settings.Default.TB_rebalance_sellcancel2_F = 취소시간_2차;
            Properties.Settings.Default.CB_rebalance_매도체크_F = 매도체크;
            Properties.Settings.Default.CBB_rebalance_Selltime_F = 감시주문시간;
            Properties.Settings.Default.TB_rebalance_감시_value_F = 감시주문값;
            Properties.Settings.Default.combo_rebalance_감시_jumun_F = 감시주문구분;

            Properties.Settings.Default.CB_rebalance_TS_1차_F = TS_1차;
            Properties.Settings.Default.TB_rebalance_TS_1차_down_F = TS_1차_down;
            Properties.Settings.Default.TB_rebalance_TS_1차_mma_F = TB_1차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_F = CBB_1차_이평;
            Properties.Settings.Default.CB_rebalance_TS_2차_F = TS_2차;
            Properties.Settings.Default.TB_rebalance_TS_2차_down_F = TS_2차_down;
            Properties.Settings.Default.TB_rebalance_TS_2차_mma_F = TB_2차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_F = CBB_2차_이평;
        }
        private static void 계좌관리_리밸런싱_G(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
                                              double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
                                              int 반복시간, double 주문가격, int 주문구분, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
                                              double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시주문구분,
                                              bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_G = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "리밸런싱_G", 검색식);

            Properties.Settings.Default.CB_rebalance_G = 사용;
            Properties.Settings.Default.MT_rebalance_starttime_G = 시작시간;
            Properties.Settings.Default.MT_rebalance_stoptime_G = 종료시간;
            Properties.Settings.Default.combo_rebalance_use_condition_G = 검색식사용;
            Properties.Settings.Default.combo_rebalance_condition_G = 검색식;
            Properties.Settings.Default.MTB_rebalance_delay_G = 검색유지시간;
            Properties.Settings.Default.TB_rebalance_매입금_G = 매입금;
            Properties.Settings.Default.TB_rebalance_누적거래량_G = 누적거래량;
            Properties.Settings.Default.TB_rebalance_누적거래대금_G = 누적거래대금;

            Properties.Settings.Default.TB_rebalance_mma_G = TB_mma1;
            Properties.Settings.Default.CBB_rebalance_mma_G = CBB_mma1;
            Properties.Settings.Default.TB_rebalance_mma2_G = TB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma2_G = CBB_mma2;
            Properties.Settings.Default.CBB_rebalance_mma_배열_G = CBB_mma_배열;
            Properties.Settings.Default.TB_rebalance_dma1_G = TB_dma1;
            Properties.Settings.Default.CBB_rebalance_dma1_G = CBB_dma1;
            Properties.Settings.Default.TB_rebalance_dma2_G = TB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma2_G = CBB_dma2;
            Properties.Settings.Default.CBB_rebalance_dma_배열_G = CBB_dma_배열;

            Properties.Settings.Default.TB_rebalance_suik_1_G = 수익범위1;
            Properties.Settings.Default.CB_rebalance_choice_G = 수익범위선택;
            Properties.Settings.Default.TB_rebalance_suik_2_G = 수익범위2;
            Properties.Settings.Default.combo_rebalance_suik_gubun_G = 수익구분;
            Properties.Settings.Default.TB_rebalance_sell_ratio_G = 매수비중;
            Properties.Settings.Default.combo_rebalance_sell_gubun_G = 매수구분;
            Properties.Settings.Default.TB_rebalance_maemae_1_G = 매매범위1;
            Properties.Settings.Default.TB_rebalance_maemae_2_G = 매매범위2;
            Properties.Settings.Default.combo_rebalance_maemae_gubun_G = 매매범위기준;
            Properties.Settings.Default.MT_rebalance_repeat_time_G = 반복시간;
            Properties.Settings.Default.TB_rebalance_value_G = 주문가격;
            Properties.Settings.Default.combo_rebalance_jumun_G = 주문구분;
            Properties.Settings.Default.MTB_rebalance_Cancel_time_G = 취소시간;

            Properties.Settings.Default.CB_rebalance_option_G = 감시시점;
            Properties.Settings.Default.TB_rebalance_sellratio1_G = 주문조건_1차;
            Properties.Settings.Default.리밸매도기준1_G = 주문조건선택_1차;
            Properties.Settings.Default.TB_rebalance_sellvolume1_G = 매도비중_1차;
            Properties.Settings.Default.TB_rebalance_sellcancel1_G = 취소시간_1차;
            Properties.Settings.Default.TB_rebalance_sellratio2_G = 주문조건_2차;
            Properties.Settings.Default.리밸매도기준2_G = 주문조건선택_2차;
            Properties.Settings.Default.TB_rebalance_sellvolume2_G = 매도비중_2차;
            Properties.Settings.Default.TB_rebalance_sellcancel2_G = 취소시간_2차;
            Properties.Settings.Default.CB_rebalance_매도체크_G = 매도체크;
            Properties.Settings.Default.CBB_rebalance_Selltime_G = 감시주문시간;
            Properties.Settings.Default.TB_rebalance_감시_value_G = 감시주문값;
            Properties.Settings.Default.combo_rebalance_감시_jumun_G = 감시주문구분;

            Properties.Settings.Default.CB_rebalance_TS_1차_G = TS_1차;
            Properties.Settings.Default.TB_rebalance_TS_1차_down_G = TS_1차_down;
            Properties.Settings.Default.TB_rebalance_TS_1차_mma_G = TB_1차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_1차_mma_G = CBB_1차_이평;
            Properties.Settings.Default.CB_rebalance_TS_2차_G = TS_2차;
            Properties.Settings.Default.TB_rebalance_TS_2차_down_G = TS_2차_down;
            Properties.Settings.Default.TB_rebalance_TS_2차_mma_G = TB_2차_이평;
            Properties.Settings.Default.CBB_rebalance_TS_2차_mma_G = CBB_2차_이평;
        }
        private static void 계좌관리_잔고청산_A(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금1, double 매입금2, int TB이평, int CBB이평,
                                              double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매도비중, int 매도구분, int 매매범위1, int 매매범위2,
                                              int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취소후주문, int 반복횟수, bool 매도정지, bool 추매금지, bool 강제매도, bool 수익보전,
                                              bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            청산_A = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "잔고청산_A", 검색식);

            Properties.Settings.Default.CB_Liquidation_A = 사용;
            Properties.Settings.Default.MTB_Liquidation_Starttime_A = 시작시간;
            Properties.Settings.Default.MTB_Liquidation_Stoptime_A = 종료시간;
            Properties.Settings.Default.CBB_Liquidation_use_condition_A = 검색식사용;
            Properties.Settings.Default.CBB_Liquidation_condition_A = 검색식;
            Properties.Settings.Default.MTB_Liquidation_delay_A = 검색유지시간;
            Properties.Settings.Default.TB_잔고청산_매입금1_A = 매입금1;
            Properties.Settings.Default.TB_잔고청산_매입금2_A = 매입금2;
            Properties.Settings.Default.TB_Liquidation_mma_A = TB이평;
            Properties.Settings.Default.CBB_Liquidation_mma_A = CBB이평;
            Properties.Settings.Default.TB_Liquidation_suik_1_A = 수익범위1;
            Properties.Settings.Default.CB_Liquidation_choice_A = 수익범위선택;
            Properties.Settings.Default.TB_Liquidation_suik_2_A = 수익범위2;
            Properties.Settings.Default.CBB_Liquidation_suik_gubun_A = 수익구분;
            Properties.Settings.Default.TB_Liquidation_sell_ratio_A = 매도비중;
            Properties.Settings.Default.CBB_Liquidation_sell_gubun_A = 매도구분;
            Properties.Settings.Default.TB_Liquidation_maemae_1_A = 매매범위1;
            Properties.Settings.Default.TB_Liquidation_maemae_2_A = 매매범위2;
            Properties.Settings.Default.MT_Liquidation_repeat_time_A = 반복시간;
            Properties.Settings.Default.TB_Liquidation_value_A = 주문가격;
            Properties.Settings.Default.CBB_Liquidation_jumun_A = 주문구분;
            Properties.Settings.Default.MTB_Liquidation_Cancel_time_A = 취소시간;
            Properties.Settings.Default.CBB_Liquidation_Cancel_A = 취소후주문;
            Properties.Settings.Default.MTB_Liquidation_repeat_A = 반복횟수;
            Properties.Settings.Default.CB_Liquidation_SellStop_A = 매도정지;
            Properties.Settings.Default.CB_추매금지_Liquidation_A = 추매금지;
            Properties.Settings.Default.CB_Liquidation_강제매도_A = 강제매도;
            Properties.Settings.Default.CB_수익보전_Liquidation_A = 수익보전;
            Properties.Settings.Default.CB_Liquidation_TS_A = TS;
            Properties.Settings.Default.TB_Liquidation_TS_down_A = TS_down;
            Properties.Settings.Default.TB_Liquidation_TS_mma_A = TB_TS_mma;
            Properties.Settings.Default.CBB_Liquidation_TS_mma_A = CBB_TS_mma;
            Properties.Settings.Default.TB_Liquidation_TS_dma_A = TB_TS_dma;
            Properties.Settings.Default.CBB_Liquidation_TS_dma_A = CBB_TS_dma;
        }

        private static void 계좌관리_잔고청산_B(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금1, double 매입금2, int TB이평, int CBB이평,
                                             double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매도비중, int 매도구분, int 매매범위1, int 매매범위2,
                                             int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취소후주문, int 반복횟수, bool 매도정지, bool 추매금지, bool 강제매도, bool 수익보전,
                                             bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            청산_B = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "잔고청산_B", 검색식);

            Properties.Settings.Default.CB_Liquidation_B = 사용;
            Properties.Settings.Default.MTB_Liquidation_Starttime_B = 시작시간;
            Properties.Settings.Default.MTB_Liquidation_Stoptime_B = 종료시간;
            Properties.Settings.Default.CBB_Liquidation_use_condition_B = 검색식사용;
            Properties.Settings.Default.CBB_Liquidation_condition_B = 검색식;
            Properties.Settings.Default.MTB_Liquidation_delay_B = 검색유지시간;
            Properties.Settings.Default.TB_잔고청산_매입금1_B = 매입금1;
            Properties.Settings.Default.TB_잔고청산_매입금2_B = 매입금2;
            Properties.Settings.Default.TB_Liquidation_mma_B = TB이평;
            Properties.Settings.Default.CBB_Liquidation_mma_B = CBB이평;
            Properties.Settings.Default.TB_Liquidation_suik_1_B = 수익범위1;
            Properties.Settings.Default.CB_Liquidation_choice_B = 수익범위선택;
            Properties.Settings.Default.TB_Liquidation_suik_2_B = 수익범위2;
            Properties.Settings.Default.CBB_Liquidation_suik_gubun_B = 수익구분;
            Properties.Settings.Default.TB_Liquidation_sell_ratio_B = 매도비중;
            Properties.Settings.Default.CBB_Liquidation_sell_gubun_B = 매도구분;
            Properties.Settings.Default.TB_Liquidation_maemae_1_B = 매매범위1;
            Properties.Settings.Default.TB_Liquidation_maemae_2_B = 매매범위2;
            Properties.Settings.Default.MT_Liquidation_repeat_time_B = 반복시간;
            Properties.Settings.Default.TB_Liquidation_value_B = 주문가격;
            Properties.Settings.Default.CBB_Liquidation_jumun_B = 주문구분;
            Properties.Settings.Default.MTB_Liquidation_Cancel_time_B = 취소시간;
            Properties.Settings.Default.CBB_Liquidation_Cancel_B = 취소후주문;
            Properties.Settings.Default.MTB_Liquidation_repeat_B = 반복횟수;
            Properties.Settings.Default.CB_Liquidation_SellStop_B = 매도정지;
            Properties.Settings.Default.CB_추매금지_Liquidation_B = 추매금지;
            Properties.Settings.Default.CB_Liquidation_강제매도_B = 강제매도;
            Properties.Settings.Default.CB_수익보전_Liquidation_B = 수익보전;
            Properties.Settings.Default.CB_Liquidation_TS_B = TS;
            Properties.Settings.Default.TB_Liquidation_TS_down_B = TS_down;
            Properties.Settings.Default.TB_Liquidation_TS_mma_B = TB_TS_mma;
            Properties.Settings.Default.CBB_Liquidation_TS_mma_B = CBB_TS_mma;
            Properties.Settings.Default.TB_Liquidation_TS_dma_B = TB_TS_dma;
            Properties.Settings.Default.CBB_Liquidation_TS_dma_B = CBB_TS_dma;
        }

        private static void 계좌관리_잔고청산_C(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금1, double 매입금2, int TB이평, int CBB이평,
        double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매도비중, int 매도구분, int 매매범위1, int 매매범위2,
                                             int 반복시간, double 주문가격, int 주문구분, int 취소시간, int 취소후주문, int 반복횟수, bool 매도정지, bool 추매금지, bool 강제매도, bool 수익보전,
                                             bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            청산_C = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };
            검색식_유무확인(사용, "잔고청산_C", 검색식);

            Properties.Settings.Default.CB_Liquidation_C = 사용;
            Properties.Settings.Default.MTB_Liquidation_Starttime_C = 시작시간;
            Properties.Settings.Default.MTB_Liquidation_Stoptime_C = 종료시간;
            Properties.Settings.Default.CBB_Liquidation_use_condition_C = 검색식사용;
            Properties.Settings.Default.CBB_Liquidation_condition_C = 검색식;
            Properties.Settings.Default.MTB_Liquidation_delay_C = 검색유지시간;
            Properties.Settings.Default.TB_잔고청산_매입금1_C = 매입금1;
            Properties.Settings.Default.TB_잔고청산_매입금2_C = 매입금2;
            Properties.Settings.Default.TB_Liquidation_mma_C = TB이평;
            Properties.Settings.Default.CBB_Liquidation_mma_C = CBB이평;
            Properties.Settings.Default.TB_Liquidation_suik_1_C = 수익범위1;
            Properties.Settings.Default.CB_Liquidation_choice_C = 수익범위선택;
            Properties.Settings.Default.TB_Liquidation_suik_2_C = 수익범위2;
            Properties.Settings.Default.CBB_Liquidation_suik_gubun_C = 수익구분;
            Properties.Settings.Default.TB_Liquidation_sell_ratio_C = 매도비중;
            Properties.Settings.Default.CBB_Liquidation_sell_gubun_C = 매도구분;
            Properties.Settings.Default.TB_Liquidation_maemae_1_C = 매매범위1;
            Properties.Settings.Default.TB_Liquidation_maemae_2_C = 매매범위2;
            Properties.Settings.Default.MT_Liquidation_repeat_time_C = 반복시간;
            Properties.Settings.Default.TB_Liquidation_value_C = 주문가격;
            Properties.Settings.Default.CBB_Liquidation_jumun_C = 주문구분;
            Properties.Settings.Default.MTB_Liquidation_Cancel_time_C = 취소시간;
            Properties.Settings.Default.CBB_Liquidation_Cancel_C = 취소후주문;
            Properties.Settings.Default.MTB_Liquidation_repeat_C = 반복횟수;
            Properties.Settings.Default.CB_Liquidation_SellStop_C = 매도정지;
            Properties.Settings.Default.CB_추매금지_Liquidation_C = 추매금지;
            Properties.Settings.Default.CB_Liquidation_강제매도_C = 강제매도;
            Properties.Settings.Default.CB_수익보전_Liquidation_C = 수익보전;
            Properties.Settings.Default.CB_Liquidation_TS_C = TS;
            Properties.Settings.Default.TB_Liquidation_TS_down_C = TS_down;
            Properties.Settings.Default.TB_Liquidation_TS_mma_C = TB_TS_mma;
            Properties.Settings.Default.CBB_Liquidation_TS_mma_C = CBB_TS_mma;
            Properties.Settings.Default.TB_Liquidation_TS_dma_C = TB_TS_dma;
            Properties.Settings.Default.CBB_Liquidation_TS_dma_C = CBB_TS_dma;
        }

        private static void 계좌관리_실현손익담보손실매도_A(bool CB_cut, int MTB_cut_time, double TB_cut_수익금1, double TB_cut_수익금2, double TB_cut_남길퍼, int TB_cut_won,
                                             double TB_cut_P, double TB_cut_ratio, int CBB_cut_gubun, double TB_cut_value, int CBB_cut_jumun, int MTB_cut_cansel_time)
        {

            Properties.Settings.Default.CB_cut_A = CB_cut;
            Properties.Settings.Default.MTB_cut_time_A = MTB_cut_time;
            Properties.Settings.Default.TB_cut_수익금1_A = TB_cut_수익금1;
            Properties.Settings.Default.TB_cut_수익금2_A = TB_cut_수익금2;
            Properties.Settings.Default.TB_cut_남길퍼_A = TB_cut_남길퍼;
            Properties.Settings.Default.TB_cut_won_A = TB_cut_won;
            Properties.Settings.Default.TB_cut_P_A = TB_cut_P;
            Properties.Settings.Default.TB_cut_ratio_A = TB_cut_ratio;
            Properties.Settings.Default.CBB_cut_gubun_A = CBB_cut_gubun;
            Properties.Settings.Default.TB_cut_value_A = TB_cut_value;
            Properties.Settings.Default.CBB_cut_jumun_A = CBB_cut_jumun;
            Properties.Settings.Default.MTB_cut_cansel_time_A = MTB_cut_cansel_time;

        }
        private static void 계좌관리_실현손익담보손실매도_B(bool CB_cut, int MTB_cut_time, double TB_cut_수익금1, double TB_cut_수익금2, double TB_cut_남길퍼, int TB_cut_won,
                                             double TB_cut_P, double TB_cut_ratio, int CBB_cut_gubun, double TB_cut_value, int CBB_cut_jumun, int MTB_cut_cansel_time)
        {
            Properties.Settings.Default.CB_cut_B = CB_cut;
            Properties.Settings.Default.MTB_cut_time_B = MTB_cut_time;
            Properties.Settings.Default.TB_cut_수익금1_B = TB_cut_수익금1;
            Properties.Settings.Default.TB_cut_수익금2_B = TB_cut_수익금2;

            Properties.Settings.Default.TB_cut_남길퍼_B = TB_cut_남길퍼;
            Properties.Settings.Default.TB_cut_won_B = TB_cut_won;
            Properties.Settings.Default.TB_cut_P_B = TB_cut_P;
            Properties.Settings.Default.TB_cut_ratio_B = TB_cut_ratio;
            Properties.Settings.Default.CBB_cut_gubun_B = CBB_cut_gubun;
            Properties.Settings.Default.TB_cut_value_B = TB_cut_value;
            Properties.Settings.Default.CBB_cut_jumun_B = CBB_cut_jumun;
            Properties.Settings.Default.MTB_cut_cansel_time_B = MTB_cut_cansel_time;
        }
        private static void 계좌관리_실현손익담보손실매도_C(bool CB_cut, int MTB_cut_time, double TB_cut_수익금1, double TB_cut_수익금2, double TB_cut_남길퍼, int TB_cut_won,
                                             double TB_cut_P, double TB_cut_ratio, int CBB_cut_gubun, double TB_cut_value, int CBB_cut_jumun, int MTB_cut_cansel_time)
        {
            Properties.Settings.Default.CB_cut_C = CB_cut;
            Properties.Settings.Default.MTB_cut_time_C = MTB_cut_time;
            Properties.Settings.Default.TB_cut_수익금1_C = TB_cut_수익금1;
            Properties.Settings.Default.TB_cut_수익금2_C = TB_cut_수익금2;

            Properties.Settings.Default.TB_cut_남길퍼_C = TB_cut_남길퍼;
            Properties.Settings.Default.TB_cut_won_C = TB_cut_won;
            Properties.Settings.Default.TB_cut_P_C = TB_cut_P;
            Properties.Settings.Default.TB_cut_ratio_C = TB_cut_ratio;
            Properties.Settings.Default.CBB_cut_gubun_C = CBB_cut_gubun;
            Properties.Settings.Default.TB_cut_value_C = TB_cut_value;
            Properties.Settings.Default.CBB_cut_jumun_C = CBB_cut_jumun;
            Properties.Settings.Default.MTB_cut_cansel_time_C = MTB_cut_cansel_time;
        }

        private static void 검색식_유무확인(bool 사용, string 위치, string 검색식)
        {
            if (사용 && !검색식.Equals("") && !검색식.Equals("") && !검색식.Equals(Properties.Settings.Default.TB_매수탐색A) && !검색식.Equals(Properties.Settings.Default.TB_매수탐색B) && !검색식.Equals(Properties.Settings.Default.TB_매도탐색))
            {
                bool result = false;

                for (int i = 0; i < Form1.form1.ConditionList.Count; i++)
                {
                    if (Form1.form1.ConditionList[i].name.Equals(검색식))
                    {
                        result = true;
                        break;
                    }
                }

                if (!result)
                {
                    Form1.form1.CB_기능설정.Checked = false;
                    Form1.form1.tab_체결.SelectedIndex = 1;
                    Form1.MBC_sender = "";
                    Form1.중요메세지("검색식 에러알림", "검색식 가동 에러 동작_Log를 확인하세요.");

                    Form1.동작_Log("[검색식 가동실패] " + 위치 + " -> ' " + 검색식 + " ' 검색식이 없어 가동할수 없습니다.");
                }
            }
        }

        ///////////////////////       특수설정
        private static void 특수설정_신규그룹(int A, int B, int C)
        {
            Properties.Settings.Default.combo_신규그룹_A = A;
            Properties.Settings.Default.combo_신규그룹_B = B;
            Properties.Settings.Default.combo_신규그룹_C = C;
        }
        private static void 특수설정_신규그룹(bool 기준금, bool CB_매매기간_오전, int TB_매매기간_오전주문시간, bool CB_매매기간_오후, int TB_매매기간_오후주문시간)
        {
            Properties.Settings.Default.CB_매매기간_기준금 = 기준금;
            Properties.Settings.Default.CB_매매기간_오전 = CB_매매기간_오전;
            Properties.Settings.Default.TB_매매기간_오전주문시간 = TB_매매기간_오전주문시간;
            Properties.Settings.Default.CB_매매기간_오후 = CB_매매기간_오후;
            Properties.Settings.Default.TB_매매기간_오후주문시간 = TB_매매기간_오후주문시간;
        }
        private static void 특수설정_매매기간주문_A(int trading, int 기간, int 주문시간, double 기준, int 기준방법, double 매도비중, int 매도방법, double 주문가격, int 주문방법, int 취소시간, bool 강제매도, bool 수익보전,
                                             bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            Properties.Settings.Default.CBB_매매기간_trading_A = trading;
            Properties.Settings.Default.MTB_매매기간_기간_A = 기간;
            Properties.Settings.Default.CBB_매매기간_주문시간_A = 주문시간;
            Properties.Settings.Default.TB_매매기간_기준_A = 기준;
            Properties.Settings.Default.CBB_매매기간_기준_A = 기준방법;
            Properties.Settings.Default.TB_매매기간_ratio_A = 매도비중;
            Properties.Settings.Default.CBB_매매기간_choice_A = 매도방법;
            Properties.Settings.Default.TB_매매기간_value_A = 주문가격;
            Properties.Settings.Default.CBB_매매기간_Jumun_A = 주문방법;
            Properties.Settings.Default.TB_매매기간_취소시간_A = 취소시간;
            Properties.Settings.Default.CB_매매기간_강제매도_A = 강제매도;
            Properties.Settings.Default.CB_매매기간_수익보전_A = 수익보전;
            Properties.Settings.Default.CB_매매기간_TS_A = TS;
            Properties.Settings.Default.TB_매매기간_TS_down_A = TS_down;
            Properties.Settings.Default.TB_매매기간_TS_mma_A = TB_TS_mma;
            Properties.Settings.Default.CBB_매매기간_TS_mma_A = CBB_TS_mma;
            Properties.Settings.Default.TB_매매기간_TS_dma_A = TB_TS_dma;
            Properties.Settings.Default.CBB_매매기간_TS_dma_A = CBB_TS_dma;
        }
        private static void 특수설정_매매기간주문_B(int trading, int 기간, int 주문시간, double 기준, int 기준방법, double 매도비중, int 매도방법, double 주문가격, int 주문방법, int 취소시간, bool 강제매도, bool 수익보전,
                                             bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            Properties.Settings.Default.CBB_매매기간_trading_B = trading;
            Properties.Settings.Default.MTB_매매기간_기간_B = 기간;
            Properties.Settings.Default.CBB_매매기간_주문시간_B = 주문시간;
            Properties.Settings.Default.TB_매매기간_기준_B = 기준;
            Properties.Settings.Default.CBB_매매기간_기준_B = 기준방법;
            Properties.Settings.Default.TB_매매기간_ratio_B = 매도비중;
            Properties.Settings.Default.CBB_매매기간_choice_B = 매도방법;
            Properties.Settings.Default.TB_매매기간_value_B = 주문가격;
            Properties.Settings.Default.CBB_매매기간_Jumun_B = 주문방법;
            Properties.Settings.Default.TB_매매기간_취소시간_B = 취소시간;
            Properties.Settings.Default.CB_매매기간_강제매도_B = 강제매도;
            Properties.Settings.Default.CB_매매기간_수익보전_B = 수익보전;
            Properties.Settings.Default.CB_매매기간_TS_B = TS;
            Properties.Settings.Default.TB_매매기간_TS_down_B = TS_down;
            Properties.Settings.Default.TB_매매기간_TS_mma_B = TB_TS_mma;
            Properties.Settings.Default.CBB_매매기간_TS_mma_B = CBB_TS_mma;
            Properties.Settings.Default.TB_매매기간_TS_dma_B = TB_TS_dma;
            Properties.Settings.Default.CBB_매매기간_TS_dma_B = CBB_TS_dma;
        }
        private static void 특수설정_매매기간주문_C(int trading, int 기간, int 주문시간, double 기준, int 기준방법, double 매도비중, int 매도방법, double 주문가격, int 주문방법, int 취소시간, bool 강제매도, bool 수익보전,
                                             bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            Properties.Settings.Default.CBB_매매기간_trading_C = trading;
            Properties.Settings.Default.MTB_매매기간_기간_C = 기간;
            Properties.Settings.Default.CBB_매매기간_주문시간_C = 주문시간;
            Properties.Settings.Default.TB_매매기간_기준_C = 기준;
            Properties.Settings.Default.CBB_매매기간_기준_C = 기준방법;
            Properties.Settings.Default.TB_매매기간_ratio_C = 매도비중;
            Properties.Settings.Default.CBB_매매기간_choice_C = 매도방법;
            Properties.Settings.Default.TB_매매기간_value_C = 주문가격;
            Properties.Settings.Default.CBB_매매기간_Jumun_C = 주문방법;
            Properties.Settings.Default.TB_매매기간_취소시간_C = 취소시간;
            Properties.Settings.Default.CB_매매기간_강제매도_C = 강제매도;
            Properties.Settings.Default.CB_매매기간_수익보전_C = 수익보전;
            Properties.Settings.Default.CB_매매기간_TS_C = TS;
            Properties.Settings.Default.TB_매매기간_TS_down_C = TS_down;
            Properties.Settings.Default.TB_매매기간_TS_mma_C = TB_TS_mma;
            Properties.Settings.Default.CBB_매매기간_TS_mma_C = CBB_TS_mma;
            Properties.Settings.Default.TB_매매기간_TS_dma_C = TB_TS_dma;
            Properties.Settings.Default.CBB_매매기간_TS_dma_C = CBB_TS_dma;
        }
        private static void 특수설정_매매기간주문_D(int trading, int 기간, int 주문시간, double 기준, int 기준방법, double 매도비중, int 매도방법, double 주문가격, int 주문방법, int 취소시간, bool 강제매도, bool 수익보전,
                                             bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            Properties.Settings.Default.CBB_매매기간_trading_D = trading;
            Properties.Settings.Default.MTB_매매기간_기간_D = 기간;
            Properties.Settings.Default.CBB_매매기간_주문시간_D = 주문시간;
            Properties.Settings.Default.TB_매매기간_기준_D = 기준;
            Properties.Settings.Default.CBB_매매기간_기준_D = 기준방법;
            Properties.Settings.Default.TB_매매기간_ratio_D = 매도비중;
            Properties.Settings.Default.CBB_매매기간_choice_D = 매도방법;
            Properties.Settings.Default.TB_매매기간_value_D = 주문가격;
            Properties.Settings.Default.CBB_매매기간_Jumun_D = 주문방법;
            Properties.Settings.Default.TB_매매기간_취소시간_D = 취소시간;
            Properties.Settings.Default.CB_매매기간_강제매도_D = 강제매도;
            Properties.Settings.Default.CB_매매기간_수익보전_D = 수익보전;
            Properties.Settings.Default.CB_매매기간_TS_D = TS;
            Properties.Settings.Default.TB_매매기간_TS_down_D = TS_down;
            Properties.Settings.Default.TB_매매기간_TS_mma_D = TB_TS_mma;
            Properties.Settings.Default.CBB_매매기간_TS_mma_D = CBB_TS_mma;
            Properties.Settings.Default.TB_매매기간_TS_dma_D = TB_TS_dma;
            Properties.Settings.Default.CBB_매매기간_TS_dma_D = CBB_TS_dma;
        }
        private static void 특수설정_매매기간주문_E(int trading, int 기간, int 주문시간, double 기준, int 기준방법, double 매도비중, int 매도방법, double 주문가격, int 주문방법, int 취소시간, bool 강제매도, bool 수익보전,
                                             bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            Properties.Settings.Default.CBB_매매기간_trading_E = trading;
            Properties.Settings.Default.MTB_매매기간_기간_E = 기간;
            Properties.Settings.Default.CBB_매매기간_주문시간_E = 주문시간;
            Properties.Settings.Default.TB_매매기간_기준_E = 기준;
            Properties.Settings.Default.CBB_매매기간_기준_E = 기준방법;
            Properties.Settings.Default.TB_매매기간_ratio_E = 매도비중;
            Properties.Settings.Default.CBB_매매기간_choice_E = 매도방법;
            Properties.Settings.Default.TB_매매기간_value_E = 주문가격;
            Properties.Settings.Default.CBB_매매기간_Jumun_E = 주문방법;
            Properties.Settings.Default.TB_매매기간_취소시간_E = 취소시간;
            Properties.Settings.Default.CB_매매기간_강제매도_E = 강제매도;
            Properties.Settings.Default.CB_매매기간_수익보전_E = 수익보전;
            Properties.Settings.Default.CB_매매기간_TS_E = TS;
            Properties.Settings.Default.TB_매매기간_TS_down_E = TS_down;
            Properties.Settings.Default.TB_매매기간_TS_mma_E = TB_TS_mma;
            Properties.Settings.Default.CBB_매매기간_TS_mma_E = CBB_TS_mma;
            Properties.Settings.Default.TB_매매기간_TS_dma_E = TB_TS_dma;
            Properties.Settings.Default.CBB_매매기간_TS_dma_E = CBB_TS_dma;
        }
        private static void 특수설정_매매기간주문_F(int trading, int 기간, int 주문시간, double 기준, int 기준방법, double 매도비중, int 매도방법, double 주문가격, int 주문방법, int 취소시간, bool 강제매도, bool 수익보전,
                                             bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            Properties.Settings.Default.CBB_매매기간_trading_F = trading;
            Properties.Settings.Default.MTB_매매기간_기간_F = 기간;
            Properties.Settings.Default.CBB_매매기간_주문시간_F = 주문시간;
            Properties.Settings.Default.TB_매매기간_기준_F = 기준;
            Properties.Settings.Default.CBB_매매기간_기준_F = 기준방법;
            Properties.Settings.Default.TB_매매기간_ratio_F = 매도비중;
            Properties.Settings.Default.CBB_매매기간_choice_F = 매도방법;
            Properties.Settings.Default.TB_매매기간_value_F = 주문가격;
            Properties.Settings.Default.CBB_매매기간_Jumun_F = 주문방법;
            Properties.Settings.Default.TB_매매기간_취소시간_F = 취소시간;
            Properties.Settings.Default.CB_매매기간_강제매도_F = 강제매도;
            Properties.Settings.Default.CB_매매기간_수익보전_F = 수익보전;
            Properties.Settings.Default.CB_매매기간_TS_F = TS;
            Properties.Settings.Default.TB_매매기간_TS_down_F = TS_down;
            Properties.Settings.Default.TB_매매기간_TS_mma_F = TB_TS_mma;
            Properties.Settings.Default.CBB_매매기간_TS_mma_F = CBB_TS_mma;
            Properties.Settings.Default.TB_매매기간_TS_dma_F = TB_TS_dma;
            Properties.Settings.Default.CBB_매매기간_TS_dma_F = CBB_TS_dma;
        }

        ///////////////////////       매매그룹설정


        private static void 매매그룹설정_익절(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_IK_group_A = A;
            Properties.Settings.Default.CB_IK_group_B = B;
            Properties.Settings.Default.CB_IK_group_C = C;
            Properties.Settings.Default.CB_IK_group_D = D;
            Properties.Settings.Default.CB_IK_group_E = E;
            Properties.Settings.Default.CB_IK_group_F = F;
            Properties.Settings.Default.CB_IK_group_G = G;
            Properties.Settings.Default.CB_IK_group_H = H;
            Properties.Settings.Default.CB_IK_group_I = I;
            Properties.Settings.Default.CB_IK_group_J = J;
            Properties.Settings.Default.CB_IK_group_K = K;
            Properties.Settings.Default.CB_IK_group_L = L;
        }
        private static void 매매그룹설정_손절(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_손절_A = A;
            Properties.Settings.Default.CB_손절_B = B;
            Properties.Settings.Default.CB_손절_C = C;
            Properties.Settings.Default.CB_손절_D = D;
            Properties.Settings.Default.CB_손절_E = E;
            Properties.Settings.Default.CB_손절_F = F;
            Properties.Settings.Default.CB_손절_G = G;
            Properties.Settings.Default.CB_손절_H = H;
            Properties.Settings.Default.CB_손절_I = I;
            Properties.Settings.Default.CB_손절_J = J;
            Properties.Settings.Default.CB_손절_K = K;
            Properties.Settings.Default.CB_손절_L = L;
        }
        private static void 매매그룹설정_반복A(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_A_A = A;
            Properties.Settings.Default.CB_Repeat_A_B = B;
            Properties.Settings.Default.CB_Repeat_A_C = C;
            Properties.Settings.Default.CB_Repeat_A_D = D;
            Properties.Settings.Default.CB_Repeat_A_E = E;
            Properties.Settings.Default.CB_Repeat_A_F = F;
            Properties.Settings.Default.CB_Repeat_A_G = G;
            Properties.Settings.Default.CB_Repeat_A_H = H;
            Properties.Settings.Default.CB_Repeat_A_I = I;
            Properties.Settings.Default.CB_Repeat_A_J = J;
            Properties.Settings.Default.CB_Repeat_A_K = K;
            Properties.Settings.Default.CB_Repeat_A_L = L;
        }
        private static void 매매그룹설정_반복B(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_B_A = A;
            Properties.Settings.Default.CB_Repeat_B_B = B;
            Properties.Settings.Default.CB_Repeat_B_C = C;
            Properties.Settings.Default.CB_Repeat_B_D = D;
            Properties.Settings.Default.CB_Repeat_B_E = E;
            Properties.Settings.Default.CB_Repeat_B_F = F;
            Properties.Settings.Default.CB_Repeat_B_G = G;
            Properties.Settings.Default.CB_Repeat_B_H = H;
            Properties.Settings.Default.CB_Repeat_B_I = I;
            Properties.Settings.Default.CB_Repeat_B_J = J;
            Properties.Settings.Default.CB_Repeat_B_K = K;
            Properties.Settings.Default.CB_Repeat_B_L = L;
        }
        private static void 매매그룹설정_반복C(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_C_A = A;
            Properties.Settings.Default.CB_Repeat_C_B = B;
            Properties.Settings.Default.CB_Repeat_C_C = C;
            Properties.Settings.Default.CB_Repeat_C_D = D;
            Properties.Settings.Default.CB_Repeat_C_E = E;
            Properties.Settings.Default.CB_Repeat_C_F = F;
            Properties.Settings.Default.CB_Repeat_C_G = G;
            Properties.Settings.Default.CB_Repeat_C_H = H;
            Properties.Settings.Default.CB_Repeat_C_I = I;
            Properties.Settings.Default.CB_Repeat_C_J = J;
            Properties.Settings.Default.CB_Repeat_C_K = K;
            Properties.Settings.Default.CB_Repeat_C_L = L;
        }
        private static void 매매그룹설정_반복D(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_D_A = A;
            Properties.Settings.Default.CB_Repeat_D_B = B;
            Properties.Settings.Default.CB_Repeat_D_C = C;
            Properties.Settings.Default.CB_Repeat_D_D = D;
            Properties.Settings.Default.CB_Repeat_D_E = E;
            Properties.Settings.Default.CB_Repeat_D_F = F;
            Properties.Settings.Default.CB_Repeat_D_G = G;
            Properties.Settings.Default.CB_Repeat_D_H = H;
            Properties.Settings.Default.CB_Repeat_D_I = I;
            Properties.Settings.Default.CB_Repeat_D_J = J;
            Properties.Settings.Default.CB_Repeat_D_K = K;
            Properties.Settings.Default.CB_Repeat_D_L = L;
        }
        private static void 매매그룹설정_반복E(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_E_A = A;
            Properties.Settings.Default.CB_Repeat_E_B = B;
            Properties.Settings.Default.CB_Repeat_E_C = C;
            Properties.Settings.Default.CB_Repeat_E_D = D;
            Properties.Settings.Default.CB_Repeat_E_E = E;
            Properties.Settings.Default.CB_Repeat_E_F = F;
            Properties.Settings.Default.CB_Repeat_E_G = G;
            Properties.Settings.Default.CB_Repeat_E_H = H;
            Properties.Settings.Default.CB_Repeat_E_I = I;
            Properties.Settings.Default.CB_Repeat_E_J = J;
            Properties.Settings.Default.CB_Repeat_E_K = K;
            Properties.Settings.Default.CB_Repeat_E_L = L;
        }
        private static void 매매그룹설정_반복F(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_F_A = A;
            Properties.Settings.Default.CB_Repeat_F_B = B;
            Properties.Settings.Default.CB_Repeat_F_C = C;
            Properties.Settings.Default.CB_Repeat_F_D = D;
            Properties.Settings.Default.CB_Repeat_F_E = E;
            Properties.Settings.Default.CB_Repeat_F_F = F;
            Properties.Settings.Default.CB_Repeat_F_G = G;
            Properties.Settings.Default.CB_Repeat_F_H = H;
            Properties.Settings.Default.CB_Repeat_F_I = I;
            Properties.Settings.Default.CB_Repeat_F_J = J;
            Properties.Settings.Default.CB_Repeat_F_K = K;
            Properties.Settings.Default.CB_Repeat_F_L = L;
        }
        private static void 매매그룹설정_반복G(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_G_A = A;
            Properties.Settings.Default.CB_Repeat_G_B = B;
            Properties.Settings.Default.CB_Repeat_G_C = C;
            Properties.Settings.Default.CB_Repeat_G_D = D;
            Properties.Settings.Default.CB_Repeat_G_E = E;
            Properties.Settings.Default.CB_Repeat_G_F = F;
            Properties.Settings.Default.CB_Repeat_G_G = G;
            Properties.Settings.Default.CB_Repeat_G_H = H;
            Properties.Settings.Default.CB_Repeat_G_I = I;
            Properties.Settings.Default.CB_Repeat_G_J = J;
            Properties.Settings.Default.CB_Repeat_G_K = K;
            Properties.Settings.Default.CB_Repeat_G_L = L;
        }
        private static void 매매그룹설정_반복H(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_H_A = A;
            Properties.Settings.Default.CB_Repeat_H_B = B;
            Properties.Settings.Default.CB_Repeat_H_C = C;
            Properties.Settings.Default.CB_Repeat_H_D = D;
            Properties.Settings.Default.CB_Repeat_H_E = E;
            Properties.Settings.Default.CB_Repeat_H_F = F;
            Properties.Settings.Default.CB_Repeat_H_G = G;
            Properties.Settings.Default.CB_Repeat_H_H = H;
            Properties.Settings.Default.CB_Repeat_H_I = I;
            Properties.Settings.Default.CB_Repeat_H_J = J;
            Properties.Settings.Default.CB_Repeat_H_K = K;
            Properties.Settings.Default.CB_Repeat_H_L = L;
        }
        private static void 매매그룹설정_반복I(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_I_A = A;
            Properties.Settings.Default.CB_Repeat_I_B = B;
            Properties.Settings.Default.CB_Repeat_I_C = C;
            Properties.Settings.Default.CB_Repeat_I_D = D;
            Properties.Settings.Default.CB_Repeat_I_E = E;
            Properties.Settings.Default.CB_Repeat_I_F = F;
            Properties.Settings.Default.CB_Repeat_I_G = G;
            Properties.Settings.Default.CB_Repeat_I_H = H;
            Properties.Settings.Default.CB_Repeat_I_I = I;
            Properties.Settings.Default.CB_Repeat_I_J = J;
            Properties.Settings.Default.CB_Repeat_I_K = K;
            Properties.Settings.Default.CB_Repeat_I_L = L;
        }
        private static void 매매그룹설정_반복J(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_J_A = A;
            Properties.Settings.Default.CB_Repeat_J_B = B;
            Properties.Settings.Default.CB_Repeat_J_C = C;
            Properties.Settings.Default.CB_Repeat_J_D = D;
            Properties.Settings.Default.CB_Repeat_J_E = E;
            Properties.Settings.Default.CB_Repeat_J_F = F;
            Properties.Settings.Default.CB_Repeat_J_G = G;
            Properties.Settings.Default.CB_Repeat_J_H = H;
            Properties.Settings.Default.CB_Repeat_J_I = I;
            Properties.Settings.Default.CB_Repeat_J_J = J;
            Properties.Settings.Default.CB_Repeat_J_K = K;
            Properties.Settings.Default.CB_Repeat_J_L = L;
        }
        private static void 매매그룹설정_반복K(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_K_A = A;
            Properties.Settings.Default.CB_Repeat_K_B = B;
            Properties.Settings.Default.CB_Repeat_K_C = C;
            Properties.Settings.Default.CB_Repeat_K_D = D;
            Properties.Settings.Default.CB_Repeat_K_E = E;
            Properties.Settings.Default.CB_Repeat_K_F = F;
            Properties.Settings.Default.CB_Repeat_K_G = G;
            Properties.Settings.Default.CB_Repeat_K_H = H;
            Properties.Settings.Default.CB_Repeat_K_I = I;
            Properties.Settings.Default.CB_Repeat_K_J = J;
            Properties.Settings.Default.CB_Repeat_K_K = K;
            Properties.Settings.Default.CB_Repeat_K_L = L;
        }
        private static void 매매그룹설정_반복L(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_L_A = A;
            Properties.Settings.Default.CB_Repeat_L_B = B;
            Properties.Settings.Default.CB_Repeat_L_C = C;
            Properties.Settings.Default.CB_Repeat_L_D = D;
            Properties.Settings.Default.CB_Repeat_L_E = E;
            Properties.Settings.Default.CB_Repeat_L_F = F;
            Properties.Settings.Default.CB_Repeat_L_G = G;
            Properties.Settings.Default.CB_Repeat_L_H = H;
            Properties.Settings.Default.CB_Repeat_L_I = I;
            Properties.Settings.Default.CB_Repeat_L_J = J;
            Properties.Settings.Default.CB_Repeat_L_K = K;
            Properties.Settings.Default.CB_Repeat_L_L = L;
        }
        private static void 매매그룹설정_반복M(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_M_A = A;
            Properties.Settings.Default.CB_Repeat_M_B = B;
            Properties.Settings.Default.CB_Repeat_M_C = C;
            Properties.Settings.Default.CB_Repeat_M_D = D;
            Properties.Settings.Default.CB_Repeat_M_E = E;
            Properties.Settings.Default.CB_Repeat_M_F = F;
            Properties.Settings.Default.CB_Repeat_M_G = G;
            Properties.Settings.Default.CB_Repeat_M_H = H;
            Properties.Settings.Default.CB_Repeat_M_I = I;
            Properties.Settings.Default.CB_Repeat_M_J = J;
            Properties.Settings.Default.CB_Repeat_M_K = K;
            Properties.Settings.Default.CB_Repeat_M_L = L;
        }
        private static void 매매그룹설정_반복N(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Repeat_N_A = A;
            Properties.Settings.Default.CB_Repeat_N_B = B;
            Properties.Settings.Default.CB_Repeat_N_C = C;
            Properties.Settings.Default.CB_Repeat_N_D = D;
            Properties.Settings.Default.CB_Repeat_N_E = E;
            Properties.Settings.Default.CB_Repeat_N_F = F;
            Properties.Settings.Default.CB_Repeat_N_G = G;
            Properties.Settings.Default.CB_Repeat_N_H = H;
            Properties.Settings.Default.CB_Repeat_N_I = I;
            Properties.Settings.Default.CB_Repeat_N_J = J;
            Properties.Settings.Default.CB_Repeat_N_K = K;
            Properties.Settings.Default.CB_Repeat_N_L = L;
        }
        private static void 매매그룹설정_리밸_A(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_rebalance_A_A = A;
            Properties.Settings.Default.CB_rebalance_A_B = B;
            Properties.Settings.Default.CB_rebalance_A_C = C;
            Properties.Settings.Default.CB_rebalance_A_D = D;
            Properties.Settings.Default.CB_rebalance_A_E = E;
            Properties.Settings.Default.CB_rebalance_A_F = F;
            Properties.Settings.Default.CB_rebalance_A_G = G;
            Properties.Settings.Default.CB_rebalance_A_H = H;
            Properties.Settings.Default.CB_rebalance_A_I = I;
            Properties.Settings.Default.CB_rebalance_A_J = J;
            Properties.Settings.Default.CB_rebalance_A_K = K;
            Properties.Settings.Default.CB_rebalance_A_L = L;
        }
        private static void 매매그룹설정_리밸_B(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_rebalance_B_A = A;
            Properties.Settings.Default.CB_rebalance_B_B = B;
            Properties.Settings.Default.CB_rebalance_B_C = C;
            Properties.Settings.Default.CB_rebalance_B_D = D;
            Properties.Settings.Default.CB_rebalance_B_E = E;
            Properties.Settings.Default.CB_rebalance_B_F = F;
            Properties.Settings.Default.CB_rebalance_B_G = G;
            Properties.Settings.Default.CB_rebalance_B_H = H;
            Properties.Settings.Default.CB_rebalance_B_I = I;
            Properties.Settings.Default.CB_rebalance_B_J = J;
            Properties.Settings.Default.CB_rebalance_B_K = K;
            Properties.Settings.Default.CB_rebalance_B_L = L;
        }
        private static void 매매그룹설정_리밸_C(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_rebalance_C_A = A;
            Properties.Settings.Default.CB_rebalance_C_B = B;
            Properties.Settings.Default.CB_rebalance_C_C = C;
            Properties.Settings.Default.CB_rebalance_C_D = D;
            Properties.Settings.Default.CB_rebalance_C_E = E;
            Properties.Settings.Default.CB_rebalance_C_F = F;
            Properties.Settings.Default.CB_rebalance_C_G = G;
            Properties.Settings.Default.CB_rebalance_C_H = H;
            Properties.Settings.Default.CB_rebalance_C_I = I;
            Properties.Settings.Default.CB_rebalance_C_J = J;
            Properties.Settings.Default.CB_rebalance_C_K = K;
            Properties.Settings.Default.CB_rebalance_C_L = L;
        }
        private static void 매매그룹설정_리밸_D(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_rebalance_D_A = A;
            Properties.Settings.Default.CB_rebalance_D_B = B;
            Properties.Settings.Default.CB_rebalance_D_C = C;
            Properties.Settings.Default.CB_rebalance_D_D = D;
            Properties.Settings.Default.CB_rebalance_D_E = E;
            Properties.Settings.Default.CB_rebalance_D_F = F;
            Properties.Settings.Default.CB_rebalance_D_G = G;
            Properties.Settings.Default.CB_rebalance_D_H = H;
            Properties.Settings.Default.CB_rebalance_D_I = I;
            Properties.Settings.Default.CB_rebalance_D_J = J;
            Properties.Settings.Default.CB_rebalance_D_K = K;
            Properties.Settings.Default.CB_rebalance_D_L = L;
        }
        private static void 매매그룹설정_리밸_E(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_rebalance_E_A = A;
            Properties.Settings.Default.CB_rebalance_E_B = B;
            Properties.Settings.Default.CB_rebalance_E_C = C;
            Properties.Settings.Default.CB_rebalance_E_D = D;
            Properties.Settings.Default.CB_rebalance_E_E = E;
            Properties.Settings.Default.CB_rebalance_E_F = F;
            Properties.Settings.Default.CB_rebalance_E_G = G;
            Properties.Settings.Default.CB_rebalance_E_H = H;
            Properties.Settings.Default.CB_rebalance_E_I = I;
            Properties.Settings.Default.CB_rebalance_E_J = J;
            Properties.Settings.Default.CB_rebalance_E_K = K;
            Properties.Settings.Default.CB_rebalance_E_L = L;
        }
        private static void 매매그룹설정_리밸_F(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_rebalance_F_A = A;
            Properties.Settings.Default.CB_rebalance_F_B = B;
            Properties.Settings.Default.CB_rebalance_F_C = C;
            Properties.Settings.Default.CB_rebalance_F_D = D;
            Properties.Settings.Default.CB_rebalance_F_E = E;
            Properties.Settings.Default.CB_rebalance_F_F = F;
            Properties.Settings.Default.CB_rebalance_F_G = G;
            Properties.Settings.Default.CB_rebalance_F_H = H;
            Properties.Settings.Default.CB_rebalance_F_I = I;
            Properties.Settings.Default.CB_rebalance_F_J = J;
            Properties.Settings.Default.CB_rebalance_F_K = K;
            Properties.Settings.Default.CB_rebalance_F_L = L;
        }
        private static void 매매그룹설정_리밸_G(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_rebalance_G_A = A;
            Properties.Settings.Default.CB_rebalance_G_B = B;
            Properties.Settings.Default.CB_rebalance_G_C = C;
            Properties.Settings.Default.CB_rebalance_G_D = D;
            Properties.Settings.Default.CB_rebalance_G_E = E;
            Properties.Settings.Default.CB_rebalance_G_F = F;
            Properties.Settings.Default.CB_rebalance_G_G = G;
            Properties.Settings.Default.CB_rebalance_G_H = H;
            Properties.Settings.Default.CB_rebalance_G_I = I;
            Properties.Settings.Default.CB_rebalance_G_J = J;
            Properties.Settings.Default.CB_rebalance_G_K = K;
            Properties.Settings.Default.CB_rebalance_G_L = L;
        }
        private static void 매매그룹설정_청산_A(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Liquidation_A_A = A;
            Properties.Settings.Default.CB_Liquidation_A_B = B;
            Properties.Settings.Default.CB_Liquidation_A_C = C;
            Properties.Settings.Default.CB_Liquidation_A_D = D;
            Properties.Settings.Default.CB_Liquidation_A_E = E;
            Properties.Settings.Default.CB_Liquidation_A_F = F;
            Properties.Settings.Default.CB_Liquidation_A_G = G;
            Properties.Settings.Default.CB_Liquidation_A_H = H;
            Properties.Settings.Default.CB_Liquidation_A_I = I;
            Properties.Settings.Default.CB_Liquidation_A_J = J;
            Properties.Settings.Default.CB_Liquidation_A_K = K;
            Properties.Settings.Default.CB_Liquidation_A_L = L;
        }
        private static void 매매그룹설정_청산_B(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Liquidation_B_A = A;
            Properties.Settings.Default.CB_Liquidation_B_B = B;
            Properties.Settings.Default.CB_Liquidation_B_C = C;
            Properties.Settings.Default.CB_Liquidation_B_D = D;
            Properties.Settings.Default.CB_Liquidation_B_E = E;
            Properties.Settings.Default.CB_Liquidation_B_F = F;
            Properties.Settings.Default.CB_Liquidation_B_G = G;
            Properties.Settings.Default.CB_Liquidation_B_H = H;
            Properties.Settings.Default.CB_Liquidation_B_I = I;
            Properties.Settings.Default.CB_Liquidation_B_J = J;
            Properties.Settings.Default.CB_Liquidation_B_K = K;
            Properties.Settings.Default.CB_Liquidation_B_L = L;
        }
        private static void 매매그룹설정_청산_C(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Liquidation_C_A = A;
            Properties.Settings.Default.CB_Liquidation_C_B = B;
            Properties.Settings.Default.CB_Liquidation_C_C = C;
            Properties.Settings.Default.CB_Liquidation_C_D = D;
            Properties.Settings.Default.CB_Liquidation_C_E = E;
            Properties.Settings.Default.CB_Liquidation_C_F = F;
            Properties.Settings.Default.CB_Liquidation_C_G = G;
            Properties.Settings.Default.CB_Liquidation_C_H = H;
            Properties.Settings.Default.CB_Liquidation_C_I = I;
            Properties.Settings.Default.CB_Liquidation_C_J = J;
            Properties.Settings.Default.CB_Liquidation_C_K = K;
            Properties.Settings.Default.CB_Liquidation_C_L = L;
        }
        private static void 매매그룹설정_기간매도_A(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_day_A_A = A;
            Properties.Settings.Default.CB_day_A_B = B;
            Properties.Settings.Default.CB_day_A_C = C;
            Properties.Settings.Default.CB_day_A_D = D;
            Properties.Settings.Default.CB_day_A_E = E;
            Properties.Settings.Default.CB_day_A_F = F;
            Properties.Settings.Default.CB_day_A_G = G;
            Properties.Settings.Default.CB_day_A_H = H;
            Properties.Settings.Default.CB_day_A_I = I;
            Properties.Settings.Default.CB_day_A_J = J;
            Properties.Settings.Default.CB_day_A_K = K;
            Properties.Settings.Default.CB_day_A_L = L;
        }
        private static void 매매그룹설정_기간매도_B(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_day_B_A = A;
            Properties.Settings.Default.CB_day_B_B = B;
            Properties.Settings.Default.CB_day_B_C = C;
            Properties.Settings.Default.CB_day_B_D = D;
            Properties.Settings.Default.CB_day_B_E = E;
            Properties.Settings.Default.CB_day_B_F = F;
            Properties.Settings.Default.CB_day_B_G = G;
            Properties.Settings.Default.CB_day_B_H = H;
            Properties.Settings.Default.CB_day_B_I = I;
            Properties.Settings.Default.CB_day_B_J = J;
            Properties.Settings.Default.CB_day_B_K = K;
            Properties.Settings.Default.CB_day_B_L = L;
        }
        private static void 매매그룹설정_기간매도_C(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_day_C_A = A;
            Properties.Settings.Default.CB_day_C_B = B;
            Properties.Settings.Default.CB_day_C_C = C;
            Properties.Settings.Default.CB_day_C_D = D;
            Properties.Settings.Default.CB_day_C_E = E;
            Properties.Settings.Default.CB_day_C_F = F;
            Properties.Settings.Default.CB_day_C_G = G;
            Properties.Settings.Default.CB_day_C_H = H;
            Properties.Settings.Default.CB_day_C_I = I;
            Properties.Settings.Default.CB_day_C_J = J;
            Properties.Settings.Default.CB_day_C_K = K;
            Properties.Settings.Default.CB_day_C_L = L;
        }
        private static void 매매그룹설정_기간매도_D(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_day_D_A = A;
            Properties.Settings.Default.CB_day_D_B = B;
            Properties.Settings.Default.CB_day_D_C = C;
            Properties.Settings.Default.CB_day_D_D = D;
            Properties.Settings.Default.CB_day_D_E = E;
            Properties.Settings.Default.CB_day_D_F = F;
            Properties.Settings.Default.CB_day_D_G = G;
            Properties.Settings.Default.CB_day_D_H = H;
            Properties.Settings.Default.CB_day_D_I = I;
            Properties.Settings.Default.CB_day_D_J = J;
            Properties.Settings.Default.CB_day_D_K = K;
            Properties.Settings.Default.CB_day_D_L = L;
        }
        private static void 매매그룹설정_기간매도_E(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_day_E_A = A;
            Properties.Settings.Default.CB_day_E_B = B;
            Properties.Settings.Default.CB_day_E_C = C;
            Properties.Settings.Default.CB_day_E_D = D;
            Properties.Settings.Default.CB_day_E_E = E;
            Properties.Settings.Default.CB_day_E_F = F;
            Properties.Settings.Default.CB_day_E_G = G;
            Properties.Settings.Default.CB_day_E_H = H;
            Properties.Settings.Default.CB_day_E_I = I;
            Properties.Settings.Default.CB_day_E_J = J;
            Properties.Settings.Default.CB_day_E_K = K;
            Properties.Settings.Default.CB_day_E_L = L;
        }
        private static void 매매그룹설정_기간매도_F(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_day_F_A = A;
            Properties.Settings.Default.CB_day_F_B = B;
            Properties.Settings.Default.CB_day_F_C = C;
            Properties.Settings.Default.CB_day_F_D = D;
            Properties.Settings.Default.CB_day_F_E = E;
            Properties.Settings.Default.CB_day_F_F = F;
            Properties.Settings.Default.CB_day_F_G = G;
            Properties.Settings.Default.CB_day_F_H = H;
            Properties.Settings.Default.CB_day_F_I = I;
            Properties.Settings.Default.CB_day_F_J = J;
            Properties.Settings.Default.CB_day_F_K = K;
            Properties.Settings.Default.CB_day_F_L = L;
        }
        private static void 매매그룹설정_미수금정리(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_미수금정리_A = A;
            Properties.Settings.Default.CB_미수금정리_B = B;
            Properties.Settings.Default.CB_미수금정리_C = C;
            Properties.Settings.Default.CB_미수금정리_D = D;
            Properties.Settings.Default.CB_미수금정리_E = E;
            Properties.Settings.Default.CB_미수금정리_F = F;
            Properties.Settings.Default.CB_미수금정리_G = G;
            Properties.Settings.Default.CB_미수금정리_H = H;
            Properties.Settings.Default.CB_미수금정리_I = I;
            Properties.Settings.Default.CB_미수금정리_J = J;
            Properties.Settings.Default.CB_미수금정리_K = K;
            Properties.Settings.Default.CB_미수금정리_L = L;
        }
        private static void 매매그룹설정_손익담보매도_A(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Cut_A_A = A;
            Properties.Settings.Default.CB_Cut_A_B = B;
            Properties.Settings.Default.CB_Cut_A_C = C;
            Properties.Settings.Default.CB_Cut_A_D = D;
            Properties.Settings.Default.CB_Cut_A_E = E;
            Properties.Settings.Default.CB_Cut_A_F = F;
            Properties.Settings.Default.CB_Cut_A_G = G;
            Properties.Settings.Default.CB_Cut_A_H = H;
            Properties.Settings.Default.CB_Cut_A_I = I;
            Properties.Settings.Default.CB_Cut_A_J = J;
            Properties.Settings.Default.CB_Cut_A_K = K;
            Properties.Settings.Default.CB_Cut_A_L = L;
        }
        private static void 매매그룹설정_손익담보매도_B(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Cut_B_A = A;
            Properties.Settings.Default.CB_Cut_B_B = B;
            Properties.Settings.Default.CB_Cut_B_C = C;
            Properties.Settings.Default.CB_Cut_B_D = D;
            Properties.Settings.Default.CB_Cut_B_E = E;
            Properties.Settings.Default.CB_Cut_B_F = F;
            Properties.Settings.Default.CB_Cut_B_G = G;
            Properties.Settings.Default.CB_Cut_B_H = H;
            Properties.Settings.Default.CB_Cut_B_I = I;
            Properties.Settings.Default.CB_Cut_B_J = J;
            Properties.Settings.Default.CB_Cut_B_K = K;
            Properties.Settings.Default.CB_Cut_B_L = L;
        }
        private static void 매매그룹설정_손익담보매도_C(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_Cut_C_A = A;
            Properties.Settings.Default.CB_Cut_C_B = B;
            Properties.Settings.Default.CB_Cut_C_C = C;
            Properties.Settings.Default.CB_Cut_C_D = D;
            Properties.Settings.Default.CB_Cut_C_E = E;
            Properties.Settings.Default.CB_Cut_C_F = F;
            Properties.Settings.Default.CB_Cut_C_G = G;
            Properties.Settings.Default.CB_Cut_C_H = H;
            Properties.Settings.Default.CB_Cut_C_I = I;
            Properties.Settings.Default.CB_Cut_C_J = J;
            Properties.Settings.Default.CB_Cut_C_K = K;
            Properties.Settings.Default.CB_Cut_C_L = L;
        }
        private static void 매매그룹설정_계좌청산_특정시간(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_특정시간_계좌청산_A = A;
            Properties.Settings.Default.CB_특정시간_계좌청산_B = B;
            Properties.Settings.Default.CB_특정시간_계좌청산_C = C;
            Properties.Settings.Default.CB_특정시간_계좌청산_D = D;
            Properties.Settings.Default.CB_특정시간_계좌청산_E = E;
            Properties.Settings.Default.CB_특정시간_계좌청산_F = F;
            Properties.Settings.Default.CB_특정시간_계좌청산_G = G;
            Properties.Settings.Default.CB_특정시간_계좌청산_H = H;
            Properties.Settings.Default.CB_특정시간_계좌청산_I = I;
            Properties.Settings.Default.CB_특정시간_계좌청산_J = J;
            Properties.Settings.Default.CB_특정시간_계좌청산_K = K;
            Properties.Settings.Default.CB_특정시간_계좌청산_L = L;
        }
        private static void 매매그룹설정_계좌청산_실현손익(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_실현손익_계좌청산_A = A;
            Properties.Settings.Default.CB_실현손익_계좌청산_B = B;
            Properties.Settings.Default.CB_실현손익_계좌청산_C = C;
            Properties.Settings.Default.CB_실현손익_계좌청산_D = D;
            Properties.Settings.Default.CB_실현손익_계좌청산_E = E;
            Properties.Settings.Default.CB_실현손익_계좌청산_F = F;
            Properties.Settings.Default.CB_실현손익_계좌청산_G = G;
            Properties.Settings.Default.CB_실현손익_계좌청산_H = H;
            Properties.Settings.Default.CB_실현손익_계좌청산_I = I;
            Properties.Settings.Default.CB_실현손익_계좌청산_J = J;
            Properties.Settings.Default.CB_실현손익_계좌청산_K = K;
            Properties.Settings.Default.CB_실현손익_계좌청산_L = L;
        }
        private static void 매매그룹설정_계좌청산_예상손실(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_예상손실_계좌청산_A = A;
            Properties.Settings.Default.CB_예상손실_계좌청산_B = B;
            Properties.Settings.Default.CB_예상손실_계좌청산_C = C;
            Properties.Settings.Default.CB_예상손실_계좌청산_D = D;
            Properties.Settings.Default.CB_예상손실_계좌청산_E = E;
            Properties.Settings.Default.CB_예상손실_계좌청산_F = F;
            Properties.Settings.Default.CB_예상손실_계좌청산_G = G;
            Properties.Settings.Default.CB_예상손실_계좌청산_H = H;
            Properties.Settings.Default.CB_예상손실_계좌청산_I = I;
            Properties.Settings.Default.CB_예상손실_계좌청산_J = J;
            Properties.Settings.Default.CB_예상손실_계좌청산_K = K;
            Properties.Settings.Default.CB_예상손실_계좌청산_L = L;
        }
        private static void 매매그룹설정_계좌청산_예상수익(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_예상수익_계좌청산_A = A;
            Properties.Settings.Default.CB_예상수익_계좌청산_B = B;
            Properties.Settings.Default.CB_예상수익_계좌청산_C = C;
            Properties.Settings.Default.CB_예상수익_계좌청산_D = D;
            Properties.Settings.Default.CB_예상수익_계좌청산_E = E;
            Properties.Settings.Default.CB_예상수익_계좌청산_F = F;
            Properties.Settings.Default.CB_예상수익_계좌청산_G = G;
            Properties.Settings.Default.CB_예상수익_계좌청산_H = H;
            Properties.Settings.Default.CB_예상수익_계좌청산_I = I;
            Properties.Settings.Default.CB_예상수익_계좌청산_J = J;
            Properties.Settings.Default.CB_예상수익_계좌청산_K = K;
            Properties.Settings.Default.CB_예상수익_계좌청산_L = L;
        }
        private static void 매매그룹설정_시간청산_A(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_시간청산A_A = A;
            Properties.Settings.Default.CB_시간청산A_B = B;
            Properties.Settings.Default.CB_시간청산A_C = C;
            Properties.Settings.Default.CB_시간청산A_D = D;
            Properties.Settings.Default.CB_시간청산A_E = E;
            Properties.Settings.Default.CB_시간청산A_F = F;
            Properties.Settings.Default.CB_시간청산A_G = G;
            Properties.Settings.Default.CB_시간청산A_H = H;
            Properties.Settings.Default.CB_시간청산A_I = I;
            Properties.Settings.Default.CB_시간청산A_J = J;
            Properties.Settings.Default.CB_시간청산A_K = K;
            Properties.Settings.Default.CB_시간청산A_L = L;
        }
        private static void 매매그룹설정_시간청산_B(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_시간청산B_A = A;
            Properties.Settings.Default.CB_시간청산B_B = B;
            Properties.Settings.Default.CB_시간청산B_C = C;
            Properties.Settings.Default.CB_시간청산B_D = D;
            Properties.Settings.Default.CB_시간청산B_E = E;
            Properties.Settings.Default.CB_시간청산B_F = F;
            Properties.Settings.Default.CB_시간청산B_G = G;
            Properties.Settings.Default.CB_시간청산B_H = H;
            Properties.Settings.Default.CB_시간청산B_I = I;
            Properties.Settings.Default.CB_시간청산B_J = J;
            Properties.Settings.Default.CB_시간청산B_K = K;
            Properties.Settings.Default.CB_시간청산B_L = L;
        }
        private static void 매매그룹설정_시간청산_C(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            Properties.Settings.Default.CB_시간청산C_A = A;
            Properties.Settings.Default.CB_시간청산C_B = B;
            Properties.Settings.Default.CB_시간청산C_C = C;
            Properties.Settings.Default.CB_시간청산C_D = D;
            Properties.Settings.Default.CB_시간청산C_E = E;
            Properties.Settings.Default.CB_시간청산C_F = F;
            Properties.Settings.Default.CB_시간청산C_G = G;
            Properties.Settings.Default.CB_시간청산C_H = H;
            Properties.Settings.Default.CB_시간청산C_I = I;
            Properties.Settings.Default.CB_시간청산C_J = J;
            Properties.Settings.Default.CB_시간청산C_K = K;
            Properties.Settings.Default.CB_시간청산C_L = L;
        }


        ///////////////////////       대금탐색
        private static void 대금탐색_누적거래대금(int 거래대금)
        {
            Properties.Settings.Default.TB_accumulate_Price = 거래대금;
        }
        private static void 대금탐색_매수_A(int 잔량, double 매도호가별대금, double 매도호가합대금, double 매수호가별대금, double 매수호가합대금,
                                           bool 매수탐색사용, string 검색식, int 기준초, double 탐색등락률, int 상승카운터, bool 상승옵션, int 하락카운터, bool 하락옵션,
                                           int 탐색주가_1, int 탐색주가_2, int 탐색주가_3, int 탐색주가_4, int 탐색주가_5, int 탐색주가_6,
                                           int 탐색대금_1, int 탐색대금_2, int 탐색대금_3, int 탐색대금_4, int 탐색대금_5, int 탐색대금_6,
                                           int 반복, int 분봉, int 일봉)
        {
            Properties.Settings.Default.CBB_M_잔량 = 잔량;
            Properties.Settings.Default.TB_M_매도호가별대금 = 매도호가별대금;
            Properties.Settings.Default.TB_M_매도호가합대금 = 매도호가합대금;
            Properties.Settings.Default.TB_M_매수호가별대금 = 매수호가별대금;
            Properties.Settings.Default.TB_M_매수호가합대금 = 매수호가합대금;

            Properties.Settings.Default.TB_매수탐색A = 검색식;
            Properties.Settings.Default.CB_매수탐색A = 매수탐색사용;

            Properties.Settings.Default.TB_Buy_A_기준초 = 기준초;
            Properties.Settings.Default.TB_Buy_A_탐색rate = 탐색등락률;
            Properties.Settings.Default.TB_Buy_상승카운터_A = 상승카운터;
            Properties.Settings.Default.CB_Buy_상승옵션_A = 상승옵션;
            Properties.Settings.Default.TB_Buy_하락카운터_A = 하락카운터;
            Properties.Settings.Default.CB_Buy_하락옵션_A = 하락옵션;

            Properties.Settings.Default.TB_Buy_A_탐색주가_1 = 탐색주가_1;
            Properties.Settings.Default.TB_Buy_A_탐색주가_2 = 탐색주가_2;
            Properties.Settings.Default.TB_Buy_A_탐색주가_3 = 탐색주가_3;
            Properties.Settings.Default.TB_Buy_A_탐색주가_4 = 탐색주가_4;
            Properties.Settings.Default.TB_Buy_A_탐색주가_5 = 탐색주가_5;
            Properties.Settings.Default.TB_Buy_A_탐색주가_6 = 탐색주가_6;

            Properties.Settings.Default.TB_Buy_A_탐색대금_1 = 탐색대금_1;
            Properties.Settings.Default.TB_Buy_A_탐색대금_2 = 탐색대금_2;
            Properties.Settings.Default.TB_Buy_A_탐색대금_3 = 탐색대금_3;
            Properties.Settings.Default.TB_Buy_A_탐색대금_4 = 탐색대금_4;
            Properties.Settings.Default.TB_Buy_A_탐색대금_5 = 탐색대금_5;
            Properties.Settings.Default.TB_Buy_A_탐색대금_6 = 탐색대금_6;

            Properties.Settings.Default.MTB_M_반복 = 반복;
            Properties.Settings.Default.CBB_Buy_A_분봉 = 분봉;
            Properties.Settings.Default.CBB_Buy_A_일봉 = 일봉;
        }

        private static void 대금탐색_매수_B(int 잔량, double 매도호가별대금, double 매도호가합대금, double 매수호가별대금, double 매수호가합대금,
                                           bool 매수탐색사용, string 검색식, int 기준초, double 탐색등락률, int 상승카운터, bool 상승옵션, int 하락카운터, bool 하락옵션,
                                           int 탐색주가_1, int 탐색주가_2, int 탐색주가_3, int 탐색주가_4, int 탐색주가_5, int 탐색주가_6,
                                           int 탐색대금_1, int 탐색대금_2, int 탐색대금_3, int 탐색대금_4, int 탐색대금_5, int 탐색대금_6,
                                           int 반복, int 분봉, int 일봉)
        {
            Properties.Settings.Default.CBB_M_잔량_2 = 잔량;
            Properties.Settings.Default.TB_M_매도호가별대금_2 = 매도호가별대금;
            Properties.Settings.Default.TB_M_매도호가합대금_2 = 매도호가합대금;
            Properties.Settings.Default.TB_M_매수호가별대금_2 = 매수호가별대금;
            Properties.Settings.Default.TB_M_매수호가합대금_2 = 매수호가합대금;

            Properties.Settings.Default.TB_매수탐색B = 검색식;
            Properties.Settings.Default.CB_매수탐색B = 매수탐색사용;

            Properties.Settings.Default.TB_Buy_B_기준초 = 기준초;
            Properties.Settings.Default.TB_Buy_B_탐색rate = 탐색등락률;
            Properties.Settings.Default.TB_Buy_상승카운터_B = 상승카운터;
            Properties.Settings.Default.CB_Buy_상승옵션_B = 상승옵션;
            Properties.Settings.Default.TB_Buy_하락카운터_B = 하락카운터;
            Properties.Settings.Default.CB_Buy_하락옵션_B = 하락옵션;

            Properties.Settings.Default.TB_Buy_B_탐색주가_1 = 탐색주가_1;
            Properties.Settings.Default.TB_Buy_B_탐색주가_2 = 탐색주가_2;
            Properties.Settings.Default.TB_Buy_B_탐색주가_3 = 탐색주가_3;
            Properties.Settings.Default.TB_Buy_B_탐색주가_4 = 탐색주가_4;
            Properties.Settings.Default.TB_Buy_B_탐색주가_5 = 탐색주가_5;
            Properties.Settings.Default.TB_Buy_B_탐색주가_6 = 탐색주가_6;

            Properties.Settings.Default.TB_Buy_B_탐색대금_1 = 탐색대금_1;
            Properties.Settings.Default.TB_Buy_B_탐색대금_2 = 탐색대금_2;
            Properties.Settings.Default.TB_Buy_B_탐색대금_3 = 탐색대금_3;
            Properties.Settings.Default.TB_Buy_B_탐색대금_4 = 탐색대금_4;
            Properties.Settings.Default.TB_Buy_B_탐색대금_5 = 탐색대금_5;
            Properties.Settings.Default.TB_Buy_B_탐색대금_6 = 탐색대금_6;

            Properties.Settings.Default.MTB_M_반복_2 = 반복;
            Properties.Settings.Default.CBB_Buy_B_분봉 = 분봉;
            Properties.Settings.Default.CBB_Buy_B_일봉 = 일봉;
        }
        private static void 대금탐색_매도(bool 매도탐색사용, string 검색식, int 기준초, double 탐색등락률, int 상승카운터, bool 상승옵션, int 하락카운터, bool 하락옵션,
                                           int 탐색주가_1, int 탐색주가_2, int 탐색주가_3, int 탐색주가_4, int 탐색주가_5, int 탐색주가_6,
                                           int 탐색대금_1, int 탐색대금_2, int 탐색대금_3, int 탐색대금_4, int 탐색대금_5, int 탐색대금_6,
                                           int 분봉, int 일봉)
        {
            Properties.Settings.Default.TB_매도탐색 = 검색식;
            Properties.Settings.Default.CB_매도탐색 = 매도탐색사용;

            Properties.Settings.Default.TB_Sell_기준초 = 기준초;
            Properties.Settings.Default.TB_Sell_탐색rate = 탐색등락률;
            Properties.Settings.Default.TB_Sell_상승카운터 = 상승카운터;
            Properties.Settings.Default.CB_Sell_상승옵션 = 상승옵션;
            Properties.Settings.Default.TB_Sell_하락카운터 = 하락카운터;
            Properties.Settings.Default.CB_Sell_하락옵션 = 하락옵션;

            Properties.Settings.Default.TB_Sell_탐색주가_1 = 탐색주가_1;
            Properties.Settings.Default.TB_Sell_탐색주가_2 = 탐색주가_2;
            Properties.Settings.Default.TB_Sell_탐색주가_3 = 탐색주가_3;
            Properties.Settings.Default.TB_Sell_탐색주가_4 = 탐색주가_4;
            Properties.Settings.Default.TB_Sell_탐색주가_5 = 탐색주가_5;
            Properties.Settings.Default.TB_Sell_탐색주가_6 = 탐색주가_6;

            Properties.Settings.Default.TB_Sell_탐색대금_1 = 탐색대금_1;
            Properties.Settings.Default.TB_Sell_탐색대금_2 = 탐색대금_2;
            Properties.Settings.Default.TB_Sell_탐색대금_3 = 탐색대금_3;
            Properties.Settings.Default.TB_Sell_탐색대금_4 = 탐색대금_4;
            Properties.Settings.Default.TB_Sell_탐색대금_5 = 탐색대금_5;
            Properties.Settings.Default.TB_Sell_탐색대금_6 = 탐색대금_6;

            Properties.Settings.Default.CBB_Sell_탐색_분봉 = 분봉;
            Properties.Settings.Default.CBB_Sell_탐색_일봉 = 일봉;
        }

        private static void 기능설정값(bool 편입추가, bool 최종가업데이트, bool 신규매수정지, bool 추가매수정지, bool VI매수취소, bool VI매도취소, bool 상매수취소, bool 하매도취소, bool 상전량청산, bool 하전량청산, bool NXT, bool ETF매입비제외)
        {
            Tab_InterestGroup.그룹변경("신규_A", "기본값");
            Tab_InterestGroup.그룹변경("신규_B", "기본값");
            Tab_InterestGroup.그룹변경("신규_C", "기본값");
            Tab_InterestGroup.그룹변경("반복_A", "기본값");
            Tab_InterestGroup.그룹변경("반복_B", "기본값");
            Tab_InterestGroup.그룹변경("반복_C", "기본값");
            Tab_InterestGroup.그룹변경("반복_D", "기본값");
            Tab_InterestGroup.그룹변경("반복_E", "기본값");
            Tab_InterestGroup.그룹변경("반복_F", "기본값");
            Tab_InterestGroup.그룹변경("반복_G", "기본값");
            Tab_InterestGroup.그룹변경("반복_H", "기본값");
            Tab_InterestGroup.그룹변경("반복_I", "기본값");
            Tab_InterestGroup.그룹변경("반복_J", "기본값");
            Tab_InterestGroup.그룹변경("반복_K", "기본값");
            Tab_InterestGroup.그룹변경("반복_L", "기본값");
            Tab_InterestGroup.그룹변경("반복_M", "기본값");
            Tab_InterestGroup.그룹변경("반복_N", "기본값");
            Tab_InterestGroup.그룹변경("리밸_A", "기본값");
            Tab_InterestGroup.그룹변경("리밸_B", "기본값");
            Tab_InterestGroup.그룹변경("리밸_C", "기본값");
            Tab_InterestGroup.그룹변경("리밸_D", "기본값");
            Tab_InterestGroup.그룹변경("리밸_E", "기본값");
            Tab_InterestGroup.그룹변경("리밸_F", "기본값");
            Tab_InterestGroup.그룹변경("리밸_G", "기본값");
            Tab_InterestGroup.그룹변경("청산_A", "기본값");
            Tab_InterestGroup.그룹변경("청산_B", "기본값");
            Tab_InterestGroup.그룹변경("청산_C", "기본값");
            Tab_InterestGroup.그룹변경("기간_A", "기본값");
            Tab_InterestGroup.그룹변경("기간_B", "기본값");
            Tab_InterestGroup.그룹변경("기간_C", "기본값");
            Tab_InterestGroup.그룹변경("기간_D", "기본값");
            Tab_InterestGroup.그룹변경("기간_E", "기본값");
            Tab_InterestGroup.그룹변경("기간_F", "기본값");
            Tab_InterestGroup.매매관심그룹리스트확인();
            Tab_InterestGroup.save_관심그룹_Title_기록();

            if (!Properties.Settings.Default.CB_기본매매변경)
            {
                Form_Function.form.CB_편입추가.Checked = 편입추가;
                Form_Function.form.CB_최종가업데이트.Checked = 최종가업데이트;
                Form_Function.form.CB_신규매수정지.Checked = 신규매수정지;
                Form_Function.form.CB_추가매수정지.Checked = 추가매수정지;
                Form_Function.form.CB_VI매수취소.Checked = VI매수취소;
                Form_Function.form.CB_VI매도취소.Checked = VI매도취소;
                Form_Function.form.CB_상매수취소.Checked = 상매수취소;
                Form_Function.form.CB_하매도취소.Checked = 하매도취소;
                Form_Function.form.CB_상전량청산.Checked = 상전량청산;
                Form_Function.form.CB_하전량청산.Checked = 하전량청산;
                Form_Function.form.CB_NXT.Checked = NXT;
                Form_Function.form.CB_ETF매입비제외.Checked = ETF매입비제외;

                Properties.Settings.Default.CB_편입추가 = 편입추가;
                Properties.Settings.Default.CB_최종가업데이트 = 최종가업데이트;
                Properties.Settings.Default.CB_신규매수정지 = 신규매수정지;
                Properties.Settings.Default.CB_추가매수정지 = 추가매수정지;
                Properties.Settings.Default.CB_VI매수취소 = VI매수취소;
                Properties.Settings.Default.CB_VI매도취소 = VI매도취소;
                Properties.Settings.Default.CB_상매수취소 = 상매수취소;
                Properties.Settings.Default.CB_하매도취소 = 하매도취소;
                Properties.Settings.Default.CB_상전량청산 = 상전량청산;
                Properties.Settings.Default.CB_하전량청산 = 하전량청산;
                Properties.Settings.Default.CB_NXT = NXT;
                Properties.Settings.Default.CB_ETF매입비제외 = ETF매입비제외;

                Form1.신규매수정지 = 신규매수정지;
                Form1.추가매수정지 = 추가매수정지;
            }
        }

        public static void 가이드매매저장()
        {
            string 체크박스(bool 체크) { if (체크) return "true"; else return "false"; }
            Console.WriteLine("####################### 가이드매매저장 #######################");
            계좌설정();
            기본매매설정();
            반복매매설정();
            계좌관리설정();
            특수설정();
            매매그룹설정();
            대금탐색설정();
            기능설정();

            void 계좌설정()
            {
                Properties.Settings.Default.계좌설정 =
                "계좌설정_(" + Properties.Settings.Default.MT_starttime
                + "," + Properties.Settings.Default.MT_stoptime
                + "," + Properties.Settings.Default.TB_setjango
                + "," + 체크박스(Properties.Settings.Default.CB_계좌매입비_매수제한)
                + "," + Properties.Settings.Default.TB_계좌매입비_제한비중
                + "," + Properties.Settings.Default.CBB_계좌매입비_제한선택
                + "," + 체크박스(Properties.Settings.Default.CB_잔고매입비_추매제한)
                + "," + Properties.Settings.Default.TB_잔고매입비_추매제한
                + "," + Properties.Settings.Default.CBB_지수연동_신규
                + "," + Properties.Settings.Default.CBB_지수연동_추매
                + ");\n계좌설정_코스피(" + Properties.Settings.Default.TB_p_ratio_use
                + "," + Properties.Settings.Default.combo_p_ratio_UD
                + "," + Properties.Settings.Default.combo_p_ratio
                + "," + Properties.Settings.Default.TB_p_go_use
                + "," + Properties.Settings.Default.combo_p_go_UD
                + "," + Properties.Settings.Default.combo_p_go
                + "," + Properties.Settings.Default.TB_p_down_use
                + "," + Properties.Settings.Default.combo_p_down_UD
                + "," + Properties.Settings.Default.combo_p_down
                + ");\n계좌설정_코스닥(" + Properties.Settings.Default.TB_q_ratio_use
                + "," + Properties.Settings.Default.combo_q_ratio_UD
                + "," + Properties.Settings.Default.combo_q_ratio
                + "," + Properties.Settings.Default.TB_q_go_use
                + "," + Properties.Settings.Default.combo_q_go_UD
                + "," + Properties.Settings.Default.combo_q_go
                + "," + Properties.Settings.Default.TB_q_down_use
                + "," + Properties.Settings.Default.combo_q_down_UD
                + "," + Properties.Settings.Default.combo_q_down
                + ");\n계좌설정_미수사용(" + 체크박스(Properties.Settings.Default.CB_misu)
                + "," + Properties.Settings.Default.MT_misu_time
                + "," + Properties.Settings.Default.Combo_misu
                + "," + Properties.Settings.Default.TB_misu_ratio
                + "," + Properties.Settings.Default.TB_misu_value
                + "," + Properties.Settings.Default.Combo_misu_jumnun
                + "," + Properties.Settings.Default.TB_misu_repeat_time
                + ");\n계좌설정_이평선(" + 체크박스(Properties.Settings.Default.CB_kospi_new_stop)
                + "," + 체크박스(Properties.Settings.Default.CB_kospi_add_stop)
                + ",\n" + 체크박스(Properties.Settings.Default.CB_use_kospi_min_03)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kospi_min_05)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kospi_min_10)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kospi_min_20)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kospi_min_30)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kospi_min_60)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kospi_day_03)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kospi_day_05)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kospi_day_10)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kospi_day_20)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kospi_day_40)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kospi_day_60)
                + ",\n" + 체크박스(Properties.Settings.Default.CB_UD_kospi_min_03)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kospi_min_05)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kospi_min_10)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kospi_min_20)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kospi_min_30)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kospi_min_60)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kospi_day_03)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kospi_day_05)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kospi_day_10)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kospi_day_20)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kospi_day_40)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kospi_day_60)
                + ",\n" + 체크박스(Properties.Settings.Default.CB_kosdaq_new_stop)
                + "," + 체크박스(Properties.Settings.Default.CB_kosdaq_add_stop)
                + ",\n" + 체크박스(Properties.Settings.Default.CB_use_kosdaq_day_03)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kosdaq_min_05)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kosdaq_min_10)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kosdaq_min_20)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kosdaq_min_30)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kosdaq_min_60)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kosdaq_day_03)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kosdaq_day_05)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kosdaq_day_10)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kosdaq_day_20)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kosdaq_day_40)
                + "," + 체크박스(Properties.Settings.Default.CB_use_kosdaq_day_60)
                + ",\n" + 체크박스(Properties.Settings.Default.CB_UD_kosdaq_min_03)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kosdaq_min_05)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kosdaq_min_10)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kosdaq_min_20)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kosdaq_min_30)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kosdaq_min_60)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kosdaq_day_03)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kosdaq_day_05)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kosdaq_day_10)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kosdaq_day_20)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kosdaq_day_40)
                + "," + 체크박스(Properties.Settings.Default.CB_UD_kosdaq_day_60)
                + ");";
            }
            void 기본매매설정()
            {
                Properties.Settings.Default.기본매매설정 =
                "기본매매_신규횟수제한(" + 체크박스(Properties.Settings.Default.CB_신규횟수제한)
                + "," + Properties.Settings.Default.TB_신규횟수제한
                + ");\n기본매매_new_A(" + 체크박스(Properties.Settings.Default.CB_new_recatch_A)
                + "," + Properties.Settings.Default.MTB_new_delay_A
                + "," + Properties.Settings.Default.MTB_new_canceltime_A
                + "," + Properties.Settings.Default.combo_new_cancel_buy_A
                + "," + Properties.Settings.Default.MTB_new_repeat_A
                + "," + 체크박스(Properties.Settings.Default.CB_new_A)
                + "," + Properties.Settings.Default.combo_new_or_A
                + "," + Properties.Settings.Default.combo_new_condition_A
                + "," + Properties.Settings.Default.MT_new_start_A
                + "," + Properties.Settings.Default.MT_new_end_A
                + "," + Properties.Settings.Default.combo_new_choice_A
                + "," + Properties.Settings.Default.TB_new_value_A
                + "," + Properties.Settings.Default.combo_new_jumun_A
                + "," + Properties.Settings.Default.MT_new_ratio_A
                + ");\n기본매매_new_B(" + 체크박스(Properties.Settings.Default.CB_new_recatch_B)
                  + "," + Properties.Settings.Default.MTB_new_delay_B
                + "," + Properties.Settings.Default.MTB_new_canceltime_B
                + "," + Properties.Settings.Default.combo_new_cancel_buy_B
                + "," + Properties.Settings.Default.MTB_new_repeat_B
                + "," + 체크박스(Properties.Settings.Default.CB_new_B)
                + "," + Properties.Settings.Default.combo_new_or_B
                + "," + Properties.Settings.Default.combo_new_condition_B
                + "," + Properties.Settings.Default.MT_new_start_B
                + "," + Properties.Settings.Default.MT_new_end_B
                + "," + Properties.Settings.Default.combo_new_choice_B
                + "," + Properties.Settings.Default.TB_new_value_B
                + "," + Properties.Settings.Default.combo_new_jumun_B
                + "," + Properties.Settings.Default.MT_new_ratio_B
                + ");\n기본매매_new_C(" + 체크박스(Properties.Settings.Default.CB_new_recatch_C)
               + "," + Properties.Settings.Default.MTB_new_delay_C
                + "," + Properties.Settings.Default.MTB_new_canceltime_C
                + "," + Properties.Settings.Default.combo_new_cancel_buy_C
                + "," + Properties.Settings.Default.MTB_new_repeat_C
                + "," + 체크박스(Properties.Settings.Default.CB_new_C)
                + "," + Properties.Settings.Default.combo_new_or_C
                + "," + Properties.Settings.Default.combo_new_condition_C
                + "," + Properties.Settings.Default.MT_new_start_C
                + "," + Properties.Settings.Default.MT_new_end_C
                + "," + Properties.Settings.Default.combo_new_choice_C
                + "," + Properties.Settings.Default.TB_new_value_C
                + "," + Properties.Settings.Default.combo_new_jumun_C
                + "," + Properties.Settings.Default.MT_new_ratio_C
                + ");\n기본매매_신규매수조건(" + Properties.Settings.Default.TB_신규주가이상
                + "," + Properties.Settings.Default.TB_신규주가이하
                + "," + Properties.Settings.Default.TB_신규등락률이상
                + "," + Properties.Settings.Default.TB_신규등락률이하
                + "," + Properties.Settings.Default.MTB_추가매수딜레이
                + "," + 체크박스(Properties.Settings.Default.CB_new_rebuy)
                + "," + Properties.Settings.Default.MTB_new_rebuytime
                + ");\n기본매매_신규매수_세부조건(" + Properties.Settings.Default.TB_잔고개수_신규A
                + "," + Properties.Settings.Default.TB_잔고개수_신규B
                + "," + Properties.Settings.Default.TB_잔고개수_신규C
                + "," + Properties.Settings.Default.TB_Limit_New_A
                + "," + Properties.Settings.Default.TB_Limit_New_B
                + "," + Properties.Settings.Default.TB_Limit_New_C
                + "," + 체크박스(Properties.Settings.Default.CB_익절재매수A)
                + "," + 체크박스(Properties.Settings.Default.CB_익절재매수B)
                + "," + 체크박스(Properties.Settings.Default.CB_익절재매수C)
                + ");\n기본매매_트레일링스탑_조건(" + 체크박스(Properties.Settings.Default.CB_TS_손실제한)
                + "," + 체크박스(Properties.Settings.Default.CB_TS_취소후)
                + "," + 체크박스(Properties.Settings.Default.CB_TS_기준금)
                + "," + Properties.Settings.Default.MTB_TS_canceltime
                + "," + Properties.Settings.Default.CBB_TS_cancel_sell
                + "," + Properties.Settings.Default.MTB_TS_repeat
                + ");\n기본매매_트레일링스탑_A(" + 체크박스(Properties.Settings.Default.CB_TS_A)
                + "," + Properties.Settings.Default.TB_TS_upper_A
                + "," + Properties.Settings.Default.CBB_TS_upper_A
                + "," + Properties.Settings.Default.TB_TS_down_A
                + "," + Properties.Settings.Default.TB_TS_ratio_A
                + "," + Properties.Settings.Default.CBB_TS_ratio_A
                + "," + Properties.Settings.Default.TB_TS_Jumun_A
                + "," + Properties.Settings.Default.CBB_TS_Jumun_A
                + ");\n기본매매_트레일링스탑_B(" + 체크박스(Properties.Settings.Default.CB_TS_B)
                + "," + Properties.Settings.Default.TB_TS_upper_B
                + "," + Properties.Settings.Default.CBB_TS_upper_B
                + "," + Properties.Settings.Default.TB_TS_down_B
                + "," + Properties.Settings.Default.TB_TS_ratio_B
                + "," + Properties.Settings.Default.CBB_TS_ratio_B
                + "," + Properties.Settings.Default.TB_TS_Jumun_B
                + "," + Properties.Settings.Default.CBB_TS_Jumun_B
                + ");\n기본매매_트레일링스탑_C(" + 체크박스(Properties.Settings.Default.CB_TS_C)
                + "," + Properties.Settings.Default.TB_TS_upper_C
                + "," + Properties.Settings.Default.CBB_TS_upper_C
                + "," + Properties.Settings.Default.TB_TS_down_C
                + "," + Properties.Settings.Default.TB_TS_ratio_C
                + "," + Properties.Settings.Default.CBB_TS_ratio_C
                + "," + Properties.Settings.Default.TB_TS_Jumun_C
                + "," + Properties.Settings.Default.CBB_TS_Jumun_C
                + ");\n기본매매_트레일링스탑_D(" + 체크박스(Properties.Settings.Default.CB_TS_D)
                + "," + Properties.Settings.Default.TB_TS_upper_D
                + "," + Properties.Settings.Default.CBB_TS_upper_D
                + "," + Properties.Settings.Default.TB_TS_down_D
                + "," + Properties.Settings.Default.TB_TS_ratio_D
                + "," + Properties.Settings.Default.CBB_TS_ratio_D
                + "," + Properties.Settings.Default.TB_TS_Jumun_D
                + "," + Properties.Settings.Default.CBB_TS_Jumun_D
                + ");\n기본매매_트레일링스탑_E(" + 체크박스(Properties.Settings.Default.CB_TS_E)
                + "," + Properties.Settings.Default.TB_TS_upper_E
                + "," + Properties.Settings.Default.CBB_TS_upper_E
                + "," + Properties.Settings.Default.TB_TS_down_E
                + "," + Properties.Settings.Default.TB_TS_ratio_E
                + "," + Properties.Settings.Default.CBB_TS_ratio_E
                + "," + Properties.Settings.Default.TB_TS_Jumun_E
                + "," + Properties.Settings.Default.CBB_TS_Jumun_E
                + ");\n기본매매_트레일링스탑_F(" + 체크박스(Properties.Settings.Default.CB_TS_F)
                + "," + Properties.Settings.Default.TB_TS_upper_F
                + "," + Properties.Settings.Default.CBB_TS_upper_F
                + "," + Properties.Settings.Default.TB_TS_down_F
                + "," + Properties.Settings.Default.TB_TS_ratio_F
                + "," + Properties.Settings.Default.CBB_TS_ratio_F
                + "," + Properties.Settings.Default.TB_TS_Jumun_F
                + "," + Properties.Settings.Default.CBB_TS_Jumun_F
                + ");\n기본매매_트레일링스탑_G(" + 체크박스(Properties.Settings.Default.CB_TS_G)
                + "," + Properties.Settings.Default.TB_TS_upper_G
                + "," + Properties.Settings.Default.CBB_TS_upper_G
                + "," + Properties.Settings.Default.TB_TS_down_G
                + "," + Properties.Settings.Default.TB_TS_ratio_G
                + "," + Properties.Settings.Default.CBB_TS_ratio_G
                + "," + Properties.Settings.Default.TB_TS_Jumun_G
                + "," + Properties.Settings.Default.CBB_TS_Jumun_G
                + ");\n기본매매_트레일링스탑_H(" + 체크박스(Properties.Settings.Default.CB_TS_H)
                + "," + Properties.Settings.Default.TB_TS_upper_H
                + "," + Properties.Settings.Default.CBB_TS_upper_H
                + "," + Properties.Settings.Default.TB_TS_down_H
                + "," + Properties.Settings.Default.TB_TS_ratio_H
                + "," + Properties.Settings.Default.CBB_TS_ratio_H
                + "," + Properties.Settings.Default.TB_TS_Jumun_H
                + "," + Properties.Settings.Default.CBB_TS_Jumun_H
                + ");\n기본매매_트레일링스탑_I(" + 체크박스(Properties.Settings.Default.CB_TS_I)
                + "," + Properties.Settings.Default.TB_TS_upper_I
                + "," + Properties.Settings.Default.CBB_TS_upper_I
                + "," + Properties.Settings.Default.TB_TS_down_I
                + "," + Properties.Settings.Default.TB_TS_ratio_I
                + "," + Properties.Settings.Default.CBB_TS_ratio_I
                + "," + Properties.Settings.Default.TB_TS_Jumun_I
                + "," + Properties.Settings.Default.CBB_TS_Jumun_I
                + ");";
            }

            void 반복매매설정()
            {
                Properties.Settings.Default.반복매매설정 =
                "반복기준금(" + 체크박스(Properties.Settings.Default.CB_Repeat_기준금)
                + ");\n반복매매_A(" + 체크박스(Properties.Settings.Default.CB_repeat_use_A)
                + "," + Properties.Settings.Default.MT_repeat_time_start_A
                + "," + Properties.Settings.Default.MT_repeat_time_end_A
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_A)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_A
                + "," + Properties.Settings.Default.combo_repeat_condition_A
                + "," + Properties.Settings.Default.MTB_repeat_delay_A
                + "," + Properties.Settings.Default.TB_repeat_매입금_A
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_A
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_A
                + "," + Properties.Settings.Default.TB_repeat_mma_A
                + "," + Properties.Settings.Default.CBB_repeat_mma_A
                + "," + Properties.Settings.Default.TB_repeat_mma2_A
                + "," + Properties.Settings.Default.CBB_repeat_mma2_A
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_A
                + "," + Properties.Settings.Default.TB_repeat_dma1_A
                + "," + Properties.Settings.Default.CBB_repeat_dma1_A
                + "," + Properties.Settings.Default.TB_repeat_dma2_A
                + "," + Properties.Settings.Default.CBB_repeat_dma2_A
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_A
                + "," + Properties.Settings.Default.TB_repeat_suik_1_A
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_A)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_A
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_A
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_A
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_A
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_A
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_A
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_A
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_A
                + "," + Properties.Settings.Default.TB_repeat_value_A
                + "," + Properties.Settings.Default.combo_repeat_jumun_A
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_A
                + "," + Properties.Settings.Default.combo_repeat_Cancel_A
                + "," + Properties.Settings.Default.MTB_repeat_repeat_A
                + ");\n반복매매_B(" + 체크박스(Properties.Settings.Default.CB_repeat_use_B)
                + "," + Properties.Settings.Default.MT_repeat_time_start_B
                + "," + Properties.Settings.Default.MT_repeat_time_end_B
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_B)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_B
                + "," + Properties.Settings.Default.combo_repeat_condition_B
                + "," + Properties.Settings.Default.MTB_repeat_delay_B
                + "," + Properties.Settings.Default.TB_repeat_매입금_B
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_B
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_B
           + "," + Properties.Settings.Default.TB_repeat_mma_B
                + "," + Properties.Settings.Default.CBB_repeat_mma_B
                + "," + Properties.Settings.Default.TB_repeat_mma2_B
                + "," + Properties.Settings.Default.CBB_repeat_mma2_B
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_B
                + "," + Properties.Settings.Default.TB_repeat_dma1_B
                + "," + Properties.Settings.Default.CBB_repeat_dma1_B
                + "," + Properties.Settings.Default.TB_repeat_dma2_B
                + "," + Properties.Settings.Default.CBB_repeat_dma2_B
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_B
                + "," + Properties.Settings.Default.TB_repeat_suik_1_B
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_B)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_B
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_B
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_B
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_B
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_B
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_B
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_B
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_B
                + "," + Properties.Settings.Default.TB_repeat_value_B
                + "," + Properties.Settings.Default.combo_repeat_jumun_B
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_B
                + "," + Properties.Settings.Default.combo_repeat_Cancel_B
                + "," + Properties.Settings.Default.MTB_repeat_repeat_B
                   + ");\n반복매매_C(" + 체크박스(Properties.Settings.Default.CB_repeat_use_C)
                + "," + Properties.Settings.Default.MT_repeat_time_start_C
                + "," + Properties.Settings.Default.MT_repeat_time_end_C
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_C)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_C
                + "," + Properties.Settings.Default.combo_repeat_condition_C
                + "," + Properties.Settings.Default.MTB_repeat_delay_C
                + "," + Properties.Settings.Default.TB_repeat_매입금_C
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_C
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_C
             + "," + Properties.Settings.Default.TB_repeat_mma_C
                + "," + Properties.Settings.Default.CBB_repeat_mma_C
                + "," + Properties.Settings.Default.TB_repeat_mma2_C
                + "," + Properties.Settings.Default.CBB_repeat_mma2_C
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_C
                + "," + Properties.Settings.Default.TB_repeat_dma1_C
                + "," + Properties.Settings.Default.CBB_repeat_dma1_C
                + "," + Properties.Settings.Default.TB_repeat_dma2_C
                + "," + Properties.Settings.Default.CBB_repeat_dma2_C
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_C
                + "," + Properties.Settings.Default.TB_repeat_suik_1_C
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_C)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_C
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_C
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_C
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_C
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_C
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_C
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_C
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_C
                + "," + Properties.Settings.Default.TB_repeat_value_C
                + "," + Properties.Settings.Default.combo_repeat_jumun_C
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_C
                + "," + Properties.Settings.Default.combo_repeat_Cancel_C
                + "," + Properties.Settings.Default.MTB_repeat_repeat_C
                   + ");\n반복매매_D(" + 체크박스(Properties.Settings.Default.CB_repeat_use_D)
                + "," + Properties.Settings.Default.MT_repeat_time_start_D
                + "," + Properties.Settings.Default.MT_repeat_time_end_D
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_D)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_D
                + "," + Properties.Settings.Default.combo_repeat_condition_D
                + "," + Properties.Settings.Default.MTB_repeat_delay_D
                + "," + Properties.Settings.Default.TB_repeat_매입금_D
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_D
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_D
             + "," + Properties.Settings.Default.TB_repeat_mma_D
                + "," + Properties.Settings.Default.CBB_repeat_mma_D
                + "," + Properties.Settings.Default.TB_repeat_mma2_D
                + "," + Properties.Settings.Default.CBB_repeat_mma2_D
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_D
                + "," + Properties.Settings.Default.TB_repeat_dma1_D
                + "," + Properties.Settings.Default.CBB_repeat_dma1_D
                + "," + Properties.Settings.Default.TB_repeat_dma2_D
                + "," + Properties.Settings.Default.CBB_repeat_dma2_D
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_D
                + "," + Properties.Settings.Default.TB_repeat_suik_1_D
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_D)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_D
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_D
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_D
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_D
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_D
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_D
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_D
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_D
                + "," + Properties.Settings.Default.TB_repeat_value_D
                + "," + Properties.Settings.Default.combo_repeat_jumun_D
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_D
                + "," + Properties.Settings.Default.combo_repeat_Cancel_D
                + "," + Properties.Settings.Default.MTB_repeat_repeat_D
                   + ");\n반복매매_E(" + 체크박스(Properties.Settings.Default.CB_repeat_use_E)
                + "," + Properties.Settings.Default.MT_repeat_time_start_E
                + "," + Properties.Settings.Default.MT_repeat_time_end_E
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_E)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_E
                + "," + Properties.Settings.Default.combo_repeat_condition_E
                + "," + Properties.Settings.Default.MTB_repeat_delay_E
                + "," + Properties.Settings.Default.TB_repeat_매입금_E
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_E
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_E
             + "," + Properties.Settings.Default.TB_repeat_mma_E
                + "," + Properties.Settings.Default.CBB_repeat_mma_E
                + "," + Properties.Settings.Default.TB_repeat_mma2_E
                + "," + Properties.Settings.Default.CBB_repeat_mma2_E
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_E
                + "," + Properties.Settings.Default.TB_repeat_dma1_E
                + "," + Properties.Settings.Default.CBB_repeat_dma1_E
                + "," + Properties.Settings.Default.TB_repeat_dma2_E
                + "," + Properties.Settings.Default.CBB_repeat_dma2_E
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_E
                + "," + Properties.Settings.Default.TB_repeat_suik_1_E
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_E)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_E
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_E
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_E
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_E
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_E
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_E
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_E
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_E
                + "," + Properties.Settings.Default.TB_repeat_value_E
                + "," + Properties.Settings.Default.combo_repeat_jumun_E
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_E
                + "," + Properties.Settings.Default.combo_repeat_Cancel_E
                + "," + Properties.Settings.Default.MTB_repeat_repeat_E
                   + ");\n반복매매_F(" + 체크박스(Properties.Settings.Default.CB_repeat_use_F)
                + "," + Properties.Settings.Default.MT_repeat_time_start_F
                + "," + Properties.Settings.Default.MT_repeat_time_end_F
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_F)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_F
                + "," + Properties.Settings.Default.combo_repeat_condition_F
                + "," + Properties.Settings.Default.MTB_repeat_delay_F
                + "," + Properties.Settings.Default.TB_repeat_매입금_F
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_F
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_F
              + "," + Properties.Settings.Default.TB_repeat_mma_F
                + "," + Properties.Settings.Default.CBB_repeat_mma_F
                + "," + Properties.Settings.Default.TB_repeat_mma2_F
                + "," + Properties.Settings.Default.CBB_repeat_mma2_F
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_F
                + "," + Properties.Settings.Default.TB_repeat_dma1_F
                + "," + Properties.Settings.Default.CBB_repeat_dma1_F
                + "," + Properties.Settings.Default.TB_repeat_dma2_F
                + "," + Properties.Settings.Default.CBB_repeat_dma2_F
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_F
                + "," + Properties.Settings.Default.TB_repeat_suik_1_F
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_F)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_F
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_F
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_F
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_F
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_F
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_F
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_F
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_F
                + "," + Properties.Settings.Default.TB_repeat_value_F
                + "," + Properties.Settings.Default.combo_repeat_jumun_F
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_F
                + "," + Properties.Settings.Default.combo_repeat_Cancel_F
                + "," + Properties.Settings.Default.MTB_repeat_repeat_F
                   + ");\n반복매매_G(" + 체크박스(Properties.Settings.Default.CB_repeat_use_G)
                + "," + Properties.Settings.Default.MT_repeat_time_start_G
                + "," + Properties.Settings.Default.MT_repeat_time_end_G
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_G)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_G
                + "," + Properties.Settings.Default.combo_repeat_condition_G
                + "," + Properties.Settings.Default.MTB_repeat_delay_G
                + "," + Properties.Settings.Default.TB_repeat_매입금_G
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_G
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_G
                + "," + Properties.Settings.Default.TB_repeat_mma_G
                + "," + Properties.Settings.Default.CBB_repeat_mma_G
                + "," + Properties.Settings.Default.TB_repeat_mma2_G
                + "," + Properties.Settings.Default.CBB_repeat_mma2_G
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_G
                + "," + Properties.Settings.Default.TB_repeat_dma1_G
                + "," + Properties.Settings.Default.CBB_repeat_dma1_G
                + "," + Properties.Settings.Default.TB_repeat_dma2_G
                + "," + Properties.Settings.Default.CBB_repeat_dma2_G
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_G
                + "," + Properties.Settings.Default.TB_repeat_suik_1_G
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_G)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_G
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_G
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_G
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_G
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_G
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_G
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_G
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_G
                + "," + Properties.Settings.Default.TB_repeat_value_G
                + "," + Properties.Settings.Default.combo_repeat_jumun_G
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_G
                + "," + Properties.Settings.Default.combo_repeat_Cancel_G
                + "," + Properties.Settings.Default.MTB_repeat_repeat_G
                   + ");\n반복매매_H(" + 체크박스(Properties.Settings.Default.CB_repeat_use_H)
                + "," + Properties.Settings.Default.MT_repeat_time_start_H
                + "," + Properties.Settings.Default.MT_repeat_time_end_H
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_H)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_H
                + "," + Properties.Settings.Default.combo_repeat_condition_H
                + "," + Properties.Settings.Default.MTB_repeat_delay_H
                + "," + Properties.Settings.Default.TB_repeat_매입금_H
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_H
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_H
             + "," + Properties.Settings.Default.TB_repeat_mma_H
                + "," + Properties.Settings.Default.CBB_repeat_mma_H
                + "," + Properties.Settings.Default.TB_repeat_mma2_H
                + "," + Properties.Settings.Default.CBB_repeat_mma2_H
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_H
                + "," + Properties.Settings.Default.TB_repeat_dma1_H
                + "," + Properties.Settings.Default.CBB_repeat_dma1_H
                + "," + Properties.Settings.Default.TB_repeat_dma2_H
                + "," + Properties.Settings.Default.CBB_repeat_dma2_H
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_H
                + "," + Properties.Settings.Default.TB_repeat_suik_1_H
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_H)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_H
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_H
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_H
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_H
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_H
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_H
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_H
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_H
                + "," + Properties.Settings.Default.TB_repeat_value_H
                + "," + Properties.Settings.Default.combo_repeat_jumun_H
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_H
                + "," + Properties.Settings.Default.combo_repeat_Cancel_H
                + "," + Properties.Settings.Default.MTB_repeat_repeat_H
                   + ");\n반복매매_I(" + 체크박스(Properties.Settings.Default.CB_repeat_use_I)
                + "," + Properties.Settings.Default.MT_repeat_time_start_I
                + "," + Properties.Settings.Default.MT_repeat_time_end_I
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_I)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_I
                + "," + Properties.Settings.Default.combo_repeat_condition_I
                + "," + Properties.Settings.Default.MTB_repeat_delay_I
                + "," + Properties.Settings.Default.TB_repeat_매입금_I
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_I
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_I
             + "," + Properties.Settings.Default.TB_repeat_mma_I
                + "," + Properties.Settings.Default.CBB_repeat_mma_I
                + "," + Properties.Settings.Default.TB_repeat_mma2_I
                + "," + Properties.Settings.Default.CBB_repeat_mma2_I
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_I
                + "," + Properties.Settings.Default.TB_repeat_dma1_I
                + "," + Properties.Settings.Default.CBB_repeat_dma1_I
                + "," + Properties.Settings.Default.TB_repeat_dma2_I
                + "," + Properties.Settings.Default.CBB_repeat_dma2_I
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_I
                + "," + Properties.Settings.Default.TB_repeat_suik_1_I
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_I)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_I
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_I
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_I
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_I
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_I
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_I
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_I
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_I
                + "," + Properties.Settings.Default.TB_repeat_value_I
                + "," + Properties.Settings.Default.combo_repeat_jumun_I
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_I
                + "," + Properties.Settings.Default.combo_repeat_Cancel_I
                + "," + Properties.Settings.Default.MTB_repeat_repeat_I
                   + ");\n반복매매_J(" + 체크박스(Properties.Settings.Default.CB_repeat_use_J)
                + "," + Properties.Settings.Default.MT_repeat_time_start_J
                + "," + Properties.Settings.Default.MT_repeat_time_end_J
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_J)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_J
                + "," + Properties.Settings.Default.combo_repeat_condition_J
                + "," + Properties.Settings.Default.MTB_repeat_delay_J
                + "," + Properties.Settings.Default.TB_repeat_매입금_J
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_J
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_J
              + "," + Properties.Settings.Default.TB_repeat_mma_J
                + "," + Properties.Settings.Default.CBB_repeat_mma_J
                + "," + Properties.Settings.Default.TB_repeat_mma2_J
                + "," + Properties.Settings.Default.CBB_repeat_mma2_J
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_J
                + "," + Properties.Settings.Default.TB_repeat_dma1_J
                + "," + Properties.Settings.Default.CBB_repeat_dma1_J
                + "," + Properties.Settings.Default.TB_repeat_dma2_J
                + "," + Properties.Settings.Default.CBB_repeat_dma2_J
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_J
                + "," + Properties.Settings.Default.TB_repeat_suik_1_J
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_J)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_J
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_J
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_J
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_J
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_J
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_J
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_J
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_J
                + "," + Properties.Settings.Default.TB_repeat_value_J
                + "," + Properties.Settings.Default.combo_repeat_jumun_J
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_J
                + "," + Properties.Settings.Default.combo_repeat_Cancel_J
                + "," + Properties.Settings.Default.MTB_repeat_repeat_J
                   + ");\n반복매매_K(" + 체크박스(Properties.Settings.Default.CB_repeat_use_K)
                + "," + Properties.Settings.Default.MT_repeat_time_start_K
                + "," + Properties.Settings.Default.MT_repeat_time_end_K
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_K)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_K
                + "," + Properties.Settings.Default.combo_repeat_condition_K
                + "," + Properties.Settings.Default.MTB_repeat_delay_K
                + "," + Properties.Settings.Default.TB_repeat_매입금_K
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_K
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_K
             + "," + Properties.Settings.Default.TB_repeat_mma_K
                + "," + Properties.Settings.Default.CBB_repeat_mma_K
                + "," + Properties.Settings.Default.TB_repeat_mma2_K
                + "," + Properties.Settings.Default.CBB_repeat_mma2_K
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_K
                + "," + Properties.Settings.Default.TB_repeat_dma1_K
                + "," + Properties.Settings.Default.CBB_repeat_dma1_K
                + "," + Properties.Settings.Default.TB_repeat_dma2_K
                + "," + Properties.Settings.Default.CBB_repeat_dma2_K
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_K
                + "," + Properties.Settings.Default.TB_repeat_suik_1_K
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_K)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_K
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_K
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_K
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_K
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_K
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_K
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_K
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_K
                + "," + Properties.Settings.Default.TB_repeat_value_K
                + "," + Properties.Settings.Default.combo_repeat_jumun_K
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_K
                + "," + Properties.Settings.Default.combo_repeat_Cancel_K
                + "," + Properties.Settings.Default.MTB_repeat_repeat_K
                   + ");\n반복매매_L(" + 체크박스(Properties.Settings.Default.CB_repeat_use_L)
                + "," + Properties.Settings.Default.MT_repeat_time_start_L
                + "," + Properties.Settings.Default.MT_repeat_time_end_L
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_L)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_L
                + "," + Properties.Settings.Default.combo_repeat_condition_L
                + "," + Properties.Settings.Default.MTB_repeat_delay_L
                + "," + Properties.Settings.Default.TB_repeat_매입금_L
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_L
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_L
            + "," + Properties.Settings.Default.TB_repeat_mma_L
                + "," + Properties.Settings.Default.CBB_repeat_mma_L
                + "," + Properties.Settings.Default.TB_repeat_mma2_L
                + "," + Properties.Settings.Default.CBB_repeat_mma2_L
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_L
                + "," + Properties.Settings.Default.TB_repeat_dma1_L
                + "," + Properties.Settings.Default.CBB_repeat_dma1_L
                + "," + Properties.Settings.Default.TB_repeat_dma2_L
                + "," + Properties.Settings.Default.CBB_repeat_dma2_L
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_L
                + "," + Properties.Settings.Default.TB_repeat_suik_1_L
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_L)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_L
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_L
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_L
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_L
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_L
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_L
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_L
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_L
                + "," + Properties.Settings.Default.TB_repeat_value_L
                + "," + Properties.Settings.Default.combo_repeat_jumun_L
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_L
                + "," + Properties.Settings.Default.combo_repeat_Cancel_L
                + "," + Properties.Settings.Default.MTB_repeat_repeat_L
                   + ");\n반복매매_M(" + 체크박스(Properties.Settings.Default.CB_repeat_use_M)
                + "," + Properties.Settings.Default.MT_repeat_time_start_M
                + "," + Properties.Settings.Default.MT_repeat_time_end_M
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_M)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_M
                + "," + Properties.Settings.Default.combo_repeat_condition_M
                + "," + Properties.Settings.Default.MTB_repeat_delay_M
                + "," + Properties.Settings.Default.TB_repeat_매입금_M
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_M
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_M
             + "," + Properties.Settings.Default.TB_repeat_mma_M
                + "," + Properties.Settings.Default.CBB_repeat_mma_M
                + "," + Properties.Settings.Default.TB_repeat_mma2_M
                + "," + Properties.Settings.Default.CBB_repeat_mma2_M
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_M
                + "," + Properties.Settings.Default.TB_repeat_dma1_M
                + "," + Properties.Settings.Default.CBB_repeat_dma1_M
                + "," + Properties.Settings.Default.TB_repeat_dma2_M
                + "," + Properties.Settings.Default.CBB_repeat_dma2_M
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_M
                + "," + Properties.Settings.Default.TB_repeat_suik_1_M
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_M)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_M
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_M
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_M
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_M
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_M
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_M
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_M
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_M
                + "," + Properties.Settings.Default.TB_repeat_value_M
                + "," + Properties.Settings.Default.combo_repeat_jumun_M
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_M
                + "," + Properties.Settings.Default.combo_repeat_Cancel_M
                + "," + Properties.Settings.Default.MTB_repeat_repeat_M
                   + ");\n반복매매_N(" + 체크박스(Properties.Settings.Default.CB_repeat_use_N)
                + "," + Properties.Settings.Default.MT_repeat_time_start_N
                + "," + Properties.Settings.Default.MT_repeat_time_end_N
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_kind_N)
                + "," + Properties.Settings.Default.combo_repeat_use_condition_N
                + "," + Properties.Settings.Default.combo_repeat_condition_N
                + "," + Properties.Settings.Default.MTB_repeat_delay_N
                + "," + Properties.Settings.Default.TB_repeat_매입금_N
                + "," + Properties.Settings.Default.TB_repeat_누적거래량_N
                + "," + Properties.Settings.Default.TB_repeat_누적거래대금_N
                + "," + Properties.Settings.Default.TB_repeat_mma_N
                + "," + Properties.Settings.Default.CBB_repeat_mma_N
                + "," + Properties.Settings.Default.TB_repeat_mma2_N
                + "," + Properties.Settings.Default.CBB_repeat_mma2_N
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_N
                + "," + Properties.Settings.Default.TB_repeat_dma1_N
                + "," + Properties.Settings.Default.CBB_repeat_dma1_N
                + "," + Properties.Settings.Default.TB_repeat_dma2_N
                + "," + Properties.Settings.Default.CBB_repeat_dma2_N
                + "," + Properties.Settings.Default.CBB_repeat_mma_배열_N
                + "," + Properties.Settings.Default.TB_repeat_suik_1_N
                + "," + 체크박스(Properties.Settings.Default.CB_repeat_choice_N)
                + "," + Properties.Settings.Default.TB_repeat_suik_2_N
                + "," + Properties.Settings.Default.combo_repeat_suik_gubun_N
                + "," + Properties.Settings.Default.TB_repeat_sell_ratio_N
                + "," + Properties.Settings.Default.combo_repeat_sell_gubun_N
                + "," + Properties.Settings.Default.TB_repeat_maemae_1_N
                + "," + Properties.Settings.Default.TB_repeat_maemae_2_N
                + "," + Properties.Settings.Default.combo_repeat_maemae_gubun_N
                + "," + Properties.Settings.Default.MT_repeat_repeat_time_N
                + "," + Properties.Settings.Default.TB_repeat_value_N
                + "," + Properties.Settings.Default.combo_repeat_jumun_N
                + "," + Properties.Settings.Default.MTB_repeat_Cancel_time_N
                + "," + Properties.Settings.Default.combo_repeat_Cancel_N
                + "," + Properties.Settings.Default.MTB_repeat_repeat_N
                + ");";
            }
            void 계좌관리설정()
            {
                Properties.Settings.Default.계좌관리설정 =
                "계좌관리_추매조건(" + 체크박스(Properties.Settings.Default.CB_총매수금)
                + "," + Properties.Settings.Default.TB_총매수금
                + "," + 체크박스(Properties.Settings.Default.CB_일매수제한금)
                + "," + Properties.Settings.Default.TB_일매수제한금
                + "," + 체크박스(Properties.Settings.Default.CB_회수제한)
                + "," + Properties.Settings.Default.TB_회수제한
                + "," + Properties.Settings.Default.TB_추매주가이상
                + "," + Properties.Settings.Default.TB_추매주가이하
                + "," + Properties.Settings.Default.TB_추매등락률이상
                + "," + Properties.Settings.Default.TB_추매등락률이하
                + ");\n계좌관리_분할주문(" + Properties.Settings.Default.TB_분할간격_A
                + "," + Properties.Settings.Default.TB_분할간격_B
                + "," + Properties.Settings.Default.TB_분할간격_C
                + "," + Properties.Settings.Default.TB_분할횟수_A
                + "," + Properties.Settings.Default.TB_분할횟수_B
                + "," + Properties.Settings.Default.TB_분할횟수_C
                + ");\n계좌관리_기준비율관리(" + 체크박스(Properties.Settings.Default.CB_매수기준)
                + "," + Properties.Settings.Default.TB_매수비율
                + "," + 체크박스(Properties.Settings.Default.CB_손익기준)
                + "," + Properties.Settings.Default.TB_손익비율
                + ");\n계좌관리_감시주문시간n기준금(" + Properties.Settings.Default.MTB_rebalance_Selltime_오전
                + "," + Properties.Settings.Default.MTB_rebalance_Selltime_오후
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_기준금)
                + "," + 체크박스(Properties.Settings.Default.CB_cut_기준금)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_기준금)
                + ");\n"
                + "\n계좌관리_리밸런싱_A(" + 체크박스(Properties.Settings.Default.CB_rebalance_A)
                + "," + Properties.Settings.Default.MT_rebalance_starttime_A
                + "," + Properties.Settings.Default.MT_rebalance_stoptime_A
                + "," + Properties.Settings.Default.combo_rebalance_use_condition_A
                + "," + Properties.Settings.Default.combo_rebalance_condition_A
                + "," + Properties.Settings.Default.MTB_rebalance_delay_A
                + "," + Properties.Settings.Default.TB_rebalance_매입금_A
                + "," + Properties.Settings.Default.TB_rebalance_누적거래량_A
                + "," + Properties.Settings.Default.TB_rebalance_누적거래대금_A
                + "," + Properties.Settings.Default.TB_rebalance_mma_A
                + "," + Properties.Settings.Default.CBB_rebalance_mma_A
                   + "," + Properties.Settings.Default.TB_rebalance_mma2_A
                + "," + Properties.Settings.Default.CBB_rebalance_mma2_A
                + "," + Properties.Settings.Default.CBB_rebalance_mma_배열_A
                   + "," + Properties.Settings.Default.TB_rebalance_dma1_A
                + "," + Properties.Settings.Default.CBB_rebalance_dma1_A
                   + "," + Properties.Settings.Default.TB_rebalance_dma2_A
                + "," + Properties.Settings.Default.CBB_rebalance_dma2_A
                + "," + Properties.Settings.Default.CBB_rebalance_dma_배열_A
                + "," + Properties.Settings.Default.TB_rebalance_suik_1_A
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_choice_A)
                + "," + Properties.Settings.Default.TB_rebalance_suik_2_A
                + "," + Properties.Settings.Default.combo_rebalance_suik_gubun_A
                + "," + Properties.Settings.Default.TB_rebalance_sell_ratio_A
                + "," + Properties.Settings.Default.combo_rebalance_sell_gubun_A
                + "," + Properties.Settings.Default.TB_rebalance_maemae_1_A
                + "," + Properties.Settings.Default.TB_rebalance_maemae_2_A
                + "," + Properties.Settings.Default.combo_rebalance_maemae_gubun_A
                + "," + Properties.Settings.Default.MT_rebalance_repeat_time_A
                + "," + Properties.Settings.Default.TB_rebalance_value_A
                + "," + Properties.Settings.Default.combo_rebalance_jumun_A
                + "," + Properties.Settings.Default.MTB_rebalance_Cancel_time_A
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_option_A)
                + "," + Properties.Settings.Default.TB_rebalance_sellratio1_A
                + "," + Properties.Settings.Default.리밸매도기준1_A
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume1_A
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel1_A
                + "," + Properties.Settings.Default.TB_rebalance_sellratio2_A
                + "," + Properties.Settings.Default.리밸매도기준2_A
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume2_A
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel2_A
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_매도체크_A)
                + "," + Properties.Settings.Default.CBB_rebalance_Selltime_A
                + "," + Properties.Settings.Default.TB_rebalance_감시_value_A
                + "," + Properties.Settings.Default.combo_rebalance_감시_jumun_A
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_1차_A)
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_down_A
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_mma_A
                + "," + Properties.Settings.Default.CBB_rebalance_TS_1차_mma_A
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_2차_A)
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_down_A
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_mma_A
                + "," + Properties.Settings.Default.CBB_rebalance_TS_2차_mma_A
                + ");" +
                "\n계좌관리_리밸런싱_B(" + 체크박스(Properties.Settings.Default.CB_rebalance_B)
                + "," + Properties.Settings.Default.MT_rebalance_starttime_B
                + "," + Properties.Settings.Default.MT_rebalance_stoptime_B
                + "," + Properties.Settings.Default.combo_rebalance_use_condition_B
                + "," + Properties.Settings.Default.combo_rebalance_condition_B
                + "," + Properties.Settings.Default.MTB_rebalance_delay_B
                + "," + Properties.Settings.Default.TB_rebalance_매입금_B
                + "," + Properties.Settings.Default.TB_rebalance_누적거래량_B
                + "," + Properties.Settings.Default.TB_rebalance_누적거래대금_B
               + "," + Properties.Settings.Default.TB_rebalance_mma_B
                + "," + Properties.Settings.Default.CBB_rebalance_mma_B
                   + "," + Properties.Settings.Default.TB_rebalance_mma2_B
                + "," + Properties.Settings.Default.CBB_rebalance_mma2_B
                + "," + Properties.Settings.Default.CBB_rebalance_mma_배열_B
                   + "," + Properties.Settings.Default.TB_rebalance_dma1_B
                + "," + Properties.Settings.Default.CBB_rebalance_dma1_B
                   + "," + Properties.Settings.Default.TB_rebalance_dma2_B
                + "," + Properties.Settings.Default.CBB_rebalance_dma2_B
                + "," + Properties.Settings.Default.CBB_rebalance_dma_배열_B
                + "," + Properties.Settings.Default.TB_rebalance_suik_1_B
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_choice_B)
                + "," + Properties.Settings.Default.TB_rebalance_suik_2_B
                + "," + Properties.Settings.Default.combo_rebalance_suik_gubun_B
                + "," + Properties.Settings.Default.TB_rebalance_sell_ratio_B
                + "," + Properties.Settings.Default.combo_rebalance_sell_gubun_B
                + "," + Properties.Settings.Default.TB_rebalance_maemae_1_B
                + "," + Properties.Settings.Default.TB_rebalance_maemae_2_B
                + "," + Properties.Settings.Default.combo_rebalance_maemae_gubun_B
                + "," + Properties.Settings.Default.MT_rebalance_repeat_time_B
                + "," + Properties.Settings.Default.TB_rebalance_value_B
                + "," + Properties.Settings.Default.combo_rebalance_jumun_B
                + "," + Properties.Settings.Default.MTB_rebalance_Cancel_time_B
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_option_B)
                + "," + Properties.Settings.Default.TB_rebalance_sellratio1_B
                + "," + Properties.Settings.Default.리밸매도기준1_B
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume1_B
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel1_B
                + "," + Properties.Settings.Default.TB_rebalance_sellratio2_B
                + "," + Properties.Settings.Default.리밸매도기준2_B
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume2_B
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel2_B
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_매도체크_B)
                + "," + Properties.Settings.Default.CBB_rebalance_Selltime_B
                + "," + Properties.Settings.Default.TB_rebalance_감시_value_B
                + "," + Properties.Settings.Default.combo_rebalance_감시_jumun_B
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_1차_B)
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_down_B
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_mma_B
                + "," + Properties.Settings.Default.CBB_rebalance_TS_1차_mma_B
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_2차_B)
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_down_B
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_mma_B
                + "," + Properties.Settings.Default.CBB_rebalance_TS_2차_mma_B
                + ");"
                + "\n계좌관리_리밸런싱_C(" + 체크박스(Properties.Settings.Default.CB_rebalance_C)
                + "," + Properties.Settings.Default.MT_rebalance_starttime_C
                + "," + Properties.Settings.Default.MT_rebalance_stoptime_C
                + "," + Properties.Settings.Default.combo_rebalance_use_condition_C
                + "," + Properties.Settings.Default.combo_rebalance_condition_C
                + "," + Properties.Settings.Default.MTB_rebalance_delay_C
                + "," + Properties.Settings.Default.TB_rebalance_매입금_C
                + "," + Properties.Settings.Default.TB_rebalance_누적거래량_C
                + "," + Properties.Settings.Default.TB_rebalance_누적거래대금_C
               + "," + Properties.Settings.Default.TB_rebalance_mma_C
                + "," + Properties.Settings.Default.CBB_rebalance_mma_C
                   + "," + Properties.Settings.Default.TB_rebalance_mma2_C
                + "," + Properties.Settings.Default.CBB_rebalance_mma2_C
                + "," + Properties.Settings.Default.CBB_rebalance_mma_배열_C
                   + "," + Properties.Settings.Default.TB_rebalance_dma1_C
                + "," + Properties.Settings.Default.CBB_rebalance_dma1_C
                   + "," + Properties.Settings.Default.TB_rebalance_dma2_C
                + "," + Properties.Settings.Default.CBB_rebalance_dma2_C
                + "," + Properties.Settings.Default.CBB_rebalance_dma_배열_C
                + "," + Properties.Settings.Default.TB_rebalance_suik_1_C
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_choice_C)
                + "," + Properties.Settings.Default.TB_rebalance_suik_2_C
                + "," + Properties.Settings.Default.combo_rebalance_suik_gubun_C
                + "," + Properties.Settings.Default.TB_rebalance_sell_ratio_C
                + "," + Properties.Settings.Default.combo_rebalance_sell_gubun_C
                + "," + Properties.Settings.Default.TB_rebalance_maemae_1_C
                + "," + Properties.Settings.Default.TB_rebalance_maemae_2_C
                + "," + Properties.Settings.Default.combo_rebalance_maemae_gubun_C
                + "," + Properties.Settings.Default.MT_rebalance_repeat_time_C
                + "," + Properties.Settings.Default.TB_rebalance_value_C
                + "," + Properties.Settings.Default.combo_rebalance_jumun_C
                + "," + Properties.Settings.Default.MTB_rebalance_Cancel_time_C
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_option_C)
                + "," + Properties.Settings.Default.TB_rebalance_sellratio1_C
                + "," + Properties.Settings.Default.리밸매도기준1_C
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume1_C
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel1_C
                + "," + Properties.Settings.Default.TB_rebalance_sellratio2_C
                + "," + Properties.Settings.Default.리밸매도기준2_C
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume2_C
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel2_C
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_매도체크_C)
                + "," + Properties.Settings.Default.CBB_rebalance_Selltime_C
                + "," + Properties.Settings.Default.TB_rebalance_감시_value_C
                + "," + Properties.Settings.Default.combo_rebalance_감시_jumun_C
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_1차_C)
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_down_C
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_mma_C
                + "," + Properties.Settings.Default.CBB_rebalance_TS_1차_mma_C
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_2차_C)
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_down_C
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_mma_C
                + "," + Properties.Settings.Default.CBB_rebalance_TS_2차_mma_C
                + ");" +
                "\n계좌관리_리밸런싱_D(" + 체크박스(Properties.Settings.Default.CB_rebalance_D)
                + "," + Properties.Settings.Default.MT_rebalance_starttime_D
                + "," + Properties.Settings.Default.MT_rebalance_stoptime_D
                + "," + Properties.Settings.Default.combo_rebalance_use_condition_D
                + "," + Properties.Settings.Default.combo_rebalance_condition_D
                + "," + Properties.Settings.Default.MTB_rebalance_delay_D
                + "," + Properties.Settings.Default.TB_rebalance_매입금_D
                + "," + Properties.Settings.Default.TB_rebalance_누적거래량_D
                + "," + Properties.Settings.Default.TB_rebalance_누적거래대금_D
               + "," + Properties.Settings.Default.TB_rebalance_mma_D
                + "," + Properties.Settings.Default.CBB_rebalance_mma_D
                   + "," + Properties.Settings.Default.TB_rebalance_mma2_D
                + "," + Properties.Settings.Default.CBB_rebalance_mma2_D
                + "," + Properties.Settings.Default.CBB_rebalance_mma_배열_D
                   + "," + Properties.Settings.Default.TB_rebalance_dma1_D
                + "," + Properties.Settings.Default.CBB_rebalance_dma1_D
                   + "," + Properties.Settings.Default.TB_rebalance_dma2_D
                + "," + Properties.Settings.Default.CBB_rebalance_dma2_D
                + "," + Properties.Settings.Default.CBB_rebalance_dma_배열_D
                + "," + Properties.Settings.Default.TB_rebalance_suik_1_D
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_choice_D)
                + "," + Properties.Settings.Default.TB_rebalance_suik_2_D
                + "," + Properties.Settings.Default.combo_rebalance_suik_gubun_D
                + "," + Properties.Settings.Default.TB_rebalance_sell_ratio_D
                + "," + Properties.Settings.Default.combo_rebalance_sell_gubun_D
                + "," + Properties.Settings.Default.TB_rebalance_maemae_1_D
                + "," + Properties.Settings.Default.TB_rebalance_maemae_2_D
                + "," + Properties.Settings.Default.combo_rebalance_maemae_gubun_D
                + "," + Properties.Settings.Default.MT_rebalance_repeat_time_D
                + "," + Properties.Settings.Default.TB_rebalance_value_D
                + "," + Properties.Settings.Default.combo_rebalance_jumun_D
                + "," + Properties.Settings.Default.MTB_rebalance_Cancel_time_D
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_option_D)
                + "," + Properties.Settings.Default.TB_rebalance_sellratio1_D
                + "," + Properties.Settings.Default.리밸매도기준1_D
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume1_D
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel1_D
                + "," + Properties.Settings.Default.TB_rebalance_sellratio2_D
                + "," + Properties.Settings.Default.리밸매도기준2_D
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume2_D
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel2_D
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_매도체크_D)
                + "," + Properties.Settings.Default.CBB_rebalance_Selltime_D
                + "," + Properties.Settings.Default.TB_rebalance_감시_value_D
                + "," + Properties.Settings.Default.combo_rebalance_감시_jumun_D
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_1차_D)
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_down_D
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_mma_D
                + "," + Properties.Settings.Default.CBB_rebalance_TS_1차_mma_D
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_2차_D)
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_down_D
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_mma_D
                + "," + Properties.Settings.Default.CBB_rebalance_TS_2차_mma_D
                + ");"
                + "\n계좌관리_리밸런싱_E(" + 체크박스(Properties.Settings.Default.CB_rebalance_E)
                + "," + Properties.Settings.Default.MT_rebalance_starttime_E
                + "," + Properties.Settings.Default.MT_rebalance_stoptime_E
                + "," + Properties.Settings.Default.combo_rebalance_use_condition_E
                + "," + Properties.Settings.Default.combo_rebalance_condition_E
                + "," + Properties.Settings.Default.MTB_rebalance_delay_E
                + "," + Properties.Settings.Default.TB_rebalance_매입금_E
                + "," + Properties.Settings.Default.TB_rebalance_누적거래량_E
                + "," + Properties.Settings.Default.TB_rebalance_누적거래대금_E
              + "," + Properties.Settings.Default.TB_rebalance_mma_E
                + "," + Properties.Settings.Default.CBB_rebalance_mma_E
                   + "," + Properties.Settings.Default.TB_rebalance_mma2_E
                + "," + Properties.Settings.Default.CBB_rebalance_mma2_E
                + "," + Properties.Settings.Default.CBB_rebalance_mma_배열_E
                   + "," + Properties.Settings.Default.TB_rebalance_dma1_E
                + "," + Properties.Settings.Default.CBB_rebalance_dma1_E
                   + "," + Properties.Settings.Default.TB_rebalance_dma2_E
                + "," + Properties.Settings.Default.CBB_rebalance_dma2_E
                + "," + Properties.Settings.Default.CBB_rebalance_dma_배열_E
                + "," + Properties.Settings.Default.TB_rebalance_suik_1_E
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_choice_E)
                + "," + Properties.Settings.Default.TB_rebalance_suik_2_E
                + "," + Properties.Settings.Default.combo_rebalance_suik_gubun_E
                + "," + Properties.Settings.Default.TB_rebalance_sell_ratio_E
                + "," + Properties.Settings.Default.combo_rebalance_sell_gubun_E
                + "," + Properties.Settings.Default.TB_rebalance_maemae_1_E
                + "," + Properties.Settings.Default.TB_rebalance_maemae_2_E
                + "," + Properties.Settings.Default.combo_rebalance_maemae_gubun_E
                + "," + Properties.Settings.Default.MT_rebalance_repeat_time_E
                + "," + Properties.Settings.Default.TB_rebalance_value_E
                + "," + Properties.Settings.Default.combo_rebalance_jumun_E
                + "," + Properties.Settings.Default.MTB_rebalance_Cancel_time_E
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_option_E)
                + "," + Properties.Settings.Default.TB_rebalance_sellratio1_E
                + "," + Properties.Settings.Default.리밸매도기준1_E
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume1_E
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel1_E
                + "," + Properties.Settings.Default.TB_rebalance_sellratio2_E
                + "," + Properties.Settings.Default.리밸매도기준2_E
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume2_E
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel2_E
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_매도체크_E)
                + "," + Properties.Settings.Default.CBB_rebalance_Selltime_E
                + "," + Properties.Settings.Default.TB_rebalance_감시_value_E
                + "," + Properties.Settings.Default.combo_rebalance_감시_jumun_E
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_1차_E)
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_down_E
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_mma_E
                + "," + Properties.Settings.Default.CBB_rebalance_TS_1차_mma_E
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_2차_E)
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_down_E
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_mma_E
                + "," + Properties.Settings.Default.CBB_rebalance_TS_2차_mma_E
                + ");"
                + "\n계좌관리_리밸런싱_F(" + 체크박스(Properties.Settings.Default.CB_rebalance_F)
                + "," + Properties.Settings.Default.MT_rebalance_starttime_F
                + "," + Properties.Settings.Default.MT_rebalance_stoptime_F
                + "," + Properties.Settings.Default.combo_rebalance_use_condition_F
                + "," + Properties.Settings.Default.combo_rebalance_condition_F
                + "," + Properties.Settings.Default.MTB_rebalance_delay_F
                + "," + Properties.Settings.Default.TB_rebalance_매입금_F
                + "," + Properties.Settings.Default.TB_rebalance_누적거래량_F
                + "," + Properties.Settings.Default.TB_rebalance_누적거래대금_F
               + "," + Properties.Settings.Default.TB_rebalance_mma_F
                + "," + Properties.Settings.Default.CBB_rebalance_mma_F
                   + "," + Properties.Settings.Default.TB_rebalance_mma2_F
                + "," + Properties.Settings.Default.CBB_rebalance_mma2_F
                + "," + Properties.Settings.Default.CBB_rebalance_mma_배열_F
                   + "," + Properties.Settings.Default.TB_rebalance_dma1_F
                + "," + Properties.Settings.Default.CBB_rebalance_dma1_F
                   + "," + Properties.Settings.Default.TB_rebalance_dma2_F
                + "," + Properties.Settings.Default.CBB_rebalance_dma2_F
                + "," + Properties.Settings.Default.CBB_rebalance_dma_배열_F
                + "," + Properties.Settings.Default.TB_rebalance_suik_1_F
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_choice_F)
                + "," + Properties.Settings.Default.TB_rebalance_suik_2_F
                + "," + Properties.Settings.Default.combo_rebalance_suik_gubun_F
                + "," + Properties.Settings.Default.TB_rebalance_sell_ratio_F
                + "," + Properties.Settings.Default.combo_rebalance_sell_gubun_F
                + "," + Properties.Settings.Default.TB_rebalance_maemae_1_F
                + "," + Properties.Settings.Default.TB_rebalance_maemae_2_F
                + "," + Properties.Settings.Default.combo_rebalance_maemae_gubun_F
                + "," + Properties.Settings.Default.MT_rebalance_repeat_time_F
                + "," + Properties.Settings.Default.TB_rebalance_value_F
                + "," + Properties.Settings.Default.combo_rebalance_jumun_F
                + "," + Properties.Settings.Default.MTB_rebalance_Cancel_time_F
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_option_F)
                + "," + Properties.Settings.Default.TB_rebalance_sellratio1_F
                + "," + Properties.Settings.Default.리밸매도기준1_F
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume1_F
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel1_F
                + "," + Properties.Settings.Default.TB_rebalance_sellratio2_F
                + "," + Properties.Settings.Default.리밸매도기준2_F
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume2_F
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel2_F
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_매도체크_F)
                + "," + Properties.Settings.Default.CBB_rebalance_Selltime_F
                + "," + Properties.Settings.Default.TB_rebalance_감시_value_F
                + "," + Properties.Settings.Default.combo_rebalance_감시_jumun_F
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_1차_F)
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_down_F
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_mma_F
                + "," + Properties.Settings.Default.CBB_rebalance_TS_1차_mma_F
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_2차_F)
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_down_F
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_mma_F
                + "," + Properties.Settings.Default.CBB_rebalance_TS_2차_mma_F
                + ");"
                + "\n계좌관리_리밸런싱_G(" + 체크박스(Properties.Settings.Default.CB_rebalance_G)
                + "," + Properties.Settings.Default.MT_rebalance_starttime_G
                + "," + Properties.Settings.Default.MT_rebalance_stoptime_G
                + "," + Properties.Settings.Default.combo_rebalance_use_condition_G
                + "," + Properties.Settings.Default.combo_rebalance_condition_G
                + "," + Properties.Settings.Default.MTB_rebalance_delay_G
                + "," + Properties.Settings.Default.TB_rebalance_매입금_G
                + "," + Properties.Settings.Default.TB_rebalance_누적거래량_G
                + "," + Properties.Settings.Default.TB_rebalance_누적거래대금_G
                + "," + Properties.Settings.Default.TB_rebalance_mma_G
                + "," + Properties.Settings.Default.CBB_rebalance_mma_G
                   + "," + Properties.Settings.Default.TB_rebalance_mma2_G
                + "," + Properties.Settings.Default.CBB_rebalance_mma2_G
                + "," + Properties.Settings.Default.CBB_rebalance_mma_배열_G
                   + "," + Properties.Settings.Default.TB_rebalance_dma1_G
                + "," + Properties.Settings.Default.CBB_rebalance_dma1_G
                   + "," + Properties.Settings.Default.TB_rebalance_dma2_G
                + "," + Properties.Settings.Default.CBB_rebalance_dma2_G
                + "," + Properties.Settings.Default.CBB_rebalance_dma_배열_G
                + "," + Properties.Settings.Default.TB_rebalance_suik_1_G
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_choice_G)
                + "," + Properties.Settings.Default.TB_rebalance_suik_2_G
                + "," + Properties.Settings.Default.combo_rebalance_suik_gubun_G
                + "," + Properties.Settings.Default.TB_rebalance_sell_ratio_G
                + "," + Properties.Settings.Default.combo_rebalance_sell_gubun_G
                + "," + Properties.Settings.Default.TB_rebalance_maemae_1_G
                + "," + Properties.Settings.Default.TB_rebalance_maemae_2_G
                + "," + Properties.Settings.Default.combo_rebalance_maemae_gubun_G
                + "," + Properties.Settings.Default.MT_rebalance_repeat_time_G
                + "," + Properties.Settings.Default.TB_rebalance_value_G
                + "," + Properties.Settings.Default.combo_rebalance_jumun_G
                + "," + Properties.Settings.Default.MTB_rebalance_Cancel_time_G
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_option_G)
                + "," + Properties.Settings.Default.TB_rebalance_sellratio1_G
                + "," + Properties.Settings.Default.리밸매도기준1_G
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume1_G
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel1_G
                + "," + Properties.Settings.Default.TB_rebalance_sellratio2_G
                + "," + Properties.Settings.Default.리밸매도기준2_G
                + "," + Properties.Settings.Default.TB_rebalance_sellvolume2_G
                + "," + Properties.Settings.Default.TB_rebalance_sellcancel2_G
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_매도체크_G)
                + "," + Properties.Settings.Default.CBB_rebalance_Selltime_G
                + "," + Properties.Settings.Default.TB_rebalance_감시_value_G
                + "," + Properties.Settings.Default.combo_rebalance_감시_jumun_G
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_1차_G)
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_down_G
                + "," + Properties.Settings.Default.TB_rebalance_TS_1차_mma_G
                + "," + Properties.Settings.Default.CBB_rebalance_TS_1차_mma_G
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_TS_2차_G)
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_down_G
                + "," + Properties.Settings.Default.TB_rebalance_TS_2차_mma_G
                + "," + Properties.Settings.Default.CBB_rebalance_TS_2차_mma_G
                + ");\n"
                + "\n계좌관리_잔고청산_A(" + 체크박스(Properties.Settings.Default.CB_Liquidation_A)
                + "," + Properties.Settings.Default.MTB_Liquidation_Starttime_A
                + "," + Properties.Settings.Default.MTB_Liquidation_Stoptime_A
                + "," + Properties.Settings.Default.CBB_Liquidation_use_condition_A
                + "," + Properties.Settings.Default.CBB_Liquidation_condition_A
                + "," + Properties.Settings.Default.MTB_Liquidation_delay_A
                + "," + Properties.Settings.Default.TB_잔고청산_매입금1_A
                + "," + Properties.Settings.Default.TB_잔고청산_매입금2_A
                + "," + Properties.Settings.Default.TB_Liquidation_mma_A
                + "," + Properties.Settings.Default.CBB_Liquidation_mma_A
                + "," + Properties.Settings.Default.TB_Liquidation_suik_1_A
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_choice_A)
                + "," + Properties.Settings.Default.TB_Liquidation_suik_2_A
                + "," + Properties.Settings.Default.CBB_Liquidation_suik_gubun_A
                + "," + Properties.Settings.Default.TB_Liquidation_sell_ratio_A
                + "," + Properties.Settings.Default.CBB_Liquidation_sell_gubun_A
                + "," + Properties.Settings.Default.TB_Liquidation_maemae_1_A
                + "," + Properties.Settings.Default.TB_Liquidation_maemae_2_A
                + "," + Properties.Settings.Default.MT_Liquidation_repeat_time_A
                + "," + Properties.Settings.Default.TB_Liquidation_value_A
                + "," + Properties.Settings.Default.CBB_Liquidation_jumun_A
                + "," + Properties.Settings.Default.MTB_Liquidation_Cancel_time_A
                + "," + Properties.Settings.Default.CBB_Liquidation_Cancel_A
                + "," + Properties.Settings.Default.MTB_Liquidation_repeat_A
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_SellStop_A)
                + "," + 체크박스(Properties.Settings.Default.CB_추매금지_Liquidation_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_강제매도_A)
                + "," + 체크박스(Properties.Settings.Default.CB_수익보전_Liquidation_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_TS_A)
                + "," + Properties.Settings.Default.TB_Liquidation_TS_down_A
                + "," + Properties.Settings.Default.TB_Liquidation_TS_mma_A
                + "," + Properties.Settings.Default.CBB_Liquidation_TS_mma_A
                + "," + Properties.Settings.Default.TB_Liquidation_TS_dma_A
                + "," + Properties.Settings.Default.CBB_Liquidation_TS_dma_A
                + ");\n계좌관리_잔고청산_B(" + 체크박스(Properties.Settings.Default.CB_Liquidation_B)
                + "," + Properties.Settings.Default.MTB_Liquidation_Starttime_B
                + "," + Properties.Settings.Default.MTB_Liquidation_Stoptime_B
                + "," + Properties.Settings.Default.CBB_Liquidation_use_condition_B
                + "," + Properties.Settings.Default.CBB_Liquidation_condition_B
                + "," + Properties.Settings.Default.MTB_Liquidation_delay_B
                + "," + Properties.Settings.Default.TB_잔고청산_매입금1_B
                + "," + Properties.Settings.Default.TB_잔고청산_매입금2_B
                + "," + Properties.Settings.Default.TB_Liquidation_mma_B
                + "," + Properties.Settings.Default.CBB_Liquidation_mma_B
                + "," + Properties.Settings.Default.TB_Liquidation_suik_1_B
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_choice_B)
                + "," + Properties.Settings.Default.TB_Liquidation_suik_2_B
                + "," + Properties.Settings.Default.CBB_Liquidation_suik_gubun_B
                + "," + Properties.Settings.Default.TB_Liquidation_sell_ratio_B
                + "," + Properties.Settings.Default.CBB_Liquidation_sell_gubun_B
                + "," + Properties.Settings.Default.TB_Liquidation_maemae_1_B
                + "," + Properties.Settings.Default.TB_Liquidation_maemae_2_B
                + "," + Properties.Settings.Default.MT_Liquidation_repeat_time_B
                + "," + Properties.Settings.Default.TB_Liquidation_value_B
                + "," + Properties.Settings.Default.CBB_Liquidation_jumun_B
                + "," + Properties.Settings.Default.MTB_Liquidation_Cancel_time_B
                + "," + Properties.Settings.Default.CBB_Liquidation_Cancel_B
                + "," + Properties.Settings.Default.MTB_Liquidation_repeat_B
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_SellStop_B)
                + "," + 체크박스(Properties.Settings.Default.CB_추매금지_Liquidation_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_강제매도_B)
                + "," + 체크박스(Properties.Settings.Default.CB_수익보전_Liquidation_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_TS_B)
                + "," + Properties.Settings.Default.TB_Liquidation_TS_down_B
                + "," + Properties.Settings.Default.TB_Liquidation_TS_mma_B
                + "," + Properties.Settings.Default.CBB_Liquidation_TS_mma_B
                + "," + Properties.Settings.Default.TB_Liquidation_TS_dma_B
                + "," + Properties.Settings.Default.CBB_Liquidation_TS_dma_B
                + ");\n계좌관리_잔고청산_C(" + 체크박스(Properties.Settings.Default.CB_Liquidation_C)
                + "," + Properties.Settings.Default.MTB_Liquidation_Starttime_C
                + "," + Properties.Settings.Default.MTB_Liquidation_Stoptime_C
                + "," + Properties.Settings.Default.CBB_Liquidation_use_condition_C
                + "," + Properties.Settings.Default.CBB_Liquidation_condition_C
                + "," + Properties.Settings.Default.MTB_Liquidation_delay_C
                + "," + Properties.Settings.Default.TB_잔고청산_매입금1_C
                + "," + Properties.Settings.Default.TB_잔고청산_매입금2_C
                + "," + Properties.Settings.Default.TB_Liquidation_mma_C
                + "," + Properties.Settings.Default.CBB_Liquidation_mma_C
                + "," + Properties.Settings.Default.TB_Liquidation_suik_1_C
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_choice_C)
                + "," + Properties.Settings.Default.TB_Liquidation_suik_2_C
                + "," + Properties.Settings.Default.CBB_Liquidation_suik_gubun_C
                + "," + Properties.Settings.Default.TB_Liquidation_sell_ratio_C
                + "," + Properties.Settings.Default.CBB_Liquidation_sell_gubun_C
                + "," + Properties.Settings.Default.TB_Liquidation_maemae_1_C
                + "," + Properties.Settings.Default.TB_Liquidation_maemae_2_C
                + "," + Properties.Settings.Default.MT_Liquidation_repeat_time_C
                + "," + Properties.Settings.Default.TB_Liquidation_value_C
                + "," + Properties.Settings.Default.CBB_Liquidation_jumun_C
                + "," + Properties.Settings.Default.MTB_Liquidation_Cancel_time_C
                + "," + Properties.Settings.Default.CBB_Liquidation_Cancel_C
                + "," + Properties.Settings.Default.MTB_Liquidation_repeat_C
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_SellStop_C)
                + "," + 체크박스(Properties.Settings.Default.CB_추매금지_Liquidation_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_강제매도_C)
                + "," + 체크박스(Properties.Settings.Default.CB_수익보전_Liquidation_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_TS_C)
                + "," + Properties.Settings.Default.TB_Liquidation_TS_down_C
                + "," + Properties.Settings.Default.TB_Liquidation_TS_mma_C
                + "," + Properties.Settings.Default.CBB_Liquidation_TS_mma_C
                + "," + Properties.Settings.Default.TB_Liquidation_TS_dma_C
                + "," + Properties.Settings.Default.CBB_Liquidation_TS_dma_C
                + ");\n"
                + "\n계좌관리_실현손익담보손실매도_A(" + 체크박스(Properties.Settings.Default.CB_cut_A)
                + "," + Properties.Settings.Default.MTB_cut_time_A
                + "," + Properties.Settings.Default.TB_cut_수익금1_A
                + "," + Properties.Settings.Default.TB_cut_수익금2_A
                + "," + Properties.Settings.Default.TB_cut_남길퍼_A
                + "," + Properties.Settings.Default.TB_cut_won_A
                + "," + Properties.Settings.Default.TB_cut_P_A
                + "," + Properties.Settings.Default.TB_cut_ratio_A
                + "," + Properties.Settings.Default.CBB_cut_gubun_A
                + "," + Properties.Settings.Default.TB_cut_value_A
                + "," + Properties.Settings.Default.CBB_cut_jumun_A
                + "," + Properties.Settings.Default.MTB_cut_cansel_time_A
                + ");\n계좌관리_실현손익담보손실매도_B(" + 체크박스(Properties.Settings.Default.CB_cut_B)
                + "," + Properties.Settings.Default.MTB_cut_time_B
                + "," + Properties.Settings.Default.TB_cut_수익금1_B
                + "," + Properties.Settings.Default.TB_cut_수익금2_B
                + "," + Properties.Settings.Default.TB_cut_남길퍼_B
                + "," + Properties.Settings.Default.TB_cut_won_B
                + "," + Properties.Settings.Default.TB_cut_P_B
                + "," + Properties.Settings.Default.TB_cut_ratio_B
                + "," + Properties.Settings.Default.CBB_cut_gubun_B
                + "," + Properties.Settings.Default.TB_cut_value_B
                + "," + Properties.Settings.Default.CBB_cut_jumun_B
                + "," + Properties.Settings.Default.MTB_cut_cansel_time_B
                + ");\n계좌관리_실현손익담보손실매도_C(" + 체크박스(Properties.Settings.Default.CB_cut_C)
                + "," + Properties.Settings.Default.MTB_cut_time_C
                + "," + Properties.Settings.Default.TB_cut_수익금1_C
                + "," + Properties.Settings.Default.TB_cut_수익금2_C
                + "," + Properties.Settings.Default.TB_cut_남길퍼_C
                + "," + Properties.Settings.Default.TB_cut_won_C
                + "," + Properties.Settings.Default.TB_cut_P_C
                + "," + Properties.Settings.Default.TB_cut_ratio_C
                + "," + Properties.Settings.Default.CBB_cut_gubun_C
                + "," + Properties.Settings.Default.TB_cut_value_C
                + "," + Properties.Settings.Default.CBB_cut_jumun_C
                + "," + Properties.Settings.Default.MTB_cut_cansel_time_C
                + ");";
            }
            void 특수설정()
            {
                Properties.Settings.Default.특수설정 =
               "특수설정_신규그룹(" + Properties.Settings.Default.combo_신규그룹_A
               + "," + Properties.Settings.Default.combo_신규그룹_B
               + "," + Properties.Settings.Default.combo_신규그룹_C
               + ");\n특수설정_신규그룹(" + 체크박스(Properties.Settings.Default.CB_매매기간_기준금)
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_오전)
               + "," + Properties.Settings.Default.TB_매매기간_오전주문시간
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_오후)
               + "," + Properties.Settings.Default.TB_매매기간_오후주문시간
               + ");\n\n특수설정_매매기간주문_A(" + Properties.Settings.Default.CBB_매매기간_trading_A
               + "," + Properties.Settings.Default.MTB_매매기간_기간_A
               + "," + Properties.Settings.Default.CBB_매매기간_주문시간_A
               + "," + Properties.Settings.Default.TB_매매기간_기준_A
               + "," + Properties.Settings.Default.CBB_매매기간_기준_A
               + "," + Properties.Settings.Default.TB_매매기간_ratio_A
               + "," + Properties.Settings.Default.CBB_매매기간_choice_A
               + "," + Properties.Settings.Default.TB_매매기간_value_A
               + "," + Properties.Settings.Default.CBB_매매기간_Jumun_A
               + "," + Properties.Settings.Default.TB_매매기간_취소시간_A
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_강제매도_A)
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_수익보전_A)
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_TS_A)
               + "," + Properties.Settings.Default.TB_매매기간_TS_down_A
               + "," + Properties.Settings.Default.TB_매매기간_TS_mma_A
               + "," + Properties.Settings.Default.CBB_매매기간_TS_mma_A
               + "," + Properties.Settings.Default.TB_매매기간_TS_dma_A
               + "," + Properties.Settings.Default.CBB_매매기간_TS_dma_A
               + ");\n특수설정_매매기간주문_B(" + Properties.Settings.Default.CBB_매매기간_trading_B
               + "," + Properties.Settings.Default.MTB_매매기간_기간_B
               + "," + Properties.Settings.Default.CBB_매매기간_주문시간_B
               + "," + Properties.Settings.Default.TB_매매기간_기준_B
               + "," + Properties.Settings.Default.CBB_매매기간_기준_B
               + "," + Properties.Settings.Default.TB_매매기간_ratio_B
               + "," + Properties.Settings.Default.CBB_매매기간_choice_B
               + "," + Properties.Settings.Default.TB_매매기간_value_B
               + "," + Properties.Settings.Default.CBB_매매기간_Jumun_B
               + "," + Properties.Settings.Default.TB_매매기간_취소시간_B
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_강제매도_B)
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_수익보전_B)
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_TS_B)
               + "," + Properties.Settings.Default.TB_매매기간_TS_down_B
               + "," + Properties.Settings.Default.TB_매매기간_TS_mma_B
               + "," + Properties.Settings.Default.CBB_매매기간_TS_mma_B
               + "," + Properties.Settings.Default.TB_매매기간_TS_dma_B
               + "," + Properties.Settings.Default.CBB_매매기간_TS_dma_B
               + ");\n특수설정_매매기간주문_C(" + Properties.Settings.Default.CBB_매매기간_trading_C
               + "," + Properties.Settings.Default.MTB_매매기간_기간_C
               + "," + Properties.Settings.Default.CBB_매매기간_주문시간_C
               + "," + Properties.Settings.Default.TB_매매기간_기준_C
               + "," + Properties.Settings.Default.CBB_매매기간_기준_C
               + "," + Properties.Settings.Default.TB_매매기간_ratio_C
               + "," + Properties.Settings.Default.CBB_매매기간_choice_C
               + "," + Properties.Settings.Default.TB_매매기간_value_C
               + "," + Properties.Settings.Default.CBB_매매기간_Jumun_C
               + "," + Properties.Settings.Default.TB_매매기간_취소시간_C
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_강제매도_C)
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_수익보전_C)
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_TS_C)
               + "," + Properties.Settings.Default.TB_매매기간_TS_down_C
               + "," + Properties.Settings.Default.TB_매매기간_TS_mma_C
               + "," + Properties.Settings.Default.CBB_매매기간_TS_mma_C
               + "," + Properties.Settings.Default.TB_매매기간_TS_dma_C
               + "," + Properties.Settings.Default.CBB_매매기간_TS_dma_C
               + ");\n특수설정_매매기간주문_D(" + Properties.Settings.Default.CBB_매매기간_trading_D
               + "," + Properties.Settings.Default.MTB_매매기간_기간_D
               + "," + Properties.Settings.Default.CBB_매매기간_주문시간_D
               + "," + Properties.Settings.Default.TB_매매기간_기준_D
               + "," + Properties.Settings.Default.CBB_매매기간_기준_D
               + "," + Properties.Settings.Default.TB_매매기간_ratio_D
               + "," + Properties.Settings.Default.CBB_매매기간_choice_D
               + "," + Properties.Settings.Default.TB_매매기간_value_D
               + "," + Properties.Settings.Default.CBB_매매기간_Jumun_D
               + "," + Properties.Settings.Default.TB_매매기간_취소시간_D
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_강제매도_D)
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_수익보전_D)
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_TS_D)
               + "," + Properties.Settings.Default.TB_매매기간_TS_down_D
               + "," + Properties.Settings.Default.TB_매매기간_TS_mma_D
               + "," + Properties.Settings.Default.CBB_매매기간_TS_mma_D
               + "," + Properties.Settings.Default.TB_매매기간_TS_dma_D
               + "," + Properties.Settings.Default.CBB_매매기간_TS_dma_D
               + ");\n특수설정_매매기간주문_E(" + Properties.Settings.Default.CBB_매매기간_trading_E
               + "," + Properties.Settings.Default.MTB_매매기간_기간_E
               + "," + Properties.Settings.Default.CBB_매매기간_주문시간_E
               + "," + Properties.Settings.Default.TB_매매기간_기준_E
               + "," + Properties.Settings.Default.CBB_매매기간_기준_E
               + "," + Properties.Settings.Default.TB_매매기간_ratio_E
               + "," + Properties.Settings.Default.CBB_매매기간_choice_E
               + "," + Properties.Settings.Default.TB_매매기간_value_E
               + "," + Properties.Settings.Default.CBB_매매기간_Jumun_E
               + "," + Properties.Settings.Default.TB_매매기간_취소시간_E
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_강제매도_E)
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_수익보전_E)
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_TS_E)
               + "," + Properties.Settings.Default.TB_매매기간_TS_down_E
               + "," + Properties.Settings.Default.TB_매매기간_TS_mma_E
               + "," + Properties.Settings.Default.CBB_매매기간_TS_mma_E
               + "," + Properties.Settings.Default.TB_매매기간_TS_dma_E
               + "," + Properties.Settings.Default.CBB_매매기간_TS_dma_E
               + ");\n특수설정_매매기간주문_F(" + Properties.Settings.Default.CBB_매매기간_trading_F
               + "," + Properties.Settings.Default.MTB_매매기간_기간_F
               + "," + Properties.Settings.Default.CBB_매매기간_주문시간_F
               + "," + Properties.Settings.Default.TB_매매기간_기준_F
               + "," + Properties.Settings.Default.CBB_매매기간_기준_F
               + "," + Properties.Settings.Default.TB_매매기간_ratio_F
               + "," + Properties.Settings.Default.CBB_매매기간_choice_F
               + "," + Properties.Settings.Default.TB_매매기간_value_F
               + "," + Properties.Settings.Default.CBB_매매기간_Jumun_F
               + "," + Properties.Settings.Default.TB_매매기간_취소시간_F
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_강제매도_F)
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_수익보전_F)
               + "," + 체크박스(Properties.Settings.Default.CB_매매기간_TS_F)
               + "," + Properties.Settings.Default.TB_매매기간_TS_down_F
               + "," + Properties.Settings.Default.TB_매매기간_TS_mma_F
               + "," + Properties.Settings.Default.CBB_매매기간_TS_mma_F
               + "," + Properties.Settings.Default.TB_매매기간_TS_dma_F
               + "," + Properties.Settings.Default.CBB_매매기간_TS_dma_F
               + ");";
            }
            void 매매그룹설정()
            {
                Properties.Settings.Default.매매그룹설정 =
                "매매그룹설정_익절(" + 체크박스(Properties.Settings.Default.CB_IK_group_A)
                + "," + 체크박스(Properties.Settings.Default.CB_IK_group_B)
                + "," + 체크박스(Properties.Settings.Default.CB_IK_group_C)
                + "," + 체크박스(Properties.Settings.Default.CB_IK_group_D)
                + "," + 체크박스(Properties.Settings.Default.CB_IK_group_E)
                + "," + 체크박스(Properties.Settings.Default.CB_IK_group_F)
                + "," + 체크박스(Properties.Settings.Default.CB_IK_group_G)
                + "," + 체크박스(Properties.Settings.Default.CB_IK_group_H)
                + "," + 체크박스(Properties.Settings.Default.CB_IK_group_I)
                + "," + 체크박스(Properties.Settings.Default.CB_IK_group_J)
                + "," + 체크박스(Properties.Settings.Default.CB_IK_group_K)
                + "," + 체크박스(Properties.Settings.Default.CB_IK_group_L)
                + ");\n매매그룹설정_손절(" + 체크박스(Properties.Settings.Default.CB_손절_A)
                + "," + 체크박스(Properties.Settings.Default.CB_손절_B)
                + "," + 체크박스(Properties.Settings.Default.CB_손절_C)
                + "," + 체크박스(Properties.Settings.Default.CB_손절_D)
                + "," + 체크박스(Properties.Settings.Default.CB_손절_E)
                + "," + 체크박스(Properties.Settings.Default.CB_손절_F)
                + "," + 체크박스(Properties.Settings.Default.CB_손절_G)
                + "," + 체크박스(Properties.Settings.Default.CB_손절_H)
                + "," + 체크박스(Properties.Settings.Default.CB_손절_I)
                + "," + 체크박스(Properties.Settings.Default.CB_손절_J)
                + "," + 체크박스(Properties.Settings.Default.CB_손절_K)
                + "," + 체크박스(Properties.Settings.Default.CB_손절_L)
                + ");\n매매그룹설정_반복A(" + 체크박스(Properties.Settings.Default.CB_Repeat_A_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_A_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_A_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_A_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_A_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_A_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_A_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_A_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_A_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_A_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_A_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_A_L)
                + ");\n매매그룹설정_반복B(" + 체크박스(Properties.Settings.Default.CB_Repeat_B_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_B_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_B_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_B_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_B_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_B_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_B_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_B_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_B_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_B_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_B_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_B_L)
                + ");\n매매그룹설정_반복C(" + 체크박스(Properties.Settings.Default.CB_Repeat_C_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_C_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_C_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_C_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_C_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_C_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_C_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_C_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_C_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_C_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_C_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_C_L)
                + ");\n매매그룹설정_반복D(" + 체크박스(Properties.Settings.Default.CB_Repeat_D_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_D_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_D_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_D_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_D_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_D_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_D_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_D_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_D_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_D_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_D_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_D_L)
                + ");\n매매그룹설정_반복E(" + 체크박스(Properties.Settings.Default.CB_Repeat_E_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_E_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_E_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_E_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_E_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_E_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_E_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_E_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_E_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_E_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_E_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_E_L)
                + ");\n매매그룹설정_반복F(" + 체크박스(Properties.Settings.Default.CB_Repeat_F_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_F_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_F_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_F_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_F_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_F_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_F_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_F_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_F_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_F_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_F_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_F_L)
                + ");\n매매그룹설정_반복G(" + 체크박스(Properties.Settings.Default.CB_Repeat_G_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_G_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_G_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_G_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_G_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_G_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_G_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_G_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_G_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_G_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_G_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_G_L)
                + ");\n매매그룹설정_반복H(" + 체크박스(Properties.Settings.Default.CB_Repeat_H_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_H_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_H_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_H_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_H_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_H_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_H_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_H_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_H_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_H_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_H_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_H_L)
                + ");\n매매그룹설정_반복I(" + 체크박스(Properties.Settings.Default.CB_Repeat_I_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_I_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_I_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_I_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_I_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_I_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_I_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_I_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_I_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_I_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_I_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_I_L)
                + ");\n매매그룹설정_반복J(" + 체크박스(Properties.Settings.Default.CB_Repeat_J_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_J_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_J_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_J_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_J_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_J_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_J_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_J_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_J_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_J_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_J_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_J_L)
                + ");\n매매그룹설정_반복K(" + 체크박스(Properties.Settings.Default.CB_Repeat_K_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_K_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_K_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_K_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_K_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_K_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_K_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_K_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_K_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_K_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_K_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_K_L)
                + ");\n매매그룹설정_반복L(" + 체크박스(Properties.Settings.Default.CB_Repeat_L_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_L_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_L_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_L_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_L_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_L_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_L_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_L_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_L_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_L_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_L_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_L_L)
                + ");\n매매그룹설정_반복M(" + 체크박스(Properties.Settings.Default.CB_Repeat_M_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_M_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_M_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_M_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_M_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_M_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_M_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_M_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_M_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_M_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_M_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_M_L)
                + ");\n매매그룹설정_반복N(" + 체크박스(Properties.Settings.Default.CB_Repeat_N_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_N_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_N_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_N_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_N_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_N_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_N_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_N_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_N_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_N_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_N_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Repeat_N_L)
                + ");\n매매그룹설정_리밸_A(" + 체크박스(Properties.Settings.Default.CB_rebalance_A_A)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_A_B)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_A_C)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_A_D)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_A_E)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_A_F)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_A_G)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_A_H)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_A_I)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_A_J)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_A_K)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_A_L)
                + ");\n매매그룹설정_리밸_B(" + 체크박스(Properties.Settings.Default.CB_rebalance_B_A)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_B_B)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_B_C)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_B_D)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_B_E)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_B_F)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_B_G)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_B_H)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_B_I)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_B_J)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_B_K)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_B_L)
                + ");\n매매그룹설정_리밸_C(" + 체크박스(Properties.Settings.Default.CB_rebalance_C_A)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_C_B)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_C_C)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_C_D)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_C_E)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_C_F)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_C_G)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_C_H)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_C_I)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_C_J)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_C_K)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_C_L)
                + ");\n매매그룹설정_리밸_D(" + 체크박스(Properties.Settings.Default.CB_rebalance_D_A)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_D_B)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_D_C)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_D_D)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_D_E)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_D_F)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_D_G)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_D_H)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_D_I)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_D_J)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_D_K)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_D_L)
                + ");\n매매그룹설정_리밸_E(" + 체크박스(Properties.Settings.Default.CB_rebalance_E_A)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_E_B)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_E_C)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_E_D)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_E_E)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_E_F)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_E_G)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_E_H)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_E_I)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_E_J)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_E_K)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_E_L)
                + ");\n매매그룹설정_리밸_F(" + 체크박스(Properties.Settings.Default.CB_rebalance_F_A)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_F_B)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_F_C)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_F_D)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_F_E)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_F_F)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_F_G)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_F_H)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_F_I)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_F_J)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_F_K)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_F_L)
                + ");\n매매그룹설정_리밸_G(" + 체크박스(Properties.Settings.Default.CB_rebalance_G_A)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_G_B)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_G_C)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_G_D)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_G_E)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_G_F)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_G_G)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_G_H)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_G_I)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_G_J)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_G_K)
                + "," + 체크박스(Properties.Settings.Default.CB_rebalance_G_L)
                  + ");\n매매그룹설정_청산_A(" + 체크박스(Properties.Settings.Default.CB_Liquidation_A_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_A_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_A_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_A_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_A_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_A_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_A_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_A_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_A_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_A_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_A_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_A_L)
                   + ");\n매매그룹설정_청산_B(" + 체크박스(Properties.Settings.Default.CB_Liquidation_B_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_B_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_B_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_B_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_B_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_B_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_B_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_B_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_B_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_B_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_B_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_B_L)
                   + ");\n매매그룹설정_청산_C(" + 체크박스(Properties.Settings.Default.CB_Liquidation_C_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_C_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_C_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_C_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_C_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_C_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_C_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_C_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_C_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_C_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_C_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Liquidation_C_L)
                    + ");\n매매그룹설정_기간매도_A(" + 체크박스(Properties.Settings.Default.CB_day_A_A)
                + "," + 체크박스(Properties.Settings.Default.CB_day_A_B)
                + "," + 체크박스(Properties.Settings.Default.CB_day_A_C)
                + "," + 체크박스(Properties.Settings.Default.CB_day_A_D)
                + "," + 체크박스(Properties.Settings.Default.CB_day_A_E)
                + "," + 체크박스(Properties.Settings.Default.CB_day_A_F)
                + "," + 체크박스(Properties.Settings.Default.CB_day_A_G)
                + "," + 체크박스(Properties.Settings.Default.CB_day_A_H)
                + "," + 체크박스(Properties.Settings.Default.CB_day_A_I)
                + "," + 체크박스(Properties.Settings.Default.CB_day_A_J)
                + "," + 체크박스(Properties.Settings.Default.CB_day_A_K)
                + "," + 체크박스(Properties.Settings.Default.CB_day_A_L)
                     + ");\n매매그룹설정_기간매도_B(" + 체크박스(Properties.Settings.Default.CB_day_B_A)
                + "," + 체크박스(Properties.Settings.Default.CB_day_B_B)
                + "," + 체크박스(Properties.Settings.Default.CB_day_B_C)
                + "," + 체크박스(Properties.Settings.Default.CB_day_B_D)
                + "," + 체크박스(Properties.Settings.Default.CB_day_B_E)
                + "," + 체크박스(Properties.Settings.Default.CB_day_B_F)
                + "," + 체크박스(Properties.Settings.Default.CB_day_B_G)
                + "," + 체크박스(Properties.Settings.Default.CB_day_B_H)
                + "," + 체크박스(Properties.Settings.Default.CB_day_B_I)
                + "," + 체크박스(Properties.Settings.Default.CB_day_B_J)
                + "," + 체크박스(Properties.Settings.Default.CB_day_B_K)
                + "," + 체크박스(Properties.Settings.Default.CB_day_B_L)
                     + ");\n매매그룹설정_기간매도_C(" + 체크박스(Properties.Settings.Default.CB_day_C_A)
                + "," + 체크박스(Properties.Settings.Default.CB_day_C_B)
                + "," + 체크박스(Properties.Settings.Default.CB_day_C_C)
                + "," + 체크박스(Properties.Settings.Default.CB_day_C_D)
                + "," + 체크박스(Properties.Settings.Default.CB_day_C_E)
                + "," + 체크박스(Properties.Settings.Default.CB_day_C_F)
                + "," + 체크박스(Properties.Settings.Default.CB_day_C_G)
                + "," + 체크박스(Properties.Settings.Default.CB_day_C_H)
                + "," + 체크박스(Properties.Settings.Default.CB_day_C_I)
                + "," + 체크박스(Properties.Settings.Default.CB_day_C_J)
                + "," + 체크박스(Properties.Settings.Default.CB_day_C_K)
                + "," + 체크박스(Properties.Settings.Default.CB_day_C_L)
                     + ");\n매매그룹설정_기간매도_D(" + 체크박스(Properties.Settings.Default.CB_day_D_A)
                + "," + 체크박스(Properties.Settings.Default.CB_day_D_B)
                + "," + 체크박스(Properties.Settings.Default.CB_day_D_C)
                + "," + 체크박스(Properties.Settings.Default.CB_day_D_D)
                + "," + 체크박스(Properties.Settings.Default.CB_day_D_E)
                + "," + 체크박스(Properties.Settings.Default.CB_day_D_F)
                + "," + 체크박스(Properties.Settings.Default.CB_day_D_G)
                + "," + 체크박스(Properties.Settings.Default.CB_day_D_H)
                + "," + 체크박스(Properties.Settings.Default.CB_day_D_I)
                + "," + 체크박스(Properties.Settings.Default.CB_day_D_J)
                + "," + 체크박스(Properties.Settings.Default.CB_day_D_K)
                + "," + 체크박스(Properties.Settings.Default.CB_day_D_L)
                     + ");\n매매그룹설정_기간매도_E(" + 체크박스(Properties.Settings.Default.CB_day_E_A)
                + "," + 체크박스(Properties.Settings.Default.CB_day_E_B)
                + "," + 체크박스(Properties.Settings.Default.CB_day_E_C)
                + "," + 체크박스(Properties.Settings.Default.CB_day_E_D)
                + "," + 체크박스(Properties.Settings.Default.CB_day_E_E)
                + "," + 체크박스(Properties.Settings.Default.CB_day_E_F)
                + "," + 체크박스(Properties.Settings.Default.CB_day_E_G)
                + "," + 체크박스(Properties.Settings.Default.CB_day_E_H)
                + "," + 체크박스(Properties.Settings.Default.CB_day_E_I)
                + "," + 체크박스(Properties.Settings.Default.CB_day_E_J)
                + "," + 체크박스(Properties.Settings.Default.CB_day_E_K)
                + "," + 체크박스(Properties.Settings.Default.CB_day_E_L)
                     + ");\n매매그룹설정_기간매도_F(" + 체크박스(Properties.Settings.Default.CB_day_F_A)
                + "," + 체크박스(Properties.Settings.Default.CB_day_F_B)
                + "," + 체크박스(Properties.Settings.Default.CB_day_F_C)
                + "," + 체크박스(Properties.Settings.Default.CB_day_F_D)
                + "," + 체크박스(Properties.Settings.Default.CB_day_F_E)
                + "," + 체크박스(Properties.Settings.Default.CB_day_F_F)
                + "," + 체크박스(Properties.Settings.Default.CB_day_F_G)
                + "," + 체크박스(Properties.Settings.Default.CB_day_F_H)
                + "," + 체크박스(Properties.Settings.Default.CB_day_F_I)
                + "," + 체크박스(Properties.Settings.Default.CB_day_F_J)
                + "," + 체크박스(Properties.Settings.Default.CB_day_F_K)
                + "," + 체크박스(Properties.Settings.Default.CB_day_F_L)
                     + ");\n매매그룹설정_미수금정리(" + 체크박스(Properties.Settings.Default.CB_미수금정리_A)
                + "," + 체크박스(Properties.Settings.Default.CB_미수금정리_B)
                + "," + 체크박스(Properties.Settings.Default.CB_미수금정리_C)
                + "," + 체크박스(Properties.Settings.Default.CB_미수금정리_D)
                + "," + 체크박스(Properties.Settings.Default.CB_미수금정리_E)
                + "," + 체크박스(Properties.Settings.Default.CB_미수금정리_F)
                + "," + 체크박스(Properties.Settings.Default.CB_미수금정리_G)
                + "," + 체크박스(Properties.Settings.Default.CB_미수금정리_H)
                + "," + 체크박스(Properties.Settings.Default.CB_미수금정리_I)
                + "," + 체크박스(Properties.Settings.Default.CB_미수금정리_J)
                + "," + 체크박스(Properties.Settings.Default.CB_미수금정리_K)
                + "," + 체크박스(Properties.Settings.Default.CB_미수금정리_L)
                + ");\n매매그룹설정_손익담보매도_A(" + 체크박스(Properties.Settings.Default.CB_Cut_A_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_A_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_A_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_A_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_A_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_A_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_A_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_A_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_A_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_A_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_A_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_A_L)
                + ");\n매매그룹설정_손익담보매도_B(" + 체크박스(Properties.Settings.Default.CB_Cut_B_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_B_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_B_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_B_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_B_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_B_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_B_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_B_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_B_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_B_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_B_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_B_L)
                + ");\n매매그룹설정_손익담보매도_C(" + 체크박스(Properties.Settings.Default.CB_Cut_C_A)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_C_B)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_C_C)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_C_D)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_C_E)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_C_F)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_C_G)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_C_H)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_C_I)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_C_J)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_C_K)
                + "," + 체크박스(Properties.Settings.Default.CB_Cut_C_L)
                + ");\n매매그룹설정_계좌청산_특정시간(" + 체크박스(Properties.Settings.Default.CB_특정시간_계좌청산_A)
                + "," + 체크박스(Properties.Settings.Default.CB_특정시간_계좌청산_B)
                + "," + 체크박스(Properties.Settings.Default.CB_특정시간_계좌청산_C)
                + "," + 체크박스(Properties.Settings.Default.CB_특정시간_계좌청산_D)
                + "," + 체크박스(Properties.Settings.Default.CB_특정시간_계좌청산_E)
                + "," + 체크박스(Properties.Settings.Default.CB_특정시간_계좌청산_F)
                + "," + 체크박스(Properties.Settings.Default.CB_특정시간_계좌청산_G)
                + "," + 체크박스(Properties.Settings.Default.CB_특정시간_계좌청산_H)
                + "," + 체크박스(Properties.Settings.Default.CB_특정시간_계좌청산_I)
                + "," + 체크박스(Properties.Settings.Default.CB_특정시간_계좌청산_J)
                + "," + 체크박스(Properties.Settings.Default.CB_특정시간_계좌청산_K)
                + "," + 체크박스(Properties.Settings.Default.CB_특정시간_계좌청산_L)
                + ");\n매매그룹설정_계좌청산_실현손익(" + 체크박스(Properties.Settings.Default.CB_실현손익_계좌청산_A)
                + "," + 체크박스(Properties.Settings.Default.CB_실현손익_계좌청산_B)
                + "," + 체크박스(Properties.Settings.Default.CB_실현손익_계좌청산_C)
                + "," + 체크박스(Properties.Settings.Default.CB_실현손익_계좌청산_D)
                + "," + 체크박스(Properties.Settings.Default.CB_실현손익_계좌청산_E)
                + "," + 체크박스(Properties.Settings.Default.CB_실현손익_계좌청산_F)
                + "," + 체크박스(Properties.Settings.Default.CB_실현손익_계좌청산_G)
                + "," + 체크박스(Properties.Settings.Default.CB_실현손익_계좌청산_H)
                + "," + 체크박스(Properties.Settings.Default.CB_실현손익_계좌청산_I)
                + "," + 체크박스(Properties.Settings.Default.CB_실현손익_계좌청산_J)
                + "," + 체크박스(Properties.Settings.Default.CB_실현손익_계좌청산_K)
                + "," + 체크박스(Properties.Settings.Default.CB_실현손익_계좌청산_L)
                + ");\n매매그룹설정_계좌청산_예상손실(" + 체크박스(Properties.Settings.Default.CB_예상손실_계좌청산_A)
                + "," + 체크박스(Properties.Settings.Default.CB_예상손실_계좌청산_B)
                + "," + 체크박스(Properties.Settings.Default.CB_예상손실_계좌청산_C)
                + "," + 체크박스(Properties.Settings.Default.CB_예상손실_계좌청산_D)
                + "," + 체크박스(Properties.Settings.Default.CB_예상손실_계좌청산_E)
                + "," + 체크박스(Properties.Settings.Default.CB_예상손실_계좌청산_F)
                + "," + 체크박스(Properties.Settings.Default.CB_예상손실_계좌청산_G)
                + "," + 체크박스(Properties.Settings.Default.CB_예상손실_계좌청산_H)
                + "," + 체크박스(Properties.Settings.Default.CB_예상손실_계좌청산_I)
                + "," + 체크박스(Properties.Settings.Default.CB_예상손실_계좌청산_J)
                + "," + 체크박스(Properties.Settings.Default.CB_예상손실_계좌청산_K)
                + "," + 체크박스(Properties.Settings.Default.CB_예상손실_계좌청산_L)
                + ");\n매매그룹설정_계좌청산_예상수익(" + 체크박스(Properties.Settings.Default.CB_예상수익_계좌청산_A)
                + "," + 체크박스(Properties.Settings.Default.CB_예상수익_계좌청산_B)
                + "," + 체크박스(Properties.Settings.Default.CB_예상수익_계좌청산_C)
                + "," + 체크박스(Properties.Settings.Default.CB_예상수익_계좌청산_D)
                + "," + 체크박스(Properties.Settings.Default.CB_예상수익_계좌청산_E)
                + "," + 체크박스(Properties.Settings.Default.CB_예상수익_계좌청산_F)
                + "," + 체크박스(Properties.Settings.Default.CB_예상수익_계좌청산_G)
                + "," + 체크박스(Properties.Settings.Default.CB_예상수익_계좌청산_H)
                + "," + 체크박스(Properties.Settings.Default.CB_예상수익_계좌청산_I)
                + "," + 체크박스(Properties.Settings.Default.CB_예상수익_계좌청산_J)
                + "," + 체크박스(Properties.Settings.Default.CB_예상수익_계좌청산_K)
                + "," + 체크박스(Properties.Settings.Default.CB_예상수익_계좌청산_L)
                + ");\n매매그룹설정_시간청산_A(" + 체크박스(Properties.Settings.Default.CB_시간청산A_A)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산A_B)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산A_C)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산A_D)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산A_E)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산A_F)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산A_G)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산A_H)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산A_I)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산A_J)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산A_K)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산A_L)
                + ");\n매매그룹설정_시간청산_B(" + 체크박스(Properties.Settings.Default.CB_시간청산B_A)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산B_B)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산B_C)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산B_D)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산B_E)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산B_F)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산B_G)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산B_H)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산B_I)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산B_J)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산B_K)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산B_L)
                + ");\n매매그룹설정_시간청산_C(" + 체크박스(Properties.Settings.Default.CB_시간청산C_A)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산C_B)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산C_C)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산C_D)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산C_E)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산C_F)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산C_G)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산C_H)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산C_I)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산C_J)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산C_K)
                + "," + 체크박스(Properties.Settings.Default.CB_시간청산C_L)
                + ");";
            }
            void 대금탐색설정()
            {
                Properties.Settings.Default.대금탐색설정 =
                "대금탐색_누적거래대금(" + Properties.Settings.Default.TB_accumulate_Price
                + ");\n대금탐색_매수_A(" + Properties.Settings.Default.CBB_M_잔량
                + "," + Properties.Settings.Default.TB_M_매도호가별대금
                + "," + Properties.Settings.Default.TB_M_매도호가합대금
                + "," + Properties.Settings.Default.TB_M_매수호가별대금
                + "," + Properties.Settings.Default.TB_M_매수호가합대금
                + "," + 체크박스(Properties.Settings.Default.CB_매수탐색A)
                + "," + Properties.Settings.Default.TB_매수탐색A
                + "," + Properties.Settings.Default.TB_Buy_A_기준초
                + "," + Properties.Settings.Default.TB_Buy_A_탐색rate
                + "," + Properties.Settings.Default.TB_Buy_상승카운터_A
                + "," + 체크박스(Properties.Settings.Default.CB_Buy_상승옵션_A)
                + "," + Properties.Settings.Default.TB_Buy_하락카운터_A
                + "," + 체크박스(Properties.Settings.Default.CB_Buy_하락옵션_A)
                + "," + Properties.Settings.Default.TB_Buy_A_탐색주가_1
                + "," + Properties.Settings.Default.TB_Buy_A_탐색주가_2
                + "," + Properties.Settings.Default.TB_Buy_A_탐색주가_3
                + "," + Properties.Settings.Default.TB_Buy_A_탐색주가_4
                + "," + Properties.Settings.Default.TB_Buy_A_탐색주가_5
                + "," + Properties.Settings.Default.TB_Buy_A_탐색주가_6
                + "," + Properties.Settings.Default.TB_Buy_A_탐색대금_1
                + "," + Properties.Settings.Default.TB_Buy_A_탐색대금_2
                + "," + Properties.Settings.Default.TB_Buy_A_탐색대금_3
                + "," + Properties.Settings.Default.TB_Buy_A_탐색대금_4
                + "," + Properties.Settings.Default.TB_Buy_A_탐색대금_5
                + "," + Properties.Settings.Default.TB_Buy_A_탐색대금_6
                + "," + Properties.Settings.Default.MTB_M_반복
                + "," + Properties.Settings.Default.CBB_Buy_A_분봉
                + "," + Properties.Settings.Default.CBB_Buy_A_일봉
                + ");\n대금탐색_매수_B(" + Properties.Settings.Default.CBB_M_잔량_2
                + "," + Properties.Settings.Default.TB_M_매도호가별대금_2
                + "," + Properties.Settings.Default.TB_M_매도호가합대금_2
                + "," + Properties.Settings.Default.TB_M_매수호가별대금_2
                + "," + Properties.Settings.Default.TB_M_매수호가합대금_2
                + "," + 체크박스(Properties.Settings.Default.CB_매수탐색B)
                + "," + Properties.Settings.Default.TB_매수탐색B
                + "," + Properties.Settings.Default.TB_Buy_B_기준초
                + "," + Properties.Settings.Default.TB_Buy_B_탐색rate
                + "," + Properties.Settings.Default.TB_Buy_상승카운터_B
                + "," + 체크박스(Properties.Settings.Default.CB_Buy_상승옵션_B)
                + "," + Properties.Settings.Default.TB_Buy_하락카운터_B
                + "," + 체크박스(Properties.Settings.Default.CB_Buy_하락옵션_B)
                + "," + Properties.Settings.Default.TB_Buy_B_탐색주가_1
                + "," + Properties.Settings.Default.TB_Buy_B_탐색주가_2
                + "," + Properties.Settings.Default.TB_Buy_B_탐색주가_3
                + "," + Properties.Settings.Default.TB_Buy_B_탐색주가_4
                + "," + Properties.Settings.Default.TB_Buy_B_탐색주가_5
                + "," + Properties.Settings.Default.TB_Buy_B_탐색주가_6
                + "," + Properties.Settings.Default.TB_Buy_B_탐색대금_1
                + "," + Properties.Settings.Default.TB_Buy_B_탐색대금_2
                + "," + Properties.Settings.Default.TB_Buy_B_탐색대금_3
                + "," + Properties.Settings.Default.TB_Buy_B_탐색대금_4
                + "," + Properties.Settings.Default.TB_Buy_B_탐색대금_5
                + "," + Properties.Settings.Default.TB_Buy_B_탐색대금_6
                + "," + Properties.Settings.Default.MTB_M_반복_2
                + "," + Properties.Settings.Default.CBB_Buy_B_분봉
                + "," + Properties.Settings.Default.CBB_Buy_B_일봉
                + ");\n대금탐색_매도(" + 체크박스(Properties.Settings.Default.CB_매도탐색)
                + "," + Properties.Settings.Default.TB_매도탐색
                + "," + Properties.Settings.Default.TB_Sell_기준초
                + "," + Properties.Settings.Default.TB_Sell_탐색rate
                + "," + Properties.Settings.Default.TB_Sell_상승카운터
                + "," + 체크박스(Properties.Settings.Default.CB_Sell_상승옵션)
                + "," + Properties.Settings.Default.TB_Sell_하락카운터
                + "," + 체크박스(Properties.Settings.Default.CB_Sell_하락옵션)
                + "," + Properties.Settings.Default.TB_Sell_탐색주가_1
                + "," + Properties.Settings.Default.TB_Sell_탐색주가_2
                + "," + Properties.Settings.Default.TB_Sell_탐색주가_3
                + "," + Properties.Settings.Default.TB_Sell_탐색주가_4
                + "," + Properties.Settings.Default.TB_Sell_탐색주가_5
                + "," + Properties.Settings.Default.TB_Sell_탐색주가_6
                + "," + Properties.Settings.Default.TB_Sell_탐색대금_1
                + "," + Properties.Settings.Default.TB_Sell_탐색대금_2
                + "," + Properties.Settings.Default.TB_Sell_탐색대금_3
                + "," + Properties.Settings.Default.TB_Sell_탐색대금_4
                + "," + Properties.Settings.Default.TB_Sell_탐색대금_5
                + "," + Properties.Settings.Default.TB_Sell_탐색대금_6
                + "," + Properties.Settings.Default.CBB_Sell_탐색_분봉
                + "," + Properties.Settings.Default.CBB_Sell_탐색_일봉
                + ");";
            }
            void 기능설정()
            {
                Properties.Settings.Default.기능설정 =
                "기능설정값(" + 체크박스(Properties.Settings.Default.CB_편입추가)
                + "," + 체크박스(Properties.Settings.Default.CB_최종가업데이트)
                + "," + 체크박스(Properties.Settings.Default.CB_신규매수정지)
                + "," + 체크박스(Properties.Settings.Default.CB_추가매수정지)
                + "," + 체크박스(Properties.Settings.Default.CB_VI매수취소)
                + "," + 체크박스(Properties.Settings.Default.CB_VI매도취소)
                + "," + 체크박스(Properties.Settings.Default.CB_상매수취소)
                + "," + 체크박스(Properties.Settings.Default.CB_하매도취소)
                + "," + 체크박스(Properties.Settings.Default.CB_상전량청산)
                + "," + 체크박스(Properties.Settings.Default.CB_하전량청산)
                + "," + 체크박스(Properties.Settings.Default.CB_NXT)
                + "," + 체크박스(Properties.Settings.Default.CB_ETF매입비제외)


                + ");";
            }

            //Console.WriteLine("\nProperties.Settings.Default.계좌설정:: \n" + Properties.Settings.Default.계좌설정);
            //Console.WriteLine("\nProperties.Settings.Default.기본매매설정:: \n" + Properties.Settings.Default.기본매매설정);
            //Console.WriteLine("\nProperties.Settings.Default.반복매매설정:: \n" + Properties.Settings.Default.반복매매설정);
            //Console.WriteLine("\nProperties.Settings.Default.계좌관리설정:: \n" + Properties.Settings.Default.계좌관리설정);
            //Console.WriteLine("\nProperties.Settings.Default.특수설정:: \n" + Properties.Settings.Default.특수설정);
            //Console.WriteLine("\nProperties.Settings.Default.매매그룹설정:: \n" + Properties.Settings.Default.매매그룹설정);
            //Console.WriteLine("\nProperties.Settings.Default.대금탐색설정:: \n" + Properties.Settings.Default.대금탐색설정);
            //Console.WriteLine("\nProperties.Settings.Default.기능설정:: \n" + Properties.Settings.Default.기능설정);


            Task task = new Task(() =>
            {
                Form1.form1.account_comboBox.Invoke((MethodInvoker)delegate ()  //      
                {
                    if (Form1.form1.account_comboBox.Text.Length > 0)
                    {
                        string File_Check = Application.StartupPath + @"\Data\가이드매매설정.txt";

                        using (StreamWriter writer_ = new StreamWriter(File_Check))
                        {
                            writer_.WriteLine(Properties.Settings.Default.계좌설정);
                            writer_.WriteLine();
                            writer_.WriteLine(Properties.Settings.Default.기본매매설정);
                            writer_.WriteLine();
                            writer_.WriteLine(Properties.Settings.Default.반복매매설정);
                            writer_.WriteLine();
                            writer_.WriteLine(Properties.Settings.Default.계좌관리설정);
                            writer_.WriteLine();
                            writer_.WriteLine(Properties.Settings.Default.특수설정);
                            writer_.WriteLine();
                            writer_.WriteLine(Properties.Settings.Default.매매그룹설정);
                            writer_.WriteLine();
                            writer_.WriteLine(Properties.Settings.Default.대금탐색설정);
                            writer_.WriteLine();
                            writer_.WriteLine(Properties.Settings.Default.기능설정);
                        }
                    }
                });
            });
            Form1.writing_Manager.RequestTrData(task);
        }

        public static void Load_condition_textprint()
        {
            string GetString(int index)
            {
                string result = " ( X ) ";
                if (index == 1) result = " 진입 ";
                if (index == 2) result = " 이탈 ";
                return result;
            }

            string Get_NewAA(int index)
            {
                string result = " I.기준 ";
                if (index == 1) result = " D.기준 ";
                return result;
            }

            string Get_NewBC(int index)
            {
                string result = " I.OR ";
                if (index == 1) result = " I.AND ";
                if (index == 2) result = " D.OR ";
                if (index == 3) result = " D.AND ";
                return result;
            }

            FileInfo File_ = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\\지니_64BackUP\\사용검색식.txt");
            if (File_.Exists)
            {
                //파일열기
                StreamReader SR = new StreamReader(System.Windows.Forms.Application.StartupPath + @"\\지니_64BackUP\\사용검색식.txt");
                bool.TryParse(SR.ReadLine(), out bool first);
                SR.Close(); // 파일닫기

                if (!first)
                {
                    파일열기();
                }
            }
            else
            {
                파일열기();
            }

            void 파일열기()
            {
                string File_Check = System.Windows.Forms.Application.StartupPath + @"\\지니_64BackUP\\사용검색식.txt";
                using (StreamWriter writer_ = new StreamWriter(File_Check))
                {
                    writer_.WriteLine("True");
                    writer_.WriteLine("가이드매매 검색식");
                    writer_.WriteLine();
                    writer_.WriteLine("*주의> 키움HTS를 통해 검색식을 만들고 사용하기 바랍니다.*");
                    writer_.WriteLine();
                    writer_.WriteLine("신규 A :: " + 신규_A[0] + "      사용 :: " + Get_NewAA(int.Parse(신규_A[1])) + "   검색식  :: " + 신규_A[2]);
                    writer_.WriteLine("신규 B :: " + 신규_B[0] + "      사용 :: " + Get_NewBC(int.Parse(신규_B[1])) + "     검색식  :: " + 신규_B[2]);
                    writer_.WriteLine("신규 C :: " + 신규_C[0] + "      사용 :: " + Get_NewBC(int.Parse(신규_C[1])) + "     검색식  :: " + 신규_C[2]);
                    writer_.WriteLine();
                    writer_.WriteLine("반복 A :: " + 반복_A[0] + "      사용 :: " + GetString(int.Parse(반복_A[1])) + "     검색식  :: " + 반복_A[2]);
                    writer_.WriteLine("반복 B :: " + 반복_B[0] + "      사용 :: " + GetString(int.Parse(반복_B[1])) + "     검색식  :: " + 반복_B[2]);
                    writer_.WriteLine("반복 C :: " + 반복_C[0] + "      사용 :: " + GetString(int.Parse(반복_C[1])) + "     검색식  :: " + 반복_C[2]);
                    writer_.WriteLine("반복 D :: " + 반복_D[0] + "      사용 :: " + GetString(int.Parse(반복_D[1])) + "     검색식  :: " + 반복_D[2]);
                    writer_.WriteLine("반복 E :: " + 반복_E[0] + "      사용 :: " + GetString(int.Parse(반복_E[1])) + "     검색식  :: " + 반복_E[2]);
                    writer_.WriteLine("반복 F :: " + 반복_F[0] + "      사용 :: " + GetString(int.Parse(반복_F[1])) + "     검색식  :: " + 반복_F[2]);
                    writer_.WriteLine("반복 G :: " + 반복_G[0] + "      사용 :: " + GetString(int.Parse(반복_G[1])) + "     검색식  :: " + 반복_G[2]);
                    writer_.WriteLine("반복 H :: " + 반복_H[0] + "      사용 :: " + GetString(int.Parse(반복_H[1])) + "     검색식  :: " + 반복_H[2]);
                    writer_.WriteLine("반복 I  :: " + 반복_I[0] + "      사용 :: " + GetString(int.Parse(반복_I[1])) + "     검색식  :: " + 반복_I[2]);
                    writer_.WriteLine("반복 J  :: " + 반복_J[0] + "      사용 :: " + GetString(int.Parse(반복_J[1])) + "     검색식  :: " + 반복_J[2]);
                    writer_.WriteLine("반복 K :: " + 반복_K[0] + "      사용 :: " + GetString(int.Parse(반복_K[1])) + "     검색식  :: " + 반복_K[2]);
                    writer_.WriteLine("반복 L :: " + 반복_L[0] + "      사용 :: " + GetString(int.Parse(반복_L[1])) + "     검색식  :: " + 반복_L[2]);
                    writer_.WriteLine("반복 M :: " + 반복_M[0] + "     사용 :: " + GetString(int.Parse(반복_M[1])) + "     검색식  :: " + 반복_M[2]);
                    writer_.WriteLine("반복 N :: " + 반복_N[0] + "      사용 :: " + GetString(int.Parse(반복_N[1])) + "     검색식  :: " + 반복_N[2]);
                    writer_.WriteLine();
                    writer_.WriteLine("리밸 A :: " + 리밸_A[0] + "      사용 :: " + GetString(int.Parse(리밸_A[1])) + "     검색식  :: " + 리밸_A[2]);
                    writer_.WriteLine("리밸 B :: " + 리밸_B[0] + "      사용 :: " + GetString(int.Parse(리밸_B[1])) + "     검색식  :: " + 리밸_B[2]);
                    writer_.WriteLine("리밸 C :: " + 리밸_C[0] + "      사용 :: " + GetString(int.Parse(리밸_C[1])) + "     검색식  :: " + 리밸_C[2]);
                    writer_.WriteLine("리밸 D :: " + 리밸_D[0] + "      사용 :: " + GetString(int.Parse(리밸_D[1])) + "     검색식  :: " + 리밸_D[2]);
                    writer_.WriteLine("리밸 E :: " + 리밸_E[0] + "      사용 :: " + GetString(int.Parse(리밸_E[1])) + "     검색식  :: " + 리밸_E[2]);
                    writer_.WriteLine("리밸 F :: " + 리밸_F[0] + "      사용 :: " + GetString(int.Parse(리밸_F[1])) + "     검색식  :: " + 리밸_F[2]);
                    writer_.WriteLine("리밸 G :: " + 리밸_G[0] + "      사용 :: " + GetString(int.Parse(리밸_G[1])) + "     검색식  :: " + 리밸_G[2]);
                    writer_.WriteLine();
                    writer_.WriteLine("청산 A :: " + 청산_A[0] + "      사용 :: " + GetString(int.Parse(청산_A[1])) + "     검색식  :: " + 청산_A[2]);
                    writer_.WriteLine("청산 B :: " + 청산_B[0] + "      사용 :: " + GetString(int.Parse(청산_B[1])) + "     검색식  :: " + 청산_B[2]);
                    writer_.WriteLine("청산 C :: " + 청산_C[0] + "      사용 :: " + GetString(int.Parse(청산_C[1])) + "     검색식  :: " + 청산_C[2]);
                    writer_.WriteLine();
                    writer_.WriteLine("*주의> 키움HTS를 통해 검색식을 만들고 사용하기 바랍니다.*");
                }

                System.Diagnostics.Process.Start("Notepad.exe", System.Windows.Forms.Application.StartupPath + @"\\지니_64BackUP\\사용검색식.txt");
            }
        }

    }
}
