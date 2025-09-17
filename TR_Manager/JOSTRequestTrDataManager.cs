using System.Collections.Concurrent;
using System.Linq;
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

namespace 지니_64
{
    public class JOSTRequestTrDataManager
    {
        private static JOSTRequestTrDataManager jostRequestTrDataManager;

        ConcurrentQueue<Task> requestTaskQueue = new ConcurrentQueue<Task>(); //TR요청 Task Queue

        Thread taskWorker; // Task Queue에 쌓인 TR요청을 순차적으로 처리하는 쓰레드

        bool Stop_ = false;
        bool Dequeue_Run = true;

        private JOSTRequestTrDataManager()
        {
            taskWorker = new Thread(delegate ()
            {
                while (true)
                {
                    try
                    {
                        if (Stop_) break;

                        while (requestTaskQueue.Count > 0)
                        {
                            if (Dequeue_Run)
                            {
                                bool ok = requestTaskQueue.TryDequeue(out Task result);
                                if (ok)
                                {
                                    result.RunSynchronously();

                                    Thread.Sleep(300);
                                }
                            }
                        }
                        Thread.Sleep(200);
                    }
                    catch
                    {
                        Form1.Error_Log("[에러내역] JOSTRequestTrDataManager 의 심각한 에러발생");
                    }
                }
            });
        }

        public static JOSTRequestTrDataManager GetInstance()
        {
            if (jostRequestTrDataManager == null)
                jostRequestTrDataManager = new JOSTRequestTrDataManager();

            return jostRequestTrDataManager;
        }

        public void Run()
        {
            taskWorker.Start();
        }

        public void Stop()
        {
            Stop_ = true;
        }

        public void DequeueRun()
        {
            Dequeue_Run = true;
        }
        public void DequeueStop()
        {
            Dequeue_Run = false;
        }
        public int count()
        {
            return requestTaskQueue.Count;
        }

        public void RequestTrData(Task task) // Task 형식으로 TR Data Request를 전달받는다.
        {
            requestTaskQueue.Enqueue(task);
        }
    }

    public class CondotionManager
    {
        private static CondotionManager condotionManager;

        ConcurrentQueue<Task> CondotionTaskQueue = new ConcurrentQueue<Task>(); //TR요청 Task Queue

        Thread taskWorker; // Task Queue에 쌓인 TR요청을 순차적으로 처리하는 쓰레드

        bool Stop_ = false;
        bool Dequeue_Run = false;

        private CondotionManager()
        {
            taskWorker = new Thread(delegate ()
            {
                while (true)
                {
                    try
                    {
                        if (Stop_) break;

                        while (CondotionTaskQueue.Count > 0)
                        {
                            if (Dequeue_Run)
                            {
                                bool ok = CondotionTaskQueue.TryDequeue(out Task result);
                                if (ok)
                                {
                                    result.RunSynchronously();
                                    Thread.Sleep(250);
                                }
                            }
                        }
                        Thread.Sleep(300);
                    }
                    catch
                    {
                        Form1.Error_Log("[에러내역] CondotionManager 의 심각한 에러발생");
                    }
                }
            });
        }

        public static CondotionManager GetInstance()
        {
            if (condotionManager == null)
                condotionManager = new CondotionManager();

            return condotionManager;
        }

        public void Run()
        {
            taskWorker.Start();
        }
        public void Stop()
        {
            Stop_ = true;
        }

        public void DequeueRun()
        {
            Dequeue_Run = true;
        }

        public int Condotion_count()
        {
            return CondotionTaskQueue.Count();
        }

        public void RequestTrData(Task task) // Task 형식으로 TR Data Request를 전달받는다.
        {
            CondotionTaskQueue.Enqueue(task);
        }
    }


    public class Writing_Manager
    {
        private static Writing_Manager writing_Manager;

        ConcurrentQueue<Task> writingQueue = new ConcurrentQueue<Task>(); //TR요청 Task Queue

        Thread taskWorker; // Task Queue에 쌓인 TR요청을 순차적으로 처리하는 쓰레드

        bool Stop_ = false;

        private Writing_Manager()
        {
            taskWorker = new Thread(delegate ()
            {
                while (true)
                {
                    try
                    {
                        while (writingQueue.Count > 0)
                        {
                            bool ok = writingQueue.TryDequeue(out Task result);
                            if (ok)
                            {
                                result.RunSynchronously();
                            }
                        }

                        if (Stop_) break;

                        Thread.Sleep(10);
                    }
                    catch
                    {
                        Form1.Error_Log("[에러내역] Writing_Manager 의 심각한 에러발생");
                    }
                }
            });
        }

        public static Writing_Manager GetInstance()
        {
            if (writing_Manager == null)
                writing_Manager = new Writing_Manager();

            return writing_Manager;
        }

        public void Run()
        {
            taskWorker.Start();
        }

        public void Stop()
        {
            Stop_ = true;
        }

        public void RequestTrData(Task task) // Task 형식으로 TR Data Request를 전달받는다.
        {
            writingQueue.Enqueue(task);
        }
    }

    public class Today_condition_data
    {
        private static Today_condition_data condition_data_Manager;

        ConcurrentQueue<Task> condition_data_Queue = new ConcurrentQueue<Task>(); //TR요청 Task Queue

