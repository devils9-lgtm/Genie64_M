using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Telegram.Bot.Types;
using 지니64.RESTAPI;
using static 지니64.초기화;


namespace 지니64
{
    internal class Trade_tasks : Form1
    {
        public static async void 잔고_매매()
        {
            // Task.Run으로 감싸서 메인 UI와 완전히 분리된 백그라운드 스레드에서 실행
            _ = Task.Run(async () =>
            {
                await 매매_Loop_Async();
            });
        }

        // 통합된 병렬 매매 루프
        private static async Task 매매_Loop_Async()
        {
            // =================================================================
            // 1. 초기 로딩 로직 (로딩이 끝날 때까지 여기서 대기)
            // =================================================================
            while (!로딩완료)
            {
                if (form1 == null || form1.IsDisposed || form1.Disposing || Form_close) return;

                await ProcessLoadingStep(); // 하단에 분리된 메서드 참고
                await Task.Delay(1000);     // 로딩 중에는 1초 간격으로 체크하여 CPU 낭비 방지
            }

            Form1.Console_print("로딩 완료! 지니 병렬 매매 엔진 가동을 시작합니다.");

            // =================================================================
            // 2. 병렬 매매 루프 실행 (각자 독립된 타이머로 돌아감)
            // =================================================================
            var taskList = new List<Task>();

            // [루프 1] 250ms 주기 작업 (시세 및 예약주문 확인)
            taskList.Add(Loop_Action_Async(250, async () =>
            {
                Intervaler._250();
                await Form1.form1.예약주문_RUN();
                Condition_Management.검색식사용제한();
            }));

            // [루프 2] 500ms 주기 작업 (핵심 매매 로직)
            taskList.Add(Loop_Action_Async(500, async () =>
            {
                await Trading_Run();
            }));

            // [루프 3] 1000ms 주기 작업 (화면 출력 및 갱신)
            taskList.Add(Loop_Action_Async(1000, async () =>
            {
                await 유휴자금관리();
                Helper.안전한_UI_업데이트(Form1.form1, async () =>
                {
                    await Form_Print();
                });
            }));

            // 위 3개의 독립된 타이머 루프를 동시에 무한 실행
            await Task.WhenAll(taskList);
        }

