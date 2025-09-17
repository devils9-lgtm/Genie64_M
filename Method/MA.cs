using System;
using System.Collections.Generic;
using System.Linq;

namespace 지니_64
{
    class MA
    {
        static Dictionary<string, Stockbalance> stockBalanceList = Form1.stockBalanceList;

        public static void Moving_Average(Stockbalance 잔고)
        {
            if (잔고 == null)
            {
                foreach (var code in stockBalanceList.ToList())
                {
                    잔고 = stockBalanceList[code.Key];

                    if (잔고.분_리스트.Contains(";"))
                    {
                        string[] 리스트 = 잔고.분_리스트.Split(';');
                        if (리스트.Length >= 60)
                        {
                            잔고.분_리스트 = "";
                            for (int i = 0; i < 리스트.Length; i++)
                            {
                                if (i == 59) break;

                                if (잔고.분_리스트.Length < 1)
                                {
                                    잔고.분_리스트 = 리스트[i];
                                }
                                else
                                {
                                    잔고.분_리스트 = 잔고.분_리스트 + ";" + 리스트[i];
                                }
                            }

                            잔고.분_리스트 = 잔고.현재가.ToString() + ";" + 잔고.분_리스트;
                        }
                    }
                }
            }
            else
            {
                if (잔고.분_리스트.Contains(";"))
                {
                    string[] 리스트 = 잔고.분_리스트.Split(';');
                    if (리스트.Length >= 60)
                    {
                        잔고.분_리스트 = "";
                        for (int i = 0; i < 리스트.Length; i++)
                        {
                            if (i == 0)
                            {
                                잔고.분_리스트 = 잔고.현재가.ToString();
                            }
                            else
                            {
                                잔고.분_리스트 = 잔고.분_리스트 + ";" + 리스트[i];
                            }
                        }
                    }
                }

                if (잔고.일_리스트.Contains(";"))
                {
                    string[] 리스트 = 잔고.일_리스트.Split(';');
                    if (리스트.Length >= 60)
                    {
                        잔고.일_리스트 = "";
                        for (int i = 0; i < 리스트.Length; i++)
                        {
                            if (i == 0)
                            {
                                잔고.일_리스트 = 잔고.현재가.ToString();
                            }
                            else
                            {
                                잔고.일_리스트 = 잔고.일_리스트 + ";" + 리스트[i];
                            }
                        }
                    }
                }
            }
        }


        public static void Get_MA()
        {
            change_value();
            foreach (var code in Form1.stockBalanceList.ToList())
            {
                Stockbalance 잔고 = Form1.stockBalanceList[code.Key];
                MA.Get_Min_Moving_Average(잔고);
                MA.Get_Day_Moving_Average(잔고);
            }
        }

        public static void Mma_Record(Stockbalance 잔고)
        {
            if (잔고 == null)
            {
                foreach (var code in stockBalanceList.ToList())
                {
                    잔고 = stockBalanceList[code.Key];

                    if (잔고.분_리스트.Contains(";"))
                    {
                        string[] 리스트 = 잔고.분_리스트.Split(';');
                        if (리스트.Length >= 300)
                        {
                            잔고.분_리스트 = "";
                            for (int i = 0; i < 리스트.Length; i++)
                            {
                                if (i == 300) break;

                                if (잔고.분_리스트.Length < 1)
                                {
                                    잔고.분_리스트 = 리스트[i];
                                }
                                else
                                {
                                    잔고.분_리스트 = 잔고.분_리스트 + ";" + 리스트[i];
                                }
                            }

                            잔고.분_리스트 = 잔고.현재가.ToString() + ";" + 잔고.분_리스트;
                        }
                    }
                }
            }
            else
            {
                string[] 리스트 = 잔고.분_리스트.Split(';');

                if (리스트.Length >= 300)
                {
                    잔고.분_리스트 = "";
                    for (int i = 0; i < 리스트.Length; i++)
                    {
                        if (i == 0)
                        {
                            잔고.분_리스트 = 잔고.현재가.ToString();
                        }
                        else
                        {
                            잔고.분_리스트 = 잔고.분_리스트 + ";" + 리스트[i];
                        }
                    }
                }
            }

            Get_Min_Moving_Average(잔고);
        }

