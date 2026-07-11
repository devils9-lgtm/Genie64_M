using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 지니64
{
    public class 초기화
    {
        public class 숫자
        {
            public int 시장시작;
            public int 시장종료;
            public int 메인마켓시작;
            public int 메인마켓종료;
            public int 로딩완료타임;
            public int TimeNow;

            public int Timer_S;
            public int Timer_M;
            public int Timer_H;
            public int 분_180;
            public int 분_count;
            public int 분_time;

            public int Repeat_time_A;
            public int Repeat_time_B;
            public int Repeat_time_C;
            public int Repeat_time_D;
            public int Repeat_time_E;
            public int Repeat_time_F;
            public int Repeat_time_G;
            public int Repeat_time_H;
            public int Repeat_time_I;
            public int Repeat_time_J;
            public int Repeat_time_K;
            public int Repeat_time_L;
            public int Repeat_time_M;
            public int Repeat_time_N;
            public int TT_rebalance_time_A;
            public int TT_rebalance_time_B;
            public int TT_rebalance_time_C;
            public int TT_rebalance_time_D;
            public int TT_rebalance_time_E;
            public int TT_rebalance_time_F;
            public int TT_rebalance_time_G;
            public int TT_Liqu_time_A;
            public int TT_Liqu_time_B;
            public int TT_Liqu_time_C;

            public int time_Run_time;
            public int time_Run_silson_W;
            public int time_Run_예상수익;
            public int time_Run_예상손실;

            public int 주문_S;
            public int 주문_M;
            public int 주문_H;

            public int 신규개수A;
            public int 신규개수B;
            public int 신규개수C;
            public int maximum_time;
            public int 오전감시시간;
            public int 오후감시시간;
            public int Ten;
            public int 미체결종목_index;
            public int box1_Closetime;
            public int layoutindex;

            public long 실현손익_예상;
            public long 실현손익_시작;

            public double Cut_남길금액_A;
            public double Cut_남길금액_B;
            public double Cut_남길금액_C;
            public double 예상수익_TS;
            public int 검색식_tick;
            public int 체결갯수;

            public int 신규횟수;
            public int 매수탐색count_A;
            public int 매수탐색count_B;
            public int 매도탐색count;

            public static 숫자 LoadFromSettings()
            {
                DateTime now = DateTime.Now;
                int 현재시간_숫자 = (now.Hour * 10000) + (now.Minute * 100) + now.Second;

                return new 숫자
                {
                    시장시작 = 80000,
                    시장종료 = 200000,
                    메인마켓시작 = 90000,
                    메인마켓종료 = 153000,
                    로딩완료타임 = 242400,
                    TimeNow = 현재시간_숫자,
                    분_time = 1,
                    체결갯수 = 1,
                    maximum_time = 10000,
                    box1_Closetime = 10
                };
            }
        }

        // =================================================================
        // [최적화] 구조체(struct)를 클래스(class)로 변경하여 무거운 문자열 복사 제거!
        // =================================================================
        public class 문자
        {
            public string Week;
            public string today;
            public string Month;
            public string date_yyyyMMdd;
            public string 이탈삭제;
            public string 신규매수방법;

            public string cut_LB_A;
            public string cut_LB_B;
            public string cut_LB_C;

            public string 청산검색식;
            public string 주문초과종목명;
            public string MBC_sender;
            public string 매매시작;
            public string 개장일;
            public string 수능일;

            public static 문자 LoadFromSettings()
            {
                return new 문자
                {
                    Week = GET.요일가져오기(),
                    today = DateTime.Now.ToString("yyyy/MM/dd"),
                    Month = DateTime.Now.ToString("yyyyMM"),
                    date_yyyyMMdd = DateTime.Now.ToString("yyyyMMdd"),
                    개장일 = "0102",
                    수능일 = "0000",
                    cut_LB_A = "X",
                    cut_LB_B = "X",
                    cut_LB_C = "X"
                };
            }
        }

        // =================================================================
        // [최적화] 검색식 조건들을 담는 상자도 클래스(class)로 경량화!
        // =================================================================
        public class 검색식
        {
            public string new_condition_A;
            public string new_condition_B;
            public string new_condition_C;

            public string repeat_condition_A;
            public string repeat_condition_B;
            public string repeat_condition_C;
            public string repeat_condition_D;
            public string repeat_condition_E;
            public string repeat_condition_F;
            public string repeat_condition_G;
            public string repeat_condition_H;
            public string repeat_condition_I;
            public string repeat_condition_J;
            public string repeat_condition_K;
            public string repeat_condition_L;
            public string repeat_condition_M;
            public string repeat_condition_N;

            public string rebalance_condition_A;
            public string rebalance_condition_B;
            public string rebalance_condition_C;
            public string rebalance_condition_D;
            public string rebalance_condition_E;
            public string rebalance_condition_F;
            public string rebalance_condition_G;

            public string Liquidation_condition_A;
            public string Liquidation_condition_B;
            public string Liquidation_condition_C;

            public string watch_추가_condition_A;
            public string watch_추가_condition_B;
            public string watch_추가_condition_C;
            public string watch_추가_condition_D;

            public static 검색식 LoadFromSettings()
            {
                return new 검색식()
                {
                    // 기본값이 필요하다면 여기에 추가하시면 됩니다.
                };
            }
        }

        public class Account
        {
            // ==========================================================
            // ⭐ [신규 추가 필드] - 최상단 배치 (이미지 기반)
            // ==========================================================
            public long 투자원금;          // 계좌.png: '투자원금' 기준값
            public long 주문가능금액;      // 계좌.png: '주문가능'
            public long 총매수수수료;      // 수익률산출식.png: 10원 미만 절사 합계
            public long 총매도수수료;      // 수익률산출식.png: 10원 미만 절사 합계
            public long 총제세금;          // 수익률산출식.png: 원 미만 절사 합계
            public long 총신용이자;        // 수익률산출식.png 하단: 실시간 오차 보정용 이자

            // ==========================================================
            // 🔹 [기존 필드] - 변수명 변경 및 누락 없이 그대로 유지
            // ==========================================================
            public long 기준계산금;
            public long 증가자산;
            public long 추정자산;
            public long 당일예수금;
            public long 증거금현금;
            public long 추정당일예수금;
            public long D2;
            public long 추정D2;
            public long 매입금;
            public long 평가금;
            public long 평가손익;
            public double 평가손익률;
            public long 실현손익;
            public double 실현손익률;
            public string 시간청산;
            public string 실현손익금청산;
            public string 예상손실청산;
            public string 예상수익청산;
            public double 피_현재가;
            public double 피_등락률;
            public double 피_저가대비;
            public double 피_고가대비;
            public double 닥_현재가;
            public double 닥_등락률;
            public double 닥_저가대비;
            public double 닥_고가대비;
            public string 신규_A;
            public string 신규_B;
            public string 신규_C;
            public string 기간_A;
            public string 기간_B;
            public string 기간_C;
            public string 기간_D;
            public string 기간_E;
            public string 기간_F;
            public string 반복_A;
            public string 반복_B;
            public string 반복_C;
            public string 반복_D;
            public string 반복_E;
            public string 반복_F;
            public string 반복_G;
            public string 반복_H;
            public string 반복_I;
            public string 반복_J;
            public string 반복_K;
            public string 반복_L;
            public string 반복_M;
            public string 반복_N;
            public string 리밸_A;
            public string 리밸_B;
            public string 리밸_C;
            public string 리밸_D;
            public string 리밸_E;
            public string 리밸_F;
            public string 리밸_G;
            public string 청산_A;
            public string 청산_B;
            public string 청산_C;
            public double 매입비;
            public int 피_외인;
            public int 피_기관;
            public int 피_프로그램;
            public int 닥_외인;
            public int 닥_기관;
            public int 닥_프로그램;
            public string 피_누적거래대금;
            public string 피_상한종목수;
            public string 피_상승종목수;
            public string 피_보합종목수;
            public string 피_하한종목수;
            public string 피_하락종목수;
            public string 닥_누적거래대금;
            public string 닥_상한종목수;
            public string 닥_상승종목수;
            public string 닥_보합종목수;
            public string 닥_하한종목수;
            public string 닥_하락종목수;

            // ==========================================================
            // ⚙️ [설정 로드]
            // ==========================================================
            public static Account LoadFromSettings()
            {
                return new Account
                {
                    투자원금 = 300000,        // 계좌.png 이미지의 예시값 기준
                    시간청산 = "0",
                    실현손익금청산 = "0",
                    예상손실청산 = "0",
                    예상수익청산 = "0",
                    신규_A = "기본값",
                    신규_B = "기본값",
                    신규_C = "기본값",
                    기간_A = "기본값",
                    기간_B = "기본값",
                    기간_C = "기본값",
                    기간_D = "기본값",
                    기간_E = "기본값",
                    기간_F = "기본값",
                    반복_A = "기본값",
                    반복_B = "기본값",
                    반복_C = "기본값",
                    반복_D = "기본값",
                    반복_E = "기본값",
                    반복_F = "기본값",
                    반복_G = "기본값",
                    반복_H = "기본값",
                    반복_I = "기본값",
                    반복_J = "기본값",
                    반복_K = "기본값",
                    반복_L = "기본값",
                    반복_M = "기본값",
                    반복_N = "기본값",
                    리밸_A = "기본값",
                    리밸_B = "기본값",
                    리밸_C = "기본값",
                    리밸_D = "기본값",
                    리밸_E = "기본값",
                    리밸_F = "기본값",
                    리밸_G = "기본값",
                    청산_A = "기본값",
                    청산_B = "기본값",
                    청산_C = "기본값"
                };
            }

            // --- [수익률 산출 보조 메서드] ---
            public long 절사10원(long 금액) => (금액 / 10) * 10;
        }


        // =================================================================
        // [최적화 완료] bool 변수 모음도 struct에서 class로 변경하여 메모리 복사 제거!
        // =================================================================
        public class CanTrade
        {
            public bool 반복매매_USE;
            public bool Rebalancing_USE;
            public bool 계좌통매도_청산;
            public bool 수익금기준손절;
            public bool Liquidation_USE;
            public bool 잔고_익절;
            public bool 잔고_보전;
            public bool 잔고_손실매도;
            public bool 트레일링스탑;
            public bool 미수정리;
            public bool TimeSell;
            public bool 그룹지정매매;
            public bool 매매일_기준거래;
            public bool 상하_전량매도;
            public bool Watch;

            public static CanTrade LoadFromSettings()
            {
                return new CanTrade
                {
                    미수정리 = GenieConfig.CB_misu,

                    반복매매_USE = GenieConfig.CB_repeat_use_A || GenieConfig.CB_repeat_use_B || GenieConfig.CB_repeat_use_C ||
                                   GenieConfig.CB_repeat_use_D || GenieConfig.CB_repeat_use_E || GenieConfig.CB_repeat_use_F ||
                                   GenieConfig.CB_repeat_use_G || GenieConfig.CB_repeat_use_H || GenieConfig.CB_repeat_use_I ||
                                   GenieConfig.CB_repeat_use_J || GenieConfig.CB_repeat_use_K || GenieConfig.CB_repeat_use_L ||
                                   GenieConfig.CB_repeat_use_M || GenieConfig.CB_repeat_use_N,

                    Rebalancing_USE = GenieConfig.CB_rebalance_A || GenieConfig.CB_rebalance_B || GenieConfig.CB_rebalance_C ||
                                      GenieConfig.CB_rebalance_D || GenieConfig.CB_rebalance_E || GenieConfig.CB_rebalance_F ||
                                      GenieConfig.CB_rebalance_G,

                    계좌통매도_청산 = GenieConfig.CB_sell_time_use || GenieConfig.CB_silson_use_W || GenieConfig.CB_예상손실_use || GenieConfig.CB_예상수익사용,

                    수익금기준손절 = GenieConfig.CB_cut_A || GenieConfig.CB_cut_B || GenieConfig.CB_cut_C,

                    Liquidation_USE = GenieConfig.CB_Liquidation_A || GenieConfig.CB_Liquidation_B || GenieConfig.CB_Liquidation_C,

                    잔고_익절 = GenieConfig.CB_ik_A || GenieConfig.CB_ik_B || GenieConfig.CB_ik_C || GenieConfig.CB_ik_D || GenieConfig.CB_ik_E || GenieConfig.CB_ik_F || GenieConfig.CB_ik_G || GenieConfig.CB_ik_H || GenieConfig.CB_ik_I,
                    잔고_보전 = GenieConfig.CB_ik_down_A || GenieConfig.CB_ik_down_B || GenieConfig.CB_ik_down_C || GenieConfig.CB_ik_down_D || GenieConfig.CB_ik_down_E || GenieConfig.CB_ik_down_F || GenieConfig.CB_ik_down_G || GenieConfig.CB_ik_down_H || GenieConfig.CB_ik_down_I,
                    잔고_손실매도 = GenieConfig.CB_sell_use_A || GenieConfig.CB_sell_use_B || GenieConfig.CB_sell_use_C || GenieConfig.CB_sell_use_D || GenieConfig.CB_sell_use_E || GenieConfig.CB_sell_use_F,
                    트레일링스탑 = GenieConfig.CB_TS_A || GenieConfig.CB_TS_B || GenieConfig.CB_TS_C || GenieConfig.CB_TS_D || GenieConfig.CB_TS_E || GenieConfig.CB_TS_F || GenieConfig.CB_TS_G || GenieConfig.CB_TS_H || GenieConfig.CB_TS_I,

                    TimeSell = GenieConfig.CB_TimeSell_A || GenieConfig.CB_TimeSell_B || GenieConfig.CB_TimeSell_C,
                    그룹지정매매 = GenieConfig.CBB_In_group_A > 0 || GenieConfig.CBB_In_group_B > 0 || GenieConfig.CBB_In_group_C > 0 || GenieConfig.CBB_In_group_D > 0,
                    매매일_기준거래 = GenieConfig.CBB_매매기간_trading_A > 0 || GenieConfig.CBB_매매기간_trading_B > 0 || GenieConfig.CBB_매매기간_trading_C > 0 || GenieConfig.CBB_매매기간_trading_D > 0 || GenieConfig.CBB_매매기간_trading_E > 0 || GenieConfig.CBB_매매기간_trading_F > 0,
                    상하_전량매도 = GenieConfig.CB_상전량청산 || GenieConfig.CB_하전량청산,

                    Watch = GenieConfig.CBB_Watch_ID_A > 0 || GenieConfig.CBB_Watch_ID_B > 0 || GenieConfig.CBB_Watch_ID_C > 0 || GenieConfig.CBB_Watch_ID_D > 0,
                };
            }
        }
    }
}