        // =================================================================
        // [공통 헬퍼] 무한 비동기 루프 엔진 (에러 방어 + 지정 시간 대기)
        // =================================================================
        private static async Task Loop_Action_Async(int intervalMs, Action action)
        {
            while (true)
            {
                // 폼이 꺼지거나 종료 신호가 오면 깔끔하게 루프 탈출
                if (form1 == null || form1.IsDisposed || form1.Disposing || Form_close) break;

                try
                {
                    action(); // 할당된 작업 실행
                }
                catch (Exception ex)
                {
                    Form1.Console_print($"[엔진 오류] Loop({intervalMs}ms) 실행 중 에러: {ex.Message}");
                }

                // 10ms 강제 휴식이 아닌, 각자의 타이머 주기에 맞게 완벽한 비동기 휴식!
                await Task.Delay(intervalMs);
            }
        }

       
        // 로딩 절차를 깔끔하게 분리
        private static async Task ProcessLoadingStep()
        {
            // string 비교보다는 enum 사용이 좋지만, 기존 코드 호환을 위해 유지
            switch (매매시작)
            {
                case "Load_completion":
                    Load_completion();

                    Helper.안전한_UI_업데이트(form1, () =>
                    {
                        // 💡 [핵심] 서브폼이 화면에 표시된 직후 강제로 포커스를 이동시킵니다.
                        if (form1.JanGo_dataGridView != null && !form1.JanGo_dataGridView.IsDisposed)
                        {
                            form1.JanGo_dataGridView.Focus();    // 포커스 이동
                        }
                    });

                    break;

                case "Setting_로딩완료":
                    Console_print(" ------------ Load_completion  매매시작 = 주문내역요청 = > 차트 조회 ------------- ");

                    if (Get.시장종료 < Get.TimeNow)
                    {
                        // ToList() 제거하고 Values 순회
                        foreach (var 잔고 in stockBalanceList.Values)
                        {
                            info.요청(잔고.종목코드, "info_loading", "", false);
                        }
                    }

                    // ToList()를 한 번만 호출하여 변수에 담고 사용 (또는 Values 사용)
                    var currentStockList = stockBalanceList.Values; // 수정 불가피한 경우에만 ToList

                    foreach (var 잔고 in currentStockList)
                    {
                        REG.실시간시세등록(잔고.종목코드);
                    }

                    foreach (var 잔고 in currentStockList)
                    {
                      Form1.차트로딩_남은수++; // ◀ 요청하기 직전에 1 증가
                        TR_요청.주식분봉차트조회요청(잔고.종목코드, false);
                    }

                    foreach (var 잔고 in currentStockList)
                    {
                        Form1.차트로딩_남은수++; // ◀ 요청하기 직전에 1 증가
                        TR_요청.주식일봉차트조회요청(잔고.종목코드, false);
                    }
                    //foreach (var 잔고 in currentStockList) REG.실시간시세등록(잔고.종목코드);
                    //foreach (var 잔고 in currentStockList) TR_요청.주식분봉차트조회요청(잔고.종목코드, false);
                    //foreach (var 잔고 in currentStockList) TR_요청.주식일봉차트조회요청(잔고.종목코드, false);

                    매매시작 = "차트조회요청";
                    break;

                case "차트조회요청":
                    SaveToFile.잔고_파일저장();
                    LogAndConsole(GenieConfig.textBox_계좌번호 + " 잔고 요청 완료");

                    Tab_AccountManagement.감시주문동기화();
                    LogAndConsole(GenieConfig.textBox_계좌번호 + " 감시주문 동기화 완료");

                    Order_Reserve.예약주문동기화();
                    LogAndConsole(GenieConfig.textBox_계좌번호 + " 예약주문 동기화 완료");

                    LoadFromFile.Rebuystock_파일로딩();
                    LogAndConsole(GenieConfig.textBox_계좌번호 + " Rebuystock_파일로딩 완료");

                    REG.업종실시간등록();
                    매매시작 = "실시간등록";
                    break;

                //case "실시간등록":
                //    int count = tr_scheduler.QueueCount;

                //    if(Helper.IsDebugMode)
                //    {
                //        count = 0;
                //    }

                //    Helper.안전한_UI_업데이트(form1, () =>
                //    {
                //        Console_print("#####  차트로딩 중: " + DateTime.Now.ToString("HH:mm:ss.ffff") + " 남은수 : " + count);
                //        form1.LB_Log.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss :: ") + "차트로딩 중 - " + count);
                //    });

                //    if (count == 0)
                //    {
                //        매매시작 = "주문로딩완료";
                //        int.TryParse(DateTime.Now.ToString("HHmmss"), out int Time);
                //        Get.로딩완료타임 = Time;
                //    }

                //    break;

                case "실시간등록":
                    // 수정 전: int count = tr_scheduler.QueueCount;
                    // 수정 후: 백그라운드 TR과 무관하게 차트 남은 수만 체크합니다.
                    int count = Form1.차트로딩_남은수;

                    if (Helper.IsDebugMode)
                    {
                        count = 0;
                    }

                    Helper.안전한_UI_업데이트(form1, () =>
                    {
                        Console_print("#####  차트로딩 중: " + DateTime.Now.ToString("HH:mm:ss.ffff") + " 남은수 : " + count);
                        form1.LB_Log.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss :: ") + "차트로딩 중 - " + count);
                    });

                    if (count == 0)
                    {
                        매매시작 = "주문로딩완료";
                        int.TryParse(DateTime.Now.ToString("HHmmss"), out int Time);
                        Get.로딩완료타임 = Time;

                        // (선택) 로딩 완료 로그 추가
                        Console_print("#####  차트 로딩 완료. 메인 프로세스 시작.");
                    }

                    break;

                case "주문로딩완료":

                    Helper.안전한_UI_업데이트(Form1.form1, () =>
                    {
                        // [+] 딕셔너리에 값이 존재하는지 먼저 안전하게 확인한 뒤 UI에 적용합니다. (KeyNotFoundException 방어)
                        if (Form1.위치별검색식리스트.TryGetValue("신규_A", out var 신규A값)) { Form_Basic.form.신규_A.Items.Add(신규A값.이름); Form_Basic.form.신규_A.Text = 신규A값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("신규_B", out var 신규B값)) { Form_Basic.form.신규_B.Items.Add(신규B값.이름); Form_Basic.form.신규_B.Text = 신규B값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("신규_C", out var 신규C값)) { Form_Basic.form.신규_C.Items.Add(신규C값.이름); Form_Basic.form.신규_C.Text = 신규C값.이름; }

                        // [+] 딕셔너리에 값이 존재하는지 먼저 안전하게 확인한 뒤 UI에 적용합니다. (KeyNotFoundException 방어)
                        if (Form1.위치별검색식리스트.TryGetValue("반복_A", out var 반복A값)) { Form_Repeat.form.반복_A.Items.Add(반복A값.이름); Form_Repeat.form.반복_A.Text = 반복A값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("반복_B", out var 반복B값)) { Form_Repeat.form.반복_B.Items.Add(반복B값.이름); Form_Repeat.form.반복_B.Text = 반복B값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("반복_C", out var 반복C값)) { Form_Repeat.form.반복_C.Items.Add(반복C값.이름); Form_Repeat.form.반복_C.Text = 반복C값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("반복_D", out var 반복D값)) { Form_Repeat.form.반복_D.Items.Add(반복D값.이름); Form_Repeat.form.반복_D.Text = 반복D값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("반복_E", out var 반복E값)) { Form_Repeat.form.반복_E.Items.Add(반복E값.이름); Form_Repeat.form.반복_E.Text = 반복E값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("반복_F", out var 반복F값)) { Form_Repeat.form.반복_F.Items.Add(반복F값.이름); Form_Repeat.form.반복_F.Text = 반복F값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("반복_G", out var 반복G값)) { Form_Repeat.form.반복_G.Items.Add(반복G값.이름); Form_Repeat.form.반복_G.Text = 반복G값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("반복_H", out var 반복H값)) { Form_Repeat.form.반복_H.Items.Add(반복H값.이름); Form_Repeat.form.반복_H.Text = 반복H값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("반복_I", out var 반복I값)) { Form_Repeat.form.반복_I.Items.Add(반복I값.이름); Form_Repeat.form.반복_I.Text = 반복I값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("반복_J", out var 반복J값)) { Form_Repeat.form.반복_J.Items.Add(반복J값.이름); Form_Repeat.form.반복_J.Text = 반복J값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("반복_K", out var 반복K값)) { Form_Repeat.form.반복_K.Items.Add(반복K값.이름); Form_Repeat.form.반복_K.Text = 반복K값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("반복_L", out var 반복L값)) { Form_Repeat.form.반복_L.Items.Add(반복L값.이름); Form_Repeat.form.반복_L.Text = 반복L값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("반복_M", out var 반복M값)) { Form_Repeat.form.반복_M.Items.Add(반복M값.이름); Form_Repeat.form.반복_M.Text = 반복M값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("반복_N", out var 반복N값)) { Form_Repeat.form.반복_N.Items.Add(반복N값.이름); Form_Repeat.form.반복_N.Text = 반복N값.이름; }

                        // [+] 딕셔너리에 값이 존재하는지 먼저 안전하게 확인한 뒤 UI에 적용합니다. (KeyNotFoundException 방어)
                        if (Form1.위치별검색식리스트.TryGetValue("리밸_A", out var 리밸A값)) { Form_AccountManagement.form.리밸_A.Items.Add(리밸A값.이름); Form_AccountManagement.form.리밸_A.Text = 리밸A값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("리밸_B", out var 리밸B값)) { Form_AccountManagement.form.리밸_B.Items.Add(리밸B값.이름); Form_AccountManagement.form.리밸_B.Text = 리밸B값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("리밸_C", out var 리밸C값)) { Form_AccountManagement.form.리밸_C.Items.Add(리밸C값.이름); Form_AccountManagement.form.리밸_C.Text = 리밸C값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("리밸_D", out var 리밸D값)) { Form_AccountManagement.form.리밸_D.Items.Add(리밸D값.이름); Form_AccountManagement.form.리밸_D.Text = 리밸D값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("리밸_E", out var 리밸E값)) { Form_AccountManagement.form.리밸_E.Items.Add(리밸E값.이름); Form_AccountManagement.form.리밸_E.Text = 리밸E값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("리밸_F", out var 리밸F값)) { Form_AccountManagement.form.리밸_F.Items.Add(리밸F값.이름); Form_AccountManagement.form.리밸_F.Text = 리밸F값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("리밸_G", out var 리밸G값)) { Form_AccountManagement.form.리밸_G.Items.Add(리밸G값.이름); Form_AccountManagement.form.리밸_G.Text = 리밸G값.이름; }

                        if (Form1.위치별검색식리스트.TryGetValue("청산_A", out var 청산A값)) { Form_AccountManagement.form.청산_A.Items.Add(청산A값.이름); Form_AccountManagement.form.청산_A.Text = 청산A값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("청산_B", out var 청산B값)) { Form_AccountManagement.form.청산_B.Items.Add(청산B값.이름); Form_AccountManagement.form.청산_B.Text = 청산B값.이름; }
                        if (Form1.위치별검색식리스트.TryGetValue("청산_C", out var 청산C값)) { Form_AccountManagement.form.청산_C.Items.Add(청산C값.이름); Form_AccountManagement.form.청산_C.Text = 청산C값.이름; }

                        form1.panel_기능버튼.Enabled = true;
                        form1.CB_세로보기.Enabled = true;
                    
                        if (Sub_form.form != null && !Sub_form.form.IsDisposed)
                        {
                            Sub_form.form.CB_Layout_A.Enabled = true;
                            Sub_form.form.CB_Layout_B.Enabled = true;
                            Sub_form.form.CB_Layout_C.Enabled = true;
                            Sub_form.form.CB_Layout_X.Enabled = true;
                            Sub_form.form.CB_통계.Enabled = true;
                        }
                    });

                    매매시작 = "매매시작";
                    로딩완료 = true;
                    Console_print("매매시작");

                    Log.동작기록("MoneyGame 에 입장 하였습니다.");

                    Helper.안전한_UI_업데이트(Form1.form1, () =>
                    {
                        Form1.form1.CB_세로보기.Checked = GenieConfig.CB_세로보기;

                        if (GenieConfig.CB_세로보기)
                        {
                            // [수정] 서브폼이 없다면 즉시 생성(인스턴스화)하여 에러 방지
                            if (Sub_form.form == null || Sub_form.form.IsDisposed)
                            {
                                // 폼을 새로 생성합니다. 
                                // 주의: 이 시점에서 폼의 생성자(Constructor)와 Load 이벤트가 실행됩니다.
                                Sub_form.form = new Sub_form();
                            }

                            // 이제 서브폼이 반드시 존재하므로 안전하게 호출합니다.
                            Sub_form.form.레이아웃_설정_불러오기();
                        }
                    
                    });

                    break;
            }
        }


