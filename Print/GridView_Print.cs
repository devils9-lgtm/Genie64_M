using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 지니64.box;

namespace 지니64
{
    class GridView_Print : Form1
    {

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////           주문그리드뷰           ///////////////////

        public static int jumuncount = 1;

        public static void DGV_Jumun(JumunItem JumunItem, string 주문시간, string 접수내역, string 종목명, int 현재가, string 매수매도, string 시장가구분, int 주문가, int 수량, string 종목코드, string 비고, int 취소대기, int 취소N주문, string 검색식, string 주문번호, double Value, int 주문, int 반복)
        {
            // =================================================================
            // [핵심 최적화 1] Task.Run 진입 전, 변동될 수 있는 값들을 영구 박제합니다.
            // =================================================================
            double 고정_등락률 = JumunItem.등락률;

            // =================================================================
            // [핵심 최적화 2] 메인 스레드에서 잔고의 현재 수익률을 그대로 복사해 옵니다. (계산 X)
            // =================================================================
            double 잔고_수익률_스냅샷 = 0;

            // 종목코드로 잔고 데이터를 안전하게 찾아옵니다.
            if (Form1.stockBalanceList.TryGetValue(종목코드, out var 잔고))
            {
                // 계산 로직 없이, 잔고 객체가 가지고 있는 현재 수익률을 그대로 가져옵니다.
                잔고_수익률_스냅샷 = 잔고.수익률;
            }

            // =================================================================
            // [최적화 3] Task.Run을 사용하여 메인 스레드 대기 없이 즉시 백그라운드 실행
            // =================================================================
            Task.Run(() =>
            {
                // ----------------------------------------------------
                // [알바생 구역] UI 부하 없는 백그라운드 연산 처리
                // ----------------------------------------------------
                string 기록_검색식 = 검색식;
                double 등락률 = 고정_등락률;

                // [+] 밖에서 미리 찍어둔 수익률 사진(스냅샷)을 그대로 사용합니다.
                double 수익률 = 잔고_수익률_스냅샷;

                long 적용금액 = Method.적용금액계산(주문가, 수량, 현재가);
                string 취소N주문_문자열 = GET.Cansel_order_string(취소N주문);
                string 주문_문자열 = GET.After_order_string(주문, Value);

                int 현재주문카운트 = System.Threading.Interlocked.Increment(ref jumuncount) - 1;

                string 포맷된시간 = 주문시간;
                if (!주문시간.Contains(":"))
                {
                    if (int.TryParse(주문시간, out int 시간숫자))
                    {
                        포맷된시간 = 시간숫자.ToString("00':'00':'00");
                    }
                }

                // 하드디스크 파일 쓰기는 백그라운드에서 조용히 처리합니다.
                string 로그내용 = $"{현재주문카운트};{매수매도};{포맷된시간};{종목명};{기록_검색식};{등락률};{현재가};{주문가};{수량};{적용금액};{수익률};{주문_문자열};{반복}회;{취소N주문_문자열};{시장가구분};{취소대기}초;{종목코드};{주문번호};{접수내역};{비고}";
                Writing_Management.주문기록(로그내용);

                // ----------------------------------------------------
                // [사장님 구역] 미리 만들어둔 변수들만 화면(그리드)에 안전하게 출력합니다.
                // ----------------------------------------------------
                if (Form_JuMun.form != null && Form_JuMun.form.IsHandleCreated)
                {
                    Helper.안전한_UI_업데이트(Form_JuMun.form, () =>
                    {
                        if (GenieConfig.TB_주문row > 0 && Form_JuMun.form.JuMun_dataGridView != null)
                        {
                            Form_JuMun.form.JuMun_dataGridView.Rows.Insert(0);
                            Form_JuMun.form.JuMun_dataGridView["Num_주문A", 0].Value = 현재주문카운트;
                            Form_JuMun.form.JuMun_dataGridView["주문시간_주문A", 0].Value = 포맷된시간;
                            Form_JuMun.form.JuMun_dataGridView["검색식_주문A", 0].Value = 기록_검색식;
                            Form_JuMun.form.JuMun_dataGridView["접수내역_주문A", 0].Value = 접수내역;
                            Form_JuMun.form.JuMun_dataGridView["종목명_주문A", 0].Value = 종목명;
                            Form_JuMun.form.JuMun_dataGridView["P현재가_주문A", 0].Value = 현재가;
                            Form_JuMun.form.JuMun_dataGridView["주문유형_주문A", 0].Value = 매수매도;
                            Form_JuMun.form.JuMun_dataGridView["거래구분_주문A", 0].Value = 시장가구분;
                            Form_JuMun.form.JuMun_dataGridView["수익률_주문A", 0].Value = 수익률; // <- 잔고 수익률 그대로 출력
                            Form_JuMun.form.JuMun_dataGridView["주문가_주문A", 0].Value = 주문가;
                            Form_JuMun.form.JuMun_dataGridView["수량_주문A", 0].Value = 수량;
                            Form_JuMun.form.JuMun_dataGridView["적용금액_주문A", 0].Value = 적용금액;
                            Form_JuMun.form.JuMun_dataGridView["코드_주문A", 0].Value = 종목코드;
                            Form_JuMun.form.JuMun_dataGridView["취소대기_주문A", 0].Value = 취소대기 + "초";
                            Form_JuMun.form.JuMun_dataGridView["취소N주문_주문A", 0].Value = 취소N주문_문자열;
                            Form_JuMun.form.JuMun_dataGridView["P등락률_주문A", 0].Value = 등락률;
                            Form_JuMun.form.JuMun_dataGridView["주문번호_주문A", 0].Value = 주문번호;
                            Form_JuMun.form.JuMun_dataGridView["주문_주문A", 0].Value = 주문_문자열;
                            Form_JuMun.form.JuMun_dataGridView["반복_주문A", 0].Value = 반복 + "회";
                            Form_JuMun.form.JuMun_dataGridView["비고_주문A", 0].Value = 비고;

                            Form_JuMun.form.JuMun_dataGridView.CurrentCell = null;
                        }
                    });
                }
            });
        }
        //////////////////////           주문그리드뷰           ///////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        public static void OUT_DGV_print()// 미체결그리드뷰 그리기
        {
            if (Form_Outstanding.form.Outstanding_DataGridView.CurrentCell != null)
            {
                return;
            }

            var 미체결_그리드뷰 = Form_Outstanding.form.Outstanding_DataGridView;

            // [지니 최적화] 연산 도중 그리드뷰가 화면을 갱신하지 않도록 잠금
            미체결_그리드뷰.SuspendLayout();

            try
            {
                // =========================================================================
                // 1. [최우선 처리: 체결 및 취소된 주문 완전 삭제]
                // 가장 먼저, 화면에 남아있는 쓸모없는 데이터부터 싹 지워서 깨끗한 상태를 만듭니다.
                // =========================================================================
                for (int 인덱스 = 미체결_그리드뷰.Rows.Count - 1; 인덱스 >= 0; 인덱스--)
                {
                    var 현재_행 = 미체결_그리드뷰.Rows[인덱스];
                    var 검색식_상태 = 현재_행.Cells["검색식"]?.Value?.ToString();

                    if (검색식_상태 == "취소" || 검색식_상태 == "체결")
                    {
                        미체결_그리드뷰.Rows.RemoveAt(인덱스);
                    }
                }
                미체결_그리드뷰.CurrentCell = null;

                // =========================================================================
                // 2. 최대 행수 확인 및 데이터 추출
                // =========================================================================
                int 최대출력_행수 = 0;
                int.TryParse(Form_Outstanding.form.TB_미체결row.Text, out 최대출력_행수);

                // 3. 조건 확인: 최대 행 수가 0보다 크고, 콤보박스가 "( ALL )" 상태인 경우
                if (최대출력_행수 > 0 && Get.미체결종목_index < 1)
                {
                    // [지니 최적화] Contains("+") 대신 속도가 훨씬 빠른 IndexOf('+') < 0 으로 교체
                    List<JumunItem> 정렬된_주문리스트 = Form1.JumunItem_List.Values
                        .Where(주문 => 주문.주문번호.IndexOf('+') < 0)
                        .OrderBy(주문 => Math.Abs(주문.Tik_cap))
                        .ToList();

                    int 실제출력_개수 = Math.Min(최대출력_행수, 정렬된_주문리스트.Count);

                    // 화면 싹 비우기
                    미체결_그리드뷰.Rows.Clear();
                    Method.SortClear(미체결_그리드뷰);

                    for (int 인덱스 = 0; 인덱스 < 실제출력_개수; 인덱스++)
                    {
                        JumunItem 현재주문 = 정렬된_주문리스트[인덱스];

                        if (Find_OutstandingStock(현재주문))
                        {
                            Outstanding_insert(현재주문, 미체결_그리드뷰.Rows.Count);
                        }
                    }
                }
                else
                {
                    // =========================================================================
                    // 4. [개별 종목 모드] 남은 행들의 실시간 값 업데이트
                    // ALL 모드일 때는 위에서 전체를 싹 새로 그렸으므로 이 무거운 루프를 돌지 않도록 최적화했습니다.
                    // =========================================================================
                    foreach (DataGridViewRow 현재_행 in 미체결_그리드뷰.Rows)
                    {
                        var 그리드뷰_주문번호 = 현재_행.Cells["주문번호"]?.Value?.ToString();

                        // 그리드뷰_주문번호가 null일 때 튕기는 현상(ArgumentNullException) 완벽 방지
                        if (그리드뷰_주문번호 != null && Form1.JumunItem_List.TryGetValue(그리드뷰_주문번호, out JumunItem 검색된_주문))
                        {
                            현재_행.Cells["검색식"].Value = 검색된_주문.검색식;
                            현재_행.Cells["취소대기"].Value = 검색된_주문.취소timer + "초";
                            현재_행.Cells["반복"].Value = 검색된_주문.반복횟수 + " 회";
                            현재_행.Cells["미체결량"].Value = 검색된_주문.미체결량;
                            현재_행.Cells["현재가"].Value = 검색된_주문.현재가;
                            현재_행.Cells["등락률"].Value = 검색된_주문.등락률;
                            현재_행.Cells["수익률"].Value = 검색된_주문.수익률;
                            현재_행.Cells["주문"].Value = GET.After_order_string(검색된_주문.시장가구분, 검색된_주문.주문값);
                            현재_행.Cells["틱차이"].Value = 검색된_주문.Tik_cap + " 틱";
                            현재_행.Cells["주문가격"].Value = 검색된_주문.주문가격;
                        }
                        else
                        {
                            // 메인 장부에서 이미 사라진 주문이라면 텍스트를 변경하여 다음 턴 맨 위(1번)에서 지워지게 함
                            var 취소대기_시간 = 현재_행.Cells["취소대기"]?.Value?.ToString();
                            if (취소대기_시간 == "0초")
                            {
                                현재_행.Cells["검색식"].Value = "취소";
                            }
                            else
                            {
                                현재_행.Cells["검색식"].Value = "체결";
                            }
                        }
                    }
                }

                // 5. 예외 처리: 종목이 선택되어 있는데 리스트가 비었으면 ALL(0)로 되돌림
                if (Get.미체결종목_index > 0 && 미체결_그리드뷰.Rows.Count == 0)
                {
                    Form_Outstanding.form.CBB_미체결종목.SelectedIndex = 0;
                }
            }
            finally
            {
                // 모든 삭제와 갱신이 깔끔하게 끝난 뒤에 화면 락 해제
                미체결_그리드뷰.ResumeLayout();
            }

            return;
        }


