using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;    // List 사용

namespace 지니64
{
    public class Jumun
    {

        public static bool 메인장부_중복_검사(string 종목코드, string 검색식 = "", 감시주문 감시 = null)
        {
            bool 중복존재 = false;

            try
            {

                //  Console_print($"=== [중복검사 진입] 종목: {감시.종목명}({종목코드}) | 검색식: {검색식}  | 타겟감시번호: {감시.감시번호} ===");


                // 1. [검색식 중복 검사] 검색식 텍스트가 전달된 경우
                if (!string.IsNullOrEmpty(검색식))
                {
                    // [지니 최적화] Any() 대신 FirstOrDefault()를 사용하여 중복된 객체의 실체를 가져옵니다.
                    // 조건에 맞는 첫 번째 항목을 찾는 즉시 검색을 멈추므로 속도 저하는 없습니다.
                    JumunItem 중복된주문 = Form1.JumunItem_List.Values.FirstOrDefault(개별주문 =>
                        개별주문.종목코드 == 종목코드 && 개별주문.검색식 == 검색식);

                    if (중복된주문 != null)
                    {
                        중복존재 = true;
                        //    Console_print($"=== [중복검사 [-] 검색식 중복 매칭] >> 종목: {감시.종목명} | 감시번호: {감시.감시번호} | 중복된 검색식: {검색식} ===");
                    }

                    return 중복존재;
                }

                // 2. [감시번호 중복 검사] 감시 객체가 전달된 경우 (상세 로그 포함)
                if (감시 != null)
                {

                    JumunItem 중복된주문 = Form1.JumunItem_List.Values.FirstOrDefault(개별주문 =>
                        개별주문.종목코드 == 종목코드 && 개별주문.감시번호 == 감시.감시번호);

                    if (중복된주문 != null)
                    {
                        중복존재 = true;
                        //   Console_print($"===  [중복검사 [-] 감시번호 중복 매칭] >> 종목: {감시.종목명} | 감시번호: {감시.감시번호} | 검색식: {검색식} ===");
                    }

                    return 중복존재;
                }
            }
            catch// (Exception 에러)
            {
                // 에러 발생 시 로그 출력 후 안전하게 중복(매매/주문 불가) 상태로 처리
                if (감시 != null)
                {
                    //   Console_print($"=== [중복검사 [-] [치명적 에러] 감시번호 메인장부_중복_검사 중 예외 발생: {에러.Message}");
                }
                else
                {
                    //  Console_print($"=== [중복검사 [-] [치명적 에러] 검색식 메인장부_중복_검사 중 예외 발생: {에러.Message}");
                }
                중복존재 = true;
            }

            return 중복존재;
        }



        public static JumunItem 출처찾기_및_업데이트(string 주문번호, string 종목코드, int 주문수량)
        {
            JumunItem 가장빠른객체 = null;
            long 최소시간 = long.MaxValue; // 가장 작은 값을 찾기 위한 초기화
            int 임시객체수 = 0; // [+] 동일 조건의 임시객체 카운트용 변수

            // 1. 메인 리스트를 순회하며 "+++" 상태인 주문 중 가장 먼저 생성된 객체를 찾습니다.
            foreach (var 항목 in Form1.JumunItem_List)
            {
                if (항목.Value.주문번호 == "+++" &&
                    항목.Value.종목코드 == 종목코드 &&
                    항목.Value.주문수량 == 주문수량)
                {
                    임시객체수++; // 조건이 일치하는 임시주문 발견 시 카운트 증가

                    // 현재 찾은 항목의 시간이 지금까지 찾은 최소시간보다 작다면(더 빠르다면) 갱신
                    if (항목.Value.주문시간_Ticks < 최소시간)
                    {
                        최소시간 = 항목.Value.주문시간_Ticks;
                        가장빠른객체 = 항목.Value;
                    }
                }
            }

            // 2. 가장 빠른 객체를 찾아 UpdateKey로 넘깁니다.
            if (가장빠른객체 != null)
            {
                Form1.Console_print($"[+] >> {가장빠른객체.종목명}({종목코드}) 임시주문(+++) 출처 확인 완료 (대기중인 동종 주문: {임시객체수}개) -> 실제 주문번호[{주문번호}] 업데이트 진행");
                return UpdateKey(주문번호, 가장빠른객체.Screennum, 가장빠른객체.종목코드, 가장빠른객체);
            }
            else
            {
                // 못 찾았을 때의 예외 상황 로그 (디버깅용)
                if (임시객체수 > 0) Form1.Console_print($"[-] >> {종목코드} 임시주문(+++) 출처를 찾지 못했습니다. (대기중인 동종 주문: {임시객체수}개 | 수신 주문번호: {주문번호} | 수량: {주문수량})");
            }

            return null;
        }

