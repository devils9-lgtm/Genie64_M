using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using 지니64.box;
using 지니64.Timer;
using static 지니64.Form1;

// 주문제한 시간당 3600 번 

namespace 지니64
{
    public partial class Form1 : Form
    {
        public static TR_Scheduler tr_scheduler = new TR_Scheduler();
        public static Order_Scheduler order_scheduler = new Order_Scheduler();

        // 처음에는 null로 비워두어 스레드가 백그라운드에서 돌지 않도록 합니다.
        public static 한투_TR스케줄러 한투_스케줄러 = null;
        public static LS_TR스케줄러 LS_스케줄러 = null;

        // 새롭게 추가되는 한투/LS 전용 주문 스케줄러 (null로 초기화)
        public static 한투_주문스케줄러 한투_주문스케줄러 = null;
        public static LS_주문스케줄러 LS_주문스케줄러 = null;

        public static string serverIp = "";

        public static string 프로그램명 = GET.이름_자동설정();
        public static string 버전_디버그 = "1.0.55";
        public static string 버전_서버 = "0.0.0";
        public static bool 릴리즈 = true;
        public static bool 텔레그램TEST = false;
        readonly string ProjectName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
        readonly string ProjectVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        string configPath = "";
        string configPathAndName = "";

        public static bool ON_LINE = false;
        public static string API_token = "";
        public static string 한투_API_token = "";
        public static string 한투_WS_approval_key = "";
        public static string LS_API_token = "";

        public static string 한투_앱키 = "";
        public static string 한투_시크릿키 = "";
        public static string 한투_CANO = "";
        public static string 한투_ACNT_PRDT_CD = "";

        public static string LS_앱키 = "";
        public static string LS_시크릿키 = "";
        public static string LS_AcntNo = "";
        public static string LS_InptPwd = "";

        public static bool 중복접속 = false;

        public static WebSocketClient websocketClient;
        public static bool 키움_TR유량제한 = false;
        public static bool 한투_TR유량제한 = false;
        public static bool LS_TR유량제한 = false;
        public static bool 키움_주문유량제한 = false;
        public static bool 한투_주문유량제한 = false;
        public static bool LS_주문유량제한 = false;

        public static string 미수금정리 = "대기";
        public static string 매매시작 = "";
        public static bool 내아이디 = false;
        public static bool VIP_mode = false;
        public static int 차트로딩_남은수 = 0;

        public static Uri uri;

        public static ClientWebSocket websocket;
        public static bool connected;
        public static bool 프로그램종료중 = false;

        public static string startupPath = Application.StartupPath; //시작할 프로그램

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool Beep(int n, int m);

        // [지니 최적화] 스레드 수 계산 (동시 접근 예상 수)
        // 보통 CPU 코어 수 * 4 정도로 잡으면 락 걸림 없이 가장 빠릅니다.
        private static readonly int concurrencyLevel = Environment.ProcessorCount * 4;
        public static ConcurrentDictionary<string, Stockbalance> stockBalanceList = new ConcurrentDictionary<string, Stockbalance>(concurrencyLevel, 100);
        public static ConcurrentDictionary<string, Market_Item> Market_Item_List = new ConcurrentDictionary<string, Market_Item>(concurrencyLevel, 4000);
        public static ConcurrentDictionary<string, MAPeriod> Min_ma_list = new ConcurrentDictionary<string, MAPeriod>(concurrencyLevel, 100);
        public static ConcurrentDictionary<string, MAPeriod> Day_ma_list = new ConcurrentDictionary<string, MAPeriod>(concurrencyLevel, 100);
        public static ConcurrentDictionary<string, Watch> Watch_List = new ConcurrentDictionary<string, Watch>();                          // Watch 검색 종목 상태

        public List<string> 실시간요청코드_List = new List<string>(100);                                         // 실시간시세 요청한 종목코드 리스트 키움에서 100개 까지 제한  리스트에 없는 코드만 실시간 감시요청

        public static int Run_condition_count = 0;                                                                     // 실시간구동중인 검색식 count  10까지 가능
        public static List<Condition> ConditionList = new List<Condition>();                                         // 조건식 리스트
        public static List<RunCondition> Run_condition_List = new List<RunCondition>();                                //구동중인 실시간검색식 리스트
        public static ConcurrentDictionary<string, 위치별검색식> 위치별검색식리스트 = new ConcurrentDictionary<string, 위치별검색식>();                   //위치별 사용검색식

        public ConcurrentDictionary<string, HashSet<string>> Condition_Catch_Map = new ConcurrentDictionary<string, HashSet<string>>();
        public HashSet<string> Interest_condition_List = new HashSet<string>();                                              // 조건식 - 관심그룹 실시간 조건식 

        public static List<Newstock> NewStock_List = new List<Newstock>();                                        // 신규매수 검색 종목리스트
        public HashSet<string> NewBuyItem_List = new HashSet<string>();                                                      // 신규주문 종목리스트
        public List<ABC> ABC_List = new List<ABC>();                                                             // AnB 검색 종목리스트

        public static ConcurrentDictionary<string, string> 잔고보정Dict = new ConcurrentDictionary<string, string>(); 
        public static bool 잔고보정요청 = true;


        public static ConcurrentDictionary<string, List<최종매입가>> 최종매입가_List = new ConcurrentDictionary<string, List<최종매입가>>();                                // 잔고 - 최종매입가 

        public static List<재매수> Rebuystock_List = new List<재매수>();                                                       // 잔고 - 전량매도 + 보유잔고리스트

        public HashSet<string> Telegram_users = new HashSet<string>();                                                       // telegram users_ID 

        //---------------------------------------------------------------------------------//
        public static ConcurrentDictionary<string, Catch_stock> Catch_Stock_List = new ConcurrentDictionary<string, Catch_stock>();   //위치별 검색식에 검색된 종목들

        public List<Rebal_Sell> Rebal_Sell_List = new List<Rebal_Sell>();                                               // 리밸런싱 등록 리스트
        public List<Scalping> Scalping_List = new List<Scalping>();                                                     // 스켈핑 등록 리스트

        public static ConcurrentQueue<Trading_item> Trading_Pool = new ConcurrentQueue<Trading_item>();  // 매매중인 종목들을 담아두는곳  매매중복 방지요
        public static ConcurrentDictionary<Trading_item, byte> Active_List = new ConcurrentDictionary<Trading_item, byte>();

        public static ConcurrentDictionary<string, byte> 체결주문번호_List = new ConcurrentDictionary<string, byte>();
        public static ConcurrentDictionary<string, JumunItem> JumunItem_List = new ConcurrentDictionary<string, JumunItem>();      // 주문 - 주문 리스트 

        public static ConcurrentDictionary<string, 감시주문> 감시주문_List = new ConcurrentDictionary<string, 감시주문>();   // 감시주문 - 감시주문 리스트 

        public List<주문예약> 주문예약_List = new List<주문예약>();                                                       // 주문 - 주문예약 
        public static List<체결> 체결기록list = new List<체결>();                                                                      // 주문 - 체결기록  리스트 
        public ConcurrentQueue<주문예약> 예약주문_List = new ConcurrentQueue<주문예약>();                                 // 주문 - 예약주문 현재가 대기 받아오기 

        public static List<Interest_stock> Interest_stock_List = new List<Interest_stock>();                            // 관심종목 리스트   
        public static HashSet<string> Interest_Title_List = new HashSet<string>();                                            // 관심종목 그룹 리스트   
        public HashSet<string> 검색결과_List = new HashSet<string>();                                                          // 검색결과 리스트   
        public static HashSet<string> Interest_AutoAdd_List = new HashSet<string>();                                          // 관심종목 자동등록 리스트   

        public static List<재주문> 재주문_LIST = new List<재주문>();
        public static ConcurrentDictionary<string, 검색이탈> 검색이탈_Dic = new ConcurrentDictionary<string, 검색이탈>();// 재주문
        public static HashSet<string> NewBuyWrite_List = new HashSet<string>();                                                             // 신규매수기록
        public List<TradeLog> 매매내역_List = new List<TradeLog>(2000); // 넉넉하게
        public List<TradeLog> 기준일매매내역_List = new List<TradeLog>(2000);

        public static ConcurrentDictionary<string, 신규조회> 신규조회_List = new ConcurrentDictionary<string, 신규조회>();

        public HashSet<string> SearchView_List = new HashSet<string>();
        public HashSet<string> 신규거부로그_List = new HashSet<string>(); // 거부_List 프로그램실행시 1회만 메세지 출력되고 저장됨
        public HashSet<string> 추매거부로그_List = new HashSet<string>();
        public HashSet<string> 매도거부로그_List = new HashSet<string>();

        public static AVG_price[] kospi_avg_day = new AVG_price[60];
        public static AVG_price[] kosdaq_avg_day = new AVG_price[60];

        // 현재 몇 개까지 채워졌는지 확인용 (초기 로딩 시 필요)
        public static int kospi_day_count = 0;
        public static int kosdaq_day_count = 0;

        public static AVG_price[] kospi_avg_min = new AVG_price[60];
        public static AVG_price[] kosdaq_avg_min = new AVG_price[60];

        // 현재 몇 개까지 채워졌는지 확인용 (초기 로딩 시 필요)
        public static int kospi_min_count = 0;
        public static int kosdaq_min_count = 0;

        public static List<지수이평추세> 지수이평추세 = new List<지수이평추세>();                      
        public static Dictionary<string, 지수이평사용값[]> 그룹별_지수사용값 = new Dictionary<string, 지수이평사용값[]>();

        public static HashSet<string> NXT_list = new HashSet<string>(800);

        /////////////////////      코스피 & 코스닥 마켓로깅     /////////////////////
        public AutoCompleteStringCollection collection = new AutoCompleteStringCollection();


        public static SemaphoreSlim _semaphore = new SemaphoreSlim(50);


        public static bool kos_avg_update = true;

        public static readonly object Jumun_screen_ScreenLock = new object();
        public static bool Jumun_screen_isInitialized = false;
        public static readonly object OrderLock = new object();
        public static bool _isOrderInitialized = false;

        public static int 스크린Num = 1100; // 화면번호 총 300 개까지 가능
        public static int Jumun_screen = 1000; // 1000 ~ 1299 까지 가능
        public static int Order번호_ = 0;

        public static string 사용기간 = "";

        public static double TAX = 0.2 / 100;
        public static double 수수료 = 0;

        public static string server = "서버연결안됨";
        public static string server_알림 = "로딩중";
        public static bool NXT_server = false;
        public static string 접속_ID = "***";
        public string telegram_ChatID = "6194572746";
        public string Telegram_Token = "6068795633:AAFlxx-LruM9AXyxq5rBNnEgMdKk8o1pm2g"; // @JinE_bot

        public static bool 장전동시 = true;
        public static DateTime 서버_마지막_통신시간 = DateTime.Now;
        public static bool 서버연결끊김_기록됨;
        public static string 단절원인;
        
        public static string 재접속 = "on";
        public static bool 로딩완료 = false;
        public static bool 호가요청 = true;
        public static bool 지니64n종료 = true;
        public static bool HI지니64시작 = false;

        public static bool OcamRun = true;
        public static bool RecordON = true;
        public static bool RecordOff = true;
        public static bool 음소거 = true;
        public bool 동작실시간 = true;
        public bool 수익금or수익률 = true;
        public static bool 신규매수정지 = true;
        public static bool 추가매수정지 = true;
        public static bool CB개장일 = false;
        public static bool CB수능일 = false;
        public static bool 예약주문_장전 = true;
        public static bool 예약주문_종가 = true;

        public static bool 매매기간_오전 = true;
        public static bool 매매기간_오후 = true;

        public bool CBscalping = false;

        public static bool FormJisu_Open = false;
        public static bool FormJisu_print_Open = false;
        public static bool FormBasic_Open = false;
        public static bool FormRepeat_Open = false;
        public static bool FormAccountManagement_Open = false;
        public static bool FormSpecial_Open = false;
        public static bool FormPriceSearch_Open = false;
        public static bool FormTradeGroup_Open = false;
        public static bool FormFunction_Open = false;

        public static bool 메인마켓_주문취소 = true;

