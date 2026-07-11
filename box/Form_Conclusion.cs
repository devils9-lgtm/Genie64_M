using System;
using System.Drawing;
using System.Windows.Forms;

namespace 지니64.box
{
    public partial class Form_Conclusion : UserControl
    {
        public static Form_Conclusion form;

        public Form_Conclusion()
        {
            form = this;
            InitializeComponent();
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
                if (sender.Equals(Form_Conclusion.form.Conclusion_DataGridView))
                {
                    try
                    {
                        DataGridViewCell cell_구분 = view["주문유형_체결", e.RowIndex];
                        string text_구분 = cell_구분.FormattedValue.ToString();

                        if (text_구분.Contains("매수"))
                        {
                            cell_구분.Style.ForeColor = Color.Red;
                        }
                        else if (text_구분.Contains("매도"))
                        {
                            cell_구분.Style.ForeColor = Color.Blue;
                        }
                        Conclusion_DataGridView["검색식_체결", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;
                        Conclusion_DataGridView["누적체결량_체결", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;
                        Conclusion_DataGridView["누적금액_체결", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;

                        DataGridViewCell cell_수익률 = view["수익률_체결", e.RowIndex];
                        Form1.form1.DGV_color(cell_수익률);

                        DataGridViewCell cell_등락률 = view["P등락률_체결", e.RowIndex];
                        Form1.form1.DGV_color(cell_등락률);
                        Conclusion_DataGridView["P현재가_체결", e.RowIndex].Style.ForeColor = cell_등락률.Style.ForeColor;
                    }
                    catch
                    {
                        Log.에러기록("Conclusion_DataGridView  에러:: 메세지 _Grid_CellValueChanged 에러");
                    }
                }
            }
        }


        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e) // 잔고 선택 체크박스 동작
        {
            this.ActiveControl = null;

            DataGridView view = sender as DataGridView;

            if (e.RowIndex == -1) view.CurrentCell = null;

            if (sender.Equals(Conclusion_DataGridView))
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    Order_Reserve.종목선택(Conclusion_DataGridView["종목명_체결", e.RowIndex].Value.ToString());
                    DataGridViewCell cell_color = view[e.ColumnIndex, e.RowIndex];
                    Conclusion_DataGridView[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = cell_color.Style.ForeColor;
                }
            }
        }

        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e) // 데이터그리드뷰 동작 속도 올리기
        {
            if (Conclusion_DataGridView.CurrentCell is DataGridViewCheckBoxCell)
            {
                Conclusion_DataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void Conclusion_DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Front_End.DataGridView_CellPainting(sender, e);
        }

        private void Conclusion_DataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            Front_End.DataGridView_MouseDown_(sender, e);
        }
    }
}
