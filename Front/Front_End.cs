using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using 지니64.box;

namespace 지니64
{
    internal class Front_End
    {

        public static void CB_미니시계_CheckedChanged(object sender)
        {
            Form1.form1.Form_Top_Most();
            CheckBox CB = (sender as CheckBox);
            Form1.form1.체크박스_비프(sender);

            if (CB.Checked)
            {
                if (Form1.form1.tab_잔고.SelectedIndex == 2) Form1.form1.tab_잔고.SelectedIndex = 0;
                Form1.form1.panel_form.Controls.Add(Form1.form1.panel_clock);
                Form1.form1.panel_clock.BringToFront();
                Form1.form1.Controls.Remove(Form1.form1.panel_계좌설정);
            }
            else
            {
                Form1.form1.panel_form.Controls.Remove(Form1.form1.panel_clock);
                Form1.form1.panel_form.Controls.Add(Form1.form1.panel_계좌설정);
            }
        }

        public static void CB_종목비공개_CheckedChanged(object sender)
        {
            Form1.form1.Form_Top_Most();

            CheckBox CB = (sender as CheckBox);
            Form1.form1.체크박스_비프(sender);
            if (CB.Checked)
            {
                Form1.form1.JanGo_dataGridView.Controls.Add(Form1.form1.종목감추기_잔고);
                Form_JuMun.form.JuMun_dataGridView.Controls.Add(Form_JuMun.form.종목감추기_주문);
                Form_Outstanding.form.Controls.Add(Form_Outstanding.form.종목감추기_미체결);
                Form_Conclusion.form.Controls.Add(Form_Conclusion.form.종목감추기_체결);
                Form1.form1.LB_Log.Controls.Add(Form1.form1.종목감추기_로그);

                Form1.form1.종목감추기_잔고.BringToFront();
                Form_Outstanding.form.종목감추기_미체결.BringToFront();
                Form_Conclusion.form.종목감추기_체결.BringToFront();
                Form1.form1.종목감추기_로그.BringToFront();
                Form_JuMun.form.종목감추기_주문.BringToFront();

                if (Form1.form1.DGV_통계B.Columns[0].HeaderText.Equals("종목명"))
                {
                    Form1.form1.panel_TP_통계.Controls.Add(Form1.form1.종목감추기6);
                    Form1.form1.종목감추기6.BringToFront();
                }

                if (Form1.form1.CB_검색보기.Checked)
                {
                    Form1.form1.Panel_Search_List.Controls.Add(Form1.form1.종목감추기7);
                    Form1.form1.종목감추기7.BringToFront();
                }
                else
                {
                    Form1.form1.Panel_Search_List.Controls.Remove(Form1.form1.종목감추기7);
                }
            }
            else
            {
                Form1.form1.JanGo_dataGridView.Controls.Remove(Form1.form1.종목감추기_잔고);
                Form_JuMun.form.JuMun_dataGridView.Controls.Remove(Form_JuMun.form.종목감추기_주문);
                Form_Outstanding.form.Controls.Remove(Form_Outstanding.form.종목감추기_미체결);
                Form_Conclusion.form.Controls.Remove(Form_Conclusion.form.종목감추기_체결);
                Form1.form1.LB_Log.Controls.Remove(Form1.form1.종목감추기_로그);

                Form1.form1.panel_TP_통계.Controls.Remove(Form1.form1.종목감추기6);
                Form1.form1.Panel_Search_List.Controls.Remove(Form1.form1.종목감추기7);
            }
        }

        public static void CB_검색보기_CheckedChanged(object sender)
        {
            Form1.form1.Form_Top_Most();
            CheckBox CB = (sender as CheckBox);
            Form1.form1.체크박스_비프(sender);
            if (CB.Checked)
            {
                Form1.form1.panel_지수.Controls.Remove(Form1.form1.panel_지수설정);

                if (Form1.form1.tab_잔고.SelectedIndex == 2) Form1.form1.tab_잔고.SelectedIndex = 0;

                Form1.form1.panel_지수.Controls.Add(Form1.form1.Panel_Search_List);

                if (Form1.form1.CB_종목비공개.Checked)
                {
                    Form1.form1.Panel_Search_List.Controls.Add(Form1.form1.종목감추기7);
                    Form1.form1.종목감추기7.BringToFront();
                }
                else
                {
                    Form1.form1.Panel_Search_List.Controls.Remove(Form1.form1.종목감추기7);
                }

                Form1.form1.CBB_SearchCondition.Items.Add(GenieConfig.CBB_SearchCondition.Trim());
                Form1.form1.CBB_SearchCondition.SelectedItem = GenieConfig.CBB_SearchCondition.Trim();


                if (GenieConfig.지수이평 == "지수이평프린터")
                {
                    Form1.form1.Controls.Remove(Form1.form1.Jisu_print_Form);
                    Form1.FormJisu_print_Open = false;
                }
            }
            else
            {
                Form1.form1.panel_지수.Controls.Remove(Form1.form1.Panel_Search_List);
                Form1.form1.panel_지수.Controls.Add(Form1.form1.panel_지수설정);

                if (GenieConfig.지수이평 == "지수이평프린터") LayoutChange.CB_기능설정_CheckedChanged("지수");
            }
        }

