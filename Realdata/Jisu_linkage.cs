using System.Drawing;
using System.Windows.Forms;

namespace 지니64
{
    class Jisu_linkage
    {

        public static bool 업종별지수연동(string 위치, Market_Item Market)
        {
            bool 통과여부 = false;
            string 메세지 = "";
            string 시장 = (Market.Market.Equals("P") || Market.Market.Equals("E")) ? "코스피" : "코스닥";

            // ==========================================
            // 1. 기존 수급(매매동향) 통과 여부 1차 확인
            // ==========================================
            if (위치.Equals("신규"))
            {
                통과여부 = 시장.Equals("코스피") ? Form1.신규_지수업종연동통과_피 : Form1.신규_지수업종연동통과_닥;
                if (!통과여부) 메세지 = "[신규매수 제한] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ지수 or 매매동향 설정값을 벗어났습니다.";
            }
            else
            {
                // 신규가 아닌 모든 경우(추매, 반복, 리밸런싱 등)
                통과여부 = 시장.Equals("코스피") ? Form1.추가_지수업종연동통과_피 : Form1.추가_지수업종연동통과_닥;
                if (!통과여부) 메세지 = "[" + 위치 + "제한] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ지수 or 매매동향 설정값을 벗어났습니다.";
            }

            // ==========================================
            // 2. 수급을 통과했다면, '해당 그룹'의 지수이평 조건을 2차 확인
            // ==========================================
            if (통과여부)
            {
                // 딕셔너리에서 꺼내올 Key 이름을 만듭니다. (예: "신규" -> "CB_지수이평_신규", "반복_A" -> "CB_지수이평_반복_A")
                string 그룹키 = "CB_지수이평_" + 위치;

                // 만약 위치가 "추매"로 넘어왔다면, UI에 있는 "그외" 체크박스 그룹과 매핑해 줍니다.
                if (위치.Equals("추매")) 그룹키 = "CB_지수이평_그외";

                // [*변경포인트*] Form1.그룹별_AVG_jisu 딕셔너리 이름과 클래스명(지수이평사용값)을 신규 구조로 교체합니다.
                if (Form1.그룹별_지수사용값.TryGetValue(그룹키, out 지수이평사용값[] 현재그룹지수))
                {
                    // [*변경포인트*] 클래스명 교체
                    지수이평사용값 avg = 시장.Equals("코스피") ? 현재그룹지수[0] : 현재그룹지수[1];

                    // 그룹 사용유무(체크박스)를 봅니다.
                    bool stop = avg.사용유무;

                    if (stop) // 해당 그룹에서 지수 감시 옵션을 켜두었다면 검사 시작
                    {
                        bool result = true;

                        // [분봉 체크]
                        if (result) result = avg.결과값_min_03;
                        if (result) result = avg.결과값_min_05;
                        if (result) result = avg.결과값_min_10;
                        if (result) result = avg.결과값_min_20;
                        if (result) result = avg.결과값_min_30;
                        if (result) result = avg.결과값_min_60;

                        if (!result) 메세지 = "[" + 위치 + "제한] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ지수 분봉이평 설정값을 벗어났습니다.";

                        // 테스트(NXT) 서버일 경우 분봉 패스
                        if (Form1.NXT_server) result = true;

                        // [일봉 체크]
                        if (result) result = avg.결과값_day_03;
                        if (result) result = avg.결과값_day_05;
                        if (result) result = avg.결과값_day_10;
                        if (result) result = avg.결과값_day_20;
                        if (result) result = avg.결과값_day_40;
                        if (result) result = avg.결과값_day_60;

                        // 분봉은 통과했는데 일봉에서 막혔을 경우에만 메세지 덮어쓰기
                        if (!result && !메세지.Contains("분봉"))
                        {
                            메세지 = "[" + 위치 + "제한] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ지수 일봉이평 설정값을 벗어났습니다.";
                        }

                        // 지수이평 조건 검사 결과를 최종 통과여부에 반영
                        통과여부 = result;
                    }
                }
            }

            // ==========================================
            // 3. 최종 결과가 거부라면 메세지 출력
            // ==========================================
            if (!통과여부)
            {
                Tab_Basic.매매거부_메세지출력(Market.종목명, 메세지);
            }

            return 통과여부;
        }



