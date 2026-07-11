using System;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 지니64
{
    class AutoClosingMessageBox
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)] // win32 api
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)] // 마샬링 방법 지정 이름관리 제어
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        System.Threading.Timer _timeoutTimer; //쓰레드 타이머
        string _caption = string.Empty;

        const int WM_CLOSE = 0x0010; //close 명령 


        AutoClosingMessageBox(string text, string title, int timeout)
        {
            this._caption = title;

            // 타이머 시작 (지정된 시간 후 닫기 시도)
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed, null, timeout, System.Threading.Timeout.Infinite);

            // [지니 최적화] 위치 계산을 위해 가짜 폼(Mbox)을 만들 필요가 없습니다.
            // 이미 구현하신 'MessageBoxCenter'는 넘겨받은 주인(Owner) 폼의 중앙을 자동으로 계산합니다.
            // 따라서 'Form1.form1'을 직접 넘겨주기만 하면 됩니다.

            // UI 스레드에서 안전하게 실행 (Hook킹 및 모달 창 처리를 위해 필수)
            Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                try
                {
                    // Mbox 대신 Form1.form1을 주인으로 전달 -> 폼 중앙에 뜸
                    MessageBoxCenter.Show(Form1.form1, text, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch
                {
                    // 에러 무시
                }
            });
        }

        private const string POPUP_NAME = "Genie_AutoClose_Popup";

        public static void Show(string text, string title, int timeout)
        {
            Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                CreateAndShowForm(text, title, timeout);
            });
        }
        private static void CreateAndShowForm(string text, string title, int timeout)
        {
            // 1. 기본 폼 설정
            Form popup = new Form();
            popup.Name = POPUP_NAME;
            popup.Text = title;
            popup.FormBorderStyle = FormBorderStyle.FixedDialog;
            popup.MaximizeBox = false;
            popup.MinimizeBox = false;
            popup.BackColor = Color.White;
            popup.ShowIcon = false;
            popup.TopMost = true;
            popup.StartPosition = FormStartPosition.Manual;

            int clientWidth = 400;
            popup.ClientSize = new Size(clientWidth, 150);

            // 2. 아이콘
            PictureBox iconBox = new PictureBox();
            iconBox.Image = SystemIcons.Warning.ToBitmap();
            iconBox.Size = new Size(32, 32);
            iconBox.Location = new Point(20, 20);
            iconBox.SizeMode = PictureBoxSizeMode.StretchImage;
            popup.Controls.Add(iconBox);

            // 3. 라벨
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(70, 20);
            lbl.MaximumSize = new Size(clientWidth - 90, 0);
            lbl.AutoSize = true;
            lbl.Font = new Font("맑은 고딕", 9, FontStyle.Regular);
            popup.Controls.Add(lbl);

            // 4. 버튼
            Button btn = new Button();
            btn.Text = "확인";
            btn.Size = new Size(80, 28);
            btn.UseVisualStyleBackColor = true;
            btn.Click += (s, e) => { popup.Close(); };
            popup.Controls.Add(btn);

            // [높이 자동 계산]
            int requiredHeight = 20 + lbl.Height + 20 + btn.Height + 20;
            int finalHeight = Math.Max(requiredHeight, 130);
            popup.ClientSize = new Size(clientWidth, finalHeight);
            btn.Location = new Point((clientWidth - btn.Width) / 2, finalHeight - btn.Height - 20);

            // -----------------------------------------------------------
            // 5. [핵심 수정] 위치 계산 (대각선 계단식 정렬)
            // -----------------------------------------------------------

            // (1) 현재 떠 있는 팝업 개수 확인
            int currentCount = Application.OpenForms.OfType<Form>()
                                          .Count(f => f.Name == POPUP_NAME);

            // (2) 메인 폼 정중앙 좌표 계산
            int startX = Form1.form1.Location.X + (Form1.form1.Width - popup.Width) / 2;
            int startY = Form1.form1.Location.Y + (Form1.form1.Height - popup.Height) / 2;

            // (3) 요청하신 옵셋 적용 (개수 * 25px)
            int step = 25;
            int offsetX = currentCount * step;
            int offsetY = currentCount * step;

            // (4) 화면 밖으로 나가는지 체크 (모니터 해상도 기준)
            // 너무 많이 떠서 화면 밖으로 나가면 다시 중앙(0,0)부터 시작
            Rectangle screenRect = Screen.FromControl(Form1.form1).WorkingArea;

            if (startX + offsetX + popup.Width > screenRect.Right ||
                startY + offsetY + popup.Height > screenRect.Bottom)
            {
                offsetX = 0;
                offsetY = 0;
            }

            // (5) 최종 위치 적용
            popup.Location = new Point(startX + offsetX, startY + offsetY);

            // -----------------------------------------------------------

            // 6. 타이머 및 실행
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = timeout;
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                timer.Dispose();
                if (!popup.IsDisposed) popup.Close();
            };
            timer.Start();

            SystemSounds.Exclamation.Play();
            popup.Show(Form1.form1);
        }

        //    시간이 다되면 close 메세지를 보냄
        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow(null, _caption);
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }
    }

    class MessageBoxCenter
    {
        private static IWin32Window _owner;
        private static HookProc _hookProc;
        private static IntPtr _hHook;

        public static DialogResult Show(string text)
        {
            Initialize();
            return MessageBox.Show(text);
        }

        public static DialogResult Show(string text, string caption)
        {
            Initialize();
            return MessageBox.Show(text, caption);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            Initialize();
            return MessageBox.Show(text, caption, buttons);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            Initialize();
            return MessageBox.Show(text, caption, buttons, icon);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton)
        {
            Initialize();
            return MessageBox.Show(text, caption, buttons, icon, defButton);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton, MessageBoxOptions options)
        {
            Initialize();
            return MessageBox.Show(text, caption, buttons, icon, defButton, options);
        }

        public static DialogResult Show(IWin32Window owner, string text)
        {
            _owner = owner;
            Initialize();
            return MessageBox.Show(owner, text);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption)
        {
            _owner = owner;
            Initialize();
            return MessageBox.Show(owner, text, caption);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            _owner = owner;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            _owner = owner;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton)
        {
            _owner = owner;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon, defButton);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton, MessageBoxOptions options)
        {
            _owner = owner;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon, defButton, options);
        }

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public delegate void TimerProc(IntPtr hWnd, uint uMsg, UIntPtr nIDEvent, uint dwTime);

        public const int WH_CALLWNDPROCRET = 12;

        public enum CbtHookAction : int
        {
            HCBT_MOVESIZE = 0,
            HCBT_MINMAX = 1,
            HCBT_QS = 2,
            HCBT_CREATEWND = 3,
            HCBT_DESTROYWND = 4,
            HCBT_ACTIVATE = 5,
            HCBT_CLICKSKIPPED = 6,
            HCBT_KEYSKIPPED = 7,
            HCBT_SYSCOMMAND = 8,
            HCBT_SETFOCUS = 9
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);

        [DllImport("user32.dll")]
        private static extern int MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("User32.dll")]
        public static extern UIntPtr SetTimer(IntPtr hWnd, UIntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);

        [DllImport("User32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll")]
        public static extern int UnhookWindowsHookEx(IntPtr idHook);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength);

        [DllImport("user32.dll")]
        public static extern int EndDialog(IntPtr hDlg, IntPtr nResult);

        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();

        [StructLayout(LayoutKind.Sequential)]
        public struct CWPRETSTRUCT
        {
            public IntPtr lResult;
            public IntPtr lParam;
            public IntPtr wParam;
            public uint message;
            public IntPtr hwnd;
        };

        static MessageBoxCenter()
        {
            _hookProc = new HookProc(MessageBoxHookProc);
            _hHook = IntPtr.Zero;
        }

        private static void Initialize()
        {
            if (_hHook != IntPtr.Zero)
            {
                throw new NotSupportedException("multiple calls are not supported");
            }

            if (_owner != null)
            {
#pragma warning disable CS0618 // 형식 또는 멤버는 사용되지 않습니다.
                _hHook = SetWindowsHookEx(WH_CALLWNDPROCRET, _hookProc, IntPtr.Zero, AppDomain.GetCurrentThreadId());
#pragma warning restore CS0618 // 형식 또는 멤버는 사용되지 않습니다.
            }
        }

        private static IntPtr MessageBoxHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CallNextHookEx(_hHook, nCode, wParam, lParam);
            }

            CWPRETSTRUCT msg = (CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT));
            IntPtr hook = _hHook;

            if (msg.message == (int)CbtHookAction.HCBT_ACTIVATE)
            {
                try
                {
                    CenterWindow(msg.hwnd);
                }
                finally
                {
                    UnhookWindowsHookEx(_hHook);
                    _hHook = IntPtr.Zero;
                }
            }

            return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        private static void CenterWindow(IntPtr hChildWnd)
        {
            Rectangle recChild = new Rectangle(0, 0, 0, 0);
            bool success = GetWindowRect(hChildWnd, ref recChild);

            int width = recChild.Width - recChild.X;
            int height = recChild.Height - recChild.Y;

            Rectangle recParent = new Rectangle(0, 0, 0, 0);
            success = GetWindowRect(_owner.Handle, ref recParent);

            Point ptCenter = new Point(0, 0);
            ptCenter.X = recParent.X + ((recParent.Width - recParent.X) / 2);
            ptCenter.Y = recParent.Y + ((recParent.Height - recParent.Y) / 2);


            Point ptStart = new Point(0, 0);
            ptStart.X = (ptCenter.X - (width / 2));
            ptStart.Y = (ptCenter.Y - (height / 2));

            // ptStart.X = (ptStart.X < 0) ? 0 : ptStart.X;
            ptStart.Y = (ptStart.Y < 0) ? 0 : ptStart.Y;

            int result = MoveWindow(hChildWnd, ptStart.X, ptStart.Y, width,
                                    height, false);
        }

    }
}

