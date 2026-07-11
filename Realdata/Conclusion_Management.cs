using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using 지니64.box;
using 지니64.RESTAPI;
using static 지니64.초기화;

namespace 지니64
{
    public class Conclusion_Management : Form1
    {

        // 파라미터 마지막에 'bool 신용여부'가 추가되었습니다!
        public static void 잔고업데이트(Stockbalance 잔고, int 현재가, int 업데이트수량, int 주문가능수량, int 평균단가, long 매입금액, long 실현손익, string 신용구분)
        {
            if (신용구분 == "99") return;

            Form1.Acc.실현손익 = 실현손익;
            잔고.현재가 = 현재가;

            // =========================================================================
            // 1. [분리 업데이트] 신용 데이터인지 현물 데이터인지에 따라 각각 장부에 기록
            // =========================================================================
            if (신용구분 == "03")
            {
                // 🌟 [핵심 변경] 신용은 대출일별로 쪼개져 있으므로, 리스트를 싹 돌면서 총합을 구합니다!
                int 총_신용수량 = 0;
                long 총_신용매입금액 = 0;
                long 총_신용이자 = 0;

                if (잔고.신용상세리스트 != null)
                {
                    foreach (var item in 잔고.신용상세리스트)
                    {
                        총_신용수량 += item.보유수량;
                        총_신용매입금액 += item.매입금액;
                        총_신용이자 += item.신용이자; // 기왕 도는 김에 이자 합계도 같이 구합니다!
                    }
                }

                // 구한 총합을 전체 장부에 업데이트!
                잔고.신용_보유수량 = 총_신용수량;
                잔고.신용_매입금액 = 총_신용매입금액;
                잔고.신용_이자합계 = 총_신용이자;

                // 평균단가 = 총 매입금액 / 총 보유수량
                if (총_신용수량 > 0)
                {
                    잔고.신용_평균단가 = (int)(총_신용매입금액 / 총_신용수량);
                }
                // 전량 매도되어 0이 되었을 때는 기존 단가를 유지하거나 0으로 세팅
            }
            else if (신용구분 == "00")
            {
                if (업데이트수량 == 0) 평균단가 = 잔고.평균단가;
                잔고.보유수량 = 업데이트수량;
                잔고.평균단가 = 평균단가;
                잔고.매입금액 = 매입금액;
                잔고.주문가능수량 = 주문가능수량;
            }

            // =========================================================================
            // 2. [통합 계산] 비중, 수수료, 세금은 '현물 + 신용'을 합친 총량으로 계산!
            // =========================================================================
            int 총보유수량 = 잔고.보유수량 + 잔고.신용_보유수량;
            long 총매입금액 = 잔고.매입금액 + 잔고.신용_매입금액;

            // 비중 = (내 총 매입금액 / 매수기준금액) * 100
            잔고.보유비중 = Math.Round((double)총매입금액 / (double)GenieConfig.MT_buying_standard * 100, 2);

            // 매도기준 수량 업데이트 (총 보유수량 기준)
            if (잔고.매도기준update)
            {
                잔고.매도기준 = 총보유수량;
            }
            else
            {
                if (잔고.매도기준 < 총보유수량) 잔고.매도기준 = 총보유수량;
            }

            // 수수료 / 세금 계산 (총 보유수량 기준)
            double tax_ = Form1.TAX;
            if (잔고.시장.Equals("E")) tax_ = 0;

            잔고.수수료 = (int)Math.Round((잔고.현재가 * 총보유수량 * Form1.수수료 / 10) * 10);
            잔고.세금 = (int)Math.Round((잔고.현재가 * 총보유수량 * tax_ / 10) * 10);

            // =========================================================================
            // 3. [전량 매도 체크] 현물과 신용이 둘 다 0이 되어야 진짜 전량매도!
            // =========================================================================
            잔고.전량매도 = (총보유수량 == 0);
        }

        public static void 체결잔고_취소기록(string 종목코드, string 종목명b, int 현재가b, string 매수매도, string 시장가구분, int 취소수량, string 원주문번호)
        {
            if (Form1.JumunItem_List.TryGetValue(원주문번호, out JumunItem jumun))
            {
                GridView_Print.DGV_Jumun(jumun, Get.TimeNow.ToString("##:##:##"), "성공", 종목명b, 현재가b, 매수매도, 시장가구분, jumun.주문가격, 취소수량, jumun.종목코드, jumun.비고, 0, 0, jumun.검색식, 원주문번호, 0, 1, 0);

                재주문(jumun, 종목코드, 종목명b, 취소수량, 매수매도.AsSpan(1).ToString());  //(JumunItem, 원주문번호, 코드b, 종목명b, 취소수량, 화면번호, "재주문", 주문유형b.Substring(1), 주문가격b);

                Tab_AccountManagement.취소_리밸주문(jumun.Screennum, 종목코드, 매수매도, 취소수량);

                if (Form1.form1.CBscalping) Tab_Basic.취소_스켈핑주문(jumun.Screennum, 종목코드);
            }
        }

