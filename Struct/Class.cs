using System;

namespace 지니64
{
    
    public class 신규조회
    {
        public string itemcode { get; set; }
        public string where { get; set; }
        public int timer { get; set; }
        public string 검색식 { get; set; }
        public 신규조회(string itemcode, string where, int timer, string 검색식)
        {
            this.itemcode = itemcode;
            this.where = where;
            this.timer = timer;
            this.검색식 = 검색식;
        }
    }

    public class 재매수
    {
        public string Itemcode { get; set; }
        public string ItemName { get; set; }
        public string 결과 { get; set; }
        public 재매수(string Itemcode, string ItemName, string 결과)
        {
            this.Itemcode = Itemcode;
            this.ItemName = ItemName;
            this.결과 = 결과;
        }
    }


    public class 보정
    {
        public string 코드 { get; set; }
        public string 이름 { get; set; }
        public bool 요청 { get; set; }
        public 보정(string 코드, string 이름, bool 요청)
        {
            this.코드 = 코드;
            this.이름 = 이름;
            this.요청 = 요청;
        }
    }


    public class 최종매입가
    {
        public string 종목명 { get; set; } = "";
        public string 종목코드 { get; set; } = "";
        public string 위치 { get; set; } = "";
        public int 번호 { get; set; }
        public int 매입가 { get; set; }

        // [1] JSON 로딩을 위한 빈 생성자 (필수)
        public 최종매입가() { }
    }


    public class 체결
    {
        public string con_num { get; set; }
        public string 주문N체결시간 { get; set; }
        public string 종목명 { get; set; }
        public string 체결검색식 { get; set; }
        public string 주문 { get; set; }
        public string 거래구분 { get; set; }
        public string 수익률 { get; set; }
        public string 체결가 { get; set; }
        public string 주문수량 { get; set; }
        public string 체결량 { get; set; }
        public string 현재가 { get; set; }
        public string 등락률 { get; set; }
        public string 주문번호 { get; set; }
        public string 종목코드 { get; set; }
        public 체결(string con_num, string 주문N체결시간, string 종목명, string 체결검색식, string 주문, string 거래구분, string 수익률, string 체결가, string 주문수량, string 체결량, string 현재가, string 등락률, string 주문번호, string 종목코드)
        {
            this.con_num = con_num;
            this.주문N체결시간 = 주문N체결시간;
            this.종목명 = 종목명;
            this.체결검색식 = 체결검색식;
            this.주문 = 주문;
            this.거래구분 = 거래구분;
            this.수익률 = 수익률;
            this.체결가 = 체결가;
            this.주문수량 = 주문수량;
            this.체결량 = 체결량;
            this.현재가 = 현재가;
            this.등락률 = 등락률;
            this.주문번호 = 주문번호;
            this.종목코드 = 종목코드;
        }
    }

    public class 주문예약
    {
        public string 등록일 { get; set; }
        public int 예약번호 { get; set; }
        public string 스크린번호 { get; set; }
        public int 매수매도 { get; set; }
        public string 종목코드 { get; set; }
        public string 종목명 { get; set; }
        public double 주문비 { get; set; } // 주문비율 변경
        public double 비중 { get; set; }
        public int 선택 { get; set; }
        public int 주문가 { get; set; }
        public int 주문수량 { get; set; }
        public int 체결수량 { get; set; }
        public string 검색식 { get; set; }
        public string 주문번호 { get; set; }
        public bool 등록 { get; set; }
        public bool 연동 { get; set; }
        public bool 체결완료삭제 { get; set; }
        public bool 전량매도삭제 { get; set; }

        public 주문예약(string 등록일, int 예약번호, string 스크린번호, int 매수매도, string 종목코드, string 종목명, double 주문비, double 비중, int 선택, int 주문가, int 주문수량, int 체결수량, string 검색식, string 주문번호, bool 등록, bool 연동, bool 체결완료삭제, bool 전량매도삭제)
        {
            this.등록일 = 등록일;
            this.예약번호 = 예약번호;
            this.스크린번호 = 스크린번호;
            this.매수매도 = 매수매도;
            this.종목코드 = 종목코드;
            this.종목명 = 종목명;
            this.주문비 = 주문비;
            this.비중 = 비중;
            this.선택 = 선택;
            this.주문가 = 주문가;
            this.주문수량 = 주문수량;
            this.체결수량 = 체결수량;
            this.검색식 = 검색식;
            this.주문번호 = 주문번호;
            this.등록 = 등록;
            this.연동 = 연동;
            this.체결완료삭제 = 체결완료삭제;
            this.전량매도삭제 = 전량매도삭제;
        }
    }

