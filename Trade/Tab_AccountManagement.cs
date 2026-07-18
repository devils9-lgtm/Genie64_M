using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static 지니64.초기화;

namespace 지니64
{
    class Tab_AccountManagement : Form1
    {
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ///////////////            계좌 리밸런싱 관리              ////////////////

        public static void Rebalancing_condition(string ST_ID, string itemcode, string condition)
        {
            bool ID = false;

            string[] assign = { "A", "B", "C", "D", "E", "F", "G" };

            int[] settings = new int[] {
                 GenieConfig.combo_rebalance_use_condition_A,
                 GenieConfig.combo_rebalance_use_condition_B,
                 GenieConfig.combo_rebalance_use_condition_C,
                 GenieConfig.combo_rebalance_use_condition_D,
                 GenieConfig.combo_rebalance_use_condition_E,
                 GenieConfig.combo_rebalance_use_condition_F,
                 GenieConfig.combo_rebalance_use_condition_G
             };

            for (int i = 0; i < settings.Length; i++)
            {
                int settingValue = settings[i];
                string where = "리밸_" + assign[i];

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

            void in_out(string where)
            {
                if (condition.Equals(Form1.위치별검색식리스트[where].이름))
                {
                    // where 값은 "Rebal_A", "Rebal_B" 등 설정 값과 연동되어 있어야 합니다.
                    string key = where + itemcode;

                    if (ID) // 추가 조건 (종목이 리스트에 진입할 때)
                    {
                        // [최적화] TryAdd를 사용하여 키가 없으면 추가하고, 있으면 무시 (스레드 안전, O(1))
                        // Catch_stock 객체는 itemcode와 location 정보가 필요할 것입니다.
                        Catch_Stock_List.TryAdd(key, new Catch_stock(itemcode, 0));

                        // 참고: Catch_stock 생성자에 location 정보가 필요하다면, 
                        // new Catch_stock(itemcode, 0, where) 형태로 변경해야 합니다.
                    }
                    else // 제거 조건 (종목이 리스트에서 나갈 때)
                    {
                        // [최적화] TryRemove를 사용하여 키가 있으면 안전하게 제거 (O(1))
                        // 불필요한 ContainsKey 체크와 Find/Remove 연산을 모두 대체합니다.
                        Catch_Stock_List.TryRemove(key, out Catch_stock removedItem);
                    }
                }
            }
        }

        private static bool 감시리스트체크(string Code, string 위치, bool 매도체크) // 감시주문이 매도 체결 되었는지 체크후 매수 주문합니다.
        {
            // 매도체크가 false이거나 감시주문 리스트가 비어있으면 바로 매수 가능(true)
            if (!매도체크 || Form1.감시주문_List.IsEmpty)
            {
                return true;
            }

            // [최적화] Dictionary의 .Values를 사용하여 값만 순회하며, LINQ의 Any()로 조건을 확인합니다.
            // Any()는 조건에 맞는 첫 항목을 찾으면 즉시 검색을 중단하므로 효율적입니다.

            // 조건: 종목코드가 일치하고 (종목코드 == Code) AND 검색식에 위치 정보가 포함된 주문이 있는지 확인
            bool isOrderExist = Form1.감시주문_List.Values.Any(o =>
                o.종목코드.Equals(Code) &&
                o.검색식.Contains(위치));

            // 감시 주문이 발견되면 (주문이 진행 중이면) 매수 불가(false)
            return !isOrderExist;
        }


        public static async Task Rebalancing_USE(Stockbalance 잔고)
        {
            // 1. 공통 유효성 검사 (빠른 탈출)
            if (Form1.재시작 || !잔고.매매가능 || GET.총주문가능수량(잔고) <= 0) return;

            // 2. 공통 데이터 캐싱 (한 번만 불러와서 성능 향상)
            string 종목코드 = 잔고.종목코드;

            // 딕셔너리 키 존재 여부 확인 후 안전하게 접근 (에러 방지)
            if (!Form1.Market_Item_List.ContainsKey(종목코드)) return;
            Market_Item market_Item = Form1.Market_Item_List[종목코드];

            // 이평선 데이터
            MAPeriod min_mma = Form1.Min_ma_list.ContainsKey(종목코드) ? Form1.Min_ma_list[종목코드] : null;
            MAPeriod day_mma = Form1.Day_ma_list.ContainsKey(종목코드) ? Form1.Day_ma_list[종목코드] : null;

            if (min_mma == null || day_mma == null) return;

            // 3. 그룹별 순차 실행 (A -> B -> ... -> G)
            // 배열 할당을 최소화하기 위해 직접 호출하거나, 정적 배열을 사용하는 것이 좋음
            await 리밸런싱_그룹_실행(잔고, market_Item, min_mma, day_mma, "A");
            await 리밸런싱_그룹_실행(잔고, market_Item, min_mma, day_mma, "B");
            await 리밸런싱_그룹_실행(잔고, market_Item, min_mma, day_mma, "C");
            await 리밸런싱_그룹_실행(잔고, market_Item, min_mma, day_mma, "D");
            await 리밸런싱_그룹_실행(잔고, market_Item, min_mma, day_mma, "E");
            await 리밸런싱_그룹_실행(잔고, market_Item, min_mma, day_mma, "F");
            await 리밸런싱_그룹_실행(잔고, market_Item, min_mma, day_mma, "G");
        }

        // [핵심 로직] 그룹명(suffix)에 따라 설정을 불러오고 실행
        private static async Task 리밸런싱_그룹_실행(Stockbalance 잔고, Market_Item market_Item, MAPeriod min_mma, MAPeriod day_mma, string suffix)
        {
            // =========================================================
            // 1. 변수 선언 (기본값 초기화)
            // =========================================================
            bool 사용체크 = false;
            bool 잔고가동 = false;
            int 시작시간 = 0, 종료시간 = 0;
            double 매입비중 = 0;
            long 누적거래량 = 0, 누적거래대금 = 0;

            int 검색식사용 = 0;
            string 검색식명 = "";
            string 위치 = "리밸_" + suffix;
            bool 매도감시 = false;

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
            int 지연시간 = 0;

            // =========================================================
            // 2. 변수 매핑 (Switch문으로 설정값 할당)
            // =========================================================
            switch (suffix)
            {
                case "A":
                    사용체크 = GenieConfig.CB_rebalance_A; 잔고가동 = 잔고.가동_리밸A;
                    시작시간 = GenieConfig.MT_rebalance_starttime_A; 종료시간 = GenieConfig.MT_rebalance_stoptime_A;
                    매입비중 = GenieConfig.TB_Rebalance_매입금_A;
                    누적거래량 = GenieConfig.TB_rebalance_누적거래량_A; 누적거래대금 = GenieConfig.TB_rebalance_누적거래대금_A;
                    검색식사용 = GenieConfig.combo_rebalance_use_condition_A; 검색식명 = Form1.위치별검색식리스트["리밸_A"].이름;
                    매도감시 = GenieConfig.CB_rebalance_매도체크_A;

                    분_P1 = GenieConfig.CBB_rebalance_MinMAPeriod1_A; 분_P2 = GenieConfig.CBB_rebalance_MinMAPeriod2_A; 분_배열 = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_A;
                    분_V1 = min_mma.Rebalance_MAValue1_A; 분_V2 = min_mma.Rebalance_MAValue2_A;
                    일_P1 = GenieConfig.CBB_rebalance_DayMAPeriod1_A; 일_P2 = GenieConfig.CBB_rebalance_DayMAPeriod2_A; 일_배열 = GenieConfig.CBB_rebalance_DayMAPeriod_배열_A;
                    일_V1 = day_mma.Rebalance_MAValue1_A; 일_V2 = day_mma.Rebalance_MAValue2_A;

                    주문값 = GenieConfig.TB_rebalance_value_A; 주문구분 = GenieConfig.combo_rebalance_jumun_A;
                    수익1 = GenieConfig.TB_rebalance_suik_1_A; 수익2 = GenieConfig.TB_rebalance_suik_2_A; 수익선택 = GenieConfig.CB_rebalance_choice_A; 수익구분 = GenieConfig.combo_rebalance_suik_gubun_A;
                    매도비율 = GenieConfig.TB_rebalance_sell_ratio_A; 매도구분 = GenieConfig.combo_rebalance_sell_gubun_A;
                    매매1 = GenieConfig.TB_rebalance_maemae_1_A; 매매2 = GenieConfig.TB_rebalance_maemae_2_A; 매매구분 = GenieConfig.combo_rebalance_maemae_gubun_A;
                    취소시간 = GenieConfig.MTB_rebalance_Cancel_time_A; 지연시간 = GenieConfig.MTB_rebalance_delay_A;
                    break;

                case "B":
                    사용체크 = GenieConfig.CB_rebalance_B; 잔고가동 = 잔고.가동_리밸B;
                    시작시간 = GenieConfig.MT_rebalance_starttime_B; 종료시간 = GenieConfig.MT_rebalance_stoptime_B;
                    매입비중 = GenieConfig.TB_Rebalance_매입금_B;
                    누적거래량 = GenieConfig.TB_rebalance_누적거래량_B; 누적거래대금 = GenieConfig.TB_rebalance_누적거래대금_B;
                    검색식사용 = GenieConfig.combo_rebalance_use_condition_B; 검색식명 = Form1.위치별검색식리스트["리밸_B"].이름;
                    매도감시 = GenieConfig.CB_rebalance_매도체크_B;

                    분_P1 = GenieConfig.CBB_rebalance_MinMAPeriod1_B; 분_P2 = GenieConfig.CBB_rebalance_MinMAPeriod2_B; 분_배열 = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_B;
                    분_V1 = min_mma.Rebalance_MAValue1_B; 분_V2 = min_mma.Rebalance_MAValue2_B;
                    일_P1 = GenieConfig.CBB_rebalance_DayMAPeriod1_B; 일_P2 = GenieConfig.CBB_rebalance_DayMAPeriod2_B; 일_배열 = GenieConfig.CBB_rebalance_DayMAPeriod_배열_B;
                    일_V1 = day_mma.Rebalance_MAValue1_B; 일_V2 = day_mma.Rebalance_MAValue2_B;

                    주문값 = GenieConfig.TB_rebalance_value_B; 주문구분 = GenieConfig.combo_rebalance_jumun_B;
                    수익1 = GenieConfig.TB_rebalance_suik_1_B; 수익2 = GenieConfig.TB_rebalance_suik_2_B; 수익선택 = GenieConfig.CB_rebalance_choice_B; 수익구분 = GenieConfig.combo_rebalance_suik_gubun_B;
                    매도비율 = GenieConfig.TB_rebalance_sell_ratio_B; 매도구분 = GenieConfig.combo_rebalance_sell_gubun_B;
                    매매1 = GenieConfig.TB_rebalance_maemae_1_B; 매매2 = GenieConfig.TB_rebalance_maemae_2_B; 매매구분 = GenieConfig.combo_rebalance_maemae_gubun_B;
                    취소시간 = GenieConfig.MTB_rebalance_Cancel_time_B; 지연시간 = GenieConfig.MTB_rebalance_delay_B;
                    break;

                case "C":
                    사용체크 = GenieConfig.CB_rebalance_C; 잔고가동 = 잔고.가동_리밸C;
                    시작시간 = GenieConfig.MT_rebalance_starttime_C; 종료시간 = GenieConfig.MT_rebalance_stoptime_C;
                    매입비중 = GenieConfig.TB_Rebalance_매입금_C;
                    누적거래량 = GenieConfig.TB_rebalance_누적거래량_C; 누적거래대금 = GenieConfig.TB_rebalance_누적거래대금_C;
                    검색식사용 = GenieConfig.combo_rebalance_use_condition_C; 검색식명 = Form1.위치별검색식리스트["리밸_C"].이름;
                    매도감시 = GenieConfig.CB_rebalance_매도체크_C;

                    분_P1 = GenieConfig.CBB_rebalance_MinMAPeriod1_C; 분_P2 = GenieConfig.CBB_rebalance_MinMAPeriod2_C; 분_배열 = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_C;
                    분_V1 = min_mma.Rebalance_MAValue1_C; 분_V2 = min_mma.Rebalance_MAValue2_C;
                    일_P1 = GenieConfig.CBB_rebalance_DayMAPeriod1_C; 일_P2 = GenieConfig.CBB_rebalance_DayMAPeriod2_C; 일_배열 = GenieConfig.CBB_rebalance_DayMAPeriod_배열_C;
                    일_V1 = day_mma.Rebalance_MAValue1_C; 일_V2 = day_mma.Rebalance_MAValue2_C;

                    주문값 = GenieConfig.TB_rebalance_value_C; 주문구분 = GenieConfig.combo_rebalance_jumun_C;
                    수익1 = GenieConfig.TB_rebalance_suik_1_C; 수익2 = GenieConfig.TB_rebalance_suik_2_C; 수익선택 = GenieConfig.CB_rebalance_choice_C; 수익구분 = GenieConfig.combo_rebalance_suik_gubun_C;
                    매도비율 = GenieConfig.TB_rebalance_sell_ratio_C; 매도구분 = GenieConfig.combo_rebalance_sell_gubun_C;
                    매매1 = GenieConfig.TB_rebalance_maemae_1_C; 매매2 = GenieConfig.TB_rebalance_maemae_2_C; 매매구분 = GenieConfig.combo_rebalance_maemae_gubun_C;
                    취소시간 = GenieConfig.MTB_rebalance_Cancel_time_C; 지연시간 = GenieConfig.MTB_rebalance_delay_C;
                    break;

                case "D":
                    사용체크 = GenieConfig.CB_rebalance_D; 잔고가동 = 잔고.가동_리밸D; 
                    시작시간 = GenieConfig.MT_rebalance_starttime_D; 종료시간 = GenieConfig.MT_rebalance_stoptime_D;
                    매입비중 = GenieConfig.TB_Rebalance_매입금_D;
                    누적거래량 = GenieConfig.TB_rebalance_누적거래량_D; 누적거래대금 = GenieConfig.TB_rebalance_누적거래대금_D;
                    검색식사용 = GenieConfig.combo_rebalance_use_condition_D; 검색식명 = Form1.위치별검색식리스트["리밸_D"].이름;
                    매도감시 = GenieConfig.CB_rebalance_매도체크_D;

                    분_P1 = GenieConfig.CBB_rebalance_MinMAPeriod1_D; 분_P2 = GenieConfig.CBB_rebalance_MinMAPeriod2_D; 분_배열 = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_D;
                    분_V1 = min_mma.Rebalance_MAValue1_D; 분_V2 = min_mma.Rebalance_MAValue2_D;
                    일_P1 = GenieConfig.CBB_rebalance_DayMAPeriod1_D; 일_P2 = GenieConfig.CBB_rebalance_DayMAPeriod2_D; 일_배열 = GenieConfig.CBB_rebalance_DayMAPeriod_배열_D;
                    일_V1 = day_mma.Rebalance_MAValue1_D; 일_V2 = day_mma.Rebalance_MAValue2_D;

                    주문값 = GenieConfig.TB_rebalance_value_D; 주문구분 = GenieConfig.combo_rebalance_jumun_D;
                    수익1 = GenieConfig.TB_rebalance_suik_1_D; 수익2 = GenieConfig.TB_rebalance_suik_2_D; 수익선택 = GenieConfig.CB_rebalance_choice_D; 수익구분 = GenieConfig.combo_rebalance_suik_gubun_D;
                    매도비율 = GenieConfig.TB_rebalance_sell_ratio_D; 매도구분 = GenieConfig.combo_rebalance_sell_gubun_D;
                    매매1 = GenieConfig.TB_rebalance_maemae_1_D; 매매2 = GenieConfig.TB_rebalance_maemae_2_D; 매매구분 = GenieConfig.combo_rebalance_maemae_gubun_D;
                    취소시간 = GenieConfig.MTB_rebalance_Cancel_time_D; 지연시간 = GenieConfig.MTB_rebalance_delay_D;
                    break;

                case "E":
                    사용체크 = GenieConfig.CB_rebalance_E; 잔고가동 = 잔고.가동_리밸E; 
                    시작시간 = GenieConfig.MT_rebalance_starttime_E; 종료시간 = GenieConfig.MT_rebalance_stoptime_E;
                    매입비중 = GenieConfig.TB_Rebalance_매입금_E;
                    누적거래량 = GenieConfig.TB_rebalance_누적거래량_E; 누적거래대금 = GenieConfig.TB_rebalance_누적거래대금_E;
                    검색식사용 = GenieConfig.combo_rebalance_use_condition_E; 검색식명 = Form1.위치별검색식리스트["리밸_E"].이름;
                    매도감시 = GenieConfig.CB_rebalance_매도체크_E;

                    분_P1 = GenieConfig.CBB_rebalance_MinMAPeriod1_E; 분_P2 = GenieConfig.CBB_rebalance_MinMAPeriod2_E; 분_배열 = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_E;
                    분_V1 = min_mma.Rebalance_MAValue1_E; 분_V2 = min_mma.Rebalance_MAValue2_E;
                    일_P1 = GenieConfig.CBB_rebalance_DayMAPeriod1_E; 일_P2 = GenieConfig.CBB_rebalance_DayMAPeriod2_E; 일_배열 = GenieConfig.CBB_rebalance_DayMAPeriod_배열_E;
                    일_V1 = day_mma.Rebalance_MAValue1_E; 일_V2 = day_mma.Rebalance_MAValue2_E;

                    주문값 = GenieConfig.TB_rebalance_value_E; 주문구분 = GenieConfig.combo_rebalance_jumun_E;
                    수익1 = GenieConfig.TB_rebalance_suik_1_E; 수익2 = GenieConfig.TB_rebalance_suik_2_E; 수익선택 = GenieConfig.CB_rebalance_choice_E; 수익구분 = GenieConfig.combo_rebalance_suik_gubun_E;
                    매도비율 = GenieConfig.TB_rebalance_sell_ratio_E; 매도구분 = GenieConfig.combo_rebalance_sell_gubun_E;
                    매매1 = GenieConfig.TB_rebalance_maemae_1_E; 매매2 = GenieConfig.TB_rebalance_maemae_2_E; 매매구분 = GenieConfig.combo_rebalance_maemae_gubun_E;
                    취소시간 = GenieConfig.MTB_rebalance_Cancel_time_E; 지연시간 = GenieConfig.MTB_rebalance_delay_E;
                    break;

                case "F":
                    사용체크 = GenieConfig.CB_rebalance_F; 잔고가동 = 잔고.가동_리밸F; 
                    시작시간 = GenieConfig.MT_rebalance_starttime_F; 종료시간 = GenieConfig.MT_rebalance_stoptime_F;
                    매입비중 = GenieConfig.TB_Rebalance_매입금_F;
                    누적거래량 = GenieConfig.TB_rebalance_누적거래량_F; 누적거래대금 = GenieConfig.TB_rebalance_누적거래대금_F;
                    검색식사용 = GenieConfig.combo_rebalance_use_condition_F; 검색식명 = Form1.위치별검색식리스트["리밸_F"].이름;
                    매도감시 = GenieConfig.CB_rebalance_매도체크_F;

                    분_P1 = GenieConfig.CBB_rebalance_MinMAPeriod1_F; 분_P2 = GenieConfig.CBB_rebalance_MinMAPeriod2_F; 분_배열 = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_F;
                    분_V1 = min_mma.Rebalance_MAValue1_F; 분_V2 = min_mma.Rebalance_MAValue2_F;
                    일_P1 = GenieConfig.CBB_rebalance_DayMAPeriod1_F; 일_P2 = GenieConfig.CBB_rebalance_DayMAPeriod2_F; 일_배열 = GenieConfig.CBB_rebalance_DayMAPeriod_배열_F;
                    일_V1 = day_mma.Rebalance_MAValue1_F; 일_V2 = day_mma.Rebalance_MAValue2_F;

                    주문값 = GenieConfig.TB_rebalance_value_F; 주문구분 = GenieConfig.combo_rebalance_jumun_F;
                    수익1 = GenieConfig.TB_rebalance_suik_1_F; 수익2 = GenieConfig.TB_rebalance_suik_2_F; 수익선택 = GenieConfig.CB_rebalance_choice_F; 수익구분 = GenieConfig.combo_rebalance_suik_gubun_F;
                    매도비율 = GenieConfig.TB_rebalance_sell_ratio_F; 매도구분 = GenieConfig.combo_rebalance_sell_gubun_F;
                    매매1 = GenieConfig.TB_rebalance_maemae_1_F; 매매2 = GenieConfig.TB_rebalance_maemae_2_F; 매매구분 = GenieConfig.combo_rebalance_maemae_gubun_F;
                    취소시간 = GenieConfig.MTB_rebalance_Cancel_time_F; 지연시간 = GenieConfig.MTB_rebalance_delay_F;
                    break;

                case "G":
                    사용체크 = GenieConfig.CB_rebalance_G; 잔고가동 = 잔고.가동_리밸G; 
                    시작시간 = GenieConfig.MT_rebalance_starttime_G; 종료시간 = GenieConfig.MT_rebalance_stoptime_G;
                    매입비중 = GenieConfig.TB_Rebalance_매입금_G;
                    누적거래량 = GenieConfig.TB_rebalance_누적거래량_G; 누적거래대금 = GenieConfig.TB_rebalance_누적거래대금_G;
                    검색식사용 = GenieConfig.combo_rebalance_use_condition_G; 검색식명 = Form1.위치별검색식리스트["리밸_G"].이름;
                    매도감시 = GenieConfig.CB_rebalance_매도체크_G;

                    분_P1 = GenieConfig.CBB_rebalance_MinMAPeriod1_G; 분_P2 = GenieConfig.CBB_rebalance_MinMAPeriod2_G; 분_배열 = GenieConfig.CBB_rebalance_MinMAPeriod1_배열_G;
                    분_V1 = min_mma.Rebalance_MAValue1_G; 분_V2 = min_mma.Rebalance_MAValue2_G;
                    일_P1 = GenieConfig.CBB_rebalance_DayMAPeriod1_G; 일_P2 = GenieConfig.CBB_rebalance_DayMAPeriod2_G; 일_배열 = GenieConfig.CBB_rebalance_DayMAPeriod_배열_G;
                    일_V1 = day_mma.Rebalance_MAValue1_G; 일_V2 = day_mma.Rebalance_MAValue2_G;

                    주문값 = GenieConfig.TB_rebalance_value_G; 주문구분 = GenieConfig.combo_rebalance_jumun_G;
                    수익1 = GenieConfig.TB_rebalance_suik_1_G; 수익2 = GenieConfig.TB_rebalance_suik_2_G; 수익선택 = GenieConfig.CB_rebalance_choice_G; 수익구분 = GenieConfig.combo_rebalance_suik_gubun_G;
                    매도비율 = GenieConfig.TB_rebalance_sell_ratio_G; 매도구분 = GenieConfig.combo_rebalance_sell_gubun_G;
                    매매1 = GenieConfig.TB_rebalance_maemae_1_G; 매매2 = GenieConfig.TB_rebalance_maemae_2_G; 매매구분 = GenieConfig.combo_rebalance_maemae_gubun_G;
                    취소시간 = GenieConfig.MTB_rebalance_Cancel_time_G; 지연시간 = GenieConfig.MTB_rebalance_delay_G;
                    break;
            }

            // =========================================================
            // 3. 로직 수행 (할당된 변수로 통합 로직 실행)
            // =========================================================

            // 3-1. 기본 가동 체크
            if (!사용체크) return;
            if (!잔고가동) return;

            // 3-2. 자금 및 거래량 체크
            long 목표매입금 = (long)(매입비중 / 100.0 * GenieConfig.MT_buying_standard);
            if (잔고.매입금액 + 잔고.신용_매입금액 <= 목표매입금) return;
            if (market_Item.누적거래량 <= 누적거래량) return;
            if (market_Item.누적거래대금 <= 누적거래대금) return;

            // 3-3. 시간 및 추매가능 체크
            if (!Method.RunTime(시작시간, 종료시간)) return;
            if (!Method.추매가능_Check(잔고, true, false)) return;

            string 검색식 = (검색식사용 == 0) ? $"{위치} []" : $"{위치} [{검색식명}]";
            if (수익구분 == 7)
            {
                int 공백위치 = 검색식.IndexOf(' '); // 첫 번째 공백의 위치를 찾음
                string 위치_정보 = (공백위치 == -1) ? 검색식 : 검색식.Substring(0, 공백위치);

                int 현재_최대번호 = -1;
                if (Form1.최종매입가_List.TryGetValue(잔고.종목코드, out List<최종매입가> 종목데이터_리스트))
                {
                    // [최적화 3] 리스트 순회 시 스레드 안전을 위한 잠금
                    lock (종목데이터_리스트)
                    {
                        // LINQ(OrderBy) 대신 직접 루프를 돌아 가장 큰 번호를 찾습니다. (Zero-GC)
                        // 저사양 PC에서 정렬 연산은 매우 무겁기 때문에 이 방식이 훨씬 빠릅니다.
                        foreach (var 항목 in 종목데이터_리스트)
                        {
                            if (항목.위치 == 위치_정보)
                            {
                                if (항목.번호 > 현재_최대번호)
                                {
                                    현재_최대번호 = 항목.번호;
                                }
                            }
                        }
                    }
                }

                검색식 = $"{검색식} {현재_최대번호 + 1}";
            }

            if (!Method.매매진입_가능여부(잔고.종목코드, 검색식)) return;
            if (!감시리스트체크(잔고.종목코드, 위치, 매도감시)) return;

            // 3-5. 이평선 체크
            if (!MA.Get_이평(잔고, 분_P1, 분_P2, 분_배열, 분_V1, 분_V2)) return;
            if (!MA.Get_이평(잔고, 일_P1, 일_P2, 일_배열, 일_V1, 일_V2)) return;

            // 3-6. 매매 실행 (Catch List 연동 최적화)
            string 익절그룹 = GET.익절그룹(위치);
            bool 매수모드 = true; // 리밸런싱은 매수
            int 취소N주문 = 0;
            int 반복 = 0;

            if (검색식사용 > 0) // 검색식 사용
            {
                string 검색키 = 위치 + 잔고.종목코드;

                // [최적화] TryGetValue로 두 번 검색 방지
                if (Form1.Catch_Stock_List.TryGetValue(검색키, out Catch_stock item))
                {
                    if (지연시간 <= item.timer)
                    {
                        bool 매매결과 = await Tab_Repeat.반복매매(잔고, 매수모드, 주문값, 주문구분, 수익1, 수익2, 수익선택, 수익구분,
                                매도비율, 매도구분, 매매1, 매매2, 매매구분, 익절그룹, 취소시간, 취소N주문, 반복, 위치, 검색식명);

                        if (매매결과)
                        {
                            // [최적화] TryRemove로 안전하게 제거
                            Form1.Catch_Stock_List.TryRemove(검색키, out _);
                        }
                    }
                }
            }
            else // 즉시 실행
            {
                await Tab_Repeat.반복매매(잔고, 매수모드, 주문값, 주문구분, 수익1, 수익2, 수익선택, 수익구분,
                    매도비율, 매도구분, 매매1, 매매2, 매매구분, 익절그룹, 취소시간, 취소N주문, 반복, 위치, "");
            }
        }


        public static void 리밸등록(string 주문번호, string Screennum, string 종목코드, string 위치, int 매수수량, int 주문가격, int 수익구분)
        {
            // 💡 [최적화 1] 기본값(A)을 가장 먼저 꽉 채워둡니다.
            string 리밸매도기준_1 = GenieConfig.리밸매도기준1_A;
            string 리밸매도기준_2 = GenieConfig.리밸매도기준2_A;
            double 비중_1 = GenieConfig.TB_rebalance_sellvolume1_A;
            double 비중_2 = GenieConfig.TB_rebalance_sellvolume2_A;

            // 💡 [궁극의 최적화 2] 3개의 Contains를 모조리 버리고 switch문으로 0.0001초 만에 다이렉트 점프!
            switch (위치)
            {
                case "리밸_A":
                    // 이미 A로 세팅되어 있으므로 아무것도 안 하고 통과!
                    break;

                case "리밸_B":
                    리밸매도기준_1 = GenieConfig.리밸매도기준1_B;
                    리밸매도기준_2 = GenieConfig.리밸매도기준2_B;
                    비중_1 = GenieConfig.TB_rebalance_sellvolume1_B;
                    비중_2 = GenieConfig.TB_rebalance_sellvolume2_B;
                    break;

                case "리밸_C":
                    리밸매도기준_1 = GenieConfig.리밸매도기준1_C;
                    리밸매도기준_2 = GenieConfig.리밸매도기준2_C;
                    비중_1 = GenieConfig.TB_rebalance_sellvolume1_C;
                    비중_2 = GenieConfig.TB_rebalance_sellvolume2_C;
                    break;

                case "리밸_D":
                    리밸매도기준_1 = GenieConfig.리밸매도기준1_D;
                    리밸매도기준_2 = GenieConfig.리밸매도기준2_D;
                    비중_1 = GenieConfig.TB_rebalance_sellvolume1_D;
                    비중_2 = GenieConfig.TB_rebalance_sellvolume2_D;
                    break;

                case "리밸_E":
                    리밸매도기준_1 = GenieConfig.리밸매도기준1_E;
                    리밸매도기준_2 = GenieConfig.리밸매도기준2_E;
                    비중_1 = GenieConfig.TB_rebalance_sellvolume1_E;
                    비중_2 = GenieConfig.TB_rebalance_sellvolume2_E;
                    break;

                case "리밸_F":
                    리밸매도기준_1 = GenieConfig.리밸매도기준1_F;
                    리밸매도기준_2 = GenieConfig.리밸매도기준2_F;
                    비중_1 = GenieConfig.TB_rebalance_sellvolume1_F;
                    비중_2 = GenieConfig.TB_rebalance_sellvolume2_F;
                    break;

                case "리밸_G":
                    리밸매도기준_1 = GenieConfig.리밸매도기준1_G;
                    리밸매도기준_2 = GenieConfig.리밸매도기준2_G;
                    비중_1 = GenieConfig.TB_rebalance_sellvolume1_G;
                    비중_2 = GenieConfig.TB_rebalance_sellvolume2_G;
                    break;

                default:
                    // 💡 [입구 컷] "리밸_A~G"가 정확히 아니면 ([감시]가 붙어있든 엉뚱한 글자든) 
                    // 더 이상 밑으로 내려가지 않고 여기서 즉시 탈출(Return)합니다!
                    return;
            }

            // =======================================================
            // [+] 수량_1 최소 1주 보장 및 수량_2 안전 분배 로직
            // =======================================================
            int 수량_1 = 0;
            int 수량_2 = 0;

            if (리밸매도기준_1 != "(    X    )")
            {
                // ---------------------------------------------------
                // [1] 수량_1 계산 및 최소 1주 보장
                // ---------------------------------------------------
                if (비중_1 == 100)
                {
                    수량_1 = 매수수량;
                }
                else
                {
                    수량_1 = (int)Math.Truncate(매수수량 * 비중_1 / 100.0); // 버림

                    // 0주 방지: 내 잔고가 1주 이상이면 최소 1주 강제 할당
                    if (수량_1 == 0 && 매수수량 >= 1)
                    {
                        수량_1 = 1;
                    }
                }

                // ---------------------------------------------------
                // [2] 수량_2 계산 (비중_2 == 100 조건 완벽 유지)
                // ---------------------------------------------------
                if (리밸매도기준_2 != "(    X    )")
                {
                    if (비중_2 == 100)
                    {
                        // 요청하신 원본 조건 100% 유지 (전량 청산용)
                        수량_2 = 매수수량;
                    }
                    else
                    {
                        if (비중_1 + 비중_2 == 100)
                        {
                            수량_2 = 매수수량 - 수량_1;
                        }
                        else
                        {
                            수량_2 = (int)Math.Truncate(매수수량 * 비중_2 / 100.0); // 버림
                        }

                        // 비중이 100이 아닐 때(부분 매도일 때)만 초과 매도 방지 로직 적용
                        if (수량_1 + 수량_2 > 매수수량)
                        {
                            수량_2 = 매수수량 - 수량_1;
                        }
                    }

                    if (수량_2 < 0)
                    {
                        수량_2 = 0;
                    }
                }

                // ---------------------------------------------------
                // [3] 최종 주문 리스트 등록 (둘 중 하나라도 1주 이상일 때만)
                // ---------------------------------------------------
                if (수량_1 > 0 || 수량_2 > 0)
                {
                    Rebal_Sell Rebal = new Rebal_Sell(위치, 종목코드, Screennum, 주문번호, 수량_1, 수량_2, 주문가격, 수익구분);
                    Form1.form1.Rebal_Sell_List.Add(Rebal);
                }
            }
        }

        public static void 리밸매도(string 종목코드, int 체_체결가, int 체_단위체결량, int 체결수량, int 체_주문수량, string Screennum)
        {
            // 💡 [최적화 1] 함수 호출 오버헤드가 있는 .Equals() 대신 == 연산자를 사용하여 속도를 높입니다.
            Rebal_Sell 리밸 = Form1.form1.Rebal_Sell_List.Find(o => o.종목코드 == 종목코드 && o.Screennum == Screennum);
            if (리밸 == null) return; // 💡 없으면 즉시 탈출 (Early Return)

            int 주문수량 = 0;

            // 💡 [최적화 2] 기본값(A)을 아예 먼저 채워둡니다. (리밸_A일 경우 검사조차 안 하고 0.00초만에 바로 통과)
            bool 단위체결 = GenieConfig.CB_rebalance_option_A;
            double _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_A;

            // 💡 [궁극의 최적화 3] 글자를 찾는 Contains 대신, 다이렉트로 꽂아버리는 switch-case 도입!
            switch (리밸.위치)
            {
                case "리밸_B":
                    단위체결 = GenieConfig.CB_rebalance_option_B;
                    _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_B;
                    break;

                case "리밸_C":
                    단위체결 = GenieConfig.CB_rebalance_option_C;
                    _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_C;
                    break;

                case "리밸_D":
                    단위체결 = GenieConfig.CB_rebalance_option_D;
                    _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_D;
                    break;

                case "리밸_E":
                    단위체결 = GenieConfig.CB_rebalance_option_E;
                    _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_E;
                    break;

                case "리밸_F":
                    단위체결 = GenieConfig.CB_rebalance_option_F;
                    _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_F;
                    break;

                case "리밸_G":
                    단위체결 = GenieConfig.CB_rebalance_option_G;
                    _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_G;
                    break;
            }

            if (단위체결) // 단위체결 후 주문
            {
                if (리밸._1차수량 > 체_단위체결량 && 리밸._1차수량 > 0)
                {
                    주문수량 = 체_단위체결량;
                    리밸._1차수량 -= 체_단위체결량;

                    리밸_오더(종목코드, 주문수량, 체_체결가, 리밸.위치 + "_1차", 0, 리밸.수익구분);
                }
                else
                {
                    주문수량 = 리밸._1차수량;
                    체_단위체결량 -= 리밸._1차수량;
                    리밸._1차수량 = 0;

                    if (주문수량 > 0) 리밸_오더(종목코드, 주문수량, 체_체결가, 리밸.위치 + "_1차", 0, 리밸.수익구분);

                    if (리밸._1차수량 == 0 && 리밸._2차수량 > 0)
                    {
                        if (리밸._2차수량 > 체_단위체결량)
                        {
                            주문수량 = 체_단위체결량;
                            리밸._2차수량 -= 체_단위체결량;

                            if (주문수량 > 0) 리밸_오더(종목코드, 주문수량, 체_체결가, 리밸.위치 + "_2차", 0, 리밸.수익구분);
                        }
                        else
                        {
                            주문수량 = 리밸._2차수량;
                            체_단위체결량 -= 리밸._2차수량;
                            리밸._2차수량 = 0;

                            if (주문수량 > 0) 리밸_오더(종목코드, 주문수량, 체_체결가, 리밸.위치 + "_2차", 0, 리밸.수익구분);
                            Form1.form1.Rebal_Sell_List.Remove(리밸);
                        }
                    }
                    else
                    {
                        Form1.form1.Rebal_Sell_List.Remove(리밸);
                    }
                }
            }
            else // 전량체결 후 주문
            {
                if (체_주문수량 != 체결수량) return; // 단위체결이 아닌데 아직 전량체결이 안 됐으면 대기 (탈출)

                int 연동감시번호 = GET_연동감시번호();
                if (_1차수비중 == 100)
                {
                    if (체결수량 >= 리밸._1차수량)
                    {
                        리밸_오더(종목코드, 리밸._1차수량, 리밸.체결가격, 리밸.위치 + "_1차", 연동감시번호, 리밸.수익구분);

                        if (리밸._2차수량 > 0)
                        {
                            리밸_오더(종목코드, 리밸._2차수량, 리밸.체결가격, 리밸.위치 + "_2차", 연동감시번호, 리밸.수익구분);
                        }
                    }
                    else
                    {
                        리밸_오더(종목코드, 체결수량, 리밸.체결가격, 리밸.위치 + "_1차", 연동감시번호, 리밸.수익구분);
                    }
                }
                else
                {
                    int 남는수량 = 체결수량 - 리밸._1차수량;
                    if (남는수량 > 0)
                    {
                        리밸_오더(종목코드, 리밸._1차수량, 리밸.체결가격, 리밸.위치 + "_1차", 연동감시번호, 리밸.수익구분);
                        리밸_오더(종목코드, 남는수량, 리밸.체결가격, 리밸.위치 + "_2차", 연동감시번호, 리밸.수익구분);
                    }
                    else
                    {
                        리밸_오더(종목코드, 리밸._1차수량, 리밸.체결가격, 리밸.위치 + "_1차", 연동감시번호, 리밸.수익구분);
                    }
                }
                Form1.form1.Rebal_Sell_List.Remove(리밸);
            }
        }

        public static void 취소_리밸주문(string Screennum, string 종목코드, string 매수매도, int 취소수량)
        {
            // 💡 [최적화 1] 입구 컷: "매수취소"가 아니면 더 볼 것도 없이 즉시 탈출! (들여쓰기 깊이 대폭 감소)
            if (매수매도 != "매수취소") return;

            // 💡 [최적화 2] 무거운 .Equals() 대신 == 연산자 사용, 없으면 즉시 탈출!
            Rebal_Sell 리밸 = Form1.form1.Rebal_Sell_List.Find(o => o.종목코드 == 종목코드 && o.Screennum == Screennum);
            if (리밸 == null) return;

            // 💡 기본값(A)을 먼저 채워둡니다.
            double _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_A;

            // 💡 [궁극의 최적화 3] Contains 7연타를 버리고 switch-case로 0.0001초 다이렉트 점프!
            switch (리밸.위치)
            {
                case "리밸_A": break;                 // 이미 A로 세팅되어 있으므로 아무것도 안 하고 통과!
                case "리밸_B": _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_B; break;
                case "리밸_C": _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_C; break;
                case "리밸_D": _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_D; break;
                case "리밸_E": _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_E; break;
                case "리밸_F": _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_F; break;
                case "리밸_G": _1차수비중 = GenieConfig.TB_rebalance_sellvolume1_G; break;
            }

            int 주문수량 = 리밸._1차수량 + 리밸._2차수량;
            int 체결수량 = 주문수량 - 취소수량;

            int 연동감시번호 = GET_연동감시번호();
            if (_1차수비중 == 100)
            {
                if (체결수량 >= 리밸._1차수량)
                {
                    리밸_오더(종목코드, 리밸._1차수량, 리밸.체결가격, 리밸.위치 + "_1차", 연동감시번호, 리밸.수익구분);

                    if (리밸._2차수량 > 0)
                    {
                        리밸_오더(종목코드, 리밸._2차수량, 리밸.체결가격, 리밸.위치 + "_2차", 연동감시번호, 리밸.수익구분);
                    }
                }
                else
                {
                    리밸_오더(종목코드, 체결수량, 리밸.체결가격, 리밸.위치 + "_1차", 연동감시번호, 리밸.수익구분);
                }
            }
            else
            {
                int 남는수량 = 체결수량 - 리밸._1차수량;
                if (남는수량 > 0)
                {
                    리밸_오더(종목코드, 리밸._1차수량, 리밸.체결가격, 리밸.위치 + "_1차", 연동감시번호, 리밸.수익구분);
                    리밸_오더(종목코드, 남는수량, 리밸.체결가격, 리밸.위치 + "_2차", 연동감시번호, 리밸.수익구분);
                }
                else
                {
                    리밸_오더(종목코드, 리밸._1차수량, 리밸.체결가격, 리밸.위치 + "_1차", 연동감시번호, 리밸.수익구분);
                }
            }
            Form1.form1.Rebal_Sell_List.Remove(리밸);
        }

        public static void 리밸_오더(string 종목코드, int 주문수량, int 주문체결가격, string 검색식, int 연동감시번호, int 수익구분)
        {
            if (주문수량 <= 0) return;
            if (stockBalanceList.TryGetValue(종목코드, out Stockbalance 잔고))
            {
                if (GET.총주문가능수량(잔고) > 0)
                {
                    int 차수기준 = 100;

                    // ==============================================================================
                    // 1. 구역 및 차수 사전 판별 (단 1번만 수행)
                    // ==============================================================================
                    string suffix = "";
                    string 구역 = "";
                    if (!string.IsNullOrEmpty(검색식) && 검색식.Contains("리밸_"))
                    {
                        int index = 검색식.IndexOf("리밸_") + 3;
                        if (검색식.Length > index)
                        {
                            suffix = 검색식.Substring(index, 1); // "A", "B"... 추출
                            구역 = "리밸_" + suffix;
                        }
                    }

                    // 차수 판별: 기본은 1차, "_2차" 글자가 있을 때만 2차로 인식
                    string 차수 = (검색식 != null && 검색식.Contains("_2차")) ? "_2차" : "_1차";

                    // 변수 초기화
                    bool 단위_기준 = GenieConfig.CB_rebalance_기준금;
                    double 감시_주문값 = 0;
                    int 감시_시장가구분 = 0;
                    int TimeChoice = 0;
                    double 차수주문값 = 0;
                    string 리밸매도기준 = "";
                    int 취소시간 = 0;
                    bool CB_rebalance_TS = false;
                    double TB_rebalance_TS_down = 0;
                    int TB_rebalance_TS_이평 = 0;
                    int CBB_rebalance_TS_이평 = 0;

                    // ==============================================================================
                    // 2. [Switch] 구역별 설정값 할당 (세로 정렬 버전)
                    // ==============================================================================
                    switch (suffix)
                    {
                        case "A":
                            감시_주문값 = GenieConfig.TB_rebalance_감시_value_A;
                            감시_시장가구분 = GenieConfig.combo_rebalance_감시_jumun_A;
                            TimeChoice = GenieConfig.CBB_rebalance_Selltime_A;

                            if (차수 == "_1차")
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio1_A;
                                리밸매도기준 = GenieConfig.리밸매도기준1_A;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel1_A;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_1차_A;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_1차_down_A;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_A;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_A;
                            }
                            else
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio2_A;
                                리밸매도기준 = GenieConfig.리밸매도기준2_A;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel2_A;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_2차_A;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_2차_down_A;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_A;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_A;
                            }
                            break;

                        case "B":
                            감시_주문값 = GenieConfig.TB_rebalance_감시_value_B;
                            감시_시장가구분 = GenieConfig.combo_rebalance_감시_jumun_B;
                            TimeChoice = GenieConfig.CBB_rebalance_Selltime_B;

                            if (차수 == "_1차")
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio1_B;
                                리밸매도기준 = GenieConfig.리밸매도기준1_B;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel1_B;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_1차_B;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_1차_down_B;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_B;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_B;
                            }
                            else
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio2_B;
                                리밸매도기준 = GenieConfig.리밸매도기준2_B;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel2_B;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_2차_B;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_2차_down_B;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_B;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_B;
                            }
                            break;

                        case "C":
                            감시_주문값 = GenieConfig.TB_rebalance_감시_value_C;
                            감시_시장가구분 = GenieConfig.combo_rebalance_감시_jumun_C;
                            TimeChoice = GenieConfig.CBB_rebalance_Selltime_C;

                            if (차수 == "_1차")
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio1_C;
                                리밸매도기준 = GenieConfig.리밸매도기준1_C;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel1_C;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_1차_C;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_1차_down_C;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_C;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_C;
                            }
                            else
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio2_C;
                                리밸매도기준 = GenieConfig.리밸매도기준2_C;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel2_C;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_2차_C;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_2차_down_C;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_C;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_C;
                            }
                            break;

