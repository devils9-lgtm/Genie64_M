using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace 지니64.RESTAPI
{
    internal class info
    {

        private static readonly HttpClient client = new HttpClient();

        internal static void 요청(string code, string where, string parameter, bool Priority)
        {
            //   Form1.Console_print("--------------- info_종목정보 --------------- 신규조회 리스트: " + Form1.신규조회_List.Count);

            if (!where.Equals("info_NEWBUY"))
            {
                if (!Form1.신규조회_List.ContainsKey(code))
                {
                    신규조회 Add = new 신규조회(code, where, 0, where);
                    Form1.신규조회_List.TryAdd(code, Add);
                }
            }

            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10001", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10001", 요청);
            }

            async Task 요청()
            {
                try
                {
                    // 1. 토큰 설정
                    string MY_ACCESS_TOKEN = Form1.API_token; // 접근 토큰

                    // 2. 요청 데이터
                    var paramsData = new
                    {
                        stk_cd = code + "_AL"// 종목코드 거래소별 종목코드 (KRX:039490,NXT:039490_NX,SOR:039490_AL)
                    };

                    // 3. API 실행
                    await 종목정보(MY_ACCESS_TOKEN, paramsData, "ka10001", code, where, parameter);
                }
                catch (Exception ex)
                {
                    Form1.Console_print("info_수동 요청 실패: " + ex.Message);
                }
            }
        }

        // [지니 최적화] 안전한 문자열 변환 헬퍼 (JObject["key"] 대체용)
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

        public static async Task 종목정보(string token, object data, string tr_id, string code, string where, string parameter, string cont_yn = "N", string next_key = "")
        {
            // 1. 요청할 API URL
            string host = "https://api.kiwoom.com"; // 실전투자
            if (GenieConfig.checkBox_Simulation) host = "https://mockapi.kiwoom.com"; // 모의투자

            string endpoint = "/api/dostk/stkinfo";
            string url = host + endpoint;

            try
            {
                // 3. HTTP POST 요청 데이터 준비
                // System.Text.Json 직렬화 (한글 깨짐 방지 옵션 적용)
                var options = new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };
                var json = JsonSerializer.Serialize(data, options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // === [수술 완료] 공용 헤더를 건드리지 않고, 이 요청만을 위한 전용 편지봉투를 만듭니다 ===
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    request.Headers.Add("cont-yn", cont_yn);
                    request.Headers.Add("next-key", next_key);
                    request.Headers.Add("api-id", tr_id); // TR명

                    request.Content = content;

                    // 통신 단절 시 여기서 catch문으로 점프
                    using (var response = await client.SendAsync(request))
                    {
                        // 응답 본문 출력
                        var responseBody = await response.Content.ReadAsStringAsync();

                        // ==========================================================
                        // 1. [긴급 검사] JSON 파싱 전, 토큰 만료 에러부터 가장 먼저 요격!
                        // ==========================================================
                        if (responseBody.Contains("8005") && responseBody.Contains("유효하지 않습니다"))
                        {
                            Form1.Console_print(">> [REST API] 접근 토큰이 만료되었습니다! (에러 8005)");
                            Log.에러기록("[토큰 만료] 서버로부터 8005 에러 수신. 안전 종료를 시도합니다.");

                            Form1.중복접속 = false; // 단순 토큰 만료이므로 중복 접속은 아님을 명시
                            Helper.안전종료하기();
                            return; // 에러 메시지이므로 더 이상 아래(JSON 파싱)로 내려가지 않고 즉시 컷!
                        }

                        // System.Text.Json 파싱
                        using (JsonDocument doc = JsonDocument.Parse(responseBody))
                        {
                            JsonElement root = doc.RootElement;
                            string returnCode = GetSafeString(root, "return_code");

                            if (returnCode == "0")
                            {
                                // JObject 값 추출 -> GetSafeString 사용
                                string itemcode = GetSafeString(root, "stk_cd").Split('_')[0];

                                int.TryParse(GetSafeString(root, "base_pric"), out int 기준가);
                                double.TryParse(GetSafeString(root, "flu_rt"), out double 등락율);

                                double.TryParse(GetSafeString(root, "cur_prc"), out double 현재가_);
                                double.TryParse(GetSafeString(root, "open_pric"), out double 시가_);
                                double.TryParse(GetSafeString(root, "high_pric"), out double 고가_);
                                double.TryParse(GetSafeString(root, "low_pric"), out double 저가_);

                                double 현재가 = Math.Abs(현재가_);
                                double 시가 = Math.Abs(시가_);
                                double 고가 = Math.Abs(고가_);
                                double 저가 = Math.Abs(저가_);

                                string 시가총액 = GetSafeString(root, "mac").Trim();
                                string 누적거래량 = GetSafeString(root, "trde_qty").Trim();
                                string 전일거래량대비 = GetSafeString(root, "trde_pre").Trim();
                                string 거래회전율 = "0";

                                // Market 정보 갱신
                                if (Form1.Market_Item_List.TryGetValue(itemcode, out Market_Item Market))
                                {
                                    Market.start_price = (int)시가;
                                    Market.현재가 = (int)현재가;
                                    Market.등락율 = 등락율;
                                    Market.누적거래량 = long.Parse(누적거래량);
                                }

                                // 신규조회 리스트 정리
                                if (Form1.신규조회_List.TryGetValue(itemcode, out 신규조회 targetItem))
                                {
                                    if (targetItem.where == where)
                                    {
                                        Form1.신규조회_List.TryRemove(itemcode, out _);
                                    }
                                }

                                // where 분기 처리
                                if (where.Equals("info_loading"))
                                {
                                    string 누적거래대금 = "";
                                    if (Market != null) 누적거래대금 = Market.누적거래대금.ToString();

                                    RealData_Management.Stock_update(
                                      false,
                                      itemcode,
                                      현재가.ToString(),
                                      GetSafeString(root, "flu_rt"),
                                      GetSafeString(root, "trde_qty"),
                                      누적거래량,
                                      누적거래대금);
                                }

                                if (where.Equals("info_Watch"))
                                {
                                    if (Market != null)
                                    {
                                        Tab_Watch.Watch_update(itemcode, 등락율, (int)현재가, 누적거래량, 0, (int)시가, (int)고가, (int)저가, 0, 전일거래량대비, 거래회전율, 시가총액);
                                    }
                                }

                                if (where.Equals("info_예약주문실행"))
                                {
                                    if (Market != null) await Order_Reserve.예약주문_등록(int.Parse(parameter), Market);
                                }

                                if (where.Equals("info_예약종목선택"))
                                {
                                    if (Market != null) Order_Reserve.예약종목선택(Market);
                                }

                                if (where.Equals("info_수동종목선택"))
                                {
                                    if (Market != null) Order_Reserve.수동종목선택(Market);
                                }

                                if (where.Equals("info_종목선택"))
                                {
                                    if (Market != null)
                                    {
                                        Order_Reserve.수동종목선택(Market);
                                        Order_Reserve.예약종목선택(Market);
                                    }
                                }

                                if (where.Equals("info_NEWBUY"))
                                {
                                    if (Market != null)
                                    {
                                        if (Method.매매확인_VI_모투가능확인(Market, 1))
                                        {
                                            string[] para = parameter.Split('^');
                                            string 검색식 = para[1];
                                            string 종목명 = para[2];
                                            string Screennum = para[4];
                                            double 주문값 = double.Parse(para[5]);
                                            int 시장가구분 = int.Parse(para[6]);
                                            int 비중선택 = int.Parse(para[7]);
                                            double 주문비중 = double.Parse(para[8]);
                                            int 취소시간 = int.Parse(para[9]);
                                            int 취소N주문 = int.Parse(para[10]);
                                            int 반복횟수 = int.Parse(para[11]);

                                            await Tab_Basic.신규매수실행(Market, 종목명, 검색식, Screennum, 주문값, 시장가구분, 비중선택, 주문비중, 취소시간, 취소N주문, 반복횟수);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                요청(code, where, parameter, true);
                            }
                        } // using JsonDocument 끝
                    } // using response 끝
                } // using request 끝
            }
            catch (Exception error)
            {
                Log.에러기록("종목정보 요청 실패: " + error.ToString());
                Form1.Console_print("종목정보 요청 실패: " + error.Message);
            }
        }
    }
}

