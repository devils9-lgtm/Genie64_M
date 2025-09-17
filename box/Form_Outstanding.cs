using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace 지니_64.box
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
            form.CBB_미체결종목.Items.Clear();
            form.CBB_미체결종목.Items.Add("( ALL )");
            foreach (var Item in Form1.JumunItem_List.ToList())
            {
                if (!form.CBB_미체결종목.Items.Contains(Item.종목명)) form.CBB_미체결종목.Items.Add(Item.종목명);
            }
        }

        private void CBB_미체결종목_SelectedIndexChanged(object sender, EventArgs e)
        {
            form.Outstanding_DataGridView.Rows.Clear();
            if (form.CBB_미체결종목.SelectedIndex > 0)
            {
                int row = 0;
                foreach (var JumunItem in Form1.JumunItem_List.ToList())
                {
                    if (form.CBB_미체결종목.SelectedItem.Equals(JumunItem.종목명))
                    {
                        if (GridView_Print.find_OutstandingStock(JumunItem))
                        {
                            GridView_Print.Outstanding_insert(JumunItem, row); // 종목선택 정렬
                            row++;
                        }
                    }
                }
            }

            Form1.form1.미체결종목_index = form.CBB_미체결종목.SelectedIndex;
        }

        private void CBB_미체결종목_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.로딩완료) Form1.비프음("체크");

            if (form.CBB_미체결종목.SelectedIndex == -1)
            {
                form.CBB_미체결종목.SelectedIndex = Form1.form1.미체결종목_index;
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
                        DataGridViewCell cell_구분 = view["주문유형_미체결", e.RowIndex];
                        string text_구분 = cell_구분.FormattedValue.ToString();

                        if (text_구분 == "매수")
                        {
                            cell_구분.Style.ForeColor = Color.Red;
                        }
                        else if (text_구분 == "매도")
                        {
                            cell_구분.Style.ForeColor = Color.Blue;
                        }
                        Form_Outstanding.form.Outstanding_DataGridView["검색식_미체결", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;
                        Form_Outstanding.form.Outstanding_DataGridView["적용금액_미체결", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;

                        DataGridViewCell cell_틱차이 = view["틱차이_미체결", e.RowIndex];
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
                        Form_Outstanding.form.Outstanding_DataGridView["주문가격_미체결", e.RowIndex].Style.ForeColor = cell_틱차이.Style.ForeColor;


                        DataGridViewCell cell_수익률 = view["수익률_미체결", e.RowIndex];
                        Form1.form1.DGV_color(cell_수익률);

                        DataGridViewCell cell_등락률 = view["등락률_미체결", e.RowIndex];
                        Form1.form1.DGV_color(cell_등락률);
                        Form_Outstanding.form.Outstanding_DataGridView["현재가_미체결", e.RowIndex].Style.ForeColor = cell_등락률.Style.ForeColor;
                    }
                    catch
                    {
                        Form1.Error_Log("Outstanding_DataGridView_A  에러:: 메세지 _Grid_CellValueChanged 에러");
                    }
                }
            }
        }

        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)             // 원하는 칼럼에 자동 번호 매기기
        {
            if (sender.Equals(Form_Outstanding.form.Outstanding_DataGridView))
            {
                Form_Outstanding.form.Outstanding_DataGridView.Rows[e.RowIndex].Cells["Num_미체결"].Value = Form_Outstanding.form.Outstanding_DataGridView.Rows.Count - (e.RowIndex + 1) + 1;
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e) // 잔고 선택 체크박스 동작
        {
            this.ActiveControl = null;

            DataGridView view = sender as DataGridView;

            if (e.RowIndex == -1) view.CurrentCell = null;

            if (sender.Equals(Form_Outstanding.form.Outstanding_DataGridView)) // 주문취소 클릭
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (e.ColumnIndex == Form_Outstanding.form.Outstanding_DataGridView.Columns["주문취소_미체결"].Index)
                    {
                        string 주문번호 = Form_Outstanding.form.Outstanding_DataGridView["주문번호_미체결", e.RowIndex].Value.ToString();

                        JumunItem JumunItem = Form1.JumunItem_List.Find(o => o.주문번호.Equals(주문번호));

                        if (JumunItem != null)
                        {
                            if (JumunItem.취소timer > 0 || (JumunItem.취소timer == 0 && JumunItem.반복횟수 > 0))
                            {
                                var thread = new Thread(
                                 () =>
                                 {
                                     Form1.form1.Invoke((MethodInvoker)delegate ()
                                     {
                                         using (new CenterWinDialog(Form1.form1))
                                             if (MessageBox.Show(JumunItem.종목명 + "\n 주문유형: " + GET.주문유형(JumunItem.주문유형) + " 수량: " + JumunItem.미체결량 + "을 '취소' 하시겠습니까 ?", "취소알림", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                             {
                                                 if (JumunItem.검색식.Contains("신규_") && JumunItem.주문수량 == JumunItem.미체결량)
                                                 {
                                                     Properties.Settings.Default.신규횟수--;
                                                     Form1.신규count--;
                                                 }

                                                 JumunItem.비고 = "'취소'버튼클릭";
                                                 JumunItem.반복횟수 = 0;
                                                 JumunItem.취소시간 = 0;
                                                 JumunItem.취소timer = 0;

                                                 Form1.동작_Log(" ");
                                                 Form1.동작_Log("[주문취소] 종목명:" + JumunItem.종목명 + " 주문유형:" + GET.주문유형(JumunItem.주문유형) + " 수량: " + JumunItem.미체결량 + " 주문을 취소 하였습니다.");
                                                 Form1.동작_Log(" ");
                                             }
                                     });
                                 });
                                thread.Start();
                            }
                        }
                    }

                    Order_Reserve.종목선택(Form_Outstanding.form.Outstanding_DataGridView["종목명_미체결", e.RowIndex].Value.ToString());

                    DataGridViewCell cell_color = view[e.ColumnIndex, e.RowIndex];
                    Form_Outstanding.form.Outstanding_DataGridView[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = cell_color.Style.ForeColor;
                }
            }
        }


        private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e) // 데이터그리드뷰 동작 속도 올리기
        {
            if (Form_Outstanding.form.Outstanding_DataGridView.CurrentCell is DataGridViewCheckBoxCell)
            {
                Form_Outstanding.form.Outstanding_DataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

    }
}
