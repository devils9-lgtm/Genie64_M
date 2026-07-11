using System;
using System.Drawing;
using System.Windows.Forms;

namespace 지니64.box
{
    public partial class Form_JuMun : UserControl
    {
        public static Form_JuMun form;

        public Form_JuMun()
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
                if (sender.Equals(Form_JuMun.form.JuMun_dataGridView))
                {
                    try
                    {
                        DataGridViewCell cell_구분 = view["주문유형_주문A", e.RowIndex];
                        string text_구분 = cell_구분.FormattedValue.ToString();

                        if (text_구분 == "익절")
                        {
                            cell_구분.Style.ForeColor = Color.Red;
                        }
                        else if (text_구분 == "보전")
                        {
                            cell_구분.Style.ForeColor = Color.Green;
                        }
                        else if (text_구분 == "매수" || text_구분 == "매수신용")
                        {
                            cell_구분.Style.ForeColor = Color.Red;
                        }
                        else if (text_구분 == "매도" || text_구분 == "매도신용")
                        {
                            cell_구분.Style.ForeColor = Color.Blue;
                        }
                        form.JuMun_dataGridView["검색식_주문A", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;
                        form.JuMun_dataGridView["적용금액_주문A", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;


                        DataGridViewCell cell_수익률 = view["수익률_주문A", e.RowIndex];
                        Form1.form1.DGV_color(cell_수익률);

                        //form.JuMun_dataGridView["검색식_주문A", e.RowIndex].Style.ForeColor = cell_수익률.Style.ForeColor;
                        //form.JuMun_dataGridView["적용금액_주문A", e.RowIndex].Style.ForeColor = cell_수익률.Style.ForeColor;
                        //form.JuMun_dataGridView["종목명_주문A", e.RowIndex].Style.ForeColor = cell_수익률.Style.ForeColor;


                        DataGridViewCell cell_등락률 = view["P등락률_주문A", e.RowIndex];
                        Form1.form1.DGV_color(cell_등락률);

                        if (text_구분.Contains("취소"))
                        {
                            cell_구분.Style.ForeColor = Color.Black;

                            form.JuMun_dataGridView["검색식_주문A", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;
                            form.JuMun_dataGridView["적용금액_주문A", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;
                            form.JuMun_dataGridView["종목명_주문A", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;
                            form.JuMun_dataGridView["P등락률_주문A", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;
                            form.JuMun_dataGridView["수익률_주문A", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;
                        }
                    }
                    catch
                    {
                        Log.에러기록("JuMun_dataGridView_A  에러:: 메세지 _Grid_CellValueChanged 에러");
                    }
                }

            }
        }



        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e) // 잔고 선택 체크박스 동작
        {
            this.ActiveControl = null;

            DataGridView view = sender as DataGridView;

            if (e.RowIndex == -1) view.CurrentCell = null;

            if (sender.Equals(Form_JuMun.form.JuMun_dataGridView))
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    Order_Reserve.종목선택(Form_JuMun.form.JuMun_dataGridView["종목명_주문A", e.RowIndex].Value.ToString());
                    DataGridViewCell cell_color = view[e.ColumnIndex, e.RowIndex];
                    Form_JuMun.form.JuMun_dataGridView[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = cell_color.Style.ForeColor;
                }
            }

        }


        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e) // 데이터그리드뷰 동작 속도 올리기
        {
            if (Form_JuMun.form.JuMun_dataGridView.CurrentCell is DataGridViewCheckBoxCell)
            {
                Form_JuMun.form.JuMun_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void JuMun_DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Front_End.DataGridView_CellPainting(sender, e);
        }

        private void JuMun_dataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            Front_End.DataGridView_MouseDown_(sender, e);
        }
    }
}