        public static void Get_Min_Moving_Average(Stockbalance 잔고)
        {
            if (잔고.분_리스트.Contains(';'))
            {
                ma avg = Form1.Min_ma_list[잔고.종목코드];

                avg.repeat_ma1_A = 값구하기(avg.repeat_ma1_value_A);
                avg.repeat_ma1_B = 값구하기(avg.repeat_ma1_value_B);
                avg.repeat_ma1_C = 값구하기(avg.repeat_ma1_value_C);
                avg.repeat_ma1_D = 값구하기(avg.repeat_ma1_value_D);
                avg.repeat_ma1_E = 값구하기(avg.repeat_ma1_value_E);
                avg.repeat_ma1_F = 값구하기(avg.repeat_ma1_value_F);
                avg.repeat_ma1_G = 값구하기(avg.repeat_ma1_value_G);
                avg.repeat_ma1_H = 값구하기(avg.repeat_ma1_value_H);
                avg.repeat_ma1_I = 값구하기(avg.repeat_ma1_value_I);
                avg.repeat_ma1_J = 값구하기(avg.repeat_ma1_value_J);
                avg.repeat_ma1_K = 값구하기(avg.repeat_ma1_value_K);
                avg.repeat_ma1_L = 값구하기(avg.repeat_ma1_value_L);
                avg.repeat_ma1_M = 값구하기(avg.repeat_ma1_value_M);
                avg.repeat_ma1_N = 값구하기(avg.repeat_ma1_value_N);

                avg.repeat_ma2_A = 값구하기(avg.repeat_ma2_value_A);
                avg.repeat_ma2_B = 값구하기(avg.repeat_ma2_value_B);
                avg.repeat_ma2_C = 값구하기(avg.repeat_ma2_value_C);
                avg.repeat_ma2_D = 값구하기(avg.repeat_ma2_value_D);
                avg.repeat_ma2_E = 값구하기(avg.repeat_ma2_value_E);
                avg.repeat_ma2_F = 값구하기(avg.repeat_ma2_value_F);
                avg.repeat_ma2_G = 값구하기(avg.repeat_ma2_value_G);
                avg.repeat_ma2_H = 값구하기(avg.repeat_ma2_value_H);
                avg.repeat_ma2_I = 값구하기(avg.repeat_ma2_value_I);
                avg.repeat_ma2_J = 값구하기(avg.repeat_ma2_value_J);
                avg.repeat_ma2_K = 값구하기(avg.repeat_ma2_value_K);
                avg.repeat_ma2_L = 값구하기(avg.repeat_ma2_value_L);
                avg.repeat_ma2_M = 값구하기(avg.repeat_ma2_value_M);
                avg.repeat_ma2_N = 값구하기(avg.repeat_ma2_value_N);

                avg.Rebal_ma1_A = 값구하기(avg.Rebal_ma1_value_A);
                avg.Rebal_ma1_B = 값구하기(avg.Rebal_ma1_value_B);
                avg.Rebal_ma1_C = 값구하기(avg.Rebal_ma1_value_C);
                avg.Rebal_ma1_D = 값구하기(avg.Rebal_ma1_value_D);
                avg.Rebal_ma1_E = 값구하기(avg.Rebal_ma1_value_E);
                avg.Rebal_ma1_F = 값구하기(avg.Rebal_ma1_value_F);
                avg.Rebal_ma1_G = 값구하기(avg.Rebal_ma1_value_G);

                avg.Rebal_ma2_A = 값구하기(avg.Rebal_ma2_value_A);
                avg.Rebal_ma2_B = 값구하기(avg.Rebal_ma2_value_B);
                avg.Rebal_ma2_C = 값구하기(avg.Rebal_ma2_value_C);
                avg.Rebal_ma2_D = 값구하기(avg.Rebal_ma2_value_D);
                avg.Rebal_ma2_E = 값구하기(avg.Rebal_ma2_value_E);
                avg.Rebal_ma2_F = 값구하기(avg.Rebal_ma2_value_F);
                avg.Rebal_ma2_G = 값구하기(avg.Rebal_ma2_value_G);


                avg.Rebal_TS_ma_1차_A = 값구하기(avg.Rebal_TS_ma_1차_value_A);
                avg.Rebal_TS_ma_1차_B = 값구하기(avg.Rebal_TS_ma_1차_value_B);
                avg.Rebal_TS_ma_1차_C = 값구하기(avg.Rebal_TS_ma_1차_value_C);
                avg.Rebal_TS_ma_1차_D = 값구하기(avg.Rebal_TS_ma_1차_value_D);
                avg.Rebal_TS_ma_1차_E = 값구하기(avg.Rebal_TS_ma_1차_value_E);
                avg.Rebal_TS_ma_1차_F = 값구하기(avg.Rebal_TS_ma_1차_value_F);
                avg.Rebal_TS_ma_1차_G = 값구하기(avg.Rebal_TS_ma_1차_value_G);

                avg.Rebal_TS_ma_2차_A = 값구하기(avg.Rebal_TS_ma_2차_value_A);
                avg.Rebal_TS_ma_2차_B = 값구하기(avg.Rebal_TS_ma_2차_value_B);
                avg.Rebal_TS_ma_2차_C = 값구하기(avg.Rebal_TS_ma_2차_value_C);
                avg.Rebal_TS_ma_2차_D = 값구하기(avg.Rebal_TS_ma_2차_value_D);
                avg.Rebal_TS_ma_2차_E = 값구하기(avg.Rebal_TS_ma_2차_value_E);
                avg.Rebal_TS_ma_2차_F = 값구하기(avg.Rebal_TS_ma_2차_value_F);
                avg.Rebal_TS_ma_2차_G = 값구하기(avg.Rebal_TS_ma_2차_value_G);

                avg.Liquidation_ma_A = 값구하기(avg.Liquidation_ma_value_A);
                avg.Liquidation_ma_B = 값구하기(avg.Liquidation_ma_value_B);
                avg.Liquidation_ma_C = 값구하기(avg.Liquidation_ma_value_C);

                avg.Liquidation_TS_ma_A = 값구하기(avg.Liquidation_TS_ma_value_A);
                avg.Liquidation_TS_ma_B = 값구하기(avg.Liquidation_TS_ma_value_B);
                avg.Liquidation_TS_ma_C = 값구하기(avg.Liquidation_TS_ma_value_C);

                avg.매매기간_TS_ma_A = 값구하기(avg.매매기간_TS_ma_value_A);
                avg.매매기간_TS_ma_B = 값구하기(avg.매매기간_TS_ma_value_B);
                avg.매매기간_TS_ma_C = 값구하기(avg.매매기간_TS_ma_value_C);
                avg.매매기간_TS_ma_D = 값구하기(avg.매매기간_TS_ma_value_D);
                avg.매매기간_TS_ma_E = 값구하기(avg.매매기간_TS_ma_value_E);
                avg.매매기간_TS_ma_F = 값구하기(avg.매매기간_TS_ma_value_F);

                double 값구하기(int 주기)
                {
                    string[] list = 잔고.분_리스트.Split(';');
                    double 평균합 = 0;

                    for (int i = 0; i < list.Length; i++)
                    {
                        if (i == 주기) break;
                        double.TryParse(list[i], out double result);
                        평균합 = 평균합 + result;
                    }

                    if (list.Length >= 주기)
                    {
                        return 평균합 / 주기;
                    }
                    else
                    {
                        return 평균합 / list.Length;
                    }
                }

                //if (잔고.종목명.Equals("국전약품"))
                //{
                //    Console.WriteLine("\nmarket_code: " + avg.code);
                //    Console.WriteLine("DateTime.Now: " + DateTime.Now.ToString("HHmmss"));

                //    Console.WriteLine("잔고.분_리스트 Length : " + 잔고.분_리스트.Split(';').Length);
                //    Console.WriteLine("잔고.분_리스트: " + 잔고.분_리스트);

                //    Console.WriteLine();
                //    Console.WriteLine("Rebal_mma1_A: " + avg.Rebal_ma1_A);
                //    Console.WriteLine("Rebal_mma1_B: " + avg.Rebal_ma1_B);
                //    Console.WriteLine("Rebal_mma1_C: " + avg.Rebal_ma1_C);
                //    Console.WriteLine("Rebal_mma1_D: " + avg.Rebal_ma1_D);
                //    Console.WriteLine("Rebal_mma1_E: " + avg.Rebal_ma1_E);
                //    Console.WriteLine("Rebal_mma1_F: " + avg.Rebal_ma1_F);
                //    Console.WriteLine("Rebal_mma1_G: " + avg.Rebal_ma1_G);
                //    Console.WriteLine();
                //    Console.WriteLine("Rebal_mma2_A: " + avg.Rebal_ma2_A);
                //    Console.WriteLine("Rebal_mma2_B: " + avg.Rebal_ma2_B);
                //    Console.WriteLine("Rebal_mma2_C: " + avg.Rebal_ma2_C);
                //    Console.WriteLine("Rebal_mma2_D: " + avg.Rebal_ma2_D);
                //    Console.WriteLine("Rebal_mma2_E: " + avg.Rebal_ma2_E);
                //    Console.WriteLine("Rebal_mma2_F: " + avg.Rebal_ma2_F);
                //    Console.WriteLine("Rebal_mma2_G: " + avg.Rebal_ma2_G);
                //    Console.WriteLine();
                //    Console.WriteLine("Rebal_TS_ma_1차_A: " + avg.Rebal_TS_ma_1차_A);
                //    Console.WriteLine("Rebal_TS_ma_1차_B: " + avg.Rebal_TS_ma_1차_B);
                //    Console.WriteLine("Rebal_TS_ma_1차_C: " + avg.Rebal_TS_ma_1차_C);
                //    Console.WriteLine("Rebal_TS_ma_1차_D: " + avg.Rebal_TS_ma_1차_D);
                //    Console.WriteLine("Rebal_TS_ma_1차_E: " + avg.Rebal_TS_ma_1차_E);
                //    Console.WriteLine("Rebal_TS_ma_1차_F: " + avg.Rebal_TS_ma_1차_F);
                //    Console.WriteLine("Rebal_TS_ma_1차_G: " + avg.Rebal_TS_ma_1차_G);
                //    Console.WriteLine();
                //    Console.WriteLine("Rebal_TS_ma_2차_A: " + avg.Rebal_TS_ma_2차_A);
                //    Console.WriteLine("Rebal_TS_ma_2차_B: " + avg.Rebal_TS_ma_2차_B);
                //    Console.WriteLine("Rebal_TS_ma_2차_C: " + avg.Rebal_TS_ma_2차_C);
                //    Console.WriteLine("Rebal_TS_ma_2차_D: " + avg.Rebal_TS_ma_2차_D);
                //    Console.WriteLine("Rebal_TS_ma_2차_E: " + avg.Rebal_TS_ma_2차_E);
                //    Console.WriteLine("Rebal_TS_ma_2차_F: " + avg.Rebal_TS_ma_2차_F);
                //    Console.WriteLine("Rebal_TS_ma_2차_G: " + avg.Rebal_TS_ma_2차_G);
                //}
            }
        }

