using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace 지니_64
{
    public class KiwoomApi
    {
        private static readonly HttpClient client = new HttpClient();

        // 로그인 상태 확인
        public static async Task CheckLoginStatus(string token)
        {
            var url = "https://api.kiwoom.com/v1/login/status";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Login Status: " + jsonResponse);
        }

        // TR 데이터 요청
        public static async Task RequestTrData(string token, string trCode)
        {
            var url = $"https://api.kiwoom.com/v1/tr/{trCode}";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine("TR Data: " + jsonResponse);
        }

        // 메시지 수신
        public static async Task ReceiveMessage(string token)
        {
            var url = "https://api.kiwoom.com/v1/messages";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Messages: " + jsonResponse);
        }

        // 실시간 데이터 수신
        public static async Task ReceiveRealData(string token, string stockCode)
        {
            var url = $"https://api.kiwoom.com/v1/realtime/{stockCode}";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Real Data: " + jsonResponse);
        }

        // 체결 잔고 데이터 수신
        public static async Task ReceiveChejanData(string token)
        {
            var url = "https://api.kiwoom.com/v1/chejan";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Chejan Data: " + jsonResponse);
        }

        // 실행 예시
        public static async Task Main(string[] args)
        {
            string accessToken = "[접근 토큰]";  // 발급받은 접근 토큰
            await CheckLoginStatus(accessToken);
            await RequestTrData(accessToken, "TR_CODE");
            await ReceiveMessage(accessToken);
            await ReceiveRealData(accessToken, "A005930");  // 삼성전자
            await ReceiveChejanData(accessToken);
        }
    }
}
