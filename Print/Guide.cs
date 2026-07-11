using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using 지니64.box;

namespace 지니64
{
    public class Guide
    {
        static string[] 신규_A;
        static string[] 신규_B;
        static string[] 신규_C;

        static string[] 반복_A;
        static string[] 반복_B;
        static string[] 반복_C;
        static string[] 반복_D;
        static string[] 반복_E;
        static string[] 반복_F;
        static string[] 반복_G;
        static string[] 반복_H;
        static string[] 반복_I;
        static string[] 반복_J;
        static string[] 반복_K;
        static string[] 반복_L;
        static string[] 반복_M;
        static string[] 반복_N;

        static string[] 리밸_A;
        static string[] 리밸_B;
        static string[] 리밸_C;
        static string[] 리밸_D;
        static string[] 리밸_E;
        static string[] 리밸_F;
        static string[] 리밸_G;

        static string[] 청산_A;
        static string[] 청산_B;
        static string[] 청산_C;

        public static void GuideLoding()
        {
            if (GenieConfig.CB_가이드매매)
            {
                Form1.Console_print("####################### 가이드매매 로딩 #######################");

                패널감추기_신규매수();
                패널감추기_특수매매();

                ControllerDisable.Form_1_Disable();
            }
        }

        public static void 가이드매매설정로딩()
        {
            Form1.음소거 = true;

            Form1.Console_print("####################### 가이드매매 설정 로딩 #######################");
            Condition_DataLoad();

            계좌설정();
            기본매매설정();
            대금탐색설정();
            반복매매설정();
            계좌관리설정();
            매매그룹설정();
            특수설정();
            기능설정();

            가이드검색식로딩();

            Form1.음소거 = GenieConfig.CB_음소거;
            Load_condition_textprint();
        }


        public static void Condition_DataLoad() // 계좌 번호에 따른 조건식 불러오기
        {
            if (Form1.로딩완료)
            {
                foreach (var kvp in Form1.위치별검색식리스트)
                {
                    var posCon = kvp.Value;

                    // 1. 실행여부가 true일 때만 작동
                    if (posCon.실행여부)
                    {
                        // 2. 리스트에서 검색식을 딱 한 번만 찾아서 변수에 저장합니다.
                        Condition foundCondition = Form1.ConditionList.Find(o => o.name == posCon.이름);

                        // 3. 찾은 결과가 null이 아닐 때만 중지 함수를 부릅니다.
                        if (foundCondition != null)
                        {
                            Condition_Management.Stop_Monitoring(foundCondition, kvp.Key);
                        }
                    }
                }
            }
            else
            {
                string filePath = Path.Combine(Form1.startupPath, "Data", $"{GenieConfig.textBox_ID}__{GenieConfig.textBox_계좌번호}__", "검색식", "J사용검색식.json");

                if (File.Exists(filePath))
                {
                    try
                    {
                        string jsonString = File.ReadAllText(filePath);

                        // [방어] 파일 내용이 없으면 중단
                        if (string.IsNullOrWhiteSpace(jsonString)) return;

                        // [옵션] 유연한 변환 설정
                        var options = new System.Text.Json.JsonSerializerOptions
                        {
                            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
                            PropertyNameCaseInsensitive = true
                        };

                        try
                        {
                            // 1. 일단 일반 Dictionary로 읽어옵니다. (JSON -> Dictionary)
                            var temp_data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, 위치별검색식>>(jsonString, options);

                            if (temp_data != null)
                            {
                                // 2. [핵심 최적화] 로드된 데이터를 메인 리스트에 넣기 전에 미리 싹 세탁(초기화)합니다.
                                foreach (var item in temp_data.Values)
                                {
                                    // JSON에 true로 저장되어 있더라도, 로드할 때는 무조건 강제로 끕니다! (안전제일)
                                    item.실행여부 = false;
                                    item.이름 ??= "";
                                }

                                // 3. 깨끗하게 세탁된 temp_data를 ConcurrentDictionary로 변환해서 최종 할당!
                                Form1.위치별검색식리스트 = new System.Collections.Concurrent.ConcurrentDictionary<string, 위치별검색식>(temp_data);
                                Form1.Console_print("주식 자동매매프로그램 지니: 검색식 데이터 로드 성공 및 안전 초기화 완료!");
                            }
                        }
                        catch (Exception ex)
                        {
                            Form1.Console_print($"[에러] 검색식 로드 및 변환 실패: {ex.Message}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // 여기서 에러 메시지가 출력되면 파일 내용이 깨졌거나 형식이 바뀐 것입니다.
                        Form1.Console_print($"검색식 파일 읽기 실패: {ex.Message}");
                    }
                }
            }
        }

        private static void 가이드검색식로딩()
        {
            // 1. 저장 경로 설정
            string folderPath = Path.Combine(Form1.startupPath, "Data", $"{GenieConfig.textBox_ID}__{GenieConfig.textBox_계좌번호}__", "검색식");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath); // 폴더가 없으면 생성

            string filePath = Path.Combine(folderPath, "J사용검색식.json");

            try
            {
                //// 저장 전 데이터 검증 출력
                //Form1.Console_print($"\n--- [주식 자동매매프로그램 지니] 검색식 저장 전 데이터 확인 (총 {Form1.위치별검색식리스트.Count}건) ---");

                //foreach (var kvp in Form1.위치별검색식리스트)
                //{
                //    // kvp.Key는 검색식 이름이나 ID, kvp.Value는 위치별검색식 객체입니다.
                //    var con = kvp.Value;

                //    // 백억아빠님이 위치별검색식 클래스에서 보고 싶으신 속성들 위주로 출력해보세요.
                //    // 예시: 검색식명, 진입조건, 사용여부 등
                //    Form1.Console_print($"[검색식 키]: {kvp.Key} | [이름]: {con.name} | [설정값]: {con.where} | [상태]: {con.overlap}");
                //}

                //Form1.Console_print("----------------------------------------------------------------------\n");


                // 2. JSON 저장 옵션 (한글 깨짐 방지 + 가독성)
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true, // 사람이 보기 편하게 정렬
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // 한글 안 깨지게
                };

                // 3. 복잡한 반복문 없이 딕셔너리 전체를 한 번에 직렬화하여 저장
                // $ 기호나 루프가 필요 없습니다.
                string jsonString = System.Text.Json.JsonSerializer.Serialize(Form1.위치별검색식리스트, options);
                File.WriteAllText(filePath, jsonString);

                Form1.Console_print("주식 자동매매프로그램 지니: 가이드 검색식 JSON 저장 완료!");

                // 4. 저장 후 데이터 로드 및 플래그 설정
                Condition_Management.Condition_DataLoad();
                Form1.form1.가이드검색식확인 = true;
            }
            catch (Exception ex)
            {
                Form1.Console_print($"가이드 검색식 저장 에러: {ex.Message}");
            }
        }

        public static void 계좌설정()
        {
            계좌설정_(
                 최대잔고: 20,              // 내 맘대로 잔고부터 적음!
                 매수제한: true,
                 매수제한매입비: 65,
                 추매제한: false,
                 추매제한매입비: 50,
                 시작시간: 80000,           // 시간은 뒤로 뺌
                 종료시간: 200000,
                 매수사용법: 0,
                 지수연동_신규: 11,
                 지수연동_추매: 1
              );

            계좌설정_미수사용(
                사용: true,
                정리시간: 151500,
                사용법: 5,
                회당주문금액: 10,
                주문값: 0,
                주문방법: 1,
                반복시간: 30
            );

            계좌설정_코스피(
                등락률: -1.5,
                등락률상하: 0,
                등락률사용법: 3,
                고가대비등락률: -1.5,
                고가대비상하: 0,
                고가대비사용법: 3,
                저가대비등락률: 0.1,
                저가대비상하: 0,
                저가대비사용법: 3
            );

            계좌설정_코스닥(
                등락률: -1.5,
                등락률상하: 0,
                등락률사용법: 3,
                고가대비등락률: -1.5,
                고가대비상하: 0,
                고가대비사용법: 3,
                저가대비등락률: 0.1,
                저가대비상하: 0,
                저가대비사용법: 3
            );

            계좌설정_이평선(
                 // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
                 대상그룹명: "CB_지수이평_신규",

                 // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
                 코스피_사용여부: true,
                 use_kospi_min_03: true, use_kospi_min_05: true, use_kospi_min_10: false,
                 use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
                 use_kospi_day_03: true, use_kospi_day_05: true, use_kospi_day_10: false,
                 use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
                 UD_kospi_min_03: true, UD_kospi_min_05: true, UD_kospi_min_10: false,
                 UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
                 UD_kospi_day_03: true, UD_kospi_day_05: true, UD_kospi_day_10: false,
                 UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

                 // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
                 코스닥_사용여부: true,
                 use_kosdaq_min_03: true, use_kosdaq_min_05: true, use_kosdaq_min_10: false,
                 use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
                 use_kosdaq_day_03: true, use_kosdaq_day_05: true, use_kosdaq_day_10: false,
                 use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
                 UD_kosdaq_min_03: true, UD_kosdaq_min_05: true, UD_kosdaq_min_10: false,
                 UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
                 UD_kosdaq_day_03: true, UD_kosdaq_day_05: true, UD_kosdaq_day_10: false,
                 UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
             );

            계좌설정_이평선(
               // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
               대상그룹명: "CB_지수이평_그외",

               // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
               코스피_사용여부: true,
               use_kospi_min_03: true, use_kospi_min_05: true, use_kospi_min_10: false,
               use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
               use_kospi_day_03: true, use_kospi_day_05: true, use_kospi_day_10: false,
               use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
               UD_kospi_min_03: true, UD_kospi_min_05: true, UD_kospi_min_10: false,
               UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
               UD_kospi_day_03: true, UD_kospi_day_05: true, UD_kospi_day_10: false,
               UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

               // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
               코스닥_사용여부: true,
               use_kosdaq_min_03: true, use_kosdaq_min_05: true, use_kosdaq_min_10: false,
               use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
               use_kosdaq_day_03: true, use_kosdaq_day_05: true, use_kosdaq_day_10: false,
               use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
               UD_kosdaq_min_03: true, UD_kosdaq_min_05: true, UD_kosdaq_min_10: false,
               UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
               UD_kosdaq_day_03: true, UD_kosdaq_day_05: true, UD_kosdaq_day_10: false,
               UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
           );

            계좌설정_이평선(
             // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
             대상그룹명: "CB_지수이평_반복_A",

             // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
             코스피_사용여부: true,
             use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
             use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
             use_kospi_day_03: true, use_kospi_day_05: true, use_kospi_day_10: true,
             use_kospi_day_20: true, use_kospi_day_40: false, use_kospi_day_60: false,

             UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
             UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
             UD_kospi_day_03: true, UD_kospi_day_05: true, UD_kospi_day_10: true,
             UD_kospi_day_20: true, UD_kospi_day_40: false, UD_kospi_day_60: false,

             // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
             코스닥_사용여부: true,
             use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
             use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
             use_kosdaq_day_03: true, use_kosdaq_day_05: true, use_kosdaq_day_10: true,
             use_kosdaq_day_20: true, use_kosdaq_day_40: false, use_kosdaq_day_60: false,

             UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
             UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
             UD_kosdaq_day_03: true, UD_kosdaq_day_05: true, UD_kosdaq_day_10: true,
             UD_kosdaq_day_20: true, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
         );

            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_반복_B",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: true,
           use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: true, use_kospi_day_05: true, use_kospi_day_10: true,
           use_kospi_day_20: true, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: true, UD_kospi_day_05: true, UD_kospi_day_10: true,
           UD_kospi_day_20: true, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: true,
           use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: true, use_kosdaq_day_05: true, use_kosdaq_day_10: true,
           use_kosdaq_day_20: true, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: true, UD_kosdaq_day_05: true, UD_kosdaq_day_10: true,
           UD_kosdaq_day_20: true, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
       );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_반복_C",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: false,
           use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: false, use_kospi_day_05: false, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: false, UD_kospi_day_05: false, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: false,
           use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: false, use_kosdaq_day_05: false, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: false, UD_kosdaq_day_05: false, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
       );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_반복_D",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: false,
           use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: false, use_kospi_day_05: false, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: false, UD_kospi_day_05: false, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: false,
           use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: false, use_kosdaq_day_05: false, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: false, UD_kosdaq_day_05: false, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
       );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_반복_E",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: false,
           use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: false, use_kospi_day_05: false, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: false, UD_kospi_day_05: false, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: false,
           use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: false, use_kosdaq_day_05: false, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: false, UD_kosdaq_day_05: false, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
       );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_반복_F",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: false,
           use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: false, use_kospi_day_05: false, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: false, UD_kospi_day_05: false, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: false,
           use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: false, use_kosdaq_day_05: false, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: false, UD_kosdaq_day_05: false, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
       );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_반복_G",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: false,
           use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: false, use_kospi_day_05: false, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: false, UD_kospi_day_05: false, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: false,
           use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: false, use_kosdaq_day_05: false, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: false, UD_kosdaq_day_05: false, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
       );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_반복_H",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: false,
           use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: false, use_kospi_day_05: false, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: false, UD_kospi_day_05: false, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: false,
           use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: false, use_kosdaq_day_05: false, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: false, UD_kosdaq_day_05: false, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
       );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_반복_I",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: false,
           use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: false, use_kospi_day_05: false, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: false, UD_kospi_day_05: false, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: false,
           use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: false, use_kosdaq_day_05: false, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: false, UD_kosdaq_day_05: false, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
       );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_반복_J",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: false,
           use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: false, use_kospi_day_05: false, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: false, UD_kospi_day_05: false, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: false,
           use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: false, use_kosdaq_day_05: false, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: false, UD_kosdaq_day_05: false, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
       );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_반복_K",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: false,
           use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: false, use_kospi_day_05: false, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: false, UD_kospi_day_05: false, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: false,
           use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: false, use_kosdaq_day_05: false, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: false, UD_kosdaq_day_05: false, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
       );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_반복_L",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: false,
           use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: false, use_kospi_day_05: false, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: false, UD_kospi_day_05: false, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: false,
           use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: false, use_kosdaq_day_05: false, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: false, UD_kosdaq_day_05: false, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
       );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_반복_M",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: false,
           use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: false, use_kospi_day_05: false, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: false, UD_kospi_day_05: false, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: false,
           use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: false, use_kosdaq_day_05: false, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: false, UD_kosdaq_day_05: false, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
       );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_반복_N",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: false,
           use_kospi_min_03: false, use_kospi_min_05: false, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: false, use_kospi_day_05: false, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: false, UD_kospi_min_05: false, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: false, UD_kospi_day_05: false, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: false,
           use_kosdaq_min_03: false, use_kosdaq_min_05: false, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: false, use_kosdaq_day_05: false, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: false, UD_kosdaq_min_05: false, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: false, UD_kosdaq_day_05: false, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
       );


            계좌설정_이평선(
             // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
             대상그룹명: "CB_지수이평_리밸_A",
           
             // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
             코스피_사용여부: true,
             use_kospi_min_03: true, use_kospi_min_05: true, use_kospi_min_10: false,
             use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
             use_kospi_day_03: true, use_kospi_day_05: true, use_kospi_day_10: true,
             use_kospi_day_20: true, use_kospi_day_40: false, use_kospi_day_60: false,
             UD_kospi_min_03: true, UD_kospi_min_05: true, UD_kospi_min_10: false,
             UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
             UD_kospi_day_03: true, UD_kospi_day_05: true, UD_kospi_day_10: true,
             UD_kospi_day_20: true, UD_kospi_day_40: false, UD_kospi_day_60: false,
           
             // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
             코스닥_사용여부: true,
             use_kosdaq_min_03: true, use_kosdaq_min_05: true, use_kosdaq_min_10: false,
             use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
             use_kosdaq_day_03: true, use_kosdaq_day_05: true, use_kosdaq_day_10: true,
             use_kosdaq_day_20: true, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
             UD_kosdaq_min_03: true, UD_kosdaq_min_05: true, UD_kosdaq_min_10: false,
             UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
             UD_kosdaq_day_03: true, UD_kosdaq_day_05: true, UD_kosdaq_day_10: true,
             UD_kosdaq_day_20: true, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
             );

            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_리밸_B",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: true,
           use_kospi_min_03: true, use_kospi_min_05: true, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: true, use_kospi_day_05: true, use_kospi_day_10: true,
           use_kospi_day_20: true, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: true, UD_kospi_min_05: true, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: true, UD_kospi_day_05: true, UD_kospi_day_10: true,
           UD_kospi_day_20: true, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: true,
           use_kosdaq_min_03: true, use_kosdaq_min_05: true, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: true, use_kosdaq_day_05: true, use_kosdaq_day_10: true,
           use_kosdaq_day_20: true, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: true, UD_kosdaq_min_05: true, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: true, UD_kosdaq_day_05: true, UD_kosdaq_day_10: true,
           UD_kosdaq_day_20: true, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
           );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_리밸_C",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: true,
           use_kospi_min_03: true, use_kospi_min_05: true, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: true, use_kospi_day_05: true, use_kospi_day_10: true,
           use_kospi_day_20: true, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: true, UD_kospi_min_05: true, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: true, UD_kospi_day_05: true, UD_kospi_day_10: true,
           UD_kospi_day_20: true, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: true,
           use_kosdaq_min_03: true, use_kosdaq_min_05: true, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: true, use_kosdaq_day_05: true, use_kosdaq_day_10: true,
           use_kosdaq_day_20: true, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: true, UD_kosdaq_min_05: true, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: true, UD_kosdaq_day_05: true, UD_kosdaq_day_10: true,
           UD_kosdaq_day_20: true, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
           );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_리밸_D",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: true,
           use_kospi_min_03: true, use_kospi_min_05: true, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: true, use_kospi_day_05: true, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: true, UD_kospi_min_05: true, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: true, UD_kospi_day_05: true, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: true,
           use_kosdaq_min_03: true, use_kosdaq_min_05: true, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: true, use_kosdaq_day_05: true, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: true, UD_kosdaq_min_05: true, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: true, UD_kosdaq_day_05: true, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
           );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_리밸_E",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: true,
           use_kospi_min_03: true, use_kospi_min_05: true, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: true, use_kospi_day_05: true, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: true, UD_kospi_min_05: true, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: true, UD_kospi_day_05: true, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: true,
           use_kosdaq_min_03: true, use_kosdaq_min_05: true, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: true, use_kosdaq_day_05: true, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: true, UD_kosdaq_min_05: true, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: true, UD_kosdaq_day_05: true, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
           );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_리밸_F",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: true,
           use_kospi_min_03: true, use_kospi_min_05: true, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: true, use_kospi_day_05: true, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: true, UD_kospi_min_05: true, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: true, UD_kospi_day_05: true, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: true,
           use_kosdaq_min_03: true, use_kosdaq_min_05: true, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: true, use_kosdaq_day_05: true, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: true, UD_kosdaq_min_05: true, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: true, UD_kosdaq_day_05: true, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
           );
            계좌설정_이평선(
           // [+] 여기에 값을 강제로 주입할 대상 그룹명을 가장 먼저 적어줍니다.
           대상그룹명: "CB_지수이평_리밸_G",

           // ================= [ 코스피(KOSPI) 이평선 설정 ] =================
           코스피_사용여부: true,
           use_kospi_min_03: true, use_kospi_min_05: true, use_kospi_min_10: false,
           use_kospi_min_20: false, use_kospi_min_30: false, use_kospi_min_60: false,
           use_kospi_day_03: true, use_kospi_day_05: true, use_kospi_day_10: false,
           use_kospi_day_20: false, use_kospi_day_40: false, use_kospi_day_60: false,
           UD_kospi_min_03: true, UD_kospi_min_05: true, UD_kospi_min_10: false,
           UD_kospi_min_20: false, UD_kospi_min_30: false, UD_kospi_min_60: false,
           UD_kospi_day_03: true, UD_kospi_day_05: true, UD_kospi_day_10: false,
           UD_kospi_day_20: false, UD_kospi_day_40: false, UD_kospi_day_60: false,

           // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================
           코스닥_사용여부: true,
           use_kosdaq_min_03: true, use_kosdaq_min_05: true, use_kosdaq_min_10: false,
           use_kosdaq_min_20: false, use_kosdaq_min_30: false, use_kosdaq_min_60: false,
           use_kosdaq_day_03: true, use_kosdaq_day_05: true, use_kosdaq_day_10: false,
           use_kosdaq_day_20: false, use_kosdaq_day_40: false, use_kosdaq_day_60: false,
           UD_kosdaq_min_03: true, UD_kosdaq_min_05: true, UD_kosdaq_min_10: false,
           UD_kosdaq_min_20: false, UD_kosdaq_min_30: false, UD_kosdaq_min_60: false,
           UD_kosdaq_day_03: true, UD_kosdaq_day_05: true, UD_kosdaq_day_10: false,
           UD_kosdaq_day_20: false, UD_kosdaq_day_40: false, UD_kosdaq_day_60: false
           );

        }

        public static void 기본매매설정()
        {
            // 1. 신규 횟수 제한 설정
            기본매매_신규횟수제한(
                CB_신규횟수제한: true,
                TB_신규횟수제한: 6
            );

            // 2. 신규 매수 검색식 설정 (A, B, C)
            기본매매_new_A(
                사용: true, 검색식: "종목선정", 검색식사용법: 0,
                시작시간: 100000, 종료시간: 152000,
                비중: 1, 비중방법: 0,
                주문값: 0, 주문방법: 1,
                재진입: true, 유지: 0, 반복횟수: 0,
                취소시간: 3600, 취소후: 0
            );

            기본매매_new_B(
                사용: false, 검색식: "", 검색식사용법: 0,
                시작시간: 100000, 종료시간: 152000,
                비중: 1, 비중방법: 0,
                주문값: 0, 주문방법: 1,
                재진입: false, 유지: 0, 반복횟수: 0,
                취소시간: 3600, 취소후: 0
            );

            기본매매_new_C(
                사용: false, 검색식: "", 검색식사용법: 0,
                시작시간: 100000, 종료시간: 152000,
                비중: 1, 비중방법: 0,
                주문값: 0, 주문방법: 1,
                재진입: false, 유지: 0, 반복횟수: 0,
                취소시간: 3600, 취소후: 0
            );

            // 3. 신규 매수 기본/세부 조건 설정
            기본매매_신규매수조건(
                신규주가이상: 3000, 신규주가이하: 50000,
                신규등락률이상: -10, 신규등락률이하: 3,
                추가매수딜레이: 180,
                재매수: true, 재매수_지연시간: 180
            );

            기본매매_신규매수_세부조건(
                잔고개수_신규A: 20, 잔고개수_신규B: 10, 잔고개수_신규C: 10,
                재진입제한_A: 360000, 재진입제한_B: 360000, 재진입제한_C: 360000,
                익절재매수A: false, 익절재매수B: false, 익절재매수C: false
            );

            // 4. 트레일링스탑 공통 조건
            기본매매_트레일링스탑_조건(
                손실제한: true, 취소후: true, 기준금: true,
                canceltime: 60, cancel_sell: 1, repeat: 0
            );

            // 5. 트레일링스탑 개별 설정 (A~I) 
            // (표처럼 한눈에 보이게 정렬하여, 값 비교와 수정이 극도로 편해집니다!)
            기본매매_트레일링스탑_A(CB_TS: false, TB_TS_upper: 10, CBB_TS_upper: 2, TB_TS_down: -3.0, TB_TS_ratio: 25, CBB_TS_ratio: 3, TB_TS_Jumun: 0, CBB_TS_Jumun: 0);
            기본매매_트레일링스탑_B(CB_TS: false, TB_TS_upper: 10, CBB_TS_upper: 2, TB_TS_down: -4.0, TB_TS_ratio: 50, CBB_TS_ratio: 3, TB_TS_Jumun: 0, CBB_TS_Jumun: 0);
            기본매매_트레일링스탑_C(CB_TS: false, TB_TS_upper: 10, CBB_TS_upper: 2, TB_TS_down: -5.0, TB_TS_ratio: 100, CBB_TS_ratio: 3, TB_TS_Jumun: 0, CBB_TS_Jumun: 0);
            기본매매_트레일링스탑_D(CB_TS: false, TB_TS_upper: 10, CBB_TS_upper: 2, TB_TS_down: -5.2, TB_TS_ratio: 100, CBB_TS_ratio: 3, TB_TS_Jumun: 0, CBB_TS_Jumun: 0);
            기본매매_트레일링스탑_E(CB_TS: false, TB_TS_upper: 10, CBB_TS_upper: 2, TB_TS_down: -5.4, TB_TS_ratio: 100, CBB_TS_ratio: 3, TB_TS_Jumun: 0, CBB_TS_Jumun: 0);
            기본매매_트레일링스탑_F(CB_TS: false, TB_TS_upper: 10, CBB_TS_upper: 2, TB_TS_down: -5.6, TB_TS_ratio: 100, CBB_TS_ratio: 3, TB_TS_Jumun: 0, CBB_TS_Jumun: 0);
            기본매매_트레일링스탑_G(CB_TS: false, TB_TS_upper: 10, CBB_TS_upper: 2, TB_TS_down: -2.0, TB_TS_ratio: 1, CBB_TS_ratio: 3, TB_TS_Jumun: 0, CBB_TS_Jumun: 0);
            기본매매_트레일링스탑_H(CB_TS: false, TB_TS_upper: 10, CBB_TS_upper: 2, TB_TS_down: -2.0, TB_TS_ratio: 1, CBB_TS_ratio: 3, TB_TS_Jumun: 0, CBB_TS_Jumun: 0);
            기본매매_트레일링스탑_I(CB_TS: false, TB_TS_upper: 10, CBB_TS_upper: 2, TB_TS_down: -2.0, TB_TS_ratio: 1, CBB_TS_ratio: 3, TB_TS_Jumun: 0, CBB_TS_Jumun: 0);
        }

        private static void 반복매매설정()
        {
            // 1. 반복 기준금 및 추가매수 조건
            반복기준금_추매조건(기준금: true, 추매주가이상: 1000, 추매주가이하: 1000000, 추매등락률이상: -0.3, 추매등락률이하: 22);

            // 2. 반복매매 A ~ N 상세 설정 (한 줄 정렬)
            반복매매_A(사용: false, 시작시간: 150500, 종료시간: 151500, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 1000, TB_mma1: 3, CBB_mma1: 1, TB_mma2: 5, CBB_mma2: 1, CBB_mma_배열: 1, TB_dma1: 5, CBB_dma1: 1, TB_dma2: 10, CBB_dma2: 1, CBB_dma_배열: 1, 수익범위1: -100, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 2, 매매범위1: 0, 매매범위2: 1000, 매매범위기준: 1, 반복시간: 1800, 주문가격: 0, 매수매도: 0, 취소시간: 360, 취n주문: 0, 재주문: 0);
            반복매매_B(사용: false, 시작시간: 80000, 종료시간: 200000, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 0, TB_mma1: 3, CBB_mma1: 0, TB_mma2: 5, CBB_mma2: 0, CBB_mma_배열: 0, TB_dma1: 5, CBB_dma1: 0, TB_dma2: 10, CBB_dma2: 0, CBB_dma_배열: 0, 수익범위1: 0, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 0, 매매범위1: 0, 매매범위2: 100, 매매범위기준: 0, 반복시간: 60, 주문가격: 0, 매수매도: 0, 취소시간: 180, 취n주문: 0, 재주문: 0);
            반복매매_C(사용: false, 시작시간: 80000, 종료시간: 200000, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 0, TB_mma1: 3, CBB_mma1: 0, TB_mma2: 5, CBB_mma2: 0, CBB_mma_배열: 0, TB_dma1: 5, CBB_dma1: 0, TB_dma2: 10, CBB_dma2: 0, CBB_dma_배열: 0, 수익범위1: 0, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 0, 매매범위1: 0, 매매범위2: 100, 매매범위기준: 0, 반복시간: 60, 주문가격: 0, 매수매도: 0, 취소시간: 180, 취n주문: 0, 재주문: 0);
            반복매매_D(사용: false, 시작시간: 80000, 종료시간: 200000, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 0, TB_mma1: 3, CBB_mma1: 0, TB_mma2: 5, CBB_mma2: 0, CBB_mma_배열: 0, TB_dma1: 5, CBB_dma1: 0, TB_dma2: 10, CBB_dma2: 0, CBB_dma_배열: 0, 수익범위1: 0, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 0, 매매범위1: 0, 매매범위2: 100, 매매범위기준: 0, 반복시간: 60, 주문가격: 0, 매수매도: 0, 취소시간: 180, 취n주문: 0, 재주문: 0);
            반복매매_E(사용: false, 시작시간: 80000, 종료시간: 200000, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 0, TB_mma1: 3, CBB_mma1: 0, TB_mma2: 5, CBB_mma2: 0, CBB_mma_배열: 0, TB_dma1: 5, CBB_dma1: 0, TB_dma2: 10, CBB_dma2: 0, CBB_dma_배열: 0, 수익범위1: 0, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 0, 매매범위1: 0, 매매범위2: 100, 매매범위기준: 0, 반복시간: 60, 주문가격: 0, 매수매도: 0, 취소시간: 180, 취n주문: 0, 재주문: 0);
            반복매매_F(사용: false, 시작시간: 80000, 종료시간: 200000, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 0, TB_mma1: 3, CBB_mma1: 0, TB_mma2: 5, CBB_mma2: 0, CBB_mma_배열: 0, TB_dma1: 5, CBB_dma1: 0, TB_dma2: 10, CBB_dma2: 0, CBB_dma_배열: 0, 수익범위1: 0, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 0, 매매범위1: 0, 매매범위2: 100, 매매범위기준: 0, 반복시간: 60, 주문가격: 0, 매수매도: 0, 취소시간: 180, 취n주문: 0, 재주문: 0);
            반복매매_G(사용: false, 시작시간: 80000, 종료시간: 200000, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 0, TB_mma1: 3, CBB_mma1: 0, TB_mma2: 5, CBB_mma2: 0, CBB_mma_배열: 0, TB_dma1: 5, CBB_dma1: 0, TB_dma2: 10, CBB_dma2: 0, CBB_dma_배열: 0, 수익범위1: 0, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 0, 매매범위1: 0, 매매범위2: 100, 매매범위기준: 0, 반복시간: 60, 주문가격: 0, 매수매도: 0, 취소시간: 180, 취n주문: 0, 재주문: 0);
            반복매매_H(사용: false, 시작시간: 80000, 종료시간: 200000, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 0, TB_mma1: 3, CBB_mma1: 0, TB_mma2: 5, CBB_mma2: 0, CBB_mma_배열: 0, TB_dma1: 5, CBB_dma1: 0, TB_dma2: 10, CBB_dma2: 0, CBB_dma_배열: 0, 수익범위1: 0, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 0, 매매범위1: 0, 매매범위2: 100, 매매범위기준: 0, 반복시간: 60, 주문가격: 0, 매수매도: 0, 취소시간: 180, 취n주문: 0, 재주문: 0);
            반복매매_I(사용: false, 시작시간: 80000, 종료시간: 200000, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 0, TB_mma1: 3, CBB_mma1: 0, TB_mma2: 5, CBB_mma2: 0, CBB_mma_배열: 0, TB_dma1: 5, CBB_dma1: 0, TB_dma2: 10, CBB_dma2: 0, CBB_dma_배열: 0, 수익범위1: 0, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 0, 매매범위1: 0, 매매범위2: 100, 매매범위기준: 0, 반복시간: 60, 주문가격: 0, 매수매도: 0, 취소시간: 180, 취n주문: 0, 재주문: 0);
            반복매매_J(사용: false, 시작시간: 80000, 종료시간: 200000, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 0, TB_mma1: 3, CBB_mma1: 0, TB_mma2: 5, CBB_mma2: 0, CBB_mma_배열: 0, TB_dma1: 5, CBB_dma1: 0, TB_dma2: 10, CBB_dma2: 0, CBB_dma_배열: 0, 수익범위1: 0, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 0, 매매범위1: 0, 매매범위2: 100, 매매범위기준: 0, 반복시간: 60, 주문가격: 0, 매수매도: 0, 취소시간: 180, 취n주문: 0, 재주문: 0);
            반복매매_K(사용: false, 시작시간: 80000, 종료시간: 200000, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 0, TB_mma1: 3, CBB_mma1: 0, TB_mma2: 5, CBB_mma2: 0, CBB_mma_배열: 0, TB_dma1: 5, CBB_dma1: 0, TB_dma2: 10, CBB_dma2: 0, CBB_dma_배열: 0, 수익범위1: 0, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 0, 매매범위1: 0, 매매범위2: 100, 매매범위기준: 0, 반복시간: 60, 주문가격: 0, 매수매도: 0, 취소시간: 180, 취n주문: 0, 재주문: 0);
            반복매매_L(사용: false, 시작시간: 80000, 종료시간: 200000, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 0, TB_mma1: 3, CBB_mma1: 0, TB_mma2: 5, CBB_mma2: 0, CBB_mma_배열: 0, TB_dma1: 5, CBB_dma1: 0, TB_dma2: 10, CBB_dma2: 0, CBB_dma_배열: 0, 수익범위1: 0, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 0, 매매범위1: 0, 매매범위2: 100, 매매범위기준: 0, 반복시간: 60, 주문가격: 0, 매수매도: 0, 취소시간: 180, 취n주문: 0, 재주문: 0);
            반복매매_M(사용: false, 시작시간: 80000, 종료시간: 200000, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 0, TB_mma1: 3, CBB_mma1: 0, TB_mma2: 5, CBB_mma2: 0, CBB_mma_배열: 0, TB_dma1: 5, CBB_dma1: 0, TB_dma2: 10, CBB_dma2: 0, CBB_dma_배열: 0, 수익범위1: 0, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 0, 매매범위1: 0, 매매범위2: 100, 매매범위기준: 0, 반복시간: 60, 주문가격: 0, 매수매도: 0, 취소시간: 180, 취n주문: 0, 재주문: 0);
            반복매매_N(사용: false, 시작시간: 80000, 종료시간: 200000, 매매종류: true, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 0, TB_mma1: 3, CBB_mma1: 0, TB_mma2: 5, CBB_mma2: 0, CBB_mma_배열: 0, TB_dma1: 5, CBB_dma1: 0, TB_dma2: 10, CBB_dma2: 0, CBB_dma_배열: 0, 수익범위1: 0, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 0, 매매범위1: 0, 매매범위2: 100, 매매범위기준: 0, 반복시간: 60, 주문가격: 0, 매수매도: 0, 취소시간: 180, 취n주문: 0, 재주문: 0);
        }

        private static void 계좌관리설정()
        {
            // 1. 공통 및 기본 조건 설정
            계좌관리_추매조건(CB_총매수금: false, 종목최대매수금: 1000, CB_일매수제한금: true, 일매수제한금: 20, CB_회수제한: false, 회수제한: 5, 추매주가이상: 1000, 추매주가이하: 1000000, 추매등락률이상: -0.3, 추매등락률이하: 22);
            계좌관리_분할주문(분할간격_A: -2, 분할횟수_A: -1, 분할간격_B: 1, 분할횟수_B: 4, 분할간격_C: 5, 분할횟수_C: 3);
            계좌관리_기준비율관리(CB_매수기준: false, TB_매수비율: 14, CB_손익기준: false, TB_손익비율: 14);
            계좌관리_감시주문시간n기준금(Selltime_오전: 90300, Selltime_오후: 151000, rebalance_기준금: true, cut_기준금: true, Liquidation_기준금: true);

            // =========================================================================
            // 2. 계좌관리 리밸런싱 (A ~ G)
            // =========================================================================
            계좌관리_리밸런싱_A(사용: true, 시작시간: 91000, 종료시간: 151000, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 0, 누적거래대금: 1000, TB_mma1: 3, CBB_mma1: 1, TB_mma2: 5, CBB_mma2: 1, CBB_mma_배열: 1, TB_dma1: 5, CBB_dma1: 1, TB_dma2: 10, CBB_dma2: 1, CBB_dma_배열: 1, 수익범위1: 0.3, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 3, 매수구분: 2, 매매범위1: 0, 매매범위2: 1000, 매매범위기준: 1, 반복시간: 10, 주문가격: 0, 매수매도: 1, 취소시간: 60, 감시시점: false, 주문조건_1차: 1.5, 주문조건선택_1차: "매도수익률", 매도비중_1차: 100, 취소시간_1차: 3600, 주문조건_2차: -5, 주문조건선택_2차: "손절매도수익률", 매도비중_2차: 50, 취소시간_2차: 3600, 매도체크: true, 감시주문시간: 0, 감시주문값: 0, 감시매수매도: 1, TS_1차: false, TS_1차_down: 0, TB_1차_이평: 5, CBB_1차_이평: 0, TS_2차: false, TS_2차_down: 0, TB_2차_이평: 5, CBB_2차_이평: 0);
            계좌관리_리밸런싱_B(사용: true, 시작시간: 91000, 종료시간: 151000, 검색식사용: 1, 검색식: "매수탐색_B", 검색유지시간: 0, 매입금: 0, 누적거래량: 100000, 누적거래대금: 10000, TB_mma1: 3, CBB_mma1: 1, TB_mma2: 5, CBB_mma2: 1, CBB_mma_배열: 1, TB_dma1: 5, CBB_dma1: 1, TB_dma2: 10, CBB_dma2: 1, CBB_dma_배열: 1, 수익범위1: -100, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 3, 매수구분: 2, 매매범위1: 0, 매매범위2: 1000, 매매범위기준: 1, 반복시간: 60, 주문가격: 0, 매수매도: 1, 취소시간: 60, 감시시점: false, 주문조건_1차: 1.5, 주문조건선택_1차: "매도수익률", 매도비중_1차: 100, 취소시간_1차: 3600, 주문조건_2차: -5, 주문조건선택_2차: "손절매도수익률", 매도비중_2차: 50, 취소시간_2차: 3600, 매도체크: true, 감시주문시간: 0, 감시주문값: 0, 감시매수매도: 1, TS_1차: false, TS_1차_down: 0, TB_1차_이평: 5, CBB_1차_이평: 0, TS_2차: false, TS_2차_down: 0, TB_2차_이평: 5, CBB_2차_이평: 0);
            계좌관리_리밸런싱_C(사용: true, 시작시간: 91000, 종료시간: 151000, 검색식사용: 1, 검색식: "매수탐색_A", 검색유지시간: 0, 매입금: 0, 누적거래량: 100000, 누적거래대금: 10000, TB_mma1: 3, CBB_mma1: 1, TB_mma2: 5, CBB_mma2: 1, CBB_mma_배열: 1, TB_dma1: 5, CBB_dma1: 1, TB_dma2: 10, CBB_dma2: 1, CBB_dma_배열: 1, 수익범위1: -100, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 3, 매수구분: 2, 매매범위1: 0, 매매범위2: 1000, 매매범위기준: 1, 반복시간: 60, 주문가격: 0, 매수매도: 1, 취소시간: 60, 감시시점: false, 주문조건_1차: 1.5, 주문조건선택_1차: "매도수익률", 매도비중_1차: 100, 취소시간_1차: 3600, 주문조건_2차: -5, 주문조건선택_2차: "손절매도수익률", 매도비중_2차: 50, 취소시간_2차: 3600, 매도체크: true, 감시주문시간: 0, 감시주문값: 0, 감시매수매도: 1, TS_1차: false, TS_1차_down: 0, TB_1차_이평: 5, CBB_1차_이평: 0, TS_2차: false, TS_2차_down: 0, TB_2차_이평: 5, CBB_2차_이평: 0);
            계좌관리_리밸런싱_D(사용: true, 시작시간: 91000, 종료시간: 151000, 검색식사용: 1, 검색식: "매수탐색_A", 검색유지시간: 0, 매입금: 0, 누적거래량: 100000, 누적거래대금: 10000, TB_mma1: 3, CBB_mma1: 1, TB_mma2: 5, CBB_mma2: 1, CBB_mma_배열: 1, TB_dma1: 5, CBB_dma1: 1, TB_dma2: 10, CBB_dma2: 1, CBB_dma_배열: 1, 수익범위1: -2, 수익범위선택: false, 수익범위2: 100, 수익구분: 0, 매수비중: 1, 매수구분: 2, 매매범위1: 0, 매매범위2: 1000, 매매범위기준: 1, 반복시간: 60, 주문가격: 0, 매수매도: 1, 취소시간: 60, 감시시점: false, 주문조건_1차: 1.2, 주문조건선택_1차: "매도수익률", 매도비중_1차: 100, 취소시간_1차: 3600, 주문조건_2차: -5, 주문조건선택_2차: "손절매도수익률", 매도비중_2차: 100, 취소시간_2차: 3600, 매도체크: false, 감시주문시간: 0, 감시주문값: 0, 감시매수매도: 1, TS_1차: false, TS_1차_down: 0, TB_1차_이평: 5, CBB_1차_이평: 0, TS_2차: false, TS_2차_down: 0, TB_2차_이평: 5, CBB_2차_이평: 0);
            계좌관리_리밸런싱_E(사용: true, 시작시간: 130000, 종료시간: 151500, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 1000, 누적거래대금: 1000, TB_mma1: 3, CBB_mma1: 1, TB_mma2: 5, CBB_mma2: 1, CBB_mma_배열: 1, TB_dma1: 5, CBB_dma1: 1, TB_dma2: 10, CBB_dma2: 1, CBB_dma_배열: 1, 수익범위1: 0, 수익범위선택: false, 수익범위2: -3, 수익구분: 7, 매수비중: 4, 매수구분: 2, 매매범위1: 0, 매매범위2: 1000, 매매범위기준: 1, 반복시간: 5, 주문가격: 0, 매수매도: 1, 취소시간: 180, 감시시점: false, 주문조건_1차: 3, 주문조건선택_1차: "매도수익률", 매도비중_1차: 100, 취소시간_1차: 3600, 주문조건_2차: 0, 주문조건선택_2차: "(    X    )", 매도비중_2차: 10, 취소시간_2차: 3600, 매도체크: false, 감시주문시간: 0, 감시주문값: 0, 감시매수매도: 1, TS_1차: false, TS_1차_down: 0, TB_1차_이평: 5, CBB_1차_이평: 0, TS_2차: false, TS_2차_down: 0, TB_2차_이평: 5, CBB_2차_이평: 0);
            계좌관리_리밸런싱_F(사용: true, 시작시간: 130000, 종료시간: 151500, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 1000, 누적거래대금: 1000, TB_mma1: 3, CBB_mma1: 1, TB_mma2: 5, CBB_mma2: 1, CBB_mma_배열: 1, TB_dma1: 5, CBB_dma1: 1, TB_dma2: 10, CBB_dma2: 1, CBB_dma_배열: 1, 수익범위1: 0, 수익범위선택: false, 수익범위2: -6, 수익구분: 7, 매수비중: 4, 매수구분: 2, 매매범위1: 0, 매매범위2: 1000, 매매범위기준: 1, 반복시간: 5, 주문가격: 0, 매수매도: 1, 취소시간: 180, 감시시점: false, 주문조건_1차: 6, 주문조건선택_1차: "매도수익률", 매도비중_1차: 100, 취소시간_1차: 3600, 주문조건_2차: 0, 주문조건선택_2차: "(    X    )", 매도비중_2차: 1, 취소시간_2차: 3600, 매도체크: false, 감시주문시간: 0, 감시주문값: 0, 감시매수매도: 1, TS_1차: false, TS_1차_down: 0, TB_1차_이평: 5, CBB_1차_이평: 0, TS_2차: false, TS_2차_down: 0, TB_2차_이평: 5, CBB_2차_이평: 0);
            계좌관리_리밸런싱_G(사용: true, 시작시간: 130000, 종료시간: 151500, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금: 0, 누적거래량: 1000, 누적거래대금: 1000, TB_mma1: 3, CBB_mma1: 1, TB_mma2: 5, CBB_mma2: 1, CBB_mma_배열: 1, TB_dma1: 5, CBB_dma1: 1, TB_dma2: 10, CBB_dma2: 1, CBB_dma_배열: 1, 수익범위1: 0, 수익범위선택: false, 수익범위2: -9, 수익구분: 7, 매수비중: 4, 매수구분: 2, 매매범위1: 0, 매매범위2: 1000, 매매범위기준: 1, 반복시간: 5, 주문가격: 0, 매수매도: 1, 취소시간: 180, 감시시점: false, 주문조건_1차: 9, 주문조건선택_1차: "매도수익률", 매도비중_1차: 100, 취소시간_1차: 3600, 주문조건_2차: 0, 주문조건선택_2차: "(    X    )", 매도비중_2차: 10, 취소시간_2차: 3600, 매도체크: false, 감시주문시간: 0, 감시주문값: 0, 감시매수매도: 1, TS_1차: false, TS_1차_down: 0, TB_1차_이평: 5, CBB_1차_이평: 0, TS_2차: false, TS_2차_down: 0, TB_2차_이평: 5, CBB_2차_이평: 0);

            // =========================================================================
            // 3. 계좌관리 잔고청산 (A ~ C)
            // =========================================================================
            계좌관리_잔고청산_A(사용: true, 시작시간: 80000, 종료시간: 200000, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금1: 0, 매입금2: 10000, TB이평: 30, CBB이평: 0, 수익범위1: 3, 수익범위선택: false, 수익범위2: 20, 수익구분: 4, 매도비중: 20, 매도구분: 3, 매매범위1: 0, 매매범위2: 100, 반복시간: 5, 주문가격: 0, 매수매도: 1, 취소시간: 30, 취소후주문: 0, 반복횟수: 0, 매도정지: true, 추매금지: true, 강제매도: true, 수익보전: true, TS: true, TS_down: -2, TB_TS_mma: 3, CBB_TS_mma: 2, TB_TS_dma: 3, CBB_TS_dma: 0);
            계좌관리_잔고청산_B(사용: true, 시작시간: 80000, 종료시간: 200000, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금1: 0, 매입금2: 10000, TB이평: 30, CBB이평: 0, 수익범위1: 10, 수익범위선택: false, 수익범위2: 0, 수익구분: 4, 매도비중: 20, 매도구분: 3, 매매범위1: 50, 매매범위2: 100, 반복시간: 5, 주문가격: 0, 매수매도: 1, 취소시간: 30, 취소후주문: 0, 반복횟수: 0, 매도정지: false, 추매금지: false, 강제매도: true, 수익보전: true, TS: true, TS_down: -2, TB_TS_mma: 3, CBB_TS_mma: 2, TB_TS_dma: 3, CBB_TS_dma: 0);
            계좌관리_잔고청산_C(사용: true, 시작시간: 80000, 종료시간: 200000, 검색식사용: 0, 검색식: "", 검색유지시간: 0, 매입금1: 0, 매입금2: 10000, TB이평: 30, CBB이평: 0, 수익범위1: 20, 수익범위선택: false, 수익범위2: 0, 수익구분: 4, 매도비중: 20, 매도구분: 3, 매매범위1: 0, 매매범위2: 100, 반복시간: 5, 주문가격: 0, 매수매도: 1, 취소시간: 30, 취소후주문: 0, 반복횟수: 0, 매도정지: false, 추매금지: false, 강제매도: true, 수익보전: true, TS: true, TS_down: -2, TB_TS_mma: 3, CBB_TS_mma: 2, TB_TS_dma: 3, CBB_TS_dma: 0);

            // =========================================================================
            // 4. 실현손익 담보 손실매도 (A ~ C)
            // =========================================================================
            계좌관리_실현손익담보손실매도_A(CB_cut: true, MTB_cut_time: 151500, TB_cut_수익금1: 5, TB_cut_수익금2: 100, TB_cut_남길퍼: 50, TB_cut_won: 25, TB_cut_P: -20, TB_cut_ratio: 5, CBB_cut_gubun: 2, TB_cut_value: 0, CBB_cut_jumun: 1, MTB_cut_cansel_time: 60);
            계좌관리_실현손익담보손실매도_B(CB_cut: false, MTB_cut_time: 151500, TB_cut_수익금1: 3, TB_cut_수익금2: 100, TB_cut_남길퍼: 50, TB_cut_won: 25, TB_cut_P: -20, TB_cut_ratio: 5, CBB_cut_gubun: 2, TB_cut_value: 0, CBB_cut_jumun: 1, MTB_cut_cansel_time: 60);
            계좌관리_실현손익담보손실매도_C(CB_cut: false, MTB_cut_time: 151500, TB_cut_수익금1: 5, TB_cut_수익금2: 100, TB_cut_남길퍼: 50, TB_cut_won: 25, TB_cut_P: -20, TB_cut_ratio: 5, CBB_cut_gubun: 2, TB_cut_value: 0, CBB_cut_jumun: 1, MTB_cut_cansel_time: 60);
        }

        private static void 특수설정()
        {
            // 1. 신규 그룹 설정 (오버로딩된 두 개의 함수 각각 이름표 부착!)
            특수설정_신규그룹(A: 3, B: 2, C: 1);
            특수설정_신규그룹(기준금: true, CB_매매기간_오전: false, TB_매매기간_오전주문시간: 90300, CB_매매기간_오후: false, TB_매매기간_오후주문시간: 150000);

            // =========================================================================
            // 2. 특수설정 매매기간주문 A ~ F 상세 설정 (한 줄 정렬)
            // =========================================================================
            특수설정_매매기간주문_A(trading: 0, 기간: 7, 주문시간: 2, 기준: 2.0, 기준방법: 4, 매도비중: 50, 매도방법: 3, 주문가격: 0, 주문방법: 1, 취소시간: 60, 강제매도: true, 수익보전: true, TS: false, TS_down: 0, TB_TS_mma: 5, CBB_TS_mma: -1, TB_TS_dma: 5, CBB_TS_dma: -1);
            특수설정_매매기간주문_B(trading: 0, 기간: 14, 주문시간: 2, 기준: 1.0, 기준방법: 4, 매도비중: 50, 매도방법: 3, 주문가격: 0, 주문방법: 1, 취소시간: 60, 강제매도: true, 수익보전: true, TS: false, TS_down: 0, TB_TS_mma: 5, CBB_TS_mma: -1, TB_TS_dma: 5, CBB_TS_dma: -1);
            특수설정_매매기간주문_C(trading: 0, 기간: 21, 주문시간: 2, 기준: 0.5, 기준방법: 4, 매도비중: 50, 매도방법: 3, 주문가격: 0, 주문방법: 1, 취소시간: 60, 강제매도: true, 수익보전: true, TS: false, TS_down: 0, TB_TS_mma: 5, CBB_TS_mma: -1, TB_TS_dma: 5, CBB_TS_dma: -1);
            특수설정_매매기간주문_D(trading: 0, 기간: 30, 주문시간: 2, 기준: 2.5, 기준방법: 4, 매도비중: 50, 매도방법: 3, 주문가격: 0, 주문방법: 1, 취소시간: 60, 강제매도: true, 수익보전: true, TS: false, TS_down: 0, TB_TS_mma: 5, CBB_TS_mma: -1, TB_TS_dma: 5, CBB_TS_dma: -1);
            특수설정_매매기간주문_E(trading: 0, 기간: 60, 주문시간: 2, 기준: 2.5, 기준방법: 4, 매도비중: 50, 매도방법: 3, 주문가격: 0, 주문방법: 1, 취소시간: 60, 강제매도: true, 수익보전: true, TS: false, TS_down: 0, TB_TS_mma: 5, CBB_TS_mma: -1, TB_TS_dma: 5, CBB_TS_dma: -1);
            특수설정_매매기간주문_F(trading: 0, 기간: 10, 주문시간: 2, 기준: 3.0, 기준방법: 4, 매도비중: 50, 매도방법: 3, 주문가격: 0, 주문방법: 1, 취소시간: 60, 강제매도: true, 수익보전: true, TS: false, TS_down: 0, TB_TS_mma: 5, CBB_TS_mma: -1, TB_TS_dma: 5, CBB_TS_dma: -1);
        }

        private static void 매매그룹설정()
        {
            // =========================================================================
            // 1. 공통 매매 설정 (익절, 손절)
            // =========================================================================
            매매그룹설정_익절(A: true, B: true, C: true, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_손절(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);

            // =========================================================================
            // 2. 반복매매 (A ~ N)
            // =========================================================================
            매매그룹설정_반복A(A: true, B: true, C: true, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_반복B(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_반복C(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_반복D(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_반복E(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_반복F(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_반복G(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_반복H(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_반복I(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_반복J(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_반복K(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_반복L(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_반복M(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_반복N(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);

            // =========================================================================
            // 3. 리밸런싱 (A ~ G)
            // =========================================================================
            매매그룹설정_리밸_A(A: true, B: true, C: true, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_리밸_B(A: true, B: true, C: true, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_리밸_C(A: true, B: true, C: true, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_리밸_D(A: true, B: true, C: true, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_리밸_E(A: true, B: true, C: true, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_리밸_F(A: true, B: true, C: true, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_리밸_G(A: true, B: true, C: true, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);

            // =========================================================================
            // 4. 잔고청산 (A ~ C)
            // =========================================================================
            매매그룹설정_청산_A(A: true, B: true, C: true, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_청산_B(A: false, B: true, C: true, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_청산_C(A: false, B: true, C: true, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);

            // =========================================================================
            // 5. 기간매도 (A ~ F)
            // =========================================================================
            매매그룹설정_기간매도_A(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_기간매도_B(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_기간매도_C(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_기간매도_D(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_기간매도_E(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_기간매도_F(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);

            // =========================================================================
            // 6. 기타 계좌/시간 관리
            // =========================================================================
            매매그룹설정_미수금정리(A: true, B: true, C: true, D: false, E: true, F: false, G: false, H: false, I: false, J: false, K: false, L: false);

            매매그룹설정_손익담보매도_A(A: true, B: true, C: true, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_손익담보매도_B(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_손익담보매도_C(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);

            매매그룹설정_계좌청산_특정시간(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_계좌청산_실현손익(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_계좌청산_예상손실(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_계좌청산_예상수익(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);

            매매그룹설정_시간청산_A(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_시간청산_B(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
            매매그룹설정_시간청산_C(A: false, B: false, C: false, D: false, E: false, F: false, G: false, H: false, I: false, J: false, K: false, L: false);
        }

        private static void 대금탐색설정()
        {
            // 1. 공통 누적거래대금 설정
            대금탐색_누적거래대금(거래대금: 1000);

            // =========================================================================
            // 2. 대금탐색 매수 설정 (A, B)
            // =========================================================================
            대금탐색_매수_A(
                잔량: 2, 매도호가별대금: 1, 매도호가합대금: 100, 매수호가별대금: 1, 매수호가합대금: 100,
                매수탐색사용: true, 기준초: 1, Combo_기준초회: 1, 탐색등락률: 0, 상승카운터: 0, 상승옵션: true, 하락카운터: 0, 하락옵션: false,
                탐색주가_1: 5000, 탐색주가_2: 20000, 탐색주가_3: 50000, 탐색주가_4: 200000, 탐색주가_5: 500000, 탐색주가_6: 500000,
                탐색대금_1: 3000, 탐색대금_2: 5000, 탐색대금_3: 5000, 탐색대금_4: 5000, 탐색대금_5: 10000, 탐색대금_6: 10000,
                반복: 30, 분봉: 0, 일봉: 3
            );

            대금탐색_매수_B(
                잔량: 2, 매도호가별대금: 1, 매도호가합대금: 100, 매수호가별대금: 1, 매수호가합대금: 100,
                매수탐색사용: true, 기준초: 1, Combo_기준초회: 0, 탐색등락률: 0, 상승카운터: 1, 상승옵션: true, 하락카운터: 0, 하락옵션: false,
                탐색주가_1: 5000, 탐색주가_2: 20000, 탐색주가_3: 50000, 탐색주가_4: 200000, 탐색주가_5: 500000, 탐색주가_6: 500000,
                탐색대금_1: 3000, 탐색대금_2: 5000, 탐색대금_3: 5000, 탐색대금_4: 5000, 탐색대금_5: 10000, 탐색대금_6: 10000,
                반복: 30, 분봉: 0, 일봉: 3
            );

            // =========================================================================
            // 3. 대금탐색 매도 설정
            // =========================================================================
            대금탐색_매도(
                매도탐색사용: false, 기준초: 1, Combo_기준초회: 1, 탐색등락률: 1, 상승카운터: 2, 상승옵션: true, 하락카운터: 1, 하락옵션: true,
                탐색주가_1: 5000, 탐색주가_2: 500000, 탐색주가_3: 200000, 탐색주가_4: 50000, 탐색주가_5: 20000, 탐색주가_6: 500000,
                탐색대금_1: 3000, 탐색대금_2: 30000, 탐색대금_3: 20000, 탐색대금_4: 10000, 탐색대금_5: 5000, 탐색대금_6: 30000,
                분봉: 1, 일봉: 1
            );
        }

        // =========================================================================
        // 4. 공통 기능 설정 (True/False 지옥 탈출)
        // =========================================================================
        private static void 기능설정()
        {
            기능설정값(
                편입추가: false,
                최종가업데이트: true,
                신규매수정지: false,
                추가매수정지: false,
                VI매수취소: true,
                VI매도취소: false,
                상매수취소: true,
                하매도취소: false,
                상전량청산: false,
                하전량청산: false,
                ETF매입비제외: true,
                CB_중간가주문: true,
                NXT: true,
                NXT_매수금지: true,
                NXT_손실제한: true
            );
        }

        private static string 검색식_유무확인(bool 사용, string 위치, string 검색식)
        {
            if (사용 && !검색식.Equals("") && !검색식.Equals("매수탐색_A") && !검색식.Equals("매수탐색_B") && !검색식.Equals("매도탐색"))
            {
                Condition item = Form1.ConditionList.Find(o => o.name.Equals(검색식));
                if (item == null)
                {
                    Form1.form1.CB_기능설정.Checked = false;
                    Form1.form1.tab_체결.SelectedIndex = 1;
                    Form1.MBC_sender = "";
                    Form1.AutoClosingAlram("검색식 에러알림", "검색식 가동 에러Log.동작기록를 확인하세요.", 30000, "검색식알림");

                    Log.동작기록("[검색식 가동실패] " + 위치 + " -> ' " + 검색식 + " ' 검색식이 없어 가동할수 없습니다.");
                    검색식 = "";
                }
            }
            return 검색식;
        }

        private static void 패널감추기_신규매수()
        {
            // [UI 숨기기]
            Form_Basic.form.P_계좌매매.Hide();
            Form_Basic.form.P_손실청산.Hide();
            Form_Basic.form.P_익절매도.Hide();
            Form_Basic.form.P_잔고시간청산.Hide();
            Form_Basic.form.P_체결즉시.Hide();

            // [패널 위치 조정]
            Form_Basic.form.P_진입조건.Location = new System.Drawing.Point(430, 141);
            Form_Basic.form.lb_TS.Location = new System.Drawing.Point(-1, 120);
            Form_Basic.form.P_트레일링스탑.Location = new System.Drawing.Point(-1, 141);
            Form_Basic.form.P_트레일링스탑.SendToBack();

            // [라벨 비활성화]
            Form_Basic.form.label_신규취소_A.Enabled = false;
            Form_Basic.form.label_신규취소_B.Enabled = false;
            Form_Basic.form.label_신규취소_C.Enabled = false;

            // [설정값 및 UI 체크박스 모두 끄기 (False)]

            // 1. 익절 모니터 (CB_ik)
            Form_Basic.form.CB_ik_A.Checked = GenieConfig.CB_ik_A = false;
            Form_Basic.form.CB_ik_B.Checked = GenieConfig.CB_ik_B = false;
            Form_Basic.form.CB_ik_C.Checked = GenieConfig.CB_ik_C = false;
            Form_Basic.form.CB_ik_D.Checked = GenieConfig.CB_ik_D = false;
            Form_Basic.form.CB_ik_E.Checked = GenieConfig.CB_ik_E = false;
            Form_Basic.form.CB_ik_F.Checked = GenieConfig.CB_ik_F = false;
            Form_Basic.form.CB_ik_G.Checked = GenieConfig.CB_ik_G = false;
            Form_Basic.form.CB_ik_H.Checked = GenieConfig.CB_ik_H = false;
            Form_Basic.form.CB_ik_I.Checked = GenieConfig.CB_ik_I = false;

            // 2. 익절 한 번만 (CB_ik_one)
            Form_Basic.form.CB_ik_one_A.Checked = GenieConfig.CB_ik_one_A = false;
            Form_Basic.form.CB_ik_one_B.Checked = GenieConfig.CB_ik_one_B = false;
            Form_Basic.form.CB_ik_one_C.Checked = GenieConfig.CB_ik_one_C = false;
            Form_Basic.form.CB_ik_one_D.Checked = GenieConfig.CB_ik_one_D = false;
            Form_Basic.form.CB_ik_one_E.Checked = GenieConfig.CB_ik_one_E = false;
            Form_Basic.form.CB_ik_one_F.Checked = GenieConfig.CB_ik_one_F = false;
            Form_Basic.form.CB_ik_one_G.Checked = GenieConfig.CB_ik_one_G = false;
            Form_Basic.form.CB_ik_one_H.Checked = GenieConfig.CB_ik_one_H = false;
            Form_Basic.form.CB_ik_one_I.Checked = GenieConfig.CB_ik_one_I = false;

            // 3. 손절 (CB_sell_use)
            Form_Basic.form.CB_sell_use_A.Checked = GenieConfig.CB_sell_use_A = false;
            Form_Basic.form.CB_sell_use_B.Checked = GenieConfig.CB_sell_use_B = false;
            Form_Basic.form.CB_sell_use_C.Checked = GenieConfig.CB_sell_use_C = false;
            Form_Basic.form.CB_sell_use_D.Checked = GenieConfig.CB_sell_use_D = false;
            Form_Basic.form.CB_sell_use_E.Checked = GenieConfig.CB_sell_use_E = false;
            Form_Basic.form.CB_sell_use_F.Checked = GenieConfig.CB_sell_use_F = false;

            // 4. 기타 설정 (CB_sell_time_use, CB_silson_use_W 등)
            Form_Basic.form.CB_sell_time_use.Checked = GenieConfig.CB_sell_time_use = false;
            Form_Basic.form.CB_silson_use_W.Checked = GenieConfig.CB_silson_use_W = false;
            Form_Basic.form.CB_예상손실_use.Checked = GenieConfig.CB_예상손실_use = false;
            Form_Basic.form.CB_예상수익사용.Checked = GenieConfig.CB_예상수익사용 = false;

            // 5. 스캘핑
            Form_Basic.form.CB_scalping.Checked = GenieConfig.CB_scalping = false;
            Form_Basic.form.CB_scalping_A.Checked = GenieConfig.CB_scalping_A = false;
            Form_Basic.form.CB_scalping_B.Checked = GenieConfig.CB_scalping_B = false;
            Form_Basic.form.CB_scalping_C.Checked = GenieConfig.CB_scalping_C = false;

            // 6. 시간 청산 (CB_TimeSell) -> Setting.basic (수정됨)
            Form_Basic.form.CB_TimeSell_A.Checked = GenieConfig.CB_TimeSell_A = false;
            Form_Basic.form.CB_TimeSell_B.Checked = GenieConfig.CB_TimeSell_B = false;
            Form_Basic.form.CB_TimeSell_C.Checked = GenieConfig.CB_TimeSell_C = false;
        }

    
        private static void 패널감추기_특수매매()
        {
            // 1. [데이터 세팅] 폼 생성 여부와 무관하게 메모리(GenieConfig) 값부터 무조건 0으로 초기화합니다.
            GenieConfig.CBB_In_group_A = 0;
            GenieConfig.CBB_In_group_B = 0;
            GenieConfig.CBB_In_group_C = 0;
            GenieConfig.CBB_In_group_D = 0;

            // 2. [UI 세팅] 폼이 실제로 생성되어 있을 때만 화면을 조작합니다. (Null Reference 방어막)
            if (Form_Special.form != null && !Form_Special.form.IsDisposed)
            {
                // [UI 숨기기]
                Form_Special.form.P_조건별매매그룹지정.Hide();
                Form_Special.form.LB_예약리스트.Hide();
                Form_Special.form.P_예약주문.Hide();

                // [위치 조정]
                Form_Special.form.P_매매기간주문.Location = new System.Drawing.Point(1, 131);

                // [콤보박스 화면 초기화]
                Form_Special.form.CBB_In_group_A.SelectedIndex = 0;
                Form_Special.form.CBB_In_group_B.SelectedIndex = 0;
                Form_Special.form.CBB_In_group_C.SelectedIndex = 0;
                Form_Special.form.CBB_In_group_D.SelectedIndex = 0;
            }
        }

        ///////////////////////       계좌설정
        private static void 계좌설정_(int 시작시간, int 종료시간, int 최대잔고, bool 매수제한, double 매수제한매입비, int 매수사용법, bool 추매제한, double 추매제한매입비, int 지수연동_신규, int 지수연동_추매)
        {
            // [UI 업데이트]
            Form1.form1.CB_미체결취소.Checked = false;

            Form1.form1.TB_starttime.Text = 시작시간.ToString();
            Form1.form1.TB_stoptime.Text = 종료시간.ToString();
            Form1.form1.CBB_지수연동_신규.SelectedIndex = 지수연동_신규;
            Form1.form1.CBB_지수연동_추매.SelectedIndex = 지수연동_추매;

            // [조건부 설정 업데이트]
            if (!GenieConfig.CB_기본매매변경)
            {
                // UI 반영
                Form1.form1.TB_setjango.Text = 최대잔고.ToString();
                Form1.form1.CB_계좌매입비_매수제한.Checked = 매수제한;
                Form1.form1.TB_계좌매입비_제한비중.Text = 매수제한매입비.ToString();
                Form1.form1.CBB_계좌매입비_제한선택.SelectedIndex = 매수사용법;
                Form1.form1.CB_잔고매입비_추매제한.Checked = 추매제한;
                Form1.form1.TB_잔고매입비_추매제한.Text = 추매제한매입비.ToString();

                // 설정값 저장 (Setting 클래스 사용)
                GenieConfig.TB_setjango = 최대잔고;                 // 계좌 관리 (보유종목수)

                GenieConfig.CB_계좌매입비_매수제한 = 매수제한;         // 기본 계좌 (매수제한)
                GenieConfig.TB_계좌매입비_제한비중 = 매수제한매입비;   // 기본 계좌 (비중)
                GenieConfig.CBB_계좌매입비_제한선택 = 매수사용법;      // 기본 계좌 (방법)

                GenieConfig.CB_잔고매입비_추매제한 = 추매제한;         // 기본 계좌 (추매제한)
                GenieConfig.TB_잔고매입비_추매제한 = 추매제한매입비;   // 기본 계좌 (추매비중)
            }

            // [공통 설정 업데이트]
            GenieConfig.CB_미체결취소 = false;      // 기능 설정
            GenieConfig.MT_starttime = 시작시간;         // 기본 계좌
            GenieConfig.MT_stoptime = 종료시간;          // 기본 계좌
            GenieConfig.CBB_지수연동_신규 = 지수연동_신규; // 지수 설정
            GenieConfig.CBB_지수연동_추매 = 지수연동_추매; // 지수 설정
        }

        private static void 계좌설정_코스피(double 등락률, int 등락률상하, int 등락률사용법, double 고가대비등락률, int 고가대비상하, int 고가대비사용법, double 저가대비등락률, int 저가대비상하, int 저가대비사용법)
        {
            // [UI 업데이트]
            Form1.form1.TB_p_ratio_use.Text = 등락률.ToString();
            Form1.form1.combo_p_ratio_UD.SelectedIndex = 등락률상하;
            Form1.form1.combo_p_ratio.SelectedIndex = 등락률사용법;

            Form1.form1.TB_p_go_use.Text = 고가대비등락률.ToString();
            Form1.form1.combo_p_go_UD.SelectedIndex = 고가대비상하;
            Form1.form1.combo_p_go.SelectedIndex = 고가대비사용법;

            Form1.form1.TB_p_down_use.Text = 저가대비등락률.ToString();
            Form1.form1.combo_p_down_UD.SelectedIndex = 저가대비상하;
            Form1.form1.combo_p_down.SelectedIndex = 저가대비사용법;

            // [설정값 저장] (Properties.Settings -> Setting.jisu)
            GenieConfig.TB_p_ratio_use = 등락률;
            GenieConfig.combo_p_ratio_UD = 등락률상하;
            GenieConfig.combo_p_ratio = 등락률사용법;

            GenieConfig.TB_p_go_use = 고가대비등락률;
            GenieConfig.combo_p_go_UD = 고가대비상하;
            GenieConfig.combo_p_go = 고가대비사용법;

            GenieConfig.TB_p_down_use = 저가대비등락률;
            GenieConfig.combo_p_down_UD = 저가대비상하;
            GenieConfig.combo_p_down = 저가대비사용법;
        }

        // ---------------------------------------------------------
        // 코스닥 지수 설정 (Setting.jisu 사용)
        // ---------------------------------------------------------
        private static void 계좌설정_코스닥(double 등락률, int 등락률상하, int 등락률사용법, double 고가대비등락률, int 고가대비상하, int 고가대비사용법, double 저가대비등락률, int 저가대비상하, int 저가대비사용법)
        {
            // [UI 업데이트]
            Form1.form1.TB_q_ratio_use.Text = 등락률.ToString();
            Form1.form1.combo_q_ratio_UD.SelectedIndex = 등락률상하;
            Form1.form1.combo_q_ratio.SelectedIndex = 등락률사용법;

            Form1.form1.TB_q_go_use.Text = 고가대비등락률.ToString();
            Form1.form1.combo_q_go_UD.SelectedIndex = 고가대비상하;
            Form1.form1.combo_q_go.SelectedIndex = 고가대비사용법;

            Form1.form1.TB_q_down_use.Text = 저가대비등락률.ToString();
            Form1.form1.combo_q_down_UD.SelectedIndex = 저가대비상하;
            Form1.form1.combo_q_down.SelectedIndex = 저가대비사용법;

            // [설정값 저장] (Properties.Settings -> Setting.jisu)
            GenieConfig.TB_q_ratio_use = 등락률;
            GenieConfig.combo_q_ratio_UD = 등락률상하;
            GenieConfig.combo_q_ratio = 등락률사용법;

            GenieConfig.TB_q_go_use = 고가대비등락률;
            GenieConfig.combo_q_go_UD = 고가대비상하;
            GenieConfig.combo_q_go = 고가대비사용법;

            GenieConfig.TB_q_down_use = 저가대비등락률;
            GenieConfig.combo_q_down_UD = 저가대비상하;
            GenieConfig.combo_q_down = 저가대비사용법;
        }

        // ---------------------------------------------------------
        // 미수 사용 설정 (Setting.accmgr 사용)
        // ---------------------------------------------------------
        private static void 계좌설정_미수사용(bool 사용, int 정리시간, int 사용법, int 회당주문금액, int 주문값, int 주문방법, int 반복시간)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                // [UI 업데이트]
                Form1.form1.CB_misu.Checked = 사용;
                Form1.form1.MT_misu_time.Text = 정리시간.ToString();
                Form1.form1.Combo_misu.SelectedIndex = 사용법;
                Form1.form1.TB_misu_ratio.Text = 회당주문금액.ToString();
                Form1.form1.TB_misu_value.Text = 주문값.ToString();
                Form1.form1.Combo_misu_jumnun.SelectedIndex = 주문방법;
                Form1.form1.TB_misu_repeat_time.Text = 반복시간.ToString();

                // [설정값 저장] (Properties.Settings -> Setting.accmgr)
                GenieConfig.CB_misu = 사용;
                GenieConfig.MT_misu_time = 정리시간;
                GenieConfig.Combo_misu = 사용법;
                GenieConfig.TB_misu_ratio = 회당주문금액;
                GenieConfig.TB_misu_value = 주문값;
                GenieConfig.Combo_misu_jumnun = 주문방법;
                GenieConfig.TB_misu_repeat_time = 반복시간;
            }
        }

        // 호출 예시: 계좌설정_이평선("CB_지수이평_반복_A", true, true, false, false...);

        private static void 계좌설정_이평선(string 대상그룹명,
        bool 코스피_사용여부,
        bool use_kospi_min_03, bool use_kospi_min_05, bool use_kospi_min_10, bool use_kospi_min_20, bool use_kospi_min_30, bool use_kospi_min_60,
        bool use_kospi_day_03, bool use_kospi_day_05, bool use_kospi_day_10, bool use_kospi_day_20, bool use_kospi_day_40, bool use_kospi_day_60,
        bool UD_kospi_min_03, bool UD_kospi_min_05, bool UD_kospi_min_10, bool UD_kospi_min_20, bool UD_kospi_min_30, bool UD_kospi_min_60,
        bool UD_kospi_day_03, bool UD_kospi_day_05, bool UD_kospi_day_10, bool UD_kospi_day_20, bool UD_kospi_day_40, bool UD_kospi_day_60,
        bool 코스닥_사용여부,
        bool use_kosdaq_min_03, bool use_kosdaq_min_05, bool use_kosdaq_min_10, bool use_kosdaq_min_20, bool use_kosdaq_min_30, bool use_kosdaq_min_60,
        bool use_kosdaq_day_03, bool use_kosdaq_day_05, bool use_kosdaq_day_10, bool use_kosdaq_day_20, bool use_kosdaq_day_40, bool use_kosdaq_day_60,
        bool UD_kosdaq_min_03, bool UD_kosdaq_min_05, bool UD_kosdaq_min_10, bool UD_kosdaq_min_20, bool UD_kosdaq_min_30, bool UD_kosdaq_min_60,
        bool UD_kosdaq_day_03, bool UD_kosdaq_day_05, bool UD_kosdaq_day_10, bool UD_kosdaq_day_20, bool UD_kosdaq_day_40, bool UD_kosdaq_day_60)
        {
            if (string.IsNullOrEmpty(대상그룹명)) return;

            // ==========================================
            // 1. [UI 설정 저장] -> GenieConfig.그룹별_지수설정
            // ==========================================
            Form_Jisu.지수설정_세트 강제설정 = new Form_Jisu.지수설정_세트
            {
                // 신규나 추매 중 하나라도 true면 해당 시장의 지수이평을 사용하는 것으로 간주합니다.
                지수이평_사용_kospi = 코스피_사용여부,
                지수이평_사용_kosdaq = 코스닥_사용여부,

                Use_kospi_min_03 = use_kospi_min_03,
                Use_kospi_min_05 = use_kospi_min_05,
                Use_kospi_min_10 = use_kospi_min_10,
                Use_kospi_min_20 = use_kospi_min_20,
                Use_kospi_min_30 = use_kospi_min_30,
                Use_kospi_min_60 = use_kospi_min_60,

                Use_kospi_day_03 = use_kospi_day_03,
                Use_kospi_day_05 = use_kospi_day_05,
                Use_kospi_day_10 = use_kospi_day_10,
                Use_kospi_day_20 = use_kospi_day_20,
                Use_kospi_day_40 = use_kospi_day_40,
                Use_kospi_day_60 = use_kospi_day_60,

                UD_kospi_min_03 = UD_kospi_min_03,
                UD_kospi_min_05 = UD_kospi_min_05,
                UD_kospi_min_10 = UD_kospi_min_10,
                UD_kospi_min_20 = UD_kospi_min_20,
                UD_kospi_min_30 = UD_kospi_min_30,
                UD_kospi_min_60 = UD_kospi_min_60,

                UD_kospi_day_03 = UD_kospi_day_03,
                UD_kospi_day_05 = UD_kospi_day_05,
                UD_kospi_day_10 = UD_kospi_day_10,
                UD_kospi_day_20 = UD_kospi_day_20,
                UD_kospi_day_40 = UD_kospi_day_40,
                UD_kospi_day_60 = UD_kospi_day_60,

                Use_kosdaq_min_03 = use_kosdaq_min_03,
                Use_kosdaq_min_05 = use_kosdaq_min_05,
                Use_kosdaq_min_10 = use_kosdaq_min_10,
                Use_kosdaq_min_20 = use_kosdaq_min_20,
                Use_kosdaq_min_30 = use_kosdaq_min_30,
                Use_kosdaq_min_60 = use_kosdaq_min_60,

                Use_kosdaq_day_03 = use_kosdaq_day_03,
                Use_kosdaq_day_05 = use_kosdaq_day_05,
                Use_kosdaq_day_10 = use_kosdaq_day_10,
                Use_kosdaq_day_20 = use_kosdaq_day_20,
                Use_kosdaq_day_40 = use_kosdaq_day_40,
                Use_kosdaq_day_60 = use_kosdaq_day_60,

                UD_kosdaq_min_03 = UD_kosdaq_min_03,
                UD_kosdaq_min_05 = UD_kosdaq_min_05,
                UD_kosdaq_min_10 = UD_kosdaq_min_10,
                UD_kosdaq_min_20 = UD_kosdaq_min_20,
                UD_kosdaq_min_30 = UD_kosdaq_min_30,
                UD_kosdaq_min_60 = UD_kosdaq_min_60,

                UD_kosdaq_day_03 = UD_kosdaq_day_03,
                UD_kosdaq_day_05 = UD_kosdaq_day_05,
                UD_kosdaq_day_10 = UD_kosdaq_day_10,
                UD_kosdaq_day_20 = UD_kosdaq_day_20,
                UD_kosdaq_day_40 = UD_kosdaq_day_40,
                UD_kosdaq_day_60 = UD_kosdaq_day_60
            };

            // UI 딕셔너리에 덮어쓰기
            GenieConfig.그룹별_지수설정[대상그룹명] = 강제설정;


            // ==========================================
            // 2. [실제 적용 변수 업데이트] -> Form1.그룹별_지수사용값
            // ==========================================
            if (!Form1.그룹별_지수사용값.ContainsKey(대상그룹명))
            {
                Form1.그룹별_지수사용값[대상그룹명] = new 지수이평사용값[2] { new 지수이평사용값(), new 지수이평사용값() };
            }

            지수이평사용값[] 타겟실행값 = Form1.그룹별_지수사용값[대상그룹명];

            // [코스피 Index 0]
            타겟실행값[0].사용유무 = 코스피_사용여부;

            타겟실행값[0].Use_min_03 = use_kospi_min_03; 타겟실행값[0].Use_min_05 = use_kospi_min_05;
            타겟실행값[0].Use_min_10 = use_kospi_min_10; 타겟실행값[0].Use_min_20 = use_kospi_min_20;
            타겟실행값[0].Use_min_30 = use_kospi_min_30; 타겟실행값[0].Use_min_60 = use_kospi_min_60;

            타겟실행값[0].Use_day_03 = use_kospi_day_03; 타겟실행값[0].Use_day_05 = use_kospi_day_05;
            타겟실행값[0].Use_day_10 = use_kospi_day_10; 타겟실행값[0].Use_day_20 = use_kospi_day_20;
            타겟실행값[0].Use_day_40 = use_kospi_day_40; 타겟실행값[0].Use_day_60 = use_kospi_day_60;

            타겟실행값[0].추세사용값_min_03 = UD_kospi_min_03; 타겟실행값[0].추세사용값_min_05 = UD_kospi_min_05;
            타겟실행값[0].추세사용값_min_10 = UD_kospi_min_10; 타겟실행값[0].추세사용값_min_20 = UD_kospi_min_20;
            타겟실행값[0].추세사용값_min_30 = UD_kospi_min_30; 타겟실행값[0].추세사용값_min_60 = UD_kospi_min_60;

            타겟실행값[0].추세사용값_day_03 = UD_kospi_day_03; 타겟실행값[0].추세사용값_day_05 = UD_kospi_day_05;
            타겟실행값[0].추세사용값_day_10 = UD_kospi_day_10; 타겟실행값[0].추세사용값_day_20 = UD_kospi_day_20;
            타겟실행값[0].추세사용값_day_40 = UD_kospi_day_40; 타겟실행값[0].추세사용값_day_60 = UD_kospi_day_60;

            // [코스닥 Index 1]
            타겟실행값[1].사용유무 = 코스닥_사용여부;

            타겟실행값[1].Use_min_03 = use_kosdaq_min_03; 타겟실행값[1].Use_min_05 = use_kosdaq_min_05;
            타겟실행값[1].Use_min_10 = use_kosdaq_min_10; 타겟실행값[1].Use_min_20 = use_kosdaq_min_20;
            타겟실행값[1].Use_min_30 = use_kosdaq_min_30; 타겟실행값[1].Use_min_60 = use_kosdaq_min_60;

            타겟실행값[1].Use_day_03 = use_kosdaq_day_03; 타겟실행값[1].Use_day_05 = use_kosdaq_day_05;
            타겟실행값[1].Use_day_10 = use_kosdaq_day_10; 타겟실행값[1].Use_day_20 = use_kosdaq_day_20;
            타겟실행값[1].Use_day_40 = use_kosdaq_day_40; 타겟실행값[1].Use_day_60 = use_kosdaq_day_60;

            타겟실행값[1].추세사용값_min_03 = UD_kosdaq_min_03; 타겟실행값[1].추세사용값_min_05 = UD_kosdaq_min_05;
            타겟실행값[1].추세사용값_min_10 = UD_kosdaq_min_10; 타겟실행값[1].추세사용값_min_20 = UD_kosdaq_min_20;
            타겟실행값[1].추세사용값_min_30 = UD_kosdaq_min_30; 타겟실행값[1].추세사용값_min_60 = UD_kosdaq_min_60;

            타겟실행값[1].추세사용값_day_03 = UD_kosdaq_day_03; 타겟실행값[1].추세사용값_day_05 = UD_kosdaq_day_05;
            타겟실행값[1].추세사용값_day_10 = UD_kosdaq_day_10; 타겟실행값[1].추세사용값_day_20 = UD_kosdaq_day_20;
            타겟실행값[1].추세사용값_day_40 = UD_kosdaq_day_40; 타겟실행값[1].추세사용값_day_60 = UD_kosdaq_day_60;
        }

        // ---------------------------------------------------------
        // 기본매매 - 신규 횟수 제한 (Setting.basic 사용)
        // ---------------------------------------------------------
        private static void 기본매매_신규횟수제한(bool CB_신규횟수제한, int TB_신규횟수제한)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                // [수정] Properties.Settings -> Setting.basic
                GenieConfig.CB_신규횟수제한 = CB_신규횟수제한;
                GenieConfig.TB_신규횟수제한 = TB_신규횟수제한;
            }
        }

        // ---------------------------------------------------------
        // 신규 매수 A 설정 (Setting.basic 사용)
        // ---------------------------------------------------------
        private static void 기본매매_new_A(bool 재진입, int 유지, int 취소시간, int 취소후, int 반복횟수, bool 사용, int 검색식사용법, string 검색식, int 시작시간, int 종료시간, double 비중, int 비중방법, int 주문값, int 주문방법)
        {
            신규_A = new string[] { 사용.ToString(), 검색식사용법.ToString(), 검색식 };

            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.CB_new_recatch_A = 재진입;
                GenieConfig.MTB_new_delay_A = 유지;
                GenieConfig.MTB_new_canceltime_A = 취소시간;
                GenieConfig.combo_new_cancel_buy_A = 취소후;
                GenieConfig.MTB_new_repeat_A = 반복횟수;
                GenieConfig.CB_new_A = 사용;
                GenieConfig.combo_new_or_A = 검색식사용법;

                // (Form1 관련 로직은 유지)
                if (Form1.위치별검색식리스트.ContainsKey("신규_A"))
                    Form1.위치별검색식리스트["신규_A"].이름 = 검색식_유무확인(사용, "신규매수_A", 검색식);

                GenieConfig.MT_new_start_A = 시작시간;
                GenieConfig.MT_new_end_A = 종료시간;
                GenieConfig.combo_new_choice_A = 비중방법;
                GenieConfig.TB_new_value_A = 주문값;
                GenieConfig.combo_new_jumun_A = 주문방법;
                GenieConfig.MT_new_ratio_A = 비중;
            }
        }

        // ---------------------------------------------------------
        // 신규 매수 B 설정 (Setting.basic 사용)
        // ---------------------------------------------------------
        private static void 기본매매_new_B(bool 재진입, int 유지, int 취소시간, int 취소후, int 반복횟수, bool 사용, int 검색식사용법, string 검색식, int 시작시간, int 종료시간, double 비중, int 비중방법, int 주문값, int 주문방법)
        {
            신규_B = new string[] { 사용.ToString(), 검색식사용법.ToString(), 검색식 };

            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.CB_new_recatch_B = 재진입;
                GenieConfig.MTB_new_delay_B = 유지;
                GenieConfig.MTB_new_canceltime_B = 취소시간;
                GenieConfig.combo_new_cancel_buy_B = 취소후;
                GenieConfig.MTB_new_repeat_B = 반복횟수;
                GenieConfig.CB_new_B = 사용;
                GenieConfig.combo_new_or_B = 검색식사용법;

                if (Form1.위치별검색식리스트.ContainsKey("신규_B"))
                    Form1.위치별검색식리스트["신규_B"].이름 = 검색식_유무확인(사용, "신규매수_B", 검색식);

                GenieConfig.MT_new_start_B = 시작시간;
                GenieConfig.MT_new_end_B = 종료시간;
                GenieConfig.combo_new_choice_B = 비중방법;
                GenieConfig.TB_new_value_B = 주문값;
                GenieConfig.combo_new_jumun_B = 주문방법;
                GenieConfig.MT_new_ratio_B = 비중;
            }
        }

        // ---------------------------------------------------------
        // 신규 매수 C 설정 (Setting.basic 사용)
        // ---------------------------------------------------------
        private static void 기본매매_new_C(bool 재진입, int 유지, int 취소시간, int 취소후, int 반복횟수, bool 사용, int 검색식사용법, string 검색식, int 시작시간, int 종료시간, double 비중, int 비중방법, int 주문값, int 주문방법)
        {
            신규_C = new string[] { 사용.ToString(), 검색식사용법.ToString(), 검색식 };

            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.CB_new_recatch_C = 재진입;
                GenieConfig.MTB_new_delay_C = 유지;
                GenieConfig.MTB_new_canceltime_C = 취소시간;
                GenieConfig.combo_new_cancel_buy_C = 취소후;
                GenieConfig.MTB_new_repeat_C = 반복횟수;
                GenieConfig.CB_new_C = 사용;
                GenieConfig.combo_new_or_C = 검색식사용법;

                if (Form1.위치별검색식리스트.ContainsKey("신규_C"))
                    Form1.위치별검색식리스트["신규_C"].이름 = 검색식_유무확인(사용, "신규매수_C", 검색식);

                GenieConfig.MT_new_start_C = 시작시간;
                GenieConfig.MT_new_end_C = 종료시간;
                GenieConfig.combo_new_choice_C = 비중방법;
                GenieConfig.TB_new_value_C = 주문값;
                GenieConfig.combo_new_jumun_C = 주문방법;
                GenieConfig.MT_new_ratio_C = 비중;
            }
        }
        // ---------------------------------------------------------
        // 기본매매 - 신규 매수 조건 (Setting.basic 사용)
        // ---------------------------------------------------------
        private static void 기본매매_신규매수조건(int 신규주가이상, int 신규주가이하, double 신규등락률이상, double 신규등락률이하, int 추가매수딜레이, bool 재매수, int 재매수_지연시간)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.TB_신규주가이상 = 신규주가이상;
                GenieConfig.TB_신규주가이하 = 신규주가이하;
                GenieConfig.TB_신규등락률이상 = 신규등락률이상;
                GenieConfig.TB_신규등락률이하 = 신규등락률이하;
                GenieConfig.MTB_추가매수딜레이 = 추가매수딜레이;
                GenieConfig.CB_new_rebuy = 재매수;
                GenieConfig.MTB_new_rebuytime = 재매수_지연시간;
            }
        }

        // ---------------------------------------------------------
        // 기본매매 - 신규 매수 세부 조건 (Setting.basic 사용)
        // ---------------------------------------------------------
        private static void 기본매매_신규매수_세부조건(int 잔고개수_신규A, int 잔고개수_신규B, int 잔고개수_신규C, int 재진입제한_A, int 재진입제한_B, int 재진입제한_C, bool 익절재매수A, bool 익절재매수B, bool 익절재매수C)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.TB_잔고개수_신규A = 잔고개수_신규A;
                GenieConfig.TB_잔고개수_신규B = 잔고개수_신규B;
                GenieConfig.TB_잔고개수_신규C = 잔고개수_신규C;
                GenieConfig.TB_Limit_New_A = 재진입제한_A;
                GenieConfig.TB_Limit_New_B = 재진입제한_B;
                GenieConfig.TB_Limit_New_C = 재진입제한_C;
                GenieConfig.CB_익절재매수A = 익절재매수A;
                GenieConfig.CB_익절재매수B = 익절재매수B;
                GenieConfig.CB_익절재매수C = 익절재매수C;
            }
        }

        // ---------------------------------------------------------
        // 기본매매 - 트레일링 스탑 조건 (Setting.basic 사용)
        // ---------------------------------------------------------
        private static void 기본매매_트레일링스탑_조건(bool 손실제한, bool 취소후, bool 기준금, int canceltime, int cancel_sell, int repeat)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.CB_TS_손실제한 = 손실제한;
                GenieConfig.CB_TS_취소후 = 취소후;
                GenieConfig.CB_TS_기준금 = 기준금;
                GenieConfig.MTB_TS_canceltime = canceltime;
                GenieConfig.CBB_TS_cancel_sell = cancel_sell;
                GenieConfig.MTB_TS_repeat = repeat;
            }
        }
        // ---------------------------------------------------------
        // 트레일링 스탑 A~I 설정 (Setting.basic 사용)
        // ---------------------------------------------------------

        private static void 기본매매_트레일링스탑_A(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.CB_TS_A = CB_TS;
                GenieConfig.TB_TS_upper_A = TB_TS_upper;
                GenieConfig.CBB_TS_upper_A = CBB_TS_upper;
                GenieConfig.TB_TS_down_A = TB_TS_down;
                GenieConfig.TB_TS_ratio_A = TB_TS_ratio;
                GenieConfig.CBB_TS_ratio_A = CBB_TS_ratio;
                GenieConfig.TB_TS_Jumun_A = TB_TS_Jumun;
                GenieConfig.CBB_TS_Jumun_A = CBB_TS_Jumun;
            }
        }

        private static void 기본매매_트레일링스탑_B(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.CB_TS_B = CB_TS;
                GenieConfig.TB_TS_upper_B = TB_TS_upper;
                GenieConfig.CBB_TS_upper_B = CBB_TS_upper;
                GenieConfig.TB_TS_down_B = TB_TS_down;
                GenieConfig.TB_TS_ratio_B = TB_TS_ratio;
                GenieConfig.CBB_TS_ratio_B = CBB_TS_ratio;
                GenieConfig.TB_TS_Jumun_B = TB_TS_Jumun;
                GenieConfig.CBB_TS_Jumun_B = CBB_TS_Jumun;
            }
        }

        private static void 기본매매_트레일링스탑_C(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.CB_TS_C = CB_TS;
                GenieConfig.TB_TS_upper_C = TB_TS_upper;
                GenieConfig.CBB_TS_upper_C = CBB_TS_upper;
                GenieConfig.TB_TS_down_C = TB_TS_down;
                GenieConfig.TB_TS_ratio_C = TB_TS_ratio;
                GenieConfig.CBB_TS_ratio_C = CBB_TS_ratio;
                GenieConfig.TB_TS_Jumun_C = TB_TS_Jumun;
                GenieConfig.CBB_TS_Jumun_C = CBB_TS_Jumun;
            }
        }

        private static void 기본매매_트레일링스탑_D(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.CB_TS_D = CB_TS;
                GenieConfig.TB_TS_upper_D = TB_TS_upper;
                GenieConfig.CBB_TS_upper_D = CBB_TS_upper;
                GenieConfig.TB_TS_down_D = TB_TS_down;
                GenieConfig.TB_TS_ratio_D = TB_TS_ratio;
                GenieConfig.CBB_TS_ratio_D = CBB_TS_ratio;
                GenieConfig.TB_TS_Jumun_D = TB_TS_Jumun;
                GenieConfig.CBB_TS_Jumun_D = CBB_TS_Jumun;
            }
        }

        private static void 기본매매_트레일링스탑_E(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.CB_TS_E = CB_TS;
                GenieConfig.TB_TS_upper_E = TB_TS_upper;
                GenieConfig.CBB_TS_upper_E = CBB_TS_upper;
                GenieConfig.TB_TS_down_E = TB_TS_down;
                GenieConfig.TB_TS_ratio_E = TB_TS_ratio;
                GenieConfig.CBB_TS_ratio_E = CBB_TS_ratio;
                GenieConfig.TB_TS_Jumun_E = TB_TS_Jumun;
                GenieConfig.CBB_TS_Jumun_E = CBB_TS_Jumun;
            }
        }

        private static void 기본매매_트레일링스탑_F(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.CB_TS_F = CB_TS;
                GenieConfig.TB_TS_upper_F = TB_TS_upper;
                GenieConfig.CBB_TS_upper_F = CBB_TS_upper;
                GenieConfig.TB_TS_down_F = TB_TS_down;
                GenieConfig.TB_TS_ratio_F = TB_TS_ratio;
                GenieConfig.CBB_TS_ratio_F = CBB_TS_ratio;
                GenieConfig.TB_TS_Jumun_F = TB_TS_Jumun;
                GenieConfig.CBB_TS_Jumun_F = CBB_TS_Jumun;
            }
        }

        private static void 기본매매_트레일링스탑_G(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.CB_TS_G = CB_TS;
                GenieConfig.TB_TS_upper_G = TB_TS_upper;
                GenieConfig.CBB_TS_upper_G = CBB_TS_upper;
                GenieConfig.TB_TS_down_G = TB_TS_down;
                GenieConfig.TB_TS_ratio_G = TB_TS_ratio;
                GenieConfig.CBB_TS_ratio_G = CBB_TS_ratio;
                GenieConfig.TB_TS_Jumun_G = TB_TS_Jumun;
                GenieConfig.CBB_TS_Jumun_G = CBB_TS_Jumun;
            }
        }

        private static void 기본매매_트레일링스탑_H(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.CB_TS_H = CB_TS;
                GenieConfig.TB_TS_upper_H = TB_TS_upper;
                GenieConfig.CBB_TS_upper_H = CBB_TS_upper;
                GenieConfig.TB_TS_down_H = TB_TS_down;
                GenieConfig.TB_TS_ratio_H = TB_TS_ratio;
                GenieConfig.CBB_TS_ratio_H = CBB_TS_ratio;
                GenieConfig.TB_TS_Jumun_H = TB_TS_Jumun;
                GenieConfig.CBB_TS_Jumun_H = CBB_TS_Jumun;
            }
        }

        private static void 기본매매_트레일링스탑_I(bool CB_TS, double TB_TS_upper, int CBB_TS_upper, double TB_TS_down, double TB_TS_ratio, int CBB_TS_ratio, double TB_TS_Jumun, int CBB_TS_Jumun)
        {
            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.CB_TS_I = CB_TS;
                GenieConfig.TB_TS_upper_I = TB_TS_upper;
                GenieConfig.CBB_TS_upper_I = CBB_TS_upper;
                GenieConfig.TB_TS_down_I = TB_TS_down;
                GenieConfig.TB_TS_ratio_I = TB_TS_ratio;
                GenieConfig.CBB_TS_ratio_I = CBB_TS_ratio;
                GenieConfig.TB_TS_Jumun_I = TB_TS_Jumun;
                GenieConfig.CBB_TS_Jumun_I = CBB_TS_Jumun;
            }
        }
        // ---------------------------------------------------------
        // 반복매매 - 기준금 및 추매 조건 (Setting.repeat 사용)
        // ---------------------------------------------------------
        private static void 반복기준금_추매조건(bool 기준금, int 추매주가이상, int 추매주가이하, double 추매등락률이상, double 추매등락률이하)
        {
            // [수정] Properties.Settings -> Setting.repeat
            GenieConfig.CB_Repeat_기준금 = 기준금;
            GenieConfig.TB_반복_추매주가이상 = 추매주가이상;
            GenieConfig.TB_반복_추매주가이하 = 추매주가이하;
            GenieConfig.TB_반복_추매등락률이상 = 추매등락률이상;
            GenieConfig.TB_반복_추매등락률이하 = 추매등락률이하;
        }
        // ---------------------------------------------------------
        // 반복 매매 A~N 설정 (Setting.repeat 사용)
        // ---------------------------------------------------------

        private static void 반복매매_A(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_A = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_A = 사용;
            GenieConfig.MT_repeat_time_start_A = 시작시간;
            GenieConfig.MT_repeat_time_end_A = 종료시간;
            GenieConfig.CB_repeat_kind_A = 매매종류;
            GenieConfig.combo_repeat_use_condition_A = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_A"))
                Form1.위치별검색식리스트["반복_A"].이름 = 검색식_유무확인(사용, "반복매매_A", 검색식);

            GenieConfig.MTB_repeat_delay_A = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_A = 매입금;
            GenieConfig.TB_repeat_누적거래량_A = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_A = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_A = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_A = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_A = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_A = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_A = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_A = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_A = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_A = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_A = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_A = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_A = 수익범위1;
            GenieConfig.CB_repeat_choice_A = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_A = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_A = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_A = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_A = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_A = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_A = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_A = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_A = 반복시간;
            GenieConfig.TB_repeat_value_A = 주문가격;
            GenieConfig.combo_repeat_jumun_A = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_A = 취소시간;
            GenieConfig.combo_repeat_Cancel_A = 취n주문;
            GenieConfig.MTB_repeat_repeat_A = 재주문;
        }

        private static void 반복매매_B(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_B = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_B = 사용;
            GenieConfig.MT_repeat_time_start_B = 시작시간;
            GenieConfig.MT_repeat_time_end_B = 종료시간;
            GenieConfig.CB_repeat_kind_B = 매매종류;
            GenieConfig.combo_repeat_use_condition_B = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_B"))
                Form1.위치별검색식리스트["반복_B"].이름 = 검색식_유무확인(사용, "반복매매_B", 검색식);

            GenieConfig.MTB_repeat_delay_B = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_B = 매입금;
            GenieConfig.TB_repeat_누적거래량_B = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_B = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_B = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_B = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_B = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_B = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_B = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_B = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_B = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_B = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_B = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_B = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_B = 수익범위1;
            GenieConfig.CB_repeat_choice_B = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_B = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_B = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_B = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_B = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_B = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_B = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_B = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_B = 반복시간;
            GenieConfig.TB_repeat_value_B = 주문가격;
            GenieConfig.combo_repeat_jumun_B = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_B = 취소시간;
            GenieConfig.combo_repeat_Cancel_B = 취n주문;
            GenieConfig.MTB_repeat_repeat_B = 재주문;
        }

        private static void 반복매매_C(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_C = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_C = 사용;
            GenieConfig.MT_repeat_time_start_C = 시작시간;
            GenieConfig.MT_repeat_time_end_C = 종료시간;
            GenieConfig.CB_repeat_kind_C = 매매종류;
            GenieConfig.combo_repeat_use_condition_C = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_C"))
                Form1.위치별검색식리스트["반복_C"].이름 = 검색식_유무확인(사용, "반복매매_C", 검색식);

            GenieConfig.MTB_repeat_delay_C = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_C = 매입금;
            GenieConfig.TB_repeat_누적거래량_C = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_C = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_C = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_C = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_C = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_C = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_C = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_C = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_C = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_C = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_C = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_C = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_C = 수익범위1;
            GenieConfig.CB_repeat_choice_C = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_C = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_C = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_C = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_C = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_C = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_C = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_C = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_C = 반복시간;
            GenieConfig.TB_repeat_value_C = 주문가격;
            GenieConfig.combo_repeat_jumun_C = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_C = 취소시간;
            GenieConfig.combo_repeat_Cancel_C = 취n주문;
            GenieConfig.MTB_repeat_repeat_C = 재주문;
        }

        private static void 반복매매_D(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_D = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_D = 사용;
            GenieConfig.MT_repeat_time_start_D = 시작시간;
            GenieConfig.MT_repeat_time_end_D = 종료시간;
            GenieConfig.CB_repeat_kind_D = 매매종류;
            GenieConfig.combo_repeat_use_condition_D = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_D"))
                Form1.위치별검색식리스트["반복_D"].이름 = 검색식_유무확인(사용, "반복매매_D", 검색식);

            GenieConfig.MTB_repeat_delay_D = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_D = 매입금;
            GenieConfig.TB_repeat_누적거래량_D = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_D = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_D = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_D = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_D = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_D = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_D = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_D = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_D = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_D = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_D = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_D = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_D = 수익범위1;
            GenieConfig.CB_repeat_choice_D = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_D = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_D = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_D = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_D = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_D = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_D = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_D = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_D = 반복시간;
            GenieConfig.TB_repeat_value_D = 주문가격;
            GenieConfig.combo_repeat_jumun_D = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_D = 취소시간;
            GenieConfig.combo_repeat_Cancel_D = 취n주문;
            GenieConfig.MTB_repeat_repeat_D = 재주문;
        }

        private static void 반복매매_E(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_E = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_E = 사용;
            GenieConfig.MT_repeat_time_start_E = 시작시간;
            GenieConfig.MT_repeat_time_end_E = 종료시간;
            GenieConfig.CB_repeat_kind_E = 매매종류;
            GenieConfig.combo_repeat_use_condition_E = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_E"))
                Form1.위치별검색식리스트["반복_E"].이름 = 검색식_유무확인(사용, "반복매매_E", 검색식);

            GenieConfig.MTB_repeat_delay_E = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_E = 매입금;
            GenieConfig.TB_repeat_누적거래량_E = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_E = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_E = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_E = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_E = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_E = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_E = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_E = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_E = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_E = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_E = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_E = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_E = 수익범위1;
            GenieConfig.CB_repeat_choice_E = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_E = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_E = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_E = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_E = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_E = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_E = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_E = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_E = 반복시간;
            GenieConfig.TB_repeat_value_E = 주문가격;
            GenieConfig.combo_repeat_jumun_E = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_E = 취소시간;
            GenieConfig.combo_repeat_Cancel_E = 취n주문;
            GenieConfig.MTB_repeat_repeat_E = 재주문;
        }

        private static void 반복매매_F(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_F = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_F = 사용;
            GenieConfig.MT_repeat_time_start_F = 시작시간;
            GenieConfig.MT_repeat_time_end_F = 종료시간;
            GenieConfig.CB_repeat_kind_F = 매매종류;
            GenieConfig.combo_repeat_use_condition_F = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_F"))
                Form1.위치별검색식리스트["반복_F"].이름 = 검색식_유무확인(사용, "반복매매_F", 검색식);

            GenieConfig.MTB_repeat_delay_F = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_F = 매입금;
            GenieConfig.TB_repeat_누적거래량_F = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_F = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_F = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_F = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_F = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_F = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_F = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_F = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_F = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_F = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_F = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_F = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_F = 수익범위1;
            GenieConfig.CB_repeat_choice_F = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_F = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_F = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_F = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_F = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_F = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_F = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_F = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_F = 반복시간;
            GenieConfig.TB_repeat_value_F = 주문가격;
            GenieConfig.combo_repeat_jumun_F = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_F = 취소시간;
            GenieConfig.combo_repeat_Cancel_F = 취n주문;
            GenieConfig.MTB_repeat_repeat_F = 재주문;
        }

        private static void 반복매매_G(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_G = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_G = 사용;
            GenieConfig.MT_repeat_time_start_G = 시작시간;
            GenieConfig.MT_repeat_time_end_G = 종료시간;
            GenieConfig.CB_repeat_kind_G = 매매종류;
            GenieConfig.combo_repeat_use_condition_G = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_G"))
                Form1.위치별검색식리스트["반복_G"].이름 = 검색식_유무확인(사용, "반복매매_G", 검색식);

            GenieConfig.MTB_repeat_delay_G = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_G = 매입금;
            GenieConfig.TB_repeat_누적거래량_G = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_G = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_G = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_G = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_G = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_G = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_G = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_G = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_G = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_G = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_G = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_G = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_G = 수익범위1;
            GenieConfig.CB_repeat_choice_G = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_G = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_G = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_G = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_G = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_G = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_G = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_G = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_G = 반복시간;
            GenieConfig.TB_repeat_value_G = 주문가격;
            GenieConfig.combo_repeat_jumun_G = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_G = 취소시간;
            GenieConfig.combo_repeat_Cancel_G = 취n주문;
            GenieConfig.MTB_repeat_repeat_G = 재주문;
        }

        private static void 반복매매_H(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_H = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_H = 사용;
            GenieConfig.MT_repeat_time_start_H = 시작시간;
            GenieConfig.MT_repeat_time_end_H = 종료시간;
            GenieConfig.CB_repeat_kind_H = 매매종류;
            GenieConfig.combo_repeat_use_condition_H = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_H"))
                Form1.위치별검색식리스트["반복_H"].이름 = 검색식_유무확인(사용, "반복매매_H", 검색식);

            GenieConfig.MTB_repeat_delay_H = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_H = 매입금;
            GenieConfig.TB_repeat_누적거래량_H = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_H = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_H = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_H = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_H = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_H = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_H = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_H = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_H = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_H = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_H = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_H = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_H = 수익범위1;
            GenieConfig.CB_repeat_choice_H = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_H = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_H = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_H = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_H = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_H = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_H = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_H = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_H = 반복시간;
            GenieConfig.TB_repeat_value_H = 주문가격;
            GenieConfig.combo_repeat_jumun_H = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_H = 취소시간;
            GenieConfig.combo_repeat_Cancel_H = 취n주문;
            GenieConfig.MTB_repeat_repeat_H = 재주문;
        }

        private static void 반복매매_I(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_I = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_I = 사용;
            GenieConfig.MT_repeat_time_start_I = 시작시간;
            GenieConfig.MT_repeat_time_end_I = 종료시간;
            GenieConfig.CB_repeat_kind_I = 매매종류;
            GenieConfig.combo_repeat_use_condition_I = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_I"))
                Form1.위치별검색식리스트["반복_I"].이름 = 검색식_유무확인(사용, "반복매매_I", 검색식);

            GenieConfig.MTB_repeat_delay_I = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_I = 매입금;
            GenieConfig.TB_repeat_누적거래량_I = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_I = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_I = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_I = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_I = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_I = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_I = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_I = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_I = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_I = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_I = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_I = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_I = 수익범위1;
            GenieConfig.CB_repeat_choice_I = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_I = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_I = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_I = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_I = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_I = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_I = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_I = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_I = 반복시간;
            GenieConfig.TB_repeat_value_I = 주문가격;
            GenieConfig.combo_repeat_jumun_I = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_I = 취소시간;
            GenieConfig.combo_repeat_Cancel_I = 취n주문;
            GenieConfig.MTB_repeat_repeat_I = 재주문;
        }

        private static void 반복매매_J(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_J = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_J = 사용;
            GenieConfig.MT_repeat_time_start_J = 시작시간;
            GenieConfig.MT_repeat_time_end_J = 종료시간;
            GenieConfig.CB_repeat_kind_J = 매매종류;
            GenieConfig.combo_repeat_use_condition_J = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_J"))
                Form1.위치별검색식리스트["반복_J"].이름 = 검색식_유무확인(사용, "반복매매_J", 검색식);

            GenieConfig.MTB_repeat_delay_J = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_J = 매입금;
            GenieConfig.TB_repeat_누적거래량_J = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_J = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_J = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_J = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_J = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_J = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_J = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_J = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_J = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_J = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_J = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_J = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_J = 수익범위1;
            GenieConfig.CB_repeat_choice_J = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_J = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_J = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_J = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_J = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_J = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_J = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_J = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_J = 반복시간;
            GenieConfig.TB_repeat_value_J = 주문가격;
            GenieConfig.combo_repeat_jumun_J = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_J = 취소시간;
            GenieConfig.combo_repeat_Cancel_J = 취n주문;
            GenieConfig.MTB_repeat_repeat_J = 재주문;
        }

        private static void 반복매매_K(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_K = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_K = 사용;
            GenieConfig.MT_repeat_time_start_K = 시작시간;
            GenieConfig.MT_repeat_time_end_K = 종료시간;
            GenieConfig.CB_repeat_kind_K = 매매종류;
            GenieConfig.combo_repeat_use_condition_K = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_K"))
                Form1.위치별검색식리스트["반복_K"].이름 = 검색식_유무확인(사용, "반복매매_K", 검색식);

            GenieConfig.MTB_repeat_delay_K = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_K = 매입금;
            GenieConfig.TB_repeat_누적거래량_K = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_K = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_K = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_K = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_K = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_K = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_K = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_K = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_K = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_K = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_K = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_K = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_K = 수익범위1;
            GenieConfig.CB_repeat_choice_K = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_K = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_K = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_K = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_K = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_K = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_K = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_K = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_K = 반복시간;
            GenieConfig.TB_repeat_value_K = 주문가격;
            GenieConfig.combo_repeat_jumun_K = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_K = 취소시간;
            GenieConfig.combo_repeat_Cancel_K = 취n주문;
            GenieConfig.MTB_repeat_repeat_K = 재주문;
        }

        private static void 반복매매_L(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_L = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_L = 사용;
            GenieConfig.MT_repeat_time_start_L = 시작시간;
            GenieConfig.MT_repeat_time_end_L = 종료시간;
            GenieConfig.CB_repeat_kind_L = 매매종류;
            GenieConfig.combo_repeat_use_condition_L = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_L"))
                Form1.위치별검색식리스트["반복_L"].이름 = 검색식_유무확인(사용, "반복매매_L", 검색식);

            GenieConfig.MTB_repeat_delay_L = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_L = 매입금;
            GenieConfig.TB_repeat_누적거래량_L = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_L = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_L = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_L = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_L = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_L = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_L = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_L = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_L = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_L = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_L = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_L = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_L = 수익범위1;
            GenieConfig.CB_repeat_choice_L = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_L = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_L = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_L = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_L = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_L = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_L = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_L = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_L = 반복시간;
            GenieConfig.TB_repeat_value_L = 주문가격;
            GenieConfig.combo_repeat_jumun_L = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_L = 취소시간;
            GenieConfig.combo_repeat_Cancel_L = 취n주문;
            GenieConfig.MTB_repeat_repeat_L = 재주문;
        }

        private static void 반복매매_M(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_M = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_M = 사용;
            GenieConfig.MT_repeat_time_start_M = 시작시간;
            GenieConfig.MT_repeat_time_end_M = 종료시간;
            GenieConfig.CB_repeat_kind_M = 매매종류;
            GenieConfig.combo_repeat_use_condition_M = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_M"))
                Form1.위치별검색식리스트["반복_M"].이름 = 검색식_유무확인(사용, "반복매매_M", 검색식);

            GenieConfig.MTB_repeat_delay_M = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_M = 매입금;
            GenieConfig.TB_repeat_누적거래량_M = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_M = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_M = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_M = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_M = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_M = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_M = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_M = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_M = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_M = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_M = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_M = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_M = 수익범위1;
            GenieConfig.CB_repeat_choice_M = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_M = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_M = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_M = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_M = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_M = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_M = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_M = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_M = 반복시간;
            GenieConfig.TB_repeat_value_M = 주문가격;
            GenieConfig.combo_repeat_jumun_M = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_M = 취소시간;
            GenieConfig.combo_repeat_Cancel_M = 취n주문;
            GenieConfig.MTB_repeat_repeat_M = 재주문;
        }

        private static void 반복매매_N(bool 사용, int 시작시간, int 종료시간, bool 매매종류, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취n주문, int 재주문)
        {
            반복_N = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_repeat_use_N = 사용;
            GenieConfig.MT_repeat_time_start_N = 시작시간;
            GenieConfig.MT_repeat_time_end_N = 종료시간;
            GenieConfig.CB_repeat_kind_N = 매매종류;
            GenieConfig.combo_repeat_use_condition_N = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("반복_N"))
                Form1.위치별검색식리스트["반복_N"].이름 = 검색식_유무확인(사용, "반복매매_N", 검색식);

            GenieConfig.MTB_repeat_delay_N = 검색유지시간;
            GenieConfig.TB_Repeat_매입금_N = 매입금;
            GenieConfig.TB_repeat_누적거래량_N = 누적거래량;
            GenieConfig.TB_repeat_누적거래대금_N = 누적거래대금;

            GenieConfig.TB_repeat_MinMAPeriod1_N = TB_mma1;
            GenieConfig.CBB_repeat_MinMAPeriod1_N = CBB_mma1;
            GenieConfig.TB_repeat_MinMAPeriod2_N = TB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod2_N = CBB_mma2;
            GenieConfig.CBB_repeat_MinMAPeriod1_배열_N = CBB_mma_배열;
            GenieConfig.TB_repeat_DayMAPeriod1_N = TB_dma1;
            GenieConfig.CBB_repeat_DayMAPeriod1_N = CBB_dma1;
            GenieConfig.TB_repeat_DayMAPeriod2_N = TB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod2_N = CBB_dma2;
            GenieConfig.CBB_repeat_DayMAPeriod_배열_N = CBB_dma_배열;

            GenieConfig.TB_repeat_suik_1_N = 수익범위1;
            GenieConfig.CB_repeat_choice_N = 수익범위선택;
            GenieConfig.TB_repeat_suik_2_N = 수익범위2;
            GenieConfig.combo_repeat_suik_gubun_N = 수익구분;
            GenieConfig.TB_repeat_sell_ratio_N = 매수비중;
            GenieConfig.combo_repeat_sell_gubun_N = 매수구분;
            GenieConfig.TB_Repeat_maemae_1_N = 매매범위1;
            GenieConfig.TB_Repeat_maemae_2_N = 매매범위2;
            GenieConfig.combo_Repeat_maemae_gubun_N = 매매범위기준;
            GenieConfig.MT_repeat_repeat_time_N = 반복시간;
            GenieConfig.TB_repeat_value_N = 주문가격;
            GenieConfig.combo_repeat_jumun_N = 매수매도;
            GenieConfig.MTB_repeat_Cancel_time_N = 취소시간;
            GenieConfig.combo_repeat_Cancel_N = 취n주문;
            GenieConfig.MTB_repeat_repeat_N = 재주문;
        }
        // ---------------------------------------------------------
        // 계좌관리 - 추매 조건 (Setting.accmgr 사용)
        // ---------------------------------------------------------
        private static void 계좌관리_추매조건(bool CB_총매수금, double 종목최대매수금, bool CB_일매수제한금, double 일매수제한금, bool CB_회수제한, int 회수제한,
                                            int 추매주가이상, int 추매주가이하, double 추매등락률이상, double 추매등락률이하)
        {
            GenieConfig.CB_총매수금 = CB_총매수금;
            GenieConfig.TB_총매수금 = 종목최대매수금;
            GenieConfig.CB_일매수제한금 = CB_일매수제한금;
            GenieConfig.TB_일매수제한금 = 일매수제한금;
            GenieConfig.CB_회수제한 = CB_회수제한;
            GenieConfig.TB_회수제한 = 회수제한;

            // [수정] Properties -> Setting.accmgr
            GenieConfig.TB_리밸_추매주가이상 = 추매주가이상;
            GenieConfig.TB_리밸_추매주가이하 = 추매주가이하;
            GenieConfig.TB_리밸_추매등락률이상 = 추매등락률이상;
            GenieConfig.TB_리밸_추매등락률이하 = 추매등락률이하;
        }

        // ---------------------------------------------------------
        // 계좌관리 - 분할 주문 (Setting.accmgr 사용)
        // ---------------------------------------------------------
        private static void 계좌관리_분할주문(int 분할간격_A, int 분할횟수_A, int 분할간격_B, int 분할횟수_B, int 분할간격_C, int 분할횟수_C)
        {
            // [수정] Properties -> Setting.accmgr
            GenieConfig.TB_분할간격_A = 분할간격_A;
            GenieConfig.TB_분할간격_B = 분할간격_B;
            GenieConfig.TB_분할간격_C = 분할간격_C;
            GenieConfig.TB_분할횟수_A = 분할횟수_A;
            GenieConfig.TB_분할횟수_B = 분할횟수_B;
            GenieConfig.TB_분할횟수_C = 분할횟수_C;
        }

        // ---------------------------------------------------------
        // 계좌관리 - 기준 비율 관리 (Setting.accmgr & Setting.acc 사용)
        // ---------------------------------------------------------
        private static void 계좌관리_기준비율관리(bool CB_매수기준, int TB_매수비율, bool CB_손익기준, int TB_손익비율)
        {
            // [수정] Properties -> Setting.accmgr
            GenieConfig.CB_매수기준 = CB_매수기준;

            // [주의] MT_principal(투자원금)은 Setting.acc에 있습니다.
            GenieConfig.Today_매수기준금 = GenieConfig.MT_principal + "@" + GenieConfig.MT_principal;

            GenieConfig.CB_손익기준 = CB_손익기준;
            GenieConfig.Today_손익기준금 = GenieConfig.MT_principal + "@" + GenieConfig.MT_principal;

            if (!GenieConfig.CB_기본매매변경)
            {
                GenieConfig.TB_매수비율 = TB_매수비율;
                GenieConfig.TB_손익비율 = TB_손익비율;
            }
        }

        // ---------------------------------------------------------
        // 계좌관리 - 감시 주문 시간 및 기준금 (Setting.accmgr 사용)
        // ---------------------------------------------------------
        private static void 계좌관리_감시주문시간n기준금(int Selltime_오전, int Selltime_오후, bool rebalance_기준금, bool cut_기준금, bool Liquidation_기준금)
        {
            // [수정] Properties -> Setting.accmgr
            GenieConfig.MTB_rebalance_Selltime_오전 = Selltime_오전;
            GenieConfig.MTB_rebalance_Selltime_오후 = Selltime_오후;
            GenieConfig.CB_rebalance_기준금 = rebalance_기준금;
            GenieConfig.CB_cut_기준금 = cut_기준금;
            GenieConfig.CB_Liquidation_기준금 = Liquidation_기준금;
        }

        // ---------------------------------------------------------
        // 계좌관리 - 리밸런싱 A~G 설정 (Setting.accmgr 사용)
        // ---------------------------------------------------------

        private static void 계좌관리_리밸런싱_A(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
            double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시매수매도,
            bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_A = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_rebalance_A = 사용;
            GenieConfig.MT_rebalance_starttime_A = 시작시간;
            GenieConfig.MT_rebalance_stoptime_A = 종료시간;
            GenieConfig.combo_rebalance_use_condition_A = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("리밸_A"))
                Form1.위치별검색식리스트["리밸_A"].이름 = 검색식_유무확인(사용, "리밸런싱_A", 검색식);

            GenieConfig.MTB_rebalance_delay_A = 검색유지시간;
            GenieConfig.TB_Rebalance_매입금_A = 매입금;
            GenieConfig.TB_rebalance_누적거래량_A = 누적거래량;
            GenieConfig.TB_rebalance_누적거래대금_A = 누적거래대금;

            GenieConfig.TB_rebalance_MinMAPeriod1_A = TB_mma1;
            GenieConfig.CBB_rebalance_MinMAPeriod1_A = CBB_mma1;
            GenieConfig.TB_rebalance_MinMAPeriod2_A = TB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod2_A = CBB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod1_배열_A = CBB_mma_배열;
            GenieConfig.TB_rebalance_DayMAPeriod1_A = TB_dma1;
            GenieConfig.CBB_rebalance_DayMAPeriod1_A = CBB_dma1;
            GenieConfig.TB_rebalance_DayMAPeriod2_A = TB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod2_A = CBB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod_배열_A = CBB_dma_배열;

            GenieConfig.TB_rebalance_suik_1_A = 수익범위1;
            GenieConfig.CB_rebalance_choice_A = 수익범위선택;
            GenieConfig.TB_rebalance_suik_2_A = 수익범위2;
            GenieConfig.combo_rebalance_suik_gubun_A = 수익구분;
            GenieConfig.TB_rebalance_sell_ratio_A = 매수비중;
            GenieConfig.combo_rebalance_sell_gubun_A = 매수구분;
            GenieConfig.TB_rebalance_maemae_1_A = 매매범위1;
            GenieConfig.TB_rebalance_maemae_2_A = 매매범위2;
            GenieConfig.combo_rebalance_maemae_gubun_A = 매매범위기준;
            GenieConfig.MT_rebalance_repeat_time_A = 반복시간;
            GenieConfig.TB_rebalance_value_A = 주문가격;
            GenieConfig.combo_rebalance_jumun_A = 매수매도;
            GenieConfig.MTB_rebalance_Cancel_time_A = 취소시간;

            GenieConfig.CB_rebalance_option_A = 감시시점;
            GenieConfig.TB_rebalance_sellratio1_A = 주문조건_1차;
            GenieConfig.리밸매도기준1_A = 주문조건선택_1차;
            GenieConfig.TB_rebalance_sellvolume1_A = 매도비중_1차;
            GenieConfig.TB_rebalance_sellcancel1_A = 취소시간_1차;
            GenieConfig.TB_rebalance_sellratio2_A = 주문조건_2차;
            GenieConfig.리밸매도기준2_A = 주문조건선택_2차;
            GenieConfig.TB_rebalance_sellvolume2_A = 매도비중_2차;
            GenieConfig.TB_rebalance_sellcancel2_A = 취소시간_2차;
            GenieConfig.CB_rebalance_매도체크_A = 매도체크;
            GenieConfig.CBB_rebalance_Selltime_A = 감시주문시간;
            GenieConfig.TB_rebalance_감시_value_A = 감시주문값;
            GenieConfig.combo_rebalance_감시_jumun_A = 감시매수매도;

            GenieConfig.CB_rebalance_TS_1차_A = TS_1차;
            GenieConfig.TB_rebalance_TS_1차_down_A = TS_1차_down;
            GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_A = TB_1차_이평;
            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_A = CBB_1차_이평;
            GenieConfig.CB_rebalance_TS_2차_A = TS_2차;
            GenieConfig.TB_rebalance_TS_2차_down_A = TS_2차_down;
            GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_A = TB_2차_이평;
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_A = CBB_2차_이평;
        }

        private static void 계좌관리_리밸런싱_B(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
            double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시매수매도,
            bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_B = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_rebalance_B = 사용;
            GenieConfig.MT_rebalance_starttime_B = 시작시간;
            GenieConfig.MT_rebalance_stoptime_B = 종료시간;
            GenieConfig.combo_rebalance_use_condition_B = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("리밸_B"))
            {
                Form1.위치별검색식리스트["리밸_B"].이름 = 검색식_유무확인(사용, "리밸런싱_B", 검색식);
            }

            GenieConfig.MTB_rebalance_delay_B = 검색유지시간;
            GenieConfig.TB_Rebalance_매입금_B = 매입금;
            GenieConfig.TB_rebalance_누적거래량_B = 누적거래량;
            GenieConfig.TB_rebalance_누적거래대금_B = 누적거래대금;

            GenieConfig.TB_rebalance_MinMAPeriod1_B = TB_mma1;
            GenieConfig.CBB_rebalance_MinMAPeriod1_B = CBB_mma1;
            GenieConfig.TB_rebalance_MinMAPeriod2_B = TB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod2_B = CBB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod1_배열_B = CBB_mma_배열;
            GenieConfig.TB_rebalance_DayMAPeriod1_B = TB_dma1;
            GenieConfig.CBB_rebalance_DayMAPeriod1_B = CBB_dma1;
            GenieConfig.TB_rebalance_DayMAPeriod2_B = TB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod2_B = CBB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod_배열_B = CBB_dma_배열;

            GenieConfig.TB_rebalance_suik_1_B = 수익범위1;
            GenieConfig.CB_rebalance_choice_B = 수익범위선택;
            GenieConfig.TB_rebalance_suik_2_B = 수익범위2;
            GenieConfig.combo_rebalance_suik_gubun_B = 수익구분;
            GenieConfig.TB_rebalance_sell_ratio_B = 매수비중;
            GenieConfig.combo_rebalance_sell_gubun_B = 매수구분;
            GenieConfig.TB_rebalance_maemae_1_B = 매매범위1;
            GenieConfig.TB_rebalance_maemae_2_B = 매매범위2;
            GenieConfig.combo_rebalance_maemae_gubun_B = 매매범위기준;
            GenieConfig.MT_rebalance_repeat_time_B = 반복시간;
            GenieConfig.TB_rebalance_value_B = 주문가격;
            GenieConfig.combo_rebalance_jumun_B = 매수매도;
            GenieConfig.MTB_rebalance_Cancel_time_B = 취소시간;

            GenieConfig.CB_rebalance_option_B = 감시시점;
            GenieConfig.TB_rebalance_sellratio1_B = 주문조건_1차;
            GenieConfig.리밸매도기준1_B = 주문조건선택_1차;
            GenieConfig.TB_rebalance_sellvolume1_B = 매도비중_1차;
            GenieConfig.TB_rebalance_sellcancel1_B = 취소시간_1차;
            GenieConfig.TB_rebalance_sellratio2_B = 주문조건_2차;
            GenieConfig.리밸매도기준2_B = 주문조건선택_2차;
            GenieConfig.TB_rebalance_sellvolume2_B = 매도비중_2차;
            GenieConfig.TB_rebalance_sellcancel2_B = 취소시간_2차;
            GenieConfig.CB_rebalance_매도체크_B = 매도체크;
            GenieConfig.CBB_rebalance_Selltime_B = 감시주문시간;
            GenieConfig.TB_rebalance_감시_value_B = 감시주문값;
            GenieConfig.combo_rebalance_감시_jumun_B = 감시매수매도;

            GenieConfig.CB_rebalance_TS_1차_B = TS_1차;
            GenieConfig.TB_rebalance_TS_1차_down_B = TS_1차_down;
            GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_B = TB_1차_이평;
            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_B = CBB_1차_이평;
            GenieConfig.CB_rebalance_TS_2차_B = TS_2차;
            GenieConfig.TB_rebalance_TS_2차_down_B = TS_2차_down;
            GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_B = TB_2차_이평;
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_B = CBB_2차_이평;
        }

        private static void 계좌관리_리밸런싱_C(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
            double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시매수매도,
            bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_C = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_rebalance_C = 사용;
            GenieConfig.MT_rebalance_starttime_C = 시작시간;
            GenieConfig.MT_rebalance_stoptime_C = 종료시간;
            GenieConfig.combo_rebalance_use_condition_C = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("리밸_C"))
                Form1.위치별검색식리스트["리밸_C"].이름 = 검색식_유무확인(사용, "리밸런싱_C", 검색식);

            GenieConfig.MTB_rebalance_delay_C = 검색유지시간;
            GenieConfig.TB_Rebalance_매입금_C = 매입금;
            GenieConfig.TB_rebalance_누적거래량_C = 누적거래량;
            GenieConfig.TB_rebalance_누적거래대금_C = 누적거래대금;

            GenieConfig.TB_rebalance_MinMAPeriod1_C = TB_mma1;
            GenieConfig.CBB_rebalance_MinMAPeriod1_C = CBB_mma1;
            GenieConfig.TB_rebalance_MinMAPeriod2_C = TB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod2_C = CBB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod1_배열_C = CBB_mma_배열;
            GenieConfig.TB_rebalance_DayMAPeriod1_C = TB_dma1;
            GenieConfig.CBB_rebalance_DayMAPeriod1_C = CBB_dma1;
            GenieConfig.TB_rebalance_DayMAPeriod2_C = TB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod2_C = CBB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod_배열_C = CBB_dma_배열;

            GenieConfig.TB_rebalance_suik_1_C = 수익범위1;
            GenieConfig.CB_rebalance_choice_C = 수익범위선택;
            GenieConfig.TB_rebalance_suik_2_C = 수익범위2;
            GenieConfig.combo_rebalance_suik_gubun_C = 수익구분;
            GenieConfig.TB_rebalance_sell_ratio_C = 매수비중;
            GenieConfig.combo_rebalance_sell_gubun_C = 매수구분;
            GenieConfig.TB_rebalance_maemae_1_C = 매매범위1;
            GenieConfig.TB_rebalance_maemae_2_C = 매매범위2;
            GenieConfig.combo_rebalance_maemae_gubun_C = 매매범위기준;
            GenieConfig.MT_rebalance_repeat_time_C = 반복시간;
            GenieConfig.TB_rebalance_value_C = 주문가격;
            GenieConfig.combo_rebalance_jumun_C = 매수매도;
            GenieConfig.MTB_rebalance_Cancel_time_C = 취소시간;

            GenieConfig.CB_rebalance_option_C = 감시시점;
            GenieConfig.TB_rebalance_sellratio1_C = 주문조건_1차;
            GenieConfig.리밸매도기준1_C = 주문조건선택_1차;
            GenieConfig.TB_rebalance_sellvolume1_C = 매도비중_1차;
            GenieConfig.TB_rebalance_sellcancel1_C = 취소시간_1차;
            GenieConfig.TB_rebalance_sellratio2_C = 주문조건_2차;
            GenieConfig.리밸매도기준2_C = 주문조건선택_2차;
            GenieConfig.TB_rebalance_sellvolume2_C = 매도비중_2차;
            GenieConfig.TB_rebalance_sellcancel2_C = 취소시간_2차;
            GenieConfig.CB_rebalance_매도체크_C = 매도체크;
            GenieConfig.CBB_rebalance_Selltime_C = 감시주문시간;
            GenieConfig.TB_rebalance_감시_value_C = 감시주문값;
            GenieConfig.combo_rebalance_감시_jumun_C = 감시매수매도;

            GenieConfig.CB_rebalance_TS_1차_C = TS_1차;
            GenieConfig.TB_rebalance_TS_1차_down_C = TS_1차_down;
            GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_C = TB_1차_이평;
            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_C = CBB_1차_이평;
            GenieConfig.CB_rebalance_TS_2차_C = TS_2차;
            GenieConfig.TB_rebalance_TS_2차_down_C = TS_2차_down;
            GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_C = TB_2차_이평;
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_C = CBB_2차_이평;
        }

        private static void 계좌관리_리밸런싱_D(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
            double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시매수매도,
            bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_D = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_rebalance_D = 사용;
            GenieConfig.MT_rebalance_starttime_D = 시작시간;
            GenieConfig.MT_rebalance_stoptime_D = 종료시간;
            GenieConfig.combo_rebalance_use_condition_D = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("리밸_D"))
                Form1.위치별검색식리스트["리밸_D"].이름 = 검색식_유무확인(사용, "리밸런싱_D", 검색식);

            GenieConfig.MTB_rebalance_delay_D = 검색유지시간;
            GenieConfig.TB_Rebalance_매입금_D = 매입금;
            GenieConfig.TB_rebalance_누적거래량_D = 누적거래량;
            GenieConfig.TB_rebalance_누적거래대금_D = 누적거래대금;

            GenieConfig.TB_rebalance_MinMAPeriod1_D = TB_mma1;
            GenieConfig.CBB_rebalance_MinMAPeriod1_D = CBB_mma1;
            GenieConfig.TB_rebalance_MinMAPeriod2_D = TB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod2_D = CBB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod1_배열_D = CBB_mma_배열;
            GenieConfig.TB_rebalance_DayMAPeriod1_D = TB_dma1;
            GenieConfig.CBB_rebalance_DayMAPeriod1_D = CBB_dma1;
            GenieConfig.TB_rebalance_DayMAPeriod2_D = TB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod2_D = CBB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod_배열_D = CBB_dma_배열;

            GenieConfig.TB_rebalance_suik_1_D = 수익범위1;
            GenieConfig.CB_rebalance_choice_D = 수익범위선택;
            GenieConfig.TB_rebalance_suik_2_D = 수익범위2;
            GenieConfig.combo_rebalance_suik_gubun_D = 수익구분;
            GenieConfig.TB_rebalance_sell_ratio_D = 매수비중;
            GenieConfig.combo_rebalance_sell_gubun_D = 매수구분;
            GenieConfig.TB_rebalance_maemae_1_D = 매매범위1;
            GenieConfig.TB_rebalance_maemae_2_D = 매매범위2;
            GenieConfig.combo_rebalance_maemae_gubun_D = 매매범위기준;
            GenieConfig.MT_rebalance_repeat_time_D = 반복시간;
            GenieConfig.TB_rebalance_value_D = 주문가격;
            GenieConfig.combo_rebalance_jumun_D = 매수매도;
            GenieConfig.MTB_rebalance_Cancel_time_D = 취소시간;

            GenieConfig.CB_rebalance_option_D = 감시시점;
            GenieConfig.TB_rebalance_sellratio1_D = 주문조건_1차;
            GenieConfig.리밸매도기준1_D = 주문조건선택_1차;
            GenieConfig.TB_rebalance_sellvolume1_D = 매도비중_1차;
            GenieConfig.TB_rebalance_sellcancel1_D = 취소시간_1차;
            GenieConfig.TB_rebalance_sellratio2_D = 주문조건_2차;
            GenieConfig.리밸매도기준2_D = 주문조건선택_2차;
            GenieConfig.TB_rebalance_sellvolume2_D = 매도비중_2차;
            GenieConfig.TB_rebalance_sellcancel2_D = 취소시간_2차;
            GenieConfig.CB_rebalance_매도체크_D = 매도체크;
            GenieConfig.CBB_rebalance_Selltime_D = 감시주문시간;
            GenieConfig.TB_rebalance_감시_value_D = 감시주문값;
            GenieConfig.combo_rebalance_감시_jumun_D = 감시매수매도;

            GenieConfig.CB_rebalance_TS_1차_D = TS_1차;
            GenieConfig.TB_rebalance_TS_1차_down_D = TS_1차_down;
            GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_D = TB_1차_이평;
            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_D = CBB_1차_이평;
            GenieConfig.CB_rebalance_TS_2차_D = TS_2차;
            GenieConfig.TB_rebalance_TS_2차_down_D = TS_2차_down;
            GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_D = TB_2차_이평;
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_D = CBB_2차_이평;
        }

        private static void 계좌관리_리밸런싱_E(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
            double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시매수매도,
            bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_E = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_rebalance_E = 사용;
            GenieConfig.MT_rebalance_starttime_E = 시작시간;
            GenieConfig.MT_rebalance_stoptime_E = 종료시간;
            GenieConfig.combo_rebalance_use_condition_E = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("리밸_E"))
                Form1.위치별검색식리스트["리밸_E"].이름 = 검색식_유무확인(사용, "리밸런싱_E", 검색식);

            GenieConfig.MTB_rebalance_delay_E = 검색유지시간;
            GenieConfig.TB_Rebalance_매입금_E = 매입금;
            GenieConfig.TB_rebalance_누적거래량_E = 누적거래량;
            GenieConfig.TB_rebalance_누적거래대금_E = 누적거래대금;

            GenieConfig.TB_rebalance_MinMAPeriod1_E = TB_mma1;
            GenieConfig.CBB_rebalance_MinMAPeriod1_E = CBB_mma1;
            GenieConfig.TB_rebalance_MinMAPeriod2_E = TB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod2_E = CBB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod1_배열_E = CBB_mma_배열;
            GenieConfig.TB_rebalance_DayMAPeriod1_E = TB_dma1;
            GenieConfig.CBB_rebalance_DayMAPeriod1_E = CBB_dma1;
            GenieConfig.TB_rebalance_DayMAPeriod2_E = TB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod2_E = CBB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod_배열_E = CBB_dma_배열;

            GenieConfig.TB_rebalance_suik_1_E = 수익범위1;
            GenieConfig.CB_rebalance_choice_E = 수익범위선택;
            GenieConfig.TB_rebalance_suik_2_E = 수익범위2;
            GenieConfig.combo_rebalance_suik_gubun_E = 수익구분;
            GenieConfig.TB_rebalance_sell_ratio_E = 매수비중;
            GenieConfig.combo_rebalance_sell_gubun_E = 매수구분;
            GenieConfig.TB_rebalance_maemae_1_E = 매매범위1;
            GenieConfig.TB_rebalance_maemae_2_E = 매매범위2;
            GenieConfig.combo_rebalance_maemae_gubun_E = 매매범위기준;
            GenieConfig.MT_rebalance_repeat_time_E = 반복시간;
            GenieConfig.TB_rebalance_value_E = 주문가격;
            GenieConfig.combo_rebalance_jumun_E = 매수매도;
            GenieConfig.MTB_rebalance_Cancel_time_E = 취소시간;

            GenieConfig.CB_rebalance_option_E = 감시시점;
            GenieConfig.TB_rebalance_sellratio1_E = 주문조건_1차;
            GenieConfig.리밸매도기준1_E = 주문조건선택_1차;
            GenieConfig.TB_rebalance_sellvolume1_E = 매도비중_1차;
            GenieConfig.TB_rebalance_sellcancel1_E = 취소시간_1차;
            GenieConfig.TB_rebalance_sellratio2_E = 주문조건_2차;
            GenieConfig.리밸매도기준2_E = 주문조건선택_2차;
            GenieConfig.TB_rebalance_sellvolume2_E = 매도비중_2차;
            GenieConfig.TB_rebalance_sellcancel2_E = 취소시간_2차;
            GenieConfig.CB_rebalance_매도체크_E = 매도체크;
            GenieConfig.CBB_rebalance_Selltime_E = 감시주문시간;
            GenieConfig.TB_rebalance_감시_value_E = 감시주문값;
            GenieConfig.combo_rebalance_감시_jumun_E = 감시매수매도;

            GenieConfig.CB_rebalance_TS_1차_E = TS_1차;
            GenieConfig.TB_rebalance_TS_1차_down_E = TS_1차_down;
            GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_E = TB_1차_이평;
            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_E = CBB_1차_이평;
            GenieConfig.CB_rebalance_TS_2차_E = TS_2차;
            GenieConfig.TB_rebalance_TS_2차_down_E = TS_2차_down;
            GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_E = TB_2차_이평;
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_E = CBB_2차_이평;
        }

        private static void 계좌관리_리밸런싱_F(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
            double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시매수매도,
            bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_F = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_rebalance_F = 사용;
            GenieConfig.MT_rebalance_starttime_F = 시작시간;
            GenieConfig.MT_rebalance_stoptime_F = 종료시간;
            GenieConfig.combo_rebalance_use_condition_F = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("리밸_F"))
                Form1.위치별검색식리스트["리밸_F"].이름 = 검색식_유무확인(사용, "리밸런싱_F", 검색식);

            GenieConfig.MTB_rebalance_delay_F = 검색유지시간;
            GenieConfig.TB_Rebalance_매입금_F = 매입금;
            GenieConfig.TB_rebalance_누적거래량_F = 누적거래량;
            GenieConfig.TB_rebalance_누적거래대금_F = 누적거래대금;

            GenieConfig.TB_rebalance_MinMAPeriod1_F = TB_mma1;
            GenieConfig.CBB_rebalance_MinMAPeriod1_F = CBB_mma1;
            GenieConfig.TB_rebalance_MinMAPeriod2_F = TB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod2_F = CBB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod1_배열_F = CBB_mma_배열;
            GenieConfig.TB_rebalance_DayMAPeriod1_F = TB_dma1;
            GenieConfig.CBB_rebalance_DayMAPeriod1_F = CBB_dma1;
            GenieConfig.TB_rebalance_DayMAPeriod2_F = TB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod2_F = CBB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod_배열_F = CBB_dma_배열;

            GenieConfig.TB_rebalance_suik_1_F = 수익범위1;
            GenieConfig.CB_rebalance_choice_F = 수익범위선택;
            GenieConfig.TB_rebalance_suik_2_F = 수익범위2;
            GenieConfig.combo_rebalance_suik_gubun_F = 수익구분;
            GenieConfig.TB_rebalance_sell_ratio_F = 매수비중;
            GenieConfig.combo_rebalance_sell_gubun_F = 매수구분;
            GenieConfig.TB_rebalance_maemae_1_F = 매매범위1;
            GenieConfig.TB_rebalance_maemae_2_F = 매매범위2;
            GenieConfig.combo_rebalance_maemae_gubun_F = 매매범위기준;
            GenieConfig.MT_rebalance_repeat_time_F = 반복시간;
            GenieConfig.TB_rebalance_value_F = 주문가격;
            GenieConfig.combo_rebalance_jumun_F = 매수매도;
            GenieConfig.MTB_rebalance_Cancel_time_F = 취소시간;

            GenieConfig.CB_rebalance_option_F = 감시시점;
            GenieConfig.TB_rebalance_sellratio1_F = 주문조건_1차;
            GenieConfig.리밸매도기준1_F = 주문조건선택_1차;
            GenieConfig.TB_rebalance_sellvolume1_F = 매도비중_1차;
            GenieConfig.TB_rebalance_sellcancel1_F = 취소시간_1차;
            GenieConfig.TB_rebalance_sellratio2_F = 주문조건_2차;
            GenieConfig.리밸매도기준2_F = 주문조건선택_2차;
            GenieConfig.TB_rebalance_sellvolume2_F = 매도비중_2차;
            GenieConfig.TB_rebalance_sellcancel2_F = 취소시간_2차;
            GenieConfig.CB_rebalance_매도체크_F = 매도체크;
            GenieConfig.CBB_rebalance_Selltime_F = 감시주문시간;
            GenieConfig.TB_rebalance_감시_value_F = 감시주문값;
            GenieConfig.combo_rebalance_감시_jumun_F = 감시매수매도;

            GenieConfig.CB_rebalance_TS_1차_F = TS_1차;
            GenieConfig.TB_rebalance_TS_1차_down_F = TS_1차_down;
            GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_F = TB_1차_이평;
            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_F = CBB_1차_이평;
            GenieConfig.CB_rebalance_TS_2차_F = TS_2차;
            GenieConfig.TB_rebalance_TS_2차_down_F = TS_2차_down;
            GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_F = TB_2차_이평;
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_F = CBB_2차_이평;
        }

        private static void 계좌관리_리밸런싱_G(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금, int 누적거래량, int 누적거래대금,
            int TB_mma1, int CBB_mma1, int TB_mma2, int CBB_mma2, int CBB_mma_배열, int TB_dma1, int CBB_dma1, int TB_dma2, int CBB_dma2, int CBB_dma_배열,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매수비중, int 매수구분, int 매매범위1, int 매매범위2, int 매매범위기준,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, bool 감시시점, double 주문조건_1차, string 주문조건선택_1차, double 매도비중_1차, int 취소시간_1차,
            double 주문조건_2차, string 주문조건선택_2차, double 매도비중_2차, int 취소시간_2차, bool 매도체크, int 감시주문시간, double 감시주문값, int 감시매수매도,
            bool TS_1차, double TS_1차_down, int TB_1차_이평, int CBB_1차_이평, bool TS_2차, double TS_2차_down, int TB_2차_이평, int CBB_2차_이평)
        {
            리밸_G = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_rebalance_G = 사용;
            GenieConfig.MT_rebalance_starttime_G = 시작시간;
            GenieConfig.MT_rebalance_stoptime_G = 종료시간;
            GenieConfig.combo_rebalance_use_condition_G = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("리밸_G"))
                Form1.위치별검색식리스트["리밸_G"].이름 = 검색식_유무확인(사용, "리밸런싱_G", 검색식);

            GenieConfig.MTB_rebalance_delay_G = 검색유지시간;
            GenieConfig.TB_Rebalance_매입금_G = 매입금;
            GenieConfig.TB_rebalance_누적거래량_G = 누적거래량;
            GenieConfig.TB_rebalance_누적거래대금_G = 누적거래대금;

            GenieConfig.TB_rebalance_MinMAPeriod1_G = TB_mma1;
            GenieConfig.CBB_rebalance_MinMAPeriod1_G = CBB_mma1;
            GenieConfig.TB_rebalance_MinMAPeriod2_G = TB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod2_G = CBB_mma2;
            GenieConfig.CBB_rebalance_MinMAPeriod1_배열_G = CBB_mma_배열;
            GenieConfig.TB_rebalance_DayMAPeriod1_G = TB_dma1;
            GenieConfig.CBB_rebalance_DayMAPeriod1_G = CBB_dma1;
            GenieConfig.TB_rebalance_DayMAPeriod2_G = TB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod2_G = CBB_dma2;
            GenieConfig.CBB_rebalance_DayMAPeriod_배열_G = CBB_dma_배열;

            GenieConfig.TB_rebalance_suik_1_G = 수익범위1;
            GenieConfig.CB_rebalance_choice_G = 수익범위선택;
            GenieConfig.TB_rebalance_suik_2_G = 수익범위2;
            GenieConfig.combo_rebalance_suik_gubun_G = 수익구분;
            GenieConfig.TB_rebalance_sell_ratio_G = 매수비중;
            GenieConfig.combo_rebalance_sell_gubun_G = 매수구분;
            GenieConfig.TB_rebalance_maemae_1_G = 매매범위1;
            GenieConfig.TB_rebalance_maemae_2_G = 매매범위2;
            GenieConfig.combo_rebalance_maemae_gubun_G = 매매범위기준;
            GenieConfig.MT_rebalance_repeat_time_G = 반복시간;
            GenieConfig.TB_rebalance_value_G = 주문가격;
            GenieConfig.combo_rebalance_jumun_G = 매수매도;
            GenieConfig.MTB_rebalance_Cancel_time_G = 취소시간;

            GenieConfig.CB_rebalance_option_G = 감시시점;
            GenieConfig.TB_rebalance_sellratio1_G = 주문조건_1차;
            GenieConfig.리밸매도기준1_G = 주문조건선택_1차;
            GenieConfig.TB_rebalance_sellvolume1_G = 매도비중_1차;
            GenieConfig.TB_rebalance_sellcancel1_G = 취소시간_1차;
            GenieConfig.TB_rebalance_sellratio2_G = 주문조건_2차;
            GenieConfig.리밸매도기준2_G = 주문조건선택_2차;
            GenieConfig.TB_rebalance_sellvolume2_G = 매도비중_2차;
            GenieConfig.TB_rebalance_sellcancel2_G = 취소시간_2차;
            GenieConfig.CB_rebalance_매도체크_G = 매도체크;
            GenieConfig.CBB_rebalance_Selltime_G = 감시주문시간;
            GenieConfig.TB_rebalance_감시_value_G = 감시주문값;
            GenieConfig.combo_rebalance_감시_jumun_G = 감시매수매도;

            GenieConfig.CB_rebalance_TS_1차_G = TS_1차;
            GenieConfig.TB_rebalance_TS_1차_down_G = TS_1차_down;
            GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_G = TB_1차_이평;
            GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_G = CBB_1차_이평;
            GenieConfig.CB_rebalance_TS_2차_G = TS_2차;
            GenieConfig.TB_rebalance_TS_2차_down_G = TS_2차_down;
            GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_G = TB_2차_이평;
            GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_G = CBB_2차_이평;
        }
        // ---------------------------------------------------------
        // 계좌관리 - 잔고 청산 A~C 설정 (Setting.accmgr 사용)
        // ---------------------------------------------------------

        private static void 계좌관리_잔고청산_A(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금1, double 매입금2, int TB이평, int CBB이평,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매도비중, int 매도구분, int 매매범위1, int 매매범위2,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취소후주문, int 반복횟수, bool 매도정지, bool 추매금지, bool 강제매도, bool 수익보전,
            bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            청산_A = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_Liquidation_A = 사용;
            GenieConfig.MTB_Liquidation_Starttime_A = 시작시간;
            GenieConfig.MTB_Liquidation_Stoptime_A = 종료시간;
            GenieConfig.CBB_Liquidation_use_condition_A = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("청산_A"))
                Form1.위치별검색식리스트["청산_A"].이름 = 검색식_유무확인(사용, "잔고청산_A", 검색식);

            GenieConfig.MTB_Liquidation_delay_A = 검색유지시간;
            GenieConfig.TB_잔고청산_매입금1_A = 매입금1;
            GenieConfig.TB_잔고청산_매입금2_A = 매입금2;
            GenieConfig.TB_Liquidation_MinMAPeriod_A = TB이평;
            GenieConfig.CBB_Liquidation_MinMAPeriod_A = CBB이평;
            GenieConfig.TB_Liquidation_suik_1_A = 수익범위1;
            GenieConfig.CB_Liquidation_choice_A = 수익범위선택;
            GenieConfig.TB_Liquidation_suik_2_A = 수익범위2;
            GenieConfig.CBB_Liquidation_suik_gubun_A = 수익구분;
            GenieConfig.TB_Liquidation_sell_ratio_A = 매도비중;
            GenieConfig.CBB_Liquidation_sell_gubun_A = 매도구분;
            GenieConfig.TB_Liquidation_maemae_1_A = 매매범위1;
            GenieConfig.TB_Liquidation_maemae_2_A = 매매범위2;
            GenieConfig.MT_Liquidation_repeat_time_A = 반복시간;
            GenieConfig.TB_Liquidation_value_A = 주문가격;
            GenieConfig.CBB_Liquidation_jumun_A = 매수매도;
            GenieConfig.MTB_Liquidation_Cancel_time_A = 취소시간;
            GenieConfig.CBB_Liquidation_Cancel_A = 취소후주문;
            GenieConfig.MTB_Liquidation_repeat_A = 반복횟수;
            GenieConfig.CB_Liquidation_SellStop_A = 매도정지;
            GenieConfig.CB_추매금지_Liquidation_A = 추매금지;
            GenieConfig.CB_Liquidation_강제매도_A = 강제매도;
            GenieConfig.CB_수익보전_Liquidation_A = 수익보전;
            GenieConfig.CB_Liquidation_TS_A = TS;
            GenieConfig.TB_Liquidation_TS_down_A = TS_down;
            GenieConfig.TB_Liquidation_TS_MinMAPeriod_A = TB_TS_mma;
            GenieConfig.CBB_Liquidation_TS_MinMAPeriod_A = CBB_TS_mma;
            GenieConfig.TB_Liquidation_TS_DayMAPeriod_A = TB_TS_dma;
            GenieConfig.CBB_Liquidation_TS_DayMAPeriod_A = CBB_TS_dma;
        }

        private static void 계좌관리_잔고청산_B(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금1, double 매입금2, int TB이평, int CBB이평,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매도비중, int 매도구분, int 매매범위1, int 매매범위2,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취소후주문, int 반복횟수, bool 매도정지, bool 추매금지, bool 강제매도, bool 수익보전,
            bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            청산_B = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_Liquidation_B = 사용;
            GenieConfig.MTB_Liquidation_Starttime_B = 시작시간;
            GenieConfig.MTB_Liquidation_Stoptime_B = 종료시간;
            GenieConfig.CBB_Liquidation_use_condition_B = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("청산_B"))
                Form1.위치별검색식리스트["청산_B"].이름 = 검색식_유무확인(사용, "잔고청산_B", 검색식);

            GenieConfig.MTB_Liquidation_delay_B = 검색유지시간;
            GenieConfig.TB_잔고청산_매입금1_B = 매입금1;
            GenieConfig.TB_잔고청산_매입금2_B = 매입금2;
            GenieConfig.TB_Liquidation_MinMAPeriod_B = TB이평;
            GenieConfig.CBB_Liquidation_MinMAPeriod_B = CBB이평;
            GenieConfig.TB_Liquidation_suik_1_B = 수익범위1;
            GenieConfig.CB_Liquidation_choice_B = 수익범위선택;
            GenieConfig.TB_Liquidation_suik_2_B = 수익범위2;
            GenieConfig.CBB_Liquidation_suik_gubun_B = 수익구분;
            GenieConfig.TB_Liquidation_sell_ratio_B = 매도비중;
            GenieConfig.CBB_Liquidation_sell_gubun_B = 매도구분;
            GenieConfig.TB_Liquidation_maemae_1_B = 매매범위1;
            GenieConfig.TB_Liquidation_maemae_2_B = 매매범위2;
            GenieConfig.MT_Liquidation_repeat_time_B = 반복시간;
            GenieConfig.TB_Liquidation_value_B = 주문가격;
            GenieConfig.CBB_Liquidation_jumun_B = 매수매도;
            GenieConfig.MTB_Liquidation_Cancel_time_B = 취소시간;
            GenieConfig.CBB_Liquidation_Cancel_B = 취소후주문;
            GenieConfig.MTB_Liquidation_repeat_B = 반복횟수;
            GenieConfig.CB_Liquidation_SellStop_B = 매도정지;
            GenieConfig.CB_추매금지_Liquidation_B = 추매금지;
            GenieConfig.CB_Liquidation_강제매도_B = 강제매도;
            GenieConfig.CB_수익보전_Liquidation_B = 수익보전;
            GenieConfig.CB_Liquidation_TS_B = TS;
            GenieConfig.TB_Liquidation_TS_down_B = TS_down;
            GenieConfig.TB_Liquidation_TS_MinMAPeriod_B = TB_TS_mma;
            GenieConfig.CBB_Liquidation_TS_MinMAPeriod_B = CBB_TS_mma;
            GenieConfig.TB_Liquidation_TS_DayMAPeriod_B = TB_TS_dma;
            GenieConfig.CBB_Liquidation_TS_DayMAPeriod_B = CBB_TS_dma;
        }

        private static void 계좌관리_잔고청산_C(bool 사용, int 시작시간, int 종료시간, int 검색식사용, string 검색식, int 검색유지시간, double 매입금1, double 매입금2, int TB이평, int CBB이평,
            double 수익범위1, bool 수익범위선택, double 수익범위2, int 수익구분, double 매도비중, int 매도구분, int 매매범위1, int 매매범위2,
            int 반복시간, double 주문가격, int 매수매도, int 취소시간, int 취소후주문, int 반복횟수, bool 매도정지, bool 추매금지, bool 강제매도, bool 수익보전,
            bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            청산_C = new string[] { 사용.ToString(), 검색식사용.ToString(), 검색식 };

            GenieConfig.CB_Liquidation_C = 사용;
            GenieConfig.MTB_Liquidation_Starttime_C = 시작시간;
            GenieConfig.MTB_Liquidation_Stoptime_C = 종료시간;
            GenieConfig.CBB_Liquidation_use_condition_C = 검색식사용;

            if (Form1.위치별검색식리스트.ContainsKey("청산_C"))
                Form1.위치별검색식리스트["청산_C"].이름 = 검색식_유무확인(사용, "잔고청산_C", 검색식);

            GenieConfig.MTB_Liquidation_delay_C = 검색유지시간;
            GenieConfig.TB_잔고청산_매입금1_C = 매입금1;
            GenieConfig.TB_잔고청산_매입금2_C = 매입금2;
            GenieConfig.TB_Liquidation_MinMAPeriod_C = TB이평;
            GenieConfig.CBB_Liquidation_MinMAPeriod_C = CBB이평;
            GenieConfig.TB_Liquidation_suik_1_C = 수익범위1;
            GenieConfig.CB_Liquidation_choice_C = 수익범위선택;
            GenieConfig.TB_Liquidation_suik_2_C = 수익범위2;
            GenieConfig.CBB_Liquidation_suik_gubun_C = 수익구분;
            GenieConfig.TB_Liquidation_sell_ratio_C = 매도비중;
            GenieConfig.CBB_Liquidation_sell_gubun_C = 매도구분;
            GenieConfig.TB_Liquidation_maemae_1_C = 매매범위1;
            GenieConfig.TB_Liquidation_maemae_2_C = 매매범위2;
            GenieConfig.MT_Liquidation_repeat_time_C = 반복시간;
            GenieConfig.TB_Liquidation_value_C = 주문가격;
            GenieConfig.CBB_Liquidation_jumun_C = 매수매도;
            GenieConfig.MTB_Liquidation_Cancel_time_C = 취소시간;
            GenieConfig.CBB_Liquidation_Cancel_C = 취소후주문;
            GenieConfig.MTB_Liquidation_repeat_C = 반복횟수;
            GenieConfig.CB_Liquidation_SellStop_C = 매도정지;
            GenieConfig.CB_추매금지_Liquidation_C = 추매금지;
            GenieConfig.CB_Liquidation_강제매도_C = 강제매도;
            GenieConfig.CB_수익보전_Liquidation_C = 수익보전;
            GenieConfig.CB_Liquidation_TS_C = TS;
            GenieConfig.TB_Liquidation_TS_down_C = TS_down;
            GenieConfig.TB_Liquidation_TS_MinMAPeriod_C = TB_TS_mma;
            GenieConfig.CBB_Liquidation_TS_MinMAPeriod_C = CBB_TS_mma;
            GenieConfig.TB_Liquidation_TS_DayMAPeriod_C = TB_TS_dma;
            GenieConfig.CBB_Liquidation_TS_DayMAPeriod_C = CBB_TS_dma;
        }

        // ---------------------------------------------------------
        // 계좌관리 - 실현손익담보 손실매도 A~C 설정 (Setting.accmgr 사용)
        // ---------------------------------------------------------

        private static void 계좌관리_실현손익담보손실매도_A(bool CB_cut, int MTB_cut_time, double TB_cut_수익금1, double TB_cut_수익금2, double TB_cut_남길퍼, double TB_cut_won,
            double TB_cut_P, double TB_cut_ratio, int CBB_cut_gubun, double TB_cut_value, int CBB_cut_jumun, int MTB_cut_cansel_time)
        {
            GenieConfig.CB_cut_A = CB_cut;
            GenieConfig.MTB_cut_time_A = MTB_cut_time;
            GenieConfig.TB_cut_수익금1_A = TB_cut_수익금1;
            GenieConfig.TB_cut_수익금2_A = TB_cut_수익금2;
            GenieConfig.TB_cut_남길퍼_A = TB_cut_남길퍼;
            GenieConfig.TB_cut_won_A = TB_cut_won;
            GenieConfig.TB_cut_P_A = TB_cut_P;
            GenieConfig.TB_cut_ratio_A = TB_cut_ratio;
            GenieConfig.CBB_cut_gubun_A = CBB_cut_gubun;
            GenieConfig.TB_cut_value_A = TB_cut_value;
            GenieConfig.CBB_cut_jumun_A = CBB_cut_jumun;
            GenieConfig.MTB_cut_cansel_time_A = MTB_cut_cansel_time;
        }

        private static void 계좌관리_실현손익담보손실매도_B(bool CB_cut, int MTB_cut_time, double TB_cut_수익금1, double TB_cut_수익금2, double TB_cut_남길퍼, double TB_cut_won,
            double TB_cut_P, double TB_cut_ratio, int CBB_cut_gubun, double TB_cut_value, int CBB_cut_jumun, int MTB_cut_cansel_time)
        {
            GenieConfig.CB_cut_B = CB_cut;
            GenieConfig.MTB_cut_time_B = MTB_cut_time;
            GenieConfig.TB_cut_수익금1_B = TB_cut_수익금1;
            GenieConfig.TB_cut_수익금2_B = TB_cut_수익금2;
            GenieConfig.TB_cut_남길퍼_B = TB_cut_남길퍼;
            GenieConfig.TB_cut_won_B = TB_cut_won;
            GenieConfig.TB_cut_P_B = TB_cut_P;
            GenieConfig.TB_cut_ratio_B = TB_cut_ratio;
            GenieConfig.CBB_cut_gubun_B = CBB_cut_gubun;
            GenieConfig.TB_cut_value_B = TB_cut_value;
            GenieConfig.CBB_cut_jumun_B = CBB_cut_jumun;
            GenieConfig.MTB_cut_cansel_time_B = MTB_cut_cansel_time;
        }

        private static void 계좌관리_실현손익담보손실매도_C(bool CB_cut, int MTB_cut_time, double TB_cut_수익금1, double TB_cut_수익금2, double TB_cut_남길퍼, double TB_cut_won,
            double TB_cut_P, double TB_cut_ratio, int CBB_cut_gubun, double TB_cut_value, int CBB_cut_jumun, int MTB_cut_cansel_time)
        {
            GenieConfig.CB_cut_C = CB_cut;
            GenieConfig.MTB_cut_time_C = MTB_cut_time;
            GenieConfig.TB_cut_수익금1_C = TB_cut_수익금1;
            GenieConfig.TB_cut_수익금2_C = TB_cut_수익금2;
            GenieConfig.TB_cut_남길퍼_C = TB_cut_남길퍼;
            GenieConfig.TB_cut_won_C = TB_cut_won;
            GenieConfig.TB_cut_P_C = TB_cut_P;
            GenieConfig.TB_cut_ratio_C = TB_cut_ratio;
            GenieConfig.CBB_cut_gubun_C = CBB_cut_gubun;
            GenieConfig.TB_cut_value_C = TB_cut_value;
            GenieConfig.CBB_cut_jumun_C = CBB_cut_jumun;
            GenieConfig.MTB_cut_cansel_time_C = MTB_cut_cansel_time;
        }

    

        // ---------------------------------------------------------
        // 특수 설정 - 신규 그룹 및 매매 기간 (Setting.special 사용)
        // ---------------------------------------------------------

        private static void 특수설정_신규그룹(int A, int B, int C)
        {
            GenieConfig.combo_신규그룹_A = A;
            GenieConfig.combo_신규그룹_B = B;
            GenieConfig.combo_신규그룹_C = C;
        }

        private static void 특수설정_신규그룹(bool 기준금, bool CB_매매기간_오전, int TB_매매기간_오전주문시간, bool CB_매매기간_오후, int TB_매매기간_오후주문시간)
        {
            GenieConfig.CB_매매기간_기준금 = 기준금;
            GenieConfig.CB_매매기간_오전 = CB_매매기간_오전;
            GenieConfig.TB_매매기간_오전주문시간 = TB_매매기간_오전주문시간;
            GenieConfig.CB_매매기간_오후 = CB_매매기간_오후;
            GenieConfig.TB_매매기간_오후주문시간 = TB_매매기간_오후주문시간;
        }

        private static void 특수설정_매매기간주문_A(int trading, int 기간, int 주문시간, double 기준, int 기준방법, double 매도비중, int 매도방법, double 주문가격, int 주문방법, int 취소시간, bool 강제매도, bool 수익보전,
            bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            GenieConfig.CBB_매매기간_trading_A = trading;
            GenieConfig.MTB_매매기간_기간_A = 기간;
            GenieConfig.CBB_매매기간_주문시간_A = 주문시간;
            GenieConfig.TB_매매기간_기준_A = 기준;
            GenieConfig.CBB_매매기간_기준_A = 기준방법;
            GenieConfig.TB_매매기간_ratio_A = 매도비중;
            GenieConfig.CBB_매매기간_choice_A = 매도방법;
            GenieConfig.TB_매매기간_value_A = 주문가격;
            GenieConfig.CBB_매매기간_Jumun_A = 주문방법;
            GenieConfig.TB_매매기간_취소시간_A = 취소시간;
            GenieConfig.CB_매매기간_강제매도_A = 강제매도;
            GenieConfig.CB_매매기간_수익보전_A = 수익보전;
            GenieConfig.CB_매매기간_TS_A = TS;
            GenieConfig.TB_매매기간_TS_down_A = TS_down;
            GenieConfig.TB_매매기간_TS_MinMAPeriod_A = TB_TS_mma;
            GenieConfig.CBB_매매기간_TS_MinMAPeriod_A = CBB_TS_mma;
            GenieConfig.TB_매매기간_TS_DayMAPeriod_A = TB_TS_dma;
            GenieConfig.CBB_매매기간_TS_DayMAPeriod_A = CBB_TS_dma;
        }

        private static void 특수설정_매매기간주문_B(int trading, int 기간, int 주문시간, double 기준, int 기준방법, double 매도비중, int 매도방법, double 주문가격, int 주문방법, int 취소시간, bool 강제매도, bool 수익보전,
            bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            GenieConfig.CBB_매매기간_trading_B = trading;
            GenieConfig.MTB_매매기간_기간_B = 기간;
            GenieConfig.CBB_매매기간_주문시간_B = 주문시간;
            GenieConfig.TB_매매기간_기준_B = 기준;
            GenieConfig.CBB_매매기간_기준_B = 기준방법;
            GenieConfig.TB_매매기간_ratio_B = 매도비중;
            GenieConfig.CBB_매매기간_choice_B = 매도방법;
            GenieConfig.TB_매매기간_value_B = 주문가격;
            GenieConfig.CBB_매매기간_Jumun_B = 주문방법;
            GenieConfig.TB_매매기간_취소시간_B = 취소시간;
            GenieConfig.CB_매매기간_강제매도_B = 강제매도;
            GenieConfig.CB_매매기간_수익보전_B = 수익보전;
            GenieConfig.CB_매매기간_TS_B = TS;
            GenieConfig.TB_매매기간_TS_down_B = TS_down;
            GenieConfig.TB_매매기간_TS_MinMAPeriod_B = TB_TS_mma;
            GenieConfig.CBB_매매기간_TS_MinMAPeriod_B = CBB_TS_mma;
            GenieConfig.TB_매매기간_TS_DayMAPeriod_B = TB_TS_dma;
            GenieConfig.CBB_매매기간_TS_DayMAPeriod_B = CBB_TS_dma;
        }

        private static void 특수설정_매매기간주문_C(int trading, int 기간, int 주문시간, double 기준, int 기준방법, double 매도비중, int 매도방법, double 주문가격, int 주문방법, int 취소시간, bool 강제매도, bool 수익보전,
            bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            GenieConfig.CBB_매매기간_trading_C = trading;
            GenieConfig.MTB_매매기간_기간_C = 기간;
            GenieConfig.CBB_매매기간_주문시간_C = 주문시간;
            GenieConfig.TB_매매기간_기준_C = 기준;
            GenieConfig.CBB_매매기간_기준_C = 기준방법;
            GenieConfig.TB_매매기간_ratio_C = 매도비중;
            GenieConfig.CBB_매매기간_choice_C = 매도방법;
            GenieConfig.TB_매매기간_value_C = 주문가격;
            GenieConfig.CBB_매매기간_Jumun_C = 주문방법;
            GenieConfig.TB_매매기간_취소시간_C = 취소시간;
            GenieConfig.CB_매매기간_강제매도_C = 강제매도;
            GenieConfig.CB_매매기간_수익보전_C = 수익보전;
            GenieConfig.CB_매매기간_TS_C = TS;
            GenieConfig.TB_매매기간_TS_down_C = TS_down;
            GenieConfig.TB_매매기간_TS_MinMAPeriod_C = TB_TS_mma;
            GenieConfig.CBB_매매기간_TS_MinMAPeriod_C = CBB_TS_mma;
            GenieConfig.TB_매매기간_TS_DayMAPeriod_C = TB_TS_dma;
            GenieConfig.CBB_매매기간_TS_DayMAPeriod_C = CBB_TS_dma;
        }

        private static void 특수설정_매매기간주문_D(int trading, int 기간, int 주문시간, double 기준, int 기준방법, double 매도비중, int 매도방법, double 주문가격, int 주문방법, int 취소시간, bool 강제매도, bool 수익보전,
            bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            GenieConfig.CBB_매매기간_trading_D = trading;
            GenieConfig.MTB_매매기간_기간_D = 기간;
            GenieConfig.CBB_매매기간_주문시간_D = 주문시간;
            GenieConfig.TB_매매기간_기준_D = 기준;
            GenieConfig.CBB_매매기간_기준_D = 기준방법;
            GenieConfig.TB_매매기간_ratio_D = 매도비중;
            GenieConfig.CBB_매매기간_choice_D = 매도방법;
            GenieConfig.TB_매매기간_value_D = 주문가격;
            GenieConfig.CBB_매매기간_Jumun_D = 주문방법;
            GenieConfig.TB_매매기간_취소시간_D = 취소시간;
            GenieConfig.CB_매매기간_강제매도_D = 강제매도;
            GenieConfig.CB_매매기간_수익보전_D = 수익보전;
            GenieConfig.CB_매매기간_TS_D = TS;
            GenieConfig.TB_매매기간_TS_down_D = TS_down;
            GenieConfig.TB_매매기간_TS_MinMAPeriod_D = TB_TS_mma;
            GenieConfig.CBB_매매기간_TS_MinMAPeriod_D = CBB_TS_mma;
            GenieConfig.TB_매매기간_TS_DayMAPeriod_D = TB_TS_dma;
            GenieConfig.CBB_매매기간_TS_DayMAPeriod_D = CBB_TS_dma;
        }

        private static void 특수설정_매매기간주문_E(int trading, int 기간, int 주문시간, double 기준, int 기준방법, double 매도비중, int 매도방법, double 주문가격, int 주문방법, int 취소시간, bool 강제매도, bool 수익보전,
            bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            GenieConfig.CBB_매매기간_trading_E = trading;
            GenieConfig.MTB_매매기간_기간_E = 기간;
            GenieConfig.CBB_매매기간_주문시간_E = 주문시간;
            GenieConfig.TB_매매기간_기준_E = 기준;
            GenieConfig.CBB_매매기간_기준_E = 기준방법;
            GenieConfig.TB_매매기간_ratio_E = 매도비중;
            GenieConfig.CBB_매매기간_choice_E = 매도방법;
            GenieConfig.TB_매매기간_value_E = 주문가격;
            GenieConfig.CBB_매매기간_Jumun_E = 주문방법;
            GenieConfig.TB_매매기간_취소시간_E = 취소시간;
            GenieConfig.CB_매매기간_강제매도_E = 강제매도;
            GenieConfig.CB_매매기간_수익보전_E = 수익보전;
            GenieConfig.CB_매매기간_TS_E = TS;
            GenieConfig.TB_매매기간_TS_down_E = TS_down;
            GenieConfig.TB_매매기간_TS_MinMAPeriod_E = TB_TS_mma;
            GenieConfig.CBB_매매기간_TS_MinMAPeriod_E = CBB_TS_mma;
            GenieConfig.TB_매매기간_TS_DayMAPeriod_E = TB_TS_dma;
            GenieConfig.CBB_매매기간_TS_DayMAPeriod_E = CBB_TS_dma;
        }

        private static void 특수설정_매매기간주문_F(int trading, int 기간, int 주문시간, double 기준, int 기준방법, double 매도비중, int 매도방법, double 주문가격, int 주문방법, int 취소시간, bool 강제매도, bool 수익보전,
            bool TS, double TS_down, int TB_TS_mma, int CBB_TS_mma, int TB_TS_dma, int CBB_TS_dma)
        {
            GenieConfig.CBB_매매기간_trading_F = trading;
            GenieConfig.MTB_매매기간_기간_F = 기간;
            GenieConfig.CBB_매매기간_주문시간_F = 주문시간;
            GenieConfig.TB_매매기간_기준_F = 기준;
            GenieConfig.CBB_매매기간_기준_F = 기준방법;
            GenieConfig.TB_매매기간_ratio_F = 매도비중;
            GenieConfig.CBB_매매기간_choice_F = 매도방법;
            GenieConfig.TB_매매기간_value_F = 주문가격;
            GenieConfig.CBB_매매기간_Jumun_F = 주문방법;
            GenieConfig.TB_매매기간_취소시간_F = 취소시간;
            GenieConfig.CB_매매기간_강제매도_F = 강제매도;
            GenieConfig.CB_매매기간_수익보전_F = 수익보전;
            GenieConfig.CB_매매기간_TS_F = TS;
            GenieConfig.TB_매매기간_TS_down_F = TS_down;
            GenieConfig.TB_매매기간_TS_MinMAPeriod_F = TB_TS_mma;
            GenieConfig.CBB_매매기간_TS_MinMAPeriod_F = CBB_TS_mma;
            GenieConfig.TB_매매기간_TS_DayMAPeriod_F = TB_TS_dma;
            GenieConfig.CBB_매매기간_TS_DayMAPeriod_F = CBB_TS_dma;
        }

        // ---------------------------------------------------------
        // 매매 그룹 설정 (Setting.tradegroup 사용)
        // ---------------------------------------------------------

        private static void 매매그룹설정_익절(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_IK_group_A = A;
            GenieConfig.CB_IK_group_B = B;
            GenieConfig.CB_IK_group_C = C;
            GenieConfig.CB_IK_group_D = D;
            GenieConfig.CB_IK_group_E = E;
            GenieConfig.CB_IK_group_F = F;
            GenieConfig.CB_IK_group_G = G;
            GenieConfig.CB_IK_group_H = H;
            GenieConfig.CB_IK_group_I = I;
            GenieConfig.CB_IK_group_J = J;
            GenieConfig.CB_IK_group_K = K;
            GenieConfig.CB_IK_group_L = L;
        }

        private static void 매매그룹설정_손절(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_손절_A = A;
            GenieConfig.CB_손절_B = B;
            GenieConfig.CB_손절_C = C;
            GenieConfig.CB_손절_D = D;
            GenieConfig.CB_손절_E = E;
            GenieConfig.CB_손절_F = F;
            GenieConfig.CB_손절_G = G;
            GenieConfig.CB_손절_H = H;
            GenieConfig.CB_손절_I = I;
            GenieConfig.CB_손절_J = J;
            GenieConfig.CB_손절_K = K;
            GenieConfig.CB_손절_L = L;
        }

        private static void 매매그룹설정_반복A(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_A_A = A;
            GenieConfig.CB_Repeat_A_B = B;
            GenieConfig.CB_Repeat_A_C = C;
            GenieConfig.CB_Repeat_A_D = D;
            GenieConfig.CB_Repeat_A_E = E;
            GenieConfig.CB_Repeat_A_F = F;
            GenieConfig.CB_Repeat_A_G = G;
            GenieConfig.CB_Repeat_A_H = H;
            GenieConfig.CB_Repeat_A_I = I;
            GenieConfig.CB_Repeat_A_J = J;
            GenieConfig.CB_Repeat_A_K = K;
            GenieConfig.CB_Repeat_A_L = L;
        }

        private static void 매매그룹설정_반복B(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_B_A = A;
            GenieConfig.CB_Repeat_B_B = B;
            GenieConfig.CB_Repeat_B_C = C;
            GenieConfig.CB_Repeat_B_D = D;
            GenieConfig.CB_Repeat_B_E = E;
            GenieConfig.CB_Repeat_B_F = F;
            GenieConfig.CB_Repeat_B_G = G;
            GenieConfig.CB_Repeat_B_H = H;
            GenieConfig.CB_Repeat_B_I = I;
            GenieConfig.CB_Repeat_B_J = J;
            GenieConfig.CB_Repeat_B_K = K;
            GenieConfig.CB_Repeat_B_L = L;
        }

        private static void 매매그룹설정_반복C(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_C_A = A;
            GenieConfig.CB_Repeat_C_B = B;
            GenieConfig.CB_Repeat_C_C = C;
            GenieConfig.CB_Repeat_C_D = D;
            GenieConfig.CB_Repeat_C_E = E;
            GenieConfig.CB_Repeat_C_F = F;
            GenieConfig.CB_Repeat_C_G = G;
            GenieConfig.CB_Repeat_C_H = H;
            GenieConfig.CB_Repeat_C_I = I;
            GenieConfig.CB_Repeat_C_J = J;
            GenieConfig.CB_Repeat_C_K = K;
            GenieConfig.CB_Repeat_C_L = L;
        }

        private static void 매매그룹설정_반복D(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_D_A = A;
            GenieConfig.CB_Repeat_D_B = B;
            GenieConfig.CB_Repeat_D_C = C;
            GenieConfig.CB_Repeat_D_D = D;
            GenieConfig.CB_Repeat_D_E = E;
            GenieConfig.CB_Repeat_D_F = F;
            GenieConfig.CB_Repeat_D_G = G;
            GenieConfig.CB_Repeat_D_H = H;
            GenieConfig.CB_Repeat_D_I = I;
            GenieConfig.CB_Repeat_D_J = J;
            GenieConfig.CB_Repeat_D_K = K;
            GenieConfig.CB_Repeat_D_L = L;
        }

        private static void 매매그룹설정_반복E(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_E_A = A;
            GenieConfig.CB_Repeat_E_B = B;
            GenieConfig.CB_Repeat_E_C = C;
            GenieConfig.CB_Repeat_E_D = D;
            GenieConfig.CB_Repeat_E_E = E;
            GenieConfig.CB_Repeat_E_F = F;
            GenieConfig.CB_Repeat_E_G = G;
            GenieConfig.CB_Repeat_E_H = H;
            GenieConfig.CB_Repeat_E_I = I;
            GenieConfig.CB_Repeat_E_J = J;
            GenieConfig.CB_Repeat_E_K = K;
            GenieConfig.CB_Repeat_E_L = L;
        }

        private static void 매매그룹설정_반복F(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_F_A = A;
            GenieConfig.CB_Repeat_F_B = B;
            GenieConfig.CB_Repeat_F_C = C;
            GenieConfig.CB_Repeat_F_D = D;
            GenieConfig.CB_Repeat_F_E = E;
            GenieConfig.CB_Repeat_F_F = F;
            GenieConfig.CB_Repeat_F_G = G;
            GenieConfig.CB_Repeat_F_H = H;
            GenieConfig.CB_Repeat_F_I = I;
            GenieConfig.CB_Repeat_F_J = J;
            GenieConfig.CB_Repeat_F_K = K;
            GenieConfig.CB_Repeat_F_L = L;
        }

        private static void 매매그룹설정_반복G(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_G_A = A;
            GenieConfig.CB_Repeat_G_B = B;
            GenieConfig.CB_Repeat_G_C = C;
            GenieConfig.CB_Repeat_G_D = D;
            GenieConfig.CB_Repeat_G_E = E;
            GenieConfig.CB_Repeat_G_F = F;
            GenieConfig.CB_Repeat_G_G = G;
            GenieConfig.CB_Repeat_G_H = H;
            GenieConfig.CB_Repeat_G_I = I;
            GenieConfig.CB_Repeat_G_J = J;
            GenieConfig.CB_Repeat_G_K = K;
            GenieConfig.CB_Repeat_G_L = L;
        }

        private static void 매매그룹설정_반복H(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_H_A = A;
            GenieConfig.CB_Repeat_H_B = B;
            GenieConfig.CB_Repeat_H_C = C;
            GenieConfig.CB_Repeat_H_D = D;
            GenieConfig.CB_Repeat_H_E = E;
            GenieConfig.CB_Repeat_H_F = F;
            GenieConfig.CB_Repeat_H_G = G;
            GenieConfig.CB_Repeat_H_H = H;
            GenieConfig.CB_Repeat_H_I = I;
            GenieConfig.CB_Repeat_H_J = J;
            GenieConfig.CB_Repeat_H_K = K;
            GenieConfig.CB_Repeat_H_L = L;
        }

        private static void 매매그룹설정_반복I(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_I_A = A;
            GenieConfig.CB_Repeat_I_B = B;
            GenieConfig.CB_Repeat_I_C = C;
            GenieConfig.CB_Repeat_I_D = D;
            GenieConfig.CB_Repeat_I_E = E;
            GenieConfig.CB_Repeat_I_F = F;
            GenieConfig.CB_Repeat_I_G = G;
            GenieConfig.CB_Repeat_I_H = H;
            GenieConfig.CB_Repeat_I_I = I;
            GenieConfig.CB_Repeat_I_J = J;
            GenieConfig.CB_Repeat_I_K = K;
            GenieConfig.CB_Repeat_I_L = L;
        }

        private static void 매매그룹설정_반복J(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_J_A = A;
            GenieConfig.CB_Repeat_J_B = B;
            GenieConfig.CB_Repeat_J_C = C;
            GenieConfig.CB_Repeat_J_D = D;
            GenieConfig.CB_Repeat_J_E = E;
            GenieConfig.CB_Repeat_J_F = F;
            GenieConfig.CB_Repeat_J_G = G;
            GenieConfig.CB_Repeat_J_H = H;
            GenieConfig.CB_Repeat_J_I = I;
            GenieConfig.CB_Repeat_J_J = J;
            GenieConfig.CB_Repeat_J_K = K;
            GenieConfig.CB_Repeat_J_L = L;
        }

        private static void 매매그룹설정_반복K(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_K_A = A;
            GenieConfig.CB_Repeat_K_B = B;
            GenieConfig.CB_Repeat_K_C = C;
            GenieConfig.CB_Repeat_K_D = D;
            GenieConfig.CB_Repeat_K_E = E;
            GenieConfig.CB_Repeat_K_F = F;
            GenieConfig.CB_Repeat_K_G = G;
            GenieConfig.CB_Repeat_K_H = H;
            GenieConfig.CB_Repeat_K_I = I;
            GenieConfig.CB_Repeat_K_J = J;
            GenieConfig.CB_Repeat_K_K = K;
            GenieConfig.CB_Repeat_K_L = L;
        }

        private static void 매매그룹설정_반복L(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_L_A = A;
            GenieConfig.CB_Repeat_L_B = B;
            GenieConfig.CB_Repeat_L_C = C;
            GenieConfig.CB_Repeat_L_D = D;
            GenieConfig.CB_Repeat_L_E = E;
            GenieConfig.CB_Repeat_L_F = F;
            GenieConfig.CB_Repeat_L_G = G;
            GenieConfig.CB_Repeat_L_H = H;
            GenieConfig.CB_Repeat_L_I = I;
            GenieConfig.CB_Repeat_L_J = J;
            GenieConfig.CB_Repeat_L_K = K;
            GenieConfig.CB_Repeat_L_L = L;
        }

        private static void 매매그룹설정_반복M(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_M_A = A;
            GenieConfig.CB_Repeat_M_B = B;
            GenieConfig.CB_Repeat_M_C = C;
            GenieConfig.CB_Repeat_M_D = D;
            GenieConfig.CB_Repeat_M_E = E;
            GenieConfig.CB_Repeat_M_F = F;
            GenieConfig.CB_Repeat_M_G = G;
            GenieConfig.CB_Repeat_M_H = H;
            GenieConfig.CB_Repeat_M_I = I;
            GenieConfig.CB_Repeat_M_J = J;
            GenieConfig.CB_Repeat_M_K = K;
            GenieConfig.CB_Repeat_M_L = L;
        }

        private static void 매매그룹설정_반복N(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Repeat_N_A = A;
            GenieConfig.CB_Repeat_N_B = B;
            GenieConfig.CB_Repeat_N_C = C;
            GenieConfig.CB_Repeat_N_D = D;
            GenieConfig.CB_Repeat_N_E = E;
            GenieConfig.CB_Repeat_N_F = F;
            GenieConfig.CB_Repeat_N_G = G;
            GenieConfig.CB_Repeat_N_H = H;
            GenieConfig.CB_Repeat_N_I = I;
            GenieConfig.CB_Repeat_N_J = J;
            GenieConfig.CB_Repeat_N_K = K;
            GenieConfig.CB_Repeat_N_L = L;
        }

        private static void 매매그룹설정_리밸_A(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_rebalance_A_A = A;
            GenieConfig.CB_rebalance_A_B = B;
            GenieConfig.CB_rebalance_A_C = C;
            GenieConfig.CB_rebalance_A_D = D;
            GenieConfig.CB_rebalance_A_E = E;
            GenieConfig.CB_rebalance_A_F = F;
            GenieConfig.CB_rebalance_A_G = G;
            GenieConfig.CB_rebalance_A_H = H;
            GenieConfig.CB_rebalance_A_I = I;
            GenieConfig.CB_rebalance_A_J = J;
            GenieConfig.CB_rebalance_A_K = K;
            GenieConfig.CB_rebalance_A_L = L;
        }

        private static void 매매그룹설정_리밸_B(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_rebalance_B_A = A;
            GenieConfig.CB_rebalance_B_B = B;
            GenieConfig.CB_rebalance_B_C = C;
            GenieConfig.CB_rebalance_B_D = D;
            GenieConfig.CB_rebalance_B_E = E;
            GenieConfig.CB_rebalance_B_F = F;
            GenieConfig.CB_rebalance_B_G = G;
            GenieConfig.CB_rebalance_B_H = H;
            GenieConfig.CB_rebalance_B_I = I;
            GenieConfig.CB_rebalance_B_J = J;
            GenieConfig.CB_rebalance_B_K = K;
            GenieConfig.CB_rebalance_B_L = L;
        }

        private static void 매매그룹설정_리밸_C(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_rebalance_C_A = A;
            GenieConfig.CB_rebalance_C_B = B;
            GenieConfig.CB_rebalance_C_C = C;
            GenieConfig.CB_rebalance_C_D = D;
            GenieConfig.CB_rebalance_C_E = E;
            GenieConfig.CB_rebalance_C_F = F;
            GenieConfig.CB_rebalance_C_G = G;
            GenieConfig.CB_rebalance_C_H = H;
            GenieConfig.CB_rebalance_C_I = I;
            GenieConfig.CB_rebalance_C_J = J;
            GenieConfig.CB_rebalance_C_K = K;
            GenieConfig.CB_rebalance_C_L = L;
        }

        private static void 매매그룹설정_리밸_D(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_rebalance_D_A = A;
            GenieConfig.CB_rebalance_D_B = B;
            GenieConfig.CB_rebalance_D_C = C;
            GenieConfig.CB_rebalance_D_D = D;
            GenieConfig.CB_rebalance_D_E = E;
            GenieConfig.CB_rebalance_D_F = F;
            GenieConfig.CB_rebalance_D_G = G;
            GenieConfig.CB_rebalance_D_H = H;
            GenieConfig.CB_rebalance_D_I = I;
            GenieConfig.CB_rebalance_D_J = J;
            GenieConfig.CB_rebalance_D_K = K;
            GenieConfig.CB_rebalance_D_L = L;
        }

        private static void 매매그룹설정_리밸_E(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_rebalance_E_A = A;
            GenieConfig.CB_rebalance_E_B = B;
            GenieConfig.CB_rebalance_E_C = C;
            GenieConfig.CB_rebalance_E_D = D;
            GenieConfig.CB_rebalance_E_E = E;
            GenieConfig.CB_rebalance_E_F = F;
            GenieConfig.CB_rebalance_E_G = G;
            GenieConfig.CB_rebalance_E_H = H;
            GenieConfig.CB_rebalance_E_I = I;
            GenieConfig.CB_rebalance_E_J = J;
            GenieConfig.CB_rebalance_E_K = K;
            GenieConfig.CB_rebalance_E_L = L;
        }

        private static void 매매그룹설정_리밸_F(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_rebalance_F_A = A;
            GenieConfig.CB_rebalance_F_B = B;
            GenieConfig.CB_rebalance_F_C = C;
            GenieConfig.CB_rebalance_F_D = D;
            GenieConfig.CB_rebalance_F_E = E;
            GenieConfig.CB_rebalance_F_F = F;
            GenieConfig.CB_rebalance_F_G = G;
            GenieConfig.CB_rebalance_F_H = H;
            GenieConfig.CB_rebalance_F_I = I;
            GenieConfig.CB_rebalance_F_J = J;
            GenieConfig.CB_rebalance_F_K = K;
            GenieConfig.CB_rebalance_F_L = L;
        }

        private static void 매매그룹설정_리밸_G(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_rebalance_G_A = A;
            GenieConfig.CB_rebalance_G_B = B;
            GenieConfig.CB_rebalance_G_C = C;
            GenieConfig.CB_rebalance_G_D = D;
            GenieConfig.CB_rebalance_G_E = E;
            GenieConfig.CB_rebalance_G_F = F;
            GenieConfig.CB_rebalance_G_G = G;
            GenieConfig.CB_rebalance_G_H = H;
            GenieConfig.CB_rebalance_G_I = I;
            GenieConfig.CB_rebalance_G_J = J;
            GenieConfig.CB_rebalance_G_K = K;
            GenieConfig.CB_rebalance_G_L = L;
        }

        private static void 매매그룹설정_청산_A(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Liquidation_A_A = A;
            GenieConfig.CB_Liquidation_A_B = B;
            GenieConfig.CB_Liquidation_A_C = C;
            GenieConfig.CB_Liquidation_A_D = D;
            GenieConfig.CB_Liquidation_A_E = E;
            GenieConfig.CB_Liquidation_A_F = F;
            GenieConfig.CB_Liquidation_A_G = G;
            GenieConfig.CB_Liquidation_A_H = H;
            GenieConfig.CB_Liquidation_A_I = I;
            GenieConfig.CB_Liquidation_A_J = J;
            GenieConfig.CB_Liquidation_A_K = K;
            GenieConfig.CB_Liquidation_A_L = L;
        }

        private static void 매매그룹설정_청산_B(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Liquidation_B_A = A;
            GenieConfig.CB_Liquidation_B_B = B;
            GenieConfig.CB_Liquidation_B_C = C;
            GenieConfig.CB_Liquidation_B_D = D;
            GenieConfig.CB_Liquidation_B_E = E;
            GenieConfig.CB_Liquidation_B_F = F;
            GenieConfig.CB_Liquidation_B_G = G;
            GenieConfig.CB_Liquidation_B_H = H;
            GenieConfig.CB_Liquidation_B_I = I;
            GenieConfig.CB_Liquidation_B_J = J;
            GenieConfig.CB_Liquidation_B_K = K;
            GenieConfig.CB_Liquidation_B_L = L;
        }

        private static void 매매그룹설정_청산_C(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Liquidation_C_A = A;
            GenieConfig.CB_Liquidation_C_B = B;
            GenieConfig.CB_Liquidation_C_C = C;
            GenieConfig.CB_Liquidation_C_D = D;
            GenieConfig.CB_Liquidation_C_E = E;
            GenieConfig.CB_Liquidation_C_F = F;
            GenieConfig.CB_Liquidation_C_G = G;
            GenieConfig.CB_Liquidation_C_H = H;
            GenieConfig.CB_Liquidation_C_I = I;
            GenieConfig.CB_Liquidation_C_J = J;
            GenieConfig.CB_Liquidation_C_K = K;
            GenieConfig.CB_Liquidation_C_L = L;
        }

        private static void 매매그룹설정_기간매도_A(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_day_A_A = A;
            GenieConfig.CB_day_A_B = B;
            GenieConfig.CB_day_A_C = C;
            GenieConfig.CB_day_A_D = D;
            GenieConfig.CB_day_A_E = E;
            GenieConfig.CB_day_A_F = F;
            GenieConfig.CB_day_A_G = G;
            GenieConfig.CB_day_A_H = H;
            GenieConfig.CB_day_A_I = I;
            GenieConfig.CB_day_A_J = J;
            GenieConfig.CB_day_A_K = K;
            GenieConfig.CB_day_A_L = L;
        }

        private static void 매매그룹설정_기간매도_B(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_day_B_A = A;
            GenieConfig.CB_day_B_B = B;
            GenieConfig.CB_day_B_C = C;
            GenieConfig.CB_day_B_D = D;
            GenieConfig.CB_day_B_E = E;
            GenieConfig.CB_day_B_F = F;
            GenieConfig.CB_day_B_G = G;
            GenieConfig.CB_day_B_H = H;
            GenieConfig.CB_day_B_I = I;
            GenieConfig.CB_day_B_J = J;
            GenieConfig.CB_day_B_K = K;
            GenieConfig.CB_day_B_L = L;
        }

        private static void 매매그룹설정_기간매도_C(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_day_C_A = A;
            GenieConfig.CB_day_C_B = B;
            GenieConfig.CB_day_C_C = C;
            GenieConfig.CB_day_C_D = D;
            GenieConfig.CB_day_C_E = E;
            GenieConfig.CB_day_C_F = F;
            GenieConfig.CB_day_C_G = G;
            GenieConfig.CB_day_C_H = H;
            GenieConfig.CB_day_C_I = I;
            GenieConfig.CB_day_C_J = J;
            GenieConfig.CB_day_C_K = K;
            GenieConfig.CB_day_C_L = L;
        }

        private static void 매매그룹설정_기간매도_D(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_day_D_A = A;
            GenieConfig.CB_day_D_B = B;
            GenieConfig.CB_day_D_C = C;
            GenieConfig.CB_day_D_D = D;
            GenieConfig.CB_day_D_E = E;
            GenieConfig.CB_day_D_F = F;
            GenieConfig.CB_day_D_G = G;
            GenieConfig.CB_day_D_H = H;
            GenieConfig.CB_day_D_I = I;
            GenieConfig.CB_day_D_J = J;
            GenieConfig.CB_day_D_K = K;
            GenieConfig.CB_day_D_L = L;
        }

        private static void 매매그룹설정_기간매도_E(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_day_E_A = A;
            GenieConfig.CB_day_E_B = B;
            GenieConfig.CB_day_E_C = C;
            GenieConfig.CB_day_E_D = D;
            GenieConfig.CB_day_E_E = E;
            GenieConfig.CB_day_E_F = F;
            GenieConfig.CB_day_E_G = G;
            GenieConfig.CB_day_E_H = H;
            GenieConfig.CB_day_E_I = I;
            GenieConfig.CB_day_E_J = J;
            GenieConfig.CB_day_E_K = K;
            GenieConfig.CB_day_E_L = L;
        }

        private static void 매매그룹설정_기간매도_F(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_day_F_A = A;
            GenieConfig.CB_day_F_B = B;
            GenieConfig.CB_day_F_C = C;
            GenieConfig.CB_day_F_D = D;
            GenieConfig.CB_day_F_E = E;
            GenieConfig.CB_day_F_F = F;
            GenieConfig.CB_day_F_G = G;
            GenieConfig.CB_day_F_H = H;
            GenieConfig.CB_day_F_I = I;
            GenieConfig.CB_day_F_J = J;
            GenieConfig.CB_day_F_K = K;
            GenieConfig.CB_day_F_L = L;
        }

        private static void 매매그룹설정_미수금정리(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_미수금정리_A = A;
            GenieConfig.CB_미수금정리_B = B;
            GenieConfig.CB_미수금정리_C = C;
            GenieConfig.CB_미수금정리_D = D;
            GenieConfig.CB_미수금정리_E = E;
            GenieConfig.CB_미수금정리_F = F;
            GenieConfig.CB_미수금정리_G = G;
            GenieConfig.CB_미수금정리_H = H;
            GenieConfig.CB_미수금정리_I = I;
            GenieConfig.CB_미수금정리_J = J;
            GenieConfig.CB_미수금정리_K = K;
            GenieConfig.CB_미수금정리_L = L;
        }

        private static void 매매그룹설정_손익담보매도_A(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Cut_A_A = A;
            GenieConfig.CB_Cut_A_B = B;
            GenieConfig.CB_Cut_A_C = C;
            GenieConfig.CB_Cut_A_D = D;
            GenieConfig.CB_Cut_A_E = E;
            GenieConfig.CB_Cut_A_F = F;
            GenieConfig.CB_Cut_A_G = G;
            GenieConfig.CB_Cut_A_H = H;
            GenieConfig.CB_Cut_A_I = I;
            GenieConfig.CB_Cut_A_J = J;
            GenieConfig.CB_Cut_A_K = K;
            GenieConfig.CB_Cut_A_L = L;
        }

        private static void 매매그룹설정_손익담보매도_B(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Cut_B_A = A;
            GenieConfig.CB_Cut_B_B = B;
            GenieConfig.CB_Cut_B_C = C;
            GenieConfig.CB_Cut_B_D = D;
            GenieConfig.CB_Cut_B_E = E;
            GenieConfig.CB_Cut_B_F = F;
            GenieConfig.CB_Cut_B_G = G;
            GenieConfig.CB_Cut_B_H = H;
            GenieConfig.CB_Cut_B_I = I;
            GenieConfig.CB_Cut_B_J = J;
            GenieConfig.CB_Cut_B_K = K;
            GenieConfig.CB_Cut_B_L = L;
        }

        private static void 매매그룹설정_손익담보매도_C(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_Cut_C_A = A;
            GenieConfig.CB_Cut_C_B = B;
            GenieConfig.CB_Cut_C_C = C;
            GenieConfig.CB_Cut_C_D = D;
            GenieConfig.CB_Cut_C_E = E;
            GenieConfig.CB_Cut_C_F = F;
            GenieConfig.CB_Cut_C_G = G;
            GenieConfig.CB_Cut_C_H = H;
            GenieConfig.CB_Cut_C_I = I;
            GenieConfig.CB_Cut_C_J = J;
            GenieConfig.CB_Cut_C_K = K;
            GenieConfig.CB_Cut_C_L = L;
        }

        private static void 매매그룹설정_계좌청산_특정시간(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_특정시간_계좌청산_A = A;
            GenieConfig.CB_특정시간_계좌청산_B = B;
            GenieConfig.CB_특정시간_계좌청산_C = C;
            GenieConfig.CB_특정시간_계좌청산_D = D;
            GenieConfig.CB_특정시간_계좌청산_E = E;
            GenieConfig.CB_특정시간_계좌청산_F = F;
            GenieConfig.CB_특정시간_계좌청산_G = G;
            GenieConfig.CB_특정시간_계좌청산_H = H;
            GenieConfig.CB_특정시간_계좌청산_I = I;
            GenieConfig.CB_특정시간_계좌청산_J = J;
            GenieConfig.CB_특정시간_계좌청산_K = K;
            GenieConfig.CB_특정시간_계좌청산_L = L;
        }

        private static void 매매그룹설정_계좌청산_실현손익(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_실현손익_계좌청산_A = A;
            GenieConfig.CB_실현손익_계좌청산_B = B;
            GenieConfig.CB_실현손익_계좌청산_C = C;
            GenieConfig.CB_실현손익_계좌청산_D = D;
            GenieConfig.CB_실현손익_계좌청산_E = E;
            GenieConfig.CB_실현손익_계좌청산_F = F;
            GenieConfig.CB_실현손익_계좌청산_G = G;
            GenieConfig.CB_실현손익_계좌청산_H = H;
            GenieConfig.CB_실현손익_계좌청산_I = I;
            GenieConfig.CB_실현손익_계좌청산_J = J;
            GenieConfig.CB_실현손익_계좌청산_K = K;
            GenieConfig.CB_실현손익_계좌청산_L = L;
        }

        private static void 매매그룹설정_계좌청산_예상손실(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_예상손실_계좌청산_A = A;
            GenieConfig.CB_예상손실_계좌청산_B = B;
            GenieConfig.CB_예상손실_계좌청산_C = C;
            GenieConfig.CB_예상손실_계좌청산_D = D;
            GenieConfig.CB_예상손실_계좌청산_E = E;
            GenieConfig.CB_예상손실_계좌청산_F = F;
            GenieConfig.CB_예상손실_계좌청산_G = G;
            GenieConfig.CB_예상손실_계좌청산_H = H;
            GenieConfig.CB_예상손실_계좌청산_I = I;
            GenieConfig.CB_예상손실_계좌청산_J = J;
            GenieConfig.CB_예상손실_계좌청산_K = K;
            GenieConfig.CB_예상손실_계좌청산_L = L;
        }

        private static void 매매그룹설정_계좌청산_예상수익(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_예상수익_계좌청산_A = A;
            GenieConfig.CB_예상수익_계좌청산_B = B;
            GenieConfig.CB_예상수익_계좌청산_C = C;
            GenieConfig.CB_예상수익_계좌청산_D = D;
            GenieConfig.CB_예상수익_계좌청산_E = E;
            GenieConfig.CB_예상수익_계좌청산_F = F;
            GenieConfig.CB_예상수익_계좌청산_G = G;
            GenieConfig.CB_예상수익_계좌청산_H = H;
            GenieConfig.CB_예상수익_계좌청산_I = I;
            GenieConfig.CB_예상수익_계좌청산_J = J;
            GenieConfig.CB_예상수익_계좌청산_K = K;
            GenieConfig.CB_예상수익_계좌청산_L = L;
        }

        private static void 매매그룹설정_시간청산_A(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_시간청산A_A = A;
            GenieConfig.CB_시간청산A_B = B;
            GenieConfig.CB_시간청산A_C = C;
            GenieConfig.CB_시간청산A_D = D;
            GenieConfig.CB_시간청산A_E = E;
            GenieConfig.CB_시간청산A_F = F;
            GenieConfig.CB_시간청산A_G = G;
            GenieConfig.CB_시간청산A_H = H;
            GenieConfig.CB_시간청산A_I = I;
            GenieConfig.CB_시간청산A_J = J;
            GenieConfig.CB_시간청산A_K = K;
            GenieConfig.CB_시간청산A_L = L;
        }

        private static void 매매그룹설정_시간청산_B(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_시간청산B_A = A;
            GenieConfig.CB_시간청산B_B = B;
            GenieConfig.CB_시간청산B_C = C;
            GenieConfig.CB_시간청산B_D = D;
            GenieConfig.CB_시간청산B_E = E;
            GenieConfig.CB_시간청산B_F = F;
            GenieConfig.CB_시간청산B_G = G;
            GenieConfig.CB_시간청산B_H = H;
            GenieConfig.CB_시간청산B_I = I;
            GenieConfig.CB_시간청산B_J = J;
            GenieConfig.CB_시간청산B_K = K;
            GenieConfig.CB_시간청산B_L = L;
        }

        private static void 매매그룹설정_시간청산_C(bool A, bool B, bool C, bool D, bool E, bool F, bool G, bool H, bool I, bool J, bool K, bool L)
        {
            GenieConfig.CB_시간청산C_A = A;
            GenieConfig.CB_시간청산C_B = B;
            GenieConfig.CB_시간청산C_C = C;
            GenieConfig.CB_시간청산C_D = D;
            GenieConfig.CB_시간청산C_E = E;
            GenieConfig.CB_시간청산C_F = F;
            GenieConfig.CB_시간청산C_G = G;
            GenieConfig.CB_시간청산C_H = H;
            GenieConfig.CB_시간청산C_I = I;
            GenieConfig.CB_시간청산C_J = J;
            GenieConfig.CB_시간청산C_K = K;
            GenieConfig.CB_시간청산C_L = L;
        }


        // ---------------------------------------------------------
        // 대금 탐색 설정 (Setting.pricesearch 사용)
        // ---------------------------------------------------------

        private static void 대금탐색_누적거래대금(int 거래대금)
        {
            GenieConfig.TB_accumulate_Price = 거래대금;
        }

        private static void 대금탐색_매수_A(int 잔량, double 매도호가별대금, double 매도호가합대금, double 매수호가별대금, double 매수호가합대금,
            bool 매수탐색사용, int 기준초, int Combo_기준초회, double 탐색등락률, int 상승카운터, bool 상승옵션, int 하락카운터, bool 하락옵션,
            int 탐색주가_1, int 탐색주가_2, int 탐색주가_3, int 탐색주가_4, int 탐색주가_5, int 탐색주가_6,
            int 탐색대금_1, int 탐색대금_2, int 탐색대금_3, int 탐색대금_4, int 탐색대금_5, int 탐색대금_6,
            int 반복, int 분봉, int 일봉)
        {
            GenieConfig.CBB_M_잔량 = 잔량;
            GenieConfig.TB_M_매도호가별대금 = 매도호가별대금;
            GenieConfig.TB_M_매도호가합대금 = 매도호가합대금;
            GenieConfig.TB_M_매수호가별대금 = 매수호가별대금;
            GenieConfig.TB_M_매수호가합대금 = 매수호가합대금;

            GenieConfig.CB_매수탐색A = 매수탐색사용;

            GenieConfig.TB_Buy_A_기준초 = 기준초;
            GenieConfig.Combo_Buy_A_초회 = Combo_기준초회;
            GenieConfig.TB_Buy_A_탐색rate = 탐색등락률;
            GenieConfig.TB_Buy_상승카운터_A = 상승카운터;
            GenieConfig.CB_Buy_상승옵션_A = 상승옵션;
            GenieConfig.TB_Buy_하락카운터_A = 하락카운터;
            GenieConfig.CB_Buy_하락옵션_A = 하락옵션;

            GenieConfig.TB_Buy_A_탐색주가_1 = 탐색주가_1;
            GenieConfig.TB_Buy_A_탐색주가_2 = 탐색주가_2;
            GenieConfig.TB_Buy_A_탐색주가_3 = 탐색주가_3;
            GenieConfig.TB_Buy_A_탐색주가_4 = 탐색주가_4;
            GenieConfig.TB_Buy_A_탐색주가_5 = 탐색주가_5;
            GenieConfig.TB_Buy_A_탐색주가_6 = 탐색주가_6;

            GenieConfig.TB_Buy_A_탐색대금_1 = 탐색대금_1;
            GenieConfig.TB_Buy_A_탐색대금_2 = 탐색대금_2;
            GenieConfig.TB_Buy_A_탐색대금_3 = 탐색대금_3;
            GenieConfig.TB_Buy_A_탐색대금_4 = 탐색대금_4;
            GenieConfig.TB_Buy_A_탐색대금_5 = 탐색대금_5;
            GenieConfig.TB_Buy_A_탐색대금_6 = 탐색대금_6;

            GenieConfig.MTB_M_반복 = 반복;
            GenieConfig.CBB_Buy_A_분봉 = 분봉;
            GenieConfig.CBB_Buy_A_일봉 = 일봉;
        }

        private static void 대금탐색_매수_B(int 잔량, double 매도호가별대금, double 매도호가합대금, double 매수호가별대금, double 매수호가합대금,
            bool 매수탐색사용, int 기준초, int Combo_기준초회, double 탐색등락률, int 상승카운터, bool 상승옵션, int 하락카운터, bool 하락옵션,
            int 탐색주가_1, int 탐색주가_2, int 탐색주가_3, int 탐색주가_4, int 탐색주가_5, int 탐색주가_6,
            int 탐색대금_1, int 탐색대금_2, int 탐색대금_3, int 탐색대금_4, int 탐색대금_5, int 탐색대금_6,
            int 반복, int 분봉, int 일봉)
        {
            GenieConfig.CBB_M_잔량_2 = 잔량;
            GenieConfig.TB_M_매도호가별대금_2 = 매도호가별대금;
            GenieConfig.TB_M_매도호가합대금_2 = 매도호가합대금;
            GenieConfig.TB_M_매수호가별대금_2 = 매수호가별대금;
            GenieConfig.TB_M_매수호가합대금_2 = 매수호가합대금;

            GenieConfig.CB_매수탐색B = 매수탐색사용;

            GenieConfig.TB_Buy_B_기준초 = 기준초;
            GenieConfig.Combo_Buy_B_초회 = Combo_기준초회;
            GenieConfig.TB_Buy_B_탐색rate = 탐색등락률;
            GenieConfig.TB_Buy_상승카운터_B = 상승카운터;
            GenieConfig.CB_Buy_상승옵션_B = 상승옵션;
            GenieConfig.TB_Buy_하락카운터_B = 하락카운터;
            GenieConfig.CB_Buy_하락옵션_B = 하락옵션;

            GenieConfig.TB_Buy_B_탐색주가_1 = 탐색주가_1;
            GenieConfig.TB_Buy_B_탐색주가_2 = 탐색주가_2;
            GenieConfig.TB_Buy_B_탐색주가_3 = 탐색주가_3;
            GenieConfig.TB_Buy_B_탐색주가_4 = 탐색주가_4;
            GenieConfig.TB_Buy_B_탐색주가_5 = 탐색주가_5;
            GenieConfig.TB_Buy_B_탐색주가_6 = 탐색주가_6;

            GenieConfig.TB_Buy_B_탐색대금_1 = 탐색대금_1;
            GenieConfig.TB_Buy_B_탐색대금_2 = 탐색대금_2;
            GenieConfig.TB_Buy_B_탐색대금_3 = 탐색대금_3;
            GenieConfig.TB_Buy_B_탐색대금_4 = 탐색대금_4;
            GenieConfig.TB_Buy_B_탐색대금_5 = 탐색대금_5;
            GenieConfig.TB_Buy_B_탐색대금_6 = 탐색대금_6;

            GenieConfig.MTB_M_반복_2 = 반복;
            GenieConfig.CBB_Buy_B_분봉 = 분봉;
            GenieConfig.CBB_Buy_B_일봉 = 일봉;
        }

        private static void 대금탐색_매도(bool 매도탐색사용, int 기준초, int Combo_기준초회, double 탐색등락률, int 상승카운터, bool 상승옵션, int 하락카운터, bool 하락옵션,
            int 탐색주가_1, int 탐색주가_2, int 탐색주가_3, int 탐색주가_4, int 탐색주가_5, int 탐색주가_6,
            int 탐색대금_1, int 탐색대금_2, int 탐색대금_3, int 탐색대금_4, int 탐색대금_5, int 탐색대금_6,
            int 분봉, int 일봉)
        {
            GenieConfig.CB_매도탐색 = 매도탐색사용;

            GenieConfig.TB_Sell_기준초 = 기준초;
            GenieConfig.combo_Sell_초회 = Combo_기준초회;
            GenieConfig.TB_Sell_탐색rate = 탐색등락률;
            GenieConfig.TB_Sell_상승카운터 = 상승카운터;
            GenieConfig.CB_Sell_상승옵션 = 상승옵션;
            GenieConfig.TB_Sell_하락카운터 = 하락카운터;
            GenieConfig.CB_Sell_하락옵션 = 하락옵션;

            GenieConfig.TB_Sell_탐색주가_1 = 탐색주가_1;
            GenieConfig.TB_Sell_탐색주가_2 = 탐색주가_2;
            GenieConfig.TB_Sell_탐색주가_3 = 탐색주가_3;
            GenieConfig.TB_Sell_탐색주가_4 = 탐색주가_4;
            GenieConfig.TB_Sell_탐색주가_5 = 탐색주가_5;
            GenieConfig.TB_Sell_탐색주가_6 = 탐색주가_6;

            GenieConfig.TB_Sell_탐색대금_1 = 탐색대금_1;
            GenieConfig.TB_Sell_탐색대금_2 = 탐색대금_2;
            GenieConfig.TB_Sell_탐색대금_3 = 탐색대금_3;
            GenieConfig.TB_Sell_탐색대금_4 = 탐색대금_4;
            GenieConfig.TB_Sell_탐색대금_5 = 탐색대금_5;
            GenieConfig.TB_Sell_탐색대금_6 = 탐색대금_6;

            GenieConfig.CBB_Sell_탐색_분봉 = 분봉;
            GenieConfig.CBB_Sell_탐색_일봉 = 일봉;
        }
        // ---------------------------------------------------------
        // 기능 설정 값 (Setting.function 사용)
        // ---------------------------------------------------------

        //        private static void 기능설정값(bool 편입추가, bool 최종가업데이트, bool 신규매수정지, bool 추가매수정지, bool VI매수취소, bool VI매도취소,
        //        bool 상매수취소, bool 하매도취소, bool 상전량청산, bool 하전량청산, bool ETF매입비제외, bool CB_중간가주문, bool NXT, bool NXT_매수금지, bool NXT_손실제한)

        private static void 기능설정값(bool 편입추가, bool 최종가업데이트, bool 신규매수정지, bool 추가매수정지, bool VI매수취소, bool VI매도취소,
            bool 상매수취소, bool 하매도취소, bool 상전량청산, bool 하전량청산, bool ETF매입비제외, bool CB_중간가주문, bool NXT, bool NXT_매수금지, bool NXT_손실제한)
        {
            // [관심 그룹 초기화 로직 (기존 유지)]
            Tab_InterestGroup.그룹변경("신규_A", "기본값");
            Tab_InterestGroup.그룹변경("신규_B", "기본값");
            Tab_InterestGroup.그룹변경("신규_C", "기본값");

            // (반복 A~N)
            Tab_InterestGroup.그룹변경("반복_A", "기본값");
            Tab_InterestGroup.그룹변경("반복_B", "기본값");
            Tab_InterestGroup.그룹변경("반복_C", "기본값");
            Tab_InterestGroup.그룹변경("반복_D", "기본값");
            Tab_InterestGroup.그룹변경("반복_E", "기본값");
            Tab_InterestGroup.그룹변경("반복_F", "기본값");
            Tab_InterestGroup.그룹변경("반복_G", "기본값");
            Tab_InterestGroup.그룹변경("반복_H", "기본값");
            Tab_InterestGroup.그룹변경("반복_I", "기본값");
            Tab_InterestGroup.그룹변경("반복_J", "기본값");
            Tab_InterestGroup.그룹변경("반복_K", "기본값");
            Tab_InterestGroup.그룹변경("반복_L", "기본값");
            Tab_InterestGroup.그룹변경("반복_M", "기본값");
            Tab_InterestGroup.그룹변경("반복_N", "기본값");

            // (리밸 A~G)
            Tab_InterestGroup.그룹변경("리밸_A", "기본값");
            Tab_InterestGroup.그룹변경("리밸_B", "기본값");
            Tab_InterestGroup.그룹변경("리밸_C", "기본값");
            Tab_InterestGroup.그룹변경("리밸_D", "기본값");
            Tab_InterestGroup.그룹변경("리밸_E", "기본값");
            Tab_InterestGroup.그룹변경("리밸_F", "기본값");
            Tab_InterestGroup.그룹변경("리밸_G", "기본값");

            // (청산 A~C)
            Tab_InterestGroup.그룹변경("청산_A", "기본값");
            Tab_InterestGroup.그룹변경("청산_B", "기본값");
            Tab_InterestGroup.그룹변경("청산_C", "기본값");

            // (기간 A~F)
            Tab_InterestGroup.그룹변경("기간_A", "기본값");
            Tab_InterestGroup.그룹변경("기간_B", "기본값");
            Tab_InterestGroup.그룹변경("기간_C", "기본값");
            Tab_InterestGroup.그룹변경("기간_D", "기본값");
            Tab_InterestGroup.그룹변경("기간_E", "기본값");
            Tab_InterestGroup.그룹변경("기간_F", "기본값");

            Tab_InterestGroup.매매관심그룹리스트확인();
            SaveToFile.관심그룹_Title_파일저장(Form1.로딩완료);

            if (!GenieConfig.CB_기본매매변경)
            {
                // [UI 업데이트]
                Form_Function.form.CB_편입추가.Checked = 편입추가;
                Form_Function.form.CB_최종가업데이트.Checked = 최종가업데이트;
                Form_Function.form.CB_신규매수정지.Checked = 신규매수정지;
                Form_Function.form.CB_추가매수정지.Checked = 추가매수정지;
                Form_Function.form.CB_VI매수취소.Checked = VI매수취소;
                Form_Function.form.CB_VI매도취소.Checked = VI매도취소;
                Form_Function.form.CB_상매수취소.Checked = 상매수취소;
                Form_Function.form.CB_하매도취소.Checked = 하매도취소;
                Form_Function.form.CB_상전량청산.Checked = 상전량청산;
                Form_Function.form.CB_하전량청산.Checked = 하전량청산;
                Form_Function.form.CB_중간가주문.Checked = CB_중간가주문;
                Form_Function.form.CB_NXT.Checked = NXT;
                Form_Function.form.CB_ETF매입비제외.Checked = ETF매입비제외;

                // [설정값 저장] (Properties.Settings -> Setting.function)
                GenieConfig.CB_편입추가 = 편입추가;
                GenieConfig.CB_최종가업데이트 = 최종가업데이트;
                GenieConfig.CB_신규매수정지 = 신규매수정지;
                GenieConfig.CB_추가매수정지 = 추가매수정지;
                GenieConfig.CB_VI매수취소 = VI매수취소;
                GenieConfig.CB_VI매도취소 = VI매도취소;
                GenieConfig.CB_상매수취소 = 상매수취소;
                GenieConfig.CB_하매도취소 = 하매도취소;
                GenieConfig.CB_상전량청산 = 상전량청산;
                GenieConfig.CB_하전량청산 = 하전량청산;
                GenieConfig.CB_중간가주문 = CB_중간가주문;
                GenieConfig.CB_ETF매입비제외 = ETF매입비제외;

                GenieConfig.CB_NXT = NXT;
                GenieConfig.CB_NXT_매수금지 = NXT_매수금지;
                GenieConfig.CB_NXT_손실제한 = NXT_손실제한;
            }
        }

        public static void 가이드매매저장()
        {
            string 체크박스(bool 체크) { if (체크) return "true"; else return "false"; }
            Form1.Console_print("####################### 가이드매매저장 #######################");
            계좌설정();
            기본매매설정();
            반복매매설정();
            계좌관리설정();
            특수설정();
            매매그룹설정();
            대금탐색설정();
            기능설정();

            // ---------------------------------------------------------
            // 전체 계좌 설정 스크립트 생성 (Setting 클래스 통합)
            // ---------------------------------------------------------
            void 계좌설정()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                // 1. [계좌설정_] 
                sb.AppendLine("계좌설정_(");
                sb.AppendLine($"    최대잔고: {GenieConfig.TB_setjango},");
                sb.AppendLine($"    매수제한: {체크박스(GenieConfig.CB_계좌매입비_매수제한)},");
                sb.AppendLine($"    매수제한매입비: {GenieConfig.TB_계좌매입비_제한비중},");
                sb.AppendLine($"    추매제한: {체크박스(GenieConfig.CB_잔고매입비_추매제한)},");
                sb.AppendLine($"    추매제한매입비: {GenieConfig.TB_잔고매입비_추매제한},");
                sb.AppendLine($"    시작시간: {GenieConfig.MT_starttime},");
                sb.AppendLine($"    종료시간: {GenieConfig.MT_stoptime},");
                sb.AppendLine($"    매수사용법: {GenieConfig.CBB_계좌매입비_제한선택},");
                sb.AppendLine($"    지수연동_신규: {GenieConfig.CBB_지수연동_신규},");
                sb.AppendLine($"    지수연동_추매: {GenieConfig.CBB_지수연동_추매}");
                sb.AppendLine(");\n");

                // 2. [계좌설정_코스피]
                sb.AppendLine("계좌설정_코스피(");
                sb.AppendLine($"    등락률: {GenieConfig.TB_p_ratio_use},");
                sb.AppendLine($"    등락률상하: {GenieConfig.combo_p_ratio_UD},");
                sb.AppendLine($"    등락률사용법: {GenieConfig.combo_p_ratio},");
                sb.AppendLine($"    고가대비등락률: {GenieConfig.TB_p_go_use},");
                sb.AppendLine($"    고가대비상하: {GenieConfig.combo_p_go_UD},");
                sb.AppendLine($"    고가대비사용법: {GenieConfig.combo_p_go},");
                sb.AppendLine($"    저가대비등락률: {GenieConfig.TB_p_down_use},");
                sb.AppendLine($"    저가대비상하: {GenieConfig.combo_p_down_UD},");
                sb.AppendLine($"    저가대비사용법: {GenieConfig.combo_p_down}");
                sb.AppendLine(");\n");

                // 3. [계좌설정_코스닥]
                sb.AppendLine("계좌설정_코스닥(");
                sb.AppendLine($"    등락률: {GenieConfig.TB_q_ratio_use},");
                sb.AppendLine($"    등락률상하: {GenieConfig.combo_q_ratio_UD},");
                sb.AppendLine($"    등락률사용법: {GenieConfig.combo_q_ratio},");
                sb.AppendLine($"    고가대비등락률: {GenieConfig.TB_q_go_use},");
                sb.AppendLine($"    고가대비상하: {GenieConfig.combo_q_go_UD},");
                sb.AppendLine($"    고가대비사용법: {GenieConfig.combo_q_go},");
                sb.AppendLine($"    저가대비등락률: {GenieConfig.TB_q_down_use},");
                sb.AppendLine($"    저가대비상하: {GenieConfig.combo_q_down_UD},");
                sb.AppendLine($"    저가대비사용법: {GenieConfig.combo_q_down}");
                sb.AppendLine(");\n");

                // 4. [계좌설정_미수사용]
                sb.AppendLine("계좌설정_미수사용(");
                sb.AppendLine($"    사용: {체크박스(GenieConfig.CB_misu)},");
                sb.AppendLine($"    정리시간: {GenieConfig.MT_misu_time},");
                sb.AppendLine($"    사용법: {GenieConfig.Combo_misu},");
                sb.AppendLine($"    회당주문금액: {GenieConfig.TB_misu_ratio},");
                sb.AppendLine($"    주문값: {GenieConfig.TB_misu_value},");
                sb.AppendLine($"    주문방법: {GenieConfig.Combo_misu_jumnun},");
                sb.AppendLine($"    반복시간: {GenieConfig.TB_misu_repeat_time}");
                sb.AppendLine(");\n");

                // ==========================================
                // 5. [계좌설정_이평선] - 등록된 모든 그룹 출력
                // ==========================================

                // 만약 저장된 그룹이 하나도 없다면 예외 처리를 위해 비어있는지 확인합니다.
                if (GenieConfig.그룹별_지수설정 != null && GenieConfig.그룹별_지수설정.Count > 0)
                {
                    foreach (KeyValuePair<string, Form_Jisu.지수설정_세트> 그룹 in GenieConfig.그룹별_지수설정)
                    {
                        string 그룹명 = 그룹.Key;
                        Form_Jisu.지수설정_세트 설정값 = 그룹.Value;

                        sb.AppendLine("계좌설정_이평선(");
                        sb.AppendLine($"    // [+] 대상 그룹명: {그룹명}");
                        sb.AppendLine($"    대상그룹명: \"{그룹명}\",");
                        sb.AppendLine();

                        sb.AppendLine("    // ================= [ 코스피(KOSPI) 이평선 설정 ] =================");
                        sb.AppendLine($"    코스피_사용여부: {체크박스(설정값.지수이평_사용_kospi)},");

                        // 분봉 
                        sb.AppendLine($"    use_kospi_min_03: {체크박스(설정값.Use_kospi_min_03)}, use_kospi_min_05: {체크박스(설정값.Use_kospi_min_05)}, use_kospi_min_10: {체크박스(설정값.Use_kospi_min_10)},");
                        sb.AppendLine($"    use_kospi_min_20: {체크박스(설정값.Use_kospi_min_20)}, use_kospi_min_30: {체크박스(설정값.Use_kospi_min_30)}, use_kospi_min_60: {체크박스(설정값.Use_kospi_min_60)},");

                        // 일봉
                        sb.AppendLine($"    use_kospi_day_03: {체크박스(설정값.Use_kospi_day_03)}, use_kospi_day_05: {체크박스(설정값.Use_kospi_day_05)}, use_kospi_day_10: {체크박스(설정값.Use_kospi_day_10)},");
                        sb.AppendLine($"    use_kospi_day_20: {체크박스(설정값.Use_kospi_day_20)}, use_kospi_day_40: {체크박스(설정값.Use_kospi_day_40)}, use_kospi_day_60: {체크박스(설정값.Use_kospi_day_60)},");

                        // 분봉 UD
                        sb.AppendLine($"    UD_kospi_min_03: {체크박스(설정값.UD_kospi_min_03)}, UD_kospi_min_05: {체크박스(설정값.UD_kospi_min_05)}, UD_kospi_min_10: {체크박스(설정값.UD_kospi_min_10)},");
                        sb.AppendLine($"    UD_kospi_min_20: {체크박스(설정값.UD_kospi_min_20)}, UD_kospi_min_30: {체크박스(설정값.UD_kospi_min_30)}, UD_kospi_min_60: {체크박스(설정값.UD_kospi_min_60)},");

                        // 일봉 UD
                        sb.AppendLine($"    UD_kospi_day_03: {체크박스(설정값.UD_kospi_day_03)}, UD_kospi_day_05: {체크박스(설정값.UD_kospi_day_05)}, UD_kospi_day_10: {체크박스(설정값.UD_kospi_day_10)},");
                        sb.AppendLine($"    UD_kospi_day_20: {체크박스(설정값.UD_kospi_day_20)}, UD_kospi_day_40: {체크박스(설정값.UD_kospi_day_40)}, UD_kospi_day_60: {체크박스(설정값.UD_kospi_day_60)},");

                        sb.AppendLine();
                        sb.AppendLine("    // ================= [ 코스닥(KOSDAQ) 이평선 설정 ] =================");
                        sb.AppendLine($"    코스닥_사용여부: {체크박스(설정값.지수이평_사용_kosdaq)},");

                        // 분봉
                        sb.AppendLine($"    use_kosdaq_min_03: {체크박스(설정값.Use_kosdaq_min_03)}, use_kosdaq_min_05: {체크박스(설정값.Use_kosdaq_min_05)}, use_kosdaq_min_10: {체크박스(설정값.Use_kosdaq_min_10)},");
                        sb.AppendLine($"    use_kosdaq_min_20: {체크박스(설정값.Use_kosdaq_min_20)}, use_kosdaq_min_30: {체크박스(설정값.Use_kosdaq_min_30)}, use_kosdaq_min_60: {체크박스(설정값.Use_kosdaq_min_60)},");

                        // 일봉
                        sb.AppendLine($"    use_kosdaq_day_03: {체크박스(설정값.Use_kosdaq_day_03)}, use_kosdaq_day_05: {체크박스(설정값.Use_kosdaq_day_05)}, use_kosdaq_day_10: {체크박스(설정값.Use_kosdaq_day_10)},");
                        sb.AppendLine($"    use_kosdaq_day_20: {체크박스(설정값.Use_kosdaq_day_20)}, use_kosdaq_day_40: {체크박스(설정값.Use_kosdaq_day_40)}, use_kosdaq_day_60: {체크박스(설정값.Use_kosdaq_day_60)},");

                        // 분봉 UD
                        sb.AppendLine($"    UD_kosdaq_min_03: {체크박스(설정값.UD_kosdaq_min_03)}, UD_kosdaq_min_05: {체크박스(설정값.UD_kosdaq_min_05)}, UD_kosdaq_min_10: {체크박스(설정값.UD_kosdaq_min_10)},");
                        sb.AppendLine($"    UD_kosdaq_min_20: {체크박스(설정값.UD_kosdaq_min_20)}, UD_kosdaq_min_30: {체크박스(설정값.UD_kosdaq_min_30)}, UD_kosdaq_min_60: {체크박스(설정값.UD_kosdaq_min_60)},");

                        // 일봉 UD
                        sb.AppendLine($"    UD_kosdaq_day_03: {체크박스(설정값.UD_kosdaq_day_03)}, UD_kosdaq_day_05: {체크박스(설정값.UD_kosdaq_day_05)}, UD_kosdaq_day_10: {체크박스(설정값.UD_kosdaq_day_10)},");
                        sb.AppendLine($"    UD_kosdaq_day_20: {체크박스(설정값.UD_kosdaq_day_20)}, UD_kosdaq_day_40: {체크박스(설정값.UD_kosdaq_day_40)}, UD_kosdaq_day_60: {체크박스(설정값.UD_kosdaq_day_60)}");
                        sb.AppendLine(");");
                        sb.AppendLine(); // 다음 그룹과 띄어쓰기
                    }
                }

                // [최종 저장]
                GenieConfig.계좌설정 = sb.ToString();
            }

            void 기본매매설정()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                // 1. [기본매매_신규횟수제한]
                sb.AppendLine("기본매매_신규횟수제한(");
                sb.AppendLine($"    CB_신규횟수제한: {체크박스(GenieConfig.CB_신규횟수제한)},");
                sb.AppendLine($"    TB_신규횟수제한: {GenieConfig.TB_신규횟수제한}");
                sb.AppendLine(");\n");

                // 2. [기본매매_new_A]
                // 3항 연산자(검색식 이름 유무)도 보간 안에서 깔끔하게 처리됩니다.
                string 검색식A = Form1.위치별검색식리스트.ContainsKey("신규_A") ? $"\"{Form1.위치별검색식리스트["신규_A"].이름}\"" : "\"\"";
                sb.AppendLine("기본매매_new_A(");
                sb.AppendLine($"    재진입: {체크박스(GenieConfig.CB_new_recatch_A)}, 유지: {GenieConfig.MTB_new_delay_A}, 취소시간: {GenieConfig.MTB_new_canceltime_A}, 취소후: {GenieConfig.combo_new_cancel_buy_A}, 반복횟수: {GenieConfig.MTB_new_repeat_A},");
                sb.AppendLine($"    사용: {체크박스(GenieConfig.CB_new_A)}, 검색식사용법: {GenieConfig.combo_new_or_A}, 검색식: {검색식A},");
                sb.AppendLine($"    시작시간: {GenieConfig.MT_new_start_A}, 종료시간: {GenieConfig.MT_new_end_A},");
                sb.AppendLine($"    비중방법: {GenieConfig.combo_new_choice_A}, 주문값: {GenieConfig.TB_new_value_A}, 주문방법: {GenieConfig.combo_new_jumun_A}, 비중: {GenieConfig.MT_new_ratio_A}");
                sb.AppendLine(");\n");

                // 3. [기본매매_new_B]
                string 검색식B = Form1.위치별검색식리스트.ContainsKey("신규_B") ? $"\"{Form1.위치별검색식리스트["신규_B"].이름}\"" : "\"\"";
                sb.AppendLine("기본매매_new_B(");
                sb.AppendLine($"    재진입: {체크박스(GenieConfig.CB_new_recatch_B)}, 유지: {GenieConfig.MTB_new_delay_B}, 취소시간: {GenieConfig.MTB_new_canceltime_B}, 취소후: {GenieConfig.combo_new_cancel_buy_B}, 반복횟수: {GenieConfig.MTB_new_repeat_B},");
                sb.AppendLine($"    사용: {체크박스(GenieConfig.CB_new_B)}, 검색식사용법: {GenieConfig.combo_new_or_B}, 검색식: {검색식B},");
                sb.AppendLine($"    시작시간: {GenieConfig.MT_new_start_B}, 종료시간: {GenieConfig.MT_new_end_B},");
                sb.AppendLine($"    비중방법: {GenieConfig.combo_new_choice_B}, 주문값: {GenieConfig.TB_new_value_B}, 주문방법: {GenieConfig.combo_new_jumun_B}, 비중: {GenieConfig.MT_new_ratio_B}");
                sb.AppendLine(");\n");

                // 4. [기본매매_new_C]
                string 검색식C = Form1.위치별검색식리스트.ContainsKey("신규_C") ? $"\"{Form1.위치별검색식리스트["신규_C"].이름}\"" : "\"\"";
                sb.AppendLine("기본매매_new_C(");
                sb.AppendLine($"    재진입: {체크박스(GenieConfig.CB_new_recatch_C)}, 유지: {GenieConfig.MTB_new_delay_C}, 취소시간: {GenieConfig.MTB_new_canceltime_C}, 취소후: {GenieConfig.combo_new_cancel_buy_C}, 반복횟수: {GenieConfig.MTB_new_repeat_C},");
                sb.AppendLine($"    사용: {체크박스(GenieConfig.CB_new_C)}, 검색식사용법: {GenieConfig.combo_new_or_C}, 검색식: {검색식C},");
                sb.AppendLine($"    시작시간: {GenieConfig.MT_new_start_C}, 종료시간: {GenieConfig.MT_new_end_C},");
                sb.AppendLine($"    비중방법: {GenieConfig.combo_new_choice_C}, 주문값: {GenieConfig.TB_new_value_C}, 주문방법: {GenieConfig.combo_new_jumun_C}, 비중: {GenieConfig.MT_new_ratio_C}");
                sb.AppendLine(");\n");

                // 5. [기본매매_신규매수조건]
                sb.AppendLine("기본매매_신규매수조건(");
                sb.AppendLine($"    신규주가이상: {GenieConfig.TB_신규주가이상}, 신규주가이하: {GenieConfig.TB_신규주가이하},");
                sb.AppendLine($"    신규등락률이상: {GenieConfig.TB_신규등락률이상}, 신규등락률이하: {GenieConfig.TB_신규등락률이하},");
                sb.AppendLine($"    추가매수딜레이: {GenieConfig.MTB_추가매수딜레이}, 재매수: {체크박스(GenieConfig.CB_new_rebuy)}, 재매수_지연시간: {GenieConfig.MTB_new_rebuytime}");
                sb.AppendLine(");\n");

                // 6. [기본매매_신규매수_세부조건]
                sb.AppendLine("기본매매_신규매수_세부조건(");
                sb.AppendLine($"    잔고개수_신규A: {GenieConfig.TB_잔고개수_신규A}, 잔고개수_신규B: {GenieConfig.TB_잔고개수_신규B}, 잔고개수_신규C: {GenieConfig.TB_잔고개수_신규C},");
                sb.AppendLine($"    재진입제한_A: {GenieConfig.TB_Limit_New_A}, 재진입제한_B: {GenieConfig.TB_Limit_New_B}, 재진입제한_C: {GenieConfig.TB_Limit_New_C},");
                sb.AppendLine($"    익절재매수A: {체크박스(GenieConfig.CB_익절재매수A)}, 익절재매수B: {체크박스(GenieConfig.CB_익절재매수B)}, 익절재매수C: {체크박스(GenieConfig.CB_익절재매수C)}");
                sb.AppendLine(");\n");

                // 7. [기본매매_트레일링스탑_조건]
                sb.AppendLine("기본매매_트레일링스탑_조건(");
                sb.AppendLine($"    손실제한: {체크박스(GenieConfig.CB_TS_손실제한)}, 취소후: {체크박스(GenieConfig.CB_TS_취소후)}, 기준금: {체크박스(GenieConfig.CB_TS_기준금)},");
                sb.AppendLine($"    canceltime: {GenieConfig.MTB_TS_canceltime}, cancel_sell: {GenieConfig.CBB_TS_cancel_sell}, repeat: {GenieConfig.MTB_TS_repeat}");
                sb.AppendLine(");\n");

                // 8. [기본매매_트레일링스탑_A~I]
                // 텍스트 파일에서도 표처럼 예쁘게 보일 수 있도록 정렬하여 출력합니다.
                sb.AppendLine($"기본매매_트레일링스탑_A(CB_TS: {체크박스(GenieConfig.CB_TS_A)}, TB_TS_upper: {GenieConfig.TB_TS_upper_A}, CBB_TS_upper: {GenieConfig.CBB_TS_upper_A}, TB_TS_down: {GenieConfig.TB_TS_down_A}, TB_TS_ratio: {GenieConfig.TB_TS_ratio_A}, CBB_TS_ratio: {GenieConfig.CBB_TS_ratio_A}, TB_TS_Jumun: {GenieConfig.TB_TS_Jumun_A}, CBB_TS_Jumun: {GenieConfig.CBB_TS_Jumun_A});");
                sb.AppendLine($"기본매매_트레일링스탑_B(CB_TS: {체크박스(GenieConfig.CB_TS_B)}, TB_TS_upper: {GenieConfig.TB_TS_upper_B}, CBB_TS_upper: {GenieConfig.CBB_TS_upper_B}, TB_TS_down: {GenieConfig.TB_TS_down_B}, TB_TS_ratio: {GenieConfig.TB_TS_ratio_B}, CBB_TS_ratio: {GenieConfig.CBB_TS_ratio_B}, TB_TS_Jumun: {GenieConfig.TB_TS_Jumun_B}, CBB_TS_Jumun: {GenieConfig.CBB_TS_Jumun_B});");
                sb.AppendLine($"기본매매_트레일링스탑_C(CB_TS: {체크박스(GenieConfig.CB_TS_C)}, TB_TS_upper: {GenieConfig.TB_TS_upper_C}, CBB_TS_upper: {GenieConfig.CBB_TS_upper_C}, TB_TS_down: {GenieConfig.TB_TS_down_C}, TB_TS_ratio: {GenieConfig.TB_TS_ratio_C}, CBB_TS_ratio: {GenieConfig.CBB_TS_ratio_C}, TB_TS_Jumun: {GenieConfig.TB_TS_Jumun_C}, CBB_TS_Jumun: {GenieConfig.CBB_TS_Jumun_C});");
                sb.AppendLine($"기본매매_트레일링스탑_D(CB_TS: {체크박스(GenieConfig.CB_TS_D)}, TB_TS_upper: {GenieConfig.TB_TS_upper_D}, CBB_TS_upper: {GenieConfig.CBB_TS_upper_D}, TB_TS_down: {GenieConfig.TB_TS_down_D}, TB_TS_ratio: {GenieConfig.TB_TS_ratio_D}, CBB_TS_ratio: {GenieConfig.CBB_TS_ratio_D}, TB_TS_Jumun: {GenieConfig.TB_TS_Jumun_D}, CBB_TS_Jumun: {GenieConfig.CBB_TS_Jumun_D});");
                sb.AppendLine($"기본매매_트레일링스탑_E(CB_TS: {체크박스(GenieConfig.CB_TS_E)}, TB_TS_upper: {GenieConfig.TB_TS_upper_E}, CBB_TS_upper: {GenieConfig.CBB_TS_upper_E}, TB_TS_down: {GenieConfig.TB_TS_down_E}, TB_TS_ratio: {GenieConfig.TB_TS_ratio_E}, CBB_TS_ratio: {GenieConfig.CBB_TS_ratio_E}, TB_TS_Jumun: {GenieConfig.TB_TS_Jumun_E}, CBB_TS_Jumun: {GenieConfig.CBB_TS_Jumun_E});");
                sb.AppendLine($"기본매매_트레일링스탑_F(CB_TS: {체크박스(GenieConfig.CB_TS_F)}, TB_TS_upper: {GenieConfig.TB_TS_upper_F}, CBB_TS_upper: {GenieConfig.CBB_TS_upper_F}, TB_TS_down: {GenieConfig.TB_TS_down_F}, TB_TS_ratio: {GenieConfig.TB_TS_ratio_F}, CBB_TS_ratio: {GenieConfig.CBB_TS_ratio_F}, TB_TS_Jumun: {GenieConfig.TB_TS_Jumun_F}, CBB_TS_Jumun: {GenieConfig.CBB_TS_Jumun_F});");
                sb.AppendLine($"기본매매_트레일링스탑_G(CB_TS: {체크박스(GenieConfig.CB_TS_G)}, TB_TS_upper: {GenieConfig.TB_TS_upper_G}, CBB_TS_upper: {GenieConfig.CBB_TS_upper_G}, TB_TS_down: {GenieConfig.TB_TS_down_G}, TB_TS_ratio: {GenieConfig.TB_TS_ratio_G}, CBB_TS_ratio: {GenieConfig.CBB_TS_ratio_G}, TB_TS_Jumun: {GenieConfig.TB_TS_Jumun_G}, CBB_TS_Jumun: {GenieConfig.CBB_TS_Jumun_G});");
                sb.AppendLine($"기본매매_트레일링스탑_H(CB_TS: {체크박스(GenieConfig.CB_TS_H)}, TB_TS_upper: {GenieConfig.TB_TS_upper_H}, CBB_TS_upper: {GenieConfig.CBB_TS_upper_H}, TB_TS_down: {GenieConfig.TB_TS_down_H}, TB_TS_ratio: {GenieConfig.TB_TS_ratio_H}, CBB_TS_ratio: {GenieConfig.CBB_TS_ratio_H}, TB_TS_Jumun: {GenieConfig.TB_TS_Jumun_H}, CBB_TS_Jumun: {GenieConfig.CBB_TS_Jumun_H});");
                sb.AppendLine($"기본매매_트레일링스탑_I(CB_TS: {체크박스(GenieConfig.CB_TS_I)}, TB_TS_upper: {GenieConfig.TB_TS_upper_I}, CBB_TS_upper: {GenieConfig.CBB_TS_upper_I}, TB_TS_down: {GenieConfig.TB_TS_down_I}, TB_TS_ratio: {GenieConfig.TB_TS_ratio_I}, CBB_TS_ratio: {GenieConfig.CBB_TS_ratio_I}, TB_TS_Jumun: {GenieConfig.TB_TS_Jumun_I}, CBB_TS_Jumun: {GenieConfig.CBB_TS_Jumun_I});");

                // [최종 저장]
                GenieConfig.기본매매설정 = sb.ToString();
            }

            void 반복매매설정()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                // 1. [반복기준금_추매조건]
                sb.AppendLine("반복기준금_추매조건(");
                sb.AppendLine($"    기준금: {체크박스(GenieConfig.CB_Repeat_기준금)}, 추매주가이상: {GenieConfig.TB_반복_추매주가이상}, 추매주가이하: {GenieConfig.TB_반복_추매주가이하}, 추매등락률이상: {GenieConfig.TB_반복_추매등락률이상}, 추매등락률이하: {GenieConfig.TB_반복_추매등락률이하}");
                sb.AppendLine(");\n");

                string 검색식A = Form1.위치별검색식리스트.ContainsKey("반복_A") ? $"\"{Form1.위치별검색식리스트["반복_A"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_A(사용: {체크박스(GenieConfig.CB_repeat_use_A)}, 시작시간: {GenieConfig.MT_repeat_time_start_A}, 종료시간: {GenieConfig.MT_repeat_time_end_A}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_A)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_A}, 검색식: {검색식A}, 검색유지시간: {GenieConfig.MTB_repeat_delay_A}, 매입금: {GenieConfig.TB_Repeat_매입금_A}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_A}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_A}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_A}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_A}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_A}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_A}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_A}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_A}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_A}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_A}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_A}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_A}, 수익범위1: {GenieConfig.TB_repeat_suik_1_A}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_A)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_A}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_A}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_A}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_A}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_A}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_A}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_A}, 반복시간: {GenieConfig.MT_repeat_repeat_time_A}, 주문가격: {GenieConfig.TB_repeat_value_A}, 매수매도: {GenieConfig.combo_repeat_jumun_A}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_A}, 취n주문: {GenieConfig.combo_repeat_Cancel_A}, 재주문: {GenieConfig.MTB_repeat_repeat_A});");
                string 검색식B = Form1.위치별검색식리스트.ContainsKey("반복_B") ? $"\"{Form1.위치별검색식리스트["반복_B"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_B(사용: {체크박스(GenieConfig.CB_repeat_use_B)}, 시작시간: {GenieConfig.MT_repeat_time_start_B}, 종료시간: {GenieConfig.MT_repeat_time_end_B}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_B)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_B}, 검색식: {검색식B}, 검색유지시간: {GenieConfig.MTB_repeat_delay_B}, 매입금: {GenieConfig.TB_Repeat_매입금_B}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_B}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_B}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_B}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_B}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_B}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_B}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_B}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_B}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_B}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_B}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_B}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_B}, 수익범위1: {GenieConfig.TB_repeat_suik_1_B}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_B)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_B}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_B}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_B}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_B}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_B}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_B}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_B}, 반복시간: {GenieConfig.MT_repeat_repeat_time_B}, 주문가격: {GenieConfig.TB_repeat_value_B}, 매수매도: {GenieConfig.combo_repeat_jumun_B}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_B}, 취n주문: {GenieConfig.combo_repeat_Cancel_B}, 재주문: {GenieConfig.MTB_repeat_repeat_B});");
                string 검색식C = Form1.위치별검색식리스트.ContainsKey("반복_C") ? $"\"{Form1.위치별검색식리스트["반복_C"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_C(사용: {체크박스(GenieConfig.CB_repeat_use_C)}, 시작시간: {GenieConfig.MT_repeat_time_start_C}, 종료시간: {GenieConfig.MT_repeat_time_end_C}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_C)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_C}, 검색식: {검색식C}, 검색유지시간: {GenieConfig.MTB_repeat_delay_C}, 매입금: {GenieConfig.TB_Repeat_매입금_C}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_C}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_C}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_C}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_C}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_C}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_C}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_C}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_C}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_C}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_C}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_C}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_C}, 수익범위1: {GenieConfig.TB_repeat_suik_1_C}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_C)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_C}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_C}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_C}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_C}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_C}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_C}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_C}, 반복시간: {GenieConfig.MT_repeat_repeat_time_C}, 주문가격: {GenieConfig.TB_repeat_value_C}, 매수매도: {GenieConfig.combo_repeat_jumun_C}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_C}, 취n주문: {GenieConfig.combo_repeat_Cancel_C}, 재주문: {GenieConfig.MTB_repeat_repeat_C});");
                string 검색식D = Form1.위치별검색식리스트.ContainsKey("반복_D") ? $"\"{Form1.위치별검색식리스트["반복_D"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_D(사용: {체크박스(GenieConfig.CB_repeat_use_D)}, 시작시간: {GenieConfig.MT_repeat_time_start_D}, 종료시간: {GenieConfig.MT_repeat_time_end_D}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_D)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_D}, 검색식: {검색식D}, 검색유지시간: {GenieConfig.MTB_repeat_delay_D}, 매입금: {GenieConfig.TB_Repeat_매입금_D}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_D}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_D}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_D}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_D}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_D}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_D}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_D}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_D}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_D}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_D}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_D}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_D}, 수익범위1: {GenieConfig.TB_repeat_suik_1_D}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_D)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_D}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_D}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_D}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_D}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_D}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_D}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_D}, 반복시간: {GenieConfig.MT_repeat_repeat_time_D}, 주문가격: {GenieConfig.TB_repeat_value_D}, 매수매도: {GenieConfig.combo_repeat_jumun_D}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_D}, 취n주문: {GenieConfig.combo_repeat_Cancel_D}, 재주문: {GenieConfig.MTB_repeat_repeat_D});");
                string 검색식E = Form1.위치별검색식리스트.ContainsKey("반복_E") ? $"\"{Form1.위치별검색식리스트["반복_E"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_E(사용: {체크박스(GenieConfig.CB_repeat_use_E)}, 시작시간: {GenieConfig.MT_repeat_time_start_E}, 종료시간: {GenieConfig.MT_repeat_time_end_E}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_E)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_E}, 검색식: {검색식E}, 검색유지시간: {GenieConfig.MTB_repeat_delay_E}, 매입금: {GenieConfig.TB_Repeat_매입금_E}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_E}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_E}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_E}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_E}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_E}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_E}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_E}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_E}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_E}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_E}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_E}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_E}, 수익범위1: {GenieConfig.TB_repeat_suik_1_E}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_E)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_E}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_E}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_E}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_E}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_E}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_E}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_E}, 반복시간: {GenieConfig.MT_repeat_repeat_time_E}, 주문가격: {GenieConfig.TB_repeat_value_E}, 매수매도: {GenieConfig.combo_repeat_jumun_E}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_E}, 취n주문: {GenieConfig.combo_repeat_Cancel_E}, 재주문: {GenieConfig.MTB_repeat_repeat_E});");
                string 검색식F = Form1.위치별검색식리스트.ContainsKey("반복_F") ? $"\"{Form1.위치별검색식리스트["반복_F"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_F(사용: {체크박스(GenieConfig.CB_repeat_use_F)}, 시작시간: {GenieConfig.MT_repeat_time_start_F}, 종료시간: {GenieConfig.MT_repeat_time_end_F}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_F)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_F}, 검색식: {검색식F}, 검색유지시간: {GenieConfig.MTB_repeat_delay_F}, 매입금: {GenieConfig.TB_Repeat_매입금_F}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_F}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_F}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_F}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_F}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_F}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_F}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_F}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_F}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_F}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_F}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_F}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_F}, 수익범위1: {GenieConfig.TB_repeat_suik_1_F}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_F)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_F}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_F}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_F}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_F}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_F}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_F}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_F}, 반복시간: {GenieConfig.MT_repeat_repeat_time_F}, 주문가격: {GenieConfig.TB_repeat_value_F}, 매수매도: {GenieConfig.combo_repeat_jumun_F}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_F}, 취n주문: {GenieConfig.combo_repeat_Cancel_F}, 재주문: {GenieConfig.MTB_repeat_repeat_F});");
                string 검색식G = Form1.위치별검색식리스트.ContainsKey("반복_G") ? $"\"{Form1.위치별검색식리스트["반복_G"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_G(사용: {체크박스(GenieConfig.CB_repeat_use_G)}, 시작시간: {GenieConfig.MT_repeat_time_start_G}, 종료시간: {GenieConfig.MT_repeat_time_end_G}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_G)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_G}, 검색식: {검색식G}, 검색유지시간: {GenieConfig.MTB_repeat_delay_G}, 매입금: {GenieConfig.TB_Repeat_매입금_G}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_G}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_G}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_G}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_G}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_G}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_G}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_G}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_G}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_G}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_G}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_G}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_G}, 수익범위1: {GenieConfig.TB_repeat_suik_1_G}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_G)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_G}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_G}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_G}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_G}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_G}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_G}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_G}, 반복시간: {GenieConfig.MT_repeat_repeat_time_G}, 주문가격: {GenieConfig.TB_repeat_value_G}, 매수매도: {GenieConfig.combo_repeat_jumun_G}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_G}, 취n주문: {GenieConfig.combo_repeat_Cancel_G}, 재주문: {GenieConfig.MTB_repeat_repeat_G});");
                string 검색식H = Form1.위치별검색식리스트.ContainsKey("반복_H") ? $"\"{Form1.위치별검색식리스트["반복_H"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_H(사용: {체크박스(GenieConfig.CB_repeat_use_H)}, 시작시간: {GenieConfig.MT_repeat_time_start_H}, 종료시간: {GenieConfig.MT_repeat_time_end_H}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_H)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_H}, 검색식: {검색식H}, 검색유지시간: {GenieConfig.MTB_repeat_delay_H}, 매입금: {GenieConfig.TB_Repeat_매입금_H}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_H}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_H}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_H}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_H}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_H}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_H}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_H}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_H}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_H}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_H}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_H}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_H}, 수익범위1: {GenieConfig.TB_repeat_suik_1_H}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_H)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_H}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_H}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_H}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_H}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_H}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_H}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_H}, 반복시간: {GenieConfig.MT_repeat_repeat_time_H}, 주문가격: {GenieConfig.TB_repeat_value_H}, 매수매도: {GenieConfig.combo_repeat_jumun_H}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_H}, 취n주문: {GenieConfig.combo_repeat_Cancel_H}, 재주문: {GenieConfig.MTB_repeat_repeat_H});");
                string 검색식I = Form1.위치별검색식리스트.ContainsKey("반복_I") ? $"\"{Form1.위치별검색식리스트["반복_I"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_I(사용: {체크박스(GenieConfig.CB_repeat_use_I)}, 시작시간: {GenieConfig.MT_repeat_time_start_I}, 종료시간: {GenieConfig.MT_repeat_time_end_I}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_I)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_I}, 검색식: {검색식I}, 검색유지시간: {GenieConfig.MTB_repeat_delay_I}, 매입금: {GenieConfig.TB_Repeat_매입금_I}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_I}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_I}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_I}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_I}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_I}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_I}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_I}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_I}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_I}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_I}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_I}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_I}, 수익범위1: {GenieConfig.TB_repeat_suik_1_I}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_I)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_I}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_I}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_I}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_I}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_I}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_I}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_I}, 반복시간: {GenieConfig.MT_repeat_repeat_time_I}, 주문가격: {GenieConfig.TB_repeat_value_I}, 매수매도: {GenieConfig.combo_repeat_jumun_I}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_I}, 취n주문: {GenieConfig.combo_repeat_Cancel_I}, 재주문: {GenieConfig.MTB_repeat_repeat_I});");
                string 검색식J = Form1.위치별검색식리스트.ContainsKey("반복_J") ? $"\"{Form1.위치별검색식리스트["반복_J"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_J(사용: {체크박스(GenieConfig.CB_repeat_use_J)}, 시작시간: {GenieConfig.MT_repeat_time_start_J}, 종료시간: {GenieConfig.MT_repeat_time_end_J}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_J)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_J}, 검색식: {검색식J}, 검색유지시간: {GenieConfig.MTB_repeat_delay_J}, 매입금: {GenieConfig.TB_Repeat_매입금_J}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_J}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_J}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_J}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_J}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_J}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_J}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_J}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_J}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_J}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_J}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_J}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_J}, 수익범위1: {GenieConfig.TB_repeat_suik_1_J}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_J)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_J}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_J}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_J}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_J}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_J}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_J}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_J}, 반복시간: {GenieConfig.MT_repeat_repeat_time_J}, 주문가격: {GenieConfig.TB_repeat_value_J}, 매수매도: {GenieConfig.combo_repeat_jumun_J}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_J}, 취n주문: {GenieConfig.combo_repeat_Cancel_J}, 재주문: {GenieConfig.MTB_repeat_repeat_J});");
                string 검색식K = Form1.위치별검색식리스트.ContainsKey("반복_K") ? $"\"{Form1.위치별검색식리스트["반복_K"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_K(사용: {체크박스(GenieConfig.CB_repeat_use_K)}, 시작시간: {GenieConfig.MT_repeat_time_start_K}, 종료시간: {GenieConfig.MT_repeat_time_end_K}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_K)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_K}, 검색식: {검색식K}, 검색유지시간: {GenieConfig.MTB_repeat_delay_K}, 매입금: {GenieConfig.TB_Repeat_매입금_K}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_K}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_K}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_K}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_K}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_K}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_K}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_K}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_K}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_K}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_K}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_K}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_K}, 수익범위1: {GenieConfig.TB_repeat_suik_1_K}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_K)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_K}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_K}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_K}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_K}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_K}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_K}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_K}, 반복시간: {GenieConfig.MT_repeat_repeat_time_K}, 주문가격: {GenieConfig.TB_repeat_value_K}, 매수매도: {GenieConfig.combo_repeat_jumun_K}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_K}, 취n주문: {GenieConfig.combo_repeat_Cancel_K}, 재주문: {GenieConfig.MTB_repeat_repeat_K});");
                string 검색식L = Form1.위치별검색식리스트.ContainsKey("반복_L") ? $"\"{Form1.위치별검색식리스트["반복_L"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_L(사용: {체크박스(GenieConfig.CB_repeat_use_L)}, 시작시간: {GenieConfig.MT_repeat_time_start_L}, 종료시간: {GenieConfig.MT_repeat_time_end_L}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_L)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_L}, 검색식: {검색식L}, 검색유지시간: {GenieConfig.MTB_repeat_delay_L}, 매입금: {GenieConfig.TB_Repeat_매입금_L}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_L}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_L}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_L}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_L}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_L}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_L}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_L}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_L}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_L}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_L}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_L}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_L}, 수익범위1: {GenieConfig.TB_repeat_suik_1_L}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_L)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_L}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_L}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_L}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_L}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_L}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_L}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_L}, 반복시간: {GenieConfig.MT_repeat_repeat_time_L}, 주문가격: {GenieConfig.TB_repeat_value_L}, 매수매도: {GenieConfig.combo_repeat_jumun_L}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_L}, 취n주문: {GenieConfig.combo_repeat_Cancel_L}, 재주문: {GenieConfig.MTB_repeat_repeat_L});");
                string 검색식M = Form1.위치별검색식리스트.ContainsKey("반복_M") ? $"\"{Form1.위치별검색식리스트["반복_M"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_M(사용: {체크박스(GenieConfig.CB_repeat_use_M)}, 시작시간: {GenieConfig.MT_repeat_time_start_M}, 종료시간: {GenieConfig.MT_repeat_time_end_M}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_M)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_M}, 검색식: {검색식M}, 검색유지시간: {GenieConfig.MTB_repeat_delay_M}, 매입금: {GenieConfig.TB_Repeat_매입금_M}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_M}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_M}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_M}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_M}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_M}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_M}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_M}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_M}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_M}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_M}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_M}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_M}, 수익범위1: {GenieConfig.TB_repeat_suik_1_M}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_M)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_M}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_M}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_M}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_M}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_M}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_M}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_M}, 반복시간: {GenieConfig.MT_repeat_repeat_time_M}, 주문가격: {GenieConfig.TB_repeat_value_M}, 매수매도: {GenieConfig.combo_repeat_jumun_M}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_M}, 취n주문: {GenieConfig.combo_repeat_Cancel_M}, 재주문: {GenieConfig.MTB_repeat_repeat_M});");
                string 검색식N = Form1.위치별검색식리스트.ContainsKey("반복_N") ? $"\"{Form1.위치별검색식리스트["반복_N"].이름}\"" : "\"\"";
                sb.AppendLine($"반복매매_N(사용: {체크박스(GenieConfig.CB_repeat_use_N)}, 시작시간: {GenieConfig.MT_repeat_time_start_N}, 종료시간: {GenieConfig.MT_repeat_time_end_N}, 매매종류: {체크박스(GenieConfig.CB_repeat_kind_N)}, 검색식사용: {GenieConfig.combo_repeat_use_condition_N}, 검색식: {검색식N}, 검색유지시간: {GenieConfig.MTB_repeat_delay_N}, 매입금: {GenieConfig.TB_Repeat_매입금_N}, 누적거래량: {GenieConfig.TB_repeat_누적거래량_N}, 누적거래대금: {GenieConfig.TB_repeat_누적거래대금_N}, TB_mma1: {GenieConfig.TB_repeat_MinMAPeriod1_N}, CBB_mma1: {GenieConfig.CBB_repeat_MinMAPeriod1_N}, TB_mma2: {GenieConfig.TB_repeat_MinMAPeriod2_N}, CBB_mma2: {GenieConfig.CBB_repeat_MinMAPeriod2_N}, CBB_mma_배열: {GenieConfig.CBB_repeat_MinMAPeriod1_배열_N}, TB_dma1: {GenieConfig.TB_repeat_DayMAPeriod1_N}, CBB_dma1: {GenieConfig.CBB_repeat_DayMAPeriod1_N}, TB_dma2: {GenieConfig.TB_repeat_DayMAPeriod2_N}, CBB_dma2: {GenieConfig.CBB_repeat_DayMAPeriod2_N}, CBB_dma_배열: {GenieConfig.CBB_repeat_DayMAPeriod_배열_N}, 수익범위1: {GenieConfig.TB_repeat_suik_1_N}, 수익범위선택: {체크박스(GenieConfig.CB_repeat_choice_N)}, 수익범위2: {GenieConfig.TB_repeat_suik_2_N}, 수익구분: {GenieConfig.combo_repeat_suik_gubun_N}, 매수비중: {GenieConfig.TB_repeat_sell_ratio_N}, 매수구분: {GenieConfig.combo_repeat_sell_gubun_N}, 매매범위1: {GenieConfig.TB_Repeat_maemae_1_N}, 매매범위2: {GenieConfig.TB_Repeat_maemae_2_N}, 매매범위기준: {GenieConfig.combo_Repeat_maemae_gubun_N}, 반복시간: {GenieConfig.MT_repeat_repeat_time_N}, 주문가격: {GenieConfig.TB_repeat_value_N}, 매수매도: {GenieConfig.combo_repeat_jumun_N}, 취소시간: {GenieConfig.MTB_repeat_Cancel_time_N}, 취n주문: {GenieConfig.combo_repeat_Cancel_N}, 재주문: {GenieConfig.MTB_repeat_repeat_N});");

                // [최종 저장] (GenieConfig.반복매매설정)
                GenieConfig.반복매매설정 = sb.ToString();
            }

            void 계좌관리설정()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                // 1. 공통 및 기본 조건 설정
                sb.AppendLine("계좌관리_추매조건(");
                sb.AppendLine($"    CB_총매수금: {체크박스(GenieConfig.CB_총매수금)}, 종목최대매수금: {GenieConfig.TB_총매수금}, CB_일매수제한금: {체크박스(GenieConfig.CB_일매수제한금)}, 일매수제한금: {GenieConfig.TB_일매수제한금}, CB_회수제한: {체크박스(GenieConfig.CB_회수제한)}, 회수제한: {GenieConfig.TB_회수제한},");
                sb.AppendLine($"    추매주가이상: {GenieConfig.TB_리밸_추매주가이상}, 추매주가이하: {GenieConfig.TB_리밸_추매주가이하}, 추매등락률이상: {GenieConfig.TB_리밸_추매등락률이상}, 추매등락률이하: {GenieConfig.TB_리밸_추매등락률이하}");
                sb.AppendLine(");\n");

                sb.AppendLine("계좌관리_분할주문(");
                sb.AppendLine($"    분할간격_A: {GenieConfig.TB_분할간격_A}, 분할횟수_A: {GenieConfig.TB_분할횟수_A}, 분할간격_B: {GenieConfig.TB_분할간격_B}, 분할횟수_B: {GenieConfig.TB_분할횟수_B}, 분할간격_C: {GenieConfig.TB_분할간격_C}, 분할횟수_C: {GenieConfig.TB_분할횟수_C}");
                sb.AppendLine(");\n");

                sb.AppendLine("계좌관리_기준비율관리(");
                sb.AppendLine($"    CB_매수기준: {체크박스(GenieConfig.CB_매수기준)}, TB_매수비율: {GenieConfig.TB_매수비율}, CB_손익기준: {체크박스(GenieConfig.CB_손익기준)}, TB_손익비율: {GenieConfig.TB_손익비율}");
                sb.AppendLine(");\n");

                sb.AppendLine("계좌관리_감시주문시간n기준금(");
                sb.AppendLine($"    Selltime_오전: {GenieConfig.MTB_rebalance_Selltime_오전}, Selltime_오후: {GenieConfig.MTB_rebalance_Selltime_오후}, rebalance_기준금: {체크박스(GenieConfig.CB_rebalance_기준금)}, cut_기준금: {체크박스(GenieConfig.CB_cut_기준금)}, Liquidation_기준금: {체크박스(GenieConfig.CB_Liquidation_기준금)}");
                sb.AppendLine(");\n");

                // =========================================================================
                // 2. 계좌관리 리밸런싱 (A ~ G) 
                // =========================================================================
                string 검색식ReA = Form1.위치별검색식리스트.ContainsKey("리밸_A") ? $"\"{Form1.위치별검색식리스트["리밸_A"].이름}\"" : "\"\"";
                sb.AppendLine($"계좌관리_리밸런싱_A(사용: {체크박스(GenieConfig.CB_rebalance_A)}, 시작시간: {GenieConfig.MT_rebalance_starttime_A}, 종료시간: {GenieConfig.MT_rebalance_stoptime_A}, 검색식사용: {GenieConfig.combo_rebalance_use_condition_A}, 검색식: {검색식ReA}, 검색유지시간: {GenieConfig.MTB_rebalance_delay_A}, 매입금: {GenieConfig.TB_Rebalance_매입금_A}, 누적거래량: {GenieConfig.TB_rebalance_누적거래량_A}, 누적거래대금: {GenieConfig.TB_rebalance_누적거래대금_A}, TB_mma1: {GenieConfig.TB_rebalance_MinMAPeriod1_A}, CBB_mma1: {GenieConfig.CBB_rebalance_MinMAPeriod1_A}, TB_mma2: {GenieConfig.TB_rebalance_MinMAPeriod2_A}, CBB_mma2: {GenieConfig.CBB_rebalance_MinMAPeriod2_A}, CBB_mma_배열: {GenieConfig.CBB_rebalance_MinMAPeriod1_배열_A}, TB_dma1: {GenieConfig.TB_rebalance_DayMAPeriod1_A}, CBB_dma1: {GenieConfig.CBB_rebalance_DayMAPeriod1_A}, TB_dma2: {GenieConfig.TB_rebalance_DayMAPeriod2_A}, CBB_dma2: {GenieConfig.CBB_rebalance_DayMAPeriod2_A}, CBB_dma_배열: {GenieConfig.CBB_rebalance_DayMAPeriod_배열_A}, 수익범위1: {GenieConfig.TB_rebalance_suik_1_A}, 수익범위선택: {체크박스(GenieConfig.CB_rebalance_choice_A)}, 수익범위2: {GenieConfig.TB_rebalance_suik_2_A}, 수익구분: {GenieConfig.combo_rebalance_suik_gubun_A}, 매수비중: {GenieConfig.TB_rebalance_sell_ratio_A}, 매수구분: {GenieConfig.combo_rebalance_sell_gubun_A}, 매매범위1: {GenieConfig.TB_rebalance_maemae_1_A}, 매매범위2: {GenieConfig.TB_rebalance_maemae_2_A}, 매매범위기준: {GenieConfig.combo_rebalance_maemae_gubun_A}, 반복시간: {GenieConfig.MT_rebalance_repeat_time_A}, 주문가격: {GenieConfig.TB_rebalance_value_A}, 매수매도: {GenieConfig.combo_rebalance_jumun_A}, 취소시간: {GenieConfig.MTB_rebalance_Cancel_time_A}, 감시시점: {체크박스(GenieConfig.CB_rebalance_option_A)}, 주문조건_1차: {GenieConfig.TB_rebalance_sellratio1_A}, 주문조건선택_1차: \"{GenieConfig.리밸매도기준1_A}\", 매도비중_1차: {GenieConfig.TB_rebalance_sellvolume1_A}, 취소시간_1차: {GenieConfig.TB_rebalance_sellcancel1_A}, 주문조건_2차: {GenieConfig.TB_rebalance_sellratio2_A}, 주문조건선택_2차: \"{GenieConfig.리밸매도기준2_A}\", 매도비중_2차: {GenieConfig.TB_rebalance_sellvolume2_A}, 취소시간_2차: {GenieConfig.TB_rebalance_sellcancel2_A}, 매도체크: {체크박스(GenieConfig.CB_rebalance_매도체크_A)}, 감시주문시간: {GenieConfig.CBB_rebalance_Selltime_A}, 감시주문값: {GenieConfig.TB_rebalance_감시_value_A}, 감시매수매도: {GenieConfig.combo_rebalance_감시_jumun_A}, TS_1차: {체크박스(GenieConfig.CB_rebalance_TS_1차_A)}, TS_1차_down: {GenieConfig.TB_rebalance_TS_1차_down_A}, TB_1차_이평: {GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_A}, CBB_1차_이평: {GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_A}, TS_2차: {체크박스(GenieConfig.CB_rebalance_TS_2차_A)}, TS_2차_down: {GenieConfig.TB_rebalance_TS_2차_down_A}, TB_2차_이평: {GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_A}, CBB_2차_이평: {GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_A});");

                string 검색식ReB = Form1.위치별검색식리스트.ContainsKey("리밸_B") ? $"\"{Form1.위치별검색식리스트["리밸_B"].이름}\"" : "\"\"";
                sb.AppendLine($"계좌관리_리밸런싱_B(사용: {체크박스(GenieConfig.CB_rebalance_B)}, 시작시간: {GenieConfig.MT_rebalance_starttime_B}, 종료시간: {GenieConfig.MT_rebalance_stoptime_B}, 검색식사용: {GenieConfig.combo_rebalance_use_condition_B}, 검색식: {검색식ReB}, 검색유지시간: {GenieConfig.MTB_rebalance_delay_B}, 매입금: {GenieConfig.TB_Rebalance_매입금_B}, 누적거래량: {GenieConfig.TB_rebalance_누적거래량_B}, 누적거래대금: {GenieConfig.TB_rebalance_누적거래대금_B}, TB_mma1: {GenieConfig.TB_rebalance_MinMAPeriod1_B}, CBB_mma1: {GenieConfig.CBB_rebalance_MinMAPeriod1_B}, TB_mma2: {GenieConfig.TB_rebalance_MinMAPeriod2_B}, CBB_mma2: {GenieConfig.CBB_rebalance_MinMAPeriod2_B}, CBB_mma_배열: {GenieConfig.CBB_rebalance_MinMAPeriod1_배열_B}, TB_dma1: {GenieConfig.TB_rebalance_DayMAPeriod1_B}, CBB_dma1: {GenieConfig.CBB_rebalance_DayMAPeriod1_B}, TB_dma2: {GenieConfig.TB_rebalance_DayMAPeriod2_B}, CBB_dma2: {GenieConfig.CBB_rebalance_DayMAPeriod2_B}, CBB_dma_배열: {GenieConfig.CBB_rebalance_DayMAPeriod_배열_B}, 수익범위1: {GenieConfig.TB_rebalance_suik_1_B}, 수익범위선택: {체크박스(GenieConfig.CB_rebalance_choice_B)}, 수익범위2: {GenieConfig.TB_rebalance_suik_2_B}, 수익구분: {GenieConfig.combo_rebalance_suik_gubun_B}, 매수비중: {GenieConfig.TB_rebalance_sell_ratio_B}, 매수구분: {GenieConfig.combo_rebalance_sell_gubun_B}, 매매범위1: {GenieConfig.TB_rebalance_maemae_1_B}, 매매범위2: {GenieConfig.TB_rebalance_maemae_2_B}, 매매범위기준: {GenieConfig.combo_rebalance_maemae_gubun_B}, 반복시간: {GenieConfig.MT_rebalance_repeat_time_B}, 주문가격: {GenieConfig.TB_rebalance_value_B}, 매수매도: {GenieConfig.combo_rebalance_jumun_B}, 취소시간: {GenieConfig.MTB_rebalance_Cancel_time_B}, 감시시점: {체크박스(GenieConfig.CB_rebalance_option_B)}, 주문조건_1차: {GenieConfig.TB_rebalance_sellratio1_B}, 주문조건선택_1차: \"{GenieConfig.리밸매도기준1_B}\", 매도비중_1차: {GenieConfig.TB_rebalance_sellvolume1_B}, 취소시간_1차: {GenieConfig.TB_rebalance_sellcancel1_B}, 주문조건_2차: {GenieConfig.TB_rebalance_sellratio2_B}, 주문조건선택_2차: \"{GenieConfig.리밸매도기준2_B}\", 매도비중_2차: {GenieConfig.TB_rebalance_sellvolume2_B}, 취소시간_2차: {GenieConfig.TB_rebalance_sellcancel2_B}, 매도체크: {체크박스(GenieConfig.CB_rebalance_매도체크_B)}, 감시주문시간: {GenieConfig.CBB_rebalance_Selltime_B}, 감시주문값: {GenieConfig.TB_rebalance_감시_value_B}, 감시매수매도: {GenieConfig.combo_rebalance_감시_jumun_B}, TS_1차: {체크박스(GenieConfig.CB_rebalance_TS_1차_B)}, TS_1차_down: {GenieConfig.TB_rebalance_TS_1차_down_B}, TB_1차_이평: {GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_B}, CBB_1차_이평: {GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_B}, TS_2차: {체크박스(GenieConfig.CB_rebalance_TS_2차_B)}, TS_2차_down: {GenieConfig.TB_rebalance_TS_2차_down_B}, TB_2차_이평: {GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_B}, CBB_2차_이평: {GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_B});");

                string 검색식ReC = Form1.위치별검색식리스트.ContainsKey("리밸_C") ? $"\"{Form1.위치별검색식리스트["리밸_C"].이름}\"" : "\"\"";
                sb.AppendLine($"계좌관리_리밸런싱_C(사용: {체크박스(GenieConfig.CB_rebalance_C)}, 시작시간: {GenieConfig.MT_rebalance_starttime_C}, 종료시간: {GenieConfig.MT_rebalance_stoptime_C}, 검색식사용: {GenieConfig.combo_rebalance_use_condition_C}, 검색식: {검색식ReC}, 검색유지시간: {GenieConfig.MTB_rebalance_delay_C}, 매입금: {GenieConfig.TB_Rebalance_매입금_C}, 누적거래량: {GenieConfig.TB_rebalance_누적거래량_C}, 누적거래대금: {GenieConfig.TB_rebalance_누적거래대금_C}, TB_mma1: {GenieConfig.TB_rebalance_MinMAPeriod1_C}, CBB_mma1: {GenieConfig.CBB_rebalance_MinMAPeriod1_C}, TB_mma2: {GenieConfig.TB_rebalance_MinMAPeriod2_C}, CBB_mma2: {GenieConfig.CBB_rebalance_MinMAPeriod2_C}, CBB_mma_배열: {GenieConfig.CBB_rebalance_MinMAPeriod1_배열_C}, TB_dma1: {GenieConfig.TB_rebalance_DayMAPeriod1_C}, CBB_dma1: {GenieConfig.CBB_rebalance_DayMAPeriod1_C}, TB_dma2: {GenieConfig.TB_rebalance_DayMAPeriod2_C}, CBB_dma2: {GenieConfig.CBB_rebalance_DayMAPeriod2_C}, CBB_dma_배열: {GenieConfig.CBB_rebalance_DayMAPeriod_배열_C}, 수익범위1: {GenieConfig.TB_rebalance_suik_1_C}, 수익범위선택: {체크박스(GenieConfig.CB_rebalance_choice_C)}, 수익범위2: {GenieConfig.TB_rebalance_suik_2_C}, 수익구분: {GenieConfig.combo_rebalance_suik_gubun_C}, 매수비중: {GenieConfig.TB_rebalance_sell_ratio_C}, 매수구분: {GenieConfig.combo_rebalance_sell_gubun_C}, 매매범위1: {GenieConfig.TB_rebalance_maemae_1_C}, 매매범위2: {GenieConfig.TB_rebalance_maemae_2_C}, 매매범위기준: {GenieConfig.combo_rebalance_maemae_gubun_C}, 반복시간: {GenieConfig.MT_rebalance_repeat_time_C}, 주문가격: {GenieConfig.TB_rebalance_value_C}, 매수매도: {GenieConfig.combo_rebalance_jumun_C}, 취소시간: {GenieConfig.MTB_rebalance_Cancel_time_C}, 감시시점: {체크박스(GenieConfig.CB_rebalance_option_C)}, 주문조건_1차: {GenieConfig.TB_rebalance_sellratio1_C}, 주문조건선택_1차: \"{GenieConfig.리밸매도기준1_C}\", 매도비중_1차: {GenieConfig.TB_rebalance_sellvolume1_C}, 취소시간_1차: {GenieConfig.TB_rebalance_sellcancel1_C}, 주문조건_2차: {GenieConfig.TB_rebalance_sellratio2_C}, 주문조건선택_2차: \"{GenieConfig.리밸매도기준2_C}\", 매도비중_2차: {GenieConfig.TB_rebalance_sellvolume2_C}, 취소시간_2차: {GenieConfig.TB_rebalance_sellcancel2_C}, 매도체크: {체크박스(GenieConfig.CB_rebalance_매도체크_C)}, 감시주문시간: {GenieConfig.CBB_rebalance_Selltime_C}, 감시주문값: {GenieConfig.TB_rebalance_감시_value_C}, 감시매수매도: {GenieConfig.combo_rebalance_감시_jumun_C}, TS_1차: {체크박스(GenieConfig.CB_rebalance_TS_1차_C)}, TS_1차_down: {GenieConfig.TB_rebalance_TS_1차_down_C}, TB_1차_이평: {GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_C}, CBB_1차_이평: {GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_C}, TS_2차: {체크박스(GenieConfig.CB_rebalance_TS_2차_C)}, TS_2차_down: {GenieConfig.TB_rebalance_TS_2차_down_C}, TB_2차_이평: {GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_C}, CBB_2차_이평: {GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_C});");

                string 검색식ReD = Form1.위치별검색식리스트.ContainsKey("리밸_D") ? $"\"{Form1.위치별검색식리스트["리밸_D"].이름}\"" : "\"\"";
                sb.AppendLine($"계좌관리_리밸런싱_D(사용: {체크박스(GenieConfig.CB_rebalance_D)}, 시작시간: {GenieConfig.MT_rebalance_starttime_D}, 종료시간: {GenieConfig.MT_rebalance_stoptime_D}, 검색식사용: {GenieConfig.combo_rebalance_use_condition_D}, 검색식: {검색식ReD}, 검색유지시간: {GenieConfig.MTB_rebalance_delay_D}, 매입금: {GenieConfig.TB_Rebalance_매입금_D}, 누적거래량: {GenieConfig.TB_rebalance_누적거래량_D}, 누적거래대금: {GenieConfig.TB_rebalance_누적거래대금_D}, TB_mma1: {GenieConfig.TB_rebalance_MinMAPeriod1_D}, CBB_mma1: {GenieConfig.CBB_rebalance_MinMAPeriod1_D}, TB_mma2: {GenieConfig.TB_rebalance_MinMAPeriod2_D}, CBB_mma2: {GenieConfig.CBB_rebalance_MinMAPeriod2_D}, CBB_mma_배열: {GenieConfig.CBB_rebalance_MinMAPeriod1_배열_D}, TB_dma1: {GenieConfig.TB_rebalance_DayMAPeriod1_D}, CBB_dma1: {GenieConfig.CBB_rebalance_DayMAPeriod1_D}, TB_dma2: {GenieConfig.TB_rebalance_DayMAPeriod2_D}, CBB_dma2: {GenieConfig.CBB_rebalance_DayMAPeriod2_D}, CBB_dma_배열: {GenieConfig.CBB_rebalance_DayMAPeriod_배열_D}, 수익범위1: {GenieConfig.TB_rebalance_suik_1_D}, 수익범위선택: {체크박스(GenieConfig.CB_rebalance_choice_D)}, 수익범위2: {GenieConfig.TB_rebalance_suik_2_D}, 수익구분: {GenieConfig.combo_rebalance_suik_gubun_D}, 매수비중: {GenieConfig.TB_rebalance_sell_ratio_D}, 매수구분: {GenieConfig.combo_rebalance_sell_gubun_D}, 매매범위1: {GenieConfig.TB_rebalance_maemae_1_D}, 매매범위2: {GenieConfig.TB_rebalance_maemae_2_D}, 매매범위기준: {GenieConfig.combo_rebalance_maemae_gubun_D}, 반복시간: {GenieConfig.MT_rebalance_repeat_time_D}, 주문가격: {GenieConfig.TB_rebalance_value_D}, 매수매도: {GenieConfig.combo_rebalance_jumun_D}, 취소시간: {GenieConfig.MTB_rebalance_Cancel_time_D}, 감시시점: {체크박스(GenieConfig.CB_rebalance_option_D)}, 주문조건_1차: {GenieConfig.TB_rebalance_sellratio1_D}, 주문조건선택_1차: \"{GenieConfig.리밸매도기준1_D}\", 매도비중_1차: {GenieConfig.TB_rebalance_sellvolume1_D}, 취소시간_1차: {GenieConfig.TB_rebalance_sellcancel1_D}, 주문조건_2차: {GenieConfig.TB_rebalance_sellratio2_D}, 주문조건선택_2차: \"{GenieConfig.리밸매도기준2_D}\", 매도비중_2차: {GenieConfig.TB_rebalance_sellvolume2_D}, 취소시간_2차: {GenieConfig.TB_rebalance_sellcancel2_D}, 매도체크: {체크박스(GenieConfig.CB_rebalance_매도체크_D)}, 감시주문시간: {GenieConfig.CBB_rebalance_Selltime_D}, 감시주문값: {GenieConfig.TB_rebalance_감시_value_D}, 감시매수매도: {GenieConfig.combo_rebalance_감시_jumun_D}, TS_1차: {체크박스(GenieConfig.CB_rebalance_TS_1차_D)}, TS_1차_down: {GenieConfig.TB_rebalance_TS_1차_down_D}, TB_1차_이평: {GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_D}, CBB_1차_이평: {GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_D}, TS_2차: {체크박스(GenieConfig.CB_rebalance_TS_2차_D)}, TS_2차_down: {GenieConfig.TB_rebalance_TS_2차_down_D}, TB_2차_이평: {GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_D}, CBB_2차_이평: {GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_D});");

                string 검색식ReE = Form1.위치별검색식리스트.ContainsKey("리밸_E") ? $"\"{Form1.위치별검색식리스트["리밸_E"].이름}\"" : "\"\"";
                sb.AppendLine($"계좌관리_리밸런싱_E(사용: {체크박스(GenieConfig.CB_rebalance_E)}, 시작시간: {GenieConfig.MT_rebalance_starttime_E}, 종료시간: {GenieConfig.MT_rebalance_stoptime_E}, 검색식사용: {GenieConfig.combo_rebalance_use_condition_E}, 검색식: {검색식ReE}, 검색유지시간: {GenieConfig.MTB_rebalance_delay_E}, 매입금: {GenieConfig.TB_Rebalance_매입금_E}, 누적거래량: {GenieConfig.TB_rebalance_누적거래량_E}, 누적거래대금: {GenieConfig.TB_rebalance_누적거래대금_E}, TB_mma1: {GenieConfig.TB_rebalance_MinMAPeriod1_E}, CBB_mma1: {GenieConfig.CBB_rebalance_MinMAPeriod1_E}, TB_mma2: {GenieConfig.TB_rebalance_MinMAPeriod2_E}, CBB_mma2: {GenieConfig.CBB_rebalance_MinMAPeriod2_E}, CBB_mma_배열: {GenieConfig.CBB_rebalance_MinMAPeriod1_배열_E}, TB_dma1: {GenieConfig.TB_rebalance_DayMAPeriod1_E}, CBB_dma1: {GenieConfig.CBB_rebalance_DayMAPeriod1_E}, TB_dma2: {GenieConfig.TB_rebalance_DayMAPeriod2_E}, CBB_dma2: {GenieConfig.CBB_rebalance_DayMAPeriod2_E}, CBB_dma_배열: {GenieConfig.CBB_rebalance_DayMAPeriod_배열_E}, 수익범위1: {GenieConfig.TB_rebalance_suik_1_E}, 수익범위선택: {체크박스(GenieConfig.CB_rebalance_choice_E)}, 수익범위2: {GenieConfig.TB_rebalance_suik_2_E}, 수익구분: {GenieConfig.combo_rebalance_suik_gubun_E}, 매수비중: {GenieConfig.TB_rebalance_sell_ratio_E}, 매수구분: {GenieConfig.combo_rebalance_sell_gubun_E}, 매매범위1: {GenieConfig.TB_rebalance_maemae_1_E}, 매매범위2: {GenieConfig.TB_rebalance_maemae_2_E}, 매매범위기준: {GenieConfig.combo_rebalance_maemae_gubun_E}, 반복시간: {GenieConfig.MT_rebalance_repeat_time_E}, 주문가격: {GenieConfig.TB_rebalance_value_E}, 매수매도: {GenieConfig.combo_rebalance_jumun_E}, 취소시간: {GenieConfig.MTB_rebalance_Cancel_time_E}, 감시시점: {체크박스(GenieConfig.CB_rebalance_option_E)}, 주문조건_1차: {GenieConfig.TB_rebalance_sellratio1_E}, 주문조건선택_1차: \"{GenieConfig.리밸매도기준1_E}\", 매도비중_1차: {GenieConfig.TB_rebalance_sellvolume1_E}, 취소시간_1차: {GenieConfig.TB_rebalance_sellcancel1_E}, 주문조건_2차: {GenieConfig.TB_rebalance_sellratio2_E}, 주문조건선택_2차: \"{GenieConfig.리밸매도기준2_E}\", 매도비중_2차: {GenieConfig.TB_rebalance_sellvolume2_E}, 취소시간_2차: {GenieConfig.TB_rebalance_sellcancel2_E}, 매도체크: {체크박스(GenieConfig.CB_rebalance_매도체크_E)}, 감시주문시간: {GenieConfig.CBB_rebalance_Selltime_E}, 감시주문값: {GenieConfig.TB_rebalance_감시_value_E}, 감시매수매도: {GenieConfig.combo_rebalance_감시_jumun_E}, TS_1차: {체크박스(GenieConfig.CB_rebalance_TS_1차_E)}, TS_1차_down: {GenieConfig.TB_rebalance_TS_1차_down_E}, TB_1차_이평: {GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_E}, CBB_1차_이평: {GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_E}, TS_2차: {체크박스(GenieConfig.CB_rebalance_TS_2차_E)}, TS_2차_down: {GenieConfig.TB_rebalance_TS_2차_down_E}, TB_2차_이평: {GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_E}, CBB_2차_이평: {GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_E});");

                string 검색식ReF = Form1.위치별검색식리스트.ContainsKey("리밸_F") ? $"\"{Form1.위치별검색식리스트["리밸_F"].이름}\"" : "\"\"";
                sb.AppendLine($"계좌관리_리밸런싱_F(사용: {체크박스(GenieConfig.CB_rebalance_F)}, 시작시간: {GenieConfig.MT_rebalance_starttime_F}, 종료시간: {GenieConfig.MT_rebalance_stoptime_F}, 검색식사용: {GenieConfig.combo_rebalance_use_condition_F}, 검색식: {검색식ReF}, 검색유지시간: {GenieConfig.MTB_rebalance_delay_F}, 매입금: {GenieConfig.TB_Rebalance_매입금_F}, 누적거래량: {GenieConfig.TB_rebalance_누적거래량_F}, 누적거래대금: {GenieConfig.TB_rebalance_누적거래대금_F}, TB_mma1: {GenieConfig.TB_rebalance_MinMAPeriod1_F}, CBB_mma1: {GenieConfig.CBB_rebalance_MinMAPeriod1_F}, TB_mma2: {GenieConfig.TB_rebalance_MinMAPeriod2_F}, CBB_mma2: {GenieConfig.CBB_rebalance_MinMAPeriod2_F}, CBB_mma_배열: {GenieConfig.CBB_rebalance_MinMAPeriod1_배열_F}, TB_dma1: {GenieConfig.TB_rebalance_DayMAPeriod1_F}, CBB_dma1: {GenieConfig.CBB_rebalance_DayMAPeriod1_F}, TB_dma2: {GenieConfig.TB_rebalance_DayMAPeriod2_F}, CBB_dma2: {GenieConfig.CBB_rebalance_DayMAPeriod2_F}, CBB_dma_배열: {GenieConfig.CBB_rebalance_DayMAPeriod_배열_F}, 수익범위1: {GenieConfig.TB_rebalance_suik_1_F}, 수익범위선택: {체크박스(GenieConfig.CB_rebalance_choice_F)}, 수익범위2: {GenieConfig.TB_rebalance_suik_2_F}, 수익구분: {GenieConfig.combo_rebalance_suik_gubun_F}, 매수비중: {GenieConfig.TB_rebalance_sell_ratio_F}, 매수구분: {GenieConfig.combo_rebalance_sell_gubun_F}, 매매범위1: {GenieConfig.TB_rebalance_maemae_1_F}, 매매범위2: {GenieConfig.TB_rebalance_maemae_2_F}, 매매범위기준: {GenieConfig.combo_rebalance_maemae_gubun_F}, 반복시간: {GenieConfig.MT_rebalance_repeat_time_F}, 주문가격: {GenieConfig.TB_rebalance_value_F}, 매수매도: {GenieConfig.combo_rebalance_jumun_F}, 취소시간: {GenieConfig.MTB_rebalance_Cancel_time_F}, 감시시점: {체크박스(GenieConfig.CB_rebalance_option_F)}, 주문조건_1차: {GenieConfig.TB_rebalance_sellratio1_F}, 주문조건선택_1차: \"{GenieConfig.리밸매도기준1_F}\", 매도비중_1차: {GenieConfig.TB_rebalance_sellvolume1_F}, 취소시간_1차: {GenieConfig.TB_rebalance_sellcancel1_F}, 주문조건_2차: {GenieConfig.TB_rebalance_sellratio2_F}, 주문조건선택_2차: \"{GenieConfig.리밸매도기준2_F}\", 매도비중_2차: {GenieConfig.TB_rebalance_sellvolume2_F}, 취소시간_2차: {GenieConfig.TB_rebalance_sellcancel2_F}, 매도체크: {체크박스(GenieConfig.CB_rebalance_매도체크_F)}, 감시주문시간: {GenieConfig.CBB_rebalance_Selltime_F}, 감시주문값: {GenieConfig.TB_rebalance_감시_value_F}, 감시매수매도: {GenieConfig.combo_rebalance_감시_jumun_F}, TS_1차: {체크박스(GenieConfig.CB_rebalance_TS_1차_F)}, TS_1차_down: {GenieConfig.TB_rebalance_TS_1차_down_F}, TB_1차_이평: {GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_F}, CBB_1차_이평: {GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_F}, TS_2차: {체크박스(GenieConfig.CB_rebalance_TS_2차_F)}, TS_2차_down: {GenieConfig.TB_rebalance_TS_2차_down_F}, TB_2차_이평: {GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_F}, CBB_2차_이평: {GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_F});");

                string 검색식ReG = Form1.위치별검색식리스트.ContainsKey("리밸_G") ? $"\"{Form1.위치별검색식리스트["리밸_G"].이름}\"" : "\"\"";
                sb.AppendLine($"계좌관리_리밸런싱_G(사용: {체크박스(GenieConfig.CB_rebalance_G)}, 시작시간: {GenieConfig.MT_rebalance_starttime_G}, 종료시간: {GenieConfig.MT_rebalance_stoptime_G}, 검색식사용: {GenieConfig.combo_rebalance_use_condition_G}, 검색식: {검색식ReG}, 검색유지시간: {GenieConfig.MTB_rebalance_delay_G}, 매입금: {GenieConfig.TB_Rebalance_매입금_G}, 누적거래량: {GenieConfig.TB_rebalance_누적거래량_G}, 누적거래대금: {GenieConfig.TB_rebalance_누적거래대금_G}, TB_mma1: {GenieConfig.TB_rebalance_MinMAPeriod1_G}, CBB_mma1: {GenieConfig.CBB_rebalance_MinMAPeriod1_G}, TB_mma2: {GenieConfig.TB_rebalance_MinMAPeriod2_G}, CBB_mma2: {GenieConfig.CBB_rebalance_MinMAPeriod2_G}, CBB_mma_배열: {GenieConfig.CBB_rebalance_MinMAPeriod1_배열_G}, TB_dma1: {GenieConfig.TB_rebalance_DayMAPeriod1_G}, CBB_dma1: {GenieConfig.CBB_rebalance_DayMAPeriod1_G}, TB_dma2: {GenieConfig.TB_rebalance_DayMAPeriod2_G}, CBB_dma2: {GenieConfig.CBB_rebalance_DayMAPeriod2_G}, CBB_dma_배열: {GenieConfig.CBB_rebalance_DayMAPeriod_배열_G}, 수익범위1: {GenieConfig.TB_rebalance_suik_1_G}, 수익범위선택: {체크박스(GenieConfig.CB_rebalance_choice_G)}, 수익범위2: {GenieConfig.TB_rebalance_suik_2_G}, 수익구분: {GenieConfig.combo_rebalance_suik_gubun_G}, 매수비중: {GenieConfig.TB_rebalance_sell_ratio_G}, 매수구분: {GenieConfig.combo_rebalance_sell_gubun_G}, 매매범위1: {GenieConfig.TB_rebalance_maemae_1_G}, 매매범위2: {GenieConfig.TB_rebalance_maemae_2_G}, 매매범위기준: {GenieConfig.combo_rebalance_maemae_gubun_G}, 반복시간: {GenieConfig.MT_rebalance_repeat_time_G}, 주문가격: {GenieConfig.TB_rebalance_value_G}, 매수매도: {GenieConfig.combo_rebalance_jumun_G}, 취소시간: {GenieConfig.MTB_rebalance_Cancel_time_G}, 감시시점: {체크박스(GenieConfig.CB_rebalance_option_G)}, 주문조건_1차: {GenieConfig.TB_rebalance_sellratio1_G}, 주문조건선택_1차: \"{GenieConfig.리밸매도기준1_G}\", 매도비중_1차: {GenieConfig.TB_rebalance_sellvolume1_G}, 취소시간_1차: {GenieConfig.TB_rebalance_sellcancel1_G}, 주문조건_2차: {GenieConfig.TB_rebalance_sellratio2_G}, 주문조건선택_2차: \"{GenieConfig.리밸매도기준2_G}\", 매도비중_2차: {GenieConfig.TB_rebalance_sellvolume2_G}, 취소시간_2차: {GenieConfig.TB_rebalance_sellcancel2_G}, 매도체크: {체크박스(GenieConfig.CB_rebalance_매도체크_G)}, 감시주문시간: {GenieConfig.CBB_rebalance_Selltime_G}, 감시주문값: {GenieConfig.TB_rebalance_감시_value_G}, 감시매수매도: {GenieConfig.combo_rebalance_감시_jumun_G}, TS_1차: {체크박스(GenieConfig.CB_rebalance_TS_1차_G)}, TS_1차_down: {GenieConfig.TB_rebalance_TS_1차_down_G}, TB_1차_이평: {GenieConfig.TB_rebalance_TS_1차_MinMAPeriod_G}, CBB_1차_이평: {GenieConfig.CBB_rebalance_TS_1차_MinMAPeriod_G}, TS_2차: {체크박스(GenieConfig.CB_rebalance_TS_2차_G)}, TS_2차_down: {GenieConfig.TB_rebalance_TS_2차_down_G}, TB_2차_이평: {GenieConfig.TB_rebalance_TS_2차_MinMAPeriod_G}, CBB_2차_이평: {GenieConfig.CBB_rebalance_TS_2차_MinMAPeriod_G});");
                sb.AppendLine();

                // =========================================================================
                // 3. 계좌관리 잔고청산 (A ~ C)
                // =========================================================================
                string 검색식LiqA = Form1.위치별검색식리스트.ContainsKey("청산_A") ? $"\"{Form1.위치별검색식리스트["청산_A"].이름}\"" : "\"\"";
                sb.AppendLine($"계좌관리_잔고청산_A(사용: {체크박스(GenieConfig.CB_Liquidation_A)}, 시작시간: {GenieConfig.MTB_Liquidation_Starttime_A}, 종료시간: {GenieConfig.MTB_Liquidation_Stoptime_A}, 검색식사용: {GenieConfig.CBB_Liquidation_use_condition_A}, 검색식: {검색식LiqA}, 검색유지시간: {GenieConfig.MTB_Liquidation_delay_A}, 매입금1: {GenieConfig.TB_잔고청산_매입금1_A}, 매입금2: {GenieConfig.TB_잔고청산_매입금2_A}, TB이평: {GenieConfig.TB_Liquidation_MinMAPeriod_A}, CBB이평: {GenieConfig.CBB_Liquidation_MinMAPeriod_A}, 수익범위1: {GenieConfig.TB_Liquidation_suik_1_A}, 수익범위선택: {체크박스(GenieConfig.CB_Liquidation_choice_A)}, 수익범위2: {GenieConfig.TB_Liquidation_suik_2_A}, 수익구분: {GenieConfig.CBB_Liquidation_suik_gubun_A}, 매도비중: {GenieConfig.TB_Liquidation_sell_ratio_A}, 매도구분: {GenieConfig.CBB_Liquidation_sell_gubun_A}, 매매범위1: {GenieConfig.TB_Liquidation_maemae_1_A}, 매매범위2: {GenieConfig.TB_Liquidation_maemae_2_A}, 반복시간: {GenieConfig.MT_Liquidation_repeat_time_A}, 주문가격: {GenieConfig.TB_Liquidation_value_A}, 매수매도: {GenieConfig.CBB_Liquidation_jumun_A}, 취소시간: {GenieConfig.MTB_Liquidation_Cancel_time_A}, 취소후주문: {GenieConfig.CBB_Liquidation_Cancel_A}, 반복횟수: {GenieConfig.MTB_Liquidation_repeat_A}, 매도정지: {체크박스(GenieConfig.CB_Liquidation_SellStop_A)}, 추매금지: {체크박스(GenieConfig.CB_추매금지_Liquidation_A)}, 강제매도: {체크박스(GenieConfig.CB_Liquidation_강제매도_A)}, 수익보전: {체크박스(GenieConfig.CB_수익보전_Liquidation_A)}, TS: {체크박스(GenieConfig.CB_Liquidation_TS_A)}, TS_down: {GenieConfig.TB_Liquidation_TS_down_A}, TB_TS_mma: {GenieConfig.TB_Liquidation_TS_MinMAPeriod_A}, CBB_TS_mma: {GenieConfig.CBB_Liquidation_TS_MinMAPeriod_A}, TB_TS_dma: {GenieConfig.TB_Liquidation_TS_DayMAPeriod_A}, CBB_TS_dma: {GenieConfig.CBB_Liquidation_TS_DayMAPeriod_A});");

                string 검색식LiqB = Form1.위치별검색식리스트.ContainsKey("청산_B") ? $"\"{Form1.위치별검색식리스트["청산_B"].이름}\"" : "\"\"";
                sb.AppendLine($"계좌관리_잔고청산_B(사용: {체크박스(GenieConfig.CB_Liquidation_B)}, 시작시간: {GenieConfig.MTB_Liquidation_Starttime_B}, 종료시간: {GenieConfig.MTB_Liquidation_Stoptime_B}, 검색식사용: {GenieConfig.CBB_Liquidation_use_condition_B}, 검색식: {검색식LiqB}, 검색유지시간: {GenieConfig.MTB_Liquidation_delay_B}, 매입금1: {GenieConfig.TB_잔고청산_매입금1_B}, 매입금2: {GenieConfig.TB_잔고청산_매입금2_B}, TB이평: {GenieConfig.TB_Liquidation_MinMAPeriod_B}, CBB이평: {GenieConfig.CBB_Liquidation_MinMAPeriod_B}, 수익범위1: {GenieConfig.TB_Liquidation_suik_1_B}, 수익범위선택: {체크박스(GenieConfig.CB_Liquidation_choice_B)}, 수익범위2: {GenieConfig.TB_Liquidation_suik_2_B}, 수익구분: {GenieConfig.CBB_Liquidation_suik_gubun_B}, 매도비중: {GenieConfig.TB_Liquidation_sell_ratio_B}, 매도구분: {GenieConfig.CBB_Liquidation_sell_gubun_B}, 매매범위1: {GenieConfig.TB_Liquidation_maemae_1_B}, 매매범위2: {GenieConfig.TB_Liquidation_maemae_2_B}, 반복시간: {GenieConfig.MT_Liquidation_repeat_time_B}, 주문가격: {GenieConfig.TB_Liquidation_value_B}, 매수매도: {GenieConfig.CBB_Liquidation_jumun_B}, 취소시간: {GenieConfig.MTB_Liquidation_Cancel_time_B}, 취소후주문: {GenieConfig.CBB_Liquidation_Cancel_B}, 반복횟수: {GenieConfig.MTB_Liquidation_repeat_B}, 매도정지: {체크박스(GenieConfig.CB_Liquidation_SellStop_B)}, 추매금지: {체크박스(GenieConfig.CB_추매금지_Liquidation_B)}, 강제매도: {체크박스(GenieConfig.CB_Liquidation_강제매도_B)}, 수익보전: {체크박스(GenieConfig.CB_수익보전_Liquidation_B)}, TS: {체크박스(GenieConfig.CB_Liquidation_TS_B)}, TS_down: {GenieConfig.TB_Liquidation_TS_down_B}, TB_TS_mma: {GenieConfig.TB_Liquidation_TS_MinMAPeriod_B}, CBB_TS_mma: {GenieConfig.CBB_Liquidation_TS_MinMAPeriod_B}, TB_TS_dma: {GenieConfig.TB_Liquidation_TS_DayMAPeriod_B}, CBB_TS_dma: {GenieConfig.CBB_Liquidation_TS_DayMAPeriod_B});");

                string 검색식LiqC = Form1.위치별검색식리스트.ContainsKey("청산_C") ? $"\"{Form1.위치별검색식리스트["청산_C"].이름}\"" : "\"\"";
                sb.AppendLine($"계좌관리_잔고청산_C(사용: {체크박스(GenieConfig.CB_Liquidation_C)}, 시작시간: {GenieConfig.MTB_Liquidation_Starttime_C}, 종료시간: {GenieConfig.MTB_Liquidation_Stoptime_C}, 검색식사용: {GenieConfig.CBB_Liquidation_use_condition_C}, 검색식: {검색식LiqC}, 검색유지시간: {GenieConfig.MTB_Liquidation_delay_C}, 매입금1: {GenieConfig.TB_잔고청산_매입금1_C}, 매입금2: {GenieConfig.TB_잔고청산_매입금2_C}, TB이평: {GenieConfig.TB_Liquidation_MinMAPeriod_C}, CBB이평: {GenieConfig.CBB_Liquidation_MinMAPeriod_C}, 수익범위1: {GenieConfig.TB_Liquidation_suik_1_C}, 수익범위선택: {체크박스(GenieConfig.CB_Liquidation_choice_C)}, 수익범위2: {GenieConfig.TB_Liquidation_suik_2_C}, 수익구분: {GenieConfig.CBB_Liquidation_suik_gubun_C}, 매도비중: {GenieConfig.TB_Liquidation_sell_ratio_C}, 매도구분: {GenieConfig.CBB_Liquidation_sell_gubun_C}, 매매범위1: {GenieConfig.TB_Liquidation_maemae_1_C}, 매매범위2: {GenieConfig.TB_Liquidation_maemae_2_C}, 반복시간: {GenieConfig.MT_Liquidation_repeat_time_C}, 주문가격: {GenieConfig.TB_Liquidation_value_C}, 매수매도: {GenieConfig.CBB_Liquidation_jumun_C}, 취소시간: {GenieConfig.MTB_Liquidation_Cancel_time_C}, 취소후주문: {GenieConfig.CBB_Liquidation_Cancel_C}, 반복횟수: {GenieConfig.MTB_Liquidation_repeat_C}, 매도정지: {체크박스(GenieConfig.CB_Liquidation_SellStop_C)}, 추매금지: {체크박스(GenieConfig.CB_추매금지_Liquidation_C)}, 강제매도: {체크박스(GenieConfig.CB_Liquidation_강제매도_C)}, 수익보전: {체크박스(GenieConfig.CB_수익보전_Liquidation_C)}, TS: {체크박스(GenieConfig.CB_Liquidation_TS_C)}, TS_down: {GenieConfig.TB_Liquidation_TS_down_C}, TB_TS_mma: {GenieConfig.TB_Liquidation_TS_MinMAPeriod_C}, CBB_TS_mma: {GenieConfig.CBB_Liquidation_TS_MinMAPeriod_C}, TB_TS_dma: {GenieConfig.TB_Liquidation_TS_DayMAPeriod_C}, CBB_TS_dma: {GenieConfig.CBB_Liquidation_TS_DayMAPeriod_C});");
                sb.AppendLine();

                // =========================================================================
                // 4. 실현손익 담보 손실매도 (A ~ C)
                // =========================================================================
                sb.AppendLine($"계좌관리_실현손익담보손실매도_A(CB_cut: {체크박스(GenieConfig.CB_cut_A)}, MTB_cut_time: {GenieConfig.MTB_cut_time_A}, TB_cut_수익금1: {GenieConfig.TB_cut_수익금1_A}, TB_cut_수익금2: {GenieConfig.TB_cut_수익금2_A}, TB_cut_남길퍼: {GenieConfig.TB_cut_남길퍼_A}, TB_cut_won: {GenieConfig.TB_cut_won_A}, TB_cut_P: {GenieConfig.TB_cut_P_A}, TB_cut_ratio: {GenieConfig.TB_cut_ratio_A}, CBB_cut_gubun: {GenieConfig.CBB_cut_gubun_A}, TB_cut_value: {GenieConfig.TB_cut_value_A}, CBB_cut_jumun: {GenieConfig.CBB_cut_jumun_A}, MTB_cut_cansel_time: {GenieConfig.MTB_cut_cansel_time_A});");
                sb.AppendLine($"계좌관리_실현손익담보손실매도_B(CB_cut: {체크박스(GenieConfig.CB_cut_B)}, MTB_cut_time: {GenieConfig.MTB_cut_time_B}, TB_cut_수익금1: {GenieConfig.TB_cut_수익금1_B}, TB_cut_수익금2: {GenieConfig.TB_cut_수익금2_B}, TB_cut_남길퍼: {GenieConfig.TB_cut_남길퍼_B}, TB_cut_won: {GenieConfig.TB_cut_won_B}, TB_cut_P: {GenieConfig.TB_cut_P_B}, TB_cut_ratio: {GenieConfig.TB_cut_ratio_B}, CBB_cut_gubun: {GenieConfig.CBB_cut_gubun_B}, TB_cut_value: {GenieConfig.TB_cut_value_B}, CBB_cut_jumun: {GenieConfig.CBB_cut_jumun_B}, MTB_cut_cansel_time: {GenieConfig.MTB_cut_cansel_time_B});");
                sb.AppendLine($"계좌관리_실현손익담보손실매도_C(CB_cut: {체크박스(GenieConfig.CB_cut_C)}, MTB_cut_time: {GenieConfig.MTB_cut_time_C}, TB_cut_수익금1: {GenieConfig.TB_cut_수익금1_C}, TB_cut_수익금2: {GenieConfig.TB_cut_수익금2_C}, TB_cut_남길퍼: {GenieConfig.TB_cut_남길퍼_C}, TB_cut_won: {GenieConfig.TB_cut_won_C}, TB_cut_P: {GenieConfig.TB_cut_P_C}, TB_cut_ratio: {GenieConfig.TB_cut_ratio_C}, CBB_cut_gubun: {GenieConfig.CBB_cut_gubun_C}, TB_cut_value: {GenieConfig.TB_cut_value_C}, CBB_cut_jumun: {GenieConfig.CBB_cut_jumun_C}, MTB_cut_cansel_time: {GenieConfig.MTB_cut_cansel_time_C});");

                // [최종 저장]
                GenieConfig.계좌관리설정 = sb.ToString();
            }
            void 특수설정()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                // 1. [특수설정_신규그룹] (A, B, C 지정)
                sb.AppendLine("특수설정_신규그룹(");
                sb.AppendLine($"    A: {GenieConfig.combo_신규그룹_A}, B: {GenieConfig.combo_신규그룹_B}, C: {GenieConfig.combo_신규그룹_C}");
                sb.AppendLine(");\n");

                // 2. [특수설정_신규그룹] (주문 시간 설정)
                sb.AppendLine("특수설정_신규그룹(");
                sb.AppendLine($"    기준금: {체크박스(GenieConfig.CB_매매기간_기준금)}, CB_매매기간_오전: {체크박스(GenieConfig.CB_매매기간_오전)}, TB_매매기간_오전주문시간: {GenieConfig.TB_매매기간_오전주문시간}, CB_매매기간_오후: {체크박스(GenieConfig.CB_매매기간_오후)}, TB_매매기간_오후주문시간: {GenieConfig.TB_매매기간_오후주문시간}");
                sb.AppendLine(");\n");

                // =========================================================================
                // 3. 특수설정 매매기간주문 (A ~ F) 
                // =========================================================================
                sb.AppendLine($"특수설정_매매기간주문_A(trading: {GenieConfig.CBB_매매기간_trading_A}, 기간: {GenieConfig.MTB_매매기간_기간_A}, 주문시간: {GenieConfig.CBB_매매기간_주문시간_A}, 기준: {GenieConfig.TB_매매기간_기준_A}, 기준방법: {GenieConfig.CBB_매매기간_기준_A}, 매도비중: {GenieConfig.TB_매매기간_ratio_A}, 매도방법: {GenieConfig.CBB_매매기간_choice_A}, 주문가격: {GenieConfig.TB_매매기간_value_A}, 주문방법: {GenieConfig.CBB_매매기간_Jumun_A}, 취소시간: {GenieConfig.TB_매매기간_취소시간_A}, 강제매도: {체크박스(GenieConfig.CB_매매기간_강제매도_A)}, 수익보전: {체크박스(GenieConfig.CB_매매기간_수익보전_A)}, TS: {체크박스(GenieConfig.CB_매매기간_TS_A)}, TS_down: {GenieConfig.TB_매매기간_TS_down_A}, TB_TS_mma: {GenieConfig.TB_매매기간_TS_MinMAPeriod_A}, CBB_TS_mma: {GenieConfig.CBB_매매기간_TS_MinMAPeriod_A}, TB_TS_dma: {GenieConfig.TB_매매기간_TS_DayMAPeriod_A}, CBB_TS_dma: {GenieConfig.CBB_매매기간_TS_DayMAPeriod_A});");
                sb.AppendLine($"특수설정_매매기간주문_B(trading: {GenieConfig.CBB_매매기간_trading_B}, 기간: {GenieConfig.MTB_매매기간_기간_B}, 주문시간: {GenieConfig.CBB_매매기간_주문시간_B}, 기준: {GenieConfig.TB_매매기간_기준_B}, 기준방법: {GenieConfig.CBB_매매기간_기준_B}, 매도비중: {GenieConfig.TB_매매기간_ratio_B}, 매도방법: {GenieConfig.CBB_매매기간_choice_B}, 주문가격: {GenieConfig.TB_매매기간_value_B}, 주문방법: {GenieConfig.CBB_매매기간_Jumun_B}, 취소시간: {GenieConfig.TB_매매기간_취소시간_B}, 강제매도: {체크박스(GenieConfig.CB_매매기간_강제매도_B)}, 수익보전: {체크박스(GenieConfig.CB_매매기간_수익보전_B)}, TS: {체크박스(GenieConfig.CB_매매기간_TS_B)}, TS_down: {GenieConfig.TB_매매기간_TS_down_B}, TB_TS_mma: {GenieConfig.TB_매매기간_TS_MinMAPeriod_B}, CBB_TS_mma: {GenieConfig.CBB_매매기간_TS_MinMAPeriod_B}, TB_TS_dma: {GenieConfig.TB_매매기간_TS_DayMAPeriod_B}, CBB_TS_dma: {GenieConfig.CBB_매매기간_TS_DayMAPeriod_B});");
                sb.AppendLine($"특수설정_매매기간주문_C(trading: {GenieConfig.CBB_매매기간_trading_C}, 기간: {GenieConfig.MTB_매매기간_기간_C}, 주문시간: {GenieConfig.CBB_매매기간_주문시간_C}, 기준: {GenieConfig.TB_매매기간_기준_C}, 기준방법: {GenieConfig.CBB_매매기간_기준_C}, 매도비중: {GenieConfig.TB_매매기간_ratio_C}, 매도방법: {GenieConfig.CBB_매매기간_choice_C}, 주문가격: {GenieConfig.TB_매매기간_value_C}, 주문방법: {GenieConfig.CBB_매매기간_Jumun_C}, 취소시간: {GenieConfig.TB_매매기간_취소시간_C}, 강제매도: {체크박스(GenieConfig.CB_매매기간_강제매도_C)}, 수익보전: {체크박스(GenieConfig.CB_매매기간_수익보전_C)}, TS: {체크박스(GenieConfig.CB_매매기간_TS_C)}, TS_down: {GenieConfig.TB_매매기간_TS_down_C}, TB_TS_mma: {GenieConfig.TB_매매기간_TS_MinMAPeriod_C}, CBB_TS_mma: {GenieConfig.CBB_매매기간_TS_MinMAPeriod_C}, TB_TS_dma: {GenieConfig.TB_매매기간_TS_DayMAPeriod_C}, CBB_TS_dma: {GenieConfig.CBB_매매기간_TS_DayMAPeriod_C});");
                sb.AppendLine($"특수설정_매매기간주문_D(trading: {GenieConfig.CBB_매매기간_trading_D}, 기간: {GenieConfig.MTB_매매기간_기간_D}, 주문시간: {GenieConfig.CBB_매매기간_주문시간_D}, 기준: {GenieConfig.TB_매매기간_기준_D}, 기준방법: {GenieConfig.CBB_매매기간_기준_D}, 매도비중: {GenieConfig.TB_매매기간_ratio_D}, 매도방법: {GenieConfig.CBB_매매기간_choice_D}, 주문가격: {GenieConfig.TB_매매기간_value_D}, 주문방법: {GenieConfig.CBB_매매기간_Jumun_D}, 취소시간: {GenieConfig.TB_매매기간_취소시간_D}, 강제매도: {체크박스(GenieConfig.CB_매매기간_강제매도_D)}, 수익보전: {체크박스(GenieConfig.CB_매매기간_수익보전_D)}, TS: {체크박스(GenieConfig.CB_매매기간_TS_D)}, TS_down: {GenieConfig.TB_매매기간_TS_down_D}, TB_TS_mma: {GenieConfig.TB_매매기간_TS_MinMAPeriod_D}, CBB_TS_mma: {GenieConfig.CBB_매매기간_TS_MinMAPeriod_D}, TB_TS_dma: {GenieConfig.TB_매매기간_TS_DayMAPeriod_D}, CBB_TS_dma: {GenieConfig.CBB_매매기간_TS_DayMAPeriod_D});");
                sb.AppendLine($"특수설정_매매기간주문_E(trading: {GenieConfig.CBB_매매기간_trading_E}, 기간: {GenieConfig.MTB_매매기간_기간_E}, 주문시간: {GenieConfig.CBB_매매기간_주문시간_E}, 기준: {GenieConfig.TB_매매기간_기준_E}, 기준방법: {GenieConfig.CBB_매매기간_기준_E}, 매도비중: {GenieConfig.TB_매매기간_ratio_E}, 매도방법: {GenieConfig.CBB_매매기간_choice_E}, 주문가격: {GenieConfig.TB_매매기간_value_E}, 주문방법: {GenieConfig.CBB_매매기간_Jumun_E}, 취소시간: {GenieConfig.TB_매매기간_취소시간_E}, 강제매도: {체크박스(GenieConfig.CB_매매기간_강제매도_E)}, 수익보전: {체크박스(GenieConfig.CB_매매기간_수익보전_E)}, TS: {체크박스(GenieConfig.CB_매매기간_TS_E)}, TS_down: {GenieConfig.TB_매매기간_TS_down_E}, TB_TS_mma: {GenieConfig.TB_매매기간_TS_MinMAPeriod_E}, CBB_TS_mma: {GenieConfig.CBB_매매기간_TS_MinMAPeriod_E}, TB_TS_dma: {GenieConfig.TB_매매기간_TS_DayMAPeriod_E}, CBB_TS_dma: {GenieConfig.CBB_매매기간_TS_DayMAPeriod_E});");
                sb.AppendLine($"특수설정_매매기간주문_F(trading: {GenieConfig.CBB_매매기간_trading_F}, 기간: {GenieConfig.MTB_매매기간_기간_F}, 주문시간: {GenieConfig.CBB_매매기간_주문시간_F}, 기준: {GenieConfig.TB_매매기간_기준_F}, 기준방법: {GenieConfig.CBB_매매기간_기준_F}, 매도비중: {GenieConfig.TB_매매기간_ratio_F}, 매도방법: {GenieConfig.CBB_매매기간_choice_F}, 주문가격: {GenieConfig.TB_매매기간_value_F}, 주문방법: {GenieConfig.CBB_매매기간_Jumun_F}, 취소시간: {GenieConfig.TB_매매기간_취소시간_F}, 강제매도: {체크박스(GenieConfig.CB_매매기간_강제매도_F)}, 수익보전: {체크박스(GenieConfig.CB_매매기간_수익보전_F)}, TS: {체크박스(GenieConfig.CB_매매기간_TS_F)}, TS_down: {GenieConfig.TB_매매기간_TS_down_F}, TB_TS_mma: {GenieConfig.TB_매매기간_TS_MinMAPeriod_F}, CBB_TS_mma: {GenieConfig.CBB_매매기간_TS_MinMAPeriod_F}, TB_TS_dma: {GenieConfig.TB_매매기간_TS_DayMAPeriod_F}, CBB_TS_dma: {GenieConfig.CBB_매매기간_TS_DayMAPeriod_F});");

                // [최종 저장]
                GenieConfig.특수설정 = sb.ToString();
            }

            // ---------------------------------------------------------
            // 매매 그룹 설정 스크립트 생성
            // ---------------------------------------------------------
            void 매매그룹설정()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                // 1. 공통 매매 설정
                sb.AppendLine($"매매그룹설정_익절(A: {체크박스(GenieConfig.CB_IK_group_A)}, B: {체크박스(GenieConfig.CB_IK_group_B)}, C: {체크박스(GenieConfig.CB_IK_group_C)}, D: {체크박스(GenieConfig.CB_IK_group_D)}, E: {체크박스(GenieConfig.CB_IK_group_E)}, F: {체크박스(GenieConfig.CB_IK_group_F)}, G: {체크박스(GenieConfig.CB_IK_group_G)}, H: {체크박스(GenieConfig.CB_IK_group_H)}, I: {체크박스(GenieConfig.CB_IK_group_I)}, J: {체크박스(GenieConfig.CB_IK_group_J)}, K: {체크박스(GenieConfig.CB_IK_group_K)}, L: {체크박스(GenieConfig.CB_IK_group_L)});");
                sb.AppendLine($"매매그룹설정_손절(A: {체크박스(GenieConfig.CB_손절_A)}, B: {체크박스(GenieConfig.CB_손절_B)}, C: {체크박스(GenieConfig.CB_손절_C)}, D: {체크박스(GenieConfig.CB_손절_D)}, E: {체크박스(GenieConfig.CB_손절_E)}, F: {체크박스(GenieConfig.CB_손절_F)}, G: {체크박스(GenieConfig.CB_손절_G)}, H: {체크박스(GenieConfig.CB_손절_H)}, I: {체크박스(GenieConfig.CB_손절_I)}, J: {체크박스(GenieConfig.CB_손절_J)}, K: {체크박스(GenieConfig.CB_손절_K)}, L: {체크박스(GenieConfig.CB_손절_L)});");
                sb.AppendLine();

                // 2. 반복매매 (A ~ N)
                sb.AppendLine($"매매그룹설정_반복A(A: {체크박스(GenieConfig.CB_Repeat_A_A)}, B: {체크박스(GenieConfig.CB_Repeat_A_B)}, C: {체크박스(GenieConfig.CB_Repeat_A_C)}, D: {체크박스(GenieConfig.CB_Repeat_A_D)}, E: {체크박스(GenieConfig.CB_Repeat_A_E)}, F: {체크박스(GenieConfig.CB_Repeat_A_F)}, G: {체크박스(GenieConfig.CB_Repeat_A_G)}, H: {체크박스(GenieConfig.CB_Repeat_A_H)}, I: {체크박스(GenieConfig.CB_Repeat_A_I)}, J: {체크박스(GenieConfig.CB_Repeat_A_J)}, K: {체크박스(GenieConfig.CB_Repeat_A_K)}, L: {체크박스(GenieConfig.CB_Repeat_A_L)});");
                sb.AppendLine($"매매그룹설정_반복B(A: {체크박스(GenieConfig.CB_Repeat_B_A)}, B: {체크박스(GenieConfig.CB_Repeat_B_B)}, C: {체크박스(GenieConfig.CB_Repeat_B_C)}, D: {체크박스(GenieConfig.CB_Repeat_B_D)}, E: {체크박스(GenieConfig.CB_Repeat_B_E)}, F: {체크박스(GenieConfig.CB_Repeat_B_F)}, G: {체크박스(GenieConfig.CB_Repeat_B_G)}, H: {체크박스(GenieConfig.CB_Repeat_B_H)}, I: {체크박스(GenieConfig.CB_Repeat_B_I)}, J: {체크박스(GenieConfig.CB_Repeat_B_J)}, K: {체크박스(GenieConfig.CB_Repeat_B_K)}, L: {체크박스(GenieConfig.CB_Repeat_B_L)});");
                sb.AppendLine($"매매그룹설정_반복C(A: {체크박스(GenieConfig.CB_Repeat_C_A)}, B: {체크박스(GenieConfig.CB_Repeat_C_B)}, C: {체크박스(GenieConfig.CB_Repeat_C_C)}, D: {체크박스(GenieConfig.CB_Repeat_C_D)}, E: {체크박스(GenieConfig.CB_Repeat_C_E)}, F: {체크박스(GenieConfig.CB_Repeat_C_F)}, G: {체크박스(GenieConfig.CB_Repeat_C_G)}, H: {체크박스(GenieConfig.CB_Repeat_C_H)}, I: {체크박스(GenieConfig.CB_Repeat_C_I)}, J: {체크박스(GenieConfig.CB_Repeat_C_J)}, K: {체크박스(GenieConfig.CB_Repeat_C_K)}, L: {체크박스(GenieConfig.CB_Repeat_C_L)});");
                sb.AppendLine($"매매그룹설정_반복D(A: {체크박스(GenieConfig.CB_Repeat_D_A)}, B: {체크박스(GenieConfig.CB_Repeat_D_B)}, C: {체크박스(GenieConfig.CB_Repeat_D_C)}, D: {체크박스(GenieConfig.CB_Repeat_D_D)}, E: {체크박스(GenieConfig.CB_Repeat_D_E)}, F: {체크박스(GenieConfig.CB_Repeat_D_F)}, G: {체크박스(GenieConfig.CB_Repeat_D_G)}, H: {체크박스(GenieConfig.CB_Repeat_D_H)}, I: {체크박스(GenieConfig.CB_Repeat_D_I)}, J: {체크박스(GenieConfig.CB_Repeat_D_J)}, K: {체크박스(GenieConfig.CB_Repeat_D_K)}, L: {체크박스(GenieConfig.CB_Repeat_D_L)});");
                sb.AppendLine($"매매그룹설정_반복E(A: {체크박스(GenieConfig.CB_Repeat_E_A)}, B: {체크박스(GenieConfig.CB_Repeat_E_B)}, C: {체크박스(GenieConfig.CB_Repeat_E_C)}, D: {체크박스(GenieConfig.CB_Repeat_E_D)}, E: {체크박스(GenieConfig.CB_Repeat_E_E)}, F: {체크박스(GenieConfig.CB_Repeat_E_F)}, G: {체크박스(GenieConfig.CB_Repeat_E_G)}, H: {체크박스(GenieConfig.CB_Repeat_E_H)}, I: {체크박스(GenieConfig.CB_Repeat_E_I)}, J: {체크박스(GenieConfig.CB_Repeat_E_J)}, K: {체크박스(GenieConfig.CB_Repeat_E_K)}, L: {체크박스(GenieConfig.CB_Repeat_E_L)});");
                sb.AppendLine($"매매그룹설정_반복F(A: {체크박스(GenieConfig.CB_Repeat_F_A)}, B: {체크박스(GenieConfig.CB_Repeat_F_B)}, C: {체크박스(GenieConfig.CB_Repeat_F_C)}, D: {체크박스(GenieConfig.CB_Repeat_F_D)}, E: {체크박스(GenieConfig.CB_Repeat_F_E)}, F: {체크박스(GenieConfig.CB_Repeat_F_F)}, G: {체크박스(GenieConfig.CB_Repeat_F_G)}, H: {체크박스(GenieConfig.CB_Repeat_F_H)}, I: {체크박스(GenieConfig.CB_Repeat_F_I)}, J: {체크박스(GenieConfig.CB_Repeat_F_J)}, K: {체크박스(GenieConfig.CB_Repeat_F_K)}, L: {체크박스(GenieConfig.CB_Repeat_F_L)});");
                sb.AppendLine($"매매그룹설정_반복G(A: {체크박스(GenieConfig.CB_Repeat_G_A)}, B: {체크박스(GenieConfig.CB_Repeat_G_B)}, C: {체크박스(GenieConfig.CB_Repeat_G_C)}, D: {체크박스(GenieConfig.CB_Repeat_G_D)}, E: {체크박스(GenieConfig.CB_Repeat_G_E)}, F: {체크박스(GenieConfig.CB_Repeat_G_F)}, G: {체크박스(GenieConfig.CB_Repeat_G_G)}, H: {체크박스(GenieConfig.CB_Repeat_G_H)}, I: {체크박스(GenieConfig.CB_Repeat_G_I)}, J: {체크박스(GenieConfig.CB_Repeat_G_J)}, K: {체크박스(GenieConfig.CB_Repeat_G_K)}, L: {체크박스(GenieConfig.CB_Repeat_G_L)});");
                sb.AppendLine($"매매그룹설정_반복H(A: {체크박스(GenieConfig.CB_Repeat_H_A)}, B: {체크박스(GenieConfig.CB_Repeat_H_B)}, C: {체크박스(GenieConfig.CB_Repeat_H_C)}, D: {체크박스(GenieConfig.CB_Repeat_H_D)}, E: {체크박스(GenieConfig.CB_Repeat_H_E)}, F: {체크박스(GenieConfig.CB_Repeat_H_F)}, G: {체크박스(GenieConfig.CB_Repeat_H_G)}, H: {체크박스(GenieConfig.CB_Repeat_H_H)}, I: {체크박스(GenieConfig.CB_Repeat_H_I)}, J: {체크박스(GenieConfig.CB_Repeat_H_J)}, K: {체크박스(GenieConfig.CB_Repeat_H_K)}, L: {체크박스(GenieConfig.CB_Repeat_H_L)});");
                sb.AppendLine($"매매그룹설정_반복I(A: {체크박스(GenieConfig.CB_Repeat_I_A)}, B: {체크박스(GenieConfig.CB_Repeat_I_B)}, C: {체크박스(GenieConfig.CB_Repeat_I_C)}, D: {체크박스(GenieConfig.CB_Repeat_I_D)}, E: {체크박스(GenieConfig.CB_Repeat_I_E)}, F: {체크박스(GenieConfig.CB_Repeat_I_F)}, G: {체크박스(GenieConfig.CB_Repeat_I_G)}, H: {체크박스(GenieConfig.CB_Repeat_I_H)}, I: {체크박스(GenieConfig.CB_Repeat_I_I)}, J: {체크박스(GenieConfig.CB_Repeat_I_J)}, K: {체크박스(GenieConfig.CB_Repeat_I_K)}, L: {체크박스(GenieConfig.CB_Repeat_I_L)});");
                sb.AppendLine($"매매그룹설정_반복J(A: {체크박스(GenieConfig.CB_Repeat_J_A)}, B: {체크박스(GenieConfig.CB_Repeat_J_B)}, C: {체크박스(GenieConfig.CB_Repeat_J_C)}, D: {체크박스(GenieConfig.CB_Repeat_J_D)}, E: {체크박스(GenieConfig.CB_Repeat_J_E)}, F: {체크박스(GenieConfig.CB_Repeat_J_F)}, G: {체크박스(GenieConfig.CB_Repeat_J_G)}, H: {체크박스(GenieConfig.CB_Repeat_J_H)}, I: {체크박스(GenieConfig.CB_Repeat_J_I)}, J: {체크박스(GenieConfig.CB_Repeat_J_J)}, K: {체크박스(GenieConfig.CB_Repeat_J_K)}, L: {체크박스(GenieConfig.CB_Repeat_J_L)});");
                sb.AppendLine($"매매그룹설정_반복K(A: {체크박스(GenieConfig.CB_Repeat_K_A)}, B: {체크박스(GenieConfig.CB_Repeat_K_B)}, C: {체크박스(GenieConfig.CB_Repeat_K_C)}, D: {체크박스(GenieConfig.CB_Repeat_K_D)}, E: {체크박스(GenieConfig.CB_Repeat_K_E)}, F: {체크박스(GenieConfig.CB_Repeat_K_F)}, G: {체크박스(GenieConfig.CB_Repeat_K_G)}, H: {체크박스(GenieConfig.CB_Repeat_K_H)}, I: {체크박스(GenieConfig.CB_Repeat_K_I)}, J: {체크박스(GenieConfig.CB_Repeat_K_J)}, K: {체크박스(GenieConfig.CB_Repeat_K_K)}, L: {체크박스(GenieConfig.CB_Repeat_K_L)});");
                sb.AppendLine($"매매그룹설정_반복L(A: {체크박스(GenieConfig.CB_Repeat_L_A)}, B: {체크박스(GenieConfig.CB_Repeat_L_B)}, C: {체크박스(GenieConfig.CB_Repeat_L_C)}, D: {체크박스(GenieConfig.CB_Repeat_L_D)}, E: {체크박스(GenieConfig.CB_Repeat_L_E)}, F: {체크박스(GenieConfig.CB_Repeat_L_F)}, G: {체크박스(GenieConfig.CB_Repeat_L_G)}, H: {체크박스(GenieConfig.CB_Repeat_L_H)}, I: {체크박스(GenieConfig.CB_Repeat_L_I)}, J: {체크박스(GenieConfig.CB_Repeat_L_J)}, K: {체크박스(GenieConfig.CB_Repeat_L_K)}, L: {체크박스(GenieConfig.CB_Repeat_L_L)});");
                sb.AppendLine($"매매그룹설정_반복M(A: {체크박스(GenieConfig.CB_Repeat_M_A)}, B: {체크박스(GenieConfig.CB_Repeat_M_B)}, C: {체크박스(GenieConfig.CB_Repeat_M_C)}, D: {체크박스(GenieConfig.CB_Repeat_M_D)}, E: {체크박스(GenieConfig.CB_Repeat_M_E)}, F: {체크박스(GenieConfig.CB_Repeat_M_F)}, G: {체크박스(GenieConfig.CB_Repeat_M_G)}, H: {체크박스(GenieConfig.CB_Repeat_M_H)}, I: {체크박스(GenieConfig.CB_Repeat_M_I)}, J: {체크박스(GenieConfig.CB_Repeat_M_J)}, K: {체크박스(GenieConfig.CB_Repeat_M_K)}, L: {체크박스(GenieConfig.CB_Repeat_M_L)});");
                sb.AppendLine($"매매그룹설정_반복N(A: {체크박스(GenieConfig.CB_Repeat_N_A)}, B: {체크박스(GenieConfig.CB_Repeat_N_B)}, C: {체크박스(GenieConfig.CB_Repeat_N_C)}, D: {체크박스(GenieConfig.CB_Repeat_N_D)}, E: {체크박스(GenieConfig.CB_Repeat_N_E)}, F: {체크박스(GenieConfig.CB_Repeat_N_F)}, G: {체크박스(GenieConfig.CB_Repeat_N_G)}, H: {체크박스(GenieConfig.CB_Repeat_N_H)}, I: {체크박스(GenieConfig.CB_Repeat_N_I)}, J: {체크박스(GenieConfig.CB_Repeat_N_J)}, K: {체크박스(GenieConfig.CB_Repeat_N_K)}, L: {체크박스(GenieConfig.CB_Repeat_N_L)});");
                sb.AppendLine();

                // 3. 리밸런싱 (A ~ G)
                sb.AppendLine($"매매그룹설정_리밸_A(A: {체크박스(GenieConfig.CB_rebalance_A_A)}, B: {체크박스(GenieConfig.CB_rebalance_A_B)}, C: {체크박스(GenieConfig.CB_rebalance_A_C)}, D: {체크박스(GenieConfig.CB_rebalance_A_D)}, E: {체크박스(GenieConfig.CB_rebalance_A_E)}, F: {체크박스(GenieConfig.CB_rebalance_A_F)}, G: {체크박스(GenieConfig.CB_rebalance_A_G)}, H: {체크박스(GenieConfig.CB_rebalance_A_H)}, I: {체크박스(GenieConfig.CB_rebalance_A_I)}, J: {체크박스(GenieConfig.CB_rebalance_A_J)}, K: {체크박스(GenieConfig.CB_rebalance_A_K)}, L: {체크박스(GenieConfig.CB_rebalance_A_L)});");
                sb.AppendLine($"매매그룹설정_리밸_B(A: {체크박스(GenieConfig.CB_rebalance_B_A)}, B: {체크박스(GenieConfig.CB_rebalance_B_B)}, C: {체크박스(GenieConfig.CB_rebalance_B_C)}, D: {체크박스(GenieConfig.CB_rebalance_B_D)}, E: {체크박스(GenieConfig.CB_rebalance_B_E)}, F: {체크박스(GenieConfig.CB_rebalance_B_F)}, G: {체크박스(GenieConfig.CB_rebalance_B_G)}, H: {체크박스(GenieConfig.CB_rebalance_B_H)}, I: {체크박스(GenieConfig.CB_rebalance_B_I)}, J: {체크박스(GenieConfig.CB_rebalance_B_J)}, K: {체크박스(GenieConfig.CB_rebalance_B_K)}, L: {체크박스(GenieConfig.CB_rebalance_B_L)});");
                sb.AppendLine($"매매그룹설정_리밸_C(A: {체크박스(GenieConfig.CB_rebalance_C_A)}, B: {체크박스(GenieConfig.CB_rebalance_C_B)}, C: {체크박스(GenieConfig.CB_rebalance_C_C)}, D: {체크박스(GenieConfig.CB_rebalance_C_D)}, E: {체크박스(GenieConfig.CB_rebalance_C_E)}, F: {체크박스(GenieConfig.CB_rebalance_C_F)}, G: {체크박스(GenieConfig.CB_rebalance_C_G)}, H: {체크박스(GenieConfig.CB_rebalance_C_H)}, I: {체크박스(GenieConfig.CB_rebalance_C_I)}, J: {체크박스(GenieConfig.CB_rebalance_C_J)}, K: {체크박스(GenieConfig.CB_rebalance_C_K)}, L: {체크박스(GenieConfig.CB_rebalance_C_L)});");
                sb.AppendLine($"매매그룹설정_리밸_D(A: {체크박스(GenieConfig.CB_rebalance_D_A)}, B: {체크박스(GenieConfig.CB_rebalance_D_B)}, C: {체크박스(GenieConfig.CB_rebalance_D_C)}, D: {체크박스(GenieConfig.CB_rebalance_D_D)}, E: {체크박스(GenieConfig.CB_rebalance_D_E)}, F: {체크박스(GenieConfig.CB_rebalance_D_F)}, G: {체크박스(GenieConfig.CB_rebalance_D_G)}, H: {체크박스(GenieConfig.CB_rebalance_D_H)}, I: {체크박스(GenieConfig.CB_rebalance_D_I)}, J: {체크박스(GenieConfig.CB_rebalance_D_J)}, K: {체크박스(GenieConfig.CB_rebalance_D_K)}, L: {체크박스(GenieConfig.CB_rebalance_D_L)});");
                sb.AppendLine($"매매그룹설정_리밸_E(A: {체크박스(GenieConfig.CB_rebalance_E_A)}, B: {체크박스(GenieConfig.CB_rebalance_E_B)}, C: {체크박스(GenieConfig.CB_rebalance_E_C)}, D: {체크박스(GenieConfig.CB_rebalance_E_D)}, E: {체크박스(GenieConfig.CB_rebalance_E_E)}, F: {체크박스(GenieConfig.CB_rebalance_E_F)}, G: {체크박스(GenieConfig.CB_rebalance_E_G)}, H: {체크박스(GenieConfig.CB_rebalance_E_H)}, I: {체크박스(GenieConfig.CB_rebalance_E_I)}, J: {체크박스(GenieConfig.CB_rebalance_E_J)}, K: {체크박스(GenieConfig.CB_rebalance_E_K)}, L: {체크박스(GenieConfig.CB_rebalance_E_L)});");
                sb.AppendLine($"매매그룹설정_리밸_F(A: {체크박스(GenieConfig.CB_rebalance_F_A)}, B: {체크박스(GenieConfig.CB_rebalance_F_B)}, C: {체크박스(GenieConfig.CB_rebalance_F_C)}, D: {체크박스(GenieConfig.CB_rebalance_F_D)}, E: {체크박스(GenieConfig.CB_rebalance_F_E)}, F: {체크박스(GenieConfig.CB_rebalance_F_F)}, G: {체크박스(GenieConfig.CB_rebalance_F_G)}, H: {체크박스(GenieConfig.CB_rebalance_F_H)}, I: {체크박스(GenieConfig.CB_rebalance_F_I)}, J: {체크박스(GenieConfig.CB_rebalance_F_J)}, K: {체크박스(GenieConfig.CB_rebalance_F_K)}, L: {체크박스(GenieConfig.CB_rebalance_F_L)});");
                sb.AppendLine($"매매그룹설정_리밸_G(A: {체크박스(GenieConfig.CB_rebalance_G_A)}, B: {체크박스(GenieConfig.CB_rebalance_G_B)}, C: {체크박스(GenieConfig.CB_rebalance_G_C)}, D: {체크박스(GenieConfig.CB_rebalance_G_D)}, E: {체크박스(GenieConfig.CB_rebalance_G_E)}, F: {체크박스(GenieConfig.CB_rebalance_G_F)}, G: {체크박스(GenieConfig.CB_rebalance_G_G)}, H: {체크박스(GenieConfig.CB_rebalance_G_H)}, I: {체크박스(GenieConfig.CB_rebalance_G_I)}, J: {체크박스(GenieConfig.CB_rebalance_G_J)}, K: {체크박스(GenieConfig.CB_rebalance_G_K)}, L: {체크박스(GenieConfig.CB_rebalance_G_L)});");
                sb.AppendLine();

                // 4. 잔고청산 (A ~ C)
                sb.AppendLine($"매매그룹설정_청산_A(A: {체크박스(GenieConfig.CB_Liquidation_A_A)}, B: {체크박스(GenieConfig.CB_Liquidation_A_B)}, C: {체크박스(GenieConfig.CB_Liquidation_A_C)}, D: {체크박스(GenieConfig.CB_Liquidation_A_D)}, E: {체크박스(GenieConfig.CB_Liquidation_A_E)}, F: {체크박스(GenieConfig.CB_Liquidation_A_F)}, G: {체크박스(GenieConfig.CB_Liquidation_A_G)}, H: {체크박스(GenieConfig.CB_Liquidation_A_H)}, I: {체크박스(GenieConfig.CB_Liquidation_A_I)}, J: {체크박스(GenieConfig.CB_Liquidation_A_J)}, K: {체크박스(GenieConfig.CB_Liquidation_A_K)}, L: {체크박스(GenieConfig.CB_Liquidation_A_L)});");
                sb.AppendLine($"매매그룹설정_청산_B(A: {체크박스(GenieConfig.CB_Liquidation_B_A)}, B: {체크박스(GenieConfig.CB_Liquidation_B_B)}, C: {체크박스(GenieConfig.CB_Liquidation_B_C)}, D: {체크박스(GenieConfig.CB_Liquidation_B_D)}, E: {체크박스(GenieConfig.CB_Liquidation_B_E)}, F: {체크박스(GenieConfig.CB_Liquidation_B_F)}, G: {체크박스(GenieConfig.CB_Liquidation_B_G)}, H: {체크박스(GenieConfig.CB_Liquidation_B_H)}, I: {체크박스(GenieConfig.CB_Liquidation_B_I)}, J: {체크박스(GenieConfig.CB_Liquidation_B_J)}, K: {체크박스(GenieConfig.CB_Liquidation_B_K)}, L: {체크박스(GenieConfig.CB_Liquidation_B_L)});");
                sb.AppendLine($"매매그룹설정_청산_C(A: {체크박스(GenieConfig.CB_Liquidation_C_A)}, B: {체크박스(GenieConfig.CB_Liquidation_C_B)}, C: {체크박스(GenieConfig.CB_Liquidation_C_C)}, D: {체크박스(GenieConfig.CB_Liquidation_C_D)}, E: {체크박스(GenieConfig.CB_Liquidation_C_E)}, F: {체크박스(GenieConfig.CB_Liquidation_C_F)}, G: {체크박스(GenieConfig.CB_Liquidation_C_G)}, H: {체크박스(GenieConfig.CB_Liquidation_C_H)}, I: {체크박스(GenieConfig.CB_Liquidation_C_I)}, J: {체크박스(GenieConfig.CB_Liquidation_C_J)}, K: {체크박스(GenieConfig.CB_Liquidation_C_K)}, L: {체크박스(GenieConfig.CB_Liquidation_C_L)});");
                sb.AppendLine();

                // 5. 기간매도 (A ~ F)
                sb.AppendLine($"매매그룹설정_기간매도_A(A: {체크박스(GenieConfig.CB_day_A_A)}, B: {체크박스(GenieConfig.CB_day_A_B)}, C: {체크박스(GenieConfig.CB_day_A_C)}, D: {체크박스(GenieConfig.CB_day_A_D)}, E: {체크박스(GenieConfig.CB_day_A_E)}, F: {체크박스(GenieConfig.CB_day_A_F)}, G: {체크박스(GenieConfig.CB_day_A_G)}, H: {체크박스(GenieConfig.CB_day_A_H)}, I: {체크박스(GenieConfig.CB_day_A_I)}, J: {체크박스(GenieConfig.CB_day_A_J)}, K: {체크박스(GenieConfig.CB_day_A_K)}, L: {체크박스(GenieConfig.CB_day_A_L)});");
                sb.AppendLine($"매매그룹설정_기간매도_B(A: {체크박스(GenieConfig.CB_day_B_A)}, B: {체크박스(GenieConfig.CB_day_B_B)}, C: {체크박스(GenieConfig.CB_day_B_C)}, D: {체크박스(GenieConfig.CB_day_B_D)}, E: {체크박스(GenieConfig.CB_day_B_E)}, F: {체크박스(GenieConfig.CB_day_B_F)}, G: {체크박스(GenieConfig.CB_day_B_G)}, H: {체크박스(GenieConfig.CB_day_B_H)}, I: {체크박스(GenieConfig.CB_day_B_I)}, J: {체크박스(GenieConfig.CB_day_B_J)}, K: {체크박스(GenieConfig.CB_day_B_K)}, L: {체크박스(GenieConfig.CB_day_B_L)});");
                sb.AppendLine($"매매그룹설정_기간매도_C(A: {체크박스(GenieConfig.CB_day_C_A)}, B: {체크박스(GenieConfig.CB_day_C_B)}, C: {체크박스(GenieConfig.CB_day_C_C)}, D: {체크박스(GenieConfig.CB_day_C_D)}, E: {체크박스(GenieConfig.CB_day_C_E)}, F: {체크박스(GenieConfig.CB_day_C_F)}, G: {체크박스(GenieConfig.CB_day_C_G)}, H: {체크박스(GenieConfig.CB_day_C_H)}, I: {체크박스(GenieConfig.CB_day_C_I)}, J: {체크박스(GenieConfig.CB_day_C_J)}, K: {체크박스(GenieConfig.CB_day_C_K)}, L: {체크박스(GenieConfig.CB_day_C_L)});");
                sb.AppendLine($"매매그룹설정_기간매도_D(A: {체크박스(GenieConfig.CB_day_D_A)}, B: {체크박스(GenieConfig.CB_day_D_B)}, C: {체크박스(GenieConfig.CB_day_D_C)}, D: {체크박스(GenieConfig.CB_day_D_D)}, E: {체크박스(GenieConfig.CB_day_D_E)}, F: {체크박스(GenieConfig.CB_day_D_F)}, G: {체크박스(GenieConfig.CB_day_D_G)}, H: {체크박스(GenieConfig.CB_day_D_H)}, I: {체크박스(GenieConfig.CB_day_D_I)}, J: {체크박스(GenieConfig.CB_day_D_J)}, K: {체크박스(GenieConfig.CB_day_D_K)}, L: {체크박스(GenieConfig.CB_day_D_L)});");
                sb.AppendLine($"매매그룹설정_기간매도_E(A: {체크박스(GenieConfig.CB_day_E_A)}, B: {체크박스(GenieConfig.CB_day_E_B)}, C: {체크박스(GenieConfig.CB_day_E_C)}, D: {체크박스(GenieConfig.CB_day_E_D)}, E: {체크박스(GenieConfig.CB_day_E_E)}, F: {체크박스(GenieConfig.CB_day_E_F)}, G: {체크박스(GenieConfig.CB_day_E_G)}, H: {체크박스(GenieConfig.CB_day_E_H)}, I: {체크박스(GenieConfig.CB_day_E_I)}, J: {체크박스(GenieConfig.CB_day_E_J)}, K: {체크박스(GenieConfig.CB_day_E_K)}, L: {체크박스(GenieConfig.CB_day_E_L)});");
                sb.AppendLine($"매매그룹설정_기간매도_F(A: {체크박스(GenieConfig.CB_day_F_A)}, B: {체크박스(GenieConfig.CB_day_F_B)}, C: {체크박스(GenieConfig.CB_day_F_C)}, D: {체크박스(GenieConfig.CB_day_F_D)}, E: {체크박스(GenieConfig.CB_day_F_E)}, F: {체크박스(GenieConfig.CB_day_F_F)}, G: {체크박스(GenieConfig.CB_day_F_G)}, H: {체크박스(GenieConfig.CB_day_F_H)}, I: {체크박스(GenieConfig.CB_day_F_I)}, J: {체크박스(GenieConfig.CB_day_F_J)}, K: {체크박스(GenieConfig.CB_day_F_K)}, L: {체크박스(GenieConfig.CB_day_F_L)});");
                sb.AppendLine();

                // 6. 기타 계좌/시간 관리
                sb.AppendLine($"매매그룹설정_미수금정리(A: {체크박스(GenieConfig.CB_미수금정리_A)}, B: {체크박스(GenieConfig.CB_미수금정리_B)}, C: {체크박스(GenieConfig.CB_미수금정리_C)}, D: {체크박스(GenieConfig.CB_미수금정리_D)}, E: {체크박스(GenieConfig.CB_미수금정리_E)}, F: {체크박스(GenieConfig.CB_미수금정리_F)}, G: {체크박스(GenieConfig.CB_미수금정리_G)}, H: {체크박스(GenieConfig.CB_미수금정리_H)}, I: {체크박스(GenieConfig.CB_미수금정리_I)}, J: {체크박스(GenieConfig.CB_미수금정리_J)}, K: {체크박스(GenieConfig.CB_미수금정리_K)}, L: {체크박스(GenieConfig.CB_미수금정리_L)});");
                sb.AppendLine($"매매그룹설정_손익담보매도_A(A: {체크박스(GenieConfig.CB_Cut_A_A)}, B: {체크박스(GenieConfig.CB_Cut_A_B)}, C: {체크박스(GenieConfig.CB_Cut_A_C)}, D: {체크박스(GenieConfig.CB_Cut_A_D)}, E: {체크박스(GenieConfig.CB_Cut_A_E)}, F: {체크박스(GenieConfig.CB_Cut_A_F)}, G: {체크박스(GenieConfig.CB_Cut_A_G)}, H: {체크박스(GenieConfig.CB_Cut_A_H)}, I: {체크박스(GenieConfig.CB_Cut_A_I)}, J: {체크박스(GenieConfig.CB_Cut_A_J)}, K: {체크박스(GenieConfig.CB_Cut_A_K)}, L: {체크박스(GenieConfig.CB_Cut_A_L)});");
                sb.AppendLine($"매매그룹설정_손익담보매도_B(A: {체크박스(GenieConfig.CB_Cut_B_A)}, B: {체크박스(GenieConfig.CB_Cut_B_B)}, C: {체크박스(GenieConfig.CB_Cut_B_C)}, D: {체크박스(GenieConfig.CB_Cut_B_D)}, E: {체크박스(GenieConfig.CB_Cut_B_E)}, F: {체크박스(GenieConfig.CB_Cut_B_F)}, G: {체크박스(GenieConfig.CB_Cut_B_G)}, H: {체크박스(GenieConfig.CB_Cut_B_H)}, I: {체크박스(GenieConfig.CB_Cut_B_I)}, J: {체크박스(GenieConfig.CB_Cut_B_J)}, K: {체크박스(GenieConfig.CB_Cut_B_K)}, L: {체크박스(GenieConfig.CB_Cut_B_L)});");
                sb.AppendLine($"매매그룹설정_손익담보매도_C(A: {체크박스(GenieConfig.CB_Cut_C_A)}, B: {체크박스(GenieConfig.CB_Cut_C_B)}, C: {체크박스(GenieConfig.CB_Cut_C_C)}, D: {체크박스(GenieConfig.CB_Cut_C_D)}, E: {체크박스(GenieConfig.CB_Cut_C_E)}, F: {체크박스(GenieConfig.CB_Cut_C_F)}, G: {체크박스(GenieConfig.CB_Cut_C_G)}, H: {체크박스(GenieConfig.CB_Cut_C_H)}, I: {체크박스(GenieConfig.CB_Cut_C_I)}, J: {체크박스(GenieConfig.CB_Cut_C_J)}, K: {체크박스(GenieConfig.CB_Cut_C_K)}, L: {체크박스(GenieConfig.CB_Cut_C_L)});");
                sb.AppendLine($"매매그룹설정_계좌청산_특정시간(A: {체크박스(GenieConfig.CB_특정시간_계좌청산_A)}, B: {체크박스(GenieConfig.CB_특정시간_계좌청산_B)}, C: {체크박스(GenieConfig.CB_특정시간_계좌청산_C)}, D: {체크박스(GenieConfig.CB_특정시간_계좌청산_D)}, E: {체크박스(GenieConfig.CB_특정시간_계좌청산_E)}, F: {체크박스(GenieConfig.CB_특정시간_계좌청산_F)}, G: {체크박스(GenieConfig.CB_특정시간_계좌청산_G)}, H: {체크박스(GenieConfig.CB_특정시간_계좌청산_H)}, I: {체크박스(GenieConfig.CB_특정시간_계좌청산_I)}, J: {체크박스(GenieConfig.CB_특정시간_계좌청산_J)}, K: {체크박스(GenieConfig.CB_특정시간_계좌청산_K)}, L: {체크박스(GenieConfig.CB_특정시간_계좌청산_L)});");
                sb.AppendLine($"매매그룹설정_계좌청산_실현손익(A: {체크박스(GenieConfig.CB_실현손익_계좌청산_A)}, B: {체크박스(GenieConfig.CB_실현손익_계좌청산_B)}, C: {체크박스(GenieConfig.CB_실현손익_계좌청산_C)}, D: {체크박스(GenieConfig.CB_실현손익_계좌청산_D)}, E: {체크박스(GenieConfig.CB_실현손익_계좌청산_E)}, F: {체크박스(GenieConfig.CB_실현손익_계좌청산_F)}, G: {체크박스(GenieConfig.CB_실현손익_계좌청산_G)}, H: {체크박스(GenieConfig.CB_실현손익_계좌청산_H)}, I: {체크박스(GenieConfig.CB_실현손익_계좌청산_I)}, J: {체크박스(GenieConfig.CB_실현손익_계좌청산_J)}, K: {체크박스(GenieConfig.CB_실현손익_계좌청산_K)}, L: {체크박스(GenieConfig.CB_실현손익_계좌청산_L)});");
                sb.AppendLine($"매매그룹설정_계좌청산_예상손실(A: {체크박스(GenieConfig.CB_예상손실_계좌청산_A)}, B: {체크박스(GenieConfig.CB_예상손실_계좌청산_B)}, C: {체크박스(GenieConfig.CB_예상손실_계좌청산_C)}, D: {체크박스(GenieConfig.CB_예상손실_계좌청산_D)}, E: {체크박스(GenieConfig.CB_예상손실_계좌청산_E)}, F: {체크박스(GenieConfig.CB_예상손실_계좌청산_F)}, G: {체크박스(GenieConfig.CB_예상손실_계좌청산_G)}, H: {체크박스(GenieConfig.CB_예상손실_계좌청산_H)}, I: {체크박스(GenieConfig.CB_예상손실_계좌청산_I)}, J: {체크박스(GenieConfig.CB_예상손실_계좌청산_J)}, K: {체크박스(GenieConfig.CB_예상손실_계좌청산_K)}, L: {체크박스(GenieConfig.CB_예상손실_계좌청산_L)});");
                sb.AppendLine($"매매그룹설정_계좌청산_예상수익(A: {체크박스(GenieConfig.CB_예상수익_계좌청산_A)}, B: {체크박스(GenieConfig.CB_예상수익_계좌청산_B)}, C: {체크박스(GenieConfig.CB_예상수익_계좌청산_C)}, D: {체크박스(GenieConfig.CB_예상수익_계좌청산_D)}, E: {체크박스(GenieConfig.CB_예상수익_계좌청산_E)}, F: {체크박스(GenieConfig.CB_예상수익_계좌청산_F)}, G: {체크박스(GenieConfig.CB_예상수익_계좌청산_G)}, H: {체크박스(GenieConfig.CB_예상수익_계좌청산_H)}, I: {체크박스(GenieConfig.CB_예상수익_계좌청산_I)}, J: {체크박스(GenieConfig.CB_예상수익_계좌청산_J)}, K: {체크박스(GenieConfig.CB_예상수익_계좌청산_K)}, L: {체크박스(GenieConfig.CB_예상수익_계좌청산_L)});");
                sb.AppendLine($"매매그룹설정_시간청산_A(A: {체크박스(GenieConfig.CB_시간청산A_A)}, B: {체크박스(GenieConfig.CB_시간청산A_B)}, C: {체크박스(GenieConfig.CB_시간청산A_C)}, D: {체크박스(GenieConfig.CB_시간청산A_D)}, E: {체크박스(GenieConfig.CB_시간청산A_E)}, F: {체크박스(GenieConfig.CB_시간청산A_F)}, G: {체크박스(GenieConfig.CB_시간청산A_G)}, H: {체크박스(GenieConfig.CB_시간청산A_H)}, I: {체크박스(GenieConfig.CB_시간청산A_I)}, J: {체크박스(GenieConfig.CB_시간청산A_J)}, K: {체크박스(GenieConfig.CB_시간청산A_K)}, L: {체크박스(GenieConfig.CB_시간청산A_L)});");
                sb.AppendLine($"매매그룹설정_시간청산_B(A: {체크박스(GenieConfig.CB_시간청산B_A)}, B: {체크박스(GenieConfig.CB_시간청산B_B)}, C: {체크박스(GenieConfig.CB_시간청산B_C)}, D: {체크박스(GenieConfig.CB_시간청산B_D)}, E: {체크박스(GenieConfig.CB_시간청산B_E)}, F: {체크박스(GenieConfig.CB_시간청산B_F)}, G: {체크박스(GenieConfig.CB_시간청산B_G)}, H: {체크박스(GenieConfig.CB_시간청산B_H)}, I: {체크박스(GenieConfig.CB_시간청산B_I)}, J: {체크박스(GenieConfig.CB_시간청산B_J)}, K: {체크박스(GenieConfig.CB_시간청산B_K)}, L: {체크박스(GenieConfig.CB_시간청산B_L)});");
                sb.AppendLine($"매매그룹설정_시간청산_C(A: {체크박스(GenieConfig.CB_시간청산C_A)}, B: {체크박스(GenieConfig.CB_시간청산C_B)}, C: {체크박스(GenieConfig.CB_시간청산C_C)}, D: {체크박스(GenieConfig.CB_시간청산C_D)}, E: {체크박스(GenieConfig.CB_시간청산C_E)}, F: {체크박스(GenieConfig.CB_시간청산C_F)}, G: {체크박스(GenieConfig.CB_시간청산C_G)}, H: {체크박스(GenieConfig.CB_시간청산C_H)}, I: {체크박스(GenieConfig.CB_시간청산C_I)}, J: {체크박스(GenieConfig.CB_시간청산C_J)}, K: {체크박스(GenieConfig.CB_시간청산C_K)}, L: {체크박스(GenieConfig.CB_시간청산C_L)});");

                GenieConfig.매매그룹설정 = sb.ToString();
            }

            // ---------------------------------------------------------
            // 대금 탐색 설정 스크립트 생성
            // ---------------------------------------------------------
            void 대금탐색설정()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.AppendLine($"대금탐색_누적거래대금(거래대금: {GenieConfig.TB_accumulate_Price});\n");

                sb.AppendLine($"대금탐색_매수_A(잔량: {GenieConfig.CBB_M_잔량}, 매도호가별대금: {GenieConfig.TB_M_매도호가별대금}, 매도호가합대금: {GenieConfig.TB_M_매도호가합대금}, 매수호가별대금: {GenieConfig.TB_M_매수호가별대금}, 매수호가합대금: {GenieConfig.TB_M_매수호가합대금}, 매수탐색사용: {체크박스(GenieConfig.CB_매수탐색A)}, 기준초: {GenieConfig.TB_Buy_A_기준초}, Combo_기준초회: {GenieConfig.Combo_Buy_A_초회}, 탐색등락률: {GenieConfig.TB_Buy_A_탐색rate}, 상승카운터: {GenieConfig.TB_Buy_상승카운터_A}, 상승옵션: {체크박스(GenieConfig.CB_Buy_상승옵션_A)}, 하락카운터: {GenieConfig.TB_Buy_하락카운터_A}, 하락옵션: {체크박스(GenieConfig.CB_Buy_하락옵션_A)}, 탐색주가_1: {GenieConfig.TB_Buy_A_탐색주가_1}, 탐색주가_2: {GenieConfig.TB_Buy_A_탐색주가_2}, 탐색주가_3: {GenieConfig.TB_Buy_A_탐색주가_3}, 탐색주가_4: {GenieConfig.TB_Buy_A_탐색주가_4}, 탐색주가_5: {GenieConfig.TB_Buy_A_탐색주가_5}, 탐색주가_6: {GenieConfig.TB_Buy_A_탐색주가_6}, 탐색대금_1: {GenieConfig.TB_Buy_A_탐색대금_1}, 탐색대금_2: {GenieConfig.TB_Buy_A_탐색대금_2}, 탐색대금_3: {GenieConfig.TB_Buy_A_탐색대금_3}, 탐색대금_4: {GenieConfig.TB_Buy_A_탐색대금_4}, 탐색대금_5: {GenieConfig.TB_Buy_A_탐색대금_5}, 탐색대금_6: {GenieConfig.TB_Buy_A_탐색대금_6}, 반복: {GenieConfig.MTB_M_반복}, 분봉: {GenieConfig.CBB_Buy_A_분봉}, 일봉: {GenieConfig.CBB_Buy_A_일봉});");
                sb.AppendLine($"대금탐색_매수_B(잔량: {GenieConfig.CBB_M_잔량_2}, 매도호가별대금: {GenieConfig.TB_M_매도호가별대금_2}, 매도호가합대금: {GenieConfig.TB_M_매도호가합대금_2}, 매수호가별대금: {GenieConfig.TB_M_매수호가별대금_2}, 매수호가합대금: {GenieConfig.TB_M_매수호가합대금_2}, 매수탐색사용: {체크박스(GenieConfig.CB_매수탐색B)}, 기준초: {GenieConfig.TB_Buy_B_기준초}, Combo_기준초회: {GenieConfig.Combo_Buy_B_초회}, 탐색등락률: {GenieConfig.TB_Buy_B_탐색rate}, 상승카운터: {GenieConfig.TB_Buy_상승카운터_B}, 상승옵션: {체크박스(GenieConfig.CB_Buy_상승옵션_B)}, 하락카운터: {GenieConfig.TB_Buy_하락카운터_B}, 하락옵션: {체크박스(GenieConfig.CB_Buy_하락옵션_B)}, 탐색주가_1: {GenieConfig.TB_Buy_B_탐색주가_1}, 탐색주가_2: {GenieConfig.TB_Buy_B_탐색주가_2}, 탐색주가_3: {GenieConfig.TB_Buy_B_탐색주가_3}, 탐색주가_4: {GenieConfig.TB_Buy_B_탐색주가_4}, 탐색주가_5: {GenieConfig.TB_Buy_B_탐색주가_5}, 탐색주가_6: {GenieConfig.TB_Buy_B_탐색주가_6}, 탐색대금_1: {GenieConfig.TB_Buy_B_탐색대금_1}, 탐색대금_2: {GenieConfig.TB_Buy_B_탐색대금_2}, 탐색대금_3: {GenieConfig.TB_Buy_B_탐색대금_3}, 탐색대금_4: {GenieConfig.TB_Buy_B_탐색대금_4}, 탐색대금_5: {GenieConfig.TB_Buy_B_탐색대금_5}, 탐색대금_6: {GenieConfig.TB_Buy_B_탐색대금_6}, 반복: {GenieConfig.MTB_M_반복_2}, 분봉: {GenieConfig.CBB_Buy_B_분봉}, 일봉: {GenieConfig.CBB_Buy_B_일봉});\n");
                sb.AppendLine($"대금탐색_매도(매도탐색사용: {체크박스(GenieConfig.CB_매도탐색)}, 기준초: {GenieConfig.TB_Sell_기준초}, Combo_기준초회: {GenieConfig.combo_Sell_초회}, 탐색등락률: {GenieConfig.TB_Sell_탐색rate}, 상승카운터: {GenieConfig.TB_Sell_상승카운터}, 상승옵션: {체크박스(GenieConfig.CB_Sell_상승옵션)}, 하락카운터: {GenieConfig.TB_Sell_하락카운터}, 하락옵션: {체크박스(GenieConfig.CB_Sell_하락옵션)}, 탐색주가_1: {GenieConfig.TB_Sell_탐색주가_1}, 탐색주가_2: {GenieConfig.TB_Sell_탐색주가_2}, 탐색주가_3: {GenieConfig.TB_Sell_탐색주가_3}, 탐색주가_4: {GenieConfig.TB_Sell_탐색주가_4}, 탐색주가_5: {GenieConfig.TB_Sell_탐색주가_5}, 탐색주가_6: {GenieConfig.TB_Sell_탐색주가_6}, 탐색대금_1: {GenieConfig.TB_Sell_탐색대금_1}, 탐색대금_2: {GenieConfig.TB_Sell_탐색대금_2}, 탐색대금_3: {GenieConfig.TB_Sell_탐색대금_3}, 탐색대금_4: {GenieConfig.TB_Sell_탐색대금_4}, 탐색대금_5: {GenieConfig.TB_Sell_탐색대금_5}, 탐색대금_6: {GenieConfig.TB_Sell_탐색대금_6}, 분봉: {GenieConfig.CBB_Sell_탐색_분봉}, 일봉: {GenieConfig.CBB_Sell_탐색_일봉});");

                GenieConfig.대금탐색설정 = sb.ToString();
            }

            // ---------------------------------------------------------
            // 기능 설정 스크립트 생성
            // ---------------------------------------------------------
            void 기능설정()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.AppendLine($"기능설정값(편입추가: {체크박스(GenieConfig.CB_편입추가)}, 최종가업데이트: {체크박스(GenieConfig.CB_최종가업데이트)}, 신규매수정지: {체크박스(GenieConfig.CB_신규매수정지)}, 추가매수정지: {체크박스(GenieConfig.CB_추가매수정지)}, VI매수취소: {체크박스(GenieConfig.CB_VI매수취소)}, VI매도취소: {체크박스(GenieConfig.CB_VI매도취소)}, 상매수취소: {체크박스(GenieConfig.CB_상매수취소)}, 하매도취소: {체크박스(GenieConfig.CB_하매도취소)}, 상전량청산: {체크박스(GenieConfig.CB_상전량청산)}, 하전량청산: {체크박스(GenieConfig.CB_하전량청산)}, NXT: {체크박스(GenieConfig.CB_NXT)}, ETF매입비제외: {체크박스(GenieConfig.CB_ETF매입비제외)}, CB_중간가주문: {체크박스(GenieConfig.CB_중간가주문)});");

                GenieConfig.기능설정 = sb.ToString();
            }


            UnifiedDataManager.Instance.Writing.Enqueue(() =>
            {
                if (Form1.로딩완료)
                {
                    string File_Check = Application.StartupPath + @"\Data\가이드매매설정.txt";

                    using (StreamWriter writer_ = new StreamWriter(File_Check))
                    {
                        // [수정] .Settings.Default -> Setting.general
                        writer_.WriteLine(GenieConfig.계좌설정);
                        writer_.WriteLine();
                        writer_.WriteLine(GenieConfig.기본매매설정);
                        writer_.WriteLine();
                        writer_.WriteLine(GenieConfig.반복매매설정);
                        writer_.WriteLine();
                        writer_.WriteLine(GenieConfig.계좌관리설정);
                        writer_.WriteLine();
                        writer_.WriteLine(GenieConfig.특수설정);
                        writer_.WriteLine();
                        writer_.WriteLine(GenieConfig.매매그룹설정);
                        writer_.WriteLine();
                        writer_.WriteLine(GenieConfig.대금탐색설정);
                        writer_.WriteLine();
                        writer_.WriteLine(GenieConfig.기능설정);
                    }
                }
            });
        }

        public static void Load_condition_textprint()
        {
            string GetString(int index)
            {
                string result = " ( X ) ";
                if (index == 1) result = " 진입 ";
                if (index == 2) result = " 이탈 ";
                return result;
            }

            string Get_NewAA(int index)
            {
                string result = " I.기준 ";
                if (index == 1) result = " D.기준 ";
                return result;
            }

            string Get_NewBC(int index)
            {
                string result = " I.OR ";
                if (index == 1) result = " I.AND ";
                if (index == 2) result = " D.OR ";
                if (index == 3) result = " D.AND ";
                return result;
            }

            FileInfo File_ = new FileInfo(Path.Combine(Form1.startupPath, "Data", $"{GenieConfig.textBox_ID}__{GenieConfig.textBox_계좌번호}__", "검색식", "가이드검색식설정.txt"));
            if (File_.Exists)
            {
                //파일열기
                StreamReader SR = new StreamReader(Path.Combine(Form1.startupPath, "Data", $"{GenieConfig.textBox_ID}__{GenieConfig.textBox_계좌번호}__", "검색식", "가이드검색식설정.txt"));
                bool.TryParse(SR.ReadLine(), out bool first);
                SR.Close(); // 파일닫기

                if (!first)
                {
                    파일열기();
                }
            }
            else
            {
                파일열기();
            }

            void 파일열기()
            {
                string File_Check = Path.Combine(Form1.startupPath, "Data", $"{GenieConfig.textBox_ID}__{GenieConfig.textBox_계좌번호}__", "검색식", "가이드검색식설정.txt");
                using (StreamWriter writer_ = new StreamWriter(File_Check))
                {
                    writer_.WriteLine("True");
                    writer_.WriteLine("가이드매매 검색식");
                    writer_.WriteLine();
                    writer_.WriteLine("*주의> 키움HTS를 통해 검색식을 만들고 사용하기 바랍니다.*");
                    writer_.WriteLine();
                    writer_.WriteLine("신규 A :: " + 신규_A[0] + "      사용 :: " + Get_NewAA(int.Parse(신규_A[1])) + "   검색식  :: " + 신규_A[2]);
                    writer_.WriteLine("신규 B :: " + 신규_B[0] + "      사용 :: " + Get_NewBC(int.Parse(신규_B[1])) + "     검색식  :: " + 신규_B[2]);
                    writer_.WriteLine("신규 C :: " + 신규_C[0] + "      사용 :: " + Get_NewBC(int.Parse(신규_C[1])) + "     검색식  :: " + 신규_C[2]);
                    writer_.WriteLine();
                    writer_.WriteLine("반복 A :: " + 반복_A[0] + "      사용 :: " + GetString(int.Parse(반복_A[1])) + "     검색식  :: " + 반복_A[2]);
                    writer_.WriteLine("반복 B :: " + 반복_B[0] + "      사용 :: " + GetString(int.Parse(반복_B[1])) + "     검색식  :: " + 반복_B[2]);
                    writer_.WriteLine("반복 C :: " + 반복_C[0] + "      사용 :: " + GetString(int.Parse(반복_C[1])) + "     검색식  :: " + 반복_C[2]);
                    writer_.WriteLine("반복 D :: " + 반복_D[0] + "      사용 :: " + GetString(int.Parse(반복_D[1])) + "     검색식  :: " + 반복_D[2]);
                    writer_.WriteLine("반복 E :: " + 반복_E[0] + "      사용 :: " + GetString(int.Parse(반복_E[1])) + "     검색식  :: " + 반복_E[2]);
                    writer_.WriteLine("반복 F :: " + 반복_F[0] + "      사용 :: " + GetString(int.Parse(반복_F[1])) + "     검색식  :: " + 반복_F[2]);
                    writer_.WriteLine("반복 G :: " + 반복_G[0] + "      사용 :: " + GetString(int.Parse(반복_G[1])) + "     검색식  :: " + 반복_G[2]);
                    writer_.WriteLine("반복 H :: " + 반복_H[0] + "      사용 :: " + GetString(int.Parse(반복_H[1])) + "     검색식  :: " + 반복_H[2]);
                    writer_.WriteLine("반복 I  :: " + 반복_I[0] + "      사용 :: " + GetString(int.Parse(반복_I[1])) + "     검색식  :: " + 반복_I[2]);
                    writer_.WriteLine("반복 J  :: " + 반복_J[0] + "      사용 :: " + GetString(int.Parse(반복_J[1])) + "     검색식  :: " + 반복_J[2]);
                    writer_.WriteLine("반복 K :: " + 반복_K[0] + "      사용 :: " + GetString(int.Parse(반복_K[1])) + "     검색식  :: " + 반복_K[2]);
                    writer_.WriteLine("반복 L :: " + 반복_L[0] + "      사용 :: " + GetString(int.Parse(반복_L[1])) + "     검색식  :: " + 반복_L[2]);
                    writer_.WriteLine("반복 M :: " + 반복_M[0] + "     사용 :: " + GetString(int.Parse(반복_M[1])) + "     검색식  :: " + 반복_M[2]);
                    writer_.WriteLine("반복 N :: " + 반복_N[0] + "      사용 :: " + GetString(int.Parse(반복_N[1])) + "     검색식  :: " + 반복_N[2]);
                    writer_.WriteLine();
                    writer_.WriteLine("리밸 A :: " + 리밸_A[0] + "      사용 :: " + GetString(int.Parse(리밸_A[1])) + "     검색식  :: " + 리밸_A[2]);
                    writer_.WriteLine("리밸 B :: " + 리밸_B[0] + "      사용 :: " + GetString(int.Parse(리밸_B[1])) + "     검색식  :: " + 리밸_B[2]);
                    writer_.WriteLine("리밸 C :: " + 리밸_C[0] + "      사용 :: " + GetString(int.Parse(리밸_C[1])) + "     검색식  :: " + 리밸_C[2]);
                    writer_.WriteLine("리밸 D :: " + 리밸_D[0] + "      사용 :: " + GetString(int.Parse(리밸_D[1])) + "     검색식  :: " + 리밸_D[2]);
                    writer_.WriteLine("리밸 E :: " + 리밸_E[0] + "      사용 :: " + GetString(int.Parse(리밸_E[1])) + "     검색식  :: " + 리밸_E[2]);
                    writer_.WriteLine("리밸 F :: " + 리밸_F[0] + "      사용 :: " + GetString(int.Parse(리밸_F[1])) + "     검색식  :: " + 리밸_F[2]);
                    writer_.WriteLine("리밸 G :: " + 리밸_G[0] + "      사용 :: " + GetString(int.Parse(리밸_G[1])) + "     검색식  :: " + 리밸_G[2]);
                    writer_.WriteLine();
                    writer_.WriteLine("청산 A :: " + 청산_A[0] + "      사용 :: " + GetString(int.Parse(청산_A[1])) + "     검색식  :: " + 청산_A[2]);
                    writer_.WriteLine("청산 B :: " + 청산_B[0] + "      사용 :: " + GetString(int.Parse(청산_B[1])) + "     검색식  :: " + 청산_B[2]);
                    writer_.WriteLine("청산 C :: " + 청산_C[0] + "      사용 :: " + GetString(int.Parse(청산_C[1])) + "     검색식  :: " + 청산_C[2]);
                    writer_.WriteLine();
                    writer_.WriteLine("*주의> 키움HTS를 통해 검색식을 만들고 사용하기 바랍니다.*");
                }

                System.Diagnostics.Process.Start("Notepad.exe", Path.Combine(Form1.startupPath, "Data", $"{GenieConfig.textBox_ID}__{GenieConfig.textBox_계좌번호}__", "검색식", "가이드검색식설정.txt"));
            }
        }

    }
}
