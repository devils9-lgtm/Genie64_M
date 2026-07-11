using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64
{
    public class Intervaler : Form1
    {
        public static void _250()
        {
            Get.Timer_S++;
            Get.Timer_M++;
            Get.Timer_H++;

            if (Get.Timer_S.Equals(4))
            {
                Get.Timer_S = 0;
                Get.주문_S = 0;
            }
            if (Get.Timer_M.Equals(240))
            {
                Get.Timer_M = 0;
                Get.주문_M = 0;
            }
            if (Get.Timer_H.Equals(14400))
            {
                Get.Timer_H = 0;
                Get.주문_H = 0;
            }

            // =================================================================
            // 1. [백그라운드 연산 구역] - 문자열 조립, 연산 등 (알바생 구역)
            // =================================================================
            DateTime now = DateTime.Now;
            string Time = now.ToLongTimeString();
            Get.TimeNow = int.Parse(now.ToString("HHmmss"));

            // ---------------------------------------------------------------
            // 💡 증권사 서버 연결 상태 계산 (PING 감시 및 단절 기록)
            // ---------------------------------------------------------------
            TimeSpan 서버멈춘시간 = now - Form1.서버_마지막_통신시간;
            string 서버통신_상태 = "";

            if (서버멈춘시간.TotalSeconds > 60)
            {
                // 1. [수정 완료] 끊긴 시간을 가장 먼저 뽑아냅니다. 그래야 아래 상태창에서 바로 쓸 수 있어요!
                string 끊긴시간 = Form1.서버_마지막_통신시간.ToString("HH:mm:ss");

                // 2. 상태창에 끊긴 시간을 띄워줍니다! (경고등이니까 빨간불 🔵 로 바꿨습니다)
                서버통신_상태 = $" | 📡 서버응답없음 🔵 마지막 통신:({끊긴시간})";

                // 💡 [핵심] 끊긴 '그 순간' 딱 한 번만 로그에 기록합니다! (로그 폭탄 방지)
                if (Form1.서버연결끊김_기록됨 == false)
                {
                    Form1.서버연결끊김_기록됨 = true; // 기록 완료 도장 쾅!

                    // 20초로 변경하셨길래 텍스트도 20초에 맞게 수정했습니다.
                    Form1.단절원인 = "60초 이상 서버 응답(PING) 없음 (타임아웃/네트워크 불안정)";

                    // 동작 로그에 빨간 글씨나 경고 느낌으로 남깁니다.
                    Log.동작기록($"[🚨 서버 통신 단절] 마지막 통신: {끊긴시간}");
                    Log.동작기록($"[🚨 단절 원인] {Form1.단절원인}");

                    // 텔레그램 알림을 쓰신다면 여기에 추가하셔도 아주 좋습니다!
                    // Telegram_Send($"[긴급] 서버 통신이 끊어졌습니다. ({끊긴시간})");
                }
            }
            else
            {
                // 💡 처음 감성 그대로! 파란색 원과 파란색 하트
                string 파란불 = (now.Second % 2 == 0) ? "🔵" : "💙";
                서버통신_상태 = $" | 📡 통신 정상 {파란불}";

                // 💡 [보너스] 끊겼던 서버가 다시 연결되면 '복구' 로그를 남겨줍니다.
                if (Form1.서버연결끊김_기록됨 == true)
                {
                    Form1.서버연결끊김_기록됨 = false; // 다시 정상 상태로 리셋
                    Log.동작기록($"[💚 서버 통신 복구] 증권사 서버와 다시 연결되었습니다!");
                }
            }

            // ---------------------------------------------------------------
            // 💡 [3] 지니 최적화: 타이틀 뒤에 엔진 상태 + 서버 상태를 한 번에 조립!
            // ---------------------------------------------------------------
            string titleText = $"{Form1.프로그램명} {Form1.버전_디버그} / {GenieConfig.textBox_ID} / {str.Week} / [ {Time} ] / {Form1.server} / {Form1.server_알림} / 검색식 Run: {Form1.Run_condition_count}개 / 주문 count 초당- {Get.주문_S}회 분당- {Get.주문_M}회 시간당- {Get.주문_H}회 ( ※ 키움서버 주문제한 : 시간당 3600회 입니다.) / 사용기간: {Form1.사용기간} 까지 | {서버통신_상태}";
            string Sub_titleText = $"{Form1.프로그램명} / {str.Week} / [ {Time} ] / {Form1.server} / {Form1.server_알림} / 사용기간: {Form1.사용기간}";

            // 미체결 카운트 문자열 미리 조립
            string 미체결Text = "미체결 주문:  " + Form1.JumunItem_List.Count() + " 건";

            string currentSs = now.ToString("ss");
            string currentMm = now.ToString("mm");

            if (!currentSs.Equals("00")) Get.분_180 = 0;
            if (int.Parse(currentMm) % Get.분_time == 0 && currentSs.Equals("00"))
            {
                if (Get.분_180 == 0)
                {
                    MA.Mma_Record(null);

                    if (Get.분_count < 20)
                    {
                        Get.분_count++;
                        if (Get.분_count == 20) Get.분_time = 3;
                    }
                }
                Get.분_180++;
            }

            // =================================================================
            // 2. [UI 업데이트 구역] - 완성된 문자열만 화면에 출력! (사장님 결재 구역)
            // =================================================================
            Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                // 체크박스 상태 읽기 및 라벨 변경은 반드시 여기서!
                if (Form1.form1.CB_미니시계.Checked)
                {
                    Form1.form1.label_time.Text = Time;
                }

                // 미리 만들어둔 긴 글자를 한 방에 쏙! (부하 거의 제로)
                Form1.form1.Text = titleText;
                if (Sub_form.form != null && !Sub_form.form.IsDisposed)
                {
                    Sub_form.form.Text = Sub_titleText;
                }
                // 서브 폼(미체결 창)이 켜져 있을 때만 에러 없이 업데이트하도록 방어 코드 추가
                if (box.Form_Outstanding.form != null && box.Form_Outstanding.form.IsHandleCreated)
                {
                    box.Form_Outstanding.form.LB_미체결주문.Text = 미체결Text;
                }
            });
        }

        ////////////////////////////    카운터쓰레드    /// ///////////////////////
        public static async Task Delay_timer() // 1 초 타이머
        {
            if (kosdaq_day_count > 0 && server_알림 == "메인마켓")
            {
                string date = DateTime.Now.ToString("HHmmss");
                int Time = int.Parse(date);
                string second = date.Substring(4); // "ss" 부분 추출

                // [1] 매분 00초가 되면 -> 1분 주기 로직 실행
                if (second.Equals("00"))
                {
                    if (kos_avg_update)
                    {
                        kos_avg_update = false;

                        // ---------------------------------------------------------
                        // [Array.Copy 핵심] 
                        // 0번부터 58개를 -> 1번부터 59번 자리로 복사 (마지막 59번값은 자동 삭제됨)
                        // ---------------------------------------------------------

                        // 코스피 밀어내기
                        Array.Copy(kospi_avg_min, 0, kospi_avg_min, 1, 59);
                        // 0번 자리에 새로운 값 덮어쓰기 (종목코드는 불필요하여 제거한 버전 가정)
                        kospi_avg_min[0] = new AVG_price(Acc.피_현재가, Time);

                        // 코스닥 밀어내기
                        Array.Copy(kosdaq_avg_min, 0, kosdaq_avg_min, 1, 59);
                        // 0번 자리에 새로운 값 덮어쓰기
                        kosdaq_avg_min[0] = new AVG_price(Acc.닥_현재가, Time);

                        // [당일주도주+ 검색식] ★ 1분마다 랭킹 분석 실행 ★
                        if (Form1.내아이디) Ranking.랭킹분석시작();
                    }
                }
                // [2] 00초가 아닐 때 -> 0번 데이터(현재봉) 실시간 갱신
                else
                {
                    kos_avg_update = true;

                    // [배열의 장점] List와 달리 값을 꺼내지 않고 '즉시 수정' 가능합니다.
                    // 코스피 0번 갱신
                    kospi_avg_min[0].Endprice = Acc.피_현재가;
                    kospi_avg_min[0].Time = Time;

                    // 코스닥 0번 갱신
                    kosdaq_avg_min[0].Endprice = Acc.닥_현재가;
                    kosdaq_avg_min[0].Time = Time;
                }
            }



            // 1. A, B, C의 지연 설정값을 배열이나 리스트로 통합하여 관리 (필요한 경우)
            // 현재 로직을 유지하면서 반복문의 중복을 제거하는 방식
            if (GenieConfig.MTB_new_delay_A > 0 ||
                GenieConfig.MTB_new_delay_B > 0 ||
                GenieConfig.MTB_new_delay_C > 0)
            {
                // 단 한 번만 전체 목록을 순회합니다. (O(N) -> O(N))
                for (int i = 0; i < NewStock_List.Count; i++)
                {
                    var currentStock = NewStock_List[i]; // 가독성을 위해 변수 할당

                    // "진입" 상태인 종목에 대해서만 처리
                    // (state.Contains("진입")은 문자열 비교이므로, 가능하면 정확한 비교("진입" == state)가 더 좋음)
                    if (currentStock.state.Contains("진입"))
                    {
                        // 타이머 증가 (maximum_time보다 작을 때만)
                        if (currentStock.timer < Get.maximum_time)
                        {
                            currentStock.timer++;
                        }

                        // --- A 검색식 처리 ---
                        if (GenieConfig.MTB_new_delay_A > 0 &&
                            GenieConfig.MTB_new_delay_A <= currentStock.timer)
                        {
                         await   Tab_Basic.매수검색식_Check(currentStock);
                        }

                        // --- B 검색식 처리 ---
                        if (GenieConfig.MTB_new_delay_B > 0 &&
                            GenieConfig.MTB_new_delay_B <= currentStock.timer)
                        {
                            await Tab_Basic.매수검색식_Check(currentStock);
                        }

                        // --- C 검색식 처리 ---
                        if (GenieConfig.MTB_new_delay_C > 0 &&
                            GenieConfig.MTB_new_delay_C <= currentStock.timer)
                        {
                            await Tab_Basic.매수검색식_Check(currentStock);
                        }
                    }
                }
            }

            if (Form1.잔고시간_ON)
            {
                foreach (var 잔고 in Form1.stockBalanceList.Values)
                {
                    if (잔고.시간청산반복_A > 0) 잔고.시간청산반복_A--;
                    if (잔고.시간청산반복_B > 0) 잔고.시간청산반복_B--;
                    if (잔고.시간청산반복_C > 0) 잔고.시간청산반복_C--;
                }
            }

            if (Catch_Stock_List != null)
            {
                List<Catch_stock> stockValues = Catch_Stock_List.Values.ToList();
                for (int i = stockValues.Count - 1; i >= 0; i--)
                {
                    stockValues[i].timer++;
                }
            }


            if (검색이탈_Dic?.Count > 0) await CheckExpiredItems();

            if (Form1.Watch_List.Count > 0)
            {
                foreach (var key in Form1.Watch_List.ToList())
                {
                    Watch watch = Form1.Watch_List[key.Key];

                    // [수정] Setting.watch 모듈 사용
                    int 지연 = GenieConfig.MTB_watch_지연_A;
                    int CBB_watch_trading = GenieConfig.CBB_watch_trading_A;
                    string 관심_title = GenieConfig.CBB_Watch관심_A;
                    bool result = false;

                    if (key.Key.Contains("A:"))
                    {
                        result = true;
                    }
                    if (key.Key.Contains("B:"))
                    {
                        result = true;
                        지연 = GenieConfig.MTB_watch_지연_B;
                        CBB_watch_trading = GenieConfig.CBB_watch_trading_B;
                        관심_title = GenieConfig.CBB_Watch관심_B;
                    }
                    if (key.Key.Contains("C:"))
                    {
                        result = true;
                        지연 = GenieConfig.MTB_watch_지연_C;
                        CBB_watch_trading = GenieConfig.CBB_watch_trading_C;
                        관심_title = GenieConfig.CBB_Watch관심_C;

                    }
                    if (key.Key.Contains("D:"))
                    {
                        result = true;
                        지연 = GenieConfig.MTB_watch_지연_D;
                        CBB_watch_trading = GenieConfig.CBB_watch_trading_D;
                        관심_title = GenieConfig.CBB_Watch관심_D;
                    }

                    if (result && watch.매매.Equals("-"))
                    {
                        if (!관심_title.Equals("기본값"))
                        {
                            Interest_stock stock = Form1.Interest_stock_List.Find(o => o.Name.Equals(watch.Name) && o.Title.Equals(관심_title));
                            if (stock != null)
                            {
                                매수();
                            }
                        }
                        else
                        {
                            매수();
                        }

                        void 매수()
                        {
                            if (watch.timer >= 지연 && watch.Nowprice > 1 && watch.매매.Equals("-"))
                            {
                                if (CBB_watch_trading == 1)
                                {
                                    if (watch.State.Equals("진입"))
                                    {
                                        watch.매매 = "매수";
                                        watch.매수가 = (int)watch.Nowprice;
                                    }
                                }

                                if (CBB_watch_trading == 2)
                                {
                                    if (watch.State.Contains("진입"))
                                    {
                                        watch.매매 = "매수";
                                        watch.매수가 = (int)watch.Nowprice;
                                    }
                                }
                            }
                        }
                    }

                    if (watch.timer < Get.maximum_time) if (watch.State.Contains("진입")) watch.timer++;
                }
            }

           잔고보정();
        }

        // =========================================================
        // 전역 변수: 매번 새로 만들지 않고 재사용할 녀석들 (가비지 컬렉터 방지)
        // =========================================================
        private static readonly HashSet<string> _주문중인종목 = new HashSet<string>();
        private static readonly object _잔고보정_자물쇠 = new object();
        private static int _잔고보정_요청횟수 = 0;

        // 🚀 [추가된 전역 변수] 무한 루프 방지용 (블랙리스트 카운터)
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, int> _보정실패카운트 = new System.Collections.Concurrent.ConcurrentDictionary<string, int>();

        // 클래스 상단에 TR 쿨타임 및 타임아웃을 관리하기 위한 전역 변수를 수정합니다.
        private static DateTime _마지막_잔고보정_시간 = DateTime.MinValue;
        private static readonly TimeSpan _잔고보정_대기시간 = TimeSpan.FromSeconds(10); // 10초 절대 대기

        public static void 잔고보정()
        {
            lock (_잔고보정_자물쇠)
            {
                _주문중인종목.Clear();

                foreach (var item in JumunItem_List.Values)
                {
                    _주문중인종목.Add(item.종목코드);
                }

                foreach (var 잔고 in Form1.stockBalanceList.Values)
                {
                    if (잔고.전량매도) continue;

                    string 종목코드 = 잔고.종목코드;

                    if (_주문중인종목.Contains(종목코드)) continue;

                    if (잔고.보유수량 != 잔고.주문가능수량 || 잔고.신용_보유수량 != 잔고.신용_주문가능수량)
                    {
                        _보정실패카운트.TryGetValue(종목코드, out int 실패횟수);
                        if (실패횟수 >= 3)
                        {
                            continue; // 3회 이상 실패 시 조용히 패스 (블랙리스트)
                        }

                        if (Form1.잔고보정Dict.TryAdd(종목코드, 종목코드))
                        {
                            _보정실패카운트.AddOrUpdate(종목코드, 1, (key, oldValue) => oldValue + 1);
                            Form1.Console_print($"[+] 잔고보정({실패횟수 + 1}차 시도) : {잔고.종목명} [현금]보유:{잔고.보유수량}/주문가능:{잔고.주문가능수량} [신용]보유:{잔고.신용_보유수량}/주문가능:{잔고.신용_주문가능수량}");
                        }
                    }
                    else
                    {
                        // 수량이 정상화되어 블랙리스트에서 제거할 때만 로그 출력
                        if (_보정실패카운트.ContainsKey(종목코드))
                        {
                            Form1.Console_print($"=== [잔고보정 완료] 수량 정상화 확인, 블랙리스트 해제 >> {잔고.종목명} ===");
                            _보정실패카운트.TryRemove(종목코드, out _);
                        }
                    }
                }

                // [극강 최적화 3] 중복 TR 요청 차단 + 10초 타임아웃 재요청 로직
                if (Form1.잔고보정Dict.Count > 0)
                {
                    // 현재 시간과 마지막으로 TR을 쐈던 시간의 차이를 계산합니다.
                    TimeSpan 경과시간 = DateTime.Now - _마지막_잔고보정_시간;

                    // 10초가 지났을 때만 실행합니다. (10초 미만이면 외부에서 아무리 요청해도 절대 무시)
                    if (경과시간 >= _잔고보정_대기시간)
                    {
                        // 1. 정상적인 신규 요청(잔고보정요청 == true)이거나,
                        // 2. 응답이 10초 넘게 안 와서 여전히 Dict에 보정할 항목이 남아있는 경우(타임아웃) 무조건 발송

                        _잔고보정_요청횟수++;

                        Form1.Console_print($"[===] [잔고보정] 계좌평가잔고내역요청 TR 발송 (누적 {_잔고보정_요청횟수}회) {DateTime.Now.ToString("HHmmss.fff")}");

                        // 기준 시간을 '지금'으로 갱신하여, 앞으로 10초 동안은 굳게 자물쇠를 잠급니다.
                        _마지막_잔고보정_시간 = DateTime.Now;

                        Form1.잔고보정요청 = false;
                        TR_요청.계좌평가잔고내역요청("Y", "", false);
                    }
                }
            }
        }

        public static async Task CheckExpiredItems()
        {
            // [안전장치] Null 체크 (?. 사용)
            if (검색이탈_Dic == null || 검색이탈_Dic.IsEmpty) return;

            DateTime 현재시간 = DateTime.Now;

            // ConcurrentDictionary는 foreach 중에도 수정/삭제가 안전하지 않을 수 있으니
            // KeyValuePair를 순회하며 조건에 맞는 것만 TryRemove 하는 패턴이 가장 안전하고 빠릅니다.

            foreach (var 항목 in 검색이탈_Dic)
            {
                // 종료 시간이 지났는지 확인 (값 읽기 - 빠름)
                if (현재시간 >= 항목.Value.ExpireTime)
                {
                    // [스레드 안전 삭제] TryRemove 사용
                    // 삭제에 성공한 경우에만(out 제거된항목) UI 로직 실행
                    if (검색이탈_Dic.TryRemove(항목.Key, out var 제거된항목))
                    {
                        string 현재시간_문자열 = 현재시간.ToString("HH:mm:ss");

                        // 미리 파싱된 변수 사용 (Split 비용 없음)
                        string 종목코드 = 제거된항목.종목코드;
                        string 검색식 = 제거된항목.검색식;

                        // UI 및 로직 수행
                        if (제거된항목.신규)
                        {
                            await Tab_Basic.New_Buy("D", 종목코드, 검색식);
                        }
                        Tab_Watch.Watch_In_Out("D", 종목코드, 검색식, 현재시간_문자열);
                        Tab_Repeat.Repeat_condition("D", 종목코드, 검색식);
                        Tab_AccountManagement.Rebalancing_condition("D", 종목코드, 검색식);
                        Tab_AccountManagement.Liquidation_condition("D", 종목코드, 검색식);
                    }
                }
            }
        }

        public static void Threadcount()
        {
            int.TryParse(Form1.form1.TB_주문row.Text, out int 주문row);
            int.TryParse(Form1.form1.TB_체결row.Text, out int 체결row);

            if (box.Form_JuMun.form.JuMun_dataGridView.RowCount > 주문row)
            {
                int count = box.Form_JuMun.form.JuMun_dataGridView.RowCount - 주문row;

                for (int i = 0; i < count; i++)
                {
                    box.Form_JuMun.form.JuMun_dataGridView.Rows.Remove(box.Form_JuMun.form.JuMun_dataGridView.Rows[주문row]);
                }

                box.Form_JuMun.form.JuMun_dataGridView.CurrentCell = null;
            }

            if (box.Form_Conclusion.form.Conclusion_DataGridView.RowCount > 체결row)
            {
                int count = box.Form_Conclusion.form.Conclusion_DataGridView.RowCount - 체결row;

                for (int i = 0; i < count; i++)
                {
                    box.Form_Conclusion.form.Conclusion_DataGridView.Rows.Remove(box.Form_Conclusion.form.Conclusion_DataGridView.Rows[체결row]);
                }

                box.Form_Conclusion.form.Conclusion_DataGridView.CurrentCell = null;
            }

            // =================================================================
            // 1. 일반 반복 타이머 (A ~ N)
            // =================================================================
            if (Get.Repeat_time_A > 0) Get.Repeat_time_A--;
            if (Get.Repeat_time_B > 0) Get.Repeat_time_B--;
            if (Get.Repeat_time_C > 0) Get.Repeat_time_C--;
            if (Get.Repeat_time_D > 0) Get.Repeat_time_D--;
            if (Get.Repeat_time_E > 0) Get.Repeat_time_E--;
            if (Get.Repeat_time_F > 0) Get.Repeat_time_F--;
            if (Get.Repeat_time_G > 0) Get.Repeat_time_G--;
            if (Get.Repeat_time_H > 0) Get.Repeat_time_H--;
            if (Get.Repeat_time_I > 0) Get.Repeat_time_I--;
            if (Get.Repeat_time_J > 0) Get.Repeat_time_J--;
            if (Get.Repeat_time_K > 0) Get.Repeat_time_K--;
            if (Get.Repeat_time_L > 0) Get.Repeat_time_L--;
            if (Get.Repeat_time_M > 0) Get.Repeat_time_M--;
            if (Get.Repeat_time_N > 0) Get.Repeat_time_N--;

            // =================================================================
            // 2. 리밸런싱 타이머 (A ~ G)
            // [최적화] 전위 감소 연산자(--)와 단축 평가(&&)를 사용하여 코드를 한 줄로 압축
            // =================================================================
            if (Get.TT_rebalance_time_A > 0) { Get.TT_rebalance_time_A--; if (Get.TT_rebalance_time_A == 0 && Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_A.Text = "X"; }
            if (Get.TT_rebalance_time_B > 0) { Get.TT_rebalance_time_B--; if (Get.TT_rebalance_time_B == 0 && Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_B.Text = "X"; }
            if (Get.TT_rebalance_time_C > 0) { Get.TT_rebalance_time_C--; if (Get.TT_rebalance_time_C == 0 && Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_C.Text = "X"; }
            if (Get.TT_rebalance_time_D > 0) { Get.TT_rebalance_time_D--; if (Get.TT_rebalance_time_D == 0 && Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_D.Text = "X"; }
            if (Get.TT_rebalance_time_E > 0) { Get.TT_rebalance_time_E--; if (Get.TT_rebalance_time_E == 0 && Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_E.Text = "X"; }
            if (Get.TT_rebalance_time_F > 0) { Get.TT_rebalance_time_F--; if (Get.TT_rebalance_time_F == 0 && Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_F.Text = "X"; }
            if (Get.TT_rebalance_time_G > 0) { Get.TT_rebalance_time_G--; if (Get.TT_rebalance_time_G == 0 && Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_G.Text = "X"; }
            // =================================================================
            // 3. 청산 타이머 (A ~ C)
            // =================================================================
            if (Get.TT_Liqu_time_A > 0) Get.TT_Liqu_time_A--;
            if (Get.TT_Liqu_time_B > 0) Get.TT_Liqu_time_B--;
            if (Get.TT_Liqu_time_C > 0) Get.TT_Liqu_time_C--;

            // =================================================================
            // 4. 실행 대기 타이머 (Run_ 플래그)
            // [최적화] 플래그가 false일 때만 시간 체크 및 감소 처리
            // =================================================================
            if (!Form1.Run_time && Get.time_Run_time > 0 && --Get.time_Run_time == 0) Form1.Run_time = true;
            if (!Form1.Run_silson_W && Get.time_Run_silson_W > 0 && --Get.time_Run_silson_W == 0) Form1.Run_silson_W = true;
            if (!Form1.Run_예상손실 && Get.time_Run_예상손실 > 0 && --Get.time_Run_예상손실 == 0) Form1.Run_예상손실 = true;
            if (!Form1.Run_예상수익 && Get.time_Run_예상수익 > 0 && --Get.time_Run_예상수익 == 0) Form1.Run_예상수익 = true;

            if (Form1.체결기록list.Count > 0)
            {
                string 주문번호 = Form1.체결기록list[0].주문번호;
                if (!Form1.JumunItem_List.Values.Any(o => o.주문번호 == 주문번호))
                {
                    // 1. 먼저 리스트의 0번 항목을 변수에 담습니다 (반복 호출 제거로 속도 향상!)
                    var log = Form1.체결기록list[0];

                    // 2. 문자열 보간($)을 사용하여 한 방에 정리
                    Writing_Management.체결기록(
                        $"{log.con_num};{log.주문};{log.주문N체결시간};{log.종목명};{log.체결검색식};{log.등락률};{log.수익률};{log.체결가};{log.주문수량};{log.체결량};{int.Parse(log.체결가) * int.Parse(log.체결량)};{log.현재가};{log.거래구분};{log.종목코드};{log.주문번호}"
                    );
                    Form1.체결기록list.Remove(Form1.체결기록list[0]);
                }
            }

            foreach (var 잔고 in Form1.stockBalanceList.Values)
            {
                if (잔고.전량매도)  //전량매도 재매수 타이머 
                {
                    if (잔고.재매수 > 0)
                    {
                        잔고.재매수--;
                    }
                }
                else
                {
                    if (잔고.추매딜레이 > 0)
                    {
                        잔고.추매딜레이--;
                    }
                }
            }

            if (Form1.재주문_LIST.Count > 0)
            {
                foreach (var item in Form1.재주문_LIST.ToList())
                {
                    if (item.timer > 0)
                    {
                        item.timer--;

                        if (item.timer == 0) Form1.재주문_LIST.Remove(item);
                    }
                }
            }

            bool 주문시간 = false;
            if (Form1.server.Equals("실서버"))
            {
                주문시간 = true;
            }
            else
            {
                if (Get.TimeNow <= (Get.메인마켓종료 - 1000)) // 모의투자용
                {
                    주문시간 = true;
                }
            }

            // =========================================================================
            // [지니 최적화 1] 공통 문자열 검사를 루프 바깥으로 뺌 (CPU 점유율 대폭 하락)
            // 수백 개의 주문이 돌아도 마켓 상태 확인은 1번만 수행합니다.
            // =========================================================================
            bool 마켓오픈상태 = Form1.server_알림.Contains("마켓");

            // =========================================================================
            // [지니 최적화 2] ToList() 완전 제거 (메모리 낭비 제로화)
            // 복사본을 만들지 않고 딕셔너리를 직접 순회합니다. (ConcurrentDictionary는 락 없이 안전함)
            // =========================================================================
            foreach (var 현재주문_쌍 in Form1.JumunItem_List)
            {
                JumunItem 개별주문 = 현재주문_쌍.Value;

                // [최적화 3] 불필요한 임시변수 생성 없이 객체 값을 직접 참조
                if (개별주문.취소timer >= 0 && 개별주문.취소timer != 99999 && 주문시간)
                {
                    // 미리 계산해둔 '마켓오픈상태' 변수 사용
                    if (마켓오픈상태 && !개별주문.주문번호.Contains("+"))
                    {
                        if (개별주문.취소timer > 0)
                        {
                            개별주문.취소timer--;
                        }
                    }

                    // 타이머가 0이 된 딱 그 순간에만 실행
                    if (개별주문.취소timer <= 0)
                    {
                        if (개별주문.주문취소 && 개별주문.미체결량 > 0)
                        {
                            개별주문.주문취소 = false; // [기존 방어막] 두 번 취소 방지

                            if (개별주문.매수매도 == 1) 개별주문.매수매도 = 10;
                            if (개별주문.매수매도 == 2) 개별주문.매수매도 = 20;
                            if (개별주문.비고.Length < 2) 개별주문.비고 = "미체결 취소";

                            ExecuteTrade.Que_order(개별주문);
                            Form1.비프음("언체크");
                        }
                    }
                }

                // 삭제 타이머 로직
                if (개별주문.Deletetimer > 0)
                {
                    개별주문.Deletetimer++;

                    if (개별주문.Deletetimer >= 5)
                    {
                        Jumun.ExecuteDelete(개별주문);
                    }
                }
            }

            if (!Form1.Form_close)  // 미체결 그리드뷰 취소시간 타이머
            {
                // -------------------------------------------------------------
                // [1] 미수금 발생 시 강제 동기화 로직 (기존 유지)
                // -------------------------------------------------------------
                if (Get.TimeNow > GenieConfig.MT_misu_time && GenieConfig.CB_misu && GenieConfig.Combo_misu != 0)
                {
                    Get.Ten++;
                    if (Get.Ten >= 10)
                    {
                        Get.Ten = 0;
                        if (Form1.Acc.D2 < 0 && Get.TimeNow < Get.시장종료)
                        {
                            _ = Helper.미체결내역동기화(false);
                            TR_요청.계좌요청_분기발행("Y", "", false);
                            TR_요청.실현손익요청("Y", "", true);
                        }
                    }
                }

                // -------------------------------------------------------------
                // [2] 장 초반 데이터 부재 시 예외 요청 로직 (기존 유지)
                // -------------------------------------------------------------
                if (Form1.Acc.피_외인 == 0 || Form1.Acc.피_기관 == 0)
                {
                    if (Get.메인마켓시작 + 100 < Get.TimeNow)
                    {
                        Get.Ten++;

                        if (Get.Ten >= 10)
                        {
                            Get.Ten = 0;
                            TR_요청.코스피투자자순매수요청(false);
                            TR_요청.코스피_프로그램매매추이요청시간대별(false);
                        }
                    }
                }
                else
                {
                    // 성능 최적화: 문자열 파싱 오버헤드 제거
                    int minutes = DateTime.Now.Minute;
                    int seconds = DateTime.Now.Second;
                    int TR_time = (minutes % 10) * 100 + seconds;

                    // -------------------------------------------------------------
                    // [3] 투자자 순매수 및 프로그램 매매 정기 요청
                    // -------------------------------------------------------------
                    if (Form1.순매수조회)
                    {
                        switch (TR_time)
                        {
                            case 0:
                            case 200:
                            case 400:
                            case 600:
                            case 800:
                                TR_요청.코스피투자자순매수요청(false);
                                break;
                            case 100:
                            case 300:
                            case 500:
                            case 700:
                            case 900:
                                TR_요청.코스피_프로그램매매추이요청시간대별(false);
                                break;
                        }
                    }
                    else
                    {
                        if ((minutes % 60 == 0 && seconds == 0) || (minutes % 60 == 30 && seconds == 0))
                        {
                            TR_요청.코스피투자자순매수요청(false);
                        }
                        else if ((minutes % 60 == 15 && seconds == 0) || (minutes % 60 == 45 && seconds == 0))
                        {
                            TR_요청.코스피_프로그램매매추이요청시간대별(false);
                        }
                    }

                    // -------------------------------------------------------------
                    // [4] 정기 타이머 요청 (💡 주문 큐가 완전히 비워진 상태에서만 실행 허용)
                    // -------------------------------------------------------------
                    if (Form1.order_scheduler.QueueCount == 0 && !주문큐_작동중)
                    {
                        if (TR_time % 500 == 0) // 0 또는 500일 때
                        {
                            if (Form1.주문내역요청 && Form1.주문내역동기화)
                            {
                                _ = Helper.미체결내역동기화(false);
                                TR_요청.실현손익요청("Y", "", true);
                                Form1.주문내역요청 = false;
                                Form1.주문내역동기화 = false;
                            }
                        }
                        else if (TR_time % 250 == 0) // 250 또는 750일 때
                        {
                            if (Form1.예수금요청)
                            {
                                if (GenieConfig.CB_misu)
                                {
                                    if (Get.TimeNow < GenieConfig.MT_misu_time)
                                    {
                                        TR_요청.계좌요청_분기발행("Y", "", false);
                                    }
                                }
                                else
                                {
                                    TR_요청.계좌요청_분기발행("Y", "", false);
                                }
                                Form1.예수금요청 = false;
                            }
                        }
                    }

                    // [5] 정기 타이머 펌프 플래그 충전
                    int TR_time_ = DateTime.Now.Second % 10;
                    if (TR_time_ != 0 && (Get.TimeNow < GenieConfig.MT_misu_time || !GenieConfig.CB_misu))
                    {
                        Form1.주문내역요청 = true;
                        Form1.예수금요청 = true;
                    }

                    // -------------------------------------------------------------
                    // [6] 주문 큐 감시 로직 (기존 로직 완벽 유지: 큐 종료 시 즉시 1회 요청)
                    // -------------------------------------------------------------
                    if (Form1.order_scheduler.QueueCount > 0)
                    {
                        // 1. 큐에 주문이 처음 들어온 순간 작동 플래그 가동
                        if (!주문큐_작동중)
                        {
                            주문큐_작동중 = true;
                            Form1.Console_print(">> [주문 큐 발생] 실시간 자산 추적 가동");
                        }

                        // 2. 큐에 잔여 주문이 진행 중일 때 (매도 전용 상태인지 체크)
                        bool 매수주문진행중 = Form1.JumunItem_List.Values.Any(jumun => jumun.매수매도 == 1);

                        if (!매수주문진행중)
                        {
                            // 매도만 진행 중일 때는 타이머 플래그 간섭 없이 즉시 계좌 정보 갱신
                            TR_요청.계좌요청_분기발행("Y", "", false);
                        }
                    }
                    else
                    {
                        // 3. 큐가 완전히 비워진 평화 상태 진입 '그 순간' 딱 한 번만 실행
                        if (주문큐_작동중)
                        {
                            Form1.Console_print(">> [주문 큐 종료] 최종 자산 정밀 동기화 즉시 실행");

                            // 미체결 및 당일 실현손익 확정 데이터 최종 동기화
                            _ = Helper.미체결내역동기화(false);
                            TR_요청.실현손익요청("Y", "", true);

                            // 핵심 해결: 다른 타이머 플래그 상태나 미수 시간 제한에 상관없이 
                            // 주문 큐가 종료된 시점에는 무조건 완결성 있게 최종 계좌 조회를 실행합니다.
                            TR_요청.계좌요청_분기발행("Y", "", false);

                            // 모든 작업 완료 후 평화 상태로 스위치 리셋
                            주문큐_작동중 = false;
                        }
                    }
                }
            }

            if (Form1.시장가탐색)
            {
                Dictionary<string, Market_Item> findResult = Form1.Market_Item_List.Where(o => o.Value.재매수_A > 0 || o.Value.재매수_B > 0).ToDictionary(o => o.Key, o => o.Value);
                foreach (var item in findResult)
                {
                    if (GenieConfig.CB_매수탐색A && item.Value.재매수_A > 0) Form1.Market_Item_List[item.Key].재매수_A--;
                    if (GenieConfig.CB_매수탐색B && item.Value.재매수_B > 0) Form1.Market_Item_List[item.Key].재매수_B--;
                }
            }

            if (Form1.NewBuyWrite_List.Count > 0)
            {
                List<string> itemsToRemove = new List<string>();

                // [최적화 1] HashSet을 foreach로 순회합니다. (인덱스 접근 대신)
                foreach (string item in Form1.NewBuyWrite_List)
                {
                    string[] parts = item.Split(';');
                    // 최소한 2개의 요소가 있어야 함 (안전성 확보)
                    if (parts.Length < 2) continue;

                    string 주문번호 = parts[0]; // 주문번호 (키가 아님)
                    string code = parts[1];    // 종목코드

                    // [성능 저하 구간] JumunItem_List가 ConcurrentDictionary라면, 
                    // 주문번호를 키로 사용하는 것이 가장 빠르지만, 현재는 Values.Any() O(N) 순차 검색을 수행해야 합니다.
                    if (!Form1.JumunItem_List.Values.Any(o => o.주문번호 == 주문번호))
                    {
                        // 잔고 목록은 Dictionary이므로 TryGetValue는 O(1)로 빠릅니다.
                        if (Form1.stockBalanceList.TryGetValue(code, out Stockbalance 잔고))
                        {
                            // 로그 데이터 생성 및 기록 (기존 로직 유지)
                            string logData = string.Join(";",
                                str.today,
                                잔고.종목명,
                                잔고.초기매수검색식,
                                Form1.Get.TimeNow.ToString("##:##:##"),
                                잔고.등락율,
                                잔고.평균단가,
                                잔고.보유수량,
                                Method.단위변환(잔고.평균단가 * 잔고.보유수량),
                                Form1.stockBalanceList.Count,
                                Method.단위변환(Acc.추정D2),
                                Method.단위변환(Acc.매입금),
                                Method.단위변환(Acc.추정자산),
                                Method.단위변환(Acc.증가자산)
                            );

                            Writing_Management.신규매수기록(logData + ";"); // 끝에 세미콜론 추가

                            // 로그 출력 (기존 로직 유지)
                            Log.에러기록(" ");
                            Log.에러기록($"**{잔고.종목명} => 신규매수 되었습니다.**");
                            Log.에러기록($"{잔고.종목명} 매수검색식: {잔고.초기매수검색식} 금일등락율: {잔고.등락율} 매입가: {잔고.평균단가} 보유수량: {잔고.보유수량} 적용금액: {Method.단위변환(잔고.평균단가 * 잔고.보유수량)}");
                            Log.에러기록($"{잔고.종목명} 잔고수: {Form1.stockBalanceList.Count} 개 예수금: {Method.단위변환(Acc.추정D2)} 매입금: {Method.단위변환(Acc.매입금)} 추정자산: {Method.단위변환(Acc.추정자산)} 증가자산: {Method.단위변환(Acc.증가자산)}");
                            Log.에러기록(" ");

                            // [최적화 2] 제거할 목록에 현재 항목(item)을 추가합니다.
                            itemsToRemove.Add(item);
                        }
                    }
                }

                // ----------------------------------------------------------------------
                // [최적화 3] 루프 종료 후, 모아둔 항목을 원본 HashSet에서 O(1)로 제거
                // ----------------------------------------------------------------------
                foreach (string item in itemsToRemove)
                {
                    // HashSet.Remove는 O(1)로 매우 빠릅니다.
                    Form1.NewBuyWrite_List.Remove(item);
                }
            }
        }

        private static bool 재시작_073000_완료 = false;
        public static async Task UpdateNowDateTime() // 현재시간 출력
        {
            Form1.Writing_time++;
            if (Form1.Writing_time == 60)
            {
                Form1.Writing_time = 0;
                if (Get.TimeNow <= Get.시장종료)
                {
                    SaveToFile.잔고_파일저장();
                    SaveToFile.최종매입가_파일저장(Form1.로딩완료);
                    SaveToFile.리밸감시주문_파일저장();
                    SaveToFile.주문리스트_파일저장();
                }
            }

            if (Form1.로딩완료 && Form1.ON_LINE)
            {
                if (Jine_Run)
                {
                    if (DateTime.Now.DayOfWeek.Equals(DayOfWeek.Saturday) || DateTime.Now.DayOfWeek.Equals(DayOfWeek.Sunday))
                    {
                        장종료();
                       Log.동작기록(" ");
                       Log.동작기록("주말 에는 MoneyGame 을 할수 없습니다.");
                        Jine_Run = false;
                    }
                    else
                    {
                        if (server_알림.Equals("로딩중"))
                        {
                            server_알림 = "시작전";
                        }

                        if (!server_알림.Equals("장종료"))
                        {
                            if (Get.시장종료 <= Get.TimeNow)
                            {
                                장종료();
                               Log.동작기록(" ");
                               Log.동작기록("MoneyGame 이 '종료' 되었습니다.");
                                Jine_Run = false;
                            }
                        }
                    }
                }

                if (Get.시장시작 <= Get.TimeNow && !Form1.공휴일)
                {
                    if (Jine_Run)
                    {
                        if (form1.CB_Auto_tradingstart.Checked && !form1.매도_ON && (server_알림.Contains("마켓") || server_알림.Contains("동시") || server_알림.Equals("애프터전")) && !공휴일)   // 자동시작&정지 
                        {
                            if (GenieConfig.MT_starttime <= Get.TimeNow)
                            {
                                Jine_Run = false;

                                // [수정된 부분] 멈추지 않고 대기
                                if (Get.로딩완료타임 > Get.시장시작)
                                {
                                    await Task.Delay(2000); // 여기서 2초간 비동기 대기 (UI 안 멈춤)
                                }

                                form1.RB_sell_run.Checked = true;

                                if (GenieConfig.CB_신규매수정지 && GenieConfig.CB_추가매수정지)
                                {
                                    AutoClosingAlram("신규매수 - [ 정지 ], 추가매수 - [ 정지 ] 입니다. \n그룹n기능의 설정을 확인 해주세요.", "매수 정지알림", 60, "동작");
                                }
                                else
                                {
                                    form1.RB_buy_run.Checked = true;
                                }
                            }
                        }
                    }

                    if (server_알림.Equals("시작전") && !공휴일)
                    {
                        if (!CB수능일 && !CB개장일)
                        {
                            if ((Get.메인마켓시작 - 10000) <= Get.TimeNow && Get.TimeNow < Get.메인마켓시작)
                            {
                                server_알림 = "프리마켓";
                                NXT_server = true;
                                Market_Run("프리마켓", "마켓대기");
                            }
                        }

                        if ((Get.메인마켓시작 - 5000) <= Get.TimeNow && Get.TimeNow < Get.메인마켓시작)
                        {
                            server_알림 = "장전동시";
                            NXT_server = false;

                            List<Stockbalance> 잔고List = stockBalanceList.Values.ToList();
                            for (int i = 잔고List.Count - 1; i >= 0; i--)
                            {
                                Stockbalance 잔고 = 잔고List[i];
                                if (!잔고.종목상태.Contains("거래정지"))
                                {
                                    잔고.매매가능 = false;
                                    잔고.잔고청산 = false;
                                    잔고.종목상태 = "장전동시";
                                }
                            }
                        }

                        if (Get.메인마켓시작 <= Get.TimeNow && Get.TimeNow < Get.메인마켓종료) { server_알림 = "메인마켓"; NXT_server = false; }
                        if (Get.메인마켓종료 - 1000 <= Get.TimeNow && Get.TimeNow < Get.메인마켓종료) { server_알림 = "동시호가"; NXT_server = false; }
                        if (Get.메인마켓종료 <= Get.TimeNow && Get.TimeNow < Get.메인마켓종료 + 1000) { server_알림 = "애프터전"; NXT_server = false; Market_Run("애프터전", "마켓종료"); }
                        if (Get.메인마켓종료 + 1000 <= Get.TimeNow && Get.TimeNow < (Get.메인마켓종료 + 47000)) { server_알림 = "애프터마켓"; NXT_server = true; Market_Run("애프터마켓", "마켓종료"); }

                        if (server_알림 != "시작전")
                        {
                           Log.동작기록(" ");
                           Log.동작기록("MoneyGame 이 '시작' 되었습니다.");
                        }
                    }

                    if (server_알림.Equals("프리마켓"))
                    {
                        if ((Get.메인마켓시작 - 5000) <= Get.TimeNow && Get.TimeNow < Get.메인마켓시작)
                        {
                            server_알림 = "장전동시";
                            NXT_server = false;

                            List<Stockbalance> 잔고List = stockBalanceList.Values.ToList();
                            for (int i = 잔고List.Count - 1; i >= 0; i--)
                            {
                                Stockbalance 잔고 = 잔고List[i];
                                if (!잔고.종목상태.Contains("거래정지"))
                                {
                                    잔고.매매가능 = false;
                                    잔고.잔고청산 = false;
                                    잔고.종목상태 = "장전동시";
                                }
                            }


                            if (주문정리_완료 == false && (Get.메인마켓시작 - 4045) <= Get.TimeNow)
                            {
                                주문정리_완료 = true;

                                List<JumunItem> jumunList = JumunItem_List.Values.ToList();
                                for (int i = jumunList.Count - 1; i >= 0; i--)
                                {
                                    JumunItem jumun = jumunList[i];

                                    if (!jumun.비고.Equals("메인마켓 시작전 주문정리"))
                                    {
                                        if (jumun.검색식.Contains("신규_") && jumun.주문수량 == jumun.미체결량)
                                        {
                                            Get.신규횟수--;
                                        }

                                        // 주문 상태 변경 및 초기화
                                        jumun.비고 = "메인마켓 시작전 주문정리";
                                        jumun.반복횟수 = 0;
                                        jumun.취소시간 = 0;
                                        jumun.취소timer = 0;

                                        // 로그 출력 (문자열 보간 사용으로 가독성 향상)
                                       Log.동작기록(" ");
                                       Log.동작기록($"[메인마켓 시작전] 종목명:{jumun.종목명} 주문유형:{GET.매수매도str(jumun.매수매도)} 수량: {jumun.미체결량} 주문이 취소 됩니다.");
                                       Log.동작기록(" ");
                                    }
                                }
                            }
                        }
                    }

                    if (server_알림.Equals("장전동시"))
                    {
                        if (Get.메인마켓시작 <= Get.TimeNow && Get.TimeNow < Get.메인마켓종료)
                        {
                            NXT_server = false;
                            server_알림 = "메인마켓";
                            주문정리_완료 = false;
                        }
                    }

                    if (server_알림 == "메인마켓")
                    {
                        if (Get.메인마켓종료 - 1000 <= Get.TimeNow && Get.TimeNow < Get.메인마켓종료)
                        {
                            NXT_server = false;
                            server_알림 = "동시호가";
                        }
                    }

                    if (server_알림 == "동시호가")
                    {
                        if (Get.메인마켓종료 < Get.TimeNow && Get.TimeNow < Get.메인마켓종료 + 1000)
                        {
                            NXT_server = false;
                            server_알림 = "애프터전";

                            Market_Run("애프터전", "마켓종료");
                        }
                    }

                    if (server_알림.Equals("애프터전"))
                    {
                        if (Get.메인마켓종료 + 500 < Get.TimeNow && 주문정리_완료 == false) // 애프터마켓 시작전 주문 취소
                        {
                            주문정리_완료 = true;

                            List<JumunItem> jumunList = Form1.JumunItem_List.Values.ToList();

                            for (int i = 0; i < jumunList.Count; i++)
                            {
                                JumunItem jumun = jumunList[i];

                                if (!jumun.비고.Equals("애프터마켓 시작전 주문정리"))
                                {
                                    if (jumun.검색식.Contains("신규_") && jumun.주문수량 == jumun.미체결량)
                                    {
                                        Get.신규횟수--;
                                    }

                                    jumun.비고 = "애프터마켓 시작전 주문정리";
                                    jumun.반복횟수 = 0;
                                    jumun.취소시간 = 0;
                                    jumun.취소timer = 0;

                                    // 로그 출력 (문자열 보간 사용으로 가독성 향상)
                                   Log.동작기록(" ");
                                   Log.동작기록($"[애프터마켓 시작전] 종목명:{jumun.종목명} 주문유형:{GET.매수매도str(jumun.매수매도)} 수량: {jumun.미체결량} 주문이 취소 됩니다.");
                                   Log.동작기록(" ");
                                }
                            }
                        }

                        if (Get.메인마켓종료 + 1000 <= Get.TimeNow && Get.TimeNow < (Get.메인마켓종료 + 47000))
                        {
                            NXT_server = true;
                            server_알림 = "애프터마켓";

                            Market_Run("애프터마켓", "마켓종료");
                        }
                    }

                    if (Form1.server_알림.Contains("마켓"))
                    {
                        if (Get.시장종료 <= Get.TimeNow)
                        {
                            NXT_server = false;
                            장종료();
                           Log.동작기록(" ");
                           Log.동작기록("MoneyGame 이 '종료' 되었습니다.");
                        }

                        if (호가요청)
                        {
                            if (Get.시장시작 < Get.로딩완료타임)
                            {
                                if (Get.로딩완료타임 + 30 < Get.TimeNow) 요청();
                            }
                            else
                            {
                                if (Get.시장시작 + 30 < Get.TimeNow) 요청();
                            }

                            void 요청()
                            {
                                Form1.호가요청 = false;

                                foreach (var 잔고 in Form1.stockBalanceList.Values)
                                {
                                    if (!잔고.매매가능 && !잔고.종목상태.Contains("거래정지"))
                                    {
                                        TR_요청.주식호가요청(잔고.종목코드, false);
                                    }
                                }
                            }
                        }
                    }
                }

                void Market_Run(string sever, string text)
                {
                    List<Stockbalance> 잔고List = stockBalanceList.Values.ToList();

                    for (int i = 잔고List.Count - 1; i >= 0; i--)
                    {
                        Stockbalance 잔고 = 잔고List[i];

                        if (!잔고.종목상태.Contains("거래정지"))
                        {
                            if (NXT_list.Contains(잔고.종목코드))
                            {
                                잔고.매매가능 = true;
                                잔고.잔고청산 = true;

                                if (sever.Equals("애프터전"))
                                {
                                    잔고.종목상태 = "애프터전";
                                }
                                else
                                {
                                    잔고.종목상태 = GET.Jango_state(잔고.종목코드);
                                }
                            }
                            else
                            {
                                잔고.매매가능 = false;
                                잔고.잔고청산 = false;
                                잔고.종목상태 = text;
                            }
                        }
                    }
                }

            }

            if (Jine_Stop && GenieConfig.MT_stoptime <= Get.TimeNow && (form1.매수_ON || form1.매도_ON))
            {
                Jine_Stop = false;
                form1.RB_sell_run.Checked = false;
                form1.RB_buy_run.Checked = false;

                form1.RB_buy_stop.Checked = true;
                form1.RB_sell_stop.Checked = true;
            }


            if (Form1.지니64n종료)
            {
                if (GenieConfig.MT_closetime < Get.TimeNow)
                {
                    if (GenieConfig.CBB_지니64종료 == 0)
                    {
                        Form1.지니64n종료 = false;
                        Application.Exit();
                    }
                    else if (GenieConfig.CBB_지니64종료 == 1)
                    {
                        Form1.지니64n종료 = false;
                        Form1.HI지니64시작 = true;
                        Application.Exit();
                    }
                }
            }

            if (Get.로딩완료타임 < 073000)
            {
                if (073000 <= Get.TimeNow && !재시작_073000_완료)
                {
                    재시작_073000_완료 = true;

                    Form1.재시작가동("재시작", " 7시30분전 가동하였습니다. 지니64 재시작 을 위해 HI지니64를 실행합니다.");
                }
            }

            if (!Form1.공휴일)
            {
                ////////////// 예약 주문 넣기

                if (GenieConfig.CB_예약주문_장전 && Form1.예약주문_장전)
                {
                    if (GenieConfig.MTB_예약주문_장전주문시간 < Get.TimeNow)
                    {
                        Form1.예약주문_장전 = false;
                        await Order_Reserve.예약주문실행(true);
                    }
                }

                if (GenieConfig.CB_예약주문_종가 && Form1.예약주문_종가)
                {
                    if (GenieConfig.MTB_예약주문_종가주문시간 < Get.TimeNow)
                    {
                        Form1.예약주문_종가 = false;
                    await    Order_Reserve.예약주문실행(false);
                    }
                }


                if (GenieConfig.CB_매매기간_오전 && Form1.매매기간_오전)
                {
                    if (GenieConfig.TB_매매기간_오전주문시간 < Get.TimeNow)
                    {
                        Form1.매매기간_오전 = false;

                        foreach (var 잔고 in Form1.stockBalanceList.Values)
                        {
                            Tab_Special.매매일_기준거래(잔고, "오전");
                        }
                    }
                }

                if (GenieConfig.CB_매매기간_오후 && Form1.매매기간_오후)
                {
                    if (GenieConfig.TB_매매기간_오후주문시간 < Get.TimeNow)
                    {
                        Form1.매매기간_오후 = false;

                        foreach (var 잔고 in Form1.stockBalanceList.Values)
                        {
                            Tab_Special.매매일_기준거래(잔고, "오후");
                        }
                    }
                }
            }
         

            if (Form1.server_알림.Contains("마켓") || Form1.server_알림.Contains("동시")) // 신규매수 시작시간에 검색 신호주기
            {
                // === 기존 메인 로직 처리 부분 ===
                // A, B, C 각각에 대해 일반화된 함수 호출 (ref 대신 반환값으로 갱신)
                신규매수신호_A = await 신규매수신호_처리(신규매수신호_A, GenieConfig.MT_new_start_A, "New_A");
                신규매수신호_B = await 신규매수신호_처리(신규매수신호_B, GenieConfig.MT_new_start_B, "New_B");
                신규매수신호_C = await 신규매수신호_처리(신규매수신호_C, GenieConfig.MT_new_start_C, "New_C");

                // 반복되는 로직을 처리하는 일반화된 함수 (로컬 비동기 함수)
                // C# 컴파일러 규칙상 async에는 ref를 쓸 수 없어 Task<bool>로 결과를 반환받습니다.
                async Task<bool> 신규매수신호_처리(bool 신호상태, int 시작시간설정, string 위치코드)
                {
                    if (신호상태 && 시작시간설정 <= Get.TimeNow)
                    {
                        // [최적화] 리스트 역순회는 인덱스 꼬임 방지와 속도 측면에서 유리합니다.
                        for (int i = NewStock_List.Count - 1; i >= 0; i--)
                        {
                            if (NewStock_List[i].Pos == 위치코드)
                            {
                                await Tab_Basic.매수검색식_Check(NewStock_List[i]);
                            }
                        }

                        // 로직 수행이 완료되었으므로, 다음 중복 실행을 막기 위해 false 반환
                        return false;
                    }

                    // 아직 조건이 충족되지 않았다면 기존 신호 상태를 그대로 유지하여 반환
                    return 신호상태;
                }
            }


            if (GenieConfig.CB_Record)
            {
                Console_print("time: " + Get.TimeNow + "camrun: " + Form1.OcamRun);
                int Ocamtime = GenieConfig.TB_Record_Run;

                if (Form1.OcamRun && Get.TimeNow >= Ocamtime)
                {
                    Form1.OcamRun = false;

                    if (GenieConfig.CBB_Record == 1)
                    {
                        FileInfo fi_오켐 = new FileInfo("C:\\Program Files (x86)\\oCam\\oCam.exe");
                        if (fi_오켐.Exists)
                        {
                            string filename = "oCam"; //종료시킬 프로그램

                            Process[] process = Process.GetProcessesByName(filename);
                            if (process.GetLength(0) == 0)
                            {
                                System.Diagnostics.Process ps = new System.Diagnostics.Process();
                                ps.StartInfo.FileName = "oCam.exe";
                                ps.StartInfo.WorkingDirectory = "C:\\Program Files (x86)\\oCam\\";
                                ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                ps.Start();

                            }
                            else
                            {
                                Form1.AutoClosingAlram("오캠 이 실행중입니다", "실행알람", 10, "에러");
                            }
                        }
                        else
                        {
                            Form1.AutoClosingAlram("오캠 경로에 파일이 없습니다.\n경로 C:\\Program Files (x86)\\oCam\\oCam.exe", "파일에러", 10, "에러");
                        }
                    }
                    else if (GenieConfig.CBB_Record == 2)
                    {
                        FileInfo fi_OBS = new FileInfo("C:\\Program Files\\obs-studio\\bin\\64bit\\obs64.exe");
                        if (fi_OBS.Exists)
                        {
                            string filename = "obs64"; //종료시킬 프로그램

                            Process[] process = Process.GetProcessesByName(filename);
                            if (process.GetLength(0) == 0)
                            {
                                System.Diagnostics.Process ps = new System.Diagnostics.Process();
                                ps.StartInfo.FileName = "obs64.exe";
                                ps.StartInfo.WorkingDirectory = "C:\\Program Files\\obs-studio\\bin\\64bit\\";
                                ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                ps.Start();

                            }
                            else
                            {
                                Form1.AutoClosingAlram("OBS studio 가 실행중입니다", "실행알람", 10, "에러");
                            }
                        }
                        else
                        {
                            Form1.AutoClosingAlram("OBS studio 경로에 파일이 없습니다.\n경로 C:\\Program Files\\obs-studio\\bin\\64bit\\obs64.exe", "알람", 10, "에러");
                        }
                    }
                }

                if (GenieConfig.TB_Record_start - 1 < Get.TimeNow)
                {
                    if (Form1.RecordON)
                    {
                        Form1.RecordON = false;
                        await Task.Delay(100);
                        SendKeys.Send("{F2}");
                    }
                }
                else
                {
                    Form1.RecordON = true;
                }

                if (GenieConfig.TB_Record_end < Get.TimeNow)
                {
                    if (Form1.RecordOff)
                    {
                        Form1.RecordOff = false;
                        await Task.Delay(100);
                        SendKeys.Send("{F2}");
                    }
                }
                else
                {
                    Form1.RecordOff = true;
                }
            }
        }


    }
}
