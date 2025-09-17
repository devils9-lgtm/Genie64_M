using System;

namespace 지니_64
{
    public class Market_Item
    {
        public bool 매수증거금알림 { get; set; }
        public bool 매매알림_모투제한 { get; set; }
        public bool 매매알림_상한가 { get; set; }
        public bool 매매알림_하한가 { get; set; }
        public bool 매매알림_vI매수 { get; set; }
        public bool 매매알림_VI매도 { get; set; }
        public string 종목명 { get; set; }
        public string 종목코드 { get; set; }
        public string Market { get; set; }
        public int start_price { get; set; }
        public int 현재가 { get; set; }
        public int 누적거래량 { get; set; }
        public int 누적거래대금 { get; set; }
        public int Last_price { get; set; }
        public double 등락율 { get; set; }
        public string 과열 { get; set; }
        public long 도대금 { get; set; }
        public int 도시간 { get; set; }
        public int Sell_start { get; set; }
        public int Sell_End { get; set; }
        public bool 매수가능_A { get; set; }
        public int 재매수_A { get; set; }
        public long 수대금_A { get; set; }
        public int 수시간_A { get; set; }
        public int Buy_start_A { get; set; }
        public int Buy_End_A { get; set; }
        public bool 매수가능_B { get; set; }
        public int 재매수_B { get; set; }
        public long 수대금_B { get; set; }
        public int 수시간_B { get; set; }
        public int Buy_start_B { get; set; }
        public int Buy_End_B { get; set; }
        public int minute { get; set; }
        public int S_minute { get; set; }
        public int Buy_상승카운터_A { get; set; }
        public int Buy_하락카운터_A { get; set; }
        public int Buy_현재가_A { get; set; }
        public int Buy_상승카운터_B { get; set; }
        public int Buy_하락카운터_B { get; set; }
        public int Buy_현재가_B { get; set; }
        public int Sell_상승카운터 { get; set; }
        public int Sell_하락카운터 { get; set; }
        public int Sell_현재가 { get; set; }

        public Market_Item(bool 매수증거금알림, bool 매매알림_모투제한, bool 매매알림_상한가, bool 매매알림_하한가, bool 매매알림_vI매수, bool 매매알림_VI매도, string 종목명, string 종목코드, string Market, int start_price, int 현재가,
               int 누적거래량, int 누적거래대금, int Last_price, double 등락율, string 과열, long 도대금, int 도시간, int Sell_start, int Sell_End,
               bool 매수가능_A, int 재매수_A, long 수대금_A, int 수시간_A, int Buy_start_A, int Buy_End_A,
               bool 매수가능_B, int 재매수_B, long 수대금_B, int 수시간_B, int Buy_start_B, int Buy_End_B, int minute, int S_minute,
               int Buy_상승카운터_A, int Buy_하락카운터_A, int Buy_현재가_A, int Buy_상승카운터_B, int Buy_하락카운터_B, int Buy_현재가_B, int Sell_상승카운터, int Sell_하락카운터, int Sell_현재가)
        {
            this.매수증거금알림 = 매수증거금알림;
            this.매매알림_모투제한 = 매매알림_모투제한;
            this.매매알림_상한가 = 매매알림_상한가;
            this.매매알림_하한가 = 매매알림_하한가;
            this.매매알림_vI매수 = 매매알림_vI매수;
            this.매매알림_VI매도 = 매매알림_VI매도;
            this.종목명 = 종목명;
            this.종목코드 = 종목코드;
            this.Market = Market;
            this.start_price = start_price;
            this.현재가 = 현재가;
            this.누적거래량 = 누적거래량;
            this.누적거래대금 = 누적거래대금;
            this.Last_price = Last_price;
            this.등락율 = 등락율;
            this.과열 = 과열;
            this.도대금 = 도대금;
            this.도시간 = 도시간;
            this.Sell_start = Sell_start;
            this.Sell_End = Sell_End;
            this.매수가능_A = 매수가능_A;
            this.재매수_A = 재매수_A;
            this.수대금_A = 수대금_A;
            this.수시간_A = 수시간_A;
            this.Buy_start_A = Buy_start_A;
            this.Buy_End_A = Buy_End_A;
            this.매수가능_B = 매수가능_B;
            this.재매수_B = 재매수_B;
            this.수대금_B = 수대금_B;
            this.수시간_B = 수시간_B;
            this.Buy_start_B = Buy_start_B;
            this.Buy_End_B = Buy_End_B;
            this.minute = minute;
            this.S_minute = S_minute;
            this.Buy_상승카운터_A = Buy_상승카운터_A;
            this.Buy_하락카운터_A = Buy_하락카운터_A;
            this.Buy_현재가_A = Buy_현재가_A;
            this.Buy_상승카운터_B = Buy_상승카운터_B;
            this.Buy_하락카운터_B = Buy_하락카운터_B;
            this.Buy_현재가_B = Buy_현재가_B;
            this.Sell_상승카운터 = Sell_상승카운터;
            this.Sell_하락카운터 = Sell_하락카운터;
            this.Sell_현재가 = Sell_현재가;
        }
    }