        public static bool Find_OutstandingStock(JumunItem 타겟주문)
        {
            if (타겟주문 != null)
            {
                // 1. 주문번호에 '+' 문자가 포함되어 있다면, 무조건 false를 반환합니다.
                if (타겟주문.주문번호.Contains("+"))
                {
                    return false;
                }

                // 2. 주문번호에 '+' 문자가 포함되어 있지 않다면,
                //    DataGridView에 해당 주문번호가 있는지 확인합니다.

                // 🌟 LINQ Any()를 사용하여 순회를 대체하고 코드를 간결하게 만듭니다. 🌟
                // DataGridViewRow를 순회하며, 주문번호가 일치하는 행이 '하나라도 있는지' 확인합니다.
                bool 그리드뷰에_존재함 = Form_Outstanding.form.Outstanding_DataGridView.Rows.Cast<DataGridViewRow>()
                    .Any(현재_행 => 현재_행.Cells["주문번호"]?.Value?.ToString().Equals(타겟주문.주문번호) == true);

                // 그리드뷰에_존재함이 false일 때만 최종적으로 true를 반환합니다.
                // 즉, "주문번호에 '+'가 없고 && DGV에도 해당 주문번호가 없어야" true입니다.
                return !그리드뷰에_존재함;
            }
            else
            {
                return false;
            }
        }

