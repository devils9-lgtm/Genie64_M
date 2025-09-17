using System;
using System.Drawing;

namespace 지니_64
{
    class Jisu_linkage
    {
        public static bool 업종별지수연동(string 위치, Market_Item Market)
        {
            bool result = false;
            string str = "";
            string 시장 = "코스피";
            if (Market.Market.Equals("P")) 시장 = "코스피";
            if (Market.Market.Equals("D")) 시장 = "코스닥";

            if (위치.Equals("신규"))
            {
                if (Market.Market.Equals("P") || Market.Market.Equals("E")) result = Form1.신규매수_동작_피;
                if (Market.Market.Equals("D")) result = Form1.신규매수_동작_닥;

                if (!result) str = "[신규매수 제한] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ지수 or 매매동향 설절값 을 벗어났습니다.";

                avg_check();
            }
            else
            {
                if (Market.Market.Equals("P") || Market.Market.Equals("E")) result = Form1.추가매수_동작_피;
                if (Market.Market.Equals("D")) result = Form1.추가매수_동작_닥;

                if (result)
                {
                    avg_check();
                }
                else
                {
                    str = "[추매제한] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ지수 or 매매동향 설절값 을 벗어났습니다.";
                }
            }

            void avg_check()
            {
                if (result)
                {
                    AVG_jisu avg = Form1.AVG_jisu[0];
                    bool stop = avg.new_stop;

                    if (Market.Market.Equals("D")) avg = Form1.AVG_jisu[1];
                    if (!위치.Equals("신규")) stop = avg.add_stop;
                    if (stop)
                    {
                        if (result) result = avg.check_min_03;
                        if (result) result = avg.check_min_05;
                        if (result) result = avg.check_min_10;
                        if (result) result = avg.check_min_20;
                        if (result) result = avg.check_min_30;
                        if (result) result = avg.check_min_60;
                        if (!result) str = "[" + 위치 + "제한] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ일봉 이평선 설정값을 벗어났습니다.";
                        if (Form1.NXT_server) result = true;

                              // Console.WriteLine("코스닥 이평선 분봉: " + result);
                        if (result) result = avg.check_day_03;
                        if (result) result = avg.check_day_05;
                        if (result) result = avg.check_day_10;
                        if (result) result = avg.check_day_20;
                        if (result) result = avg.check_day_40;
                        if (result) result = avg.check_day_60;
                        if (!result) str = "[" + 위치 + "제한] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ일봉 이평선 설정값을 벗어났습니다.";

                               // Console.WriteLine("코스닥 이평선 일봉: " + result);

                    }
                }
            }

            if (!result)
            {
                Tab_Basic.매매거부_메세지출력(Market.종목명, str);
            }

            return result;
        }

