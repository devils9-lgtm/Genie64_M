//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace 지니64
//{
//    // 요청 정보를 담을 간단한 클래스
//    public class Order_ApiRequest
//    {
//        public string TrCode { get; set; }     
//        public Func<Task> TaskToRun { get; set; }
//        public DateTime RequestTime { get; set; }
//    }

//    public class Order_Scheduler
//    {
//        // 1. 설정을 위한 상수 (안전하게 1.1초로 설정)
//        private const int TIME_WINDOW_MS = 1100;
//        private const int MAX_GLOBAL_REQUESTS = 5; // 1초당 최대 5회
//        private const int MAX_SAME_TR_REQUESTS = 2; // 동일 TR 1초당 최대 2회

//        // 2. 데이터 저장소
//        private readonly List<Order_ApiRequest> _pendingRequests = new List<Order_ApiRequest>(); // 대기열 (리스트로 관리)
//        private readonly List<DateTime> _globalHistory = new List<DateTime>(); // 전체 전송 기록
//        private readonly Dictionary<string, List<DateTime>> _trHistory = new Dictionary<string, List<DateTime>>(); // TR별 전송 기록

//        // 3. 스레드 동기화를 위한 Lock 객체
//        private readonly object _lockObj = new object();
//        private bool _isRunning = false;

//        /// <summary>
//        /// 스케줄러 시작 (폼 로드 시 한 번만 호출)
//        /// </summary>
//        public void Start()
//        {
//            if (_isRunning) return;
//            _isRunning = true;

//            // 별도의 Task에서 무한 루프 실행 (UI 멈춤 방지)
//            Task.Run(ProcessQueue);
//        }

//        public int QueueCount
//        {
//            get
//            {
//                // 리스트에 접근할 때는 항상 잠금(Lock)이 필요합니다.
//                lock (_lockObj)
//                {
//                    return _pendingRequests.Count;
//                }
//            }
//        }

//        /// <summary>
//        /// 외부에서 요청을 넣는 함수
//        /// </summary>
//        /// <param name="trCode">TR 코드 (예: opt10001)</param>
//        /// <param name="action">실행할 API 호출 코드</param>
//        public void EnqueueRequest(string trCode, Func<Task> task)
//        {
//            lock (_lockObj)
//            {
//                _pendingRequests.Add(new Order_ApiRequest
//                {
//                    TrCode = trCode,
//                    TaskToRun = task, // 변경
//                    RequestTime = DateTime.Now
//                });
//            }
//        }

//        /// <summary>
//        /// 요청을 대기열 맨 앞에 추가합니다. (우선순위 요청)
//        /// </summary>
//        public void EnqueuePriorityRequest(string trCode, Func<Task> task)
//        {
//            lock (_lockObj)
//            {
//                _pendingRequests.Insert(0, new Order_ApiRequest
//                {
//                    TrCode = trCode,
//                    TaskToRun = task, // 변경
//                    RequestTime = DateTime.Now
//                });
//            }
//        }

//        /// <summary>
//        /// **[추가된 기능]** 대기열에 있는 모든 요청을 즉시 비웁니다.
//        /// </summary>
//        public void ClearQueue()
//        {
//            lock (_lockObj)
//            {
//                // Clear()는 List의 모든 요소를 O(N)보다 빠르게 제거하며,
//                // 메모리(GC) 측면에서도 효율적입니다.
//                _pendingRequests.Clear();
//            }
//        }

//        /// <summary>
//        /// 실제 처리를 담당하는 워커 루프
//        /// </summary>
//        private async Task ProcessQueue()
//        {
//            while (_isRunning)
//            {
//                Order_ApiRequest requestToExecute = null;

//                lock (_lockObj)
//                {
//                    // 1. 오래된 기록 청소 (1.1초 지난 기록은 삭제하여 메모리 관리)
//                    CleanUpHistory();

//                    // 2. 글로벌 제한 체크 (최근 1초간 5회 이상이면 아무것도 못함)
//                    if (_globalHistory.Count < MAX_GLOBAL_REQUESTS)
//                    {
//                        // 3. 리스트를 순회하며 "지금 보낼 수 있는" 요청 찾기 (인터리빙 핵심 로직)
//                        // (맨 앞에게 막혔다고 멈추지 않고, 뒤에 가능한 놈을 찾음)
//                        foreach (var req in _pendingRequests)
//                        {
//                            int currentTrCount = GetTrCount(req.TrCode);