        public static void Outstanding_insert(JumunItem 삽입할_주문, int 타겟_행번호)
        {
            // =================================================================
            // 1. [백그라운드 연산 구역] - 모든 데이터 계산과 문자열 조립을 미리 끝냄!
            // =================================================================

            // 이 검사도 밖에서 먼저 해버리면, +가 포함되어 있을 땐 UI 스레드를 아예 안 부름 (CPU 절약)
            if (삽입할_주문.주문번호.Contains("+")) return;

            // 미리 계산 및 문자열 조립
            string 텍스트_주문시간 = 삽입할_주문.주문시간.ToString("00':'00':'00");
            string 텍스트_매수매도 = 내부_매수매도(삽입할_주문.매수매도, 삽입할_주문.신용주문);
            string 텍스트_시장가 = 내부_시장가구분(삽입할_주문.매수매도);
            string 텍스트_취소대기 = $"{삽입할_주문.취소timer}초";
            string 텍스트_주문 = GET.After_order_string(삽입할_주문.매수매도, 삽입할_주문.주문값);
            string 텍스트_취소N주문 = GET.Cansel_order_string(삽입할_주문.취소N주문);
            string 텍스트_반복 = $"{삽입할_주문.반복횟수} 회";
            string 텍스트_틱차이 = $"{삽입할_주문.Tik_cap} 틱";
            long 계산된_적용금액 = Method.적용금액계산(삽입할_주문.주문가격, 삽입할_주문.미체결량, 삽입할_주문.현재가);

            // =================================================================
            // 2. [UI 업데이트 구역] - 완성된 결과물만 화면에 꽂아 넣음!
            // =================================================================
            Helper.안전한_UI_업데이트(Form_Outstanding.form, () =>
            {
                // 폼이 꺼져있을 때 뻗지 않도록 방어 로직 추가
                if (Form_Outstanding.form != null && Form_Outstanding.form.Outstanding_DataGridView != null)
                {
                    Form_Outstanding.form.Outstanding_DataGridView.Rows.Insert(타겟_행번호);

                    // 미리 가공해둔 변수들을 꽂아넣기만 함 (초경량)
                    Form_Outstanding.form.Outstanding_DataGridView["수익률", 타겟_행번호].Value = 삽입할_주문.수익률;
                    Form_Outstanding.form.Outstanding_DataGridView["주문시간", 타겟_행번호].Value = 텍스트_주문시간;
                    Form_Outstanding.form.Outstanding_DataGridView["검색식", 타겟_행번호].Value = 삽입할_주문.검색식;
                    Form_Outstanding.form.Outstanding_DataGridView["주문번호", 타겟_행번호].Value = 삽입할_주문.주문번호;
                    Form_Outstanding.form.Outstanding_DataGridView["종목코드", 타겟_행번호].Value = 삽입할_주문.종목코드;
                    Form_Outstanding.form.Outstanding_DataGridView["종목명", 타겟_행번호].Value = 삽입할_주문.종목명;
                    Form_Outstanding.form.Outstanding_DataGridView["매수매도", 타겟_행번호].Value = 텍스트_매수매도;
                    Form_Outstanding.form.Outstanding_DataGridView["시장가", 타겟_행번호].Value = 텍스트_시장가;
                    Form_Outstanding.form.Outstanding_DataGridView["취소대기", 타겟_행번호].Value = 텍스트_취소대기;
                    Form_Outstanding.form.Outstanding_DataGridView["주문", 타겟_행번호].Value = 텍스트_주문;
                    Form_Outstanding.form.Outstanding_DataGridView["취소N주문", 타겟_행번호].Value = 텍스트_취소N주문;
                    Form_Outstanding.form.Outstanding_DataGridView["주문수량", 타겟_행번호].Value = 삽입할_주문.주문수량;
                    Form_Outstanding.form.Outstanding_DataGridView["미체결량", 타겟_행번호].Value = 삽입할_주문.미체결량;
                    Form_Outstanding.form.Outstanding_DataGridView["현재가", 타겟_행번호].Value = 삽입할_주문.현재가;
                    Form_Outstanding.form.Outstanding_DataGridView["등락률", 타겟_행번호].Value = 삽입할_주문.등락률;
                    Form_Outstanding.form.Outstanding_DataGridView["반복", 타겟_행번호].Value = 텍스트_반복;
                    Form_Outstanding.form.Outstanding_DataGridView["주문가격", 타겟_행번호].Value = 삽입할_주문.주문가격;
                    Form_Outstanding.form.Outstanding_DataGridView["적용금액", 타겟_행번호].Value = 계산된_적용금액;
                    Form_Outstanding.form.Outstanding_DataGridView["틱차이", 타겟_행번호].Value = 텍스트_틱차이;

                    Form_Outstanding.form.Outstanding_DataGridView.CurrentCell = null;
                }
            });

            // -----------------------------------------------------------------
            // [로컬 헬퍼 함수] UI 업데이트 밖으로 빼냄
            // -----------------------------------------------------------------
            string 내부_매수매도(int 입력_매수매도, bool 신용여부)
            {
                string 결과_매수매도 = "";
                if (입력_매수매도 == 1)
                {
                    결과_매수매도 = "매수";
                    if (신용여부) 결과_매수매도 = "매수신용";
                }
                else if (입력_매수매도 == 2)
                {
                    결과_매수매도 = "매도";
                    if (신용여부) 결과_매수매도 = "매도신용";
                }
                else if (입력_매수매도 == 10 || 입력_매수매도 == 20)
                {
                    결과_매수매도 = "취소";
                }

                return 결과_매수매도;
            }

            string 내부_시장가구분(int 입력_시장가)
            {
                return 입력_시장가 == 0 ? "시장가" : "보통가";
            }
        }

