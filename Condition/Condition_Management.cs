using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot.Types;

namespace 지니64
{
    public class Condition_Management : Form1
    {
        public static void 검색식_초기화()
        {
            // [최적화] ConcurrentDictionary는 Count보다 IsEmpty가 훨씬 빠름 (락을 안 걸기 때문)
            if (!Form1.위치별검색식리스트.IsEmpty) return;

            // [내부 헬퍼] 반복되는 등록 로직을 함수 하나로 통합
            void 초기화_등록(string 헤더, char 시작, char 끝)
            {
                for (char c = 시작; c <= 끝; c++)
                {
                    string key = $"{헤더}_{c}";

                    // 💡 괄호() 대신 중괄호{}를 써서, 어떤 변수에 어떤 값이 들어가는지 이름표를 붙여줍니다!
                    Form1.위치별검색식리스트.TryAdd(key, new 위치별검색식
                    {
                        위치 = key,
                        이름 = "",
                        중복여부 = false,
                        실행여부 = false
                    });
                }
            }

            // 1. 신규 (A ~ C)
            초기화_등록("신규", 'A', 'C');

            // 2. 반복 (A ~ N)
            초기화_등록("반복", 'A', 'N');

            // 3. 리밸 (A ~ G)
            초기화_등록("리밸", 'A', 'G');

            // 4. 청산 (A ~ C)
            초기화_등록("청산", 'A', 'C');

            // 5. 와치 (A ~ D)
            초기화_등록("와치", 'A', 'D');
        }


        public static void Condition_Add(object sender)
        {
            ComboBox combobox = sender as ComboBox;

            combobox.Items.Clear();

            combobox.Items.Add("");

            for (int i = 0; i < Form1.ConditionList.Count; i++)
            {
                combobox.Items.Add(Form1.ConditionList[i].name);
            }
        }

        public static void Condition_DataLoad() // 계좌 번호에 따른 조건식 불러오기
        {
            // 1. 관심 검색식 설정
            if (Form1.form1.CBB_관심검색식.Items.Contains(GenieConfig.CBB_관심검색식))
            {
                Form1.form1.CBB_관심검색식.SelectedItem = GenieConfig.CBB_관심검색식;
            }
            else
            {
                Form1.form1.CBB_관심검색식.SelectedIndex = -1;
            }

            // 2. JSON 파일 로드 (J사용검색식.json)
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
                            // 2. [핵심 수정] 일반 Dictionary를 ConcurrentDictionary로 변환해서 할당!
                            Form1.위치별검색식리스트 = new System.Collections.Concurrent.ConcurrentDictionary<string, 위치별검색식>(temp_data);

                            foreach (var Values in Form1.위치별검색식리스트.Values)
                            {
                                Values.이름 ??= ""; // null 방지 (JSON에서 누락된 경우)
                            }

                            Form1.Console_print("주식 자동매매프로그램 지니: 검색식 데이터 로드 성공!");
                        }
                    }
                    catch (Exception ex)
                    {
                        Form1.Console_print($"[에러] 검색식 로드 실패: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    // 여기서 에러 메시지가 출력되면 파일 내용이 깨졌거나 형식이 바뀐 것입니다.
                    Form1.Console_print($"검색식 로드 실패: {ex.Message}");
                }
            }


            // 3. 조건식 자동 실행 및 검증 (Setting 변수 사용)

            // [신규 매수]
            if (GenieConfig.CB_new_A && !condition_Run("신규_A")) { Form1.위치별검색식리스트["신규_A"].이름 = ""; GenieConfig.CB_new_A = false; }
            if (GenieConfig.CB_new_B && !condition_Run("신규_B")) { Form1.위치별검색식리스트["신규_B"].이름 = ""; GenieConfig.CB_new_B = false; }
            if (GenieConfig.CB_new_C && !condition_Run("신규_C")) { Form1.위치별검색식리스트["신규_C"].이름 = ""; GenieConfig.CB_new_C = false; }

            // [반복 매매 (A~N)]
            if (GenieConfig.combo_repeat_use_condition_A > 0 && !condition_Run("반복_A")) { Reset_Repeat("반복_A"); GenieConfig.combo_repeat_use_condition_A = 0; GenieConfig.CB_repeat_use_A = false; }
            if (GenieConfig.combo_repeat_use_condition_B > 0 && !condition_Run("반복_B")) { Reset_Repeat("반복_B"); GenieConfig.combo_repeat_use_condition_B = 0; GenieConfig.CB_repeat_use_B = false; }
            if (GenieConfig.combo_repeat_use_condition_C > 0 && !condition_Run("반복_C")) { Reset_Repeat("반복_C"); GenieConfig.combo_repeat_use_condition_C = 0; GenieConfig.CB_repeat_use_C = false; }
            if (GenieConfig.combo_repeat_use_condition_D > 0 && !condition_Run("반복_D")) { Reset_Repeat("반복_D"); GenieConfig.combo_repeat_use_condition_D = 0; GenieConfig.CB_repeat_use_D = false; }
            if (GenieConfig.combo_repeat_use_condition_E > 0 && !condition_Run("반복_E")) { Reset_Repeat("반복_E"); GenieConfig.combo_repeat_use_condition_E = 0; GenieConfig.CB_repeat_use_E = false; }
            if (GenieConfig.combo_repeat_use_condition_F > 0 && !condition_Run("반복_F")) { Reset_Repeat("반복_F"); GenieConfig.combo_repeat_use_condition_F = 0; GenieConfig.CB_repeat_use_F = false; }
            if (GenieConfig.combo_repeat_use_condition_G > 0 && !condition_Run("반복_G")) { Reset_Repeat("반복_G"); GenieConfig.combo_repeat_use_condition_G = 0; GenieConfig.CB_repeat_use_G = false; }
            if (GenieConfig.combo_repeat_use_condition_H > 0 && !condition_Run("반복_H")) { Reset_Repeat("반복_H"); GenieConfig.combo_repeat_use_condition_H = 0; GenieConfig.CB_repeat_use_H = false; }
            if (GenieConfig.combo_repeat_use_condition_I > 0 && !condition_Run("반복_I")) { Reset_Repeat("반복_I"); GenieConfig.combo_repeat_use_condition_I = 0; GenieConfig.CB_repeat_use_I = false; }
            if (GenieConfig.combo_repeat_use_condition_J > 0 && !condition_Run("반복_J")) { Reset_Repeat("반복_J"); GenieConfig.combo_repeat_use_condition_J = 0; GenieConfig.CB_repeat_use_J = false; }
            if (GenieConfig.combo_repeat_use_condition_K > 0 && !condition_Run("반복_K")) { Reset_Repeat("반복_K"); GenieConfig.combo_repeat_use_condition_K = 0; GenieConfig.CB_repeat_use_K = false; }
            if (GenieConfig.combo_repeat_use_condition_L > 0 && !condition_Run("반복_L")) { Reset_Repeat("반복_L"); GenieConfig.combo_repeat_use_condition_L = 0; GenieConfig.CB_repeat_use_L = false; }
            if (GenieConfig.combo_repeat_use_condition_M > 0 && !condition_Run("반복_M")) { Reset_Repeat("반복_M"); GenieConfig.combo_repeat_use_condition_M = 0; GenieConfig.CB_repeat_use_M = false; }
            if (GenieConfig.combo_repeat_use_condition_N > 0 && !condition_Run("반복_N")) { Reset_Repeat("반복_N"); GenieConfig.combo_repeat_use_condition_N = 0; GenieConfig.CB_repeat_use_N = false; }

