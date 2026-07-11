using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64
{
    internal class 홀딩잔고:Form1
    {
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////       잔고정보 메소드모음         ////////////////

        // 클래스 상단 어딘가에 (또는 DataManagement 클래스 안에) 자물쇠 객체를 딱 하나 만들어 줍니다.
        private static readonly object _lockAcc = new object();

        public static void 예수금업데이트(string 매수매도, int 주문가격, int 주문수량, string 상황이름, string 종목코드, bool 원주문_신용여부)
        {
            // 1. 사전 계산
            long 총금액 = (long)주문가격 * 주문수량;
            double tax_rate = Form1.TAX;
            double 증거금률 = 1.0;

            if (Form1.Market_Item_List.TryGetValue(종목코드, out Market_Item marketItem))
            {
                if (marketItem.Market == "E") tax_rate = 0;

                // 신용주문일 경우, 종목 기본 증거금률과 45% 중 큰 값을 무조건 적용
                if (원주문_신용여부)
                {
                    증거금률 = Math.Max(0.45, marketItem.증거금률);
                }
            }

            long 차감필요금액 = (long)(총금액 * 증거금률);
            int 체결수수료 = (int)(총금액 * Form1.수수료);

            // 2. 매수/매도 정확한 판정
            bool 진짜매수계열 = (매수매도.Contains("수") && !매수매도.Contains("매도"));
            bool 진짜매도계열 = (매수매도.Contains("도") || 매수매도.Contains("매도"));

            lock (_lockAcc)
            {
                long 전_D2 = Form1.Acc.D2;
                long 전_D2추정 = Form1.Acc.추정D2;

                switch (상황이름)
                {
                    case "주문":
                        if (진짜매수계열)
                        {
                            Form1.Acc.추정D2 = 전_D2추정 - 차감필요금액;
                        }
                        break;

                    case "취소":
                    case "일괄취소":
                        if (진짜매수계열)
                        {
                            Form1.Acc.추정D2 = 전_D2추정 + 차감필요금액;
                        }
                        break;

                    case "체결":
                        if (진짜매수계열)
                        {
                            Form1.Acc.D2 = 전_D2 - 차감필요금액 - 체결수수료;
                            Form1.Acc.추정D2 = 전_D2추정 - 체결수수료;
                        }
                        else if (진짜매도계열)
                        {
                            long 정산금액 = 0;
                            int 체결세금 = (int)(총금액 * tax_rate);

                            if (원주문_신용여부)
                            {
                                long 갚아야할_대출금 = (long)(총금액 * (1.0 - 증거금률));
                                정산금액 = 총금액 - 갚아야할_대출금 - 체결수수료 - 체결세금;
                            }
                            else
                            {
                                정산금액 = 총금액 - 체결수수료 - 체결세금;
                            }

                            Form1.Acc.D2 = 전_D2 + 정산금액;
                            Form1.Acc.추정D2 = 전_D2추정 + 정산금액;
                        }
                        break;
                }

                // 최후의 안전장치: 혹시 모를 사태를 대비해 마이너스 감지 로그만 조용히 남겨둡니다.
                if (Form1.Acc.추정D2 < 0)
                {
                    Form1.Console_print($"[위험] 추정D2 마이너스 감지: {Form1.Acc.추정D2:N0}원");
                }
            }
        }


        // =========================================================================================
        // [함수] 매도/취소 시 잔고(주문가능수량)를 업데이트합니다.
        // -----------------------------------------------------------------------------------------
        // 파라미터 설명:
        //   원주문_신용여부: 매도 시 차감할 장부(신용/현금)를 결정하거나, 취소 시 복구할 장부를 결정
        // =========================================================================================
        public static void 주문가능수업데이트(Stockbalance 잔고, string 매수매도, int 주문수량, string 상황이름, bool 원주문_신용여부)
        {
            // 1. 🚨 [필수 안전장치] 잔고 객체가 없으면 봇이 뻗지 않도록 즉시 종료
            if (잔고 == null) return;

            lock (잔고)
            {
                switch (상황이름)
                {
                    // =================================================================
                    // CASE 1: [매도 주문] -> 넘겨받은 신용여부에 따라 각각 차감
                    // =================================================================
                    case "매도주문":
                        if (원주문_신용여부)
                        {
                            잔고.신용_주문가능수량 -= 주문수량;
                            if (잔고.신용_주문가능수량 < 0) 잔고.신용_주문가능수량 = 0; // 마이너스 방지
                        }
                        else
                        {
                            잔고.주문가능수량 -= 주문수량;
                            if (잔고.주문가능수량 < 0) 잔고.주문가능수량 = 0; // 마이너스 방지
                        }
                        break;

                    // =================================================================
                    // CASE 2: [주문 취소] -> 취소된 수량만큼 다시 장부에 복구 (+)
                    // =================================================================
                    case "취소":
                        if (매수매도.Contains("도취소"))
                        {
                            if (원주문_신용여부)
                                잔고.신용_주문가능수량 += 주문수량; // 신용 복구
                            else
                                잔고.주문가능수량 += 주문수량;     // 현금 복구
                        }
                        break;

                    // =================================================================
                    // CASE 3: [일괄 취소]
                    // =================================================================
                    case "일괄취소":
                        if (매수매도 == "매도")
                        {
                            if (원주문_신용여부)
                                잔고.신용_주문가능수량 += 주문수량;
                            else
                                잔고.주문가능수량 += 주문수량;
                        }
                        break;
                }
            }
        }

        public static void 신규매수개수확인(string 검색식, bool 추가)
        {
            if (추가)
            {
                if (검색식.Contains("신규_A")) Get.신규개수A++;
                if (검색식.Contains("신규_B")) Get.신규개수B++;
                if (검색식.Contains("신규_C")) Get.신규개수C++;
            }
            else
            {
                if (Get.신규개수A > 0) if (검색식.Contains("신규_A")) Get.신규개수A--;
                if (Get.신규개수B > 0) if (검색식.Contains("신규_B")) Get.신규개수B--;
                if (Get.신규개수C > 0) if (검색식.Contains("신규_C")) Get.신규개수C--;
            }
        }


        ////////////////////////       잔고정보 메소드모음         ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        public static void JanGo_dataGridView_print()// 잔고그리드뷰 그리기
        {
            try
            {
                if (Form1.form1.tab_잔고.TabPages[0].Contains(Form1.form1.JanGo_dataGridView) || (Sub_form.form != null && (GenieConfig.Sub_layout =="A" || GenieConfig.Sub_layout == "B")))
                {
                    var dgv = Form1.form1.JanGo_dataGridView;

                    // ==========================================================
                    // 💡 [기억 장치] 렌더링 중지 직전, 스크롤 위치와 "선택된 셀"을 모두 외웁니다.
                    // ==========================================================
                    int 기억해둔_스크롤위치 = -1;
                    int 기억해둔_선택행 = -1;
                    int 기억해둔_선택열 = -1;

                    if (dgv.Rows.Count > 0)
                    {
                        기억해둔_스크롤위치 = dgv.FirstDisplayedScrollingRowIndex;

                        // 사용자가 특정 셀을 클릭(선택)해 둔 상태라면 그 좌표를 저장
                        if (dgv.CurrentCell != null)
                        {
                            기억해둔_선택행 = dgv.CurrentCell.RowIndex;
                            기억해둔_선택열 = dgv.CurrentCell.ColumnIndex;
                        }
                    }

                    dgv.SuspendLayout(); // 렌더링 중지
                    try
                    {
                        // 1. 루프를 역순으로 변경하여 DataGridView 행 삭제 시 인덱스 문제 방지
                        for (int i = dgv.Rows.Count - 1; i >= 0; i--)
                        {
                            // DataGridView에서 종목 코드를 가져옴
                            string 종목코드 = dgv["코드_잔고", i].Value?.ToString();

                            // 널 체크 및 잔고 데이터 가져오기
                            if (string.IsNullOrEmpty(종목코드) || !Form1.stockBalanceList.ContainsKey(종목코드))
                            {
                                // 잔고 목록에 없거나 코드가 유효하지 않으면 해당 행을 삭제하고 다음 루프로 진행
                                dgv.Rows.RemoveAt(i);
                                continue;
                            }

                            Stockbalance 잔고 = Form1.stockBalanceList[종목코드];

                            // 잔고 객체 자체의 널 체크 (딕셔너리에 키는 있지만 값이 null일 경우 대비)
                            if (잔고 == null) continue;

                            if (잔고.재매수 > 0)
                            {
                                // 재매수 처리: 잔고 행 갱신
                                JangoRow_print(i, 잔고);
                            }
                            else
                            {
                                // 재매수가 0 이하일 때 (전량 매도 가능성)
                                if (잔고.전량매도)
                                {
                                    // **전량 매도 처리 시작**

                                    // 2. 주문 취소 목록 처리
                                    List<JumunItem> Cansel_List = Form1.JumunItem_List.Values
                                                       .Where(o => o.종목코드.Equals(잔고.종목코드))
                                                       .ToList();

                                    foreach (JumunItem item in Cansel_List)
                                    {
                                        // 불필요한 널 체크 대신 foreach 사용 권장
                                        item.반복횟수 = 0; 
                                        item.취소시간 = 0;
                                        item.취소timer = 0;
                                    }

                                    // 3. 데이터 관리 및 로깅
                                    신규매수개수확인(잔고.초기매수검색식, false); // 전량매도 플래그

                                    string logHeader = $"[전량매도] {잔고.종목명}";
                                    string profitInfo = $"수익금: {잔고.누적손익:N0} 이 전량 매도 되었습니다.";
                                    string tradingStats = $"{잔고.종목명} 금일등락율: {잔고.등락율} 수익률: {Math.Round(잔고.수익률, 2)} 최고수익률: {Math.Round(잔고.최고수익률, 2)} 최저수익률: {Math.Round(잔고.최저수익률, 2)}";
                                    string moneyStats = $"{잔고.종목명} 금일수익금: {Method.단위변환(잔고.금일수익금)} 총수익금: {Method.단위변환(잔고.누적손익)} 최고예상손익금: {Method.단위변환(잔고.최고예상손익금)} 최저예상손익금: {Method.단위변환(잔고.최저예상손익금)} 금일매도금: {Method.단위변환(잔고.금일매도금)}";
                                    string dateInfo = $"{잔고.종목명} 매수검색식: {잔고.초기매수검색식} 초기매수일: {잔고.초기매수일:yyyy/MM/dd} 거래일: {잔고.거래일}";

                                    // 동작 로그
                                    Log.동작기록(" ");
                                    Log.동작기록(logHeader + " " + profitInfo);
                                    Log.동작기록(" ");

                                    // 에러 로그 (여기서는 중요한 정보 기록 용도)
                                    Log.에러기록(" ");
                                    Log.에러기록($"{잔고.종목명} 이 전량 매도 되었습니다.");
                                    Log.에러기록(tradingStats);
                                    Log.에러기록(moneyStats);
                                    Log.에러기록(dateInfo);
                                    Log.에러기록(" ");

                                    // 4. 전량 매도 기록
                                    Writing_Management.전량매도기록(
                                        $"{str.today};{잔고.종목명};{잔고.초기매수검색식};{잔고.초기매수일:yyyy/MM/dd};{잔고.거래일};" +
                                        $"{Method.단위변환(잔고.금일수익금)};{Method.단위변환(잔고.누적손익)};{Method.단위변환(잔고.최고예상손익금)};{Method.단위변환(잔고.최저예상손익금)};{Method.단위변환(잔고.금일매도금)};" +
                                        $"{잔고.등락율};{Math.Round(잔고.수익률, 2)};{Math.Round(잔고.최고수익률, 2)};{Math.Round(잔고.최저수익률, 2)};" +
                                        $"{Method.단위변환(Form1.Acc.실현손익)};{Method.단위변환(Form1.Acc.증가자산)}");

                                    // 5. 텔레그램 알림
                                    if (GenieConfig.CB_텔레그램사용)
                                    {
                                        _ = TelegramMessenger.Telegram_Send(
                                            $"[전량매도] :: 종목: {잔고.종목명} 매수식: {잔고.초기매수검색식} 초기매수일: {잔고.초기매수일:yyyy/MM/dd} 거래일: {잔고.거래일} 수익률: {잔고.수익률} " +
                                            $"금일수익금: {Method.단위변환(잔고.금일수익금)} 총수익금: {Method.단위변환(잔고.누적손익)} " +
                                            $"계좌실현손익: {Method.단위변환(Form1.Acc.실현손익)} 증가자산: {Method.단위변환(Form1.Acc.증가자산)} D2예수금: {Method.단위변환(Form1.Acc.추정D2)}");
                                    }

                                    Form1.비프음("실행");

                                    // 6. 재매수 목록 업데이트 및 저장
                                    재매수 Item = Form1.Rebuystock_List.Find(o => o.Itemcode.Contains(잔고.종목코드));
                                    if (Item != null)
                                    {
                                        Item.결과 = (잔고.금일수익금 < 0) ? "손실" : "수익";
                                        SaveToFile.Rebuystock_List_파일저장();
                                    }

                                    // 7. 주문 예약 목록 정리
                                    if (Form1.form1.주문예약_List.Count > 0)
                                    {
                                        // RemoveAll을 사용하여 조건에 맞는 항목을 한 번에 제거
                                        Form1.form1.주문예약_List.RemoveAll(item => item.전량매도삭제 && item.종목코드.Equals(잔고.종목코드));
                                    }

                                    // 8. 최종가 종목 콤보박스에서 제거
                                    if (Form1.form1.CBB_최종가종목.Items.Contains(잔고.종목명))
                                    {
                                        Form1.form1.CBB_최종가종목.Items.Remove(잔고.종목명);
                                    }

                                    // 9. 콘솔 로그 및 거부 목록 정리
                                    Console_print($"############## [{잔고.종목명}] 잔고 전량 매도 ##############");

                                    Form1.form1.신규거부로그_List.Remove(잔고.종목명);
                                    Form1.form1.추매거부로그_List.Remove(잔고.종목명);
                                    Form1.form1.매도거부로그_List.Remove(잔고.종목명);

                                    // 10. DataGridView 행 및 stockBalanceList 제거
                                    dgv.Rows.RemoveAt(i); // 역순 루프의 장점

                                    Form1.stockBalanceList.TryRemove(잔고.종목코드, out _); // 잔고 딕셔너리에서 제거

                                    // 11. 최종 데이터 저장 및 동기화
                                    SaveToFile.잔고_파일저장();
                                    Tab_AccountManagement.감시주문동기화();

                                    // **전량 매도 처리 종료**
                                }
                            }
                        }
                    }
                    finally
                    {
                        // ==========================================================
                        // 💡 [복구 장치] 렌더링 재개 직전, 기억해둔 "선택"과 "스크롤"을 강제 복구!
                        // ==========================================================

                        // 1. 선택(포커스) 복구
                        if (기억해둔_선택행 >= 0 && 기억해둔_선택행 < dgv.Rows.Count && 기억해둔_선택열 >= 0)
                        {
                            try { dgv.CurrentCell = dgv[기억해둔_선택열, 기억해둔_선택행]; } catch { dgv.CurrentCell = null; }
                        }
                        else
                        {
                            // 만약 전량 매도 등으로 내가 클릭했던 종목이 삭제되었다면 포커스 날림
                            dgv.CurrentCell = null;
                        }

                        // 2. 스크롤 위치 복구 (반드시 선택 복구 후에 해야 스크롤이 안 튑니다!)
                        if (기억해둔_스크롤위치 >= 0 && 기억해둔_스크롤위치 < dgv.Rows.Count)
                        {
                            try { dgv.FirstDisplayedScrollingRowIndex = 기억해둔_스크롤위치; } catch { }
                        }

                        dgv.ResumeLayout(); // 렌더링 재개
                    }
                }
            }
            catch
            {
                Log.에러기록("[에러] 잔고표시 에러");
            }
        }


        public static void JangoRow_print(int i, Stockbalance 잔고)
        {

            // =========================================================================
            // 🛡️ [절대 방어] 이 함수는 무조건 UI 스레드에서 실행되도록 강제합니다.
            // =========================================================================
            if (Form1.form1.InvokeRequired)
            {
                // 백그라운드에서 왔다면, UI 스레드로 던지고 '즉시 종료'
                Form1.form1.Invoke(new MethodInvoker(delegate { JangoRow_print(i, 잔고); }));
                return;
            }
            // =========================================================================

            var dgv = Form1.form1.JanGo_dataGridView;
            var dgvRow = dgv.Rows[i];

            // 중복 코드를 줄이기 위한 헬퍼 메서드: 값이 다를 때만 업데이트
            void UpdateCellValue(string columnName, object newValue)
            {
                if (dgv[columnName, i].Value == null || !dgv[columnName, i].Value.Equals(newValue))
                {
                    dgv[columnName, i].Value = newValue;
                }
            }

            // 매매가능 여부 설정 (매매_가능 변수 재사용)
            string 매매_가능 = 잔고.매매가능 ? "T" : "F";
            // 매매가능이 변경되었을 때만 종목상태 업데이트 필요

            // 지역 함수: 매입가_수익률 계산
            double 매입가_수익률(int 매입가)
            {
                double tax_ = Form1.TAX;
                if (잔고.시장.Equals("E")) tax_ = 0;

                // Math.Truncate를 사용하여 소수점 두 자리까지만 표시
                return Math.Truncate((((double)(잔고.현재가 - 매입가) / 매입가 * (double)100) - ((Form1.수수료 + Form1.수수료 + tax_) * 100)) * 100) / 100;
            }

      
        // ----------------------------------------------------------------------------------------------------

        // 1. 선택 여부
        UpdateCellValue("선택_잔고", 잔고.선택);

            // DataGridViewComboBoxCell 처리 (문자열 비교 및 인덱스 안전 처리)
            var cbx = (DataGridViewComboBoxCell)dgvRow.Cells["그룹_잔고"];

            // 1. 인덱스 가져오기
            int targetIndex = 잔고.매매그룹;

            // [수정 포인트] 값이 -1이면 0으로 강제 변경
            if (targetIndex == -1)
            {
                targetIndex = 0;
            }

            // [안전장치] 인덱스가 유효한지(콤보박스 아이템 개수보다 작은지) 확인
            // 콤보박스에 아이템이 하나도 없거나, 인덱스가 범위를 벗어나면 에러가 나므로 이를 방지합니다.
            if (cbx.Items.Count > 0 && targetIndex >= 0 && targetIndex < cbx.Items.Count)
            {
                string newGroupValue = cbx.Items[targetIndex].ToString();

                // 값 변경 여부 확인 (화면 깜빡임 방지 최적화)
                if (cbx.Value == null || !cbx.Value.ToString().Equals(newGroupValue))
                {
                    cbx.Value = newGroupValue;
                }
            }

            // 3. 주요 잔고 및 손익 정보
            UpdateCellValue("누적손익_잔고", 잔고.누적손익);
            UpdateCellValue("예상손익_잔고", 잔고.예상손익);
            UpdateCellValue("금일수익금_잔고", 잔고.금일수익금);

            // 5. 시장 구분 (조건부 업데이트)
            string currentMarket = 잔고.시장;
            if (currentMarket.Equals("R"))
            {
                currentMarket = Form1.Market_Item_List.ContainsKey(잔고.종목코드) ? Form1.Market_Item_List[잔고.종목코드].Market : currentMarket;
            }

            UpdateCellValue("시장구분_잔고", 잔고.시장);

          

            // 7. 기타 상세 정보
            string 종목상태_매매가능 = 잔고.종목상태 + "｜" + 매매_가능;
            UpdateCellValue("종목명_잔고", 잔고.종목명);
            UpdateCellValue("종목상태_잔고", 종목상태_매매가능); // 종목상태와 매매가능 상태가 합쳐져서 업데이트
            UpdateCellValue("현재가_잔고", 잔고.현재가);
            UpdateCellValue("등락율_잔고", 잔고.등락율);
       
            UpdateCellValue("매수횟수_잔고", 잔고.매수횟수);
            UpdateCellValue("매도횟수_잔고", 잔고.매도횟수);
            UpdateCellValue("거래일_잔고", 잔고.거래일);
            
        
            UpdateCellValue("초기매수일_잔고", 잔고.초기매수일.ToString("yyyy/MM/dd"));
            UpdateCellValue("초기매수검색식", 잔고.초기매수검색식);
            UpdateCellValue("재매수_잔고", 잔고.재매수);
            UpdateCellValue("추가매수일_잔고", 잔고.매수일);

            if (Form1.form1.수익금or수익률)
            {
                UpdateCellValue("최고수익률_잔고", 잔고.최고예상손익금.ToString("###,###"));
                UpdateCellValue("최저수익률_잔고", 잔고.최저예상손익금.ToString("###,###"));
            }
            else
            {
                UpdateCellValue("최고수익률_잔고", 잔고.최고수익률);
                UpdateCellValue("최저수익률_잔고", 잔고.최저수익률);
            }

            UpdateCellValue("금일매수금_잔고", 잔고.금일매수금);
            UpdateCellValue("금일매도금_잔고", 잔고.금일매도금);
            UpdateCellValue("전일매수량_잔고", 잔고.전일매수량 );
            UpdateCellValue("전일매도량_잔고", 잔고.전일매도량 );
            UpdateCellValue("보유비중_잔고", 잔고.보유비중 );

            string 현융합 = "합";
            if (잔고.보유수량 == 0) 현융합 = "융";
            if (잔고.신용_보유수량 == 0) 현융합 = "현";
            UpdateCellValue("융자_잔고", 현융합);

            UpdateCellValue("보유수량_잔고", $"{잔고.보유수량} / {잔고.신용_보유수량}");
            UpdateCellValue("주문가능수량_잔고", $"{잔고.주문가능수량} / {잔고.신용_주문가능수량}");

            UpdateCellValue("매입금액_잔고", 잔고.매입금액 + 잔고.신용_매입금액);
            UpdateCellValue("평가금액_잔고", 잔고.평가금액 + 잔고.신용_평가금액);

            // ★ [수정됨] 단순 합산이 아니라, 수수료/세금/이자를 뺀 '진짜 순손익' 표시
            UpdateCellValue("평가손익_잔고", 잔고.평가손익);

            UpdateCellValue("수익률_잔고", 잔고.수익률);

            // 평균단가 계산 (0 나누기 방지)
            long 총매입 = 잔고.매입금액 + 잔고.신용_매입금액;
            long 총수량 = 잔고.보유수량 + 잔고.신용_보유수량;
            long 평균단가 = (총수량 > 0) ? 총매입 / 총수량 : 0;

            UpdateCellValue("평균단가_잔고", 평균단가);

            // 신용이자 표시
            long _총신용이자 = 신용이자_계산하기(잔고);
            UpdateCellValue("신용이자", _총신용이자.ToString("N0"));

            long 신용이자_계산하기(Stockbalance 잔고)
            {
                long 이자합계 = 0;

                // 리스트가 있고, 내용물이 있을 때만 계산
                if (잔고.신용상세리스트 != null && 잔고.신용상세리스트.Count > 0)
                {
                    foreach (var 상세 in 잔고.신용상세리스트)
                    {
                        이자합계 += 상세.신용이자;
                    }
                }

                return 이자합계;
            }

            // 8. 시작 가격 및 수익률 (설정 기반)
            if (GenieConfig.CB_시작가격보기)
            {
                UpdateCellValue("시작가", 잔고.시작가격);
                UpdateCellValue("시작%", 매입가_수익률(잔고.시작가격));
            }

            // 9. 기준 가격 및 수익률 (설정 및 폼 상태 기반)
            if (GenieConfig.CB_기준가격보기)
            {
                if (Form1.form1.기준값변경)
                {
                    UpdateCellValue("기준가", 잔고.기준가격);
                    UpdateCellValue("기준%", 잔고.기준수익률);
                }
            }

            // 10. 최종 매입가 (리밸런싱) - 최적화된 지역 함수
            void UpdateFinalPurchasePrice(bool isEnabled, string location, string charCode)
            {
                // 1. 사용 안 함 설정이면 즉시 종료 (빠른 탈출)
                if (!isEnabled) return;

                // [지니 최적화] 전체 리스트 검색(FindAll) 제거 -> 딕셔너리에서 종목코드로 즉시 조회
                if (Form1.최종매입가_List.TryGetValue(잔고.종목코드, out List<최종매입가> 해당종목_리스트))
                {
                    List<최종매입가> 타겟_리스트 = null;

                    // 리스트 사용 중 충돌 방지 (Lock)
                    lock (해당종목_리스트)
                    {
                        // 2. 해당 종목의 리스트 중에서, 현재 위치(예: "리밸_A")에 해당하는 것만 추려냄
                        //    (전체 주문이 아니라, 해당 종목 몇 개 중에서만 고르므로 매우 빠름)
                        타겟_리스트 = 해당종목_리스트.Where(o => o.위치.Equals(location)).ToList();
                    }

                    // 3. 데이터가 있을 때만 계산 및 UI 업데이트
                    if (타겟_리스트.Count > 0)
                    {
                        // 번호가 가장 큰(최신) 항목 하나만 가져오기
                        // 전체 정렬(OrderBy) 후 리스트 변환보다, Max값 하나 찾는 게 훨씬 가벼움
                        var 최신_항목 = 타겟_리스트.OrderByDescending(o => o.번호).FirstOrDefault();

                        if (최신_항목 != null)
                        {
                            var finalPrice = 최신_항목.매입가;

                            // UI 업데이트 (기존 로직 유지)
                            UpdateCellValue($"차수_{charCode}", 타겟_리스트.Count - 1);
                            UpdateCellValue($"최종가_{charCode}", finalPrice);
                            UpdateCellValue($"수익률_{charCode}", 매입가_수익률(finalPrice));
                        }
                    }
                }
            }

            // 호출부는 기존 그대로 유지
            UpdateFinalPurchasePrice(GenieConfig.CB_최종매입가_A, "리밸_A", "A");
            UpdateFinalPurchasePrice(GenieConfig.CB_최종매입가_B, "리밸_B", "B");
            UpdateFinalPurchasePrice(GenieConfig.CB_최종매입가_C, "리밸_C", "C");
            UpdateFinalPurchasePrice(GenieConfig.CB_최종매입가_D, "리밸_D", "D");
            UpdateFinalPurchasePrice(GenieConfig.CB_최종매입가_E, "리밸_E", "E");
            UpdateFinalPurchasePrice(GenieConfig.CB_최종매입가_F, "리밸_F", "F");
            UpdateFinalPurchasePrice(GenieConfig.CB_최종매입가_G, "리밸_G", "G");

            // [Helper] 모니터링 및 정지 상태 업데이트
            // (변경사항: 변수명 문자열 대신 bool 값 직접 수신, 미사용 monitorType 인자 제거)
            void UpdateMonitorStatus(bool isEnabled, string columnName, object value)
            {
                // [최적화] 리플렉션 제거 -> 인자로 받은 값(isEnabled)이 True일 때만 UI 갱신
                if (isEnabled)
                {
                    UpdateCellValue(columnName, value);
                }
            }

            // 4. 모니터링 상태 (GenieConfig 변수 직접 전달)
            UpdateMonitorStatus(GenieConfig.CB_익회모니터, "일회모니터", GET.전광판현황(잔고, "일회"));
            UpdateMonitorStatus(GenieConfig.CB_익절모니터, "익절 & 트레일링", GET.전광판현황(잔고, "익절"));
            UpdateMonitorStatus(GenieConfig.CB_보전모니터, "보전모니터", GET.전광판현황(잔고, "보전"));
            UpdateMonitorStatus(GenieConfig.CB_손절모니터, "손절모니터", GET.전광판현황(잔고, "손절"));

            UpdateMonitorStatus(GenieConfig.CB_시간청산범위, "시간청산", GET.전광판현황(잔고, "시간청산"));
            UpdateMonitorStatus(GenieConfig.CB_반복매매범위, "반복매매 범위모니터", GET.전광판현황(잔고, "반복"));
            UpdateMonitorStatus(GenieConfig.CB_리밸런싱범위, "리밸런싱", GET.전광판현황(잔고, "리밸"));
            UpdateMonitorStatus(GenieConfig.CB_잔고청산범위, "잔고청산", GET.전광판현황(잔고, "잔고청산"));

            // 5. 매매 정지/청산 상태 (GenieConfig 변수 직접 전달)
            // (기존 코드에서 null을 넘기던 인자는 제거된 함수 정의에 맞춰 생략)
            UpdateMonitorStatus(GenieConfig.CB_매도X, "매도_정지", 잔고.매도정지);
            UpdateMonitorStatus(GenieConfig.CB_추매X, "추매_정지", 잔고.추매정지);

            UpdateMonitorStatus(GenieConfig.CB_잔고청산_A, "잔고청산_A", 잔고.잔고청산_A);
            UpdateMonitorStatus(GenieConfig.CB_잔고청산_B, "잔고청산_B", 잔고.잔고청산_B);
            UpdateMonitorStatus(GenieConfig.CB_잔고청산_C, "잔고청산_C", 잔고.잔고청산_C);

            // 12. 매매 정지/청산 상태 (설정 기반)
            if (GenieConfig.CB_매도X) UpdateCellValue("매도_정지", 잔고.매도정지);
            if (GenieConfig.CB_추매X) UpdateCellValue("추매_정지", 잔고.추매정지);
            if (GenieConfig.CB_잔고청산_A) UpdateCellValue("잔고청산_A", 잔고.잔고청산_A);
            if (GenieConfig.CB_잔고청산_B) UpdateCellValue("잔고청산_B", 잔고.잔고청산_B);
            if (GenieConfig.CB_잔고청산_C) UpdateCellValue("잔고청산_C", 잔고.잔고청산_C);


            if (Form1.form1.tab_주문.SelectedIndex != 2)
            {
                return;
            }

            // 13. 최종 매입가 DGV 업데이트 (잔고와 종목명이 일치하는 경우)
            string 종목명 = Form1.form1.CBB_최종가종목.Text;
            if (종목명 == 잔고.종목명)
            {
                DataGridView DGV = Form1.form1.DGV_최종매입가View;

                // [지니 최적화] 전체 검색(FindAll) 대신, 딕셔너리에서 해당 종목 리스트만 즉시 가져옴
                if (Form1.최종매입가_List.TryGetValue(잔고.종목코드, out List<최종매입가> 해당종목_리스트))
                {
                    if (DGV.Rows.Count > 0)
                    {
                        // [로컬 함수] 특정 위치(pos)의 데이터를 DGV 특정 열(colIndex)에 업데이트
                        void DGVupdata(string pos, int colIndex)
                        {
                            List<최종매입가> 타겟_리스트 = null;

                            // 리스트는 스레드 안전하지 않으므로 잠금(Lock) 후 필터링
                            lock (해당종목_리스트)
                            {
                                // 해당 위치(예: 리밸_A)인 것만 골라냄
                                타겟_리스트 = 해당종목_리스트.Where(o => o.위치 == pos).ToList();
                            }

                            // 1. 데이터 개수가 DGV 행보다 많아지면 전체 새로고침 (기존 로직 유지)
                            if (타겟_리스트.Count > DGV.Rows.Count)
                            {
                                GridView_Print.최종매입가view();
                                return;
                            }

                            // 2. 셀 값 업데이트
                            for (int i = 0; i < 타겟_리스트.Count; i++)
                            {
                                var item = 타겟_리스트[i];
                                if (item != null)
                                {
                                    // '매입가_수익률' 함수는 외부에 있다고 가정
                                    string 새로운_수익률 = 매입가_수익률(item.매입가).ToString("N2");

                                    // [최적화] 값이 실제로 변했을 때만 UI 업데이트 (깜빡임 방지 핵심)
                                    var 현재_값 = DGV[colIndex, i].Value;

                                    if (현재_값 == null || !현재_값.ToString().Equals(새로운_수익률))
                                    {
                                        DGV[colIndex, i].Value = 새로운_수익률;
                                    }
                                }
                            }
                        }

                        // 각 위치별 업데이트 실행
                        DGVupdata("리밸_A", 2);
                        DGVupdata("리밸_B", 4);
                        DGVupdata("리밸_C", 6);
                        DGVupdata("리밸_D", 8);
                        DGVupdata("리밸_E", 10);
                        DGVupdata("리밸_F", 12);
                        DGVupdata("리밸_G", 14);
                    }
                }
            }
        }






    }
}
