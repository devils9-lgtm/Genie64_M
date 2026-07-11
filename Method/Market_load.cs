using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static 지니64.box.Form_Jisu;

namespace 지니64
{
    public class Market_load : Form1
    {
        public static void Loading(string code, string name, string lastPrice, string state, string marketName, string orderWarning, string nxtEnable)
        {
            var collection = Form1.form1.collection;


            string market = "D";
            if (marketName == "거래소") market = "P";
            if (marketName == "ETF") market = "E";

            int 종가 = int.Parse(lastPrice);

            // [변수 초기화] 일단 안전하게 '신용불가 / 증거금 100%'로 시작
            bool 신용가능 = false;
            double 증거금률 = 1.0;

            // =========================================================================
            // 🛠️ [파싱 로직] state 문자열 분석 (관리/정지/증거금률)
            // =========================================================================
            // 1. 위험 종목 확인 (관리, 정지, 정리) -> 포함되면 바로 신용 불가
            if (state.IndexOf("관리") >= 0 || state.IndexOf("정지") >= 0 || state.IndexOf("정리") >= 0)
            {
                신용가능 = false;
                증거금률 = 1.0;
            }
            else
            {
                // 2. "증거금" 키워드 위치 찾기
                int findIndex = state.IndexOf("증거금");

                if (findIndex >= 0)
                {
                    // "증거금" 글자수(3) 만큼 뒤로 가서 숫자 읽기
                    int numIdx = findIndex + 3;

                    // 문자열 길이 안전 체크 (숫자 2자리는 있어야 함)
                    if (state.Length >= numIdx + 2)
                    {
                        // ★ ASCII 코드로 숫자 바로 계산 (Fast!)
                        int calcRate = (state[numIdx] - '0') * 10 + (state[numIdx + 1] - '0');

                        // 예외처리: "10"으로 읽히면 "100%"로 간주
                        if (calcRate == 10) calcRate = 100;

                        // 증거금률 설정 (0.4, 0.3, 1.0 등)
                        증거금률 = (double)calcRate / 100.0;

                        // 3. 신용 가능 여부 최종 판단 (100% 미만일 때만 True)
                        if (calcRate < 100)
                        {
                            신용가능 = true;
                        }
                        else
                        {
                            신용가능 = false; // 증거금 100%는 신용 불가
                        }
                    }
                }
            }

            if (Stock_Add())
            {
                Market_Item 신규_아이템 = new Market_Item
                {
                    종목명 = name,
                    state = state,
                    신용가능 = 신용가능,
                    증거금률 = 증거금률,
                    종목코드 = code,
                    Market = market,
                    Last_price = 종가,

                    // 시간 관련 변수들은 필요한 값으로 초기화
                    도시간 = Get.TimeNow,
                    수시간_A = Get.TimeNow,
                    수시간_B = Get.TimeNow
                };

                if (Form1.Market_Item_List.TryAdd(code, 신규_아이템))
                {
                    collection.Add(name);
                }
            }

            Form1.form1.TB_관심그룹_종목명.AutoCompleteCustomSource = collection;

            bool Stock_Add()
            {
                bool 등록 = true;

                if (name.Contains("KIWOOM ")) 등록 = true;
                if (name.Contains("KODEX ")) 등록 = true;
                if (name.Contains("TIGER ")) 등록 = true;
                if (name.Contains("RISE ")) 등록 = true;
                if (name.Contains("ACE ")) 등록 = true;
                if (name.Contains("SOL ")) 등록 = true;

                if (name.Contains("KoAct ")) 등록 = false;
                if (name.Contains("1Q ")) 등록 = false;
                if (name.Contains("PLUS ")) 등록 = false;
                if (name.Contains("KoAct ")) 등록 = false;
                if (name.Contains("WON ")) 등록 = false;
                if (name.Contains("BNK ")) 등록 = false;
                if (name.Contains("ITF ")) 등록 = false;
                if (name.Contains("HK ")) 등록 = false;
                if (name.Contains("KCGI ")) 등록 = false;
                if (name.Contains("TRUSTON ")) 등록 = false;
                if (name.Contains("파워 ")) 등록 = false;
                if (name.Contains("DAISHIN343 ")) 등록 = false;
                if (name.Contains(" ETN")) 등록 = false;

                if (name.Contains("TREX ")) 등록 = false;
                if (name.Contains("ARIRANG ")) 등록 = false;
                if (name.Contains("마이다스 ")) 등록 = false;
                if (name.Contains("KTOP ")) 등록 = false;
                if (name.Contains("FOCUS ")) 등록 = false;
                if (name.Contains("마이티 ")) 등록 = false;
                if (name.Contains("HANARO ")) 등록 = false;
                if (name.Contains("TIMEFOLIO ")) 등록 = false;
                if (name.Contains("MASTER ")) 등록 = false;
                if (name.Contains("히어로즈 ")) 등록 = false;
                if (name.Contains("VITA ")) 등록 = false;
                if (name.Contains("WOORI ")) 등록 = false;
                if (name.Contains("UNICORN ")) 등록 = false;
                if (name.Contains("에셋플러스 ")) 등록 = false;
                if (name.Contains("대신343 ")) 등록 = false;
                if (name.Contains("스팩")) 등록 = false;
                if (name.Contains("한국ANKOR유전")) 등록 = false;

                return 등록;
            }

            if (nxtEnable.Equals("Y"))
            {
                NXT_list.Add(code.Trim());
            }
        }

        public static void 지수이평추세_초기화()
        {
            // 리스트 초기화 (중복 추가 방지)
            지수이평추세.Clear();

            // [1] 코스피 객체 생성 및 설정
            var kospi = new 지수이평추세
            {
                Day_추세_03 = true,
                Day_추세_05 = true,
                Day_추세_10 = true,
                Day_추세_20 = true,
                Day_추세_40 = true,
                Day_추세_60 = true,
                Min_추세_03 = true,
                Min_추세_05 = true,
                Min_추세_10 = true,
                Min_추세_20 = true,
                Min_추세_30 = true,
                Min_추세_60 = true,
            };
            지수이평추세.Add(kospi); // 0번 인덱스에 추가

            // [2] 코스닥 객체 생성 및 설정
            var kosdaq = new 지수이평추세
            {
                Day_추세_03 = true,
                Day_추세_05 = true,
                Day_추세_10 = true,
                Day_추세_20 = true,
                Day_추세_40 = true,
                Day_추세_60 = true,
                Min_추세_03 = true,
                Min_추세_05 = true,
                Min_추세_10 = true,
                Min_추세_20 = true,
                Min_추세_30 = true,
                Min_추세_60 = true,
            };
            지수이평추세.Add(kosdaq); // 1번 인덱스에 추가
        }
    }
}
