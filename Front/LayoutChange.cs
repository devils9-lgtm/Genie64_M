using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using 지니64.box;
using 지니64.RESTAPI;
using static System.Net.Mime.MediaTypeNames;

namespace 지니64
{
    public class LayoutChange
    {
        public static void CBB_layout_SelectedIndexChanged(object sender)
        {
            Form1.form1.CB_기본매매.Checked = false;
            Form1.form1.CB_반복매매.Checked = false;
            Form1.form1.CB_계좌관리.Checked = false;
            Form1.form1.CB_특수매매.Checked = false;
            Form1.form1.CB_대금탐색.Checked = false;
            Form1.form1.CB_매매그룹.Checked = false;
            Form1.form1.CB_기능설정.Checked = false;
            if (Form1.form1.tab_잔고.SelectedIndex == 2) Form1.form1.tab_잔고.SelectedIndex = 0;

            if ((sender as ComboBox) != null)
                CBB_layout_SelectedIndex((sender as ComboBox).SelectedIndex);

            int index = Form1.form1.CBB_layout.SelectedIndex;
            GenieConfig.CBB_layout = index;

            if (index == 3 || index == 4)
            {
                Form1.form1.TB_WatchRow_A.Text = "9";
                Form1.form1.TB_WatchRow_B.Text = "9";
                Form1.form1.TB_WatchRow_C.Text = "9";
                Form1.form1.TB_WatchRow_D.Text = "9";
            }
            else if (index == 6)
            {
                Form1.form1.TB_WatchRow_A.Text = "14";
                Form1.form1.TB_WatchRow_B.Text = "13";
                Form1.form1.TB_WatchRow_C.Text = "14";
                Form1.form1.TB_WatchRow_D.Text = "13";
            }
            else
            {
                Form1.form1.TB_WatchRow_A.Text = "12";
                Form1.form1.TB_WatchRow_B.Text = "12";
                Form1.form1.TB_WatchRow_C.Text = "12";
                Form1.form1.TB_WatchRow_D.Text = "12";
            }
        }


        public static void CBB_layout_SelectedIndex(int index)
        {
            Form1.form1.CBB_layout.SendToBack();
            Form1.form1.panel_tab_주문.BringToFront();

            if (Form1.form1.tab_체결.TabPages.Count == 1)
            {
                TabPage TabPage = new TabPage("동작_Log");
                Form1.form1.tab_체결.TabPages.Add(TabPage);

                Form1.form1.tab_체결.TabPages[1].Controls.Add(Form1.form1.LB_Log);
                Form1.form1.tab_체결.TabPages[1].BackColor = SystemColors.ControlLight;
                Form1.form1.tab_체결.TabPages[1].BorderStyle = BorderStyle.FixedSingle;

                Form1.form1.LB_Log.Location = new Point(0, 2);
                Form1.form1.LB_Log.Size = new Size(600, 170);
                Form1.form1.LB_Log.BringToFront();
                Form1.form1.LB_Log.BorderStyle = BorderStyle.None;
            }

            if (index == 6)
            {
                if (Form1.form1.tab_주문.TabPages[0].ToString().Contains("주문 내역")) Form1.form1.tab_주문.TabPages.RemoveAt(0);

                Form1.form1.panel_TP_잔고.Controls.Add(Form_JuMun.form.JuMun_dataGridView);
                Form1.form1.panel_TP_잔고.Controls.Add(Form_JuMun.form.종목감추기_주문);
                if (Form1.form1.CB_종목비공개.Checked) Form_JuMun.form.종목감추기_주문.BringToFront();

                Form1.form1.panel_TP_잔고.Controls.Add(Form1.form1.Label_주문내역);
                Form1.form1.panel_TP_잔고.Controls.Add(Form1.form1.Label_주문row);
                Form1.form1.panel_TP_잔고.Controls.Add(Form1.form1.TB_주문row);

                Form1.form1.Label_주문내역.Location = new Point(-1, 286);
                Form1.form1.Label_주문row.Location = new Point(88, 286);
                Form1.form1.TB_주문row.Location = new Point(177, 286);

                Form1.form1.Label_주문내역.BringToFront();
                Form1.form1.Label_주문row.BringToFront();
                Form1.form1.TB_주문row.BringToFront();
            }
            else
            {
                Form1.form1.panel_TP_잔고.Controls.Remove(Form1.form1.Label_주문row);
                Form1.form1.panel_TP_잔고.Controls.Remove(Form1.form1.TB_주문row);
                Form1.form1.panel_TP_잔고.Controls.Remove(Form1.form1.Label_주문내역);
                Form1.form1.panel_TP_잔고.Controls.Remove(Form_JuMun.form.종목감추기_주문);

                if (!Form1.form1.tab_주문.TabPages[0].ToString().Contains("주문 내역"))
                {
                    TabPage myTabPage = new TabPage("주문 내역");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Insert(0, myTabPage);
                }

                Form1.form1.tab_주문.TabPages[0].Controls.Add(Form_JuMun.form.JuMun_dataGridView);
                Form_JuMun.form.JuMun_dataGridView.BringToFront();
                if (Form1.form1.CB_종목비공개.Checked)
                {
                    Form_JuMun.form.JuMun_dataGridView.Controls.Add(Form_JuMun.form.종목감추기_주문);
                    Form_JuMun.form.종목감추기_주문.BringToFront();
                }
            }

            if (Form1.form1.CB_종목비공개.Checked) Form_JuMun.form.종목감추기_주문.BringToFront();

            if (index == -1)
            {
                Form1.form1.panel_TP_잔고.Size = new Size(1103, 538);
                Form1.form1.panel_tab_잔고.Location = new Point(-1, 139);
                Form1.form1.panel_tab_잔고.Size = new Size(1103, 538);
                Form1.form1.tab_잔고.Location = new Point(-5, -3);
                Form1.form1.tab_잔고.Size = new Size(1111, 544);
                Form1.form1.JanGo_dataGridView.Location = new Point(-1, -1); //new Point(-3, -2);
                Form1.form1.JanGo_dataGridView.Size = new Size(1106, 520);
                Form1.form1.종목감추기_잔고.Location = new Point(207, 19);
                Form1.form1.종목감추기_잔고.Size = new Size(111, 484);
                Form1.form1.LB_검색결과n관심리스트.Size = new Size(235, 498);
                Form1.form1.LB_관심_A.Size = new Size(234, 498);
                Form1.form1.LB_관심_B.Size = new Size(234, 498);
                Form1.form1.LB_관심_C.Size = new Size(235, 498);

                //주문패널
                Form1.form1.panel_tab_주문.Location = new Point(1101, 20);
                Form1.form1.panel_tab_주문.Size = new Size(820, 333);
                Form1.form1.tab_주문.Location = new Point(-5, -3);
                Form1.form1.tab_주문.Size = new Size(829, 339);
                Form1.form1.LB_JumunList.Location = new Point(-1, 1);
                Form1.form1.LB_JumunList.Size = new Size(820, 317);
                Form1.form1.LB_error.Location = new Point(-1, 1);
                Form1.form1.LB_error.Size = new Size(820, 317);
                Form_JuMun.form.Size = new Size(823, 315);
                Form_JuMun.form.JuMun_dataGridView.Location = new Point(-2, -2);
                Form_JuMun.form.JuMun_dataGridView.Size = new Size(823, 315);
                Form_JuMun.form.종목감추기_주문.Location = new Point(148, 17);
                Form_JuMun.form.종목감추기_주문.Size = new Size(96, 279);
                Form1.form1.Label_주문row.Location = new Point(586, -1);
                Form1.form1.TB_주문row.Location = new Point(675, -1);

                //미체결패널
                Form_Outstanding.form.Size = new Size(441, 325);
                Form_Outstanding.form.Location = new Point(1101, 352);
                Form_Outstanding.form.Outstanding_DataGridView.Location = new Point(-2, 19);
                Form_Outstanding.form.Outstanding_DataGridView.Size = new Size(443, 306);
                Form_Outstanding.form.종목감추기_미체결.Location = new Point(186, 38);
                Form_Outstanding.form.종목감추기_미체결.Size = new Size(101, 270);
                Form_Outstanding.form.label_미체결내역.Location = new Point(-2, -1);
                Form_Outstanding.form.L_미체결row.Location = new Point(107, -1);
                Form_Outstanding.form.TB_미체결row.Location = new Point(198, -1);
                Form_Outstanding.form.LB_미체결주문.Location = new Point(242, -1);

                //체결패널
                Form1.form1.panel_체결.Size = new Size(380, 326);
                Form1.form1.panel_체결.Location = new Point(1541, 351);
                Form1.form1.tab_체결.Size = new Size(392, 331);
                Form1.form1.tab_체결.Location = new Point(-5, -2);

                Form_Conclusion.form.Size = new Size(380, 306);
                Form_Conclusion.form.Conclusion_DataGridView.Size = new Size(380, 306);
                Form_Conclusion.form.Conclusion_DataGridView.Location = new Point(-2, -1);
                Form_Conclusion.form.종목감추기_체결.Size = new Size(96, 270);
                Form_Conclusion.form.종목감추기_체결.Location = new Point(153, 18);
                Form1.form1.LB_Log.Size = new Size(378, 320);
                Form1.form1.LB_Log.Location = new Point(0, 1);
                Form1.form1.종목감추기_로그.Size = new Size(118, 320);
                Form1.form1.종목감추기_로그.Location = new Point(135, -1);
                Form1.form1.Label_체결row.Location = new Point(153, 0);
                Form1.form1.TB_체결row.Location = new Point(242, 0);

                if (Form1.form1.tab_주문.TabPages.Count < 6)
                {
                    for (int i = Form1.form1.tab_주문.TabPages.Count; i > 3; i--)
                    {
                        Form1.form1.tab_주문.TabPages.RemoveAt(Form1.form1.tab_주문.TabPages.Count - 1);
                    }

                    TabPage myTabPage = new TabPage("조건식TEST A");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST B");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST C");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST D");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);
                }

                Form1.form1.panel_TEST_B.Location = new Point(-1, -1);
                Form1.form1.panel_TEST_C.Location = new Point(-1, -1);
                Form1.form1.panel_TEST_D.Location = new Point(-1, -1);

                Form1.form1.panel_TEST_A.Size = new Size(821, 314);
                Form1.form1.panel_TEST_B.Size = new Size(821, 314);
                Form1.form1.panel_TEST_C.Size = new Size(821, 314);
                Form1.form1.panel_TEST_D.Size = new Size(821, 314);

                Form1.form1.dataGridView_watch_A.Size = new Size(826, 274);
                Form1.form1.dataGridView_watch_B.Size = new Size(826, 274);
                Form1.form1.dataGridView_watch_C.Size = new Size(826, 274);
                Form1.form1.dataGridView_watch_D.Size = new Size(826, 274);
            }