        Thread taskWorker; // Task Queue에 쌓인 TR요청을 순차적으로 처리하는 쓰레드

        bool Stop_ = false;

        private Today_condition_data()
        {
            taskWorker = new Thread(delegate ()
            {
                while (true)
                {
                    try
                    {
                        if (Stop_) break;

                        while (condition_data_Queue.Count > 0)
                        {
                            bool ok = condition_data_Queue.TryDequeue(out Task result);
                            if (ok)
                            {
                                result.RunSynchronously();
                            }
                        }

                        Thread.Sleep(10);
                    }
                    catch
                    {
                        Form1.Error_Log("[에러내역] Real_data_Manager 의 심각한 에러발생");
                    }
                }
            });
        }

        public static Today_condition_data GetInstance()
        {
            if (condition_data_Manager == null)
                condition_data_Manager = new Today_condition_data();

            return condition_data_Manager;
        }

        public void Run()
        {
            taskWorker.Start();
        }

        public void Stop()
        {
            Stop_ = true;
        }

        public void RequestTrData(Task task) // Task 형식으로 TR Data Request를 전달받는다.
        {
            condition_data_Queue.Enqueue(task);
        }
    }

    public class Real_data
    {
        private static Real_data Real_data_Manager;

        ConcurrentQueue<Task> Real_data_Queue = new ConcurrentQueue<Task>(); //TR요청 Task Queue

        Thread taskWorker; // Task Queue에 쌓인 TR요청을 순차적으로 처리하는 쓰레드

        bool Stop_ = false;

        private Real_data()
        {
            taskWorker = new Thread(delegate ()
            {
                while (true)
                {
                    try
                    {
                        if (Stop_) break;

                        while (Real_data_Queue.Count > 0)
                        {
                            bool ok = Real_data_Queue.TryDequeue(out Task result);
                            if (ok)
                            {
                                result.RunSynchronously();
                            }
                        }

                        Thread.Sleep(10);
                    }
                    catch
                    {
                        Form1.Error_Log("[에러내역] Real_price_Manager 의 심각한 에러발생");
                    }
                }
            });
        }

        public static Real_data GetInstance()
        {
            if (Real_data_Manager == null)
                Real_data_Manager = new Real_data();

            return Real_data_Manager;
        }

        public void Run()
        {
            taskWorker.Start();
        }

        public void Stop()
        {
            Stop_ = true;
        }

        public void RequestTrData(Task task) // Task 형식으로 TR Data Request를 전달받는다.
        {
            Real_data_Queue.Enqueue(task);
        }
    }

    public class Real_price
    {
        private static Real_price Real_price_Manager;

        ConcurrentQueue<Task> Real_price_Queue = new ConcurrentQueue<Task>(); //TR요청 Task Queue

        Thread taskWorker; // Task Queue에 쌓인 TR요청을 순차적으로 처리하는 쓰레드

        bool Stop_ = false;

        private Real_price()
        {
            taskWorker = new Thread(delegate ()
            {
                while (true)
                {
                    try
                    {
                        if (Stop_) break;

                        while (Real_price_Queue.Count > 0)
                        {
                            bool ok = Real_price_Queue.TryDequeue(out Task result);
                            if (ok)
                            {
                                result.RunSynchronously();
                            }
                        }

                        Thread.Sleep(10);
                    }
                    catch
                    {
                        Form1.Error_Log("[에러내역] Real_price_Manager 의 심각한 에러발생");
                    }
                }
            });
        }

        public static Real_price GetInstance()
        {
            if (Real_price_Manager == null)
                Real_price_Manager = new Real_price();

            return Real_price_Manager;
        }

        public void Run()
        {
            taskWorker.Start();
        }

        public void Stop()
        {
            Stop_ = true;
        }

        public void RequestTrData(Task task) // Task 형식으로 TR Data Request를 전달받는다.
        {
            Real_price_Queue.Enqueue(task);
        }
    }

    public class Real_Hoga
    {
        private static Real_Hoga Real_Hoga_Manager;

        ConcurrentQueue<Task> Real_Hoga_Queue = new ConcurrentQueue<Task>(); //TR요청 Task Queue

        Thread taskWorker; // Task Queue에 쌓인 TR요청을 순차적으로 처리하는 쓰레드

        bool Stop_ = false;

        private Real_Hoga()
        {
            taskWorker = new Thread(delegate ()
            {
                while (true)
                {
                    try
                    {
                        if (Stop_) break;

                        while (Real_Hoga_Queue.Count > 0)
                        {
                            bool ok = Real_Hoga_Queue.TryDequeue(out Task result);
                            if (ok)
                            {
                                result.RunSynchronously();
                            }
                        }

                        Thread.Sleep(10);
                    }
                    catch
                    {
                        Form1.Error_Log("[에러내역] Real_Hoga_Manager 의 심각한 에러발생");
                    }
                }
            });
        }

        public static Real_Hoga GetInstance()
        {
            if (Real_Hoga_Manager == null)
                Real_Hoga_Manager = new Real_Hoga();

            return Real_Hoga_Manager;
        }

        public void Run()
        {
            taskWorker.Start();
        }

        public void Stop()
        {
            Stop_ = true;
        }

        public void RequestTrData(Task task) // Task 형식으로 TR Data Request를 전달받는다.
        {
            Real_Hoga_Queue.Enqueue(task);
        }
    }



}








