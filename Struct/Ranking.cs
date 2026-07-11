using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64
{
    internal class Ranking
    {
        // 💡 [로그 스위치] true 면 모든 과정을 출력, false 면 조용히 결과만 전송!
        public static bool 로그활성화 = false;

        public static ConcurrentDictionary<string, 랭킹종목정보> 통합랭킹딕셔너리 = new ConcurrentDictionary<string, 랭킹종목정보>();
        public static List<랭킹종목정보> 등락률상위_List = new List<랭킹종목정보>();
        public static List<랭킹종목정보> 거래대금상위_List = new List<랭킹종목정보>();
        public static HashSet<string> 이전주도주목록 = new HashSet<string>();
        public static int 응답완료카운트 = 0;

        // 1. 요청 전 모든 깃발 내리기 및 리스트 비우기
        public static void 플래그_리셋()
        {
            // [핵심 수정] 리스트를 반드시 비워줘야 새로운 랭킹 데이터만 담깁니다.
            등락률상위_List.Clear();
            거래대금상위_List.Clear();

            // 딕셔너리는 데이터를 유지하되(캐싱), 상태값만 초기화합니다.
            foreach (var 종목 in 통합랭킹딕셔너리.Values)
            {
                종목.거래대금순위권 = false;
                종목.등락률순위권 = false;
                종목.조회순위 = 0; // 0으로 리셋해서 순위권 밖임을 표시
            }
        }

        // 인자: 상위N등까지 (필터링 후 남은 것 중에서 상위 몇 개를 쓸지)
        public static async void 랭킹분석시작()
        {
            int 상위N등까지 = 1;

            // 💡 [수정] 무조건 출력되던 진단 로그를 다시 스위치(로그활성화) 안으로 집어넣어 조용하게 만듭니다.
            if (로그활성화)
            {
                Form1.Console_print($"\n[시스템] 랭킹분석시작() 함수 호출됨. 데이터 요청을 시작합니다.");
                Form1.Console_print($"====== [ 지니 랭킹 분석: 120일 돌파 & 전체 명단 로그 모드 ] ======");
            }

            Ranking.플래그_리셋();
            Ranking.응답완료카운트 = 0;
            string 현재시간 = DateTime.Now.ToString("HHmmss");
            string 검색식 = "랭킹분석";

            // [단계 1~2] 데이터 요청 및 대기
            Rankinginfo.거래대금상위요청(false);
            await Task.Delay(50);
            Rankinginfo.전일대비등락률상위요청(false);
            await Task.Delay(50);
            Rankinginfo.실시간종목조회순위(false);

            int timeout = 0;
            while (Ranking.응답완료카운트 < 3)
            {
                await Task.Delay(100);
                if (++timeout > 50)
                {
                    Form1.Console_print($"[에러 발생] 랭킹 데이터 수신 타임아웃! (현재 카운트: {Ranking.응답완료카운트}/3) -> 분석을 중단합니다.");
                    return;
                }
            }

            if (로그활성화) Form1.Console_print($"[진행] 랭킹 데이터 3종 수신 완료! (필터링 분석 진입)");

            // =================================================================================
            // [디버그 로그] 명단 전체 출력 (등락 -> 대금 -> 순위)
            // =================================================================================
            var 등락률_List = Ranking.등락률상위_List.Take(100).ToList();
            var 거래대금_List = Ranking.거래대금상위_List.Take(35).ToList();
            var 조회순위_List = Ranking.통합랭킹딕셔너리.Values
                .Where(x => x.조회순위 > 0 && x.조회순위 <= 5)
                .OrderBy(x => x.조회순위).ToList();

            if (로그활성화)
            {
                Form1.Console_print($"\n--- [1. 전일대비 등락률 Top 100 ({등락률_List.Count}개)] ---");
                if (등락률_List.Count > 0)
                {
                    string logBuffer = "";
                    for (int i = 0; i < 등락률_List.Count; i++)
                    {
                        logBuffer += $"{등락률_List[i].종목명}({등락률_List[i].종목코드.Trim()}) ";
                        if ((i + 1) % 5 == 0) { Form1.Console_print(logBuffer); logBuffer = ""; }
                    }
                    if (!string.IsNullOrEmpty(logBuffer)) Form1.Console_print(logBuffer);
                }

                Form1.Console_print($"\n--- [2. 거래대금 상위 Top 35 ({거래대금_List.Count}개)] ---");
                foreach (var item in 거래대금_List)
                {
                    Form1.Console_print($"[{거래대금_List.IndexOf(item) + 1}위] {item.종목명}({item.종목코드.Trim()}) - {item.거래대금:N0}백만");
                }

                Form1.Console_print($"\n--- [3. 실시간 검색 Top 5 ({조회순위_List.Count}개)] ---");
                foreach (var item in 조회순위_List)
                {
                    Form1.Console_print($"[{item.조회순위}위] {item.종목명}({item.종목코드.Trim()})");
                }

                Form1.Console_print($"\n--- [탈락 로그: 1차 거래대금/등락률/ETF 필터] ---");
            }

            // =================================================================================
            // [단계 3] 필터링 데이터 준비 및 1차 탈락 로그
            // =================================================================================
            var 등락률_Set = 등락률_List.Select(x => x.종목코드.Trim()).ToHashSet();
            var 조회순위_Set = 조회순위_List.Select(x => x.종목코드.Trim()).ToHashSet();

            var 후보리스트 = new List<랭킹종목정보>();

            for (int i = 0; i < 거래대금_List.Count; i++)
            {
                var item = 거래대금_List[i];
                string code = item.종목코드.Trim();

                bool 등락률100위_조건 = 등락률_Set.Contains(code);
                bool 거래대금5위_조건 = (i < 5);

                if (등락률100위_조건 || 거래대금5위_조건)
                {
                    if (Ranking.통합랭킹딕셔너리.TryGetValue(code, out var 원본))
                    {
                        bool isEtf = (원본.시장 == "E");
                        if (Form1.Market_Item_List.TryGetValue(code, out var info))
                        {
                            if (info.Market == "ETF" || info.Market == "ETN") isEtf = true;
                        }

                        if (!isEtf)
                        {
                            후보리스트.Add(원본);
                        }
                        else
                        {
                            if (로그활성화) Form1.Console_print($"[탈락] {원본.종목명}({code}): ETF/ETN 종목 제외");
                        }
                    }
                }
                else
                {
                    if (로그활성화) Form1.Console_print($"[탈락] {item.종목명}({code}): 등락률 100위 밖 & 거래대금 5위 밖");
                }
            }

            // [단계 4] 차트 데이터 요청 및 수신 대기
            if (로그활성화) Form1.Console_print($"\n-> {후보리스트.Count}종목 120일 차트 검증 시작...");
            foreach (var 종목 in 후보리스트)
            {
                if (종목.종가120일최고 > 0) continue;
                TR_요청.주식일봉차트조회요청(종목.종목코드.Trim(), false);
                await Task.Delay(150);
            }

            timeout = 0;
            while (timeout < 50 && 후보리스트.Any(x => x.종가120일최고 == 0)) { await Task.Delay(100); timeout++; }

            // =================================================================================
            // [단계 5] 상세 필터링 (양봉 + 120일 신고가) 및 2차 탈락 로그
            // =================================================================================
            if (로그활성화) Form1.Console_print($"\n--- [탈락 로그: 2차 양봉/신고가/이상감지 필터] ---");

            var 필터링된리스트 = new List<랭킹종목정보>();
            foreach (var x in 후보리스트.OrderByDescending(j => j.거래대금))
            {
                bool 양봉조건 = x.현재가 >= x.시가;
                bool 신고가조건 = x.현재가 >= x.종가120일최고;
                bool 이상감지없음 = !x.최근10일이내이상감지;

                if (양봉조건 && 신고가조건 && 이상감지없음)
                {
                    필터링된리스트.Add(x);
                }
                else
                {
                    if (로그활성화)
                    {
                        List<string> 탈락사유 = new List<string>();
                        if (!양봉조건) 탈락사유.Add("음봉");
                        if (!신고가조건) 탈락사유.Add($"120일선 미돌파(현재가:{x.현재가}/120최고:{x.종가120일최고})");
                        if (!이상감지없음) 탈락사유.Add("최근 10일 이상감지");

                        Form1.Console_print($"[탈락] {x.종목명}({x.종목코드.Trim()}): {string.Join(", ", 탈락사유)}");
                    }
                }
            }

            // =================================================================================
            // [단계 6] 폭포수 필터링 및 3차 탈락 로그
            // =================================================================================
            if (로그활성화) Form1.Console_print($"\n--- [탈락 로그: 3차 실시간 검색순위 필터] ---");

            var 최종합격리스트 = new List<랭킹종목정보>();
            foreach (var 종목 in 필터링된리스트)
            {
                if (조회순위_Set.Contains(종목.종목코드.Trim()))
                {
                    최종합격리스트.Add(종목);
                }
                else
                {
                    if (로그활성화) Form1.Console_print($"[탈락] {종목.종목명}({종목.종목코드.Trim()}): 조회순위 Top 5 밖 (폭포수 컷 적용, 하위 종목 검사 중단)");
                    continue;
                }
            }

            // =================================================================================
            // [단계 7] 최종 인원 컷 및 4차 탈락 로그
            // =================================================================================
            if (상위N등까지 > 0 && 최종합격리스트.Count > 상위N등까지)
            {
                if (로그활성화)
                {
                    Form1.Console_print($"\n--- [탈락 로그: 4차 최종 정원 초과] ---");
                    var 잘린종목들 = 최종합격리스트.Skip(상위N등까지).ToList();
                    foreach (var 종목 in 잘린종목들)
                    {
                        Form1.Console_print($"[탈락] {종목.종목명}({종목.종목코드.Trim()}): 최종 정원({상위N등까지}명) 초과로 제외");
                    }
                }

                최종합격리스트 = 최종합격리스트.Take(상위N등까지).ToList();
            }

            // =================================================================================
            // [단계 8] 최종 결과 출력 및 신호 전송
            // =================================================================================
            var 현재코드목록 = 최종합격리스트.Select(x => x.종목코드.Trim()).ToHashSet();
            foreach (var 이전코드 in 이전주도주목록)
                if (!현재코드목록.Contains(이전코드)) await 지니_신호_전송("D", 이전코드, 검색식, 현재시간);

            // 💡 [추가] 최종 선정된 종목이 하나도 없을 때 알려주는 기능
            if (true)//로그활성화)
            {
                if (최종합격리스트.Count == 0)
                {
                    Form1.Console_print($"\n====== [랭킹분석 최종 선정 : {최종합격리스트.Count}선 ] ======");
                }
                else
                {
                    Form1.Console_print($"\n");

                  //  Form1.Console_print($"\n====== [랭킹분석 최종 선정 : {최종합격리스트.Count}선 ] ======");
                }
            }


            int 순번 = 1;
            foreach (var 종목 in 최종합격리스트)
            {
                string cleanCode = 종목.종목코드.Trim();

                int 등락순위 = Ranking.등락률상위_List.FindIndex(x => x.종목코드.Trim() == cleanCode) + 1;
                int 대금순위 = Ranking.거래대금상위_List.FindIndex(x => x.종목코드.Trim() == cleanCode) + 1;

                REG.실시간시세등록(cleanCode);
              await  지니_신호_전송("I", cleanCode, 검색식, 현재시간);

                double 전고점비율 = 0;
                if (종목.종가120일최고 > 0) 전고점비율 = (double)종목.현재가 / 종목.종가120일최고 * 100;

                // 💡 이전에 강제로 true로 열어두셨던 부분을 다시 로그활성화 조건으로 되돌렸습니다.

                if (true)//로그활성화)
                {
                    Form1.Console_print($"====== [랭킹분석 최종 선정 : {최종합격리스트.Count}선 ] {순번++}위 {종목.종목명} ======");
                }

                if (로그활성화)
                {
                    Form1.Console_print($"[{순번++}위] {종목.종목명}({cleanCode})\n" +
                                         $"   ▶ 순위: [조회 {종목.조회순위}위] / [등락 {등락순위}위] / [대금 {대금순위}위]\n" +
                                         $"   ▶ 대금: {종목.거래대금:N0}백만 / 120일최고: {종목.종가120일최고:N0}\n" +
                                         $"   ▶ 현재상태: {종목.현재가:N0} (돌파율: {전고점비율:F1}%)");
                }
            }
            이전주도주목록 = 현재코드목록;
        }

        // 지니_신호_전송 함수 (생략 없이 유지)
        private static async Task 지니_신호_전송(string 구분, string 코드, string 검색식, string 시간)
        {
          await  Tab_Basic.New_Buy(구분, 코드, 검색식);
            Tab_Watch.Watch_In_Out(구분, 코드, 검색식, 시간);
            Tab_Repeat.Repeat_condition(구분, 코드, 검색식);
            Tab_AccountManagement.Rebalancing_condition(구분, 코드, 검색식);
            Tab_AccountManagement.Liquidation_condition(구분, 코드, 검색식);
            Condition_Management.SearchView_add(구분, 코드, 검색식, 시간);
        }

    }

    public class 랭킹종목정보
    {
        public bool 최근10일이내이상감지 { get; set; }
        public string 종목코드 { get; set; }
        public string 종목명 { get; set; }
        public string 시장 { get; set; }
        public int 전일종가 { get; set; }
        public int 현재가 { get; set; }
        public int 시가 { get; set; }
        public double 등락률 { get; set; }
        public long 거래대금 { get; set; }
        public int 종가120일최고 { get; set; }
        public int 현재순위 { get; set; }
        public int 조회순위 { get; set; }

        public bool 거래대금순위권 { get; set; }
        public bool 등락률순위권 { get; set; }

        public void 데이터갱신(string 현재가문자, string 전일대비문자, string 등락률문자, string 대금문자, int 순위, bool 대금기준)
        {
            this.현재가 = Math.Abs(int.Parse(현재가문자));

            if (Form1.Market_Item_List.TryGetValue(this.종목코드, out var itemInfo))
            {
                this.시장 = itemInfo.Market;
                this.전일종가 = itemInfo.Last_price;
            }
            else
            {
                int 파싱된전일대비 = int.Parse(전일대비문자);
                this.전일종가 = this.현재가 - 파싱된전일대비;
                this.시장 = "E";
            }

            if (대금기준)
            {
                this.거래대금 = long.Parse(대금문자);
                this.현재순위 = 순위;
                this.거래대금순위권 = true;
            }
            else
            {
                this.등락률 = double.Parse(등락률문자);
                this.등락률순위권 = true;
            }
        }

        public void 조회순위갱신(int 순위)
        {
            this.조회순위 = 순위;
        }
    }
}