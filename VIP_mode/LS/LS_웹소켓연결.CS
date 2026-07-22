using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace 지니64
{
    public class LS_웹소켓연결
    {
        public static ClientWebSocket websocket;
        public static bool connected = false;

        // 💡 [최적화] 송신 시 스레드 충돌 방지용 가벼운 락
        private static readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);

        // 💡 [수정] LS증권 체결통보 등 긴 데이터 수신을 대비해 넉넉한 버퍼 크기 유지
        private const int ReceiveBufferSize = 16384;
        private static readonly byte[] _buffer = new byte[ReceiveBufferSize];

        // ====================================================================
        // 💡 [저사양 PC 메모리/CPU 최적화 변수]
        // 10초마다 JSON 객체를 만들지 않고, 미리 만들어둔 고정 바이트 배열을 전송하여 
        // 가비지 컬렉터(GC) 호출과 CPU 낭비를 원천 차단합니다.
        // LS증권의 핑(PING) 규격인 tr_type: "3" 을 사용합니다.
        // ====================================================================
        private static readonly byte[] _lsPingBytes = System.Text.Encoding.UTF8.GetBytes("{\"header\":{\"tr_type\":\"3\"}}");
        private static CancellationTokenSource cts_핑;


        public static async Task ConnectAsync(bool isMock)
        {
            try
            {
                // 실전/모의투자에 따른 LS증권 웹소켓 URL 분기
                string url = isMock
                    ? "wss://openapi.ls-sec.co.kr:29443/websocket" // 모의 Domain
                    : "wss://openapi.ls-sec.co.kr:9443/websocket"; // 실전 Domain

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
                Form1.Console_print($">> [LS증권 웹소켓] 서버 연결을 시도 중입니다... ({url})");

                websocket = new ClientWebSocket();

                // ====================================================================
                // 💡 [저사양 PC 최고 수준 최적화]
                // 수동 JSON PING 전송을 없애고, C# 웹소켓 내장 표준 Ping/Pong 프레임을 
                // 10초 간격으로 자동 전송하도록 설정합니다. (CPU/메모리 사용률 0%)
                // ====================================================================
                websocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(10);

                await websocket.ConnectAsync(uri, CancellationToken.None);
                connected = true;

                Form1.Console_print($">> [LS증권 웹소켓] 서버 접속 성공! ({url})");

                // LS증권은 복호화 키(AES) 대기 과정이 없으므로 연결 직후 바로 수신 시작
                // 💡 잘못된 JSON 핑 전송 함수 호출 제거 완료
                _ = ListenAsync();
            }
            catch (Exception ex)
            {
                Form1.Console_print($"[LS증권 웹소켓 연결 실패] {ex.ToString()}");
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
                            Form1.Console_print(">> 🤝 LS증권 서버로부터 정상 종료(Close) 요청을 받았습니다.");
                            break;
                        }

                        // 💡 [CPU 최적화] ToArray() 대신 GetBuffer()를 사용하여 메모리 복사 최소화 
                        if (stream.Length == 0) continue;
                        string response = Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);

                        // 웹소켓 서버에서 수신되는 모든 원본 데이터 출력 (디버깅용)
                        Form1.Console_print($"[LS증권 수신(전체메시지)] {response}");

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
                        Form1.Console_print($"[LS증권 수신 에러] {ex.ToString()}");
                        break;
                    }
                }
            }

            connected = false;

            // 💡 잘못된 핑 중지 함수 호출 제거 완료
            Form1.Console_print(">> [LS증권] 웹소켓 연결이 해제되었습니다. 재연결이 필요합니다.");
        }

        private static void ProcessMessage(string response)
        {
            if (string.IsNullOrWhiteSpace(response)) return;

            // LS증권은 모든 응답이 평문 JSON 형태로 수신됩니다 (AES 복호화 불필요)
            try
            {
                using (JsonDocument doc = JsonDocument.Parse(response))
                {
                    JsonElement root = doc.RootElement;

                    // PING 응답 등 불필요한 시스템 메시지 필터링 (로그 간결화)
                    if (root.TryGetProperty("header", out JsonElement header))
                    {
                        if (header.TryGetProperty("tr_type", out var trType) && trType.GetString() == "3")
                        {
                            Form1.Console_print($"[LS PING 수신] 서버_마지막_통신시간: {DateTime.Now.ToString("HH:mm:ss")}");
                            // PING(3)에 대한 단순 응답이므로 무시하고 리턴
                            return;
                        }
                    }

                    // 💡 [수정] 바디(body) 영역이 존재할 때 msg1이나 기타 메시지를 안전하게 추출하여 출력
                    if (root.TryGetProperty("body", out JsonElement body))
                    {
                        string bodyJson = body.GetRawText();
                        Form1.Console_print($"[LS 바디 데이터 수신] {bodyJson}");
                    }

                    // ==========================================================
                    // 💡 여기에 LS증권 실시간 체결 데이터 파싱 및 분배 로직 작성
                    // ==========================================================
                    // 예: if (header.TryGetProperty("tr_cd", out var trCd)) { ... }

                }
            }
            catch (Exception ex)
            {
                // 예외 발생 시 상세 StackTrace 출력
                Form1.Console_print($"[ProcessMessage 에러] {ex.ToString()}");
            }
        }

      
    }
}