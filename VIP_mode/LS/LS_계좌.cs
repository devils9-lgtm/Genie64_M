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
    internal class TR_LS_계좌 : Form1
    {
        // 저사양 PC 최적화: HttpClient는 정적(Static)으로 한 번만 할당하여 소켓 고갈 방지
        private static readonly HttpClient 클라이언트 = new HttpClient();

        // 헬퍼: 안전하게 string 가져오기 (숫자/문자 모두 처리)
        private static string 제이슨_문자열_가져오기(JsonElement 요소, string 키)
        {
            if (요소.ValueKind == JsonValueKind.Object)
            {
                if (요소.TryGetProperty(키, out JsonElement 속성))
                {
                    if (속성.ValueKind == JsonValueKind.String) return 속성.GetString();
                    if (속성.ValueKind == JsonValueKind.Number) return 속성.ToString();
                    return 속성.ToString();
                }
            }
            return "";
        }

        // =========================================================================
        // [계좌 조회] 현물계좌예수금 주문가능금액 총평가 조회 [CSPAQ12200] 실호출 API
        // =========================================================================
        public static async Task 계좌조회_요청(string 접근토큰, string 앱키, string 앱시크릿, object 요청_파라미터, string 티알코드, string 연속조회여부)
        {
            // 키움 스타일의 도메인/엔드포인트 분할 선언 구조 적용
            string 호스트 = "https://openapi.ls-sec.co.kr:8080";
            if (GenieConfig.checkBox_Simulation) 호스트 = "https://openapi.ls-sec.co.kr:8080"; // 필요시 모의 도메인 변경 가능

            string 엔드포인트 = "/stock/accno";
            string 전체_URL = 호스트 + 엔드포인트;

            try
            {
                // [최적화] 저사양 PC를 위해 키움의 StringContent 대신 바이트 배열 직접 직렬화(GC 오버헤드 차단)
                byte[] json바이트 = JsonSerializer.SerializeToUtf8Bytes(요청_파라미터);
                var 페이로드 = new ByteArrayContent(json바이트);
                페이로드.Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" };

                // [독립 요청] 공용 클라이언트의 헤더 스레드 충돌을 방지하기 위한 HttpRequestMessage 독립 생성
                using (var 요청 = new HttpRequestMessage(HttpMethod.Post, 전체_URL))
                {
                    요청.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    요청.Headers.Authorization = new AuthenticationHeaderValue("Bearer", 접근토큰.Trim());
                    요청.Headers.Add("appkey", 앱키.Trim());
                    요청.Headers.Add("appsecret", 앱시크릿.Trim());
                    요청.Headers.Add("tr_cd", 티알코드);
                    요청.Headers.Add("tr_cont", string.IsNullOrWhiteSpace(연속조회여부) ? "N" : 연속조회여부.Trim());

                    // 법인인 경우 필수 세팅 주석 유지
                    // 요청.Headers.Add("mac_address", Form1.MAC_주소 ?? "00:00:00:00:00:00");

                    요청.Content = 페이로드;

                    using (var 취소소스 = new CancellationTokenSource())
                    {
                        취소소스.CancelAfter(TimeSpan.FromSeconds(30));

                        // [최적화] ConfigureAwait(false)로 스케줄러 스레드의 안전한 작업 보장 및 UI 프리징 방지
                        using (var 응답 = await 클라이언트.SendAsync(요청, 취소소스.Token).ConfigureAwait(false))
                        {
                            // [최적화] ReadAsStringAsync 대신 고속 Stream 파싱 엔진을 사용하여 힙 메모리 할당 원천 차단
                            using (Stream 응답스트림 = await 응답.Content.ReadAsStreamAsync().ConfigureAwait(false))
                            using (JsonDocument 제이슨_문서 = await JsonDocument.ParseAsync(응답스트림, default, 취소소스.Token).ConfigureAwait(false))
                            {
                                JsonElement 루트_요소 = 제이슨_문서.RootElement;

                                // LS증권 응답코드 및 메시지 추출
                                string 응답코드 = 제이슨_문서_가져오기(루트_요소, "rsp_cd");
                                string 응답메시지 = 제이슨_문서_가져오기(루트_요소, "rsp_msg");

                                // ==========================================================
                                // 1. [긴급 검사] 키움 스타일 적용: 토큰 만료 에러 최우선 요격 및 안전 종료
                                // ==========================================================
                                if (응답코드 == "9102" || 응답메시지.Contains("유효하지 않습니다") || 응답메시지.Contains("만료"))
                                {
                                    Form1.Console_print($">> [REST API] LS증권 접근 토큰이 만료되었습니다! (에러 코드: {응답코드})");
                                    Log.에러기록($"[토큰 만료] 서버로부터 에러 수신: {응답메시지}. 시스템 안전 종료를 시도합니다.");

                                    Form1.중복접속 = false;
                                    Helper.안전종료하기();
                                    return; // 즉시 컷하여 하단 비즈니스 로직 진입 원천 차단
                                }

                                Form1.Console_print($"[계좌조회 전문] {응답메시지} (코드: {응답코드})");


                                //// 일반 통신 에러 처리
                                //if (응답코드 != "00000" && 응답코드 != "0000")
                                //{
                                //    Form1.Console_print($"[계좌조회 오류] {응답메시지} (코드: {응답코드})");
                                //    return;
                                //}

                                string 다음_연속키 = "";
                                if (응답.Headers.TryGetValues("tr_cont", out var 연속키_목록))
                                {
                                    다음_연속키 = 연속키_목록.FirstOrDefault() ?? "";
                                }

                                Form1.Console_print($">> [REST API] 응답 수신 성공 (결과코드: {응답코드})");

                                // [추가] 파싱 분기 전에 수신된 원본 JSON 데이터를 출력합니다.
                                // 이미 파싱된 RootElement에서 GetRawText()를 호출하여 추가적인 메모리 할당(GC 오버헤드)을 방지합니다.
                                string 원문_데이터 = 루트_요소.GetRawText();
                                Form1.Console_print($"\n[API 수신 원문]\n{원문_데이터}\n");

                                // ==========================================================
                                // 2. [분기 매핑] 키움 스타일 적용: TR코드 조건별 파싱 함수 라우팅
                                // ==========================================================
                                if (티알코드.Equals("CSPAQ12200"))
                                {
                                    파싱_주식잔고조회(루트_요소, 응답, ref 연속조회여부, ref 다음_연속키);
                                }
                                else if (티알코드.Equals("CSPAQ12300"))
                                {
                                    // 현물계좌 잔고내역조회(CSPAQ12300) 파싱 함수로 연결
                                    파싱_현물계좌_잔고내역(루트_요소, 응답, ref 연속조회여부, ref 다음_연속키);
                                }
                                else if (티알코드.Equals("t0424"))
                                {
                                    // 주식잔고2(t0424) 파싱 함수로 연결
                                    파싱_주식잔고2(루트_요소, 응답, ref 연속조회여부, ref 다음_연속키);
                                }
                                // 추후 다른 LS증권 TR코드가 추가될 경우 이곳에 else if 문으로 확장 가능
                            }
                        }
                    }
                }
            }
            catch (Exception 예외)
            {
                Form1.Console_print("요청 실패: " + 예외.Message);
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
                Form1.Console_print("\n========== [LS증권 현물계좌예수금_주문가능금액_총평가조회(CSPAQ12200) 전체 데이터 출력 시작] ==========");

                // -----------------------------------------------------------------
                // [A] CSPAQ12200OutBlock1 (입력/계좌 기본 정보 파싱)
                // -----------------------------------------------------------------
                Form1.Console_print("\n[1. 계좌 기본 정보 (CSPAQ12200OutBlock1)]");

                if (루트_요소.TryGetProperty("CSPAQ12200OutBlock1", out JsonElement 아웃풋1))
                {
                    // [최적화] 저사양 PC의 GC(가비지 컬렉터) 부하를 최소화하기 위해 Dictionary 대신 메모리 할당이 적은 튜플 배열 사용
                    var 블록1_항목들 = new (string 한글명, string 키)[]
                    {
                  ("레코드갯수", "RecCnt"),
                  ("관리지점번호", "MgmtBrnNo"),
                  ("계좌번호", "AcntNo"),
                  ("비밀번호", "Pwd"),
                  ("잔고생성구분", "BalCreTp")
                    };

                    foreach (var 항목 in 블록1_항목들)
                    {
                        string 파싱된_값 = 제이슨_문자열_가져오기(아웃풋1, 항목.키);
                        if (!string.IsNullOrEmpty(파싱된_값))
                        {
                            Form1.Console_print($"- {항목.한글명} ({항목.키}) : {파싱된_값}");
                        }
                    }
                }
                else
                {
                    Form1.Console_print(">> CSPAQ12200OutBlock1 데이터를 찾을 수 없습니다.");
                }

                // -----------------------------------------------------------------
                // [B] CSPAQ12200OutBlock2 (계좌 종합 잔고 전체 파싱)
                // -----------------------------------------------------------------
                Form1.Console_print("\n[2. 계좌 종합 잔고 (CSPAQ12200OutBlock2)]");

                if (루트_요소.TryGetProperty("CSPAQ12200OutBlock2", out JsonElement 아웃풋2))
                {
                    // [최적화] 명세서에 있는 모든 항목을 누락 없이 포함하되, 튜플 배열로 처리하여 CPU 및 메모리 사용률 최적화
                    var 블록2_항목들 = new (string 한글명, string 키)[]
                    {
                  ("레코드갯수", "RecCnt"),
                  ("지점명", "BrnNm"),
                  ("계좌명", "AcntNm"),
                  ("현금주문가능금액", "MnyOrdAbleAmt"),
                  ("출금가능금액", "MnyoutAbleAmt"),
                  ("거래소금액", "SeOrdAbleAmt"),
                  ("코스닥금액", "KdqOrdAbleAmt"),
                  ("잔고평가금액", "BalEvalAmt"),
                  ("미수금액", "RcvblAmt"),
                  ("예탁자산총액", "DpsastTotamt"),
                  ("손익율", "PnlRat"),
                  ("투자원금", "InvstOrgAmt"),
                  ("투자손익금액", "InvstPlAmt"),
                  ("신용담보주문금액", "CrdtPldgOrdAmt"),
                  ("예수금", "Dps"),
                  ("대용금액", "SubstAmt"),
                  ("D1예수금", "D1Dps"),
                  ("D2예수금", "D2Dps"),
                  ("현금미수금액", "MnyrclAmt"),
                  ("증거금현금", "MgnMny"),
                  ("증거금대용", "MgnSubst"),
                  ("수표금액", "ChckAmt"),
                  ("대용주문가능금액", "SubstOrdAbleAmt"),
                  ("증거금률100퍼센트주문가능금액", "MgnRat100pctOrdAbleAmt"),
                  ("증거금률35%주문가능금액", "MgnRat35ordAbleAmt"),
                  ("증거금률50%주문가능금액", "MgnRat50ordAbleAmt"),
                  ("전일매도정산금액", "PrdaySellAdjstAmt"),
                  ("전일매수정산금액", "PrdayBuyAdjstAmt"),
                  ("금일매도정산금액", "CrdaySellAdjstAmt"),
                  ("금일매수정산금액", "CrdayBuyAdjstAmt"),
                  ("D1연체변제소요금액", "D1ovdRepayRqrdAmt"),
                  ("D2연체변제소요금액", "D2ovdRepayRqrdAmt"),
                  ("D1추정인출가능금액", "D1PrsmptWthdwAbleAmt"),
                  ("D2추정인출가능금액", "D2PrsmptWthdwAbleAmt"),
                  ("예탁담보대출금액", "DpspdgLoanAmt"),
                  ("신용설정보증금", "Imreq"),
                  ("융자금액", "MloanAmt"),
                  ("변경후담보비율", "ChgAfPldgRat"),
                  ("원담보금액", "OrgPldgAmt"),
                  ("부담보금액", "SubPldgAmt"),
                  ("소요담보금액", "RqrdPldgAmt"),
                  ("원담보부족금액", "OrgPdlckAmt"),
                  ("담보부족금액", "PdlckAmt"),
                  ("추가담보현금", "AddPldgMny"),
                  ("D1주문가능금액", "D1OrdAbleAmt"),
                  ("신용이자미납금액", "CrdtIntdltAmt"),
                  ("기타대여금액", "EtclndAmt"),
                  ("익일추정반대매매금액", "NtdayPrsmptCvrgAmt"),
                  ("원담보합계금액", "OrgPldgSumAmt"),
                  ("신용주문가능금액", "CrdtOrdAbleAmt"),
                  ("부담보합계금액", "SubPldgSumAmt"),
                  ("신용담보금현금", "CrdtPldgAmtMny"),
                  ("신용담보대용금액", "CrdtPldgSubstAmt"),
                  ("추가신용담보현금", "AddCrdtPldgMny"),
                  ("신용담보재사용금액", "CrdtPldgRuseAmt"),
                  ("추가신용담보대용", "AddCrdtPldgSubst"),
                  ("매도대금담보대출금액", "CslLoanAmtdt1"),
                  ("처분제한금액", "DpslRestrcAmt"),
                  ("미수불가주문가능금액", "RcvblUablOrdAbleAmt")
                    };

                    foreach (var 항목 in 블록2_항목들)
                    {
                        string 파싱된_값 = 제이슨_문자열_가져오기(아웃풋2, 항목.키);

                        // 값이 존재하는 항목만 콘솔에 출력합니다
                        if (!string.IsNullOrEmpty(파싱된_값))
                        {
                            Form1.Console_print($"- {항목.한글명} ({항목.키}) : {파싱된_값}");
                        }
                    }
                }
                else
                {
                    Form1.Console_print(">> CSPAQ12200OutBlock2 데이터를 찾을 수 없습니다.");
                }

                Form1.Console_print("\n========== [주식잔고조회 전체 데이터 출력 종료] ==========\n");
            }
            catch (Exception 에러)
            {
                Form1.Console_print("파싱 중 에러 발생: " + 에러.Message);
            }
        }



        private static void 파싱_현물계좌_잔고내역(JsonElement 루트_요소, HttpResponseMessage 응답, ref string 연속조회여부, ref string 다음_연속키)
        {
            try
            {
                Form1.Console_print("\n========== [LS증권 현물계좌 잔고내역(CSPAQ12300) 전체 데이터 출력 시작] ==========");

                // -----------------------------------------------------------------
                // [A] CSPAQ12300OutBlock1 (입력/기본 정보 파싱)
                // -----------------------------------------------------------------
                Form1.Console_print("\n[1. 기본 정보 (CSPAQ12300OutBlock1)]");

                if (루트_요소.TryGetProperty("CSPAQ12300OutBlock1", out JsonElement 아웃풋1))
                {
                    var 블록1_항목들 = new (string 한글명, string 키)[]
                    {
                        ("레코드갯수", "RecCnt"),
                        ("계좌번호", "AcntNo"),
                        ("비밀번호", "Pwd"),
                        ("잔고생성구분", "BalCreTp"),
                        ("수수료적용구분", "CmsnAppTpCode"),
                        ("D2잔고기준조회구분", "D2balBaseQryTp"),
                        ("단가구분", "UprcTpCode")
                    };

                    foreach (var 항목 in 블록1_항목들)
                    {
                        string 파싱된_값 = 제이슨_문자열_가져오기(아웃풋1, 항목.키);
                        if (!string.IsNullOrEmpty(파싱된_값))
                        {
                            Form1.Console_print($"- {항목.한글명} ({항목.키}) : {파싱된_값}");
                        }
                    }
                }
                else
                {
                    Form1.Console_print(">> CSPAQ12300OutBlock1 데이터를 찾을 수 없습니다.");
                }

                // -----------------------------------------------------------------
                // [B] CSPAQ12300OutBlock2 (계좌 종합 잔고 파싱)
                // -----------------------------------------------------------------
                Form1.Console_print("\n[2. 계좌 종합 잔고 (CSPAQ12300OutBlock2)]");

                if (루트_요소.TryGetProperty("CSPAQ12300OutBlock2", out JsonElement 아웃풋2))
                {
                    var 블록2_항목들 = new (string 한글명, string 키)[]
                    {
                        ("레코드갯수", "RecCnt"),
                        ("지점명", "BrnNm"),
                        ("계좌명", "AcntNm"),
                        ("현금주문가능금액", "MnyOrdAbleAmt"),
                        ("출금가능금액", "MnyoutAbleAmt"),
                        ("거래소금액", "SeOrdAbleAmt"),
                        ("코스닥금액", "KdqOrdAbleAmt"),
                        ("HTS주문가능금액", "HtsOrdAbleAmt"),
                        ("증거금률100퍼센트주문가능금액", "MgnRat100pctOrdAbleAmt"),
                        ("잔고평가금액", "BalEvalAmt"),
                        ("매입금액", "PchsAmt"),
                        ("미수금액", "RcvblAmt"),
                        ("손익율", "PnlRat"),
                        ("투자원금", "InvstOrgAmt"),
                        ("투자손익금액", "InvstPlAmt"),
                        ("신용담보주문금액", "CrdtPldgOrdAmt"),
                        ("예수금", "Dps"),
                        ("D1예수금", "D1Dps"),
                        ("D2예수금", "D2Dps"),
                        ("주문일", "OrdDt"),
                        ("현금증거금액", "MnyMgn"),
                        ("대용증거금액", "SubstMgn"),
                        ("대용금액", "SubstAmt"),
                        ("전일매수체결금액", "PrdayBuyExecAmt"),
                        ("전일매도체결금액", "PrdaySellExecAmt"),
                        ("금일매수체결금액", "CrdayBuyExecAmt"),
                        ("금일매도체결금액", "CrdaySellExecAmt"),
                        ("평가손익합계", "EvalPnlSum"),
                        ("예탁자산총액", "DpsastTotamt"),
                        ("제비용", "Evrprc"),
                        ("재사용금액", "RuseAmt"),
                        ("기타대여금액", "EtclndAmt"),
                        ("가정산금액", "PrcAdjstAmt"),
                        ("D1수수료", "D1CmsnAmt"),
                        ("D2수수료", "D2CmsnAmt"),
                        ("D1제세금", "D1EvrTax"),
                        ("D2제세금", "D2EvrTax"),
                        ("D1결제예정금액", "D1SettPrergAmt"),
                        ("D2결제예정금액", "D2SettPrergAmt"),
                        ("전일KSE현금증거금", "PrdayKseMnyMgn"),
                        ("전일KSE대용증거금", "PrdayKseSubstMgn"),
                        ("전일KSE신용현금증거금", "PrdayKseCrdtMnyMgn"),
                        ("전일KSE신용대용증거금", "PrdayKseCrdtSubstMgn"),
                        ("금일KSE현금증거금", "CrdayKseMnyMgn"),
                        ("금일KSE대용증거금", "CrdayKseSubstMgn"),
                        ("금일KSE신용현금증거금", "CrdayKseCrdtMnyMgn"),
                        ("금일KSE신용대용증거금", "CrdayKseCrdtSubstMgn"),
                        ("전일코스닥현금증거금", "PrdayKdqMnyMgn"),
                        ("전일코스닥대용증거금", "PrdayKdqSubstMgn"),
                        ("전일코스닥신용현금증거금", "PrdayKdqCrdtMnyMgn"),
                        ("전일코스닥신용대용증거금", "PrdayKdqCrdtSubstMgn"),
                        ("금일코스닥현금증거금", "CrdayKdqMnyMgn"),
                        ("금일코스닥대용증거금", "CrdayKdqSubstMgn"),
                        ("금일코스닥신용현금증거금", "CrdayKdqCrdtMnyMgn"),
                        ("금일코스닥신용대용증거금", "CrdayKdqCrdtSubstMgn"),
                        ("전일프리보드현금증거금", "PrdayFrbrdMnyMgn"),
                        ("전일프리보드대용증거금", "PrdayFrbrdSubstMgn"),
                        ("금일프리보드현금증거금", "CrdayFrbrdMnyMgn"),
                        ("금일프리보드대용증거금", "CrdayFrbrdSubstMgn"),
                        ("전일장외현금증거금", "PrdayCrbmkMnyMgn"),
                        ("전일장외대용증거금", "PrdayCrbmkSubstMgn"),
                        ("금일장외현금증거금", "CrdayCrbmkMnyMgn"),
                        ("금일장외대용증거금", "CrdayCrbmkSubstMgn"),
                        ("예탁담보수량", "DpspdgQty"),
                        ("매수정산금(D+2)", "BuyAdjstAmtD2"),
                        ("매도정산금(D+2)", "SellAdjstAmtD2"),
                        ("변제소요금(D+1)", "RepayRqrdAmtD1"),
                        ("변제소요금(D+2)", "RepayRqrdAmtD2"),
                        ("대출금액", "LoanAmt")
                    };

                    foreach (var 항목 in 블록2_항목들)
                    {
                        string 파싱된_값 = 제이슨_문자열_가져오기(아웃풋2, 항목.키);
                        if (!string.IsNullOrEmpty(파싱된_값))
                        {
                            Form1.Console_print($"- {항목.한글명} ({항목.키}) : {파싱된_값}");
                        }
                    }
                }
                else
                {
                    Form1.Console_print(">> CSPAQ12300OutBlock2 데이터를 찾을 수 없습니다.");
                }

                // -----------------------------------------------------------------
                // [C] CSPAQ12300OutBlock3 (개별 종목 상세 잔고 파싱)
                // -----------------------------------------------------------------
                Form1.Console_print("\n[3. 보유 종목 상세 내역 (CSPAQ12300OutBlock3)]");

                if (루트_요소.TryGetProperty("CSPAQ12300OutBlock3", out JsonElement 아웃풋3_배열) && 아웃풋3_배열.ValueKind == JsonValueKind.Array)
                {
                    int 종목_카운트 = 1;

                    // 루프 반복 생성 방지를 위해 외부에 할당
                    var 종목_상세_항목들 = new (string 한글명, string 키)[]
                    {
                        ("종목번호", "IsuNo"),
                        ("종목명", "IsuNm"),
                        ("유가증권잔고유형코드", "SecBalPtnCode"),
                        ("유가증권잔고유형명", "SecBalPtnNm"),
                        ("잔고수량", "BalQty"),
                        ("매매기준잔고수량", "BnsBaseBalQty"),
                        ("금일매수체결수량", "CrdayBuyExecQty"),
                        ("금일매도체결수량", "CrdaySellExecQty"),
                        ("매도가", "SellPrc"),
                        ("매수가", "BuyPrc"),
                        ("매도손익금액", "SellPnlAmt"),
                        ("손익율", "PnlRat"),
                        ("현재가", "NowPrc"),
                        ("신용금액", "CrdtAmt"),
                        ("만기일", "DueDt"),
                        ("전일매도체결가", "PrdaySellExecPrc"),
                        ("전일매도수량", "PrdaySellQty"),
                        ("전일매수체결가", "PrdayBuyExecPrc"),
                        ("전일매수수량", "PrdayBuyQty"),
                        ("대출일", "LoanDt"),
                        ("평균단가", "AvrUprc"),
                        ("매도가능수량", "SellAbleQty"),
                        ("매도주문수량", "SellOrdQty"),
                        ("금일매수체결금액", "CrdayBuyExecAmt"),
                        ("금일매도체결금액", "CrdaySellExecAmt"),
                        ("전일매수체결금액", "PrdayBuyExecAmt"),
                        ("전일매도체결금액", "PrdaySellExecAmt"),
                        ("잔고평가금액", "BalEvalAmt"),
                        ("평가손익", "EvalPnl"),
                        ("현금주문가능금액", "MnyOrdAbleAmt"),
                        ("주문가능금액", "OrdAbleAmt"),
                        ("매도미체결수량", "SellUnercQty"),
                        ("매도미결제수량", "SellUnsttQty"),
                        ("매수미체결수량", "BuyUnercQty"),
                        ("매수미결제수량", "BuyUnsttQty"),
                        ("미결제수량", "UnsttQty"),
                        ("미체결수량", "UnercQty"),
                        ("전일종가", "PrdayCprc"),
                        ("매입금액", "PchsAmt"),
                        ("등록시장코드", "RegMktCode"),
                        ("대출상세분류코드", "LoanDtlClssCode"),
                        ("예탁담보대출수량", "DpspdgLoanQty")
                    };

                    foreach (JsonElement 아웃풋3 in 아웃풋3_배열.EnumerateArray())
                    {
                        Form1.Console_print($"\n--- < 보유 종목 {종목_카운트} > ---");

                        foreach (var 항목 in 종목_상세_항목들)
                        {
                            string 파싱된_값 = 제이슨_문자열_가져오기(아웃풋3, 항목.키);
                            if (!string.IsNullOrEmpty(파싱된_값))
                            {
                                Form1.Console_print($"  * {항목.한글명} ({항목.키}) : {파싱된_값}");
                            }
                        }
                        종목_카운트++;
                    }

                    if (종목_카운트 == 1)
                    {
                        Form1.Console_print(">> 현재 보유 중인 종목이 없습니다.");
                    }
                }
                else
                {
                    Form1.Console_print(">> CSPAQ12300OutBlock3 데이터(개별 종목)를 찾을 수 없습니다.");
                }

                Form1.Console_print("\n========== [주식잔고내역조회 전체 데이터 출력 종료] ==========\n");
            }
            catch (Exception 에러)
            {
                Form1.Console_print("파싱 중 에러 발생: " + 에러.Message);
            }
        }


        private static void 파싱_주식잔고2(JsonElement 루트_요소, HttpResponseMessage 응답, ref string 연속조회여부, ref string 다음_연속키)
        {
            try
            {
                Form1.Console_print("\n========== [LS증권 주식잔고2(t0424) 전체 데이터 출력 시작] ==========");

                // -----------------------------------------------------------------
                // [A] t0424OutBlock (계좌 종합 잔고 파싱)
                // -----------------------------------------------------------------
                Form1.Console_print("\n[1. 계좌 종합 잔고 (t0424OutBlock)]");

                if (루트_요소.TryGetProperty("t0424OutBlock", out JsonElement 아웃풋1))
                {
                    // [최적화] 저사양 PC의 GC(가비지 컬렉터) 부하를 최소화하기 위해 메모리 할당이 적은 튜플 배열 사용
                    var 종합잔고_항목들 = new (string 한글명, string 키)[]
                    {
                        ("추정순자산", "sunamt"),
                        ("실현손익", "dtsunik"),
                        ("매입금액", "mamt"),
                        ("추정D2예수금", "sunamt1"),
                        ("CTS_종목번호", "cts_expcode"),
                        ("평가금액", "tappamt"),
                        ("평가손익", "tdtsunik")
                    };

                    foreach (var 항목 in 종합잔고_항목들)
                    {
                        string 파싱된_값 = 제이슨_문자열_가져오기(아웃풋1, 항목.키);
                        if (!string.IsNullOrEmpty(파싱된_값))
                        {
                            Form1.Console_print($"- {항목.한글명} ({항목.키}) : {파싱된_값}");
                        }
                    }
                }
                else
                {
                    Form1.Console_print(">> t0424OutBlock 데이터를 찾을 수 없습니다.");
                }

                // -----------------------------------------------------------------
                // [B] t0424OutBlock1 (개별 종목 상세 잔고 파싱)
                // -----------------------------------------------------------------
                Form1.Console_print("\n[2. 보유 종목 상세 내역 (t0424OutBlock1)]");

                if (루트_요소.TryGetProperty("t0424OutBlock1", out JsonElement 아웃풋2_배열) && 아웃풋2_배열.ValueKind == JsonValueKind.Array)
                {
                    int 종목_카운트 = 1;

                    // [최적화] 루프 외부에서 한 번만 배열을 할당하여 반복적인 메모리 낭비 방지
                    var 종목_상세_항목들 = new (string 한글명, string 키)[]
                    {
                        ("종목번호", "expcode"),
                        ("잔고구분", "jangb"),
                        ("잔고수량", "janqty"),
                        ("매도가능수량", "mdposqt"),
                        ("평균단가", "pamt"),
                        ("매입금액", "mamt"),
                        ("대출금액", "sinamt"),
                        ("만기일자", "lastdt"),
                        ("당일매수금액", "msat"),
                        ("당일매수단가", "mpms"),
                        ("당일매도금액", "mdat"),
                        ("당일매도단가", "mpmd"),
                        ("전일매수금액", "jsat"),
                        ("전일매수단가", "jpms"),
                        ("전일매도금액", "jdat"),
                        ("전일매도단가", "jpmd"),
                        ("처리순번", "sysprocseq"),
                        ("대출일자", "loandt"),
                        ("종목명", "hname"),
                        ("시장구분", "marketgb"),
                        ("종목구분", "jonggb"),
                        ("보유비중", "janrt"),
                        ("현재가", "price"),
                        ("평가금액", "appamt"),
                        ("평가손익", "dtsunik"),
                        ("수익율", "sunikrt"),
                        ("수수료", "fee"),
                        ("제세금", "tax"),
                        ("신용이자", "sininter")
                    };

                    foreach (JsonElement 아웃풋2 in 아웃풋2_배열.EnumerateArray())
                    {
                        Form1.Console_print($"\n--- < 보유 종목 {종목_카운트} > ---");

                        foreach (var 항목 in 종목_상세_항목들)
                        {
                            string 파싱된_값 = 제이슨_문자열_가져오기(아웃풋2, 항목.키);
                            if (!string.IsNullOrEmpty(파싱된_값))
                            {
                                Form1.Console_print($"  * {항목.한글명} ({항목.키}) : {파싱된_값}");
                            }
                        }
                        종목_카운트++;
                    }

                    if (종목_카운트 == 1)
                    {
                        Form1.Console_print(">> 현재 보유 중인 종목이 없습니다.");
                    }
                }
                else
                {
                    Form1.Console_print(">> t0424OutBlock1 데이터(개별 종목)를 찾을 수 없습니다.");
                }

                Form1.Console_print("\n========== [주식잔고2 전체 데이터 출력 종료] ==========\n");
            }
            catch (Exception 에러)
            {
                Form1.Console_print("파싱 중 에러 발생: " + 에러.Message);
            }
        }
    }
}