        //////////////////////       미체결 그리드뷰 그리기        ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        public static void 최종매입가view()
        {
            DataGridView DGV = form1.DGV_최종매입가View;

            // 💡 [UI 마법 1] 데이터 넣는 동안 화면 그리기(깜빡임)를 일시 정지시킵니다! 속도가 10배 이상 빨라집니다.
            DGV.SuspendLayout();

            DGV.Rows.Clear();
            Method.SortClear(DGV);

            Stockbalance 잔고 = null;
            string Itemcode = "";
            string selectedName = form1.CBB_최종가종목.Text;

            // 💡 [최적화 1] Equals 대신 == 사용, 찾으면 즉시 루프 탈출(break)
            foreach (var item in stockBalanceList.Values)
            {
                if (item.종목명 == selectedName)
                {
                    Itemcode = item.종목코드;
                    잔고 = item;
                    break;
                }
            }

            // 💡 [방어막 1] 잔고를 못 찾았으면 안전하게 화면 정지를 풀고 함수를 탈출합니다.
            if (잔고 == null || string.IsNullOrEmpty(Itemcode))
            {
                DGV.ResumeLayout();
                return;
            }

            if (Form1.최종매입가_List.TryGetValue(Itemcode, out List<최종매입가> 종목데이터_리스트))
            {
                lock (종목데이터_리스트)
                {
                    // 💡 [속도 마법 2] 줄 번호를 0.00초 만에 찾기 위한 '이정표(Dictionary)'를 만듭니다.
                    // 기존처럼 DGV.Rows를 위에서부터 훑지 않고 다이렉트로 줄 위치를 찾아냅니다.
                    Dictionary<int, int> rowMap = new Dictionary<int, int>();

                    // 💡 [속도 마법 3] 리스트를 7번씩 쪼개서 돌지 않고, 딱 1번만 돕니다! (CPU 점유율 급감)
                    for (int i = 0; i < 종목데이터_리스트.Count; i++)
                    {
                        var 항목 = 종목데이터_리스트[i];
                        int colA = -1, colB = -1;

                        // 💡 Contains 7번 대신 궁극의 switch-case로 열(Column) 번호를 즉시 세팅합니다.
                        switch (항목.위치)
                        {
                            case "리밸_A": colA = 1; colB = 2; break;
                            case "리밸_B": colA = 3; colB = 4; break;
                            case "리밸_C": colA = 5; colB = 6; break;
                            case "리밸_D": colA = 7; colB = 8; break;
                            case "리밸_E": colA = 9; colB = 10; break;
                            case "리밸_F": colA = 11; colB = 12; break;
                            case "리밸_G": colA = 13; colB = 14; break;
                        }

                        // A~G가 아닌 이상한 값이면 무시하고 다음 데이터로 넘어갑니다.
                        if (colA == -1) continue;

                        int currentNum = 항목.번호;
                        int targetRowIndex;

                        // 💡 이 번호(currentNum)로 만들어진 줄이 있는지 이정표에서 0.00초 만에 확인!
                        if (!rowMap.TryGetValue(currentNum, out targetRowIndex))
                        {
                            // 💡 [방어막 2] 기존에 없으면 무조건 새 줄을 맨 밑에 추가하고, 그 안전한 '진짜 줄 번호'를 기억합니다.
                            targetRowIndex = DGV.Rows.Add();
                            DGV[0, targetRowIndex].Value = currentNum;
                            rowMap[currentNum] = targetRowIndex; // 다음 번 검사를 위해 이정표에 등록
                        }

                        // 💡 확보된 안전한 칸에 가격 데이터를 넣습니다. (인덱스 에러 0%)
                        DGV[colA, targetRowIndex].Value = 항목.매입가.ToString("N0");

                        // 💡 [최적화 4] 수익률 계산 함수를 굳이 따로 부르지 않고 바로 계산해서 메모리를 아낍니다.
                        double tax_ = (잔고.시장 == "E") ? 0 : TAX;
                        double 수익률 = Math.Truncate((((double)(잔고.현재가 - 항목.매입가) / 항목.매입가 * 100.0) - ((수수료 + 수수료 + tax_) * 100.0)) * 100.0) / 100.0;

                        DGV[colB, targetRowIndex].Value = 수익률.ToString("N2");
                    }
                }
            }

            DGV.ClearSelection();

            // 💡 [UI 마법 2] 계산이 완벽하게 다 끝난 후, 정지했던 화면을 짠! 하고 한 번에 뿌려줍니다. (버벅임 제로)
            DGV.ResumeLayout();
        }