            if (index == 0)
            {
                Form1.form1.TB_주문row.Text = "26";
                Form_Outstanding.form.TB_미체결row.Text = "12";
                Form1.form1.TB_체결row.Text = "12";

                //잔고패널
                Form1.form1.panel_TP_잔고.Size = new Size(1103, 538);
                Form1.form1.panel_tab_잔고.Location = new Point(-1, 139);
                Form1.form1.panel_tab_잔고.Size = new Size(1103, 538);
                Form1.form1.tab_잔고.Location = new Point(-5, -3);
                Form1.form1.tab_잔고.Size = new Size(1111, 544);
                Form1.form1.JanGo_dataGridView.Location = new Point(-1, -1); //new Point(-3, -2);
                Form1.form1.JanGo_dataGridView.Size = new Size(1106, 520);
                Form1.form1.종목감추기_잔고.Location = new Point(207, 19);
                Form1.form1.종목감추기_잔고.Size = new Size(111, 484);
                Form1.form1.LB_검색결과n관심리스트.Size = new Size(235, 498);
                Form1.form1.LB_관심_A.Size = new Size(234, 498);
                Form1.form1.LB_관심_B.Size = new Size(234, 498);
                Form1.form1.LB_관심_C.Size = new Size(235, 498);

                //미체결패널
                Form_Outstanding.form.Location = new Point(-2, 676);
                Form_Outstanding.form.Size = new Size(1104, 343);
                Form_Outstanding.form.Outstanding_DataGridView.Location = new Point(-1, 19);
                Form_Outstanding.form.Outstanding_DataGridView.Size = new Size(1103, 321);
                Form_Outstanding.form.종목감추기_미체결.Location = new Point(186, 38);
                Form_Outstanding.form.종목감추기_미체결.Size = new Size(101, 286);
                Form_Outstanding.form.label_미체결내역.Location = new Point(-1, -1);
                Form_Outstanding.form.L_미체결row.Location = new Point(108, -1);
                Form_Outstanding.form.TB_미체결row.Location = new Point(199, -1);
                Form_Outstanding.form.LB_미체결주문.Location = new Point(243, -1);

                //주문패널
                Form1.form1.panel_tab_주문.Location = new Point(1101, 20);
                Form1.form1.panel_tab_주문.Size = new Size(820, 657);
                Form1.form1.tab_주문.Location = new Point(-5, -3);
                Form1.form1.tab_주문.Size = new Size(828, 663);
                Form1.form1.LB_JumunList.Location = new Point(-1, 1);
                Form1.form1.LB_JumunList.Size = new Size(820, 640);
                Form1.form1.LB_error.Location = new Point(-1, 1);
                Form1.form1.LB_error.Size = new Size(820, 640);
                Form_JuMun.form.Size = new Size(823, 639);
                Form_JuMun.form.JuMun_dataGridView.Location = new Point(-2, -2);
                Form_JuMun.form.JuMun_dataGridView.Size = new Size(823, 639);
                Form_JuMun.form.종목감추기_주문.Location = new Point(150, 19);
                Form_JuMun.form.종목감추기_주문.Size = new Size(96, 603);
                Form1.form1.Label_주문row.Location = new Point(463, -1);
                Form1.form1.TB_주문row.Location = new Point(552, -1);
                Form1.form1.DGV_최종매입가View.Size = new Size(825, 641);

                //체결패널
                Form1.form1.panel_체결.Location = new Point(1101, 676);
                Form1.form1.panel_체결.Size = new Size(820, 343);
                Form1.form1.tab_체결.Location = new Point(-5, -3);
                Form1.form1.tab_체결.Size = new Size(828, 664);
                Form_Conclusion.form.Size = new Size(822, 321);
                Form_Conclusion.form.Conclusion_DataGridView.Location = new Point(-2, -1);
                Form_Conclusion.form.Conclusion_DataGridView.Size = new Size(822, 321);
                Form_Conclusion.form.종목감추기_체결.Location = new Point(153, 18);
                Form_Conclusion.form.종목감추기_체결.Size = new Size(96, 285);
                Form1.form1.LB_Log.Location = new Point(0, 1);
                Form1.form1.LB_Log.Size = new Size(821, 331);
                Form1.form1.종목감추기_로그.Location = new Point(135, -1);
                Form1.form1.종목감추기_로그.Size = new Size(118, 331);
                Form1.form1.Label_체결row.Location = new Point(153, -1);
                Form1.form1.TB_체결row.Location = new Point(242, -1);

                if (Form1.form1.tab_주문.TabPages.Count != 5)
                {
                    for (int i = Form1.form1.tab_주문.TabPages.Count; i > 3; i--)
                    {
                        Form1.form1.tab_주문.TabPages.RemoveAt(Form1.form1.tab_주문.TabPages.Count - 1);
                    }

                    TabPage myTabPage = new TabPage("조건식TEST A & B");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST C & D");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);
                }

                Form1.form1.panel_TEST_B.Location = new Point(-1, 316);
                Form1.form1.panel_TEST_C.Location = new Point(-1, -1);
                Form1.form1.panel_TEST_D.Location = new Point(-1, 316);

                Form1.form1.panel_TEST_A.Size = new Size(821, 318);
                Form1.form1.panel_TEST_B.Size = new Size(821, 321);
                Form1.form1.panel_TEST_C.Size = new Size(821, 318);
                Form1.form1.panel_TEST_D.Size = new Size(821, 321);

                Form1.form1.dataGridView_watch_A.Size = new Size(823, 279);
                Form1.form1.dataGridView_watch_B.Size = new Size(823, 281);
                Form1.form1.dataGridView_watch_C.Size = new Size(823, 279);
                Form1.form1.dataGridView_watch_D.Size = new Size(823, 281);
            }
            else if (index == 1)
            {
                Form1.form1.TB_주문row.Text = "26";
                Form_Outstanding.form.TB_미체결row.Text = "12";
                Form1.form1.TB_체결row.Text = "12";

                //잔고패널
                Form1.form1.panel_TP_잔고.Size = new Size(1103, 879);
                Form1.form1.panel_tab_잔고.Location = new Point(-1, 139);
                Form1.form1.panel_tab_잔고.Size = new Size(1103, 879);
                Form1.form1.tab_잔고.Location = new Point(-5, -3);
                Form1.form1.tab_잔고.Size = new Size(1111, 885);
                Form1.form1.JanGo_dataGridView.Location = new Point(-1, -1); //new Point(-3, -2);
                Form1.form1.JanGo_dataGridView.Size = new Size(1106, 859);
                Form1.form1.종목감추기_잔고.Location = new Point(207, 19);
                Form1.form1.종목감추기_잔고.Size = new Size(111, 823);
                Form1.form1.LB_검색결과n관심리스트.Size = new Size(235, 838);
                Form1.form1.LB_관심_A.Size = new Size(234, 838);
                Form1.form1.LB_관심_B.Size = new Size(234, 838);
                Form1.form1.LB_관심_C.Size = new Size(235, 838);

                //미체결패널
                Form_Outstanding.form.Location = new Point(1101, 676);
                Form_Outstanding.form.Size = new Size(442, 342);
                Form_Outstanding.form.Outstanding_DataGridView.Location = new Point(-2, 19);
                Form_Outstanding.form.Outstanding_DataGridView.Size = new Size(445, 321);
                Form_Outstanding.form.종목감추기_미체결.Location = new Point(186, 38);
                Form_Outstanding.form.종목감추기_미체결.Size = new Size(101, 285);
                Form_Outstanding.form.label_미체결내역.Location = new Point(-2, -1);
                Form_Outstanding.form.L_미체결row.Location = new Point(107, -1);
                Form_Outstanding.form.TB_미체결row.Location = new Point(198, -1);
                Form_Outstanding.form.LB_미체결주문.Location = new Point(242, -1);

                //주문패널
                Form1.form1.panel_tab_주문.Location = new Point(1101, 20);
                Form1.form1.panel_tab_주문.Size = new Size(820, 657);
                Form1.form1.tab_주문.Location = new Point(-5, -3);
                Form1.form1.tab_주문.Size = new Size(828, 663);
                Form1.form1.LB_JumunList.Location = new Point(-1, 1);
                Form1.form1.LB_JumunList.Size = new Size(820, 640);
                Form1.form1.LB_error.Location = new Point(-1, 1);
                Form1.form1.LB_error.Size = new Size(820, 640);
                Form_JuMun.form.Size = new Size(823, 639);
                Form_JuMun.form.JuMun_dataGridView.Location = new Point(-2, -2);
                Form_JuMun.form.JuMun_dataGridView.Size = new Size(823, 639);
                Form_JuMun.form.종목감추기_주문.Location = new Point(150, 19);
                Form_JuMun.form.종목감추기_주문.Size = new Size(96, 603);
                Form1.form1.Label_주문row.Location = new Point(463, -1);
                Form1.form1.TB_주문row.Location = new Point(552, -1);

                //체결패널
                Form1.form1.panel_체결.Location = new Point(1541, 675);
                Form1.form1.panel_체결.Size = new Size(380, 343);
                Form1.form1.tab_체결.Location = new Point(-5, -2);
                Form1.form1.tab_체결.Size = new Size(392, 348);
                Form_Conclusion.form.Size = new Size(380, 321);
                Form_Conclusion.form.Conclusion_DataGridView.Location = new Point(-2, -1);
                Form_Conclusion.form.Conclusion_DataGridView.Size = new Size(380, 321);
                Form_Conclusion.form.종목감추기_체결.Location = new Point(153, 18);
                Form_Conclusion.form.종목감추기_체결.Size = new Size(96, 285);
                Form1.form1.LB_Log.Location = new Point(0, 1);
                Form1.form1.LB_Log.Size = new Size(380, 332);
                Form1.form1.종목감추기_로그.Location = new Point(135, -1);
                Form1.form1.종목감추기_로그.Size = new Size(118, 332);
                Form1.form1.Label_체결row.Location = new Point(153, 0);
                Form1.form1.TB_체결row.Location = new Point(242, 0);


                if (Form1.form1.tab_주문.TabPages.Count != 5)
                {
                    for (int i = Form1.form1.tab_주문.TabPages.Count; i > 3; i--)
                    {
                        Form1.form1.tab_주문.TabPages.RemoveAt(Form1.form1.tab_주문.TabPages.Count - 1);
                    }

                    TabPage myTabPage = new TabPage("조건식TEST A & B");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST C & D");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);
                }

                Form1.form1.panel_TEST_B.Location = new Point(-1, 316);
                Form1.form1.panel_TEST_C.Location = new Point(-1, -1);
                Form1.form1.panel_TEST_D.Location = new Point(-1, 316);

                Form1.form1.panel_TEST_A.Size = new Size(821, 318);
                Form1.form1.panel_TEST_B.Size = new Size(821, 321);
                Form1.form1.panel_TEST_C.Size = new Size(821, 318);
                Form1.form1.panel_TEST_D.Size = new Size(821, 321);

                Form1.form1.dataGridView_watch_A.Size = new Size(823, 279);
                Form1.form1.dataGridView_watch_B.Size = new Size(823, 281);
                Form1.form1.dataGridView_watch_C.Size = new Size(823, 279);
                Form1.form1.dataGridView_watch_D.Size = new Size(823, 281);
            }
            else if (index == 2)
            {
                Form1.form1.TB_주문row.Text = "12";
                Form_Outstanding.form.TB_미체결row.Text = "12";
                Form1.form1.TB_체결row.Text = "12";

                //잔고패널
                Form1.form1.panel_TP_잔고.Size = new Size(1103, 879);
                Form1.form1.panel_tab_잔고.Location = new Point(-1, 139);
                Form1.form1.panel_tab_잔고.Size = new Size(1103, 879);
                Form1.form1.tab_잔고.Location = new Point(-5, -3);
                Form1.form1.tab_잔고.Size = new Size(1111, 888);
                Form1.form1.JanGo_dataGridView.Location = new Point(-1, -1); //new Point(-3, -2);
                Form1.form1.JanGo_dataGridView.Size = new Size(1106, 859);
                Form1.form1.종목감추기_잔고.Location = new Point(207, 19);
                Form1.form1.종목감추기_잔고.Size = new Size(111, 823);
                Form1.form1.LB_검색결과n관심리스트.Size = new Size(235, 838);
                Form1.form1.LB_관심_A.Size = new Size(234, 838);
                Form1.form1.LB_관심_B.Size = new Size(234, 838);
                Form1.form1.LB_관심_C.Size = new Size(235, 838);

                //미체결패널
                Form_Outstanding.form.Location = new Point(1101, 352);
                Form_Outstanding.form.Size = new Size(820, 332);
                Form_Outstanding.form.Outstanding_DataGridView.Location = new Point(-2, 19);
                Form_Outstanding.form.Outstanding_DataGridView.Size = new Size(823, 314);
                Form_Outstanding.form.종목감추기_미체결.Location = new Point(186, 38);
                Form_Outstanding.form.종목감추기_미체결.Size = new Size(101, 278);
                Form_Outstanding.form.label_미체결내역.Location = new Point(-2, -1);
                Form_Outstanding.form.L_미체결row.Location = new Point(107, -1);
                Form_Outstanding.form.TB_미체결row.Location = new Point(198, -1);
                Form_Outstanding.form.LB_미체결주문.Location = new Point(242, -1);
                Form_Outstanding.form.BringToFront();

                //주문패널
                Form1.form1.panel_tab_주문.Location = new Point(1101, 20);
                Form1.form1.panel_tab_주문.Size = new Size(820, 333);
                Form1.form1.tab_주문.Location = new Point(-5, -3);
                Form1.form1.tab_주문.Size = new Size(829, 339);
                Form1.form1.LB_JumunList.Location = new Point(-1, 1);
                Form1.form1.LB_JumunList.Size = new Size(820, 317);
                Form1.form1.LB_error.Location = new Point(-1, 1);
                Form1.form1.LB_error.Size = new Size(820, 317);
                Form_JuMun.form.Size = new Size(823, 315);
                Form_JuMun.form.JuMun_dataGridView.Location = new Point(-2, -2);
                Form_JuMun.form.JuMun_dataGridView.Size = new Size(823, 315);
                Form_JuMun.form.종목감추기_주문.Location = new Point(150, 19);
                Form_JuMun.form.종목감추기_주문.Size = new Size(96, 279);
                Form1.form1.Label_주문row.Location = new Point(586, -1);
                Form1.form1.TB_주문row.Location = new Point(675, -1);

                //체결패널
                Form1.form1.panel_체결.Location = new Point(1101, 682);
                Form1.form1.panel_체결.Size = new Size(820, 336);
                Form1.form1.tab_체결.Location = new Point(-5, -2);
                Form1.form1.tab_체결.Size = new Size(829, 341);
                Form_Conclusion.form.Size = new Size(823, 315);
                Form_Conclusion.form.Conclusion_DataGridView.Location = new Point(-2, -2);
                Form_Conclusion.form.Conclusion_DataGridView.Size = new Size(823, 315);
                Form_Conclusion.form.종목감추기_체결.Location = new Point(153, 17);
                Form_Conclusion.form.종목감추기_체결.Size = new Size(96, 279);
                Form1.form1.LB_Log.Location = new Point(0, 4);
                Form1.form1.LB_Log.Size = new Size(814, 321);
                Form1.form1.종목감추기_로그.Location = new Point(135, -1);
                Form1.form1.종목감추기_로그.Size = new Size(118, 321);
                Form1.form1.Label_체결row.Location = new Point(153, 0);
                Form1.form1.TB_체결row.Location = new Point(241, 0);

                if (Form1.form1.tab_주문.TabPages.Count < 6)
                {
                    for (int i = Form1.form1.tab_주문.TabPages.Count; i > 3; i--)
                    {
                        Form1.form1.tab_주문.TabPages.RemoveAt(Form1.form1.tab_주문.TabPages.Count - 1);
                    }

                    TabPage myTabPage = new TabPage("조건식TEST A");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST B");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST C");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST D");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);
                }

