using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using 지니64.RESTAPI;

namespace 지니64
{
    public class Tab_Basic : Form1
    {
        public static async Task New_Buy(string strType, string itemcode, string condition)
        {
            bool ID_A = false;
            bool ID_B = false;
            bool ID_C = false;

            if (strType.Equals("I"))
            {
                if (GenieConfig.combo_new_or_A == 0) ID_A = true;
                if (GenieConfig.combo_new_or_B == 0 || GenieConfig.combo_new_or_B == 1) ID_B = true;
                if (GenieConfig.combo_new_or_C == 0 || GenieConfig.combo_new_or_C == 1) ID_C = true;
            }
            else if (strType.Equals("D"))
            {
                if (GenieConfig.combo_new_or_A == 1) ID_A = true;
                if (GenieConfig.combo_new_or_B == 2 || GenieConfig.combo_new_or_B == 3) ID_B = true;
                if (GenieConfig.combo_new_or_C == 2 || GenieConfig.combo_new_or_C == 3) ID_C = true;
            }

            // 메인 로직 내
            await HandleNewStock("A", GenieConfig.CB_new_A, Form1.위치별검색식리스트["신규_A"].이름, ID_A, GenieConfig.MTB_new_delay_A, condition, NewStock_List);
            await HandleNewStock("B", GenieConfig.CB_new_B, Form1.위치별검색식리스트["신규_B"].이름, ID_B, GenieConfig.MTB_new_delay_B, condition, NewStock_List);
            await HandleNewStock("C", GenieConfig.CB_new_C, Form1.위치별검색식리스트["신규_C"].이름, ID_C, GenieConfig.MTB_new_delay_C, condition, NewStock_List);

            async Task HandleNewStock(
                 string suffix,
                 bool isEnabled,
                 string requiredCondition,
                 bool ID,
                 int delayTime,
                 string currentCondition,
                 List<Newstock> stockList)
            {
                // 1. 활성화 및 조건 일치 여부 확인 (기존 if 문의 조건)
                if (isEnabled && requiredCondition.Equals(currentCondition))
                {
                    string pos = "New_" + suffix; // 예: "New_A"
                    Newstock stock = stockList.Find(o => o.Pos == pos && o.code.Equals(itemcode));

                    if (stock == null)
                    {
                        // 항목이 없는 경우 (신규 진입)
                        if (ID)
                        {
                            stock = new Newstock(pos, currentCondition, itemcode, "진입", 0, DateTime.Now);
                            stockList.Add(stock);
                            if (delayTime == 0) await 매수검색식_Check(stock);
                        }
                    }
                    else
                    {
                        // 항목이 있는 경우 (상태 업데이트)
                        if (ID)
                        {
                            stock.state = "재진입";
                        }
                        else
                        {
                            stock.state = "이탈";

                            if (pos == "New_B")
                            {
                                form1.ABC_List.RemoveAll(o => o.Code == itemcode && (o.Loc == "AnB" || o.Loc == "AnBnC"));
                            }
                            else if (pos == "New_C")
                            {
                                form1.ABC_List.RemoveAll(o => o.Code == itemcode && o.Loc == "AnBnC");
                            }
                        }

                        stock.timer = 0;
                        if (ID && delayTime == 0) await 매수검색식_Check(stock);
                    }
                }
            }
        }

        // [+] 다중 스레드의 동시 접근을 막는 자물쇠
        private static readonly object 매수검사_자물쇠 = new object();

        // [+] 큐에 들어가기 전 '가승인' 상태의 주문을 잠깐(2초) 기억하는 장부
        private static List<가승인장부> 최근승인내역 = new List<가승인장부>();

        public class 가승인장부
        {
            public string 위치;
            public DateTime 시간;
        }