        public static string 지수연동(string 업종)
        {
            string 신규 = "신규";
            string 추매 = "추매";

            if (업종.Equals("코스피"))
            {
                double 등락율 = Form1.Acc.피_등락률;
                double 등락율_use = GenieConfig.TB_p_ratio_use;
                int 등락율_UD = GenieConfig.combo_p_ratio_UD;//이하이상
                int 등락율_조건 = GenieConfig.combo_p_ratio;

                if (등락율_조건 > 0)
                {
                    신규 = 신규계산(등락율, 등락율_use, 등락율_UD, 등락율_조건);
                    추매 = 추매계산(등락율, 등락율_use, 등락율_UD, 등락율_조건);
                }

                double 고가대비 = Form1.Acc.피_고가대비;
                double 고가대비_use = GenieConfig.TB_p_go_use;
                int 고가대비_UD = GenieConfig.combo_p_go_UD;
                int 고가대비_조건 = GenieConfig.combo_p_go;

                if (고가대비_조건 > 0)
                {
                    if (신규.Equals("신규")) 신규 = 신규계산(고가대비, 고가대비_use, 고가대비_UD, 고가대비_조건);
                    if (추매.Equals("추매")) 추매 = 추매계산(고가대비, 고가대비_use, 고가대비_UD, 고가대비_조건);
                }

                double 저가대비 = Form1.Acc.피_저가대비;
                double 저가대비_use = GenieConfig.TB_p_down_use;
                int 저가대비_UD = GenieConfig.combo_p_down_UD;
                int 저가대비_조건 = GenieConfig.combo_p_down;

                if (저가대비_조건 > 0)
                {
                    if (신규.Equals("신규")) 신규 = 신규계산(저가대비, 저가대비_use, 저가대비_UD, 저가대비_조건);
                    if (추매.Equals("추매")) 추매 = 추매계산(저가대비, 저가대비_use, 저가대비_UD, 저가대비_조건);
                }
            }
            else
            {
                double 등락율 = Form1.Acc.닥_등락률;
                double 등락율_use = GenieConfig.TB_q_ratio_use;
                int 등락율_UD = GenieConfig.combo_q_ratio_UD;
                int 등락율_조건 = GenieConfig.combo_q_ratio;

                if (등락율_조건 > 0)
                {
                    신규 = 신규계산(등락율, 등락율_use, 등락율_UD, 등락율_조건);
                    추매 = 추매계산(등락율, 등락율_use, 등락율_UD, 등락율_조건);
                }

                double 고가대비 = Form1.Acc.닥_고가대비;
                double 고가대비_use = GenieConfig.TB_q_go_use;
                int 고가대비_UD = GenieConfig.combo_q_go_UD;
                int 고가대비_조건 = GenieConfig.combo_q_go;

                if (고가대비_조건 > 0)
                {
                    if (신규.Equals("신규")) 신규 = 신규계산(고가대비, 고가대비_use, 고가대비_UD, 고가대비_조건);
                    if (추매.Equals("추매")) 추매 = 추매계산(고가대비, 고가대비_use, 고가대비_UD, 고가대비_조건);
                }

                double 저가대비 = Form1.Acc.닥_저가대비;
                double 저가대비_use = GenieConfig.TB_q_down_use;
                int 저가대비_UD = GenieConfig.combo_q_down_UD;
                int 저가대비_조건 = GenieConfig.combo_q_down;

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
            // 1. 순매수 주체 확인 및 기본값 설정
            bool 외인 = true;
            bool 기관 = true;
            bool 프로그램 = true;
            bool 신규매수 = false;
            bool 추가매수 = false;

            // 코스피/코스닥에 따라 외인/기관/프로그램 순매수 여부 설정 (0 이하이면 순매도가 간주되어 false)
            if (업종.Equals("코스피"))
            {
                if (Form1.Acc.피_외인 <= 0) 외인 = false;
                if (Form1.Acc.피_기관 <= 0) 기관 = false;
                if (Form1.Acc.피_프로그램 <= 0) 프로그램 = false;
            }
            else // 코스닥
            {
                if (Form1.Acc.닥_외인 <= 0) 외인 = false;
                if (Form1.Acc.닥_기관 <= 0) 기관 = false;
                if (Form1.Acc.닥_프로그램 <= 0) 프로그램 = false;
            }

            // 지수연동 상태 (지수 매매 제한) 확인
            if (지수연동(업종).Contains("신규")) 신규매수 = true;
            if (지수연동(업종).Contains("추매")) 추가매수 = true;

            // 2. 설정 값에 따른 신규/추가 매수 허용 여부 계산
            bool 신규_허용 = CalculateBuyPermission(
                GenieConfig.CBB_지수연동_신규,
                외인, 기관, 프로그램, 신규매수
            );

            bool 추가_허용 = CalculateBuyPermission(
                GenieConfig.CBB_지수연동_추매,
                외인, 기관, 프로그램, 추가매수
            );

            // 3. Form1 변수에 결과 반영 (중복 로직 최소화)
            if (Form1.NXT_server)
            {
                // NXT_server 모드에서는 무조건 true
                if (업종.Equals("코스피"))
                {
                    Form1.신규_지수업종연동통과_피 = true;
                    Form1.추가_지수업종연동통과_피 = true;
                }
                else
                {
                    Form1.신규_지수업종연동통과_닥 = true;
                    Form1.추가_지수업종연동통과_닥 = true;
                }
            }
            else
            {
                // 일반 모드에서는 계산된 결과 반영
                if (업종.Equals("코스피"))
                {
                    Form1.신규_지수업종연동통과_피 = 신규_허용;
                    Form1.추가_지수업종연동통과_피 = 추가_허용;
                }
                else
                {
                    Form1.신규_지수업종연동통과_닥 = 신규_허용;
                    Form1.추가_지수업종연동통과_닥 = 추가_허용;
                }
            }

            // 4. UI 업데이트
             Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                지수연동_print(업종);
            });
        }

      
        private static bool CalculateBuyPermission(int index, bool 외인, bool 기관, bool 프로그램, bool 지수)
        {
            // index에 따른 논리 연산을 switch-case로 구조화하여 중복을 제거합니다.
            switch (index)
            {
                case 0:  // ( X ): 무조건 허용
                    return true;
                case 1:  // 지수 매매제한
                    return 지수;
                case 2:  // 외국인 순매수
                    return 외인;
                case 3:  // 기관 순매수
                    return 기관;
                case 4:  // 외인 or 기관 순매수
                    return 외인 || 기관;
                case 5:  // 외 or 기 or 프 순매수
                    return 외인 || 기관 || 프로그램;
                case 6:  // 외인 and 기관 순매수
                    return 외인 && 기관;
                case 7:  // 외 and 기 and 프 순매
                    return 외인 && 기관 && 프로그램;
                case 8:  // 외인 순매수 n 지수 (AND)
                    return 외인 && 지수;
                case 9:  // 기관 순매수 n 지수 (AND)
                    return 기관 && 지수;
                case 10: // 외 o 기 순매 n 지수 (OR, AND)
                    return (외인 || 기관) && 지수;
                case 11: // 외o기o프 순매 n 지수 (OR, AND)
                    return (외인 || 기관 || 프로그램) && 지수;
                case 12: // 외 n 기 순매 n 지수 (AND, AND)
                    return 외인 && 기관 && 지수;
                case 13: // 외n기n프 순매 n 지수 (AND, AND)
                    return 외인 && 기관 && 프로그램 && 지수;
                case 14: // 외인 순매수 o 지수 (OR)
                    return 외인 || 지수;
                case 15: // 기관 순매수 o 지수 (OR)
                    return 기관 || 지수;
                case 16: // 외 o 기 순매 o 지수 (OR, OR)
                    return (외인 || 기관) || 지수;
                case 17: // 외o기o프 순매 o 지수 (OR, OR)
                    return (외인 || 기관 || 프로그램) || 지수;
                case 18: // 외 n 기 순매 o 지수 (AND, OR)
                    return (외인 && 기관) || 지수;
                case 19: // 외n기n프 순매 o 지수 (AND, OR)
                    return (외인 && 기관 && 프로그램) || 지수;
                default:
                    // 정의되지 않은 index 값에 대한 안전 장치 (예: 기본적으로 허용하지 않음)
                    return false;
            }
        }

