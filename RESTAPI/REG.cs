//using System;
//using System.Collections.Generic;
//using System.Net.WebSockets;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace 지니64
//{
//    public class REG
//    {


//        public static async Task Websoketconnect(string where)
//        {
//            // ====================================================================
//            // 🛡️ [좀비 방어막] 프로그램이 종료 중이라면, 그 어떤 접속 시도도 즉시 거절!
//            // ====================================================================
//            if (Form1.프로그램종료중)
//            {
//                Form1.Console_print(">> 🛑 시스템 종료 중이므로 웹소켓 접속을 시도하지 않습니다.");
//                return;
//            }

//            // 💡 [안전한 재시도 로직] 연결에 성공할 때까지 3초 간격으로 계속 문을 두드립니다.
//            while (true)
//            {
//                try
//                {
//                    if (Form1.websocketClient == null)
//                    {
//                        Form1.websocketClient = new WebSocketClient();
//                    }

//                    // 접속 시도
//                    await Form1.websocketClient.ConnectAsync();

//                    // 여기까지 에러 없이 내려왔다면 연결 성공! while 무한루프 탈출!
//                    break;
//                }
//                catch (Exception ex)
//                {
//                    Form1.Console_print($">> 웹소켓 접속 실패. 3초 뒤 다시 시도합니다. ({ex.Message})");
//                    Form1.websocketClient = null; // 찌꺼기 비우기
//                    await Task.Delay(3000); // 서버에 무리가 가지 않도록 3초 대기 (이게 핵심입니다!)
//                }
//            }

//            // ==========================================================
//            // 여기서부터는 웹소켓이 완벽하게 연결된(Open) 상태입니다.
//            // ==========================================================
//            Form1.Console_print(">> 웹소켓 서버 접속 성공! 데이터 수신을 시작합니다.");

//            // 메시지 수신 대기 시작
//            _ = Form1.websocketClient.ListenAsync();

//            if (where == "재연결")
//            {
//                Form1.MBC_sender = "재연결";

//                Form1.Console_print(">> 재연결 후 실시간 데이터 및 조건식 복구 완료!");
//                Form1.Console_print("> 실시간 조회 로딩시작 1단계 검색식 목록조회 시작 >>>");
//                REG.목록조회_재연결();  // 사용자 조건식 불러오기

//            }


//        }

//        public static void 검색식목록조회_로딩()
//        {
//            Form1.tr_scheduler.EnqueuePriorityRequest("CNSRLST", async () =>
//            {
//                await Websoketconnect("목록조회");

//                await Task.Delay(1000);

//                await Form1.websocketClient.SendMessageAsync(new
//                {
//                    trnm = "CNSRLST"
//                });
//            });
//        }

//        public static void 검색식목록조회_재조회()
//        {
//            Form1.tr_scheduler.EnqueuePriorityRequest("CNSRLST", async () =>
//            {
//                await Task.Delay(1000);

//                await Form1.websocketClient.SendMessageAsync(new
//                {
//                    trnm = "CNSRLST"
//                });
//            });
//        }

//        public static void 목록조회_재연결()
//        {
//            Form1.tr_scheduler.EnqueuePriorityRequest("CNSRLST", async () =>
//            {
//                await Task.Delay(1000);

//                await Form1.websocketClient.SendMessageAsync(new
//                {
//                    trnm = "CNSRLST"
//                });
//            });
//        }

//        public static void 검색요청(string 일련번호)
//        {
//            Form1.tr_scheduler.EnqueuePriorityRequest("CNSRREQ", async () =>
//            {
//                await Form1.websocketClient.SendMessageAsync(new
//                {
//                    trnm = "CNSRREQ", // 서비스명
//                    seq = 일련번호, //  조건검색식 일련번호
//                    search_type = 1, //  조회타입 1: 조건검색+실시간조건검색
//                    stex_tp = "K" // 거래소구분
//                });
//            });
//        }

//        public static void 검색해제(string 일련번호)
//        {
//            Form1.tr_scheduler.EnqueuePriorityRequest("CNSRCLR", async () =>
//            {
//                await Form1.websocketClient.SendMessageAsync(new
//                {
//                    trnm = "CNSRCLR", // 서비스명
//                    seq = 일련번호, //  조건검색식 일련번호
//                });
//            });
//        }

//        public static void 업종실시간등록()
//        {
//            업종지수("001");
//            업종지수("101");
//            업종등락("001");
//            업종등락("101");
//            주문체결();
//            잔고();

