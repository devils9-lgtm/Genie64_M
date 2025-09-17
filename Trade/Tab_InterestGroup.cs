using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니_64
{
    class Tab_InterestGroup
    {
        public static string 그룹변경(string item, string title)
        {
            string 위치 = "";

            switch (item)
            {
                case "신규_A":
                    Form1.Acc[0].신규_A = title;
                    break;
                case "신규_B":
                    Form1.Acc[0].신규_B = title;
                    break;
                case "신규_C":
                    Form1.Acc[0].신규_C = title;
                    break;
                case "반복_A":
                    Form1.Acc[0].반복_A = title;
                    break;
                case "반복_B":
                    Form1.Acc[0].반복_B = title;
                    break;
                case "반복_C":
                    Form1.Acc[0].반복_C = title;
                    break;
                case "반복_D":
                    Form1.Acc[0].반복_D = title;
                    break;
                case "반복_E":
                    Form1.Acc[0].반복_E = title;
                    break;
                case "반복_F":
                    Form1.Acc[0].반복_F = title;
                    break;
                case "반복_G":
                    Form1.Acc[0].반복_G = title;
                    break;
                case "반복_H":
                    Form1.Acc[0].반복_H = title;
                    break;
                case "반복_I":
                    Form1.Acc[0].반복_I = title;
                    break;
                case "반복_J":
                    Form1.Acc[0].반복_J = title;
                    break;
                case "반복_K":
                    Form1.Acc[0].반복_K = title;
                    break;
                case "반복_L":
                    Form1.Acc[0].반복_L = title;
                    break;
                case "반복_M":
                    Form1.Acc[0].반복_M = title;
                    break;
                case "반복_N":
                    Form1.Acc[0].반복_N = title;
                    break;
                case "리밸_A":
                    Form1.Acc[0].리밸_A = title;
                    break;
                case "리밸_B":
                    Form1.Acc[0].리밸_B = title;
                    break;
                case "리밸_C":
                    Form1.Acc[0].리밸_C = title;
                    break;
                case "리밸_D":
                    Form1.Acc[0].리밸_D = title;
                    break;
                case "리밸_E":
                    Form1.Acc[0].리밸_E = title;
                    break;
                case "리밸_F":
                    Form1.Acc[0].리밸_F = title;
                    break;
                case "리밸_G":
                    Form1.Acc[0].리밸_G = title;
                    break;
                case "청산_A":
                    Form1.Acc[0].청산_A = title;
                    break;
                case "청산_B":
                    Form1.Acc[0].청산_B = title;
                    break;
                case "청산_C":
                    Form1.Acc[0].청산_C = title;
                    break;
                case "기간_A":
                    Form1.Acc[0].기간_A = title;
                    break;
                case "기간_B":
                    Form1.Acc[0].기간_B = title;
                    break;
                case "기간_C":
                    Form1.Acc[0].기간_C = title;
                    break;
                case "기간_D":
                    Form1.Acc[0].기간_D = title;
                    break;
                case "기간_E":
                    Form1.Acc[0].기간_E = title;
                    break;
                case "기간_F":
                    Form1.Acc[0].기간_F = title;
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
                    cc = Form1.Acc[0].신규_A;
                    break;
                case "신규_B":
                    cc = Form1.Acc[0].신규_B;
                    break;
                case "신규_C":
                    cc = Form1.Acc[0].신규_C;
                    break;
                case "반복_A":
                    cc = Form1.Acc[0].반복_A;
                    break;
                case "반복_B":
                    cc = Form1.Acc[0].반복_B;
                    break;
                case "반복_C":
                    cc = Form1.Acc[0].반복_C;
                    break;
                case "반복_D":
                    cc = Form1.Acc[0].반복_D;
                    break;
                case "반복_E":
                    cc = Form1.Acc[0].반복_E;
                    break;
                case "반복_F":
                    cc = Form1.Acc[0].반복_F;
                    break;
                case "반복_G":
                    cc = Form1.Acc[0].반복_G;
                    break;
                case "반복_H":
                    cc = Form1.Acc[0].반복_H;
                    break;
                case "반복_I":
                    cc = Form1.Acc[0].반복_I;
                    break;
                case "반복_J":
                    cc = Form1.Acc[0].반복_J;
                    break;
                case "반복_K":
                    cc = Form1.Acc[0].반복_K;
                    break;
                case "반복_L":
                    cc = Form1.Acc[0].반복_L;
                    break;
                case "반복_M":
                    cc = Form1.Acc[0].반복_M;
                    break;
                case "반복_N":
                    cc = Form1.Acc[0].반복_N;
                    break;
                case "리밸_A":
                    cc = Form1.Acc[0].리밸_A;
                    break;
                case "리밸_B":
                    cc = Form1.Acc[0].리밸_B;
                    break;
                case "리밸_C":
                    cc = Form1.Acc[0].리밸_C;
                    break;
                case "리밸_D":
                    cc = Form1.Acc[0].리밸_D;
                    break;
                case "리밸_E":
                    cc = Form1.Acc[0].리밸_E;
                    break;
                case "리밸_F":
                    cc = Form1.Acc[0].리밸_F;
                    break;
                case "리밸_G":
                    cc = Form1.Acc[0].리밸_G;
                    break;
                case "잔고청산_A":
                    cc = Form1.Acc[0].청산_A;
                    break;
                case "잔고청산_B":
                    cc = Form1.Acc[0].청산_B;
                    break;
                case "잔고청산_C":
                    cc = Form1.Acc[0].청산_C;
                    break;
                case "Day_A":
                    cc = Form1.Acc[0].기간_A;
                    break;
                case "Day_B":
                    cc = Form1.Acc[0].기간_B;
                    break;
                case "Day_C":
                    cc = Form1.Acc[0].기간_C;
                    break;
                case "Day_D":
                    cc = Form1.Acc[0].기간_D;
                    break;
                case "Day_E":
                    cc = Form1.Acc[0].기간_E;
                    break;
                case "Day_F":
                    cc = Form1.Acc[0].기간_F;
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
            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 1;

                Form1.form1.LB_검색결과n관심리스트.Items.Clear();
                Form1.form1.LB_검색결과n관심리스트.Items.Add("신규매수_A :: " + Form1.Acc[0].신규_A);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("신규매수_B :: " + Form1.Acc[0].신규_B);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("신규매수_C :: " + Form1.Acc[0].신규_C);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("");

                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_A :: " + Form1.Acc[0].반복_A);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_B :: " + Form1.Acc[0].반복_B);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_C :: " + Form1.Acc[0].반복_C);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_D :: " + Form1.Acc[0].반복_D);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_E :: " + Form1.Acc[0].반복_E);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_F :: " + Form1.Acc[0].반복_F);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_G :: " + Form1.Acc[0].반복_G);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_H :: " + Form1.Acc[0].반복_H);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_I :: " + Form1.Acc[0].반복_I);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_J :: " + Form1.Acc[0].반복_J);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_K :: " + Form1.Acc[0].반복_K);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_L :: " + Form1.Acc[0].반복_L);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_M :: " + Form1.Acc[0].반복_M);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("반복매매_N :: " + Form1.Acc[0].반복_N);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("");

                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_A :: " + Form1.Acc[0].리밸_A);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_B :: " + Form1.Acc[0].리밸_B);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_C :: " + Form1.Acc[0].리밸_C);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_D :: " + Form1.Acc[0].리밸_D);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_E :: " + Form1.Acc[0].리밸_E);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_F :: " + Form1.Acc[0].리밸_F);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("리밸런싱_G :: " + Form1.Acc[0].리밸_G);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("");

                Form1.form1.LB_검색결과n관심리스트.Items.Add("잔고청산_A :: " + Form1.Acc[0].청산_A);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("잔고청산_B :: " + Form1.Acc[0].청산_B);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("잔고청산_C :: " + Form1.Acc[0].청산_C);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("");

                Form1.form1.LB_검색결과n관심리스트.Items.Add("매매기간_A :: " + Form1.Acc[0].기간_A);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("매매기간_B :: " + Form1.Acc[0].기간_B);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("매매기간_C :: " + Form1.Acc[0].기간_C);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("매매기간_D :: " + Form1.Acc[0].기간_D);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("매매기간_E :: " + Form1.Acc[0].기간_E);
                Form1.form1.LB_검색결과n관심리스트.Items.Add("매매기간_F :: " + Form1.Acc[0].기간_F);
            });
        }


        public static void 관심자동등록리스트확인()
        {
            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 2;
                Form1.form1.LB_검색결과n관심리스트.Items.Clear();

                Form1.form1.LB_검색결과n관심리스트.Items.Add("관심그룹 자동등록 리스트");
                Form1.form1.LB_검색결과n관심리스트.Items.Add("N /그룹 / 검색식 / 시간");

                int num = 0;
                for (int i = 0; i < Form1.Interest_AutoAdd_List.Count; i++)
                {
                    string[] list = Form1.Interest_AutoAdd_List[i].Split(';');

                    bool 성공 = int.TryParse(list[2], out int result);

                    if (성공 && int.Parse(list[2].Trim()) > 999)
                    {
                        num++;
                        Form1.form1.LB_검색결과n관심리스트.Items.Add(num + "ㆍ" + list[0] + "ㆍ" + list[1] + "ㆍ" + list[2]);
                    }
                    else if (!성공)
                    {
                        num++;
                        Form1.form1.LB_검색결과n관심리스트.Items.Add(num + "ㆍ" + list[0] + "ㆍ" + list[1] + "ㆍ" + list[2]);
                    }
                }

                num = 0;
                Form1.form1.LB_검색결과n관심리스트.Items.Add(" ");
                Form1.form1.LB_검색결과n관심리스트.Items.Add("관심그룹 종목 자동삭제 리스트");
                Form1.form1.LB_검색결과n관심리스트.Items.Add("N /기간 / 그룹 / ");

                for (int i = 0; i < Form1.Interest_AutoAdd_List.Count; i++)
                {
                    string[] list = Form1.Interest_AutoAdd_List[i].Split(';');

                    bool 성공 = int.TryParse(list[2], out int result);

                    if (성공 && int.Parse(list[2].Trim()) <= 999)
                    {
                        num++;
                        Form1.form1.LB_검색결과n관심리스트.Items.Add(num + "ㆍ" + list[2] + "ㆍ" + list[0] + "ㆍ" + list[1]);
                    }
                }
            });
        }

        public static void save_관심그룹_Title_기록()
        {
            Form1.form1.account_comboBox.Invoke((MethodInvoker)delegate ()
            {
                if (Form1.form1.account_comboBox.Text.Length > 0)
                {
                    Task Interest_Title_ = new Task(() =>
                    {
                        string File_Check = Application.StartupPath + @"\Data\" + Form1.USER_ID + "__" + Properties.Settings.Default.select_account + "__\\관심그룹\\관심그룹_Title.txt";

                        using (StreamWriter writer_ = new StreamWriter(File_Check))
                        {
                            writer_.Write(Form1.Acc[0].신규_A + "^" + Form1.Acc[0].신규_B + "^" + Form1.Acc[0].신규_C + "^" +
                                 Form1.Acc[0].반복_A + "^" + Form1.Acc[0].반복_B + "^" + Form1.Acc[0].반복_C + "^" + Form1.Acc[0].반복_D + "^" + Form1.Acc[0].반복_E + "^" + Form1.Acc[0].반복_F + "^" + Form1.Acc[0].반복_G + "^" + Form1.Acc[0].반복_H + "^" + Form1.Acc[0].반복_I + "^" + Form1.Acc[0].반복_J + "^" +
                                 Form1.Acc[0].반복_K + "^" + Form1.Acc[0].반복_L + "^" + Form1.Acc[0].반복_M + "^" + Form1.Acc[0].반복_N + "^" + Form1.Acc[0].리밸_A + "^" + Form1.Acc[0].리밸_B + "^" + Form1.Acc[0].리밸_C + "^" + Form1.Acc[0].리밸_D + "^" + Form1.Acc[0].리밸_E + "^" + Form1.Acc[0].리밸_F + "^" + Form1.Acc[0].리밸_G + "^" +
                                 Form1.Acc[0].청산_A + "^" + Form1.Acc[0].청산_B + "^" + Form1.Acc[0].청산_C + "^" + Form1.Acc[0].기간_A + "^" + Form1.Acc[0].기간_B + "^" + Form1.Acc[0].기간_C + "^" + Form1.Acc[0].기간_D + "^" + Form1.Acc[0].기간_E + "^" + Form1.Acc[0].기간_F + "^" + "&\n");

                            Form1.Interest_Title_List.Distinct().ToList();

                            for (int i = 0; i < Form1.Interest_Title_List.Count; i++)
                            {
                                writer_.Write(Form1.Interest_Title_List[i] + ";");
                                if ((i + 1) % 15 == 0)
                                {
                                    writer_.Write("\n");
                                }
                            }

                            writer_.Write("&\n");

                            for (int i = 0; i < Form1.Interest_AutoAdd_List.Count; i++)
                            {
                                writer_.Write(Form1.Interest_AutoAdd_List[i] + "^");
                                if ((i + 1) % 5 == 0)
                                {
                                    writer_.Write("\n");
                                }
                            }
                        }
                    });
                    Form1.writing_Manager.RequestTrData(Interest_Title_);
                }
            });
        }

        public static void save_관심그룹_List_기록()
        {
            Form1.form1.account_comboBox.Invoke((MethodInvoker)delegate ()
            {
                if (Form1.form1.account_comboBox.Text.Length > 0)
                {
                    Task InterestGroup_ = new Task(() =>
                    {
                        string File_Check = Application.StartupPath + @"\Data\" + Form1.USER_ID + "__" + Properties.Settings.Default.select_account + "__\\관심그룹\\관심그룹_List.txt";

                        using (StreamWriter writer_ = new StreamWriter(File_Check))
                        {
                            for (int i = 0; i < Form1.Interest_stock_List.Count; i++)
                            {
                                if (i == Form1.Interest_stock_List.Count - 1)
                                {
                                    writer_.Write(Form1.Interest_stock_List[i].date + "^" + Form1.Interest_stock_List[i].Code + "^" + Form1.Interest_stock_List[i].Title + "^" + Form1.Interest_stock_List[i].시세등록 + "^" + i);
                                }
                                else
                                {
                                    writer_.Write(Form1.Interest_stock_List[i].date + "^" + Form1.Interest_stock_List[i].Code + "^" + Form1.Interest_stock_List[i].Title + "^" + Form1.Interest_stock_List[i].시세등록 + "^" + i + ";");
                                }

                                if ((i + 1) % 20 == 0)
                                {
                                    writer_.Write("\n");
                                }
                            }
                        }
                    });
                    Form1.writing_Manager.RequestTrData(InterestGroup_);
                }
            });
        }

        public static void DataLoad_관심그룹_Title_기록()
        {
            FileInfo File_Check = new FileInfo(Application.StartupPath + @"\Data\" + Form1.USER_ID + "__" + Properties.Settings.Default.select_account + "__\\관심그룹\\관심그룹_Title.txt");

            if (File_Check.Exists)
            {
                try
                {
                    Form1.Interest_AutoAdd_List.Clear();
                    Form1.Interest_Title_List.Clear();

                    string path = Application.StartupPath + @"\Data\" + Form1.USER_ID + "__" + Properties.Settings.Default.select_account + "__\\관심그룹\\관심그룹_Title.txt";
                    string OptionLists = File.ReadAllText(path);
                    string 그룹 = OptionLists.Split('&')[0];

                    Form1.Acc[0].신규_A = 그룹.Split('^')[0];
                    Form1.Acc[0].신규_B = 그룹.Split('^')[1];
                    Form1.Acc[0].신규_C = 그룹.Split('^')[2];

                    Form1.Acc[0].반복_A = 그룹.Split('^')[3];
                    Form1.Acc[0].반복_B = 그룹.Split('^')[4];
                    Form1.Acc[0].반복_C = 그룹.Split('^')[5];
                    Form1.Acc[0].반복_D = 그룹.Split('^')[6];
                    Form1.Acc[0].반복_E = 그룹.Split('^')[7];
                    Form1.Acc[0].반복_F = 그룹.Split('^')[8];
                    Form1.Acc[0].반복_G = 그룹.Split('^')[9];
                    Form1.Acc[0].반복_H = 그룹.Split('^')[10];
                    Form1.Acc[0].반복_I = 그룹.Split('^')[11];
                    Form1.Acc[0].반복_J = 그룹.Split('^')[12];
                    Form1.Acc[0].반복_K = 그룹.Split('^')[13];
                    Form1.Acc[0].반복_L = 그룹.Split('^')[14];
                    Form1.Acc[0].반복_M = 그룹.Split('^')[15];
                    Form1.Acc[0].반복_N = 그룹.Split('^')[16];

                    Form1.Acc[0].리밸_A = 그룹.Split('^')[17];
                    Form1.Acc[0].리밸_B = 그룹.Split('^')[18];
                    Form1.Acc[0].리밸_C = 그룹.Split('^')[19];

                    Form1.Acc[0].리밸_D = 그룹.Split('^')[20];
                    Form1.Acc[0].리밸_E = 그룹.Split('^')[21];
                    Form1.Acc[0].리밸_G = "기본값";
                    Form1.Acc[0].리밸_F = "기본값";

                    Form1.Acc[0].청산_A = 그룹.Split('^')[22];
                    Form1.Acc[0].청산_B = 그룹.Split('^')[23];
                    Form1.Acc[0].청산_B = "기본값";

                    Form1.Acc[0].기간_A = 그룹.Split('^')[24];
                    Form1.Acc[0].기간_B = 그룹.Split('^')[25];
                    Form1.Acc[0].기간_C = 그룹.Split('^')[26];
                    Form1.Acc[0].기간_D = 그룹.Split('^')[27];
                    Form1.Acc[0].기간_E = 그룹.Split('^')[28];
                    Form1.Acc[0].기간_F = 그룹.Split('^')[29];

                    if (그룹.Split('^').Length > 33)
                    {
                        Form1.Acc[0].리밸_G = 그룹.Split('^')[22];
                        Form1.Acc[0].리밸_F = 그룹.Split('^')[23];

                        Form1.Acc[0].청산_A = 그룹.Split('^')[24];
                        Form1.Acc[0].청산_B = 그룹.Split('^')[25];
                        Form1.Acc[0].청산_B = 그룹.Split('^')[26];

                        Form1.Acc[0].기간_A = 그룹.Split('^')[27];
                        Form1.Acc[0].기간_B = 그룹.Split('^')[28];
                        Form1.Acc[0].기간_C = 그룹.Split('^')[29];
                        Form1.Acc[0].기간_D = 그룹.Split('^')[30];
                        Form1.Acc[0].기간_E = 그룹.Split('^')[31];
                        Form1.Acc[0].기간_F = 그룹.Split('^')[32];
                    }

                    if (OptionLists.Split('&')[1].Length > 0)
                    {
                        Form1.form1.CBB_관심그룹.Items.Clear();
                        Form1.form1.CBB_관심그룹변경_Title.Items.Clear();
                        Form1.form1.CBB_관심그룹변경_Title.Items.Add("기본값");

                        Form1.form1.CBB_관심그룹_A.Items.Clear();
                        Form1.form1.CBB_관심그룹_B.Items.Clear();
                        Form1.form1.CBB_관심그룹_C.Items.Clear();
                        Form1.form1.CBB_Watch관심_A.Items.Clear();
                        Form1.form1.CBB_Watch관심_B.Items.Clear();
                        Form1.form1.CBB_Watch관심_C.Items.Clear();
                        Form1.form1.CBB_Watch관심_D.Items.Clear();
                        Form1.form1.CBB_Watch관심_A.Items.Add("기본값");
                        Form1.form1.CBB_Watch관심_B.Items.Add("기본값");
                        Form1.form1.CBB_Watch관심_C.Items.Add("기본값");
                        Form1.form1.CBB_Watch관심_D.Items.Add("기본값");

                        string[] full_관심_Title = OptionLists.Split('&')[1].Trim().Split(';');

                        for (int n = 0; n < full_관심_Title.Length - 1; n++)
                        {
                            string Title = full_관심_Title[n].Trim();

                            if (!Form1.Interest_Title_List.Contains(Title))
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
                            }
                        }
                    }

                    if (Form1.form1.CBB_Watch관심_A.Items.Contains(Properties.Settings.Default.CBB_Watch관심_A)) { Form1.form1.CBB_Watch관심_A.Text = Properties.Settings.Default.CBB_Watch관심_A; }
                    else { Form1.form1.CBB_Watch관심_A.SelectedIndex = 0; }

                    if (Form1.form1.CBB_Watch관심_B.Items.Contains(Properties.Settings.Default.CBB_Watch관심_B)) { Form1.form1.CBB_Watch관심_B.Text = Properties.Settings.Default.CBB_Watch관심_B; }
                    else { Form1.form1.CBB_Watch관심_B.SelectedIndex = 0; }

                    if (Form1.form1.CBB_Watch관심_C.Items.Contains(Properties.Settings.Default.CBB_Watch관심_C)) { Form1.form1.CBB_Watch관심_C.Text = Properties.Settings.Default.CBB_Watch관심_C; }
                    else { Form1.form1.CBB_Watch관심_C.SelectedIndex = 0; }

                    if (Form1.form1.CBB_Watch관심_D.Items.Contains(Properties.Settings.Default.CBB_Watch관심_D)) { Form1.form1.CBB_Watch관심_D.Text = Properties.Settings.Default.CBB_Watch관심_D; }
                    else { Form1.form1.CBB_Watch관심_D.SelectedIndex = 0; }

                    Form1.form1.CBB_관심그룹변경.SelectedIndex = Properties.Settings.Default.CBB_신규그룹;


                    Form1.form1.CBB_신규그룹_DropDownClosed(Form1.form1.CBB_관심그룹변경, null);

                    if (OptionLists.Split('&')[2].Length > 0)
                    {
                        string[] full_자동등록 = OptionLists.Split('&')[2].Split('^');

                        for (int n = 0; n < full_자동등록.Length - 1; n++)
                        {
                            Form1.Interest_AutoAdd_List.Add(full_자동등록[n].Trim());
                        }
                    }

                    Form1.form1.CBB_관심그룹변경.SelectedIndex = Properties.Settings.Default.CBB_신규그룹;
                    Form1.form1.CBB_관심그룹변경_Title.Text = Properties.Settings.Default.CBB_신규그룹_관심종목;
                    Form1.form1.CBB_관심검색식.Text = Properties.Settings.Default.CBB_관심검색식;
                    Form1.form1.CBB_관심그룹.Text = Properties.Settings.Default.CBB_관심그룹;
                    Form1.form1.CBB_관심그룹_A.Text = Properties.Settings.Default.CBB_관심그룹_A;
                    Form1.form1.CBB_관심그룹_B.Text = Properties.Settings.Default.CBB_관심그룹_B;
                    Form1.form1.CBB_관심그룹_C.Text = Properties.Settings.Default.CBB_관심그룹_C;

                    매매관심그룹리스트확인();
                }
                catch
                {
                    Form1.AutoClosingAlram("관심그룹_Title.txt 로딩 에러 파일을 확인하세요", "로딩에러", 20, "에러");
                    Form1.Error_Log("[에러 확인] 관심그룹_Title.txt 로딩 에러");
                }
            }
        }


        public static void DataLoad_관심그룹_List_기록()
        {
            FileInfo File_Check = new FileInfo(Application.StartupPath + @"\Data\" + Form1.USER_ID + "__" + Properties.Settings.Default.select_account + "__\\관심그룹\\관심그룹_List.txt");

            if (File_Check.Exists)
            {
                try
                {
                    //파일열기
                    StreamReader SR = new StreamReader(Application.StartupPath + @"\Data\" + Form1.USER_ID + "__" + Properties.Settings.Default.select_account + "__\\관심그룹\\관심그룹_List.txt");

                    string OptionLists = SR.ReadToEnd();    //sehwa308bb850107

                    SR.Close(); // 파일닫기

                    Form1.Interest_stock_List.Clear();

                    if (OptionLists.Length > 0)
                    {
                        string[] full_관심_List = OptionLists.Split(';');

                        for (int n = 0; n < full_관심_List.Length; n++)
                        {
                            string[] 관심 = full_관심_List[n].Split('^');

                            string 날짜 = 관심[0].Trim();
                            string 종목코드 = 관심[1].Trim();
                            string Title = 관심[2].Trim();
                            bool.TryParse(관심[3].Trim(), out bool 시세등록);

                            Interest_stock item = Form1.Interest_stock_List.Find(o => o.Title.Equals(Title) && o.Code.Equals(종목코드));
                            if (item == null)
                            {
                                Interest_stock Interest_item = new Interest_stock(종목코드, Form1.form1.axKHOpenAPI1.GetMasterCodeName(종목코드), 날짜, Title, 시세등록);
                                Form1.Interest_stock_List.Add(Interest_item);
                            }
                        }
                    }
                }
                catch
                {
                    Form1.AutoClosingAlram("관심그룹_List.txt 로딩 에러 파일을 확인하세요", "로딩에러", 20, "에러");
                    Form1.Error_Log("[에러 확인] 관심그룹_List.txt 로딩 에러");
                }
            }
        }


        public static void BT_관심그룹변경_Click()
        {
            string title = Form1.form1.CBB_관심그룹변경_Title.Text;
            string item = Form1.form1.CBB_관심그룹변경.SelectedItem.ToString();

            if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("신규_A") && !Form1.Acc[0].신규_A.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("신규_B") && !Form1.Acc[0].신규_B.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("신규_C") && !Form1.Acc[0].신규_C.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_A") && !Form1.Acc[0].반복_A.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_B") && !Form1.Acc[0].반복_B.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_C") && !Form1.Acc[0].반복_C.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_D") && !Form1.Acc[0].반복_D.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_E") && !Form1.Acc[0].반복_E.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_F") && !Form1.Acc[0].반복_F.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_G") && !Form1.Acc[0].반복_G.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_H") && !Form1.Acc[0].반복_H.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_I") && !Form1.Acc[0].반복_I.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_J") && !Form1.Acc[0].반복_J.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_K") && !Form1.Acc[0].반복_K.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_L") && !Form1.Acc[0].반복_L.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_M") && !Form1.Acc[0].반복_M.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("반복_N") && !Form1.Acc[0].반복_N.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_A") && !Form1.Acc[0].리밸_A.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_B") && !Form1.Acc[0].리밸_B.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_C") && !Form1.Acc[0].리밸_C.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_D") && !Form1.Acc[0].리밸_D.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_E") && !Form1.Acc[0].리밸_E.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_F") && !Form1.Acc[0].리밸_F.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("리밸_G") && !Form1.Acc[0].리밸_G.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("청산_A") && !Form1.Acc[0].청산_A.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("청산_B") && !Form1.Acc[0].청산_B.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("청산_C") && !Form1.Acc[0].청산_C.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("기간_A") && !Form1.Acc[0].기간_A.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("기간_B") && !Form1.Acc[0].기간_B.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("기간_C") && !Form1.Acc[0].기간_C.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("기간_D") && !Form1.Acc[0].기간_D.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("기간_E") && !Form1.Acc[0].기간_E.Equals(title))
            {
                그룹변경물음();
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedItem.Equals("기간_F") && !Form1.Acc[0].기간_F.Equals(title))
            {
                그룹변경물음();
            }

            void 그룹변경물음()
            {
                var thread = new Thread(
                () =>
                {
                    Form1.form1.Invoke((MethodInvoker)delegate ()
                    {
                        using (new CenterWinDialog(Form1.form1))
                            if (MessageBox.Show(item + " 의 관심그룹을 '" + title + "' 로 변경 하시겠습니까?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                그룹변경(item, title);

                                매매관심그룹리스트확인();
                                save_관심그룹_Title_기록();
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
                        Form1.form1.Invoke((MethodInvoker)delegate ()
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

                                        save_관심그룹_Title_기록();
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
                if (Form1.Interest_Title_List.Contains(Title))
                {
                    var thread = new Thread(
                    () =>
                    {
                        string 관심종목 = "관심종목 없음";
                        string 자동등록 = "자동등록 없음";

                        List<Interest_stock> 그룹_확인 = Form1.Interest_stock_List.FindAll(o => o.Title.Equals(Title));
                        if (그룹_확인.Count > 0)
                        {
                            관심종목 = "관심종목 있음";
                        }
                        string 자동삭제_확인 = Form1.Interest_AutoAdd_List.Find(o => o.StartsWith(Title));
                        if (자동삭제_확인 != null)
                        {
                            자동등록 = "자동등록 있름";
                        }

                        Form1.form1.Invoke((MethodInvoker)delegate ()
                        {
                            using (new CenterWinDialog(Form1.form1))
                                if (MessageBox.Show(Title + " 관심그룹을 삭제 하시겠습니까?\n(" + Title + " 그룹에 " + 관심종목 + " " + 자동등록 + ")\n같이 삭제 됩니다.", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    Form1.Interest_Title_List.Remove(Title);

                                    Form1.form1.CBB_관심그룹.Items.Remove(Title);
                                    Form1.form1.CBB_관심그룹변경_Title.Items.Remove(Title);
                                    Form1.form1.CBB_관심그룹_A.Items.Remove(Title);
                                    Form1.form1.CBB_관심그룹_B.Items.Remove(Title);
                                    Form1.form1.CBB_관심그룹_C.Items.Remove(Title);
                                    Form1.form1.CBB_Watch관심_A.Items.Remove(Title);
                                    Form1.form1.CBB_Watch관심_B.Items.Remove(Title);
                                    Form1.form1.CBB_Watch관심_C.Items.Remove(Title);
                                    Form1.Interest_Title_List.Remove(Title);

                                    List<Interest_stock> 그룹 = Form1.Interest_stock_List.FindAll(o => o.Title.Equals(Title));

                                    if (그룹.Count > 0)
                                    {
                                        for (int i = 0; i < 그룹.Count; i++)
                                        {
                                            Interest_stock 삭제 = 그룹[i];
                                            Form1.Interest_stock_List.Remove(삭제);
                                        }
                                    }

                                    string 자동삭제 = Form1.Interest_AutoAdd_List.Find(o => o.StartsWith(Title));

                                    if (자동삭제 != null)
                                    {
                                        Form1.Interest_AutoAdd_List.Remove(자동삭제);
                                    }

                                    매매관심그룹리스트확인();

                                    save_관심그룹_Title_기록();
                                    save_관심그룹_List_기록();

                                    string position = "신규_A";
                                    if (Form1.Acc[0].신규_A.Equals(Title)) 그룹변경물음();
                                    if (Form1.Acc[0].신규_B.Equals(Title)) { position = "신규_B"; 그룹변경물음(); }
                                    if (Form1.Acc[0].신규_C.Equals(Title)) { position = "신규_C"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_A.Equals(Title)) { position = "반복_A"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_B.Equals(Title)) { position = "반복_B"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_C.Equals(Title)) { position = "반복_C"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_D.Equals(Title)) { position = "반복_D"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_E.Equals(Title)) { position = "반복_E"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_F.Equals(Title)) { position = "반복_F"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_G.Equals(Title)) { position = "반복_G"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_H.Equals(Title)) { position = "반복_H"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_I.Equals(Title)) { position = "반복_I"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_J.Equals(Title)) { position = "반복_J"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_K.Equals(Title)) { position = "반복_K"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_L.Equals(Title)) { position = "반복_L"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_M.Equals(Title)) { position = "반복_M"; 그룹변경물음(); }
                                    if (Form1.Acc[0].반복_N.Equals(Title)) { position = "반복_N"; 그룹변경물음(); }
                                    if (Form1.Acc[0].리밸_A.Equals(Title)) { position = "리밸_A"; 그룹변경물음(); }
                                    if (Form1.Acc[0].리밸_B.Equals(Title)) { position = "리밸_B"; 그룹변경물음(); }
                                    if (Form1.Acc[0].리밸_C.Equals(Title)) { position = "리밸_C"; 그룹변경물음(); }
                                    if (Form1.Acc[0].리밸_D.Equals(Title)) { position = "리밸_D"; 그룹변경물음(); }
                                    if (Form1.Acc[0].리밸_E.Equals(Title)) { position = "리밸_E"; 그룹변경물음(); }
                                    if (Form1.Acc[0].리밸_F.Equals(Title)) { position = "리밸_F"; 그룹변경물음(); }
                                    if (Form1.Acc[0].리밸_G.Equals(Title)) { position = "리밸_G"; 그룹변경물음(); }
                                    if (Form1.Acc[0].청산_A.Equals(Title)) { position = "청산_A"; 그룹변경물음(); }
                                    if (Form1.Acc[0].청산_B.Equals(Title)) { position = "청산_B"; 그룹변경물음(); }
                                    if (Form1.Acc[0].청산_C.Equals(Title)) { position = "청산_C"; 그룹변경물음(); }
                                    if (Form1.Acc[0].기간_A.Equals(Title)) { position = "기간_A"; 그룹변경물음(); }
                                    if (Form1.Acc[0].기간_B.Equals(Title)) { position = "기간_B"; 그룹변경물음(); }
                                    if (Form1.Acc[0].기간_C.Equals(Title)) { position = "기간_C"; 그룹변경물음(); }
                                    if (Form1.Acc[0].기간_D.Equals(Title)) { position = "기간_D"; 그룹변경물음(); }
                                    if (Form1.Acc[0].기간_E.Equals(Title)) { position = "기간_E"; 그룹변경물음(); }
                                    if (Form1.Acc[0].기간_F.Equals(Title)) { position = "기간_F"; 그룹변경물음(); }

                                    void 그룹변경물음()
                                    {
                                        using (new CenterWinDialog(Form1.form1))
                                            if (MessageBox.Show(position + " _관심그룹인 " + Title + " 이 삭제 되었습니다.\n신규_C 관심그룹을 '기본값'으로 변경 하시겠습니까?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                            {
                                                그룹변경(position, "기본값");
                                                매매관심그룹리스트확인();
                                                save_관심그룹_Title_기록();
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

        public static void 검색요청(string 스크린넘버, string 검색식)
        {
            if (!Form1.form1.검색결과_List.Contains(검색식))
            {
                Form1.form1.검색결과_List.Clear();
                Form1.form1.검색결과_List.Add(DateTime.Now.ToString("HH:mm:ss"));                //0
                Form1.form1.검색결과_List.Add(검색식);                                           //1
            }
            else
            {
                Form1.form1.검색결과_List[0] = DateTime.Now.ToString("HH:mm:ss");
            }

            Condition condition = Form1.form1.ConditionList.Find(o => o.name.Equals(검색식));
            if (condition != null)
            {
                int result = Form1.form1.axKHOpenAPI1.SendCondition(스크린넘버, condition.name, condition.index, 0);

                if (result == 1)
                {
                    Form1.AutoClosingAlram(검색식 + " 검색 요청 하였습니다.", "동작알림", 5, "동작");
                }
                else
                {
                    if (Form1.form1.LB_검색결과n관심리스트.Items.Count > 0)
                    {
                        if (!Form1.form1.LB_검색결과n관심리스트.Items[2].ToString().Contains("검색실패"))
                        {
                            Form1.form1.LB_검색결과n관심리스트.Items[2] = ("0 검색개수: 검색실패");
                            Form1.form1.LB_검색결과n관심리스트.Items.Insert(3, "0 동일검색식 1분에 1회 요청가능");
                        }
                    }
                    else
                    {
                        Form1.form1.LB_검색결과n관심리스트.Items.Add("0 시간: " + DateTime.Now.ToString("HH:mm:ss"));
                        Form1.form1.LB_검색결과n관심리스트.Items.Add("0 검색식:: " + 검색식);
                        Form1.form1.LB_검색결과n관심리스트.Items.Add("0 검색개수:: 검색실패");
                        Form1.form1.LB_검색결과n관심리스트.Items.Add("0 동일검색식 1분에 1회 요청가능");
                        Form1.form1.LB_검색결과n관심리스트.Items.Add("0 -----------------------------");
                    }

                    Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 0;
                    CBB_실시간n그룹n관심자동_indexchange(0);
                }
            }
        }

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
                                    Form1.form1.Invoke((MethodInvoker)delegate ()
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
                    Interest_stock item = Form1.Interest_stock_List.Find(o => o.Title.Equals(Form1.form1.CBB_관심그룹.Text) && o.Code.Equals(Code));
                    if (item == null)
                    {
                        Interest_stock Interest_item = new Interest_stock(Code, Form1.Market_Item_List[Code].종목명, Form1.today, Form1.form1.CBB_관심그룹.Text, Form1.form1.CB_시세감시등록.Checked);
                        Form1.Interest_stock_List.Add(Interest_item);
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

                save_관심그룹_List_기록();
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
                        Form1.form1.LB_관심_A.Items.Add(interests[i].date + " : " + interests[i].Name);
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
                        Form1.form1.LB_관심_B.Items.Add(interests[i].date + " : " + interests[i].Name);
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
                        Form1.form1.LB_관심_C.Items.Add(interests[i].date + " : " + interests[i].Name);
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
                        Form1.form1.Invoke((MethodInvoker)delegate ()
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

                                    save_관심그룹_List_기록();

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
                            Form1.form1.Invoke((MethodInvoker)delegate ()
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
                                        save_관심그룹_List_기록();
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
            void 초기화()
            {
                Form1.form1.CBB_관심그룹변경_Title.SelectedIndex = 0;
                Form1.form1.CBB_관심그룹.SelectedIndex = -1;
            }

            if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(0))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].신규_A))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].신규_A;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].신규_A;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].신규_A;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(1))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].신규_B))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].신규_B;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].신규_B;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].신규_B;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(2))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].신규_C))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].신규_C;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].신규_C;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].신규_C;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(3))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].반복_A))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].반복_A;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].반복_A;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].반복_A;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(4))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].반복_B))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].반복_B;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].반복_B;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].반복_B;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(5))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].반복_C))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].반복_C;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].반복_C;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].반복_C;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(6))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].반복_D))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].반복_D;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].반복_D;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].반복_D;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(7))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].반복_E))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].반복_E;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].반복_E;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].반복_E;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(8))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].반복_F))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].반복_F;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].반복_F;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].반복_F;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(9))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].반복_G))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].반복_G;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].반복_G;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].반복_G;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(10))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].반복_H))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].반복_H;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].반복_H;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].반복_H;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(11))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].반복_I))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].반복_I;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].반복_I;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].반복_I;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(12))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].반복_J))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].반복_J;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].반복_J;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].반복_J;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(13))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].반복_K))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].반복_K;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].반복_K;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].반복_K;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(14))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].반복_L))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].반복_L;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].반복_L;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].반복_L;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(15))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].반복_M))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].반복_M;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].반복_M;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].반복_M;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(16))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].리밸_A))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].리밸_A;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].리밸_A;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].리밸_A;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(17))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].리밸_B))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].리밸_B;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].리밸_B;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].리밸_B;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(18))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].리밸_C))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].리밸_C;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].리밸_C;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].리밸_C;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(19))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].리밸_D))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].리밸_D;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].리밸_D;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].리밸_D;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(20))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].리밸_E))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].리밸_E;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].리밸_E;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].리밸_E;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(21))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].리밸_F))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].리밸_F;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].리밸_F;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].리밸_F;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(22))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].리밸_G))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].리밸_G;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].리밸_G;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].리밸_G;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(23))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].청산_A))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].청산_A;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].청산_A;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].청산_A;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(24))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].청산_B))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].청산_B;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].청산_B;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].청산_B;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(25))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].청산_C))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].청산_C;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].청산_C;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].청산_C;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(26))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].기간_A))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].기간_A;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].기간_A;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].기간_A;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(27))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].기간_B))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].기간_B;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].기간_B;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].기간_B;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(28))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].기간_C))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].기간_C;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].기간_C;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].기간_C;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(29))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].기간_D))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].기간_D;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].기간_D;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].기간_D;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(30))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].기간_E))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].기간_E;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].기간_E;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].기간_E;
                }
                else
                {
                    초기화();
                }
            }
            else if (Form1.form1.CBB_관심그룹변경.SelectedIndex.Equals(31))
            {
                if (Form1.form1.CBB_관심그룹변경_Title.Items.Contains(Form1.Acc[0].기간_F))
                {
                    Form1.form1.CBB_관심그룹변경_Title.SelectedItem = Form1.Acc[0].기간_F;
                    Form1.form1.CBB_관심그룹.SelectedItem = Form1.Acc[0].기간_F;
                    Form1.form1.TB_관심그룹추가.Text = Form1.Acc[0].기간_F;
                }
                else
                {
                    초기화();
                }
            }
        }

        public static void BT_자동등록_Click()
        {
            string 시간 = "";
            int.TryParse(Form1.form1.TB_자동관심_동작시간.Text, out int Time);
            if (Time > 235959 || Time == 0)
            {
                시간 = "153000";
                Form1.form1.TB_자동관심_동작시간.Text = 시간;
            }
            else
            {
                시간 = Time.ToString();
            }

            string 검색식 = Form1.form1.CBB_관심검색식.Text;
            string 관심그룹 = Form1.form1.CBB_관심그룹.Text;
            string 실시간등록 = Form1.form1.CB_시세감시등록.Checked.ToString();


            if (검색식.Trim().Length > 0 && 관심그룹.Trim().Length > 0)
            {
                int num = 1;
                for (int i = 0; i < Form1.Interest_AutoAdd_List.Count; i++)
                {
                    string 자동시간 = Form1.Interest_AutoAdd_List[i].Split(';')[2];
                    if (시간.Equals(자동시간))
                    {
                        if (!자동시간.Equals("실시간"))
                        {
                            num++;
                        }
                    }
                }

                if (num > 2)
                {
                    Form1.알림창("[ 관심그룹 ]\n\n시간중복 (" + 시간 + ") 으로 자동등록된 그룹이 2개 이상입니다.\n\n1초에 2개 등록 가능합니다.", 10, false);
                }
                else
                {
                    if (!Form1.Interest_AutoAdd_List.Exists(o => o.StartsWith(관심그룹 + ";" + 검색식 + ";" + 시간)))
                    {
                        var thread = new Thread(
                        () =>
                        {
                            string para = 관심그룹 + ";" + 검색식 + ";" + 시간 + ";" + GET.ScreenNum() + ";" + 실시간등록;
                            if (Form1.form1.CB_실시간관심등록.Checked)
                            {
                                시간 = "실시간";
                                para = 관심그룹 + ";" + 검색식 + ";" + 시간 + ";" + "1100" + ";" + 실시간등록;
                            }
                            Form1.form1.Invoke((MethodInvoker)delegate ()
                            {
                                using (new CenterWinDialog(Form1.form1))
                                    if (MessageBox.Show("검색식(" + 검색식 + ")을 시간(" + 시간 + ")에 검색하고 검색된 종목을 관심그룹(" + 관심그룹 + ")에 자동등록. 위 조건을 '등록' 하시겠습니까?", "자동등록알림", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        Form1.Interest_AutoAdd_List.Add(para);

                                        관심자동등록리스트확인();
                                        save_관심그룹_Title_기록();

                                        관심실시간자동등록(true);
                                    }
                            });
                        });
                        thread.Start();
                    }
                    else
                    {
                        Form1.알림창("[ 자동등록 중복 ]\n\n검색식(" + 검색식 + ")을 시간(" + 시간 + ")에 검색하고 검색된 종목을\n\n관심그룹(" + 관심그룹 + ")에 자동등록 합니다. 위 조건이 이미등록 되어 있습니다.", 20, false);
                    }
                }
            }
        }

        public static void BT_자동해제_Click()
        {
            if (Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex == 2)
            {
                if (Form1.form1.LB_검색결과n관심리스트.SelectedItems.Count > 0)
                {
                    for (int i = Form1.form1.LB_검색결과n관심리스트.SelectedItems.Count; i > 0; i--)
                    {
                        string item = Form1.form1.LB_검색결과n관심리스트.SelectedItems[i - 1].ToString();

                        if (item.Contains("ㆍ"))
                        {
                            검색식해제(item.Split('ㆍ')[1].Trim(), item.Split('ㆍ')[2].Trim(), item.Split('ㆍ')[3].Trim());
                        }
                    }
                }

                void 검색식해제(string 관심그룹, string 검색식, string 시간)
                {
                    var thread = new Thread(
                      () =>
                      {
                          Form1.form1.Invoke((MethodInvoker)delegate ()
                          {
                              string para = 관심그룹 + ";" + 검색식 + ";" + 시간;

                              using (new CenterWinDialog(Form1.form1))
                                  if (MessageBox.Show("검색식(" + 검색식 + ")을 시간(" + 시간 + ")에 검색하고 검색된 종목을 관심그룹(" + 관심그룹 + ")에 자동등록. 위 조건을 '해제' 하시겠습니까?", "자동등록해제", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                  {
                                      string 삭제 = Form1.Interest_AutoAdd_List.Find(o => o.StartsWith(para));
                                      if (삭제 != null)
                                      {
                                          Form1.Interest_AutoAdd_List.Remove(삭제);
                                      }

                                      if (시간.Equals("실시간") && Form1.로딩완료)
                                      {
                                          if (Form1.Run_condition_List.Contains(검색식))
                                          {
                                              Condition condition = Form1.form1.ConditionList.Find(o => o.name.Equals(검색식));
                                              if (condition != null)
                                              {
                                                  Form1.form1.Interest_condition_List.Remove(condition.name);
                                                  Condition_Management.Stop_Monitoring(condition, "관심그룹");

                                                  Form1.AutoClosingAlram("[관심그룹] 검색식 해제 " + 검색식 + " 의 실시간감시를 해제 하고 그룹(" + 관심그룹 + ")에 자동등록을 중지 합니다.", "동작알림", 10, "동작");
                                              }
                                          }
                                      }

                                      관심자동등록리스트확인();
                                      save_관심그룹_Title_기록();

                                      if (Form1.form1.LB_검색결과n관심리스트.Items.Count > 32)
                                          Form1.form1.LB_검색결과n관심리스트.SelectedIndex = 31;

                                  }
                          });
                      });
                    thread.Start();
                }
            }
        }

        public static void 관심자동등록실행()
        {
            int time_기록 = 0;
            string 검색식_기록 = "";
            string 검색식 = "";

            for (int i = 0; i < Form1.Interest_AutoAdd_List.Count; i++)
            {
                검색식 = Form1.Interest_AutoAdd_List[i].Split(';')[1];
                bool time_실행 = int.TryParse(Form1.Interest_AutoAdd_List[i].Split(';')[2], out int 실행_시간);
                string 스크린넘버 = Form1.Interest_AutoAdd_List[i].Split(';')[3];

                if (Form1.timenow.Equals(실행_시간))
                {
                    if (time_기록 != 실행_시간)
                    {
                        if (검색식 != 검색식_기록)
                        {
                            실행();
                        }
                        else
                        {
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

                    void 실행()
                    {
                        Form1.form1.Invoke((MethodInvoker)delegate ()
                        {
                            검색요청(스크린넘버, 검색식);

                            time_기록 = 실행_시간;
                            검색식_기록 = 검색식;
                        });
                    }
                }
            }

            if (Form1.로딩완료 && Form1.form1.fail_condition_List.Count > 0)
            {
                for (int n = 0; n < Form1.Interest_AutoAdd_List.Count; n++)
                {
                    검색식 = Form1.Interest_AutoAdd_List[n].Split(';')[1];

                    Fail_condition fail = Form1.form1.fail_condition_List.Find(o => o.condition.Equals(검색식));
                    if (fail != null)
                    {
                        Form1.form1.재가동검색식 = fail.condition;
                    }
                }
            }

            if (Form1.form1.재가동검색식 != null && !Form1.Run_condition_List.Contains(Form1.form1.재가동검색식))
            {
                Condition condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Form1.form1.재가동검색식));
                if (condition != null)
                {
                    Fail_condition fail = Form1.form1.fail_condition_List.Find(o => o.condition.Equals(Form1.form1.재가동검색식));
                    if (fail == null)
                    {
                        if (Form1.Run_condition_List.Contains(condition.name))
                        {
                            Form1.AutoClosingAlram("중복알림  검색식 [ " + condition.name + " ]이 실시간 감시 중 입니다.", "에러알림", 10, "에러");
                        }
                        else
                        {
                            Condition_Management.Start_Monitoring(condition, "관심그룹");
                        }

                        Form1.form1.재가동검색식 = null;
                    }
                }
            }
        }

        public static void 관심실시간자동등록(bool 신규)
        {
            for (int i = 0; i < Form1.Interest_AutoAdd_List.Count; i++)
            {
                if (신규)
                {
                    if (i + 1 == Form1.Interest_AutoAdd_List.Count)
                        실시간등록();
                }
                else
                {
                    실시간등록();
                }

                void 실시간등록()
                {
                    string 검색식 = Form1.Interest_AutoAdd_List[i].Split(';')[1];

                    if (Form1.Interest_AutoAdd_List[i].Split(';')[2].Contains("실시간"))
                    {
                        Condition condition = Form1.form1.ConditionList.Find(o => o.name.Equals(검색식));
                        if (condition != null)
                        {
                            if (Form1.Run_condition_List.Count < 10 || Form1.Run_condition_List.Contains(검색식)) // 개수제한 10 
                            {
                                if (!Form1.Run_condition_List.Contains(검색식))
                                {
                                    Fail_condition fail = Form1.form1.fail_condition_List.Find(o => o.condition.Equals(condition.name));
                                    if (fail == null)
                                    {
                                        if (Form1.Run_condition_List.Contains(condition.name))
                                        {
                                            Form1.AutoClosingAlram("중복알림  검색식 [ " + condition.name + " ]이 실시간 감시 중 입니다.", "에러알림", 10, "에러");
                                        }
                                        else
                                        {
                                            Condition_Management.Start_Monitoring(condition, "관심그룹");
                                        }

                                        등록();
                                    }

                                    if (condition.name.Equals(Properties.Settings.Default.TB_매수탐색A) || condition.name.Equals(Properties.Settings.Default.TB_매수탐색B) || condition.name.Equals(Properties.Settings.Default.TB_매도탐색))
                                    {
                                        등록();
                                    }
                                }
                                else
                                {
                                    등록();
                                }

                                void 등록()
                                {
                                    Form1.form1.Interest_condition_List.Add(condition.name);
                                    Form1.알림창("[ 관심그룹 ]\n\n검색식등록 알림 " + 검색식 + " 을 실시간 감시하여 \n\n" + Form1.Interest_AutoAdd_List[i].Split(';')[0] + " 그룹에 실시간 등록 합니다.", 10, false);
                                    Form1.동작_Log("[관심그룹] 검색식등록 알림 " + 검색식 + " 을 실시간 감시하여 " + Form1.Interest_AutoAdd_List[i].Split(';')[0] + " 그룹에 실시간 등록 합니다.");
                                    string para = Form1.Interest_AutoAdd_List[i].Split(';')[0] + ";" + 검색식 + ";" + "실시간" + ";" + Form1.Interest_AutoAdd_List[i].Split(';')[3] + ";" + Form1.Interest_AutoAdd_List[i].Split(';')[4];
                                    Form1.Interest_AutoAdd_List[i] = para;
                                    save_관심그룹_Title_기록();

                                }
                            }
                            else
                            {
                                Form1.알림창("[ 관심그룹 ]\n\n검색식등록 실패 가동검색식이 10개를 넘어 (" + 검색식 + ")\n\n을 가동할수 없습니다.", 10, false);
                                Form1.동작_Log("[관심그룹] 검색식등록 실패 가동검색식이 10개를 넘어 (" + 검색식 + ") 을 가동할수 없습니다.");
                                string para = Form1.Interest_AutoAdd_List[i].Split(';')[0] + ";" + 검색식 + ";" + "실시간(개수초과)" + ";" + Form1.Interest_AutoAdd_List[i].Split(';')[3] + ";" + Form1.Interest_AutoAdd_List[i].Split(';')[4];
                                Form1.Interest_AutoAdd_List[i] = para;
                                save_관심그룹_Title_기록();
                            }
                        }
                        else
                        {
                            Form1.알림창("[ 관심그룹 ]\n\n검색식등록 실패 검색식이 찾을 수 없어 (" + 검색식 + ")\n\n실시간 감시 할수 없습니다.", 10, false);
                            Form1.동작_Log("[관심그룹] 검색식등록 실패 검색식이 찾을 수 없어 (" + 검색식 + ") 실시간 감시 할수 없습니다.");

                            string para = Form1.Interest_AutoAdd_List[i].Split(';')[0] + ";" + 검색식 + ";" + "식없음" + ";" + Form1.Interest_AutoAdd_List[i].Split(';')[3] + ";" + Form1.Interest_AutoAdd_List[i].Split(';')[4];
                            Form1.Interest_AutoAdd_List[i] = para;
                            save_관심그룹_Title_기록();
                        }
                    }
                }
            }
        }

        public static void BT_자동삭제_Click()
        {
            if (Form1.form1.CBB_그룹자동삭제.SelectedIndex == 0)
            {
                if (Form1.form1.LB_검색결과n관심리스트.SelectedItems.Count > 0)
                {
                    for (int i = Form1.form1.LB_검색결과n관심리스트.SelectedItems.Count; i > 0; i--)
                    {
                        string item = Form1.form1.LB_검색결과n관심리스트.SelectedItems[i - 1].ToString();

                        if (item.Contains("ㆍ") && (item.Split('ㆍ')[3].Trim().Equals("종목삭제") || item.Split('ㆍ')[3].Trim().Equals("이탈삭제")))
                        {
                            자동삭제해제(item.Split('ㆍ')[1].Trim(), item.Split('ㆍ')[2].Trim(), item.Split('ㆍ')[3].Trim());
                        }
                    }
                }

                void 자동삭제해제(string 기간, string 관심그룹, string 조건)
                {
                    var thread = new Thread(
                     () =>
                     {
                         Form1.form1.Invoke((MethodInvoker)delegate ()
                         {
                             using (new CenterWinDialog(Form1.form1))
                                 if (MessageBox.Show("자동삭제해제\n관심그룹(" + 관심그룹 + "), 조건(" + 조건 + "), 기간(" + 기간 + ")인 자동삭제 조건을 '해제' 하시겠습니까?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                 {
                                     string para = 관심그룹 + ";" + 조건 + ";" + 기간 + ";" + "1100";

                                     string 삭제 = Form1.Interest_AutoAdd_List.Find(o => o.StartsWith(para));
                                     if (삭제 != null)
                                         Form1.Interest_AutoAdd_List.Remove(삭제);

                                     Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 2;
                                     CBB_실시간n그룹n관심자동_indexchange(2);
                                 }

                             관심자동등록리스트확인();
                             save_관심그룹_Title_기록();

                             Form1.form1.LB_검색결과n관심리스트.SelectedIndex = Form1.form1.LB_검색결과n관심리스트.Items.Count - 1;
                         });
                     });
                    thread.Start();
                }
            }
            else if (Form1.form1.CBB_그룹자동삭제.SelectedIndex == 1)
            {
                int.TryParse(Form1.form1.MTB_자동삭제기간.Text, out int 기간);
                if (기간 == 0) { 기간 = 1; Form1.form1.MTB_자동삭제기간.Text = 기간.ToString(); }
                string 관심그룹 = Form1.form1.CBB_관심그룹.Text;
                string 검색식 = "종목삭제";
                string 중복확인 = 관심그룹 + ";" + 검색식 + ";" + 기간.ToString() + ";" + "1100";

                if (Form1.Interest_AutoAdd_List.Exists(o => o.StartsWith(중복확인)))
                {
                    Form1.AutoClosingAlram("[관심그룹] 자동삭제 중복  기간(" + 기간.ToString() + ") 일 이 지난 종목을 관심그룹(" + 관심그룹 + ")에서 자동삭제 합니다. 위 조건이 이미 '등록'되어 있습니다.", "에러알림", 10, "에러");
                }
                else
                {
                    var thread = new Thread(
                    () =>
                    {
                        Form1.form1.Invoke((MethodInvoker)delegate ()
                        {
                            string para = 관심그룹 + ";" + 검색식 + ";" + 기간.ToString() + ";" + "1100" + ";" + Form1.form1.CB_시세감시등록.Checked.ToString();
                            using (new CenterWinDialog(Form1.form1))
                                if (MessageBox.Show("자동삭제등록\n기간(" + 기간.ToString() + ") 일 이 지난 종목을 관심그룹(" + 관심그룹 + ")에서 자동삭제 합니다.\n 위 조건을 '등록' 하시겠습니까 ?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    Form1.Interest_AutoAdd_List.Add(para);

                                    관심자동등록리스트확인();
                                    save_관심그룹_Title_기록();
                                    Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 2;
                                    CBB_실시간n그룹n관심자동_indexchange(2);
                                }
                        });
                    });
                    thread.Start();
                }
            }
            else if (Form1.form1.CBB_그룹자동삭제.SelectedIndex == 2)
            {
                string 기간 = "0";
                string 관심그룹 = Form1.form1.CBB_관심그룹.Text;
                string 검색식 = "이탈삭제";
                string 중복확인 = 관심그룹 + ";" + 검색식 + ";" + 기간 + ";" + "1100";

                if (Form1.Interest_AutoAdd_List.Exists(o => o.StartsWith(중복확인)))
                {
                    Form1.AutoClosingAlram("[관심그룹] 자동삭제 중복  기간(" + 기간 + ") 일 이 지난 종목을 관심그룹(" + 관심그룹 + ")에서 자동삭제 합니다. 위 조건이 이미 '등록'되어 있습니다.", "에러알림", 10, "에러");
                }
                else
                {
                    var thread = new Thread(
                    () =>
                    {
                        Form1.form1.Invoke((MethodInvoker)delegate ()
                        {
                            string para = 관심그룹 + ";" + 검색식 + ";" + 기간 + ";" + "1100" + ";" + Form1.form1.CB_시세감시등록.Checked.ToString();

                            using (new CenterWinDialog(Form1.form1))
                                if (MessageBox.Show("자동삭제등록\n검색 이탈된 종목을 관심그룹(" + 관심그룹 + ")에서 자동삭제 합니다.\n 위 조건을 '등록' 하시겠습니까 ?", "저장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    Form1.Interest_AutoAdd_List.Add(para);
                                    Form1.form1.이탈삭제 = Form1.form1.이탈삭제 + 관심그룹 + "^";

                                    관심자동등록리스트확인();
                                    save_관심그룹_Title_기록();

                                    Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 2;
                                    CBB_실시간n그룹n관심자동_indexchange(2);
                                }
                        });
                    });
                    thread.Start();
                }
            }
        }

        public static void 자동삭제실행()
        {
            for (int i = 0; i < Form1.Interest_AutoAdd_List.Count; i++)
            {
                string[] list = Form1.Interest_AutoAdd_List[i].Split(';');

                int.TryParse(list[2], out int 기간);

                if (list[1].Equals("종목삭제") && 기간 > 0)
                {
                    string 관심그룹 = list[0];

                    List<Interest_stock> 관종_list = Form1.Interest_stock_List.FindAll(o => o.Title.Equals(관심그룹));

                    for (int n = 0; n < 관종_list.Count; n++)
                    {
                        Interest_stock 관종 = 관종_list[n];
                        if (관심그룹.Equals(관종.Title))
                        {
                            DateTime today = DateTime.Parse(DateTime.Now.ToShortDateString());
                            DateTime day = DateTime.Parse(관종.date);
                            TimeSpan ts = today.Date - day.Date;

                            if (ts.Days >= 기간)
                            {
                                Form1.Interest_stock_List.Remove(관종);
                            }
                        }
                    }
                }

                if (list[1].Equals("이탈삭제") && 기간 == 0)
                {
                    if (Form1.form1.이탈삭제.Length == 0)
                    {
                        Form1.form1.이탈삭제 = list[0] + "^";
                    }
                    else
                    {
                        Form1.form1.이탈삭제 = Form1.form1.이탈삭제 + list[0] + "^";
                    }
                }
            }

            save_관심그룹_Title_기록();
            save_관심그룹_List_기록();

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
            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                if (Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex == 0)
                {
                    Form1.form1.LB_검색결과n관심리스트.BeginUpdate();
                    Form1.form1.LB_검색결과n관심리스트.Items.Clear();
                    Form1.form1.LB_검색결과n관심리스트.Items.Add("0 시간: " + Form1.form1.검색결과_List[0]);
                    Form1.form1.LB_검색결과n관심리스트.Items.Add("0 검색식 : " + Form1.form1.검색결과_List[1]);
                    Form1.form1.LB_검색결과n관심리스트.Items.Add("0 검색개수 : " + (Form1.form1.검색결과_List.Count - 2));
                    Form1.form1.LB_검색결과n관심리스트.Items.Add("0 -----------------------------");

                    for (int i = 2; i < Form1.form1.검색결과_List.Count; i++)
                    {
                        if (Form1.Market_Item_List.TryGetValue(Form1.form1.검색결과_List[i], out Market_Item Market))
                        {
                            Form1.form1.LB_검색결과n관심리스트.Items.Add((Form1.form1.LB_검색결과n관심리스트.Items.Count - 3) + "ㆍ" + Market.Market + "ㆍ" + Market.종목명 + "ㆍ" + Market.현재가.ToString("N0"));
                        }
                    }

                    Form1.form1.LB_검색결과n관심리스트.EndUpdate();
                }
            });
        }

        public static void 관심검색_실시간보기(string 검색식, string ID, string code)
        {
            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                if (Form1.form1.CB_실시간검색결과보기.Checked)
                {
                    if (ID.Equals("I"))
                    {
                        if (Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex == 0)
                        {
                            for (int i = 0; i < Form1.Interest_AutoAdd_List.Count; i++)
                            {
                                string[] auto = Form1.Interest_AutoAdd_List[i].Split(';');
                                string 관심식 = auto[1];

                                //para = 관심그룹 + ";" + 검색식 + ";" + 시간 + ";" + "1100" + ";" + 실시간등록;

                                if (관심식.Equals(검색식) && auto[2].Equals("실시간"))
                                {
                                    if (ID.Equals("I"))
                                    {
                                        if (code.Contains(";"))
                                        {
                                            string[] list = code.Split(';');
                                            for (int n = 0; n < list.Length - 1; n++)
                                            {
                                                실시간보기(list[n]);
                                            }
                                        }
                                        else
                                        {
                                            실시간보기(code);
                                        }
                                    }
                                }
                            }

                            void 실시간보기(string 코드)
                            {
                                Form1.form1.LB_검색결과n관심리스트.BeginUpdate();
                                if (Form1.Market_Item_List.TryGetValue(코드, out Market_Item Market))
                                {
                                    bool 추가 = true;
                                    for (int i = 0; i < Form1.form1.LB_검색결과n관심리스트.Items.Count; i++)
                                    {
                                        if (Form1.form1.LB_검색결과n관심리스트.Items[i].ToString().Contains('ㆍ'))
                                        {
                                            string[] str = Form1.form1.LB_검색결과n관심리스트.Items[i].ToString().Split('ㆍ');

                                            if (!str[0].Contains(':')) Form1.form1.LB_검색결과n관심리스트.Items.RemoveAt(i);
                                            else
                                            {
                                                if (str[1].Equals(검색식) && str[2].Equals(Market.종목명)) 추가 = false;
                                            }
                                        }
                                        else
                                        {
                                            Form1.form1.LB_검색결과n관심리스트.Items.RemoveAt(i);
                                        }
                                    }
                                    if (추가)
                                    {
                                        if (Form1.form1.LB_검색결과n관심리스트.Items.Count == 0) Form1.form1.LB_검색결과n관심리스트.Items.Add(DateTime.Now.ToString("HH:mm:ssㆍ") + Market.Market + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                                        else Form1.form1.LB_검색결과n관심리스트.Items.Insert(0, DateTime.Now.ToString("HH:mm:ssㆍ") + Market.Market + "ㆍ" + Market.종목명 + "ㆍ" + 검색식);
                                    }
                                }
                                int index = 36;
                                if (Form1.form1.CBB_layout.SelectedIndex == 0 || Form1.form1.CBB_layout.SelectedIndex == 3) index = 36;
                                if (Form1.form1.CBB_layout.SelectedIndex == 1 || Form1.form1.CBB_layout.SelectedIndex == 2) index = 62;
                                if (Form1.form1.CBB_layout.SelectedIndex == 4) index = 42;
                                if (Form1.form1.CB_기본매매.Checked || Form1.form1.CB_반복매매.Checked || Form1.form1.CB_계좌관리.Checked || Form1.form1.CB_특수매매.Checked || Form1.form1.CB_대금탐색.Checked || Form1.form1.CB_매매그룹.Checked || Form1.form1.CB_기능설정.Checked) index = 36;

                                while (Form1.form1.LB_검색결과n관심리스트.Items.Count > index)
                                {
                                    Form1.form1.LB_검색결과n관심리스트.Items.RemoveAt(Form1.form1.LB_검색결과n관심리스트.Items.Count - 1);
                                }
                                Form1.form1.LB_검색결과n관심리스트.EndUpdate();
                            }
                        }
                    }
                }
            });
        }


        public static void 관심_검색종목_등록실행(string sScrNo, string strCodeList, string strConditionName, string type)
        {
            if (Form1.Interest_AutoAdd_List.Count > 0) // 실시간 등록 
            {
                Form1.form1.Invoke((MethodInvoker)delegate ()
                {
                    int 실행시간_기록 = 0;
                    string 검색식_기록 = "";

                    for (int i = 0; i < Form1.Interest_AutoAdd_List.Count; i++)
                    {
                        string[] auto = Form1.Interest_AutoAdd_List[i].Split(';');
                        string 관심그룹 = auto[0];
                        string 검색식 = auto[1];
                        bool 시간 = int.TryParse(auto[2], out int 실행시간);
                        string 스크린 = auto[3];
                        bool.TryParse(auto[4], out bool 실시간등록);

                        //para = 관심그룹 + ";" + 검색식 + ";" + 시간 + ";" + "1100" + ";" + 실시간등록;

                        if (시간)
                        {
                            if (type.Equals("I") && !검색식.Equals("이탈삭제") && !검색식.Equals("종목삭제"))
                            {
                                if (sScrNo.Equals(스크린) && strConditionName.Equals(검색식))
                                {
                                    string[] list = strCodeList.Split(';');
                                    for (int n = 0; n < list.Length - 1; n++)
                                    {
                                        관종등록(list[n]);
                                    }

                                    실행시간_기록 = 실행시간;
                                    검색식_기록 = 검색식;
                                }

                                if (실행시간_기록 <= 실행시간 && 실행시간 <= (실행시간_기록 + 100) && 검색식 == 검색식_기록)
                                {
                                    string[] list = strCodeList.Split(';');
                                    for (int n = 0; n < list.Length - 1; n++)
                                    {
                                        관종등록(list[n]);
                                    }
                                }

                                Form1.form1.CBB_실시간n그룹n관심자동.SelectedIndex = 0;
                                CBB_실시간n그룹n관심자동_indexchange(0);
                            }
                        }
                        else
                        {
                            if (strConditionName.Equals(검색식) && auto[2].Equals("실시간"))
                            {
                                if (type.Equals("I"))
                                {
                                    if (strCodeList.Contains(";"))
                                    {
                                        string[] list = strCodeList.Split(';');
                                        for (int n = 0; n < list.Length - 1; n++)
                                        {
                                            관종등록(list[n]);
                                        }
                                    }
                                    else
                                    {
                                        관종등록(strCodeList);
                                    }
                                }
                                else
                                {
                                    관종삭제(strCodeList);
                                }
                            }
                        }

                        void 관종등록(string code)
                        {
                            if (Form1.Market_Item_List.ContainsKey(code))
                            {
                                Interest_stock item = Form1.Interest_stock_List.Find(o => o.Title.Equals(관심그룹) && o.Code.Equals(code));
                                if (item == null)
                                {
                                    string ItemCode = code;
                                    Interest_stock 추가 = new Interest_stock(code, Form1.Market_Item_List[code].종목명, Form1.today, 관심그룹, 실시간등록);
                                    Form1.Interest_stock_List.Add(추가);

                                    if (실시간등록) Method.실시간시세등록(code);
                                }
                            }
                        }

                        void 관종삭제(string code)
                        {
                            if (Form1.form1.이탈삭제.Contains(관심그룹))
                            {
                                Interest_stock I_stock = Form1.Interest_stock_List.Find(o => o.Title.Equals(관심그룹) && o.Code.Equals(code));
                                if (I_stock != null)
                                {
                                    Form1.Interest_stock_List.Remove(I_stock);

                                    Form1.form1.Invoke((MethodInvoker)delegate ()
                                    {
                                        Form1.form1.LB_관심_A.Items.Clear();
                                        Form1.form1.LB_관심_B.Items.Clear();
                                        Form1.form1.LB_관심_C.Items.Clear();
                                    });
                                }
                            }
                        }
                    }

                    save_관심그룹_List_기록();

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
