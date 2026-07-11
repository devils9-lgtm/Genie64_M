using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using 지니64.Timer;

namespace 지니64
{
    public class Tab_Repeat : Form1
    {
        public static void Repeat_condition(string ST_ID, string itemcode, string condition)
        {
            bool ID = false;

            string[] assign = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N" };

            int[] settings = new int[] {
                 GenieConfig.combo_repeat_use_condition_A,
                 GenieConfig.combo_repeat_use_condition_B,
                 GenieConfig.combo_repeat_use_condition_C,
                 GenieConfig.combo_repeat_use_condition_D,
                 GenieConfig.combo_repeat_use_condition_E,
                 GenieConfig.combo_repeat_use_condition_F,
                 GenieConfig.combo_repeat_use_condition_G,
                 GenieConfig.combo_repeat_use_condition_H,
                 GenieConfig.combo_repeat_use_condition_I,
                 GenieConfig.combo_repeat_use_condition_J,
                 GenieConfig.combo_repeat_use_condition_K,
                 GenieConfig.combo_repeat_use_condition_L,
                 GenieConfig.combo_repeat_use_condition_M,
                 GenieConfig.combo_repeat_use_condition_N
             };

            for (int i = 0; i < settings.Length; i++)
            {
                int settingValue = settings[i];
                string where = "반복_" + assign[i];

                if (settingValue == 1)
                {
                    if (ST_ID.Equals("I")) ID = true;
                    in_out(where);
                }
                else if (settingValue == 2)
                {
                    if (ST_ID.Equals("D")) ID = true;
                    in_out(where);
                }
            }

            // [가정] Form1.Repeat_catch_list는 ConcurrentDictionary<string, Catch_stock> 타입으로 변경됨
            // 키는 location + itemcode 조합을 사용합니다.

            void in_out(string where)
            {
                // 1. 조건 이름 체크
                if (condition.Equals(Form1.위치별검색식리스트[where].이름))
                {
                    // 2. O(1) 검색을 위한 고유 키 생성
                    string key = where + itemcode;

                    if (ID) // 추가 조건 (ID == true: 조건 만족, 리스트에 진입해야 함)
                    {
                        // [최적화] TryAdd를 사용하여 키가 없으면 추가하고, 있으면 무시 (스레드 안전, O(1))
                        // Repeat_catch 대신 Catch_stock 객체를 생성하여 추가합니다.
                        Form1.Catch_Stock_List.TryAdd(key, new Catch_stock(itemcode, 0));
                        // 
                    }
                    else // 제거 조건 (ID == false: 조건 불만족, 리스트에서 나가야 함)
                    {
                        // [최적화] TryRemove를 사용하여 키가 있으면 안전하게 제거 (O(1))
                        // List.Find, List.Remove의 느린 연산을 모두 대체합니다.
                        Form1.Catch_Stock_List.TryRemove(key, out _);
                    }
                }
            }
        }

        public static async Task 반복매매_USE(Stockbalance 잔고)
        {
            // 1. 공통 유효성 검사 (빠른 탈출)
            if (재시작) return;
            if (!잔고.매매가능) return;
            if (GET.총주문가능수량(잔고) <= 0) return;

            // 2. 공통 데이터 캐싱 (한 번만 불러와서 성능 향상)
            string 종목코드 = 잔고.종목코드;

            // 데이터 존재 여부 확인 (안전성)
            if (!Form1.Market_Item_List.ContainsKey(종목코드)) return;
            Market_Item market_Item = Form1.Market_Item_List[종목코드];

            MAPeriod min_mma = Form1.Min_ma_list.ContainsKey(종목코드) ? Form1.Min_ma_list[종목코드] : null;
            MAPeriod day_mma = Form1.Day_ma_list.ContainsKey(종목코드) ? Form1.Day_ma_list[종목코드] : null;

            if (min_mma == null || day_mma == null) return;

            // 3. 그룹별 순차 실행 (A -> B -> ... -> N)
            // 필요한 그룹만큼 배열에 추가하면 자동으로 실행됨
            string[] groups = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N" };

            foreach (var suffix in groups)
            {
              await  반복매매_그룹_실행(잔고, market_Item, min_mma, day_mma, suffix);
            }
        }

