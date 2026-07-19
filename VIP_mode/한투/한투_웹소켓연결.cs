//using System;
//using System.IO;
//using System.Net.WebSockets;
//using System.Security.Cryptography;
//using System.Text;
//using System.Text.Json;
//using System.Threading;
//using System.Threading.Tasks;

//namespace 지니64
//{
//    public class 한투_웹소켓연결
//    {
//        public static ClientWebSocket websocket;
//        public static bool connected = false;

//        // 💡 [최적화] 송신 시 스레드 충돌 방지용 가벼운 락 [cite: 2]
//        private static readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);
//        private const int ReceiveBufferSize = 4096;
//        private static readonly byte[] _buffer = new byte[ReceiveBufferSize];

//        // 💡 한국투자증권 AES256 복호화용 Key / IV 캐싱
//        public static string AES_Key = "";
//        public static string AES_IV = "";

//        // ====================================================================
//        // 💡 [저사양 PC 메모리/CPU 최적화 변수] [cite: 2]
//        // 10초마다 JSON 객체를 만들지 않고, 미리 만들어둔 고정 바이트 배열을 전송하여 
//        // 가비지 컬렉터(GC) 호출과 CPU 낭비를 원천 차단합니다.
//        // ====================================================================
//        private static readonly byte[] _kisPingBytes = System.Text.Encoding.UTF8.GetBytes("{\"header\":{\"tr_id\":\"PINGPONG\"}}");
//        private static CancellationTokenSource cts_핑;


//        public static async Task ConnectAsync(bool isMock)
//        {
//            try
//            {
//                // 실전/모의투자에 따른 URL 분기
//                string url = isMock
//                    ? "ws://ops.koreainvestment.com:31000" // 모의 Domain
//                    : "ws://ops.koreainvestment.com:21000"; // 실전 Domain

//                Uri uri = new Uri(url);

//                if (websocket != null)
//                {
//                    try { websocket.Dispose(); } catch { }
//                }

//                // 💡 [디버깅 추가] 연결 진행 상태 출력
//                Form1.Console_print($">> [한국투자증권 웹소켓] 서버 연결을 시도 중입니다... ({url})");

//                websocket = new ClientWebSocket();
//                await websocket.ConnectAsync(uri, CancellationToken.None);
//                connected = true;

//                Form1.Console_print($">> [한국투자증권 웹소켓] 서버 접속 성공! ({url})");

//                // 수신 대기 및 10초 핑(하트비트) 시작 연결
//                _ = ListenAsync();
//                핑_10초_반복시작();
//            }
//            catch (Exception ex)
//            {
//                Form1.Console_print($"[한투 웹소켓 연결 실패] {ex.Message}");
//                // 필요시 3초 후 재연결 로직 호출
//            }
//        }

//        public static async Task SendMessageAsync(object message)
//        {
//            if (connected && websocket != null && websocket.State == WebSocketState.Open)
//            {
//                // 💡 [메모리 최적화] string 변환 없이 객체를 바로 UTF8 바이트 배열로 변환 (GC 부하 감소) [cite: 2]
//                var bytes = JsonSerializer.SerializeToUtf8Bytes(message);

//                await _sendLock.WaitAsync();
//                try
//                {
//                    if (websocket.State == WebSocketState.Open)
//                    {
//                        await websocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
//                    }
//                }
//                finally
//                {
//                    _sendLock.Release();
//                }
//            }
//        }

//        public static async Task ListenAsync()
//        {
//            var buffer = _buffer;

//            while (connected && websocket != null && websocket.State == WebSocketState.Open)
//            {
//                using (var stream = new MemoryStream())
//                {
//                    try
//                    {
//                        WebSocketReceiveResult result;
//                        do
//                        {
//                            result = await websocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
//                            if (result.MessageType == WebSocketMessageType.Close) break;
//                            stream.Write(buffer, 0, result.Count);
//                        } while (!result.EndOfMessage);

//                        if (result.MessageType == WebSocketMessageType.Close)
//                        {
//                            Form1.Console_print(">> 🤝 한투 서버로부터 정상 종료(Close) 요청을 받았습니다.");
//                            break;
//                        }

//                        // 💡 [CPU 최적화] ToArray() 대신 GetBuffer()를 사용하여 메모리 복사 최소화 
//                        if (stream.Length == 0) continue;
//                        string response = Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);

