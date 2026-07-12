using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64
{
    public partial class Sub_form : Form
    {
        public static Sub_form form;

        // [+] 핵심: 'X' 버튼을 눌러서 끄는 것인지, 단순히 가로모드로 돌아가는 것인지 구분하는 스위치
        public bool 가로모드전환중 = false;

        public Sub_form()
        {
            form = this;
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer |
                       ControlStyles.UserPaint |
                       ControlStyles.AllPaintingInWmPaint |
                       ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
        }

        //public void 레이아웃_설정_불러오기()
        //{
        //    // 1. 저장되어 있는 레이아웃 값(예: "A", "B", "C" 등)을 가져옵니다.
        //    string 설정값 = GenieConfig.Sub_layout;

        //    if (설정값 == null) 설정값 = "A"; // 기본값 설정 (예: "A")

        //    // 2. 각 체크박스에 값이 일치하는지 확인하여 체크 상태를 결정합니다.
        //    CB_Layout_A.Checked = (CB_Layout_A.Text == 설정값);
        //    CB_Layout_B.Checked = (CB_Layout_B.Text == 설정값);
        //    CB_Layout_C.Checked = (CB_Layout_C.Text == 설정값);

        //    if (Form1.로딩완료)
        //    {
        //        CB_Layout_X.Checked = GenieConfig.Sub_layout_X;
        //    }
        //}

        private bool 이벤트_무시 = false;

        public void 레이아웃_설정_불러오기()
        {
            // [+] 폼이 로딩되면서 코드로 체크박스 상태를 바꿀 때, 이벤트가 실행되지 않도록 잠급니다.
            이벤트_무시 = true;

            try
            {
                // 1. 저장되어 있는 레이아웃 값(예: "A", "B", "C" 등)을 가져옵니다.
                string 설정값 = GenieConfig.Sub_layout;

                if (string.IsNullOrEmpty(설정값)) 설정값 = "A"; // 기본값 설정 (예: "A")

                // 2. 각 체크박스에 값이 일치하는지 확인하여 체크 상태를 결정합니다.
                CB_Layout_A.Checked = (CB_Layout_A.Text == 설정값);
                CB_Layout_B.Checked = (CB_Layout_B.Text == 설정값);
                CB_Layout_C.Checked = (CB_Layout_C.Text == 설정값);

                CB_Layout_X.Checked = GenieConfig.Sub_layout_X;
            }
            finally
            {
                // [+] 체크박스 세팅이 안전하게 모두 끝난 뒤에 다시 자물쇠를 풀어줍니다.
                이벤트_무시 = false;
            }
        }


        // --------------------------------------------------------------------------------
        // [Sub_form.cs] 가로/세로 보기 모드 전환 및 화면 상태 제어 (화면 이탈 방지 추가)
        // --------------------------------------------------------------------------------
        public static void 세로보기(bool 세로보기)
        {
            if (Form1.form1 == null) return;

            if (세로보기)
            {
                // 1. [통합] 서브폼이 없거나 닫혔다면 이곳에서 생성합니다.
                if (form == null || form.IsDisposed)
                {
                    form = new Sub_form();
                }

                if (GenieConfig.서브폼_X != -1 && GenieConfig.서브폼_Y != -1)
                {
                    form.StartPosition = FormStartPosition.Manual;

                    Point 복원좌표 = new Point(GenieConfig.서브폼_X, GenieConfig.서브폼_Y);
                    bool 화면내_존재여부 = false;

                    // 현재 PC에 연결된 모든 모니터의 실제 가시 영역을 전수조사합니다.
                    foreach (Screen 모니터 in Screen.AllScreens)
                    {
                        if (모니터.WorkingArea.Contains(복원좌표))
                        {
                            화면내_존재여부 = true;
                            break;
                        }
                    }

                    if (!화면내_존재여부)
                    {
                        복원좌표 = new Point(-1, -1);
                    }

                    form.Location = 복원좌표;
                }

                if (Form1.로딩완료)
                {
                    form.CB_Layout_X.Checked = GenieConfig.Sub_layout_X;
                }
                
                form.Opacity = 1.0;
                Form1.form1.Opacity = 0.0;
                form.가로모드전환중 = false;
                form.Show();

                form.메인폼_전체_컨트롤_가져오기();
                Form1.form1.Hide();

            }
            else
            {
                if (form != null && !form.IsDisposed)
                {
                    if (Sub_form.form.WindowState == FormWindowState.Normal)
                    {
                        GenieConfig.서브폼_X = Sub_form.form.Location.X;
                        GenieConfig.서브폼_Y = Sub_form.form.Location.Y;
                    }
                    
                    Form1.form1.Opacity = 1.0;
                    form.Opacity = 0.0;

                    form.가로모드전환중 = true;
                    form.메인폼_전체_컨트롤_돌려주기();
                    form.Close();
                }

                // 6. 메인폼 복귀 및 초점 제어
                Form1.form1.Show();
                Form1.form1.BringToFront();
                Form1.form1.Activate();
            }

            if (form != null && !form.IsDisposed)
            {
                form.ActiveControl = null;
            }
        }

        // --------------------------------------------------------------------------------
        // [Sub_form.cs] 레이아웃 설정(A, B, C)에 따른 컨트롤 일괄 가져오기 및 배치
        // --------------------------------------------------------------------------------
        public void 메인폼_전체_컨트롤_가져오기()
        {
            if (Form1.form1 == null || Form1.원본상태_사전 == null) return;

            Size 공통크기 = new Size(121, 25);
            Font 공통폰트 = new Font("맑은 고딕", 11, FontStyle.Bold);

            foreach (var 항목 in Form1.원본상태_사전)
            {
                Control 컨트롤 = 항목.Key;
                if (컨트롤 == null) continue;

                if (!this.Controls.Contains(컨트롤))
                {
                    this.Controls.Add(컨트롤);
                }

                bool 공통컨트롤여부 = true;
                bool Sub_layout_X = false;
                if (Form1.로딩완료) Sub_layout_X = GenieConfig.Sub_layout_X;

                // 공통 상단 UI 배치
                switch (컨트롤.Name)
                {
                    case "CB_세로보기": 컨트롤.Location = new Point(-1, 0); break;
                    case "label_ONLINE": 컨트롤.Location = new Point(143, 0); 컨트롤.Size = new Size(177, 21); 컨트롤.BringToFront(); break;
                    case "label_투자원금": 컨트롤.Location = new Point(319, 0); 컨트롤.Size = new Size(65, 21); 컨트롤.SendToBack(); if (Sub_layout_X) 컨트롤.Hide(); else 컨트롤.Show(); break;
                    case "MT_투자원금": 컨트롤.Location = new Point(382, 0); 컨트롤.BringToFront(); if (Sub_layout_X) 컨트롤.Hide(); else 컨트롤.Show(); break;
                    case "panel_잔고": 컨트롤.Location = new Point(491, 0); if (Sub_layout_X) 컨트롤.Hide(); else 컨트롤.Show(); break;

                    case "label_추정증가자산": 컨트롤.Location = new Point(-1, 20); 컨트롤.Font = 공통폰트; 컨트롤.Size = 공통크기; if (Sub_layout_X) 컨트롤.Hide(); else 컨트롤.Show(); break;
                    case "TB_추정자산": 컨트롤.Location = new Point(-1, 44); 컨트롤.Font = 공통폰트; 컨트롤.Size = 공통크기; if (Sub_layout_X) 컨트롤.Hide(); else 컨트롤.Show(); break;
                    case "TB_증가자산": 컨트롤.Location = new Point(-1, 70); 컨트롤.Font = 공통폰트; 컨트롤.Size = 공통크기; if (Sub_layout_X) 컨트롤.Hide(); else 컨트롤.Show(); break;

                    case "label_매입평가금": 컨트롤.Location = new Point(239, 20); 컨트롤.Font = 공통폰트; 컨트롤.Size = 공통크기; if (Sub_layout_X) 컨트롤.Hide(); else 컨트롤.Show(); break;
                    case "TB_매입금": 컨트롤.Location = new Point(239, 44); 컨트롤.Font = 공통폰트; 컨트롤.Size = 공통크기; if (Sub_layout_X) 컨트롤.Hide(); else 컨트롤.Show(); break;
                    case "TB_평가금": 컨트롤.Location = new Point(239, 70); 컨트롤.Font = 공통폰트; 컨트롤.Size = 공통크기; if (Sub_layout_X) 컨트롤.Hide(); else 컨트롤.Show(); break;

                    case "label_평가손익": 컨트롤.Location = new Point(359, 20); 컨트롤.Font = 공통폰트; 컨트롤.Size = 공통크기; if (Sub_layout_X) 컨트롤.Hide(); else 컨트롤.Show(); break;
                    case "TB_평가손익금": 컨트롤.Location = new Point(359, 44); 컨트롤.Font = 공통폰트; 컨트롤.Size = 공통크기; if (Sub_layout_X) 컨트롤.Hide(); else 컨트롤.Show(); break;
                    case "TB_평가손익율": 컨트롤.Location = new Point(359, 70); 컨트롤.Font = 공통폰트; 컨트롤.Size = 공통크기; if (Sub_layout_X) 컨트롤.Hide(); else 컨트롤.Show(); break;

                    case "label_D2주문가능": 컨트롤.Location = new Point(119, 20); 컨트롤.Font = 공통폰트; 컨트롤.Size = new Size(122, 25); break;
                    case "TB_D2": 컨트롤.Location = new Point(119, 44); 컨트롤.Font = 공통폰트; 컨트롤.Size = new Size(122, 25); break;
                    case "TB_추정D2": 컨트롤.Location = new Point(119, 70); 컨트롤.Font = 공통폰트; 컨트롤.Size = new Size(122, 25); break;

                    case "label_실현손익": 컨트롤.Location = new Point(479, 20); 컨트롤.Font = 공통폰트; 컨트롤.Size = new Size(122, 25); break;
                    case "TB_실현손익": 컨트롤.Location = new Point(479, 44); 컨트롤.Font = 공통폰트; 컨트롤.Size = new Size(122, 25); break;
                    case "TB_실현손익율": 컨트롤.Location = new Point(479, 70); 컨트롤.Font = 공통폰트; 컨트롤.Size = new Size(122, 25); break;

                    default:
                        공통컨트롤여부 = false;
                        break;
                }

                // Sub_layout_X 특수 확장 배치 레이아웃
                if (Sub_layout_X)
                {
                    switch (컨트롤.Name)
                    {
                        case "label_ONLINE": 컨트롤.Location = new Point(143, 0); 컨트롤.Size = new Size(174, 21); 컨트롤.BringToFront(); break;

                        case "label_D2주문가능":
                            컨트롤.Location = new Point(-1, 20); 컨트롤.Font = 공통폰트; 컨트롤.Size = new Size(318, 25);
                            if (컨트롤 is Label 라벨) 라벨.TextAlign = ContentAlignment.MiddleCenter;
                            break;
                        case "TB_D2":
                            컨트롤.Location = new Point(-1, 44); 컨트롤.Font = 공통폰트; 컨트롤.Size = new Size(318, 25);
                            if (컨트롤 is TextBox 텍스트박) 텍스트박.TextAlign = HorizontalAlignment.Center;
                            break;
                        case "TB_추정D2":
                            컨트롤.Location = new Point(-1, 70); 컨트롤.Font = 공통폰트; 컨트롤.Size = new Size(318, 25);
                            if (컨트롤 is TextBox 텍스트박_) 텍스트박_.TextAlign = HorizontalAlignment.Center;
                            break;

                        case "label_실현손익":
                            컨트롤.Location = new Point(316, 20); 컨트롤.Font = 공통폰트; 컨트롤.Size = new Size(285, 25);
                            if (컨트롤 is Label 라벨_) 라벨_.TextAlign = ContentAlignment.MiddleCenter;
                            break;
                        case "TB_실현손익":
                            컨트롤.Location = new Point(316, 44); 컨트롤.Font = 공통폰트; 컨트롤.Size = new Size(285, 25);
                            if (컨트롤 is TextBox 텍스트박_a) 텍스트박_a.TextAlign = HorizontalAlignment.Center;
                            break;
                        case "TB_실현손익율":
                            컨트롤.Location = new Point(316, 70); 컨트롤.Font = 공통폰트; 컨트롤.Size = new Size(285, 25);
                            if (컨트롤 is TextBox 텍스트박_b) 텍스트박_b.TextAlign = HorizontalAlignment.Center;
                            break;
                    }
                }

                // 개별 레이아웃 분기 처리 (A, B, C, 통계)
                if (!공통컨트롤여부)
                {
                    string 현재레이아웃 = "A";
                    if (Form1.로딩완료) 현재레이아웃 = GenieConfig.Sub_layout;

                    if (현재레이아웃 == "A")
                    {
                        switch (컨트롤.Name)
                        {
                            case "JanGo_dataGridView":
                                컨트롤.Location = new Point(-1, 96);
                                컨트롤.Size = new Size(603, 730);
                                컨트롤.Show();
                                컨트롤.SendToBack();

                                DataGridView 잔고그리드 = 컨트롤 as DataGridView;
                                if (잔고그리드 != null)
                                {
                                    foreach (DataGridViewColumn 열 in 잔고그리드.Columns) { 열.Frozen = false; }
                                    잔고그리드.CurrentCell = null;
                                    잔고그리드.ClearSelection();

                                    if (잔고그리드.Columns.Contains("평균단가_잔고") && 잔고그리드.Columns.Contains("주문가능수량_잔고"))
                                    {
                                        잔고그리드.Columns["평균단가_잔고"].DisplayIndex = 17;
                                        잔고그리드.Columns["주문가능수량_잔고"].DisplayIndex = 9;
                                        잔고그리드.Columns["수익률_잔고"].DisplayIndex = 10;
                                    }
                                }
                                break;

                            case "LB_Log":
                                컨트롤.Location = new Point(-1, 823);
                                컨트롤.Size = new Size(603, 170);
                                컨트롤.Show();
                                컨트롤.BringToFront();
                                break;

                            case "JuMun_dataGridView":
                            case "Outstanding_DataGridView":
                            case "Conclusion_DataGridView":
                            case "DGV_통계":
                            case "DGV_통계B":
                                컨트롤.Hide();
                                break;
                        }
                    }
                    else if (현재레이아웃 == "B")
                    {
                        switch (컨트롤.Name)
                        {
                            case "LB_Log":
                            case "JuMun_dataGridView":
                            case "DGV_통계":
                            case "DGV_통계B":
                                컨트롤.Hide();
                                break;

                            case "JanGo_dataGridView":
                                컨트롤.Location = new Point(-1, 96);
                                컨트롤.Size = new Size(600, 450);
                                컨트롤.Show();
                                컨트롤.BringToFront();

                                DataGridView 잔고그리드 = 컨트롤 as DataGridView;
                                if (잔고그리드 != null)
                                {
                                    foreach (DataGridViewColumn 열 in 잔고그리드.Columns) { 열.Frozen = false; }
                                    잔고그리드.CurrentCell = null;
                                    잔고그리드.ClearSelection();

                                    if (잔고그리드.Columns.Contains("평균단가_잔고") && 잔고그리드.Columns.Contains("주문가능수량_잔고"))
                                    {
                                        잔고그리드.Columns["평균단가_잔고"].DisplayIndex = 17;
                                        잔고그리드.Columns["주문가능수량_잔고"].DisplayIndex = 9;
                                        잔고그리드.Columns["수익률_잔고"].DisplayIndex = 10;
                                    }
                                }
                                break;

                            case "Outstanding_DataGridView":
                                컨트롤.Location = new Point(-1, 543);
                                컨트롤.Size = new Size(600, 226);
                                컨트롤.Show();
                                컨트롤.BringToFront();

                                DataGridView Outstanding_DataGridView = 컨트롤 as DataGridView;
                                if (Outstanding_DataGridView != null)
                                {
                                    Outstanding_DataGridView.CurrentCell = null;
                                    Outstanding_DataGridView.ClearSelection();
                                }
                                break;

                            case "Conclusion_DataGridView":
                                컨트롤.Location = new Point(-1, 766);
                                컨트롤.Size = new Size(600, 227);
                                컨트롤.Show();
                                컨트롤.BringToFront();

                                DataGridView Conclusion_DataGridView = 컨트롤 as DataGridView;
                                if (Conclusion_DataGridView != null)
                                {
                                    Conclusion_DataGridView.CurrentCell = null;
                                    Conclusion_DataGridView.ClearSelection();
                                }
                                break;
                        }
                    }
                    else if (현재레이아웃 == "C")
                    {
                        switch (컨트롤.Name)
                        {
                            case "LB_Log":
                            case "JanGo_dataGridView":
                            case "DGV_통계":
                            case "DGV_통계B":
                                컨트롤.Hide();
                                break;

                            case "JuMun_dataGridView":
                                컨트롤.Location = new Point(-1, 96);
                                컨트롤.Size = new Size(600, 450);
                                컨트롤.Show();
                                컨트롤.SendToBack();

                                DataGridView JuMun_dataGridView = 컨트롤 as DataGridView;
                                if (JuMun_dataGridView != null)
                                {
                                    JuMun_dataGridView.CurrentCell = null;
                                    JuMun_dataGridView.ClearSelection();
                                }
                                break;

                            case "Outstanding_DataGridView":
                                컨트롤.Location = new Point(-1, 543);
                                컨트롤.Size = new Size(600, 226);
                                컨트롤.Show();
                                컨트롤.BringToFront();

                                DataGridView Outstanding_DataGridView = 컨트롤 as DataGridView;
                                if (Outstanding_DataGridView != null)
                                {
                                    Outstanding_DataGridView.CurrentCell = null;
                                    Outstanding_DataGridView.ClearSelection();
                                }
                                break;

                            case "Conclusion_DataGridView":
                                컨트롤.Location = new Point(-1, 766);
                                컨트롤.Size = new Size(600, 227);
                                컨트롤.Show();
                                컨트롤.BringToFront();

                                DataGridView Conclusion_DataGridView = 컨트롤 as DataGridView;
                                if (Conclusion_DataGridView != null)
                                {
                                    Conclusion_DataGridView.CurrentCell = null;
                                    Conclusion_DataGridView.ClearSelection();
                                }
                                break;
                        }
                    }
                    else if (현재레이아웃 == "통계")
                    {
                        switch (컨트롤.Name)
                        {
                            case "LB_Log":
                            case "JanGo_dataGridView":
                            case "JuMun_dataGridView":
                            case "Outstanding_DataGridView":
                            case "Conclusion_DataGridView":
                                컨트롤.Hide();
                                break;

                            case "DGV_통계":
                                DataGridView dgv통계 = 컨트롤 as DataGridView;
                                if (dgv통계 != null)
                                {
                                    dgv통계.CurrentCell = null;
                                    dgv통계.ClearSelection();

                                    if (dgv통계.Columns.Contains("총매수금액_통계") && dgv통계.Columns.Contains("총매도금액_통계") &&
                                        dgv통계.Columns.Contains("총매매수수료_통계") && dgv통계.Columns.Contains("총매매세금_통계") &&
                                        dgv통계.Columns.Contains("총실현손익_통계") && dgv통계.Columns.Contains("실현손익율_통계"))
                                    {
                                        dgv통계.Columns["총매수금액_통계"].DisplayIndex = 0;
                                        dgv통계.Columns["총매도금액_통계"].DisplayIndex = 1;
                                        dgv통계.Columns["총매매수수료_통계"].DisplayIndex = 2;
                                        dgv통계.Columns["총매매세금_통계"].DisplayIndex = 3;
                                        dgv통계.Columns["총실현손익_통계"].DisplayIndex = 4;
                                        dgv통계.Columns["실현손익율_통계"].DisplayIndex = 5;

                                        dgv통계.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                                        dgv통계.Columns["총매수금액_통계"].Width = 120;
                                        dgv통계.Columns["총매도금액_통계"].Width = 120;
                                        dgv통계.Columns["총매매수수료_통계"].Width = 120;
                                        dgv통계.Columns["총매매세금_통계"].Width = 120;
                                        dgv통계.Columns["총실현손익_통계"].Width = 120;
                                        dgv통계.Columns["실현손익율_통계"].Width = 80;
                                    }

                                    dgv통계.CurrentCell = null;
                                    dgv통계.ClearSelection();
                                }

                                // [+] 1단계: 컨트롤의 내부 레이아웃 연산을 강제로 멈춥니다.
                                컨트롤.SuspendLayout();

                                // [+] 2단계: 크기와 위치를 주입합니다.
                                컨트롤.Location = new Point(-1, 96);
                                컨트롤.Size = new Size(600, 55);

                                // [+] 3단계: 연산을 재개하면서 부모 폼에게 즉시 레이아웃을 다시 계산하라고 멱살을 잡습니다. (강제 갱신)
                                컨트롤.ResumeLayout(true);
                                컨트롤.Show();
                                컨트롤.BringToFront();
                                컨트롤.Refresh(); // 💡 [핵심] 윈도우 엔진에게 미루지 말고 지금 당장 화면에 그리라고 명령합니다.
                                break;

                            case "DGV_통계B":
                                DataGridView dgv통계B = 컨트롤 as DataGridView;
                                if (dgv통계B != null)
                                {
                                    dgv통계B.CurrentCell = null;
                                    dgv통계B.ClearSelection();

                                    if (dgv통계B.Columns.Contains("일자_통계B") && dgv통계B.Columns.Contains("매수금액_통계B") &&
                                        dgv통계B.Columns.Contains("매도금액_통계B") && dgv통계B.Columns.Contains("실현손익_통계B") &&
                                        dgv통계B.Columns.Contains("실현손익율_통계B") && dgv통계B.Columns.Contains("수익n손실_통계B"))
                                    {
                                        dgv통계B.Columns["일자_통계B"].DisplayIndex = 0;
                                        dgv통계B.Columns["매수금액_통계B"].DisplayIndex = 1;
                                        dgv통계B.Columns["매도금액_통계B"].DisplayIndex = 2;
                                        dgv통계B.Columns["실현손익_통계B"].DisplayIndex = 3;
                                        dgv통계B.Columns["실현손익율_통계B"].DisplayIndex = 4;
                                        dgv통계B.Columns["수익n손실_통계B"].DisplayIndex = 5;

                                        dgv통계B.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                                        dgv통계B.Columns["수익n손실_통계B"].Width = 89;
                                    }
                                }

                                // [+] 변경 전 크기 저장
                                int 변경전_가로 = 컨트롤.Width;
                                int 변경전_세로 = 컨트롤.Height;
                                Form1.Console_print($"\n[DGV_통계B] 레이아웃 주입 전 크기 -> 가로: {변경전_가로}, 세로: {변경전_세로}");

                                // [+] 1단계: 연산 일시 정지
                                컨트롤.SuspendLayout();

                                // [+] 2단계: 크기 주입
                                컨트롤.Location = new Point(-1, 135);
                                컨트롤.Size = new Size(600, 880);

                                // [+] 3단계: 연산 재개 및 즉시 다시 그리기(Refresh)
                                컨트롤.ResumeLayout(true);
                                컨트롤.Show();
                                컨트롤.BringToFront();
                                컨트롤.Refresh(); // 엔진 강제 갱신

                                // [+] 변경 후 실제 적용된 크기 가져오기
                                int 변경후_가로 = 컨트롤.Width;
                                int 변경후_세로 = 컨트롤.Height;
                                Form1.Console_print($"[DGV_통계B] 레이아웃 주입 후 크기 -> 가로: {변경후_가로}, 세로: {변경후_세로}");

                                if (컨트롤 is DataGridView dgvB)
                                {
                                    dgvB.CurrentCell = null;
                                    dgvB.ClearSelection();
                                }

                                //if(변경전_세로 == 변경후_세로)
                                //{
                                //    // [+] 4단계: 메세지박스 출력 (디버깅용)
                                //    MessageBox.Show(
                                //        "[DGV_통계B] 크기 변경 검증 완료\n\n" +
                                //        "=== 변경 전 ===\n" +
                                //        "가로: " + 변경전_가로 + "\n" +
                                //        "세로: " + 변경전_세로 + "\n\n" +
                                //        "=== 변경 후 ===\n" +
                                //        "가로: " + 변경후_가로 + "\n" +
                                //        "세로: " + 변경후_세로,
                                //        "크기 변경 확인",
                                //        MessageBoxButtons.OK,
                                //        MessageBoxIcon.Information);
                                //}

                                break;
                        }
                    }
                }
            }
        }

        // --------------------------------------------------------------------------------
        // [Sub_form.cs] 명부에 적힌 모든 컨트롤을 원래 패널, 원래 크기, 원래 좌표, 원래 셀 와이드로 복구
        // --------------------------------------------------------------------------------
        public void 메인폼_전체_컨트롤_돌려주기()
        {
            if (Form1.form1 == null || Form1.원본상태_사전 == null) return;

            foreach (var 항목 in Form1.원본상태_사전)
            {
                Control 컨트롤 = 항목.Key;
                Form1.컨트롤_원본상태 상태 = 항목.Value;

                if (컨트롤 != null && 상태.부모 != null)
                {
                    컨트롤.Font = 상태.원본폰트;
                    컨트롤.Size = 상태.원본크기;
                    컨트롤.Location = 상태.좌표;

                    if (컨트롤 is System.Windows.Forms.Label 라벨)
                    {
                        라벨.TextAlign = 상태.라벨정렬;
                    }
                    else if (컨트롤 is TextBox 텍스트박스)
                    {
                        텍스트박스.TextAlign = 상태.텍스트박스정렬;
                    }

                    if (컨트롤 is DataGridView dgv)
                    {
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    }

                    if (!상태.부모.Controls.Contains(컨트롤))
                    {
                        상태.부모.Controls.Add(컨트롤);
                    }
                }
            }

            if (Form1.form1.JanGo_dataGridView != null)
            {
                foreach (DataGridViewColumn 열 in Form1.form1.JanGo_dataGridView.Columns)
                {
                    열.Frozen = false;
                }

                if (Form1.원본상태_사전.ContainsKey(Form1.form1.JanGo_dataGridView))
                {
                    Form1.form1.JanGo_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    Form1.form1.JanGo_dataGridView.Size = Form1.원본상태_사전[Form1.form1.JanGo_dataGridView].원본크기;
                }

                if (Form1.잔고그리드_원본열순서 != null)
                {
                    foreach (DataGridViewColumn 열 in Form1.form1.JanGo_dataGridView.Columns)
                    {
                        if (Form1.잔고그리드_원본열순서.ContainsKey(열.Name))
                        {
                            열.DisplayIndex = Form1.잔고그리드_원본열순서[열.Name];
                        }
                    }
                }

                if (Form1.form1.JanGo_dataGridView.Columns.Contains("수익률_잔고"))
                {
                    Form1.form1.JanGo_dataGridView.Columns["수익률_잔고"].Frozen = true;
                }

                Form1.form1.JanGo_dataGridView.CurrentCell = null;
                Form1.form1.JanGo_dataGridView.ClearSelection();
            }

            if (Form1.form1.DGV_통계 != null)
            {
                if (Form1.통계그리드_원본열너비 != null)
                {
                    foreach (DataGridViewColumn 열 in Form1.form1.DGV_통계.Columns)
                    {
                        if (Form1.통계그리드_원본열너비.ContainsKey(열.Name))
                        {
                            열.Width = Form1.통계그리드_원본열너비[열.Name];
                        }
                    }
                }

                if (Form1.통계그리드_원본열순서 != null)
                {
                    foreach (DataGridViewColumn 열 in Form1.form1.DGV_통계.Columns)
                    {
                        if (Form1.통계그리드_원본열순서.ContainsKey(열.Name))
                        {
                            열.DisplayIndex = Form1.통계그리드_원본열순서[열.Name];
                        }
                    }
                }

                Form1.form1.DGV_통계.CurrentCell = null;
                Form1.form1.DGV_통계.ClearSelection();
                Form1.form1.DGV_통계.Refresh();
            }

            if (Form1.form1.DGV_통계B != null)
            {
                if (Form1.통계B그리드_원본열너비 != null)
                {
                    foreach (DataGridViewColumn 열 in Form1.form1.DGV_통계B.Columns)
                    {
                        if (Form1.통계B그리드_원본열너비.ContainsKey(열.Name))
                        {
                            열.Width = Form1.통계B그리드_원본열너비[열.Name];
                        }
                    }
                }

                if (Form1.통계B그리드_원본열순서 != null)
                {
                    foreach (DataGridViewColumn 열 in Form1.form1.DGV_통계B.Columns)
                    {
                        if (Form1.통계B그리드_원본열순서.ContainsKey(열.Name))
                        {
                            열.DisplayIndex = Form1.통계B그리드_원본열순서[열.Name];
                        }
                    }
                }

                Form1.form1.DGV_통계B.CurrentCell = null;
                Form1.form1.DGV_통계B.ClearSelection();
                Form1.form1.DGV_통계B.Refresh();
            }

            foreach (var 항목 in Form1.원본상태_사전)
            {
                Control 컨트롤 = 항목.Key;
                if (컨트롤 != null)
                {
                    컨트롤.Show();
                }
            }

            Form1.form1.BringToFront();
            Form1.form1.Activate();
        }

        // --------------------------------------------------------------------------------
        // [Sub_form.cs] 닫기 직전(FormClosing) 좌표 추적 로그 보완
        // --------------------------------------------------------------------------------
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            GenieConfig.Sub_layout_X = CB_Layout_X.Checked;

            if (this.WindowState == FormWindowState.Normal)
            {
                GenieConfig.서브폼_X = this.Location.X;
                GenieConfig.서브폼_Y = this.Location.Y;
            }

            if (가로모드전환중)
            {
                return;
            }

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                if (Form1.form1 != null && !Form1.form1.IsDisposed)
                {
                    Form1.form1.Close();
                }
            }
        }

      

        // --------------------------------------------------------------------------------
        // [Sub_form.cs] 확장 레이아웃 X 체크박스 변경
        // --------------------------------------------------------------------------------
        private void CB_Layout_X_CheckedChanged(object sender, EventArgs e)
        {
            // 💡 [버그 수정] 이곳에 있던 this.Location 좌표 수집 및 GenieConfig 덮어쓰기 로직을 완전히 삭제했습니다.

            if (Form1.로딩완료)
            {
                GenieConfig.Sub_layout_X = CB_Layout_X.Checked;
                메인폼_전체_컨트롤_돌려주기();
                메인폼_전체_컨트롤_가져오기();
            }
        }

        private string 통계진입전_기존레이아웃 = "A";

        private void CB_통계_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox 통계체크 = sender as CheckBox;
            if (통계체크 == null) return;

            if (통계체크.Checked)
            {
                Form1.form1.CB_세로보기.Enabled = false;

                if (Form1.ON_LINE)
                {
                    Statistical_chart.매매내역확인();
                }

                if (this.WindowState == FormWindowState.Normal)
                {
                    GenieConfig.서브폼_X = this.Location.X;
                    GenieConfig.서브폼_Y = this.Location.Y;
                }

                통계진입전_기존레이아웃 = GenieConfig.Sub_layout;
                GenieConfig.Sub_layout = "통계";

                CB_Layout_A.Checked = false;
                CB_Layout_B.Checked = false;
                CB_Layout_C.Checked = false;

                Sub_form.세로보기(true);

                this.ActiveControl = null;
              
            }
            else
            {
                Form1.form1.CB_세로보기.Enabled = true;

                GenieConfig.Sub_layout = 통계진입전_기존레이아웃;

                if (GenieConfig.Sub_layout == "A") CB_Layout_A.Checked = true;
                else if (GenieConfig.Sub_layout == "B") CB_Layout_B.Checked = true;
                else if (GenieConfig.Sub_layout == "C") CB_Layout_C.Checked = true;
            }
        }



        // --------------------------------------------------------------------------------
        // [Sub_form.cs] 3개의 체크박스 중 하나만 선택 및 변경 시 서브폼 레이아웃 실시간 갱신
        // --------------------------------------------------------------------------------
        private void CB_Layout_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox clickedCheckBox = sender as CheckBox;
            if (clickedCheckBox == null) return;

            if (clickedCheckBox.Checked)
            {
                if (Sub_form.form != null && !Sub_form.form.IsDisposed)
                {
                    Sub_form.form.메인폼_전체_컨트롤_돌려주기();
                }

                CheckBox[] allCheckBoxes = { CB_Layout_A, CB_Layout_B, CB_Layout_C };
                foreach (CheckBox cb in allCheckBoxes)
                {
                    if (cb != clickedCheckBox) cb.Checked = false;
                }

                if (Form1.로딩완료) GenieConfig.Sub_layout = clickedCheckBox.Text;

                if (Sub_form.form != null && !Sub_form.form.IsDisposed)
                {
                    if (CB_통계.Checked) CB_통계.Checked = false;
                    Sub_form.form.메인폼_전체_컨트롤_가져오기();
                }
            }
            else
            {
                bool anyChecked = (CB_Layout_A.Checked || CB_Layout_B.Checked || CB_Layout_C.Checked || CB_통계.Checked);

                if (!anyChecked)
                {
                    CB_Layout_A.Checked = true;
                    GenieConfig.Sub_layout = CB_Layout_A.Text;
                }
            }
        }

        private void Sub_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            CB_통계.Checked = false;
        }
    }
}