        public static string 지수연동(string 업종)
        {
            string 신규 = "신규";
            string 추매 = "추매";

            if (업종.Equals("코스피"))
            {
                double 등락율 = Form1.Acc[0].피_등락률;
                double 등락율_use = Properties.Settings.Default.TB_p_ratio_use;
                int 등락율_UD = Properties.Settings.Default.combo_p_ratio_UD;//이하이상
                int 등락율_조건 = Properties.Settings.Default.combo_p_ratio;

                if (등락율_조건 > 0)
                {
                    신규 = 신규계산(등락율, 등락율_use, 등락율_UD, 등락율_조건);
                    추매 = 추매계산(등락율, 등락율_use, 등락율_UD, 등락율_조건);
                }

                double 고가대비 = Form1.Acc[0].피_고가대비;
                double 고가대비_use = Properties.Settings.Default.TB_p_go_use;
                int 고가대비_UD = Properties.Settings.Default.combo_p_go_UD;
                int 고가대비_조건 = Properties.Settings.Default.combo_p_go;

                if (고가대비_조건 > 0)
                {
                    if (신규.Equals("신규")) 신규 = 신규계산(고가대비, 고가대비_use, 고가대비_UD, 고가대비_조건);
                    if (추매.Equals("추매")) 추매 = 추매계산(고가대비, 고가대비_use, 고가대비_UD, 고가대비_조건);
                }

                double 저가대비 = Form1.Acc[0].피_저가대비;
                double 저가대비_use = Properties.Settings.Default.TB_p_down_use;
                int 저가대비_UD = Properties.Settings.Default.combo_p_down_UD;
                int 저가대비_조건 = Properties.Settings.Default.combo_p_down;

                if (저가대비_조건 > 0)
                {
                    if (신규.Equals("신규")) 신규 = 신규계산(저가대비, 저가대비_use, 저가대비_UD, 저가대비_조건);
                    if (추매.Equals("추매")) 추매 = 추매계산(저가대비, 저가대비_use, 저가대비_UD, 저가대비_조건);
                }
            }
            else
            {
                double 등락율 = Form1.Acc[0].닥_등락률;
                double 등락율_use = Properties.Settings.Default.TB_q_ratio_use;
                int 등락율_UD = Properties.Settings.Default.combo_q_ratio_UD;
                int 등락율_조건 = Properties.Settings.Default.combo_q_ratio;

                if (등락율_조건 > 0)
                {
                    신규 = 신규계산(등락율, 등락율_use, 등락율_UD, 등락율_조건);
                    추매 = 추매계산(등락율, 등락율_use, 등락율_UD, 등락율_조건);
                }

                double 고가대비 = Form1.Acc[0].닥_고가대비;
                double 고가대비_use = Properties.Settings.Default.TB_q_go_use;
                int 고가대비_UD = Properties.Settings.Default.combo_q_go_UD;
                int 고가대비_조건 = Properties.Settings.Default.combo_q_go;

                if (고가대비_조건 > 0)
                {
                    if (신규.Equals("신규")) 신규 = 신규계산(고가대비, 고가대비_use, 고가대비_UD, 고가대비_조건);
                    if (추매.Equals("추매")) 추매 = 추매계산(고가대비, 고가대비_use, 고가대비_UD, 고가대비_조건);
                }

                double 저가대비 = Form1.Acc[0].닥_저가대비;
                double 저가대비_use = Properties.Settings.Default.TB_q_down_use;
                int 저가대비_UD = Properties.Settings.Default.combo_q_down_UD;
                int 저가대비_조건 = Properties.Settings.Default.combo_q_down;

                if (저가대비_조건 > 0)
                {
                    if (신규.Equals("신규")) 신규 = 신규계산(저가대비, 저가대비_use, 저가대비_UD, 저가대비_조건);
                    if (추매.Equals("추매")) 추매 = 추매계산(저가대비, 저가대비_use, 저가대비_UD, 저가대비_조건);
                }
            }

            string 신규계산(double Ratio, double Use, int UD, int condition)
            {
                string result = "신규";

                switch (condition)
                {
                    case 1: // 신규중지
                        if (UD == 0) // 이하
                        {
                            if (Ratio <= Use) result = "-";
                        }
                        else // 이상
                        {
                            if (Ratio >= Use) result = "-";
                        }
                        break;
                    case 3: // 신&추중지
                        if (UD == 0)
                        {
                            if (Ratio <= Use) result = "-";
                        }
                        else
                        {
                            if (Ratio >= Use) result = "-";
                        }
                        break;
                }

                return result;
            }

            string 추매계산(double Ratio, double Use, int UD, int condition)
            {
                string result = "추매";

                switch (condition)
                {
                    case 2: // 추매중지
                        if (UD == 0)
                        {
                            if (Ratio <= Use) result = "-";
                        }
                        else
                        {
                            if (Ratio >= Use) result = "-";
                        }
                        break;
                    case 3: // 신&추중지
                        if (UD == 0)
                        {
                            if (Ratio <= Use) result = "-";
                        }
                        else
                        {
                            if (Ratio >= Use) result = "-";
                        }
                        break;
                }

                return result;
            }

            return 신규 + "^" + 추매;
        }

        //00 ( X )
        //01 지수 매매제한
        //02 외국인 순매수
        //03 기관 순매수
        //04 외인 or 기관 순매수
        //05 외 or 기 or 프 순매수
        //06 외인 and 기관 순매수
        //07 외 and 기 and 프 순매
        //08 외인 순매수 and 지수
        //09 기관 순매수 and 지수
        //10 외 o 기 순매수 n 지수
        //11 외o기o프 순매 n 지수
        //12 외 n 기 순매 n 지수
        //13 외n기n프 순매 n 지수
        //14 외인 순매수 o 지수
        //15 기관 순매수 o 지수
        //16 외 o 기 순매 o 지수
        //17 외o기o프 순매 o 지수
        //18 외 n 기 순매 o 지수
        //19 외n기n프 순매 o 지수