        public static void 체결DGV_print(string 체_주문번호, string 코드, string 체_종목명, int 체_체결가, int 체_체결량,
                                  int 체_주문수량, int 체_주문N체결시간, string 체_주문유형, string 체_거래구분, int 체_현재가,
                                  string 검색식, double 체_등락률, double 체_수익률)
        {
            // =================================================================
            // 1. [백그라운드 연산 구역] - 모든 문자열 조립과 계산을 미리 끝냄!
            // =================================================================

            // 금액 오버플로우 방지 계산
            long 누적금액 = (long)체_체결가 * (long)체_체결량;

            // 안전한 문자열 처리 (Substring 에러 방어)
            string safeOrderType = string.IsNullOrEmpty(체_주문유형) ? "" :
                                  (체_주문유형.Length > 1 ? 체_주문유형.Substring(1) : 체_주문유형);

            // 시간 문자열 포맷팅
            string 체결시간_포맷 = 체_주문N체결시간.ToString("00':'00':'00");

            // =================================================================
            // 2. [UI 업데이트 구역] - 완성된 결과물만 화면에 꽂아 넣음!
            // =================================================================
            Helper.안전한_UI_업데이트(Form_Conclusion.form, () =>
            {
                try
                {
                    // 폼이나 그리드가 닫혀있을 때를 대비한 방어
                    if (Form_Conclusion.form == null || Form_Conclusion.form.Conclusion_DataGridView == null) return;

                    var dgv = Form_Conclusion.form.Conclusion_DataGridView;

                    // 컬럼이 실제로 존재하는지 확인 (오류 방지)
                    if (dgv.Columns["주문번호_체결"] == null) return;

                    int orderNumColIdx = dgv.Columns["주문번호_체결"].Index;
                    int foundRowIndex = -1;

                    // 1. 기존 주문번호 찾기
                    for (int i = 0; i < dgv.RowCount; i++)
                    {
                        if (dgv.Rows[i].IsNewRow) continue;

                        var cellValue = dgv[orderNumColIdx, i].Value;
                        if (cellValue != null && cellValue.Equals(체_주문번호))
                        {
                            foundRowIndex = i;
                            break;
                        }
                    }

                    // 2. 업데이트 또는 신규 추가
                    if (foundRowIndex >= 0)
                    {
                        // [UPDATE] 기존 행 업데이트 (미리 계산된 누적금액 사용)
                        dgv["누적체결량_체결", foundRowIndex].Value = 체_체결량;
                        dgv["체결가_체결", foundRowIndex].Value = 체_체결가;
                        dgv["누적금액_체결", foundRowIndex].Value = 누적금액;
                    }
                    else
                    {
                        // [INSERT] 신규 행 추가
                        // (최적화 팁: TB_체결row 값을 전역 int 변수로 들고 있으면 TryParse를 뺄 수 있음)
                        if (int.TryParse(Form1.form1.TB_체결row.Text, out int limitRowCount) && limitRowCount > 0)
                        {
                            dgv.Rows.Insert(0); // 맨 위에 행 추가

                            // 미리 가공해둔 변수들을 꽂아넣기만 함
                            dgv["Num_체결", 0].Value = Get.체결갯수;
                            dgv["체결시간_체결", 0].Value = 체결시간_포맷;
                            dgv["종목명_체결", 0].Value = 체_종목명;
                            dgv["검색식_체결", 0].Value = 검색식;
                            dgv["주문유형_체결", 0].Value = safeOrderType;
                            dgv["거래구분_체결", 0].Value = 체_거래구분;
                            dgv["체결가_체결", 0].Value = 체_체결가;
                            dgv["주문수량_체결", 0].Value = 체_주문수량;
                            dgv["누적체결량_체결", 0].Value = 체_체결량;
                            dgv["P현재가_체결", 0].Value = 체_현재가;
                            dgv["P등락률_체결", 0].Value = 체_등락률;
                            dgv["주문번호_체결", 0].Value = 체_주문번호;
                            dgv["코드_체결", 0].Value = 코드;
                            dgv["누적금액_체결", 0].Value = 누적금액;
                            dgv["수익률_체결", 0].Value = 체_수익률;

                            dgv.ClearSelection();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 로그 출력도 가급적 밖으로 빼거나 File I/O를 비동기로 하는 것이 좋습니다.
                    Form1.Console_print($"[체결Grid 에러] {ex.Message}");
                }
            });
        }
    }
}