            // [리밸런싱 (A~G)]
            if (GenieConfig.combo_rebalance_use_condition_A > 0 && !condition_Run("리밸_A")) { Reset_Repeat("리밸_A"); GenieConfig.combo_rebalance_use_condition_A = 0; GenieConfig.CB_rebalance_A = false; }
            if (GenieConfig.combo_rebalance_use_condition_B > 0 && !condition_Run("리밸_B")) { Reset_Repeat("리밸_B"); GenieConfig.combo_rebalance_use_condition_B = 0; GenieConfig.CB_rebalance_B = false; }
            if (GenieConfig.combo_rebalance_use_condition_C > 0 && !condition_Run("리밸_C")) { Reset_Repeat("리밸_C"); GenieConfig.combo_rebalance_use_condition_C = 0; GenieConfig.CB_rebalance_C = false; }
            if (GenieConfig.combo_rebalance_use_condition_D > 0 && !condition_Run("리밸_D")) { Reset_Repeat("리밸_D"); GenieConfig.combo_rebalance_use_condition_D = 0; GenieConfig.CB_rebalance_D = false; }
            if (GenieConfig.combo_rebalance_use_condition_E > 0 && !condition_Run("리밸_E")) { Reset_Repeat("리밸_E"); GenieConfig.combo_rebalance_use_condition_E = 0; GenieConfig.CB_rebalance_E = false; }
            if (GenieConfig.combo_rebalance_use_condition_F > 0 && !condition_Run("리밸_F")) { Reset_Repeat("리밸_F"); GenieConfig.combo_rebalance_use_condition_F = 0; GenieConfig.CB_rebalance_F = false; }
            if (GenieConfig.combo_rebalance_use_condition_G > 0 && !condition_Run("리밸_G")) { Reset_Repeat("리밸_G"); GenieConfig.combo_rebalance_use_condition_G = 0; GenieConfig.CB_rebalance_G = false; }

            // [계좌 청산 (A~C)]
            if (GenieConfig.CBB_Liquidation_use_condition_A > 0 && !condition_Run("청산_A")) { Reset_Repeat("청산_A"); GenieConfig.CBB_Liquidation_use_condition_A = 0; GenieConfig.CB_Liquidation_A = false; }
            if (GenieConfig.CBB_Liquidation_use_condition_B > 0 && !condition_Run("청산_B")) { Reset_Repeat("청산_B"); GenieConfig.CBB_Liquidation_use_condition_B = 0; GenieConfig.CB_Liquidation_B = false; }
            if (GenieConfig.CBB_Liquidation_use_condition_C > 0 && !condition_Run("청산_C")) { Reset_Repeat("청산_C"); GenieConfig.CBB_Liquidation_use_condition_C = 0; GenieConfig.CB_Liquidation_C = false; }

            //// 4. 감시(Watch) 설정 UI 반영
            try { Form1.form1.와치_A.Items.Add(Form1.위치별검색식리스트["와치_A"].이름); Form1.form1.와치_A.Text = Form1.위치별검색식리스트["와치_A"].이름; } catch { }
            try { Form1.form1.와치_B.Items.Add(Form1.위치별검색식리스트["와치_B"].이름); Form1.form1.와치_B.Text = Form1.위치별검색식리스트["와치_B"].이름; } catch { }
            try { Form1.form1.와치_C.Items.Add(Form1.위치별검색식리스트["와치_C"].이름); Form1.form1.와치_C.Text = Form1.위치별검색식리스트["와치_C"].이름; } catch { }
            try { Form1.form1.와치_D.Items.Add(Form1.위치별검색식리스트["와치_D"].이름); Form1.form1.와치_D.Text = Form1.위치별검색식리스트["와치_D"].이름; } catch { }

            if (GenieConfig.CB_watch_use_A && !condition_Run("와치_A")) { Reset_Repeat("와치_A"); GenieConfig.CB_watch_use_A = false; }
            if (GenieConfig.CB_watch_use_B && !condition_Run("와치_B")) { Reset_Repeat("와치_B"); GenieConfig.CB_watch_use_B = false; }
            if (GenieConfig.CB_watch_use_C && !condition_Run("와치_C")) { Reset_Repeat("와치_C"); GenieConfig.CB_watch_use_C = false; }
            if (GenieConfig.CB_watch_use_D && !condition_Run("와치_D")) { Reset_Repeat("와치_D"); GenieConfig.CB_watch_use_D = false; }


            // 내부 헬퍼 함수: 이름 초기화용 (코드 중복 줄이기)
            void Reset_Repeat(string key)
            {
                if (Form1.위치별검색식리스트.ContainsKey(key)) Form1.위치별검색식리스트[key].이름 = "";
            }

            // 조건식 실행 함수
            bool condition_Run(string where)
            {
                Form1.Console_print($"Condition_DataLoad where : {where}");
                bool result = false;
                if (Form1.위치별검색식리스트.ContainsKey(where))
                {
                    Condition start_condition = Form1.ConditionList.Find(o => o.name.Equals(Form1.위치별검색식리스트[where].이름));
                    if (start_condition != null)
                    {
                        result = Start_Monitoring(start_condition, where, null, null);
                    }
                }
                return result;
            }