        public static void Dma_Record(Stockbalance 잔고)
        {
            string[] 리스트 = 잔고.일_리스트.Split(';');
            if (리스트.Length >= 300)
            {
                잔고.일_리스트 = "";
                for (int i = 0; i < 리스트.Length; i++)
                {
                    if (i == 0)
                    {
                        잔고.일_리스트 = 잔고.현재가.ToString();
                    }
                    else
                    {
                        잔고.일_리스트 = 잔고.일_리스트 + ";" + 리스트[i];
                    }
                }
            }

            Get_Day_Moving_Average(잔고);
        }

        public static void Get_Day_Moving_Average(Stockbalance 잔고)
        {

            if (잔고.일_리스트.Contains(';'))
            {
                ma avg = Form1.Day_ma_list[잔고.종목코드];

                avg.repeat_ma1_A = 값구하기(avg.repeat_ma1_value_A);
                avg.repeat_ma1_B = 값구하기(avg.repeat_ma1_value_B);
                avg.repeat_ma1_C = 값구하기(avg.repeat_ma1_value_C);
                avg.repeat_ma1_D = 값구하기(avg.repeat_ma1_value_D);
                avg.repeat_ma1_E = 값구하기(avg.repeat_ma1_value_E);
                avg.repeat_ma1_F = 값구하기(avg.repeat_ma1_value_F);
                avg.repeat_ma1_G = 값구하기(avg.repeat_ma1_value_G);
                avg.repeat_ma1_H = 값구하기(avg.repeat_ma1_value_H);
                avg.repeat_ma1_I = 값구하기(avg.repeat_ma1_value_I);
                avg.repeat_ma1_J = 값구하기(avg.repeat_ma1_value_J);
                avg.repeat_ma1_K = 값구하기(avg.repeat_ma1_value_K);
                avg.repeat_ma1_L = 값구하기(avg.repeat_ma1_value_L);
                avg.repeat_ma1_M = 값구하기(avg.repeat_ma1_value_M);
                avg.repeat_ma1_N = 값구하기(avg.repeat_ma1_value_N);

                avg.repeat_ma2_A = 값구하기(avg.repeat_ma2_value_A);
                avg.repeat_ma2_B = 값구하기(avg.repeat_ma2_value_B);
                avg.repeat_ma2_C = 값구하기(avg.repeat_ma2_value_C);
                avg.repeat_ma2_D = 값구하기(avg.repeat_ma2_value_D);
                avg.repeat_ma2_E = 값구하기(avg.repeat_ma2_value_E);
                avg.repeat_ma2_F = 값구하기(avg.repeat_ma2_value_F);
                avg.repeat_ma2_G = 값구하기(avg.repeat_ma2_value_G);
                avg.repeat_ma2_H = 값구하기(avg.repeat_ma2_value_H);
                avg.repeat_ma2_I = 값구하기(avg.repeat_ma2_value_I);
                avg.repeat_ma2_J = 값구하기(avg.repeat_ma2_value_J);
                avg.repeat_ma2_K = 값구하기(avg.repeat_ma2_value_K);
                avg.repeat_ma2_L = 값구하기(avg.repeat_ma2_value_L);
                avg.repeat_ma2_M = 값구하기(avg.repeat_ma2_value_M);
                avg.repeat_ma2_N = 값구하기(avg.repeat_ma2_value_N);

                avg.Rebal_ma1_A = 값구하기(avg.Rebal_ma1_value_A);
                avg.Rebal_ma1_B = 값구하기(avg.Rebal_ma1_value_B);
                avg.Rebal_ma1_C = 값구하기(avg.Rebal_ma1_value_C);
                avg.Rebal_ma1_D = 값구하기(avg.Rebal_ma1_value_D);
                avg.Rebal_ma1_E = 값구하기(avg.Rebal_ma1_value_E);
                avg.Rebal_ma1_F = 값구하기(avg.Rebal_ma1_value_F);
                avg.Rebal_ma1_G = 값구하기(avg.Rebal_ma1_value_G);

                avg.Rebal_ma2_A = 값구하기(avg.Rebal_ma2_value_A);
                avg.Rebal_ma2_B = 값구하기(avg.Rebal_ma2_value_B);
                avg.Rebal_ma2_C = 값구하기(avg.Rebal_ma2_value_C);
                avg.Rebal_ma2_D = 값구하기(avg.Rebal_ma2_value_D);
                avg.Rebal_ma2_E = 값구하기(avg.Rebal_ma2_value_E);
                avg.Rebal_ma2_F = 값구하기(avg.Rebal_ma2_value_F);
                avg.Rebal_ma2_G = 값구하기(avg.Rebal_ma2_value_G);


                avg.Liquidation_TS_ma_A = 값구하기(avg.Liquidation_TS_ma_value_A);
                avg.Liquidation_TS_ma_B = 값구하기(avg.Liquidation_TS_ma_value_B);
                avg.Liquidation_TS_ma_C = 값구하기(avg.Liquidation_TS_ma_value_C);

                avg.매매기간_TS_ma_A = 값구하기(avg.매매기간_TS_ma_value_A);
                avg.매매기간_TS_ma_B = 값구하기(avg.매매기간_TS_ma_value_B);
                avg.매매기간_TS_ma_C = 값구하기(avg.매매기간_TS_ma_value_C);
                avg.매매기간_TS_ma_D = 값구하기(avg.매매기간_TS_ma_value_D);
                avg.매매기간_TS_ma_E = 값구하기(avg.매매기간_TS_ma_value_E);
                avg.매매기간_TS_ma_F = 값구하기(avg.매매기간_TS_ma_value_F);

                double 값구하기(int 주기)
                {
                    string[] list = 잔고.일_리스트.Split(';');
                    double 평균합 = 0;

                    for (int i = 0; i < list.Length; i++)
                    {
                        if (i == 주기) break;
                        double.TryParse(list[i], out double result);
                        평균합 = 평균합 + result;
                    }

                    if (list.Length >= 주기)
                    {
                        return 평균합 / 주기;
                    }
                    else
                    {
                        return 평균합 / list.Length;
                    }
                }

                //if (잔고.종목명.Equals("국전약품"))
                //{
                //    Console.WriteLine("\nmarket_code: " + avg.code);
                //    Console.WriteLine("DateTime.Now: " + DateTime.Now.ToString("HHmmss"));

                //    Console.WriteLine("잔고.일_리스트 Length : " + 잔고.일_리스트.Split(';').Length);
                //    Console.WriteLine("잔고.일_리스트: " + 잔고.일_리스트);

                //    Console.WriteLine();
                //    Console.WriteLine("Rebal_dma1_A: " + avg.Rebal_ma1_A);
                //    Console.WriteLine("Rebal_dma1_B: " + avg.Rebal_ma1_B);
                //    Console.WriteLine("Rebal_dma1_C: " + avg.Rebal_ma1_C);
                //    Console.WriteLine("Rebal_dma1_D: " + avg.Rebal_ma1_D);
                //    Console.WriteLine("Rebal_dma1_E: " + avg.Rebal_ma1_E);
                //    Console.WriteLine("Rebal_dma1_F: " + avg.Rebal_ma1_F);
                //    Console.WriteLine("Rebal_dma1_G: " + avg.Rebal_ma1_G);
                //    Console.WriteLine();
                //    Console.WriteLine("Rebal_dma2_A: " + avg.Rebal_ma2_A);
                //    Console.WriteLine("Rebal_dma2_B: " + avg.Rebal_ma2_B);
                //    Console.WriteLine("Rebal_dma2_C: " + avg.Rebal_ma2_C);
                //    Console.WriteLine("Rebal_dma2_D: " + avg.Rebal_ma2_D);
                //    Console.WriteLine("Rebal_dma2_E: " + avg.Rebal_ma2_E);
                //    Console.WriteLine("Rebal_dma2_F: " + avg.Rebal_ma2_F);
                //    Console.WriteLine("Rebal_dma2_G: " + avg.Rebal_ma2_G);
                //    Console.WriteLine();
                //    Console.WriteLine("매매기간_TS_ma_A: " + avg.매매기간_TS_ma_A);
                //    Console.WriteLine("매매기간_TS_ma_B: " + avg.매매기간_TS_ma_B);
                //    Console.WriteLine("매매기간_TS_ma_C: " + avg.매매기간_TS_ma_C);
                //    Console.WriteLine("매매기간_TS_ma_D: " + avg.매매기간_TS_ma_D);
                //    Console.WriteLine("매매기간_TS_ma_E: " + avg.매매기간_TS_ma_E);
                //    Console.WriteLine("매매기간_TS_ma_F: " + avg.매매기간_TS_ma_F);
                //}
            }
        }

