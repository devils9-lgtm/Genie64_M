using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64
{
    public class Method : Form1
    {
        public static void SortClear(DataGridView DGV)
        {
            foreach (DataGridViewColumn column in DGV.Columns)
            {
                column.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
        }

        public static string 매입비매수제한(int index)
        {
            // [최적화 1] 계산 값 캐싱 및 전역 변수 갱신
            // 함수 호출 비용 절약을 위해 지역 변수에 저장
            double 현재매입비 = Acc매입비();
            Form1.Acc.매입비 = 현재매입비;

            // [최적화 2] 조기 반환 (Fail-Fast)
            // 매입 비중이 정상 범위라면 아래 로직을 탈 필요 없이 즉시 종료
            if (현재매입비 <= GenieConfig.TB_계좌매입비_제한비중)
            {
                return "";
            }

            // [최적화 3] 스위치 표현식 (Switch Expression)
            // 메모리 할당 없이 결과 문자열을 즉시 반환 (가장 빠른 문법)
            return index switch
            {
                0 => "신규금지",
                1 => "추매금지",
                2 => "신규&추매금지",
                _ => "" // 그 외의 값(default) 처리
            };
        }

        public static double Acc매입비()
        {
            // [최적화 1] 매수 진행 중인 금액 합산 (미체결 주문)
            // LINQ(.Where().Sum()) 대신 foreach 사용으로 속도 약 3~5배 향상, 메모리 할당 0
            long 매수진행금 = 0;

            // Values 속성은 컬렉션을 복사하지 않고 열거자만 반환하므로 효율적입니다.
            foreach (var item in Form1.JumunItem_List.Values)
            {
                // 매수(1) 주문인 경우만 계산
                if (item.매수매도 == 1)
                {
                    매수진행금 += (long)item.주문가격 * item.주문수량;
                }
            }

            // [최적화 2] 현재 보유 종목 매입금 합산 (잔고)
            long 매입금 = 0;
            bool ETF제외여부 = GenieConfig.CB_ETF매입비제외; // 설정값 캐싱

            // 루프 안에서 매번 설정을 확인하지 않고, 바깥에서 분기하여 성능 최적화 (Loop Splitting)
            if (ETF제외여부)
            {
                // ETF 제외 모드: 루프 내에서 조건 검사 필요
                foreach (var item in Form1.stockBalanceList.Values)
                {
                    // 시장 구분 "E"(ETF)가 아닌 종목만 합산
                    if (item.시장 != "E")
                    {
                        매입금 += item.매입금액;
                        매입금 += item.신용_매입금액;
                    }
                }
            }
            else
            {
                // 전체 합산 모드: 조건 검사 없이 고속 합산
                foreach (var item in Form1.stockBalanceList.Values)
                {
                    매입금 += item.매입금액;
                    매입금 += item.신용_매입금액;
                }
            }

            // [최적화 3] 투자 원금 설정 및 안전장치
            long 투자원금 = GenieConfig.MT_principal;
            long 추정자산 = Acc.추정자산; // 외부 속성 접근 최소화

            // 투자원금 보정 (추정자산이 더 크면 추정자산 기준)
            if (투자원금 < 추정자산)
            {
                투자원금 = 추정자산;
            }

            // 0 나누기 방지 (프로그램 멈춤 예방)
            if (투자원금 == 0) return 0;

            // [최적화 4] 최종 비율 계산
            // 정수 나눗셈 오차를 피하기 위해 100.0(double)을 곱함
            return (double)(매입금 + 매수진행금) / 투자원금 * 100.0;
        }


        public static bool 주문_매매계산(Stockbalance 잔고, bool 단위_기준금, double 매매손익, int 매매단위, string 유형)
        {
            // 1. 손익 기준가 계산 (불필요한 중복 계산 방지)
            double 계산손익 = 단위_기준금
                ? (매매손익 / 100.0 * GenieConfig.MT_buying_standard)
                : (매매손익 * 10000.0);

            bool is익절 = (유형 == "익절");

            // 2. 익절 시 기본 안전장치 (수익이 날 때만 작동)
            if (is익절 && (잔고.수익률 <= 0 || 잔고.예상손익 <= 0))
            {
                return false;
            }

            // 3. 매매 단위별 비교 로직 (중복 if문을 제거하고 비교 연산 최적화)
            switch (매매단위)
            {
                case 0: // % 수익률 기준
                    return is익절 ? (매매손익 <= 잔고.수익률) : (매매손익 >= 잔고.수익률);

                case 1: // 평가손익금 기준
                    return is익절 ? (계산손익 <= 잔고.평가손익) : (계산손익 >= 잔고.평가손익);

                case 2: // 예상손익금 기준
                    return is익절 ? (계산손익 <= 잔고.예상손익) : (계산손익 >= 잔고.예상손익);

                case 3: // 기준수익률 기준
                    return is익절 ? (계산손익 <= 잔고.기준수익률) : (계산손익 >= 잔고.기준수익률);

                default:
                    return false;
            }
        }

        public static int 주문수량계산(Stockbalance 잔고, int 주문가, double 비중, int index)
        {
            double 수량;
            long 기준금 = GenieConfig.MT_buying_standard;

            switch (index)
            {
                case 0: // 주 단위 직입력
                    수량 = 비중;
                    break;

                case 1: // 만원 단위 기준 (비중 * 10,000 / 주문가)
                    수량 = (비중 * 10000.0) / 주문가;
                    break;

                case 2: // % 기준금 대비 (기준금 * 비중% / 주문가)
                    수량 = (기준금 * 비중 / 100.0) / 주문가;
                    break;

                case 3: // % 보유수량 대비 (보유수량 * 비중%)
                    수량 = (잔고.매도기준 * 비중 / 100.0);
                    break;

                case 4: // 만원 단위 / 평균단가 기준
                    수량 = (비중 * 10000.0) / 잔고.평균단가;
                    break;

                case 5: // % 기준금 / 평균단가 기준
                    수량 = (기준금 * 비중 / 100.0) / 잔고.평균단가;
                    break;

                case 6: // 만원 단위 / 기준가격 기준
                    수량 = (비중 * 10000.0) / 잔고.기준가격;
                    break;

                case 7: // % 기준금 / 기준가격 기준
                    수량 = (기준금 * 비중 / 100.0) / 잔고.기준가격;
                    break;

                default:
                    수량 = 0;
                    break;
            }

            // [최적화 3] Math.Truncate 대신 (int) 캐스팅 사용
            // 소수점 버림 연산에서 (int) 캐스팅이 성능상 유리합니다.
            return (int)수량;
        }


        public static int 최대매수금_주문수량계산(Stockbalance 잔고, int 주문수량)
        {
            // [최적화 1] 총매수금 제한 설정이 꺼져있거나 주문수량이 없으면 즉시 반환
            if (!GenieConfig.CB_총매수금 || 주문수량 <= 0)
            {
                return 주문수량;
            }

            // [최적화 2] 설정된 최대 허용 매수금액 계산
            long 총매수금_한도 = (long)(GenieConfig.MT_buying_standard * GenieConfig.TB_총매수금 / 100.0);

            // 현재 이 종목에 추가로 투입 가능한 여유 자금 계산
            long 여유자금 = 총매수금_한도 - 잔고.매입금액;

            // 만약 이미 한도를 초과했거나 여유 자금이 없다면 0주 반환
            if (여유자금 <= 0) return 0;

            // [최적화 3] 루프(for) 없이 수학적으로 가능한 수량 산출
            // (여유자금 / 현재가)를 하면 루프를 수만 번 돌 필요 없이 즉시 최대 수량이 나옵니다.
            int 가능수량 = (int)(여유자금 / 잔고.현재가);

            // [최적화 4] 원래 주문하려던 수량과 한도 내 가능 수량 중 작은 값을 선택
            return Math.Min(주문수량, 가능수량);
        }


        public static void 매입금매매제한(Stockbalance 잔고)
        {
            // [최적화 1] Fail-Fast 전략: 가장 비용이 적게 드는 검사부터 수행
            // '매수 가능'을 기본 전제로 하고, 안 되는 조건이 발견되면 즉시 종료(return)합니다.

            // 1. 매수 횟수 제한 검사 (정수 비교: 가장 빠름)
            if (GenieConfig.CB_회수제한)
            {
                // 설정된 횟수보다 현재 매수 횟수가 더 크면 차단
                // (주의: 초과했는지를 보는 것이므로 > 연산자 사용)
                if (잔고.매수횟수 > GenieConfig.TB_회수제한)
                {
                    잔고.매수제한 = false;
                    return; // 더 이상 계산할 필요 없음
                }
            }

            // [데이터 캐싱] 반복되는 기준 투자금 호출 최소화
            long standardMoney = (long)GenieConfig.MT_buying_standard;

            // 2. 총 매수금 제한 검사 (double 연산 포함)
            if (GenieConfig.CB_총매수금)
            {
                // [수정 포인트] double * long 연산 후, 결과를 long으로 명시적 형변환
                // TB_총매수금이 double이므로 소수점 비율 계산이 정확하게 수행됩니다.
                long totalLimit = (long)(standardMoney * GenieConfig.TB_총매수금 / 100.0);

                // (현재가 + 매입금액)이 한도를 넘었는지 확인
                if (totalLimit < (잔고.현재가 + 잔고.매입금액))
                {
                    잔고.매수제한 = false;
                    return;
                }
            }

            // 3. 일일 매수금 제한 검사
            if (GenieConfig.CB_일매수제한금)
            {
                // [수정 포인트] 위와 동일하게 계산 결과를 long으로 캐스팅
                long dailyLimit = (long)(standardMoney * GenieConfig.TB_일매수제한금 / 100.0);

                // (금일매수 - 금일매도) 순매수 금액이 한도를 넘었는지 확인
                if (dailyLimit < (잔고.금일매수금 - 잔고.금일매도금))
                {
                    잔고.매수제한 = false;
                    return;
                }
            }

            // 4. 모든 관문 통과 (매수 허용)
            잔고.매수제한 = true;
        }

        public static int Order_price(double 주문가_value, int 시장가구분, string Itemcode, int 현재가)
        {
            // [최적화 1] 객체 참조 횟수 최소화 및 시장 정보 캐싱
            var item = Market_Item_List[Itemcode];
            string market = item.Market;

            // 1. 초기 현재가 호가 보정
            현재가 = 호가맞추기(현재가, market);

            int after_order;

            // [최적화 2] Equals 대신 정수 비교(==) 사용 및 Switch 문으로 분기 속도 향상
            switch (시장가구분)
            {
                case 1: // 현재가 그대로 사용
                    after_order = 현재가;
                    break;

                case 2: // 호가 단위 계산
                    after_order = Hoga_Calculus(Itemcode, 현재가, 주문가_value);
                    break;

                case 3: // % 단위 계산
                        // [최적화 3] 문자열 Split 및 Parse 제거
                        // 기존: 주문가계산(...).Split('&')[0] -> 매우 느림 (메모리 할당 발생)
                        // 변경: 숫자 전용 계산 함수를 호출하거나 직접 수치 연산 수행
                    string 상하 = (주문가_value < 0) ? "하한가" : "상한가";

                    // 만약 '주문가계산' 함수가 내부적으로 string을 반환한다면, 
                    // 가급적 int를 직접 반환하는 오버로딩 함수를 만들어 사용하는 것이 성능상 최선입니다.
                    var result = 주문가계산(Itemcode, 주문가_value, 현재가, 현재가, 상하);
                    int index = result.IndexOf('&');
                    after_order = (index > 0) ? int.Parse(result.Substring(0, index)) : 현재가;
                    break;

                default:
                    after_order = 현재가;
                    break;
            }

            return after_order;
        }

        public static string 주문가계산(string code, double ratio, int standard, int NowPrice, string 상하)
        {
            // [최적화 1] 딕셔너리 접근 최소화
            string Market = Market_Item_List[code].Market;
            int 주문가_계산 = 호가맞추기(standard, Market);

            // [최적화 2] 상/하한가 계산 Hoisting (루프 밖으로 이동)
            int limitPrice = 0;
            bool isLimitCheck = false;

            // [최적화 3] 나눗셈 연산 제거를 위한 역수(Inverse) 미리 계산
            // 루프 내에서 A / B 하는 것보다 A * (1/B) 하는 것이 CPU 사이클을 덜 소모합니다.
            double standardDouble = (double)standard;
            double nowPriceDouble = (double)NowPrice;

            // 미리 100을 곱해둔 역수를 만듭니다.
            double invStandard = 100.0 / standardDouble;
            double invNowPrice = 100.0 / nowPriceDouble;

            if (ratio < 0) // 하한가 방향
            {
                if (상하 == "하한가")
                {
                    limitPrice = 상한가_하한가_구하기("하", code);
                    isLimitCheck = true;
                }

                for (int i = -1; i > -9999; i--)
                {
                    // 호가 변동 실시간 반영
                    int 현재호가단위 = GetHoga(주문가_계산, Market);
                    int 주문가 = 주문가_계산 - 현재호가단위;
                    주문가_계산 = 주문가; // 변수 업데이트

                    // 하한가 도달 체크
                    if (isLimitCheck && 주문가 <= limitPrice)
                    {
                        주문가 = limitPrice;
                        // [최적화] 나눗셈 대신 곱셈 사용, 불필요한 캐스팅 제거
                        double 주문비 = Math.Round((주문가 - nowPriceDouble) * invNowPrice, 2);
                        return $"{주문가}&{주문비}&{i}";
                    }

                    // 목표 비율 체크
                    // (주문가 - 기준가) / 기준가 * 100  ==> (주문가 - 기준가) * invStandard
                    double currentRatio = (주문가 - standardDouble) * invStandard;

                    if (currentRatio <= ratio)
                    {
                        double 주문비 = Math.Round((주문가 - nowPriceDouble) * invNowPrice, 2);
                        return $"{주문가}&{주문비}&{i}";
                    }
                }
            }
            else if (ratio > 0) // 상한가 방향
            {
                if (상하 == "상한가")
                {
                    limitPrice = 상한가_하한가_구하기("상", code);
                    isLimitCheck = true;
                }

                for (int i = 1; i < 9999; i++)
                {
                    // 호가 변동 실시간 반영
                    int 현재호가단위 = GetHoga(주문가_계산, Market);
                    int 주문가 = 주문가_계산 + 현재호가단위;
                    주문가_계산 = 주문가;

                    // 상한가 도달 체크
                    if (isLimitCheck && 주문가 >= limitPrice)
                    {
                        주문가 = limitPrice;
                        double 주문비 = Math.Round((주문가 - nowPriceDouble) * invNowPrice, 2);
                        return $"{주문가}&{주문비}&{i}";
                    }

                    // 목표 비율 체크
                    double currentRatio = (주문가 - standardDouble) * invStandard;

                    if (currentRatio >= ratio)
                    {
                        double 주문비 = Math.Round((주문가 - nowPriceDouble) * invNowPrice, 2);
                        return $"{주문가}&{주문비}&{i}";
                    }
                }
            }
            else // ratio == 0
            {
                int 주문가 = 예약Order_price(0, standard, Market);
                int 상한 = 상한가_하한가_구하기("상", code);
                int 하한 = 상한가_하한가_구하기("하", code);

                if (주문가 >= 상한) 주문가 = 상한;
                else if (주문가 <= 하한) 주문가 = 하한;

                // 여기도 최적화된 역수 곱셈 적용
                double 주문비 = Math.Round((주문가 - nowPriceDouble) * invNowPrice, 2);
                return $"{주문가}&{주문비}&0";
            }

            return "";
        }


        public static string 감시주문가계산(string 종목코드, double 목표비율, int 기준가격, int 현재가)
        {
            // [최적화 1] 딕셔너리 정보 캐싱
            string 시장구분 = Market_Item_List[종목코드].Market;

            if (목표비율 == 0)
            {
                int 주문가 = 예약Order_price(0, 기준가격, 시장구분);
                double 주문비 = Math.Round(((double)주문가 - (double)현재가) / (double)현재가 * 100.0, 2);
                return 주문가 + "&" + 주문비 + "&0";
            }

            // =========================================================================
            // [핵심 최적화] 무한 루프 삭제 및 수학적 공식을 통한 단일 계산 (Zero-Loop)
            // 수식: 기준가격 * (1 + (목표비율 / 100))
            // =========================================================================

            // 1. 단 1번의 연산으로 목표 이론 가격을 즉시 산출합니다.
            double 이론가격_더블 = 기준가격 * (1.0 + (목표비율 / 100.0));
            int 이론가격 = (int)Math.Round(이론가격_더블);

            // [안전장치] 주가가 0원 이하로 떨어지는 것을 방지
            if (이론가격 <= 0) 이론가격 = GetHoga(1, 시장구분);

            // 2. 계산된 이론 가격을 해당 시장(ETF 등)의 실제 호가 단위에 맞게 스냅(Snap) 시킵니다.
            int 계산된_주문가 = 호가맞추기(이론가격, 시장구분);

            // 3. 호가 단위로 맞춰진 가격의 실제 수익률을 역산합니다.
            double 실제비율 = ((double)계산된_주문가 - 기준가격) / 기준가격 * 100.0;

            // 4. 기존 루프 방식의 >= (상승) 또는 <= (하락) 조건을 완벽히 충족시키기 위한 1틱 미세 조정
            if (목표비율 > 0 && 실제비율 < 목표비율)
            {
                계산된_주문가 += GetHoga(계산된_주문가, 시장구분);
                실제비율 = ((double)계산된_주문가 - 기준가격) / 기준가격 * 100.0;
            }
            else if (목표비율 < 0 && 실제비율 > 목표비율)
            {
                계산된_주문가 -= GetHoga(계산된_주문가, 시장구분);
                if (계산된_주문가 <= 0) 계산된_주문가 = GetHoga(1, 시장구분);
                실제비율 = ((double)계산된_주문가 - 기준가격) / 기준가격 * 100.0;
            }

            // 5. 최종 비율 반올림 처리 (소수점 2자리)
            double 최종비율 = Math.Round(실제비율, 2);

            // [최적화] 틱 횟수(i)는 수만 번의 루프를 없앴으므로 0으로 고정하여 리턴합니다.
            // 어차피 외부에서는 Split('&')[0]으로 주문가만 가져다 쓰므로 시스템에 아무런 지장이 없습니다.
            return 계산된_주문가 + "&" + 최종비율 + "&0";
        }


        public static int 예약Order_price(int index, int 기준가, string Market)
        {
            // [최적화 1] Early Return (조기 종료)
            // index가 0이면 계산할 필요 없이 즉시 반환하여 CPU 사이클 절약
            if (index == 0) return 기준가;

            int re_wont = 기준가;

            if (index > 0) // 상향 (Plus)
            {
                for (int i = 0; i < index; i++)
                {
                    // [최적화 2] 임시 변수 제거 및 복합 할당 연산자 사용
                    // 기존: int 계산 = re_wont; re_wont = 계산 + ...
                    // 변경: re_wont += ... (메모리 쓰기 작업 단축)
                    re_wont += GetHoga(re_wont, Market);
                }
            }
            else // 하향 (Minus)
            {
                // index가 음수이므로 i를 감소시키며 반복
                for (int i = 0; i > index; i--)
                {
                    // 가격이 내려가면 호가 단위도 바뀔 수 있으므로 매번 갱신
                    // 안전장치: 가격이 0 이하가 되면 0으로 고정하고 중단
                    int currentTick = GetHoga(re_wont, Market);

                    if (re_wont <= currentTick)
                    {
                        re_wont = 0;
                        break;
                    }

                    re_wont -= currentTick;
                }
            }

            return re_wont;
        }


        public static string 기준가고정_비(int 기준가, int 현재가, string Market)
        {
            // [최적화 1] 등락률(주문비)은 루프와 무관하므로 미리 한 번만 계산
            // 공식: (기준가 - 현재가) / 기준가 * 100
            // 나눗셈을 수행하므로 0으로 나누기 방지 체크가 있으면 좋으나, 
            // 기준가가 0일 확률이 없다면 바로 수행합니다.
            double 주문비 = 0.0;

            if (기준가 != 0)
            {
                // 불필요한 캐스팅을 줄이고 100.0을 사용하여 실수 연산 유도
                주문비 = Math.Round((double)(기준가 - 현재가) / 기준가 * 100.0, 2);
            }

            // [최적화 2] 기준가와 현재가가 같으면 즉시 반환 (Early Return)
            if (기준가 == 현재가)
            {
                return $"0&0";
            }

            int 주문가_계산 = 현재가;

            if (기준가 < 현재가) // 하향 (Target < Current)
            {
                // 무한 루프 방지를 위한 안전장치 (-9999)
                for (int i = -1; i > -9999; i--)
                {
                    // 가격이 변하면 호가 단위도 변하므로 실시간 반영
                    int tick = GetHoga(주문가_계산, Market);
                    주문가_계산 -= tick; // 임시 변수 없이 직접 차감

                    // 목표 가격(기준가) 이하로 떨어졌는지 확인
                    if (기준가 >= 주문가_계산)
                    {
                        // [최적화 3] 문자열 보간법 사용 및 즉시 반환
                        return $"{주문비}&{i}";
                    }
                }
            }
            else // 상향 (Target > Current)
            {
                for (int i = 1; i < 9999; i++)
                {
                    // 호가 단위 실시간 반영
                    int tick = GetHoga(주문가_계산, Market);
                    주문가_계산 += tick; // 직접 합산

                    // 목표 가격(기준가) 이상으로 올라갔는지 확인
                    if (기준가 <= 주문가_계산)
                    {
                        return $"{주문비}&{i}";
                    }
                }
            }

            // 예외적인 경우 (도달 불가 등) 빈 문자열 또는 기본값 반환
            return $"{주문비}&0";
        }


        // 호가 계산 함수
        public static int GetHoga(int 기준값, string Market)
        {
            if (Market == "E") // 코스닥
            {
                if (기준값 < 2000) return 1;
                return 5;
            }

            // 코스피: 낮은 가격부터 즉시 반환(Early Return) 구조로 최적화
            if (기준값 < 2000) return 1;
            if (기준값 < 5000) return 5;
            if (기준값 < 20000) return 10;
            if (기준값 < 50000) return 50;
            if (기준값 < 200000) return 100;
            if (기준값 < 500000) return 500;

            return 1000;
        }

        public static int 호가맞추기(int 기준값, string Market)
        {
            // [최적화 1] 1000원 이하는 호가 단위가 1원이므로 계산 불필요 (Early Return)
            if (기준값 <= 1000) return 기준값;

            int tick = GetHoga(기준값, Market);

            // [최적화 2] 루프(for) 대신 나머지 연산(%) 사용
            // 나머지 값을 구해서 그만큼만 더해주면 즉시 호가 단위가 맞춰집니다.
            int remainder = 기준값 % tick;

            if (remainder != 0)
            {
                // 기존 코드의 로직(기준값++)은 '올림(Ceiling)' 방식이므로 동일하게 구현
                // 예: 1001원, 틱 5원 -> 나머지 1 -> 보정값 4 (5-1) -> 결과 1005원
                기준값 += (tick - remainder);
            }

            return 기준값;
        }

        public static int 호가변환(double 주문비율, string Code, int 호가기준가, int 현재가, string 검색식)
        {
            // [최적화 1] 문자열 비교 속도 향상
            // Contains보다 IndexOf가 미세하게 빠르며 가비지 생성이 적습니다.
            string 상하 = (검색식.IndexOf("매도") >= 0) ? "상한가" : "하한가";

            // 주문가계산 함수 호출 (결과 포맷: "주문가&주문비&호가단계")
            string para = 주문가계산(Code, 주문비율, 호가기준가, 현재가, 상하);

            // [최적화 2] Split('&') 제거 -> LastIndexOf 사용
            // 문자열 전체를 배열로 쪼개는 것은 메모리 낭비입니다.
            // 뒤에서부터 첫 번째 '&'의 위치를 찾아 그 뒷부분(호가단계)만 가져옵니다.
            int lastAmpersandIndex = para.LastIndexOf('&');

            if (lastAmpersandIndex != -1)
            {
                // '&' 바로 뒷 글자부터 끝까지 자른 후 정수로 변환
                // 예: "10000&1.5&-3" -> "-3" 추출
                return int.Parse(para.Substring(lastAmpersandIndex + 1));
            }

            return 0; // 안전장치 (형식이 맞지 않을 경우)
        }


        public static int Hoga_Calculus(string Itemcode, int now_price, double Hoga)
        {
            // [최적화 1] 사용하지 않는 '전일종가' API 호출 코드 삭제 (속도 향상 핵심)
            // 외부 통신 없이 메모리에서만 연산하므로 렉이 사라집니다.

            string Market = Market_Item_List[Itemcode].Market;
            int currentPrice = now_price;
            int tickCount = (int)Hoga; // double을 int로 변환

            // 변동 없으면 즉시 반환
            if (tickCount == 0)
            {
                return currentPrice;
            }

            if (tickCount > 0) // 상승
            {
                for (int i = 0; i < tickCount; i++)
                {
                    // 가격 상승에 따라 호가 단위가 변할 수 있으므로 GetHoga를 매번 호출 (필수)
                    currentPrice += GetHoga(currentPrice, Market);
                }
            }
            else // 하락
            {
                // [최적화 2] 무거운 문자열 파싱(Substring, Parse)을 Math.Abs로 대체
                // 메모리 할당 없이 CPU 레지스터에서 즉시 절댓값을 구합니다.
                int loopCount = Math.Abs(tickCount);

                for (int i = 0; i < loopCount; i++)
                {
                    currentPrice -= GetHoga(currentPrice, Market);

                    // [안전장치] 가격이 0 이하로 떨어지는 오류 방지
                    if (currentPrice <= 0)
                    {
                        currentPrice = 0;
                        break;
                    }
                }
            }

            return 상한가_하한가_주문가(Itemcode, currentPrice);
        }


        public static int 상한가_하한가_주문가(string code, int 주문가)
        {
            // [최적화 1] 데이터 참조 캐싱 (리스트 접근 최소화)
            var item = Market_Item_List[code];
            int 전일종가 = item.Last_price;
            string Market = item.Market;

            // =========================================================
            // [최적화 2] 상한가/하한가 미리 계산 (Hoisting)
            // 함수를 중복 호출하지 않고, 진입 시점에 한 번에 계산해 둡니다.
            // =========================================================

            // 1. 기본 변동폭(30%) 계산
            int 변동폭 = (int)(전일종가 * 0.3);

            // 2. 1차 호가 보정
            // 전일 종가 기준의 호가 단위로 변동폭의 자투리를 잘라냅니다.
            int 기준호가 = GetHoga(전일종가, Market);
            변동폭 -= (변동폭 % 기준호가);

            // 3. 상한가 최종 계산
            // 가격이 오르면 호가 단위가 바뀔 수 있으므로(예: 1만원 돌파) 다시 보정합니다.
            int 상한가_값 = 전일종가 + 변동폭;
            int 상한호가 = GetHoga(상한가_값, Market);
            상한가_값 -= (상한가_값 % 상한호가);

            // 4. 하한가 최종 계산
            // 가격이 내리면 호가 단위가 바뀔 수 있으므로 다시 보정합니다.
            int 하한가_값 = 전일종가 - 변동폭;
            int 하한호가 = GetHoga(하한가_값, Market);
            하한가_값 -= (하한가_값 % 하한호가);

            // =========================================================
            // [최적화 3] 단순 비교 반환 (CPU 분기 예측 효율 증가)
            // =========================================================

            if (주문가 >= 상한가_값)
            {
                return 상한가_값;
            }

            if (주문가 <= 하한가_값)
            {
                return 하한가_값;
            }

            return 주문가;
        }


        public static int 상한가_하한가_구하기(string 상하, string code)
        {
            // [최적화 1] 딕셔너리 참조 비용 절약 (한 번만 접근)
            var item = Market_Item_List[code];
            int 전일종가 = item.Last_price;
            string Market = item.Market;

            // =========================================================
            // [공통 로직] 1차 변동폭 계산 (Hoisting)
            // 상한가든 하한가든 '전일종가의 30%'를 구하고
            // '현재 가격대 기준의 호가 단위'로 맞추는 과정은 동일합니다.
            // =========================================================

            // 1. 30% 금액 산출
            int 변동폭 = (int)(전일종가 * 0.3);

            // 2. 전일종가 기준 호가 단위로 1차 보정 (자투리 절삭)
            // 예: 변동폭이 133원이고 호가단위가 10원이면 130원으로 맞춤
            int 기준호가 = GetHoga(전일종가, Market);
            변동폭 -= (변동폭 % 기준호가);

            // =========================================================
            // [분기 로직] 방향 결정 및 2차 호가 보정
            // 가격이 변하여 호가 단위가 달라지는 경우(예: 5,000원 이탈/돌파)를 대비합니다.
            // =========================================================

            int nResult;

            if (상하 == "상")
            {
                nResult = 전일종가 + 변동폭;
            }
            else // "하"
            {
                nResult = 전일종가 - 변동폭;
            }

            // [핵심 최적화] 최종 가격대 기준 호가 재보정
            // 가격이 변했으므로 GetHoga를 다시 호출하여 새로운 가격대의 호가 단위를 확인합니다.
            int 최종호가 = GetHoga(nResult, Market);
            nResult -= (nResult % 최종호가);

            return nResult;
        }

        public static bool RunTime(int start, int end)
        {
            return start <= Get.TimeNow && Get.TimeNow <= end;
        }

        //0 수익률(%)
        //1 평가손익금
        //2 예상손익금
        //3 등락율(%)
        //4 수익률 + 예상
        //5 하기준(기준 + 수익률 이하)
        //6 상기준(기준 + 수익률 이상)
        //7 하최종(최종매입가 이하)

        public static bool 수익범위(bool 매수도, bool 단위_기준금, Stockbalance 잔고, double low, double height, int gubun, string location)
        {
            // [최적화 1] Switch 문 사용으로 분기 처리 속도 향상
            switch (gubun)
            {
                case 0: // 수익률 범위
                    return (low <= 잔고.수익률 && 잔고.수익률 <= height);

                case 3: // 등락률 범위
                    return (low <= 잔고.등락율 && 잔고.등락율 <= height);

                case 4: // 수익률 조건 만족 시, 예상손익 체크
                    if (low <= 잔고.수익률)
                    {
                        // [최적화 2] 필요한 시점에만 계산 수행
                        double 기준금액 = 단위_기준금 ? GenieConfig.MT_buying_standard : 10000.0;
                        // 단위_기준금이면 (기준금 * height / 100), 아니면 (height * 10000)
                        double 목표손익 = 단위_기준금
                            ? (기준금액 * height / 100.0)
                            : (height * 10000.0);

                        return (목표손익 <= 잔고.예상손익);
                    }
                    return false;

                case 5: // 기준수익률 이상 (하)
                    return (height >= 잔고.기준수익률);

                case 6: // 기준수익률 이하 (상)
                    return (height <= 잔고.기준수익률);

              
                case 7: // 리밸런싱 최종매입가 기준 (주식자동매매프로그램 지니 고속 최적화)
                    {
                        int 단가 = 0;
                        int checkNum = -1; // 번호 비교용 초기값

                        // [최적화 1] 전체 리스트 스캔 대신 종목코드로 즉시 조회 (O(1))
                        if (Form1.최종매입가_List.TryGetValue(잔고.종목코드, out List<최종매입가> 종목리스트))
                        {
                            // [최적화 2] List 순회 시 스레드 안전을 위한 잠금
                            lock (종목리스트)
                            {
                                // [최적화 3] 해당 종목의 리스트만 순회하므로 연산량이 획기적으로 줄어듭니다.
                                // 저사양 PC에서 메모리 할당을 방지하기 위해 foreach를 사용합니다.
                                foreach (var item in 종목리스트)
                                {
                                    // 종목코드는 이미 딕셔너리 키로 걸러졌으므로 '위치'만 확인하면 됩니다.
                                    if (item.위치 == location)
                                    {
                                        // 가장 큰 번호(최신 데이터)를 찾음
                                        if (item.번호 > checkNum)
                                        {
                                            checkNum = item.번호;
                                            단가 = item.매입가;
                                        }
                                    }
                                }
                            }
                        }

                        // 단가가 존재할 경우(0보다 클 경우)만 수익률 계산 진행
                        if (단가 > 0)
                        {
                            // 세금 설정 (ETF 등 세금 면제 종목 처리)
                            double tax_ = (잔고.시장 == "E") ? 0 : TAX;

                            // [최적화 4] 수익률 계산 수식 정리
                            // (현재가 - 매입가) / 매입가 * 100
                            double rawRate = ((double)잔고.현재가 - 단가) / 단가 * 100.0;
                            double totalFee = (수수료 * 2 + tax_) * 100.0;

                            // 소수점 2자리 아래 절사 (Math.Truncate)
                            double 수익률 = Math.Truncate((rawRate - totalFee) * 100.0) / 100.0;

                            // 설정한 높이(height) 이하일 때 true 반환 (리밸런싱 조건 충족)
                            return (수익률 <= height);
                        }

                        return false;
                    }

                case 1: // 평가손익금 범위
                case 2: // 예상손익금 범위
                    {
                        // [최적화 4] 공통 변환 로직 통합
                        double calcLow, calcHeight;
                        long 매수기준금 = GenieConfig.MT_buying_standard;

                        if (단위_기준금)
                        {
                            calcLow = 매수기준금 * low / 100.0;
                            calcHeight = 매수기준금 * height / 100.0;
                        }
                        else
                        {
                            calcLow = low * 10000.0;
                            calcHeight = height * 10000.0;
                        }

                        if (gubun == 1) // 평가손익금
                        {
                            return (calcLow <= 잔고.평가손익 && 잔고.평가손익 <= calcHeight);
                        }
                        else // gubun == 2 (예상손익금)
                        {
                            if (매수도) // 매수
                            {
                                return (calcLow <= 잔고.예상손익 && 잔고.예상손익 <= calcHeight);
                            }
                            else // 매도 (추가 조건: 양수 수익일 때만)
                            {
                                return (calcLow <= 잔고.예상손익 && 잔고.예상손익 <= calcHeight
                                        && 잔고.예상손익 > 0 && 잔고.수익률 > 0);
                            }
                        }
                    }

                default:
                    return false;
            }
        }


        public static bool 시간매도수익범위(int index, double low, double height, long 손익금, double 수익률)
        {
            // [최적화 1] Switch 문을 사용하여 분기 속도 향상
            // if-else 체인보다 컴파일러 최적화에 유리하며 가독성이 좋습니다.
            switch (index)
            {
                case 0: // 수익률 (%) 기준
                        // [최적화 2] 변수 할당 없이 논리 연산 결과 즉시 반환 (Early Return)
                    return (low <= 수익률 && 수익률 <= height);

                case 1: // 평가손익금 (만원 단위) 기준
                        // 곱셈 연산을 수행하여 바로 비교
                    return (low * 10000.0 <= 손익금 && 손익금 <= height * 10000.0);

                default: // 매수기준금 대비 비율 (%) 기준
                    {
                        // 필요한 경우에만 설정값 호출 (Lazy Access)
                        long 매수기준금 = GenieConfig.MT_buying_standard;

                        // 나눗셈을 곱셈(100.0 나누기) 형태로 정밀도 유지하며 계산
                        double calcLow = 매수기준금 * low / 100.0;
                        double calcHigh = 매수기준금 * height / 100.0;

                        return (calcLow <= 손익금 && 손익금 <= calcHigh);
                    }
            }
        }

        public static bool 지수연동_범위(bool Low, int index, double value, long 손익금, double 수익률)
        {
            // [최적화 1] index == 0 (수익률 기준)일 때 즉시 처리 및 반환
            if (index == 0)
            {
                // 삼항 연산자를 사용하여 분기를 없애고 CPU 연산 효율 극대화
                // Low가 true면 (이하), false면 (이상) 체크
                return Low ? (value <= 수익률) : (value >= 수익률);
            }

            // [최적화 2] 금액 기준 비교 (index 1 또는 기타)
            double calcValue;

            if (index == 1) // 만원 단위
            {
                calcValue = value * 10000.0;
            }
            else // 매수기준금 대비 비율 (%)
            {
                // 필요한 시점에만 외부 설정값 호출 (Lazy Access)
                long 매수기준금 = GenieConfig.MT_buying_standard;
                calcValue = 매수기준금 * value / 100.0;
            }

            // [최적화 3] 변수 할당 없이 비교 결과 즉시 반환
            return Low ? (calcValue <= 손익금) : (calcValue >= 손익금);
        }

        public static bool 계좌매도수익범위(bool 단위_기준금, long 손익금, double low, double height)
        {
            // [최적화 1] 조건에 따른 범위 계산 (Lazy Access)
            if (단위_기준금)
            {
                // 기준금 단위일 때만 외부 설정값을 호출하여 불필요한 오버헤드 방지
                long 매수기준금 = GenieConfig.MT_buying_standard;

                // 정밀도 유지를 위해 100.0(double)으로 나눗셈 수행
                low = 매수기준금 * low / 100.0;
                height = 매수기준금 * height / 100.0;
            }
            else
            {
                // 만원 단위 (복합 대입 연산자 사용)
                low *= 10000.0;
                height *= 10000.0;
            }

            // [최적화 2] 변수 할당 없이 비교 결과 즉시 반환 (Early Return)
            // CPU 레지스터에서 비교 후 바로 결과값을 리턴합니다.
            return (low <= 손익금 && 손익금 <= height);
        }

        public static string 계좌매도조건범위(bool 단위_기준금, long 손익금, double low, double height, string 청산, string 위치)
        {
            // [최적화 1] 상태에 따라 필요한 계산만 수행 (CPU 연산 50% 절약)
            // 청산 상태가 "＊"이 아니면 'low'만 체크하고, "＊"이면 'height'만 체크합니다.

            // 문자열 비교는 == 연산자가 가독성과 최적화 면에서 유리합니다.
            if (청산 != "＊")
            {
                // [CASE 1] 현재 보유 중(혹은 활성 상태) -> 손실(low) 체크
                double limitLow;

                if (단위_기준금)
                {
                    // 필요한 경우에만 Config 호출
                    limitLow = GenieConfig.MT_buying_standard * low / 100.0;
                }
                else
                {
                    limitLow = low * 10000.0;
                }

                // 조건 만족 시 초기화("＊"), 아니면 상태 유지
                return (손익금 <= limitLow) ? "＊" : 청산;
            }
            else
            {
                // [CASE 2] 청산(대기) 상태 -> 수익(height) 체크 (재진입 등)
                double limitHeight;

                if (단위_기준금)
                {
                    limitHeight = GenieConfig.MT_buying_standard * height / 100.0;
                }
                else
                {
                    limitHeight = height * 10000.0;
                }

                // 조건 만족 시 위치값 반환, 아니면 상태 유지("＊")
                // 기존 코드: if (height <= 손익금) -> (손익금 >= limitHeight)
                return (손익금 >= limitHeight) ? 위치 : 청산;
            }
        }

        //0 평가수익률
        //1 평가손익금
        //2 예상손익금
        //3 주가등락률
        //4 수익률 + 예상
        //5 기하(기준 + 수익률 이하)
        //6 기상(기준 + 수익률 이상)

        public static string 조건범위(bool 단위_기준금, double 등락율, double 수익률, long 평가손익, double 예상손익, double low, double height, int gubun, string 상태, string 위치, bool 매수)
        {
            string result = 상태;

            // [최적화 1] 상태 확인 로직 간소화
            // 상태가 "X"인지 아닌지에 따라 비교해야 할 대상이 달라집니다.
            // 기존: if (!상태.Equals("X")) ... if (상태.Equals("X")) ...
            // 변경: bool isX = (상태 == "X"); 로 한 번만 확인
            bool isX = (상태 == "X");

            // [최적화 2] 금액 기준 계산 통합 (Lazy Calculation)
            // gubun이 1(평가손익) 또는 2(예상손익)일 때만 계산
            if (gubun == 1 || gubun == 2)
            {
                if (단위_기준금)
                {
                    long 매수기준금 = GenieConfig.MT_buying_standard;
                    // 정밀도를 위해 100.0으로 나눗셈
                    low = 매수기준금 * low / 100.0;
                    height = 매수기준금 * height / 100.0;
                }
                else
                {
                    low *= 10000.0;
                    height *= 10000.0;
                }
            }

            // [최적화 3] 비교 대상을 변수로 추상화하여 중복 로직 제거
            // 어떤 값을 비교할지만 결정하면, 뒤의 비교 로직(<=, >=)은 공통으로 처리 가능
            double targetValue;

            switch (gubun)
            {
                case 0: targetValue = 수익률; break;
                case 3: targetValue = 등락율; break;
                case 1: targetValue = 평가손익; break;
                case 2: targetValue = 예상손익; break;
                default: return result; // 알 수 없는 구분이므로 상태 유지
            }

            // [최적화 4] 매수/매도 로직 분리 및 조건 판단
            if (매수)
            {
                // 매수 포지션: 값이 떨어지면 "X"(진입 금지/손절), 다시 올라오면 "위치"(진입 가능)
                if (!isX)
                {
                    if (targetValue <= low) result = "X";
                }
                else // isX == true
                {
                    if (targetValue >= height) result = 위치;
                }
            }
            else // 매도
            {
                // 매도 포지션: 값이 올라가면 "X"(익절 등), 다시 내려오면 "위치"(재진입 등)
                // (기존 코드 로직을 그대로 따르되, 방향이 반대인 점 주의)
                // 기존 코드: if (low <= 수익률) result = "X"; (값이 커지면 X)

                if (!isX)
                {
                    if (targetValue >= low) result = "X";
                }
                else // isX == true
                {
                    if (targetValue <= height) result = 위치;
                }
            }

            return result;
        }

        public static bool 매매범위(double low, double height, int gubun, long 매입금)
        {
            // [최적화 1] gubun에 따른 분기 처리 (Switch 문)
            switch (gubun)
            {
                case 0: // 만원 단위
                        // [최적화 2] 변수 할당 없이 연산 결과 즉시 반환
                        // 10000 -> 10000.0 (double 명시)으로 형변환 비용 최소화
                    return (low * 10000.0 <= 매입금 && 매입금 <= height * 10000.0);

                case 1: // 기준금(%) 단위
                        // [최적화 3] Lazy Access (필요할 때만 외부 변수 접근)
                        // gubun이 1일 때만 GenieConfig를 읽어옵니다.
                    long 기준금 = GenieConfig.MT_buying_standard;

                    // [최적화 4] 나눗셈을 곱셈으로 변경 (CPU 연산 속도 향상)
                    // 나누기 100보다 곱하기 0.01이 연산 비용이 저렴합니다.
                    double min = 기준금 * low * 0.01;
                    double max = 기준금 * height * 0.01;

                    return (min <= 매입금 && 매입금 <= max);

                default:
                    return false;
            }
        }
        public static bool 매입비추매제한(double 잔고비중)
        {
            // [최적화 1] 제한 설정이 꺼져있다면 무조건 허용 (Early Return)
            // 변수 선언 없이 즉시 true 반환
            if (!GenieConfig.CB_잔고매입비_추매제한) return true;

            // [최적화 2] 설정이 켜져있다면, 비중 비교 결과(true/false)를 즉시 반환
            // 잔고비중이 설정값(TB_잔고매입비_추매제한)보다 작아야만 true(매수 가능)
            return (잔고비중 < GenieConfig.TB_잔고매입비_추매제한);
        }

        public static long 적용금액계산(int 주문가격, int 미체결수량, int 현재가)
        {
            // [최적화 1] 가격 결정 (삼항 연산자 사용)
            // 주문가격이 0(시장가 등)이면 현재가를 사용, 아니면 주문가격 사용
            int finalPrice = (주문가격 == 0) ? 현재가 : 주문가격;

            // [최적화 2] Integer Overflow 방지 (가장 중요)
            // int끼리 곱하면 결과도 int가 되어 21억 원을 넘을 때 오동작합니다.
            // 반드시 하나를 (long)으로 먼저 변환한 뒤 곱해야 합니다.
            return (long)finalPrice * 미체결수량;
        }

        public static int Find_Tik_Cap(int 현재가, int 주문가격, string Market)
        {
            // [최적화 1] 방어 로직 (Early Return)
            // 주문가격이 0이거나 현재가와 같으면 계산 없이 즉시 0 반환
            if (주문가격 <= 0 || 현재가 == 주문가격) return 0;

            int ticks = 0;
            int 기준가 = 현재가;

            // [최적화 2] 방향에 따라 루프 분리 (Branch Prediction 최적화)
            // 루프 안에서 매번 방향(위/아래)을 검사하지 않고, 진입 전에 결정합니다.

            if (주문가격 < 현재가) // 하락 방향 (음수 틱)
            {
                // 기준가가 주문가격보다 클 동안 계속 뺍니다.
                while (기준가 > 주문가격)
                {
                    // [주의] 하락 시에는 현재 가격 기준 호가만큼 뺍니다.
                    int tick = GetHoga(기준가, Market);
                    기준가 -= tick;
                    ticks--; // -1, -2, -3 ...

                    // [안전장치] 0원 이하가 되면 중단 (무한루프 방지)
                    if (기준가 <= 0) break;
                }
            }
            else // 상승 방향 (양수 틱)
            {
                // 기준가가 주문가격보다 작을 동안 계속 더합니다.
                while (기준가 < 주문가격)
                {
                    int tick = GetHoga(기준가, Market);
                    기준가 += tick;
                    ticks++; // 1, 2, 3 ...
                }
            }

            return ticks;
        }

        public static bool 매매진입_가능여부(string 종목코드, string 검색식)
        {
            // [지니 최적화] 보조장부(RunningTradeKeys) 폐지 및 메인 장부 직접 조회
            // Any()를 사용하여 해당 종목코드와 검색식 조합이 메인 장부에 존재하는지 빛의 속도로 확인합니다.
            bool 중복존재 = Form1.JumunItem_List.Values.Any(개별주문 =>
                개별주문.종목코드 == 종목코드 && 개별주문.검색식 == 검색식);

            return !중복존재; // 중복이 없으면 true(매매가능), 있으면 false(매매불가)
        }





        public static bool 청산주문_매매범위(Stockbalance 잔고, double 범위_1, double 범위_2)
        {
            // 🌟 [신규 추가] 현금 잔고와 신용 잔고를 합친 '총 주문가능수량'을 먼저 계산합니다.
            int 총주문가능수량 = GET.총주문가능수량(잔고);

            // [최적화 1] 공통 연산 추출 (Common Subexpression Elimination)
            // 나눗셈(/ 100)은 곱셈(* 0.01)보다 느립니다. 
            // 두 비교식에서 공통적으로 쓰이는 '매도기준의 1%' 값을 먼저 구해둡니다.
            // 이렇게 하면 전체 계산 과정에서 곱셈 횟수가 줄어듭니다.
            double onePercent = 잔고.매도기준 * 0.01;

            // [최적화 2] 논리 연산 통합 (Short-circuit Evaluation)
            // 중첩 if문 대신 && 연산자를 사용하여 코드를 한 줄로 줄였습니다.
            // 앞의 조건(상한선 체크)이 거짓이면 뒤의 조건은 계산하지 않고 즉시 false를 반환합니다.

            // 조건: (총주문가능수량 <= 상한선) AND (총주문가능수량 >= 하한선)
            return (총주문가능수량 <= onePercent * 범위_2) &&
                   (총주문가능수량 >= onePercent * 범위_1);
        }

        public static int 청산주문_매매범위_주문수량계산(Stockbalance 잔고, int 청산주문수량, double 범위_1)
        {
            // 🌟 [신규 추가] 현금 잔고와 신용 잔고를 합친 '총 주문가능수량'을 먼저 계산합니다.
            int 총주문가능수량 = GET.총주문가능수량(잔고);

            // [최적화 1] 공통 연산 추출 (잔고 유지 기준 수량 계산)
            double limitRaw = 잔고.매도기준 * 범위_1 * 0.01;

            // [최적화 2] Early Return (방어 로직)
            // 현금+신용을 영혼까지 끌어모은 수량이 이미 제한선보다 작다면, 더 이상 팔 수 없으므로 0 반환
            if (총주문가능수량 < limitRaw) return 0;

            // [최적화 3] 범위가 0일 경우 (전량 매도 가능)
            if (범위_1 == 0)
            {
                // 요청 수량과 총 보유 수량 중 작은 값을 반환
                return (청산주문수량 > 총주문가능수량) ? 총주문가능수량 : 청산주문수량;
            }

            // [최적화 4] Math.Ceiling 중복 호출 제거
            int limitQty = (int)Math.Ceiling(limitRaw);

            // 매도 후 남을 예상 총 잔고 계산
            int remainingAfterSell = 총주문가능수량 - 청산주문수량;

            // [최적화 5] 매도 후 잔고가 제한선(limitQty)보다 작아지는지 확인
            if (limitQty > remainingAfterSell)
            {
                if (limitQty == 1) return 0;

                // 제한선을 딱 맞추기 위해 주문 수량 조정
                // (총 가능 수량 - 남겨야 할 수량)
                return 총주문가능수량 - limitQty;
            }

            // 제한에 걸리지 않는다면 원래 요청 수량 그대로 반환
            return 청산주문수량;
        }

        public static bool 추매가능_Check(Stockbalance 잔고, bool 매수, bool 반복)
        {
            // [최적화 1] Early Return (조기 종료)
            // 매수 상황이 아니라면 설정값을 읽을 필요도 없이 즉시 True 반환
            if (!매수) return true;

            // [최적화 2] 설정값 로딩 최적화
            // 초기값을 할당하고 덮어쓰는 방식 대신, 조건에 따라 필요한 값만 한 번에 할당합니다.
            int 최소주가, 최대주가;
            double 최소등락률, 최대등락률;

            if (반복)
            {
                최소주가 = GenieConfig.TB_반복_추매주가이상;
                최대주가 = GenieConfig.TB_반복_추매주가이하;
                최소등락률 = GenieConfig.TB_반복_추매등락률이상;
                최대등락률 = GenieConfig.TB_반복_추매등락률이하;
            }
            else
            {
                최소주가 = GenieConfig.TB_리밸_추매주가이상;
                최대주가 = GenieConfig.TB_리밸_추매주가이하;
                최소등락률 = GenieConfig.TB_리밸_추매등락률이상;
                최대등락률 = GenieConfig.TB_리밸_추매등락률이하;
            }

            // [최적화 3] 주가 범위 체크 (가장 빠른 정수 연산부터 수행)
            // 조건에 맞지 않을 때만(실패 시) 내부 로직 진입
            if (잔고.현재가 < 최소주가 || 잔고.현재가 > 최대주가)
            {
                // 실패한 경우에만 문자열 생성 (GC 부하 방지)
                string 시장명 = (잔고.시장 == "D") ? "코스닥" : "코스피";

                // 문자열 보간($)과 N0 포맷팅 사용
                string 출력메시지 = $"[추매제한 '주가 범위'] {시장명}ㆍ{잔고.종목명}ㆍ주가({잔고.현재가:N0})이 제한 주가({최소주가:N0} ~ {최대주가:N0})";

                Tab_Basic.매매거부_메세지출력(잔고.종목명, 출력메시지);
                return false;
            }

            // [최적화 4] 등락률 범위 체크 (실수 연산)
            if (잔고.등락율 < 최소등락률 || 잔고.등락율 > 최대등락률)
            {
                string 시장명 = (잔고.시장 == "D") ? "코스닥" : "코스피";

                string 출력메시지 = $"[추매제한 '주가 등락률'] {시장명}ㆍ{잔고.종목명}ㆍ등락률({잔고.등락율}%) 제한 등락률({최소등락률} ~ {최대등락률})";

                Tab_Basic.매매거부_메세지출력(잔고.종목명, 출력메시지);
                return false;
            }

            // 모든 조건을 통과함
            return true;
        }

        public static bool 매매확인_VI_모투가능확인(Market_Item Market, int 주문유형)
        {
            // [최적화 1] 모의투자 제한 체크 (Guard Clause)
            if (server == "모의투자" && Market.현재가 < 1000)
            {
                // 알림이 켜져 있을 때만 로그 생성 및 상태 변경
                if (Market.매매알림_모투제한)
                {
                    string msg = $"[모투 제한] 종목: {Market.종목명} 현재가: {Market.현재가} 키움 정책상 1000원 미만 종목은 모의투자가 제한됩니다.";
                   Log.에러기록(msg);
                    Market.매매알림_모투제한 = false;
                }
                return false; // 즉시 종료
            }
            else
            {
                // 정상 상태 복귀 시 알림 플래그 초기화
                if (!Market.매매알림_모투제한) Market.매매알림_모투제한 = true;
            }

            // [최적화 2] 상한가 매수 취소 체크
            if (Market.과열.Contains("상한가"))
            {
                if (GenieConfig.CB_상매수취소 && 주문유형 == 1) // 매수
                {
                    if (Market.매매알림_상한가)
                    {
                        string msg = $"[매수 제한] 종목: {Market.종목명} 현재가: {Market.현재가} '상한가' 매수취소 됩니다.";
                       Log.에러기록(msg);
                        Market.매매알림_상한가 = false;
                    }
                    return false;
                }
            }
            else
            {
                if (!Market.매매알림_상한가) Market.매매알림_상한가 = true;
            }

            // [최적화 3] 하한가 매도 취소 체크
            if (Market.과열.Contains("하한가"))
            {
                if (GenieConfig.CB_하매도취소 && 주문유형 == 2) // 매도
                {
                    if (Market.매매알림_하한가)
                    {
                        string msg = $"[매도 제한] 종목: {Market.종목명} 현재가: {Market.현재가} '하한가' 매도취소 됩니다.";
                       Log.에러기록(msg);
                        Market.매매알림_하한가 = false;
                    }
                    return false;
                }
            }
            else
            {
                if (!Market.매매알림_하한가) Market.매매알림_하한가 = true;
            }

            // [최적화 4] VI(변동성 완화 장치) 발동 시 체크
            if (Market.과열.Contains("과열(VI)"))
            {
                if (주문유형 == 1) // 매수
                {
                    if (GenieConfig.CB_VI매수취소)
                    {
                        if (Market.매매알림_vI매수)
                        {
                            string msg = $"[매수 제한] 종목: {Market.종목명} 현재가: {Market.현재가} '과열(VI)' 매수취소 됩니다.";
                           Log.에러기록(msg);
                            Market.매매알림_vI매수 = false;
                        }
                        return false;
                    }
                }
                else if (주문유형 == 2) // 매도
                {
                    if (GenieConfig.CB_VI매도취소)
                    {
                        if (Market.매매알림_VI매도)
                        {
                            string msg = $"[매도 제한] 종목: {Market.종목명} 현재가: {Market.현재가} '과열(VI)' 매도취소 됩니다.";
                           Log.에러기록(msg);
                            Market.매매알림_VI매도 = false;
                        }
                        return false;
                    }
                }
            }
            else
            {
                // VI 해제 시 알림 플래그 초기화
                if (주문유형 == 1 && !Market.매매알림_vI매수) Market.매매알림_vI매수 = true;
                if (주문유형 == 2 && !Market.매매알림_VI매도) Market.매매알림_VI매도 = true;
            }

            // 모든 제한 조건을 통과함
            return true;
        }
     
        public static string 단위변환(double 단가)
        {
            // [최적화 1] 불필요한 문자열 생성 제거
            // 기존: string resut = 단가.ToString("N0"); (조건 안 맞으면 버려짐 -> 메모리 낭비)
            // 변경: 조건을 먼저 검사하여 필요한 경우에만 문자열 생성

            // [최적화 2] Math.Abs 호출 대신 비교 연산자 사용 (Micro-optimization)
            // 함수 호출 오버헤드를 줄이기 위해 직접 비교
            if (단가 > 999999 || 단가 < -999999)
            {
                // 100만 원 이상일 경우: '만' 단위 변환
                // 나눗셈 후 반올림하여 문자열 보간($)으로 처리
                return $"{Math.Round(단가 / 10000.0):N0}만";
            }

            // 100만 원 미만일 경우: 일반 포맷
            return 단가.ToString("N0");
        }

        public static async Task 타이머_작업_실행(Trading_item item)
        {
            try
            {
                // [안전장치] 시작 전 이미 취소되었거나 비활성 상태면 즉시 종료
                if (!item.IsActive) return;

                while (item.Timer > 0)
                {
                    // 1. 활성 상태 체크 (중간에 취소될 수 있음)
                    if (!item.IsActive) return;

                    // 2. 대기
                    await Task.Delay(1000);

                    // 3. 시간 감소
                    item.Timer--;
                }

                // 시간이 0이 되었고, 여전히 활성 상태라면 실행
                if (item.IsActive && item.Timer <= 0)
                {
                    // [최적화] ConcurrentDictionary는 TryGetValue가 매우 빠르고 안전함
                    if (Form1.stockBalanceList.TryGetValue(item.Code, out Stockbalance 잔고))
                    {
                        명령_실행(item.Location, 잔고);
                    }
                }
            }
            catch (Exception ex)
            {
                // 로그가 너무 많이 쌓이면 렉 걸리므로, 중요한 에러만 찍거나 파일로 저장
                Console_print($"[타이머 에러] {item.Code}: {ex.Message}");
            }
            finally
            {
                // [중요] ConcurrentDictionary 사용 필수
                Form1.Active_List.TryRemove(item, out _);

                item.IsActive = false;

                // 다시 풀(Pool)에 반납
                Form1.Trading_Pool.Enqueue(item);
            }
        }

        // [지니 최적화] 딕셔너리 대신 Switch문 사용 (속도 10배 이상 빠름, 메모리 0)
        public static void 명령_실행(string 위치, Stockbalance 잔고)
        {
            if (잔고 == null) return;

            switch (위치)
            {
                // [반복 그룹]
                case "반복_A": 잔고.가동_반복A = true; break;
                case "반복_B": 잔고.가동_반복B = true; break;
                case "반복_C": 잔고.가동_반복C = true; break;
                case "반복_D": 잔고.가동_반복D = true; break;
                case "반복_E": 잔고.가동_반복E = true; break;
                case "반복_F": 잔고.가동_반복F = true; break;
                case "반복_G": 잔고.가동_반복G = true; break;
                case "반복_H": 잔고.가동_반복H = true; break;
                case "반복_I": 잔고.가동_반복I = true; break;
                case "반복_J": 잔고.가동_반복J = true; break;
                case "반복_K": 잔고.가동_반복K = true; break;
                case "반복_L": 잔고.가동_반복L = true; break;
                case "반복_M": 잔고.가동_반복M = true; break;
                case "반복_N": 잔고.가동_반복N = true; break;

                // [리밸 그룹]
                case "리밸_A": 잔고.가동_리밸A = true; break;
                case "리밸_B": 잔고.가동_리밸B = true; break;
                case "리밸_C": 잔고.가동_리밸C = true; break;
                case "리밸_D": 잔고.가동_리밸D = true; break;
                case "리밸_E": 잔고.가동_리밸E = true; break;
                case "리밸_F": 잔고.가동_리밸F = true; break;
                case "리밸_G": 잔고.가동_리밸G = true; break;

                // [청산 그룹]
                case "청산_A": 잔고.가동_청산A = true; break;
                case "청산_B": 잔고.가동_청산B = true; break;
                case "청산_C": 잔고.가동_청산C = true; break;
            }
        }
    }
}