            // 5. 매매 시작 상태가 아닐 때 UI 업데이트
            if (Form1.매매시작 != "매매시작")
            {
                Form1.form1.CB_watch_use_A.Checked = GenieConfig.CB_watch_use_A;
                Form1.form1.CB_watch_use_B.Checked = GenieConfig.CB_watch_use_B;
                Form1.form1.CB_watch_use_C.Checked = GenieConfig.CB_watch_use_C;
                Form1.form1.CB_watch_use_D.Checked = GenieConfig.CB_watch_use_D;

                Tab_InterestGroup.관심실시간자동등록(false);
                Tab_InterestGroup.자동삭제실행();

                Log.동작기록("사용자 검색식 불러오기 완료.");
            }
        }

        // ---------------------------------------------------------
        // 체크박스 변경 시 조건식 감시 시작/정지 (수정됨)
        // ---------------------------------------------------------

        public static void CB_condition_CheckedChanged(object sender) // 체크박스 와 콤보박스 사용 갯수 제한 
        {
            if (ConditionList.Count == 0) return;

            CheckBox checkbox = (sender as CheckBox);
            ComboBox combo = null;

            // [수정] check 변수는 현재 로직에서 직접 쓰이진 않지만, 값 매핑을 최신 구조로 변경합니다.
            bool check = false;

            switch (checkbox.Name)
            {
                // [신규 매수] -> Setting.basic
                case "CB_new_A": combo = Form_Basic.form.신규_A; check = GenieConfig.CB_new_A; break;
                case "CB_new_B": combo = Form_Basic.form.신규_B; check = GenieConfig.CB_new_B; break;
                case "CB_new_C": combo = Form_Basic.form.신규_C; check = GenieConfig.CB_new_C; break;

                // [감시(Watch)] -> Setting.watch
                case "CB_watch_use_A": combo = Form1.form1.와치_A; check = GenieConfig.CB_watch_use_A; break;
                case "CB_watch_use_B": combo = Form1.form1.와치_B; check = GenieConfig.CB_watch_use_B; break;
                case "CB_watch_use_C": combo = Form1.form1.와치_C; check = GenieConfig.CB_watch_use_C; break;
                case "CB_watch_use_D": combo = Form1.form1.와치_D; check = GenieConfig.CB_watch_use_D; break;
            }

            if (checkbox.Checked)
            {
                Console_print($"checkbox.Name [{checkbox.Name}]   combo.Text [{combo.Text}] ConditionList.count [{ConditionList.Count}]");

                // [조건식 시작 로직]
                Condition start_condition = Form1.ConditionList.Find(o => o.name.Equals(combo.Text));
                if (start_condition != null)
                {
                    if (combo.Text.Equals(""))
                    {
                        Form1.AutoClosingAlram(combo.Name.ToString() + " - 검색식이 비어 있습니다.", "검색식알림", 5, "동작");
                        checkbox.Checked = false;
                    }
                    else
                    {
                        // Start_Monitoring 호출
                        if (!Start_Monitoring(start_condition, combo.Name, checkbox, null))
                        {
                            checkbox.Checked = false;
                        }
                    }
                }
                else
                {
                    Form1.AutoClosingAlram(combo.Name.ToString() + " - 검색식이 비어 있습니다.", "검색식알림", 5, "동작");
                    checkbox.Checked = false;
                }
            }
            else
            {
                // [조건식 정지 로직]
                // Form1.위치별검색식리스트 딕셔너리에 키가 있는지 먼저 확인하는 것이 안전합니다.
                if (Form1.위치별검색식리스트.ContainsKey(combo.Name) && Form1.위치별검색식리스트[combo.Name].실행여부)
                {
                    Condition stop_condition = Form1.ConditionList.Find(o => o.name.Equals(Form1.위치별검색식리스트[combo.Name].이름));
                    if (stop_condition != null)
                    {
                        Stop_Monitoring(stop_condition, combo.Name);  // 검색식 정지 요청
                    }
                }
            }
        }
        public static void Combo_use_condition_SelectedIndexChanged(object sender)
        {
            if (Form1.로딩완료)
            {
                CheckBox checkbox = null;
                ComboBox use = sender as ComboBox;
                ComboBox combo = null;

                // [중요] 기존 설정값을 읽거나 저장하기 위해 Setting 클래스를 사용합니다.
                switch (use.Name)
                {
                    // =========================================================
                    // [반복 매매 (Repeat) 그룹]
                    // =========================================================
                    case "combo_repeat_use_condition_A":
                        checkbox = Form_Repeat.form.CB_repeat_use_A; combo = Form_Repeat.form.반복_A;
                        GenieConfig.combo_repeat_use_condition_A = selectedindex(GenieConfig.combo_repeat_use_condition_A);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_A;
                        break;
                    case "combo_repeat_use_condition_B":
                        checkbox = Form_Repeat.form.CB_repeat_use_B; combo = Form_Repeat.form.반복_B;
                        GenieConfig.combo_repeat_use_condition_B = selectedindex(GenieConfig.combo_repeat_use_condition_B);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_B;
                        break;
                    case "combo_repeat_use_condition_C":
                        checkbox = Form_Repeat.form.CB_repeat_use_C; combo = Form_Repeat.form.반복_C;
                        GenieConfig.combo_repeat_use_condition_C = selectedindex(GenieConfig.combo_repeat_use_condition_C);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_C;
                        break;
                    case "combo_repeat_use_condition_D":
                        checkbox = Form_Repeat.form.CB_repeat_use_D; combo = Form_Repeat.form.반복_D;
                        GenieConfig.combo_repeat_use_condition_D = selectedindex(GenieConfig.combo_repeat_use_condition_D);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_D;
                        break;
                    case "combo_repeat_use_condition_E":
                        checkbox = Form_Repeat.form.CB_repeat_use_E; combo = Form_Repeat.form.반복_E;
                        GenieConfig.combo_repeat_use_condition_E = selectedindex(GenieConfig.combo_repeat_use_condition_E);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_E;
                        break;
                    case "combo_repeat_use_condition_F":
                        checkbox = Form_Repeat.form.CB_repeat_use_F; combo = Form_Repeat.form.반복_F;
                        GenieConfig.combo_repeat_use_condition_F = selectedindex(GenieConfig.combo_repeat_use_condition_F);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_F;
                        break;
                    case "combo_repeat_use_condition_G":
                        checkbox = Form_Repeat.form.CB_repeat_use_G; combo = Form_Repeat.form.반복_G;
                        GenieConfig.combo_repeat_use_condition_G = selectedindex(GenieConfig.combo_repeat_use_condition_G);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_G;
                        break;
                    case "combo_repeat_use_condition_H":
                        checkbox = Form_Repeat.form.CB_repeat_use_H; combo = Form_Repeat.form.반복_H;
                        GenieConfig.combo_repeat_use_condition_H = selectedindex(GenieConfig.combo_repeat_use_condition_H);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_H;
                        break;
                    case "combo_repeat_use_condition_I":
                        checkbox = Form_Repeat.form.CB_repeat_use_I; combo = Form_Repeat.form.반복_I;
                        GenieConfig.combo_repeat_use_condition_I = selectedindex(GenieConfig.combo_repeat_use_condition_I);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_I;
                        break;
                    case "combo_repeat_use_condition_J":
                        checkbox = Form_Repeat.form.CB_repeat_use_J; combo = Form_Repeat.form.반복_J;
                        GenieConfig.combo_repeat_use_condition_J = selectedindex(GenieConfig.combo_repeat_use_condition_J);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_J;
                        break;
                    case "combo_repeat_use_condition_K":
                        checkbox = Form_Repeat.form.CB_repeat_use_K; combo = Form_Repeat.form.반복_K;
                        GenieConfig.combo_repeat_use_condition_K = selectedindex(GenieConfig.combo_repeat_use_condition_K);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_K;
                        break;
                    case "combo_repeat_use_condition_L":
                        checkbox = Form_Repeat.form.CB_repeat_use_L; combo = Form_Repeat.form.반복_L;
                        GenieConfig.combo_repeat_use_condition_L = selectedindex(GenieConfig.combo_repeat_use_condition_L);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_L;
                        break;
                    case "combo_repeat_use_condition_M":
                        checkbox = Form_Repeat.form.CB_repeat_use_M; combo = Form_Repeat.form.반복_M;
                        GenieConfig.combo_repeat_use_condition_M = selectedindex(GenieConfig.combo_repeat_use_condition_M);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_M;
                        break;
                    case "combo_repeat_use_condition_N":
                        checkbox = Form_Repeat.form.CB_repeat_use_N; combo = Form_Repeat.form.반복_N;
                        GenieConfig.combo_repeat_use_condition_N = selectedindex(GenieConfig.combo_repeat_use_condition_N);
                        use.SelectedIndex = GenieConfig.combo_repeat_use_condition_N;
                        break;

                    // =========================================================
                    // [리밸런싱 (Rebalance) 그룹]
                    // =========================================================
                    case "combo_rebalance_use_condition_A":
                        checkbox = Form_AccountManagement.form.CB_rebalance_A; combo = Form_AccountManagement.form.리밸_A;
                        GenieConfig.combo_rebalance_use_condition_A = selectedindex(GenieConfig.combo_rebalance_use_condition_A);
                        use.SelectedIndex = GenieConfig.combo_rebalance_use_condition_A;
                        break;
                    case "combo_rebalance_use_condition_B":
                        checkbox = Form_AccountManagement.form.CB_rebalance_B; combo = Form_AccountManagement.form.리밸_B;
                        GenieConfig.combo_rebalance_use_condition_B = selectedindex(GenieConfig.combo_rebalance_use_condition_B);
                        use.SelectedIndex = GenieConfig.combo_rebalance_use_condition_B;
                        break;
                    case "combo_rebalance_use_condition_C":
                        checkbox = Form_AccountManagement.form.CB_rebalance_C; combo = Form_AccountManagement.form.리밸_C;
                        GenieConfig.combo_rebalance_use_condition_C = selectedindex(GenieConfig.combo_rebalance_use_condition_C);
                        use.SelectedIndex = GenieConfig.combo_rebalance_use_condition_C;
                        break;
                    case "combo_rebalance_use_condition_D":
                        checkbox = Form_AccountManagement.form.CB_rebalance_D; combo = Form_AccountManagement.form.리밸_D;
                        GenieConfig.combo_rebalance_use_condition_D = selectedindex(GenieConfig.combo_rebalance_use_condition_D);
                        use.SelectedIndex = GenieConfig.combo_rebalance_use_condition_D;
                        break;
                    case "combo_rebalance_use_condition_E":
                        checkbox = Form_AccountManagement.form.CB_rebalance_E; combo = Form_AccountManagement.form.리밸_E;
                        GenieConfig.combo_rebalance_use_condition_E = selectedindex(GenieConfig.combo_rebalance_use_condition_E);
                        use.SelectedIndex = GenieConfig.combo_rebalance_use_condition_E;
                        break;
                    case "combo_rebalance_use_condition_F":
                        checkbox = Form_AccountManagement.form.CB_rebalance_F; combo = Form_AccountManagement.form.리밸_F;
                        GenieConfig.combo_rebalance_use_condition_F = selectedindex(GenieConfig.combo_rebalance_use_condition_F);
                        use.SelectedIndex = GenieConfig.combo_rebalance_use_condition_F;
                        break;
                    case "combo_rebalance_use_condition_G":
                        checkbox = Form_AccountManagement.form.CB_rebalance_G; combo = Form_AccountManagement.form.리밸_G;
                        GenieConfig.combo_rebalance_use_condition_G = selectedindex(GenieConfig.combo_rebalance_use_condition_G);
                        use.SelectedIndex = GenieConfig.combo_rebalance_use_condition_G;
                        break;

                    // =========================================================
                    // [계좌 청산 (Liquidation) 그룹]
                    // =========================================================
                    case "CBB_Liquidation_use_condition_A":
                        checkbox = Form_AccountManagement.form.CB_Liquidation_A; combo = Form_AccountManagement.form.청산_A;
                        GenieConfig.CBB_Liquidation_use_condition_A = selectedindex(GenieConfig.CBB_Liquidation_use_condition_A);
                        use.SelectedIndex = GenieConfig.CBB_Liquidation_use_condition_A;
                        break;
                    case "CBB_Liquidation_use_condition_B":
                        checkbox = Form_AccountManagement.form.CB_Liquidation_B; combo = Form_AccountManagement.form.청산_B;
                        GenieConfig.CBB_Liquidation_use_condition_B = selectedindex(GenieConfig.CBB_Liquidation_use_condition_B);
                        use.SelectedIndex = GenieConfig.CBB_Liquidation_use_condition_B;
                        break;
                    case "CBB_Liquidation_use_condition_C":
                        checkbox = Form_AccountManagement.form.CB_Liquidation_C; combo = Form_AccountManagement.form.청산_C;
                        GenieConfig.CBB_Liquidation_use_condition_C = selectedindex(GenieConfig.CBB_Liquidation_use_condition_C);
                        use.SelectedIndex = GenieConfig.CBB_Liquidation_use_condition_C;
                        break;
                }

                // [내부 로컬 함수] 인덱스 변경 로직
                int selectedindex(int befor_index)
                {
                    int after_index = use.SelectedIndex;

                    if (befor_index != after_index)
                    {
                        if (use.SelectedIndex > 0)
                        {
                            Condition start_condition = Form1.ConditionList.Find(o => o.name.Equals(combo.Text));
                            if (start_condition != null)
                            {
                                if (combo.Text != Form1.위치별검색식리스트[combo.Name.ToString()].이름)
                                {
                                    if (!Start_Monitoring(start_condition, combo.Name, null, use))
                                    {
                                        after_index = 0;
                                    }
                                }
                            }
                            else
                            {
                                Helper.알림창_멀티("검색식 가동확인", combo.Name + " - 검색식 존재하지 않습니다.", 10, false);
                                after_index = 0;
                            }
                        }
                        else
                        {
                            Condition stop_condition = Form1.ConditionList.Find(o => o.name.Equals(Form1.위치별검색식리스트[combo.Name.ToString()].이름));
                            if (stop_condition != null)
                            {
                                Stop_Monitoring(stop_condition, combo.Name);  // 검색식 정지 요청
                            }
                        }

                        if (checkbox != null && checkbox.Checked) checkbox.Checked = false;

                        if (after_index == 0)
                        {
                            Form1.위치별검색식리스트[combo.Name.ToString()].이름 = "";
                        }

                        return after_index;
                    }

                    return befor_index;
                }
            }
        }


        public static void Combo_condition_SelectedIndexChanged(object sender)
        {
            ComboBox combobox = sender as ComboBox;
            int combo_index = combobox.SelectedIndex;
            string combo_text = combobox.Text;
            string combo_name = combobox.Name;

            switch (combobox.Name)
            {
                case "신규_A":
                    if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                    {
                        GenieConfig.CB_new_A = false;
                        Form1.NewStock_List.RemoveAll(o => o.Pos == "New_A");
                        Form_Basic.form.CB_new_A.Checked = false;
                    }
                    break;
                case "신규_B":
                    if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                    {
                        GenieConfig.CB_new_B = false;
                        Form1.NewStock_List.RemoveAll(o => o.Pos == "New_B");
                        Form_Basic.form.CB_new_B.Checked = false;
                    }
                    break;
                case "신규_C":
                    if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                    {
                        GenieConfig.CB_new_C = false;
                        Form1.NewStock_List.RemoveAll(o => o.Pos == "New_C");
                        Form_Basic.form.CB_new_C.Checked = false;
                    }
                    break;
                case "반복_A":
                    if (Form_Repeat.form.combo_repeat_use_condition_A.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_A.Checked = false;
                            Catch_Stock_List_Clear("반복_A");

                            GenieConfig.combo_repeat_use_condition_A = 0;
                            Form_Repeat.form.combo_repeat_use_condition_A.SelectedIndex = 0;
                        }
                    }
                    break;
                case "반복_B":
                    if (Form_Repeat.form.combo_repeat_use_condition_B.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_B.Checked = false;
                            Catch_Stock_List_Clear("반복_B");

                            GenieConfig.combo_repeat_use_condition_B = 0;
                            Form_Repeat.form.combo_repeat_use_condition_B.SelectedIndex = 0;
                        }
                    }
                    break;
                case "반복_C":
                    if (Form_Repeat.form.combo_repeat_use_condition_C.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_C.Checked = false;
                            GenieConfig.combo_repeat_use_condition_C = 0;
                            Catch_Stock_List_Clear("반복_C");

                            Form_Repeat.form.combo_repeat_use_condition_C.SelectedIndex = 0;
                        }
                    }
                    break;
                case "반복_D":
                    if (Form_Repeat.form.combo_repeat_use_condition_D.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_D.Checked = false;
                            GenieConfig.combo_repeat_use_condition_D = 0;
                            Catch_Stock_List_Clear("반복_D");

                            Form_Repeat.form.combo_repeat_use_condition_D.SelectedIndex = 0;
                        }
                    }
                    break;
                case "반복_E":
                    if (Form_Repeat.form.combo_repeat_use_condition_E.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_E.Checked = false;
                            GenieConfig.combo_repeat_use_condition_E = 0;
                            Catch_Stock_List_Clear("반복_E");

                            Form_Repeat.form.combo_repeat_use_condition_E.SelectedIndex = 0;
                        }
                    }
                    break;
                case "반복_F":
                    if (Form_Repeat.form.combo_repeat_use_condition_F.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_F.Checked = false;
                            GenieConfig.combo_repeat_use_condition_F = 0;
                            Catch_Stock_List_Clear("반복_F");

                            Form_Repeat.form.combo_repeat_use_condition_F.SelectedIndex = 0;
                        }
                    }
                    break;
                case "반복_G":
                    if (Form_Repeat.form.combo_repeat_use_condition_G.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_G.Checked = false;
                            GenieConfig.combo_repeat_use_condition_G = 0;
                            Catch_Stock_List_Clear("반복_G");

                            Form_Repeat.form.combo_repeat_use_condition_G.SelectedIndex = 0;
                        }
                    }
                    break;
                case "반복_H":
                    if (Form_Repeat.form.combo_repeat_use_condition_H.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_H.Checked = false;
                            GenieConfig.combo_repeat_use_condition_H = 0;
                            Catch_Stock_List_Clear("반복_H");

                            Form_Repeat.form.combo_repeat_use_condition_H.SelectedIndex = 0;
                        }
                    }
                    break;
                case "반복_I":
                    if (Form_Repeat.form.combo_repeat_use_condition_I.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_I.Checked = false;
                            GenieConfig.combo_repeat_use_condition_I = 0;
                            Catch_Stock_List_Clear("반복_I");

                            Form_Repeat.form.combo_repeat_use_condition_I.SelectedIndex = 0;
                        }
                    }
                    break;
                case "반복_J":
                    if (Form_Repeat.form.combo_repeat_use_condition_J.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_J.Checked = false;
                            GenieConfig.combo_repeat_use_condition_J = 0;
                            Catch_Stock_List_Clear("반복_J");

                            Form_Repeat.form.combo_repeat_use_condition_J.SelectedIndex = 0;
                        }
                    }
                    break;
                case "반복_K":
                    if (Form_Repeat.form.combo_repeat_use_condition_K.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_K.Checked = false;
                            GenieConfig.combo_repeat_use_condition_K = 0;
                            Catch_Stock_List_Clear("반복_K");

                            Form_Repeat.form.combo_repeat_use_condition_K.SelectedIndex = 0;
                        }
                    }
                    break;
                case "반복_L":
                    if (Form_Repeat.form.combo_repeat_use_condition_L.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_L.Checked = false;
                            GenieConfig.combo_repeat_use_condition_L = 0;
                            Catch_Stock_List_Clear("반복_L");

                            Form_Repeat.form.combo_repeat_use_condition_L.SelectedIndex = 0;
                        }
                    }
                    break;
                case "반복_M":
                    if (Form_Repeat.form.combo_repeat_use_condition_M.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_M.Checked = false;
                            GenieConfig.combo_repeat_use_condition_M = 0;
                            Catch_Stock_List_Clear("반복_M");

                            Form_Repeat.form.combo_repeat_use_condition_M.SelectedIndex = 0;
                        }
                    }
                    break;
                case "반복_N":
                    if (Form_Repeat.form.combo_repeat_use_condition_N.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_N.Checked = false;
                            GenieConfig.combo_repeat_use_condition_N = 0;
                            Catch_Stock_List_Clear("반복_N");

                            Form_Repeat.form.combo_repeat_use_condition_N.SelectedIndex = 0;
                        }
                    }
                    break;
                case "리밸_A":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_A.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_A.Checked = false;
                            Catch_Stock_List_Clear("리밸_A");

                            GenieConfig.combo_rebalance_use_condition_A = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_A.SelectedIndex = 0;
                        }
                    }
                    break;
                case "리밸_B":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_B.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_B.Checked = false;
                            Catch_Stock_List_Clear("리밸_B");

                            GenieConfig.combo_rebalance_use_condition_B = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_B.SelectedIndex = 0;
                        }
                    }
                    break;
                case "리밸_C":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_C.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_C.Checked = false;
                            Catch_Stock_List_Clear("리밸_C");

                            GenieConfig.combo_rebalance_use_condition_C = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_C.SelectedIndex = 0;
                        }
                    }
                    break;
                case "리밸_D":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_D.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_D.Checked = false;
                            Catch_Stock_List_Clear("리밸_D");

                            GenieConfig.combo_rebalance_use_condition_D = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_D.SelectedIndex = 0;
                        }
                    }
                    break;
                case "리밸_E":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_E.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_E.Checked = false;
                            Catch_Stock_List_Clear("리밸_E");

                            GenieConfig.combo_rebalance_use_condition_E = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_E.SelectedIndex = 0;
                        }
                    }
                    break;
                case "리밸_F":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_F.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_F.Checked = false;
                            Catch_Stock_List_Clear("리밸_F");

                            GenieConfig.combo_rebalance_use_condition_F = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_F.SelectedIndex = 0;
                        }
                    }
                    break;
                case "리밸_G":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_G.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_G.Checked = false;
                            Catch_Stock_List_Clear("리밸_G");

                            GenieConfig.combo_rebalance_use_condition_G = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_G.SelectedIndex = 0;
                        }
                    }
                    break;

                case "청산_A":
                    if (Form_AccountManagement.form.CBB_Liquidation_use_condition_A.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_AccountManagement.form.CB_Liquidation_A.Checked = false;
                            Catch_Stock_List_Clear("청산_A");

                            GenieConfig.CBB_Liquidation_use_condition_A = 0;
                            Form_AccountManagement.form.CBB_Liquidation_use_condition_A.SelectedIndex = 0;
                        }
                    }
                    break;
                case "청산_B":
                    if (Form_AccountManagement.form.CBB_Liquidation_use_condition_B.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_AccountManagement.form.CB_Liquidation_B.Checked = false;
                            Catch_Stock_List_Clear("청산_B");

                            GenieConfig.CBB_Liquidation_use_condition_B = 0;
                            Form_AccountManagement.form.CBB_Liquidation_use_condition_B.SelectedIndex = 0;
                        }
                    }
                    break;
                case "청산_C":
                    if (Form_AccountManagement.form.CBB_Liquidation_use_condition_C.SelectedIndex != 0)
                    {
                        if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                        {
                            Form_AccountManagement.form.CB_Liquidation_C.Checked = false;
                            Catch_Stock_List_Clear("청산_C");

                            GenieConfig.CBB_Liquidation_use_condition_C = 0;
                            Form_AccountManagement.form.CBB_Liquidation_use_condition_C.SelectedIndex = 0;
                        }
                    }
                    break;

                case "와치_A":
                    if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                    {
                        Form1.form1.CB_watch_use_A.Checked = false;
                    }
                    break;
                case "와치_B":
                    if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                    {
                        Form1.form1.CB_watch_use_B.Checked = false;
                    }
                    break;
                case "와치_C":
                    if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                    {
                        Form1.form1.CB_watch_use_C.Checked = false;
                    }
                    break;
                case "와치_D":
                    if (!Form1.위치별검색식리스트[combo_name].이름.Equals(combo_text) || (combo_index == 0 && combo_index.Equals("")))
                    {
                        Form1.form1.CB_watch_use_D.Checked = false;
                    }
                    break;
            }

        }

        public static bool Start_Monitoring(Condition condition, string where, CheckBox checkbox, ComboBox combobox)
        {
            Form1.Console_print("######### Start_Monitoring 위치: " + where + " 검색식: " + condition.name);

            if (condition.index == "index")
            {
                switch (condition.name)
                {
                    case "매수탐색_A":
                        if (!GenieConfig.CB_매수탐색A)
                            return false;
                        break;
                    case "매수탐색_B":
                        if (!GenieConfig.CB_매수탐색B)
                            return false;
                        break;
                    case "매도탐색":
                        if (!GenieConfig.CB_매도탐색)
                            return false;
                        break;
                }
            }

            if (condition.index != "index")
            {
                List<RunCondition> list = Form1.Run_condition_List.FindAll(o => o.index.Equals(condition.index));
                if (list.Count == 0)
                {
                    if (Form1.Run_condition_count < 10)
                    {
                        REG.검색요청(condition.index);
                        Form1.Run_condition_count++;
                    }
                    else
                    {
                        if (checkbox != null) checkbox.Checked = false;
                        if (combobox != null) combobox.SelectedIndex = 0;

                        Helper.알림창_멀티("검색식 가동확인", where + " - 검색식은 10개 까지 사용할수 있습니다.", 10, false);
                        return false;
                    }
                }
            }

            Form1.위치별검색식리스트[where].이름 = condition.name;
            Form1.위치별검색식리스트[where].실행여부 = true;

            Form1.Run_condition_List.Add(new RunCondition(condition.index, condition.name, where));


            RunCondition 시장가 = Form1.Run_condition_List.Find(o => o.name.Equals("매수탐색_A") || o.name.Equals("매수탐색_B") || o.name.Equals("매도탐색"));
            if (시장가 == null) Form1.시장가탐색 = false;
            else Form1.시장가탐색 = true;

            if (Form1.로딩완료) Form1.비프음("실행");
            Log.동작기록("[검색식 RUN] " + where + " - 검색식[ " + condition.name + " ]이 실시간 감시를 시작 합니다.");

            return true;
        }




        // 실시간 조건식 사용 해제
        public static void Stop_Monitoring(Condition condition, string where)
        {
            Form1.Console_print("Stop_Monitoring 위치: " + where + " 검색식: " + condition.name);

            if (condition.index != "index")
            {
                List<RunCondition> list = Form1.Run_condition_List.FindAll(o => o.index.Equals(condition.index));
                if (list.Count == 1)
                {
                    REG.검색해제(condition.index);
                    Form1.Run_condition_count--;
                }
            }

            Form1.위치별검색식리스트[where].실행여부 = false;

            RunCondition item = Form1.Run_condition_List.Find(o => o.index.Equals(condition.index) && o.where.Equals(where));
            if (item != null) Form1.Run_condition_List.Remove(item);

            RunCondition 시장가 = Form1.Run_condition_List.Find(o => o.name.Equals("매수탐색_A") || o.name.Equals("매수탐색_B") || o.name.Equals("매도탐색"));
            if (시장가 == null) Form1.시장가탐색 = false;
            else Form1.시장가탐색 = true;

            Log.동작기록("[검색식 STOP] " + where + "검색식 [ " + condition.name + " ]이 실시간 감시를 해제 합니다.");
            if (Form1.로딩완료) Form1.비프음("정지");
        }

        // 전역 변수(또는 클래스 멤버)로 선언되어 있다고 가정합니다.
        // (이미 차단된 검색식을 또 차단하지 않기 위함)
        private static HashSet<string> overLoadSearchNames = new HashSet<string>();

        public static void 검색식사용제한()
        {
            // 1. 4초 주기 초기화 로직
            // (ConcurrentDictionary는 Clear해도 안전합니다)
            Get.검색식_tick++;
            if (Get.검색식_tick >= 4)
            {
                Get.검색식_tick = 0;

                // [변경] Map 전체 비우기 (매우 빠름)
                Form1.form1.Condition_Catch_Map.Clear();

                // (선택사항) 4초마다 차단 목록도 초기화해서 다시 기회를 줄지, 
                // 아니면 영구 차단할지에 따라 이 줄을 넣거나 뺍니다.
                // overLoadSearchNames.Clear(); 
            }

            // 2. 과부하 검색식 감지 로직
            if (로딩완료 && (server_알림.Contains("마켓") || server_알림.Contains("동시")))
            {
                // [최적화 핵심] 
                // 기존: 리스트 전체를 루프 돌며 Split + Count (느림)
                // 변경: 딕셔너리의 Key(검색식)만 확인하면 됨 (빠름)

                foreach (var item in Form1.form1.Condition_Catch_Map)
                {
                    string 검색식 = item.Key;

                    // 이미 차단된 검색식은 패스 (불필요한 연산 방지)
                    if (overLoadSearchNames.Contains(검색식)) continue;

                    // [속도 최적화] HashSet의 개수만 확인하면 끝! (O(1))
                    int count = 0;

                    // HashSet은 Thread-Safe하지 않으므로 읽을 때 lock 권장
                    // (ConcurrentDictionary 안에 있는 HashSet이라도 내용은 보호해야 함)
                    lock (item.Value)
                    {
                        count = item.Value.Count;
                    }

                    // 3. 100개 초과 시 차단 로직
                    if (count > 100)
                    {
                        overLoadSearchNames.Add(검색식); // 차단 목록에 추가

                        string message = $"[{검색식}] 식이 사용 중지됩니다.\n(원인: 1초당 {count}개 이상 과다 포착)";

                        AutoClosingAlram(message, "검색식 과부하 경고", 1800, "동작");

                        // 로그 기록
                        Log.동작기록("=================================");
                        Log.동작기록(message);
                        Log.동작기록("=================================");
                        Log.에러기록(message);

                        // 강제 중지 로직 실행 (API 요청 중단)
                        검색식강제중지(검색식);

                        // [정리] 메모리 절약을 위해 해당 검색식의 종목 리스트 비우기
                        lock (item.Value)
                        {
                            item.Value.Clear();
                        }
                    }
                }
            }
        }

        public static void 검색식강제중지(string 검색식)
        {
            Helper.안전한_UI_업데이트(Form1.form1, () =>
           {
               if (Form1.위치별검색식리스트["신규_A"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "신규_A");
               if (Form1.위치별검색식리스트["신규_B"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "신규_B");
               if (Form1.위치별검색식리스트["신규_C"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "신규_C");

               if (Form1.위치별검색식리스트["반복_A"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_A");
               if (Form1.위치별검색식리스트["반복_B"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_B");
               if (Form1.위치별검색식리스트["반복_C"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_C");
               if (Form1.위치별검색식리스트["반복_D"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_D");
               if (Form1.위치별검색식리스트["반복_E"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_E");
               if (Form1.위치별검색식리스트["반복_F"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_F");
               if (Form1.위치별검색식리스트["반복_G"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_G");
               if (Form1.위치별검색식리스트["반복_H"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_H");
               if (Form1.위치별검색식리스트["반복_I"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_I");
               if (Form1.위치별검색식리스트["반복_J"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_J");
               if (Form1.위치별검색식리스트["반복_K"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_K");
               if (Form1.위치별검색식리스트["반복_L"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_L");
               if (Form1.위치별검색식리스트["반복_M"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_M");
               if (Form1.위치별검색식리스트["반복_N"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "반복_N");

               if (Form1.위치별검색식리스트["리밸_A"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_A");
               if (Form1.위치별검색식리스트["리밸_B"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_B");
               if (Form1.위치별검색식리스트["리밸_C"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_C");
               if (Form1.위치별검색식리스트["리밸_D"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_D");
               if (Form1.위치별검색식리스트["리밸_E"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_E");
               if (Form1.위치별검색식리스트["리밸_F"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_F");
               if (Form1.위치별검색식리스트["리밸_G"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_G");
               if (Form1.위치별검색식리스트["청산_A"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "청산_A");
               if (Form1.위치별검색식리스트["청산_B"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "청산_B");
               if (Form1.위치별검색식리스트["청산_C"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "청산_C");

               if (Form1.위치별검색식리스트["와치_A"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "Watch_A");
               if (Form1.위치별검색식리스트["와치_B"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "Watch_B");
               if (Form1.위치별검색식리스트["와치_C"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "Watch_C");
               if (Form1.위치별검색식리스트["와치_D"].이름.Equals(검색식)) Stop_Monitoring(리턴(), "Watch_D");
           });

            Condition 리턴()
            {
                Condition result = Form1.ConditionList.Find(o => o.name.Equals(검색식));
                return result;
            }
        }

        public static void 검색식사용불가_강제정지()
        {
            // [최적화 1] 전체 개수가 40개 이하라면 아예 로직을 수행하지 않아 CPU를 아낍니다.
            if (Form1.신규조회_List.Count <= 40) return;

            // [최적화 2] Dictionary를 사용하여 한 번만 순회하면서 개수를 셉니다. (LINQ 사용 안 함)
            // ConcurrentDictionary는 Values 속성으로 값들에 접근합니다.
            Dictionary<string, int> 카운터 = new Dictionary<string, int>();
            string 과부하_검색식 = "";

            foreach (var item in Form1.신규조회_List.Values)
            {
                if (item == null) continue;

                string 현재검색식 = item.검색식;

                if (!카운터.ContainsKey(현재검색식))
                    카운터[현재검색식] = 0;

                카운터[현재검색식]++;

                // [최적화 3] 세다가 40개가 넘는 순간 즉시 루프를 멈추고 해당 검색식을 범인으로 지목합니다.
                if (카운터[현재검색식] > 40)
                {
                    과부하_검색식 = 현재검색식;
                    break;
                }
            }

            // 40개 초과한 검색식이 발견된 경우에만 정지 로직 실행
            if (!string.IsNullOrEmpty(과부하_검색식))
            {
                string 위치 = "";

                // 검색식 명칭 비교 및 정지 실행
                if (과부하_검색식.Equals(Form1.위치별검색식리스트["신규_A"].이름) && GenieConfig.CB_new_A)
                {
                    위치 = "신규_A";
                    Stop_Monitoring(리턴(과부하_검색식), 위치);
                    알림(위치, 과부하_검색식);
                }
                else if (과부하_검색식.Equals(Form1.위치별검색식리스트["신규_B"].이름) && GenieConfig.CB_new_B)
                {
                    위치 = "신규_B";
                    Stop_Monitoring(리턴(과부하_검색식), 위치);
                    알림(위치, 과부하_검색식);
                }
                else if (과부하_검색식.Equals(Form1.위치별검색식리스트["신규_C"].이름) && GenieConfig.CB_new_C)
                {
                    위치 = "신규_C";
                    Stop_Monitoring(리턴(과부하_검색식), 위치);
                    알림(위치, 과부하_검색식);
                }
            }

            // [로컬 함수] 알림 (변수들을 인자로 받도록 수정하여 안전성 확보)
            void 알림(string pos, string name)
            {
                Form1.AutoClosingAlram(pos + " [" + name + "] 검색식을 강제 중지합니다. \n신규매수 검색식의 검색허용치는 초당 40개로 제한됩니다.", "검색허용치 초과", 1800, "동작");
                Log.에러기록(pos + " [" + name + "] 검색식을 강제 중지합니다. 신규매수 검색식의 검색허용치는 초당 40개로 제한됩니다.");
            }

            // [로컬 함수] Condition 리턴 (인자 추가)
            Condition 리턴(string name)
            {
                return Form1.ConditionList.Find(o => o.name.Equals(name));
            }
        }


        public static void 대금탐색취소(String 검색식)
        {
            if (Form1.위치별검색식리스트["신규_A"].이름.Equals(검색식)) GenieConfig.CB_new_A = false;
            if (Form1.위치별검색식리스트["신규_B"].이름.Equals(검색식)) GenieConfig.CB_new_B = false;
            if (Form1.위치별검색식리스트["신규_C"].이름.Equals(검색식)) GenieConfig.CB_new_C = false;

            if (Form1.위치별검색식리스트["와치_A"].이름.Equals(검색식)) Form1.form1.CB_watch_use_A.Checked = false;
            if (Form1.위치별검색식리스트["와치_B"].이름.Equals(검색식)) Form1.form1.CB_watch_use_B.Checked = false;
            if (Form1.위치별검색식리스트["와치_C"].이름.Equals(검색식)) Form1.form1.CB_watch_use_C.Checked = false;
            if (Form1.위치별검색식리스트["와치_D"].이름.Equals(검색식)) Form1.form1.CB_watch_use_D.Checked = false;

            if (Form1.위치별검색식리스트["반복_A"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_A = 0;
            if (Form1.위치별검색식리스트["반복_B"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_B = 0;
            if (Form1.위치별검색식리스트["반복_C"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_C = 0;
            if (Form1.위치별검색식리스트["반복_D"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_D = 0;
            if (Form1.위치별검색식리스트["반복_E"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_E = 0;
            if (Form1.위치별검색식리스트["반복_F"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_F = 0;
            if (Form1.위치별검색식리스트["반복_G"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_G = 0;
            if (Form1.위치별검색식리스트["반복_H"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_H = 0;
            if (Form1.위치별검색식리스트["반복_I"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_I = 0;
            if (Form1.위치별검색식리스트["반복_J"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_J = 0;
            if (Form1.위치별검색식리스트["반복_K"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_K = 0;
            if (Form1.위치별검색식리스트["반복_L"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_L = 0;
            if (Form1.위치별검색식리스트["반복_M"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_M = 0;
            if (Form1.위치별검색식리스트["반복_N"].이름.Equals(검색식)) GenieConfig.combo_repeat_use_condition_N = 0;

            if (Form1.위치별검색식리스트["리밸_A"].이름.Equals(검색식)) GenieConfig.combo_rebalance_use_condition_A = 0;
            if (Form1.위치별검색식리스트["리밸_B"].이름.Equals(검색식)) GenieConfig.combo_rebalance_use_condition_B = 0;
            if (Form1.위치별검색식리스트["리밸_C"].이름.Equals(검색식)) GenieConfig.combo_rebalance_use_condition_C = 0;
            if (Form1.위치별검색식리스트["리밸_D"].이름.Equals(검색식)) GenieConfig.combo_rebalance_use_condition_D = 0;
            if (Form1.위치별검색식리스트["리밸_E"].이름.Equals(검색식)) GenieConfig.combo_rebalance_use_condition_E = 0;
            if (Form1.위치별검색식리스트["리밸_F"].이름.Equals(검색식)) GenieConfig.combo_rebalance_use_condition_F = 0;
            if (Form1.위치별검색식리스트["리밸_G"].이름.Equals(검색식)) GenieConfig.combo_rebalance_use_condition_G = 0;
            if (Form1.위치별검색식리스트["청산_A"].이름.Equals(검색식)) GenieConfig.CBB_Liquidation_use_condition_A = 0;
            if (Form1.위치별검색식리스트["청산_B"].이름.Equals(검색식)) GenieConfig.CBB_Liquidation_use_condition_B = 0;
            if (Form1.위치별검색식리스트["청산_C"].이름.Equals(검색식)) GenieConfig.CBB_Liquidation_use_condition_C = 0;
        }

        public static void Catch_Stock_List_Clear(string key)
        {
            var keysToRemove = Form1.Catch_Stock_List
           .Where(kvp => kvp.Key.Contains(key))
           .Select(kvp => kvp.Key)
           .ToList();

            foreach (string keyToRemove in keysToRemove)
            {
                Form1.Catch_Stock_List.TryRemove(keyToRemove, out _);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////           검색식 종목 진입&이탈 메소드              ////////////////

        public static void RealCondition_item_search(string 종목코드, string 일련번호, string 삽입삭제)
        {
            // 1. 일련번호로 검색식 찾기
            Condition 검색식 = Form1.ConditionList.Find(o => o.index.Equals(일련번호));
            if (검색식 == null) return; // 검색식을 찾지 못한 경우 예외 방지

            if (Form1.server_알림.Contains("마켓") || Form1.server_알림.Contains("동시"))
            {
                if (Form1.Market_Item_List.ContainsKey(종목코드))
                {
                    // [수정 핵심] 딕셔너리에서 해당 검색식의 방(HashSet)을 가져오거나 없으면 새로 만듭니다.
                    // GetOrAdd는 쓰레드 세이프하게 방을 생성해줍니다.
                    var 종목셋 = Form1.form1.Condition_Catch_Map.GetOrAdd(검색식.name, k => new HashSet<string>());

                    // [최적화] HashSet 자체는 멀티스레드에 안전하지 않으므로 추가/삭제 시 lock을 걸어줍니다.
                    lock (종목셋)
                    {
                        if (삽입삭제 == "I") // 종목 포착(Insert)
                        {
                            // HashSet이라 중복 종목은 알아서 걸러집니다.
                            종목셋.Add(종목코드);
                        }
                        else if (삽입삭제 == "D") // 종목 이탈(Delete)
                        {
                            // 이탈한 종목은 리스트에서 제거해서 카운트에서 제외합니다.
                            종목셋.Remove(종목코드);
                        }
                    }

                    // 기존 매수/감시 로직 실행
                    Item_search(삽입삭제, 종목코드, 검색식.name);
                }
            }
        }

        public static void Item_search(string 삽입삭제, string Code, string conditionName)
        {
            string SearchTime = Get.TimeNow.ToString("##:##:##");

            UnifiedDataManager.Instance.Condition.Enqueue(async () =>
            {
             await    Tab_Basic.New_Buy(삽입삭제, Code, conditionName);
                Tab_Repeat.Repeat_condition(삽입삭제, Code, conditionName);
                Tab_AccountManagement.Rebalancing_condition(삽입삭제, Code, conditionName);
                Tab_AccountManagement.Liquidation_condition(삽입삭제, Code, conditionName);
                Tab_Watch.Watch_In_Out(삽입삭제, Code, conditionName, SearchTime);

                SearchView_add(삽입삭제, Code, conditionName, SearchTime);

                Tab_InterestGroup.관심_검색종목_등록실행(Code, conditionName, 삽입삭제);
                Tab_InterestGroup.관심검색_실시간보기(conditionName, 삽입삭제, Code);
            });
        }

        public static void SearchView_add(string 삽입삭제, string Code, string conditionName, string SearchTime)
        {
            Helper.안전한_UI_업데이트(Form1.form1, () =>
           {
               if (conditionName.Equals(Form1.form1.CBB_SearchCondition.Text) && 삽입삭제.Equals("I"))
               {
                   if (!Form1.form1.SearchView_List.Contains(Code))
                   {
                       Form1.form1.SearchView_List.Add(Code);
                       Form1.form1.Search_List.Items.Insert(0, SearchTime + " | " + Form1.Market_Item_List[Code].종목명);

                       if (Form1.form1.Search_List.Items[1].ToString().Contains("지니64오토스탁")) Form1.form1.Search_List.Items.RemoveAt(1);
                       if (Form1.form1.Search_List.Items.Count > 8) Form1.form1.Search_List.Items.RemoveAt(8);
                   }
               }
           });
        }
        public static void API_OnReceiveTRCondition(string Code, string 일련번호)
        {
            // 1. [데이터 연산 구역] 에러 방어 및 데이터 처리 0.001초 컷
            string itemcode = Code.Substring(1);

            // .Equals 대신 == 로 속도 향상, 못 찾았을 경우(null) 뻗는 현상 완벽 방어
            Condition 검색식 = Form1.ConditionList.FirstOrDefault(o => o.index == 일련번호);

            // 검색식이 없거나 시장에 없는 종목이면 즉시 컷! (NullReference 에러 원천 차단)
            if (검색식 == null || !Form1.Market_Item_List.ContainsKey(itemcode))
            {
                return;
            }

            string 검색식명 = 검색식.name;

            // UI 스레드를 괴롭히지 않고 백그라운드에서 리스트 검사 및 추가
            if (Form1.form1.검색결과_List.Contains(검색식명) && !Form1.form1.검색결과_List.Contains(itemcode))
            {
                Form1.form1.검색결과_List.Add(itemcode);
            }

            if (GenieConfig.CB_편입추가)
            {
                Item_search("I", itemcode, 검색식명);
            }

            Tab_InterestGroup.관심_검색종목_등록실행(itemcode, 검색식명, "I");

            // 2. [UI 업데이트 구역] 중복 호출 방지 및 최소화
            if (Form1.로딩완료)
            {
                Helper.안전한_UI_업데이트(Form1.form1, () =>
                {
                    // 이미 0번이 아닐 때만 0번으로 변경! (종목이 100개 쏟아져도 화면 변경은 딱 1번만 일어남)
                    if (Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex != 0)
                    {
                        Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 0;
                        Tab_InterestGroup.CBB_실시간n그룹n관심자동_indexchange(0);
                    }
                });
            }
        }
        ///////////////           검색식 종목 진입&이탈 메소드              /////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////

    }
}
