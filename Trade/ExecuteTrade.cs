using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 지니64.RESTAPI;

namespace 지니64
{
    public class ExecuteTrade : Form1
    {

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////       서버주문 전달         //////////////////////

        // [메인 함수] 잔고 주문 오더 (진입점)
        public static bool 잔고주문_오더(Stockbalance 잔고, string 검색식, int 매수매도, double 비중, int 비중단위, double 주문값, int 시장가구분, int 취소시간, int 취소N주문, int 반복횟수, string 비고, string 위치, int 수익구분, bool 청산, double 범위_1)
        {
            // 1. 데이터 준비
            string 종목코드 = 잔고.종목코드;
            if (!Form1.Market_Item_List.ContainsKey(종목코드)) return false;
            Market_Item 종목_데이터 = Form1.Market_Item_List[종목코드];

            // 2. 주문 가격 계산 (0: 매도 시 시장가 등 고려, 1: 매수 시 계산)
            // 매도(0)일 때는 0원으로 설정했다가 나중에 현재가로 기록
            int 주문가격 = (매수매도 == 0) ? 0 : Method.Order_price(주문값, 시장가구분, 종목_데이터.종목코드, 잔고.현재가);
            int 기록_주문가격 = (매수매도 == 0) ? 잔고.현재가 : 주문가격;

            // 3. 매매 가능 여부 확인 (VI, 모의투자, 장운영 상태 등)
            if (!Method.매매확인_VI_모투가능확인(종목_데이터, 매수매도)) return false;

            // 4. 기본 주문 수량 계산
            int 주문수량 = Method.주문수량계산(잔고, 기록_주문가격, 비중, 비중단위);
            if (주문수량 < 1)
            {
                주문수량 = 1;
            }

            bool 주문_성공여부 = false;

            // =========================================================
            // [분기 1] 매수 로직 (예수금 확인 필수)
            // =========================================================
            if (매수매도 == 1)
            {
                // 예수금 확인 및 수량 조절 (한글 함수 호출)
                주문수량 = 예수금확인_및_수량조절(잔고, 종목_데이터, 기록_주문가격, 주문수량, 검색식);

                if (주문수량 > 0)
                {
                    //   Form1.Console_print($"잔고:{잔고.종목명} -> 주문수량: {주문수량}");

                    // 최종 주문 실행
                    주문_성공여부 = 최종_주문_실행(잔고, 검색식, 매수매도, 비중, 비중단위, 주문값, 시장가구분, 취소시간, 취소N주문, 반복횟수, 비고, 위치, 수익구분, 기록_주문가격, 주문수량);

                 //   Form1.Console_print($"잔고:{잔고.종목명} -> 최종_주문_실행: {주문_성공여부}");
                }
            }
            // =========================================================
            // [분기 2] 매도 로직 (청산 및 손절 계산)
            // =========================================================
            else
            {
                // 1. 청산 주문일 경우 수량 재계산 (범위 지정)
                if (청산)
                {
                    주문수량 = Method.청산주문_매매범위_주문수량계산(잔고, 주문수량, 범위_1);
                }

                // 2. 수익금 손절 로직 (목표 금액에 맞춰 수량 조절)
                if (위치.Contains("수익금손절"))
                {
                    int 단위손절금 = 잔고.현재가 - 잔고.평균단가; // 1주당 손실액 (보통 음수)

                    // 설정된 목표 남길 금액 가져오기
                    double 목표_남길금액 = Get.Cut_남길금액_A;
                    if (검색식 == "수익금손절_B") 목표_남길금액 = Get.Cut_남길금액_B;
                    else if (검색식 == "수익금손절_C") 목표_남길금액 = Get.Cut_남길금액_C;

                    // 현재까지 확정된 손익 + 이번 주문으로 확정될 예상 손익
                    double 현재_예상합계 = Get.실현손익_예상 + (주문수량 * 단위손절금);

                    // 목표 금액보다 부족하고, 현재 손실 중이라면 수량 줄여서 맞춤
                    if (목표_남길금액 > 현재_예상합계 && 단위손절금 < 0)
                    {
                        double 부족한금액 = 목표_남길금액 - Get.실현손익_예상;

                        // [최적화] 반복문 대신 나눗셈으로 한 번에 계산
                        주문수량 = (int)(부족한금액 / 단위손절금);
                        if (주문수량 < 1) 주문수량 = 1;
                    }

                    // 예상 손익 전역 변수에 반영
                    Get.실현손익_예상 += (주문수량 * 단위손절금);
                }

                // 3. 매도 주문 실행 (잔고 보유 수량 확인)
                int 총주문가능수량 = GET.총주문가능수량(잔고);
                if (주문수량 > 0 && 총주문가능수량 >= 주문수량)
                {
                    주문_성공여부 = 최종_주문_실행(잔고, 검색식, 매수매도, 비중, 비중단위, 주문값, 시장가구분, 취소시간, 취소N주문, 반복횟수, 비고, 위치, 수익구분, 기록_주문가격, 주문수량);
                }
            }

            return 주문_성공여부;
        }