        // [핵심 로직] 그룹명(suffix)에 따라 설정을 매핑하고 실행
        private static async Task 반복매매_그룹_실행(Stockbalance 잔고, Market_Item market_Item, MAPeriod min_mma, MAPeriod day_mma, string suffix)
        {
            // =========================================================
            // 1. 변수 선언 (기본값 초기화)
            // =========================================================
            bool 사용체크 = false;
            bool 잔고가동 = false;
            int 시작시간 = 0, 종료시간 = 0;
            double 매입비중 = 0;
            long 누적거래량 = 0, 누적거래대금 = 0;
            bool 매수도 = false; // 매수/매도 구분

            int 반복조건 = 0;
            string 검색식명 = "";
            string 위치 = "반복_" + suffix;

            // MA 설정
            int 분_P1 = 0, 분_P2 = 0, 분_배열 = 0;
            double 분_V1 = 0, 분_V2 = 0;
            int 일_P1 = 0, 일_P2 = 0, 일_배열 = 0;
            double 일_V1 = 0, 일_V2 = 0;

            // 매매 설정
            double 주문값 = 0;
            int 주문구분 = 0;
            double 수익1 = 0, 수익2 = 0;
            bool 수익선택 = false;
            int 수익구분 = 0;
            double 매도비율 = 0;
            int 매도구분 = 0;
            double 매매1 = 0, 매매2 = 0;
            int 매매구분 = 0;
            int 취소시간 = 0;
            int 취소N주문 = 0;
            int 반복횟수 = 0;
            int 지연시간 = 0;

            // =========================================================
            // 2. 변수 매핑 (Switch문으로 설정값 할당)
            // =========================================================
            switch (suffix)
            {
                case "A":
                    사용체크 = GenieConfig.CB_repeat_use_A; 잔고가동 = 잔고.가동_반복A;
                    시작시간 = GenieConfig.MT_repeat_time_start_A; 종료시간 = GenieConfig.MT_repeat_time_end_A;
                    매입비중 = GenieConfig.TB_Repeat_매입금_A;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_A; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_A;
                    매수도 = GenieConfig.CB_repeat_kind_A;
                    반복조건 = GenieConfig.combo_repeat_use_condition_A; 검색식명 = Form1.위치별검색식리스트["반복_A"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_A; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_A; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_A;
                    분_V1 = min_mma.Repeat_MAValue1_A; 분_V2 = min_mma.Repeat_MAValue2_A;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_A; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_A; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_A;
                    일_V1 = day_mma.Repeat_MAValue1_A; 일_V2 = day_mma.Repeat_MAValue2_A;

                    주문값 = GenieConfig.TB_repeat_value_A; 주문구분 = GenieConfig.combo_repeat_jumun_A;
                    수익1 = GenieConfig.TB_repeat_suik_1_A; 수익2 = GenieConfig.TB_repeat_suik_2_A; 수익선택 = GenieConfig.CB_repeat_choice_A; 수익구분 = GenieConfig.combo_repeat_suik_gubun_A;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_A; 매도구분 = GenieConfig.combo_repeat_sell_gubun_A;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_A; 매매2 = GenieConfig.TB_Repeat_maemae_2_A; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_A;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_A; 취소N주문 = GenieConfig.combo_repeat_Cancel_A;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_A; 지연시간 = GenieConfig.MTB_repeat_delay_A;
                    break;

                case "B":
                    사용체크 = GenieConfig.CB_repeat_use_B; 잔고가동 = 잔고.가동_반복B;
                    시작시간 = GenieConfig.MT_repeat_time_start_B; 종료시간 = GenieConfig.MT_repeat_time_end_B;
                    매입비중 = GenieConfig.TB_Repeat_매입금_B;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_B; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_B;
                    매수도 = GenieConfig.CB_repeat_kind_B;
                    반복조건 = GenieConfig.combo_repeat_use_condition_B; 검색식명 = Form1.위치별검색식리스트["반복_B"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_B; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_B; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_B;
                    분_V1 = min_mma.Repeat_MAValue1_B; 분_V2 = min_mma.Repeat_MAValue2_B;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_B; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_B; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_B;
                    일_V1 = day_mma.Repeat_MAValue1_B; 일_V2 = day_mma.Repeat_MAValue2_B;

                    주문값 = GenieConfig.TB_repeat_value_B; 주문구분 = GenieConfig.combo_repeat_jumun_B;
                    수익1 = GenieConfig.TB_repeat_suik_1_B; 수익2 = GenieConfig.TB_repeat_suik_2_B; 수익선택 = GenieConfig.CB_repeat_choice_B; 수익구분 = GenieConfig.combo_repeat_suik_gubun_B;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_B; 매도구분 = GenieConfig.combo_repeat_sell_gubun_B;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_B; 매매2 = GenieConfig.TB_Repeat_maemae_2_B; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_B;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_B; 취소N주문 = GenieConfig.combo_repeat_Cancel_B;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_B; 지연시간 = GenieConfig.MTB_repeat_delay_B;
                    break;

                case "C":
                    사용체크 = GenieConfig.CB_repeat_use_C; 잔고가동 = 잔고.가동_반복C;
                    시작시간 = GenieConfig.MT_repeat_time_start_C; 종료시간 = GenieConfig.MT_repeat_time_end_C;
                    매입비중 = GenieConfig.TB_Repeat_매입금_C;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_C; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_C;
                    매수도 = GenieConfig.CB_repeat_kind_C;
                    반복조건 = GenieConfig.combo_repeat_use_condition_C; 검색식명 = Form1.위치별검색식리스트["반복_C"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_C; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_C; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_C;
                    분_V1 = min_mma.Repeat_MAValue1_C; 분_V2 = min_mma.Repeat_MAValue2_C;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_C; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_C; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_C;
                    일_V1 = day_mma.Repeat_MAValue1_C; 일_V2 = day_mma.Repeat_MAValue2_C;

                    주문값 = GenieConfig.TB_repeat_value_C; 주문구분 = GenieConfig.combo_repeat_jumun_C;
                    수익1 = GenieConfig.TB_repeat_suik_1_C; 수익2 = GenieConfig.TB_repeat_suik_2_C; 수익선택 = GenieConfig.CB_repeat_choice_C; 수익구분 = GenieConfig.combo_repeat_suik_gubun_C;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_C; 매도구분 = GenieConfig.combo_repeat_sell_gubun_C;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_C; 매매2 = GenieConfig.TB_Repeat_maemae_2_C; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_C;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_C; 취소N주문 = GenieConfig.combo_repeat_Cancel_C;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_C; 지연시간 = GenieConfig.MTB_repeat_delay_C;
                    break;

                case "D":
                    사용체크 = GenieConfig.CB_repeat_use_D; 잔고가동 = 잔고.가동_반복D;
                    시작시간 = GenieConfig.MT_repeat_time_start_D; 종료시간 = GenieConfig.MT_repeat_time_end_D;
                    매입비중 = GenieConfig.TB_Repeat_매입금_D;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_D; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_D;
                    매수도 = GenieConfig.CB_repeat_kind_D;
                    반복조건 = GenieConfig.combo_repeat_use_condition_D; 검색식명 = Form1.위치별검색식리스트["반복_D"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_D; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_D; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_D;
                    분_V1 = min_mma.Repeat_MAValue1_D; 분_V2 = min_mma.Repeat_MAValue2_D;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_D; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_D; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_D;
                    일_V1 = day_mma.Repeat_MAValue1_D; 일_V2 = day_mma.Repeat_MAValue2_D;

                    주문값 = GenieConfig.TB_repeat_value_D; 주문구분 = GenieConfig.combo_repeat_jumun_D;
                    수익1 = GenieConfig.TB_repeat_suik_1_D; 수익2 = GenieConfig.TB_repeat_suik_2_D; 수익선택 = GenieConfig.CB_repeat_choice_D; 수익구분 = GenieConfig.combo_repeat_suik_gubun_D;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_D; 매도구분 = GenieConfig.combo_repeat_sell_gubun_D;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_D; 매매2 = GenieConfig.TB_Repeat_maemae_2_D; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_D;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_D; 취소N주문 = GenieConfig.combo_repeat_Cancel_D;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_D; 지연시간 = GenieConfig.MTB_repeat_delay_D;
                    break;

                case "E":
                    사용체크 = GenieConfig.CB_repeat_use_E; 잔고가동 = 잔고.가동_반복E;
                    시작시간 = GenieConfig.MT_repeat_time_start_E; 종료시간 = GenieConfig.MT_repeat_time_end_E;
                    매입비중 = GenieConfig.TB_Repeat_매입금_E;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_E; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_E;
                    매수도 = GenieConfig.CB_repeat_kind_E;
                    반복조건 = GenieConfig.combo_repeat_use_condition_E; 검색식명 = Form1.위치별검색식리스트["반복_E"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_E; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_E; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_E;
                    분_V1 = min_mma.Repeat_MAValue1_E; 분_V2 = min_mma.Repeat_MAValue2_E;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_E; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_E; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_E;
                    일_V1 = day_mma.Repeat_MAValue1_E; 일_V2 = day_mma.Repeat_MAValue2_E;

                    주문값 = GenieConfig.TB_repeat_value_E; 주문구분 = GenieConfig.combo_repeat_jumun_E;
                    수익1 = GenieConfig.TB_repeat_suik_1_E; 수익2 = GenieConfig.TB_repeat_suik_2_E; 수익선택 = GenieConfig.CB_repeat_choice_E; 수익구분 = GenieConfig.combo_repeat_suik_gubun_E;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_E; 매도구분 = GenieConfig.combo_repeat_sell_gubun_E;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_E; 매매2 = GenieConfig.TB_Repeat_maemae_2_E; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_E;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_E; 취소N주문 = GenieConfig.combo_repeat_Cancel_E;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_E; 지연시간 = GenieConfig.MTB_repeat_delay_E;
                    break;

                case "F":
                    사용체크 = GenieConfig.CB_repeat_use_F; 잔고가동 = 잔고.가동_반복F;
                    시작시간 = GenieConfig.MT_repeat_time_start_F; 종료시간 = GenieConfig.MT_repeat_time_end_F;
                    매입비중 = GenieConfig.TB_Repeat_매입금_F;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_F; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_F;
                    매수도 = GenieConfig.CB_repeat_kind_F;
                    반복조건 = GenieConfig.combo_repeat_use_condition_F; 검색식명 = Form1.위치별검색식리스트["반복_F"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_F; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_F; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_F;
                    분_V1 = min_mma.Repeat_MAValue1_F; 분_V2 = min_mma.Repeat_MAValue2_F;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_F; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_F; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_F;
                    일_V1 = day_mma.Repeat_MAValue1_F; 일_V2 = day_mma.Repeat_MAValue2_F;

                    주문값 = GenieConfig.TB_repeat_value_F; 주문구분 = GenieConfig.combo_repeat_jumun_F;
                    수익1 = GenieConfig.TB_repeat_suik_1_F; 수익2 = GenieConfig.TB_repeat_suik_2_F; 수익선택 = GenieConfig.CB_repeat_choice_F; 수익구분 = GenieConfig.combo_repeat_suik_gubun_F;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_F; 매도구분 = GenieConfig.combo_repeat_sell_gubun_F;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_F; 매매2 = GenieConfig.TB_Repeat_maemae_2_F; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_F;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_F; 취소N주문 = GenieConfig.combo_repeat_Cancel_F;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_F; 지연시간 = GenieConfig.MTB_repeat_delay_F;
                    break;

                case "G":
                    사용체크 = GenieConfig.CB_repeat_use_G; 잔고가동 = 잔고.가동_반복G;
                    시작시간 = GenieConfig.MT_repeat_time_start_G; 종료시간 = GenieConfig.MT_repeat_time_end_G;
                    매입비중 = GenieConfig.TB_Repeat_매입금_G;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_G; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_G;
                    매수도 = GenieConfig.CB_repeat_kind_G;
                    반복조건 = GenieConfig.combo_repeat_use_condition_G; 검색식명 = Form1.위치별검색식리스트["반복_G"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_G; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_G; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_G;
                    분_V1 = min_mma.Repeat_MAValue1_G; 분_V2 = min_mma.Repeat_MAValue2_G;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_G; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_G; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_G;
                    일_V1 = day_mma.Repeat_MAValue1_G; 일_V2 = day_mma.Repeat_MAValue2_G;

                    주문값 = GenieConfig.TB_repeat_value_G; 주문구분 = GenieConfig.combo_repeat_jumun_G;
                    수익1 = GenieConfig.TB_repeat_suik_1_G; 수익2 = GenieConfig.TB_repeat_suik_2_G; 수익선택 = GenieConfig.CB_repeat_choice_G; 수익구분 = GenieConfig.combo_repeat_suik_gubun_G;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_G; 매도구분 = GenieConfig.combo_repeat_sell_gubun_G;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_G; 매매2 = GenieConfig.TB_Repeat_maemae_2_G; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_G;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_G; 취소N주문 = GenieConfig.combo_repeat_Cancel_G;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_G; 지연시간 = GenieConfig.MTB_repeat_delay_G;
                    break;

                case "H":
                    사용체크 = GenieConfig.CB_repeat_use_H; 잔고가동 = 잔고.가동_반복H;
                    시작시간 = GenieConfig.MT_repeat_time_start_H; 종료시간 = GenieConfig.MT_repeat_time_end_H;
                    매입비중 = GenieConfig.TB_Repeat_매입금_H;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_H; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_H;
                    매수도 = GenieConfig.CB_repeat_kind_H;
                    반복조건 = GenieConfig.combo_repeat_use_condition_H; 검색식명 = Form1.위치별검색식리스트["반복_H"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_H; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_H; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_H;
                    분_V1 = min_mma.Repeat_MAValue1_H; 분_V2 = min_mma.Repeat_MAValue2_H;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_H; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_H; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_H;
                    일_V1 = day_mma.Repeat_MAValue1_H; 일_V2 = day_mma.Repeat_MAValue2_H;

                    주문값 = GenieConfig.TB_repeat_value_H; 주문구분 = GenieConfig.combo_repeat_jumun_H;
                    수익1 = GenieConfig.TB_repeat_suik_1_H; 수익2 = GenieConfig.TB_repeat_suik_2_H; 수익선택 = GenieConfig.CB_repeat_choice_H; 수익구분 = GenieConfig.combo_repeat_suik_gubun_H;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_H; 매도구분 = GenieConfig.combo_repeat_sell_gubun_H;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_H; 매매2 = GenieConfig.TB_Repeat_maemae_2_H; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_H;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_H; 취소N주문 = GenieConfig.combo_repeat_Cancel_H;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_H; 지연시간 = GenieConfig.MTB_repeat_delay_H;
                    break;

                case "I":
                    사용체크 = GenieConfig.CB_repeat_use_I; 잔고가동 = 잔고.가동_반복I;
                    시작시간 = GenieConfig.MT_repeat_time_start_I; 종료시간 = GenieConfig.MT_repeat_time_end_I;
                    매입비중 = GenieConfig.TB_Repeat_매입금_I;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_I; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_I;
                    매수도 = GenieConfig.CB_repeat_kind_I;
                    반복조건 = GenieConfig.combo_repeat_use_condition_I; 검색식명 = Form1.위치별검색식리스트["반복_I"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_I; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_I; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_I;
                    분_V1 = min_mma.Repeat_MAValue1_I; 분_V2 = min_mma.Repeat_MAValue2_I;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_I; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_I; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_I;
                    일_V1 = day_mma.Repeat_MAValue1_I; 일_V2 = day_mma.Repeat_MAValue2_I;

                    주문값 = GenieConfig.TB_repeat_value_I; 주문구분 = GenieConfig.combo_repeat_jumun_I;
                    수익1 = GenieConfig.TB_repeat_suik_1_I; 수익2 = GenieConfig.TB_repeat_suik_2_I; 수익선택 = GenieConfig.CB_repeat_choice_I; 수익구분 = GenieConfig.combo_repeat_suik_gubun_I;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_I; 매도구분 = GenieConfig.combo_repeat_sell_gubun_I;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_I; 매매2 = GenieConfig.TB_Repeat_maemae_2_I; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_I;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_I; 취소N주문 = GenieConfig.combo_repeat_Cancel_I;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_I; 지연시간 = GenieConfig.MTB_repeat_delay_I;
                    break;

                case "J":
                    사용체크 = GenieConfig.CB_repeat_use_J; 잔고가동 = 잔고.가동_반복J;
                    시작시간 = GenieConfig.MT_repeat_time_start_J; 종료시간 = GenieConfig.MT_repeat_time_end_J;
                    매입비중 = GenieConfig.TB_Repeat_매입금_J;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_J; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_J;
                    매수도 = GenieConfig.CB_repeat_kind_J;
                    반복조건 = GenieConfig.combo_repeat_use_condition_J; 검색식명 = Form1.위치별검색식리스트["반복_J"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_J; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_J; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_J;
                    분_V1 = min_mma.Repeat_MAValue1_J; 분_V2 = min_mma.Repeat_MAValue2_J;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_J; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_J; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_J;
                    일_V1 = day_mma.Repeat_MAValue1_J; 일_V2 = day_mma.Repeat_MAValue2_J;

                    주문값 = GenieConfig.TB_repeat_value_J; 주문구분 = GenieConfig.combo_repeat_jumun_J;
                    수익1 = GenieConfig.TB_repeat_suik_1_J; 수익2 = GenieConfig.TB_repeat_suik_2_J; 수익선택 = GenieConfig.CB_repeat_choice_J; 수익구분 = GenieConfig.combo_repeat_suik_gubun_J;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_J; 매도구분 = GenieConfig.combo_repeat_sell_gubun_J;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_J; 매매2 = GenieConfig.TB_Repeat_maemae_2_J; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_J;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_J; 취소N주문 = GenieConfig.combo_repeat_Cancel_J;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_J; 지연시간 = GenieConfig.MTB_repeat_delay_J;
                    break;

                case "K":
                    사용체크 = GenieConfig.CB_repeat_use_K; 잔고가동 = 잔고.가동_반복K;
                    시작시간 = GenieConfig.MT_repeat_time_start_K; 종료시간 = GenieConfig.MT_repeat_time_end_K;
                    매입비중 = GenieConfig.TB_Repeat_매입금_K;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_K; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_K;
                    매수도 = GenieConfig.CB_repeat_kind_K;
                    반복조건 = GenieConfig.combo_repeat_use_condition_K; 검색식명 = Form1.위치별검색식리스트["반복_K"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_K; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_K; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_K;
                    분_V1 = min_mma.Repeat_MAValue1_K; 분_V2 = min_mma.Repeat_MAValue2_K;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_K; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_K; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_K;
                    일_V1 = day_mma.Repeat_MAValue1_K; 일_V2 = day_mma.Repeat_MAValue2_K;

                    주문값 = GenieConfig.TB_repeat_value_K; 주문구분 = GenieConfig.combo_repeat_jumun_K;
                    수익1 = GenieConfig.TB_repeat_suik_1_K; 수익2 = GenieConfig.TB_repeat_suik_2_K; 수익선택 = GenieConfig.CB_repeat_choice_K; 수익구분 = GenieConfig.combo_repeat_suik_gubun_K;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_K; 매도구분 = GenieConfig.combo_repeat_sell_gubun_K;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_K; 매매2 = GenieConfig.TB_Repeat_maemae_2_K; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_K;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_K; 취소N주문 = GenieConfig.combo_repeat_Cancel_K;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_K; 지연시간 = GenieConfig.MTB_repeat_delay_K;
                    break;

                case "L":
                    사용체크 = GenieConfig.CB_repeat_use_L; 잔고가동 = 잔고.가동_반복L;
                    시작시간 = GenieConfig.MT_repeat_time_start_L; 종료시간 = GenieConfig.MT_repeat_time_end_L;
                    매입비중 = GenieConfig.TB_Repeat_매입금_L;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_L; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_L;
                    매수도 = GenieConfig.CB_repeat_kind_L;
                    반복조건 = GenieConfig.combo_repeat_use_condition_L; 검색식명 = Form1.위치별검색식리스트["반복_L"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_L; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_L; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_L;
                    분_V1 = min_mma.Repeat_MAValue1_L; 분_V2 = min_mma.Repeat_MAValue2_L;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_L; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_L; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_L;
                    일_V1 = day_mma.Repeat_MAValue1_L; 일_V2 = day_mma.Repeat_MAValue2_L;

                    주문값 = GenieConfig.TB_repeat_value_L; 주문구분 = GenieConfig.combo_repeat_jumun_L;
                    수익1 = GenieConfig.TB_repeat_suik_1_L; 수익2 = GenieConfig.TB_repeat_suik_2_L; 수익선택 = GenieConfig.CB_repeat_choice_L; 수익구분 = GenieConfig.combo_repeat_suik_gubun_L;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_L; 매도구분 = GenieConfig.combo_repeat_sell_gubun_L;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_L; 매매2 = GenieConfig.TB_Repeat_maemae_2_L; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_L;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_L; 취소N주문 = GenieConfig.combo_repeat_Cancel_L;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_L; 지연시간 = GenieConfig.MTB_repeat_delay_L;
                    break;

                case "M":
                    사용체크 = GenieConfig.CB_repeat_use_M; 잔고가동 = 잔고.가동_반복M;
                    시작시간 = GenieConfig.MT_repeat_time_start_M; 종료시간 = GenieConfig.MT_repeat_time_end_M;
                    매입비중 = GenieConfig.TB_Repeat_매입금_M;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_M; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_M;
                    매수도 = GenieConfig.CB_repeat_kind_M;
                    반복조건 = GenieConfig.combo_repeat_use_condition_M; 검색식명 = Form1.위치별검색식리스트["반복_M"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_M; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_M; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_M;
                    분_V1 = min_mma.Repeat_MAValue1_M; 분_V2 = min_mma.Repeat_MAValue2_M;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_M; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_M; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_M;
                    일_V1 = day_mma.Repeat_MAValue1_M; 일_V2 = day_mma.Repeat_MAValue2_M;

                    주문값 = GenieConfig.TB_repeat_value_M; 주문구분 = GenieConfig.combo_repeat_jumun_M;
                    수익1 = GenieConfig.TB_repeat_suik_1_M; 수익2 = GenieConfig.TB_repeat_suik_2_M; 수익선택 = GenieConfig.CB_repeat_choice_M; 수익구분 = GenieConfig.combo_repeat_suik_gubun_M;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_M; 매도구분 = GenieConfig.combo_repeat_sell_gubun_M;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_M; 매매2 = GenieConfig.TB_Repeat_maemae_2_M; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_M;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_M; 취소N주문 = GenieConfig.combo_repeat_Cancel_M;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_M; 지연시간 = GenieConfig.MTB_repeat_delay_M;
                    break;

                case "N":
                    사용체크 = GenieConfig.CB_repeat_use_N; 잔고가동 = 잔고.가동_반복N;
                    시작시간 = GenieConfig.MT_repeat_time_start_N; 종료시간 = GenieConfig.MT_repeat_time_end_N;
                    매입비중 = GenieConfig.TB_Repeat_매입금_N;
                    누적거래량 = GenieConfig.TB_repeat_누적거래량_N; 누적거래대금 = GenieConfig.TB_repeat_누적거래대금_N;
                    매수도 = GenieConfig.CB_repeat_kind_N;
                    반복조건 = GenieConfig.combo_repeat_use_condition_N; 검색식명 = Form1.위치별검색식리스트["반복_N"].이름;

                    분_P1 = GenieConfig.CBB_repeat_MinMAPeriod1_N; 분_P2 = GenieConfig.CBB_repeat_MinMAPeriod2_N; 분_배열 = GenieConfig.CBB_repeat_MinMAPeriod1_배열_N;
                    분_V1 = min_mma.Repeat_MAValue1_N; 분_V2 = min_mma.Repeat_MAValue2_N;
                    일_P1 = GenieConfig.CBB_repeat_DayMAPeriod1_N; 일_P2 = GenieConfig.CBB_repeat_DayMAPeriod2_N; 일_배열 = GenieConfig.CBB_repeat_DayMAPeriod_배열_N;
                    일_V1 = day_mma.Repeat_MAValue1_N; 일_V2 = day_mma.Repeat_MAValue2_N;

                    주문값 = GenieConfig.TB_repeat_value_N; 주문구분 = GenieConfig.combo_repeat_jumun_N;
                    수익1 = GenieConfig.TB_repeat_suik_1_N; 수익2 = GenieConfig.TB_repeat_suik_2_N; 수익선택 = GenieConfig.CB_repeat_choice_N; 수익구분 = GenieConfig.combo_repeat_suik_gubun_N;
                    매도비율 = GenieConfig.TB_repeat_sell_ratio_N; 매도구분 = GenieConfig.combo_repeat_sell_gubun_N;
                    매매1 = GenieConfig.TB_Repeat_maemae_1_N; 매매2 = GenieConfig.TB_Repeat_maemae_2_N; 매매구분 = GenieConfig.combo_Repeat_maemae_gubun_N;
                    취소시간 = GenieConfig.MTB_repeat_Cancel_time_N; 취소N주문 = GenieConfig.combo_repeat_Cancel_N;
                    반복횟수 = GenieConfig.MTB_repeat_repeat_N; 지연시간 = GenieConfig.MTB_repeat_delay_N;
                    break;
            }

            // =========================================================
            // 3. 로직 수행 (할당된 변수로 통합 로직 실행)
            // =========================================================
            // 3-1. 기본 가동 체크
            if (!사용체크) return;
            if (!잔고가동) return;

            // 3-2. 시간 체크
            if (!Method.RunTime(시작시간, 종료시간)) return;

            // 3 - 3.자금 및 거래량 체크
            long 목표매입금 = (long)(매입비중 / 100.0 * GenieConfig.MT_buying_standard);
            if (잔고.매입금액 + 잔고.신용_매입금액 <= 목표매입금) return;
            if (market_Item.누적거래량 <= 누적거래량) return;
            if (market_Item.누적거래대금 <= 누적거래대금) return;
            // 3-4. 추매 가능 및 중복 체크
            if (!Method.추매가능_Check(잔고, 매수도, true)) return;

            string 검색식 = (반복조건 == 0) ? "" : 검색식명;
            if (!Method.매매진입_가능여부(잔고.종목코드, $"{위치} [{검색식}]")) return;
            // 3-5. 이평선 체크
            if (!MA.Get_이평(잔고, 분_P1, 분_P2, 분_배열, 분_V1, 분_V2)) return;
            if (!MA.Get_이평(잔고, 일_P1, 일_P2, 일_배열, 일_V1, 일_V2)) return;

            // 3-6. 매매 실행 (Catch List 연동)
            string 익절그룹 = GET.익절그룹(위치);
            string 종목코드 = 잔고.종목코드; // itemCode 변수명 한글 변경

            if (반복조건 > 0) // 검색식 사용 (지연 조건)
            {
                string 검색키 = 위치 + 종목코드; // key 변수명 한글 변경
                if (Form1.Catch_Stock_List.TryGetValue(검색키, out Catch_stock 아이템)) // item 변수명 한글 변경
                {
                    if (지연시간 <= 아이템.timer)
                    {
                        // 비동기 메서드 호출을 위해 await 사용 및 결과 변수에 저장
                        bool 매매결과 = await Tab_Repeat.반복매매(잔고, 매수도, 주문값, 주문구분, 수익1, 수익2, 수익선택, 수익구분,
                            매도비율, 매도구분, 매매1, 매매2, 매매구분, 익절그룹, 취소시간, 취소N주문, 반복횟수, 위치, 검색식명);

                        if (매매결과)
                        {
                            Form1.Catch_Stock_List.TryRemove(검색키, out _);
                        }
                    }
                }
            }
            else // 즉시 실행
            {
                // 비동기 메서드 호출을 위해 await 사용
                await Tab_Repeat.반복매매(잔고, 매수도, 주문값, 주문구분, 수익1, 수익2, 수익선택, 수익구분,
                    매도비율, 매도구분, 매매1, 매매2, 매매구분, 익절그룹, 취소시간, 취소N주문, 반복횟수, 위치, "");
            }
        }

        public static async Task<bool> 반복매매(Stockbalance 잔고, bool 매수도, double 주문값, int 매수매도, double suik_low, double suik_height, bool suik_choice, int suik_gubun, double 비중,
                                    int 비중단위, double maemae_low, double maemae_height, int maemae_gubun, string 매매그룹, int 취소시간, int 취소N주문, int 반복시간, string location, string 검색식명)
        {
            bool result = false;

            if (매수도) // 매수
            {
                if (Helper.NXT_매수매도_금지(매수도)) return result;
            }
            else // 매도
            {
                if (Helper.NXT_매수매도_금지(매수도))
                {
                    if (잔고.수익률 < 0.3) return result;
                }
            }

            if (매매그룹.Contains(GET.그룹변환(잔고.매매그룹)) && Tab_InterestGroup.관심그룹확인(location, 잔고.종목코드))
            {
                bool 단위_기준금 = GenieConfig.CB_Repeat_기준금;

                if (location.Contains("리밸_"))
                    단위_기준금 = GenieConfig.CB_rebalance_기준금;

                bool 반복매매Run = true;

                foreach (var item in Form1.JumunItem_List.Values)
                {
                    if (item.종목코드 == 잔고.종목코드 && item.Pos == "신규매수")
                    {
                        반복매매Run = false;
                        break; // 찾았으면 더 볼 필요 없이 즉시 탈출 (CPU 절약)
                    }
                }

                if (반복매매Run)
                {
                    if (Method.매입비추매제한(잔고.보유비중))
                    {
                        //0 수익률(%)
                        //1 평가손익금
                        //2 예상손익금
                        //3 등락율(%)
                        //4 수익률 + 예상
                        //5 하기준(기준 + 수익률 이하)
                        //6 상기준(기준 + 수익률 이상)
                        //7 하최종(최종매입가 이하)

                        if (!suik_choice) // →
                        {
                            if (Method.수익범위(매수도, 단위_기준금, 잔고, suik_low, suik_height, suik_gubun, location))
                            {
                              await  매매진행();
                            }
                        }
                        else // ⇒
                        {
                            switch (location)
                            {
                                case "반복_A": 잔고.반복A = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복A, "A", 매수도); if (잔고.반복A.Equals("X")) {await 매매진행(); } break;
                                case "반복_B": 잔고.반복B = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복B, "B", 매수도); if (잔고.반복B.Equals("X")) {await 매매진행(); } break;
                                case "반복_C": 잔고.반복C = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복C, "C", 매수도); if (잔고.반복C.Equals("X")) {await 매매진행(); } break;
                                case "반복_D": 잔고.반복D = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복D, "D", 매수도); if (잔고.반복D.Equals("X")) {await 매매진행(); } break;
                                case "반복_E": 잔고.반복E = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복E, "E", 매수도); if (잔고.반복E.Equals("X")) {await 매매진행(); } break;
                                case "반복_F": 잔고.반복F = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복F, "F", 매수도); if (잔고.반복F.Equals("X")) {await 매매진행(); } break;
                                case "반복_G": 잔고.반복G = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복G, "G", 매수도); if (잔고.반복G.Equals("X")) {await 매매진행(); } break;
                                case "반복_H": 잔고.반복H = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복H, "H", 매수도); if (잔고.반복H.Equals("X")) {await 매매진행(); } break;
                                case "반복_I": 잔고.반복I = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복I, "I", 매수도); if (잔고.반복I.Equals("X")) {await 매매진행(); } break;
                                case "반복_J": 잔고.반복J = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복J, "J", 매수도); if (잔고.반복J.Equals("X")) {await 매매진행(); } break;
                                case "반복_K": 잔고.반복K = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복K, "K", 매수도); if (잔고.반복K.Equals("X")) {await 매매진행(); } break;
                                case "반복_L": 잔고.반복L = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복L, "L", 매수도); if (잔고.반복L.Equals("X")) {await 매매진행(); } break;
                                case "반복_M": 잔고.반복M = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복M, "M", 매수도); if (잔고.반복M.Equals("X")) {await 매매진행(); } break;
                                case "반복_N": 잔고.반복N = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.반복N, "N", 매수도); if (잔고.반복N.Equals("X")) {await 매매진행(); } break;
                                                                                                                                                                                                                                  
                                case "리밸_A": 잔고.리벨A = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨A, "A", 매수도); if (잔고.리벨A.Equals("X")) {await 매매진행(); } break;
                                case "리밸_B": 잔고.리벨B = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨B, "B", 매수도); if (잔고.리벨B.Equals("X")) {await 매매진행(); } break;
                                case "리밸_C": 잔고.리벨C = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨C, "C", 매수도); if (잔고.리벨C.Equals("X")) {await 매매진행(); } break;
                                case "리밸_D": 잔고.리벨D = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨D, "D", 매수도); if (잔고.리벨D.Equals("X")) {await 매매진행(); } break;
                                case "리밸_E": 잔고.리벨E = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨E, "E", 매수도); if (잔고.리벨E.Equals("X")) {await 매매진행(); } break;
                                case "리밸_F": 잔고.리벨F = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨F, "F", 매수도); if (잔고.리벨F.Equals("X")) {await 매매진행(); } break;
                                case "리밸_G": 잔고.리벨G = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.리벨G, "G", 매수도); if (잔고.리벨G.Equals("X")) { await 매매진행(); } break;
                            }
                        }
                    }

                    async Task 매매진행()
                    {
                        if (Method.매매범위(maemae_low, maemae_height, maemae_gubun, 잔고.매입금액 + 잔고.신용_매입금액))
                        {
                            if (GET.총주문가능수량(잔고) > 0)
                            {
                                Market_Item market_Item = Form1.Market_Item_List[잔고.종목코드];

                                int 주문가 = Method.Order_price(주문값, 매수매도, market_Item.종목코드, 잔고.현재가); // 주문가격
                                int 예_주문가 = 주문가;

                                if (매수매도 == 0) // 시장가
                                    예_주문가 = 잔고.현재가;

                                int 주문수량 = Method.주문수량계산(잔고, 예_주문가, 비중, 비중단위);

                                if (주문수량 < 1)
                                {
                                    주문수량 = 1;
                                }

                                if (매수도) // 추가매수
                                {
                                    if (!Form1.추가매수정지 && form1.매수_ON) //&& Form1.form1.RB_buy_run.Checked)
                                    {
                                        if (잔고.추매딜레이 == 0 && !잔고.추매정지)
                                        {
                                            if (Method.매입비추매제한(잔고.보유비중))
                                            {
                                                if (Jisu_linkage.업종별지수연동(location, market_Item))
                                                {
                                                    if (GenieConfig.CB_계좌매입비_매수제한) // 매입비
                                                    {
                                                        if (!Method.매입비매수제한(GenieConfig.CBB_계좌매입비_제한선택).Contains("추매"))
                                                        {
                                                          await  주문전달();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        await 주문전달();
                                                    }
                                                }
                                            }

                                            async Task 주문전달()
                                            {
                                                if (잔고.매수제한)
                                                {
                                                    if (매수매도 < 4)
                                                    {
                                                        if (ExecuteTrade.잔고주문_오더(잔고, location + " [" + 검색식명 + "]", 1, 비중, 비중단위, 주문값, 매수매도, 취소시간, 취소N주문, 반복시간, "", location, suik_gubun, false, 0))
                                                        {
                                                            반복가동();
                                                            result = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        주문수량 = Method.최대매수금_주문수량계산(잔고, 주문수량);

                                                        if (주문수량 > 0)
                                                        {
                                                            if (await Tab_AccountManagement.분할주문(location, 1, 매수매도, 잔고.종목코드, 잔고.종목명, 주문수량, 잔고.현재가, location + " [" + 검색식명 + "]", 취소시간))
                                                            {
                                                                반복가동();
                                                                result = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Log.에러기록("[매수 제한]최대 매수금 초과 종목: " + 잔고.종목명 + " 주문가격: " + 예_주문가 + " 주문수량: " + 주문수량 + " 주문금액: " + (예_주문가 * 주문수량));
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else // 매도 
                                {
                                    if (Form1.form1.RB_sell_run.Checked && !잔고.매도정지)
                                    {
                                        if (GET.총주문가능수량(잔고) >= 주문수량)
                                        {
                                            if (매수매도 < 4)
                                            {
                                                if (ExecuteTrade.잔고주문_오더(잔고, location + " [" + 검색식명 + "]", 2, 비중, 비중단위, 주문값, 매수매도, 취소시간, 취소N주문, 반복시간, "", location, 0, false, 0))
                                                {
                                                    반복가동();
                                                    result = true;
                                                }
                                            }
                                            else
                                            {
                                                if (await Tab_AccountManagement.분할주문(location, 2, 매수매도, 잔고.종목코드, 잔고.종목명, 주문수량, 잔고.현재가, location + " [" + 검색식명 + "]", 취소시간))
                                                {
                                                    반복가동();
                                                    result = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        void 반복가동()
                        {
                            // 1. 공통 변수 선언 (설정 시간을 담을 변수)
                            int targetTime = 0;

                            // 2. 설정값 가져오기 및 플래그 초기화 (객체 생성 코드 제거됨)
                            switch (location)
                            {
                                // --- 반복 매매 (A ~ N) ---
                                case "반복_A": targetTime = GenieConfig.MT_repeat_repeat_time_A; Get.Repeat_time_A = targetTime; 잔고.가동_반복A = false; break;
                                case "반복_B": targetTime = GenieConfig.MT_repeat_repeat_time_B; Get.Repeat_time_B = targetTime; 잔고.가동_반복B = false; break;
                                case "반복_C": targetTime = GenieConfig.MT_repeat_repeat_time_C; Get.Repeat_time_C = targetTime; 잔고.가동_반복C = false; break;
                                case "반복_D": targetTime = GenieConfig.MT_repeat_repeat_time_D; Get.Repeat_time_D = targetTime; 잔고.가동_반복D = false; break;
                                case "반복_E": targetTime = GenieConfig.MT_repeat_repeat_time_E; Get.Repeat_time_E = targetTime; 잔고.가동_반복E = false; break;
                                case "반복_F": targetTime = GenieConfig.MT_repeat_repeat_time_F; Get.Repeat_time_F = targetTime; 잔고.가동_반복F = false; break;
                                case "반복_G": targetTime = GenieConfig.MT_repeat_repeat_time_G; Get.Repeat_time_G = targetTime; 잔고.가동_반복G = false; break;
                                case "반복_H": targetTime = GenieConfig.MT_repeat_repeat_time_H; Get.Repeat_time_H = targetTime; 잔고.가동_반복H = false; break;
                                case "반복_I": targetTime = GenieConfig.MT_repeat_repeat_time_I; Get.Repeat_time_I = targetTime; 잔고.가동_반복I = false; break;
                                case "반복_J": targetTime = GenieConfig.MT_repeat_repeat_time_J; Get.Repeat_time_J = targetTime; 잔고.가동_반복J = false; break;
                                case "반복_K": targetTime = GenieConfig.MT_repeat_repeat_time_K; Get.Repeat_time_K = targetTime; 잔고.가동_반복K = false; break;
                                case "반복_L": targetTime = GenieConfig.MT_repeat_repeat_time_L; Get.Repeat_time_L = targetTime; 잔고.가동_반복L = false; break;
                                case "반복_M": targetTime = GenieConfig.MT_repeat_repeat_time_M; Get.Repeat_time_M = targetTime; 잔고.가동_반복M = false; break;
                                case "반복_N": targetTime = GenieConfig.MT_repeat_repeat_time_N; Get.Repeat_time_N = targetTime; 잔고.가동_반복N = false; break;

                                // --- 리밸런싱 (A ~ G) ---
                                case "리밸_A": targetTime = GenieConfig.MT_rebalance_repeat_time_A; Get.TT_rebalance_time_A = targetTime; 잔고.가동_리밸A = false; break;
                                case "리밸_B": targetTime = GenieConfig.MT_rebalance_repeat_time_B; Get.TT_rebalance_time_B = targetTime; 잔고.가동_리밸B = false; break;
                                case "리밸_C": targetTime = GenieConfig.MT_rebalance_repeat_time_C; Get.TT_rebalance_time_C = targetTime; 잔고.가동_리밸C = false; break;
                                case "리밸_D": targetTime = GenieConfig.MT_rebalance_repeat_time_D; Get.TT_rebalance_time_D = targetTime; 잔고.가동_리밸D = false; break;
                                case "리밸_E": targetTime = GenieConfig.MT_rebalance_repeat_time_E; Get.TT_rebalance_time_E = targetTime; 잔고.가동_리밸E = false; break;
                                case "리밸_F": targetTime = GenieConfig.MT_rebalance_repeat_time_F; Get.TT_rebalance_time_F = targetTime; 잔고.가동_리밸F = false; break;
                                case "리밸_G": targetTime = GenieConfig.MT_rebalance_repeat_time_G; Get.TT_rebalance_time_G = targetTime; 잔고.가동_리밸G = false; break;
                            }

                            // ----------------------------------------------------------------------
                            // [최적화 완료] Queue 방식 객체 풀링 + 비동기 타이머 실행
                            // ----------------------------------------------------------------------

                            // 1. 대기열(Pool)에서 놀고 있는 객체를 꺼냅니다.
                            if (Form1.Trading_Pool.TryDequeue(out Trading_item item))
                            {
                                // [재사용] 1단계에서 만든 Initialize 함수로 한 번에 초기화
                                item.Initialize(잔고.종목코드, location, targetTime);
                            }
                            else
                            {
                                // [신규 생성] 대기열이 텅 비었음 -> 새로 만듦
                                // (생성자 내부에서 Initialize가 호출되도록 1단계에서 수정했습니다)
                                item = new Trading_item(잔고.종목코드, location, targetTime);
                            }

                            // [추가] 작업 시작하니까 '작업 중 명부'에 등록
                            Form1.Active_List.TryAdd(item, 0);

                            // ----------------------------------------------------------------------
                            // [추가된 부분] 2. 비동기 타이머 스레드 실행 (Fire-and-Forget)
                            // ----------------------------------------------------------------------
                            // 이제 반복문 루프 없이, 이 코드가 실행되면 알아서 시간이 흐르고 로직이 실행됩니다.
                            _ = Task.Run(async () =>
                            {
                                // 입장 대기 (자리가 날 때까지 기다림)
                                await _semaphore.WaitAsync();
                                try
                                {
                                    await Method.타이머_작업_실행(item);
                                }
                                finally
                                {
                                    // 퇴장 (자리 반납)
                                    _semaphore.Release();
                                }
                            });
                        }

                    }
                }
            }
            return result;
        }






    }
}
