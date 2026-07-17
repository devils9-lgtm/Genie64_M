using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace 지니64.box
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
             Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                if (Form1.form1.Contains(box)) Form1.form1.Controls.Remove(box);
                Form1.form1.MBC_close = true;
                알림메세지();

            });
            Form1.form1.Form_Top_Most();
        }

        private void BT_취소_Click(object sender, EventArgs e)
        {
             Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                if (Form1.form1.Contains(box)) Form1.form1.Controls.Remove(box);
                Form1.form1.MBC_close = true;

                if (Form1.MBC_sender.Equals("CB_가이드매매"))
                {
                    Form_Function.form.CB_가이드매매.Checked = GenieConfig.CB_가이드매매;
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
                 Helper.안전한_UI_업데이트(Form1.form1, async () =>
                {
                    bool isSettingsSaved = false;
                    switch (Form1.MBC_sender)
                    {
                        case "BT_계좌설정저장":
                            Set_default.ACC_save();          // 계좌설정 저장
                            foreach (var 잔고 in Form1.stockBalanceList.Values)
                            {
                                잔고.보유비중 = Math.Round((double)잔고.매입금액 / (double)GenieConfig.MT_buying_standard * 100, 2);
                            }
                            SaveToFile.투자원금_계좌TS_파일저장();

                            Log.동작기록("계좌설정 설정 을 저장 하였습니다.");
                            isSettingsSaved = true;
                            break;
                        case "BT_지수이평저장":
                            Form_Jisu.form.SAVE_지수이평();

                            RealData_Management.AVG_jisu_print("001", Form1.Acc.피_현재가);
                            RealData_Management.AVG_jisu_print("101", Form1.Acc.닥_현재가);
                            Log.동작기록("계좌설정 설정 을 저장 하였습니다.");
                            isSettingsSaved = true;
                            break;

                        case "BT_기본매매저장":
                            Form_Basic.기본매매_save();
                            SaveToFile.검색식_파일저장();   // 검색식 저장
                            Log.동작기록("기본매매 설정 을 저장 하였습니다.");
                            isSettingsSaved = true;
                            break;
                        case "BT_반복매매저장":
                            Form_Repeat.반복매매_저장();
                            SaveToFile.검색식_파일저장();    // 검색식 저장
                            Log.동작기록("반복매매 설정 을 저장 하였습니다.");
                            isSettingsSaved = true;
                            break;
                        case "BT_계좌관리저장":
                            Form_AccountManagement.계좌관리_SAVE();
                            SaveToFile.검색식_파일저장();   // 검색식 저장
                            Log.동작기록("계좌관리 설정 을 저장 하였습니다.");
                            isSettingsSaved = true;
                            break;

                        case "BT_특수매매저장":
                            Form_Special.특수매매_저장();
                            Log.동작기록("특수매매 설정 을 저장 하였습니다.");
                            isSettingsSaved = true;
                            break;
                        case "BT_매매그룹저장":
                            Form_TradeGroup.매매그룹설정_저장();
                            Log.동작기록("매매그룹 설정 을 저장 하였습니다.");
                            isSettingsSaved = true;
                            break;
                        case "BT_기능설정저장":
                            Form_Function.기능설정_저장();
                            Log.동작기록("기능설정 설정 을 저장 하였습니다.");
                            isSettingsSaved = true;
                            break;
                        case "BT_대금탐색저장":
                            Form_PriceSearch.대금탐색_저장();
                            Log.동작기록("대금탐색 설정 을 저장 하였습니다.");
                            isSettingsSaved = true;
                            break;
                        case "BT_매수기준금적용":
                            string 알림말1 = "";
                            string 알림말2 = "";
                            long 계산금액 = 0;
                            double.TryParse(Form_AccountManagement.form.TB_매수비율.Text, out double 매수비율);
                            if (매수비율 == 0) 매수비율 = 10;
                            GenieConfig.TB_매수비율 = 매수비율;
                            Form_AccountManagement.form.TB_매수비율.Text = 매수비율.ToString();

                            if (Form_AccountManagement.form.CB_매수기준.Checked)
                            {
                                long.TryParse(Form_AccountManagement.form.TB_매수기준.Text.Replace(",", ""), out long 매수기준);
                                if (매수기준 == 0) 매수기준 = GenieConfig.MT_principal;
                                계산금액 = 매수기준;

                                GenieConfig.MT_buying_standard = (long)Math.Round(매수기준 * 매수비율 / 100, 0);

                                알림말1 = "[기준금자동적용] '매수기준금' 이 계산되어 적용됩니다.";
                                알림말2 = "'매수기준금' 이 계산되어 적용됩니다.";
                                GenieConfig.Today_매수기준금 = 매수기준 + "@" + Form1.str.today;
                            }
                            else
                            {
                                if (GenieConfig.MT_principal == 0) GenieConfig.MT_principal = 10000000;
                                Form_AccountManagement.form.TB_매수기준.Text = GenieConfig.MT_principal.ToString("N0");
                                계산금액 = GenieConfig.MT_principal;

                                GenieConfig.MT_buying_standard = (long)Math.Round(GenieConfig.MT_principal * 매수비율 / 100, 0);

                                알림말1 = "[기준금자동적용] '매수기준금' 이 투자원금으로 초기화 됩니다.";
                                알림말2 = "'매수기준금' 이 투자원금으로 초기화 됩니다.";
                                GenieConfig.Today_매수기준금 = GenieConfig.MT_principal + "@" + Form1.str.today;
                            }

                            Form1.form1.MT_buying_standard.Text = GenieConfig.MT_buying_standard.ToString("N0");
                            Form_AccountManagement.form.TB_매수기준.Text = 계산금액.ToString("N0");

                            Log.동작기록("");
                            Log.동작기록(알림말1);
                            Log.동작기록("");
                            Helper.알림창_멀티("기준금자동적용",알림말2, 10, false);
                            isSettingsSaved = true;
                            break;

                        case "BT_손익기준금적용":
                            string 손익알림말1 = "";
                            string 손익알림말2 = "";
                            long 손익계산금액 = 0;

                            double.TryParse(Form_AccountManagement.form.TB_손익비율.Text, out double 손익비율);
                            if (손익비율 == 0) 손익비율 = 10;
                            GenieConfig.TB_손익비율 = 손익비율;
                            Form_AccountManagement.form.TB_손익비율.Text = 손익비율.ToString();

                            if (Form_AccountManagement.form.CB_손익기준.Checked)
                            {
                                long.TryParse(Form_AccountManagement.form.TB_손익기준.Text.Replace(",", ""), out long 손익기준);
                                if (손익기준 == 0) 손익기준 = GenieConfig.MT_principal;
                                손익계산금액 = 손익기준;

                                GenieConfig.MT_sonik_price = (long)Math.Round(손익기준 * 손익비율 / 100, 0);

                                손익알림말1 = "[기준금자동적용] '손익률기준금' 이 계산되어 적용됩니다.";
                                손익알림말2 = "'손익률기준금' 이 계산되어 적용됩니다.";
                                GenieConfig.Today_손익기준금 = 손익계산금액 + "@" + Form1.str.today;
                            }
                            else
                            {
                                if (GenieConfig.MT_principal == 0) GenieConfig.MT_principal = 10000000;
                                Form_AccountManagement.form.TB_손익기준.Text = GenieConfig.MT_principal.ToString("N0");
                                손익계산금액 = GenieConfig.MT_principal;
                                GenieConfig.MT_sonik_price = (long)Math.Round(GenieConfig.MT_principal * 손익비율 / 100, 0);

                                손익알림말1 = "[기준금자동적용] '손익률기준금' 이 투자원금으로 초기화 됩니다.";
                                손익알림말2 = "'손익률기준금' 이 투자원금으로 초기화 됩니다.";
                                GenieConfig.Today_손익기준금 = GenieConfig.MT_principal + "@" + Form1.str.today;
                            }

                            Form1.form1.MT_sonik_price.Text = GenieConfig.MT_sonik_price.ToString("N0");
                            Form_AccountManagement.form.TB_손익기준.Text = 손익계산금액.ToString("N0");

                            Log.동작기록("");
                            Log.동작기록(손익알림말1);
                            Log.동작기록("");
                            Helper.알림창_멀티("기준금자동적용",손익알림말2, 10, false);
                            isSettingsSaved = true;
                            break;

                        case "BT_감시삭제":

                            // ==========================================================
                            // [UI 이벤트 영역: 리스트박스 선택 삭제]
                            // ==========================================================
                            if (Form1.form1.LB_JumunList.SelectedItems.Count > 0)
                            {
                                Log.동작기록(" ");

                                // [최적화 1] 파일 저장을 매번 하지 않기 위해 삭제 여부만 기억해둡니다.
                                bool 삭제가_발생했음 = false;

                                for (int i = 0; i < Form1.form1.LB_JumunList.SelectedItems.Count; i++)
                                {
                                    // [최적화 2] 무거운 텍스트 쪼개기(Split) 작업을 1번만 해서 변수에 담아 재사용합니다.
                                    string 선택된항목 = Form1.form1.LB_JumunList.SelectedItems[i].ToString();
                                    string[] 항목분리 = 선택된항목.Split(':');

                                    if (항목분리.Length > 3)
                                    {
                                        string num = 항목분리[항목분리.Length - 4];
                                        int.TryParse(num.Split(' ')[1].Trim(), out int 감시번호);

                                        string key = 감시번호.ToString();
                                        Form1.감시주문_List.TryGetValue(key, out 감시주문 감시);

                                        if (감시 != null)
                                        {
                                            // [최적화 3] 여기서 호출하던 최종주문가_매도체결()은 감시주문삭제 내부에도 있으므로 중복을 제거했습니다.
                                            감시주문삭제(감시, "선택삭제");
                                            삭제가_발생했음 = true;
                                        }
                                    }
                                }
                                Log.동작기록(" ");

                                // [최적화 1] 하드디스크 부담을 줄이기 위해, 삭제 작업이 다 끝난 뒤에 딱 1번만 감시주문 파일을 저장합니다.
                                if (삭제가_발생했음)
                                {
                                    SaveToFile.리밸감시주문_파일저장();
                                }

                                if (Form1.form1.일반주문확인) Tab_AccountManagement.감시주문_확인("일반주문");
                                else Tab_AccountManagement.감시주문_확인("최종주문");
                            }

                            // ==========================================================
                            // [함수 영역: 감시주문 삭제 처리 (수동 삭제 전용)]
                            // ==========================================================
                            void 감시주문삭제(감시주문 감시, string title)
                            {
                                string 감시값확인(감시주문 item)
                                {
                                    string 감시값 = "";
                                    long 매수기준금 = GenieConfig.MT_buying_standard;
                                    double Velue = item.차수주문값;
                                    if (item.단위_기준) Velue = 매수기준금 * (double)item.차수주문값 / 100 / 10000;

                                    // [최적화 4] 불필요한 비교를 막기 위해 else if로 연결하여 CPU 연산을 줄였습니다.
                                    if (item.리밸매도기준.Equals("매도수익률")) 감시값 = $"감시가격:{item.감시주문가격}";
                                    else if (item.리밸매도기준.Equals("평가수익률")) 감시값 = $"{item.차수주문값}%(평가수익률)";
                                    else if (item.리밸매도기준.Equals("기준수익률")) 감시값 = $"{item.차수주문값}%(기준수익률)";
                                    else if (item.리밸매도기준.Equals("평가손익금")) 감시값 = $"{Velue:N2} 만(평가손익)";
                                    else if (item.리밸매도기준.Equals("예상손익금")) 감시값 = $"{Velue:N2} 만(예상손익)";
                                    else if (item.리밸매도기준.Equals("이익수익률")) 감시값 = $"이익감시가격:{item.감시주문가격}";
                                    else if (item.리밸매도기준.Equals("손절매도수익률")) 감시값 = $"손절주문가격:{item.손절주문가격}";
                                    else if (item.리밸매도기준.Equals("손절평가수익률")) 감시값 = $"{item.차수주문값}%(손절평가수익률)";
                                    else if (item.리밸매도기준.Equals("손절기준수익률")) 감시값 = $"{item.차수주문값}%(손절기준수익률)";

                                    return 감시값;
                                }

                                Tab_AccountManagement.최종주문가_매도체결(감시);
                                Log.동작기록(" ");

                                // [강제종료 에러 방어] 종목 코드가 없을 경우를 대비한 안전장치
                                long 현재가 = 0;
                                if (Form1.Market_Item_List.TryGetValue(감시.종목코드, out var mItem))
                                {
                                    현재가 = (long)mItem.현재가;
                                }
                                Log.동작기록($"[감시주문 삭제] '{title}' 종목: {감시.종목명} 감시일: {감시.감시일} 감시값: {감시값확인(감시)} 주문수량: {감시.주문수량:N0} 매도금액: {현재가 * 감시.주문수량:N0}");

                                // ==============================================================================
                                // 1. 연동 감시주문 삭제
                                // ==============================================================================
                                if (감시.검색식 != null && 감시.검색식.Contains("1차") && 감시.연동감시번호 > 0)
                                {
                                    // [최적화 5] 메모리를 잡아먹는 LINQ를 제거하고 직접 순회하여 램 점유율 하락
                                    foreach (var item in Form1.감시주문_List)
                                    {
                                        if (item.Value.연동감시번호 == 감시.연동감시번호)
                                        {
                                            string key = item.Key;
                                            감시주문 targetOrder = item.Value;

                                            if (감시.감시번호 != targetOrder.감시번호)
                                            {
                                                long price = 0;
                                                if (Form1.Market_Item_List.TryGetValue(targetOrder.종목코드, out var marketItem))
                                                {
                                                    price = (long)marketItem.현재가;
                                                }

                                                Log.동작기록($"[감시주문 '연동'삭제] '{title}' 종목: {targetOrder.종목명} 감시일: {targetOrder.감시일} 감시값: {감시값확인(targetOrder)} 주문수량: {targetOrder.주문수량:N0} 매도금액: {price * targetOrder.주문수량:N0}");
                                                Form1.감시주문_List.TryRemove(key, out _);
                                            }
                                        }
                                    }
                                }

                                // ==============================================================================
                                // 2. 최종매입가 핀셋 삭제 (지우기 전에 검사)
                                // ==============================================================================
                                if (감시.수익구분 == 7 && 감시.검색식 != null && 감시.검색식.Contains("_1차"))
                                {
                                    string 구역 = "";
                                    var match = System.Text.RegularExpressions.Regex.Match(감시.검색식, @"리밸_[A-Z]");

                                    if (match.Success)
                                    {
                                        구역 = match.Value;

                                        if (Form1.최종매입가_List.TryGetValue(감시.종목코드, out var 해당종목_리스트))
                                        {
                                            lock (해당종목_리스트)
                                            {
                                                int 삭제된_개수 = 해당종목_리스트.RemoveAll(item => item.위치 == 구역 && item.번호 == 감시.최종번호);

                                                if (삭제된_개수 > 0)
                                                {
                                                    Form1.Console_print($"[{감시.종목코드}] 수동 매입가 핀셋 삭제 완료 -> {구역} 구역의 {감시.최종번호}차 데이터 제거됨");
                                                }
                                            }
                                            // 매입가 장부가 변경되었으므로 즉시 저장
                                            SaveToFile.최종매입가_파일저장(Form1.로딩완료);
                                        }
                                    }
                                }

                                // ==============================================================================
                                // 3. 최종 마무리: 모든 볼일을 끝내고 자기 자신을 장부에서 완벽히 삭제합니다.
                                // ==============================================================================
                                var selfItem = 감시.감시번호.ToString();
                                Form1.감시주문_List.TryRemove(selfItem, out _);

                                Log.동작기록(" ");
                            }
                            break;


                        case "BT_jangos_sell":
                            int 반복횟수 = int.Parse(Form1.form1.TB_jango_sell_repeat.Text);
                            int 반복타임 = int.Parse(Form1.form1.TB_jango_sell_time.Text);
                            double 주문값 = double.Parse(Form1.form1.TB_jango_sell_value.Text);
                            int 시장가구분 = Form1.form1.combo_jango_sell.SelectedIndex;

                            foreach (var 잔고 in Form1.stockBalanceList.Values)
                            {
                                if (잔고.선택 && 잔고.매매가능)
                                {
                                    int 총주문가능수량 = GET.총주문가능수량(잔고);
                                    if (총주문가능수량 > 0)
                                    {
                                        Market_Item Market_Item = Form1.Market_Item_List[잔고.종목코드];

                                        if (Method.매매확인_VI_모투가능확인(Market_Item, 2))
                                        {
                                            int Order번호 = GET.Order번호();
                                            string 검색식 = "선택청산";
                                            int 주문가격 = Method.Order_price(주문값, 시장가구분, Market_Item.종목코드, 잔고.현재가);
                                            int 기록_주문가격 = 주문가격;
                                            int 매수매도 = 2;

                                            if (시장가구분 == 0)
                                            {
                                                기록_주문가격 = 주문가격;
                                                주문가격 = 0;
                                            }

                                            신용계산.신용주문_분할매도_실행(잔고, 총주문가능수량, async (is신용, 대출일, 수량) =>
                                            {
                                               await 주문(is신용, 대출일, 수량);
                                            });

                                            async Task 주문(bool 신용주문, string 대출일, int 주문수량)
                                            {
                                                JumunItem 새주문 = new JumunItem
                                                {
                                                    신용주문 = 신용주문,
                                                    대출일 = 대출일,
                                                    Deletetimer = 0,
                                                    Screennum = GET.JumunScreen(),
                                                    종목코드 = 잔고.종목코드,
                                                    종목명 = 잔고.종목명,
                                                    주문번호 = "+++",
                                                    원주문번호 = "---",
                                                    검색식 = 검색식,
                                                    주문값 = 주문값,
                                                    시장가구분 = 시장가구분,
                                                    취소시간 = 반복타임,       // 기존 10번째 인자
                                                    취소N주문 = 3,             // 기존 11번째 인자 (고정값 3)
                                                    반복횟수 = 반복횟수,
                                                    비고 = "",
                                                    Pos = 검색식,              // 기존 코드 순서상 14번째 인자에 '검색식'이 들어감
                                                    주문수량 = 주문수량,
                                                    주문가격 = 기록_주문가격,  // *주의: 여기 변수명이 '기록_주문가격'임
                                                    매수매도 = 매수매도,
                                                    비중 = 0,
                                                    비중단위 = 0,
                                                    취소timer = 반복타임,      // 기존 20번째 인자
                                                    현재가 = 잔고.현재가,
                                                    등락률 = 0,
                                                    주문시간 = Form1.Get.TimeNow,
                                                    미체결량 = 주문수량,
                                                    주문취소 = true,
                                                    가동전 = false,
                                                    Tik_cap = Method.Find_Tik_Cap(잔고.현재가, 주문가격, 잔고.시장),
                                                    Tik_price = 잔고.현재가,
                                                    수익률 = 잔고.수익률,
                                                    주문동기화 = false,
                                                    감시번호 = 0,
                                                    Order번호 = Order번호,
                                                    수익구분 = 0,
                                                    NXT = Form1.NXT_server,
                                                    주문시간_Ticks = DateTime.Now.Ticks
                                                };

                                                // 리스트 및 큐에 추가
                                                await Jumun.Add(새주문);
                                                ExecuteTrade.Que_order(새주문); // 이제 주석 처리된 긴 인자들 필요 없이 깔끔하게!
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
                            Form1.재시작가동("재시작", "지니64 재시작 을 위해 HI지니64를 실행합니다.");
                            break;
                        case "BT_미체결취소":
                            Form1.form1.RB_sell_stop.Checked = true;
                            Form1.form1.RB_buy_stop.Checked = true;

                            Form1.form1.미체결일괄취소();

                            Log.동작기록("");
                            Log.동작기록("[미체결 일괄취소] 미체결 주문을 일괄 취소 하였습니다.");
                            Log.동작기록("");

                            Helper.알림창_멀티("미체결 일괄취소","미체결 주문을 일괄 취소 하였습니다.", 10, false);
                            break;
                        case "BT_condition_loading":
                            REG.검색식목록조회_재조회();  //사용자 조건식 불러오기
                            break;
                        case "BT_jagoGroup_initialization":
                            foreach (var 잔고 in Form1.stockBalanceList.Values)
                            {
                                잔고.매매그룹 = 0;
                            }

                            SaveToFile.잔고_파일저장();

                            Form1.AutoClosingAlram("모든 잔고 종목의 그룹을 초기화 하였습니다.", "초기화", 3, "동작");
                            break;
                        case "BT_미체결요청":
                            _ = Helper.미체결내역동기화(false);
                            TR_요청.계좌평가현황요청("Y", "", false);
                            break;
                        case "Watch_Save":
                            Set_default.Watch_save();
                            SaveToFile.검색식_파일저장();
                            Log.동작기록("검색식TEST 설정 을 저장 하였습니다.");
                            break;

                        case "BT_매수ALL취소":
                            foreach (JumunItem item in Form1.JumunItem_List.Values)
                            {
                                if (item.매수매도 == 1)
                                {
                                    bool isSpecificStockSelected = Form_Outstanding.form.CBB_미체결종목.SelectedIndex > 0;

                                    bool shouldReset = false;
                                    if (isSpecificStockSelected)
                                    {
                                        if (Form_Outstanding.form.CBB_미체결종목.SelectedItem.Equals(item.종목명))
                                        {
                                            shouldReset = true;
                                        }
                                    }
                                    else
                                    {
                                        shouldReset = true;
                                    }

                                    if (shouldReset)
                                    {
                                        item.반복횟수 = 0;
                                        item.취소시간 = 0;
                                        item.취소timer = 0;
                                        item.비고 = "매수 ALL'취소'";
                                    }
                                }
                            }
                            break;
                        case "BT_매도ALL취소":
                            foreach (JumunItem item in Form1.JumunItem_List.Values)
                            {
                                if (item.매수매도 == 2)
                                {
                                    bool isSpecificStockSelected = Form_Outstanding.form.CBB_미체결종목.SelectedIndex > 0;

                                    bool shouldReset = false;
                                    if (isSpecificStockSelected)
                                    {
                                        if (Form_Outstanding.form.CBB_미체결종목.SelectedItem.Equals(item.종목명))
                                        {
                                            shouldReset = true;
                                        }
                                    }
                                    else
                                    {
                                        shouldReset = true;
                                    }

                                    if (shouldReset)
                                    {
                                        item.반복횟수 = 0;
                                        item.취소시간 = 0;
                                        item.취소timer = 0;
                                        item.비고 = "매수 ALL'취소'";
                                    }
                                }
                            }
                            break;
                        case "CB_가이드매매":
                            Form_Function.기능설정_저장();
                            Form1.재시작가동("재시작", "지니64 재시작 을 위해 HI지니64를 실행합니다.");
                            break;
                        case "BT_가이드매매":
                            FileInfo File_ = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\\지니64BackUP\\사용검색식.txt");
                            if (File_.Exists)
                            {
                                File_.Delete();
                            }

                            Log.동작기록(" ");
                            Log.동작기록(" ");
                            Log.동작기록("[설정로딩] 가이드매매 설정을 불러옵니다.");

                            Form1.form1.RB_sell_stop.Checked = true;
                            Form1.form1.RB_buy_stop.Checked = true;

                            Form1.form1.CB_미니시계.Checked = false;
                            Form1.form1.CB_검색보기.Checked = false;
                            Form1.form1.CB_종목비공개.Checked = false;

                            Guide.가이드매매설정로딩();
                            isSettingsSaved = true;
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
                            Form_Function.form.CB_중간가주문.Enabled = true;
                            Form_Function.form.BT_가이드매매.Enabled = true;

                            Form_Function.form.CB_NXT.Enabled = true;
                            Form_Function.form.CB_NXT_매수금지.Enabled = true;
                            Form_Function.form.CB_NXT_손실제한.Enabled = true;


                            Form_Function.기능설정_저장();
                            isSettingsSaved = true;
                            break;

                    }

                    Form1.MBC_sender = "";

                    // =================================================================
                    // [공통 갱신 로직] switch 문 밖으로 뺐습니다.
                    // =================================================================
                    if (isSettingsSaved) // ★ 저장이 발생했을 때만 실행!
                    {
                        GenieConfig.SaveAll_Genie_Config();

                        // - 전역 구조체(CanTrade) 갱신
                        Form1.CanTrade = 초기화.CanTrade.LoadFromSettings();

                        // (옵션) 로그 남기기
                        Log.동작기록("모든 설정값이 메모리에 반영되었습니다.");
						Form1.Console_print("모든 설정값이 메모리에 반영되었습니다.");
                    }
                });
            });
            thread.Start();
        }
    }
}