        private static void Load_completion()
        {
            int.TryParse(DateTime.Now.ToString("HHmmss"), out int Time);

            Console_print("/////////////////////////// 로딩완료  :: " + DateTime.Now.ToString("hh:mm:ss.ffff") + " ///////////////////////////");

            // ---------------------------------------------------------
            // 시간 감시 및 상태 체크 (수정됨)
            // ---------------------------------------------------------

            // [특수 매매] 설정의 시간 값들 (Setting.special)
            if (예약주문_장전 && GenieConfig.MTB_예약주문_장전주문시간 < Time) 예약주문_장전 = false;
            if (예약주문_종가 && GenieConfig.MTB_예약주문_종가주문시간 < Time) 예약주문_종가 = false;
            if (매매기간_오전 && GenieConfig.TB_매매기간_오전주문시간 < Time) 매매기간_오전 = false;
            if (매매기간_오후 && GenieConfig.TB_매매기간_오후주문시간 < Time) 매매기간_오후 = false;

            // (감시 시간은 변수(Get)를 사용하므로 수정 없음)
            if (form1.오전감시 && Get.오전감시시간 < Time) form1.오전감시 = false;
            if (form1.오후감시 && Get.오후감시시간 < Time) form1.오후감시 = false;

            // [계좌 기본] 설정의 시작 시간 (Setting.acc)
            if (GenieConfig.MT_starttime - 30 > Get.TimeNow)
            {
                if (신규매수정지 || 추가매수정지)
                {
                    string 신규 = "가능";
                    if (신규매수정지)
                        신규 = "정지";

                    string 추가 = "가능";
                    if (추가매수정지)
                        추가 = "정지";

                    AutoClosingAlram("신규매수 - [ " + 신규 + " ], 추가매수 - [ " + 추가 + " ] 입니다. \n그룹n기능의 설정을 확인 해주세요.", "매수 정지알림", 30, "동작");
                }
            }

            // [기능] 설정의 음소거 (Setting.function)
            Form1.음소거 = GenieConfig.CB_음소거;

            if (릴리즈) login.접속확인();

            Helper.안전한_UI_업데이트(form1, () =>
            {
                Check_모의투자.Print();
                form1.와치_A.Enabled = true;
                form1.와치_B.Enabled = true;
                form1.와치_C.Enabled = true;
                form1.와치_D.Enabled = true;

                if (GenieConfig.CB_가이드매매)
                {
                    Guide.가이드매매설정로딩();
                }
                else
                {
                    Console_print(GenieConfig.textBox_계좌번호 + " 검색식 로딩");
                    Condition_Management.Condition_DataLoad();
                }

                특정일시작시간조정();
            });
        }