//                            // 동일 TR 제한(2회)에 걸리지 않았는가?
//                            if (currentTrCount < MAX_SAME_TR_REQUESTS)
//                            {
//                                requestToExecute = req;
//                                break; // 보낼 녀석을 찾았으니 탐색 중단
//                            }
//                            // 여기에 걸리면 다음 req(다른 TR일 수 있음)를 검사하러 갑니다.
//                        }
//                    }
//                }

//                if (requestToExecute != null)
//                {
//                    // 4. 실행 및 기록
//                    try
//                    {
//                        // 변경점 3: 실제 비동기 작업이 끝날 때까지 스케줄러가 기다려 줌 (중요!)
//                        // 이렇게 해야 REST API 응답이 올 때까지 다음 요청을 보내지 않거나 타이밍을 맞출 수 있음
//                        if (requestToExecute.TaskToRun != null)
//                        {
//                            await requestToExecute.TaskToRun.Invoke();
//                        }

//                    }
//                    catch (Exception ex)
//                    {
//                         Form1.Console_print($"Error executing TR: {ex.Message}");
//                    }

//                    lock (_lockObj)
//                    {
//                        // 대기열에서 제거
//                        _pendingRequests.Remove(requestToExecute);

//                        // 기록 추가
//                        DateTime now = DateTime.Now;
//                        _globalHistory.Add(now);

//                        if (!_trHistory.ContainsKey(requestToExecute.TrCode))
//                            _trHistory[requestToExecute.TrCode] = new List<DateTime>();

//                        _trHistory[requestToExecute.TrCode].Add(now);
//                    }

//                    // 너무 빠른 연속 호출 방지를 위한 미세 지연 (선택 사항)
//                    await Task.Delay(50);
//                }
//                else
//                {
//                    // 할 일이 없거나, 제한에 걸려서 쉴 때
//                    await Task.Delay(100);
//                }
//            }
//        }

//        // --- 헬퍼 함수들 ---

//        /// <summary>
//        /// 시간 창(TIME_WINDOW_MS)을 벗어난 오래된 전송 기록을 청소하여 메모리 사용량을 줄입니다.
//        /// </summary>
//        private void CleanUpHistory()
//        {
//            DateTime threshold = DateTime.Now.AddMilliseconds(-TIME_WINDOW_MS);

//            // 글로벌 기록 청소
//            _globalHistory.RemoveAll(t => t < threshold);

//            // TR별 기록 청소
//            foreach (var key in _trHistory.Keys.ToList())
//            {
//                _trHistory[key].RemoveAll(t => t < threshold);
//            }
//        }
//        /// <summary>
//        /// 현재 시간 창 내의 TR 코드를 이용한 전송 횟수를 반환합니다.
//        /// </summary>
//        private int GetTrCount(string trCode)
//        {
//            if (_trHistory.ContainsKey(trCode))
//            {
//                return _trHistory[trCode].Count;
//            }
//            return 0;
//        }
//    }



//    public class 한국투자_주문_스케줄러
//    {
//        // 주문 전용 큐 (매수/매도/정정/취소 등)
//        private readonly ConcurrentQueue<Func<Task>> _주문_대기_큐 = new ConcurrentQueue<Func<Task>>();

//        private readonly SemaphoreSlim _신호_세마포어 = new SemaphoreSlim(0);
//        private readonly CancellationTokenSource _취소_토큰_소스 = new CancellationTokenSource();
//        private Task _주문_처리_스레드;

//        // 한투 주문 유량 제한 고려 (안전 마진 300ms)
//        private readonly int _주문_실행_간격_밀리초 = 300;

//        public 한국투자_주문_스케줄러()
//        {
//            _주문_처리_스레드 = Task.Run(() => 주문_처리_루프(_취소_토큰_소스.Token));
//        }

//        /// <summary>
//        /// 신규 매수, 매도, 정정, 취소 주문을 큐에 등록합니다.
//        /// </summary>
//        public void 주문_요청_추가(Func<Task> 한국투자_주문_작업)
//        {
//            _주문_대기_큐.Enqueue(한국투자_주문_작업);
//            _신호_세마포어.Release();
//        }