    public class Condition
    {
        public int index; //조건식 인덱스
        public string name; //조건식 이름

        public Condition(int _index, string _name)
        {
            index = _index;
            name = _name;
        }
    }

    public class RunCondition
    {
        public string condition; //조건식 이름
        public int number; //조건식 이름

        public RunCondition(string condition, int number)
        {
            this.condition = condition;
            this.number = number;
        }
    }

    public class Fail_condition
    {
        public string condition { get; set; } //조건식 이름
        public int count { get; set; } //조건식 인덱스

        public Fail_condition(string condition, int count)
        {
            this.condition = condition;
            this.count = count;
        }
    }

    public class Account
    {
        public long 기준계산금 { get; set; }
        public long 증가자산 { get; set; }
        public long 추정자산 { get; set; }
        public long D2 { get; set; }
        public long 추정D2 { get; set; }
        public long 매입금 { get; set; }
        public long 평가금 { get; set; }
        public long 평가손익 { get; set; }
        public double 평가손익률 { get; set; }
        public long 실현손익 { get; set; }
        public double 실현손익률 { get; set; }
        public string 시간청산 { get; set; }
        public string 실현손익금청산 { get; set; }
        public string 예상손실청산 { get; set; }
        public string 예상수익청산 { get; set; }


        public string 신규_A { get; set; }
        public string 신규_B { get; set; }
        public string 신규_C { get; set; }
        public string 기간_A { get; set; }
        public string 기간_B { get; set; }
        public string 기간_C { get; set; }
        public string 기간_D { get; set; }
        public string 기간_E { get; set; }
        public string 기간_F { get; set; }
        public string 반복_A { get; set; }
        public string 반복_B { get; set; }
        public string 반복_C { get; set; }
        public string 반복_D { get; set; }
        public string 반복_E { get; set; }
        public string 반복_F { get; set; }
        public string 반복_G { get; set; }
        public string 반복_H { get; set; }
        public string 반복_I { get; set; }
        public string 반복_J { get; set; }
        public string 반복_K { get; set; }
        public string 반복_L { get; set; }
        public string 반복_M { get; set; }
        public string 반복_N { get; set; }
        public string 리밸_A { get; set; }
        public string 리밸_B { get; set; }
        public string 리밸_C { get; set; }
        public string 리밸_D { get; set; }
        public string 리밸_E { get; set; }
        public string 리밸_F { get; set; }
        public string 리밸_G { get; set; }
        public string 청산_A { get; set; }
        public string 청산_B { get; set; }
        public string 청산_C { get; set; }
        public double 매입비 { get; set; }
        public double 피_현재가 { get; set; }
        public double 피_등락률 { get; set; }
        public double 피_저가대비 { get; set; }
        public double 피_고가대비 { get; set; }
        public int 피_외인 { get; set; }
        public int 피_기관 { get; set; }
        public int 피_프로그램 { get; set; }
        public string 피_누적거래대금 { get; set; }
        public string 피_상한종목수 { get; set; }
        public string 피_상승종목수 { get; set; }
        public string 피_보합종목수 { get; set; }
        public string 피_하한종목수 { get; set; }
        public string 피_하락종목수 { get; set; }
        public double 닥_현재가 { get; set; }
        public double 닥_등락률 { get; set; }
        public double 닥_저가대비 { get; set; }
        public double 닥_고가대비 { get; set; }
        public int 닥_외인 { get; set; }
        public int 닥_기관 { get; set; }
        public int 닥_프로그램 { get; set; }
        public string 닥_누적거래대금 { get; set; }
        public string 닥_상한종목수 { get; set; }
        public string 닥_상승종목수 { get; set; }
        public string 닥_보합종목수 { get; set; }
        public string 닥_하한종목수 { get; set; }
        public string 닥_하락종목수 { get; set; }

