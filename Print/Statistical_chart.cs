using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace 지니64
{
    public class Statistical_chart : Form1
    {
        public static void 통계_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender.Equals(form1.DGV_통계))
            {
                form1.DGV_통계.CurrentCell = null;
                form1.CBB_통계.SelectedIndex = 0;
            }

            if (sender.Equals(form1.DGV_통계B))
            {
                if (form1.DGV_통계B.Rows.Count > 0 && e.RowIndex > -1)
                {
                    if (form1.DGV_통계B[0, e.RowIndex].Value != null)
                    {
                        string 일자 = form1.DGV_통계B[0, e.RowIndex].Value.ToString();
                        if (일자.Equals("01 월")) { form1.CBB_통계.SelectedIndex = 1; return; }
                        if (일자.Equals("02 월")) { form1.CBB_통계.SelectedIndex = 2; return; }
                        if (일자.Equals("03 월")) { form1.CBB_통계.SelectedIndex = 3; return; }
                        if (일자.Equals("04 월")) { form1.CBB_통계.SelectedIndex = 4; return; }
                        if (일자.Equals("05 월")) { form1.CBB_통계.SelectedIndex = 5; return; }
                        if (일자.Equals("06 월")) { form1.CBB_통계.SelectedIndex = 6; return; }
                        if (일자.Equals("07 월")) { form1.CBB_통계.SelectedIndex = 7; return; }
                        if (일자.Equals("08 월")) { form1.CBB_통계.SelectedIndex = 8; return; }
                        if (일자.Equals("09 월")) { form1.CBB_통계.SelectedIndex = 9; return; }
                        if (일자.Equals("10 월")) { form1.CBB_통계.SelectedIndex = 10; return; }
                        if (일자.Equals("11 월")) { form1.CBB_통계.SelectedIndex = 11; return; }
                        if (일자.Equals("12 월")) { form1.CBB_통계.SelectedIndex = 12; return; }
                    }
                }

                if (e.RowIndex == -1) form1.DGV_통계B.CurrentCell = null;
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
                    if (form1.DGV_통계B[0, e.RowIndex].Value != null)
                    {
                        string 일자 = form1.DGV_통계B[0, e.RowIndex].Value.ToString();

                        if (form1.CBB_통계.SelectedIndex > 0)
                        {
                            int.TryParse(form1.CBB_통계.Text.Substring(0, 2), out int 월);
                            int.TryParse(일자.Substring(0, 2), out int 일);

                            DateTime.TryParse(DateTime.Now.ToString("yyyy-") + 월 + "-" + 일, out DateTime 기준일자);
                            form1.DTP_기준일.Value = 기준일자;

                            기준일매매내역();
                        }
                    }
                }
            }
            catch (Exception 에러)
            {
                Form1.Console_print("DGV_통계B 더블클릭 " + 에러.Message);
               Log.에러기록(" form1.DGV_통계B  에러:: 메세지 " + 에러.Message);
            }
        }

        public static void Print_기준일매매일지()
        {
            // [+] 1. 레이아웃 연산 일시 중지 (크기 조절 먹통 및 화면 깜빡임 방지)
            form1.DGV_통계.SuspendLayout();
            form1.DGV_통계B.SuspendLayout();

            // 2. UI 위치 및 크기 초기화
            form1.chart_Month.Location = new Point(1102, 5);
            form1.chart_Month.Size = new Size(826, 500);
            form1.chart_Day.Location = new Point(1102, 497);
            form1.chart_Day.Size = new Size(826, 500);

            // >> 주의: DGV_통계B 속성창에 Anchor(닻)가 설정되어 있다면, 아래 강제 Size 할당이 충돌을 일으킬 수 있습니다.
            // >> 크기 조절이 계속 안 된다면 Form 디자이너에서 DGV_통계B의 Anchor 속성을 지우거나 'Top, Left'로만 맞춰야 합니다.

            if (!form1.CB_세로보기.Checked)
            {
                form1.DGV_통계B.Size = new Size(1104, 935);
            }

            // 3. 종목 비공개 처리
            if (form1.CB_종목비공개.Checked)
            {
                if (!form1.panel_TP_통계.Controls.Contains(form1.종목감추기6))
                {
                    form1.panel_TP_통계.Controls.Add(form1.종목감추기6);
                    form1.종목감추기6.Location = new Point(-1, 81);
                    form1.종목감추기6.Size = new Size(126, 924);
                    form1.종목감추기6.BringToFront();
                }
            }
            else
            {
                form1.panel_TP_통계.Controls.Remove(form1.종목감추기6);
            }

            // =========================================================
            // 4. 전체 합계 계산
            // =========================================================
            long 총매수금 = 0, 총매도금 = 0, 총실현손익 = 0, 총수수료 = 0, 총세금 = 0;

            foreach (var item in form1.기준일매매내역_List)
            {
                총매수금 += item.BuySum;
                총매도금 += item.SellSum;
                총실현손익 += item.Profit;
                총수수료 += item.Fee;
                총세금 += item.Tax;
            }

            // DGV_통계 업데이트
            form1.DGV_통계.Rows.Insert(0);
            var topRow = form1.DGV_통계.Rows[0];
            double 기준 = (double)GenieConfig.TB_월통계기준;
            if (기준 == 0) 기준 = 1;

            topRow.Cells[0].Value = 총매수금;
            topRow.Cells[1].Value = 총매수금 / 기준 * 100;
            topRow.Cells[2].Value = 총매도금;
            topRow.Cells[3].Value = 총매도금 / 기준 * 100;
            topRow.Cells[4].Value = 총실현손익;
            topRow.Cells[5].Value = 총실현손익 / 기준 * 100;
            topRow.Cells[6].Value = 총수수료;
            topRow.Cells[7].Value = 총수수료 / 기준 * 100;
            topRow.Cells[8].Value = 총세금;
            topRow.Cells[9].Value = 총세금 / 기준 * 100;

            // =========================================================
            // 5. 종목별 집계 (GroupBy 사용)
            // =========================================================
            int 수익cnt = 0;
            int 손실cnt = 0;

            var groupedList = form1.기준일매매내역_List
                    .GroupBy(x => x.Name)
                    .Select(g => new
                    {
                        Name = g.Key,
                        BuySum = g.Sum(x => x.BuySum),
                        SellSum = g.Sum(x => x.SellSum),
                        Profit = g.Sum(x => x.Profit),
                        Fee = g.Sum(x => x.Fee),
                        Tax = g.Sum(x => x.Tax),
                        AvgPrice = g.Average(x => x.Price),
                        AvgRate = g.Average(x => x.Rate)
                    })
                    .OrderBy(g => g.Profit)
                    .ToList();

            double 일기준 = (double)GenieConfig.TB_일통계기준;
            if (일기준 == 0) 일기준 = 1;

            // DGV_통계B에 뿌리기
            foreach (var g in groupedList)
            {
                form1.DGV_통계B.Rows.Insert(0);
                var row = form1.DGV_통계B.Rows[0];

                row.Cells[0].Value = g.Name;
                row.Cells[1].Value = g.BuySum;
                row.Cells[2].Value = g.BuySum / 일기준 * 100;
                row.Cells[3].Value = g.SellSum;
                row.Cells[4].Value = g.SellSum / 일기준 * 100;
                row.Cells[5].Value = g.Profit;
                row.Cells[6].Value = g.Profit / 일기준 * 100;
                row.Cells[7].Value = g.Fee + g.Tax;
                row.Cells[8].Value = (g.Fee + g.Tax) / 일기준 * 100;
                row.Cells[9].Value = (int)g.AvgPrice;
                row.Cells[10].Value = g.AvgRate;

                // 수익/손실 카운트
                string sn = "-";
                if (g.Profit > 0) { sn = "수익"; 수익cnt++; }
                else if (g.Profit < 0) { sn = "손실"; 손실cnt++; }

                row.Cells["수익n손실_통계B"].Value = sn;
            }

            // 6. 최종 요약 업데이트
            form1.DGV_통계["수익횟수_통계", 0].Value = 수익cnt;
            form1.DGV_통계["손실횟수_통계", 0].Value = 손실cnt;
            form1.LB_종목수.Text = "종목: " + (수익cnt + 손실cnt) + " EA";

            form1.DGV_통계.CurrentCell = null;
            form1.DGV_통계B.CurrentCell = null;

            // [+] 7. 모든 데이터 삽입이 끝난 후, 레이아웃 연산 단 한 번만 재개 (엔진 부하 해소)
            form1.DGV_통계.ResumeLayout();
            form1.DGV_통계B.ResumeLayout();

        }

        public static void CBB_통계_확인()
        {
            // 1. 데이터 없음 체크
            if (form1.매매내역_List.Count == 0)
            {
                AutoClosingAlram("매매내역 조회를 먼저 하기 바랍니다.", "조회순서알림", 5, "동작");
                return;
            }

            // 2. UI 초기화
            form1.panel_TP_통계.Controls.Remove(form1.종목감추기6);
            if (form1.CBB_통계.SelectedIndex == -1) return;

            form1.통계수익률 = false; // 일자별 보기 모드

            // DGV 컬럼 설정
            form1.DGV_통계B.Columns[0].HeaderText = "일자";
            form1.DGV_통계B.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            form1.DGV_통계B.Columns[0].Width = 45;
            form1.DGV_통계B.Columns[1].Width = 128;
            form1.DGV_통계B.Columns[2].Width = 90;
            form1.DGV_통계B.Columns[3].Width = 128;
            form1.DGV_통계B.Columns[4].Width = 90;
            form1.DGV_통계B.Columns[5].Width = 128;
            form1.DGV_통계B.Columns[6].Width = 90;

            form1.DGV_통계B.Columns[7].HeaderText = "매매수수료";
            form1.DGV_통계B.Columns[9].HeaderText = "매매세금";
            form1.DGV_통계B.Columns[9].Width = 95;
            form1.DGV_통계B.Columns[10].HeaderText = "세금율";

            form1.DGV_통계.Columns[8].HeaderText = "총매매세금";
            form1.DGV_통계.Columns[9].HeaderText = "세금율";

            // DGV 내용 비우기
            form1.DGV_통계.Rows.Clear(); 
            Method.SortClear(form1.DGV_통계);
            form1.DGV_통계B.Rows.Clear();  
            Method.SortClear(form1.DGV_통계B);

            // =========================================================
            // [데이터 준비] 월별 분류 (LINQ Lookup 사용)
            // =========================================================
            string targetYear = form1.TB_통계.Text.Trim();

            var monthlyData = form1.매매내역_List
                .Where(x => x.Date.Length >= 6 && x.Date.StartsWith(targetYear))
                .ToLookup(x =>
                {
                    if (int.TryParse(x.Date.Substring(4, 2), out int month)) return month;
                    return 0;
                });

            // =========================================================
            // [메인 로직] 전체 보기 vs 특정 월 보기 (레이아웃 잠금 최적화 추가)
            // =========================================================

            // [+] 1. 루프 진입 전 통계 그리드뷰들의 레이아웃 연산을 완전히 잠급니다.
            // 이렇게 해야 내부에서 행이 삽입되거나 크기가 바뀔 때 발생하는 UI 스레드 버벅임이 소멸됩니다.
            form1.DGV_통계.SuspendLayout();
            form1.DGV_통계B.SuspendLayout();

            if (form1.CBB_통계.SelectedIndex == 0) // 전체 통계 (1~12월)
            {
                // UI 사이즈 및 위치 조정
                if (!form1.CB_세로보기.Checked) form1.DGV_통계B.Size = new Size(1104, 298);
                form1.chart_Day.Location = new Point(1102, -10);
                form1.chart_Day.Size = new Size(826, 1000);
                form1.chart_Month.Location = new Point(-25, 372);
                form1.chart_Month.Size = new Size(1169, 640);
                form1.chart_Month.BringToFront();
                form1.chart_Day.BringToFront();

                // 0번 인덱스(총합 데이터) 처리
                if (form1.매매내역_List.Count > 0)
                {
                    // 총합 데이터가 0번에 있다고 가정
                    InsertTotalStats(form1.매매내역_List[0]);
                }

                // 차트 초기화
                form1.chart_Month.Series[0].Points.Clear();
                form1.chart_Month.Series[1].Points.Clear();
                form1.chart_Day.Series[0].Points.Clear();
                form1.chart_Day.Series[1].Points.Clear();

                long cumulativeProfit = 0; // 누적 손익

                // [차트용 루프] 1월 -> 12월 (정순)
                for (int m = 1; m <= 12; m++)
                {
                    var list = monthlyData[m].ToList();
                    long monthProfit = 0;

                    if (list.Count > 0)
                    {
                        // 차트용 계산 (onlyCalc = true)
                        monthProfit = ProcessMonthData(list, true, m);
                    }

                    // 차트 업데이트
                    var p1 = form1.chart_Month.Series[0].Points.AddXY($"{m:D2}월", monthProfit);
                    if (monthProfit < 0) form1.chart_Month.Series[0].Points[p1].Color = Color.Blue;

                    cumulativeProfit += monthProfit;
                    var p2 = form1.chart_Month.Series[1].Points.AddXY($"{m:D2}월", cumulativeProfit);
                    if (cumulativeProfit < 0) form1.chart_Month.Series[1].Points[p2].Color = Color.MediumSeaGreen;
                }

                // [DGV용 루프] 12월 -> 1월 (역순)
                for (int m = 12; m >= 1; m--)
                {
                    var list = monthlyData[m].ToList();
                    if (list.Count > 0)
                    {
                        form1.DGV_통계B.Rows.Insert(0);
                        form1.DGV_통계B["일자_통계B", 0].Value = $"{m:D2} 월";
                        ProcessMonthData(list, false, m); // DGV 출력
                    }
                    else
                    {
                        form1.DGV_통계B.Rows.Insert(0);
                        초기화_행(m);
                    }
                }
            }
            // ---------------------------------------------------------
            // Case 1~12: 특정 월 선택
            // ---------------------------------------------------------
            else
            {
                // UI 사이즈 조정
                if (!form1.CB_세로보기.Checked) form1.DGV_통계B.Size = new Size(1104, 500);
                form1.chart_Month.Location = new Point(1102, -3);
                form1.chart_Month.Size = new Size(826, 575);
                form1.chart_Day.Location = new Point(-75, 550);
                form1.chart_Day.Size = new Size(2047, 440);

                int selectedMonth = form1.CBB_통계.SelectedIndex;
                var list = monthlyData[selectedMonth].ToList();

                // =========================================================
                // [★ 수정된 부분] 해당 월의 합계를 계산하여 상단 DGV_통계 채우기
                // =========================================================

                // 1. 해당 월 데이터 합산 (LINQ Sum 사용)
                long mBuy = list.Sum(x => x.BuySum);
                long mSell = list.Sum(x => x.SellSum);
                long mProfit = list.Sum(x => x.Profit);
                long mFee = list.Sum(x => x.Fee);
                long mTax = list.Sum(x => x.Tax);

                // 2. 구조체에 담아서 출력 함수 호출
                TradeLog monthTotal = new TradeLog
                {
                    BuySum = mBuy,
                    SellSum = mSell,
                    Profit = mProfit,
                    Fee = mFee,
                    Tax = mTax
                };

                InsertTotalStats(monthTotal); // -> 이제 상단 통계가 나옵니다!

                // =========================================================

                if (list.Count > 0)
                {
                    ProcessMonthData(list, false, selectedMonth); // 일자별 출력
                }
                else
                {
                    form1.DGV_통계B.Rows.Insert(0);
                    초기화_행(selectedMonth);
                }
            }

            form1.DGV_통계.CurrentCell = null;
            form1.DGV_통계B.CurrentCell = null;

            // [+] 2. 모든 데이터 제어 및 행 삽입 연산이 끝났으므로 레이아웃을 일괄 갱신합니다.
            // 이 연산 덕분에 그리드뷰의 크기가 굳어버리는 레이아웃 락 현상이 완벽히 방지됩니다.
            form1.DGV_통계.ResumeLayout();
            form1.DGV_통계B.ResumeLayout();

            // =========================================================
            // [내부 헬퍼 함수 정의] (이 함수들은 CBB_통계_확인 안에서만 동작함)
            // =========================================================

            // 1. 상단 통계 DGV 채우기 (총합)
            void InsertTotalStats(TradeLog total)
            {
                form1.DGV_통계.Rows.Insert(0);
                double 기준 = (double)GenieConfig.TB_월통계기준;
                if (기준 == 0) 기준 = 1;

                form1.DGV_통계["총매수금액_통계", 0].Value = total.BuySum;
                form1.DGV_통계["매수회전_통계", 0].Value = (double)total.BuySum / 기준 * 100;
                form1.DGV_통계["총매도금액_통계", 0].Value = total.SellSum;
                form1.DGV_통계["매도회전_통계", 0].Value = (double)total.SellSum / 기준 * 100;
                form1.DGV_통계["총실현손익_통계", 0].Value = total.Profit;
                form1.DGV_통계["실현손익율_통계", 0].Value = (double)total.Profit / 기준 * 100;
                form1.DGV_통계["총매매수수료_통계", 0].Value = total.Fee;
                form1.DGV_통계["수수료율_통계", 0].Value = (double)total.Fee / 기준 * 100;
                form1.DGV_통계["총매매세금_통계", 0].Value = total.Tax;
                form1.DGV_통계["세금율_통계", 0].Value = (double)total.Tax / 기준 * 100;
            }

            // 2. 월 데이터 처리 및 DGV/차트 출력
            long ProcessMonthData(List<TradeLog> list, bool forChartOnly, int month)
            {
                long b = 0, s = 0, p = 0, f = 0, t = 0;

                // [Case A] 특정 월 선택 시 (일자별 리스트 출력 + ★차트 그리기★)
                if (!forChartOnly && form1.CBB_통계.SelectedIndex != 0)
                {
                    // ---------------------------------------------------------
                    // 1. 차트 그리기 (날짜 오름차순: 1일 -> 31일)
                    // ---------------------------------------------------------
                    form1.chart_Day.Series[0].Points.Clear(); // 막대 (일별손익)
                    form1.chart_Day.Series[1].Points.Clear(); // 라인 (누적손익) - ★이게 노란선입니다

                    // 차트를 위해 날짜순으로 정렬된 리스트 생성
                    var chartList = list.OrderBy(x => x.Date).ToList();
                    long cumulativeProfit = 0; // 누적 수익금 저장용

                    foreach (var item in chartList)
                    {
                        // 누적 수익 계산
                        cumulativeProfit += item.Profit;

                        // 라벨 생성
                        string chartLabel = item.Date;
                        if (item.Date.Length >= 8)
                            chartLabel = item.Date.Substring(6, 2) + "일";

                        // [Series 0] 막대 그래프 (일별 손익)
                        int pIdx = form1.chart_Day.Series[0].Points.AddXY(chartLabel, item.Profit);

                        // 색상 설정
                        if (item.Profit < 0)
                            form1.chart_Day.Series[0].Points[pIdx].Color = Color.Blue;
                        else
                            form1.chart_Day.Series[0].Points[pIdx].Color = Color.Red;

                        // [Series 1] 라인 그래프 (누적 손익) - ★빠졌던 부분 추가★
                        form1.chart_Day.Series[1].Points.AddXY(chartLabel, cumulativeProfit);
                    }

                    // ---------------------------------------------------------
                    // 2. DGV 출력 (기존 리스트 순서대로: 보통 최신순)
                    // ---------------------------------------------------------
                    foreach (var item in list)
                    {
                        // 전체 합계 계산
                        b += item.BuySum; s += item.SellSum; p += item.Profit; f += item.Fee; t += item.Tax;

                        // DGV 행 추가
                        form1.DGV_통계B.Rows.Insert(0);

                        string dayStr = item.Date;
                        if (item.Date.Length >= 8) dayStr = item.Date.Substring(6, 2) + " 일";

                        form1.DGV_통계B["일자_통계B", 0].Value = dayStr;
                        SetDgvRowValues(item.BuySum, item.SellSum, item.Profit, item.Fee, item.Tax);
                    }
                }
                // [Case B] 전체 통계 보기 (월별 합계 출력 - 차트X)
                else
                {
                    foreach (var item in list)
                    {
                        b += item.BuySum; s += item.SellSum; p += item.Profit; f += item.Fee; t += item.Tax;
                    }

                    if (!forChartOnly)
                    {
                        SetDgvRowValues(b, s, p, f, t);
                    }
                }
                return p; // 월 실현손익 리턴
            }

            // 3. DGV 값 할당 (중복 코드 제거)
            void SetDgvRowValues(long b, long s, long p, long f, long t)
            {
                var r = form1.DGV_통계B.Rows[0];
                double daily = (double)GenieConfig.TB_일통계기준;
                if (daily == 0) daily = 1;

                r.Cells["매수금액_통계B"].Value = b;
                r.Cells["매수회전율_통계B"].Value = (double)b / daily * 100;
                r.Cells["매도금액_통계B"].Value = s;
                r.Cells["매도회전율_통계B"].Value = (double)s / daily * 100;
                r.Cells["실현손익_통계B"].Value = p;
                r.Cells["실현손익율_통계B"].Value = (double)p / daily * 100;
                r.Cells["매매수수료_통계B"].Value = f;
                r.Cells["매매수수료율_통계B"].Value = (double)f / daily * 100;
                r.Cells["매매세금_통계B"].Value = t;
                r.Cells["매매세금율_통계B"].Value = (double)t / daily * 100;

                string sn = "-";
                if (p > 0) sn = "수익";
                else if (p < 0) sn = "손실";

                r.Cells["수익n손실_통계B"].Value = sn;
            }

            // 4. 빈 데이터 초기화
            void 초기화_행(int month)
            {
                form1.DGV_통계B["일자_통계B", 0].Value = $"{month:D2} 월";
                SetDgvRowValues(0, 0, 0, 0, 0);
            }
        }



        public static void BT_기준일별확인()
        {
            var thread = new Thread(
            () =>
            {
                Helper.안전한_UI_업데이트(form1, () =>  //      
                {
                    using (new CenterWinDialog(form1))
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
            Helper.안전한_UI_업데이트(form1, () =>  //      
            {
                string 계좌번호 = GenieConfig.textBox_계좌번호;

                form1.기준일매매내역_List.Clear();

                form1.DGV_통계.Rows.Clear(); 
                Method.SortClear(form1.DGV_통계);
                form1.DGV_통계B.Rows.Clear();  
                Method.SortClear(form1.DGV_통계B);

                form1.DGV_통계B.Columns[0].HeaderText = "종목명";
                form1.DGV_통계B.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                form1.DGV_통계B.Columns[0].Width = 126;
                form1.DGV_통계B.Columns[1].Width = 105;
                form1.DGV_통계B.Columns[2].Width = 80;
                form1.DGV_통계B.Columns[3].Width = 105;
                form1.DGV_통계B.Columns[4].Width = 80;
                form1.DGV_통계B.Columns[6].Width = 80;

                form1.DGV_통계B.Columns[7].HeaderText = "수수료_세금";
                form1.DGV_통계B.Columns[9].HeaderText = "매도평균가";
                form1.DGV_통계B.Columns[9].Width = 90;
                form1.DGV_통계B.Columns[10].HeaderText = "수익률";

                form1.통계수익률 = true;

                TR_요청.일자별종목별실현손익요청_통계("Y", "", Form1.form1.DTP_기준일.Value.ToString("yyyyMMdd"), false);
            });
        }

        public static void 매매내역확인()
        {
            long.TryParse(form1.TB_월통계기준.Text.Replace(",", ""), out long 월통계기준);
            if (월통계기준 == 0) 월통계기준 = GenieConfig.MT_principal;
            GenieConfig.TB_월통계기준 = 월통계기준;

            long.TryParse(form1.TB_일통계기준.Text.Replace(",", ""), out long 일통계기준);
            if (일통계기준 == 0) 일통계기준 = GenieConfig.MT_sonik_price;
            GenieConfig.TB_일통계기준 = 일통계기준;

            form1.TB_월통계기준.Text = GenieConfig.TB_월통계기준.ToString("N0");
            form1.TB_일통계기준.Text = GenieConfig.TB_일통계기준.ToString("N0");

            string 계좌번호 = GenieConfig.textBox_계좌번호;

            form1.매매내역_List.Clear();
            TR_요청.일자별실현손익요청_통계("Y", "", false);

            //var thread = new Thread(
            //() =>
            //{
            //    Helper.안전한_UI_업데이트(form1, () =>  //      
            //    {
            //        using (new CenterWinDialog(form1))
            //            if (MessageBox.Show("매매내역을 요청 하시겠습니까 ? ", "서버요청", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //            {
            //                form1.매매내역_List.Clear();
            //                TR_요청.일자별실현손익요청_통계("Y", "", false);
            //            }
            //    });
            //});
            //thread.Start();
        }
    }
}