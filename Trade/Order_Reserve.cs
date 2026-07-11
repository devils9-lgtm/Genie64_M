using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 지니64.RESTAPI;

namespace 지니64
{
    public class Order_Reserve : Form1
    {
        public static int Get_예약번호()
        {
            int 예약번호 = 10000;

            for (int i = 10000; i > 1; i++)
            {
                주문예약 result = Form1.form1.주문예약_List.Find(o => o.예약번호.Equals(i));
                if (result == null)
                {
                    예약번호 = i;
                    break;
                }
            }
            return 예약번호;
        }

        public static void CBB_예약종류_SelectedIndexChanged()
        {
            if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(0))
            {
                if (!Form_Special.form.TB_예약주문_주문비.Text.StartsWith("-"))
                {
                    Form_Special.form.TB_예약주문_주문비.Text = "-" + Form_Special.form.TB_예약주문_주문비.Text;
                }

                Form_Special.form.LB_예약리스트.BeginUpdate();
                Form_Special.form.LB_예약리스트.Items.Clear();
                Form_Special.form.LB_예약리스트.Items.Add("장전 ' 매수 ' 예약");
                리스트확인("장전'매수'");
            }

            if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(1))
            {
                if (Form_Special.form.TB_예약주문_주문비.Text.StartsWith("-"))
                {
                    Form_Special.form.TB_예약주문_주문비.Text = Form_Special.form.TB_예약주문_주문비.Text.Substring(1);
                }

                Form_Special.form.LB_예약리스트.BeginUpdate();
                Form_Special.form.LB_예약리스트.Items.Clear();
                Form_Special.form.LB_예약리스트.Items.Add("장전 ' 매도 ' 예약");
                리스트확인("장전'매도'");
            }