        // =================================================================================
        // [외부 함수 1] 실제 주문 객체 생성 및 전송 (한글 함수명)
        // =================================================================================
        private static bool 최종_주문_실행(Stockbalance 잔고, string 검색식, int 매수매도, double 비중, int 비중단위, double 주문값, int 시장가구분, int 취소시간, int 취소N주문, int 반복횟수, string 비고, string 위치, int 수익구분, int 기록_주문가격, int 주문수량)
        {
            // 1. NXT 서버 체크 (설정되어 있는데 체크박스 꺼져있으면 중단)
            if (NXT_server && !GenieConfig.CB_NXT) return false;

            // 2. 검색식 번호 매기기 (중복 방지 및 분할 매도 관리)
            if (수익구분 == 7) // 7번: 분할매도 등 특수 상황
            {
                int 공백위치 = 검색식.IndexOf(' ');
                string 위치_식별자 = (공백위치 == -1) ? 검색식 : 검색식.Substring(0, 공백위치);
                int 현재_최대번호 = -1;

                // [최적화] Dictionary 조회 및 Lock 사용하여 번호 채번
                if (Form1.최종매입가_List.TryGetValue(잔고.종목코드, out List<최종매입가> 매입가_리스트))
                {
                    lock (매입가_리스트)
                    {
                        foreach (var 항목 in 매입가_리스트)
                        {
                            if (항목.위치 == 위치_식별자 && 항목.번호 > 현재_최대번호)
                                현재_최대번호 = 항목.번호;
                        }
                    }
                }

                검색식 = $"{검색식} {현재_최대번호 + 1}";
            }

            // =================================================================================
            // [+] [지니 최적화] lock 구문 추가! (스레드 시간차 공격 원천 차단)
            // 오직 동일한 종목(잔고)에 대해서만 락이 걸리므로 프로그램 속도 저하는 전혀 없습니다.
            // =================================================================================
            lock (잔고)
            {
                // 3. 락 내부에서 중복 체크 후 주문 객체 생성 및 전송
                if (Method.매매진입_가능여부(잔고.종목코드, 검색식))
                {
                    bool 신용주문 = 신용계산.신용주문_해야하나(매수매도, 주문수량, Form1.Market_Item_List[잔고.종목코드], 잔고, out int 실제주문수량);

                    if (실제주문수량 <= 0)
                    {
                        return false;
                    }

                    if (매수매도 == 1)
                    {
                        if (GenieConfig.CB_신용_주문사용 && GenieConfig.CB_신용_가능만매수)
                        {
                            if (!Form1.Market_Item_List[잔고.종목코드].신용가능) return false;
                        }

                        홀딩잔고.예수금업데이트(GET.매수매도str(매수매도), 기록_주문가격, 실제주문수량, "주문", 잔고.종목코드, 신용주문);
                        주문(신용주문, "", 실제주문수량);
                    }
                    else
                    {
                        신용계산.신용주문_분할매도_실행(잔고, 실제주문수량, (is신용, 대출일, 수량) =>
                        {
                            주문(is신용, 대출일, 수량);
                        });
                        금액알림 = true;
                    }

                    // C# 컴파일 에러 방지를 위해 매개변수 이름을 _내부 로 변경했습니다.
                    void 주문(bool 신용주문_내부, string 대출일_내부, int 주문수량_내부)
                    {
                        JumunItem 새주문 = new JumunItem
                        {
                            신용주문 = 신용주문_내부,
                            대출일 = 대출일_내부,
                            Deletetimer = 0,
                            Screennum = GET.JumunScreen(),
                            종목코드 = 잔고.종목코드,
                            종목명 = 잔고.종목명,
                            주문번호 = "+++",
                            원주문번호 = "---",
                            검색식 = 검색식,
                            주문값 = 주문값,
                            시장가구분 = 시장가구분,
                            취소시간 = 취소시간,
                            취소N주문 = 취소N주문,
                            반복횟수 = 반복횟수,
                            비고 = 비고,
                            Pos = 위치,
                            주문수량 = 주문수량_내부,
                            주문가격 = 기록_주문가격,
                            매수매도 = 매수매도,
                            비중 = 비중,
                            비중단위 = 비중단위,
                            취소timer = 취소시간,
                            현재가 = 잔고.현재가,
                            등락률 = 잔고.등락율,
                            주문시간 = Get.TimeNow,
                            미체결량 = 주문수량_내부,
                            주문취소 = true,
                            가동전 = false,
                            Tik_cap = Method.Find_Tik_Cap(잔고.현재가, 기록_주문가격, 잔고.시장),
                            Tik_price = 잔고.현재가,
                            수익률 = 잔고.수익률,
                            주문동기화 = false,
                            감시번호 = 0,
                            Order번호 = GET.Order번호(),
                            수익구분 = 수익구분,
                            NXT = NXT_server,
                            주문시간_Ticks = DateTime.Now.Ticks
                        };

                        Jumun.Add(새주문).Wait(); // 여기서 장부에 완벽히 기록될 때까지 락이 유지됩니다.
                        Que_order(새주문);
                        Tab_AccountManagement.리밸등록("+++", 새주문.Screennum, 새주문.종목코드, 위치, 주문수량_내부, 기록_주문가격, 수익구분);
                    }

                    return true; // 주문 성공
                }
            } // >> 여기서 잔고 락 해제

            return false; // 중복으로 인한 실패
        }

