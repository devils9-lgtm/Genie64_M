//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace 지니64
//{
//    internal class Get_token
//    {
//        // 💡 [최적화 핵심] HttpClient는 프로그램 전체에서 단 1개만 생성하여 재사용합니다. (소켓 고갈 방지)
//        private static readonly HttpClient 공용_클라이언트;
//        private static string 키움_앱키 = GenieConfig.textBox_appKey;
//        private static string 키움_시크릿키 = GenieConfig.textBox_appsecret;

//        // 정적 생성자: 클래스가 처음 호출될 때 단 한 번만 헤더를 세팅합니다.
//        static Get_token()
//        {
//            공용_클라이언트 = new HttpClient();
//            공용_클라이언트.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//        }

//        public static void logIn()
//        {
//            Form1.tr_scheduler.EnqueuePriorityRequest("fn_au10001", 요청);

//            async Task 요청()
//            {
//                try
//                {
//                    string host = "https://api.kiwoom.com";
//                    if (GenieConfig.checkBox_Simulation) host = "https://mockapi.kiwoom.com";


//                    await fn_au10001(host, 키움_앱키, 키움_시크릿키);
//                }
//                catch (Exception ex)
//                {
//                    Form1.Console_print("로그인 요청 실패: " + ex.Message);
//                }
//            }
//        }

//        static async Task fn_au10001(string host, string 앱키, string 시크릿키)
//        {
//            string endpoint = "/oauth2/token";
//            string url = host + endpoint;

//            var paramsData = new
//            {
//                grant_type = "client_credentials",
//                appkey = 앱키,
//                secretkey = 시크릿키
//            };

//            var json = JsonConvert.SerializeObject(paramsData);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");

//            try
//            {
//                // 재사용 클라이언트로 POST 요청
//                var response = await 공용_클라이언트.PostAsync(url, content);
//                var responseBody = await response.Content.ReadAsStringAsync();

//                if (responseBody.StartsWith("<") || responseBody.Contains("시스템작업") || responseBody.Contains("서비스 중단"))
//                {
//                    Form1.Console_print(">>> 키움증권 서버 정기 점검이 감지되었습니다.");

//                    string 순수텍스트 = System.Text.RegularExpressions.Regex.Replace(responseBody, "<[^>]*>", "");
//                    string 점검시간안내 = "점검 일시를 불러올 수 없습니다.";

//                    int 시작점 = 순수텍스트.IndexOf("1. 중단 일시");
//                    int 끝점 = 순수텍스트.IndexOf("2. 중단 내용");

//                    if (시작점 != -1 && 끝점 != -1 && 시작점 < 끝점)
//                    {
//                        시작점 += "1. 중단 일시".Length;
//                        string 추출된시간 = 순수텍스트.Substring(시작점, 끝점 - 시작점).Trim();
//                        추출된시간 = 추출된시간.Trim('-', ' ', '\r', '\n', '\t');
//                        점검시간안내 = $"[예상 점검 시간]\n{추출된시간}";
//                    }

//                    MessageBox.Show($"현재 키움증권 서버 정기 점검 또는 서비스 중단 시간입니다.\n\n{점검시간안내}\n\n점검이 끝난 후 봇을 다시 실행해 주세요.",
//                                    "키움증권 서버 점검중",
//                                    MessageBoxButtons.OK,
//                                    MessageBoxIcon.Warning);

//                    Environment.Exit(0);
//                    return;
//                }

//                if (!response.IsSuccessStatusCode)
//                {
//                    Form1.Console_print($">>> 서버 통신 오류 (상태 코드: {(int)response.StatusCode})");
//                    Form1.Console_print($"[응답 원본] {responseBody}");
//                    return;
//                }

//                try
//                {
//                    // 💡 [최적화 핵심] 중복 파싱 제거, 필요한 JObject만 단일 파싱
//                    JObject json_Object = JObject.Parse(responseBody);

//                    if ((string)json_Object["return_code"] == "0")
//                    {
//                        Form1.Console_print("[API_LOGIN] expires_dt : " + json_Object["expires_dt"]);
//                        Form1.Console_print("[API_LOGIN] token_type : " + json_Object["token_type"]);
//                        Form1.Console_print("[API_LOGIN] token : " + json_Object["token"]);
//                        Form1.Console_print("[API_LOGIN] return_code : " + json_Object["return_code"]);
//                        Form1.Console_print("[API_LOGIN] return_msg : " + json_Object["return_msg"]);

