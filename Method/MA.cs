using System;
using System.Collections.Generic;
using System.Linq;

namespace 지니64
{
    class MA
    {
      

        public static void Get_MA()
        {
            foreach (var 잔고 in Form1.stockBalanceList.Values)
            {
                MA.Get_Min_Moving_Average(잔고);
                MA.Get_Day_Moving_Average(잔고);
            }
        }

        public static void Mma_Record(Stockbalance 잔고)
        {
            // [Case 1] 1분 주기 실행 (잔고 == null인 경우)
            // 모든 종목을 순회하며 '새로운 1분 캔들'을 추가하고, 오래된 데이터를 버림
            if (잔고 == null)
            {
                foreach (var 잔고_ in Form1.stockBalanceList.Values)
                {
                    if (string.IsNullOrEmpty(잔고_.분_리스트)) continue;

                    // 1. 현재 데이터 개수 확인 (세미콜론 개수로 파악)
                    // (Split 비용을 아끼기 위해 문자열 탐색)
                    int count = 0;
                    int cutIndex = -1; // 299번째 세미콜론 위치 (300개 남기기 위함)

                    for (int i = 0; i < 잔고_.분_리스트.Length; i++)
                    {
                        if (잔고_.분_리스트[i] == ';')
                        {
                            count++;
                            if (count == 299) // 299번째 구분자를 찾음 (앞에 300개 데이터 유지)
                            {
                                cutIndex = i;
                                break;
                            }
                        }
                    }

                    // 2. 데이터 정리 및 추가
                    if (count >= 299 && cutIndex != -1)
                    {
                        // [최적화] 뒤에 있는 오래된 데이터는 잘라내고(Substring), 앞에 새 데이터를 붙임
                        // 예: "100;99;...;old" -> "현재가;100;99;..."
                        잔고_.분_리스트 = $"{잔고_.현재가};{잔고_.분_리스트.Substring(0, cutIndex)}";
                    }
                    else
                    {
                        // 데이터가 아직 300개 안 찼으면 그냥 앞에 추가
                        잔고_.분_리스트 = $"{잔고_.현재가};{잔고_.분_리스트}";
                    }

                    // 3. 이평선 재계산
                    Get_Min_Moving_Average(잔고_);
                }
            }
            // [Case 2] 실시간 현재가 갱신 (잔고 != null)
            // 맨 앞의 데이터(0번 인덱스)만 현재가로 교체
            else
            {
                if (string.IsNullOrEmpty(잔고.분_리스트)) return;

                // 1. 첫 번째 세미콜론(;) 위치 찾기
                int firstSemiIndex = 잔고.분_리스트.IndexOf(';');

                // 2. 문자열 조립 (Split, For문 제거됨)
                if (firstSemiIndex != -1)
                {
                    // [최적화] 앞부분(옛날 현재가)은 버리고, 새 현재가와 뒷부분(과거 데이터)을 붙임
                    잔고.분_리스트 = $"{잔고.현재가}{잔고.분_리스트.Substring(firstSemiIndex)}";
                }
                else
                {
                    // 데이터가 1개뿐일 때
                    잔고.분_리스트 = 잔고.현재가.ToString();
                }

                // 3. 이평선 재계산
                Get_Min_Moving_Average(잔고);
            }
        }

