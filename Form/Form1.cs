using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 지니_64.box;

// 주문제한 시간당 3600 번 

namespace 지니_64
{
    public partial class Form1 : Form
    {
        public static string serverIp = "";

        public static string 프로그램명 = "지니_64";
        public static string 버전 = "4.5.3";
        public static bool 릴리즈 = true;
        string projectName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
        string projectVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        string configPath = "";
        string configPathAndName = "";
        public static string token = "";


        public static string startupPath = Application.StartupPath; //시작할 프로그램

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool Beep(int n, int m);

        public List<string> 실시간시세_List = new List<string>();                                                       // 실시간체결_List
        public static Dictionary<string, Market_Item> Market_Item_List = new Dictionary<string, Market_Item>();        // 전종목마켓 구분
        public static List<AVG_jisu> AVG_jisu = new List<AVG_jisu>();                                                  //지수 조건
        public static Dictionary<string, ma> Min_ma_list = new Dictionary<string, ma>();
        public static Dictionary<string, ma> Day_ma_list = new Dictionary<string, ma>();

        public List<Condition> ConditionList = new List<Condition>();                                                  // 조건식 - 전체 조건식 리스트
        public static List<string> Run_condition_List = new List<string>();                                            // 조건식 - 체크박스위치 + 사용조건식 10개 제한 
        public List<RunCondition> search_condition_List = new List<RunCondition>();                                    // 조건식 - 초당 검색 검색개수저장
        public List<string> Overlap_condition_List = new List<string>();                                               // 조건식 - 조건식 중복 체크 

        public List<string> Interest_condition_List = new List<string>();                                              // 조건식 - 관심그룹 실시간 조건식 
        public List<Fail_condition> fail_condition_List = new List<Fail_condition>();                                  // 조건식 - 조건식 중복 체크 

        public static Dictionary<string, Watch> Watch_List = new Dictionary<string, Watch>();                          // Watch 검색 종목 상태

        public static List<NewCatch_A> NewCatch_List_A = new List<NewCatch_A>();                                       // 신규매수A 검색 종목리스트
        public static List<NewCatch_B> NewCatch_List_B = new List<NewCatch_B>();                                       // 신규매수B 검색 종목리스트
        public static List<NewCatch_C> NewCatch_List_C = new List<NewCatch_C>();                                       // 신규매수C 검색 종목리스트

        public List<string> NewBuyItem_List = new List<string>();                                                      // 신규주문 종목리스트
        public List<string> AnB_List = new List<string>();                                                             // AnB 검색 종목리스트
        public List<string> AnBnC_List = new List<string>();                                                           // AnBnC 검색 종목리스트

        public static List<Account> Acc = new List<Account>();                                                         // 잔고 - 계좌내용 
        public static Dictionary<string, Stockbalance> stockBalanceList = new Dictionary<string, Stockbalance>();      // 잔고 - 보유잔고리스트
        public static List<보정> 잔고보정_list = new List<보정>();                                                       // 잔고 - 보정요청 

        public static List<최종매입가> 최종매입가_List = new List<최종매입가>();                                                 // 잔고 - 최종매입가 

        public static List<재매수> Rebuystock_List = new List<재매수>();                                                       // 잔고 - 전량매도 + 보유잔고리스트
        public static List<Order> Order_list = new List<Order>();                                                             // 주문 - 요청리스트 

        public List<string> Telegram_users = new List<string>();                                                       // telegram users_ID 

        //---------------------------------------------------------------------------------//

        public static List<Repeat_condition_A> Repeat_condition_List_A = new List<Repeat_condition_A>();                       // 반복매매 검색식 아이템 등록 리스트
        public static List<Repeat_condition_B> Repeat_condition_List_B = new List<Repeat_condition_B>();                       // 반복매매 검색식 아이템 등록 리스트
        public static List<Repeat_condition_C> Repeat_condition_List_C = new List<Repeat_condition_C>();                       // 반복매매 검색식 아이템 등록 리스트
        public static List<Repeat_condition_D> Repeat_condition_List_D = new List<Repeat_condition_D>();                       // 반복매매 검색식 아이템 등록 리스트
        public static List<Repeat_condition_E> Repeat_condition_List_E = new List<Repeat_condition_E>();                       // 반복매매 검색식 아이템 등록 리스트
        public static List<Repeat_condition_F> Repeat_condition_List_F = new List<Repeat_condition_F>();                       // 반복매매 검색식 아이템 등록 리스트
        public static List<Repeat_condition_G> Repeat_condition_List_G = new List<Repeat_condition_G>();                       // 반복매매 검색식 아이템 등록 리스트
        public static List<Repeat_condition_H> Repeat_condition_List_H = new List<Repeat_condition_H>();                       // 반복매매 검색식 아이템 등록 리스트
        public static List<Repeat_condition_I> Repeat_condition_List_I = new List<Repeat_condition_I>();                       // 반복매매 검색식 아이템 등록 리스트
        public static List<Repeat_condition_J> Repeat_condition_List_J = new List<Repeat_condition_J>();                       // 반복매매 검색식 아이템 등록 리스트
        public static List<Repeat_condition_K> Repeat_condition_List_K = new List<Repeat_condition_K>();                       // 반복매매 검색식 아이템 등록 리스트
        public static List<Repeat_condition_L> Repeat_condition_List_L = new List<Repeat_condition_L>();                       // 반복매매 검색식 아이템 등록 리스트
        public static List<Repeat_condition_M> Repeat_condition_List_M = new List<Repeat_condition_M>();                       // 반복매매 검색식 아이템 등록 리스트
        public static List<Repeat_condition_N> Repeat_condition_List_N = new List<Repeat_condition_N>();                       // 반복매매 검색식 아이템 등록 리스트

        public static List<Rebal_condition_A> Rebal_condition_List_A = new List<Rebal_condition_A>();                          // 리밸런싱 검색식 아이템 등록 리스트
        public static List<Rebal_condition_B> Rebal_condition_List_B = new List<Rebal_condition_B>();                          // 리밸런싱 검색식 아이템 등록 리스트
        public static List<Rebal_condition_C> Rebal_condition_List_C = new List<Rebal_condition_C>();                          // 리밸런싱 검색식 아이템 등록 리스트
        public static List<Rebal_condition_D> Rebal_condition_List_D = new List<Rebal_condition_D>();                          // 리밸런싱 검색식 아이템 등록 리스트
        public static List<Rebal_condition_E> Rebal_condition_List_E = new List<Rebal_condition_E>();                          // 리밸런싱 검색식 아이템 등록 리스트
        public static List<Rebal_condition_F> Rebal_condition_List_F = new List<Rebal_condition_F>();                          // 리밸런싱 검색식 아이템 등록 리스트
        public static List<Rebal_condition_G> Rebal_condition_List_G = new List<Rebal_condition_G>();                          // 리밸런싱 검색식 아이템 등록 리스트

        public static List<Liquidation_condition_A> Liquidation_condition_List_A = new List<Liquidation_condition_A>();        // 리밸런싱 검색식 아이템 등록 리스트
        public static List<Liquidation_condition_B> Liquidation_condition_List_B = new List<Liquidation_condition_B>();        // 리밸런싱 검색식 아이템 등록 리스트
        public static List<Liquidation_condition_C> Liquidation_condition_List_C = new List<Liquidation_condition_C>();        // 리밸런싱 검색식 아이템 등록 리스트

        public List<string> Condition_Catch_List = new List<string>();                                                  // 검색식 실시간검색 아이템 등록 리스트

        public List<Rebal_Sell> Rebal_Sell_List = new List<Rebal_Sell>();                                               // 리밸런싱 등록 리스트
        public List<Scalping> Scalping_List = new List<Scalping>();                                                     // 스켈핑 등록 리스트

        public static List<trading_item> Trading_Item_List = new List<trading_item>();                                         // 특수설정 매매 리스트

        public static List<string> Conclusion_List = new List<string>();                                                              // 주문 - 체결 주문번호 리스트   
        public static List<string> Conclusion_remove_List = new List<string>();                                                       // 주문 - 체결완료삭제 주문번호 리스트   

        public static List<JumunItem> JumunItem_List = new List<JumunItem>();                                                  // 주문 - 주문 리스트 
        public static List<감시주문> 감시주문_List = new List<감시주문>();                                                       // 감시주문 - 감시주문 리스트 
        ConcurrentQueue<JumunItem> CancselJumun_list = new ConcurrentQueue<JumunItem>();                                // 취소 주문 리스트

        public List<주문예약> 주문예약_List = new List<주문예약>();                                                       // 주문 - 주문예약 
        public static List<체결> 체결기록list = new List<체결>();                                                                      // 주문 - 체결기록  리스트 
        public ConcurrentQueue<주문예약> 예약주문_List = new ConcurrentQueue<주문예약>();                                 // 주문 - 예약주문 현재가 대기 받아오기 

        public static List<Interest_stock> Interest_stock_List = new List<Interest_stock>();                            // 관심종목 리스트   
        public static List<string> Interest_Title_List = new List<string>();                                            // 관심종목 그룹 리스트   
        public List<string> 검색결과_List = new List<string>();                                                          // 검색결과 리스트   
        public static List<string> Interest_AutoAdd_List = new List<string>();                                          // 관심종목 자동등록 리스트   

        public static List<재주문> 재주문_LIST = new List<재주문>();                                                                    // 재주문
        public static List<검색이탈> 검색이탈_LIST = new List<검색이탈>();                                                       // 검색이탈
        public static List<string> NewBuyWrite_List = new List<string>();                                                             // 신규매수기록
        public List<string> 매매내역_List = new List<string>();                                                          // 매매내역_List
        public List<string> 기준일매매내역_List = new List<string>();                                                     // 기준일매매내역_List

        public static ConcurrentQueue<string> TR_catch_Item_List = new ConcurrentQueue<string>();
        public static List<신규조회> 신규조회_List = new List<신규조회>();
        public List<string> SearchView_List = new List<string>();
        public List<string> 신규거부_List = new List<string>();
        public List<string> 추매거부_List = new List<string>();
        public List<string> 매도거부_List = new List<string>();


        public static List<AVG_price> kospi_avg_day = new List<AVG_price>();
        public static List<AVG_price> kosdaq_avg_day = new List<AVG_price>();
        public static List<AVG_price> kospi_avg_min = new List<AVG_price>();
        public static List<AVG_price> kosdaq_avg_min = new List<AVG_price>();
        public static List<AVG_price> code_avg = new List<AVG_price>();
        public static List<NXT> NXT_list = new List<NXT>();

        /////////////////////      코스피 & 코스닥 마켓로깅     /////////////////////
        public AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

        public static bool kos_avg_update = true;

        public int 스크린Num = 1100;

        public static string 사용기간 = "";

        public string 개장일 = "0102";
        public string 수능일 = "1114";

        public static double TAX = 0.15 / 100;
        public static double 수수료 = 0;

        public static string server = "서버연결안됨";
        public static string server_알림 = "로딩중";
        public static bool NXT_server = false;
        public static string USER_ID = "***";
        public static string 접속_ID = "***";
        public string telegram_ChatID = "6194572746";
        public string Telegram_Token = "6068795633:AAFlxx-LruM9AXyxq5rBNnEgMdKk8o1pm2g"; // @JinE_bot
        public static bool 장전동시 = true;

        public static string 재접속 = "on";
        public static bool 로딩완료 = false;
        public static int 로딩완료타임 = 090000;
        public static bool 호가요청 = true;
        public static bool 지니_64n종료 = true;
        public static bool HI지니_64시작 = false;

        public static bool OcamRun = true;
        public static bool RecordON = true;
        public static bool RecordOff = true;
        public static bool 음소거 = true;
        public bool 동작실시간 = true;
        public bool 수익금or수익률 = true;
        public static bool 신규매수정지 = true;
        public static bool 추가매수정지 = true;
        public static bool 매매엑셀저장 = false;
        public static bool 체결엑셀저장 = false;
        public static bool 주문엑셀저장 = false;
        public static bool CB개장일 = false;
        public static bool CB수능일 = false;
        public static bool 예약주문_장전 = true;
        public static bool 예약주문_종가 = true;

        public static bool 매매기간_오전 = true;
        public static bool 매매기간_오후 = true;

        public bool CBscalping = false;

        public static bool FormJisu_Open = false;
        public static bool FormBasic_Open = false;
        public static bool FormRepeat_Open = false;
        public static bool FormAccountManagement_Open = false;
        public static bool FormSpecial_Open = false;
        public static bool FormPriceSearch_Open = false;
        public static bool FormTradeGroup_Open = false;
        public static bool FormFunction_Open = false;

        public static bool NXT = Properties.Settings.Default.CB_NXT;
        public static int 시장시작 = 80000;
        public static int 시장종료 = 200000;
        public static int 메인마켓시작 = 90000;
        public static int 메인마켓종료 = 153000;
        public static bool 메인마켓_주문취소 = true;

        public static string 요일 = GET.요일가져오기();
        public static string today = DateTime.Now.ToString("yyyy/MM/dd");
        public static string Month = DateTime.Now.ToString("yyyyMM");

        public static int timenow = int.Parse(DateTime.Now.ToString("HHmmss"));

        public string 이탈삭제 = "";

        public string 신규매수방법 = "";
        public static int timer_S = 0;
        public static int timer_M = 0;
        public static int timer_H = 0;
        public static int 분_180 = 0;
        public static int 분_count = 0;
        public static int 분_time = 1;

        public static int 주문_S = 0;
        public static int 주문_M = 0;
        public static int 주문_H = 0;

        public static int 신규개수A = 0;
        public static int 신규개수B = 0;
        public static int 신규개수C = 0;

        public bool 그룹지정_A = true;
        public bool 그룹지정_B = true;
        public bool 그룹지정_C = true;
        public bool 그룹지정_D = true;

        public bool 오전감시 = true;
        public bool 오후감시 = true;
        public int 오전감시시간 = 0;
        public int 오후감시시간 = 0;

        public static long 실현손익_예상 = 0;
        public static long 실현손익_시작 = 0;

        public bool Cut_A = false;
        public bool Cut_B = false;
        public bool Cut_C = false;

        public string cut_LB_A = "X";
        public string cut_LB_B = "X";
        public string cut_LB_C = "X";

        public double Cut_남길금액_A = 0;
        public double Cut_남길금액_B = 0;
        public double Cut_남길금액_C = 0;

        public static bool Run_time = true;
        public static bool Run_silson_W = true;
        public static bool Run_예상수익 = true;
        public static bool Run_예상손실 = true;

        public static bool 매입금_Run_time = false;
        public static bool 매입금_silson_W = false;
        public static bool 매입금_예상손익 = false;
        public static bool 매입금_예상손실 = false;

        public static bool Run_silson_trading = false;
        public static bool Run_예상수익_trading = false;
        public static bool Run_예상손실_trading = false;

        public static bool 일괄취소_time = true;
        public static bool 일괄취소_silson_W = true;
        public static bool 일괄취소_예상수익 = true;
        public static bool 일괄취소_예상손실 = true;

        public static bool Form_run = true;
        public bool 미수정리_미수알림 = true;
        public static long 추정예수금 = 0;

        public bool 수량알림 = true;
        public bool 금액알림 = true;
        public static bool 공휴일 = false;
        public static bool Jine_Run = true;
        public static bool Jine_Stop = true;

        public static bool 재시작 = true;

        public static bool 시장가탐색 = false;
        public bool 동작상태보기 = false;
        public static bool 매수증거금 = true;

        public static bool 순매수조회 = true;
        public static bool 신규매수_동작_피 = false;
        public static bool 추가매수_동작_피 = false;
        public static bool 신규매수_동작_닥 = false;
        public static bool 추가매수_동작_닥 = false;

        public static bool 잔고시간_ON = false;

        public bool 기준값변경 = true;
        public static bool 주문내역요청 = false;
        public static bool 예수금요청 = false;
        public static bool 예수금요청_ON = false;
        public static bool 주문내역요청_ON = false;

        public static int Ten = 0;
        public int 미체결종목_index = 0;

        public static bool 신규매수신호_A = true;
        public static bool 신규매수신호_B = true;
        public static bool 신규매수신호_C = true;

        public static int Repeat_time_A = 0;
        public static int Repeat_time_B = 0;
        public static int Repeat_time_C = 0;
        public static int Repeat_time_D = 0;
        public static int Repeat_time_E = 0;
        public static int Repeat_time_F = 0;
        public static int Repeat_time_G = 0;
        public static int Repeat_time_H = 0;
        public static int Repeat_time_I = 0;
        public static int Repeat_time_J = 0;
        public static int Repeat_time_K = 0;
        public static int Repeat_time_L = 0;
        public static int Repeat_time_M = 0;
        public static int Repeat_time_N = 0;
        public static int TT_rebalance_time_A = 0;
        public static int TT_rebalance_time_B = 0;
        public static int TT_rebalance_time_C = 0;
        public static int TT_rebalance_time_D = 0;
        public static int TT_rebalance_time_E = 0;
        public static int TT_rebalance_time_F = 0;
        public static int TT_rebalance_time_G = 0;
        public static int TT_Liqu_time_A = 0;
        public static int TT_Liqu_time_B = 0;
        public static int TT_Liqu_time_C = 0;

        public static int time_Run_time = 0;
        public static int time_Run_silson_W = 0;
        public static int time_Run_예상수익 = 0;
        public static int time_Run_예상손실 = 0;
        public double 예상수익_TS = 0;
        public string 청산검색식 = "";
        public string 주문초과종목명 = "";
        public static int 신규count = 0;

        public static int maximum_time = 10000;
        int Acc_change = 0;
        public bool 통계수익률 = false;

        public string 재가동검색식 = null;

        public Telegram.Bot.TelegramBotClient Telegram_Bot;

        messgebox.boxEX box1 = new messgebox.boxEX();
        public box.MBC MBC = new box.MBC();

        public box.Form_Outstanding out_Form = new box.Form_Outstanding();
        public box.Form_JuMun jumun_Form = new box.Form_JuMun();
        public box.Form_Conclusion conclusion_Form = new box.Form_Conclusion();
        public box.Form_Jisu Jisu_Form = new box.Form_Jisu();

        public int box1_Closetime = 10;
        public bool MBC_result = false;
        public bool MBC_close = false;
        public static string MBC_sender = "";
        public static Form1 form1;

        public bool 일반주문확인 = true;
        public int layoutindex = 0;

        public static bool 청산_A = true;
        public static bool 청산_B = true;
        public static bool 청산_C = true;

        public bool 가이드매매메세지 = true;
        public bool 가이드검색식확인 = false;

        public static JOSTRequestTrDataManager jostRequestTrDataManager;                                    // TR 전용  delay 300
        public static Today_condition_data condition_data_Manager;                                                 // 실시간 검색식 dalay 10
        public static CondotionManager condotionManager;                                                           // 검색식 가동 , 정지
        public static Writing_Manager writing_Manager;                                                      // 로그 기록 매니저 큐 
        public static Real_data Real_data_Manager;
        public static Real_price Real_price_Manager;
        public static Real_Hoga Real_Hoga_Manager;

        public Form1()
        {
            form1 = this;

            InitializeComponent();
            Invalidate();

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.StartupPath + @"\Data");
            if (!di.Exists) { di.Create(); }

            jostRequestTrDataManager = JOSTRequestTrDataManager.GetInstance();
            condotionManager = CondotionManager.GetInstance();
            writing_Manager = Writing_Manager.GetInstance();
            condition_data_Manager = Today_condition_data.GetInstance();
            Real_data_Manager = Real_data.GetInstance();
            Real_price_Manager = Real_price.GetInstance();
            Real_Hoga_Manager = Real_Hoga.GetInstance();

            jostRequestTrDataManager.Run();
            condotionManager.Run();
            writing_Manager.Run();
            condition_data_Manager.Run();
            Real_data_Manager.Run();
            Real_price_Manager.Run();
            Real_Hoga_Manager.Run();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            axKHOpenAPI1.OnEventConnect += API_OnEventConnect;                  // 로그인 상태 에러 메세지 
            axKHOpenAPI1.OnReceiveTrData += API_OnReceiveTrData;                // tr요청 데이터 수신
            axKHOpenAPI1.OnReceiveMsg += API_OnReceiveMsg;                      // Msg
            axKHOpenAPI1.OnReceiveConditionVer += API_OnReceiveConditionVer;    // 조건식리스트 수신
            axKHOpenAPI1.OnReceiveRealData += API_OnReceiveRealData;            // 실시간 데이터 수신
            axKHOpenAPI1.OnReceiveRealCondition += API_OnReceiveRealCondition;  // 실시간 검색식 종목 편입or이탈
            axKHOpenAPI1.OnReceiveTrCondition += API_OnReceiveTRCondition;      // 조건식검색결과 TR 수신
            axKHOpenAPI1.OnReceiveChejanData += API_OnReceiveChejanData;        // 실시간 체결 잔고 data