        private static void 특정일시작시간조정()
        {
            string 날짜 = DateTime.Now.ToString("MMdd");

            if (날짜.StartsWith("01"))
            {
                if (str.개장일.Equals(날짜))
                {
                    Helper.알림창_멀티("개장일알림","새해 첫 개장일 입니다.\n프리마켓은 열리지 않습니다.\n정규장 시작시간은 10시00분 종료시간은 15시30분 입니다.", 30, false);

                   Log.동작기록("");
                   Log.동작기록("[개장일알림] 프리마켓은 열리지 않습니다.");
                   Log.동작기록("[개장일알림] 정규장 시작시간은 10시00분 종료시간은 15시30분 입니다.");
                   Log.동작기록("[개장일알림] 새해 첫 개장일 입니다.");
                   Log.동작기록("");

                    CB개장일 = true;
                    Get.메인마켓시작 += 10000;
                }
            }
            else
            {
                if (str.수능일.Equals(날짜))
                {
                    Helper.알림창_멀티("수능일알림","수능일 입니다.\n프리마켓은 열리지 않습니다.\n정규장 시작시간은 10시00분 종료시간은 16시30분 입니다.\n시작시간과 종료시간이 자동으로 적용됩니다.", 30, false);

                   Log.동작기록("");
                   Log.동작기록("[수능일알림] 프리마켓은 열리지 않습니다.");
                   Log.동작기록("[수능일알림] 정규장 시작시간은 10시00분 종료시간은 16시30분 입니다.");
                   Log.동작기록("[수능일알림] 시작시간과 종료시간이 자동으로 적용됩니다.");
                   Log.동작기록("[수능일알림] 수능일 입니다.");
                   Log.동작기록("");

                    if (!CB수능일)
                    {
                        CB수능일 = true;
                        FormPrint.CB_개장일n수능일_Checked("CB_수능일");
                    }
                }
                else
                {
                    if (CB수능일)
                    {
                        CB수능일 = false;
                        FormPrint.CB_개장일n수능일_Checked("CB_수능일");
                    }
                }
            }

            매매시작 = "Setting_로딩완료";
        }

