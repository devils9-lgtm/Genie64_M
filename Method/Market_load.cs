using System;

namespace 지니_64
{
    public class Market_load
    {
        public static void play()
        {
            var axKHOpenAPI1 = Form1.form1.axKHOpenAPI1;
            var collection = Form1.form1.collection;

            Console.WriteLine("Market loding 시작  :: " + Form1.server + "    " + DateTime.Now.ToString("HH:mm:ss.fff"));

            Form1.동작_Log("Market loding 시작  :: " + Form1.server);

            string codeList_0 = axKHOpenAPI1.GetCodeListByMarket("0");
            string[] codeArray_0 = codeList_0.Split(';');

            for (int i = 0; i < codeArray_0.Length; i++)
            {
                string Code = codeArray_0[i];

                if (Code.Length > 0)
                {
                    string market = "P";
                    string Pname = Form1.form1.axKHOpenAPI1.GetMasterCodeName(Code);
                    int 종가 = int.Parse(axKHOpenAPI1.GetMasterLastPrice(Code));

                    if (Stock_Add(Pname))
                    {
                        if (!Form1.Market_Item_List.ContainsKey(Code))
                        {
                            if (Pname.Contains("KODEX ")) market = "E";
                            if (Pname.Contains("TIGER ")) market = "E";
                            if (Pname.Contains("KOSEF ")) market = "E";
                            if (Pname.Contains("RISE ")) market = "E";
                            if (Pname.Contains("ACE ")) market = "E";
                            if (Pname.Contains("SOL ")) market = "E";

                            Form1.Market_Item_List.Add(Code, new Market_Item(true, true, true, true, true, true, Pname, Code, market, 0, 0, 0, 0, 종가, 0, "정상", 0, Form1.timenow, 0, 0, false, 0, 0, Form1.timenow, 0, 0, false, 0, 0, Form1.timenow, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));

                            collection.Add(Pname);
                        }
                    }
                }
            }

            string codeList_10 = axKHOpenAPI1.GetCodeListByMarket("10");
            string[] codeArray_10 = codeList_10.Split(';');

            for (int n = 0; n < codeArray_10.Length; n++)
            {
                string Code = codeArray_10[n];

                if (Code.Length > 0)
                {
                    string market = "D";
                    string Dname = axKHOpenAPI1.GetMasterCodeName(Code);
                    int 종가 = int.Parse(axKHOpenAPI1.GetMasterLastPrice(Code));

                    if (Stock_Add(Dname))
                    {
                        if (!Form1.Market_Item_List.ContainsKey(Code))
                        {
                            Form1.Market_Item_List.Add(Code, new Market_Item(true, true, true, true, true, true, Dname, Code, market, 0, 0, 0, 0, 종가, 0, "정상", 0, Form1.timenow, 0, 0, false, 0, 0, Form1.timenow, 0, 0, false, 0, 0, Form1.timenow, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));

                            collection.Add(Dname);
                        }
                    }
                }
            }

            Form1.form1.TB_관심그룹_종목명.AutoCompleteCustomSource = collection;

            bool Stock_Add(string name)
            {
                bool 등록 = true;

                if (name.Contains("KIWOOM ")) 등록 = true;
                if (name.Contains("KODEX ")) 등록 = true;
                if (name.Contains("TIGER ")) 등록 = true;
                if (name.Contains("RISE ")) 등록 = true;
                if (name.Contains("ACE ")) 등록 = true;
                if (name.Contains("SOL ")) 등록 = true;

                if (name.Contains("KoAct ")) 등록 = false;
                if (name.Contains("1Q ")) 등록 = false;
                if (name.Contains("PLUS ")) 등록 = false;
                if (name.Contains("KoAct ")) 등록 = false;
                if (name.Contains("WON ")) 등록 = false;
                if (name.Contains("BNK ")) 등록 = false;
                if (name.Contains("ITF ")) 등록 = false;
                if (name.Contains("HK ")) 등록 = false;
                if (name.Contains("KCGI ")) 등록 = false;
                if (name.Contains("TRUSTON ")) 등록 = false;
                if (name.Contains("파워 ")) 등록 = false;
                if (name.Contains("DAISHIN343 ")) 등록 = false;
                if (name.Contains(" ETN")) 등록 = false;

                if (name.Contains("TREX ")) 등록 = false;
                if (name.Contains("ARIRANG ")) 등록 = false;
                if (name.Contains("마이다스 ")) 등록 = false;
                if (name.Contains("KTOP ")) 등록 = false;
                if (name.Contains("FOCUS ")) 등록 = false;
                if (name.Contains("마이티 ")) 등록 = false;
                if (name.Contains("HANARO ")) 등록 = false;
                if (name.Contains("TIMEFOLIO ")) 등록 = false;
                if (name.Contains("MASTER ")) 등록 = false;
                if (name.Contains("히어로즈 ")) 등록 = false;
                if (name.Contains("VITA ")) 등록 = false;
                if (name.Contains("WOORI ")) 등록 = false;
                if (name.Contains("UNICORN ")) 등록 = false;
                if (name.Contains("에셋플러스 ")) 등록 = false;
                if (name.Contains("대신343 ")) 등록 = false;
                if (name.Contains("스팩")) 등록 = false;
                if (name.Contains("한국ANKOR유전")) 등록 = false;

                //   if (등록) Console.WriteLine(" ETF :: " + name);

                return 등록;
            }

            string codeList_NXT = axKHOpenAPI1.GetCodeListByMarket("NXT");
            string[] codeArray_NXT = codeList_NXT.Split(';');

            for (int n = 0; n < codeArray_NXT.Length; n++)
            {
                string Code = codeArray_NXT[n];

                Form1.NXT_list.Add(new NXT(Code.Trim()));
            }

            Form1.동작_Log("NXT가능종목 불러오기 완료.");

            Console.WriteLine("Market loding 완료  :: " + Form1.server + "    " + DateTime.Now.ToString("HH:mm:ss.fff"));
            Form1.동작_Log("Market loding 완료  :: " + Form1.server);

         

        }

