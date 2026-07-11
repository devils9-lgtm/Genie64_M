using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 지니64
{
    internal class 신규잔고:Form1
    {

        public static void New_StockBalance(bool 신용구분, string 종목코드, string 종목명, int 현재가, int 체결가, int 체결량, string 검색식, int 매매세금, int 매매수수료)
        {
            int group = 0;

            REG.실시간시세등록(종목코드);
            홀딩잔고.신규매수개수확인(검색식, true); // 신규매수

            if (GenieConfig.CB_new_A && 검색식.Contains(Form1.위치별검색식리스트["신규_A"].이름))
            {
                group = GenieConfig.combo_신규그룹_A;
            }
            else if (GenieConfig.CB_new_B && 검색식.Contains(Form1.위치별검색식리스트["신규_B"].이름) && (GenieConfig.combo_new_or_B.Equals(0) || GenieConfig.combo_new_or_B.Equals(2)))
            {
                group = GenieConfig.combo_신규그룹_B;
            }
            else if (GenieConfig.CB_new_C && 검색식.Contains(Form1.위치별검색식리스트["신규_C"].이름) && (GenieConfig.combo_new_or_C.Equals(0) || GenieConfig.combo_new_or_C.Equals(2)))
            {
                group = GenieConfig.combo_신규그룹_C;
            }

            bool 선택 = false;
            int 매매그룹 = group;
            int s현재가 = 현재가;
            double 등락율 = 0;
            int 보유수량 = 체결량;
            long 매입금액 = 체결가 * 체결량;
            double 보유비중 = 0;
            long 평가금액 = 현재가 * 체결량;
            long 평가손익 = 0;
            long 누적손익 = 0;
            long 예상손익 = 평가손익 + 누적손익;
            long 금일매수금 = 매입금액;
            long 금일매도금 = 0;
            int 매수횟수 = 0;
            int 매도횟수 = 0;
            DateTime 초기매수일 = DateTime.Now;
            string 추가매수일 = "";
            string 매수일 = ""; ;
            string 매도일 = ""; ;

            string 거래일 = "0";
            string 초기매수검색식 = 검색식;
            int 전일매수량 = 0;
            int 전일매도량 = 0;
            int 재매수 = 10;
            string 매수주문번호 = "-";
            string 매도주문번호 = "-";
            int 주문가능수량 = 체결량;
            string 일회A = "on";
            string 일회B = "on";
            string 일회C = "on";
            string 일회D = "on";
            string 일회E = "on";
            string 일회F = "on";
            string 일회G = "on";
            string 일회H = "on";
            string 일회I = "on";
            string 반복A = "A";
            string 반복B = "B";
            string 반복C = "C";
            string 반복D = "D";
            string 반복E = "E";
            string 반복F = "F";
            string 반복G = "G";
            string 반복H = "H";
            string 반복I = "I";
            string 반복J = "J";
            string 반복K = "K";
            string 반복L = "L";
            string 반복M = "M";
            string 반복N = "N";
            string 리벨A = "A";
            string 리벨B = "B";
            string 리벨C = "C";
            string 리벨D = "D";
            string 리벨E = "E";
            string 리벨F = "F";
            string 리벨G = "G";
            string 잔고청산A = "A";
            string 잔고청산B = "B";
            string 잔고청산C = "C";
            string 시간청산A = "A";
            string 시간청산B = "B";
            string 시간청산C = "C";
            string 최종매입가_A = 체결가.ToString();
            string 최종매입가_B = 체결가.ToString();
            string 최종매입가_C = 체결가.ToString();
            string 최종매입가_D = 체결가.ToString();
            string 최종매입가_E = 체결가.ToString();
            string 최종매입가_F = 체결가.ToString();
            string 최종매입가_G = 체결가.ToString();

            int 잔고청산_TS_high_A = 0;
            int 잔고청산_TS_high_B = 0;
            int 잔고청산_TS_high_C = 0;
            int 매매기간_TS_high_A = 0;
            int 매매기간_TS_high_B = 0;
            int 매매기간_TS_high_C = 0;
            int 매매기간_TS_high_D = 0;
            int 매매기간_TS_high_E = 0;
            int 매매기간_TS_high_F = 0;

            string 분_리스트 = "";
            string 일_리스트 = "";

            int 시작가 = 체결가;
            int 기준가 = 체결가;
            string 시장 = "R";
            시장 = Form1.Market_Item_List[종목코드].Market;

            double tax_ = Form1.TAX;
            if (시장.Equals("E")) tax_ = 0;

            double 수익률 = Math.Truncate((((double)(현재가 - 체결가) / 체결가 * (double)100) - ((Form1.수수료 + Form1.수수료 + tax_) * 100)) * 100) / 100;
            double 기준수익률 = 수익률;
            double 시작수익률 = 수익률;

            double 최고예상손익금 = 0;
            double 최저예상손익금 = 0;

            if (0 < 예상손익) 최고예상손익금 = 예상손익;
            if (예상손익 < 0) 최저예상손익금 = 예상손익;

            double 최고수익률 = 0;
            double 최저수익률 = 0;

            if (0 < 수익률) 최고수익률 = 수익률;
            if (수익률 < 0) 최저수익률 = 수익률;

            if (GenieConfig.CB_new_rebuy)
            {
                재매수 = GenieConfig.MTB_new_rebuytime;
            }
            int 추매딜레이 = GenieConfig.MTB_추가매수딜레이;

            Console_print(" ------------ 신규매수 차트 조회 ------------- " + 종목명);
            TR_요청.주식분봉차트조회요청(종목코드, false);
            TR_요청.주식일봉차트조회요청(종목코드, false);

            // =========================================================
            // 1. [변수 준비] 신용 변수 초기화 (기본값 0)
            // =========================================================
            int _신용_보유수량 = 0;
            int _신용_주문가능수량 = 0;
            long _신용_매입금액 = 0;
            long _신용_평가금액 = 0;
            long _신용_평가손익 = 0;
            int _신용_평균단가 = 0;
            double _신용_수익률 = 0;

            // =========================================================
            // 2. [값 할당] 신용일 경우에만 파싱한 값을 매핑
            // =========================================================
            if (신용구분) // 신용인 경우
            {
                _신용_보유수량 = 보유수량;
                _신용_주문가능수량 = 주문가능수량;
                _신용_매입금액 = 매입금액;
                _신용_평가금액 = 평가금액;
                _신용_평가손익 = 평가손익;
                _신용_평균단가 = 체결가;
                _신용_수익률 = 수익률;
                
                보유수량 = 0;
                주문가능수량 = 0;
                매입금액 = 0;
                평가금액 = 0;
                평가손익 = 0;
                체결가 = 0;
                수익률 = 0;
            }

            Form1.stockBalanceList.TryAdd(종목코드, new Stockbalance
            {
                신용_보유수량 = _신용_보유수량,
                신용_주문가능수량 = _신용_주문가능수량,
                신용_매입금액 = _신용_매입금액,
                신용_평가금액 = _신용_평가금액,
                신용_평가손익 = _신용_평가손익,
                신용_평균단가 = _신용_평균단가,
                신용_수익률 = _신용_수익률,

                Today = str.today,
                매수제한 = false,
                전량매도 = false,
                추매딜레이 = 추매딜레이,
                선택 = 선택,
                매매그룹 = 매매그룹,
                시장 = 시장,
                종목명 = 종목명,
                종목코드 = 종목코드, // JSON 매핑용

                // [시세 및 금액]
                현재가 = s현재가,
                등락율 = 등락율,
                평균단가 = 체결가,
                보유수량 = 보유수량,
                매입금액 = 매입금액,
                평가금액 = 평가금액,
                평가손익 = 평가손익,
                금일수익금 = 0,
                누적손익 = 누적손익,
                예상손익 = 예상손익,
                수익률 = 수익률,
                최고수익률 = 최고수익률,
                최저수익률 = 최저수익률,
                최고예상손익금 = 최고예상손익금,
                최저예상손익금 = 최저예상손익금,

                // [매매 이력]
                금일매수금 = 금일매수금,
                금일매도금 = 금일매도금,
                매수횟수 = 매수횟수,
                매도횟수 = 매도횟수,
                초기매수일 = 초기매수일,
                거래일 = 거래일,

                // [전략 및 주문]
                초기매수검색식 = 초기매수검색식,
                보유비중 = 보유비중,
                전일매수량 = 전일매수량,
                전일매도량 = 전일매도량,
                재매수 = 재매수,
                추가매수일 = 추가매수일,
                매수일 = 매수일,
                매도일 = 매도일,
                주문가능수량 = 주문가능수량,
                매수주문번호 = 매수주문번호,
                매도주문번호 = 매도주문번호,

                // [익절/보전/손절 설정]
                익절A = true,
                익절B = true,
                익절C = true,
                익절D = true,
                익절E = true,
                익절F = true,
                익절G = true,
                익절H = true,
                익절I = true,

                보전A = "0",
                보전B = "0",
                보전C = "0",
                보전D = "0",
                보전E = "0",
                보전F = "0",
                보전G = "0",
                보전H = "0",
                보전I = "0",

                일회A = 일회A,
                일회B = 일회B,
                일회C = 일회C,
                일회D = 일회D,
                일회E = 일회E,
                일회F = 일회F,
                일회G = 일회G,
                일회H = 일회H,
                일회I = 일회I,

                손절A = true,
                손절B = true,
                손절C = true,
                손절D = true,
                손절E = true,
                손절F = true,

                // [반복 설정]
                반복A = 반복A,
                반복B = 반복B,
                반복C = 반복C,
                반복D = 반복D,
                반복E = 반복E,
                반복F = 반복F,
                반복G = 반복G,
                반복H = 반복H,
                반복I = 반복I,
                반복J = 반복J,
                반복K = 반복K,
                반복L = 반복L,
                반복M = 반복M,
                반복N = 반복N,

                // [비용 및 상태]
                세금 = 매매세금,
                수수료 = 매매수수료,
                매도기준 = 0,
                매도기준update = true,
                종목상태 = GET.Jango_state(종목코드),
                매매가능 = true,

                // [그룹 지정]
                그룹지정_A = Form1.그룹지정_A,
                그룹지정_B = Form1.그룹지정_B,
                그룹지정_C = Form1.그룹지정_C,
                그룹지정_D = Form1.그룹지정_D,

                // [전략 세부]
                리벨A = 리벨A,
                리벨B = 리벨B,
                리벨C = 리벨C,
                리벨D = 리벨D,
                리벨E = 리벨E,
                리벨F = 리벨F,
                리벨G = 리벨G,
                청산A = 잔고청산A,
                청산B = 잔고청산B,
                청산C = 잔고청산C,

                // [트레일링 스탑]
                TS_1 = true,
                TS_2 = true,
                TS_3 = true,
                TS_4 = true,
                TS_5 = true,
                TS_6 = true,
                TS_7 = true,
                TS_8 = true,
                TS_9 = true,
                트레일링값 = "0@0@0@0",
                시작가격 = 시작가,
                시작수익률 = 시작수익률,
                기준가격 = 기준가,
                기준수익률 = 기준수익률,

                // [가동 여부]
                가동_반복A = true,
                가동_반복B = true,
                가동_반복C = true,
                가동_반복D = true,
                가동_반복E = true,
                가동_반복F = true,
                가동_반복G = true,
                가동_반복H = true,
                가동_반복I = true,
                가동_반복J = true,
                가동_반복K = true,
                가동_반복L = true,
                가동_반복M = true,
                가동_반복N = true,

                가동_리밸A = true,
                가동_리밸B = true,
                가동_리밸C = true,
                가동_리밸D = true,
                가동_리밸E = true,
                가동_리밸F = true,
                가동_리밸G = true,

                가동_청산A = true,
                가동_청산B = true,
                가동_청산C = true,

                // [기타 설정]
                시간청산반복_A = 0,
                시간청산반복_B = 0,
                시간청산반복_C = 0,
                추매정지 = false,
                잔고청산 = true,

                TimeSell_매입금_A = false,
                TimeSell_매입금_B = false,
                TimeSell_매입금_C = false,
                잔고청산_매입금_A = false,
                잔고청산_매입금_B = false,
                잔고청산_매입금_C = false,

                Repeat_매입금_A = false,
                Repeat_매입금_B = false,
                Repeat_매입금_C = false,
                Repeat_매입금_D = false,
                Repeat_매입금_E = false,
                Repeat_매입금_F = false,
                Repeat_매입금_G = false,
                Repeat_매입금_H = false,
                Repeat_매입금_I = false,
                Repeat_매입금_J = false,
                Repeat_매입금_K = false,
                Repeat_매입금_L = false,
                Repeat_매입금_M = false,
                Repeat_매입금_N = false,

                Rebalance_매입금_A = false,
                Rebalance_매입금_B = false,
                Rebalance_매입금_C = false,
                Rebalance_매입금_D = false,
                Rebalance_매입금_E = false,
                Rebalance_매입금_F = false,
                Rebalance_매입금_G = false,

                시간청산동작_A = 시간청산A,
                시간청산동작_B = 시간청산B,
                시간청산동작_C = 시간청산C,

                잔고청산_A = false,
                잔고청산_B = false,
                잔고청산_C = false,
                매도정지 = false,
                분_리스트 = 분_리스트,
                일_리스트 = 일_리스트,

                TS_알림_A = true,
                TS_알림_B = true,
                TS_알림_C = true,
                TS_알림_D = true,
                TS_알림_E = true,
                TS_알림_F = true,
                TS_알림_G = true,
                TS_알림_H = true,
                TS_알림_I = true,

                // [TS High 값]
                잔고청산_TS_high_A = 잔고청산_TS_high_A,
                잔고청산_TS_high_B = 잔고청산_TS_high_B,
                잔고청산_TS_high_C = 잔고청산_TS_high_C,

                매매기간_TS_high_A = 매매기간_TS_high_A,
                매매기간_TS_high_B = 매매기간_TS_high_B,
                매매기간_TS_high_C = 매매기간_TS_high_C,
                매매기간_TS_high_D = 매매기간_TS_high_D,
                매매기간_TS_high_E = 매매기간_TS_high_E,
                매매기간_TS_high_F = 매매기간_TS_high_F
            });

            MA.New_item(종목코드);
            Helper.최종매입가_신규추가(종목코드, 체결가);

            Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                Form1.form1.JanGo_dataGridView.Rows.Insert(0);
                Form1.form1.JanGo_dataGridView["코드_잔고", 0].Value = 종목코드;
                홀딩잔고.JangoRow_print(0, Form1.stockBalanceList[종목코드]);
                Form1.form1.JanGo_dataGridView.ClearSelection();
            });

            재매수 Item = Form1.Rebuystock_List.Find(o => o.Itemcode.Equals(종목코드));
            if (Item == null)
            {
                재매수 Add = new 재매수(종목코드, 종목명, "");
                Form1.Rebuystock_List.Add(Add);
            }

            Log.동작기록("");
            Log.동작기록($"[신규매수] 종목명: {종목명} 검색식: {검색식} 평균단가: {체결가:N0} 신규매수 되었습니다.");
            Log.동작기록("");

            string Itemname = 종목명;
            if (Form1.form1.CB_종목비공개.Checked) Itemname = "XXXXX";
            Helper.알림창_멀티("신규매수", $"종목명: {Itemname} 검색식: {검색식}\n평균단가: {체결가:N0} 신규매수 되었습니다.", 5, false);

            if (GenieConfig.CB_텔레그램사용)
            {
                _ = TelegramMessenger.Telegram_Send($"[신규매수] :: 검색식: {검색식} 종목: {종목명} 체결가: {체결가:N0} D2예수금: {Method.단위변환(Form1.Acc.추정D2)}");
            }

            SaveToFile.잔고_파일저장();
            SaveToFile.최종매입가_파일저장(Form1.로딩완료);
        }
    }
}