//                        Form1.API_token = json_Object["token"].ToString();
//                        Form1.ON_LINE = true;

//                        Helper.안전한_UI_업데이트(Form1.form1, () =>
//                        {
//                            DataManagement.Drictroy_Create();
//                            DataManagement.파일삭제();

//                            Form1.form1.label_ONLINE.Text = GenieConfig.CB_신용_주문사용 ? "ON LINE (신용주문)" : "ON LINE";
//                            Form1.form1.label_ONLINE.ForeColor = System.Drawing.Color.Red;
//                        });

//                        Form1.Console_print("로그인 성공:: " + DateTime.Now.ToString("HH:mm:ss.fff"));

//                        if (GenieConfig.checkBox_Simulation)
//                        {
//                            Form1.server = "모의투자";
//                            Log.동작기록("모의투자로 접속 하였습니다.");
//                            Form1.수수료 = GenieConfig.TB_mo_commission / 100.0;
//                        }
//                        else
//                        {
//                            Form1.server = "실서버";
//                            Log.동작기록("실서버로 접속 하였습니다.");
//                            Form1.수수료 = GenieConfig.TB_sil_commission / 100.0;
//                        }

//                        GenieConfig.SaveAll_Genie_Config();

//                        Log.동작기록("조건식 불러오기 시작");
//                        Form1.매매시작 = "Loding_00_목록조회";
//                    }
//                    else
//                    {
//                        Helper.알림창_멀티("로그인 실패", json_Object["return_msg"].ToString(), 60, false);
//                        Helper.안전한_UI_업데이트(Form1.form1, () =>
//                        {
//                            Form1.form1.checkBox_key.Checked = false;
//                            Form1.form1.checkBox_key.Checked = true;
//                        });
//                    }
//                }
//                catch (JsonException)
//                {
//                    Form1.Console_print("Body: " + responseBody);
//                }
//            }
//            catch (Exception ex)
//            {
//                Form1.Console_print("요청 실패: " + ex.Message);
//            }
//        }

//        public static void logOut()
//        {
//            Form1.tr_scheduler.EnqueuePriorityRequest("FnAu10002", 요청);

//            async Task 요청()
//            {
//                try
//                {
//                    string host = "https://api.kiwoom.com";
//                    if (GenieConfig.checkBox_Simulation) host = "https://mockapi.kiwoom.com";

//                    await FnAu10002(host, 키움_앱키, 키움_시크릿키);
//                }
//                catch (Exception ex)
//                {
//                    Form1.Console_print("로그아웃 요청 실패: " + ex.Message);
//                }
//            }
//        }

//        static async Task FnAu10002(string host, string 앱키, string 시크릿키)
//        {
//            var endpoint = "/oauth2/revoke";
//            var url = host + endpoint;

//            var paramsData = new
//            {
//                appkey = 앱키,
//                secretkey = 시크릿키,
//                token = Form1.API_token
//            };

//            var json = JsonConvert.SerializeObject(paramsData);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");

//            try
//            {
//                // 여기도 재사용 클라이언트 사용
//                var response = await 공용_클라이언트.PostAsync(url, content);

