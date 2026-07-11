using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace 지니64
{
    internal class login
    {
        public static bool IsDebugMode
        {
            get
            {
               // return false;


#if DEBUG
                return true;  // 디버그 모드일 때는 true를 반환
#else
                        return false; // 릴리즈 모드일 때는 false를 반환
#endif
            }
        }

        public static void 릴리즈체크()
        {
            if (IsDebugMode)
            {
                Form1.serverIp = "192.168.0.2";
                Form1.릴리즈 = false;
            }
            else
            {
                Form1.serverIp = "211.37.175.15";
                Form1.릴리즈 = true;
            }

            if (Form1.릴리즈)
            {
                bool 올바른폴더 = false;

                switch (Form1.startupPath)
                {
                    case @"C:\지니64_A":
                    case @"C:\지니64_B":
                    case @"C:\지니64_C":
                    case @"C:\지니64_D":
                        올바른폴더 = true;
                        break;

                    case @"C:\CS_START\지니64\bin\Debug":
                        올바른폴더 = true;
                        Form1.텔레그램TEST = true;

                        Form1.Console_print($"릴리즈체크:{Form1.릴리즈} 폴더: {Form1.startupPath} ");

                        break;
                }

                // 💡 [최적화 2] 4개 중 하나라도 맞지 않으면(올바른폴더 == false) 종료 프로세스 실행
                if (!올바른폴더)
                {
                    // 💡 [최적화 3] 문자열 더하기(+) 대신, 메모리 낭비가 적은 보간법($) 사용
                    MessageBox.Show($"현재 설치폴더가 '{Form1.startupPath}' 입니다.\n설치폴더를 'C:\\, 지니64_A, 지니64_B, 지니64_C, 지니64_D' 에 설치하기 바랍니다.\n프로그램을 종료합니다.",
                                    "🚨 설치 경로 오류",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    // 💡 [궁극의 최적화 4] Process를 무겁게 부르지 않고, CPU 점유율 0%로 가장 빠르고 깔끔하게 프로그램 전원을 뽑아버립니다.
                    Environment.Exit(0);
                }
                else
                {
                    string File_ = Form1.startupPath + @"\BotVersion.txt";
                    FileInfo BotVersion_Check = new FileInfo(File_);

                    if (BotVersion_Check.Exists)
                    {
                        // =========================================================
                        // 1. 파일 검증 및 접속 ID 로딩 (새로운 줄바꿈 방식 적용)
                        // =========================================================
                        try
                        {
                            // 텍스트 전체를 한 번에 줄(Line) 단위 배열로 싹 읽어옵니다.
                            string[] 읽은데이터 = File.ReadAllLines(File_, System.Text.Encoding.UTF8);

                            // 최소 3줄(버전, Run여부, 아이디)은 있어야 검증이 가능합니다.
                            if (읽은데이터.Length >= 3)
                            {
                                // 읽은데이터[1] = True/False (기존 Split('^')[1] 자리)
                                bool.TryParse(읽은데이터[1].Trim(), out bool Run);

                                if (Run)
                                {
                                    // 읽은데이터[2] = 아이디 (기존 Split('^')[2] 자리)
                                    Form1.접속_ID = 읽은데이터[2].Trim();
                                }
                                else
                                {
                                    logout.접속종료();
                                    Form1.form1.Message_Alram("HI지니64 로 접속 바랍니다.", "접속방법");
                                    Environment.Exit(0);
                                    return; // 봇이 꺼지므로 아래 저장 로직 건너뜀
                                }
                            }
                            else
                            {
                                // 줄 수가 부족하면 파일이 깨진 것으로 간주하고 아래 catch 블록으로 던집니다.
                                throw new Exception("파일 데이터 부족");
                            }
                        }
                        catch
                        {
                            Form1.form1.Message_Alram("HI지니64 로 접속 바랍니다. 로딩에러 BotVersion.txt 파일이 손상 되었습니다.", "접속방법");
                            Environment.Exit(0);
                            return; // 봇이 꺼지므로 아래 저장 로직 건너뜀
                        }

                        // =========================================================
                        // 2. 버전 정보 갈아끼우기 (저장)
                        // =========================================================
                        try
                        {
                            Form1.Console_print("버전 저장: " + Form1.버전_서버);

                            // 위에서 검증을 통과했으므로 안심하고 다시 줄 단위로 읽어옵니다.
                            string[] 모든줄 = System.IO.File.ReadAllLines(File_, System.Text.Encoding.UTF8);

                            if (모든줄.Length > 0)
                            {
                                // 첫 번째 줄(0번 인덱스)의 버전만 새 버전으로 싹 바꿉니다.
                                if (!Form1.텔레그램TEST)
                                {
                                    모든줄[0] = Form1.버전_디버그;
                                    모든줄[1] = "False";
                                    모든줄[2] = "";
                                    모든줄[3] = "";
                                    모든줄[4] = "";
                                    모든줄[5] = "";
                                    모든줄[6] = "";
                                }

                                // 나머지 계좌번호, 앱키 등은 건드리지 않고, 수정된 배열을 통째로 덮어씁니다.
                                System.IO.File.WriteAllLines(File_, 모든줄, System.Text.Encoding.UTF8);
                            }
                            else
                            {
                                // 혹시라도 파일이 비어있을 경우 예외 처리
                                System.IO.File.WriteAllText(File_, Form1.버전_디버그 + Environment.NewLine + "False", System.Text.Encoding.UTF8);
                            }
                        }
                        catch (Exception ex)
                        {
                            Form1.Console_print($"버전 저장 실패: {ex.Message}");
                        }
                    }
                    else
                    {
                        Form1.form1.Message_Alram("HI지니64 로 접속 바랍니다. 로딩에러 BotVersion.txt 파일이 손상 되었습니다.", "접속방법");
                        Environment.Exit(0);
                    }
                }
            }
        }



        public static void 접속확인()
        {
            string hostname = SystemInformation.ComputerName;
            IPAddress[] listIPAddress = Dns.GetHostAddresses(hostname);
            IPAddress[] listIPV4 = listIPAddress.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToArray();
            string IPV4 = listIPV4[0].ToString();

            long 예수금 = Form1.Acc.D2 + Form1.Acc.매입금;
            long 실현손익 = Form1.Acc.실현손익;
            long 평가예수금 = Form1.Acc.D2 + Form1.Acc.평가금;

            if (평가예수금 < 예수금)
                예수금 = 평가예수금;

            Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                try
                {
                    const int serverPort = 5425;

                    TcpClient client = new TcpClient();
                    IPEndPoint serverAdress = new IPEndPoint(IPAddress.Parse(Form1.serverIp), serverPort);
           
                    Form1.Console_print($"클라이언트: {client.ToString()}, 서버: {serverAdress.ToString()}");
           
                    client.Connect(serverAdress);
                    NetworkStream stream = client.GetStream();
           
                    string server_ = "모";
                    if (Form1.server.Equals("실서버")) server_ = "실";
           
                    string message = "Check_JinE^" + GenieConfig.textBox_ID + "^" + IPV4 + "^" + 예수금 + "^" + 실현손익 + "^" + server_;
                    byte[] data = Encoding.Default.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    Form1.Console_print($"송신: {message}");
           
                    data = new byte[256];
                    string responseData = "";
                    int bytes = stream.Read(data, 0, data.Length);
                    responseData = Encoding.Default.GetString(data, 0, bytes);
                    Form1.Console_print($"수신: {responseData}");
           
                    Form1.Console_print("서버수신: " + responseData);
           
                    if (responseData.Split('^')[0].Contains("접속"))
                    {
                        Form1.Console_print("접속확인: " + responseData);
           
                        string 라이센스 = responseData.Split('^')[1];
                        string 종료일 = responseData.Split('^')[2];
                        int.TryParse(responseData.Split('^')[3], out int 남은일);
                        Form1.str.개장일 = responseData.Split('^')[4];
                        Form1.str.수능일 = responseData.Split('^')[5];
           
                        Form1.사용기간 = 종료일;
                        Form1.form1.LB_license.Text = 라이센스;
                        Form1.form1.LB_남은기간.Text = "D - " + 남은일 + "일";
           
                        string 알림말 = "";
                        string text = "";
           
                        TelegramMessenger.Telegram_alram("logIn");
           
                        if (라이센스.Equals("VIP_0"))
                        {
                            if (Form1.server.Equals("실서버") && 예수금 > 5000000)
                            {
                                알림말 = "VIP_0 은 평가예수금[예수금 + (평가금 or 매입금)중 작은금액]\n5백만원 까지 사용가능합니다. 현재 예수금: " + 예수금.ToString("N0") + "원";
                                closed();
                            }
                            else
                            {
                                text = "VIP_0 라이센스 입니다.\n평가예수금[예수금 + (평가금 or 매입금)중 작은금액]\n5백만원 까지 가능합니다.\n60일이상 장기 미접속 시 사용해지 됩니다.";
                            }
                        }
                        else
                        {
                            if (남은일 > 0)
                            {
                                switch (라이센스)
                                {
                                    case "VIP_1":
                                        if (Form1.server.Equals("실서버") && 예수금 > 100000000)
                                        {
                                            알림말 = "VIP_1 은 평가예수금[예수금 + (평가금 or 매입금)중 작은금액]\n1억원 까지 사용가능합니다. 현재 예수금: " + 예수금.ToString("N0") + "원";
                                            closed();
                                        }
                                        else
                                        {
                                            text = "VIP_1 라이센스 입니다.\n평가예수금[예수금 + (평가금 or 매입금)중 작은금액]\n1억원 까지 가능합니다. 현재 예수금: " + 예수금.ToString("N0") + "원\n종료일: " + 남은일 + " 일 남았습니다.";
                                        }
                                        break;
           
                                    case "VIP_2":
                                        if (Form1.server.Equals("실서버") && 예수금 > 200000000)
                                        {
                                            알림말 = "VIP_2 은 평가예수금[예수금 + (평가금 or 매입금)중 작은금액]\n2억원 까지 사용가능합니다. 현재 예수금: " + 예수금.ToString("N0") + "원";
                                            closed();
                                        }
                                        else
                                        {
                                            text = "VIP_2 라이센스 입니다.\n평가예수금[예수금 + (평가금 or 매입금)중 작은금액]\n2억원 까지 가능합니다. 현재 예수금: " + 예수금.ToString("N0") + "원\n종료일: " + 남은일 + " 일 남았습니다.";
                                        }
                                        break;
           
                                    case "VIP_3":
                                        if (Form1.server.Equals("실서버") && 예수금 > 300000000)
                                        {
                                            알림말 = "VIP_3 은 평가예수금[예수금 + (평가금 or 매입금)중 작은금액]\n3억원 까지 사용가능합니다. 현재 예수금: " + 예수금.ToString("N0") + "원";
                                            closed();
                                        }
                                        else
                                        {
                                            text = "VIP_3 라이센스 입니다.\n평가예수금[예수금 + (평가금 or 매입금)중 작은금액]\n3억원 까지 가능합니다. 현재 예수금: " + 예수금.ToString("N0") + "원\n종료일: " + 남은일 + " 일 남았습니다.";
                                        }
                                        break;
           
                                    case "VIP_4":
                                        if (Form1.server.Equals("실서버") && 예수금 > 400000000)
                                        {
                                            알림말 = "VIP_4 은 평가예수금[예수금 + (평가금 or 매입금)중 작은금액]\n4억원 까지 사용가능합니다. 현재 예수금: " + 예수금.ToString("N0") + "원";
                                            closed();
                                        }
                                        else
                                        {
                                            text = "VIP_4 라이센스 입니다.\n평가예수금[예수금 + (평가금 or 매입금)중 작은금액]\n4억원 까지 가능합니다. 현재 예수금: " + 예수금.ToString("N0") + "원\n종료일: " + 남은일 + " 일 남았습니다.";
                                        }
                                        break;
           
                                    case "VIP_5":
                                        if (Form1.server.Equals("실서버") && 예수금 > 500000000)
                                        {
                                            알림말 = "VIP_5 은 평가예수금[예수금 + (평가금 or 매입금)중 작은금액]\n5억원 까지 사용가능합니다. 현재 예수금: " + 예수금.ToString("N0") + "원";
                                            closed();
                                        }
                                        else
                                        {
                                            text = "VIP_5 라이센스 입니다.\n평가예수금[예수금 + (평가금 or 매입금)중 작은금액]\n5억원 까지 가능합니다. 현재 예수금: " + 예수금.ToString("N0") + "원\n종료일: " + 남은일 + " 일 남았습니다.";
                                        }
                                        break;
           
                                    case "VIP_6":
           
                                        text = "VIP_6 라이센스 입니다.\n평가예수금[예수금 + (평가금 or 매입금)중 작은금액]\n제한 없이 사용 가능합니다.\n종료일: " + 남은일 + " 일 남았습니다.";
                                        break;
                                }
                            }
                            else
                            {
                                알림말 = "사용기간이 종료 되었습니다.";
                                closed();
                            }
                        }
           
                        Form1.MBC_sender = "";
                        Form1.중요메세지("라이센스 알림", text);
           
                        void closed()
                        {
                            Form1.form1.Message_Alram(알림말, "지니64종료");
                            logout.접속종료();
                            _ = Form1.form1.Form_Closing();
                        }
                    }
                }
                catch
                {
                    logout.접속종료();
                    Form1.form1.Message_Alram("지니64 서버에 접속할수 없습니다. 잠시후 다시 접속해 주세요.", "서버다운");
                    Environment.Exit(0);
                }
            });
        }

    }
}