            RB_sell_stop.Checked = true;
            RB_buy_stop.Checked = true;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Form_Outstanding.form.Outstanding_DataGridView, new object[] { true });
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, JanGo_dataGridView, new object[] { true });
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Form_JuMun.form.JuMun_dataGridView, new object[] { true });
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Form_Conclusion.form.Conclusion_DataGridView, new object[] { true });
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, dataGridView_watch_A, new object[] { true });
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, dataGridView_watch_B, new object[] { true });
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, dataGridView_watch_C, new object[] { true });
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, dataGridView_watch_D, new object[] { true });
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, DGV_통계, new object[] { true });
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, DGV_통계B, new object[] { true });

            typeof(ListBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, LB_검색결과n관심리스트, new object[] { true });
            typeof(ListBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, LB_관심_A, new object[] { true });
            typeof(ListBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, LB_관심_B, new object[] { true });
            typeof(ListBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, LB_관심_C, new object[] { true });
            typeof(ListBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, LB_JumunList, new object[] { true });
            typeof(ListBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, LB_error, new object[] { true });
            typeof(ListBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, LB_Log, new object[] { true });

            typeof(TabControl).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, tab_잔고, new object[] { true });
            typeof(TabControl).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, tab_주문, new object[] { true });
            typeof(TabControl).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, tab_체결, new object[] { true });

            typeof(TextBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, MT_principal, new object[] { true });
            typeof(TextBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, TB_추정자산, new object[] { true });
            typeof(TextBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, TB_증가자산, new object[] { true });
            typeof(TextBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, TB_D2, new object[] { true });
            typeof(TextBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, TB_추정D2, new object[] { true });
            typeof(TextBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, TB_매입금, new object[] { true });
            typeof(TextBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, TB_평가금, new object[] { true });
            typeof(TextBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, TB_평가손익금, new object[] { true });
            typeof(TextBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, TB_평가손익율, new object[] { true });
            typeof(TextBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, TB_실현손익, new object[] { true });
            typeof(TextBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, TB_실현손익율, new object[] { true });
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            DataManagement.릴리즈체크();

            configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), projectName, projectVersion);
            configPathAndName = Path.Combine(configPath, projectName + ".config");

            Setting_backup.GetProject(projectName, projectVersion, configPath, configPathAndName);

            string direc = configPath;
            DirectoryInfo di = new DirectoryInfo(direc);
            if (di.Exists == false)
            {
                di.Create();
            }

            form1.Controls.Add(out_Form);
            out_Form.Show();
            out_Form.BringToFront();

            TP_주문내역.Controls.Add(jumun_Form);
            jumun_Form.Show();
            jumun_Form.BringToFront();

            TP_체결내역.Controls.Add(conclusion_Form);
            conclusion_Form.Show();
            conclusion_Form.BringToFront();

            form1.Controls.Add(Jisu_Form);
            Jisu_Form.BringToFront();
            Jisu_Form.Hide();

            Account add = new Account(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "0", "0", "0", 0, 0, 0, 0, 0, 0, 0, 0,
                "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값"
                , 0, 0, 0, 0, 0, 0, 0, "", "", "", "", "", "", "", "", "", "", "", "");
            Acc.Add(add);

            this.Text = 프로그램명 + " " + 버전 + " / " + USER_ID + " / " + 요일 + " / [ " + DateTime.Now.ToLongTimeString() + " ] / " + server + " / " + server_알림 + " / ( ※ 키움서버 주문제한 : 시간당 3600회 입니다.)";

            if (Properties.Settings.Default.MT_closetime <= timenow) 지니_64n종료 = false;

            종목감추기6.Hide();

            if (Properties.Settings.Default.TB_Record_Run <= timenow)
            {
                OcamRun = false;
            }

            chart_Month.Hide();
            chart_Day.Hide();

            종목감추기_잔고.Hide();
            Form_Outstanding.form.종목감추기_미체결.Hide();
            Form_Conclusion.form.종목감추기_체결.Hide();
            종목감추기_로그.Hide();
            Form_JuMun.form.종목감추기_주문.Hide();
            종목감추기6.Hide();
            종목감추기7.Hide();
            label_Search.Hide();
            CBB_SearchCondition.Hide();
            P_Search_List.Hide();
            Search_List.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " | 지니_64오토스탁");
            CBB_layout.Show();
            CBB_layout.BringToFront();
            CBB_layout.SelectedIndex = 0;
            최종매입가View.Hide();
            최종매입가View.SendToBack();
            Form1.form1.CBB_최종가종목.Hide();
            Form1.form1.BT_종목업.Hide();
            Form1.form1.BT_종목다운.Hide();
            Form1.form1.CBB_최종가종목.SendToBack();
            Form1.form1.BT_종목업.SendToBack();
            Form1.form1.BT_종목다운.SendToBack();

            DataManagement.검색식_TEST폴더삭제();

            GET.신규매수방법();
            Set_default.Form_load();

            Form_Basic Form_Basic = new Form_Basic();
            Form_Repeat Form_Repeat = new Form_Repeat();
            Form_AccountManagement Form_AccountManagement = new Form_AccountManagement();
            Form_Special Form_Special = new Form_Special();
            Form_PriceSearch Form_PriceSearch = new Form_PriceSearch();
            Form_TradeGroup Form_TradeGroup = new Form_TradeGroup();
            Form_Function Form_Function = new Form_Function();

            Form_Basic.Location = new System.Drawing.Point(-32000, -32000);
            Form_Repeat.Location = new System.Drawing.Point(-32000, -32000);
            Form_AccountManagement.Location = new System.Drawing.Point(-32000, -32000);
            Form_Special.Location = new System.Drawing.Point(-32000, -32000);
            Form_PriceSearch.Location = new System.Drawing.Point(-32000, -32000);
            Form_TradeGroup.Location = new System.Drawing.Point(-32000, -32000);
            Form_Function.Location = new System.Drawing.Point(-32000, -32000);

            Form_Basic.form.TopMost = true;
            Form_Repeat.form.TopMost = true;
            Form_AccountManagement.form.TopMost = true;
            Form_Special.form.TopMost = true;
            Form_PriceSearch.form.TopMost = true;
            Form_TradeGroup.form.TopMost = true;
            Form_Function.form.TopMost = true;

            Form_Basic.form.Show();
            Form_Repeat.form.Show();
            Form_AccountManagement.form.Show();
            Form_Special.form.Show();
            Form_PriceSearch.form.Show();
            Form_TradeGroup.form.Show();
            Form_Function.form.Show();

            Form_Basic.form.Hide();
            Form_Repeat.form.Hide();
            Form_AccountManagement.form.Hide();
            Form_Special.form.Hide();
            Form_PriceSearch.form.Hide();
            Form_TradeGroup.form.Hide();
            Form_Function.form.Hide();

            this.TopMost = true;
            this.TopMost = Properties.Settings.Default.CB_지니_64항상위에;
            if (Properties.Settings.Default.CB_최대화로실행) this.WindowState = FormWindowState.Maximized;
            if (Properties.Settings.Default.CB_지니_64크기고정)
            {
                Form1.form1.MaximumSize = new Size(1936, 1054);
                Form1.form1.MinimumSize = new Size(1936, 1054);
                Form1.form1.Size = new Size(1936, 1054);
            }

            if (!Properties.Settings.Default.신규횟수날짜.Equals(today))
            {
                Properties.Settings.Default.신규횟수 = 0;
                Properties.Settings.Default.신규횟수날짜 = today;
            }

            tab_주문.SelectedIndex = Properties.Settings.Default.주문_tap;

            FormPrint.잔고표시내역();
            시장가탐색 = Condition_Management.시장가대금탐색();

            Guide.공휴일계산();
            Guide.GuideLoding();
            Market_load.Add_AVG_jisu();

            Login_Start();
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////                  잔고매매 시작                     /////////////////
        public static string 매매시작 = "";
        public int 검색식_tick = 0;

        public void 잔고_매매()
        {
            Thread thread_매매 = new Thread(매매);
            thread_매매.IsBackground = true;
            thread_매매.Start();

            void 매매()
            {
                while (true)
                {
                    if (this.IsDisposed || this.Disposing) break; // 작업을 중단

                    if (매매시작.Equals(""))
                    {
                        if (condotionManager.Condotion_count() == 0)
                        {
                            매매시작 = "로딩중";
                            Console.WriteLine("검색식 요청 개수 점검: " + condotionManager.Condotion_count());

                            동작_Log("사용자 검색식 불러오기 완료.");

                            Load_completion();
                        }
                    }

                    if (매매시작.Equals("로딩완료"))
                    {
                        매매시작 = "주문내역요청";
                        Console.WriteLine("주문내역요청");

                        TR_주문내역요청_6();
                    }

                    if (매매시작.Equals("차트조회요청"))
                    {
                        Console.WriteLine("#####  차트조회요청 종료: " + DateTime.Now.ToString("HH:mm:ss.ffff") + " 남은수 : " + jostRequestTrDataManager.count());

                        Form1.form1.Invoke((MethodInvoker)delegate ()  //      
                        {
                            form1.LB_Log.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss :: ") + "차트로딩 중 - " + jostRequestTrDataManager.count());
                        });

                        if (jostRequestTrDataManager.count() == 0)
                            매매시작 = "주문로딩완료";
                    }

                    if (매매시작.Equals("주문로딩완료"))
                    {
                        매매시작 = "매매시작";
                        Console.WriteLine("매매시작");

                        var thr_250_A = new Thread(Sleep_250_A);
                        thr_250_A.Start();

                        var thr_250_B = new Thread(Sleep_250_B);
                        thr_250_B.Start();

                        var thr_500 = new Thread(Sleep_500);
                        thr_500.Start();

                        var thr_1000 = new Thread(Sleep_1000);
                        thr_1000.Start();

                        if (Properties.Settings.Default.CB_가이드매매)
                        {
                            Form1.form1.Invoke((MethodInvoker)delegate ()
                            {
                                Form1.form1.CB_기본매매.Enabled = true;
                                Form1.form1.CB_반복매매.Enabled = true;
                                Form1.form1.CB_계좌관리.Enabled = true;
                                Form1.form1.CB_특수매매.Enabled = true;
                                Form1.form1.CB_매매그룹.Enabled = true;
                                Form1.form1.CB_대금탐색.Enabled = true;
                                Form1.form1.CB_기능설정.Enabled = true;
                                Form1.form1.CB_Jisu_avgset.Enabled = true;
                            });
                        }

                        break;
                    }

                    Console.WriteLine("매매 쓰레드");
                    Thread.Sleep(1000);
                }
            }

            void Sleep_250_A()
            {
                while (true)
                {
                    if (!Form_run) break;

                    Form1.form1.Invoke((MethodInvoker)delegate ()
                    {
                        timer.Tr_timer();
                        시세요청();
                    });

                    Thread.Sleep(250);
                }
            }

            void Sleep_250_B()
            {
                while (true)
                {
                    if (!Form_run) break;

                    Form1.form1.Invoke((MethodInvoker)delegate ()
                    {
                        서버주문전달();
                        예약주문_RUN();
                        Condition_Management.검색식사용제한();
                    });

                    Thread.Sleep(250);
                }
            }

            void Sleep_500()
            {
                while (true)
                {
                    if (!Form_run) break;

                    Form1.form1.Invoke((MethodInvoker)delegate ()
                    {
                        Trading_Run();
                    });

                    Thread.Sleep(500);
                }
            }

            void Sleep_1000()
            {
                while (true)
                {
                    if (!Form_run) break;

                    Form1.form1.Invoke((MethodInvoker)delegate ()
                    {
                    Form_Print();
                    });

                    Thread.Sleep(1000);
                }
            }
        }

        private void Trading_Run()
        {
            if (server_알림.Contains("마켓"))
            {
                if (RB_sell_run.Checked)
                {
                    Tab_Basic.계좌청산();
                    Misu_liquidation.미수정리();
                    Tab_AccountManagement.수익금기준손절();
                }

                foreach (var code in stockBalanceList.ToList())
                {
                    Stockbalance 잔고 = code.Value;

                    if (!잔고.종목상태.Contains("거래정지") && !잔고.종목상태.Contains("동시호가"))
                    {
                        if (RB_buy_run.Checked || RB_sell_run.Checked)
                        {
                            if (잔고.주문가능수량 > 0) Method.매입금매매제한(잔고);
                            if (잔고.주문가능수량 > 0) Tab_Repeat.반복매매_USE(잔고);
                            if (잔고.주문가능수량 > 0) Tab_AccountManagement.Rebalancing_USE(잔고);

                            if (RB_sell_run.Checked)
                            {
                                if (잔고.주문가능수량 > 0) Tab_AccountManagement.Liquidation_USE(잔고);
                                if (잔고.주문가능수량 > 0) Tab_Basic.TimeSell(잔고);

                                if (잔고.주문가능수량 > 0) Tab_AccountManagement.리밸감시_감시중(잔고);
                                if (잔고.주문가능수량 > 0) Tab_Basic.잔고_익절(잔고);
                                if (잔고.주문가능수량 > 0) Tab_Basic.잔고_보전(잔고);
                                if (잔고.주문가능수량 > 0) Tab_Basic.잔고_손실매도(잔고);
                                if (잔고.주문가능수량 > 0) Tab_Basic.트레일링스탑(잔고);

                                Tab_Basic.상하_전량매도(잔고);
                            }

                            if (Properties.Settings.Default.CBB_In_group_A > 0 || Properties.Settings.Default.CBB_In_group_B > 0 || Properties.Settings.Default.CBB_In_group_C > 0 || Properties.Settings.Default.CBB_In_group_D > 0)
                                if (잔고.주문가능수량 > 0) Tab_Special.그룹지정매매(잔고);

                            if (잔고.주문가능수량 > 0) Tab_Special.매매일_기준거래(잔고, "리얼");

                            if (잔고.주문가능수량 <= 0 && !잔고.전량매도)
                            {
                                JumunItem 주문 = JumunItem_List.Find(o => o.종목코드.Equals(잔고.종목코드));
                                if (주문 == null)
                                {
                                    if (잔고.보유수량 != 잔고.주문가능수량)
                                    {
                                        보정 item = 잔고보정_list.Find(o => o.코드.Equals(잔고.종목코드));
                                        if (item == null)
                                        {
                                            잔고보정_list.Add(new 보정(잔고.종목코드, 잔고.종목명, 0));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Form_Print()
        {
            if (server_알림.Contains("마켓") || server_알림.Contains("동시"))
            {
                timer.delay_timer();
                timer.Threadcount();
            }
            timer.UpdateNowDateTime();

            FormPrint.acc_print();

                GridView_Print.JanGo_dataGridView_print();
                if (로딩완료) GridView_Print.OUT_DGV_print();
                if (Properties.Settings.Default.CBB_Watch_ID_A > 0) Tab_Watch.DGV_watch_print("A:");
                if (Properties.Settings.Default.CBB_Watch_ID_B > 0) Tab_Watch.DGV_watch_print("B:");
                if (Properties.Settings.Default.CBB_Watch_ID_C > 0) Tab_Watch.DGV_watch_print("C:");
                if (Properties.Settings.Default.CBB_Watch_ID_D > 0) Tab_Watch.DGV_watch_print("D:");

                Tab_InterestGroup.관심자동등록실행();
                Condition_Management.검색식사용불가_강제정지();
                FormPrint.동작상태();
        }


        ///////////////                  잔고매매 시작                     /////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////
        /////////////////          자동 로그인 및 로그인 상태       ///////////////////

        private void Autologin_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Autologin_CheckBox.Checked)
            {
                비프음("체크");
                Autologin_CheckBox.Text = "자동";
                Autologin_CheckBox.ForeColor = Color.Red;
                Autologin_CheckBox.BackColor = Color.MistyRose;

                BT_Login.ForeColor = Color.Red;
                Login_Start();
            }
            else
            {
                비프음("언체크");
                Autologin_CheckBox.Text = "수동";

                Autologin_CheckBox.ForeColor = Color.Black;
                Autologin_CheckBox.BackColor = Color.Gainsboro;

                BT_Login.ForeColor = Color.Black;
            }
        }

        private async void Login_Start()
        {
            // 1. 서버 연결 상태 확인
            try
            {
                // 여기에 사용자가 제공한 코드를 넣습니다.
                string appKey = Properties.Settings.Default.appKey;
                string appsecret = Properties.Settings.Default.appsecret;

                var token = await GetAccessToken(appKey, appsecret);

                Console.WriteLine("Access Token: " + token);
            }
            catch (Exception ex)
            {
                Console.WriteLine("오류 발생: " + ex.Message);
            }
        }


        // 로그인 상태 알림          
        private void API_OnEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            MBC_sender = "";
            if (e.nErrCode == 0)
            {
                Console.WriteLine("로그인 성공:: " + DateTime.Now.ToString("HH:mm:ss.fff"));

                string Server_Gubun = axKHOpenAPI1.GetLoginInfo("GetServerGubun"); // 접속 서버 구분
                USER_ID = axKHOpenAPI1.GetLoginInfo("USER_ID"); // 계좌 정보 반환 

                if (Server_Gubun == "1") // 모의, 실투 구분 하기 
                {
                    server = "모의투자";
                    동작_Log("모의투자로 접속 하였습니다.");

                    수수료 = Properties.Settings.Default.TB_mo_commission / (double)100;
                }
                else
                {
                    server = "실서버";
                    동작_Log("실서버로 접속 하였습니다.");

                    수수료 = Properties.Settings.Default.TB_sil_commission / (double)100;
                }

                동작_Log("조건식 불러오기 시작");
                axKHOpenAPI1.GetConditionLoad(); //사용자 조건식 불러오기

                Market_load.play();

                string account = axKHOpenAPI1.GetLoginInfo("ACCLIST"); // 계좌 정보 반환 
                string[] accountArr = account.Split(';'); // 계좌번호 ";"기준 잘라서 문자 배열 만들기 

                foreach (string accountlist in accountArr)
                {
                    if (accountlist.Length > 0)
                    {
                        account_comboBox.Items.Add(accountlist);
                    }
                }

                동작_Log("계좌번호 불러오기 완료.");
                if (account_comboBox.Items.Contains(Properties.Settings.Default.account_comboBox))
                {
                    account_comboBox.SelectedItem = Properties.Settings.Default.account_comboBox;
                }
                else
                {
                    CB_계좌비공개.Checked = false;
                    CB_종목비공개.Checked = false;
                    중요메세지("계좌변경알림", "사용계좌가 변경되었습니다. 계좌번호 선택후 API계좌 비밀번호 입력한다음 계좌번호를 다시 선택하여 주세요.\n로그인이 완료되면 원할한 매매를 위해서 재시작 하는것이 좋습니다.");
                }
            }
            else
            {
                동작_Log("로그인 실패 하였습니다.");
                Error_Log("로그인 실패 하였습니다.");
                중요메세지("로그인 실패", "알수없는 이유로 로그인에 실패 하였습니다. 재시작 하세요.");
            }

            string fid = "10;12;14;17;18;291;295;251;252;253;254;255";
            axKHOpenAPI1.SetRealReg(GET.ScreenNum(), "001", fid, "1");
            axKHOpenAPI1.SetRealReg(GET.ScreenNum(), "101", fid, "1");
        }

        /////////////////          자동 로그인 및 로그인 상태       ///////////////////
        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////        계좌선택 및 계좌OR잔고 TR요청         //////////////////

        private void 주문카운터()
        {
            주문_S++;
            주문_M++;
            주문_H++;
        }

        private void Account_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account_comboBox.SelectedIndex > -1)
            {
                if (Acc_change > 0)
                {
                    AutoClosingAlram(Acc_change + "초 후 계좌변경 가능합니다. 계좌 변경은 1분에 한번씩 할수 있습니다.", "제한알림", 10, "에러");
                    account_comboBox.Text = Properties.Settings.Default.select_account;
                    return;
                }

                if (로딩완료)
                {
                    tab_체결.SelectedIndex = 1;

                    로딩완료 = false;
                    매매시작 = "";

                    Acc_change = 60;
                    AutoClosingAlram("매매가 중단 됩니다. 매매 설정과 검색식을 확인후 매매 하세요", "주의알림", 10, "에러");

                    var thread = new Thread(
                    () =>
                    {
                        for (int n = Acc_change; n > -1; n--)
                        {
                            if (Acc_change > 0) Acc_change--;
                            Delay(1000);
                        }
                    });
                    thread.Start();
                }

                동작_Log(account_comboBox.Text + " 선택 되었습니다.");

                JanGo_dataGridView.Rows.Clear();
                Form_Outstanding.form.Outstanding_DataGridView.Rows.Clear();
                Form_JuMun.form.JuMun_dataGridView.Rows.Clear();
                Form_Conclusion.form.Conclusion_DataGridView.Rows.Clear();
                DGV_통계.Rows.Clear();
                DGV_통계B.Rows.Clear();

                dataGridView_watch_A.Rows.Clear();
                dataGridView_watch_B.Rows.Clear();
                dataGridView_watch_C.Rows.Clear();
                dataGridView_watch_D.Rows.Clear();

                Watch_List.Clear();

                NewCatch_List_A.Clear();
                NewCatch_List_B.Clear();
                NewCatch_List_C.Clear();
                NewBuyItem_List.Clear();
                AnB_List.Clear();
                AnBnC_List.Clear();
                stockBalanceList.Clear();
                잔고보정_list.Clear();
                최종매입가_List.Clear();

                Rebuystock_List.Clear();
                JumunItem_List.Clear();
                Order_list.Clear();
                Repeat_condition_List_A.Clear();
                Repeat_condition_List_B.Clear();
                Repeat_condition_List_C.Clear();
                Repeat_condition_List_D.Clear();
                Repeat_condition_List_E.Clear();
                Repeat_condition_List_F.Clear();
                Repeat_condition_List_G.Clear();
                Repeat_condition_List_H.Clear();
                Repeat_condition_List_I.Clear();
                Repeat_condition_List_J.Clear();
                Repeat_condition_List_K.Clear();
                Repeat_condition_List_L.Clear();
                Repeat_condition_List_M.Clear();
                Repeat_condition_List_N.Clear();
                Rebal_condition_List_A.Clear();
                Rebal_condition_List_B.Clear();
                Rebal_condition_List_C.Clear();
                Rebal_condition_List_D.Clear();
                Rebal_condition_List_E.Clear();
                Rebal_condition_List_F.Clear();
                Rebal_condition_List_G.Clear();
                Liquidation_condition_List_A.Clear();
                Liquidation_condition_List_B.Clear();
                Liquidation_condition_List_C.Clear();

                Rebal_Sell_List.Clear();
                Scalping_List.Clear();
                Trading_Item_List.Clear();
                Conclusion_List.Clear();
                Conclusion_remove_List.Clear();
                감시주문_List.Clear();
                주문예약_List.Clear();
                체결기록list.Clear();

                검색결과_List.Clear();

                검색결과_List.Add("-");
                검색결과_List.Add("-");

                재주문_LIST.Clear();
                검색이탈_LIST.Clear();
                NewBuyWrite_List.Clear();

                매매내역_List.Clear();
                기준일매매내역_List.Clear();
                신규조회_List.Clear();
                SearchView_List.Clear();
                신규거부_List.Clear();

                Repeat_time_A = 0;
                Repeat_time_B = 0;
                Repeat_time_C = 0;
                Repeat_time_D = 0;
                Repeat_time_E = 0;
                Repeat_time_F = 0;
                Repeat_time_G = 0;
                Repeat_time_H = 0;
                Repeat_time_I = 0;
                Repeat_time_J = 0;
                Repeat_time_K = 0;
                Repeat_time_L = 0;
                Repeat_time_M = 0;
                Repeat_time_N = 0;
                TT_rebalance_time_A = 0;
                TT_rebalance_time_B = 0;
                TT_rebalance_time_C = 0;
                TT_rebalance_time_D = 0;
                TT_rebalance_time_E = 0;
                TT_rebalance_time_F = 0;
                TT_rebalance_time_G = 0;
                TT_Liqu_time_A = 0;
                TT_Liqu_time_B = 0;
                TT_Liqu_time_C = 0;

                time_Run_time = 0;
                time_Run_silson_W = 0;
                time_Run_예상수익 = 0;
                time_Run_예상손실 = 0;

                신규개수A = 0;
                신규개수B = 0;
                신규개수C = 0;

                실현손익_예상 = 0;
                Cut_A = false;
                Cut_B = false;
                Cut_C = false;

                Cut_남길금액_A = 0;
                Cut_남길금액_B = 0;
                Cut_남길금액_C = 0;

                Run_time = true;
                Run_silson_W = true;
                Run_예상수익 = true;
                Run_예상손실 = true;

                매입금_Run_time = false;
                매입금_silson_W = false;
                매입금_예상손익 = false;
                매입금_예상손실 = false;

                Run_silson_trading = false;
                Run_예상수익_trading = false;
                Run_예상손실_trading = false;

                일괄취소_time = true;
                일괄취소_silson_W = true;
                일괄취소_예상수익 = true;
                일괄취소_예상손실 = true;

                미수정리_미수알림 = true;
                추정예수금 = 0;

                수량알림 = true;
                금액알림 = true;
                신규매수_동작_피 = false;
                추가매수_동작_피 = false;
                신규매수_동작_닥 = false;
                추가매수_동작_닥 = false;

                신규매수신호_A = true;
                신규매수신호_B = true;
                신규매수신호_C = true;

                for (int i = 0; i < CancselJumun_list.Count; i++)
                {
                    CancselJumun_list.TryDequeue(out JumunItem 종목);
                }

                for (int i = 0; i < 예약주문_List.Count; i++)
                {
                    예약주문_List.TryDequeue(out 주문예약 주문);
                }

                while (TR_catch_Item_List.Count > 0)
                {
                    bool result = TR_catch_Item_List.TryDequeue(out string item);
                }

                Acc.Clear();

                Account add = new Account(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "0", "0", "0", 0, 0, 0, 0, 0, 0, 0, 0, "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값",
                                                     "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", "기본값", 0, 0, 0, 0, 0, 0, 0, "", "", "", "", "", "", "", "", "", "", "", "");
                Acc.Add(add);

                Properties.Settings.Default.select_account = account_comboBox.Text;

                DataManagement.Drictroy_Create();
                DataManagement.파일삭제();

                DataManagement.DataLoad_Acc(Properties.Settings.Default.select_account);
                DataManagement.acc_save_DataLoad(Properties.Settings.Default.select_account); //세이브 잔고 불러오기 
                DataManagement.최종매입가_불러오기(Properties.Settings.Default.select_account);
                DataManagement.DATALOAD_주문리스트(Properties.Settings.Default.select_account);
                Order_Reserve.DataLoad_주문예약(Properties.Settings.Default.select_account);

                Tab_InterestGroup.DataLoad_관심그룹_List_기록();
                Tab_InterestGroup.DataLoad_관심그룹_Title_기록();
                DataManagement.리밸_감시_List_로딩();
                Console.WriteLine("잔고 로딩 시작 합니다.");

                Loadig_Kospi_1();
            }
        }

        //로딩시작
        private void Loadig_Kospi_1()
        {
            Console.WriteLine("지수시세 요청 :: " + server + "    " + DateTime.Now.ToString("HH:mm:ss.fff"));
            동작_Log("사용자 정보를 불러옵니다. ");

            //코스피 시세요청
            axKHOpenAPI1.SetInputValue("시장구분", "0");
            axKHOpenAPI1.SetInputValue("업종코드", "001"); // 업종코드 = 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고

            int result_C = axKHOpenAPI1.CommRqData("Loadig_Kospi_1", "opt20001", 0, "1101");

            if (result_C == 0)
            {
                Console.WriteLine("코스피 업종 시세 요청 성공:: " + server + "    " + DateTime.Now.ToString("HH:mm:ss.fff"));
            }
            else
            {
                Error_Log("코스피 업종 시세를 불러 오기 실패. 메세지: " + GET.오류코드(result_C));
            }
        }
        private void Loadig_Kosdaq_2()
        {
            //코스닥 시세요청
            axKHOpenAPI1.SetInputValue("시장구분", "0");
            axKHOpenAPI1.SetInputValue("업종코드", "101"); // 업종코드 = 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고

            int result_D = axKHOpenAPI1.CommRqData("Loadig_Kosdaq_2", "opt20001", 0, "1102");

            if (result_D == 0)
            {
                Console.WriteLine("코스닥 업종 시세 요청 성공:: " + server + "    " + DateTime.Now.ToString("HH:mm:ss.fff"));
            }
            else
            {
                Error_Log("코스닥 업종 시세조회 실패 메세지: " + GET.오류코드(result_D));
            }
        }
        private void TR_예수금조회_3()
        {
            Task JO_Manager = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("계좌번호", Properties.Settings.Default.select_account);
                axKHOpenAPI1.SetInputValue("비밀번호", "");
                axKHOpenAPI1.SetInputValue("상장폐지조회구분", "0");
                axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
                axKHOpenAPI1.SetInputValue("거래소구분", "KRX");

                int result_A = axKHOpenAPI1.CommRqData("TR_예수금조회_3", "opw00004", 0, "1103");

                if (result_A == 0)
                {
                    Console.WriteLine(Properties.Settings.Default.select_account + " 예수금 조회");
                }
                else
                {
                    Error_Log(Properties.Settings.Default.select_account + " 예수금 조회 실패" + " 에러메세지: " + GET.오류코드(result_A));
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager); // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }
        private void TR_실현손익요청_4()
        {
            Task JO_Manager = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("계좌번호", Properties.Settings.Default.select_account);
                axKHOpenAPI1.SetInputValue("시작일자", DateTime.Now.ToString("yyyyMMdd"));
                axKHOpenAPI1.SetInputValue("종료일자", DateTime.Now.ToString("yyyyMMdd"));

                int result_opt10073_A = axKHOpenAPI1.CommRqData("TR_실현손익요청_4", "opt10074", 0, "1104");

                if (result_opt10073_A == 0)
                {
                    Console.WriteLine(Properties.Settings.Default.select_account + " 실현손익 요청");
                }
                else
                {
                    Error_Log(Properties.Settings.Default.select_account + " 실현손익 불러오기 실패 메세지: " + GET.오류코드(result_opt10073_A));
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager);
        }
        private void TR_잔고요청_5()
        {
            Task JO_Manager = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("계좌번호", Properties.Settings.Default.select_account);
                axKHOpenAPI1.SetInputValue("비밀번호", "");
                axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
                axKHOpenAPI1.SetInputValue("조회구분", "1");
                axKHOpenAPI1.SetInputValue("거래소구분", "KRX");

                int result_C = axKHOpenAPI1.CommRqData("TR_잔고요청_5", "opw00018", 0, "1105");
                if (result_C == 0)
                {
                    Console.WriteLine(Properties.Settings.Default.select_account + " 잔고 요청");
                }
                else
                {
                    Error_Log(Properties.Settings.Default.select_account + " 잔고 요청 실패 메세지: " + GET.오류코드(result_C));
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager);  // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }
        private void TR_주문내역요청_6()
        {
            Task JO_Manager = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("계좌번호", Properties.Settings.Default.select_account);
                axKHOpenAPI1.SetInputValue("전체종목구분", "0");
                axKHOpenAPI1.SetInputValue("매매구분", "0");
                axKHOpenAPI1.SetInputValue("종목코드", "0");
                axKHOpenAPI1.SetInputValue("체결구분", "1");
                axKHOpenAPI1.SetInputValue("거래소구분", "1");

                int result_opt10075_A = axKHOpenAPI1.CommRqData("TR_주문내역요청_6", "opt10075", 0, "1106");
                if (result_opt10075_A == 0)
                {
                    Console.WriteLine(Properties.Settings.Default.select_account + " 주문 접수내역 요청 ");
                }
                else
                {
                    Error_Log(Properties.Settings.Default.select_account + " 주문 접수내역 요청실패 메세지: " + GET.오류코드(result_opt10075_A));
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager); // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }

        //로딩완료
        public static void TR_투자자순매수요청_코스피_7(bool 로딩중)
        {
            Task JO_Manager = new Task(() =>
            {
                form1.axKHOpenAPI1.SetInputValue("시장구분", "0");  // 0: 코스피  1: 코스닥
                form1.axKHOpenAPI1.SetInputValue("금액수량구분", "0"); //  금액수량구분 = 금액:0, 수량: 1
                form1.axKHOpenAPI1.SetInputValue("기준일자", DateTime.Now.ToString("yyyyMMdd")); //   기준일자 = YYYYMMDD(20170101 연도4자리, 월 2자리, 일 2자리 형식)
                form1.axKHOpenAPI1.SetInputValue("거래소구분", "1");

                int result_C = form1.axKHOpenAPI1.CommRqData("TR_투자자순매수요청_코스피_7;" + 로딩중, "opt10051", 0, "1107");
                if (result_C == 0)
                {
                    //       Console.WriteLine("TR_투자자순매수요청_코스피_7 :  요청");
                }
                else
                {
                    Error_Log("업종별투자자순매수요청:  코스피 실패");
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager);  // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }
        private void TR_투자자순매수요청_코스닥_8(bool 로딩중)
        {
            Task JO_Manager = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("시장구분", "1");  // 0: 코스피  1: 코스닥
                axKHOpenAPI1.SetInputValue("금액수량구분", "0"); //  금액수량구분 = 금액:0, 수량: 1
                axKHOpenAPI1.SetInputValue("기준일자", DateTime.Now.ToString("yyyyMMdd")); //   기준일자 = YYYYMMDD(20170101 연도4자리, 월 2자리, 일 2자리 형식)
                axKHOpenAPI1.SetInputValue("거래소구분", "1");

                int result_C = axKHOpenAPI1.CommRqData("TR_투자자순매수요청_코스닥_8;" + 로딩중, "opt10051", 0, "1108");
                if (result_C == 0)
                {
                    //  Console.WriteLine("TR_투자자순매수요청_코스닥_8:  코스닥 요청");
                }
                else
                {
                    Error_Log("업종별투자자순매수요청:  코스닥 실패");
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager);  // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }
        public static void TR_프로그램매매추이요청_코스피_9(bool 로딩중)
        {
            Task JO_Manager = new Task(() =>
            {
                form1.axKHOpenAPI1.SetInputValue("날짜", DateTime.Now.ToString("yyyyMMdd"));
                form1.axKHOpenAPI1.SetInputValue("시간구분", "2");
                form1.axKHOpenAPI1.SetInputValue("금액수량구분", "1");
                form1.axKHOpenAPI1.SetInputValue("시장구분", "P00101");
                form1.axKHOpenAPI1.SetInputValue("분틱구분", "1");
                form1.axKHOpenAPI1.SetInputValue("거래소구분", "1");

                int result_C = form1.axKHOpenAPI1.CommRqData("TR_프로그램매매추이요청_코스피_9;" + 로딩중, "opt90005", 0, "1109");

                if (result_C == 0)
                {
                    //        Console.WriteLine("TR_프로그램매매추이요청_코스피_9:  코스피 요청");
                }
                else
                {
                    Error_Log("업종별투자자순매수요청:  코스닥 실패");
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager);  // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }
        private void TR_프로그램매매추이요청_코스닥_10(bool 로딩중)
        {
            Task JO_Manager = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("날짜", DateTime.Now.ToString("yyyyMMdd"));
                axKHOpenAPI1.SetInputValue("시간구분", "2");
                axKHOpenAPI1.SetInputValue("금액수량구분", "1");
                axKHOpenAPI1.SetInputValue("시장구분", "P10102");
                axKHOpenAPI1.SetInputValue("분틱구분", "1");
                axKHOpenAPI1.SetInputValue("거래소구분", "1");

                int result_C = axKHOpenAPI1.CommRqData("TR_프로그램매매추이요청_코스닥_10;" + 로딩중, "opt90005", 0, "1110");

                if (result_C == 0)
                {
                    //        Console.WriteLine("TR_프로그램매매추이요청_코스닥_10:  코스피 요청");
                }
                else
                {
                    Error_Log("업종별투자자순매수요청:  코스닥 실패");
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager);  // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }
        public static void TR_호가요청_11()
        {
            foreach (var code in stockBalanceList.ToList())
            {
                Stockbalance 잔고 = code.Value;
                if (!잔고.매매가능 && !잔고.종목상태.Contains("거래정지"))
                {
                    Task JO_Manager = new Task(() =>
                    {
                        form1.axKHOpenAPI1.SetInputValue("종목코드", 잔고.종목코드);
                        int result = form1.axKHOpenAPI1.CommRqData(잔고.종목코드 + "^TR_호가요청_11", "opt10004", 0, "1111");

                        if (result == 0)
                        {
                            //Console.WriteLine("TR_호가요청 호가요청 ' 성공 '  종목명- " + 종목명 +" 상태: " + jostRequestTrDataManager.Dequeuestate());
                            // 동작_Log("TR_호가요청: " + 잔고.종목명);
                        }
                        else
                        {
                            Error_Log("호가요청  ' 실패 ' 종목명- " + 잔고.종목명 + "실패코드 : " + GET.오류코드(result));
                        }

                        Delay(300);
                    });
                    jostRequestTrDataManager.RequestTrData(JO_Manager); // 생성된 Task 조스트 매니지먼트에 요청 등록. 
                }
            }
        }

        public static void TR_주문내역요청_12() //opt10075 미체결 불러오기
        {
            Task JO_Manager = new Task(() =>
            {
                for (int i = 0; i < JumunItem_List.ToList().Count; i++)
                {
                    JumunItem_List[i].주문동기화 = true;
                }

                form1.axKHOpenAPI1.SetInputValue("계좌번호", Properties.Settings.Default.select_account);
                form1.axKHOpenAPI1.SetInputValue("전체종목구분", "0");
                form1.axKHOpenAPI1.SetInputValue("매매구분", "0");
                form1.axKHOpenAPI1.SetInputValue("종목코드", "0");
                form1.axKHOpenAPI1.SetInputValue("체결구분", "1");
                form1.axKHOpenAPI1.SetInputValue("거래소구분", "1");

                int result_opt10075_A = form1.axKHOpenAPI1.CommRqData("TR_주문내역요청_12", "opt10075", 0, "1112");
                if (result_opt10075_A == 0)
                {
                    if (!로딩완료) 동작_Log(Properties.Settings.Default.select_account + " 주문 접수내역 요청 ");
                }
                else
                {
                    Error_Log(Properties.Settings.Default.select_account + " 주문 접수내역 요청실패 메세지: " + GET.오류코드(result_opt10075_A));
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager); // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }

        public static void TR_예수금요청_13()
        {
            Task JO_Manager = new Task(() =>
            {
                form1.axKHOpenAPI1.SetInputValue("계좌번호", Properties.Settings.Default.select_account);
                form1.axKHOpenAPI1.SetInputValue("비밀번호", "");
                form1.axKHOpenAPI1.SetInputValue("상장폐지조회구분", "0");
                form1.axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
                form1.axKHOpenAPI1.SetInputValue("거래소구분", "KRX");

                int result_A = form1.axKHOpenAPI1.CommRqData("TR_예수금요청_13", "opw00004", 0, "1113");

                if (result_A == 0)
                {
                    if (!로딩완료)
                        동작_Log(Properties.Settings.Default.select_account + " 예수금 조회 성공");
                }
                else
                {
                    Error_Log(Properties.Settings.Default.select_account + " 예수금 조회 실패" + " 에러메세지: " + GET.오류코드(result_A));
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager); // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }

        public static void TR_잔고보정요청_14(string 종목코드)
        {
            Task JO_Manager = new Task(() =>
            {
                //  OPW18_계좌조회  잔고 불러오기
                form1.axKHOpenAPI1.SetInputValue("계좌번호", Properties.Settings.Default.select_account);
                form1.axKHOpenAPI1.SetInputValue("비밀번호", "");
                form1.axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
                form1.axKHOpenAPI1.SetInputValue("조회구분", "1");
                form1.axKHOpenAPI1.SetInputValue("거래소구분", "KRX");

                int result_C = form1.axKHOpenAPI1.CommRqData("TR_잔고보정요청_14^" + 종목코드, "opw00018", 0, GET.ScreenNum());

                if (result_C == 0)
                {
                    //  Console.WriteLine( DateTime.Now.ToString("HH:mm:ss.ff")+ "/////// TR요청 [TR_잔고보정요청_14 TR요청] " + axKHOpenAPI1.GetMasterCodeName(종목코드) + " 주문수량 보정 요청 ///////");
                }
                else
                {
                    Error_Log(종목코드 + " 잔고보정 요청 실패 메세지: " + GET.오류코드(result_C));
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager); // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }

        public static void TR_opt10074_실현손익요청()
        {
            Task JO_Manager = new Task(() =>
            {
                form1.axKHOpenAPI1.SetInputValue("계좌번호", Properties.Settings.Default.select_account);
                form1.axKHOpenAPI1.SetInputValue("시작일자", DateTime.Now.ToString("yyyyMMdd"));
                form1.axKHOpenAPI1.SetInputValue("종료일자", DateTime.Now.ToString("yyyyMMdd"));

                int result_opt10073_A = form1.axKHOpenAPI1.CommRqData("TR_opt10074_실현손익요청", "opt10074", 0, "1114");

                if (result_opt10073_A == 0)
                {
                    //Console.WriteLine("\n "+Properties.Settings.Default.select_account + " 실현손익 요청");
                }
                else
                {
                    Error_Log(Properties.Settings.Default.select_account + " 실현손익 불러오기 실패 메세지: " + GET.오류코드(result_opt10073_A));
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager);
        }

        public void TR_주식분봉차트조회요청(string code)
        {
            Task opt10080 = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("종목코드", code + "_AL");
                axKHOpenAPI1.SetInputValue("틱범위", "1");
                axKHOpenAPI1.SetInputValue("수정주가구분", "0");

                int result_opt10080 = axKHOpenAPI1.CommRqData("TR_주식분봉차트조회요청", "opt10080", 0, GET.ScreenNum());

                if (result_opt10080 == 0)
                {
                    //  Console.WriteLine("TR_주식분봉차트조회요청");
                }
                else
                {
                    Error_Log("TR_주식분봉차트조회요청 불러오기 실패 메세지: " + GET.오류코드(result_opt10080));
                }
            });
            jostRequestTrDataManager.RequestTrData(opt10080);
        }

        public void TR_주식일봉차트조회요청(string code)
        {
            Task JO_opt10081 = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("종목코드", code + "_AL");
                axKHOpenAPI1.SetInputValue("기준일자", DateTime.Now.ToString("yyyyMMdd"));
                axKHOpenAPI1.SetInputValue("수정주가구분", "0");

                int opt10081 = axKHOpenAPI1.CommRqData("TR_주식일봉차트조회요청", "opt10081", 0, GET.ScreenNum());

                if (opt10081 == 0)
                {
                    //     Console.WriteLine("TR_주식일봉차트조회요청");
                }
                else
                {
                    Error_Log("TR_주식일봉차트조회요청 불러오기 실패 메세지: " + GET.오류코드(opt10081));
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_opt10081);
        }

        private void TR_업종분봉조회요청()
        {
            Task opt20005_001 = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("업종코드", "001");
                axKHOpenAPI1.SetInputValue("틱범위", "1");

                int result_opt20005 = axKHOpenAPI1.CommRqData("TR_업종분봉조회요청", "opt20005", 0, GET.ScreenNum());

                if (result_opt20005 == 0)
                {
                    Console.WriteLine("TR_업종분봉조회요청 001");
                }
                else
                {
                    Error_Log("TR_업종분봉조회요청 불러오기 실패 메세지: " + GET.오류코드(result_opt20005));
                }
            });
            jostRequestTrDataManager.RequestTrData(opt20005_001);

            Task opt20005_101 = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("업종코드", "101");
                axKHOpenAPI1.SetInputValue("틱범위", "1");

                int result_opt20005 = axKHOpenAPI1.CommRqData("TR_업종분봉조회요청", "opt20005", 0, GET.ScreenNum());

                if (result_opt20005 == 0)
                {
                    Console.WriteLine("TR_업종분봉조회요청 101");
                }
                else
                {
                    Error_Log("TR_업종분봉조회요청 불러오기 실패 메세지: " + GET.오류코드(result_opt20005));
                }
            });
            jostRequestTrDataManager.RequestTrData(opt20005_101);
        }

        private void TR_업종일봉조회요청()
        {
            Task JO_001 = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("업종코드", "001");
                axKHOpenAPI1.SetInputValue("기준일자", DateTime.Now.ToString("yyyyMMdd"));

                int result_opt20006 = axKHOpenAPI1.CommRqData("TR_업종일봉조회요청", "opt20006", 0, GET.ScreenNum());

                if (result_opt20006 == 0)
                {
                    Console.WriteLine("TR_업종일봉조회요청 001");
                }
                else
                {
                    Error_Log("TR_업종일봉조회요청 불러오기 실패 메세지: " + GET.오류코드(result_opt20006));
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_001);

            Task JO_101 = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("업종코드", "101");
                axKHOpenAPI1.SetInputValue("기준일자", DateTime.Now.ToString("yyyyMMdd"));

                int result_opt20006 = axKHOpenAPI1.CommRqData("TR_업종일봉조회요청", "opt20006", 0, GET.ScreenNum());

                if (result_opt20006 == 0)
                {
                    Console.WriteLine("TR_업종일봉조회요청 101 ");
                }
                else
                {
                    Error_Log("TR_업종일봉조회요청 불러오기 실패 메세지: " + GET.오류코드(result_opt20006));
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_101);
        }



        private void Que_TRopt10001_request(string itemcode, string para, string 요청위치) // 주식기본정보요청
        {
            Task JO_Manager = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("종목코드", itemcode + "_AL");
                int result = axKHOpenAPI1.CommRqData(para, "opt10001", 0, "1115");

                if (result.Equals(0))
                {
                    //   동작_Log("TR_시세조회 ' 성공 ' 요청위치- " + 요청위치 + " 종목명- " + axKHOpenAPI1.GetMasterCodeName(itemcode));
                }
                else
                {
                    Error_Log("TR_시세조회  ' 실패 ' 요청위치- " + 요청위치 + " 종목명- " + axKHOpenAPI1.GetMasterCodeName(itemcode) + "실패코드 : " + GET.오류코드(result));
                    GridView_Print.DGV_Jumun(DateTime.Now.ToString("HH:mm:ss"), "실패", axKHOpenAPI1.GetMasterCodeName(itemcode), 0, "-", "-", 0, 0, itemcode, "실패코드 : " + result, 0, 0, "시세조회", "000000", 0, 1, 0);
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager);  // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }

        public void 통계_opt10074_일자별실현손익요청(string 계좌번호)
        {
            Task JO_Manager = new Task(() =>
            {
                string 년 = TB_통계.Text.Trim();
                string 시작일 = 년 + "0101";
                string 종료일 = 년 + "1231";

                axKHOpenAPI1.SetInputValue("계좌번호", 계좌번호);
                axKHOpenAPI1.SetInputValue("시작일자", 시작일);
                axKHOpenAPI1.SetInputValue("종료일자", 종료일);

                int result_TR_opt10074_매매내역요청 = axKHOpenAPI1.CommRqData("통계_opt10074_일자별실현손익요청", "opt10074", 0, "1170");
                if (result_TR_opt10074_매매내역요청 == 0)
                {
                    if (!로딩완료) 동작_Log(Properties.Settings.Default.select_account + "_매매내역요청 ");
                }
                else
                {
                    Console.WriteLine(Properties.Settings.Default.select_account + "_매매내역 요청 실패 메세지: " + GET.오류코드(result_TR_opt10074_매매내역요청));
                    Error_Log(Properties.Settings.Default.select_account + "_매매내역 요청 실패 메세지: " + GET.오류코드(result_TR_opt10074_매매내역요청));
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager); // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }
        public void TR_opt10073_일자별종목별실현손익요청(string 계좌번호)
        {
            Task JO_Manager = new Task(() =>
            {
                axKHOpenAPI1.SetInputValue("계좌번호", 계좌번호);
                axKHOpenAPI1.SetInputValue("종목코드", "");
                axKHOpenAPI1.SetInputValue("시작일자", DTP_기준일.Value.ToString("yyyyMMdd"));
                axKHOpenAPI1.SetInputValue("종료일자", DTP_기준일.Value.ToString("yyyyMMdd"));

                int TR_일자별종목별실현손익요청 = axKHOpenAPI1.CommRqData("TR_일자별종목별실현손익요청", "opt10073", 0, "1180");
                if (TR_일자별종목별실현손익요청 == 0)
                {
                    동작_Log(Properties.Settings.Default.select_account + "_기준일매매일지요청 ");
                    Console.WriteLine("\nopt10073요청");
                }
                else
                {
                    Console.WriteLine(Properties.Settings.Default.select_account + "기준일매매일지 요청 실패 메세지: " + GET.오류코드(TR_일자별종목별실현손익요청));
                    Error_Log(Properties.Settings.Default.select_account + "기준일매매일지 요청 실패 메세지: " + GET.오류코드(TR_일자별종목별실현손익요청));
                }
            });
            jostRequestTrDataManager.RequestTrData(JO_Manager); // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }


        ///////////////        계좌선택 및 계좌OR잔고OR업종 TR요청         //////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        /////////////////////////       TR 데이터 수신          ///////////////////////////

        private void API_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            //로딩시작
            if (e.sRQName.Equals("Loadig_Kospi_1"))
            {
                Console.WriteLine("\nopt20001  코스피 요청 ::     " + DateTime.Now.ToString("HH:mm:ss.fff"));

                string 지수등락율 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "등락률").Trim();
                string 지수현재가 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim();
                string 지수고가 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "고가").Trim();
                string 지수저가 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "저가").Trim();

                Console.WriteLine("opt20001  코스피 요청 ::     " + DateTime.Now.ToString("HH:mm:ss.fff") + " 현재가: " + 지수현재가 + " 고가: " + 지수고가 + " 저가: " + 지수저가);

                if (지수저가.Equals("-0.00")) 지수저가 = 지수현재가;
                if (지수고가.Equals("-0.00")) 지수고가 = 지수현재가;

                RealData_Management.Market_update("001", 지수등락율, 지수현재가, 지수저가, 지수고가);

                string 누적거래대금 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래대금").Trim();
                string 상한종목수 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "상한").Trim();
                string 상승종목수 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "상승").Trim();
                string 보합종목수 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "보합").Trim();
                string 하락종목수 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "하락").Trim();
                string 하한종목수 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "하한").Trim();

                RealData_Management.Market_fluctuate("001", 누적거래대금, 상한종목수, 상승종목수, 보합종목수, 하한종목수, 하락종목수);

                동작_Log("코스피 시세 확인.");

                Loadig_Kosdaq_2();
            }

            if (e.sRQName.Equals("Loadig_Kosdaq_2"))
            {
                Console.WriteLine("opt20001  코스닥 요청 ::     " + DateTime.Now.ToString("HH:mm:ss.fff"));

                string 지수등락율 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "등락률").Trim();
                string 지수현재가 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim();
                string 지수고가 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "고가").Trim();
                string 지수저가 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "저가").Trim();

                if (지수저가.Equals("-0.00")) 지수저가 = 지수현재가;
                if (지수고가.Equals("-0.00")) 지수고가 = 지수현재가;

                RealData_Management.Market_update("101", 지수등락율, 지수현재가, 지수저가, 지수고가);

                string 누적거래대금 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래대금").Trim();
                string 상한종목수 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "상한").Trim();
                string 상승종목수 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "상승").Trim();
                string 보합종목수 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "보합").Trim();
                string 하락종목수 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "하락").Trim();
                string 하한종목수 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "하한").Trim();

                RealData_Management.Market_fluctuate("101", 누적거래대금, 상한종목수, 상승종목수, 보합종목수, 하한종목수, 하락종목수);

                동작_Log("코스닥 시세 확인");

                Console.WriteLine("코스닥 시세 확인 완료");
                TR_예수금조회_3();
            }

            if (e.sRQName.Equals("TR_예수금조회_3"))
            {
                bool 성공 = long.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "예수금"), out long 계좌예수금);

                if (성공)
                {
                    long D2예수금 = long.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "D+2추정예수금").Trim()); //D+2 예수금

                    Acc[0].D2 = D2예수금;

                    List<JumunItem> 매수리스트 = JumunItem_List.FindAll(o => o.주문유형 == 1);
                    if (매수리스트.Count > 0)
                    {
                        for (int i = 0; i < 매수리스트.Count; i++)
                        {
                            JumunItem 주문 = 매수리스트[i];

                            int 주문가격 = 주문.주문가격;
                            if (주문가격 == 0)
                                주문가격 = 주문.현재가;

                            Acc[0].추정D2 = D2예수금 - (주문가격 * 주문.주문수량);
                        }
                    }
                    else
                    {
                        Acc[0].추정D2 = D2예수금;
                    }
                }

                동작_Log(Properties.Settings.Default.select_account + " 예수금 조회 완료");
                Console.WriteLine(Properties.Settings.Default.select_account + " 예수금 조회 완료");

                TR_실현손익요청_4();
            }

            if (e.sRQName.Equals("TR_실현손익요청_4")) // 실현손익
            {
                long.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "실현손익"), out long 실현손익);

                long 손익률기준금 = Properties.Settings.Default.MT_sonik_price;
                double 매도손익률 = 0;

                if (실현손익 != 0)
                {
                    매도손익률 = 실현손익 / (double)손익률기준금 * 100;
                }

                double 실현손익률 = Math.Round(매도손익률, 2);

                Acc[0].실현손익 = 실현손익;
                Acc[0].실현손익률 = 실현손익률;

                동작_Log(Properties.Settings.Default.select_account + " 실현손익 조회 완료");
                Console.WriteLine(Properties.Settings.Default.select_account + " 실현손익 조회 완료");

                TR_잔고요청_5();
            }

            if (e.sRQName.Contains("TR_잔고요청_5"))
            {
                /////////////////////////////////////////////////////////////////////////////////////////////
                ///////                                    멀티데이터                                   /////
                /////////////////////////////////////////////////////////////////////////////////////////////
                ///////                                   보유잔고조회                                   /////
                /////////////////////////////////////////////////////////////////////////////////////////////

                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);

                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(i + " 잔고 종목명: " + axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim());

                    string co_de = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목번호").Trim();
                    if (co_de.Length > 0)
                    {
                        string 종목코드 = co_de.Substring(1);

                        if (Form1.Market_Item_List.TryGetValue(종목코드, out Market_Item Market))
                        {
                            bool 선택 = false;
                            int 그룹 = 0;
                            int 금일매수수량 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "금일매수수량").Trim());
                            int 금일매도수량 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "금일매도수량").Trim());
                            int 전일매수량 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "전일매수수량").Trim());
                            int 전일매도량 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "전일매도수량").Trim());
                            string itemName = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim();
                            int 현재가 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim());
                            double 전일종가 = (double)Market.Last_price;
                            double 등락율 = Math.Round((현재가 - 전일종가) / 전일종가 * 100, 2);
                            int 평균단가 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매입가").Trim()); //  = 평균단가
                            int 보유수량 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "보유수량").Trim());
                            long 매입금액 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매입금액").Trim());
                            double 보유비중 = Math.Round((double)매입금액 / (double)Properties.Settings.Default.MT_buying_standard * 100, 2);
                            long 평가금액 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "평가금액").Trim());
                            long 평가손익 = long.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "평가손익").Trim());
                            long 누적손익 = 0;
                            long 예상손익 = 평가손익 + 누적손익;
                            long 금일매수금 = 금일매수수량 * 현재가;
                            long 금일매도금 = 0;
                            int 매수횟수 = 0;
                            int 매도횟수 = 0;
                            DateTime 초기매수일 = DateTime.Now;
                            string 추가매수일 = "";
                            string 매수일 = "";
                            string 매도일 = "";

                            string 거래일 = "0";
                            string 초기매수검색식 = "수동매수";
                            int 재매수 = 10;
                            int 주문가능수량 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매매가능수량").Trim());
                            string 매수주문번호 = " ";
                            string 매도주문번호 = " ";
                            string 일회A = "on";
                            string 일회B = "on";
                            string 일회C = "on";
                            string 일회D = "on";
                            string 일회E = "on";
                            string 일회F = "on";
                            string 일회G = "on";
                            string 일회H = "on";
                            string 일회I = "on";
                            string 반복A = "A";
                            string 반복B = "B";
                            string 반복C = "C";
                            string 반복D = "D";
                            string 반복E = "E";
                            string 반복F = "F";
                            string 반복G = "G";
                            string 반복H = "H";
                            string 반복I = "I";
                            string 반복J = "J";
                            string 반복K = "K";
                            string 반복L = "L";
                            string 반복M = "M";
                            string 반복N = "N";
                            string 리벨A = "A";
                            string 리벨B = "B";
                            string 리벨C = "C";
                            string 리벨D = "D";
                            string 리벨E = "E";
                            string 리벨F = "F";
                            string 리벨G = "G";

                            string 잔고청산A = "A";
                            string 잔고청산B = "B";
                            string 잔고청산C = "C";
                            string 시간청산A = "A";
                            string 시간청산B = "B";
                            string 시간청산C = "C";

                            string 분_리스트 = "";
                            string 일_리스트 = "";

                            int 세금 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "세금").Trim());
                            int 잔고수수료 = (int)(Math.Truncate((double)현재가 * 보유수량 * 수수료 / 10) * 10);
                            string 시장 = Market.Market;
                            double tax_ = TAX;
                            if (시장.Equals("E")) tax_ = 0;

                            double 세금_수수료 = Math.Truncate(((double)평균단가 * (double)보유수량 * 수수료) + ((double)현재가 * (double)보유수량 * 수수료) + ((double)현재가 * (double)보유수량 * tax_));
                            double 평손 = (double)평가금액 - (double)매입금액 - 세금_수수료;
                            평가손익 = (long)(평손);

                            double 수익률 = Math.Truncate((((double)(현재가 - 평균단가) / 평균단가 * (double)100) - ((수수료 + 수수료 + tax_) * 100)) * 100) / 100;
                            int 시작가 = 평균단가;
                            int 기준가 = 평균단가;
                            double 시작수익률 = 수익률;
                            double 기준수익률 = 수익률;

                            double 최고수익률 = 0;
                            double 최저수익률 = 0;

                            if (0 < 수익률) 최고수익률 = 수익률;
                            if (수익률 < 0) 최저수익률 = 수익률;

                            double 최고예상손익금 = 0;
                            double 최저예상손익금 = 0;

                            if (0 < 예상손익) 최고예상손익금 = 예상손익;
                            if (예상손익 < 0) 최저예상손익금 = 예상손익;

                            if (Properties.Settings.Default.CB_new_rebuy)
                            {
                                재매수 = Properties.Settings.Default.MTB_new_rebuytime;
                            }

                            string 종목상태 = GET.Jango_state(종목코드);

                            bool 매매_가능 = false;

                            Market.현재가 = 현재가;
                            Market.등락율 = 등락율;


                            if (보유수량 > 0)
                            {
                                재매수 Item = Rebuystock_List.Find(o => o.ItemCode.Equals(종목코드));
                                if (Item == null)
                                {
                                    재매수 Add = new 재매수(종목코드, itemName, "");
                                    Rebuystock_List.Add(Add);
                                }
                            }

                            Acc[0].매입금 = Acc[0].매입금 + 매입금액;
                            Acc[0].평가금 = Acc[0].평가금 + 평가금액;
                            Acc[0].평가손익 = Acc[0].평가손익 + 평가손익;

                            if (stockBalanceList.TryGetValue(종목코드, out Stockbalance 잔고))
                            {
                                TimeSpan TS = DateTime.Now.Date - 잔고.초기매수일.Date;
                                잔고.거래일 = TS.Days.ToString();

                                잔고.전량매도 = false;
                                잔고.현재가 = 현재가;
                                잔고.등락율 = 등락율;
                                잔고.평균단가 = 평균단가;
                                잔고.보유수량 = 보유수량;
                                잔고.매입금액 = 매입금액;
                                잔고.평가금액 = 평가금액;
                                잔고.평가손익 = 평가손익;
                                잔고.수익률 = 수익률;
                                잔고.주문가능수량 = 주문가능수량;
                                잔고.시장 = 시장;
                                잔고.전일매수량 = 전일매수량;
                                잔고.전일매도량 = 전일매도량;
                                잔고.보유비중 = 보유비중;
                                잔고.세금 = 세금;
                                잔고.수수료 = (int)(Math.Truncate((double)잔고.현재가 * 잔고.보유수량 * 수수료 / 10) * 10);
                                잔고.종목상태 = 종목상태;
                                잔고.매매가능 = 매매_가능;
                                잔고.매도기준 = 보유수량;
                                잔고.예상손익 = 잔고.누적손익 + 평가손익;

                                double.TryParse(잔고.기준가격.ToString(), out double 기준가격);
                                if (기준가격 == 0) 잔고.기준가격 = 기준가;
                                double.TryParse(잔고.시작가격.ToString(), out double 시작가격);
                                if (시작가격 == 0) 잔고.시작가격 = 시작가;

                                잔고.기준수익률 = Math.Truncate((((double)(현재가 - 잔고.기준가격) / 잔고.기준가격 * (double)100) - ((수수료 + 수수료 + tax_) * 100)) * 100) / 100;
                                잔고.시작수익률 = Math.Truncate((((double)(현재가 - 잔고.시작가격) / 잔고.시작가격 * (double)100) - ((수수료 + 수수료 + tax_) * 100)) * 100) / 100;

                                if (!잔고.초기매수일.Date.Equals(DateTime.Now.Date) && !잔고.매수일.Equals(today))
                                {
                                    잔고.금일매수금 = 0;

                                }

                                if (!잔고.매도일.Equals(today))
                                {
                                    잔고.금일매도금 = 0;
                                }
                            }
                            else
                            {
                                int 잔고청산_TS_high_A = 0;
                                int 잔고청산_TS_high_B = 0;
                                int 잔고청산_TS_high_C = 0;
                                int 매매기간_TS_high_A = 0;
                                int 매매기간_TS_high_B = 0;
                                int 매매기간_TS_high_C = 0;
                                int 매매기간_TS_high_D = 0;
                                int 매매기간_TS_high_E = 0;
                                int 매매기간_TS_high_F = 0;

                                Form1.form1.TR_주식분봉차트조회요청(종목코드);
                                Form1.form1.TR_주식일봉차트조회요청(종목코드);

                                stockBalanceList.Add(종목코드, new Stockbalance(today, false, false, 0, 선택, 그룹, 시장, itemName, 종목코드, 현재가, 등락율, 평균단가, 보유수량, 매입금액, 평가금액, 평가손익, 0, 누적손익, 예상손익, 수익률, 최고수익률, 최저수익률, 최고예상손익금, 최저예상손익금, 금일매수금, 금일매도금, 매수횟수, 매도횟수, 초기매수일, 거래일,
                                                                              초기매수검색식, 보유비중, 전일매수량, 전일매도량, 재매수, 추가매수일, 매수일, 매도일, 주문가능수량, 매수주문번호, 매도주문번호,
                                                                              true, true, true, true, true, true, true, true, true, "0", "0", "0", "0", "0", "0", "0", "0", "0", 일회A, 일회B, 일회C, 일회D, 일회E, 일회F, 일회G, 일회H, 일회I,
                                                                                true, true, true, true, true, true, 반복A, 반복B, 반복C, 반복D, 반복E, 반복F, 반복G, 반복H, 반복I, 반복J, 반복K, 반복L, 반복M, 반복N, 세금, 잔고수수료,
                                                                              보유수량, true, 종목상태, 매매_가능, 그룹지정_A, 그룹지정_B, 그룹지정_C, 그룹지정_D, 리벨A, 리벨B, 리벨C, 리벨D, 리벨E, 리벨F, 리벨G, 잔고청산A, 잔고청산B, 잔고청산C,
                                                                              true, true, true, true, true, true, true, true, true, "0@0@0@0", 시작가, 시작수익률, 기준가, 기준수익률,
                                                                              true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, 0, 0, 0, false, true, false, false, false, false, false,
                                                                              false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, 시간청산A, 시간청산B, 시간청산C, false, false, false, false, 분_리스트, 일_리스트,
                                                                              true, true, true, true, true, true, true, true, true,
                                                                              잔고청산_TS_high_A, 잔고청산_TS_high_B, 잔고청산_TS_high_C,
                                                                              매매기간_TS_high_A, 매매기간_TS_high_B, 매매기간_TS_high_C, 매매기간_TS_high_D, 매매기간_TS_high_E, 매매기간_TS_high_F));

                                MA.New_item(종목코드);

                                동작_Log("");
                                동작_Log("[수동매수] 종목명: " + itemName + " 평균단가: " + 평균단가.ToString("N0") + " 매수수량: " + 보유수량.ToString("N0") + " 매입금액: " + 매입금액.ToString("N0") + " 수동 매수 되었습니다.");
                                동작_Log("");

                                알림창("[ 수동매수 ]\n\n종목명: " + itemName + " 평균단가: " + 평균단가.ToString("N0") + "\n\n매수수량: " + 보유수량.ToString("N0") + " 매입금액: " + 매입금액.ToString("N0") + " 수동 매수 되었습니다.", 5, false);
                            }

                            if (!최종매입가_List.Exists(o => o.종목코드.Equals(종목코드)))
                            {
                                최종매입가_List.Add(new 최종매입가(종목코드, "리밸_A", 0, 평균단가));
                                최종매입가_List.Add(new 최종매입가(종목코드, "리밸_B", 0, 평균단가));
                                최종매입가_List.Add(new 최종매입가(종목코드, "리밸_C", 0, 평균단가));
                                최종매입가_List.Add(new 최종매입가(종목코드, "리밸_D", 0, 평균단가));
                                최종매입가_List.Add(new 최종매입가(종목코드, "리밸_E", 0, 평균단가));
                                최종매입가_List.Add(new 최종매입가(종목코드, "리밸_F", 0, 평균단가));
                                최종매입가_List.Add(new 최종매입가(종목코드, "리밸_G", 0, 평균단가));
                            }
                        }
                    }
                }

                if (e.sPrevNext.Equals("2"))
                {
                    jostRequestTrDataManager.DequeueStop();

                    axKHOpenAPI1.SetInputValue("계좌번호", Properties.Settings.Default.select_account);
                    axKHOpenAPI1.SetInputValue("비밀번호", "");
                    axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
                    axKHOpenAPI1.SetInputValue("조회구분", "1");
                    axKHOpenAPI1.SetInputValue("거래소구분", "KRX");

                    axKHOpenAPI1.CommRqData("TR_잔고요청_5", "opw00018", 2, "1105");
                    Delay(300);
                    return;
                }

                //if (!e.sPrevNext.Equals("2"))
                //{
                //    jostRequestTrDataManager.DequeueRun();

                //    var newList = from pair in stockBalanceList orderby pair.Value.초기매수일 ascending select pair;

                //    foreach (var code in newList.ToList())
                //    {
                //        Stockbalance 잔고 = code.Value;
                //        Method.매입금매매제한(잔고);

                //        if (잔고.보유수량 == 0)
                //        {
                //            stockBalanceList.Remove(잔고.종목코드);
                //        }
                //        else
                //        {
                //            Method.실시간시세등록(잔고.종목코드);

                //            if (!공휴일)
                //            {
                //                if (timenow < 시장시작) // 장시작 후 로딩
                //                {
                //                    if (!잔고.종목상태.Equals("거래정지")) 잔고.종목상태 = "마켓대기";
                //                }

                //                if (시장시작 <= timenow) // 장시작 후 로딩
                //                {
                //                    if (!잔고.종목상태.Equals("거래정지")) 잔고.종목상태 = "시세로딩";
                //                }
                //            }

                //            JanGo_dataGridView.Rows.Insert(0);
                //            JanGo_dataGridView["코드_잔고A", 0].Value = 잔고.종목코드;
                //            GridView_Print.JangoRow_print(0, 잔고);
                //            JanGo_dataGridView.ClearSelection();
                //        }
                //    }

                //    TB_jango_count.Text = stockBalanceList.Count.ToString();

                //    DataManagement.save_jango();
                //    동작_Log(Properties.Settings.Default.select_account + " 잔고 요청 완료");
                //    Console.WriteLine(Properties.Settings.Default.select_account + " 잔고 요청 완료");

                //    Tab_AccountManagement.감시주문동기화();
                //    동작_Log(Properties.Settings.Default.select_account + " 감시주문 동기화 완료");
                //    Console.WriteLine(Properties.Settings.Default.select_account + " 감시주문 동기화 완료");

                //    Order_Reserve.예약주문동기화();
                //    동작_Log(Properties.Settings.Default.select_account + " 예약주문 동기화 완료");
                //    Console.WriteLine(Properties.Settings.Default.select_account + " 예약주문 동기화 완료");

                //    DataManagement.Rebuystock_DataLoad();
                //    Rebuystock_List = Rebuystock_List.Distinct().ToList();
                //    Console.WriteLine(Properties.Settings.Default.select_account + " Rebuystock_DataLoad 완료");

                //    Form_Print();
                //    TR_투자자순매수요청_코스피_7(true);
                //}

                if (!e.sPrevNext.Equals("2"))
                {
                    jostRequestTrDataManager.DequeueRun();

                    // 초기 매수일 기준으로 잔고 목록을 정렬
                    var sortedStockBalances = from pair in stockBalanceList orderby pair.Value.초기매수일 descending select pair;

                    // UI 업데이트를 위한 DataGridView 초기화 (필요하다면)
                    JanGo_dataGridView.Rows.Clear();

                    // 잔고 목록을 순회하며 데이터 처리
                    foreach (var code in sortedStockBalances)
                    {
                        Stockbalance balance = code.Value;

                        // 매매 제한 로직 실행
                        Method.매입금매매제한(balance);

                        if (balance.보유수량 == 0)
                        {
                            // 보유 수량이 0인 종목은 목록에서 제거
                            stockBalanceList.Remove(balance.종목코드);
                        }
                        else
                        {
                            // 실시간 시세 등록 및 종목 상태 업데이트
                            Method.실시간시세등록(balance.종목코드);

                            if (!공휴일)
                            {
                                // 주식 시장 상태에 따라 종목 상태 업데이트
                                if (timenow < 시장시작)
                                {
                                    if (!balance.종목상태.Equals("거래정지")) balance.종목상태 = "마켓대기";
                                }
                                else
                                {
                                    if (!balance.종목상태.Equals("거래정지")) balance.종목상태 = "시세로딩";
                                }
                            }

                            // 데이터그리드뷰에 행 추가
                            JanGo_dataGridView.Rows.Add();
                            int rowIndex = JanGo_dataGridView.Rows.Count - 1; // 마지막 행 인덱스
                            JanGo_dataGridView["코드_잔고A", rowIndex].Value = balance.종목코드;
                            GridView_Print.JangoRow_print(rowIndex, balance);
                        }
                    }

                    // 모든 데이터 처리 후 UI 업데이트
                    JanGo_dataGridView.ClearSelection();
                    TB_jango_count.Text = stockBalanceList.Count.ToString();

                    // 데이터 저장 및 로그 기록
                    DataManagement.save_jango();
                    LogAndConsole(Properties.Settings.Default.select_account + " 잔고 요청 완료");

                    // 동기화 작업
                    Tab_AccountManagement.감시주문동기화();
                    LogAndConsole(Properties.Settings.Default.select_account + " 감시주문 동기화 완료");

                    Order_Reserve.예약주문동기화();
                    LogAndConsole(Properties.Settings.Default.select_account + " 예약주문 동기화 완료");

                    DataManagement.Rebuystock_DataLoad();
                    Rebuystock_List = Rebuystock_List.Distinct().ToList();
                    LogAndConsole(Properties.Settings.Default.select_account + " Rebuystock_DataLoad 완료");

                    Form_Print();
                    TR_투자자순매수요청_코스피_7(true);
                }

                // 헬퍼 함수: 로그 및 콘솔 출력
                void LogAndConsole(string message)
                {
                    동작_Log(message);
                    Console.WriteLine(message);
                }
            }

            // 로딩완료
            if (e.sRQName.Equals("TR_주문내역요청_6"))    //opt10075 미체결 불러오기
            {
                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);

                for (int i = 0; i < count; i++)
                {
                    string co_de = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목코드").Trim();

                    if (co_de.Length > 0)
                    {
                        string 종목코드 = co_de;
                        if (Form1.Market_Item_List.TryGetValue(종목코드, out Market_Item Market))
                        {
                            string 종목명 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim();
                            string 주문번호 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "주문번호").Trim();
                            int 주문수량 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "주문수량").Trim());
                            int 주문가격 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "주문가격").Trim());
                            int 미체결수량 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "미체결수량").Trim());
                            string 주문구분 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "주문구분").Trim().Substring(1);
                            string 매매구분 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매매구분").Trim();
                            int.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "시간"), out int 주문시간);
                            int 현재가 = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim()));

                            int 유형 = 2;
                            if (주문구분.Contains("매수")) 유형 = 1;

                            int.TryParse(Form_Outstanding.form.TB_미체결row.Text, out int max_row);

                            JumunItem JumunItem = JumunItem_List.Find(o => o.주문번호.Equals(주문번호));

                            if (JumunItem != null)
                            {
                                JumunItem.종목명 = 종목명;
                                JumunItem.주문가격 = 주문가격;
                                JumunItem.현재가 = 현재가;
                                JumunItem.주문시간 = 주문시간;
                                JumunItem.미체결량 = 미체결수량;
                                JumunItem.주문동기화 = false;
                                JumunItem.Tik_price = 현재가;
                                JumunItem.Tik_cap = Method.Find_Tik_Cap(현재가, 주문가격, Market.Market);
                            }
                            else
                            {
                                int tik_price = 현재가;
                                int tik_cap = Method.Find_Tik_Cap(현재가, 주문가격, Market.Market);
                                double 수익률 = 0;
                                int OrderN = GET.Order번호();
                                bool 주문동기화 = false;

                                JumunItem JumunItem_Add = new JumunItem(0, 0, GET.ScreenNum(), 종목코드, 종목명, 주문번호, "---", "리스트'누락'", 0, 1, 60, 0, 0, "", "TR_미체결조회", 주문수량, 주문가격, 유형, 0, 0, 30, 현재가, 0, 주문시간,
                                                                        미체결수량, true, true, 1, tik_cap, tik_price, 수익률, 주문동기화, 0, OrderN, 0, Form1.NXT_server);
                                JumunItem_List.Add(JumunItem_Add);
                            }

                            동작_Log("미체결 확인:: " + 종목명 + " 주문유형: " + 주문구분 + " 주문번호: " + 주문번호);
                        }
                    }
                }

                if (e.sPrevNext.Equals("2"))
                {
                    jostRequestTrDataManager.DequeueStop();

                    axKHOpenAPI1.SetInputValue("계좌번호", Properties.Settings.Default.select_account);
                    axKHOpenAPI1.SetInputValue("전체종목구분", "0");
                    axKHOpenAPI1.SetInputValue("매매구분", "0");
                    axKHOpenAPI1.SetInputValue("종목코드", "0");
                    axKHOpenAPI1.SetInputValue("체결구분", "1");
                    axKHOpenAPI1.SetInputValue("거래소구분", "1");

                    int result_opt10075_A = axKHOpenAPI1.CommRqData("TR_주문내역요청_6", "opt10075", 2, "1106");
                    if (result_opt10075_A == 0)
                    {
                        if (!로딩완료) 동작_Log(Properties.Settings.Default.select_account + " 주문 접수내역 추가요청 ");
                    }
                    else
                    {
                        Error_Log(Properties.Settings.Default.select_account + " 주문 접수내역 추가요청 실패 메세지: " + GET.오류코드(result_opt10075_A));
                    }
                    Delay(300);

                    return;
                }

                if (!e.sPrevNext.Equals("2"))
                {
                    jostRequestTrDataManager.DequeueRun();

                    foreach (var item in JumunItem_List.ToList())
                    {
                        if (item.주문동기화)
                        {
                            JumunItem_List.Remove(item);
                        }
                    }

                    Console.WriteLine(Properties.Settings.Default.select_account + " 주문 접수내역 조회완료");
                    동작_Log(Properties.Settings.Default.select_account + " 주문 접수내역 조회완료");

                    TR_업종분봉조회요청();

                    TelegramMessenger.Telegram_alram("logIn_user");
                }
            }

            if (e.sRQName.Contains("TR_투자자순매수요청_코스피_7"))
            {
                if (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "업종코드").Trim().Equals("001"))
                {
                    Acc[0].피_외인 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "외국인순매수").Trim());
                    Acc[0].피_기관 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "기관계순매수").Trim());
                    Jisu_linkage.지수업종별연동("코스피");

                    TR_투자자순매수요청_코스닥_8(bool.Parse(e.sRQName.Split(';')[1]));
                }
            }

            if (e.sRQName.Contains("TR_투자자순매수요청_코스닥_8"))
            {
                if (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "업종코드").Trim().Equals("101"))
                {
                    Acc[0].닥_외인 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "외국인순매수").Trim());
                    Acc[0].닥_기관 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "기관계순매수").Trim());
                    Jisu_linkage.지수업종별연동("코스닥");

                    if (!로딩완료) TR_프로그램매매추이요청_코스피_9(bool.Parse(e.sRQName.Split(';')[1]));
                }
            }

            if (e.sRQName.Contains("TR_프로그램매매추이요청_코스피_9"))
            {
                int.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "전체순매수").Trim(), out int 프로그램);
                Acc[0].피_프로그램 = 프로그램 / 100;

                Jisu_linkage.지수업종별연동("코스피");

                TR_프로그램매매추이요청_코스닥_10(bool.Parse(e.sRQName.Split(';')[1]));
            }

            if (e.sRQName.Contains("TR_프로그램매매추이요청_코스닥_10"))
            {
                int.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "전체순매수").Trim(), out int 프로그램);

                Acc[0].닥_프로그램 = 프로그램 / 100;
                Jisu_linkage.지수업종별연동("코스닥");

                if (bool.Parse(e.sRQName.Split(';')[1]))
                {
                    if (Properties.Settings.Default.CB_가이드매매)
                    {
                        Guide.가이드매매설정로딩();
                    }
                    else
                    {
                        Console.WriteLine(Properties.Settings.Default.select_account + " 검색식 로딩");
                        Condition_Management.Condition_DataLoad(Properties.Settings.Default.select_account);
                    }
                }
            }

            if (e.sRQName.Contains("TR_호가요청_11")) // 호가요청
            {
                string[] para = e.sRQName.Split('^');
                string itemCode = para[0];

                GET.StockState(itemCode, axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "매수2차선호가"),
                                         axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "매수4차선호가"),
                                         axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "매도2차선호가"),
                                         axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "매도4차선호가"));
            }

            if (e.sRQName.Equals("통계_opt10074_일자별실현손익요청"))
            {
                CBB_통계.SelectedIndex = -1;
                TB_월통계기준.Text = Properties.Settings.Default.TB_월통계기준.ToString("N0");

                string 총매수금액 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "총매수금액").Trim();
                string 총매도금액 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "총매도금액").Trim();
                string 실현손익 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "실현손익").Trim();
                string 매매수수료 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "매매수수료").Trim();
                string 매매세금 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "매매세금").Trim();

                매매내역_List.Add(총매수금액 + "^" + 총매도금액 + "^" + 실현손익 + "^" + 매매수수료 + "^" + 매매세금);

                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);

                for (int i = 0; i < count; i++)
                {
                    string 일자 = (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "일자").Trim());
                    string 매수금액 = (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매수금액").Trim());
                    string 매도금액 = (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매도금액").Trim());
                    string 당일매도손익 = (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "당일매도손익").Trim());
                    string 당일매매수수료 = (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "당일매매수수료").Trim());
                    string 당일매매세금 = (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "당일매매세금").Trim());

                    매매내역_List.Add(일자 + "^" + 매수금액 + "^" + 매도금액 + "^" + 당일매도손익 + "^" + 당일매매수수료 + "^" + 당일매매세금);
                }

                CBB_통계.SelectedIndex = 0;
            }

            if (e.sRQName.Equals("TR_일자별종목별실현손익요청"))
            {
                CBB_통계.SelectedIndex = -1;

                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);

                Console.WriteLine("TR결과값: e.sTrCode: " + e.sTrCode + " e.sPrevNext: " + e.sPrevNext + " axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName): " + axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName));

                for (int i = 0; i < count; i++)
                {
                    string 종목명 = (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim());
                    string 체결량 = (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "체결량").Trim());
                    string 매입단가 = (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매입단가").Trim());
                    string 체결가 = (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "체결가").Trim());
                    string 손익율 = (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "손익율").Trim());
                    string 당일매매수수료 = (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "당일매매수수료").Trim());
                    string 당일매매세금 = (axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "당일매매세금").Trim());

                    double.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "당일매도손익"), out double 당일매도손익);
                    int.TryParse(체결량, out int 체결량_);
                    double.TryParse(매입단가, out double 매입단가_);
                    double.TryParse(체결가, out double 체결가_);

                    double 매수금액 = (매입단가_ * (double)체결량_);
                    double 매도금액 = (체결가_ * (double)체결량_);

                    기준일매매내역_List.Add(종목명 + "^" + 매수금액 + "^" + 매도금액 + "^" + Math.Truncate(당일매도손익) + "^" + 당일매매수수료 + "^" + 당일매매세금 + "^" + 체결가 + "^" + 손익율);
                }

                if (e.sPrevNext.Equals("2"))
                {
                    jostRequestTrDataManager.DequeueStop();

                    axKHOpenAPI1.SetInputValue("계좌번호", Properties.Settings.Default.select_account);
                    axKHOpenAPI1.SetInputValue("종목코드", "");
                    axKHOpenAPI1.SetInputValue("시작일자", DTP_기준일.Value.ToString("yyyyMMdd"));
                    axKHOpenAPI1.SetInputValue("종료일자", DTP_기준일.Value.ToString("yyyyMMdd"));

                    int TR_일자별종목별실현손익요청 = axKHOpenAPI1.CommRqData("TR_일자별종목별실현손익요청", "opt10073", 2, "1180");
                    if (TR_일자별종목별실현손익요청 == 0)
                    {
                        // Console.WriteLine("\n기준일매매일지 추가요청");
                    }
                    else
                    {
                        Console.WriteLine(Properties.Settings.Default.select_account + "기준일매매일지 요청 실패 메세지: " + GET.오류코드(TR_일자별종목별실현손익요청));
                        Error_Log(Properties.Settings.Default.select_account + "기준일매매일지 요청 실패 메세지: " + GET.오류코드(TR_일자별종목별실현손익요청));
                    }
                    Delay(300);
                    return;
                }

                if (!e.sPrevNext.Equals("2"))
                {
                    jostRequestTrDataManager.DequeueRun();
                    Statistical_chart.Print_기준일매매일지();
                }
            }

            if (e.sRQName.Equals("TR_주문내역요청_12"))    //opt10075 미체결 불러오기
            {
                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);

                for (int i = 0; i < count; i++)
                {
                    string co_de = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목코드").Trim();

                    if (co_de.Length > 0)
                    {
                        string 종목코드 = co_de;

                        if (Form1.Market_Item_List.TryGetValue(종목코드, out Market_Item Market))
                        {
                            string 종목명 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim();
                            string 주문번호 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "주문번호").Trim();
                            int 주문수량 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "주문수량").Trim());
                            int 주문가격 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "주문가격").Trim());
                            int 미체결수량 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "미체결수량").Trim());
                            string 주문구분 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "주문구분").Trim().Substring(1);
                            string 매매구분 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매매구분").Trim();
                            int.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "시간"), out int 주문시간);
                            int 현재가 = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim()));

                            int 유형 = 2;
                            if (주문구분.Contains("매수")) 유형 = 1;

                            JumunItem JumunItem = JumunItem_List.Find(o => o.주문번호.Contains(주문번호));

                            if (JumunItem != null)
                            {
                                JumunItem.종목명 = 종목명;
                                JumunItem.주문가격 = 주문가격;
                                JumunItem.현재가 = 현재가;
                                JumunItem.주문시간 = 주문시간;
                                JumunItem.미체결량 = 미체결수량;
                                JumunItem.주문동기화 = false;
                                JumunItem.Tik_price = 현재가;
                                JumunItem.Tik_cap = Method.Find_Tik_Cap(현재가, 주문가격, Market.Market);
                            }
                            else
                            {
                                int tik_price = 현재가;
                                int tik_cap = Method.Find_Tik_Cap(현재가, 주문가격, Market.Market);
                                double 수익률 = 0;
                                int OrderN = GET.Order번호();

                                JumunItem JumunItem_Add = new JumunItem(0, 0, GET.ScreenNum(), 종목코드, 종목명, 주문번호, "---", "리스트'누락'", 0, 1, 60, 0, 0, "", "TR_미체결조회", 주문수량, 주문가격, 유형, 0, 0, 30, 현재가, 0, 주문시간,
                                                                        미체결수량, true, true, 1, tik_cap, tik_price, 수익률, false, 0, OrderN, 0, NXT_server);
                                JumunItem_List.Add(JumunItem_Add);

                                동작_Log("미체결 확인:: " + 종목명 + " 주문유형: " + 주문구분 + " 주문수량: " + 주문수량 + " 미체결수량: " + 미체결수량 + " 주문번호: " + 주문번호);

                                JumunItem = JumunItem_Add;
                            }

                            if (GridView_Print.find_OutstandingStock(JumunItem)) GridView_Print.Outstanding_insert(JumunItem, 0); // 주문내역요청

                            if (!로딩완료) 동작_Log("미체결 확인:: " + 종목명 + " 주문유형: " + 주문구분 + " 주문번호: " + 주문번호);
                        }
                    }
                }

                if (e.sPrevNext.Equals("2"))
                {
                    jostRequestTrDataManager.DequeueStop();

                    axKHOpenAPI1.SetInputValue("계좌번호", Properties.Settings.Default.select_account);
                    axKHOpenAPI1.SetInputValue("전체종목구분", "0");
                    axKHOpenAPI1.SetInputValue("매매구분", "0");
                    axKHOpenAPI1.SetInputValue("종목코드", "0");
                    axKHOpenAPI1.SetInputValue("체결구분", "1");
                    axKHOpenAPI1.SetInputValue("거래소구분", "1");

                    int result_opt10075_A = axKHOpenAPI1.CommRqData("TR_주문내역요청_12", "opt10075", int.Parse(e.sPrevNext), "1112");
                    if (result_opt10075_A == 0)
                    {
                        if (!로딩완료) 동작_Log(Properties.Settings.Default.select_account + " 주문 접수내역 추가요청 ");
                    }
                    else
                    {
                        Error_Log(Properties.Settings.Default.select_account + " 주문 접수내역 추가요청 실패 메세지: " + GET.오류코드(result_opt10075_A));
                    }
                    Delay(300);
                    return;
                }

                if (!e.sPrevNext.Equals("2"))
                {
                    jostRequestTrDataManager.DequeueRun();

                    foreach (var item in JumunItem_List.ToList())
                    {
                        if (item.주문동기화)
                        {
                            JumunItem_List.Remove(item);
                        }
                    }

                    if (!로딩완료) 동작_Log("주문내역요청 요청 완료");

                    if (timenow > Properties.Settings.Default.MT_misu_time && Properties.Settings.Default.CB_misu)
                    {
                        주문내역요청 = true;
                    }
                }
            }

            if (e.sRQName.Equals("TR_예수금요청_13")) // opw00004
            {
                bool 성공 = long.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "예수금"), out long 계좌예수금);

                if (성공)
                {
                    long D2예수금 = long.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "D+2추정예수금").Trim()); //D+2 예수금

                    Acc[0].D2 = D2예수금;

                    List<JumunItem> 매수리스트 = JumunItem_List.FindAll(o => o.주문유형 == 1);
                    if (매수리스트.Count > 0)
                    {
                        for (int i = 0; i < 매수리스트.Count; i++)
                        {
                            JumunItem 주문 = 매수리스트[i];

                            int 주문가격 = 주문.주문가격;
                            if (주문가격 == 0)
                                주문가격 = 주문.현재가;

                            Acc[0].추정D2 = D2예수금 - (주문가격 * 주문.주문수량);
                        }
                    }
                    else
                    {
                        Acc[0].추정D2 = D2예수금;
                    }

                    if (!매수증거금)
                    {
                        AutoClosingAlram("매수 가능합니다. 예수금 확인후 주문하세요.", "매수가능 알림", 5, "동작");
                        매수증거금 = true;

                        foreach (var Market in Market_Item_List.Values)
                        {
                            Market.매수증거금알림 = true;
                        }
                    }
                }

                if (timenow < Properties.Settings.Default.MT_misu_time && Properties.Settings.Default.CB_misu || !Properties.Settings.Default.CB_misu)
                {
                    JanGo_dataGridView.Rows.Clear();

                    var sortedStocks = stockBalanceList.Values.OrderByDescending(stock => stock.초기매수일);

                    foreach (var stock in sortedStocks)
                    {
                        int rowIndex = JanGo_dataGridView.Rows.Add();
                        JanGo_dataGridView.Rows[rowIndex].Cells["코드_잔고A"].Value = stock.종목코드;
                        GridView_Print.JangoRow_print(rowIndex, stock);
                    }

                    JanGo_dataGridView.ClearSelection();
                    Console.WriteLine("잔고 업데이트 ---------------------------------");
                }


                //if ((timenow < Properties.Settings.Default.MT_misu_time && Properties.Settings.Default.CB_misu) || !Properties.Settings.Default.CB_misu)
                //{
                //    JanGo_dataGridView.Rows.Clear();
                //    var newList = from pair in stockBalanceList orderby pair.Value.초기매수일 ascending select pair;
                //    foreach (var code in newList.ToList())
                //    {
                //        Stockbalance 잔고 = code.Value;
                //        JanGo_dataGridView.Rows.Insert(0);
                //        JanGo_dataGridView["코드_잔고A", 0].Value = 잔고.종목코드;
                //        GridView_Print.JangoRow_print(0, 잔고);
                //    }

                //    Console.WriteLine("잔고 업데이트 ---------------------------------");
                //}
            }

            if (e.sRQName.Contains("TR_잔고보정요청_14"))
            {
                /////////////////////////////////////////////////////////////////////////////////////////////
                ///////                                    멀티데이터                                   /////
                /////////////////////////////////////////////////////////////////////////////////////////////
                ///////                                   보유잔고조회                                   /////
                /////////////////////////////////////////////////////////////////////////////////////////////

                string 보정Code = e.sRQName.Split('^')[1];

                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                for (int i = 0; i < count; i++)
                {
                    string co_de = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목번호").Trim();
                    if (co_de.Length > 0)
                    {
                        string Code = co_de.Substring(1);

                        if (Code != null)
                        {
                            string itemCode = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목번호").Trim().Substring(1);
                            string itemName = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim();
                            int 보유수량 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "보유수량").Trim());
                            int 주문가능수량 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매매가능수량").Trim());

                            if (itemCode.Equals(보정Code))
                            {
                                if (stockBalanceList.TryGetValue(itemCode, out Stockbalance 잔고))
                                {
                                    잔고.보유수량 = 보유수량;
                                    잔고.주문가능수량 = 주문가능수량;
                                    잔고.매도기준 = 보유수량;

                                    알림창("[ 잔고보정 ]\n\n" + itemName + " 보유수량:" + 보유수량 + " 주문가능수량:" + 주문가능수량 + "\n\n잔고가 보정되었습니다.", 5, false);

                                    동작_Log(" ");
                                    동작_Log("[잔고보정완료] 종목:" + itemName + " 보유수량:" + 보유수량 + " 주문가능수량:" + 주문가능수량 + " 으로 보정되었습니다.");
                                    동작_Log(" ");

                                    e.sPrevNext = "";
                                }
                            }
                        }
                    }
                }

                if (e.sPrevNext.Equals("2"))
                {
                    jostRequestTrDataManager.DequeueStop();

                    axKHOpenAPI1.SetInputValue("계좌번호", Properties.Settings.Default.select_account);
                    axKHOpenAPI1.SetInputValue("비밀번호", "");
                    axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
                    axKHOpenAPI1.SetInputValue("조회구분", "1");
                    axKHOpenAPI1.SetInputValue("거래소구분", "KRX");

                    int opw00018잔고보정 = axKHOpenAPI1.CommRqData(e.sRQName, "opw00018", 2, GET.ScreenNum()); // 100종목 까지 가능
                    if (opw00018잔고보정 == 0)
                    {
                        //  동작_Log("잔고보정 추가요청 ");
                    }
                    else
                    {
                        Error_Log(Properties.Settings.Default.select_account + " 잔고보정 추가요청 실패 메세지: " + GET.오류코드(opw00018잔고보정));
                    }

                    Delay(300);
                    return;
                }

                if (!e.sPrevNext.Equals("2"))
                {
                    보정 item = 잔고보정_list.Find(o => o.코드.Equals(보정Code));
                    if (item != null)
                    {
                        잔고보정_list.Remove(item);
                    }

                    jostRequestTrDataManager.DequeueRun();
                }
            }

            if (e.sRQName.Equals("TR_opt10074_실현손익요청")) // 실현손익
            {
                long.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "실현손익"), out long 실현손익);

                long 손익률기준금 = Properties.Settings.Default.MT_sonik_price;
                double 매도손익률 = 0;

                if (실현손익 != 0)
                {
                    매도손익률 = 실현손익 / (double)손익률기준금 * 100;
                }

                double 실현손익률 = Math.Round(매도손익률, 2);

                Acc[0].실현손익 = 실현손익;
                Acc[0].실현손익률 = 실현손익률;
            }

            if (e.sRQName.Contains("opt10001")) // 주식기본정보요청
            {
                string[] para = e.sRQName.Split('^');
                string condition = para[1];
                string name = para[2];
                string itemCode = para[3];
                bool 성공 = double.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가"), out double 현재가_);

                if (성공)
                {
                    double 현재가 = Math.Abs(현재가_);
                    int 기준가 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "기준가"));
                    double 등락율 = double.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "등락율"));

                    double.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "시가"), out double 시가_);
                    double.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "고가"), out double 고가_);
                    double.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "저가"), out double 저가_);
                    double 시가 = Math.Abs(시가_);
                    double 고가 = Math.Abs(고가_);
                    double 저가 = Math.Abs(저가_);

                    string 시가총액 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "시가총액").Trim();
                    string 누적거래량 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim();
                    string 전일거래량대비 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래대비").Trim();
                    string 거래회전율 = "0";

                    if (Market_Item_List.ContainsKey(itemCode))
                    {
                        Market_Item Market = Market_Item_List[itemCode];
                        Market.start_price = (int)시가;
                        Market.현재가 = (int)현재가;
                        Market.등락율 = 등락율;
                        Market.누적거래량 = int.Parse(누적거래량);

                        if (para[0].Equals("NEWBUYopt10001"))
                        {
                            if (Method.매매확인_VI_모투가능확인(Market, 1))
                            {
                                // string para = "NEWBUYopt10001^" + 검색식 + "^" + name + "^" + itemCode + "^" + screen + "^" + value + "^" + Jumun + "^" + Choice + "^" + ratio + "^" + 취소시간 + "^" + 취소N주문 + "^" + 반복횟수 ;

                                string screen = para[4];
                                double value = double.Parse(para[5]);
                                int Jumun = int.Parse(para[6]);
                                int Choice = int.Parse(para[7]);
                                double ratio = double.Parse(para[8]);
                                int 취소시간 = int.Parse(para[9]);
                                int 취소N주문 = int.Parse(para[10]);
                                int 반복횟수 = int.Parse(para[11]);

                                Tab_Basic.신규매수실행(itemCode, name, condition, para[4], value, Jumun, Choice, ratio, (int)현재가, 취소시간, 취소N주문, 반복횟수); //신규매수 주문넣기
                            }
                        }
                        else if (para[0].Equals("수동예약opt10001"))
                        {
                            Order_Reserve.수동종목선택(Market);
                            Order_Reserve.예약종목선택(Market);
                        }
                        else if (para[0].Equals("수동opt10001"))
                        {
                            Order_Reserve.수동종목선택(Market);
                        }
                        else if (para[0].Equals("예약opt10001"))
                        {
                            Order_Reserve.예약종목선택(Market);
                        }
                        else if (para[0].Equals("예약실행opt10001"))
                        {
                            Order_Reserve.예약주문_등록(int.Parse(para[4]), Market);
                        }
                        else if (para[0].Equals("Watch_opt10001"))
                        {
                            Tab_Watch.Watch_update(itemCode, 등락율, 현재가, 누적거래량, 0, (int)시가, (int)고가, (int)저가, 0, 전일거래량대비, 거래회전율, 시가총액);
                        }

                        Method.실시간시세등록(itemCode);
                    }
                }
            }

            if (e.sRQName.Equals("TR_주식분봉차트조회요청"))
            {
                string Code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim().Substring(0, 6);

                Stockbalance 잔고 = stockBalanceList[Code];
                잔고.분_리스트 = "";

                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                for (int i = 0; i < count; i++)
                {
                    if (Code != null)
                    {
                        double.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim(), out double 현재가);
                        string date = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "체결시간").Trim();

                        // Console.WriteLine("현재가: " + 현재가);
                        //   Console.WriteLine("date: " + date);

                        if (잔고.분_리스트.Length < 1)
                        {
                            잔고.분_리스트 = Math.Abs(현재가).ToString();
                        }
                        else
                        {
                            잔고.분_리스트 = 잔고.분_리스트 + ";" + Math.Abs(현재가);
                        }

                        if (i == 299)
                        {
                            break;
                        }
                    }
                }

                MA.Get_Min_Moving_Average(잔고);

                //   Console.WriteLine(잔고.종목명 + " 분리스트 : " + 잔고.분_리스트);
                //   Console.WriteLine("#####   TR_주식분봉차트조회요청 종료: " + DateTime.Now.ToString("HH:mm:ss.ffff") + " 남은수 : " + jostRequestTrDataManager.count());
            }

            if (e.sRQName.Equals("TR_주식일봉차트조회요청"))
            {
                string Code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim().Substring(0, 6);

                Stockbalance 잔고 = stockBalanceList[Code];
                잔고.일_리스트 = "";

                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                for (int i = 0; i < count; i++)
                {
                    if (Code != null)
                    {
                        if (Code != null)
                        {
                            double.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim(), out double 현재가);
                            string 일자 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "일자").Trim();

                            //Console.WriteLine("현재가: " + 현재가);
                            //Console.WriteLine("일자: " + 일자);

                            if (잔고.일_리스트.Length < 1)
                            {
                                잔고.일_리스트 = Math.Abs(현재가).ToString();
                            }
                            else
                            {
                                잔고.일_리스트 = 잔고.일_리스트 + ";" + Math.Abs(현재가);
                            }

                            if (i == 299)
                            {
                                break;
                            }
                        }
                    }
                }

                MA.Get_Day_Moving_Average(잔고);

                //   Console.WriteLine("#####   TR_주식일봉차트조회요청 종료: " + DateTime.Now.ToString("HH:mm:ss.ffff") + " 남은수 : " + jostRequestTrDataManager.count());

            }

            if (e.sRQName.Equals("TR_업종분봉조회요청"))
            {
                string Code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "업종코드").Trim();

                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                for (int i = 0; i < count; i++)
                {
                    if (Code != null)
                    {
                        double.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim(), out double 현재가);
                        string date = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "체결시간").Trim();

                        if (Code.Equals("001")) kospi_avg_min.Add(new AVG_price(Code, (Math.Abs(현재가) / 100), date));
                        if (Code.Equals("101")) kosdaq_avg_min.Add(new AVG_price(Code, (Math.Abs(현재가) / 100), date));
                        if (i == 59) break;
                    }
                }

                if (Code.Equals("101")) TR_업종일봉조회요청();
            }



            if (e.sRQName.Equals("TR_업종일봉조회요청"))
            {
                string Code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "업종코드").Trim();

                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                for (int i = 0; i < count; i++)
                {
                    if (Code != null)
                    {
                        double.TryParse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim(), out double 현재가);
                        string date = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "일자").Trim();

                        if (Code.Equals("001"))
                        {
                            kospi_avg_day.Add(new AVG_price(Code, (현재가 / 100), date));
                        }
                        if (Code.Equals("101"))
                        {
                            kosdaq_avg_day.Add(new AVG_price(Code, (현재가 / 100), date));
                        }
                        if (i == 59) break;
                    }
                }

                if (Code.Equals("101"))
                {
                    foreach (var item in stockBalanceList)
                    {
                        TR_주식분봉차트조회요청(item.Value.종목코드);
                        TR_주식일봉차트조회요청(item.Value.종목코드);
                    }

                    Console.WriteLine("sRQName TR_업종일봉조회요청");

                    RealData_Management.AVG_jisu_print("001", kospi_avg_min[0].endprice);
                    RealData_Management.AVG_jisu_print("101", kosdaq_avg_min[0].endprice);

                    매매시작 = "차트조회요청";
                    동작_Log(" ");
                    동작_Log("MoneyGame 에 입장 하였습니다.");

                    if (!DateTime.Now.DayOfWeek.Equals(DayOfWeek.Saturday) && !DateTime.Now.DayOfWeek.Equals(DayOfWeek.Sunday))
                    {
                        if ((server_알림.Contains("마켓") || server_알림.Contains("동시") || server_알림.Contains("동시")) && !공휴일)
                        {
                            동작_Log(" ");
                            동작_Log("MoneyGame 이 '시작' 되었습니다.");
                        }

                        if (공휴일)
                        {
                            server_알림 = "공휴일";
                            동작_Log(" ");
                            동작_Log("공휴일 입니다. MoneyGame 을 할수 없습니다.");
                            Jine_Run = false;
                            장종료();

                            RB_sell_stop.Checked = true;
                            RB_buy_stop.Checked = true;
                        }
                    }

                }
            }



            {
                //Console.WriteLine("\n////////////////////////////  시작  API_TR   ///////////////////////////");
                //Console.WriteLine("e.sScrNo: " + e.sScrNo);
                //Console.WriteLine("e.sRQName: " + e.sRQName);
                //Console.WriteLine("e.sTrCode: " + e.sTrCode);
                //Console.WriteLine("e.nDataLength: " + e.nDataLength);
                //Console.WriteLine("e.sErrorCode: " + e.sErrorCode);
                //Console.WriteLine("e.sMessage: " + e.sMessage);
                //Console.WriteLine("e.sPrevNext: " + e.sPrevNext);
                //Console.WriteLine("e.sRecordName: " + e.sRecordName);
                //Console.WriteLine("e.sRQName: " + e.sRQName);
                //Console.WriteLine("e.sSplmMsg: " + e.sSplmMsg);
                //Console.WriteLine("e.sTrCode: " + e.sTrCode);
                //Console.WriteLine("////////////////////////////  끝  API_TR   ///////////////////////////\n");
            }
        }

        private void API_OnReceiveMsg(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEvent e)
        {
            //Console.WriteLine("\n////////////////////////////  시작  API_OnReceiveMsg: ///////////////////////////");
            //Console.WriteLine("e.sScrNo: " + e.sScrNo);
            //Console.WriteLine("e.sRQName: " + e.sRQName);
            //Console.WriteLine("e.sTrCode: " + e.sTrCode);
            //Console.WriteLine("e.sMsg: " + e.sMsg);
            //Console.WriteLine("////////////////////////////  끝  API_OnReceiveMsg: ///////////////////////////\n");

            if (e.sMsg.Contains("매수증거금이"))
            {
                if (매수증거금)
                {
                    Error_Log("[키움서버 알림] " + e.sMsg);
                    AutoClosingAlram("[키움서버 알림] 매수증거금이 부족합니다. 최대 5분간 매수주문이 일시정지됩니다.", "매수증거금부족", 10, "동작");
                    매수증거금 = false;
                }
            }
        }

        private void Load_completion()
        {
            form1.BeginInvoke((MethodInvoker)delegate
            {
                combo_watch_condition_AA.Enabled = true;
                combo_watch_condition_BB.Enabled = true;
                combo_watch_condition_CC.Enabled = true;
                combo_watch_condition_DD.Enabled = true;
            });

            int.TryParse(DateTime.Now.ToString("HHmmss"), out int Time);
            로딩완료타임 = Time;

            Console.WriteLine("/////////////////////////// 로딩완료  :: " + DateTime.Now.ToString("hh:mm:ss.ffff") + " ///////////////////////////");

            if (예약주문_장전 && Properties.Settings.Default.MTB_예약주문_장전주문시간 < Time) 예약주문_장전 = false;
            if (예약주문_종가 && Properties.Settings.Default.MTB_예약주문_종가주문시간 < Time) 예약주문_종가 = false;
            if (매매기간_오전 && Properties.Settings.Default.TB_매매기간_오전주문시간 < Time) 매매기간_오전 = false;
            if (매매기간_오후 && Properties.Settings.Default.TB_매매기간_오후주문시간 < Time) 매매기간_오후 = false;

            if (오전감시 && 오전감시시간 < Time) 오전감시 = false;
            if (오후감시 && 오후감시시간 < Time) 오후감시 = false;

            if (Properties.Settings.Default.MT_starttime - 30 > timenow)
            {
                if (신규매수정지 || 추가매수정지)
                {
                    string 신규 = "가능";
                    if (신규매수정지)
                        신규 = "정지";

                    string 추가 = "가능";
                    if (추가매수정지)
                        추가 = "정지";

                    AutoClosingAlram("신규매수 - [ " + 신규 + " ], 추가매수 - [ " + 추가 + " ] 입니다. \n그룹n기능의 설정을 확인 해주세요.", "매수 정지알림", 30, "동작");
                }
            }

            if (릴리즈) DataManagement.접속확인();
            특정일시작시간조정();

            로딩완료 = true;
            매매시작 = "로딩완료";
        }

        private void 특정일시작시간조정()
        {
            string 날짜 = DateTime.Now.ToString("MMdd");

            if (날짜.StartsWith("01"))
            {
                if (개장일.Equals(날짜))
                {
                    알림창("[ 개장일알림 ]\n\n새해 첫 개장일 입니다.\n\n정규장 시작시간은 10시00분 종료시간은 15시30분 입니다.\n\n시작시간과 종료시간이 자동으로 적용됩니다.", 30, false);

                    동작_Log("");
                    동작_Log("[개장일알림] 시작시간과 종료시간이 자동으로 적용됩니다.");
                    동작_Log("[개장일알림] 새해 첫 개장일 입니다. 정규장 시작시간은 10시00분 종료시간은 15시30분 입니다. 시작시간과 종료시간이 자동으로 적용됩니다.");
                    동작_Log("");

                    if (!CB개장일)
                    {
                        CB개장일 = true;
                        FormPrint.CB_개장일n수능일_Checked("CB_개장일");
                    }
                }
                else
                {
                    if (CB개장일)
                    {
                        CB개장일 = false;
                        FormPrint.CB_개장일n수능일_Checked("CB_개장일");
                    }
                }
            }
            else
            {
                if (수능일.Equals(날짜))
                {
                    알림창("[ 수능일알림 ]\n\n수능일 입니다.\n\n정규장 시작시간은 10시00분 종료시간은 16시30분 입니다.\n\n시작시간과 종료시간이 자동으로 적용됩니다.", 30, false);

                    동작_Log("");
                    동작_Log("[수능일알림] 시작시간과 종료시간이 자동으로 적용됩니다.");
                    동작_Log("[수능일알림] 수능일 입니다. 정규장 시작시간은 10시00분 종료시간은 16시30분 입니다.");
                    동작_Log("");

                    if (!CB수능일)
                    {
                        CB수능일 = true;
                        FormPrint.CB_개장일n수능일_Checked("CB_수능일");
                    }
                }
                else
                {
                    if (CB수능일)
                    {
                        CB수능일 = false;
                        FormPrint.CB_개장일n수능일_Checked("CB_수능일");
                    }
                }
            }
        }

        /////////////////////////       TR 데이터 수신          ///////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////        실시간 데이터 수신          ////////////////////////

        private void API_OnReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            try
            {
                string code = e.sRealKey;
                if (code.Contains("_AL")) code = code.Substring(0, 6);

                //if (!code.Equals("001") && !code.Equals("101") && !code.Equals("P10102") && !code.Equals("09"))
                //{
                //    Console.WriteLine("\n실시간 종목명:: " + Market_Item_List[code].종목명);
                //    Console.WriteLine("e.sRealType:: " + e.sRealType);
                //    Console.WriteLine("e.sRealData:: " + e.sRealData);
                //}
                //else
                //{
                //    Console.WriteLine("e.sRealType:: " + e.sRealType);
                //    Console.WriteLine("e.sRealData:: " + e.sRealData);
                //}



                if (e.sRealType.Equals("주식예상체결"))
                {
                    if (stockBalanceList.ContainsKey(code))
                    {
                        if (stockBalanceList[code].종목상태.Equals("장전동시") || stockBalanceList[code].종목상태.Equals("과열(VI)") || stockBalanceList[code].종목상태.Equals("동시호가"))
                        {
                            RealData_Management.Stock_update(true, code, axKHOpenAPI1.GetCommRealData(code, 10), axKHOpenAPI1.GetCommRealData(code, 12), axKHOpenAPI1.GetCommRealData(code, 13), Market_Item_List[code].누적거래대금.ToString(), Market_Item_List[code].Last_price.ToString());
                        }
                    }
                }

                if (e.sRealType.Equals("예상업종지수"))
                {
                    RealData_Management.Market_update(code, axKHOpenAPI1.GetCommRealData(code, 295), axKHOpenAPI1.GetCommRealData(code, 291), axKHOpenAPI1.GetCommRealData(code, 291), axKHOpenAPI1.GetCommRealData(code, 291));
                }

                if (e.sRealType.Equals("업종지수"))
                {
                    RealData_Management.Market_update(code, axKHOpenAPI1.GetCommRealData(code, 12), axKHOpenAPI1.GetCommRealData(code, 10), axKHOpenAPI1.GetCommRealData(code, 18), axKHOpenAPI1.GetCommRealData(code, 17));
                }

                if (e.sRealType.Equals("업종등락"))
                {
                    RealData_Management.Market_fluctuate(code, axKHOpenAPI1.GetCommRealData(code, 14).Trim(), axKHOpenAPI1.GetCommRealData(code, 251).Trim(), axKHOpenAPI1.GetCommRealData(code, 252).Trim(), axKHOpenAPI1.GetCommRealData(code, 253).Trim(),
                    axKHOpenAPI1.GetCommRealData(code, 254).Trim(), axKHOpenAPI1.GetCommRealData(code, 255).Trim());
                }

                if (e.sRealType.Equals("주식체결"))
                {
                    if (시장가탐색)
                    {
                        Tab_PriceSearch.Stock_search_거래대금(code, axKHOpenAPI1.GetCommRealData(code, 10), axKHOpenAPI1.GetCommRealData(code, 20), axKHOpenAPI1.GetCommRealData(code, 15), axKHOpenAPI1.GetCommRealData(code, 14));
                    }

                    RealData_Management.Stock_update(false, code, axKHOpenAPI1.GetCommRealData(code, 10), axKHOpenAPI1.GetCommRealData(code, 12), axKHOpenAPI1.GetCommRealData(code, 13), axKHOpenAPI1.GetCommRealData(code, 14), axKHOpenAPI1.GetCommRealData(code, 16));

                    RealData_Management.Real_Watch_update(code, axKHOpenAPI1.GetCommRealData(code, 12), axKHOpenAPI1.GetCommRealData(code, 10), axKHOpenAPI1.GetCommRealData(code, 16), axKHOpenAPI1.GetCommRealData(code, 17), axKHOpenAPI1.GetCommRealData(code, 18),
                                                  axKHOpenAPI1.GetCommRealData(code, 13).Trim(), axKHOpenAPI1.GetCommRealData(code, 14), axKHOpenAPI1.GetCommRealData(code, 29), axKHOpenAPI1.GetCommRealData(code, 30).Trim(), axKHOpenAPI1.GetCommRealData(code, 31).Trim(), axKHOpenAPI1.GetCommRealData(code, 311).Trim());
                }

                if (e.sRealType.Equals("주식호가잔량"))
                {
                    if (시장가탐색)
                    {
                        Tab_PriceSearch.Stock_search_호가별대금(code, axKHOpenAPI1.GetCommRealData(code, 42), axKHOpenAPI1.GetCommRealData(code, 43), axKHOpenAPI1.GetCommRealData(code, 44), axKHOpenAPI1.GetCommRealData(code, 45), axKHOpenAPI1.GetCommRealData(code, 46), axKHOpenAPI1.GetCommRealData(code, 47), axKHOpenAPI1.GetCommRealData(code, 48), axKHOpenAPI1.GetCommRealData(code, 49), axKHOpenAPI1.GetCommRealData(code, 50),
                                                axKHOpenAPI1.GetCommRealData(code, 52), axKHOpenAPI1.GetCommRealData(code, 53), axKHOpenAPI1.GetCommRealData(code, 54), axKHOpenAPI1.GetCommRealData(code, 55), axKHOpenAPI1.GetCommRealData(code, 56), axKHOpenAPI1.GetCommRealData(code, 57), axKHOpenAPI1.GetCommRealData(code, 58), axKHOpenAPI1.GetCommRealData(code, 59), axKHOpenAPI1.GetCommRealData(code, 60),
                                                axKHOpenAPI1.GetCommRealData(code, 62), axKHOpenAPI1.GetCommRealData(code, 63), axKHOpenAPI1.GetCommRealData(code, 64), axKHOpenAPI1.GetCommRealData(code, 65), axKHOpenAPI1.GetCommRealData(code, 66), axKHOpenAPI1.GetCommRealData(code, 67), axKHOpenAPI1.GetCommRealData(code, 68), axKHOpenAPI1.GetCommRealData(code, 69), axKHOpenAPI1.GetCommRealData(code, 70),
                                                axKHOpenAPI1.GetCommRealData(code, 72), axKHOpenAPI1.GetCommRealData(code, 73), axKHOpenAPI1.GetCommRealData(code, 74), axKHOpenAPI1.GetCommRealData(code, 75), axKHOpenAPI1.GetCommRealData(code, 76), axKHOpenAPI1.GetCommRealData(code, 77), axKHOpenAPI1.GetCommRealData(code, 78), axKHOpenAPI1.GetCommRealData(code, 79), axKHOpenAPI1.GetCommRealData(code, 80));
                    }

                    if (로딩완료)
                    {
                        GET.StockState(code, axKHOpenAPI1.GetCommRealData(code, 52), axKHOpenAPI1.GetCommRealData(code, 54),
                                             axKHOpenAPI1.GetCommRealData(code, 42), axKHOpenAPI1.GetCommRealData(code, 44));
                    }
                }
            }
            catch (Exception i)
            {
                Console.WriteLine("\n[\n실시간 데이터 수신");
                Console.WriteLine(" e.sRealType:: " + e.sRealType);
                Console.WriteLine(" code:: " + e.sRealKey);
                Console.WriteLine(" 리얼:: " + i.Message + "\n]");
            }
        }

        ///////////////////////        실시간 데이터 수신          /////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        //////////////////            체결잔고 데이터 수신          ////////////////////////
        public static int con_num = 1;
        private void API_OnReceiveChejanData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent e)
        {
            string 계좌번호 = axKHOpenAPI1.GetChejanData(9201);
            string 종목코드 = axKHOpenAPI1.GetChejanData(9001).Substring(1);
            string 주문번호 = axKHOpenAPI1.GetChejanData(9203).Trim();
            string 종목명 = axKHOpenAPI1.GetChejanData(302).Trim();
            string 원주문번호 = axKHOpenAPI1.GetChejanData(904).Trim();
            string 주문유형 = axKHOpenAPI1.GetChejanData(905); // 주문유형 1:신규매수, 2:신규매도 3:수취소, 4:도취소,
            string 매매구분 = axKHOpenAPI1.GetChejanData(906); // 보통(00) or 시장가(03)
            string 주문상태 = axKHOpenAPI1.GetChejanData(913);
            string 화면번호 = axKHOpenAPI1.GetChejanData(920);
            string 매도매수 = axKHOpenAPI1.GetChejanData(946);
            int.TryParse(axKHOpenAPI1.GetChejanData(908), out int 주문N체결시간);
            int.TryParse(axKHOpenAPI1.GetChejanData(930), out int 보유수량);
            int.TryParse(axKHOpenAPI1.GetChejanData(931), out int 평균단가);
            int.TryParse(axKHOpenAPI1.GetChejanData(900), out int 주문수량);
            int.TryParse(axKHOpenAPI1.GetChejanData(910), out int 체결가);
            int.TryParse(axKHOpenAPI1.GetChejanData(911), out int 체결량);
            int.TryParse(axKHOpenAPI1.GetChejanData(914), out int 단위체결가);
            int.TryParse(axKHOpenAPI1.GetChejanData(915), out int 단위체결량);
            int.TryParse(axKHOpenAPI1.GetChejanData(938), out int 매매수수료);
            int.TryParse(axKHOpenAPI1.GetChejanData(939), out int 매매세금);
            int.TryParse(axKHOpenAPI1.GetChejanData(901), out int 주문가격);
            int.TryParse(axKHOpenAPI1.GetChejanData(902), out int 미체결량);
            int.TryParse(axKHOpenAPI1.GetChejanData(27), out int 최우선매도호가);
            int.TryParse(axKHOpenAPI1.GetChejanData(28), out int 최우선매수호가);
            long.TryParse(axKHOpenAPI1.GetChejanData(932), out long 매입금액);
            long.TryParse(axKHOpenAPI1.GetChejanData(990), out long 실현손익);

            int.TryParse(axKHOpenAPI1.GetChejanData(10), out int 현재가_);
            int 현재가 = Math.Abs(현재가_);

            //Console.WriteLine("\n################### 체결잔고 종목: " + 종목명);
            //Console.WriteLine("체결잔고 주문유형: " + 주문유형);
            //Console.WriteLine("체결잔고 매매구분: " + 매매구분);
            //Console.WriteLine("체결잔고 주문상태: " + 주문상태);

            if (현재가 > 0 && Properties.Settings.Default.select_account.Equals(계좌번호))
            {
                if (e.sGubun.Equals("1"))
                {
                    if (stockBalanceList.TryGetValue(종목코드, out Stockbalance 잔고))
                        Conclusion_Management.잔고업데이트(잔고, 현재가, 보유수량, 평균단가, 매입금액, 실현손익);
                }

                if (e.sGubun.Equals("0"))
                {
                    if (매매구분.Equals("시장가"))
                    {
                        if (주문유형.Contains("매수"))
                        {
                            주문가격 = Math.Abs(최우선매도호가);
                        }
                        else if (주문유형.Contains("매도"))
                        {
                            주문가격 = Math.Abs(최우선매수호가);
                        }
                    }

                    if (주문상태.Equals("확인"))
                    {
                        if (주문유형.Contains("취소"))
                        {
                            Conclusion_Management.체결잔고_취소기록(화면번호, 종목코드, 종목명, 현재가, 주문유형.Substring(1), 매매구분, 주문수량, 원주문번호);
                        }
                    }

                    //Console.WriteLine("//////////////////            체결잔고 데이터 수신          ////////////////////////");
                    //Console.WriteLine("주문번호: [" + 주문번호 + "] 종목:" + 종목명);
                    //Console.WriteLine("주문상태 : " + 주문상태 + " 주문수량: " + 주문수량 + " 미체결량:" + 미체결량);
                    //Console.WriteLine("체결가_ : " + axKHOpenAPI1.GetChejanData(910));


                    if (주문상태.Equals("접수"))
                    {

                        if (!주문유형.Contains("취소") && 미체결량 > 0)
                        {
                            Conclusion_Management.체결잔고_주문기록(화면번호, 종목코드, 종목명, 현재가, 주문유형.Substring(1), 매매구분, 주문가격, 주문수량, 주문번호, 주문N체결시간, 미체결량);
                        }
                    }

                    if (주문상태.Equals("체결"))
                    {
                        bool 성공 = int.TryParse(axKHOpenAPI1.GetChejanData(910), out int 체결가_);
                        if (성공)
                        {
                            Conclusion_Management.체결잔고_체결기록(주문번호, 종목코드, 종목명, 단위체결가, 체결가, 단위체결량, 체결량, 매매세금, 매매수수료, 주문수량, 주문N체결시간, 주문유형, 매매구분, 현재가, 미체결량, 화면번호);
                        }
                    }
                }
            }
        }



        //////////////////            체결잔고 데이터 수신          ////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////           검색식 종목 진입&이탈 메소드              ////////////////

        private void API_OnReceiveRealCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent e)
        {
            if (server_알림.Contains("마켓") || server_알림.Contains("동시"))
            {
                if (Market_Item_List.ContainsKey(e.sTrCode))
                {
                    if (!Condition_Catch_List.Contains(e.sTrCode + "^" + e.strConditionName))
                        Condition_Catch_List.Add(e.sTrCode + "^" + e.strConditionName);

                    item_search(e.strType, e.sTrCode, e.strConditionName);
                }
            }
        }

        private void API_OnReceiveTRCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent e)
        {
            string[] code_list = e.strCodeList.Split(';');
            for (int i = 0; i < code_list.Length - 1; i++)
            {
                if (Market_Item_List.ContainsKey(code_list[i]))
                {
                    Form1.form1.Invoke((MethodInvoker)delegate ()
                    {
                        if (검색결과_List.Contains(e.strConditionName))
                            if (!검색결과_List.Contains(code_list[i]))
                                검색결과_List.Add(code_list[i]);

                        if (Properties.Settings.Default.CB_편입추가)
                            item_search("I", code_list[i], e.strConditionName);

                        if (로딩완료)
                        {
                            CBB_실시간n그룹n관심자동.SelectedIndex = 0;
                            Tab_InterestGroup.CBB_실시간n그룹n관심자동_indexchange(CBB_실시간n그룹n관심자동.SelectedIndex);
                        }
                    });
                }
            }

            Tab_InterestGroup.관심_검색종목_등록실행(e.sScrNo, e.strCodeList, e.strConditionName, "I"); //  특정시간 등록
        }

        private void item_search(string ID, string Code, string conditionName)
        {
            string SearchTime = DateTime.Now.ToString("HH:mm:ss");

            var Task = new Task(() =>
            {
                Tab_Basic.New_Buy(ID, Code, conditionName);
                Tab_Repeat.Repeat_condition(ID, Code, conditionName);
                Tab_AccountManagement.Rebalancing_condition(ID, Code, conditionName);
                Tab_AccountManagement.Liquidation_condition(ID, Code, conditionName);
                Tab_Watch.Watch_In_Out(ID, Code, conditionName, SearchTime);

                SearchView_add(ID, Code, conditionName, SearchTime);

                Tab_InterestGroup.관심_검색종목_등록실행("1100", Code, conditionName, ID);
                Tab_InterestGroup.관심검색_실시간보기(conditionName, ID, Code);
            });
            Form1.condition_data_Manager.RequestTrData(Task);
        }

        public void SearchView_add(string ID, string Code, string conditionName, string SearchTime)
        {
            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                if (conditionName.Equals(CBB_SearchCondition.Text) && ID.Equals("I"))
                {
                    if (!SearchView_List.Contains(Code))
                    {
                        SearchView_List.Add(Code);
                        Search_List.Items.Insert(0, SearchTime + " | " + Market_Item_List[Code].종목명);

                        if (Search_List.Items[1].ToString().Contains("지니_64오토스탁")) Search_List.Items.RemoveAt(1);
                        if (Search_List.Items.Count > 8) Search_List.Items.RemoveAt(8);
                    }
                }
            });
        }

        ///////////////           검색식 종목 진입&이탈 메소드              /////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////       서버주문 전달         //////////////////////

        public bool 잔고주문_오더(Stockbalance 잔고, string 검색식, int 주문유형, double 비중, int 비중단위, double 주문값, int 주문구분, int 취소시간, int 취소N주문, int 반복횟수, string 비고, string 위치, int 수익구분, bool 청산, double 범위_1)
        {

            bool 오더_가능 = false;
            string 거래구분 = "00"; // 지정가
            int 주문가격 = Method.order_price(주문값, 주문구분, 잔고.종목코드, 잔고.현재가);
            int 기록_주문가격 = 주문가격;

            if (주문구분.Equals(0))
            {
                거래구분 = "03"; // 시장가
                주문가격 = 0;
                기록_주문가격 = 잔고.현재가;
            }

            Market_Item Market = Market_Item_List[잔고.종목코드];

            if (Method.매매확인_VI_모투가능확인(Market, 주문유형))
            {
                int 주문수량 = Method.주문수량계산(잔고, 기록_주문가격, 비중, 비중단위, 주문유형);
                if (주문수량 < 1) 주문수량 = 1;

                if (주문유형 == 1) // 매수
                {
                    bool 예수금확인()
                    {
                        bool result = false;

                        if (Properties.Settings.Default.CB_misu)
                        {
                            if (매수증거금)
                                result = true;
                            else
                                result = false;
                        }
                        else
                        {
                            for (int i = 0; i > -1; i++)
                            {
                                if (Acc[0].추정D2 < 기록_주문가격)
                                {
                                    if (수량알림)
                                    {
                                        수량알림 = false;

                                        if (Market.매수증거금알림)
                                        {
                                            Market.매수증거금알림 = false;
                                            Error_Log("[매수 제한] 예수금 부족 종목: " + 잔고.종목명 + " 주문가격: " + 기록_주문가격 + " 주문수량: " + 주문수량 + " 주문금액: " + (기록_주문가격 * 주문수량));
                                        }
                                    }

                                    break;
                                }
                                else
                                {
                                    if (Acc[0].추정D2 > 기록_주문가격 * (주문수량 - i))
                                    {
                                        if ((주문수량 - i) > 0)
                                        {
                                            주문수량 = 주문수량 - i;
                                            result = true;
                                        }
                                        else
                                        {
                                            if (Market.매수증거금알림)
                                            {
                                                Market.매수증거금알림 = false;
                                                Error_Log("[매수 제한] 예수금 부족 종목: " + 잔고.종목명 + " 주문가격: " + 기록_주문가격 + " 주문수량: " + 주문수량 + " 주문금액: " + (기록_주문가격 * 주문수량));
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        }

                        return result;
                    }

                    if (예수금확인())
                    {
                        주문수량 = Method.최대매수금_주문수량계산(잔고, 주문수량);

                        if (주문수량 > 0)
                        {
                            주문실행();
                        }
                        else
                        {
                            Error_Log("[매수 제한] 최대 매수금 초과 종목: " + 잔고.종목명 + " 주문가격: " + 기록_주문가격 + " 주문수량: " + 주문수량 + " 주문금액: " + (기록_주문가격 * 주문수량));
                        }

                        수량알림 = true;
                    }
                }
                else // 매도
                {
                    if (청산)
                    {
                        주문수량 = Method.청산주문_매매범위_주문수량계산(잔고, 주문수량, 범위_1);
                    }

                    if (위치.Contains("수익금손절"))
                    {
                        int 단위손절금 = 잔고.현재가 - 잔고.평균단가;

                        while (true)
                        {
                            double 남길금액 = Form1.form1.Cut_남길금액_A;
                            if (검색식.Equals("수익금손절_B")) 남길금액 = Form1.form1.Cut_남길금액_B;
                            if (검색식.Equals("수익금손절_C")) 남길금액 = Form1.form1.Cut_남길금액_C;

                            if (남길금액 <= (실현손익_예상 + (주문수량 * 단위손절금)) || 주문수량 == 1)
                            {
                                실현손익_예상 = 실현손익_예상 + (주문수량 * 단위손절금);
                                break;
                            }

                            if (남길금액 > (실현손익_예상 + (주문수량 * 단위손절금))) 주문수량--;
                        }
                    }

                    if (주문수량 > 0 && 잔고.주문가능수량 >= 주문수량)
                    {
                        주문실행();
                    }
                }

                void 주문실행()
                {
                    if (NXT_server)
                    {
                        if (NXT) Run();
                    }
                    else
                    {
                        Run();
                    }

                    void Run()
                    {
                        int ScreenNumber = GET.jumunScreen(잔고.종목코드);
                        if (ScreenNumber == 1300)
                        {
                            Method.주문초과알림(잔고.종목명);
                        }
                        else
                        {
                            if (주문유형 == 1)
                            {
                                DataManagement.예수금업데이트(GET.주문유형(주문유형), 기록_주문가격, 주문수량, "주문", 잔고.종목코드);
                            }
                            else
                            {
                                DataManagement.주문가능수업데이트(잔고, "매도", 주문수량, "매도주문");
                                금액알림 = true;
                            }

                            int Order번호 = GET.Order번호();


                            JumunItem ItemList = new JumunItem(0, 0, ScreenNumber.ToString(), 잔고.종목코드, 잔고.종목명, "+++", "---", 검색식, 주문값, 주문구분, 취소시간, 취소N주문, 반복횟수, 비고, 위치, 주문수량, 기록_주문가격, 주문유형, 비중, 비중단위, 취소시간, 잔고.현재가, 잔고.등락율, Form1.timenow,
                                                               주문수량, true, false, 0, Method.Find_Tik_Cap(잔고.현재가, 주문가격, 잔고.시장),
                                                               잔고.현재가, 잔고.수익률, false, 0, Order번호, 수익구분, Form1.NXT_server); // 자동 매도 일때  주문추가 
                            JumunItem_List.Add(ItemList);

                            que_order(ScreenNumber.ToString(), 잔고.종목명, 주문유형, 잔고.종목코드, 주문수량, 주문가격, 거래구분, "+++", 검색식, Order번호);

                            오더_가능 = true;

                            Tab_AccountManagement.리밸등록("+++", ScreenNumber.ToString(), 잔고.종목코드, 위치, 주문수량, 주문가격, 수익구분);
                        }
                    }
                }
            }

            return 오더_가능;
        }

        public static void que_order(string Screennum, string 종목명, int 주문유형, string 종목코드, int 주문수량, int 주문가, string 거래구분, string 주문번호, string 검색식, int Order번호)  // SendOrder Queue 로 주문 넣기 
        {
            Order add_order = new Order(Screennum, 주문유형, 종목코드, 주문수량, 주문가, 거래구분, 주문번호, 검색식, 종목명, Order번호, false);
            Order_list.Add(add_order);
        }

        private void 서버주문전달()
        {
            if (Order_list.Count > 0)
            {
                Order item = Order_list[0];

                JumunItem JumunItem = JumunItem_List.Find(o => o.Order번호.Equals(item.Order번호));
                if (JumunItem != null)
                {
                    string 주문번호 = "";
                    int 주문가격 = item.주문가;
                    int 주문수량 = item.주문수량;

                    try
                    {
                        if (item.주문유형 == 2 || item.주문유형 == 4)
                        {
                            if (!stockBalanceList.ContainsKey(item.종목코드))
                            {
                                Order_list.Remove(item);
                                JumunItem_List.Remove(JumunItem);
                                return;
                            }
                            else
                            {
                                if (item.주문유형 == 2 && item.주문수량 == 0)
                                {
                                    Order_list.Remove(item);
                                    JumunItem_List.Remove(JumunItem);
                                    return;
                                }
                            }
                        }

                        //   XXX주문    1 = XXX매수,  2 = XXX매도,  3 = 매수취소, 4 = 매도취소 5 = 매수정정 6 = 매도정정
                        //   NXT주문   21 = NXT매수, 22 = NXT매도, 23 = NXT취소, 25 = NXT정정 (주의 24 아님!!)
                        //

                        int 주문 = item.주문유형;

                        if (NXT_server && server.Equals("실서버"))
                        {
                            주문 = 23;
                            if (item.주문유형 == 1) 주문 = 21;
                            if (item.주문유형 == 2) 주문 = 22;
                        }

                        if (item.주문유형 == 3 || item.주문유형 == 4)
                        {
                            주문번호 = item.원주문번호;
                            주문수량 = 0;
                            주문가격 = 0;

                            if (JumunItem.NXT)
                            {
                                주문 = 23;
                            }
                            else
                            {
                                주문 = item.주문유형;
                            }
                            //   if (JumunItem.NXT || (JumunItem.주문시간 < (메인마켓시작 - 5000))) 주문 = 23;
                        }

                        if (item.거래구분.Equals("03"))
                        {
                            주문가격 = 0;
                        }

                        int Result = axKHOpenAPI1.SendOrder("주문", item.screennum, Properties.Settings.Default.select_account, 주문, item.종목코드, 주문수량, 주문가격, item.거래구분, 주문번호); // 신규 일때 원주문번호 = null 취소일때 원주문번호 입력. 

                        if (Result == 0)
                        {
                            //if (server.Equals("실서버"))
                            //{
                            //    if (item.주문유형 == 1)
                            //    {

                            //        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@ SOR주문 성공: " + 주문 + " 시간: " + timenow.ToString("##:##:##  Order번호 : ") + item.Order번호);
                            //        Console.WriteLine("종목: " + item.종목명);
                            //        Console.WriteLine("item.screennum: " + item.screennum);
                            //        Console.WriteLine("주문수량: " + 주문수량);
                            //        Console.WriteLine("주문가격: " + 주문가격);
                            //        Console.WriteLine("거래구분: " + item.거래구분);
                            //        Console.WriteLine("주문번호: " + 주문번호);
                            //        Console.WriteLine("원주문번호: " + JumunItem.원주문번호);
                            //    }
                            //}
                        }
                        else
                        {
                            //if (server.Equals("실서버")) Console.WriteLine("$$$$$$$$$$$$$$$$$$$$ SOR주문 실패: " + 주문);

                            알림창("[ 키움서버 에러알림 ]\n\n메세지: " + GET.오류코드(Result) + " -->\n\n종목명: " + item.종목명 + " 검색식: " + item.검색식 + " 주문유형: " + GET.주문유형(item.주문유형) + "\n\n키움서버로 주문이 전달되지 못 하였습니다.", 10, false);

                            Error_Log(" ");
                            Error_Log("[키움서버 에러알림] 메세지: " + GET.오류코드(Result) + " --> 종목명: " + item.종목명 + " 검색식: " + item.검색식 + " 주문유형: " + GET.주문유형(item.주문유형) + " 키움서버로 주문이 전달되지 못 하였습니다.");
                            Error_Log(" ");
                            Console.WriteLine("[키움서버 에러알림] 메세지: " + GET.오류코드(Result) + " --> 종목명: " + item.종목명 + " 검색식: " + item.검색식 + " 주문유형: " + GET.주문유형(item.주문유형) + "키움서버로 주문이 전달되지 못 하였습니다.");
                        }

                        주문카운터();
                        Order_list.Remove(item);

                        if (JumunItem.주문유형 == 1 || JumunItem.주문유형 == 2) // 매매주문
                        {
                            JumunItem.Order_count = 1;
                        }
                        else // 취소주문
                        {
                            JumunItem.Order_count = 4;
                        }
                    }
                    catch
                    {
                        Error_Log("[주문에러] 주문오더가 잘못 되었습니다. --> 종목명: " + item.종목명 + " 검색식: " + item.검색식 + " 주문유형: " + GET.주문유형(item.주문유형) + " 키움서버로 주문이 전달되지 못 하였습니다.");
                    }
                }
                else
                {
                    Console.WriteLine("서버주문전달 삭제 :: " + item.종목명 + " 오더번호: " + item.Order번호);

                    Order_list.Remove(item);
                }
            }
        }

        private void 시세요청()
        {
            if (매매시작.Equals("매매시작") && TR_catch_Item_List.Count > 0)
            {
                bool result = TR_catch_Item_List.TryDequeue(out string item);

                if (result)
                {
                    string itemcode = item.Split(';')[0]; // itemcode
                    string parameter = item.Split(';')[1]; // para

                    if (Market_Item_List[itemcode].현재가 == 0)
                    {
                        Que_TRopt10001_request(itemcode, parameter, "시세조회");
                    }
                    else
                    {
                        string[] para = parameter.Split('^');
                        string condition = para[1];
                        string name = para[2];
                        string itemCode = para[3];

                        if (Market_Item_List.TryGetValue(itemCode, out Market_Item Market))
                        {
                            if (para[0].Equals("NEWBUYopt10001"))
                            {
                                if (Method.매매확인_VI_모투가능확인(Market, 1))
                                {
                                    // string para = "NEWBUYopt10001^" + 검색식 + "^" + name + "^" + itemCode + "^" + screen + "^" + value + "^" + Jumun + "^" + Choice + "^" + ratio + "^" + 취소시간 + "^" + 취소N주문 + "^" + 반복횟수 + "^" + DateTime.Now.ToString("HH:mm:ss");

                                    string screen = para[4];
                                    double value = double.Parse(para[5]);
                                    int Jumun = int.Parse(para[6]);
                                    int Choice = int.Parse(para[7]);
                                    double ratio = double.Parse(para[8]);
                                    int 취소시간 = int.Parse(para[9]);
                                    int 취소N주문 = int.Parse(para[10]);
                                    int 반복횟수 = int.Parse(para[11]);

                                    Tab_Basic.신규매수실행(itemCode, name, condition, screen, value, Jumun, Choice, ratio, Market.현재가, 취소시간, 취소N주문, 반복횟수); //신규매수 주문넣기
                                }
                            }
                            else if (para[0].Equals("수동예약opt10001"))
                            {
                                Order_Reserve.수동종목선택(Market);
                                Order_Reserve.예약종목선택(Market);
                            }
                            else if (para[0].Equals("수동opt10001"))
                            {
                                Order_Reserve.수동종목선택(Market);
                            }
                            else if (para[0].Equals("예약opt10001"))
                            {
                                Order_Reserve.예약종목선택(Market);
                            }
                            else if (para[0].Equals("예약실행opt10001"))
                            {
                                Order_Reserve.예약주문_등록(int.Parse(para[4]), Market);
                            }
                            else if (para[0].Equals("Watch_opt10001"))
                            {
                                Que_TRopt10001_request(itemcode, parameter, "시세조회");
                            }

                            Method.실시간시세등록(itemCode);
                        }
                    }
                }
            }
        }

        private void 예약주문_RUN()
        {
            bool 성공 = 예약주문_List.TryDequeue(out 주문예약 주문);
            if (성공)
            {
                Market_Item Market = Market_Item_List[주문.종목코드];

                if (Market.현재가 > 1)
                {
                    string 상하 = "하한가";
                    int 주문유형 = 1;

                    if (주문.검색식.Contains("매도"))
                    {
                        상하 = "상한가";
                        주문유형 = 2;
                    }

                    string para = Method.주문가계산(주문.종목코드, 주문.주문비, Market.현재가, Market.현재가, 상하);

                    int 주문가 = int.Parse(para.Split('&')[0]);
                    int 주문호가 = int.Parse(para.Split('&')[2]);
                    int 주문수량 = Method.주문수량계산(null, 주문가, 주문.비중, 주문.선택, 주문유형);

                    Order_Reserve.예약주문_주문실행(주문, 주문가, 주문수량, 주문호가);
                }
                else
                {
                    예약주문_List.Enqueue(주문);
                }
            }
        }



        ////////////////////////       서버주문 전달         //////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        //////////////////           현재시간 및 시간활용 메소드               //////////////


        public static int Writing_time = 0;


        public static void 장종료()
        {
            foreach (var code in stockBalanceList.ToList())
            {
                Stockbalance 잔고 = code.Value;
                잔고.매매가능 = false;
                잔고.종목상태 = GET.Jango_state(잔고.종목코드);
            }

            if (!server_알림.Equals("공휴일"))
            {
                server_알림 = "장종료";
                List<JumunItem> 취소_item = JumunItem_List.FindAll(o => o.주문번호.Contains("+"));
                if (취소_item.Count > 0 && Order_list.Count > 0)
                {
                    for (int a = 0; a < 취소_item.Count; a++)
                    {
                        DataManagement.주문가능수업데이트(stockBalanceList[취소_item[a].종목코드], GET.주문유형(취소_item[a].주문유형), 취소_item[a].주문수량, "일괄취소");
                        DataManagement.예수금업데이트(GET.주문유형(취소_item[a].주문유형), 취소_item[a].주문가격, 취소_item[a].주문수량, "일괄취소", 취소_item[a].종목코드);
                        JumunItem_List.Remove(취소_item[a]);
                    }
                }

                Console.WriteLine("\n::장 종료 알림:: ");
            }
        }


        //////////////////          현재시간 및 시간활용 메소드               ///////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        /////////////////////            그룹&관심 동작            ////////////////

        private void 그룹선택_CheckedChanged(object sender, EventArgs e)
        {
            체크박스_비프(sender);

            CheckBox CB = (sender as CheckBox);
            string text = CB.Text.Substring(4);
            if (CB.Checked)
            {
                CB.Text = "■ 전체" + text;
                CB.ForeColor = Color.Red;
            }
            else
            {
                CB.Text = "□ 선택" + text;
                CB.ForeColor = Color.Black;
            }
        }

        private void 검색시간_CheckedChanged(object sender, EventArgs e)
        {
            체크박스_비프(sender);

            CheckBox CB = (sender as CheckBox);
            if (CB.Checked)
            {
                CB.Text = "■ 실시간";
                CB.ForeColor = Color.Red;
            }
            else
            {
                CB.Text = "□검색시간";
                CB.ForeColor = Color.Black;
            }
        }

        private void Box_CheckedChanged(object sender, EventArgs e)
        {
            체크박스_비프(sender);

            CheckBox CB = (sender as CheckBox);
            string Text = CB.Text.Substring(1);
            if (CB.Checked)
            {
                CB.Text = "■" + Text;
                CB.ForeColor = Color.Red;

                if (sender.Equals(CB_실시간검색결과보기))
                {
                    CBB_실시간n그룹n관심자동.SelectedIndex = 0;
                    Tab_InterestGroup.CBB_실시간n그룹n관심자동_indexchange(CBB_실시간n그룹n관심자동.SelectedIndex);
                }
            }
            else
            {
                CB.Text = "□" + Text;
                CB.ForeColor = Color.Black;
            }
        }

        private void CBB_실시간n그룹n관심자동_DropDownClosed(object sender, EventArgs e)
        {
            if (로딩완료) 비프음("체크");
            Tab_InterestGroup.CBB_실시간n그룹n관심자동_indexchange(CBB_실시간n그룹n관심자동.SelectedIndex);
        }

        private void BT_관심그룹변경_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Tab_InterestGroup.BT_관심그룹변경_Click();
        }

        private void BT_그룹추가_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Tab_InterestGroup.BT_그룹추가_Click();
        }

        private void BT_그룹삭제_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Tab_InterestGroup.BT_그룹삭제_Click();
        }

        private void 콤보_관심그룹_DropDownClosed(object sender, EventArgs e)
        {
            Tab_InterestGroup.콤보_관심그룹_DropDownClosed(sender);
        }

        private void BT_검색요청_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            if (CBB_관심검색식.Text.Length > 0)
                Tab_InterestGroup.검색요청(GET.ScreenNum(), CBB_관심검색식.Text);
            else
                AutoClosingAlram("검색식이 선택되지 않았습니다.", "검색식알림", 5, "동작");
        }

        private void BT_관심등록_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Tab_InterestGroup.BT_관심등록_Click();
        }

        private void BT_관심삭제_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Tab_InterestGroup.BT_관심삭제_Click();
        }

        public void CBB_신규그룹_DropDownClosed(object sender, EventArgs e)
        {
            if (로딩완료) 비프음("체크");
            Tab_InterestGroup.CBB_신규그룹_DropDownClosed();
        }

        private void BT_자동등록_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Tab_InterestGroup.BT_자동등록_Click();
        }

        private void BT_자동해제_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Tab_InterestGroup.BT_자동해제_Click();
        }

        private void BT_자동삭제_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Tab_InterestGroup.BT_자동삭제_Click();
        }

        private void LB_검색결과n관심리스트_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tab_InterestGroup.LB_검색결과n관심리스트_SelectedIndexChanged();
        }

        private void LB_관심_A_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tab_InterestGroup.LB_관심_A_SelectedIndexChanged(sender);
        }

        private void LB_관심_A_Click(object sender, EventArgs e)
        {
            Tab_InterestGroup.LB_관심_A_Click(sender);
        }

        /////////////////////            그룹&관심 동작            ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////////              선택청산 매매            ////////////////

        private void BT_jangos_sell_Click(object sender, EventArgs e) // 잔고 선택청산
        {
            this.ActiveControl = null;
            if (server_알림.Contains("마켓") || server_알림.Contains("동시"))
            {
                MBC_sender = (sender as Button).Name;
                중요메세지("잔고 '청산'", "선택된 종목을 전량청산 하시겠습니까?");
            }
            else
            {
                AutoClosingAlram("정규 시장이 종료 되었습니다.", "장종료", 5, "동작");
            }
        }

        ////////////////              선택청산 매매                ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////       재시작 동작 & 일괄취소            ////////////////

        public static void 재시작가동(string title, string text)
        {
            재시작 = false;

            form1.RB_sell_stop.Checked = true;
            form1.RB_buy_stop.Checked = true;

            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                form1.CB_기본매매.Checked = false;
                form1.CB_반복매매.Checked = false;
                form1.CB_계좌관리.Checked = false;
                form1.CB_특수매매.Checked = false;
                form1.CB_대금탐색.Checked = false;
                form1.CB_매매그룹.Checked = false;
                form1.CB_기능설정.Checked = false;

                form1.tab_잔고.SelectedIndex = 0;
                form1.tab_주문.SelectedIndex = 0;
                if (form1.tab_체결.TabCount > 1) form1.tab_체결.SelectedIndex = 1;
            });

            동작_Log(" ");
            동작_Log(" ");
            동작_Log("[" + title + "] " + text);

            foreach (var code in stockBalanceList.ToList())
            {
                Stockbalance 잔고 = code.Value;
                잔고.매매가능 = false;
            }
            Properties.Settings.Default.Save();

            HI지니_64시작 = true;
            Application.Exit();
        }

        private void BT_재시작_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            MBC_sender = (sender as Button).Name;
            중요메세지("재시작", "재시작 하시겠습니까 ?");
        }

        private void BT_미체결취소_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            MBC_sender = (sender as Button).Name;
            중요메세지("미체결 취소", "미체결 주문을 일괄 취소 하시겠습니까 ? (매매가 정지됩니다.)");
        }

        public void 미체결일괄취소()
        {
            List<JumunItem> 취소_item = JumunItem_List.FindAll(o => o.주문번호.Contains("+"));
            if (취소_item.Count > 0)
            {
                for (int i = 0; i < 취소_item.Count; i++)
                {
                    Order 오더 = Order_list.Find(o => o.Order번호 == 취소_item[i].Order번호);
                    if (오더 != null)
                    {
                        Order_list.Remove(오더);
                        DataManagement.주문가능수업데이트(stockBalanceList[취소_item[i].종목코드], GET.주문유형(취소_item[i].주문유형), 취소_item[i].주문수량, "일괄취소");
                        DataManagement.예수금업데이트(GET.주문유형(취소_item[i].주문유형), 취소_item[i].주문가격, 취소_item[i].주문수량, "일괄취소", 취소_item[i].종목코드);
                        JumunItem_List.Remove(취소_item[i]);
                    }
                }
            }

            for (int i = 0; i < JumunItem_List.Count; i++)
            {
                JumunItem JumunItem = JumunItem_List[i];

                JumunItem.반복횟수 = 0;
                JumunItem.취소시간 = 0;
                JumunItem.취소timer = 0;

                JumunItem.비고 = "미체결 '취소'";
            }
        }
        //////////////////       재시작 동작 & 일괄취소            ////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        ///////////////////             WAtch 검색식           //////////////////

        private void CBB_Watch_DropDownClosed(object sender, EventArgs e)
        {
            if (로딩완료) 비프음("체크");
        }

        ////////////////////    실시간 화면 저장 && 불러오기   /////////////////////////////

        private void BT_watch_Save_Click(object sender, EventArgs e) // 실시간 화면 :: 조건식 선택 화면 값 저장하기
        {
            this.ActiveControl = null;
            비프음("실행");

            if (form1.account_comboBox.Text.Length > 0)
            {
                MBC_sender = "Watch_Save";
                중요메세지("Watch저장", "Watch 검색식과 TEST 설정 을 저장 하겠 습니까 ?");
            }
            else
            {
                if (form1.axKHOpenAPI1.GetConnectState() > 0)//서버 연결 상태 확인. 
                {
                    if (form1.account_comboBox.Text.Length < 1)
                    {
                        Message_Alram("'Watch' :: 계좌번호를 선택하여 주세요.", "계좌선택");
                    }
                }
            }
        }

        private void BT_test_viwe_A_Click(object sender, EventArgs e)
        {
            Tab_Watch.BT_test_viwe_A_Click(sender);
        }

        ////////////////////////////    Watch 검색식    //////////////////////////
        /////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////////      검색식 10개 제한 하기       ////////////////////

        //조건식 불러와서 콤보박스에 넣기
        private void API_OnReceiveConditionVer(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEvent e)
        {
            Condition_Management.API_OnReceiveConditionVer();
        }

        private void CB_condition_CheckedChanged(object sender, EventArgs e) // 체크박스 와 콤보박스 사용 갯수 제한 
        {
            Condition_Management.CB_condition_CheckedChanged(sender);

            시장가탐색 = Condition_Management.시장가대금탐색();
        }

        private void combo_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (로딩완료)
                Condition_Management.combo_condition_SelectedIndexChanged(sender);
        }

        private void combo_Condition_Add(object sender, EventArgs e)
        {
            Condition_Management.Condition_Add(sender);
        }

        private void combo_Condition_TextChanged(object sender, EventArgs e)
        {
            Condition_Management.Condition_TextChanged(sender);
        }

        private void combo_Watch_DropDown(object sender, EventArgs e)
        {
            Tab_Watch.combo_Watch_DropDown(sender);
        }

        private void combo_watch_DropDownClosed(object sender, EventArgs e)
        {
            if (로딩완료) 비프음("체크");
            Tab_Watch.combo_watch_DropDownClosed(sender);
        }

        private void combo_watch_Changed(object sender, EventArgs e)
        {
            Tab_Watch.combo_watch_Changed(sender);
        }

        ////////////////////  검색식불러오기 10개 제한 하기  ////////////////////
        /////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////    메소드 모음    /// ///////////////////////



        ////////////////////////////    로그기록    /// ///////////////////////
        public static void 동작_Log(string log) // 로그 기록하기 
        {
            string Time = DateTime.Now.ToString("HH:mm:ss :: ");

            Form1.form1.Invoke((MethodInvoker)delegate ()  //      
            {
                try
                {
                    if (Form_run)
                    {
                        form1.LB_Log.Items.Insert(0, Time + log);

                        int index = 26;
                        if (form1.CBB_layout.SelectedIndex == 0) index = 26;
                        if (form1.CBB_layout.SelectedIndex == 1) index = 25;
                        if (form1.CBB_layout.SelectedIndex == 2) index = 25;
                        if (form1.CBB_layout.SelectedIndex == 3) index = 25;
                        if (form1.CBB_layout.SelectedIndex == 4) index = 12;
                        if (form1.CBB_layout.SelectedIndex == 5) index = 26;
                        if (form1.CBB_layout.SelectedIndex == 6) index = 22;
                        if (form1.CB_기본매매.Checked || form1.CB_반복매매.Checked || form1.CB_계좌관리.Checked ||
                            form1.CB_특수매매.Checked || form1.CB_대금탐색.Checked || form1.CB_매매그룹.Checked ||
                            form1.CB_기능설정.Checked) index = 24;

                        if (form1.LB_Log.Items.Count > index)
                        {
                            while (form1.LB_Log.Items.Count > index)
                            {
                                form1.LB_Log.Items.RemoveAt(form1.LB_Log.Items.Count - 1);
                            }
                        }
                    }
                }
                catch
                {
                    AutoClosingAlram("동작Log.txt 저장 실패 설정 을확인하세요", "에러로그", 10, "에러");
                }
            });


            Task Wr_Manager = new Task(() =>
            {
                Form1.form1.Invoke((MethodInvoker)delegate ()  //      
                {
                    try
                    {
                        if (Properties.Settings.Default.select_account != null)
                        {
                            string filePath = Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\Log\\동작_Log\\" + DateTime.Now.ToString("yyyyMMdd") + "_동작Log.txt";

                            FileInfo fi = new FileInfo(filePath);

                            if (!fi.Exists)
                            {
                                using (StreamWriter sw = new StreamWriter(filePath))
                                {
                                    sw.WriteLine(Time + log);
                                }
                            }
                            else
                            {
                                using (StreamWriter sw = System.IO.File.AppendText(filePath))
                                {
                                    sw.WriteLine(Time + log);
                                }
                            }
                        }
                    }
                    catch
                    {
                        AutoClosingAlram("동작Log.txt 저장 실패 설정 을확인하세요", "에러로그", 10, "에러");
                    }
                });
            });
            writing_Manager.RequestTrData(Wr_Manager); // 생성된 Task  요청 등록. 
        }

        public static void Error_Log(string log) // 로그 기록하기 
        {
            string Time = DateTime.Now.ToString("HH:mm:ss :: ");

            Form1.form1.Invoke((MethodInvoker)delegate ()  //      
            {
                try
                {
                    if (Form_run)
                    {
                        form1.LB_error.Items.Insert(0, Time + log);

                        while (form1.LB_error.Items.Count > 81)
                        {
                            form1.LB_error.Items.RemoveAt(form1.LB_error.Items.Count - 1);
                        }
                    }
                }
                catch
                {
                    AutoClosingAlram("_ErrorLog 저장 실패 설정 을확인하세요", "에러로그", 10, "에러");
                }
            });

            Task Wr_Manager = new Task(() =>
            {
                Form1.form1.Invoke((MethodInvoker)delegate ()  //      
                {
                    try
                    {
                        if (Properties.Settings.Default.select_account != null)
                        {
                            string filePath = Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\Log\\Error_Log\\" + DateTime.Now.ToString("yyyyMMdd") + "_ErrorLog.txt";

                            FileInfo fi = new FileInfo(filePath);

                            if (!fi.Exists)
                            {
                                using (StreamWriter sw = new StreamWriter(filePath))
                                {
                                    sw.WriteLine(Time + log);
                                }
                            }
                            else
                            {
                                using (StreamWriter sw = System.IO.File.AppendText(filePath))
                                {
                                    sw.WriteLine(Time + log);
                                }
                            }
                        }
                    }
                    catch
                    {
                        AutoClosingAlram("_ErrorLog 저장 실패 설정 을확인하세요", "에러로그", 10, "에러");
                    }
                });
            });
            writing_Manager.RequestTrData(Wr_Manager); // 생성된 Task  요청 등록. 
        }

        ////////////////////////////    메소드 모음    /// ///////////////////////
        /////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////////
        ////////////////////        버튼및 기타등등 동작 설정     //////////////////////////
        //////////////////////////////////////////////////////////////////////////////////

        private void CheckBox_Check_TEXT_Changed(object sender, EventArgs e)
        {
            체크박스_비프(sender);
            CheckBox CB = (sender as CheckBox);

            string text = CB.Text.Substring(1);
            if (CB.Checked)
            {
                CB.Text = "■" + text;
            }
            else
            {
                CB.Text = "□" + text;

                if (sender.Equals(CB_misu)) Properties.Settings.Default.CB_misu = false;
                if (sender.Equals(CB_계좌매입비_매수제한)) Properties.Settings.Default.CB_계좌매입비_매수제한 = false;
                if (sender.Equals(CB_잔고매입비_추매제한)) Properties.Settings.Default.CB_잔고매입비_추매제한 = false;
            }
        }

        private void CBB_지수연동_추매_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBB_지수연동_신규.SelectedIndex > 1) 순매수조회 = true;
            if (CBB_지수연동_추매.SelectedIndex > 1) 순매수조회 = true;

            순매수조회변경();
        }

        public static void 순매수조회변경()
        {
            순매수조회 = false;

            if (Properties.Settings.Default.CB_지수연동범위사용) 순매수조회 = true;
            if (form1.CBB_지수연동_신규.SelectedIndex > 1) 순매수조회 = true;
            if (form1.CBB_지수연동_추매.SelectedIndex > 1) 순매수조회 = true;
        }

        private void Once_CheckedChanged(object sender, EventArgs e)
        {
            체크박스_비프(sender);

            if (sender.Equals(CB_Auto_tradingstart))
            {
                CB_Auto_tradingstart.Text = CB_Auto_tradingstart.Checked ? "자동 시작" : "수동 시작";
            }     CB_Auto_tradingstart.ForeColor = CB_Auto_tradingstart.Checked ? Color.Red : Color.Black;

            string 위치 = "A:";
            DataGridView DGV = dataGridView_watch_A;

            if (sender.Equals(CB_watch_remove_A))
            {
                if (!CB_watch_remove_A.Checked)
                {
                    DGVinsert();
                }
            }

            if (sender.Equals(CB_watch_remove_B))
            {
                if (!CB_watch_remove_B.Checked)
                {
                    위치 = "B:";
                    DGV = dataGridView_watch_B;
                    DGVinsert();
                }
            }

            if (sender.Equals(CB_watch_remove_C))
            {
                if (!CB_watch_remove_C.Checked)
                {
                    위치 = "C:";
                    DGV = dataGridView_watch_C;
                    DGVinsert();
                }
            }

            if (sender.Equals(CB_watch_remove_D))
            {
                if (!CB_watch_remove_D.Checked)
                {
                    위치 = "D:";
                    DGV = dataGridView_watch_D;
                    DGVinsert();
                }
            }

            void DGVinsert()
            {
                DGV.Rows.Clear();
                Dictionary<string, Watch> findResult = Watch_List.Where(o => o.Key.Contains(위치)).ToDictionary(o => o.Key, o => o.Value);
                foreach (var result in findResult.ToList())
                {
                    DGV.Rows.Insert(0);
                    DGV[23, 0].Value = findResult[result.Key].Code;
                }
            }
        }

        private void TB_추정자산_TextChanged(object sender, EventArgs e)
        {
            long.TryParse(TB_추정자산.Text.Replace(",", ""), out long 추정자산);

            if (Properties.Settings.Default.MT_principal < 추정자산)
            {
                (sender as TextBox).ForeColor = Color.Red;
            }
            else if (Properties.Settings.Default.MT_principal == 추정자산)
            {
                (sender as TextBox).ForeColor = Color.Black;
            }
            else if (Properties.Settings.Default.MT_principal > 추정자산)
            {
                (sender as TextBox).ForeColor = Color.Blue;
            }
        }

        /// /////////// 텍스트박스 색변화 && 글자제한

        private void 숫자콤마넣기_TextChanged(object sender, EventArgs e)
        {
            TextValue.숫자콤마넣기_TextChanged(sender);
        }

        private void TextBox_소수자리제한(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_소수자리제한(sender);
        }

        private void TextBox_빨파검(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_빨파검(sender);
        }

        public void TextBox_빨파검_소수2자리제한(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_빨파검_소수2자리제한(sender);
        }

        private void TextBox_음수만입력_소수2자리제한(object sender, EventArgs e) // 사용
        {
            TextValue.TextBox_음수만입력_소수2자리제한(sender);
        }

        private void _양수음수소수_키프레스(object sender, KeyPressEventArgs e) // 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, true); // textbox 에 양수, 음수 , 소수  숫자만 입력 받을수 있음 
        }

        private void _양수소수_키프레스(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, false); // textbox 에 양수 , 소수 숫자만 입력 받을수 있음
        }

        private void _양수실수_키프레스(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, false); // textbox 에 양수 , 실수 숫자만 입력 받을수 있음
        }

        private void _양수음수실수_키프레스(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, true); // textbox 에 양수, 음수 , 실수 숫자만 입력 받을수 있음
        }

        ///////////////////////////////////////////////////////////////////////////////////////////


        private void BT_condition_loading_Click(object sender, EventArgs e) //서버에서 사용자 조건식 전문 불러오기
        {
            this.ActiveControl = null;
            MBC_sender = (sender as Button).Name;
            중요메세지("검색식요청", "검색식을 불러오기 하시겠습니까 ?");
        }

        private void 매도시작_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_sell_run.Checked)
            {
                RB_sell_run.Text = "매도";
                RB_sell_run.ForeColor = Color.Blue;
                RB_sell_stop.Text = "정지";

                동작_Log(" ");
                동작_Log("지니_64 가 '매도' 를 시작 합니다.");
            }
            else
            {
                RB_sell_run.Text = "시작";
                RB_sell_run.ForeColor = Color.Black;
                RB_sell_stop.Text = "매도";

                if (Form_run)
                {
                    for (int i = 0; i < JumunItem_List.Count; i++)
                    {
                        JumunItem_List[i].반복횟수 = 0;
                    }
                }

            }
            CheckeBox_Beep_도미솔(sender);
        }

        private void 매수시작_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_buy_run.Checked)
            {
                RB_buy_run.Text = "매수";
                RB_buy_run.ForeColor = Color.Red;
                RB_buy_stop.Text = "정지";

                if (신규매수정지 || 추가매수정지)
                {
                    string 신규 = "가능";
                    if (신규매수정지)
                        신규 = "정지";

                    string 추가 = "가능";
                    if (추가매수정지)
                        추가 = "정지";

                    AutoClosingAlram("신규매수 - [ " + 신규 + " ], 추가매수 - [ " + 추가 + " ] 입니다. \n그룹n기능의 설정을 확인 해주세요.", "매수 정지알림", 30, "동작");
                }

                동작_Log(" ");
                동작_Log("지니_64 가 '매수' 를 시작 합니다.");
            }
            else
            {
                RB_buy_run.Text = "시작";
                RB_buy_run.ForeColor = Color.Black;
                RB_buy_stop.Text = "매수";

                동작_Log(" ");
                동작_Log("지니_64 가 '매수' 를 멈춥니다.");
            }

            if (!RB_buy_run.Checked && RB_sell_run.Checked)
            {
                if (JumunItem_List.Count > 0)
                {
                    MBC_sender = "체결대기주문취소";
                    중요메세지("취소알림", "체결대기 중 주문도 같이 '취소' 하시겠습니까 ?");
                }
            }

            CheckeBox_Beep_도미솔(sender);
        }


        ////////////////////////////// 그리드뷰 설정 변화  ////////////////////////////////////


        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)             // 원하는 칼럼에 자동 번호 매기기
        {
            if (sender.Equals(JanGo_dataGridView))
            {
                this.JanGo_dataGridView.Rows[e.RowIndex].Cells["NUM_잔고A"].Value = JanGo_dataGridView.Rows.Count - (e.RowIndex + 1) + 1;
            }
            if (sender.Equals(dataGridView_watch_A))
            {
                this.dataGridView_watch_A.Rows[e.RowIndex].Cells["Num_Watch_A"].Value = dataGridView_watch_A.Rows.Count - (e.RowIndex + 1) + 1;
            }
            if (sender.Equals(dataGridView_watch_B))
            {
                this.dataGridView_watch_B.Rows[e.RowIndex].Cells["Num_Watch_B"].Value = dataGridView_watch_B.Rows.Count - (e.RowIndex + 1) + 1;
            }
            if (sender.Equals(dataGridView_watch_C))
            {
                this.dataGridView_watch_C.Rows[e.RowIndex].Cells["Num_Watch_C"].Value = dataGridView_watch_C.Rows.Count - (e.RowIndex + 1) + 1;
            }
            if (sender.Equals(dataGridView_watch_D))
            {
                this.dataGridView_watch_D.Rows[e.RowIndex].Cells["Num_Watch_D"].Value = dataGridView_watch_D.Rows.Count - (e.RowIndex + 1) + 1;
            }
        }

        public void DGV_color(DataGridViewCell cell_T)
        {
            double.TryParse(cell_T.FormattedValue.ToString(), out double T);

            if (T > 0)
            {
                cell_T.Style.ForeColor = Color.Red;
            }
            else if (T < 0)
            {
                cell_T.Style.ForeColor = Color.Blue;
            }
            else if (T == 0)
            {
                cell_T.Style.ForeColor = Color.Black;
            }
        }

        public void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e) // 그리드뷰 색깔 변화   &&  잔고 (A,B) 선택 변화 
        {
            DataGridView view = sender as DataGridView;

            if (e.ColumnIndex == -1 || e.RowIndex == -1)
            {
                return;
            }
            else
            {
                if (sender.Equals(JanGo_dataGridView))
                {
                    try
                    {
                        DataGridViewCell cell_등락율 = view["등락율_잔고A", e.RowIndex];
                        DGV_color(cell_등락율);
                        JanGo_dataGridView["현재가_잔고A", e.RowIndex].Style.ForeColor = cell_등락율.Style.ForeColor;
                        JanGo_dataGridView["종목명_잔고A", e.RowIndex].Style.ForeColor = cell_등락율.Style.ForeColor;

                        DataGridViewCell cell_수익률 = view["수익률_잔고A", e.RowIndex];
                        DGV_color(cell_수익률);
                        JanGo_dataGridView["평가손익_잔고A", e.RowIndex].Style.ForeColor = cell_수익률.Style.ForeColor;

                        DataGridViewCell cell_최고수익률 = view["최고수익률_잔고A", e.RowIndex];
                        DGV_color(cell_최고수익률);

                        DataGridViewCell cell_최저수익률 = view["최저수익률_잔고A", e.RowIndex];
                        DGV_color(cell_최저수익률);

                        DataGridViewCell cell_누적손익 = view["누적손익_잔고A", e.RowIndex];
                        DGV_color(cell_누적손익);

                        DataGridViewCell cell_예상손익 = view["예상손익_잔고A", e.RowIndex];
                        DGV_color(cell_예상손익);

                        DataGridViewCell cell_금일수익금 = view["금일수익금_잔고A", e.RowIndex];
                        DGV_color(cell_금일수익금);

                        if (JanGo_dataGridView.Columns.Contains("시작%")) { DataGridViewCell cell_시작 = view["시작%", e.RowIndex]; DGV_color(cell_시작); }
                        if (JanGo_dataGridView.Columns.Contains("기준%")) { DataGridViewCell cell_기준수익률 = view["기준%", e.RowIndex]; DGV_color(cell_기준수익률); }
                        if (JanGo_dataGridView.Columns.Contains("수익률_A")) { DataGridViewCell 수익률_A = view["수익률_A", e.RowIndex]; DGV_color(수익률_A); }
                        if (JanGo_dataGridView.Columns.Contains("수익률_B")) { DataGridViewCell 수익률_B = view["수익률_B", e.RowIndex]; DGV_color(수익률_B); }
                        if (JanGo_dataGridView.Columns.Contains("수익률_C")) { DataGridViewCell 수익률_C = view["수익률_C", e.RowIndex]; DGV_color(수익률_C); }
                        if (JanGo_dataGridView.Columns.Contains("수익률_D")) { DataGridViewCell 수익률_D = view["수익률_D", e.RowIndex]; DGV_color(수익률_D); }
                        if (JanGo_dataGridView.Columns.Contains("수익률_E")) { DataGridViewCell 수익률_E = view["수익률_E", e.RowIndex]; DGV_color(수익률_E); }
                        if (JanGo_dataGridView.Columns.Contains("수익률_F")) { DataGridViewCell 수익률_F = view["수익률_F", e.RowIndex]; DGV_color(수익률_F); }
                        if (JanGo_dataGridView.Columns.Contains("수익률_G")) { DataGridViewCell 수익률_G = view["수익률_G", e.RowIndex]; DGV_color(수익률_G); }

                    }
                    catch
                    {
                        Error_Log("JanGo_dataGridView_A  에러:: 메세지 _Grid_CellValueChanged 에러");
                    }
                }

                if (sender.Equals(최종매입가View))
                {
                    try
                    {
                        DataGridViewCell 수익률A = view[2, e.RowIndex]; DGV_color(수익률A); 최종매입가View[1, e.RowIndex].Style.ForeColor = 수익률A.Style.ForeColor;
                        DataGridViewCell 수익률B = view[4, e.RowIndex]; DGV_color(수익률B); 최종매입가View[3, e.RowIndex].Style.ForeColor = 수익률B.Style.ForeColor;
                        DataGridViewCell 수익률C = view[6, e.RowIndex]; DGV_color(수익률C); 최종매입가View[5, e.RowIndex].Style.ForeColor = 수익률C.Style.ForeColor;
                        DataGridViewCell 수익률D = view[8, e.RowIndex]; DGV_color(수익률D); 최종매입가View[7, e.RowIndex].Style.ForeColor = 수익률D.Style.ForeColor;
                        DataGridViewCell 수익률E = view[10, e.RowIndex]; DGV_color(수익률E); 최종매입가View[9, e.RowIndex].Style.ForeColor = 수익률E.Style.ForeColor;
                        DataGridViewCell 수익률F = view[12, e.RowIndex]; DGV_color(수익률F); 최종매입가View[11, e.RowIndex].Style.ForeColor = 수익률F.Style.ForeColor;
                        DataGridViewCell 수익률G = view[14, e.RowIndex]; DGV_color(수익률G); 최종매입가View[13, e.RowIndex].Style.ForeColor = 수익률G.Style.ForeColor;
                    }
                    catch
                    {
                        Error_Log("최종매입가View  에러:: 메세지 _Grid_CellValueChanged 에러");
                    }
                }

                if (sender.Equals(dataGridView_watch_A))
                {
                    try
                    {
                        DataGridViewCell cell_구분 = view["매매_Watch_A", e.RowIndex];
                        string text_구분 = cell_구분.FormattedValue.ToString();

                        if (text_구분 == "매수")
                        {
                            cell_구분.Style.ForeColor = Color.Green;
                        }
                        else if (text_구분 == "익절")
                        {
                            cell_구분.Style.ForeColor = Color.Red;
                        }
                        else if (text_구분 == "손절")
                        {
                            cell_구분.Style.ForeColor = Color.Blue;
                        }
                        dataGridView_watch_A["매수가_Watch_A", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;

                        DataGridViewCell cell_등락률 = view["등락율_Watch_A", e.RowIndex];
                        DGV_color(cell_등락률);
                        dataGridView_watch_A["현재가_Watch_A", e.RowIndex].Style.ForeColor = cell_등락률.Style.ForeColor;
                        dataGridView_watch_A["종목명_Watch_A", e.RowIndex].Style.ForeColor = cell_등락률.Style.ForeColor;

                        DataGridViewCell cell_진입후등락 = view["진입후등락_Watch_A", e.RowIndex];
                        DGV_color(cell_진입후등락);

                        DataGridViewCell cell_최고 = view["진입후최고_Watch_A", e.RowIndex];
                        DGV_color(cell_최고);

                        DataGridViewCell cell_최저 = view["진입후최저_Watch_A", e.RowIndex];
                        DGV_color(cell_최저);
                    }
                    catch
                    {
                        Error_Log("dataGridView_watch_A  에러:: 메세지 _Grid_CellValueChanged 에러");
                    }

                }

                if (sender.Equals(dataGridView_watch_B))
                {
                    try
                    {
                        DataGridViewCell cell_구분 = view["매매_Watch_B", e.RowIndex];
                        string text_구분 = cell_구분.FormattedValue.ToString();

                        if (text_구분 == "매수")
                        {
                            cell_구분.Style.ForeColor = Color.Green;
                        }
                        else if (text_구분 == "익절")
                        {
                            cell_구분.Style.ForeColor = Color.Red;
                        }
                        else if (text_구분 == "손절")
                        {
                            cell_구분.Style.ForeColor = Color.Blue;
                        }
                        dataGridView_watch_B["매수가_Watch_B", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;

                        DataGridViewCell cell_등락률 = view["등락율_Watch_B", e.RowIndex];
                        DGV_color(cell_등락률);
                        dataGridView_watch_B["현재가_Watch_B", e.RowIndex].Style.ForeColor = cell_등락률.Style.ForeColor;
                        dataGridView_watch_B["종목명_Watch_B", e.RowIndex].Style.ForeColor = cell_등락률.Style.ForeColor;

                        DataGridViewCell cell_진입후등락 = view["진입후등락_Watch_B", e.RowIndex];
                        DGV_color(cell_진입후등락);

                        DataGridViewCell cell_최고 = view["진입후최고_Watch_B", e.RowIndex];
                        DGV_color(cell_최고);

                        DataGridViewCell cell_최저 = view["진입후최저_Watch_B", e.RowIndex];
                        DGV_color(cell_최저);
                    }
                    catch
                    {
                        Error_Log("dataGridView_watch_B  에러:: 메세지 _Grid_CellValueChanged 에러");
                    }
                }

                if (sender.Equals(dataGridView_watch_C))
                {
                    try
                    {
                        DataGridViewCell cell_구분 = view["매매_Watch_C", e.RowIndex];
                        string text_구분 = cell_구분.FormattedValue.ToString();

                        if (text_구분 == "매수")
                        {
                            cell_구분.Style.ForeColor = Color.Green;
                        }
                        else if (text_구분 == "익절")
                        {
                            cell_구분.Style.ForeColor = Color.Red;
                        }
                        else if (text_구분 == "손절")
                        {
                            cell_구분.Style.ForeColor = Color.Blue;
                        }
                        dataGridView_watch_C["매수가_Watch_C", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;

                        DataGridViewCell cell_등락률 = view["등락율_Watch_C", e.RowIndex];
                        DGV_color(cell_등락률);
                        dataGridView_watch_C["현재가_Watch_C", e.RowIndex].Style.ForeColor = cell_등락률.Style.ForeColor;
                        dataGridView_watch_C["종목명_Watch_C", e.RowIndex].Style.ForeColor = cell_등락률.Style.ForeColor;

                        DataGridViewCell cell_진입후등락 = view["진입후등락_Watch_C", e.RowIndex];
                        DGV_color(cell_진입후등락);

                        DataGridViewCell cell_최고 = view["진입후최고_Watch_C", e.RowIndex];
                        DGV_color(cell_최고);

                        DataGridViewCell cell_최저 = view["진입후최저_Watch_C", e.RowIndex];
                        DGV_color(cell_최저);
                    }
                    catch
                    {
                        Error_Log("dataGridView_watch_C  에러:: 메세지 _Grid_CellValueChanged 에러");
                    }
                }

                if (sender.Equals(dataGridView_watch_D))
                {
                    try
                    {
                        DataGridViewCell cell_구분 = view["매매_Watch_D", e.RowIndex];
                        string text_구분 = cell_구분.FormattedValue.ToString();

                        if (text_구분 == "매수")
                        {
                            cell_구분.Style.ForeColor = Color.Green;
                        }
                        else if (text_구분 == "익절")
                        {
                            cell_구분.Style.ForeColor = Color.Red;
                        }
                        else if (text_구분 == "손절")
                        {
                            cell_구분.Style.ForeColor = Color.Blue;
                        }
                        dataGridView_watch_D["매수가_Watch_D", e.RowIndex].Style.ForeColor = cell_구분.Style.ForeColor;

                        DataGridViewCell cell_등락률 = view["등락율_Watch_D", e.RowIndex];
                        DGV_color(cell_등락률);
                        dataGridView_watch_D["현재가_Watch_D", e.RowIndex].Style.ForeColor = cell_등락률.Style.ForeColor;
                        dataGridView_watch_D["종목명_Watch_D", e.RowIndex].Style.ForeColor = cell_등락률.Style.ForeColor;

                        DataGridViewCell cell_진입후등락 = view["진입후등락_Watch_D", e.RowIndex];
                        DGV_color(cell_진입후등락);

                        DataGridViewCell cell_최고 = view["진입후최고_Watch_D", e.RowIndex];
                        DGV_color(cell_최고);

                        DataGridViewCell cell_최저 = view["진입후최저_Watch_D", e.RowIndex];
                        DGV_color(cell_최저);
                    }
                    catch
                    {
                        Error_Log("dataGridView_watch_D  에러:: 메세지 _Grid_CellValueChanged 에러");
                    }
                }

                if (sender.Equals(DGV_통계))
                {
                    try
                    {
                        DataGridViewCell cell_총매수 = view[0, e.RowIndex];
                        string text_총매수 = cell_총매수.FormattedValue.ToString();

                        if (!text_총매수.Equals("0"))
                            DGV_통계[0, e.RowIndex].Style.ForeColor = Color.Red;

                        DataGridViewCell cell_총매도 = view["총매도금액_통계", e.RowIndex];
                        string text_총매도 = cell_총매도.FormattedValue.ToString();

                        if (!text_총매도.Equals("0"))
                            DGV_통계["총매도금액_통계", e.RowIndex].Style.ForeColor = Color.Blue;

                        DataGridViewCell cell_손익 = view[4, e.RowIndex];
                        string text_손익 = cell_손익.FormattedValue.ToString();

                        DGV_color(cell_손익);
                        DGV_통계[5, e.RowIndex].Style.ForeColor = cell_손익.Style.ForeColor;

                        DataGridViewCell cell_수익횟수 = view["수익횟수_통계", e.RowIndex];
                        string text_수익횟수 = cell_수익횟수.FormattedValue.ToString();
                        DGV_통계["수익횟수_통계", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        int.TryParse(text_수익횟수, out int 수익횟수);
                        if (수익횟수 > 0)
                            DGV_통계["수익횟수_통계", e.RowIndex].Style.ForeColor = Color.Red;

                        DataGridViewCell cell_손실횟수 = view["손실횟수_통계", e.RowIndex];
                        string text_손실횟수 = cell_손실횟수.FormattedValue.ToString();
                        DGV_통계["손실횟수_통계", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        int.TryParse(text_손실횟수, out int 손실횟수);
                        if (손실횟수 > 0)
                            DGV_통계["손실횟수_통계", e.RowIndex].Style.ForeColor = Color.Blue;

                        DataGridViewCell cell_수익률 = view[9, e.RowIndex];
                        string text_수익률 = cell_수익률.FormattedValue.ToString();
                        if (text_수익률.StartsWith("+"))
                            cell_수익률.Style.ForeColor = Color.Red;
                        else if (text_수익률.StartsWith("-"))
                            cell_수익률.Style.ForeColor = Color.Blue;
                        else
                            cell_수익률.Style.ForeColor = Color.Black;
                    }
                    catch
                    {
                        Error_Log("DGV_통계  에러:: 메세지 _Grid_CellValueChanged 에러");
                    }
                }

                if (sender.Equals(DGV_통계B))
                {
                    try
                    {
                        DataGridViewCell cell_손익 = view["실현손익_통계B", e.RowIndex];
                        string text_손익 = cell_손익.FormattedValue.ToString();

                        DGV_color(cell_손익);
                        DGV_통계B["실현손익율_통계B", e.RowIndex].Style.ForeColor = cell_손익.Style.ForeColor;

                        DataGridViewCell cell_수익n손실 = view["수익n손실_통계B", e.RowIndex];
                        string text_수익n손실 = cell_수익n손실.FormattedValue.ToString();
                        DGV_통계B["수익n손실_통계B", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        if (text_수익n손실.Equals("수익"))
                            DGV_통계B["수익n손실_통계B", e.RowIndex].Style.ForeColor = Color.Red;
                        else if (text_수익n손실.Equals("손실"))
                            DGV_통계B["수익n손실_통계B", e.RowIndex].Style.ForeColor = Color.Blue;
                        else
                            DGV_통계B["수익n손실_통계B", e.RowIndex].Style.ForeColor = Color.Black;

                        DataGridViewCell cell_총매수 = view["매수금액_통계B", e.RowIndex];
                        string text_총매수 = cell_총매수.FormattedValue.ToString();

                        if (!text_총매수.Equals("0"))
                            DGV_통계B["매수금액_통계B", e.RowIndex].Style.ForeColor = Color.Red;
                        else
                            DGV_통계B["매수금액_통계B", e.RowIndex].Style.ForeColor = Color.Black;

                        DataGridViewCell cell_총매도 = view["매도금액_통계B", e.RowIndex];
                        string text_총매도 = cell_총매도.FormattedValue.ToString();

                        if (!text_총매도.Equals("0"))
                            DGV_통계B["매도금액_통계B", e.RowIndex].Style.ForeColor = Color.Blue;
                        else
                            DGV_통계B["매도금액_통계B", e.RowIndex].Style.ForeColor = Color.Black;

                        DataGridViewCell cell_수익률 = view[10, e.RowIndex];
                        string text_수익률 = cell_수익률.FormattedValue.ToString();
                        if (통계수익률)
                        {
                            if (text_수익률.Equals("0"))
                                cell_수익률.Style.ForeColor = Color.Black;
                            else if (text_수익률.Contains("-"))
                                cell_수익률.Style.ForeColor = Color.Blue;
                            else
                                cell_수익률.Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            cell_수익률.Style.ForeColor = Color.Black;
                        }
                    }
                    catch
                    {
                        Error_Log("DGV_통계B  에러:: 메세지 _Grid_CellValueChanged 에러");
                    }
                }
            }
        }


        private void DGV_통계_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Statistical_chart.통계_CellClick(sender, e);
        }


        private void DGV_통계B_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Statistical_chart.통계_CellDoubleClick(sender, e);
        }

        public static DateTime Delay(int MS) // Time Delay 함수 
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        private void JanGo_dataGridView_A_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            {
                var datagridview = sender as DataGridView;
                if (e.ColumnIndex == 1)
                {
                    datagridview.BeginEdit(true);
                    ((ComboBox)datagridview.EditingControl).DroppedDown = true;
                }
                DataGridViewRow dataGridViewRow = this.JanGo_dataGridView.Rows[e.RowIndex];
                DataGridViewCell dataGridViewCell = dataGridViewRow.Cells[e.ColumnIndex];

                if (dataGridViewCell is DataGridViewComboBoxCell)
                {
                    JanGo_dataGridView.CurrentCell = dataGridViewCell;
                    JanGo_dataGridView.BeginEdit(true);

                    DataGridViewComboBoxEditingControl comboboxEdit = (DataGridViewComboBoxEditingControl)this.JanGo_dataGridView.EditingControl;

                    if (comboboxEdit != null)
                    {
                        comboboxEdit.DroppedDown = true;
                    }
                }
            }

            if (JanGo_dataGridView.CurrentCell != null)
            {
                if (JanGo_dataGridView.Columns.Contains("기준가"))
                {
                    int _index = JanGo_dataGridView.Columns["기준가"].Index;

                    if (JanGo_dataGridView.CurrentCell.ColumnIndex == _index)
                    {
                        기준값변경 = false;
                    }
                    else
                    {
                        for (int i = 0; i < JanGo_dataGridView.RowCount; i++)
                        {
                            if (JanGo_dataGridView.Rows[i].Cells["기준가"].Value == null) return;

                            string ItemCode = JanGo_dataGridView["코드_잔고A", i].Value.ToString();
                            if (stockBalanceList.TryGetValue(ItemCode, out Stockbalance 잔고))
                            {
                                int.TryParse(JanGo_dataGridView.Rows[i].Cells["기준가"].Value.ToString(), out int 기준가격);

                                if (기준가격 == 0)
                                {
                                    기준가격 = 잔고.평균단가;
                                }

                                잔고.기준가격 = 기준가격;
                                double tax_ = TAX;
                                if (잔고.시장.Equals("E")) tax_ = 0;

                                잔고.기준수익률 = Math.Truncate((((double)(잔고.현재가 - 잔고.기준가격) / 잔고.기준가격 * (double)100) - ((수수료 + 수수료 + tax_) * 100)) * 100) / 100;
                            }
                        }

                        기준값변경 = true;
                    }
                }
            }
        }

        private void JanGo_dataGridView_A_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (JanGo_dataGridView.CurrentCell.ColumnIndex == 1)
            {
                int rowindex = this.JanGo_dataGridView.CurrentCell.RowIndex;
                string group = this.JanGo_dataGridView.Rows[rowindex].Cells[1].Value as string;

                ComboBox combobox = e.Control as ComboBox;

                if (combobox != null)
                {
                    if (JanGo_dataGridView.CurrentCell.RowIndex >= 0)
                    {
                        if (JanGo_dataGridView.CurrentCell.ColumnIndex == 1)
                        {
                            object value = combobox.SelectedItem;
                            combobox.SelectedIndexChanged -= comboBox_SelectedIndexChanged;
                            combobox.Items.Clear();

                            if (group.Length > 0)
                            {
                                combobox.Items.AddRange(new object[] { " ", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" });
                            }

                            combobox.SelectedItem = value;

                            combobox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
                        }
                    }
                }
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (JanGo_dataGridView.CurrentCell.RowIndex >= 0)
                {
                    if (JanGo_dataGridView.CurrentCell.ColumnIndex == 1)
                    {
                        this.JanGo_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        this.JanGo_dataGridView.UpdateCellValue(JanGo_dataGridView.CurrentCell.ColumnIndex, JanGo_dataGridView.CurrentCell.RowIndex);

                        DataGridViewComboBoxCell cbxV = (DataGridViewComboBoxCell)JanGo_dataGridView["그룹_잔고A", JanGo_dataGridView.CurrentCell.RowIndex];

                        string ItemCode = JanGo_dataGridView["코드_잔고A", JanGo_dataGridView.CurrentCell.RowIndex].Value.ToString();
                        int index = cbxV.Items.IndexOf(cbxV.Value);
                        int group = stockBalanceList[ItemCode].매매그룹;

                        if (index != group)
                        {
                            stockBalanceList[ItemCode].매매그룹 = index;

                            stockBalanceList[ItemCode].그룹지정_A = true;
                            stockBalanceList[ItemCode].그룹지정_B = true;
                            stockBalanceList[ItemCode].그룹지정_C = true;
                            stockBalanceList[ItemCode].그룹지정_D = true;

                            DataManagement.save_jango();

                            비프음("체크");
                        }
                    }
                }
            }
            catch
            {
                AutoClosingAlram("[에러 확인] 잔고 그룹변경 정상적으로 변경되지 않았습니다.", "에러알림", 5, "에러");
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e) // 잔고 선택 체크박스 동작
        {
            this.ActiveControl = null;

            DataGridView view = sender as DataGridView;

            if (e.RowIndex == -1) view.CurrentCell = null;

            if (sender.Equals(JanGo_dataGridView))
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    Order_Reserve.종목선택(JanGo_dataGridView["종목명_잔고A", e.RowIndex].Value.ToString());
                    DataGridViewCell cell_color = view[e.ColumnIndex, e.RowIndex];
                    JanGo_dataGridView[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = cell_color.Style.ForeColor;

                    string ItemCode = JanGo_dataGridView["코드_잔고A", e.RowIndex].Value.ToString();

                    try
                    {
                        if (JanGo_dataGridView.Columns["선택_잔고A"].Index == e.ColumnIndex)
                        {
                            if ((bool)JanGo_dataGridView["선택_잔고A", e.RowIndex].Value)
                            {
                                JanGo_dataGridView["선택_잔고A", e.RowIndex].Value = false;
                                stockBalanceList[ItemCode].선택 = false;
                            }
                            else
                            {
                                JanGo_dataGridView["선택_잔고A", e.RowIndex].Value = true;
                                stockBalanceList[ItemCode].선택 = true;
                            }
                        }

                        if (JanGo_dataGridView.Columns.Contains("매도_정지"))
                            if (JanGo_dataGridView.Columns["매도_정지"].Index == e.ColumnIndex)
                            {
                                if ((bool)JanGo_dataGridView["매도_정지", e.RowIndex].Value)
                                {
                                    JanGo_dataGridView["매도_정지", e.RowIndex].Value = false;
                                    stockBalanceList[ItemCode].매도정지 = false;
                                }
                                else
                                {
                                    JanGo_dataGridView["매도_정지", e.RowIndex].Value = true;
                                    stockBalanceList[ItemCode].매도정지 = true;
                                }
                            }

                        if (JanGo_dataGridView.Columns.Contains("추매_정지"))
                            if (JanGo_dataGridView.Columns["추매_정지"].Index == e.ColumnIndex)
                            {
                                if ((bool)JanGo_dataGridView["추매_정지", e.RowIndex].Value)
                                {
                                    JanGo_dataGridView["추매_정지", e.RowIndex].Value = false;
                                    stockBalanceList[ItemCode].추매정지 = false;
                                }
                                else
                                {
                                    JanGo_dataGridView["추매_정지", e.RowIndex].Value = true;
                                    stockBalanceList[ItemCode].추매정지 = true;
                                }
                            }

                        if (JanGo_dataGridView.Columns.Contains("잔고청산_A"))
                            if (JanGo_dataGridView.Columns["잔고청산_A"].Index == e.ColumnIndex)
                            {
                                if ((bool)JanGo_dataGridView["잔고청산_A", e.RowIndex].Value)
                                {
                                    JanGo_dataGridView["잔고청산_A", e.RowIndex].Value = false;
                                    stockBalanceList[ItemCode].잔고청산_A = false;
                                }
                                else
                                {
                                    JanGo_dataGridView["잔고청산_A", e.RowIndex].Value = true;
                                    stockBalanceList[ItemCode].잔고청산_A = true;
                                }
                            }

                        if (JanGo_dataGridView.Columns.Contains("잔고청산_B"))
                            if (JanGo_dataGridView.Columns["잔고청산_B"].Index == e.ColumnIndex)
                            {
                                if ((bool)JanGo_dataGridView["잔고청산_B", e.RowIndex].Value)
                                {
                                    JanGo_dataGridView["잔고청산_B", e.RowIndex].Value = false;
                                    stockBalanceList[ItemCode].잔고청산_B = false;
                                }
                                else
                                {
                                    JanGo_dataGridView["잔고청산_B", e.RowIndex].Value = true;
                                    stockBalanceList[ItemCode].잔고청산_B = true;
                                }
                            }

                        if (JanGo_dataGridView.Columns.Contains("잔고청산_C"))
                            if (JanGo_dataGridView.Columns["잔고청산_C"].Index == e.ColumnIndex)
                            {
                                if ((bool)JanGo_dataGridView["잔고청산_C", e.RowIndex].Value)
                                {
                                    JanGo_dataGridView["잔고청산_C", e.RowIndex].Value = false;
                                    stockBalanceList[ItemCode].잔고청산_C = false;
                                }
                                else
                                {
                                    JanGo_dataGridView["잔고청산_C", e.RowIndex].Value = true;
                                    stockBalanceList[ItemCode].잔고청산_C = true;
                                }
                            }
                    }
                    catch
                    {
                        JanGo_dataGridView.Rows.Remove(JanGo_dataGridView.Rows[e.RowIndex]);
                        JanGo_dataGridView.CurrentCell = null;
                        stockBalanceList.Remove(ItemCode);
                    }
                }
            }

            if (sender.Equals(dataGridView_watch_A))
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    Order_Reserve.종목선택(dataGridView_watch_A["종목명_Watch_A", e.RowIndex].Value.ToString());
                    DataGridViewCell cell_color = view[e.ColumnIndex, e.RowIndex];
                    dataGridView_watch_A[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = cell_color.Style.ForeColor;
                }
            }

            if (sender.Equals(dataGridView_watch_B))
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    Order_Reserve.종목선택(dataGridView_watch_B["종목명_Watch_B", e.RowIndex].Value.ToString());
                    DataGridViewCell cell_color = view[e.ColumnIndex, e.RowIndex];
                    dataGridView_watch_B[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = cell_color.Style.ForeColor;
                }
            }

            if (sender.Equals(dataGridView_watch_C))
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    Order_Reserve.종목선택(dataGridView_watch_C["종목명_Watch_C", e.RowIndex].Value.ToString());
                    DataGridViewCell cell_color = view[e.ColumnIndex, e.RowIndex];
                    dataGridView_watch_C[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = cell_color.Style.ForeColor;
                }
            }

            if (sender.Equals(dataGridView_watch_D))
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    Order_Reserve.종목선택(dataGridView_watch_D["종목명_Watch_D", e.RowIndex].Value.ToString());
                    DataGridViewCell cell_color = view[e.ColumnIndex, e.RowIndex];
                    dataGridView_watch_D[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = cell_color.Style.ForeColor;
                }
            }
        }


        private void dataGridView_JanGo_A_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) // 잔고A 그리드뷰 선택컬럼헤드 체크박스 넣기
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                e.PaintBackground(e.ClipBounds, false);

                Point pt = e.CellBounds.Location;  // where you want the bitmap in the cell

                int nChkBoxWidth = 13;
                int nChkBoxHeight = 13;
                int offsetx = (e.CellBounds.Width - nChkBoxWidth) / 2;
                int offsety = (e.CellBounds.Height - nChkBoxHeight) / 2;

                pt.X += offsetx;
                pt.Y += offsety;

                CheckBox cb = new CheckBox();
                cb.Size = new Size(nChkBoxWidth, nChkBoxHeight);
                cb.Location = pt;

                cb.CheckedChanged += new EventHandler(gvSheetListCheckBox_Checked);

                ((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }
        }


        private void gvSheetListCheckBox_Checked(object sender, EventArgs e) // 잔고A 그리드뷰 선택컬럼헤드 체크박스 전체선택
        {
            foreach (DataGridViewRow Row in JanGo_dataGridView.Rows)
            {
                if (stockBalanceList.TryGetValue(Row.Cells["코드_잔고A"].Value.ToString(), out Stockbalance 잔고))
                {
                    Row.Cells["선택_잔고A"].Value = ((CheckBox)sender).Checked;
                    잔고.선택 = (bool)Row.Cells["선택_잔고A"].Value;
                }
            }
        }

        private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e) // 데이터그리드뷰 동작 속도 올리기
        {
            if (JanGo_dataGridView.CurrentCell is DataGridViewCheckBoxCell)
            {
                JanGo_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            if (dataGridView_watch_A.CurrentCell is DataGridViewCheckBoxCell)
            {
                dataGridView_watch_A.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            if (dataGridView_watch_B.CurrentCell is DataGridViewCheckBoxCell)
            {
                dataGridView_watch_B.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            if (dataGridView_watch_C.CurrentCell is DataGridViewCheckBoxCell)
            {
                dataGridView_watch_C.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            if (dataGridView_watch_D.CurrentCell is DataGridViewCheckBoxCell)
            {
                dataGridView_watch_D.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            if (DGV_통계.CurrentCell is DataGridViewCheckBoxCell)
            {
                DGV_통계.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            if (DGV_통계B.CurrentCell is DataGridViewCheckBoxCell)
            {
                DGV_통계B.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }


        //////////////////////////////////////////////////////////////////////////////////
        ///////////////       계좌 에 따른 검색식 & 투자원금 저장하기      /////////////////
        //////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////// 계좌 설정 저장 ////////////////////////////////   
        private void BT_계좌설정저장_Click(object sender, EventArgs e)
        {
            Form_Top_Most();
            MBC_sender = (sender as Button).Name;
            중요메세지("계좌설정", "계좌설정 저장 하시겠습니까?");
        }



        /////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////    폼 로딩 &    종료전 설정값 불러오기    ///////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////


        private void BT_주문내역_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            비프음("버튼");

            if (Properties.Settings.Default.select_account != null)
            {
                string file = Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\매매내역\\주문내역\\" + DateTime.Now.ToString("yyyyMMdd") + "_주문내역.xlsx";
                FileInfo fileInfo = new FileInfo(file);

                if (fileInfo.Exists)
                {
                    Process.Start(file);
                }
                else
                {
                    file = Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\매매내역\\주문내역\\" + DateTime.Now.ToString("yyyyMMdd") + "_주문내역.txt";
                    fileInfo = new FileInfo(file);
                    if (fileInfo.Exists)
                    {
                        Process.Start(file);
                    }
                    else
                    {
                        Process.Start(Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\매매내역\\주문내역\\");
                    }
                }
            }
        }


        private void BT_체결내역_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            비프음("버튼");

            if (Properties.Settings.Default.select_account != null)
            {
                string file = Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\매매내역\\체결내역\\" + DateTime.Now.ToString("yyyyMMdd") + "_체결내역.xlsx";
                FileInfo fileInfo = new FileInfo(file);

                if (fileInfo.Exists)
                {
                    Process.Start(file);
                }
                else
                {
                    file = Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\매매내역\\체결내역\\" + DateTime.Now.ToString("yyyyMMdd") + "_체결내역.txt";
                    fileInfo = new FileInfo(file);
                    if (fileInfo.Exists)
                    {
                        Process.Start(file);
                    }
                    else
                    {
                        Process.Start(Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\매매내역\\체결내역\\");
                    }
                }
            }
        }

        private void BT_신규매수내역_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            비프음("버튼");

            if (Properties.Settings.Default.select_account != null)
            {
                string file = Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\매매내역\\신규매수\\" + Month + ".xlsx";

                FileInfo fileInfo = new FileInfo(file);

                if (fileInfo.Exists)
                {
                    Process.Start(file);
                }
                else
                {
                    file = Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\매매내역\\신규매수\\" + Month + ".txt";
                    fileInfo = new FileInfo(file);
                    if (fileInfo.Exists)
                    {
                        Process.Start(file);
                    }
                    else
                    {
                        Process.Start(Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\매매내역\\신규매수\\");
                    }
                }
            }
        }

        private void BT_전량매도내역_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            비프음("버튼");

            if (Properties.Settings.Default.select_account != null)
            {
                string file = Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\매매내역\\전량매도\\" + Month + ".xlsx";

                FileInfo fileInfo = new FileInfo(file);

                if (fileInfo.Exists)
                {
                    Process.Start(file);
                }
                else
                {
                    file = Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\매매내역\\전량매도\\" + Month + ".txt";
                    fileInfo = new FileInfo(file);
                    if (fileInfo.Exists)
                    {
                        Process.Start(file);
                    }
                    else
                    {
                        Process.Start(Application.StartupPath + @"\Data\" + USER_ID + "__" + Properties.Settings.Default.select_account + "__\\매매내역\\전량매도\\");
                    }
                }
            }
        }

        public void 체크박스_비프(object sender)
        {
            if (로딩완료)
            {
                if ((sender as CheckBox).Checked)
                {
                    비프음("체크");
                }
                else
                {
                    비프음("언체크");
                }
            }
        }

        private void CheckeBox_Beep_도미솔(object sender)
        {
            if ((sender as RadioButton).Checked)
            {
                비프음("체크");
            }
            else
            {
                비프음("언체크");
            }
        }

        private void CBB_DropDownClosed(object sender, EventArgs e)
        {
            if (로딩완료) 비프음("체크");
        }

        public static void 비프음(string 동작)
        {
            if (!음소거)
            {
                switch (동작)
                {
                    case "체크":
                        Beep(640, 20);// 도 0.05초
                        break;
                    case "언체크":
                        Beep(1024, 20); // 높도 0.05초
                        break;
                    case "버튼":
                        Beep(640, 20);// 도 0.05초
                        Beep(1024, 10); // 솔 0.05초
                        break;
                    case "실행":
                        Beep(512, 10); // 도 0.3초  삐리리
                        Beep(640, 20); // 미 0.3초
                        Beep(768, 30); // 솔 0.3초
                        break;
                    case "정지":
                        Beep(768, 30); // 솔 0.3초
                        Beep(640, 20); // 미 0.3초
                        Beep(512, 10); // 도 0.3초  삐리리
                        break;
                }
            }
        }

        public static void AutoClosingAlram(string text, string title, double time, string 기록)
        {
            if (기록.Equals("동작"))
                동작_Log(text);
            else
                Error_Log(text);

            Thread Task_alram = new Thread(
            () =>
            {
                AutoClosingMessageBox.Show(text, title, (int)(time * 1000));
            });
            Task_alram.Start();
        }

        public void Message_Alram(string text, string title)
        {
            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                using (new CenterWinDialog(form1))
                    MessageBox.Show(new Form { TopMost = false }, text, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            });
        }


        Thread thr_alram;
        public static void 알림창(string text, int time, bool Condition)
        {
            비프음("언체크");

            form1.box1_Closetime = time;

            if (form1.Contains(form1.box1))
            {
                form1.box1.LB_text.BeginInvoke((MethodInvoker)delegate ()
                {
                    if (Condition)
                    {
                        form1.box1.LB_text.Text = "[검색식가동실패] 검색식: " + text + "\n" + time.ToString() + "초 뒤 감시요청 가능 합니다.";
                    }
                    else
                    {
                        form1.box1.LB_text.Text = text;
                    }
                });
            }
            else
            {
                Form1.form1.Invoke((MethodInvoker)delegate ()
                {
                    if (Condition) Condition_timer();
                    else 관심그룹알림창_timer();
                });

                form1.box1.Location = new Point(form1.Width / 2 - 107, form1.Height / 2 - 177);

                void 관심그룹알림창_timer()
                {
                    form1.thr_alram = new Thread(run);
                    form1.thr_alram.IsBackground = true;
                    form1.thr_alram.Start();

                    void run()
                    {
                        Form1.form1.Invoke((MethodInvoker)delegate ()
                        {
                            form1.Controls.Add(form1.box1);
                            form1.box1.BringToFront();
                            form1.box1.button1.Select();
                        });

                        form1.box1.LB_text.BeginInvoke((MethodInvoker)delegate ()
                        {
                            form1.box1.LB_text.Text = text;
                        });

                        while (form1.Contains(form1.box1) && form1.box1_Closetime > -2)
                        {
                            int closetime = form1.box1_Closetime;
                            form1.box1.LB_time.BeginInvoke((MethodInvoker)delegate ()
                            {
                                form1.box1.LB_time.Text = closetime.ToString();
                            });

                            if (form1.box1_Closetime < 0)
                            {
                                form1.box1.BeginInvoke((MethodInvoker)delegate ()
                                {
                                    form1.Controls.Remove(form1.box1);
                                });
                                break;
                            }
                            form1.box1_Closetime--;
                            Thread.Sleep(1000);
                        }
                    }
                }

                void Condition_timer()
                {
                    form1.box1_Closetime = time + 5;

                    form1.thr_alram = new Thread(run);
                    form1.thr_alram.IsBackground = true;
                    form1.thr_alram.Start();

                    void run()
                    {
                        Form1.form1.Invoke((MethodInvoker)delegate ()
                        {
                            form1.Controls.Add(form1.box1);
                            form1.box1.BringToFront();
                            form1.box1.button1.Select();
                        });

                        while (form1.Contains(form1.box1) && form1.box1_Closetime > -2)
                        {
                            int closetime = form1.box1_Closetime;
                            form1.box1.LB_time.BeginInvoke((MethodInvoker)delegate ()
                            {
                                form1.box1.LB_time.Text = closetime.ToString();
                            });

                            form1.box1.LB_text.BeginInvoke((MethodInvoker)delegate ()
                            {
                                int retime = closetime - 5;
                                if (retime < 0) retime = 0;
                                form1.box1.LB_text.Text = "[검색식가동실패] 검색식: " + text + "\n" + (retime).ToString() + "초 뒤 감시요청 가능 합니다.";
                            });

                            if (form1.box1_Closetime < 0)
                            {
                                form1.box1.BeginInvoke((MethodInvoker)delegate ()
                                {
                                    form1.Controls.Remove(form1.box1);
                                });
                                break;
                            }
                            form1.box1_Closetime--;
                            Thread.Sleep(1000);
                        }
                    }
                }
            }
        }

        Thread thr_MBC;

        public static void 중요메세지(string title, string text)
        {
            비프음("버튼");
            form1.thr_MBC = new Thread(run);
            form1.thr_MBC.IsBackground = true;
            form1.thr_MBC.Start();
            form1.MBC_close = false;
            form1.MBC_result = false;

            void run()
            {
                Form1.form1.Invoke((MethodInvoker)delegate ()
                {
                    form1.Controls.Add(form1.MBC);
                    form1.MBC.Location = new Point(759, 246);

                    form1.MBC.BringToFront();
                    form1.MBC.Show();
                    form1.MBC.BT_적용.Select();

                    form1.MBC.LB_title.BeginInvoke((MethodInvoker)delegate ()
                    {
                        form1.MBC.LB_title.Text = title;
                    });

                    form1.MBC.LB_text.BeginInvoke((MethodInvoker)delegate ()
                    {
                        form1.MBC.LB_text.Text = text;
                    });
                });

                while (true)
                {
                    if (form1.MBC_close)
                    {
                        Form1.form1.Invoke((MethodInvoker)delegate ()
                        {
                            if (Form1.form1.Contains(form1.MBC)) Form1.form1.Controls.Remove(form1.MBC);

                        });
                        break;
                    }
                    Thread.Sleep(200);
                }
            }
        }

        // 환경설정저장

        private void BT_Setting_Save_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            비프음("버튼");

            try
            {
                Setting_backup.Userdata_Save();
            }
            catch
            {
                알림창("[ 에러알림 ]\n\n환경설정 저장을 완료하지 못 하였습니다.", 10, false);

                Error_Log(" ");
                Error_Log("[에러알림] 환경설정 저장을 완료하지 못 하였습니다.");
                Error_Log(" ");
            }
        }

        private void BT_Setting_Load_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            비프음("버튼");

            try
            {
                Setting_backup.Userdata_load();
            }
            catch
            {
                알림창("[ 에러알림 ]\n\n환경설정을 불러오지 못 하였습니다.", 10, false);

                Error_Log(" ");
                Error_Log("[에러알림] 환경설정을 불러오지 못 하였습니다.");
                Error_Log(" ");
            }
        }

        private void BT_jagoGroup_initialization_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            MBC_sender = (sender as Button).Name;
            중요메세지("저장확인", "모든 잔고 종목의 그룹을 초기화 하시겠습니까 ?");
        }

        private void tabControl_R_Top_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tab_주문.SelectedIndex == 0)
            {
                L_주문row.Show();
                TB_주문row.Show();
                if (CBB_layout.SelectedIndex != 6)
                {
                    L_주문row.BringToFront();
                    TB_주문row.BringToFront();
                }
            }
            else
            {
                if (CBB_layout.SelectedIndex != 6)
                {
                    L_주문row.Hide();
                    TB_주문row.Hide();
                }
            }

            if (tab_주문.SelectedIndex != 2 && CB_계좌관리.Checked)
            {
                //주문패널
                panel_주문.Location = new Point(1101, 20);
                panel_주문.Size = new Size(820, 333);
                tab_주문.Location = new Point(-5, -3);
                tab_주문.Size = new Size(829, 339);
                LB_JumunList.Location = new Point(-1, 1);
                LB_JumunList.Size = new Size(820, 317);
                LB_error.Location = new Point(-1, 1);
                LB_error.Size = new Size(820, 317);

                Form_JuMun.form.Size = new Size(823, 315);
                Form_JuMun.form.JuMun_dataGridView.Location = new Point(-2, -2);
                Form_JuMun.form.JuMun_dataGridView.Size = new Size(823, 315);
                Form_JuMun.form.종목감추기_주문.Location = new Point(148, 17);
                Form_JuMun.form.종목감추기_주문.Size = new Size(96, 279);
                L_주문row.Location = new Point(586, -1);
                TB_주문row.Location = new Point(675, -1);

                //미체결패널
                Form_Outstanding.form.Location = new Point(1101, 352);
                Form_Outstanding.form.Size = new Size(441, 325);
                Form_Outstanding.form.Outstanding_DataGridView.Location = new Point(-1, 19);
                Form_Outstanding.form.Outstanding_DataGridView.Size = new Size(443, 307);
                Form_Outstanding.form.종목감추기_미체결.Location = new Point(168, 38);
                Form_Outstanding.form.종목감추기_미체결.Size = new Size(101, 271);
                Form_Outstanding.form.label_미체결내역.Location = new Point(-1, -1);
                Form_Outstanding.form.L_미체결row.Location = new Point(108, -1);
                Form_Outstanding.form.TB_미체결row.Location = new Point(199, -1);
                Form_Outstanding.form.LB_미체결주문.Location = new Point(243, -1);


                //체결패널
                panel_체결.Location = new Point(1541, 351);
                panel_체결.Size = new Size(380, 326);
                tab_체결.Location = new Point(-5, -2);
                tab_체결.Size = new Size(392, 331);

                Form_Conclusion.form.Size = new Size(380, 307);
                Form_Conclusion.form.Conclusion_DataGridView.Location = new Point(-2, -1);
                Form_Conclusion.form.Conclusion_DataGridView.Size = new Size(380, 307);
                Form_Conclusion.form.종목감추기_체결.Location = new Point(153, 18);
                Form_Conclusion.form.종목감추기_체결.Size = new Size(96, 271);
                LB_Log.Location = new Point(0, 2);
                LB_Log.Size = new Size(380, 330);
                종목감추기_로그.Location = new Point(135, -1);
                종목감추기_로그.Size = new Size(118, 319);
                L_체결row.Location = new Point(153, 0);
                TB_체결row.Location = new Point(242, 0);

                Form_AccountManagement.form.MaximumSize = new Size(1936, 389);
                Form_AccountManagement.form.MinimumSize = new Size(1936, 389);
                Form_AccountManagement.form.Size = new Size(1936, 389);
            }

            if (tab_주문.SelectedTab.Text.ToString().Equals("동작/감시 보기"))
            {
                if (!동작상태보기)
                {
                    동작상태보기 = true;
                    동작실시간 = true;

                    LB_JumunList.SelectionMode = SelectionMode.None;
                }
            }
            else
            {
                동작상태보기 = false;
                LB_JumunList.Items.Clear();

                LB_JumunList.SelectionMode = SelectionMode.MultiExtended;
            }

            Form1.form1.LB_JumunList.Show();
            Form1.form1.LB_JumunList.BringToFront();
            Form1.form1.최종매입가View.Hide();
            Form1.form1.최종매입가View.SendToBack();
            Form1.form1.최종매입가View.Rows.Clear();
            Form1.form1.CBB_최종가종목.Hide();
            Form1.form1.BT_종목업.Hide();
            Form1.form1.BT_종목다운.Hide();
            Form1.form1.CBB_최종가종목.SendToBack();
            Form1.form1.BT_종목업.SendToBack();
            Form1.form1.BT_종목다운.SendToBack();

            Form1.form1.CBB_최종가종목.Items.Clear();
            최종매입가View.Rows.Clear();
        }

        private void tab_체결_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tab_체결.SelectedIndex == 0)
            {
                L_체결row.Show();
                TB_체결row.Show();
            }
            else
            {
                L_체결row.Hide();
                TB_체결row.Hide();
            }
        }

        private void BT_미체결요청_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            MBC_sender = (sender as Button).Name;
            중요메세지("미체결요청", "미체결내역을 요청 하시겠습니까 ?");
        }

        private void BT_매매내역확인_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            jostRequestTrDataManager.DequeueRun();
            Statistical_chart.매매내역확인();
        }

        private void CBB_통계_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBB_통계.SelectedIndex > -1)
            {
                Statistical_chart.CBB_통계_확인();
            }
        }

        private void BT_기준일별매매확인_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            jostRequestTrDataManager.DequeueRun();
            Statistical_chart.BT_기준일별확인();
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////         설정 값  저장하기           ////////////////////////
        //////////////////////////////////////////////////////////////////////////////////

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!USER_ID.Equals("***"))
                TelegramMessenger.Telegram_alram("logOut");

            Set_default.그룹n관심_save();
            Form_Closing();
        }

        public void Form_Closing()
        {
          
            form1.Enabled = false;
            if (릴리즈) DataManagement.접속종료알림();

            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                CB_기본매매.Checked = false;
                CB_반복매매.Checked = false;
                CB_계좌관리.Checked = false;
                CB_특수매매.Checked = false;
                CB_대금탐색.Checked = false;
                CB_매매그룹.Checked = false;
                CB_기능설정.Checked = false;

                tab_잔고.SelectedIndex = 0;
                if (tab_체결.TabCount > 1) tab_체결.SelectedIndex = 1;
            });

            if (CB_종목비공개.Checked)
            {
                종목감추기_잔고.Show();
                Form_Outstanding.form.종목감추기_미체결.Show();
                Form_Conclusion.form.종목감추기_체결.Show();
                종목감추기_로그.Show();
                Form_JuMun.form.종목감추기_주문.Show();

                종목감추기_잔고.BringToFront();
                Form_Outstanding.form.종목감추기_미체결.BringToFront();
                Form_Conclusion.form.종목감추기_체결.BringToFront();
                종목감추기_로그.BringToFront();
                Form_JuMun.form.종목감추기_주문.BringToFront();
            }

            if (CB_계좌비공개.Checked)
            {
                계좌감추기1.Show();
                label_Search.Show();
                CBB_SearchCondition.Show();
                P_Search_List.Show();

                계좌감추기1.BringToFront();
                label_Search.BringToFront();
                CBB_SearchCondition.BringToFront();
                P_Search_List.BringToFront();

                if (CB_종목비공개.Checked)
                {
                    종목감추기7.Show();
                    종목감추기7.BringToFront();
                }
            }

            동작_Log(" ");
            동작_Log(" ");
            동작_Log("MoneyGame 을 종료 합니다. ");

            재시작 = false;

            RB_sell_stop.Checked = true;
            RB_buy_stop.Checked = true;

            try
            {
                foreach (var code in stockBalanceList.ToList())
                {
                    Stockbalance 잔고 = code.Value;
                    잔고.매매가능 = false;
                }

                if (CB_미체결취소.Checked && 시장시작 <= timenow && timenow <= 시장종료)
                {
                    미체결일괄취소();

                    동작_Log("");
                    동작_Log("[미체결 일괄취소] 미체결 주문을 일괄 취소 하였습니다.");
                    동작_Log("");

                    알림창("[ 미체결 일괄취소 ]\n\n미체결 주문을 일괄 취소 하였습니다.", 10, false);

                    while (true)
                    {
                        서버주문전달();

                        int num = 0;
                        for (int i = 0; i < Order_list.Count; i++)
                        {
                            if (Order_list[i].주문유형.Equals(3) || Order_list[i].주문유형.Equals(4))
                            {
                                num++;
                            }
                        }
                        Console.WriteLine("취소대기: " + num + "EA 남았습니다.");

                        if (JumunItem_List.Count == 0)
                        {
                            if (Form_Outstanding.form.Outstanding_DataGridView.Rows.Count == 0)
                            {
                                break;
                            }
                            else
                            {
                                for (int i = 0; i < Form_Outstanding.form.Outstanding_DataGridView.RowCount; i++)
                                {
                                    if (Form_Outstanding.form.Outstanding_DataGridView["검색식_미체결", i].Value.ToString().Contains("삭제"))
                                    {
                                        Form_Outstanding.form.Outstanding_DataGridView.Rows.Remove(Form_Outstanding.form.Outstanding_DataGridView.Rows[i]);
                                    }
                                }
                                Form_Outstanding.form.Outstanding_DataGridView.CurrentCell = null;
                            }
                        }

                        Delay(220);
                    }
                }

                try
                {
                    if (combo_watch_condition_A.Text.Trim().Length > 0) Tab_Watch.test_save_("BT_test_save_A");
                    if (combo_watch_condition_B.Text.Trim().Length > 0) Tab_Watch.test_save_("BT_test_save_B");
                    if (combo_watch_condition_C.Text.Trim().Length > 0) Tab_Watch.test_save_("BT_test_save_C");
                    if (combo_watch_condition_D.Text.Trim().Length > 0) Tab_Watch.test_save_("BT_test_save_D");
                }
                catch
                {
                    Error_Log("[에러 확인] watchsave_ 에러 ");
                }

                Console.WriteLine("종료합니다");
                동작_Log("종료합니다.");

                for (int x = 3; x > 0; x--)
                {
                    if (x == 2)
                    {
                        DataManagement.save_jango(); // 종료전 잔고 저장
                        DataManagement.SAVE_주문리스트();
                        DataManagement.최종매입가_저장();
                        DataManagement.리밸_감시_List_기록();

                        if (!DateTime.Now.DayOfWeek.Equals(DayOfWeek.Saturday) && !DateTime.Now.DayOfWeek.Equals(DayOfWeek.Sunday))
                        {
                            if (!공휴일) DataManagement.DataBackUp(Application.StartupPath);
                        }
                    }

                    Console.WriteLine("종료 - " + x);
                    동작_Log("종료 - " + x);
                    Delay(1000);
                }

                if (!USER_ID.Equals("***"))
                    TelegramMessenger.Telegram_alram("logOut_user");

                F_close();

                if (HI지니_64시작)
                {
                    System.Diagnostics.Process ps = new System.Diagnostics.Process();
                    ps.StartInfo.FileName = "HI지니_64.exe";
                    ps.StartInfo.WorkingDirectory = "C:\\지니_64_Autostock";
                    ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

                    //프로그램 시작
                    try
                    {
                        ps.Start();
                    }
                    catch
                    {
                        Message_Alram("지니_64 재시작 할 수 없습니다. HI지니_64 가 정상적으로 시작 하지 못했습니다", "재시작오류");
                    }
                }

                Process.GetCurrentProcess().Kill();
            }
            catch
            {
                Error_Log("[에러 확인] FormClosing 에러 ");
            }
        }


        private void F_close()
        {
            Form_run = false;
            jostRequestTrDataManager.Stop();
            condotionManager.Stop();
            writing_Manager.Stop();

            condition_data_Manager.Stop();
            Real_data_Manager.Stop();
            Real_price_Manager.Stop();
            Real_Hoga_Manager.Stop();

            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                //폼 종료시 현재 폼의 위치를 저장
                Properties.Settings.Default.Location = this.Location;

                if (account_comboBox.SelectedIndex > -1 && 로딩완료)
                {
                    if (Acc[0].실현손익 > 0)
                    {
                        if (Acc[0].추정자산 > Properties.Settings.Default.MT_principal)
                        {
                            if (Properties.Settings.Default.Today_매수기준금.Contains(today))
                            {
                                Properties.Settings.Default.매수계산기준금 = long.Parse(Properties.Settings.Default.Today_매수기준금.Split('@')[0]) + Acc[0].실현손익;
                            }
                            else
                            {
                                Properties.Settings.Default.Today_매수기준금 = Properties.Settings.Default.매수계산기준금 + "@" + today;
                                Properties.Settings.Default.매수계산기준금 = Properties.Settings.Default.매수계산기준금 + Acc[0].실현손익;
                            }

                            if (Properties.Settings.Default.Today_손익기준금.Contains(today))
                            {
                                Properties.Settings.Default.손익계산기준금 = long.Parse(Properties.Settings.Default.Today_손익기준금.Split('@')[0]) + Acc[0].실현손익;
                            }
                            else
                            {
                                Properties.Settings.Default.Today_손익기준금 = Properties.Settings.Default.손익계산기준금 + "@" + today;
                                Properties.Settings.Default.손익계산기준금 = Properties.Settings.Default.손익계산기준금 + Acc[0].실현손익;
                            }
                        }
                    }

                    Set_default.FormClose_Save();
                    Condition_Management.Condition_save();
                }
                Properties.Settings.Default.주문_tap = tab_주문.SelectedIndex;
                Properties.Settings.Default.Autologin_CheckBox = Form1.form1.Autologin_CheckBox.Checked;
                Properties.Settings.Default.account_comboBox = Form1.form1.account_comboBox.Text;
                Properties.Settings.Default.Save();
            });
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            //  아이콘을 더블클릭하면 폼 화면을 보여줌
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
            }
            this.Activate();
        }


        private void contextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.Trim().Equals("최대화"))
            {
                비프음("체크");
                this.Show();
                this.WindowState = FormWindowState.Maximized;
            }
            if (e.ClickedItem.Text.Trim().Equals("보이기"))
            {
                비프음("체크");
                this.Show();
            }
            if (e.ClickedItem.Text.Trim().Equals("감추기"))
            {
                비프음("체크");
                this.Hide();
            }
            if (e.ClickedItem.Text.Trim().Equals("재시작"))
            {
                this.ActiveControl = null;
                MBC_sender = "BT_재시작";
                중요메세지("재시작", "재시작 하시겠습니까 ?");
            }
            if (e.ClickedItem.Text.Trim().Equals("종 료"))
            {
                AutoClosingAlram("지니_64를 종료 합니다.", "종료", 5, "에러");
                Application.Exit();
            }
        }

        private void tabControl_L_Top_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font fntTab;
            Brush bshBack;
            Brush bshFore;

            TabControl Tab = sender as TabControl;

            if (e.Index == Tab.SelectedIndex)
            {
                fntTab = new Font(e.Font, FontStyle.Bold);
                bshBack = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, SystemColors.ControlDark, SystemColors.ControlDark, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
                bshFore = Brushes.DarkRed;
            }
            else
            {
                fntTab = e.Font;
                bshBack = new SolidBrush(SystemColors.Control);
                bshFore = new SolidBrush(Color.Black);
            }

            if (e.Index < Tab.TabCount)
            {
                string tabName = Tab.TabPages[e.Index].Text;
                StringFormat sftTab = new StringFormat(StringFormatFlags.NoClip);
                sftTab.Alignment = StringAlignment.Center;
                sftTab.LineAlignment = StringAlignment.Center;

                e.Graphics.FillRectangle(bshBack, e.Bounds);

                Rectangle recTab = e.Bounds;

#if true    // <--- 여기를 true 로 변경하면 텍스트의 좌우를 뒤집지 않음
                recTab = new Rectangle(recTab.X, recTab.Y + 4, recTab.Width, recTab.Height - 4);
                e.Graphics.DrawString(tabName, fntTab, bshFore, recTab, sftTab);
#else
             recTab = new Rectangle(0, 0, recTab.Width, recTab.Height);
             Bitmap bitmap = new Bitmap(e.Bounds.Width, e.Bounds.Height);
             Graphics g = Graphics.FromImage(bitmap);
             g.Clear(BackColor);        // <--- 여기에 원하는 배경색상을 지정
             g.DrawString(tabName, fntTab, bshFore, recTab, sftTab);
             bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
             e.Graphics.DrawImage(bitmap, e.Bounds.X + 1, e.Bounds.Y);   // +1 안해주면 왼쪽에서 짤림
             g.Dispose();
             bitmap.Dispose();
#endif
            }
        }

        private void CB_미니시계_CheckedChanged(object sender, EventArgs e)
        {
            LayoutChange.CB_미니시계_CheckedChanged(sender);
        }

        private void CB_종목비공개_CheckedChanged(object sender, EventArgs e)
        {
            LayoutChange.CB_종목비공개_CheckedChanged(sender);
        }

        private void CB_계좌비공개_CheckedChanged(object sender, EventArgs e)
        {
            LayoutChange.CB_계좌비공개_CheckedChanged(sender);
        }

        public void Form_Top_Most()
        {
            if (FormBasic_Open) Form_Basic.form.TopMost = true;
            if (FormRepeat_Open) Form_Repeat.form.TopMost = true;
            if (FormAccountManagement_Open) Form_AccountManagement.form.TopMost = true;
            if (FormSpecial_Open) Form_AccountManagement.form.TopMost = true;
            if (FormPriceSearch_Open) Form_PriceSearch.form.TopMost = true;
            if (FormTradeGroup_Open) Form_TradeGroup.form.TopMost = true;
            if (FormFunction_Open) Form_Function.form.TopMost = true;
        }

        private void CBB_RunCondition_DropDown(object sender, EventArgs e)
        {
            Tab_Watch.combo_Watch_DropDown(sender);
        }

        private void CBB_RunCondition_DropDownClosed(object sender, EventArgs e)
        {
            if (로딩완료) 비프음("체크");
            if (CBB_SearchCondition.SelectedIndex > -1 && !Properties.Settings.Default.CBB_SearchCondition.Equals(CBB_SearchCondition.Text))
            {
                Form1.form1.SearchView_List.Clear();
                Search_List.Items.Clear();
                Search_List.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " | 지니_64오토스탁");
            }
        }

        private void CBB_SearchCondition_TextChanged(object sender, EventArgs e)
        {
            if (CBB_SearchCondition.SelectedIndex == -1)
            {
                CBB_SearchCondition.SelectedItem = Properties.Settings.Default.CBB_SearchCondition;
            }
            else
            {
                Properties.Settings.Default.CBB_SearchCondition = CBB_SearchCondition.Text;
            }
        }

        private void tabControl_L_Top_SelectedIndexChanged(object sender, EventArgs e)
        {
            CB_기본매매.Checked = false;
            CB_반복매매.Checked = false;
            CB_계좌관리.Checked = false;
            CB_특수매매.Checked = false;
            CB_대금탐색.Checked = false;
            CB_매매그룹.Checked = false;
            CB_기능설정.Checked = false;

            if (tab_잔고.SelectedIndex == 0)
            {
                tab_잔고.TabPages[0].Controls.Add(JanGo_dataGridView);
                JanGo_dataGridView.BringToFront();

                var orderedList = stockBalanceList.Values.OrderByDescending(balance => balance.초기매수일);

                foreach (var balance in orderedList)
                {
                    int newRowIndex = JanGo_dataGridView.Rows.Add();
                    JanGo_dataGridView["코드_잔고A", newRowIndex].Value = balance.종목코드;
                    GridView_Print.JangoRow_print(newRowIndex, balance);
                }
                JanGo_dataGridView.ClearSelection();
            }
            else
            {
                JanGo_dataGridView.Rows.Clear();
                tab_잔고.TabPages[0].Controls.Remove(JanGo_dataGridView);
            }

            if (tab_잔고.SelectedIndex == 2)
            {
                Form1.form1.chart_Month.Series[0].Points.Clear();
                Form1.form1.chart_Month.Series[1].Points.Clear();
                Form1.form1.chart_Day.Series[0].Points.Clear();
                Form1.form1.chart_Day.Series[1].Points.Clear();
                Form1.form1.DGV_통계.Rows.Clear();
                Form1.form1.DGV_통계B.Rows.Clear();

                panel_잔고.Location = new Point(-1, 0);
                panel_잔고.Size = new Size(1922, 1020);
                tab_잔고.Location = new Point(-5, -3);
                tab_잔고.Size = new Size(1930, 1026);

                panel_잔고.BringToFront();
                chart_Month.Show();
                chart_Day.Show();

                chart_Day.Location = new Point(1102, 5);
                chart_Day.Size = new Size(826, 500);
                chart_Month.Location = new Point(1102, 497);
                chart_Month.Size = new Size(826, 500);

                chart_Month.BringToFront();
                chart_Day.BringToFront();

                if (CB_종목비공개.Checked)
                {
                    if (DGV_통계B.Columns[0].HeaderText.Equals("종목명"))
                    {
                        종목감추기6.Show();
                        종목감추기6.BringToFront();
                    }
                }
                else
                {
                    종목감추기6.Hide();
                }

                PN_지수연동.Hide();

                CBB_layout.BringToFront();
                CB_기본매매.BringToFront();
                CB_반복매매.BringToFront();
                CB_계좌관리.BringToFront();
                CB_특수매매.BringToFront();
                CB_대금탐색.BringToFront();
                CB_매매그룹.BringToFront();
                CB_기능설정.BringToFront();
                CB_미니시계.BringToFront();
                CB_계좌비공개.BringToFront();
                CB_종목비공개.BringToFront();
            }
            else
            {
                LayoutChange.CBB_layout_SelectedIndex(CBB_layout.SelectedIndex);

                PN_지수연동.Show();
                PN_지수연동.BringToFront();
                chart_Month.Hide();
                chart_Day.Hide();
                chart_Month.SendToBack();
                chart_Day.SendToBack();
            }
        }


        private void combo_new_condition_A_MouseHover(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            toolTip1.SetToolTip(combo, combo.Text);
        }

        private void combo_watch_condition_A_MouseHover(object sender, EventArgs e)
        {
            string Tip1 = "\n#신규매수 검색식 중 AND 검색식 일 경우\n  진입가 오차가 발생합니다.\n#검색식 이름에 <  >  ?  [  ]  : | * 등의\n  문자가 들어 있으면 저장되지 않습니다.";
            string Tip2 = "\n추가 검색식 :: 선택한뒤 조건식 선택에서 확인할수 있습니다.\n조건식 변경후 'Watch 저장' 버튼을 클릭 하면 적용 됩니다.";

            toolTip1.SetToolTip(combo_watch_condition_A, "검색식 : " + combo_watch_condition_A.Text + Tip1);
            toolTip1.SetToolTip(combo_watch_condition_B, "검색식 : " + combo_watch_condition_B.Text + Tip1);
            toolTip1.SetToolTip(combo_watch_condition_C, "검색식 : " + combo_watch_condition_C.Text + Tip1);
            toolTip1.SetToolTip(combo_watch_condition_D, "검색식 : " + combo_watch_condition_D.Text + Tip1);
            toolTip1.SetToolTip(combo_watch_condition_AA, "검색식 : " + combo_watch_condition_AA.Text + Tip2);
            toolTip1.SetToolTip(combo_watch_condition_BB, "검색식 : " + combo_watch_condition_BB.Text + Tip2);
            toolTip1.SetToolTip(combo_watch_condition_CC, "검색식 : " + combo_watch_condition_CC.Text + Tip2);
            toolTip1.SetToolTip(combo_watch_condition_DD, "검색식 : " + combo_watch_condition_DD.Text + Tip2);
        }

        private void CBB_layout_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayoutChange.CBB_layout_SelectedIndexChanged(sender);
        }

        private void CB_기능설정_CheckedChanged(object sender, EventArgs e)
        {
            LayoutChange.CB_기능설정_CheckedChanged(sender);
        }




        private void CBB_최종가종목_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView_Print.최종매입가view();
        }

        private void BT_종목업_Click(object sender, EventArgs e)
        {
            if (CBB_최종가종목.SelectedIndex < CBB_최종가종목.Items.Count - 1) CBB_최종가종목.SelectedIndex = CBB_최종가종목.SelectedIndex + 1;
        }

        private void BT_종목다운_Click(object sender, EventArgs e)
        {
            if (CBB_최종가종목.SelectedIndex > 0) CBB_최종가종목.SelectedIndex = CBB_최종가종목.SelectedIndex - 1;
        }

        private void 최종매입가View_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            최종매입가View.ClearSelection();
        }

        private void 최종매입가View_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            최종매입가View.ClearSelection();
        }

        private void CBB_jumun_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrint.CBB_jumun_SelectedIndex(sender);
        }

        private void CB_Jisu_avgset_CheckedChanged(object sender, EventArgs e)
        {
            LayoutChange.CB_기능설정_CheckedChanged(sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form1.TR_예수금요청_13();
          
            
            //JanGo_dataGridView.Invalidate();
            //JanGo_dataGridView.Refresh();

            //JanGo_dataGridView.Rows.Clear();
            //var newList = from pair in stockBalanceList orderby pair.Value.초기매수일 ascending select pair;
            //foreach (var code in newList.ToList())
            //{
            //    Stockbalance 잔고 = code.Value;
            //    JanGo_dataGridView.Rows.Insert(0);
            //    JanGo_dataGridView["코드_잔고A", 0].Value = 잔고.종목코드;
            //    GridView_Print.JangoRow_print(0, 잔고);
            //}

            //JanGo_dataGridView.ClearSelection();

            //Console.WriteLine("잔고 업데이트 ---------------------------------");
        }

        private void checkBox_key_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_key.Checked)
            {
                label_키움ID.BringToFront();
                label_appkey.BringToFront();
                label_appsecret.BringToFront();
                textBox_키움ID.BringToFront();
                textBox_appkey.BringToFront();
                textBox_appsecret.BringToFront();
                button_save_key.BringToFront();
                checkBox_mockapi.BringToFront();
            }
            else
            {
                label_키움ID.SendToBack();
                label_appkey.SendToBack();
                label_appsecret.SendToBack();
                textBox_키움ID.SendToBack();
                textBox_appkey.SendToBack();
                textBox_appsecret.SendToBack();
                button_save_key.SendToBack();
                checkBox_mockapi.SendToBack();
            }
        }
    }

}






















