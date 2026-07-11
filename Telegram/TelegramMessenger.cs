using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Exceptions; // 이 네임스페이스 추가 필요

namespace 지니64
{
    class TelegramMessenger : Form1
    {
        public static void Telegram_alram(string 요청타입)
        {
            if (릴리즈)
            {
                // [최적화 1] UI 값은 메인 스레드에서 미리 가져오거나 Invoke로 가져와야 함
                // 백그라운드 작업 큐에 넣기 전에 필요한 메시지 데이터를 미리 생성하는 것이 안전합니다.
                string 메시지본문 = "";

                // UI 접근은 반드시 Invoke 사용
                Helper.안전한_UI_업데이트(form1, () =>
                {
                    long 예수금 = Acc.D2 + Acc.매입금;
                    long 실현손익 = Acc.실현손익;
                    long 평가예수금 = Acc.D2 + Acc.평가금;

                    if (평가예수금 < 예수금) 예수금 = 평가예수금;

                    // 공통 메시지 포맷 생성
                    메시지본문 = $"투자원금: {form1.MT_투자원금.Text} 증가자산: {form1.TB_증가자산.Text} " +
                                  $"당일실현손익: {form1.TB_실현손익.Text} 예수금: {예수금:N0} " +
                                  $"라이센스: {form1.LB_license.Text} 남은기간: {form1.LB_남은기간.Text}";
                });

                // 비동기 큐 작업 시작
                UnifiedDataManager.Instance.Writing.Enqueue(() =>
                {
                    try
                    {
                        string 동작구분 = "";
                        bool 관리자전송여부 = false;
                        bool 사용자전송여부 = false;

                        // [최적화 2] 로직 단순화
                        if (요청타입.Equals("logIn"))
                        {
                            동작구분 = "Log IN";
                            관리자전송여부 = !GenieConfig.TB_Chat_ID.Equals("6194572746");
                        }
                        else if (요청타입.Equals("logOut"))
                        {
                            동작구분 = "Log Out";
                            관리자전송여부 = !GenieConfig.TB_Chat_ID.Equals("6194572746");
                        }
                        else if (요청타입.Equals("logIn_user"))
                        {
                            동작구분 = "Log IN";
                            사용자전송여부 = GenieConfig.CB_텔레그램사용;
                        }
                        else if (요청타입.Equals("logOut_user"))
                        {
                            동작구분 = "Log Out";
                            사용자전송여부 = GenieConfig.CB_텔레그램사용;
                        }

                        if (!string.IsNullOrEmpty(동작구분))
                        {
                            string 최종메시지 = $"{DateTime.Now:HH:mm:ss} [{GenieConfig.textBox_ID} _{server} _{GenieConfig.textBox_계좌번호}] :: {프로그램명} {버전_디버그} {동작구분} {메시지본문}";

                            HashSet<string> 전송대상자들 = new HashSet<string>();

                            if (관리자전송여부)
                            {
                                전송대상자들.Add(form1.telegram_ChatID);
                                form1.Telegram_Bot = new Telegram.Bot.TelegramBotClient(form1.Telegram_Token);
                                _ = Telegram_Message(최종메시지, 전송대상자들).ConfigureAwait(false);
                            }
                            else if (사용자전송여부)
                            {
                                전송대상자들.Add(GenieConfig.TB_Chat_ID);
                                form1.Telegram_Bot = new Telegram.Bot.TelegramBotClient(GenieConfig.TB_token);
                                _ = Telegram_Message(최종메시지, 전송대상자들).ConfigureAwait(false);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console_print($"Telegram_alram 오류: {ex.Message}");
                    }
                });
            }
        }

        public static async Task Telegram_Send(string message)
        {
            string fullMessage = DateTime.Now.ToString("HH:mm:ss ::") + message +
                                 " [ " + GenieConfig.textBox_ID + " _" + server + " _" +
                                 GenieConfig.textBox_계좌번호 + "]";

            // [+] 봇 객체 초기화 (안 되어 있을 경우를 대비)
            if (form1.Telegram_Bot == null)
            {
                form1.Telegram_Bot = new Telegram.Bot.TelegramBotClient(GenieConfig.TB_token);
            }

            HashSet<string> 전송대상자들 = new HashSet<string>();
            전송대상자들.Add(GenieConfig.TB_Chat_ID); // 기본 사용자 ID 추가

            await Telegram_Message(fullMessage, 전송대상자들);
        }

        public static async Task Telegram_Message(string message, HashSet<string> users)
        {
            // 1. 전달받은 대상이 없을 경우 기본 사용자 세팅
            if (users == null || users.Count == 0)
            {
                if (users == null)
                {
                    users = new HashSet<string>();
                }

                users.Add(GenieConfig.TB_Chat_ID); // 사용자 ID 강제 추가

                // 객체가 비어있을 때만 새로 생성하여 메모리 낭비 방지
                if (form1.Telegram_Bot == null)
                {
                    form1.Telegram_Bot = new Telegram.Bot.TelegramBotClient(GenieConfig.TB_token);
                }
            }

            // 2. 텔레그램 봇 객체 유효성 검사
            if (form1.Telegram_Bot == null)
            {
                HandleTelegramError(); // 오류 처리 로직
                return;
            }

            // 3. 비동기 전송 작업 목록 생성
            List<Task> 전송작업리스트 = new List<Task>();

            foreach (var 대상 in users)
            {
                전송작업리스트.Add(SendMessageWithRetryAsync(대상, message));
            }

            // 4. 전송 실행
            try
            {
                await Task.WhenAll(전송작업리스트);
            }
            catch (Exception 예외)
            {
                // 실제 운용 중 문제가 생겼을 때만 최소한의 에러 로그 출력
                Console_print($"[-] >> 텔레그램 메시지 전송 중 오류 발생: {예외.Message}");
            }
        }

        private static async Task SendMessageWithRetryAsync(string chatId, string message)
        {
            const int maxRetries = 3;
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                try
                {
                    if (form1 == null || form1.Telegram_Bot == null)
                        throw new InvalidOperationException("Telegram Bot 객체 오류");

                    await form1.Telegram_Bot.SendMessage(chatId, message).ConfigureAwait(false);
                    return; // 성공 시 종료
                }
                catch (ApiRequestException apiEx)
                {
                    // 🚨 핵심 수정: 텔레그램 API가 명확한 거절 의사를 보낸 경우 (400 에러)
                    // Chat not found는 ErrorCode 400입니다.
                    if (apiEx.ErrorCode == 400)
                    {
                        Console_print($"[치명적 오류] Chat ID {chatId}가 존재하지 않거나 봇을 시작하지 않았습니다. 재시도 중단.");
                    }

                    retryCount++;
                    Console_print($"API 전송 오류 (시도 {retryCount}/{maxRetries}): {apiEx.Message}");
                }
                catch (Exception ex) // 그 외 일반적인 에러 (네트워크 끊김 등)
                {
                    retryCount++;
                    Console_print($"전송 실패 (시도 {retryCount}/{maxRetries}): {ex.Message}");
                }

                // 마지막 시도 실패 시
                if (retryCount >= maxRetries)
                {
                    HandleTelegramError();
                    return;
                }

                int delaySeconds = (int)Math.Pow(2, retryCount) * 1000;
                await Task.Delay(delaySeconds).ConfigureAwait(false);
            }
        }

        // 📌 기존 코드의 오류 처리 및 설정 초기화 로직을 분리
        private static void HandleTelegramError()
        {
            AutoClosingAlram("Telegram Bot과 대화를 시작 하지 않았거나 Chat_ID 또는 Token 이 잘못되었습니다.", "텔레그램 연결에러", 10, "에러");

            // 오류 발생 시 경고 알림 기능을 끕니다.
            GenieConfig.CB_텔레그램사용 = false;
            GenieConfig.CB_매수알림 = false;
            GenieConfig.CB_매도알림 = false;

            // [지니 최적화] 중복 코드 제거 및 스레드 안전 업데이트
            Helper.안전한_UI_업데이트(Form_Function.form, () =>
            {
                // 이 안쪽은 스레드 상관없이 안전하게 실행됩니다.
                Form_Function.form.CB_텔레그램사용.Checked = false;
                Form_Function.form.CB_매수알림.Checked = false;
                Form_Function.form.CB_매도알림.Checked = false;
            });
        }
    }
}

