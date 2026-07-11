using System;
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
}