    public class Newstock
    {
        public string Pos { get; set; }
        public string condition { get; set; }
        public string code { get; set; }
        public string state { get; set; }
        public int timer { get; set; }
        public DateTime CatchTime { get; set; }

        public Newstock(string Pos, string condition, string code, string state, int timer, DateTime CatchTime)
        {
            this.Pos = Pos;
            this.condition = condition;
            this.code = code;
            this.state = state;
            this.timer = timer;
            this.CatchTime = CatchTime;
        }
    }

    public class ABC
    {
        public string Code { get; set; }
        public string Loc { get; set; }

        public ABC(string Code, string Loc)
        {
            this.Code = Code;
            this.Loc = Loc;
        }
    }

    public class Catch_stock
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Catch_stock(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }

    public class Rebal_Sell
    {
        public string 위치 { get; set; }
        public string 종목코드 { get; set; }
        public string Screennum { get; set; }
        public string 주문번호 { get; set; }
        public int _1차수량 { get; set; }
        public int _2차수량 { get; set; }
        public int 체결가격 { get; set; }
        public int 수익구분 { get; set; }

        public Rebal_Sell(string 위치, string 종목코드, string Screennum, string 주문번호, int _1차수량, int _2차수량, int 체결가격, int 수익구분)
        {
            this.위치 = 위치;
            this.종목코드 = 종목코드;
            this.Screennum = Screennum;
            this.주문번호 = 주문번호;
            this._1차수량 = _1차수량;
            this._2차수량 = _2차수량;
            this.체결가격 = 체결가격;
            this.체결가격 = 체결가격;
            this.수익구분 = 수익구분;
        }
    }

    public class Scalping
    {
        public string 검색식 { get; set; }
        public string 코드 { get; set; }
        public string Screennum { get; set; }
        public int 수량A { get; set; }
        public int 수량B { get; set; }
        public int 수량C { get; set; }
        public int 수량D { get; set; }
        public int 수량E { get; set; }
        public int 수량F { get; set; }
        public int 수량G { get; set; }
        public int 수량H { get; set; }
        public int 수량I { get; set; }

        public Scalping(string 검색식, string 코드, string Screennum, int 수량A, int 수량B, int 수량C, int 수량D, int 수량E, int 수량F, int 수량G, int 수량H, int 수량I)
        {
            this.검색식 = 검색식;
            this.코드 = 코드;
            this.Screennum = Screennum;
            this.수량A = 수량A;
            this.수량B = 수량B;
            this.수량C = 수량C;
            this.수량D = 수량D;
            this.수량E = 수량E;
            this.수량F = 수량F;
            this.수량G = 수량G;
            this.수량H = 수량H;
            this.수량I = 수량I;
        }
    }

    public class Interest_stock
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string AddedDate { get; set; }
        public string Title { get; set; }
        public bool 시세등록 { get; set; }

