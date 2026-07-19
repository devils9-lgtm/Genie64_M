
using System;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Globalization; // System.Text.Json 사용

namespace 지니64
{
    public class WebSocketClient
    {
        private const int ReceiveBufferSize = 4096;
        private readonly byte[] _buffer = new byte[ReceiveBufferSize];
        private readonly RealDataDispatcher _dispatcher;

        public WebSocketClient()
        {
            Form1.websocket = new ClientWebSocket();
            Form1.connected = false;
            _dispatcher = new RealDataDispatcher();
        }

     
        //public async Task SendPingAsync(object message)
        //{
        //    if (Form1.connected && Form1.websocket != null && Form1.websocket.State == System.Net.WebSockets.WebSocketState.Open)
        //    {
        //        var jsonMessage = System.Text.Json.JsonSerializer.Serialize(message);
        //        var bytes = System.Text.Encoding.UTF8.GetBytes(jsonMessage);

        //        // 💡 만약 여기서 _sendLock에 빨간줄이 간다면, 위에 SendMessageAsync에서 쓰는 자물쇠 변수 이름을 확인해서 똑같이 맞춰주세요.
        //        await _sendLock.WaitAsync();
        //        try
        //        {
        //            if (Form1.websocket.State == System.Net.WebSockets.WebSocketState.Open)
        //            {
        //                await Form1.websocket.SendAsync(new ArraySegment<byte>(bytes), System.Net.WebSockets.WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);
        //            }
        //        }
        //        finally
        //        {
        //            _sendLock.Release();
        //        }
        //    }
        //}

        public async Task ConnectAsync()
        {
            try
            {
                string url = "wss://api.kiwoom.com:10000/api/dostk/websocket"; // 실전투자

                if (GenieConfig.checkBox_Simulation)
                {
                    url = "wss://mockapi.kiwoom.com:10000/api/dostk/websocket"; // 모의투자
                }
                Uri uri = new Uri(url);

                // [핵심 1] 헌 소켓 버리기 (메모리 누수 방지)
                if (Form1.websocket != null)
                {
                    try { Form1.websocket.Dispose(); } catch { }
                }

                // [핵심 2] 새 소켓 장착! (이 한 줄이 에러를 완벽하게 해결합니다)
                Form1.websocket = new System.Net.WebSockets.ClientWebSocket();

                // 새 소켓으로 접속 시도
                await Form1.websocket.ConnectAsync(uri, CancellationToken.None);
                Form1.connected = true;

                var loginPacket = new
                {
                    trnm = "LOGIN",
                    token = Form1.API_token
                };

                await SendMessageAsync(loginPacket);
                Form1.Console_print(">> [웹소켓] 서버 접속 및 로그인 패킷 전송 완료!");
            }
            catch (Exception ex)
            {
                // [핵심 3] 시한폭탄 제거: MessageBox.Show 삭제
                Form1.Console_print($"[웹소켓 연결 실패] {ex.Message}");

                // 에러를 밖으로 던져서, 바깥쪽 while 루프가 3초 대기 후 다시 시도하게 만듭니다.
                throw;
            }
        }

        // 1. [핵심] 메시지 전송이 겹치지 않게 줄을 세우는 비동기 신호등(Lock) 추가
        private static readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);

        //public async Task SendMessageAsync(object message)
        //{
        //    if (Form1.connected && Form1.websocket.State == WebSocketState.Open)
        //    {
        //        var jsonMessage = JsonSerializer.Serialize(message);
        //        var bytes = Encoding.UTF8.GetBytes(jsonMessage);

        //        // 2. 파란불이 켜질 때까지(앞선 전송이 끝날 때까지) 대기합니다.
        //        await _sendLock.WaitAsync();
        //        try
        //        {
        //            // 3. 내 차례가 왔을 때 소켓이 아직 열려있는지 한 번 더 확인 후 진짜 전송!
        //            if (Form1.websocket.State == WebSocketState.Open)
        //            {
        //                await Form1.websocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
        //            }
        //        }
        //        finally
        //        {
        //            // 4. 전송이 무사히 끝났거나 에러가 나더라도 무조건 다음 사람을 위해 신호등을 풀어줍니다.
        //            _sendLock.Release();
        //        }
        //    }
        //    else
        //    {
        //        string 메세지 = "SendMessageAsync =>> WebSocket Disconnected. Cannot send message.";
        //        if (!Form1.중복접속) REG.재연결(메세지);
        //    }
        //}