//                Form1.Console_print("code : " + (int)response.StatusCode);
//                var responseBody = await response.Content.ReadAsStringAsync();
//                Form1.Console_print("body : " + responseBody);
//            }
//            catch (Exception error)
//            {
//                Form1.Console_print("요청 실패: " + error.Message);
//            }
//        }
//    }
//}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64
{
    internal class Get_token
    {
        // 1. 공용 통신 객체 (단 1개만 사용)
        private static readonly HttpClient 공용_클라이언트;

        // 2. 키움증권 인증 정보
        private static string 키움_앱키 = GenieConfig.textBox_appKey;
        private static string 키움_시크릿키 = GenieConfig.textBox_appsecret;

        // 3. 한국투자 및 LS증권 키 보관 변수 (파일에서 읽어와 저장할 공간)
        private static string 한투_실투_앱키 = "";
        private static string 한투_실투_시크릿키 = "";
        private static string 한투_모투_앱키 = "";
        private static string 한투_모투_시크릿키 = "";

        private static string LS_실투_앱키 = "";
        private static string LS_실투_시크릿키 = "";
        private static string LS_모투_앱키 = "";
        private static string LS_모투_시크릿키 = "";

        // 실제 통신에 사용될 최종 키와 발급된 토큰 저장소
        private static string 한투_앱키 = "";
        private static string 한투_시크릿키 = "";
      

        private static string LS_앱키 = "";
        private static string LS_시크릿키 = "";


        static Get_token()
        {
            공용_클라이언트 = new HttpClient();
            공용_클라이언트.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // =========================================================================
        // [수정] 파일이 있으면 true, 없으면 false를 반환하는 파서
        // =========================================================================
        private static bool 앱키_파일_로딩()
        {
            string 파일경로 = Path.Combine(Application.StartupPath, "한투_LS_앱키.txt");

            if (!File.Exists(파일경로))
            {
                Form1.Console_print("[-] 한투_LS_앱키.txt 파일이 없습니다. (키움증권만 단독 로그인합니다)");
                return false; // 파일이 없으므로 false 반환
            }

            try
            {
                string[] 줄데이터 = File.ReadAllLines(파일경로);
                string 현재구간 = "";

                foreach (string 원본줄 in 줄데이터)
                {
                    string 줄 = 원본줄.Trim();

                    if (string.IsNullOrEmpty(줄)) continue;

                    // 1. 구간 구분 인식
                    if (줄 == "한투실투") { 현재구간 = "한투실투"; continue; }
                    if (줄 == "한투모투") { 현재구간 = "한투모투"; continue; }
                    if (줄 == "LS증권실투자") { 현재구간 = "LS실투"; continue; }
                    if (줄 == "LS증권모투") { 현재구간 = "LS모투"; continue; }

                    // 2. 불필요한 라벨(설명) 줄 무시
                    string 대문자_줄 = 줄.ToUpper();
                    if (대문자_줄.Contains("APP") || 대문자_줄.Contains("KEY") ||
                        대문자_줄.Contains("KET") || 대문자_줄.Contains("SECRET") ||
                        대문자_줄.Contains("앱") || 대문자_줄.Contains("시크릿"))
                    {
                        continue;
                    }

                    // 3. 실제 키 데이터 할당
                    switch (현재구간)
                    {
                        case "한투실투":
                            if (string.IsNullOrEmpty(한투_실투_앱키)) 한투_실투_앱키 = 줄;
                            else if (string.IsNullOrEmpty(한투_실투_시크릿키)) 한투_실투_시크릿키 = 줄;
                            break;

                        case "한투모투":
                            if (string.IsNullOrEmpty(한투_모투_앱키)) 한투_모투_앱키 = 줄;
                            else if (string.IsNullOrEmpty(한투_모투_시크릿키)) 한투_모투_시크릿키 = 줄;
                            break;

                        case "LS실투":
                            if (string.IsNullOrEmpty(LS_실투_앱키)) LS_실투_앱키 = 줄;
                            else if (string.IsNullOrEmpty(LS_실투_시크릿키)) LS_실투_시크릿키 = 줄;
                            break;

                        case "LS모투":
                            if (string.IsNullOrEmpty(LS_모투_앱키)) LS_모투_앱키 = 줄;
                            else if (string.IsNullOrEmpty(LS_모투_시크릿키)) LS_모투_시크릿키 = 줄;
                            break;
                    }
                }

                Form1.Console_print("[+] 외부 API 키 파일(한투_LS_앱키.txt) 로딩 완료");
                return true; // 로딩 성공
            }
            catch (Exception ex)
            {
                Form1.Console_print("[-] 앱키 파일 파싱 실패: " + ex.Message);
                return false;
            }
        }

        // =========================================================================
        // 통합 로그인 트리거
        // =========================================================================
        public static void logIn()
        {
            Form1.tr_scheduler.EnqueuePriorityRequest("통합로그인", 요청);

            async Task 요청()
            {
                try
                {
                    // 1. '내아이디' 조건 확인 후 텍스트 파일이 존재하는지 2차 확인
                    if (Form1.내아이디)
                    {
                        // 파일이 존재하고 정상적으로 읽혔을 때만 내부에 진입
                        if (앱키_파일_로딩())
                        {
                            Form1.Console_print(">> [VIP 모드] 타 증권사 추가 인증을 시작합니다.");

                            if (GenieConfig.checkBox_Simulation)
                            {
                                한투_앱키 = 한투_모투_앱키;
                                한투_시크릿키 = 한투_모투_시크릿키;
                                LS_앱키 = LS_모투_앱키;
                                LS_시크릿키 = LS_모투_시크릿키;
                            }
                            else
                            {
                                한투_앱키 = 한투_실투_앱키;
                                한투_시크릿키 = 한투_실투_시크릿키;
                                LS_앱키 = LS_실투_앱키;
                                LS_시크릿키 = LS_실투_시크릿키;
                            }

                            await Task.Delay(500);

                            string kis_host = GenieConfig.checkBox_Simulation ? "https://openapivts.koreainvestment.com:29443" : "https://openapi.koreainvestment.com:9443";
                            await KIS_로그인요청(kis_host, 한투_앱키, 한투_시크릿키);

                            await Task.Delay(500);

                            string ls_host = "https://openapi.ls-sec.co.kr:8080";
                            await LS_로그인요청(ls_host, LS_앱키, LS_시크릿키);

                            Form1.VIP_mode = true;

                            // =========================================================================
                            // [추가] 내아이디 확인 및 앱키 로딩, 로그인까지 완벽히 성공했을 때만 스케줄러 작동 시작
                            // =========================================================================
                            if (Form1.한투_스케줄러 == null)
                            {
                                Form1.한투_스케줄러 = new 한국투자_TR_스케줄러();
                                Form1.한투_주문스케줄러 = new 한국투자_주문_스케줄러();
                                Form1.Console_print(">> [스케줄러] 한국투자증권 유량 제어, 주문 전용 스케줄러가 시작되었습니다.");
                            }

                            if (Form1.ls_스케줄러 == null)
                            {
                                Form1.ls_스케줄러 = new LS_TR_스케줄러();
                                Form1.ls_주문스케줄러 = new LS_주문_스케줄러();
                                Form1.Console_print(">> [스케줄러] LS증권 유량 제어,주문 전용 스케줄러가 시작되었습니다.");
                            }
                            // =========================================================================
                        }
                    }

                    // 2. 키움증권 기본 로그인 (무조건 실행)
                    string kiwoom_host = GenieConfig.checkBox_Simulation ? "https://mockapi.kiwoom.com" : "https://api.kiwoom.com";
                    await fn_au10001(kiwoom_host, 키움_앱키, 키움_시크릿키);
                }
                catch (Exception ex)
                {
                    Form1.Console_print("[-] 통합 로그인 요청 실패: " + ex.Message);
                }
            }
        }

        // =========================================================================
        // 키움증권 로그인 함수 
        // =========================================================================
        static async Task fn_au10001(string host, string 앱키, string 시크릿키)
        {
            string endpoint = "/oauth2/token";
            string url = host + endpoint;

            var paramsData = new
            {
                grant_type = "client_credentials",
                appkey = 앱키,
                secretkey = 시크릿키
            };

            var json = JsonConvert.SerializeObject(paramsData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await 공용_클라이언트.PostAsync(url, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (responseBody.StartsWith("<") || responseBody.Contains("시스템작업") || responseBody.Contains("서비스 중단"))
                {
                    Form1.Console_print("[-] 키움증권 서버 정기 점검이 감지되었습니다.");

                    string 순수텍스트 = System.Text.RegularExpressions.Regex.Replace(responseBody, "<[^>]*>", "");
                    string 점검시간안내 = "점검 일시를 불러올 수 없습니다.";

                    int 시작점 = 순수텍스트.IndexOf("1. 중단 일시");
                    int 끝점 = 순수텍스트.IndexOf("2. 중단 내용");

                    if (시작점 != -1 && 끝점 != -1 && 시작점 < 끝점)
                    {
                        시작점 += "1. 중단 일시".Length;
                        string 추출된시간 = 순수텍스트.Substring(시작점, 끝점 - 시작점).Trim();
                        추출된시간 = 추출된시간.Trim('-', ' ', '\r', '\n', '\t');
                        점검시간안내 = $"[예상 점검 시간]\n{추출된시간}";
                    }

                    MessageBox.Show($"현재 키움증권 서버 정기 점검 또는 서비스 중단 시간입니다.\n\n{점검시간안내}\n\n점검이 끝난 후 봇을 다시 실행해 주세요.",
                                    "[경고] 키움증권 서버 점검중",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    Environment.Exit(0);
                    return;
                }

                if (!response.IsSuccessStatusCode)
                {
                    Form1.Console_print($"[-] 서버 통신 오류 (상태 코드: {(int)response.StatusCode})");
                    Form1.Console_print($"[응답 원본] {responseBody}");
                    return;
                }

                try
                {
                    JObject json_Object = JObject.Parse(responseBody);

                    if ((string)json_Object["return_code"] == "0")
                    {
                        Form1.Console_print("[API_LOGIN] expires_dt : " + json_Object["expires_dt"]);
                        Form1.Console_print("[API_LOGIN] token_type : " + json_Object["token_type"]);
                        Form1.Console_print("[API_LOGIN] token : " + json_Object["token"]);
                        Form1.Console_print("[API_LOGIN] return_code : " + json_Object["return_code"]);
                        Form1.Console_print("[API_LOGIN] return_msg : " + json_Object["return_msg"]);

                        Form1.API_token = json_Object["token"].ToString();
                        Form1.ON_LINE = true;

                        Helper.안전한_UI_업데이트(Form1.form1, () =>
                        {
                            DataManagement.Drictroy_Create();
                            DataManagement.파일삭제();

                            Form1.form1.label_ONLINE.Text = GenieConfig.CB_신용_주문사용 ? "ON LINE (신용주문)" : "ON LINE";
                            Form1.form1.label_ONLINE.ForeColor = System.Drawing.Color.Red;
                        });

                        Form1.Console_print("[+] 키움증권 로그인 성공:: " + DateTime.Now.ToString("HH:mm:ss.fff"));

                        if (GenieConfig.checkBox_Simulation)
                        {
                            Form1.server = "모의투자";
                            Log.동작기록("모의투자로 접속 하였습니다.");
                            Form1.수수료 = GenieConfig.TB_mo_commission / 100.0;
                        }
                        else
                        {
                            Form1.server = "실서버";
                            Log.동작기록("실서버로 접속 하였습니다.");
                            Form1.수수료 = GenieConfig.TB_sil_commission / 100.0;
                        }

                        GenieConfig.SaveAll_Genie_Config();

                        Log.동작기록("조건식 불러오기 시작");
                        Form1.매매시작 = "Loding_00_목록조회";
                    }
                    else
                    {
                        Helper.알림창_멀티("로그인 실패", json_Object["return_msg"].ToString(), 60, false);
                        Helper.안전한_UI_업데이트(Form1.form1, () =>
                        {
                            Form1.form1.checkBox_key.Checked = false;
                            Form1.form1.checkBox_key.Checked = true;
                        });
                    }
                }
                catch (JsonException)
                {
                    Form1.Console_print("Body: " + responseBody);
                }
            }
            catch (Exception ex)
            {
                Form1.Console_print("요청 실패: " + ex.Message);
            }
        }

        // =========================================================================
        // 한국투자증권 로그인 (REST 토큰 + 웹소켓 접속키 연속 발급)
        // =========================================================================
        static async Task KIS_로그인요청(string host, string 앱키, string 시크릿키)
        {
            if (string.IsNullOrEmpty(앱키) || string.IsNullOrEmpty(시크릿키))
            {
                Form1.Console_print("[-] 한국투자증권 키가 비어있습니다.");
                return;
            }

            앱키 = 앱키.Trim();
            시크릿키 = 시크릿키.Trim();

            try
            {
                string rest_url = host + "/oauth2/tokenP";
                var rest_params = new
                {
                    grant_type = "client_credentials",
                    appkey = 앱키,
                    appsecret = 시크릿키
                };

                var rest_content = new StringContent(JsonConvert.SerializeObject(rest_params), Encoding.UTF8, "application/json");
                var rest_response = await 공용_클라이언트.PostAsync(rest_url, rest_content);
                var rest_body = await rest_response.Content.ReadAsStringAsync();

                if (rest_response.IsSuccessStatusCode)
                {
                    JObject json_Object = JObject.Parse(rest_body);
                    Form1.한투_API_token = json_Object["access_token"].ToString();
                    Form1.Console_print("[+] 한국투자증권 REST API 토큰 발급 성공");
                }
                else
                {
                    Form1.Console_print($"[-] 한투 REST 인증 실패: {(int)rest_response.StatusCode}");
                    Form1.Console_print($"[상세 이유]: {rest_body}");
                    return;
                }

                await Task.Delay(300);

                string ws_url = host + "/oauth2/Approval";
                var ws_params = new
                {
                    grant_type = "client_credentials",
                    appkey = 앱키,
                    secretkey = 시크릿키
                };

                var ws_content = new StringContent(JsonConvert.SerializeObject(ws_params), Encoding.UTF8, "application/json");
                var ws_response = await 공용_클라이언트.PostAsync(ws_url, ws_content);
                var ws_body = await ws_response.Content.ReadAsStringAsync();

                if (ws_response.IsSuccessStatusCode)
                {
                    JObject json_Object = JObject.Parse(ws_body);
                    Form1.한투_WS_approval_key = json_Object["approval_key"].ToString();
                    Form1.Console_print("[+] 한국투자증권 웹소켓 접속키(Approval Key) 발급 성공");
                }
                else
                {
                    Form1.Console_print($"[-] 한투 웹소켓 접속키 발급 실패: {(int)ws_response.StatusCode}");
                    Form1.Console_print($"[상세 이유]: {ws_body}");
                }
            }
            catch (Exception ex)
            {
                Form1.Console_print("[-] 한국투자증권 통신 오류: " + ex.Message);
            }
        }

        // =========================================================================
        // LS증권 로그인 함수 
        // =========================================================================
        static async Task LS_로그인요청(string host, string 앱키, string 시크릿키)
        {
            if (string.IsNullOrEmpty(앱키) || string.IsNullOrEmpty(시크릿키))
            {
                Form1.Console_print("[-] LS증권 키가 비어있습니다.");
                return;
            }

            앱키 = 앱키.Trim();
            시크릿키 = 시크릿키.Trim();

            string endpoint = "/oauth2/token";
            string url = host + endpoint;

            var form데이터_리스트 = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "appkey", 앱키 },
                { "appsecretkey", 시크릿키 },
                { "scope", "oob" }
            };

            var content = new FormUrlEncodedContent(form데이터_리스트);

            try
            {
                var response = await 공용_클라이언트.PostAsync(url, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    JObject json_Object = JObject.Parse(responseBody);
                    Form1.LS_API_token = json_Object["access_token"].ToString();
                    Form1.Console_print("[+] LS증권 토큰 발급 성공");
                }
                else
                {
                    Form1.Console_print($"[-] LS증권 인증 실패: {(int)response.StatusCode}");
                    Form1.Console_print($"[LS 응답]: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                Form1.Console_print("[-] LS증권 통신 오류: " + ex.Message);
            }
        }

        // =========================================================================
        // 로그아웃
        // =========================================================================
        public static void logOut()
        {
            Form1.tr_scheduler.EnqueuePriorityRequest("FnAu10002", 요청);

            async Task 요청()
            {
                try
                {
                    string host = GenieConfig.checkBox_Simulation ? "https://mockapi.kiwoom.com" : "https://api.kiwoom.com";
                    await FnAu10002(host, 키움_앱키, 키움_시크릿키);
                }
                catch (Exception ex)
                {
                    Form1.Console_print("[-] 로그아웃 요청 실패: " + ex.Message);
                }
            }
        }

        static async Task FnAu10002(string host, string 앱키, string 시크릿키)
        {
            var endpoint = "/oauth2/revoke";
            var url = host + endpoint;

            var paramsData = new
            {
                appkey = 앱키,
                secretkey = 시크릿키,
                token = Form1.API_token
            };

            var json = JsonConvert.SerializeObject(paramsData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await 공용_클라이언트.PostAsync(url, content);

                Form1.Console_print("code : " + (int)response.StatusCode);
                var responseBody = await response.Content.ReadAsStringAsync();
                Form1.Console_print("body : " + responseBody);
            }
            catch (Exception error)
            {
                Form1.Console_print("[-] 요청 실패: " + error.Message);
            }
        }
    }
}