                Form1.form1.panel_TEST_B.Location = new Point(-1, -1);
                Form1.form1.panel_TEST_C.Location = new Point(-1, -1);
                Form1.form1.panel_TEST_D.Location = new Point(-1, -1);

                Form1.form1.panel_TEST_A.Size = new Size(821, 314);
                Form1.form1.panel_TEST_B.Size = new Size(821, 314);
                Form1.form1.panel_TEST_C.Size = new Size(821, 314);
                Form1.form1.panel_TEST_D.Size = new Size(821, 314);

                Form1.form1.dataGridView_watch_A.Size = new Size(826, 274);
                Form1.form1.dataGridView_watch_B.Size = new Size(826, 274);
                Form1.form1.dataGridView_watch_C.Size = new Size(826, 274);
                Form1.form1.dataGridView_watch_D.Size = new Size(826, 274);
            }
            else if (index == 3)
            {
                Form1.form1.TB_주문row.Text = "41";
                Form_Outstanding.form.TB_미체결row.Text = "12";
                Form1.form1.TB_체결row.Text = "12";

                //잔고패널
                Form1.form1.panel_TP_잔고.Size = new Size(1103, 538);
                Form1.form1.panel_tab_잔고.Location = new Point(-1, 139);
                Form1.form1.panel_tab_잔고.Size = new Size(1103, 538);
                Form1.form1.tab_잔고.Location = new Point(-5, -3);
                Form1.form1.tab_잔고.Size = new Size(1111, 544);
                Form1.form1.JanGo_dataGridView.Location = new Point(-1, -1); //new Point(-3, -2);
                Form1.form1.JanGo_dataGridView.Size = new Size(1106, 520);
                Form1.form1.종목감추기_잔고.Location = new Point(207, 19);
                Form1.form1.종목감추기_잔고.Size = new Size(111, 484);
                Form1.form1.LB_검색결과n관심리스트.Size = new Size(235, 498);
                Form1.form1.LB_관심_A.Size = new Size(234, 498);
                Form1.form1.LB_관심_B.Size = new Size(234, 498);
                Form1.form1.LB_관심_C.Size = new Size(235, 498);

                //주문패널
                Form1.form1.panel_tab_주문.Location = new Point(1101, 20);
                Form1.form1.panel_tab_주문.Size = new Size(820, 999);
                Form1.form1.tab_주문.Location = new Point(-5, -3);
                Form1.form1.tab_주문.Size = new Size(829, 1005);
                Form1.form1.LB_JumunList.Location = new Point(-1, 1);
                Form1.form1.LB_JumunList.Size = new Size(820, 975);
                Form1.form1.LB_error.Location = new Point(-1, 1);
                Form1.form1.LB_error.Size = new Size(820, 975);
                Form_JuMun.form.Size = new Size(823, 978);
                Form_JuMun.form.JuMun_dataGridView.Location = new Point(-2, -2);
                Form_JuMun.form.JuMun_dataGridView.Size = new Size(823, 978);
                Form_JuMun.form.종목감추기_주문.Location = new Point(150, 19);
                Form_JuMun.form.종목감추기_주문.Size = new Size(96, 942);
                Form1.form1.Label_주문row.Location = new Point(304, -1);
                Form1.form1.TB_주문row.Location = new Point(393, -1);

                //미체결패널
                Form_Outstanding.form.Location = new Point(-2, 676);
                Form_Outstanding.form.Size = new Size(553, 343);
                Form_Outstanding.form.Outstanding_DataGridView.Location = new Point(-1, 19);
                Form_Outstanding.form.Outstanding_DataGridView.Size = new Size(553, 321);
                Form_Outstanding.form.종목감추기_미체결.Location = new Point(168, 38);
                Form_Outstanding.form.종목감추기_미체결.Size = new Size(101, 285);
                Form_Outstanding.form.label_미체결내역.Location = new Point(-1, -1);
                Form_Outstanding.form.L_미체결row.Location = new Point(108, -1);
                Form_Outstanding.form.TB_미체결row.Location = new Point(199, -1);
                Form_Outstanding.form.LB_미체결주문.Location = new Point(243, -1);

                //체결패널
                Form1.form1.panel_체결.Location = new Point(550, 675);
                Form1.form1.panel_체결.Size = new Size(552, 344);
                Form1.form1.tab_체결.Location = new Point(-5, -2);
                Form1.form1.tab_체결.Size = new Size(561, 349);
                Form_Conclusion.form.Size = new Size(553, 321);
                Form_Conclusion.form.Conclusion_DataGridView.Location = new Point(-2, -1);
                Form_Conclusion.form.Conclusion_DataGridView.Size = new Size(553, 321);
                Form_Conclusion.form.종목감추기_체결.Location = new Point(153, 18);
                Form_Conclusion.form.종목감추기_체결.Size = new Size(96, 285);
                Form1.form1.LB_Log.Location = new Point(0, 1);
                Form1.form1.LB_Log.Size = new Size(550, 333);
                Form1.form1.종목감추기_로그.Location = new Point(135, -1);
                Form1.form1.종목감추기_로그.Size = new Size(118, 333);
                Form1.form1.Label_체결row.Location = new Point(153, 0);
                Form1.form1.TB_체결row.Location = new Point(242, 0);
                Form1.form1.panel_체결.SendToBack();

                if (Form1.form1.tab_주문.TabPages.Count > 3)
                {
                    for (int i = Form1.form1.tab_주문.TabPages.Count; i > 3; i--)
                    {
                        Form1.form1.tab_주문.TabPages.RemoveAt(Form1.form1.tab_주문.TabPages.Count - 1);
                    }
                }

                TabPage myTabPage = new TabPage("조건식TEST");
                myTabPage.BackColor = SystemColors.ControlLight;
                myTabPage.BorderStyle = BorderStyle.FixedSingle;
                Form1.form1.tab_주문.TabPages.Add(myTabPage);

                Form1.form1.panel_TEST_A.Location = new Point(-1, -1);
                Form1.form1.panel_TEST_B.Location = new Point(-1, 242);
                Form1.form1.panel_TEST_C.Location = new Point(-1, 485);
                Form1.form1.panel_TEST_D.Location = new Point(-1, 729);

                Form1.form1.panel_TEST_A.Size = new Size(821, 245);
                Form1.form1.panel_TEST_B.Size = new Size(821, 244);
                Form1.form1.panel_TEST_C.Size = new Size(821, 245);
                Form1.form1.panel_TEST_D.Size = new Size(821, 249);

                Form1.form1.dataGridView_watch_A.Size = new Size(821, 206);
                Form1.form1.dataGridView_watch_B.Size = new Size(821, 206);
                Form1.form1.dataGridView_watch_C.Size = new Size(821, 206);
                Form1.form1.dataGridView_watch_D.Size = new Size(821, 207);

            }
            else if (index == 4)
            {
                Form1.form1.TB_주문row.Text = "9";
                Form_Outstanding.form.TB_미체결row.Text = "9";
                Form1.form1.TB_체결row.Text = "9";

                //잔고패널
                Form1.form1.panel_TP_잔고.Size = new Size(1922, 611);
                Form1.form1.panel_tab_잔고.Location = new Point(-1, 139);
                Form1.form1.panel_tab_잔고.Size = new Size(1922, 611);
                Form1.form1.tab_잔고.Location = new Point(-5, -3);
                Form1.form1.tab_잔고.Size = new Size(1930, 617);
                Form1.form1.JanGo_dataGridView.Location = new Point(-1, -1); //new Point(-3, -2);
                Form1.form1.JanGo_dataGridView.Size = new Size(1924, 589);
                Form1.form1.종목감추기_잔고.Location = new Point(207, 19);
                Form1.form1.종목감추기_잔고.Size = new Size(111, 553);

                Form1.form1.Controls.Add(Form1.form1.LB_Log);
                Form1.form1.LB_Log.Location = new Point(1101, 14);
                Form1.form1.LB_Log.Size = new Size(820, 152);
                Form1.form1.LB_Log.BringToFront();
                Form1.form1.LB_Log.BorderStyle = BorderStyle.FixedSingle;

                Form1.form1.종목감추기_로그.Location = new Point(135, -1);
                Form1.form1.종목감추기_로그.Size = new Size(118, 146);

                Form1.form1.panel_지수.BringToFront();
                Form1.form1.LB_코스닥하락.BringToFront();

                Form1.form1.tab_체결.TabPages.RemoveAt(1);

                Form1.form1.LB_검색결과n관심리스트.Size = new Size(235, 580);
                Form1.form1.LB_관심_A.Size = new Size(234, 600);
                Form1.form1.LB_관심_B.Size = new Size(234, 600);
                Form1.form1.LB_관심_C.Size = new Size(235, 600);

                //주문패널
                Form1.form1.panel_tab_주문.Location = new Point(-1, 746);
                Form1.form1.panel_tab_주문.Size = new Size(820, 272);
                Form1.form1.tab_주문.Location = new Point(-5, -3);
                Form1.form1.tab_주문.Size = new Size(829, 278);
                Form1.form1.LB_JumunList.Location = new Point(-1, 1);
                Form1.form1.LB_JumunList.Size = new Size(820, 251);
                Form1.form1.LB_error.Location = new Point(-1, 1);
                Form1.form1.LB_error.Size = new Size(820, 251);
                Form_JuMun.form.Size = new Size(823, 251);
                Form_JuMun.form.JuMun_dataGridView.Location = new Point(-2, -1);
                Form_JuMun.form.JuMun_dataGridView.Size = new Size(823, 251);
                Form_JuMun.form.종목감추기_주문.Location = new Point(150, 19);
                Form_JuMun.form.종목감추기_주문.Size = new Size(96, 215);
                Form1.form1.Label_주문row.Location = new Point(586, -1);
                Form1.form1.TB_주문row.Location = new Point(675, -1);

                //미체결패널
                Form_Outstanding.form.Location = new Point(818, 746);
                Form_Outstanding.form.Size = new Size(552, 272);
                Form_Outstanding.form.Outstanding_DataGridView.Location = new Point(-2, 19);
                Form_Outstanding.form.Outstanding_DataGridView.Size = new Size(552, 251);
                Form_Outstanding.form.종목감추기_미체결.Location = new Point(186, 38);
                Form_Outstanding.form.종목감추기_미체결.Size = new Size(101, 215);
                Form_Outstanding.form.label_미체결내역.Location = new Point(-2, -1);
                Form_Outstanding.form.L_미체결row.Location = new Point(107, -1);
                Form_Outstanding.form.TB_미체결row.Location = new Point(198, -1);
                Form_Outstanding.form.LB_미체결주문.Location = new Point(242, -1);
                Form_Outstanding.form.BringToFront();

                //체결패널
                Form1.form1.panel_체결.BringToFront();
                Form1.form1.panel_체결.Location = new Point(1369, 746);
                Form1.form1.panel_체결.Size = new Size(552, 272);
                Form1.form1.tab_체결.Location = new Point(-5, -3);
                Form1.form1.tab_체결.Size = new Size(561, 351);
                Form_Conclusion.form.Size = new Size(553, 251);
                Form_Conclusion.form.Conclusion_DataGridView.Location = new Point(-2, -1);
                Form_Conclusion.form.Conclusion_DataGridView.Size = new Size(553, 251);
                Form_Conclusion.form.종목감추기_체결.Location = new Point(153, 18);
                Form_Conclusion.form.종목감추기_체결.Size = new Size(96, 215);

                Form1.form1.Label_체결row.Location = new Point(153, -1);
                Form1.form1.TB_체결row.Location = new Point(242, -1);

                if (Form1.form1.tab_주문.TabPages.Count != 7)
                {
                    for (int i = Form1.form1.tab_주문.TabPages.Count; i > 3; i--)
                    {
                        Form1.form1.tab_주문.TabPages.RemoveAt(Form1.form1.tab_주문.TabPages.Count - 1);
                    }

                    TabPage myTabPage = new TabPage("조건식TEST A");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST B");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST C");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST D");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);
                }