        //public async Task ListenAsync()
        //{
        //    var buffer = _buffer;

        //    // 1. 웹소켓이 살아있을 때만 루프를 돕니다.
        //    while (Form1.connected && Form1.websocket != null && Form1.websocket.State == WebSocketState.Open)
        //    {
        //        using (var stream = new MemoryStream())
        //        {
        //            WebSocketReceiveResult result = null;
        //            try
        //            {
        //                do
        //                {
        //                    result = await Form1.websocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        //                    // 1. 여기서 안쪽(do-while) 루프 탈출!
        //                    if (result.MessageType == WebSocketMessageType.Close)
        //                    {
        //                        break;
        //                    }
        //                    stream.Write(buffer, 0, result.Count);
        //                } while (!result.EndOfMessage);

        //                // ==========================================================
        //                // 💡 [핵심] 안쪽 루프를 빠져나온 후, Close 메시지면 바깥쪽(while) 루프도 마저 탈출!
        //                // ==========================================================
        //                if (result.MessageType == WebSocketMessageType.Close)
        //                {
        //                    Form1.Console_print(">> 🤝 서버로부터 정상 종료(Close) 요청을 받았습니다. 재연결을 준비합니다.");
        //                    break; // 여기서 진짜 바깥쪽 루프를 깨고 밑바닥(재연결 로직)으로 내려갑니다!
        //                }

        //                var response = Encoding.UTF8.GetString(stream.ToArray());

        //                // 💡 [안전장치] 혹시라도 알맹이가 없는 빈 깡통이 오면 무시!
        //                if (string.IsNullOrWhiteSpace(response)) continue;

        //                ProcessMessage(response);
        //            }
        //            // 💡 [그물 1] 방금 뜨신 0x83760002 같은 모든 통신/프로토콜 에러를 여기서 한 방에 다 흡수합니다!
        //            catch (WebSocketException ex)
        //            {
        //                Form1.Console_print($"ListenAsync =>> 웹소켓 통신 불안정 감지 (사유: {ex.Message})");
        //                break; // 탈출해서 재연결 로직으로 이동
        //            }
        //            // 💡 [그물 2] 서버가 선을 뽑거나 우리가 토큰 만료로 강제로 닫았을 때
        //            catch (OperationCanceledException)
        //            {
        //                Form1.Console_print($"ListenAsync =>> 수신 작업이 취소/중단 되었습니다.");
        //                break;
        //            }
        //            // 💡 [그물 3] 나머지 진짜 알 수 없는 기타 에러
        //            catch (Exception ex)
        //            {
        //                Form1.Console_print($"Listen Error: {ex.Message}");
        //                break;
        //            }
        //        }
        //    }

        //    // ==========================================================
        //    // 💡 2. 루프를 빠져나온 후 처리 (살릴지, 우아하게 종료할지 결정)
        //    // ==========================================================

        //    if (Form1.중복접속)
        //    {
        //        Form1.Console_print("[긴급] 중복 접속 감지! 안전 종료 시퀀스를 시작합니다.");
        //        Helper.안전종료하기();
        //    }
        //    else
        //    {
        //        // 💡 3. 단순 네트워크 불안정으로 끊긴 경우 (기존 유지)
        //        string 메세지 = "[시스템] 웹소켓 수신이 중단되었습니다. 재연결 프로세스를 시작합니다...";
        //        REG.재연결(메세지);
        //    }

        //}


        public async Task SendPingAsync(object message)
        {
            if (Form1.connected && Form1.websocket != null && Form1.websocket.State == System.Net.WebSockets.WebSocketState.Open)
            {
                // 💡 [최적화] string 변환을 거치지 않고 직접 UTF8 바이트 배열로 직렬화하여 가비지 컬렉션(GC) 렉 방지
                var bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(message);

                // 💡 만약 여기서 _sendLock에 빨간줄이 간다면, 위에 SendMessageAsync에서 쓰는 자물쇠 변수 이름을 확인해서 똑같이 맞춰주세요.
                await _sendLock.WaitAsync();
                try
                {
                    if (Form1.websocket.State == System.Net.WebSockets.WebSocketState.Open)
                    {
                        await Form1.websocket.SendAsync(new ArraySegment<byte>(bytes), System.Net.WebSockets.WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);
                    }
                }
                finally
                {
                    _sendLock.Release();
                }
            }
        }


