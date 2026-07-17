using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using 지니64.RESTAPI;

namespace 지니64.Timer
{
    public class TR_timer_ : Form1
    {
        private static int 로딩점_상태 = 0;
        public static void Tick()
        {
            if (Form1.매매시작.Equals("Loding_00_목록조회"))
            {

                Helper.안전한_UI_업데이트(form1, () =>
                {
                    // 💡 [핵심] 서브폼이 화면에 표시된 직후 강제로 포커스를 이동시킵니다.
                    if (form1.JanGo_dataGridView != null && !form1.JanGo_dataGridView.IsDisposed)
                    {
                        form1.JanGo_dataGridView.Focus();    // 포커스 이동
                    }
                });

                Console_print("Loding_00_목록조회 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "목록조회";
                REG.검색식목록조회_로딩();  //사용자 조건식 불러오기

            }

            if (Form1.매매시작.Equals("Loding_01_코스피리스트요청"))
            {
                Console_print("Loding_01_코스피리스트요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "코스피리스트요청";
                TR_loding.종목정보리스트("0", false);
            }

            if (Form1.매매시작.Equals("Loding_02_코스닥리스트요청"))
            {
                Console_print("Loding_02_코스닥리스트요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "코스닥리스트요청";
                TR_loding.종목정보리스트("10", false);
            }

            if (Form1.매매시작.Equals("Loding_03_코스피현재가요청"))
            {
                Console_print("Loding_03_코스피현재가요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "코스피현재가요청";
                TR_loding.코스피현재가요청(false);
            }

            if (Form1.매매시작.Equals("Loding_04_코스닥현재가요청"))
            {
                Console_print("Loding_04_코스닥현재가요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "코스닥현재가요청";
                TR_loding.코스닥현재가요청(false);
            }

            if (Form1.매매시작.Equals("Loding_05_코스피분봉조회요청"))
            {
                Console_print("Loding_05_코스피분봉조회요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "코스피분봉조회요청";
                TR_loding.업종분봉조회요청("001", false);
            }

            if (Form1.매매시작.Equals("Loding_06_코스피일봉조회요청"))
            {
                Console_print("Loding_06_코스피일봉조회요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "코스피일봉조회요청";
                TR_loding.업종일봉조회요청("001", false);
            }

            if (Form1.매매시작.Equals("Loding_07_코스닥분봉조회요청"))
            {
                Console_print("Loding_07_코스닥분봉조회요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "코스닥분봉조회요청";
                TR_loding.업종분봉조회요청("101", false);
            }

            if (Form1.매매시작.Equals("Loding_08_코스닥일봉조회요청"))
            {
                Console_print("Loding_08_코스닥일봉조회요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "코스닥일봉조회요청";
                TR_loding.업종일봉조회요청("101", false);
            }

            if (Form1.매매시작.Equals("Loding_09_업종순매수요청"))
            {
                Console_print("Loding_09_업종순매수요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "업종순매수요청";
                TR_요청.코스피투자자순매수요청(false);
            }

            if (Form1.매매시작.Equals("Loding_10_프로그램순매수요청"))
            {
                Console_print("Loding_10_프로그램순매수요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "프로그램순매수요청";
                TR_요청.코스피_프로그램매매추이요청시간대별(false);
            }

            if (Form1.매매시작.Equals("Loding_11_미체결요청"))
            {
                Console_print("Loding_11_미체결요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                LoadFromFile.주문리스트_파일로딩();

                Form1.매매시작 = "미체결요청";
                TR_요청.미체결요청("Y", "", false);
            }

            if (Form1.매매시작.Equals("Loding_12_계좌평가현황요청"))
            {
                Helper.안전한_UI_업데이트(Form1.form1, () =>
                {
                   form1.checkBox_key.Checked = false;
                });

                Console_print($"잔고로딩시작 합니다. 계좌번호 [{GenieConfig.textBox_계좌번호}]");
                Log.동작기록("사용자 정보를 불러옵니다. ");

                LoadFromFile.투자원금_계좌TS_파일로딩();
                LoadFromFile.잔고_파일로딩(); 
                LoadFromFile.최종매입가_파일로딩();
                LoadFromFile.주문예약_파일로딩();

                LoadFromFile.관심그룹_List_파일로딩();
                LoadFromFile.관심그룹_Title_파일로딩();
                LoadFromFile.리밸감시주문_파일로딩();

                Console_print("Loding_12_계좌평가현황요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "계좌평가현황요청";
                TR_요청.계좌평가현황요청("Y", "", false);
            }
        
            if (Form1.매매시작.Equals("Loding_13_일자별실현손익요청"))
            {
                Console_print("Loding_13_일자별실현손익요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "일자별실현손익요청";
                TR_요청.실현손익요청("Y", "", true);
            }


            if (Form1.매매시작.Equals("Loding_14_계좌평가잔고내역요청")) //전체잔고 요청
            {
                Console_print("Loding_14_계좌평가잔고내역요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "계좌평가잔고내역요청";
                TR_요청.계좌평가잔고내역요청("Y", "", false);
            }

            if (Form1.매매시작.Equals("Loding_15_계좌수익률요청")) // 신용잔고 요청
            {
                Console_print("Loding_15_계좌수익률요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
                Form1.매매시작 = "계좌수익률요청";
                TR_요청.계좌수익률요청("Y", "", false);
            }

            //if (Form1.매매시작.Equals("Loding_16_신용융자가능종목요청"))
            //{
            //    Console_print("Loding_16_신용융자가능종목요청 TR_timer_ Tick " + DateTime.Now.ToString("HH:mm:ss.fff"));
            //    Form1.매매시작 = "신용융자가능종목요청";
            //    TR_요청.신용융자가능종목요청("N", "", false);
            //}

            if (!Form1.매매시작.Equals("매매시작"))
            {
                // Console_print($"\n################################     매매시작  [{Form1.매매시작}]");

                string Time = DateTime.Now.ToLongTimeString();

                // 1. 타이머가 돌 때마다 점의 상태를 1씩 증가시킵니다.
                로딩점_상태++;

                // 2. 점이 3개를 초과하면 다시 1개로 되돌립니다.
                if (로딩점_상태 > 5)
                {
                    로딩점_상태 = 1;
                }

                // 3. 현재 상태 숫자만큼 점(.)으로 이루어진 문자열을 생성합니다. (예: 1=".", 2="..", 3="...")
                string 점 = new string('.', 로딩점_상태);

                // [+] 메인폼 방어 코드 (메인폼이 닫히는 순간 발생할 수 있는 에러도 함께 차단합니다)
                if (Form1.form1 != null && !Form1.form1.IsDisposed)
                {
                    Form1.form1.Text = Form1.프로그램명 + " " + Form1.버전_디버그 + " / " + GenieConfig.textBox_ID + " / " + str.Week + " / [ " + Time + " ] / " + Form1.server + " / " + Form1.매매시작 + " / 검색식 Run: " + Form1.Run_condition_count +
                     "개 / 주문 count 초당- " + Get.주문_S + "회 분당- " + Get.주문_M + "회 시간당- " + Get.주문_H + "회 ( ※ 키움서버 주문제한 : 시간당 3600회 입니다.) 사용기간: " + Form1.사용기간 + " 까지" + " / loading " + 점;
                }

                // [+] 서브폼 방어 코드 및 로딩 애니메이션 처리
                if (Sub_form.form != null && !Sub_form.form.IsDisposed)
                {
                    // 4. 서브폼 제목줄 끝에 생성된 점을 붙여줍니다.
                    Sub_form.form.Text = Form1.프로그램명 + " / " + str.Week + " / [ " + Time + " ] / " + Form1.server + " / " + Form1.매매시작 + " / loading " + 점;
                }
            }
        }
    }
}
