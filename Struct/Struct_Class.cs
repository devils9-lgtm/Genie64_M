namespace 지니64
{
    public struct AVG_price
    {
        public double Endprice;
        public long Time;

        public AVG_price(double endPrice, long Time)
        {
            this.Endprice = endPrice;
            this.Time = Time;
        }
    }

    public class 지수이평사용값
    {
        public bool 사용유무 { get; set; }

        public bool Use_min_03 { get; set; }
        public bool Use_min_05 { get; set; }
        public bool Use_min_10 { get; set; }
        public bool Use_min_20 { get; set; }
        public bool Use_min_30 { get; set; }
        public bool Use_min_60 { get; set; }
        public bool Use_day_03 { get; set; }
        public bool Use_day_05 { get; set; }
        public bool Use_day_10 { get; set; }
        public bool Use_day_20 { get; set; }
        public bool Use_day_40 { get; set; }
        public bool Use_day_60 { get; set; }

        public bool 추세사용값_min_03 { get; set; }
        public bool 추세사용값_min_05 { get; set; }
        public bool 추세사용값_min_10 { get; set; }
        public bool 추세사용값_min_20 { get; set; }
        public bool 추세사용값_min_30 { get; set; }
        public bool 추세사용값_min_60 { get; set; }
        public bool 추세사용값_day_03 { get; set; }
        public bool 추세사용값_day_05 { get; set; }
        public bool 추세사용값_day_10 { get; set; }
        public bool 추세사용값_day_20 { get; set; }
        public bool 추세사용값_day_40 { get; set; }
        public bool 추세사용값_day_60 { get; set; }

        public bool 결과값_day_03 { get; set; }
        public bool 결과값_day_05 { get; set; }
        public bool 결과값_day_10 { get; set; }
        public bool 결과값_day_20 { get; set; }
        public bool 결과값_day_40 { get; set; }
        public bool 결과값_day_60 { get; set; }
        public bool 결과값_min_03 { get; set; }
        public bool 결과값_min_05 { get; set; }
        public bool 결과값_min_10 { get; set; }
        public bool 결과값_min_20 { get; set; }
        public bool 결과값_min_30 { get; set; }
        public bool 결과값_min_60 { get; set; }

        public 지수이평사용값() { }
    }

    public class 지수이평추세
    {
        public bool Min_추세_03 { get; set; }
        public bool Min_추세_05 { get; set; }
        public bool Min_추세_10 { get; set; }
        public bool Min_추세_20 { get; set; }
        public bool Min_추세_30 { get; set; }
        public bool Min_추세_60 { get; set; }
        public bool Day_추세_03 { get; set; }
        public bool Day_추세_05 { get; set; }
        public bool Day_추세_10 { get; set; }
        public bool Day_추세_20 { get; set; }
        public bool Day_추세_40 { get; set; }
        public bool Day_추세_60 { get; set; }

        public 지수이평추세() { }
    }


    public class MAPeriod
    {
        public string Code { get; set; }

        public double Repeat_MAValue1_A { get; set; }
        public double Repeat_MAValue1_B { get; set; }
        public double Repeat_MAValue1_C { get; set; }
        public double Repeat_MAValue1_D { get; set; }
        public double Repeat_MAValue1_E { get; set; }
        public double Repeat_MAValue1_F { get; set; }
        public double Repeat_MAValue1_G { get; set; }
        public double Repeat_MAValue1_H { get; set; }
        public double Repeat_MAValue1_I { get; set; }
        public double Repeat_MAValue1_J { get; set; }
        public double Repeat_MAValue1_K { get; set; }
        public double Repeat_MAValue1_L { get; set; }
        public double Repeat_MAValue1_M { get; set; }
        public double Repeat_MAValue1_N { get; set; }
        public double Repeat_MAValue2_A { get; set; }
        public double Repeat_MAValue2_B { get; set; }
        public double Repeat_MAValue2_C { get; set; }
        public double Repeat_MAValue2_D { get; set; }
        public double Repeat_MAValue2_E { get; set; }
        public double Repeat_MAValue2_F { get; set; }
        public double Repeat_MAValue2_G { get; set; }
        public double Repeat_MAValue2_H { get; set; }
        public double Repeat_MAValue2_I { get; set; }
        public double Repeat_MAValue2_J { get; set; }
        public double Repeat_MAValue2_K { get; set; }
        public double Repeat_MAValue2_L { get; set; }
        public double Repeat_MAValue2_M { get; set; }
        public double Repeat_MAValue2_N { get; set; }
        public double Rebalance_MAValue1_A { get; set; }
        public double Rebalance_MAValue1_B { get; set; }
        public double Rebalance_MAValue1_C { get; set; }
        public double Rebalance_MAValue1_D { get; set; }
        public double Rebalance_MAValue1_E { get; set; }
        public double Rebalance_MAValue1_F { get; set; }
        public double Rebalance_MAValue1_G { get; set; }
        public double Rebalance_MAValue2_A { get; set; }
        public double Rebalance_MAValue2_B { get; set; }
        public double Rebalance_MAValue2_C { get; set; }
        public double Rebalance_MAValue2_D { get; set; }
        public double Rebalance_MAValue2_E { get; set; }
        public double Rebalance_MAValue2_F { get; set; }
        public double Rebalance_MAValue2_G { get; set; }
        public double Liquidation_MAValue_A { get; set; }
        public double Liquidation_MAValue_B { get; set; }
        public double Liquidation_MAValue_C { get; set; }
        public double Rebalance_TS_MAValue_1차_A { get; set; }
        public double Rebalance_TS_MAValue_1차_B { get; set; }
        public double Rebalance_TS_MAValue_1차_C { get; set; }
        public double Rebalance_TS_MAValue_1차_D { get; set; }
        public double Rebalance_TS_MAValue_1차_E { get; set; }
        public double Rebalance_TS_MAValue_1차_F { get; set; }
        public double Rebalance_TS_MAValue_1차_G { get; set; }
        public double Rebalance_TS_MAValue_2차_A { get; set; }
        public double Rebalance_TS_MAValue_2차_B { get; set; }
        public double Rebalance_TS_MAValue_2차_C { get; set; }
        public double Rebalance_TS_MAValue_2차_D { get; set; }
        public double Rebalance_TS_MAValue_2차_E { get; set; }
        public double Rebalance_TS_MAValue_2차_F { get; set; }
        public double Rebalance_TS_MAValue_2차_G { get; set; }
        public double Liquidation_TS_MAValue_A { get; set; }
        public double Liquidation_TS_MAValue_B { get; set; }
        public double Liquidation_TS_MAValue_C { get; set; }
        public double 매매기간_TS_MAValue_A { get; set; }
        public double 매매기간_TS_MAValue_B { get; set; }
        public double 매매기간_TS_MAValue_C { get; set; }
        public double 매매기간_TS_MAValue_D { get; set; }
        public double 매매기간_TS_MAValue_E { get; set; }
        public double 매매기간_TS_MAValue_F { get; set; }

        public MAPeriod(string code)
        {
            this.Code = code;
        }
    }


    // [최적화] 매매내역 전용 구조체 (문자열 파싱 비용 제거)
    public struct TradeLog
    {
        // [공통 필드]
        public string Date;         // 일자
        public string Name;         // 종목명 (일자별 조회 시 사용)

        // [금액 필드] (계산 편의를 위해 double/long 사용)
        public long BuySum;         // 매수금액
        public long SellSum;        // 매도금액
        public long Profit;         // 실현손익
        public long Fee;            // 수수료
        public long Tax;            // 세금

        // [추가 필드] (기준일매매내역용)
        public double Price;        // 체결가 (평균단가 등)
        public double Rate;         // 수익률

        // 생성자 1: 일자별 실현손익 (통계_opt10074)
        public TradeLog(string date, string buy, string sell, string profit, string fee, string tax)
        {
            Date = date; Name = ""; Price = 0; Rate = 0;
            long.TryParse(buy, out BuySum);
            long.TryParse(sell, out SellSum);
            long.TryParse(profit, out Profit);
            long.TryParse(fee, out Fee);
            long.TryParse(tax, out Tax);
        }

        // 생성자 2: 종목별 실현손익 (TR_일자별종목별실현손익요청)
        public TradeLog(string name, double buySum, double sellSum, double profit, string fee, string tax, string price, string rate)
        {
            Date = ""; Name = name;
            BuySum = (long)buySum;
            SellSum = (long)sellSum;
            Profit = (long)profit;
            long.TryParse(fee, out Fee);
            long.TryParse(tax, out Tax);
            double.TryParse(price, out Price);
            double.TryParse(rate, out Rate);
        }
    }
}
