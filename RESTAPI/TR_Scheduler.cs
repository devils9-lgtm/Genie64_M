//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace 지니64
//{
//    // 요청 정보를 담을 간단한 클래스
//    public class ApiRequest
//    {
//        public string TrCode { get; set; }     
//        public Func<Task> TaskToRun { get; set; }
//        public DateTime RequestTime { get; set; }
//    }

//    public class TR_Scheduler
//    {
//        // 1. 설정을 위한 상수 (안전하게 1.1초로 설정)
//        private const int TIME_WINDOW_MS = 1100;
//        private const int MAX_GLOBAL_REQUESTS = 5; // 1초당 최대 5회
//        private const int MAX_SAME_TR_REQUESTS = 2; // 동일 TR 1초당 최대 2회

//        // 2. 데이터 저장소
//        private readonly List<ApiRequest> _pendingRequests = new List<ApiRequest>(); // 대기열 (리스트로 관리)
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
//                _pendingRequests.Add(new ApiRequest
//                {
//                    TrCode = trCode,
//                    TaskToRun = task, // 변경
//                    RequestTime = DateTime.Now
//                });
//            }
//        }

//        public void EnqueuePriorityRequest(string trCode, Func<Task> task)
//        {
//            lock (_lockObj)
//            {
//                _pendingRequests.Insert(0, new ApiRequest
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
//                ApiRequest requestToExecute = null;

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

//                        if (Form1.매매시작 != "매매시작" && QueueCount != 0)
//                        {
//                        //     Form1.Console_print($"TR_queue 남은 요청 갯수 : {QueueCount} timenow : {DateTime.Now:hhmmss.ffff}");
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

//        private int GetTrCount(string trCode)
//        {
//            if (_trHistory.ContainsKey(trCode))
//            {
//                return _trHistory[trCode].Count;
//            }
//            return 0;
//        }
//    }


//    public class 한국투자_TR_스케줄러
//    {
//        // 요청 처리를 위한 스레드 안전한 큐 (우선순위 큐와 일반 큐)
//        private readonly ConcurrentQueue<Func<Task>> _우선_요청_큐 = new ConcurrentQueue<Func<Task>>();
//        private readonly ConcurrentQueue<Func<Task>> _일반_요청_큐 = new ConcurrentQueue<Func<Task>>();

//        private readonly SemaphoreSlim _신호_세마포어 = new SemaphoreSlim(0);
//        private readonly CancellationTokenSource _취소_토큰_소스 = new CancellationTokenSource();
//        private Task _작업_스레드;

//        // 한국투자증권 초당 제한 안전 마진 (초당 3회 수준 = 약 330ms 마다 1회 실행)
//        private readonly int _실행_간격_밀리초 = 330;

//        public 한국투자_TR_스케줄러()
//        {
//            _작업_스레드 = Task.Run(() => 스케줄러_루프_시작(_취소_토큰_소스.Token));
//        }

//        /// <summary>
//        /// 일반적인 조회나 데이터 요청을 큐에 추가합니다.
//        /// </summary>
//        public void 요청_추가(Func<Task> 한국투자_API_작업)
//        {
//            _일반_요청_큐.Enqueue(한국투자_API_작업);
//            _신호_세마포어.Release();
//        }

//        /// <summary>
//        /// 잔고 확인이나 실시간 판단에 필요한 긴급 TR을 우선순위 큐에 추가합니다.
//        /// </summary>
//        public void 우선_요청_추가(Func<Task> 한국투자_우선_API_작업)
//        {
//            _우선_요청_큐.Enqueue(한국투자_우선_API_작업);
//            _신호_세마포어.Release();
//        }

//        /// <summary>
//        /// 한국투자증권의 유량 제한 규정을 준수하며 큐를 소비하는 핵심 루프입니다.
//        /// </summary>
//        private async Task 스케줄러_루프_시작(CancellationToken 취소_토큰)
//        {
//            while (!취소_토큰.IsCancellationRequested)
//            {
//                await _신호_세마포어.WaitAsync(취소_토큰);