        private static void 재주문(JumunItem jumun, string 종목코드, string 종목명c, int 주문수량c, string 매수매도)
        {
            Market_Item Marketitem = Form1.Market_Item_List[종목코드];
            string Market = Marketitem.Market;

            if (jumun.매수매도 == 10) jumun.매수매도 = 1;
            if (jumun.매수매도 == 20) jumun.매수매도 = 2;

            if (jumun.비고.Contains("미체결"))
            {
                if (jumun.취소N주문.Equals(0))    //후 취소
                {
                    if (jumun.검색식.Contains("신규_"))
                    {
                        if (jumun.주문수량 == jumun.미체결량)
                        {
                            GenieConfig.신규횟수--;
                            Get.신규횟수--;
                        }
                    }

                    주문삭제();
                }
                else if (jumun.취소N주문 == 1)   //후 시장가
                {
                    if (jumun.반복횟수 > 0)
                    {
                        jumun.반복횟수--;

                        jumun.비고 = "미체결 시장가";

                        jumun.주문번호 = "+++";
                        jumun.주문가격 = jumun.현재가;
                        jumun.Tik_cap = Method.Find_Tik_Cap(jumun.현재가, jumun.현재가, Market);
                        jumun.시장가구분 = 0;
                        jumun.주문값 = 0;

                        ExecuteTrade.Que_order(jumun);// [최적화] + 연산자를 제거하고 문자열 보간($)을 사용하여 가독성과 속도를 모두 잡음
                        Log.동작기록($"[재주문] {종목명c} 을 {GET.시장가구분(jumun.시장가구분)} 주문 가격: 시장가 주문수량: {주문수량c:N0} 으로 재주문합니다.");
                    }
                    else
                    {
                        주문삭제();
                    }
                }
                else if (jumun.취소N주문 == 2)   //후 현재가
                {
                    if (jumun.반복횟수 > 0)
                    {
                        jumun.반복횟수--;

                        jumun.비고 = "미체결 현재가";

                        jumun.주문번호 = "+++";
                        jumun.주문가격 = jumun.현재가;
                        jumun.Tik_cap = Method.Find_Tik_Cap(jumun.현재가, jumun.현재가, Market);
                        jumun.시장가구분 = 1;
                        jumun.주문값 = 0;

                        ExecuteTrade.Que_order(jumun);// [최적화] 함수 호출과 변수를 한데 묶어 메모리 효율을 높임
                        Log.동작기록($"[재주문] {종목명c} 을 {GET.시장가구분(jumun.시장가구분)} 주문 가격: 현재가 주문수량: {주문수량c:N0} 으로 재주문합니다.");
                    }
                    else
                    {
                        주문삭제();
                    }
                }
                else if (jumun.취소N주문 == 3)   //후 재주문
                {
                    if (jumun.반복횟수 > 0)
                    {
                        jumun.반복횟수--;

                        jumun.비고 = "미체결 재주문";

                        jumun.주문번호 = "+++";
                        int 주문가격 = Method.Order_price(jumun.주문값, jumun.시장가구분, Marketitem.종목코드, jumun.현재가);
                        jumun.Tik_cap = Method.Find_Tik_Cap(jumun.현재가, 주문가격, Market);

                        ExecuteTrade.Que_order(jumun);
                        // [최적화] 함수 호출도 { } 안에 쏙 넣고, 숫자에는 :N0을 붙여서 콤마 자동 생성
                        Log.동작기록($"[재주문] {종목명c} 을 {GET.시장가구분(jumun.시장가구분)} 주문 가격: {주문가격:N0} 주문수량: {주문수량c:N0} 으로 재주문합니다.");
                    }
                    else
                    {
                        주문삭제();
                    }
                }
            }
            else
            {
                주문삭제();
            }

            void 주문삭제()
            {
                bool 신용주문 = jumun.신용주문;

                if (!jumun.가동전)
                {
                    홀딩잔고.예수금업데이트(매수매도, jumun.주문가격, jumun.미체결량, "취소", jumun.종목코드, 신용주문);
                }

                if (stockBalanceList.TryGetValue(jumun.종목코드, out var 잔고아이템))
                {
                    홀딩잔고.주문가능수업데이트(잔고아이템, 매수매도, 주문수량c, "취소", 신용주문);
                }

                // [최적화] 미리 변수에 담아 계산 비용과 UI 렌더링 비용 분리
                string 시장구분 = GET.시장가구분(jumun.시장가구분);
                var 주문가격 = Method.Order_price(jumun.주문값, jumun.시장가구분, Marketitem.종목코드, jumun.현재가);

                // [최적화] 문자열 보간($) 사용 -> 내부적으로 String.Format으로 처리되어 메모리 할당 최소화
                Log.동작기록($"[주문취소] {종목명c} 을 {시장구분} 주문 가격: {주문가격} 주문수량: {주문수량c} 주문 취소 합니다.");

                Jumun_remove(jumun);
            }
        }

