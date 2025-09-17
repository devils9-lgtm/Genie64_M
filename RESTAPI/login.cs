//using System;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;

//class Program
//{
//    static async Task Main(string[] args)
//    {
//        // 1. 요청할 API URL
//        string host = "https://api.kiwoom.com"; // 실전투자
//        string endpoint = "/oauth2/token";
//        string url = host + endpoint;

//        // 2. 요청 데이터
//        var paramsData = new
//        {
//            grant_type = "client_credentials", // grant_type
//            appkey = "AxserEsdcredca.....", // 앱키
//            secretkey = "SEefdcwcforehDre2fdvc...." // 시크릿키
//        };

//        // 3. HttpClient를 사용하여 POST 요청
//        using (var client = new HttpClient())
//        {
//            client.DefaultRequestHeaders.Add("Content-Type", "application/json;charset=UTF-8");

//            var json = JsonConvert.SerializeObject(paramsData);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");

//            var response = await client.PostAsync(url, content);

//            // 4. 응답 상태 코드와 데이터 출력
//            Console.WriteLine("Code: " + response.StatusCode);
//            var responseBody = await response.Content.ReadAsStringAsync();
//            Console.WriteLine("Body: " + responseBody);
//        }
//    }
//}


using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

class Program
{
    // C#에서는 async/await를 사용해 비동기 HTTP 요청을 처리합니다.
    static async Task Main(string[] args)
    {
        await fn_au10001();
    }

    static async Task fn_au10001()
    {
        // 1. 요청할 API URL
        string host = "https://api.kiwoom.com"; // 실전투자
        string endpoint = "/oauth2/token";
        string url = host + endpoint;

        // 2. 요청 데이터
        var paramsData = new
        {
            grant_type = "client_credentials",
            appkey = "AxserEsdcredca.....", // 앱키
            secretkey = "SEefdcwcforehDre2fdvc...." // 시크릿키
        };

        // 3. HttpClient를 사용하여 POST 요청
        using (var client = new HttpClient())
        {
            // 헤더 설정 (Python 코드의 headers와 동일)
            client.DefaultRequestHeaders.Add("Content-Type", "application/json;charset=UTF-8");

            // JSON 직렬화
            var json = JsonConvert.SerializeObject(paramsData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // HTTP POST 요청
            var response = await client.PostAsync(url, content);

            // 4. 응답 상태 코드와 데이터 출력
            Console.WriteLine("Code: " + response.StatusCode);

            // 헤더 출력 (Python 코드의 headers 출력과 동일하게)
            // C#에서는 헤더를 직접 접근하여 출력할 수 있습니다.
            //Console.WriteLine("Header:");
            //Console.WriteLine("next-key: " + response.Headers.TryGetValues("next-key", out var nextKey) ? string.Join(", ", nextKey) : "N/A");
            //Console.WriteLine("cont-yn: " + response.Headers.TryGetValues("cont-yn", out var contYn) ? string.Join(", ", contYn) : "N/A");
            //Console.WriteLine("api-id: " + response.Headers.TryGetValues("api-id", out var apiId) ? string.Join(", ", apiId) : "N/A");

            // 삼항 연산자를 괄호로 감싸서 먼저 문자열을 생성하도록 합니다.
            Console.WriteLine($"next-key: {(response.Headers.TryGetValues("next-key", out var nextKey) ? string.Join(", ", nextKey) : "N/A")}");
            Console.WriteLine($"cont-yn: {(response.Headers.TryGetValues("cont-yn", out var contYn) ? string.Join(", ", contYn) : "N/A")}");
            Console.WriteLine($"api-id: {(response.Headers.TryGetValues("api-id", out var apiId) ? string.Join(", ", apiId) : "N/A")}");

            // 응답 본문 출력
            var responseBody = await response.Content.ReadAsStringAsync();

            // 응답 본문이 JSON인지 확인 후 예쁘게 출력
            try
            {
                var jsonObject = JsonConvert.DeserializeObject(responseBody);
                Console.WriteLine("Body: " + JsonConvert.SerializeObject(jsonObject, Formatting.Indented));
            }
            catch (JsonException)
            {
                Console.WriteLine("Body: " + responseBody);
            }
        }
    }
}