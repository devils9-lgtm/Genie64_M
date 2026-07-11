using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace 지니64
{
    // 요청 정보를 담을 간단한 클래스
    public class ApiRequest
    {
        public string TrCode { get; set; }     
        public Func<Task> TaskToRun { get; set; }
        public DateTime RequestTime { get; set; }
    }

    public class TR_Scheduler
    {
        // 1. 설정을 위한 상수 (안전하게 1.1초로 설정)
        private const int TIME_WINDOW_MS = 1100;
        private const int MAX_GLOBAL_REQUESTS = 5; // 1초당 최대 5회
        private const int MAX_SAME_TR_REQUESTS = 2; // 동일 TR 1초당 최대 2회

        // 2. 데이터 저장소
        private readonly List<ApiRequest> _pendingRequests = new List<ApiRequest>(); // 대기열 (리스트로 관리)
        private readonly List<DateTime> _globalHistory = new List<DateTime>(); // 전체 전송 기록
        private readonly Dictionary<string, List<DateTime>> _trHistory = new Dictionary<string, List<DateTime>>(); // TR별 전송 기록

        // 3. 스레드 동기화를 위한 Lock 객체
        private readonly object _lockObj = new object();
        private bool _isRunning = false;

        /// <summary>
        /// 스케줄러 시작 (폼 로드 시 한 번만 호출)
        /// </summary>
        public void Start()
        {
            if (_isRunning) return;
            _isRunning = true;

            // 별도의 Task에서 무한 루프 실행 (UI 멈춤 방지)
            Task.Run(ProcessQueue);
        }

        public int QueueCount
        {
            get
            {
                // 리스트에 접근할 때는 항상 잠금(Lock)이 필요합니다.
                lock (_lockObj)
                {
                    return _pendingRequests.Count;
                }
            }
        }

        /// <summary>
        /// 외부에서 요청을 넣는 함수
        /// </summary>
        /// <param name="trCode">TR 코드 (예: opt10001)</param>
        /// <param name="action">실행할 API 호출 코드</param>
        public void EnqueueRequest(string trCode, Func<Task> task)
        {
            lock (_lockObj)
            {
                _pendingRequests.Add(new ApiRequest
                {
                    TrCode = trCode,
                    TaskToRun = task, // 변경
                    RequestTime = DateTime.Now
                });
            }
        }

        public void EnqueuePriorityRequest(string trCode, Func<Task> task)
        {
            lock (_lockObj)
            {
                _pendingRequests.Insert(0, new ApiRequest
                {
                    TrCode = trCode,
                    TaskToRun = task, // 변경
                    RequestTime = DateTime.Now
                });
            }
        }

        /// <summary>
        /// **[추가된 기능]** 대기열에 있는 모든 요청을 즉시 비웁니다.
        /// </summary>
        public void ClearQueue()
        {
            lock (_lockObj)
            {
                // Clear()는 List의 모든 요소를 O(N)보다 빠르게 제거하며,
                // 메모리(GC) 측면에서도 효율적입니다.
                _pendingRequests.Clear();
            }
        }

        /// <summary>
        /// 실제 처리를 담당하는 워커 루프
        /// </summary>
        private async Task ProcessQueue()
        {
            while (_isRunning)
            {
                ApiRequest requestToExecute = null;

                lock (_lockObj)
                {
                    // 1. 오래된 기록 청소 (1.1초 지난 기록은 삭제하여 메모리 관리)
                    CleanUpHistory();

                    // 2. 글로벌 제한 체크 (최근 1초간 5회 이상이면 아무것도 못함)
                    if (_globalHistory.Count < MAX_GLOBAL_REQUESTS)
                    {
                        // 3. 리스트를 순회하며 "지금 보낼 수 있는" 요청 찾기 (인터리빙 핵심 로직)
                        // (맨 앞에게 막혔다고 멈추지 않고, 뒤에 가능한 놈을 찾음)
                        foreach (var req in _pendingRequests)
                        {
                            int currentTrCount = GetTrCount(req.TrCode);

                            // 동일 TR 제한(2회)에 걸리지 않았는가?
                            if (currentTrCount < MAX_SAME_TR_REQUESTS)
                            {
                                requestToExecute = req;
                                break; // 보낼 녀석을 찾았으니 탐색 중단
                            }
                            // 여기에 걸리면 다음 req(다른 TR일 수 있음)를 검사하러 갑니다.
                        }
                    }
                }

                if (requestToExecute != null)
                {
                    // 4. 실행 및 기록
                    try
                    {
                        // 변경점 3: 실제 비동기 작업이 끝날 때까지 스케줄러가 기다려 줌 (중요!)
                        // 이렇게 해야 REST API 응답이 올 때까지 다음 요청을 보내지 않거나 타이밍을 맞출 수 있음
                        if (requestToExecute.TaskToRun != null)
                        {
                            await requestToExecute.TaskToRun.Invoke();
                        }

                        if (Form1.매매시작 != "매매시작" && QueueCount != 0)
                        {
                        //     Form1.Console_print($"TR_queue 남은 요청 갯수 : {QueueCount} timenow : {DateTime.Now:hhmmss.ffff}");
                        }
                    }
                    catch (Exception ex)
                    {
                         Form1.Console_print($"Error executing TR: {ex.Message}");
                    }

                    lock (_lockObj)
                    {
                        // 대기열에서 제거
                        _pendingRequests.Remove(requestToExecute);

                        // 기록 추가
                        DateTime now = DateTime.Now;
                        _globalHistory.Add(now);

                        if (!_trHistory.ContainsKey(requestToExecute.TrCode))
                            _trHistory[requestToExecute.TrCode] = new List<DateTime>();

                        _trHistory[requestToExecute.TrCode].Add(now);
                    }

                    // 너무 빠른 연속 호출 방지를 위한 미세 지연 (선택 사항)
                    await Task.Delay(50);
                }
                else
                {
                    // 할 일이 없거나, 제한에 걸려서 쉴 때
                    await Task.Delay(100);
                }
            }
        }

        // --- 헬퍼 함수들 ---

        private void CleanUpHistory()
        {
            DateTime threshold = DateTime.Now.AddMilliseconds(-TIME_WINDOW_MS);

            // 글로벌 기록 청소
            _globalHistory.RemoveAll(t => t < threshold);

            // TR별 기록 청소
            foreach (var key in _trHistory.Keys.ToList())
            {
                _trHistory[key].RemoveAll(t => t < threshold);
            }
        }

        private int GetTrCount(string trCode)
        {
            if (_trHistory.ContainsKey(trCode))
            {
                return _trHistory[trCode].Count;
            }
            return 0;
        }
    }


    public class 한국투자_TR_스케줄러
    {
        // 요청 처리를 위한 스레드 안전한 큐 (우선순위 큐와 일반 큐)
        private readonly ConcurrentQueue<Func<Task>> _우선_요청_큐 = new ConcurrentQueue<Func<Task>>();
        private readonly ConcurrentQueue<Func<Task>> _일반_요청_큐 = new ConcurrentQueue<Func<Task>>();

        private readonly SemaphoreSlim _신호_세마포어 = new SemaphoreSlim(0);
        private readonly CancellationTokenSource _취소_토큰_소스 = new CancellationTokenSource();
        private Task _작업_스레드;

        // 한국투자증권 초당 제한 안전 마진 (초당 3회 수준 = 약 330ms 마다 1회 실행)
        private readonly int _실행_간격_밀리초 = 330;

        public 한국투자_TR_스케줄러()
        {
            _작업_스레드 = Task.Run(() => 스케줄러_루프_시작(_취소_토큰_소스.Token));
        }

        /// <summary>
        /// 일반적인 조회나 데이터 요청을 큐에 추가합니다.
        /// </summary>
        public void 요청_추가(Func<Task> 한국투자_API_작업)
        {
            _일반_요청_큐.Enqueue(한국투자_API_작업);
            _신호_세마포어.Release();
        }

        /// <summary>
        /// 잔고 확인이나 실시간 판단에 필요한 긴급 TR을 우선순위 큐에 추가합니다.
        /// </summary>
        public void 우선_요청_추가(Func<Task> 한국투자_우선_API_작업)
        {
            _우선_요청_큐.Enqueue(한국투자_우선_API_작업);
            _신호_세마포어.Release();
        }

        /// <summary>
        /// 한국투자증권의 유량 제한 규정을 준수하며 큐를 소비하는 핵심 루프입니다.
        /// </summary>
        private async Task 스케줄러_루프_시작(CancellationToken 취소_토큰)
        {
            while (!취소_토큰.IsCancellationRequested)
            {
                await _신호_세마포어.WaitAsync(취소_토큰);

                Func<Task> 실행할_작업 = null;

                // 1. 우선순위 높은 큐부터 먼저 확인하여 꺼냄
                if (_우선_요청_큐.TryDequeue(out 실행할_작업))
                {
                    // 우선 큐에서 작업을 꺼냄
                }
                // 2. 우선 큐가 비어있다면 일반 큐에서 꺼냄
                else if (_일반_요청_큐.TryDequeue(out 실행할_작업))
                {
                    // 일반 큐에서 작업을 꺼냄
                }

                if (실행할_작업 != null)
                {
                    try
                    {
                        // 실제 API 비동기 메서드 호출
                        await 실행할_작업();
                    }
                    catch (Exception 예외)
                    {
                        // 텔레그램이나 로그 시스템에 에러 출력 가능
                        Console.WriteLine($"[한국투자 스케줄러 에러] {예외.Message}");
                    }

                    // 한국투자증권 서버 유량 초과(과부하) 방지를 위한 강제 대기 시간
                    await Task.Delay(_실행_간격_밀리초, 취소_토큰);
                }
            }
        }

        /// <summary>
        /// 프로그램 종료 시 스케줄러를 안전하게 정지시킵니다.
        /// </summary>
        public void 스케줄러_종료()
        {
            _취소_토큰_소스.Cancel();
            try
            {
                _작업_스레드.Wait();
            }
            catch (AggregateException) { }

            _신호_세마포어.Dispose();
            _취소_토큰_소스.Dispose();
        }
    }


    public class LS_TR_스케줄러
    {
        // LS증권 전용 비동기 요청 관리 큐
        private readonly ConcurrentQueue<Func<Task>> _긴급_요청_큐 = new ConcurrentQueue<Func<Task>>();
        private readonly ConcurrentQueue<Func<Task>> _일반_요청_큐 = new ConcurrentQueue<Func<Task>>();

        private readonly SemaphoreSlim _신호_세마포어 = new SemaphoreSlim(0);
        private readonly CancellationTokenSource _중단_토큰_소스 = new CancellationTokenSource();
        private Task _소비_프로세스;

        // LS증권 안전 마진 설정 (초당 2회 수준 제한 대응 = 500ms 마다 1회 실행)
        private readonly int _정지_지연_밀리초 = 500;

        public LS_TR_스케줄러()
        {
            _소비_프로세스 = Task.Run(() => 큐_모니터링_루프(_중단_토큰_소스.Token));
        }

        /// <summary>
        /// LS증권 일반 시세조회 및 TR 요청을 큐에 등록합니다.
        /// </summary>
        public void TR_등록(Func<Task> LS_API_메서드)
        {
            _일반_요청_큐.Enqueue(LS_API_메서드);
            _신호_세마포어.Release();
        }

        /// <summary>
        /// 잔고조회, 주문체결 확인 등 즉시 처리되어야 하는 TR을 최우선 처리 큐에 등록합니다.
        /// </summary>
        public void 긴급_TR_등록(Func<Task> LS_우선_API_메서드)
        {
            _긴급_요청_큐.Enqueue(LS_우선_API_메서드);
            _신호_세마포어.Release();
        }

        /// <summary>
        /// LS증권 제한 제한 규정을 회피하기 위한 모니터링 반복 제어문입니다.
        /// </summary>
        private async Task 큐_모니터링_루프(CancellationToken 중단_토큰)
        {
            while (!중단_토큰.IsCancellationRequested)
            {
                await _신호_세마포어.WaitAsync(중단_토큰);

                Func<Task> 처리할_작업 = null;

                // 긴급 처리 큐 우선 추출
                if (_긴급_요청_큐.TryDequeue(out 처리할_작업))
                {
                    // 긴급 큐 처리 성공
                }
                // 일반 처리 큐 추출
                else if (_일반_요청_큐.TryDequeue(out 처리할_작업))
                {
                    // 일반 큐 처리 성공
                }

                if (처리할_작업 != null)
                {
                    try
                    {
                        // LS증권 API 실제 전송 및 대기
                        await 처리할_작업();
                    }
                    catch (Exception 에러)
                    {
                        Console.WriteLine($"[LS증권 스케줄러 에러] {에러.Message}");
                    }

                    // LS증권 초당 조회 제한(유량 에러)을 회피하기 위한 딜레이 타임 적용
                    await Task.Delay(_정지_지연_밀리초, 중단_토큰);
                }
            }
        }

        /// <summary>
        /// 애플리케이션 종료 시 안전하게 백그라운드 태스크를 해제합니다.
        /// </summary>
        public void 스케줄러_정지()
        {
            _중단_토큰_소스.Cancel();
            try
            {
                _소비_프로세스.Wait();
            }
            catch (AggregateException) { }

            _신호_세마포어.Dispose();
            _중단_토큰_소스.Dispose();
        }
    }
}



