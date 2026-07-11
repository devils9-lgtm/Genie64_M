using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace 지니64
{
    internal class 로딩잔고
    {

        private static string GetSafeString(JsonElement element, string key)
        {
            if (element.TryGetProperty(key, out JsonElement prop))
            {
                if (prop.ValueKind == JsonValueKind.String) return prop.GetString();
                if (prop.ValueKind == JsonValueKind.Number) return prop.ToString();
                return prop.ToString();
            }
            return "";
        }


        // =================================================================================
        // [외부 함수] 개별 종목 파싱 및 잔고 업데이트/신규추가 처리
        // =================================================================================
        public static void 업데이트_및_신규추가(JsonElement item, string 종목코드, Market_Item Market, List<string> 기존잔고_추적, List<string> 신규잔고_추적)
        {
            // 1. [변수 초기화]
            bool 선택 = false; int 그룹 = 0; long 누적손익 = 0; long 금일매도금 = 0;
            int 매수횟수 = 0; int 매도횟수 = 0; DateTime 초기매수일 = DateTime.Now;
            string 추가매수일 = ""; string 매수일 = ""; string 매도일 = ""; string 거래일 = "0";
            string 초기매수검색식 = "수동매수"; int 재매수 = 10;
            string 매수주문번호 = " "; string 매도주문번호 = " ";
            string 분_리스트 = ""; string 일_리스트 = "";

            // 2. [데이터 파싱]
            int 금일매수수량 = Helper.StringToInt(GetSafeString(item, "tdy_buyq"));
          //  int 금일매도수량 = Helper.StringToInt(GetSafeString(item, "tdy_sellq"));
            int 전일매수량 = Helper.StringToInt(GetSafeString(item, "pred_buyq"));
            int 전일매도량 = Helper.StringToInt(GetSafeString(item, "pred_sellq"));

            string 신용구분 = GetSafeString(item, "crd_tp");
            string 신용구분명 = GetSafeString(item, "crd_tp_nm");
          //  string 대출일 = GetSafeString(item, "crd_loan_dt");
            string itemName = GetSafeString(item, "stk_nm").TrimStart('*').Trim();
            int 현재가 = Helper.StringToInt(GetSafeString(item, "cur_prc"));

            double 전일종가 = (double)Market.Last_price;
            double 등락율 = Math.Round((현재가 - 전일종가) / 전일종가 * 100, 2);
          //  string 파싱_전일종가 = GetSafeString(item, "pred_close_pric");

            int 평균단가 = Helper.StringToInt(GetSafeString(item, "pur_pric"));
            int 보유수량 = Helper.StringToInt(GetSafeString(item, "rmnd_qty"));

            long 매입금액 = Helper.StringToLong(GetSafeString(item, "pur_amt"));
            double 보유비중 = Math.Round((double)매입금액 / (double)GenieConfig.MT_buying_standard * 100, 2);
         //   string 파싱_보유비중 = GetSafeString(item, "poss_rt");

            long 평가금액 = Helper.StringToLong(GetSafeString(item, "evlt_amt"));
            long 평가손익 = long.Parse(GetSafeString(item, "evltv_prft").Trim());
            int 주문가능수량 = Helper.StringToInt(GetSafeString(item, "trde_able_qty"));
            int 세금 = Helper.StringToInt(GetSafeString(item, "tax"));
         //   string 수익률_ = GetSafeString(item, "prft_rt");

         //   long 매입수수료 = Helper.StringToLong(GetSafeString(item, "pur_cmsn"));
         //   long 평가수수료 = Helper.StringToLong(GetSafeString(item, "sell_cmsn"));
         //   long 수수료합계 = Helper.StringToLong(GetSafeString(item, "sum_cmsn"));

            //string printMsg = $"\n▶ [{itemName}({종목코드})] 파싱 결과 (신용:{신용구분명}({신용구분}) / 대출일:{대출일})\n" +
            //                  $"  - 가격: 현재가 {현재가:N0}원 | 전일종가 {전일종가:N0}원(파싱:{파싱_전일종가}) | 등락율 {등락율}% | 평균단가 {평균단가:N0}원\n" +
            //                  $"  - 수량: 보유 {보유수량:N0}주 (비중계산 {보유비중}%/파싱 {파싱_보유비중}%) | 주문가능 {주문가능수량}\n" +
            //                  $"  - 수량(당일): 매수 {금일매수수량} / 매도 {금일매도수량} | (전일): 매수 {전일매수량} / 매도 {전일매도량}\n" +
            //                  $"  - 금액: 매입금액 {매입금액:N0}원 | 평가금액 {평가금액:N0}원 | 평가손익 {평가손익:N0}원 | 수익률 {수익률_}%\n" +
            //                  $"  - 비용: 세금 {세금:N0}원 | 매입수수료 {매입수수료:N0}원 | 평가수수료 {평가수수료:N0}원 | 수수료합계 {수수료합계:N0}원";
            //Console_print(printMsg);

            // 3. [시장 및 계산 로직]
            string 시장 = Market.Market;
            double tax_ = 시장.Equals("E") ? 0 : Form1.TAX;
            int 시작가 = 평균단가;
            int 기준가 = 평균단가;
            if (GenieConfig.CB_new_rebuy) 재매수 = GenieConfig.MTB_new_rebuytime;
            string 종목상태 = GET.Jango_state(종목코드); ;
            if (종목상태 != "거래정지")
            {
                if (Form1.Get.TimeNow < Form1.Get.메인마켓시작 - 10000)
                {
                    종목상태 = "마켓대기";
                }
                else
                {
                    종목상태 = "시세로딩";
                }
            }

            bool 매매_가능 = false;

            Market.현재가 = 현재가;
            Market.등락율 = 등락율;

            long 예상손익 = 평가손익 + 누적손익;
            long 금일매수금 = 금일매수수량 * 현재가;
            int 잔고수수료 = (int)(Math.Truncate((double)현재가 * 보유수량 * Form1.수수료 / 10) * 10);
            double 세금_수수료 = Math.Truncate(((double)평균단가 * (double)보유수량 * Form1.수수료) + ((double)현재가 * (double)보유수량 * Form1.수수료) + ((double)현재가 * (double)보유수량 * tax_));
            double 평손 = (double)평가금액 - (double)매입금액 - 세금_수수료;
            평가손익 = (long)(평손);

            double 수익률 = Math.Truncate((((double)(현재가 - 평균단가) / 평균단가 * (double)100) - ((Form1.수수료 + Form1.수수료 + tax_) * 100)) * 100) / 100;
            double 시작수익률 = 수익률;
            double 기준수익률 = 수익률;
            double 최고수익률 = (0 < 수익률) ? 수익률 : 0;
            double 최저수익률 = (수익률 < 0) ? 수익률 : 0;
            double 최고예상손익금 = (0 < 예상손익) ? 예상손익 : 0;
            double 최저예상손익금 = (예상손익 < 0) ? 예상손익 : 0;

            // ==========================================================
            // 🔹 4. [기존 잔고 업데이트] (유저 요청 핵심 분리 구간)
            // ==========================================================
            if (Form1.stockBalanceList.TryGetValue(종목코드, out Stockbalance 잔고))
            {
             //  Form1. Console_print("[기존 잔고 업데이트] 잔고 데이터 종목명: " + itemName);

                기존잔고_추적.Add(itemName); // 분리 바구니

                if (!Form1.로딩완료)
                {
                  
                    잔고.종목명 = itemName;
                    잔고.매매가능 = 매매_가능;
                    잔고.종목상태 = 종목상태;
                    잔고.거래일 = CheckHoliday.Get실제거래일계산(잔고.초기매수일.Date);
                    잔고.전량매도 = false;
                    잔고.현재가 = 현재가;
                    잔고.등락율 = 등락율;
                    잔고.시장 = 시장;
                    double.TryParse(잔고.기준가격.ToString(), out double 기준가격);
                    if (기준가격 == 0) 잔고.기준가격 = 기준가;
                    double.TryParse(잔고.시작가격.ToString(), out double 시작가격);
                    if (시작가격 == 0) 잔고.시작가격 = 시작가;

                    잔고.예상손익 += (잔고.누적손익 + 평가손익);
                    잔고.전일매수량 += 전일매수량;
                    잔고.전일매도량 += 전일매도량;
                    잔고.보유비중 += 보유비중;
                    잔고.세금 += 세금;
                    잔고.수수료 += (int)(Math.Truncate((double)잔고.현재가 * 잔고.보유수량 * Form1.수수료 / 10) * 10);
                    잔고.매도기준 += 보유수량;
                    잔고.기준수익률 = Math.Truncate((((double)(현재가 - 잔고.기준가격) / 잔고.기준가격 * (double)100) - ((Form1.수수료 + Form1.수수료 + tax_) * 100)) * 100) / 100;
                    잔고.시작수익률 = Math.Truncate((((double)(현재가 - 잔고.시작가격) / 잔고.시작가격 * (double)100) - ((Form1.수수료 + Form1.수수료 + tax_) * 100)) * 100) / 100;

                    if (!잔고.초기매수일.Date.Equals(DateTime.Now.Date) && !잔고.매수일.Equals(Form1.str.today))
                    {
                        잔고.금일매수금 = 0;
                    }
                    if (!잔고.매도일.Equals(Form1.str.today))
                    {
                        잔고.금일매도금 = 0;
                    }

                    잔고.최고수익률 = 최고수익률;
                    잔고.최저수익률 = 최저수익률;

                    if (신용구분 == "00")
                    {
                        잔고.보유수량 = 0; //수량초기화
                        잔고.주문가능수량 = 0;

                        잔고.수익률 = 수익률;
                        잔고.평균단가 = 평균단가;
                        잔고.매입금액 = 매입금액;
                        잔고.평가금액 = 평가금액;
                        잔고.평가손익 = 평가손익;
                        잔고.주문가능수량 = 주문가능수량;
                        잔고.보유수량 = 보유수량;
                    }
                    else
                    {
                        잔고.신용_보유수량 = 0; //수량초기화
                        잔고.신용_주문가능수량 = 0;

                        잔고.신용_보유수량 = 보유수량;
                        잔고.신용_주문가능수량 = 주문가능수량;
                        잔고.신용_매입금액 = 매입금액;
                        잔고.신용_평가금액 = 평가금액;
                        잔고.신용_평가손익 = 평가손익;
                        잔고.신용_평균단가 = 평균단가;
                        잔고.신용_수익률 = 수익률;
                    }
                }
            }
            // ==========================================================
            // 🔸 5. [신규 잔고 추가] (유저 요청 핵심 분리 구간)
            // ==========================================================
            else
            {
                신규잔고_추적.Add(itemName); // 분리 바구니
               // Form1.Console_print("신규매수 종목코드 : " + itemName + " 신용구분 : " + 신용구분);

                int _신용_보유수량 = 0; int _신용_주문가능수량 = 0; long _신용_매입금액 = 0; long _신용_평가금액 = 0;
                long _신용_평가손익 = 0; int _신용_평균단가 = 0; double _신용_수익률 = 0;

                //long _신용_예상손익 = 0;
                //int _신용_전일매수량 = 0; 
                //int _신용_전일매도량 = 0; 
                //double _신용_보유비중 = 0; 
                //int _신용_세금 = 0;
                //int _신용_수수료 = 0;

                if (신용구분 == "00")
                {
                //    Form1.Console_print($"[신규매수] ### 현금 ### {itemName} 현금구분: {신용구분} 보유수량 : {보유수량} 주문가능수량 : {주문가능수량}");
                }
                else 
                {
                    _신용_보유수량 = 보유수량; _신용_주문가능수량 = 주문가능수량; _신용_매입금액 = 매입금액;
                    _신용_평가금액 = 평가금액; _신용_평가손익 = 평가손익; _신용_평균단가 = 평균단가;
                    _신용_수익률 = 수익률;
                    //_신용_예상손익 = 예상손익; 
                    //_신용_전일매수량 = 전일매수량;
                    //_신용_전일매도량 = 전일매도량;
                    //_신용_보유비중 = 보유비중;
                    //_신용_세금 = 세금;
                    //_신용_수수료 = 잔고수수료;

                    보유수량 = 0; 주문가능수량 = 0; 매입금액 = 0; 평가금액 = 0; 평가손익 = 0; 평균단가 = 0;
                    예상손익 = 0; 전일매수량 = 0; 전일매도량 = 0; 보유비중 = 0; 세금 = 0; 잔고수수료 = 0;

                 //   Form1.Console_print($"[신규매수] ### 신용 ### {itemName} 신용구분: {신용구분} 보유수량 : {_신용_보유수량} 주문가능수량 : {_신용_주문가능수량}");
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

                    Today = Form1.str.today,
                    매수제한 = false,
                    전량매도 = false,
                    추매딜레이 = 0,
                    선택 = 선택,
                    매매그룹 = 그룹,
                    시장 = 시장,
                    종목명 = itemName,
                    종목코드 = 종목코드,
                    현재가 = 현재가,
                    등락율 = 등락율,
                    평균단가 = 평균단가,
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
                    금일매수금 = 금일매수금,
                    금일매도금 = 금일매도금,
                    매수횟수 = 매수횟수,
                    매도횟수 = 매도횟수,
                    초기매수일 = 초기매수일,
                    거래일 = 거래일,
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
                    일회A = "on",
                    일회B = "on",
                    일회C = "on",
                    일회D = "on",
                    일회E = "on",
                    일회F = "on",
                    일회G = "on",
                    일회H = "on",
                    일회I = "on",
                    손절A = true,
                    손절B = true,
                    손절C = true,
                    손절D = true,
                    손절E = true,
                    손절F = true,
                    반복A = "A",
                    반복B = "B",
                    반복C = "C",
                    반복D = "D",
                    반복E = "E",
                    반복F = "F",
                    반복G = "G",
                    반복H = "H",
                    반복I = "I",
                    반복J = "J",
                    반복K = "K",
                    반복L = "L",
                    반복M = "M",
                    반복N = "N",
                    세금 = 세금,
                    수수료 = 잔고수수료,
                    매도기준 = 보유수량,
                    매도기준update = true,
                    종목상태 = 종목상태,
                    매매가능 = 매매_가능,
                    그룹지정_A = Form1.그룹지정_A,
                    그룹지정_B = Form1.그룹지정_B,
                    그룹지정_C = Form1.그룹지정_C,
                    그룹지정_D = Form1.그룹지정_D,
                    리벨A = "A",
                    리벨B = "B",
                    리벨C = "C",
                    리벨D = "D",
                    리벨E = "E",
                    리벨F = "F",
                    리벨G = "G",
                    청산A = "A",
                    청산B = "B",
                    청산C = "C",
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
                    시간청산동작_A = "A",
                    시간청산동작_B = "B",
                    시간청산동작_C = "C",
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
                    잔고청산_TS_high_A = 0,
                    잔고청산_TS_high_B = 0,
                    잔고청산_TS_high_C = 0,
                    매매기간_TS_high_A = 0,
                    매매기간_TS_high_B = 0,
                    매매기간_TS_high_C = 0,
                    매매기간_TS_high_D = 0,
                    매매기간_TS_high_E = 0,
                    매매기간_TS_high_F = 0
                });

                MA.New_item(종목코드);

                Log.동작기록($"[수동매수] {신용구분명} 종목명: {itemName} 평균단가: {평균단가:N0} 매수수량: {보유수량:N0} 매입금액: {매입금액:N0} 수동 매수 되었습니다.");
                Helper.알림창_멀티("수동매수", $"{신용구분명} 종목명: {itemName} 평균단가: {평균단가:N0}\n\n매수수량: {보유수량:N0} 매입금액: {매입금액:N0} 수동 매수 되었습니다.", 5, false);

                if (!Form1.최종매입가_List.ContainsKey(종목코드))
                {
                    Helper.최종매입가_신규추가(종목코드, (int)평균단가);
                }
            }
        }



    }
}