        public Account(long 기준계산금, long 증가자산, long 추정자산, long D2, long 추정D2, long 매입금, long 평가금, long 평가손익, double 평가손익률, long 실현손익, double 실현손익률, string 시간청산, string 실현손익금청산, string 예상손실청산, string 예상수익청산
                       , double 피_현재가, double 피_등락률, double 피_저가대비, double 피_고가대비, double 닥_현재가, double 닥_등락률, double 닥_저가대비, double 닥_고가대비, string 신규_A, string 신규_B, string 신규_C, string 기간_A, string 기간_B, string 기간_C, string 기간_D, string 기간_E, string 기간_F
                       , string 반복_A, string 반복_B, string 반복_C, string 반복_D, string 반복_E, string 반복_F, string 반복_G, string 반복_H, string 반복_I, string 반복_J, string 반복_K, string 반복_L, string 반복_M, string 반복_N, string 리밸_A, string 리밸_B, string 리밸_C, string 리밸_D, string 리밸_E, string 리밸_F, string 리밸_G, string 청산_A, string 청산_B, string 청산_C, double 매입비
                       , int 피_외인, int 피_기관, int 피_프로그램, int 닥_외인, int 닥_기관, int 닥_프로그램, string 피_누적거래대금, string 피_상한종목수, string 피_상승종목수, string 피_보합종목수, string 피_하한종목수, string 피_하락종목수, string 닥_누적거래대금, string 닥_상한종목수, string 닥_상승종목수, string 닥_보합종목수, string 닥_하한종목수, string 닥_하락종목수)
        {
            this.기준계산금 = 기준계산금;
            this.증가자산 = 증가자산;
            this.추정자산 = 추정자산;
            this.D2 = D2;
            this.추정D2 = 추정D2;
            this.매입금 = 매입금;
            this.평가금 = 평가금;
            this.평가손익 = 평가손익;
            this.평가손익률 = 평가손익률;
            this.실현손익 = 실현손익;
            this.실현손익률 = 실현손익률;
            this.시간청산 = 시간청산;
            this.실현손익금청산 = 실현손익금청산;
            this.예상손실청산 = 예상손실청산;
            this.예상수익청산 = 예상수익청산;
            this.피_현재가 = 피_현재가;
            this.피_등락률 = 피_등락률;
            this.피_저가대비 = 피_저가대비;
            this.피_고가대비 = 피_고가대비;
            this.닥_현재가 = 닥_현재가;
            this.닥_등락률 = 닥_등락률;
            this.닥_저가대비 = 닥_저가대비;
            this.닥_고가대비 = 닥_고가대비;

            this.피_외인 = 피_외인;
            this.피_기관 = 피_기관;
            this.피_프로그램 = 피_프로그램;

            this.닥_외인 = 닥_외인;
            this.닥_기관 = 닥_기관;
            this.닥_프로그램 = 닥_프로그램;

            this.신규_A = 신규_A;
            this.신규_B = 신규_B;
            this.신규_C = 신규_C;
            this.기간_A = 기간_A;
            this.기간_B = 기간_B;
            this.기간_C = 기간_C;
            this.기간_D = 기간_D;
            this.기간_E = 기간_E;
            this.기간_F = 기간_F;

            this.반복_A = 반복_A;
            this.반복_B = 반복_B;
            this.반복_C = 반복_C;
            this.반복_D = 반복_D;
            this.반복_E = 반복_E;
            this.반복_F = 반복_F;
            this.반복_G = 반복_G;
            this.반복_H = 반복_H;
            this.반복_I = 반복_I;
            this.반복_J = 반복_J;
            this.반복_K = 반복_K;
            this.반복_L = 반복_L;
            this.반복_M = 반복_M;
            this.반복_N = 반복_N;

            this.리밸_A = 리밸_A;
            this.리밸_B = 리밸_B;
            this.리밸_C = 리밸_C;
            this.리밸_D = 리밸_D;
            this.리밸_E = 리밸_E;
            this.리밸_F = 리밸_F;
            this.리밸_G = 리밸_G;

            this.청산_A = 청산_A;
            this.청산_B = 청산_B;
            this.청산_C = 청산_C;

            this.매입비 = 매입비;

            this.피_누적거래대금 = 피_누적거래대금;
            this.피_상한종목수 = 피_상한종목수;
            this.피_상승종목수 = 피_상승종목수;
            this.피_보합종목수 = 피_보합종목수;
            this.피_하한종목수 = 피_하한종목수;
            this.피_하락종목수 = 피_하락종목수;
            this.닥_누적거래대금 = 닥_누적거래대금;
            this.닥_상한종목수 = 닥_상한종목수;
            this.닥_상승종목수 = 닥_상승종목수;
            this.닥_보합종목수 = 닥_보합종목수;
            this.닥_하한종목수 = 닥_하한종목수;
            this.닥_하락종목수 = 닥_하락종목수;
        }
    }