        // ---------------------------------------------------------
        // 주문 추가 (누락된 주문)
        // ---------------------------------------------------------
        public static void Add누락주문(JumunItem 신규주문)
        {
            // 1. 메인 리스트에만 심플하게 추가
            bool 추가성공 = Form1.JumunItem_List.TryAdd(신규주문.주문번호, 신규주문);

            if (!추가성공)
            {
                Form1.Console_print($"[-] JumunItem_List Add누락주문 오류: 키 '{신규주문.주문번호}'는 이미 존재합니다. 항목을 추가하지 않았습니다.");
            }
        }

        // 종목별로 하나씩만 순차적으로 통과시키기 위한 동기화 자물쇠 저장소입니다.
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> _종목별자물쇠명단 = new ConcurrentDictionary<string, SemaphoreSlim>();

        public static async Task Add(JumunItem 신규주문)
        {
            // 1. 해당 종목 전용 번호표(세마포어)를 가져오거나 없으면 새로 생성합니다.
            var 종목자물쇠 = _종목별자물쇠명단.GetOrAdd(신규주문.종목코드, _ => new SemaphoreSlim(1, 1));

            // 2. 다른 스레드가 사용 중이라면 내 차례가 올 때까지 입구에서 비동기로 대기합니다.
            await 종목자물쇠.WaitAsync();

            try
            {
                int 대기횟수 = 0;
                int 최대대기횟수 = 50; // 100ms * 50 = 총 5초 대기

                // 3. 입구를 통과했으므로, 장부에 동일 종목의 "+++" 임시 주문이 있는지 검사
                while (대기횟수 < 최대대기횟수)
                {
                    int 임시주문갯수 = 0;

                    foreach (var 항목인자 in Form1.JumunItem_List.Values)
                    {
                        if (항목인자.종목코드 == 신규주문.종목코드 && 항목인자.주문번호 == "+++")
                        {
                            임시주문갯수++;
                        }
                    }

                    if (임시주문갯수 > 0)
                    {
                        await Task.Delay(100);
                        대기횟수++;
                    }
                    else
                    {
                        break;
                    }
                }

                // 4. 타임아웃 발생 시
                if (대기횟수 >= 최대대기횟수)
                {
                    Form1.Console_print($"[!] >> [타임아웃 발생] {신규주문.종목명} 5초간 번호 미수신. 강제로 장부 기록을 진행합니다.");
                }

                // 5. 키값 생성
                string 주문키 = 신규주문.주문번호;
                if (!string.IsNullOrEmpty(주문키) && 주문키[0] == '+')
                {
                    주문키 = $"{신규주문.종목코드}^{신규주문.Screennum}";
                }

                // =========================================================================
                // [+] 데이터 동기화: 장부에 넣기 직전 수익률과 등락률 스냅샷 고정
                // =========================================================================
                if (Form1.stockBalanceList.TryGetValue(신규주문.종목코드, out Stockbalance 잔고))
                {
                    // 보유 종목일 경우 내 잔고 데이터 기준
                    신규주문.수익률 = 잔고.수익률;
                    신규주문.등락률 = 잔고.등락율;
                }
                else
                {
                    // 완전 신규 매수일 경우 수익률은 0, 등락율은 시장 데이터 기준
                    신규주문.수익률 = 0;
                    if (Form1.Market_Item_List.TryGetValue(신규주문.종목코드, out Market_Item 시장데이터))
                    {
                        신규주문.등락률 = 시장데이터.등락율;
                    }
                }

                // 6. 메인 장부에 안전하게 추가
                Form1.JumunItem_List.TryAdd(주문키, 신규주문);
            }
            catch (Exception 예외)
            {
                Form1.Console_print($"[-] >> 장부 추가 중 치명적 오류 발생: {예외.Message}");
            }
            finally
            {
                // 7. 자물쇠 해제
                종목자물쇠.Release();
            }
        }