        public static async Task 체결잔고_접수(bool 신용주문, JumunItem jumun, string Screennum, string 종목코드, string 종목명a, int 현재가a, string 매수매도, string 시장가, int 주문가격a, int 주문수량a, string 주문번호a, int 주문N체결시간, int 미체결량)
        {
            //Console_print("\n[모든주문_접수] ######################");
            //Console_print("[모든주문_접수] 종목 : " + Form1.Market_Item_List[종목코드].종목명);
            //Console_print("[모든주문_접수] 주문번호 : " + 주문번호a);
            //Console_print("[모든주문_접수] Screennum : " + Screennum);
            //Console_print("[모든주문_접수] JumunItem == null : " + (jumun == null));

            if (jumun == null)
            {
                string 검색식 = "키움서버오류";   // HTS 주문으로 매수, 매도      # 주문 아이템에 등록 해주어야 한다.

                if (int.TryParse(Screennum, out int Screen_) && Screen_ >= 1000 && Screen_ <= 1300)
                {
                    검색식 = "API주문";
                }
                else if (!string.IsNullOrEmpty(Screennum)) // 빈 값이 아닐 때만
                {
                    검색식 = "HTS주문";
                }
                else
                {
                    검색식 = "MTS/미확인";
                }

                int OrderN = GET.Order번호();

                if (!매수매도.Contains("취소"))
                {
                    int 매수매도int = 2;
                    if (매수매도.Contains("매수"))
                    {
                        매수매도int = 1;
                        홀딩잔고.예수금업데이트(매수매도, 주문가격a, 주문수량a, "주문", 종목코드, 신용주문);
                    }
                    else
                    {
                        홀딩잔고.주문가능수업데이트(Form1.stockBalanceList[종목코드], "매도", 주문수량a, "매도주문", 신용주문);
                    }
                    string Marketitem = Form1.Market_Item_List[종목코드].Market;
                    int tik_price = 현재가a;
                    int tik_cap = Method.Find_Tik_Cap(tik_price, 주문가격a, Marketitem);
                    double 수익률 = 0;
                    bool 주문취소 = true;

                    int 시장가구분 = 1;
                    if (시장가.Contains("시장가")) 시장가구분 = 0;

                    // [지니 최적화] 긴 생성자 대신 명확한 속성 할당 사용
                    jumun = new JumunItem
                    {
                        신용주문 = 신용주문,
                        Deletetimer = 0,
                        Screennum = GET.JumunScreen(),
                        종목코드 = 종목코드,
                        종목명 = 종목명a,
                        주문번호 = 주문번호a,
                        원주문번호 = 주문번호a,     // 기존 코드에서 6번째 인자도 '주문번호a'였음
                        검색식 = 검색식,
                        주문값 = 0,
                        시장가구분 = 시장가구분,
                        취소시간 = 99999,          // 기존 10번째 인자
                        취소N주문 = 0,
                        반복횟수 = 0,
                        비고 = "",
                        Pos = $"{검색식} 접수",       // 기존 14번째 인자
                        주문수량 = 주문수량a,
                        주문가격 = 주문가격a,
                        매수매도 = 매수매도int,
                        비중 = 0,
                        비중단위 = 0,
                        취소timer = 99999,         // 기존 20번째 인자
                        현재가 = 현재가a,
                        등락률 = Form1.Market_Item_List[종목코드].등락율,
                        주문시간 = 주문N체결시간,
                        미체결량 = 미체결량,
                        주문취소 = 주문취소,       // bool 변수
                        가동전 = false,
                        Tik_cap = tik_cap,
                        Tik_price = tik_price,
                        수익률 = 수익률,
                        주문동기화 = false,
                        감시번호 = 0,
                        Order번호 = OrderN,
                        수익구분 = 0,
                        NXT = NXT_server,
                        주문시간_Ticks = DateTime.Now.Ticks
                    };

                    await Jumun.Add(jumun);

                    GridView_Print.Outstanding_insert(jumun, 0); // 수동주문
                    GridView_Print.DGV_Jumun(jumun, Get.TimeNow.ToString("##:##:##"), "성공", 종목명a, 현재가a, 매수매도, 시장가, 주문가격a, 주문수량a, 종목코드, " ", 99999, 0, 검색식, 주문번호a, 0, 1, 0);
                }

                long 주문금액 = 주문가격a * 주문수량a;
                if (주문가격a == 0)
                    주문금액 = 현재가a * 주문수량a;

                Console_print("[외부주문_접수] JumunItem == null ++++++++++++++ 외부주문 : ");
                Console_print("[외부주문_접수] 검색식 : " + 검색식);
                Console_print("[외부주문_접수] 종목 : " + Form1.Market_Item_List[종목코드].종목명);
                Console_print("[외부주문_접수] 주문번호 : " + 주문번호a);
                Console_print("[외부주문_접수] Screennum : " + Screennum);
                Console_print("[외부주문_접수] 주문취소 : " + jumun.주문취소);
                Console_print("[외부주문_접수] 반복횟수 : " + jumun.반복횟수);
                Console_print("[외부주문_접수] 취소시간 : " + jumun.취소시간);
                Console_print("[외부주문_접수] 취소timer : " + jumun.취소timer);

                string 로그메시지1 = $"주문수량: {주문수량a:N0} 주문금액: {주문금액:N0} {검색식} 접수되었습니다. [Screennum :{Screennum}]";
                string 로그메시지2 = $"[{검색식}알림] 종목명: {종목명a} 주문: {매수매도} 주문가격: {주문가격a:N0}";
                string 알림메시지 = $"종목명: {종목명a} 주문: {매수매도}\n주문가격: {주문가격a:N0} 주문수량: {주문수량a:N0} 주문금액: {주문금액:N0}\n\n{검색식} 접수되었습니다.\n[Screennum :{Screennum}]";

                Log.동작기록("");
                Log.동작기록(로그메시지1);
                Log.동작기록(로그메시지2);
                Log.동작기록("");

                Helper.알림창_멀티($"{검색식}알림", 알림메시지, 5, false);

                SaveToFile.주문리스트_파일저장();

                //if (검색식 == "MTS주문")
                //{
                //    Console_print("MTS주문으로 인한 프로그램 종료 처리 진행...");
                //    Console_print(로그메시지1);
                //    Console_print(로그메시지2);
                //    Environment.Exit(0);
                //}
            }
            else
            {
                if (!매수매도.Contains("취소"))
                {

                    if (jumun.비고.Contains("미체결일괄"))
                    {
                        jumun.취소timer = 0;
                        jumun.취소시간 = 0;
                    }

                    jumun.주문취소 = true;
                    jumun.주문번호 = 주문번호a;
                    jumun.주문가격 = 주문가격a;
                    jumun.주문수량 = 주문수량a;
                    jumun.등락률 = jumun.등락률;
                    jumun.주문시간 = 주문N체결시간;
                    jumun.미체결량 = 미체결량;
                    jumun.취소timer = jumun.취소시간;

                    주문예약 item = Form1.form1.주문예약_List.Find(o => o.스크린번호 == jumun.Screennum && o.종목코드 == 종목코드);
                    if (item != null)
                    {
                        item.주문번호 = 주문번호a;
                    }

                    if (!jumun.원주문번호.Contains("-"))
                    {
                        string targetOrderNum = jumun.원주문번호; // 클로저 캡처를 위해 변수 할당

                        Helper.안전한_UI_업데이트(Form_Outstanding.form, () =>
                        {
                            var grid = Form_Outstanding.form.Outstanding_DataGridView;

                            foreach (DataGridViewRow row in grid.Rows)
                            {
                                if (row.Cells["주문번호"].Value?.ToString() == targetOrderNum)
                                {
                                    row.Cells["주문번호"].Value = 주문번호a;
                                    break;
                                }
                            }
                        });
                    }

                    GridView_Print.DGV_Jumun(jumun, Get.TimeNow.ToString("##:##:##"), "성공", 종목명a, 현재가a, 매수매도, 시장가, 주문가격a, 주문수량a, 종목코드,
                    jumun.비고, jumun.취소시간, jumun.취소N주문, jumun.검색식, 주문번호a, jumun.주문값, jumun.매수매도, jumun.반복횟수);
                }
            }
        }

