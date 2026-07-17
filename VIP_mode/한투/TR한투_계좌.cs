using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 지니64.box;

namespace 지니64
{
    internal class TR한투_계좌 : Form1
    {
        // 저사양 PC 최적화: HttpClient는 정적(Static)으로 한 번만 할당하여 소켓 고갈 방지
        private static readonly HttpClient 클라이언트 = new HttpClient();

        // 헬퍼: 안전하게 string 가져오기 (숫자/문자 모두 처리)
        private static string 제이슨_문자열_가져오기(JsonElement 요소, string 키)
        {
            // 1. 요소가 객체(Object, {}) 형태인지 먼저 확인합니다.
            if (요소.ValueKind == JsonValueKind.Object)
            {
                if (요소.TryGetProperty(키, out JsonElement 속성))
                {
                    if (속성.ValueKind == JsonValueKind.String) return 속성.GetString();
                    if (속성.ValueKind == JsonValueKind.Number) return 속성.ToString();
                    return 속성.ToString();
                }
            }
            // 2. 요소가 배열(Array, [])인 경우 에러 대신 메시지를 출력하거나 빈 값을 반환합니다.
            else if (요소.ValueKind == JsonValueKind.Array)
            {
                //Form1.Console_print($"[디버그] '{키}'를 찾으려 했으나, 이 데이터는 배열(Array)입니다.");
            }

            return "";
        }

        // =========================================================================
        // [계좌 조회] 주식잔고조회 [v1_국내주식-006] 실호출 API
        // =========================================================================
        public static async Task 계좌조회_요청(string 접근토큰, string 앱키, string 앱시크릿, Dictionary<string, string> 요청_파라미터, string 티알명_조건, string 연속조회여부)
        {
            string 실전_도메인 = "https://openapi.koreainvestment.com:9443";
            string 모의_도메인 = "https://openapivts.koreainvestment.com:29443";
            string 호스트 = GenieConfig.checkBox_Simulation ? 모의_도메인 : 실전_도메인;

            string 티알코드 = 티알명_조건.Split('_')[0];
            string 엔드포인트 = "/uapi/domestic-stock/v1/trading/inquire-balance";

            // [오류 수정] 필수 파라미터가 비어있으면 한투 서버가 반응을 안 하므로 "N" 강제 할당
            if (요청_파라미터.ContainsKey("OFL_YN") && string.IsNullOrEmpty(요청_파라미터["OFL_YN"]))
            {
                요청_파라미터["OFL_YN"] = "N";
            }

            // [최적화] StringBuilder로 쿼리 스트링 조립하여 동적 가비지 수집 부하 최소화
            var sb = new StringBuilder();
            foreach (var 파람 in 요청_파라미터)
            {
                if (sb.Length > 0) sb.Append('&');
                sb.Append(파람.Key).Append('=').Append(Uri.EscapeDataString(파람.Value ?? ""));
            }
            string 쿼리스트링 = sb.ToString();
            string 전체_URL = $"{호스트}{엔드포인트}?{쿼리스트링}";

            Form1.Console_print("===== 요청 정보 =====");
            Form1.Console_print($"HOST=[{호스트}]");
            Form1.Console_print($"CANO=[{요청_파라미터["CANO"]}]");
            Form1.Console_print($"ACNT_PRDT_CD=[{요청_파라미터["ACNT_PRDT_CD"]}]");
            Form1.Console_print($"Authorization = Bearer {접근토큰.Substring(0, Math.Min(20, 접근토큰.Length))}...");
            Form1.Console_print($"URL : {전체_URL}");
            Form1.Console_print("====================");

            try
            {
                using (var 요청 = new HttpRequestMessage(HttpMethod.Get, 전체_URL))
                {
                    // [해결] GET 전송에 Content 설정을 강제하면 .NET Framework에서 ProtocolViolationException 발생하므로 Header 주입으로 우회
                    요청.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    요청.Headers.TryAddWithoutValidation("content-type", "application/json; charset=utf-8");

                    요청.Headers.Authorization = new AuthenticationHeaderValue("Bearer", 접근토큰.Trim());
                    요청.Headers.Add("appkey", 앱키.Trim());
                    요청.Headers.Add("appsecret", 앱시크릿.Trim());
                    요청.Headers.Add("tr_id", 티알코드);
                    요청.Headers.Add("custtype", "P");

                    if (!string.IsNullOrWhiteSpace(연속조회여부))
                    {
                        요청.Headers.Add("tr_cont", 연속조회여부.Trim());
                    }

                    Form1.Console_print($">> [한투 주식잔고조회] 요청 전송 (tr_id: {티알코드}, 연속: [{연속조회여부}])");

                    using (var 취소소스 = new CancellationTokenSource())
                    {
                        취소소스.CancelAfter(TimeSpan.FromSeconds(30));

                        // [최적화] ConfigureAwait(false) 적용으로 데드락을 방지하고 UI 스레드 오버헤드 감소
                        using (var 응답 = await 클라이언트.SendAsync(요청, 취소소스.Token).ConfigureAwait(false))
                        {
                            // [최적화] ReadAsStringAsync 대신 고속 Stream 파싱 엔진을 사용하여 힙 메모리 할당 차단
                            using (Stream 응답스트림 = await 응답.Content.ReadAsStreamAsync().ConfigureAwait(false))
                            using (JsonDocument 제이슨_문서 = await JsonDocument.ParseAsync(응답스트림, default, 취소소스.Token).ConfigureAwait(false))
                            {
                                JsonElement 루트_요소 = 제이슨_문서.RootElement;

                                string 응답코드 = 제이슨_문서_가져오기(루트_요소, "rt_cd");
                                string 응답메시지 = 제이슨_문서_가져오기(루트_요소, "msg1");

                                if (응답코드 != "0")
                                {
                                    Form1.Console_print($"[계좌조회 오류] {응답메시지}");
                                    return;
                                }

                                if (응답코드 == "EGW00201")
                                {
                                    Form1.Console_print(">> [유량 제한] 초당 거래건수 초과");
                                    return;
                                }

                                string 다음_연속키 = "";
                                if (응답.Headers.TryGetValues("tr_cont", out var 연속키_목록))
                                {
                                    다음_연속키 = 연속키_목록.FirstOrDefault() ?? "";
                                }

                                Form1.Console_print($">> [REST API] 응답 수신 성공 (결과코드: {응답코드})");

                                // 데이터 파싱 처리 (내부 비즈니스 로직 연동)
                                파싱_주식잔고조회(루트_요소, 응답, ref 연속조회여부, ref 다음_연속키);
                            }
                        }
                    }
                }
            }
            catch (TaskCanceledException ex)
            {
                Form1.Console_print("[-] 요청 타임아웃 발생 (서버 무응답 또는 데드락 가능성)");
                Form1.Console_print(ex.ToString());
            }
            catch (HttpRequestException ex)
            {
                Form1.Console_print("[-] 한국투자증권 네트워크 요청 실패: " + ex.Message);
            }
            catch (Exception ex)
            {
                Form1.Console_print("[-] 시스템 예외 발생: " + ex.ToString());
            }
        }

