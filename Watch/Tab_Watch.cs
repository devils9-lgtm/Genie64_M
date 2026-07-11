using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 지니64.RESTAPI;
using Excel = Microsoft.Office.Interop.Excel;

namespace 지니64
{
    class Tab_Watch : Form1
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        ///////////////////             WAtch 검색식           //////////////////

        public static void Watch_In_Out(string State_ID, string itemcode, string condition_name, string SearchTime) // 진입 & 이탈
        {
            if (GenieConfig.CBB_Watch_ID_A > 0 || GenieConfig.CBB_Watch_ID_B > 0 || GenieConfig.CBB_Watch_ID_C > 0 || GenieConfig.CBB_Watch_ID_D > 0)
            {

                Market_Item Market = Form1.Market_Item_List[itemcode];
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

                string para = "info_Watch^" + condition_name + "^" + name + "^" + itemcode; // TR요청용 데이터

                bool ID_A = false;
                bool ID_B = false;
                bool ID_C = false;
                bool ID_D = false;

                if (State_ID.Equals("I"))
                {
                    if (GenieConfig.CBB_Watch_ID_A.Equals(1)) ID_A = true;
                    if (GenieConfig.CBB_Watch_ID_B.Equals(1)) ID_B = true;
                    if (GenieConfig.CBB_Watch_ID_C.Equals(1)) ID_C = true;
                    if (GenieConfig.CBB_Watch_ID_D.Equals(1)) ID_D = true;

                }
                else if (State_ID.Equals("D"))
                {
                    if (GenieConfig.CBB_Watch_ID_A.Equals(2)) ID_A = true;
                    if (GenieConfig.CBB_Watch_ID_B.Equals(2)) ID_B = true;
                    if (GenieConfig.CBB_Watch_ID_C.Equals(2)) ID_C = true;
                    if (GenieConfig.CBB_Watch_ID_D.Equals(2)) ID_D = true;
                }

                if (GenieConfig.watch_condition_A.Equals(condition_name) && GenieConfig.CBB_Watch_ID_A > 0)
                {
                    상태변경("A:" + itemcode, ID_A, GenieConfig.CB_TR_A, GenieConfig.CB_Watch_log_A);
                }

                if (GenieConfig.watch_condition_B.Equals(condition_name) && GenieConfig.CBB_Watch_ID_B > 0)
                {
                    상태변경("B:" + itemcode, ID_B, GenieConfig.CB_TR_B, GenieConfig.CB_Watch_log_B);
                }

                if (GenieConfig.watch_condition_C.Equals(condition_name) && GenieConfig.CBB_Watch_ID_C > 0)
                {
                    상태변경("C:" + itemcode, ID_C, GenieConfig.CB_TR_C, GenieConfig.CB_Watch_log_C);
                }

                if (GenieConfig.watch_condition_D.Equals(condition_name) && GenieConfig.CBB_Watch_ID_D > 0)
                {
                    상태변경("D:" + itemcode, ID_D, GenieConfig.CB_TR_D, GenieConfig.CB_Watch_log_D);
                }

                void 상태변경(string Item, bool ID, bool TR, bool log)
                {
                    if (ID)
                    {
                        // 1. [최적화] 객체 미리 생성 (필요할 때만 딕셔너리에 넣음)
                        // 이렇게 하면 '검색'과 '추가'를 TryAdd 한 번으로 처리할 수 있어.
                        var newWatch = new Watch(Market.Market, condition_name, "진입", name, 등락율, itemcode, Nowprice, LastPrice, 진입후등락율, 진입후최고, 진입후최저, 누적거래량, 누적거래대금, 시가, 고가, 저가, 거래대금증감, 전일거래량대비, 거래회전율, 시가총액, 진입가, SearchTime, 0, "-", 0, 0);

                        // 2. [핵심] TryAdd: 없으면 넣고(true), 있으면 실패(false)
                        // 기존의 ContainsKey + Add 조합을 이 한 줄로 끝냄!
                        if (Form1.Watch_List.TryAdd(Item, newWatch))
                        {
                            // [A] 신규 추가 성공 시 (처음 검색된 종목)
                            if (Market.현재가 < 1)
                            {
                                if (TR)
                                {
                                    // FindAll 대신 Any 사용 (훨씬 빠름!)
                                    // 리스트를 다 뒤져서 목록을 만드는 것보다, 하나라도 있는지 확인하는 게 효율적
                                    if (Form1.Run_condition_List.Any(o => o.name.Equals(condition_name)))
                                    {
                                        info.요청(itemcode, "info_Watch", "", false);
                                    }
                                }
                            }
                            else
                            {
                                // 이미 newWatch에 데이터가 들어있지만, 필요하다면 업데이트
                                // (객체 생성 시 이미 값이 들어갔으므로 이 부분은 사실 중복일 수 있으나 로직 유지)
                                newWatch.Nowprice = Market.현재가;
                                newWatch.진입가 = Market.현재가;
                                newWatch.등락율 = Market.등락율;
                            }

                            if (Form1.form1.CB_Watch_log_A.Checked)
                            {
                                Log.동작기록($"[종목 검색 _ 신규검색] 시장구분: {Market.Market} 종목명: {Market.종목명} 검색식: {condition_name}");
                            }
                        }
                        else
                        {
                            // [B] 이미 존재하는 경우 (재진입)
                            // 딕셔너리에서 기존 값을 빠르게 가져와서 상태만 변경
                            if (Form1.Watch_List.TryGetValue(Item, out Watch existingWatch))
                            {
                                existingWatch.State = "재진입";
                            }

                            if (log)
                            {
                                Log.동작기록($"[종목 검색 _ 재검색] 시장구분: {Market.Market} 종목명: {Market.종목명} 검색식: {condition_name}");
                            }
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

            Watch watch;

            if (Form1.Watch_List.ContainsKey("A:" + 종목코드))
            {
                watch = Form1.Watch_List["A:" + 종목코드];
                Updata(GenieConfig.TB_watch_익절_A, GenieConfig.TB_watch_손절_A);
            }
            if (Form1.Watch_List.ContainsKey("B:" + 종목코드))
            {
                watch = Form1.Watch_List["B:" + 종목코드];
                Updata(GenieConfig.TB_watch_익절_B, GenieConfig.TB_watch_손절_B);
            }
            if (Form1.Watch_List.ContainsKey("C:" + 종목코드))
            {
                watch = Form1.Watch_List["C:" + 종목코드];
                Updata(GenieConfig.TB_watch_익절_C, GenieConfig.TB_watch_손절_C);
            }
            if (Form1.Watch_List.ContainsKey("D:" + 종목코드))
            {
                watch = Form1.Watch_List["D:" + 종목코드];
                Updata(GenieConfig.TB_watch_익절_D, GenieConfig.TB_watch_손절_D);
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

        //public static void DGV_watch_print(string 위치)
        //{
        //    int MaxRow = 0;
        //    int.TryParse(Form1.form1.TB_WatchRow_A.Text, out int max);
        //    MaxRow = Math.Abs(max);
        //    DataGridView DGV = Form1.form1.dataGridView_watch_A;
        //    CheckBox CB_watch_remove = Form1.form1.CB_watch_remove_A;

        //    if (위치.Equals("B:"))
        //    {
        //        int.TryParse(Form1.form1.TB_WatchRow_B.Text, out int max_B);
        //        MaxRow = Math.Abs(max_B);
        //        DGV = Form1.form1.dataGridView_watch_B;
        //        CB_watch_remove = Form1.form1.CB_watch_remove_B;
        //    }
        //    if (위치.Equals("C:"))
        //    {
        //        int.TryParse(Form1.form1.TB_WatchRow_C.Text, out int max_C);
        //        MaxRow = Math.Abs(max_C);
        //        DGV = Form1.form1.dataGridView_watch_C;
        //        CB_watch_remove = Form1.form1.CB_watch_remove_C;
        //    }
        //    if (위치.Equals("D:"))
        //    {
        //        int.TryParse(Form1.form1.TB_WatchRow_D.Text, out int max_D);
        //        MaxRow = Math.Abs(max_D);
        //        DGV = Form1.form1.dataGridView_watch_D;
        //        CB_watch_remove = Form1.form1.CB_watch_remove_D;
        //    }

        //    Watch watch = null;
        //    Dictionary<string, Watch> findResult = Form1.Watch_List.Where(o => o.Key.Contains(위치)).ToDictionary(o => o.Key, o => o.Value);
        //    foreach (var result in findResult.ToList())
        //    {
        //        watch = findResult[result.Key];

        //        if (DGV.RowCount == 0)
        //        {
        //            if (CB_watch_remove.Checked)
        //            {
        //                if (watch.State.Contains("진입"))
        //                {
        //                    인서트();
        //                }
        //            }
        //            else
        //            {
        //                인서트();
        //            }
        //        }
        //        else
        //        {
        //            if (watch.State.Contains("진입"))
        //            {
        //                for (int n = 0; n < DGV.RowCount; n++)
        //                {
        //                    if (DGV[23, n].Value.Equals(watch.Code))
        //                    {
        //                        DGV.Rows.Remove(DGV.Rows[n]);
        //                        break;
        //                    }
        //                }

        //                인서트();
        //            }
        //        }

        //        void 인서트()
        //        {
        //            DGV.Rows.Insert(0);
        //            DGV[23, 0].Value = watch.Code;
        //            print_watch(0);

        //            if (DGV.RowCount > MaxRow)
        //            {
        //                int count = DGV.RowCount - MaxRow;

        //                for (int n = 0; n < count; n++)
        //                {
        //                    DGV.Rows.Remove(DGV.Rows[DGV.RowCount - 1]);
        //                }
        //            }

        //            DGV.CurrentCell = null;
        //        }
        //    }

        //    for (int i = 0; i < DGV.Rows.Count; i++)
        //    {
        //        watch = Form1.Watch_List[위치 + DGV[23, i].Value.ToString()];

        //        if (CB_watch_remove.Checked)
        //        {
        //            if (watch.State.Contains("이탈"))
        //            {
        //                DGV.Rows.Remove(DGV.Rows[i]);

        //                DGV.CurrentCell = null;
        //            }
        //            else
        //            {
        //                print_watch(i);
        //            }
        //        }
        //        else
        //        {
        //            print_watch(i);
        //        }
        //    }

        //    void print_watch(int row)
        //    {
        //        DGV[1, row].Value = watch.Market;
        //        DGV[2, row].Value = watch.진입시간;
        //        DGV[3, row].Value = watch.State;
        //        DGV[4, row].Value = watch.timer;
        //        DGV[5, row].Value = watch.Name;
        //        DGV[6, row].Value = watch.Nowprice;
        //        DGV[7, row].Value = watch.등락율;
        //        DGV[8, row].Value = watch.진입가;
        //        DGV[9, row].Value = watch.매매;
        //        DGV[10, row].Value = watch.매수가;
        //        DGV[11, row].Value = watch.진입후등락율;
        //        DGV[12, row].Value = watch.진입후최고;
        //        DGV[13, row].Value = watch.진입후최저;
        //        DGV[14, row].Value = watch.시가;
        //        DGV[15, row].Value = watch.고가;
        //        DGV[16, row].Value = watch.저가;
        //        DGV[17, row].Value = int.Parse(watch.누적거래량).ToString("N0");
        //        DGV[18, row].Value = watch.전일거래량대비 + "%";
        //        DGV[19, row].Value = watch.누적거래대금.ToString("N0") + "억";
        //        DGV[20, row].Value = watch.거래대금증감.ToString("N0") + "억";
        //        DGV[21, row].Value = watch.거래회전율;
        //        DGV[22, row].Value = int.Parse(watch.시가총액).ToString("N0") + "억";
        //    }
        //}



        public static void DGV_watch_print(string 위치)
        {
            // 1. 변수 초기화 및 위치별 설정
            int MaxRow = 0;
            DataGridView DGV;
            CheckBox CB_watch_remove;

            // 위치별 설정 (코드를 간결하게 유지하기 위해 switch 문으로 대체합니다.)
            switch (위치)
            {
                case "A:":
                    int.TryParse(Form1.form1.TB_WatchRow_A.Text, out int max_A);
                    MaxRow = Math.Abs(max_A);
                    DGV = Form1.form1.dataGridView_watch_A;
                    CB_watch_remove = Form1.form1.CB_watch_remove_A;
                    break;
                case "B:":
                    int.TryParse(Form1.form1.TB_WatchRow_B.Text, out int max_B);
                    MaxRow = Math.Abs(max_B);
                    DGV = Form1.form1.dataGridView_watch_B;
                    CB_watch_remove = Form1.form1.CB_watch_remove_B;
                    break;
                case "C:":
                    int.TryParse(Form1.form1.TB_WatchRow_C.Text, out int max_C);
                    MaxRow = Math.Abs(max_C);
                    DGV = Form1.form1.dataGridView_watch_C;
                    CB_watch_remove = Form1.form1.CB_watch_remove_C;
                    break;
                case "D:":
                    int.TryParse(Form1.form1.TB_WatchRow_D.Text, out int max_D);
                    MaxRow = Math.Abs(max_D);
                    DGV = Form1.form1.dataGridView_watch_D;
                    CB_watch_remove = Form1.form1.CB_watch_remove_D;
                    break;
                default:
                    return; // 유효하지 않은 위치
            }

            // ⭐️ [CurrentCell 유지 로직] 현재 셀 위치 저장
            int currentRowIndex = -1;
            int currentColumnIndex = -1;

            if (DGV.CurrentCell != null)
            {
                currentRowIndex = DGV.CurrentCell.RowIndex;
                currentColumnIndex = DGV.CurrentCell.ColumnIndex;
            }

            Watch watch = null;
            // Form1.Watch_List를 사용하도록 수정
            Dictionary<string, Watch> findResult = Form1.Watch_List.Where(o => o.Key.Contains(위치)).ToDictionary(o => o.Key, o => o.Value);

            // 2. '진입' 항목 처리 및 'MaxRow' 초과 행 정리 로직
            foreach (var result in findResult.ToList())
            {
                watch = findResult[result.Key];

                if (watch.State.Contains("진입"))
                {
                    // DGV의 24번째 컬럼(index 23)에 Code가 저장되어 있다고 가정
                    int existingRowIndex = -1;
                    for (int n = 0; n < DGV.RowCount; n++)
                    {
                        if (DGV[23, n].Value != null && DGV[23, n].Value.Equals(watch.Code))
                        {
                            existingRowIndex = n;
                            break;
                        }
                    }

                    if (existingRowIndex != -1)
                    {
                        if (existingRowIndex == 0)
                        {
                            print_watch(existingRowIndex);
                        }
                        else
                        {
                            // ⭐️ [CurrentCell 유지] 행 삭제 전 인덱스 조정
                            if (currentRowIndex == existingRowIndex)
                            {
                                currentRowIndex = -1; // 선택 셀이 삭제되면 인덱스 무효화
                            }
                            else if (currentRowIndex > existingRowIndex)
                            {
                                currentRowIndex--; // 삭제될 행 뒤의 셀이면 인덱스 1 감소
                            }

                            DGV.Rows.RemoveAt(existingRowIndex);

                            // '인서트' 시 기존 행들이 밀리므로, 저장된 인덱스 조정
                            if (currentRowIndex != -1)
                            {
                                currentRowIndex++; // 0번에 새 행이 삽입되므로 기존 인덱스 1 증가
                            }

                            인서트(); // 인서트 내에서 print_watch(0) 호출
                        }
                    }
                    else
                    {
                        // ⭐️ [CurrentCell 유지] 행 삽입 전 인덱스 조정
                        if (currentRowIndex != -1)
                        {
                            currentRowIndex++; // 0번에 새 행이 삽입되므로 기존 인덱스 1 증가
                        }

                        인서트();
                    }
                }
            }


            // 3. 기존 DGV 항목 업데이트 및 '이탈' 항목 삭제
            // 인덱스 오류 방지를 위해 역순으로 반복
            for (int i = DGV.Rows.Count - 1; i >= 0; i--)
            {
                if (i >= DGV.Rows.Count) continue;

                // DGV의 24번째 컬럼(index 23) 사용
                string market_code_key = DGV[23, i].Value?.ToString();
                if (string.IsNullOrEmpty(market_code_key) || !Form1.Watch_List.ContainsKey(위치 + market_code_key))
                {
                    continue;
                }

                watch = Form1.Watch_List[위치 + market_code_key];

                if (CB_watch_remove.Checked && watch.State.Contains("이탈"))
                {
                    // ⭐️ [CurrentCell 유지] 행 삭제 전 인덱스 조정
                    if (currentRowIndex == i)
                    {
                        currentRowIndex = -1; // 선택 셀이 삭제되면 인덱스 무효화
                    }
                    else if (currentRowIndex > i)
                    {
                        currentRowIndex--; // 삭제될 행 뒤의 셀이면 인덱스 1 감소
                    }

                    DGV.Rows.RemoveAt(i);
                }
                else
                {
                    print_watch(i); // 변경된 값만 업데이트
                }
            }

            // ⭐️ [CurrentCell 유지 로직] 저장된 셀 위치 복구
            if (currentRowIndex != -1 && currentRowIndex < DGV.RowCount && currentColumnIndex < DGV.ColumnCount)
            {
                // DataGridView의 포커스(CurrentCell)를 복구
                DGV.CurrentCell = DGV.Rows[currentRowIndex].Cells[currentColumnIndex];
            }
            else
            {
                // 유효하지 않은 경우, 선택 해제 (기존 인서트/이탈 로직의 DGV.CurrentCell = null; 대체)
                DGV.CurrentCell = null;
                DGV.ClearSelection();
            }


            // *********************************************************************************
            // 로컬 함수 정의
            // *********************************************************************************

            // 인서트 함수: DGV 맨 위(0번)에 새 행을 삽입하고 MaxRow를 초과하는 행을 정리
            void 인서트()
            {
                // 기존 코드: DGV.Rows.Insert(0); -> 행을 추가하고, 행의 속성 값을 설정해야 합니다.
                // DataGridView.Rows.Insert(0, 1) 또는 DataGridView.Rows.Add() 후 인서트가 필요할 수 있습니다.
                // 현재 코드에 맞춰 DGV.Rows.Insert(0); 후 값을 설정하는 로직을 유지합니다.
                DGV.Rows.Insert(0);
                DGV[23, 0].Value = watch.Code; // Code는 23번 인덱스에 저장
                print_watch(0);

                // MaxRow 초과 시 하단 행 제거
                while (DGV.RowCount > MaxRow)
                {
                    DGV.Rows.RemoveAt(DGV.RowCount - 1);
                }

                // 인서트 내의 DGV.CurrentCell = null; 로직은 메인 로직의 마지막 복구 로직으로 대체됩니다.
            }

            // print_watch 함수: 변경된 값만 업데이트하여 성능 개선 (새로운 컬럼 인덱스/변수명 반영)
            void print_watch(int row)
            {
                // ⭐️ 값이 다를 경우에만 DGV 셀을 업데이트합니다.

                // Column 1: Market
                if (!object.Equals(DGV[1, row].Value?.ToString(), watch.Market))
                    DGV[1, row].Value = watch.Market;

                // Column 2: 진입시간
                if (!object.Equals(DGV[2, row].Value?.ToString(), watch.진입시간))
                    DGV[2, row].Value = watch.진입시간;

                // Column 3: State
                if (!object.Equals(DGV[3, row].Value?.ToString(), watch.State))
                    DGV[3, row].Value = watch.State;

                // Column 4: timer
                if (!object.Equals(DGV[4, row].Value, watch.timer))
                    DGV[4, row].Value = watch.timer;

                // Column 5: Name
                if (!object.Equals(DGV[5, row].Value?.ToString(), watch.Name))
                    DGV[5, row].Value = watch.Name;

                // Column 6: Nowprice
                if (!object.Equals(DGV[6, row].Value, watch.Nowprice))
                    DGV[6, row].Value = watch.Nowprice;

                // Column 7: 등락율
                if (!object.Equals(DGV[7, row].Value, watch.등락율))
                    DGV[7, row].Value = watch.등락율;

                // Column 8: 진입가
                if (!object.Equals(DGV[8, row].Value, watch.진입가))
                    DGV[8, row].Value = watch.진입가;

                // Column 9: 매매
                if (!object.Equals(DGV[9, row].Value?.ToString(), watch.매매))
                    DGV[9, row].Value = watch.매매;

                // Column 10: 매수가
                if (!object.Equals(DGV[10, row].Value, watch.매수가))
                    DGV[10, row].Value = watch.매수가;

                // Column 11: 진입후등락율
                if (!object.Equals(DGV[11, row].Value, watch.진입후등락율))
                    DGV[11, row].Value = watch.진입후등락율;

                // Column 12: 진입후최고
                if (!object.Equals(DGV[12, row].Value, watch.진입후최고))
                    DGV[12, row].Value = watch.진입후최고;

                // Column 13: 진입후최저
                if (!object.Equals(DGV[13, row].Value, watch.진입후최저))
                    DGV[13, row].Value = watch.진입후최저;

                // Column 14: 시가
                if (!object.Equals(DGV[14, row].Value, watch.시가))
                    DGV[14, row].Value = watch.시가;

                // Column 15: 고가
                if (!object.Equals(DGV[15, row].Value, watch.고가))
                    DGV[15, row].Value = watch.고가;

                // Column 16: 저가
                if (!object.Equals(DGV[16, row].Value, watch.저가))
                    DGV[16, row].Value = watch.저가;

                // Column 17: 누적거래량 (포맷팅 적용)
                string 누적거래량_formatted = int.Parse(watch.누적거래량).ToString("N0");
                if (!object.Equals(DGV[17, row].Value?.ToString(), 누적거래량_formatted))
                    DGV[17, row].Value = 누적거래량_formatted;

                // Column 18: 전일거래량대비
                string 전일거래량대비_formatted = watch.전일거래량대비 + "%";
                if (!object.Equals(DGV[18, row].Value?.ToString(), 전일거래량대비_formatted))
                    DGV[18, row].Value = 전일거래량대비_formatted;

                // Column 19: 누적거래대금 (포맷팅 적용)
                string 누적거래대금_formatted = watch.누적거래대금.ToString("N0") + "억";
                if (!object.Equals(DGV[19, row].Value?.ToString(), 누적거래대금_formatted))
                    DGV[19, row].Value = 누적거래대금_formatted;

                // Column 20: 거래대금증감 (포맷팅 적용)
                string 거래대금증감_formatted = watch.거래대금증감.ToString("N0") + "억";
                if (!object.Equals(DGV[20, row].Value?.ToString(), 거래대금증감_formatted))
                    DGV[20, row].Value = 거래대금증감_formatted;

                // Column 21: 거래회전율
                if (!object.Equals(DGV[21, row].Value, watch.거래회전율))
                    DGV[21, row].Value = watch.거래회전율;

                // Column 22: 시가총액 (포맷팅 적용)
                string 시가총액_formatted = int.Parse(watch.시가총액).ToString("N0") + "억";
                if (!object.Equals(DGV[22, row].Value?.ToString(), 시가총액_formatted))
                    DGV[22, row].Value = 시가총액_formatted;

                // Column 23: Code (키 값, 업데이트 불필요)
            }
        }
























        public static void Test_save_(string sender)
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
            
            if (sender.Equals("BT_watch_Save_A"))
            {
                combo = Form1.form1.watch_condition_A;
                dgv = Form1.form1.dataGridView_watch_A;
                watch = "Watch_A_";
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

            if (sender.Equals("BT_watch_Save_B"))
            {
                combo = Form1.form1.watch_condition_B;
                dgv = Form1.form1.dataGridView_watch_B;
                watch = "Watch_B_";
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

            if (sender.Equals("BT_watch_Save_C"))
            {
                combo = Form1.form1.watch_condition_C;
                dgv = Form1.form1.dataGridView_watch_C;
                watch = "Watch_C_";
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

            if (sender.Equals("BT_watch_Save_D"))
            {
                combo = Form1.form1.watch_condition_D;
                dgv = Form1.form1.dataGridView_watch_D;
                watch = "Watch_D_";
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
                    // 1. 경로 및 파일명 설정 (.txt 확장자 사용, 탭으로 구분)
                    string folderPath = Application.StartupPath + @"\Data\검색식_TEST\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                    string fileName = watch + 검색식 + ".txt";
                    string fullPath = folderPath + fileName;

                    // 폴더가 없으면 생성
                    Directory.CreateDirectory(folderPath);

                    // 2. 저장 실행 여부 판단
                    // (장중에는 무조건 갱신/덮어쓰기, 장 마감 후에는 파일이 없을 때만 생성)
                    bool shouldSave = false;
                    if (Get.시장시작 <= Get.TimeNow)
                    {
                        if (Get.TimeNow <= Get.시장종료) shouldSave = true; // 장중
                        else if (!File.Exists(fullPath)) shouldSave = true; // 장마감 후 파일 없을 때
                    }

                    if (shouldSave)
                    {
                        // 3. 텍스트 파일 쓰기 (false: 덮어쓰기 모드)
                        // Encoding.Default: 엑셀에서 한글 깨짐 방지 (필수)
                        using (StreamWriter sw = new StreamWriter(fullPath, false, Encoding.Default))
                        {
                            // --- [상단 메타데이터 영역] ---
                            // [Row 1] 메타데이터 제목
                            sw.WriteLine("날짜\t검색식\t검색방법\t관심그룹\t매수옵션\t진입유지\t익절\t손절");

                            // [Row 2] 메타데이터 값 (탭 \t 으로 구분)
                            string 날짜   = DateTime.Now.ToString("yyyyMMdd");
                            string metaData = $"{날짜}\t{검색식}\t{I_D}\t{관심그룹}\t{매수옵션}\t{진입유지}\t{익절}\t{손절}";
                            sw.WriteLine(metaData);

                            // [Row 3] 공백 줄 (엑셀의 row=4 시작을 맞추기 위함)
                            sw.WriteLine();

                            // --- [하단 데이터 테이블 영역] ---
                            // [Row 4] 컬럼 헤더 (DataGridView 기준)
                            string headerLine = "";
                            foreach (DataGridViewColumn col in dgv.Columns)
                            {
                                headerLine += col.HeaderText + "\t";
                            }
                            sw.WriteLine(headerLine.TrimEnd('\t'));

                            // [Row 5~] 실제 데이터 목록 작성
                            int index = 0;

                            // '위치' 필터링 로직 유지
                            var findResult = Watch_List.Where(o => o.Key.Contains(위치)).ToList();

                            foreach (var item in findResult)
                            {
                                Watch w = item.Value;

                                // 숫자 변환 (에러 방지용)
                                long.TryParse(w.누적거래량, out long vol);
                                long.TryParse(w.시가총액, out long cap);

                                // 데이터 조립 (탭 \t 으로 연결)
                                string line = $"{index + 1}\t" +               // 1. 연번
                                              $"{w.Market}\t" +                 // 2. 구분
                                              $"{w.진입시간}\t" +               // 3. 진입시간
                                              $"{w.State}\t" +                  // 4. 상태
                                              $"{w.timer}\t" +                  // 5. 타이머
                                              $"{w.Name}\t" +                   // 6. 종목명
                                              $"{w.Nowprice}\t" +               // 7. 현재가
                                              $"{w.등락율}\t" +                 // 8. 등락율
                                              $"{w.진입가}\t" +                 // 9. 진입가
                                              $"{w.매매}\t" +                   // 10. 매매
                                              $"{w.매수가}\t" +                 // 11. 매수가
                                              $"{w.진입후등락율}\t" +           // 12. 진입후등락율
                                              $"{w.진입후최고}\t" +             // 13. 진입후최고
                                              $"{w.진입후최저}\t" +             // 14. 진입후최저
                                              $"{w.시가}\t" +                   // 15. 시가
                                              $"{w.고가}\t" +                   // 16. 고가
                                              $"{w.저가}\t" +                   // 17. 저가
                                              $"{vol:N0}\t" +                   // 18. 누적거래량
                                              $"{w.전일거래량대비}%\t" +        // 19. 전일비
                                              $"{w.누적거래대금:N0}억\t" +      // 20. 누적거래대금
                                              $"{w.거래대금증감:N0}억\t" +      // 21. 거래대금증감
                                              $"{w.거래회전율}\t" +             // 22. 회전율
                                              $"{cap:N0}억\t" +                 // 23. 시가총액
                                              $"{w.Code}";                      // 24. 종목코드

                                sw.WriteLine(line);
                                index++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // AutoClosingAlram("검색식 테스트 저장 오류: " + ex.Message, ...);
                    Console_print(ex.Message);
                }
            }
        }

        public static void BT_test_viwe_A_Click(object sender)
        {
            form1.ActiveControl = null;
            비프음("버튼");

            try
            {
                // 1. 버튼에 따른 변수 설정
                DataGridView dgv = form1.dataGridView_watch_A;
                string Test_save_key = "BT_watch_Save_A";
                string location = "Watch_A_";
                ComboBox combo = form1.watch_condition_A;

                if (sender.Equals(form1.BT_test_viwe_B))
                {
                    dgv = form1.dataGridView_watch_B;
                    Test_save_key = "BT_watch_Save_B";
                    location = "Watch_B_";
                    combo = form1.watch_condition_D;
                }
                else if (sender.Equals(form1.BT_test_viwe_C))
                {
                    dgv = form1.dataGridView_watch_C;
                    Test_save_key = "BT_watch_Save_C";
                    location = "Watch_C_";
                    combo = form1.watch_condition_C;
                }
                else if (sender.Equals(form1.BT_test_viwe_D))
                {
                    dgv = form1.dataGridView_watch_D;
                    Test_save_key = "BT_watch_Save_D";
                    location = "Watch_D_";
                    combo = form1.watch_condition_D;
                }

                // 2. 데이터 저장 및 열기 로직
                if (dgv.RowCount > 0)
                {
                    // (1) 텍스트 파일로 저장 실행
                    Tab_Watch.Test_save_(Test_save_key);

                    string 검색식 = combo.Text.Trim();
                    // (2) 파일 경로 구성
                    // 주의: 'watch'와 '검색식' 변수가 이 함수에서 접근 가능해야 합니다 (전역변수 가정)
                    string folderPath = Application.StartupPath + @"\Data\검색식_TEST\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                    string fileName = location + 검색식 + ".txt"; // 저장할 때와 동일한 이름 규칙
                    string fullPath = folderPath + fileName;

                    Console_print("텍스트 파일 fileName : " + fileName);

                    // (3) 스마트 열기 로직 (엑셀 확인)
                    if (File.Exists(fullPath))
                    {
                        // 엑셀 설치 경로 확인 (GetExcelPath 함수 필요)
                        string excelPath = GetExcelPath();

                        if (!string.IsNullOrEmpty(excelPath))
                        {
                            // 엑셀이 있으면 -> 엑셀로 강제 실행
                            Process.Start(excelPath, $"\"{fullPath}\"");
                        }
                        else
                        {
                            // 엑셀이 없으면 -> 연결된 프로그램(메모장 등)으로 실행
                            Process.Start(fullPath);
                        }
                    }
                    else
                    {
                        // 파일이 없으면 폴더라도 열기
                        if (Directory.Exists(folderPath))
                        {
                            Process.Start(folderPath);
                        }
                    }
                }
                else
                {
                    AutoClosingAlram(location + " 검색식 TEST 결과가 없습니다.", "없음", 5, "에러");
                }
            }
            catch (Exception ex)
            {
                form1.Message_Alram("열기 실패: " + ex.Message, "에러");
            }
        }


        // 엑셀 설치 경로를 찾는 함수 (필수)
        private static string GetExcelPath()
        {
            try
            {
                string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\excel.exe";
                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath))
                {
                    if (key != null)
                    {
                        return key.GetValue(null) as string;
                    }
                }
            }
            catch { }
            return null;
        }
        //Watch 콤보박스 변화

        //Watch 콤보박스 변화

        public static void Combo_Watch_DropDown(object sender)
        {
            (sender as ComboBox).Items.Clear();
            (sender as ComboBox).Items.Add(" ");

            for (int i = 0; i < Form1.Run_condition_List.Count; i++)
            {
                if (!(sender as ComboBox).Items.Contains(Form1.Run_condition_List[i].name))
                    (sender as ComboBox).Items.Add(Form1.Run_condition_List[i].name);
            }
        }

        public static void Combo_watch_DropDownClosed(object sender)
        {
            if (sender.Equals(Form1.form1.watch_condition_A))
            {
                if (Form1.form1.watch_condition_A.SelectedItem == null)
                {
                    Form1.form1.watch_condition_A.SelectedItem = GenieConfig.watch_condition_A;
                }
            }

            if (sender.Equals(Form1.form1.watch_condition_B))
            {
                if (Form1.form1.watch_condition_B.SelectedItem == null)
                {
                    Form1.form1.watch_condition_B.SelectedItem = GenieConfig.watch_condition_B;
                }
            }

            if (sender.Equals(Form1.form1.watch_condition_C))
            {
                if (Form1.form1.watch_condition_C.SelectedItem == null)
                {
                    Form1.form1.watch_condition_C.SelectedItem = GenieConfig.watch_condition_C;
                }
            }

            if (sender.Equals(Form1.form1.watch_condition_D))
            {
                if (Form1.form1.watch_condition_D.SelectedItem == null)
                {
                    Form1.form1.watch_condition_D.SelectedItem = GenieConfig.watch_condition_D;
                }
            }
        }
        public static void Combo_watch_Changed(object sender)
        {
            if (sender.Equals(Form1.form1.watch_condition_A))
            {
                if (Form1.form1.watch_condition_A.SelectedItem != null)
                {
                    if (!Form1.form1.watch_condition_A.SelectedItem.Equals(GenieConfig.watch_condition_A) && Form1.form1.watch_condition_A.SelectedItem != null)
                    {
                        Form1.form1.dataGridView_watch_A.Rows.Clear(); 
                        Method.SortClear(Form1.form1.dataGridView_watch_A);
                        ListClear("A:");
                    }

                    GenieConfig.watch_condition_A = Form1.form1.watch_condition_A.Text;
                }
            }
            if (sender.Equals(Form1.form1.watch_condition_B))
            {
                if (Form1.form1.watch_condition_B.SelectedItem != null)
                {
                    if (!Form1.form1.watch_condition_B.SelectedItem.Equals(GenieConfig.watch_condition_B) && Form1.form1.watch_condition_B.SelectedItem != null)
                    {
                        Form1.form1.dataGridView_watch_B.Rows.Clear(); 
                        Method.SortClear(Form1.form1.dataGridView_watch_B);
                        ListClear("B:");
                    }
                    GenieConfig.watch_condition_B = Form1.form1.watch_condition_B.Text;
                }
            }
            if (sender.Equals(Form1.form1.watch_condition_C))
            {
                if (Form1.form1.watch_condition_C.SelectedItem != null)
                {
                    if (!Form1.form1.watch_condition_C.SelectedItem.Equals(GenieConfig.watch_condition_C) && Form1.form1.watch_condition_C.SelectedItem != null)
                    {
                        Form1.form1.dataGridView_watch_C.Rows.Clear();  
                        Method.SortClear(Form1.form1.dataGridView_watch_C);
                        ListClear("C:");
                    }
                    GenieConfig.watch_condition_C = Form1.form1.watch_condition_C.Text;
                }
            }
            if (sender.Equals(Form1.form1.watch_condition_D))
            {
                if (Form1.form1.watch_condition_D.SelectedItem != null)
                {
                    if (!Form1.form1.watch_condition_D.SelectedItem.Equals(GenieConfig.watch_condition_D) && Form1.form1.watch_condition_D.SelectedItem != null)
                    {
                        Form1.form1.dataGridView_watch_D.Rows.Clear();  
                        Method.SortClear(Form1.form1.dataGridView_watch_D);
                        ListClear("D:");
                    }
                    GenieConfig.watch_condition_D = Form1.form1.watch_condition_D.Text;
                }
            }

            void ListClear(string 위치)
            {
                // [최적화 1] 무겁게 전체 데이터를 복사(ToDictionary)하지 않고, 'Key'만 가볍게 찾음
                // Watch 객체(Value)는 건드리지 않아서 메모리 사용량이 확 줄어듦.
                var 삭제할키들 = Form1.Watch_List.Keys
                                      .Where(k => k.Contains(위치))
                                      .ToList();

                foreach (var key in 삭제할키들)
                {
                    // [최적화 2] ConcurrentDictionary는 TryRemove 사용 (스레드 충돌 방지)
                    Form1.Watch_List.TryRemove(key, out _);
                }
            }
        }


    }
}
