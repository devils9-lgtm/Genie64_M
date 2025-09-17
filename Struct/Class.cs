using System;

namespace 지니_64
{
    public class Order
    {
        public string screennum { get; set; }
        public int 주문유형 { get; set; }
        public string 종목코드 { get; set; }
        public int 주문수량 { get; set; }
        public int 주문가 { get; set; }
        public string 거래구분 { get; set; }
        public string 원주문번호 { get; set; }
        public string 검색식 { get; set; }
        public string 종목명 { get; set; }
        public int Order번호 { get; set; }
        public bool 서버전달 { get; set; }


        public Order(string screennum, int 주문유형, string 종목코드, int 주문수량, int 주문가, string 거래구분, string 원주문번호, string 검색식, string 종목명, int Order번호, bool 서버전달)
        {
            this.screennum = screennum;
            this.주문유형 = 주문유형;
            this.종목코드 = 종목코드;
            this.주문수량 = 주문수량;
            this.주문가 = 주문가;
            this.거래구분 = 거래구분;
            this.원주문번호 = 원주문번호;
            this.검색식 = 검색식;
            this.종목명 = 종목명;
            this.Order번호 = Order번호;
            this.서버전달 = 서버전달;
        }
    }

    public class 신규조회
    {
        public string ItemCode { get; set; }
        public string Para { get; set; }
        public int timer { get; set; }
        public string 검색식 { get; set; }
        public 신규조회(string ItemCode, string Para, int timer, string 검색식)
        {
            this.ItemCode = ItemCode;
            this.Para = Para;
            this.timer = timer;
            this.검색식 = 검색식;
        }
    }