//                        // 💡 [디버깅 추가] 웹소켓 서버에서 수신되는 모든 원본 데이터 출력
//                        Form1.Console_print($"[한투 수신(전체메시지)] {response}");

//                        ProcessMessage(response);
//                    }
//                    catch (Exception ex)
//                    {
//                        Form1.Console_print($"[한투 수신 에러] {ex.Message}");
//                        break;
//                    }
//                }
//            }

//            connected = false;

//            // 💡 루프 탈출 시(연결 끊김) 핑 반복 중지
//            핑_반복중지();

//            Form1.Console_print(">> [한국투자증권] 웹소켓 연결이 해제되었습니다. 재연결이 필요합니다.");
//        }

//        private static void ProcessMessage(string response)
//        {
//            if (string.IsNullOrWhiteSpace(response)) return;

//            // 1. JSON 형태 응답 (구독 성공 및 AES256 KEY/IV 수신)
//            if (response.StartsWith("{"))
//            {
//                try
//                {
//                    using (JsonDocument doc = JsonDocument.Parse(response))
//                    {
//                        JsonElement root = doc.RootElement;
//                        if (root.TryGetProperty("body", out JsonElement body))
//                        {
//                            string msg1 = body.TryGetProperty("msg1", out var m1) ? m1.GetString() : "";

//                            // 구독 성공 시 복호화 키 저장
//                            if (msg1 == "SUBSCRIBE SUCCESS" && body.TryGetProperty("output", out JsonElement output))
//                            {
//                                AES_IV = output.TryGetProperty("iv", out var iv) ? iv.GetString() : "";
//                                AES_Key = output.TryGetProperty("key", out var key) ? key.GetString() : "";
//                                Form1.Console_print(">> [한투] 실시간 구독 성공! (AES 암호키 수신 완료)");
//                            }
//                            else
//                            {
//                                Form1.Console_print($">> [한투 시스템 메시지] {msg1}");
//                            }
//                        }
//                    }
//                }
//                catch { }
//                return;
//            }

//            // 2. 실시간 체결 데이터 응답 ("|" 로 구분)
//            // ex) 1|H0STCNI0|001|암호화된데이터... 또는 0|H0STCNI0|001|평문데이터...
//            string[] parts = response.Split('|');
//            if (parts.Length >= 4)
//            {
//                string isEncrypted = parts[0]; // 0: 평문, 1: 암호화
//                string tr_id = parts[1];
//                string dataCount = parts[2];
//                string realData = parts[3];

//                if (isEncrypted == "1")
//                {
//                    realData = AES_Decrypt(realData, AES_Key, AES_IV);
//                }

//                // "^" 구분자로 데이터 분리 (체결 통보 데이터)
//                string[] dataFields = realData.Split('^');

//                // 체결 데이터 처리 로직 호출
//                // if (tr_id == "H0STCNI0") -> 실전투자 체결통보
//                // if (tr_id == "H0STCNI9") -> 모의투자 체결통보
//                Form1.Console_print($"[한투 체결데이터 수신] {realData}");
//            }
//        }

//        // =================================================================
//        // 💡 [필수] 한국투자증권 AES256 복호화 함수
//        // =================================================================
//        public static string AES_Decrypt(string cipherText, string key, string iv)
//        {
//            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(iv))
//                return cipherText;

//            try
//            {
//                byte[] cipherBytes = Convert.FromBase64String(cipherText);
//                using (Aes aesAlg = Aes.Create())
//                {
//                    aesAlg.Key = Encoding.UTF8.GetBytes(key);
//                    aesAlg.IV = Encoding.UTF8.GetBytes(iv);
//                    aesAlg.Mode = CipherMode.CBC;
//                    aesAlg.Padding = PaddingMode.PKCS7;

//                    using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
//                    using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
//                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
//                    using (StreamReader srDecrypt = new StreamReader(csDecrypt, Encoding.UTF8))
//                    {
//                        return srDecrypt.ReadToEnd();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Form1.Console_print($"[복호화 에러] {ex.Message}");
//                return "";
//            }
//        }

