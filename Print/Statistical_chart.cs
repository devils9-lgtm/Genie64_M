using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace 지니_64
{
    public class Statistical_chart
    {
        public static void 통계_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender.Equals(Form1.form1.DGV_통계))
            {
                Form1.form1.DGV_통계.CurrentCell = null;
                Form1.form1.CBB_통계.SelectedIndex = 0;
            }

            if (sender.Equals(Form1.form1.DGV_통계B))
            {
                if (Form1.form1.DGV_통계B.Rows.Count > 0 && e.RowIndex > -1)
                {
                    if (Form1.form1.DGV_통계B[0, e.RowIndex].Value != null)
                    {
                        string 일자 = Form1.form1.DGV_통계B[0, e.RowIndex].Value.ToString();
                        if (일자.Equals("01 월")) { Form1.form1.CBB_통계.SelectedIndex = 1; return; }
                        if (일자.Equals("02 월")) { Form1.form1.CBB_통계.SelectedIndex = 2; return; }
                        if (일자.Equals("03 월")) { Form1.form1.CBB_통계.SelectedIndex = 3; return; }
                        if (일자.Equals("04 월")) { Form1.form1.CBB_통계.SelectedIndex = 4; return; }
                        if (일자.Equals("05 월")) { Form1.form1.CBB_통계.SelectedIndex = 5; return; }
                        if (일자.Equals("06 월")) { Form1.form1.CBB_통계.SelectedIndex = 6; return; }
                        if (일자.Equals("07 월")) { Form1.form1.CBB_통계.SelectedIndex = 7; return; }
                        if (일자.Equals("08 월")) { Form1.form1.CBB_통계.SelectedIndex = 8; return; }
                        if (일자.Equals("09 월")) { Form1.form1.CBB_통계.SelectedIndex = 9; return; }
                        if (일자.Equals("10 월")) { Form1.form1.CBB_통계.SelectedIndex = 10; return; }
                        if (일자.Equals("11 월")) { Form1.form1.CBB_통계.SelectedIndex = 11; return; }
                        if (일자.Equals("12 월")) { Form1.form1.CBB_통계.SelectedIndex = 12; return; }
                    }
                }

                if (e.RowIndex == -1) Form1.form1.DGV_통계B.CurrentCell = null;
            }
        }

        public static void 통계_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == -1 || e.RowIndex == -1)
                {
                    return;
                }
                else
                {
                    if (Form1.form1.DGV_통계B[0, e.RowIndex].Value != null)
                    {
                        string 일자 = Form1.form1.DGV_통계B[0, e.RowIndex].Value.ToString();

                        if (Form1.form1.CBB_통계.SelectedIndex > 0)
                        {
                            int.TryParse(Form1.form1.CBB_통계.Text.Substring(0, 2), out int 월);
                            int.TryParse(일자.Substring(0, 2), out int 일);

                            DateTime.TryParse(DateTime.Now.ToString("yyyy-") + 월 + "-" + 일, out DateTime 기준일자);
                            Form1.form1.DTP_기준일.Value = 기준일자;

                            기준일매매내역();
                        }
                    }
                }
            }
            catch (Exception 에러)
            {
                Console.WriteLine("DGV_통계B 더블클릭 " + 에러.Message);
                Form1.Error_Log("Form1.form1.DGV_통계B  에러:: 메세지 " + 에러.Message);
            }
        }


        public static void Print_기준일매매일지()
        {
            Form1.form1.chart_Month.Location = new Point(1102, 5);
            Form1.form1.chart_Month.Size = new Size(826, 500);
            Form1.form1.chart_Day.Location = new Point(1102, 497);
            Form1.form1.chart_Day.Size = new Size(826, 500);

            Form1.form1.DGV_통계B.Size = new Size(1104, 935);
            Form1.form1.종목감추기6.Size = new Size(126, 924);

            if (Form1.form1.CB_종목비공개.Checked)
            {
                Form1.form1.종목감추기6.Show();
                Form1.form1.종목감추기6.BringToFront();
            }
            else
            {
                Form1.form1.종목감추기6.Hide();
            }

            Form1.form1.DGV_통계.Rows.Insert(0);

            double 총매수금 = 0;
            double 총매도금 = 0;
            double 총실현손익 = 0;
            double 총수수료 = 0;
            double 총세금 = 0;

            for (int i = 0; i < Form1.form1.기준일매매내역_List.Count; i++)
            {
                총매수금 = 총매수금 + double.Parse(Form1.form1.기준일매매내역_List[i].Split('^')[1]);
                총매도금 = 총매도금 + double.Parse(Form1.form1.기준일매매내역_List[i].Split('^')[2]);
                총실현손익 = 총실현손익 + double.Parse(Form1.form1.기준일매매내역_List[i].Split('^')[3]);
                총수수료 = 총수수료 + double.Parse(Form1.form1.기준일매매내역_List[i].Split('^')[4]);
                총세금 = 총세금 + double.Parse(Form1.form1.기준일매매내역_List[i].Split('^')[5]);
            }

            Form1.form1.DGV_통계[0, 0].Value = 총매수금;
            Form1.form1.DGV_통계[1, 0].Value = (double)총매수금 / (double)Properties.Settings.Default.TB_월통계기준 * 100;
            Form1.form1.DGV_통계[2, 0].Value = 총매도금;
            Form1.form1.DGV_통계[3, 0].Value = (double)총매도금 / (double)Properties.Settings.Default.TB_월통계기준 * 100;
            Form1.form1.DGV_통계[4, 0].Value = 총실현손익;
            Form1.form1.DGV_통계[5, 0].Value = (double)총실현손익 / (double)Properties.Settings.Default.TB_월통계기준 * 100;
            Form1.form1.DGV_통계[6, 0].Value = 총수수료;
            Form1.form1.DGV_통계[7, 0].Value = (double)총수수료 / (double)Properties.Settings.Default.TB_월통계기준 * 100;
            Form1.form1.DGV_통계[8, 0].Value = 총세금;
            Form1.form1.DGV_통계[9, 0].Value = (double)총세금 / (double)Properties.Settings.Default.TB_월통계기준 * 100;

            int 수익 = 0;
            int 손실 = 0;
            string 수익n손실 = "-";

            while (Form1.form1.기준일매매내역_List.Count > 0)
            {
                string 종목명 = Form1.form1.기준일매매내역_List[0].Split('^')[0];

                List<string> 종목_List = Form1.form1.기준일매매내역_List.FindAll(o => o.Contains(종목명));

                if (종목_List.Count > 0)
                {
                    double 매수금액 = 0;
                    double 매도금액 = 0;
                    double 당일매도손익 = 0;
                    double 당일매매수수료 = 0;
                    double 당일매매세금 = 0;
                    int 체결가 = 0;
                    double 손익율 = 0;

                    for (int n = 0; n < 종목_List.Count; n++)
                    {
                        string 내역 = 종목_List[n];

                        매수금액 = 매수금액 + double.Parse(내역.Split('^')[1]);
                        매도금액 = 매도금액 + double.Parse(내역.Split('^')[2]);
                        당일매도손익 = 당일매도손익 + double.Parse(내역.Split('^')[3]);
                        당일매매수수료 = 당일매매수수료 + double.Parse(내역.Split('^')[4]);
                        당일매매세금 = 당일매매세금 + double.Parse(내역.Split('^')[5]);
                        체결가 = 체결가 + int.Parse(내역.Split('^')[6]);

                        string 손익 = 내역.Split('^')[7];
                        if (손익.StartsWith("+"))
                            손익 = 손익.Substring(1);

                        손익율 = 손익율 + double.Parse(손익);

                        if (n + 1 == 종목_List.Count)
                        {
                            체결가 = 체결가 / 종목_List.Count;
                            손익율 = 손익율 / 종목_List.Count;
                        }

                        Form1.form1.기준일매매내역_List.Remove(내역);
                    }


                    Form1.form1.DGV_통계B.Rows.Insert(0);
                    Form1.form1.DGV_통계B[0, 0].Value = 종목명;
                    Form1.form1.DGV_통계B[1, 0].Value = 매수금액;
                    Form1.form1.DGV_통계B[2, 0].Value = (double)매수금액 / (double)Properties.Settings.Default.TB_일통계기준 * 100;
                    Form1.form1.DGV_통계B[3, 0].Value = 매도금액;
                    Form1.form1.DGV_통계B[4, 0].Value = (double)매도금액 / (double)Properties.Settings.Default.TB_일통계기준 * 100;
                    Form1.form1.DGV_통계B[5, 0].Value = 당일매도손익;
                    Form1.form1.DGV_통계B[6, 0].Value = (double)당일매도손익 / (double)Properties.Settings.Default.TB_일통계기준 * 100;
                    Form1.form1.DGV_통계B[7, 0].Value = 당일매매수수료 + 당일매매세금;
                    Form1.form1.DGV_통계B[8, 0].Value = (double)(당일매매수수료 + 당일매매세금) / (double)Properties.Settings.Default.TB_일통계기준 * 100;
                    Form1.form1.DGV_통계B[9, 0].Value = 체결가;
                    Form1.form1.DGV_통계B[10, 0].Value = 손익율;

                    if (당일매도손익 != 0)
                    {
                        if (당일매도손익 > 0)
                        { 수익n손실 = "수익"; 수익++; }
                        else
                        { 수익n손실 = "손실"; 손실++; }
                    }

                    Form1.form1.DGV_통계B["수익n손실_통계B", 0].Value = 수익n손실;
                }
            }

            Form1.form1.DGV_통계["수익횟수_통계", 0].Value = 수익;
            Form1.form1.DGV_통계["손실횟수_통계", 0].Value = 손실;

            Form1.form1.LB_종목수.Text = "종목: " + (수익 + 손실) + " EA";

            Form1.form1.DGV_통계.CurrentCell = null;
            Form1.form1.DGV_통계B.CurrentCell = null;
        }


        public static void CBB_통계_확인()
        {
            if (Form1.form1.매매내역_List.Count > 0)
            {
                Form1.form1.종목감추기6.Hide();

                if (Form1.form1.CBB_통계.SelectedIndex > -1)
                {
                    Form1.form1.통계수익률 = false;

                    Form1.form1.DGV_통계B.Columns[0].HeaderText = "일자";
                    Form1.form1.DGV_통계B.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Form1.form1.DGV_통계B.Columns[0].Width = 45;
                    Form1.form1.DGV_통계B.Columns[1].Width = 128;
                    Form1.form1.DGV_통계B.Columns[2].Width = 90;
                    Form1.form1.DGV_통계B.Columns[3].Width = 128;
                    Form1.form1.DGV_통계B.Columns[4].Width = 90;
                    Form1.form1.DGV_통계B.Columns[5].Width = 128;
                    Form1.form1.DGV_통계B.Columns[6].Width = 90;

                    Form1.form1.DGV_통계B.Columns[7].HeaderText = "매매수수료";
                    Form1.form1.DGV_통계B.Columns[9].HeaderText = "매매세금";
                    Form1.form1.DGV_통계B.Columns[9].Width = 95;
                    Form1.form1.DGV_통계B.Columns[10].HeaderText = "세금율";

                    Form1.form1.DGV_통계.Columns[8].HeaderText = "총매매세금";
                    Form1.form1.DGV_통계.Columns[9].HeaderText = "세금율";

                    List<string> 매매월12_LIST = Form1.form1.매매내역_List.FindAll(o => o.Split('^')[0].Contains(Form1.form1.TB_통계.Text.Trim() + "12"));
                    List<string> 매매월11_LIST = Form1.form1.매매내역_List.FindAll(o => o.Split('^')[0].Contains(Form1.form1.TB_통계.Text.Trim() + "11"));
                    List<string> 매매월10_LIST = Form1.form1.매매내역_List.FindAll(o => o.Split('^')[0].Contains(Form1.form1.TB_통계.Text.Trim() + "10"));
                    List<string> 매매월9_LIST = Form1.form1.매매내역_List.FindAll(o => o.Split('^')[0].Contains(Form1.form1.TB_통계.Text.Trim() + "09"));
                    List<string> 매매월8_LIST = Form1.form1.매매내역_List.FindAll(o => o.Split('^')[0].Contains(Form1.form1.TB_통계.Text.Trim() + "08"));
                    List<string> 매매월7_LIST = Form1.form1.매매내역_List.FindAll(o => o.Split('^')[0].Contains(Form1.form1.TB_통계.Text.Trim() + "07"));
                    List<string> 매매월6_LIST = Form1.form1.매매내역_List.FindAll(o => o.Split('^')[0].Contains(Form1.form1.TB_통계.Text.Trim() + "06"));
                    List<string> 매매월5_LIST = Form1.form1.매매내역_List.FindAll(o => o.Split('^')[0].Contains(Form1.form1.TB_통계.Text.Trim() + "05"));
                    List<string> 매매월4_LIST = Form1.form1.매매내역_List.FindAll(o => o.Split('^')[0].Contains(Form1.form1.TB_통계.Text.Trim() + "04"));
                    List<string> 매매월3_LIST = Form1.form1.매매내역_List.FindAll(o => o.Split('^')[0].Contains(Form1.form1.TB_통계.Text.Trim() + "03"));
                    List<string> 매매월2_LIST = Form1.form1.매매내역_List.FindAll(o => o.Split('^')[0].Contains(Form1.form1.TB_통계.Text.Trim() + "02"));
                    List<string> 매매월1_LIST = Form1.form1.매매내역_List.FindAll(o => o.Split('^')[0].Contains(Form1.form1.TB_통계.Text.Trim() + "01"));

                    Form1.form1.DGV_통계.Rows.Clear();
                    Form1.form1.DGV_통계B.Rows.Clear();

                    int 수익 = 0;
                    int 손실 = 0;
                    long Month_01 = 0;
                    long Month_02 = 0;
                    long Month_03 = 0;
                    long Month_04 = 0;
                    long Month_05 = 0;
                    long Month_06 = 0;
                    long Month_07 = 0;
                    long Month_08 = 0;
                    long Month_09 = 0;
                    long Month_10 = 0;
                    long Month_11 = 0;
                    long Month_12 = 0;

                    if (Form1.form1.CBB_통계.SelectedIndex == 0)
                    {
                        Form1.form1.DGV_통계.Rows.Insert(0);
                        Form1.form1.DGV_통계["총매수금액_통계", 0].Value = long.Parse(Form1.form1.매매내역_List[0].Split('^')[0]);
                        Form1.form1.DGV_통계["매수회전_통계", 0].Value = (double.Parse(Form1.form1.매매내역_List[0].Split('^')[0]) / (double)Properties.Settings.Default.TB_월통계기준 * 100);
                        Form1.form1.DGV_통계["총매도금액_통계", 0].Value = long.Parse(Form1.form1.매매내역_List[0].Split('^')[1]);
                        Form1.form1.DGV_통계["매도회전_통계", 0].Value = (double.Parse(Form1.form1.매매내역_List[0].Split('^')[1]) / (double)Properties.Settings.Default.TB_월통계기준 * 100);
                        Form1.form1.DGV_통계["총실현손익_통계", 0].Value = long.Parse(Form1.form1.매매내역_List[0].Split('^')[2]);
                        Form1.form1.DGV_통계["실현손익율_통계", 0].Value = (double.Parse(Form1.form1.매매내역_List[0].Split('^')[2]) / (double)Properties.Settings.Default.TB_월통계기준 * 100);
                        Form1.form1.DGV_통계["총매매수수료_통계", 0].Value = long.Parse(Form1.form1.매매내역_List[0].Split('^')[3]);
                        Form1.form1.DGV_통계["수수료율_통계", 0].Value = (double.Parse(Form1.form1.매매내역_List[0].Split('^')[3]) / (double)Properties.Settings.Default.TB_월통계기준 * 100);
                        Form1.form1.DGV_통계["총매매세금_통계", 0].Value = long.Parse(Form1.form1.매매내역_List[0].Split('^')[4]);
                        Form1.form1.DGV_통계["세금율_통계", 0].Value = (double.Parse(Form1.form1.매매내역_List[0].Split('^')[4]) / (double)Properties.Settings.Default.TB_월통계기준 * 100);

                        수익 = 0;
                        손실 = 0;
                        Month_01 = 0;
                        Month_02 = 0;
                        Month_03 = 0;
                        Month_04 = 0;
                        Month_05 = 0;
                        Month_06 = 0;
                        Month_07 = 0;
                        Month_08 = 0;
                        Month_09 = 0;
                        Month_10 = 0;
                        Month_11 = 0;
                        Month_12 = 0;

                        Form1.form1.DGV_통계B.Rows.Insert(0);
                        Form1.form1.DGV_통계B["일자_통계B", 0].Value = "12 월";
                        if (매매월12_LIST.Count == 0) 초기화(); else 월통계Insert(매매월12_LIST, true, 12);

                        Form1.form1.DGV_통계B.Rows.Insert(0);
                        Form1.form1.DGV_통계B["일자_통계B", 0].Value = "11 월";
                        if (매매월11_LIST.Count == 0) 초기화(); else 월통계Insert(매매월11_LIST, true, 11);

                        Form1.form1.DGV_통계B.Rows.Insert(0);
                        Form1.form1.DGV_통계B["일자_통계B", 0].Value = "10 월";
                        if (매매월10_LIST.Count == 0) 초기화(); else 월통계Insert(매매월10_LIST, true, 10);

                        Form1.form1.DGV_통계B.Rows.Insert(0);
                        Form1.form1.DGV_통계B["일자_통계B", 0].Value = "09 월";
                        if (매매월9_LIST.Count == 0) 초기화(); else 월통계Insert(매매월9_LIST, true, 9);

                        Form1.form1.DGV_통계B.Rows.Insert(0);
                        Form1.form1.DGV_통계B["일자_통계B", 0].Value = "08 월";
                        if (매매월8_LIST.Count == 0) 초기화(); else 월통계Insert(매매월8_LIST, true, 8);

                        Form1.form1.DGV_통계B.Rows.Insert(0);
                        Form1.form1.DGV_통계B["일자_통계B", 0].Value = "07 월";
                        if (매매월7_LIST.Count == 0) 초기화(); else 월통계Insert(매매월7_LIST, true, 7);

                        Form1.form1.DGV_통계B.Rows.Insert(0);
                        Form1.form1.DGV_통계B["일자_통계B", 0].Value = "06 월";
                        if (매매월6_LIST.Count == 0) 초기화(); else 월통계Insert(매매월6_LIST, true, 6);

                        Form1.form1.DGV_통계B.Rows.Insert(0);
                        Form1.form1.DGV_통계B["일자_통계B", 0].Value = "05 월";
                        if (매매월5_LIST.Count == 0) 초기화(); else 월통계Insert(매매월5_LIST, true, 5);

                        Form1.form1.DGV_통계B.Rows.Insert(0);
                        Form1.form1.DGV_통계B["일자_통계B", 0].Value = "04 월";
                        if (매매월4_LIST.Count == 0) 초기화(); else 월통계Insert(매매월4_LIST, true, 4);

                        Form1.form1.DGV_통계B.Rows.Insert(0);
                        Form1.form1.DGV_통계B["일자_통계B", 0].Value = "03 월";
                        if (매매월3_LIST.Count == 0) 초기화(); else 월통계Insert(매매월3_LIST, true, 3);

                        Form1.form1.DGV_통계B.Rows.Insert(0);
                        Form1.form1.DGV_통계B["일자_통계B", 0].Value = "02 월";
                        if (매매월2_LIST.Count == 0) 초기화(); else 월통계Insert(매매월2_LIST, true, 2);

                        Form1.form1.DGV_통계B.Rows.Insert(0);
                        Form1.form1.DGV_통계B["일자_통계B", 0].Value = "01 월";
                        if (매매월1_LIST.Count == 0) 초기화(); else 월통계Insert(매매월1_LIST, true, 1);

                        Form1.form1.DGV_통계["수익횟수_통계", 0].Value = 수익;
                        Form1.form1.DGV_통계["손실횟수_통계", 0].Value = 손실;

                        if (true)
                        {
                            Form1.form1.chart_Month.Series[0].Points.Clear();
                            Form1.form1.chart_Month.Series[1].Points.Clear();

                            Form1.form1.chart_Month.Series[0].Points.AddXY("01월", Month_01);
                            Form1.form1.chart_Month.Series[0].Points.AddXY("02월", Month_02);
                            Form1.form1.chart_Month.Series[0].Points.AddXY("03월", Month_03);
                            Form1.form1.chart_Month.Series[0].Points.AddXY("04월", Month_04);
                            Form1.form1.chart_Month.Series[0].Points.AddXY("05월", Month_05);
                            Form1.form1.chart_Month.Series[0].Points.AddXY("06월", Month_06);
                            Form1.form1.chart_Month.Series[0].Points.AddXY("07월", Month_07);
                            Form1.form1.chart_Month.Series[0].Points.AddXY("08월", Month_08);
                            Form1.form1.chart_Month.Series[0].Points.AddXY("09월", Month_09);
                            Form1.form1.chart_Month.Series[0].Points.AddXY("10월", Month_10);
                            Form1.form1.chart_Month.Series[0].Points.AddXY("11월", Month_11);
                            Form1.form1.chart_Month.Series[0].Points.AddXY("12월", Month_12);

                            if (Month_01 < 0) Form1.form1.chart_Month.Series[0].Points[0].Color = Color.Blue;
                            if (Month_02 < 0) Form1.form1.chart_Month.Series[0].Points[1].Color = Color.Blue;
                            if (Month_03 < 0) Form1.form1.chart_Month.Series[0].Points[2].Color = Color.Blue;
                            if (Month_04 < 0) Form1.form1.chart_Month.Series[0].Points[3].Color = Color.Blue;
                            if (Month_05 < 0) Form1.form1.chart_Month.Series[0].Points[4].Color = Color.Blue;
                            if (Month_06 < 0) Form1.form1.chart_Month.Series[0].Points[5].Color = Color.Blue;
                            if (Month_07 < 0) Form1.form1.chart_Month.Series[0].Points[6].Color = Color.Blue;
                            if (Month_08 < 0) Form1.form1.chart_Month.Series[0].Points[7].Color = Color.Blue;
                            if (Month_09 < 0) Form1.form1.chart_Month.Series[0].Points[8].Color = Color.Blue;
                            if (Month_10 < 0) Form1.form1.chart_Month.Series[0].Points[9].Color = Color.Blue;
                            if (Month_11 < 0) Form1.form1.chart_Month.Series[0].Points[10].Color = Color.Blue;
                            if (Month_12 < 0) Form1.form1.chart_Month.Series[0].Points[11].Color = Color.Blue;

                            long 합1 = Month_01;
                            long 합2 = 합1 + Month_02;
                            long 합3 = 합2 + Month_03;
                            long 합4 = 합3 + Month_04;
                            long 합5 = 합4 + Month_05;
                            long 합6 = 합5 + Month_06;
                            long 합7 = 합6 + Month_07;
                            long 합8 = 합7 + Month_08;
                            long 합9 = 합8 + Month_09;
                            long 합10 = 합9 + Month_10;
                            long 합11 = 합10 + Month_11;
                            long 합12 = 합11 + Month_12;

                            Form1.form1.chart_Month.Series[1].Points.AddXY("01월", 합1);
                            Form1.form1.chart_Month.Series[1].Points.AddXY("02월", 합2);
                            Form1.form1.chart_Month.Series[1].Points.AddXY("03월", 합3);
                            Form1.form1.chart_Month.Series[1].Points.AddXY("04월", 합4);
                            Form1.form1.chart_Month.Series[1].Points.AddXY("05월", 합5);
                            Form1.form1.chart_Month.Series[1].Points.AddXY("06월", 합6);
                            Form1.form1.chart_Month.Series[1].Points.AddXY("07월", 합7);
                            Form1.form1.chart_Month.Series[1].Points.AddXY("08월", 합8);
                            Form1.form1.chart_Month.Series[1].Points.AddXY("09월", 합9);
                            Form1.form1.chart_Month.Series[1].Points.AddXY("10월", 합10);
                            Form1.form1.chart_Month.Series[1].Points.AddXY("11월", 합11);
                            Form1.form1.chart_Month.Series[1].Points.AddXY("12월", 합12);

                            if (합1 < 0) Form1.form1.chart_Month.Series[1].Points[0].Color = Color.MediumSeaGreen;
                            if (합2 < 0) Form1.form1.chart_Month.Series[1].Points[1].Color = Color.MediumSeaGreen;
                            if (합3 < 0) Form1.form1.chart_Month.Series[1].Points[2].Color = Color.MediumSeaGreen;
                            if (합4 < 0) Form1.form1.chart_Month.Series[1].Points[3].Color = Color.MediumSeaGreen;
                            if (합5 < 0) Form1.form1.chart_Month.Series[1].Points[4].Color = Color.MediumSeaGreen;
                            if (합6 < 0) Form1.form1.chart_Month.Series[1].Points[5].Color = Color.MediumSeaGreen;
                            if (합7 < 0) Form1.form1.chart_Month.Series[1].Points[6].Color = Color.MediumSeaGreen;
                            if (합8 < 0) Form1.form1.chart_Month.Series[1].Points[7].Color = Color.MediumSeaGreen;
                            if (합9 < 0) Form1.form1.chart_Month.Series[1].Points[8].Color = Color.MediumSeaGreen;
                            if (합10 < 0) Form1.form1.chart_Month.Series[1].Points[9].Color = Color.MediumSeaGreen;
                            if (합11 < 0) Form1.form1.chart_Month.Series[1].Points[10].Color = Color.MediumSeaGreen;
                            if (합12 < 0) Form1.form1.chart_Month.Series[1].Points[11].Color = Color.MediumSeaGreen;

                            Form1.form1.종목감추기6.Hide();
                        }
                        Form1.form1.DGV_통계B.Size = new Size(1104, 298);

                        Form1.form1.chart_Day.Location = new Point(1102, -10);
                        Form1.form1.chart_Day.Size = new Size(826, 1000);

                        Form1.form1.chart_Month.Location = new Point(-25, 372);
                        Form1.form1.chart_Month.Size = new Size(1169, 640);

                        Form1.form1.chart_Month.BringToFront();
                        Form1.form1.chart_Day.BringToFront();
                        Form1.form1.chart_Day.Series[0].Points.Clear();
                        Form1.form1.chart_Day.Series[1].Points.Clear();
                    }

                    else if (Form1.form1.CBB_통계.SelectedIndex == 1)//1월
                    {
                        월통계Insert(매매월1_LIST, false, 0);
                    }
                    else if (Form1.form1.CBB_통계.SelectedIndex == 2)
                    {
                        월통계Insert(매매월2_LIST, false, 0);
                    }
                    else if (Form1.form1.CBB_통계.SelectedIndex == 3)
                    {
                        월통계Insert(매매월3_LIST, false, 0);
                    }
                    else if (Form1.form1.CBB_통계.SelectedIndex == 4)
                    {
                        월통계Insert(매매월4_LIST, false, 0);
                    }
                    else if (Form1.form1.CBB_통계.SelectedIndex == 5)
                    {
                        월통계Insert(매매월5_LIST, false, 0);
                    }
                    else if (Form1.form1.CBB_통계.SelectedIndex == 6)
                    {
                        월통계Insert(매매월6_LIST, false, 0);
                    }
                    else if (Form1.form1.CBB_통계.SelectedIndex == 7)
                    {
                        월통계Insert(매매월7_LIST, false, 0);
                    }
                    else if (Form1.form1.CBB_통계.SelectedIndex == 8)
                    {
                        월통계Insert(매매월8_LIST, false, 0);
                    }
                    else if (Form1.form1.CBB_통계.SelectedIndex == 9)
                    {
                        월통계Insert(매매월9_LIST, false, 0);
                    }
                    else if (Form1.form1.CBB_통계.SelectedIndex == 10)
                    {
                        월통계Insert(매매월10_LIST, false, 0);
                    }
                    else if (Form1.form1.CBB_통계.SelectedIndex == 11)
                    {
                        월통계Insert(매매월11_LIST, false, 0);
                    }
                    else if (Form1.form1.CBB_통계.SelectedIndex == 12)
                    {
                        월통계Insert(매매월12_LIST, false, 0);
                    }

                    if (Form1.form1.CBB_통계.SelectedIndex != 0)
                    {
                        Form1.form1.DGV_통계B.Size = new Size(1104, 500);

                        Form1.form1.chart_Month.Location = new Point(1102, -3);
                        Form1.form1.chart_Month.Size = new Size(826, 575);

                        Form1.form1.chart_Day.Location = new Point(-75, 550);
                        Form1.form1.chart_Day.Size = new Size(2047, 440);
                    }

                    void 월통계Insert(List<String> List, bool 년통계, int 월)
                    {
                        long 매수금 = 0;
                        long 매도금 = 0;
                        long 실현손익 = 0;
                        long 수수료 = 0;
                        long 세금 = 0;
                        long 총매수금 = 0;
                        long 총매도금 = 0;
                        long 총실현손익 = 0;
                        long 총수수료 = 0;
                        long 총세금 = 0;

                        string 수익n손실 = "-";
                        int index = 0;

                        if (년통계)
                        {
                            for (int i = 0; i < List.Count; i++)
                            {
                                매수금 = 매수금 + long.Parse(List[i].Split('^')[1]);
                                매도금 = 매도금 + long.Parse(List[i].Split('^')[2]);
                                실현손익 = 실현손익 + long.Parse(List[i].Split('^')[3]);
                                수수료 = 수수료 + long.Parse(List[i].Split('^')[4]);
                                세금 = 세금 + long.Parse(List[i].Split('^')[5]);
                            }

                            월통계Print((double)Properties.Settings.Default.TB_월통계기준, false);

                            if (월 == 1) Month_01 = 실현손익;
                            if (월 == 2) Month_02 = 실현손익;
                            if (월 == 3) Month_03 = 실현손익;
                            if (월 == 4) Month_04 = 실현손익;
                            if (월 == 5) Month_05 = 실현손익;
                            if (월 == 6) Month_06 = 실현손익;
                            if (월 == 7) Month_07 = 실현손익;
                            if (월 == 8) Month_08 = 실현손익;
                            if (월 == 9) Month_09 = 실현손익;
                            if (월 == 10) Month_10 = 실현손익;
                            if (월 == 11) Month_11 = 실현손익;
                            if (월 == 12) Month_12 = 실현손익;
                        }
                        else
                        {
                            if (List.Count > 0)
                            {
                                for (int i = 0; i < List.Count; i++)
                                {
                                    매수금 = long.Parse(List[i].Split('^')[1]);
                                    매도금 = long.Parse(List[i].Split('^')[2]);
                                    실현손익 = long.Parse(List[i].Split('^')[3]);
                                    수수료 = long.Parse(List[i].Split('^')[4]);
                                    세금 = long.Parse(List[i].Split('^')[5]);

                                    총매수금 = 총매수금 + 매수금;
                                    총매도금 = 총매도금 + 매도금;
                                    총실현손익 = 총실현손익 + 실현손익;
                                    총수수료 = 총수수료 + 수수료;
                                    총세금 = 총세금 + 세금;
                                    index = i;
                                    월통계Print((double)Properties.Settings.Default.TB_일통계기준, true);
                                }
                            }
                            else
                            {
                                Form1.form1.DGV_통계B.Rows.Insert(0);
                                초기화();
                            }

                            통계합Print();

                            long Sum = 0;

                            for (int i = 0; i < Form1.form1.chart_Day.Series[0].Points.Count; i++)
                            {
                                if (long.Parse(Form1.form1.chart_Day.Series[0].Points[i].YValues[0].ToString()) < 0) Form1.form1.chart_Day.Series[0].Points[i].Color = Color.Blue;

                                Sum = long.Parse(Form1.form1.chart_Day.Series[0].Points[i].YValues[0].ToString()) + Sum;
                                Form1.form1.chart_Day.Series[1].Points.AddXY(Form1.form1.chart_Day.Series[0].Points[i].Label, Sum);
                                if (long.Parse(Form1.form1.chart_Day.Series[1].Points[i].YValues[0].ToString()) < 0) Form1.form1.chart_Day.Series[1].Points[i].Color = Color.MediumSeaGreen;
                            }


                        }

                        void 월통계Print(double 통계기준, bool ChartPrint)
                        {
                            if (!년통계)
                            {
                                Form1.form1.DGV_통계B.Rows.Insert(0);
                                Form1.form1.DGV_통계B["일자_통계B", 0].Value = List[index].Split('^')[0].Substring(6) + " 일";
                            }
                            Form1.form1.DGV_통계B["매수금액_통계B", 0].Value = 매수금;
                            Form1.form1.DGV_통계B["매수회전율_통계B", 0].Value = ((double)매수금 / 통계기준 * 100);
                            Form1.form1.DGV_통계B["매도금액_통계B", 0].Value = 매도금;
                            Form1.form1.DGV_통계B["매도회전율_통계B", 0].Value = ((double)매도금 / 통계기준 * 100);
                            Form1.form1.DGV_통계B["실현손익_통계B", 0].Value = 실현손익;
                            Form1.form1.DGV_통계B["실현손익율_통계B", 0].Value = ((double)실현손익 / 통계기준 * 100);
                            Form1.form1.DGV_통계B["매매수수료_통계B", 0].Value = 수수료;
                            Form1.form1.DGV_통계B["매매수수료율_통계B", 0].Value = ((double)수수료 / 통계기준 * 100);
                            Form1.form1.DGV_통계B["매매세금_통계B", 0].Value = 세금;
                            Form1.form1.DGV_통계B["매매세금율_통계B", 0].Value = ((double)세금 / 통계기준 * 100);

                            if (실현손익 != 0)
                            {
                                if (실현손익 > 0)
                                { 수익n손실 = "수익"; 수익++; }
                                else
                                { 수익n손실 = "손실"; 손실++; }
                            }

                            Form1.form1.DGV_통계B["수익n손실_통계B", 0].Value = 수익n손실;

                            if (ChartPrint)
                            {
                                Form1.form1.chart_Day.Series[0].Points.InsertXY(0, List[index].Split('^')[0].Substring(6) + " 일", 실현손익);
                            }
                        }

                        void 통계합Print()
                        {
                            Form1.form1.DGV_통계.Rows.Insert(0);
                            Form1.form1.DGV_통계["총매수금액_통계", 0].Value = 총매수금;
                            Form1.form1.DGV_통계["매수회전_통계", 0].Value = ((double)총매수금 / (double)Properties.Settings.Default.TB_월통계기준 * 100);
                            Form1.form1.DGV_통계["총매도금액_통계", 0].Value = 총매도금;
                            Form1.form1.DGV_통계["매도회전_통계", 0].Value = ((double)총매도금 / (double)Properties.Settings.Default.TB_월통계기준 * 100);
                            Form1.form1.DGV_통계["총실현손익_통계", 0].Value = 총실현손익;
                            Form1.form1.DGV_통계["실현손익율_통계", 0].Value = ((double)총실현손익 / (double)Properties.Settings.Default.TB_월통계기준 * 100);
                            Form1.form1.DGV_통계["총매매수수료_통계", 0].Value = 총수수료;
                            Form1.form1.DGV_통계["수수료율_통계", 0].Value = ((double)총수수료 / (double)Properties.Settings.Default.TB_월통계기준 * 100);
                            Form1.form1.DGV_통계["총매매세금_통계", 0].Value = 총세금;
                            Form1.form1.DGV_통계["세금율_통계", 0].Value = ((double)총세금 / (double)Properties.Settings.Default.TB_월통계기준 * 100);
                            Form1.form1.DGV_통계["수익횟수_통계", 0].Value = 수익;
                            Form1.form1.DGV_통계["손실횟수_통계", 0].Value = 손실;
                        }
                    }

                    void 초기화()
                    {
                        Form1.form1.DGV_통계B["매수금액_통계B", 0].Value = (long)0;
                        Form1.form1.DGV_통계B["매수회전율_통계B", 0].Value = (double)0;
                        Form1.form1.DGV_통계B["매도금액_통계B", 0].Value = (long)0;
                        Form1.form1.DGV_통계B["매도회전율_통계B", 0].Value = (double)0;
                        Form1.form1.DGV_통계B["실현손익_통계B", 0].Value = (long)0;
                        Form1.form1.DGV_통계B["실현손익율_통계B", 0].Value = (double)0;
                        Form1.form1.DGV_통계B["매매수수료_통계B", 0].Value = (long)0;
                        Form1.form1.DGV_통계B["매매수수료율_통계B", 0].Value = (double)0;
                        Form1.form1.DGV_통계B["매매세금_통계B", 0].Value = (long)0;
                        Form1.form1.DGV_통계B["매매세금율_통계B", 0].Value = (double)0;
                        Form1.form1.DGV_통계B["수익n손실_통계B", 0].Value = "-";
                    }

                    Form1.form1.LB_종목수.Text = "종목: " + 0 + " EA";
                }

                Form1.form1.DGV_통계.CurrentCell = null;
                Form1.form1.DGV_통계B.CurrentCell = null;
                Form1.form1.DGV_통계B.Sort(Form1.form1.DGV_통계B.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
            }
            else
            {
                Form1.AutoClosingAlram("매매내역 조회를 먼저 하기 바랍니다.", "조회순서알림", 5, "동작");
                Form1.form1.CBB_통계.SelectedIndex = -1;
            }
        }

        public static void BT_기준일별확인()
        {
            var thread = new Thread(
           () =>
           {
               Form1.form1.Invoke((MethodInvoker)delegate ()  //      
               {
                   using (new CenterWinDialog(Form1.form1))
                       if (MessageBox.Show("기준일매매내역을 요청 하시겠습니까 ? ", "서버요청", MessageBoxButtons.YesNo) == DialogResult.Yes)
                       {
                           기준일매매내역();
                       }
               });
           });
            thread.Start();
        }

        public static void 기준일매매내역()
        {
            Form1.form1.Invoke((MethodInvoker)delegate ()  //      
            {
                string 계좌번호 = Form1.form1.account_comboBox.Text.Trim();

                Form1.form1.기준일매매내역_List.Clear();

                Form1.form1.DGV_통계.Rows.Clear();
                Form1.form1.DGV_통계B.Rows.Clear();

                Form1.form1.DGV_통계B.Columns[0].HeaderText = "종목명";
                Form1.form1.DGV_통계B.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                Form1.form1.DGV_통계B.Columns[0].Width = 126;
                Form1.form1.DGV_통계B.Columns[1].Width = 105;
                Form1.form1.DGV_통계B.Columns[2].Width = 80;
                Form1.form1.DGV_통계B.Columns[3].Width = 105;
                Form1.form1.DGV_통계B.Columns[4].Width = 80;
                Form1.form1.DGV_통계B.Columns[6].Width = 80;

                Form1.form1.DGV_통계B.Columns[7].HeaderText = "수수료_세금";
                Form1.form1.DGV_통계B.Columns[9].HeaderText = "매도평균가";
                Form1.form1.DGV_통계B.Columns[9].Width = 90;
                Form1.form1.DGV_통계B.Columns[10].HeaderText = "수익률";

                Form1.form1.통계수익률 = true;

                Form1.form1.TR_opt10073_일자별종목별실현손익요청(계좌번호);


            });
        }

        public static void 매매내역확인()
        {
            long.TryParse(Form1.form1.TB_월통계기준.Text.Replace(",", ""), out long 월통계기준);
            if (월통계기준 == 0) 월통계기준 = Properties.Settings.Default.MT_principal;
            Properties.Settings.Default.TB_월통계기준 = 월통계기준;

            long.TryParse(Form1.form1.TB_일통계기준.Text.Replace(",", ""), out long 일통계기준);
            if (월통계기준 == 0) 일통계기준 = Properties.Settings.Default.MT_sonik_price;
            Properties.Settings.Default.TB_일통계기준 = 일통계기준;

            Form1.form1.TB_월통계기준.Text = Properties.Settings.Default.TB_월통계기준.ToString("N0");
            Form1.form1.TB_일통계기준.Text = Properties.Settings.Default.TB_일통계기준.ToString("N0");

            string 계좌번호 = Form1.form1.account_comboBox.Text.Trim();

            var thread = new Thread(
            () =>
            {
                Form1.form1.Invoke((MethodInvoker)delegate ()  //      
                {
                    using (new CenterWinDialog(Form1.form1))
                        if (MessageBox.Show("매매내역을 요청 하시겠습니까 ? ", "서버요청", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Form1.form1.매매내역_List.Clear();
                            Form1.form1.통계_opt10074_일자별실현손익요청(계좌번호);
                        }
                });
            });
            thread.Start();
        }

    }
}
