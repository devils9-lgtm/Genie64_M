using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace 지니_64
{
    public partial class Form_Function : Form
    {
        public static Form_Function form;

        public Form_Function()
        {
            form = this;

            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void Form_Function_Load()
        {
            Form1.음소거 = true;
            CB_에러내역보기.Checked = Properties.Settings.Default.CB_에러내역보기;
            CB_신규매수정지.Checked = Properties.Settings.Default.CB_신규매수정지;
            CB_추가매수정지.Checked = Properties.Settings.Default.CB_추가매수정지;
            CB_상매수취소.Checked = Properties.Settings.Default.CB_상매수취소;
            CB_하매도취소.Checked = Properties.Settings.Default.CB_하매도취소;
            CB_VI매수취소.Checked = Properties.Settings.Default.CB_VI매수취소;
            CB_VI매도취소.Checked = Properties.Settings.Default.CB_VI매도취소;
            CB_상전량청산.Checked = Properties.Settings.Default.CB_상전량청산;
            CB_하전량청산.Checked = Properties.Settings.Default.CB_하전량청산;
            CB_NXT.Checked = Properties.Settings.Default.CB_NXT;
            CB_ETF매입비제외.Checked = Properties.Settings.Default.CB_ETF매입비제외;

            CB_수익금or수익률.Checked = Properties.Settings.Default.CB_수익금or수익률;
            CB_편입추가.Checked = Properties.Settings.Default.CB_편입추가;
            CB_주문엑셀저장.Checked = Properties.Settings.Default.CB_주문엑셀저장;
            CB_체결엑셀저장.Checked = Properties.Settings.Default.CB_체결엑셀저장;
            CB_매매엑셀저장.Checked = Properties.Settings.Default.CB_매매엑셀저장;
            CB_텔레그램사용.Checked = Properties.Settings.Default.CB_텔레그램사용;
            CB_매수알림.Checked = Properties.Settings.Default.CB_매수알림;
            CB_매도알림.Checked = Properties.Settings.Default.CB_매도알림;
            TB_Chat_ID.Text = Properties.Settings.Default.TB_Chat_ID;
            TB_telegram_send_ID.Text = Properties.Settings.Default.TB_telegram_send_ID;
            TB_token.Text = Properties.Settings.Default.TB_token;
            CB_Record.Checked = Properties.Settings.Default.CB_Record;
            TB_Record_Run.Text = Properties.Settings.Default.TB_Record_Run.ToString();
            CBB_Record.SelectedIndex = Properties.Settings.Default.CBB_Record;
            int.TryParse(TB_Record_start.Text, out int Record_start);
            int.TryParse(TB_Record_end.Text, out int Record_end);
            Properties.Settings.Default.TB_Record_start = Math.Abs(Record_start);
            Properties.Settings.Default.TB_Record_end = Math.Abs(Record_end);
            TB_Record_start.Text = Properties.Settings.Default.TB_Record_start.ToString();
            TB_Record_end.Text = Properties.Settings.Default.TB_Record_end.ToString();
            CB_지니_64항상위에.Checked = Properties.Settings.Default.CB_지니_64항상위에;
            CB_최대화로실행.Checked = Properties.Settings.Default.CB_최대화로실행;
            CB_지니_64크기고정.Checked = Properties.Settings.Default.CB_지니_64크기고정;

            CB_최종가업데이트.Checked = Properties.Settings.Default.CB_최종가업데이트;
            CB_최종매입가_A.Checked = Properties.Settings.Default.CB_최종매입가_A;
            CB_최종매입가_B.Checked = Properties.Settings.Default.CB_최종매입가_B;
            CB_최종매입가_C.Checked = Properties.Settings.Default.CB_최종매입가_C;
            CB_최종매입가_D.Checked = Properties.Settings.Default.CB_최종매입가_D;
            CB_최종매입가_E.Checked = Properties.Settings.Default.CB_최종매입가_E;
            CB_최종매입가_F.Checked = Properties.Settings.Default.CB_최종매입가_F;
            CB_최종매입가_G.Checked = Properties.Settings.Default.CB_최종매입가_G;

            CB_익회모니터.Checked = Properties.Settings.Default.CB_익회모니터;
            CB_익절모니터.Checked = Properties.Settings.Default.CB_익절모니터;
            CB_보전모니터.Checked = Properties.Settings.Default.CB_보전모니터;
            CB_손절모니터.Checked = Properties.Settings.Default.CB_손절모니터;
            CB_시간청산범위.Checked = Properties.Settings.Default.CB_시간청산범위;
            CB_반복매매범위.Checked = Properties.Settings.Default.CB_반복매매범위;
            CB_리밸런싱범위.Checked = Properties.Settings.Default.CB_리밸런싱범위;
            CB_잔고청산범위.Checked = Properties.Settings.Default.CB_잔고청산범위;

            CB_시작가격보기.Checked = Properties.Settings.Default.CB_시작가격보기;
            CB_기준가격보기.Checked = Properties.Settings.Default.CB_기준가격보기;

            CB_잔고청산_A.Checked = Properties.Settings.Default.CB_잔고청산_A;
            CB_잔고청산_B.Checked = Properties.Settings.Default.CB_잔고청산_B;
            CB_잔고청산_C.Checked = Properties.Settings.Default.CB_잔고청산_C;
            CB_매도X.Checked = Properties.Settings.Default.CB_매도X;
            CB_추매X.Checked = Properties.Settings.Default.CB_추매X;

            CB_가이드매매.Checked = Properties.Settings.Default.CB_가이드매매;
            CB_기본매매변경.Checked = Properties.Settings.Default.CB_기본매매변경;



            BT_검색식확인.Enabled = Form1.form1.가이드검색식확인;

            if (CB_가이드매매.Checked)
            {
                CB_기본매매변경.Enabled = true;
            }
            else
            {
                Properties.Settings.Default.CB_기본매매변경 = false;
                CB_기본매매변경.Checked = false;
                CB_기본매매변경.Enabled = false;
            }

            if (Properties.Settings.Default.CB_가이드매매) ControllerDisable.Form_Function_Disable();

            Form1.음소거 = Properties.Settings.Default.CB_음소거;
        }

        public static void 기능설정_저장()
        {
            if (form.TB_Chat_ID.Text.Length < 5)
            {
                if (form.CB_텔레그램사용.Checked)
                {
                    form.CB_텔레그램사용.Checked = false;
                    form.CB_매수알림.Checked = false;
                    form.CB_매도알림.Checked = false;
                    form.TB_Chat_ID.Text = "";
                    Form1.AutoClosingAlram("Telegram Chat_ID 가 맞지 않습니다. 다시 확인하여 주세요.", "Telegram Error", 5, "에러");
                }
            }

            Properties.Settings.Default.CB_텔레그램사용 = form.CB_텔레그램사용.Checked;

            Properties.Settings.Default.CB_매수알림 = form.CB_매수알림.Checked;
            Properties.Settings.Default.CB_매도알림 = form.CB_매도알림.Checked;
            Properties.Settings.Default.TB_Chat_ID = form.TB_Chat_ID.Text;
            Properties.Settings.Default.TB_telegram_send_ID = form.TB_telegram_send_ID.Text;
            Properties.Settings.Default.TB_token = form.TB_token.Text;
            Properties.Settings.Default.CB_주문엑셀저장 = form.CB_주문엑셀저장.Checked;
            Properties.Settings.Default.CB_체결엑셀저장 = form.CB_체결엑셀저장.Checked;
            Properties.Settings.Default.CB_매매엑셀저장 = form.CB_매매엑셀저장.Checked;

            Properties.Settings.Default.CB_편입추가 = form.CB_편입추가.Checked;
            Properties.Settings.Default.CB_음소거 = form.CB_음소거.Checked;

            Properties.Settings.Default.CB_에러내역보기 = form.CB_에러내역보기.Checked;
            Properties.Settings.Default.CB_추가매수정지 = form.CB_추가매수정지.Checked;
            Properties.Settings.Default.CB_신규매수정지 = form.CB_신규매수정지.Checked;
            Properties.Settings.Default.CB_상매수취소 = form.CB_상매수취소.Checked;
            Properties.Settings.Default.CB_하매도취소 = form.CB_하매도취소.Checked;
            Properties.Settings.Default.CB_VI매수취소 = form.CB_VI매수취소.Checked;
            Properties.Settings.Default.CB_VI매도취소 = form.CB_VI매도취소.Checked;
            Properties.Settings.Default.CB_상전량청산 = form.CB_상전량청산.Checked;
            Properties.Settings.Default.CB_하전량청산 = form.CB_하전량청산.Checked;
            Properties.Settings.Default.CB_NXT = form.CB_NXT.Checked;
            Form1.NXT = form.CB_NXT.Checked;

            Properties.Settings.Default.CB_수익금or수익률 = form.CB_수익금or수익률.Checked;

            Properties.Settings.Default.CB_Record = form.CB_Record.Checked;
            Properties.Settings.Default.CBB_Record = form.CBB_Record.SelectedIndex;

            int.TryParse(form.TB_Record_Run.Text, out int OcamRun);
            if (OcamRun == 0) OcamRun = 084000;
            Properties.Settings.Default.TB_Record_Run = Math.Abs(OcamRun);
            form.TB_Record_Run.Text = Properties.Settings.Default.TB_Record_Run.ToString();

            int.TryParse(form.TB_Record_start.Text, out int TB_Record_start);
            if (TB_Record_start == 0) TB_Record_start = 800000;
            Properties.Settings.Default.TB_Record_start = Math.Abs(TB_Record_start);
            form.TB_Record_start.Text = Properties.Settings.Default.TB_Record_start.ToString();

            int.TryParse(form.TB_Record_end.Text, out int TB_Record_end);
            if (TB_Record_end == 0) TB_Record_end = 200000;
            Properties.Settings.Default.TB_Record_end = Math.Abs(TB_Record_end);
            form.TB_Record_end.Text = Properties.Settings.Default.TB_Record_end.ToString();

            Form1.음소거 = Properties.Settings.Default.CB_음소거;
            Form1.form1.수익금or수익률 = Properties.Settings.Default.CB_수익금or수익률;
            Form1.신규매수정지 = Properties.Settings.Default.CB_신규매수정지;
            Form1.추가매수정지 = Properties.Settings.Default.CB_추가매수정지;
            Form1.매매엑셀저장 = Properties.Settings.Default.CB_매매엑셀저장;
            Form1.체결엑셀저장 = Properties.Settings.Default.CB_체결엑셀저장;
            Form1.주문엑셀저장 = Properties.Settings.Default.CB_주문엑셀저장;

            Properties.Settings.Default.CB_최대화로실행 = form.CB_최대화로실행.Checked;
            Properties.Settings.Default.CB_지니_64항상위에 = form.CB_지니_64항상위에.Checked;
            Properties.Settings.Default.CB_지니_64크기고정 = form.CB_지니_64크기고정.Checked;


            Properties.Settings.Default.CB_최종가업데이트 = form.CB_최종가업데이트.Checked;
            Properties.Settings.Default.CB_최종매입가_A = form.CB_최종매입가_A.Checked;
            Properties.Settings.Default.CB_최종매입가_B = form.CB_최종매입가_B.Checked;
            Properties.Settings.Default.CB_최종매입가_C = form.CB_최종매입가_C.Checked;
            Properties.Settings.Default.CB_최종매입가_D = form.CB_최종매입가_D.Checked;
            Properties.Settings.Default.CB_최종매입가_E = form.CB_최종매입가_E.Checked;
            Properties.Settings.Default.CB_최종매입가_F = form.CB_최종매입가_F.Checked;
            Properties.Settings.Default.CB_최종매입가_G = form.CB_최종매입가_G.Checked;

            Properties.Settings.Default.CB_익회모니터 = form.CB_익회모니터.Checked;
            Properties.Settings.Default.CB_익절모니터 = form.CB_익절모니터.Checked;
            Properties.Settings.Default.CB_보전모니터 = form.CB_보전모니터.Checked;
            Properties.Settings.Default.CB_손절모니터 = form.CB_손절모니터.Checked;
            Properties.Settings.Default.CB_시간청산범위 = form.CB_시간청산범위.Checked;
            Properties.Settings.Default.CB_반복매매범위 = form.CB_반복매매범위.Checked;
            Properties.Settings.Default.CB_리밸런싱범위 = form.CB_리밸런싱범위.Checked;
            Properties.Settings.Default.CB_잔고청산범위 = form.CB_잔고청산범위.Checked;

            Properties.Settings.Default.CB_시작가격보기 = form.CB_시작가격보기.Checked;
            Properties.Settings.Default.CB_기준가격보기 = form.CB_기준가격보기.Checked;

            Properties.Settings.Default.CB_잔고청산_A = form.CB_잔고청산_A.Checked;
            Properties.Settings.Default.CB_잔고청산_B = form.CB_잔고청산_B.Checked;
            Properties.Settings.Default.CB_잔고청산_C = form.CB_잔고청산_C.Checked;
            Properties.Settings.Default.CB_매도X = form.CB_매도X.Checked;
            Properties.Settings.Default.CB_추매X = form.CB_추매X.Checked;

            Properties.Settings.Default.CB_가이드매매 = form.CB_가이드매매.Checked;
            Properties.Settings.Default.CB_ETF매입비제외 = form.CB_ETF매입비제외.Checked;

            if (!form.CB_기본매매변경.Checked && Properties.Settings.Default.CB_기본매매변경)
            {
                Properties.Settings.Default.CB_기본매매변경 = form.CB_기본매매변경.Checked;
                Guide.계좌설정();
                Guide.기본매매설정();
            }

            Properties.Settings.Default.CB_기본매매변경 = form.CB_기본매매변경.Checked;
            if (!form.CB_기본매매변경.Checked && form.CB_가이드매매.Checked)
            {
                Form1.form1.TB_setjango.Enabled = false;
                form.CB_편입추가.Enabled = false;
                form.CB_최종가업데이트.Enabled = false;
                form.CB_신규매수정지.Enabled = false;
                form.CB_추가매수정지.Enabled = false;
                form.CB_VI매수취소.Enabled = false;
                form.CB_VI매도취소.Enabled = false;
                form.CB_상매수취소.Enabled = false;
                form.CB_하매도취소.Enabled = false;
                form.CB_상전량청산.Enabled = false;
                form.CB_하전량청산.Enabled = false;
                form.CB_NXT.Enabled = false;
                form.BT_가이드매매.Enabled = false;
                Form1.form1.CB_계좌매입비_매수제한.Enabled = false;
                Form1.form1.TB_계좌매입비_제한비중.Enabled = false;
                Form1.form1.CBB_계좌매입비_제한선택.Enabled = false;
                Form1.form1.TB_계좌매입비_현비중.Enabled = false;
                Form1.form1.CB_잔고매입비_추매제한.Enabled = false;
                Form1.form1.TB_잔고매입비_추매제한.Enabled = false;
                Form1.form1.CB_misu.Enabled = false;
                Form1.form1.CB_misu.Enabled = false;
                Form1.form1.CB_misu.Enabled = false;
                Form1.form1.MT_misu_time.Enabled = false;
                Form1.form1.Combo_misu.Enabled = false;
                Form1.form1.TB_misu_ratio.Enabled = false;
                Form1.form1.TB_misu_value.Enabled = false;
                Form1.form1.Combo_misu_jumnun.Enabled = false;
                Form1.form1.TB_misu_repeat_time.Enabled = false;
                Form1.form1.label_계좌매입비1.Enabled = false;
                Form1.form1.label_계좌매입비2.Enabled = false;
                Form1.form1.label_잔고매입비.Enabled = false;
                Form1.form1.label_misu.Enabled = false;
            }

            칼람추가();
        }

        public static void 칼람추가()
        {
            int insert_index = Form1.form1.JanGo_dataGridView.Columns["초기매수검색식"].Index + 1;
            체크박스칼럼추가(Properties.Settings.Default.CB_매도X, "도", "매도_정지", insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("매도_정지")) insert_index = Form1.form1.JanGo_dataGridView.Columns["매도_정지"].Index + 1;
            체크박스칼럼추가(Properties.Settings.Default.CB_추매X, "추", "추매_정지", insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("추매_정지")) insert_index = Form1.form1.JanGo_dataGridView.Columns["추매_정지"].Index + 1;
            체크박스칼럼추가(Properties.Settings.Default.CB_잔고청산_A, "A", "잔고청산_A", insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("잔고청산_A")) insert_index = Form1.form1.JanGo_dataGridView.Columns["잔고청산_A"].Index + 1;
            체크박스칼럼추가(Properties.Settings.Default.CB_잔고청산_B, "B", "잔고청산_B", insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("잔고청산_B")) insert_index = Form1.form1.JanGo_dataGridView.Columns["잔고청산_B"].Index + 1;
            체크박스칼럼추가(Properties.Settings.Default.CB_잔고청산_C, "C", "잔고청산_C", insert_index);

            void 체크박스칼럼추가(bool result, string Title, string Name, int index)
            {
                if (result)
                {
                    if (!Form1.form1.JanGo_dataGridView.Columns.Contains(Name))
                    {
                        DataGridViewCheckBoxColumn Column = new DataGridViewCheckBoxColumn();

                        Column.HeaderText = Title;
                        Column.Name = Name;
                        Column.MinimumWidth = 10;
                        Column.ReadOnly = false;
                        Column.Width = 25;
                        Column.ToolTipText = Name;
                        Column.TrueValue = true;
                        Column.ReadOnly = true;

                        Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        Form1.form1.JanGo_dataGridView.Columns.Insert(index, Column);
                    }
                }
                else
                {
                    if (Form1.form1.JanGo_dataGridView.Columns.Contains(Name)) Form1.form1.JanGo_dataGridView.Columns.Remove(Name);
                }
            }

            insert_index = Form1.form1.JanGo_dataGridView.Columns["수익률_잔고A"].Index + 1;
            매입가컬럼추가(Properties.Settings.Default.CB_시작가격보기, "시작", insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("시작가")) insert_index = Form1.form1.JanGo_dataGridView.Columns["시작%"].Index + 1;
            매입가컬럼추가(Properties.Settings.Default.CB_기준가격보기, "기준", insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("기준가")) insert_index = Form1.form1.JanGo_dataGridView.Columns["기준%"].Index + 1;
            최종컬럼추가(Properties.Settings.Default.CB_최종매입가_A, "A", insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("수익률_A")) insert_index = Form1.form1.JanGo_dataGridView.Columns["수익률_A"].Index + 1;
            최종컬럼추가(Properties.Settings.Default.CB_최종매입가_B, "B", insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("수익률_B")) insert_index = Form1.form1.JanGo_dataGridView.Columns["수익률_B"].Index + 1;
            최종컬럼추가(Properties.Settings.Default.CB_최종매입가_C, "C", insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("수익률_C")) insert_index = Form1.form1.JanGo_dataGridView.Columns["수익률_C"].Index + 1;
            최종컬럼추가(Properties.Settings.Default.CB_최종매입가_D, "D", insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("수익률_D")) insert_index = Form1.form1.JanGo_dataGridView.Columns["수익률_D"].Index + 1;
            최종컬럼추가(Properties.Settings.Default.CB_최종매입가_E, "E", insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("수익률_E")) insert_index = Form1.form1.JanGo_dataGridView.Columns["수익률_E"].Index + 1;
            최종컬럼추가(Properties.Settings.Default.CB_최종매입가_F, "F", insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("수익률_F")) insert_index = Form1.form1.JanGo_dataGridView.Columns["수익률_F"].Index + 1;
            최종컬럼추가(Properties.Settings.Default.CB_최종매입가_G, "G", insert_index);

            void 매입가컬럼추가(bool result, string POS, int index)
            {
                if (result)
                {
                    if (!Form1.form1.JanGo_dataGridView.Columns.Contains(POS + "가"))
                    {
                        DataGridViewTextBoxColumn Column = new DataGridViewTextBoxColumn();
                        Column.ReadOnly = true;
                        Column.SortMode = DataGridViewColumnSortMode.Automatic;
                        Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Column.MinimumWidth = 10;
                        Column.HeaderText = POS + "가";
                        Column.Name = POS + "가";
                        Column.Width = 55;
                        Column.DefaultCellStyle.Format = "N0";
                        Column.DefaultCellStyle.NullValue = null;
                        if (POS.Equals("시작")) Column.ToolTipText = "최초 매수가격";
                        else
                        {
                            Column.ReadOnly = false;
                            Column.ToolTipText = "*기본값(첫 진입가격) 변경방법: 1. 기준가 셀 선택 하여 입력한다 2. 기준가셀이 아닌 다른셀을 선택한다. 3 . 한번의 여러종목의 기준값을 입력후 다른셀을 선택하면 한번에 저장됩니다.";
                        }
                        Form1.form1.JanGo_dataGridView.Columns.Insert(index, Column);
                    }

                    if (!Form1.form1.JanGo_dataGridView.Columns.Contains(POS + "%"))
                    {
                        DataGridViewTextBoxColumn Column = new DataGridViewTextBoxColumn();
                        Column.ReadOnly = true;
                        Column.SortMode = DataGridViewColumnSortMode.Automatic;
                        Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Column.MinimumWidth = 10;
                        Column.HeaderText = POS + "%";
                        Column.Name = POS + "%";
                        Column.Width = 50;
                        Column.DefaultCellStyle.Format = "N2";
                        Column.DefaultCellStyle.NullValue = null;
                        if (POS.Equals("시작")) Column.ToolTipText = "최초 매수가격 수익률 (세금+수수료 포함)";
                        else Column.ToolTipText = "기준가격 수익률(세금+수수료 포함)";
                        Form1.form1.JanGo_dataGridView.Columns.Insert(index + 1, Column);
                    }
                }
                else
                {
                    if (Form1.form1.JanGo_dataGridView.Columns.Contains(POS + "가")) Form1.form1.JanGo_dataGridView.Columns.Remove(POS + "가");
                    if (Form1.form1.JanGo_dataGridView.Columns.Contains(POS + "%")) Form1.form1.JanGo_dataGridView.Columns.Remove(POS + "%");
                }
            }

            void 최종컬럼추가(bool result, string Num, int index)
            {
                if (result)
                {
                    if (!Form1.form1.JanGo_dataGridView.Columns.Contains("차수_" + Num))
                    {
                        DataGridViewTextBoxColumn Column = new DataGridViewTextBoxColumn();
                        Column.ReadOnly = true;
                        Column.SortMode = DataGridViewColumnSortMode.Automatic;
                        Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Column.MinimumWidth = 10;
                        Column.HeaderText = Num + "_차수";
                        Column.Name = "차수_" + Num;
                        Column.Width = 50;
                        Column.ToolTipText = Num + "_리밸런싱 수익범위 옵션(최종매입가 n%이하)으로 매수된 차수";
                        Form1.form1.JanGo_dataGridView.Columns.Insert(index, Column);
                    }
                    if (!Form1.form1.JanGo_dataGridView.Columns.Contains("최종가_" + Num))
                    {
                        DataGridViewTextBoxColumn Column = new DataGridViewTextBoxColumn();
                        Column.ReadOnly = true;
                        Column.SortMode = DataGridViewColumnSortMode.Automatic;
                        Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Column.MinimumWidth = 10;
                        Column.HeaderText = "_최종가_";
                        Column.Name = "최종가_" + Num;
                        Column.Width = 60;
                        Column.DefaultCellStyle.Format = "N0";
                        Column.DefaultCellStyle.NullValue = null;
                        Column.ToolTipText = Num + "_리밸런싱 수익범위 옵션(최종매입가 n%이하)으로 매수된 최종매입가";
                        Form1.form1.JanGo_dataGridView.Columns.Insert(index + 1, Column);
                    }

                    if (!Form1.form1.JanGo_dataGridView.Columns.Contains("수익률_" + Num))
                    {
                        DataGridViewTextBoxColumn Column = new DataGridViewTextBoxColumn();
                        Column.ReadOnly = true;
                        Column.SortMode = DataGridViewColumnSortMode.Automatic;
                        Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Column.MinimumWidth = 10;
                        Column.HeaderText = "수익_" + Num;
                        Column.Name = "수익률_" + Num;
                        Column.Width = 50;
                        Column.DefaultCellStyle.Format = "N2";
                        Column.DefaultCellStyle.NullValue = null;
                        Column.ToolTipText = Num + "_리밸런싱수익범위 옵션(최종매입가 n%이하)으로 매수된 최종매입가의 현재가 대비 수익률 (세금+수수료 포함)";
                        Form1.form1.JanGo_dataGridView.Columns.Insert(index + 2, Column);
                    }
                }
                else
                {
                    if (Form1.form1.JanGo_dataGridView.Columns.Contains("차수_" + Num)) Form1.form1.JanGo_dataGridView.Columns.Remove("차수_" + Num);
                    if (Form1.form1.JanGo_dataGridView.Columns.Contains("최종가_" + Num)) Form1.form1.JanGo_dataGridView.Columns.Remove("최종가_" + Num);
                    if (Form1.form1.JanGo_dataGridView.Columns.Contains("수익률_" + Num)) Form1.form1.JanGo_dataGridView.Columns.Remove("수익률_" + Num);
                }
            }

            insert_index = Form1.form1.JanGo_dataGridView.Columns["재매수_잔고A"].Index + 1;
            컬럼추가(Properties.Settings.Default.CB_익회모니터, "일회모니터", 118, insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("일회모니터")) insert_index = Form1.form1.JanGo_dataGridView.Columns["일회모니터"].Index + 1;
            컬럼추가(Properties.Settings.Default.CB_익절모니터, "익절 & 트레일링", 118, insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("익절 & 트레일링")) insert_index = Form1.form1.JanGo_dataGridView.Columns["익절 & 트레일링"].Index + 1;
            컬럼추가(Properties.Settings.Default.CB_보전모니터, "보전모니터", 118, insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("보전모니터")) insert_index = Form1.form1.JanGo_dataGridView.Columns["보전모니터"].Index + 1;
            컬럼추가(Properties.Settings.Default.CB_손절모니터, "손절모니터", 80, insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("손절모니터")) insert_index = Form1.form1.JanGo_dataGridView.Columns["손절모니터"].Index + 1;
            컬럼추가(Properties.Settings.Default.CB_시간청산범위, "시간청산", 60, insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("시간청산")) insert_index = Form1.form1.JanGo_dataGridView.Columns["시간청산"].Index + 1;
            컬럼추가(Properties.Settings.Default.CB_반복매매범위, "반복매매 범위모니터", 180, insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("반복매매 범위모니터")) insert_index = Form1.form1.JanGo_dataGridView.Columns["반복매매 범위모니터"].Index + 1;
            컬럼추가(Properties.Settings.Default.CB_리밸런싱범위, "리밸런싱", 95, insert_index);

            if (Form1.form1.JanGo_dataGridView.Columns.Contains("리밸런싱")) insert_index = Form1.form1.JanGo_dataGridView.Columns["리밸런싱"].Index + 1;
            컬럼추가(Properties.Settings.Default.CB_잔고청산범위, "잔고청산", 60, insert_index);

            void 컬럼추가(bool result, string Title, int Size, int index)
            {
                if (result)
                {
                    if (!Form1.form1.JanGo_dataGridView.Columns.Contains(Title))
                    {
                        DataGridViewTextBoxColumn Column = new DataGridViewTextBoxColumn();
                        Column.HeaderText = Title;
                        Column.Name = Title;
                        Column.MinimumWidth = 10;
                        Column.ReadOnly = true;
                        Column.Width = Size;
                        Column.ToolTipText = "ON 일때는 알파벳, OFF 일때는 ' - ' 표시 됩니다.";
                        Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Form1.form1.JanGo_dataGridView.Columns.Insert(index, Column);
                    }
                }
                else
                {
                    if (Form1.form1.JanGo_dataGridView.Columns.Contains(Title)) Form1.form1.JanGo_dataGridView.Columns.Remove(Title);
                }
            }

            foreach (var code in Form1.stockBalanceList.ToList())
            {
                Stockbalance 잔고 = code.Value;
                for (int i = 0; i < Form1.form1.JanGo_dataGridView.Rows.Count; i++)
                {
                    if (Form1.form1.JanGo_dataGridView["코드_잔고A", i].Value.ToString().Equals(잔고.종목코드))
                    {
                        if (잔고.재매수 > 0)
                        {
                            GridView_Print.JangoRow_print(i, 잔고);
                        }
                    }
                }
            }
        }


        private void Form_Function_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Properties.Settings.Default.CB_레이아웃고정_기능설정 = CB_레이아웃고정_기능설정.Checked;
                Properties.Settings.Default.CB_기능설정_시작위치저장 = form.CB_기능설정_시작위치저장.Checked;
                if (Properties.Settings.Default.CB_기능설정_시작위치저장) Properties.Settings.Default.WindowLocation = form.Location; LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
                LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
                Form1.form1.CB_기능설정.Checked = false;
                Form1.FormFunction_Open = false;
                Form1.form1.TopMost = Properties.Settings.Default.CB_지니_64항상위에;

                e.Cancel = true;
                Hide();
            }
        }

        private void CB_레이아웃고정_기능설정_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CB_레이아웃고정_기능설정 = CB_레이아웃고정_기능설정.Checked;

            if (!CB_레이아웃고정_기능설정.Checked) LayoutChange.CBB_layout_SelectedIndex(-1);
            else LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
        }

        private void 기능설정_저장_Click(object sender, EventArgs e)
        {
            Form1.form1.Select();
            Form1.MBC_sender = (sender as Button).Name;
            Form1.중요메세지("기능설정", "기능설정 설정 을 저장 하시겠습니까?");
        }

        private void Once_CheckedChanged(object sender, EventArgs e)
        {
            if (Form1.FormFunction_Open)
            {
                Form1.form1.체크박스_비프(sender);

                if (sender.Equals(CB_텔레그램사용))
                {
                    Form1.form1.Telegram_Bot = new Telegram.Bot.TelegramBotClient(Properties.Settings.Default.TB_token);
                }

                if (sender.Equals(CB_수익금or수익률))
                {
                    CB_수익금();
                }

                if (sender.Equals(CB_동작실시간))
                {
                    Form1.form1.동작실시간 = CB_동작실시간.Checked;
                }
            }
        }

        private void CB_텔레그램n녹화_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);
            string text = CB.Text.Substring(1);
            if (CB.Checked)
            {
                if (Form1.로딩완료)
                {
                    Form1.비프음("체크");
                }

                CB.Text = "■" + text;

                if (sender.Equals(CB_매수알림))
                {
                    if (!CB_텔레그램사용.Checked)
                    {
                        CB_매수알림.Checked = false;
                        Form1.알림창("[ 사용방법 알림 ]\n\n텔레그램 사용 일때만 매수 알림을 받을수 있습니다.", 10, false);
                    }
                }

                if (sender.Equals(CB_매도알림))
                {
                    if (!CB_텔레그램사용.Checked)
                    {
                        CB_매도알림.Checked = false;
                        Form1.알림창("[ 사용방법 알림 ]\n\n텔레그램 사용 일때만 매도 알림을 받을수 있습니다.", 10, false);
                    }
                }
            }
            else
            {
                if (Form1.로딩완료) Form1.비프음("언체크");
                CB.Text = "□" + text;
            }
        }

        private void BT_OcamRun_Click(object sender, EventArgs e)
        {
            if (CBB_Record.SelectedIndex == 1)
            {
                this.ActiveControl = null;
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
                    Form1.AutoClosingAlram("오캠 경로에 파일이 없습니다.\n경로 C:\\Program Files (x86)\\oCam\\oCam.exe", "알람", 10, "에러");
                }
            }
            else if (CBB_Record.SelectedIndex == 2)
            {
                this.ActiveControl = null;
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


        private void BT_telegram_send_Click(object sender, EventArgs e)
        {
            ActiveControl = null;
            if (TB_token.Text.Trim().Length == 0)
            {
                TB_token.Text = "7029294465:AAHV_SY5VANBrNFTdq3rxUSBVjEpCqXwDss";
            }

            if (TB_Chat_ID.Text.Trim().Length > 0)
            {
                Form1.form1.Telegram_users.Clear();
                Form1.form1.Telegram_users.Add(TB_Chat_ID.Text);
                Properties.Settings.Default.TB_Chat_ID = TB_Chat_ID.Text;

                Form1.form1.Telegram_Bot = new Telegram.Bot.TelegramBotClient(TB_token.Text);

                string message = TB_telegram_send_ID.Text;
                TelegramMessenger.Telegram_Send(message);

                CB_텔레그램사용.Checked = Properties.Settings.Default.CB_텔레그램사용;
                CB_매수알림.Checked = Properties.Settings.Default.CB_매수알림;
                CB_매도알림.Checked = Properties.Settings.Default.CB_매도알림;
            }
            else
            {
                Form1.form1.Invoke((MethodInvoker)delegate ()
                {
                    using (new CenterWinDialog(Form1.form1))
                        MessageBox.Show("텔레그램 메세지전달 에러.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                });
            }
        }

        public void CB_수익금()
        {
            foreach (DataGridViewColumn column in Form1.form1.JanGo_dataGridView.Columns)
            {
                if (CB_수익금or수익률.Checked)
                {
                    if (column.HeaderText.Equals("최고수익률")) column.HeaderText = "최고손익금";
                    if (column.HeaderText.Equals("최저수익률")) column.HeaderText = "최저손익금";
                }
                else
                {
                    if (column.HeaderText.Equals("최고손익금")) column.HeaderText = "최고수익률";
                    if (column.HeaderText.Equals("최저손익금")) column.HeaderText = "최저수익률";
                }
            }
        }

        private void CB_윈도우최상위_CheckedChanged(object sender, EventArgs e)
        {
            if (Form1.FormFunction_Open)
            {
                if (CB_지니_64항상위에.Checked)
                    Form1.form1.TopMost = true;
                else
                    Form1.form1.TopMost = false;
            }
        }

        private void 버튼음_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.FormFunction_Open) Form1.비프음("체크");
        }

        private void CB_최대화로실행_CheckedChanged(object sender, EventArgs e)
        {
            if (Form1.FormFunction_Open)
            {
                Form1.form1.체크박스_비프(sender);

                if (CB_최대화로실행.Checked) Form1.form1.WindowState = FormWindowState.Maximized;
                else Form1.form1.WindowState = FormWindowState.Normal;
            }
        }

        private void TextBox_양실수만(object sender, EventArgs e)
        {
            TextValue.TextBox_양실수만(sender);
        }

        private void _양수실수_키프레스(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, false); // textbox 에 양수 , 실수 숫자만 입력 받을수 있음
        }

        private void CB_지니_64크기고정_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_지니_64크기고정.Checked)
            {
                Form1.form1.MaximumSize = new Size(1936, 1054);
                Form1.form1.MinimumSize = new Size(1936, 1054);
                Form1.form1.Size = new Size(1936, 1054);
            }
            else
            {
                Form1.form1.MaximumSize = new Size(0, 0);
                Form1.form1.MinimumSize = new Size(0, 0);
            }
        }

        private void CB_가이드매매_CheckedChanged(object sender, EventArgs e)
        {
            if (Form1.FormFunction_Open)
            {
                Form_Function.form.Enabled = false;

                Form1.form1.Select();
                Form1.MBC_sender = (sender as CheckBox).Name;

                if (Form1.form1.가이드매매메세지)
                {
                    Form1.form1.가이드매매메세지 = false;

                    if (CB_가이드매매.Checked)
                    {
                        Form1.중요메세지("가이드매매 자동적용", "가이드매매 자동적용을 위해서는 재시작이 필요 합니다.\n지금 지니_64를 재시작 하겠습니까?");
                    }
                    else
                    {
                        Form1.중요메세지("가이드매매 자동적용", "가이드매매 자동적용 '해제' 를 위한 재시작이 필요 합니다.\n지금 지니_64를 재시작 하겠습니까?");
                    }
                }
            }
        }

        private void BT_가이드매매_Click(object sender, EventArgs e)
        {

            Form1.form1.Select();
            Form1.MBC_sender = (sender as Button).Name;
            Form1.중요메세지("가이드매매 설정", "가이드매매 설정을 불러옵니다. 매매가 중단 됩니다.\n지금 설정을 불러 오겠습니까?");

            Guide.가이드매매저장();
        }

        private void CB_기본매매변경_CheckedChanged(object sender, EventArgs e)
        {
            if (Form1.FormFunction_Open)
            {
                Form1.form1.Select();
                Form1.MBC_sender = (sender as CheckBox).Name;

                if (CB_기본매매변경.Checked && !Properties.Settings.Default.CB_기본매매변경)
                {
                    Form1.중요메세지("가이드매매 자동적용", "계좌설정, 기본매매, 기능설정 값을 " +
                          "\n\n변경할수 있습니다.");
                }
            }
        }

        private void BT_검색식확인_Click(object sender, EventArgs e)
        {
            FileInfo File_ = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\\지니_64BackUP\\사용검색식.txt");
            if (File_.Exists)
            {
                File_.Delete();
            }

            Guide.Load_condition_textprint();
        }

        private void CB_SOR_CheckedChanged(object sender, EventArgs e)
        {
            if (!Form1.server.Equals("실서버"))
            {
                form.CB_NXT.Checked = false;
                Form1.MBC_sender = (sender as CheckBox).Name;
                Form1.중요메세지("NXT 주문사용", "실서버만 NXT 주문을 사용할수 있습니다.\nNXT 주문 시간은 8:00:00 ~ 8:50:00, 15:40:00 ~ 20:00:00 입니다.");
                return;
            }
            else
            {
                if (!Properties.Settings.Default.CB_NXT && CB_NXT.Checked)
                {
                    Form1.중요메세지("NXT 주문사용", "※ 실서버만 NXT 주문을 사용할수 있습니다.\n" +
                                         "※ NXT 주문 간시간은\n" +
                                         "※ 8:00:00 ~ 8:50:00, 15:40:00 ~ 20:00:00 입니다.\n" +
                                         "※ NXT 주문 시간에는 지수이평의 일봉만 적용됩니다.");
                }
            }

            Form1.form1.체크박스_비프(sender);
        }
    }
}