        // [+] 클래스 전역 변수 영역에 아래 타이머 딕셔너리를 추가해 주세요.
        // 다중 스레드 환경에서도 안전하게 시간을 기록하기 위해 ConcurrentDictionary를 사용합니다.
        private static System.Collections.Concurrent.ConcurrentDictionary<string, DateTime> 종목별_에러로그_타이머 = new System.Collections.Concurrent.ConcurrentDictionary<string, DateTime>();

        private static int 예수금확인_및_수량조절(Stockbalance 잔고, Market_Item 종목_데이터, int 주문가격, int 희망수량, string 검색식)
        {
            // 1. 미수 사용 시 (증거금만 있으면 통과)
            if (GenieConfig.CB_misu)
            {
                return 매수증거금 ? 희망수량 : 0;
            }

            long 필요금액 = (long)주문가격 * 희망수량;
            DateTime 현재시간 = DateTime.Now;

            // 2. 단 1주도 살 수 없는 경우 (추정D2 < 주문가격)
            if (Acc.추정D2 < 주문가격)
            {
                if (수량알림)
                {
                    수량알림 = false;
                    if (종목_데이터.매수증거금알림)
                    {
                        종목_데이터.매수증거금알림 = false;

                        // [스마트 로그 제어 1] 1주도 못 사는 완전한 예수금 부족 에러
                        string 에러키_예수금 = 잔고.종목코드 + "_예수금부족";
                        if (!종목별_에러로그_타이머.TryGetValue(에러키_예수금, out DateTime 마지막기록) || (현재시간 - 마지막기록).TotalMinutes >= 3)
                        {
                            Log.에러기록($"[매수 불가] 1주 매수금 부족 종목: {잔고.종목명} 검색식: {검색식} 주문가격: {주문가격} 추정D2: {Acc.추정D2}");
                            종목별_에러로그_타이머[에러키_예수금] = 현재시간; // 기록 시간 갱신
                        }
                    }
                }
                return 0; // 1주도 못 사므로 0 반환
            }

            // 3. 돈이 부족하지만 1주 이상은 살 수 있는 경우 (부분 매수 및 저사양 PC 최적화)
            int 조정된수량 = 희망수량;

            if (Acc.추정D2 < 필요금액)
            {
                // for 루프 없이 현재 남은 돈으로 살 수 있는 최대 수량을 즉시 산출
                조정된수량 = (int)(Acc.추정D2 / 주문가격);

                // 계산된 수량이 원래 희망수량을 넘지 않도록 방어
                조정된수량 = Math.Min(희망수량, 조정된수량);
            }

            // 4. 조정된 수량으로 최대 매수금 제한 체크 후 최종 반환
            int 최종수량 = Method.최대매수금_주문수량계산(잔고, 조정된수량);

            if (최종수량 <= 0)
            {
                // [스마트 로그 제어 2] 최대 매수금 초과 에러
                string 에러키_최대매수금 = 잔고.종목코드 + "_최대매수금초과";
                if (!종목별_에러로그_타이머.TryGetValue(에러키_최대매수금, out DateTime 마지막기록) || (현재시간 - 마지막기록).TotalMinutes >= 3)
                {
                    Log.에러기록($"[매수 제한] 최대 매수금 초과 종목: {잔고.종목명} 검색식: {검색식} 수량: {최종수량}");
                    종목별_에러로그_타이머[에러키_최대매수금] = 현재시간; // 기록 시간 갱신
                }
            }

            수량알림 = true; // 성공 시 알림 리셋 
            return 최종수량;
        }

