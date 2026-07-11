using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks; // Newtonsoft.Json 대신 사용

namespace 지니64
{
    internal class RealDataDispatcher
    {
        // 1. 핸들러 매핑 딕셔너리 (JObject -> JsonElement 변경)
        private readonly Dictionary<string, Action<string, JsonElement>> _realDataHandlers;

        public RealDataDispatcher()
        {
            // 2. 생성자: 기능별 메서드 연결
            _realDataHandlers = new Dictionary<string, Action<string, JsonElement>>
            {
                { "조건검색", HandleConditionSearch },
                { "예상업종지수", HandleExpectedMarketIndex },
                { "업종지수", HandleMarketIndex },
                { "업종등락", HandleMarketFluctuation },
                { "주식예상체결", HandleStockExpectedConclusion },
                { "주식체결", HandleStockConclusion },
                { "주식호가잔량", HandleStockHoga },
                { "현물잔고", HandleAccountBalance },
                { "주문체결", HandleOrderConclusion }
            };
        }

        // 3. [최적화] 메인 진입점
        public void ProcessRealData(JsonElement root)
        {
            // [최적화] "trnm" 값 확인 (메모리 할당 없이 확인)
            if (!root.TryGetProperty("trnm", out JsonElement trnmElement) ||
                trnmElement.GetString() != "REAL")
            {
                return;
            }

            // "data" 배열 접근 확인
            if (!root.TryGetProperty("data", out JsonElement dataArray) ||
                dataArray.ValueKind != JsonValueKind.Array)
            {
                return;
            }

            // 배열 순회 (EnumerateArray는 구조체를 반환하므로 가비지가 적음)
            foreach (JsonElement item in dataArray.EnumerateArray())
            {
                // [최적화] Name 추출
                if (!item.TryGetProperty("name", out JsonElement nameElement)) continue;
                string name = nameElement.GetString();

                // 딕셔너리 조회 (O(1))
                if (!string.IsNullOrEmpty(name) && _realDataHandlers.TryGetValue(name, out var handler))
                {
                    // [최적화] 종목코드 추출 (문자열 파싱 최소화)
                    string itemcode = string.Empty;
                    if (item.TryGetProperty("item", out JsonElement itemElement))
                    {
                        string rawItem = itemElement.GetString();
                        if (!string.IsNullOrEmpty(rawItem))
                        {
                            // Split 대신 IndexOf 사용
                            int idx = rawItem.IndexOf('_');
                            itemcode = (idx != -1) ? rawItem.Substring(0, idx) : rawItem;
                        }
                    }

                    // [최적화] values 데이터 처리
                    if (item.TryGetProperty("values", out JsonElement valuesToken))
                    {
                        // case A: 이미 JSON 객체인 경우
                        if (valuesToken.ValueKind == JsonValueKind.Object)
                        {
                            handler(itemcode, valuesToken);
                        }
                        // case B: 문자열로 이중 인코딩된 경우 (파싱 필요)
                        else if (valuesToken.ValueKind == JsonValueKind.String)
                        {
                            try
                            {
                                using (JsonDocument doc = JsonDocument.Parse(valuesToken.GetString()))
                                {
                                    // 중요: using 블록을 벗어나면 데이터가 사라지므로 Clone() 사용
                                    handler(itemcode, doc.RootElement.Clone());
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
        }

        // =========================================================
        // 4. 각 기능별 핸들러 (System.Text.Json 적용)
        // =========================================================

        // [헬퍼] JsonElement에서 안전하게 문자열 가져오기
        private string GetStr(JsonElement v, string key, string def = "")
        {
            if (v.TryGetProperty(key, out JsonElement val))
            {
                return val.ToString(); // 숫자든 문자든 문자열로 변환
            }
            return def;
        }

        // [헬퍼] JsonElement에서 안전하게 int 가져오기
        private int GetInt(JsonElement v, string key, int def = 0)
        {
            if (v.TryGetProperty(key, out JsonElement val))
            {
                if (val.ValueKind == JsonValueKind.Number && val.TryGetInt32(out int i)) return i;
                if (val.ValueKind == JsonValueKind.String && int.TryParse(val.GetString(), out int s)) return s;
            }
            return def;
        }

        // [헬퍼] JsonElement에서 안전하게 long 가져오기
        private long GetLong(JsonElement v, string key, long def = 0)
        {
            if (v.TryGetProperty(key, out JsonElement val))
            {
                if (val.ValueKind == JsonValueKind.Number && val.TryGetInt64(out long l)) return l;
                if (val.ValueKind == JsonValueKind.String && long.TryParse(val.GetString(), out long s)) return s;
            }
            return def;
        }


        private void HandleConditionSearch(string itemcode, JsonElement v)
        {
            Condition_Management.RealCondition_item_search(
                GetStr(v, "9001"), // 종목코드
                GetStr(v, "841"),  // 조건식명
                GetStr(v, "843")   // 편입/이탈
            );
        }

        private void HandleExpectedMarketIndex(string itemcode, JsonElement v)
        {
            RealData_Management.Market_update(
                itemcode,
                GetStr(v, "295"), GetStr(v, "291"), GetStr(v, "291"), GetStr(v, "291")
            );
        }

        private void HandleMarketIndex(string itemcode, JsonElement v)
        {
            RealData_Management.Market_update(
                itemcode,
                GetStr(v, "12"), GetStr(v, "10"), GetStr(v, "18"), GetStr(v, "17")
            );
        }

        private void HandleMarketFluctuation(string itemcode, JsonElement v)
        {
            RealData_Management.Market_fluctuate(
                itemcode,
                GetStr(v, "14"), GetStr(v, "251"), GetStr(v, "252"),
                GetStr(v, "253"), GetStr(v, "254"), GetStr(v, "255")
            );
        }

        private void HandleStockExpectedConclusion(string itemcode, JsonElement v)
        {
            // ContainsKey 체크 (빠른 접근)
            if (Form1.stockBalanceList.ContainsKey(itemcode) && Form1.Market_Item_List.TryGetValue(itemcode, out var marketItem))
            {
                var status = Form1.stockBalanceList[itemcode].종목상태;
                if (status == "장전동시" || status == "과열(VI)" || status == "동시호가")
                {
                    RealData_Management.Stock_update(
                        true, itemcode,
                        GetStr(v, "10"), GetStr(v, "12"), GetStr(v, "13"),
                        marketItem.누적거래대금.ToString(), // 기존 값 유지
                        marketItem.Last_price.ToString()    // 기존 값 유지
                    );
                }
            }
        }

        private void HandleStockConclusion(string itemcode, JsonElement v)
        {
            if (Form1.시장가탐색)
            {
                // Tab_PriceSearch로 데이터 전달
                Tab_PriceSearch.Stock_search_거래대금(
                    itemcode,
                    GetStr(v, "10"), // 현재가
                    GetStr(v, "20"), // 체결시간
                    GetStr(v, "15"), // 거래량
                    GetStr(v, "14")  // 누적거래대금
                );
            }

            RealData_Management.Stock_update(
                false, itemcode,
                GetStr(v, "10"), GetStr(v, "12"), GetStr(v, "13"), GetStr(v, "14"), GetStr(v, "16")
            );

            RealData_Management.Real_Watch_update(
                itemcode,
                GetStr(v, "12"), GetStr(v, "10"), GetStr(v, "16"), GetStr(v, "17"),
                GetStr(v, "18"), GetStr(v, "13"), GetStr(v, "14"), GetStr(v, "29"),
                GetStr(v, "30"), GetStr(v, "31"), GetStr(v, "311")
            );
        }

        private void HandleStockHoga(string itemcode, JsonElement v)
        {
            if (Form1.시장가탐색)
            {
                Tab_PriceSearch.Stock_search_호가별대금(
                    itemcode,
                    GetStr(v, "42"), GetStr(v, "43"), GetStr(v, "44"), GetStr(v, "45"), GetStr(v, "46"),
                    GetStr(v, "47"), GetStr(v, "48"), GetStr(v, "49"), GetStr(v, "50"), GetStr(v, "52"),
                    GetStr(v, "53"), GetStr(v, "54"), GetStr(v, "55"), GetStr(v, "56"), GetStr(v, "57"),
                    GetStr(v, "58"), GetStr(v, "59"), GetStr(v, "60"), GetStr(v, "62"), GetStr(v, "63"),
                    GetStr(v, "64"), GetStr(v, "65"), GetStr(v, "66"), GetStr(v, "67"), GetStr(v, "68"),
                    GetStr(v, "69"), GetStr(v, "70"), GetStr(v, "72"), GetStr(v, "73"), GetStr(v, "74"),
                    GetStr(v, "75"), GetStr(v, "76"), GetStr(v, "77"), GetStr(v, "78"), GetStr(v, "79"),
                    GetStr(v, "80")
                );
            }

            if (Form1.로딩완료)
            {
                GET.StockState(
                    itemcode,
                    GetStr(v, "51"), GetStr(v, "54"), GetStr(v, "55"), GetStr(v, "56"), GetStr(v, "57"), GetStr(v, "41"), GetStr(v, "44"), GetStr(v, "45"), GetStr(v, "46"), GetStr(v, "47")
                );
            }
        }

        private void HandleAccountBalance(string itemcode, JsonElement v)
        {
            // ==========================================================
            // 1. 기존 핵심 로직 (변환 및 장부 업데이트)
            // ==========================================================
            string 종목코드 = GetStr(v, "9001");
            string 원본종목코드 = 종목코드; // 로그용으로 원본 저장
            string 신용구분 = GetStr(v, "917");
            string 대출일 = GetStr(v, "916");

            // 숫자 직접 변환 (절댓값 처리)
            int rawPrice = GetInt(v, "10");
            int 현재가 = (rawPrice < 0) ? -rawPrice : rawPrice;

            int 보유수량 = GetInt(v, "930");
            int 평균단가 = GetInt(v, "931");
            long 매입금액 = GetLong(v, "932");

            long 실현손익 = GetLong(v, "990");
            long 실현손익_신용 = GetLong(v, "992");
            실현손익 += 실현손익_신용;

            // ==========================================================
            // 2. 🌟 요청하신 모든 데이터 추출 및 전체 출력 로직 추가
            // ==========================================================
            string 만기일 = GetStr(v, "918");
            string 종목명 = GetStr(v, "302");


            string 계좌번호 = GetStr(v, "9201");
            int 주문가능수량 = GetInt(v, "933");
            string 당일순매수량 = GetStr(v, "945");
            string 매도매수구분 = GetStr(v, "946");
            string 당일총매도손익 = GetStr(v, "950");
            string extraItem951 = GetStr(v, "951");
            string 매도호가 = GetStr(v, "27");
            string 매수호가 = GetStr(v, "28");
            string 기준가 = GetStr(v, "307");
            string 손익률 = GetStr(v, "8019");
            string 신용금액 = GetStr(v, "957");
            string 신용이자 = GetStr(v, "958");
            string 당일실현손익율_유가 = GetStr(v, "991");
            string 당일실현손익율_신용 = GetStr(v, "993");
            string 담보대출수량 = GetStr(v, "959");
            string extraItem924 = GetStr(v, "924");

            //if (신용구분 == "03")
            //{
            //    string log = $"\n================ [실시간 잔고 데이터 수신] ================\n" +
            //                 $"[{계좌번호}] {종목명} ({원본종목코드})\n" +
            //                 $"-----------------------------------------------------------\n" +
            //                 $"▶ 신용구분(917): {신용구분} | 대출일(916): {대출일} | 만기일(918): {만기일}\n" +
            //                 $"▶ 현재가(10): {rawPrice} (절댓값 변환: {현재가})\n" +
            //                 $"▶ 보유수량(930): {보유수량} | 주문가능수량(933): {주문가능수량}\n" +
            //                 $"▶ 매입단가(931): {평균단가} | 총매입가(932)#총매입금액: {매입금액}\n" +
            //                 $"▶ 매도/매수구분(946) #1=매도 2=매수 : {매도매수구분} | 당일순매수량(945): {당일순매수량}\n" +
            //                 $"▶ 당일총매도손익(950): {당일총매도손익} | 실현손익률(8019): {손익률}%\n" +
            //                 $"▶ 실현손익(유가/990): {GetStr(v, "990")} | 손익율(991): {당일실현손익율_유가}%\n" +
            //                 $"▶ 실현손익(신용/992): {GetStr(v, "992")} | 손익율(993): {당일실현손익율_신용}%\n" +
            //                 $"▶ 신용금액(957): {신용금액} | 신용이자(958): {신용이자} | 담보대출수량(959): {담보대출수량}\n" +
            //                 $"▶ (최우선)매도호가(27): {매도호가} | (최우선)매수호가(28): {매수호가} | 기준가(307): {기준가}\n" +
            //                 $"▶ Extra(951): {extraItem951} | Extra(924): {extraItem924}\n" +
            //                 $"===========================================================";

            //    Form1.Console_print(log);
            //}
            //else
            //{
            //    Form1.Console_print($"현물잔고 종목명:{종목명} 신용구분:{신용구분}");
            //}

            // (위쪽의 데이터 파싱 로직은 그대로 유지) ...

            if (Form1.stockBalanceList.TryGetValue(종목코드, out var 잔고))
            {
                if (신용구분 == "03")
                {
                    // ==========================================================
                    // 🚀 2. [추가] 신용상세리스트 개별 대출건 업데이트!
                    // ==========================================================
                    var 개별신용 = 잔고.신용상세리스트.FirstOrDefault(x => x.대출일 == 대출일 && x.신용구분 == 신용구분);

                    // 🌟 [로그용 변수] 신규인지 업데이트인지 구분하기 위해 만듭니다.
                    //string 작업상태 = "업데이트";

                    // 만약 리스트에 없던 새로운 대출건이라면? (신규 매수 체결 등) -> 새로 만들어서 넣어줍니다.
                    if (개별신용 == null)
                    {
                        //작업상태 = "신규추가"; // 객체가 없었으므로 상태를 바꿔줍니다.

                        개별신용 = new 신용상세
                        {
                            일자 = DateTime.Now.ToString("yyyyMMdd"), // 빈칸 대신 오늘 날짜 기록 추천!
                            종목코드 = 원본종목코드,
                            종목명 = 종목명,
                            신용구분 = 신용구분,
                            대출일 = 대출일
                        };


                        // 리스트에 새 대출건 추가!
                        잔고.신용상세리스트.Add(개별신용);
                    }

                    // 🌟 3. 찾았거나 새로 만든 객체에 실시간 최신 데이터를 덮어씌웁니다.
                    개별신용.만기일 = 만기일;
                    개별신용.현재가 = 현재가;
                    개별신용.보유수량 = 보유수량;
                    개별신용.청산가능수량 = GetInt(v, "933"); // 933(주문가능수량)이 개별 대출건의 청산가능수량!
                    개별신용.매입가 = 평균단가;               // 931(매입단가)
                    개별신용.매입금액 = 매입금액;             // 932(총매입가)

                    개별신용.신용금액 = GetLong(v, "957");
                    개별신용.신용이자 = GetLong(v, "958");

                    // 손익 데이터
                    개별신용.당일매도손익 = GetLong(v, "950");

                    if (double.TryParse(GetStr(v, "8019"), out double 변환된수익률))
                    {
                        개별신용.수익률 = 변환된수익률;
                    }

                    // ==========================================================
                    // 🖨️ [로그 출력] 덮어씌운 최신 데이터를 콘솔에 예쁘게 찍어줍니다.
                    // ==========================================================
                    //Form1.Console_print($"[신용상세 내역] >> {작업상태} {개별신용.종목명}(대출일:{개별신용.대출일}) " +
                    //                    $"| 수량:{개별신용.보유수량}(가능:{개별신용.청산가능수량}) " +
                    //                    $"| 수익률:{개별신용.수익률:F2}% | 평가금:{개별신용.매입금액:N0}원");
                }
                else if (신용구분 == "99")
                {
                //    Form1.Console_print($"신용구분 999  종목명:{종목명} 신용구분:{신용구분}");

                    잔고.신용_보유수량 = 보유수량;
                    잔고.신용_주문가능수량 = 주문가능수량;
                }

                // 1. 기존 잔고 전체 통계 업데이트 (기존 로직 유지)
                Conclusion_Management.잔고업데이트(잔고, 현재가, 보유수량, 주문가능수량, 평균단가, 매입금액, 실현손익, 신용구분);
            }

        }

        private async void HandleOrderConclusion(string itemcode, JsonElement v)
        {
            // 1. 데이터 추출
            string 주문상태 = GetStr(v, "913");
            string 매수매도 = GetStr(v, "905").Trim();
            string 시장가구분 = GetStr(v, "906");
            string 종목코드 = GetStr(v, "9001");
            if (string.IsNullOrEmpty(종목코드)) 종목코드 = itemcode;

            string 주문번호 = GetStr(v, "9203");
            string Screennum = GetStr(v, "920");
            string 거부사유 = GetStr(v, "919", "0");
            string 원주문번호 = GetStr(v, "904");
            string 종목명 = GetStr(v, "302");

            int 주문가격 = GetInt(v, "901");
            int 주문수량 = GetInt(v, "900");
            int 미체결량 = GetInt(v, "902");
            int 체결가 = GetInt(v, "910");

            int 주문N체결시간 = GetInt(v, "908");

            int rawPrice = GetInt(v, "10");
            int 현재가 = (rawPrice < 0) ? -rawPrice : rawPrice;

            string 신용구분 = GetStr(v, "922");
            string 대출일 = GetStr(v, "923");

            // 매매 거부 로그
            if (거부사유 != "0")
            {
                Form1.Console_print($"\n\n########## 매매거부 ########## \n{종목명} 거부사유 : {거부사유} 주문시간: {주문N체결시간}");
                Form1.Console_print($"주문상태:{주문상태} 주문수량:{주문수량} 주문번호:{주문번호}");
                Log.에러기록($"########## 매매거부 ########## {종목명} 거부사유: {거부사유} 주문시간: {주문N체결시간}");
                Log.에러기록($"########## 매매거부 ########## {종목명} 매수매도: {매수매도} 주문상태:{주문상태} 주문수량:{주문수량} 주문번호:{주문번호}");

                // ==========================================================
                // [+] 장부에서 주문번호로 해당 객체를 검색한 뒤 삭제 함수로 전달
                // ==========================================================
                if (Form1.JumunItem_List.TryGetValue(주문번호, out JumunItem 삭제할객체))
                {
                    // 1. 키값(주문번호)으로 한 번에 깔끔하게 찾은 경우
                    Jumun.ExecuteDelete(삭제할객체);
                }

                // ==========================================================
                // [+] 주문 거부 발생 시 매도 기능 30초 일시 정지 (안전장치)
                // ==========================================================
                if (Form1.form1.매도_ON) // 매도가 켜져 있을 때만 작동
                {
                    // 1. 스레드 충돌 에러를 방지하기 위해 BeginInvoke로 안전하게 화면(UI) 변수를 조작합니다.
                    Form1.form1.BeginInvoke(new Action(() =>
                    {
                        Form1.form1.매도_ON = false;
                        Log.에러기록("[-] 주문 거부 감지: 10초 동안 매도 기능을 일시 정지합니다.");
                    }));

                    // 2. 프로그램 화면 멈춤 없이, 백그라운드에서 조용히 10초를 기다립니다.
                 _= Task.Run(async () =>
                    {
                        await Task.Delay(10000); // 10,000 밀리초 = 10초 대기

                        // 3. 10초가 지나면 다시 안전하게 매도 기능을 켭니다.
                        Form1.form1.BeginInvoke(new Action(() =>
                        {
                            Form1.form1.매도_ON = true;
                            Log.에러기록("[+] 10초 쿨타임 완료: 매도 기능을 다시 활성화합니다.");
                        }));
                    });
                }
            }

            // 시장가 가격 보정
            if (시장가구분 == "시장가")
            {
                if (매수매도.IndexOf("매수", StringComparison.Ordinal) >= 0)
                {
                    int ask = GetInt(v, "27");
                    주문가격 = (ask < 0) ? -ask : ask;
                }
                else if (매수매도.IndexOf("매도", StringComparison.Ordinal) >= 0)
                {
                    int bid = GetInt(v, "28");
                    주문가격 = (bid < 0) ? -bid : bid;
                }
            }

            // 매수매도 텍스트 정리
            string 정리된_매수매도 = (매수매도.Length > 1 && (매수매도[0] == '+' || 매수매도[0] == '-'))
                                 ? 매수매도.AsSpan(1).ToString()
                                 : 매수매도;

            // 3. 취소 주문 처리
            if (주문상태 == "확인" && 매수매도.IndexOf("취소", StringComparison.Ordinal) >= 0)
            {
                //  Form1.Console_print($"체결잔고_취소기록:{종목명} {주문상태} 주문수량:{주문수량} 미체결량:{미체결량} ");
                Conclusion_Management.체결잔고_취소기록(종목코드, 종목명, 현재가, 정리된_매수매도, 시장가구분, 주문수량, 원주문번호);
                return;
            }

            // 4. 접수 및 체결 처리
            if ((주문상태 == "접수" || 주문상태 == "체결") && 매수매도.IndexOf("취소", StringComparison.Ordinal) == -1)
            {
                // =================================================================
                // [지니 로직] 접수 단계에서 '주문추적기'를 통해 출처 찾기!
                // =================================================================
                if (주문상태 == "접수")
                {
                    JumunItem 찾은객체 = Jumun.출처찾기_및_업데이트(주문번호, 종목코드, 주문수량);

                    if (찾은객체 != null)
                    {
                        Form1.Console_print($"[역매칭 성공] {찾은객체.종목명} 주문번호:{주문번호} -> 전략:{찾은객체.검색식}");
                        Form1.Console_print("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    }
                    else
                    {
                        // 외부 주문이거나 이미 매칭된 경우
                        if (미체결량 != 0)
                        {
                            var jumun_ = Jumun.UpdateKey(주문번호, Screennum, 종목코드, null);
                            if (jumun_ == null)
                            {
                                Form1.Console_print("\n\n###################################");
                                Form1.Console_print($"[미확인] 매수매도 {매수매도} / 주문수량 {주문수량} / 미체결량 {미체결량} / 주문번호 {주문번호} /  출처를 찾을 수 없음");
                                Form1.Console_print($"[미확인] 주문상태 {주문상태} / Screennum {Screennum} / 주문가격 {주문가격}/ 체결가 {체결가} / 출처를 찾을 수 없음");
                                Form1.Console_print("###################################");
                            }
                        }
                    }
                }

                var jumun = Jumun.UpdateKey(주문번호, Screennum, 종목코드, null);

                bool is신용주문 = (신용구분 == "03");


                // 접수
                if (체결가 == 0)
                {
                    if (주문수량 == 미체결량)
                    {
                        await Conclusion_Management.체결잔고_접수(is신용주문,
                              jumun, Screennum, 종목코드, 종목명,
                              현재가, 정리된_매수매도, 시장가구분,
                              주문가격, 주문수량, 주문번호, 주문N체결시간, 미체결량
                              );
                    }
                }
                else // 체결
                {
                    if (jumun != null)
                    {
                       // Form1.Console_print($"### 체결 신용구분: {신용구분} ");

                        is신용주문 = (매수매도 == "+매수신용" || 매수매도 == "-매도신용");

                        int 단위체결가 = GetInt(v, "914");
                        int 단위체결량 = GetInt(v, "915");
                        int 총체결량 = GetInt(v, "911");
                        int 세금 = GetInt(v, "939");
                        int 수수료 = GetInt(v, "938");

                        Conclusion_Management.체결잔고_체결(
                            jumun, is신용주문, 대출일, 주문번호, 종목코드, 종목명,
                            단위체결가, 체결가, 단위체결량, 총체결량,
                            세금, 수수료,
                            주문수량, 주문N체결시간, 매수매도, 시장가구분,
                            현재가, 미체결량
                        );
                    }
                }
            }








        }
    }
}