//        // =================================================================
//        // 1. PING 10초 반복 시작 
//        // =================================================================
//        public static void 핑_10초_반복시작()
//        {
//            // 이미 실행 중이면 중복 실행 방지
//            if (cts_핑 != null) return;

//            cts_핑 = new CancellationTokenSource();
//            Form1.Console_print("[한투 웹소켓] PING 10초 반복 전송을 시작합니다.");

//            Task.Run(async () =>
//            {
//                while (!cts_핑.IsCancellationRequested)
//                {
//                    // 💡 무거운 SendMessageAsync(new { ... }) 대신 초고속 핑() 함수 호출 [cite: 2]
//                    await 핑();

//                    try { await Task.Delay(10000, cts_핑.Token); }
//                    catch (TaskCanceledException) { break; }
//                }
//            }, cts_핑.Token);
//        }

//        // =================================================================
//        // 2. PING 10초 반복 중지
//        // =================================================================
//        public static void 핑_반복중지()
//        {
//            if (cts_핑 != null)
//            {
//                cts_핑.Cancel();
//                cts_핑.Dispose();
//                cts_핑 = null;
//                Form1.Console_print("[한투 웹소켓] PING 전송이 중지되었습니다.");
//            }
//        }

//        // =================================================================
//        // 3. [최적화] 실제 PING 고속 전송 함수 [cite: 2]
//        // =================================================================
//        public static async Task 핑()
//        {
//            // [입구컷 1] 봇이 종료 중이면 즉시 중단
//            if (Form1.프로그램종료중) return;

//            // [입구컷 2] 소켓이 열려있지 않다면 허공에 쏘지 않음
//            if (!connected || websocket == null || websocket.State != WebSocketState.Open)
//            {
//                return;
//            }

//            // [저사양 PC 최적화] 매번 string 생성 및 JSON 변환을 하지 않고, 
//            // 캐싱된 바이트 배열(_kisPingBytes)을 즉시 전송하여 시스템 부하 제로화 [cite: 2]
//            await _sendLock.WaitAsync();
//            try
//            {
//                if (websocket.State == WebSocketState.Open)
//                {
//                    await websocket.SendAsync(new ArraySegment<byte>(_kisPingBytes), WebSocketMessageType.Text, true, CancellationToken.None);
//                }
//            }
//            finally
//            {
//                _sendLock.Release();
//            }
//        }
//    }
//}