        public static void Get_Min_Moving_Average(Stockbalance 잔고)
        {
            // 1. 유효성 검사 (데이터 없으면 빠른 종료)
            if (잔고 == null || string.IsNullOrEmpty(잔고.분_리스트)) return;
            if (!Form1.Min_ma_list.TryGetValue(잔고.종목코드, out MAPeriod MAD)) return;

            // 2. [최적화 1단계] 문자열 분리 (Split)
            string[] s_prices = 잔고.분_리스트.Split(';');

            // 3. [최적화 2단계] 미리 double 배열로 변환 (Parsing 비용 제거)
            // 데이터 개수만큼 double 배열을 만듭니다.
            double[] d_prices = new double[s_prices.Length];

            // 한 번만 싹 변환해 둡니다. (여기서만 시간 소요됨)
            for (int i = 0; i < s_prices.Length; i++)
            {
                double.TryParse(s_prices[i], out d_prices[i]);
            }

            // 4. [최적화된 함수] + [로그 출력 기능 추가]
            // debugName을 추가하여 어떤 이평선인지 확인할 수 있게 했습니다.
            double CalculateMa(int period, bool use_cb, bool use_combo, bool In_check, string debugName)
            {
                if (In_check)
                {
                    if (!Form1.감시주문_List.Values.Any(o => o.종목코드 == 잔고.종목코드 && o.TS))
                    {
                        return 0;
                    }
                }

                if (!use_cb || !use_combo) return 0;
                if (period <= 0) return 0;

                double sum = 0;
                int count = 0;
                int limit = Math.Min(d_prices.Length, period);

                // 숫자 배열을 바로 더함 (CPU가 가장 좋아하는 연산)
                for (int i = 0; i < limit; i++)
                {
                    sum += d_prices[i];
                    count++;
                }

                double result = (count > 0) ? sum / count : 0;

                // =========================================================
                // [★ 로그 출력] 원하시는 계산 로그입니다.
                // 너무 많이 출력되면 주석 처리 하세요.
                // =========================================================
                // 예: [이평계산] 005930 | repeat_MAValue1_A (기간:5) -> 72500
                bool showLog = false; // false로 바꾸면 로그 안 나옴

                if (showLog)
                {
                  Form1.Console_print($"[이평계산] {잔고.종목명} | {debugName} (기간:{period}) -> {result:N0} (현재가:{잔고.현재가:N0})");
                }

                return result;
            }

            // -----------------------------------------------------------
            // 5. 값 계산 및 할당 (변수명을 같이 넘겨주어 로그 확인이 쉽습니다)
            // -----------------------------------------------------------

            //체크박스 사용인지 확인
            // [반복 매매 1차]
            MAD.Repeat_MAValue1_A = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_A, GenieConfig.CB_repeat_use_A, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_A");
            MAD.Repeat_MAValue1_B = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_B, GenieConfig.CB_repeat_use_B, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_B");
            MAD.Repeat_MAValue1_C = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_C, GenieConfig.CB_repeat_use_C, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_C");
            MAD.Repeat_MAValue1_D = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_D, GenieConfig.CB_repeat_use_D, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_D");
            MAD.Repeat_MAValue1_E = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_E, GenieConfig.CB_repeat_use_E, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_E");
            MAD.Repeat_MAValue1_F = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_F, GenieConfig.CB_repeat_use_F, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_F");
            MAD.Repeat_MAValue1_G = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_G, GenieConfig.CB_repeat_use_G, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_G");
            MAD.Repeat_MAValue1_H = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_H, GenieConfig.CB_repeat_use_H, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_H");
            MAD.Repeat_MAValue1_I = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_I, GenieConfig.CB_repeat_use_I, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_I");
            MAD.Repeat_MAValue1_J = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_J, GenieConfig.CB_repeat_use_J, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_J");
            MAD.Repeat_MAValue1_K = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_K, GenieConfig.CB_repeat_use_K, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_K");
            MAD.Repeat_MAValue1_L = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_L, GenieConfig.CB_repeat_use_L, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_L");
            MAD.Repeat_MAValue1_M = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_M, GenieConfig.CB_repeat_use_M, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_M");
            MAD.Repeat_MAValue1_N = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod1_N, GenieConfig.CB_repeat_use_N, GenieConfig.CBB_repeat_MinMAPeriod1_A > 0, false, "Min repeat_MAValue1_N");

            // [반복 매매 2차]
            MAD.Repeat_MAValue2_A = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_A, GenieConfig.CB_repeat_use_A, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_A");
            MAD.Repeat_MAValue2_B = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_B, GenieConfig.CB_repeat_use_B, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_B");
            MAD.Repeat_MAValue2_C = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_C, GenieConfig.CB_repeat_use_C, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_C");
            MAD.Repeat_MAValue2_D = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_D, GenieConfig.CB_repeat_use_D, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_D");
            MAD.Repeat_MAValue2_E = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_E, GenieConfig.CB_repeat_use_E, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_E");
            MAD.Repeat_MAValue2_F = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_F, GenieConfig.CB_repeat_use_F, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_F");
            MAD.Repeat_MAValue2_G = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_G, GenieConfig.CB_repeat_use_G, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_G");
            MAD.Repeat_MAValue2_H = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_H, GenieConfig.CB_repeat_use_H, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_H");
            MAD.Repeat_MAValue2_I = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_I, GenieConfig.CB_repeat_use_I, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_I");
            MAD.Repeat_MAValue2_J = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_J, GenieConfig.CB_repeat_use_J, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_J");
            MAD.Repeat_MAValue2_K = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_K, GenieConfig.CB_repeat_use_K, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_K");
            MAD.Repeat_MAValue2_L = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_L, GenieConfig.CB_repeat_use_L, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_L");
            MAD.Repeat_MAValue2_M = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_M, GenieConfig.CB_repeat_use_M, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_M");
            MAD.Repeat_MAValue2_N = CalculateMa(GenieConfig.TB_repeat_MinMAPeriod2_N, GenieConfig.CB_repeat_use_N, GenieConfig.CBB_repeat_MinMAPeriod2_A > 0, false, "Min repeat_MAValue2_N");