        public static void 체결잔고_체결(JumunItem jumun, bool 신용여부, string 대출일, string 체_주문번호, string 종목코드, string 체_종목명, int 체_단위체결가,
                                        int 체_체결가, int 체_단위체결량, int 체_체결량, int 체_매매세금, int 체_매매수수료, int 체_주문수량, int 체_주문N체결시간,
                                        string 매수매도, string 체_거래구분, int 체_현재가, int 체_미체결량)
        {
        //    Form1.Console_print($"신용여부 : {신용여부} 매수매도 : {매수매도}");

            Stockbalance 잔고 = null;
            string Screennum = jumun.Screennum;
            double 체_등락률 = 0;
            double 수익률 = 0;
            string 검색식 = jumun.검색식;
            bool 신용주문 = jumun.신용주문;

            if (Form1.stockBalanceList.TryGetValue(종목코드, out Stockbalance 잔고_))
            {
                잔고 = 잔고_;
                체_등락률 = 잔고.등락율;
                수익률 = 잔고.수익률;
            }
            else
            {
                체_등락률 = Form1.Market_Item_List[종목코드].등락율;
            }

            GridView_Print.체결DGV_print(체_주문번호, 종목코드, 체_종목명, 체_체결가, 체_체결량,
                                 체_주문수량, 체_주문N체결시간, 매수매도, 체_거래구분, 체_현재가,
                                 검색식, 체_등락률, 수익률);

            if (체결주문번호_List.ContainsKey(체_주문번호))
            {
                if (jumun.주문번호.Equals(체_주문번호))
                {
                    jumun.미체결량 = 체_미체결량;
                }

                if (잔고 != null)
                {
                    if (매수매도.Contains("+매수"))
                    {
                        잔고.금일매수금 += (체_단위체결가 * 체_단위체결량);
                    }
                    else
                    {
                        잔고.금일매도금 += (체_단위체결량 * 체_단위체결가);

                        long 실현손익 = 0;

                        if (신용여부)
                        {
                            신용상세 찾은결과 = Helper.신용상세_검색(종목코드, 대출일);

                            // [+] 1차 방어: 찾은결과가 null이 아닐 때만 계산 (크래시 방지)
                            if (찾은결과 != null)
                            {
                                long 이번_체결이자 = 0;

                                // [+] 2차 방어 & 논리 적용: 전량 매도 vs 일부 매도 분기 처리
                                if (찾은결과.보유수량 > 0)
                                {
                                    // [일부 매도] 남은 보유수량으로 주당 이자를 계산하여 이번 단위체결량만큼만 부과
                                    double 주당이자 = (double)찾은결과.신용이자 / 찾은결과.보유수량;
                                    이번_체결이자 = (long)Math.Round(주당이자 * 체_단위체결량);
                                }
                                else
                                {
                                    // [전량 매도] 보유수량이 0이라면, 남은 신용이자 '전부'를 이번 체결에서 털어냄
                                    이번_체결이자 = 찾은결과.신용이자;
                                }

                                실현손익 = ((체_단위체결가 - 찾은결과.매입가) * 체_단위체결량) - 체_매매수수료 - 체_매매세금 - 이번_체결이자;
                            }
                            else
                            {
                                // 에러 상황: 신용상세를 못 찾았을 경우 평단가로 대체 계산하거나 로그 출력
                                Form1.Console_print($"[-] >> 에러: {잔고.종목명} 신용상세를 찾을 수 없어 평단가로 임시 계산합니다.");
                                실현손익 = ((체_단위체결가 - 잔고.평균단가) * 체_단위체결량) - 체_매매수수료 - 체_매매세금;
                            }
                        }
                        else
                        {
                            실현손익 = ((체_단위체결가 - 잔고.평균단가) * 체_단위체결량) - 체_매매수수료 - 체_매매세금;
                        }

                        // ==========================================
                        // [+] 3차 방어: 비정상적인 손익 데이터 필터링 및 추적 로그
                        // 실현손익이 비정상적으로 크거나(예: -100억 이하) 작을 경우 데이터 꼬임을 의심합니다.
                        // ==========================================
                        if (실현손익 < -10000000000 || 실현손익 > 10000000000)
                        {
                            Form1.Console_print($"[-] >> 경고: {잔고.종목명} 비정상 실현손익 감지! 값: {실현손익}");
                            Form1.Console_print($"    >> 체결가: {체_단위체결가}, 평단가: {잔고.평균단가}, 수량: {체_단위체결량}");

                            // 비정상 값은 누적시키지 않고 무시하여 잔고 망가짐을 방지합니다. (필요 시 주석 해제)
                            실현손익 = 0;
                        }

                        잔고.금일수익금 += 실현손익;

                        long 누적손익 = 잔고.누적손익;
                        잔고.누적손익 = 누적손익 + 실현손익;
                        잔고.예상손익 = 잔고.평가손익 + 잔고.누적손익;
                    }

                    잔고.세금 += 체_매매세금;
                    잔고.수수료 += 체_매매수수료;
                }

                홀딩잔고.예수금업데이트(매수매도, 체_단위체결가, 체_단위체결량, "체결", 종목코드, 신용주문);

                체결 종목 = Form1.체결기록list.Find(o => o.주문번호.Equals(체_주문번호));
                if (종목 != null)
                {
                    종목.체결량 = 체_체결량.ToString();
                }

                주문예약 예약종목 = Form1.form1.주문예약_List.Find(o => o.주문번호.Equals(체_주문번호));  //예약주문 미체결량 업데이트
                if (예약종목 != null)
                {
                    예약종목.체결수량 += 체_단위체결량;
                }

                Log.동작기록($"[체결알림] '{매수매도.Substring(1)}' {체_종목명} 등락률: {체_등락률} 수익률: {수익률} 체결가: {체_체결가:N0} 체결량: {체_단위체결량:N0} 미체결량: {체_미체결량:N0} 체결금액: {체_체결가 * 체_체결량:N0}");

                체결삭제();
                예약체결(예약종목);

                // =================================================================
                // [지니 최적화] 무거운 Task 껍데기 제거 + break 추가 + 인덱스 0 오타 수정
                // =================================================================
                Helper.안전한_UI_업데이트(Form_Conclusion.form, () =>
                {
                    // 폼이나 그리드가 닫혀있을 때를 대비한 방어 코드
                    if (Form_Conclusion.form != null && Form_Conclusion.form.Conclusion_DataGridView != null)
                    {
                        for (int i = 0; i < Form_Conclusion.form.Conclusion_DataGridView.RowCount; i++)
                        {
                            var cell = Form_Conclusion.form.Conclusion_DataGridView["주문번호_체결", i];

                            // 아까 배운 null 방어 로직! 셀이 비어있으면 건너뜀
                            if (cell != null && cell.Value != null && cell.Value.Equals(체_주문번호))
                            {
                                Form_Conclusion.form.Conclusion_DataGridView["누적체결량_체결", i].Value = 체_체결량;
                                Form_Conclusion.form.Conclusion_DataGridView["체결가_체결", i].Value = 체_체결가;

                                // [버그 수정] 0이 아니라 현재 찾은 줄(i)에 넣어야 함!
                                Form_Conclusion.form.Conclusion_DataGridView["누적금액_체결", i].Value = 체_체결가 * 체_체결량;

                                break; // [최적화] 찾았으니 반복문 즉시 탈출 (CPU 절약)
                            }
                        }
                    }
                });
            }
            else
            {
                if (체_체결량 > 0)
                {
                    체결주문번호_List.TryAdd(체_주문번호, 0);
                    홀딩잔고.예수금업데이트(매수매도, 체_체결가, 체_체결량, "체결", 종목코드, 신용주문);

                    if (잔고 != null)
                    {
                        수익률 = Math.Round(잔고.수익률, 2);
                        long 금일매수금 = 잔고.금일매수금;

                        if (매수매도.Contains("+매수"))
                        {
                            if (!잔고.매수일.Equals(str.today))
                            {
                                잔고.매수일 = str.today;
                            }

                            잔고.금일매수금 = 금일매수금 + (체_체결량 * 체_체결가);
                            잔고.매수횟수++;
                        }
                        else
                        {
                            if (!잔고.매도일.Equals(str.today)) 잔고.매도일 = str.today;

                            잔고.매도횟수++;

                            잔고.금일매도금 += (체_체결량 * 체_체결가);

                            long 실현손익 = 0;
                            if (신용여부)
                            {
                                신용상세 찾은결과 = Helper.신용상세_검색(종목코드, 대출일);

                                // [+] 1차 방어: 찾은결과가 null이 아닐 때만 계산 (크래시 방지)
                                if (찾은결과 != null)
                                {
                                    long 이번_체결이자 = 0;

                                    // [+] 돈나뚝님 논리 적용: 전량 매도 vs 일부 매도 분기 처리
                                    if (찾은결과.보유수량 > 0)
                                    {
                                        // [일부 매도] 남은 보유수량으로 주당 이자를 계산하여 이번 체결량만큼만 부과
                                        double 주당이자 = (double)찾은결과.신용이자 / 찾은결과.보유수량;
                                        이번_체결이자 = (long)Math.Round(주당이자 * 체_체결량);
                                    }
                                    else
                                    {
                                        // [전량 매도] 보유수량이 0이라면, 남은 신용이자 '전부'를 이번 체결에서 털어냄
                                        이번_체결이자 = 찾은결과.신용이자;
                                        // Form1.Console_print($"[+] >> {잔고.종목명} 전량 매도 인식 완료. 잔여 신용이자 일시 청산: {이번_체결이자}원");
                                    }

                                    실현손익 = ((체_체결가 - 찾은결과.매입가) * 체_체결량) - 체_매매수수료 - 체_매매세금 - 이번_체결이자;
                                }
                                else
                                {
                                    // 에러 상황: 신용상세를 못 찾았을 경우 평단가로 임시 계산
                                    Form1.Console_print($"[-] >> 에러: {잔고.종목명} 신용상세를 찾을 수 없어 평단가로 임시 계산합니다.");
                                    실현손익 = ((체_체결가 - 잔고.평균단가) * 체_체결량) - 체_매매수수료 - 체_매매세금;
                                }
                            }
                            else
                            {
                                실현손익 = ((체_체결가 - 잔고.평균단가) * 체_체결량) - 체_매매수수료 - 체_매매세금;
                            }

                            // ==========================================
                            // [+] 3차 방어: 비정상적인 손익 데이터 필터링 및 추적 로그
                            // 실현손익이 비정상적으로 크거나(예: -100억 이하) 작을 경우 데이터 꼬임을 의심합니다.
                            // ==========================================
                            if (실현손익 < -10000000000 || 실현손익 > 10000000000)
                            {
                                Form1.Console_print($"[-] >> 경고: {잔고.종목명} 비정상 실현손익 감지! 값: {실현손익}");
                                Form1.Console_print($"    >> 체결가: {체_단위체결가}, 평단가: {잔고.평균단가}, 수량: {체_단위체결량}");

                                // 비정상 값은 누적시키지 않고 무시하여 잔고 망가짐을 방지합니다. (필요 시 주석 해제)
                                실현손익 = 0;
                            }

                            잔고.금일수익금 += 실현손익;

                            long 누적손익 = 잔고.누적손익;
                            잔고.누적손익 = 누적손익 + 실현손익;
                            잔고.예상손익 = 잔고.평가손익 + 잔고.누적손익;
                        }

                        잔고.세금 += 체_매매세금;
                        잔고.수수료 += 체_매매수수료;

                        jumun.미체결량 = 체_미체결량;
                    }
                    else
                    {
                        if (매수매도.Contains("+매수"))
                        {
                            if (Form1.Market_Item_List.ContainsKey(종목코드))
                            {
                                신규잔고.New_StockBalance(신용여부, 종목코드, 체_종목명, 체_현재가, 체_체결가, 체_체결량, 검색식, 체_매매세금, 체_매매수수료);
                                Form1.NewBuyWrite_List.Add(체_주문번호 + ";" + 종목코드);
                                Get.신규횟수--;
                            }
                            else
                            {
                                Helper.알림창_멀티("매매종목안내", "코스피, 코스닥 종목만 매매가능합니다.", 20, false);
                            }
                        }
                    }

                    체결 add = new 체결(Get.체결갯수.ToString(), 체_주문N체결시간.ToString("##:##:##"), 체_종목명, 검색식, 매수매도.Substring(1), 체_거래구분, 수익률.ToString(), 체_체결가.ToString(), 체_주문수량.ToString(), 체_체결량.ToString(), 체_현재가.ToString(), 체_등락률.ToString(), 체_주문번호, 종목코드);
                    Form1.체결기록list.Add(add);

                    주문예약 예약종목 = Form1.form1.주문예약_List.Find(o => o.주문번호.Equals(체_주문번호));  //예약주문 미체결량 업데이트
                    if (예약종목 != null) 예약종목.체결수량 = 체_체결량;

                    // [최적화] + 연산과 .ToString 호출을 제거하여 CPU 부하와 메모리 낭비를 방지
                    Log.동작기록($"[체결알림] '{(매수매도.Length > 1 ? 매수매도.Substring(1) : 매수매도)}' {체_종목명} 등락률: {체_등락률} 수익률: {수익률} 체결가: {체_체결가:N0} 체결량: {체_체결량:N0} 미체결량: {체_미체결량:N0} 체결금액: {체_체결가 * 체_체결량:N0}");

                    체결삭제();
                    예약체결(예약종목);
                    Get.체결갯수++;
                }
            }

            void 체결삭제()
            {
                // 전량 체결 완료 시 (주문수량 == 누적체결량)
                if (체_주문수량 == 체_체결량)
                {
                    // 1. [캡처] 체결(매도) 직전의 계좌 실현손익을 기억해 둡니다.
                    long 체결직전_계좌실현손익 = Form1.Acc.실현손익;

                    // 2. 만약을 대비한 임시 계산값 (기존 로직 유지)
                    long 매입금 = (long)체_체결가 * 체_체결량;
                    long 임시_이번주문_실현손익 = 0;
                    string 검색식_기억 = 검색식; // Task 내부로 넘겨줄 변수 캡처

                    if (잔고 != null)
                    {
                        if (매수매도.Substring(1).Equals("매수"))
                        {
                            매입금 = 잔고.매입금액 + 잔고.신용_매입금액 + 매입금;
                        }
                        else
                        {
                            매입금 = 잔고.매입금액 + 잔고.신용_매입금액 - 매입금;
                            if (매입금 < 0 || 매입금 < 잔고.현재가) 매입금 = 0;
                            임시_이번주문_실현손익 = (long)((체_체결가 - 잔고.평균단가) * 체_체결량) - 체_매매세금 - 체_매매수수료;
                            if (신용여부)
                            {
                                신용상세 찾은결과 = Helper.신용상세_검색(종목코드, 대출일);
                                if (찾은결과 != null) 임시_이번주문_실현손익 -= 찾은결과.신용이자;
                            }
                        }
                    }


                    // ============================================================
                    // [+] 수정: Equals 대신 Contains를 사용하여 신용/현금 모두 완벽하게 잡아냅니다.
                    // ============================================================
                    bool 텔알림 = (매수매도.Contains("매수") && GenieConfig.CB_매수알림) ||
                                 (매수매도.Contains("매도") && GenieConfig.CB_매도알림);
                    // ============================================================
                    // 3. 핵심: 텔레그램 발송을 백그라운드로 넘기고 0.5초 대기합니다.
                    // ============================================================
                    if (텔알림)
                    {
                        Task.Run(async () =>
                        {
                            // 키움증권 서버에서 '진짜 실현손익' 잔고 데이터가 내려올 때까지 0.5초 대기
                            await Task.Delay(500);

                            // 0.5초 뒤, 방금 서버가 업데이트해 준 완벽한 실현손익을 꺼냅니다.
                            long 확정_계좌실현손익 = Form1.Acc.실현손익;
                            long 확정_이번주문_수익 = 확정_계좌실현손익 - 체결직전_계좌실현손익;

                            // 방어 코드: 0.5초 내에 서버 응답이 없었거나, 수수료만 나가서 차액이 0일 경우 임시값 사용
                            if (확정_이번주문_수익 == 0 && 매수매도.Contains("매도"))
                            {
                                확정_이번주문_수익 = 임시_이번주문_실현손익;
                            }

                            // 최신 잔고 정보 다시 가져오기
                            string 금일수익금 = (잔고 != null) ? 잔고.금일수익금.ToString("N0") : "0";
                            string 누적수익금 = (잔고 != null) ? 잔고.누적손익.ToString("N0") : "0";

                            // 이번수익메시지 역시 Contains로 매도/매도신용 모두 처리
                            string 이번수익메시지 = 매수매도.Contains("매도")
                                                ? $"이번체결 수익: {Method.단위변환(확정_이번주문_수익)} \n"
                                                : "";

                            // [+] 공통 메시지 조립: 신용이든 아니든 똑같이 발송되는 앞부분을 미리 합쳐둡니다.
                            string 기본메시지 = $"[{매수매도.Substring(1)}<{체_종목명}>] : {검색식_기억} \n" +
                                              $"체결가: {체_체결가:N0} " +
                                              $"체결량: {체_체결량:N0} " +
                                              $"체결금: {Method.단위변환(체_체결가 * 체_체결량)} \n" +
                                              $"총매입: {Method.단위변환(매입금)} " +
                                              $"등락률: {체_등락률} " +
                                              $"수익률: {수익률} \n" +
                                              이번수익메시지 +
                                              $"금일수익: {Method.단위변환(double.Parse(금일수익금))} " +
                                              $"누적수익: {Method.단위변환(double.Parse(누적수익금))} \n";


                            // [+] 모의투자가 아니면서(실전투자) && 신용주문을 사용할 때만 상세 출력
                            if (!GenieConfig.checkBox_Simulation && GenieConfig.CB_신용_주문사용)
                            {
                                long 총_현금매입금 = 0;
                                long 총_신용매입금 = 0;
                                long 총_빌린돈 = 0;

                                // 스레드 안전성을 위해 lock 적용
                                lock (Form1.stockBalanceList)
                                {
                                    foreach (var item in Form1.stockBalanceList.Values)
                                    {
                                        long 종목별_신용매입금 = 0;
                                        long 종목별_빌린돈 = 0;

                                        // 1. 신용상세리스트를 순회하여 '신용매입금액'과 '실제 빌린돈(신용금액)' 합산
                                        if (item.신용상세리스트 != null && item.신용상세리스트.Count > 0)
                                        {
                                            foreach (var 상세 in item.신용상세리스트)
                                            {
                                                종목별_신용매입금 += 상세.매입금액;
                                                종목별_빌린돈 += 상세.신용금액;
                                            }
                                        }

                                        // 2. 종목의 전체 매입금액에서 신용으로 산 금액을 빼면 순수 현금매입금이 나옵니다.
                                        long 종목별_현금매입금 = item.매입금액;

                                        총_현금매입금 += 종목별_현금매입금;
                                        총_신용매입금 += 종목별_신용매입금;
                                        총_빌린돈 += 종목별_빌린돈;
                                    }
                                }

                                _ = TelegramMessenger.Telegram_Send(
                                    기본메시지 +
                                    $"<실현손익: {Method.단위변환(확정_계좌실현손익)} " +
                                    $"D+2: {Method.단위변환(Form1.Acc.D2)}>\n" +
                                    $"<총매입: {Method.단위변환(Form1.Acc.매입금)} " +
                                    $"현금매입: {Method.단위변환(총_현금매입금)} " +
                                    $"신용매입: {Method.단위변환(총_신용매입금)} " +
                                    $"대출원금: {Method.단위변환(총_빌린돈)} " +
                                    $"추정자산: {Method.단위변환(Form1.Acc.추정자산)}>"
                                );
                            }
                            else
                            {
                                // [-] 일반 현금 모드 또는 모의투자: 기존의 심플한 포맷 출력
                                _ = TelegramMessenger.Telegram_Send(
                                    기본메시지 +
                                    $"<계좌-실현손익: {Method.단위변환(확정_계좌실현손익)} " +
                                    $"D+2: {Method.단위변환(Form1.Acc.D2)} " +
                                    $"총매입금액: {Method.단위변환(Form1.Acc.매입금)}" +
                                    $"추정자산: {Method.단위변환(Form1.Acc.추정자산)}" +
                                    $">"
                                );
                            }
                        });
                    }

                    Jumun_remove(jumun);

                    감시주문 감시 = Form1.감시주문_List.Values
                                    .FirstOrDefault(o => o.원주문번호.Equals(체_주문번호));

                    if (감시 != null)
                    {
                        DataManagement.감시주문삭제(감시, "주문체결");
                    }
                }
            }

            void 예약체결(주문예약 예약종목)
            {
                if (예약종목 != null)
                {
                    if (예약종목.주문수량 == 예약종목.체결수량)
                    {
                        if (예약종목.체결완료삭제)
                        {
                            // [최적화 1] 알림창용 메시지 (줄바꿈 포함)
                            string 알림메시지 = $"종목명: {예약종목.종목명} 등록일: {예약종목.등록일} 주문가격: {예약종목.주문가:N0} 주문수량: {예약종목.주문수량:N0}\n\n체결수량: {예약종목.체결수량:N0} 매매금액: {예약종목.주문가 * 예약종목.체결수량:N0}";

                            // [최적화 2] 로그 및 콘솔용 메시지 (한 줄로 정리)
                            string 로그메시지 = $"[예약주문 체결삭제] 종목명: {예약종목.종목명} 등록일: {예약종목.등록일} 주문가격: {예약종목.주문가:N0} 주문수량: {예약종목.주문수량:N0} 체결수량: {예약종목.체결수량:N0} 매매금액: {예약종목.주문가 * 예약종목.체결수량:N0}";

                            // 알림창 출력
                            Helper.알림창_멀티("예약주문 체결삭제", 알림메시지, 5, false);

                            // 로그 기록
                            Log.동작기록(" ");
                            Log.동작기록(로그메시지);
                            Log.동작기록(" ");

                            // 콘솔 출력
                            Console_print(로그메시지);

                            Form1.form1.주문예약_List.Remove(예약종목);
                        }
                    }

                    SaveToFile.주문예약_파일저장();
                }
            }

            if (매수매도.Contains("+매수"))
            {
                if (Form1.form1.CBscalping)
                    Tab_Basic.스캘핑주문(종목코드, 체_체결가, 체_단위체결량, 체_미체결량, Screennum);

                Tab_AccountManagement.리밸매도(종목코드, 체_체결가, 체_단위체결량, 체_체결량, 체_주문수량, Screennum);
            }
            else
            {
                if (Form1.form1.CBscalping &&
                   ((GenieConfig.CB_scalping_A && GenieConfig.CB_new_A) || (GenieConfig.CB_scalping_B && GenieConfig.CB_new_B) || (GenieConfig.CB_scalping_C && GenieConfig.CB_new_C)))
                {
                    Tab_Basic.스켈핑차수조정(체_주문번호);
                }
            }
        }