//        private async Task 주문_처리_루프(CancellationToken 취소_토큰)
//        {
//            while (!취소_토큰.IsCancellationRequested)
//            {
//                // 주문이 들어올 때까지 대기
//                await _신호_세마포어.WaitAsync(취소_토큰);

//                if (_주문_대기_큐.TryDequeue(out Func<Task> 실행할_주문))
//                {
//                    try
//                    {
//                        // 실제 주문 API 전송
//                        await 실행할_주문();
//                    }
//                    catch (Exception 예외)
//                    {
//                        Console.WriteLine($"[한투 주문 에러] {예외.Message}");
//                    }

//                    // 연속 주문 시 증권사 서버 차단 방지용 딜레이
//                    await Task.Delay(_주문_실행_간격_밀리초, 취소_토큰);
//                }
//            }
//        }

//        public void 스케줄러_종료()
//        {
//            _취소_토큰_소스.Cancel();
//            try { _주문_처리_스레드.Wait(); } catch { }
//            _신호_세마포어.Dispose();
//            _취소_토큰_소스.Dispose();
//        }
//    }


//    public class LS_주문_스케줄러
//    {
//        private readonly ConcurrentQueue<Func<Task>> _주문_대기_큐 = new ConcurrentQueue<Func<Task>>();
//        private readonly SemaphoreSlim _신호_세마포어 = new SemaphoreSlim(0);
//        private readonly CancellationTokenSource _중단_토큰_소스 = new CancellationTokenSource();
//        private Task _주문_처리_스레드;

//        // LS증권 주문 유량 제한 고려 (안전 마진 400ms)
//        private readonly int _주문_실행_간격_밀리초 = 400;

//        public LS_주문_스케줄러()
//        {
//            _주문_처리_스레드 = Task.Run(() => 주문_모니터링_루프(_중단_토큰_소스.Token));
//        }

//        public void 주문_등록(Func<Task> LS_주문_메서드)
//        {
//            _주문_대기_큐.Enqueue(LS_주문_메서드);
//            _신호_세마포어.Release();
//        }

//        private async Task 주문_모니터링_루프(CancellationToken 중단_토큰)
//        {
//            while (!중단_토큰.IsCancellationRequested)
//            {
//                await _신호_세마포어.WaitAsync(중단_토큰);

//                if (_주문_대기_큐.TryDequeue(out Func<Task> 처리할_주문))
//                {
//                    try
//                    {
//                        // LS증권 API 실제 주문 전송
//                        await 처리할_주문();
//                    }
//                    catch (Exception 에러)
//                    {
//                        Console.WriteLine($"[LS증권 주문 에러] {에러.Message}");
//                    }

//                    // LS증권 연속 주문 차단 방지 대기
//                    await Task.Delay(_주문_실행_간격_밀리초, 중단_토큰);
//                }
//            }
//        }

//        public void 스케줄러_정지()
//        {
//            _중단_토큰_소스.Cancel();
//            try { _주문_처리_스레드.Wait(); } catch { }
//            _신호_세마포어.Dispose();
//            _중단_토큰_소스.Dispose();
//        }
//    }

//}