        public static bool 신규매수진행(Market_Item Market, string 검색식, string 위치)
        {
            // [+] 1단계 방어: 여러 신호가 몰려와도 무조건 한 줄로 서서 순서대로 검사받도록 자물쇠를 채웁니다.
            lock (매수검사_자물쇠)
            {
                DateTime 현재시간 = DateTime.Now;

                // [+] 2단계 방어 (메모리 최적화): 승인된 지 2초가 지난 기록은 실제 큐에 들어갔다고 판단하고 장부에서 지웁니다.
                최근승인내역.RemoveAll(x => (현재시간 - x.시간).TotalSeconds > 2);

                bool result = true;

                if (!Form1.신규매수정지)
                {
                    // 진행 중인(아직 리스트엔 없지만 승인된) 전체 주문 개수를 파악합니다.
                    int 진행중인_전체주문 = 최근승인내역.Count;

                    if (Get.신규횟수 + Form1.stockBalanceList.Count + 진행중인_전체주문 < GenieConfig.TB_setjango)
                    {
                        if (GenieConfig.CB_신규횟수제한)
                        {
                            if (GenieConfig.신규횟수 + 진행중인_전체주문 >= GenieConfig.TB_신규횟수제한)
                            {
                                거부메세지("횟수제한");
                                result = false;
                            }
                        }

                        // 내부 함수 개선: 특정 위치(A, B, C)의 주문 개수를 검사합니다.
                        int 주문개수(string 타겟위치)
                        {
                            int orderCount = 0;

                            // 1. 실제 주문 큐에 들어간 개수
                            foreach (var item in Form1.JumunItem_List)
                            {
                                if (item.Value.검색식.Contains(타겟위치)) orderCount++;
                            }

                            // 2. 큐에는 안 들어갔지만, 방금 전 2초 내에 승인해준 '예약' 개수를 더합니다. (핵심)
                            foreach (var item in 최근승인내역)
                            {
                                if (item.위치 == 타겟위치) orderCount++;
                            }

                            return orderCount;
                        }

                        if (result && 위치.Equals("신규_A")) { if (Get.신규개수A + 주문개수("신규_A") >= GenieConfig.TB_잔고개수_신규A) { 거부메세지("신규개수"); result = false; } }
                        if (result && 위치.Equals("신규_B")) { if (Get.신규개수B + 주문개수("신규_B") >= GenieConfig.TB_잔고개수_신규B) { 거부메세지("신규개수"); result = false; } }
                        if (result && 위치.Equals("신규_C")) { if (Get.신규개수C + 주문개수("신규_C") >= GenieConfig.TB_잔고개수_신규C) { 거부메세지("신규개수"); result = false; } }

                        if (result && GenieConfig.CB_계좌매입비_매수제한)
                        {
                            if (Method.매입비매수제한(GenieConfig.CBB_계좌매입비_제한선택).Contains("신규"))
                            {
                                거부메세지("매입비매수제한");
                                result = false;
                            }
                        }

                        if (result && !Jisu_linkage.업종별지수연동("신규", Market))
                        {
                            거부메세지("업종별지수연동");
                            result = false;
                        }
                    }
                    else
                    {
                        거부메세지("잔고수");
                        result = false;
                    }
                }
                else
                {
                    거부메세지("신규매수정지");
                    result = false;
                }

                // [+] 3단계 방어: 모든 깐깐한 검사를 통과했다면, 다음 녀석이 뚫고 들어오지 못하게 '가승인 장부'에 즉시 기록을 남깁니다.
                if (result)
                {
                    최근승인내역.Add(new 가승인장부 { 위치 = 위치, 시간 = 현재시간 });
                }

                void 거부메세지(string 신호)
                {
                    string 시장 = "코스피";
                    if (Market.Market.Equals("D")) 시장 = "코스닥";
                    string message = "";

                    if (신호.Equals("횟수제한")) message = ("[신규매수 제한 '매수횟수'] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                    else if (신호.Equals("신규개수")) message = ("[신규매수 제한 '조건별 잔고제한'] " + 위치 + " -> " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                    else if (신호.Equals("잔고수")) message = ("[신규매수 제한 '최대잔고'] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                    else if (신호.Equals("매입비매수제한")) message = ("[신규매수 제한 '매입비매수제한'] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                    else if (신호.Equals("업종별지수연동")) message = ("[신규매수 제한 '업종별지수연동'] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                    else if (신호.Equals("신규매수정지")) message = ("[신규매수 제한 '신규매수정지'] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);

                    매매거부_메세지출력(Market.종목명, message);
                }

                return result;
            }
        }


        public static async Task 매수검색식_Check(Newstock item)
        {
            string Code = item.code;
            if (!stockBalanceList.ContainsKey(Code)) //잔고에 없을때
            {
                bool Recatch = GenieConfig.CB_new_recatch_A;
                if (item.Pos == "New_B") Recatch = GenieConfig.CB_new_recatch_B;
                else if (item.Pos == "New_C") Recatch = GenieConfig.CB_new_recatch_C;

                if (Recatch) //재진입 포함 매수 
                {
                    if (item.state.Contains("진입"))
                    {
                        await New_buying(item);
                    }
                }
                else
                {
                    if (item.state.Equals("진입")) //최초 진입시에만 매수 
                    {
                        await New_buying(item);
                    }
                }
            }
        }


        public static async Task New_buying(Newstock newstock) //신규매수 종목&가동조건 전달
        {
            string itemcode = newstock.code;
            string condition = newstock.condition;
            Market_Item Market = Form1.Market_Item_List[itemcode];

            if (Form1.form1.매수_ON)
            {
                // 1. 필요한 검색을 미리 한 번만 수행 (O(N) 호출 횟수 감소)
                var existingAnB = form1.ABC_List.Find(o => o.Code == itemcode && o.Loc == "AnB");
                var existingAnBnC = form1.ABC_List.Find(o => o.Code == itemcode && o.Loc == "AnBnC");

                if (str.신규매수방법.Equals("AnB"))
                {
                    if (위치별검색식리스트["신규_B"].이름.Equals(condition))
                    {
                        if (existingAnB != null)
                        {
                            await _1_중복확인_매수();
                        }
                    }
                }
                else if (str.신규매수방법.Equals("AnBnC"))
                {
                    if (위치별검색식리스트["신규_B"].이름.Equals(condition))
                    {
                        if (existingAnB == null) // 미리 검색된 결과 사용
                        {
                            form1.ABC_List.Add(new ABC(itemcode, "AnB"));
                        }
                    }

                    if (위치별검색식리스트["신규_C"].이름.Equals(condition))
                    {
                        if (existingAnBnC == null) // 미리 검색된 결과 사용
                        {
                            form1.ABC_List.Add(new ABC(itemcode, "AnBnC"));
                        }
                    }

                    if (위치별검색식리스트["신규_A"].이름.Equals(condition))
                    {
                        if (existingAnB != null && existingAnBnC != null)
                        {
                            await _1_중복확인_매수();
                        }
                    }
                }
                else if (str.신규매수방법.Equals("AnBoC"))
                {
                    if (위치별검색식리스트["신규_B"].Equals(condition))
                    {
                        if (existingAnB == null) // 미리 검색된 결과 사용
                        {
                            form1.ABC_List.Add(new ABC(itemcode, "AnB"));
                        }
                    }

                    if (위치별검색식리스트["신규_A"].이름.Equals(condition))
                    {
                        if (existingAnB != null)
                        {
                            await _1_중복확인_매수();
                        }
                    }

                    if (위치별검색식리스트["신규_C"].이름.Equals(condition) && GenieConfig.combo_new_or_C == 0)
                    {
                        await _1_중복확인_매수();
                    }
                }
                else // 그외의것 A B C AoB BoC AoBoC
                {
                    await _1_중복확인_매수();
                }

                async Task _1_중복확인_매수()
                {
                    bool 횟수제한()
                    {

                        bool result = true;

                        if (Get.신규횟수 + Form1.stockBalanceList.Count >= GenieConfig.TB_setjango) result = false;

                        if (GenieConfig.CB_신규횟수제한 && result)
                        {
                            if (GenieConfig.신규횟수 >= GenieConfig.TB_신규횟수제한)
                            {
                                result = false;
                            }
                        }
                        return result;
                    }

                    if (횟수제한())
                    {
                        if (!Form1.JumunItem_List.Values.Any(o => o.종목코드.Equals(itemcode) && o.검색식.Equals(condition)))
                        {
                            if (!Form1.stockBalanceList.ContainsKey(itemcode))
                            {
                                if (GenieConfig.CB_new_rebuy)
                                {
                                    await _2_a_매수();
                                }
                                else
                                {
                                    재매수 Item = Form1.Rebuystock_List.Find(o => o.Itemcode.Equals(itemcode));
                                    if (Item == null)
                                    {
                                        await _2_a_매수();
                                    }
                                }
                            }

                            async Task _2_a_매수()
                            {
                                int 현재가 = Market.현재가;
                                string 종목명 = Market.종목명;
                                int start_time = GenieConfig.MT_new_start_A;
                                int end_time = GenieConfig.MT_new_end_A;
                                double 주문값 = GenieConfig.TB_new_value_A;
                                int 시장가구분 = GenieConfig.combo_new_jumun_A;
                                int 비중선택 = GenieConfig.combo_new_choice_A;
                                double 주문비중 = GenieConfig.MT_new_ratio_A;
                                double TB_Limit_New = GenieConfig.TB_Limit_New_A;
                                string 위치 = "신규_A";
                                bool Result = false;

                                if (GenieConfig.CB_new_A && Form1.위치별검색식리스트["신규_A"].이름.Equals(condition))
                                {
                                    if (신규매수진행(Market, condition, 위치))
                                    {
                                        if (Tab_InterestGroup.관심그룹확인(위치, itemcode))
                                        {
                                            if (newstock.CatchTime.AddSeconds(TB_Limit_New) > DateTime.Now) Result = true;
                                        }
                                    }
                                }

                                if (GenieConfig.CB_new_B && Form1.위치별검색식리스트["신규_B"].이름.Equals(condition))
                                {
                                    위치 = "신규_B";

                                    if (신규매수진행(Market, condition, 위치))
                                    {
                                        start_time = GenieConfig.MT_new_start_B;
                                        end_time = GenieConfig.MT_new_end_B;
                                        주문값 = GenieConfig.TB_new_value_B;
                                        시장가구분 = GenieConfig.combo_new_jumun_B;
                                        비중선택 = GenieConfig.combo_new_choice_B;
                                        주문비중 = GenieConfig.MT_new_ratio_B;
                                        TB_Limit_New = GenieConfig.TB_Limit_New_B;

                                        if (Tab_InterestGroup.관심그룹확인(위치, itemcode))
                                        {
                                            if (newstock.CatchTime.AddSeconds(TB_Limit_New) > DateTime.Now) Result = true;
                                        }
                                    }
                                }

                                if (GenieConfig.CB_new_C && Form1.위치별검색식리스트["신규_C"].이름.Equals(condition))
                                {
                                    위치 = "신규_C";

                                    if (신규매수진행(Market, condition, 위치))
                                    {
                                        start_time = GenieConfig.MT_new_start_C;
                                        end_time = GenieConfig.MT_new_end_C;
                                        주문값 = GenieConfig.TB_new_value_C;
                                        시장가구분 = GenieConfig.combo_new_jumun_C;
                                        비중선택 = GenieConfig.combo_new_choice_C;
                                        주문비중 = GenieConfig.MT_new_ratio_C;
                                        TB_Limit_New = GenieConfig.TB_Limit_New_C;

                                        if (Tab_InterestGroup.관심그룹확인(위치, itemcode))
                                        {
                                            if (newstock.CatchTime.AddSeconds(TB_Limit_New) > DateTime.Now) Result = true;
                                        }
                                    }
                                }

                                if (Result)
                                {
                                    string 검색식 = 위치 + " [" + condition + "]";

                                    if (Method.RunTime(start_time, end_time))
                                    {
                                        int 취소시간 = GenieConfig.MTB_new_canceltime_A;
                                        int 취소N주문 = GenieConfig.combo_new_cancel_buy_A;
                                        int 반복횟수 = GenieConfig.MTB_new_repeat_A;

                                        if (GenieConfig.CB_new_B && 검색식.Contains(Form1.위치별검색식리스트["신규_B"].이름))
                                        {
                                            취소시간 = GenieConfig.MTB_new_canceltime_B;
                                            취소N주문 = GenieConfig.combo_new_cancel_buy_B;
                                            반복횟수 = GenieConfig.MTB_new_repeat_B;
                                        }

                                        if (GenieConfig.CB_new_C && 검색식.Contains(Form1.위치별검색식리스트["신규_C"].이름))
                                        {
                                            취소시간 = GenieConfig.MTB_new_canceltime_C;
                                            취소N주문 = GenieConfig.combo_new_cancel_buy_C;
                                            반복횟수 = GenieConfig.MTB_new_repeat_C;
                                        }

                                        string Screennum = GET.JumunScreen();

                                        if (현재가 > 1)
                                        {
                                            if (Method.매매확인_VI_모투가능확인(Market, 1))
                                            {
                                                await 신규매수실행(Market, 종목명, 검색식, Screennum, 주문값, 시장가구분, 비중선택, 주문비중, 취소시간, 취소N주문, 반복횟수);
                                            }
                                        }
                                        else
                                        {
                                            // [최적화 1] 리스트 순회(Find) 제거 -> Key로 즉시 확인 (O(1))
                                            // 데이터가 많아져도 렉이 발생하지 않습니다.
                                            if (!Form1.신규조회_List.ContainsKey(itemcode))
                                            {
                                                신규조회 Add = new 신규조회(itemcode, "info_NEWBUY", 0, 검색식);

                                                // [최적화 2] Dictionary에 안전하게 추가
                                                Form1.신규조회_List.TryAdd(itemcode, Add);

                                                // [최적화 3] 문자열 연결(+) 제거 -> 문자열 보간($) 사용
                                                // 메모리 할당을 최소화하여 가비지 컬렉터(GC) 부하를 줄입니다.
                                                string para = $"NEWBUY^{검색식}^{종목명}^{itemcode}^{Screennum}^{주문값}^{시장가구분}^{비중선택}^{주문비중}^{취소시간}^{취소N주문}^{반복횟수}";

                                                info.요청(itemcode, "info_NEWBUY", para, false);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string 시장 = "코스피";
                                        if (Market.Market.Equals("D")) 시장 = "코스닥";

                                        매매거부_메세지출력(종목명, "[신규매수 제한 '가동시간'] " + 시장 + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        public static async Task 신규매수실행(Market_Item market_Item, string 종목명, string 검색식, string Screennum, double 주문값, int 시장가구분, int 비중선택, double 주문비중, int 취소시간, int 취소N주문, int 반복횟수) //신규매수 주문넣기
        {
            if (Helper.NXT_매수매도_금지(true)) return;

            string itemcode = market_Item.종목코드;
            string 시장 = "코스피";
            int 현재가 = market_Item.현재가;
            double 등락율 = market_Item.등락율;

            if (market_Item.Market == "D") 시장 = "코스닥";

            if (GenieConfig.TB_신규주가이상 <= 현재가 && 현재가 <= GenieConfig.TB_신규주가이하)
            {
                if (GenieConfig.TB_신규등락률이상 <= 등락율 && 등락율 <= GenieConfig.TB_신규등락률이하)
                {
                    int 주문가 = 0;
                    int 주문수량 = 0;
                    int 예_주문가 = 0; //예수금 계산 가격

                    if (시장가구분 == 0) //  [거래구분]
                    {
                        주문수량 = Method.주문수량계산(null, 현재가, 주문비중, 비중선택);
                        예_주문가 = 현재가;
                    }
                    else
                    {
                        주문가 = Method.Order_price(주문값, 시장가구분, market_Item.종목코드, 현재가);

                        주문수량 = Method.주문수량계산(null, 주문가, 주문비중, 비중선택);
                        예_주문가 = 주문가;
                    }

                    // 1. [전략 및 예수금 확인] 신용/현금 여부와 살 수 있는 최대 수량을 먼저 계산합니다.
                    //    (기존의 느린 예수금확인() 함수를 완벽히 대체합니다.)
                    bool 신용주문 = 신용계산.신용주문_해야하나(1, 주문수량, market_Item, null, out int 실제주문수량);

                    if (GenieConfig.CB_신용_주문사용 && GenieConfig.CB_신용_가능만매수)
                    {
                        if (!market_Item.신용가능)
                        {
                            매매거부_메세지출력(종목명, "[신규매수 제한 '신용불가'] " + 시장 + "ㆍ" + 종목명 + "ㆍ" + 검색식);
                            return;
                        }
                    }

                    // 실제 주문할 수 있는 수량이 1주라도 있어야만 다음 단계를 진행합니다.
                    if (실제주문수량 >= 1)
                    {
                        // 2. [신규 주문 중복 확인] 동일 종목 중복 진입 방지
                        if (신규주문중복확인(itemcode, 검색식))
                        {
                            // 3. [매매 제한 로직 시작] 내부 함수 정의 (생략 없이 전체 포함)
                            bool 매매제한_통과 = true;

                            // A. 최대 잔고 수 제한 확인
                            if (Get.신규횟수 + Form1.stockBalanceList.Count >= GenieConfig.TB_setjango)
                            {
                                매매거부_메세지출력(종목명, "[신규매수 제한 '최대잔고'] " + 시장 + "ㆍ" + 종목명 + "ㆍ" + 검색식);
                                매매제한_통과 = false;
                            }

                            // B. 당일 신규 매수 횟수 제한 확인
                            if (GenieConfig.CB_신규횟수제한 && 매매제한_통과)
                            {
                                if (GenieConfig.신규횟수 >= GenieConfig.TB_신규횟수제한)
                                {
                                    매매거부_메세지출력(종목명, "[신규매수 제한 '매수횟수'] " + 시장 + "ㆍ" + 종목명 + "ㆍ" + 검색식);
                                    매매제한_통과 = false;
                                }
                            }

                            // C. 동일 종목 실시간 주문 중복 확인 (메인 장부 다이렉트 최적화 버전)
                            if (매매제한_통과)
                            {
                                // [지니 최적화] 보조장부 락(lock) 및 리스트 생성 과정을 폐지하고, 메인 장부에서 즉시 확인합니다.
                                // Any()를 통해 조건에 맞는 '신규_' 주문을 발견하는 즉시 검색을 멈추고 true를 반환합니다.
                                bool 신규주문_존재여부 = Form1.JumunItem_List.Values.Any(개별주문 =>
                                    개별주문.종목코드 == itemcode && 개별주문.검색식.Contains("신규_"));

                                if (신규주문_존재여부)
                                {
                                    매매제한_통과 = false; // 이미 신규 주문이 진행 중이면 중단
                                }
                            }

                            // 4. [최종 실행] 모든 제한을 통과했다면 주문을 실행합니다.
                            if (매매제한_통과)
                            {
                                GenieConfig.신규횟수++;
                                Get.신규횟수++;

                                string targetPos = "New_A";
                                if (검색식.Contains("신규_A")) targetPos = "New_A";
                                else if (검색식.Contains("신규_B")) targetPos = "New_B";
                                else if (검색식.Contains("신규_C")) targetPos = "New_C";

                                if (시장가구분 < 4)
                                {
                                    int Order번호 = GET.Order번호();

                                    // [장부 업데이트] 결정된 신용 여부(신용주문여부)에 따라 증거금을 정확히 깎습니다.
                                    홀딩잔고.예수금업데이트("매수", 예_주문가, 실제주문수량, "주문", itemcode, 신용주문);

                                    // [주문 객체 생성]
                                    JumunItem 새주문 = new JumunItem
                                    {
                                        신용주문 = 신용주문,
                                        대출일 = "",
                                        Deletetimer = 0,
                                        Screennum = Screennum,
                                        종목코드 = itemcode,
                                        종목명 = 종목명,
                                        주문번호 = "+++",
                                        원주문번호 = "---",
                                        검색식 = 검색식,
                                        주문값 = 주문값,
                                        시장가구분 = 시장가구분,
                                        취소시간 = 취소시간,
                                        취소N주문 = 취소N주문,
                                        반복횟수 = 반복횟수,
                                        비고 = "",
                                        Pos = "신규매수",
                                        주문수량 = 실제주문수량,    // 보정된 실제 수량
                                        주문가격 = 예_주문가,
                                        매수매도 = 1,
                                        비중 = 주문비중,
                                        비중단위 = 비중선택,
                                        취소timer = 취소시간,
                                        현재가 = 현재가,
                                        등락률 = 0,
                                        주문시간 = Get.TimeNow,
                                        미체결량 = 실제주문수량,
                                        주문취소 = true,
                                        가동전 = false,
                                        Tik_cap = Method.Find_Tik_Cap(현재가, 예_주문가, Form1.Market_Item_List[itemcode].Market),
                                        Tik_price = 현재가,
                                        수익률 = 0,
                                        주문동기화 = false,
                                        감시번호 = 0,
                                        Order번호 = Order번호,
                                        수익구분 = 0,
                                        NXT = NXT_server,
                                        주문시간_Ticks = DateTime.Now.Ticks
                                    };

                                    // 리스트 추가 및 큐 전송
                                    await Jumun.Add(새주문);
                                    ExecuteTrade.Que_order(새주문);
                                    스켈핑등록(검색식, Screennum, itemcode, 실제주문수량);
                                }
                                else
                                {
                                    // 분할 주문 시에도 실제 계산된 수량 적용
                                    await Tab_AccountManagement.분할주문("신규매수", 1, 시장가구분, itemcode, 종목명, 실제주문수량, 현재가, 검색식, 취소시간);
                                }

                                // 시그널 상태 업데이트
                                Newstock Item = NewStock_List.Find(o => o.Pos == targetPos && o.code.Equals(itemcode));
                                if (Item != null)
                                {
                                    Item.state = "이탈";
                                    Item.timer = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        // 수량이 0인 경우 (자금 부족 또는 비중 제한)
                        if (Form1.Market_Item_List[itemcode].매수증거금알림)
                        {
                            Form1.Market_Item_List[itemcode].매수증거금알림 = false;
                            매매거부_메세지출력(종목명, "[신규매수 제한] 예수금 부족으로 1주도 주문할 수 없습니다.");
                        }
                    }

                }
                else
                {
                    매매거부_메세지출력(종목명, "[신규매수 제한 '등락률범위'] " + 시장 + "ㆍ" + 종목명 + "ㆍ" + 검색식 + " 등락률(" + Form1.Market_Item_List[itemcode].등락율 + "%) 제한 등락률(" + GenieConfig.TB_신규등락률이상 + " ~ " + GenieConfig.TB_신규등락률이하 + ")");
                }
            }
            else
            {
                매매거부_메세지출력(종목명, "[신규매수 제한 '주가범위'] " + 시장 + "ㆍ" + 종목명 + "ㆍ" + 검색식 + " 주가(" + Form1.Market_Item_List[itemcode].현재가 + ")이 제한 주가(" + GenieConfig.TB_신규주가이상 + " ~ " + GenieConfig.TB_신규주가이하 + ")");
            }
        }

        public static void 매매거부_메세지출력(string 종목명, string 메세지)
        {
            if (!Form1.form1.신규거부로그_List.Contains(종목명))
            {
                Log.에러기록("");
                Log.에러기록(메세지);
                Log.에러기록("");
                Log.동작기록("");
                Log.동작기록(메세지);
                Log.동작기록("");

                Form1.form1.신규거부로그_List.Add(종목명);
            }
        }

        public static bool 신규주문중복확인(string 코드, string 검색식)
        {
            bool result = false;

            // 1. 설정값 로딩 (기존 로직 유지)
            bool CB_recatch = GenieConfig.CB_new_recatch_A;
            bool CB_익절재매수 = GenieConfig.CB_익절재매수A;
            string New_condition = Form1.위치별검색식리스트["신규_A"].이름;

            if (검색식.Contains("신규_B"))
            {
                CB_recatch = GenieConfig.CB_new_recatch_B;
                CB_익절재매수 = GenieConfig.CB_익절재매수B;
                New_condition = Form1.위치별검색식리스트["신규_B"].이름;
            }
            else if (검색식.Contains("신규_C"))
            {
                CB_recatch = GenieConfig.CB_new_recatch_C;
                CB_익절재매수 = GenieConfig.CB_익절재매수C;
                New_condition = Form1.위치별검색식리스트["신규_C"].이름;
            }

            // [최적화 1] List.Find(O(N)) 대신 HashSet.Contains(O(1)) 사용
            // string NewBuyItem = Form1.form1.NewBuyItem_List.Find(o => o.Equals(코드)); 대신
            bool itemExists = Form1.form1.NewBuyItem_List.Contains(코드);

            if (!itemExists) // 신규 종목인 경우
            {
                // HashSet.Add는 O(1)로 빠르고 중복 추가를 자동 방지합니다.
                Form1.form1.NewBuyItem_List.Add(코드);
                result = true;
            }
            else // 이미 목록에 존재하는 경우 (중복)
            {
                if (CB_recatch) // 재매수 옵션이 켜져 있으면 기본적으로 허용
                {
                    result = true;

                    if (CB_익절재매수) // 익절 재매수 옵션이 켜져 있으면 추가 조건 검사
                    {
                        // [O(N) 유지] List<재매수>.Find를 사용하여 재매수 종목 검색
                        재매수 종목 = Form1.Rebuystock_List.Find(o => o.Itemcode.Equals(코드));
                        if (종목 != null)
                        {
                            // 재매수 목록에서 찾은 경우 (O(N) 검색)
                            if (종목.결과.Equals("수익"))
                                result = true; // 수익을 낸 종목이면 재매수 허용
                            else
                                result = false; // 손실/기타 결과면 재매수 불허
                        }
                        // else: 재매수 목록에 없으면 result=true 유지 (기존 로직 의도)
                    }
                }
                else
                {
                    result = false; // 재매수 옵션이 꺼져 있으면 중복 확인 시 무조건 불허
                }
            }

            return result;
        }

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////       잔고익절 메소드모음         ////////////////

        public static void 잔고_익절(Stockbalance 잔고)
        {
            if (Form1.재시작) return;

            string sender = "CB_ik_one_A";
            bool 일회 = false;
            double 익절값 = GenieConfig.TB_ik_son_A;
            int 익절단위 = GenieConfig.combo_ik_A;
            double 익절비중 = GenieConfig.TB_ik_son_ratio_A;
            int 익절비중단위 = GenieConfig.combo_ik_ratio_A;
            double 주문값 = GenieConfig.TB_ik_value_A;
            int 매수매도 = GenieConfig.combo_ik_jumun_A;

            if (잔고.매매가능 && GET.총주문가능수량(잔고) > 0 && !잔고.매도정지)
            {
                if (GenieConfig.CB_ik_A)
                {
                    if (잔고.익절A)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (GenieConfig.CB_ik_one_A && 잔고.일회A.Equals("on"))
                        {
                            일회 = true;
                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (GenieConfig.CB_ik_B)
                {
                    sender = "CB_ik_one_B";
                    익절값 = GenieConfig.TB_ik_son_B;
                    익절단위 = GenieConfig.combo_ik_B;
                    익절비중 = GenieConfig.TB_ik_son_ratio_B;
                    익절비중단위 = GenieConfig.combo_ik_ratio_B;
                    주문값 = GenieConfig.TB_ik_value_B;
                    매수매도 = GenieConfig.combo_ik_jumun_B;

                    if (잔고.익절B)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (GenieConfig.CB_ik_one_B && 잔고.일회B.Equals("on"))
                        {
                            일회 = true;
                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (GenieConfig.CB_ik_C)
                {
                    sender = "CB_ik_one_C";
                    익절값 = GenieConfig.TB_ik_son_C;
                    익절단위 = GenieConfig.combo_ik_C;
                    익절비중 = GenieConfig.TB_ik_son_ratio_C;
                    익절비중단위 = GenieConfig.combo_ik_ratio_C;
                    주문값 = GenieConfig.TB_ik_value_C;
                    매수매도 = GenieConfig.combo_ik_jumun_C;

                    if (잔고.익절C)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (GenieConfig.CB_ik_one_C && 잔고.일회C.Equals("on"))
                        {
                            일회 = true;
                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (GenieConfig.CB_ik_D)
                {
                    sender = "CB_ik_one_D";
                    익절값 = GenieConfig.TB_ik_son_D;
                    익절단위 = GenieConfig.combo_ik_D;
                    익절비중 = GenieConfig.TB_ik_son_ratio_D;
                    익절비중단위 = GenieConfig.combo_ik_ratio_D;
                    주문값 = GenieConfig.TB_ik_value_D;
                    매수매도 = GenieConfig.combo_ik_jumun_D;

                    if (잔고.익절D)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (GenieConfig.CB_ik_one_D && 잔고.일회D.Equals("on"))
                        {
                            일회 = true;

                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (GenieConfig.CB_ik_E)
                {
                    sender = "CB_ik_one_E";
                    익절값 = GenieConfig.TB_ik_son_E;
                    익절단위 = GenieConfig.combo_ik_E;
                    익절비중 = GenieConfig.TB_ik_son_ratio_E;
                    익절비중단위 = GenieConfig.combo_ik_ratio_E;
                    주문값 = GenieConfig.TB_ik_value_E;
                    매수매도 = GenieConfig.combo_ik_jumun_E;

                    if (잔고.익절E)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (GenieConfig.CB_ik_one_E && 잔고.일회E.Equals("on"))
                        {
                            일회 = true;

                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (GenieConfig.CB_ik_F)
                {
                    sender = "CB_ik_one_F";
                    익절값 = GenieConfig.TB_ik_son_F;
                    익절단위 = GenieConfig.combo_ik_F;
                    익절비중 = GenieConfig.TB_ik_son_ratio_F;
                    익절비중단위 = GenieConfig.combo_ik_ratio_F;
                    주문값 = GenieConfig.TB_ik_value_F;
                    매수매도 = GenieConfig.combo_ik_jumun_F;

                    if (잔고.익절F)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (GenieConfig.CB_ik_one_F && 잔고.일회F.Equals("on"))
                        {
                            일회 = true;

                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (GenieConfig.CB_ik_G)
                {
                    sender = "CB_ik_one_G";
                    익절값 = GenieConfig.TB_ik_son_G;
                    익절단위 = GenieConfig.combo_ik_G;
                    익절비중 = GenieConfig.TB_ik_son_ratio_G;
                    익절비중단위 = GenieConfig.combo_ik_ratio_G;
                    주문값 = GenieConfig.TB_ik_value_G;
                    매수매도 = GenieConfig.combo_ik_jumun_G;

                    if (잔고.익절G)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (GenieConfig.CB_ik_one_G && 잔고.일회G.Equals("on"))
                        {
                            일회 = true;

                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (GenieConfig.CB_ik_H)
                {
                    sender = "CB_ik_one_H";
                    익절값 = GenieConfig.TB_ik_son_H;
                    익절단위 = GenieConfig.combo_ik_H;
                    익절비중 = GenieConfig.TB_ik_son_ratio_H;
                    익절비중단위 = GenieConfig.combo_ik_ratio_H;
                    주문값 = GenieConfig.TB_ik_value_H;
                    매수매도 = GenieConfig.combo_ik_jumun_H;

                    if (잔고.익절H)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (GenieConfig.CB_ik_one_H && 잔고.일회H.Equals("on"))
                        {
                            일회 = true;

                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                        }
                    }
                }

                if (GenieConfig.CB_ik_I)
                {
                    sender = "CB_ik_one_I";
                    익절값 = GenieConfig.TB_ik_son_I;
                    익절단위 = GenieConfig.combo_ik_I;
                    익절비중 = GenieConfig.TB_ik_son_ratio_I;
                    익절비중단위 = GenieConfig.combo_ik_ratio_I;
                    주문값 = GenieConfig.TB_ik_value_I;
                    매수매도 = GenieConfig.combo_ik_jumun_I;

                    if (잔고.익절I)
                    {
                        익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                    }
                    else
                    {
                        if (GenieConfig.CB_ik_one_I && 잔고.일회I.Equals("on"))
                        {
                            일회 = true;

                            익절주문전달(잔고, 일회, 익절값, 익절단위, 주문값, 매수매도, 익절비중단위, 익절비중, sender);
                        }
                    }
                }
            }
        }

        public static void 익절주문전달(Stockbalance 잔고, bool 일회, double 익절손익, int 익절손익단위, double Value, int 주문, int 익절비중단위, double 익절비중, string sender)
        {
            if (GET.익절그룹("익절").Contains(GET.그룹변환(잔고.매매그룹)))
            {
                bool 단위_기준금 = GenieConfig.CB_ik_기준금;

                long 매수기준금 = GenieConfig.MT_buying_standard;

                if (Method.주문_매매계산(잔고, 단위_기준금, 익절손익, 익절손익단위, "익절"))
                {
                    int 취소시간 = GenieConfig.MTB_ik_canceltime;
                    int 취소N주문 = GenieConfig.combo_ik_cancel_sell;
                    int 반복 = GenieConfig.MTB_ik_repeat;

                    if (GenieConfig.CBB_ik_CancelOrder > 0) // 취소사용 index > 0 
                    {
                        int 취소옵션_인덱스 = GenieConfig.CBB_ik_CancelOrder;

                        List<JumunItem> 최종_취소리스트 = Form1.JumunItem_List.Values
                            .Where(개별주문 =>
                                개별주문.종목코드 == 잔고.종목코드 &&
                                (
                                    취소옵션_인덱스 == 1 || // 1: 전부 취소
                                    (취소옵션_인덱스 == 2 && 개별주문.매수매도 == 1) || // 2: 매수 취소
                                    (취소옵션_인덱스 == 3 && 개별주문.매수매도 == 2)    // 3: 매도 취소
                                )
                            ).ToList();

                        // 필터링된 결과가 존재할 때만 내부 로직 실행
                        if (최종_취소리스트.Count > 0)
                        {
                            if (잔고.매매가능)
                            {
                                잔고.매매가능 = false;

                                // for문 대신 foreach를 사용하여 코드를 간결하게 만들고 안전하게 순회합니다.
                                foreach (JumunItem 취소할주문 in 최종_취소리스트)
                                {
                                    if (취소할주문.취소timer > 0)
                                    {
                                        취소할주문.취소timer = 0;
                                        취소할주문.취소시간 = 0;
                                        취소할주문.반복횟수 = 0;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // 취소할 항목이 없다면 신규 주문 발송 (외부 파라미터인 Value, 주문 등은 원형 유지)
                            if (일회)
                            {
                                if (익절N보전_sendorder(잔고, Value, 주문, 익절비중단위, 익절비중, "C익절(일회)_" + 일회차수(false), 취소시간, 취소N주문, 반복))
                                {
                                    일회차수(false);
                                }
                            }
                            else
                            {
                                string 차수 = 일회차수(true);
                                익절N보전_sendorder(잔고, Value, 주문, 익절비중단위, 익절비중, "C익절_" + 차수, 취소시간, 취소N주문, 반복);
                            }
                            잔고.매매가능 = true;
                        }
                    }
                    else
                    {
                        if (잔고.매매가능)
                        {
                            if (일회)
                            {
                                if (익절N보전_sendorder(잔고, Value, 주문, 익절비중단위, 익절비중, "익절(일회)_" + 일회차수(false), 취소시간, 취소N주문, 반복))
                                    일회차수(false);
                            }
                            else
                            {
                                string 차수 = 일회차수(true);
                                익절N보전_sendorder(잔고, Value, 주문, 익절비중단위, 익절비중, "익절_" + 차수, 취소시간, 취소N주문, 반복);
                            }
                        }
                    }

                    string 일회차수(bool 보전)
                    {
                        string 차수 = "0";
                        switch (sender)
                        {
                            case "CB_ik_one_A":
                                잔고.익절A = false;
                                if (보전) 잔고.보전A = "1";
                                잔고.일회A = "off " + str.today;
                                차수 = "A";
                                break;
                            case "CB_ik_one_B":
                                잔고.익절B = false;
                                if (보전) 잔고.보전B = "1";
                                잔고.일회B = "off " + str.today;
                                차수 = "B";
                                break;
                            case "CB_ik_one_C":
                                잔고.익절C = false;
                                if (보전) 잔고.보전C = "1";
                                잔고.일회C = "off " + str.today;
                                차수 = "C";
                                break;
                            case "CB_ik_one_D":
                                잔고.익절D = false;
                                if (보전) 잔고.보전D = "1";
                                잔고.일회D = "off " + str.today;
                                차수 = "D";
                                break;
                            case "CB_ik_one_E":
                                잔고.익절E = false;
                                if (보전) 잔고.보전E = "1";
                                잔고.일회E = "off " + str.today;
                                차수 = "E";
                                break;
                            case "CB_ik_one_F":
                                잔고.익절F = false;
                                if (보전) 잔고.보전F = "1";
                                잔고.일회F = "off " + str.today;
                                차수 = "F";
                                break;
                            case "CB_ik_one_G":
                                잔고.익절G = false;
                                if (보전) 잔고.보전G = "1";
                                잔고.일회G = "off " + str.today;
                                차수 = "G";
                                break;
                            case "CB_ik_one_H":
                                잔고.익절H = false;
                                if (보전) 잔고.보전H = "1";
                                잔고.일회H = "off " + str.today;
                                차수 = "H";
                                break;
                            case "CB_ik_one_I":
                                잔고.익절I = false;
                                if (보전) 잔고.보전I = "1";
                                잔고.일회I = "off " + str.today;
                                차수 = "I";
                                break;
                        }
                        return 차수;
                    }
                }
            }
        }


        public static void 잔고_보전(Stockbalance 잔고)
        {
            if (Form1.재시작) return;

            string 보전차수 = "A";
            double 보전손익 = GenieConfig.TB_ik_down_A;
            int 보전손익단위 = GenieConfig.combo_ik_down_A;
            int 보전비중단위 = GenieConfig.combo_ik_down_ratio_A;
            double 보전비중 = GenieConfig.TB_ik_down_ratio_A;
            double Value = GenieConfig.TB_ik_down_value_A;
            int 주문 = GenieConfig.combo_ik_down_jumun_A;
            bool 단위_기준금 = GenieConfig.CB_ik_down_기준금;

            string 매매그룹 = GET.그룹변환(잔고.매매그룹);
            bool result = false;

            if (잔고.매매가능 && GET.총주문가능수량(잔고) > 0 && !잔고.매도정지)
            {
                if (GenieConfig.CB_ik_down_A && 잔고.보전A.Equals("1"))
                {
                    result = true;
                }

                if (GenieConfig.CB_ik_down_B && 잔고.보전B.Equals("1"))
                {
                    보전손익 = GenieConfig.TB_ik_down_B;
                    보전손익단위 = GenieConfig.combo_ik_down_B;
                    보전비중 = GenieConfig.TB_ik_down_ratio_B;
                    보전비중단위 = GenieConfig.combo_ik_down_ratio_B;
                    보전차수 = "B";
                    Value = GenieConfig.TB_ik_down_value_B;
                    주문 = GenieConfig.combo_ik_down_jumun_B;
                    result = true;
                }
                if (GenieConfig.CB_ik_down_C && 잔고.보전C.Equals("1"))
                {
                    보전손익 = GenieConfig.TB_ik_down_C;
                    보전손익단위 = GenieConfig.combo_ik_down_C;
                    보전비중 = GenieConfig.TB_ik_down_ratio_C;
                    보전비중단위 = GenieConfig.combo_ik_down_ratio_C;
                    보전차수 = "C";
                    Value = GenieConfig.TB_ik_down_value_C;
                    주문 = GenieConfig.combo_ik_down_jumun_C;
                    result = true;
                }
                if (GenieConfig.CB_ik_down_D && 잔고.보전D.Equals("1"))
                {
                    보전손익 = GenieConfig.TB_ik_down_D;
                    보전손익단위 = GenieConfig.combo_ik_down_D;
                    보전비중 = GenieConfig.TB_ik_down_ratio_D;
                    보전비중단위 = GenieConfig.combo_ik_down_ratio_D;
                    보전차수 = "D";
                    Value = GenieConfig.TB_ik_down_value_D;
                    주문 = GenieConfig.combo_ik_down_jumun_D;
                    result = true;
                }
                if (GenieConfig.CB_ik_down_E && 잔고.보전E.Equals("1"))
                {
                    보전손익 = GenieConfig.TB_ik_down_E;
                    보전손익단위 = GenieConfig.combo_ik_down_E;
                    보전비중 = GenieConfig.TB_ik_down_ratio_E;
                    보전비중단위 = GenieConfig.combo_ik_down_ratio_E;
                    보전차수 = "E";
                    Value = GenieConfig.TB_ik_down_value_E;
                    주문 = GenieConfig.combo_ik_down_jumun_E;
                    result = true;
                }
                if (GenieConfig.CB_ik_down_F && 잔고.보전F.Equals("1"))
                {
                    보전손익 = GenieConfig.TB_ik_down_F;
                    보전손익단위 = GenieConfig.combo_ik_down_F;
                    보전비중 = GenieConfig.TB_ik_down_ratio_F;
                    보전비중단위 = GenieConfig.combo_ik_down_ratio_F;
                    보전차수 = "F";
                    Value = GenieConfig.TB_ik_down_value_F;
                    주문 = GenieConfig.combo_ik_down_jumun_F;
                    result = true;
                }
                if (GenieConfig.CB_ik_down_G && 잔고.보전G.Equals("1"))
                {
                    보전손익 = GenieConfig.TB_ik_down_G;
                    보전손익단위 = GenieConfig.combo_ik_down_G;
                    보전비중 = GenieConfig.TB_ik_down_ratio_G;
                    보전비중단위 = GenieConfig.combo_ik_down_ratio_G;
                    보전차수 = "G";
                    Value = GenieConfig.TB_ik_down_value_G;
                    주문 = GenieConfig.combo_ik_down_jumun_G;
                    result = true;
                }
                if (GenieConfig.CB_ik_down_H && 잔고.보전H.Equals("1"))
                {
                    보전손익 = GenieConfig.TB_ik_down_H;
                    보전손익단위 = GenieConfig.combo_ik_down_H;
                    보전비중 = GenieConfig.TB_ik_down_ratio_H;
                    보전비중단위 = GenieConfig.combo_ik_down_ratio_H;
                    보전차수 = "H";
                    Value = GenieConfig.TB_ik_down_value_H;
                    주문 = GenieConfig.combo_ik_down_jumun_H;
                    result = true;
                }
                if (GenieConfig.CB_ik_down_I && 잔고.보전I.Equals("1"))
                {
                    보전손익 = GenieConfig.TB_ik_down_I;
                    보전손익단위 = GenieConfig.combo_ik_down_I;
                    보전비중 = GenieConfig.TB_ik_down_ratio_I;
                    보전비중단위 = GenieConfig.combo_ik_down_ratio_I;
                    보전차수 = "I";
                    Value = GenieConfig.TB_ik_down_value_I;
                    주문 = GenieConfig.combo_ik_down_jumun_I;
                    result = true;
                }

                if (result)
                {
                    if (GET.익절그룹("익절").Contains(매매그룹))
                    {
                        보전주문전달(잔고, 보전손익, 단위_기준금, 보전손익단위, 보전차수, 보전비중단위, 보전비중, Value, 주문);
                    }
                }
            }
        }

        public static void 보전주문전달(Stockbalance 잔고, double 보전손익, bool 단위_기준금, int 보전손익단위, string 보전차수, int 보전비중단위, double 보전비중, double Value, int 주문)
        {
            if (Method.주문_매매계산(잔고, 단위_기준금, 보전손익, 보전손익단위, "보전"))
            {
                int 취소시간 = GenieConfig.MTB_ik_down_canceltime;
                int 취소N주문 = GenieConfig.combo_ik_down_cancel_sell;
                int 반복 = GenieConfig.MTB_ik_down_repeat;

                if (GenieConfig.CB_ik_down_CancelOrder)
                {
                    // [+] [지니 최적화] 보조장부와 lock 문을 완전히 폐지하고, 
                    // 메인 장부(JumunItem_List)에서 다이렉트로 해당 종목의 주문 리스트만 추출합니다.
                    List<JumunItem> 취소대상_리스트 = Form1.JumunItem_List.Values
                        .Where(개별주문 => 개별주문.종목코드 == 잔고.종목코드)
                        .ToList();

                    // 결과: 취소대상_리스트에는 해당 종목의 모든 주문이 담겨 있습니다.
                    if (취소대상_리스트.Count > 0)
                    {
                        if (잔고.매매가능)
                        {
                            잔고.매매가능 = false;

                            // for문 대신 foreach를 사용하여 더 직관적이고 빠르게 순회합니다.
                            foreach (JumunItem 취소할주문 in 취소대상_리스트)
                            {
                                if (취소할주문.취소timer > 0)
                                {
                                    취소할주문.취소timer = 0;
                                    취소할주문.취소시간 = 0;
                                    취소할주문.반복횟수 = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        // 취소할 항목이 없다면 보전 주문 발송
                        if (익절N보전_sendorder(잔고, Value, 주문, 보전비중단위, 보전비중, "C보전_" + 보전차수, 취소시간, 취소N주문, 반복))
                        {
                            보전조정(보전차수);
                        }

                        잔고.매매가능 = true;
                    }
                }
                else
                {
                    if (잔고.매매가능)
                    {
                        if (익절N보전_sendorder(잔고, Value, 주문, 보전비중단위, 보전비중, "보전_" + 보전차수, 취소시간, 취소N주문, 반복))
                            보전조정(보전차수);
                    }
                }
            }

            void 보전조정(string 차수)
            {
                switch (차수)
                {
                    case "A":
                        잔고.보전A = "3";
                        break;
                    case "B":
                        잔고.보전B = "3";
                        break;
                    case "C":
                        잔고.보전C = "3";
                        break;
                    case "D":
                        잔고.보전D = "3";
                        break;
                    case "E":
                        잔고.보전E = "3";
                        break;
                    case "F":
                        잔고.보전F = "3";
                        break;
                    case "G":
                        잔고.보전G = "3";
                        break;
                    case "H":
                        잔고.보전H = "3";
                        break;
                    case "I":
                        잔고.보전I = "3";
                        break;
                }
            }
        }

        public static bool 익절N보전_sendorder(Stockbalance 잔고, double 주문값, int 매수매도, int 비중단위, double 비중, string 검색식, int 취소시간, int 취소N주문, int 반복)
        {
            bool 매매확인 = false;


            if (ExecuteTrade.잔고주문_오더(잔고, 검색식, 2, 비중, 비중단위, 주문값, 매수매도, 취소시간, 취소N주문, 반복, "", 검색식, 0, false, 0))
            {
                매매확인 = true;
            }

            return 매매확인;
        }

        ////////////////////////       잔고익절 메소드모음         ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////





        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////       잔고손절 메소드모음         ////////////////

        public static void 잔고_손실매도(Stockbalance 잔고)
        {
            if (Form1.재시작) return;
            if (Helper.NXT_매수매도_금지(false)) return;

            string 손절차수 = "A";
            double 손절손익 = GenieConfig.TB_sell_son_A;
            int 손절손익단위 = GenieConfig.combo_sell_son_A;
            double 손절비중 = GenieConfig.TB_sell_ratio_A;
            int 손절비중단위 = GenieConfig.combo_sell_ratio_A;
            double Value = GenieConfig.TB_sell_value_A;
            int 주문 = GenieConfig.combo_sell_jumun_A;
            bool 단위_기준금 = GenieConfig.CB_sell_기준금;

            string 매매그룹 = GET.그룹변환(잔고.매매그룹);
            if (잔고.매매가능 && GET.총주문가능수량(잔고) > 0 && !잔고.매도정지)
            {
                if (GET.익절그룹("손절").Contains(매매그룹))
                {
                    int start = GenieConfig.MTB_sell_starttime;
                    int end = GenieConfig.MTB_sell_endtime;

                    if (GenieConfig.CB_sell_use_A && 잔고.손절A)
                    {
                        손절주문전달(잔고, start, end, 손절차수, 손절손익, 단위_기준금, 손절손익단위, 손절비중, 손절비중단위, Value, 주문);
                    }
                    if (GenieConfig.CB_sell_use_B && 잔고.손절B)
                    {
                        손절손익 = GenieConfig.TB_sell_son_B;
                        손절손익단위 = GenieConfig.combo_sell_son_B;
                        손절비중 = GenieConfig.TB_sell_ratio_B;
                        Value = GenieConfig.TB_sell_value_B;
                        주문 = GenieConfig.combo_sell_jumun_B;
                        손절비중단위 = GenieConfig.combo_sell_ratio_B;
                        손절차수 = "B";

                        손절주문전달(잔고, start, end, 손절차수, 손절손익, 단위_기준금, 손절손익단위, 손절비중, 손절비중단위, Value, 주문);
                    }
                    if (GenieConfig.CB_sell_use_C && 잔고.손절C)
                    {
                        손절손익 = GenieConfig.TB_sell_son_C;
                        손절손익단위 = GenieConfig.combo_sell_son_C;
                        손절비중 = GenieConfig.TB_sell_ratio_C;
                        Value = GenieConfig.TB_sell_value_C;
                        주문 = GenieConfig.combo_sell_jumun_C;
                        손절비중단위 = GenieConfig.combo_sell_ratio_B;
                        손절차수 = "C";

                        손절주문전달(잔고, start, end, 손절차수, 손절손익, 단위_기준금, 손절손익단위, 손절비중, 손절비중단위, Value, 주문);
                    }
                    if (GenieConfig.CB_sell_use_D && 잔고.손절D)
                    {
                        손절손익 = GenieConfig.TB_sell_son_D;
                        손절손익단위 = GenieConfig.combo_sell_son_D;
                        손절비중 = GenieConfig.TB_sell_ratio_D;
                        Value = GenieConfig.TB_sell_value_D;
                        주문 = GenieConfig.combo_sell_jumun_D;
                        손절비중단위 = GenieConfig.combo_sell_ratio_D;
                        손절차수 = "D";

                        손절주문전달(잔고, start, end, 손절차수, 손절손익, 단위_기준금, 손절손익단위, 손절비중, 손절비중단위, Value, 주문);
                    }
                    if (GenieConfig.CB_sell_use_E && 잔고.손절E)
                    {
                        손절손익 = GenieConfig.TB_sell_son_E;
                        손절손익단위 = GenieConfig.combo_sell_son_E;
                        손절비중 = GenieConfig.TB_sell_ratio_E;
                        Value = GenieConfig.TB_sell_value_E;
                        주문 = GenieConfig.combo_sell_jumun_E;
                        손절비중단위 = GenieConfig.combo_sell_ratio_E;
                        손절차수 = "E";

                        손절주문전달(잔고, start, end, 손절차수, 손절손익, 단위_기준금, 손절손익단위, 손절비중, 손절비중단위, Value, 주문);
                    }
                    if (GenieConfig.CB_sell_use_F && 잔고.손절F)
                    {
                        손절손익 = GenieConfig.TB_sell_son_F;
                        손절손익단위 = GenieConfig.combo_sell_son_F;
                        손절비중 = GenieConfig.TB_sell_ratio_F;
                        Value = GenieConfig.TB_sell_value_F;
                        주문 = GenieConfig.combo_sell_jumun_F;
                        손절비중단위 = GenieConfig.combo_sell_ratio_F;
                        손절차수 = "F";

                        손절주문전달(잔고, start, end, 손절차수, 손절손익, 단위_기준금, 손절손익단위, 손절비중, 손절비중단위, Value, 주문);
                    }



                }
            }
        }

        public static void 손절주문전달(Stockbalance 잔고, int start, int end, string 손절차수, double 손절손익, bool 단위_기준금, int 손절손익단위, double 손절비중, int 손절비중단위, double 주문값, int 매수매도)
        {
            if (손절손익 <= 0)
            {
                if (Method.RunTime(start, end))
                {
                    if (Method.주문_매매계산(잔고, 단위_기준금, 손절손익, 손절손익단위, "손절"))
                    {
                        if (GenieConfig.CB_sell_CancelOrder)
                        {
                            // [+] [지니 최적화] 보조장부와 lock 문을 완전히 폐지하고, 
                            // 메인 장부(JumunItem_List)에서 다이렉트로 해당 종목의 주문 리스트만 추출합니다.
                            List<JumunItem> 취소대상_리스트 = Form1.JumunItem_List.Values
                                .Where(개별주문 => 개별주문.종목코드 == 잔고.종목코드)
                                .ToList();

                            // 결과: 취소대상_리스트에는 해당 종목의 모든 주문이 담겨 있습니다.
                            if (취소대상_리스트.Count > 0)
                            {
                                if (잔고.매매가능)
                                {
                                    잔고.매매가능 = false;

                                    // for문 대신 foreach를 사용하여 더 직관적이고 빠르게 순회합니다.
                                    foreach (JumunItem 취소할주문 in 취소대상_리스트)
                                    {
                                        취소할주문.취소timer = 0;
                                        취소할주문.취소시간 = 0;
                                        취소할주문.반복횟수 = 0;
                                    }
                                }
                            }
                            else
                            {
                                손절_sendorder();
                                잔고.매매가능 = true;
                            }
                        }
                        else
                        {
                            if (잔고.매매가능)
                            {
                                손절_sendorder();
                            }
                        }
                    }
                }

                void 손절_sendorder() // 손절 에 따른 주문 정보 받기 for 기본설정 
                {
                    string 검색식 = "손절_" + 손절차수;
                    int 취소시간 = GenieConfig.MTB_sell_cancel_time;
                    int 취소N주문 = GenieConfig.combo_sell_cancel_sell;
                    int 반복 = GenieConfig.MTB_sell_cancel_repeat;

                    if (ExecuteTrade.잔고주문_오더(잔고, 검색식, 2, 손절비중, 손절비중단위, 주문값, 매수매도, 취소시간, 취소N주문, 반복, "", 검색식, 0, false, 0))
                    {
                        switch (검색식)
                        {
                            case "손절_A":
                                잔고.손절A = false;
                                break;
                            case "손절_B":
                                잔고.손절B = false;
                                break;
                            case "손절_C":
                                잔고.손절C = false;
                                break;
                            case "손절_D":
                                잔고.손절D = false;
                                break;
                            case "손절_E":
                                잔고.손절E = false;
                                break;
                            case "손절_F":
                                잔고.손절F = false;
                                break;
                        }
                    }
                }
            }
        }

        ////////////////////////       잔고손절 메소드모음         ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        /////////////////////              계좌 매도               ////////////////


        public static void 계좌청산()
        {
            long 매수기준금 = GenieConfig.MT_buying_standard;
            int 취소시간 = GenieConfig.MT_sell_time_cansel;

            double Value = GenieConfig.TB_sell_time_value;
            int jumun = GenieConfig.combo_sell_time_jumun;
            double ratio = GenieConfig.TB_sell_time_ratio;
            int gubun = GenieConfig.combo_sell_time_gubun;
            double trade_1 = GenieConfig.TB_sell_time_trade_1;
            double trade_2 = GenieConfig.TB_sell_time_trade_2;
            bool overlap = GenieConfig.CB_sell_time_overlap;
            bool Buy_stop = GenieConfig.CB_sell_time_Buystop;
            bool 매매취소 = GenieConfig.CB_sell_time_잔량취소;
            string position = "특정시간_청산";
            double low = GenieConfig.TB_silson_ik_W_1;
            double height = GenieConfig.TB_silson_ik_W_2;
            long 매입금 = (long)(GenieConfig.TB_sell_time_매입금 / 100 * 매수기준금);

            if (GenieConfig.CB_sell_time_use)
            {
                if (!Form1.매입금_Run_time && Acc.매입금 >= 매입금)
                {
                    Form1.매입금_Run_time = true;
                }

                if (Method.RunTime(GenieConfig.MT_sell_time_start, GenieConfig.MT_sell_time_end) && Form1.Run_time && Form1.매입금_Run_time)
                {
                    if (Form1.stockBalanceList.Count > 0)
                    {
                        Get.time_Run_silson_W = GenieConfig.MT_silson_repeat_W;
                        Get.time_Run_예상수익 = GenieConfig.MT_예상수익_repeat;
                        Get.time_Run_예상손실 = GenieConfig.MT_예상손실_repeat;

                        Acc_Sell_Run(취소시간, Value, jumun, ratio, gubun, trade_1, trade_2, position, overlap, Buy_stop, 매매취소, Form1.일괄취소_time, true, GenieConfig.TB_sell_time_ik_1, GenieConfig.TB_sell_time_ik_2);
                    }
                }
            }

            if (GenieConfig.CB_silson_use_W)
            {
                매입금 = (long)(GenieConfig.TB_silson_매입금_W / 100 * 매수기준금);

                if (!Form1.매입금_silson_W && Acc.매입금 >= 매입금)
                {
                    Form1.매입금_silson_W = true;
                }

                if (Method.RunTime(GenieConfig.MT_silson_start_W, GenieConfig.MT_silson_end_W) && Form1.Run_silson_W && Form1.매입금_silson_W)
                {
                    취소시간 = GenieConfig.MT_silson_cancel_W;
                    Value = GenieConfig.TB_silson_value_W;
                    jumun = GenieConfig.combo_silson_jumun_W;
                    ratio = GenieConfig.TB_silson_ratio_W;
                    gubun = GenieConfig.combo_silson_gubun_W;
                    trade_1 = GenieConfig.TB_silson_trade_W_1;
                    trade_2 = GenieConfig.TB_silson_trade_W_2;
                    overlap = GenieConfig.CB_silson_overlap_W;
                    Buy_stop = GenieConfig.CB_silson_Buystop_W;
                    매매취소 = GenieConfig.CB_silson_잔량취소;
                    position = "실현손익_청산";

                    if (Form1.Run_silson_trading)
                    {
                        Acc_Sell_Run(취소시간, Value, jumun, ratio, gubun, trade_1, trade_2, position, overlap, Buy_stop, 매매취소, Form1.일괄취소_silson_W, false, 0, 0);
                    }
                    else
                    {
                        if (GenieConfig.CB_silson_choice_W) //  "⇒"
                        {
                            Acc.실현손익금청산 = Method.계좌매도조건범위(GenieConfig.CB_silson_청산기준, Acc.실현손익, low, height, Acc.실현손익금청산, "실현손익금청산");

                            if (Acc.실현손익금청산.Equals("＊"))
                            {
                                Form1.Run_silson_trading = true;
                            }
                        }
                        else //  "→";
                        {
                            if (Method.계좌매도수익범위(GenieConfig.CB_silson_청산기준, Acc.실현손익, low, height))
                            {
                                Form1.Run_silson_trading = true;
                            }
                        }
                    }
                }
            }

            if (GenieConfig.CB_예상손실_use)
            {
                매입금 = (long)(GenieConfig.TB_예상손실_매입 / 100 * 매수기준금);

                if (!Form1.매입금_예상손실 && Acc.매입금 >= 매입금)
                {
                    Form1.매입금_예상손실 = true;
                }

                if (Method.RunTime(GenieConfig.MT_예상손실_start, GenieConfig.MT_예상손실_end) && Form1.Run_예상손실 && Form1.매입금_예상손실)
                {
                    취소시간 = GenieConfig.MT_예상손실_CanceTime;
                    Value = GenieConfig.TB_예상손실_value;
                    jumun = GenieConfig.combo_예상손실_jumun;
                    ratio = GenieConfig.TB_예상손실_ratio;
                    gubun = GenieConfig.combo_예상손실_gubun;
                    trade_1 = GenieConfig.TB_예상손실_trade_1;
                    trade_2 = GenieConfig.TB_예상손실_trade_2;
                    overlap = GenieConfig.CB_예상손실_overlap;
                    Buy_stop = GenieConfig.CB_예상손실_Buystop;
                    매매취소 = GenieConfig.CB_예상손실_잔량취소;
                    position = "예상손실_청산";
                    low = GenieConfig.TB_예상손실_ik_1;
                    height = GenieConfig.TB_예상손실_ik_2;

                    if (Form1.Run_예상손실_trading)
                    {
                        Acc_Sell_Run(취소시간, Value, jumun, ratio, gubun, trade_1, trade_2, position, overlap, Buy_stop, 매매취소, Form1.일괄취소_예상손실, false, 0, 0);
                    }
                    else
                    {
                        if (GenieConfig.CB_예상손실_choice) // "⇒"
                        {
                            Acc.예상손실청산 = Method.계좌매도조건범위(GenieConfig.CB_예상손실_청산기준, Acc.평가손익 + Acc.실현손익, low, height, Acc.예상손실청산, "예상손실청산");

                            if (Acc.예상손실청산.Equals("＊"))
                            {
                                Form1.Run_예상손실_trading = true;
                            }
                        }
                        else // "→";
                        {
                            if (Method.계좌매도수익범위(GenieConfig.CB_예상손실_청산기준, Acc.평가손익 + Acc.실현손익, low, height))
                            {
                                Form1.Run_예상손실_trading = true;
                            }
                        }
                    }
                }
            }

            if (GenieConfig.CB_예상수익사용)
            {
                매입금 = (long)(GenieConfig.TB_예상수익_매입금 / 100 * 매수기준금);

                if (!Form1.매입금_예상손익 && Acc.매입금 >= 매입금)
                {
                    Form1.매입금_예상손익 = true;
                }

                if (Acc.평가손익 + Acc.실현손익 > Get.예상수익_TS)
                    Get.예상수익_TS = Acc.평가손익 + Acc.실현손익;

                if (Method.RunTime(GenieConfig.MT_예상수익_start, GenieConfig.MT_예상수익_end) && Form1.Run_예상수익 && Form1.매입금_예상손익)
                {
                    취소시간 = GenieConfig.MT_예상수익_cancel;
                    Value = GenieConfig.TB_예상수익_value;
                    jumun = GenieConfig.combo_예상수익_jumun;
                    ratio = GenieConfig.TB_예상수익_ratio;
                    gubun = GenieConfig.combo_예상수익_gubun;
                    trade_1 = GenieConfig.TB_예상수익_trade_1;
                    trade_2 = GenieConfig.TB_예상수익_trade_2;
                    overlap = GenieConfig.CB_예상수익_overlap;
                    Buy_stop = GenieConfig.CB_예상수익_Buystop;
                    매매취소 = GenieConfig.CB_예상수익_잔량취소; //전체취소
                    position = "예상수익_청산";
                    low = GenieConfig.TB_예상수익_ik_1;
                    height = GenieConfig.TB_예상수익_ik_2;

                    if (Form1.Run_예상수익_trading)
                    {
                        Acc_Sell_Run(취소시간, Value, jumun, ratio, gubun, trade_1, trade_2, position, overlap, Buy_stop, 매매취소, Form1.일괄취소_예상수익, false, 0, 0);
                    }
                    else
                    {
                        if (GenieConfig.CB_예상수익TS)
                        {
                            double 상승값 = GenieConfig.TB_예상수익TS_상승값 * 10000;
                            double 하락값 = GenieConfig.TB_예상수익TS_하락값 * 10000;

                            if (GenieConfig.CB_예상수익TS_시작)
                            {
                                상승값 = 매수기준금 * GenieConfig.TB_예상수익TS_상승값 / 100;
                                하락값 = 매수기준금 * GenieConfig.TB_예상수익TS_하락값 / 100;
                            }

                            if (Get.예상수익_TS > 상승값)
                            {
                                if (Acc.평가손익 + Acc.실현손익 < Get.예상수익_TS + 하락값)
                                {
                                    str.청산검색식 = "예상수익_청산[TS]";
                                    Form1.Run_예상수익_trading = true;
                                }
                            }
                        }
                        else
                        {
                            str.청산검색식 = "";

                            if (GenieConfig.CB_예상수익_choice) // "⇒"
                            {
                                Acc.예상수익청산 = Method.계좌매도조건범위(GenieConfig.CB_예상수익_청산기준, Acc.평가손익 + Acc.실현손익, low, height, Acc.예상수익청산, "예상수익청산");

                                if (Acc.예상수익청산.Equals("＊"))
                                {
                                    Form1.Run_예상수익_trading = true;
                                }
                            }
                            else // "→";
                            {
                                if (Method.계좌매도수익범위(GenieConfig.CB_예상수익_청산기준, Acc.평가손익 + Acc.실현손익, low, height))
                                {
                                    Form1.Run_예상수익_trading = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void Acc_Sell_Run(int 취소시간, double 주문값, int 매수매도, double 비중, int 비중선택, double trade_1, double trade_2, string position, bool overlap, bool Buy_stop, bool 매매취소, bool 일괄취소, bool Time_sell, double low_ratio, double hight_ratio)
        {
            foreach (var 잔고 in stockBalanceList.Values)
            {
                if (Form1.재시작) return;

                string 매매그룹 = GET.그룹변환(잔고.매매그룹);

                if (!잔고.종목상태.Contains("거래정지") && !잔고.종목상태.Contains("동시호가"))
                    if (GET.익절그룹(position).Contains(매매그룹) && 잔고.매매가능 && GET.총주문가능수량(잔고) > 0)
                    {
                        if (Method.청산주문_매매범위(잔고, trade_1, trade_2))
                        {
                            if (Time_sell)
                            {
                                if (Method.시간매도수익범위(GenieConfig.CBB_sell_time_choice, low_ratio, hight_ratio, 잔고.평가손익, 잔고.수익률))
                                {
                                    주문전달();
                                }
                            }
                            else
                            {
                                if (position.Equals("예상손실_청산"))
                                {
                                    if (GenieConfig.CB_지수연동청산)
                                    {
                                        bool Run = false;

                                        if (Form1.Acc.피_외인 < 0 && Form1.Acc.피_기관 < 0 && Form1.Acc.피_프로그램 < 0) Run = true;

                                        if (잔고.시장.Equals("D"))
                                        {
                                            if (Form1.Acc.닥_외인 < 0 && Form1.Acc.닥_기관 < 0 && Form1.Acc.닥_프로그램 < 0) Run = true;
                                        }

                                        if (Run)
                                        {
                                            if (GenieConfig.CB_지수연동범위사용)
                                            {
                                                if (Method.지수연동_범위(true, GenieConfig.combo_지수연동이하, GenieConfig.TB_지수연동이하, 잔고.평가손익, 잔고.수익률))
                                                { 주문전달(); }

                                                if (Method.지수연동_범위(false, GenieConfig.combo_지수연동이상, GenieConfig.TB_지수연동이상, 잔고.평가손익, 잔고.수익률))
                                                { 주문전달(); }
                                            }
                                            else
                                            {
                                                주문전달();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        주문전달();
                                    }
                                }
                                else
                                {
                                    주문전달();
                                }
                            }
                        }
                    }

                void 주문전달()
                {
                    if (Form1.form1.매수_ON || Form1.form1.매도_ON)
                    {
                        if (Buy_stop)
                        {
                            if (Form1.form1.RB_buy_run.Checked)
                            {
                                Form1.form1.RB_sell_stop.Checked = true;
                                Form1.form1.RB_buy_stop.Checked = true;
                                Form1.form1.RB_sell_run.Checked = true;
                            }
                        }

                        if (일괄취소 && 매매취소)
                        {
                            일괄취소가동();
                            Form1.form1.미체결일괄취소();
                        }
                    }

                    if (Buy_stop)
                    {
                        //List<JumunItem> 매수_List = Form1.JumunItem_List.Values
                        //                                 .Where(o => o.매수매도 == 1)
                        //                                 .ToList(); // 스냅샷 생성

                        // [지니 최적화] 전체 주문 중 '매수' 주문만 추출 (LINQ 제거로 메모리 절약)

                        // 1. 결과를 담을 빈 리스트 생성
                        List<JumunItem> 매수_List = new List<JumunItem>();

                        // 2. Form1.JumunItem_List를 직접 돕니다. 
                        // (.Values를 쓰지 않고 이렇게 도는 것이 메모리를 가장 적게 씁니다)
                        foreach (var kvp in Form1.JumunItem_List)
                        {
                            JumunItem item = kvp.Value;

                            // 3. 매수매도 구분이 '1'(매수)인 것만 담습니다.
                            if (item.매수매도 == 1)
                            {
                                매수_List.Add(item);
                            }
                        }

                        // 결과: 매수_List에는 모든 종목의 매수 주문이 담깁니다.

                        if (매수_List.Count > 0)
                        {
                            for (int n = 0; n < 매수_List.Count; n++)
                            {
                                if (!매수_List[n].주문번호.Contains("+") && 매수_List[n].취소timer > 0)
                                {
                                    매수_List[n].반복횟수 = 0;
                                    매수_List[n].취소시간 = 0;
                                    매수_List[n].취소timer = 0;

                                    매수_List[n].비고 = "계좌청산_매수취소 '취소'";
                                }
                            }
                        }
                    }

                    string 검색식 = position;
                    if (position.Equals("예상수익_청산")) if (!str.청산검색식.Equals("")) 검색식 = str.청산검색식;

                    if (overlap)
                    {
                        while (true)
                        {
                            bool off = true;
                            if (Method.청산주문_매매범위(잔고, trade_1, trade_2))
                            {
                                if (ExecuteTrade.잔고주문_오더(잔고, 검색식, 2, 비중, 비중선택, 주문값, 매수매도, 취소시간, 0, 0, "계좌청산", position, 0, true, 0))
                                {
                                    계좌매도가동(잔고);
                                    off = false;
                                }
                            }
                            if (off) break;
                        }
                    }
                    else
                    {
                        if (Method.매매진입_가능여부(잔고.종목코드, 검색식))
                        {
                            if (ExecuteTrade.잔고주문_오더(잔고, 검색식, 2, 비중, 비중선택, 주문값, 매수매도, 취소시간, 0, 0, "계좌청산", position, 0, true, 0))
                                계좌매도가동(잔고);
                        }
                    }
                }
            }

            void 계좌매도가동(Stockbalance 잔고)
            {
                잔고.매도기준update = false;

                switch (position)
                {
                    case "특정시간_청산":
                        Form1.Run_time = false;
                        Get.time_Run_time = GenieConfig.MT_sell_time_repeat;
                        break;

                    case "실현손익_청산":
                        Form1.Run_silson_W = false;
                        Get.time_Run_silson_W = GenieConfig.MT_silson_repeat_W;
                        break;

                    case "예상손실_청산":
                        Form1.Run_예상손실 = false;
                        Get.time_Run_예상손실 = GenieConfig.MT_예상손실_repeat;
                        break;

                    case "예상수익_청산":
                        Form1.Run_예상수익 = false;
                        Get.time_Run_예상수익 = GenieConfig.MT_예상수익_repeat;
                        break;
                }
            }

            void 일괄취소가동()
            {
                switch (position)
                {
                    case "특정시간_청산":
                        Form1.일괄취소_time = false;
                        break;

                    case "실현손익_청산":
                        Form1.일괄취소_silson_W = false;
                        break;

                    case "예상손실_청산":
                        Form1.일괄취소_예상손실 = false;
                        break;

                    case "예상수익_청산":
                        Form1.일괄취소_예상수익 = false;
                        break;
                }
            }
        }

        /////////////////////              계좌 매도               ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        /////////////////////              시간 청산               ////////////////


        public static void TimeSell(Stockbalance 잔고)
        {
            if (Form1.재시작) return;
            if (잔고.잔고청산 && !잔고.매도정지)
            {
                if (Helper.NXT_매수매도_금지(false))
                {
                    if (잔고.수익률 < 0.3) return;
                }

                long 매수기준금 = GenieConfig.MT_buying_standard;
                bool 단위_기준금 = GenieConfig.CB_TimeSell_기준금;
                int StartTime = GenieConfig.TB_TimeSell_start_A;
                int CBB_TimeSell_start = GenieConfig.CBB_TimeSell_start_A;
                int 거래일 = GenieConfig.TB_TimeSell_거래일_A;
                double 수익범위_1 = GenieConfig.TB_TimeSell_수익범위_1_A;
                double 수익범위_2 = GenieConfig.TB_TimeSell_수익범위_2_A;
                int 수익범위_구분 = GenieConfig.CBB_TimeSell_수익구분_A;
                double 매도비중 = GenieConfig.TB_TimeSell_매도비중_A;
                int CBB_매도비중 = GenieConfig.CBB_TimeSell_매도비중_A;
                double 매매범위_1 = GenieConfig.TB_TimeSell_매매범위_1_A;
                double 매매범위_2 = GenieConfig.TB_TimeSell_매매범위_2_A;
                double TB_주문가격 = GenieConfig.TB_TimeSell_주문가격_A;
                int CBB_주문가격 = GenieConfig.CBB_TimeSell_주문가격_A;
                bool 수익범위_choice = GenieConfig.CB_TimeSell_수익범위_choice_A;
                int 취소간격 = GenieConfig.MT_TimeSell_취소간격_A;
                string position = "잔고시간청산_A";

                double 매입금1 = GenieConfig.TB_TimeSell_매입금1_A / 100 * 매수기준금;
                double 매입금2 = GenieConfig.TB_TimeSell_매입금2_A / 100 * 매수기준금;
                long 총매입금 = 잔고.매입금액 + 잔고.신용_매입금액;

                if (GenieConfig.CB_TimeSell_A)
                {
                    if (!잔고.TimeSell_매입금_A && 매입금1 <= 총매입금 && 총매입금 <= 매입금2) if (매매검사()) 잔고.TimeSell_매입금_A = true;
                    if (잔고.TimeSell_매입금_A && 잔고.시간청산반복_A == 0 && 매매검사()) 주문();
                    if (잔고.시간청산반복_A > 0) 잔고.시간청산반복_A--;
                }

                if (GenieConfig.CB_TimeSell_B)
                {
                    StartTime = GenieConfig.TB_TimeSell_start_B;
                    CBB_TimeSell_start = GenieConfig.CBB_TimeSell_start_B;
                    거래일 = GenieConfig.TB_TimeSell_거래일_B;
                    수익범위_1 = GenieConfig.TB_TimeSell_수익범위_1_B;
                    수익범위_2 = GenieConfig.TB_TimeSell_수익범위_2_B;
                    수익범위_구분 = GenieConfig.CBB_TimeSell_수익구분_B;
                    매도비중 = GenieConfig.TB_TimeSell_매도비중_B;
                    CBB_매도비중 = GenieConfig.CBB_TimeSell_매도비중_B;
                    매매범위_1 = GenieConfig.TB_TimeSell_매매범위_1_B;
                    매매범위_2 = GenieConfig.TB_TimeSell_매매범위_2_B;
                    TB_주문가격 = GenieConfig.TB_TimeSell_주문가격_B;
                    CBB_주문가격 = GenieConfig.CBB_TimeSell_주문가격_B;
                    수익범위_choice = GenieConfig.CB_TimeSell_수익범위_choice_B;
                    취소간격 = GenieConfig.MT_TimeSell_취소간격_B;
                    position = "잔고시간청산_B";

                    매입금1 = GenieConfig.TB_TimeSell_매입금1_B / 100 * 매수기준금;
                    매입금2 = GenieConfig.TB_TimeSell_매입금2_B / 100 * 매수기준금;

                    if (!잔고.TimeSell_매입금_B && 매입금1 <= 총매입금 && 총매입금 <= 매입금2) if (매매검사()) 잔고.TimeSell_매입금_B = true;
                    if (잔고.TimeSell_매입금_B && 잔고.시간청산반복_B == 0 && 매매검사()) 주문();
                    if (잔고.시간청산반복_B > 0) 잔고.시간청산반복_B--;
                }

                if (GenieConfig.CB_TimeSell_C)
                {
                    StartTime = GenieConfig.TB_TimeSell_start_C;
                    CBB_TimeSell_start = GenieConfig.CBB_TimeSell_start_C;
                    거래일 = GenieConfig.TB_TimeSell_거래일_C;
                    수익범위_1 = GenieConfig.TB_TimeSell_수익범위_1_C;
                    수익범위_2 = GenieConfig.TB_TimeSell_수익범위_2_C;
                    수익범위_구분 = GenieConfig.CBB_TimeSell_수익구분_C;
                    매도비중 = GenieConfig.TB_TimeSell_매도비중_C;
                    CBB_매도비중 = GenieConfig.CBB_TimeSell_매도비중_C;
                    매매범위_1 = GenieConfig.TB_TimeSell_매매범위_1_C;
                    매매범위_2 = GenieConfig.TB_TimeSell_매매범위_2_C;

                    TB_주문가격 = GenieConfig.TB_TimeSell_주문가격_C;
                    CBB_주문가격 = GenieConfig.CBB_TimeSell_주문가격_C;
                    수익범위_choice = GenieConfig.CB_TimeSell_수익범위_choice_C;
                    취소간격 = GenieConfig.MT_TimeSell_취소간격_C;
                    position = "잔고시간청산_C";

                    매입금1 = GenieConfig.TB_TimeSell_매입금1_C / 100 * 매수기준금;
                    매입금2 = GenieConfig.TB_TimeSell_매입금2_C / 100 * 매수기준금;

                    if (!잔고.TimeSell_매입금_C && 매입금1 <= 총매입금 && 총매입금 <= 매입금2) if (매매검사()) 잔고.TimeSell_매입금_C = true;
                    if (잔고.TimeSell_매입금_C && 잔고.시간청산반복_C == 0 && 매매검사()) 주문();
                    if (잔고.시간청산반복_C > 0) 잔고.시간청산반복_C--;
                }

                bool 매매검사()
                {
                    bool result = false;

                    if (CBB_TimeSell_start == 0)
                    {
                        if (Get.TimeNow > StartTime) 검사();

                    }
                    else
                    {
                        if (잔고.초기매수일.AddSeconds(StartTime) < DateTime.Now) 검사();
                    }

                    void 검사()
                    {
                        TimeSpan 매매일 = DateTime.Now.Date - 잔고.초기매수일.Date;
                        if (매매일.Days <= 거래일)
                        {
                            string 매매그룹 = GET.그룹변환(잔고.매매그룹);
                            if (GET.익절그룹(position).Contains(매매그룹))
                            {
                                if (!수익범위_choice) // →
                                {
                                    if (Method.수익범위(false, 단위_기준금, 잔고, 수익범위_1, 수익범위_2, 수익범위_구분, position))
                                    {
                                        result = true;

                                    }
                                }
                                else // ⇒
                                {
                                    switch (position)
                                    {
                                        case "잔고시간청산_A":
                                            잔고.시간청산동작_A = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, 수익범위_1, 수익범위_2, 수익범위_구분, 잔고.시간청산동작_A, "A", false);
                                            if (잔고.시간청산동작_A.Equals("X"))
                                            {
                                                result = true;
                                            }
                                            break;

                                        case "잔고시간청산_B":
                                            잔고.시간청산동작_B = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, 수익범위_1, 수익범위_2, 수익범위_구분, 잔고.시간청산동작_B, "B", false);
                                            if (잔고.시간청산동작_B.Equals("X"))
                                            {
                                                result = true;
                                            }
                                            break;

                                        case "잔고시간청산_C":
                                            잔고.시간청산동작_C = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, 수익범위_1, 수익범위_2, 수익범위_구분, 잔고.시간청산동작_C, "C", false);
                                            if (잔고.시간청산동작_C.Equals("X"))
                                            {
                                                result = true;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    return result;
                }

                void 주문()
                {
                    if (Method.청산주문_매매범위(잔고, 매매범위_1, 매매범위_2))
                    {
                        List<JumunItem> 조건맞는_주문리스트 = Form1.JumunItem_List.Values
                            .Where(개별주문 => 개별주문.종목코드 == 잔고.종목코드 && !개별주문.검색식.Contains("잔고시간청산"))
                            .ToList();

                        // 2. 조건에 맞는 주문이 존재하는지 확인
                        if (조건맞는_주문리스트.Count > 0)
                        {
                            // --- [CASE A] 미체결 주문이 있는 경우: 주문 취소 준비 ---
                            잔고.매매가능 = false;

                            // 필터링된 항목을 순회하며 상태 초기화
                            foreach (JumunItem 취소할주문 in 조건맞는_주문리스트)
                            {
                                // JumunItem 객체의 속성을 수정 (원본 딕셔너리에 즉시 반영됨)
                                취소할주문.반복횟수 = 0;
                                취소할주문.취소시간 = 0;
                                취소할주문.취소timer = 0;
                                취소할주문.비고 = "잔고 시간청산_미체결 '취소'";
                            }
                        }
                        else
                        {
                            잔고.매도기준update = false;
                            잔고.매도기준 = 잔고.보유수량;

                            TimeSell_Run();
                            잔고.매매가능 = true;
                        }
                    }

                    void TimeSell_Run()
                    {
                        while (true)
                        {
                            bool off = true;
                            if (Method.청산주문_매매범위(잔고, 매매범위_1, 매매범위_2))
                            {
                                if (ExecuteTrade.잔고주문_오더(잔고, position, 2, 매도비중, CBB_매도비중, TB_주문가격, CBB_주문가격, 취소간격, 0, 0, "", position, 0, true, 매매범위_1))
                                {
                                    주문_완료();
                                    off = false;
                                }
                            }

                            if (off) break;
                        }

                        void 주문_완료()
                        {
                            switch (position)
                            {
                                case "잔고시간청산_A":
                                    잔고.시간청산반복_A = GenieConfig.MT_TimeSell_반복간격_A;
                                    break;
                                case "잔고시간청산_B":
                                    잔고.시간청산반복_B = GenieConfig.MT_TimeSell_반복간격_B;
                                    break;
                                case "잔고시간청산_C":
                                    잔고.시간청산반복_C = GenieConfig.MT_TimeSell_반복간격_C;
                                    break;
                            }
                        }
                    }
                }
            }
        }



        /////////////////////              시간 청산               ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////            상,하 전량매도               ////////////////

        public static void 상하_전량매도(Stockbalance 잔고)
        {
            if (Form1.재시작) return;

            if (잔고.매매가능 && !잔고.매도정지)
            {
                // --- 1. 상한가 청산 조건 확인 ---
                if (GenieConfig.CB_상전량청산 &&
                    잔고.종목상태.Contains("상한가") &&
                    잔고.수익률 > 0)
                {
                    ExecuteLiquidationOrder(잔고, "상한_전량청산");
                }

                // --- 2. 하한가 청산 조건 확인 ---
                if (GenieConfig.CB_하전량청산 &&
                    잔고.종목상태.Contains("하한가"))
                {
                    ExecuteLiquidationOrder(잔고, "하한_전량청산");
                }
            }
        }

        // 중복 코드를 방지하기 위해 별도 메서드로 분리
        private static void ExecuteLiquidationOrder(dynamic 잔고, string 검색식)
        {
            // [+] [지니 최적화] 보조장부와 lock 문을 완전히 폐지하고, 
            // 메인 장부(JumunItem_List)에서 다이렉트로 종목코드와 검색식 조건을 동시에 필터링합니다.
            List<JumunItem> 충돌_주문리스트 = Form1.JumunItem_List.Values
                .Where(개별주문 =>
                    개별주문.종목코드 == 잔고.종목코드 &&
                    !개별주문.검색식.Contains(검색식) // 현재 실행하려는 청산 주문이 아닌 다른 주문들 필터링
                )
                .ToList();

            // 결과: 충돌_주문리스트에는 해당 종목이면서, 현재 로직(검색식)과 다른 주문들만 담깁니다.

            // 2. [분기 처리]
            if (충돌_주문리스트.Count > 0)
            {
                // --- [CASE A] 충돌하는 기존 주문이 있으면 -> 취소 처리 ---

                // 스냅샷 리스트를 순회하지만, 참조는 원본 객체를 가리키므로 즉시 반영됨
                foreach (JumunItem 취소할주문 in 충돌_주문리스트)
                {
                    취소할주문.반복횟수 = 0;
                    취소할주문.취소시간 = 0;
                    취소할주문.취소timer = 0;
                    취소할주문.비고 = 검색식 + "_미체결일괄'취소'";
                }

                // 기존 주문이 취소될 때까지 매매 잠금
                잔고.매매가능 = false;
            }
            else
            {
                // --- [CASE B] 방해되는 주문이 없으면 -> 신규 청산 주문 실행 ---

                if (GET.총주문가능수량(잔고) > 0)
                {
                    // form1.잔고주문_오더 호출 (인자는 기존 코드 유지)
                    ExecuteTrade.잔고주문_오더(잔고, 검색식, 2, 100, 3, 0, 0, 3600, 0, 0, "", 검색식, 0, false, 0);
                }
            }
        }


        //////////////////            상,하 전량매도               ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////





        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ///////////////                스캘핑 주문                ////////////////

        public static void 스켈핑등록(string 검색식, string Screennum, string 코드, int 주문수량)
        {
            if (GenieConfig.CB_scalping)
            {
                if (GenieConfig.CB_scalping_A)
                {
                    int 수량A = 0; if (GenieConfig.CB_ik_A && GenieConfig.CB_scalping_A_1) 수량A = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_A / 100);
                    int 수량B = 0; if (GenieConfig.CB_ik_B && GenieConfig.CB_scalping_A_2) 수량B = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_B / 100);
                    int 수량C = 0; if (GenieConfig.CB_ik_C && GenieConfig.CB_scalping_A_3) 수량C = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_C / 100);
                    int 수량D = 0; if (GenieConfig.CB_ik_D && GenieConfig.CB_scalping_A_4) 수량D = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_D / 100);
                    int 수량E = 0; if (GenieConfig.CB_ik_E && GenieConfig.CB_scalping_A_5) 수량E = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_E / 100);
                    int 수량F = 0; if (GenieConfig.CB_ik_F && GenieConfig.CB_scalping_A_6) 수량F = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_F / 100);
                    int 수량G = 0; if (GenieConfig.CB_ik_G && GenieConfig.CB_scalping_A_7) 수량G = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_G / 100);
                    int 수량H = 0; if (GenieConfig.CB_ik_H && GenieConfig.CB_scalping_A_8) 수량H = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_H / 100);
                    int 수량I = 0; if (GenieConfig.CB_ik_I && GenieConfig.CB_scalping_A_9) 수량I = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_I / 100);

                    Scalping 스켈등록 = new Scalping(검색식, 코드, Screennum, 수량A, 수량B, 수량C, 수량D, 수량E, 수량F, 수량G, 수량H, 수량I);
                    Form1.form1.Scalping_List.Add(스켈등록);
                }

                if (GenieConfig.CB_scalping_B)
                {
                    int 수량A = 0; if (GenieConfig.CB_ik_A && GenieConfig.CB_scalping_B_1) 수량A = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_A / 100);
                    int 수량B = 0; if (GenieConfig.CB_ik_B && GenieConfig.CB_scalping_B_2) 수량B = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_B / 100);
                    int 수량C = 0; if (GenieConfig.CB_ik_C && GenieConfig.CB_scalping_B_3) 수량C = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_C / 100);
                    int 수량D = 0; if (GenieConfig.CB_ik_D && GenieConfig.CB_scalping_B_4) 수량D = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_D / 100);
                    int 수량E = 0; if (GenieConfig.CB_ik_E && GenieConfig.CB_scalping_B_5) 수량E = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_E / 100);
                    int 수량F = 0; if (GenieConfig.CB_ik_F && GenieConfig.CB_scalping_B_6) 수량F = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_F / 100);
                    int 수량G = 0; if (GenieConfig.CB_ik_G && GenieConfig.CB_scalping_B_7) 수량G = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_G / 100);
                    int 수량H = 0; if (GenieConfig.CB_ik_H && GenieConfig.CB_scalping_B_8) 수량H = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_H / 100);
                    int 수량I = 0; if (GenieConfig.CB_ik_I && GenieConfig.CB_scalping_B_9) 수량I = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_I / 100);

                    Scalping 스켈등록 = new Scalping(검색식, 코드, Screennum, 수량A, 수량B, 수량C, 수량D, 수량E, 수량F, 수량G, 수량H, 수량I);
                    Form1.form1.Scalping_List.Add(스켈등록);
                }

                if (GenieConfig.CB_scalping_C)
                {
                    int 수량A = 0; if (GenieConfig.CB_ik_A && GenieConfig.CB_scalping_C_1) 수량A = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_A / 100);
                    int 수량B = 0; if (GenieConfig.CB_ik_B && GenieConfig.CB_scalping_C_2) 수량B = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_B / 100);
                    int 수량C = 0; if (GenieConfig.CB_ik_C && GenieConfig.CB_scalping_C_3) 수량C = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_C / 100);
                    int 수량D = 0; if (GenieConfig.CB_ik_D && GenieConfig.CB_scalping_C_4) 수량D = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_D / 100);
                    int 수량E = 0; if (GenieConfig.CB_ik_E && GenieConfig.CB_scalping_C_5) 수량E = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_E / 100);
                    int 수량F = 0; if (GenieConfig.CB_ik_F && GenieConfig.CB_scalping_C_6) 수량F = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_F / 100);
                    int 수량G = 0; if (GenieConfig.CB_ik_G && GenieConfig.CB_scalping_C_7) 수량G = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_G / 100);
                    int 수량H = 0; if (GenieConfig.CB_ik_H && GenieConfig.CB_scalping_C_8) 수량H = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_H / 100);
                    int 수량I = 0; if (GenieConfig.CB_ik_I && GenieConfig.CB_scalping_C_9) 수량I = (int)Math.Ceiling(주문수량 * GenieConfig.TB_ik_son_ratio_I / 100);

                    Scalping 스켈등록 = new Scalping(검색식, 코드, Screennum, 수량A, 수량B, 수량C, 수량D, 수량E, 수량F, 수량G, 수량H, 수량I);
                    Form1.form1.Scalping_List.Add(스켈등록);
                }
            }
        }

        public static void 스캘핑주문(string 종목코드, int 체_체결가, int 체_단위체결량, int 체_미체결량, string Screennum)
        {
            if (GenieConfig.CB_scalping)
            {
                if (stockBalanceList.TryGetValue(종목코드, out Stockbalance 잔고))
                {
                    string find검색식()
                    {
                        string 검색식 = "신규_A";
                        if (잔고.초기매수검색식.Contains("신규_B"))
                        {
                            검색식 = "신규_B";
                        }
                        if (잔고.초기매수검색식.Contains("신규_C"))
                        {
                            검색식 = "신규_C";
                        }
                        return 검색식;
                    }

                    Scalping Item = Form1.form1.Scalping_List.Find(o => o.코드.Equals(종목코드) && o.Screennum.Equals(Screennum));
                    if (Item != null)
                    {
                        if (GenieConfig.CB_scalping)
                        {
                            if (GenieConfig.CB_new_A && Item.검색식.Contains(Form1.위치별검색식리스트["신규_A"].이름))
                            {
                                스켈주문();
                            }
                            if (GenieConfig.CB_new_B && Item.검색식.Contains(Form1.위치별검색식리스트["신규_B"].이름))
                            {
                                스켈주문();
                            }
                            if (GenieConfig.CB_new_C && Item.검색식.Contains(Form1.위치별검색식리스트["신규_C"].이름))

                                스켈주문();
                        }
                    }

                    void 스켈주문()
                    {
                        int 주문수량 = 체_단위체결량;
                        double 주문비 = 0;
                        string 검색식 = "";

                        if (Item.수량A > 0)
                        {
                            if (체_단위체결량 >= Item.수량A)
                            {
                                주문수량 = Item.수량A;
                            }
                            Item.수량A -= 주문수량;
                            검색식 = find검색식() + " 스켈익절_A";

                            주문비 = GenieConfig.TB_ik_son_A;

                            주문오더();

                            체_단위체결량 -= 주문수량;

                            if (체_단위체결량 > 0)
                            {
                                B주문();
                            }
                            else
                            {
                                삭제();
                            }
                        }
                        else
                        {
                            B주문();
                        }

                        void B주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량B > 0)
                            {
                                if (체_단위체결량 >= Item.수량B)
                                {
                                    주문수량 = Item.수량B;
                                }
                                Item.수량B -= 주문수량;
                                검색식 = find검색식() + " 스켈익절_B";
                                주문비 = GenieConfig.TB_ik_son_B;

                                주문오더();

                                체_단위체결량 -= 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    C주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                C주문();
                            }
                        }

                        void C주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량C > 0)
                            {
                                if (체_단위체결량 >= Item.수량C)
                                {
                                    주문수량 = Item.수량C;
                                }
                                Item.수량C -= 주문수량;
                                검색식 = find검색식() + " 스켈익절_C";
                                주문비 = GenieConfig.TB_ik_son_C;

                                주문오더();

                                체_단위체결량 -= 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    D주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                D주문();
                            }
                        }

                        void D주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량D > 0)
                            {
                                if (체_단위체결량 >= Item.수량D)
                                {
                                    주문수량 = Item.수량D;
                                }
                                Item.수량D -= 주문수량;
                                검색식 = find검색식() + " 스켈익절_D";
                                주문비 = GenieConfig.TB_ik_son_D;

                                주문오더();

                                체_단위체결량 -= 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    E주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                E주문();
                            }
                        }

                        void E주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량E > 0)
                            {
                                if (체_단위체결량 >= Item.수량E)
                                {
                                    주문수량 = Item.수량E;
                                }
                                Item.수량E -= 주문수량;
                                검색식 = find검색식() + " 스켈익절_E";
                                주문비 = GenieConfig.TB_ik_son_E;

                                주문오더();

                                체_단위체결량 -= 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    F주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                F주문();
                            }
                        }

                        void F주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량F > 0)
                            {
                                if (체_단위체결량 >= Item.수량F)
                                {
                                    주문수량 = Item.수량F;
                                }
                                Item.수량F -= 주문수량;
                                검색식 = find검색식() + " 스켈익절_F";
                                주문비 = GenieConfig.TB_ik_son_F;

                                주문오더();

                                체_단위체결량 -= 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    G주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                G주문();
                            }
                        }

                        void G주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량G > 0)
                            {
                                if (체_단위체결량 >= Item.수량G)
                                {
                                    주문수량 = Item.수량G;
                                }
                                Item.수량G -= 주문수량;
                                검색식 = find검색식() + " 스켈익절_G";
                                주문비 = GenieConfig.TB_ik_son_G;

                                주문오더();

                                체_단위체결량 -= 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    H주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                H주문();
                            }
                        }

                        void H주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량H > 0)
                            {
                                if (체_단위체결량 >= Item.수량H)
                                {
                                    주문수량 = Item.수량H;
                                }
                                Item.수량H -= 주문수량;
                                검색식 = find검색식() + " 스켈익절_H";
                                주문비 = GenieConfig.TB_ik_son_H;

                                주문오더();

                                체_단위체결량 -= 주문수량;

                                if (체_단위체결량 > 0)
                                {
                                    I주문();
                                }
                                else
                                {
                                    삭제();
                                }
                            }
                            else
                            {
                                I주문();
                            }
                        }

                        void I주문()
                        {
                            주문수량 = 체_단위체결량;

                            if (Item.수량I > 0)
                            {
                                if (체_단위체결량 >= Item.수량I)
                                {
                                    주문수량 = Item.수량I;
                                }
                                Item.수량I -= 주문수량;
                                검색식 = find검색식() + " 스켈익절_I";
                                주문비 = GenieConfig.TB_ik_son_I;

                                주문오더();

                                삭제();
                            }
                            else
                            {
                                삭제();
                            }
                        }

                        void 주문오더()
                        {
                            double tax_ = Form1.TAX;
                            if (잔고.시장.Equals("E")) tax_ = 0;

                            int 주문가 = int.Parse(Method.주문가계산(종목코드, 주문비 + (tax_ * 100) + (Form1.수수료 * 100 * 2), 체_체결가, 잔고.현재가, "상한가").Split('&')[0]);
                            스캘핑_오더(잔고, 주문수량, 주문가, 검색식);
                        }

                        void 삭제()
                        {
                            if (체_미체결량 == 0)
                            {
                                Form1.form1.Scalping_List.Remove(Item);
                            }
                        }
                    }
                }
            }
        }


        public static void 스캘핑_오더(Stockbalance 잔고, int 주문수량, int 주문가격, string 검색식)
        {
            int 취소시간 = GenieConfig.MTB_ik_canceltime;
            int 주문유형 = 2;
            int 총주문가능수량 = GET.총주문가능수량(잔고);

            if (총주문가능수량 > 0)
            {
                if (총주문가능수량 < 주문수량)
                {
                    주문수량 = 총주문가능수량;
                }

                if (Method.매매확인_VI_모투가능확인(Form1.Market_Item_List[잔고.종목코드], 주문유형))
                {
                    신용계산.신용주문_분할매도_실행(잔고, 주문수량, async (is신용, 대출일, 수량) =>
                    {
                        await 주문(is신용, 대출일, 수량);
                    });

                    async Task 주문(bool 신용주문, string 대출일, int 주문수량)
                    {
                        int Order번호 = GET.Order번호();
                        string Screennum = GET.JumunScreen();

                        // [지니 최적화] 긴 생성자 대신 명확한 속성 할당 사용
                        JumunItem 새주문 = new JumunItem
                        {
                            신용주문 = 신용주문,
                            Deletetimer = 0,
                            Screennum = Screennum,
                            종목코드 = 잔고.종목코드,
                            종목명 = 잔고.종목명,
                            주문번호 = "+++",
                            원주문번호 = "---",
                            검색식 = 검색식,
                            주문값 = 0,
                            시장가구분 = 1,             // *주의: 9번째 인자값 '1' (고정)
                            취소시간 = 취소시간,
                            취소N주문 = 0,
                            반복횟수 = 0,
                            비고 = "",
                            Pos = 검색식,               // *주의: 14번째 인자에 '검색식' 재사용
                            주문수량 = 주문수량,
                            주문가격 = 주문가격,
                            매수매도 = 주문유형,        // *주의: 17번째 인자에 '주문유형' 변수 사용
                            비중 = 0,
                            비중단위 = 0,
                            취소timer = 취소시간,       // *주의: 20번째 인자 '취소시간' (재사용)
                            현재가 = 0,                 // *주의: 21번째 인자 0
                            등락률 = 0,                 // *주의: 22번째 인자 0
                            주문시간 = Get.TimeNow,
                            미체결량 = 주문수량,        // 미체결량 초기화
                            주문취소 = true,
                            가동전 = false,
                            Tik_cap = Method.Find_Tik_Cap(잔고.현재가, 주문가격, 잔고.시장),
                            Tik_price = 잔고.현재가,    // *주의: 28번째 인자 '잔고.현재가'
                            수익률 = 잔고.수익률,       // *주의: 29번째 인자 '잔고.수익률'
                            주문동기화 = false,
                            감시번호 = 0,
                            Order번호 = Order번호,
                            수익구분 = 0,
                            NXT = NXT_server,
                            주문시간_Ticks = DateTime.Now.Ticks
                        };

                        // 리스트 추가
                        await Jumun.Add(새주문);
                        ExecuteTrade.Que_order(새주문);



                    }
                }

                금액알림 = true;
            }
            else
            {
                if (금액알림)
                {
                    금액알림 = false;

                    Form1.AutoClosingAlram("[스켈핑 주문불가] 종목명:" + 잔고.종목명 + "주문가능 수량이 없어 ' 매도 '주문 할수 없습니다.", "주문불가", 10, "에러");
                }
            }
        }

        public static void 취소_스켈핑주문(string Screennum, string 코드b)
        {
            Scalping 종목 = Form1.form1.Scalping_List.Find(o => o.코드.Equals(코드b) && o.Screennum.Equals(Screennum));
            if (종목 != null)
            {
                Form1.form1.Scalping_List.Remove(종목);
            }
        }

        public static void 스켈핑차수조정(string 주문번호)
        {
            // 1. 주문번호(Key)로 주문 아이템을 즉시 찾습니다. (O(1) 속도)
            // Find()를 써서 전체를 뒤지는 것보다 훨씬 빠릅니다.
            if (Form1.JumunItem_List.TryGetValue(주문번호, out JumunItem jumun))
            {
                // 2. 해당 주문의 종목코드로 잔고를 찾습니다.
                if (stockBalanceList.TryGetValue(jumun.종목코드, out Stockbalance 잔고))
                {
                    // 3. 검색식에 따라 분기 처리 (switch 문 사용으로 가독성/성능 향상)
                    switch (jumun.검색식)
                    {
                        case "스켈핑_익절_A":
                            if (잔고.익절A) { 잔고.익절A = false; 잔고.보전A = "1"; }
                            break;
                        case "스켈핑_익절_B":
                            if (잔고.익절B) { 잔고.익절B = false; 잔고.보전B = "1"; }
                            break;
                        case "스켈핑_익절_C":
                            if (잔고.익절C) { 잔고.익절C = false; 잔고.보전C = "1"; }
                            break;
                        case "스켈핑_익절_D":
                            if (잔고.익절D) { 잔고.익절D = false; 잔고.보전D = "1"; }
                            break;
                        case "스켈핑_익절_E":
                            if (잔고.익절E) { 잔고.익절E = false; 잔고.보전E = "1"; }
                            break;
                        case "스켈핑_익절_F":
                            if (잔고.익절F) { 잔고.익절F = false; 잔고.보전F = "1"; }
                            break;
                        case "스켈핑_익절_G":
                            if (잔고.익절G) { 잔고.익절G = false; 잔고.보전G = "1"; }
                            break;
                        case "스켈핑_익절_H":
                            if (잔고.익절H) { 잔고.익절H = false; 잔고.보전H = "1"; }
                            break;
                        case "스켈핑_익절_I":
                            if (잔고.익절I) { 잔고.익절I = false; 잔고.보전I = "1"; }
                            break;
                    }
                }
            }
            else
            {
                // (선택 사항) 주문번호에 해당하는 주문이 딕셔너리에 없을 경우에 대한 로그 처리
                // Console_print($"오류: 주문번호 {주문번호}를 딕셔너리에서 찾을 수 없습니다.");
            }
        }

        ///////////////                스캘핑 주문                ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        public static void 트레일링_값갱신(Stockbalance 잔고)
        {
            if (잔고.트레일링값.Split('@').Length < 4) 잔고.트레일링값 += "@0";

            double.TryParse(잔고.트레일링값.Split('@')[0], out double 수익률);
            long.TryParse(잔고.트레일링값.Split('@')[1], out long 평가손익);
            long.TryParse(잔고.트레일링값.Split('@')[2], out long 예상손익);
            double.TryParse(잔고.트레일링값.Split('@')[3], out double 기준수익률);

            if (잔고.수익률 > 수익률) 수익률 = 잔고.수익률;
            if (잔고.평가손익 > 평가손익) 평가손익 = 잔고.평가손익;
            if (잔고.예상손익 > 예상손익) 예상손익 = 잔고.예상손익;
            if (잔고.기준수익률 > 기준수익률) 기준수익률 = 잔고.기준수익률;

            잔고.트레일링값 = 수익률 + "@" + 평가손익 + "@" + 예상손익 + "@" + 기준수익률;
            잔고.예상손익 = 잔고.누적손익 + 잔고.평가손익;
        }

        public static double 트레일값(Stockbalance 잔고, int index)
        {
            double.TryParse(잔고.트레일링값.Split('@')[index], out double result);
            return result;
        }

        public static double 트레일링스탑_구분(Stockbalance 잔고, int index)
        {
            double value = 0;
            switch (index)
            {
                case 0:
                    value = 잔고.수익률;
                    break;
                case 1:
                    value = 잔고.평가손익;
                    break;
                case 2:
                    value = 잔고.예상손익;
                    break;
                case 3:
                    value = 잔고.기준수익률;
                    break;
            }

            return value;
        }

        public static void 트레일링스탑(Stockbalance 잔고)
        {
            if (!잔고.매도정지)
            {
                bool 기준금 = GenieConfig.CB_TS_기준금;

                int CBB_index = GenieConfig.CBB_TS_upper_A;
                double 잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                double upper_value = 기준금_변환(GenieConfig.TB_TS_upper_A, CBB_index, 기준금);
                double down_value = 기준금_변환(GenieConfig.TB_TS_down_A, CBB_index, 기준금);
                double 트레일값_ = 트레일값(잔고, CBB_index);
                double 익절비중 = GenieConfig.TB_TS_ratio_A;
                int 익절비중단위 = GenieConfig.CBB_TS_ratio_A;
                double 주문_Value = GenieConfig.TB_TS_Jumun_A;
                int 주문 = GenieConfig.CBB_TS_Jumun_A;
                string 검색식 = "트레일링스탑_A";

                if (잔고.TS_1 && GenieConfig.CB_TS_A)
                {
                    if (트레일링스탑()) 잔고.TS_1 = false;
                }
                if (잔고.TS_2 && GenieConfig.CB_TS_B)
                {
                    CBB_index = GenieConfig.CBB_TS_upper_B;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(GenieConfig.TB_TS_upper_B, CBB_index, 기준금);
                    down_value = 기준금_변환(GenieConfig.TB_TS_down_B, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = GenieConfig.TB_TS_ratio_B;
                    익절비중단위 = GenieConfig.CBB_TS_ratio_B;
                    주문_Value = GenieConfig.TB_TS_Jumun_B;
                    주문 = GenieConfig.CBB_TS_Jumun_B;
                    검색식 = "트레일링스탑_B";

                    if (트레일링스탑()) 잔고.TS_2 = false;
                }
                if (잔고.TS_3 && GenieConfig.CB_TS_C)
                {
                    CBB_index = GenieConfig.CBB_TS_upper_C;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(GenieConfig.TB_TS_upper_C, CBB_index, 기준금);
                    down_value = 기준금_변환(GenieConfig.TB_TS_down_C, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = GenieConfig.TB_TS_ratio_C;
                    익절비중단위 = GenieConfig.CBB_TS_ratio_C;
                    주문_Value = GenieConfig.TB_TS_Jumun_C;
                    주문 = GenieConfig.CBB_TS_Jumun_C;
                    검색식 = "트레일링스탑_C";

                    if (트레일링스탑()) 잔고.TS_3 = false;
                }
                if (잔고.TS_4 && GenieConfig.CB_TS_D)
                {
                    CBB_index = GenieConfig.CBB_TS_upper_D;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(GenieConfig.TB_TS_upper_D, CBB_index, 기준금);
                    down_value = 기준금_변환(GenieConfig.TB_TS_down_D, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = GenieConfig.TB_TS_ratio_D;
                    익절비중단위 = GenieConfig.CBB_TS_ratio_D;
                    주문_Value = GenieConfig.TB_TS_Jumun_D;
                    주문 = GenieConfig.CBB_TS_Jumun_D;
                    검색식 = "트레일링스탑_D";

                    if (트레일링스탑()) 잔고.TS_4 = false;
                }
                if (잔고.TS_5 && GenieConfig.CB_TS_E)
                {
                    CBB_index = GenieConfig.CBB_TS_upper_E;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(GenieConfig.TB_TS_upper_E, CBB_index, 기준금);
                    down_value = 기준금_변환(GenieConfig.TB_TS_down_E, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = GenieConfig.TB_TS_ratio_E;
                    익절비중단위 = GenieConfig.CBB_TS_ratio_E;
                    주문_Value = GenieConfig.TB_TS_Jumun_E;
                    주문 = GenieConfig.CBB_TS_Jumun_E;
                    검색식 = "트레일링스탑_E";

                    if (트레일링스탑()) 잔고.TS_5 = false;
                }
                if (잔고.TS_6 && GenieConfig.CB_TS_F)
                {
                    CBB_index = GenieConfig.CBB_TS_upper_F;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(GenieConfig.TB_TS_upper_F, CBB_index, 기준금);
                    down_value = 기준금_변환(GenieConfig.TB_TS_down_F, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = GenieConfig.TB_TS_ratio_F;
                    익절비중단위 = GenieConfig.CBB_TS_ratio_F;
                    주문_Value = GenieConfig.TB_TS_Jumun_F;
                    주문 = GenieConfig.CBB_TS_Jumun_F;
                    검색식 = "트레일링스탑_F";

                    if (트레일링스탑()) 잔고.TS_6 = false;
                }
                if (잔고.TS_7 && GenieConfig.CB_TS_G)
                {
                    CBB_index = GenieConfig.CBB_TS_upper_G;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(GenieConfig.TB_TS_upper_G, CBB_index, 기준금);
                    down_value = 기준금_변환(GenieConfig.TB_TS_down_G, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = GenieConfig.TB_TS_ratio_G;
                    익절비중단위 = GenieConfig.CBB_TS_ratio_G;
                    주문_Value = GenieConfig.TB_TS_Jumun_G;
                    주문 = GenieConfig.CBB_TS_Jumun_G;
                    검색식 = "트레일링스탑_G";

                    if (트레일링스탑()) 잔고.TS_7 = false;
                }
                if (잔고.TS_8 && GenieConfig.CB_TS_H)
                {
                    CBB_index = GenieConfig.CBB_TS_upper_H;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(GenieConfig.TB_TS_upper_H, CBB_index, 기준금);
                    down_value = 기준금_변환(GenieConfig.TB_TS_down_H, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = GenieConfig.TB_TS_ratio_H;
                    익절비중단위 = GenieConfig.CBB_TS_ratio_H;
                    주문_Value = GenieConfig.TB_TS_Jumun_H;
                    주문 = GenieConfig.CBB_TS_Jumun_H;
                    검색식 = "트레일링스탑_H";

                    if (트레일링스탑()) 잔고.TS_8 = false;
                }
                if (잔고.TS_9 && GenieConfig.CB_TS_I)
                {
                    CBB_index = GenieConfig.CBB_TS_upper_I;
                    잔고평가값 = 트레일링스탑_구분(잔고, CBB_index);
                    upper_value = 기준금_변환(GenieConfig.TB_TS_upper_I, CBB_index, 기준금);
                    down_value = 기준금_변환(GenieConfig.TB_TS_down_I, CBB_index, 기준금);
                    트레일값_ = 트레일값(잔고, CBB_index);
                    익절비중 = GenieConfig.TB_TS_ratio_I;
                    익절비중단위 = GenieConfig.CBB_TS_ratio_I;
                    주문_Value = GenieConfig.TB_TS_Jumun_I;
                    주문 = GenieConfig.CBB_TS_Jumun_I;
                    검색식 = "트레일링스탑_I";

                    if (트레일링스탑()) 잔고.TS_9 = false;
                }

                bool 트레일링스탑()
                {
                    bool Run = false;
                    트레일링_값갱신(잔고);

                    if (트레일값_ > upper_value)
                    {
                        if (트레일값_ + down_value >= 잔고평가값)
                        {
                            if (GenieConfig.CB_TS_손실제한)
                            {
                                if (잔고.수익률 > 0 && 잔고.예상손익 > 0)
                                {
                                    TS_주문();
                                }
                                else
                                {
                                    bool true_ = true;
                                    if (검색식.Contains("A")) { true_ = 잔고.TS_알림_A; 잔고.TS_알림_A = false; }
                                    if (검색식.Contains("B")) { true_ = 잔고.TS_알림_B; 잔고.TS_알림_B = false; }
                                    if (검색식.Contains("C")) { true_ = 잔고.TS_알림_C; 잔고.TS_알림_C = false; }
                                    if (검색식.Contains("D")) { true_ = 잔고.TS_알림_D; 잔고.TS_알림_D = false; }
                                    if (검색식.Contains("E")) { true_ = 잔고.TS_알림_E; 잔고.TS_알림_E = false; }
                                    if (검색식.Contains("F")) { true_ = 잔고.TS_알림_F; 잔고.TS_알림_F = false; }
                                    if (검색식.Contains("G")) { true_ = 잔고.TS_알림_G; 잔고.TS_알림_G = false; }
                                    if (검색식.Contains("H")) { true_ = 잔고.TS_알림_H; 잔고.TS_알림_H = false; }
                                    if (검색식.Contains("I")) { true_ = 잔고.TS_알림_I; 잔고.TS_알림_I = false; }

                                    if (true_)
                                    {
                                        GridView_Print.DGV_Jumun(null, Form1.Get.TimeNow.ToString("##:##:##"), "취소", 잔고.종목명, 잔고.현재가, GET.매수매도str(2), "차수조정", 잔고.현재가, 0, 잔고.종목코드, "손실제한으로 TS 취소 됩니다. ", 0, 0, 검색식 + "_취소", "-", 주문_Value, 주문, 0); ;
                                    }
                                }
                            }
                            else
                            {
                                TS_주문();
                            }

                            void TS_주문()
                            {
                                if (Helper.NXT_매수매도_금지(false))
                                {
                                    if (잔고.수익률 < 0.3) return;
                                }

                                int 취소시간 = GenieConfig.MTB_TS_canceltime;
                                int 취소N주문 = GenieConfig.CBB_TS_cancel_sell;
                                int 반복 = GenieConfig.MTB_TS_repeat;

                                if (GenieConfig.CB_TS_취소후)
                                {
                                    // [+] [지니 최적화] 보조장부와 lock 문을 완전히 폐지하고, 
                                    // 메인 장부(JumunItem_List)에서 다이렉트로 해당 종목의 주문 리스트만 추출합니다.
                                    List<JumunItem> 취소대상_리스트 = Form1.JumunItem_List.Values
                                        .Where(개별주문 => 개별주문.종목코드 == 잔고.종목코드)
                                        .ToList();

                                    // 결과: 취소대상_리스트에는 해당 종목의 모든 주문이 담겨 있습니다.
                                    if (취소대상_리스트.Count > 0)
                                    {
                                        if (잔고.매매가능)
                                        {
                                            잔고.매매가능 = false;

                                            // for문 대신 foreach를 사용하여 더 직관적이고 빠르게 순회합니다.
                                            foreach (JumunItem 취소할주문 in 취소대상_리스트)
                                            {
                                                if (취소할주문.취소timer > 0)
                                                {
                                                    취소할주문.취소timer = 0;
                                                    취소할주문.취소시간 = 0;
                                                    취소할주문.반복횟수 = 0;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        익절N보전_sendorder(잔고, 주문_Value, 주문, 익절비중단위, 익절비중, "C" + 검색식, 취소시간, 취소N주문, 반복);
                                        Run = true;
                                        잔고.매매가능 = true;
                                    }
                                }
                                else
                                {
                                    if (잔고.매매가능)
                                    {
                                        익절N보전_sendorder(잔고, 주문_Value, 주문, 익절비중단위, 익절비중, 검색식, 취소시간, 취소N주문, 반복);
                                        Run = true;
                                    }
                                }
                            }
                        }
                    }

                    return Run;
                }
            }
        }

        public static double 기준금_변환(double value, int index, bool 기준금)
        {
            long 매수기준금 = GenieConfig.MT_buying_standard;

            if (index != 0)
            {
                if (기준금)
                {
                    value = (value / 100 * 매수기준금);
                }
                else
                {
                    value *= 10000;
                }
            }
            return value;
        }

        ///////////////                트레일링 스탑               ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////





    }
}
