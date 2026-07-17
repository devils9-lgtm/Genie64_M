using System;
using System.Threading.Tasks;

namespace 지니64
{
    internal class TR_요청
    {

        internal static void 신용융자가능종목요청(string cont_yn, string next_key, bool Priority)
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("kt20016", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("kt20016", 요청);
            }

            async Task 요청()
            {
                try
                {
                    // 1. 토큰 설정
                    string MY_ACCESS_TOKEN = Form1.API_token; // 접근 토큰
             
                    //// 2. 요청 데이터
                    var paramsData = new
                    {
                        crd_stk_grde_tp = "%", //# 신용종목등급구분 %:전체, A:A군, B:B군, C:C군, D:D군, E:E군
                        mrkt_deal_tp = "%" // # 시장거래구분 %:전체, 1:코스피, 0:코스닥
                    };

                    // 3. API 실행
                    await TR_종목정보.종목정보(MY_ACCESS_TOKEN, paramsData, "kt20016", cont_yn, next_key);
                }
                catch (Exception ex)
                {
                    Form1.Console_print("신용융자가능종목요청 요청 실패: " + ex.Message);
                }
            }
        }

        public static void 종목정보조회(string itemcode, bool Priority) // 0
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10100", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10100", 요청);
            }

            async Task 요청()
            {
                try
                {
                    // 1. 토큰 설정
                    string MY_ACCESS_TOKEN = Form1.API_token; // 접근 토큰

                    //// 2. 요청 데이터
                    var paramsData = new
                    {
                        stk_cd = itemcode
                    };

                    //// 3. API 실행
                    await TR_종목정보.종목정보(MY_ACCESS_TOKEN, paramsData, "ka10100", "N", "");
                }
                catch (Exception ex)
                {
                     Form1.Console_print("종목정보리스트 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 계좌평가현황요청(string cont_yn, string next_key, bool Priority) //예수금조회_3
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("kt00004", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("kt00004", 요청);
            }

            async Task 요청()
            {
                try
                {
                    // 1. 토큰 설정
                    string MY_ACCESS_TOKEN = Form1.API_token; // 접근 토큰

                    string dmst_stex_tp = "KRX"; // 거래소구분 KRX:한국거래소,NXT:넥스트트레이드
                    if(Form1.NXT_server)
                    {
                        dmst_stex_tp = "NXT";
                    }

                    // 2. 요청 데이터
                    var paramsData = new
                    {
                        qry_tp = "0",  // # 0:전체, 1:상장폐지종목제외
                        dmst_stex_tp
                    };

                    // 3. API 실행
                    await TR_계좌.계좌(MY_ACCESS_TOKEN, paramsData, "kt00004_계좌평가현황요청", cont_yn, next_key);
                }
                catch (Exception ex)
                {
                     Form1.Console_print("계좌평가현황요청 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 계좌평가잔고내역요청(string cont_yn, string next_key, bool Priority) //잔고요청_5
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("kt00018", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("kt00018", 요청);
            }
           
            async Task 요청()
            {
                try
                {
                    // 1. 토큰 설정
                    string MY_ACCESS_TOKEN = Form1.API_token; // 접근 토큰

                    string dmst_stex_tp = "KRX"; // 거래소구분 KRX:한국거래소,NXT:넥스트트레이드
                    if (Form1.NXT_server)
                    {
                        dmst_stex_tp = "NXT";
                    }

                    // 2. 요청 데이터
                    var paramsData = new
                    {
                        qry_tp = "1",  // 1:합산, 2:개별
                        dmst_stex_tp
                    };

                    // 3. API 실행
                    await TR_계좌.계좌(MY_ACCESS_TOKEN, paramsData, "kt00018_계좌평가잔고내역요청", cont_yn, next_key);
                }
                catch (Exception ex)
                {
                     Form1.Console_print("계좌평가잔고내역요청 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 계좌수익률요청(string cont_yn, string next_key, bool Priority) //예수금조회_3
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10085", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10085", 요청);
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
                        stex_tp = "0",  // # 거래소구분  0 : 통합, 1 : KRX, 2 : NXT
                    };

                    // 3. API 실행
                    await TR_계좌.계좌(MY_ACCESS_TOKEN, paramsData, "ka10085_계좌수익률요청", cont_yn, next_key);
                }
                catch (Exception ex)
                {
                    Form1.Console_print("계좌수익률요청 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 미체결요청(string cont_yn, string next_key, bool Priority) //TR_주문내역요청_6()
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10075", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10075", 요청);
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
                        all_stk_tp = "1",  // 전체종목구분 0:전체, 1:종목
                        trde_tp = "0",  // 매수매도 0:전체, 1:매도, 2:매수
                        stk_cd = "0",  // 종목코드
                        stex_tp = "0"  // 거래소구분 0 : 통합, 1 : KRX, 2 : NXT
                    };

                    // 3. API 실행
                    await TR_계좌.계좌(MY_ACCESS_TOKEN, paramsData, "ka10075_미체결요청", cont_yn, next_key);
                }
                catch (Exception ex)
                {
                     Form1.Console_print("미체결요청 요청 실패: " + ex.Message);
                }
            }

        }

        internal static void 코스피투자자순매수요청(bool Priority) // TR_투자자순매수요청_코스피_7
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10051", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10051", 요청);
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
                        mrkt_tp = "0",  // 시장구분 0:코스피, 1:코스닥, 2:코스피200
                        amt_qty_tp = "0", // 금액수량구분 금액:0, 수량:1
                        base_dt = DateTime.Now.ToString("yyyyMMdd"), // 기준일자 YYYYMMDD
                        stex_tp = "3"  // 거래소구분 1:KRX, 2:NXT, 3:통합
                    };

                    // 3. API 실행
                    await TR_업종.업종(MY_ACCESS_TOKEN, paramsData, "ka10051");
                }
                catch (Exception ex)
                {
                     Form1.Console_print("코스피투자자순매수요청 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 코스닥투자자순매수요청(bool Priority) // TR_투자자순매수요청_코스닥_8
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10051", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10051", 요청);
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
                        mrkt_tp = "1",  // 시장구분 0:코스피, 1:코스닥, 2:코스피200
                        amt_qty_tp = "0", // 금액수량구분 금액:0, 수량:1
                        base_dt = DateTime.Now.ToString("yyyyMMdd"), // 기준일자 YYYYMMDD
                        stex_tp = "3"  // 거래소구분 1:KRX, 2:NXT, 3:통합
                    };

                    // 3. API 실행
                    await TR_업종.업종(MY_ACCESS_TOKEN, paramsData, "ka10051");
                }
                catch (Exception ex)
                {
                     Form1.Console_print("코스닥투자자순매수요청 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 코스피_프로그램매매추이요청시간대별(bool Priority) // TR_프로그램매매추이요청_코스피_9
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka90005", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka90005", 요청);
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
                        date = DateTime.Now.ToString("yyyyMMdd"),  // 날짜 YYYYMMDD
                        amt_qty_tp = "1", // 금액수량구분 1:금액(백만원), 2:수량(천주)
                        mrkt_tp = "P001_AL01", // 시장구분 코스피
                        min_tic_tp = "1",  // 분틱구분 0:틱, 1:분
                        stex_tp = "3"  // 거래소구분 1:KRX, 2:NXT 3.통합
                    };

                    // 3. API 실행
                    await TR_시세.시세(MY_ACCESS_TOKEN, paramsData, "ka90005");
                }
                catch (Exception ex)
                {
                    Form1.Console_print($"[오류] 코스피_프로그램매매추이요청시간대별 요청 실패: {ex.Message}");
                }
            }
        }

        internal static void 코스닥_프로그램매매추이요청시간대별(bool Priority) // TR_프로그램매매추이요청_코스닥_10
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka90005", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka90005", 요청);
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
                        date = DateTime.Now.ToString("yyyyMMdd"),  // 날짜 YYYYMMDD
                        amt_qty_tp = "1", // 금액수량구분 1:금액(백만원), 2:수량(천주)
                        mrkt_tp = "P101_AL02", // 시장구분 코스피- 거래소구분값 1일경우:P00101, 2일경우:P001_NX01, 3일경우:P001_AL01
                                               // 코스닥 - 거래소구분값 1일경우: P10102, 2일경우: P101_NX02, 3일경우: P101_AL02
                        min_tic_tp = "1",  // 분틱구분 0:틱, 1:분
                        stex_tp = "3"  // 거래소구분 1:KRX, 2:NXT 3.통합
                    };

                    // 3. API 실행
                    await TR_시세.시세(MY_ACCESS_TOKEN, paramsData, "ka90005");
                }
                catch (Exception ex)
                {
                     Form1.Console_print("코스닥_프로그램매매추이요청시간대별 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 주식호가요청(string itemcode, bool Priority) // TR_호가요청_11()
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10004", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10004", 요청);
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
                        stk_cd = itemcode + "_AL" // 종목코드 (KRX: 039490, NXT: 039490_NX, SOR: 039490_AL)
                    };

                    // 3. API 실행
                    await TR_시세.시세(MY_ACCESS_TOKEN, paramsData, "ka10004");
                }
                catch (Exception ex)
                {
                     Form1.Console_print("주식호가요청 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 주식분봉차트조회요청(string itemcode, bool Priority)
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10080", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10080", 요청);
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
                        stk_cd = itemcode + "_AL",  // 종목코드 거래소별 종목코드 (KRX:039490,NXT:039490_NX,SOR:039490_AL)
                        tic_scope = "1",  // 틱범위 1:1분, 3:3분, 5:5분, 10:10분, 15:15분, 30:30분, 45:45분, 60:60분
                        upd_stkpc_tp = "1"  // 수정주가구분 0 or 1
                    };

                    // 3. API 실행
                    await TR_차트.차트(MY_ACCESS_TOKEN, paramsData, "ka10080");
                }
                catch (Exception ex)
                {
                     Form1.Console_print("주식분봉차트조회요청 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 주식일봉차트조회요청(string itemcode, bool Priority)
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10081", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10081", 요청);
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
                        stk_cd = itemcode + "_AL",  // 종목코드 거래소별 종목코드 (KRX:039490,NXT:039490_NX,SOR:039490_AL)
                        base_dt = DateTime.Now.ToString("yyyyMMdd"),  // 기준일자 YYYYMMDD
                        upd_stkpc_tp = "1"  // 수정주가구분 0 or 1
                    };

                    // 3. API 실행
                    await TR_차트.차트(MY_ACCESS_TOKEN, paramsData, "ka10081");
                }
                catch (Exception ex)
                {
                     Form1.Console_print("주식일봉차트조회요청 요청 실패: " + ex.Message);
                }
            }
        }




        internal static void 당일실현손익요청(string cont_yn, string next_key, bool Priority)
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10072", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10072", 요청);
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
                        stk_cd = "",  // 종목코드 
                        strt_dt = DateTime.Now.ToString("yyyyMMdd")  // 시작일자 YYYYMMDD 
                    };

                    // 3. API 실행
                    await TR_계좌.계좌(MY_ACCESS_TOKEN, paramsData, "ka10072_당일실현손익요청", cont_yn, next_key);
                }
                catch (Exception ex)
                {
                     Form1.Console_print("당일실현손익요청 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 실현손익요청(string cont_yn, string next_key, bool Priority)
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10074", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10074", 요청);
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
                        strt_dt = DateTime.Now.ToString("yyyyMMdd"),  // 종목코드 
                        end_dt = DateTime.Now.ToString("yyyyMMdd")  // 시작일자 YYYYMMDD 
                    };

                    // 3. API 실행
                    await TR_계좌.계좌(MY_ACCESS_TOKEN, paramsData, "ka10074_실현손익요청", cont_yn, next_key);
                }
                catch (Exception ex)
                {
                     Form1.Console_print("실현손익요청 요청 실패: " + ex.Message);
                }
            }
        }


        internal static void 일자별실현손익요청_통계(string cont_yn, string next_key, bool Priority)
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10074", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10074", 요청);
            }

            async Task 요청()
            {
                string 년 = Form1.form1.TB_통계.Text.Trim();
                string 시작일 = 년 + "0101";
                string 종료일 = 년 + "1231";

                try
                {
                    // 1. 토큰 설정
                    string MY_ACCESS_TOKEN = Form1.API_token; // 접근 토큰

                    // 2. 요청 데이터
                    var paramsData = new
                    {
                        strt_dt = 시작일,  // 시작일자
                        end_dt = 종료일  // 종료일자
                    };

                    // 3. API 실행
                    await TR_계좌.계좌(MY_ACCESS_TOKEN, paramsData, "ka10074_일자별실현손익요청통계", cont_yn, next_key);
                }
                catch (Exception ex)
                {
                     Form1.Console_print("일자별실현손익요청_통계 요청 실패: " + ex.Message);
                }
            }
        }


        internal static void 일자별종목별실현손익요청_통계(string cont_yn, string next_key, string strt_dt, bool Priority)
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10072", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10072", 요청);
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
                        stk_cd = " ",  // 종목코드 
                        strt_dt = strt_dt  // 시작일자 YYYYMMDD 
                    };

                    // 3. API 실행
                    await TR_계좌.계좌(MY_ACCESS_TOKEN, paramsData, $"ka10072_일자별종목별실현손익요청통계_{strt_dt}", cont_yn, next_key);
                }
                catch (Exception ex)
                {
                     Form1.Console_print("일자별종목별실현손익요청_통계 요청 실패: " + ex.Message);
                }
            }
        }

    }
}
