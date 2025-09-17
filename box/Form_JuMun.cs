using System;
using System.Drawing;
using System.Windows.Forms;

namespace 지니_64.box
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
                        else if (text_구분 == "매수")
                        {
                            cell_구분.Style.ForeColor = Color.Red;
                        }
                        else if (text_구분 == "매도")
                        {
                            cell_구분.Style.ForeColor = Color.Blue;
                        }
                        Form_JuMun.form.JuMun_dataGridView["검색식_주문A", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;
                        Form_JuMun.form.JuMun_dataGridView["적용금액_주문A", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;

                        DataGridViewCell cell_수익률 = view["수익률_주문A", e.RowIndex];
                        Form1.form1.DGV_color(cell_수익률);

                        DataGridViewCell cell_등락률 = view["P등락률_주문A", e.RowIndex];
                        Form1.form1.DGV_color(cell_등락률);
                    }
                    catch
                    {
                        Form1.Error_Log("JuMun_dataGridView_A  에러:: 메세지 _Grid_CellValueChanged 에러");
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


        private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e) // 데이터그리드뷰 동작 속도 올리기
        {
            if (Form_JuMun.form.JuMun_dataGridView.CurrentCell is DataGridViewCheckBoxCell)
            {
                Form_JuMun.form.JuMun_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }



    }
}
