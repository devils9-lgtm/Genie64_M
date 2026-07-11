using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64
{
    public class Helper
    {
        public static bool IsDebugMode
        {

            get
            {
                return false;


                //#if DEBUG
                //                return true;  // 디버그 모드일 때는 true를 반환
                //#else
                //        return false; // 릴리즈 모드일 때는 false를 반환
                //#endif
            }
        }

        public static 신용상세 신용상세_검색(string 검색_종목코드, string 검색_대출일)
        {
            // 1. 딕셔너리에서 종목코드로 해당 Stockbalance를 단번에 찾습니다.
            if (Form1.stockBalanceList.TryGetValue(검색_종목코드, out Stockbalance 잔고))
            {
                // 2. 잔고가 존재하면, 그 안의 신용상세리스트를 순회하며 대출일을 찾습니다.
                foreach (var 상세 in 잔고.신용상세리스트)
                {
                    if (상세.대출일 == 검색_대출일)
                    {
                        return 상세; // 일치하는 객체를 찾으면 즉시 반환 (속도 최적화)
                    }
                }
            }

            // 종목코드가 없거나, 대출일이 일치하는 항목이 없으면 null 반환
            return null;
        }

        public static void 안전종료하기()
        {
            if (!Form1.프로그램종료중)
            {
                // 백그라운드 스레드에서 UI를 건드려야 하므로 Invoke 처리 필수!
                if (Form1.form1.InvokeRequired)
                {
                    Form1.form1.Invoke(new Action(() =>
                    {
                        // 경고창 띄우기 (여기서 아빠님이 '확인'을 누를 때까지 코드가 멈춰서 기다립니다)
                        MessageBox.Show("다른 PC(또는 모바일)에서 중복 접속이 감지되었습니다.\n데이터 보호를 위해 프로그램을 안전하게 종료합니다.",
                                            "🚨 긴급 종료 (중복 접속)",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);

                        // 💡 [마법의 스위치] '확인'을 누르면 Form1_FormClosing 이벤트를 발동시킵니다!
                        Form1.form1.Close();
                    }));
                }
                else
                {
                    MessageBox.Show("다른 PC(또는 모바일)에서 중복 접속이 감지되었습니다.\n데이터 보호를 위해 프로그램을 안전하게 종료합니다.",
                                    "🚨 긴급 종료 (중복 접속)",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    Form1.form1.Close();
                }
            }
        }




        public static async void 알림창_멀티(string title, string text, int time, bool Condition)
        {
            // 백그라운드 스레드일 경우 UI 스레드로 재전송 (title 매개변수 추가됨)
            if (Form1.form1.InvokeRequired)
            {
                Form1.form1.Invoke(new MethodInvoker(() => 알림창_멀티(title, text, time, Condition)));
                return;
            }

            // 1. [소리 재생]
            Form1.비프음("언체크");

            // 2. [동적 UI 생성]
            Form popup = new Form();
            Label lb_title = new Label();
            Label lb_text = new Label();
            Button btn_close = new Button();
            Panel panel_center = new Panel();
            Panel panel_bottom = new Panel();

            // ---------------------------------------------------------
            // [UI 디자인 설정]
            // ---------------------------------------------------------
            popup.Size = new Size(550, 180);
            popup.BackColor = Color.FromArgb(180, 210, 235);
            popup.FormBorderStyle = FormBorderStyle.None;
            popup.TopMost = false; // 가장위에
            popup.StartPosition = FormStartPosition.Manual;
            popup.Padding = new Padding(3);

            // [테두리]
            popup.Paint += (sender, e) =>
            {
                using Pen pen = new Pen(Color.Black, 1);
                pen.DashStyle = DashStyle.Dot;
                Rectangle rect = popup.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                e.Graphics.DrawRectangle(pen, rect);
            };

            // 3. [위치 설정]
            try
            {
                if (Form1.form1 != null && !Form1.form1.IsDisposed && Form1.form1.Visible)
                {
                    Random rnd = new Random();
                    int centerX = Form1.form1.Location.X + (Form1.form1.Width - popup.Width) / 2;
                    int centerY = Form1.form1.Location.Y + (Form1.form1.Height - popup.Height) / 3;
                    int x = centerX + rnd.Next(-50, 50);
                    int y = centerY + rnd.Next(-50, 50);
                    popup.Location = new Point(x, y);
                }
                else
                {
                    popup.StartPosition = FormStartPosition.CenterScreen;
                }
            }
            catch
            {
                popup.StartPosition = FormStartPosition.CenterScreen;
            }

            // --- 컨트롤 배치 ---
            lb_title.Dock = DockStyle.Top;
            lb_title.Height = 35;
            lb_title.TextAlign = ContentAlignment.MiddleCenter;
            lb_title.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            lb_title.ForeColor = Color.Black;
            lb_title.BackColor = Color.Transparent;

            panel_bottom.Dock = DockStyle.Bottom;
            panel_bottom.Height = 50;
            panel_bottom.BackColor = Color.Transparent;

            btn_close.Text = "닫기";
            btn_close.Size = new Size(100, 30);
            btn_close.FlatStyle = FlatStyle.Standard;
            btn_close.Cursor = Cursors.Hand;
            btn_close.Click += (sender, e) => { popup.Close(); };
            btn_close.Location = new Point((popup.Width - btn_close.Width) / 2 - 175, 8);
            btn_close.Anchor = AnchorStyles.Top;
            panel_bottom.Controls.Add(btn_close);

            panel_center.Dock = DockStyle.Fill;
            panel_center.BackColor = Color.Transparent;
            panel_center.Padding = new Padding(20, 10, 20, 0);

            lb_text.Dock = DockStyle.None;
            lb_text.TextAlign = ContentAlignment.TopCenter;
            lb_text.Font = new Font("맑은 고딕", 10, FontStyle.Regular);
            lb_text.ForeColor = Color.Black;
            lb_text.BackColor = Color.Transparent;

            lb_text.Text = text;
            // [핵심 변경 포인트]
            lb_text.AutoSize = false; // 반드시 false로 해야 꽉 채워집니다.
            lb_text.Dock = DockStyle.Fill; // 패널의 남은 공간을 꽉 채움
            lb_text.TextAlign = ContentAlignment.MiddleCenter; // 꽉 채워진 공간 안에서 가로/세로 정중앙 정렬

            panel_center.Controls.Add(lb_text);
            popup.Controls.Add(panel_center);
            popup.Controls.Add(panel_bottom);
            popup.Controls.Add(lb_title);

            // [초기 설정] - 넘겨받은 title 변수를 제목에 적용합니다.
            if (Condition)
            {
                time += 5;
                lb_title.Text = $"[{title}]  {time} (재시도 대기)";
                lb_title.ForeColor = Color.DarkRed;
                lb_text.Text = "[실패] " + text;
            }
            else
            {
                lb_title.Text = $"[{title}]  {time}";
            }

            popup.Show(Form1.form1);

            // ---------------------------------------------------------
            // [타이머 로직]
            // ---------------------------------------------------------
            CancellationTokenSource cts = new CancellationTokenSource();
            popup.FormClosed += (sender, e) => { try { cts.Cancel(); } catch { } };

            try
            {
                while (time > 0)
                {
                    await Task.Delay(1000, cts.Token);
                    time--;

                    if (popup.IsDisposed || !popup.Visible) return;

                    // 타이머가 줄어들 때도 title 텍스트를 계속 유지합니다.
                    if (Condition)
                    {
                        int retime = time - 5;
                        if (retime < 0) retime = 0;
                        lb_title.Text = $"[{title}]  {time} (재시도 대기 {retime}초)";
                    }
                    else
                    {
                        lb_title.Text = $"[{title}]  {time}";
                    }

                    lb_title.Update();
                }

                if (!popup.IsDisposed) popup.Close();
            }
            catch (OperationCanceledException) { }
            catch (Exception) { }
            finally
            {
                cts.Dispose();
                if (!popup.IsDisposed) popup.Dispose();
            }
        }


        // [지니 최적화] JSON 데이터 안전한 정수 변환 (빈 값, 에러 처리 포함)
        public static int StringToInt(object value)
        {
            if (value == null) return 0;

            string s = value.ToString().Trim();
            if (string.IsNullOrEmpty(s)) return 0;

            // 성공하면 숫자 반환, 실패하면 0 반환
            if (int.TryParse(s, out int result))
            {
                return Math.Abs(result); // 가격은 항상 양수로 반환 (필요시 제거 가능)
            }

            return 0;
        }

        // [지니 최적화] JSON 데이터 안전한 실수(Double) 변환 (빈 값, 에러 처리 포함)
        public static double StringToDouble(object value)
        {
            if (value == null) return 0;

            string s = value.ToString().Trim();
            if (string.IsNullOrEmpty(s)) return 0;

            // 성공하면 숫자 반환, 실패하면 0 반환
            if (double.TryParse(s, out double result))
            {
                return result;
            }

            return 0;
        }

        public static long StringToLong(object value)
        {
            if (value == null) return 0;
            string s = value.ToString().Trim();
            if (string.IsNullOrEmpty(s)) return 0;
            if (long.TryParse(s, out long result)) return result;
            return 0;
        }
        public static void 안전한_UI_업데이트(Control 컨트롤, Action 작업)
        {
            // 1. 객체 자체가 null이면 즉시 종료 (이건 에러 안 남)
            if (컨트롤 == null) return;

            try
            {
                // 2. [핵심 수정] 속성 읽기(IsDisposed, InvokeRequired 등)를 try 안으로 넣었습니다.
                //    종료 시점에는 'IsHandleCreated'나 'InvokeRequired'를 읽는 것만으로도 에러가 날 수 있습니다.
                if (컨트롤.IsDisposed || 컨트롤.Disposing || !컨트롤.IsHandleCreated) return;

                // 3. 스레드 체크
                if (컨트롤.InvokeRequired)
                {
                    // [비동기 처리] BeginInvoke를 사용하여 UI 스레드가 바쁠 때 대기하지 않고 넘김 (저사양 PC 버벅임 방지)
                    // 반드시 동기(Invoke)가 필요한 상황이 아니라면 UI 업데이트는 BeginInvoke가 훨씬 부드럽습니다.
                    컨트롤.BeginInvoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            // 마샬링 되어 들어온 사이 컨트롤이 죽었는지 다시 확인
                            if (컨트롤 != null && !컨트롤.IsDisposed && !컨트롤.Disposing && 컨트롤.IsHandleCreated)
                            {
                                작업();
                            }
                        }
                        catch
                        {
                            // UI 스레드 내부 실행 중 에러 무시
                        }
                    }));
                }
                else
                {
                    // 같은 스레드면 바로 실행
                    작업();
                }
            }
            catch (Exception)
            {
                // 4. [완벽 방어]
                // 프로그램 종료 시점에 발생하는 모든 UI 에러(Overflow, Handle 등)는
                // 사용자에게 보여줄 필요 없이 무시하는 것이 정답입니다.
            }
        }

        // [지니 최적화] 최종매입가 추가하기
        public static void 최종매입가_추가(string 종목코드, string 위치, int 번호, int 가격)
        {
            // 해당 종목의 리스트가 없으면 새로 만들고 가져옴
            var 리스트 = Form1.최종매입가_List.GetOrAdd(종목코드, _ => new List<최종매입가>());

            lock (리스트)
            {
                리스트.Add(new 최종매입가
                {
                    종목명 = Form1.Market_Item_List[종목코드].종목명,
                    종목코드 = 종목코드,
                    위치 = 위치,
                    번호 = 번호,
                    매입가 = 가격
                });
            }
        }

        // [지니 최적화] 특정 위치(예: 리밸_A)의 가장 큰 번호 가져오기
        // 기존의 복잡한 정렬 로직을 이 함수 하나로 대체합니다.
        public static int 통합_최종번호_가져오기(string 종목코드, string 위치)
        {
            int 장부_최대번호 = 0;
            int 메모리_최대번호 = 0;

            // 1. [과거 장부 스캔] 최종매입가_List 확인
            if (Form1.최종매입가_List.TryGetValue(종목코드, out List<최종매입가> 리스트))
            {
                lock (리스트)
                {
                    장부_최대번호 = 리스트.Where(o => o.위치 == 위치)
                                          .Select(o => o.번호)
                                          .DefaultIfEmpty(0)
                                          .Max();
                }
            }

            // 2. [실시간 메모리 스캔] 감시주문_List 확인
            // 아빠님이 쓰셨던 DefaultIfEmpty(0).Max() 패턴을 여기서도 똑같이 써서 아주 깔끔하게 줄였습니다.
            메모리_최대번호 = Form1.감시주문_List.Values
                                   .Where(x => x.종목코드 == 종목코드 && x.검색식 != null && x.검색식.Contains(위치))
                                   .Select(x => x.최종번호)
                                   .DefaultIfEmpty(0)
                                   .Max();

            // 3. 둘 중 더 큰 번호를 진정한 최대 번호로 반환!
            return Math.Max(장부_최대번호, 메모리_최대번호);
        }

        public static void 최종매입가_신규추가(string 종목코드, int 체결가)
        {
            최종매입가_추가(종목코드, "리밸_A", 0, 체결가);
            최종매입가_추가(종목코드, "리밸_B", 0, 체결가);
            최종매입가_추가(종목코드, "리밸_C", 0, 체결가);
            최종매입가_추가(종목코드, "리밸_D", 0, 체결가);
            최종매입가_추가(종목코드, "리밸_E", 0, 체결가);
            최종매입가_추가(종목코드, "리밸_F", 0, 체결가);
            최종매입가_추가(종목코드, "리밸_G", 0, 체결가);
        }

        public static bool NXT_매수매도_금지(bool 매수매도)
        {
            // [최적화 1] 조기 종료
            // 서버가 꺼져있다면 '금지 상태'가 아니므로(또는 작동 안 하므로) false 반환
            if (!Form1.NXT_server) return false;

            // [최적화 2] 삼항 연산자로 단축
            // if-else 지옥을 없애고, 설정값을 '있는 그대로' 반환합니다.
            // 해석: 매수(true)인가요? -> 그럼 '매수금지' 체크박스 값을 주세요.
            //       아니면(매도)?   -> 그럼 '손실제한' 체크박스 값을 주세요.
            return 매수매도 ? GenieConfig.CB_NXT_매수금지 : GenieConfig.CB_NXT_손실제한;
        }

        public static void 최종매입가_채워넣기_및_검사()
        {
            if (Form1.로딩완료 == false) return;

            bool 상세로그_켜기 = true;
            System.Text.StringBuilder 파일로그_저장용 = new System.Text.StringBuilder();

            string[] requiredLocations = { "리밸_A", "리밸_B", "리밸_C", "리밸_D", "리밸_E", "리밸_F", "리밸_G" };

            int 감시주문_보정된_갯수 = 0;
            int 보정된_갯수 = 0;
            int 삭제된_갯수 = 0;
            int 재정렬된_갯수 = 0;

            if (상세로그_켜기) 파일로그_저장용.AppendLine($"\n========== [데이터 무결성 검사 (0차 절대방어 및 연동 매칭) : {DateTime.Now:yyyy-MM-dd HH:mm:ss}] ==========");

            // ==============================================================================
            // [1단계] 감시주문 족보 정리 (체결가격순 정렬 및 연동감시번호 매칭)
            // ==============================================================================
            var 감시주문_KVP_목록 = Form1.감시주문_List.Where(kvp => kvp.Value.수익구분 == 7).ToList();
            var 감시주문_그룹 = 감시주문_KVP_목록.GroupBy(kvp => new { kvp.Value.종목코드, kvp.Value.종목명 }).ToList();

            int 오류종목_카운트 = 0;

            foreach (var group in 감시주문_그룹)
            {
                var locationGrouped = group.GroupBy(kvp =>
                {
                    string text = kvp.Value.검색식 ?? "";
                    foreach (var reqLoc in requiredLocations)
                    {
                        if (text.Contains(reqLoc)) return reqLoc;
                    }
                    return "기타";
                }).Where(g => g.Key != "기타").OrderBy(g => g.Key);

                List<string> 오류_로그_임시보관 = new List<string>();
                bool 이종목에서_수정발생 = false;

                foreach (var locGroup in locationGrouped)
                {
                    var 마스터_목록 = locGroup.Where(k => (k.Value.검색식 ?? "").Contains("_1차")).ToList();
                    var 종속_목록 = locGroup.Where(k => !(k.Value.검색식 ?? "").Contains("_1차")).ToList();

                    if (마스터_목록.Count == 0) continue;

                    var 정렬된_마스터들 = 마스터_목록.OrderByDescending(k => k.Value.주문체결가격).ToList();
                    int 올바른_차수 = 1;

                    foreach (var masterKvp in 정렬된_마스터들)
                    {
                        var master = masterKvp.Value;

                        if (master.최종번호 != 올바른_차수)
                        {
                            오류_로그_임시보관.Add($"     -> [1차 정렬] {master.주문체결가격:N0}원 : 기존 {master.최종번호}차 -> {올바른_차수}차로 변경");
                            master.최종번호 = 올바른_차수;
                            감시주문_보정된_갯수++;
                            이종목에서_수정발생 = true;
                        }

                        var 내_자식들 = 종속_목록.Where(slave =>
                            slave.Value.연동감시번호 != 0 &&
                            (slave.Value.연동감시번호 == master.감시번호 || slave.Value.연동감시번호 == master.연동감시번호)
                        ).ToList();

                        foreach (var slaveKvp in 내_자식들)
                        {
                            var slave = slaveKvp.Value;
                            if (slave.최종번호 != 올바른_차수)
                            {
                                오류_로그_임시보관.Add($"     -> [2차 연동] 부모 따라가기 : 기존 {slave.최종번호}차 -> {올바른_차수}차로 동기화 ({slave.검색식})");
                                slave.최종번호 = 올바른_차수;
                                감시주문_보정된_갯수++;
                                이종목에서_수정발생 = true;
                            }
                        }
                        올바른_차수++;
                    }

                    if (이종목에서_수정발생) 오류_로그_임시보관.Insert(0, $"  >> {locGroup.Key} 구역 족보 재배치 완료");
                }

                if (이종목에서_수정발생 && 상세로그_켜기)
                {
                    파일로그_저장용.AppendLine($"\n[감시주문 족보 복구] {group.Key.종목명}({group.Key.종목코드})");
                    foreach (var log in 오류_로그_임시보관) 파일로그_저장용.AppendLine(log);
                    오류종목_카운트++;
                }
            }

            // ==============================================================================
            // [2단계] 최종매입가 동기화 (지니의 메모리 '감시주문_List' 기준 절대 동기화)
            // ==============================================================================
            foreach (var kvp in Form1.최종매입가_List)
            {
                string 종목코드 = kvp.Key;
                List<최종매입가> 아이템리스트 = kvp.Value;
                string 종목명 = Form1.Market_Item_List.ContainsKey(종목코드) ? Form1.Market_Item_List[종목코드].종목명 : 종목코드;

                bool 이종목_최종매입가로그_찍힘 = false;

                // 💡 [안전장치 1] 0차 생성 및 보정을 위한 기준 가격 확보 로직
                int 시작가격 = 0;
                var 다른위치_0차 = 아이템리스트.FirstOrDefault(x => x.번호 == 0 && x.매입가 > 0);

                if (다른위치_0차 != null)
                {
                    // 1. 다른 구역에 정상적인 0차가 존재하면 그 가격을 복사
                    시작가격 = (int)다른위치_0차.매입가;
                }
                else
                {
                    // 2. 다른 구역에도 없거나 전부 0원이면 잔고 데이터를 뒤져서 가져옴
                    if (Form1.stockBalanceList.TryGetValue(종목코드, out var 잔고데이터))
                    {
                        시작가격 = (int)잔고데이터.시작가격;

                        if (시작가격 == 0)
                        {
                            시작가격 = 잔고데이터.현재가 > 0 ? 잔고데이터.현재가 : 잔고데이터.평균단가;
                        }
                    }
                    else if (아이템리스트.Count > 0)
                    {
                        var 유효매입가 = 아이템리스트.FirstOrDefault(x => x.매입가 > 0);
                        if (유효매입가 != null) 시작가격 = (int)유효매입가.매입가; // 최후의 보루
                    }
                }

                bool 영차_생성_스킵 = (시작가격 == 0);

                lock (아이템리스트)
                {
                    foreach (string location in requiredLocations)
                    {
                        List<string> locLogs = new List<string>();
                        bool 현재_위치에_문제가_있었음 = false;

                        // [타겟] 내 장부
                        var 내장부_아이템들 = 아이템리스트.Where(x => x.위치 == location).OrderBy(x => x.번호).ToList();

                        // [기준] 지니 감시주문
                        var 지니의_감시주문_목록 = Form1.감시주문_List.Values.Where(o =>
                            o.수익구분 == 7 && o.종목코드 == 종목코드 && (o.검색식 ?? "").Contains(location) && (o.검색식 ?? "").Contains("_1차")).ToList();

                        var 지니의_유효번호_리스트 = 지니의_감시주문_목록.Select(o => o.최종번호).ToList();

                        // ----------------------------------------------------------------------
                        // 1. 0차 생성 및 기존 0원짜리 강제 치료 로직
                        // ----------------------------------------------------------------------
                        var zeroItem = 내장부_아이템들.FirstOrDefault(x => x.번호 == 0);

                        if (zeroItem == null)
                        {
                            // [신규 생성] 0차가 아예 없을 때
                            if (!영차_생성_스킵)
                            {
                                string 생성출처 = 다른위치_0차 != null ? $"다른구역({다른위치_0차.위치}) 복사" : "잔고 시작/현재가 적용";
                                locLogs.Add($"     -> [+] 0차 필수 생성 : {시작가격:N0}원으로 생성 ({생성출처})");

                                zeroItem = new 최종매입가 { 종목명 = 종목명, 종목코드 = 종목코드, 위치 = location, 번호 = 0, 매입가 = 시작가격 };
                                아이템리스트.Add(zeroItem);
                                내장부_아이템들.Insert(0, zeroItem);
                                보정된_갯수++;
                                현재_위치에_문제가_있었음 = true;
                            }
                            else if (상세로그_켜기)
                            {
                                locLogs.Add($"     -> [!] 0차 생성 보류 : 기준 가격이 0원이라 데이터 오염 방지를 위해 생성을 건너뜁니다.");
                                현재_위치에_문제가_있었음 = true;
                            }
                        }
                        else if (zeroItem.매입가 == 0)
                        {
                            // 💡 [치료 로직] 0차 데이터가 있긴 한데 매입가가 0원인 쓰레기 데이터일 때
                            if (!영차_생성_스킵)
                            {
                                locLogs.Add($"     -> [~] 0원 오류 치료 : 기존 0차 데이터의 0원 오류를 {시작가격:N0}원으로 강제 복구");
                                zeroItem.매입가 = 시작가격;
                                보정된_갯수++;
                                현재_위치에_문제가_있었음 = true;
                            }
                        }

                        // ----------------------------------------------------------------------
                        // 2. [삭제] 지니의 감시주문 리스트에는 없는데, 내 장부에만 있는 유령 삭제
                        // ----------------------------------------------------------------------
                        for (int i = 내장부_아이템들.Count - 1; i >= 0; i--)
                        {
                            var item = 내장부_아이템들[i];
                            if (item.번호 != 0 && !지니의_유효번호_리스트.Contains(item.번호))
                            {
                                locLogs.Add($"     -> [-] 유령 매입가 삭제 : {item.번호}차 지움 (지니의 감시주문 리스트에 존재하지 않음)");
                                아이템리스트.Remove(item);
                                내장부_아이템들.RemoveAt(i);
                                삭제된_갯수++;
                                현재_위치에_문제가_있었음 = true;
                            }
                        }

                        // ----------------------------------------------------------------------
                        // 3. [생성/수정] 지니 감시주문 기준 누락분 생성 및 가격 동기화
                        // ----------------------------------------------------------------------
                        foreach (var 지니주문 in 지니의_감시주문_목록)
                        {
                            int 지니_차수 = 지니주문.최종번호;
                            int 지니_가격 = (int)지니주문.주문체결가격;
                            var 내장부_존재확인 = 내장부_아이템들.FirstOrDefault(x => x.번호 == 지니_차수);

                            if (내장부_존재확인 == null)
                            {
                                if (지니_가격 > 0)
                                {
                                    locLogs.Add($"     -> [+] 누락 복구 : {지니_차수}차 매입가 생성 (지니 감시주문 체결가 기준: {지니_가격:N0})");
                                    var newItem = new 최종매입가 { 종목명 = 종목명, 종목코드 = 종목코드, 위치 = location, 번호 = 지니_차수, 매입가 = 지니_가격 };
                                    아이템리스트.Add(newItem);
                                    내장부_아이템들.Add(newItem);
                                    보정된_갯수++;
                                    현재_위치에_문제가_있었음 = true;
                                }
                            }
                            else if (내장부_존재확인.매입가 != 지니_가격 || 내장부_존재확인.매입가 == 0)
                            {
                                // 💡 1차 이상의 데이터 중 가격이 틀리거나 0원짜리 골칫덩이일 경우 수정
                                if (지니_가격 > 0)
                                {
                                    locLogs.Add($"     -> [~] 가격 보정 : {지니_차수}차 단가 오류 수정 ({내장부_존재확인.매입가:N0} -> 지니 기준가 {지니_가격:N0})");
                                    내장부_존재확인.매입가 = 지니_가격;
                                    보정된_갯수++;
                                    현재_위치에_문제가_있었음 = true;
                                }
                            }
                        }

                        // ----------------------------------------------------------------------
                        // 4. [재정렬] 뒤죽박죽 꼬인 족보를 0, 1, 2, 3... 순서대로 이빨 맞추기
                        // ----------------------------------------------------------------------
                        내장부_아이템들 = 내장부_아이템들.OrderBy(x => x.번호).ToList();
                        for (int i = 0; i < 내장부_아이템들.Count; i++)
                        {
                            if (내장부_아이템들[i].번호 != i)
                            {
                                locLogs.Add($"     -> [~] 족보 정렬 : 기존 {내장부_아이템들[i].번호}차를 -> {i}차로 강제 수정 (이빨 맞추기)");
                                내장부_아이템들[i].번호 = i;
                                재정렬된_갯수++;
                                현재_위치에_문제가_있었음 = true;
                            }
                        }

                        if (현재_위치에_문제가_있었음 && 상세로그_켜기)
                        {
                            if (!이종목_최종매입가로그_찍힘)
                            {
                                파일로그_저장용.AppendLine($"\n[최종매입가 보정 완료] {종목명}({종목코드})");
                                이종목_최종매입가로그_찍힘 = true;
                            }
                            파일로그_저장용.AppendLine($"  >> {location}");
                            foreach (var msg in locLogs) 파일로그_저장용.AppendLine(msg);
                        }
                    }
                }
            }

            if (!Form1.릴리즈)
            {
                // ==============================================================================
                // [결과 요약 리포트 저장]
                // ==============================================================================
                if (감시주문_보정된_갯수 > 0 || 보정된_갯수 > 0 || 삭제된_갯수 > 0 || 재정렬된_갯수 > 0)
                {
                    DateTime now = DateTime.Now;

                    파일로그_저장용.Insert(0, $"\n[{now:HH:mm:ss} 무결성 검사 작동]\n");

                    파일로그_저장용.AppendLine($"=========================================================");
                    파일로그_저장용.AppendLine($" [데이터 청소 및 동기화 완료 리포트]");
                    파일로그_저장용.AppendLine($"=========================================================");
                    if (감시주문_보정된_갯수 > 0) 파일로그_저장용.AppendLine($" [족보 교정] {오류종목_카운트}개 종목에서 {감시주문_보정된_갯수}건의 감시주문 차수를 가격순 및 연동번호로 맞췄습니다.");
                    if (보정된_갯수 > 0) 파일로그_저장용.AppendLine($" >> [누락 복구/치료] 0원 오류 치료 및 {보정된_갯수}건의 매입가를 생성/수정했습니다.");
                    if (삭제된_갯수 > 0) 파일로그_저장용.AppendLine($" >> [유령 삭제] 지니의 감시주문 리스트에 없는 가짜 매입가 {삭제된_갯수}개를 지웠습니다.");
                    if (재정렬된_갯수 > 0) 파일로그_저장용.AppendLine($" >> [매입가 정렬] {재정렬된_갯수}건의 꼬인 매입가 번호를 0차부터 순차 배열했습니다.");
                    파일로그_저장용.AppendLine($"=========================================================\n");

                    try
                    {
                        string 데이터폴더경로 = Application.StartupPath + "\\Data";
                        if (!System.IO.Directory.Exists(데이터폴더경로)) System.IO.Directory.CreateDirectory(데이터폴더경로);

                        string 파일명 = $"무결성검사_완전판_{now:yyyyMMdd}.txt";
                        string 최종경로 = System.IO.Path.Combine(데이터폴더경로, 파일명);

                        if (System.IO.File.Exists(최종경로))
                        {
                            string 기존내용 = System.IO.File.ReadAllText(최종경로, System.Text.Encoding.UTF8);
                            파일로그_저장용.Append(기존내용);
                        }

                        System.IO.File.WriteAllText(최종경로, 파일로그_저장용.ToString(), System.Text.Encoding.UTF8);

                        Form1.Console_print($"[데이터 무결성 검사] 오류 치료 완료! (Data 폴더 로그: {파일명})");
                    }
                    catch { }
                }
            }
        }

        // 💡 void 대신 async Task로 변경하여 최신 비동기 트렌드 적용!
        // [추가] 문지기 역할을 할 변수입니다. (0: 대기 중, 1: 실행 중)
        public static int _미체결동기화_진행중 = 0;

        public static async Task 미체결내역동기화(bool error) //opt10075 미체결 불러오기
        {
            if (Interlocked.Exchange(ref _미체결동기화_진행중, 1) == 1) return;

            await Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(1000);
                    int 대기시간 = 0;

                    while (true)
                    {
                        // === [좀비 방어막] 큐가 비워지길 기다리다가 봇을 끄면? 즉시 퇴근! ===
                        if (Form1.프로그램종료중) return;

                        if (Form1.order_scheduler.QueueCount == 0)
                        {
                            // 1. 메인 장부의 속성을 true로 변경 (보조 장부의 속성도 자동으로 true로 바뀜)
                            foreach (var kvp in Form1.JumunItem_List)
                            {
                                kvp.Value.주문동기화 = true;
                            }

                            // 3. 깨끗해진 장부 상태로 서버에 미체결 내역 요청
                            TR_요청.미체결요청("Y", "", error);

                            Form1.주문내역동기화 = true;
                            Form1.Console_print(">> 대기열 처리 및 장부 동기화 완료! 미체결 내역 서버 요청을 진행합니다.");
                            break;
                        }

                        대기시간++;
                        if (대기시간 % 5 == 0)
                        {
                            Form1.Console_print($"-> 미체결 동기화 대기 중... (남은 대기열: {Form1.order_scheduler.QueueCount}개)");
                        }

                        await Task.Delay(1000);
                    }
                }
                finally
                {
                    // === [문지기 초기화] ===
                    Interlocked.Exchange(ref _미체결동기화_진행중, 0);
                }
            });
        }








    }
}