        public static bool 그룹지정_A = true;
        public static bool 그룹지정_B = true;
        public static bool 그룹지정_C = true;
        public static bool 그룹지정_D = true;

        public bool 오전감시 = true;
        public bool 오후감시 = true;

        public bool Cut_A = false;
        public bool Cut_B = false;
        public bool Cut_C = false;

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

        public static bool Form_close = false;
        public bool 미수정리_미수알림 = true;

        public static bool 수량알림 = true;
        public static bool 금액알림 = true;
        public static bool 공휴일 = false;
        public static bool Jine_Run = true;
        public static bool Jine_Stop = true;

        public static bool 재시작 = false;

        public static bool 시장가탐색 = false;
        public bool 동작상태보기 = false;
        public static bool 매수증거금 = true;

        public static bool 순매수조회 = true;
        public static bool 신규_지수업종연동통과_피 = false;
        public static bool 추가_지수업종연동통과_피 = false;
        public static bool 신규_지수업종연동통과_닥 = false;
        public static bool 추가_지수업종연동통과_닥 = false;

        public static bool 잔고시간_ON = false;

        public bool 기준값변경 = true;
        public static bool 주문큐_작동중 = false;
        public static bool 예수금요청 = false;
        public static bool 예수금요청_ON = false;
        public static bool 주문내역요청 = false;
        public static bool 주문내역요청_ON = false;
        public static bool 주문내역동기화 = false;

        public static bool 신규매수신호_A = true;
        public static bool 신규매수신호_B = true;
        public static bool 신규매수신호_C = true;

        public bool 통계수익률 = false;

        public Telegram.Bot.TelegramBotClient Telegram_Bot;
        public box.MBC MBC = new box.MBC();

        public box.Form_Outstanding out_Form = new box.Form_Outstanding();
        public box.Form_JuMun jumun_Form = new box.Form_JuMun();
        public box.Form_Conclusion conclusion_Form = new box.Form_Conclusion();
        public box.Form_Jisu Jisu_Form = new box.Form_Jisu();
        public box.Form_Jisu_print Jisu_print_Form = new box.Form_Jisu_print();

        public Form_Basic Form_Basic;
        public Form_Repeat Form_Repeat;
        public Form_AccountManagement Form_AccountManagement;
        public Form_Special Form_Special;
        public Form_PriceSearch Form_PriceSearch;
        public Form_TradeGroup Form_TradeGroup;
        public Form_Function Form_Function;

        public int Box1_Closetime = 10;
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

        public static int tab_잔고_index = 0;

        public static 초기화.숫자 Get = 초기화.숫자.LoadFromSettings();
        public static 초기화.문자 str = 초기화.문자.LoadFromSettings();
        public static 초기화.Account Acc = 초기화.Account.LoadFromSettings();
        public static 초기화.검색식 식 = 초기화.검색식.LoadFromSettings();
        public static 초기화.CanTrade CanTrade;

        public bool 매수_ON = false;
        public bool 매도_ON = false;

        public static bool 주문정리_완료 = true;

        // --------------------------------------------------------------------------------
        // [Form1.cs] 전역 구조체 및 백업 명부 정의
        // --------------------------------------------------------------------------------
        public struct 컨트롤_원본상태
        {
            public Point 좌표;
            public Control 부모;
            public Size 원본크기;
            public Font 원본폰트;
            public System.Drawing.ContentAlignment 라벨정렬;         // Label 정렬 백업용 추가
            public HorizontalAlignment 텍스트박스정렬;  // TextBox 정렬 백업용 추가
        }

        public static Dictionary<Control, 컨트롤_원본상태> 원본상태_사전 = new Dictionary<Control, 컨트롤_원본상태>();
        public static Dictionary<string, int> 잔고그리드_원본열순서 = new Dictionary<string, int>();
        public static Dictionary<string, int> 통계그리드_원본열순서 = new Dictionary<string, int>();
        public static Dictionary<string, int> 통계B그리드_원본열순서 = new Dictionary<string, int>();
        // 💡 [핵심 추가] 통계 그리드뷰 열들의 원래 너비(와이드)를 저장할 기억 장치
        public static Dictionary<string, int> 통계그리드_원본열너비 = new Dictionary<string, int>();
        public static Dictionary<string, int> 통계B그리드_원본열너비 = new Dictionary<string, int>();

        private void 초기_컨트롤_상태_백업하기()
        {
            Control[] 컨트롤_명부 = new Control[]
            {
              CB_세로보기, label_ONLINE, label_투자원금, label_추정증가자산, label_D2주문가능, label_매입평가금, label_평가손익, label_실현손익,
              MT_투자원금, TB_추정자산, TB_D2, TB_매입금, TB_평가손익금, TB_실현손익,
              panel_잔고, TB_증가자산, TB_추정D2, TB_평가금, TB_평가손익율, TB_실현손익율, JanGo_dataGridView, LB_Log,
              out_Form.Outstanding_DataGridView, jumun_Form.JuMun_dataGridView, conclusion_Form.Conclusion_DataGridView,DGV_통계,DGV_통계B
            };

            foreach (Control ctrl in 컨트롤_명부)
            {
                if (ctrl != null)
                {
                    // 💡 최초 생성 시점의 정렬 상태까지 안전하게 백업합니다.
                    System.Drawing.ContentAlignment 라벨값 = System.Drawing.ContentAlignment.MiddleLeft;
                    HorizontalAlignment 텍스트값 = HorizontalAlignment.Left;

                    if (ctrl is System.Windows.Forms.Label 라벨)
                    {
                        라벨값 = 라벨.TextAlign;
                    }
                    else if (ctrl is TextBox 텍스트박스)
                    {
                        텍스트값 = 텍스트박스.TextAlign;
                    }

                    원본상태_사전[ctrl] = new 컨트롤_원본상태
                    {
                        좌표 = ctrl.Location,
                        부모 = ctrl.Parent,
                        원본크기 = ctrl.Size,
                        원본폰트 = ctrl.Font,
                        라벨정렬 = 라벨값,
                        텍스트박스정렬 = 텍스트값
                    };
                }
            }

            if (form1.JanGo_dataGridView != null)
            {
                foreach (DataGridViewColumn 열 in form1.JanGo_dataGridView.Columns)
                {
                    잔고그리드_원본열순서[열.Name] = 열.DisplayIndex;
                }
            }
            // 💡 [DGV_통계] 최초 디자인 창의 순서와 셀 너비를 동시에 백업합니다.
            if (form1.DGV_통계 != null)
            {
                foreach (DataGridViewColumn 열 in form1.DGV_통계.Columns)
                {
                    통계그리드_원본열순서[열.Name] = 열.DisplayIndex;
                    통계그리드_원본열너비[열.Name] = 열.Width; // 원래 너비 기억
                }
            }

            // 💡 [DGV_통계B] 최초 디자인 창의 순서와 셀 너비를 동시에 백업합니다.
            if (form1.DGV_통계B != null)
            {
                foreach (DataGridViewColumn 열 in form1.DGV_통계B.Columns)
                {
                    통계B그리드_원본열순서[열.Name] = 열.DisplayIndex;
                    통계B그리드_원본열너비[열.Name] = 열.Width; // 원래 너비 기억
                }
            }
        }
        public Form1()
        {
            form1 = this;

            InitializeComponent();
            Invalidate();

            // 프로그램 시작 시, 이동시킬 모든 컨트롤의 원본 상태를 한 번에 백업합니다.
            초기_컨트롤_상태_백업하기();

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.StartupPath + @"\Data");
            if (!di.Exists) { di.Create(); }

            this.SetStyle(ControlStyles.DoubleBuffer |
                    ControlStyles.UserPaint |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            RB_sell_stop.Checked = true;
            RB_buy_stop.Checked = true;

            // DataGridView 더블버퍼링 설정
            Form_Outstanding.form.Outstanding_DataGridView.SetDoubleBuffered(true);
            JanGo_dataGridView.SetDoubleBuffered(true);
            Form_JuMun.form.JuMun_dataGridView.SetDoubleBuffered(true);
            Form_Conclusion.form.Conclusion_DataGridView.SetDoubleBuffered(true);
            dataGridView_watch_A.SetDoubleBuffered(true);
            dataGridView_watch_B.SetDoubleBuffered(true);
            dataGridView_watch_C.SetDoubleBuffered(true);
            dataGridView_watch_D.SetDoubleBuffered(true);
            DGV_통계.SetDoubleBuffered(true);
            DGV_통계B.SetDoubleBuffered(true);

            // ListBox 더블버퍼링 설정
            LB_검색결과n관심리스트.SetDoubleBuffered(true);
            LB_관심_A.SetDoubleBuffered(true);
            LB_관심_B.SetDoubleBuffered(true);
            LB_관심_C.SetDoubleBuffered(true);
            LB_JumunList.SetDoubleBuffered(true);
            LB_error.SetDoubleBuffered(true);
            LB_Log.SetDoubleBuffered(true);

            // TabControl 더블버퍼링 설정
            tab_잔고.SetDoubleBuffered(true);
            tab_주문.SetDoubleBuffered(true);
            tab_체결.SetDoubleBuffered(true);

            // TextBox 더블버퍼링 설정
            MT_투자원금.SetDoubleBuffered(true);
            TB_추정자산.SetDoubleBuffered(true);
            TB_증가자산.SetDoubleBuffered(true);
            TB_D2.SetDoubleBuffered(true);
            TB_추정D2.SetDoubleBuffered(true);
            TB_매입금.SetDoubleBuffered(true);
            TB_평가금.SetDoubleBuffered(true);
            TB_평가손익금.SetDoubleBuffered(true);
            TB_평가손익율.SetDoubleBuffered(true);
            TB_실현손익.SetDoubleBuffered(true);
            TB_실현손익율.SetDoubleBuffered(true);

            this.Load += new EventHandler(Form1_Load);
            form1.Opacity = 1.0;

            //   this.Shown += new EventHandler(Form1_Shown);
        }