//             Form1.Console_print("업종 실시간 등록 완료 -------------------");
//        }


//        public static void 업종지수(string Code)
//        {
//            Form1.tr_scheduler.EnqueuePriorityRequest("REG", async () =>
//            {
//                await Form1.websocketClient.SendMessageAsync(new
//                {
//                    trnm = "REG", // 서비스명
//                    grp_no = 1, // 그룹번호
//                    refresh = 1, // 기존등록유지여부
//                    data = new[]
//                      {
//                       new
//                       {
//                           item = new[] { Code }, // 실시간 등록 요소
//                           type = new[] { "0J" } // 실시간 항목
//                       }
//                    }
//                });
//            });
//        }

//        public static void 업종등락(string Code)
//        {
//            Form1.tr_scheduler.EnqueuePriorityRequest("0U", async () =>
//            {
//                await Form1.websocketClient.SendMessageAsync(new
//                {
//                    trnm = "REG", // 서비스명
//                    grp_no = 1, // 그룹번호
//                    refresh = 1, // 기존등록유지여부
//                    data = new[]
//                  {
//                        new
//                        {
//                            item = new[] { Code }, // 실시간 등록 요소
//                            type = new[] { "0U" } // 실시간 항목
//                        }
//                    }
//                });
//            });
//        }

//        public static void 주문체결()
//        {
//            Form1.tr_scheduler.EnqueuePriorityRequest("00", async () =>
//            {
//                await Form1.websocketClient.SendMessageAsync(new
//                {
//                    trnm = "REG", // 서비스명
//                    grp_no = 1, // 그룹번호
//                    refresh = 1, // 기존등록유지여부
//                    data = new[]
//                   {
//                        new
//                        {
//                            item = new[] { "" }, // 실시간 등록 요소
//                            type = new[] { "00" } // 실시간 항목
//                        }
//                    }
//                });
//            });
//        }

//        public static void 잔고()
//        {
//            // 1. 스케줄러 확인
//            if (Form1.tr_scheduler == null)
//            {
//                 Form1.Console_print("오류: Form1.tr_scheduler가 아직 생성되지 않았습니다 (null).");
//                return; // 더 이상 진행하지 않고 종료
//            }

//            Form1.tr_scheduler.EnqueuePriorityRequest("04", async () =>
//            {
//                // 2. 웹소켓 클라이언트 확인
//                if (Form1.websocketClient == null)
//                {
//                     Form1.Console_print("오류: Form1.websocketClient가 아직 연결되지 않았거나 null입니다.");
//                    return; // 전송 중단
//                }

//                try
//                {
//                    await Form1.websocketClient.SendMessageAsync(new
//                    {
//                        trnm = "REG",
//                        grp_no = 1,
//                        refresh = 1,
//                        data = new[]
//                        {
//                    new
//                    {
//                        item = new[] { "" },
//                        type = new[] { "04" }
//                    }
//                }
//                    });
//                }
//                catch (Exception ex)
//                {
//                     Form1.Console_print($"메시지 전송 중 오류 발생: {ex.Message}");
//                }
//            });
//        }

//        // 1. 순서를 유지하기 위해 List로 변경 (또는 List와 HashSet 병행)
//        public List<string> 실시간요청코드_List = new List<string>();

//        public static void 실시간시세등록(string itemcode)
//        {
//            string code = itemcode;

//            // [Step 1] 이미 등록된 코드라면 중단
//            if (Form1.form1.실시간요청코드_List.Contains(code)) return;

//            // [Step 2] 100개가 꽉 찼다면 교체 대상 찾기
//            if (Form1.form1.실시간요청코드_List.Count >= 100)
//            {
//                string 삭제대상코드 = "";

//                // 앞에서부터 순회 (먼저 들어온 순서)
//                for (int i = 0; i < Form1.form1.실시간요청코드_List.Count; i++)
//                {
//                    string 대상 = Form1.form1.실시간요청코드_List[i];

//                    bool 잔고에있는가 = Form1.stockBalanceList.ContainsKey(대상);
//                    if (!잔고에있는가)
//                    {
//                        삭제대상코드 = 대상;
//                        break; // 가장 오래된 '잔고 외 종목'을 찾았으므로 루프 탈출
//                    }
//                }

