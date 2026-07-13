using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 지니64;

namespace 지니64.VIP_mode.로그인
{
    internal class Vip_login
    {
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


        public static async Task 로그인()
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

                    지니64.VIP_mode.모드변경.모드변경.VIP증권사컬럼추가(Form1.form1.JanGo_dataGridView);
                }
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
                var rest_response = await Get_token.공용_클라이언트.PostAsync(rest_url, rest_content);
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
                var ws_response = await Get_token.공용_클라이언트.PostAsync(ws_url, ws_content);
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
                var response = await Get_token.공용_클라이언트.PostAsync(url, content);
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

    }
}
