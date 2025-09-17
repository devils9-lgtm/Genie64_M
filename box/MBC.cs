using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace 지니_64.box
{
    public partial class MBC : UserControl
    {
        public static MBC box;

        public MBC()
        {
            box = this;
            InitializeComponent();
        }
        private void MBC_Load(object sender, EventArgs e)
        {
            BT_적용.Select();
        }

        private void BT_적용_Click(object sender, EventArgs e)
        {
            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                if (Form1.form1.Contains(box)) Form1.form1.Controls.Remove(box);
                Form1.form1.MBC_close = true;
                알림메세지();

            });
            Form1.form1.Form_Top_Most();
        }

        private void BT_취소_Click(object sender, EventArgs e)
        {
            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                if (Form1.form1.Contains(box)) Form1.form1.Controls.Remove(box);
                Form1.form1.MBC_close = true;

                if (Form1.MBC_sender.Equals("CB_가이드매매"))
                {
                    Form_Function.form.CB_가이드매매.Checked = Properties.Settings.Default.CB_가이드매매;
                    Form1.form1.가이드매매메세지 = true;
                    Form_Function.form.Enabled = true;
                }

                if (Form1.MBC_sender.Equals("CB_기본매매변경"))
                {
                    Form_Function.form.CB_기본매매변경.Checked = false;
                }
            });
            Form1.form1.Form_Top_Most();
        }

        private void MBC_Click(object sender, EventArgs e)
        {
            box.BringToFront();
        }

        private void LB_text_Click(object sender, EventArgs e)
        {
            box.BringToFront();
        }

        private void LB_title_Click(object sender, EventArgs e)
        {
            box.BringToFront();
        }

        public void 알림메세지()
        {
            var thread = new Thread(
            () =>
            {
                Form1.form1.Invoke((MethodInvoker)delegate ()
                {
                    switch (Form1.MBC_sender)
                    {
                        case "BT_계좌설정저장":
                            Set_default.ACC_save();          // 계좌설정 저장
                            foreach (var code in Form1.stockBalanceList.ToList())
                            {
                                Stockbalance 잔고 = code.Value;
                                잔고.보유비중 = Math.Round((double)잔고.매입금액 / (double)Properties.Settings.Default.MT_buying_standard * 100, 2);
                            }
                            Form_Jisu.form.SAVE_Jisu_avg_Checked();
                            DataManagement.SAVE_투자원금_계좌TS();
                            Properties.Settings.Default.Save();

                            RealData_Management.AVG_jisu_print("001", Form1.Acc[0].피_현재가);
                            RealData_Management.AVG_jisu_print("101", Form1.Acc[0].닥_현재가);

                            Form1.AutoClosingAlram("계좌설정 을 저장 하였습니다.", "설정저장", 3, "동작");
                            break;

                        case "BT_기본매매저장":
                            Form_Basic.기본매매_save();
                            Condition_Management.Condition_save();   // 검색식 저장
                            Properties.Settings.Default.Save();
                            Form1.AutoClosingAlram("기본매매 설정 을 저장 하였습니다.", "설정저장", 3, "동작");
                            break;
                        case "BT_반복매매저장":
                            Form_Repeat.가이드확인_반복매매저장();
                            Condition_Management.Condition_save();    // 검색식 저장
                            Properties.Settings.Default.Save();
                            Form1.AutoClosingAlram("반복매매 설정 을 저장 하였습니다.", "설정저장", 3, "동작");
                            break;
                        case "BT_계좌관리저장":
                            Form_AccountManagement.계좌관리_SAVE();
                            Condition_Management.Condition_save();   // 검색식 저장
                            Properties.Settings.Default.Save();
                            Form1.AutoClosingAlram("계좌관리 설정 을 저장 하였습니다.", "설정저장", 3, "동작");
                            break;
                        case "BT_특수매매저장":
                            Form_Special.특수매매_저장();
                            Properties.Settings.Default.Save();
                            Form1.AutoClosingAlram("특수매매 설정을 저장 하였습니다.", "설정저장", 3, "동작");
                            break;
                        case "BT_매매그룹저장":
                            Form_TradeGroup.매매그룹설정_저장();
                            Properties.Settings.Default.Save();
                            Form1.AutoClosingAlram("매매그룹 설정값 을 저장 하였습니다.", "설정저장", 3, "동작");
                            break;
                        case "BT_기능설정저장":
                            Form_Function.기능설정_저장();
                            Properties.Settings.Default.Save();
                            Form1.AutoClosingAlram("기능설정 설정값 을 저장 하였습니다.", "설정저장", 3, "동작");
                            break;
                        case "BT_대금탐색저장":
                            Form_PriceSearch.대금탐색_저장();
                            Properties.Settings.Default.Save();
                            Form1.AutoClosingAlram("대금탐색 설정 을 저장 하였습니다.", "설정저장", 3, "동작");
                            break;
                        case "BT_매수기준금적용":
                            string 알림말1 = "";
                            string 알림말2 = "";
                            long 계산금액 = 0;
                            double.TryParse(Form_AccountManagement.form.TB_매수비율.Text, out double 매수비율);
                            if (매수비율 == 0) 매수비율 = 10;
                            Properties.Settings.Default.TB_매수비율 = 매수비율;
                            Form_AccountManagement.form.TB_매수비율.Text = 매수비율.ToString();

                            if (Form_AccountManagement.form.CB_매수기준.Checked)
                            {
                                long.TryParse(Form_AccountManagement.form.TB_매수기준.Text.Replace(",", ""), out long 매수기준);
                                if (매수기준 == 0) 매수기준 = Properties.Settings.Default.MT_principal;
                                계산금액 = 매수기준;

                                Properties.Settings.Default.MT_buying_standard = (long)Math.Round(매수기준 * 매수비율 / 100, 0);

                                알림말1 = "[기준금자동적용] '매수기준금' 이 계산되어 적용됩니다.";
                                알림말2 = "[기준금자동적용]\n\n\'매수기준금' 이 계산되어 적용됩니다.";
                                Properties.Settings.Default.Today_매수기준금 = 매수기준 + "@" + Form1.today;
                            }
                            else
                            {
                                if (Properties.Settings.Default.MT_principal == 0) Properties.Settings.Default.MT_principal = 10000000;
                                Form_AccountManagement.form.TB_매수기준.Text = Properties.Settings.Default.MT_principal.ToString("N0");
                                계산금액 = Properties.Settings.Default.MT_principal;

                                Properties.Settings.Default.MT_buying_standard = (long)Math.Round(Properties.Settings.Default.MT_principal * 매수비율 / 100, 0);

                                알림말1 = "[기준금자동적용] '매수기준금' 이 투자원금으로 초기화 됩니다.";
                                알림말2 = "[기준금자동적용]\n\n\'매수기준금' 이 투자원금으로 초기화 됩니다.";
                                Properties.Settings.Default.Today_매수기준금 = Properties.Settings.Default.MT_principal + "@" + Form1.today;
                            }
                            Properties.Settings.Default.Save();

                            Form1.form1.MT_buying_standard.Text = Properties.Settings.Default.MT_buying_standard.ToString("N0");
                            Form_AccountManagement.form.TB_매수기준.Text = 계산금액.ToString("N0");

                            Form1.동작_Log("");
                            Form1.동작_Log(알림말1);
                            Form1.동작_Log("");
                            Form1.알림창(알림말2, 10, false);
                            break;

                        case "BT_손익기준금적용":
                            string 손익알림말1 = "";
                            string 손익알림말2 = "";
                            long 손익계산금액 = 0;

                            double.TryParse(Form_AccountManagement.form.TB_손익비율.Text, out double 손익비율);
                            if (손익비율 == 0) 손익비율 = 10;
                            Properties.Settings.Default.TB_손익비율 = 손익비율;
                            Form_AccountManagement.form.TB_손익비율.Text = 손익비율.ToString();

                            if (Form_AccountManagement.form.CB_손익기준.Checked)
                            {
                                long.TryParse(Form_AccountManagement.form.TB_손익기준.Text.Replace(",", ""), out long 손익기준);
                                if (손익기준 == 0) 손익기준 = Properties.Settings.Default.MT_principal;
                                손익계산금액 = 손익기준;

                                Properties.Settings.Default.MT_sonik_price = (long)Math.Round(손익기준 * 손익비율 / 100, 0);

                                손익알림말1 = "[기준금자동적용] '손익률기준금' 이 계산되어 적용됩니다.";
                                손익알림말2 = "[ 기준금자동적용 ]\n\n'손익률기준금' 이 계산되어 적용됩니다.";
                                Properties.Settings.Default.Today_손익기준금 = 손익계산금액 + "@" + Form1.today;
                            }
                            else
                            {
                                if (Properties.Settings.Default.MT_principal == 0) Properties.Settings.Default.MT_principal = 10000000;
                                Form_AccountManagement.form.TB_손익기준.Text = Properties.Settings.Default.MT_principal.ToString("N0");
                                손익계산금액 = Properties.Settings.Default.MT_principal;
                                Properties.Settings.Default.MT_sonik_price = (long)Math.Round(Properties.Settings.Default.MT_principal * 손익비율 / 100, 0);

                                손익알림말1 = "[기준금자동적용] '손익률기준금' 이 투자원금으로 초기화 됩니다.";
                                손익알림말2 = "[ 기준금자동적용 ]\n\n'손익률기준금' 이 투자원금으로 초기화 됩니다.";
                                Properties.Settings.Default.Today_손익기준금 = Properties.Settings.Default.MT_principal + "@" + Form1.today;
                            }
                            Properties.Settings.Default.Save();

                            Form1.form1.MT_sonik_price.Text = Properties.Settings.Default.MT_sonik_price.ToString("N0");
                            Form_AccountManagement.form.TB_손익기준.Text = 손익계산금액.ToString("N0");

                            Form1.동작_Log("");
                            Form1.동작_Log(손익알림말1);
                            Form1.동작_Log("");
                            Form1.알림창(손익알림말2, 10, false);
                            break;

                        case "BT_감시삭제":
                            if (Form1.form1.LB_JumunList.SelectedItems.Count > 0)
                            {
                                Form1.동작_Log(" ");

                                for (int i = 0; i < Form1.form1.LB_JumunList.SelectedItems.Count; i++)
                                {
                                    if (Form1.form1.LB_JumunList.SelectedItems[i].ToString().Split(':').Length > 3)
                                    {
                                        string num = Form1.form1.LB_JumunList.SelectedItems[i].ToString().Split(':')[Form1.form1.LB_JumunList.SelectedItems[i].ToString().Split(':').Length - 4];
                                        int.TryParse(num.Split(' ')[1].Trim(), out int 감시번호);

                                        감시주문 감시 = Form1.감시주문_List.Find(o => o.감시번호.Equals(감시번호));
                                        DataManagement.감시주문삭제(감시, "선택삭제");
                                    }
                                }
                                Form1.동작_Log(" ");

                                if (Form1.form1.일반주문확인) Tab_AccountManagement.감시주문_확인("일반주문");
                                else Tab_AccountManagement.감시주문_확인("최종주문");
                            }

                            DataManagement.리밸_감시_List_기록();
                            break;
                        case "BT_jangos_sell":
                            int 반복횟수 = int.Parse(Form1.form1.TB_jango_sell_repeat.Text);
                            int 반복타임 = int.Parse(Form1.form1.TB_jango_sell_time.Text);
                            double Value = double.Parse(Form1.form1.TB_jango_sell_value.Text);
                            int 주문 = Form1.form1.combo_jango_sell.SelectedIndex;

                            foreach (var code in Form1.stockBalanceList.ToList())
                            {
                                Stockbalance 잔고 = code.Value;
                                if (잔고.선택 && 잔고.매매가능)
                                {
                                    if (잔고.주문가능수량 > 0)
                                    {
                                        if (Method.매매확인_VI_모투가능확인(Form1.Market_Item_List[잔고.종목코드], 2))
                                        {
                                            int ScreenNumber = GET.jumunScreen(잔고.종목코드);
                                            if (ScreenNumber == 1300)
                                            {
                                                Method.주문초과알림(잔고.종목명);
                                            }
                                            else
                                            {
                                                int Order번호 = GET.Order번호();
                                                string 검색식 = "선택청산";
                                                string HogaGB = "00";
                                                int 주문가격 = Method.order_price(Value, 주문, 잔고.종목코드, 잔고.현재가);
                                                int 주문_주문가격 = 주문가격;
                                                int 주문수량 = 잔고.주문가능수량;

                                                if (주문.Equals(0))
                                                {
                                                    HogaGB = "03";
                                                    주문가격 = 0;

                                                    주문_주문가격 = Method.order_price(21, 2, 잔고.종목코드, 잔고.현재가);
                                                }

                                                DataManagement.주문가능수업데이트(잔고, "매도", 주문수량, "매도주문");

                                                JumunItem ItemList = new JumunItem(0, 0, ScreenNumber.ToString(), 잔고.종목코드, 잔고.종목명, "++", "---", 검색식, Value, 주문, 반복타임, 3, 반복횟수, "", 검색식, 주문수량, 주문_주문가격, 2, 0, 0, 반복타임, 잔고.현재가, 0, Form1.timenow,
                                                                                   주문수량, true, false, 0, Method.Find_Tik_Cap(잔고.현재가, 주문가격, 잔고.시장), 잔고.현재가, 잔고.수익률, false, 0, Order번호, 0, Form1.NXT_server); // 자동 매도 일때  주문추가 
                                                Form1.JumunItem_List.Add(ItemList);
                                                Form1.que_order(ScreenNumber.ToString(), 잔고.종목명, 2, 잔고.종목코드, 주문수량, 주문가격, HogaGB, "++", 검색식, Order번호);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Form1.AutoClosingAlram("주문가능수량 오류  " + 잔고.종목명 + " 주문가능수량이 0 입니다.", "주문거부", 10, "에러");
                                    }
                                }
                            }
                            break;
                        case "BT_재시작":
                            Form1.재시작가동("재시작", "지니_64 재시작 을 위해 HI지니_64를 실행합니다.");
                            break;
                        case "BT_미체결취소":
                            Form1.form1.RB_sell_stop.Checked = true;
                            Form1.form1.RB_buy_stop.Checked = true;

                            Form1.form1.미체결일괄취소();

                            Form1.동작_Log("");
                            Form1.동작_Log("[미체결 일괄취소] 미체결 주문을 일괄 취소 하였습니다.");
                            Form1.동작_Log("");

                            Form1.알림창("[ 미체결 일괄취소 ]\n\n미체결 주문을 일괄 취소 하였습니다.", 10, false);
                            break;
                        case "BT_condition_loading":
                            Form1.form1.axKHOpenAPI1.GetConditionLoad();
                            break;
                        case "체결대기주문취소":
                            for (int i = 0; i < Form1.JumunItem_List.Count; i++)
                            {
                                JumunItem 대기주문 = Form1.JumunItem_List[i];

                                if (대기주문.주문유형.Equals(1))
                                {
                                    대기주문.반복횟수 = 0;
                                    대기주문.취소시간 = 0;
                                    대기주문.취소timer = 0;
                                }
                            }
                            break;
                        case "BT_jagoGroup_initialization":
                            foreach (var code in Form1.stockBalanceList.ToList())
                            {
                                Stockbalance 잔고 = code.Value;
                                잔고.매매그룹 = 0;
                            }

                            DataManagement.save_jango();

                            Form1.AutoClosingAlram("모든 잔고 종목의 그룹을 초기화 하였습니다.", "초기화", 3, "동작");
                            break;
                        case "BT_미체결요청":
                            Form1.TR_주문내역요청_12();
                            Form1.TR_예수금요청_13();
                            break;
                        case "Watch_Save":
                            StreamWriter writer_;
                            string strFilePatth = Form1.startupPath + @"\Data\" + Form1.USER_ID + "__" + Properties.Settings.Default.select_account + "__\\검색식\\Test검색식.txt";

                            writer_ = System.IO.File.CreateText(strFilePatth);
                            writer_.Write("0 " + "계좌번호" + "^" + Properties.Settings.Default.select_account + ";");
                            writer_.Write("1 " + "Watch_검색식_A" + "^" + Form1.form1.combo_watch_condition_A.Text + ";");
                            writer_.Write("2 " + "Watch_검색식_B" + "^" + Form1.form1.combo_watch_condition_B.Text + ";");
                            writer_.Write("3 " + "Watch_검색식_C" + "^" + Form1.form1.combo_watch_condition_C.Text + ";");
                            writer_.Write("4 " + "Watch_검색식_D" + "^" + Form1.form1.combo_watch_condition_D.Text + ";");

                            writer_.Close();

                            Set_default.Watch_save();
                            Condition_Management.Condition_save();
                            Properties.Settings.Default.Save();
                            Form1.AutoClosingAlram("검색식TEST 설정 을 저장 하였습니다.", "설정저장", 5, "동작");
                            break;

                        case "BT_매수ALL취소":
                            foreach (var Item in Form1.JumunItem_List.ToList())
                            {
                                if (Item.주문유형.Equals(1))
                                {
                                    if (Form_Outstanding.form.CBB_미체결종목.SelectedIndex > 0)
                                    {
                                        if (Form_Outstanding.form.CBB_미체결종목.SelectedItem.Equals(Item.종목명))
                                        {
                                            Item.반복횟수 = 0;
                                            Item.취소시간 = 0;
                                            Item.취소timer = 0;
                                            Item.비고 = "매수 ALL'취소'";
                                        }
                                    }
                                    else
                                    {
                                        Item.반복횟수 = 0;
                                        Item.취소시간 = 0;
                                        Item.취소timer = 0;
                                        Item.비고 = "매수 ALL'취소'";
                                    }
                                }
                            }
                            break;
                        case "BT_매도ALL취소":
                            foreach (var Item in Form1.JumunItem_List.ToList())
                            {
                                if (Item.주문유형.Equals(2))
                                {
                                    if (Form_Outstanding.form.CBB_미체결종목.SelectedIndex > 0)
                                    {
                                        if (Form_Outstanding.form.CBB_미체결종목.SelectedItem.Equals(Item.종목명))
                                        {
                                            Item.반복횟수 = 0;
                                            Item.취소시간 = 0;
                                            Item.취소timer = 0;
                                            Item.비고 = "매도 ALL'취소'";
                                        }
                                    }
                                    else
                                    {
                                        Item.반복횟수 = 0;
                                        Item.취소시간 = 0;
                                        Item.취소timer = 0;
                                        Item.비고 = "매도 ALL'취소'";
                                    }
                                }
                            }
                            break;
                        case "CB_가이드매매":
                            Form_Function.기능설정_저장();
                            Form1.재시작가동("재시작", "지니_64 재시작 을 위해 HI지니_64를 실행합니다.");
                            break;
                        case "BT_가이드매매":
                            FileInfo File_ = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\\지니_64BackUP\\사용검색식.txt");
                            if (File_.Exists)
                            {
                                File_.Delete();
                            }

                            Form1.동작_Log(" ");
                            Form1.동작_Log(" ");
                            Form1.동작_Log("[설정로딩] 가이드매매 설정을 불러옵니다.");

                            Form1.form1.RB_sell_stop.Checked = true;
                            Form1.form1.RB_buy_stop.Checked = true;

                            Form1.form1.CB_미니시계.Checked = false;
                            Form1.form1.CB_계좌비공개.Checked = false;
                            Form1.form1.CB_종목비공개.Checked = false;

                            Guide.가이드매매설정로딩();
                            Properties.Settings.Default.Save();
                            break;
                        case "CB_기본매매변경":
                            Form1.form1.TB_setjango.Enabled = true;
                            Form1.form1.CB_계좌매입비_매수제한.Enabled = true;
                            Form1.form1.TB_계좌매입비_제한비중.Enabled = true;
                            Form1.form1.CBB_계좌매입비_제한선택.Enabled = true;
                            Form1.form1.TB_계좌매입비_현비중.Enabled = true;

                            Form1.form1.CB_잔고매입비_추매제한.Enabled = true;
                            Form1.form1.TB_잔고매입비_추매제한.Enabled = true;
                            Form1.form1.CB_misu.Enabled = true;
                            Form1.form1.MT_misu_time.Enabled = true;
                            Form1.form1.Combo_misu.Enabled = true;
                            Form1.form1.TB_misu_ratio.Enabled = true;
                            Form1.form1.TB_misu_value.Enabled = true;
                            Form1.form1.Combo_misu_jumnun.Enabled = true;
                            Form1.form1.TB_misu_repeat_time.Enabled = true;
                            Form1.form1.label_계좌매입비1.Enabled = true;
                            Form1.form1.label_계좌매입비2.Enabled = true;
                            Form1.form1.label_잔고매입비.Enabled = true;
                            Form1.form1.label_misu.Enabled = true;

                            Form_Function.form.CB_편입추가.Enabled = true;
                            Form_Function.form.CB_최종가업데이트.Enabled = true;
                            Form_Function.form.CB_신규매수정지.Enabled = true;
                            Form_Function.form.CB_추가매수정지.Enabled = true;
                            Form_Function.form.CB_VI매수취소.Enabled = true;
                            Form_Function.form.CB_VI매도취소.Enabled = true;
                            Form_Function.form.CB_상매수취소.Enabled = true;
                            Form_Function.form.CB_하매도취소.Enabled = true;
                            Form_Function.form.CB_상전량청산.Enabled = true;
                            Form_Function.form.CB_하전량청산.Enabled = true;
                            Form_Function.form.BT_가이드매매.Enabled = true;

                            Form_Function.기능설정_저장();
                            Properties.Settings.Default.Save();
                            break;

                    }
                });
            });
            thread.Start();
        }
    }
}