        public async Task SendMessageAsync(object message)
        {
            if (Form1.connected && Form1.websocket.State == WebSocketState.Open)
            {
                // 💡 [최적화] string 변환을 거치지 않고 직접 UTF8 바이트 배열로 직렬화하여 메모리 할당 최소화
                var bytes = JsonSerializer.SerializeToUtf8Bytes(message);

                // 2. 파란불이 켜질 때까지(앞선 전송이 끝날 때까지) 대기합니다.
                await _sendLock.WaitAsync();
                try
                {
                    // 3. 내 차례가 왔을 때 소켓이 아직 열려있는지 한 번 더 확인 후 진짜 전송!
                    if (Form1.websocket.State == WebSocketState.Open)
                    {
                        await Form1.websocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
                finally
                {
                    // 4. 전송이 무사히 끝났거나 에러가 나더라도 무조건 다음 사람을 위해 신호등을 풀어줍니다.
                    _sendLock.Release();
                }
            }
            else
            {
                string 메세지 = "SendMessageAsync =>> WebSocket Disconnected. Cannot send message.";
                if (!Form1.중복접속) REG.재연결(메세지);
            }
        }

        public async Task ListenAsync()
        {
            var buffer = _buffer;

            // 1. 웹소켓이 살아있을 때만 루프를 돕니다.
            while (Form1.connected && Form1.websocket != null && Form1.websocket.State == WebSocketState.Open)
            {
                using (var stream = new MemoryStream())
                {
                    WebSocketReceiveResult result = null;
                    try
                    {
                        do
                        {
                            result = await Form1.websocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                            // 1. 여기서 안쪽(do-while) 루프 탈출!
                            if (result.MessageType == WebSocketMessageType.Close)
                            {
                                break;
                            }
                            stream.Write(buffer, 0, result.Count);
                        } while (!result.EndOfMessage);

                        // ==========================================================
                        // 💡 [핵심] 안쪽 루프를 빠져나온 후, Close 메시지면 바깥쪽(while) 루프도 마저 탈출!
                        // ==========================================================
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            Form1.Console_print(">> 🤝 서버로부터 정상 종료(Close) 요청을 받았습니다. 재연결을 준비합니다.");
                            break; // 여기서 진짜 바깥쪽 루프를 깨고 밑바닥(재연결 로직)으로 내려갑니다!
                        }

                        // 💡 [최적화] ToArray()를 사용하면 메모리에 배열을 통째로 복사하므로 심각한 병목이 발생합니다.
                        // 저사양 PC를 위해 복사 없이 메모리 스트림의 내부 버퍼(GetBuffer)를 직접 읽어 처리 속도를 극대화합니다.
                        if (stream.Length == 0) continue;
                        var response = Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);

                        // 💡 [안전장치] 혹시라도 알맹이가 없는 빈 깡통이 오면 무시!
                        if (string.IsNullOrWhiteSpace(response)) continue;

                        ProcessMessage(response);
                    }
                    // 💡 [그물 1] 방금 뜨신 0x83760002 같은 모든 통신/프로토콜 에러를 여기서 한 방에 다 흡수합니다!
                    catch (WebSocketException ex)
                    {
                        Form1.Console_print($"ListenAsync =>> 웹소켓 통신 불안정 감지 (사유: {ex.Message})");
                        break; // 탈출해서 재연결 로직으로 이동
                    }
                    // 💡 [그물 2] 서버가 선을 뽑거나 우리가 토큰 만료로 강제로 닫았을 때
                    catch (OperationCanceledException)
                    {
                        Form1.Console_print($"ListenAsync =>> 수신 작업이 취소/중단 되었습니다.");
                        break;
                    }
                    // 💡 [그물 3] 나머지 진짜 알 수 없는 기타 에러
                    catch (Exception ex)
                    {
                        Form1.Console_print($"Listen Error: {ex.Message}");
                        break;
                    }
                }
            }

            // ==========================================================
            // 💡 2. 루프를 빠져나온 후 처리 (살릴지, 우아하게 종료할지 결정)
            // ==========================================================

            if (Form1.중복접속)
            {
                Form1.Console_print("[긴급] 중복 접속 감지! 안전 종료 시퀀스를 시작합니다.");
                Helper.안전종료하기();
            }
            else
            {
                // 💡 3. 단순 네트워크 불안정으로 끊긴 경우 (기존 유지)
                string 메세지 = "[시스템] 웹소켓 수신이 중단되었습니다. 재연결 프로세스를 시작합니다...";
                REG.재연결(메세지);
            }
        }

      

