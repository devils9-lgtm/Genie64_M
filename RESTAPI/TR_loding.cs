using System;
using System.Threading.Tasks;

namespace 지니64.RESTAPI
{
    internal class TR_loding
    {
        public static void 종목정보리스트(string market, bool Priority) // 0
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka10099", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka10099", 요청);
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
                        mrkt_tp = market // 시장구분 0:코스피,10:코스닥,3:ELW,8:ETF,30:K-OTC,50:코넥스,5:신주인수권,4:뮤추얼펀드,6:리츠,9:하이일드
                    };

                    //// 3. API 실행
                    await TR_종목정보.종목정보(MY_ACCESS_TOKEN, paramsData, "ka10099", "N", "");
                }
                catch (Exception ex)
                {
                     Form1.Console_print("종목정보리스트 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 코스피현재가요청(bool Priority) // 코스피현재가요청_1 
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka20001", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka20001", 요청);
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
                        mrkt_tp = "0",  // # 시장구분 0:코스피, 1:코스닥, 2:코스피200
                        inds_cd = "001"  // # 업종코드 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고
                    };

                    // 3. API 실행
                    await TR_업종.업종(MY_ACCESS_TOKEN, paramsData, "ka20001");
                }
                catch (Exception ex)
                {
                     Form1.Console_print("코스피현재가요청 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 코스닥현재가요청(bool Priority) // 코스닥현재가요청_2
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka20001", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka20001", 요청);
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
                        mrkt_tp = "1",  // # 시장구분 0:코스피, 1:코스닥, 2:코스피200
                        inds_cd = "101"  // # 업종코드 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고
                    };

                    // 3. API 실행
                    await TR_업종.업종(MY_ACCESS_TOKEN, paramsData, "ka20001");
                }
                catch (Exception ex)
                {
                     Form1.Console_print("코스닥현재가요청 요청 실패: " + ex.Message);
                }
            }
        }


        internal static void 업종분봉조회요청(string inds_cd, bool Priority)
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka20005", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka20005", 요청);
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
                        inds_cd = inds_cd,  // 업종코드 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고
                        tic_scope = "1"  // 틱범위 1:1틱, 3:3틱, 5:5틱, 10:10틱, 30:30틱
                    };

                    // 3. API 실행
                    await TR_차트.차트(MY_ACCESS_TOKEN, paramsData, "ka20005");
                }
                catch (Exception ex)
                {
                     Form1.Console_print("업종분봉조회요청 요청 실패: " + ex.Message);
                }
            }
        }

        internal static void 업종일봉조회요청(string inds_cd, bool Priority)
        {
            if (Priority)
            {
                Form1.tr_scheduler.EnqueuePriorityRequest("ka20006", 요청);
            }
            else
            {
                Form1.tr_scheduler.EnqueueRequest("ka20006", 요청);
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
                        inds_cd = inds_cd,  // 업종코드 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고
                        base_dt = DateTime.Now.ToString("yyyyMMdd")  // 기준일자 YYYYMMDD
                    };

                    // 3. API 실행
                    await TR_차트.차트(MY_ACCESS_TOKEN, paramsData, "ka20006");
                }
                catch (Exception ex)
                {
                     Form1.Console_print("업종일봉조회요청 요청 실패: " + ex.Message);
                }
            }
        }

    }
}
