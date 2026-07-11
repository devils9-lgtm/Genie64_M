using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace 지니64.box
{
    public partial class Form_Outstanding : UserControl
    {
        public static Form_Outstanding form;

        public Form_Outstanding()
        {
            form = this;
            InitializeComponent();
        }

        private void BT_미체결크게보기_Click(object sender, EventArgs e)
        {
            Form1.form1.CB_기본매매.Checked = false;
            Form1.form1.CB_반복매매.Checked = false;
            Form1.form1.CB_계좌관리.Checked = false;
            Form1.form1.CB_특수매매.Checked = false;
            Form1.form1.CB_대금탐색.Checked = false;
            Form1.form1.CB_매매그룹.Checked = false;
            Form1.form1.CB_기능설정.Checked = false;
            Form1.form1.CBB_layout.SelectedIndex = 5;
        }

        private void CBB_미체결종목_DropDown(object sender, EventArgs e)
        {
            ComboBox targetComboBox = form.CBB_미체결종목;
           
            targetComboBox.Items.Clear();
            targetComboBox.Items.Add("( ALL )");

            IEnumerable<string> distinctStockNames = Form1.JumunItem_List.Values
            .Select(item => item.종목명) // 모든 항목에서 종목명만 선택
            .Distinct();                // 중복되는 종목명 제거 (O(N) 효율)

            foreach (string stockName in distinctStockNames)
            {
                targetComboBox.Items.Add(stockName);
            }
        }

        private void CBB_미체결종목_SelectedIndexChanged(object sender, EventArgs e)
        {
            // DataGridView 초기화
            form.Outstanding_DataGridView.Rows.Clear();  Method.SortClear(form.Outstanding_DataGridView);

            int selectedIndex = form.CBB_미체결종목.SelectedIndex;

            // 인덱스 0("( ALL )")은 제외하고 특정 종목이 선택된 경우에만 처리 (기존 로직 유지)
            if (selectedIndex > 0)
            {
                // 1. 선택된 종목명 가져오기
                object selectedStock = form.CBB_미체결종목.SelectedItem;
                int row = 0;

                // 2. [필터링] 딕셔너리의 '값(Value)' 컬렉션에서 선택된 종목과 일치하는 주문만 필터링합니다.
                // 불필요한 ToList() 호출을 제거하고 LINQ Where를 사용합니다.
                IEnumerable<JumunItem> filteredOrders = Form1.JumunItem_List.Values
                    .Where(jumun => selectedStock.Equals(jumun.종목명));

                // 3. 필터링된 주문 목록을 순회하며 DataGridView에 출력합니다.
                foreach (var jumun in filteredOrders)
                {
                    // GridView 출력 조건 검사 (기존 로직 유지)
                    if (GridView_Print.Find_OutstandingStock(jumun))
                    {
                        // UI 업데이트
                        GridView_Print.Outstanding_insert(jumun, row); // 종목선택 정렬
                        row++;
                    }
                }
            }

            // 4. 선택된 인덱스를 전역 변수에 저장 (기존 로직 유지)
            Form1.Get.미체결종목_index = selectedIndex;
        }

        private void CBB_미체결종목_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.로딩완료) Form1.비프음("체크");

            if (form.CBB_미체결종목.SelectedIndex == -1)
            {
                form.CBB_미체결종목.SelectedIndex = Form1.Get.미체결종목_index;
            }
        }

        private void BT_매수ALL취소_Click(object sender, EventArgs e)
        {
            Form1.MBC_sender = (sender as Button).Name;
            Form1.중요메세지("매수 ALL취소", Form_Outstanding.form.CBB_미체결종목.Text + " 종목의 미체결 매수 주문을 전부 취소 하겠습니까 ?");
        }

        private void BT_매도ALL취소_Click(object sender, EventArgs e)
        {
            Form1.MBC_sender = (sender as Button).Name;
            Form1.중요메세지("매도 ALL취소", Form_Outstanding.form.CBB_미체결종목.Text + " 종목의 미체결 매도 주문을 전부 취소 하겠습니까 ?");
        }

        public void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e) // 그리드뷰 색깔 변화   &&  잔고 (A,B) 선택 변화 
        {
            DataGridView view = sender as DataGridView;

            if (e.ColumnIndex == -1 || e.RowIndex == -1)
            {
                return;
            }
            else
            {
                if (sender.Equals(Form_Outstanding.form.Outstanding_DataGridView))
                {
                    try
                    {
                        DataGridViewCell cell_구분 = view["매수매도", e.RowIndex];
                        string text_구분 = cell_구분.FormattedValue.ToString();

                        if (text_구분.Contains("매수"))
                        {
                            cell_구분.Style.ForeColor = Color.Red;
                        }
                        else if (text_구분.Contains("매도"))
                        {
                            cell_구분.Style.ForeColor = Color.Blue;
                        }
                        else if (text_구분.Contains("취소"))
                        {
                            cell_구분.Style.ForeColor = Color.Black;
                        }
                        form.Outstanding_DataGridView["검색식", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;
                        form.Outstanding_DataGridView["적용금액", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;

                        DataGridViewCell cell_틱차이 = view["틱차이", e.RowIndex];
                        string text_틱차이 = cell_틱차이.FormattedValue.ToString();

                        if (text_틱차이.Equals("0 틱"))
                        {
                            cell_틱차이.Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            if (text_틱차이.Contains("-"))
                            {
                                cell_틱차이.Style.ForeColor = Color.Blue;
                            }
                            else
                            {
                                cell_틱차이.Style.ForeColor = Color.Red;
                            }
                        }
                        form.Outstanding_DataGridView["주문가격", e.RowIndex].Style.ForeColor = cell_틱차이.Style.ForeColor;

                        DataGridViewCell cell_수익률 = view["수익률", e.RowIndex];
                        Form1.form1.DGV_color(cell_수익률);

                        DataGridViewCell cell_등락률 = view["등락률", e.RowIndex];
                        Form1.form1.DGV_color(cell_등락률);
                        Form_Outstanding.form.Outstanding_DataGridView["현재가", e.RowIndex].Style.ForeColor = cell_등락률.Style.ForeColor;
                    }
                    catch
                    {
                        Log.에러기록("Outstanding_DataGridView_A  에러:: 메세지 _Grid_CellValueChanged 에러");
                    }
                }
            }
        }

        private void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)             // 원하는 칼럼에 자동 번호 매기기
        {
            if (sender.Equals(Form_Outstanding.form.Outstanding_DataGridView))
            {
                Form_Outstanding.form.Outstanding_DataGridView.Rows[e.RowIndex].Cells["Num"].Value = Form_Outstanding.form.Outstanding_DataGridView.Rows.Count - (e.RowIndex + 1) + 1;
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.ActiveControl = null;

            DataGridView view = sender as DataGridView;

            if (e.RowIndex == -1) view.CurrentCell = null;

            if (sender.Equals(Form_Outstanding.form.Outstanding_DataGridView)) // 주문취소 클릭
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (e.ColumnIndex == Form_Outstanding.form.Outstanding_DataGridView.Columns["주문취소"].Index)
                    {
                        string 주문번호 = Form_Outstanding.form.Outstanding_DataGridView["주문번호", e.RowIndex].Value.ToString();

                        if (Form1.JumunItem_List.TryGetValue(주문번호, out JumunItem jumun))
                        {
                            if (jumun.취소timer > 0 || (jumun.취소timer == 0 && jumun.반복횟수 > 0))
                            {
                                var thread = new Thread(
                                () =>
                                {
                                    Helper.안전한_UI_업데이트(Form1.form1, () =>
                                    {
                                        using (new CenterWinDialog(Form1.form1))
                                        {
                                            // [문자열 보간 적용] 메시지 박스 내용 정리
                                            string 질문 = $"{jumun.종목명}\n 주문유형: {GET.매수매도str(jumun.매수매도)} 수량: {jumun.미체결량:N0}을 '취소' 하시겠습니까 ?";
                               
                                            if (MessageBox.Show(질문, "취소알림", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                            {
                                                if (jumun.검색식.Contains("신규_") && jumun.주문수량 == jumun.미체결량)
                                                {
                                                    GenieConfig.신규횟수--;
                                                }
                               
                                                jumun.비고 = "'취소'버튼클릭";
                                                jumun.반복횟수 = 0;
                                                jumun.취소시간 = 0;
                                                jumun.취소timer = 0;
                               
                                                // [문자열 보간 적용] 로그 메시지 통합 및 최적화
                                                string 취소로그 = $"[주문취소] 종목명:{jumun.종목명} 주문유형:{GET.매수매도str(jumun.매수매도)} 수량: {jumun.미체결량:N0} 주문을 취소 하였습니다.";
                               
                                                Log.동작기록(" ");
                                                Log.동작기록(취소로그);
                                                Log.동작기록(" ");
                                            }
                                        }
                                    });
                                });
                                thread.Start();
                            }
                        }
                    }

                    Order_Reserve.종목선택(Form_Outstanding.form.Outstanding_DataGridView["종목명", e.RowIndex].Value.ToString());

                    DataGridViewCell cell_color = view[e.ColumnIndex, e.RowIndex];
                    Form_Outstanding.form.Outstanding_DataGridView[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = cell_color.Style.ForeColor;
                }
            }
        }


        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e) // 데이터그리드뷰 동작 속도 올리기
        {
            if (Form_Outstanding.form.Outstanding_DataGridView.CurrentCell is DataGridViewCheckBoxCell)
            {
                Form_Outstanding.form.Outstanding_DataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void Outstanding_DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Front_End.DataGridView_CellPainting(sender, e);
        }

        private void label_미체결내역_MouseDown(object sender, MouseEventArgs e)
        {
            DGV_sell_null();
        }

        private void Form_Outstanding_MouseDown(object sender, MouseEventArgs e)
        {
            DGV_sell_null();
        }

        private void L_미체결row_MouseDown(object sender, MouseEventArgs e)
        {
            DGV_sell_null();
        }

        private void LB_미체결주문_MouseDown(object sender, MouseEventArgs e)
        {
            DGV_sell_null();
        }

        private void DGV_sell_null()
        {
            DataGridView dgv = Form_Outstanding.form.Outstanding_DataGridView;

            if (dgv.CurrentCell != null)
            {
                dgv.CurrentCell = null; // 현재 셀 선택 해제
            }

            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                row.Selected = false;
            }
        }

        private void Outstanding_DataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            Front_End.DataGridView_MouseDown_(sender, e);
        }
    }
}