        // 헬퍼 함수
        static void LogAndConsole(string message)
        {
            Log.동작기록(message);
            Console_print(message);
        }


        // ★ CPU/메모리 최적화된 매매 로직
        private static async Task Trading_Run()
        {
            // 1. 공통 조건 체크 (진입 장벽)
            // 서버 알림 체크가 빈번하므로 가장 먼저 확인
            if (!server_알림.Contains("마켓")) return;

            // 시뮬레이션 모드 체크 간소화
            if (GenieConfig.checkBox_Simulation && server_알림 != "메인마켓") return;

            // 2. 미수 정리 로직
            if (form1.RB_sell_run.Checked)
            {
                if (미수금정리 != "종료") await Misu_liquidation.미수정리();

                if (미수금정리 == "대기" || 미수금정리 == "종료")
                {
                    // Tab_Basic.계좌청산(); // 이 함수가 무거우면 필요할 때만 호출하도록 조건 추가 고려
                    if (server_알림 == "메인마켓") Tab_AccountManagement.수익금기준손절();
                }
            }

            // 3. 잔고 순회 최적화 (가장 중요한 부분!)
            // ToList()를 제거하고 Values를 직접 순회합니다. 
            // 단, 루프 도중 stockBalanceList가 변경되면 에러가 날 수 있으니, 
            // stockBalanceList가 동시성 컬렉션(ConcurrentDictionary)이 아니라면 lock을 걸거나 Keys만 복사해야 합니다.
            // 여기서는 메모리 절약을 위해 Values 순회를 추천하되, 안전장치로 try-catch를 두거나 Keys 복사를 권장합니다.

            // 4. 공통 변수 캐싱
            bool isBuyRun = form1.매수_ON;
            bool isSellRun = form1.매도_ON;

            // ★ 메모리 최적화: ToList() 제거하고 Values 사용
            foreach (var 잔고 in stockBalanceList.Values)
            {
                // 5. 중첩 if문 펼치기 (CPU 분기 예측 효율화)
                // 거래정지나 동시호가면 다음 종목으로 패스 (Guard Clause)
                if (잔고.종목상태.Contains("거래정지") || 잔고.종목상태.Contains("동시호가")) continue;
                if (미수금정리 != "대기" && 미수금정리 != "종료") continue;

                // 주문 가능 수량이 없으면 대부분의 로직 스킵
                if (GET.총주문가능수량(잔고) <= 0) continue;

                // 6. 실제 매매 로직 실행 (주문가능수량 > 0 인 경우만 여기까지 도달함)
                if (isBuyRun || isSellRun)
                {
                    Method.매입금매매제한(잔고);
                    if (CanTrade.반복매매_USE) await Tab_Repeat.반복매매_USE(잔고);
                    if (CanTrade.Rebalancing_USE) await Tab_AccountManagement.Rebalancing_USE(잔고);

                    if (isSellRun)
                    {
                        if (CanTrade.Liquidation_USE) await Tab_AccountManagement.Liquidation_USE(잔고);
                        if (CanTrade.TimeSell) Tab_Basic.TimeSell(잔고);
                        if (감시주문_List.Count > 0) Tab_AccountManagement.리밸감시_감시중(잔고);
                        if (CanTrade.잔고_익절) Tab_Basic.잔고_익절(잔고);
                        if (CanTrade.잔고_보전) Tab_Basic.잔고_보전(잔고);
                        if (CanTrade.잔고_손실매도) Tab_Basic.잔고_손실매도(잔고);
                        if (CanTrade.트레일링스탑) Tab_Basic.트레일링스탑(잔고);
                        if (CanTrade.상하_전량매도) Tab_Basic.상하_전량매도(잔고);
                    }

                    if (CanTrade.그룹지정매매) Tab_Special.그룹지정매매(잔고);
                    if (CanTrade.매매일_기준거래) Tab_Special.매매일_기준거래(잔고, "리얼");
                }
            }
        }