        private void ProcessMessage(string response)
        {
            // 💡 [빈 깡통 입구컷]
            if (string.IsNullOrWhiteSpace(response)) return;

            // ==========================================================
            // 💡 1. [긴급 검사] 웹소켓 토큰 만료 에러(8005) 원천 차단
            // JSON 파싱 전에 문자열 자체에서 빠르게 낚아챕니다!
            // ==========================================================
            if (response.Contains("8005") && response.Contains("유효하지 않습니다"))
            {
                Form1.Console_print(">> [웹소켓] 접근 토큰이 만료되었습니다! (에러 8005)");
                Log.에러기록("[웹소켓 토큰 만료] 서버로부터 8005 에러 수신. 재연결을 시도합니다.");

                Form1.중복접속 = false;
                Helper.안전종료하기();
                return; // 에러 메시지이므로 더 이상 아래(JSON 파싱)로 내려가지 않고 즉시 컷!
            }

            // ==========================================================
            // 💡 2. 정상적인 데이터 (REAL, PING 등) JSON 파싱 및 처리
            // ==========================================================
            try
            {
                using (JsonDocument doc = JsonDocument.Parse(response))
                {
                    JsonElement root = doc.RootElement;
                    string trName = GetSafeString(root, "trnm");

                    if (string.IsNullOrEmpty(trName)) return;

                    if (trName.Equals("REAL"))
                    {
                        _dispatcher.ProcessRealData(root.Clone());
                        return;
                    }

                    if (trName.Equals("PING"))
                    {
                        // 💡 서버 통신 생존 신고 도장 쾅!
                        Form1.서버_마지막_통신시간 = DateTime.Now;
                        return;
                    }

                    HandleGeneralTR(trName, root);
                }
            }
            catch (Exception ex)
            {
                Form1.Console_print($"JSON Processing Error: {ex.Message}");
            }
        }

        // [핵심 헬퍼] 숫자/문자 상관없이 안전하게 string으로 변환
        private string GetSafeString(JsonElement element, string key)
        {
            if (element.TryGetProperty(key, out JsonElement prop))
            {
                if (prop.ValueKind == JsonValueKind.String) return prop.GetString();
                if (prop.ValueKind == JsonValueKind.Number) return prop.ToString();
                return prop.ToString(); // Null, True/False 등도 문자열로 변환
            }
            return "";
        }