    public class Stockbalance
    {
        public string today { get; set; }
        public bool 매수제한 { get; set; }
        public bool 전량매도 { get; set; }
        public int 추매딜레이 { get; set; }
        public bool 선택 { get; set; }
        public int 매매그룹 { get; set; }
        public string 시장 { get; set; }
        public string 종목명 { get; set; }
        public string 종목코드 { get; set; }
        public int 현재가 { get; set; }
        public double 등락율 { get; set; }
        public int 평균단가 { get; set; }
        public int 보유수량 { get; set; }
        public long 매입금액 { get; set; }
        public long 평가금액 { get; set; }
        public long 평가손익 { get; set; }
        public long 금일수익금 { get; set; }
        public long 누적손익 { get; set; }
        public long 예상손익 { get; set; }
        public double 수익률 { get; set; }
        public double 최고수익률 { get; set; }
        public double 최저수익률 { get; set; }
        public double 최고예상손익금 { get; set; }
        public double 최저예상손익금 { get; set; }
        public long 금일매수금 { get; set; }
        public long 금일매도금 { get; set; }
        public int 매수횟수 { get; set; }
        public int 매도횟수 { get; set; }
        public DateTime 초기매수일 { get; set; }
        public string 추가매수일 { get; set; }
        public string 거래일 { get; set; }
        public string 초기매수검색식 { get; set; }
        public double 보유비중 { get; set; }
        public int 전일매수량 { get; set; }
        public int 전일매도량 { get; set; }
        public int 재매수 { get; set; }
        public string 매수일 { get; set; }
        public string 매도일 { get; set; }
        public string 매수주문번호 { get; set; }
        public string 매도주문번호 { get; set; }
        public int 주문가능수량 { get; set; }
        public bool 익절A { get; set; }
        public bool 익절B { get; set; }
        public bool 익절C { get; set; }
        public bool 익절D { get; set; }
        public bool 익절E { get; set; }
        public bool 익절F { get; set; }
        public bool 익절G { get; set; }
        public bool 익절H { get; set; }
        public bool 익절I { get; set; }
        public string 보전A { get; set; }
        public string 보전B { get; set; }
        public string 보전C { get; set; }
        public string 보전D { get; set; }
        public string 보전E { get; set; }
        public string 보전F { get; set; }
        public string 보전G { get; set; }
        public string 보전H { get; set; }
        public string 보전I { get; set; }
        public string 일회A { get; set; }
        public string 일회B { get; set; }
        public string 일회C { get; set; }
        public string 일회D { get; set; }
        public string 일회E { get; set; }
        public string 일회F { get; set; }
        public string 일회G { get; set; }
        public string 일회H { get; set; }
        public string 일회I { get; set; }
        public bool 손절A { get; set; }
        public bool 손절B { get; set; }
        public bool 손절C { get; set; }
        public bool 손절D { get; set; }
        public bool 손절E { get; set; }
        public bool 손절F { get; set; }
        public string 반복A { get; set; }
        public string 반복B { get; set; }
        public string 반복C { get; set; }
        public string 반복D { get; set; }
        public string 반복E { get; set; }
        public string 반복F { get; set; }
        public string 반복G { get; set; }
        public string 반복H { get; set; }
        public string 반복I { get; set; }
        public string 반복J { get; set; }
        public string 반복K { get; set; }
        public string 반복L { get; set; }
        public string 반복M { get; set; }
        public string 반복N { get; set; }
        public int 세금 { get; set; }
        public int 수수료 { get; set; }
        public int 매도기준 { get; set; }
        public bool 매도기준update { get; set; }
        public string 종목상태 { get; set; }
        public bool 매매가능 { get; set; }

        public bool 그룹지정_A { get; set; }
        public bool 그룹지정_B { get; set; }
        public bool 그룹지정_C { get; set; }
        public bool 그룹지정_D { get; set; }