//                Func<Task> 실행할_작업 = null;

//                // 1. 우선순위 높은 큐부터 먼저 확인하여 꺼냄
//                if (_우선_요청_큐.TryDequeue(out 실행할_작업))
//                {
//                    // 우선 큐에서 작업을 꺼냄
//                }
//                // 2. 우선 큐가 비어있다면 일반 큐에서 꺼냄
//                else if (_일반_요청_큐.TryDequeue(out 실행할_작업))
//                {
//                    // 일반 큐에서 작업을 꺼냄
//                }

//                if (실행할_작업 != null)
//                {
//                    try
//                    {
//                        // 실제 API 비동기 메서드 호출
//                        await 실행할_작업();
//                    }
//                    catch (Exception 예외)
//                    {
//                        // 텔레그램이나 로그 시스템에 에러 출력 가능
//                        Console.WriteLine($"[한국투자 스케줄러 에러] {예외.Message}");
//                    }

//                    // 한국투자증권 서버 유량 초과(과부하) 방지를 위한 강제 대기 시간
//                    await Task.Delay(_실행_간격_밀리초, 취소_토큰);
//                }
//            }
//        }

//        /// <summary>
//        /// 프로그램 종료 시 스케줄러를 안전하게 정지시킵니다.
//        /// </summary>
//        public void 스케줄러_종료()
//        {
//            _취소_토큰_소스.Cancel();
//            try
//            {
//                _작업_스레드.Wait();
//            }
//            catch (AggregateException) { }

//            _신호_세마포어.Dispose();
//            _취소_토큰_소스.Dispose();
//        }
//    }


//    public class LS_TR_스케줄러
//    {
//        // LS증권 전용 비동기 요청 관리 큐
//        private readonly ConcurrentQueue<Func<Task>> _긴급_요청_큐 = new ConcurrentQueue<Func<Task>>();
//        private readonly ConcurrentQueue<Func<Task>> _일반_요청_큐 = new ConcurrentQueue<Func<Task>>();

//        private readonly SemaphoreSlim _신호_세마포어 = new SemaphoreSlim(0);
//        private readonly CancellationTokenSource _중단_토큰_소스 = new CancellationTokenSource();
//        private Task _소비_프로세스;

//        // LS증권 안전 마진 설정 (초당 2회 수준 제한 대응 = 500ms 마다 1회 실행)
//        private readonly int _정지_지연_밀리초 = 500;

//        public LS_TR_스케줄러()
//        {
//            _소비_프로세스 = Task.Run(() => 큐_모니터링_루프(_중단_토큰_소스.Token));
//        }

//        /// <summary>
//        /// LS증권 일반 시세조회 및 TR 요청을 큐에 등록합니다.
//        /// </summary>
//        public void TR_등록(Func<Task> LS_API_메서드)
//        {
//            _일반_요청_큐.Enqueue(LS_API_메서드);
//            _신호_세마포어.Release();
//        }

//        /// <summary>
//        /// 잔고조회, 주문체결 확인 등 즉시 처리되어야 하는 TR을 최우선 처리 큐에 등록합니다.
//        /// </summary>
//        public void 긴급_TR_등록(Func<Task> LS_우선_API_메서드)
//        {
//            _긴급_요청_큐.Enqueue(LS_우선_API_메서드);
//            _신호_세마포어.Release();
//        }

//        /// <summary>
//        /// LS증권 제한 제한 규정을 회피하기 위한 모니터링 반복 제어문입니다.
//        /// </summary>
//        private async Task 큐_모니터링_루프(CancellationToken 중단_토큰)
//        {
//            while (!중단_토큰.IsCancellationRequested)
//            {
//                await _신호_세마포어.WaitAsync(중단_토큰);

//                Func<Task> 처리할_작업 = null;

