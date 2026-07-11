//using System;
//using System.Diagnostics;           //Process 
//using System.Windows.Forms;

//namespace 지니64
//{
//    static class Program
//    {
//        [STAThread]

//        static void Main()
//        {
//            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

//             Form1.Console_print("프로그램 실행");

//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);

//            // 1. 메인 폼 생성
//            Form1 mainForm = new Form1();

//            // 2. [핵심] 폼이 닫힐 때(사용자가 X 버튼 누를 때) 모든 스레드를 종료하도록 설정
//            mainForm.FormClosed += (sender, e) =>
//            {
//                Application.ExitThread();
//                Environment.Exit(0);
//            };

//            Application.Run(mainForm);
//        }

//    }
//}


using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace 지니64
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // 1. 기존 설정 유지 (TLS 1.2 통신 보안 설정)
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            // 2. 폴더 기반 중복 실행 방지 (Mutex)
            string 실행경로 = Application.StartupPath;
            string 뮤텍스이름 = "Genie_" + 실행경로.Replace("\\", "_").Replace(":", "_");
            bool 신규실행여부;

            // 뮤텍스를 통해 현재 폴더 경로에 대한 소유권을 확인합니다.
            using (Mutex 고유뮤텍스 = new Mutex(true, 뮤텍스이름, out 신규실행여부))
            {
                if (!신규실행여부)
                {
                    // 이미 해당 폴더에서 실행 중인 경우 경고창을 띄우고 즉시 종료
                    MessageBox.Show("해당 폴더에서 이미 지니64 프로그램이 실행 중입니다.", "중복 실행 방지", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. 중복이 아닐 경우 기존 메인 로직 정상 실행
                Form1.Console_print("프로그램 실행");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // 메인 폼 생성
                Form1 mainForm = new Form1();

                // [핵심] 폼이 닫힐 때(사용자가 X 버튼 누를 때) 모든 스레드를 종료하도록 설정
                mainForm.FormClosed += (sender, e) =>
                {
                    Application.ExitThread();
                    Environment.Exit(0);
                };

                Application.Run(mainForm);
            }
        }
    }
}