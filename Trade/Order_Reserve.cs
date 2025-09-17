using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니_64
{
    public class Order_Reserve
    {
        public static int get_예약번호()
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

        public static void CBB_예약종류_SelectedIndexChanged(object sender)
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

                            int 기준가 = Market.현재가;
                            if (Form_Special.form.CBB_예약주문_예약종류.SelectedIndex == 0 || Form_Special.form.CBB_예약주문_예약종류.SelectedIndex == 1)
                            {
                                기준가 = Market.Last_price;
                            }

                            Form_Special.form.LB_예약리스트.Items.Add(주문.예약번호 + " " + 주문.종목명 + " :: 등록일: " + 주문.등록일 + " " + GET.주문유형(주문.주문유형) + " 주문가격: " + 주문.주문가 + " 주문수량: " + 주문.주문수량 + " 체결수량: " + 주문.체결수량 + " 주문비: " + Math.Round((주문.주문가 - 기준가) / (double)주문.주문가 * 100, 2));
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
                                int 주문수량 = Method.주문수량계산(잔고, 주문가, 장전매수비중, Form_Special.form.CBB_예약주문_장전매수선택.SelectedIndex, 1);
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
                                int 주문수량 = Method.주문수량계산(잔고, 주문가, 종가매수비중, Form_Special.form.CBB_예약주문_종가매수선택.SelectedIndex, 1);
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
                                int 주문수량 = Method.주문수량계산(잔고, 주문가, 장전매수비중, Form_Special.form.CBB_예약주문_장전매수선택.SelectedIndex, 1);
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
                                int 주문수량 = Method.주문수량계산(잔고, 주문가, 장전매도비중, Form_Special.form.CBB_예약주문_장전매도선택.SelectedIndex, 2);
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
                                int 주문수량 = Method.주문수량계산(잔고, 주문가, 종가매수비중, Form_Special.form.CBB_예약주문_종가매수선택.SelectedIndex, 1);
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
                                int 주문수량 = Method.주문수량계산(잔고, 주문가, 종가매도비중, Form_Special.form.CBB_예약주문_종가매도선택.SelectedIndex, 2);
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
                            Form1.알림창("[ 예약주문에러 ]\n\n종목당 예약주문은 최대 200개 까지 등록가능 합니다.", 5, false);

                            Form1.Error_Log(" ");
                            Form1.Error_Log("[예약주문에러] 종목당 예약주문은 최대 200개 까지 등록가능 합니다.");
                            Form1.Error_Log(" ");
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
                            string 주문시간 = Properties.Settings.Default.MTB_예약주문_장전주문시간.ToString();
                            if (위치.Contains("종가"))
                            {
                                주문시간 = Properties.Settings.Default.MTB_예약주문_종가주문시간.ToString();
                            }

                            int 주문유형 = 1;
                            if (위치.Contains("매도"))
                            {
                                주문유형 = 2;
                            }

                            Form_Special.form.Invoke((MethodInvoker)delegate ()
                            {
                                using (new CenterWinDialog(Form_Special.form))
                                    if (MessageBox.Show(Form_Special.form.TB_예약주문_종목명.Text + "\n' " + 위치 + " ' 주문가: " + 주문가 + "로 예약 하시겠습니까 ?" + "\n(주문시간 은 " + 주문시간 + " 입니다.)", 위치 + " 추가", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        double 기준가_ = double.Parse(Form_Special.form.TB_예약주문_기준가.Text);

                                        주문예약 주문추가 = new 주문예약(Form1.today, get_예약번호(), "0000", 주문유형, Market.종목코드, Market.종목명, 주문비, 비중, 선택, 주문가, 주문수량, 0, "[예약주문] " + 위치, "++", true, 연동, 체결완료삭제, 전량매도삭제);
                                        Form1.form1.주문예약_List.Add(주문추가);

                                        save_주문예약();

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
                        Form_Special.form.Invoke((MethodInvoker)delegate ()
                        {
                            int index = Form_Special.form.LB_예약리스트.SelectedIndex;
                            string 종목명 = Form_Special.form.LB_예약리스트.SelectedItem.ToString().Split(' ')[1];

                            using (new CenterWinDialog(Form_Special.form))
                                if (MessageBox.Show(종목명 + " :: " + 위치 + "예약 을 취소 하겠습니까? ", "주문취소 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    주문예약 종목 = Form1.form1.주문예약_List.Find(o => o.예약번호.Equals(예약번호));
                                    if (종목 != null)
                                    {
                                        JumunItem JumunItem = Form1.JumunItem_List.Find(o => o.주문번호.Equals(종목.주문번호));
                                        if (JumunItem != null)
                                        {
                                            using (new CenterWinDialog(Form_Special.form))
                                                if (MessageBox.Show(JumunItem.종목명 + " 주문번호(" + JumunItem.주문번호 + ") " + GET.주문유형(JumunItem.주문유형) + " 주문이 있습니다. 취소하시겠습니까? ", "주문취소 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                {
                                                    JumunItem.반복횟수 = 0;
                                                    JumunItem.취소시간 = 0;
                                                    JumunItem.취소timer = 0;
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
                                        save_주문예약();

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
            Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(Form_Special.form.TB_수동주문_종목명.Text.Trim())).Value;
            if (Market != null)
            {
                if (sender.Equals(Form_Special.form.TB_수동주문_종목명))
                {
                    수동종목선택(Market);
                }

                if (sender.Equals(Form_Special.form.TB_예약주문_종목명))
                {
                    예약종목선택(Market);
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
                    if (Market.현재가 > 1)
                        수동주문비기준계산(Market.종목코드, Market.현재가, Market.Market);
                }
            }

            if (sender.Equals(Form_Special.form.TB_예약주문_주문비))
            {
                Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(Form_Special.form.TB_예약주문_종목명.Text.Trim())).Value;
                if (Market != null)
                {
                    if (Market.현재가 > 1)
                        예약주문비기준계산(Market.종목코드, Market.현재가, Market.Last_price, Market.Market);
                }
            }

        }

        public static void 주문가계산(object sender)
        {
            Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(Form_Special.form.TB_수동주문_종목명.Text.Trim())).Value;
            if (Market != null)
            {
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
                        if (Market.현재가 > 1)
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
                                    int 주문가_계산 = Method.예약order_price(-i, 기준가, Market.Market);

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
                                    int 주문가_계산 = Method.예약order_price(i, 기준가, Market.Market);

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
                            Form_Special.form.TB_수동주문_주문가.Text = (Method.예약order_price(int.Parse(Form_Special.form.TB_수동주문_tick.Text.Trim()), Market.현재가, Market.Market)).ToString();

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
                            Form_Special.form.TB_수동주문_주문가.Text = (Method.예약order_price(int.Parse(Form_Special.form.TB_수동주문_tick.Text.Trim()), Market.현재가, Market.Market)).ToString();

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
                if (sender.Equals(Form_Special.form.TB_예약주문_장전매수호가))
                {
                    if (!Form1.stockBalanceList.ContainsKey(Market.종목코드))
                        Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 4;
                    else
                        Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 0;

                    int 기준가 = Market.Last_price;
                    if (Form1.timenow > 90000) 기준가 = Market.현재가;
                    int.TryParse(Form_Special.form.TB_예약주문_장전매수호가.Text, out int 장전매수);
                    Form_Special.form.TB_예약주문_장전매수호가.Text = 장전매수.ToString();

                    Form_Special.form.TB_예약주문_주문가.Text = (Method.예약order_price(장전매수, 기준가, Market.Market)).ToString();
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

                    Form_Special.form.TB_예약주문_주문가.Text = (Method.예약order_price(종가매수, Market.현재가, Market.Market)).ToString();
                    double 주문가 = double.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                    Form_Special.form.TB_예약주문_주문비계산값.Text = Math.Round(((주문가 - Market.현재가) / 주문가 * 100), 2).ToString();
                }

                if (Form1.stockBalanceList.ContainsKey(Market.종목코드))
                {
                    if (sender.Equals(Form_Special.form.TB_예약주문_장전매도호가))
                    {
                        Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 1;

                        int 기준가 = Market.Last_price;
                        if (Form1.timenow > 90000) 기준가 = Market.현재가;
                        int.TryParse(Form_Special.form.TB_예약주문_장전매도호가.Text, out int 장전매도);
                        Form_Special.form.TB_예약주문_장전매도호가.Text = 장전매도.ToString();

                        Form_Special.form.TB_예약주문_주문가.Text = (Method.예약order_price(장전매도, 기준가, Market.Market)).ToString();
                        double 주문가 = double.Parse(Form_Special.form.TB_예약주문_주문가.Text);
                        Form_Special.form.TB_예약주문_주문비계산값.Text = Math.Round(((주문가 - 기준가) / 주문가 * 100), 2).ToString();
                    }

                    if (sender.Equals(Form_Special.form.TB_예약주문_종가매도호가))
                    {
                        Form_Special.form.CBB_예약주문_예약종류.SelectedIndex = 3;

                        int.TryParse(Form_Special.form.TB_예약주문_종가매도호가.Text, out int 종가매도);
                        Form_Special.form.TB_예약주문_종가매도호가.Text = 종가매도.ToString();

                        Form_Special.form.TB_예약주문_주문가.Text = (Method.예약order_price(종가매도, Market.현재가, Market.Market)).ToString();
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
                    int 주문가_계산 = Method.예약order_price(-i, 기준가, Market);

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
                    int 주문가_계산 = Method.예약order_price(i, 기준가, Market);

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

        public static void LB_예약리스트_Click(object sender)
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

        public static void 수동종목선택(Market_Item Market)
        {
            if (Market.현재가 > 1)
            {
                Form_Special.form.TB_수동주문_종목명.Text = Market.종목명;
                Form_Special.form.TB_수동주문_price.Text = Market.현재가.ToString();

                수동주문비기준계산(Market.종목코드, Market.현재가, Market.Market);
            }
            else
            {
                신규조회 중복 = Form1.신규조회_List.Find(o => o.ItemCode.Equals(Market.종목코드));
                if (중복 == null)
                {
                    string para = "수동opt10001^Manual^" + Market.종목명 + "^" + Market.종목코드; // TR요청용 데이터

                    신규조회 Add = new 신규조회(Market.종목코드, para, 0, "수동종목");
                    Form1.신규조회_List.Add(Add);

                    if (!Form1.TR_catch_Item_List.Contains(Market.종목코드 + ";" + para))
                        Form1.TR_catch_Item_List.Enqueue(Market.종목코드 + ";" + para);
                }
            }
        }

        public static void 예약종목선택(Market_Item Market)
        {
            if (Market.현재가 > 1)
            {
                Form_Special.form.TB_예약주문_종목명.Text = Market.종목명;
                Form_Special.form.TB_예약주문_현재가.Text = Market.현재가.ToString();

                예약주문비기준계산(Market.종목코드, Market.현재가, Market.Last_price, Market.Market);
            }
            else
            {
                신규조회 중복 = Form1.신규조회_List.Find(o => o.ItemCode.Equals(Market.종목코드));
                if (중복 == null)
                {
                    string para = "예약opt10001^Manual^" + Market.종목명 + "^" + Market.종목코드; // TR요청용 데이터

                    신규조회 Add = new 신규조회(Market.종목코드, para, 0, "예약종목");
                    Form1.신규조회_List.Add(Add);

                    Form1.TR_catch_Item_List.Enqueue(Market.종목코드 + ";" + para);
                }
            }
        }

        public static void 종목선택(string 종목명)
        {
            if (Form1.form1.CB_특수매매.Checked)
            {
                Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(종목명)).Value;
                if (Market != null)
                {
                    if (Market.현재가 > 1)
                    {
                        Form_Special.form.TB_수동주문_종목명.Text = 종목명;
                        Form_Special.form.TB_수동주문_price.Text = Market.현재가.ToString();

                        Form_Special.form.TB_예약주문_종목명.Text = 종목명;
                        Form_Special.form.TB_예약주문_현재가.Text = Market.현재가.ToString();

                        수동주문비기준계산(Market.종목코드, Market.현재가, Market.Market);
                        예약주문비기준계산(Market.종목코드, Market.현재가, Market.Last_price, Market.Market);
                    }
                    else
                    {
                        신규조회 중복 = Form1.신규조회_List.Find(o => o.ItemCode.Equals(Market.종목코드));
                        if (중복 == null)
                        {
                            string para = "수동예약opt10001^Manual^" + 종목명 + "^" + Market.종목코드 + "^" + Market.Last_price; // TR요청용 데이터

                            신규조회 Add = new 신규조회(Market.종목코드, para, 0, "종목선택");
                            Form1.신규조회_List.Add(Add);

                            Form1.TR_catch_Item_List.Enqueue(Market.종목코드 + ";" + para);
                        }
                    }
                }
            }

            if (Form1.form1.CBB_최종가종목.Items.Contains(종목명))
            {
                Form1.form1.CBB_최종가종목.SelectedItem = 종목명;
            }
        }

        public static void 수동주문비기준계산(string Code, int 현재가, string Market)
        {
            string 상하 = "상한가";

            if (Form_Special.form.TB_수동주문_주문비.Text.StartsWith("-"))
            {
                상하 = "하한가";
            }

            double.TryParse(Form_Special.form.TB_수동주문_주문비.Text, out double 주문비);
            string para = Method.주문가계산(Code, 주문비, 현재가, 현재가, 상하);

            Form_Special.form.TB_수동주문_주문가.Text = para.Split('&')[0];
            Form_Special.form.TB_수동주문_주문비계산값.Text = para.Split('&')[1];
            Form_Special.form.TB_수동주문_tick.Text = para.Split('&')[2];
        }

        public static void 예약주문비기준계산(string Code, int 현재가, int 전일종가, string Market)
        {
            Form_Special.form.TB_예약주문_현재가.Text = 현재가.ToString();
            if (Form1.timenow > 90000) 전일종가 = 현재가;
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


        public static void 예약주문실행(bool 장전)
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
                                int 주문호가 = Method.호가변환(주문.주문비, 주문.종목코드, Market.Last_price, Market.Last_price, Market.Market, 주문.검색식);
                                int 상한가 = Method.상한가_하한가_구하기("상", 주문.종목코드);
                                int 하한가 = Method.상한가_하한가_구하기("하", 주문.종목코드);
                                int 주문유형 = 1;

                                if (주문.검색식.Contains("매도"))
                                {
                                    주문유형 = 2;
                                }

                                if (주문.연동)
                                {
                                    주문가 = Method.예약order_price(주문호가, Market.Last_price, Market.Market);
                                    주문수량 = Method.주문수량계산(Form1.stockBalanceList[주문.종목코드], 주문가, 주문.비중, 주문.선택, 주문유형);
                                }

                                if (주문가 > 상한가)
                                {
                                    Form1.알림창("[ 예약주문 주문가오류 ]\n\n" + 주문.검색식 + " 주문의 " + 주문.종목명 + "\n\n주문가( " + 주문.주문가.ToString("N0") + " ) 가 상한가( " + 상한가.ToString("N0") + " ) 를 초과 하여 주문할수 없습니다. ", 5, false);

                                    Form1.Error_Log(" ");
                                    Form1.Error_Log("[예약주문 주문가오류] " + 주문.검색식 + " 주문의 " + 주문.종목명 + " 주문가( " + 주문.주문가.ToString("N0") + " ) 가 상한가( " + 상한가.ToString("N0") + " ) 를 초과 하여 주문할수 없습니다. ");
                                    Form1.Error_Log(" ");
                                }
                                else if (주문가 < 하한가)
                                {
                                    Form1.알림창("[ 예약주문 주문가오류 ]\n\n" + 주문.검색식 + " 주문의 " + 주문.종목명 + "\n\n주문가( " + 주문.주문가 + " ) 가 하한가( " + 하한가 + " ) 를 미만 이어 주문할수 없습니다. ", 5, false);

                                    Form1.Error_Log(" ");
                                    Form1.Error_Log("[예약주문 주문가오류] " + 주문.검색식 + " 주문의 " + 주문.종목명 + " 주문가( " + 주문.주문가 + " ) 가 하한가( " + 하한가 + " ) 를 미만 이어 주문할수 없습니다. ");
                                    Form1.Error_Log(" ");
                                }
                                else
                                {
                                    예약주문_주문실행(주문, 주문가, 주문수량, 주문호가);
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
                                int 주문가 = 주문.주문가;
                                int 주문수량 = 주문.주문수량;
                                int 주문호가 = Method.호가변환(주문.주문비, 주문.종목코드, Market.현재가, Market.현재가, Market.Market, 주문.검색식);
                                int 상한가 = Method.상한가_하한가_구하기("상", 주문.종목코드);
                                int 하한가 = Method.상한가_하한가_구하기("하", 주문.종목코드);

                                if (주문.연동)
                                {
                                    if (Market.현재가 > 1)
                                    {
                                        예약주문_등록(주문.예약번호, Market);
                                    }
                                    else
                                    {
                                        Form1.form1.Invoke((MethodInvoker)delegate ()
                                        {
                                            신규조회 중복 = Form1.신규조회_List.Find(o => o.ItemCode.Equals(주문.종목코드));
                                            if (중복 == null)
                                            {
                                                string para = "예약실행opt10001^Manual^" + Market.종목명 + "^" + 주문.종목코드 + "^" + 주문.예약번호; // TR요청용 데이터

                                                신규조회 Add = new 신규조회(주문.종목코드, para, 0, "예약실행");
                                                Form1.신규조회_List.Add(Add);

                                                if (!Form1.TR_catch_Item_List.Contains(주문.종목코드 + ";" + para))
                                                    Form1.TR_catch_Item_List.Enqueue(주문.종목코드 + ";" + para);
                                            }
                                        });
                                    }
                                }
                                else
                                {
                                    if (주문가 > 상한가)
                                    {
                                        Form1.알림창("[ 예약주문 주문가오류 ]\n\n" + 주문.검색식 + " 주문의 " + 주문.종목명 + "\n\n주문가( " + 주문.주문가.ToString("N0") + " ) 가 상한가( " + 상한가.ToString("N0") + " ) 를 초과 하여 주문할수 없습니다. ", 5, false);

                                        Form1.Error_Log(" ");
                                        Form1.Error_Log("[예약주문 주문가오류] " + 주문.검색식 + " 주문의 " + 주문.종목명 + " 주문가( " + 주문.주문가.ToString("N0") + " ) 가 상한가( " + 상한가.ToString("N0") + " ) 를 초과 하여 주문할수 없습니다. ");
                                        Form1.Error_Log(" ");
                                    }
                                    else if (주문가 < 하한가)
                                    {
                                        Form1.알림창("[ 예약주문 주문가오류 ]\n\n" + 주문.검색식 + " 주문의 " + 주문.종목명 + "\n\n주문가( " + 주문.주문가 + " ) 가 하한가( " + 하한가 + " ) 를 미만 이어 주문할수 없습니다. ", 5, false);

                                        Form1.Error_Log(" ");
                                        Form1.Error_Log("[예약주문 주문가오류] " + 주문.검색식 + " 주문의 " + 주문.종목명 + " 주문가( " + 주문.주문가 + " ) 가 하한가( " + 하한가 + " ) 를 미만 이어 주문할수 없습니다. ");
                                        Form1.Error_Log(" ");
                                    }
                                    else
                                    {
                                        예약주문_주문실행(주문, 주문가, 주문수량, 주문호가);
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }

        public static void 예약주문_등록(int 예약번호, Market_Item Market)
        {
            주문예약 주문 = Form1.form1.주문예약_List.Find(o => o.예약번호.Equals(예약번호));
            int 주문가 = 주문.주문가;
            int 주문수량 = 주문.주문수량;
            int 주문호가 = Method.호가변환(주문.주문비, 주문.종목코드, Market.현재가, Market.현재가, Market.Market, 주문.검색식);
            int 상한가 = Method.상한가_하한가_구하기("상", 주문.종목코드);
            int 하한가 = Method.상한가_하한가_구하기("하", 주문.종목코드);
            int 주문유형 = 1;
            string 상하 = "상한가";
            if (주문.검색식.Contains("매도")) { 주문유형 = 2; 상하 = "하한가"; }

            string 주문가_계산 = Method.주문가계산(주문.종목코드, 주문.주문비, Market.현재가, Market.현재가, 상하);

            주문가 = int.Parse(주문가_계산.Split('&')[0]);
            주문호가 = int.Parse(주문가_계산.Split('&')[2]);
            주문수량 = Method.주문수량계산(Form1.stockBalanceList[주문.종목코드], 주문가, 주문.비중, 주문.선택, 주문유형);

            예약주문_주문실행(주문, 주문가, 주문수량, 주문호가);
        }

        public static void 예약주문_주문실행(주문예약 주문, int 주문가격, int 주문수량, double Value)
        {
            Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목코드.Equals(주문.종목코드)).Value;
            if (Market != null)
            {
                Stockbalance 잔고 = null;
                if (Form1.stockBalanceList.TryGetValue(Market.종목코드, out Stockbalance item)) 잔고 = item;

                if (주문.주문유형.Equals(1)) // 매수
                {
                    if (잔고 != null)
                    {
                        if (Method.매입비추매제한(잔고.보유비중))
                        {
                            if (Jisu_linkage.업종별지수연동("추매", Market))
                            {
                                if (Properties.Settings.Default.CB_계좌매입비_매수제한) // 매입비
                                {
                                    if (!Method.매입비매수제한(Properties.Settings.Default.CBB_계좌매입비_제한선택).Contains("추매"))
                                    {
                                        매수진행();
                                    }
                                }
                                else
                                {
                                    매수진행();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Jisu_linkage.업종별지수연동("신규", Market))
                        {
                            if (Properties.Settings.Default.CB_계좌매입비_매수제한) // 매입비
                            {
                                if (!Method.매입비매수제한(Properties.Settings.Default.CBB_계좌매입비_제한선택).Contains("신규"))
                                {
                                    매수진행();
                                }
                            }
                            else
                            {
                                매수진행();
                            }
                        }
                    }

                    void 매수진행()
                    {
                        if (Method.매매확인_VI_모투가능확인(Market, 주문.주문유형))
                        {
                            long 추정d2 = (Form1.Acc[0].추정D2 - (주문가격 * 주문수량));
                            if (추정d2 > 0)
                            {
                                if (잔고 != null)
                                {
                                    주문수량 = Method.최대매수금_주문수량계산(잔고, 주문수량);
                                    if (주문수량 > 0)
                                    {
                                        매수실행();
                                    }
                                    else
                                    {
                                        Form1.알림창("[ 최대매수금 초과 ]\n\n종목명: " + 잔고.종목명 + " 주문가격: " + 주문가격.ToString("N0") + "\n\n주문수량: " + 주문수량.ToString("N0") + " 주문금액: " + (주문가격 * 주문수량).ToString("N0") + "\n최대매수금 초과 하여 주문 할수 없습니다.", 5, false);

                                        Form1.Error_Log(" ");
                                        Form1.Error_Log("[최대매수금 초과] 종목명: " + 잔고.종목명 + " 주문가격: " + 주문가격.ToString("N0") + " 주문수량: " + 주문수량.ToString("N0") + " 주문금액: " + (주문가격 * 주문수량).ToString("N0") + " 최대매수금 초과 하여 주문 할수 없습니다.");
                                        Form1.Error_Log(" ");
                                    }
                                }
                                else
                                {
                                    매수실행();
                                }

                                void 매수실행()
                                {
                                    int ScreenNumber = GET.jumunScreen(Market.종목코드);
                                    if (ScreenNumber == 1300)
                                    {
                                        Method.주문초과알림(Market.종목명);
                                    }
                                    else
                                    {
                                        int Order번호 = GET.Order번호();
                                        주문.스크린번호 = ScreenNumber.ToString();

                                        DataManagement.예수금업데이트(GET.주문유형(주문.주문유형), 주문가격, 주문수량, "주문", Market.종목코드);


                                        JumunItem ItemList = new JumunItem(0, 0, ScreenNumber.ToString(), 주문.종목코드, 주문.종목명, "+++", "---", 주문.검색식, Value, 3, 99999, 0, 0, "예약주문", 주문.검색식, 주문수량, 주문가격, 주문.주문유형, 0, 0, 99999, 0, 0,
                                                                          Form1.timenow, 주문수량, true, false, 0, Method.Find_Tik_Cap(Market.현재가, 주문가격, Market.Market),
                                                                          Market.현재가, 0, false, 0, Order번호, 0, Form1.NXT_server); // 자동 매도 일때  주문추가 
                                        Form1.JumunItem_List.Add(ItemList);

                                        Form1.que_order(ScreenNumber.ToString(), 주문.종목명, 주문.주문유형, 주문.종목코드, 주문수량, 주문가격, "00", "+++", 주문.검색식, Order번호);
                                        주문.등록 = false;
                                    }
                                }
                            }
                            else
                            {
                                Form1.알림창("[ 주문불가알림 ]\n\n종목명: " + 주문.종목명 + " 주문수량: " + 주문수량.ToString("N0") + "\n\n주문가격: " + 주문가격.ToString("N0") + " 주문금액: " + (주문가격 * 주문수량).ToString("N0") + "\n\n주문가능금액이 부족하여 ' 매수 ' 주문 할수 없습니다.", 5, false);

                                Form1.Error_Log(" ");
                                Form1.Error_Log("[주문불가알림] 종목명: " + 주문.종목명 + " 주문수량: " + 주문수량.ToString("N0") + " 주문가격: " + 주문가격.ToString("N0") + " 주문금액: " + (주문가격 * 주문수량).ToString("N0") + " 주문가능금액이 부족하여 ' 매수 ' 주문 할수 없습니다.");
                                Form1.Error_Log(" ");
                            }
                        }
                    }
                }
                else
                {
                    if (잔고 != null)
                    {
                        if (잔고.주문가능수량 > 0)
                        {
                            if (Method.매매확인_VI_모투가능확인(Market, 주문.주문유형))
                            {
                                int ScreenNumber = GET.jumunScreen(Market.종목코드);
                                if (ScreenNumber == 1300)
                                {
                                    Method.주문초과알림(Market.종목명);
                                }
                                else
                                {
                                    if (Form1.stockBalanceList[주문.종목코드].주문가능수량 < 주문수량)
                                    {
                                        주문수량 = Form1.stockBalanceList[주문.종목코드].주문가능수량;
                                    }
                                    if (주문수량 > 0)
                                    {
                                        int Order번호 = GET.Order번호();
                                        주문.스크린번호 = ScreenNumber.ToString();

                                        DataManagement.주문가능수업데이트(잔고, "매도", 주문수량, "매도주문");


                                        JumunItem ItemList = new JumunItem(0, 0, ScreenNumber.ToString(), 주문.종목코드, 주문.종목명, "+++", "---", 주문.검색식, Value, 3, 99999, 0, 0, "예약주문", 주문.검색식, 주문수량, 주문가격, 주문.주문유형, 0, 0, 99999, 0, 0,
                                                                           Form1.timenow, 주문수량, true, false, 0, Method.Find_Tik_Cap(잔고.현재가, 주문가격, Market.Market),
                                                                           잔고.현재가, 잔고.수익률, false, 0, Order번호, 0, Form1.NXT_server); // 자동 매도 일때  주문추가 
                                        Form1.JumunItem_List.Add(ItemList);

                                        Form1.que_order(ScreenNumber.ToString(), 주문.종목명, 주문.주문유형, 주문.종목코드, 주문수량, 주문가격, "00", "+++", 주문.검색식, Order번호);
                                    }
                                    Form1.form1.금액알림 = true;
                                    주문.등록 = false;
                                }
                            }
                        }
                        else
                        {
                            if (Form1.form1.금액알림)
                            {
                                Form1.form1.금액알림 = false;

                                Form1.알림창("[ 주문불가알림 ]\n\n종목명: " + 주문.종목명 + "\n\n주문가능 수량이 없어  ' 매도 '주문 할수 없습니다.", 5, false);

                                Form1.Error_Log(" ");
                                Form1.Error_Log("[주문불가알림] 종목명: " + 주문.종목명 + " 주문가능 수량이 없어  ' 매도 '주문 할수 없습니다.");
                                Form1.Error_Log(" ");
                            }
                        }
                    }
                    else
                    {
                        Form1.form1.금액알림 = true;
                        if (Form1.form1.금액알림)
                        {
                            Form1.form1.금액알림 = false;
                            Form1.알림창("[ 주문불가알림 ]\n\n종목명: " + 주문.종목명 + "\n\n전량매도 되어 ' 매도 '주문 할수 없습니다.", 5, false);

                            Form1.Error_Log(" ");
                            Form1.Error_Log("[주문불가알림] 종목명: " + 주문.종목명 + " 전량매도 되어 ' 매도 '주문 할수 없습니다.");
                            Form1.Error_Log(" ");
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
                if (Market.종목명.Equals(Form_Special.form.TB_수동주문_종목명.Text.Trim()) && Market.현재가 > 1)
                    수동주문비기준계산(Market.종목코드, Market.현재가, Market.Market);
            }
        }

        public static void combo_수동주문_choice_DropDownClosed(object sender)
        {
            if (Form_Special.form.combo_수동주문_choice.SelectedIndex.Equals(3))
            {
                if (Form_Special.form.RB_매수.Checked)
                {
                    Form_Special.form.combo_수동주문_choice.SelectedIndex = 0;
                }
            }
        }

        public static void BT_수동주문_실행_Click(object sender) // 잔고 수동매매 
        {
            if (Form_Special.form.TB_수동주문_종목명.Text.Length == 0)
            {
                Form1.AutoClosingAlram("선택된 종목이 없습니다.", "주문불가", 10, "에러");
            }

            Market_Item Market = Form1.Market_Item_List.FirstOrDefault(o => o.Value.종목명.Equals(Form_Special.form.TB_수동주문_종목명.Text.Trim())).Value;
            if (Market != null)
            {
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
                    if (수동주문_주문가 == 0) 수동주문_주문가 = Market.현재가;

                    Form_Special.form.MTB_수동주문_repeat.Text = 수동주문_cansel_time.ToString();
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

                    string HogaGB = "00";
                    int Value = int.Parse(Form_Special.form.TB_수동주문_tick.Text);
                    string 시장가 = "보통";

                    if (Form_Special.form.CB_수동주문_시장가.Checked)// 시장가
                    {
                        시장가 = "시장가";
                        HogaGB = "03";
                        T_주문가격 = Market.현재가;
                        주문가격 = 0;
                        시장가구분 = 0;
                    }

                    int 주문유형 = 2;
                    if (Form_Special.form.RB_매수.Checked) 주문유형 = 1;

                    if (Form1.시장시작 < Form1.timenow && Form1.timenow < Form1.시장종료)
                    {
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
                                            Form1.form1.Invoke((MethodInvoker)delegate ()
                                            {
                                                using (new CenterWinDialog(Form1.form1))
                                                    if (MessageBox.Show(Market.종목명 + "\n(" + 시장가 + ") " + T_주문가격 + "원 주문수량: " + 주문수량 + "\n" + "적용금액: " + (T_주문가격 * 주문수량) + "원 으로 '매수' 하시겠습니까 ?", "선택'매수'", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                    {
                                                        int ScreenNumber = GET.jumunScreen(Market.종목코드);
                                                        if (ScreenNumber == 1300)
                                                        {
                                                            Method.주문초과알림(Market.종목명);
                                                        }
                                                        else
                                                        {
                                                            int Order번호 = GET.Order번호();
                                                            string 검색식 = "수동매수";

                                                            DataManagement.예수금업데이트("매수", T_주문가격, 주문수량, "주문", Market.종목코드);


                                                            JumunItem ItemList = new JumunItem(0, 0, ScreenNumber.ToString(), Market.종목코드, Market.종목명, "++", "---", 검색식, Value, 시장가구분, 반복타임, 3, 반복횟수, "", 검색식, 주문수량, T_주문가격, 1, 비중, 선택, 반복타임, Market.현재가, 0, Form1.timenow,
                                                                                               주문수량, true, false, 0, Method.Find_Tik_Cap(Market.현재가, T_주문가격, Market.Market),
                                                                                               Market.현재가, 0, false, 0, Order번호, 0, Form1.NXT_server); // 자동 매도 일때  주문추가 
                                                            Form1.JumunItem_List.Add(ItemList);

                                                            Form1.que_order(ScreenNumber.ToString(), Market.종목명, 1, Market.종목코드, 주문수량, 주문가격, HogaGB, "++", 검색식, Order번호);
                                                        }
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
                            if (Form1.stockBalanceList.TryGetValue(Market.종목코드, out Stockbalance 잔고))
                            {
                                Task Task_Action = new Task(
                                () =>
                                {
                                    int 주문수량 = Method.주문수량계산(잔고, T_주문가격, 비중, 선택, 주문유형);

                                    if (주문수량 > 0)
                                    {
                                        if (Method.매매확인_VI_모투가능확인(Market, 2))
                                        {
                                            Form1.form1.Invoke((MethodInvoker)delegate ()
                                            {
                                                using (new CenterWinDialog(Form1.form1))
                                                    if (MessageBox.Show(잔고.종목명 + "\n(" + 시장가 + ") " + T_주문가격 + "원 주문수량: " + 주문수량 + "\n" + "적용금액: " + (T_주문가격 * 주문수량) + "원 으로 '매도' 하시겠습니까 ?", "선택'매도'", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                    {
                                                        if (주문수량 > 잔고.주문가능수량)
                                                        {
                                                            주문수량 = 잔고.주문가능수량;
                                                        }

                                                        if (주문수량 > 0)
                                                        {
                                                            int ScreenNumber = GET.jumunScreen(잔고.종목코드);
                                                            if (ScreenNumber == 1300)
                                                            {
                                                                Method.주문초과알림(잔고.종목명);
                                                            }
                                                            else
                                                            {
                                                                int Order번호 = GET.Order번호();
                                                                string 검색식 = "수동매도";
                                                                DataManagement.주문가능수업데이트(잔고, "매도", 주문수량, "매도주문");

                                                                JumunItem ItemList = new JumunItem(0, 0, ScreenNumber.ToString(), Market.종목코드, 잔고.종목명, "++", "---", 검색식, Value, 시장가구분, 반복타임, 3, 반복횟수, "", 검색식, 주문수량, T_주문가격, 2, 비중, 선택, 반복타임, 잔고.현재가, 0, Form1.timenow,
                                                                                                   주문수량, true, false, 0, Method.Find_Tik_Cap(잔고.현재가, T_주문가격, Market.Market),
                                                                                                   잔고.현재가, 잔고.수익률, false, 0, Order번호, 0, Form1.NXT_server); // 자동 매도 일때  주문추가 
                                                                Form1.JumunItem_List.Add(ItemList);

                                                                Form1.que_order(ScreenNumber.ToString(), 잔고.종목명, 2, Market.종목코드, 주문수량, 주문가격, HogaGB, "++", 검색식, Order번호);
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
                    long 기준금 = Properties.Settings.Default.MT_buying_standard;

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
                Form_Special.form.Invoke((MethodInvoker)delegate ()
                {
                    //if (Market.종목명.Equals(Form_Special.form.TB_수동주문_종목명.Text))
                    //{
                    //    Form_Special.form.TB_수동주문_price.Text = Market.현재가.ToString();

                    //    if (!Form_Special.form.CB_수동주문_주문가고정.Checked)
                    //    {
                    //        수동주문비기준계산(Market.종목코드, Market.현재가, Market.Market);
                    //    }
                    //    else
                    //    {
                    //        bool 성공 = int.TryParse(Form_Special.form.TB_수동주문_주문가.Text, out int 주문가);
                    //        if (성공)
                    //        {
                    //            if (주문가 == Market.현재가)
                    //            {
                    //                Form_Special.form.TB_수동주문_주문비계산값.Text = "0";
                    //                Form_Special.form.TB_수동주문_tick.Text = "0";
                    //            }
                    //            else
                    //            {
                    //                string para = Method.기준가고정_비(주문가, (int)Market.현재가, Market.Market);

                    //                Form_Special.form.TB_수동주문_주문비계산값.Text = double.Parse(para.Split('&')[0]).ToString();
                    //                Form_Special.form.TB_수동주문_tick.Text = int.Parse(para.Split('&')[1]).ToString();
                    //            }
                    //        }
                    //    }
                    //}

                    if (Market.종목명.Equals(Form_Special.form.TB_예약주문_종목명.Text))
                    {
                        Form_Special.form.TB_예약주문_현재가.Text = Market.현재가.ToString();

                        if (!Form_Special.form.CB_예약주문_주문가고정.Checked)
                        {
                            예약주문비기준계산(Market.종목코드, Market.현재가, Market.Last_price, Market.Market);
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
                        Method.실시간시세등록(item.종목코드);
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
            save_주문예약();
        }

        public static void save_주문예약() //주문예약 저장 
        {
            Task task = new Task(() =>
            {
                Form1.form1.account_comboBox.Invoke((MethodInvoker)delegate ()  //      
                {
                    if (Form1.form1.account_comboBox.Text.Length > 0)
                    {
                        string File_Check = Application.StartupPath + @"\Data\" + Form1.USER_ID + "__" + Properties.Settings.Default.select_account + "__\\주문\\주문예약\\주문예약.txt";

                        using (StreamWriter writer_ = new StreamWriter(File_Check))
                        {
                            int i = 1;
                            foreach (주문예약 name in Form1.form1.주문예약_List)
                            {
                                if (i == Form1.form1.주문예약_List.Count)
                                {
                                    writer_.Write(name.등록일 + "^" + name.예약번호 + "^" + name.스크린번호 + "^" + name.주문유형 + "^" + name.종목코드 + "^" + name.종목명 + "^" + name.주문비 + "^" + name.비중 + "^" + name.선택 + "^" + name.주문가 + "^" + name.주문수량 + "^" + name.검색식 + "^" + name.연동 + "^" + name.체결완료삭제 + "^" + name.전량매도삭제 + "^" + i);
                                }
                                else
                                {
                                    writer_.Write(name.등록일 + "^" + name.예약번호 + "^" + name.스크린번호 + "^" + name.주문유형 + "^" + name.종목코드 + "^" + name.종목명 + "^" + name.주문비 + "^" + name.비중 + "^" + name.선택 + "^" + name.주문가 + "^" + name.주문수량 + "^" + name.검색식 + "^" + name.연동 + "^" + name.체결완료삭제 + "^" + name.전량매도삭제 + "^" + i + ";\n");
                                }
                                i++;
                            }
                        }
                    }
                });
            });
            Form1.writing_Manager.RequestTrData(task);
        }

        public static void DataLoad_주문예약(string Account) // 잔고 :: 제외,매수일자,매수검색식, 저장 불러오기
        {
            FileInfo File_Check = new FileInfo(Application.StartupPath + @"\Data\" + Form1.USER_ID + "__" + Account + "__\\주문\\주문예약\\주문예약.txt");

            if (File_Check.Exists)
            {
                try
                {
                    string path = Application.StartupPath + @"\Data\" + Form1.USER_ID + "__" + Account + "__\\주문\\주문예약\\주문예약.txt";
                    string OptionLists = File.ReadAllText(path);

                    if (OptionLists.Length > 0)
                    {
                        string[] full_예약 = OptionLists.Split(';');

                        for (int n = 0; n < full_예약.Length; n++)
                        {
                            string[] 예약 = full_예약[n].Split('^');

                            string 등록일 = 예약[0].Trim();
                            int 예약번호 = int.Parse(예약[1]);
                            string 스크린번호 = 예약[2];
                            int 주문유형 = int.Parse(예약[3]);
                            string 종목코드 = 예약[4];
                            string 종목명 = 예약[5];
                            double 주문비 = double.Parse(예약[6]);
                            double 비중 = double.Parse(예약[7]);
                            int 선택 = int.Parse(예약[8]);
                            int 주문가 = int.Parse(예약[9]);
                            int 주문수량 = int.Parse(예약[10]);
                            string 검색식 = 예약[11];
                            bool 연동 = bool.Parse(예약[12]);
                            bool 체결완료삭제 = bool.Parse(예약[13]);
                            bool 전량매도삭제 = bool.Parse(예약[14]);

                            주문예약 stock = new 주문예약(등록일, 예약번호, "0000", 주문유형, 종목코드, 종목명, 주문비, 비중, 선택, 주문가, 주문수량, 0, 검색식, "++", true, 연동, 체결완료삭제, 전량매도삭제);
                            Form1.form1.주문예약_List.Add(stock);
                        }
                    }
                }
                catch
                {
                    Form1.form1.Message_Alram("주문예약.txt 로딩 에러 파일을 확인하세요", "파일에러");
                    Form1.Error_Log("[에러 확인] 주문예약.txt 로딩 에러");
                }
            }
        }


    }
}
