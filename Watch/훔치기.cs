using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace 지니64
{
    internal class 훔치기
    {
        // [로그 OFF] 스캔 속도 측정을 위해 상세 좌표/클릭 추적 로그를 잠시 끕니다. 
        public static bool 상세추적로그_켜기 = false;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool IsIconic(IntPtr hWnd);

        const int SW_RESTORE = 9;

        // 마켓중심 전용 데이터 구조체
        struct MarketText
        {
            public string Text;
            public int X;
            public MarketText(string t, int x) { Text = t; X = x; }
        }
        private static List<MarketText> 마켓글자기록 = new List<MarketText>();

        // F존 전용 변수
        private static List<string> 글자기록 = new List<string>();
        private static string 현재서브탭;
        private static int 탐색카운트 = 0;
        private static bool 스캔종료 = false;
        private static bool 스캔중 = false;
        private static bool 진짜데이터시작 = false;

        private static HashSet<string> 종목명사전 = null;
        private static Stopwatch 탭스캔타이머 = new Stopwatch();
        private static long 마지막발견시간 = 0;

        private static bool 티마_창_깨우기()
        {
            try
            {
                var 티마창 = 티마_창_찾기();
                if (티마창 == null) return false;

                IntPtr 핸들 = new IntPtr(티마창.Current.NativeWindowHandle);
                if (핸들 != IntPtr.Zero)
                {
                    if (IsIconic(핸들)) { ShowWindow(핸들, SW_RESTORE); Thread.Sleep(200); }
                    SetForegroundWindow(핸들);
                }
                return true;
            }
            catch { return false; }
        }

        // ====================================================
        // [1] 마켓중심 스캔 (초고속 단일 스캔 모드)
        // ====================================================
        public static void 수동스캔_마켓중심()
        {
            if (스캔중) return;
            스캔중 = true;
            try
            {
                if (!티마_창_깨우기()) { Form1.Console_print("[오류] 티마 창을 찾을 수 없습니다."); return; }

                Stopwatch 전체sw = Stopwatch.StartNew();
                Form1.Console_print("\n========== [독립엔진] 마켓중심 초고속 스캔 시작 ==========");

                현재서브탭 = "";
                마켓글자기록.Clear();
                탐색카운트 = 0;
                스캔종료 = false;
                마지막발견시간 = 0;

                if (!단순_버튼_클릭_실행("마켓중심")) { Form1.Console_print("[오류] 마켓중심 탭 클릭 실패!"); return; }
                스마트_대기(1500);

                var 티마창 = 티마_창_찾기();
                if (티마창 != null)
                {
                    탭스캔타이머.Restart();
                    마켓중심_파고들기(티마창, 0);
                    탭스캔타이머.Stop();
                }

                if (마켓글자기록.Count == 0)
                {
                    Form1.Console_print("[경고] 마켓중심 데이터 없음!");
                }
                else
                {
                    Form1.Console_print($"\n--- [수집된 데이터 분석 결과 ({마켓글자기록.Count}개 단어)] ---");

                    // 각 열(기둥)의 시작점(X좌표 최소값) 찾기
                    List<int> columnBases = new List<int>();
                    for (int i = 0; i < 마켓글자기록.Count; i++)
                    {
                        string name = 마켓글자기록[i].Text;
                        if ((name == "억" || name == "%") && i >= 2)
                        {
                            int targetX = 마켓글자기록[i - 2].X;
                            bool isFound = false;
                            for (int j = 0; j < columnBases.Count; j++)
                            {
                                if (Math.Abs(columnBases[j] - targetX) < 50)
                                {
                                    columnBases[j] = Math.Min(columnBases[j], targetX);
                                    isFound = true; break;
                                }
                            }
                            if (!isFound) columnBases.Add(targetX);
                        }
                    }

                    // 출력
                    for (int i = 0; i < 마켓글자기록.Count; i++)
                    {
                        string name = 마켓글자기록[i].Text;

                        if (name == "억" && i >= 2)
                        {
                            var sector = 마켓글자기록[i - 2];
                            int baseX = sector.X;
                            foreach (int b in columnBases) if (Math.Abs(b - sector.X) < 50) { baseX = b; break; }

                            string mark = (sector.X >= baseX + 12) ? "[별] " : "";
                            Form1.Console_print($"{mark}[{sector.Text}] (섹터)");
                        }
                        else if (name == "%" && i >= 2)
                        {
                            var stock = 마켓글자기록[i - 2];
                            if (진짜종목인지확인(stock.Text))
                            {
                                int baseX = stock.X;
                                foreach (int b in columnBases) if (Math.Abs(b - stock.X) < 50) { baseX = b; break; }

                                string mark = (stock.X >= baseX + 12) ? "[왕관] " : "";
                                Form1.Console_print($"{mark}{stock.Text} (종목)");
                            }
                        }
                    }
                }

                전체sw.Stop();
                Form1.Console_print($"\n[T] 마켓중심 스캔 총 소요시간: {전체sw.ElapsedMilliseconds}ms");
            }
            finally { 스캔중 = false; }
        }

        private static void 마켓중심_파고들기(AutomationElement 부모요소, int 깊이)
        {
            if (부모요소 == null || 스캔종료 || 깊이 > 30) return;

            long 현재시간 = 탭스캔타이머.ElapsedMilliseconds;
            if (현재시간 - 마지막발견시간 > 4000 && 탐색카운트 > 50) { 스캔종료 = true; return; }
            if (현재시간 > 25000) { 스캔종료 = true; return; }

            AutomationElement 자식 = TreeWalker.ControlViewWalker.GetFirstChild(부모요소);

            while (자식 != null && !스캔종료)
            {
                탐색카운트++;
                bool 스킵할까요 = false;
                try
                {
                    // 화면에 안 보이는 애들은 가차 없이 스킵! (속도 향상의 핵심)
                    if (자식.Current.IsOffscreen) 스킵할까요 = true;
                    var 사각형 = 자식.Current.BoundingRectangle;
                    if (사각형.IsEmpty || 사각형.Width <= 0 || 사각형.Height <= 0) 스킵할까요 = true;

                    // 38스윙 유령은 Y=119 부근이므로, 170까지만 자릅니다!
                    if (사각형.Y > 0 && 사각형.Y < 170) 스킵할까요 = true;
                }
                catch { }

                if (!스킵할까요)
                {
                    try
                    {
                        var 컨트롤타입 = 자식.Current.ControlType;
                        string 이름 = (자식.Current.Name ?? "").Trim();
                        var 사각형 = 자식.Current.BoundingRectangle;

                        if (컨트롤타입 == ControlType.Text && !string.IsNullOrWhiteSpace(이름))
                        {
                            마지막발견시간 = 탭스캔타이머.ElapsedMilliseconds;

                            if (이름.Contains("티마에 검색 또는 도출") || 이름 == "시장종합") { 스캔종료 = true; return; }

                            if (상세추적로그_켜기)
                            {
                                Form1.Console_print($"[좌표추적] X:{사각형.X} | Y:{사각형.Y} | 글자:'{이름}'");
                            }

                            string 순수이름 = 이름.Replace("★", "").Replace("👑", "").Replace("🔥", "").Trim();
                            마켓글자기록.Add(new MarketText(순수이름, (int)사각형.X));
                        }
                    }
                    catch { }
                }

                if (!스캔종료) 마켓중심_파고들기(자식, 깊이 + 1);
                자식 = TreeWalker.ControlViewWalker.GetNextSibling(자식);
            }
        }

        // ====================================================
        // [2] F존 전체 스캔 (초고속 단일 스캔 모드)
        // ====================================================
        public static void 수동스캔_F존_전체()
        {
            if (스캔중) return;
            스캔중 = true;

            try
            {
                if (!티마_창_깨우기()) { Form1.Console_print("[오류] 티마 창을 찾을 수 없습니다."); return; }

                Stopwatch 전체sw = Stopwatch.StartNew();
                Form1.Console_print("\n========== [독립엔진] F존 전체 지능형 스캔 시작 ==========");

                if (!단순_버튼_클릭_실행("F존")) { Form1.Console_print("[오류] F존 메인 탭 클릭 실패!"); return; }
                스마트_대기(1000);

                List<string> 탭들 = 서브탭_이름_목록_가져오기("F존포착");
                if (탭들.Count == 0) 탭들 = new List<string> { "F존포착", "F존 +", "SF존", "골드존", "38스윙" };

                foreach (string 서브탭 in 탭들)
                {
                    현재서브탭 = 서브탭;
                    Form1.Console_print($"\n>>> [{서브탭}] 스캔 시작");

                    글자기록.Clear();
                    탐색카운트 = 0;
                    스캔종료 = false;
                    진짜데이터시작 = false;
                    마지막발견시간 = 0;

                    if (!단순_버튼_클릭_실행(서브탭 == "F존 +" ? "+" : 서브탭))
                        Form1.Console_print($"[경고] '{서브탭}' 클릭 실패!");

                    스마트_대기(800);

                    // 탭 클릭 후 화면 갱신을 위해 요소를 다시 가져옵니다.
                    var 티마창 = 티마_창_찾기();
                    if (티마창 != null)
                    {
                        탭스캔타이머.Restart();
                        F존_파고들기(티마창, 0);
                        탭스캔타이머.Stop();

                        F존_데이터_일괄분석();
                    }
                }

                전체sw.Stop();
                Form1.Console_print($"\n[최종보고] F존 전체 전수조사 총 소요시간: {전체sw.ElapsedMilliseconds}ms");
            }
            finally
            {
                스캔중 = false;
            }
        }

        private static void F존_파고들기(AutomationElement 부모요소, int 깊이)
        {
            if (부모요소 == null || 스캔종료 || 깊이 > 30) return;

            long 현재시간 = 탭스캔타이머.ElapsedMilliseconds;
            if (현재시간 - 마지막발견시간 > 3000 && 탐색카운트 > 50) { 스캔종료 = true; return; }
            if (현재시간 > 30000) { 스캔종료 = true; return; }

            AutomationElement 자식 = TreeWalker.RawViewWalker.GetFirstChild(부모요소);

            while (자식 != null && !스캔종료)
            {
                탐색카운트++;
                bool 스킵할까요 = false;
                try
                {
                    if (자식.Current.IsOffscreen) 스킵할까요 = true;
                    var 사각형 = 자식.Current.BoundingRectangle;
                    if (사각형.IsEmpty || 사각형.Width <= 0 || 사각형.Height <= 0) 스킵할까요 = true;
                }
                catch { }

                if (스킵할까요)
                {
                    자식 = TreeWalker.RawViewWalker.GetNextSibling(자식);
                    continue;
                }

                try
                {
                    if (자식.Current.ControlType == ControlType.Text)
                    {
                        string 이름 = (자식.Current.Name ?? "").Trim();
                        if (!string.IsNullOrWhiteSpace(이름))
                        {
                            마지막발견시간 = 탭스캔타이머.ElapsedMilliseconds;

                            if (이름.Contains("티마에 검색 또는 도출") || 이름 == "시장종합") { 스캔종료 = true; return; }

                            if (!진짜데이터시작)
                            {
                                if (이름 == "일정" || 이름 == "38스윙" || 이름 == "종목명" || 이름 == "상한가" || 이름 == "현재가") 진짜데이터시작 = true;
                            }
                            else
                            {
                                글자기록.Add(이름);
                            }
                        }
                    }
                }
                catch { }

                if (!스캔종료) F존_파고들기(자식, 깊이 + 1);
                자식 = TreeWalker.RawViewWalker.GetNextSibling(자식);
            }
        }

        // ====================================================
        // [4] 지능형 테이블 분석기 (F존 전용)
        // ====================================================
        private static void F존_데이터_일괄분석()
        {
            HashSet<string> 출력체크 = new HashSet<string>();

            if (현재서브탭 == "F존포착")
            {
                string 종목명 = "", 뱃지 = "";
                int 현 = 0, 목 = 0, b1 = 0, b2 = 0, b3 = 0;
                bool 다음은종목 = false;

                for (int i = 0; i < 글자기록.Count; i++)
                {
                    string 이름 = 글자기록[i];
                    if (이름 == "38스윙") { 다음은종목 = true; continue; }
                    if (다음은종목 && 진짜종목인지확인(이름)) { 종목명 = 이름; 다음은종목 = false; 현 = 0; 목 = 0; b1 = 0; b2 = 0; b3 = 0; 뱃지 = ""; continue; }

                    if (이름 == "현재가" && i > 0) { string 이전 = 글자기록[i - 1]; if (!이전.Contains("월") && !이전.Contains(":")) 뱃지 = 이전; }
                    else if (i > 0 && 글자기록[i - 1] == "현재가") 현 = 가격변환(이름);
                    else if (i > 0 && 글자기록[i - 1] == "목표가") 목 = 가격변환(이름);
                    else if (i > 0 && 글자기록[i - 1] == "B1" && 현 > 0) b1 = 가격변환(이름);
                    else if (i > 0 && 글자기록[i - 1] == "B2") b2 = 가격변환(이름);
                    else if (i > 0 && 글자기록[i - 1] == "B3")
                    {
                        b3 = 가격변환(이름);
                        string 결과 = "대기중";
                        if (b3 > 0 && 현 <= b3) 결과 = "B3 포착";
                        else if (b2 > 0 && 현 <= b2) 결과 = "B2 포착";
                        else if (b1 > 0 && 현 <= b1) 결과 = "B1 포착";

                        string 뱃지표시 = string.IsNullOrEmpty(뱃지) ? "" : $" [{뱃지}]";
                        string 라인 = $"[{현재서브탭}][{종목명}]{뱃지표시} 현재:{현:N0} | 목표:{목:N0} | B1:{b1:N0} | B2:{b2:N0} | B3:{b3:N0} => {결과}";

                        if (출력체크.Add(라인)) Form1.Console_print(라인);
                        다음은종목 = true;
                    }
                }
            }
            else
            {
                string 종목명 = "";
                List<int> 숫자들 = new List<int>();

                foreach (string 글자 in 글자기록)
                {
                    if (진짜종목인지확인(글자))
                    {
                        출력_테이블_종목(종목명, 숫자들, 출력체크);
                        종목명 = 글자;
                        숫자들.Clear();
                    }
                    else if (!string.IsNullOrEmpty(종목명))
                    {
                        int 값 = 가격변환(글자);
                        if (값 > 0) 숫자들.Add(값);
                    }
                }
                출력_테이블_종목(종목명, 숫자들, 출력체크);
            }
        }

        private static void 출력_테이블_종목(string 종목명, List<int> 숫자들, HashSet<string> 출력체크)
        {
            if (string.IsNullOrEmpty(종목명) || 숫자들.Count < 3) return;

            string 특이사항 = "";
            int idx = 글자기록.IndexOf(종목명);
            if (idx > 0 && 글자기록[idx - 1].StartsWith("[특이:"))
            {
                특이사항 = 글자기록[idx - 1].Replace("[특이:", "").Replace("]", "") + " ";
            }
            string 최종종목명 = 특이사항 + 종목명;

            int 현재가 = 0, T1 = 0, T2 = 0, T3 = 0;
            string 타점명 = "B";

            if (현재서브탭 == "38스윙" || 현재서브탭 == "골드존")
            {
                타점명 = 현재서브탭 == "38스윙" ? "J" : "G";
                현재가 = 숫자들[0];
                if (숫자들.Count >= 5) { T1 = 숫자들[2]; T2 = 숫자들[3]; T3 = 숫자들[4]; }
            }
            else if (현재서브탭 == "F존")
            {
                타점명 = "B";
                현재가 = 숫자들[0];
                if (숫자들.Count >= 5) { T1 = 숫자들[3]; T2 = 숫자들[4]; }
            }
            else if (현재서브탭 == "SF존")
            {
                타점명 = "S";
                현재가 = 숫자들[1];
                if (숫자들.Count >= 5) { T1 = 숫자들[3]; T2 = 숫자들[4]; }
            }

            if (T1 == 0 && T2 == 0 && T3 == 0) return;

            string 결과 = "대기중";
            if (T3 > 0 && 현재가 <= T3) 결과 = $"{타점명}3 포착";
            else if (T2 > 0 && 현재가 <= T2) 결과 = $"{타점명}2 포착";
            else if (T1 > 0 && 현재가 <= T1) 결과 = $"{타점명}1 포착";

            string t3표시 = T3 > 0 ? $" | {타점명}3:{T3:N0}" : "";
            string t2표시 = T2 > 0 ? $" | {타점명}2:{T2:N0}" : "";

            string 라인 = $"[{현재서브탭}][{최종종목명}] 현재:{현재가:N0} | {타점명}1:{T1:N0}{t2표시}{t3표시} => {결과}";
            if (출력체크.Add(라인)) Form1.Console_print(라인);
        }

        // ====================================================
        // [5] 강제 클릭기
        // ====================================================
        private static bool 단순_버튼_클릭_실행(string 버튼이름)
        {
            try
            {
                if (상세추적로그_켜기) Form1.Console_print($"[클릭추적] '{버튼이름}' 탭 찾는 중...");
                var 티마창 = 티마_창_찾기();
                if (티마창 == null) return false;

                var 조건 = new AndCondition(
                    new PropertyCondition(AutomationElement.NameProperty, 버튼이름),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text)
                );

                var 대상 = 티마창.FindFirst(TreeScope.Descendants, 조건)
                        ?? 티마창.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, 버튼이름));

                if (대상 == null) return false;

                AutomationElement 버튼 = TreeWalker.RawViewWalker.GetParent(대상) ?? 대상;
                bool 클릭성공 = false;

                if (버튼.TryGetCurrentPattern(ScrollItemPattern.Pattern, out object scrollObj)) { try { ((ScrollItemPattern)scrollObj).ScrollIntoView(); } catch { } }
                if (버튼.TryGetCurrentPattern(InvokePattern.Pattern, out object invObj)) { ((InvokePattern)invObj).Invoke(); 클릭성공 = true; }
                else if (버튼.TryGetCurrentPattern(SelectionItemPattern.Pattern, out object selObj)) { try { ((SelectionItemPattern)selObj).Select(); 클릭성공 = true; } catch { } }
                if (버튼.Current.IsKeyboardFocusable) { 버튼.SetFocus(); 클릭성공 = true; }

                return 클릭성공;
            }
            catch { return false; }
        }

        // ====================================================
        // [6] 기타 유틸리티
        // ====================================================
        private static bool 진짜종목인지확인(string 이름)
        {
            if (string.IsNullOrWhiteSpace(이름) || 이름 == "NEW") return false;
            if (종목명사전 == null)
            {
                종목명사전 = new HashSet<string>();
                foreach (var item in Form1.Market_Item_List.Values) { 종목명사전.Add(item.종목명); }
            }
            return 종목명사전.Contains(이름);
        }

        private static int 가격변환(string txt)
        {
            if (txt.Contains("월") || txt.Contains("일") || txt.Contains(":") || txt.Contains(".") || txt.Contains("%") || txt.Contains("D") || txt.Contains("NEW")) return 0;
            string 숫자만 = txt.Replace(",", "").Replace("원", "").Replace("+", "").Replace("-", "").Trim();
            if (int.TryParse(숫자만, out int n)) return n;
            return 0;
        }

        private static AutomationElement 티마_창_찾기() { return AutomationElement.RootElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "티마")); }

        private static List<string> 서브탭_이름_목록_가져오기(string 시작탭)
        {
            List<string> 목록 = new List<string>();
            try
            {
                var 티마창 = 티마_창_찾기();
                var 시작 = 티마창.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, 시작탭));
                if (시작 != null)
                {
                    var 현재 = TreeWalker.RawViewWalker.GetParent(시작);
                    while (현재 != null)
                    {
                        var 글자 = TreeWalker.RawViewWalker.GetFirstChild(현재);
                        if (글자 != null)
                        {
                            string 이름 = 글자.Current.Name.Trim();
                            if (이름 == "+") 이름 = "F존 +";
                            목록.Add(이름);
                            if (이름 == "38스윙") break;
                        }
                        현재 = TreeWalker.RawViewWalker.GetNextSibling(현재);
                    }
                }
            }
            catch { }
            return 목록;
        }

        private static void 스마트_대기(int 대기시간_ms)
        {
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < 대기시간_ms)
            {
                System.Windows.Forms.Application.DoEvents();
                Thread.Sleep(5);
            }
            sw.Stop();
        }
    }
}