            // [리밸런싱 1차]
            MAD.Rebalance_MAValue1_A = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod1_A, GenieConfig.CB_rebalance_A, GenieConfig.CBB_rebalance_MinMAPeriod1_A > 0, false, "Min Rebalance_MAValue1_A");
            MAD.Rebalance_MAValue1_B = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod1_B, GenieConfig.CB_rebalance_B, GenieConfig.CBB_rebalance_MinMAPeriod1_B > 0, false, "Min Rebalance_MAValue1_B");
            MAD.Rebalance_MAValue1_C = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod1_C, GenieConfig.CB_rebalance_C, GenieConfig.CBB_rebalance_MinMAPeriod1_C > 0, false, "Min Rebalance_MAValue1_C");
            MAD.Rebalance_MAValue1_D = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod1_D, GenieConfig.CB_rebalance_D, GenieConfig.CBB_rebalance_MinMAPeriod1_D > 0, false, "Min Rebalance_MAValue1_D");
            MAD.Rebalance_MAValue1_E = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod1_E, GenieConfig.CB_rebalance_E, GenieConfig.CBB_rebalance_MinMAPeriod1_E > 0, false, "Min Rebalance_MAValue1_E");
            MAD.Rebalance_MAValue1_F = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod1_F, GenieConfig.CB_rebalance_F, GenieConfig.CBB_rebalance_MinMAPeriod1_F > 0, false, "Min Rebalance_MAValue1_F");
            MAD.Rebalance_MAValue1_G = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod1_G, GenieConfig.CB_rebalance_G, GenieConfig.CBB_rebalance_MinMAPeriod1_G > 0, false, "Min Rebalance_MAValue1_G");

            // [리밸런싱 2차]
            MAD.Rebalance_MAValue2_A = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod2_A, GenieConfig.CB_rebalance_A, GenieConfig.CBB_rebalance_MinMAPeriod2_A > 0, false, "Min Rebalance_MAValue2_A");
            MAD.Rebalance_MAValue2_B = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod2_B, GenieConfig.CB_rebalance_B, GenieConfig.CBB_rebalance_MinMAPeriod2_B > 0, false, "Min Rebalance_MAValue2_B");
            MAD.Rebalance_MAValue2_C = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod2_C, GenieConfig.CB_rebalance_C, GenieConfig.CBB_rebalance_MinMAPeriod2_C > 0, false, "Min Rebalance_MAValue2_C");
            MAD.Rebalance_MAValue2_D = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod2_D, GenieConfig.CB_rebalance_D, GenieConfig.CBB_rebalance_MinMAPeriod2_D > 0, false, "Min Rebalance_MAValue2_D");
            MAD.Rebalance_MAValue2_E = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod2_E, GenieConfig.CB_rebalance_E, GenieConfig.CBB_rebalance_MinMAPeriod2_E > 0, false, "Min Rebalance_MAValue2_E");
            MAD.Rebalance_MAValue2_F = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod2_F, GenieConfig.CB_rebalance_F, GenieConfig.CBB_rebalance_MinMAPeriod2_F > 0, false, "Min Rebalance_MAValue2_F");
            MAD.Rebalance_MAValue2_G = CalculateMa(GenieConfig.TB_rebalance_MinMAPeriod2_G, GenieConfig.CB_rebalance_G, GenieConfig.CBB_rebalance_MinMAPeriod2_G > 0, false, "Min Rebalance_MAValue2_G");

            // [청산]
            MAD.Liquidation_MAValue_A = CalculateMa(GenieConfig.TB_Liquidation_MinMAPeriod_A, GenieConfig.CB_Liquidation_A, GenieConfig.CBB_Liquidation_MinMAPeriod_A > 0, false, "Min Liquidation_MAValue_A");
            MAD.Liquidation_MAValue_B = CalculateMa(GenieConfig.TB_Liquidation_MinMAPeriod_B, GenieConfig.CB_Liquidation_B, GenieConfig.CBB_Liquidation_MinMAPeriod_B > 0, false, "Min Liquidation_MAValue_B");
            MAD.Liquidation_MAValue_C = CalculateMa(GenieConfig.TB_Liquidation_MinMAPeriod_C, GenieConfig.CB_Liquidation_C, GenieConfig.CBB_Liquidation_MinMAPeriod_C > 0, false, "Min Liquidation_MAValue_C");

            // [청산 TS]
            MAD.Liquidation_TS_MAValue_A = CalculateMa(GenieConfig.TB_Liquidation_TS_MinMAPeriod_A, GenieConfig.CB_Liquidation_A, GenieConfig.CBB_Liquidation_TS_MinMAPeriod_A > 0, false, "Min Liquidation_TS_MAValue_A");
            MAD.Liquidation_TS_MAValue_B = CalculateMa(GenieConfig.TB_Liquidation_TS_MinMAPeriod_B, GenieConfig.CB_Liquidation_B, GenieConfig.CBB_Liquidation_TS_MinMAPeriod_B > 0, false, "Min Liquidation_TS_MAValue_B");
            MAD.Liquidation_TS_MAValue_C = CalculateMa(GenieConfig.TB_Liquidation_TS_MinMAPeriod_C, GenieConfig.CB_Liquidation_C, GenieConfig.CBB_Liquidation_TS_MinMAPeriod_C > 0, false, "Min Liquidation_TS_MAValue_C");

            // [매매기간]
            MAD.매매기간_TS_MAValue_A = CalculateMa(GenieConfig.TB_매매기간_TS_MinMAPeriod_A, GenieConfig.CBB_매매기간_trading_A > 0, GenieConfig.CBB_매매기간_TS_MinMAPeriod_A > 0, false, "Min 매매기간_TS_MAValue_A");
            MAD.매매기간_TS_MAValue_B = CalculateMa(GenieConfig.TB_매매기간_TS_MinMAPeriod_B, GenieConfig.CBB_매매기간_trading_B > 0, GenieConfig.CBB_매매기간_TS_MinMAPeriod_B > 0, false, "Min 매매기간_TS_MAValue_B");
            MAD.매매기간_TS_MAValue_C = CalculateMa(GenieConfig.TB_매매기간_TS_MinMAPeriod_C, GenieConfig.CBB_매매기간_trading_C > 0, GenieConfig.CBB_매매기간_TS_MinMAPeriod_C > 0, false, "Min 매매기간_TS_MAValue_C");
            MAD.매매기간_TS_MAValue_D = CalculateMa(GenieConfig.TB_매매기간_TS_MinMAPeriod_D, GenieConfig.CBB_매매기간_trading_D > 0, GenieConfig.CBB_매매기간_TS_MinMAPeriod_D > 0, false, "Min 매매기간_TS_MAValue_D");
            MAD.매매기간_TS_MAValue_E = CalculateMa(GenieConfig.TB_매매기간_TS_MinMAPeriod_E, GenieConfig.CBB_매매기간_trading_E > 0, GenieConfig.CBB_매매기간_TS_MinMAPeriod_E > 0, false, "Min 매매기간_TS_MAValue_E");
            MAD.매매기간_TS_MAValue_F = CalculateMa(GenieConfig.TB_매매기간_TS_MinMAPeriod_F, GenieConfig.CBB_매매기간_trading_F > 0, GenieConfig.CBB_매매기간_TS_MinMAPeriod_F > 0, false, "Min 매매기간_TS_MAValue_F");

            //감시주문이 있는지 먼저 확인후 실행
            // [리밸런싱 TS 1차]
            MAD.Rebalance_TS_MAValue_1차_A = CalculateMa(GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_A, true, true, true, "Min Rebalance_TS_MAValue_1차_A");
            MAD.Rebalance_TS_MAValue_1차_B = CalculateMa(GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_B, true, true, true, "Min Rebalance_TS_MAValue_1차_B");
            MAD.Rebalance_TS_MAValue_1차_C = CalculateMa(GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_C, true, true, true, "Min Rebalance_TS_MAValue_1차_C");
            MAD.Rebalance_TS_MAValue_1차_D = CalculateMa(GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_D, true, true, true, "Min Rebalance_TS_MAValue_1차_D");
            MAD.Rebalance_TS_MAValue_1차_E = CalculateMa(GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_E, true, true, true, "Min Rebalance_TS_MAValue_1차_E");
            MAD.Rebalance_TS_MAValue_1차_F = CalculateMa(GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_F, true, true, true, "Min Rebalance_TS_MAValue_1차_F");
            MAD.Rebalance_TS_MAValue_1차_G = CalculateMa(GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_G, true, true, true, "Min Rebalance_TS_MAValue_1차_G");

            // [리밸런싱 TS 2차]
            MAD.Rebalance_TS_MAValue_2차_A = CalculateMa(GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_A, true, true, true, "Min Rebalance_TS_MAValue_2차_A");
            MAD.Rebalance_TS_MAValue_2차_B = CalculateMa(GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_B, true, true, true, "Min Rebalance_TS_MAValue_2차_B");
            MAD.Rebalance_TS_MAValue_2차_C = CalculateMa(GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_C, true, true, true, "Min Rebalance_TS_MAValue_2차_C");
            MAD.Rebalance_TS_MAValue_2차_D = CalculateMa(GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_D, true, true, true, "Min Rebalance_TS_MAValue_2차_D");
            MAD.Rebalance_TS_MAValue_2차_E = CalculateMa(GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_E, true, true, true, "Min Rebalance_TS_MAValue_2차_E");
            MAD.Rebalance_TS_MAValue_2차_F = CalculateMa(GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_F, true, true, true, "Min Rebalance_TS_MAValue_2차_F");
            MAD.Rebalance_TS_MAValue_2차_G = CalculateMa(GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_G, true, true, true, "Min Rebalance_TS_MAValue_2차_G");
        }

      
        public static void Dma_Record(Stockbalance 잔고)
        {
            // 1. 데이터 유효성 검사
            if (잔고 == null || string.IsNullOrEmpty(잔고.일_리스트)) return;

            // 2. 첫 번째 세미콜론(;)의 위치를 찾습니다.
            // 예: "1000;900;800;..." 에서 첫 번째 ;의 위치
            int firstSemiIndex = 잔고.일_리스트.IndexOf(';');

            if (firstSemiIndex != -1)
            {
                // [핵심 로직]
                // 앞부분(옛날 현재가)은 버리고, 새 현재가와 뒷부분(과거 데이터)을 붙입니다.
                // Substring(firstSemiIndex)는 세미콜론을 포함한 뒷부분을 가져옵니다. (;900;800...)
                잔고.일_리스트 = $"{잔고.현재가}{잔고.일_리스트.Substring(firstSemiIndex)}";
            }
            else
            {
                // 데이터가 1개밖에 없을 경우 (세미콜론이 없음) -> 그냥 통째로 바꿈
                잔고.일_리스트 = 잔고.현재가.ToString();
            }

            // 3. 이평선 재계산 호출
            Get_Day_Moving_Average(잔고);
        }

      

        public static void Get_Day_Moving_Average(Stockbalance 잔고)
        {
            // 1. 유효성 검사
            if (잔고 == null || string.IsNullOrEmpty(잔고.일_리스트)) return;

            // Day_ma_list에서 해당 종목의 설정(MAD)을 가져옵니다.
            // (GET.Code는 사용자님 환경에 맞게 사용하세요)
            if (!Form1.Day_ma_list.TryGetValue(잔고.종목코드, out MAPeriod MAD)) return;

            // 2. [최적화 1단계] 문자열 분리 (1회 수행)
            string[] s_prices = 잔고.일_리스트.Split(';');

            // 3. [최적화 2단계] Double 배열 변환 (1회 수행)
            double[] d_prices = new double[s_prices.Length];
            for (int i = 0; i < s_prices.Length; i++)
            {
                double.TryParse(s_prices[i], out d_prices[i]);
            }

            // 4. [내부 함수] 이평선 계산 로직 (최적화됨)
            double CalculateMa(int period, bool use_cb, bool use_combo, string debugName)
            {
                if (!use_cb || !use_combo) return 0;
                if (period <= 0) return 0;

                double sum = 0;
                int count = 0;

                // 데이터 개수와 설정된 기간 중 작은 값만큼만 반복
                int limit = Math.Min(d_prices.Length, period);

                for (int i = 0; i < limit; i++)
                {
                    sum += d_prices[i];
                    count++;
                }

                double result = (count > 0) ? sum / count : 0;

                // [로그 출력] (필요 시 false로 변경)
                bool showLog = true;
                if (showLog)
                {
                    // 일봉 로그는 너무 많을 수 있으니 필요할 때만 켜세요.
                    // Form1.Console_print($"[일봉이평] {잔고.종목명} | {debugName} (기간:{period}) -> {result:N0}");
                }

                return result;
            }

            // -----------------------------------------------------------
            // 5. 값 계산 및 할당
            // (GenieConfig의 TB(기간), CBB(사용여부) 변수명은 사용자님 환경에 맞게 확인해주세요)
            // -----------------------------------------------------------

            // [반복 매매 1차]
            MAD.Repeat_MAValue1_A = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_A, GenieConfig.CB_repeat_use_A, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_A");
            MAD.Repeat_MAValue1_B = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_B, GenieConfig.CB_repeat_use_B, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_B");
            MAD.Repeat_MAValue1_C = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_C, GenieConfig.CB_repeat_use_C, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_C");
            MAD.Repeat_MAValue1_D = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_D, GenieConfig.CB_repeat_use_D, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_D");
            MAD.Repeat_MAValue1_E = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_E, GenieConfig.CB_repeat_use_E, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_E");
            MAD.Repeat_MAValue1_F = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_F, GenieConfig.CB_repeat_use_F, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_F");
            MAD.Repeat_MAValue1_G = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_G, GenieConfig.CB_repeat_use_G, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_G");
            MAD.Repeat_MAValue1_H = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_H, GenieConfig.CB_repeat_use_H, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_H");
            MAD.Repeat_MAValue1_I = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_I, GenieConfig.CB_repeat_use_I, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_I");
            MAD.Repeat_MAValue1_J = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_J, GenieConfig.CB_repeat_use_J, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_J");
            MAD.Repeat_MAValue1_K = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_K, GenieConfig.CB_repeat_use_K, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_K");
            MAD.Repeat_MAValue1_L = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_L, GenieConfig.CB_repeat_use_L, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_L");
            MAD.Repeat_MAValue1_M = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_M, GenieConfig.CB_repeat_use_M, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_M");
            MAD.Repeat_MAValue1_N = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod1_N, GenieConfig.CB_repeat_use_N, GenieConfig.CBB_repeat_DayMAPeriod1_A > 0, "Day repeat_MAValue1_N");

            // [반복 매매 2차Day ]
            MAD.Repeat_MAValue2_A = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_A, GenieConfig.CB_repeat_use_A, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_A");
            MAD.Repeat_MAValue2_B = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_B, GenieConfig.CB_repeat_use_B, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_B");
            MAD.Repeat_MAValue2_C = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_C, GenieConfig.CB_repeat_use_C, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_C");
            MAD.Repeat_MAValue2_D = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_D, GenieConfig.CB_repeat_use_D, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_D");
            MAD.Repeat_MAValue2_E = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_E, GenieConfig.CB_repeat_use_E, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_E");
            MAD.Repeat_MAValue2_F = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_F, GenieConfig.CB_repeat_use_F, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_F");
            MAD.Repeat_MAValue2_G = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_G, GenieConfig.CB_repeat_use_G, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_G");
            MAD.Repeat_MAValue2_H = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_H, GenieConfig.CB_repeat_use_H, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_H");
            MAD.Repeat_MAValue2_I = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_I, GenieConfig.CB_repeat_use_I, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_I");
            MAD.Repeat_MAValue2_J = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_J, GenieConfig.CB_repeat_use_J, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_J");
            MAD.Repeat_MAValue2_K = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_K, GenieConfig.CB_repeat_use_K, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_K");
            MAD.Repeat_MAValue2_L = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_L, GenieConfig.CB_repeat_use_L, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_L");
            MAD.Repeat_MAValue2_M = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_M, GenieConfig.CB_repeat_use_M, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_M");
            MAD.Repeat_MAValue2_N = CalculateMa(GenieConfig.TB_repeat_DayMAPeriod2_N, GenieConfig.CB_repeat_use_N, GenieConfig.CBB_repeat_DayMAPeriod2_A > 0, "Day repeat_MAValue2_N");

            // [리밸런싱 1차]
            MAD.Rebalance_MAValue1_A = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod1_A, GenieConfig.CB_rebalance_A, GenieConfig.CBB_rebalance_DayMAPeriod1_A > 0, "Day Rebalance_MAValue1_A");
            MAD.Rebalance_MAValue1_B = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod1_B, GenieConfig.CB_rebalance_B, GenieConfig.CBB_rebalance_DayMAPeriod1_B > 0, "Day Rebalance_MAValue1_B");
            MAD.Rebalance_MAValue1_C = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod1_C, GenieConfig.CB_rebalance_C, GenieConfig.CBB_rebalance_DayMAPeriod1_C > 0, "Day Rebalance_MAValue1_C");
            MAD.Rebalance_MAValue1_D = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod1_D, GenieConfig.CB_rebalance_D, GenieConfig.CBB_rebalance_DayMAPeriod1_D > 0, "Day Rebalance_MAValue1_D");
            MAD.Rebalance_MAValue1_E = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod1_E, GenieConfig.CB_rebalance_E, GenieConfig.CBB_rebalance_DayMAPeriod1_E > 0, "Day Rebalance_MAValue1_E");
            MAD.Rebalance_MAValue1_F = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod1_F, GenieConfig.CB_rebalance_F, GenieConfig.CBB_rebalance_DayMAPeriod1_F > 0, "Day Rebalance_MAValue1_F");
            MAD.Rebalance_MAValue1_G = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod1_G, GenieConfig.CB_rebalance_G, GenieConfig.CBB_rebalance_DayMAPeriod1_G > 0, "Day Rebalance_MAValue1_G");

            // [리밸런싱 2차]                                                                                                      
            MAD.Rebalance_MAValue2_A = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod2_A, GenieConfig.CB_rebalance_A, GenieConfig.CBB_rebalance_DayMAPeriod2_A > 0, "Day Rebalance_MAValue2_A");
            MAD.Rebalance_MAValue2_B = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod2_B, GenieConfig.CB_rebalance_B, GenieConfig.CBB_rebalance_DayMAPeriod2_B > 0, "Day Rebalance_MAValue2_B");
            MAD.Rebalance_MAValue2_C = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod2_C, GenieConfig.CB_rebalance_C, GenieConfig.CBB_rebalance_DayMAPeriod2_C > 0, "Day Rebalance_MAValue2_C");
            MAD.Rebalance_MAValue2_D = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod2_D, GenieConfig.CB_rebalance_D, GenieConfig.CBB_rebalance_DayMAPeriod2_D > 0, "Day Rebalance_MAValue2_D");
            MAD.Rebalance_MAValue2_E = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod2_E, GenieConfig.CB_rebalance_E, GenieConfig.CBB_rebalance_DayMAPeriod2_E > 0, "Day Rebalance_MAValue2_E");
            MAD.Rebalance_MAValue2_F = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod2_F, GenieConfig.CB_rebalance_F, GenieConfig.CBB_rebalance_DayMAPeriod2_F > 0, "Day Rebalance_MAValue2_F");
            MAD.Rebalance_MAValue2_G = CalculateMa(GenieConfig.TB_rebalance_DayMAPeriod2_G, GenieConfig.CB_rebalance_G, GenieConfig.CBB_rebalance_DayMAPeriod2_G > 0, "Day Rebalance_MAValue2_G");

            // [청산 TS]
            MAD.Liquidation_TS_MAValue_A = CalculateMa(GenieConfig.TB_Liquidation_TS_DayMAPeriod_A, GenieConfig.CB_Liquidation_A, GenieConfig.CBB_Liquidation_TS_DayMAPeriod_A > 0, "Day Liquidation_TS_MAValue_A");
            MAD.Liquidation_TS_MAValue_B = CalculateMa(GenieConfig.TB_Liquidation_TS_DayMAPeriod_B, GenieConfig.CB_Liquidation_B, GenieConfig.CBB_Liquidation_TS_DayMAPeriod_B > 0, "Day Liquidation_TS_MAValue_B");
            MAD.Liquidation_TS_MAValue_C = CalculateMa(GenieConfig.TB_Liquidation_TS_DayMAPeriod_C, GenieConfig.CB_Liquidation_C, GenieConfig.CBB_Liquidation_TS_DayMAPeriod_C > 0, "Day Liquidation_TS_MAValue_C");

            // [매매기간]
            MAD.매매기간_TS_MAValue_A = CalculateMa(GenieConfig.TB_매매기간_TS_DayMAPeriod_A, GenieConfig.CBB_매매기간_trading_A > 0, GenieConfig.CBB_매매기간_TS_DayMAPeriod_A > 0, "Day 매매기간_TS_MAValue_A");
            MAD.매매기간_TS_MAValue_B = CalculateMa(GenieConfig.TB_매매기간_TS_DayMAPeriod_B, GenieConfig.CBB_매매기간_trading_B > 0, GenieConfig.CBB_매매기간_TS_DayMAPeriod_B > 0, "Day 매매기간_TS_MAValue_B");
            MAD.매매기간_TS_MAValue_C = CalculateMa(GenieConfig.TB_매매기간_TS_DayMAPeriod_C, GenieConfig.CBB_매매기간_trading_C > 0, GenieConfig.CBB_매매기간_TS_DayMAPeriod_C > 0, "Day 매매기간_TS_MAValue_C");
            MAD.매매기간_TS_MAValue_D = CalculateMa(GenieConfig.TB_매매기간_TS_DayMAPeriod_D, GenieConfig.CBB_매매기간_trading_D > 0, GenieConfig.CBB_매매기간_TS_DayMAPeriod_D > 0, "Day 매매기간_TS_MAValue_D");
            MAD.매매기간_TS_MAValue_E = CalculateMa(GenieConfig.TB_매매기간_TS_DayMAPeriod_E, GenieConfig.CBB_매매기간_trading_E > 0, GenieConfig.CBB_매매기간_TS_DayMAPeriod_E > 0, "Day 매매기간_TS_MAValue_E");
            MAD.매매기간_TS_MAValue_F = CalculateMa(GenieConfig.TB_매매기간_TS_DayMAPeriod_F, GenieConfig.CBB_매매기간_trading_F > 0, GenieConfig.CBB_매매기간_TS_DayMAPeriod_F > 0, "Day 매매기간_TS_MAValue_F");
        }

        public static void New_item(String 종목코드)
        {
            String 코드 = 종목코드;
            Form1.Min_ma_list.TryAdd(코드, new MAPeriod(코드));
            Form1.Day_ma_list.TryAdd(코드, new MAPeriod(코드));
        }

        public static bool Get_이평(Stockbalance 잔고, int CBB_mma1, int CBB_mma2, int CBB_배열, double ma1, double ma2)
        {
            bool result = false;

            if (CBB_mma1 == 0) result = true;
            if (CBB_mma1 == 1 && 잔고.현재가 > ma1) result = true;
            if (CBB_mma1 == 2 && 잔고.현재가 < ma1) result = true;

            if (result && CBB_mma2 == 0) result = true;
            if (result && CBB_mma2 == 1 && 잔고.현재가 > ma2) result = true;
            if (result && CBB_mma2 == 2 && 잔고.현재가 < ma2) result = true;

            if (CBB_mma1 != 0 && CBB_mma2 != 0)
            {
                if (result && CBB_배열 == 0) result = true;
                if (result && CBB_배열 == 1 && ma1 > ma2) result = true;
                if (result && CBB_배열 == 2 && ma1 < ma2) result = true;
            }

            return result;
        }

        public static bool Get_TS_이평(Stockbalance 잔고, int CBB_mma, double ma)
        {
            bool result = false;

            if (CBB_mma == 0) result = true;
            if (CBB_mma == 1 && 잔고.현재가 > ma) result = true;
            if (CBB_mma == 2 && 잔고.현재가 < ma) result = true;

            return result;
        }


    }
}
