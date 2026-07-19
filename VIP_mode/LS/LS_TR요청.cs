using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using 지니64.VIP_mode.로그인;

namespace 지니64
{
    // =========================================================================
    // LS증권 전용 주식잔고조회 스케줄러 브릿지 클래스
    // =========================================================================
    public static class LS_TR요청
    {
       
        public static void LS_현물계좌예수금_주문가능금액_총평가조회(string 연속_여부, bool 우선순위_여부)
        {
            Form1.Console_print($"LS증권 현물계좌예수금 주문가능금액 총평가 조회 요청 준비");

            // [최적화] 내부 대리자의 불필요한 GC 점유를 피하기 위한 로컬 비동기 태스크 선언
            async Task API_요청_작업()
            {
                try
                {
                    bool isSimulation = GenieConfig.checkBox_Simulation;

                    string 접근_토큰 = Form1.LS_API_token;
                    string 앱키 = Form1.LS_앱키;
                    string 앱시크릿 = Form1.LS_시크릿키;

                    //  [최적화] Dictionary 할당 비용 및 Boxing / Unboxing 부하를 없애기 위해 익명 객체(Anonymous Type) 사용
                    var 파라미터_데이터 = new
                    {
                        CSPAQ12200InBlock1 = new
                        {
                            BalCreTp = "0"             // 잔고생성구분 String  Y   1
                        }
                    };

                    string tr_식별자 = "CSPAQ12200";

                    // ConfigureAwait(false)로 스케줄러 스레드의 안전한 작업 보장
                    await TR_LS_계좌.계좌조회_요청(접근_토큰, 앱키, 앱시크릿, 파라미터_데이터, tr_식별자, 연속_여부).ConfigureAwait(false);
                }
                catch (Exception 예외)
                {
                    Form1.Console_print("[-] LS증권 주식잔고조회 실패: " + 예외.Message);
                }
            }

            // 스케줄러 큐 등록 분기 (기존 형식 유지)
            if (우선순위_여부)
            {
                Form1.LS_스케줄러.우선_요청_추가(API_요청_작업);
            }
            else
            {
                Form1.LS_스케줄러.요청_추가(API_요청_작업);
            }
        }

        public static void LS_현물계좌_잔고내역조회(string 연속_여부, bool 우선순위_여부)
        {
            Form1.Console_print($"LS증권 현물계좌 잔고내역 조회(CSPAQ12300) 요청 준비");

            // [최적화] 내부 대리자의 불필요한 GC 점유를 피하기 위한 로컬 비동기 태스크 선언
            async Task API_요청_작업()
            {
                try
                {
                    bool isSimulation = GenieConfig.checkBox_Simulation;

                    string 접근_토큰 = Form1.LS_API_token;
                    string 앱키 = Form1.LS_앱키;
                    string 앱시크릿 = Form1.LS_시크릿키;

                    // [최적화] Dictionary 할당 비용 및 Boxing/Unboxing 부하를 없애기 위해 익명 객체(Anonymous Type) 사용
                    var 파라미터_데이터 = new
                    {
                        CSPAQ12300InBlock1 = new
                        {
                            RecCnt = 1,
                            BalCreTp = "0",          // 잔고생성구분
                            CmsnAppTpCode = "0",     // 수수료적용구분
                            D2balBaseQryTp = "0",    // D2잔고기준 조회구분
                            UprcTpCode = "0"         // 단가구분
                        }
                    };

                    string tr_식별자 = "CSPAQ12300";

                    // ConfigureAwait(false)로 스케줄러 스레드의 안전한 작업 보장 및 불필요한 컨텍스트 스위칭 최소화
                    await TR_LS_계좌.계좌조회_요청(접근_토큰, 앱키, 앱시크릿, 파라미터_데이터, tr_식별자, 연속_여부).ConfigureAwait(false);
                }
                catch (Exception 예외)
                {
                    Form1.Console_print("[-] LS증권 주식잔고내역 조회(CSPAQ12300) 실패: " + 예외.Message);
                }
            }

            // 스케줄러 큐 등록 분기 (기존 형식 유지)
            if (우선순위_여부)
            {
                Form1.LS_스케줄러.우선_요청_추가(API_요청_작업);
            }
            else
            {
                Form1.LS_스케줄러.요청_추가(API_요청_작업);
            }
        }



        public static void LS_주식잔고2(string 연속_여부, bool 우선순위_여부)
        {
            Form1.Console_print($"LS증권 현물계좌 잔고내역 조회(CSPAQ12300) 요청 준비");

            // [최적화] 내부 대리자의 불필요한 GC 점유를 피하기 위한 로컬 비동기 태스크 선언
            async Task API_요청_작업()
            {
                try
                {
                    bool isSimulation = GenieConfig.checkBox_Simulation;

                    string 접근_토큰 = Form1.LS_API_token;
                    string 앱키 = Form1.LS_앱키;
                    string 앱시크릿 = Form1.LS_시크릿키;

                    // [최적화] Dictionary 할당 비용 및 Boxing/Unboxing 부하를 없애기 위해 익명 객체(Anonymous Type) 사용
                    var 파라미터_데이터 = new
                    {
                        t0424InBlock = new
                        {
                            prcgb = "1",        // 단가구분 1 : 평균단가 2 : BEP단가
                            chegb = "1",           // 체결구분  0 : 결제기준잔고 2 : 체결기준(잔고가 0이 아닌 종목만 조회)
                            dangb = "0",     // 제비용포함여부  0 : 정규장 1 : 시간외단일가
                            charge = "1",     // 제비용포함여부 	0 : 제비용미포함 1 : 제비용포함
                            cts_expcode = ""       //CTS_종목번호 연속조회시 OutBlock의 동일필드 입력
                        }
                    };

                    string tr_식별자 = "t0424";

                    // ConfigureAwait(false)로 스케줄러 스레드의 안전한 작업 보장 및 불필요한 컨텍스트 스위칭 최소화
                    await TR_LS_계좌.계좌조회_요청(접근_토큰, 앱키, 앱시크릿, 파라미터_데이터, tr_식별자, 연속_여부).ConfigureAwait(false);
                }
                catch (Exception 예외)
                {
                    Form1.Console_print("[-] LS_주식잔고2 조회(t0424) 실패: " + 예외.Message);
                }
            }

            // 스케줄러 큐 등록 분기 (기존 형식 유지)
            if (우선순위_여부)
            {
                Form1.LS_스케줄러.우선_요청_추가(API_요청_작업);
            }
            else
            {
                Form1.LS_스케줄러.요청_추가(API_요청_작업);
            }
        }
    }
}