using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace 지니64
{
    // =========================================================
    // 1. 키움증권 주문 스케줄러 (모의/실전 동일: 초당 5회 제한)
    // =========================================================
    public class Order_ApiRequest
    {
        public string TrCode { get; set; }
        public Func<Task> TaskToRun { get; set; }
        public DateTime RequestTime { get; set; }
    }

    public class Order_Scheduler
    {
        private const int TIME_WINDOW_MS = 1100;
        private const int MAX_GLOBAL_REQUESTS = 5; // 1초당 최대 5회
        private const int MAX_SAME_TR_REQUESTS = 2; // 동일 TR 1초당 최대 2회

        private readonly List<Order_ApiRequest> _pendingRequests = new List<Order_ApiRequest>();
        private readonly List<DateTime> _globalHistory = new List<DateTime>();
        private readonly Dictionary<string, List<DateTime>> _trHistory = new Dictionary<string, List<DateTime>>();

        private readonly object _lockObj = new object();
        private bool _isRunning = false;

        public void Start()
        {
            if (_isRunning) return;
            _isRunning = true;
            Task.Run(ProcessQueue);
        }

        public int QueueCount
        {
            get { lock (_lockObj) { return _pendingRequests.Count; } }
        }

        public void EnqueueRequest(string trCode, Func<Task> task)
        {
            lock (_lockObj)
            {
                _pendingRequests.Add(new Order_ApiRequest { TrCode = trCode, TaskToRun = task, RequestTime = DateTime.Now });
            }
        }

        public void EnqueuePriorityRequest(string trCode, Func<Task> task)
        {
            lock (_lockObj)
            {
                _pendingRequests.Insert(0, new Order_ApiRequest { TrCode = trCode, TaskToRun = task, RequestTime = DateTime.Now });
            }
        }

        public void ClearQueue()
        {
            lock (_lockObj) { _pendingRequests.Clear(); }
        }

        private async Task ProcessQueue()
        {
            while (_isRunning)
            {
                Order_ApiRequest requestToExecute = null;

                lock (_lockObj)
                {
                    CleanUpHistory();

                    if (_globalHistory.Count < MAX_GLOBAL_REQUESTS)
                    {
                        foreach (var req in _pendingRequests)
                        {
                            int currentTrCount = GetTrCount(req.TrCode);
                            if (currentTrCount < MAX_SAME_TR_REQUESTS)
                            {
                                requestToExecute = req;
                                break;
                            }
                        }
                    }
                }

                if (requestToExecute != null)
                {
                    try
                    {
                        if (requestToExecute.TaskToRun != null)
                        {
                            await requestToExecute.TaskToRun.Invoke();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Form1.Console_print($"Error executing Order TR: {ex.Message}");
                        Console.WriteLine($"[키움 주문 에러] {ex.Message}");
                    }

                    lock (_lockObj)
                    {
                        _pendingRequests.Remove(requestToExecute);
                        DateTime now = DateTime.Now;
                        _globalHistory.Add(now);

                        if (!_trHistory.ContainsKey(requestToExecute.TrCode))
                            _trHistory[requestToExecute.TrCode] = new List<DateTime>();

                        _trHistory[requestToExecute.TrCode].Add(now);
                    }

                    await Task.Delay(50);
                }
                else
                {
                    await Task.Delay(100);
                }
            }
        }

        private void CleanUpHistory()
        {
            DateTime threshold = DateTime.Now.AddMilliseconds(-TIME_WINDOW_MS);
            _globalHistory.RemoveAll(t => t < threshold);
            foreach (var key in _trHistory.Keys.ToList())
            {
                _trHistory[key].RemoveAll(t => t < threshold);
            }
        }

        private int GetTrCount(string trCode)
        {
            if (_trHistory.ContainsKey(trCode)) return _trHistory[trCode].Count;
            return 0;
        }
    }


    // =========================================================
    // 2. 한국투자증권 주문 스케줄러 (모의 1건/초, 실전 18건/초)
    // =========================================================
    public class 한투_주문스케줄러
    {
        private readonly ConcurrentQueue<Func<Task>> _주문_대기_큐 = new ConcurrentQueue<Func<Task>>();
        private readonly SemaphoreSlim _신호_세마포어 = new SemaphoreSlim(0);
        private readonly CancellationTokenSource _취소_토큰_소스 = new CancellationTokenSource();
        private Task _주문_처리_스레드;

        public 한투_주문스케줄러()
        {
            _주문_처리_스레드 = Task.Run(() => 주문_처리_루프(_취소_토큰_소스.Token));
        }

        public void 주문_요청_추가(Func<Task> 한투_주문_작업)
        {
            _주문_대기_큐.Enqueue(한투_주문_작업);
            _신호_세마포어.Release();
        }

        private async Task 주문_처리_루프(CancellationToken 취소_토큰)
        {
            while (!취소_토큰.IsCancellationRequested)
            {
                await _신호_세마포어.WaitAsync(취소_토큰);

                if (_주문_대기_큐.TryDequeue(out Func<Task> 실행할_주문))
                {
                    try
                    {
                        await 실행할_주문();
                    }
                    catch (Exception 예외)
                    {
                        Console.WriteLine($"[한투 주문 에러] {예외.Message}");
                    }

                    // [변경] 모의투자는 초당 1회(매우 엄격), 실전은 초당 18회(빠름)
                    int 지연_시간 = GenieConfig.checkBox_Simulation ? 1050 : 70;

                    await Task.Delay(지연_시간, 취소_토큰);
                }
            }
        }

        public void 스케줄러_종료()
        {
            _취소_토큰_소스.Cancel();
            try { _주문_처리_스레드.Wait(); } catch { }
            _신호_세마포어.Dispose();
            _취소_토큰_소스.Dispose();
        }
    }


    // =========================================================
    // 3. LS증권 주문 스케줄러 (신규/정정/취소 속도 다름)
    // =========================================================
    // [추가] LS증권은 주문 종류(신규 vs 정정/취소)에 따라 제한이 다르므로 TR코드를 함께 보관합니다.
    public class LS_Order_Request
    {
        public string TrCode { get; set; }
        public Func<Task> TaskToRun { get; set; }
    }

    public class LS_주문스케줄러
    {
        private readonly ConcurrentQueue<LS_Order_Request> _주문_대기_큐 = new ConcurrentQueue<LS_Order_Request>();
        private readonly SemaphoreSlim _신호_세마포어 = new SemaphoreSlim(0);
        private readonly CancellationTokenSource _중단_토큰_소스 = new CancellationTokenSource();
        private Task _주문_처리_스레드;

        // LS증권 주문 TR별 지연 시간 설정 (안전 마진 포함)
        private static readonly Dictionary<string, int> _lsOrderDelays = new Dictionary<string, int>
        {
            {"CSPAT00601", 110}, // 신규 현물 주문: 초당 10건 (약 100ms + 10ms 마진)
            {"CSPAT00701", 350}, // 정정 현물 주문: 초당 3건 (약 333ms + 17ms 마진)
            {"CSPAT00801", 350}  // 취소 현물 주문: 초당 3건 (약 333ms + 17ms 마진)
        };

        public LS_주문스케줄러()
        {
            _주문_처리_스레드 = Task.Run(() => 주문_모니터링_루프(_중단_토큰_소스.Token));
        }

        /// <summary>
        /// (호환성 유지용) TR코드 없이 등록 시, 가장 보수적인 350ms(취소주문 기준)로 적용됩니다.
        /// </summary>
        public void 주문_등록(Func<Task> LS_주문_메서드)
        {
            주문_등록("DEFAULT", LS_주문_메서드);
        }

        /// <summary>
        /// [권장] TR코드를 포함하여 등록하면 신규(빠름) / 정정취소(느림)에 맞게 딜레이가 적용됩니다.
        /// </summary>
        public void 주문_등록(string trCode, Func<Task> LS_주문_메서드)
        {
            _주문_대기_큐.Enqueue(new LS_Order_Request { TrCode = trCode, TaskToRun = LS_주문_메서드 });
            _신호_세마포어.Release();
        }

        private async Task 주문_모니터링_루프(CancellationToken 중단_토큰)
        {
            while (!중단_토큰.IsCancellationRequested)
            {
                await _신호_세마포어.WaitAsync(중단_토큰);

                if (_주문_대기_큐.TryDequeue(out LS_Order_Request 처리할_주문))
                {
                    try
                    {
                        await 처리할_주문.TaskToRun();
                    }
                    catch (Exception 에러)
                    {
                        Console.WriteLine($"[LS증권 주문 에러] {에러.Message}");
                    }

                    // 1. TR코드별 주문 속도 가져오기 (기본값 350ms - 보수적 적용)
                    int 지연_시간 = 350;
                    if (!string.IsNullOrEmpty(처리할_주문.TrCode) && _lsOrderDelays.ContainsKey(처리할_주문.TrCode))
                    {
                        지연_시간 = _lsOrderDelays[처리할_주문.TrCode];
                    }

                    // 2. 모의투자일 경우, 서버 튕김을 고려하여 최소 500ms(0.5초) 간격을 강제합니다.
                    if (GenieConfig.checkBox_Simulation)
                    {
                        지연_시간 = Math.Max(지연_시간, 500);
                    }

                    await Task.Delay(지연_시간, 중단_토큰);
                }
            }
        }

        public void 스케줄러_정지()
        {
            _중단_토큰_소스.Cancel();
            try { _주문_처리_스레드.Wait(); } catch { }
            _신호_세마포어.Dispose();
            _중단_토큰_소스.Dispose();
        }
    }
}