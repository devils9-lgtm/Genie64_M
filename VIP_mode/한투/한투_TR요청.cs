using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using 지니64.VIP_mode.로그인;

namespace 지니64
{
    // =========================================================================
    // 한국투자증권 전용 주식잔고조회 스케줄러 브릿지 클래스
    // =========================================================================
    public static class 한투_TR요청
    {
        public static void 한투_주식잔고조회(string CTX_AREA_FK100, string CTX_AREA_NK100, string 연속_여부, bool 우선순위_여부)
        {
            Form1.Console_print($"한국투자 주식잔고조회 요청 준비");

            // [최적화] 내부 대리자의 불필요한 GC 점유를 피하기 위한 로컬 비동기 태스크 선언
            async Task API_요청_작업()
            {
                try
                {
                    bool isSimulation = GenieConfig.checkBox_Simulation;

                    string 접근_토큰 = Form1.한투_API_token;
                    string 앱키 = Form1.한투_앱키;
                    string 앱시크릿 = Form1.한투_시크릿키;

                    // [오류 방지 및 최적화] 딕셔너리 사전 크기를 지정하고, 빈 문자열 유발 에러 방지
                    var 파라미터_데이터 = new Dictionary<string, string>(12)
                    {
                        { "CANO", Form1.한투_CANO },
                        { "ACNT_PRDT_CD", Form1.한투_ACNT_PRDT_CD },
                        { "AFHR_FLPR_YN", "N" },
                        { "OFL_YN", "N" }, // <- 빈 문자열("")에서 "N"으로 수정하여 서버 타임아웃 방지
                        { "INQR_DVSN", "01" },
                        { "UNPR_DVSN", "01" },
                        { "FUND_STTL_ICLD_YN", "N" },
                        { "FNCG_AMT_AUTO_RDPT_YN", "N" },
                        { "PRCS_DVSN", "00" },
                        { "CTX_AREA_FK100", CTX_AREA_FK100 },
                        { "CTX_AREA_NK100", CTX_AREA_NK100 }
                    };

                    string tr_식별자 = isSimulation ? "VTTC8434R_주식잔고조회" : "TTTC8434R_주식잔고조회";

                    // ConfigureAwait(false)로 스케줄러 스레드의 안전한 작업 보장
                    await TR한투_계좌.계좌조회_요청(접근_토큰, 앱키, 앱시크릿, 파라미터_데이터, tr_식별자, 연속_여부).ConfigureAwait(false);
                }
                catch (Exception 예외)
                {
                    Form1.Console_print("[-] 한국투자 주식잔고조회 실패: " + 예외.Message);
                }
            }

            // 스케줄러 큐 등록 분기
            if (우선순위_여부)
            {
                Form1.한투_스케줄러.우선_요청_추가(API_요청_작업);
            }
            else
            {
                Form1.한투_스케줄러.요청_추가(API_요청_작업);
            }
        }
    }

}