        public static void New_item(String 종목코드)
        {
            Form1.Min_ma_list.Add(종목코드,
                                new ma(종목코드,
                                     0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0
                                     ));

            Form1.Day_ma_list.Add(종목코드,
                                  new ma(종목코드,
                                       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0,
                                       0, 0, 0, 0, 0, 0
                                       ));

            change_value();
        }

        public static void change_value()
        {
            foreach (var key in Form1.Min_ma_list.ToList())
            {
                key.Value.repeat_ma1_value_A = Properties.Settings.Default.TB_repeat_mma_A;
                key.Value.repeat_ma1_value_B = Properties.Settings.Default.TB_repeat_mma_B;
                key.Value.repeat_ma1_value_C = Properties.Settings.Default.TB_repeat_mma_C;
                key.Value.repeat_ma1_value_D = Properties.Settings.Default.TB_repeat_mma_D;
                key.Value.repeat_ma1_value_E = Properties.Settings.Default.TB_repeat_mma_E;
                key.Value.repeat_ma1_value_F = Properties.Settings.Default.TB_repeat_mma_F;
                key.Value.repeat_ma1_value_G = Properties.Settings.Default.TB_repeat_mma_G;
                key.Value.repeat_ma1_value_H = Properties.Settings.Default.TB_repeat_mma_H;
                key.Value.repeat_ma1_value_I = Properties.Settings.Default.TB_repeat_mma_I;
                key.Value.repeat_ma1_value_J = Properties.Settings.Default.TB_repeat_mma_J;
                key.Value.repeat_ma1_value_K = Properties.Settings.Default.TB_repeat_mma_K;
                key.Value.repeat_ma1_value_L = Properties.Settings.Default.TB_repeat_mma_L;
                key.Value.repeat_ma1_value_M = Properties.Settings.Default.TB_repeat_mma_M;
                key.Value.repeat_ma1_value_N = Properties.Settings.Default.TB_repeat_mma_N;

                key.Value.repeat_ma2_value_A = Properties.Settings.Default.TB_repeat_mma2_A;
                key.Value.repeat_ma2_value_B = Properties.Settings.Default.TB_repeat_mma2_B;
                key.Value.repeat_ma2_value_C = Properties.Settings.Default.TB_repeat_mma2_C;
                key.Value.repeat_ma2_value_D = Properties.Settings.Default.TB_repeat_mma2_D;
                key.Value.repeat_ma2_value_E = Properties.Settings.Default.TB_repeat_mma2_E;
                key.Value.repeat_ma2_value_F = Properties.Settings.Default.TB_repeat_mma2_F;
                key.Value.repeat_ma2_value_G = Properties.Settings.Default.TB_repeat_mma2_G;
                key.Value.repeat_ma2_value_H = Properties.Settings.Default.TB_repeat_mma2_H;
                key.Value.repeat_ma2_value_I = Properties.Settings.Default.TB_repeat_mma2_I;
                key.Value.repeat_ma2_value_J = Properties.Settings.Default.TB_repeat_mma2_J;
                key.Value.repeat_ma2_value_K = Properties.Settings.Default.TB_repeat_mma2_K;
                key.Value.repeat_ma2_value_L = Properties.Settings.Default.TB_repeat_mma2_L;
                key.Value.repeat_ma2_value_M = Properties.Settings.Default.TB_repeat_mma2_M;
                key.Value.repeat_ma2_value_N = Properties.Settings.Default.TB_repeat_mma2_N;

                key.Value.Rebal_ma1_value_A = Properties.Settings.Default.TB_rebalance_mma_A;
                key.Value.Rebal_ma1_value_B = Properties.Settings.Default.TB_rebalance_mma_B;
                key.Value.Rebal_ma1_value_C = Properties.Settings.Default.TB_rebalance_mma_C;
                key.Value.Rebal_ma1_value_D = Properties.Settings.Default.TB_rebalance_mma_D;
                key.Value.Rebal_ma1_value_E = Properties.Settings.Default.TB_rebalance_mma_E;
                key.Value.Rebal_ma1_value_F = Properties.Settings.Default.TB_rebalance_mma_F;
                key.Value.Rebal_ma1_value_G = Properties.Settings.Default.TB_rebalance_mma_G;

                key.Value.Rebal_ma2_value_A = Properties.Settings.Default.TB_rebalance_mma2_A;
                key.Value.Rebal_ma2_value_B = Properties.Settings.Default.TB_rebalance_mma2_B;
                key.Value.Rebal_ma2_value_C = Properties.Settings.Default.TB_rebalance_mma2_C;
                key.Value.Rebal_ma2_value_D = Properties.Settings.Default.TB_rebalance_mma2_D;
                key.Value.Rebal_ma2_value_E = Properties.Settings.Default.TB_rebalance_mma2_E;
                key.Value.Rebal_ma2_value_F = Properties.Settings.Default.TB_rebalance_mma2_F;
                key.Value.Rebal_ma2_value_G = Properties.Settings.Default.TB_rebalance_mma2_G;

                key.Value.Rebal_TS_ma_1차_value_A = Properties.Settings.Default.TB_rebalance_TS_1차_mma_A;
                key.Value.Rebal_TS_ma_1차_value_B = Properties.Settings.Default.TB_rebalance_TS_1차_mma_B;
                key.Value.Rebal_TS_ma_1차_value_C = Properties.Settings.Default.TB_rebalance_TS_1차_mma_C;
                key.Value.Rebal_TS_ma_1차_value_D = Properties.Settings.Default.TB_rebalance_TS_1차_mma_D;
                key.Value.Rebal_TS_ma_1차_value_E = Properties.Settings.Default.TB_rebalance_TS_1차_mma_E;
                key.Value.Rebal_TS_ma_1차_value_F = Properties.Settings.Default.TB_rebalance_TS_1차_mma_F;
                key.Value.Rebal_TS_ma_1차_value_G = Properties.Settings.Default.TB_rebalance_TS_1차_mma_G;

                key.Value.Rebal_TS_ma_2차_value_A = Properties.Settings.Default.TB_rebalance_TS_2차_mma_A;
                key.Value.Rebal_TS_ma_2차_value_B = Properties.Settings.Default.TB_rebalance_TS_2차_mma_B;
                key.Value.Rebal_TS_ma_2차_value_C = Properties.Settings.Default.TB_rebalance_TS_2차_mma_C;
                key.Value.Rebal_TS_ma_2차_value_D = Properties.Settings.Default.TB_rebalance_TS_2차_mma_D;
                key.Value.Rebal_TS_ma_2차_value_E = Properties.Settings.Default.TB_rebalance_TS_2차_mma_E;
                key.Value.Rebal_TS_ma_2차_value_F = Properties.Settings.Default.TB_rebalance_TS_2차_mma_F;
                key.Value.Rebal_TS_ma_2차_value_G = Properties.Settings.Default.TB_rebalance_TS_2차_mma_G;

                key.Value.Liquidation_ma_value_A = Properties.Settings.Default.TB_Liquidation_mma_A;
                key.Value.Liquidation_ma_value_B = Properties.Settings.Default.TB_Liquidation_mma_B;
                key.Value.Liquidation_ma_value_C = Properties.Settings.Default.TB_Liquidation_mma_C;

                key.Value.Liquidation_TS_ma_value_A = Properties.Settings.Default.TB_Liquidation_TS_mma_A;
                key.Value.Liquidation_TS_ma_value_B = Properties.Settings.Default.TB_Liquidation_TS_mma_B;
                key.Value.Liquidation_TS_ma_value_C = Properties.Settings.Default.TB_Liquidation_TS_mma_C;

                key.Value.매매기간_TS_ma_value_A = Properties.Settings.Default.TB_매매기간_TS_mma_A;
                key.Value.매매기간_TS_ma_value_B = Properties.Settings.Default.TB_매매기간_TS_mma_B;
                key.Value.매매기간_TS_ma_value_C = Properties.Settings.Default.TB_매매기간_TS_mma_C;
                key.Value.매매기간_TS_ma_value_D = Properties.Settings.Default.TB_매매기간_TS_mma_D;
                key.Value.매매기간_TS_ma_value_E = Properties.Settings.Default.TB_매매기간_TS_mma_E;
                key.Value.매매기간_TS_ma_value_F = Properties.Settings.Default.TB_매매기간_TS_mma_F;
            }

            foreach (var key in Form1.Day_ma_list.ToList())
            {
                key.Value.repeat_ma1_value_A = Properties.Settings.Default.TB_repeat_dma1_A;
                key.Value.repeat_ma1_value_B = Properties.Settings.Default.TB_repeat_dma1_B;
                key.Value.repeat_ma1_value_C = Properties.Settings.Default.TB_repeat_dma1_C;
                key.Value.repeat_ma1_value_D = Properties.Settings.Default.TB_repeat_dma1_D;
                key.Value.repeat_ma1_value_E = Properties.Settings.Default.TB_repeat_dma1_E;
                key.Value.repeat_ma1_value_F = Properties.Settings.Default.TB_repeat_dma1_F;
                key.Value.repeat_ma1_value_G = Properties.Settings.Default.TB_repeat_dma1_G;
                key.Value.repeat_ma1_value_H = Properties.Settings.Default.TB_repeat_dma1_H;
                key.Value.repeat_ma1_value_I = Properties.Settings.Default.TB_repeat_dma1_I;
                key.Value.repeat_ma1_value_J = Properties.Settings.Default.TB_repeat_dma1_J;
                key.Value.repeat_ma1_value_K = Properties.Settings.Default.TB_repeat_dma1_K;
                key.Value.repeat_ma1_value_L = Properties.Settings.Default.TB_repeat_dma1_L;
                key.Value.repeat_ma1_value_M = Properties.Settings.Default.TB_repeat_dma1_M;
                key.Value.repeat_ma1_value_N = Properties.Settings.Default.TB_repeat_dma1_N;

                key.Value.repeat_ma2_value_A = Properties.Settings.Default.TB_repeat_dma2_A;
                key.Value.repeat_ma2_value_B = Properties.Settings.Default.TB_repeat_dma2_B;
                key.Value.repeat_ma2_value_C = Properties.Settings.Default.TB_repeat_dma2_C;
                key.Value.repeat_ma2_value_D = Properties.Settings.Default.TB_repeat_dma2_D;
                key.Value.repeat_ma2_value_E = Properties.Settings.Default.TB_repeat_dma2_E;
                key.Value.repeat_ma2_value_F = Properties.Settings.Default.TB_repeat_dma2_F;
                key.Value.repeat_ma2_value_G = Properties.Settings.Default.TB_repeat_dma2_G;
                key.Value.repeat_ma2_value_H = Properties.Settings.Default.TB_repeat_dma2_H;
                key.Value.repeat_ma2_value_I = Properties.Settings.Default.TB_repeat_dma2_I;
                key.Value.repeat_ma2_value_J = Properties.Settings.Default.TB_repeat_dma2_J;
                key.Value.repeat_ma2_value_K = Properties.Settings.Default.TB_repeat_dma2_K;
                key.Value.repeat_ma2_value_L = Properties.Settings.Default.TB_repeat_dma2_L;
                key.Value.repeat_ma2_value_M = Properties.Settings.Default.TB_repeat_dma2_M;
                key.Value.repeat_ma2_value_N = Properties.Settings.Default.TB_repeat_dma2_N;

                key.Value.Rebal_ma1_value_A = Properties.Settings.Default.TB_rebalance_dma1_A;
                key.Value.Rebal_ma1_value_B = Properties.Settings.Default.TB_rebalance_dma1_B;
                key.Value.Rebal_ma1_value_C = Properties.Settings.Default.TB_rebalance_dma1_C;
                key.Value.Rebal_ma1_value_D = Properties.Settings.Default.TB_rebalance_dma1_D;
                key.Value.Rebal_ma1_value_E = Properties.Settings.Default.TB_rebalance_dma1_E;
                key.Value.Rebal_ma1_value_F = Properties.Settings.Default.TB_rebalance_dma1_F;
                key.Value.Rebal_ma1_value_G = Properties.Settings.Default.TB_rebalance_dma1_G;

                key.Value.Rebal_ma2_value_A = Properties.Settings.Default.TB_rebalance_dma2_A;
                key.Value.Rebal_ma2_value_B = Properties.Settings.Default.TB_rebalance_dma2_B;
                key.Value.Rebal_ma2_value_C = Properties.Settings.Default.TB_rebalance_dma2_C;
                key.Value.Rebal_ma2_value_D = Properties.Settings.Default.TB_rebalance_dma2_D;
                key.Value.Rebal_ma2_value_E = Properties.Settings.Default.TB_rebalance_dma2_E;
                key.Value.Rebal_ma2_value_F = Properties.Settings.Default.TB_rebalance_dma2_F;
                key.Value.Rebal_ma2_value_G = Properties.Settings.Default.TB_rebalance_dma2_G;

                key.Value.Liquidation_TS_ma_value_A = Properties.Settings.Default.TB_Liquidation_TS_dma_A;
                key.Value.Liquidation_TS_ma_value_B = Properties.Settings.Default.TB_Liquidation_TS_dma_B;
                key.Value.Liquidation_TS_ma_value_C = Properties.Settings.Default.TB_Liquidation_TS_dma_C;

                key.Value.매매기간_TS_ma_value_A = Properties.Settings.Default.TB_매매기간_TS_dma_A;
                key.Value.매매기간_TS_ma_value_B = Properties.Settings.Default.TB_매매기간_TS_dma_B;
                key.Value.매매기간_TS_ma_value_C = Properties.Settings.Default.TB_매매기간_TS_dma_C;
                key.Value.매매기간_TS_ma_value_D = Properties.Settings.Default.TB_매매기간_TS_dma_D;
                key.Value.매매기간_TS_ma_value_E = Properties.Settings.Default.TB_매매기간_TS_dma_E;
                key.Value.매매기간_TS_ma_value_F = Properties.Settings.Default.TB_매매기간_TS_dma_F;
            }
        }