        public Interest_stock () {}
    }

    public class Trading_item
    {
        // [C# 7.0+] Auto-Property Initializer: 기본값 안전 설정 (Null 방지)
        public bool IsActive { get; set; } = false;
        public string Code { get; set; } = string.Empty;     // null 대신 빈 문자열
        public string Location { get; set; } = string.Empty; // null 대신 빈 문자열
        public int Timer { get; set; } = 0;

        // 1. 기본 생성자
        // (풀링 시스템에서 빈 객체를 미리 만들어둘 때 필요합니다)
        public Trading_item() { }

        // 2. 값 주입 생성자 (신규 생성 시 편의용)
        // 내부적으로 Initialize를 호출하여 코드를 통일합니다.
        public Trading_item(string code, string location, int timer)
        {
            Initialize(code, location, timer);
        }

        // 3. [★핵심] 재사용 초기화 메서드
        // 대기열에서 꺼낸 '헌 객체'를 '새 객체'처럼 값을 채워주는 함수입니다.
        public void Initialize(string code, string location, int timer)
        {
            this.Code = code;
            this.Location = location;
            this.Timer = timer;
            this.IsActive = true; // 값을 채웠으니 자동으로 활성화
        }
    }

    public class 감시주문
    {
        public string 종목명 { get; set; }
        public string 종목코드 { get; set; }
        public int 주문수량 { get; set; }
        public int 주문체결가격 { get; set; }
        public int 감시주문가격 { get; set; }
        public int 손절주문가격 { get; set; }
        public double 감시_주문값 { get; set; }
        public int 감시_시장가구분 { get; set; }
        public string 원주문번호 { get; set; }
        public string 검색식 { get; set; }
        public string 감시일 { get; set; }
        public string 주문일 { get; set; }
        public int 감시번호 { get; set; }
        public int 연동감시번호 { get; set; }
        public string 주문시간 { get; set; }
        public bool 단위_기준 { get; set; }
        public double 차수주문값 { get; set; }
        public int 차수기준 { get; set; }
        public int 취소시간 { get; set; }
        public int 수익구분 { get; set; }
        public int 최종번호 { get; set; }
        public string 리밸매도기준 { get; set; }
        public bool TS { get; set; }
        public int TS_high { get; set; }
        public double TS_down { get; set; }
        public int TS_이평 { get; set; }
        public int CBB_TS_이평 { get; set; }

        // 생성자는 이거 하나만 있으면 돼!
        public 감시주문() { }
    }


    public class 재주문
    {
        public string Item { get; set; }
        public int timer { get; set; }

        public 재주문(string Item, int timer)
        {
            this.Item = Item;
            this.timer = timer;
        }
    }

    public class 검색이탈
    {
        // 타이머(int) 대신 종료 예정 시간(DateTime) 사용 -> CPU 연산 제거
        public DateTime ExpireTime { get; set; }

        public string 코드_검색식 { get; set; }
        public bool 신규 { get; set; }

        // [최적화] 자주 쓰는 값 미리 파싱해둠 (Split 비용 제거)
        public string 종목코드 { get; set; }
        public string 검색식 { get; set; }

        public 검색이탈(int durationSeconds, string key, bool isNew)
        {
            this.ExpireTime = DateTime.Now.AddSeconds(durationSeconds);
            this.코드_검색식 = key;
            this.신규 = isNew;

            // 생성 시 1회만 Split 수행
            var parts = key.Split('^');
            this.종목코드 = parts.Length > 0 ? parts[0] : "";
            this.검색식 = parts.Length > 1 ? parts[1] : "";
        }
    }

    public class Watch
    {
        public string Market { get; set; }
        public string Condition { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public double 등락율 { get; set; }
        public string Code { get; set; }
        public double Nowprice { get; set; }
        public double LastPrice { get; set; }
        public double 진입후등락율 { get; set; }
        public double 진입후최고 { get; set; }
        public double 진입후최저 { get; set; }
        public string 누적거래량 { get; set; }
        public double 누적거래대금 { get; set; }
        public int 시가 { get; set; }
        public int 고가 { get; set; }
        public int 저가 { get; set; }
        public double 거래대금증감 { get; set; }
        public string 전일거래량대비 { get; set; }
        public string 거래회전율 { get; set; }
        public string 시가총액 { get; set; }
        public double 진입가 { get; set; }
        public string 진입시간 { get; set; }
        public int timer { get; set; }
        public string 매매 { get; set; }
        public int 매수가 { get; set; }
        public double 수익률 { get; set; }


        public Watch(string Market, string Condition, string State, string Name, double 등락율, string Code, double Nowprice, double LastPrice, double 진입후등락율, double 진입후최고, double 진입후최저, string 누적거래량,
            double 누적거래대금, int 시가, int 고가, int 저가, double 거래대금증감, string 전일거래량대비, string 거래회전율, string 시가총액, double 진입가, string 진입시간, int timer, string 매매, int 매수가, double 수익률)
        {
            this.Market = Market;
            this.Condition = Condition;
            this.State = State;
            this.Name = Name;
            this.등락율 = 등락율;
            this.Code = Code;
            this.Nowprice = Nowprice;
            this.LastPrice = LastPrice;
            this.진입후등락율 = 진입후등락율;
            this.진입후최고 = 진입후최고;
            this.진입후최저 = 진입후최저;
            this.누적거래량 = 누적거래량;
            this.누적거래대금 = 누적거래대금;
            this.시가 = 시가;
            this.고가 = 고가;
            this.저가 = 저가;
            this.거래대금증감 = 거래대금증감;
            this.전일거래량대비 = 전일거래량대비;
            this.거래회전율 = 거래회전율;
            this.시가총액 = 시가총액;
            this.진입가 = 진입가;
            this.진입시간 = 진입시간;
            this.timer = timer;
            this.매매 = 매매;
            this.매수가 = 매수가;
            this.수익률 = 수익률;
        }
    }

}
