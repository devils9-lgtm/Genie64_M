using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 지니64
{
    class Misu_liquidation : Form1
    {
        public static async Task 미수정리()
        {
            if (GenieConfig.CB_misu && GenieConfig.Combo_misu > 0)
            {
                // =====================================================================
                // [스마트 스위치] 내아이디 여부에 따라 타겟 금액을 결정합니다!
                // =====================================================================
                bool 내아이디 = true; // 실제 백억아빠의 조건(예: GenieConfig.내아이디)으로 변경해 주세요.

                // 1. 기준 예수금 스위치
                long 타겟_예수금 = 내아이디 ? Form1.Acc.당일예수금 : Form1.Acc.D2;

                // 2. 추정 예수금 스위치 (동적 계산을 위해 로컬 함수로 만듭니다)
                long 타겟_추정예수금()
                {
                    // ※ 주의: Form1.Acc.추정당일예수금 변수를 미리 만들어 두어야 합니다!
                    return 내아이디 ? Form1.Acc.추정당일예수금 : 추정예수금();
                }
                // =====================================================================

                string 미수정리시간 = GenieConfig.MT_misu_time.ToString();

                if (GenieConfig.MT_misu_time < 100000)
                {
                    미수정리시간 = "0" + 미수정리시간;
                }

                DateTime 정리시작 = DateTime.ParseExact(미수정리시간, "HHmmss", null);

                int 추가시간 = (int)(Form1.JumunItem_List.Count / 1.6);
                int 정리시간 = int.Parse(정리시작.AddSeconds(-(추가시간 + 15)).ToString("HHmmss"));

                // [변경] Form1.Acc.추정D2 -> 타겟_추정예수금()
                if (Form1.form1.미수정리_미수알림 && Get.TimeNow > 정리시간 && 타겟_추정예수금() < 0)
                {
                    Form1.form1.미수정리_미수알림 = false;
                    Form1.form1.RB_buy_stop.Checked = true;
                    Form1.미수금정리 = "준비";

                    Log.동작기록(" ");
                    Log.동작기록("[미수금정리] " + Form1.form1.Combo_misu.Text + "기준 미수금을 정리 합니다. 미체결 주문이 일괄 취소 됩니다. 약 ' " + 추가시간 + " ' 초뒤 미수정리를 시작 합니다.");
                    Log.동작기록(" ");

                    Helper.알림창_멀티("미수금정리", $"{Form1.form1.Combo_misu.Text}기준 미수금을 정리 합니다.\n\n미체결 주문이 일괄 취소 됩니다.\n약 ' " + 추가시간 + " ' 초뒤 미수정리를 시작 합니다.", 5, false);

                    Form1.form1.미체결일괄취소();
                    Form1.Delay(1000);
                }

                if (Form1.미수금정리 == "준비")
                {
                    if (Form1.JumunItem_List.Count == 0 && order_scheduler.QueueCount == 0)
                    {
                        Form1.미수금정리 = "시작";
                    }
                }

                int 미수정리_시작 = int.Parse(미수정리시간);
                string sTime = 미수정리_시작.ToString("000000");
                DateTime dt = DateTime.ParseExact(sTime, "HHmmss", null);
                DateTime dtAfter30 = dt.AddMinutes(30);
                int 미수정리_종료 = int.Parse(dtAfter30.ToString("HHmmss"));

                if (미수정리_시작 <= Get.TimeNow && Get.TimeNow <= 미수정리_종료 && Form1.미수금정리 == "시작")
                {
                    // [변경] Form1.Acc.D2 -> 타겟_예수금
                    if (타겟_예수금 > 0)
                    {
                        List<JumunItem> newlist = JumunItem_List.Values.ToList();
                        if (newlist.Count > 0)
                        {
                            for (int i = 0; i < newlist.Count; i++)
                            {
                                JumunItem jumun = newlist[i];

                                jumun.주문취소 = true;
                                jumun.반복횟수 = 0;
                                jumun.취소시간 = 0;
                                jumun.취소timer = 0;
                                jumun.비고 = "미수금정리종료 '취소'";
                            }
                        }
                        Form1.미수금정리 = "종료";
                    }

                    // [변경] Form1.Acc.D2 -> 타겟_예수금
                    if (타겟_예수금 < 0)
                    {
                        // [변경] 타겟에 맞춰 추정값 초기화
                        if (내아이디) Form1.Acc.추정당일예수금 = Form1.Acc.당일예수금;
                        else Form1.Acc.추정D2 = Form1.Acc.D2;

                        // [변경] 추정예수금() -> 타겟_추정예수금()
                        if (!Form1.form1.미수정리_미수알림 && 타겟_추정예수금() < 0 && 타겟_추정예수금() < 0)
                        {
                            double 비중 = GenieConfig.TB_misu_ratio;
                            double 비중_TB_misu_ratio = GenieConfig.TB_misu_ratio;
                            double 주문값 = GenieConfig.TB_misu_value;
                            int 시장가구분 = GenieConfig.Combo_misu_jumnun;
                            int 취소시간 = GenieConfig.TB_misu_repeat_time;
                            bool CB_ETF매입비제외 = GenieConfig.CB_ETF매입비제외;

                            총매도금액_체크(비중);

                            List<JumunItem> 필터링된_리스트 = new List<JumunItem>();

                            foreach (var kvp in Form1.JumunItem_List)
                            {
                                JumunItem item = kvp.Value;
                                if (item.검색식 != "미수금정리")
                                {
                                    필터링된_리스트.Add(item);
                                }
                            }

                            if (필터링된_리스트.Count > 0)
                            {
                                foreach (JumunItem jumun in 필터링된_리스트)
                                {
                                    jumun.반복횟수 = 0;
                                    jumun.취소시간 = 0;
                                    jumun.비고 = "미수금 주문정리 '취소'";

                                    if (jumun.취소timer > GenieConfig.TB_misu_repeat_time)
                                    {
                                        jumun.취소timer = GenieConfig.TB_misu_repeat_time;
                                    }
                                    else
                                    {
                                        if (jumun.주문취소 == false)
                                        {
                                            jumun.주문취소 = true;
                                            if (jumun.매수매도 == 10) jumun.매수매도 = 1;
                                            if (jumun.매수매도 == 20) jumun.매수매도 = 2;
                                        }
                                    }
                                }
                            }

                            Stockbalance 기준잔고1 = null;
                            Stockbalance 기준잔고2 = null;
                            Stockbalance 기준잔고3 = null;

                            if (CB_ETF매입비제외)
                            {
                                var NewstockBalanceList = Form1.stockBalanceList.Values
                                                                .Where(잔고 => 잔고.시장.Equals("E") && 잔고.매입금액 > 0)
                                                                .ToArray();
                                if (NewstockBalanceList.Length > 0)
                                {
                                    시장가구분 = 0;
                                    비중 = Math.Abs(타겟_추정예수금()) * 0.0001;
                                    foreach (var 잔고 in NewstockBalanceList)
                                    {
                                        if (타겟_추정예수금() >= 0) break;
                                        if (미수정리(잔고))
                                        {
                                        await      주문(잔고);
                                        }
                                    }
                                }
                                else
                                {
                                    시장가구분 = GenieConfig.Combo_misu_jumnun;
                                    if (타겟_추정예수금() < 0) await  Run();
                                }
                            }
                            else
                            {
                                if (타겟_추정예수금() < 0)await   Run();
                            }

                            async Task Run()
                            {
                                if (GenieConfig.Combo_misu.Equals(1))
                                {
                                    var NewstockBalanceList = Form1.stockBalanceList.Values
                                                                            .Where(잔고 => 잔고.시장.Equals("E"))
                                                                            .ToArray();
                                    if (NewstockBalanceList.Length > 0)
                                    {
                                        시장가구분 = 0;
                                        비중 = Math.Abs(타겟_추정예수금()) * 0.0001;
                                        foreach (var 잔고 in NewstockBalanceList)
                                        {
                                            if (타겟_추정예수금() >= 0) break;
                                            if (미수정리(잔고)) await 주문(잔고);
                                        }
                                    }
                                }
                                else if (GenieConfig.Combo_misu.Equals(2))
                                {
                                    if (Get.TimeNow <= (미수정리_시작 + 500))
                                    {
                                        var profitableBalances = Form1.stockBalanceList.Values
                                                                                .Where(잔고 => !잔고.종목상태.Contains("거래정지"))
                                                                                .Where(잔고 => 잔고.수익률 > 0.1)
                                                                                .OrderByDescending(잔고 => 잔고.수익률)
                                                                                .ToList();

                                        if (profitableBalances.Count > 0)
                                        {
                                            var totalEvaluationAmount = profitableBalances.Sum(잔고 => 잔고.평가금액);
                                            if (totalEvaluationAmount < Math.Abs(타겟_추정예수금()))
                                            {
                                                foreach (var code in profitableBalances)
                                                {
                                                    비중 = Math.Abs(타겟_추정예수금()) * 0.0001;
                                                    if (타겟_추정예수금() >= 0) break;
                                                    Stockbalance 잔고 = code;

                                                    if (미수정리(잔고)) await 주문(잔고);
                                                }
                                            }
                                            else
                                            {
                                                foreach (var code in profitableBalances)
                                                {
                                                    if (타겟_추정예수금() >= 0) break;
                                                    Stockbalance 잔고 = code;

                                                    if (미수정리(잔고)) await 주문(잔고);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            await 정리();
                                        }
                                    }
                                    else
                                    {
                                        await 정리();
                                    }

                                    async Task 정리()
                                    {
                                        var NewstockBalanceList = Form1.stockBalanceList.Values.ToArray();
                                        foreach (var 잔고 in NewstockBalanceList)
                                        {
                                            if (타겟_추정예수금() >= 0) break;

                                            if (미수정리(잔고))
                                            {
                                                bool orderExists = JumunItem_List.Values
                                                    .Any(o => o.종목코드.Equals(잔고.종목코드));

                                                if (!orderExists) await 주문(잔고);
                                            }
                                        }
                                    }
                                }
                                else if (GenieConfig.Combo_misu.Equals(3))
                                {
                                    var top3profitableBalances = Form1.stockBalanceList.Values
                                        .Where(o => o.전량매도 != true)
                                        .Where(o => o.매매가능 == true)
                                        .OrderByDescending(잔고 => 잔고.수익률)
                                        .ToList();

                                    기준잔고1 = top3profitableBalances.Count > 0 ? top3profitableBalances[0] : null;
                                    기준잔고2 = top3profitableBalances.Count > 1 ? top3profitableBalances[1] : null;
                                    기준잔고3 = top3profitableBalances.Count > 2 ? top3profitableBalances[2] : null;

                                    if (타겟_추정예수금() < 0 && 기준잔고1 != null && 미수정리(기준잔고1)) await 주문(기준잔고1);
                                    if (타겟_추정예수금() < 0 && 기준잔고2 != null && 미수정리(기준잔고2)) await 주문(기준잔고2);
                                    if (타겟_추정예수금() < 0 && 기준잔고3 != null && 미수정리(기준잔고3)) await 주문(기준잔고3);
                                }
                                else if (GenieConfig.Combo_misu.Equals(4))
                                {
                                    var top3LossBalances = Form1.stockBalanceList.Values
                                        .Where(o => o.전량매도 != true)
                                        .Where(o => o.매매가능 == true)
                                        .OrderBy(잔고 => 잔고.수익률)
                                        .ToList();

                                    기준잔고1 = top3LossBalances.Count > 0 ? top3LossBalances[0] : null;
                                    기준잔고2 = top3LossBalances.Count > 1 ? top3LossBalances[1] : null;
                                    기준잔고3 = top3LossBalances.Count > 2 ? top3LossBalances[2] : null;

                                    if (타겟_추정예수금() < 0 && 기준잔고1 != null && 미수정리(기준잔고1)) await 주문(기준잔고1);
                                    if (타겟_추정예수금() < 0 && 기준잔고2 != null && 미수정리(기준잔고2)) await 주문(기준잔고2);
                                    if (타겟_추정예수금() < 0 && 기준잔고3 != null && 미수정리(기준잔고3)) await 주문(기준잔고3);
                                }
                                else if (GenieConfig.Combo_misu.Equals(5))
                                {
                                    // [최적화] 영문 변수명을 직관적인 한글로 변경
                                    var 전체_잔고배열 = Form1.stockBalanceList.Values.ToArray();

                                    foreach (var 잔고 in 전체_잔고배열)
                                    {
                                        if (타겟_추정예수금() >= 0) break;

                                        if (미수정리(잔고))
                                        {
                                            // [지니 최적화] 보조장부 폐지에 따라 메인 장부(JumunItem_List)에서 다이렉트로 검색합니다.
                                            // Any()를 사용하면 조건에 맞는 데이터를 하나라도 찾는 즉시 루프를 탈출하므로 CPU 낭비가 없습니다.
                                            bool 주문존재여부 = Form1.JumunItem_List.Values.Any(개별주문 => 개별주문.종목코드 == 잔고.종목코드);

                                            if (!주문존재여부) await 주문(잔고);

                                        }
                                    }
                                }
                            }

                            async Task 주문(Stockbalance 잔고)
                            {
                                if (잔고 != null)
                                {
                                    if (Form1.재시작) return;

                                    Market_Item Marketitem = Form1.Market_Item_List[잔고.종목코드];
                                    if (Method.매매확인_VI_모투가능확인(Marketitem, 2))
                                    {
                                        int 주문수량 = _주문수량(잔고);
                                        if (Get.TimeNow >= (Get.시장종료 - 1000))
                                        {
                                            취소시간 = 660;
                                            시장가구분 = 0;
                                        }

                                        int 주문가격 = _주문가격(잔고);
                                        int 기록_주문가격 = 주문가격;

                                        if (시장가구분 == 0)
                                        {
                                            주문가격 = 0;
                                            기록_주문가격 = 잔고.현재가;
                                        }

                                        if (주문수량 > 0)
                                        {
                                            if (타겟_추정예수금() < 0)
                                            {
                                                int order수량 = 주문수량;
                                                int Order번호 = GET.Order번호();
                                                string 검색식 = "미수금정리";
                                                int 취소N주문 = 0;
                                                int 매수매도 = 2;

                                                // 미수정리용 매도에서는 현금수량으로만 계산한다
                                                for (int i = 1; i <= 주문수량; i++)
                                                {
                                                    if (Math.Abs(타겟_추정예수금()) < i * 기록_주문가격) 
                                                    {
                                                        order수량 = i;
                                                        if (잔고.주문가능수량 < order수량) order수량 = 잔고.주문가능수량;
                                                        break;
                                                    }
                                                }

                                                홀딩잔고.주문가능수업데이트(잔고, "매도", order수량, "매도주문", false);

                                                JumunItem 새주문 = new JumunItem
                                                {
                                                    신용주문 = false,
                                                    대출일 ="",
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
                                                    반복횟수 = 0,
                                                    비고 = "",
                                                    Pos = 검색식,
                                                    주문수량 = 주문수량,
                                                    주문가격 = 기록_주문가격,
                                                    매수매도 = 매수매도,
                                                    비중 = 비중,
                                                    비중단위 = 0,
                                                    취소timer = 취소시간,
                                                    현재가 = 잔고.현재가,
                                                    등락률 = 잔고.등락율,
                                                    주문시간 = Get.TimeNow,
                                                    미체결량 = 주문수량,
                                                    주문취소 = true,
                                                    가동전 = false,
                                                    Tik_cap = Method.Find_Tik_Cap(잔고.현재가, 주문가격, 잔고.시장),
                                                    Tik_price = 잔고.현재가,
                                                    수익률 = 잔고.수익률,
                                                    주문동기화 = false,
                                                    감시번호 = 0,
                                                    Order번호 = Order번호,
                                                    수익구분 = 0,
                                                    NXT = NXT_server,
                                                    주문시간_Ticks = DateTime.Now.Ticks
                                                };

                                              await  Jumun.Add(새주문);
                                                ExecuteTrade.Que_order(새주문);
                                            }
                                        }
                                    }

                                    int _주문가격(Stockbalance 잔고)
                                    {
                                        int result = Method.Order_price(주문값, 시장가구분, Marketitem.종목코드, 잔고.현재가);
                                        if (시장가구분 == 0) result = 잔고.현재가;
                                        return result;
                                    }

                                    int _주문수량(Stockbalance 잔고)
                                    {
                                        if (Math.Abs(타겟_추정예수금()) < (비중_TB_misu_ratio * 10000 * 2))
                                        {
                                            비중 *= 0.20;
                                        }

                                        int result = 0;
                                        int 주문가격 = _주문가격(잔고);
                                        int 기록_주문가격 = 주문가격;
                                        if (시장가구분 == 0) 기록_주문가격 = 잔고.현재가;

                                        result = (int)Math.Ceiling((double)비중 * (double)10000 / (double)기록_주문가격);

                                        if (-타겟_추정예수금() < 비중 * 10000)
                                        {
                                            result = (int)Math.Ceiling((double)-타겟_추정예수금() / (double)기록_주문가격);
                                        }

                                        if (잔고.주문가능수량 < result) result = 잔고.주문가능수량;

                                        return result;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void 총매도금액_체크(double 비중)
        {
            // --- 1. 매도 미체결 금액 계산 ---

            // 딕셔너리의 값(Value)만 가져와 주문구분 2 또는 4인 항목을 필터링하고 금액을 합산합니다.
            //long 총매도금액 = Form1.JumunItem_List.Values
            //    .Where(o => o.매수매도 == 2 || o.매수매도 == 20)
            //    .Sum(o => (long)o.미체결량 * o.주문가격); // LINQ Sum 사용

            // [지니 최적화] 전체 매도 주문 총액 계산 (LINQ 제거로 메모리/속도 최적화)

            long 총매도금액 = 0;

            // 1. .Values를 호출하면 리스트 복사가 일어나므로, 딕셔너리 원본을 직접 돕니다.
            foreach (var kvp in Form1.JumunItem_List)
            {
                JumunItem o = kvp.Value;

                // 2. 매도(2) 또는 매도관련(20) 주문인지 확인
                if (o.매수매도 == 2 || o.매수매도 == 20)
                {
                    // 3. 미체결량 * 주문가격을 누적 (long 형변환으로 오버플로우 방지)
                    총매도금액 += (long)o.미체결량 * o.주문가격;
                }
            }

            // 결과: 총매도금액 변수에 계산된 값이 담깁니다.

            // --- 2. 매도 비중 조정 (기존 로직 유지) ---

            double 매도비중 = 비중;

            // 조건이 서로 덮어쓰는 구조이므로, 최종 조건을 만족하는 값으로 설정됩니다.
            if (비중 * 10000 < 500000) 매도비중 = 비중 * 1.2;
            if (비중 * 10000 < 200000) 매도비중 = 비중 * 1.4;
            if (비중 * 10000 < 100000) 매도비중 = 비중 * 1.5;

            // --- 3. 일괄 상태 초기화 ---

            // Acc.D2와 총매도금액을 비교하는 조건
            if (Math.Abs(Acc.D2) < 총매도금액 && (비중 * 10000) < 총매도금액)
            {
                // 딕셔너리의 전체 항목을 순회하며 상태를 변경합니다.
                // foreach를 통한 Value 속성 변경은 스레드 안전합니다.
                foreach (JumunItem jumun in Form1.JumunItem_List.Values)
                {
                    // 미체결량이 1을 초과하는 주문만 처리
                    if (jumun.미체결량 > 1) // && (jumun.주문가격 * jumun.미체결량) > (비중 * 10000))
                    {
                        // 주문 속성 일괄 변경 및 초기화
                        jumun.주문취소 = true;
                        jumun.반복횟수 = 0;
                        jumun.취소시간 = 0;
                        jumun.취소timer = 0;
                        jumun.비고 = "미수금 총매도금액_체크 '취소'";
                    }
                }
            }
        }


        //public static long 추정예수금()
        //{
        //    long estimatedCash = Acc.추정D2;

        //    long totalSellAmount = Form1.JumunItem_List.Values
        //        .Where(o => o.매수매도 == 2 || o.매수매도 == 20)
        //        .Sum(item => (long)item.주문수량 * item.주문가격); // 총 매도(출금 예상) 금액 합산

        //    estimatedCash += totalSellAmount;

        //    return estimatedCash;
        //}

        public static long 추정예수금()
        {
            long 추정_예수금 = Acc.추정D2;
            long 총_매도금액 = 0;

            // [지니 최적화] .Values를 쓰지 않고 딕셔너리를 직접 순회합니다.
            // 메모리 복사(GC 부하)를 방지하고 CPU 속도를 가장 빠르게 합니다.
            foreach (var kvp in Form1.JumunItem_List)
            {
                JumunItem item = kvp.Value;

                // 매도(2) 또는 매도관련(20) 주문인지 확인
                if (item.매수매도 == 2 || item.매수매도 == 20)
                {
                    // (주문수량 * 주문가격)을 계산하여 누적
                    // long으로 형변환하여 금액이 커져도 에러 방지
                    총_매도금액 += (long)item.주문수량 * item.주문가격;
                }
            }

            추정_예수금 += 총_매도금액;

            return 추정_예수금;
        }

        public static bool 미수정리(Stockbalance 잔고)
        {
            bool result = true;

            if (잔고.종목상태.Contains("거래정지")) result = false;
            if (result && 잔고.종목상태.Contains("동시호가")) result = false;
            if (result && 잔고.종목상태.Contains("과열(VI)")) result = false;
            if (result && 잔고.종목상태.Contains("하한가")) result = false;
            if (result && !잔고.매매가능) result = false;
            if (result && 잔고.주문가능수량 <= 0) result = false;
            if (result && !GET.익절그룹("미수금정리").Contains(GET.그룹변환(잔고.매매그룹))) result = false;

            return result;
        }



    }
}