        // --------------------------------------------------------------------------------
        // [Form1.cs] 폼이 화면에 완전히 렌더링을 시도하는 시점 제어
        // --------------------------------------------------------------------------------
        public static void Form1_Shown(object sender, EventArgs e)
        {
            if (GenieConfig.CB_세로보기)
            {
                Sub_form.세로보기(true);
            }
            else
            {
                form1.Opacity = 1.0;
            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            Load_지니_Autostock.이전_데이터_가져오기();

            string 파일경로 = System.IO.Path.Combine(Application.StartupPath, "BotVersion.txt");
            if (System.IO.File.Exists(파일경로))
            {
                try
                {
                    // ★ [핵심 해결] 텍스트 파일의 각 줄(Enter 기준)을 그대로 읽어서 배열 방(0번, 1번...)에 쏙쏙 집어넣습니다.
                    // 기존처럼 \r\n을 찾아서 지우고 쪼개는 복잡한 과정이 필요 없습니다.
                    string[] 데이터 = System.IO.File.ReadAllLines(파일경로);
                    if (데이터.Length >= 7)
                    {
                        GenieConfig.textBox_ID = 데이터[2].Trim();
                        GenieConfig.textBox_계좌번호 = 데이터[3].Trim();

                        GenieConfig.LoadAll_Genie_Config();

                        GenieConfig.textBox_ID = 데이터[2].Trim();
                        GenieConfig.textBox_계좌번호 = 데이터[3].Trim();
                        GenieConfig.textBox_appKey = 데이터[4].Trim();
                        GenieConfig.textBox_appsecret = 데이터[5].Trim();
                        GenieConfig.checkBox_Simulation = (데이터[6].Trim() == "모투");
                    }
                }
                catch { } // 에러 나면 조용히 넘어감
            }

            textBox_키움ID.Text = GenieConfig.textBox_ID;
            textBox_계좌번호.Text = GenieConfig.textBox_계좌번호;

            textBox_appkey.Text = GenieConfig.textBox_appKey;
            textBox_appsecret.Text = GenieConfig.textBox_appsecret;
            checkBox_Simulation.Checked = GenieConfig.checkBox_Simulation;

            // 프로그램 시작 시 스케줄러 가동
            tr_scheduler.Start();
            order_scheduler.Start();
            UnifiedDataManager.Instance.StartAll();

            login.릴리즈체크();
            GenieConfig.아이디체크실행();

            if (Form1.내아이디)
            {
                Form1.Console_print($"[인증 성공] {GenieConfig.textBox_ID} 님, 환영합니다.");
            }
            else
            {
                Form1.Console_print($"[인증 실패] 등록되지 않은 아이디입니다: {GenieConfig.textBox_ID}");
            }

            Form1.CanTrade = 초기화.CanTrade.LoadFromSettings();
            Form1.음소거 = true;

            configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ProjectName, ProjectVersion);
            configPathAndName = Path.Combine(configPath, ProjectName + ".config");

            Setting_backup.GetProject(ProjectName, ProjectVersion, configPath, configPathAndName);

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

            this.Text = 프로그램명 + " " + 버전_디버그 + " / " + GenieConfig.textBox_ID + " / " + str.Week + " / [ " + DateTime.Now.ToLongTimeString() + " ] / " + server + " / " + server_알림 + " / ( ※ 키움서버 주문제한 : 시간당 3600회 입니다.)";

            if (GenieConfig.MT_closetime <= Get.TimeNow) 지니64n종료 = false;
            if (GenieConfig.TB_Record_Run <= Get.TimeNow) OcamRun = false;
            Search_List.Items.Add(Get.TimeNow.ToString("##:##:##") + " | 지니64오토스탁");

            DataManagement.검색식_TEST폴더삭제();

            GET.신규매수방법();
            Set_default.Form_load();

            LayoutChange.모든서브폼_미리생성();

            this.TopMost = GenieConfig.CB_지니64항상위에;
            if (GenieConfig.CB_최대화로실행) this.WindowState = FormWindowState.Maximized;
            if (GenieConfig.CB_지니64크기고정)
            {
                Form1.form1.MaximumSize = new Size(1936, 1054);
                Form1.form1.MinimumSize = new Size(1936, 1054);
                Form1.form1.Size = new Size(1936, 1054);
            }

            if (!GenieConfig.신규횟수날짜.Equals(str.today))
            {
                GenieConfig.신규횟수 = 0;
                GenieConfig.신규횟수날짜 = str.today;
            }

            if (GenieConfig.지수이평 == "지수이평프린터") LayoutChange.CB_기능설정_CheckedChanged("지수");
            tab_주문.SelectedIndex = GenieConfig.주문_tap;
            form1.checkBox_key.Checked = true;

            FormPrint.잔고표시내역();

            CheckHoliday.공휴일계산();
            Guide.GuideLoding();
            LoadFromFile.지수이평설정_파일로딩();
            Market_load.지수이평추세_초기화();
            Condition_Management.검색식_초기화();

            Form1.form1.CBB_layout.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_layout);

            // 5. 검증 완벽 통과 -> 로그인 실행
            Get_token.logIn();

            panel_기능버튼.Enabled = false;
        }