        public static JumunItem UpdateKey(string 주문번호, string Screennum, string 종목코드, JumunItem 입력주문)
        {
            // 1. 이미 새 주문번호(Key)로 등록된 게 있는지 1차 확인 (중복 체결 신호 방지)
            if (Form1.JumunItem_List.TryGetValue(주문번호, out JumunItem 이미_존재하는_주문))
            {
                return 이미_존재하는_주문; // 이미 처리되었으면 그거 리턴
            }

            // 2. 전달받은 객체가 있고, 아직 임시 주문번호("+++")인 경우
            if (입력주문 != null && 입력주문.주문번호 == "+++")
            {
                string 구_키값 = 종목코드 + "^" + Screennum;

                // 3. 딕셔너리에서 '구_키값'으로 데이터를 꺼내면서 동시에 삭제 (스레드 안전)
                if (Form1.JumunItem_List.TryRemove(구_키값, out JumunItem 꺼낸_주문))
                {
                    // 4. 내용물(Value)의 주문번호를 진짜 번호로 변경
                    꺼낸_주문.주문번호 = 주문번호;

                    // 5. 새로운 키(새 주문번호)로 딕셔너리에 다시 저장
                    Form1.JumunItem_List.TryAdd(주문번호, 꺼낸_주문);

                    감시번호_업데이트(꺼낸_주문, 주문번호);

                    return 꺼낸_주문; // 업데이트된 객체 반환
                }
            }

            // [지니 최적화] 불필요한 else if 중복 검색 블록 삭제 완료
            // 위에서 못 찾았고, 업데이트할 객체도 없다면 그냥 원래 들어온 입력주문(null일 수도 있음)을 그대로 반환
            return 입력주문;
        }

        public static void 감시번호_업데이트(JumunItem 주문항목, string 새_주문번호)
        {
            // 1. [최적화] Contains 대신 IndexOf >= 0 사용 (저사양 PC에서 미세하게 더 빠름)
            bool 감시모드 = 주문항목.검색식.IndexOf("[감시]") >= 0 || 주문항목.검색식.IndexOf("매도감시") >= 0;

            if (감시모드)
            {
                감시주문 찾은_감시 = null;

                if (주문항목.감시번호 > 0)
                {
                    // [최적화 2] ToString() 호출 최소화. 
                    // 딕셔너리 Key가 string이라면 어쩔 수 없지만, 가능하다면 int형 Key를 쓰는 게 좋음.
                    string 키값 = 주문항목.감시번호.ToString();
                    Form1.감시주문_List.TryGetValue(키값, out 찾은_감시);
                }
                else
                {
                    // [최적화 3] LINQ(.Values.FirstOrDefault) 제거 -> 순수 foreach 사용
                    // 메모리 할당(GC)을 0으로 만들어 렉을 방지함
                    foreach (var 감시항목 in Form1.감시주문_List.Values)
                    {
                        // [최적화 4] .Equals 대신 == 연산자 사용 (가장 빠름)
                        if (감시항목.원주문번호 == 주문항목.원주문번호)
                        {
                            찾은_감시 = 감시항목;
                            break; // 찾으면 즉시 탈출 (CPU 절약)
                        }
                    }
                }

                // 감시 주문을 찾았다면 정보 갱신
                if (찾은_감시 != null)
                {
                    찾은_감시.원주문번호 = 새_주문번호;
                    // [최적화 5] 파일 저장은 무거운 작업이므로 비동기로 처리하거나 
                    // 데이터가 정말 변했을 때만 호출되는지 확인 필요
                    SaveToFile.리밸감시주문_파일저장();
                }
            }
            else
            {
                // 일반 주문 처리
                if (주문항목.원주문번호.IndexOf("-") >= 0)
                {
                    // 미체결 그리드 삽입
                    GridView_Print.Outstanding_insert(주문항목, 0);
                }

                주문항목.원주문번호 = 새_주문번호;
            }
        }