//                if (!string.IsNullOrEmpty(삭제대상코드))
//                {
//                    Form1.form1.실시간요청코드_List.Remove(삭제대상코드);
//                    실시간체결_주식호가잔량_주식예상체결_해제(삭제대상코드);
//                }
//                else
//                {
//                    // 만약 100개 전부가 잔고 종목이라 지울 게 없다면... 
//                    // 이 경우 신규 등록을 포기하거나, 정책상 가장 오래된 것을 강제로 지워야 해.
//                    Form1.Console_print("실시간 슬롯 부족: 100개 모두 잔고 종목입니다.");
//                    return;
//                }
//            }

//            // [Step 4] 신규 종목 등록
//            실시간체결_주식호가잔량_주식예상체결_신규등록(code);

//            Form1.form1.실시간요청코드_List.Add(code);
//        }

//        public static void 실시간체결_주식호가잔량_주식예상체결_신규등록(string Code)
//        {
//            Form1.tr_scheduler.EnqueuePriorityRequest("0B", async () =>
//            {
//                await Form1.websocketClient.SendMessageAsync(new
//                {
//                    trnm = "REG", // REG : 등록 , REMOVE : 해지
//                    grp_no = 1, // 그룹번호
//                    refresh = 1, // 등록(REG)시 0:기존유지안함 1:기존유지(Default) 0일경우 기존등록한 item/type은 해지, 1일경우 기존등록한 item/type 유지해지(REMOVE)시 값 불필요
//                    data = new[]
//                    {
//                           new
//                           {
//                               item = new[] { Code + "_AL" }, // 실시간 등록 요소
//                               type = new[] { "0B" ,"0D" , "0H" } // 실시간 항목
//                           }
//                       }
//                   });
//            });
//        }

//        public static void 실시간체결_주식호가잔량_주식예상체결_해제(string Code)
//        {
//            // 해제 요청도 우선순위 큐에 넣어서 처리 (안정성 확보)
//            Form1.tr_scheduler.EnqueuePriorityRequest("REMOVE", async () =>
//            {
//                await Form1.websocketClient.SendMessageAsync(new
//                {
//                    trnm = "REMOVE", // [핵심] 등록(REG) -> 해지(REMOVE)로 변경
//                    grp_no = 1,      // 등록했던 그룹 번호와 동일하게
//                    refresh = 0,     // 해지 시에는 값 불필요 (보통 0으로 전송)
//                    data = new[]
//                    {
//                       new
//                       {
//                           item = new[] { Code + "_AL" }, 
//                           type = new[] { "0B", "0D", "0H" }
//                       }
//                    }
//                });
//            });
//        }



//        // 💡 [핵심 방어막 1] 다중 스레드(송신, 수신)에서 동시에 쳐들어와도 절대 뚫리지 않는 강제 락(Lock) 객체
//        private static readonly object _reconnectLock = new object();
//        private static bool 재연결_진행중 = false;

//        public static void 재연결(string message)
//        {
//            // 💡 [좀비 방지 입구 컷] 프로그램이 이미 닫히는 중이라면 심폐소생술(재연결) 절대 금지!!
//            if (Form1.프로그램종료중)
//            {
//                Form1.Console_print(">> 🛑 시스템 종료 중이므로 재연결을 시도하지 않습니다.");
//                return;
//            }

//            // 💡 [입구 컷] 로딩 전이거나, 중복 접속으로 봇이 '안전 종료' 중일 때는 재연결 시도 자체를 무시!
//            if (!Form1.로딩완료 || Form1.중복접속) return;

//            // 💡 [스레드 세이프 락] 0.001초 차이로 재연결 요청이 여러 개 들어와도, 무조건 한 놈만 통과시킵니다.
//            lock (_reconnectLock)
//            {
//                if (재연결_진행중) return; // 이미 누가 진행 중이면 조용히 퇴근

//                // 💡 무거운 ToString() 문자 비교를 버리고, C# 기본 상태(Enum)로 0.00초 만에 다이렉트 비교!
//                // 소켓이 살아있으면 굳이 끊을 필요 없음
//                if (Form1.websocket != null && Form1.websocket.State == WebSocketState.Open) return;

//                재연결_진행중 = true; // 자물쇠 찰칵!
//            }

//            // =========================================================
//            // 💡 여기서부터는 우주가 멸망해도 단 1번만 실행됨이 보장됩니다.
//            // =========================================================
//            Form1.Console_print(message);
//            Form1.Console_print(">> 기존 소켓 해제 및 자동 재연결 프로세스를 시작합니다...");

//            Form1.connected = false;