                Form1.form1.panel_TEST_B.Location = new Point(-1, -1);
                Form1.form1.panel_TEST_C.Location = new Point(-1, -1);
                Form1.form1.panel_TEST_D.Location = new Point(-1, -1);

                Form1.form1.panel_TEST_A.Size = new Size(821, 319);
                Form1.form1.panel_TEST_B.Size = new Size(821, 319);
                Form1.form1.panel_TEST_C.Size = new Size(821, 319);
                Form1.form1.panel_TEST_D.Size = new Size(821, 319);

                Form1.form1.dataGridView_watch_A.Size = new Size(821, 211);
                Form1.form1.dataGridView_watch_B.Size = new Size(821, 211);
                Form1.form1.dataGridView_watch_C.Size = new Size(821, 211);
                Form1.form1.dataGridView_watch_D.Size = new Size(821, 211);
            }
            else if (index == 5)
            {
                Form1.form1.TB_주문row.Text = "12";
                Form_Outstanding.form.TB_미체결row.Text = "200";
                Form1.form1.TB_체결row.Text = "12";

                //잔고패널
                Form1.form1.panel_TP_잔고.Size = new Size(1103, 538);
                Form1.form1.panel_tab_잔고.Location = new Point(-1, 139);
                Form1.form1.panel_tab_잔고.Size = new Size(1103, 538);
                Form1.form1.tab_잔고.Location = new Point(-5, -3);
                Form1.form1.tab_잔고.Size = new Size(1111, 544);
                Form1.form1.JanGo_dataGridView.Location = new Point(-1, -1); //new Point(-3, -2);
                Form1.form1.JanGo_dataGridView.Size = new Size(1106, 520);
                Form1.form1.종목감추기_잔고.Location = new Point(207, 19);
                Form1.form1.종목감추기_잔고.Size = new Size(111, 484);
                Form1.form1.LB_검색결과n관심리스트.Size = new Size(235, 498);
                Form1.form1.LB_관심_A.Size = new Size(234, 498);
                Form1.form1.LB_관심_B.Size = new Size(234, 498);
                Form1.form1.LB_관심_C.Size = new Size(235, 498);

                //주문패널
                Form1.form1.panel_tab_주문.Location = new Point(-1, 676);
                Form1.form1.panel_tab_주문.Size = new Size(820, 341);
                Form1.form1.tab_주문.Location = new Point(-5, -3);
                Form1.form1.tab_주문.Size = new Size(829, 347);
                Form1.form1.LB_JumunList.Location = new Point(-1, 2);
                Form1.form1.LB_JumunList.Size = new Size(820, 321);
                Form1.form1.LB_error.Location = new Point(-1, 2);
                Form1.form1.LB_error.Size = new Size(820, 321);
                Form_JuMun.form.Size = new Size(823, 321);
                Form_JuMun.form.JuMun_dataGridView.Location = new Point(-2, -1);
                Form_JuMun.form.JuMun_dataGridView.Size = new Size(823, 321);
                Form_JuMun.form.종목감추기_주문.Location = new Point(150, 19);
                Form_JuMun.form.종목감추기_주문.Size = new Size(96, 285);
                Form1.form1.Label_주문row.Location = new Point(586, -1);
                Form1.form1.TB_주문row.Location = new Point(675, -1);

                if (Form1.form1.tab_주문.TabPages.Count != 7)
                {
                    for (int i = Form1.form1.tab_주문.TabPages.Count; i > 3; i--)
                    {
                        Form1.form1.tab_주문.TabPages.RemoveAt(Form1.form1.tab_주문.TabPages.Count - 1);
                    }

                    TabPage myTabPage = new TabPage("조건식TEST A");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST B");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST C");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST D");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);
                }

                Form1.form1.panel_TEST_B.Location = new Point(-1, -1);
                Form1.form1.panel_TEST_C.Location = new Point(-1, -1);
                Form1.form1.panel_TEST_D.Location = new Point(-1, -1);

                Form1.form1.panel_TEST_A.Size = new Size(825, 321);
                Form1.form1.panel_TEST_B.Size = new Size(825, 321);
                Form1.form1.panel_TEST_C.Size = new Size(825, 321);
                Form1.form1.panel_TEST_D.Size = new Size(825, 321);

                Form1.form1.dataGridView_watch_A.Size = new Size(825, 281);
                Form1.form1.dataGridView_watch_B.Size = new Size(825, 281);
                Form1.form1.dataGridView_watch_C.Size = new Size(825, 281);
                Form1.form1.dataGridView_watch_D.Size = new Size(825, 281);

                //체결패널
                Form1.form1.panel_체결.Location = new Point(818, 676);
                Form1.form1.panel_체결.Size = new Size(284, 341);
                Form1.form1.tab_체결.Location = new Point(-5, -3);
                Form1.form1.tab_체결.Size = new Size(292, 347);
                Form_Conclusion.form.Size = new Size(288, 321);
                Form_Conclusion.form.Conclusion_DataGridView.Location = new Point(-2, -1);
                Form_Conclusion.form.Conclusion_DataGridView.Size = new Size(288, 321);
                Form_Conclusion.form.종목감추기_체결.Location = new Point(153, 18);
                Form_Conclusion.form.종목감추기_체결.Size = new Size(96, 285);
                Form1.form1.LB_Log.Location = new Point(0, 1);
                Form1.form1.LB_Log.Size = new Size(286, 331);
                Form1.form1.종목감추기_로그.Location = new Point(135, -1);
                Form1.form1.종목감추기_로그.Size = new Size(118, 331);
                Form1.form1.Label_체결row.Location = new Point(153, -1);
                Form1.form1.TB_체결row.Location = new Point(242, -1);

                //미체결패널
                Form_Outstanding.form.Location = new Point(1101, 20);
                Form_Outstanding.form.Size = new Size(820, 997);
                Form_Outstanding.form.Outstanding_DataGridView.Location = new Point(-2, 19);
                Form_Outstanding.form.Outstanding_DataGridView.Size = new Size(823, 977);
                Form_Outstanding.form.종목감추기_미체결.Location = new Point(186, 38);
                Form_Outstanding.form.종목감추기_미체결.Size = new Size(101, 941);
                Form_Outstanding.form.label_미체결내역.Location = new Point(-2, -1);
                Form_Outstanding.form.L_미체결row.Location = new Point(107, -1);
                Form_Outstanding.form.TB_미체결row.Location = new Point(198, -1);
                Form_Outstanding.form.LB_미체결주문.Location = new Point(242, -1);
                Form_Outstanding.form.BringToFront();
            }
            else if (index == 6)
            {
                Form1.form1.TB_주문row.Text = "10";
                Form_Outstanding.form.TB_미체결row.Text = "10";
                Form1.form1.TB_체결row.Text = "10";

                //잔고패널
                Form1.form1.panel_TP_잔고.Size = new Size(1103, 591);
                Form1.form1.panel_tab_잔고.Location = new Point(-1, 139);
                Form1.form1.panel_tab_잔고.Size = new Size(1103, 591);
                Form1.form1.tab_잔고.Location = new Point(-5, -3);
                Form1.form1.tab_잔고.Size = new Size(1111, 597);

                Form1.form1.JanGo_dataGridView.Location = new Point(-1, -1); //new Point(-3, -2);
                Form1.form1.JanGo_dataGridView.Size = new Size(1106, 306);

                Form1.form1.종목감추기_잔고.Location = new Point(207, 19);
                Form1.form1.종목감추기_잔고.Size = new Size(111, 270);

                Form_JuMun.form.Size = new Size(1106, 268);
                Form_JuMun.form.JuMun_dataGridView.Location = new Point(-3, 305);
                Form_JuMun.form.JuMun_dataGridView.Size = new Size(1106, 268);

                Form1.form1.LB_검색결과n관심리스트.Size = new Size(235, 552);
                Form1.form1.LB_관심_A.Size = new Size(234, 552);
                Form1.form1.LB_관심_B.Size = new Size(234, 552);
                Form1.form1.LB_관심_C.Size = new Size(235, 552);

                //주문패널
                Form1.form1.panel_tab_주문.Location = new Point(1101, 20);
                Form1.form1.panel_tab_주문.Size = new Size(820, 710);
                Form1.form1.tab_주문.Location = new Point(-5, -3);
                Form1.form1.tab_주문.Size = new Size(828, 716);
                Form1.form1.LB_JumunList.Location = new Point(-1, 2);
                Form1.form1.LB_JumunList.Size = new Size(820, 695);
                Form1.form1.LB_error.Location = new Point(-1, 2);
                Form1.form1.LB_error.Size = new Size(820, 695);
                Form_JuMun.form.종목감추기_주문.Location = new Point(147, 324);
                Form_JuMun.form.종목감추기_주문.Size = new Size(96, 232);

                if (Form1.form1.tab_주문.TabPages.Count != 4)
                {
                    for (int i = Form1.form1.tab_주문.TabPages.Count; i > 2; i--)
                    {
                        Form1.form1.tab_주문.TabPages.RemoveAt(Form1.form1.tab_주문.TabPages.Count - 1);
                    }

                    TabPage myTabPage = new TabPage("조건식TEST A & B");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);

                    myTabPage = new TabPage("조건식TEST C & D");
                    myTabPage.BackColor = SystemColors.ControlLight;
                    myTabPage.BorderStyle = BorderStyle.FixedSingle;
                    Form1.form1.tab_주문.TabPages.Add(myTabPage);
                }

                Form1.form1.panel_TEST_C.Location = new Point(-1, -1);
                Form1.form1.panel_TEST_B.Location = new Point(-1, 355);
                Form1.form1.panel_TEST_D.Location = new Point(-1, 355);

                Form1.form1.panel_TEST_A.Size = new Size(821, 357);
                Form1.form1.panel_TEST_C.Size = new Size(821, 357);
                Form1.form1.panel_TEST_B.Size = new Size(821, 334);
                Form1.form1.panel_TEST_D.Size = new Size(821, 334);

                Form1.form1.dataGridView_watch_A.Size = new Size(823, 317);
                Form1.form1.dataGridView_watch_C.Size = new Size(823, 317);
                Form1.form1.dataGridView_watch_B.Size = new Size(823, 297);
                Form1.form1.dataGridView_watch_D.Size = new Size(823, 297);

                //미체결패널
                Form_Outstanding.form.Location = new Point(-2, 729);
                Form_Outstanding.form.Size = new Size(1104, 290);
                Form_Outstanding.form.Outstanding_DataGridView.Location = new Point(-1, 19);
                Form_Outstanding.form.Outstanding_DataGridView.Size = new Size(1103, 268);
                Form_Outstanding.form.종목감추기_미체결.Location = new Point(168, 38);
                Form_Outstanding.form.종목감추기_미체결.Size = new Size(101, 232);
                Form_Outstanding.form.label_미체결내역.Location = new Point(-1, -1);
                Form_Outstanding.form.L_미체결row.Location = new Point(108, -1);
                Form_Outstanding.form.TB_미체결row.Location = new Point(199, -1);
                Form_Outstanding.form.LB_미체결주문.Location = new Point(243, -1);

                //체결패널
                Form1.form1.panel_체결.Location = new Point(1101, 729);
                Form1.form1.panel_체결.Size = new Size(820, 290);
                Form1.form1.tab_체결.Location = new Point(-5, -3);
                Form1.form1.tab_체결.Size = new Size(828, 324);
                Form_Conclusion.form.Size = new Size(822, 268);
                Form_Conclusion.form.Conclusion_DataGridView.Location = new Point(-2, -1);
                Form_Conclusion.form.Conclusion_DataGridView.Size = new Size(822, 268);
                Form_Conclusion.form.종목감추기_체결.Location = new Point(153, 18);
                Form_Conclusion.form.종목감추기_체결.Size = new Size(96, 232);
                Form1.form1.LB_Log.Location = new Point(0, 1);
                Form1.form1.LB_Log.Size = new Size(821, 265);

                Form1.form1.Label_체결row.Location = new Point(153, -1);
                Form1.form1.TB_체결row.Location = new Point(242, -1);



                Form1.form1.종목감추기_로그.Location = new Point(135, -1);
                Form1.form1.종목감추기_로그.Size = new Size(118, 331);


            }

            if (Form1.form1.Contains(Form1.form1.MBC))
            {
                Form1.form1.MBC.BringToFront();
                Form1.form1.MBC.BT_적용.Select();
            }

            Front_End.Tab_주문_SelectedIndexChanged();

        }


        // [+] 다른 메뉴들의 체크를 해제하여 라디오 버튼처럼 동작하게 만드는 전용 함수
        public static void 다른_메뉴_체크해제(CheckBox 선택된_체크박스)
        {
            if (선택된_체크박스 == null) return;

            // 본인(선택된_체크박스)이 아닌 다른 체크박스들은 모두 강제로 끕니다.
            // 체크가 꺼지는 순간 자동으로 기존 이벤트의 Hide() 로직을 타면서 창이 스르륵 닫힙니다.
            if (!선택된_체크박스.Equals(Form1.form1.CB_기본매매)) Form1.form1.CB_기본매매.Checked = false;
            if (!선택된_체크박스.Equals(Form1.form1.CB_반복매매)) Form1.form1.CB_반복매매.Checked = false;
            if (!선택된_체크박스.Equals(Form1.form1.CB_계좌관리)) Form1.form1.CB_계좌관리.Checked = false;
            if (!선택된_체크박스.Equals(Form1.form1.CB_특수매매)) Form1.form1.CB_특수매매.Checked = false;
            if (!선택된_체크박스.Equals(Form1.form1.CB_대금탐색)) Form1.form1.CB_대금탐색.Checked = false;
            if (!선택된_체크박스.Equals(Form1.form1.CB_매매그룹)) Form1.form1.CB_매매그룹.Checked = false;
            if (!선택된_체크박스.Equals(Form1.form1.CB_기능설정)) Form1.form1.CB_기능설정.Checked = false;

            Form_AccountManagement.form.MinimumSize = new Size(0, 0); // 임시 해제
            Form_AccountManagement.form.MaximumSize = new Size(1936, 389);
            Form_AccountManagement.form.MinimumSize = new Size(1936, 389);
            Form_AccountManagement.form.Size = new Size(1936, 389);

        }


        public static void CB_기능설정_CheckedChanged(object sender)
        {
            CheckBox CB = sender as CheckBox;
            Form1.form1.체크박스_비프(sender);

            if (Form1.form1.tab_잔고.SelectedIndex == 2) Form1.form1.tab_잔고.SelectedIndex = 0;

            if (!sender.Equals("지수"))
            {
                // CB가 null이 아니고 체크가 '켜졌을 때'만 작동 (무한 루프 방지)
                if (CB != null && CB.Checked)
                {
                    // =========================================================
                    // [+] 분리해둔 전용 함수를 호출하여 본인 외 나머지 창들을 닫습니다.
                    // =========================================================
                    다른_메뉴_체크해제(CB);

                    if (Form1.FormJisu_Open) { Form1.form1.Controls.Remove(Form1.form1.Jisu_Form); GenieConfig.지수이평 = "지수설정"; }
                }
                else
                {
                    CBB_layout_SelectedIndex(GenieConfig.CBB_layout);
                }
            }

            int X = Form1.form1.Location.X;
            int Y = Form1.form1.Location.Y + 707;
            Point startpoint = new Point(X, Y);
            if (Properties.Settings.Default.WindowLocation.X.Equals(-32000)) Properties.Settings.Default.WindowLocation = startpoint;

            if (sender.Equals("지수"))
            {
                if (GenieConfig.지수이평 == "지수이평설정")
                {
                    Form1.form1.Controls.Add(Form1.form1.Jisu_Form);
                    Form_Jisu.form.Location = new Point(791, 20);
                    Form_Jisu.form.BringToFront();
                    
                    Form_Jisu.form.Jisu_Form_Load();
                    Form_Jisu.form.Show();
                    Form1.FormJisu_Open = true;
                }
                else
                {
                    Form1.form1.Controls.Remove(Form1.form1.Jisu_Form);
                    Form1.FormJisu_Open = false;
                }

                if (GenieConfig.지수이평 == "지수이평프린터")
                {
                    Form1.form1.Controls.Add(Form1.form1.Jisu_print_Form);
                    Form_Jisu_print.form.Location = new Point(905, 0);
                    Form_Jisu_print.form.BringToFront();
                    Form_Jisu_print.form.Show();
                    Form1.FormJisu_print_Open = true;
                }
                else
                {
                    Form1.form1.Controls.Remove(Form1.form1.Jisu_print_Form);
                    Form1.FormJisu_print_Open = false;
                }
            }

            // 1. [기본매매]
            if (sender.Equals(Form1.form1.CB_기본매매))
            {
                if (CB.Checked)
                {
                    Form_Basic.form.CB_기본매매_시작위치저장.Checked = GenieConfig.CB_기본매매_시작위치저장;
                    if (GenieConfig.CB_기본매매_시작위치저장) startpoint = Properties.Settings.Default.WindowLocation;
                    Form_Basic.form.Location = startpoint;
                    StartLocationChanged(Form_Basic.form, X, Y);

                    Form_Basic.form.CB_레이아웃고정_기본매매.Checked = GenieConfig.CB_레이아웃고정_기본매매;
                    if (!Form_Basic.form.CB_레이아웃고정_기본매매.Checked) CBB_layout_SelectedIndex(-1);
                    else CBB_layout_SelectedIndex(GenieConfig.CBB_layout);

                    Form1.FormBasic_Open = true;
                    Form_Basic.form.Form_Basic_Load();
                    Form_Basic.form.Show();
                }
                else
                {
                    GenieConfig.CB_레이아웃고정_기본매매 = Form_Basic.form.CB_레이아웃고정_기본매매.Checked;
                    GenieConfig.CB_기본매매_시작위치저장 = Form_Basic.form.CB_기본매매_시작위치저장.Checked;
                    if (GenieConfig.CB_기본매매_시작위치저장) Properties.Settings.Default.WindowLocation = Form_Basic.form.Location;

                    Form_Basic.form.Hide();
                    Form1.FormBasic_Open = false;
                }
            }

            // 2. [반복매매]
            if (sender.Equals(Form1.form1.CB_반복매매))
            {
                if (CB.Checked)
                {
                    Form_Repeat.form.CB_반복매매_시작위치저장.Checked = GenieConfig.CB_반복매매_시작위치저장;
                    if (GenieConfig.CB_반복매매_시작위치저장) startpoint = Properties.Settings.Default.WindowLocation;
                    Form_Repeat.form.Location = startpoint;
                    StartLocationChanged(Form_Repeat.form, X, Y);

                    Form_Repeat.form.CB_레이아웃고정_반복매매.Checked = GenieConfig.CB_레이아웃고정_반복매매;
                    if (!Form_Repeat.form.CB_레이아웃고정_반복매매.Checked) CBB_layout_SelectedIndex(-1);
                    else CBB_layout_SelectedIndex(GenieConfig.CBB_layout);

                    Form1.FormRepeat_Open = true;
                    Form_Repeat.form.Form_Repeat_Load();
                    Form_Repeat.form.Show();
                }
                else
                {
                    GenieConfig.CB_레이아웃고정_반복매매 = Form_Repeat.form.CB_레이아웃고정_반복매매.Checked;
                    GenieConfig.CB_반복매매_시작위치저장 = Form_Repeat.form.CB_반복매매_시작위치저장.Checked;
                    if (GenieConfig.CB_반복매매_시작위치저장) Properties.Settings.Default.WindowLocation = Form_Repeat.form.Location;

                    Form_Repeat.form.Hide();
                    Form1.FormRepeat_Open = false;
                }
            }

            // 3. [계좌관리]
            if (sender.Equals(Form1.form1.CB_계좌관리))
            {
                if (CB.Checked)
                {
                    Form1.form1.동작상태보기 = true;
                    Form_AccountManagement.form.CB_계좌관리_시작위치저장.Checked = GenieConfig.CB_계좌관리_시작위치저장;
                    if (GenieConfig.CB_계좌관리_시작위치저장) startpoint = Properties.Settings.Default.WindowLocation;
                    Form_AccountManagement.form.Location = startpoint;
                    StartLocationChanged(Form_AccountManagement.form, X, Y);

                    Form_AccountManagement.form.CB_레이아웃고정_계좌관리.Checked = GenieConfig.CB_레이아웃고정_계좌관리;
                    if (!Form_AccountManagement.form.CB_레이아웃고정_계좌관리.Checked) CBB_layout_SelectedIndex(-1);
                    else CBB_layout_SelectedIndex(GenieConfig.CBB_layout);

                    Form1.FormAccountManagement_Open = true;
                    Form_AccountManagement.form.Form_AccountManagement_Load();
                    Form_AccountManagement.form.Show();

                }
                else
                {
                    GenieConfig.CB_레이아웃고정_계좌관리 = Form_AccountManagement.form.CB_레이아웃고정_계좌관리.Checked;
                    GenieConfig.CB_계좌관리_시작위치저장 = Form_AccountManagement.form.CB_계좌관리_시작위치저장.Checked;
                    if (GenieConfig.CB_계좌관리_시작위치저장) Properties.Settings.Default.WindowLocation = Form_AccountManagement.form.Location;

                    Form_AccountManagement.form.Hide();
                    Form1.FormAccountManagement_Open = false;
                }
            }

            // 4. [특수매매]
            if (sender.Equals(Form1.form1.CB_특수매매))
            {
                if (CB.Checked)
                {
                    Form_Special.form.CB_특수매매_시작위치저장.Checked = GenieConfig.CB_특수매매_시작위치저장;
                    if (GenieConfig.CB_특수매매_시작위치저장) startpoint = Properties.Settings.Default.WindowLocation;
                    Form_Special.form.Location = startpoint;
                    StartLocationChanged(Form_Special.form, X, Y);

                    Form_Special.form.CB_레이아웃고정_특수매매.Checked = GenieConfig.CB_레이아웃고정_특수매매;
                    if (!Form_Special.form.CB_레이아웃고정_특수매매.Checked) CBB_layout_SelectedIndex(-1);
                    else CBB_layout_SelectedIndex(GenieConfig.CBB_layout);

                    Form1.FormSpecial_Open = true;
                    Form_Special.form.Form_Special_Load();
                    Form_Special.form.Show();
                    Form_Special.form.TB_수동주문_종목명.AutoCompleteCustomSource = Form1.form1.collection;
                    Form_Special.form.TB_예약주문_종목명.AutoCompleteCustomSource = Form1.form1.collection;
                    Form_Special.form.TB_자금관리종목.AutoCompleteCustomSource = Form1.form1.collection;
               
                    // 1. 설정된 종목코드를 변수에 담고, 혹시 비어있는지(null) 검사합니다.
                    string 관리코드 = GenieConfig.label_관리종목코드;

                    if (!string.IsNullOrEmpty(관리코드))
                    {
                        // 2. [핵심 최적화] TryGetValue를 사용하여 딕셔너리에 코드가 있는지 조용히 찔러봅니다.
                        // 코드가 없어도 에러를 뿜지 않고 부드럽게 false를 반환하며 지나갑니다.
                        if (Form1.Market_Item_List.TryGetValue(관리코드, out Market_Item 찾은_종목))
                        {
                            // 3. 종목 객체와 목적지(텍스트박스)가 모두 정상적으로 존재할 때만 글자를 씁니다.
                            if (찾은_종목 != null && Form_Special.form != null && Form_Special.form.TB_자금관리종목 != null)
                            {
                                Form_Special.form.TB_자금관리종목.Text = 찾은_종목.종목명;
                                info.요청(찾은_종목.종목코드, "info_자금관리", "", false);
                            }
                        }
                        else
                        {
                            // [선택 사항] 만약 딕셔너리에 종목이 없다면 텍스트박스를 깔끔하게 비워줍니다.
                            if (Form_Special.form != null && Form_Special.form.TB_자금관리종목 != null)
                            {
                                Form_Special.form.TB_자금관리종목.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    try
                    {
                        GenieConfig.CB_수동주문_시장가 = Form_Special.form.CB_수동주문_시장가.Checked;
                        GenieConfig.CB_수동주문_주문가고정 = Form_Special.form.CB_수동주문_주문가고정.Checked;
                        GenieConfig.수동주문_RB_매수 = Form_Special.form.RB_매수.Checked;

                        int.TryParse(Form_Special.form.TB_수동주문_tick.Text, out int TB_수동주문_tick);
                        int.TryParse(Form_Special.form.MTB_수동주문_repeat.Text, out int MTB_수동주문_repeat);
                        int.TryParse(Form_Special.form.MTB_수동주문_cansel_time.Text, out int MTB_수동주문_cansel_time);
                        double.TryParse(Form_Special.form.TB_수동주문_ratio.Text, out double TB_수동주문_ratio);
                        double.TryParse(Form_Special.form.TB_수동주문_주문비.Text, out double TB_수동주문_주문비);

                        if (MTB_수동주문_cansel_time == 0) MTB_수동주문_cansel_time = 30;
                        if (TB_수동주문_ratio == 0) TB_수동주문_ratio = 1;

                        GenieConfig.TB_수동주문_tick = TB_수동주문_tick;
                        GenieConfig.MTB_수동주문_repeat = MTB_수동주문_repeat;
                        GenieConfig.MTB_수동주문_cansel_time = MTB_수동주문_cansel_time;
                        GenieConfig.TB_수동주문_ratio = TB_수동주문_ratio;
                        GenieConfig.TB_수동주문_주문비 = TB_수동주문_주문비;

                        Form_Special.form.TB_수동주문_tick.Text = TB_수동주문_tick.ToString();
                        Form_Special.form.MTB_수동주문_repeat.Text = MTB_수동주문_repeat.ToString();
                        Form_Special.form.MTB_수동주문_cansel_time.Text = MTB_수동주문_cansel_time.ToString();
                        Form_Special.form.TB_수동주문_ratio.Text = TB_수동주문_ratio.ToString();
                        Form_Special.form.TB_수동주문_주문비.Text = TB_수동주문_주문비.ToString();

                        GenieConfig.combo_수동주문_choice = Form_Special.form.combo_수동주문_choice.SelectedIndex;
                    }
                    catch (Exception e)
                    {
                        Form1.Console_print("특수매매_저장 수동주문입력 에러: " + e.Message); Log.에러기록("특수매매_저장 수동주문입력 에러: " + e.Message);
                    }

                    GenieConfig.CB_레이아웃고정_특수매매 = Form_Special.form.CB_레이아웃고정_특수매매.Checked;
                    GenieConfig.CB_특수매매_시작위치저장 = Form_Special.form.CB_특수매매_시작위치저장.Checked;
                    if (GenieConfig.CB_특수매매_시작위치저장) Properties.Settings.Default.WindowLocation = Form_Special.form.Location;

                    Form_Special.form.Hide();
                    Form1.FormSpecial_Open = false;
                }
            }

            // 5. [대금탐색]
            if (sender.Equals(Form1.form1.CB_대금탐색))
            {
                if (CB.Checked)
                {
                    Form_PriceSearch.form.CB_대금탐색_시작위치저장.Checked = GenieConfig.CB_대금탐색_시작위치저장;
                    if (GenieConfig.CB_대금탐색_시작위치저장) startpoint = Properties.Settings.Default.WindowLocation;
                    Form_PriceSearch.form.Location = startpoint;
                    StartLocationChanged(Form_PriceSearch.form, X, Y);

                    Form_PriceSearch.form.CB_레이아웃고정_대금탐색.Checked = GenieConfig.CB_레이아웃고정_대금탐색;
                    if (!Form_PriceSearch.form.CB_레이아웃고정_대금탐색.Checked) CBB_layout_SelectedIndex(-1);
                    else CBB_layout_SelectedIndex(GenieConfig.CBB_layout);

                    Form1.FormPriceSearch_Open = true;
                    Form_PriceSearch.form.Form_PriceSearch_Load();
                    Form_PriceSearch.form.Show();
                }
                else
                {
                    GenieConfig.CB_레이아웃고정_대금탐색 = Form_PriceSearch.form.CB_레이아웃고정_대금탐색.Checked;
                    GenieConfig.CB_대금탐색_시작위치저장 = Form_PriceSearch.form.CB_대금탐색_시작위치저장.Checked;
                    if (GenieConfig.CB_대금탐색_시작위치저장) Properties.Settings.Default.WindowLocation = Form_PriceSearch.form.Location;

                    Form_PriceSearch.form.Hide();
                    Form1.FormPriceSearch_Open = false;
                }
            }

            // 6. [매매그룹]
            if (sender.Equals(Form1.form1.CB_매매그룹))
            {
                if (CB.Checked)
                {
                    startpoint = new Point(X, Form1.form1.Location.Y + 422);
                    Form_TradeGroup.form.CB_매매그룹_시작위치저장.Checked = GenieConfig.CB_매매그룹_시작위치저장;
                    if (GenieConfig.CB_매매그룹_시작위치저장) startpoint = Properties.Settings.Default.WindowLocation;
                    Form_TradeGroup.form.Location = startpoint;
                    StartLocationChanged(Form_TradeGroup.form, X, Y);

                    Form_TradeGroup.form.CB_레이아웃고정_매매그룹.Checked = GenieConfig.CB_레이아웃고정_매매그룹;
                    if (!Form_TradeGroup.form.CB_레이아웃고정_매매그룹.Checked) CBB_layout_SelectedIndex(-1);
                    else CBB_layout_SelectedIndex(GenieConfig.CBB_layout);

                    Form1.FormTradeGroup_Open = true;
                    Form_TradeGroup.form.Form_TradeGroup_Load();
                    Form_TradeGroup.form.Show();
                }
                else
                {
                    GenieConfig.CB_레이아웃고정_매매그룹 = Form_TradeGroup.form.CB_레이아웃고정_매매그룹.Checked;
                    GenieConfig.CB_매매그룹_시작위치저장 = Form_TradeGroup.form.CB_매매그룹_시작위치저장.Checked;
                    if (GenieConfig.CB_매매그룹_시작위치저장) Properties.Settings.Default.WindowLocation = Form_TradeGroup.form.Location;

                    Form_TradeGroup.form.Hide();
                    Form1.FormTradeGroup_Open = false;
                }
            }

            // 7. [기능설정]
            if (sender.Equals(Form1.form1.CB_기능설정))
            {
                if (CB.Checked)
                {
                    Form_Function.form.CB_기능설정_시작위치저장.Checked = GenieConfig.CB_기능설정_시작위치저장;
                    if (GenieConfig.CB_기능설정_시작위치저장) startpoint = Properties.Settings.Default.WindowLocation;
                    Form_Function.form.Location = startpoint;
                    StartLocationChanged(Form_Function.form, X, Y);

                    Form_Function.form.CB_레이아웃고정_기능설정.Checked = GenieConfig.CB_레이아웃고정_기능설정;
                    if (!Form_Function.form.CB_레이아웃고정_기능설정.Checked) CBB_layout_SelectedIndex(-1);
                    else CBB_layout_SelectedIndex(GenieConfig.CBB_layout);

                    Form1.FormFunction_Open = true;
                    Form_Function.form.Form_Function_Load();
                    Form_Function.form.Show();
                }
                else
                {
                    GenieConfig.CB_레이아웃고정_기능설정 = Form_Function.form.CB_레이아웃고정_기능설정.Checked;
                    GenieConfig.CB_기능설정_시작위치저장 = Form_Function.form.CB_기능설정_시작위치저장.Checked;
                    if (GenieConfig.CB_기능설정_시작위치저장) Properties.Settings.Default.WindowLocation = Form_Function.form.Location;

                    Form1.form1.TopMost = GenieConfig.CB_지니64항상위에;
                    Form_Function.form.Hide();
                    Form1.FormFunction_Open = false;
                }
            }
        }

        // [수정] void 앞에 async 예약어를 추가하여 비동기 함수로 변경합니다.
        public static async void 모든서브폼_미리생성()
        {
            Form1.Console_print(">> [최적화] 서브폼 스텔스 렌더링 비동기 분산 시작...");

            try
            {
                // 1. 기본매매
                if (Form1.form1.Form_Basic == null)
                {
                    Form1.form1.Form_Basic = new Form_Basic();
                    Form_Basic.form = Form1.form1.Form_Basic;
                    Form_Basic.form.TopMost = true;
                    Form_Basic.form.MaximumSize = new Size(1936, 389);
                    Form_Basic.form.MinimumSize = new Size(1936, 389);
                    Form_Basic.form.Size = new Size(1936, 389);

                    Form_Basic.form.Opacity = 0;
                    Form_Basic.form.Show();
                    Form_Basic.form.Hide();
                    Form_Basic.form.Opacity = 1;
                }

                // [+] 핵심: 메인 UI가 멈추지 않도록 0.1초(100ms) 휴식하며 윈도우 자원을 양보합니다.
                await Task.Delay(100);

                // 2. 반복매매
                if (Form1.form1.Form_Repeat == null)
                {
                    Form1.form1.Form_Repeat = new Form_Repeat();
                    Form_Repeat.form = Form1.form1.Form_Repeat;
                    Form_Repeat.form.TopMost = true;
                    Form_Repeat.form.MaximumSize = new Size(1936, 389);
                    Form_Repeat.form.MinimumSize = new Size(1936, 389);
                    Form_Repeat.form.Size = new Size(1936, 389);

                    Form_Repeat.form.Opacity = 0;
                    Form_Repeat.form.Show();
                    Form_Repeat.form.Hide();
                    Form_Repeat.form.Opacity = 1;
                }

                await Task.Delay(100);

                // 3. 계좌관리
                if (Form1.form1.Form_AccountManagement == null)
                {
                    Form1.form1.Form_AccountManagement = new Form_AccountManagement();
                    Form_AccountManagement.form = Form1.form1.Form_AccountManagement;
                    Form_AccountManagement.form.TopMost = true;
                    Form_AccountManagement.form.MaximumSize = new Size(1936, 389);
                    Form_AccountManagement.form.MinimumSize = new Size(1936, 389);
                    Form_AccountManagement.form.Size = new Size(1936, 389);
                    Form_AccountManagement.form.Form_AccountManagement_Load();

                    Form_AccountManagement.form.Opacity = 0;
                    Form_AccountManagement.form.Show();
                    Form_AccountManagement.form.Hide();
                    Form_AccountManagement.form.Opacity = 1;
                }

                await Task.Delay(100);

                // 4. 특수매매
                if (Form1.form1.Form_Special == null)
                {
                    Form1.form1.Form_Special = new Form_Special();
                    Form_Special.form = Form1.form1.Form_Special;
                    Form_Special.form.TopMost = true;
                    Form_Special.form.MaximumSize = new Size(1936, 389);
                    Form_Special.form.MinimumSize = new Size(1936, 389);
                    Form_Special.form.Size = new Size(1936, 389);

                    Form_Special.form.Opacity = 0;
                    Form_Special.form.Show();
                    Form_Special.form.Hide();
                    Form_Special.form.Opacity = 1;
                }

                await Task.Delay(100);

                // 5. 대금탐색
                if (Form1.form1.Form_PriceSearch == null)
                {
                    Form1.form1.Form_PriceSearch = new Form_PriceSearch();
                    Form_PriceSearch.form = Form1.form1.Form_PriceSearch;
                    Form_PriceSearch.form.TopMost = true;
                    Form_PriceSearch.form.MaximumSize = new Size(1936, 389);
                    Form_PriceSearch.form.MinimumSize = new Size(1936, 389);
                    Form_PriceSearch.form.Size = new Size(1936, 389);

                    Form_PriceSearch.form.Opacity = 0;
                    Form_PriceSearch.form.Show();
                    Form_PriceSearch.form.Hide();
                    Form_PriceSearch.form.Opacity = 1;
                }

                await Task.Delay(100);

                // 6. 매매그룹
                if (Form1.form1.Form_TradeGroup == null)
                {
                    Form1.form1.Form_TradeGroup = new Form_TradeGroup();
                    Form_TradeGroup.form = Form1.form1.Form_TradeGroup;
                    Form_TradeGroup.form.TopMost = true;
                    Form_TradeGroup.form.MaximumSize = new Size(1936, 674);
                    Form_TradeGroup.form.MinimumSize = new Size(1936, 674);
                    Form_TradeGroup.form.Size = new Size(1936, 674);

                    Form_TradeGroup.form.Opacity = 0;
                    Form_TradeGroup.form.Show();
                    Form_TradeGroup.form.Hide();
                    Form_TradeGroup.form.Opacity = 1;
                }

                await Task.Delay(100);

                // 7. 기능설정
                if (Form1.form1.Form_Function == null)
                {
                    Form1.form1.Form_Function = new Form_Function();
                    Form_Function.form = Form1.form1.Form_Function;
                    Form_Function.form.TopMost = true;
                    Form_Function.form.MaximumSize = new Size(1936, 389);
                    Form_Function.form.MinimumSize = new Size(1936, 389);
                    Form_Function.form.Size = new Size(1936, 389);

                    Form_Function.form.Opacity = 0;
                    Form_Function.form.Show();
                    Form_Function.form.Hide();
                    Form_Function.form.Opacity = 1;
                }

                Form1.Console_print("[+] === 서브폼 비동기 완전 렌더링 완료 ===");
            }
            catch (Exception ex)
            {
                Form1.Console_print("[-] >> 서브폼 초기화 중 오류: " + ex.Message);
            }
        }
        public static void StartLocationChanged(Form form, int X, int Y)
        {
            Screen[] sc = Screen.AllScreens;
            if (sc.Length == 1)
            {
                if (form.Location.X <= -(form.Width / 2 + form.Width / 3))
                    form.Location = new Point(X, Y);

                if (form.Location.X + (form.Width / 2 - form.Width / 3) >= Screen.PrimaryScreen.Bounds.Width)
                    form.Location = new Point(X, Y);

                if (form.Location.Y < -3)
                    form.Location = new Point(X, Y);

                if (form.Location.Y >= Screen.PrimaryScreen.Bounds.Height - (form.Height / 2 - form.Height / 3))
                    form.Location = new Point(X, Y);
            }
        }


    }
}
