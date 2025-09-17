using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace 지니_64
{
    public class timer
    {
        public static void Tr_timer() //  Thread.Sleep(250);
        {
            int 취소 = 0;
            int 주문 = 0;

            string Time = DateTime.Now.ToLongTimeString();

            Form1.timenow = int.Parse(DateTime.Now.ToString("HHmmss"));
            if (Form1.form1.CB_미니시계.Checked)
            {
                Form1.form1.label_time.Text = Time;

                //    Form1.form1.label_time.Text = Form1.timenow.ToString("##:##:##");
            }

            for (int i = 0; i < Form1.Order_list.Count; i++)
            {
                if (Form1.Order_list[i].주문유형.Equals(3) || Form1.Order_list[i].주문유형.Equals(4))
                {
                    취소++;
                }
                else
                {
                    주문++;
                }
            }

            List<JumunItem> 신규주문중Item = Form1.JumunItem_List.FindAll(o => o.주문번호.Contains("+"));


            Form1.form1.Text = Form1.프로그램명 + " " + Form1.버전 + " / " + "***" + Form1.USER_ID.Substring(3) + " / " + Form1.요일 + " / [ " + Time + " ] / " + Form1.server + " / " + Form1.server_알림 + " / 검색식 Run: " + Form1.Run_condition_List.Count +
            "개 / 주문 count 초당- " + Form1.주문_S + "회 분당- " + Form1.주문_M + "회 시간당- " + Form1.주문_H + "회 ( ※ 키움서버 주문제한 : 시간당 3600회 입니다.) 사용기간: " + Form1.사용기간 + " 까지";

            box.Form_Outstanding.form.LB_미체결주문.Text = "미체결 주문:  " + Form1.JumunItem_List.Count() + " 건";


            Form1.timer_S++;
            Form1.timer_M++;
            Form1.timer_H++;

            if (Form1.timer_S.Equals(4))
            {
                Form1.timer_S = 0;
                Form1.주문_S = 0;
            }
            if (Form1.timer_M.Equals(240))
            {
                Form1.timer_M = 0;
                Form1.주문_M = 0;


            }
            if (Form1.timer_H.Equals(14400))
            {
                Form1.timer_H = 0;
                Form1.주문_H = 0;
            }

            if (!DateTime.Now.ToString("ss").Equals("00")) Form1.분_180 = 0;
            if (int.Parse(DateTime.Now.ToString("mm")) % Form1.분_time == 0 && DateTime.Now.ToString("ss").Equals("00"))
            {
                if (Form1.분_180 == 0)
                {
                    //    RealData_Management.Moving_Average(null);
                    MA.Mma_Record(null);

                    if (Form1.분_count < 20)
                    {
                        Form1.분_count++;
                        if (Form1.분_count == 20) Form1.분_time = 3;
                    }
                }
                Form1.분_180++;
            }

            int out_count = Form1.JumunItem_List.Count;

            if (3600 <= (Form1.주문_H + 20 + out_count)) // 주문과다 재시작 
            {
                if (Form1.재시작)
                {
                    Form1.재시작가동("주문과다", "시간당주문 (" + Form1.주문_H + ") + 미체결주문(" + out_count + ")이\n3600건을 넘어 재시작 됩니다.");
                }
            }
        }


        ////////////////////////////    카운터쓰레드    /// ///////////////////////
        public static void delay_timer() // 1 초 타이머
        {
            if (Form1.kosdaq_avg_min.Count > 0  && Form1.server_알림.Contains("메인마켓")) 
            {
                string date = DateTime.Now.ToString("HHmmss");
                if (date.Substring(4).Equals("00"))
                {
                    if (Form1.kos_avg_update)
                    {
                        Form1.kos_avg_update = false;
                        Form1.kospi_avg_min.Insert(0, new AVG_price("001", Form1.Acc[0].피_현재가, date));
                        Form1.kosdaq_avg_min.Insert(0, new AVG_price("101", Form1.Acc[0].닥_현재가, date));
                        Form1.kospi_avg_min.RemoveAt(Form1.kospi_avg_min.Count - 1);
                        Form1.kosdaq_avg_min.RemoveAt(Form1.kosdaq_avg_min.Count - 1);
                    }
                }
                else
                {
                    Form1.kos_avg_update = true;
                }
            }

            if (Properties.Settings.Default.MTB_new_delay_A > 0)
            {
                for (int i = 0; i < Form1.NewCatch_List_A.Count; i++)
                {
                    if (Form1.NewCatch_List_A[i].state.Contains("진입"))
                    {
                        if (Form1.NewCatch_List_A[i].timer < Form1.maximum_time)
                            Form1.NewCatch_List_A[i].timer++;

                        if (Properties.Settings.Default.MTB_new_delay_A <= Form1.NewCatch_List_A[i].timer)
                        {
                            Tab_Basic.매수검색식_A(Form1.NewCatch_List_A[i]);
                        }
                    }
                }
            }

            if (Properties.Settings.Default.MTB_new_delay_B > 0)
            {
                for (int i = 0; i < Form1.NewCatch_List_B.Count; i++)
                {
                    if (Form1.NewCatch_List_B[i].state.Contains("진입"))
                    {
                        if (Form1.NewCatch_List_B[i].timer < Form1.maximum_time)
                            Form1.NewCatch_List_B[i].timer++;

                        if (Properties.Settings.Default.MTB_new_delay_B <= Form1.NewCatch_List_B[i].timer)
                        {
                            Tab_Basic.매수검색식_B(Form1.NewCatch_List_B[i]);
                        }
                    }
                }
            }

            if (Properties.Settings.Default.MTB_new_delay_C > 0)
            {
                for (int i = 0; i < Form1.NewCatch_List_C.Count; i++)
                {
                    if (Form1.NewCatch_List_C[i].state.Contains("진입"))
                    {
                        if (Form1.NewCatch_List_C[i].timer < Form1.maximum_time)
                            Form1.NewCatch_List_C[i].timer++;

                        if (Properties.Settings.Default.MTB_new_delay_C <= Form1.NewCatch_List_C[i].timer)
                        {
                            Tab_Basic.매수검색식_C(Form1.NewCatch_List_C[i]);
                        }
                    }
                }
            }

            if (Form1.잔고시간_ON)
            {
                foreach (var code in Form1.stockBalanceList.ToList())
                {
                    Stockbalance 잔고 = code.Value;
                    if (잔고.시간청산반복_A > 0) 잔고.시간청산반복_A--;
                    if (잔고.시간청산반복_B > 0) 잔고.시간청산반복_B--;
                    if (잔고.시간청산반복_C > 0) 잔고.시간청산반복_C--;
                }
            }

            foreach (var List in Form1.Repeat_condition_List_A.ToList()) List.timer++;
            foreach (var List in Form1.Repeat_condition_List_B.ToList()) List.timer++;
            foreach (var List in Form1.Repeat_condition_List_C.ToList()) List.timer++;
            foreach (var List in Form1.Repeat_condition_List_D.ToList()) List.timer++;
            foreach (var List in Form1.Repeat_condition_List_E.ToList()) List.timer++;
            foreach (var List in Form1.Repeat_condition_List_F.ToList()) List.timer++;
            foreach (var List in Form1.Repeat_condition_List_G.ToList()) List.timer++;
            foreach (var List in Form1.Repeat_condition_List_H.ToList()) List.timer++;
            foreach (var List in Form1.Repeat_condition_List_I.ToList()) List.timer++;
            foreach (var List in Form1.Repeat_condition_List_J.ToList()) List.timer++;
            foreach (var List in Form1.Repeat_condition_List_K.ToList()) List.timer++;
            foreach (var List in Form1.Repeat_condition_List_L.ToList()) List.timer++;
            foreach (var List in Form1.Repeat_condition_List_M.ToList()) List.timer++;
            foreach (var List in Form1.Repeat_condition_List_N.ToList()) List.timer++;

            foreach (var List in Form1.Rebal_condition_List_A.ToList()) List.timer++;
            foreach (var List in Form1.Rebal_condition_List_B.ToList()) List.timer++;
            foreach (var List in Form1.Rebal_condition_List_C.ToList()) List.timer++;
            foreach (var List in Form1.Rebal_condition_List_D.ToList()) List.timer++;
            foreach (var List in Form1.Rebal_condition_List_E.ToList()) List.timer++;
            foreach (var List in Form1.Rebal_condition_List_F.ToList()) List.timer++;
            foreach (var List in Form1.Rebal_condition_List_G.ToList()) List.timer++;

            foreach (var List in Form1.Liquidation_condition_List_A.ToList()) List.timer++;
            foreach (var List in Form1.Liquidation_condition_List_B.ToList()) List.timer++;
            foreach (var List in Form1.Liquidation_condition_List_C.ToList()) List.timer++;

            if (Form1.검색이탈_LIST.Count > 0)
            {
                for (int i = 0; i < Form1.검색이탈_LIST.Count; i++)
                {
                    검색이탈 이탈 = Form1.검색이탈_LIST[i];

                    if (이탈.타이머 > 0)
                        이탈.타이머--;
                }

                List<검색이탈> 이탈_LIST = Form1.검색이탈_LIST.FindAll(o => o.타이머 == 0);
                for (int i = 0; i < 이탈_LIST.Count; i++)
                {
                    string SearchTime = DateTime.Now.ToString("HH:mm:ss");

                    검색이탈 이탈 = 이탈_LIST[i];

                    if (이탈.신규) Tab_Basic.New_Buy("D", 이탈.코드_검색식.Split('^')[0], 이탈.코드_검색식.Split('^')[1]);
                    Tab_Watch.Watch_In_Out("D", 이탈.코드_검색식.Split('^')[0], 이탈.코드_검색식.Split('^')[1], SearchTime);
                    Tab_Repeat.Repeat_condition("D", 이탈.코드_검색식.Split('^')[0], 이탈.코드_검색식.Split('^')[1]);
                    Tab_AccountManagement.Rebalancing_condition("D", 이탈.코드_검색식.Split('^')[0], 이탈.코드_검색식.Split('^')[1]);
                    Tab_AccountManagement.Liquidation_condition("D", 이탈.코드_검색식.Split('^')[0], 이탈.코드_검색식.Split('^')[1]);

                    Form1.검색이탈_LIST.Remove(이탈);
                }
            }

            if (Form1.Watch_List.Count > 0)
            {
                foreach (var key in Form1.Watch_List.ToList())
                {
                    Watch watch = Form1.Watch_List[key.Key];

                    int 지연 = Properties.Settings.Default.MTB_watch_지연_A;
                    int CBB_watch_trading = Properties.Settings.Default.CBB_watch_trading_A;
                    string 관심_title = Properties.Settings.Default.CBB_Watch관심_A;
                    bool result = false;

                    if (key.Key.Contains("A:"))
                    {
                        result = true;
                    }
                    if (key.Key.Contains("B:"))
                    {
                        result = true;
                        지연 = Properties.Settings.Default.MTB_watch_지연_B;
                        CBB_watch_trading = Properties.Settings.Default.CBB_watch_trading_B;
                        관심_title = Properties.Settings.Default.CBB_Watch관심_B;
                    }
                    if (key.Key.Contains("C:"))
                    {
                        result = true;
                        지연 = Properties.Settings.Default.MTB_watch_지연_C;
                        CBB_watch_trading = Properties.Settings.Default.CBB_watch_trading_C;
                        관심_title = Properties.Settings.Default.CBB_Watch관심_C;

                    }
                    if (key.Key.Contains("D:"))
                    {
                        result = true;
                        지연 = Properties.Settings.Default.MTB_watch_지연_D;
                        CBB_watch_trading = Properties.Settings.Default.CBB_watch_trading_D;
                        관심_title = Properties.Settings.Default.CBB_Watch관심_D;
                    }

                    if (result && watch.매매.Equals("-"))
                    {
                        if (!관심_title.Equals("기본값"))
                        {
                            Interest_stock stock = Form1.Interest_stock_List.Find(o => o.Name.Equals(watch.Name) && o.Title.Equals(관심_title));
                            if (stock != null)
                            {
                                매수();
                            }
                        }
                        else
                        {
                            매수();
                        }

                        void 매수()
                        {
                            if (watch.timer >= 지연 && watch.Nowprice > 1 && watch.매매.Equals("-"))
                            {
                                if (CBB_watch_trading == 1)
                                {
                                    if (watch.State.Equals("진입"))
                                    {
                                        watch.매매 = "매수";
                                        watch.매수가 = (int)watch.Nowprice;
                                    }
                                }

                                if (CBB_watch_trading == 2)
                                {
                                    if (watch.State.Contains("진입"))
                                    {
                                        watch.매매 = "매수";
                                        watch.매수가 = (int)watch.Nowprice;
                                    }
                                }
                            }
                        }
                    }

                    if (watch.timer < Form1.maximum_time) if (watch.State.Contains("진입")) watch.timer++;
                }
            }

            if (Form1.잔고보정_list.Count > 0)
            {
                foreach (var item in Form1.잔고보정_list.ToList())
                {
                    item.시간++;

                    if (item.시간 > 5)
                    {
                        JumunItem 주문 = Form1.JumunItem_List.Find(o => o.종목코드.Equals(item.코드));
                        if (주문 == null)
                        {
                            Stockbalance 잔고 = Form1.stockBalanceList[item.코드];

                            if (잔고 != null)
                            {
                                if (잔고.보유수량 != 잔고.주문가능수량)
                                {
                                    Form1.주문내역요청 = false;
                                    Form1.TR_주문내역요청_12();
                                    Form1.TR_잔고보정요청_14(잔고.종목코드);
                                }
                                else
                                {
                                    Form1.잔고보정_list.Remove(item);
                                }
                            }
                            else
                            {
                                Form1.잔고보정_list.Remove(item);
                            }
                        }
                        else
                        {
                            Form1.잔고보정_list.Remove(item);
                        }
                    }
                }
            }
        }

        public static void Threadcount()
        {
            int.TryParse(Form1.form1.TB_주문row.Text, out int 주문row);
            int.TryParse(Form1.form1.TB_체결row.Text, out int 체결row);

            if (box.Form_JuMun.form.JuMun_dataGridView.RowCount > 주문row)
            {
                int count = box.Form_JuMun.form.JuMun_dataGridView.RowCount - 주문row;

                for (int i = 0; i < count; i++)
                {
                    box.Form_JuMun.form.JuMun_dataGridView.Rows.Remove(box.Form_JuMun.form.JuMun_dataGridView.Rows[주문row]);
                }

                box.Form_JuMun.form.JuMun_dataGridView.CurrentCell = null;
            }

            if (box.Form_Conclusion.form.Conclusion_DataGridView.RowCount > 체결row)
            {
                int count = box.Form_Conclusion.form.Conclusion_DataGridView.RowCount - 체결row;

                for (int i = 0; i < count; i++)
                {
                    box.Form_Conclusion.form.Conclusion_DataGridView.Rows.Remove(box.Form_Conclusion.form.Conclusion_DataGridView.Rows[체결row]);
                }

                box.Form_Conclusion.form.Conclusion_DataGridView.CurrentCell = null;
            }

            if (Form1.Trading_Item_List.Count > 0)
            {
                foreach (var item in Form1.Trading_Item_List.ToList())
                {
                    if (item.timer > 0) item.timer--;

                    if (item.timer == 0)
                    {
                        if (Form1.stockBalanceList.ContainsKey(item.Code))
                        {
                            가동(Form1.stockBalanceList[item.Code], item.location);
                        }

                        Form1.Trading_Item_List.Remove(item);
                    }
                }

                void 가동(Stockbalance 잔고, string location)
                {
                    switch (location)
                    {
                        case "반복_A": 잔고.가동_반복A = true; break;
                        case "반복_B": 잔고.가동_반복B = true; break;
                        case "반복_C": 잔고.가동_반복C = true; break;
                        case "반복_D": 잔고.가동_반복D = true; break;
                        case "반복_E": 잔고.가동_반복E = true; break;
                        case "반복_F": 잔고.가동_반복F = true; break;
                        case "반복_G": 잔고.가동_반복G = true; break;
                        case "반복_H": 잔고.가동_반복H = true; break;
                        case "반복_I": 잔고.가동_반복I = true; break;
                        case "반복_J": 잔고.가동_반복J = true; break;
                        case "반복_K": 잔고.가동_반복K = true; break;
                        case "반복_L": 잔고.가동_반복L = true; break;
                        case "반복_M": 잔고.가동_반복M = true; break;
                        case "반복_N": 잔고.가동_반복N = true; break;
                        case "리밸_A": 잔고.가동_리밸A = true; break;
                        case "리밸_B": 잔고.가동_리밸B = true; break;
                        case "리밸_C": 잔고.가동_리밸C = true; break;
                        case "리밸_D": 잔고.가동_리밸D = true; break;
                        case "리밸_E": 잔고.가동_리밸E = true; break;
                        case "리밸_F": 잔고.가동_리밸F = true; break;
                        case "리밸_G": 잔고.가동_리밸G = true; break;

                        case "청산_A": 잔고.가동_청산A = true; break;
                        case "청산_B": 잔고.가동_청산B = true; break;
                        case "청산_C": 잔고.가동_청산C = true; break;
                    }
                }
            }

            if (Form1.Repeat_time_A > 0) Form1.Repeat_time_A--;
            if (Form1.Repeat_time_B > 0) Form1.Repeat_time_B--;
            if (Form1.Repeat_time_C > 0) Form1.Repeat_time_C--;
            if (Form1.Repeat_time_D > 0) Form1.Repeat_time_D--;
            if (Form1.Repeat_time_E > 0) Form1.Repeat_time_E--;
            if (Form1.Repeat_time_F > 0) Form1.Repeat_time_F--;
            if (Form1.Repeat_time_G > 0) Form1.Repeat_time_G--;
            if (Form1.Repeat_time_H > 0) Form1.Repeat_time_H--;
            if (Form1.Repeat_time_I > 0) Form1.Repeat_time_I--;
            if (Form1.Repeat_time_J > 0) Form1.Repeat_time_J--;
            if (Form1.Repeat_time_K > 0) Form1.Repeat_time_K--;
            if (Form1.Repeat_time_L > 0) Form1.Repeat_time_L--;
            if (Form1.Repeat_time_M > 0) Form1.Repeat_time_M--;
            if (Form1.Repeat_time_N > 0) Form1.Repeat_time_N--;

            if (Form1.TT_rebalance_time_A > 0)
            {
                Form1.TT_rebalance_time_A--;
                if (Form1.TT_rebalance_time_A == 0) if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_A.Text = "X";
            }

            if (Form1.TT_rebalance_time_B > 0)
            {
                Form1.TT_rebalance_time_B--;
                if (Form1.TT_rebalance_time_B == 0) if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_B.Text = "X";
            }

            if (Form1.TT_rebalance_time_C > 0)
            {
                Form1.TT_rebalance_time_C--;
                if (Form1.TT_rebalance_time_C == 0) if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_C.Text = "X";
            }
            if (Form1.TT_rebalance_time_D > 0)
            {
                Form1.TT_rebalance_time_D--;
                if (Form1.TT_rebalance_time_D == 0) if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_D.Text = "X";
            }

            if (Form1.TT_rebalance_time_E > 0)
            {
                Form1.TT_rebalance_time_E--;
                if (Form1.TT_rebalance_time_E == 0) if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_E.Text = "X";
            }

            if (Form1.TT_rebalance_time_F > 0)
            {
                Form1.TT_rebalance_time_F--;
                if (Form1.TT_rebalance_time_F == 0) if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_F.Text = "X";
            }

            if (Form1.TT_rebalance_time_G > 0)
            {
                Form1.TT_rebalance_time_G--;
                if (Form1.TT_rebalance_time_G == 0) if (Form1.FormAccountManagement_Open) Form_AccountManagement.form.LB_rebalance_G.Text = "X";
            }

            if (Form1.TT_Liqu_time_A > 0) Form1.TT_Liqu_time_A--;
            if (Form1.TT_Liqu_time_B > 0) Form1.TT_Liqu_time_B--;
            if (Form1.TT_Liqu_time_C > 0) Form1.TT_Liqu_time_C--;

            if (!Form1.Run_time)
            {
                if (Form1.time_Run_time > 0)
                {
                    Form1.time_Run_time--;
                }

                if (Form1.time_Run_time == 0)
                {
                    Form1.Run_time = true;
                }
            }

            if (!Form1.Run_silson_W)
            {
                if (Form1.time_Run_silson_W > 0)
                {
                    Form1.time_Run_silson_W--;
                }

                if (Form1.time_Run_silson_W == 0)
                {
                    Form1.Run_silson_W = true;
                }
            }

            if (!Form1.Run_예상손실)
            {
                if (Form1.time_Run_예상손실 > 0)
                {
                    Form1.time_Run_예상손실--;
                }

                if (Form1.time_Run_예상손실 == 0)
                {
                    Form1.Run_예상손실 = true;
                }
            }

            if (!Form1.Run_예상수익)
            {
                if (Form1.time_Run_예상수익 > 0)
                {
                    Form1.time_Run_예상수익--;
                }

                if (Form1.time_Run_예상수익 == 0)
                {
                    Form1.Run_예상수익 = true;
                }
            }

            if (Form1.체결기록list.Count > 0)
            {
                JumunItem JumunItem = Form1.JumunItem_List.Find(o => o.주문번호.Equals(Form1.체결기록list[0].주문번호));
                if (JumunItem == null)
                {
                    Writing_Management.체결기록(Form1.체결기록list[0].con_num + ";" + Form1.체결기록list[0].주문유형 + ";" + Form1.체결기록list[0].주문N체결시간 + ";" + Form1.체결기록list[0].종목명 + ";" + Form1.체결기록list[0].체결검색식 + ";" + Form1.체결기록list[0].등락률 + ";" + Form1.체결기록list[0].수익률 + ";" + Form1.체결기록list[0].체결가 + ";" + Form1.체결기록list[0].주문수량 + ";" + Form1.체결기록list[0].체결량 + ";" +
                             (int.Parse(Form1.체결기록list[0].체결가) * int.Parse(Form1.체결기록list[0].체결량)).ToString() + ";" + Form1.체결기록list[0].현재가 + ";" + Form1.체결기록list[0].거래구분 + ";" + Form1.체결기록list[0].종목코드 + ";" + Form1.체결기록list[0].주문번호);

                    Form1.체결기록list.Remove(Form1.체결기록list[0]);
                }
            }

            foreach (var code in Form1.stockBalanceList.ToList())
            {
                Stockbalance 잔고 = code.Value;
                if (잔고.전량매도)  //전량매도 재매수 타이머 
                {
                    if (잔고.재매수 > 0)
                    {
                        잔고.재매수--;
                    }
                }
                else
                {
                    if (잔고.추매딜레이 > 0)
                    {
                        잔고.추매딜레이--;
                    }
                }
            }

            if (Form1.재주문_LIST.Count > 0)
            {
                foreach (var item in Form1.재주문_LIST.ToList())
                {
                    if (item.timer > 0)
                    {
                        item.timer--;

                        if (item.timer == 0) Form1.재주문_LIST.Remove(item);
                    }
                }
            }

            if (Form1.매매시작.Equals("매매시작"))
            {
                foreach (var item in Form1.신규조회_List.ToList())
                {
                    item.timer++;

                    if (item.timer > 6)
                    {
                        if (Form1.Market_Item_List[item.ItemCode].현재가 == 0)
                        {
                            if (!Form1.TR_catch_Item_List.Contains(item.ItemCode + ";" + item.Para))
                            {
                                Form1.jostRequestTrDataManager.DequeueRun();
                                Form1.TR_catch_Item_List.Enqueue(item.ItemCode + ";" + item.Para);
                                item.timer = 0;

                                Form1.Error_Log("[신규 누락] 종목:: " + Form1.Market_Item_List[item.ItemCode].종목명 + " 검색식: " + item.검색식 + " 초당 검색 종목이 너무 많습니다. ");
                                Console.WriteLine("[신규누락] 종목:: " + Form1.Market_Item_List[item.ItemCode].종목명 + " 검색식: " + item.검색식 + " 초당 검색 종목이 너무 많습니다. ");
                            }
                        }
                        else
                        {
                            Form1.신규조회_List.Remove(item);
                        }
                    }
                }
            }

            if (Form1.JumunItem_List.Count > 0)  // 미체결 그리드뷰 취소시간 타이머
            {
                foreach (var JumunItem in Form1.JumunItem_List.ToList())
                {
                    if (JumunItem != null)
                    {
                        if (JumunItem.취소timer != 99999) // 취소 타이머 카운트
                        {
                            if (!JumunItem.주문번호.Contains("+") && Form1.server_알림.Contains("마켓"))
                            {
                                if (JumunItem.취소timer > 0)
                                {
                                    JumunItem.취소timer--;
                                }
                            }

                            if (JumunItem.취소timer == 0)
                            {
                                if (JumunItem.주문취소 && JumunItem.미체결량 > 0)
                                {
                                    JumunItem.주문취소 = false;

                                    if (JumunItem.주문유형 == 1)
                                    {
                                        JumunItem.주문유형 = 3;    //매수취소
                                    }
                                    else if (JumunItem.주문유형 == 2)
                                    {
                                        JumunItem.주문유형 = 4;
                                    }

                                    if (JumunItem.비고.Length < 2)
                                    {
                                        JumunItem.비고 = "미체결 취소";
                                    }

                                    JumunItem.Order_count = 3;
                                    Form1.que_order(JumunItem.screennum, JumunItem.종목명, JumunItem.주문유형, JumunItem.종목코드, 0, 0, "00", JumunItem.주문번호, "미체결취소", JumunItem.Order번호);

                                    Form1.비프음("언체크");
                                }
                            }
                        }
                    }
                }

                foreach (var JumunItem in Form1.JumunItem_List.ToList())
                {
                    if (JumunItem != null)
                    {
                        if (JumunItem.Order_count == 1) // 매수,매도 주문누락 재주문
                        {
                            if (JumunItem.주문번호.Contains("+"))
                            {
                                JumunItem.재주문timer++;
                                if (JumunItem.재주문timer > 2)
                                {
                                    Order 주문오더 = Form1.Order_list.Find(o => o.Order번호.Equals(JumunItem.Order번호));
                                    if (주문오더 == null)
                                    {
                                        //Console.WriteLine("\n주문완료 - 주문누락 재주문 " + JumunItem.종목명);
                                        //Console.WriteLine(JumunItem.종목명+" 주문수: "+JumunItem.주문수량 + " 주문가격: "+ JumunItem.주문가격);

                                        주문누락재주문();
                                    }
                                    else
                                    {
                                        Form1.Order_list.Remove(주문오더);
                                    }

                                    void 주문누락재주문()
                                    {
                                        JumunItem.Order_count = 0;
                                        JumunItem.재주문timer = 0;
                                        JumunItem.재주문count++;

                                        if (Form1.server_알림.Contains("마켓"))
                                        {
                                            if (Form1.server.Equals("실서버"))
                                            {
                                                누락재주문();
                                            }
                                            else
                                            {
                                                if (Form1.timenow <= (Form1.메인마켓종료 - 2000)) // 모의투자용
                                                {
                                                    누락재주문();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (JumunItem.주문유형 == 1) //  매수
                                            {
                                                DataManagement.예수금업데이트("매수", JumunItem.주문가격, JumunItem.주문수량, "취소", JumunItem.종목코드);
                                            }
                                            else if (JumunItem.주문유형 == 2)
                                            {
                                                DataManagement.주문가능수업데이트(Form1.stockBalanceList[JumunItem.종목코드], "매도", JumunItem.주문수량, "취소");
                                            }

                                            Form1.JumunItem_List.Remove(JumunItem);
                                        }

                                        void 누락재주문()
                                        {
                                            if (JumunItem.주문유형 == 1) //  매수
                                            {
                                                if (Properties.Settings.Default.CB_misu)
                                                {
                                                    if (Form1.매수증거금)
                                                    {
                                                        예수금가능();
                                                    }
                                                    else
                                                    {
                                                        DataManagement.예수금업데이트("매수", JumunItem.주문가격, JumunItem.주문수량, "취소", JumunItem.종목코드);
                                                        Form1.JumunItem_List.Remove(JumunItem);
                                                    }
                                                }
                                                else
                                                {
                                                    if (Form1.Acc[0].추정D2 < JumunItem.미체결량 * JumunItem.주문가격)
                                                    {
                                                        DataManagement.예수금업데이트("매수", JumunItem.주문가격, JumunItem.주문수량, "취소", JumunItem.종목코드);

                                                        if (Form1.Order_list.Contains(주문오더)) Form1.Order_list.Remove(주문오더);
                                                        Form1.JumunItem_List.Remove(JumunItem);
                                                    }
                                                    else
                                                    {
                                                        예수금가능();
                                                    }
                                                }

                                                void 예수금가능()
                                                {
                                                    if (JumunItem.검색식.Contains("신규"))
                                                    {
                                                        주문누락기록(JumunItem, 10);

                                                        Form1.Error_Log(" ");
                                                        Form1.Error_Log("[신규주문 누락] 종목:" + JumunItem.종목명 + " 검색식:" + JumunItem.검색식 + " 거래구분:" + GET.거래구분_to한글(JumunItem.주문구분) + "주문가격:" + JumunItem.주문가격.ToString("N0") + " 주문수량:" + JumunItem.주문수량.ToString("N0") + " 주문금액:" + (JumunItem.주문가격 * JumunItem.주문수량).ToString("N0") + " '신규 매수주문' 이 재주문 됩니다.");
                                                        Form1.Error_Log(" ");
                                                    }
                                                    else
                                                    {
                                                        if (!Form1.stockBalanceList.ContainsKey(JumunItem.종목코드))
                                                        {
                                                            Console.WriteLine("누락재주문: 11111 ##### " + JumunItem.주문유형 + " 오더번호: " + JumunItem.Order번호 + " 오더카운터: " + JumunItem.Order_count);

                                                            DataManagement.예수금업데이트("매수", JumunItem.주문가격, JumunItem.주문수량, "취소", JumunItem.종목코드);
                                                            Form1.JumunItem_List.Remove(JumunItem);
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("누락재주문: 22222 ##### " + JumunItem.주문유형 + " 오더번호: " + JumunItem.Order번호 + " 오더카운터: " + JumunItem.Order_count);

                                                            주문누락기록(JumunItem, 10);

                                                            Form1.Error_Log(" ");
                                                            Form1.Error_Log("[추매주문 누락] 종목:" + JumunItem.종목명 + " 검색식:" + JumunItem.검색식 + " 거래구분:" + GET.거래구분_to한글(JumunItem.주문구분) + "주문가격:" + JumunItem.주문가격.ToString("N0") + " 주문수량:" + JumunItem.주문수량.ToString("N0") + " 주문금액:" + (JumunItem.주문가격 * JumunItem.주문수량).ToString("N0") + " '추가 매수주문' 이 재주문 됩니다.");
                                                            Form1.Error_Log(" ");
                                                        }
                                                    }
                                                }
                                            }
                                            else  // 매도
                                            {
                                                if (JumunItem.주문수량 >= 0)
                                                {
                                                    if (Form1.stockBalanceList.TryGetValue(JumunItem.종목코드, out Stockbalance 잔고))
                                                    {
                                                        if (잔고.주문가능수량 >= JumunItem.주문수량)
                                                        {
                                                            주문누락기록(JumunItem, 10);

                                                            Form1.Error_Log(" ");
                                                            Form1.Error_Log("[매도주문 누락] 종목:" + JumunItem.종목명 + " 검색식:" + JumunItem.검색식 + " 거래구분:" + GET.거래구분_to한글(JumunItem.주문구분) + "주문가격:" + JumunItem.주문가격.ToString("N0") + " 주문수량:" + JumunItem.주문수량.ToString("N0") + " 주문금액:" + (JumunItem.주문가격 * JumunItem.주문수량).ToString("N0") + " '매도 주문' 이 재주문 됩니다.");
                                                            Form1.Error_Log(" ");
                                                        }
                                                        else
                                                        {
                                                            DataManagement.주문가능수업데이트(Form1.stockBalanceList[JumunItem.종목코드], "매도", JumunItem.주문수량, "취소");
                                                            Form1.JumunItem_List.Remove(JumunItem);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Form1.JumunItem_List.Remove(JumunItem);
                                                    }
                                                }
                                                else
                                                {
                                                    Form1.JumunItem_List.Remove(JumunItem);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (JumunItem.Order_count == 4 && !JumunItem.주문취소)  // 취소주문 주문누락 재주문
                        {
                            JumunItem.재주문timer++;
                            if (JumunItem.재주문timer > 2)
                            {
                                Console.WriteLine("누락재주문: 취소주문 " + JumunItem.주문유형 + " 오더번호: " + JumunItem.Order번호 + " 재주문count: " + JumunItem.재주문count);

                                JumunItem.주문취소 = true;
                                JumunItem.재주문timer = 0;
                                JumunItem.재주문count++;
                            }
                        }

                        if (JumunItem.재주문count == 4)
                        {
                            JumunItem.재주문count = 0;
                            Form1.TR_주문내역요청_12();
                        }
                    }
                }

                void 주문누락기록(JumunItem item, int T)
                {
                    재주문 재주문 = Form1.재주문_LIST.Find(o => o.Item.Equals(item.종목코드 + "^" + item.screennum));
                    if (재주문 == null)
                    {
                        재주문 Add_Item = new 재주문(item.종목코드 + "^" + item.screennum, T);
                        Form1.재주문_LIST.Add(Add_Item);
                    }

                    int 주문가격 = item.주문가격;
                    string 거래구분 = "00";
                    if (item.주문구분 == 0)
                    {
                        주문가격 = 0;
                        거래구분 = "03";
                    }

                    Form1.que_order(item.screennum, item.종목명, item.주문유형, item.종목코드, item.주문수량, 주문가격, 거래구분, item.원주문번호, item.검색식, item.Order번호);
                }
            }

            if (Form1.Conclusion_remove_List.Count > 0)  // 주문리스트 동기화
            {
                for (int i = 0; i < Form1.Conclusion_remove_List.ToList().Count; i++)
                {
                    string 주문번호 = Form1.Conclusion_remove_List[i];

                    JumunItem JumunItem = Form1.JumunItem_List.Find(o => o.주문번호.Equals(주문번호));
                    if (JumunItem != null)
                    {
                        Conclusion_Management.Jumun_remove(JumunItem);
                    }
                }
            }

            if (Form1.timenow > Properties.Settings.Default.MT_misu_time && Properties.Settings.Default.CB_misu && Properties.Settings.Default.Combo_misu != 0 && Form1.주문내역요청)
            {
                Form1.Ten++;
                if (Form1.Ten >= 10)
                {
                    Form1.Ten = 0;
                    if (Form1.Acc[0].D2 < 0 && Form1.timenow < Form1.시장종료 )
                    {
                        Form1.주문내역요청 = false;
                        Form1.TR_주문내역요청_12();
                        Form1.TR_예수금요청_13();
                    }
                }
            }

            if (Form1.Acc[0].피_외인 == 0 || Form1.Acc[0].피_기관 == 0)
            {
                if (Form1.메인마켓시작 + 100 < Form1.timenow)
                {
                    Form1.Ten++;

                    if (Form1.Ten >= 10)
                    {
                        Form1.Ten = 0;
                        Form1.TR_투자자순매수요청_코스피_7(false);
                        Form1.TR_프로그램매매추이요청_코스피_9(false);
                    }
                }
            }
            else
            {
                //int.TryParse(DateTime.Now.ToString("mmss").Substring(1), out int TR_time);

                //if (Form1.순매수조회)
                //{
                //    if (TR_time == 0 || TR_time == 200 || TR_time == 400 || TR_time == 600 || TR_time == 800) //짝수
                //    {
                //        Form1.TR_투자자순매수요청_코스피_7(false);
                //    }

                //    if (TR_time == 100 || TR_time == 300 || TR_time == 500 || TR_time == 700 || TR_time == 900) // 음수
                //    {
                //        Form1.TR_프로그램매매추이요청_코스피_9(false);
                //    }
                //}
                //else
                //{
                //    int.TryParse(DateTime.Now.ToString("mmss"), out int halfhour);

                //    if (halfhour == 0 || halfhour == 3000) //짝수
                //    {
                //        Form1.TR_투자자순매수요청_코스피_7(false);
                //    }

                //    if (halfhour == 1500 || halfhour == 4500) // 음수
                //    {
                //        Form1.TR_프로그램매매추이요청_코스피_9(false);
                //    }
                //}

                //if (TR_time == 0 || TR_time == 500)  // 5분마다 조회
                //{
                //    if (Form1.주문내역요청 && Form1.주문내역요청_ON)
                //    {
                //        Form1.TR_주문내역요청_12();
                //        Form1.TR_opt10074_실현손익요청();

                //        Form1.주문내역요청 = false;
                //        Form1.주문내역요청_ON = false;
                //    }
                //}
                //else if (TR_time == 250 || TR_time == 750)
                //{
                //    if (Form1.예수금요청 && Form1.예수금요청_ON)
                //    {
                //        if (Properties.Settings.Default.CB_misu)
                //        {
                //            if (Form1.timenow < Properties.Settings.Default.MT_misu_time)
                //                Form1.TR_예수금요청_13();
                //        }
                //        else
                //        {
                //            Form1.TR_예수금요청_13();
                //        }

                //        Form1.예수금요청 = false;
                //        Form1.예수금요청_ON = false;
                //    }
                //}

                // 1. 성능 최적화: DateTime 속성을 직접 사용하여 문자열 변환 오버헤드 제거
                int minutes = DateTime.Now.Minute;
                int seconds = DateTime.Now.Second;

                // 2. 가독성 개선: 분과 초를 이용하여 TR_time 계산
                int TR_time = (minutes % 10) * 100 + seconds;

                // 3. 로직 간소화: switch 문으로 TR_time에 따른 동작을 명확히 구분
                if (Form1.순매수조회)
                {
                    // 순매수조회 로직
                    switch (TR_time)
                    {
                        case 0:
                        case 200:
                        case 400:
                        case 600:
                        case 800:
                            Form1.TR_투자자순매수요청_코스피_7(false);
                            break;
                        case 100:
                        case 300:
                        case 500:
                        case 700:
                        case 900:
                            Form1.TR_프로그램매매추이요청_코스피_9(false);
                            break;
                    }
                }
                else
                {
                    // 순매수조회가 false일 때의 로직
                    // 문자열 파싱 대신, 분과 초를 직접 활용
                    if ((minutes % 60 == 0 && seconds == 0) || (minutes % 60 == 30 && seconds == 0))
                    {
                        Form1.TR_투자자순매수요청_코스피_7(false);
                    }
                    else if ((minutes % 60 == 15 && seconds == 0) || (minutes % 60 == 45 && seconds == 0))
                    {
                        Form1.TR_프로그램매매추이요청_코스피_9(false);
                    }
                }

                // 4. if-else if 문으로 중복 조건 검사 방지 및 코드 흐름 명확화
                if (TR_time % 500 == 0) // 0 또는 500일 때
                {
                    if (Form1.주문내역요청 && Form1.주문내역요청_ON)
                    {
                        Form1.TR_주문내역요청_12();
                        Form1.TR_opt10074_실현손익요청();
                        Form1.주문내역요청 = false;
                        Form1.주문내역요청_ON = false;
                    }
                }
                else if (TR_time % 250 == 0) // 250 또는 750일 때
                {
                    if (Form1.예수금요청 && Form1.예수금요청_ON)
                    {
                        if (Properties.Settings.Default.CB_misu)
                        {
                            if (Form1.timenow < Properties.Settings.Default.MT_misu_time)
                                Form1.TR_예수금요청_13();
                        }
                        else
                        {
                            Form1.TR_예수금요청_13();
                        }
                        Form1.예수금요청 = false;
                        Form1.예수금요청_ON = false;
                    }
                }

                //   int.TryParse(DateTime.Now.ToString("ss").Substring(1), out int TR_time_);
                int TR_time_ = DateTime.Now.Second % 10;

                if (TR_time_ != 0 && (Form1.timenow < Properties.Settings.Default.MT_misu_time || !Properties.Settings.Default.CB_misu))
                {
                    Form1.주문내역요청 = true;
                    Form1.예수금요청 = true;
                }

                if (Form1.JumunItem_List.Count > 0)
                {
                    Form1.예수금요청_ON = true;
                    Form1.주문내역요청_ON = true;
                }
          
            }

            if (Form1.시장가탐색)
            {
                Dictionary<string, Market_Item> findResult = Form1.Market_Item_List.Where(o => o.Value.재매수_A > 0 || o.Value.재매수_B > 0).ToDictionary(o => o.Key, o => o.Value);
                foreach (var item in findResult)
                {
                    if (Properties.Settings.Default.CB_매수탐색A && item.Value.재매수_A > 0) Form1.Market_Item_List[item.Key].재매수_A--;
                    if (Properties.Settings.Default.CB_매수탐색B && item.Value.재매수_B > 0) Form1.Market_Item_List[item.Key].재매수_B--;
                }
            }

            if (Form1.NewBuyWrite_List.Count > 0)
            {
                foreach (var item in Form1.NewBuyWrite_List.ToList())
                {
                    JumunItem Item_null = Form1.JumunItem_List.Find(o => o.주문번호.Equals(item.Split(';')[0]));
                    if (Item_null == null)
                    {
                        if (Form1.stockBalanceList.TryGetValue(item.Split(';')[1], out Stockbalance 잔고))
                        {
                            Writing_Management.신규매수기록(Form1.today + ";" + 잔고.종목명 + ";" + 잔고.초기매수검색식 + ";" + DateTime.Now.ToString("HH:mm:ss") + ";" + 잔고.등락율 + ";" + 잔고.평균단가 + ";" +
                                                           잔고.보유수량 + ";" +
                                                           Method.단위변환(잔고.평균단가 * 잔고.보유수량) + ";" +
                                                           Form1.stockBalanceList.Count + ";" +
                                                           Method.단위변환(Form1.Acc[0].추정D2) + ";" +
                                                           Method.단위변환(Form1.Acc[0].매입금) + ";" +
                                                           Method.단위변환(Form1.Acc[0].추정자산) + ";" +
                                                           Method.단위변환(Form1.Acc[0].증가자산) + ";");

                            Form1.Error_Log(" ");
                            Form1.Error_Log(잔고.종목명 + " 이 신규매수 되었습니다.");
                            Form1.Error_Log(잔고.종목명 + " 매수검색식: " + 잔고.초기매수검색식 + " 금일등락율: " + 잔고.등락율 + " 매입가: " + 잔고.평균단가 + " 보유수량: " + 잔고.보유수량 +
                                                   " 적용금액: " + Method.단위변환(잔고.평균단가 * 잔고.보유수량));
                            Form1.Error_Log(잔고.종목명 + " 잔고수: " + Form1.stockBalanceList.Count + " 개" +
                                                   " 예수금: " + Method.단위변환(Form1.Acc[0].추정D2) +
                                                   " 매입금: " + Method.단위변환(Form1.Acc[0].매입금) +
                                                   " 추정자산: " + Method.단위변환(Form1.Acc[0].추정자산) +
                                                   " 증가자산: " + Method.단위변환(Form1.Acc[0].증가자산));
                            Form1.Error_Log(" ");

                            Form1.NewBuyWrite_List.Remove(item);
                        }
                    }
                }
            }
        }

        public static void UpdateNowDateTime() // 현재시간 출력
        {
            Form1.Writing_time++;
            if (Form1.Writing_time == 30)
            {
                Form1.Writing_time = 0;
                if (Form1.timenow <= Form1.시장종료)
                {
                    DataManagement.save_jango();
                    DataManagement.최종매입가_저장();
                    DataManagement.리밸_감시_List_기록();
                }
            }

            bool 서버연결()
            {
                bool result = false;

                if (Form1.Form_run)
                {
                    if (Form1.form1.axKHOpenAPI1.GetConnectState() == 1)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }

                return result;
            }

            if (!서버연결())
            {
                if (Form1.form1.Autologin_CheckBox.Checked)
                {
                    if (Form1.재접속 == "on")
                    {
                        for (int i = Properties.Settings.Default.TB_restart; i > 0; i--)
                        {
                            Form1.재접속 = "off";

                            if (i == 1)
                            {
                                Form1.재시작가동("로그인오류", "로그인이 정상적으로 이루어지지 않아\n 재시작 됩니다.");
                            }

                            Form1.동작_Log("서버 재연결 대기 :: " + i.ToString() + "초 뒤에 재시작 합니다.");
                            Form1.Delay(1000);

                            if (서버연결())
                            {
                                Form1.재접속 = "on";
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                if (Form1.로딩완료)
                {
                    if (Form1.Jine_Run)
                    {
                        if (DateTime.Now.DayOfWeek.Equals(DayOfWeek.Saturday) || DateTime.Now.DayOfWeek.Equals(DayOfWeek.Sunday))
                        {
                            Form1.장종료();
                            Form1.동작_Log(" ");
                            Form1.동작_Log("주말 에는 MoneyGame 을 할수 없습니다.");
                            Form1.Jine_Run = false;
                        }
                        else
                        {
                            if (Form1.server_알림.Equals("로딩중"))
                            {
                                Form1.server_알림 = "시작전";
                            }

                            if (!Form1.server_알림.Equals("장종료"))
                            {
                                if (Form1.시장종료 <= Form1.timenow)
                                {
                                    Form1.장종료();
                                    Form1.동작_Log(" ");
                                    Form1.동작_Log("MoneyGame 이 '종료' 되었습니다.");
                                    Form1.Jine_Run = false;
                                }
                            }
                        }
                    }

                    if (Form1.시장시작 <= Form1.timenow && !Form1.공휴일)
                    {
                        if (Form1.Jine_Run && Form1.form1.CB_Auto_tradingstart.Checked && !Form1.form1.RB_sell_run.Checked && (Form1.server_알림.Contains("마켓") || Form1.server_알림.Contains("동시") || Form1.server_알림.Equals("애프터전")) && !Form1.공휴일)   // 자동시작&정지 
                        {
                            if (Properties.Settings.Default.MT_starttime <= Form1.timenow)
                            {
                                if (Form1.로딩완료타임 > Form1.시장시작) Form1.Delay(2000);

                                Form1.form1.RB_sell_run.Checked = true;

                                if (Form1.신규매수정지 && Form1.추가매수정지)
                                {
                                    Form1.AutoClosingAlram("신규매수 - [ 정지 ], 추가매수 - [ 정지 ] 입니다. \n그룹n기능의 설정을 확인 해주세요.", "매수 정지알림", 60, "동작");
                                }
                                else
                                {
                                    Form1.form1.RB_buy_run.Checked = true;
                                }

                                Form1.Jine_Run = false;
                            }
                        }

                        if (Form1.server_알림.Equals("시작전") && !Form1.공휴일 && (Form1.메인마켓시작 - 10000) <= Form1.timenow)
                        {
                            if ((Form1.메인마켓시작 - 10000) <= Form1.timenow && Form1.timenow < Form1.메인마켓시작)
                            {
                                Form1.server_알림 = "프리마켓";
                                Form1.NXT_server = true;
                                Market_Run("프리마켓", "마켓대기");
                            }

                            if ((Form1.메인마켓시작 - 5000) <= Form1.timenow && Form1.timenow < Form1.메인마켓시작)
                            {
                                Form1.server_알림 = "장전동시";
                                Form1.NXT_server = false;

                                foreach (var code in Form1.stockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;
                                    if (!잔고.종목상태.Contains("거래정지"))
                                    {
                                        잔고.매매가능 = false;
                                        잔고.잔고청산 = false;
                                        잔고.종목상태 = "장전동시";
                                    }
                                }
                            }

                            if (Form1.메인마켓시작 <= Form1.timenow && Form1.timenow < Form1.메인마켓종료) { Form1.server_알림 = "메인마켓"; Form1.NXT_server = false; }
                            if (Form1.메인마켓종료 - 1000 <= Form1.timenow && Form1.timenow < Form1.메인마켓종료) { Form1.server_알림 = "동시호가"; Form1.NXT_server = false; }
                            if (Form1.메인마켓종료 <= Form1.timenow && Form1.timenow < Form1.메인마켓종료 + 1000) { Form1.server_알림 = "애프터전"; Form1.NXT_server = false; Market_Run("애프터전", "마켓종료"); }
                            if (Form1.메인마켓종료 + 1000 <= Form1.timenow && Form1.timenow < (Form1.메인마켓종료 + 47000)) { Form1.server_알림 = "애프터마켓"; Form1.NXT_server = true; Market_Run("애프터마켓", "마켓종료"); }

                            Form1.동작_Log(" ");
                            Form1.동작_Log("MoneyGame 이 '시작' 되었습니다.");
                        }

                        if (Form1.server_알림.Equals("프리마켓"))
                        {
                            if ((Form1.메인마켓시작 - 5000) <= Form1.timenow && Form1.timenow < Form1.메인마켓시작)
                            {
                                Form1.server_알림 = "장전동시";
                                Form1.NXT_server = false;

                                foreach (var code in Form1.stockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;
                                    if (!잔고.종목상태.Contains("거래정지"))
                                    {
                                        잔고.매매가능 = false;
                                        잔고.잔고청산 = false;
                                        잔고.종목상태 = "장전동시";
                                    }
                                }

                                for (int i = 0; i < Form1.JumunItem_List.ToList().Count; i++)
                                {
                                    JumunItem JumunItem = Form1.JumunItem_List[i];
                                    if (!JumunItem.비고.Equals("메인마켓 시작전 주문정리"))
                                    {
                                        if (JumunItem.검색식.Contains("신규_") && JumunItem.주문수량 == JumunItem.미체결량)
                                        {
                                            Properties.Settings.Default.신규횟수--;
                                            Form1.신규count--;
                                        }

                                        JumunItem.비고 = "메인마켓 시작전 주문정리";
                                        JumunItem.반복횟수 = 0;
                                        JumunItem.취소시간 = 0;
                                        JumunItem.취소timer = 0;

                                        Form1.동작_Log(" ");
                                        Form1.동작_Log("[메인마켓 시작전] 종목명:" + JumunItem.종목명 + " 주문유형:" + GET.주문유형(JumunItem.주문유형) + " 수량: " + JumunItem.미체결량 + " 주문이 취소 됩니다.");
                                        Form1.동작_Log(" ");
                                    }
                                }
                            }
                        }

                        if (Form1.server_알림.Equals("장전동시"))
                        {
                            if (Form1.메인마켓시작 <= Form1.timenow && Form1.timenow < Form1.메인마켓종료)
                            {
                                Form1.NXT_server = false;
                                Form1.server_알림 = "메인마켓";
                            }
                        }

                        if (Form1.server_알림.Equals("메인마켓"))
                        {
                            if (Form1.메인마켓종료 - 1000 <= Form1.timenow && Form1.timenow < Form1.메인마켓종료)
                            {
                                Form1.NXT_server = false;
                                Form1.server_알림 = "동시호가";
                            }
                        }

                        if (Form1.server_알림.Equals("동시호가"))
                        {
                            if (Form1.메인마켓종료 < Form1.timenow && Form1.timenow < Form1.메인마켓종료 + 1000)
                            {
                                Form1.NXT_server = false;
                                Form1.server_알림 = "애프터전";

                                Market_Run("애프터전", "마켓종료");
                            }
                        }

                        if (Form1.server_알림.Equals("애프터전"))
                        {
                            if (Form1.메인마켓종료 + 500 < Form1.timenow) // 애프터마켓 시작전 주문 취소
                            {
                                for (int i = 0; i < Form1.JumunItem_List.ToList().Count; i++)
                                {
                                    JumunItem JumunItem = Form1.JumunItem_List[i];
                                    if (!JumunItem.비고.Equals("애스터마켓 시작전 주문정리"))
                                    {
                                        if (JumunItem.검색식.Contains("신규_") && JumunItem.주문수량 == JumunItem.미체결량)
                                        {
                                            Properties.Settings.Default.신규횟수--;
                                            Form1.신규count--;
                                        }

                                        JumunItem.비고 = "애스터마켓 시작전 주문정리";
                                        JumunItem.반복횟수 = 0;
                                        JumunItem.취소시간 = 0;
                                        JumunItem.취소timer = 0;

                                        Form1.동작_Log(" ");
                                        Form1.동작_Log("[애스터마켓 시작전] 종목명:" + JumunItem.종목명 + " 주문유형:" + GET.주문유형(JumunItem.주문유형) + " 수량: " + JumunItem.미체결량 + " 주문이 취소 됩니다.");
                                        Form1.동작_Log(" ");
                                    }
                                }
                            }

                            if (Form1.메인마켓종료 + 1000 <= Form1.timenow && Form1.timenow < (Form1.메인마켓종료 + 47000))
                            {
                                Form1.NXT_server = true;
                                Form1.server_알림 = "애프터마켓";

                                Market_Run("애프터마켓", "마켓종료");
                            }
                        }

                        if (Form1.server_알림.Contains("마켓"))
                        {
                            if (Form1.시장종료 <= Form1.timenow)
                            {
                                Form1.NXT_server = false;
                                Form1.장종료();
                                Form1.동작_Log(" ");
                                Form1.동작_Log("MoneyGame 이 '종료' 되었습니다.");
                            }

                            if (Form1.호가요청)
                            {
                                if (Form1.시장시작 < Form1.로딩완료타임)
                                {
                                    if (Form1.로딩완료타임 + 30 < Form1.timenow) 요청();
                                }
                                else
                                {
                                    if (Form1.시장시작 + 30 < Form1.timenow) 요청();
                                }

                                void 요청()
                                {
                                    Form1.호가요청 = false;
                                    Form1.TR_호가요청_11();
                                }
                            }
                        }

                        void Market_Run(string sever, string text)
                        {
                            foreach (var code in Form1.stockBalanceList.ToList())
                            {
                                Stockbalance 잔고 = code.Value;
                                if (!잔고.종목상태.Contains("거래정지"))
                                {
                                    NXT nxt = Form1.NXT_list.Find(o => o.code.Equals(잔고.종목코드));
                                    if (nxt != null)
                                    {
                                        잔고.매매가능 = true;
                                        잔고.잔고청산 = true;

                                        if (sever.Equals("애프터전"))
                                        {
                                            잔고.종목상태 = "애프터전";
                                        }
                                        else
                                        {
                                            잔고.종목상태 = GET.Jango_state(잔고.종목코드);
                                        }
                                    }
                                    else
                                    {
                                        잔고.매매가능 = false;
                                        잔고.잔고청산 = false;
                                        잔고.종목상태 = text;
                                    }
                                }
                            }
                        }
                    }

                    if (Form1.Jine_Stop && Properties.Settings.Default.MT_stoptime <= Form1.timenow && (Form1.form1.RB_buy_run.Checked || Form1.form1.RB_sell_run.Checked))
                    {
                        Form1.Jine_Stop = false;
                        Form1.form1.RB_sell_run.Checked = false;
                        Form1.form1.RB_buy_run.Checked = false;

                        Form1.form1.RB_buy_stop.Checked = true;
                        Form1.form1.RB_sell_stop.Checked = true;
                    }

                    if (Form1.지니_64n종료)
                    {
                        if (Properties.Settings.Default.MT_closetime < Form1.timenow)
                        {
                            if (Properties.Settings.Default.CBB_지니_64종료 == 0)
                            {
                                Form1.지니_64n종료 = false;
                                Application.Exit();
                            }
                            else if (Properties.Settings.Default.CBB_지니_64종료 == 1)
                            {
                                Form1.지니_64n종료 = false;
                                Form1.HI지니_64시작 = true;
                                Application.Exit();
                            }
                        }
                    }

                    if (Form1.로딩완료타임 < 073500)
                    {
                        if (073500 <= Form1.timenow)
                        {
                            Form1.재시작가동("재시작", " 7시30분전 가동하였습니다. 지니_64 재시작 을 위해 HI지니_64를 실행합니다.");
                        }
                    }

                    if (!Form1.공휴일)
                    {
                        ////////////// 예약 주문 넣기

                        if (Properties.Settings.Default.CB_예약주문_장전 && Form1.예약주문_장전)
                        {
                            if (Properties.Settings.Default.MTB_예약주문_장전주문시간 < Form1.timenow)
                            {
                                Form1.예약주문_장전 = false;
                                Order_Reserve.예약주문실행(true);
                            }
                        }

                        if (Properties.Settings.Default.CB_예약주문_종가 && Form1.예약주문_종가)
                        {
                            if (Properties.Settings.Default.MTB_예약주문_종가주문시간 < Form1.timenow)
                            {
                                Form1.예약주문_종가 = false;
                                Order_Reserve.예약주문실행(false);
                            }
                        }


                        if (Properties.Settings.Default.CB_매매기간_오전 && Form1.매매기간_오전)
                        {
                            if (Properties.Settings.Default.TB_매매기간_오전주문시간 < Form1.timenow)
                            {
                                Form1.매매기간_오전 = false;

                                foreach (var code in Form1.stockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;
                                    Tab_Special.매매일_기준거래(잔고, "오전");
                                }
                            }
                        }

                        if (Properties.Settings.Default.CB_매매기간_오후 && Form1.매매기간_오후)
                        {
                            if (Properties.Settings.Default.TB_매매기간_오후주문시간 < Form1.timenow)
                            {
                                Form1.매매기간_오후 = false;

                                foreach (var code in Form1.stockBalanceList.ToList())
                                {
                                    Stockbalance 잔고 = code.Value;
                                    Tab_Special.매매일_기준거래(잔고, "오후");
                                }
                            }
                        }
                    }

                    if (Form1.server_알림.Contains("마켓") || Form1.server_알림.Contains("동시")) // 신규매수 시작시간에 검색 신호주기
                    {
                        if (Form1.신규매수신호_A && Properties.Settings.Default.MT_new_start_A <= Form1.timenow)
                        {
                            Form1.신규매수신호_A = false;
                            foreach (NewCatch_A New in Form1.NewCatch_List_A.ToList())
                            {
                                Tab_Basic.매수검색식_A(New);
                            }
                        }

                        if (Form1.신규매수신호_B && Properties.Settings.Default.MT_new_start_B <= Form1.timenow)
                        {
                            Form1.신규매수신호_B = false;
                            foreach (NewCatch_B New in Form1.NewCatch_List_B.ToList())
                            {
                                Tab_Basic.매수검색식_B(New);
                            }
                        }

                        if (Form1.신규매수신호_C && Properties.Settings.Default.MT_new_start_C <= Form1.timenow)
                        {
                            Form1.신규매수신호_C = false;
                            foreach (NewCatch_C New in Form1.NewCatch_List_C.ToList())
                            {
                                Tab_Basic.매수검색식_C(New);
                            }
                        }
                    }
                }
            }

            if (Properties.Settings.Default.CB_Record)
            {
                Console.WriteLine("time: " + Form1.timenow + "camrun: " + Form1.OcamRun);
                int Ocamtime = Properties.Settings.Default.TB_Record_Run;

                if (Form1.OcamRun && Form1.timenow >= Ocamtime)
                {
                    Form1.OcamRun = false;

                    if (Properties.Settings.Default.CBB_Record == 1)
                    {
                        FileInfo fi_오켐 = new FileInfo("C:\\Program Files (x86)\\oCam\\oCam.exe");
                        if (fi_오켐.Exists)
                        {
                            string filename = "oCam"; //종료시킬 프로그램

                            Process[] process = Process.GetProcessesByName(filename);
                            if (process.GetLength(0) == 0)
                            {
                                System.Diagnostics.Process ps = new System.Diagnostics.Process();
                                ps.StartInfo.FileName = "oCam.exe";
                                ps.StartInfo.WorkingDirectory = "C:\\Program Files (x86)\\oCam\\";
                                ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                ps.Start();

                            }
                            else
                            {
                                Form1.AutoClosingAlram("오캠 이 실행중입니다", "실행알람", 10, "에러");
                            }
                        }
                        else
                        {
                            Form1.AutoClosingAlram("오캠 경로에 파일이 없습니다.\n경로 C:\\Program Files (x86)\\oCam\\oCam.exe", "파일에러", 10, "에러");
                        }
                    }
                    else if (Properties.Settings.Default.CBB_Record == 2)
                    {
                        FileInfo fi_OBS = new FileInfo("C:\\Program Files\\obs-studio\\bin\\64bit\\obs64.exe");
                        if (fi_OBS.Exists)
                        {
                            string filename = "obs64"; //종료시킬 프로그램

                            Process[] process = Process.GetProcessesByName(filename);
                            if (process.GetLength(0) == 0)
                            {
                                System.Diagnostics.Process ps = new System.Diagnostics.Process();
                                ps.StartInfo.FileName = "obs64.exe";
                                ps.StartInfo.WorkingDirectory = "C:\\Program Files\\obs-studio\\bin\\64bit\\";
                                ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                ps.Start();

                            }
                            else
                            {
                                Form1.AutoClosingAlram("OBS studio 가 실행중입니다", "실행알람", 10, "에러");
                            }
                        }
                        else
                        {
                            Form1.AutoClosingAlram("OBS studio 경로에 파일이 없습니다.\n경로 C:\\Program Files\\obs-studio\\bin\\64bit\\obs64.exe", "알람", 10, "에러");
                        }
                    }
                }

                if (Properties.Settings.Default.TB_Record_start - 1 < Form1.timenow)
                {
                    if (Form1.RecordON)
                    {
                        Form1.RecordON = false;
                        Form1.Delay(100);
                        SendKeys.Send("{F2}");
                    }
                }
                else
                {
                    Form1.RecordON = true;
                }

                if (Properties.Settings.Default.TB_Record_end < Form1.timenow)
                {
                    if (Form1.RecordOff)
                    {
                        Form1.RecordOff = false;
                        Form1.Delay(100);
                        SendKeys.Send("{F2}");
                    }
                }
                else
                {
                    Form1.RecordOff = true;
                }
            }
        }
    }
}