        public static void 지수연동_print(string 업종)
        {
            if (업종.Equals("코스피"))
            {
                Form1.form1.LB_피외인.Text = "외:" + Form1.Acc.피_외인.ToString();
                Form1.form1.LB_피기관.Text = "기:" + Form1.Acc.피_기관.ToString();
                Form1.form1.LB_피프로그램.Text = "프:" + Form1.Acc.피_프로그램.ToString();

                if (Form1.Acc.피_외인 == 0)
                {
                    Form1.form1.LB_피외인.ForeColor = Color.Black;
                }
                else
                {
                    if (Form1.Acc.피_외인 > 0)
                    { Form1.form1.LB_피외인.ForeColor = Color.Red; }
                    else
                    { Form1.form1.LB_피외인.ForeColor = Color.Blue; }
                }

                if (Form1.Acc.피_기관 == 0)
                { Form1.form1.LB_피기관.ForeColor = Color.Black; }
                else
                {
                    if (Form1.Acc.피_기관 > 0)
                    { Form1.form1.LB_피기관.ForeColor = Color.Red; }
                    else
                    { Form1.form1.LB_피기관.ForeColor = Color.Blue; }
                }

                if (Form1.Acc.피_프로그램 == 0)
                { Form1.form1.LB_피프로그램.ForeColor = Color.Black; }
                else
                {
                    if (Form1.Acc.피_프로그램 > 0)
                    { Form1.form1.LB_피프로그램.ForeColor = Color.Red; }
                    else
                    { Form1.form1.LB_피프로그램.ForeColor = Color.Blue; }
                }
            }
            else
            {
                Form1.form1.LB_닥외인.Text = "외:" + Form1.Acc.닥_외인.ToString();
                Form1.form1.LB_닥기관.Text = "기:" + Form1.Acc.닥_기관.ToString();
                Form1.form1.LB_닥프로그램.Text = "프:" + Form1.Acc.닥_프로그램.ToString();

                if (Form1.Acc.닥_외인 == 0)
                {
                    Form1.form1.LB_닥외인.ForeColor = Color.Black;
                }
                else
                {
                    if (Form1.Acc.닥_외인 > 0)
                    {
                        Form1.form1.LB_닥외인.ForeColor = Color.Red;
                    }
                    else
                    {
                        Form1.form1.LB_닥외인.ForeColor = Color.Blue;
                    }
                }

                if (Form1.Acc.닥_기관 == 0)
                {
                    Form1.form1.LB_닥기관.ForeColor = Color.Black;
                }
                else
                {
                    if (Form1.Acc.닥_기관 > 0)
                    {
                        Form1.form1.LB_닥기관.ForeColor = Color.Red;
                    }
                    else
                    {
                        Form1.form1.LB_닥기관.ForeColor = Color.Blue;
                    }
                }

                if (Form1.Acc.닥_프로그램 == 0)
                { Form1.form1.LB_닥프로그램.ForeColor = Color.Black; }
                else
                {
                    if (Form1.Acc.닥_프로그램 > 0)
                    { Form1.form1.LB_닥프로그램.ForeColor = Color.Red; }
                    else
                    { Form1.form1.LB_닥프로그램.ForeColor = Color.Blue; }
                }
            }
          
            // 호출하실 때는 이렇게 그룹명을 같이 던져주셔야 합니다. CB_지수이평_신규, CB_지수이평_그외,
            print("CB_지수이평_신규", "신규");
            print("CB_지수이평_그외", "추매_그외");

            void print(string 타겟그룹명, string 위치)
            {
                if (!Form1.그룹별_지수사용값.TryGetValue(타겟그룹명, out 지수이평사용값[] 현재그룹지수))
                {
                    return;
                }

                bool result = true;

                // [*변경포인트*] 클래스명 교체
                지수이평사용값 avg = 현재그룹지수[0];

                bool stop = avg.사용유무;

                if (!업종.Equals("코스피"))
                    avg = 현재그룹지수[1]; // 코스닥일 경우 인덱스 1번 사용

                if (!위치.Equals("신규"))
                    stop = avg.사용유무;

                if (stop)
                {
                    if (result) result = avg.결과값_min_03;
                    if (result) result = avg.결과값_min_05;
                    if (result) result = avg.결과값_min_10;
                    if (result) result = avg.결과값_min_20;
                    if (result) result = avg.결과값_min_30;
                    if (result) result = avg.결과값_min_60;

                    if (Form1.NXT_server) result = true;

                    if (result) result = avg.결과값_day_03;
                    if (result) result = avg.결과값_day_05;
                    if (result) result = avg.결과값_day_10;
                    if (result) result = avg.결과값_day_20;
                    if (result) result = avg.결과값_day_40;
                    if (result) result = avg.결과값_day_60;
                }


                if (위치.Equals("신규"))
                {
                    if (업종.Equals("코스피"))
                    {
                        if (Form1.신규_지수업종연동통과_피 && result)
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
                        if (Form1.신규_지수업종연동통과_닥 && result)
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
                        if (Form1.추가_지수업종연동통과_피 && result)
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
                        if (Form1.추가_지수업종연동통과_닥 && result)
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