    public class 재매수
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string 결과 { get; set; }
        public 재매수(string ItemCode, string ItemName, string 결과)
        {
            this.ItemCode = ItemCode;
            this.ItemName = ItemName;
            this.결과 = 결과;
        }
    }


    public class 보정
    {
        public string 코드 { get; set; }
        public string 이름 { get; set; }
        public int 시간 { get; set; }
        public 보정(string 코드, string 이름, int 시간)
        {
            this.코드 = 코드;
            this.이름 = 이름;
            this.시간 = 시간;
        }
    }


    public class 최종매입가
    {
        public string 종목코드 { get; set; }
        public string 위치 { get; set; }
        public int 번호 { get; set; }
        public int 매입가 { get; set; }
        public 최종매입가(string 종목코드, string 위치, int 번호, int 매입가)
        {
            this.종목코드 = 종목코드;
            this.위치 = 위치;
            this.번호 = 번호;
            this.매입가 = 매입가;
        }
    }


    public class JumunItem
    {
        public int 재주문timer { get; set; }
        public int 재주문count { get; set; }
        public string screennum { get; set; }
        public string 종목코드 { get; set; }
        public string 종목명 { get; set; }
        public string 주문번호 { get; set; }
        public string 원주문번호 { get; set; }
        public string 검색식 { get; set; }
        public double 주문값 { get; set; }
        public int 주문구분 { get; set; }
        public int 취소시간 { get; set; }
        public int 취소N주문 { get; set; }
        public int 반복횟수 { get; set; }
        public string 비고 { get; set; }
        public string location { get; set; }
        public int 주문수량 { get; set; }
        public int 주문가격 { get; set; }
        public int 주문유형 { get; set; }
        public double 비중 { get; set; }
        public int 비중단위 { get; set; }
        public int 취소timer { get; set; }
        public int 현재가 { get; set; }
        public double 등락률 { get; set; }
        public int 주문시간 { get; set; }
        public int 미체결량 { get; set; }
        public bool 주문취소 { get; set; }
        public bool 가동전 { get; set; }
        public int Order_count { get; set; }
        public int Tik_cap { get; set; }
        public int Tik_price { get; set; }
        public double 수익률 { get; set; }
        public bool 주문동기화 { get; set; }
        public int 감시번호 { get; set; }
        public int Order번호 { get; set; }
        public int 수익구분 { get; set; }
        public bool NXT { get; set; }

        public JumunItem(int 재주문timer, int 재주문count, string screennum, string 종목코드, string 종목명, string 주문번호, string 원주문번호, string 검색식, double 주문값, int 주문구분, int 취소시간, int 취소N주문,
            int 반복횟수, string 비고, string location, int 주문수량, int 주문가격, int 주문유형, double 비중, int 비중단위, int 취소timer, int 현재가, double 등락률, int 주문시간, int 미체결량,
            bool 주문취소, bool 가동전, int Order_count, int Tik_cap, int Tik_price, double 수익률, bool 주문동기화, int 감시번호, int Order번호, int 수익구분, bool NXT)
        {
            this.재주문timer = 재주문timer;
            this.재주문count = 재주문count;
            this.screennum = screennum;
            this.종목코드 = 종목코드;
            this.종목명 = 종목명;
            this.주문번호 = 주문번호;
            this.원주문번호 = 원주문번호;
            this.검색식 = 검색식;
            this.주문값 = 주문값;
            this.주문구분 = 주문구분;
            this.취소시간 = 취소시간;
            this.취소N주문 = 취소N주문;
            this.반복횟수 = 반복횟수;
            this.비고 = 비고;
            this.location = location;
            this.주문수량 = 주문수량;
            this.주문가격 = 주문가격;
            this.주문유형 = 주문유형;
            this.비중 = 비중;
            this.비중단위 = 비중단위;
            this.취소timer = 취소timer;
            this.현재가 = 현재가;
            this.등락률 = 등락률;
            this.주문시간 = 주문시간;
            this.미체결량 = 미체결량;
            this.주문취소 = 주문취소;
            this.가동전 = 가동전;
            this.Order_count = Order_count;
            this.Tik_cap = Tik_cap;
            this.Tik_price = Tik_price;
            this.수익률 = 수익률;
            this.주문동기화 = 주문동기화;
            this.감시번호 = 감시번호;
            this.Order번호 = Order번호;
            this.수익구분 = 수익구분;
            this.NXT = NXT;
        }
    }

    public class 체결
    {
        public string con_num { get; set; }
        public string 주문N체결시간 { get; set; }
        public string 종목명 { get; set; }
        public string 체결검색식 { get; set; }
        public string 주문유형 { get; set; }
        public string 거래구분 { get; set; }
        public string 수익률 { get; set; }
        public string 체결가 { get; set; }
        public string 주문수량 { get; set; }
        public string 체결량 { get; set; }
        public string 현재가 { get; set; }
        public string 등락률 { get; set; }
        public string 주문번호 { get; set; }
        public string 종목코드 { get; set; }
        public 체결(string con_num, string 주문N체결시간, string 종목명, string 체결검색식, string 주문유형, string 거래구분, string 수익률, string 체결가, string 주문수량, string 체결량, string 현재가, string 등락률, string 주문번호, string 종목코드)
        {
            this.con_num = con_num;
            this.주문N체결시간 = 주문N체결시간;
            this.종목명 = 종목명;
            this.체결검색식 = 체결검색식;
            this.주문유형 = 주문유형;
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
        public int 주문유형 { get; set; }
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

        public 주문예약(string 등록일, int 예약번호, string 스크린번호, int 주문유형, string 종목코드, string 종목명, double 주문비, double 비중, int 선택, int 주문가, int 주문수량, int 체결수량, string 검색식, string 주문번호, bool 등록, bool 연동, bool 체결완료삭제, bool 전량매도삭제)
        {
            this.등록일 = 등록일;
            this.예약번호 = 예약번호;
            this.스크린번호 = 스크린번호;
            this.주문유형 = 주문유형;
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

    public class NewCatch_A
    {
        public string condition { get; set; }
        public string code { get; set; }
        public string state { get; set; }
        public int timer { get; set; }
        public DateTime CatchTime { get; set; }

        public NewCatch_A(string condition, string code, string state, int timer, DateTime CatchTime)
        {
            this.condition = condition;
            this.code = code;
            this.state = state;
            this.timer = timer;
            this.CatchTime = CatchTime;
        }
    }

    public class NewCatch_B
    {
        public string condition { get; set; }
        public string code { get; set; }
        public string state { get; set; }
        public int timer { get; set; }
        public DateTime CatchTime { get; set; }

        public NewCatch_B(string condition, string code, string state, int timer, DateTime CatchTime)
        {
            this.condition = condition;
            this.code = code;
            this.state = state;
            this.timer = timer;
            this.CatchTime = CatchTime;
        }
    }

    public class NewCatch_C
    {
        public string condition { get; set; }
        public string code { get; set; }
        public string state { get; set; }
        public int timer { get; set; }
        public DateTime CatchTime { get; set; }

        public NewCatch_C(string condition, string code, string state, int timer, DateTime CatchTime)
        {
            this.condition = condition;
            this.code = code;
            this.state = state;
            this.timer = timer;
            this.CatchTime = CatchTime;
        }
    }

    public class Repeat_condition_A
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Repeat_condition_A(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }

    public class Repeat_condition_B
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Repeat_condition_B(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }
    public class Repeat_condition_C
    {
        public string code { get; set; }
        public int timer { get; set; }
        public Repeat_condition_C(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }
    public class Repeat_condition_D
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Repeat_condition_D(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }
    public class Repeat_condition_E
    {
        public string code { get; set; }
        public int timer { get; set; }
        public Repeat_condition_E(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }
    public class Repeat_condition_F
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Repeat_condition_F(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }
    public class Repeat_condition_G
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Repeat_condition_G(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }
    public class Repeat_condition_H
    {
        public string code { get; set; }
        public int timer { get; set; }
        public Repeat_condition_H(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }
    public class Repeat_condition_I
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Repeat_condition_I(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }
    public class Repeat_condition_J
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Repeat_condition_J(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }
    public class Repeat_condition_K
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Repeat_condition_K(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }


    public class Repeat_condition_L
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Repeat_condition_L(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }

    public class Repeat_condition_M
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Repeat_condition_M(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }
    public class Repeat_condition_N
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Repeat_condition_N(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }

    public class Rebal_condition_A
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Rebal_condition_A(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }

    public class Rebal_condition_B
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Rebal_condition_B(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }

    public class Rebal_condition_C
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Rebal_condition_C(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }
    public class Rebal_condition_D
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Rebal_condition_D(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }

    public class Rebal_condition_E
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Rebal_condition_E(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }
    public class Rebal_condition_F
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Rebal_condition_F(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }
    public class Rebal_condition_G
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Rebal_condition_G(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }

    public class Liquidation_condition_A
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Liquidation_condition_A(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }

    public class Liquidation_condition_B
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Liquidation_condition_B(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }

    public class Liquidation_condition_C
    {
        public string code { get; set; }
        public int timer { get; set; }

        public Liquidation_condition_C(string code, int timer)
        {
            this.code = code;
            this.timer = timer;
        }
    }

    public class Rebal_Sell
    {
        public string location { get; set; }
        public string code { get; set; }
        public string screennum { get; set; }
        public string 주문번호 { get; set; }
        public int _1차수량 { get; set; }
        public int _2차수량 { get; set; }
        public int 체결가격 { get; set; }
        public int 수익구분 { get; set; }

        public Rebal_Sell(string location, string code, string screennum, string 주문번호, int _1차수량, int _2차수량, int 체결가격, int 수익구분)
        {
            this.location = location;
            this.code = code;
            this.screennum = screennum;
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
        public string 화면번호 { get; set; }
        public int 수량A { get; set; }
        public int 수량B { get; set; }
        public int 수량C { get; set; }
        public int 수량D { get; set; }
        public int 수량E { get; set; }
        public int 수량F { get; set; }
        public int 수량G { get; set; }
        public int 수량H { get; set; }
        public int 수량I { get; set; }

        public Scalping(string 검색식, string 코드, string 화면번호, int 수량A, int 수량B, int 수량C, int 수량D, int 수량E, int 수량F, int 수량G, int 수량H, int 수량I)
        {
            this.검색식 = 검색식;
            this.코드 = 코드;
            this.화면번호 = 화면번호;
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
        public string date { get; set; }
        public string Title { get; set; }
        public bool 시세등록 { get; set; }

        public Interest_stock(string Code, string Name, string date, string Title, bool 시세등록)
        {
            this.Code = Code;
            this.Name = Name;
            this.date = date;
            this.Title = Title;
            this.시세등록 = 시세등록;
        }
    }

    public class trading_item
    {
        public string Code { get; set; }
        public string location { get; set; }
        public int timer { get; set; }

        public trading_item(string Code, string location, int timer)
        {
            this.Code = Code;
            this.location = location;
            this.timer = timer;
        }
    }
    public class 감시주문
    {
        public string 종목코드 { get; set; }
        public int 주문수량 { get; set; }
        public int 주문체결가격 { get; set; }
        public int 감시주문가격 { get; set; }
        public int 손절주문가격 { get; set; }
        public double 감시_주문값 { get; set; }
        public int 감시_주문구분 { get; set; }
        public string 원주문번호 { get; set; }
        public string 검색식 { get; set; }
        public string 종목명 { get; set; }
        public string 감시일 { get; set; }
        public string 주문일 { get; set; }
        public int 감시번호 { get; set; }
        public int 연동감시번호 { get; set; }
        public string 주문시간 { get; set; }
        public bool 단위_기준 { get; set; }
        public double 차수주문값 { get; set; }
        public int 차수주문구분 { get; set; }
        public int 취소시간 { get; set; }
        public int 수익구분 { get; set; }
        public int 최종번호 { get; set; }
        public string 리밸매도기준 { get; set; }
        public bool TS { get; set; }
        public int TS_high { get; set; }
        public double TS_down { get; set; }
        public int TS_이평 { get; set; }
        public int CBB_TS_이평 { get; set; }

        public 감시주문(string 종목코드, int 주문수량, int 주문체결가격, int 감시주문가격, int 손절주문가격, double 감시_주문값, int 감시_주문구분, string 원주문번호, string 검색식, string 종목명, string 감시일, string 주문일, int 감시번호, int 연동감시번호,
                       string 주문시간, bool 단위_기준, double 차수주문값, int 차수주문구분, int 취소시간, int 수익구분, int 최종번호, string 리밸매도기준, bool TS, int TS_high, double TS_down, int TS_이평, int CBB_TS_이평)
        {
            this.종목코드 = 종목코드;
            this.주문수량 = 주문수량;
            this.주문체결가격 = 주문체결가격;
            this.감시주문가격 = 감시주문가격;
            this.손절주문가격 = 손절주문가격;
            this.감시_주문값 = 감시_주문값;
            this.감시_주문구분 = 감시_주문구분;
            this.원주문번호 = 원주문번호;
            this.검색식 = 검색식;
            this.종목명 = 종목명;
            this.감시일 = 감시일;
            this.주문일 = 주문일;
            this.감시번호 = 감시번호;
            this.연동감시번호 = 연동감시번호;
            this.주문시간 = 주문시간;
            this.단위_기준 = 단위_기준;
            this.차수주문값 = 차수주문값;
            this.차수주문구분 = 차수주문구분;
            this.취소시간 = 취소시간;
            this.수익구분 = 수익구분;
            this.최종번호 = 최종번호;
            this.리밸매도기준 = 리밸매도기준;
            this.TS = TS;
            this.TS_high = TS_high;
            this.TS_down = TS_down;
            this.TS_이평 = TS_이평;
            this.CBB_TS_이평 = CBB_TS_이평;
        }
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
        public int 타이머 { get; set; }
        public string 코드_검색식 { get; set; }
        public bool 신규 { get; set; }

        public 검색이탈(int 타이머, string 코드_검색식, bool 신규)
        {
            this.타이머 = 타이머;
            this.코드_검색식 = 코드_검색식;
            this.신규 = 신규;
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
