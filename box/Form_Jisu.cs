using System;
using System.Drawing;
using System.Windows.Forms;

namespace 지니64.box
{
    public partial class Form_Jisu : UserControl
    {
        public static Form_Jisu form;
        private string 현재선택된_그룹명 = "";

        public Form_Jisu()
        {
            form = this;
            InitializeComponent();
        }

        public void Jisu_Form_Load()
        {
            Form1.음소거 = true;

            CB_지수이평_신규.Checked = true; // 기본값으로 "신규" 그룹 선택

            // [기능 및 기타 설정] -> Setting.function 사용
            if (GenieConfig.CB_가이드매매) ControllerDisable.Form_Jisu_Disable();
            Form1.음소거 = GenieConfig.CB_음소거;
        }


        public void Jisu_avg_Label_print()
        {
            if (Form1.FormJisu_Open)
            {
                지수이평추세 avg = Form1.지수이평추세[0];
                label_kospi_min_03.Text = Print(avg.Min_추세_03, label_kospi_min_03);
                label_kospi_min_05.Text = Print(avg.Min_추세_05, label_kospi_min_05);
                label_kospi_min_10.Text = Print(avg.Min_추세_10, label_kospi_min_10);
                label_kospi_min_20.Text = Print(avg.Min_추세_20, label_kospi_min_20);
                label_kospi_min_30.Text = Print(avg.Min_추세_30, label_kospi_min_30);
                label_kospi_min_60.Text = Print(avg.Min_추세_60, label_kospi_min_60);
                label_kospi_day_03.Text = Print(avg.Day_추세_03, label_kospi_day_03);
                label_kospi_day_05.Text = Print(avg.Day_추세_05, label_kospi_day_05);
                label_kospi_day_10.Text = Print(avg.Day_추세_10, label_kospi_day_10);
                label_kospi_day_20.Text = Print(avg.Day_추세_20, label_kospi_day_20);
                label_kospi_day_40.Text = Print(avg.Day_추세_40, label_kospi_day_40);
                label_kospi_day_60.Text = Print(avg.Day_추세_60, label_kospi_day_60);

                avg = Form1.지수이평추세[1];
                label_kosdaq_min_03.Text = Print(avg.Min_추세_03, label_kosdaq_min_03);
                label_kosdaq_min_05.Text = Print(avg.Min_추세_05, label_kosdaq_min_05);
                label_kosdaq_min_10.Text = Print(avg.Min_추세_10, label_kosdaq_min_10);
                label_kosdaq_min_20.Text = Print(avg.Min_추세_20, label_kosdaq_min_20);
                label_kosdaq_min_30.Text = Print(avg.Min_추세_30, label_kosdaq_min_30);
                label_kosdaq_min_60.Text = Print(avg.Min_추세_60, label_kosdaq_min_60);
                label_kosdaq_day_03.Text = Print(avg.Day_추세_03, label_kosdaq_day_03);
                label_kosdaq_day_05.Text = Print(avg.Day_추세_05, label_kosdaq_day_05);
                label_kosdaq_day_10.Text = Print(avg.Day_추세_10, label_kosdaq_day_10);
                label_kosdaq_day_20.Text = Print(avg.Day_추세_20, label_kosdaq_day_20);
                label_kosdaq_day_40.Text = Print(avg.Day_추세_40, label_kosdaq_day_40);
                label_kosdaq_day_60.Text = Print(avg.Day_추세_60, label_kosdaq_day_60);
            }

            static string Print(bool 이평추세, Label label)
            {
                if (이평추세)
                {
                    label.ForeColor = Color.Red;
                    return "▲";
                }
                else
                {
                    label.ForeColor = Color.Blue;
                    return "▼";
                }
            }
        }

        private void CB_use_CheckedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            CheckBox CB = sender as CheckBox;
            if (CB.Checked)
            {
                CB.Text = "■" + CB.Text[1..];
            }
            else
            {
                CB.Text = "□" + CB.Text[1..];
            }
        }