using System;
using System.IO;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace 지니64
{
    public class 한투_웹소켓연결
    {
        public static ClientWebSocket websocket;
        public static bool connected = false;

        // 💡 [최적화] 송신 시 스레드 충돌 방지용 가벼운 락
        private static readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);

        // 💡 [수정] 한국투자 체결통보 등 긴 데이터 수신을 대비해 버퍼 크기 증가
        private const int ReceiveBufferSize = 16384;
        private static readonly byte[] _buffer = new byte[ReceiveBufferSize];

        // 💡 한국투자증권 AES256 복호화용 Key / IV 캐싱
        public static string AES_Key = "";
        public static string AES_IV = "";

        // ====================================================================
        // 💡 [저사양 PC 메모리/CPU 최적화 변수]
        // 10초마다 JSON 객체를 만들지 않고, 미리 만들어둔 고정 바이트 배열을 전송하여 
        // 가비지 컬렉터(GC) 호출과 CPU 낭비를 원천 차단합니다.
        // 한투 서버는 단순 문자열을 받으면 에러를 뱉으므로 정상적인 JSON 형태로 전송해야 합니다.
        // ====================================================================
        private static readonly byte[] _kisPingBytes = System.Text.Encoding.UTF8.GetBytes("{\"header\":{\"tr_id\":\"PINGPONG\"}}");
        private static CancellationTokenSource cts_핑;


        public static async Task ConnectAsync(bool isMock)
        {
            try
            {
                // 실전/모의투자에 따른 URL 분기
                string url = isMock
                    ? "ws://ops.koreainvestment.com:31000" // 모의 Domain
                    : "ws://ops.koreainvestment.com:21000"; // 실전 Domain

                Uri uri = new Uri(url);

                if (websocket != null)
                {
                    // Dispose 전 안전하게 CloseAsync 수행
                    if (websocket.State == WebSocketState.Open)
                    {
                        try { await websocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None); } catch { }
                    }
                    try { websocket.Dispose(); } catch { }
                }

                // 연결 진행 상태 출력
                Form1.Console_print($">> [한국투자증권 웹소켓] 서버 연결을 시도 중입니다... ({url})");

                websocket = new ClientWebSocket();
                await websocket.ConnectAsync(uri, CancellationToken.None);
                connected = true;

                Form1.Console_print($">> [한국투자증권 웹소켓] 서버 접속 성공! ({url})");

                // 수신 대기는 시작하지만, 핑(PING)은 SUBSCRIBE SUCCESS 응답을 받은 이후에 시작합니다.
                _ = ListenAsync();
            }
            catch (Exception ex)
            {
                Form1.Console_print($"[한투 웹소켓 연결 실패] {ex.ToString()}");
                // 필요시 3초 후 재연결 로직 호출
            }
        }

        public static async Task SendMessageAsync(object message)
        {
            if (connected && websocket != null && websocket.State == WebSocketState.Open)
            {
                // 💡 [메모리 최적화] string 변환 없이 객체를 바로 UTF8 바이트 배열로 변환 (GC 부하 감소)
                var bytes = JsonSerializer.SerializeToUtf8Bytes(message);

                await _sendLock.WaitAsync();
                try
                {
                    if (websocket.State == WebSocketState.Open)
                    {
                        await websocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
                finally
                {
                    _sendLock.Release();
                }
            }
        }

        public static async Task ListenAsync()
        {
            var buffer = _buffer;

            while (connected && websocket != null && websocket.State == WebSocketState.Open)
            {
                using (var stream = new MemoryStream())
                {
                    try
                    {
                        WebSocketReceiveResult result;
                        do
                        {
                            result = await websocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                            if (result.MessageType == WebSocketMessageType.Close) break;

                            // 수신된 데이터가 있을 때만 Write 처리
                            if (result.Count > 0)
                            {
                                stream.Write(buffer, 0, result.Count);
                            }
                        } while (!result.EndOfMessage);

                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            Form1.Console_print(">> 🤝 한투 서버로부터 정상 종료(Close) 요청을 받았습니다.");
                            break;
                        }

                        // 💡 [CPU 최적화] ToArray() 대신 GetBuffer()를 사용하여 메모리 복사 최소화 
                        if (stream.Length == 0) continue;
                        string response = Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);

                        // 웹소켓 서버에서 수신되는 모든 원본 데이터 출력
                        Form1.Console_print($"[한투 수신(전체메시지)] {response}");

                        ProcessMessage(response);
                    }
                    // 예외 처리 세분화
                    catch (WebSocketException)
                    {
                        break;
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        Form1.Console_print($"[한투 수신 에러] {ex.ToString()}");
                        break;
                    }
                }
            }

            connected = false;

            // 루프 탈출 시(연결 끊김) 핑 반복 중지
            핑_반복중지();

            Form1.Console_print(">> [한국투자증권] 웹소켓 연결이 해제되었습니다. 재연결이 필요합니다.");
        }

        private static void ProcessMessage(string response)
        {
            if (string.IsNullOrWhiteSpace(response)) return;

            // 1. JSON 형태 응답 (구독 성공 및 AES256 KEY/IV 수신)
            if (response.StartsWith("{"))
            {
                try
                {
                    using (JsonDocument doc = JsonDocument.Parse(response))
                    {
                        JsonElement root = doc.RootElement;

                        // PINGPONG 응답은 무시하여 일반 시스템 메시지와 구분하고 로그 간결화
                        if (root.TryGetProperty("header", out JsonElement header))
                        {
                            if (header.TryGetProperty("tr_id", out var tr) && tr.GetString() == "PINGPONG")
                            {
                                return;
                            }
                        }

                        if (root.TryGetProperty("body", out JsonElement body))
                        {
                            string msg1 = body.TryGetProperty("msg1", out var m1) ? m1.GetString() : "";

                            // 구독 성공 시 복호화 키 저장
                            if (msg1 == "SUBSCRIBE SUCCESS" && body.TryGetProperty("output", out JsonElement output))
                            {
                                AES_IV = output.TryGetProperty("iv", out var iv) ? iv.GetString() : "";
                                AES_Key = output.TryGetProperty("key", out var key) ? key.GetString() : "";
                                Form1.Console_print(">> [한투] 실시간 구독 성공! (AES 암호키 수신 완료)");

                                // 구독 성공 후 AES 키가 확보된 시점에 PING 타이머를 안전하게 시작
                                핑_10초_반복시작();
                            }
                            else
                            {
                                Form1.Console_print($">> [한투 시스템 메시지] {msg1}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 예외 발생 시 상세 StackTrace 출력
                    Form1.Console_print($"[ProcessMessage 에러] {ex.ToString()}");
                }
                return;
            }

            // 2. 실시간 체결 데이터 응답 ("|" 로 구분)
            // ex) 1|H0STCNI0|001|암호화된데이터... 또는 0|H0STCNI0|001|평문데이터...
            string[] parts = response.Split('|');
            if (parts.Length >= 4)
            {
                string isEncrypted = parts[0]; // 0: 평문, 1: 암호화
                string tr_id = parts[1];
                string dataCount = parts[2];
                string realData = parts[3];

                if (isEncrypted == "1")
                {
                    realData = AES_Decrypt(realData, AES_Key, AES_IV);
                }

                // "^" 구분자로 데이터 분리 (체결 통보 데이터)
                string[] dataFields = realData.Split('^');

                // 체결 데이터 처리 로직 호출
                // if (tr_id == "H0STCNI0") -> 실전투자 체결통보
                // if (tr_id == "H0STCNI9") -> 모의투자 체결통보
                Form1.Console_print($"[한투 체결데이터 수신] {realData}");
            }
        }

        // =================================================================
        // 💡 [필수] 한국투자증권 AES256 복호화 함수
        // =================================================================
        public static string AES_Decrypt(string cipherText, string key, string iv)
        {
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(iv))
                return cipherText;

            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(key);
                    aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                    aesAlg.Mode = CipherMode.CBC;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt, Encoding.UTF8))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                // 복호화 에러 발생 시 상세 StackTrace 출력
                Form1.Console_print($"[복호화 에러] {ex.ToString()}");
                return "";
            }
        }

        // =================================================================
        // 1. PING 10초 반복 시작 
        // =================================================================
        public static void 핑_10초_반복시작()
        {
            // 이미 실행 중이면 중복 실행 방지
            if (cts_핑 != null) return;

            cts_핑 = new CancellationTokenSource();
            Form1.Console_print("[한투 웹소켓] PING 10초 반복 전송을 시작합니다.");

            Task.Run(async () =>
            {
                while (!cts_핑.IsCancellationRequested)
                {
                    // 무거운 SendMessageAsync(new { ... }) 대신 초고속 핑() 함수 호출
                    await 핑();

                    try { await Task.Delay(10000, cts_핑.Token); }
                    catch (TaskCanceledException) { break; }
                }
            }, cts_핑.Token);
        }

        // =================================================================
        // 2. PING 10초 반복 중지
        // =================================================================
        public static void 핑_반복중지()
        {
            if (cts_핑 != null)
            {
                cts_핑.Cancel();
                cts_핑.Dispose();
                cts_핑 = null;
                Form1.Console_print("[한투 웹소켓] PING 전송이 중지되었습니다.");
            }
        }

        // =================================================================
        // 3. [최적화] 실제 PING 고속 전송 함수
        // =================================================================
        public static async Task 핑()
        {
            // [입구컷 1] 봇이 종료 중이면 즉시 중단
            if (Form1.프로그램종료중) return;

            // [입구컷 2] 소켓이 열려있지 않다면 허공에 쏘지 않음
            if (!connected || websocket == null || websocket.State != WebSocketState.Open)
            {
                return;
            }

            // SUBSCRIBE SUCCESS 이전(AES키가 아직 없는 상태)에는 Ping을 보내지 않음
            if (string.IsNullOrEmpty(AES_Key))
                return;

            // [저사양 PC 최적화] 매번 string 생성 및 JSON 변환을 하지 않고, 
            // 캐싱된 바이트 배열(_kisPingBytes)을 즉시 전송하여 시스템 부하 제로화
            await _sendLock.WaitAsync();
            try
            {
                if (websocket.State == WebSocketState.Open)
                {
                    await websocket.SendAsync(new ArraySegment<byte>(_kisPingBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            finally
            {
                _sendLock.Release();
            }
        }
    }
}