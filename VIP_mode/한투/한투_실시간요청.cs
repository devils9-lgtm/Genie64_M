using System;
using System.Threading.Tasks;

namespace 지니64
{
    public class 한투_실시간요청
    {
        // 💡 [저사양 PC 최적화] 하드코딩된 TR_ID 및 변수들을 캐싱
        private const string TR_ID_REAL = "H0STCNI0"; // 실전 체결통보
        private const string TR_ID_MOCK = "H0STCNI9"; // 모의 체결통보

        /// <summary>
        /// 실시간 체결통보 등록 (tr_type: "1")
        /// </summary>
        /// <param name="approval_key">웹소켓 접속용 발급 키 (접속 토큰과 다름)</param>
        /// <param name="hts_id">개인 HTS ID</param>
        /// <param name="isMock">모의투자 여부 (true/false)</param>
        public static async Task 체결통보_등록(string approval_key, string hts_id, bool isMock)
        {
            // ====================================================================
            // 💡 [핵심 추가] 웹소켓 연결 확인 및 자동 접속 로직
            // 데이터 요청 전, 웹소켓이 안 열려있다면 ConnectAsync를 먼저 실행합니다!
            // ====================================================================
            if (!한투_웹소켓연결.connected || 한투_웹소켓연결.websocket == null || 한투_웹소켓연결.websocket.State != System.Net.WebSockets.WebSocketState.Open)
            {
                Form1.Console_print(">> [웹소켓 미연결 감지] 체결통보 요청 전, 서버 접속을 먼저 진행합니다.");

                await 한투_웹소켓연결.ConnectAsync(isMock);

                // 서버 접속 후 통신 안정화를 위해 0.5초 대기
                await Task.Delay(500);
            }

            string targetTrId = isMock ? TR_ID_MOCK : TR_ID_REAL;

            // 한투 웹소켓 요청 표준 JSON 포맷
            var requestObj = new
            {
                header = new
                {
                    approval_key = approval_key,
                    custtype = "P",         // P: 개인, B: 법인
                    tr_type = "1",          // 1: 등록(Subscribe)
                    content_type = "utf-8"
                },
                body = new
                {
                    input = new
                    {
                        tr_id = targetTrId,
                        tr_key = hts_id     // 체결통보 시에는 HTS ID를 입력합니다.
                    }
                }
            };

            await 한투_웹소켓연결.SendMessageAsync(requestObj);
            Form1.Console_print($">> [한투] 체결통보(등록) 요청 완료! (HTS_ID: {hts_id})");
        }

        /// <summary>
        /// 실시간 체결통보 해제 (tr_type: "2")
        /// </summary>
        public static async Task 체결통보_해제(string approval_key, string hts_id, bool isMock)
        {
            string targetTrId = isMock ? TR_ID_MOCK : TR_ID_REAL;

            var requestObj = new
            {
                header = new
                {
                    approval_key = approval_key,
                    custtype = "P",
                    tr_type = "2",          // 2: 해제(Unsubscribe)
                    content_type = "utf-8"
                },
                body = new
                {
                    input = new
                    {
                        tr_id = targetTrId,
                        tr_key = hts_id
                    }
                }
            };

            await 한투_웹소켓연결.SendMessageAsync(requestObj);
            Form1.Console_print($">> [한투] 체결통보(해제) 요청 완료! (HTS_ID: {hts_id})");
        }
    }
}