        public static void Remove(JumunItem 삭제할주문)
        {
            //Form1.Console_print($"Remove: '{삭제할주문.종목명}'  종목코드 '{삭제할주문.종목코드}'  Screennum '{삭제할주문.Screennum}' JumunItem 제거");

      //      if (삭제할주문.Deletetimer < 0)
                삭제할주문.Deletetimer = 1;
        }

        public static void ExecuteDelete(JumunItem 삭제할주문)
        {
            //  Form1.Console_print($"ExecuteDelete: ");

            string 삭제할키 = 삭제할주문.주문번호;
            bool 삭제완료여부 = false;
            JumunItem 삭제된주문 = null;

            // [1차 시도] 진짜 주문번호 Key로 삭제
            if (!string.IsNullOrEmpty(삭제할키) && Form1.JumunItem_List.TryRemove(삭제할키, out 삭제된주문))
            {
                삭제완료여부 = true;
            }
            else
            {
                // [2차 시도] 임시 Key (종목코드^스크린번호)로 삭제
                string 임시키 = 삭제할주문.종목코드 + "^" + 삭제할주문.Screennum;
                if (Form1.JumunItem_List.TryRemove(임시키, out 삭제된주문))
                {
                    삭제완료여부 = true;
                }
                else
                {
                    // [3차 시도] Key가 완전히 꼬인 경우: 알맹이(Value)를 직접 뒤져서 찾아냅니다!
                    var 타겟쌍 = Form1.JumunItem_List.FirstOrDefault(x =>
                        x.Value == 삭제할주문 ||
                        (x.Value.종목코드 == 삭제할주문.종목코드 && x.Value.Screennum == 삭제할주문.Screennum && x.Value.매수매도 == 삭제할주문.매수매도)
                    );

                    // [최적화] 구조체의 기본값 확인은 Key != null 이 훨씬 안전합니다.
                    if (타겟쌍.Key != null)
                    {
                        if (Form1.JumunItem_List.TryRemove(타겟쌍.Key, out 삭제된주문))
                        {
                            삭제완료여부 = true;
                            Form1.Console_print($"[+] [3차 삭제성공] 잃어버린 Key({타겟쌍.Key})를 추적하여 '{삭제할주문.종목명}' 제거 성공");
                        }
                    }

                    // 3차 시도까지 했는데도 없으면 진짜로 리스트에 없는 겁니다.
                    if (!삭제완료여부)
                    {
                        Form1.Console_print($"[-] [삭제 실패/무시] '{삭제할주문.종목명}' -> 이미 삭제되었거나 리스트에 존재하지 않습니다. (요청키: {삭제할키} / {임시키})");
                    }
                }
            }
        }


    



    }


    public class JumunItem
    {
        // [1] 중요한 정보들을 맨 위로 올렸어 (저장 시 가독성 UP)
        public bool 신용주문 { get; set; }
        public string 대출일 { get; set; }

        public string 종목명 { get; set; }
        public string 종목코드 { get; set; }
        public string 주문번호 { get; set; }
        public string 원주문번호 { get; set; }

        // [2] 나머지 변수들
        public int Deletetimer { get; set; }
        public string Screennum { get; set; }
        public string 검색식 { get; set; }
        public double 주문값 { get; set; }
        public int 시장가구분 { get; set; }
        public int 취소시간 { get; set; }
        public int 취소N주문 { get; set; }
        public int 반복횟수 { get; set; }
        public string 비고 { get; set; }
        public string Pos { get; set; }
        public int 주문수량 { get; set; }
        public int 주문가격 { get; set; }
        public int 매수매도 { get; set; }
        public double 비중 { get; set; }
        public int 비중단위 { get; set; }
        public int 취소timer { get; set; }
        public int 현재가 { get; set; }
        public double 등락률 { get; set; }
        public int 주문시간 { get; set; }
        public int 미체결량 { get; set; }
        public bool 주문취소 { get; set; }
        public bool 가동전 { get; set; }
        public int Tik_cap { get; set; }
        public int Tik_price { get; set; }
        public double 수익률 { get; set; }
        public bool 주문동기화 { get; set; }
        public int 감시번호 { get; set; }
        public int Order번호 { get; set; }
        public int 수익구분 { get; set; }
        public bool NXT { get; set; }
        public long 주문시간_Ticks { get; set; }

        // [3] 생성자는 이거 하나만 있으면 돼! (필수)
        public JumunItem() { }
    }



}
 
