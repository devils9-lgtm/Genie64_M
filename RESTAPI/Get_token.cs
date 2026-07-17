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
using 지니64.VIP_mode.로그인;

namespace 지니64
{
    internal class Get_token
    {
        // 1. 공용 통신 객체 (단 1개만 사용)
        public static readonly HttpClient 공용_클라이언트;

        // 2. 키움증권 인증 정보
        private static string 키움_앱키 = GenieConfig.textBox_appKey;
        private static string 키움_시크릿키 = GenieConfig.textBox_appsecret;

      

        static Get_token()
        {
            공용_클라이언트 = new HttpClient();
            공용_클라이언트.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
                    await Vip_login.로그인();

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
                        Form1.Console_print("[키움 API_LOGIN] expires_dt : " + json_Object["expires_dt"]);
                        Form1.Console_print("[키움 API_LOGIN] token_type : " + json_Object["token_type"]);
                        Form1.Console_print("[키움 API_LOGIN] token : " + json_Object["token"]);
                        Form1.Console_print("[키움 API_LOGIN] return_code : " + json_Object["return_code"]);
                        Form1.Console_print("[키움 API_LOGIN] return_msg : " + json_Object["return_msg"]);

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