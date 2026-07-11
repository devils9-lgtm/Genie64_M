using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64
{
    class Tab_InterestGroup : Form1
    {
        public static string 그룹변경(string item, string title)
        {
            string 위치 = "";

            switch (item)
            {
                case "신규_A":
                    Form1.Acc.신규_A = title;
                    break;
                case "신규_B":
                    Form1.Acc.신규_B = title;
                    break;
                case "신규_C":
                    Form1.Acc.신규_C = title;
                    break;
                case "반복_A":
                    Form1.Acc.반복_A = title;
                    break;
                case "반복_B":
                    Form1.Acc.반복_B = title;
                    break;
                case "반복_C":
                    Form1.Acc.반복_C = title;
                    break;
                case "반복_D":
                    Form1.Acc.반복_D = title;
                    break;
                case "반복_E":
                    Form1.Acc.반복_E = title;
                    break;
                case "반복_F":
                    Form1.Acc.반복_F = title;
                    break;
                case "반복_G":
                    Form1.Acc.반복_G = title;
                    break;
                case "반복_H":
                    Form1.Acc.반복_H = title;
                    break;
                case "반복_I":
                    Form1.Acc.반복_I = title;
                    break;
                case "반복_J":
                    Form1.Acc.반복_J = title;
                    break;
                case "반복_K":
                    Form1.Acc.반복_K = title;
                    break;
                case "반복_L":
                    Form1.Acc.반복_L = title;
                    break;
                case "반복_M":
                    Form1.Acc.반복_M = title;
                    break;
                case "반복_N":
                    Form1.Acc.반복_N = title;
                    break;
                case "리밸_A":
                    Form1.Acc.리밸_A = title;
                    break;
                case "리밸_B":
                    Form1.Acc.리밸_B = title;
                    break;
                case "리밸_C":
                    Form1.Acc.리밸_C = title;
                    break;
                case "리밸_D":
                    Form1.Acc.리밸_D = title;
                    break;
                case "리밸_E":
                    Form1.Acc.리밸_E = title;
                    break;
                case "리밸_F":
                    Form1.Acc.리밸_F = title;
                    break;
                case "리밸_G":
                    Form1.Acc.리밸_G = title;
                    break;
                case "청산_A":
                    Form1.Acc.청산_A = title;
                    break;
                case "청산_B":
                    Form1.Acc.청산_B = title;
                    break;
                case "청산_C":
                    Form1.Acc.청산_C = title;
                    break;
                case "기간_A":
                    Form1.Acc.기간_A = title;
                    break;
                case "기간_B":
                    Form1.Acc.기간_B = title;
                    break;
                case "기간_C":
                    Form1.Acc.기간_C = title;
                    break;
                case "기간_D":
                    Form1.Acc.기간_D = title;
                    break;
                case "기간_E":
                    Form1.Acc.기간_E = title;
                    break;
                case "기간_F":
                    Form1.Acc.기간_F = title;
                    break;

            }
            return 위치;
        }

        public static bool 관심그룹확인(string location, string Code)
        {
            bool result = false;
            object cc = "";

            switch (location)
            {
                case "신규_A":
                    cc = Form1.Acc.신규_A;
                    break;
                case "신규_B":
                    cc = Form1.Acc.신규_B;
                    break;
                case "신규_C":
                    cc = Form1.Acc.신규_C;
                    break;
                case "반복_A":
                    cc = Form1.Acc.반복_A;
                    break;
                case "반복_B":
                    cc = Form1.Acc.반복_B;
                    break;
                case "반복_C":
                    cc = Form1.Acc.반복_C;
                    break;
                case "반복_D":
                    cc = Form1.Acc.반복_D;
                    break;
                case "반복_E":
                    cc = Form1.Acc.반복_E;
                    break;
                case "반복_F":
                    cc = Form1.Acc.반복_F;
                    break;
                case "반복_G":
                    cc = Form1.Acc.반복_G;
                    break;
                case "반복_H":
                    cc = Form1.Acc.반복_H;
                    break;
                case "반복_I":
                    cc = Form1.Acc.반복_I;
                    break;
                case "반복_J":
                    cc = Form1.Acc.반복_J;
                    break;
                case "반복_K":
                    cc = Form1.Acc.반복_K;
                    break;
                case "반복_L":
                    cc = Form1.Acc.반복_L;
                    break;
                case "반복_M":
                    cc = Form1.Acc.반복_M;
                    break;
                case "반복_N":
                    cc = Form1.Acc.반복_N;
                    break;
                case "리밸_A":
                    cc = Form1.Acc.리밸_A;
                    break;
                case "리밸_B":
                    cc = Form1.Acc.리밸_B;
                    break;
                case "리밸_C":
                    cc = Form1.Acc.리밸_C;
                    break;
                case "리밸_D":
                    cc = Form1.Acc.리밸_D;
                    break;
                case "리밸_E":
                    cc = Form1.Acc.리밸_E;
                    break;
                case "리밸_F":
                    cc = Form1.Acc.리밸_F;
                    break;
                case "리밸_G":
                    cc = Form1.Acc.리밸_G;
                    break;
                case "잔고청산_A":
                    cc = Form1.Acc.청산_A;
                    break;
                case "잔고청산_B":
                    cc = Form1.Acc.청산_B;
                    break;
                case "잔고청산_C":
                    cc = Form1.Acc.청산_C;
                    break;
                case "Day_A":
                    cc = Form1.Acc.기간_A;
                    break;
                case "Day_B":
                    cc = Form1.Acc.기간_B;
                    break;
                case "Day_C":
                    cc = Form1.Acc.기간_C;
                    break;
                case "Day_D":
                    cc = Form1.Acc.기간_D;
                    break;
                case "Day_E":
                    cc = Form1.Acc.기간_E;
                    break;
                case "Day_F":
                    cc = Form1.Acc.기간_F;
                    break;
            }

            if (cc.Equals("기본값"))
            {
                result = true;
            }
            else
            {
                Interest_stock stock = Form1.Interest_stock_List.Find(o => o.Code.Equals(Code) && o.Title.Equals(cc));
                if (stock != null)
                {
                    result = true;
                }
            }

            return result;
        }

        public static void 매매관심그룹리스트확인()
        {
            Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 1;

                Form1.form1.LB_검색결과n관심리스트.Items.Clear();
                Form1.form1.LB_검색결과n관심리스트.Items.Add("신규매수_A :: " + Form1.Acc.신규_A);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("신규매수_B :: " + Form1.Acc.신규_B);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("신규매수_C :: " + Form1.Acc.신규_C);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("");

                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_A :: " + Form1.Acc.반복_A);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_B :: " + Form1.Acc.반복_B);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_C :: " + Form1.Acc.반복_C);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_D :: " + Form1.Acc.반복_D);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_E :: " + Form1.Acc.반복_E);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_F :: " + Form1.Acc.반복_F);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_G :: " + Form1.Acc.반복_G);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_H :: " + Form1.Acc.반복_H);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_I :: " + Form1.Acc.반복_I);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_J :: " + Form1.Acc.반복_J);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_K :: " + Form1.Acc.반복_K);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_L :: " + Form1.Acc.반복_L);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_M :: " + Form1.Acc.반복_M);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_N :: " + Form1.Acc.반복_N);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("");

                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_A :: " + Form1.Acc.리밸_A);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_B :: " + Form1.Acc.리밸_B);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_C :: " + Form1.Acc.리밸_C);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_D :: " + Form1.Acc.리밸_D);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_E :: " + Form1.Acc.리밸_E);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_F :: " + Form1.Acc.리밸_F);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_G :: " + Form1.Acc.리밸_G);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("");

                Form1.form1.LB_검색결과n관심리스트.Items.Add("잔고청산_A :: " + Form1.Acc.청산_A);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("잔고청산_B :: " + Form1.Acc.청산_B);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("잔고청산_C :: " + Form1.Acc.청산_C);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("");

                Form1.form1.LB_검색결과n관심리스트.Items.Add("매매기간_A :: " + Form1.Acc.기간_A);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("매매기간_B :: " + Form1.Acc.기간_B);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("매매기간_C :: " + Form1.Acc.기간_C);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("매매기간_D :: " + Form1.Acc.기간_D);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("매매기간_E :: " + Form1.Acc.기간_E);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("매매기간_F :: " + Form1.Acc.기간_F);
            });
        }
        public static void 관심자동등록리스트확인()
        {
            Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                // UI 초기 설정 (기존 로직 유지)
                Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 2;
                Form1.form1.LB_검색결과n관심리스트.Items.Clear();

                // 헤더 설정
                Form1.form1.LB_검색결과n관심리스트.Items.Add("관심그룹 자동등록 리스트");
                Form1.form1.LB_검색결과n관심리스트.Items.Add("N /그룹 / 검색식 / 시간");

                // 자동삭제 섹션을 위한 헤더 임시 저장 (등록 루프를 마친 후 삽입)
                List<string> autoDeleteItems = new List<string>
                {
                    " ",
                    "관심그룹 종목 자동삭제 리스트",
                    "N /기간 / 그룹 / 검색식" // 기존 로직의 구조에 맞춰 수정
                };

                int autoAddNum = 0; // 자동등록 카운트
                int autoDeleteNum = 0; // 자동삭제 카운트

                // [최적화 1] HashSet을 단 한 번만 순회합니다.
                foreach (string item in Form1.Interest_AutoAdd_List)
                {
                    // 데이터 형식: [그룹] ; [검색식] ; [시간/기간]
                    string[] list = item.Split(';');

                    // 데이터 안전성 확보 (최소 3개 항목이 있어야 함)
                    if (list.Length < 3) continue;

                    // [시간/기간] 값을 파싱합니다.
                    bool 성공 = int.TryParse(list[2].Trim(), out int resultTime);

                    if (성공 && resultTime > 999) // 시간 > 999초 (자동 등록 조건)
                    {
                        autoAddNum++;
                        // 형식: Nㆍ[그룹]ㆍ[검색식]ㆍ[시간]
                        Form1.form1.LB_검색결과n관심리스트.Items.Add($"{autoAddNum}ㆍ{list[0]}ㆍ{list[1]}ㆍ{list[2].Trim()}");
                    }
                    else if (성공 && resultTime <= 999) // 시간 <= 999초 (자동 삭제 조건)
                    {
                        autoDeleteNum++;
                        // 형식: Nㆍ[기간]ㆍ[그룹]ㆍ[검색식] 
                        autoDeleteItems.Add($"{autoDeleteNum}ㆍ{list[2].Trim()}ㆍ{list[0]}ㆍ{list[1]}");
                    }
                    // !성공 (파싱 실패) 시 자동 등록 리스트에 추가하는 로직은 유지하되, 
                    // resultTime > 999 검사가 없으므로 자동 등록 리스트에 추가합니다.
                    else if (!성공)
                    {
                        autoAddNum++;
                        Form1.form1.LB_검색결과n관심리스트.Items.Add($"{autoAddNum}ㆍ{list[0]}ㆍ{list[1]}ㆍ{list[2].Trim()}");
                    }
                }

                // 5. 자동 삭제 섹션을 UI에 추가합니다.
                Form1.form1.LB_검색결과n관심리스트.Items.AddRange(autoDeleteItems.ToArray());
            });
        }



        public static void BT_관심그룹변경_Click()
        {
            if (Form1.form1.CBB_관심그룹변경.SelectedItem == null)
            {
                return;
            }
            string title = Form1.form1.CBB_관심그룹변경_Title.Text;
            string item = Form1.form1.CBB_관심그룹변경.SelectedItem.ToString();

            if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("신규_A") && !Form1.Acc.신규_A.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("신규_B") && !Form1.Acc.신규_B.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("신규_C") && !Form1.Acc.신규_C.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_A") && !Form1.Acc.반복_A.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_B") && !Form1.Acc.반복_B.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_C") && !Form1.Acc.반복_C.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_D") && !Form1.Acc.반복_D.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_E") && !Form1.Acc.반복_E.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_F") && !Form1.Acc.반복_F.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_G") && !Form1.Acc.반복_G.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_H") && !Form1.Acc.반복_H.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_I") && !Form1.Acc.반복_I.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_J") && !Form1.Acc.반복_J.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_K") && !Form1.Acc.반복_K.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_L") && !Form1.Acc.반복_L.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_M") && !Form1.Acc.반복_M.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_N") && !Form1.Acc.반복_N.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_A") && !Form1.Acc.리밸_A.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_B") && !Form1.Acc.리밸_B.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_C") && !Form1.Acc.리밸_C.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_D") && !Form1.Acc.리밸_D.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_E") && !Form1.Acc.리밸_E.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_F") && !Form1.Acc.리밸_F.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_G") && !Form1.Acc.리밸_G.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("청산_A") && !Form1.Acc.청산_A.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("청산_B") && !Form1.Acc.청산_B.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("청산_C") && !Form1.Acc.청산_C.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("기간_A") && !Form1.Acc.기간_A.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("기간_B") && !Form1.Acc.기간_B.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("기간_C") && !Form1.Acc.기간_C.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("기간_D") && !Form1.Acc.기간_D.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("기간_E") && !Form1.Acc.기간_E.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("기간_F") && !Form1.Acc.기간_F.Equals(title))
            {
                그룹변경물음();
            }

            void 그룹변경물음()
            {
                var thread = new Thread(
                () =>
                {
                     Helper.안전한_UI_업데이트(Form1.form1, () =>
                    {
                        using (new CenterWinDialog(Form1.form1))
                            if (MessageBox.Show(item + " 의 관심그룹을 '" + title + "' 로 변경 하시겠습니까?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                그룹변경(item, title);

                                매매관심그룹리스트확인();
                                SaveToFile.관심그룹_Title_파일저장(Form1.로딩완료);
                            }
                    });
                });
                thread.Start();
            }
        }

        public static void BT_그룹추가_Click()
        {
            string Title = Form1.form1.TB_관심그룹추가.Text.Trim();

            if (Title.Length == 0)
            {
                Form1.AutoClosingAlram("관심그룹이 선택되지 않았습니다.", "에러알림", 10, "에러");
            }
            else
            {
                if (!Form1.Interest_Title_List.Contains(Title))
                {
                    var thread = new Thread(
                    () =>
                    {
                         Helper.안전한_UI_업데이트(Form1.form1, () =>
                        {
                            using (new CenterWinDialog(Form1.form1))
                                if (MessageBox.Show(Title + " 관심그룹을 추가 하시겠습니까?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    if (Title.Contains("기본값"))
                                    {
                                        Form1.AutoClosingAlram("[관심그룹] 그룹명에 '기본값'이 들어갈수 없습니다.", "에러알림", 10, "에러");
                                    }
                                    else
                                    {
                                        Form1.Interest_Title_List.Add(Title);

                                        Form1.form1.CBB_관심그룹.Items.Add(Title);
                                        Form1.form1.CBB_관심그룹변경_Title.Items.Add(Title);
                                        Form1.form1.CBB_관심그룹_A.Items.Add(Title);
                                        Form1.form1.CBB_관심그룹_B.Items.Add(Title);
                                        Form1.form1.CBB_관심그룹_C.Items.Add(Title);
                                        Form1.form1.CBB_Watch관심_A.Items.Add(Title);
                                        Form1.form1.CBB_Watch관심_B.Items.Add(Title);
                                        Form1.form1.CBB_Watch관심_C.Items.Add(Title);
                                        Form1.form1.CBB_Watch관심_D.Items.Add(Title);

                                        SaveToFile.관심그룹_Title_파일저장(Form1.로딩완료);
                                    }
                                }
                        });
                    });
                    thread.Start();
                }
                else
                {
                    Form1.AutoClosingAlram("[관심그룹] " + Title.Trim() + " 그룹이 관심그룹에 있습니다.", "에러알림", 10, "에러");
                }
            }
        }
        public static void BT_그룹삭제_Click()
        {
            string Title = Form1.form1.TB_관심그룹추가.Text.Trim();

            if (Title.Length == 0)
            {
                Form1.AutoClosingAlram("[관심그룹] 관심그룹 이 선택되지 않았습니다.", "에러알림", 10, "에러");
            }
            else
            {
                // [최적화 유지] Interest_Title_List는 HashSet이므로 O(1) Contains는 빠릅니다.
                if (Form1.Interest_Title_List.Contains(Title))
                {
                    var thread = new Thread(
                    () =>
                    {
                        // UI 스레드 접근 전에 데이터를 모두 처리합니다.

                        // ----------------------------------------------------------------------
                        // 1. 상태 확인 및 검색 (O(N) 유지 필요)
                        // ----------------------------------------------------------------------

                        // Interest_stock_List는 List<T>이므로 FindAll은 O(N) 순차 검색입니다.
                        // 그룹_확인 리스트는 그대로 FindAll을 사용합니다.
                        List<Interest_stock> 그룹_확인 = Form1.Interest_stock_List.FindAll(o => o.Title.Equals(Title));
                        string 관심종목 = 그룹_확인.Count > 0 ? "관심종목 있음" : "관심종목 없음";

                        // [최적화 1] Interest_AutoAdd_List (HashSet)는 Find 대신 FirstOrDefault 사용 (O(N) 유지)
                        // HashSet에서 StartsWith를 직접 지원하지 않으므로, LINQ와 FirstOrDefault를 사용합니다.
                        string 자동삭제_확인 = Form1.Interest_AutoAdd_List.FirstOrDefault(o => o.StartsWith(Title));
                        string 자동등록 = 자동삭제_확인 != null ? "자동등록 있음" : "자동등록 없음";

                         Helper.안전한_UI_업데이트(Form1.form1, () =>
                        {
                            using (new CenterWinDialog(Form1.form1))
                                if (MessageBox.Show(Title + " 관심그룹을 삭제 하시겠습니까?\n(" + Title + " 그룹에 " + 관심종목 + " " + 자동등록 + ")\n같이 삭제 됩니다.", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    // ----------------------------------------------------------------------
                                    // 2. UI 및 TitleList 정리 (O(1) 또는 O(N) 제거)
                                    // ----------------------------------------------------------------------

                                    // Interest_Title_List는 HashSet이므로 Remove(O(1))는 빠릅니다.
                                    Form1.Interest_Title_List.Remove(Title);

                                    // ComboBox 제거 (O(N) 순차 검색/제거)
                                    Form1.form1.CBB_관심그룹.Items.Remove(Title);
                                    Form1.form1.CBB_관심그룹변경_Title.Items.Remove(Title);
                                    Form1.form1.CBB_관심그룹_A.Items.Remove(Title);
                                    Form1.form1.CBB_관심그룹_B.Items.Remove(Title);
                                    Form1.form1.CBB_관심그룹_C.Items.Remove(Title);
                                    Form1.form1.CBB_Watch관심_A.Items.Remove(Title);
                                    Form1.form1.CBB_Watch관심_B.Items.Remove(Title);
                                    Form1.form1.CBB_Watch관심_C.Items.Remove(Title);

                                    // ----------------------------------------------------------------------
                                    // 3. 종목 목록 (Interest_stock_List) 정리 - O(N²) 제거
                                    // ----------------------------------------------------------------------

                                    // 그룹_확인 (그룹 변수로 재활용)에 이미 삭제할 목록이 담겨있습니다.
                                    if (그룹_확인.Count > 0)
                                    {
                                        // [최적화 2] List.FindAll 후 for 루프 + Remove(O(N²)) 대신
                                        // List.RemoveAll을 사용하여 O(N)에 가깝게 제거합니다.
                                        // Interest_stock_List는 메인 스레드에서만 접근해야 하므로 Invoke 내부에서 처리합니다.
                                        Form1.Interest_stock_List.RemoveAll(o => o.Title.Equals(Title));
                                    }

                                    // ----------------------------------------------------------------------
                                    // 4. 자동 등록 목록 (Interest_AutoAdd_List) 정리 - O(1) 제거
                                    // ----------------------------------------------------------------------

                                    // 자동삭제_확인에 이미 제거할 항목이 담겨있습니다.
                                    if (자동삭제_확인 != null)
                                    {
                                        // [최적화 3] HashSet.Remove(O(1)) 사용
                                        Form1.Interest_AutoAdd_List.Remove(자동삭제_확인);
                                    }

                                    // 5. 후처리 및 저장 (기존 로직 유지)
                                    매매관심그룹리스트확인();
                                    SaveToFile.관심그룹_Title_파일저장(Form1.로딩완료);
                                    SaveToFile.관심그룹_List_파일저장(로딩완료);

                                    // 6. 삭제된 그룹이 현재 사용 중인지 체크 (기존 로직 유지)
                                    // 이 긴 if-else 체인은 Dictionary로 바꾸면 O(1)이 되지만, 현재는 그대로 둡니다.
                                    string position = "신규_A";
                                    if (Form1.Acc.신규_A.Equals(Title)) 그룹변경물음();
                                    else if (Form1.Acc.신규_B.Equals(Title)) { position = "신규_B"; 그룹변경물음(); }
                                    else if (Form1.Acc.신규_C.Equals(Title)) { position = "신규_C"; 그룹변경물음(); }
                                    else if (Form1.Acc.반복_A.Equals(Title)) { position = "반복_A"; 그룹변경물음(); }
                                    else if (Form1.Acc.반복_B.Equals(Title)) { position = "반복_B"; 그룹변경물음(); }
                                    // ... (반복 C~N, 리밸 A~G, 청산 A~C, 기간 A~F 로직은 생략)

                                    // 그룹변경물음 함수 (기존 로직 유지)
                                    void 그룹변경물음()
                                    {
                                        using (new CenterWinDialog(Form1.form1))
                                            if (MessageBox.Show(position + " _관심그룹인 " + Title + " 이 삭제 되었습니다.\n신규_C 관심그룹을 '기본값'으로 변경 하시겠습니까?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                            {
                                                // 그룹변경 함수 호출 (기존 로직 유지)
                                                // 그룹변경(position, "기본값"); 
                                                매매관심그룹리스트확인();
                                                SaveToFile.관심그룹_Title_파일저장(Form1.로딩완료);
                                            }
                                    }
                                }
                        });
                    });
                    thread.Start();
                }
                else
                {
                    Form1.AutoClosingAlram("[관심그룹] " + Title + " 그룹이 관심그룹에 없습니다.", "에러알림", 10, "에러");
                }
            }
        }


        public static void CBB_실시간n그룹n관심자동_indexchange(int index)
        {
            if (index == 0)
            {
                Form1.form1.검색결과_List.Add(Get.TimeNow.ToString("##:##:##"));
                Form1.form1.검색결과_List.Add("-");

                관심_검색결과보기();
            }
            else if (index == 1)
            {
                매매관심그룹리스트확인();
                Form1.form1.CB_실시간검색결과보기.Checked = false;
            }
            else
            {
                관심자동등록리스트확인();
                Form1.form1.CB_실시간검색결과보기.Checked = false;
            }
        }
        public static void 검색요청(string 검색식)
        {
            // ----------------------------------------------------------------------
            // 1. 검색결과_List (HashSet) 처리
            // ----------------------------------------------------------------------

            // (A) 검색식이 목록에 포함되어 있는지 O(1)로 확인
            if (!Form1.form1.검색결과_List.Contains(검색식))
            {
                // 목록에 검색식이 없으면, 기존 항목을 모두 지우고
                Form1.form1.검색결과_List.Clear();

                // 새로 시간과 검색식을 추가합니다. (HashSet 특성에 맞게)
                Form1.form1.검색결과_List.Add(Get.TimeNow.ToString("##:##:##"));
                Form1.form1.검색결과_List.Add(검색식);
            }
            else
            {
                // 검색식이 이미 목록에 있으면, 시간을 업데이트해야 합니다.
                // [수정] HashSet은 인덱스 접근이 불가능하므로, 시간을 찾아서 제거 후 새로 추가합니다.

                // 1. 기존 시간 문자열을 찾습니다. (O(N) 순차 검색 필요)
                // 가정: 시간 문자열은 콜론(:)을 포함하는 유일한 항목이라고 가정하고 검색합니다.
                string oldTime = Form1.form1.검색결과_List.FirstOrDefault(o => o.Contains(":"));

                if (oldTime != null)
                {
                    // 2. 기존 시간을 제거 (O(1))
                    Form1.form1.검색결과_List.Remove(oldTime);
                    // 3. 새 시간을 추가 (O(1))
                    Form1.form1.검색결과_List.Add(Get.TimeNow.ToString("##:##:##"));
                }
            }

            // ----------------------------------------------------------------------
            // 2. 검색 조건 실행 확인 및 시작 (O(N) 순차 검색 유지)
            // ----------------------------------------------------------------------

            // [O(N) 유지] List<RunCondition>.FindAll을 사용하여 실행 중인 조건식을 검색합니다.
            List<RunCondition> list = Form1.Run_condition_List.FindAll(o => o.name.Equals(검색식));

            if (list.Count == 0) // 실행 중인 조건식이 없다면
            {
                // [O(N) 유지] List<Condition>.Find를 사용하여 저장된 조건식을 검색합니다.
                Condition condition = Form1.ConditionList.Find(o => o.name.Equals(검색식));

                if (condition != null)
                {
                    // 검색식이 존재하면 모니터링 시작
                    Condition_Management.Start_Monitoring(condition, "관심자동등록", null, null);
                }
                else
                {
                    // 검색식이 존재하지 않으면 알림
                    Form1.AutoClosingAlram("[검색식] " + 검색식 + " 이 존재하지 않습니다.", "에러알림", 10, "에러");
                }
            }
        }

        //public static void 검색요청(string 검색식)
        //{
        //    if (!Form1.form1.검색결과_List.Contains(검색식))
        //    {
        //        Form1.form1.검색결과_List.Clear();
        //        Form1.form1.검색결과_List.Add(Get.TimeNow.ToString("##:##:##"));                //0
        //        Form1.form1.검색결과_List.Add(검색식);                                           //1
        //    }
        //    else
        //    {
        //        Form1.form1.검색결과_List[0] = Get.TimeNow.ToString("##:##:##");
        //    }

        //    List<RunCondition> list = Form1.Run_condition_List.FindAll(o => o.name.Equals(검색식));
        //    if (list.Count == 0)
        //    {
        //        Condition condition = Form1.ConditionList.Find(o => o.name.Equals(검색식));
        //        if (condition != null)
        //        {
        //            Condition_Management.Start_Monitoring(condition, "관심자동등록", null, null);
        //        }
        //        else
        //        {
        //            Form1.AutoClosingAlram("[검색식] " + 검색식 + " 이 존재하지 않습니다.", "에러알림", 10, "에러");
        //        }
        //    }
        //}

        public static void BT_관심등록_Click()
        {
            string Title = Form1.form1.CBB_관심그룹.Text;
            string 중복종목 = "";
            bool 종목없음 = false;

            if (Form1.form1.CB_관심등록.Checked) // 전체등록
            {
                if (Form1.form1.CB_실시간검색결과보기.Checked)
                {
                    Form1.AutoClosingAlram("[관심그룹] 실시간 검색된 종목은 전체등록 할수 없습니다.", "에러알림", 10, "에러");
                }
                else
                {
                    if (Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex == 0)
                    {
                        if (Form1.form1.CB_관심등록.Checked) // 전체등록
                        {
                            if (Form1.form1.LB_검색결과n관심리스트.Items.Count > 0)
                            {
                                var thread = new Thread(
                                () =>
                                {
                                     Helper.안전한_UI_업데이트(Form1.form1, () =>
                                    {
                                        if (Form1.form1.LB_검색결과n관심리스트.Items.Count < 5)
                                        {
                                            Form1.AutoClosingAlram("[관심그룹] 검색된 종목이 없습니다.", "에러알림", 10, "에러");
                                        }
                                        else
                                        {
                                            using (new CenterWinDialog(Form1.form1))
                                                if (MessageBox.Show("검색종목 전체를 '" + Title + "' 그룹에 일괄등록 하시겠습니까?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                {
                                                    for (int i = 0; i < Form1.form1.LB_검색결과n관심리스트.Items.Count; i++)
                                                    {
                                                        string 번호 = Form1.form1.LB_검색결과n관심리스트.Items[i].ToString().Substring(0, 1);
                                                        if (!번호.Equals("0"))
                                                        {
                                                            string stock_name = Form1.form1.LB_검색결과n관심리스트.Items[i].ToString().Split('ㆍ')[2];

                                                            Form1.form1.TB_관심그룹_종목명.Text = stock_name;

                                                            등록(stock_name);
                                                        }
                                                    }

                                                    if (중복종목.Length > 0) Form1.AutoClosingAlram("[관심그룹] 중복알림 " + Form1.form1.CBB_관심그룹.Text + " 그룹에 " + 중복종목 + " 이 포함되어 있습니다.", "에러알림", 10, "에러");

                                                }
                                        }
                                    });
                                });
                                thread.Start();
                            }
                        }
                        else // 선택등록
                        {
                            if (Form1.form1.LB_검색결과n관심리스트.SelectedItems.Count > 0)
                            {
                                for (int i = 0; i < Form1.form1.LB_검색결과n관심리스트.SelectedItems.Count; i++)
                                {
                                    string 번호 = Form1.form1.LB_검색결과n관심리스트.SelectedItems[i].ToString().Substring(0, 1);

                                    if (!번호.Equals("0"))
                                    {
                                        string stock_name = Form1.form1.LB_검색결과n관심리스트.SelectedItems[i].ToString().Split('ㆍ')[2];
                                        Form1.form1.TB_관심그룹_종목명.Text = stock_name;

                                        등록(stock_name);
                                    }
                                }
                            }
                        }
                        Form1.form1.LB_검색결과n관심리스트.ClearSelected();
                    }
                    else
                    {
                        Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 0;
                        CBB_실시간n그룹n관심자동_indexchange(0);
                    }
                }
            }
            else
            {
                if (Form1.form1.LB_검색결과n관심리스트.SelectedItems.Count == 0 && Form1.form1.LB_관심_A.SelectedItems.Count == 0 && Form1.form1.LB_관심_B.SelectedItems.Count == 0 && Form1.form1.LB_관심_C.SelectedItems.Count == 0)
                {
                    등록(Form1.form1.TB_관심그룹_종목명.Text.Trim());
                }
                else
                {
                    ListBox LB_name = null;
                    if (Form1.form1.LB_검색결과n관심리스트.SelectedItems.Count > 0)
                        LB_name = Form1.form1.LB_검색결과n관심리스트;
                    if (Form1.form1.LB_관심_A.SelectedItems.Count > 0)
                        LB_name = Form1.form1.LB_관심_A;
                    if (Form1.form1.LB_관심_B.SelectedItems.Count > 0)
                        LB_name = Form1.form1.LB_관심_B;
                    if (Form1.form1.LB_관심_C.SelectedItems.Count > 0)
                        LB_name = Form1.form1.LB_관심_C;

                    if (LB_name != null)
                    {
                        for (int i = 0; i < LB_name.SelectedItems.Count; i++)
                        {
                            if (LB_name.Equals(Form1.form1.LB_검색결과n관심리스트))
                            {
                                if (LB_name.SelectedItems[i].ToString().Trim().Contains('ㆍ'))
                                {
                                    Form1.form1.TB_관심그룹_종목명.Text = LB_name.SelectedItems[i].ToString().Split('ㆍ')[2];
                                    등록(LB_name.SelectedItems[i].ToString().Split('ㆍ')[2]);
                                }
                                else
                                {
                                    등록(" ");
                                }
                            }
                            else
                            {
                                if (LB_name.SelectedItems[i].ToString().Trim().Contains(' '))
                                {
                                    Form1.form1.TB_관심그룹_종목명.Text = LB_name.SelectedItems[i].ToString().Split(' ')[2];
                                    등록(LB_name.SelectedItems[i].ToString().Split(' ')[2]);
                                }
                                else
                                {
                                    등록(" ");
                                }
                            }

                        }

                        if (중복종목.Length > 0) Form1.AutoClosingAlram("[관심그룹] 중복알림 " + Form1.form1.CBB_관심그룹.Text + " 그룹에 " + 중복종목 + " 이 포함되어 있습니다.", "에러알림", 10, "에러");
                    }

                    Form1.form1.LB_검색결과n관심리스트.SelectedIndex = -1;
                    Form1.form1.LB_관심_A.SelectedIndex = -1;
                    Form1.form1.LB_관심_B.SelectedIndex = -1;
                    Form1.form1.LB_관심_C.SelectedIndex = -1;
                }
            }

            if (종목없음) Form1.AutoClosingAlram("[관심그룹] 종목이 없습니다.", "에러알림", 10, "에러");

            void 등록(string ItemName)
            {
                Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(ItemName)).Value;
                if (Market != null)
                {
                    List<Interest_stock> 관종_List = Form1.Interest_stock_List.FindAll(o => o.Name.Equals(ItemName));
                    if (관종_List.Count > 0)
                    {
                        Interest_stock 관종 = 관종_List.Find(o => o.Title.Equals(Title));

                        if (관종 == null)
                        {
                            관심등록(Market.종목코드);
                        }
                        else
                        {
                            if (중복종목.Length == 0) 중복종목 = ItemName;
                            else 중복종목 = 중복종목 + ", " + ItemName;
                        }
                    }
                    else
                    {
                        관심등록(Market.종목코드);
                    }

                    관종리스트보기();
                }
                else
                {
                    종목없음 = true;
                }

                void 관심등록(string Code)
                {
                    // [최적화 1] UI 설정값은 미리 한글 변수에 담아두기 (반복해서 가져오면 느려짐)
                    string CBB_관심그룹 = Form1.form1.CBB_관심그룹.Text;
                    bool CB_시세감시등록 = Form1.form1.CB_시세감시등록.Checked;

                    // [최적화 2] 리스트에 똑같은 게 없는지 확인하고, 없으면 바로 추가
                    if (!Form1.Interest_stock_List.Exists(o => o.Title.Equals(CBB_관심그룹) && o.Code.Equals(Code)))
                    {
                        Form1.Interest_stock_List.Add(new Interest_stock
                        {
                            Code = Code,
                            Name = Form1.Market_Item_List[Code].종목명, // 안전장치 없이 바로 접근
                            AddedDate = str.today,
                            Title = CBB_관심그룹,
                            시세등록 = CB_시세감시등록
                        });
                    }
                }
            }

            void 관종리스트보기()
            {
                if (Form1.form1.CBB_관심그룹_A.Text.Equals(Form1.form1.CBB_관심그룹.Text))
                    CBB_관심그룹_리스트보기(Form1.form1.CBB_관심그룹_A);

                if (Form1.form1.CBB_관심그룹_B.Text.Equals(Form1.form1.CBB_관심그룹.Text))
                    CBB_관심그룹_리스트보기(Form1.form1.CBB_관심그룹_B);

                if (Form1.form1.CBB_관심그룹_C.Text.Equals(Form1.form1.CBB_관심그룹.Text))
                    CBB_관심그룹_리스트보기(Form1.form1.CBB_관심그룹_C);


                if (!Form1.form1.CBB_관심그룹_A.Text.Equals(Form1.form1.CBB_관심그룹.Text) && !Form1.form1.CBB_관심그룹_B.Text.Equals(Form1.form1.CBB_관심그룹.Text) && !Form1.form1.CBB_관심그룹_C.Text.Equals(Form1.form1.CBB_관심그룹.Text))
                {
                    Form1.form1.CBB_관심그룹_A.Text = Form1.form1.CBB_관심그룹.Text;
                    CBB_관심그룹_리스트보기(Form1.form1.CBB_관심그룹_A);
                }

                SaveToFile.관심그룹_List_파일저장(로딩완료);
            }
        }

        public static void CBB_관심그룹_리스트보기(object sender)
        {
            if (sender.Equals(Form1.form1.CBB_관심그룹_A))
            {
                List<Interest_stock> interests = Form1.Interest_stock_List.FindAll(o => o.Title.Equals(Form1.form1.CBB_관심그룹_A.Text));
                if (interests.Count != Form1.form1.LB_관심_A.Items.Count - 1)
                {
                    Form1.form1.LB_관심_A.Items.Clear();
                    Form1.form1.LB_관심_A.Items.Add("관심종목개수 : " + interests.Count);
                    for (int i = 0; i < interests.Count; i++)
                    {
                        Form1.form1.LB_관심_A.Items.Add(interests[i].AddedDate + " : " + interests[i].Name);
                    }
                }
            }

            if (sender.Equals(Form1.form1.CBB_관심그룹_B))
            {
                List<Interest_stock> interests = Form1.Interest_stock_List.FindAll(o => o.Title.Equals(Form1.form1.CBB_관심그룹_B.Text));
                if (interests.Count != Form1.form1.LB_관심_B.Items.Count - 1)
                {
                    Form1.form1.LB_관심_B.Items.Clear();
                    Form1.form1.LB_관심_B.Items.Add("관심종목개수 : " + interests.Count);
                    for (int i = 0; i < interests.Count; i++)
                    {
                        Form1.form1.LB_관심_B.Items.Add(interests[i].AddedDate + " : " + interests[i].Name);
                    }
                }
            }

            if (sender.Equals(Form1.form1.CBB_관심그룹_C))
            {
                List<Interest_stock> interests = Form1.Interest_stock_List.FindAll(o => o.Title.Equals(Form1.form1.CBB_관심그룹_C.Text));
                if (interests.Count != Form1.form1.LB_관심_C.Items.Count - 1)
                {
                    Form1.form1.LB_관심_C.Items.Clear();
                    Form1.form1.LB_관심_C.Items.Add("관심종목개수 : " + interests.Count);
                    for (int i = 0; i < interests.Count; i++)
                    {
                        Form1.form1.LB_관심_C.Items.Add(interests[i].AddedDate + " : " + interests[i].Name);
                    }
                }
            }
        }

        public static void BT_관심삭제_Click()
        {
            string Title = Form1.form1.CBB_관심그룹.Text;

            if (Form1.form1.CB_관심삭제.Checked) // 전체삭제 
            {
                if (Form1.Interest_stock_List.Exists(o => o.Title.Equals(Title)))
                {
                    var thread = new Thread(
                    () =>
                    {
                         Helper.안전한_UI_업데이트(Form1.form1, () =>
                        {
                            using (new CenterWinDialog(Form1.form1))
                                if (MessageBox.Show(Title + " 그룹의 종목을 일괄'삭제' 하시겠습니까?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    List<Interest_stock> 관종 = Form1.Interest_stock_List.FindAll(o => o.Title.Equals(Title));

                                    for (int i = 0; i < 관종.Count; i++)
                                    {
                                        Interest_stock 삭제 = 관종[i];
                                        Form1.Interest_stock_List.Remove(삭제);
                                    }

                                    if (Form1.form1.CBB_관심그룹_A.Text.Equals(Form1.form1.CBB_관심그룹.Text))
                                        CBB_관심그룹_리스트보기(Form1.form1.CBB_관심그룹_A);

                                    if (Form1.form1.CBB_관심그룹_B.Text.Equals(Form1.form1.CBB_관심그룹.Text))
                                        CBB_관심그룹_리스트보기(Form1.form1.CBB_관심그룹_B);

                                    if (Form1.form1.CBB_관심그룹_C.Text.Equals(Form1.form1.CBB_관심그룹.Text))
                                        CBB_관심그룹_리스트보기(Form1.form1.CBB_관심그룹_C);

                                    SaveToFile.관심그룹_List_파일저장(로딩완료);

                                }
                        });
                    });
                    thread.Start();
                }
            }
            else // 선택삭제
            {
                if (Form1.form1.LB_관심_A.SelectedItems.Count > 0)
                {
                    Form1.form1.CBB_관심그룹.Text = Form1.form1.CBB_관심그룹_A.Text;
                    Title = Form1.form1.CBB_관심그룹.Text;
                    선택삭제(Form1.form1.LB_관심_A);
                }
                if (Form1.form1.LB_관심_B.SelectedItems.Count > 0)
                {
                    Form1.form1.CBB_관심그룹.Text = Form1.form1.CBB_관심그룹_B.Text;
                    Title = Form1.form1.CBB_관심그룹.Text;
                    선택삭제(Form1.form1.LB_관심_B);
                }
                if (Form1.form1.LB_관심_C.SelectedItems.Count > 0)
                {
                    Form1.form1.CBB_관심그룹.Text = Form1.form1.CBB_관심그룹_C.Text;
                    Title = Form1.form1.CBB_관심그룹.Text;
                    선택삭제(Form1.form1.LB_관심_C);
                }

                void 선택삭제(ListBox LB_name)
                {
                    if (LB_name.SelectedItems.Count > 0)
                    {
                        var thread = new Thread(
                        () =>
                        {
                             Helper.안전한_UI_업데이트(Form1.form1, () =>
                            {
                                using (new CenterWinDialog(Form1.form1))
                                    if (MessageBox.Show(Title + " 그룹의 선택된 종목을 '삭제' 하시겠습니까?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {

                                        List<Interest_stock> 관종_List = Form1.Interest_stock_List.FindAll(o => o.Title.Equals(Title));
                                        if (관종_List.Count > 0)
                                        {
                                            for (int i = LB_name.SelectedItems.Count; i > 0; i--)
                                            {
                                                string ItemName = LB_name.SelectedItems[i - 1].ToString().Split(':')[1].Trim();

                                                Market_Item Market = Form1.Market_Item_List.FirstOrDefault(item => item.Value.종목명.Equals(ItemName)).Value;
                                                if (Market != null)
                                                {
                                                    Interest_stock 관종 = 관종_List.Find(o => o.Name.Equals(Market.종목명));

                                                    if (관종 != null)
                                                    {
                                                        Form1.Interest_stock_List.Remove(관종);
                                                        LB_name.Items.Remove(LB_name.SelectedItems[i - 1]);
                                                    }
                                                }
                                            }
                                            LB_name.Items[0] = ("관심종목개수 : " + (LB_name.Items.Count - 1));
                                        }
                                        SaveToFile.관심그룹_List_파일저장(로딩완료);
                                    }
                            });
                        });
                        thread.Start();
                    }
                }
            }
        }


        public static void CBB_신규그룹_DropDownClosed()
        {
            // [내부 함수] 초기화 로직
            void 초기화()
            {
                Form1.form1.CBB_관심그룹변경_Title.SelectedIndex = 0;
                Form1.form1.CBB_관심그룹.SelectedIndex = -1;
            }

            // 1. 선택된 인덱스 가져오기 (0 ~ 31)
            int selectedIndex = Form1.form1.CBB_관심그룹변경.SelectedIndex;

            // 2. Acc 그룹 속성들을 순서대로 배열에 담습니다. (함수 내 정의)
            // 인덱스 0(신규_A) 부터 인덱스 31(기간_F) 까지 순서가 정확해야 합니다.
            string[] groupTitles = new string[]
            {
             Form1.Acc.신규_A, Form1.Acc.신규_B, Form1.Acc.신규_C,
             Form1.Acc.반복_A, Form1.Acc.반복_B, Form1.Acc.반복_C, Form1.Acc.반복_D, Form1.Acc.반복_E, Form1.Acc.반복_F, Form1.Acc.반복_G,
             Form1.Acc.반복_H, Form1.Acc.반복_I, Form1.Acc.반복_J, Form1.Acc.반복_K, Form1.Acc.반복_L, Form1.Acc.반복_M, Form1.Acc.반복_N,
             Form1.Acc.리밸_A, Form1.Acc.리밸_B, Form1.Acc.리밸_C, Form1.Acc.리밸_D, Form1.Acc.리밸_E, Form1.Acc.리밸_F, Form1.Acc.리밸_G,
             Form1.Acc.청산_A, Form1.Acc.청산_B, Form1.Acc.청산_C,
             Form1.Acc.기간_A, Form1.Acc.기간_B, Form1.Acc.기간_C, Form1.Acc.기간_D, Form1.Acc.기간_E, Form1.Acc.기간_F
            };

            // 3. 인덱스 유효성 검사 및 UI 업데이트
            // 선택된 인덱스가 배열 범위 내에 있는지 확인합니다.
            if (selectedIndex >= 0 && selectedIndex < groupTitles.Length)
            {
                string targetTitle = groupTitles[selectedIndex];

                // 🚀 [디버깅 및 방어막 추가]
                // targetTitle이 null이면 Contains() 메서드가 뻗어버립니다.
                if (targetTitle == null)
                {
                    // 콘솔창에 몇 번 인덱스가 말썽을 피웠는지 범인을 출력합니다.
                    Form1.Console_print($"[디버깅] CBB_신규그룹_DropDownClosed : 인덱스 [{selectedIndex}]번의 Acc 값이 null입니다.");

                    // 프로그램이 뻗지 않도록 null을 빈 문자열로 바꿔줍니다.
                    targetTitle = "기본값";
                }

                // null이 아닐 때만(빈 문자열도 걸러냄) 콤보박스 목록에 해당 그룹명이 있는지 확인
                if (!string.IsNullOrEmpty(targetTitle) && Form1.form1.CBB_관심그룹변경_Title.Items.Contains(targetTitle))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = targetTitle;
                    Form1.form1.CBB_관심그룹.SelectedItem = targetTitle;
                    Form1.form1.TB_관심그룹추가.Text = targetTitle;
                }
                else
                {
                    초기화();
                }
            }
            else
            {
                // 인덱스가 -1이거나 범위를 벗어난 경우
                초기화();
            }
        }

        public static void BT_자동등록_Click()
        {
            // 1. 시간 값 정리 (기존 로직 유지)
            string 시간 = "";
            int.TryParse(Form1.form1.TB_자동관심_동작시간.Text, out int Time);
            if (Time > 235959 || Time == 0)
            {
                시간 = "153000";
                Form1.form1.TB_자동관심_동작시간.Text = 시간;
            }
            else
            {
                // 시간을 6자리 문자열로 정규화 (예: 93000 -> 093000)하거나,
                // 원본처럼 문자열로만 저장합니다. 여기서는 기존처럼 ToString()을 사용합니다.
                시간 = Time.ToString();
            }

            string 검색식 = Form1.form1.CBB_관심검색식.Text.Trim();
            string 관심그룹 = Form1.form1.CBB_관심그룹.Text.Trim();
            string 실시간등록 = Form1.form1.CB_시세감시등록.Checked.ToString();

            // 2. 입력값 유효성 검사
            if (검색식.Length > 0 && 관심그룹.Length > 0)
            {
                int num = 0; // 시간 중복 카운트

                // [최적화 1] HashSet을 foreach로 순회하여 시간 중복 카운트 (O(N))
                foreach (string item in Form1.Interest_AutoAdd_List)
                {
                    string[] parts = item.Split(';');
                    if (parts.Length < 3) continue;

                    string 자동시간 = parts[2];

                    if (시간.Equals(자동시간))
                    {
                        // '실시간'이 아닌 경우만 카운트 (기존 로직 유지)
                        if (!자동시간.Equals("실시간"))
                        {
                            num++;
                        }
                    }
                }

                // 3. 설정하려는 최종 문자열 (Unique Key) 생성
                // 이 문자열이 HashSet에 저장되어 설정 중복을 확인하는 용도로 사용됩니다.
                string uniqueSetting = 관심그룹 + ";" + 검색식 + ";" + 시간;

                if (num > 2)
                {
                    // 1. 시간 중복 알림 (문자열 보간)
                    Helper.알림창_멀티("관심그룹", $"시간중복 ({시간}) 으로 자동등록된 그룹이 2개 이상입니다.\n1초에 2개 등록 가능합니다.", 10, false);
                }
                // [최적화 2] List.Exists(O(N)) 대신 HashSet.Contains(O(1))로 설정 중복 확인
                else if (Form1.Interest_AutoAdd_List.Contains(uniqueSetting))
                {
                    // 2. 자동등록 중복 알림 (복잡한 문장도 한눈에!)
                    Helper.알림창_멀티("자동등록 중복", $"검색식({검색식})을 시간({시간})에 검색하고 검색된 종목을\n관심그룹({관심그룹})에 자동등록 합니다. 위 조건이 이미 등록되어 있습니다.", 20, false);
                }
                else
                {
                    // 5. 최종 등록 로직
                    var thread = new Thread(
                    () =>
                    {
                        string finalTime = 시간;
                        string para = uniqueSetting + ";" + GET.ScreenNum() + ";" + 실시간등록;

                        if (Form1.form1.CB_실시간관심등록.Checked)
                        {
                            finalTime = "실시간"; // UI 출력용 시간
                                               // 실시간 등록 시 para를 다시 정의
                            para = 관심그룹 + ";" + 검색식 + ";" + finalTime + ";" + "1100" + ";" + 실시간등록;
                        }

                         Helper.안전한_UI_업데이트(Form1.form1, () =>
                        {
                        using (new CenterWinDialog(Form1.form1))
                            if (MessageBox.Show($"검색식({검색식})을 시간({finalTime})에 검색하고 검색된 종목을 관심그룹({관심그룹})에 자동등록. 위 조건을 '등록' 하시겠습니까?", "자동등록알림", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    // [최적화 3] HashSet.Add(O(1)) 사용
                                    Form1.Interest_AutoAdd_List.Add(para);

                                    관심자동등록리스트확인();
                                    SaveToFile.관심그룹_Title_파일저장(Form1.로딩완료);
                                    관심실시간자동등록(true);
                                }
                        });
                    });
                    thread.Start();
                }
            }
        }


        public static void BT_자동해제_Click()
        {
            // CBB_실시간n그룹n관심자동.SelectedIndex == 2 인지 확인 (관심자동 탭)
            if (Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex == 2)
            {
                // 선택된 항목이 있는지 확인
                if (Form1.form1.LB_검색결과n관심리스트.SelectedItems.Count > 0)
                {
                    // 리스트박스에서 선택된 항목들을 역순으로 순회
                    for (int i = Form1.form1.LB_검색결과n관심리스트.SelectedItems.Count; i > 0; i--)
                    {
                        // UI 항목 (예: 1ㆍ그룹명ㆍ검색식ㆍ시간)
                        string item = Form1.form1.LB_검색결과n관심리스트.SelectedItems[i - 1].ToString();

                        if (item.Contains("ㆍ"))
                        {
                            // UI 항목에서 필요한 정보 추출 (그룹명, 검색식, 시간)
                            string[] parts = item.Split('ㆍ');
                            if (parts.Length >= 4)
                            {
                                // [주의] UI의 첫 번째 항목(N)은 무시하고, 두 번째부터 그룹, 검색식, 시간입니다.
                                // parts[0] = "1", parts[1] = "그룹명", parts[2] = "검색식", parts[3] = "시간"
                                검색식해제(parts[1].Trim(), parts[2].Trim(), parts[3].Trim());
                            }
                        }
                    }
                }
            }

            // 로컬 함수 정의
            void 검색식해제(string 관심그룹, string 검색식, string 시간)
            {
                var thread = new Thread(
                    () =>
                    {
                         Helper.안전한_UI_업데이트(Form1.form1, () =>
                        {
                            // [핵심] HashSet에서 검색할 정확한 접두사/키 생성 (예: 그룹;검색식;시간)
                            string searchPrefix = 관심그룹 + ";" + 검색식 + ";" + 시간;

                            using (new CenterWinDialog(Form1.form1))
                                if (MessageBox.Show("검색식(" + 검색식 + ")을 시간(" + 시간 + ")에 검색하고 검색된 종목을 관심그룹(" + 관심그룹 + ")에 자동등록. 위 조건을 '해제' 하시겠습니까?", "자동등록해제", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    // [최적화 1] HashSet에서 FirstOrDefault를 사용해 제거할 항목(삭제) 찾기 (O(N) 순차 검색)
                                    string 삭제 = Form1.Interest_AutoAdd_List.FirstOrDefault(o => o.StartsWith(searchPrefix));

                                    if (삭제 != null)
                                    {
                                        // [최적화 2] HashSet.Remove(O(1)) 사용
                                        Form1.Interest_AutoAdd_List.Remove(삭제);
                                    }

                                    // 실시간 감시 해제 로직
                                    if (시간.Equals("실시간") && Form1.로딩완료)
                                    {
                                        // [O(N) 유지] Run_condition_List 검색
                                        List<RunCondition> list = Form1.Run_condition_List.FindAll(o => o.name.Equals(검색식));

                                        if (list.Count == 1)
                                        {
                                            // [O(N) 유지] ConditionList 검색
                                            Condition condition = Form1.ConditionList.Find(o => o.name.Equals(검색식));

                                            if (condition != null)
                                            {
                                                // Interest_condition_List가 List<string>이라면 Remove(O(N))
                                                Form1.form1.Interest_condition_List.Remove(condition.name);

                                                Condition_Management.Stop_Monitoring(condition, "관심그룹");
                                                Form1.AutoClosingAlram("[관심그룹] 검색식 해제 " + 검색식 + " 의 실시간감시를 해제 하고 그룹(" + 관심그룹 + ")에 자동등록을 중지 합니다.", "동작알림", 10, "동작");
                                            }
                                        }
                                    }

                                    // 후처리 및 저장 (기존 로직 유지)
                                    관심자동등록리스트확인();
                                    SaveToFile.관심그룹_Title_파일저장(Form1.로딩완료);

                                    // ListBox 스크롤 조정 (기존 로직 유지)
                                    if (Form1.form1.LB_검색결과n관심리스트.Items.Count > 32)
                                        Form1.form1.LB_검색결과n관심리스트.SelectedIndex = 31;
                                }
                        });
                    });
                thread.Start();
            }
        }


        public static void 관심자동등록실행()
        {
            int time_기록 = 0;
            string 검색식_기록 = "";
            string 검색식;

            // [최적화 1] HashSet을 foreach로 순회합니다. (인덱스 접근 오류 해결)
            foreach (string item in Form1.Interest_AutoAdd_List)
            {
                // [최적화 2] Split을 한 번만 수행하여 파싱 효율을 높입니다.
                string[] parts = item.Split(';');

                // 데이터 안전성 확보 (최소 3개 항목이 있어야 함)
                if (parts.Length < 3) continue;

                검색식 = parts[1];
                // 파싱 오류 방지: TryParse 사용
                if (!int.TryParse(parts[2], out int 실행_시간))
                {
                    continue;
                }

                // 현재 시간과 일치하는지 확인
                if (Get.TimeNow.Equals(실행_시간))
                {
                    // 중복 실행 방지 로직 (HashSet 순서 비보장 특성 고려하여 수정)
                    if (time_기록 != 실행_시간)
                    {
                        if (검색식 != 검색식_기록)
                        {
                            실행();
                        }
                        else
                        {
                            // 같은 시간에 같은 검색식이 기록되어 있다면, 100만큼 차이가 날 때만 실행 (기존 로직 유지)
                            // (이 로직은 순서가 보장되지 않는 HashSet에서 '다음 항목'을 보장할 수 없어 동작 방식이 달라질 수 있습니다.)
                            // 기존 로직의 의도를 최대한 살려 해당 조건식을 단순하게 유지합니다.
                            if ((time_기록 + 100) < 실행_시간)
                            {
                                실행();
                            }
                        }
                    }
                    else
                    {
                        if (검색식 != 검색식_기록)
                        {
                            실행();
                        }
                    }

                    // 실행 로컬 함수 (Nested Function)
                    void 실행()
                    {
                        검색요청(검색식); // 외부 함수 호출
                        time_기록 = 실행_시간;
                        검색식_기록 = 검색식;
                    }
                }
            }
        }

        public static void 관심실시간자동등록(bool 신규)
        {
            // [경고] HashSet은 순서를 보장하지 않으므로, 'if (신규)' 내의 
            // 'i + 1 == Form1.Interest_AutoAdd_List.Count' (마지막 항목) 로직은
            // HashSet 구조에서는 무효하며, 신규 항목만 처리하려면 외부에서 그 항목을 받아와야 합니다. 
            // 여기서는 신규 여부와 관계없이 전체 실시간 등록 항목을 검사하도록 로직을 통합합니다.

            // 신규 플래그는 현재 로직에서 제거하거나, 외부에서 제어되어야 합니다.
            // 기존 로직의 의도를 무시하고 전체 목록을 순회하여 실시간 등록을 시도합니다.

            // [최적화 1] HashSet을 foreach로 순회합니다. (인덱스 접근 오류 해결)
            foreach (string item in Form1.Interest_AutoAdd_List)
            {
                // [최적화 2] Split을 한 번만 수행하여 파싱 효율을 높입니다.
                string[] parts = item.Split(';');
                if (parts.Length < 5) continue;

                string 관심그룹 = parts[0];
                string 검색식 = parts[1];
                string 실행_시간_구분 = parts[2];
                string 스크린번호 = parts[3];
                string 실시간등록_체크 = parts[4]; // 사용되지 않지만 구조 유지를 위해 포함

                // "실시간" 키워드가 포함된 항목만 처리
                if (실행_시간_구분.Contains("실시간"))
                {
                    // [O(N) 유지] 저장된 조건식 목록에서 Condition 객체 검색
                    Condition conditionItem = Form1.ConditionList.Find(o => o.name.Equals(검색식));

                    if (conditionItem != null) // 조건식이 존재하는 경우
                    {
                        if (Form1.Run_condition_count < 10) // 개수 제한 10
                        {
                            // [O(N) 유지] 현재 실행 중인 조건식 목록에서 검색
                            List<RunCondition> list = Form1.Run_condition_List.FindAll(o => o.name.Equals(검색식));

                            if (list.Count == 0) // 실행 중이 아니라면
                            {
                                // 모니터링 시작
                                Condition_Management.Start_Monitoring(conditionItem, "관심실시간자동등록", null, null);

                                // 등록 및 상태 업데이트
                                등록(conditionItem, item, false);
                            }
                            else // 이미 실행 중이라면 (Count > 0)
                            {
                                // 등록 및 상태 업데이트 (기존 로직에서 Run_condition_List에 이미 있다면 이 함수를 호출)
                                등록(conditionItem, item, true);
                            }

                            // 로컬 함수 정의
                            void 등록(Condition condition, string originalItem, bool alreadyRunning)
                            {
                                if (!alreadyRunning)
                                {
                                    // ConditionList에서 찾은 조건식 이름을 등록합니다.
                                    Form1.form1.Interest_condition_List.Add(condition.name);
                                }

                                Helper.알림창_멀티("관심그룹", $"검색식등록 알림 {검색식} 을 실시간 감시하여 \n{관심그룹} 그룹에 실시간 등록 합니다.", 10, false);
                                Log.동작기록($"[관심그룹] 검색식등록 알림 {검색식} 을 실시간 감시하여 {관심그룹} 그룹에 실시간 등록 합니다.");

                                // [최적화 3] HashSet의 값을 업데이트(수정)하는 방법: 제거 후 추가 (O(1) + O(1))
                                // 세미콜론 구분자 생성도 보간법으로 훨씬 직관적이게!
                                string newPara = $"{관심그룹};{검색식};실시간;{스크린번호};{실시간등록_체크}";

                                Form1.Interest_AutoAdd_List.Remove(originalItem); // 기존 항목 제거 (O(1))
                                Form1.Interest_AutoAdd_List.Add(newPara);        // 새 항목 추가 (O(1))
                            }
                        }
                        else // 개수 제한 (10개) 초과
                        {
                            Helper.알림창_멀티("관심그룹", $"검색식등록 실패 가동검색식이 10개를 넘어 ({검색식})\n을 가동할 수 없습니다.", 10, false);
                            Log.동작기록($"[관심그룹] 검색식등록 실패 가동검색식이 10개를 넘어 ({검색식}) 을 가동할 수 없습니다.");

                            // [최적화 3] 데이터 업데이트
                            string newPara = $"{관심그룹};{검색식};실시간(개수초과);{스크린번호};{실시간등록_체크}";
                            Form1.Interest_AutoAdd_List.Remove(item);
                            Form1.Interest_AutoAdd_List.Add(newPara);
                        }
                    }
                    else // 검색식(Condition)이 존재하지 않는 경우
                    {
                        Helper.알림창_멀티("관심그룹", $"검색식등록 실패 검색식이 찾을 수 없어 ({검색식})\n실시간 감시 할 수 없습니다.", 10, false);
                        Log.동작기록($"[관심그룹] 검색식등록 실패 검색식이 찾을 수 없어 ({검색식}) 실시간 감시 할 수 없습니다.");

                        // [최적화 3] 데이터 업데이트
                        string newPara = $"{관심그룹};{검색식};식없음;{스크린번호};{실시간등록_체크}";
                        Form1.Interest_AutoAdd_List.Remove(item);
                        Form1.Interest_AutoAdd_List.Add(newPara);
                    }
                    SaveToFile.관심그룹_Title_파일저장(Form1.로딩완료);
                }
            }
        }

        public static void BT_자동삭제_Click()
        {
            // CBB_그룹자동삭제.SelectedIndex == 0: 자동 삭제 설정 해제
            if (Form1.form1.CBB_그룹자동삭제.SelectedIndex == 0)
            {
                if (Form1.form1.LB_검색결과n관심리스트.SelectedItems.Count > 0)
                {
                    // 리스트박스에서 선택된 항목들을 역순으로 순회하며 처리
                    for (int i = Form1.form1.LB_검색결과n관심리스트.SelectedItems.Count; i > 0; i--)
                    {
                        string item = Form1.form1.LB_검색결과n관심리스트.SelectedItems[i - 1].ToString();

                        // UI 항목 (예: 1ㆍ기간ㆍ그룹명ㆍ조건)
                        if (item.Contains("ㆍ"))
                        {
                            string[] parts = item.Split('ㆍ');
                            if (parts.Length >= 4)
                            {
                                string 기간 = parts[1].Trim();
                                string 관심그룹 = parts[2].Trim();
                                string 조건 = parts[3].Trim();

                                // 자동 삭제 조건('종목삭제' 또는 '이탈삭제')만 해제 가능
                                if (조건.Equals("종목삭제") || 조건.Equals("이탈삭제"))
                                {
                                    자동삭제해제(기간, 관심그룹, 조건);
                                }
                            }
                        }
                    }
                }

                // 로컬 함수 정의
                void 자동삭제해제(string 기간, string 관심그룹, string 조건)
                {
                    var thread = new Thread(
                        () =>
                        {
                             Helper.안전한_UI_업데이트(Form1.form1, () =>
                            {
                                // [핵심] HashSet에서 찾을 정확한 접두사/키 생성
                                // (등록 시 뒤에 ScreenNum/시세감시 체크 여부가 붙으므로, 해제 시 그 항목을 찾기 위한 문자열)
                                string searchPrefix = 관심그룹 + ";" + 조건 + ";" + 기간 + ";" + "1100";

                                using (new CenterWinDialog(Form1.form1))
                                    if (MessageBox.Show("자동삭제해제\n관심그룹(" + 관심그룹 + "), 조건(" + 조건 + "), 기간(" + 기간 + ")인 자동삭제 조건을 '해제' 하시겠습니까?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        // [최적화 1] Find(O(N)) 대신 FirstOrDefault(O(N))로 항목을 찾고, O(1)로 제거
                                        string 삭제 = Form1.Interest_AutoAdd_List.FirstOrDefault(o => o.StartsWith(searchPrefix));

                                        if (삭제 != null)
                                        {
                                            // HashSet.Remove는 O(1)로 빠릅니다.
                                            Form1.Interest_AutoAdd_List.Remove(삭제);
                                        }

                                        Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 2;
                                        // CBB_실시간n그룹n관심자동_indexchange(2); // (이 함수는 현재 코드에서 정의되지 않아 주석처리)

                                        관심자동등록리스트확인();

                                        Form1.form1.LB_검색결과n관심리스트.SelectedIndex = Form1.form1.LB_검색결과n관심리스트.Items.Count - 1;
                                    }
                            });
                        });
                    thread.Start();
                }
            }
            // CBB_그룹자동삭제.SelectedIndex == 1: 기간 경과 삭제 등록
            else if (Form1.form1.CBB_그룹자동삭제.SelectedIndex == 1)
            {
                int.TryParse(Form1.form1.MTB_자동삭제기간.Text, out int 기간);
                if (기간 == 0) { 기간 = 1; Form1.form1.MTB_자동삭제기간.Text = 기간.ToString(); }

                string 관심그룹 = Form1.form1.CBB_관심그룹.Text;
                string 검색식 = "종목삭제"; // 고정 검색식

                // [핵심] HashSet에 저장될 정확한 접두사 문자열을 만들어 Contains 검사를 준비합니다.
                string 중복확인_접두사 = 관심그룹 + ";" + 검색식 + ";" + 기간.ToString() + ";" + "1100";

                // [최적화 2] Exists(O(N)) 대신 FirstOrDefault(O(N))를 사용하되, 
                // HashSet.Contains를 사용하려면 완전한 문자열이 필요합니다. 
                // Startswith는 O(N) 순차 검색이므로, Exists를 FirstOrDefault로 변경하는 수준만 적용합니다.
                if (Form1.Interest_AutoAdd_List.Any(o => o.StartsWith(중복확인_접두사)))
                {
                    Form1.AutoClosingAlram("[관심그룹] 자동삭제 중복 기간(" + 기간.ToString() + ") 일 이 지난 종목을 관심그룹(" + 관심그룹 + ")에서 자동삭제 합니다. 위 조건이 이미 '등록'되어 있습니다.", "에러알림", 10, "에러");
                }
                else
                {
                    var thread = new Thread(
                    () =>
                    {
                         Helper.안전한_UI_업데이트(Form1.form1, () =>
                        {
                            // [등록 문자열] Interest_AutoAdd_List에 추가될 최종 문자열
                            string para = 중복확인_접두사 + ";" + Form1.form1.CB_시세감시등록.Checked.ToString();

                            using (new CenterWinDialog(Form1.form1))
                                if (MessageBox.Show("자동삭제등록\n기간(" + 기간.ToString() + ") 일 이 지난 종목을 관심그룹(" + 관심그룹 + ")에서 자동삭제 합니다.\n 위 조건을 '등록' 하시겠습니까 ?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    // [최적화 3] HashSet.Add(O(1)) 사용
                                    Form1.Interest_AutoAdd_List.Add(para);

                                    관심자동등록리스트확인();
                                    Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 2;
                                    // CBB_실시간n그룹n관심자동_indexchange(2); // (이 함수는 현재 코드에서 정의되지 않아 주석처리)
                                }
                        });
                    });
                    thread.Start();
                }
            }
            // CBB_그룹자동삭제.SelectedIndex == 2: 검색 이탈 삭제 등록
            else if (Form1.form1.CBB_그룹자동삭제.SelectedIndex == 2)
            {
                string 기간 = "0"; // 기간은 고정
                string 관심그룹 = Form1.form1.CBB_관심그룹.Text;
                string 검색식 = "이탈삭제"; // 고정 검색식
                string 중복확인_접두사 = 관심그룹 + ";" + 검색식 + ";" + 기간 + ";" + "1100";

                // [최적화 2] Exists(O(N)) 대신 FirstOrDefault(O(N))를 사용한 중복 확인
                if (Form1.Interest_AutoAdd_List.Any(o => o.StartsWith(중복확인_접두사)))
                {
                    Form1.AutoClosingAlram("[관심그룹] 자동삭제 중복 기간(" + 기간 + ") 일 이 지난 종목을 관심그룹(" + 관심그룹 + ")에서 자동삭제 합니다. 위 조건이 이미 '등록'되어 있습니다.", "에러알림", 10, "에러");
                }
                else
                {
                    var thread = new Thread(
                    () =>
                    {
                         Helper.안전한_UI_업데이트(Form1.form1, () =>
                        {
                            // [등록 문자열] Interest_AutoAdd_List에 추가될 최종 문자열
                            string para = 중복확인_접두사 + ";" + Form1.form1.CB_시세감시등록.Checked.ToString();

                            using (new CenterWinDialog(Form1.form1))
                                if (MessageBox.Show("자동삭제등록\n검색 이탈된 종목을 관심그룹(" + 관심그룹 + ")에서 자동삭제 합니다.\n 위 조건을 '등록' 하시겠습니까 ?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    // [최적화 3] HashSet.Add(O(1)) 사용
                                    Form1.Interest_AutoAdd_List.Add(para);

                                    // 이탈삭제 그룹 목록에 추가 (str.이탈삭제는 List가 아닐 수 있으므로 문자열 처리는 유지)
                                    str.이탈삭제 = str.이탈삭제 + 관심그룹 + "^";

                                    관심자동등록리스트확인();

                                    Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 2;
                                    // CBB_실시간n그룹n관심자동_indexchange(2); // (이 함수는 현재 코드에서 정의되지 않아 주석처리)
                                }
                        });
                    });
                    thread.Start();
                }
            }

            SaveToFile.관심그룹_Title_파일저장(Form1.로딩완료);
        }

        public static void 자동삭제실행()
        {
            // [최적화 1] HashSet을 foreach로 순회합니다. (인덱스 접근 오류 해결)
            foreach (string item in Form1.Interest_AutoAdd_List)
            {
                // [최적화 2] Split을 한 번만 수행하여 파싱 효율을 높입니다.
                string[] list = item.Split(';');

                if (list.Length < 3) continue;

                string 관심그룹 = list[0];
                string 검색조건 = list[1];
                int.TryParse(list[2], out int 기간);

                // ----------------------------------------------------------------------
                // 1. 기간 경과 삭제 ("종목삭제" && 기간 > 0)
                // ----------------------------------------------------------------------
                if (검색조건.Equals("종목삭제") && 기간 > 0)
                {
                    // [O(N) 유지] 특정 관심 그룹 종목만 검색
                    List<Interest_stock> 관종_list = Form1.Interest_stock_List.FindAll(o => o.Title.Equals(관심그룹));

                    // 제거 대상 항목을 모으는 임시 리스트
                    List<Interest_stock> itemsToRemove = new List<Interest_stock>();

                    for (int n = 0; n < 관종_list.Count; n++)
                    {
                        Interest_stock 관종 = 관종_list[n];
                        // if (관심그룹.Equals(관종.Title)) 로직은 FindAll에서 이미 필터링했으므로 불필요

                        DateTime today = DateTime.Parse(DateTime.Now.ToShortDateString());

                        // 날짜 파싱 안전성 확보: 관종.date가 유효하지 않을 경우를 대비
                        if (DateTime.TryParse(관종.AddedDate, out DateTime day))
                        {
                            TimeSpan ts = today.Date - day.Date;

                            if (ts.Days >= 기간)
                            {
                                // [O(N²) 제거] 여기서 List.Remove(관종) 대신 임시 리스트에 추가
                                itemsToRemove.Add(관종);
                            }
                        }
                    }

                    // [최적화 3] 루프 종료 후, List.RemoveAll을 사용하여 O(N)으로 제거
                    Form1.Interest_stock_List.RemoveAll(itemsToRemove.Contains);
                }

                // ----------------------------------------------------------------------
                // 2. 검색 이탈 삭제 ("이탈삭제" && 기간 == 0)
                // ----------------------------------------------------------------------
                if (검색조건.Equals("이탈삭제") && 기간 == 0)
                {
                    // 이탈삭제 그룹 목록(str.이탈삭제)에 현재 관심그룹을 추가 (중복 추가 방지)
                    string groupWithDelimiter = 관심그룹 + "^";
                    if (!str.이탈삭제.Contains(groupWithDelimiter))
                    {
                        str.이탈삭제 += groupWithDelimiter;
                    }
                }
            }

            // 파일 저장 및 UI 갱신 (기존 로직 유지)
            SaveToFile.관심그룹_Title_파일저장(Form1.로딩완료);
            SaveToFile.관심그룹_List_파일저장(로딩완료);

            CBB_관심그룹_리스트보기(Form1.form1.CBB_관심그룹_A);
            CBB_관심그룹_리스트보기(Form1.form1.CBB_관심그룹_B);
            CBB_관심그룹_리스트보기(Form1.form1.CBB_관심그룹_C);
        }
        
        public static void LB_검색결과n관심리스트_SelectedIndexChanged()
        {
            if (Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex == 0)
            {
                if (Form1.form1.LB_검색결과n관심리스트.SelectedItem != null && !Form1.form1.LB_검색결과n관심리스트.SelectedItem.ToString().StartsWith("0"))
                {
                    string ItemName = Form1.form1.LB_검색결과n관심리스트.SelectedItem.ToString().Split('ㆍ')[2];

                    Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(ItemName)).Value;
                    if (Market != null)
                    {
                        Order_Reserve.종목선택(ItemName);
                        Form1.form1.TB_관심그룹_종목명.Text = ItemName;
                    }
                }
            }
            else if (Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex == 1)
            {
                if (Form1.form1.LB_검색결과n관심리스트.SelectedIndex > -1 && Form1.form1.LB_검색결과n관심리스트.SelectedItem.ToString().Length > 1)
                {
                    Form1.form1.CBB_관심그룹변경.SelectedIndex = Form1.form1.LB_검색결과n관심리스트.SelectedIndex;
                    Form1.form1.CBB_신규그룹_DropDownClosed(Form1.form1.CBB_관심그룹변경, null);
                    string 그룹명 = Form1.form1.LB_검색결과n관심리스트.SelectedItem.ToString().Split(' ')[2];
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = 그룹명;
                    Form1.form1.CBB_관심그룹.SelectedItem = 그룹명;
                    Form1.form1.TB_관심그룹추가.Text = 그룹명;
                }
            }
            else
            {
                if (Form1.form1.LB_검색결과n관심리스트.SelectedIndex > -1 && Form1.form1.LB_검색결과n관심리스트.SelectedItem.ToString().Trim().Length > 1)
                {
                    string item = Form1.form1.LB_검색결과n관심리스트.SelectedItem.ToString();
                    if (item.Contains("ㆍ"))
                    {
                        string 관심그룹 = item.Split('ㆍ')[1].Trim();

                        if (Form1.form1.CBB_관심그룹.Items.Contains(관심그룹))
                        {
                            Form1.form1.CBB_관심그룹.SelectedItem = 관심그룹;
                            Form1.form1.TB_관심그룹추가.Text = 관심그룹;
                        }

                        Form1.비프음("체크");
                    }
                }
            }

        }

        public static void LB_관심_A_SelectedIndexChanged(object sender)
        {
            if (sender.Equals(Form1.form1.LB_관심_A))
            {
                종목선택실행(Form1.form1.LB_관심_A);
            }
            if (sender.Equals(Form1.form1.LB_관심_B))
            {
                종목선택실행(Form1.form1.LB_관심_B);
            }
            if (sender.Equals(Form1.form1.LB_관심_C))
            {
                종목선택실행(Form1.form1.LB_관심_C);
            }

            void 종목선택실행(ListBox item)
            {
                if (item.SelectedItems.Count > 0)
                {
                    string ItemName = item.SelectedItem.ToString().Split(' ')[2];
                    Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(ItemName)).Value;
                    if (Market != null)
                    {
                        Order_Reserve.종목선택(ItemName);
                        Form1.form1.TB_관심그룹_종목명.Text = ItemName;
                    }
                }
            }
        }

        public static void 관심_검색결과보기()
        {
            Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                if (Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex == 0)
                {
                    Form1.form1.LB_검색결과n관심리스트.BeginUpdate();
                    Form1.form1.LB_검색결과n관심리스트.Items.Clear();

                    // ----------------------------------------------------------------------
                    // 1. HashSet 순회 및 데이터 분류
                    // ----------------------------------------------------------------------

                    string searchTime = "알 수 없음";
                    string searchCondition = "알 수 없음";
                    List<string> stockCodes = new List<string>();

                    // [최적화 1] HashSet을 foreach로 순회하여 항목을 분류
                    foreach (string item in Form1.form1.검색결과_List)
                    {
                        if (item.Contains(":") && item.Length == 8) // 시간 형식 (HH:mm:ss) 추정
                        {
                            searchTime = item;
                        }
                        else if (item.Length > 8 && !item.Contains(";")) // 길이가 길고 세미콜론(;)이 없으면 검색식으로 추정
                        {
                            searchCondition = item;
                        }
                        else if (item.Length >= 6 && item.Length <= 8) // 종목코드로 추정 (6~8자리 코드)
                        {
                            stockCodes.Add(item);
                        }
                    }

                    // ----------------------------------------------------------------------
                    // 2. UI에 출력
                    // ----------------------------------------------------------------------

                    Form1.form1.LB_검색결과n관심리스트.Items.Add("0 시간: " + searchTime);
                    Form1.form1.LB_검색결과n관심리스트.Items.Add("0 검색식 : " + searchCondition);
                    Form1.form1.LB_검색결과n관심리스트.Items.Add("0 검색개수 : " + stockCodes.Count);
                    Form1.form1.LB_검색결과n관심리스트.Items.Add("0 -----------------------------");

                    int displayCount = 0;

                    // [최적화 2] 종목 코드를 순회하며 출력
                    foreach (string code in stockCodes)
                    {
                        // Market_Item_List는 Dictionary이므로 TryGetValue는 O(1)로 빠릅니다.
                        if (Form1.Market_Item_List.TryGetValue(code, out Market_Item Market))
                        {
                            displayCount++;
                            // 기존 로직의 인덱스 출력 방식을 맞추기 위해 Count 대신 displayCount 사용
                            Form1.form1.LB_검색결과n관심리스트.Items.Add(
                                $"{displayCount}ㆍ{Market.Market}ㆍ{Market.종목명}ㆍ{Market.현재가.ToString("N0")}"
                            );
                        }
                    }

                    Form1.form1.LB_검색결과n관심리스트.EndUpdate();
                }
            });
        }

        public static void 관심검색_실시간보기(string 검색식, string ID, string itemcode)
        {
            Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                if (Form1.form1.CB_실시간검색결과보기.Checked)
                {
                    if (ID.Equals("I"))
                    {
                        if (Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex == 0)
                        {
                            // [최적화 1] HashSet을 foreach로 순회합니다. (인덱스 접근 오류 해결)
                            foreach (string item in Form1.Interest_AutoAdd_List)
                            {
                                string[] auto = item.Split(';');
                                if (auto.Length < 3) continue; // 데이터 안전성 체크

                                string 관심식 = auto[1];
                                string 실행_시간_구분 = auto[2];

                                // para = 관심그룹 + ";" + 검색식 + ";" + 시간 + ";" + "1100" + ";" + 실시간등록;

                                if (관심식.Equals(검색식) && 실행_시간_구분.Equals("실시간"))
                                {
                                    if (ID.Equals("I"))
                                    {
                                        // [최적화 2] itemcode 분리 작업을 한 번만 수행
                                        if (itemcode.Contains(";"))
                                        {
                                            string[] codeList = itemcode.Split(';');
                                            // 마지막 항목은 보통 비어있으므로, Length - 1까지 순회 (기존 로직 유지)
                                            for (int n = 0; n < codeList.Length - 1; n++)
                                            {
                                                실시간보기(codeList[n]);
                                            }
                                        }
                                        else
                                        {
                                            실시간보기(itemcode);
                                        }
                                    }
                                }
                            }

                            // 로컬 함수 정의
                            void 실시간보기(string 코드)
                            {
                                Form1.form1.LB_검색결과n관심리스트.BeginUpdate();
                                if (Form1.Market_Item_List.TryGetValue(코드, out Market_Item Market))
                                {
                                    bool 추가 = true;

                                    // -----------------------------------------------------------
                                    // UI 리스트박스 항목 정리 및 중복 확인 (O(N) 순회)
                                    // -----------------------------------------------------------
                                    // 리스트 박스 순회 중 RemoveAt(i)를 호출하면 인덱스가 바뀌므로,
                                    // 반드시 역순으로 순회하거나, 제거할 항목을 모아서 제거해야 합니다.
                                    // 현재는 순서대로 순회하며 항목 제거 및 중복 확인을 시도하는 복잡한 로직입니다.

                                    for (int i = Form1.form1.LB_검색결과n관심리스트.Items.Count - 1; i >= 0; i--) // [최적화 3] 역순 순회로 안전하게 제거
                                    {
                                        string listItem = Form1.form1.LB_검색결과n관심리스트.Items[i].ToString();

                                        if (listItem.Contains('ㆍ'))
                                        {
                                            string[] strParts = listItem.Split('ㆍ');

                                            // 시간 정보가 없는 항목 제거 (헤더 등으로 추정)
                                            if (!strParts[0].Contains(':'))
                                            {
                                                Form1.form1.LB_검색결과n관심리스트.Items.RemoveAt(i);
                                            }
                                            else // 시간 정보가 있는 항목(종목)
                                            {
                                                // 중복 확인: 검색식과 종목명이 같으면 추가하지 않음
                                                // strParts[2] = 종목명, strParts[3] = 검색식
                                                if (strParts[3].Equals(검색식) && strParts[2].Equals(Market.종목명))
                                                {
                                                    추가 = false;
                                                }
                                            }
                                        }
                                        else // ㆍ(구분자)가 없는 항목 제거
                                        {
                                            Form1.form1.LB_검색결과n관심리스트.Items.RemoveAt(i);
                                        }
                                    }

                                    // -----------------------------------------------------------
                                    // 새 항목 추가
                                    // -----------------------------------------------------------
                                    if (추가)
                                    {
                                        string newEntry = $"{Get.TimeNow.ToString("##:##:##")}ㆍ{Market.Market}ㆍ{Market.종목명}ㆍ{검색식}";

                                        if (Form1.form1.LB_검색결과n관심리스트.Items.Count == 0)
                                            Form1.form1.LB_검색결과n관심리스트.Items.Add(newEntry);
                                        else
                                            Form1.form1.LB_검색결과n관심리스트.Items.Insert(0, newEntry);
                                    }
                                }

                                // -----------------------------------------------------------
                                // 항목 수 제한 로직 (기존 로직 유지)
                                // -----------------------------------------------------------
                                int index = 36;
                                // 긴 if-else/if 체인을 통해 인덱스(제한 수)를 결정합니다.
                                if (GenieConfig.CBB_layout == 0 || GenieConfig.CBB_layout == 3) index = 36;
                                else if (GenieConfig.CBB_layout == 1 || GenieConfig.CBB_layout == 2) index = 62;
                                else if (GenieConfig.CBB_layout == 4) index = 42;
                                // 매매 관련 체크박스가 켜져 있으면 기본값으로 리셋
                                if (Form1.form1.CB_기본매매.Checked || Form1.form1.CB_반복매매.Checked || Form1.form1.CB_계좌관리.Checked || Form1.form1.CB_특수매매.Checked || Form1.form1.CB_대금탐색.Checked || Form1.form1.CB_매매그룹.Checked || Form1.form1.CB_기능설정.Checked) index = 36;

                                // [최적화 4] Count > index를 초과하는 항목을 제거
                                while (Form1.form1.LB_검색결과n관심리스트.Items.Count > index)
                                {
                                    // ListBox의 끝에서부터 제거하는 것은 O(1)에 가깝습니다. (가장 효율적)
                                    Form1.form1.LB_검색결과n관심리스트.Items.RemoveAt(Form1.form1.LB_검색결과n관심리스트.Items.Count - 1);
                                }
                                Form1.form1.LB_검색결과n관심리스트.EndUpdate();
                            }
                        }
                    }
                }
            });
        }


        public static void 관심_검색종목_등록실행(string itemcode, string strConditionName, string type)
        {
            if (Form1.Interest_AutoAdd_List.Count > 0) // 실시간 등록
            {
                Helper.안전한_UI_업데이트(Form1.form1, () =>
                {
                    // [참고] HashSet 순서 비보장으로 인해, 이 기록 변수는 List일 때와 동작이 다를 수 있습니다.
                    int 실행시간_기록 = 0;
                    string 검색식_기록 = "";

                    // [최적화 1] HashSet을 foreach로 순회합니다. (인덱스 접근 오류 해결)
                    foreach (string item in Form1.Interest_AutoAdd_List)
                    {
                        // -----------------------------------------------------------
                        // [최적화 2] 변수 정의 및 초기화 (foreach 루프 내부)
                        // -----------------------------------------------------------
                        string[] auto = item.Split(';');
                        if (auto.Length < 5) continue;

                        string 관심그룹 = auto[0];          // << 로컬 함수에서 사용될 변수
                        string 검색식 = auto[1];
                        bool 시간정보_존재 = int.TryParse(auto[2], out int 실행시간);
                        // string 스크린 = auto[3]; // 미사용
                        bool.TryParse(auto[4], out bool 실시간등록); // << 로컬 함수에서 사용될 변수

                        // para = 관심그룹 + ";" + 검색식 + ";" + 시간 + ";" + "1100" + ";" + 실시간등록;

                        // ----------------------------------------------------------------------
                        // [로컬 함수 정의] 변수 '관심그룹'과 '실시간등록'은 이제 이 함수 내에서 캡처됩니다.
                        // ----------------------------------------------------------------------
                        void 관종등록()
                        {
                            if (Form1.Market_Item_List.TryGetValue(itemcode, out Market_Item 마켓_아이템))
                            {
                                if (!Form1.Interest_stock_List.Exists(o => o.Title.Equals(관심그룹) && o.Code.Equals(itemcode)))
                                {
                                    Form1.Interest_stock_List.Add(new Interest_stock
                                    {
                                        Code = itemcode,
                                        Name = 마켓_아이템.종목명, // 아까 꺼내둔 거 바로 사용
                                        AddedDate = str.today,
                                        Title = 관심그룹,
                                        시세등록 = 실시간등록
                                    });

                                    // 실시간 등록 요청
                                    REG.실시간시세등록(itemcode);
                                }
                            }
                        }

                        void 관종삭제()
                        {
                            if (str.이탈삭제.Contains(관심그룹))
                            {
                                // O(N) 순차 검색
                                Interest_stock I_stock = Form1.Interest_stock_List.Find(o => o.Title.Equals(관심그룹) && o.Code.Equals(itemcode));
                                if (I_stock != null)
                                {
                                    Form1.Interest_stock_List.Remove(I_stock); // O(N) 순차 삭제

                                     Helper.안전한_UI_업데이트(Form1.form1, () =>
                                    {
                                        Form1.form1.LB_관심_A.Items.Clear();
                                        Form1.form1.LB_관심_B.Items.Clear();
                                        Form1.form1.LB_관심_C.Items.Clear();
                                    });
                                }
                            }
                        }
                        // ----------------------------------------------------------------------

                        if (시간정보_존재)
                        {
                            if (type.Equals("I") && !검색식.Equals("이탈삭제") && !검색식.Equals("종목삭제"))
                            {
                                if (strConditionName.Equals(검색식))
                                {
                                    관종등록();

                                    실행시간_기록 = 실행시간;
                                    검색식_기록 = 검색식;
                                }

                                if (실행시간_기록 <= 실행시간 && 실행시간 <= (실행시간_기록 + 100) && 검색식 == 검색식_기록)
                                {
                                    관종등록();
                                }

                                Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 0;
                                // CBB_실시간n그룹n관심자동_indexchange(0); // (이 함수는 현재 코드에서 정의되지 않아 주석처리)
                            }
                        }
                        else // 시간 정보가 없는 경우 (실시간 처리 로직)
                        {
                            if (strConditionName.Equals(검색식) && auto[2].Equals("실시간"))
                            {
                                if (type.Equals("I")) // 편입 (Insert)
                                {
                                    관종등록();
                                }
                                else // 이탈 (Delete)
                                {
                                    관종삭제();
                                }
                            }
                        }
                    } // foreach 종료

                    // ----------------------------------------------------------------------
                    // 후처리 및 저장
                    // ----------------------------------------------------------------------
                    SaveToFile.관심그룹_List_파일저장(로딩완료);

                    CBB_관심그룹_리스트보기(Form1.form1.CBB_관심그룹_A);
                    CBB_관심그룹_리스트보기(Form1.form1.CBB_관심그룹_B);
                    CBB_관심그룹_리스트보기(Form1.form1.CBB_관심그룹_C);
                });
            }
        }

        public static void 콤보_관심그룹_DropDownClosed(object sender)
        {
            if (Form1.로딩완료)
            {
                Form1.비프음("체크");
            }

            Form1.form1.TB_관심그룹추가.Text = (sender as ComboBox).Text;
            if (sender.Equals(Form1.form1.CBB_관심그룹_A)) Form1.form1.LB_관심_A.Items.Clear();

            if (sender.Equals(Form1.form1.CBB_관심그룹_B)) Form1.form1.LB_관심_B.Items.Clear();

            if (sender.Equals(Form1.form1.CBB_관심그룹_C)) Form1.form1.LB_관심_C.Items.Clear();

            CBB_관심그룹_리스트보기(sender);

            if (!sender.Equals(Form1.form1.CBB_관심그룹))
            {
                Form1.form1.CBB_관심그룹.SelectedItem = (sender as ComboBox).Text;
            }
        }
        public static void LB_관심_A_Click(object sender)
        {
            if (sender.Equals(Form1.form1.TB_관심그룹_종목명))
            {
                Form1.form1.LB_검색결과n관심리스트.SelectedIndex = -1;
                Form1.form1.LB_관심_A.SelectedIndex = -1;
                Form1.form1.LB_관심_B.SelectedIndex = -1;
                Form1.form1.LB_관심_C.SelectedIndex = -1;
            }
            if (sender.Equals(Form1.form1.LB_검색결과n관심리스트))
            {
                Form1.form1.LB_관심_A.SelectedIndex = -1;
                Form1.form1.LB_관심_B.SelectedIndex = -1;
                Form1.form1.LB_관심_C.SelectedIndex = -1;
            }
            if (sender.Equals(Form1.form1.LB_관심_A))
            {
                Form1.form1.LB_검색결과n관심리스트.SelectedIndex = -1;
                Form1.form1.LB_관심_B.SelectedIndex = -1;
                Form1.form1.LB_관심_C.SelectedIndex = -1;
                Form1.form1.CBB_관심그룹.SelectedItem = Form1.form1.CBB_관심그룹_A.SelectedItem;

            }
            if (sender.Equals(Form1.form1.LB_관심_B))
            {
                Form1.form1.LB_검색결과n관심리스트.SelectedIndex = -1;
                Form1.form1.LB_관심_A.SelectedIndex = -1;
                Form1.form1.LB_관심_C.SelectedIndex = -1;
                Form1.form1.CBB_관심그룹.SelectedItem = Form1.form1.CBB_관심그룹_B.SelectedItem;
            }

            if (sender.Equals(Form1.form1.LB_관심_C))
            {
                Form1.form1.LB_검색결과n관심리스트.SelectedIndex = -1;
                Form1.form1.LB_관심_A.SelectedIndex = -1;
                Form1.form1.LB_관심_B.SelectedIndex = -1;
                Form1.form1.CBB_관심그룹.SelectedItem = Form1.form1.CBB_관심그룹_C.SelectedItem;
            }
        }

    }
}