        public static void Que_order(JumunItem jumun)
        {
            string 주문번호 = "";

            // [로그 추가] 호가맞추기 전/후 비교를 위해 원본 가격 백업
            int 원본주문가격 = jumun.주문가격;
            int 주문가격 = Method.호가맞추기(jumun.주문가격, Market_Item_List[jumun.종목코드].Market);
            int 주문수량 = jumun.주문수량;

          //  if (jumun.매수매도 == 2) Console_print($"=== [Que_order 진입] 종목명: {jumun.종목명} ({jumun.종목코드}) ===");
         //   if (jumun.매수매도 == 2) Console_print($"=== [Que_order 진입]-> 호가맞추기 결과: 원본단가 {원본주문가격}원 -> 보정단가 {주문가격}원");

            try
            {
                if (jumun.매수매도 == 2 || jumun.매수매도 == 20)
                {
                    // [+] ContainsKey 대신 TryGetValue를 사용하여 검사와 추출을 동시에 진행합니다.
                    // 종목코드가 있으면 true를 반환하고 '잔고' 변수에 데이터를 담아줍니다.
                    if (!stockBalanceList.TryGetValue(jumun.종목코드, out Stockbalance 잔고))
                    {
                        // 잔고가 없으면(false) 주문을 삭제하고 종료합니다.
                        // Console_print($"[-] 서버주문전달 삭제 (잔고없음) 종목명: {jumun.종목명} | 코드: {jumun.종목코드} | 매수매도: {jumun.매수매도}");
                        Jumun.Remove(jumun);
                        return;
                    }

                    if (jumun.신용주문)
                    {
                        if (주문수량 > 잔고.신용_보유수량)
                        {
                            주문수량 = 잔고.신용_보유수량;
                            jumun.주문수량 = 주문수량;
                        }
                    }
                    else
                    {
                        if (주문수량 > 잔고.보유수량)
                        {
                            주문수량 = 잔고.보유수량;
                            jumun.주문수량 = 주문수량;
                        }
                    }
                }

                //   XXX주문    1 = XXX매수,  2 = XXX매도,  3 = 취소

                string 거래소 = "KRX";

                // ==========================================================
                // 1. [신규 주문] 현재 시스템 설정에 따른 거래소 세팅
                // ==========================================================
                if (GenieConfig.checkBox_Simulation)
                {
                    거래소 = "KRX"; // 시뮬레이션은 무조건 KRX 고정
                }
                else if (NXT_server)
                {
                    거래소 = "NXT"; // 실거래이고 NXT 옵션이 켜져있으면 NXT
                }

                // ==========================================================
                // 2. [취소/정정 주문] 
                // ==========================================================
                if (jumun.매수매도 == 10 || jumun.매수매도 == 20)
                {
                    주문번호 = jumun.주문번호;
                    주문수량 = 0;
                    주문가격 = 0;

                    // [핵심 방어선] 
                    // 취소/정정은 현재의 NXT_server 설정 상태를 무시하고,
                    // 오직 "원주문이 어디로 나갔었는지"에만 의존하여 덮어씌웁니다.
                    if (jumun.NXT)
                    {
                        거래소 = "NXT";
                    }
                    else
                    {
                        거래소 = "KRX"; // 원주문이 KRX였다면 무조건 KRX로 강제 복구!
                    }
                }

                string dmst_stex_tp = 거래소;              // 국내거래소구분 KRX,NXT,SOR
                string stk_cd = jumun.종목코드;            // 종목코드 
                string ord_qty = 주문수량.ToString();      // 주문수량 
                string ord_uv = 주문가격.ToString();       // 주문단가 
                string trde_tp = "0";                     // 매수매도 0:보통 , 3:시장가 , 6:최유리지정가 , 7:최우선지정가 , 10:보통(IOC) , 13:시장가(IOC) , 16:최유리(IOC) , 20:보통(FOK) , 23:시장가(FOK) , 26:최유리(FOK) , 28:스톱지정가, 29:중간가, 30:중간가(IOC), 31:중간가(FOK)
                string orig_ord_no = 주문번호;             // 원주문번호 
                string crd_deal_tp = "33";                // 33:융자 , 99:융자합
                string crd_loan_dt = jumun.대출일;        // YYYYMMDD(융자일경우필수)

                if (jumun.시장가구분 == 0)//&& 신용주문)
                {
                    ord_uv = "0";
                    trde_tp = "3";
                    if (거래소 == "NXT")
                    {
                        trde_tp = "6";
                    }
                }

                if (jumun.시장가구분 == 1 && jumun.주문값 == 0) // 보통
                {
                    bool 중간가주문 = false;

                    // [최적화 1] ToString() 완전 제거! (저사양 PC 메모리 폭주 방지)
                    DateTime now = DateTime.Now;
                    int 현재시간_숫자 = (now.Hour * 10000) + (now.Minute * 100) + now.Second;

                    // [최적화 2] 문자열 비교 대신 순수 숫자 비교로 0.001초만에 계산 완료
                    if (!NXT_server && (Get.메인마켓시작 + 500 < 현재시간_숫자) && !GenieConfig.checkBox_Simulation)
                    {
                        중간가주문 = true;
                    }

                    if (GenieConfig.CB_중간가주문 && jumun.매수매도 == 2 && 중간가주문)
                    {
                        ord_uv = "0";
                        trde_tp = "29";
                    }

                    if (Form1.FormSpecial_Open)
                    {
                        if (Form_Special.form.CB_수동주문_중간가.Checked && 중간가주문)
                        {
                            ord_uv = "0";
                            trde_tp = "29";
                        }
                    }
                }

                if (server_알림.Contains("마켓") || server_알림.Contains("동시"))
                {
                    if (GenieConfig.checkBox_Simulation)
                    {
                        if (server_알림 == "메인마켓" || server_알림.Contains("동시"))
                        {
                            주문();
                        }
                    }
                    else
                    {
                        주문();
                    }
                }

                void 주문()
                {
                    // =========================================================================
                    // 1. [최종 주문 발송 로그] 주석 처리되었던 부분을 깔끔한 텍스트 기호로 부활
                    // =========================================================================
                    //string orderType = jumun.신용주문 ? "신용" : "현금";
                    //string actionType = "";
                    //if (jumun.매수매도 == 1) actionType = "매수";
                    //else if (jumun.매수매도 == 2) actionType = "매도";
                    //else if (jumun.매수매도 == 10 || jumun.매수매도 == 20) actionType = "취소/정정";

                    //if (jumun.매수매도 == 2) Console_print($"=== [Que_order 진입][+] [최종 주문 서버 발송] {orderType} {actionType}");
                    //if (jumun.매수매도 == 2) Console_print($"=== [Que_order 진입] -> 거래소: {dmst_stex_tp}");
                    //if (jumun.매수매도 == 2) Console_print($"=== [Que_order 진입] -> 수량: {ord_qty}주");

                    //if (jumun.매수매도 == 1 || jumun.매수매도 == 2)
                    //{
                    //    if (jumun.매수매도 == 2) Console_print($"=== [Que_order 진입] -> 단가: {ord_uv}원 (호가구분: {trde_tp})");
                    //}
                    //if (jumun.신용주문 && jumun.매수매도 == 2)
                    //{
                    //    if (jumun.매수매도 == 2) Console_print($"=== [Que_order 진입] -> 신용대출일: {crd_loan_dt}");
                    //}
                    //if (jumun.매수매도 == 10 || jumun.매수매도 == 20)
                    //{
                    //    if (jumun.매수매도 == 2) Console_print($"=== [Que_order 진입] -> 원주문번호: {orig_ord_no}");
                    //}
                    //if (jumun.매수매도 == 2) Console_print("=== [Que_order 진입]---------------------------------------------------------");


                   
                    // =========================================================================
                    // 2. [통합 클래스 연결] TR_통합주문 하나로 모든 요청 처리!
                    // =========================================================================
                    if (jumun.신용주문)
                    {
                        if (jumun.매수매도 == 1)
                            TR_통합주문.신용_매수(jumun, dmst_stex_tp, stk_cd, ord_qty, ord_uv, trde_tp, false);

                        if (jumun.매수매도 == 2)
                            TR_통합주문.신용_매도(jumun, dmst_stex_tp, stk_cd, ord_qty, ord_uv, trde_tp, crd_deal_tp, crd_loan_dt, false);

                        if (jumun.매수매도 == 10 || jumun.매수매도 == 20)
                            TR_통합주문.신용_취소(jumun, dmst_stex_tp, orig_ord_no, stk_cd, false);
                    }
                    else
                    {
                        if (jumun.매수매도 == 1)
                            TR_통합주문.주식_매수(jumun, dmst_stex_tp, stk_cd, ord_qty, ord_uv, trde_tp, false);

                        if (jumun.매수매도 == 2)
                            TR_통합주문.주식_매도(jumun, dmst_stex_tp, stk_cd, ord_qty, ord_uv, trde_tp, false);

                        if (jumun.매수매도 == 10 || jumun.매수매도 == 20)
                            TR_통합주문.주식_취소(jumun, dmst_stex_tp, orig_ord_no, stk_cd, false);
                    }
                }
            }
            catch (Exception ex)
            {
                if (jumun.매수매도 == 2) Console_print($"=== [Que_order 진입][-] Que_order 예외 발생: {ex.Message}");
            }
        }
    }
}