        public static void Jumun_remove(JumunItem jumun)
        {
            if (jumun.미체결량 > 0)
            {
                if (Form1.stockBalanceList.TryGetValue(jumun.종목코드, out Stockbalance 잔고))
                {
                    // [안전장치 1] 검색식이 null일 경우를 대비한 방어
                    if (jumun.검색식 != null && jumun.검색식.Contains("수익금손절"))
                    {
                        int 단위손절금 = 잔고.현재가 - 잔고.평균단가;
                        Get.실현손익_예상 -= (jumun.미체결량 * 단위손절금);
                    }
                }
            }

            try
            {
                // 폼이나 그리드가 닫혀서 null이 된 상태인지 먼저 확인합니다.
                var dgv = Form_Outstanding.form?.Outstanding_DataGridView;

                if (dgv != null && !dgv.IsDisposed)
                {
                    // UI를 조작하는 코드를 하나의 묶음(Action)으로 만듭니다.
                    Action ui안전업데이트 = () =>
                    {
                        try
                        {
                            // [안전장치 2] 열(Column)이 아직 생성되지 않은 찰나의 타이밍 방어
                            if (!dgv.Columns.Contains("주문번호")) return;

                            // [최적화 1] 스레드 충돌 및 인덱스 에러 방지를 위해 전체 행(Rows)을 순회
                            for (int i = 0; i < dgv.Rows.Count; i++)
                            {
                                DataGridViewRow row = dgv.Rows[i];

                                // [최적화 2] 데이터가 없는 빈 줄은 건너뜀
                                if (row.IsNewRow) continue;

                                // [최적화 3] 셀 값이 null인지 먼저 확인하고 안전하게 비교
                                var cellValue = row.Cells["주문번호"].Value?.ToString();

                                if (cellValue == jumun.주문번호)
                                {
                                    row.Cells["주문번호"].Value = "삭제";
                                    break; // 찾았으면 즉시 탈출
                                }
                            }
                        }
                        catch (Exception)
                        {
                            // 내부 UI 업데이트 중 꼬임이 발생해도 프로그램이 죽지 않도록 조용히 넘깁니다.
                        }
                    };

                    // [안전장치 3 핵심] API 쓰레드에서 호출되었다면 UI 쓰레드에게 작업을 위임(BeginInvoke)합니다.
                    // BeginInvoke를 사용하면 백그라운드 연산을 멈추지 않아 저사양 PC에서도 렉이 걸리지 않습니다.
                    if (dgv.InvokeRequired)
                    {
                        dgv.BeginInvoke(ui안전업데이트);
                    }
                    else
                    {
                        ui안전업데이트();
                    }
                }
            }
            catch (Exception ex)
            {
                Form1.Console_print($"[-] Jumun_remove UI 처리 중 에러: {ex.Message}");
            }

            // 그리드 화면 변경(UI)과 상관없이, 실제 내부 데이터 삭제는 즉각 처리합니다.
            Jumun.Remove(jumun);
        }

    }
}