            if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(2))
            {
                if (!Form_Special.form.TB_예약주문_주문비.Text.StartsWith("-"))
                {
                    Form_Special.form.TB_예약주문_주문비.Text = "-" + Form_Special.form.TB_예약주문_주문비.Text;
                }

                Form_Special.form.LB_예약리스트.BeginUpdate();
                Form_Special.form.LB_예약리스트.Items.Clear();
                Form_Special.form.LB_예약리스트.Items.Add("종가 ' 매수 ' 예약");
                리스트확인("종가'매수'");
            }
            if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(3))
            {
                if (Form_Special.form.TB_예약주문_주문비.Text.StartsWith("-"))
                {
                    Form_Special.form.TB_예약주문_주문비.Text = Form_Special.form.TB_예약주문_주문비.Text.Substring(1);
                }

                Form_Special.form.LB_예약리스트.BeginUpdate();
                Form_Special.form.LB_예약리스트.Items.Clear();
                Form_Special.form.LB_예약리스트.Items.Add("종가 ' 매도 ' 예약");
                리스트확인("종가'매도'");
            }
            if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(4))
            {
                if (!Form_Special.form.TB_예약주문_주문비.Text.StartsWith("-"))
                {
                    Form_Special.form.TB_예약주문_주문비.Text = "-" + Form_Special.form.TB_예약주문_주문비.Text;
                }

                Form_Special.form.LB_예약리스트.BeginUpdate();
                Form_Special.form.LB_예약리스트.Items.Clear();
                Form_Special.form.LB_예약리스트.Items.Add("신규 ' 매수 ' 예약");
                리스트확인("신규'매수'");
            }

            void 리스트확인(string 위치)
            {

                if (Form1.form1.주문예약_List.Count > 0)
                {
                    for (int i = 0; i < Form1.form1.주문예약_List.Count; i++)
                    {
                        주문예약 주문 = Form1.form1.주문예약_List[i];

                        if (주문.검색식.Contains(위치))
                        {
                            Market_Item Market = Form1.Market_Item_List[주문.종목코드];
                            int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);

                            int 기준가 = 현재가;
                            if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex == 0 || Form_Special.form.CBB_예약주문_예약종류.SelectedIndex == 1)
                            {
                                기준가 = Market.Last_price;
                            }

                            Form_Special.form.LB_예약리스트.Items.Add(주문.예약번호 + " " + 주문.종목명 + " :: 등록일: " + 주문.등록일 + " " + GET.매수매도str(주문.매수매도) + " 주문가격: " + 주문.주문가 + " 주문수량: " + 주문.주문수량 + " 체결수량: " + 주문.체결수량 + " 주문비: " + Math.Round((주문.주문가 - 기준가) / (double)주문.주문가 * 100, 2));
                            Form_Special.form.LB_예약리스트.Items.Add("              :: 종가연동: " + 주문.연동 + " 체결완료삭제: " + 주문.체결완료삭제 + " 전량매도삭제: " + 주문.전량매도삭제 + " " + 주문.검색식);
                        }
                    }
                    Form_Special.form.LB_예약리스트.SelectedIndex = 0;
                }
                Form_Special.form.LB_예약리스트.EndUpdate();
            }
        }

        public static void BT_Click_예약주문추가_(object sender)
        {
            Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(Form_Special.form.TB_예약주문_종목명.Text.Trim())).Value;
            if (Market != null)
            {
                Stockbalance 잔고 = Form1.stockBalanceList.FirstOrDefault(o => o.Value.종목코드.Equals(Market.종목코드)).Value;
                if (잔고 == null)
                {
                    if (sender.Equals(Form_Special.form.BT_예약주문_장전매수추가))
                    {
                        if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(4))
                        {
                            if (Form_Special.form.CBB_예약주문_장전매수선택.SelectedIndex > 2)
                            {
                                Form1.AutoClosingAlram("현재 선택된 비중은 신규매수 에는 사용할수 없는 비중입니다.", "비중오류", 10, "동작");
                                Form_Special.form.CBB_예약주문_장전매수선택.SelectedIndex = 1;
                                return;
                            }

                            double.TryParse(Form_Special.form.TB_예약주문_장전매수비중.Text, out double 장전매수비중);
                            Form_Special.form.TB_예약주문_장전매수비중.Text = 장전매수비중.ToString();

                            if (장전매수비중 > 0)
                            {
                                if (Form_Special.form.TB_예약주문_주문가.Text.Trim().Length == 0) 예약종목선택(Market);

                                string 위치 = "신규'매수'장전";
                                double 비중 = 장전매수비중;
                                int 선택 = Form_Special.form.CBB_예약주문_장전매수선택.SelectedIndex;
                                int 주문가 = int.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                                int 주문수량 = Method.주문수량계산(잔고, 주문가, 장전매수비중, Form_Special.form.CBB_예약주문_장전매수선택.SelectedIndex);
                                double 주문비 = double.Parse(Form_Special.form.TB_예약주문_주문비계산값.Text);

                                예약실행(위치, 비중, 선택, 주문가, 주문수량, 주문비, Form_Special.form.CB_예약주문_장전연동.Checked, Form_Special.form.CB_예약주문_장전체결삭제.Checked, Form_Special.form.CB_예약주문_장전전량매도삭제.Checked);
                            }
                            else
                            {
                                Form1.AutoClosingAlram("예약주문 비중 이 1 보다 작습니다.", "비중오류", 10, "에러");
                            }
                        }
                        else
                        {
                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 4;
                        }
                    }

                    if (sender.Equals(Form_Special.form.BT_예약주문_종가매수추가))
                    {
                        if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(4))
                        {
                            if (Form_Special.form.CBB_예약주문_종가매수선택.SelectedIndex > 2)
                            {
                                Form1.AutoClosingAlram("현재 선택된 비중은 신규매수 에는 사용할수 없는 비중입니다.", "비중오류", 10, "동작");
                                Form_Special.form.CBB_예약주문_종가매수선택.SelectedIndex = 1;
                                return;
                            }

                            double.TryParse(Form_Special.form.TB_예약주문_종가매수비중.Text, out double 종가매수비중);
                            Form_Special.form.TB_예약주문_종가매수비중.Text = 종가매수비중.ToString();

                            if (종가매수비중 > 0)
                            {
                                if (Form_Special.form.TB_예약주문_주문가.Text.Trim().Length == 0) 예약종목선택(Market);

                                string 위치 = "신규'매수'종가";
                                double 비중 = 종가매수비중;
                                int 선택 = Form_Special.form.CBB_예약주문_종가매수선택.SelectedIndex;
                                int 주문가 = int.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                                int 주문수량 = Method.주문수량계산(잔고, 주문가, 종가매수비중, Form_Special.form.CBB_예약주문_종가매수선택.SelectedIndex);
                                double 주문비 = double.Parse(Form_Special.form.TB_예약주문_주문비계산값.Text);

                                예약실행(위치, 비중, 선택, 주문가, 주문수량, 주문비, Form_Special.form.CB_예약주문_종가연동.Checked, Form_Special.form.CB_예약주문_종가체결삭제.Checked, Form_Special.form.CB_예약주문_종가전량매도삭제.Checked);
                            }
                            else
                            {
                                Form1.AutoClosingAlram("예약주문 비중 이 1 보다 작습니다.", "비중오류", 10, "에러");
                            }
                        }
                        else
                        {
                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 4;
                        }
                    }

                    if (sender.Equals(Form_Special.form.BT_예약주문_장전매도추가) || sender.Equals(Form_Special.form.BT_예약주문_종가매도추가))
                    {
                        Form1.AutoClosingAlram("잔고알림  " + Market.종목명 + " 이 잔고에 없어 매도할수 없습니다.", "잔고알림", 10, "에러");
                    }
                }
                else
                {
                    if (sender.Equals(Form_Special.form.BT_예약주문_장전매수추가))
                    {
                        if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(0))
                        {
                            double.TryParse(Form_Special.form.TB_예약주문_장전매수비중.Text, out double 장전매수비중);
                            Form_Special.form.TB_예약주문_장전매수비중.Text = 장전매수비중.ToString();

                            if (장전매수비중 > 0)
                            {
                                if (Form_Special.form.TB_예약주문_주문가.Text.Trim().Length == 0) 예약종목선택(Market);

                                string 위치 = "장전'매수'";
                                double 비중 = 장전매수비중;
                                int 선택 = Form_Special.form.CBB_예약주문_장전매수선택.SelectedIndex;
                                int 주문가 = int.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                                int 주문수량 = Method.주문수량계산(잔고, 주문가, 장전매수비중, Form_Special.form.CBB_예약주문_장전매수선택.SelectedIndex);
                                double 주문비 = double.Parse(Form_Special.form.TB_예약주문_주문비계산값.Text);

                                예약실행(위치, 비중, 선택, 주문가, 주문수량, 주문비, Form_Special.form.CB_예약주문_장전연동.Checked, Form_Special.form.CB_예약주문_장전체결삭제.Checked, Form_Special.form.CB_예약주문_장전전량매도삭제.Checked);
                            }
                            else
                            {
                                Form1.AutoClosingAlram("예약주문 비중 이 1 보다 작습니다.", "비중오류", 10, "에러");
                            }
                        }
                        else
                        {
                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 0;
                        }
                    }

                    if (sender.Equals(Form_Special.form.BT_예약주문_장전매도추가))
                    {
                        if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(1))
                        {
                            double.TryParse(Form_Special.form.TB_예약주문_장전매도비중.Text, out double 장전매도비중);
                            Form_Special.form.TB_예약주문_장전매도비중.Text = 장전매도비중.ToString();

                            if (장전매도비중 > 0)
                            {
                                if (Form_Special.form.TB_예약주문_주문가.Text.Trim().Length == 0) 예약종목선택(Market);

                                string 위치 = "장전'매도'";
                                double 비중 = 장전매도비중;
                                int 선택 = Form_Special.form.CBB_예약주문_장전매도선택.SelectedIndex;
                                int 주문가 = int.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                                int 주문수량 = Method.주문수량계산(잔고, 주문가, 장전매도비중, Form_Special.form.CBB_예약주문_장전매도선택.SelectedIndex);
                                double 주문비 = double.Parse(Form_Special.form.TB_예약주문_주문비계산값.Text);

                                예약실행(위치, 비중, 선택, 주문가, 주문수량, 주문비, Form_Special.form.CB_예약주문_장전연동.Checked, Form_Special.form.CB_예약주문_장전체결삭제.Checked, Form_Special.form.CB_예약주문_장전전량매도삭제.Checked);
                            }
                            else
                            {
                                Form1.AutoClosingAlram("예약주문 비중 이 1 보다 작습니다.", "비중오류", 10, "에러");
                            }
                        }
                        else
                        {
                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 1;
                        }
                    }

                    if (sender.Equals(Form_Special.form.BT_예약주문_종가매수추가))
                    {
                        if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(2))
                        {
                            double.TryParse(Form_Special.form.TB_예약주문_종가매수비중.Text, out double 종가매수비중);
                            Form_Special.form.TB_예약주문_종가매수비중.Text = 종가매수비중.ToString();

                            if (종가매수비중 > 0)
                            {
                                if (Form_Special.form.TB_예약주문_주문가.Text.Trim().Length == 0) 예약종목선택(Market);

                                string 위치 = "종가'매수'";
                                double 비중 = 종가매수비중;
                                int 선택 = Form_Special.form.CBB_예약주문_종가매수선택.SelectedIndex;
                                int 주문가 = int.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                                int 주문수량 = Method.주문수량계산(잔고, 주문가, 종가매수비중, Form_Special.form.CBB_예약주문_종가매수선택.SelectedIndex);
                                double 주문비 = double.Parse(Form_Special.form.TB_예약주문_주문비계산값.Text);

                                예약실행(위치, 비중, 선택, 주문가, 주문수량, 주문비, Form_Special.form.CB_예약주문_종가연동.Checked, Form_Special.form.CB_예약주문_종가체결삭제.Checked, Form_Special.form.CB_예약주문_종가전량매도삭제.Checked);
                            }
                            else
                            {
                                Form1.AutoClosingAlram("예약주문 비중 이 1 보다 작습니다.", "비중오류", 10, "에러");
                            }
                        }
                        else
                        {
                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 2;
                        }
                    }

                    if (sender.Equals(Form_Special.form.BT_예약주문_종가매도추가))
                    {
                        if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(3))
                        {
                            double.TryParse(Form_Special.form.TB_예약주문_종가매도비중.Text, out double 종가매도비중);
                            Form_Special.form.TB_예약주문_종가매도비중.Text = 종가매도비중.ToString();

                            if (종가매도비중 > 0)
                            {
                                if (Form_Special.form.TB_예약주문_주문가.Text.Trim().Length == 0) 예약종목선택(Market);

                                string 위치 = "종가'매도'";
                                double 비중 = 종가매도비중;
                                int 선택 = Form_Special.form.CBB_예약주문_종가매도선택.SelectedIndex;
                                int 주문가 = int.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                                int 주문수량 = Method.주문수량계산(잔고, 주문가, 종가매도비중, Form_Special.form.CBB_예약주문_종가매도선택.SelectedIndex);
                                double 주문비 = double.Parse(Form_Special.form.TB_예약주문_주문비계산값.Text);

                                예약실행(위치, 비중, 선택, 주문가, 주문수량, 주문비, Form_Special.form.CB_예약주문_종가연동.Checked, Form_Special.form.CB_예약주문_종가체결삭제.Checked, Form_Special.form.CB_예약주문_종가전량매도삭제.Checked);
                            }
                            else
                            {
                                Form1.AutoClosingAlram("예약주문 비중 이 1 보다 작습니다.", "비중오류", 10, "에러");
                            }
                        }
                        else
                        {
                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 3;
                        }
                    }
                }

                void 예약실행(string 위치, double 비중, int 선택, int 주문가, int 주문수량, double 주문비, bool 연동, bool 체결완료삭제, bool 전량매도삭제)
                {
                    if (Form1.form1.주문예약_List.Count > 0)
                    {
                        List<주문예약> results = Form1.form1.주문예약_List.FindAll(o => o.종목코드.Equals(Market.종목코드));
                        if (results.Count == 199)
                        {
                            Helper.알림창_멀티("예약주문에러","종목당 예약주문은 최대 200개 까지 등록가능 합니다.", 5, false);

                            Log.에러기록(" ");
                            Log.에러기록("[예약주문에러] 종목당 예약주문은 최대 200개 까지 등록가능 합니다.");
                            Log.에러기록(" ");
                        }
                        else
                        {
                            Run();
                        }
                    }
                    else
                    {
                        Run();
                    }

                    void Run()
                    {
                        Task Task_Action = new Task(
                        () =>
                        {
                            string 주문시간 = GenieConfig.MTB_예약주문_장전주문시간.ToString();
                            if (위치.Contains("종가"))
                            {
                                주문시간 = GenieConfig.MTB_예약주문_종가주문시간.ToString();
                            }

                            int 주문유형 = 1;
                            if (위치.Contains("매도"))
                            {
                                주문유형 = 2;
                            }

                            Helper.안전한_UI_업데이트(Form_Special.form, () =>
                            {
                                using (new CenterWinDialog(Form_Special.form))
                                    if (MessageBox.Show(Form_Special.form.TB_예약주문_종목명.Text + "\n' " + 위치 + " ' 주문가: " + 주문가 + "로 예약 하시겠습니까 ?" + "\n(주문시간 은 " + 주문시간 + " 입니다.)", 위치 + " 추가", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        double 기준가_ = double.Parse(Form_Special.form.TB_예약주문_기준가.Text);

                                        주문예약 주문추가 = new 주문예약(str.today, Get_예약번호(), "0000", 주문유형, Market.종목코드, Market.종목명, 주문비, 비중, 선택, 주문가, 주문수량, 0, "[예약주문] " + 위치, "+++", true, 연동, 체결완료삭제, 전량매도삭제);
                                        Form1.form1.주문예약_List.Add(주문추가);

                                        SaveToFile.주문예약_파일저장();

                                        if (위치.Contains("장전'매수'"))
                                        {
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = -1;
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 0;
                                        }
                                        else if (위치.Contains("장전'매도'"))
                                        {
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = -1;
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 1;
                                        }
                                        else if (위치.Contains("종가'매수'"))
                                        {
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = -1;
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 2;
                                        }
                                        else if (위치.Contains("종가'매도'"))
                                        {
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = -1;
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 3;
                                        }
                                        else if (위치.Contains("신규'매수'"))
                                        {
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = -1;
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 4;
                                        }
                                    }
                            });
                        });
                        Task_Action.Start();
                    }
                }
            }
            else
            {
                Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = -1;
                if (sender.Equals(Form_Special.form.BT_예약주문_장전매수추가)) Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 0;
                if (sender.Equals(Form_Special.form.BT_예약주문_장전매도추가)) Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 1;
                if (sender.Equals(Form_Special.form.BT_예약주문_종가매수추가)) Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 2;
                if (sender.Equals(Form_Special.form.BT_예약주문_종가매도추가)) Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 3;
            }
        }

        public static void BT_Click_예약삭제(object sender)
        {
            if (Form_Special.form.LB_예약리스트.SelectedItems.Count > 0)
            {
                if (sender.Equals(Form_Special.form.BT_예약주문_장전매수삭제))
                {
                    Stockbalance 잔고 = Form1.stockBalanceList.FirstOrDefault(o => o.Value.종목명.Equals(Form_Special.form.TB_예약주문_종목명.Text.Trim())).Value;
                    if (잔고 != null)
                    {
                        if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(0))
                            삭제실행("장전'매수'");
                        else
                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 0;
                    }
                    else
                    {
                        if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(4))
                            삭제실행("신규'매수'");
                        else
                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 4;
                    }
                }


                if (sender.Equals(Form_Special.form.BT_예약주문_종가매수삭제))
                {
                    Stockbalance 잔고 = Form1.stockBalanceList.FirstOrDefault(o => o.Value.종목명.Equals(Form_Special.form.TB_예약주문_종목명.Text.Trim())).Value;
                    if (잔고 != null)
                    {
                        if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(2))
                            삭제실행("종가'매수'");
                        else
                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 2;
                    }
                    else
                    {
                        if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(4))
                            삭제실행("신규'매수'");
                        else
                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 4;
                    }
                }

                if (sender.Equals(Form_Special.form.BT_예약주문_종가매도삭제))
                {
                    if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(3))
                    {
                        삭제실행("종가'매도'");
                    }
                    else
                    {
                        Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 3;
                    }
                }

                if (sender.Equals(Form_Special.form.BT_예약주문_장전매도삭제))
                {
                    if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(1))
                    {
                        삭제실행("장전'매도'");
                    }
                    else
                    {
                        Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 1;
                    }
                }
            }
            else
            {
                Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = -1;
                if (sender.Equals(Form_Special.form.BT_예약주문_장전매수삭제)) Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 0;
                if (sender.Equals(Form_Special.form.BT_예약주문_장전매도삭제)) Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 1;
                if (sender.Equals(Form_Special.form.BT_예약주문_종가매수삭제)) Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 2;
                if (sender.Equals(Form_Special.form.BT_예약주문_종가매도삭제)) Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 3;
            }

            void 삭제실행(string 위치)
            {
                bool 성공 = int.TryParse(Form_Special.form.LB_예약리스트.SelectedItem.ToString().Split(' ')[0], out int 예약번호);
                if (성공)
                {
                    Task Task_Action = new Task(
                    () =>
                    {
                        Helper.안전한_UI_업데이트(Form_Special.form, () =>
                        {
                            int index = Form_Special.form.LB_예약리스트.SelectedIndex;
                            string 종목명 = Form_Special.form.LB_예약리스트.SelectedItem.ToString().Split(' ')[1];

                            using (new CenterWinDialog(Form_Special.form))
                                if (MessageBox.Show(종목명 + " :: " + 위치 + "예약 을 취소 하겠습니까? ", "주문취소 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    주문예약 종목 = Form1.form1.주문예약_List.Find(o => o.예약번호.Equals(예약번호));
                                    if (종목 != null)
                                    {
                                        if (Form1.JumunItem_List.TryGetValue(종목.주문번호, out JumunItem jumun))
                                        {
                                            using (new CenterWinDialog(Form_Special.form))
                                                if (MessageBox.Show(jumun.종목명 + " 주문번호(" + jumun.주문번호 + ") " + GET.매수매도str(jumun.매수매도) + " 주문이 있습니다. 취소하시겠습니까? ", "주문취소 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                {
                                                    jumun.반복횟수 = 0;
                                                    jumun.취소시간 = 0;
                                                    jumun.취소timer = 0;
                                                }
                                            Run();
                                        }
                                        else
                                        {
                                            Run();
                                        }
                                    }

                                    void Run()
                                    {
                                        Form1.form1.주문예약_List.Remove(종목);
                                        SaveToFile.주문예약_파일저장();

                                        if (위치.Contains("장전'매수'"))
                                        {
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = -1;
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 0;
                                        }
                                        else if (위치.Contains("장전'매도'"))
                                        {
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = -1;
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 1;
                                        }
                                        else if (위치.Contains("종가'매수'"))
                                        {
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = -1;
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 2;
                                        }
                                        else if (위치.Contains("종가'매도'"))
                                        {
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = -1;
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 3;
                                        }
                                        else if (위치.Contains("신규'매수'"))
                                        {
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = -1;
                                            Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 4;
                                        }

                                        if (Form_Special.form.LB_예약리스트.SelectedItems.Count > 0)
                                        {
                                            Form_Special.form.LB_예약리스트.SelectedIndex = index - 2;
                                        }
                                    }
                                }
                        });
                    });
                    Task_Action.Start();
                }
            }
        }

        public static void TB_주문비_예약_TextChanged()
        {
            if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(0))
            {
                if (!Form_Special.form.TB_예약주문_주문비.Text.StartsWith("-"))
                {
                    Form_Special.form.TB_예약주문_주문비.Text = "-" + Form_Special.form.TB_예약주문_주문비.Text;
                    Form_Special.form.TB_예약주문_주문비.Select(Form_Special.form.TB_예약주문_주문비.Text.Length, 0);
                }
            }
            else if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(1))
            {
                if (Form_Special.form.TB_예약주문_주문비.Text.StartsWith("-"))
                {
                    Form_Special.form.TB_예약주문_주문비.Text = Form_Special.form.TB_예약주문_주문비.Text.Substring(1);
                    Form_Special.form.TB_예약주문_주문비.Select(Form_Special.form.TB_예약주문_주문비.Text.Length, 0);
                }
            }
        }

        public static void 종목선택(object sender)
        {
            // [1] sender를 TextBox로 변환 (입력된 텍스트를 바로 가져오기 위함)
            if (!(sender is TextBox targetBox)) return;

            string 입력된_종목명 = targetBox.Text.Trim();
            if (string.IsNullOrEmpty(입력된_종목명)) return;

            // [2] 최적화: LINQ(.FirstOrDefault) 제거하고 단순 반복문 사용
            // 딕셔너리의 값(Market_Item)들 중에서 이름이 같은 것을 찾음
            Market_Item 찾은_종목 = null;

            foreach (var item in Form1.Market_Item_List.Values)
            {
                if (item.종목명 == 입력된_종목명)
                {
                    찾은_종목 = item;
                    break; // 찾으면 즉시 중단 (CPU 절약)
                }
            }

            // [3] 종목을 찾았을 경우 처리
            if (찾은_종목 != null)
            {
                // ReferenceEquals가 .Equals보다 미세하게 더 빠름 (주소값 비교)
                if (ReferenceEquals(sender, Form_Special.form.TB_수동주문_종목명))
                {
                    수동종목선택(찾은_종목);
                }
                else if (ReferenceEquals(sender, Form_Special.form.TB_예약주문_종목명))
                {
                    예약종목선택(찾은_종목);
                }
            }
        }

        public static void 주문비계산(object sender)
        {

            if (sender.Equals(Form_Special.form.TB_수동주문_주문비))
            {
                Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(Form_Special.form.TB_수동주문_종목명.Text.Trim())).Value;
                if (Market != null)
                {
                    int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);
                    if (현재가 > 1)
                        수동주문비기준계산(Market.종목코드, 현재가);
                }
            }

            if (sender.Equals(Form_Special.form.TB_예약주문_주문비))
            {
                Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(Form_Special.form.TB_예약주문_종목명.Text.Trim())).Value;
                if (Market != null)
                {
                    int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);
                    if (현재가 > 1)
                        예약주문비기준계산(Market.종목코드, 현재가, Market.Last_price);
                }
            }

        }

        public static void 주문가계산(object sender)
        {
            Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(Form_Special.form.TB_수동주문_종목명.Text.Trim())).Value;
            if (Market != null)
            {
                int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);
                if (sender.Equals(Form_Special.form.TB_수동주문_주문가))
                {
                    int 기준가 = int.Parse(Form_Special.form.TB_수동주문_price.Text);
                    int 주문가 = int.Parse(Form_Special.form.TB_수동주문_주문가.Text);

                    if (Form_Special.form.RB_매수.Checked)
                    {
                        if (기준가 > 주문가)
                        {
                            if (주문가 > 기준가 * 0.1)
                            {
                                if (주문가 < Method.상한가_하한가_구하기("하", Market.종목코드))
                                {
                                    Form1.AutoClosingAlram("주문가가 '하한가' 보다 낮습니다.", "하한가알림", 10, "에러");

                                    Form_Special.form.TB_수동주문_주문가.Text = Method.상한가_하한가_구하기("하", Market.종목코드).ToString();
                                    주문가 = int.Parse(Form_Special.form.TB_수동주문_주문가.Text);
                                }

                                Form_Special.form.TB_수동주문_tick.Text = 수동_주문가기준_틱계산(기준가, 주문가, Market.Market).ToString();
                            }
                            else
                            {
                                Form_Special.form.TB_수동주문_주문가.Text = 기준가.ToString();
                                Form1.AutoClosingAlram("주문가는 '기준가의 10%' 보다 높아야 합니다.", "주문가알림", 10, "에러");
                            }
                        }
                        else
                        {
                            Form_Special.form.TB_수동주문_주문가.Text = 기준가.ToString();
                            Form1.AutoClosingAlram("주문가는 '기준가' 보다 낮아야 합니다.", "주문가알림", 10, "에러");
                        }
                    }
                    else if (Form_Special.form.RB_매도.Checked)
                    {
                        if (기준가 < 주문가)
                        {
                            if (주문가 > Method.상한가_하한가_구하기("상", Market.종목코드))
                            {
                                Form1.AutoClosingAlram("주문가가 '상한가' 보다 높습니다.", "상한가알림", 10, "에러");

                                Form_Special.form.TB_수동주문_주문가.Text = Method.상한가_하한가_구하기("상", Market.종목코드).ToString();
                                주문가 = int.Parse(Form_Special.form.TB_수동주문_주문가.Text);
                            }

                            Form_Special.form.TB_수동주문_tick.Text = 수동_주문가기준_틱계산(기준가, 주문가, Market.Market).ToString();
                        }
                        else
                        {
                            Form_Special.form.TB_수동주문_주문가.Text = 기준가.ToString();
                            Form1.AutoClosingAlram("주문가는 '기준가' 보다 높아야 합니다.", "주문가알림", 10, "에러");
                        }
                    }
                }

                if (sender.Equals(Form_Special.form.TB_예약주문_주문가))
                {
                    if (Form_Special.form.TB_예약주문_종목명.Text.Length > 0)
                    {
                        if (현재가 > 1)
                        {
                            if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex == 0)
                            {
                                int 기준가 = int.Parse(Form_Special.form.TB_예약주문_기준가.Text);
                                int 주문가 = int.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                                if (기준가 < 주문가)
                                {
                                    Form_Special.form.TB_예약주문_주문가.Text = 기준가.ToString();
                                    Form1.AutoClosingAlram("주문가는 '기준가' 보다 낮아야 합니다.", "주문가알림", 10, "에러");
                                }
                                else
                                {
                                    if (주문가 > 기준가 * 0.1)
                                    {
                                        Form_Special.form.TB_예약주문_장전매수호가.Text = 주문가구하기(기준가, 주문가).ToString();
                                    }
                                    else
                                    {
                                        Form_Special.form.TB_예약주문_주문가.Text = 기준가.ToString();
                                        Form1.AutoClosingAlram("주문가는 '기준가의 10%' 보다 높아야 합니다.", "주문가알림", 10, "에러");
                                    }
                                }
                            }
                            else if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex == 1)
                            {
                                int 기준가 = int.Parse(Form_Special.form.TB_예약주문_기준가.Text);
                                int 주문가 = int.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                                if (기준가 > 주문가)
                                {
                                    Form_Special.form.TB_예약주문_주문가.Text = 기준가.ToString();
                                    Form1.AutoClosingAlram("주문가는 '기준가' 보다 높아야 합니다.", "주문가알림", 10, "에러");
                                }
                                else
                                {
                                    Form_Special.form.TB_예약주문_장전매도호가.Text = 주문가구하기(기준가, 주문가).ToString();
                                }
                            }
                            else if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex == 2)
                            {
                                int 기준가 = int.Parse(Form_Special.form.TB_예약주문_현재가.Text);
                                int 주문가 = int.Parse(Form_Special.form.TB_예약주문_주문가.Text);

                                if (기준가 > 주문가)
                                {
                                    if (주문가 > 기준가 * 0.1)
                                    {
                                        if (주문가 < Method.상한가_하한가_구하기("하", Market.종목코드))
                                        {
                                            Form1.AutoClosingAlram("주문가가 '하한가' 보다 낮습니다.", "하한가알림", 10, "에러");

                                            Form_Special.form.TB_예약주문_주문가.Text = Method.상한가_하한가_구하기("하", Market.종목코드).ToString();
                                            주문가 = int.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                                        }

                                        Form_Special.form.TB_예약주문_종가매수호가.Text = 주문가구하기(기준가, 주문가).ToString();
                                    }
                                    else
                                    {
                                        Form_Special.form.TB_예약주문_주문가.Text = 기준가.ToString();
                                        Form1.AutoClosingAlram("주문가는 '기준가의 10%' 보다 높아야 합니다.", "주문가알림", 10, "에러");
                                    }
                                }
                                else
                                {
                                    Form_Special.form.TB_예약주문_주문가.Text = (기준가 - Method.GetHoga(기준가, Market.Market)).ToString();
                                    Form1.AutoClosingAlram("주문가는 '기준가' 보다 낮아야 합니다.", "주문가알림", 10, "에러");
                                }
                            }
                            else if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex == 3)
                            {
                                int 기준가 = int.Parse(Form_Special.form.TB_예약주문_현재가.Text);
                                int 주문가 = int.Parse(Form_Special.form.TB_예약주문_주문가.Text);

                                if (기준가 < 주문가)
                                {
                                    if (주문가 > Method.상한가_하한가_구하기("상", Market.종목코드))
                                    {
                                        Form1.AutoClosingAlram("주문가가 '상한가' 보다 높습니다.", "상한가알림", 10, "에러");

                                        Form_Special.form.TB_예약주문_주문가.Text = Method.상한가_하한가_구하기("상", Market.종목코드).ToString();
                                        주문가 = int.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                                    }

                                    Form_Special.form.TB_예약주문_종가매도호가.Text = 주문가구하기(기준가, 주문가).ToString();
                                }
                                else
                                {
                                    Form_Special.form.TB_예약주문_주문가.Text = (기준가 + Method.GetHoga(기준가, Market.Market)).ToString();
                                    Form1.AutoClosingAlram("주문가는 '기준가' 보다 높아야 합니다.", "주문가알림", 10, "에러");
                                }
                            }
                            else if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex == 4)
                            {
                                int 기준가 = int.Parse(Form_Special.form.TB_예약주문_기준가.Text);
                                int 주문가 = int.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                                if (기준가 < 주문가)
                                {
                                    Form_Special.form.TB_예약주문_주문가.Text = 기준가.ToString();
                                    Form1.AutoClosingAlram("주문가는 '기준가' 보다 낮아야 합니다.", "주문가알림", 10, "에러");
                                }
                                else
                                {
                                    if (주문가 > 기준가 * 0.1)
                                    {
                                        Form_Special.form.TB_예약주문_장전매수호가.Text = 주문가구하기(기준가, 주문가).ToString();
                                    }
                                    else
                                    {
                                        Form_Special.form.TB_예약주문_주문가.Text = 기준가.ToString();
                                        Form1.AutoClosingAlram("주문가는 '기준가의 10%' 보다 높아야 합니다.", "주문가알림", 10, "에러");
                                    }
                                }
                            }
                        }

                        int 주문가구하기(int 기준가, int 주문가)
                        {
                            int num = 0;
                            for (int i = 1; i > 0; i++)
                            {
                                if (기준가 > 주문가)
                                {
                                    int 주문가_계산 = Method.예약Order_price(-i, 기준가, Market.Market);

                                    if (주문가_계산 <= 주문가)
                                    {
                                        Form_Special.form.TB_예약주문_주문가.Text = 주문가_계산.ToString();

                                        double 계산값 = ((double)주문가_계산 - 기준가) / 주문가_계산 * 100;

                                        Form_Special.form.TB_예약주문_주문비계산값.Text = Math.Round(계산값, 2).ToString();
                                        num = -i;
                                        break;
                                    }
                                }
                                else
                                {
                                    int 주문가_계산 = Method.예약Order_price(i, 기준가, Market.Market);

                                    if (주문가_계산 >= 주문가)
                                    {
                                        Form_Special.form.TB_예약주문_주문가.Text = 주문가_계산.ToString();

                                        double 계산값 = ((double)주문가_계산 - 기준가) / 주문가_계산 * 100;

                                        Form_Special.form.TB_예약주문_주문비계산값.Text = Math.Round(계산값, 2).ToString();
                                        num = i;
                                        break;
                                    }
                                }
                            }
                            return num;
                        }
                    }
                }
            }
        }

        public static void 수동주문틱계산(object sender)
        {
            Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(Form_Special.form.TB_수동주문_종목명.Text.Trim())).Value;
            if (Market != null)
            {
                int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);
                if (sender.Equals(Form_Special.form.TB_수동주문_tick))
                {
                    int 기준가 = int.Parse(Form_Special.form.TB_수동주문_price.Text);

                    if (Form_Special.form.RB_매수.Checked)
                    {
                        if (!Form_Special.form.TB_수동주문_tick.Text.StartsWith("-"))
                        {
                            Form1.AutoClosingAlram("매수 주문은 ' - ' 호가로 주문 가능합니다.", "호가알림", 10, "에러");

                            Form_Special.form.TB_수동주문_tick.Text = "-" + Form_Special.form.TB_수동주문_tick.Text.ToString();
                        }
                        else
                        {
                            Form_Special.form.TB_수동주문_주문가.Text = (Method.예약Order_price(int.Parse(Form_Special.form.TB_수동주문_tick.Text.Trim()), 현재가, Market.Market)).ToString();

                            int 주문가 = int.Parse(Form_Special.form.TB_수동주문_주문가.Text);
                            if (주문가 < Method.상한가_하한가_구하기("하", Market.종목코드))
                            {
                                Form1.AutoClosingAlram("주문가가 '하한가' 보다 낮습니다.", "하한가알림", 10, "에러");

                                Form_Special.form.TB_수동주문_주문가.Text = Method.상한가_하한가_구하기("하", Market.종목코드).ToString();
                                주문가 = int.Parse(Form_Special.form.TB_수동주문_주문가.Text);
                            }

                            Form_Special.form.TB_수동주문_tick.Text = 수동_주문가기준_틱계산(기준가, 주문가, Market.Market).ToString();
                            Form_Special.form.TB_수동주문_주문비계산값.Text = Math.Round((((double)주문가 - Market.현재가) / 주문가 * 100), 2).ToString();
                        }
                    }
                    else if (Form_Special.form.RB_매도.Checked)
                    {
                        if (Form_Special.form.TB_수동주문_tick.Text.StartsWith("-"))
                        {
                            Form1.AutoClosingAlram("매도 주문은 ' + ' 호가로 주문 가능합니다.", "호가알림", 10, "에러");

                            Form_Special.form.TB_수동주문_tick.Text = Form_Special.form.TB_수동주문_tick.Text.ToString().Substring(1);
                        }
                        else
                        {
                            Form_Special.form.TB_수동주문_주문가.Text = (Method.예약Order_price(int.Parse(Form_Special.form.TB_수동주문_tick.Text.Trim()), 현재가, Market.Market)).ToString();

                            int 주문가 = int.Parse(Form_Special.form.TB_수동주문_주문가.Text);
                            if (주문가 > Method.상한가_하한가_구하기("상", Market.종목코드))
                            {
                                Form1.AutoClosingAlram("주문가가 '상한가' 보다 높습니다.", "상한가알림", 10, "에러");

                                Form_Special.form.TB_수동주문_주문가.Text = Method.상한가_하한가_구하기("상", Market.종목코드).ToString();
                                주문가 = int.Parse(Form_Special.form.TB_수동주문_주문가.Text);
                            }

                            Form_Special.form.TB_수동주문_tick.Text = 수동_주문가기준_틱계산(기준가, 주문가, Market.Market).ToString();
                            Form_Special.form.TB_수동주문_주문비계산값.Text = Math.Round((((double)주문가 - Market.현재가) / 주문가 * 100), 2).ToString();
                        }
                    }
                }
            }
        }

        public static void 예약주문호가계산(object sender)
        {
            Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(Form_Special.form.TB_예약주문_종목명.Text.Trim())).Value;
            if (Market != null)
            {
                int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);
                if (sender.Equals(Form_Special.form.TB_예약주문_장전매수호가))
                {
                    if (!Form1.stockBalanceList.ContainsKey(Market.종목코드))
                        Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 4;
                    else
                        Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 0;

                    int 기준가 = Market.Last_price;
                    if (Get.TimeNow > 90000) 기준가 = Market.현재가;
                    int.TryParse(Form_Special.form.TB_예약주문_장전매수호가.Text, out int 장전매수);
                    Form_Special.form.TB_예약주문_장전매수호가.Text = 장전매수.ToString();

                    Form_Special.form.TB_예약주문_주문가.Text = (Method.예약Order_price(장전매수, 기준가, Market.Market)).ToString();
                    double 주문가 = double.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                    Form_Special.form.TB_예약주문_주문비계산값.Text = Math.Round(((주문가 - 기준가) / 주문가 * 100), 2).ToString();
                }

                if (sender.Equals(Form_Special.form.TB_예약주문_종가매수호가))
                {
                    if (!Form1.stockBalanceList.ContainsKey(Market.종목코드))
                        Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 4;
                    else
                        Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 2;

                    int.TryParse(Form_Special.form.TB_예약주문_종가매수호가.Text, out int 종가매수);
                    Form_Special.form.TB_예약주문_종가매수호가.Text = 종가매수.ToString();

                    Form_Special.form.TB_예약주문_주문가.Text = (Method.예약Order_price(종가매수, 현재가, Market.Market)).ToString();
                    double 주문가 = double.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                    Form_Special.form.TB_예약주문_주문비계산값.Text = Math.Round(((주문가 - Market.현재가) / 주문가 * 100), 2).ToString();
                }

                if (Form1.stockBalanceList.ContainsKey(Market.종목코드))
                {
                    if (sender.Equals(Form_Special.form.TB_예약주문_장전매도호가))
                    {
                        Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 1;

                        int 기준가 = Market.Last_price;
                        if (Get.TimeNow > 90000) 기준가 = Market.현재가;
                        int.TryParse(Form_Special.form.TB_예약주문_장전매도호가.Text, out int 장전매도);
                        Form_Special.form.TB_예약주문_장전매도호가.Text = 장전매도.ToString();

                        Form_Special.form.TB_예약주문_주문가.Text = (Method.예약Order_price(장전매도, 기준가, Market.Market)).ToString();
                        double 주문가 = double.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                        Form_Special.form.TB_예약주문_주문비계산값.Text = Math.Round(((주문가 - 기준가) / 주문가 * 100), 2).ToString();
                    }

                    if (sender.Equals(Form_Special.form.TB_예약주문_종가매도호가))
                    {
                        Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 3;

                        int.TryParse(Form_Special.form.TB_예약주문_종가매도호가.Text, out int 종가매도);
                        Form_Special.form.TB_예약주문_종가매도호가.Text = 종가매도.ToString();

                        Form_Special.form.TB_예약주문_주문가.Text = (Method.예약Order_price(종가매도, 현재가, Market.Market)).ToString();
                        double 주문가 = double.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                        Form_Special.form.TB_예약주문_주문비계산값.Text = Math.Round(((주문가 - Market.현재가) / 주문가 * 100), 2).ToString();
                    }
                }
            }
        }

        public static int 수동_주문가기준_틱계산(int 기준가, int 주문가, string Market)
        {
            int num = 0;
            for (int i = 1; i > 0; i++)
            {
                if (기준가 > 주문가)
                {
                    int 주문가_계산 = Method.예약Order_price(-i, 기준가, Market);

                    if (주문가_계산 <= 주문가)
                    {
                        Form_Special.form.TB_수동주문_주문가.Text = 주문가_계산.ToString();

                        double 계산값 = ((double)주문가_계산 - 기준가) / 주문가_계산 * 100;

                        Form_Special.form.TB_수동주문_주문비계산값.Text = Math.Round(계산값, 2).ToString();
                        num = -i;
                        break;
                    }
                }
                else
                {
                    int 주문가_계산 = Method.예약Order_price(i, 기준가, Market);

                    if (주문가_계산 >= 주문가)
                    {
                        Form_Special.form.TB_수동주문_주문가.Text = 주문가_계산.ToString();

                        double 계산값 = ((double)주문가_계산 - 기준가) / 주문가_계산 * 100;

                        Form_Special.form.TB_수동주문_주문비계산값.Text = Math.Round(계산값, 2).ToString();
                        num = i;
                        break;
                    }
                }
            }
            return num;
        }

        public static void LB_예약리스트_Click()
        {
            if (Form_Special.form.LB_예약리스트.SelectedItem != null)
            {
                bool 성공 = int.TryParse(Form_Special.form.LB_예약리스트.SelectedItem.ToString().Split(' ')[0], out int 예약번호);
                if (성공)
                {
                    string name = Form_Special.form.LB_예약리스트.SelectedItem.ToString().Split(' ')[1];

                    종목선택(name);
                }
            }
        }

        public static void 수동종목선택(Market_Item 선택된_종목)
        {
            // 1. 시장별(코스피/코스닥) 호가 단위에 맞춰 가격 보정
            int 보정된_현재가 = Method.호가맞추기(선택된_종목.현재가, 선택된_종목.Market);

            // 2. 유효한 가격인지 확인 (1원보다 커야 함)
            if (보정된_현재가 > 1)
            {
                // [최적화 핵심] 화면에 이미 같은 가격이 적혀있으면 건너뜀 (CPU 절약 & 깜빡임 방지)
                string 표시할_가격 = 선택된_종목.현재가.ToString();

                Helper.안전한_UI_업데이트(Form1.form1, () =>
                {
                    if (Form_Special.form.TB_수동주문_price.Text != 표시할_가격)
                    {
                        Form_Special.form.TB_수동주문_price.Text = 표시할_가격;
                    }
                });

                // 3. 주문 가능 수량 및 비중 계산
                수동주문비기준계산(선택된_종목.종목코드, 보정된_현재가);
            }
            else
            {
                info.요청(선택된_종목.종목코드, "info_수동종목선택", "", false);
            }
        }

        public static void 예약종목선택(Market_Item Market)
        {
            int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);
            if (현재가 > 1)
            {
                Helper.안전한_UI_업데이트(Form1.form1, () =>
                {
                    Form_Special.form.TB_예약주문_종목명.Text = Market.종목명;
                    Form_Special.form.TB_예약주문_현재가.Text = Market.현재가.ToString();
                });
                예약주문비기준계산(Market.종목코드, 현재가, Market.Last_price);
            }
            else
            {
                info.요청(Market.종목코드, "info_예약종목선택", "", false);
            }
        }

        public static void 종목선택(string 종목명)
        {
            if (Form1.form1.CB_특수매매.Checked)
            {
                종목명 = 종목명.TrimStart('*');
                Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(종목명)).Value;
                if (Market != null)
                {
                    int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);
                    if (현재가 > 1)
                    {
                        Form_Special.form.TB_수동주문_종목명.Text = 종목명;
                        Form_Special.form.TB_수동주문_price.Text = Market.현재가.ToString();

                        Form_Special.form.TB_예약주문_종목명.Text = 종목명;
                        Form_Special.form.TB_예약주문_현재가.Text = Market.현재가.ToString();

                        수동주문비기준계산(Market.종목코드, 현재가);
                        예약주문비기준계산(Market.종목코드, 현재가, Market.Last_price);
                    }
                    else
                    {
                        info.요청(Market.종목코드, "info_종목선택", "", false);
                    }
                }
            }

            if (Form1.form1.CBB_최종가종목.Items.Contains(종목명))
            {
                Form1.form1.CBB_최종가종목.SelectedItem = 종목명;
            }
        }

        public static void 수동주문비기준계산(string Code, int 현재가)
        {
            // 1. 텍스트박스 값은 한 번만 읽어오기 (접근 비용 절약)
            string 입력된_주문비 = Form_Special.form.TB_수동주문_주문비.Text;

            // 2. 상한가/하한가 판단 (삼항 연산자로 깔끔하게)
            string 상하 = 입력된_주문비.StartsWith("-") ? "하한가" : "상한가";

            // 3. 숫자 변환
            double.TryParse(입력된_주문비, out double 주문비);

            // 4. 계산 결과 받아오기
            string 결과_문자열 = Method.주문가계산(Code, 주문비, 현재가, 현재가, 상하);

            // 5. [핵심 최적화] Split은 딱 한 번만 수행해서 배열에 저장! (속도 UP, 메모리 절약)
            string[] 결과_배열 = 결과_문자열.Split('&');

            // 6. 배열 길이 확인 (혹시 모를 에러 방지)
            if (결과_배열.Length >= 3)
            {
                // 7. 값이 변했을 때만 화면 업데이트 (깜빡임 방지 헬퍼 호출)
                화면_업데이트(Form_Special.form.TB_수동주문_주문가, 결과_배열[0]);
                화면_업데이트(Form_Special.form.TB_수동주문_주문비계산값, 결과_배열[1]);
                화면_업데이트(Form_Special.form.TB_수동주문_tick, 결과_배열[2]);
            }

            void 화면_업데이트(Control 컨트롤, string 새_값)
            {
                Helper.안전한_UI_업데이트(Form1.form1, () =>
                {
                    if (컨트롤.Text != 새_값)
                    {
                        컨트롤.Text = 새_값;
                    }
                });
            }
        }

        public static void 예약주문비기준계산(string Code, int 현재가, int 전일종가)
        {
            Form_Special.form.TB_예약주문_현재가.Text = 현재가.ToString();
            if (Get.TimeNow > 90000) 전일종가 = 현재가;
            Form_Special.form.TB_예약주문_기준가.Text = 전일종가.ToString();
            int 기준가 = 전일종가;
            string 상하 = "";

            double.TryParse(Form_Special.form.TB_예약주문_주문비.Text, out double 주문비);

            if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(0))
            {
                if (주문비 < -90)
                {
                    주문비 = -90;
                }
            }
            else if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(2))
            {
                상하 = "하한가";
                기준가 = 현재가;
            }
            else if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(3))
            {
                상하 = "상한가";
                기준가 = 현재가;
            }
            string para = Method.주문가계산(Code, 주문비, 기준가, 현재가, 상하);

            Form_Special.form.TB_예약주문_주문가.Text = para.Split('&')[0];
            Form_Special.form.TB_예약주문_주문비계산값.Text = para.Split('&')[1];

            if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(0))
            {
                Form_Special.form.TB_예약주문_장전매수호가.Text = para.Split('&')[2];
            }
            else if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(1))
            {
                Form_Special.form.TB_예약주문_장전매도호가.Text = para.Split('&')[2];
            }
            else if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(2))
            {
                Form_Special.form.TB_예약주문_종가매수호가.Text = para.Split('&')[2];
            }
            else if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(3))
            {
                Form_Special.form.TB_예약주문_종가매도호가.Text = para.Split('&')[2];
            }
        }


        public static async Task 예약주문실행(bool 장전)
        {
            if (장전)
            {
                for (int i = 0; i < Form1.form1.주문예약_List.Count; i++)
                {
                    주문예약 주문 = Form1.form1.주문예약_List[i];
                    if (GET.Jango_state(주문.종목코드).Contains("거래정지"))
                    {
                        Form1.AutoClosingAlram("[장전주문 주문불가] 종목명: " + 주문.종목명 + " '거래정지' 되어 주문 할수 없습니다.", "거래정지", 10, "에러");
                    }
                    else
                    {
                        if (주문.등록)
                        {
                            if (주문.검색식.Contains("장전"))
                            {
                                Market_Item Market = Form1.Market_Item_List[주문.종목코드];
                                int 주문가 = 주문.주문가;
                                int 주문수량 = 주문.주문수량;
                                int 주문호가 = Method.호가변환(주문.주문비, 주문.종목코드, Market.Last_price, Market.Last_price, 주문.검색식);
                                int 상한가 = Method.상한가_하한가_구하기("상", Market.종목코드);
                                int 하한가 = Method.상한가_하한가_구하기("하", Market.종목코드);

                                if (주문.연동)
                                {
                                    주문가 = Method.예약Order_price(주문호가, Market.Last_price, Market.Market);
                                    주문수량 = Method.주문수량계산(Form1.stockBalanceList[주문.종목코드], 주문가, 주문.비중, 주문.선택);
                                }

                                if (주문가 > 상한가)
                                {
                                    Helper.알림창_멀티("예약주문 주문가오류", $"{주문.검색식} 주문의 {주문.종목명}\n\n주문가( {주문.주문가:N0} ) 가 상한가( {상한가:N0} ) 를 초과 하여 주문할 수 없습니다. ", 5, false);

                                    Log.에러기록(" ");
                                    Log.에러기록($"[예약주문 주문가오류] {주문.검색식} 주문의 {주문.종목명} 주문가( {주문.주문가:N0} ) 가 상한가( {상한가:N0} ) 를 초과 하여 주문할 수 없습니다. ");
                                    Log.에러기록(" ");
                                }
                                else if (주문가 < 하한가)
                                {
                                    Helper.알림창_멀티("예약주문 주문가오류", $"{주문.검색식} 주문의 {주문.종목명}\n\n주문가( {주문.주문가:N0} ) 가 하한가( {하한가:N0} ) 미만이라 주문할 수 없습니다. ", 5, false);

                                    Log.에러기록(" ");
                                    Log.에러기록($"[예약주문 주문가오류] {주문.검색식} 주문의 {주문.종목명} 주문가( {주문.주문가:N0} ) 가 하한가( {하한가:N0} ) 미만이라 주문할 수 없습니다. ");
                                    Log.에러기록(" ");
                                }
                                else
                                {
                                 await   예약주문_주문실행(주문, 주문가, 주문수량, 주문호가);
                                }
                            }
                        }
                    }
                }
            }

            if (!장전)
            {
                for (int i = 0; i < Form1.form1.주문예약_List.Count; i++)
                {
                    주문예약 주문 = Form1.form1.주문예약_List[i];
                    if (GET.Jango_state(주문.종목코드).Contains("거래정지"))
                    {
                        Form1.AutoClosingAlram("[종가주문 주문불가] 종목명: " + 주문.종목명 + " '거래정지' 되어 주문 할수 없습니다.", "거래정지", 10, "에러");
                    }
                    else
                    {
                        if (주문.등록)
                        {
                            if (주문.검색식.Contains("종가"))
                            {
                                Market_Item Market = Form1.Market_Item_List[주문.종목코드];
                                int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);
                                int 주문가 = 주문.주문가;
                                int 주문수량 = 주문.주문수량;
                                int 주문호가 = Method.호가변환(주문.주문비, 주문.종목코드, 현재가, 현재가, 주문.검색식);
                                int 상한가 = Method.상한가_하한가_구하기("상", Market.종목코드);
                                int 하한가 = Method.상한가_하한가_구하기("하", Market.종목코드);

                                if (주문.연동)
                                {
                                    if (현재가 > 1)
                                    {
                                 await       예약주문_등록(주문.예약번호, Market);
                                    }
                                    else
                                    {
                                        info.요청(주문.종목코드, "info_예약주문실행", 주문.예약번호.ToString(), false);
                                    }
                                }
                                else
                                {
                                    if (주문가 > 상한가)
                                    {
                                        Helper.알림창_멀티("예약주문 주문가오류", $"{주문.검색식} 주문의 {주문.종목명}\n\n주문가( {주문.주문가:N0} ) 가 상한가( {상한가:N0} ) 를 초과 하여 주문할 수 없습니다. ", 5, false);

                                        Log.에러기록(" ");
                                        Log.에러기록($"[예약주문 주문가오류] {주문.검색식} 주문의 {주문.종목명} 주문가( {주문.주문가:N0} ) 가 상한가( {상한가:N0} ) 를 초과 하여 주문할 수 없습니다. ");
                                        Log.에러기록(" ");
                                    }
                                    else if (주문가 < 하한가)
                                    {
                                        Helper.알림창_멀티("예약주문 주문가오류", $"{주문.검색식} 주문의 {주문.종목명}\n\n주문가( {주문.주문가:N0} ) 가 하한가( {하한가:N0} ) 미만이라 주문할 수 없습니다. ", 5, false);

                                        Log.에러기록(" ");
                                        Log.에러기록($"[예약주문 주문가오류] {주문.검색식} 주문의 {주문.종목명} 주문가( {주문.주문가:N0} ) 가 하한가( {하한가:N0} ) 미만이라 주문할 수 없습니다. ");
                                        Log.에러기록(" ");
                                    }
                                    else
                                    {
                                     await   예약주문_주문실행(주문, 주문가, 주문수량, 주문호가);
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }

        public static async Task 예약주문_등록(int 예약번호, Market_Item Market)
        {
            주문예약 주문 = Form1.form1.주문예약_List.Find(o => o.예약번호.Equals(예약번호));
            int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);
            int 주문가 = 주문.주문가;
            int 주문수량 = 주문.주문수량;
            int 주문호가 = Method.호가변환(주문.주문비, 주문.종목코드, 현재가, 현재가, 주문.검색식);
            int 상한가 = Method.상한가_하한가_구하기("상", Market.종목코드);
            int 하한가 = Method.상한가_하한가_구하기("하", Market.종목코드);
            string 상하 = "상한가";
            if (주문.검색식.Contains("매도")) { 상하 = "하한가"; }

            string 주문가_계산 = Method.주문가계산(주문.종목코드, 주문.주문비, 현재가, 현재가, 상하);

            주문가 = int.Parse(주문가_계산.Split('&')[0]);
            주문호가 = int.Parse(주문가_계산.Split('&')[2]);
            주문수량 = Method.주문수량계산(Form1.stockBalanceList[주문.종목코드], 주문가, 주문.비중, 주문.선택);

         await   예약주문_주문실행(주문, 주문가, 주문수량, 주문호가);
        }

        public static async Task 예약주문_주문실행(주문예약 예약주문, int 주문가격, int 주문수량, double Value)
        {
            Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목코드.Equals(예약주문.종목코드)).Value;
            if (Market != null)
            {
                Stockbalance 잔고 = null;
                if (Form1.stockBalanceList.TryGetValue(Market.종목코드, out Stockbalance item)) 잔고 = item;

                if (예약주문.매수매도 == 1) // 매수
                {
                    if (잔고 != null)
                    {
                        if (Method.매입비추매제한(잔고.보유비중))
                        {
                            if (Jisu_linkage.업종별지수연동("추매", Market))
                            {
                                // [수정] Properties.Settings -> Setting.acc
                                if (GenieConfig.CB_계좌매입비_매수제한) // 매입비
                                {
                                    // [수정] Properties.Settings -> Setting.acc
                                    if (!Method.매입비매수제한(GenieConfig.CBB_계좌매입비_제한선택).Contains("추매"))
                                    {
                                   await     매수진행();
                                    }
                                }
                                else
                                {
                                await   매수진행();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Jisu_linkage.업종별지수연동("신규", Market))
                        {
                            // [수정] Properties.Settings -> Setting.acc
                            if (GenieConfig.CB_계좌매입비_매수제한) // 매입비
                            {
                                // [수정] Properties.Settings -> Setting.acc
                                if (!Method.매입비매수제한(GenieConfig.CBB_계좌매입비_제한선택).Contains("신규"))
                                {
                                 await   매수진행();
                                }
                            }
                            else
                            {
                             await   매수진행();
                            }
                        }
                    }

                    async Task 매수진행()
                    {
                        if (Method.매매확인_VI_모투가능확인(Market, 예약주문.매수매도))
                        {
                            long 추정d2 = (Form1.Acc.추정D2 - (주문가격 * 주문수량));
                            if (추정d2 > 0)
                            {
                                if (잔고 != null)
                                {
                                    주문수량 = Method.최대매수금_주문수량계산(잔고, 주문수량);
                                    if (주문수량 > 0)
                                    {
                                   await     매수실행();
                                    }
                                    else
                                    {
                                        // 1. 알림창 출력 (문자열 보간 및 계산식 포함)
                                        Helper.알림창_멀티("최대매수금 초과", $"종목명: {잔고.종목명} 주문가격: {주문가격:N0}\n\n주문수량: {주문수량:N0} 주문금액: {(주문가격 * 주문수량):N0}\n최대매수금 초과 하여 주문 할 수 없습니다.", 5, false);

                                        // 2. 에러 기록 로그
                                        Log.에러기록(" ");
                                        Log.에러기록($"[최대매수금 초과] 종목명: {잔고.종목명} 주문가격: {주문가격:N0} 주문수량: {주문수량:N0} 주문금액: {(주문가격 * 주문수량):N0} 최대매수금 초과 하여 주문 할 수 없습니다.");
                                        Log.에러기록(" ");
                                    }
                                }
                                else
                                {
                                 await   매수실행();
                                }

                                async Task 매수실행()
                                {
                                    string Screennum = GET.JumunScreen();
                                    int Order번호 = GET.Order번호();
                                    예약주문.스크린번호 = Screennum;

                                    bool 신용주문 = 신용계산.신용주문_해야하나(예약주문.매수매도, 주문수량, Market_Item_List[잔고.종목코드], 잔고, out int 실제주문수량);
                                    홀딩잔고.예수금업데이트(GET.매수매도str(예약주문.매수매도), 주문가격, 실제주문수량, "주문", Market.종목코드, 신용주문);

                                    int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);

                                    // [지니 최적화] 예약 주문 객체 생성 (명확한 속성 할당)
                                    JumunItem 새주문 = new JumunItem
                                    {
                                        신용주문 = 신용주문,
                                        대출일 = "",
                                        Deletetimer = 0,
                                        Screennum = Screennum,
                                        종목코드 = 예약주문.종목코드,
                                        종목명 = 예약주문.종목명,
                                        주문번호 = "+++",
                                        원주문번호 = "---",
                                        검색식 = 예약주문.검색식,
                                        주문값 = Value,             // *주의: 여기 변수명이 'Value'임
                                        시장가구분 = 3,             // 예약주문은 시장가구분 3 고정
                                        취소시간 = 99999,           // 예약주문이라 취소시간 길게 설정
                                        취소N주문 = 0,
                                        반복횟수 = 0,
                                        비고 = "예약주문",          // 기존 13번째 인자
                                        Pos = 예약주문.검색식,          // 기존 14번째 인자 (Pos에 검색식 저장)
                                        주문수량 = 실제주문수량,
                                        주문가격 = 주문가격,
                                        매수매도 = 예약주문.매수매도,
                                        비중 = 0,
                                        비중단위 = 0,
                                        취소timer = 99999,          // 기존 20번째 인자 (취소timer도 길게)
                                        현재가 = 0,                 // 기존 코드에서 0 대입
                                        등락률 = 0,                 // 기존 코드에서 0 대입
                                        주문시간 = Get.TimeNow,
                                        미체결량 = 실제주문수량,        // 미체결량 초기화
                                        주문취소 = true,
                                        가동전 = false,
                                        Tik_cap = Method.Find_Tik_Cap(Market.현재가, 주문가격, Market.Market),
                                        Tik_price = 현재가,         // *주의: 28번째 인자에 '현재가' 변수 사용
                                        수익률 = 0,
                                        주문동기화 = false,
                                        감시번호 = 0,
                                        Order번호 = Order번호,
                                        수익구분 = 0,
                                        NXT = NXT_server,
                                        주문시간_Ticks = DateTime.Now.Ticks
                                    };

                                    // 리스트 추가 등 후속 작업...
                                await    Jumun.Add(새주문);

                                    ExecuteTrade.Que_order(새주문);
                                    예약주문.등록 = false;
                                }
                            }
                            else
                            {
                                // 1. 알림창 출력 (가독성 업그레이드)
                                Helper.알림창_멀티("주문불가알림", $"종목명: {예약주문.종목명} 주문수량: {주문수량:N0}\n\n주문가격: {주문가격:N0} 주문금액: {(주문가격 * 주문수량):N0}\n\n주문가능금액이 부족하여 ' 매수 ' 주문 할 수 없습니다.", 5, false);

                                // 2. 에러 기록 로그
                                Log.에러기록(" ");
                                Log.에러기록($"[주문불가알림] 종목명: {예약주문.종목명} 주문수량: {주문수량:N0} 주문가격: {주문가격:N0} 주문금액: {(주문가격 * 주문수량):N0} 주문가능금액이 부족하여 ' 매수 ' 주문 할 수 없습니다.");
                                Log.에러기록(" ");
                            }
                        }
                    }
                }
                else //매도
                {
                    if (잔고 != null)
                    {
                        int 총주문가능수량 = GET.총주문가능수량(잔고);

                        if (총주문가능수량 > 0)
                        {
                            if (Method.매매확인_VI_모투가능확인(Market, 예약주문.매수매도))
                            {
                                if (총주문가능수량 < 주문수량)
                                {
                                    주문수량 = 총주문가능수량;
                                }

                                신용계산.신용주문_분할매도_실행(잔고, 주문수량, async (is신용, 대출일, 수량) =>
                                {
                                  await  주문(is신용, 대출일, 수량);
                                });

                                async Task 주문(bool 신용주문, string 대출일, int 주문수량)
                                {
                                    주문예약 주문 = 예약주문;
                                    string Screennum = GET.JumunScreen();
                                    int Order번호 = GET.Order번호();
                                    주문.스크린번호 = Screennum;
                                    주문.등록 = false;

                                    // [지니 최적화] 예약 주문 객체 생성 (명확한 속성 할당)
                                    JumunItem 새주문 = new JumunItem
                                    {
                                        신용주문 = 신용주문,
                                        Deletetimer = 0,
                                        Screennum = Screennum,
                                        종목코드 = 주문.종목코드,
                                        종목명 = 주문.종목명,
                                        주문번호 = "+++",
                                        원주문번호 = "---",
                                        검색식 = 주문.검색식,
                                        주문값 = Value,             // *주의: 변수명 'Value' 사용
                                        시장가구분 = 3,             // 예약주문은 시장가구분 3
                                        취소시간 = 99999,           // 예약주문이라 취소시간 길게
                                        취소N주문 = 0,
                                        반복횟수 = 0,
                                        비고 = "예약주문",          // 기존 13번째 인자
                                        Pos = 주문.검색식,          // 기존 14번째 인자 (Pos에 검색식 저장)
                                        주문수량 = 주문수량,
                                        주문가격 = 주문가격,
                                        매수매도 = 주문.매수매도,
                                        비중 = 0,
                                        비중단위 = 0,
                                        취소timer = 99999,          // 기존 20번째 인자
                                        현재가 = 0,                 // *주의: 21번째 인자 0
                                        등락률 = 0,                 // *주의: 22번째 인자 0
                                        주문시간 = Get.TimeNow,
                                        미체결량 = 주문수량,        // 미체결량 초기화
                                        주문취소 = true,
                                        가동전 = false,
                                        // [중요] 잔고 정보 사용
                                        Tik_cap = Method.Find_Tik_Cap(잔고.현재가, 주문가격, Market.Market),
                                        Tik_price = 잔고.현재가,    // *주의: 28번째 인자에 '잔고.현재가' 사용
                                        수익률 = 잔고.수익률,       // *주의: 29번째 인자에 '잔고.수익률' 사용
                                        주문동기화 = false,
                                        감시번호 = 0,
                                        Order번호 = Order번호,
                                        수익구분 = 0,
                                        NXT = NXT_server,
                                        주문시간_Ticks = DateTime.Now.Ticks
                                    };

                                  await  Jumun.Add(새주문);
                                    ExecuteTrade.Que_order(새주문);
                                }

                                금액알림 = true;
                            }
                        }
                        else
                        {
                            if (금액알림)
                            {
                                금액알림 = false;

                                // 1. 알림창 출력 (문자열 보간 적용)
                                Helper.알림창_멀티("주문불가알림", $"종목명: {예약주문.종목명}\n주문가능 수량이 없어 ' 매도 ' 주문 할 수 없습니다.", 5, false);

                                // 2. 에러 기록 로그
                                Log.에러기록(" ");
                                Log.에러기록($"[주문불가알림] 종목명: {예약주문.종목명} 주문가능 수량이 없어 ' 매도 ' 주문 할 수 없습니다.");
                                Log.에러기록(" ");
                            }
                        }
                    }
                    else
                    {
                        금액알림 = true;
                        if (금액알림)
                        {
                            금액알림 = false;

                            // 1. 알림창 출력 (가독성 업그레이드)
                            Helper.알림창_멀티("주문불가알림", $"종목명: {예약주문.종목명}\n전량매도 되어 ' 매도 ' 주문 할 수 없습니다.", 5, false);

                            // 2. 에러 기록 로그
                            Log.에러기록(" ");
                            Log.에러기록($"[주문불가알림] 종목명: {예약주문.종목명} 전량매도 되어 ' 매도 ' 주문 할 수 없습니다.");
                            Log.에러기록(" ");
                        }
                    }
                }
            }
        }

        public static void RB_매수_CheckedChanged(object sender)
        {
            if (Form1.로딩완료)
            {
                if ((sender as RadioButton).Checked)
                {
                    Form1.비프음("체크");
                }
                else
                {
                    Form1.비프음("언체크");
                }
            }

            if ((sender as RadioButton).Checked)
            {
                if (!Form_Special.form.TB_수동주문_주문비.Text.StartsWith("-"))
                {
                    Form_Special.form.TB_수동주문_주문비.Text = "-" + Form_Special.form.TB_수동주문_주문비.Text;
                    Form_Special.form.TB_수동주문_주문비.Select(Form_Special.form.TB_수동주문_주문비.Text.Length, 0);
                }
            }
            else
            {
                if (Form_Special.form.TB_수동주문_주문비.Text.StartsWith("-"))
                {
                    Form_Special.form.TB_수동주문_주문비.Text = Form_Special.form.TB_수동주문_주문비.Text.Substring(1);
                    Form_Special.form.TB_수동주문_주문비.Select(Form_Special.form.TB_수동주문_주문비.Text.Length, 0);
                }
            }

            foreach (var Market in Form1.Market_Item_List.Values)
            {
                int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);
                if (Market.종목명.Equals(Form_Special.form.TB_수동주문_종목명.Text.Trim()) && 현재가 > 1)
                    수동주문비기준계산(Market.종목코드, 현재가);
            }
        }

        public static void Combo_수동주문_choice_DropDownClosed( )
        {
            if (Form_Special.form.combo_수동주문_choice.SelectedIndex.Equals(3))
            {
                if (Form_Special.form.RB_매수.Checked)
                {
                    Form_Special.form.combo_수동주문_choice.SelectedIndex = 0;
                }
            }
        }

        public static void BT_수동주문_실행_Click( ) // 잔고 수동매매 
        {
            if (Form_Special.form.TB_수동주문_종목명.Text.Length == 0)
            {
                Form1.AutoClosingAlram("선택된 종목이 없습니다.", "주문불가", 10, "에러");
            }

            Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(Form_Special.form.TB_수동주문_종목명.Text.Trim())).Value;
            if (Market != null)
            {
                string 종목코드 = Market.종목코드;
                int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);

                if (GET.Jango_state(Market.종목코드).Contains("거래정지"))
                {
                    Form1.AutoClosingAlram("[수동 주문 주문불가] 종목:" + Market.종목명 + " 거래정지 상태 입니다.", "거래정지", 10, "에러");
                }
                else
                {
                    int.TryParse(Form_Special.form.MTB_수동주문_repeat.Text, out int 수동주문_repeat);
                    int.TryParse(Form_Special.form.MTB_수동주문_cansel_time.Text, out int 수동주문_cansel_time);
                    double.TryParse(Form_Special.form.TB_수동주문_ratio.Text, out double 수동주문_ratio);
                    int.TryParse(Form_Special.form.TB_수동주문_주문가.Text, out int 수동주문_주문가);

                    if (수동주문_cansel_time == 0) 수동주문_cansel_time = 30;
                    if (수동주문_ratio == 0) 수동주문_ratio = 1;
                    if (수동주문_주문가 == 0) 수동주문_주문가 = 현재가;

                    Form_Special.form.MTB_수동주문_repeat.Text = 수동주문_repeat.ToString();
                    Form_Special.form.MTB_수동주문_cansel_time.Text = 수동주문_cansel_time.ToString();
                    Form_Special.form.TB_수동주문_ratio.Text = 수동주문_ratio.ToString();
                    Form_Special.form.TB_수동주문_주문가.Text = 수동주문_주문가.ToString();

                    int 반복횟수 = 수동주문_repeat;
                    int 반복타임 = 수동주문_cansel_time;
                    double 비중 = 수동주문_ratio;
                    int T_주문가격 = 수동주문_주문가;

                    int 선택 = Form_Special.form.combo_수동주문_choice.SelectedIndex;
                    int 주문가격 = T_주문가격;
                    int 시장가구분 = 1;

                    int Value = int.Parse(Form_Special.form.TB_수동주문_tick.Text);
                    string 매매 = "보통";

                    if (Form_Special.form.CB_수동주문_시장가.Checked)// 시장가
                    {
                        매매 = "시장가";
                        T_주문가격 = 현재가;
                        주문가격 = 0;
                        시장가구분 = 0;
                    }

                    int 주문유형 = 2;
                    if (Form_Special.form.RB_매수.Checked) 주문유형 = 1;

                    if (Get.시장시작 < Get.TimeNow && Get.TimeNow < Get.시장종료)
                    {
                        string 주문메세지 = "";
                        bool 신용주문 = false;
                        if (Form_Special.form.CB_수동신용.Checked)
                        {
                            주문메세지 = "(신용주문)\n";
                            신용주문 = true;
                        }

                        if (Form_Special.form.RB_매수.Checked)
                        {
                            if (선택 == 3)
                            {
                                Form1.AutoClosingAlram("매수 일때는 '보유주(%)' 기준을 사용할수 없습니다.", "기준알림", 10, "에러");
                            }
                            else
                            {
                                int 주문수량 = 수동주문수량계산(T_주문가격, 비중, 선택, 주문유형);

                                if (!Form1.매수증거금)
                                {
                                    Form1.AutoClosingAlram("매수 증거금이 부족합니다. 예수금을 확인해주세요.", "증거금부족", 10, "에러");
                                }
                                else
                                {
                                    if (Method.매매확인_VI_모투가능확인(Market, 1))
                                    {
                                        Task Task_Action = new Task(
                                        () =>
                                        {
                                             Helper.안전한_UI_업데이트(Form1.form1, async () =>
                                            {
                                                using (new CenterWinDialog(Form1.form1))
                                                    if (MessageBox.Show(주문메세지 + Market.종목명 + "\n(" + 매매 + ") " + T_주문가격 + "원 주문수량: " + 주문수량 + "\n" + "적용금액: " + (T_주문가격 * 주문수량) + "원 으로 '매수' 하시겠습니까 ?", "선택'매수'", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                    {
                                                        int Order번호 = GET.Order번호();
                                                        string 검색식 = "수동매수";

                                                        홀딩잔고.예수금업데이트("매수", T_주문가격, 주문수량, "주문", 종목코드, false);

                                                        int 취소N주문 = 3;
                                                        int 매수매도 = 1;
                                                        // [지니 최적화] 명확한 속성 이름으로 매칭하여 가독성 향상
                                                        JumunItem jumun = new JumunItem
                                                        {
                                                            신용주문= 신용주문,
                                                            Deletetimer = 0,
                                                            Screennum = GET.JumunScreen(),
                                                            종목코드 = 종목코드,
                                                            종목명 = Market.종목명,
                                                            주문번호 = "+++",
                                                            원주문번호 = "---",
                                                            검색식 = 검색식,
                                                            주문값 = Value,             // *주의: 변수명 'Value'
                                                            시장가구분 = 시장가구분,
                                                            취소시간 = 반복타임,        // *주의: 10번째 인자 '반복타임'
                                                            취소N주문 = 취소N주문,
                                                            반복횟수 = 반복횟수,
                                                            비고 = "",
                                                            Pos = 검색식,               // *주의: 14번째 인자에 '검색식' 재사용
                                                            주문수량 = 주문수량,
                                                            주문가격 = T_주문가격,      // *주의: 변수명 'T_주문가격'
                                                            매수매도 = 매수매도,
                                                            비중 = 비중,
                                                            비중단위 = 선택,            // *주의: 19번째 인자 변수명 '선택'
                                                            취소timer = 반복타임,       // *주의: 20번째 인자 '반복타임'
                                                            현재가 = 현재가,
                                                            등락률 = 0,
                                                            주문시간 = Get.TimeNow,
                                                            미체결량 = 주문수량,        // 미체결량 초기화
                                                            주문취소 = true,
                                                            가동전 = false,
                                                            Tik_cap = Method.Find_Tik_Cap(Market.현재가, T_주문가격, Market.Market),
                                                            Tik_price = 현재가,         // *주의: 28번째 인자 '현재가'
                                                            수익률 = 0,
                                                            주문동기화 = false,
                                                            감시번호 = 0,
                                                            Order번호 = Order번호,
                                                            수익구분 = 0,
                                                            NXT = NXT_server,
                                                            주문시간_Ticks = DateTime.Now.Ticks
                                                        };

                                                        // 리스트 추가
                                                    await    Jumun.Add(jumun);

                                                        ExecuteTrade.Que_order(jumun);//.종목명, jumun.매수매도, jumun.종목코드, jumun.주문수량, jumun.주문가격, jumun.주문값, jumun.매수매도, jumun.주문번호, jumun.검색식, jumun.Order번호);
                                                    }
                                            });
                                        });
                                        Task_Action.Start();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Form1.stockBalanceList.TryGetValue(종목코드, out Stockbalance 잔고))
                            {
                                Task Task_Action = new Task(
                                () =>
                                {
                                    int 주문수량 = Method.주문수량계산(잔고, T_주문가격, 비중, 선택);

                                    if (주문수량 > 0)
                                    {
                                        if (Method.매매확인_VI_모투가능확인(Market, 2))
                                        {
                                             Helper.안전한_UI_업데이트(Form1.form1, () =>
                                            {
                                                using (new CenterWinDialog(Form1.form1))
                                                    if (MessageBox.Show(주문메세지 + 잔고.종목명 + "\n(" + 매매 + ") " + T_주문가격 + "원 주문수량: " + 주문수량 + "\n" + "적용금액: " + (T_주문가격 * 주문수량) + "원 으로 '매도' 하시겠습니까 ?", "선택'매도'", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                    {
                                                        int 총주문가능수량 = GET.총주문가능수량(잔고);
                                                        if (주문수량 > 총주문가능수량)
                                                        {
                                                            주문수량 = 총주문가능수량;
                                                        }

                                                        if (주문수량 > 0)
                                                        {
                                                            int Order번호 = GET.Order번호();
                                                            string 검색식 = "수동매도";
                                                            int 매수매도 = 2;

                                                            신용계산.신용주문_분할매도_실행(잔고, 주문수량, async (is신용, 대출일, 수량) =>
                                                            {
                                                             await   주문(is신용, 대출일, 수량);
                                                            });

                                                            async Task 주문(bool 신용주문, string 대출일, int 주문수량)
                                                            {
                                                                JumunItem 새주문 = new JumunItem
                                                                {
                                                                    신용주문 = 신용주문,
                                                                    대출일 = 대출일,
                                                                    Deletetimer = 0,
                                                                    Screennum = GET.JumunScreen(),
                                                                    종목코드 = 종목코드,
                                                                    종목명 = 잔고.종목명,
                                                                    주문번호 = "+++",
                                                                    원주문번호 = "---",
                                                                    검색식 = 검색식,
                                                                    주문값 = Value,             // *주의: 변수명 'Value'
                                                                    시장가구분 = 시장가구분,     // *주의: 9번째 인자에 '시장가구분' 
                                                                    취소시간 = 반복타임,        // *주의: 10번째 인자 '반복타임'
                                                                    취소N주문 = 3,              // 고정값 3
                                                                    반복횟수 = 반복횟수,
                                                                    비고 = "",
                                                                    Pos = 검색식,               // 14번째 인자 (Pos에 검색식 저장)
                                                                    주문수량 = 주문수량,
                                                                    주문가격 = T_주문가격,      // *주의: 변수명 'T_주문가격'
                                                                    매수매도 = 매수매도,        // 17번째 인자
                                                                    비중 = 비중,
                                                                    비중단위 = 선택,            // *주의: 19번째 인자 변수명 '선택'
                                                                    취소timer = 반복타임,       // *주의: 20번째 인자 '반복타임' (재사용)
                                                                    현재가 = 잔고.현재가,
                                                                    등락률 = 0,
                                                                    주문시간 = Get.TimeNow,
                                                                    미체결량 = 주문수량,        // 미체결량 초기화
                                                                    주문취소 = true,
                                                                    가동전 = false,
                                                                    Tik_cap = Method.Find_Tik_Cap(잔고.현재가, T_주문가격, Market.Market),
                                                                    Tik_price = 잔고.현재가,
                                                                    수익률 = 잔고.수익률,
                                                                    주문동기화 = false,
                                                                    감시번호 = 0,
                                                                    Order번호 = Order번호,
                                                                    수익구분 = 0,
                                                                    NXT = NXT_server,
                                                                    주문시간_Ticks = DateTime.Now.Ticks
                                                                };

                                                                // 리스트 추가
                                                            await    Jumun.Add(새주문);
                                                                ExecuteTrade.Que_order(새주문);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Form1.AutoClosingAlram("선택 '매도' 주문거부  " + 잔고.종목명 + " 주문가능수량이 0 입니다.", "주문거부", 10, "에러");
                                                        }
                                                    }
                                            });
                                        }
                                    }
                                    else
                                    {
                                        Form1.AutoClosingAlram("선택 '매도' 주문거부  " + 잔고.종목명 + " 주문가능수량이 0 입니다.", "주문거부", 10, "에러");
                                    }
                                });
                                Task_Action.Start();
                            }
                        }
                    }
                    else
                    {
                        Form1.AutoClosingAlram("정규 시장이 종료 되었습니다.", "장종료", 10, "동작");
                    }
                }

                int 수동주문수량계산(int 주문가, double 비중, int index, int 주문유형)
                {
                    double 수량 = 0;
                    long 기준금 = GenieConfig.MT_buying_standard;

                    if (주문유형 == 2) 주문가 = Market.현재가;

                    if (index.Equals(0)) // 주
                    {
                        수량 = 비중;
                    }
                    else if (index.Equals(1))  //만원
                    {
                        수량 = Math.Truncate(비중 * 10000 / (double)주문가); // 버림
                    }
                    else if (index.Equals(2))  //% 기준
                    {
                        수량 = Math.Truncate(기준금 * 비중 / 100 / (double)주문가); // 버림
                    }

                    return (int)수량;
                }
            }
        }


        public static void 수동주문_주문가변화(Market_Item Market)
        {
            if (Form1.FormSpecial_Open)
            {
                Helper.안전한_UI_업데이트(Form_Special.form, () =>
                {
                    if (Market.종목명.Equals(Form_Special.form.TB_예약주문_종목명.Text))
                    {
                        int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);
                        Form_Special.form.TB_예약주문_현재가.Text = Market.현재가.ToString();

                        if (!Form_Special.form.CB_예약주문_주문가고정.Checked)
                        {
                            예약주문비기준계산(Market.종목코드, 현재가, Market.Last_price);
                        }
                        else
                        {
                            if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(2) || Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(3))
                            {
                                bool 성공 = int.TryParse(Form_Special.form.TB_예약주문_주문가.Text, out int 주문가);
                                if (성공)
                                {
                                    string para = Method.기준가고정_비(주문가, (int)Market.현재가, Market.Market);

                                    Form_Special.form.TB_예약주문_주문비계산값.Text = double.Parse(para.Split('&')[0]).ToString();

                                    if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(2))
                                    {
                                        Form_Special.form.TB_예약주문_종가매수호가.Text = para.Split('&')[1];
                                    }
                                    else if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex.Equals(3))
                                    {
                                        Form_Special.form.TB_예약주문_종가매도호가.Text = para.Split('&')[1];
                                    }
                                }
                            }
                        }
                    }
                });
            }

        }

        public static void 예약주문동기화()
        {
            foreach (var item in Form1.form1.주문예약_List.ToList())
            {
                if (item.검색식.Contains("신규'매수'"))
                {
                    if (!Form1.stockBalanceList.ContainsKey(item.종목코드))
                    {
                        REG.실시간시세등록(item.종목코드);
                    }
                    else
                    {
                        Form1.form1.주문예약_List.Remove(item);
                    }
                }
                else
                {
                    if (!Form1.stockBalanceList.ContainsKey(item.종목코드))
                    {
                        Form1.form1.주문예약_List.Remove(item);
                    }
                }
            }
         SaveToFile.   주문예약_파일저장();
        }

     

      


    }
}
