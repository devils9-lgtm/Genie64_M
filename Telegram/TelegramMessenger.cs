using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;

namespace 지니_64
{
    class TelegramMessenger
    {
        public static void Telegram_alram(string T)
        {
            if (Form1.릴리즈)
            {
                Task task = new Task(() =>
                {
                    try
                    {
                        long 예수금 = Form1.Acc[0].D2 + Form1.Acc[0].매입금;
                        long 실현손익 = Form1.Acc[0].실현손익;
                        long 평가예수금 = Form1.Acc[0].D2 + Form1.Acc[0].평가금;

                        if (평가예수금 < 예수금)
                            예수금 = 평가예수금;

                        if (T.Equals("logIn"))
                        {
                            string Message = DateTime.Now.ToString("HH:mm:ss [") + Form1.USER_ID + " _" + Form1.server + " _" + Properties.Settings.Default.select_account + "] :: " + Form1.프로그램명 + " " + Form1.버전 + " Log IN " + " 투자원금: " + Form1.form1.MT_principal.Text + " 증가자산: " + Form1.form1.TB_증가자산.Text + " 당일실현손익: " + Form1.form1.TB_실현손익.Text + " 예수금: " + 예수금.ToString("N0") + " 라이센스: " + Form1.form1.LB_license.Text + " 남은기간: " + Form1.form1.LB_남은기간.Text;

                            if (!Properties.Settings.Default.TB_Chat_ID.Equals("6194572746"))
                            {
                                Form1.form1.Telegram_users.Clear();
                                Form1.form1.Telegram_users.Add(Form1.form1.telegram_ChatID);

                                Form1.form1.Telegram_Bot = new Telegram.Bot.TelegramBotClient(Form1.form1.Telegram_Token);
                                Telegram_Message(Message, Form1.form1.Telegram_users);
                            }
                        }

                        if (T.Equals("logOut"))
                        {
                            string Message = DateTime.Now.ToString("HH:mm:ss [") + Form1.USER_ID + " _" + Form1.server + " _" + Properties.Settings.Default.select_account + "] :: " + Form1.프로그램명 + " " + Form1.버전 + " Log Out" + " 투자원금: " + Form1.form1.MT_principal.Text + " 증가자산: " + Form1.form1.TB_증가자산.Text + " 당일실현손익: " + Form1.form1.TB_실현손익.Text + " 예수금: " + 예수금.ToString("N0") + " 라이센스: " + Form1.form1.LB_license.Text + " 남은기간: " + Form1.form1.LB_남은기간.Text;

                            if (!Properties.Settings.Default.TB_Chat_ID.Equals("6194572746"))
                            {
                                Form1.form1.Telegram_users.Clear();
                                Form1.form1.Telegram_users.Add(Form1.form1.telegram_ChatID);

                                Form1.form1.Telegram_Bot = new Telegram.Bot.TelegramBotClient(Form1.form1.Telegram_Token);
                                Telegram_Message(Message, Form1.form1.Telegram_users);
                            }
                        }

                        if (T.Contains("_user"))
                        {
                            if (T.Equals("logIn_user"))
                            {
                                string Message = DateTime.Now.ToString("HH:mm:ss [") + Form1.USER_ID + " _" + Form1.server + " _" + Properties.Settings.Default.select_account + "] :: " + Form1.프로그램명 + " " + Form1.버전 + " Log IN " + " 투자원금: " + Form1.form1.MT_principal.Text + " 증가자산: " + Form1.form1.TB_증가자산.Text + " 당일실현손익: " + Form1.form1.TB_실현손익.Text + " 예수금: " + 예수금.ToString("N0") + " 라이센스: " + Form1.form1.LB_license.Text + " 남은기간: " + Form1.form1.LB_남은기간.Text;

                                if (Properties.Settings.Default.CB_텔레그램사용)
                                {
                                    Form1.form1.Telegram_users.Clear();
                                    Form1.form1.Telegram_users.Add(Properties.Settings.Default.TB_Chat_ID);

                                    Form1.form1.Telegram_Bot = new Telegram.Bot.TelegramBotClient(Properties.Settings.Default.TB_token);
                                    Telegram_Message(Message, Form1.form1.Telegram_users);
                                }
                                else
                                {
                                    Form1.form1.Telegram_users.Clear();
                                }
                            }

                            if (T.Equals("logOut_user"))
                            {
                                string Message = DateTime.Now.ToString("HH:mm:ss [") + Form1.USER_ID + " _" + Form1.server + " _" + Properties.Settings.Default.select_account + "] :: " + Form1.프로그램명 + " " + Form1.버전 + " Log Out" + " 투자원금: " + Form1.form1.MT_principal.Text + " 증가자산: " + Form1.form1.TB_증가자산.Text + " 당일실현손익: " + Form1.form1.TB_실현손익.Text + " 예수금: " + 예수금.ToString("N0") + " 라이센스: " + Form1.form1.LB_license.Text + " 남은기간: " + Form1.form1.LB_남은기간.Text;

                                if (Properties.Settings.Default.CB_텔레그램사용)
                                {
                                    Form1.form1.Telegram_users.Clear();
                                    Form1.form1.Telegram_users.Add(Properties.Settings.Default.TB_Chat_ID);

                                    Form1.form1.Telegram_Bot = new Telegram.Bot.TelegramBotClient(Properties.Settings.Default.TB_token);
                                    Telegram_Message(Message, Form1.form1.Telegram_users);
                                }
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Telegram_alram 서버오프");
                    }
                });
                Form1.writing_Manager.RequestTrData(task);
            }
        }


        public static void Telegram_Send(string message)
        {
            Task task = new Task(() =>
            {
                Form1.form1.Invoke((MethodInvoker)delegate ()  //      
                {
                    Telegram_Message(DateTime.Now.ToString("HH:mm:ss ::") + message + " [ " + Form1.USER_ID + " _" + Form1.server + " _" + Properties.Settings.Default.select_account + "]", Form1.form1.Telegram_users);
                });
            });
            Form1.writing_Manager.RequestTrData(task);
        }

#pragma warning disable 1998
        public static async void Telegram_Message(string message, List<string> users)
        {
            if (Form1.form1.Telegram_Bot != null)
            {
                Parallel.ForEach(users, async t =>
                {
                    try
                    {
                        await Task.Run(() => Form1.form1.Telegram_Bot.SendTextMessageAsync(t, message));
                    }
                    catch
                    {
                        Form1.AutoClosingAlram("Telegram Bot과 대화를 시작 하지 않았거나 Chat_ID 또는 Token 이 잘못되었습니다.", "텔레그램 연결에러", 10, "에러");

                        Properties.Settings.Default.CB_텔레그램사용 = false;
                        Properties.Settings.Default.CB_매수알림 = false;
                        Properties.Settings.Default.CB_매도알림 = false;
                        Properties.Settings.Default.Save();
                    }
                });
            }
            else
            {
                Form1.AutoClosingAlram("Telegram Bot과 대화를 시작 하지 않았거나 Chat_ID 또는 Token 이 잘못되었습니다.", "텔레그램 연결에러", 10, "에러");

                Properties.Settings.Default.CB_텔레그램사용 = false;
                Properties.Settings.Default.CB_매수알림 = false;
                Properties.Settings.Default.CB_매도알림 = false;
                Properties.Settings.Default.Save();
            }
        }
    }
}
