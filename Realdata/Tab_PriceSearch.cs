using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using 지니64.RESTAPI;

namespace 지니64
{
    class Tab_PriceSearch
    {
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////                 시장가대금 탐색                    /////////////////

        public static void Stock_search_거래대금(string itemcode, string search_현재가, string search_체결시간, string 거래량_구분, string search_누적거래대금)
        {
            // -----------------------------------------------------------------------------------
            // [1단계: 초고속 필터링] 
            // 메모리 할당(Queue, Lambda) 전에 조건이 안 맞으면 즉시 리턴하여 CPU를 아낍니다.
            // -----------------------------------------------------------------------------------

            // 유효성 검사
            if (string.IsNullOrEmpty(itemcode) || string.IsNullOrEmpty(search_현재가)) return;

            // 누적거래대금 파싱 (가장 중요한 필터)
            // 조건: 설정된 누적액보다 작으면 아예 로직을 태우지 않음
            if (!long.TryParse(search_누적거래대금, out long 누적거래대금)) return;
            if (GenieConfig.TB_accumulate_Price > 누적거래대금) return;

            // 현재가 파싱 (절댓값 처리)
            if (!int.TryParse(search_현재가, out int rawPrice)) return;
            int 현재가 = (rawPrice < 0) ? -rawPrice : rawPrice;

            // 거래량 파싱 (문자열 조작 최소화)
            // 거래량_구분: "+100" or "-100"
            bool is매수 = true;
            if (거래량_구분.Length > 0 && 거래량_구분[0] == '-') is매수 = false;

            // TryParse는 부호(+, -)를 자동으로 처리하므로 Substring 없이 바로 파싱
            long.TryParse(거래량_구분, out long 거래량);
            거래량 = (거래량 < 0) ? -거래량 : 거래량;

            // 체결시간 파싱
            if (!int.TryParse(search_체결시간, out int 체결시간)) return;

            // 분 정보 추출 (Substring 없이 수학 연산으로 추출하여 메모리 절약)
            // 예: 090123 -> 뒤에서 4번째, 3번째 글자
            int 분 = 0;
            if (search_체결시간.Length >= 4)
            {
                int len = search_체결시간.Length;
                // ASCII 코드 값('0')을 빼서 숫자로 변환
                분 = (search_체결시간[len - 4] - '0') * 10 + (search_체결시간[len - 3] - '0');
            }

            // -----------------------------------------------------------------------------------
            // [2단계: 큐 진입] 
            // 1단계 필터를 통과한 '유의미한 데이터'만 비동기 큐에 넣습니다.
            // -----------------------------------------------------------------------------------
            UnifiedDataManager.Instance.RealPrice.Enqueue(async () =>
            {
                try
                {
                    // 종목 데이터 가져오기 (없으면 종료)
                    if (!Form1.Market_Item_List.TryGetValue(itemcode, out Market_Item Market)) return;

                    // ---------------------------------------------------------
                    // [시세 데이터 업데이트]
                    // ---------------------------------------------------------
                    if (Market.minute != 분)
                    {
                        Market.minute = 분;
                        Market.S_minute = 현재가;
                    }

                    // 초기값 설정 (0일 때만 세팅)
                    if (Market.Buy_현재가_A == 0) Market.Buy_현재가_A = 현재가;
                    if (Market.Buy_현재가_B == 0) Market.Buy_현재가_B = 현재가;
                    if (Market.Sell_현재가 == 0) Market.Sell_현재가 = 현재가;


                    // =========================================================
                    // [로컬 함수 정의] 반복 로직을 함수화하여 성능 및 가독성 향상
                    // =========================================================

                    bool CheckBong(int bunbong, int ilbong)
                    {
                        // 일봉 조건 (빠른 리턴 적용)
                        if (ilbong == 1 && 현재가 < Market.start_price) return false;
                        if (ilbong == 2 && 현재가 > Market.start_price) return false;
                        if (ilbong == 3 && 현재가 < Market.Last_price) return false;
                        if (ilbong == 4 && 현재가 > Market.Last_price) return false;

                        // 분봉 조건
                        if (bunbong == 1 && 현재가 <= Market.S_minute) return false;
                        if (bunbong == 2 && 현재가 >= Market.S_minute) return false;

                        return true;
                    }

                    bool CheckTick(int upVal, bool upOpt, int mktUp, int downVal, bool downOpt, int mktDown)
                    {
                        bool upResult = upOpt ? (upVal <= mktUp) : (upVal >= mktUp);
                        if (!upResult) return false;
                        return downOpt ? (downVal <= mktDown) : (downVal >= mktDown);
                    }

                    double GetTargetMoney(long p1, double m1, long p2, double m2, long p3, double m3,
                                          long p4, double m4, long p5, double m5, double m6)
                    {
                        if (현재가 <= p5) return m5 * 10000;
                        if (현재가 <= p4) return m4 * 10000;
                        if (현재가 <= p3) return m3 * 10000;
                        if (현재가 <= p2) return m2 * 10000;
                        if (현재가 <= p1) return m1 * 10000;
                        return m6 * 10000;
                    }

                    async Task EnterSearch(int durationSeconds, string searchName, bool isNew)
                    {
                        // Key 생성 (메모리 효율을 위해 필요한 시점에 생성)
                        string key = string.Concat(itemcode, "^", searchName);

                        if (Form1.검색이탈_Dic.TryGetValue(key, out 검색이탈 item))
                        {
                            // 이미 있으면 시간 연장
                            item.ExpireTime = DateTime.Now.AddSeconds(durationSeconds);
                        }
                        else
                        {
                            // 없으면 추가 (스레드 안전)
                            item = new 검색이탈(durationSeconds, key, isNew);
                            Form1.검색이탈_Dic.TryAdd(key, item);
                        }

                        string current_time = Form1.Get.TimeNow.ToString("##:##:##");

                        // 각 탭 로직 호출
                        if (isNew) await Tab_Basic.New_Buy("I", itemcode, searchName);
                        Tab_Watch.Watch_In_Out("I", itemcode, searchName, current_time);
                        Tab_Repeat.Repeat_condition("I", itemcode, searchName);
                        Tab_AccountManagement.Rebalancing_condition("I", itemcode, searchName);
                        Tab_AccountManagement.Liquidation_condition("I", itemcode, searchName);

                        Condition_Management.SearchView_add("I", itemcode, searchName, current_time);
                    }


                    // =========================================================
                    // [A] 매수 탐색 A 로직
                    // =========================================================
                    if (GenieConfig.CB_매수탐색A)
                    {
                        if (CheckBong(GenieConfig.CBB_Buy_A_분봉, GenieConfig.CBB_Buy_A_일봉))
                        {
                            bool resetA = false;
                            // 시간 기준 초기화
                            if (GenieConfig.Combo_Buy_A_초회 == 0)
                            {
                                if (Market.수시간_A + GenieConfig.TB_Buy_A_기준초 < 체결시간) resetA = true;
                            }
                            // 카운트 기준 초기화
                            else if (GenieConfig.Combo_Buy_A_초회 == 1)
                            {
                                if (GenieConfig.TB_Buy_A_기준초 < Form1.Get.매수탐색count_A) resetA = true;
                                Form1.Get.매수탐색count_A++;
                            }

                            if (resetA)
                            {
                                Market.수대금_A = 0;
                                Market.수시간_A = 체결시간;
                                Market.Buy_start_A = 현재가;
                                Market.Buy_상승카운터_A = 0;
                                Market.Buy_하락카운터_A = 0;
                            }

                            // 매수 체결일 때만 카운트 증가
                            if (is매수)
                            {
                                Market.수대금_A += (long)현재가 * 거래량;
                                Market.Buy_End_A = 현재가;

                                if (Market.Buy_현재가_A < 현재가)
                                {
                                    Market.Buy_상승카운터_A++;
                                    if (Market.Buy_하락카운터_A > 0) Market.Buy_하락카운터_A--;
                                }
                                else if (Market.Buy_현재가_A > 현재가)
                                {
                                    if (Market.Buy_상승카운터_A > 0) Market.Buy_상승카운터_A--;
                                    Market.Buy_하락카운터_A++;
                                }
                                Market.Buy_현재가_A = 현재가;
                            }

                            // 목표 대금 계산
                            double targetMoneyA = GetTargetMoney(
                                GenieConfig.TB_Buy_A_탐색주가_1, GenieConfig.TB_Buy_A_탐색대금_1,
                                GenieConfig.TB_Buy_A_탐색주가_2, GenieConfig.TB_Buy_A_탐색대금_2,
                                GenieConfig.TB_Buy_A_탐색주가_3, GenieConfig.TB_Buy_A_탐색대금_3,
                                GenieConfig.TB_Buy_A_탐색주가_4, GenieConfig.TB_Buy_A_탐색대금_4,
                                GenieConfig.TB_Buy_A_탐색주가_5, GenieConfig.TB_Buy_A_탐색대금_5,
                                GenieConfig.TB_Buy_A_탐색대금_6);

                            // 틱 조건 및 대금 조건 확인
                            if (CheckTick(GenieConfig.TB_Buy_상승카운터_A, GenieConfig.CB_Buy_상승옵션_A, Market.Buy_상승카운터_A,
                                          GenieConfig.TB_Buy_하락카운터_A, GenieConfig.CB_Buy_하락옵션_A, Market.Buy_하락카운터_A))
                            {
                                if (Market.수대금_A >= targetMoneyA)
                                {
                                    double msRate = 1 + (GenieConfig.TB_Buy_A_탐색rate / 100.0);

                                    if ((Market.Buy_start_A * msRate) <= Market.Buy_End_A)
                                    {
                                        if (Market.매수가능_A && Market.재매수_A == 0)
                                        {
                                            bool isNew = false;
                                            // 문자열 비교 최적화
                                            if (GenieConfig.CB_new_A && Form1.위치별검색식리스트["신규_A"].이름.Equals("매수탐색_A")) isNew = true;
                                            if (GenieConfig.CB_new_B && Form1.위치별검색식리스트["신규_B"].이름.Equals("매수탐색_A")) isNew = true;
                                            if (GenieConfig.CB_new_C && Form1.위치별검색식리스트["신규_C"].이름.Equals("매수탐색_A")) isNew = true;

                                     await        EnterSearch(GenieConfig.MTB_M_반복, "매수탐색_A", isNew);
                                            Market.재매수_A = GenieConfig.MTB_M_반복;
                                        }
                                    }
                                }
                            }
                        }
                    }


                    // =========================================================
                    // [B] 매수 탐색 B 로직
                    // =========================================================
                    if (GenieConfig.CB_매수탐색B)
                    {
                        if (CheckBong(GenieConfig.CBB_Buy_B_분봉, GenieConfig.CBB_Buy_B_일봉))
                        {
                            bool resetB = false;
                            if (GenieConfig.Combo_Buy_B_초회 == 0)
                            {
                                if (Market.수시간_B + GenieConfig.TB_Buy_B_기준초 < 체결시간) resetB = true;
                            }
                            else if (GenieConfig.Combo_Buy_B_초회 == 1)
                            {
                                if (GenieConfig.TB_Buy_B_기준초 < Form1.Get.매수탐색count_B) resetB = true;
                                Form1.Get.매수탐색count_B++;
                            }

                            if (resetB)
                            {
                                Market.수대금_B = 0;
                                Market.수시간_B = 체결시간;
                                Market.Buy_start_B = 현재가;
                                Market.Buy_상승카운터_B = 0;
                                Market.Buy_하락카운터_B = 0;
                            }

                            if (is매수)
                            {
                                Market.수대금_B += (long)현재가 * 거래량;
                                Market.Buy_End_B = 현재가;

                                if (Market.Buy_현재가_B < 현재가)
                                {
                                    Market.Buy_상승카운터_B++;
                                    if (Market.Buy_하락카운터_B > 0) Market.Buy_하락카운터_B--;
                                }
                                else if (Market.Buy_현재가_B > 현재가)
                                {
                                    if (Market.Buy_상승카운터_B > 0) Market.Buy_상승카운터_B--;
                                    Market.Buy_하락카운터_B++;
                                }
                                Market.Buy_현재가_B = 현재가;
                            }

                            double targetMoneyB = GetTargetMoney(
                                GenieConfig.TB_Buy_B_탐색주가_1, GenieConfig.TB_Buy_B_탐색대금_1,
                                GenieConfig.TB_Buy_B_탐색주가_2, GenieConfig.TB_Buy_B_탐색대금_2,
                                GenieConfig.TB_Buy_B_탐색주가_3, GenieConfig.TB_Buy_B_탐색대금_3,
                                GenieConfig.TB_Buy_B_탐색주가_4, GenieConfig.TB_Buy_B_탐색대금_4,
                                GenieConfig.TB_Buy_B_탐색주가_5, GenieConfig.TB_Buy_B_탐색대금_5,
                                GenieConfig.TB_Buy_B_탐색대금_6);

                            if (CheckTick(GenieConfig.TB_Buy_상승카운터_B, GenieConfig.CB_Buy_상승옵션_B, Market.Buy_상승카운터_B,
                                          GenieConfig.TB_Buy_하락카운터_B, GenieConfig.CB_Buy_하락옵션_B, Market.Buy_하락카운터_B))
                            {
                                if (Market.수대금_B >= targetMoneyB)
                                {
                                    double msRate = 1 + (GenieConfig.TB_Buy_B_탐색rate / 100.0);

                                    if ((Market.Buy_start_B * msRate) <= Market.Buy_End_B)
                                    {
                                        if (Market.매수가능_B && Market.재매수_B == 0)
                                        {
                                            bool isNew = false;
                                            if (GenieConfig.CB_new_A && Form1.위치별검색식리스트["신규_A"].이름.Equals("매수탐색_B")) isNew = true;
                                            if (GenieConfig.CB_new_B && Form1.위치별검색식리스트["신규_B"].이름.Equals("매수탐색_B")) isNew = true;
                                            if (GenieConfig.CB_new_C && Form1.위치별검색식리스트["신규_C"].이름.Equals("매수탐색_B")) isNew = true;

                                       await      EnterSearch(GenieConfig.MTB_M_반복_2, "매수탐색_B", isNew);
                                            Market.재매수_B = GenieConfig.MTB_M_반복_2;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // =========================================================
                    // [C] 매도 탐색 로직
                    // =========================================================
                    if (GenieConfig.CB_매도탐색)
                    {
                        if (Form1.stockBalanceList.ContainsKey(itemcode))
                        {
                            if (CheckBong(GenieConfig.CBB_Sell_탐색_분봉, GenieConfig.CBB_Sell_탐색_일봉))
                            {
                                bool resetSell = false;
                                if (GenieConfig.Combo_Buy_B_초회 == 0)
                                {
                                    if (Market.도시간 + GenieConfig.TB_Sell_기준초 < 체결시간) resetSell = true;
                                }
                                else if (GenieConfig.Combo_Buy_B_초회 == 1)
                                {
                                    if (GenieConfig.TB_Sell_기준초 < Form1.Get.매도탐색count) resetSell = true;
                                    Form1.Get.매도탐색count++;
                                }

                                if (resetSell)
                                {
                                    Market.도대금 = 0;
                                    Market.도시간 = 체결시간;
                                    Market.Sell_start = 현재가;
                                    Market.Sell_상승카운터 = 0;
                                    Market.Sell_하락카운터 = 0;
                                }

                                if (!is매수) // 매도 체결일 때
                                {
                                    Market.도대금 += (long)현재가 * 거래량;
                                    Market.Sell_End = 현재가;

                                    if (Market.Sell_현재가 < 현재가)
                                    {
                                        Market.Sell_상승카운터++;
                                        if (Market.Sell_하락카운터 > 0) Market.Sell_하락카운터--;
                                    }
                                    else if (Market.Sell_현재가 > 현재가)
                                    {
                                        if (Market.Sell_상승카운터 > 0) Market.Sell_상승카운터--;
                                        Market.Sell_하락카운터++;
                                    }
                                    Market.Sell_현재가 = 현재가;
                                }

                                double targetMoneySell = GetTargetMoney(
                                    GenieConfig.TB_Sell_탐색주가_1, GenieConfig.TB_Sell_탐색대금_1,
                                    GenieConfig.TB_Sell_탐색주가_2, GenieConfig.TB_Sell_탐색대금_2,
                                    GenieConfig.TB_Sell_탐색주가_3, GenieConfig.TB_Sell_탐색대금_3,
                                    GenieConfig.TB_Sell_탐색주가_4, GenieConfig.TB_Sell_탐색대금_4,
                                    GenieConfig.TB_Sell_탐색주가_5, GenieConfig.TB_Sell_탐색대금_5,
                                    GenieConfig.TB_Sell_탐색대금_6);

                                if (CheckTick(GenieConfig.TB_Sell_상승카운터, GenieConfig.CB_Sell_상승옵션, Market.Sell_상승카운터,
                                              GenieConfig.TB_Sell_하락카운터, GenieConfig.CB_Sell_하락옵션, Market.Sell_하락카운터))
                                {
                                    if (Market.도대금 >= targetMoneySell)
                                    {
                                        double sellRate = 1 + (GenieConfig.TB_Sell_탐색rate / 100.0);
                                        if ((Market.Sell_start * sellRate) >= Market.Sell_End)
                                        {
                                            // 매도는 신규 여부 false
                                          await  EnterSearch(5, "매도탐색", false);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                catch { } // 리얼 데이터 수신 중 오류 발생 시 루프가 멈추지 않도록 예외 무시
            });
        }


        public static void Stock_search_호가별대금(string itemcode, string 매도호가2, string 매도호가3, string 매도호가4, string 매도호가5, string 매도호가6, string 매도호가7, string 매도호가8, string 매도호가9, string 매도호가10,
                                        string 매수호가2, string 매수호가3, string 매수호가4, string 매수호가5, string 매수호가6, string 매수호가7, string 매수호가8, string 매수호가9, string 매수호가10,
                                        string 매도호가수량2, string 매도호가수량3, string 매도호가수량4, string 매도호가수량5, string 매도호가수량6, string 매도호가수량7, string 매도호가수량8, string 매도호가수량9, string 매도호가수량10,
                                        string 매수호가수량2, string 매수호가수량3, string 매수호가수량4, string 매수호가수량5, string 매수호가수량6, string 매수호가수량7, string 매수호가수량8, string 매수호가수량9, string 매수호가수량10)
        {
            // [1단계: 사전 필터링]
            // 종목이 없거나, 호가 탐색 기능(A, B)이 모두 꺼져있으면 큐에 넣지도 않고 종료
            if (!GenieConfig.CB_매수탐색A && !GenieConfig.CB_매수탐색B) return;

            UnifiedDataManager.Instance.RealHoga.Enqueue(() =>
            {
                if (!Form1.Market_Item_List.TryGetValue(itemcode, out Market_Item Market)) return;

                // -------------------------------------------------------------
                // [2단계: 데이터 파싱 및 전처리]
                // 값을 하나씩 변수에 담지 않고, 배열에 담아 루프 처리가 가능하게 만듭니다.
                // -------------------------------------------------------------

                // 로컬 함수: 가격과 수량을 받아 '거래대금(절댓값)'과 '총합'을 계산
                long ParseAndCalc(string sPrice, string sQty, ref long totalSum)
                {
                    // TryParse는 실패 시 0을 반환하므로 별도 예외처리 불필요
                    int.TryParse(sPrice, out int price);
                    int.TryParse(sQty, out int qty);

                    // 단순 연산 (Math.Abs 호출 비용 절약)
                    long amount = (long)price * qty;
                    if (amount < 0) amount = -amount;

                    totalSum += amount; // 총액 누적
                    return amount;
                }

                long totalSell = 0; // 총 매도 잔량 대금
                long totalBuy = 0;  // 총 매수 잔량 대금

                // 배열로 관리 (인덱스 0~8이 호가 2~10에 대응)
                // 힙 할당 방지를 위해 stackalloc을 쓰면 좋으나 코드가 복잡해지므로 일반 배열 사용
                // (함수 내 짧은 배열은 GC 부하가 적음)
                long[] sellAmounts = new long[9];
                long[] buyAmounts = new long[9];

                // 매도 호가 (2~10) 파싱 및 계산
                sellAmounts[0] = ParseAndCalc(매도호가2, 매도호가수량2, ref totalSell);
                sellAmounts[1] = ParseAndCalc(매도호가3, 매도호가수량3, ref totalSell);
                sellAmounts[2] = ParseAndCalc(매도호가4, 매도호가수량4, ref totalSell);
                sellAmounts[3] = ParseAndCalc(매도호가5, 매도호가수량5, ref totalSell);
                sellAmounts[4] = ParseAndCalc(매도호가6, 매도호가수량6, ref totalSell);
                sellAmounts[5] = ParseAndCalc(매도호가7, 매도호가수량7, ref totalSell);
                sellAmounts[6] = ParseAndCalc(매도호가8, 매도호가수량8, ref totalSell);
                sellAmounts[7] = ParseAndCalc(매도호가9, 매도호가수량9, ref totalSell);
                sellAmounts[8] = ParseAndCalc(매도호가10, 매도호가수량10, ref totalSell);

                // 매수 호가 (2~10) 파싱 및 계산
                buyAmounts[0] = ParseAndCalc(매수호가2, 매수호가수량2, ref totalBuy);
                buyAmounts[1] = ParseAndCalc(매수호가3, 매수호가수량3, ref totalBuy);
                buyAmounts[2] = ParseAndCalc(매수호가4, 매수호가수량4, ref totalBuy);
                buyAmounts[3] = ParseAndCalc(매수호가5, 매수호가수량5, ref totalBuy);
                buyAmounts[4] = ParseAndCalc(매수호가6, 매수호가수량6, ref totalBuy);
                buyAmounts[5] = ParseAndCalc(매수호가7, 매수호가수량7, ref totalBuy);
                buyAmounts[6] = ParseAndCalc(매수호가8, 매수호가수량8, ref totalBuy);
                buyAmounts[7] = ParseAndCalc(매수호가9, 매수호가수량9, ref totalBuy);
                buyAmounts[8] = ParseAndCalc(매수호가10, 매수호가수량10, ref totalBuy);


                // =============================================================
                // [로컬 함수] 매매 가능 여부 판별 (A, B 로직 통합)
                // =============================================================
                bool IsConditionMet(double configSellLimit, double configTotalSell, double configBuyLimit, double configTotalBuy, int ratioMode)
                {
                    // 1. 기준값 변환 (단위: 백만)
                    long limitSellEach = (long)(configSellLimit * 1000000.0);
                    long limitSellTotal = (long)(configTotalSell * 1000000.0);
                    long limitBuyEach = (long)(configBuyLimit * 1000000.0);
                    long limitBuyTotal = (long)(configTotalBuy * 1000000.0);

                    // 2. 총액 검사 (Fail-Fast)
                    if (totalSell < limitSellTotal) return false;
                    if (totalBuy < limitBuyTotal) return false;

                    // 3. 잔량 비율 검사
                    // ratioMode: 0(매도<매수면 탈락), 1(매도>매수면 탈락)
                    if (ratioMode == 0 && totalSell < totalBuy) return false;
                    if (ratioMode == 1 && totalSell > totalBuy) return false;

                    // 4. 개별 호가 검사 (루프로 처리)
                    // 하나라도 기준 미달이면 false
                    for (int i = 0; i < 9; i++)
                    {
                        if (sellAmounts[i] < limitSellEach) return false;
                        if (buyAmounts[i] < limitBuyEach) return false;
                    }

                    return true; // 모든 조건 통과
                }


                // =============================================================
                // [3단계: 조건 검사 및 결과 적용]
                // =============================================================

                // [A] 매수 탐색 A
                if (GenieConfig.CB_매수탐색A)
                {
                    Market.매수가능_A = IsConditionMet(
                        GenieConfig.TB_M_매도호가별대금,
                        GenieConfig.TB_M_매도호가합대금,
                        GenieConfig.TB_M_매수호가별대금,
                        GenieConfig.TB_M_매수호가합대금,
                        GenieConfig.CBB_M_잔량
                    );
                }

                // [B] 매수 탐색 B
                if (GenieConfig.CB_매수탐색B)
                {
                    Market.매수가능_B = IsConditionMet(
                        GenieConfig.TB_M_매도호가별대금_2,
                        GenieConfig.TB_M_매도호가합대금_2,
                        GenieConfig.TB_M_매수호가별대금_2,
                        GenieConfig.TB_M_매수호가합대금_2,
                        GenieConfig.CBB_M_잔량_2
                    );
                }
            });
        }
    }
}