        public static async Task Form_Print()
        {
            if (server_알림.Contains("마켓") || server_알림.Contains("동시"))
            {
                await Intervaler.Delay_timer();
                Intervaler.Threadcount();
            }

            _ = Intervaler.UpdateNowDateTime();
            FormPrint.acc_print();
            홀딩잔고.JanGo_dataGridView_print();
            GridView_Print.OUT_DGV_print();
            if (GenieConfig.CBB_Watch_ID_A > 0) Tab_Watch.DGV_watch_print("A:");
            if (GenieConfig.CBB_Watch_ID_B > 0) Tab_Watch.DGV_watch_print("B:");
            if (GenieConfig.CBB_Watch_ID_C > 0) Tab_Watch.DGV_watch_print("C:");
            if (GenieConfig.CBB_Watch_ID_D > 0) Tab_Watch.DGV_watch_print("D:");

            Tab_InterestGroup.관심자동등록실행();
            Condition_Management.검색식사용불가_강제정지();
            FormPrint.동작상태();
        }


        public static async Task 유휴자금관리()
        {
            // 1. [최적화] 가장 가벼운 bool 검사부터 수행하여 조건 불만족 시 즉시 차단
            if (!GenieConfig.CB_자금관리) return;

            // 매수, 매도 버튼이 둘 다 꺼져 있을 때만 유휴자금관리를 완전히 중단합니다.
            if (!form1.매수_ON && !form1.매도_ON) return;

            // 2. [최적화] 무거운 문자열 검색은 bool 검사 통과 후에만 실행
            if (server_알림 != null && server_알림 != "메인마켓") return;

            string 종목코드 = GenieConfig.label_관리종목코드;
            long 유지현금 = GenieConfig.TB_자금관리_유지현금;

            // 3. [최적화] 로컬 캐싱 데이터 확인
            long 현재예수금 = Form1.Acc.D2;

            if (현재예수금 == 유지현금) return;

            if (!Form1.Market_Item_List.TryGetValue(종목코드, out Market_Item etf)) return;

            // [+] 현재가가 0원일 경우 5초 간격으로 정보 재요청 (무한 대기 방지 루프)
            int 재요청횟수 = 0;

            while (etf.현재가 <= 0)
            {
                // 방어막: 최대 12회(총 60초)까지만 요청하고, 그래도 안 들어오면 이번 턴은 포기
                if (재요청횟수 >= 12) return;

                info.요청(etf.종목코드, "info_자금관리", "", false);
                await Task.Delay(5000); // 5초 대기

                // 5초 대기 후 서버에서 값이 들어왔을 수 있으므로 최신 데이터로 다시 갱신
                if (Form1.Market_Item_List.TryGetValue(종목코드, out Market_Item 최신etf))
                {
                    etf = 최신etf;
                }

                재요청횟수++;
            }

            if (etf.현재가 <= 0) return;

            // 💡 [핵심] API 엇박자 핑퐁을 막기 위한 1주 가격 범퍼
            long 여유버퍼 = etf.현재가;

            // =========================================================================
            // [매수] 장 종료 5분 전 전용 로직 (TR 요청 포함)
            // =========================================================================
            int 매수시작시간 = GET.범용_시간계산(Form1.Get.메인마켓종료, 더할_분: -15);

            // 1차 필터: 시간이 5분 전으로 진입했고, 로컬 데이터상 돈이 범퍼 이상 남을 때
            if (Get.TimeNow >= 매수시작시간 && 현재예수금 > 유지현금 + 여유버퍼)
            {
                // 🛡️ [API 과부하 방어] 이미 주문이 나갔다면 여기서 차단되어 TR을 쏘지 않음
                if (Jumun.메인장부_중복_검사(종목코드, "유휴자금관리_매수", null)) return;

                // --- [진짜 D+2 요청 구간] 매수 상황에 딱 한 번만 실행됨 ---
                TR_요청.계좌평가현황요청("Y", "", false);
                await Task.Delay(5000);
                현재예수금 = Form1.Acc.D2; // 서버에서 받은 진짜 최신 돈으로 덮어쓰기
                // -------------------------------------------------------------

                // 2차 필터: 진짜 최신 돈으로 봐도 돈이 확실히 남는지 최종 확인
                if (현재예수금 > 유지현금 + 여유버퍼)
                {
                    long 남는여윳돈 = 현재예수금 - 유지현금;
                    int 실제주문수량 = (int)(남는여윳돈 / etf.현재가);

                    // 🛡️ [수수료 핑퐁 방어] 수수료 포함 진짜 지출액 계산
                    if (실제주문수량 > 0)
                    {
                        long 예상매수금액 = (long)실제주문수량 * etf.현재가;
                        long 예상수수료 = (long)((예상매수금액 * Form1.수수료) / 10) * 10;

                        if (예상매수금액 + 예상수수료 > 남는여윳돈)
                        {
                            실제주문수량 -= 1;
                        }
                    }

                    if (실제주문수량 <= 0) return;

                    int Order번호 = GET.Order번호();
                    string Screennum = GET.JumunScreen();

                    // [장부 업데이트] 매수이므로 예수금을 깎습니다.
                    홀딩잔고.예수금업데이트("매수", etf.현재가, 실제주문수량, "주문", 종목코드, false);

                    JumunItem 새주문 = new JumunItem
                    {
                        신용주문 = false,
                        대출일 = "",
                        Deletetimer = 0,
                        Screennum = Screennum,
                        종목코드 = etf.종목코드,
                        종목명 = etf.종목명,
                        주문번호 = "+++",
                        원주문번호 = "---",
                        검색식 = "유휴자금관리_매수",
                        주문값 = 0,
                        시장가구분 = 0,
                        취소시간 = 600,
                        취소N주문 = 0,
                        반복횟수 = 0,
                        비고 = "여윳돈파킹",
                        Pos = "유휴자금관리",
                        주문수량 = 실제주문수량,
                        주문가격 = etf.현재가,
                        매수매도 = 1,
                        비중 = 실제주문수량,
                        비중단위 = 0,
                        취소timer = 600,
                        현재가 = etf.현재가,
                        등락률 = 0,
                        주문시간 = Get.TimeNow,
                        미체결량 = 실제주문수량,
                        주문취소 = true,
                        가동전 = false,
                        Tik_cap = Method.Find_Tik_Cap(etf.현재가, etf.현재가, etf.Market),
                        Tik_price = etf.현재가,
                        수익률 = 0,
                        주문동기화 = false,
                        감시번호 = 0,
                        Order번호 = Order번호,
                        수익구분 = 0,
                        NXT = NXT_server,
                        주문시간_Ticks = DateTime.Now.Ticks
                    };

                    await Jumun.Add(새주문);
                    ExecuteTrade.Que_order(새주문);

                    await Task.Delay(3000);
                    TR_요청.계좌평가현황요청("Y", "", false);
                }
            }
            // =========================================================================
            // [매도] 상시 감시 로직 (TR 생략, 로컬 데이터 + 1주 범퍼로 초고속 처리)
            // =========================================================================
            else if (현재예수금 < 유지현금 - 여유버퍼)
            {
                if (Jumun.메인장부_중복_검사(종목코드, "유휴자금관리_매도", null)) return;

                long 부족현금 = 유지현금 - 현재예수금;

                // 🛡️ [매도 핑퐁 방어] 팔았을 때 수수료 떼고 '내 계좌에 실제로 꽂히는 돈' 기준 계산
                double tax_rate = (etf.Market == "E") ? 0 : Form1.TAX;
                long 주당_예상수수료 = (long)((etf.현재가 * Form1.수수료) / 10) * 10;
                long 주당_예상세금 = (long)(etf.현재가 * tax_rate);

                long 주당_실수령액 = etf.현재가 - 주당_예상수수료 - 주당_예상세금;
                if (주당_실수령액 <= 0) 주당_실수령액 = etf.현재가;

                // 초고속 정수 올림 연산
                int 필요매도수량 = (int)((부족현금 + 주당_실수령액 - 1) / 주당_실수령액);

                if (!Form1.stockBalanceList.TryGetValue(종목코드, out Stockbalance 잔고) || 잔고.주문가능수량 <= 0)
                    return;

                int 실제주문수량 = Math.Min(필요매도수량, 잔고.주문가능수량);

                if (실제주문수량 <= 0) return;

                ExecuteTrade.잔고주문_오더(
                    잔고: 잔고, 
                    검색식: "유휴자금관리_매도", 
                    매수매도: 2, 
                    비중: 실제주문수량,
                    비중단위: 0,
                    주문값: 0, 
                    시장가구분: 0, 
                    취소시간: 600, 
                    취소N주문: 0, 
                    반복횟수: 0,
                    비고: "",
                    위치: "유휴자금관리", 
                    수익구분: 0, 
                    청산: false, 
                    범위_1: 0
                );

                await Task.Delay(3000);
                TR_요청.계좌평가현황요청("Y", "", false);
            }
        }


    }
}