                        case "D":
                            감시_주문값 = GenieConfig.TB_rebalance_감시_value_D;
                            감시_시장가구분 = GenieConfig.combo_rebalance_감시_jumun_D;
                            TimeChoice = GenieConfig.CBB_rebalance_Selltime_D;

                            if (차수 == "_1차")
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio1_D;
                                리밸매도기준 = GenieConfig.리밸매도기준1_D;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel1_D;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_1차_D;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_1차_down_D;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_D;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_D;
                            }
                            else
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio2_D;
                                리밸매도기준 = GenieConfig.리밸매도기준2_D;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel2_D;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_2차_D;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_2차_down_D;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_D;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_D;
                            }
                            break;

                        case "E":
                            감시_주문값 = GenieConfig.TB_rebalance_감시_value_E;
                            감시_시장가구분 = GenieConfig.combo_rebalance_감시_jumun_E;
                            TimeChoice = GenieConfig.CBB_rebalance_Selltime_E;

                            if (차수 == "_1차")
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio1_E;
                                리밸매도기준 = GenieConfig.리밸매도기준1_E;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel1_E;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_1차_E;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_1차_down_E;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_E;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_E;
                            }
                            else
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio2_E;
                                리밸매도기준 = GenieConfig.리밸매도기준2_E;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel2_E;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_2차_E;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_2차_down_E;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_E;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_E;
                            }
                            break;

                        case "F":
                            감시_주문값 = GenieConfig.TB_rebalance_감시_value_F;
                            감시_시장가구분 = GenieConfig.combo_rebalance_감시_jumun_F;
                            TimeChoice = GenieConfig.CBB_rebalance_Selltime_F;

                            if (차수 == "_1차")
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio1_F;
                                리밸매도기준 = GenieConfig.리밸매도기준1_F;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel1_F;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_1차_F;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_1차_down_F;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_F;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_F;
                            }
                            else
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio2_F;
                                리밸매도기준 = GenieConfig.리밸매도기준2_F;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel2_F;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_2차_F;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_2차_down_F;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_F;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_F;
                            }
                            break;

                        case "G":
                            감시_주문값 = GenieConfig.TB_rebalance_감시_value_G;
                            감시_시장가구분 = GenieConfig.combo_rebalance_감시_jumun_G;
                            TimeChoice = GenieConfig.CBB_rebalance_Selltime_G;

                            if (차수 == "_1차")
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio1_G;
                                리밸매도기준 = GenieConfig.리밸매도기준1_G;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel1_G;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_1차_G;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_1차_down_G;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_G;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_G;
                            }
                            else
                            {
                                차수주문값 = GenieConfig.TB_rebalance_sellratio2_G;
                                리밸매도기준 = GenieConfig.리밸매도기준2_G;
                                취소시간 = GenieConfig.TB_rebalance_sellcancel2_G;
                                CB_rebalance_TS = GenieConfig.CB_rebalance_TS_2차_G;
                                TB_rebalance_TS_down = GenieConfig.TB_rebalance_TS_2차_down_G;
                                TB_rebalance_TS_이평 = GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_G;
                                CBB_rebalance_TS_이평 = GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_G;
                            }
                            break;
                    }

                    // ==============================================================================
                    // 3. 최종번호 발급 (+1)
                    // ==============================================================================
                    int 최종번호 = 0;
                    if (수익구분 == 7 && !string.IsNullOrEmpty(구역))
                    {
                        최종번호 = Helper.통합_최종번호_가져오기(종목코드, 구역);
                        최종번호++;
                    }

                    //(X)  0
                    //매도수익률       1
                    //평가수익률       2
                    //기준수익률       3
                    //평가손익금       4
                    //예상손익금       5
                    //이익수익률       6 이익수익률 
                    //손절매도수익률   7
                    //손절평가수익률   8
                    //손절기준수익률   9

                    string 주문시간 = "Real";
                    if (TimeChoice == 1) 주문시간 = "AM";
                    else if (TimeChoice == 2) 주문시간 = "PM";

                    주문체결가격 = Method.호가맞추기(주문체결가격, Form1.Market_Item_List[종목코드].Market);
                    int 감시번호 = GET_감시번호();
                    int 감시주문가격 = 0; //Method.Order_price(감시_주문값, 감시_시장가구분, 잔고.종목코드, 감시가격);
                    int 손절주문가격 = 0;

                    double tax_ = Form1.TAX;
                    if (잔고.시장.Equals("E")) tax_ = 0;


                    // 1. 다중 if문을 switch문으로 변경하여 문자열 비교 오버헤드 완벽 제거 (저사양 PC 최적화)
                    switch (리밸매도기준)
                    {
                        case "매도수익률":
                        case "이익수익률":
                            // 두 기준의 로직이 완전히 동일하므로 하나로 묶어서 처리합니다.
                            감시주문가격 = int.Parse(Method.감시주문가계산(종목코드, 차수주문값 + (tax_ * 100) + (Form1.수수료 * 100 * 2), 주문체결가격, 잔고.현재가).Split('&')[0]);
                            break;

                        case "손절매도수익률":
                            손절주문가격 = int.Parse(Method.감시주문가계산(종목코드, 차수주문값, 주문체결가격, 잔고.현재가).Split('&')[0]);
                            break;

                        case "평가수익률":
                            if (0 < 잔고.수익률) 차수주문값 += 잔고.수익률;
                            break;

                        case "기준수익률":
                            if (0 < 잔고.기준수익률) 차수주문값 += 잔고.기준수익률;
                            break;

                        case "평가손익금":
                            if (0 < 잔고.평가손익)
                            {
                                // 불필요한 지역변수 할당을 줄이고 코드를 압축했습니다.
                                if (단위_기준) 차수주문값 += Math.Round((double)잔고.평가손익 / GenieConfig.MT_buying_standard * 100, 2);
                                else 차수주문값 += Math.Round((double)잔고.평가손익 / 10000, 2);
                            }
                            break;

                        case "예상손익금":
                            if (0 < 잔고.예상손익)
                            {
                                if (단위_기준) 차수주문값 += Math.Round((double)잔고.예상손익 / GenieConfig.MT_buying_standard * 100, 2);
                                else 차수주문값 += Math.Round((double)잔고.예상손익 / 10000, 2);
                            }
                            break;
                    }

                    // 2. 가독성을 높인 최신 C# 객체 초기화 방식 적용
                    감시주문 감시 = new 감시주문
                    {
                        종목코드 = 종목코드,
                        주문수량 = 주문수량,
                        주문체결가격 = 주문체결가격,
                        감시주문가격 = 감시주문가격,
                        손절주문가격 = 손절주문가격,
                        감시_주문값 = 감시_주문값,
                        감시_시장가구분 = 감시_시장가구분,
                        원주문번호 = "+++",                // 기존 8번째 인자
                        검색식 = 검색식 + " [감시]",       // 기존 9번째 인자
                        종목명 = 잔고.종목명,
                        감시일 = str.today,               // 기존 11번째 인자
                        주문일 = "",                      // 기존 12번째 인자
                        감시번호 = 감시번호,
                        연동감시번호 = 연동감시번호,
                        주문시간 = 주문시간,
                        단위_기준 = 단위_기준,
                        차수주문값 = 차수주문값,
                        차수기준 = 차수기준,
                        취소시간 = 취소시간,              
                        수익구분 = 수익구분,
                        최종번호 = 최종번호,
                        리밸매도기준 = 리밸매도기준,
                        TS = CB_rebalance_TS,             // 변수명 'CB...' -> 속성 'TS'
                        TS_high = 0,                      // 기존 0값 대입
                        TS_down = TB_rebalance_TS_down,
                        TS_이평 = TB_rebalance_TS_이평,
                        CBB_TS_이평 = CBB_rebalance_TS_이평
                    };

                    // 리스트에 추가
                    Form1.감시주문_List.TryAdd(감시번호.ToString(), 감시);


                    if (수익구분 == 7 && 차수 == "_1차")
                    {
                        // 1. 해당 종목의 리스트를 가져옵니다. (없으면 새로 만듦)
                        var 해당종목_리스트 = Form1.최종매입가_List.GetOrAdd(종목코드, _ => new List<최종매입가>());

                        // 2. 리스트 사용 중 충돌 방지 (Lock)
                        lock (해당종목_리스트)
                        {
                            int 현재_최대번호 = 0;
                            int count = 해당종목_리스트.Count; // Count 속성 접근 최소화

                            for (int i = 0; i < count; i++)
                            {
                                var item = 해당종목_리스트[i];

                                // 위치가 같고, 번호가 더 크면 갱신
                                if (item.위치 == 구역)
                                {
                                    if (item.번호 > 현재_최대번호)
                                    {
                                        현재_최대번호 = item.번호;
                                    }
                                }
                            }

                            // 4. 새로운 번호(최대번호 + 1)로 리스트에 추가
                            해당종목_리스트.Add(new 최종매입가
                            {
                                종목명 = Form1.Market_Item_List[종목코드].종목명,
                                종목코드 = 종목코드,
                                위치 = 구역,
                                번호 = 현재_최대번호 + 1,
                                매입가 = 주문체결가격
                            });
                        }

                        SaveToFile.최종매입가_파일저장(Form1.로딩완료);
                    }

                    SaveToFile.리밸감시주문_파일저장();
                }
                else
                {
                    if (금액알림)
                    {
                        금액알림 = false;
                        Form1.AutoClosingAlram("[리밸런싱 주문불가] 종목명:" + 잔고.종목명 + "주문가능 수량이 없어 ' 매도 '주문 할수 없습니다.", "주문불가", 10, "에러");
                    }
                }
            }
        }


        public static void 리밸감시_감시중(Stockbalance 잔고)
        {
            if (잔고.매도정지) return;
            if (!잔고.매매가능) return;

            // [지니 최적화] 리스트를 시간대별로 3개 미리 준비합니다.
            List<감시주문> list_Real = new List<감시주문>();
            List<감시주문> list_AM = new List<감시주문>();
            List<감시주문> list_PM = new List<감시주문>();

            // [원패스 분류] 전체를 한 번만 훑으면서 각각의 바구니에 담습니다.
            foreach (var 주문 in 감시주문_List.Values)
            {
                // 내 종목이 아니면 패스
                if (주문.종목코드 != 잔고.종목코드) continue;

                // 주문 시간에 따라 분류하여 담기 (문자열 비교 최소화)
                switch (주문.주문시간)
                {
                    case "Real":
                        list_Real.Add(주문);
                        break;
                    case "AM":
                        list_AM.Add(주문);
                        break;
                    case "PM":
                        list_PM.Add(주문);
                        break;
                }
            }

            // -------------------------------------------------------
            // [실행 단계] 분류된 리스트가 있는 경우에만 각각 실행
            // -------------------------------------------------------

            // 1. 실시간(Real) 감시
            if (list_Real.Count > 0)
            {
                감시주문(list_Real);
            }

            // 2. 오전(AM) 감시
            // (리스트에 내용이 있고 && 오전 감시가 켜져 있고 && 시간이 되었을 때)
            if (list_AM.Count > 0 && form1.오전감시 && Get.오전감시시간 < Get.TimeNow)
            {
                form1.오전감시 = false;

                // UI 변경 안전장치
                if (FormAccountManagement_Open)
                {
                    try { Form_AccountManagement.form.LB_리밸매도시간오전.BackColor = Color.Tan; } catch { }
                }

                감시주문(list_AM);
            }

            // 3. 오후(PM) 감시
            // (리스트에 내용이 있고 && 오후 감시가 켜져 있고 && 시간이 되었을 때)
            if (list_PM.Count > 0 && form1.오후감시 && Get.오후감시시간 < Get.TimeNow)
            {
                form1.오후감시 = false;

                // UI 변경 안전장치
                if (FormAccountManagement_Open)
                {
                    try { Form_AccountManagement.form.LB_리밸매도시간오후.BackColor = Color.Tan; } catch { }
                }

                감시주문(list_PM);
            }

            // 트레일링 스탑 감시
            리밸감시_TS_감시(잔고);

            void 감시주문(List<감시주문> 종목감시_LIST)
            {
                // 역순 for문을 사용하여 삭제 시 발생하는 컬렉션 에러 원천 차단
                for (int i = 종목감시_LIST.Count - 1; i >= 0; i--)
                {
                    감시주문 Item = 종목감시_LIST[i];

                    // 2. 중복 검사
                    bool 중복주문존재 = Item.원주문번호 != "+++" && Form1.JumunItem_List.ContainsKey(Item.원주문번호);

                    // 3. 중복이 없거나, TS_high가 설정된 경우 실행
                    if (!중복주문존재 || Item.TS_high != 0)
                    {
                        // Case A: 감시주문가격이 설정된 경우 (이익실현 등)
                        if (Item.감시주문가격 > 0)
                        {
                            if (Item.주문수량 > 0)
                            {
                                if (Item.감시_시장가구분 == 0) // 시장가
                                {
                                    if (Item.리밸매도기준 == "이익수익률")
                                    {
                                        if (잔고.수익률 > 0.3 && 잔고.현재가 >= Item.감시주문가격)
                                        {
                                            주문전달(잔고, Item, "");
                                        }
                                    }
                                    else
                                    {
                                        if (잔고.현재가 >= Item.감시주문가격)
                                        {
                                            주문전달(잔고, Item, "");
                                        }
                                    }
                                }
                                else // 지정가
                                {
                                    int 상한가 = Method.상한가_하한가_구하기("상", 잔고.종목코드);

                                    if (Item.감시주문가격 < 상한가)
                                    {
                                        if (Item.TS)
                                        {
                                            if (잔고.현재가 > Item.감시주문가격)
                                            {
                                                주문전달(잔고, Item, "high");
                                            }
                                        }
                                        else
                                        {
                                            int Tik_cap = Method.Find_Tik_Cap(잔고.현재가, Item.감시주문가격, 잔고.시장);

                                            if (Tik_cap <= 5)
                                            {
                                                if (Item.리밸매도기준 == "이익수익률")
                                                {
                                                    if (잔고.수익률 > 0.3)
                                                    {
                                                        주문전달(잔고, Item, "");
                                                    }
                                                }
                                                else
                                                {
                                                    주문전달(잔고, Item, "");
                                                }
                                            }
                                        }
                                    }
                                    else // 감시가격 >= 상한가
                                    {
                                        if (잔고.현재가 >= Item.감시주문가격)
                                        {
                                            DataManagement.감시주문삭제(Item, "주문체결");
                                        }
                                    }
                                }
                            }
                            else // 주문수량 <= 0 (체결 완료 등)
                            {
                                if (잔고.현재가 >= Item.감시주문가격)
                                {
                                    DataManagement.감시주문삭제(Item, "주문체결");
                                }
                            }
                        }
                        // Case B: 감시주문가격 미설정 (수익률 감시 등)
                        else
                        {
                            if (Item.주문수량 > 0)
                            {
                                if (Item.감시_시장가구분 == 0) // 시장가
                                {
                                    if (리밸감시_수익계산(잔고, Item))
                                    {
                                        주문전달(잔고, Item, "");
                                    }
                                }
                                else // 지정가
                                {
                                    if (Item.TS && !Item.리밸매도기준.Contains("손절"))
                                    {
                                        if (리밸감시_수익계산(잔고, Item))
                                        {
                                            주문전달(잔고, Item, "high");
                                        }
                                    }
                                    else
                                    {
                                        if (감시주문_주문가계산(잔고, Item) != "주문불가")
                                        {
                                            if (미리주문_수익계산(잔고, Item))
                                            {
                                                주문전달(잔고, Item, "");
                                            }
                                        }
                                    }
                                }
                            }
                            else // 주문수량 <= 0
                            {
                                if (리밸감시_수익계산(잔고, Item))
                                {
                                    DataManagement.감시주문삭제(Item, "주문체결");
                                }
                            }
                        }
                    }
                }
            }
        }



        public static void 리밸감시_TS_감시(Stockbalance 잔고)
        {
            foreach (감시주문 Item in 감시주문_List.Values)
            {
                // [조건 체크] 종목코드가 일치하는 경우만 로직을 수행합니다.
                if (Item.종목코드 == 잔고.종목코드)
                {
                    if (!Item.TS)
                    {
                        Item.TS_high = 0;
                    }

                    if (Item.TS && Item.TS_high != 0)
                    {
                        if (Item.TS_high < 잔고.현재가) Item.TS_high = 잔고.현재가;

                        double 주문비 = ((double)잔고.현재가 - (double)Item.TS_high) / (double)Item.TS_high * (double)100;
                        if (Item.TS_down >= 주문비)
                        {
                            bool 중복검사 = Item.원주문번호 == "+++" || !Form1.JumunItem_List.ContainsKey(Item.원주문번호);

                            if (중복검사)
                            {
                                MAPeriod mma = Form1.Min_ma_list[잔고.종목코드];
                                double min_mma = mma.Rebalance_TS_MAValue_1차_A; // 1차_A를 기본값으로 선할당

                                // [최적화] "리밸_" 문구가 포함되어 있을 때만 하위 검사를 수행합니다.
                                if (Item.검색식.Contains("리밸_"))
                                {
                                    // 2차 여부를 먼저 판별하여 트리 구조로 검색 속도를 높입니다.
                                    if (Item.검색식.Contains("2차"))
                                    {
                                        if (Item.검색식.Contains("리밸_A_2차")) min_mma = mma.Rebalance_TS_MAValue_2차_A;
                                        else if (Item.검색식.Contains("리밸_B_2차")) min_mma = mma.Rebalance_TS_MAValue_2차_B;
                                        else if (Item.검색식.Contains("리밸_C_2차")) min_mma = mma.Rebalance_TS_MAValue_2차_C;
                                        else if (Item.검색식.Contains("리밸_D_2차")) min_mma = mma.Rebalance_TS_MAValue_2차_D;
                                        else if (Item.검색식.Contains("리밸_E_2차")) min_mma = mma.Rebalance_TS_MAValue_2차_E;
                                        else if (Item.검색식.Contains("리밸_F_2차")) min_mma = mma.Rebalance_TS_MAValue_2차_F;
                                        else if (Item.검색식.Contains("리밸_G_2차")) min_mma = mma.Rebalance_TS_MAValue_2차_G;
                                    }
                                    else
                                    {
                                        // 2차가 아닌 경우 (A는 이미 기본값으로 들어있으므로 B부터 검사)
                                        if (Item.검색식.Contains("리밸_B")) min_mma = mma.Rebalance_TS_MAValue_1차_B;
                                        else if (Item.검색식.Contains("리밸_C")) min_mma = mma.Rebalance_TS_MAValue_1차_C;
                                        else if (Item.검색식.Contains("리밸_D")) min_mma = mma.Rebalance_TS_MAValue_1차_D;
                                        else if (Item.검색식.Contains("리밸_E")) min_mma = mma.Rebalance_TS_MAValue_1차_E;
                                        else if (Item.검색식.Contains("리밸_F")) min_mma = mma.Rebalance_TS_MAValue_1차_F;
                                        else if (Item.검색식.Contains("리밸_G")) min_mma = mma.Rebalance_TS_MAValue_1차_G;
                                    }
                                }

                                if (MA.Get_TS_이평(잔고, Item.CBB_TS_이평, min_mma))
                                {
                                    int Tik_cap = Method.Find_Tik_Cap(잔고.현재가, Item.감시주문가격, 잔고.시장);

                                    if (Tik_cap <= 15)
                                    {
                                        if (Item.리밸매도기준 == "이익수익률")
                                        {
                                            // [최적화] 중첩된 조건을 한 줄로 묶어 처리합니다.
                                            if (잔고.수익률 > 0.3 && 잔고.현재가 > Item.감시주문가격)
                                            {
                                                주문전달(잔고, Item, " TS");
                                            }
                                        }
                                        else
                                        {
                                            주문전달(잔고, Item, " TS");
                                        }
                                    }
                                    else
                                    {
                                        Item.TS_high = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void 주문전달(Stockbalance 잔고, 감시주문 감시, string TS)
        {
            //if (내아이디)
            //{
            //    // 수익률의 크기가(위로든 아래로든) 1.01% 미만이면 돌아가라!
            //    if (Math.Abs(잔고.수익률) < 1.01) return;
            //}

            if (server == "모의투자")
            {
                if (server_알림 != "메인마켓") return;
            }

            if (감시.주문체결가격 == 0)
            {
                감시주문_List.TryRemove(감시.감시번호.ToString(), out _);
            }
            else
            {
                if (TS.Equals("high"))
                {
                    감시.TS_high = 잔고.현재가;
                }
                else
                {
                    if (GET.총주문가능수량(잔고) >= 감시.주문수량)
                    {
                        if (Method.매매확인_VI_모투가능확인(Form1.Market_Item_List[잔고.종목코드], 2))
                        {
                            int 감시가격 = 감시.감시주문가격;

                            if (감시.감시주문가격 == 0)
                            {
                                if (감시.리밸매도기준.Equals("평가수익률") || 감시.리밸매도기준.Equals("기준수익률")) 감시가격 = int.Parse(감시주문_주문가계산(잔고, 감시));
                                else 감시가격 = 잔고.현재가;
                            }

                            int 주문가격 = Method.Order_price(감시.감시_주문값, 감시.감시_시장가구분, 잔고.종목코드, 감시가격);

                            string 비고 = "";

                            int 취소시간 = 감시.취소시간;
                            int 기록_주문가격 = 주문가격;

                            if (감시.감시_시장가구분 == 0) //  [거래구분]
                            {
                                주문가격 = 0;
                            }

                            string 검색식 = 감시.검색식 + TS;
                            if (감시.수익구분 == 7)
                            {
                                검색식 = 감시.검색식 + " " + 감시.최종번호 + TS;
                            }

                      //      Console.WriteLine($"=== [주문전달]-> 검사시작 종목명: {잔고.종목명} | 조건: {검색식} | 수량: {감시.주문수량} | 감시번호: {감시.감시번호} ===");

                            if (Jumun.메인장부_중복_검사(잔고.종목코드, 검색식, 감시))
                            {
                      //      if(주문리스트_중복)    MessageBox.Show($"[디버그 추적 지니64] {잔고.종목명} 주문 불가 판정 발생!\n\n검색식_중복: {검색식_중복}\n주문리스트_중복: {주문리스트_중복}\n\n[확인]을 누르면 종료하고 빠져나갑니다.", "지니 디버그", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                return; // 주문 중단
                            }

                            int 매수매도 = 2;
                            int Order번호 = GET.Order번호();
                            string Screennum = GET.JumunScreen();

                            // [로그 추가 1] 중복 검사를 무사히 통과하고, 실제 신용/현금 계산으로 넘어가기 직전의 확정 로그입니다.
                            //   Console.WriteLine($"=== [주문전달]-> [확정]  종목명: {잔고.종목명} | 조건: {검색식} | 수량: {감시.주문수량} | 감시번호: {감시.감시번호} ===");


                            신용계산.신용주문_분할매도_실행(잔고, 감시.주문수량, async (is신용, 대출일, 수량) =>
                            {
                                await 주문(is신용, 대출일, 수량);
                            });

                            async Task 주문(bool 신용주문, string 대출일, int 주문수량)
                            {

                              //  Console.WriteLine($"=== [주문전달]-> [주문]  종목명: {잔고.종목명} | 감시번호: {감시.감시번호} | 주문수량: {주문수량} | 신용주문: {신용주문} | 대출일: {대출일}  ===");
                                
                                string 주문유형_텍스트 = 신용주문 ? "신용" : "현금";

                                // [로그 추가 2] 신용/현금 분할이 완료되어, 실제 큐(Queue)에 들어가기 직전의 최종 로그입니다.
                                Console.WriteLine($"=== [주문전달]-> [큐 등록] {주문유형_텍스트}매도 | 종목명: {잔고.종목명} | 실제주문수량: {주문수량} | 감시번호: {감시.감시번호}");

                                JumunItem 새주문 = new JumunItem
                                {
                                    신용주문 = 신용주문,
                                    대출일 = 대출일,
                                    Deletetimer = 0,
                                    Screennum = Screennum,
                                    종목코드 = 잔고.종목코드,
                                    종목명 = 잔고.종목명,
                                    주문번호 = "+++",     // *주의: 5번째 인자에 '감시.원주문번호'
                                    원주문번호 = 감시.원주문번호,
                                    검색식 = 검색식,
                                    주문값 = 감시.감시_주문값,      // *주의: '감시.감시_주문값'
                                    시장가구분 = 감시.감시_시장가구분, // *주의: '감시.감시_시장가구분'
                                    취소시간 = 취소시간,
                                    취소N주문 = 0,
                                    반복횟수 = 0,
                                    비고 = 비고,
                                    Pos = 감시.검색식,             // *주의: 14번째 인자에 '감시.검색식'
                                    주문수량 = 주문수량,       // *주의: '감시.주문수량'
                                    주문가격 = 기록_주문가격,
                                    매수매도 = 매수매도,
                                    비중 = 1,                      // *주의: 18번째 인자값 1
                                    비중단위 = 0,
                                    취소timer = 취소시간,          // *주의: 20번째 인자 '취소시간' (재사용)
                                    현재가 = 잔고.현재가,
                                    등락률 = 0,
                                    주문시간 = Get.TimeNow,
                                    미체결량 = 주문수량,       // 미체결량 초기화
                                    주문취소 = true,
                                    가동전 = false,
                                    Tik_cap = Method.Find_Tik_Cap(잔고.현재가, 주문가격, 잔고.시장),
                                    Tik_price = 잔고.현재가,       // *주의: 28번째 인자 '잔고.현재가'
                                    수익률 = 잔고.수익률,          // *주의: 29번째 인자 '잔고.수익률'
                                    주문동기화 = false,
                                    감시번호 = 감시.감시번호,       // *주의: 31번째 인자 '감시.감시번호'
                                    Order번호 = Order번호,
                                    수익구분 = 0,
                                    NXT = NXT_server,
                                    주문시간_Ticks = DateTime.Now.Ticks
                                };

                                // 리스트 추가
                                await Jumun.Add(새주문);
                                ExecuteTrade.Que_order(새주문);
                            }
                        }
                    }
                    else
                    {
                        if (잔고.보유수량 < 감시.주문수량)
                        {
                            DataManagement.감시주문삭제(감시, "주문정리");
                        }
                    }
                }
            }
        }




        public static string 감시주문_주문가계산(Stockbalance 잔고, 감시주문 감시)
        {
            string para = "";

            if (감시.리밸매도기준.Equals("평가수익률"))
            {
                if (감시.차수주문값 <= 잔고.수익률)
                {
                    para = 잔고.현재가.ToString();
                }
                else
                {
                    계산();
                }
            }
            else if (감시.리밸매도기준.Equals("기준수익률"))
            {
                if (감시.차수주문값 <= 잔고.기준수익률)
                {
                    para = 잔고.현재가.ToString();
                }
                else
                {
                    계산();
                }
            }
            else
            {
                para = 잔고.현재가.ToString();
            }

            void 계산()
            {
                int 주문가_계산 = Method.호가맞추기(잔고.현재가, 잔고.시장);
                double 조정값 = 0.5;
                if (잔고.현재가 >= 20000) 조정값 = 1;

                for (int i = 1; i > -1; i++)
                {
                    int 주문가 = 주문가_계산 + Method.GetHoga(주문가_계산, 잔고.시장);

                    주문가_계산 = 주문가;

                    double 주문비 = ((double)주문가 - (double)잔고.현재가) / (double)잔고.현재가 * (double)100;

                    if (주문비 >= 조정값)
                    {
                        para = 주문가.ToString();
                        break;
                    }
                }

                int 상한_가 = Method.상한가_하한가_구하기("상", 잔고.종목코드);
                if (int.Parse(para) > 상한_가) para = "주문불가";
            }

            return para;
        }

        public static bool 리밸감시_수익계산(Stockbalance 잔고, 감시주문 Item)
        {
            bool on = false;

            if (Item.리밸매도기준.Equals("평가수익률"))
            {
                if (Item.차수주문값 <= 잔고.수익률)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("기준수익률"))
            {
                if (Item.차수주문값 <= 잔고.기준수익률)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("손절매도수익률"))
            {
                if (Item.손절주문가격 >= 잔고.현재가)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("손절평가수익률"))
            {
                if (Item.차수주문값 >= 잔고.수익률)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("손절기준수익률"))
            {
                if (Item.차수주문값 >= 잔고.기준수익률)
                {
                    on = true;
                }
            }
            else
            {
                long 매수기준금 = GenieConfig.MT_buying_standard;
                double 차수주문값 = Item.차수주문값 * 10000;
                if (Item.단위_기준) 차수주문값 = 매수기준금 * (double)Item.차수주문값 / 100;

                if (Item.리밸매도기준.Equals("평가손익금"))
                {
                    if (차수주문값 <= 잔고.평가손익)
                    {
                        on = true;
                    }
                }
                else if (Item.리밸매도기준.Equals("예상손익금"))
                {
                    if (차수주문값 <= 잔고.예상손익 && 잔고.평가손익 > 0 && 잔고.수익률 > 0)
                    {
                        on = true;
                    }
                }
            }
            return on;
        }

        public static bool 미리주문_수익계산(Stockbalance 잔고, 감시주문 Item)
        {
            bool on = false;
            double 조정값 = -0.5;
            if (잔고.현재가 >= 20000) 조정값 = -1;

            if (Item.리밸매도기준.Equals("평가수익률"))
            {
                if (Item.차수주문값 + 조정값 <= 잔고.수익률)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("기준수익률"))
            {
                if (Item.차수주문값 + 조정값 <= 잔고.기준수익률)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("손절매도수익률"))
            {
                if (Item.손절주문가격 >= 잔고.현재가)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("손절평가수익률"))
            {
                if (Item.차수주문값 >= 잔고.수익률)
                {
                    on = true;
                }
            }
            else if (Item.리밸매도기준.Equals("손절기준수익률"))
            {
                if (Item.차수주문값 >= 잔고.기준수익률)
                {
                    on = true;
                }
            }
            else
            {
                long 매수기준금 = GenieConfig.MT_buying_standard;
                double 차수주문값 = Item.차수주문값 * 10000;
                if (Item.단위_기준) 차수주문값 = 매수기준금 * (double)Item.차수주문값 / 100;

                if (Item.리밸매도기준.Equals("평가손익금"))
                {
                    if (차수주문값 <= 잔고.평가손익)
                    {
                        on = true;
                    }
                }
                else if (Item.리밸매도기준.Equals("예상손익금"))
                {
                    if (차수주문값 <= 잔고.예상손익 && 잔고.평가손익 > 0 && 잔고.수익률 > 0)
                    {
                        on = true;
                    }
                }
            }
            return on;
        }

        public static void 최종주문가_매도체결(감시주문 감시)
        {
            // 1. 수익구분 7이 아니면 칼같이 리턴 (CPU 보호)
            if (감시.수익구분 != 7) return;

            // 2. [안전+최적화] 검색식이 비어있거나, 2차 주문이면 여기서 즉시 컷!
            // string.IsNullOrEmpty를 먼저 써서 아까처럼 Null 에러로 뻗는 현상을 완벽 방지합니다.
            if (string.IsNullOrEmpty(감시.검색식) || 감시.검색식.Contains("_2차")) return;

            // 3. 딕셔너리에서 해당 종목 찾기 (가장 빠른 탐색)
            if (Form1.최종매입가_List.TryGetValue(감시.종목코드, out List<최종매입가> 해당종목_리스트))
            {
                bool 삭제가_발생했음 = false; // [핵심] 하드디스크 보호용 스위치

                lock (해당종목_리스트)
                {
                    // 4. 삭제할 아이템 찾기
                    var 삭제할_아이템 = 해당종목_리스트.FirstOrDefault(o =>
                        o.번호 == 감시.최종번호 &&
                        감시.검색식.Contains(o.위치)
                    );

                    // 5. 찾았을 때만 메모리에서 삭제하고 스위치를 켬
                    if (삭제할_아이템 != null)
                    {
                        해당종목_리스트.Remove(삭제할_아이템);
                        삭제가_발생했음 = true; // 지운 게 있을 때만 true로 변경
                    }
                }

                // 6. [저사양 PC 핵심 최적화] 무의미한 파일 I/O(입출력) 차단
                // 기존에는 지울 게 없어도 매번 파일을 덮어써서 컴퓨터를 버벅이게 만들었습니다.
                // 이제는 '진짜로 데이터가 삭제되었을 때 딱 1번만' 하드디스크에 저장합니다.
                if (삭제가_발생했음)
                {
                    SaveToFile.최종매입가_파일저장(Form1.로딩완료);
                }
            }
        }

        public static void 감시주문동기화()
        {
            if (stockBalanceList.Count > 0)
            {
                // 1. 데이터가 이미 존재하는 경우 (잔고에 없는 종목 정리)
                if (Form1.최종매입가_List.Count > 0)
                {
                    // 딕셔너리의 키(종목코드) 목록만 복사해와서 순회 (삭제 시 오류 방지)
                    foreach (string 종목코드 in Form1.최종매입가_List.Keys.ToList())
                    {
                        // 잔고에 해당 종목이 없다면?
                        if (!Form1.stockBalanceList.ContainsKey(종목코드))
                        {
                            // 해당 종목의 모든 기록을 딕셔너리에서 제거
                            Form1.최종매입가_List.TryRemove(종목코드, out _);
                        }
                    }
                }
                // 2. 데이터가 없는 경우 (초기화)
                else
                {
                    // 잔고에 있는 모든 종목에 대해 초기 데이터 생성
                    foreach (var 잔고 in Form1.stockBalanceList.Values)
                    {
                        // [지니 최적화] Helper 함수를 사용하여 코드를 간결하게 만듦
                        // 내부에서 A~G까지 7개를 자동으로 추가해 줍니다.
                        // (잔고.시작가격이 '체결가' 파라미터로 들어갑니다)
                        Helper.최종매입가_신규추가(잔고.종목코드, 잔고.시작가격);
                    }
                }

                SaveToFile.최종매입가_파일저장(Form1.로딩완료);

                foreach (var 감시 in Form1.감시주문_List.Values)
                {
                    if (!stockBalanceList.ContainsKey(감시.종목코드))
                    {
                        string keyToRemove = 감시.감시번호.ToString();
                        Form1.감시주문_List.TryRemove(keyToRemove, out _);
                    }
                }
            }

            List<주문예약> 신규_List = Form1.form1.주문예약_List.FindAll(o => o.검색식.Contains("신규'매수'"));
            foreach (var item in 신규_List.ToList())
            {
                if (!Form1.stockBalanceList.ContainsKey(item.종목코드))
                {
                    REG.실시간시세등록(item.종목코드);
                }
                else
                {
                    주문예약 예약 = item;
                    Form1.form1.주문예약_List.Remove(예약);
                }
            }

            SaveToFile.주문예약_파일저장();
        }


        public static void 감시주문_확인(string POS)
        {
            form1.LB_JumunList.BeginUpdate();
            form1.LB_JumunList.Items.Clear();

            List<감시주문> 최종_List = 감시주문_List.Values
                                      .Where(o => o.수익구분 == 7)
                                      .ToList();
            form1.LB_JumunList.Items.Add(" 리밸런싱관리 감시주문 총개수: " + 감시주문_List.Count + " EA  일반감시: " + (감시주문_List.Count - 최종_List.Count) + " EA   최종매입가 감시주문: " + 최종_List.Count + " EA");
            if (form1.일반주문확인) form1.LB_JumunList.Items.Add("");
            else
            {
                form1.LB_JumunList.Items.Add(" ### 최종매입가 감시주문 (참조. 식 별로 차수가 나누어지며 순번이 낮을수록 앞 차수 입니다. 차수는 무한대로 늘어 납니다.) ###");
                form1.LB_JumunList.Items.Add("");
            }

            List<감시주문> 감시_List = 감시주문_List.Values.OrderBy(i => i.종목명).ToList();

            for (int i = 0; i < 감시_List.Count; i++)
            {
                감시주문 주문 = 감시_List[i];

                if (POS.Equals("일반주문"))
                {
                    if (중복확인(주문.종목명))
                    {
                        // 1. 전체 리스트 필터링
                        List<감시주문> List = 감시_List.FindAll(o => o.종목명.Equals(주문.종목명) && o.수익구분 != 7);

                        if (List.Count > 0)
                        {
                            // 2. 헤더 출력
                            string header = " ---------------------------------------------------- " + 주문.종목명 + " ----------------------------------------------------- ";
                            form1.LB_JumunList.Items.Add(header);
                            form1.LB_JumunList.Items.Add("");

                            long 매수기준금 = GenieConfig.MT_buying_standard;

                            // =========================================================
                            // [헬퍼 함수] 검색, 정렬, 출력을 한 번에 처리
                            // =========================================================
                            void ProcessAndPrint(string criteria, Func<감시주문, object> orderBySelector, bool descending, string valueFormat = "")
                            {
                                // 1. 검색
                                var filtered = List.FindAll(o => o.리밸매도기준.Equals(criteria));
                                if (filtered.Count == 0) return;

                                // 2. 정렬
                                var sorted = descending
                                    ? filtered.OrderByDescending(orderBySelector).ToList()
                                    : filtered.OrderBy(orderBySelector).ToList();

                                // 3. 출력
                                foreach (var item in sorted)
                                {
                                    string displayValue = "";

                                    // 값 포맷팅 로직 통합
                                    if (criteria.Contains("손익금")) // 평가손익금, 예상손익금
                                    {
                                        double val = item.차수주문값;
                                        if (item.단위_기준) val = 매수기준금 * (double)item.차수주문값 / 100 / 10000;
                                        displayValue = val.ToString("N2") + valueFormat; // " 만(평가손익)"
                                    }
                                    else if (criteria.Contains("수익률")) // 각종 수익률
                                    {
                                        // [주의] 기존 코드에서 주문구분2(평가수익률)일 때만 수익구분 != 7 체크가 있었는데,
                                        // 맨 위에서 이미 전체 필터링(수익구분 != 7)을 했으므로 중복 체크 제거함.

                                        if (criteria == "매도수익률" || criteria == "이익수익률" || criteria == "손절매도수익률")
                                        {
                                            // 감시주문가격 또는 손절주문가격 사용
                                            double price = (criteria == "손절매도수익률") ? item.손절주문가격 : item.감시주문가격;
                                            displayValue = (criteria == "손절매도수익률" ? "손절값: " : (criteria == "이익수익률" ? "이익감시값: " : "감시값: ")) + price.ToString("N0");
                                        }
                                        else
                                        {
                                            // 차수주문값 사용 (평가, 기준, 손절평가, 손절기준)
                                            displayValue = item.차수주문값.ToString() + valueFormat; // "%(평가수익률)"
                                        }
                                    }

                                    // 기록 함수 호출
                                    기록(item.감시일, item.검색식, displayValue,
                                         item.주문수량.ToString("N0"), item.원주문번호, item.주문시간,
                                         item.연동감시번호.ToString(), item.감시번호.ToString(),
                                         item.TS.ToString(), item.TS_high.ToString());
                                }
                            }

                            // 3. 통합 실행 (순서대로 호출)

                            // 1) 매도수익률 (오름차순, 감시주문가격)
                            ProcessAndPrint("매도수익률", o => o.감시주문가격, false);

                            // 6) 이익수익률 (오름차순, 감시주문가격)
                            ProcessAndPrint("이익수익률", o => o.감시주문가격, false);

                            // 7) 손절매도수익률 (내림차순, 손절주문가격)
                            ProcessAndPrint("손절매도수익률", o => o.손절주문가격, true);

                            // 4) 평가손익금 (오름차순, 차수주문값)
                            ProcessAndPrint("평가손익금", o => o.차수주문값, false, " 만(평가손익)");

                            // 5) 예상손익금 (오름차순, 차수주문값)
                            ProcessAndPrint("예상손익금", o => o.차수주문값, false, " 만(예상손익)");

                            // 2) 평가수익률 (오름차순, 차수주문값)
                            ProcessAndPrint("평가수익률", o => o.차수주문값, false, "%(평가수익률)");

                            // 3) 기준수익률 (오름차순, 차수주문값)
                            ProcessAndPrint("기준수익률", o => o.차수주문값, false, "%(기준수익률)");

                            // 8) 손절평가수익률 (내림차순, 차수주문값)
                            ProcessAndPrint("손절평가수익률", o => o.차수주문값, true, "%(손절평가수익률)");

                            // 9) 손절기준수익률 (내림차순, 차수주문값)
                            ProcessAndPrint("손절기준수익률", o => o.차수주문값, true, "%(손절기준수익률)");

                            form1.LB_JumunList.Items.Add("");
                        }

                        void 기록(string 일, string 식, string 값, string 주문수, string 원주문번호, string 주문_, string 연동, string 감시번호, string TS, string TS_high)
                        {
                            form1.LB_JumunList.Items.Add(" 감시일: " + 일 + " 식: " + 식 + " 주문: " + 주문_ + " " + 값 + " = " + 주문수 + " 연동: " + 연동 + " 감시번호: " + 감시번호 + " TS: " + TS + " TS_higt: " + TS_high + " 주 주문번호:" + 원주문번호);
                        }
                    }
                }
                else
                {
                    if (중복확인(주문.종목명))
                    {
                        List<감시주문> List = 감시주문_List.Values
                                             .Where(o => o.종목명.Equals(주문.종목명) && o.수익구분 == 7)
                                             .ToList();

                        if (List.Count > 0)
                        {
                            // 1. 헤더 출력
                            string header = " ---------------------------------------------------- " + 주문.종목명 + " ----------------------------------------------------- ";
                            form1.LB_JumunList.Items.Add(header);
                            form1.LB_JumunList.Items.Add("");

                            // =========================================================
                            // [헬퍼 함수 1] 개별 아이템 기록 출력 (최종기록 호출)
                            // =========================================================
                            void PrintItem(감시주문 item)
                            {
                                최종기록(item.주문체결가격, item.감시일, item.검색식, 감시값확인(item),
                                         item.최종번호.ToString(), item.주문수량.ToString("N0"),
                                         item.원주문번호, item.주문시간,
                                         item.연동감시번호.ToString(), item.감시번호.ToString(),
                                         item.TS.ToString(), item.TS_high.ToString());
                            }

                            // =========================================================
                            // [헬퍼 함수 2] 특정 그룹(A, B...)의 1차/2차 찾기, 정렬, 출력 통합
                            // =========================================================
                            void ProcessGroup(string suffix)
                            {
                                // 1. 검색 및 정렬 (1차, 2차)
                                var list1 = List.Where(o => o.검색식.Contains($"리밸_{suffix}_1차")).OrderBy(n => n.최종번호).ToList();
                                var list2 = List.Where(o => o.검색식.Contains($"리밸_{suffix}_2차")).OrderBy(n => n.최종번호).ToList();

                                // 2. 리스트 순회하며 출력 (PrintItem 호출)
                                foreach (var item in list1) PrintItem(item);
                                foreach (var item in list2) PrintItem(item);

                                // 3. 데이터가 있었으면 공백 줄 추가 (기존 로직 유지)
                                if (list1.Count > 0 || list2.Count > 0)
                                {
                                    form1.LB_JumunList.Items.Add("");
                                }
                            }

                            // =========================================================
                            // [실행] A ~ G 순차 처리
                            // =========================================================
                            string[] targets = { "A", "B", "C", "D", "E", "F", "G" };

                            foreach (var target in targets)
                            {
                                ProcessGroup(target);
                            }
                        }

                        void 최종기록(int 매입가, string 일, string 식, string 값, string 최종번호, string 주문수, string 원주문번호, string 주문_, string 연동, string 감시번호, string TS, string TS_high)
                        {
                            form1.LB_JumunList.Items.Add(" 감시일: " + 일 + " 식: " + 식 + " 차수:" + 최종번호 + " 매입가:" + 매입가 + " 주문: " + 주문_ + " " + 값 + " = " + 주문수 + " 연동: " + 연동 + " 감시번호: " + 감시번호 + " TS: " + TS + " TS_higt: " + TS_high + " 주 주문번호:" + 원주문번호);
                        }

                        string 감시값확인(감시주문 감시Item)
                        {
                            string 감시값 = "";

                            // 💡 [디버깅 & 방어막] 리밸매도기준이 아예 비어있으면 뻗지 말고 로그를 남긴 후 빠져나갑니다.
                            if (string.IsNullOrEmpty(감시Item.리밸매도기준))
                            {
                                // 누가 Null을 달고 들어왔는지 범인 몽타주 출력!
                                Form1.Console_print($"[디버깅 🚨] {감시Item.종목명} ({감시Item.검색식})의 '리밸매도기준'이 NULL입니다!");
                                return "기준없음(Null)";
                            }

                            long 매수기준금 = GenieConfig.MT_buying_standard;
                            double Velue = 감시Item.차수주문값;
                            if (감시Item.단위_기준) Velue = 매수기준금 * (double)감시Item.차수주문값 / 100 / 10000;

                            // 💡 .Equals() 대신 == 를 쓰면 Null이 들어와도 에러가 나지 않습니다.
                            if (감시Item.리밸매도기준 == "매도수익률") 감시값 = "감시가격:" + 감시Item.감시주문가격;
                            else if (감시Item.리밸매도기준 == "평가수익률") 감시값 = 감시Item.차수주문값.ToString() + "%(평가수익률)";
                            else if (감시Item.리밸매도기준 == "기준수익률") 감시값 = 감시Item.차수주문값.ToString() + "%(기준수익률)";
                            else if (감시Item.리밸매도기준 == "평가손익금") 감시값 = Velue.ToString("N2") + " 만(평가손익)";
                            else if (감시Item.리밸매도기준 == "예상손익금") 감시값 = Velue.ToString("N2") + " 만(예상손익)";
                            else if (감시Item.리밸매도기준 == "이익수익률") 감시값 = "이익감시가격:" + 감시Item.감시주문가격;
                            else if (감시Item.리밸매도기준 == "손절매도수익률") 감시값 = "손절주문가격:" + 감시Item.손절주문가격;
                            else if (감시Item.리밸매도기준 == "손절평가수익률") 감시값 = 감시Item.차수주문값.ToString() + "%(손절평가수익률)";
                            else if (감시Item.리밸매도기준 == "손절기준수익률") 감시값 = 감시Item.차수주문값.ToString() + "%(손절기준수익률)";

                            return 감시값;
                        }


                    }
                }
            }

            form1.LB_JumunList.EndUpdate();

            bool 중복확인(string 종목)
            {
                bool 확인 = true;
                int 개수 = 0;

                for (int n = 0; n < form1.LB_JumunList.Items.Count; n++)
                {
                    if (form1.LB_JumunList.Items[n].ToString().Contains(종목))
                    {
                        개수++;
                    }
                }

                if (개수 > 0)
                {
                    확인 = false;
                }
                return 확인;
            }
        }




        private static int GET_감시번호()
        {
            int 감시번호 = 1;

            // [최적화] 현재 사용 중인 모든 '감시번호'를 HashSet으로 가져옵니다. (O(N))
            // ConcurrentDictionary.Values는 스냅샷을 제공하므로 스레드 안전합니다.
            var usedIds = new HashSet<int>(Form1.감시주문_List.Values.Select(o => o.감시번호));

            // 1부터 무한대로 돌면서 HashSet에 없는 번호를 찾습니다. (O(1) 조회)
            for (int i = 1; i < int.MaxValue; i++)
            {
                // 사용 중인 번호 목록에 i가 없으면 그 번호가 당첨!
                if (!usedIds.Contains(i))
                {
                    감시번호 = i;
                    break;
                }
            }

            return 감시번호;
        }

        private static int GET_연동감시번호()
        {
            int 감시번호 = 1; // 0번부터 시작하시려면 0으로 수정하세요 (기존 코드는 1부터 루프)

            // [최적화] 현재 사용 중인 모든 '연동감시번호'를 HashSet으로 가져옵니다.
            var usedIds = new HashSet<int>(Form1.감시주문_List.Values.Select(o => o.연동감시번호));

            for (int i = 1; i < int.MaxValue; i++)
            {
                if (!usedIds.Contains(i))
                {
                    감시번호 = i;
                    break;
                }
            }

            return 감시번호;
        }


        ///////////////            계좌 리밸런싱 관리              ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ///////////////                잔고청산                   ////////////////

        public static void Liquidation_condition(string ST_ID, string itemcode, string condition)
        {
            bool ID = false;

            string[] assign = { "A", "B", "C" };

            int[] settings = new int[] {
                 GenieConfig.CBB_Liquidation_use_condition_A,
                 GenieConfig.CBB_Liquidation_use_condition_B,
                 GenieConfig.CBB_Liquidation_use_condition_C
             };

            for (int i = 0; i < settings.Length; i++)
            {
                int settingValue = settings[i];
                string where = "리밸_" + assign[i];

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

            // [가정] Catch_Stock_List는 ConcurrentDictionary<string, Catch_stock> 타입입니다.
            // Catch_stock 클래스는 Liquidation_catch의 역할을 대신 수행한다고 가정합니다.

            void in_out(string where)
            {
                // 1. 조건 이름 체크
                if (condition.Equals(Form1.위치별검색식리스트[where].이름))
                {
                    // 2. 고유 키 생성 (O(1) 검색을 위한 핵심)
                    // 키는 'where' 값과 'itemcode'를 조합하여 생성합니다.
                    string key = where + itemcode;

                    if (ID) // 추가 조건 (종목이 리스트에 진입할 때)
                    {
                        // [최적화] TryAdd를 사용하여 키가 없으면 추가하고, 있으면 무시 (스레드 안전, O(1))
                        // 새로운 Catch_stock 객체를 생성하여 추가합니다.
                        // (Liquidation_catch 대신 Catch_stock 사용)
                        Catch_Stock_List.TryAdd(key, new Catch_stock(itemcode, 0));
                    }
                    else // 제거 조건 (종목이 리스트에서 나갈 때)
                    {
                        // [최적화] List.Find, List.Remove 대신 TryRemove를 사용한 O(1) 고속 제거
                        // 불필요한 ContainsKey 체크와 Find/Remove 연산을 모두 대체합니다.
                        Catch_Stock_List.TryRemove(key, out _);
                    }
                }
            }
        }

        public static async Task Liquidation_USE(Stockbalance 잔고)
        {
            if (Form1.재시작) return;

            if (잔고.잔고청산)
            {
                Market_Item market_Item = Form1.Market_Item_List[잔고.종목코드];

                long 매수기준금 = GenieConfig.MT_buying_standard;
                double 매입금1 = GenieConfig.TB_잔고청산_매입금1_A / 100 * 매수기준금;
                double 매입금2 = GenieConfig.TB_잔고청산_매입금2_A / 100 * 매수기준금;

                int start = GenieConfig.MTB_Liquidation_Starttime_A;
                int end = GenieConfig.MTB_Liquidation_Stoptime_A;
                int Repeat_use = GenieConfig.CBB_Liquidation_use_condition_A;
                double 주문값 = GenieConfig.TB_Liquidation_value_A;
                int 시장가구분 = GenieConfig.CBB_Liquidation_jumun_A;
                double suik_low = GenieConfig.TB_Liquidation_suik_1_A;
                double suik_height = GenieConfig.TB_Liquidation_suik_2_A;
                bool suik_choice = GenieConfig.CB_Liquidation_choice_A;
                int suik_gubun = GenieConfig.CBB_Liquidation_suik_gubun_A;
                double 비중 = GenieConfig.TB_Liquidation_sell_ratio_A;
                int 비중단위 = GenieConfig.CBB_Liquidation_sell_gubun_A;
                double maemae_low = GenieConfig.TB_Liquidation_maemae_1_A;
                double maemae_height = GenieConfig.TB_Liquidation_maemae_2_A;
                string group = GET.익절그룹("잔고청산_A");
                int 취소N주문 = GenieConfig.CBB_Liquidation_Cancel_A;
                int 취소시간 = GenieConfig.MTB_Liquidation_Cancel_time_A;
                int 반복시간 = GenieConfig.MTB_Liquidation_repeat_A;
                bool 단위_기준금 = GenieConfig.CB_Liquidation_기준금;
                bool 추매금지 = GenieConfig.CB_추매금지_Liquidation_A;
                bool 수익보전 = GenieConfig.CB_수익보전_Liquidation_A;
                bool 매도정지 = GenieConfig.CB_Liquidation_SellStop_A;
                bool 강제매도 = GenieConfig.CB_Liquidation_강제매도_A;
                bool TS = GenieConfig.CB_Liquidation_TS_A;
                int TS_high = 잔고.잔고청산_TS_high_A;
                double TS_down = GenieConfig.TB_Liquidation_TS_down_A;

                MAPeriod mma = Form1.Min_ma_list[잔고.종목코드];
                MAPeriod dma = Form1.Day_ma_list[잔고.종목코드];

                double min_mma = mma.Liquidation_MAValue_A;
                int CBB_이평 = GenieConfig.CBB_Liquidation_MinMAPeriod_A;

                double TS_min_ma = mma.Liquidation_TS_MAValue_A;
                double TS_day_ma = dma.Liquidation_TS_MAValue_A;
                int CBB_TS_mma = GenieConfig.CBB_Liquidation_TS_MinMAPeriod_A;
                int CBB_TS_dma = GenieConfig.CBB_Liquidation_TS_DayMAPeriod_A;

                string location = "잔고청산_A";
                string 검색식 = "";
                if (GenieConfig.CBB_Liquidation_use_condition_A > 0)
                    검색식 = Form1.위치별검색식리스트["청산_A"].이름;

                if (GenieConfig.CB_Liquidation_A && 잔고.가동_청산A && !잔고.잔고청산_A)
                {
                    if (!잔고.매도정지 || 강제매도)
                    {
                        if (Method.RunTime(start, end))
                        {
                            if (TS)
                            {
                                if (TS_high == 0)
                                {
                                 await    Run();
                                }
                                else
                                {
                                    if (잔고.잔고청산_TS_high_A < 잔고.현재가)
                                    {
                                        잔고.잔고청산_TS_high_A = 잔고.현재가;
                                    }
                                    else
                                    {
                                        if (수익보전)
                                        {
                                            if (잔고.수익률 > 0 && 잔고.예상손익 > 0)await  TS_check();
                                        }
                                        else
                                        {
                                         await    TS_check();
                                        }
                                    }
                                }
                            }
                            else
                            {
                              await   Run();
                            }

                            async Task Run()
                            {
                                long 총매입금 = 잔고.매입금액 + 잔고.신용_매입금액;

                                if (!잔고.잔고청산_매입금_A && 매입금1 <= 총매입금 && 총매입금 <= 매입금2) if (검사()) 잔고.잔고청산_매입금_A = true;

                                if (잔고.잔고청산_매입금_A && MA.Get_TS_이평(잔고, CBB_이평, min_mma))
                                {
                                    // 청산 위치는 "청산_A"로 고정되어 있습니다.
                                    const string Loc = "청산_A";

                                    if (Repeat_use > 0) // 검색식 사용 (Catch List를 통한 지연 조건 체크)
                                    {
                                        // 1. 매매 대상 종목의 순수한 종목 코드를 가져옵니다.
                                        string itemCode = 잔고.종목코드;

                                        // [최적화 1] ConcurrentDictionary의 O(1) 조회 키를 생성
                                        string key = Loc + itemCode;

                                        // [최적화 2] List.Find 대신 TryGetValue를 사용한 고속 검색 (O(1))
                                        // (Catch_stock 객체가 Liquidation_catch의 역할을 수행한다고 가정합니다.)
                                        if (Form1.Catch_Stock_List.TryGetValue(key, out Catch_stock item))
                                        {
                                            // 2. 딜레이 조건 확인
                                            if (GenieConfig.MTB_Liquidation_delay_A <= item.timer)
                                            {
                                                // 딜레이 조건 만족 시 매매 실행
                                                if (검사())await  매매진행();
                                            }
                                        }
                                    }
                                    else // 검색식 미사용 (지연 조건 체크 없이 즉시 실행)
                                    {
                                        if (검사())await  매매진행();
                                    }
                                }
                            }
                        }
                    }
                }

                if (GenieConfig.CB_Liquidation_B && 잔고.가동_청산B && !잔고.잔고청산_B)
                {
                    start = GenieConfig.MTB_Liquidation_Starttime_B;
                    end = GenieConfig.MTB_Liquidation_Stoptime_B;

                    if (Method.RunTime(start, end))
                    {
                        강제매도 = GenieConfig.CB_Liquidation_강제매도_B;
                        if (!잔고.매도정지 || 강제매도)
                        {
                            Repeat_use = GenieConfig.CBB_Liquidation_use_condition_B;
                            주문값 = GenieConfig.TB_Liquidation_value_B;
                            시장가구분 = GenieConfig.CBB_Liquidation_jumun_B;
                            suik_low = GenieConfig.TB_Liquidation_suik_1_B;
                            suik_height = GenieConfig.TB_Liquidation_suik_2_B;
                            suik_choice = GenieConfig.CB_Liquidation_choice_B;
                            suik_gubun = GenieConfig.CBB_Liquidation_suik_gubun_B;
                            비중 = GenieConfig.TB_Liquidation_sell_ratio_B;
                            비중단위 = GenieConfig.CBB_Liquidation_sell_gubun_B;
                            maemae_low = GenieConfig.TB_Liquidation_maemae_1_B;
                            maemae_height = GenieConfig.TB_Liquidation_maemae_2_B;
                            group = GET.익절그룹("잔고청산_B");
                            취소N주문 = GenieConfig.CBB_Liquidation_Cancel_B;
                            취소시간 = GenieConfig.MTB_Liquidation_Cancel_time_B;
                            반복시간 = GenieConfig.MTB_Liquidation_repeat_B;
                            추매금지 = GenieConfig.CB_추매금지_Liquidation_B;
                            수익보전 = GenieConfig.CB_수익보전_Liquidation_B;
                            매도정지 = GenieConfig.CB_Liquidation_SellStop_B;
                            TS = GenieConfig.CB_Liquidation_TS_B;
                            TS_down = GenieConfig.TB_Liquidation_TS_down_B;

                            min_mma = mma.Liquidation_MAValue_A;
                            CBB_이평 = GenieConfig.CBB_Liquidation_MinMAPeriod_B;

                            TS_min_ma = mma.Liquidation_TS_MAValue_B;
                            TS_day_ma = dma.Liquidation_TS_MAValue_B;
                            CBB_TS_mma = GenieConfig.CBB_Liquidation_TS_MinMAPeriod_B;
                            CBB_TS_dma = GenieConfig.CBB_Liquidation_TS_DayMAPeriod_B;

                            TS_high = 잔고.잔고청산_TS_high_B;
                            location = "잔고청산_B";
                            if (GenieConfig.CBB_Liquidation_use_condition_B > 0)
                                검색식 = Form1.위치별검색식리스트["청산_B"].이름;

                            if (TS)
                            {
                                if (TS_high == 0)
                                {
                                  await  Run();
                                }
                                else
                                {
                                    if (잔고.잔고청산_TS_high_B < 잔고.현재가)
                                    {
                                        잔고.잔고청산_TS_high_B = 잔고.현재가;
                                    }
                                    else
                                    {
                                        if (수익보전)
                                        {
                                            if (잔고.수익률 > 0 && 잔고.예상손익 > 0)await  TS_check();
                                        }
                                        else
                                        {
                                          await  TS_check();
                                        }
                                    }
                                }
                            }
                            else
                            {
                              await  Run();
                            }

                            async Task Run()
                            {
                                // 1. 매입금액 계산 (기존 로직 유지)
                                매입금1 = GenieConfig.TB_잔고청산_매입금1_B / 100.0 * 매수기준금;
                                매입금2 = GenieConfig.TB_잔고청산_매입금2_B / 100.0 * 매수기준금;
                                long 총매입금 = 잔고.매입금액 + 잔고.신용_매입금액;

                                // 2. 매입금액 조건 체크 (기존 로직 유지)
                                if (!잔고.잔고청산_매입금_B && 매입금1 <= 총매입금 && 총매입금 <= 매입금2)
                                {
                                    if (검사())
                                    {
                                        잔고.잔고청산_매입금_B = true;
                                    }
                                }

                                // 3. 청산 매매 조건 실행
                                if (잔고.잔고청산_매입금_B && MA.Get_TS_이평(잔고, CBB_이평, min_mma))
                                {
                                    if (Repeat_use > 0) // 검색식 사용 (지연 조건 체크 필요)
                                    {
                                        // 청산 위치는 "청산_B"로 고정
                                        const string loc = "청산_B";

                                        // 3-1. 종목 코드 및 O(1) 조회 키 생성
                                        string itemCode = 잔고.종목코드;
                                        string key = loc + itemCode;

                                        // [최적화] List.Find 대신 TryGetValue를 사용한 고속 검색 (O(1))
                                        // Liquidation_catch 대신 Catch_stock 사용
                                        if (Form1.Catch_Stock_List.TryGetValue(key, out Catch_stock item))
                                        {
                                            // Catch_stock 객체에 timer 속성이 있다고 가정하고 딜레이 조건 확인
                                            if (GenieConfig.MTB_Liquidation_delay_B <= item.timer)
                                            {
                                                if (검사())
                                                {
                                               await     매매진행();
                                                }
                                            }
                                        }
                                    }
                                    else // 검색식 미사용 (지연 조건 체크 없이 즉시 실행)
                                    {
                                        if (검사())
                                        {
                                          await  매매진행();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (GenieConfig.CB_Liquidation_C && 잔고.가동_청산C && !잔고.잔고청산_C)
                {
                    start = GenieConfig.MTB_Liquidation_Starttime_C;
                    end = GenieConfig.MTB_Liquidation_Stoptime_C;

                    if (Method.RunTime(start, end))
                    {
                        강제매도 = GenieConfig.CB_Liquidation_강제매도_C;
                        if (!잔고.매도정지 || 강제매도)
                        {
                            Repeat_use = GenieConfig.CBB_Liquidation_use_condition_C;
                            주문값 = GenieConfig.TB_Liquidation_value_C;
                            시장가구분 = GenieConfig.CBB_Liquidation_jumun_C;
                            suik_low = GenieConfig.TB_Liquidation_suik_1_C;
                            suik_height = GenieConfig.TB_Liquidation_suik_2_C;
                            suik_choice = GenieConfig.CB_Liquidation_choice_C;
                            suik_gubun = GenieConfig.CBB_Liquidation_suik_gubun_C;
                            비중 = GenieConfig.TB_Liquidation_sell_ratio_C;
                            비중단위 = GenieConfig.CBB_Liquidation_sell_gubun_C;
                            maemae_low = GenieConfig.TB_Liquidation_maemae_1_C;
                            maemae_height = GenieConfig.TB_Liquidation_maemae_2_C;
                            group = GET.익절그룹("잔고청산_C");
                            취소N주문 = GenieConfig.CBB_Liquidation_Cancel_C;
                            취소시간 = GenieConfig.MTB_Liquidation_Cancel_time_C;
                            반복시간 = GenieConfig.MTB_Liquidation_repeat_C;
                            추매금지 = GenieConfig.CB_추매금지_Liquidation_C;
                            수익보전 = GenieConfig.CB_수익보전_Liquidation_C;
                            매도정지 = GenieConfig.CB_Liquidation_SellStop_C;
                            TS = GenieConfig.CB_Liquidation_TS_C;
                            TS_down = GenieConfig.TB_Liquidation_TS_down_C;

                            min_mma = mma.Liquidation_MAValue_C;
                            CBB_이평 = GenieConfig.CBB_Liquidation_MinMAPeriod_C;

                            TS_min_ma = mma.Liquidation_TS_MAValue_C;
                            TS_day_ma = dma.Liquidation_TS_MAValue_C;
                            CBB_TS_mma = GenieConfig.CBB_Liquidation_TS_MinMAPeriod_C;
                            CBB_TS_dma = GenieConfig.CBB_Liquidation_TS_DayMAPeriod_C;

                            TS_high = 잔고.잔고청산_TS_high_C;

                            location = "잔고청산_C";

                            if (GenieConfig.CBB_Liquidation_use_condition_C > 0)
                                검색식 = Form1.위치별검색식리스트["청산_C"].이름;

                            if (TS)
                            {
                                if (TS_high == 0)
                                {
                               await     Run();
                                }
                                else
                                {
                                    if (잔고.잔고청산_TS_high_C < 잔고.현재가)
                                    {
                                        잔고.잔고청산_TS_high_C = 잔고.현재가;
                                    }
                                    else
                                    {
                                        if (수익보전)
                                        {
                                            if (잔고.수익률 > 0 && 잔고.예상손익 > 0) await TS_check();
                                        }
                                        else
                                        {
                                    await        TS_check();
                                        }
                                    }
                                }
                            }
                            else
                            {
                              await  Run();
                            }

                            async Task Run()
                            {
                                매입금1 = GenieConfig.TB_잔고청산_매입금1_C / 100 * 매수기준금;
                                매입금2 = GenieConfig.TB_잔고청산_매입금2_C / 100 * 매수기준금;
                                long 총매입금 = 잔고.매입금액 + 잔고.신용_매입금액;
                                if (!잔고.잔고청산_매입금_C && 매입금1 <= 총매입금 && 총매입금 <= 매입금2) if (검사()) 잔고.잔고청산_매입금_C = true;

                                if (잔고.잔고청산_매입금_C && MA.Get_TS_이평(잔고, CBB_이평, min_mma))
                                {
                                    if (Repeat_use > 0) // 검색식 사용 
                                    {
                                        // 청산 위치는 "청산_C"로 고정
                                        const string loc = "청산_C";

                                        // 1. 매매 대상 종목의 순수한 종목 코드를 가져옵니다.
                                        string itemCode = 잔고.종목코드;

                                        // [최적화 1] ConcurrentDictionary의 O(1) 조회 키를 생성
                                        string key = loc + itemCode;

                                        // [최적화 2] List.Find 대신 TryGetValue를 사용한 고속 검색 (O(1))
                                        // (Liquidation_catch 대신 Catch_stock 사용)
                                        if (Form1.Catch_Stock_List.TryGetValue(key, out Catch_stock item))
                                        {
                                            // 2. 딜레이 조건 확인 (Catch_stock 객체에 timer 속성이 있다고 가정)
                                            if (GenieConfig.MTB_Liquidation_delay_C <= item.timer)
                                            {
                                                if (검사())
                                                {
                                                  await  매매진행();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (검사())await 매매진행();
                                    }
                                }
                            }
                        }
                    }
                }

                bool 검사()
                {
                    bool result = false;

                    if (group.Contains(GET.그룹변환(잔고.매매그룹)) && Tab_InterestGroup.관심그룹확인(location, 잔고.종목코드))
                    {
                        if (!suik_choice) // →
                        {
                            if (Method.수익범위(false, 단위_기준금, 잔고, suik_low, suik_height, suik_gubun, location))
                            {
                                result = true;
                            }
                        }
                        else // ⇒
                        {
                            switch (location)
                            {
                                case "잔고청산_A":
                                    잔고.청산A = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.청산A, "A", false);
                                    if (잔고.청산A.Equals("X"))
                                    {
                                        result = true;
                                    }
                                    break;

                                case "잔고청산_B":
                                    잔고.청산B = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.청산B, "B", false);
                                    if (잔고.청산B.Equals("X"))
                                    {
                                        result = true;
                                    }
                                    break;
                                case "잔고청산_C":
                                    잔고.청산C = Method.조건범위(단위_기준금, 잔고.등락율, 잔고.수익률, 잔고.평가손익, 잔고.예상손익, suik_low, suik_height, suik_gubun, 잔고.청산C, "C", false);
                                    if (잔고.청산C.Equals("X"))
                                    {
                                        result = true;
                                    }
                                    break;
                            }
                        }
                    }

                    return result;
                }

                async Task 매매진행()
                {
                    if (강제매도) 잔고.매도기준update = true;
                    if (Method.청산주문_매매범위(잔고, maemae_low, maemae_height))
                    {
                        if (수익보전)
                        {
                            if (잔고.수익률 > 0 && 잔고.예상손익 > 0) await 매매진행_();
                        }
                        else
                        {
                      await      매매진행_();
                        }

                        async Task 매매진행_()
                        {
                            if (!잔고.추매정지 && 추매금지) 잔고.추매정지 = true;

                            if (잔고.추매정지)
                            {
                                if (!Form1.form1.추매거부로그_List.Contains(잔고.종목명))
                                {
                                    Log.동작기록("[잔고 추매금지] " + location + " 동작하여 " + 잔고.종목명 + " 추가매수 가 정지 됩니다. ");
                                    Log.에러기록(" ");
                                    Log.에러기록("[잔고 추매금지] " + location + " 동작하여 " + 잔고.종목명 + " 추가매수 정지 됩니다. ");
                                    Log.에러기록(" ");

                                    Form1.form1.추매거부로그_List.Add(잔고.종목명);
                                }

                            }
                            if (TS)
                            {
                                switch (location)
                                {
                                    case "잔고청산_A":
                                        잔고.잔고청산_TS_high_A = 잔고.현재가;
                                        break;
                                    case "잔고청산_B":
                                        잔고.잔고청산_TS_high_B = 잔고.현재가;
                                        break;
                                    case "잔고청산_C":
                                        잔고.잔고청산_TS_high_C = 잔고.현재가;
                                        break;
                                }
                            }
                            else
                            {
                                List<JumunItem> 조건맞는_주문리스트 = Form1.JumunItem_List.Values
                                    .Where(개별주문 => 개별주문.종목코드 == 잔고.종목코드 && !개별주문.검색식.Contains("잔고청산"))
                                    .ToList();

                                if (조건맞는_주문리스트.Count > 0)
                                {
                                    잔고.매매가능 = false;

                                    foreach (JumunItem 취소할주문 in 조건맞는_주문리스트)
                                    {
                                        취소할주문.반복횟수 = 0;
                                        취소할주문.취소시간 = 0;
                                        취소할주문.취소timer = 0;
                                        취소할주문.비고 = "잔고청산_미체결일괄 '취소'";
                                    }
                                }
                                else
                                {
                                    잔고.매도기준update = false;
                                    잔고.매도기준 = 잔고.보유수량;

                               await     Order_Run("");

                                    잔고.매매가능 = true;
                                    if (location.Equals("잔고청산_A")) 잔고.잔고청산_A = true;
                                    if (location.Equals("잔고청산_B")) 잔고.잔고청산_B = true;
                                    if (location.Equals("잔고청산_C")) 잔고.잔고청산_C = true;

                                    if (매도정지)
                                    {
                                        잔고.매도정지 = true;

                                        if (!Form1.form1.매도거부로그_List.Contains(잔고.종목명))
                                        {
                                            Log.동작기록("[잔고 매도정지] " + location + " 동작하여 " + 잔고.종목명 + " 매도가 정지 됩니다. ");
                                            Log.에러기록(" ");
                                            Log.에러기록("[잔고 매도정지] " + location + " 동작하여 " + 잔고.종목명 + " 매도가 정지 됩니다. ");
                                            Log.에러기록(" ");

                                            Form1.form1.매도거부로그_List.Add(잔고.종목명);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                async Task TS_check()
                {
                    double 주문비 = ((double)잔고.현재가 - (double)TS_high) / (double)TS_high * (double)100;
                    if (TS_down >= 주문비)
                    {
                        if (MA.Get_TS_이평(잔고, CBB_TS_mma, TS_min_ma) && MA.Get_TS_이평(잔고, CBB_TS_dma, TS_day_ma))
                        {
                            int 주문가 = Method.Order_price(주문값, 시장가구분, market_Item.종목코드, 잔고.현재가); // 주문가격
                            int Tik_cap = Method.Find_Tik_Cap(잔고.현재가, 주문가, 잔고.시장);
                            if (Tik_cap <= 15)
                            {
                                List<JumunItem> 조건맞는_주문리스트 = Form1.JumunItem_List.Values
                                    .Where(개별주문 => 개별주문.종목코드 == 잔고.종목코드 && !개별주문.검색식.Contains("잔고청산"))
                                    .ToList();

                                if (조건맞는_주문리스트.Count > 0)
                                {
                                    잔고.매매가능 = false;

                                    foreach (JumunItem 취소할주문 in 조건맞는_주문리스트)
                                    {
                                        취소할주문.반복횟수 = 0;
                                        취소할주문.취소시간 = 0;
                                        취소할주문.취소timer = 0;
                                        취소할주문.비고 = "잔고청산_미체결일괄 '취소'";
                                    }
                                }
                                else
                                {
                                    잔고.매도기준update = false;
                                    잔고.매도기준 = 잔고.보유수량;

                               await     Order_Run(" TS");

                                    잔고.매매가능 = true;
                                    if (location.Equals("잔고청산_A")) 잔고.잔고청산_A = true;
                                    if (location.Equals("잔고청산_B")) 잔고.잔고청산_B = true;
                                    if (location.Equals("잔고청산_C")) 잔고.잔고청산_C = true;
                                }
                            }
                            else
                            {
                                switch (location)
                                {
                                    case "잔고청산_A":
                                        잔고.잔고청산_TS_high_A = 0;
                                        break;
                                    case "잔고청산_B":
                                        잔고.잔고청산_TS_high_B = 0;
                                        break;
                                    case "잔고청산_C":
                                        잔고.잔고청산_TS_high_C = 0;
                                        break;
                                }
                            }
                        }
                    }
                }

                async Task Order_Run(string _TS)
                {
                    int 주문가 = Method.Order_price(주문값, 시장가구분, market_Item.종목코드, 잔고.현재가); // 주문가격
                    int 예_주문가 = 주문가;

                    if (시장가구분 == 0) 예_주문가 = 잔고.현재가;

                    int 주문수량 = Method.주문수량계산(잔고, 예_주문가, 비중, 비중단위);

                    while (true)
                    {
                        bool off = true;

                        if (Method.청산주문_매매범위(잔고, maemae_low, maemae_height))
                        {
                            if (시장가구분 < 4)
                            {
                                if (ExecuteTrade.잔고주문_오더(잔고, location + " [" + 검색식 + "]" + _TS, 2, 비중, 비중단위, 주문값, 시장가구분, 취소시간, 취소N주문, 반복시간, "", location, suik_gubun, true, maemae_low))
                                {
                                    청산가동();
                                    off = false;
                                }
                            }
                            else
                            {
                                if ( await 분할주문(location, 2, 시장가구분, 잔고.종목코드, 잔고.종목명, 주문수량, 잔고.현재가, location + " [" + 검색식 + "]", 취소시간))
                                {
                                    청산가동();
                                    off = false;
                                }
                            }
                        }

                        if (off) break;
                    }

                    void 청산가동()
                    {
                        // 1. 공통 변수 선언
                        int targetTime = 0;
                        string liquidationKeyPrefix = ""; // Catch_Stock_List에서 제거할 키 접두사 (예: "Liquidation_A")
                        bool cleanupCatchList = false;

                        // 2. 설정값 가져오기 및 플래그 초기화
                        switch (location)
                        {
                            case "잔고청산_A":
                                targetTime = GenieConfig.MT_Liquidation_repeat_time_A;
                                Get.TT_Liqu_time_A = targetTime;
                                잔고.가동_청산A = false;
                                liquidationKeyPrefix = "Liquidation_A";
                                cleanupCatchList = true; // Catch_Stock_List 정리 로직 활성화
                                break;

                            case "잔고청산_B":
                                targetTime = GenieConfig.MT_Liquidation_repeat_time_B;
                                Get.TT_Liqu_time_B = targetTime;
                                잔고.가동_청산B = false;
                                liquidationKeyPrefix = "Liquidation_B";
                                cleanupCatchList = true;
                                break;

                            case "잔고청산_C":
                                targetTime = GenieConfig.MT_Liquidation_repeat_time_C;
                                Get.TT_Liqu_time_C = targetTime;
                                잔고.가동_청산C = false;
                                liquidationKeyPrefix = "Liquidation_C";
                                cleanupCatchList = true;
                                break;
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


                        // ----------------------------------------------------------------------
                        // [핵심 최적화 2] Catch_Stock_List 정리 (불필요한 ContainsKey 제거)
                        // ----------------------------------------------------------------------

                        if (cleanupCatchList)
                        {
                            string key = liquidationKeyPrefix + 잔고.종목코드;

                            // ConcurrentDictionary의 TryRemove는 키가 없어도 오류 없이 안전하게 false를 반환합니다.
                            // 따라서 ContainsKey 체크가 필요 없으므로 CPU 검색 횟수를 줄입니다.
                            Catch_Stock_List.TryRemove(key, out _);
                        }
                    }

                }
            }
        }


        ///////////////               잔고 자동청산                ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////           수익금기준손절 매매             ////////////////

        public static void 수익금기준손절_주문취소(string 검색식)
        {
            List<JumunItem> filteredItems = Form1.JumunItem_List.Values
            .Where(o => o.검색식.Equals(검색식))
            .ToList();

            foreach (JumunItem item in filteredItems)
            {
                // JumunItem 객체의 속성을 0으로 초기화합니다.
                item.반복횟수 = 0;
                item.취소시간 = 0;
                item.취소timer = 0;
            }
        }

        public static void 수익금기준손절()
        {
            long 매수기준금 = GenieConfig.MT_buying_standard;
            int Cut_time = GenieConfig.MTB_cut_time_A;
            double cut_수익금1 = GenieConfig.TB_cut_수익금1_A * 10000;
            double cut_수익금2 = GenieConfig.TB_cut_수익금2_A * 10000;
            double Cut_매입금 = GenieConfig.TB_cut_won_A * 10000;

            if (GenieConfig.CB_cut_기준금)
            {
                cut_수익금1 = GenieConfig.TB_cut_수익금1_A / 100 * 매수기준금;
                cut_수익금2 = GenieConfig.TB_cut_수익금2_A / 100 * 매수기준금;
                Cut_매입금 = GenieConfig.TB_cut_won_A / 100 * 매수기준금;
            }

            double Cut_수익율 = GenieConfig.TB_cut_P_A;
            double Cut_비중 = GenieConfig.TB_cut_ratio_A;
            int Cut_비중선택 = GenieConfig.CBB_cut_gubun_A;
            double Cut_주문값 = GenieConfig.TB_cut_value_A;
            int Cut_매수매도 = GenieConfig.CBB_cut_jumun_A;
            int Cut_취소시간 = GenieConfig.MTB_cut_cansel_time_A;
            string 익절그룹 = "Cut_A";
            string 검색식 = "수익금손절_A";

            if (!Form1.form1.Cut_A && !Form1.form1.Cut_B && !Form1.form1.Cut_C &&
                 str.cut_LB_A.Equals("X") && str.cut_LB_B.Equals("X") && str.cut_LB_C.Equals("X"))
            {
                Get.실현손익_시작 = Form1.Acc.실현손익;
                Get.실현손익_예상 = Form1.Acc.실현손익;
            }

            if (GenieConfig.CB_cut_A)
            {
                if (Get.TimeNow >= Cut_time)
                {
                    if (cut_수익금1 <= Get.실현손익_시작 && Get.실현손익_시작 < cut_수익금2 && str.cut_LB_A.Equals("X"))
                    {
                        Get.Cut_남길금액_A = Get.실현손익_예상 * GenieConfig.TB_cut_남길퍼_A / 100;
                        Form1.form1.Cut_A = true;
                        str.cut_LB_A = "O";
                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.CB_cut_LB_A.Text = "O";
                    }

                    if (Form1.form1.Cut_A)
                    {
                        if (Get.실현손익_예상 > Get.Cut_남길금액_A) 주문실행();

                        if (Form1.Acc.실현손익 <= Get.Cut_남길금액_A)
                        {
                            수익금기준손절_주문취소("수익금손절_A");
                            Form1.form1.Cut_A = false;
                            매도재개();
                        }
                    }
                }
            }

            if (GenieConfig.CB_cut_B)
            {
                Cut_time = GenieConfig.MTB_cut_time_B;
                cut_수익금1 = GenieConfig.TB_cut_수익금1_B * 10000;
                cut_수익금2 = GenieConfig.TB_cut_수익금2_B * 10000;
                Cut_매입금 = GenieConfig.TB_cut_won_B * 10000;

                if (GenieConfig.CB_cut_기준금)
                {
                    cut_수익금1 = GenieConfig.TB_cut_수익금1_B / 100 * 매수기준금;
                    cut_수익금2 = GenieConfig.TB_cut_수익금2_B / 100 * 매수기준금;
                    Cut_매입금 = GenieConfig.TB_cut_won_B / 100 * 매수기준금;
                }
                Cut_수익율 = GenieConfig.TB_cut_P_B;
                Cut_비중 = GenieConfig.TB_cut_ratio_B;
                Cut_비중선택 = GenieConfig.CBB_cut_gubun_B;
                Cut_주문값 = GenieConfig.TB_cut_value_B;
                Cut_매수매도 = GenieConfig.CBB_cut_jumun_B;
                Cut_취소시간 = GenieConfig.MTB_cut_cansel_time_B;
                익절그룹 = "Cut_B";
                검색식 = "수익금손절_B";

                if (Get.TimeNow >= Cut_time)
                {
                    if (cut_수익금1 <= Get.실현손익_시작 && Get.실현손익_시작 < cut_수익금2 && str.cut_LB_B.Equals("X"))
                    {
                        Get.Cut_남길금액_B = Get.실현손익_예상 * GenieConfig.TB_cut_남길퍼_B / 100;
                        Form1.form1.Cut_B = true;
                        str.cut_LB_B = "O";
                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.CB_cut_LB_B.Text = "O";
                    }

                    if (Form1.form1.Cut_B)
                    {
                        if (Get.실현손익_예상 > Get.Cut_남길금액_B) 주문실행();

                        if (Form1.Acc.실현손익 <= Get.Cut_남길금액_B)
                        {
                            수익금기준손절_주문취소("수익금손절_B");
                            Form1.form1.Cut_B = false;
                            매도재개();
                        }
                    }
                }
            }

            if (GenieConfig.CB_cut_C)
            {
                Cut_time = GenieConfig.MTB_cut_time_C;
                cut_수익금1 = GenieConfig.TB_cut_수익금1_C * 10000;
                cut_수익금2 = GenieConfig.TB_cut_수익금2_C * 10000;
                Cut_매입금 = GenieConfig.TB_cut_won_C * 10000;

                if (GenieConfig.CB_cut_기준금)
                {
                    cut_수익금1 = GenieConfig.TB_cut_수익금1_C / 100 * 매수기준금;
                    cut_수익금2 = GenieConfig.TB_cut_수익금2_C / 100 * 매수기준금;
                    Cut_매입금 = GenieConfig.TB_cut_won_C / 100 * 매수기준금;
                }
                Cut_수익율 = GenieConfig.TB_cut_P_C;
                Cut_비중 = GenieConfig.TB_cut_ratio_C;
                Cut_비중선택 = GenieConfig.CBB_cut_gubun_C;
                Cut_주문값 = GenieConfig.TB_cut_value_C;
                Cut_매수매도 = GenieConfig.CBB_cut_jumun_C;
                Cut_취소시간 = GenieConfig.MTB_cut_cansel_time_C;
                익절그룹 = "Cut_C";
                검색식 = "수익금손절_C";

                if (Get.TimeNow >= Cut_time)
                {
                    if (cut_수익금1 <= Get.실현손익_시작 && Get.실현손익_시작 < cut_수익금2 && str.cut_LB_C.Equals("X"))
                    {
                        Get.Cut_남길금액_C = Get.실현손익_예상 * GenieConfig.TB_cut_남길퍼_C / 100;
                        Form1.form1.Cut_C = true;
                        str.cut_LB_C = "O";
                        if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.CB_cut_LB_C.Text = "O";
                    }

                    if (Form1.form1.Cut_C)
                    {
                        if (Get.실현손익_예상 > Get.Cut_남길금액_C) 주문실행();

                        if (Form1.Acc.실현손익 <= Get.Cut_남길금액_C)
                        {
                            수익금기준손절_주문취소("수익금손절_C");
                            Form1.form1.Cut_C = false;
                            매도재개();
                        }
                    }
                }
            }

            void 주문실행()
            {
                foreach (var 잔고 in Form1.stockBalanceList.Values)
                {
                    if (Form1.재시작) return;

                    if (잔고.매매가능)
                    {
                        string 잔고그룹 = GET.그룹변환(잔고.매매그룹);
                        long 총매입금 = 잔고.매입금액 + 잔고.신용_매입금액;

                        if (잔고.수익률 <= Cut_수익율 && 총매입금 >= Cut_매입금)
                        {
                            if (GET.익절그룹(익절그룹).Contains(잔고그룹) && Method.매매진입_가능여부(잔고.종목코드, 검색식))
                            {
                                잔고.매도정지 = true;

                                List<JumunItem> 취소대상_주문리스트 = Form1.JumunItem_List.Values
                                    .Where(개별주문 => 개별주문.종목코드 == 잔고.종목코드 && !개별주문.검색식.Contains("수익금손절"))
                                    .ToList();

                                // 결과: 취소대상_주문리스트에는 해당 종목이면서 '수익금손절' 주문이 아닌 것들만 담깁니다.

                                if (취소대상_주문리스트.Count > 0)
                                {
                                    foreach (JumunItem 취소할주문 in 취소대상_주문리스트)
                                    {
                                        취소할주문.반복횟수 = 0;
                                        취소할주문.취소시간 = 0;
                                        취소할주문.취소timer = 0;
                                        취소할주문.비고 = "수익금손절_미체결일괄 '취소'";
                                    }
                                }

                                ExecuteTrade.잔고주문_오더(잔고, 검색식, 2, Cut_비중, Cut_비중선택, Cut_주문값, Cut_매수매도, Cut_취소시간, 0, 0, 검색식, 검색식, 0, false, 0);
                            }
                        }
                    }
                }
            }

            void 매도재개()
            {
                foreach (var 잔고 in Form1.stockBalanceList.Values)
                {
                    if (Form1.재시작) return;
                    if (잔고.매매가능 && 잔고.매도정지)
                    {
                        string 잔고그룹 = GET.그룹변환(잔고.매매그룹);

                        if (잔고.수익률 <= Cut_수익율)
                        {
                            if (GET.익절그룹(익절그룹).Contains(잔고그룹))
                            {
                                잔고.매도정지 = false;
                            }
                        }
                    }
                }
            }
        }

        ////////////////           수익금기준손절 매매             ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////



        public static async Task<bool> 분할주문(string location, int 매수매도, int 시장가구분, string 종목코드, string _종목명, int _주문수량, int _현재가, string _검색식, int 취소시간)
        {
            bool 분할주문_가능 = false;

            int 분할간격 = GenieConfig.TB_분할간격_A;
            int 분할횟수 = GenieConfig.TB_분할횟수_A;

            if (시장가구분 == 5)
            {
                분할간격 = GenieConfig.TB_분할간격_B;
                분할횟수 = GenieConfig.TB_분할횟수_B;
            }

            if (시장가구분 == 6)
            {
                분할간격 = GenieConfig.TB_분할간격_C;
                분할횟수 = GenieConfig.TB_분할횟수_C;
            }

            int 주문호가 = 0;
            int 분할수량 = (int)(Math.Ceiling((double)_주문수량 / (double)분할횟수));

            for (int i = 0; i < 분할횟수; i++)
            {
                if (_주문수량 >= 분할수량)
                {
                    _주문수량 -= 분할수량;
                }
                else if (_주문수량 < 분할수량)
                {
                    if (_주문수량 > 0)
                    {
                        분할수량 = _주문수량;
                        _주문수량 -= 분할수량;
                    }
                    else
                    {
                        break;
                    }
                }

                Market_Item MarketItem = Form1.Market_Item_List[종목코드];

                if (Method.매매확인_VI_모투가능확인(MarketItem, 매수매도))
                {
                    int 분할가격 = Method.Hoga_Calculus(MarketItem.종목코드, _현재가, 주문호가);
                    int Order번호 = GET.Order번호();
                
                    if (Form1.stockBalanceList.TryGetValue(종목코드, out Stockbalance 잔고))

                        if (매수매도 == 1)
                        {
                            bool 신용주문 = 신용계산.신용주문_해야하나(매수매도, 분할수량, MarketItem, 잔고, out int 실제주문수량);
                            if (GenieConfig.CB_신용_주문사용 && GenieConfig.CB_신용_가능만매수)
                            {
                                if (!Form1.Market_Item_List[잔고.종목코드].신용가능) return 분할주문_가능;
                            }

                            홀딩잔고.예수금업데이트("매수", 분할가격, 분할수량, "주문", 종목코드, 신용주문);
                            await 주문(신용주문, "", 분할수량);
                        }
                        else
                        {
                            int 총주문가능수량 = GET.총주문가능수량(잔고);

                            if (총주문가능수량 > 0)
                            {
                                if (총주문가능수량 < 분할수량)
                                {
                                    분할수량 = 총주문가능수량;
                                }

                                신용계산.신용주문_분할매도_실행(잔고, 분할수량, async (is신용, 대출일, 수량) =>
                                {
                                    await 주문(is신용, 대출일, 수량);
                                });
                            }
                        }

                    async Task 주문(bool 신용주문, string 대출일, int 주문수량)
                    {
                        if (주문수량 > 0)
                        {
                            string 검색식 = "분할주문";
                            주문호가 += 분할간격;

                            // [지니 최적화] 긴 생성자 대신 명확한 속성 할당 사용
                            JumunItem 새주문 = new JumunItem
                            {
                                신용주문 = 신용주문,
                                대출일 = 대출일,
                                Deletetimer = 0,
                                Screennum = GET.JumunScreen(),
                                종목코드 = 종목코드,
                                종목명 = _종목명,            // *주의: 변수명 '_종목명'
                                주문번호 = "+++",
                                원주문번호 = "---",
                                검색식 = _검색식,            // *주의: 7번째 인자는 '_검색식'
                                주문값 = 0,
                                시장가구분 = 시장가구분,
                                취소시간 = 취소시간,
                                취소N주문 = 0,
                                반복횟수 = 0,
                                비고 = 검색식,               // *주의: 13번째 인자는 '검색식' (언더바 없음)
                                Pos = location,             // *주의: 14번째 인자에 'location' 변수
                                주문수량 = 주문수량,         // *주의: 변수명 '분할수량'
                                주문가격 = 분할가격,         // *주의: 변수명 '분할가격'
                                매수매도 = 매수매도,
                                비중 = 0,
                                비중단위 = 0,
                                취소timer = 취소시간,        // *주의: 20번째 인자 '취소시간' (재사용)
                                현재가 = _현재가,            // *주의: 변수명 '_현재가'
                                등락률 = 0,
                                주문시간 = Get.TimeNow,
                                미체결량 = 주문수량,
                                주문취소 = true,
                                가동전 = false,
                                Tik_cap = 0,
                                Tik_price = 0,
                                수익률 = 0,
                                주문동기화 = false,
                                감시번호 = 0,
                                Order번호 = Order번호,
                                수익구분 = 0,
                                NXT = NXT_server,
                                주문시간_Ticks = DateTime.Now.Ticks
                            };

                           await Jumun.Add(새주문);
                            ExecuteTrade.Que_order(새주문);
                        }
                        분할주문_가능 = true;
                    }
                }
            }

            return 분할주문_가능;
        }




















    }
}
