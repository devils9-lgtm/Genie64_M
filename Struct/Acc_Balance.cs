using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace 지니64
{
    public class Market_Item
    {
        // 💡 자주 쓰이는 기본값은 여기서 바로 세팅해 줍니다. (0이나 false는 안 적어도 알아서 들어갑니다)
        public bool 매수증거금알림 { get; set; } = true;
        public bool 매매알림_모투제한 { get; set; } = true;
        public bool 매매알림_상한가 { get; set; } = true;
        public bool 매매알림_하한가 { get; set; } = true;
        public bool 매매알림_vI매수 { get; set; } = true;
        public bool 매매알림_VI매도 { get; set; } = true;
        public string 과열 { get; set; } = "정상";

        // 기본값이 없는 애들은 그냥 선언만 해둡니다.
        public string 종목명 { get; set; }
        public string state { get; set; }
        public bool 신용가능 { get; set; }
        public double 증거금률 { get; set; }
        public string 종목코드 { get; set; }
        public string Market { get; set; }
        public int start_price { get; set; }
        public int 현재가 { get; set; }
        public long 누적거래량 { get; set; }
        public long 누적거래대금 { get; set; }
        public int Last_price { get; set; }
        public double 등락율 { get; set; }

        // ... 나머지 변수들도 동일하게 선언만 해둡니다. (0이나 false로 초기화되는 변수들)
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

        // ✂️ 기존의 길고 복잡했던 public Market_Item(...) 생성자는 삭제합니다!
    }


    public class Condition
    {
        public string index; //조건식 인덱스
        public string name; //조건식 이름

        public Condition(string _index, string _name)
        {
            index = _index;
            name = _name;
        }
    }

    public class RunCondition
    {
        public string index; //조건식 인덱스
        public string name; //조건식 이름
        public string where; //조건식 위치

        public RunCondition(string index, string name, string where)
        {
            this.index = index;
            this.name = name;
            this.where = where;
        }
    }

    public class 위치별검색식
    {
        public string 위치 { get; set; }
        public string 이름 { get; set; }
        public bool 중복여부 { get; set; }
        public bool 실행여부 { get; set; }
    }

    // [신용상세] - 대출일별 영수증 역할을 할 클래스
    public class 신용상세
    {
        // [기본 식별 정보]
        public string 일자 { get; set; }          // API: dt
        public string 종목코드 { get; set; }      // API: stk_cd
        public string 종목명 { get; set; }        // API: stk_nm
        public string 신용구분 { get; set; }      // API: crd_tp (00:현금, 03:신용 등)

        // [날짜 정보]
        public string 대출일 { get; set; }        // API: loan_dt
        public string 만기일 { get; set; }        // API: expr_dt

        // [가격 및 수량]
        public int 현재가 { get; set; }           // API: cur_prc
        public int 매입가 { get; set; }           // API: pur_pric
        public int 보유수량 { get; set; }         // API: rmnd_qty
        public int 청산가능수량 { get; set; }     // API: clrn_alow_qty
        public int 결제잔고 { get; set; }         // API: setl_remn

        // [금액 정보 (단위가 클 수 있으므로 long 권장)]
        public long 매입금액 { get; set; }        // API: pur_amt
        public long 신용금액 { get; set; }        // API: crd_amt (대출 원금)
        public long 신용이자 { get; set; }        // API: crd_int

        // [당일 매매 손익/비용]
        public long 당일매도손익 { get; set; }    // API: tdy_sel_pl
        public long 당일매매수수료 { get; set; }  // API: tdy_trde_cmsn
        public long 당일매매세금 { get; set; }    // API: tdy_trde_tax

        public long 평가손익 { get; set; }        // 개별 대출건의 순수익 (수수료,세금,이자 차감)
        public double 수익률 { get; set; }        // 개별 대출건의 수익률

        // =========================================================
        // 🚀 [추가 1] 대출순번 (LS증권 및 한투 상세 상환 시 필수)
        // =========================================================
        // 같은 날(대출일) 동일 종목을 여러 번 신용 매수했을 때 이를 구분하는 번호입니다.
        // - LS증권: 신용매도 주문 시 '대출일자'와 함께 '대출실행순번(crdt_exec_sqno)'을 요구합니다.
        // - 한국투자증권: 주문 시나 잔고 조회 시 순번(buy_seq 등)이 필요할 수 있습니다.
        public string 대출순번 { get; set; }

        // =========================================================
        // 🚀 [추가 2] 상환주문용 구분코드 (한국투자증권 필수)
        // =========================================================
        // 한투는 신용 매도 시 '주문대상잔고구분코드(ORD_OBJT_CBLC_DVSN_CD)'를 엄격히 따집니다.
        // 예: 25(자기융자상환), 27(유통융자상환), 12(주식담보대출상환)
        // 키움(03, 33)이나 LS와는 코드가 다르므로, 잔고 조회 시 받아온 원본 코드를 저장해둬야 
        // 매도(상환) 주문을 날릴 때 그대로 꺼내서 쓸 수 있습니다.
        public string 잔고구분코드 { get; set; }

        // =========================================================
        // 🚀 [추가 3] 실제 매도(상환) 가능 수량
        // =========================================================
        // 현재 '보유수량'이 10주라도, 이미 미체결 매도가 3주 걸려있다면 '매도가능수량'은 7주입니다.
        // 알고리즘 자동 매매 시 중복 주문(에러)을 막기 위해 낱건별 실 매도가능수량 추적이 필요합니다.
        public int 매도가능수량 { get; set; }
    }

    public class Stockbalance
    {
        public List<신용상세> 신용상세리스트 { get; set; } = new List<신용상세>();

        // 신용 보증금율 (한투/LS의 잔고평가에서 위험도 관리를 위해 사용)
        public double 신용_보증금율 { get; set; }

        // =========================================================
        // 🚀 [신규 추가] 신용 전용 상세 데이터 (요청하신 부분)
        // =========================================================
        public int 신용_보유수량 { get; set; }
        public int 신용_주문가능수량 { get; set; }
        public long 신용_매입금액 { get; set; }
        public long 신용_평가금액 { get; set; }
        public long 신용_이자합계 { get; set; }
        public long 신용_평가손익 { get; set; }
        public int 신용_평균단가 { get; set; }
        public double 신용_수익률 { get; set; }
        
        public string 증권사 { get; set; }
        public string Today { get; set; }
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
        public int 주문가능수량 { get; set; }

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

        public bool Repeat_매입금_A { get; set; }
        public bool Repeat_매입금_B { get; set; }
        public bool Repeat_매입금_C { get; set; }
        public bool Repeat_매입금_D { get; set; }
        public bool Repeat_매입금_E { get; set; }
        public bool Repeat_매입금_F { get; set; }
        public bool Repeat_매입금_G { get; set; }
        public bool Repeat_매입금_H { get; set; }
        public bool Repeat_매입금_I { get; set; }
        public bool Repeat_매입금_J { get; set; }
        public bool Repeat_매입금_K { get; set; }
        public bool Repeat_매입금_L { get; set; }
        public bool Repeat_매입금_M { get; set; }
        public bool Repeat_매입금_N { get; set; }
        public bool Rebalance_매입금_A { get; set; }
        public bool Rebalance_매입금_B { get; set; }
        public bool Rebalance_매입금_C { get; set; }
        public bool Rebalance_매입금_D { get; set; }
        public bool Rebalance_매입금_E { get; set; }
        public bool Rebalance_매입금_F { get; set; }
        public bool Rebalance_매입금_G { get; set; }

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

        public Stockbalance() { }
    }















}