        public static void 지수업종별연동(string 업종)
        {
            bool 외인 = true;
            bool 기관 = true;
            bool 프로그램 = true;
            bool 신규매수 = false;
            bool 추가매수 = false;

            if (업종.Equals("코스피"))
            {
                if (Form1.Acc[0].피_외인 <= 0) 외인 = false;
                if (Form1.Acc[0].피_기관 <= 0) 기관 = false;
                if (Form1.Acc[0].피_프로그램 <= 0) 프로그램 = false;
            }
            else
            {
                if (Form1.Acc[0].닥_외인 <= 0) 외인 = false;
                if (Form1.Acc[0].닥_기관 <= 0) 기관 = false;
                if (Form1.Acc[0].닥_프로그램 <= 0) 프로그램 = false;
            }

            int index = Properties.Settings.Default.CBB_지수연동_신규;

            bool 신규_코스피 = false;
            bool 신규_코스닥 = false;

            if (지수연동(업종).Contains("신규")) 신규매수 = true;

            // ( X )
            if (index == 0)
            {
                신규_코스피 = true;
                신규_코스닥 = true;
            }
            else if (index == 1)// 지수 매매제한
            {
                if (신규매수) { if (업종.Equals("코스피")) { 신규_코스피 = true; } else { 신규_코스닥 = true; } }
            }
            else if (index == 2)// 외국인 순매수
            {
                if (업종.Equals("코스피")) { if (외인) 신규_코스피 = true; } else { if (외인) 신규_코스닥 = true; }
            }
            else if (index == 3)// 기관 순매수
            {
                if (업종.Equals("코스피")) { if (기관) 신규_코스피 = true; } else { if (기관) 신규_코스닥 = true; }
            }
            else if (index == 4)// 외인 or 기관 순매수
            {
                if (업종.Equals("코스피")) { if (외인 || 기관) 신규_코스피 = true; } else { if (외인 || 기관) 신규_코스닥 = true; }
            }
            else if (index == 5)// 외 or 기 or 프 순매수
            {
                if (업종.Equals("코스피")) { if (외인 || 기관 || 프로그램) 신규_코스피 = true; } else { if (외인 || 기관 || 프로그램) 신규_코스닥 = true; }
            }
            else if (index == 6)//  외인 and 기관 순매수
            {
                if (업종.Equals("코스피")) { if (외인 && 기관) 신규_코스피 = true; } else { if (외인 && 기관) 신규_코스닥 = true; }
            }
            else if (index == 7)//외 and 기 and 프 순매
            {
                if (업종.Equals("코스피")) { if (외인 && 기관 && 프로그램) 신규_코스피 = true; } else { if (외인 && 기관 && 프로그램) 신규_코스닥 = true; }
            }
            else if (index == 8)//외인 순매수 n 지수
            {
                if (업종.Equals("코스피")) { if (외인 && 신규매수) 신규_코스피 = true; } else { if (외인 && 신규매수) 신규_코스닥 = true; }
            }
            else if (index == 9)// 기관 순매수 n 지수
            {
                if (업종.Equals("코스피")) { if (기관 && 신규매수) 신규_코스피 = true; } else { if (기관 && 신규매수) 신규_코스닥 = true; }
            }
            else if (index == 10) // 외 o 기 순매 n 지수
            {
                if (업종.Equals("코스피")) { if ((외인 || 기관) && 신규매수) 신규_코스피 = true; } else { if ((외인 || 기관) && 신규매수) 신규_코스닥 = true; }
            }
            else if (index == 11)// 외o기o프 순매 n 지수
            {
                if (업종.Equals("코스피")) { if ((외인 || 기관 || 프로그램) && 신규매수) 신규_코스피 = true; } else { if ((외인 || 기관 || 프로그램) && 신규매수) 신규_코스닥 = true; }
            }
            else if (index == 12)// 외 n 기 순매 n 지수
            {
                if (업종.Equals("코스피")) { if (외인 && 기관 && 신규매수) 신규_코스피 = true; } else { if (외인 && 기관 && 신규매수) 신규_코스닥 = true; }
            }
            else if (index == 13)// 외n기n프 순매 n 지수
            {
                if (업종.Equals("코스피")) { if (외인 && 기관 && 프로그램 && 신규매수) 신규_코스피 = true; } else { if (외인 && 기관 && 프로그램 && 신규매수) 신규_코스닥 = true; }
            }
            else if (index == 14)//외인 순매수 o 지수
            {
                if (업종.Equals("코스피")) { if (외인 || 신규매수) 신규_코스피 = true; } else { if (외인 || 신규매수) 신규_코스닥 = true; }
            }
            else if (index == 15)// 기관 순매수 o 지수
            {
                if (업종.Equals("코스피")) { if (기관 || 신규매수) 신규_코스피 = true; } else { if (기관 || 신규매수) 신규_코스닥 = true; }
            }
            else if (index == 16) // 외 o 기 순매 o 지수
            {
                if (업종.Equals("코스피")) { if (외인 || 기관 || 신규매수) 신규_코스피 = true; } else { if (외인 || 기관 || 신규매수) 신규_코스닥 = true; }
            }
            else if (index == 17)// 외o기o프 순매 o 지수
            {
                if (업종.Equals("코스피")) { if (외인 || 기관 || 프로그램 || 신규매수) 신규_코스피 = true; } else { if (외인 || 기관 || 프로그램 || 신규매수) 신규_코스닥 = true; }
            }
            else if (index == 18)// 외 n 기 순매 o 지수
            {
                if (업종.Equals("코스피")) { if ((외인 && 기관) || 신규매수) 신규_코스피 = true; } else { if ((외인 && 기관) || 신규매수) 신규_코스닥 = true; }
            }
            else if (index == 19)// 외n기n프 순매 o 지수
            {
                if (업종.Equals("코스피")) { if ((외인 && 기관 && 프로그램) || 신규매수) 신규_코스피 = true; } else { if ((외인 && 기관 && 프로그램) || 신규매수) 신규_코스닥 = true; }
            }

            index = Properties.Settings.Default.CBB_지수연동_추매;

            bool 추가_코스피 = false;
            bool 추가_코스닥 = false;
            if (지수연동(업종).Contains("추매")) 추가매수 = true;

            // ( X )
            if (index == 0)
            {
                추가_코스피 = true;
                추가_코스닥 = true;
            }
            else if (index == 1)// 지수 매매제한
            {
                if (추가매수) { if (업종.Equals("코스피")) { 추가_코스피 = true; } else { 추가_코스닥 = true; } }
            }
            else if (index == 2)// 외국인 순매수
            {
                if (업종.Equals("코스피")) { if (외인) 추가_코스피 = true; } else { if (외인) 추가_코스닥 = true; }
            }
            else if (index == 3)// 기관 순매수
            {
                if (업종.Equals("코스피")) { if (기관) 추가_코스피 = true; } else { if (기관) 추가_코스닥 = true; }
            }
            else if (index == 4)// 외인 or 기관 순매수
            {
                if (업종.Equals("코스피")) { if (외인 || 기관) 추가_코스피 = true; } else { if (외인 || 기관) 추가_코스닥 = true; }
            }
            else if (index == 5)// 외 or 기 or 프 순매수
            {
                if (업종.Equals("코스피")) { if (외인 || 기관 || 프로그램) 추가_코스피 = true; } else { if (외인 || 기관 || 프로그램) 추가_코스닥 = true; }
            }
            else if (index == 6)//  외인 and 기관 순매수
            {
                if (업종.Equals("코스피")) { if (외인 && 기관) 추가_코스피 = true; } else { if (외인 && 기관) 추가_코스닥 = true; }
            }
            else if (index == 7)//외 and 기 and 프 순매
            {
                if (업종.Equals("코스피")) { if (외인 && 기관 && 프로그램) 추가_코스피 = true; } else { if (외인 && 기관 && 프로그램) 추가_코스닥 = true; }
            }
            else if (index == 8)//외인 순매수 n 지수
            {
                if (업종.Equals("코스피")) { if (외인 && 추가매수) 추가_코스피 = true; } else { if (외인 && 추가매수) 추가_코스닥 = true; }
            }
            else if (index == 9)// 기관 순매수 n 지수
            {
                if (업종.Equals("코스피")) { if (기관 && 추가매수) 추가_코스피 = true; } else { if (기관 && 추가매수) 추가_코스닥 = true; }
            }
            else if (index == 10) // 외 o 기 순매 n지수
            {
                if (업종.Equals("코스피")) { if ((외인 || 기관) && 추가매수) 추가_코스피 = true; } else { if ((외인 || 기관) && 추가매수) 추가_코스닥 = true; }
            }
            else if (index == 11)// 외o기o프 순매 n 지수
            {
                if (업종.Equals("코스피")) { if ((외인 || 기관 || 프로그램) && 추가매수) 추가_코스피 = true; } else { if ((외인 || 기관 || 프로그램) && 추가매수) 추가_코스닥 = true; }
            }
            else if (index == 12)// 외 n 기 순매 n 지수
            {
                if (업종.Equals("코스피")) { if ((외인 && 기관) && 추가매수) 추가_코스피 = true; } else { if ((외인 && 기관) && 추가매수) 추가_코스닥 = true; }
            }
            else if (index == 13)// 외n기n프 순매 n 지수
            {
                if (업종.Equals("코스피")) { if ((외인 && 기관 && 프로그램) && 추가매수) 추가_코스피 = true; } else { if ((외인 && 기관 && 프로그램) && 추가매수) 추가_코스닥 = true; }
            }
            else if (index == 14)//외인 순매수 o 지수
            {
                if (업종.Equals("코스피")) { if (외인 || 추가매수) 추가_코스피 = true; } else { if (외인 || 추가매수) 추가_코스닥 = true; }
            }
            else if (index == 15)// 기관 순매수 o 지수
            {
                if (업종.Equals("코스피")) { if (기관 || 추가매수) 추가_코스피 = true; } else { if (기관 || 추가매수) 추가_코스닥 = true; }
            }
            else if (index == 16) // 외 o 기 순매 o 지수
            {
                if (업종.Equals("코스피")) { if ((외인 || 기관) || 추가매수) 추가_코스피 = true; } else { if ((외인 || 기관) || 추가매수) 추가_코스닥 = true; }
            }
            else if (index == 17)// 외o기o프 순매 o 지수
            {
                if (업종.Equals("코스피")) { if ((외인 || 기관 || 프로그램) || 추가매수) 추가_코스피 = true; } else { if ((외인 || 기관 || 프로그램) || 추가매수) 추가_코스닥 = true; }
            }
            else if (index == 18)// 외 n 기 순매 o 지수
            {
                if (업종.Equals("코스피")) { if ((외인 && 기관) || 추가매수) 추가_코스피 = true; } else { if ((외인 && 기관) || 추가매수) 추가_코스닥 = true; }
            }
            else if (index == 19)// 외n기n프 순매 o 지수
            {
                if (업종.Equals("코스피")) { if ((외인 && 기관 && 프로그램) || 추가매수) 추가_코스피 = true; } else { if ((외인 && 기관 && 프로그램) || 추가매수) 추가_코스닥 = true; }
            }

            if (업종.Equals("코스피"))
            {
                if (Form1.NXT_server)
                {
                    Form1.신규매수_동작_피 = true;
                    Form1.추가매수_동작_피 = true;
                }
                else
                {
                    Form1.신규매수_동작_피 = 신규_코스피;
                    Form1.추가매수_동작_피 = 추가_코스피;
                }
            }
            else
            {
                if (Form1.NXT_server)
                {
                    Form1.신규매수_동작_닥 = true;
                    Form1.추가매수_동작_닥 = true;
                }
                else
                {
                    Form1.신규매수_동작_닥 = 신규_코스닥;
                    Form1.추가매수_동작_닥 = 추가_코스닥;
                }
            }

            지수연동_print(업종);
        }

