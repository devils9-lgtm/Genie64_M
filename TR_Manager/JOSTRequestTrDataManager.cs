using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;



/* <-- 여기부터
 * 
 * 초당 5회 요청을 하되, 1분에 100회가 넘지 않는 경우에는 200ms 이상
 * 1분에 100회가 넘지만 1시간에 1000회가 넘지 않는 경우에는 600ms 이상
 * 1시간에 1000회 요청 이상이 필요한 경우 3600ms 이상
 * 값을 변경해 줄 필요가 있습니다. (단위는 ms)
 * 
 *<-- 여기까지
 */

namespace 지니64
{
    
    public class UnifiedDataManager
    {
        // 1. 싱글톤 인스턴스 (Lazy 사용으로 스레드 안전성 보장)
        private static readonly Lazy<UnifiedDataManager> _instance =
            new Lazy<UnifiedDataManager>(() => new UnifiedDataManager());

        public static UnifiedDataManager Instance => _instance.Value;

        // 2. 업무별 전담 작업자(Worker) 정의
        // --------------------------------------------------------------------------
        // [Condition]: 조건검색. 딜레이 없음.
        public readonly Worker Condition = new Worker("Condition_Manager");
        
        // [Writing]: 파일/DB 저장. 딜레이 없음.
        public readonly Worker Writing = new Worker("Writing_Manager");
        
        // [Real]: 실시간 데이터 3형제 (가장 빠름, 딜레이 없음).
        public readonly Worker RealData = new Worker("Real_Data");
        public readonly Worker RealPrice = new Worker("Real_Price");
        public readonly Worker RealHoga = new Worker("Real_Hoga");
        // --------------------------------------------------------------------------

        private List<Worker> _allWorkers;

        private UnifiedDataManager()
        {
            // 관리 편의를 위해 리스트에 담음
            _allWorkers = new List<Worker> { Condition, Writing, RealData, RealPrice, RealHoga };
        }

        // 전체 시작 (Form_Load)
        public void StartAll()
        {
            _allWorkers.ForEach(w => w.Run());
        }

        // 전체 종료 (Form_Closing)
        public void StopAll()
        {
            _allWorkers.ForEach(w => w.Stop());
        }

        // =========================================================
        // [핵심] 만능 작업자 클래스 (Producer-Consumer 패턴 + 기능 확장)
        // =========================================================
        public class Worker
        {
            private BlockingCollection<Action> _queue;
            private Task _workerTask;
            private readonly string _name;

            // 기능 1: 일시정지 제어용 이벤트 (CPU 사용 없이 대기 가능)
            // true = 실행 중, false = 일시정지
            private ManualResetEventSlim _pauseHandle = new ManualResetEventSlim(true);

            // 기능 2: 작업 간 속도 조절 (ms 단위)
            private int _interval = 0;

            public Worker(string name)
            {
                _name = name;
                _queue = new BlockingCollection<Action>();
            }

            // 설정: 작업 간 딜레이 시간 (API 제한 속도 맞춤)
            public void SetInterval(int ms)
            {
                _interval = ms;
            }

            // 동작: 일시정지 (DequeueStop 대체)
            public void Pause()
            {
                _pauseHandle.Reset();
            }

            // 동작: 재개 (DequeueRun 대체)
            public void Resume()
            {
                _pauseHandle.Set();
            }

            public void Run()
            {
                if (_workerTask != null && !_workerTask.IsCompleted) return;
                if (_queue.IsAddingCompleted) _queue = new BlockingCollection<Action>();

                // 스레드 풀에서 '오래 걸리는 작업'용 스레드를 할당받아 실행
                _workerTask = Task.Factory.StartNew(ProcessQueue, TaskCreationOptions.LongRunning);
            }

            private void ProcessQueue()
            {
                // 큐에 데이터가 없으면 여기서 스레드가 대기 상태(Sleep)가 됩니다. (CPU 0%)
                foreach (var job in _queue.GetConsumingEnumerable())
                {
                    try
                    {
                        // 1. 일시정지 상태 체크 (Paused 상태면 여기서 멈춤)
                        _pauseHandle.Wait();

                        // 2. 작업 실행
                        job.Invoke();

                        // 3. 속도 제한이 걸려있다면 대기 (JOST, Condition 등)
                        if (_interval > 0)
                        {
                            Thread.Sleep(_interval);
                        }
                    }
                    catch (Exception ex)
                    {
                        // 에러 로그 (프로그램 중단 방지)
                        System.Diagnostics.Debug.WriteLine($"[{_name} Error] {ex.Message}");
                    }
                }
            }

            public void Stop()
            {
                _queue.CompleteAdding(); // 더 이상 작업 안 받음
                _pauseHandle.Set();      // 멈춰있다면 깨워서 종료시킴
            }

            // 작업 요청 넣기 (Task 대신 Action 사용)
            public void Enqueue(Action job)
            {
                if (!_queue.IsAddingCompleted)
                {
                    _queue.Add(job);
                }
            }

            // 대기 중인 작업 수
            public int Count => _queue.Count;
        }
    }
}








