using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64
{
    public class RealData_Management :Form1
    {
        public static void Stock_update(bool 예상체결, string itemcode, string Stock_현재가, string Stock_등락율, string 누적_거래량, string 누적_거래대금, string Stock_시가)
        {
            UnifiedDataManager.Instance.RealData.Enqueue(() =>
            {
                if (Form1.Market_Item_List.ContainsKey(itemcode))
                {
                    Market_Item Market = Form1.Market_Item_List[itemcode];
                    double.TryParse(Stock_등락율, out double 등락율);
                    int 현재가 = Math.Abs(int.Parse(Stock_현재가));
                    int 시가 = Math.Abs(int.Parse(Stock_시가));
                    long.TryParse(누적_거래량, out long 누적거래량);
                    long.TryParse(누적_거래대금, out long 누적거래대금);

                    if (예상체결)
                    {
                        Market.등락율 = 등락율;
                        if (Form1.stockBalanceList.TryGetValue(itemcode, out Stockbalance 잔고))
                        {
                            잔고.등락율 = 등락율;
                        }
                    }
                    else
                    {
                        Market.현재가 = 현재가;
                        Market.start_price = 시가;
                        Market.등락율 = 등락율;
                        Market.누적거래량 = 누적거래량;
                        Market.누적거래대금 = 누적거래대금;

                        // =================================================================
                        // [지니 추가] 랭킹 종목 실시간 업데이트 (체결 데이터일 때만)
                        // =================================================================
                        if (Ranking.통합랭킹딕셔너리.TryGetValue(itemcode, out var 랭킹종목))
                        {
                            랭킹종목.현재가 = 현재가;
                            랭킹종목.등락률 = 등락율;
                        }

                        if (Form1.stockBalanceList.TryGetValue(itemcode, out Stockbalance 잔고))
                        {
                            if (!잔고.전량매도)
                            {
                                if (잔고.현재가 != 현재가)
                                {
                                    MA.Mma_Record(잔고);
                                    MA.Dma_Record(잔고);
                                }

                                double tax_ = Form1.TAX;
                                if (잔고.시장.Equals("E")) tax_ = 0;

                                int 잔고현재가 = 잔고.현재가;
                                int 보유수량 = 잔고.보유수량;
                                int 매입금액 = 잔고.평균단가 * 보유수량;

                                잔고.현재가 = (int)현재가;
                                잔고.등락율 = 등락율;
                                잔고.평가금액 = (long)(잔고.현재가 * 보유수량);

                                // 1. [기준수익률]
                                if (Form1.form1.기준값변경 && 잔고.기준가격 > 0) // 0으로 나누기 방지 추가
                                {
                                    double 기준_퍼센트 = ((double)(현재가 - 잔고.기준가격) / 잔고.기준가격) * 100.0;
                                    double 비용_퍼센트 = (Form1.수수료 * 2 + tax_) * 100.0;

                                    // HTS 방식과 동일하게 소수점 2째 자리 반올림
                                    잔고.기준수익률 = Math.Round(기준_퍼센트 - 비용_퍼센트, 2);
                                }

                                // 2. [시작수익률]
                                if (잔고.시작가격 > 0) // 0으로 나누기 방지 추가
                                {
                                    double 시작_퍼센트 = ((double)(현재가 - 잔고.시작가격) / 잔고.시작가격) * 100.0;
                                    double 비용_퍼센트 = (Form1.수수료 * 2 + tax_) * 100.0;

                                    // HTS 방식과 동일하게 소수점 2째 자리 반올림
                                    잔고.시작수익률 = Math.Round(시작_퍼센트 - 비용_퍼센트, 2);
                                }

                                잔고.수익률 = 신용계산.Get_Real_Profit_Rate(잔고, out long 실질순손익);
                                잔고.평가손익 = 실질순손익;
                                잔고.예상손익 = 잔고.평가손익 + 잔고.누적손익;

                                if (0 < 잔고.수익률 && 잔고.최고수익률 < 잔고.수익률)
                                {
                                    잔고.최고수익률 = 잔고.수익률;
                                }

                                if (잔고.수익률 < 0 && 잔고.최저수익률 > 잔고.수익률)
                                {
                                    잔고.최저수익률 = 잔고.수익률;
                                }

                                if (0 < 잔고.예상손익 && 잔고.최고예상손익금 < 잔고.예상손익)
                                {
                                    잔고.최고예상손익금 = 잔고.예상손익;
                                }

                                if (잔고.예상손익 < 0 && 잔고.최저예상손익금 > 잔고.예상손익)
                                {
                                    잔고.최저예상손익금 = 잔고.예상손익;
                                }

                                if (GenieConfig.CB_최종가업데이트)
                                {
                                    if (잔고.시작가격 < 잔고.현재가)
                                    {
                                        // [지니 최적화] 전체 검색(FindAll) 대신 딕셔너리 키로 해당 종목 리스트 즉시 조회
                                        if (Form1.최종매입가_List.TryGetValue(itemcode, out List<최종매입가> 해당종목_리스트))
                                        {
                                            // 리스트 수정 중 충돌 방지 (Lock)
                                            lock (해당종목_리스트)
                                            {
                                                // 조건: 리스트 개수가 7개일 때만 (즉, 초기 상태일 때만) 업데이트
                                                if (해당종목_리스트.Count == 7)
                                                {
                                                    // [핵심] Find를 7번 호출하는 대신, 리스트를 한 번만 훑어서 처리 (속도 향상)
                                                    foreach (var item in 해당종목_리스트)
                                                    {
                                                        // 조건 1. 번호가 0이어야 함
                                                        // 조건 2. 위치가 "리밸_"로 시작해야 함 (A~G 모두 포함됨)
                                                        if (item.번호 == 0 && item.위치.StartsWith("리밸_"))
                                                        {
                                                            // 현재가가 더 높으면 매입가 업데이트
                                                            if (item.매입가 < 잔고.현재가)
                                                            {
                                                                item.매입가 = 잔고.현재가;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        // 1. [최적화 포인트] 잔고 확인은 루프 밖에서 한 번만!
                        bool isBalanceFound = stockBalanceList.TryGetValue(itemcode, out Stockbalance 잔고_);

                        // 2. [최적화 포인트] Market 정보도 루프 안에서 매번 찾지 않고 밖에서 미리 한 번만 찾아둡니다.
                        string market = "";
                        if (Market_Item_List.TryGetValue(itemcode, out var marketItem))
                        {
                            market = marketItem.Market;
                        }

                        // 3. [최적화 포인트] LINQ (.Where.ToList) 완전 제거! 
                        // 새로운 리스트를 메모리에 만들지 않고, 딕셔너리의 Value를 직접 순회합니다.
                        foreach (JumunItem item in Form1.JumunItem_List.Values)
                        {
                            // 조건문(if)으로 필터링을 대체합니다. (메모리 할당 0, 속도 최상)
                            if (item.종목코드 == itemcode)
                            {
                                // 4. 시세 정보 업데이트
                                item.현재가 = 현재가;
                                item.등락률 = 등락율;

                                // 5. 잔고 수익률 업데이트
                                if (isBalanceFound)
                                {
                                    item.수익률 = 잔고_.수익률;
                                }

                                // 6. 호가(틱) 변화 시 계산
                                if (현재가 != item.Tik_price)
                                {
                                    // 위에서 미리 찾아둔 'market' 변수를 사용하여 딕셔너리 중복 검색(병목)을 방지합니다.
                                    item.Tik_cap = Method.Find_Tik_Cap(현재가, item.주문가격, market);
                                    item.Tik_price = 현재가;
                                }
                            }
                        }

                        Order_Reserve.수동주문_주문가변화(Market);

                    }
                }
            });
        }


        public static void Market_update(string itemcode, string Jisu_등락율, string Jisu_현재가, string Jisu_저가, string Jisu_고가)
        {
            UnifiedDataManager.Instance.RealData.Enqueue(() =>
            {
                double 현재가 = Math.Abs(double.Parse(Jisu_현재가));
                double 등락율 = double.Parse(Jisu_등락율);
                double 저가 = Math.Abs(double.Parse(Jisu_저가));
                double 고가 = Math.Abs(double.Parse(Jisu_고가));

                if (Jisu_현재가.Equals(Jisu_저가) && Jisu_저가.Equals(Jisu_고가))
                {
                    if (itemcode.Equals("001")) // 코스피 등락율
                    {
                        Form1.Acc.피_현재가 = 현재가;
                        Form1.Acc.피_등락률 = 등락율;

                        AVG_jisu_print("001", 현재가);
                    }

                    if (itemcode.Equals("101"))// 코스닥 등락율
                    {
                        Form1.Acc.닥_현재가 = 현재가;
                        Form1.Acc.닥_등락률 = 등락율;

                        AVG_jisu_print("101", 현재가);
                    }
                }
                else
                {
                    double 업종고가대비 = (Math.Truncate((double)(현재가 - 고가) / 고가 * (double)100 * 100)) / 100;
                    double 업종저가대비 = (Math.Truncate((double)(현재가 - 저가) / 저가 * (double)100 * 100)) / 100;

                    if (itemcode.Equals("001")) // 코스피 등락율
                    {
                        //if (Form1.kospi_avg_min.Count > 0) Form1.kospi_avg_min[0].endprice = 현재가;
                        //if (Form1.kospi_avg_day.Count > 0) Form1.kospi_avg_day[0].endprice = 현재가;

                        // 1. 분봉 데이터 0번 갱신
                        if (kospi_min_count > 0 && kospi_day_count > 0)
                        {
                            kospi_avg_min[0].Endprice = 현재가;
                            kospi_avg_day[0].Endprice = 현재가;
                        }

                        Form1.Acc.피_현재가 = 현재가;
                        Form1.Acc.피_등락률 = 등락율;
                        Form1.Acc.피_저가대비 = 업종저가대비;
                        Form1.Acc.피_고가대비 = 업종고가대비;

                        AVG_jisu_print("001", 현재가);
                    }

                    if (itemcode.Equals("101"))// 코스닥 등락율
                    {
                        //if (Form1.kosdaq_avg_min.Count > 0) Form1.kosdaq_avg_min[0].endprice = 현재가;
                        //if (Form1.kosdaq_avg_day.Count > 0) Form1.kosdaq_avg_day[0].endprice = 현재가;

                        if (kosdaq_min_count > 0 && kosdaq_day_count > 0)
                        {
                            kosdaq_avg_min[0].Endprice = 현재가;
                            kosdaq_avg_day[0].Endprice = 현재가;
                        }

                        Form1.Acc.닥_현재가 = 현재가;
                        Form1.Acc.닥_등락률 = 등락율;
                        Form1.Acc.닥_저가대비 = 업종저가대비;
                        Form1.Acc.닥_고가대비 = 업종고가대비;

                        AVG_jisu_print("101", 현재가);
                    }
                }
            });
     
        }



        public static void AVG_jisu_print(string market, double now_price)
        {
            int 마켓인덱스 = -1;
            AVG_price[] minArr = null;
            AVG_price[] dayArr = null;
            int minCount = 0;
            int dayCount = 0;

            // 1. 시장 구분 및 배열 참조 설정
            if (market.Equals("001")) // 코스피
            {
                마켓인덱스 = 0;
                minArr = kospi_avg_min;
                dayArr = kospi_avg_day;
                minCount = kospi_min_count;
                dayCount = kospi_day_count;
            }
            else if (market.Equals("101")) // 코스닥
            {
                마켓인덱스 = 1;
                minArr = kosdaq_avg_min;
                dayArr = kosdaq_avg_day;
                minCount = kosdaq_min_count;
                dayCount = kosdaq_day_count;
            }

            if (마켓인덱스 == -1 || minArr == null) return;

            // 글로벌 전역 변수에서 현재 시장의 추세 객체를 가져옵니다.
            지수이평추세 현재시장추세 = Form1.지수이평추세[마켓인덱스];

            // =========================================================
            // [1단계] 시장의 팩트(추세)만 순수하게 계산합니다.
            // =========================================================
            CalculateAvg(현재시장추세, minArr, minCount, true, now_price);  // 분봉 계산
            CalculateAvg(현재시장추세, dayArr, dayCount, false, now_price); // 일봉 계산

            // =========================================================
            // [2단계] 계산된 팩트를 바탕으로 모든 그룹의 결과값을 일괄 업데이트합니다.
            // =========================================================
            UpdateAllGroups(마켓인덱스, 현재시장추세);


            // ---------------------------------------------------------
            // [내부 함수 1] 평균 계산 및 추세(상승/하락 팩트) 업데이트
            // ---------------------------------------------------------
            void CalculateAvg(지수이평추세 이평추세, AVG_price[] dataArray, int dataCount, bool isMin, double currentPrice)
            {
                if (dataArray == null || dataCount == 0) return;

                double sum = 0;

                for (int i = 0; i < dataCount; i++)
                {
                    sum += dataArray[i].Endprice;
                    int days = i + 1;

                    double avgPrice = sum / days;
                    bool isHigher = (avgPrice < currentPrice); // 현재가가 이평선보다 높으면 true(상승추세)

                    if (isMin)
                    {
                        if (days == 3) 이평추세.Min_추세_03 = isHigher;
                        else if (days == 5) 이평추세.Min_추세_05 = isHigher;
                        else if (days == 10) 이평추세.Min_추세_10 = isHigher;
                        else if (days == 20) 이평추세.Min_추세_20 = isHigher;
                        else if (days == 30) 이평추세.Min_추세_30 = isHigher;
                        else if (days == 60) 이평추세.Min_추세_60 = isHigher;
                    }
                    else
                    {
                        if (days == 3) 이평추세.Day_추세_03 = isHigher;
                        else if (days == 5) 이평추세.Day_추세_05 = isHigher;
                        else if (days == 10) 이평추세.Day_추세_10 = isHigher;
                        else if (days == 20) 이평추세.Day_추세_20 = isHigher;
                        else if (days == 40) 이평추세.Day_추세_40 = isHigher;
                        else if (days == 60) 이평추세.Day_추세_60 = isHigher;
                    }
                }
            }

            // ---------------------------------------------------------
            // [내부 함수 2] 등록된 모든 그룹을 돌면서 통과 여부(결과값)를 채점합니다.
            // ---------------------------------------------------------
            void UpdateAllGroups(int idx, 지수이평추세 시장추세)
            {
                // 딕셔너리에 있는 모든 그룹(A, B, C...)을 하나씩 꺼냅니다.
                foreach (KeyValuePair<string, 지수이평사용값[]> 그룹 in Form1.그룹별_지수사용값)
                {
                    지수이평사용값 그룹설정 = 그룹.Value[idx]; // 코스피면 0, 코스닥이면 1

                    // 만약 해당 그룹에서 이 지수 자체를 아예 사용 안 한다면 패스해도 무방하지만,
                    // 확실한 처리를 위해 CheckOption 함수에서 걸러줍니다.

                    // 분봉 결과 채점
                    그룹설정.결과값_min_03 = CheckOption(그룹설정.Use_min_03, 그룹설정.추세사용값_min_03, 시장추세.Min_추세_03);
                    그룹설정.결과값_min_05 = CheckOption(그룹설정.Use_min_05, 그룹설정.추세사용값_min_05, 시장추세.Min_추세_05);
                    그룹설정.결과값_min_10 = CheckOption(그룹설정.Use_min_10, 그룹설정.추세사용값_min_10, 시장추세.Min_추세_10);
                    그룹설정.결과값_min_20 = CheckOption(그룹설정.Use_min_20, 그룹설정.추세사용값_min_20, 시장추세.Min_추세_20);
                    그룹설정.결과값_min_30 = CheckOption(그룹설정.Use_min_30, 그룹설정.추세사용값_min_30, 시장추세.Min_추세_30);
                    그룹설정.결과값_min_60 = CheckOption(그룹설정.Use_min_60, 그룹설정.추세사용값_min_60, 시장추세.Min_추세_60);

                    // 일봉 결과 채점
                    그룹설정.결과값_day_03 = CheckOption(그룹설정.Use_day_03, 그룹설정.추세사용값_day_03, 시장추세.Day_추세_03);
                    그룹설정.결과값_day_05 = CheckOption(그룹설정.Use_day_05, 그룹설정.추세사용값_day_05, 시장추세.Day_추세_05);
                    그룹설정.결과값_day_10 = CheckOption(그룹설정.Use_day_10, 그룹설정.추세사용값_day_10, 시장추세.Day_추세_10);
                    그룹설정.결과값_day_20 = CheckOption(그룹설정.Use_day_20, 그룹설정.추세사용값_day_20, 시장추세.Day_추세_20);
                    그룹설정.결과값_day_40 = CheckOption(그룹설정.Use_day_40, 그룹설정.추세사용값_day_40, 시장추세.Day_추세_40);
                    그룹설정.결과값_day_60 = CheckOption(그룹설정.Use_day_60, 그룹설정.추세사용값_day_60, 시장추세.Day_추세_60);
                }
            }

            // ---------------------------------------------------------
            // [헬퍼] 옵션 사용 여부 및 방향(UD) 체크
            // ---------------------------------------------------------
            bool CheckOption(bool use, bool isUp, bool isHigher)
            {
                if (!use) return true; // 사용 안 하면 무조건 통과(true)

                // 사용함(use=true)인 경우:
                // isUp(초과) -> isHigher(평균보다 높음)여야 통과
                // !isUp(이하) -> !isHigher(평균보다 낮음)여야 통과
                return isUp ? isHigher : !isHigher;
            }
        }

        public static void Market_fluctuate(string itemcode, string 누적거래대금, string 상한종목수, string 상승종목수, string 보합종목수, string 하한종목수, string 하락종목수)
        {
            UnifiedDataManager.Instance.RealData.Enqueue(() =>
            {
                Helper.안전한_UI_업데이트(Form1.form1, () =>
               {
                   if (itemcode.Equals("001")) // 코스피 등락율
                   {
                       Form1.Acc.피_누적거래대금 = 누적거래대금;
                       Form1.Acc.피_상한종목수 = 상한종목수;
                       Form1.Acc.피_상승종목수 = 상승종목수;
                       Form1.Acc.피_보합종목수 = 보합종목수;
                       Form1.Acc.피_하한종목수 = 하한종목수;
                       Form1.Acc.피_하락종목수 = 하락종목수;

                       Form1.form1.LB_코스피대금.Text = "금:" + int.Parse(누적거래대금) / 100 + "억";
                       Form1.form1.LB_코스피상승.Text = "▲:" + 상한종목수 + " △:" + 상승종목수;
                       Form1.form1.LB_코스피보합.Text = "－:" + 보합종목수;
                       Form1.form1.LB_코스피하락.Text = "▽:" + 하락종목수 + " ▼:" + 하한종목수;

                   }
                   else if (itemcode.Equals("101"))// 코스닥 등락율
                   {
                       Form1.Acc.닥_누적거래대금 = 누적거래대금;
                       Form1.Acc.닥_상한종목수 = 상한종목수;
                       Form1.Acc.닥_상승종목수 = 상승종목수;
                       Form1.Acc.닥_보합종목수 = 보합종목수;
                       Form1.Acc.닥_하한종목수 = 하한종목수;
                       Form1.Acc.닥_하락종목수 = 하락종목수;

                       Form1.form1.LB_코스닥대금.Text = "금:" + int.Parse(누적거래대금) / 100 + "억";
                       Form1.form1.LB_코스닥상승.Text = "▲:" + 상한종목수 + " △:" + 상승종목수;
                       Form1.form1.LB_코스닥보합.Text = "－:" + 보합종목수;
                       Form1.form1.LB_코스닥하락.Text = "▽:" + 하락종목수 + " ▼:" + 하한종목수;
                       //↑▲△↓▼▽－
                   }
               });
            });
        }

        public static void Real_Watch_update(string itemcode, string Real_등락율, string Real_현재가, string Real_시가, string Real_고가, string Real_저가, string 누적거래량, string Real_누적거래대금, string Real_거래대금증감, string 전일거래량대비, string 거래회전율, string 시가총액)
        {
            UnifiedDataManager.Instance.RealData.Enqueue(() =>
            {
                if (Form1.Watch_List.Count > 0)
                {
                    if (Form1.Market_Item_List.ContainsKey(itemcode))
                    {
                        double.TryParse(Real_등락율, out double 등락율);
                        double.TryParse(Real_현재가, out double 현재가);
                        double.TryParse(Real_누적거래대금, out double 누적거래대금);
                        int.TryParse(Real_시가, out int 시가);
                        int.TryParse(Real_고가, out int 고가);
                        int.TryParse(Real_저가, out int 저가);
                        double.TryParse(Real_거래대금증감, out double 거래대금증감);

                        Tab_Watch.Watch_update(itemcode, 등락율, Math.Abs(현재가), 누적거래량, 누적거래대금, Math.Abs(시가), Math.Abs(고가), Math.Abs(저가), 거래대금증감, 전일거래량대비, 거래회전율, 시가총액);
                    }
                }
            });
       
        }

    }
}