        public static void 지수연동_print(string 업종)
        {
            if (업종.Equals("코스피"))
            {
                Form1.form1.LB_피외인.Text = "외:" + Form1.Acc[0].피_외인.ToString();
                Form1.form1.LB_피기관.Text = "기:" + Form1.Acc[0].피_기관.ToString();
                Form1.form1.LB_피프로그램.Text = "프:" + Form1.Acc[0].피_프로그램.ToString();

                if (Form1.Acc[0].피_외인 == 0)
                {
                    Form1.form1.LB_피외인.ForeColor = Color.Black;
                }
                else
                {
                    if (Form1.Acc[0].피_외인 > 0)
                    { Form1.form1.LB_피외인.ForeColor = Color.Red; }
                    else
                    { Form1.form1.LB_피외인.ForeColor = Color.Blue; }
                }

                if (Form1.Acc[0].피_기관 == 0)
                { Form1.form1.LB_피기관.ForeColor = Color.Black; }
                else
                {
                    if (Form1.Acc[0].피_기관 > 0)
                    { Form1.form1.LB_피기관.ForeColor = Color.Red; }
                    else
                    { Form1.form1.LB_피기관.ForeColor = Color.Blue; }
                }

                if (Form1.Acc[0].피_프로그램 == 0)
                { Form1.form1.LB_피프로그램.ForeColor = Color.Black; }
                else
                {
                    if (Form1.Acc[0].피_프로그램 > 0)
                    { Form1.form1.LB_피프로그램.ForeColor = Color.Red; }
                    else
                    { Form1.form1.LB_피프로그램.ForeColor = Color.Blue; }
                }
            }
            else
            {
                Form1.form1.LB_닥외인.Text = "외:" + Form1.Acc[0].닥_외인.ToString();
                Form1.form1.LB_닥기관.Text = "기:" + Form1.Acc[0].닥_기관.ToString();
                Form1.form1.LB_닥프로그램.Text = "프:" + Form1.Acc[0].닥_프로그램.ToString();

                if (Form1.Acc[0].닥_외인 == 0)
                {
                    Form1.form1.LB_닥외인.ForeColor = Color.Black;
                }
                else
                {
                    if (Form1.Acc[0].닥_외인 > 0)
                    {
                        Form1.form1.LB_닥외인.ForeColor = Color.Red;
                    }
                    else
                    {
                        Form1.form1.LB_닥외인.ForeColor = Color.Blue;
                    }
                }

                if (Form1.Acc[0].닥_기관 == 0)
                {
                    Form1.form1.LB_닥기관.ForeColor = Color.Black;
                }
                else
                {
                    if (Form1.Acc[0].닥_기관 > 0)
                    {
                        Form1.form1.LB_닥기관.ForeColor = Color.Red;
                    }
                    else
                    {
                        Form1.form1.LB_닥기관.ForeColor = Color.Blue;
                    }
                }

                if (Form1.Acc[0].닥_프로그램 == 0)
                { Form1.form1.LB_닥프로그램.ForeColor = Color.Black; }
                else
                {
                    if (Form1.Acc[0].닥_프로그램 > 0)
                    { Form1.form1.LB_닥프로그램.ForeColor = Color.Red; }
                    else
                    { Form1.form1.LB_닥프로그램.ForeColor = Color.Blue; }
                }
            }

            print("신규");
            print("추매");

            void print(string 위치)
            {
                bool result = true;
                AVG_jisu avg = Form1.AVG_jisu[0];
                bool stop = avg.new_stop;

                if (!업종.Equals("코스피")) avg = Form1.AVG_jisu[1];
                if (!위치.Equals("신규")) stop = avg.add_stop;
                if (stop)
                {
                    if (result) result = avg.check_min_03;
                    if (result) result = avg.check_min_05;
                    if (result) result = avg.check_min_10;
                    if (result) result = avg.check_min_20;
                    if (result) result = avg.check_min_30;
                    if (result) result = avg.check_min_60;
                    if (Form1.NXT_server) result = true;

                    if (result) result = avg.check_day_03;
                    if (result) result = avg.check_day_05;
                    if (result) result = avg.check_day_10;
                    if (result) result = avg.check_day_20;
                    if (result) result = avg.check_day_40;
                    if (result) result = avg.check_day_60;

                    //Console.WriteLine(업종 + " 이평선 일봉: " + result);

                }

                // Console.WriteLine("업종: "+ 업종+ " 위치: " + 위치 + " Form1.NXT_server: " + Form1.NXT_server + " result:" + result);

                if (위치.Equals("신규"))
                {
                    if (업종.Equals("코스피"))
                    {
                        if (Form1.신규매수_동작_피 && result)
                        {
                            Form1.form1.LB_피_신규.Text = "피 매수";
                            Form1.form1.LB_피_신규.ForeColor = Color.Red;
                        }
                        else
                        {
                            Form1.form1.LB_피_신규.Text = "피 정지";
                            Form1.form1.LB_피_신규.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        if (Form1.신규매수_동작_닥 && result)
                        {
                            Form1.form1.LB_닥_신규.Text = "닥 매수";
                            Form1.form1.LB_닥_신규.ForeColor = Color.Red;
                        }
                        else
                        {
                            Form1.form1.LB_닥_신규.Text = "닥 정지";
                            Form1.form1.LB_닥_신규.ForeColor = Color.Black;
                        }
                    }
                }
                else
                {
                    if (업종.Equals("코스피"))
                    {
                        if (Form1.추가매수_동작_피 && result)
                        {
                            Form1.form1.LB_피_추매.Text = "피 매수";
                            Form1.form1.LB_피_추매.ForeColor = Color.Red;
                        }
                        else
                        {
                            Form1.form1.LB_피_추매.Text = "피 정지";
                            Form1.form1.LB_피_추매.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        if (Form1.추가매수_동작_닥 && result)
                        {
                            Form1.form1.LB_닥_추매.Text = "닥 매수";
                            Form1.form1.LB_닥_추매.ForeColor = Color.Red;
                        }
                        else
                        {
                            Form1.form1.LB_닥_추매.Text = "닥 정지";
                            Form1.form1.LB_닥_추매.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }
    }
}
