using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64
{
    internal class Han_Get_token
    {
        // 저사양 PC 최적화 (소켓 고갈 방지 및 속도 향상)를 위해 단일 HttpClient 인스턴스 재사용
        private static readonly HttpClient 통신클라이언트 = new HttpClient();

        //// 실전투자
        //// 계좌번호
        //// 44507402
        //private static string 앱키 = "PSnlC4LshJt1CuL8v3Mkgz5mltFJUY4XAHjq";
        //private static string 앱시크릿 = "SNLoYix/GyNiyV4h+712kmLun1nZACfG6C6Rogz8lJgvBSbX/qBULnCLMcC9gq5mZKqBT9QOSxx8q8LPiaQVHTrYEffqoOXCx92c1a8gOohgN6A2MEbLWD9gQNsJR38o95g1NENTdV//oUq0tD2SboiAVs8YbbUziSyFYQcsNzmVmVTAbJI=";

        // 모의투자
        // 계좌번호
        // 50195859

        private static string 앱키 = "PSvLmsCvS1DhcKT5VdhmfrdXq2qHUBBfWRr6";
        private static string 앱시크릿 = "eVuj3XamwFEq0AT3K4cOVp9m0j/bLRkD172b1n3NREfXyVWU7JFWhhA/7ghFFpkuMBTbTzssSReLBb6OcgCimKphVct5kmyjv9qihQi/QmxmNNWfybS59DDEGio99pYKwWiaD1hyLmjwkF+ZGlQM8VXPbxqRVkkkrxkv/FWYkMHmipj46Do=";



        public static void logIn()
        {
            // 한국투자증권은 보안 프로토콜(TLS 1.2)이 필수이므로 연결 전 명시적으로 설정
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Form1.tr_scheduler.EnqueuePriorityRequest("fn_kis_token", 요청);

            async Task 요청()
            {
                try
                {
                    string 접속주소 = "https://openapi.koreainvestment.com:9443"; // 실전투자
                    if (GenieConfig.checkBox_Simulation) 접속주소 = "https://openapivts.koreainvestment.com:29443"; // 모의투자

                    await 한투_토큰발급_요청(접속주소, 앱키, 앱시크릿);
                }
                catch (Exception 예외)
                {
                    Form1.Console_print("[-] 한투 토큰 발급 요청 실패: " + 예외.Message);
                }
            }
        }

        static async Task 한투_토큰발급_요청(string 접속주소, string 앱키, string 시크릿키)
        {
            // 1. 요청할 API URL (한국투자증권 접근토큰 발급)
            string 엔드포인트 = "/oauth2/tokenP";
            string 요청주소 = 접속주소 + 엔드포인트;

            // 2. 요청 데이터 (한투 API 기준 appsecret 사용)
            var 요청데이터 = new
            {
                grant_type = "client_credentials",
                appkey = 앱키,
                appsecret = 시크릿키
            };

            // 3. header 데이터
            통신클라이언트.DefaultRequestHeaders.Accept.Clear();
            통신클라이언트.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // 4. HTTP POST 요청 구성
            var 제이슨문자열 = JsonConvert.SerializeObject(요청데이터);
            var 본문데이터 = new StringContent(제이슨문자열, Encoding.UTF8, "application/json");

            try
            {
                // ======================================================================
                var 응답결과 = await 통신클라이언트.PostAsync(요청주소, 본문데이터);
                var 응답본문 = await 응답결과.Content.ReadAsStringAsync();

                // [레이더 가동] 서버가 HTML 문서를 던졌거나, 점검 안내 멘트가 포함되어 있는지 확인
                if (응답본문.StartsWith("<") || 응답본문.Contains("시스템작업") || 응답본문.Contains("서비스 중단") || 응답본문.Contains("점검"))
                {
                    Form1.Console_print(">>> 한국투자증권 서버 정기 점검이 감지되었습니다.");

                    string 순수텍스트 = System.Text.RegularExpressions.Regex.Replace(응답본문, "<[^>]*>", "");
                    MessageBox.Show($"현재 한국투자증권 서버 점검 또는 서비스 중단 시간입니다.\n\n[본문 내용]\n{순수텍스트}\n\n점검이 끝난 후 봇을 다시 실행해 주세요.",
                                            "[!] 한국투자증권 서버 점검중",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);

                    Environment.Exit(0);
                    return;
                }

                if (!응답결과.IsSuccessStatusCode)
                {
                    Form1.Console_print($">>> 서버 통신 오류 (상태 코드: {(int)응답결과.StatusCode})");
                    Form1.Console_print($"[응답 원본] {응답본문}");
                    return;
                }

                // ======================================================================
                try
                {
                    JObject 제이슨객체 = JObject.Parse(응답본문);

                    // 한투는 성공 시 access_token 필드를 반환
                    if (제이슨객체["access_token"] != null)
                    {
                        string 발급토큰 = 제이슨객체["access_token"].ToString();
                        string 만료시간 = 제이슨객체["access_token_token_expired"]?.ToString() ?? "시간 정보 없음";

                        Form1.Console_print("[API_LOGIN] token_type : Bearer");
                        Form1.Console_print("[API_LOGIN] access_token : " + 발급토큰);
                        Form1.Console_print("[API_LOGIN] expired : " + 만료시간);

                        Form1.API_token = 발급토큰;
                        Form1.ON_LINE = true;

                        Helper.안전한_UI_업데이트(Form1.form1, () =>
                        {
                            DataManagement.Drictroy_Create();
                            DataManagement.파일삭제();

                            Form1.form1.label_ONLINE.Text = "ON LINE";
                            if (GenieConfig.CB_신용_주문사용) Form1.form1.label_ONLINE.Text = "ON LINE (신용주문)";

                            Form1.form1.label_ONLINE.ForeColor = System.Drawing.Color.Red;
                        });

                        Form1.Console_print("[+] 지니 로그인 성공 :: " + DateTime.Now.ToString("HH:mm:ss.fff"));

                        if (GenieConfig.checkBox_Simulation)
                        {
                            Form1.server = "모의투자";
                            Log.동작기록("한국투자증권 모의투자로 접속 하였습니다.");
                            Form1.수수료 = GenieConfig.TB_mo_commission / 100.0;
                        }
                        else
                        {
                            Form1.server = "실서버";
                            Log.동작기록("한국투자증권 실서버로 접속 하였습니다.");
                            Form1.수수료 = GenieConfig.TB_sil_commission / 100.0;
                        }

                        GenieConfig.SaveAll_Genie_Config();
                        Log.동작기록("조건식 불러오기 시작");
                        Form1.매매시작 = "Loding_00_목록조회";
                    }
                    else
                    {
                        string 에러코드 = 제이슨객체["error_code"]?.ToString() ?? "알 수 없는 코드";
                        string 에러메시지 = 제이슨객체["error_description"]?.ToString() ?? "알 수 없는 에러";

                        Form1.Console_print($"[-] 로그인 실패 ({에러코드}): {에러메시지}");
                        Helper.알림창_멀티("로그인 실패", 에러메시지, 60, false);

                        Helper.안전한_UI_업데이트(Form1.form1, () =>
                        {
                            Form1.form1.checkBox_key.Checked = false;
                            Form1.form1.checkBox_key.Checked = true;
                        });
                    }
                }
                catch (JsonException)
                {
                    Form1.Console_print("[-] 응답 Body JSON 파싱 실패: " + 응답본문);
                }
            }
            catch (Exception 예외)
            {
                Form1.Console_print("[-] 통신 요청 실패: " + 예외.Message);
            }
        }

        public static void logOut()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Form1.tr_scheduler.EnqueuePriorityRequest("FnKisRevoke", 요청);

            async Task 요청()
            {
                try
                {
                    string 접속주소 = "https://openapi.koreainvestment.com:9443";
                    if (GenieConfig.checkBox_Simulation) 접속주소 = "https://openapivts.koreainvestment.com:29443";

                    await 한투_토큰폐기_요청(접속주소, 앱키, 앱시크릿);
                }
                catch (Exception 예외)
                {
                    Form1.Console_print("[-] 한투 토큰 폐기 요청 실패: " + 예외.Message);
                }
            }
        }

        static async Task 한투_토큰폐기_요청(string 접속주소, string 앱키, string 시크릿키)
        {
            // 1. 요청할 API URL (한국투자증권 접근토큰 폐기)
            string 엔드포인트 = "/oauth2/revokeP";
            string 요청주소 = 접속주소 + 엔드포인트;

            // 2. 요청 데이터 구성
            var 요청데이터 = new
            {
                appkey = 앱키,
                appsecret = 시크릿키,
                token = Form1.API_token
            };

            통신클라이언트.DefaultRequestHeaders.Accept.Clear();
            통신클라이언트.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var 제이슨문자열 = JsonConvert.SerializeObject(요청데이터);
                var 본문데이터 = new StringContent(제이슨문자열, Encoding.UTF8, "application/json");

                var 응답결과 = await 통신클라이언트.PostAsync(요청주소, 본문데이터);
                var 응답본문 = await 응답결과.Content.ReadAsStringAsync();

                Form1.Console_print("=== 한투 로그아웃(토큰폐기) 결과 ===");
                Form1.Console_print(">> Code : " + 응답결과.StatusCode);
                Form1.Console_print(">> Body : " + 응답본문);
            }
            catch (Exception 예외)
            {
                Form1.Console_print("[-] 토큰 폐기 요청 통신 실패: " + 예외.Message);
            }
        }
    }
}