//            // 💡 [찌꺼기 청소] 기존 뻗어버린 소켓의 숨통을 확실히 끊고 메모리에서 날려버립니다 (메모리 누수 완벽 방지)
//            try
//            {
//                if (Form1.websocket != null)
//                {
//                    Form1.websocket.Abort(); // 강제 종료
//                    Form1.websocket.Dispose(); // 메모리 반환
//                }
//            }
//            catch { }
//            finally
//            {
//                Form1.websocket = null;
//                Form1.websocketClient = null; // 클라이언트 껍데기도 완전 초기화
//            }

//            // 💡 [최우선 스케줄링] 봇이 하던 다른 매매 작업을 멈추고 연결부터 최우선으로 살려냅니다.
//            Form1.tr_scheduler.EnqueuePriorityRequest("재연결", async () =>
//            {
//                try
//                {
//                    // 💡 [신의 한 수] 서버가 끊겼을 때 바로 1초 만에 때리면 증권사 서버가 "너 디도스(DDoS) 공격하냐?" 
//                    // 하고 IP를 차단하거나 거절할 수 있습니다. 1.5초 정도 숨을 고르고 우아하게 연결합니다.
//                    await Task.Delay(1500);

//                    await Websoketconnect("재연결");
//                }
//                finally
//                {
//                    // 💡 [필수 안전장치] 연결에 성공하든, 또 에러가 나서 실패하든 무조건 자물쇠를 풀어줘야
//                    // 다음번에 다시 재연결을 시도할 수 있습니다! (영원히 봇이 굳어버리는 데드락 방지)
//                    재연결_진행중 = false;
//                }
//            });
//        }


//        // 10초 반복을 안전하게 멈추기 위한 토큰
//        private static CancellationTokenSource cts_핑;

//        // =================================================================
//        // 1. PING 10초 반복 시작 (프로그램 로딩 완료 시점에 1번만 호출!)
//        // =================================================================
//        public static void 핑_10초_반복시작()
//        {
//            // 이미 실행 중이면 중복 실행 방지
//            if (cts_핑 != null) return;

//            cts_핑 = new CancellationTokenSource();
//            Form1.Console_print("[웹소켓] PING(하트비트) 10초 반복 전송을 시작합니다.");

//            // 백그라운드에서 UI를 멈추지 않고 10초마다 핑을 날립니다.
//            Task.Run(async () =>
//            {
//                while (!cts_핑.IsCancellationRequested)
//                {
//                    // 아빠님이 만드신 기존 PING 함수 호출
//                    await 핑();

//                    // 정확히 10초(10000 밀리초) 대기
//                    try
//                    {
//                        await Task.Delay(10000, cts_핑.Token);
//                    }
//                    catch (TaskCanceledException)
//                    {
//                        // 취소 토큰이 작동하면 자연스럽게 루프 종료
//                        break;
//                    }
//                }
//            }, cts_핑.Token);
//        }

//        // =================================================================
//        // 2. PING 10초 반복 중지 (프로그램 종료 시 호출)
//        // =================================================================
//        public static void 핑_반복중지()
//        {
//            if (cts_핑 != null)
//            {
//                cts_핑.Cancel();
//                cts_핑.Dispose();
//                cts_핑 = null;
//                Form1.Console_print("[웹소켓] PING(하트비트) 전송이 중지되었습니다.");
//            }
//        }

//        // =================================================================
//        // (수정 완료) 스케줄러를 무시하고 즉시 PING을 쏘는 VIP 함수
//        // =================================================================
//        public static async Task 핑()
//        {
//            // ====================================================================
//            // 🛡️ [입구컷 1] 봇이 퇴근(종료) 중이면 핑이고 뭐고 즉시 퇴근!
//            // ====================================================================
//            if (Form1.프로그램종료중) return;

//            // ====================================================================
//            // 🛡️ [입구컷 2] 소켓이 죽어있거나 연결이 끊겼다면 허공에 핑을 쏘지 않습니다!
//            // ====================================================================
//            if (!Form1.connected || Form1.websocketClient == null || Form1.websocket == null || Form1.websocket.State != System.Net.WebSockets.WebSocketState.Open)
//            {
//                return;
//            }

//            // ====================================================================
//            // 💡 [VIP 프리패스] 로딩 중이든 완료됐든, 큐(tr_scheduler)에 줄 서지 않고 
//            // 새로 만든 고속 발송 함수(SendPingAsync)를 통해 무조건 즉시 발송합니다!
//            // ====================================================================
//            await Form1.websocketClient.SendPingAsync(new
//            {
//                trnm = "PING"
//            });
//        }