//                // 긴급 처리 큐 우선 추출
//                if (_긴급_요청_큐.TryDequeue(out 처리할_작업))
//                {
//                    // 긴급 큐 처리 성공
//                }
//                // 일반 처리 큐 추출
//                else if (_일반_요청_큐.TryDequeue(out 처리할_작업))
//                {
//                    // 일반 큐 처리 성공
//                }

//                if (처리할_작업 != null)
//                {
//                    try
//                    {
//                        // LS증권 API 실제 전송 및 대기
//                        await 처리할_작업();
//                    }
//                    catch (Exception 에러)
//                    {
//                        Console.WriteLine($"[LS증권 스케줄러 에러] {에러.Message}");
//                    }

//                    // LS증권 초당 조회 제한(유량 에러)을 회피하기 위한 딜레이 타임 적용
//                    await Task.Delay(_정지_지연_밀리초, 중단_토큰);
//                }
//            }
//        }

//        /// <summary>
//        /// 애플리케이션 종료 시 안전하게 백그라운드 태스크를 해제합니다.
//        /// </summary>
//        public void 스케줄러_정지()
//        {
//            _중단_토큰_소스.Cancel();
//            try
//            {
//                _소비_프로세스.Wait();
//            }
//            catch (AggregateException) { }

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
    // 1. 키움증권 스케줄러 (모의/실전 동일: 초당 5회 제한)
    // =========================================================
    public class ApiRequest
    {
        public string TrCode { get; set; }
        public Func<Task> TaskToRun { get; set; }
        public DateTime RequestTime { get; set; }
    }

    public class TR_Scheduler
    {
        private const int TIME_WINDOW_MS = 1100;
        private const int MAX_GLOBAL_REQUESTS = 5; // 1초당 최대 5회
        private const int MAX_SAME_TR_REQUESTS = 2; // 동일 TR 1초당 최대 2회

        private readonly List<ApiRequest> _pendingRequests = new List<ApiRequest>();
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
                _pendingRequests.Add(new ApiRequest { TrCode = trCode, TaskToRun = task, RequestTime = DateTime.Now });
            }
        }

        public void EnqueuePriorityRequest(string trCode, Func<Task> task)
        {
            lock (_lockObj)
            {
                _pendingRequests.Insert(0, new ApiRequest { TrCode = trCode, TaskToRun = task, RequestTime = DateTime.Now });
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
                ApiRequest requestToExecute = null;

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
                        Console.WriteLine($"[키움증권 스케줄러 에러] {ex.Message}");
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
    // 2. 한국투자증권 스케줄러 (실전 18건/초, 모의 1건/초)
    // =========================================================
    public class 한투_TR스케줄러
    {
        private readonly ConcurrentQueue<Func<Task>> _우선_요청_큐 = new ConcurrentQueue<Func<Task>>();
        private readonly ConcurrentQueue<Func<Task>> _일반_요청_큐 = new ConcurrentQueue<Func<Task>>();

        private readonly SemaphoreSlim _신호_세마포어 = new SemaphoreSlim(0);
        private readonly CancellationTokenSource _취소_토큰_소스 = new CancellationTokenSource();
        private Task _작업_스레드;

        public 한투_TR스케줄러()
        {
            _작업_스레드 = Task.Run(() => 스케줄러_루프_시작(_취소_토큰_소스.Token));
        }

        public void 요청_추가(Func<Task> 한투_TR_작업)
        {
            _일반_요청_큐.Enqueue(한투_TR_작업);
            _신호_세마포어.Release();
        }

        public void 우선_요청_추가(Func<Task> 한투_우선_TR_작업)
        {
            _우선_요청_큐.Enqueue(한투_우선_TR_작업);
            _신호_세마포어.Release();
        }

        private async Task 스케줄러_루프_시작(CancellationToken 취소_토큰)
        {
            while (!취소_토큰.IsCancellationRequested)
            {
                await _신호_세마포어.WaitAsync(취소_토큰);
                Func<Task> 실행할_작업 = null;

                if (_우선_요청_큐.TryDequeue(out 실행할_작업)) { }
                else if (_일반_요청_큐.TryDequeue(out 실행할_작업)) { }

                if (실행할_작업 != null)
                {
                    try
                    {
                        await 실행할_작업();
                    }
                    catch (Exception 예외)
                    {
                        Console.WriteLine($"[한국투자 스케줄러 에러] {예외.Message}");
                    }

                    // [핵심 변경] 모의투자와 실전투자의 유량 제한 동적 적용
                    // 모의투자(true): 초당 1건 (1000ms + 안전마진 50ms = 1050ms)
                    // 실전투자(false): 초당 18건 (1000ms / 18 = 약 55.5ms -> 안전마진 70ms)
                    int 지연_시간 = GenieConfig.checkBox_Simulation ? 1050 : 70;

                    await Task.Delay(지연_시간, 취소_토큰);
                }
            }
        }

        public void 스케줄러_종료()
        {
            _취소_토큰_소스.Cancel();
            try { _작업_스레드.Wait(); } catch (AggregateException) { }
            _신호_세마포어.Dispose();
            _취소_토큰_소스.Dispose();
        }
    }


    // =========================================================
    // 3. LS증권 스케줄러 (TR별 차등 제한 + 모의투자 안전 마진)
    // =========================================================

    // 메모리 최적화: 값 타입(struct)을 사용하여 가비지 컬렉터(GC) 부하를 완벽히 제거
    // 하위 C# 버전에서도 안전하게 빌드되도록 단순 필드 구조로 안정성을 높였습니다.
    public readonly struct LS_Request
    {
        public readonly string TrCode;
        public readonly Func<Task> TaskToRun;

        public LS_Request(string trCode, Func<Task> taskToRun)
        {
            this.TrCode = trCode ?? "DEFAULT";
            this.TaskToRun = taskToRun;
        }

        // 구조체 null 체크 대용 (작업이 비어있는지 확인)
        public bool IsEmpty => TaskToRun == null;
    }

    public class LS_TR스케줄러
    {
        private readonly ConcurrentQueue<LS_Request> _긴급_요청_큐 = new ConcurrentQueue<LS_Request>();
        private readonly ConcurrentQueue<LS_Request> _일반_요청_큐 = new ConcurrentQueue<LS_Request>();

        private readonly SemaphoreSlim _신호_세마포어 = new SemaphoreSlim(0);
        private readonly CancellationTokenSource _중단_토큰_소스 = new CancellationTokenSource();
        private Task _소비_프로세스;

        // 유량 제한 문서의 '변경 제한수'를 바탕으로 안전 마진을 포함한 딜레이 딕셔너리
        private static readonly Dictionary<string, int> _lsTrDelays = new Dictionary<string, int>
        {
            // 10건/초 (약 100ms -> 안전마진 포함 110ms)
            {"t1511", 110}, {"t1101", 110}, {"t1102", 110}, {"t8450", 110},
            {"t1950", 110}, {"t1956", 110}, {"t1971", 110}, {"t1906", 110},
            {"t2101", 110}, {"t2105", 110}, {"t8402", 110}, {"t8403", 110},
            {"t8456", 110}, {"t8457", 110},
            {"g3101", 110}, {"g3102", 110}, {"g3104", 110}, {"g3106", 110}, {"g3190", 110},

            // 5건/초 (약 200ms -> 안전마진 포함 220ms)
            {"t8407", 220},

            // 2건/초 (약 500ms -> 안전마진 포함 520ms)
            {"t1310", 520}, {"t1422", 520}, {"t1427", 520}, {"t1442", 520},
            {"t0424", 520}, {"t0425", 520}, {"t0434", 520}, {"t0441", 520}
        };

        public LS_TR스케줄러()
        {
            _소비_프로세스 = Task.Run(() => 큐_모니터링_루프(_중단_토큰_소스.Token));
        }

        // 한국투자증권 스케줄러와 동일한 형식의 일반 요청 추가 메서드
        public void 요청_추가(Func<Task> LS_API_메서드)
        {
            TR_등록("DEFAULT", LS_API_메서드, false);
        }

        public void 요청_추가(string trCode, Func<Task> LS_API_메서드)
        {
            TR_등록(trCode, LS_API_메서드, false);
        }

        // 한국투자증권 스케줄러와 동일한 형식의 우선 요청 추가 메서드
        public void 우선_요청_추가(Func<Task> LS_우선_API_메서드)
        {
            TR_등록("DEFAULT", LS_우선_API_메서드, true);
        }

        public void 우선_요청_추가(string trCode, Func<Task> LS_우선_API_메서드)
        {
            TR_등록(trCode, LS_우선_API_메서드, true);
        }

        // 기존 TR_등록 진입점 원본 유지 (내부 구조체 할당 및 세마포어 신호 처리)
        public void TR_등록(Func<Task> LS_API_메서드, bool 우선요청 = false)
        {
            TR_등록("DEFAULT", LS_API_메서드, 우선요청);
        }

        public void TR_등록(string trCode, Func<Task> LS_API_메서드, bool 우선요청 = false)
        {
            // 구조체(값 타입)를 사용하여 호출 시 힙 메모리 할당이 전혀 발생하지 않음
            var request = new LS_Request(trCode, LS_API_메서드);

            if (우선요청)
            {
                _긴급_요청_큐.Enqueue(request);
            }
            else
            {
                _일반_요청_큐.Enqueue(request);
            }

            _신호_세마포어.Release();
        }

        private async Task 큐_모니터링_루프(CancellationToken 중단_토큰)
        {
            while (!중단_토큰.IsCancellationRequested)
            {
                try
                {
                    // CPU 최적화: ConfigureAwait(false)를 사용하여 스레드 전환 오버헤드 최소화
                    await _신호_세마포어.WaitAsync(중단_토큰).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                LS_Request 처리할_작업 = default;
                bool 작업존재 = false;

                // 우선 순위 처리: 한국투자 스케줄러처럼 긴급(우선) 요청 큐를 항상 먼저 꺼내옴
                if (_긴급_요청_큐.TryDequeue(out 처리할_작업))
                {
                    작업존재 = true;
                }
                else if (_일반_요청_큐.TryDequeue(out 처리할_작업))
                {
                    작업존재 = true;
                }

                if (작업존재 && !처리할_작업.IsEmpty)
                {
                    try
                    {
                        // 비동기 작업 실행 시 스레드 컨텍스트 전환 비용 감소 최적화
                        await 처리할_작업.TaskToRun().ConfigureAwait(false);
                    }
                    catch (Exception 에러)
                    {
                        Console.WriteLine($"[LS증권 스케줄러 에러] {에러.Message}");
                    }

                    // 기본 지연 시간 (딕셔너리에 지정되지 않은 TR 코드는 가장 안정적인 1050ms로 자동 대응)
                    int 지연_시간 = 1050;

                    // CPU 최적화: TryGetValue를 사용하여 딕셔너리 이중 검색(Contains/Get)으로 인한 연산 낭비 차단
                    if (!string.IsNullOrEmpty(처리할_작업.TrCode) && _lsTrDelays.TryGetValue(처리할_작업.TrCode, out int delayValue))
                    {
                        지연_시간 = delayValue;
                    }

                    // 모의투자일 경우 강제로 하한선 제한 마진 적용
                    if (GenieConfig.checkBox_Simulation)
                    {
                        지연_시간 = Math.Max(지연_시간, 500);
                    }

                    try
                    {
                        await Task.Delay(지연_시간, 중단_토큰).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }
            }
        }

        public void 스케줄러_정지()
        {
            _중단_토큰_소스.Cancel();
            try { _소비_프로세스.Wait(); } catch (AggregateException) { }
            _신호_세마포어.Dispose();
            _중단_토큰_소스.Dispose();
        }
    }







}