        public static void Add_AVG_jisu()
        {
            Form1.AVG_jisu.Add(new AVG_jisu(Properties.Settings.Default.CB_kospi_new_stop,
                                      Properties.Settings.Default.CB_kospi_add_stop,
                                      Properties.Settings.Default.CB_use_kospi_min_03,
                                      Properties.Settings.Default.CB_use_kospi_min_05,
                                      Properties.Settings.Default.CB_use_kospi_min_10,
                                      Properties.Settings.Default.CB_use_kospi_min_20,
                                      Properties.Settings.Default.CB_use_kospi_min_30,
                                      Properties.Settings.Default.CB_use_kospi_min_60,
                                      Properties.Settings.Default.CB_use_kospi_day_03,
                                      Properties.Settings.Default.CB_use_kospi_day_05,
                                      Properties.Settings.Default.CB_use_kospi_day_10,
                                      Properties.Settings.Default.CB_use_kospi_day_20,
                                      Properties.Settings.Default.CB_use_kospi_day_40,
                                      Properties.Settings.Default.CB_use_kospi_day_60,
                                      Properties.Settings.Default.CB_UD_kospi_min_03,
                                      Properties.Settings.Default.CB_UD_kospi_min_05,
                                      Properties.Settings.Default.CB_UD_kospi_min_10,
                                      Properties.Settings.Default.CB_UD_kospi_min_20,
                                      Properties.Settings.Default.CB_UD_kospi_min_30,
                                      Properties.Settings.Default.CB_UD_kospi_min_60,
                                      Properties.Settings.Default.CB_UD_kospi_day_03,
                                      Properties.Settings.Default.CB_UD_kospi_day_05,
                                      Properties.Settings.Default.CB_UD_kospi_day_10,
                                      Properties.Settings.Default.CB_UD_kospi_day_20,
                                      Properties.Settings.Default.CB_UD_kospi_day_40,
                                      Properties.Settings.Default.CB_UD_kospi_day_60,
                                      true, true, true, true, true, true, true, true, true, true, true, true,
                                      true, true, true, true, true, true, true, true, true, true, true, true));

            Form1.AVG_jisu.Add(new AVG_jisu(Properties.Settings.Default.CB_kosdaq_new_stop,
                                        Properties.Settings.Default.CB_kosdaq_add_stop,
                                        Properties.Settings.Default.CB_use_kosdaq_min_03,
                                        Properties.Settings.Default.CB_use_kosdaq_min_05,
                                        Properties.Settings.Default.CB_use_kosdaq_min_10,
                                        Properties.Settings.Default.CB_use_kosdaq_min_20,
                                        Properties.Settings.Default.CB_use_kosdaq_min_30,
                                        Properties.Settings.Default.CB_use_kosdaq_min_60,
                                        Properties.Settings.Default.CB_use_kosdaq_day_03,
                                        Properties.Settings.Default.CB_use_kosdaq_day_05,
                                        Properties.Settings.Default.CB_use_kosdaq_day_10,
                                        Properties.Settings.Default.CB_use_kosdaq_day_20,
                                        Properties.Settings.Default.CB_use_kosdaq_day_40,
                                        Properties.Settings.Default.CB_use_kosdaq_day_60,
                                        Properties.Settings.Default.CB_UD_kosdaq_min_03,
                                        Properties.Settings.Default.CB_UD_kosdaq_min_05,
                                        Properties.Settings.Default.CB_UD_kosdaq_min_10,
                                        Properties.Settings.Default.CB_UD_kosdaq_min_20,
                                        Properties.Settings.Default.CB_UD_kosdaq_min_30,
                                        Properties.Settings.Default.CB_UD_kosdaq_min_60,
                                        Properties.Settings.Default.CB_UD_kosdaq_day_03,
                                        Properties.Settings.Default.CB_UD_kosdaq_day_05,
                                        Properties.Settings.Default.CB_UD_kosdaq_day_10,
                                        Properties.Settings.Default.CB_UD_kosdaq_day_20,
                                        Properties.Settings.Default.CB_UD_kosdaq_day_40,
                                        Properties.Settings.Default.CB_UD_kosdaq_day_60,
                                        true, true, true, true, true, true, true, true, true, true, true, true,
                                        true, true, true, true, true, true, true, true, true, true, true, true));

        }
    }
}