        public static void Tab_잔고_SelectedIndexChanged()
        {
            Form1.form1.CB_기본매매.Checked = false;
            Form1.form1.CB_반복매매.Checked = false;
            Form1.form1.CB_계좌관리.Checked = false;
            Form1.form1.CB_특수매매.Checked = false;
            Form1.form1.CB_대금탐색.Checked = false;
            Form1.form1.CB_매매그룹.Checked = false;
            Form1.form1.CB_기능설정.Checked = false;
            Form1.form1.Controls.Remove(Form1.form1.panel_기능버튼);
            Form1.form1.Controls.Remove(Form1.form1.panel_TP_잔고);
            Form1.form1.Controls.Remove(Form1.form1.panel_TP_관종);
            Form1.form1.Controls.Remove(Form1.form1.panel_TP_통계);
            Form1.form1.Controls.Remove(Form1.form1.Panel_지수연동);
            Form1.form1.Controls.Remove(Form1.form1.panel_form);

            if (Form1.form1.tab_잔고.SelectedIndex == 0)
            {
                Form1.form1.Controls.Add(Form1.form1.panel_form);
                Form1.form1.Controls.Add(Form1.form1.Panel_지수연동);
                Form1.form1.Controls.Add(Form1.form1.panel_기능버튼);
                LayoutChange.CBB_layout_SelectedIndex(GenieConfig.CBB_layout);
                Form1.form1.Panel_지수연동.BringToFront();
                Form1.form1.panel_기능버튼.SendToBack();

                Form1.form1.tab_잔고.TabPages[0].Controls.Add(Form1.form1.panel_TP_잔고);
                Form1.form1.JanGo_dataGridView.BringToFront();

                if (Form1.form1.CB_종목비공개.Checked)
                {
                    Form1.form1.JanGo_dataGridView.Controls.Add(Form1.form1.종목감추기_잔고);
                    Form1.form1.종목감추기_잔고.Location = new Point(207, 19);
                    Form1.form1.종목감추기_잔고.BringToFront();
                }
                else
                {
                    Form1.form1.panel_TP_잔고.Controls.Remove(Form1.form1.종목감추기_잔고);
                }

                Form1.form1.JanGo_dataGridView.SuspendLayout();
                Form1.form1.JanGo_dataGridView.Rows.Clear();
                Method.SortClear(Form1.form1.JanGo_dataGridView);

                List<Stockbalance> 정렬된잔고리스트 = new List<Stockbalance>(Form1.stockBalanceList.Values);
                정렬된잔고리스트.Sort((a, b) => b.초기매수일.CompareTo(a.초기매수일));

                foreach (Stockbalance 잔고 in 정렬된잔고리스트)
                {
                    int rowIndex = Form1.form1.JanGo_dataGridView.Rows.Add();
                    Form1.form1.JanGo_dataGridView.Rows[rowIndex].Cells["코드_잔고"].Value = 잔고.종목코드;
                    홀딩잔고.JangoRow_print(rowIndex, 잔고);
                }
                Form1.form1.JanGo_dataGridView.ClearSelection();
                Form1.form1.JanGo_dataGridView.ResumeLayout();

                if (GenieConfig.CBB_layout == 6)
                {
                    Form1.form1.Label_주문내역.BringToFront();
                    Form1.form1.Label_주문row.BringToFront();
                    Form1.form1.TB_주문row.BringToFront();
                }
            }
            else if (Form1.form1.tab_잔고.SelectedIndex == 1)
            {
                Form1.form1.JanGo_dataGridView.Rows.Clear();
                Method.SortClear(Form1.form1.JanGo_dataGridView);
                Form1.form1.Controls.Add(Form1.form1.panel_form);
                Form1.form1.Controls.Add(Form1.form1.Panel_지수연동);
                Form1.form1.Controls.Add(Form1.form1.panel_기능버튼);
                LayoutChange.CBB_layout_SelectedIndex(GenieConfig.CBB_layout);
                Form1.form1.Panel_지수연동.BringToFront();
                Form1.form1.panel_기능버튼.SendToBack();


                Form1.form1.tab_잔고.TabPages[1].Controls.Add(Form1.form1.panel_TP_관종);
                Form1.form1.panel_TP_관종.BringToFront();
                Tab_InterestGroup.CBB_실시간n그룹n관심자동_indexchange(1);
            }
            else if (Form1.form1.tab_잔고.SelectedIndex == 2)
            {
                //Form1.form1.DGV_통계.Show();
                //Form1.form1.DGV_통계B.Show();
                Form1.form1.JanGo_dataGridView.Rows.Clear();
                Method.SortClear(Form1.form1.JanGo_dataGridView);
                Form1.form1.tab_잔고.TabPages[2].Controls.Add(Form1.form1.panel_TP_통계);

                Form1.form1.chart_Month.Series[0].Points.Clear();
                Form1.form1.chart_Month.Series[1].Points.Clear();
                Form1.form1.chart_Day.Series[0].Points.Clear();
                Form1.form1.chart_Day.Series[1].Points.Clear();
                Form1.form1.DGV_통계.Rows.Clear();
                Method.SortClear(Form1.form1.DGV_통계);
                Form1.form1.DGV_통계B.Rows.Clear();
                Method.SortClear(Form1.form1.DGV_통계B);

                Form1.form1.panel_tab_잔고.Location = new Point(-1, 0);
                Form1.form1.panel_tab_잔고.Size = new Size(1922, 1020);
                Form1.form1.tab_잔고.Location = new Point(-5, -3);
                Form1.form1.tab_잔고.Size = new Size(1930, 1026);
                Form1.form1.panel_TP_통계.Size = new Size(1930, 1026);

                Form1.form1.panel_tab_잔고.BringToFront();
                Form1.form1.chart_Month.Show();
                Form1.form1.chart_Day.Show();

                Form1.form1.chart_Day.Location = new Point(1102, 5);
                Form1.form1.chart_Day.Size = new Size(826, 500);
                Form1.form1.chart_Month.Location = new Point(1102, 497);
                Form1.form1.chart_Month.Size = new Size(826, 500);

                Form1.form1.chart_Month.BringToFront();
                Form1.form1.chart_Day.BringToFront();

                Form1.form1.panel_TP_통계.Controls.Remove(Form1.form1.종목감추기6);

                if (Form1.form1.CB_종목비공개.Checked)
                {
                    if (Form1.form1.DGV_통계B.Columns[0].HeaderText.Equals("종목명"))
                    {
                        Form1.form1.panel_TP_통계.Controls.Add(Form1.form1.종목감추기6);
                        Form1.form1.종목감추기6.BringToFront();
                    }
                }
            }
        }