        private void CB_UD_CheckedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            CheckBox CB = sender as CheckBox;
            if (CB.Checked)
            {
                CB.Text = "초과";
                CB.ForeColor = Color.Red;
            }
            else
            {
                CB.Text = "이하";
                CB.ForeColor = Color.Black;
            }
        }

        private void BT_설정저장_Click(object sender, EventArgs e)
        {
            Form1.form1.Form_Top_Most();
            Form1.MBC_sender = "BT_지수이평저장";
            Form1.중요메세지("지수이평설정", "지수이평설정 저장 하시겠습니까?");
        }

        public void SAVE_지수이평()
        {
            if (!string.IsNullOrEmpty(현재선택된_그룹명))
            {
                지수이평화면_저장하기(현재선택된_그룹명);
            }
        }

        // [+] 클래스 전역 변수로 선언하여 매번 배열을 새로 만드는 메모리 낭비를 방지합니다.
        private CheckBox[] _지수이평_체크박스_목록 = null;
        // [+] 이벤트가 연쇄적으로 폭주하는 것을 막기 위한 안전장치 (락)
        private bool _체크박스_변경중 = false;

        private void CheckBox_checked(object sender, EventArgs e)
        {
            // 1. 코드로 체크 상태를 바꿀 때 함수가 무한 반복 실행되는 것을 완벽히 차단합니다.
            if (_체크박스_변경중) return;
            if (!(sender is CheckBox 클릭된_체크박스)) return;

            // 2. 최초 1회만 배열에 담아두고 평생 재사용합니다. (속도 향상 핵심)
            if (_지수이평_체크박스_목록 == null)
            {
                _지수이평_체크박스_목록 = new CheckBox[]
                {
                 CB_지수이평_신규, CB_지수이평_그외,
                 CB_지수이평_반복_A, CB_지수이평_반복_B, CB_지수이평_반복_C, CB_지수이평_반복_D,
                 CB_지수이평_반복_E, CB_지수이평_반복_F, CB_지수이평_반복_G, CB_지수이평_반복_H,
                 CB_지수이평_반복_I, CB_지수이평_반복_J, CB_지수이평_반복_K, CB_지수이평_반복_L,
                 CB_지수이평_반복_M, CB_지수이평_반복_N,
                 CB_지수이평_리밸_A, CB_지수이평_리밸_B, CB_지수이평_리밸_C, CB_지수이평_리밸_D,
                 CB_지수이평_리밸_E, CB_지수이평_리밸_F, CB_지수이평_리밸_G
                };
            }

            // 3. 방어 로직: 끄려고 할 때 다른 켜진 항목이 없으면 끄기를 취소합니다.
            if (!클릭된_체크박스.Checked)
            {
                bool 다른항목_체크됨 = false;
                foreach (CheckBox 대상_체크박스 in _지수이평_체크박스_목록)
                {
                    if (대상_체크박스 != null && 대상_체크박스 != 클릭된_체크박스 && 대상_체크박스.Checked)
                    {
                        다른항목_체크됨 = true;
                        break;
                    }
                }

                if (!다른항목_체크됨)
                {
                    _체크박스_변경중 = true; // 락을 걸고
                    클릭된_체크박스.Checked = true; // 다시 켭니다
                    _체크박스_변경중 = false; // 락을 해제합니다
                    return;
                }
            }

            // === [중요] 여기서부터 UI 상태 변경 시작 (이벤트 락 걸기) ===
            _체크박스_변경중 = true;

            try
            {
                // 4. 텍스트 기호 변경 (■ / □)
                if (클릭된_체크박스.Text.Length > 0)
                {
                    클릭된_체크박스.Text = (클릭된_체크박스.Checked ? "■" : "□") + 클릭된_체크박스.Text[1..];
                }

                // 본인이 해제된 상황이라면 더 이상 진행할 필요 없으므로 종료
                if (!클릭된_체크박스.Checked) return;

                // 5. 새롭게 체크된 경우 관련 함수 실행
                현재선택된_그룹명 = 클릭된_체크박스.Name;

                Form1.Console_print($"선택된 그룹: {현재선택된_그룹명}");

                지수이평화면_불러오기(현재선택된_그룹명);

                // 6. 다른 체크박스 모두 끄기 (락이 걸려있어 이벤트가 다시 호출되지 않아 매우 빠릅니다)
                foreach (CheckBox 대상_체크박스 in _지수이평_체크박스_목록)
                {
                    if (대상_체크박스 != null && 대상_체크박스 != 클릭된_체크박스 && 대상_체크박스.Checked)
                    {
                        대상_체크박스.Checked = false;

                        // 꺼지는 대상의 텍스트도 여기서 바로 수정해 줍니다.
                        if (대상_체크박스.Text.Length > 0 && 대상_체크박스.Text.StartsWith("■"))
                        {
                            대상_체크박스.Text = "□" + 대상_체크박스.Text[1..];
                        }
                    }
                }
            }
            finally
            {
                // 정상 동작이든, 에러가 발생하든 무조건 락을 해제하여 다음 클릭을 준비합니다.
                _체크박스_변경중 = false;
            }
        }

        // ==========================================
        // [+] 현재 화면의 체크박스 상태를 그룹 이름으로 저장 (UI 설정 보존 + 실제 로직 변수 적용)
        // ==========================================
        public void 지수이평화면_저장하기(string 저장할그룹명)
        {
            if (string.IsNullOrEmpty(저장할그룹명)) return;

            // 1. GenieConfig 용 UI 상태 저장 객체 생성
            지수설정_세트 새설정 = new 지수설정_세트
            {
                지수이평_사용_kospi = CB_지수이평사용_kospi.Checked,
                지수이평_사용_kosdaq = CB_지수이평사용_kosdaq.Checked,

                Use_kospi_min_03 = CB_use_kospi_min_03.Checked,
                Use_kospi_min_05 = CB_use_kospi_min_05.Checked,
                Use_kospi_min_10 = CB_use_kospi_min_10.Checked,
                Use_kospi_min_20 = CB_use_kospi_min_20.Checked,
                Use_kospi_min_30 = CB_use_kospi_min_30.Checked,
                Use_kospi_min_60 = CB_use_kospi_min_60.Checked,

                Use_kospi_day_03 = CB_use_kospi_day_03.Checked,
                Use_kospi_day_05 = CB_use_kospi_day_05.Checked,
                Use_kospi_day_10 = CB_use_kospi_day_10.Checked,
                Use_kospi_day_20 = CB_use_kospi_day_20.Checked,
                Use_kospi_day_40 = CB_use_kospi_day_40.Checked,
                Use_kospi_day_60 = CB_use_kospi_day_60.Checked,

                UD_kospi_min_03 = CB_UD_kospi_min_03.Checked,
                UD_kospi_min_05 = CB_UD_kospi_min_05.Checked,
                UD_kospi_min_10 = CB_UD_kospi_min_10.Checked,
                UD_kospi_min_20 = CB_UD_kospi_min_20.Checked,
                UD_kospi_min_30 = CB_UD_kospi_min_30.Checked,
                UD_kospi_min_60 = CB_UD_kospi_min_60.Checked,

                UD_kospi_day_03 = CB_UD_kospi_day_03.Checked,
                UD_kospi_day_05 = CB_UD_kospi_day_05.Checked,
                UD_kospi_day_10 = CB_UD_kospi_day_10.Checked,
                UD_kospi_day_20 = CB_UD_kospi_day_20.Checked,
                UD_kospi_day_40 = CB_UD_kospi_day_40.Checked,
                UD_kospi_day_60 = CB_UD_kospi_day_60.Checked,

                Use_kosdaq_min_03 = CB_use_kosdaq_min_03.Checked,
                Use_kosdaq_min_05 = CB_use_kosdaq_min_05.Checked,
                Use_kosdaq_min_10 = CB_use_kosdaq_min_10.Checked,
                Use_kosdaq_min_20 = CB_use_kosdaq_min_20.Checked,
                Use_kosdaq_min_30 = CB_use_kosdaq_min_30.Checked,
                Use_kosdaq_min_60 = CB_use_kosdaq_min_60.Checked,

                Use_kosdaq_day_03 = CB_use_kosdaq_day_03.Checked,
                Use_kosdaq_day_05 = CB_use_kosdaq_day_05.Checked,
                Use_kosdaq_day_10 = CB_use_kosdaq_day_10.Checked,
                Use_kosdaq_day_20 = CB_use_kosdaq_day_20.Checked,
                Use_kosdaq_day_40 = CB_use_kosdaq_day_40.Checked,
                Use_kosdaq_day_60 = CB_use_kosdaq_day_60.Checked,

                UD_kosdaq_min_03 = CB_UD_kosdaq_min_03.Checked,
                UD_kosdaq_min_05 = CB_UD_kosdaq_min_05.Checked,
                UD_kosdaq_min_10 = CB_UD_kosdaq_min_10.Checked,
                UD_kosdaq_min_20 = CB_UD_kosdaq_min_20.Checked,
                UD_kosdaq_min_30 = CB_UD_kosdaq_min_30.Checked,
                UD_kosdaq_min_60 = CB_UD_kosdaq_min_60.Checked,

                UD_kosdaq_day_03 = CB_UD_kosdaq_day_03.Checked,
                UD_kosdaq_day_05 = CB_UD_kosdaq_day_05.Checked,
                UD_kosdaq_day_10 = CB_UD_kosdaq_day_10.Checked,
                UD_kosdaq_day_20 = CB_UD_kosdaq_day_20.Checked,
                UD_kosdaq_day_40 = CB_UD_kosdaq_day_40.Checked,
                UD_kosdaq_day_60 = CB_UD_kosdaq_day_60.Checked
            };

            // 딕셔너리에 설정 정보 덮어쓰기
            GenieConfig.그룹별_지수설정[저장할그룹명] = 새설정;

            // ==========================================
            // 2. 실제 매매 로직이 사용할 그룹별 변수(그룹별_지수사용값) 적용
            // ==========================================

            // 해당 그룹의 배열이 메모리에 없다면 새로 만들어줍니다. (방어 코드)
            // [*변경포인트*] 딕셔너리 이름을 '그룹별_지수사용값'으로 수정했습니다.
            if (!Form1.그룹별_지수사용값.ContainsKey(저장할그룹명))
            {
                Form1.그룹별_지수사용값[저장할그룹명] = new 지수이평사용값[2];
                Form1.그룹별_지수사용값[저장할그룹명][0] = new 지수이평사용값();
                Form1.그룹별_지수사용값[저장할그룹명][1] = new 지수이평사용값();
            }

            // [코스피 (Index 0)] 실제 변수에 적용
            Form1.그룹별_지수사용값[저장할그룹명][0].사용유무 = CB_지수이평사용_kospi.Checked; // [+] 추가됨

            Form1.그룹별_지수사용값[저장할그룹명][0].Use_min_03 = CB_use_kospi_min_03.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].Use_min_05 = CB_use_kospi_min_05.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].Use_min_10 = CB_use_kospi_min_10.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].Use_min_20 = CB_use_kospi_min_20.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].Use_min_30 = CB_use_kospi_min_30.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].Use_min_60 = CB_use_kospi_min_60.Checked;

            Form1.그룹별_지수사용값[저장할그룹명][0].Use_day_03 = CB_use_kospi_day_03.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].Use_day_05 = CB_use_kospi_day_05.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].Use_day_10 = CB_use_kospi_day_10.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].Use_day_20 = CB_use_kospi_day_20.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].Use_day_40 = CB_use_kospi_day_40.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].Use_day_60 = CB_use_kospi_day_60.Checked;

            Form1.그룹별_지수사용값[저장할그룹명][0].추세사용값_min_03 = CB_UD_kospi_min_03.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].추세사용값_min_05 = CB_UD_kospi_min_05.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].추세사용값_min_10 = CB_UD_kospi_min_10.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].추세사용값_min_20 = CB_UD_kospi_min_20.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].추세사용값_min_30 = CB_UD_kospi_min_30.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].추세사용값_min_60 = CB_UD_kospi_min_60.Checked;

            Form1.그룹별_지수사용값[저장할그룹명][0].추세사용값_day_03 = CB_UD_kospi_day_03.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].추세사용값_day_05 = CB_UD_kospi_day_05.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].추세사용값_day_10 = CB_UD_kospi_day_10.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].추세사용값_day_20 = CB_UD_kospi_day_20.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].추세사용값_day_40 = CB_UD_kospi_day_40.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][0].추세사용값_day_60 = CB_UD_kospi_day_60.Checked;

            // [코스닥 (Index 1)] 실제 변수에 적용
            Form1.그룹별_지수사용값[저장할그룹명][1].사용유무 = CB_지수이평사용_kosdaq.Checked; // [+] 추가됨

            Form1.그룹별_지수사용값[저장할그룹명][1].Use_min_03 = CB_use_kosdaq_min_03.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].Use_min_05 = CB_use_kosdaq_min_05.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].Use_min_10 = CB_use_kosdaq_min_10.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].Use_min_20 = CB_use_kosdaq_min_20.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].Use_min_30 = CB_use_kosdaq_min_30.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].Use_min_60 = CB_use_kosdaq_min_60.Checked;

            Form1.그룹별_지수사용값[저장할그룹명][1].Use_day_03 = CB_use_kosdaq_day_03.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].Use_day_05 = CB_use_kosdaq_day_05.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].Use_day_10 = CB_use_kosdaq_day_10.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].Use_day_20 = CB_use_kosdaq_day_20.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].Use_day_40 = CB_use_kosdaq_day_40.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].Use_day_60 = CB_use_kosdaq_day_60.Checked;

            Form1.그룹별_지수사용값[저장할그룹명][1].추세사용값_min_03 = CB_UD_kosdaq_min_03.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].추세사용값_min_05 = CB_UD_kosdaq_min_05.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].추세사용값_min_10 = CB_UD_kosdaq_min_10.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].추세사용값_min_20 = CB_UD_kosdaq_min_20.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].추세사용값_min_30 = CB_UD_kosdaq_min_30.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].추세사용값_min_60 = CB_UD_kosdaq_min_60.Checked;

            Form1.그룹별_지수사용값[저장할그룹명][1].추세사용값_day_03 = CB_UD_kosdaq_day_03.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].추세사용값_day_05 = CB_UD_kosdaq_day_05.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].추세사용값_day_10 = CB_UD_kosdaq_day_10.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].추세사용값_day_20 = CB_UD_kosdaq_day_20.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].추세사용값_day_40 = CB_UD_kosdaq_day_40.Checked;
            Form1.그룹별_지수사용값[저장할그룹명][1].추세사용값_day_60 = CB_UD_kosdaq_day_60.Checked;

            SaveToFile.지수이평설정_파일저장();
        }

        // ==========================================
        // [+] 지정된 그룹 이름의 설정을 화면 체크박스에 적용
        // ==========================================
        public void 지수이평화면_불러오기(string 불러올그룹명)
        {
            if (string.IsNullOrEmpty(불러올그룹명)) return;

            // 딕셔너리에 데이터가 있다면 꺼내서 적용
            if (GenieConfig.그룹별_지수설정.TryGetValue(불러올그룹명, out 지수설정_세트 설정값))
            {
                CB_지수이평사용_kospi.Checked = 설정값.지수이평_사용_kospi;
                CB_지수이평사용_kosdaq.Checked = 설정값.지수이평_사용_kosdaq;

                CB_use_kospi_min_03.Checked = 설정값.Use_kospi_min_03;
                CB_use_kospi_min_05.Checked = 설정값.Use_kospi_min_05;
                CB_use_kospi_min_10.Checked = 설정값.Use_kospi_min_10;
                CB_use_kospi_min_20.Checked = 설정값.Use_kospi_min_20;
                CB_use_kospi_min_30.Checked = 설정값.Use_kospi_min_30;
                CB_use_kospi_min_60.Checked = 설정값.Use_kospi_min_60;

                CB_use_kospi_day_03.Checked = 설정값.Use_kospi_day_03;
                CB_use_kospi_day_05.Checked = 설정값.Use_kospi_day_05;
                CB_use_kospi_day_10.Checked = 설정값.Use_kospi_day_10;
                CB_use_kospi_day_20.Checked = 설정값.Use_kospi_day_20;
                CB_use_kospi_day_40.Checked = 설정값.Use_kospi_day_40;
                CB_use_kospi_day_60.Checked = 설정값.Use_kospi_day_60;

                CB_UD_kospi_min_03.Checked = 설정값.UD_kospi_min_03;
                CB_UD_kospi_min_05.Checked = 설정값.UD_kospi_min_05;
                CB_UD_kospi_min_10.Checked = 설정값.UD_kospi_min_10;
                CB_UD_kospi_min_20.Checked = 설정값.UD_kospi_min_20;
                CB_UD_kospi_min_30.Checked = 설정값.UD_kospi_min_30;
                CB_UD_kospi_min_60.Checked = 설정값.UD_kospi_min_60;

                CB_UD_kospi_day_03.Checked = 설정값.UD_kospi_day_03;
                CB_UD_kospi_day_05.Checked = 설정값.UD_kospi_day_05;
                CB_UD_kospi_day_10.Checked = 설정값.UD_kospi_day_10;
                CB_UD_kospi_day_20.Checked = 설정값.UD_kospi_day_20;
                CB_UD_kospi_day_40.Checked = 설정값.UD_kospi_day_40;
                CB_UD_kospi_day_60.Checked = 설정값.UD_kospi_day_60;

                CB_use_kosdaq_min_03.Checked = 설정값.Use_kosdaq_min_03;
                CB_use_kosdaq_min_05.Checked = 설정값.Use_kosdaq_min_05;
                CB_use_kosdaq_min_10.Checked = 설정값.Use_kosdaq_min_10;
                CB_use_kosdaq_min_20.Checked = 설정값.Use_kosdaq_min_20;
                CB_use_kosdaq_min_30.Checked = 설정값.Use_kosdaq_min_30;
                CB_use_kosdaq_min_60.Checked = 설정값.Use_kosdaq_min_60;

                CB_use_kosdaq_day_03.Checked = 설정값.Use_kosdaq_day_03;
                CB_use_kosdaq_day_05.Checked = 설정값.Use_kosdaq_day_05;
                CB_use_kosdaq_day_10.Checked = 설정값.Use_kosdaq_day_10;
                CB_use_kosdaq_day_20.Checked = 설정값.Use_kosdaq_day_20;
                CB_use_kosdaq_day_40.Checked = 설정값.Use_kosdaq_day_40;
                CB_use_kosdaq_day_60.Checked = 설정값.Use_kosdaq_day_60;

                CB_UD_kosdaq_min_03.Checked = 설정값.UD_kosdaq_min_03;
                CB_UD_kosdaq_min_05.Checked = 설정값.UD_kosdaq_min_05;
                CB_UD_kosdaq_min_10.Checked = 설정값.UD_kosdaq_min_10;
                CB_UD_kosdaq_min_20.Checked = 설정값.UD_kosdaq_min_20;
                CB_UD_kosdaq_min_30.Checked = 설정값.UD_kosdaq_min_30;
                CB_UD_kosdaq_min_60.Checked = 설정값.UD_kosdaq_min_60;

                CB_UD_kosdaq_day_03.Checked = 설정값.UD_kosdaq_day_03;
                CB_UD_kosdaq_day_05.Checked = 설정값.UD_kosdaq_day_05;
                CB_UD_kosdaq_day_10.Checked = 설정값.UD_kosdaq_day_10;
                CB_UD_kosdaq_day_20.Checked = 설정값.UD_kosdaq_day_20;
                CB_UD_kosdaq_day_40.Checked = 설정값.UD_kosdaq_day_40;
                CB_UD_kosdaq_day_60.Checked = 설정값.UD_kosdaq_day_60;
            }
        }


        // 그룹별 지수 설정을 담아둘 전용 상자 (총 52개 항목)
        public class 지수설정_세트
        {
            // 1. 공통 정지 설정
            public bool 지수이평_사용_kospi { get; set; }
            public bool 지수이평_사용_kosdaq { get; set; }

            // 2. 코스피 분봉 사용
            public bool Use_kospi_min_03 { get; set; }
            public bool Use_kospi_min_05 { get; set; }
            public bool Use_kospi_min_10 { get; set; }
            public bool Use_kospi_min_20 { get; set; }
            public bool Use_kospi_min_30 { get; set; }
            public bool Use_kospi_min_60 { get; set; }

            // 3. 코스피 일봉 사용
            public bool Use_kospi_day_03 { get; set; }
            public bool Use_kospi_day_05 { get; set; }
            public bool Use_kospi_day_10 { get; set; }
            public bool Use_kospi_day_20 { get; set; }
            public bool Use_kospi_day_40 { get; set; }
            public bool Use_kospi_day_60 { get; set; }

            // 4. 코스피 분봉 UD
            public bool UD_kospi_min_03 { get; set; }
            public bool UD_kospi_min_05 { get; set; }
            public bool UD_kospi_min_10 { get; set; }
            public bool UD_kospi_min_20 { get; set; }
            public bool UD_kospi_min_30 { get; set; }
            public bool UD_kospi_min_60 { get; set; }

            // 5. 코스피 일봉 UD
            public bool UD_kospi_day_03 { get; set; }
            public bool UD_kospi_day_05 { get; set; }
            public bool UD_kospi_day_10 { get; set; }
            public bool UD_kospi_day_20 { get; set; }
            public bool UD_kospi_day_40 { get; set; }
            public bool UD_kospi_day_60 { get; set; }

            // 6. 코스닥 분봉 사용
            public bool Use_kosdaq_min_03 { get; set; }
            public bool Use_kosdaq_min_05 { get; set; }
            public bool Use_kosdaq_min_10 { get; set; }
            public bool Use_kosdaq_min_20 { get; set; }
            public bool Use_kosdaq_min_30 { get; set; }
            public bool Use_kosdaq_min_60 { get; set; }

            // 7. 코스닥 일봉 사용
            public bool Use_kosdaq_day_03 { get; set; }
            public bool Use_kosdaq_day_05 { get; set; }
            public bool Use_kosdaq_day_10 { get; set; }
            public bool Use_kosdaq_day_20 { get; set; }
            public bool Use_kosdaq_day_40 { get; set; }
            public bool Use_kosdaq_day_60 { get; set; }

            // 8. 코스닥 분봉 UD
            public bool UD_kosdaq_min_03 { get; set; }
            public bool UD_kosdaq_min_05 { get; set; }
            public bool UD_kosdaq_min_10 { get; set; }
            public bool UD_kosdaq_min_20 { get; set; }
            public bool UD_kosdaq_min_30 { get; set; }
            public bool UD_kosdaq_min_60 { get; set; }

            // 9. 코스닥 일봉 UD
            public bool UD_kosdaq_day_03 { get; set; }
            public bool UD_kosdaq_day_05 { get; set; }
            public bool UD_kosdaq_day_10 { get; set; }
            public bool UD_kosdaq_day_20 { get; set; }
            public bool UD_kosdaq_day_40 { get; set; }
            public bool UD_kosdaq_day_60 { get; set; }
        }
    }
}