        private void HandleGeneralTR(string trName, JsonElement root)
        {
            // 1. 검색식 목록 조회 (CNSRLST)
            if (trName.Equals("CNSRLST"))
            {
                Form1.ConditionList.Clear();

                void con_add(bool checke, string name)
                {
                    if (checke)
                    {
                        Condition item = Form1.ConditionList.Find(o => o.name.Equals(name));
                        // 💡 아빠님이 로컬용으로 만드신 가짜 인덱스 "index"
                        if (item == null) Form1.ConditionList.Add(new Condition("index", name));
                    }
                }

                con_add(GenieConfig.CB_매수탐색A, "매수탐색_A");
                con_add(GenieConfig.CB_매수탐색B, "매수탐색_B");
                con_add(GenieConfig.CB_매도탐색, "매도탐색");
                if (Form1.내아이디) con_add(true, "랭킹분석");

                if (root.TryGetProperty("data", out JsonElement dataArray) && dataArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement item in dataArray.EnumerateArray())
                    {
                        string index = (item[0].ValueKind == JsonValueKind.Number) ? item[0].ToString() : item[0].GetString();
                        string name = (item[1].ValueKind == JsonValueKind.Number) ? item[1].ToString() : item[1].GetString();

                        Form1.Console_print($"검색식 추가  ---------index: {index} name: {name}");
                        Form1.ConditionList.Add(new Condition(index, name));
                    }
                }

                if (Form1.로딩완료)
                {
                    if (Form1.MBC_sender == "BT_condition_loading")
                    {
                        Form1.MBC_sender = "";
                        Form1.AutoClosingAlram("키움서버로 부터 검색식을 업데이트 받았습니다.", "검색식로딩", 5, "동작");
                        return; // 검색식 로딩 버튼에서 온 응답이면, 재연결 때처럼 일괄 등록하지 않고 여기서 바로 종료합니다.
                    }

                    if (Form1.MBC_sender == "재연결")
                    {
                        Form1.MBC_sender = "";

                        Form1.Console_print(">> 실시간 조회 로딩시작 2단계 업종실시간등록, 실시간체결_주식호가잔량_주식예상체결_신규등록 요청 >>>");

                        // 기존에 보던 데이터들 다시 요청
                        REG.업종실시간등록();

                        foreach (var item in Form1.form1.실시간요청코드_List)
                        {
                            REG.실시간체결_주식호가잔량_주식예상체결_신규등록(item);
                        }

                        foreach (var item in Form1.Run_condition_List)
                        {
                            if (string.IsNullOrWhiteSpace(item.index) || item.index == "index")
                            {
                            //    Form1.Console_print($"⚠️ [경고] 서버용 조건식이 아니거나 번호가 없습니다. ({item.name}) -> 등록 스킵!");
                                continue;
                            }

                            REG.검색요청(item.index);
                        }
                    }
                }
                else
                {
                    REG.핑_10초_반복시작();
                    Form1.매매시작 = "Loding_01_코스피리스트요청";
                }

                Form1.Console_print($"검색식 목록 조회 완료 --------------- 매매시작:{Form1.매매시작} MBC_sender:{Form1.MBC_sender}");
            }

            // 2. 검색식 실시간 등록 응답 (CNSRREQ)
            else if (trName.Equals("CNSRREQ"))
            {
                string returnCode = GetSafeString(root, "return_code");
                string seq = GetSafeString(root, "seq");

                Form1.Console_print("검색식을 실시간 등록 -------------------");
                Form1.Console_print("[검색식] 결과코드 : " + returnCode);
                Form1.Console_print("[검색식] 조건검색식 일련번호 : " + seq);

                if (returnCode == "0") // 성공
                {
                    if (root.TryGetProperty("data", out JsonElement dataArray) && dataArray.ValueKind == JsonValueKind.Array && dataArray.GetArrayLength() > 0)
                    {
                        Form1.Console_print("[검색결과데이터 ] -------------------");

                        foreach (JsonElement item in dataArray.EnumerateArray())
                        {
                            string jmCode = GetSafeString(item, "jmcode");
                            Condition_Management.API_OnReceiveTRCondition(jmCode, seq);
                        }

                    }
                }
                else // 실패
                {
                    string returnMsg = GetSafeString(root, "return_msg");
                    Form1.Console_print("[검색식 등록 실패] 결과메시지 : " + returnMsg);

                    // ====================================================================
                    // 🛡️ [900004 입구컷 방어막 2] 실패해서 재요청할 때도 "index" 같은 쓰레기 값이면 재요청 금지!
                    // ====================================================================
                    if (!string.IsNullOrWhiteSpace(seq) && seq != "index" && returnCode != "900003")
                    {
                        REG.검색요청(seq);
                    }
                }
            }
            // 3. 검색식 실시간 해제 응답 (CNSRCLR)
            else if (trName.Equals("CNSRCLR"))
            {
                // [안전 변환 적용]
                string returnCode = GetSafeString(root, "return_code");

                if (returnCode != "0")
                {
                    string returnMsg = GetSafeString(root, "return_msg");
                    string seq = GetSafeString(root, "seq");

                    Form1.Console_print("[검색식 해제 실패] 결과메시지 : " + returnMsg);
                    if (!returnMsg.Contains("등록되지않은"))
                    {
                        REG.검색해제(seq);
                    }
                }
            }
          
        }
    }
}