        public async Task 예약주문_RUN()
        {
            bool 성공 = 예약주문_List.TryDequeue(out 주문예약 주문);
            if (성공)
            {
                Market_Item Market = Market_Item_List[주문.종목코드];
                int 현재가 = Method.호가맞추기(Market.현재가, Market.Market);

                if (현재가 > 1)
                {
                    string 상하 = "하한가";

                    if (주문.검색식.Contains("매도"))
                    {
                        상하 = "상한가";
                    }

                    string para = Method.주문가계산(주문.종목코드, 주문.주문비, 현재가, 현재가, 상하);

                    int 주문가 = int.Parse(para.Split('&')[0]);
                    int 주문호가 = int.Parse(para.Split('&')[2]);
                    int 주문수량 = Method.주문수량계산(null, 주문가, 주문.비중, 주문.선택);

                 await    Order_Reserve.예약주문_주문실행(주문, 주문가, 주문수량, 주문호가);
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
            foreach (var 잔고 in stockBalanceList.Values)
            {
                잔고.매매가능 = false;
                잔고.종목상태 = GET.Jango_state(잔고.종목코드);
            }

            if (!server_알림.Equals("공휴일"))
            {
                server_알림 = "장종료";

                // [최적화] 딕셔너리의 KeyValuePair를 순회할 때 한글 변수(주문쌍)를 사용합니다.
                var 처리할_주문키_리스트 = JumunItem_List
                    .Where(주문쌍 => 주문쌍.Value.주문번호.Contains("+"))
                    .Select(주문쌍 => 주문쌍.Key) // Key만 선택
                    .ToList();

                if (처리할_주문키_리스트.Count > 0)
                {
                    // 1. 주문 처리: 키 리스트를 순회하며 Dictionary에서 값을 조회합니다.
                    foreach (string 주문키 in 처리할_주문키_리스트)
                    {
                        // TryGetValue를 사용하여 O(1) 고속 조회 (스레드 안전)
                        if (JumunItem_List.TryGetValue(주문키, out JumunItem 개별주문))
                        {
                            홀딩잔고.주문가능수업데이트(stockBalanceList[개별주문.종목코드], GET.매수매도str(개별주문.매수매도), 개별주문.주문수량, "일괄취소", 개별주문.신용주문);
                            홀딩잔고.예수금업데이트(GET.매수매도str(개별주문.매수매도), 개별주문.주문가격, 개별주문.주문수량, "일괄취소", 개별주문.종목코드, 개별주문.신용주문);

                            // 2. 즉시 제거: 완벽삭제 함수를 거치지 않고 메인 장부에서 다이렉트로 날려버립니다.
                            Form1.JumunItem_List.TryRemove(주문키, out _);

                            Console_print($"[-] 주문번호 '{주문키}' ({개별주문.종목명}) 항목이 메인 장부에서 제거되었습니다.");
                        }
                    }
                }
                Console_print("\n=== ::장 종료 알림:: ===");
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
            string text = CB.Text[4..];
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
            string Text = CB.Text[1..];
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
            {
                Tab_InterestGroup.검색요청(CBB_관심검색식.Text);
            }
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
            재시작 = true;

            form1.RB_sell_stop.Checked = true;
            form1.RB_buy_stop.Checked = true;

             Helper.안전한_UI_업데이트(Form1.form1, () =>
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

            Log.에러기록(" ");
            Log.에러기록(" ");
            Log.에러기록("[" + title + "] " + text);

            Log.동작기록(" ");
            Log.동작기록(" ");
            Log.동작기록("[" + title + "] " + text);

            foreach (var 잔고 in stockBalanceList.Values)
            {
                잔고.매매가능 = false;
            }

            HI지니64시작 = true;
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
            order_scheduler.ClearQueue();

            // 1. [지니 최적화] 취소 대상 키만 빠르게 추출 (이중 순회 및 메모리 낭비 방지)
            var 삭제할_주문키_리스트 = Form1.JumunItem_List
                .Where(주문쌍 => 주문쌍.Value.주문번호.Contains("+"))
                .Select(주문쌍 => 주문쌍.Key)
                .ToList();

            if (삭제할_주문키_리스트.Count > 0)
            {
                // 2. 스냅샷을 순회하며 데이터 업데이트 및 다이렉트 삭제
                foreach (string 삭제할키 in 삭제할_주문키_리스트)
                {
                    if (Form1.JumunItem_List.TryGetValue(삭제할키, out JumunItem 개별주문))
                    {
                        // 잔고 및 예수금 업데이트
                        if (stockBalanceList.TryGetValue(개별주문.종목코드, out var 잔고아이템))
                        {
                            홀딩잔고.주문가능수업데이트(잔고아이템, GET.매수매도str(개별주문.매수매도), 개별주문.주문수량, "일괄취소", 개별주문.신용주문);
                        }
                        홀딩잔고.예수금업데이트(GET.매수매도str(개별주문.매수매도), 개별주문.주문가격, 개별주문.주문수량, "일괄취소", 개별주문.종목코드, 개별주문.신용주문);

                        // 3. 메인 장부에서 즉시 다이렉트 삭제 (보조 장부 폐지에 따른 최적화)
                        Form1.JumunItem_List.TryRemove(삭제할키, out _);
                    }
                }
            }

            // 4. 딕셔너리에 남아있는 모든 항목의 상태 초기화
            foreach (JumunItem 남은주문 in Form1.JumunItem_List.Values)
            {
                남은주문.주문취소 = true;
                남은주문.반복횟수 = 0;
                남은주문.취소시간 = 0;
                남은주문.취소timer = 0;
                남은주문.비고 = "미체결 '취소'";
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

            if (ON_LINE)
            {
                MBC_sender = "Watch_Save";
                중요메세지("Watch저장", "Watch 검색식과 TEST 설정 을 저장 하겠 습니까 ?");
            }
            else
            {
                MBC_sender = " ";
                중요메세지("로그인", "로그인 되지 않았습니다.");
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

        private void CB_condition_CheckedChanged(object sender, EventArgs e) // 체크박스 와 콤보박스 사용 갯수 제한 
        {
            Condition_Management.CB_condition_CheckedChanged(sender);
        }

        private void Combo_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (로딩완료)
                Condition_Management.Combo_condition_SelectedIndexChanged(sender);
        }

        private void Combo_Condition_Add(object sender, EventArgs e)
        {
            Condition_Management.Condition_Add(sender);
        }

        private void Combo_Condition_TextChanged(object sender, EventArgs e)
        {

        }

        private void Combo_Watch_DropDown(object sender, EventArgs e)
        {
            Tab_Watch.Combo_Watch_DropDown(sender);
        }

        private void Combo_watch_DropDownClosed(object sender, EventArgs e)
        {
            if (로딩완료) 비프음("체크");
            Tab_Watch.Combo_watch_DropDownClosed(sender);
        }

        private void Combo_watch_Changed(object sender, EventArgs e)
        {
            Tab_Watch.Combo_watch_Changed(sender);
        }

        ////////////////////  검색식불러오기 10개 제한 하기  ////////////////////
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

                if (sender.Equals(CB_misu)) GenieConfig.CB_misu = false;
                if (sender.Equals(CB_계좌매입비_매수제한)) GenieConfig.CB_계좌매입비_매수제한 = false;
                if (sender.Equals(CB_잔고매입비_추매제한)) GenieConfig.CB_잔고매입비_추매제한 = false;
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

            if (GenieConfig.CB_지수연동범위사용) 순매수조회 = true;
            if (form1.CBB_지수연동_신규.SelectedIndex > 1) 순매수조회 = true;
            if (form1.CBB_지수연동_추매.SelectedIndex > 1) 순매수조회 = true;
        }

        private void Once_CheckedChanged(object sender, EventArgs e)
        {
            체크박스_비프(sender);

            if (sender.Equals(CB_Auto_tradingstart))
            {
                CB_Auto_tradingstart.Text = CB_Auto_tradingstart.Checked ? "자동 시작" : "수동 시작";
            }
            CB_Auto_tradingstart.ForeColor = CB_Auto_tradingstart.Checked ? Color.Red : Color.Black;

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
                DGV.SuspendLayout();
                DGV.Rows.Clear();  
                Method.SortClear(DGV);

                var targetWatches = Watch_List
                    .Where(item => item.Key.Contains(위치))
                    .ToArray();

                for (int i = 0; i < targetWatches.Length; i++)
                {
                    // 딕셔너리 항목 (Key와 Value)을 가져옵니다.
                    var watchItem = targetWatches[i].Value;

                    // Rows.Insert(0)는 여전히 비효율적이지만, 기존 로직 유지를 위해 유지합니다.
                    DGV.Rows.Insert(0);

                    // watchItem(Watch 객체)에서 Code 속성을 사용하여 값을 설정합니다.
                    DGV[23, 0].Value = watchItem.Code;
                }

                DGV.ResumeLayout();
                DGV.Refresh();
            }

        }

        private void TB_추정자산_TextChanged(object sender, EventArgs e)
        {
            long.TryParse(TB_추정자산.Text.Replace(",", ""), out long 추정자산);

            if (GenieConfig.MT_principal < 추정자산)
            {
                (sender as TextBox).ForeColor = Color.Red;
            }
            else if (GenieConfig.MT_principal == 추정자산)
            {
                (sender as TextBox).ForeColor = Color.Black;
            }
            else if (GenieConfig.MT_principal > 추정자산)
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

        private void 양수음수소수_키프레스_(object sender, KeyPressEventArgs e) // 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, true); // textbox 에 양수, 음수 , 소수  숫자만 입력 받을수 있음 
        }

        private void 양수소수_키프레스_(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, false); // textbox 에 양수 , 소수 숫자만 입력 받을수 있음
        }

        private void 양수실수_키프레스_(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, false); // textbox 에 양수 , 실수 숫자만 입력 받을수 있음
        }

        private void 양수음수실수_키프레스_(object sender, KeyPressEventArgs e)// 사용
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

                매도_ON = true;

               Log.동작기록(" ");
               Log.동작기록("지니64 가 '매도' 를 시작 합니다.");
            }
            else
            {
                RB_sell_run.Text = "시작";
                RB_sell_run.ForeColor = Color.Black;
                RB_sell_stop.Text = "매도";

                매도_ON = false;

                if (!Form_close)
                {
                    foreach (JumunItem item in Form1.JumunItem_List.Values)
                    {
                        item.반복횟수 = 0;
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
                매수_ON = true;

               Log.동작기록(" ");
               Log.동작기록("지니64 가 '매수' 를 시작 합니다.");
            }
            else
            {
                RB_buy_run.Text = "시작";
                RB_buy_run.ForeColor = Color.Black;
                RB_buy_stop.Text = "매수";
                매수_ON = false;

               Log.동작기록(" ");
               Log.동작기록("지니64 가 '매수' 를 멈춥니다.");
            }

            CheckeBox_Beep_도미솔(sender);
        }


        ////////////////////////////// 그리드뷰 설정 변화  ////////////////////////////////////


        private void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)             // 원하는 칼럼에 자동 번호 매기기
        {
            if (sender.Equals(JanGo_dataGridView))
            {
                this.JanGo_dataGridView.Rows[e.RowIndex].Cells["NUM_잔고"].Value = JanGo_dataGridView.Rows.Count - (e.RowIndex + 1) + 1;
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
            if (cell_T == null || cell_T.FormattedValue == null)
            {
                return;
            }

            // 값이 있을 때만 안전하게 string으로 변환 후 double 파싱
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


        public void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // [최적화 1] 유효성 검사 (빠른 탈출)
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // [최적화 2] sender 형변환 및 캐싱
            if (!(sender is DataGridView view)) return;

            // [최적화 3] Row 객체 캐싱 (매번 view[col, row]로 접근하면 느림)
            var row = view.Rows[e.RowIndex];

            // ==========================================================================
            // [내부 헬퍼 함수 정의] 이 함수 안에서만 동작하는 전용 일꾼들
            // ==========================================================================

            // [내부 헬퍼 1] 값에 따라 색상 자동 적용 (DGV_color 대체 및 최적화)
            void SetColorByValue(DataGridViewCell cell, bool isRate = false)
            {
                if (cell.Value == null) return;
                string text = cell.FormattedValue.ToString();

                // 0이거나 0.00이면 검정, -면 파랑, 그 외 빨강
                if (text == "0" || text == "0.00" || text == "0%") cell.Style.ForeColor = Color.Black;
                else if (text.Contains("-")) cell.Style.ForeColor = Color.Blue;
                else cell.Style.ForeColor = Color.Red;
            }

            // [내부 헬퍼 2] 감시 그리드(A~D) 공통 처리 로직
            void ProcessWatchGrid(string suffix)
            {
                // 매매 구분
                var cell_구분 = row.Cells["매매" + suffix];
                string text = cell_구분.FormattedValue.ToString();

                if (text == "매수") cell_구분.Style.ForeColor = Color.Green;
                else if (text == "익절") cell_구분.Style.ForeColor = Color.Red;
                else if (text == "손절") cell_구분.Style.ForeColor = Color.Blue;

                row.Cells["매수가" + suffix].Style.ForeColor = cell_구분.Style.ForeColor;

                // 등락율 -> 현재가, 종목명 색상 동기화
                var cell_등락 = row.Cells["등락율" + suffix];
                SetColorByValue(cell_등락);

                Color baseColor = cell_등락.Style.ForeColor;
                row.Cells["현재가" + suffix].Style.ForeColor = baseColor;
                row.Cells["종목명" + suffix].Style.ForeColor = baseColor;

                SetColorByValue(row.Cells["진입후등락" + suffix]);
                SetColorByValue(row.Cells["진입후최고" + suffix]);
                SetColorByValue(row.Cells["진입후최저" + suffix]);
            }

            // [내부 헬퍼 3] 단순 양수/음수 텍스트 색상 처리
            void SetColorSimple(DataGridViewCell cell, Color positiveColor, Color negativeColor)
            {
                string text = cell.FormattedValue.ToString();
                if (text == "0") return; // 0이면 유지

                // 숫자로 변환하지 않고 텍스트로만 빠르게 판단
                if (text.Contains("-")) cell.Style.ForeColor = negativeColor;
                else cell.Style.ForeColor = positiveColor;
            }

            // ==========================================================================
            // [메인 로직] ReferenceEquals로 빠르게 그리드 식별 후 내부 헬퍼 호출
            // ==========================================================================

            // 1. 잔고 그리드
            if (ReferenceEquals(view, JanGo_dataGridView))
            {
                var cell_등락 = row.Cells["등락율_잔고"];
                SetColorByValue(cell_등락);

                Color color = cell_등락.Style.ForeColor;
                row.Cells["현재가_잔고"].Style.ForeColor = color;
                row.Cells["종목명_잔고"].Style.ForeColor = color;

                var cell_수익 = row.Cells["수익률_잔고"];
                SetColorByValue(cell_수익);
                row.Cells["평가손익_잔고"].Style.ForeColor = cell_수익.Style.ForeColor;

                SetColorByValue(row.Cells["최고수익률_잔고"]);
                SetColorByValue(row.Cells["최저수익률_잔고"]);
                SetColorByValue(row.Cells["누적손익_잔고"]);
                SetColorByValue(row.Cells["예상손익_잔고"]);
                SetColorByValue(row.Cells["금일수익금_잔고"]);

                // 컬럼 존재 여부 확인 (필요한 경우만)
                if (view.Columns.Contains("기준%")) SetColorByValue(row.Cells["기준%"]);

                // 검사할 그룹들을 배열로 만들어 둡니다.
                string[] 그룹배열 = { "A", "B", "C", "D", "E", "F", "G" };

                // A부터 G까지 순서대로 돕니다.
                foreach (string 그룹 in 그룹배열)
                {
                    string 차수_컬럼명 = $"차수_{그룹}";
                    string 최종가_컬럼명 = $"최종가_{그룹}";

                    // 💡 [중요] 수익률 컬럼 이름도 A~G에 맞게 동적으로 만들어줍니다.
                    // (만약 실제 컬럼 이름이 "최종수익률_A" 라면 $"최종수익률_{그룹}" 으로 수정해 주세요!)
                    string 수익률_컬럼명 = $"수익률_{그룹}";

                    // 해당 그룹의 컬럼들이 그리드뷰에 존재하는지 확인합니다.
                    if (view.Columns.Contains(차수_컬럼명) && view.Columns.Contains(수익률_컬럼명))
                    {
                        // 1. 해당 그룹의 수익률 셀을 가져옵니다.
                        DataGridViewCell 그룹별_수익률셀 = row.Cells[수익률_컬럼명];

                        // 2. [핵심] 루프 안에서 '이 그룹만의 수익률'을 기준으로 색상을 세팅합니다!
                        SetColorByValue(그룹별_수익률셀);

                        // 3. 세팅된 색상을 해당 그룹의 차수와 최종가 셀에 칠해줍니다.
                        row.Cells[차수_컬럼명].Style.ForeColor = 그룹별_수익률셀.Style.ForeColor;
                        row.Cells[최종가_컬럼명].Style.ForeColor = 그룹별_수익률셀.Style.ForeColor;
                    }
                }


            }
            // 2. 최종매입가 그리드
            else if (ReferenceEquals(view, DGV_최종매입가View))
            {
                // 짝수 인덱스(2,4,6...) 처리
                for (int i = 2; i <= 14; i += 2)
                {
                    var cell = row.Cells[i];
                    SetColorByValue(cell);
                    row.Cells[i - 1].Style.ForeColor = cell.Style.ForeColor;
                }
            }
            // 3. 감시 그리드 (통합) -> 내부 헬퍼 호출로 코드 대폭 감소!
            else if (ReferenceEquals(view, dataGridView_watch_A)) ProcessWatchGrid("_Watch_A");
            else if (ReferenceEquals(view, dataGridView_watch_B)) ProcessWatchGrid("_Watch_B");
            else if (ReferenceEquals(view, dataGridView_watch_C)) ProcessWatchGrid("_Watch_C");
            else if (ReferenceEquals(view, dataGridView_watch_D)) ProcessWatchGrid("_Watch_D");
            // 4. 통계 A
            else if (ReferenceEquals(view, DGV_통계))
            {
                SetColorSimple(row.Cells[0], Color.Red, Color.Black); // 총매수
                SetColorSimple(row.Cells["총매도금액_통계"], Color.Blue, Color.Black);

                var cell_손익 = row.Cells[4];
                SetColorByValue(cell_손익);
                row.Cells[5].Style.ForeColor = cell_손익.Style.ForeColor;

                SetColorSimple(row.Cells["수익횟수_통계"], Color.Red, Color.Black);
                SetColorSimple(row.Cells["손실횟수_통계"], Color.Blue, Color.Black);

                // 수익률 (+ 시작하면 빨강, - 시작하면 파랑)
                var cell_Rate = row.Cells[9];
                string rate = cell_Rate.FormattedValue.ToString();
                if (rate.StartsWith("+")) cell_Rate.Style.ForeColor = Color.Red;
                else if (rate.StartsWith("-")) cell_Rate.Style.ForeColor = Color.Blue;
                else cell_Rate.Style.ForeColor = Color.Black;
            }
            // 5. 통계 B
            else if (ReferenceEquals(view, DGV_통계B))
            {
                var cell_손익 = row.Cells["실현손익_통계B"];
                SetColorByValue(cell_손익);
                row.Cells["실현손익율_통계B"].Style.ForeColor = cell_손익.Style.ForeColor;

                var cell_Status = row.Cells["수익n손실_통계B"];
                cell_Status.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                string status = cell_Status.FormattedValue.ToString();

                if (status == "수익") cell_Status.Style.ForeColor = Color.Red;
                else if (status == "손실") cell_Status.Style.ForeColor = Color.Blue;
                else cell_Status.Style.ForeColor = Color.Black;

                SetColorSimple(row.Cells["매수금액_통계B"], Color.Red, Color.Black);
                SetColorSimple(row.Cells["매도금액_통계B"], Color.Blue, Color.Black);

                // 통계수익률 옵션 처리
                if (통계수익률)
                {
                    SetColorByValue(row.Cells[10]);
                }
                else
                {
                    row.Cells[10].Style.ForeColor = Color.Black;
                }
            }
        }


        private void DGV_통계_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Statistical_chart.통계_CellClick(sender, e);
        }


        private void DGV_통계B_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Form1.form1.CB_세로보기.Checked) return;
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

                            string itemcode = JanGo_dataGridView["코드_잔고", i].Value.ToString();
                            if (stockBalanceList.TryGetValue(itemcode, out Stockbalance 잔고))
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

