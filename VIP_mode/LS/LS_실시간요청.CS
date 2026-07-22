using System;
using System.Threading.Tasks;

namespace 지니64
{
    public class LS_실시간요청
    {
        // 💡 [저사양 PC 최적화] 하드코딩된 TR_ID 및 변수들을 캐싱
        private const string TR_CD_ORDER = "SC0"; // LS증권 주식주문접수(체결통보) TR코드

        /// <summary>
        /// 실시간 체결통보(계좌) 등록 (tr_type: "1")
        /// </summary>
        /// <param name="token">웹소켓 접속용 발급 토큰 (Access Token)</param>
        /// <param name="tr_key">단축코드 (계좌등록/해제일 경우 필수 아님, 보통 "" 빈 문자열 전송)</param>
        /// <param name="isMock">모의투자 여부 (true/false)</param>
        public static async Task 체결통보_등록(string token, string tr_key, bool isMock)
        {
            // ====================================================================
            // 💡 [핵심 추가] 웹소켓 연결 확인 및 자동 접속 로직
            // 데이터 요청 전, 웹소켓이 안 열려있다면 ConnectAsync를 먼저 실행합니다!
            // ====================================================================
            if (!LS_웹소켓연결.connected || LS_웹소켓연결.websocket == null || LS_웹소켓연결.websocket.State != System.Net.WebSockets.WebSocketState.Open)
            {
                Form1.Console_print(">> [웹소켓 미연결 감지] 체결통보 요청 전, 서버 접속을 먼저 진행합니다.");

                // LS증권은 ConnectAsync에서 URL로 실전/모의투자를 구분합니다.
                await LS_웹소켓연결.ConnectAsync(isMock);

                // 서버 접속 후 통신 안정화를 위해 0.5초 대기
                await Task.Delay(500);
            }

            // LS증권 웹소켓 요청 표준 JSON 포맷
            var requestObj = new
            {
                header = new
                {
                    token = token,
                    tr_type = "1"          // 1: 계좌등록(Subscribe)
                },
                body = new
                {
                    tr_cd = TR_CD_ORDER,   // "SC0"
                    tr_key = tr_key        // 보통 빈 문자열("") 사용
                }
            };

            await LS_웹소켓연결.SendMessageAsync(requestObj);
            Form1.Console_print($">> [LS증권] 체결통보(등록) 요청 완료! (TR_KEY: {tr_key})");
        }

        /// <summary>
        /// 실시간 체결통보(계좌) 해제 (tr_type: "2")
        /// </summary>
        public static async Task 체결통보_해제(string token, string tr_key, bool isMock)
        {
            // LS증권 웹소켓 해제 표준 JSON 포맷
            var requestObj = new
            {
                header = new
                {
                    token = token,
                    tr_type = "2"          // 2: 계좌해제(Unsubscribe)
                },
                body = new
                {
                    tr_cd = TR_CD_ORDER,   // "SC0"
                    tr_key = tr_key
                }
            };

            await LS_웹소켓연결.SendMessageAsync(requestObj);
            Form1.Console_print($">> [LS증권] 체결통보(해제) 요청 완료! (TR_KEY: {tr_key})");
        }
    }
}