        public static bool Get_이평(Stockbalance 잔고, int CBB_mma1, int CBB_mma2, int CBB_배열, double ma1, double ma2)
        {
            bool result = false;

            if (CBB_mma1 == 0) result = true;
            if (CBB_mma1 == 1 && 잔고.현재가 > ma1) result = true;
            if (CBB_mma1 == 2 && 잔고.현재가 < ma1) result = true;

            if (result && CBB_mma2 == 0) result = true;
            if (result && CBB_mma2 == 1 && 잔고.현재가 > ma2) result = true;
            if (result && CBB_mma2 == 2 && 잔고.현재가 < ma2) result = true;

            if (CBB_mma1 != 0 && CBB_mma2 != 0)
            {
                if (result && CBB_배열 == 0) result = true;
                if (result && CBB_배열 == 1 && ma1 > ma2) result = true;
                if (result && CBB_배열 == 2 && ma1 < ma2) result = true;
            }

            return result;
        }

        public static bool Get_TS_이평(Stockbalance 잔고, int CBB_mma, double ma)
        {
            bool result = false;

            if (CBB_mma == 0) result = true;
            if (CBB_mma == 1 && 잔고.현재가 > ma) result = true;
            if (CBB_mma == 2 && 잔고.현재가 < ma) result = true;

            return result;
        }


    }
}