        public string 리벨A { get; set; }
        public string 리벨B { get; set; }
        public string 리벨C { get; set; }
        public string 리벨D { get; set; }
        public string 리벨E { get; set; }
        public string 리벨F { get; set; }
        public string 리벨G { get; set; }
        public string 청산A { get; set; }
        public string 청산B { get; set; }
        public string 청산C { get; set; }
        public bool TS_1 { get; set; }
        public bool TS_2 { get; set; }
        public bool TS_3 { get; set; }
        public bool TS_4 { get; set; }
        public bool TS_5 { get; set; }
        public bool TS_6 { get; set; }
        public bool TS_7 { get; set; }
        public bool TS_8 { get; set; }
        public bool TS_9 { get; set; }
        public string 트레일링값 { get; set; }
        public int 시작가격 { get; set; }
        public double 시작수익률 { get; set; }
        public int 기준가격 { get; set; }
        public double 기준수익률 { get; set; }
        public bool 가동_반복A { get; set; }
        public bool 가동_반복B { get; set; }
        public bool 가동_반복C { get; set; }
        public bool 가동_반복D { get; set; }
        public bool 가동_반복E { get; set; }
        public bool 가동_반복F { get; set; }
        public bool 가동_반복G { get; set; }
        public bool 가동_반복H { get; set; }
        public bool 가동_반복I { get; set; }
        public bool 가동_반복J { get; set; }
        public bool 가동_반복K { get; set; }
        public bool 가동_반복L { get; set; }
        public bool 가동_반복M { get; set; }
        public bool 가동_반복N { get; set; }
        public bool 가동_리밸A { get; set; }
        public bool 가동_리밸B { get; set; }
        public bool 가동_리밸C { get; set; }
        public bool 가동_리밸D { get; set; }
        public bool 가동_리밸E { get; set; }
        public bool 가동_리밸F { get; set; }
        public bool 가동_리밸G { get; set; }
        public bool 가동_청산A { get; set; }
        public bool 가동_청산B { get; set; }
        public bool 가동_청산C { get; set; }
        public int 시간청산반복_A { get; set; }
        public int 시간청산반복_B { get; set; }
        public int 시간청산반복_C { get; set; }
        public bool 추매정지 { get; set; }
        public bool 잔고청산 { get; set; }
        public bool TimeSell_매입금_A { get; set; }
        public bool TimeSell_매입금_B { get; set; }
        public bool TimeSell_매입금_C { get; set; }
        public bool 잔고청산_매입금_A { get; set; }
        public bool 잔고청산_매입금_B { get; set; }
        public bool 잔고청산_매입금_C { get; set; }


        public bool repeat_매입금_A { get; set; }
        public bool repeat_매입금_B { get; set; }
        public bool repeat_매입금_C { get; set; }
        public bool repeat_매입금_D { get; set; }
        public bool repeat_매입금_E { get; set; }
        public bool repeat_매입금_F { get; set; }
        public bool repeat_매입금_G { get; set; }
        public bool repeat_매입금_H { get; set; }
        public bool repeat_매입금_I { get; set; }
        public bool repeat_매입금_J { get; set; }
        public bool repeat_매입금_K { get; set; }
        public bool repeat_매입금_L { get; set; }
        public bool repeat_매입금_M { get; set; }
        public bool repeat_매입금_N { get; set; }
        public bool rebalance_매입금_A { get; set; }
        public bool rebalance_매입금_B { get; set; }
        public bool rebalance_매입금_C { get; set; }
        public bool rebalance_매입금_D { get; set; }
        public bool rebalance_매입금_E { get; set; }
        public bool rebalance_매입금_F { get; set; }
        public bool rebalance_매입금_G { get; set; }


        public string 시간청산동작_A { get; set; }
        public string 시간청산동작_B { get; set; }
        public string 시간청산동작_C { get; set; }
        public bool 잔고청산_A { get; set; }
        public bool 잔고청산_B { get; set; }
        public bool 잔고청산_C { get; set; }
        public bool 매도정지 { get; set; }
        public string 분_리스트 { get; set; }
        public string 일_리스트 { get; set; }
        public bool TS_알림_A { get; set; }
        public bool TS_알림_B { get; set; }
        public bool TS_알림_C { get; set; }
        public bool TS_알림_D { get; set; }
        public bool TS_알림_E { get; set; }
        public bool TS_알림_F { get; set; }
        public bool TS_알림_G { get; set; }
        public bool TS_알림_H { get; set; }
        public bool TS_알림_I { get; set; }

        public int 잔고청산_TS_high_A { get; set; }
        public int 잔고청산_TS_high_B { get; set; }
        public int 잔고청산_TS_high_C { get; set; }

        public int 매매기간_TS_high_A { get; set; }
        public int 매매기간_TS_high_B { get; set; }
        public int 매매기간_TS_high_C { get; set; }
        public int 매매기간_TS_high_D { get; set; }
        public int 매매기간_TS_high_E { get; set; }
        public int 매매기간_TS_high_F { get; set; }