        // =========================================================================
        // [유틸] JSON 요소 추출용 헬퍼 함수
        // =========================================================================
        private static string 제이슨_문서_가져오기(JsonElement 요소, string 키)
        {
            if (요소.TryGetProperty(키, out JsonElement 값))
            {
                return 값.GetString() ?? string.Empty;
            }
            return string.Empty;
        }



        private static void 파싱_주식잔고조회(JsonElement 루트_요소, HttpResponseMessage 응답, ref string 연속조회여부, ref string 다음_연속키)
        {
            try
            {
                Form1.Console_print("\n========== [한국투자증권 주식잔고조회 데이터 출력 시작] ==========");

                // -----------------------------------------------------------------
                // [A] output2 (계좌 종합 잔고 파싱)
                // -----------------------------------------------------------------
                Form1.Console_print("\n[1. 계좌 종합 잔고 (output2)]");

                // API 규격상 output2도 Object Array(배열)로 들어옵니다.
                if (루트_요소.TryGetProperty("output2", out JsonElement 아웃풋2_배열) && 아웃풋2_배열.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement 아웃풋2 in 아웃풋2_배열.EnumerateArray())
                    {
                        // 출력할 한글명과 API 키값을 딕셔너리로 매핑 (메모리 재할당 최소화)
                        var 계좌_종합_항목들 = new Dictionary<string, string>
                    {
                        { "예수금총금액", "dnca_tot_amt" },
                        { "익일정산금액", "nxdy_excc_amt" },
                        { "가수도정산금액", "prvs_rcdl_excc_amt" },
                        { "CMA평가금액", "cma_evlu_amt" },
                        { "전일매수금액", "bfdy_buy_amt" },
                        { "금일매수금액", "thdt_buy_amt" },
                        { "익일자동상환금액", "nxdy_auto_rdpt_amt" },
                        { "전일매도금액", "bfdy_sll_amt" },
                        { "금일매도금액", "thdt_sll_amt" },
                        { "D+2자동상환금액", "d2_auto_rdpt_amt" },
                        { "전일제비용금액", "bfdy_tlex_amt" },
                        { "금일제비용금액", "thdt_tlex_amt" },
                        { "총대출금액", "tot_loan_amt" },
                        { "유가평가금액", "scts_evlu_amt" },
                        { "총평가금액", "tot_evlu_amt" },
                        { "순자산금액", "nass_amt" },
                        { "융자금자동상환여부", "fncg_gld_auto_rdpt_yn" },
                        { "매입금액합계금액", "pchs_amt_smtl_amt" },
                        { "평가금액합계금액", "evlu_amt_smtl_amt" },
                        { "평가손익합계금액", "evlu_pfls_smtl_amt" },
                        { "총대주매각대금", "tot_stln_slng_chgs" },
                        { "전일총자산평가금액", "bfdy_tot_asst_evlu_amt" },
                        { "자산증감액", "asst_icdc_amt" },
                        { "자산증감수익율", "asst_icdc_erng_rt" }
                    };

                        // 반복문을 통해 한 번에 출력 처리
                        foreach (var 항목 in 계좌_종합_항목들)
                        {
                            string 파싱된_값 = 제이슨_문자열_가져오기(아웃풋2, 항목.Value);
                            Form1.Console_print($"- {항목.Key} ({항목.Value}) : {파싱된_값}");
                        }
                    }
                }

                // -----------------------------------------------------------------
                // [B] output1 (개별 종목 상세 잔고 파싱)
                // -----------------------------------------------------------------
                Form1.Console_print("\n[2. 보유 종목 상세 내역 (output1)]");

                if (루트_요소.TryGetProperty("output1", out JsonElement 아웃풋1_배열) && 아웃풋1_배열.ValueKind == JsonValueKind.Array)
                {
                    int 종목_카운트 = 1;

                    foreach (JsonElement 아웃풋1 in 아웃풋1_배열.EnumerateArray())
                    {
                        Form1.Console_print($"\n--- < 보유 종목 {종목_카운트} > ---");

                        var 종목_상세_항목들 = new Dictionary<string, string>
                    {
                        { "상품번호", "pdno" },
                        { "상품명", "prdt_name" },
                        { "매매구분명", "trad_dvsn_name" },
                        { "전일매수수량", "bfdy_buy_qty" },
                        { "전일매도수량", "bfdy_sll_qty" },
                        { "금일매수수량", "thdt_buyqty" }, // API 규격상 언더바 없음 유지
                        { "금일매도수량", "thdt_sll_qty" },
                        { "보유수량", "hldg_qty" },
                        { "주문가능수량", "ord_psbl_qty" },
                        { "매입평균가격", "pchs_avg_pric" },
                        { "매입금액", "pchs_amt" },
                        { "현재가", "prpr" },
                        { "평가금액", "evlu_amt" },
                        { "평가손익금액", "evlu_pfls_amt" },
                        { "평가손익율", "evlu_pfls_rt" },
                        { "평가수익율", "evlu_erng_rt" },
                        { "대출일자", "loan_dt" },
                        { "대출금액", "loan_amt" },
                        { "대주매각대금", "stln_slng_chgs" },
                        { "만기일자", "expd_dt" },
                        { "등락율", "fltt_rt" },
                        { "전일대비증감", "bfdy_cprs_icdc" },
                        { "종목증거금율명", "item_mgna_rt_name" },
                        { "보증금율명", "grta_rt_name" },
                        { "대용가격", "sbst_pric" },
                        { "주식대출단가", "stck_loan_unpr" }
                    };

                        foreach (var 항목 in 종목_상세_항목들)
                        {
                            string 파싱된_값 = 제이슨_문자열_가져오기(아웃풋1, 항목.Value);
                            Form1.Console_print($"  * {항목.Key} ({항목.Value}) : {파싱된_값}");
                        }
                        종목_카운트++;
                    }

                    if (종목_카운트 == 1)
                    {
                        Form1.Console_print(">> 현재 보유 중인 종목이 없습니다.");
                    }
                }

                Form1.Console_print("\n========== [주식잔고조회 데이터 출력 종료] ==========\n");
            }
            catch (Exception 에러)
            {
                Form1.Console_print("파싱 중 에러 발생: " + 에러.Message);
            }
        }

    

     
    }
}