//    }

//}

using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64
{
    public class REG
    {
        // ====================================================================
        // 💡 [저사양 PC 메모리/CPU 최적화 변수] 
        // 빈번하게 전송되는 고정 배열을 캐싱하여, 가비지 컬렉터(GC) 호출로 인한 
        // 시스템 부하 및 순간적인 렉 현상을 원천 차단합니다.
        // ====================================================================
        private static readonly string[] _type_0J = new[] { "0J" };
        private static readonly string[] _type_0U = new[] { "0U" };
        private static readonly string[] _type_00 = new[] { "00" };
        private static readonly string[] _type_04 = new[] { "04" };
        private static readonly string[] _type_Realtime = new[] { "0B", "0D", "0H" };
        private static readonly string[] _item_Empty = new[] { "" };


        public static async Task Websoketconnect(string where)
        {
            // ====================================================================
            // 🛡️ [좀비 방어막] 프로그램이 종료 중이라면, 그 어떤 접속 시도도 즉시 거절!
            // ====================================================================
            if (Form1.프로그램종료중)
            {
                Form1.Console_print(">> 🛑 시스템 종료 중이므로 웹소켓 접속을 시도하지 않습니다.");
                return;
            }

            // 💡 [안전한 재시도 로직] 연결에 성공할 때까지 3초 간격으로 계속 문을 두드립니다.
            while (true)
            {
                try
                {
                    if (Form1.websocketClient == null)
                    {
                        Form1.websocketClient = new WebSocketClient();
                    }

                    // 접속 시도
                    await Form1.websocketClient.ConnectAsync();

                    // 여기까지 에러 없이 내려왔다면 연결 성공! while 무한루프 탈출!
                    break;
                }
                catch (Exception ex)
                {
                    Form1.Console_print($">> 웹소켓 접속 실패. 3초 뒤 다시 시도합니다. ({ex.Message})");
                    Form1.websocketClient = null; // 찌꺼기 비우기
                    await Task.Delay(3000); // 서버에 무리가 가지 않도록 3초 대기 (이게 핵심입니다!)
                }
            }

            // ==========================================================
            // 여기서부터는 웹소켓이 완벽하게 연결된(Open) 상태입니다.
            // ==========================================================
            Form1.Console_print(">> 웹소켓 서버 접속 성공! 데이터 수신을 시작합니다.");

            // 메시지 수신 대기 시작
            _ = Form1.websocketClient.ListenAsync();

            if (where == "재연결")
            {
                Form1.MBC_sender = "재연결";

                Form1.Console_print(">> 재연결 후 실시간 데이터 및 조건식 복구 완료!");
                Form1.Console_print("> 실시간 조회 로딩시작 1단계 검색식 목록조회 시작 >>>");
                REG.목록조회_재연결();  // 사용자 조건식 불러오기

            }
        }

        public static void 검색식목록조회_로딩()
        {
            Form1.tr_scheduler.EnqueuePriorityRequest("CNSRLST", async () =>
            {
                await Websoketconnect("목록조회");

                await Task.Delay(1000);

                await Form1.websocketClient.SendMessageAsync(new
                {
                    trnm = "CNSRLST"
                });
            });
        }

        public static void 검색식목록조회_재조회()
        {
            Form1.tr_scheduler.EnqueuePriorityRequest("CNSRLST", async () =>
            {
                await Task.Delay(1000);

                await Form1.websocketClient.SendMessageAsync(new
                {
                    trnm = "CNSRLST"
                });
            });
        }

        public static void 목록조회_재연결()
        {
            Form1.tr_scheduler.EnqueuePriorityRequest("CNSRLST", async () =>
            {
                await Task.Delay(1000);

                await Form1.websocketClient.SendMessageAsync(new
                {
                    trnm = "CNSRLST"
                });
            });
        }

        public static void 검색요청(string 일련번호)
        {
            Form1.tr_scheduler.EnqueuePriorityRequest("CNSRREQ", async () =>
            {
                await Form1.websocketClient.SendMessageAsync(new
                {
                    trnm = "CNSRREQ", // 서비스명
                    seq = 일련번호, //  조건검색식 일련번호
                    search_type = 1, //  조회타입 1: 조건검색+실시간조건검색
                    stex_tp = "K" // 거래소구분
                });
            });
        }

        public static void 검색해제(string 일련번호)
        {
            Form1.tr_scheduler.EnqueuePriorityRequest("CNSRCLR", async () =>
            {
                await Form1.websocketClient.SendMessageAsync(new
                {
                    trnm = "CNSRCLR", // 서비스명
                    seq = 일련번호, //  조건검색식 일련번호
                });
            });
        }

        public static void 업종실시간등록()
        {
            업종지수("001");
            업종지수("101");
            업종등락("001");
            업종등락("101");
            주문체결();
            잔고();

            Form1.Console_print("업종 실시간 등록 완료 -------------------");
        }


        public static void 업종지수(string Code)
        {
            Form1.tr_scheduler.EnqueuePriorityRequest("REG", async () =>
            {
                await Form1.websocketClient.SendMessageAsync(new
                {
                    trnm = "REG", // 서비스명
                    grp_no = 1, // 그룹번호
                    refresh = 1, // 기존등록유지여부
                    data = new[]
                      {
                       new
                       {
                           item = new[] { Code }, // 실시간 등록 요소
                           type = _type_0J // [최적화] 고정 배열 재사용
                       }
                    }
                });
            });
        }

        public static void 업종등락(string Code)
        {
            Form1.tr_scheduler.EnqueuePriorityRequest("0U", async () =>
            {
                await Form1.websocketClient.SendMessageAsync(new
                {
                    trnm = "REG", // 서비스명
                    grp_no = 1, // 그룹번호
                    refresh = 1, // 기존등록유지여부
                    data = new[]
                  {
                        new
                        {
                            item = new[] { Code }, // 실시간 등록 요소
                            type = _type_0U // [최적화] 고정 배열 재사용
                        }
                    }
                });
            });
        }

        public static void 주문체결()
        {
            Form1.tr_scheduler.EnqueuePriorityRequest("00", async () =>
            {
                await Form1.websocketClient.SendMessageAsync(new
                {
                    trnm = "REG", // 서비스명
                    grp_no = 1, // 그룹번호
                    refresh = 1, // 기존등록유지여부
                    data = new[]
                   {
                        new
                        {
                            item = _item_Empty, // [최적화] 고정 배열 재사용
                            type = _type_00 // [최적화] 고정 배열 재사용
                        }
                    }
                });
            });
        }

        public static void 잔고()
        {
            // 1. 스케줄러 확인
            if (Form1.tr_scheduler == null)
            {
                Form1.Console_print("오류: Form1.tr_scheduler가 아직 생성되지 않았습니다 (null).");
                return; // 더 이상 진행하지 않고 종료
            }

            Form1.tr_scheduler.EnqueuePriorityRequest("04", async () =>
            {
                // 2. 웹소켓 클라이언트 확인
                if (Form1.websocketClient == null)
                {
                    Form1.Console_print("오류: Form1.websocketClient가 아직 연결되지 않았거나 null입니다.");
                    return; // 전송 중단
                }

                try
                {
                    await Form1.websocketClient.SendMessageAsync(new
                    {
                        trnm = "REG",
                        grp_no = 1,
                        refresh = 1,
                        data = new[]
                        {
                    new
                    {
                        item = _item_Empty, // [최적화] 고정 배열 재사용
                        type = _type_04 // [최적화] 고정 배열 재사용
                    }
                }
                    });
                }
                catch (Exception ex)
                {
                    Form1.Console_print($"메시지 전송 중 오류 발생: {ex.Message}");
                }
            });
        }

        // 1. 순서를 유지하기 위해 List로 변경 (또는 List와 HashSet 병행)
        public List<string> 실시간요청코드_List = new List<string>();

        public static void 실시간시세등록(string itemcode)
        {
            string code = itemcode;

            // [Step 1] 이미 등록된 코드라면 중단
            if (Form1.form1.실시간요청코드_List.Contains(code)) return;

            // [최적화] 반복적인 Property(Count) 접근을 줄이기 위해 로컬 변수에 캐싱 (CPU 절약)
            int listCount = Form1.form1.실시간요청코드_List.Count;

            // [Step 2] 100개가 꽉 찼다면 교체 대상 찾기
            if (listCount >= 100)
            {
                string 삭제대상코드 = "";

                // 앞에서부터 순회 (먼저 들어온 순서)
                for (int i = 0; i < listCount; i++)
                {
                    string 대상 = Form1.form1.실시간요청코드_List[i];

                    bool 잔고에있는가 = Form1.stockBalanceList.ContainsKey(대상);
                    if (!잔고에있는가)
                    {
                        삭제대상코드 = 대상;
                        break; // 가장 오래된 '잔고 외 종목'을 찾았으므로 루프 탈출
                    }
                }

                if (!string.IsNullOrEmpty(삭제대상코드))
                {
                    Form1.form1.실시간요청코드_List.Remove(삭제대상코드);
                    실시간체결_주식호가잔량_주식예상체결_해제(삭제대상코드);
                }
                else
                {
                    // 만약 100개 전부가 잔고 종목이라 지울 게 없다면... 
                    // 이 경우 신규 등록을 포기하거나, 정책상 가장 오래된 것을 강제로 지워야 해.
                    Form1.Console_print("실시간 슬롯 부족: 100개 모두 잔고 종목입니다.");
                    return;
                }
            }

            // [Step 4] 신규 종목 등록
            실시간체결_주식호가잔량_주식예상체결_신규등록(code);

            Form1.form1.실시간요청코드_List.Add(code);
        }

        public static void 실시간체결_주식호가잔량_주식예상체결_신규등록(string Code)
        {
            Form1.tr_scheduler.EnqueuePriorityRequest("0B", async () =>
            {
                await Form1.websocketClient.SendMessageAsync(new
                {
                    trnm = "REG", // REG : 등록 , REMOVE : 해지
                    grp_no = 1, // 그룹번호
                    refresh = 1, // 등록(REG)시 0:기존유지안함 1:기존유지(Default) 0일경우 기존등록한 item/type은 해지, 1일경우 기존등록한 item/type 유지해지(REMOVE)시 값 불필요
                    data = new[]
                    {
                           new
                           {
                               item = new[] { Code + "_AL" }, // 실시간 등록 요소
                               type = _type_Realtime // [최적화] 고정 배열 재사용
                           }
                       }
                });
            });
        }

        public static void 실시간체결_주식호가잔량_주식예상체결_해제(string Code)
        {
            // 해제 요청도 우선순위 큐에 넣어서 처리 (안정성 확보)
            Form1.tr_scheduler.EnqueuePriorityRequest("REMOVE", async () =>
            {
                await Form1.websocketClient.SendMessageAsync(new
                {
                    trnm = "REMOVE", // [핵심] 등록(REG) -> 해지(REMOVE)로 변경
                    grp_no = 1,      // 등록했던 그룹 번호와 동일하게
                    refresh = 0,     // 해지 시에는 값 불필요 (보통 0으로 전송)
                    data = new[]
                    {
                       new
                       {
                           item = new[] { Code + "_AL" },
                           type = _type_Realtime // [최적화] 고정 배열 재사용
                       }
                    }
                });
            });
        }



        // 💡 [핵심 방어막 1] 다중 스레드(송신, 수신)에서 동시에 쳐들어와도 절대 뚫리지 않는 강제 락(Lock) 객체
        private static readonly object _reconnectLock = new object();
        private static bool 재연결_진행중 = false;

        public static void 재연결(string message)
        {
            // 💡 [좀비 방지 입구 컷] 프로그램이 이미 닫히는 중이라면 심폐소생술(재연결) 절대 금지!!
            if (Form1.프로그램종료중)
            {
                Form1.Console_print(">> 🛑 시스템 종료 중이므로 재연결을 시도하지 않습니다.");
                return;
            }

            // 💡 [입구 컷] 로딩 전이거나, 중복 접속으로 봇이 '안전 종료' 중일 때는 재연결 시도 자체를 무시!
            if (!Form1.로딩완료 || Form1.중복접속) return;

            // 💡 [스레드 세이프 락] 0.001초 차이로 재연결 요청이 여러 개 들어와도, 무조건 한 놈만 통과시킵니다.
            lock (_reconnectLock)
            {
                if (재연결_진행중) return; // 이미 누가 진행 중이면 조용히 퇴근

                // 💡 무거운 ToString() 문자 비교를 버리고, C# 기본 상태(Enum)로 0.00초 만에 다이렉트 비교!
                // 소켓이 살아있으면 굳이 끊을 필요 없음
                if (Form1.websocket != null && Form1.websocket.State == WebSocketState.Open) return;

                재연결_진행중 = true; // 자물쇠 찰칵!
            }

            // =========================================================
            // 💡 여기서부터는 우주가 멸망해도 단 1번만 실행됨이 보장됩니다.
            // =========================================================
            Form1.Console_print(message);
            Form1.Console_print(">> 기존 소켓 해제 및 자동 재연결 프로세스를 시작합니다...");

            Form1.connected = false;

            // 💡 [찌꺼기 청소] 기존 뻗어버린 소켓의 숨통을 확실히 끊고 메모리에서 날려버립니다 (메모리 누수 완벽 방지)
            try
            {
                if (Form1.websocket != null)
                {
                    Form1.websocket.Abort(); // 강제 종료
                    Form1.websocket.Dispose(); // 메모리 반환
                }
            }
            catch { }
            finally
            {
                Form1.websocket = null;
                Form1.websocketClient = null; // 클라이언트 껍데기도 완전 초기화
            }

            // 💡 [최우선 스케줄링] 봇이 하던 다른 매매 작업을 멈추고 연결부터 최우선으로 살려냅니다.
            Form1.tr_scheduler.EnqueuePriorityRequest("재연결", async () =>
            {
                try
                {
                    // 💡 [신의 한 수] 서버가 끊겼을 때 바로 1초 만에 때리면 증권사 서버가 "너 디도스(DDoS) 공격하냐?" 
                    // 하고 IP를 차단하거나 거절할 수 있습니다. 1.5초 정도 숨을 고르고 우아하게 연결합니다.
                    await Task.Delay(1500);

                    await Websoketconnect("재연결");
                }
                finally
                {
                    // 💡 [필수 안전장치] 연결에 성공하든, 또 에러가 나서 실패하든 무조건 자물쇠를 풀어줘야
                    // 다음번에 다시 재연결을 시도할 수 있습니다! (영원히 봇이 굳어버리는 데드락 방지)
                    재연결_진행중 = false;
                }
            });
        }


        // 10초 반복을 안전하게 멈추기 위한 토큰
        private static CancellationTokenSource cts_핑;

        // =================================================================
        // 1. PING 10초 반복 시작 (프로그램 로딩 완료 시점에 1번만 호출!)
        // =================================================================
        public static void 핑_10초_반복시작()
        {
            // 이미 실행 중이면 중복 실행 방지
            if (cts_핑 != null) return;

            cts_핑 = new CancellationTokenSource();
            Form1.Console_print("[웹소켓] PING(하트비트) 10초 반복 전송을 시작합니다.");

            // 백그라운드에서 UI를 멈추지 않고 10초마다 핑을 날립니다.
            Task.Run(async () =>
            {
                while (!cts_핑.IsCancellationRequested)
                {
                    // 아빠님이 만드신 기존 PING 함수 호출
                    await 핑();

                    // 정확히 10초(10000 밀리초) 대기
                    try
                    {
                        await Task.Delay(10000, cts_핑.Token);
                    }
                    catch (TaskCanceledException)
                    {
                        // 취소 토큰이 작동하면 자연스럽게 루프 종료
                        break;
                    }
                }
            }, cts_핑.Token);
        }

        // =================================================================
        // 2. PING 10초 반복 중지 (프로그램 종료 시 호출)
        // =================================================================
        public static void 핑_반복중지()
        {
            if (cts_핑 != null)
            {
                cts_핑.Cancel();
                cts_핑.Dispose();
                cts_핑 = null;
                Form1.Console_print("[웹소켓] PING(하트비트) 전송이 중지되었습니다.");
            }
        }

        // =================================================================
        // (수정 완료) 스케줄러를 무시하고 즉시 PING을 쏘는 VIP 함수
        // =================================================================
        public static async Task 핑()
        {
            // ====================================================================
            // 🛡️ [입구컷 1] 봇이 퇴근(종료) 중이면 핑이고 뭐고 즉시 퇴근!
            // ====================================================================
            if (Form1.프로그램종료중) return;

            // ====================================================================
            // 🛡️ [입구컷 2] 소켓이 죽어있거나 연결이 끊겼다면 허공에 핑을 쏘지 않습니다!
            // ====================================================================
            if (!Form1.connected || Form1.websocketClient == null || Form1.websocket == null || Form1.websocket.State != System.Net.WebSockets.WebSocketState.Open)
            {
                return;
            }

            // ====================================================================
            // 💡 [VIP 프리패스] 로딩 중이든 완료됐든, 큐(tr_scheduler)에 줄 서지 않고 
            // 새로 만든 고속 발송 함수(SendPingAsync)를 통해 무조건 즉시 발송합니다!
            // ====================================================================
            await Form1.websocketClient.SendPingAsync(new
            {
                trnm = "PING"
            });
        }
    }
}