        public Stockbalance(string today, bool 매수제한, bool 전량매도, int 추매딜레이, bool 선택, int 매매그룹, string 시장, string 종목명, string 종목코드, int 현재가, double 등락율, int 평균단가,
                            int 보유수량, long 매입금액, long 평가금액, long 평가손익, long 금일수익금, long 누적손익, long 예상손익, double 수익률, double 최고수익률, double 최저수익률, double 최고예상손익금, double 최저예상손익금, long 금일매수금, long 금일매도금,
                            int 매수횟수, int 매도횟수, DateTime 초기매수일, string 거래일, string 초기매수검색식, double 보유비중,
                            int 전일매수량, int 전일매도량, int 재매수, string 추가매수일, string 매수일, string 매도일, int 주문가능수량, string 매수주문번호, string 매도주문번호,
                             bool 익절A, bool 익절B, bool 익절C, bool 익절D, bool 익절E, bool 익절F, bool 익절G, bool 익절H, bool 익절I, string 보전A, string 보전B, string 보전C, string 보전D, string 보전E, string 보전F, string 보전G, string 보전H, string 보전I,
                            string 일회A, string 일회B, string 일회C, string 일회D, string 일회E, string 일회F, string 일회G, string 일회H, string 일회I,
                             bool 손절A, bool 손절B, bool 손절C, bool 손절D, bool 손절E, bool 손절F,
                            string 반복A, string 반복B, string 반복C, string 반복D, string 반복E, string 반복F, string 반복G, string 반복H, string 반복I, string 반복J, string 반복K, string 반복L, string 반복M, string 반복N, int 세금, int 수수료, int 매도기준, bool 매도기준update, string 종목상태,
                            bool 매매가능, bool 그룹지정_A, bool 그룹지정_B, bool 그룹지정_C, bool 그룹지정_D, string 리벨A, string 리벨B, string 리벨C, string 리벨D, string 리벨E, string 리벨F, string 리벨G, string 청산A, string 청산B, string 청산C,
                            bool TS_1, bool TS_2, bool TS_3, bool TS_4, bool TS_5, bool TS_6, bool TS_7, bool TS_8, bool TS_9, string 트레일링값, int 시작가격, double 시작수익률, int 기준가격, double 기준수익률,
                            bool 가동_반복A, bool 가동_반복B, bool 가동_반복C, bool 가동_반복D, bool 가동_반복E, bool 가동_반복F, bool 가동_반복G, bool 가동_반복H, bool 가동_반복I, bool 가동_반복J, bool 가동_반복K, bool 가동_반복L, bool 가동_반복M, bool 가동_반복N, bool 가동_리밸A, bool 가동_리밸B, bool 가동_리밸C, bool 가동_리밸D, bool 가동_리밸E, bool 가동_리밸F, bool 가동_리밸G, bool 가동_청산A, bool 가동_청산B, bool 가동_청산C,
                            int 시간청산반복_A, int 시간청산반복_B, int 시간청산반복_C, bool 추매정지, bool 잔고청산, bool TimeSell_매입금_A, bool TimeSell_매입금_B, bool TimeSell_매입금_C, bool 잔고청산_매입금_A, bool 잔고청산_매입금_B, bool 잔고청산_매입금_C,
                            bool repeat_매입금_A, bool repeat_매입금_B, bool repeat_매입금_C, bool repeat_매입금_D, bool repeat_매입금_E, bool repeat_매입금_F, bool repeat_매입금_G, bool repeat_매입금_H, bool repeat_매입금_I, bool repeat_매입금_J, bool repeat_매입금_K, bool repeat_매입금_L, bool repeat_매입금_M, bool repeat_매입금_N,
                            bool rebalance_매입금_A, bool rebalance_매입금_B, bool rebalance_매입금_C, bool rebalance_매입금_D, bool rebalance_매입금_E, bool rebalance_매입금_F, bool rebalance_매입금_G, string 시간청산동작_A, string 시간청산동작_B, string 시간청산동작_C, bool 잔고청산_A, bool 잔고청산_B, bool 잔고청산_C, bool 매도정지, string 분_리스트, string 일_리스트,
                            bool TS_알림_A, bool TS_알림_B, bool TS_알림_C, bool TS_알림_D, bool TS_알림_E, bool TS_알림_F, bool TS_알림_G, bool TS_알림_H, bool TS_알림_I,
                            int 잔고청산_TS_high_A, int 잔고청산_TS_high_B, int 잔고청산_TS_high_C,
                            int 매매기간_TS_high_A, int 매매기간_TS_high_B, int 매매기간_TS_high_C, int 매매기간_TS_high_D, int 매매기간_TS_high_E, int 매매기간_TS_high_F

            )
        {
            this.today = today;
            this.매수제한 = 매수제한;
            this.전량매도 = 전량매도;
            this.추매딜레이 = 추매딜레이;
            this.선택 = 선택;
            this.매매그룹 = 매매그룹;
            this.시장 = 시장;
            this.종목명 = 종목명;
            this.종목코드 = 종목코드;
            this.현재가 = 현재가;
            this.등락율 = 등락율;
            this.평균단가 = 평균단가;
            this.보유수량 = 보유수량;
            this.매입금액 = 매입금액;
            this.평가금액 = 평가금액;
            this.평가손익 = 평가손익;
            this.금일수익금 = 금일수익금;
            this.누적손익 = 누적손익;
            this.예상손익 = 예상손익;
            this.수익률 = 수익률;
            this.최고수익률 = 최고수익률;
            this.최저수익률 = 최저수익률;
            this.최고예상손익금 = 최고예상손익금;
            this.최저예상손익금 = 최저예상손익금;
            this.금일매수금 = 금일매수금;
            this.금일매도금 = 금일매도금;
            this.매수횟수 = 매수횟수;
            this.매도횟수 = 매도횟수;
            this.초기매수일 = 초기매수일;
            this.추가매수일 = 추가매수일;
            this.거래일 = 거래일;
            this.초기매수검색식 = 초기매수검색식;
            this.보유비중 = 보유비중;
            this.전일매수량 = 전일매수량;
            this.전일매도량 = 전일매도량;
            this.재매수 = 재매수;
            this.매수일 = 매수일;
            this.매도일 = 매도일;
            this.주문가능수량 = 주문가능수량;
            this.매수주문번호 = 매수주문번호;
            this.매도주문번호 = 매도주문번호;
            this.익절A = 익절A;
            this.익절B = 익절B;
            this.익절C = 익절C;
            this.익절D = 익절D;
            this.익절E = 익절E;
            this.익절F = 익절F;
            this.익절G = 익절G;
            this.익절H = 익절H;
            this.익절I = 익절I;
            this.보전A = 보전A;
            this.보전B = 보전B;
            this.보전C = 보전C;
            this.보전D = 보전D;
            this.보전E = 보전E;
            this.보전F = 보전F;
            this.보전G = 보전G;
            this.보전H = 보전H;
            this.보전I = 보전I;

            this.일회A = 일회A;
            this.일회B = 일회B;
            this.일회C = 일회C;
            this.일회D = 일회D;
            this.일회E = 일회E;
            this.일회F = 일회F;
            this.일회G = 일회G;
            this.일회H = 일회H;
            this.일회I = 일회I;
            this.손절A = 손절A;
            this.손절B = 손절B;
            this.손절C = 손절C;
            this.손절D = 손절D;
            this.손절E = 손절E;
            this.손절F = 손절F;
            this.반복A = 반복A;
            this.반복B = 반복B;
            this.반복C = 반복C;
            this.반복D = 반복D;
            this.반복E = 반복E;
            this.반복F = 반복F;
            this.반복G = 반복G;
            this.반복H = 반복H;
            this.반복I = 반복I;
            this.반복J = 반복J;
            this.반복K = 반복K;
            this.반복L = 반복L;
            this.반복M = 반복M;
            this.반복N = 반복N;

            this.세금 = 세금;
            this.수수료 = 수수료;
            this.매도기준 = 매도기준;
            this.매도기준update = 매도기준update;
            this.종목상태 = 종목상태;
            this.매매가능 = 매매가능;

            this.그룹지정_A = 그룹지정_A;
            this.그룹지정_B = 그룹지정_B;
            this.그룹지정_C = 그룹지정_C;
            this.그룹지정_D = 그룹지정_D;

            this.리벨A = 리벨A;
            this.리벨B = 리벨B;
            this.리벨C = 리벨C;
            this.리벨D = 리벨D;
            this.리벨E = 리벨E;
            this.리벨F = 리벨F;
            this.리벨G = 리벨G;
            this.청산A = 청산A;
            this.청산B = 청산B;
            this.청산C = 청산C;
            this.TS_1 = TS_1;
            this.TS_2 = TS_2;
            this.TS_3 = TS_3;
            this.TS_4 = TS_4;
            this.TS_5 = TS_5;
            this.TS_6 = TS_6;
            this.TS_7 = TS_7;
            this.TS_8 = TS_8;
            this.TS_9 = TS_9;
            this.트레일링값 = 트레일링값;

            this.시작가격 = 시작가격;
            this.시작수익률 = 시작수익률;
            this.기준가격 = 기준가격;
            this.기준수익률 = 기준수익률;

            this.가동_반복A = 가동_반복A;
            this.가동_반복B = 가동_반복B;
            this.가동_반복C = 가동_반복C;
            this.가동_반복D = 가동_반복D;
            this.가동_반복E = 가동_반복E;
            this.가동_반복F = 가동_반복F;
            this.가동_반복G = 가동_반복G;
            this.가동_반복H = 가동_반복H;
            this.가동_반복I = 가동_반복I;
            this.가동_반복J = 가동_반복J;
            this.가동_반복K = 가동_반복K;
            this.가동_반복L = 가동_반복L;
            this.가동_반복M = 가동_반복M;
            this.가동_반복N = 가동_반복N;
            this.가동_리밸A = 가동_리밸A;
            this.가동_리밸B = 가동_리밸B;
            this.가동_리밸C = 가동_리밸C;
            this.가동_리밸D = 가동_리밸D;
            this.가동_리밸E = 가동_리밸E;
            this.가동_리밸F = 가동_리밸F;
            this.가동_리밸G = 가동_리밸G;
            this.가동_청산A = 가동_청산A;
            this.가동_청산B = 가동_청산B;
            this.가동_청산C = 가동_청산C;

            this.시간청산반복_A = 시간청산반복_A;
            this.시간청산반복_B = 시간청산반복_B;
            this.시간청산반복_C = 시간청산반복_C;

            this.추매정지 = 추매정지;
            this.잔고청산 = 잔고청산;

            this.TimeSell_매입금_A = TimeSell_매입금_A;
            this.TimeSell_매입금_B = TimeSell_매입금_B;
            this.TimeSell_매입금_C = TimeSell_매입금_C;

            this.잔고청산_매입금_A = 잔고청산_매입금_A;
            this.잔고청산_매입금_B = 잔고청산_매입금_B;
            this.잔고청산_매입금_C = 잔고청산_매입금_C;

            this.repeat_매입금_A = repeat_매입금_A;
            this.repeat_매입금_B = repeat_매입금_B;
            this.repeat_매입금_C = repeat_매입금_C;
            this.repeat_매입금_D = repeat_매입금_D;
            this.repeat_매입금_E = repeat_매입금_E;
            this.repeat_매입금_F = repeat_매입금_F;
            this.repeat_매입금_G = repeat_매입금_G;
            this.repeat_매입금_H = repeat_매입금_H;
            this.repeat_매입금_I = repeat_매입금_I;
            this.repeat_매입금_J = repeat_매입금_J;
            this.repeat_매입금_K = repeat_매입금_K;
            this.repeat_매입금_L = repeat_매입금_L;
            this.repeat_매입금_M = repeat_매입금_M;
            this.repeat_매입금_N = repeat_매입금_N;

            this.rebalance_매입금_A = rebalance_매입금_A;
            this.rebalance_매입금_B = rebalance_매입금_B;
            this.rebalance_매입금_C = rebalance_매입금_C;
            this.rebalance_매입금_D = rebalance_매입금_D;
            this.rebalance_매입금_E = rebalance_매입금_E;
            this.rebalance_매입금_F = rebalance_매입금_F;
            this.rebalance_매입금_G = rebalance_매입금_G;

            this.시간청산동작_A = 시간청산동작_A;
            this.시간청산동작_B = 시간청산동작_B;
            this.시간청산동작_C = 시간청산동작_C;

            this.잔고청산_A = 잔고청산_A;
            this.잔고청산_B = 잔고청산_B;
            this.잔고청산_C = 잔고청산_C;

            this.매도정지 = 매도정지;
            this.분_리스트 = 분_리스트;
            this.일_리스트 = 일_리스트;

            this.TS_알림_A = TS_알림_A;
            this.TS_알림_B = TS_알림_B;
            this.TS_알림_C = TS_알림_C;
            this.TS_알림_D = TS_알림_D;
            this.TS_알림_E = TS_알림_E;
            this.TS_알림_F = TS_알림_F;
            this.TS_알림_G = TS_알림_G;
            this.TS_알림_H = TS_알림_H;
            this.TS_알림_I = TS_알림_I;

            this.잔고청산_TS_high_A = 잔고청산_TS_high_A;
            this.잔고청산_TS_high_B = 잔고청산_TS_high_B;
            this.잔고청산_TS_high_C = 잔고청산_TS_high_C;
            this.매매기간_TS_high_A = 매매기간_TS_high_A;
            this.매매기간_TS_high_B = 매매기간_TS_high_B;
            this.매매기간_TS_high_C = 매매기간_TS_high_C;
            this.매매기간_TS_high_D = 매매기간_TS_high_D;
            this.매매기간_TS_high_E = 매매기간_TS_high_E;
            this.매매기간_TS_high_F = 매매기간_TS_high_F;
        }
    }















}
