using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64
{
    public class GET : Form1
    {
        // =========================================================================
        // [+] 범용 시간 계산기 (HHmmss 형식의 정수 시간에 시, 분, 초를 안전하게 더하거나 뺍니다)
        // =========================================================================
        public static int 범용_시간계산(int 기준시간_HHmmss, int 더할_시간 = 0, int 더할_분 = 0, int 더할_초 = 0)
        {
            // 1. 6자리 숫자(HHmmss)를 시, 분, 초로 안전하게 분리
            int 시 = 기준시간_HHmmss / 10000;
            int 분 = (기준시간_HHmmss % 10000) / 100;
            int 초 = 기준시간_HHmmss % 100;

            // 2. C#의 내장 TimeSpan 객체를 활용하여 60진수 계산 오류 원천 차단
            TimeSpan 원본시간 = new TimeSpan(시, 분, 초);

            // 3. 사용자가 입력한 시, 분, 초를 더합니다. (음수를 넣으면 빼기가 됩니다)
            TimeSpan 계산된시간 = 원본시간
                .Add(TimeSpan.FromHours(더할_시간))
                .Add(TimeSpan.FromMinutes(더할_분))
                .Add(TimeSpan.FromSeconds(더할_초));

            // 4. 결과를 다시 지니 전용 6자리 정수(HHmmss)로 조립하여 반환
            return (계산된시간.Hours * 10000) + (계산된시간.Minutes * 100) + 계산된시간.Seconds;
        }

        public static int 총주문가능수량(Stockbalance 잔고)
        {
            return 잔고.주문가능수량 + 잔고.신용_주문가능수량;
        }

        // 콤보박스에서 선택된 인덱스를 반환하되, 선택이 안 되어 있으면(-1) 0을 반환하는 함수
        public static int ComboBoxIndex(ComboBox cb)
        {
            // 콤보박스 자체가 null인 경우 대비
            if (cb == null) return 0;

            int idx = cb.SelectedIndex;
            return (idx < 0) ? 0 : idx;
        }

        public static int GenieCombobox(int index)
        {
            return (index < 0) ? 0 : index;
        }


        public static void 신규매수방법()
        {
            // [최적화 1] 데이터 캐싱 (전역 변수 접근 최소화)
            // 메모리에서 값을 한 번만 읽어와서 지역 변수에 저장합니다.
            bool 사용_A = GenieConfig.CB_new_A;
            bool 사용_B = GenieConfig.CB_new_B;
            bool 사용_C = GenieConfig.CB_new_C;

            // [최적화 2] 상태 비트마스크(Bitmask) 생성
            // if문을 여러 번 타는 대신, 상태를 하나의 숫자로 만듭니다. (CPU 연산 속도 극대화)
            // A=4, B=2, C=1 가중치 부여 -> 0~7 사이의 숫자로 모든 경우의 수 표현 가능
            // 예: A, C 사용 = 4 + 0 + 1 = 5
            int 상태코드 = (사용_A ? 4 : 0) + (사용_B ? 2 : 0) + (사용_C ? 1 : 0);

            // 아무것도 선택하지 않은 경우(0) 빠른 종료
            if (상태코드 == 0) return;

            string 결과문자열 = "";

            // [최적화 3] 논리 연산자 상태 미리 계산 (필요한 경우만)
            // 짝수(0, 2)면 OR, 홀수(1, 3)면 AND
            bool 논리_B_OR = (GenieConfig.combo_new_or_B % 2 == 0);
            bool 논리_C_OR = (GenieConfig.combo_new_or_C % 2 == 0);

            // [최적화 4] Switch 문을 이용한 O(1) 점프 테이블
            switch (상태코드)
            {
                case 4: // A만 사용 (100)
                    결과문자열 = "A";
                    break;

                case 2: // B만 사용 (010)
                    결과문자열 = "B";
                    break;

                case 1: // C만 사용 (001)
                    결과문자열 = "C";
                    break;

                case 6: // A, B 사용 (110)
                    결과문자열 = 논리_B_OR ? "AoB" : "AnB";
                    break;

                case 5: // A, C 사용 (101) - B 건너뜀
                        // 원본 로직 유지: A와 C 연결 시 C의 콤보박스 설정 사용
                    결과문자열 = 논리_C_OR ? "AoC" : "AnC";
                    break;

                case 3: // B, C 사용 (011)
                    결과문자열 = 논리_C_OR ? "BoC" : "BnC";
                    break;

                case 7: // A, B, C 모두 사용 (111)
                    if (논리_B_OR)
                    {
                        // A or B ...
                        결과문자열 = 논리_C_OR ? "AoBoC" : "AoBnC";
                    }
                    else
                    {
                        // A and B ...
                        결과문자열 = 논리_C_OR ? "AnBoC" : "AnBnC";
                    }
                    break;
            }

            // 결과 적용
            str.신규매수방법 = 결과문자열;
        }

        public static string 매수매도str(int 매수매도)
        {
            // [최적화 1] 스위치 표현식 (Switch Expression) 사용
            // C# 컴파일러가 최적화된 점프 테이블을 생성하여 CPU 분기 예측 효율을 극대화합니다.
            // 변수 할당 과정 없이 결과값을 즉시 반환(Return)하므로 가장 빠릅니다.

            return 매수매도 switch
            {
                1 => "매수",
                2 => "매도",
                10 => "수취소",
                20 => "도취소",
                _ => "" // 그 외의 값(default)은 빈 문자열 반환
            };
        }

        public static string ScreenNum() // 1101 ~ 1299
        {
            // [최적화 1] 전역 변수 접근 최소화 (지역 변수 캐싱)
            // Form1.스크린Num을 매번 부르는 것보다, 지역 변수(int)에 담아 계산하는 것이 훨씬 빠릅니다.
            int nextNum = Form1.스크린Num + 1;

            // [최적화 2] 범위 초과 시 리셋 (단순 대입)
            if (nextNum > 1299)
            {
                nextNum = 1100; // 1300이 되면 1100으로 초기화 (기존 로직 유지)
            }

            // [최적화 3] 전역 변수 업데이트 (단 1회 수행)
            Form1.스크린Num = nextNum;

            // 문자열 변환 후 반환
            return nextNum.ToString();
        }

        public static int Start_stop_time(bool start, int time)
        {
            int result = time;

            if (start)
            {
                if (GenieConfig.CB_NXT)
                {
                    if (result < 80000) result = 80000;
                    if (result > 200000) result = 80000;
                }
                else
                {
                    if (result < 90000) result = 90000;
                    if (result > 153000) result = 90000;
                }
            }
            else
            {
                if (GenieConfig.CB_NXT)
                {
                    if (result < 80000) result = 200000;
                    if (result > 200000) result = 200000;
                }
                else
                {
                    if (result < 85000) result = 152500;

                    if (Form1.CB수능일)
                    {
                        if (result > 163000) result = 162500;
                    }
                    else
                    {
                        if (result > 153000) result = 152500;
                    }
                }
            }

            return result;
        }


        public static void StockState(string itemcode, string 호가매수1, string 호가매수4, string 호가매수5, string 호가매수6, string 호가매수7, string 호가매도1, string 호가매도4, string 호가매도5, string 호가매도6, string 호가매도7)
        {
            UnifiedDataManager.Instance.RealData.Enqueue(() =>
            {
                if (Form1.Market_Item_List.ContainsKey(itemcode))
                {
                    Market_Item Market = Form1.Market_Item_List[itemcode];

                    //Form1.Console_print($"스톡업데이트 종목명 : {Market.종목명} 호가매수1:{호가매수1} 호가매수1:{호가매도1}");

                    int.TryParse(호가매수1, out int 매수1);
                    int.TryParse(호가매수4, out int 매수4);
                    int.TryParse(호가매수5, out int 매수5);
                    int.TryParse(호가매수6, out int 매수6);
                    int.TryParse(호가매수7, out int 매수7);
                    int.TryParse(호가매도1, out int 매도1);
                    int.TryParse(호가매도4, out int 매도4);
                    int.TryParse(호가매도5, out int 매도5);
                    int.TryParse(호가매도6, out int 매도6);
                    int.TryParse(호가매도7, out int 매도7);

                    long 매수47합 = 매수4 + 매수5 + 매수6 + 매수7;
                    long 매도47합 = 매도4 + 매도5 + 매도6 + 매도7;
                    bool is단일가매매 = (매수47합 == 0 && 매도47합 == 0);

                    try
                    {
                        if (Form1.stockBalanceList.TryGetValue(itemcode, out Stockbalance 잔고))
                        {
                            if (Form1.server_알림 == "메인마켓" && 잔고.종목상태.Contains("거래정지"))
                            {
                                if (is단일가매매)
                                {
                                    TR_요청.종목정보조회(잔고.종목코드, false);
                                }
                            }

                            if (Form1.server_알림 == "메인마켓")
                            {
                                if (!잔고.종목상태.Contains("거래정지"))//!잔고.매매가능 && 
                                {
                                    if (잔고.종목상태.Contains("시세로딩") || 잔고.종목상태.Contains("마켓대기") || 잔고.종목상태.Contains("장전동시"))
                                    {
                                        if (is단일가매매 || (Get.TimeNow >= Get.메인마켓시작 + 200))
                                        {
                                            잔고.매매가능 = true;
                                            잔고.잔고청산 = true;
                                            잔고.종목상태 = Jango_state(잔고.종목코드);
                                        }
                                    }
                                }
                            }

                            if (!잔고.종목상태.Contains("시세로딩") && !잔고.종목상태.Contains("마켓대기") && !잔고.종목상태.Contains("장전동시"))
                            {
                                if (is단일가매매)
                                {
                                    if (Get.TimeNow < Get.메인마켓종료)
                                    {
                                        if (Form1.server_알림.Equals("동시호가"))
                                        {
                                            잔고.종목상태 = "동시호가";
                                        }
                                        else
                                        {
                                            if (Get.메인마켓시작 + 10 <= Get.TimeNow) 잔고.종목상태 = "과열(VI)";
                                        }
                                    }
                                }
                                else
                                {
                                    if (매도1 == 0 && 매도47합 == 0)
                                    {
                                        잔고.종목상태 = "상한가";
                                    }
                                    else
                                    {
                                        if (매수1 == 0 && 매수47합 == 0)
                                        {
                                            잔고.종목상태 = "하한가";
                                        }
                                        else
                                        {
                                            if (잔고.종목상태.Equals("과열(VI)") || 잔고.종목상태.Equals("상한가") || 잔고.종목상태.Equals("하한가"))
                                            {
                                                if (NXT_server)
                                                {
                                                    if (NXT_list.Contains(itemcode))
                                                    {
                                                        잔고.종목상태 = Jango_state(itemcode);
                                                    }
                                                    else
                                                    {
                                                        잔고.종목상태 = "마켓종료";
                                                    }
                                                }
                                                else
                                                {
                                                    잔고.종목상태 = Jango_state(itemcode);
                                                }
                                            }
                                        }
                                    }

                                    if (매도1 == 0 && 매수1 == 0 && is단일가매매) // 장시작전
                                    {
                                        잔고.종목상태 = "마켓종료";
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        Log.에러기록("호가에러:: " + Market.종목명 + " 잔고에 없습니다.");
                    }

                    try
                    {
                        if (is단일가매매)
                        {
                            if (Form1.server_알림.Equals("장전종시")) // 10분전 동시효과
                            {
                                Market.과열 = "장전동시";
                            }
                            else
                            {
                                if (Get.TimeNow < Get.메인마켓종료)
                                {
                                    if (Form1.server_알림.Equals("동시호가"))
                                    {
                                        Market.과열 = "동시호가";
                                    }
                                    else
                                    {
                                        if (Get.메인마켓시작 + 10 <= Get.TimeNow) Market.과열 = "과열(VI)";
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (매도1 == 0 && 매도47합 == 0)
                            {
                                Market.과열 = "상한가";
                            }
                            else
                            {
                                if (매수1 == 0 && 매수47합 == 0)
                                {
                                    Market.과열 = "하한가";
                                }
                                else
                                {
                                    Market.과열 = "정상";
                                }
                            }
                        }
                    }
                    catch
                    {
                        Log.에러기록("호가에러:: " + Market.종목명 + " 종목상태 확인에러.");
                    }
                }
            });
        }


        public static int Order번호()
        {
            // [대기] 다른 스레드가 사용 중이면 여기서 멈춰서 기다립니다.
            lock (OrderLock)
            {
                // 1. [초기화] 프로그램 시작 후 '최초 1회'만 실행
                if (!_isOrderInitialized)
                {
                    if (JumunItem_List.Count > 0)
                    {
                        try
                        {
                            // 딕셔너리에서 가장 큰 주문번호를 가져와서 시작점으로 잡습니다.
                            int maxOrder = JumunItem_List.Values
                                                 .Max(o => o.Order번호);

                            Order번호_ = maxOrder;
                        }
                        catch
                        {
                            // 혹시라도 에러가 나면 0부터 시작 (안전장치)
                            Order번호_ = 0;
                        }
                    }
                    else
                    {
                        // 데이터가 없으면 0으로 셋팅
                        Order번호_ = 0;
                    }

                    _isOrderInitialized = true; // 초기화 완료 도장 쾅!
                }

                // 2. [증가] 
                // lock 블록 안에서는 나 혼자만 실행하므로 Interlocked가 없어도 안전합니다.
                // 기존값(Max)에서 1을 더해 새로운 번호를 만듭니다.
                Order번호_++;

                // 3. [반환]
                return Order번호_;
            }
        }

        public static string JumunScreen()
        {
            // lock 블록 시작: 이 안에는 한 번에 하나의 스레드만 들어올 수 있습니다.
            lock (Jumun_screen_ScreenLock)
            {
                // [A] 프로그램 시작 후 '최초 1회'만 실행되는 초기화 로직
                // (단순히 1000이라고 실행하면, 나중에 1300 찍고 1000으로 돌아왔을 때 또 실행되는 버그 방지)
                if (!Jumun_screen_isInitialized)
                {
                    if (JumunItem_List.Count > 0)
                    {
                        try
                        {
                            // 딕셔너리에서 현재 사용 중인 가장 큰 번호를 찾음
                            int maxScreen = JumunItem_List.Values
                                                 .Select(o => int.Parse(o.Screennum))
                                                 .Max();

                            // 그 다음 번호로 세팅
                            Jumun_screen = maxScreen + 1;
                        }
                        catch
                        {
                            // 파싱 에러 등 발생 시 안전하게 기본값 유지
                            Jumun_screen = 1000;
                        }
                    }
                    else
                    {
                        Jumun_screen = 1000;
                    }

                    Jumun_screen_isInitialized = true; // 초기화 완료 도장 쾅!
                }
                else
                {
                    // [B] 초기화 이후에는 단순히 1씩 증가
                    Jumun_screen++;
                }

                // [C] 범위 체크 (1300번이 되면 다시 1000번으로 순환)
                // 1000 ~ 1299번(총 300개)을 사용하게 됩니다.
                if (Jumun_screen >= 1300)
                {
                    Jumun_screen = 1000;
                }

                // 결과 반환
                return Jumun_screen.ToString();
            } // lock 끝: 이제 다음 스레드가 들어올 수 있음
        }


        public static string 시장가구분(int 시장가구분)
        {
            return (시장가구분 == 0) ? "시장가" : "보통가";
        }


        public static string Jango_state(string 종목코드) // 종목상태 요청
        {
            // [최적화 1] 안전한 데이터 조회
            if (!Form1.Market_Item_List.TryGetValue(종목코드, out Market_Item 아이템))
            {
                return "정보없음";
            }

            string 상태 = 아이템.state;
            bool 신용가능 = 아이템.신용가능;

            // [최적화 2] "거래정지" 우선 확인
            if (상태.IndexOf("거래정지", StringComparison.Ordinal) >= 0)
            {
                return "거래정지";
            }

            // [최적화 3] Zero-Allocation 증거금률 파싱
            // 예: "A40신용가능" -> [3]='4', [4]='0' => 40
            int 증거금률 = (상태[3] - '0') * 10 + (상태[4] - '0');

            // 10인 경우 100으로 보정
            if (증거금률 == 10)
            {
                증거금률 = 100;
            }

            // [최적화 4] 조건부 문자열 처리
            string 신 = (상태.IndexOf("신용가능", StringComparison.Ordinal) >= 0) ? "가" : "불";

            // NXT 리스트 확인
            string nxt가능 = NXT_list.Contains(종목코드) ? "Y" : "X";

            // 최종 결과값 생성
            string 결과 = $"{nxt가능}｜{신}｜{증거금률}";
            if(신용가능 == false) 결과 = $"{nxt가능}｜초｜{증거금률}";

            return 결과;
        }

        public static string Cansel_order_string(int index)
        {
            // [최적화] 스위치 표현식 (C# 8.0+)
            // 변수 선언(string after_order) 및 대입 과정을 제거하고,
            // 입력값에 맞는 문자열 상수(Literal)를 메모리에서 즉시 반환합니다.

            return index switch
            {
                0 => "주문취소",
                1 => "C 시장가",
                2 => "C 현재가",
                3 => "C 재주문",
                _ => "" // 그 외의 값은 빈 문자열 반환
            };
        }

        public static string After_order_string(int 시장가구분, double Value)
        {
            // [최적화] 스위치 표현식 (Switch Expression)
            // 변수 선언(string after_order) 및 할당 과정을 생략하고, 
            // 조건에 맞는 결과값을 메모리에서 즉시 반환(Return)합니다.

            return 시장가구분 switch
            {
                0 => "시장가",
                1 => "현재가",
                2 => Value + " 틱", // 문자열 연결 비용 최소화
                3 => Value + " %",
                4 => "A 분할",
                5 => "B 분할",
                6 => "C 분할",
                _ => "" // 그 외의 값은 빈 문자열 반환
            };
        }

      

        public static string 전광판현황(Stockbalance 잔고, string 위치)
        {
            // [최적화 1] StringBuilder 사용 (문자열 연결 속도 및 메모리 효율 극대화)
            StringBuilder sb = new StringBuilder(30); // 예상 길이에 맞춰 초기 용량 설정

            // [최적화 2] 오늘 날짜 캐싱 (반복 호출 방지)
            string today = DateTime.Now.ToString("yyyy/MM/dd");

            // [최적화 3] 공통 조건 미리 계산 (Boolean Caching)
            // 각 케이스에서 반복되는 조건들을 미리 계산해두면 CPU가 편해집니다.
            bool isIj = (위치 == "익절");
            bool isBj = (위치 == "보전");
            bool isIh = (위치 == "일회");
            bool isSj = (위치 == "손절");
            bool isRb = (위치 == "리밸");
            bool isRp = (위치 == "반복");
            bool isJc = (위치 == "잔고청산");
            bool isTc = (위치 == "시간청산");

            // [최적화 4] 로컬 함수 단순화
            // 람다 식이나 로컬 함수 대신, 루프 안에서 처리하거나 간결한 헬퍼 메서드로 분리하는 게 좋지만,
            // 여기서는 가독성을 위해 로컬 함수 구조를 유지하되 내부 로직을 최적화함.
            string GetStatus(string code)
            {
                // 기본값 "-"
                // 조건에 맞으면 해당 코드(A, B...) 반환

                switch (code)
                {
                    case "A":
                        if (isIj && (!잔고.익절A || !잔고.TS_1)) return "A";
                        if (isBj && 잔고.보전A == "3") return "A";
                        if (isIh && 잔고.일회A.Contains(today)) return "A";
                        if (isSj && !잔고.손절A) return "A";
                        if (isRb && 잔고.리벨A == "X") return "A";
                        if (isRp && 잔고.반복A == "X") return "A";
                        if (isJc && 잔고.청산A == "X") return "A";
                        if (isTc && 잔고.시간청산동작_A == "X") return "A";
                        break;
                    case "B":
                        if (isIj && (!잔고.익절B || !잔고.TS_2)) return "B";
                        if (isBj && 잔고.보전B == "3") return "B";
                        if (isIh && 잔고.일회B.Contains(today)) return "B";
                        if (isSj && !잔고.손절B) return "B";
                        if (isRb && 잔고.리벨B == "X") return "B";
                        if (isRp && 잔고.반복B == "X") return "B";
                        if (isJc && 잔고.청산B == "X") return "B";
                        if (isTc && 잔고.시간청산동작_B == "X") return "B";
                        break;
                    case "C":
                        if (isIj && (!잔고.익절C || !잔고.TS_3)) return "C";
                        if (isBj && 잔고.보전C == "3") return "C";
                        if (isIh && 잔고.일회C.Contains(today)) return "C";
                        if (isSj && !잔고.손절C) return "C";
                        if (isRb && 잔고.리벨C == "X") return "C";
                        if (isRp && 잔고.반복C == "X") return "C";
                        if (isJc && 잔고.청산C == "X") return "C";
                        if (isTc && 잔고.시간청산동작_C == "X") return "C";
                        break;
                    // D~F 공통 (손절, 리밸, 반복 포함)
                    case "D":
                        if (isIj && (!잔고.익절D || !잔고.TS_4)) return "D";
                        if (isBj && 잔고.보전D == "3") return "D";
                        if (isIh && 잔고.일회D.Contains(today)) return "D";
                        if (isSj && !잔고.손절D) return "D";
                        if (isRb && 잔고.리벨D == "X") return "D";
                        if (isRp && 잔고.반복D == "X") return "D";
                        break;
                    case "E":
                        if (isIj && (!잔고.익절E || !잔고.TS_5)) return "E";
                        if (isBj && 잔고.보전E == "3") return "E";
                        if (isIh && 잔고.일회E.Contains(today)) return "E";
                        if (isSj && !잔고.손절E) return "E";
                        if (isRb && 잔고.리벨E == "X") return "E";
                        if (isRp && 잔고.반복E == "X") return "E";
                        break;
                    case "F":
                        if (isIj && (!잔고.익절F || !잔고.TS_6)) return "F";
                        if (isBj && 잔고.보전F == "3") return "F";
                        if (isIh && 잔고.일회F.Contains(today)) return "F";
                        if (isSj && !잔고.손절F) return "F";
                        if (isRb && 잔고.리벨F == "X") return "F";
                        if (isRp && 잔고.반복F == "X") return "F";
                        break;
                    // G (리밸, 반복 포함)
                    case "G":
                        if (isIj && (!잔고.익절G || !잔고.TS_7)) return "G";
                        if (isBj && 잔고.보전G == "3") return "G";
                        if (isIh && 잔고.일회G.Contains(today)) return "G";
                        if (isRb && 잔고.리벨G == "X") return "G";
                        if (isRp && 잔고.반복G == "X") return "G";
                        break;
                    // H~I (반복 포함)
                    case "H":
                        if (isIj && (!잔고.익절H || !잔고.TS_8)) return "H";
                        if (isBj && 잔고.보전H == "3") return "H";
                        if (isIh && 잔고.일회H.Contains(today)) return "H";
                        if (isRp && 잔고.반복H == "X") return "H";
                        break;
                    case "I":
                        if (isIj && (!잔고.익절I || !잔고.TS_9)) return "I";
                        if (isBj && 잔고.보전I == "3") return "I";
                        if (isIh && 잔고.일회I.Contains(today)) return "I";
                        if (isRp && 잔고.반복I == "X") return "I";
                        break;
                    // J~N (반복 전용)
                    case "J": if (isRp && 잔고.반복J == "X") return "J"; break;
                    case "K": if (isRp && 잔고.반복K == "X") return "K"; break;
                    case "L": if (isRp && 잔고.반복L == "X") return "L"; break;
                    case "M": if (isRp && 잔고.반복M == "X") return "M"; break;
                    case "N": if (isRp && 잔고.반복N == "X") return "N"; break;
                }
                return "-";
            }

            // [최적화 5] StringBuilder로 문자열 조립 (A.B.C... 구조)
            // 필요한 만큼만 루프를 돌거나 추가합니다.

            // 기본적으로 A, B, C는 모든 모드에서 사용됨
            sb.Append(GetStatus("A")).Append('.').Append(GetStatus("B")).Append('.').Append(GetStatus("C"));

            if (isJc || isTc) // 잔고청산, 시간청산 (A~C 끝)
            {
                return sb.ToString();
            }

            sb.Append('.').Append(GetStatus("D")).Append('.').Append(GetStatus("E")).Append('.').Append(GetStatus("F"));

            if (isSj) // 손절 (A~F 끝)
            {
                return sb.ToString();
            }

            sb.Append('.').Append(GetStatus("G"));

            if (isRb) // 리밸 (A~G 끝)
            {
                return sb.ToString();
            }

            sb.Append('.').Append(GetStatus("H")).Append('.').Append(GetStatus("I"));

            if (isRp) // 반복 (A~N 끝)
            {
                sb.Append('.').Append(GetStatus("J"))
                  .Append('.').Append(GetStatus("K"))
                  .Append('.').Append(GetStatus("L"))
                  .Append('.').Append(GetStatus("M"))
                  .Append('.').Append(GetStatus("N"));
            }

            return sb.ToString();
        }

        public static string 요일가져오기()
        {
            // [최적화] 스위치 표현식 (Switch Expression)
            // if-else를 7번 비교하는 대신, 컴파일러 최적화(Jump Table)를 통해 
            // 해당 요일로 즉시 이동하여 값을 반환합니다.
            // 변수 선언 없이 메모리에서 바로 값을 던져주므로 가장 빠릅니다.

            return DateTime.Now.DayOfWeek switch
            {
                DayOfWeek.Monday => "월요일",
                DayOfWeek.Tuesday => "화요일",
                DayOfWeek.Wednesday => "수요일",
                DayOfWeek.Thursday => "목요일",
                DayOfWeek.Friday => "금요일",
                DayOfWeek.Saturday => "토요일",
                DayOfWeek.Sunday => "일요일",
                _ => "" // 예외 처리 (발생할 일 없음)
            };
        }

        public static string 익절그룹(string 위치)
        {
            // [최적화 1] StringBuilder 사용 (메모리 할당 0)
            // 기존: "0" + "A" + "B"... 할 때마다 새로운 문자열 객체 생성 (GC 부하 큼)
            // 변경: 내부 버퍼에서 조립하므로 메모리 낭비가 전혀 없음. 
            // 초기 용량을 13("0"+A~L)으로 잡아 불필요한 버퍼 확장도 방지함.
            StringBuilder sb = new StringBuilder("0", 13);

            switch (위치)
            {
                case "미수금정리":
                    if (GenieConfig.CB_미수금정리_A) sb.Append('A');
                    if (GenieConfig.CB_미수금정리_B) sb.Append('B');
                    if (GenieConfig.CB_미수금정리_C) sb.Append('C');
                    if (GenieConfig.CB_미수금정리_D) sb.Append('D');
                    if (GenieConfig.CB_미수금정리_E) sb.Append('E');
                    if (GenieConfig.CB_미수금정리_F) sb.Append('F');
                    if (GenieConfig.CB_미수금정리_G) sb.Append('G');
                    if (GenieConfig.CB_미수금정리_H) sb.Append('H');
                    if (GenieConfig.CB_미수금정리_I) sb.Append('I');
                    if (GenieConfig.CB_미수금정리_J) sb.Append('J');
                    if (GenieConfig.CB_미수금정리_K) sb.Append('K');
                    if (GenieConfig.CB_미수금정리_L) sb.Append('L');
                    break;

                case "익절":
                    if (GenieConfig.CB_IK_group_A) sb.Append('A');
                    if (GenieConfig.CB_IK_group_B) sb.Append('B');
                    if (GenieConfig.CB_IK_group_C) sb.Append('C');
                    if (GenieConfig.CB_IK_group_D) sb.Append('D');
                    if (GenieConfig.CB_IK_group_E) sb.Append('E');
                    if (GenieConfig.CB_IK_group_F) sb.Append('F');
                    if (GenieConfig.CB_IK_group_G) sb.Append('G');
                    if (GenieConfig.CB_IK_group_H) sb.Append('H');
                    if (GenieConfig.CB_IK_group_I) sb.Append('I');
                    if (GenieConfig.CB_IK_group_J) sb.Append('J');
                    if (GenieConfig.CB_IK_group_K) sb.Append('K');
                    if (GenieConfig.CB_IK_group_L) sb.Append('L');
                    break;

                case "손절":
                    if (GenieConfig.CB_손절_A) sb.Append('A');
                    if (GenieConfig.CB_손절_B) sb.Append('B');
                    if (GenieConfig.CB_손절_C) sb.Append('C');
                    if (GenieConfig.CB_손절_D) sb.Append('D');
                    if (GenieConfig.CB_손절_E) sb.Append('E');
                    if (GenieConfig.CB_손절_F) sb.Append('F');
                    if (GenieConfig.CB_손절_G) sb.Append('G');
                    if (GenieConfig.CB_손절_H) sb.Append('H');
                    if (GenieConfig.CB_손절_I) sb.Append('I');
                    if (GenieConfig.CB_손절_J) sb.Append('J');
                    if (GenieConfig.CB_손절_K) sb.Append('K');
                    if (GenieConfig.CB_손절_L) sb.Append('L');
                    break;

                case "특정시간_청산":
                    if (GenieConfig.CB_특정시간_계좌청산_A) sb.Append('A');
                    if (GenieConfig.CB_특정시간_계좌청산_B) sb.Append('B');
                    if (GenieConfig.CB_특정시간_계좌청산_C) sb.Append('C');
                    if (GenieConfig.CB_특정시간_계좌청산_D) sb.Append('D');
                    if (GenieConfig.CB_특정시간_계좌청산_E) sb.Append('E');
                    if (GenieConfig.CB_특정시간_계좌청산_F) sb.Append('F');
                    if (GenieConfig.CB_특정시간_계좌청산_G) sb.Append('G');
                    if (GenieConfig.CB_특정시간_계좌청산_H) sb.Append('H');
                    if (GenieConfig.CB_특정시간_계좌청산_I) sb.Append('I');
                    if (GenieConfig.CB_특정시간_계좌청산_J) sb.Append('J');
                    if (GenieConfig.CB_특정시간_계좌청산_K) sb.Append('K');
                    if (GenieConfig.CB_특정시간_계좌청산_L) sb.Append('L');
                    break;

                case "실현손익_청산":
                    if (GenieConfig.CB_실현손익_계좌청산_A) sb.Append('A');
                    if (GenieConfig.CB_실현손익_계좌청산_B) sb.Append('B');
                    if (GenieConfig.CB_실현손익_계좌청산_C) sb.Append('C');
                    if (GenieConfig.CB_실현손익_계좌청산_D) sb.Append('D');
                    if (GenieConfig.CB_실현손익_계좌청산_E) sb.Append('E');
                    if (GenieConfig.CB_실현손익_계좌청산_F) sb.Append('F');
                    if (GenieConfig.CB_실현손익_계좌청산_G) sb.Append('G');
                    if (GenieConfig.CB_실현손익_계좌청산_H) sb.Append('H');
                    if (GenieConfig.CB_실현손익_계좌청산_I) sb.Append('I');
                    if (GenieConfig.CB_실현손익_계좌청산_J) sb.Append('J');
                    if (GenieConfig.CB_실현손익_계좌청산_K) sb.Append('K');
                    if (GenieConfig.CB_실현손익_계좌청산_L) sb.Append('L');
                    break;

                case "예상손실_청산":
                    if (GenieConfig.CB_예상손실_계좌청산_A) sb.Append('A');
                    if (GenieConfig.CB_예상손실_계좌청산_B) sb.Append('B');
                    if (GenieConfig.CB_예상손실_계좌청산_C) sb.Append('C');
                    if (GenieConfig.CB_예상손실_계좌청산_D) sb.Append('D');
                    if (GenieConfig.CB_예상손실_계좌청산_E) sb.Append('E');
                    if (GenieConfig.CB_예상손실_계좌청산_F) sb.Append('F');
                    if (GenieConfig.CB_예상손실_계좌청산_G) sb.Append('G');
                    if (GenieConfig.CB_예상손실_계좌청산_H) sb.Append('H');
                    if (GenieConfig.CB_예상손실_계좌청산_I) sb.Append('I');
                    if (GenieConfig.CB_예상손실_계좌청산_J) sb.Append('J');
                    if (GenieConfig.CB_예상손실_계좌청산_K) sb.Append('K');
                    if (GenieConfig.CB_예상손실_계좌청산_L) sb.Append('L');
                    break;

                case "예상수익_청산":
                    if (GenieConfig.CB_예상수익_계좌청산_A) sb.Append('A');
                    if (GenieConfig.CB_예상수익_계좌청산_B) sb.Append('B');
                    if (GenieConfig.CB_예상수익_계좌청산_C) sb.Append('C');
                    if (GenieConfig.CB_예상수익_계좌청산_D) sb.Append('D');
                    if (GenieConfig.CB_예상수익_계좌청산_E) sb.Append('E');
                    if (GenieConfig.CB_예상수익_계좌청산_F) sb.Append('F');
                    if (GenieConfig.CB_예상수익_계좌청산_G) sb.Append('G');
                    if (GenieConfig.CB_예상수익_계좌청산_H) sb.Append('H');
                    if (GenieConfig.CB_예상수익_계좌청산_I) sb.Append('I');
                    if (GenieConfig.CB_예상수익_계좌청산_J) sb.Append('J');
                    if (GenieConfig.CB_예상수익_계좌청산_K) sb.Append('K');
                    if (GenieConfig.CB_예상수익_계좌청산_L) sb.Append('L');
                    break;

                case "잔고시간청산_A":
                    if (GenieConfig.CB_시간청산A_A) sb.Append('A');
                    if (GenieConfig.CB_시간청산A_B) sb.Append('B');
                    if (GenieConfig.CB_시간청산A_C) sb.Append('C');
                    if (GenieConfig.CB_시간청산A_D) sb.Append('D');
                    if (GenieConfig.CB_시간청산A_E) sb.Append('E');
                    if (GenieConfig.CB_시간청산A_F) sb.Append('F');
                    if (GenieConfig.CB_시간청산A_G) sb.Append('G');
                    if (GenieConfig.CB_시간청산A_H) sb.Append('H');
                    if (GenieConfig.CB_시간청산A_I) sb.Append('I');
                    if (GenieConfig.CB_시간청산A_J) sb.Append('J');
                    if (GenieConfig.CB_시간청산A_K) sb.Append('K');
                    if (GenieConfig.CB_시간청산A_L) sb.Append('L');
                    break;

                case "잔고시간청산_B":
                    if (GenieConfig.CB_시간청산B_A) sb.Append('A');
                    if (GenieConfig.CB_시간청산B_B) sb.Append('B');
                    if (GenieConfig.CB_시간청산B_C) sb.Append('C');
                    if (GenieConfig.CB_시간청산B_D) sb.Append('D');
                    if (GenieConfig.CB_시간청산B_E) sb.Append('E');
                    if (GenieConfig.CB_시간청산B_F) sb.Append('F');
                    if (GenieConfig.CB_시간청산B_G) sb.Append('G');
                    if (GenieConfig.CB_시간청산B_H) sb.Append('H');
                    if (GenieConfig.CB_시간청산B_I) sb.Append('I');
                    if (GenieConfig.CB_시간청산B_J) sb.Append('J');
                    if (GenieConfig.CB_시간청산B_K) sb.Append('K');
                    if (GenieConfig.CB_시간청산B_L) sb.Append('L');
                    break;

                case "잔고시간청산_C":
                    if (GenieConfig.CB_시간청산C_A) sb.Append('A');
                    if (GenieConfig.CB_시간청산C_B) sb.Append('B');
                    if (GenieConfig.CB_시간청산C_C) sb.Append('C');
                    if (GenieConfig.CB_시간청산C_D) sb.Append('D');
                    if (GenieConfig.CB_시간청산C_E) sb.Append('E');
                    if (GenieConfig.CB_시간청산C_F) sb.Append('F');
                    if (GenieConfig.CB_시간청산C_G) sb.Append('G');
                    if (GenieConfig.CB_시간청산C_H) sb.Append('H');
                    if (GenieConfig.CB_시간청산C_I) sb.Append('I');
                    if (GenieConfig.CB_시간청산C_J) sb.Append('J');
                    if (GenieConfig.CB_시간청산C_K) sb.Append('K');
                    if (GenieConfig.CB_시간청산C_L) sb.Append('L');
                    break;

                case "반복_A":
                    if (GenieConfig.CB_Repeat_A_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_A_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_A_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_A_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_A_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_A_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_A_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_A_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_A_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_A_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_A_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_A_L) sb.Append('L');
                    break;

                case "반복_B":
                    if (GenieConfig.CB_Repeat_B_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_B_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_B_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_B_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_B_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_B_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_B_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_B_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_B_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_B_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_B_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_B_L) sb.Append('L');
                    break;

                case "반복_C":
                    if (GenieConfig.CB_Repeat_C_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_C_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_C_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_C_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_C_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_C_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_C_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_C_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_C_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_C_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_C_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_C_L) sb.Append('L');
                    break;

                case "반복_D":
                    if (GenieConfig.CB_Repeat_D_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_D_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_D_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_D_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_D_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_D_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_D_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_D_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_D_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_D_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_D_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_D_L) sb.Append('L');
                    break;

                case "반복_E":
                    if (GenieConfig.CB_Repeat_E_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_E_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_E_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_E_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_E_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_E_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_E_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_E_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_E_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_E_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_E_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_E_L) sb.Append('L');
                    break;

                case "반복_F":
                    if (GenieConfig.CB_Repeat_F_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_F_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_F_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_F_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_F_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_F_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_F_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_F_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_F_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_F_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_F_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_F_L) sb.Append('L');
                    break;

                case "반복_G":
                    if (GenieConfig.CB_Repeat_G_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_G_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_G_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_G_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_G_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_G_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_G_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_G_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_G_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_G_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_G_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_G_L) sb.Append('L');
                    break;

                case "반복_H":
                    if (GenieConfig.CB_Repeat_H_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_H_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_H_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_H_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_H_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_H_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_H_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_H_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_H_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_H_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_H_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_H_L) sb.Append('L');
                    break;

                case "반복_I":
                    if (GenieConfig.CB_Repeat_I_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_I_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_I_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_I_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_I_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_I_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_I_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_I_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_I_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_I_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_I_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_I_L) sb.Append('L');
                    break;

                case "반복_J":
                    if (GenieConfig.CB_Repeat_J_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_J_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_J_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_J_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_J_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_J_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_J_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_J_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_J_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_J_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_J_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_J_L) sb.Append('L');
                    break;

                case "반복_K":
                    if (GenieConfig.CB_Repeat_K_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_K_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_K_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_K_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_K_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_K_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_K_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_K_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_K_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_K_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_K_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_K_L) sb.Append('L');
                    break;

                case "반복_L":
                    if (GenieConfig.CB_Repeat_L_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_L_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_L_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_L_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_L_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_L_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_L_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_L_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_L_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_L_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_L_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_L_L) sb.Append('L');
                    break;

                case "반복_M":
                    if (GenieConfig.CB_Repeat_M_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_M_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_M_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_M_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_M_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_M_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_M_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_M_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_M_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_M_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_M_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_M_L) sb.Append('L');
                    break;

                case "반복_N":
                    if (GenieConfig.CB_Repeat_N_A) sb.Append('A');
                    if (GenieConfig.CB_Repeat_N_B) sb.Append('B');
                    if (GenieConfig.CB_Repeat_N_C) sb.Append('C');
                    if (GenieConfig.CB_Repeat_N_D) sb.Append('D');
                    if (GenieConfig.CB_Repeat_N_E) sb.Append('E');
                    if (GenieConfig.CB_Repeat_N_F) sb.Append('F');
                    if (GenieConfig.CB_Repeat_N_G) sb.Append('G');
                    if (GenieConfig.CB_Repeat_N_H) sb.Append('H');
                    if (GenieConfig.CB_Repeat_N_I) sb.Append('I');
                    if (GenieConfig.CB_Repeat_N_J) sb.Append('J');
                    if (GenieConfig.CB_Repeat_N_K) sb.Append('K');
                    if (GenieConfig.CB_Repeat_N_L) sb.Append('L');
                    break;

                case "Day_A":
                    if (GenieConfig.CB_day_A_A) sb.Append('A');
                    if (GenieConfig.CB_day_A_B) sb.Append('B');
                    if (GenieConfig.CB_day_A_C) sb.Append('C');
                    if (GenieConfig.CB_day_A_D) sb.Append('D');
                    if (GenieConfig.CB_day_A_E) sb.Append('E');
                    if (GenieConfig.CB_day_A_F) sb.Append('F');
                    if (GenieConfig.CB_day_A_G) sb.Append('G');
                    if (GenieConfig.CB_day_A_H) sb.Append('H');
                    if (GenieConfig.CB_day_A_I) sb.Append('I');
                    if (GenieConfig.CB_day_A_J) sb.Append('J');
                    if (GenieConfig.CB_day_A_K) sb.Append('K');
                    if (GenieConfig.CB_day_A_L) sb.Append('L');
                    break;

                case "Day_B":
                    if (GenieConfig.CB_day_B_A) sb.Append('A');
                    if (GenieConfig.CB_day_B_B) sb.Append('B');
                    if (GenieConfig.CB_day_B_C) sb.Append('C');
                    if (GenieConfig.CB_day_B_D) sb.Append('D');
                    if (GenieConfig.CB_day_B_E) sb.Append('E');
                    if (GenieConfig.CB_day_B_F) sb.Append('F');
                    if (GenieConfig.CB_day_B_G) sb.Append('G');
                    if (GenieConfig.CB_day_B_H) sb.Append('H');
                    if (GenieConfig.CB_day_B_I) sb.Append('I');
                    if (GenieConfig.CB_day_B_J) sb.Append('J');
                    if (GenieConfig.CB_day_B_K) sb.Append('K');
                    if (GenieConfig.CB_day_B_L) sb.Append('L');
                    break;

                case "Day_C":
                    if (GenieConfig.CB_day_C_A) sb.Append('A');
                    if (GenieConfig.CB_day_C_B) sb.Append('B');
                    if (GenieConfig.CB_day_C_C) sb.Append('C');
                    if (GenieConfig.CB_day_C_D) sb.Append('D');
                    if (GenieConfig.CB_day_C_E) sb.Append('E');
                    if (GenieConfig.CB_day_C_F) sb.Append('F');
                    if (GenieConfig.CB_day_C_G) sb.Append('G');
                    if (GenieConfig.CB_day_C_H) sb.Append('H');
                    if (GenieConfig.CB_day_C_I) sb.Append('I');
                    if (GenieConfig.CB_day_C_J) sb.Append('J');
                    if (GenieConfig.CB_day_C_K) sb.Append('K');
                    if (GenieConfig.CB_day_C_L) sb.Append('L');
                    break;

                case "Day_D":
                    if (GenieConfig.CB_day_D_A) sb.Append('A');
                    if (GenieConfig.CB_day_D_B) sb.Append('B');
                    if (GenieConfig.CB_day_D_C) sb.Append('C');
                    if (GenieConfig.CB_day_D_D) sb.Append('D');
                    if (GenieConfig.CB_day_D_E) sb.Append('E');
                    if (GenieConfig.CB_day_D_F) sb.Append('F');
                    if (GenieConfig.CB_day_D_G) sb.Append('G');
                    if (GenieConfig.CB_day_D_H) sb.Append('H');
                    if (GenieConfig.CB_day_D_I) sb.Append('I');
                    if (GenieConfig.CB_day_D_J) sb.Append('J');
                    if (GenieConfig.CB_day_D_K) sb.Append('K');
                    if (GenieConfig.CB_day_D_L) sb.Append('L');
                    break;

                case "Day_E":
                    if (GenieConfig.CB_day_E_A) sb.Append('A');
                    if (GenieConfig.CB_day_E_B) sb.Append('B');
                    if (GenieConfig.CB_day_E_C) sb.Append('C');
                    if (GenieConfig.CB_day_E_D) sb.Append('D');
                    if (GenieConfig.CB_day_E_E) sb.Append('E');
                    if (GenieConfig.CB_day_E_F) sb.Append('F');
                    if (GenieConfig.CB_day_E_G) sb.Append('G');
                    if (GenieConfig.CB_day_E_H) sb.Append('H');
                    if (GenieConfig.CB_day_E_I) sb.Append('I');
                    if (GenieConfig.CB_day_E_J) sb.Append('J');
                    if (GenieConfig.CB_day_E_K) sb.Append('K');
                    if (GenieConfig.CB_day_E_L) sb.Append('L');
                    break;

                case "Day_F":
                    if (GenieConfig.CB_day_F_A) sb.Append('A');
                    if (GenieConfig.CB_day_F_B) sb.Append('B');
                    if (GenieConfig.CB_day_F_C) sb.Append('C');
                    if (GenieConfig.CB_day_F_D) sb.Append('D');
                    if (GenieConfig.CB_day_F_E) sb.Append('E');
                    if (GenieConfig.CB_day_F_F) sb.Append('F');
                    if (GenieConfig.CB_day_F_G) sb.Append('G');
                    if (GenieConfig.CB_day_F_H) sb.Append('H');
                    if (GenieConfig.CB_day_F_I) sb.Append('I');
                    if (GenieConfig.CB_day_F_J) sb.Append('J');
                    if (GenieConfig.CB_day_F_K) sb.Append('K');
                    if (GenieConfig.CB_day_F_L) sb.Append('L');
                    break;

                case "Cut_A":
                    if (GenieConfig.CB_Cut_A_A) sb.Append('A');
                    if (GenieConfig.CB_Cut_A_B) sb.Append('B');
                    if (GenieConfig.CB_Cut_A_C) sb.Append('C');
                    if (GenieConfig.CB_Cut_A_D) sb.Append('D');
                    if (GenieConfig.CB_Cut_A_E) sb.Append('E');
                    if (GenieConfig.CB_Cut_A_F) sb.Append('F');
                    if (GenieConfig.CB_Cut_A_G) sb.Append('G');
                    if (GenieConfig.CB_Cut_A_H) sb.Append('H');
                    if (GenieConfig.CB_Cut_A_I) sb.Append('I');
                    if (GenieConfig.CB_Cut_A_J) sb.Append('J');
                    if (GenieConfig.CB_Cut_A_K) sb.Append('K');
                    if (GenieConfig.CB_Cut_A_L) sb.Append('L');
                    break;

                case "Cut_B":
                    if (GenieConfig.CB_Cut_B_A) sb.Append('A');
                    if (GenieConfig.CB_Cut_B_B) sb.Append('B');
                    if (GenieConfig.CB_Cut_B_C) sb.Append('C');
                    if (GenieConfig.CB_Cut_B_D) sb.Append('D');
                    if (GenieConfig.CB_Cut_B_E) sb.Append('E');
                    if (GenieConfig.CB_Cut_B_F) sb.Append('F');
                    if (GenieConfig.CB_Cut_B_G) sb.Append('G');
                    if (GenieConfig.CB_Cut_B_H) sb.Append('H');
                    if (GenieConfig.CB_Cut_B_I) sb.Append('I');
                    if (GenieConfig.CB_Cut_B_J) sb.Append('J');
                    if (GenieConfig.CB_Cut_B_K) sb.Append('K');
                    if (GenieConfig.CB_Cut_B_L) sb.Append('L');
                    break;

                case "Cut_C":
                    if (GenieConfig.CB_Cut_C_A) sb.Append('A');
                    if (GenieConfig.CB_Cut_C_B) sb.Append('B');
                    if (GenieConfig.CB_Cut_C_C) sb.Append('C');
                    if (GenieConfig.CB_Cut_C_D) sb.Append('D');
                    if (GenieConfig.CB_Cut_C_E) sb.Append('E');
                    if (GenieConfig.CB_Cut_C_F) sb.Append('F');
                    if (GenieConfig.CB_Cut_C_G) sb.Append('G');
                    if (GenieConfig.CB_Cut_C_H) sb.Append('H');
                    if (GenieConfig.CB_Cut_C_I) sb.Append('I');
                    if (GenieConfig.CB_Cut_C_J) sb.Append('J');
                    if (GenieConfig.CB_Cut_C_K) sb.Append('K');
                    if (GenieConfig.CB_Cut_C_L) sb.Append('L');
                    break;

                case "리밸_A":
                    if (GenieConfig.CB_rebalance_A_A) sb.Append('A');
                    if (GenieConfig.CB_rebalance_A_B) sb.Append('B');
                    if (GenieConfig.CB_rebalance_A_C) sb.Append('C');
                    if (GenieConfig.CB_rebalance_A_D) sb.Append('D');
                    if (GenieConfig.CB_rebalance_A_E) sb.Append('E');
                    if (GenieConfig.CB_rebalance_A_F) sb.Append('F');
                    if (GenieConfig.CB_rebalance_A_G) sb.Append('G');
                    if (GenieConfig.CB_rebalance_A_H) sb.Append('H');
                    if (GenieConfig.CB_rebalance_A_I) sb.Append('I');
                    if (GenieConfig.CB_rebalance_A_J) sb.Append('J');
                    if (GenieConfig.CB_rebalance_A_K) sb.Append('K');
                    if (GenieConfig.CB_rebalance_A_L) sb.Append('L');
                    break;

                case "리밸_B":
                    if (GenieConfig.CB_rebalance_B_A) sb.Append('A');
                    if (GenieConfig.CB_rebalance_B_B) sb.Append('B');
                    if (GenieConfig.CB_rebalance_B_C) sb.Append('C');
                    if (GenieConfig.CB_rebalance_B_D) sb.Append('D');
                    if (GenieConfig.CB_rebalance_B_E) sb.Append('E');
                    if (GenieConfig.CB_rebalance_B_F) sb.Append('F');
                    if (GenieConfig.CB_rebalance_B_G) sb.Append('G');
                    if (GenieConfig.CB_rebalance_B_H) sb.Append('H');
                    if (GenieConfig.CB_rebalance_B_I) sb.Append('I');
                    if (GenieConfig.CB_rebalance_B_J) sb.Append('J');
                    if (GenieConfig.CB_rebalance_B_K) sb.Append('K');
                    if (GenieConfig.CB_rebalance_B_L) sb.Append('L');
                    break;

                case "리밸_C":
                    if (GenieConfig.CB_rebalance_C_A) sb.Append('A');
                    if (GenieConfig.CB_rebalance_C_B) sb.Append('B');
                    if (GenieConfig.CB_rebalance_C_C) sb.Append('C');
                    if (GenieConfig.CB_rebalance_C_D) sb.Append('D');
                    if (GenieConfig.CB_rebalance_C_E) sb.Append('E');
                    if (GenieConfig.CB_rebalance_C_F) sb.Append('F');
                    if (GenieConfig.CB_rebalance_C_G) sb.Append('G');
                    if (GenieConfig.CB_rebalance_C_H) sb.Append('H');
                    if (GenieConfig.CB_rebalance_C_I) sb.Append('I');
                    if (GenieConfig.CB_rebalance_C_J) sb.Append('J');
                    if (GenieConfig.CB_rebalance_C_K) sb.Append('K');
                    if (GenieConfig.CB_rebalance_C_L) sb.Append('L');
                    break;

                case "리밸_D":
                    if (GenieConfig.CB_rebalance_D_A) sb.Append('A');
                    if (GenieConfig.CB_rebalance_D_B) sb.Append('B');
                    if (GenieConfig.CB_rebalance_D_C) sb.Append('C');
                    if (GenieConfig.CB_rebalance_D_D) sb.Append('D');
                    if (GenieConfig.CB_rebalance_D_E) sb.Append('E');
                    if (GenieConfig.CB_rebalance_D_F) sb.Append('F');
                    if (GenieConfig.CB_rebalance_D_G) sb.Append('G');
                    if (GenieConfig.CB_rebalance_D_H) sb.Append('H');
                    if (GenieConfig.CB_rebalance_D_I) sb.Append('I');
                    if (GenieConfig.CB_rebalance_D_J) sb.Append('J');
                    if (GenieConfig.CB_rebalance_D_K) sb.Append('K');
                    if (GenieConfig.CB_rebalance_D_L) sb.Append('L');
                    break;

                case "리밸_E":
                    if (GenieConfig.CB_rebalance_E_A) sb.Append('A');
                    if (GenieConfig.CB_rebalance_E_B) sb.Append('B');
                    if (GenieConfig.CB_rebalance_E_C) sb.Append('C');
                    if (GenieConfig.CB_rebalance_E_D) sb.Append('D');
                    if (GenieConfig.CB_rebalance_E_E) sb.Append('E');
                    if (GenieConfig.CB_rebalance_E_F) sb.Append('F');
                    if (GenieConfig.CB_rebalance_E_G) sb.Append('G');
                    if (GenieConfig.CB_rebalance_E_H) sb.Append('H');
                    if (GenieConfig.CB_rebalance_E_I) sb.Append('I');
                    if (GenieConfig.CB_rebalance_E_J) sb.Append('J');
                    if (GenieConfig.CB_rebalance_E_K) sb.Append('K');
                    if (GenieConfig.CB_rebalance_E_L) sb.Append('L');
                    break;

                case "리밸_F":
                    if (GenieConfig.CB_rebalance_F_A) sb.Append('A');
                    if (GenieConfig.CB_rebalance_F_B) sb.Append('B');
                    if (GenieConfig.CB_rebalance_F_C) sb.Append('C');
                    if (GenieConfig.CB_rebalance_F_D) sb.Append('D');
                    if (GenieConfig.CB_rebalance_F_E) sb.Append('E');
                    if (GenieConfig.CB_rebalance_F_F) sb.Append('F');
                    if (GenieConfig.CB_rebalance_F_G) sb.Append('G');
                    if (GenieConfig.CB_rebalance_F_H) sb.Append('H');
                    if (GenieConfig.CB_rebalance_F_I) sb.Append('I');
                    if (GenieConfig.CB_rebalance_F_J) sb.Append('J');
                    if (GenieConfig.CB_rebalance_F_K) sb.Append('K');
                    if (GenieConfig.CB_rebalance_F_L) sb.Append('L');
                    break;

                case "리밸_G":
                    if (GenieConfig.CB_rebalance_G_A) sb.Append('A');
                    if (GenieConfig.CB_rebalance_G_B) sb.Append('B');
                    if (GenieConfig.CB_rebalance_G_C) sb.Append('C');
                    if (GenieConfig.CB_rebalance_G_D) sb.Append('D');
                    if (GenieConfig.CB_rebalance_G_E) sb.Append('E');
                    if (GenieConfig.CB_rebalance_G_F) sb.Append('F');
                    if (GenieConfig.CB_rebalance_G_G) sb.Append('G');
                    if (GenieConfig.CB_rebalance_G_H) sb.Append('H');
                    if (GenieConfig.CB_rebalance_G_I) sb.Append('I');
                    if (GenieConfig.CB_rebalance_G_J) sb.Append('J');
                    if (GenieConfig.CB_rebalance_G_K) sb.Append('K');
                    if (GenieConfig.CB_rebalance_G_L) sb.Append('L');
                    break;
            }

            return sb.ToString();
        }

 
        public static string 그룹변환(int num)
        {
            // [최적화] 스위치 표현식 (Switch Expression)
            // 1. 메모리 할당 제거: 변수 선언 없이 결과값을 즉시 반환(Return)합니다.
            // 2. 컴파일러 최적화: 내부적으로 점프 테이블(Jump Table)로 변환되어,
            //    입력값(num)에 해당하는 위치로 즉시 이동하므로 CPU 연산이 매우 빠릅니다.

            return num switch
            {
                1 => "A",
                2 => "B",
                3 => "C",
                4 => "D",
                5 => "E",
                6 => "F",
                7 => "G",
                8 => "H",
                9 => "I",
                10 => "J",
                11 => "K",
                12 => "L",
                13 => "X",
                _ => "0" // 그 외(0 포함)의 값은 "0" 반환
            };
        }

      

        public static string 이름_자동설정()
        {
            // 1. 현재 프로그램이 실행된 폴더의 이름을 가져옵니다.
            // 예: C:\지니64_A -> "지니64_A"
            string 현재폴더명 = new DirectoryInfo(Application.StartupPath).Name;

            // 2. 요청하신 폴더명 리스트와 비교합니다.
            if (현재폴더명 == "지니64_A" ||
                현재폴더명 == "지니64_B" ||
                현재폴더명 == "지니64_C" ||
                현재폴더명 == "지니64_D")
            {
                return 현재폴더명;
            }

            // 3. 조건에 맞지 않으면 기본값 반환
            return "지니64_DEBUG";
        }

        public static string 오류코드(string orderResult)
        {
            string result = "";

            switch (orderResult)
            {
                case "1": result = "정상처리"; break;
                case "0": result = "정상처리"; break;
                case "-10": result = "실패"; break;
                case "-11": result = "조건번호 없슴"; break;
                case "-12": result = "조건번호와 조건식 불일치"; break;
                case "-13": result = "조건검색 조회요청 초과"; break;
                case "-100": result = "사용자정보교환 실패"; break;
                case "-101": result = "서버 접속 실패"; break;
                case "-102": result = "버전처리 실패"; break;
                case "-103": result = "개인방화벽 실패"; break;
                case "-104": result = "메모리 보호실패"; break;
                case "-105": result = "함수입력값 오류"; break;
                case "-106": result = "통신연결 종료"; break;
                case "-107": result = "보안모듈 오류"; break;
                case "-108": result = "공인인증 로그인 필요"; break;
                case "-200": result = "시세조회 과부하"; break;
                case "-201": result = "전문작성 초기화 실패."; break;
                case "-202": result = "전문작성 입력값 오류."; break;
                case "-203": result = "데이터 없음."; break;
                case "-204": result = "조회가능한 종목수 초과. 한번에 조회 가능한 종목개수는 최대 100종목."; break;
                case "-205": result = "데이터 수신 실패"; break;
                case "-206": result = "조회가능한 FID수 초과. 한번에 조회 가능한 FID개수는 최대 100개."; break;
                case "-207": result = "실시간 해제오류"; break;
                case "-209": result = "시세조회제한"; break;
                case "-300": result = "입력값 오류"; break;
                case "-301": result = "계좌비밀번호 없음."; break;
                case "-302": result = "타인계좌 사용오류."; break;
                case "-303": result = "주문가격이 주문착오 금액기준 초과."; break;
                case "-304": result = "주문가격이 주문착오 금액기준 초과."; break;
                case "-305": result = "주문수량이 총발행주수의 1% 초과오류."; break;
                case "-306": result = "주문수량은 총발행주수의 3% 초과오류."; break;
                case "-307": result = "주문전송 실패"; break;
                case "-308": result = "주문전송 과부하"; break;
                case "-309": result = "주문수량 300계약 초과."; break;
                case "-310": result = "주문수량 500계약 초과."; break;
                case "-311": result = "주문전송제한 과부하"; break;
                case "-340": result = "계좌정보 없음."; break;
                case "-500": result = "종목코드 없음."; break;
            }
            return result;
        }



    }












}