        public static void Tab_주문_SelectedIndexChanged()
        {
            Form1.form1.panel_tab_주문.Controls.Remove(Form1.form1.Label_주문row);
            Form1.form1.panel_tab_주문.Controls.Remove(Form1.form1.TB_주문row);
            Form1.form1.panel_tab_주문.Controls.Remove(Form1.form1.CBB_최종가종목);
            Form1.form1.panel_tab_주문.Controls.Remove(Form1.form1.BT_종목업);
            Form1.form1.panel_tab_주문.Controls.Remove(Form1.form1.BT_종목다운);

            if (!Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("주문 내역")) Form1.form1.Controls.Remove(Form_JuMun.form);
            if (!Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("ERROG_LOG")) Form1.form1.Controls.Remove(Form1.form1.LB_error);
            if (!Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("동작/감시 보기")) Form1.form1.Controls.Remove(Form1.form1.LB_JumunList);
            if (!Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("동작/감시 보기")) Form1.form1.Controls.Remove(Form1.form1.DGV_최종매입가View);

            if (!Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST A & B") && !Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST A") && !Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST")) { Form1.form1.Controls.Remove(Form1.form1.panel_TEST_A); Form1.form1.dataGridView_watch_A.Rows.Clear(); Method.SortClear(Form1.form1.dataGridView_watch_A); }
            ;
            if (!Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST A & B") && !Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST B") && !Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST")) { Form1.form1.Controls.Remove(Form1.form1.panel_TEST_B); Form1.form1.dataGridView_watch_B.Rows.Clear(); Method.SortClear(Form1.form1.dataGridView_watch_B); }
            ;
            if (!Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST C & D") && !Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST C") && !Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST")) { Form1.form1.Controls.Remove(Form1.form1.panel_TEST_C); Form1.form1.dataGridView_watch_C.Rows.Clear(); Method.SortClear(Form1.form1.dataGridView_watch_C); }
            ;
            if (!Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST C & D") && !Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST D") && !Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST")) { Form1.form1.Controls.Remove(Form1.form1.panel_TEST_D); Form1.form1.dataGridView_watch_D.Rows.Clear(); Method.SortClear(Form1.form1.dataGridView_watch_D); }
            ;

            if (Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("주문 내역"))
            {
                Form1.form1.panel_tab_주문.Controls.Add(Form1.form1.Label_주문row);
                Form1.form1.panel_tab_주문.Controls.Add(Form1.form1.TB_주문row);

                Form1.form1.Label_주문row.BringToFront();
                Form1.form1.TB_주문row.BringToFront();
            }
            else if (Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("ERROG_LOG"))
            {
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.LB_error);
                Form1.form1.LB_error.BringToFront();
            }
            else if (Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("동작/감시 보기"))
            {
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.LB_JumunList);
                Form1.form1.LB_JumunList.BringToFront();
            }
            else if (Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST A & B"))
            {
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.panel_TEST_A);
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.panel_TEST_B);
            }
            else if (Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST C & D"))
            {
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.panel_TEST_C);
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.panel_TEST_D);
            }
            else if (Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST A"))
            {
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.panel_TEST_A);
            }
            else if (Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST B"))
            {
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.panel_TEST_B);
            }
            else if (Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST C"))
            {
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.panel_TEST_C);
            }
            else if (Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST D"))
            {
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.panel_TEST_D);
            }
            else if (Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("조건식TEST"))
            {
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.panel_TEST_A);
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.panel_TEST_B);
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.panel_TEST_C);
                Form1.form1.tab_주문.SelectedTab.Controls.Add(Form1.form1.panel_TEST_D);
            }

            if (Form1.form1.tab_주문.SelectedTab.Text.ToString().Equals("동작/감시 보기"))
            {
                if (!Form1.form1.동작상태보기)
                {
                    Form1.form1.동작상태보기 = true;
                    Form1.form1.동작실시간 = true;

                    Form1.form1.LB_JumunList.SelectionMode = SelectionMode.None;
                }
            }
            else
            {
                Form1.form1.동작상태보기 = false;
                Form1.form1.LB_JumunList.Items.Clear();

                Form1.form1.LB_JumunList.SelectionMode = SelectionMode.MultiExtended;
            }
        }

        public static void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (e.State.HasFlag(DataGridViewElementStates.Selected))
                {
                    using (SolidBrush hotPinkBrush = new SolidBrush(Color.LightGreen)) // 핫핑크 대신 Color.DeepPink 사용
                    {
                        e.Graphics.FillRectangle(hotPinkBrush, e.CellBounds);
                    }
                }
                else
                {
                    e.PaintBackground(e.ClipBounds, e.RowIndex == -1); // 헤더가 아닌 일반 셀이므로 false
                }

                e.Paint(e.CellBounds, DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.Border);

                if ((e.PaintParts & DataGridViewPaintParts.Focus) == DataGridViewPaintParts.Focus)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.Focus);
                }

                e.Handled = true; // 이벤트 처리 완료
                return;
            }
        }

        public static void DataGridView_MouseDown_(object sender, MouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            DataGridView.HitTestInfo hit = dgv.HitTest(e.X, e.Y);

            if (hit.Type == DataGridViewHitTestType.None)
            {
                if (dgv.CurrentCell != null)
                {
                    dgv.CurrentCell = null; // 현재 셀 선택 해제
                }

                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    row.Selected = false;
                }

                int rowIndex = GetBottomEmptyRowIndex(dgv, e.Location);
                if (rowIndex == dgv.Rows.Count)
                {
                    // 마지막 행 아래 빈 공간을 클릭했을 때의 추가 로직 (필요하다면 사용)
                    // MessageBox.Show($"마지막 행 아래 빈 공간 클릭! 추정된 Row Index: {rowIndex}");
                }
            }
            else if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
            {
                // 실제 데이터 행을 클릭했을 경우
                //  int rowIndex = hit.RowIndex;
                // ... (필요한 경우 로직 추가)
            }
        }

        private static int GetBottomEmptyRowIndex(DataGridView dgv, Point point)
        {
            if (dgv.Rows.Count == 0)
            {
                return -1;
            }

            int totalHeight = 0;

            int y = point.Y;

            if (dgv.FirstDisplayedScrollingRowIndex >= 0)
            {
                for (int i = 0; i < dgv.FirstDisplayedScrollingRowIndex; i++)
                {
                    totalHeight += dgv.Rows[i].Height;
                }

                y -= dgv.GetCellDisplayRectangle(0, dgv.FirstDisplayedScrollingRowIndex, false).Y;
            }

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                totalHeight += dgv.Rows[i].Height;

                if (y <= totalHeight)
                {
                    return -1;
                }
            }

            return dgv.Rows.Count;
        }
    }
}