            DataGridView_JanGo_A_CellMouseClick(sender, e);

        }

        private void JanGo_dataGridView_A_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // 1. 현재 셀이 1번 컬럼인지 확인 (필수)
            if (JanGo_dataGridView.CurrentCell.ColumnIndex != 1)
            {
                return; // 해당 컬럼이 아니면 즉시 종료
            }

            // 2. 편집 컨트롤이 ComboBox인지 확인 (필수)
            // ComboBox combobox = e.Control as ComboBox;

            if (e.Control is ComboBox combobox)
            {
                // 3. 기존 선택 값 저장
                // NOTE: 셀 값이 아닌 콤보박스 컨트롤의 현재 선택된 값을 가져옵니다.
                object value = combobox.SelectedItem ?? this.JanGo_dataGridView.CurrentCell.Value;

                // 4. 이벤트 핸들러 임시 해제 및 항목 초기화 (필수: 무한 루프 방지 및 목록 재설정)
                combobox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
                combobox.Items.Clear();

                // 5. 항목 추가 (항상 같은 목록이라면 조건문 없이 바로 추가)
                // 만약 group 값에 따라 항목이 달라진다면 group 변수를 활용하세요.
                // 현재는 항상 같은 목록을 추가한다고 가정하고 최적화했습니다.
                combobox.Items.AddRange(new object[] { " ", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "X" });

                // 6. 이전 선택 값 복원
                if (value != null)
                {
                    combobox.SelectedItem = value;
                }

                // 7. 이벤트 핸들러 재등록
                combobox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            }
        }



        // 상수 선언 (선택 사항이지만 권장)
        //private const string GroupColumnName = "그룹_잔고A";
        //private const string CodeColumnName = "코드_잔고A";

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = JanGo_dataGridView.CurrentCell.RowIndex;
                int colIndex = JanGo_dataGridView.CurrentCell.ColumnIndex;

                // 1. 현재 셀이 유효한 행/열인지 그리고 1번 컬럼인지 확인
                if (rowIndex < 0 || colIndex != 1)
                {
                    return; // 유효하지 않은 경우 즉시 종료
                }

                // 2. DataGridView에 변경 사항 커밋 (콤보박스의 새 값을 셀 값으로 반영)
                // NOTE: 이 부분이 핵심입니다. CommitEdit을 호출해야 셀 값이 업데이트됩니다.
                this.JanGo_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

                // UpdateCellValue는 CommitEdit 후에 자동으로 반영될 수 있어 생략하거나
                // 필요 시 CellFormatting 등의 이벤트에서 처리하는 것이 일반적입니다.
                // this.JanGo_dataGridView.UpdateCellValue(colIndex, rowIndex); 

                // 3. 셀 및 항목 값 가져오기
                DataGridViewComboBoxCell cbxV = (DataGridViewComboBoxCell)JanGo_dataGridView["그룹_잔고", rowIndex];

                // 현재 셀 값(콤보박스에서 선택된 그룹명: "A", "B" 등)의 인덱스를 찾습니다.
                int newGroupIndex = cbxV.Items.IndexOf(cbxV.Value);
                string itemcode = JanGo_dataGridView["코드_잔고", rowIndex].Value.ToString();

                // 4. 외부 데이터 모델의 기존 그룹 값 확인
                // NOTE: stockBalanceList가 사전에 정의되고 접근 가능한지 확인해야 합니다.
                int currentGroupIndex = stockBalanceList[itemcode].매매그룹;

                // 5. 그룹 값 변경 확인 및 데이터 업데이트 로직
                if (newGroupIndex != currentGroupIndex)
                {
                    // 새 그룹 인덱스로 데이터 모델 업데이트
                    stockBalanceList[itemcode].매매그룹 = newGroupIndex;

                    // 그룹 지정 플래그 설정
                    // NOTE: 이 4개의 플래그 설정이 항상 필요한지 확인하세요.
                    stockBalanceList[itemcode].그룹지정_A = true;
                    stockBalanceList[itemcode].그룹지정_B = true;
                    stockBalanceList[itemcode].그룹지정_C = true;
                    stockBalanceList[itemcode].그룹지정_D = true;

                    // 데이터 저장 및 알림
                    SaveToFile.잔고_파일저장();
                    비프음("체크");
                }
            }
            catch (Exception ex) // 특정 에러 대신 일반 Exception을 잡아 로그를 남길 수 있습니다.
            {
                // 에러 알림: 에러 메시지에 ex.Message 등을 포함하여 디버깅에 도움을 줄 수 있습니다.
                AutoClosingAlram($"[에러 확인] 잔고 그룹변경 중 오류 발생: {ex.Message}", "에러알림", 5, "에러");
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
                    int rowIndex = Form1.form1.JanGo_dataGridView.SelectedCells[0].RowIndex;
                  
                    string 종목명 = Form1.form1.JanGo_dataGridView["종목명_잔고", rowIndex].Value?.ToString() ?? "";
                    Order_Reserve.종목선택(종목명);

                    DataGridViewCell cell_color = view[e.ColumnIndex, e.RowIndex];
                    JanGo_dataGridView[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = cell_color.Style.ForeColor;

                    string itemcode = JanGo_dataGridView["코드_잔고", e.RowIndex].Value.ToString();

                    try
                    {
                        if (JanGo_dataGridView.Columns["선택_잔고"].Index == e.ColumnIndex)
                        {
                            if ((bool)JanGo_dataGridView["선택_잔고", e.RowIndex].Value)
                            {
                                JanGo_dataGridView["선택_잔고", e.RowIndex].Value = false;
                                stockBalanceList[itemcode].선택 = false;
                            }
                            else
                            {
                                JanGo_dataGridView["선택_잔고", e.RowIndex].Value = true;
                                stockBalanceList[itemcode].선택 = true;
                            }
                        }

                        if (JanGo_dataGridView.Columns.Contains("매도_정지"))
                            if (JanGo_dataGridView.Columns["매도_정지"].Index == e.ColumnIndex)
                            {
                                if ((bool)JanGo_dataGridView["매도_정지", e.RowIndex].Value)
                                {
                                    JanGo_dataGridView["매도_정지", e.RowIndex].Value = false;
                                    stockBalanceList[itemcode].매도정지 = false;
                                }
                                else
                                {
                                    JanGo_dataGridView["매도_정지", e.RowIndex].Value = true;
                                    stockBalanceList[itemcode].매도정지 = true;
                                }
                            }

                        if (JanGo_dataGridView.Columns.Contains("추매_정지"))
                            if (JanGo_dataGridView.Columns["추매_정지"].Index == e.ColumnIndex)
                            {
                                if ((bool)JanGo_dataGridView["추매_정지", e.RowIndex].Value)
                                {
                                    JanGo_dataGridView["추매_정지", e.RowIndex].Value = false;
                                    stockBalanceList[itemcode].추매정지 = false;
                                }
                                else
                                {
                                    JanGo_dataGridView["추매_정지", e.RowIndex].Value = true;
                                    stockBalanceList[itemcode].추매정지 = true;
                                }
                            }

                        if (JanGo_dataGridView.Columns.Contains("잔고청산_A"))
                            if (JanGo_dataGridView.Columns["잔고청산_A"].Index == e.ColumnIndex)
                            {
                                if ((bool)JanGo_dataGridView["잔고청산_A", e.RowIndex].Value)
                                {
                                    JanGo_dataGridView["잔고청산_A", e.RowIndex].Value = false;
                                    stockBalanceList[itemcode].잔고청산_A = false;
                                }
                                else
                                {
                                    JanGo_dataGridView["잔고청산_A", e.RowIndex].Value = true;
                                    stockBalanceList[itemcode].잔고청산_A = true;
                                }
                            }

                        if (JanGo_dataGridView.Columns.Contains("잔고청산_B"))
                            if (JanGo_dataGridView.Columns["잔고청산_B"].Index == e.ColumnIndex)
                            {
                                if ((bool)JanGo_dataGridView["잔고청산_B", e.RowIndex].Value)
                                {
                                    JanGo_dataGridView["잔고청산_B", e.RowIndex].Value = false;
                                    stockBalanceList[itemcode].잔고청산_B = false;
                                }
                                else
                                {
                                    JanGo_dataGridView["잔고청산_B", e.RowIndex].Value = true;
                                    stockBalanceList[itemcode].잔고청산_B = true;
                                }
                            }

                        if (JanGo_dataGridView.Columns.Contains("잔고청산_C"))
                            if (JanGo_dataGridView.Columns["잔고청산_C"].Index == e.ColumnIndex)
                            {
                                if ((bool)JanGo_dataGridView["잔고청산_C", e.RowIndex].Value)
                                {
                                    JanGo_dataGridView["잔고청산_C", e.RowIndex].Value = false;
                                    stockBalanceList[itemcode].잔고청산_C = false;
                                }
                                else
                                {
                                    JanGo_dataGridView["잔고청산_C", e.RowIndex].Value = true;
                                    stockBalanceList[itemcode].잔고청산_C = true;
                                }
                            }
                    }
                    catch
                    {
                        JanGo_dataGridView.Rows.Remove(JanGo_dataGridView.Rows[e.RowIndex]);
                        stockBalanceList.TryRemove(itemcode, out _);
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






        // 헤더 체크박스의 현재 상태를 저장하는 필드
        private bool _headerCheckBoxChecked = false;
        // 헤더 체크박스를 그릴 때 사용할 영역을 저장하는 필드
        private Rectangle _headerCheckBoxArea;


        private void DataGridView_JanGo_A_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 잔고A 그리드뷰의 첫 번째 컬럼 헤더에만 적용 (e.ColumnIndex == 0 && e.RowIndex == -1)
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                // 1. 배경 및 경계 그리기
                e.PaintBackground(e.ClipBounds, false);

                // 2. 체크박스 영역 계산
                int nChkBoxWidth = 13;
                int nChkBoxHeight = 13;

                // 가운데 정렬을 위한 위치 계산
                int offsetx = (e.CellBounds.Width - nChkBoxWidth) / 2;
                int offsety = (e.CellBounds.Height - nChkBoxHeight) / 2;

                _headerCheckBoxArea = new Rectangle(
                    e.CellBounds.X + offsetx,
                    e.CellBounds.Y + offsety,
                    nChkBoxWidth,
                    nChkBoxHeight);

                // 3. 체크박스 스타일을 사용하여 체크박스 그리기
                if (Application.RenderWithVisualStyles)
                {
                    // 체크박스 상태 결정
                    CheckBoxState state;
                    if (_headerCheckBoxChecked)
                    {
                        // 체크된 상태
                        state = CheckBoxState.CheckedNormal;
                    }
                    else
                    {
                        // 체크 해제된 상태
                        state = CheckBoxState.UncheckedNormal;
                    }

                    // VisualStyleRenderer를 사용하여 체크박스 그리기
                    VisualStyleRenderer renderer = new VisualStyleRenderer("BUTTON", 3, (int)state);
                    renderer.DrawBackground(e.Graphics, _headerCheckBoxArea);
                }
                else
                {
                    // Visual Style이 비활성화된 경우, ControlPaint로 대체 (이전 방식)
                    ControlPaint.DrawCheckBox(e.Graphics, _headerCheckBoxArea,
                        _headerCheckBoxChecked ? ButtonState.Checked : ButtonState.Normal);
                }

                // 4. 이벤트 처리 완료 알림
                e.Handled = true;
            }

            // 기존에 호출하던 Front_End.DataGridView_CellPainting 호출은 그대로 유지
            Front_End.DataGridView_CellPainting(sender, e);
        }


        //// DataGridView의 CellMouseClick 이벤트에 연결해야 합니다.
        //private void DataGridView_JanGo_A_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    // 잔고A 그리드뷰의 첫 번째 컬럼 헤더에만 적용
        //    if (e.ColumnIndex == 0 && e.RowIndex == -1)
        //    {
        //        // 클릭 위치가 계산된 체크박스 영역 내인지 확인
        //        if (_headerCheckBoxArea.Contains(e.Location))
        //        {
        //            // 헤더 체크박스 상태 반전
        //            _headerCheckBoxChecked = !_headerCheckBoxChecked;

        //            // 전체 행의 체크박스(선택_잔고) 상태 변경
        //            ToggleAllRowsSelection(_headerCheckBoxChecked);

        //            // 헤더 셀 다시 그리기 (체크박스 상태 업데이트)
        //            ((DataGridView)sender).InvalidateCell(e.ColumnIndex, e.RowIndex);
        //        }
        //    }
        //}


        //private void ToggleAllRowsSelection(bool isChecked) // 잔고A 그리드뷰 전체선택/해제 로직
        //{
        //    foreach (DataGridViewRow Row in JanGo_dataGridView.Rows)
        //    {
        //        // DGV의 행에 있는 '선택_잔고' 셀의 값을 헤더 체크박스 상태와 동기화
        //        Row.Cells["선택_잔고"].Value = isChecked;
        //  //      Row.Cells["추매_정지"].Value = isChecked;



        //        // stockBalanceList 딕셔너리의 잔고 객체도 업데이트
        //        if (stockBalanceList.TryGetValue(Row.Cells["코드_잔고"].Value.ToString(), out Stockbalance 잔고))
        //        {
        //            // 잔고 객체의 '선택' 속성도 업데이트
        //            잔고.선택 = isChecked;
        //        }
        //    }

        //    // 변경 사항을 반영하기 위해 그리드뷰 강제 새로고침
        //    JanGo_dataGridView.Refresh();
        //}

        // DataGridView의 CellMouseClick 이벤트에 연결해야 합니다.
        private void DataGridView_JanGo_A_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 잔고A 그리드뷰의 첫 번째 컬럼 헤더에만 적용
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                // 클릭 위치가 계산된 체크박스 영역 내인지 확인
                if (_headerCheckBoxArea.Contains(e.Location))
                {
                    // 헤더 체크박스 상태 반전
                    _headerCheckBoxChecked = !_headerCheckBoxChecked;

                    // 전체 행의 체크박스(선택_잔고) 상태 변경
                    ToggleAllRowsSelection(_headerCheckBoxChecked);

                    // 헤더 셀 다시 그리기 (체크박스 상태 업데이트)
                    ((DataGridView)sender).InvalidateCell(e.ColumnIndex, e.RowIndex);
                }
            }
        }


        private void ToggleAllRowsSelection(bool isChecked) // 잔고A 그리드뷰 전체선택/해제 로직
        {
            // 저사양 PC 최적화: 대량의 행 갱신 시 UI 스레드 오버헤드 및 화면 깜빡임 최소화
            JanGo_dataGridView.SuspendLayout();

            // 성능 최적화: 루프 내부에서 문자열 인덱서로 매번 검색하는 비용을 줄이기 위해 컬럼 인덱스를 미리 캐싱
            int selectIdx = JanGo_dataGridView.Columns["선택_잔고"].Index;
            int stopIdx = JanGo_dataGridView.Columns["추매_정지"].Index;
            int codeIdx = JanGo_dataGridView.Columns["코드_잔고"].Index;

            foreach (DataGridViewRow Row in JanGo_dataGridView.Rows)
            {
                // 가상 행(신규 행 추가용 빈 행)인 경우 건너뛰어 에러 예방 및 속도 향상
                if (Row.IsNewRow) continue;

                // DGV의 행에 있는 '선택_잔고' 및 '추매_정지' 셀의 값을 헤더 체크박스 상태와 동기화
                Row.Cells[selectIdx].Value = isChecked;
                Row.Cells[stopIdx].Value = isChecked;

                // 성능 최적화: 셀 값이 null일 수 있으므로 안전하게 참조
                object codeValue = Row.Cells[codeIdx].Value;
                if (codeValue != null)
                {
                    // stockBalanceList 딕셔너리의 잔고 객체도 업데이트
                    if (stockBalanceList.TryGetValue(codeValue.ToString(), out Stockbalance 잔고))
                    {
                        // 잔고 객체의 '선택' 및 '추매정지' 속성도 업데이트
                        잔고.선택 = isChecked;
                        잔고.추매정지 = isChecked;
                    }
                }
            }

            // 레이아웃 로직 재개 및 변경 사항을 반영하기 위해 그리드뷰 강제 새로고침
            JanGo_dataGridView.ResumeLayout();
            JanGo_dataGridView.Refresh();
        }


        // 이 함수 하나만 모든 DataGridView의 CurrentCellDirtyStateChanged 이벤트에 연결하면 돼!
        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // [최적화 핵심] 일일이 이름을 거명하지 않고, 'sender'를 사용해 이벤트를 발생시킨 녀석만 처리함
            // "sender야, 너 혹시 DataGridView니?" 하고 물어보고 맞으면 dgv 변수에 담음 (패턴 매칭)
            if (sender is DataGridView dgv)
            {
                // 현재 선택된 셀이 '체크박스'일 때만 즉시 반영 (속도 최적화)
                if (dgv.CurrentCell is DataGridViewCheckBoxCell)
                {
                    // 방금 누른 체크박스 상태를 즉시 확정지음 (CellValueChanged 이벤트 바로 발동)
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
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
        private void Button_save_key_Click(object sender, EventArgs e)
        {
            Form_Top_Most();

            MBC_sender = (sender as Button).Name;
            중요메세지("Key입력 저장", "key 를 변경하면 재시작 됩니다. 변경하겠습니까?");
        }

        /////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////    폼 로딩 &    종료전 설정값 불러오기    ///////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////


        private void BT_주문내역_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            비프음("버튼");
            Writing_Management.BT_주문내역_Click();
        }
        private void BT_체결내역_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            비프음("버튼");
            Writing_Management.BT_체결내역_Click();
        }
        private void BT_신규매수내역_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            비프음("버튼");
            Writing_Management.BT_신규매수내역_Click();
        }
        private void BT_전량매도내역_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            비프음("버튼");

            Writing_Management.BT_전량매도내역_Click();
        }

        public void 체크박스_비프(object sender)
        {
            if ((sender as CheckBox) == null)
            {
                비프음("체크");
                return;
            }

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
                    case "에러":
                        Beep(1030, 50);// 높은도 0.05초
                        break;
                    case "체크":
                        Beep(640, 20);// 도 0.02초
                        break;
                    case "언체크":
                        Beep(1024, 20); // 높도 0.02초
                        break;
                    case "버튼":
                        Beep(640, 20);// 도 0.02초
                        Beep(1024, 10); // 솔 0.01초
                        break;
                    case "실행":
                        Beep(512, 10); // 도 0.1초  삐리리
                        Beep(640, 20); // 미 0.2초
                        Beep(768, 30); // 솔 0.3초
                        break;
                    case "정지":
                        Beep(768, 30); // 솔 0.3초
                        Beep(640, 20); // 미 0.2초
                        Beep(512, 10); // 도 0.1초  삐리리
                        break;
                }
            }
        }

        public static void AutoClosingAlram(string text, string title, double time, string 기록)
        {
            // 1. 로그 기록 (기존 로직 유지)
            if (기록.Equals("동작"))
               Log.동작기록(text);
            else
               Log.에러기록(text);

            // [지니 최적화] Thread 생성 제거
            // AutoClosingMessageBox 자체가 비동기(Non-blocking)로 뜨도록 설계되었으므로
            // 그냥 호출해도 프로그램이 멈추지 않고, 동시에 여러 개가 잘 뜹니다.
            AutoClosingMessageBox.Show(text, title, (int)(time * 1000));
        }

        public void Message_Alram(string text, string title)
        {
             Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                using (new CenterWinDialog(form1))
                    MessageBox.Show(new Form { TopMost = false }, text, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            });
        }




        // [필수] 현재 떠 있는 메세지창을 관리하기 위한 토큰 (함수 밖에 선언)
        private static CancellationTokenSource 현재_MBC_취소토큰;
        public static async void 중요메세지(string title, string text)
        {
            비프음("버튼");

            // 1. [중복 호출 방지] 이미 떠 있는 창이 있다면 "작업 취소" 신호를 보냄
            // 강제 종료(Abort)가 아니라 자연스럽게 사라지게 만듭니다.
            if (현재_MBC_취소토큰 != null)
            {
                try
                {
                    현재_MBC_취소토큰.Cancel();
                    현재_MBC_취소토큰.Dispose();
                }
                catch { }
            }

            // 2. 새로운 토큰 생성
            현재_MBC_취소토큰 = new CancellationTokenSource();
            var token = 현재_MBC_취소토큰.Token;

            // 3. 변수 초기화 (닫힘 상태 리셋)
            Form1.form1.MBC_close = false;
            Form1.form1.MBC_result = false;

            try
            {
                // 4. [UI 설정] 단 한 번의 UI 스레드 호출로 모든 설정 완료 (최적화)
                Helper.안전한_UI_업데이트(Form1.form1, () =>
                {
                    // 4-1. 컨트롤 추가 (없을 때만)
                    if (!Form1.form1.Controls.Contains(Form1.form1.MBC))
                    {
                        Form1.form1.Controls.Add(Form1.form1.MBC);
                    }

                    // 4-2. 위치 및 화면 설정
                    Form1.form1.MBC.Location = new Point(759, 246); // 지정된 좌표
                    Form1.form1.MBC.BringToFront();
                    Form1.form1.MBC.Visible = true; // 강제로 보이게 설정
                    Form1.form1.MBC.Show();
                    Form1.form1.MBC.BT_적용.Select(); // 버튼 포커스

                    // 4-3. 텍스트 설정
                    Form1.form1.MBC.LB_title.Text = title;
                    Form1.form1.MBC.LB_text.Text = text;
                });

                // 5. [대기 루프] 사용자가 버튼을 눌러 닫을 때까지 대기
                // while(true) 대신 Task.Delay를 사용하여 CPU 점유율을 낮춤
                while (!Form1.form1.MBC_close)
                {
                    // 취소 요청(새로운 메시지가 옴)이 있으면 루프 탈출 -> catch로 이동
                    if (token.IsCancellationRequested) return;

                    // 0.2초 대기 (UI 멈춤 없음)
                    await Task.Delay(200, token);
                }

                // 6. [정상 종료] 사용자가 버튼을 눌러서 MBC_close가 true가 된 경우
                if (!token.IsCancellationRequested)
                {
                    Helper.안전한_UI_업데이트(Form1.form1, () =>
                    {
                        if (Form1.form1.Controls.Contains(Form1.form1.MBC))
                        {
                            Form1.form1.Controls.Remove(Form1.form1.MBC);
                        }
                    });
                }
            }
            catch (OperationCanceledException)
            {
                // 새로운 중요메세지가 와서 기존 창이 취소된 경우입니다.
                // 여기서 바로 return 하면, 새로운 함수가 UI를 덮어쓰므로 자연스럽게 교체됩니다.
                return;
            }
            catch (Exception)
            {
                // 기타 에러 무시
            }
        }


        // 환경설정저장

        private void BT_Setting_Save_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            비프음("버튼");

            if (로딩완료)
            {
                Setting_backup.Userdata_Save();
            }
            else
            {
                Helper.알림창_멀티("에러알림","로그인 후 저장 가능 합니다.", 10, false);
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
                Helper.알림창_멀티("에러알림","환경설정을 불러오지 못 하였습니다.", 10, false);

               Log.에러기록(" ");
               Log.에러기록("[에러알림] 환경설정을 불러오지 못 하였습니다.");
               Log.에러기록(" ");
            }
        }

        private void BT_jagoGroup_initialization_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            MBC_sender = (sender as Button).Name;
            중요메세지("저장확인", "모든 잔고 종목의 그룹을 초기화 하시겠습니까 ?");
        }

        private void Tab_주문_SelectedIndexChanged(object sender, EventArgs e)
        {
            Front_End.Tab_주문_SelectedIndexChanged();
        }

        private void Tab_체결_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tab_체결.SelectedIndex == 0)
            {
                panel_체결.Controls.Add(Label_체결row);
                panel_체결.Controls.Add(TB_체결row);
                Label_체결row.BringToFront();
                TB_체결row.BringToFront();
            }
            else
            {
                panel_체결.Controls.Remove(Label_체결row);
                panel_체결.Controls.Remove(TB_체결row);
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
            if (ON_LINE)
            {
                Statistical_chart.매매내역확인();
            }
            else
            {
                AutoClosingAlram("[에러확인] 로그인 되지 않았습니다.", "에러알림", 5, "에러");
            }
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
            if (ON_LINE)
            {
                Statistical_chart.BT_기준일별확인();
            }
            else
            {
                AutoClosingAlram("[에러확인] 로그인 되지 않았습니다.", "에러알림", 5, "에러");
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////         설정 값  저장하기           ////////////////////////
        //////////////////////////////////////////////////////////////////////////////////

        // [+] 전역 변수로 종료 상태를 추적합니다. (클래스 상단 또는 해당 함수 바로 위에 배치)
        private bool 종료진행중 = false;

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 1. 이미 종료 프로세스가 진행 중이면 중복 실행(에러)을 방지합니다.
            if (종료진행중)
            {
                e.Cancel = true;
                return;
            }

            if (GenieConfig.textBox_ID != "ID") TelegramMessenger.Telegram_alram("logOut");

            Set_default.그룹n관심_save();

            // 2. 윈도우 기본 닫기 이벤트를 멈추고(Cancel), 우리의 비동기 함수가 끝날 때까지 대기합니다.
            e.Cancel = true;
            종료진행중 = true;

            // 3. 비동기 종료 및 파일 저장 로직을 끝까지 기다립니다.
            await Form_Closing();
        }

        public async Task Form_Closing()
        {
            form1.Enabled = false;
            Form1.프로그램종료중 = true;

            RB_sell_stop.Checked = true;
            RB_buy_stop.Checked = true;

            // =========================================================================
            // [지니 최적화 1] HashSet 대신 고속 LINQ 사용 & 메인 장부 즉시 삭제
            // =========================================================================
            var 삭제할_주문키_리스트 = JumunItem_List
                .Where(주문쌍 => 주문쌍.Value.주문번호.Contains("+"))
                .Select(주문쌍 => 주문쌍.Key)
                .ToList();

            foreach (string 삭제할키 in 삭제할_주문키_리스트)
            {
                JumunItem_List.TryRemove(삭제할키, out _);
            }

            order_scheduler.ClearQueue();
            tr_scheduler.ClearQueue();

            Helper.안전한_UI_업데이트(Form1.form1, () =>
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

            Log.동작기록(" ");
            Log.동작기록(" ");
            Log.동작기록("MoneyGame 을 종료 합니다. ");

            재시작 = true;

            try
            {
                foreach (var 잔고 in stockBalanceList.Values)
                {
                    잔고.매매가능 = false;
                }

                if (CB_미체결취소.Checked && Get.시장시작 <= Get.TimeNow && Get.TimeNow <= Get.시장종료)
                {
                    미체결일괄취소();

                    Log.동작기록("");
                    Log.동작기록("[미체결 일괄취소] 미체결 주문을 일괄 취소 하였습니다.");
                    Log.동작기록("");

                    Helper.알림창_멀티("미체결 일괄취소", "미체결 주문을 일괄 취소 하였습니다.", 10, false);

                    // [+] 무한 루프 방어: 증권사 응답이 씹힐 경우를 대비해 최대 5.5초만 대기합니다.
                    int 무한루프_방지카운트 = 0;

                    while (무한루프_방지카운트 < 25)
                    {
                        if (JumunItem_List.Count == 0)
                        {
                            var 미체결_그리드뷰 = Form_Outstanding.form.Outstanding_DataGridView;

                            // [최적화 2] DataGridView가 비어있으면 루프 완전 종료
                            if (미체결_그리드뷰.Rows.Count == 0) break;

                            // =========================================================================
                            // [지니 최적화 3] 종료 시 그리드뷰 삭제 속도 극대화 (화면 그리기 일시정지)
                            // =========================================================================
                            미체결_그리드뷰.SuspendLayout();
                            try
                            {
                                for (int 인덱스 = 미체결_그리드뷰.RowCount - 1; 인덱스 >= 0; 인덱스--)
                                {
                                    var 셀값 = 미체결_그리드뷰.Rows[인덱스].Cells["검색식_미체결"]?.Value?.ToString();

                                    if (셀값?.Contains("삭제") == true)
                                    {
                                        미체결_그리드뷰.Rows.RemoveAt(인덱스);
                                    }
                                }
                                미체결_그리드뷰.CurrentCell = null;
                            }
                            finally
                            {
                                미체결_그리드뷰.ResumeLayout();
                            }
                        }

                        await Task.Delay(220);
                        무한루프_방지카운트++;
                    }
                }

                try
                {
                    if (watch_condition_A.Text.Trim().Length > 0) Tab_Watch.Test_save_("BT_watch_Save_A");
                    if (watch_condition_B.Text.Trim().Length > 0) Tab_Watch.Test_save_("BT_watch_Save_B");
                    if (watch_condition_C.Text.Trim().Length > 0) Tab_Watch.Test_save_("BT_watch_Save_C");
                    if (watch_condition_D.Text.Trim().Length > 0) Tab_Watch.Test_save_("BT_watch_Save_D");
                }
                catch
                {
                    Log.에러기록("[에러 확인] watchsave_ 에러 ");
                }

                Console_print("종료합니다");
                Log.동작기록("종료합니다.");

                if (릴리즈) logout.접속종료();
                SaveToFile.잔고_파일저장();
                SaveToFile.주문리스트_파일저장();
                SaveToFile.최종매입가_파일저장(Form1.로딩완료);
                SaveToFile.리밸감시주문_파일저장();
                SaveToFile.검색식_파일저장();

                if (!DateTime.Now.DayOfWeek.Equals(DayOfWeek.Saturday) && !DateTime.Now.DayOfWeek.Equals(DayOfWeek.Sunday))
                {
                    if (!공휴일) DataManagement.DataBackUp(Application.StartupPath);
                }

                F_close();

                // [+] 동기 딜레이(Form1.Delay) 대신 UI 멈춤 현상이 없는 비동기(Task.Delay)로 교체하여 마지막까지 부드럽게 종료
                for (int 카운트 = 3; 카운트 >= 0; 카운트--)
                {
                    Console_print("종료 - " + UnifiedDataManager.Instance.Writing.Count + " []  x - " + 카운트);
                    Log.동작기록("종료 - " + 카운트);

                    await Task.Delay(1000);
                }

                // 4. 모든 저장이 완벽하게 끝나면, CPU 점유율 없이 프로그램을 가장 깔끔하게 강제 종료합니다.
                Environment.Exit(0);
            }
            catch
            {
                Log.에러기록("[에러 확인] FormClosing 에러 ");
            }
        }

        private static bool 재시작_파워쉘_가동완료 = false;
        private void F_close()
        {
            if (GenieConfig.textBox_ID != "ID") TelegramMessenger.Telegram_alram("logOut_user");

            if (ON_LINE && 로딩완료)
            {
                if (Acc.실현손익 > 0)
                {
                    if (Acc.추정자산 > GenieConfig.MT_principal)
                    {
                        if (GenieConfig.Today_매수기준금.Contains(str.today))
                        {
                            GenieConfig.매수계산기준금 = long.Parse(GenieConfig.Today_매수기준금.Split('@')[0]) + Acc.실현손익;
                        }
                        else
                        {
                            GenieConfig.Today_매수기준금 = GenieConfig.매수계산기준금 + "@" + str.today;
                            GenieConfig.매수계산기준금 = GenieConfig.매수계산기준금 + Acc.실현손익;
                        }

                        if (GenieConfig.Today_손익기준금.Contains(str.today))
                        {
                            GenieConfig.손익계산기준금 = long.Parse(GenieConfig.Today_손익기준금.Split('@')[0]) + Acc.실현손익;
                        }
                        else
                        {
                            GenieConfig.Today_손익기준금 = GenieConfig.손익계산기준금 + "@" + str.today;
                            GenieConfig.손익계산기준금 = GenieConfig.손익계산기준금 + Acc.실현손익;
                        }
                    }
                }
            }
            REG.핑_반복중지();

            GenieConfig.CB_세로보기 = Form1.form1.CB_세로보기.Checked;
            Properties.Settings.Default.Location = this.Location;
            Properties.Settings.Default.Save();

            Set_default.FormClose_Save();
            GenieConfig.SaveAll_Genie_Config();
            if (ON_LINE) Get_token.logOut();


            UnifiedDataManager.Instance.StopAll();

            if (HI지니64시작)
            {
                // 💡 [핵심 방어벽] 이미 파워쉘 감시자를 켰다면, 중복 실행을 차단하고 즉시 함수를 빠져나갑니다.
                if (재시작_파워쉘_가동완료) return;

                // 통과했다면 잠금장치를 즉시 걸어버립니다. (두 번 다시 실행 불가)
                재시작_파워쉘_가동완료 = true;

                string targetExe = System.IO.Path.Combine(Form1.startupPath, "HI지니64.exe");

                if (System.IO.File.Exists(targetExe))
                {
                    try
                    {
                        int 현재프로세스ID = System.Diagnostics.Process.GetCurrentProcess().Id;

                        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                        psi.FileName = "powershell.exe";

                        string ps명령어 = $"Wait-Process -Id {현재프로세스ID} -ErrorAction SilentlyContinue; Start-Process -FilePath '{targetExe}' -WorkingDirectory '{Form1.startupPath}'";

                        psi.Arguments = $"-WindowStyle Hidden -NoProfile -Command \"{ps명령어}\"";

                        psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        psi.CreateNoWindow = true;
                        psi.UseShellExecute = false;

                        // 💡 [수정] 단순 실행이 아니라 반환되는 프로세스 객체를 추적합니다.
                        System.Diagnostics.Process 감시자프로세스 = System.Diagnostics.Process.Start(psi);
                    }
                    catch (Exception ex)
                    {
                        Message_Alram($"실행 중 알 수 없는 오류가 발생했습니다.\n사유: {ex.Message}", "실행 오류");
                    }
                }
                else
                {
                    Message_Alram($"지니64를 재시작 할 수 없습니다.\n{targetExe}\n경로에 실행 파일이 없습니다.", "재시작 오류");
                }
            }
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
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


        private void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
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
                AutoClosingAlram("지니64를 종료 합니다.", "종료", 5, "에러");
                Application.Exit();
            }
        }

        private void TabControl_L_Top_DrawItem(object sender, DrawItemEventArgs e)
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
                StringFormat sftTab = new StringFormat(StringFormatFlags.NoClip)
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

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
            Front_End.CB_미니시계_CheckedChanged(sender);
        }

        private void CB_종목비공개_CheckedChanged(object sender, EventArgs e)
        {
            Front_End.CB_종목비공개_CheckedChanged(sender);
        }

        private void CB_검색보기_CheckedChanged(object sender, EventArgs e)
        {
            Front_End.CB_검색보기_CheckedChanged(sender);
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
            Tab_Watch.Combo_Watch_DropDown(sender);
        }

        // ---------------------------------------------------------
        // 검색식 콤보박스 이벤트 핸들러 (수정됨)
        // ---------------------------------------------------------

        private void CBB_RunCondition_DropDownClosed(object sender, EventArgs e)
        {
            if (로딩완료) 비프음("체크");

            // [수정] .Settings.Default -> Setting.accmgr 사용
            // 기존 설정값과 현재 선택된 값이 다르면 리스트 초기화
            if (CBB_SearchCondition.SelectedIndex > -1 && !GenieConfig.CBB_SearchCondition.Equals(CBB_SearchCondition.Text))
            {
                Form1.form1.SearchView_List.Clear();
                Search_List.Items.Clear();
                Search_List.Items.Add(Get.TimeNow.ToString("##:##:##") + " | 지니64오토스탁");
            }
        }

        private void CBB_SearchCondition_TextChanged(object sender, EventArgs e)
        {
            if (CBB_SearchCondition.SelectedIndex == -1)
            {
                // [수정] 설정에서 값 불러오기
                CBB_SearchCondition.SelectedItem = GenieConfig.CBB_SearchCondition;
            }
            else
            {
                // [수정] 설정에 값 저장하기 (메모리)
                GenieConfig.CBB_SearchCondition = CBB_SearchCondition.Text;
            }
        }

        private void Tab_잔고_SelectedIndexChanged(object sender, EventArgs e)
        {
            tab_잔고_index = tab_잔고.SelectedIndex;
            Front_End.Tab_잔고_SelectedIndexChanged();
        }


        private void 신규_A_MouseHover(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            toolTip1.SetToolTip(combo, combo.Text);
        }

        private void 와치_A_MouseHover(object sender, EventArgs e)
        {
            string Tip1 = "\n#신규매수 검색식 중 AND 검색식 일 경우\n  진입가 오차가 발생합니다.\n#검색식 이름에 <  >  ?  [  ]  : | * 등의\n  문자가 들어 있으면 저장되지 않습니다.";
            string Tip2 = "\n추가 검색식 :: 선택한뒤 조건식 선택에서 확인할수 있습니다.\n조건식 변경후 'Watch 저장' 버튼을 클릭 하면 적용 됩니다.";

            toolTip1.SetToolTip(watch_condition_A, "검색식 : " + watch_condition_A.Text + Tip1);
            toolTip1.SetToolTip(watch_condition_B, "검색식 : " + watch_condition_B.Text + Tip1);
            toolTip1.SetToolTip(watch_condition_C, "검색식 : " + watch_condition_C.Text + Tip1);
            toolTip1.SetToolTip(watch_condition_D, "검색식 : " + watch_condition_D.Text + Tip1);
            toolTip1.SetToolTip(와치_A, "검색식 : " + 와치_A.Text + Tip2);
            toolTip1.SetToolTip(와치_B, "검색식 : " + 와치_B.Text + Tip2);
            toolTip1.SetToolTip(와치_C, "검색식 : " + 와치_C.Text + Tip2);
            toolTip1.SetToolTip(와치_D, "검색식 : " + 와치_D.Text + Tip2);
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
            if (CBB_최종가종목.SelectedIndex < CBB_최종가종목.Items.Count - 1) CBB_최종가종목.SelectedIndex++;
        }

        private void BT_종목다운_Click(object sender, EventArgs e)
        {
            if (CBB_최종가종목.SelectedIndex > 0) CBB_최종가종목.SelectedIndex--;
        }

        private void 최종매입가View_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DGV_최종매입가View.ClearSelection();
        }

        private void 최종매입가View_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DGV_최종매입가View.ClearSelection();
        }

        private void CBB_jumun_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrint.CBB_jumun_SelectedIndex(sender);
        }
       
        private void BT_지수이평_Click(object sender, EventArgs e)
        {
            switch (GenieConfig.지수이평)
            {
                case null:
                    GenieConfig.지수이평 = "지수이평설정";
                    break;
                case "지수이평설정":
                    CB_검색보기.Checked = false;
                    GenieConfig.지수이평 = "지수이평프린터";
                    break;
                case "지수이평프린터":
                    GenieConfig.지수이평 = "지수설정";
                    break;
                case "지수설정":
                    GenieConfig.지수이평 = "지수이평설정";
                    break;
            }

            LayoutChange.CB_기능설정_CheckedChanged("지수");
        }

        private async void CheckBox_key_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_key.Checked)
            {
                panel_key.Show();
                panel_key.BringToFront();
            }
            else
            {
                panel_key.Hide();
            }
        }

        private void CheckBox_Simulation_CheckedChanged_1(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);
            if (CB.Checked)
            {
                CB.Text = "■ 모의 투자";
            }
            else
            {
                CB.Text = "□ 모의 투자";
            }
        }

        private void TR_timer_Tick(object sender, EventArgs e)
        {
            TR_timer_.Tick();
        }


        private void DataGridView_watch_A_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Front_End.DataGridView_CellPainting(sender, e);
        }

        private void DataGridView_watch_B_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Front_End.DataGridView_CellPainting(sender, e);
        }

        private void DataGridView_watch_C_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Front_End.DataGridView_CellPainting(sender, e);
        }

        private void DataGridView_watch_D_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Front_End.DataGridView_CellPainting(sender, e);
        }




        private void 와치_DropDownClosed(object sender, EventArgs e)
        {
            Form1.비프음("체크");
            ComboBox combo = sender as ComboBox;
            if (combo.SelectedItem == null) combo.SelectedItem = Form1.위치별검색식리스트[combo.Name].이름;
        }


        [Conditional("DEBUG")] // ★ 핵심: 디버그 모드에서만 실행됨
        public static void Console_print(string msg)
        {
            Console.WriteLine(msg);
        }

        // =================================================================
        // 1. [입력 완료 보정] 마우스로 다른 곳 클릭 시 시간 보정
        // (※ 폼 디자인 창에서 MT_misu_time의 Leave 이벤트에 연결하세요!)
        // =================================================================
        private void MT_misu_time_Leave(object sender, EventArgs e)
        {
            시간_보정_및_검사(sender as MaskedTextBox);
        }

        // =================================================================
        // 2. [엔터키 처리] 엔터를 쳤을 때도 시간 보정
        // (※ 폼 디자인 창에서 MT_misu_time의 KeyDown 이벤트에 연결하세요!)
        // =================================================================
        private void MT_misu_time_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                시간_보정_및_검사(sender as MaskedTextBox);

                // 바탕(Form)으로 포커스를 옮겨서 자연스럽게 Leave 이벤트를 유도합니다.
                this.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // =================================================================
        // 3. [공통 함수] 수능일 체크박스 연동 및 최소/최대 시간 보정 알바생
        // =================================================================
        private void 시간_보정_및_검사(MaskedTextBox 마스크텍스트박스)
        {
            if (마스크텍스트박스 == null) return;

            // MaskedTextBox의 "15:15:00" 형태에서 ':'와 공백을 모두 제거해 순수 숫자만 추출 ("151500")
            string 순수텍스트 = 마스크텍스트박스.Text.Replace(":", "").Replace(" ", "");

            // [핵심 1] 수능일 여부에 따른 당일 최대시간 세팅
            int 최대시간 = GenieConfig.CB_수능일 ? 161500 : 151500;
            int 최소시간 = 150000; // 15시 00분 00초

            // 다 지우고 나갔거나 숫자가 아니면 무조건 당일 최대시간으로 강제 세팅
            if (string.IsNullOrEmpty(순수텍스트) || !int.TryParse(순수텍스트, out int 입력시간))
            {
                마스크텍스트박스.Text = 최대시간.ToString("D6");
                return;
            }

            // [핵심 2] 150000 미만이거나 최대시간 초과 시 모두 최대시간으로 덮어씌움!
            if (입력시간 < 최소시간)
            {
                입력시간 = 최대시간;
            }
            else if (입력시간 > 최대시간)
            {
                입력시간 = 최대시간;
            }

            // 보정된 숫자를 6자리 문자열("151500")로 만들어서 넣으면 MaskedTextBox가 찰떡같이 "15:15:00"으로 표시함!
            마스크텍스트박스.Text = 입력시간.ToString("D6");
        }

        // --------------------------------------------------------------------------------
        // [Form1.cs] 세로보기 체크박스 비동기 이벤트 핸들러 (에러 교정본)
        // --------------------------------------------------------------------------------
        // 💡 [교정 핵심] 기존 'private async Task'를 'private async void'로 변경하여 컴파일 에러를 해결합니다.
        private async void CB_세로보기_CheckedChanged(object sender, EventArgs e)
        {
            // 1. 체크 상태에 따라 텍스트를 즉시 변경합니다.
            CB_세로보기.Text = CB_세로보기.Checked ? "MIN" : "FULL";

            if (CB_세로보기.Checked)
            {
                // 서브폼이 없거나 닫힌 상태라면 새로 생성합니다.
                if (Sub_form.form == null || Sub_form.form.IsDisposed)
                {
                    Sub_form.form = new Sub_form();
                }

                if (로딩완료)
                {
                    Form1.form1.CB_기본매매.Checked = false;
                    Form1.form1.CB_반복매매.Checked = false;
                    Form1.form1.CB_계좌관리.Checked = false;
                    Form1.form1.CB_특수매매.Checked = false;
                    Form1.form1.CB_대금탐색.Checked = false;
                    Form1.form1.CB_매매그룹.Checked = false;
                    Form1.form1.CB_기능설정.Checked = false;

                    Sub_form.form.CB_Layout_A.Enabled = true;
                    Sub_form.form.CB_Layout_B.Enabled = true;
                    Sub_form.form.CB_Layout_C.Enabled = true;
                    Sub_form.form.CB_Layout_X.Enabled = true;
                    Sub_form.form.CB_통계.Enabled = true;
                }

                Sub_form.세로보기(true);

                if (Form1.form1.JanGo_dataGridView != null && !Form1.form1.JanGo_dataGridView.IsDisposed)
                {
                    Form1.form1.JanGo_dataGridView.SuspendLayout();
                    Form1.form1.JanGo_dataGridView.Rows.Clear();
                    Method.SortClear(Form1.form1.JanGo_dataGridView);

                    List<Stockbalance> 정렬된잔고리스트 = new List<Stockbalance>(Form1.stockBalanceList.Values);
                    정렬된잔고리스트.Sort((a, b) => b.초기매수일.CompareTo(a.초기매수일));

                    foreach (Stockbalance 잔고 in 정렬된잔고리스트)
                    {
                        int rowIndex = Form1.form1.JanGo_dataGridView.Rows.Add();
                        Form1.form1.JanGo_dataGridView.Rows[rowIndex].Cells["코드_잔고"].Value = 잔고.종목코드;
                        홀딩잔고.JangoRow_print(rowIndex, 잔고);
                    }
                    Form1.form1.JanGo_dataGridView.ClearSelection();
                    Form1.form1.JanGo_dataGridView.ResumeLayout();
                }

                // [핵심] 서브폼이 화면에 표시된 직후 강제로 포커스를 이동시킵니다.
                if (Sub_form.form != null && !Sub_form.form.IsDisposed)
                {
                    Sub_form.form.Activate(); // 서브폼을 최상위로 활성화
                    Sub_form.form.Focus();    // 포커스 이동
                }
            }
            else
            {
                // 서브폼이 열려 있을 때만 복귀(false) 명령을 내립니다.
                if (Sub_form.form != null && !Sub_form.form.IsDisposed)
                {
                
                    Sub_form.세로보기(false);

                    // UI를 멈추지 않고 지정하신 0.5초(500ms) 동안 안전하게 대기합니다.
                    LayoutChange.CBB_layout_SelectedIndex(0);
                    await Task.Delay(500);
                    LayoutChange.CBB_layout_SelectedIndex(GenieConfig.CBB_layout);

                    // [핵심] 서브폼이 닫힌 직후 Form1으로 강제 포커스를 이동시킵니다.
                    if (form1 != null && !form1.IsDisposed)
                    {
                        form1.Activate(); // 메인폼을 최상위로 활성화
                        form1.Focus();    // 포커스 이동
                    }
                }
            }
        }


        private async void Button1_Click(object sender, EventArgs e)
        {
            Form1.Console_print("\n====== [Button1_Click] ======");

            //// 토큰 캐시 파일 경로 지정
            //string tokenFolder = Path.Combine(Application.StartupPath, "토큰발급");
            //string tokenFilePath = Path.Combine(tokenFolder, "한투_token_cache.json");

            //// 폴더와 파일이 모두 실재하는지 체크 (저사양 PC 디스크 오버헤드가 적은 가벼운 검사 방법)
            //if (Directory.Exists(tokenFolder) && File.Exists(tokenFilePath))
            //{
            //    // 비동기로 스케줄러 혹은 요청이 처리되도록 await 구조로 실행 유도
            //    await Task.Run(() => 한투_TR요청.한투_주식잔고조회(null, null, "", true));
            //}

         //   LS_TR요청.LS_현물계좌예수금_주문가능금액_총평가조회("Y", false);

            await 한투_실시간요청.체결통보_등록(한투_WS_approval_key, "@3033750", GenieConfig.checkBox_Simulation);

        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            Form1.Console_print("\n====== [Button2_Click] ======");

          //  LS_TR요청.LS_주식잔고2("Y", false);
         //   LS_TR요청.LS_현물계좌_잔고내역조회("Y", false);

        //await 한투_실시간요청.체결통보_등록(한투_WS_approval_key, "@3033750", GenieConfig.checkBox_Simulation);

            // 발급받은 접근 토큰과 빈 문자열, 모의투자 여부를 전달
            await LS_실시간요청.체결통보_등록(LS_API_token, "", GenieConfig.checkBox_Simulation);

            //RealData_Management.AVG_jisu_print("001", Form1.Acc.피_현재가);
            //RealData_Management.AVG_jisu_print("101", Form1.Acc.닥_현재가);
        }

    }
}




// 🌟 1. 확장 메서드 정의 (Form1 클래스 외부에 정의한다고 가정)
public static class ControlExtensions
{
    public static void SetDoubleBuffered(this Control control, bool setting)
    {
        // 리플렉션을 사용하지 않고 protected 멤버에 접근할 수 없으므로,
        // 현재 코드처럼 리플렉션을 사용하되, 호출을 단순화합니다.
        typeof(Control).InvokeMember("DoubleBuffered",
                                     BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                                     null,
                                     control,
                                     new object[] { setting });
    }
}




















