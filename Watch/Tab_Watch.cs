using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace 지니_64
{
    class Tab_Watch
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        ///////////////////             WAtch 검색식           //////////////////

        public static void Watch_In_Out(string State_ID, string Itemcode, string condition, string SearchTime) // 진입 & 이탈
        {
            if (Properties.Settings.Default.CBB_Watch_ID_A > 0 || Properties.Settings.Default.CBB_Watch_ID_B > 0 || Properties.Settings.Default.CBB_Watch_ID_C > 0 || Properties.Settings.Default.CBB_Watch_ID_D > 0)
            {
                Market_Item Market = Form1.Market_Item_List[Itemcode];
                string name = Market.종목명;
                double LastPrice = Market.Last_price;
                double 등락율 = 0;
                int Nowprice = 0;
                double 진입후등락율 = 0;
                double 진입후최고 = 0;
                double 진입후최저 = 0;
                int 시가 = 0;
                int 고가 = 0;
                int 저가 = 0;
                string 누적거래량 = "0";
                string 전일거래량대비 = "0";
                double 누적거래대금 = 0;
                double 거래대금증감 = 0;
                string 거래회전율 = "0";
                string 시가총액 = "0";
                double 진입가 = 0;

                string para = "Watch_opt10001^" + condition + "^" + name + "^" + Itemcode; // TR요청용 데이터

                bool ID_A = false;
                bool ID_B = false;
                bool ID_C = false;
                bool ID_D = false;

                if (State_ID.Equals("I"))
                {
                    if (Properties.Settings.Default.CBB_Watch_ID_A.Equals(1)) ID_A = true;
                    if (Properties.Settings.Default.CBB_Watch_ID_B.Equals(1)) ID_B = true;
                    if (Properties.Settings.Default.CBB_Watch_ID_C.Equals(1)) ID_C = true;
                    if (Properties.Settings.Default.CBB_Watch_ID_D.Equals(1)) ID_D = true;
                }
                else if (State_ID.Equals("D"))
                {
                    if (Properties.Settings.Default.CBB_Watch_ID_A.Equals(2)) ID_A = true;
                    if (Properties.Settings.Default.CBB_Watch_ID_B.Equals(2)) ID_B = true;
                    if (Properties.Settings.Default.CBB_Watch_ID_C.Equals(2)) ID_C = true;
                    if (Properties.Settings.Default.CBB_Watch_ID_D.Equals(2)) ID_D = true;
                }

                if (Properties.Settings.Default.combo_watch_condition_A.Equals(condition) && Properties.Settings.Default.CBB_Watch_ID_A > 0)
                {
                    상태변경("A:" + Itemcode, ID_A, Properties.Settings.Default.CB_TR_A, Properties.Settings.Default.CB_Watch_log_A);
                }

                if (Properties.Settings.Default.combo_watch_condition_B.Equals(condition) && Properties.Settings.Default.CBB_Watch_ID_B > 0)
                {
                    상태변경("B:" + Itemcode, ID_B, Properties.Settings.Default.CB_TR_B, Properties.Settings.Default.CB_Watch_log_B);
                }

                if (Properties.Settings.Default.combo_watch_condition_C.Equals(condition) && Properties.Settings.Default.CBB_Watch_ID_C > 0)
                {
                    상태변경("C:" + Itemcode, ID_C, Properties.Settings.Default.CB_TR_C, Properties.Settings.Default.CB_Watch_log_C);
                }

                if (Properties.Settings.Default.combo_watch_condition_D.Equals(condition) && Properties.Settings.Default.CBB_Watch_ID_D > 0)
                {
                    상태변경("D:" + Itemcode, ID_D, Properties.Settings.Default.CB_TR_D, Properties.Settings.Default.CB_Watch_log_D);
                }

                void 상태변경(string Item, bool ID, bool TR, bool log)
                {
                    if (ID)
                    {
                        if (!Form1.Watch_List.ContainsKey(Item))
                        {
                            Form1.Watch_List.Add(Item, new Watch(Market.Market, condition, "진입", name, 등락율, Itemcode, Nowprice, LastPrice, 진입후등락율, 진입후최고, 진입후최저, 누적거래량, 누적거래대금, 시가, 고가, 저가, 거래대금증감, 전일거래량대비, 거래회전율, 시가총액, 진입가, SearchTime, 0, "-", 0, 0));

                            if (Market.현재가 < 1)
                            {
                                if (TR)
                                {
                                    if (Form1.Run_condition_List.Contains(condition))
                                    {
                                        신규조회 중복 = Form1.신규조회_List.Find(o => o.ItemCode.Equals(Itemcode));
                                        if (중복 == null)
                                        {
                                            신규조회 Add = new 신규조회(Itemcode, para, 0, condition);
                                            Form1.신규조회_List.Add(Add);

                                            Form1.TR_catch_Item_List.Enqueue(Itemcode + ";" + para);
                                        }
                                    }
                                }

                                Method.실시간시세등록(Itemcode);
                            }
                            else
                            {
                                Watch watch = Form1.Watch_List[Item];
                                watch.Nowprice = Market.현재가;
                                watch.진입가 = Market.현재가;
                                watch.등락율 = Market.등락율;
                            }

                            if (Form1.form1.CB_Watch_log_A.Checked) Form1.동작_Log("[종목 검색 _ 신규검색] 시장구분: " + Market.Market + " 종목명: " + Market.종목명 + " 검색식: " + condition);
                        }
                        else
                        {
                            Form1.Watch_List[Item].State = "재진입";
                            if (log) Form1.동작_Log("[종목 검색 _ 재검색] 시장구분: " + Market.Market + " 종목명: " + Market.종목명 + " 검색식: " + condition);
                        }
                    }
                    else
                    {
                        if (Form1.Watch_List.ContainsKey(Item))
                        {
                            Watch watch = Form1.Watch_List[Item];
                            watch.State = "이탈";
                            watch.timer = 0;
                        }
                    }
                }
            }
        }


        public static void Watch_update(string 종목코드, double 등락율, double 현재가, string 누적거래량, double 누적거래대금, int 시가, int 고가, int 저가, double 거래대금증감, string 전일거래량대비, string 거래회전율, string 시가총액) // Watch 검색식 그리드뷰
        {
            누적거래대금 = Math.Truncate(누적거래대금 / 100);  // 10단위 절상 49.5 > 49.0 * 100 = 4900.0
            거래대금증감 = Math.Truncate(거래대금증감 / 100000000);

            Watch watch = null;

            if (Form1.Watch_List.ContainsKey("A:" + 종목코드))
            {
                watch = Form1.Watch_List["A:" + 종목코드];
                Updata(Properties.Settings.Default.TB_watch_익절_A, Properties.Settings.Default.TB_watch_손절_A);
            }
            if (Form1.Watch_List.ContainsKey("B:" + 종목코드))
            {
                watch = Form1.Watch_List["B:" + 종목코드];
                Updata(Properties.Settings.Default.TB_watch_익절_B, Properties.Settings.Default.TB_watch_손절_B);
            }
            if (Form1.Watch_List.ContainsKey("C:" + 종목코드))
            {
                watch = Form1.Watch_List["C:" + 종목코드];
                Updata(Properties.Settings.Default.TB_watch_익절_C, Properties.Settings.Default.TB_watch_손절_C);
            }
            if (Form1.Watch_List.ContainsKey("D:" + 종목코드))
            {
                watch = Form1.Watch_List["D:" + 종목코드];
                Updata(Properties.Settings.Default.TB_watch_익절_D, Properties.Settings.Default.TB_watch_손절_D);
            }

            void Updata(double 익절, double 손절)
            {
                watch.등락율 = 등락율;
                watch.Nowprice = 현재가;
                watch.시가 = 시가;
                watch.고가 = 고가;
                watch.저가 = 저가;
                watch.누적거래량 = 누적거래량;
                watch.전일거래량대비 = 전일거래량대비;
                watch.누적거래대금 = 누적거래대금;
                watch.거래대금증감 = 거래대금증감;
                watch.거래회전율 = 거래회전율;
                watch.시가총액 = 시가총액;

                if (watch.진입가 < 1)
                {
                    watch.진입가 = watch.Nowprice;
                }

                double 진입후최고 = watch.진입후최고;
                double 진입후최저 = watch.진입후최저;
                double 진입후등락 = 0;

                if (watch.매수가 != 0)
                {
                    double tax_ = Form1.TAX;
                    if (Form1.Market_Item_List[종목코드].Market.Equals("E")) tax_ = 0;

                    진입후등락 = Math.Truncate((((double)(watch.Nowprice - watch.매수가) / watch.매수가 * (double)100) - ((Form1.수수료 + Form1.수수료 + tax_) * 100)) * 100) / 100;
                }

                watch.진입후등락율 = 진입후등락;

                if (0 <= 진입후등락 && 진입후최고 < 진입후등락)
                {
                    watch.진입후최고 = 진입후등락;   // 실시간 최고율 갱신
                }
                if (진입후등락 <= 0 && 진입후등락 < 진입후최저)
                {
                    watch.진입후최저 = 진입후등락;   // 실시간 최저율 갱신
                }

                if (watch.매매.Equals("매수"))
                {
                    if (진입후최고 >= 익절)
                    {
                        watch.매매 = "익절";
                    }
                    if (진입후최저 <= 손절)
                    {
                        watch.매매 = "손절";
                    }
                }
            }
        }

        public static void DGV_watch_print(string 위치)
        {
            int MaxRow = 0;
            int.TryParse(Form1.form1.TB_WatchRow_A.Text, out int max);
            MaxRow = Math.Abs(max);
            DataGridView DGV = Form1.form1.dataGridView_watch_A;
            CheckBox CB_watch_remove = Form1.form1.CB_watch_remove_A;

            if (위치.Equals("B:"))
            {
                int.TryParse(Form1.form1.TB_WatchRow_B.Text, out int max_B);
                MaxRow = Math.Abs(max_B);
                DGV = Form1.form1.dataGridView_watch_B;
                CB_watch_remove = Form1.form1.CB_watch_remove_B;
            }
            if (위치.Equals("C:"))
            {
                int.TryParse(Form1.form1.TB_WatchRow_C.Text, out int max_C);
                MaxRow = Math.Abs(max_C);
                DGV = Form1.form1.dataGridView_watch_C;
                CB_watch_remove = Form1.form1.CB_watch_remove_C;
            }
            if (위치.Equals("D:"))
            {
                int.TryParse(Form1.form1.TB_WatchRow_D.Text, out int max_D);
                MaxRow = Math.Abs(max_D);
                DGV = Form1.form1.dataGridView_watch_D;
                CB_watch_remove = Form1.form1.CB_watch_remove_D;
            }

            Watch watch = null;
            Dictionary<string, Watch> findResult = Form1.Watch_List.Where(o => o.Key.Contains(위치)).ToDictionary(o => o.Key, o => o.Value);
            foreach (var result in findResult.ToList())
            {
                watch = findResult[result.Key];

                if (DGV.RowCount == 0)
                {
                    if (CB_watch_remove.Checked)
                    {
                        if (watch.State.Contains("진입"))
                        {
                            인서트();
                        }
                    }
                    else
                    {
                        인서트();
                    }
                }
                else
                {
                    if (watch.State.Contains("진입"))
                    {
                        for (int n = 0; n < DGV.RowCount; n++)
                        {
                            if (DGV[23, n].Value.Equals(watch.Code))
                            {
                                DGV.Rows.Remove(DGV.Rows[n]);
                                break;
                            }
                        }

                        인서트();
                    }
                }

                void 인서트()
                {
                    DGV.Rows.Insert(0);
                    DGV[23, 0].Value = watch.Code;
                    print_watch(0);

                    if (DGV.RowCount > MaxRow)
                    {
                        int count = DGV.RowCount - MaxRow;

                        for (int n = 0; n < count; n++)
                        {
                            DGV.Rows.Remove(DGV.Rows[DGV.RowCount - 1]);
                        }
                    }

                    DGV.CurrentCell = null;
                }
            }

            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                watch = Form1.Watch_List[위치 + DGV[23, i].Value.ToString()];

                if (CB_watch_remove.Checked)
                {
                    if (watch.State.Contains("이탈"))
                    {
                        DGV.Rows.Remove(DGV.Rows[i]);

                        DGV.CurrentCell = null;
                    }
                    else
                    {
                        print_watch(i);
                    }
                }
                else
                {
                    print_watch(i);
                }
            }

            void print_watch(int row)
            {
                DGV[1, row].Value = watch.Market;
                DGV[2, row].Value = watch.진입시간;
                DGV[3, row].Value = watch.State;
                DGV[4, row].Value = watch.timer;
                DGV[5, row].Value = watch.Name;
                DGV[6, row].Value = watch.Nowprice;
                DGV[7, row].Value = watch.등락율;
                DGV[8, row].Value = watch.진입가;
                DGV[9, row].Value = watch.매매;
                DGV[10, row].Value = watch.매수가;
                DGV[11, row].Value = watch.진입후등락율;
                DGV[12, row].Value = watch.진입후최고;
                DGV[13, row].Value = watch.진입후최저;
                DGV[14, row].Value = watch.시가;
                DGV[15, row].Value = watch.고가;
                DGV[16, row].Value = watch.저가;
                DGV[17, row].Value = int.Parse(watch.누적거래량).ToString("N0");
                DGV[18, row].Value = watch.전일거래량대비 + "%";
                DGV[19, row].Value = watch.누적거래대금.ToString("N0") + "억";
                DGV[20, row].Value = watch.거래대금증감.ToString("N0") + "억";
                DGV[21, row].Value = watch.거래회전율;
                DGV[22, row].Value = int.Parse(watch.시가총액).ToString("N0") + "억";
            }
        }

        public static void test_save_(string sender)
        {
            ComboBox combo = null;
            DataGridView dgv = null;
            string watch = null;
            string I_D = "진입";
            string 관심그룹 = null;
            string 매수옵션 = " X ";
            string 진입유지 = null;
            string 익절 = null;
            string 손절 = null;
            string 위치 = "";

            if (sender.Equals("BT_test_save_A"))
            {
                combo = Form1.form1.combo_watch_condition_A;
                dgv = Form1.form1.dataGridView_watch_A;
                watch = "Watch_A__";
                위치 = "A:";

                if (Form1.form1.CBB_Watch_ID_A.SelectedIndex == 2)
                {
                    I_D = "이탈";
                }
                관심그룹 = Form1.form1.CBB_Watch관심_A.Text;

                if (Form1.form1.CBB_watch_trading_A.SelectedIndex == 1)
                {
                    매수옵션 = "매수(진입)";
                }
                else if (Form1.form1.CBB_watch_trading_A.SelectedIndex == 2)
                {
                    매수옵션 = "매수(재진입)";
                }
                else if (Form1.form1.CBB_watch_trading_A.SelectedIndex == 3)
                {
                    매수옵션 = "매도";
                }

                진입유지 = Form1.form1.MTB_watch_지연_A.Text + "초";
                익절 = Form1.form1.TB_watch_익절_A.Text + "%";
                손절 = Form1.form1.TB_watch_손절_A.Text + "%";
            }

            if (sender.Equals("BT_test_save_B"))
            {
                combo = Form1.form1.combo_watch_condition_B;
                dgv = Form1.form1.dataGridView_watch_B;
                watch = "Watch_B__";
                위치 = "B:";

                if (Form1.form1.CBB_Watch_ID_B.SelectedIndex == 2)
                {
                    I_D = "이탈";
                }
                관심그룹 = Form1.form1.CBB_Watch관심_B.Text;

                if (Form1.form1.CBB_watch_trading_B.SelectedIndex == 1)
                {
                    매수옵션 = "매수(진입)";
                }
                else if (Form1.form1.CBB_watch_trading_B.SelectedIndex == 2)
                {
                    매수옵션 = "매수(재진입)";
                }
                else if (Form1.form1.CBB_watch_trading_B.SelectedIndex == 3)
                {
                    매수옵션 = "매도";
                }

                진입유지 = Form1.form1.MTB_watch_지연_B.Text + "초";
                익절 = Form1.form1.TB_watch_익절_B.Text + "%";
                손절 = Form1.form1.TB_watch_손절_B.Text + "%";
            }

            if (sender.Equals("BT_test_save_C"))
            {
                combo = Form1.form1.combo_watch_condition_C;
                dgv = Form1.form1.dataGridView_watch_C;
                watch = "Watch_C__";
                위치 = "C:";

                if (Form1.form1.CBB_Watch_ID_C.SelectedIndex == 2)
                {
                    I_D = "이탈";
                }
                관심그룹 = Form1.form1.CBB_Watch관심_C.Text;

                if (Form1.form1.CBB_watch_trading_C.SelectedIndex == 1)
                {
                    매수옵션 = "매수(진입)";
                }
                else if (Form1.form1.CBB_watch_trading_C.SelectedIndex == 2)
                {
                    매수옵션 = "매수(재진입)";
                }
                else if (Form1.form1.CBB_watch_trading_C.SelectedIndex == 3)
                {
                    매수옵션 = "매도";
                }

                진입유지 = Form1.form1.MTB_watch_지연_C.Text + "초";
                익절 = Form1.form1.TB_watch_익절_C.Text + "%";
                손절 = Form1.form1.TB_watch_손절_C.Text + "%";
            }

            if (sender.Equals("BT_test_save_D"))
            {
                combo = Form1.form1.combo_watch_condition_D;
                dgv = Form1.form1.dataGridView_watch_D;
                watch = "Watch_D__";
                위치 = "D:";

                if (Form1.form1.CBB_Watch_ID_D.SelectedIndex == 2)
                {
                    I_D = "이탈";
                }
                관심그룹 = Form1.form1.CBB_Watch관심_D.Text;

                if (Form1.form1.CBB_watch_trading_D.SelectedIndex == 1)
                {
                    매수옵션 = "매수(진입)";
                }
                else if (Form1.form1.CBB_watch_trading_D.SelectedIndex == 2)
                {
                    매수옵션 = "매수(재진입)";
                }
                else if (Form1.form1.CBB_watch_trading_D.SelectedIndex == 3)
                {
                    매수옵션 = "매도";
                }

                진입유지 = Form1.form1.MTB_watch_지연_D.Text + "초";
                익절 = Form1.form1.TB_watch_익절_D.Text + "%";
                손절 = Form1.form1.TB_watch_손절_D.Text + "%";
            }

            if (dgv.RowCount > 0)
            {
                string 검색식 = combo.Text.Trim();

                try
                {
                    Excel.Application excelApp = new Excel.Application();
                    excelApp.DisplayAlerts = false;//ture일 경우 메시지 발생.

                    try
                    {
                        Excel.Workbook wb = excelApp.Workbooks.Add();//엑셀 기본 생성
                        Excel.Worksheet ws = wb.Worksheets.Add(); //기본 시트 후에 생성

                        int row = 1;

                        ws.Cells[row, 1] = "날짜";
                        ws.Cells[row, 2] = "검색식";
                        ws.Cells[row, 3] = "검색방법";
                        ws.Cells[row, 4] = "관심그룹";
                        ws.Cells[row, 5] = "매수옵션";
                        ws.Cells[row, 6] = "진입유지";
                        ws.Cells[row, 7] = "익절";
                        ws.Cells[row, 8] = "손절";

                        ws.Cells[2, 1] = DateTime.Now.ToString("yyyyMMdd");
                        ws.Cells[2, 2] = 검색식;
                        ws.Cells[2, 3] = I_D;
                        ws.Cells[2, 4] = 관심그룹;
                        ws.Cells[2, 5] = 매수옵션;
                        ws.Cells[2, 6] = 진입유지;
                        ws.Cells[2, 7] = 익절;
                        ws.Cells[2, 8] = 손절;

                        row = 4;

                        string[] head = new string[dgv.Columns.Count];
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            head[i] = dgv.Columns[i].HeaderText;
                        }

                        for (int i = 0; i < 24; i++)
                        {
                            ws.Cells[row, i + 1] = head[i];
                        }
                        row = 5;

                        int index = 0;

                        Dictionary<string, Watch> findResult = Form1.Watch_List.Where(o => o.Key.Contains(위치)).ToDictionary(o => o.Key, o => o.Value);
                        foreach (var result in findResult.ToList())
                        {
                            Watch watchitem = Form1.Watch_List[result.Key];

                            ws.Cells[row + index, 1] = index + 1;
                            ws.Cells[row + index, 2] = watchitem.Market;
                            ws.Cells[row + index, 3] = watchitem.진입시간;
                            ws.Cells[row + index, 4] = watchitem.State;
                            ws.Cells[row + index, 5] = watchitem.timer;
                            ws.Cells[row + index, 6] = watchitem.Name;
                            ws.Cells[row + index, 7] = watchitem.Nowprice;
                            ws.Cells[row + index, 8] = watchitem.등락율;
                            ws.Cells[row + index, 9] = watchitem.진입가;
                            ws.Cells[row + index, 10] = watchitem.매매;
                            ws.Cells[row + index, 11] = watchitem.매수가;
                            ws.Cells[row + index, 12] = watchitem.진입후등락율;
                            ws.Cells[row + index, 13] = watchitem.진입후최고;
                            ws.Cells[row + index, 14] = watchitem.진입후최저;
                            ws.Cells[row + index, 15] = watchitem.시가;
                            ws.Cells[row + index, 16] = watchitem.고가;
                            ws.Cells[row + index, 17] = watchitem.저가;
                            ws.Cells[row + index, 18] = int.Parse(watchitem.누적거래량).ToString("N0");
                            ws.Cells[row + index, 19] = watchitem.전일거래량대비 + "%";
                            ws.Cells[row + index, 20] = watchitem.누적거래대금.ToString("N0") + "억";
                            ws.Cells[row + index, 21] = watchitem.거래대금증감.ToString("N0") + "억";
                            ws.Cells[row + index, 22] = watchitem.거래회전율;
                            ws.Cells[row + index, 23] = int.Parse(watchitem.시가총액).ToString("N0") + "억";
                            ws.Cells[row + index, 24] = watchitem.Code;

                            index++;
                        }


                        if (Form1.시장시작 <= Form1.timenow)
                        {
                            if (Form1.timenow <= Form1.시장종료)
                            {
                                wb.SaveAs(Application.StartupPath + @"\Data\검색식_TEST\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + watch + 검색식 + ".xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook);
                            }
                            else
                            {
                                string excelPath = Application.StartupPath + @"\Data\검색식_TEST\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + watch + 검색식 + ".xlsx";

                                if (!System.IO.File.Exists(excelPath))
                                {
                                    wb.SaveAs(Application.StartupPath + @"\Data\검색식_TEST\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + watch + 검색식 + ".xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook);
                                }
                            }
                        }

                        wb.Close();
                        uint processId = 0;
                        GetWindowThreadProcessId(new IntPtr(excelApp.Hwnd), out processId);
                        excelApp.Quit();
                        excelApp.DisplayAlerts = true;

                        if (processId != 0)
                        {
                            System.Diagnostics.Process excelProcess = System.Diagnostics.Process.GetProcessById((int)processId);
                            excelProcess.CloseMainWindow();
                            excelProcess.Refresh();
                            excelProcess.Kill();
                        }
                    }
                    catch
                    {
                        uint processId = 0;
                        GetWindowThreadProcessId(new IntPtr(excelApp.Hwnd), out processId);
                        excelApp.Quit();
                        excelApp.DisplayAlerts = true;

                        if (processId != 0)
                        {
                            System.Diagnostics.Process excelProcess = System.Diagnostics.Process.GetProcessById((int)processId);
                            excelProcess.CloseMainWindow();
                            excelProcess.Refresh();
                            excelProcess.Kill();
                        }
                    }
                }
                catch
                {
                    Form1.AutoClosingAlram("test_save_ Microsoft Excel 저장중 오류가 발생 하였습니다.", "Excel Not fou", 10, "에러");
                }
            }
        }

        public static void Watch_DataLoad()
        {
            if (Properties.Settings.Default.select_account != null)
            {
                FileInfo File_Check = new FileInfo(Application.StartupPath + @"\Data\" + Form1.USER_ID + "__" + Properties.Settings.Default.select_account + "__\\검색식\\Test검색식.txt");

                if (File_Check.Exists)
                {
                    string path = Application.StartupPath + @"\Data\" + Form1.USER_ID + "__" + Properties.Settings.Default.select_account + "__\\검색식\\Test검색식.txt";
                    string OptionLists = File.ReadAllText(path);

                    if (OptionLists.Contains(Properties.Settings.Default.select_account)) // 조건식 저장값 불러오기 
                    {
                        string[] OptionArray = OptionLists.Split(';');

                        string[] A = OptionArray[1].Split('^');
                        Form1.form1.combo_watch_condition_A.Items.Add(" ");
                        Form1.form1.combo_watch_condition_A.Items.Add(A[1]);
                        Form1.form1.combo_watch_condition_A.SelectedItem = A[1];
                        Properties.Settings.Default.combo_watch_condition_A = Form1.form1.combo_watch_condition_A.Text;
                        if (Form1.form1.combo_watch_condition_A.SelectedIndex < 1)
                        {
                            Form1.form1.CBB_Watch_ID_A.SelectedIndex = 0;
                            Form1.form1.CB_TR_A.Checked = false;
                            Form1.form1.CB_Watch_log_A.Checked = false;
                        }

                        string[] B = OptionArray[2].Split('^');
                        Form1.form1.combo_watch_condition_B.Items.Add(" ");
                        Form1.form1.combo_watch_condition_B.Items.Add(B[1]);
                        Form1.form1.combo_watch_condition_B.SelectedItem = B[1];
                        Properties.Settings.Default.combo_watch_condition_B = Form1.form1.combo_watch_condition_B.Text;
                        if (Form1.form1.combo_watch_condition_B.SelectedIndex < 1)
                        {
                            Form1.form1.CBB_Watch_ID_B.SelectedIndex = 0;
                            Form1.form1.CB_TR_B.Checked = false;
                            Form1.form1.CB_Watch_log_B.Checked = false;
                        }

                        string[] C = OptionArray[3].Split('^');
                        Form1.form1.combo_watch_condition_C.Items.Add(" ");
                        Form1.form1.combo_watch_condition_C.Items.Add(C[1]);
                        Form1.form1.combo_watch_condition_C.SelectedItem = C[1];
                        Properties.Settings.Default.combo_watch_condition_C = Form1.form1.combo_watch_condition_C.Text;
                        if (Form1.form1.combo_watch_condition_C.SelectedIndex < 1)
                        {
                            Form1.form1.CBB_Watch_ID_C.SelectedIndex = 0;
                            Form1.form1.CB_TR_C.Checked = false;
                            Form1.form1.CB_Watch_log_C.Checked = false;
                        }

                        string[] D = OptionArray[4].Split('^');
                        Form1.form1.combo_watch_condition_D.Items.Add(" ");
                        Form1.form1.combo_watch_condition_D.Items.Add(D[1]);
                        Form1.form1.combo_watch_condition_D.SelectedItem = D[1];
                        Properties.Settings.Default.combo_watch_condition_D = Form1.form1.combo_watch_condition_D.Text;
                        if (Form1.form1.combo_watch_condition_D.SelectedIndex < 1)
                        {
                            Form1.form1.CBB_Watch_ID_D.SelectedIndex = 0;
                            Form1.form1.CB_TR_D.Checked = false;
                            Form1.form1.CB_Watch_log_D.Checked = false;
                        }
                    }
                }

                if (Form1.로딩완료)
                {
                    Form1.AutoClosingAlram("Watch 검색식  불러오기 완료.", "검색식로딩", 5, "동작");
                }
            }




        }// 실시간 화면 :: 조건식 선택 화면 값 불러오기

        public static void BT_test_viwe_A_Click(object sender)
        {
            Form1.form1.ActiveControl = null;
            Task Task_Action = new Task(
            () =>
            {
                Form1.비프음("버튼");
                Form1.form1.Invoke((MethodInvoker)delegate ()
                {
                    DataGridView dgv = Form1.form1.dataGridView_watch_A;
                    string test_save = "BT_test_save_A";
                    string location = "Watch_A";

                    if (sender.Equals(Form1.form1.BT_test_viwe_A)) { }

                    if (sender.Equals(Form1.form1.BT_test_viwe_B))
                    {
                        dgv = Form1.form1.dataGridView_watch_B;
                        test_save = "BT_test_save_B";
                        location = "Watch_B";
                    }

                    if (sender.Equals(Form1.form1.BT_test_viwe_C))
                    {
                        dgv = Form1.form1.dataGridView_watch_C;
                        test_save = "BT_test_save_C";
                        location = "Watch_C";
                    }

                    if (sender.Equals(Form1.form1.BT_test_viwe_D))
                    {
                        dgv = Form1.form1.dataGridView_watch_D;
                        test_save = "BT_test_save_D";
                        location = "Watch_D";
                    }

                    try
                    {
                        if (dgv.RowCount > 0)
                        {
                            Tab_Watch.test_save_(test_save);

                            string direc1 = Application.StartupPath + @"\Data\검색식_TEST\" + DateTime.Now.ToString("yyyyMMdd") + "\\";

                            DirectoryInfo dia = new DirectoryInfo(@direc1);
                            if (dia.Exists == false)
                            {
                                dia.Create();
                                Process.Start(direc1);
                            }
                            else
                            {
                                Process.Start(direc1);
                            }
                        }
                        else
                        {
                            Form1.AutoClosingAlram(location + " 검색식 TEST 결과가 없습니다.", "없음", 5, "에러");
                        }
                    }
                    catch
                    {
                        Form1.form1.Message_Alram(location + " 검색식 TEST 저장 에러 설정 을확인하세요", "저장에러");
                        Form1.Error_Log("[에러 확인] 검색식 TEST 저장 에러");
                    }
                });
            });
            Task_Action.Start();
        }



        //Watch 콤보박스 변화

        public static void combo_Watch_DropDown(object sender)
        {
            (sender as ComboBox).Items.Clear();
            (sender as ComboBox).Items.Add(" ");

            if (Properties.Settings.Default.CB_매수탐색A) (sender as ComboBox).Items.Add(Properties.Settings.Default.TB_매수탐색A);
            if (Properties.Settings.Default.CB_매수탐색B) (sender as ComboBox).Items.Add(Properties.Settings.Default.TB_매수탐색B);
            if (Properties.Settings.Default.CB_매도탐색) (sender as ComboBox).Items.Add(Properties.Settings.Default.TB_매도탐색);

            for (int i = 0; i < Form1.Run_condition_List.Count; i++)
            {
                (sender as ComboBox).Items.Add(Form1.Run_condition_List[i]);
            }
        }

        public static void combo_watch_DropDownClosed(object sender)
        {

            if (sender.Equals(Form1.form1.combo_watch_condition_A))
            {
                if (Form1.form1.combo_watch_condition_A.SelectedItem == null)
                {
                    Form1.form1.combo_watch_condition_A.SelectedItem = Properties.Settings.Default.combo_watch_condition_A;
                }
            }

            if (sender.Equals(Form1.form1.combo_watch_condition_B))
            {
                if (Form1.form1.combo_watch_condition_B.SelectedItem == null)
                {
                    Form1.form1.combo_watch_condition_B.SelectedItem = Properties.Settings.Default.combo_watch_condition_B;
                }
            }

            if (sender.Equals(Form1.form1.combo_watch_condition_C))
            {
                if (Form1.form1.combo_watch_condition_C.SelectedItem == null)
                {
                    Form1.form1.combo_watch_condition_C.SelectedItem = Properties.Settings.Default.combo_watch_condition_C;
                }
            }

            if (sender.Equals(Form1.form1.combo_watch_condition_D))
            {
                if (Form1.form1.combo_watch_condition_D.SelectedItem == null)
                {
                    Form1.form1.combo_watch_condition_D.SelectedItem = Properties.Settings.Default.combo_watch_condition_D;
                }
            }
        }
        public static void combo_watch_Changed(object sender)
        {
            if (sender.Equals(Form1.form1.combo_watch_condition_A))
            {
                if (Form1.form1.combo_watch_condition_A.SelectedItem != null)
                {
                    if (!Form1.form1.combo_watch_condition_A.SelectedItem.Equals(Properties.Settings.Default.combo_watch_condition_A) && Form1.form1.combo_watch_condition_A.SelectedItem != null)
                    {
                        Form1.form1.dataGridView_watch_A.Rows.Clear();
                        ListClear("A:");
                    }

                    Properties.Settings.Default.combo_watch_condition_A = Form1.form1.combo_watch_condition_A.Text;
                }
            }

            if (sender.Equals(Form1.form1.combo_watch_condition_B))
            {
                if (Form1.form1.combo_watch_condition_B.SelectedItem != null)
                {
                    if (!Form1.form1.combo_watch_condition_B.SelectedItem.Equals(Properties.Settings.Default.combo_watch_condition_B) && Form1.form1.combo_watch_condition_B.SelectedItem != null)
                    {
                        Form1.form1.dataGridView_watch_B.Rows.Clear();
                        ListClear("B:");
                    }
                    Properties.Settings.Default.combo_watch_condition_B = Form1.form1.combo_watch_condition_B.Text;
                }
            }
            if (sender.Equals(Form1.form1.combo_watch_condition_C))
            {
                if (Form1.form1.combo_watch_condition_C.SelectedItem != null)
                {
                    if (!Form1.form1.combo_watch_condition_C.SelectedItem.Equals(Properties.Settings.Default.combo_watch_condition_C) && Form1.form1.combo_watch_condition_C.SelectedItem != null)
                    {
                        Form1.form1.dataGridView_watch_C.Rows.Clear();
                        ListClear("C:");
                    }
                    Properties.Settings.Default.combo_watch_condition_C = Form1.form1.combo_watch_condition_C.Text;
                }
            }
            if (sender.Equals(Form1.form1.combo_watch_condition_D))
            {
                if (Form1.form1.combo_watch_condition_D.SelectedItem != null)
                {
                    if (!Form1.form1.combo_watch_condition_D.SelectedItem.Equals(Properties.Settings.Default.combo_watch_condition_D) && Form1.form1.combo_watch_condition_D.SelectedItem != null)
                    {
                        Form1.form1.dataGridView_watch_D.Rows.Clear();
                        ListClear("D:");
                    }
                    Properties.Settings.Default.combo_watch_condition_D = Form1.form1.combo_watch_condition_D.Text;
                }
            }

            void ListClear(string 위치)
            {
                Dictionary<string, Watch> findResult = Form1.Watch_List.Where(o => o.Key.Contains(위치)).ToDictionary(o => o.Key, o => o.Value);
                foreach (var result in findResult.ToList())
                {
                    Form1.Watch_List.Remove(result.Key);
                }
            }
        }


    }
}
