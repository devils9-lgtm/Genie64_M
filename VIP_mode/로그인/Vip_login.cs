using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64.VIP_mode.로그인
{
    internal class Vip_login
    {
        // 3. 한국투자 및 LS증권 키 보관 변수 (파일에서 읽어와 저장할 공간)
        // [수정됨] 실전투자(실투)와 모의투자(모투) 데이터를 완전히 분리하여 전역 변수로 선언합니다.
        public static string 한투_실투_앱키 = "";
        public static string 한투_실투_시크릿키 = "";
        public static string 한투_실투_계좌번호 = "";

        public static string 한투_모투_앱키 = "";
        public static string 한투_모투_시크릿키 = "";
        public static string 한투_모투_계좌번호 = "";

        public static string LS_실투_앱키 = "";
        public static string LS_실투_시크릿키 = "";

        public static string LS_모투_앱키 = "";
        public static string LS_모투_시크릿키 = "";

        public class TokenInfo
        {
            public string AccessToken { get; set; } = string.Empty;
            public string ApprovalKey { get; set; } = string.Empty;
            public DateTime ExpireTime { get; set; } = DateTime.MinValue;
        }

        public class KisTokenCombinedCache
        {
            public TokenInfo Simulation { get; set; } = new TokenInfo();
            public TokenInfo Real { get; set; } = new TokenInfo();
        }

        // [메모리 최적화] HttpClient 싱글톤 재사용 (소켓 및 메모리 고갈 방지)
        private static readonly HttpClient 클라이언트 = new HttpClient();

        // [메모리 최적화] 인코딩 객체 static 캐싱 (불필요한 인코딩 인스턴스화 방지)
        private static readonly Encoding _utf8WithoutBom = new UTF8Encoding(false);


        private static bool 앱키_파일_로딩()
        {
            string 파일경로 = Path.Combine(Application.StartupPath, "한투_LS_앱키.txt");
            if (!File.Exists(파일경로))
            {
                Form1.Console_print("[-] 한투_LS_앱키.txt 파일이 없습니다.");
                return false;
            }

            try
            {
                string 현재구간 = "";

                // 무시할 라벨 리스트 (반복문 밖에서 한 번만 선언하여 속도 향상)
                string[] 정확히_무시할_라벨들 = { "APP KEY", "APP SECRET", "계좌번호", "앱키", "시크릿키" };

                // [최적화] File.ReadAllLines 대신 File.ReadLines를 사용하여 
                // 텍스트를 한 줄씩만 메모리에 올려 처리합니다. (저사양 PC 메모리 부하 방지)
                foreach (string 원본줄 in File.ReadLines(파일경로))
                {
                    string 줄 = 원본줄.Trim();
                    if (string.IsNullOrEmpty(줄)) continue;

                    // 1. 구간 구분 인식
                    if (줄 == "한투실투") { 현재구간 = "한투실투"; continue; }
                    if (줄 == "한투모투") { 현재구간 = "한투모투"; continue; }
                    if (줄 == "LS증권실투자") { 현재구간 = "LS실투"; continue; }
                    if (줄 == "LS증권모투") { 현재구간 = "LS모투"; continue; }

                    // 2. 라벨(설명) 줄 무시: StartsWith 활용하여 오작동 완벽 차단
                    bool 라벨여부 = false;
                    string 비교용_줄 = 줄.Replace(" ", "").ToUpper(); // 공백 제거 및 대문자 통일

                    foreach (string 라벨 in 정확히_무시할_라벨들)
                    {
                        string 비교용_라벨 = 라벨.Replace(" ", "").ToUpper();

                        // 줄이 해당 라벨로 '시작'하거나 정확히 일치할 때만 무시함
                        if (비교용_줄.StartsWith(비교용_라벨))
                        {
                            라벨여부 = true;
                            break;
                        }
                    }
                    if (라벨여부) continue;

                    // 3. 데이터 할당 (구분된 전역 변수에 각각 분리하여 직접 저장)
                    switch (현재구간)
                    {
                        case "한투실투":
                            if (string.IsNullOrEmpty(한투_실투_앱키)) 한투_실투_앱키 = 줄;
                            else if (string.IsNullOrEmpty(한투_실투_시크릿키)) 한투_실투_시크릿키 = 줄;
                            else if (string.IsNullOrEmpty(한투_실투_계좌번호)) 한투_실투_계좌번호 = 줄;
                            break;
                        case "한투모투":
                            if (string.IsNullOrEmpty(한투_모투_앱키)) 한투_모투_앱키 = 줄;
                            else if (string.IsNullOrEmpty(한투_모투_시크릿키)) 한투_모투_시크릿키 = 줄;
                            else if (string.IsNullOrEmpty(한투_모투_계좌번호)) 한투_모투_계좌번호 = 줄;
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

                // ====================================================================
                // 파싱이 끝난 직후, 각 구간별로 잘 읽어왔는지 콘솔에 분리해서 출력 (확인용)
                // ====================================================================
                Form1.Console_print("\n========================================");
                Form1.Console_print("[텍스트 파일 데이터 로드 결과 (분리 완료)]");
                Form1.Console_print($"[한투 실투] 앱키: {한투_실투_앱키}");
                Form1.Console_print($"[한투 실투] 계좌: {한투_실투_계좌번호}");
                Form1.Console_print("- - - - - - - - - - - - - - - - - - - - ");
                Form1.Console_print($"[한투 모투] 앱키: {한투_모투_앱키}");
                Form1.Console_print($"[한투 모투] 계좌: {한투_모투_계좌번호}");
                Form1.Console_print("- - - - - - - - - - - - - - - - - - - - ");
                Form1.Console_print($"[LS 실투] 앱키: {LS_실투_앱키}");
                Form1.Console_print($"[LS 모투] 앱키: {LS_모투_앱키}");
                Form1.Console_print("========================================\n");

                return true;
            }
            catch (Exception 예외)
            {
                Form1.Console_print("[-] 앱키 파일 파싱 실패: " + 예외.Message);
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

                    // API 요청 시 사용할 변수를 상황에 맞게 쏙 빼서 쓰시면 됩니다.
                    bool 모의투자여부 = GenieConfig.checkBox_Simulation;

                    Form1.한투_앱키 = 모의투자여부 ? Vip_login.한투_모투_앱키 : Vip_login.한투_실투_앱키;
                    Form1.한투_시크릿키 = 모의투자여부 ? Vip_login.한투_모투_시크릿키 : Vip_login.한투_실투_시크릿키;

                    string 종합계좌번호 = 모의투자여부 ? Vip_login.한투_모투_계좌번호 : Vip_login.한투_실투_계좌번호;

                    // 2. [최적화] 안전한 자르기 연산으로 메모리 낭비 방지 (예외 처리 포함)
                    Form1.한투_CANO = 종합계좌번호.Length >= 8 ? 종합계좌번호.Substring(0, 8) : 종합계좌번호;
                    Form1.한투_ACNT_PRDT_CD = 종합계좌번호.Length == 10 ? 종합계좌번호.Substring(8, 2) : "01";

                    Form1.LS_앱키 = 모의투자여부 ? Vip_login.LS_모투_앱키 : Vip_login.LS_실투_앱키;
                    Form1.LS_시크릿키 = 모의투자여부 ? Vip_login.LS_모투_시크릿키 : Vip_login.LS_실투_시크릿키;

                    await Task.Delay(500);

                    string kis_host = 모의투자여부 ? "https://openapivts.koreainvestment.com:29443" : "https://openapi.koreainvestment.com:9443";
                    // [수정] 모의투자 여부를 함께 전달
                    bool isKisSuccess = await KIS_로그인요청(kis_host, Form1.한투_앱키, Form1.한투_시크릿키, 모의투자여부);
                    if (!isKisSuccess) return;

                    await Task.Delay(500);

                    string ls_host = "https://openapi.ls-sec.co.kr:8080";
                    // [수정] 모의투자 여부를 함께 전달
                    bool isLsSuccess = await LS_로그인요청(ls_host, Form1.LS_앱키, Form1.LS_시크릿키, 모의투자여부);
                    if (!isLsSuccess) return;

                    Form1.VIP_mode = true;

                    // =========================================================================
                    // [추가] 내아이디 확인 및 앱키 로딩, 로그인까지 완벽히 성공했을 때만 스케줄러 작동 시작
                    // =========================================================================
                    if (Form1.한투_스케줄러 == null)
                    {
                        Form1.한투_스케줄러 = new 한투_TR스케줄러();
                        Form1.한투_주문스케줄러 = new 한투_주문스케줄러();
                        Form1.Console_print(">> [스케줄러] 한국투자증권 유량 제어, 주문 전용 스케줄러가 시작되었습니다.");
                    }

                    if (Form1.LS_스케줄러 == null)
                    {
                        Form1.LS_스케줄러 = new LS_TR스케줄러();
                        Form1.LS_주문스케줄러 = new LS_주문스케줄러();
                        Form1.Console_print(">> [스케줄러] LS증권 유량 제어,주문 전용 스케줄러가 시작되었습니다.");
                    }

                    Form1.Console_print($">> [로그인 서버] 모의투자여부 {모의투자여부}.");
                    Form1.Console_print($">> [한투_앱키] {Form1.한투_앱키}");
                    Form1.Console_print($">> [한투_시크릿키] {Form1.한투_시크릿키}");
                    // =========================================================================

                    지니64.VIP_mode.모드변경.모드변경.VIP증권사컬럼추가(Form1.form1.JanGo_dataGridView);
                }
            }
        }


        // =========================================================================
        // [로그인] REST 토큰 + 웹소켓 접속키 연속 발급 (장 마감 시간 기반 지능형 재발급 적용)
        // =========================================================================
        public static async Task<bool> KIS_로그인요청(string host, string 앱키, string 시크릿키, bool isSimulation)
        {
            string modeText = isSimulation ? "[모의투자]" : "[실투자]";
            Form1.Console_print($">> [로그인 시작] {modeText} 한국투자증권 인증 요청을 시작합니다.");

            if (string.IsNullOrEmpty(앱키) || string.IsNullOrEmpty(시크릿키))
            {
                Form1.Console_print($"[-] 한국투자증권 {modeText} 키가 비어있습니다.");
                MessageBox.Show($"한국투자증권 {modeText} 앱키 또는 시크릿키가 비어있습니다.\n프로그램을 종료합니다.", "인증 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return false;
            }

            앱키 = 앱키.Trim();
            시크릿키 = 시크릿키.Trim();

            // 저장 폴더 및 통합 파일 경로 설정
            string tokenFolder = Path.Combine(Application.StartupPath, "토큰발급_한국투자증권");
            string tokenFilePath = Path.Combine(tokenFolder, "token_cache.json");
            KisTokenCombinedCache combinedCache = null;

            // =========================================================================
            // [1] 로컬 토큰 파일 비동기 읽기 및 지능형 유효기간(장 마감 시간) 검사
            // =========================================================================
            if (File.Exists(tokenFilePath))
            {
                try
                {
                    Form1.Console_print($">> [로컬 탐색] 토큰 파일 로드를 시도합니다. 경로: {tokenFilePath}");

                    // 비동기 스트림을 통해 UI 멈춤 방지 및 메모리 사용량 최소화
                    using (var fs = new FileStream(tokenFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
                    using (var reader = new StreamReader(fs, _utf8WithoutBom))
                    {
                        string jsonString = await reader.ReadToEndAsync();
                        combinedCache = JsonConvert.DeserializeObject<KisTokenCombinedCache>(jsonString);
                    }

                    if (combinedCache != null)
                    {
                        TokenInfo currentToken = isSimulation ? combinedCache.Simulation : combinedCache.Real;

                        if (currentToken != null && !string.IsNullOrEmpty(currentToken.AccessToken))
                        {
                            // -------------------------------------------------------------
                            // [핵심 로직] 장중(08:00 ~ 20:00) 토큰 만료를 방지하는 타겟 타임 설정
                            // -------------------------------------------------------------
                            // 목표 마감 시간: 애프터마켓 종료 시간(20:00) + 30분 여유 = 당일 20:30
                            DateTime targetEndTime = DateTime.Today.AddHours(20).AddMinutes(30);

                            // 만약 현재 시간이 당일 20시 30분을 지났다면 (즉, 밤에 프로그램을 켰다면)
                            // 내일 장 마감(익일 20:30)까지 버틸 수 있어야 하므로 타겟 타임을 하루 증가시킴
                            if (DateTime.Now > targetEndTime)
                            {
                                targetEndTime = targetEndTime.AddDays(1);
                            }

                            // 토큰 만료 시간이 목표 마감 시간(targetEndTime)보다 넉넉히 남아있는지 확인
                            if (currentToken.ExpireTime > targetEndTime)
                            {
                                Form1.한투_API_token = currentToken.AccessToken;
                                Form1.한투_WS_approval_key = currentToken.ApprovalKey;

                                Form1.Console_print($"[+] 로컬 파일에서 유효한 {modeText} 토큰을 성공적으로 불러왔습니다. (만료예정: {currentToken.ExpireTime})");
                                Form1.Console_print($">> [로그인 완료] 애프터마켓 마감 시점({targetEndTime:MM-dd HH:mm})까지 토큰이 안전하게 유지됩니다. 신규 발급을 생략합니다.");
                                return true;
                            }
                            else
                            {
                                Form1.Console_print($"[*] 기존 토큰이 장 마감 시간({targetEndTime:MM-dd HH:mm}) 이전에 만료될 예정입니다. (현재 토큰 만료시간: {currentToken.ExpireTime})");
                                Form1.Console_print($"[*] 장중 통신 끊김을 방지하기 위해 새로운 토큰을 발급합니다.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Form1.Console_print($"[-] 로컬 토큰 파일 읽기 실패 (새로 발급 진행): {ex.Message}");
                }
            }
            else
            {
                Form1.Console_print($"[*] 로컬 토큰 캐시 파일이 존재하지 않습니다. 새로 발급을 진행합니다.");
            }

            if (combinedCache == null)
            {
                combinedCache = new KisTokenCombinedCache();
            }

            try
            {
                DateTime tokenExpireTime = DateTime.Now.AddHours(24);

                // =========================================================================
                // [2] REST API 토큰 발급 (/oauth2/tokenP)
                // =========================================================================
                Form1.Console_print($">> [단계 1] {modeText} REST API 토큰 발급 시도 중...");
                string rest_url = host + "/oauth2/tokenP";

                string rest_json = JsonConvert.SerializeObject(new
                {
                    grant_type = "client_credentials",
                    appkey = 앱키,
                    appsecret = 시크릿키
                });

                Form1.Console_print($">> [한투 통신] REST 서버({rest_url})에 데이터 전송 중...");

                using (var rest_content = new StringContent(rest_json, _utf8WithoutBom, "application/json"))
                using (var rest_response = await 클라이언트.PostAsync(rest_url, rest_content))
                {
                    Form1.Console_print($">> [한투 통신] rest_response 상태: {rest_response.StatusCode}");

                    if (rest_response.IsSuccessStatusCode)
                    {
                        // [최적화] 대용량 문자열 할당 방지 및 스트림 고속 파싱 (가비지 컬렉터 부하 최소화)
                        using (Stream restStream = await rest_response.Content.ReadAsStreamAsync())
                        using (JsonDocument doc = await JsonDocument.ParseAsync(restStream))
                        {
                            var root = doc.RootElement;
                            Form1.한투_API_token = root.TryGetProperty("access_token", out var tokenProp) ? tokenProp.GetString() : string.Empty;

                            if (root.TryGetProperty("access_token_token_expired", out var expireProp))
                            {
                                string expireStr = expireProp.GetString();
                                DateTime.TryParseExact(expireStr, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out tokenExpireTime);
                            }
                        }

                        Form1.Console_print($"[+] 한투_API_token 발급 완료");
                        Form1.Console_print($"[+] 한국투자증권 {modeText} REST API 토큰 발급 성공");
                    }
                    else
                    {
                        string errorBody = await rest_response.Content.ReadAsStringAsync();
                        Form1.Console_print($"[-] 한투 {modeText} REST 인증 실패: {(int)rest_response.StatusCode}");
                        Form1.Console_print($"[-] 에러 내용: {errorBody}");
                        MessageBox.Show($"한국투자증권 {modeText} REST API 인증에 실패했습니다.\n상태 코드: {(int)rest_response.StatusCode}\n\n프로그램을 종료합니다.", "인증 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(0);
                        return false;
                    }
                }

                Form1.Console_print(">> [대기] 웹소켓 키 발급 전 안정화를 위해 300ms 대기...");
                await Task.Delay(300);

                // =========================================================================
                // [3] 웹소켓 접속키 발급 (/oauth2/Approval)
                // =========================================================================
                Form1.Console_print($">> [단계 2] {modeText} 웹소켓 접속키(Approval Key) 발급 시도 중...");
                string ws_url = host + "/oauth2/Approval";

                string ws_json = JsonConvert.SerializeObject(new
                {
                    grant_type = "client_credentials",
                    appkey = 앱키,
                    secretkey = 시크릿키
                });

                Form1.Console_print($">> [통신] 웹소켓 서버({ws_url})에 데이터 전송 중...");

                using (var ws_content = new StringContent(ws_json, _utf8WithoutBom, "application/json"))
                using (var ws_response = await 클라이언트.PostAsync(ws_url, ws_content))
                {
                    if (ws_response.IsSuccessStatusCode)
                    {
                        // [최적화] 대용량 문자열 할당 방지 및 스트림 고속 파싱
                        using (Stream wsStream = await ws_response.Content.ReadAsStreamAsync())
                        using (JsonDocument doc = await JsonDocument.ParseAsync(wsStream))
                        {
                            var root = doc.RootElement;
                            Form1.한투_WS_approval_key = root.TryGetProperty("approval_key", out var appKeyProp) ? appKeyProp.GetString() : string.Empty;
                        }

                        Form1.Console_print($"[+] 한국투자증권 {modeText} 웹소켓 접속키(Approval Key) 발급 성공");
                    }
                    else
                    {
                        string errorBody = await ws_response.Content.ReadAsStringAsync();
                        Form1.Console_print($"[-] 한투 {modeText} 웹소켓 접속키 발급 실패: {(int)ws_response.StatusCode}");
                        Form1.Console_print($"[-] 에러 내용: {errorBody}");
                        MessageBox.Show($"한국투자증권 {modeText} 웹소켓 접속키 발급에 실패했습니다.\n상태 코드: {(int)ws_response.StatusCode}\n\n프로그램을 종료합니다.", "인증 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(0);
                        return false;
                    }
                }

                // =========================================================================
                // [4] 토큰 로컬 캐시 통합 파일 저장 (.NET Framework 호환 & 디스크 I/O 최적화)
                // =========================================================================
                try
                {
                    if (!Directory.Exists(tokenFolder))
                    {
                        Directory.CreateDirectory(tokenFolder);
                    }

                    TokenInfo newInfo = new TokenInfo
                    {
                        AccessToken = Form1.한투_API_token,
                        ApprovalKey = Form1.한투_WS_approval_key,
                        ExpireTime = tokenExpireTime
                    };

                    if (isSimulation) combinedCache.Simulation = newInfo;
                    else combinedCache.Real = newInfo;

                    // [최적화] Formatting.None을 통해 공백 문자를 제거하고 디스크 I/O 크기 극소화
                    string outputJson = JsonConvert.SerializeObject(combinedCache, Formatting.None);

                    using (var fs = new FileStream(tokenFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                    using (var writer = new StreamWriter(fs, _utf8WithoutBom))
                    {
                        await writer.WriteAsync(outputJson);
                    }

                    Form1.Console_print($"[+] {modeText} 새 토큰을 통합 파일에 기록 완료했습니다. (만료예정: {tokenExpireTime})");
                }
                catch (Exception ex)
                {
                    Form1.Console_print($"[-] 토큰 파일 기록 중 오류 발생: {ex.Message}");
                }

                Form1.Console_print($">> [로그인 완료] 한국투자증권 {modeText} 인증 프로세스가 정상 종료되었습니다.");
                return true;
            }
            catch (Exception ex)
            {
                Form1.Console_print($"[-] 한국투자증권 {modeText} 통신 오류 발생: " + ex.Message);
                MessageBox.Show($"한국투자증권 {modeText} 서버 통신 중 오류가 발생했습니다.\n{ex.Message}\n\n프로그램을 종료합니다.", "통신 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return false;
            }
        }

        // =========================================================================
        // LS증권 로그인 함수 
        // =========================================================================
        // [수정] isSimulation 매개변수 추가
        static async Task<bool> LS_로그인요청(string host, string 앱키, string 시크릿키, bool isSimulation)
        {
            string modeText = isSimulation ? "[모의투자]" : "[실투자]";

            if (string.IsNullOrEmpty(앱키) || string.IsNullOrEmpty(시크릿키))
            {
                Form1.Console_print($"[-] LS증권 {modeText} 키가 비어있습니다.");
                MessageBox.Show($"LS증권 {modeText} 앱키 또는 시크릿키가 비어있습니다.\n프로그램을 종료합니다.", "인증 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return false;
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
                    Form1.Console_print($"[+] LS증권 {modeText} 토큰 발급 성공");
                    return true;
                }
                else
                {
                    Form1.Console_print($"[-] LS증권 {modeText} 인증 실패: {(int)response.StatusCode}");
                    Form1.Console_print($"[LS 응답]: {responseBody}");
                    MessageBox.Show($"LS증권 {modeText} API 인증에 실패했습니다.\n상태 코드: {(int)response.StatusCode}\n상세: {responseBody}\n\n프로그램을 종료합니다.", "인증 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Form1.Console_print($"[-] LS증권 {modeText} 통신 오류: " + ex.Message);
                MessageBox.Show($"LS증권 {modeText} 서버 통신 중 오류가 발생했습니다.\n{ex.Message}\n\n프로그램을 종료합니다.", "통신 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return false;
            }
        }




    }
}
