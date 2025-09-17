using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니_64
{
    class Condition_Management
    {
        public static void Condition_save()
        {
            if (Properties.Settings.Default.select_account != null)
            {
                string strFilePatth = Form1.startupPath + @"\Data\" + Form1.USER_ID + "__" + Properties.Settings.Default.select_account + "__\\검색식\\사용검색식.txt";

                StreamWriter writer_;

                writer_ = System.IO.File.CreateText(strFilePatth);

                //      FileInfo File_Check = new FileInfo(Form1.startupPath + @"\Data\" + Form1.USER_ID + "__" + Properties.Settings.Default.select_account + "__\\잔고\\투자원금_계좌TS.txt");
                //      if (File_Check.Exists)
                //      {
                //       writer_.Write("0 - ;\n");
                //   }
                //      else
                //      {
                writer_.Write("0 -" + "투자원금" + "^" + Properties.Settings.Default.MT_principal + "^" + Properties.Settings.Default.MT_sonik_price + "^" + Properties.Settings.Default.MT_buying_standard + "^" + Properties.Settings.Default.매수계산기준금 + "^" + Properties.Settings.Default.Today_매수기준금 + "^" + Properties.Settings.Default.손익계산기준금 + "^" + Properties.Settings.Default.Today_손익기준금 + ";\n");
                //    }

                writer_.Write("1 " + "계좌번호" + "^" + Properties.Settings.Default.select_account + ";\n");

                string 신규검색식 = Properties.Settings.Default.신규검색식;
                string 반복매매검색식 = Properties.Settings.Default.반복매매검색식;
                string 계좌관리검색식 = Properties.Settings.Default.계좌관리검색식;
                string 와치검색식 = Properties.Settings.Default.와치검색식;

                writer_.Write("2 " + "신규매수_A" + "^" + 신규검색식.Split('^')[0] + ";\n");
                writer_.Write("3 " + "신규매수_B" + "^" + 신규검색식.Split('^')[1] + ";\n");
                writer_.Write("4 " + "신규매수_C" + "^" + 신규검색식.Split('^')[2] + ";\n");

                writer_.Write("5 " + "반복매매_A" + "^" + 반복매매검색식.Split('^')[0] + ";\n");
                writer_.Write("6 " + "반복매매_B" + "^" + 반복매매검색식.Split('^')[1] + ";\n");
                writer_.Write("7 " + "반복매매_C" + "^" + 반복매매검색식.Split('^')[2] + ";\n");
                writer_.Write("8 " + "반복매매_D" + "^" + 반복매매검색식.Split('^')[3] + ";\n");
                writer_.Write("9 " + "반복매매_E" + "^" + 반복매매검색식.Split('^')[4] + ";\n");
                writer_.Write("10 " + "반복매매_F" + "^" + 반복매매검색식.Split('^')[5] + ";\n");
                writer_.Write("12 " + "반복매매_G" + "^" + 반복매매검색식.Split('^')[6] + ";\n");
                writer_.Write("13 " + "반복매매_H" + "^" + 반복매매검색식.Split('^')[7] + ";\n");
                writer_.Write("14 " + "반복매매_I" + "^" + 반복매매검색식.Split('^')[8] + ";\n");
                writer_.Write("15 " + "반복매매_J" + "^" + 반복매매검색식.Split('^')[9] + ";\n");
                writer_.Write("16 " + "반복매매_K" + "^" + 반복매매검색식.Split('^')[10] + ";\n");
                writer_.Write("17 " + "반복매매_L" + "^" + 반복매매검색식.Split('^')[11] + ";\n");
                writer_.Write("18 " + "반복매매_M" + "^" + 반복매매검색식.Split('^')[12] + ";\n");
                writer_.Write("19 " + "반복매매_N" + "^" + 반복매매검색식.Split('^')[13] + ";\n");

                writer_.Write("20 " + "Watch_추가_A" + "^" + 와치검색식.Split('^')[0] + ";\n");
                writer_.Write("21 " + "Watch_추가_B" + "^" + 와치검색식.Split('^')[1] + ";\n");
                writer_.Write("22 " + "Watch_추가_C" + "^" + 와치검색식.Split('^')[2] + ";\n");
                writer_.Write("23 " + "Watch_추가_D" + "^" + 와치검색식.Split('^')[3] + ";\n");

                writer_.Write("24 " + "리밸런싱_A" + "^" + 계좌관리검색식.Split('^')[0] + ";\n");
                writer_.Write("25 " + "리밸런싱_B" + "^" + 계좌관리검색식.Split('^')[1] + ";\n");
                writer_.Write("26 " + "리밸런싱_C" + "^" + 계좌관리검색식.Split('^')[2] + ";\n");
                writer_.Write("27 " + "리밸런싱_D" + "^" + 계좌관리검색식.Split('^')[3] + ";\n");
                writer_.Write("28 " + "리밸런싱_E" + "^" + 계좌관리검색식.Split('^')[4] + ";\n");
                writer_.Write("29 " + "리밸런싱_F" + "^" + 계좌관리검색식.Split('^')[5] + ";\n");
                writer_.Write("30 " + "리밸런싱_G" + "^" + 계좌관리검색식.Split('^')[6] + ";\n");

                writer_.Write("31 " + "청산_A" + "^" + 계좌관리검색식.Split('^')[7] + ";\n");
                writer_.Write("32 " + "청산_B" + "^" + 계좌관리검색식.Split('^')[8] + ";\n");
                writer_.Write("33 " + "청산_C" + "^" + 계좌관리검색식.Split('^')[9]);

                writer_.Close();

                Form1.시장가탐색 = Condition_Management.시장가대금탐색();
            }
        }

        public static void API_OnReceiveConditionVer()
        {
            Form1.비프음("실행");

            string ConditionLists = Form1.form1.axKHOpenAPI1.GetConditionNameList();

            string[] conditionArray = ConditionLists.Split(';');

            Form1.form1.ConditionList.Clear();

            foreach (string condition in conditionArray)
            {
                if (condition.Length > 0)
                {
                    string[] conditioninfo = condition.Split('^');
                    string index = conditioninfo[0];
                    string name = conditioninfo[1];

                    new Condition(int.Parse(index), name);
                    Condition Cd_name = new Condition(int.Parse(index), name);

                    Form1.form1.ConditionList.Add(Cd_name);
                }
            }

            if (Form1.로딩완료)
            {
                Form1.AutoClosingAlram("키움서버로 부터 검색식을 업데이트 받았습니다.", "검색식로딩", 5, "동작");
            }
        }

        public static void Condition_Add(object sender)
        {
            ComboBox combobox = sender as ComboBox;

            combobox.Items.Clear();

            추가("");
            if (Properties.Settings.Default.CB_매수탐색A) 추가(Properties.Settings.Default.TB_매수탐색A);
            if (Properties.Settings.Default.CB_매수탐색B) 추가(Properties.Settings.Default.TB_매수탐색B);
            if (Properties.Settings.Default.CB_매도탐색) 추가(Properties.Settings.Default.TB_매도탐색);
            for (int i = 0; i < Form1.form1.ConditionList.Count; i++)
            {
                추가(Form1.form1.ConditionList[i].name);
            }

            void 추가(string 식)
            {
                combobox.Items.Add(식);
            }
        }

        public static void Condition_TextChanged(object sender)
        {
            ComboBox combobox = sender as ComboBox;
            string 신규검색식 = Properties.Settings.Default.신규검색식;
            string 반복매매검색식 = Properties.Settings.Default.반복매매검색식;
            string 계좌관리검색식 = Properties.Settings.Default.계좌관리검색식;
            string 와치검색식 = Properties.Settings.Default.와치검색식;

            if (계좌관리검색식.Split('^').Length < 9)
            {
                계좌관리검색식 = 계좌관리검색식.Split('^')[0] + "^" + 계좌관리검색식.Split('^')[1] + "^" + 계좌관리검색식.Split('^')[2] + "^" + 계좌관리검색식.Split('^')[3] + "^" + 계좌관리검색식.Split('^')[4] + "^^^" + 계좌관리검색식.Split('^')[5] + "^" + 계좌관리검색식.Split('^')[6] + "^";
            }

            if (combobox.SelectedIndex == -1)
            {
                if (combobox.Name.Equals("combo_new_condition_A")) combobox.SelectedItem = 신규검색식.Split('^')[0];
                if (combobox.Name.Equals("combo_new_condition_B")) combobox.SelectedItem = 신규검색식.Split('^')[1];
                if (combobox.Name.Equals("combo_new_condition_C")) combobox.SelectedItem = 신규검색식.Split('^')[2];
                if (combobox.Name.Equals("combo_repeat_condition_A")) combobox.SelectedItem = 반복매매검색식.Split('^')[0];
                if (combobox.Name.Equals("combo_repeat_condition_B")) combobox.SelectedItem = 반복매매검색식.Split('^')[1];
                if (combobox.Name.Equals("combo_repeat_condition_C")) combobox.SelectedItem = 반복매매검색식.Split('^')[2];
                if (combobox.Name.Equals("combo_repeat_condition_D")) combobox.SelectedItem = 반복매매검색식.Split('^')[3];
                if (combobox.Name.Equals("combo_repeat_condition_E")) combobox.SelectedItem = 반복매매검색식.Split('^')[4];
                if (combobox.Name.Equals("combo_repeat_condition_F")) combobox.SelectedItem = 반복매매검색식.Split('^')[5];
                if (combobox.Name.Equals("combo_repeat_condition_G")) combobox.SelectedItem = 반복매매검색식.Split('^')[6];
                if (combobox.Name.Equals("combo_repeat_condition_H")) combobox.SelectedItem = 반복매매검색식.Split('^')[7];
                if (combobox.Name.Equals("combo_repeat_condition_I")) combobox.SelectedItem = 반복매매검색식.Split('^')[8];
                if (combobox.Name.Equals("combo_repeat_condition_J")) combobox.SelectedItem = 반복매매검색식.Split('^')[9];
                if (combobox.Name.Equals("combo_repeat_condition_K")) combobox.SelectedItem = 반복매매검색식.Split('^')[10];
                if (combobox.Name.Equals("combo_repeat_condition_L")) combobox.SelectedItem = 반복매매검색식.Split('^')[11];
                if (combobox.Name.Equals("combo_repeat_condition_M")) combobox.SelectedItem = 반복매매검색식.Split('^')[12];
                if (combobox.Name.Equals("combo_repeat_condition_N")) combobox.SelectedItem = 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_rebalance_condition_A")) combobox.SelectedItem = 계좌관리검색식.Split('^')[0];
                if (combobox.Name.Equals("combo_rebalance_condition_B")) combobox.SelectedItem = 계좌관리검색식.Split('^')[1];
                if (combobox.Name.Equals("combo_rebalance_condition_C")) combobox.SelectedItem = 계좌관리검색식.Split('^')[2];
                if (combobox.Name.Equals("combo_rebalance_condition_D")) combobox.SelectedItem = 계좌관리검색식.Split('^')[3];
                if (combobox.Name.Equals("combo_rebalance_condition_E")) combobox.SelectedItem = 계좌관리검색식.Split('^')[4];
                if (combobox.Name.Equals("combo_rebalance_condition_F")) combobox.SelectedItem = 계좌관리검색식.Split('^')[5];
                if (combobox.Name.Equals("combo_rebalance_condition_G")) combobox.SelectedItem = 계좌관리검색식.Split('^')[6];
                if (combobox.Name.Equals("CBB_Liquidation_condition_A")) combobox.SelectedItem = 계좌관리검색식.Split('^')[7];
                if (combobox.Name.Equals("CBB_Liquidation_condition_B")) combobox.SelectedItem = 계좌관리검색식.Split('^')[8];
                if (combobox.Name.Equals("CBB_Liquidation_condition_C")) combobox.SelectedItem = 계좌관리검색식.Split('^')[9];
                if (combobox.Name.Equals("combo_watch_condition_AA")) combobox.SelectedItem = 와치검색식.Split('^')[0];
                if (combobox.Name.Equals("combo_watch_condition_BB")) combobox.SelectedItem = 와치검색식.Split('^')[1];
                if (combobox.Name.Equals("combo_watch_condition_CC")) combobox.SelectedItem = 와치검색식.Split('^')[2];
                if (combobox.Name.Equals("combo_watch_condition_DD")) combobox.SelectedItem = 와치검색식.Split('^')[3];
                if (combobox.Name.Equals("CBB_관심검색식")) combobox.SelectedItem = Properties.Settings.Default.CBB_관심검색식;
            }
            else
            {
                string 검색식 = combobox.Text;
                if (combobox.Name.Equals("combo_new_condition_A")) Properties.Settings.Default.신규검색식 = 검색식 + "^" + 신규검색식.Split('^')[1] + "^" + 신규검색식.Split('^')[2];
                if (combobox.Name.Equals("combo_new_condition_B")) Properties.Settings.Default.신규검색식 = 신규검색식.Split('^')[0] + "^" + 검색식 + "^" + 신규검색식.Split('^')[2];
                if (combobox.Name.Equals("combo_new_condition_C")) Properties.Settings.Default.신규검색식 = 신규검색식.Split('^')[0] + "^" + 신규검색식.Split('^')[1] + "^" + 검색식;
                if (combobox.Name.Equals("combo_repeat_condition_A")) Properties.Settings.Default.반복매매검색식 = 검색식 + "^" + 반복매매검색식.Split('^')[1] + "^" + 반복매매검색식.Split('^')[2] + "^" + 반복매매검색식.Split('^')[3] + "^" + 반복매매검색식.Split('^')[4] + "^" + 반복매매검색식.Split('^')[5] + "^" + 반복매매검색식.Split('^')[6] + "^" + 반복매매검색식.Split('^')[7] + "^" + 반복매매검색식.Split('^')[8] + "^" + 반복매매검색식.Split('^')[9] + "^" + 반복매매검색식.Split('^')[10] + "^" + 반복매매검색식.Split('^')[11] + "^" + 반복매매검색식.Split('^')[12] + "^" + 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_repeat_condition_B")) Properties.Settings.Default.반복매매검색식 = 반복매매검색식.Split('^')[0] + "^" + 검색식 + "^" + 반복매매검색식.Split('^')[2] + "^" + 반복매매검색식.Split('^')[3] + "^" + 반복매매검색식.Split('^')[4] + "^" + 반복매매검색식.Split('^')[5] + "^" + 반복매매검색식.Split('^')[6] + "^" + 반복매매검색식.Split('^')[7] + "^" + 반복매매검색식.Split('^')[8] + "^" + 반복매매검색식.Split('^')[9] + "^" + 반복매매검색식.Split('^')[10] + "^" + 반복매매검색식.Split('^')[11] + "^" + 반복매매검색식.Split('^')[12] + "^" + 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_repeat_condition_C")) Properties.Settings.Default.반복매매검색식 = 반복매매검색식.Split('^')[0] + "^" + 반복매매검색식.Split('^')[1] + "^" + 검색식 + "^" + 반복매매검색식.Split('^')[3] + "^" + 반복매매검색식.Split('^')[4] + "^" + 반복매매검색식.Split('^')[5] + "^" + 반복매매검색식.Split('^')[6] + "^" + 반복매매검색식.Split('^')[7] + "^" + 반복매매검색식.Split('^')[8] + "^" + 반복매매검색식.Split('^')[9] + "^" + 반복매매검색식.Split('^')[10] + "^" + 반복매매검색식.Split('^')[11] + "^" + 반복매매검색식.Split('^')[12] + "^" + 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_repeat_condition_D")) Properties.Settings.Default.반복매매검색식 = 반복매매검색식.Split('^')[0] + "^" + 반복매매검색식.Split('^')[1] + "^" + 반복매매검색식.Split('^')[2] + "^" + 검색식 + "^" + 반복매매검색식.Split('^')[4] + "^" + 반복매매검색식.Split('^')[5] + "^" + 반복매매검색식.Split('^')[6] + "^" + 반복매매검색식.Split('^')[7] + "^" + 반복매매검색식.Split('^')[8] + "^" + 반복매매검색식.Split('^')[9] + "^" + 반복매매검색식.Split('^')[10] + "^" + 반복매매검색식.Split('^')[11] + "^" + 반복매매검색식.Split('^')[12] + "^" + 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_repeat_condition_E")) Properties.Settings.Default.반복매매검색식 = 반복매매검색식.Split('^')[0] + "^" + 반복매매검색식.Split('^')[1] + "^" + 반복매매검색식.Split('^')[2] + "^" + 반복매매검색식.Split('^')[3] + "^" + 검색식 + "^" + 반복매매검색식.Split('^')[5] + "^" + 반복매매검색식.Split('^')[6] + "^" + 반복매매검색식.Split('^')[7] + "^" + 반복매매검색식.Split('^')[8] + "^" + 반복매매검색식.Split('^')[9] + "^" + 반복매매검색식.Split('^')[10] + "^" + 반복매매검색식.Split('^')[11] + "^" + 반복매매검색식.Split('^')[12] + "^" + 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_repeat_condition_F")) Properties.Settings.Default.반복매매검색식 = 반복매매검색식.Split('^')[0] + "^" + 반복매매검색식.Split('^')[1] + "^" + 반복매매검색식.Split('^')[2] + "^" + 반복매매검색식.Split('^')[3] + "^" + 반복매매검색식.Split('^')[4] + "^" + 검색식 + "^" + 반복매매검색식.Split('^')[6] + "^" + 반복매매검색식.Split('^')[7] + "^" + 반복매매검색식.Split('^')[8] + "^" + 반복매매검색식.Split('^')[9] + "^" + 반복매매검색식.Split('^')[10] + "^" + 반복매매검색식.Split('^')[11] + "^" + 반복매매검색식.Split('^')[12] + "^" + 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_repeat_condition_G")) Properties.Settings.Default.반복매매검색식 = 반복매매검색식.Split('^')[0] + "^" + 반복매매검색식.Split('^')[1] + "^" + 반복매매검색식.Split('^')[2] + "^" + 반복매매검색식.Split('^')[3] + "^" + 반복매매검색식.Split('^')[4] + "^" + 반복매매검색식.Split('^')[5] + "^" + 검색식 + "^" + 반복매매검색식.Split('^')[7] + "^" + 반복매매검색식.Split('^')[8] + "^" + 반복매매검색식.Split('^')[9] + "^" + 반복매매검색식.Split('^')[10] + "^" + 반복매매검색식.Split('^')[11] + "^" + 반복매매검색식.Split('^')[12] + "^" + 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_repeat_condition_H")) Properties.Settings.Default.반복매매검색식 = 반복매매검색식.Split('^')[0] + "^" + 반복매매검색식.Split('^')[1] + "^" + 반복매매검색식.Split('^')[2] + "^" + 반복매매검색식.Split('^')[3] + "^" + 반복매매검색식.Split('^')[4] + "^" + 반복매매검색식.Split('^')[5] + "^" + 반복매매검색식.Split('^')[6] + "^" + 검색식 + "^" + 반복매매검색식.Split('^')[8] + "^" + 반복매매검색식.Split('^')[9] + "^" + 반복매매검색식.Split('^')[10] + "^" + 반복매매검색식.Split('^')[11] + "^" + 반복매매검색식.Split('^')[12] + "^" + 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_repeat_condition_I")) Properties.Settings.Default.반복매매검색식 = 반복매매검색식.Split('^')[0] + "^" + 반복매매검색식.Split('^')[1] + "^" + 반복매매검색식.Split('^')[2] + "^" + 반복매매검색식.Split('^')[3] + "^" + 반복매매검색식.Split('^')[4] + "^" + 반복매매검색식.Split('^')[5] + "^" + 반복매매검색식.Split('^')[6] + "^" + 반복매매검색식.Split('^')[7] + "^" + 검색식 + "^" + 반복매매검색식.Split('^')[9] + "^" + 반복매매검색식.Split('^')[10] + "^" + 반복매매검색식.Split('^')[11] + "^" + 반복매매검색식.Split('^')[12] + "^" + 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_repeat_condition_J")) Properties.Settings.Default.반복매매검색식 = 반복매매검색식.Split('^')[0] + "^" + 반복매매검색식.Split('^')[1] + "^" + 반복매매검색식.Split('^')[2] + "^" + 반복매매검색식.Split('^')[3] + "^" + 반복매매검색식.Split('^')[4] + "^" + 반복매매검색식.Split('^')[5] + "^" + 반복매매검색식.Split('^')[6] + "^" + 반복매매검색식.Split('^')[7] + "^" + 반복매매검색식.Split('^')[8] + "^" + 검색식 + "^" + 반복매매검색식.Split('^')[10] + "^" + 반복매매검색식.Split('^')[11] + "^" + 반복매매검색식.Split('^')[12] + "^" + 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_repeat_condition_K")) Properties.Settings.Default.반복매매검색식 = 반복매매검색식.Split('^')[0] + "^" + 반복매매검색식.Split('^')[1] + "^" + 반복매매검색식.Split('^')[2] + "^" + 반복매매검색식.Split('^')[3] + "^" + 반복매매검색식.Split('^')[4] + "^" + 반복매매검색식.Split('^')[5] + "^" + 반복매매검색식.Split('^')[6] + "^" + 반복매매검색식.Split('^')[7] + "^" + 반복매매검색식.Split('^')[8] + "^" + 반복매매검색식.Split('^')[9] + "^" + 검색식 + "^" + 반복매매검색식.Split('^')[11] + "^" + 반복매매검색식.Split('^')[12] + "^" + 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_repeat_condition_L")) Properties.Settings.Default.반복매매검색식 = 반복매매검색식.Split('^')[0] + "^" + 반복매매검색식.Split('^')[1] + "^" + 반복매매검색식.Split('^')[2] + "^" + 반복매매검색식.Split('^')[3] + "^" + 반복매매검색식.Split('^')[4] + "^" + 반복매매검색식.Split('^')[5] + "^" + 반복매매검색식.Split('^')[6] + "^" + 반복매매검색식.Split('^')[7] + "^" + 반복매매검색식.Split('^')[8] + "^" + 반복매매검색식.Split('^')[9] + "^" + 반복매매검색식.Split('^')[10] + "^" + 검색식 + "^" + 반복매매검색식.Split('^')[12] + "^" + 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_repeat_condition_M")) Properties.Settings.Default.반복매매검색식 = 반복매매검색식.Split('^')[0] + "^" + 반복매매검색식.Split('^')[1] + "^" + 반복매매검색식.Split('^')[2] + "^" + 반복매매검색식.Split('^')[3] + "^" + 반복매매검색식.Split('^')[4] + "^" + 반복매매검색식.Split('^')[5] + "^" + 반복매매검색식.Split('^')[6] + "^" + 반복매매검색식.Split('^')[7] + "^" + 반복매매검색식.Split('^')[8] + "^" + 반복매매검색식.Split('^')[9] + "^" + 반복매매검색식.Split('^')[10] + "^" + 반복매매검색식.Split('^')[11] + "^" + 검색식 + "^" + 반복매매검색식.Split('^')[13];
                if (combobox.Name.Equals("combo_repeat_condition_N")) Properties.Settings.Default.반복매매검색식 = 반복매매검색식.Split('^')[0] + "^" + 반복매매검색식.Split('^')[1] + "^" + 반복매매검색식.Split('^')[2] + "^" + 반복매매검색식.Split('^')[3] + "^" + 반복매매검색식.Split('^')[4] + "^" + 반복매매검색식.Split('^')[5] + "^" + 반복매매검색식.Split('^')[6] + "^" + 반복매매검색식.Split('^')[7] + "^" + 반복매매검색식.Split('^')[8] + "^" + 반복매매검색식.Split('^')[9] + "^" + 반복매매검색식.Split('^')[10] + "^" + 반복매매검색식.Split('^')[11] + "^" + 반복매매검색식.Split('^')[12] + "^" + 검색식;
                if (combobox.Name.Equals("combo_rebalance_condition_A")) Properties.Settings.Default.계좌관리검색식 = 검색식 + "^" + 계좌관리검색식.Split('^')[1] + "^" + 계좌관리검색식.Split('^')[2] + "^" + 계좌관리검색식.Split('^')[3] + "^" + 계좌관리검색식.Split('^')[4] + "^" + 계좌관리검색식.Split('^')[5] + "^" + 계좌관리검색식.Split('^')[6] + "^" + 계좌관리검색식.Split('^')[7] + "^" + 계좌관리검색식.Split('^')[8] + "^" + 계좌관리검색식.Split('^')[9];
                if (combobox.Name.Equals("combo_rebalance_condition_B")) Properties.Settings.Default.계좌관리검색식 = 계좌관리검색식.Split('^')[0] + "^" + 검색식 + "^" + 계좌관리검색식.Split('^')[2] + "^" + 계좌관리검색식.Split('^')[3] + "^" + 계좌관리검색식.Split('^')[4] + "^" + 계좌관리검색식.Split('^')[5] + "^" + 계좌관리검색식.Split('^')[6] + "^" + 계좌관리검색식.Split('^')[7] + "^" + 계좌관리검색식.Split('^')[8] + "^" + 계좌관리검색식.Split('^')[9];
                if (combobox.Name.Equals("combo_rebalance_condition_C")) Properties.Settings.Default.계좌관리검색식 = 계좌관리검색식.Split('^')[0] + "^" + 계좌관리검색식.Split('^')[1] + "^" + 검색식 + "^" + 계좌관리검색식.Split('^')[3] + "^" + 계좌관리검색식.Split('^')[4] + "^" + 계좌관리검색식.Split('^')[5] + "^" + 계좌관리검색식.Split('^')[6] + "^" + 계좌관리검색식.Split('^')[7] + "^" + 계좌관리검색식.Split('^')[8] + "^" + 계좌관리검색식.Split('^')[9];
                if (combobox.Name.Equals("combo_rebalance_condition_D")) Properties.Settings.Default.계좌관리검색식 = 계좌관리검색식.Split('^')[0] + "^" + 계좌관리검색식.Split('^')[1] + "^" + 계좌관리검색식.Split('^')[2] + "^" + 검색식 + "^" + 계좌관리검색식.Split('^')[4] + "^" + 계좌관리검색식.Split('^')[5] + "^" + 계좌관리검색식.Split('^')[6] + "^" + 계좌관리검색식.Split('^')[7] + "^" + 계좌관리검색식.Split('^')[8] + "^" + 계좌관리검색식.Split('^')[9];
                if (combobox.Name.Equals("combo_rebalance_condition_E")) Properties.Settings.Default.계좌관리검색식 = 계좌관리검색식.Split('^')[0] + "^" + 계좌관리검색식.Split('^')[1] + "^" + 계좌관리검색식.Split('^')[2] + "^" + 계좌관리검색식.Split('^')[3] + "^" + 검색식 + "^" + 계좌관리검색식.Split('^')[5] + "^" + 계좌관리검색식.Split('^')[6] + "^" + 계좌관리검색식.Split('^')[7] + "^" + 계좌관리검색식.Split('^')[8] + "^" + 계좌관리검색식.Split('^')[9];
                if (combobox.Name.Equals("combo_rebalance_condition_F")) Properties.Settings.Default.계좌관리검색식 = 계좌관리검색식.Split('^')[0] + "^" + 계좌관리검색식.Split('^')[1] + "^" + 계좌관리검색식.Split('^')[2] + "^" + 계좌관리검색식.Split('^')[3] + "^" + 계좌관리검색식.Split('^')[4] + "^" + 검색식 + "^" + 계좌관리검색식.Split('^')[6] + "^" + 계좌관리검색식.Split('^')[7] + "^" + 계좌관리검색식.Split('^')[8] + "^" + 계좌관리검색식.Split('^')[9];
                if (combobox.Name.Equals("combo_rebalance_condition_G")) Properties.Settings.Default.계좌관리검색식 = 계좌관리검색식.Split('^')[0] + "^" + 계좌관리검색식.Split('^')[1] + "^" + 계좌관리검색식.Split('^')[2] + "^" + 계좌관리검색식.Split('^')[3] + "^" + 계좌관리검색식.Split('^')[4] + "^" + 계좌관리검색식.Split('^')[5] + "^" + 검색식 + "^" + 계좌관리검색식.Split('^')[7] + "^" + 계좌관리검색식.Split('^')[8] + "^" + 계좌관리검색식.Split('^')[9];
                if (combobox.Name.Equals("CBB_Liquidation_condition_A")) Properties.Settings.Default.계좌관리검색식 = 계좌관리검색식.Split('^')[0] + "^" + 계좌관리검색식.Split('^')[1] + "^" + 계좌관리검색식.Split('^')[2] + "^" + 계좌관리검색식.Split('^')[3] + "^" + 계좌관리검색식.Split('^')[4] + "^" + 계좌관리검색식.Split('^')[5] + "^" + 계좌관리검색식.Split('^')[6] + "^" + 검색식 + "^" + 계좌관리검색식.Split('^')[8] + "^" + 계좌관리검색식.Split('^')[9];
                if (combobox.Name.Equals("CBB_Liquidation_condition_B")) Properties.Settings.Default.계좌관리검색식 = 계좌관리검색식.Split('^')[0] + "^" + 계좌관리검색식.Split('^')[1] + "^" + 계좌관리검색식.Split('^')[2] + "^" + 계좌관리검색식.Split('^')[3] + "^" + 계좌관리검색식.Split('^')[4] + "^" + 계좌관리검색식.Split('^')[5] + "^" + 계좌관리검색식.Split('^')[6] + "^" + 계좌관리검색식.Split('^')[7] + "^" + 검색식 + "^" + 계좌관리검색식.Split('^')[9];
                if (combobox.Name.Equals("CBB_Liquidation_condition_C")) Properties.Settings.Default.계좌관리검색식 = 계좌관리검색식.Split('^')[0] + "^" + 계좌관리검색식.Split('^')[1] + "^" + 계좌관리검색식.Split('^')[2] + "^" + 계좌관리검색식.Split('^')[3] + "^" + 계좌관리검색식.Split('^')[4] + "^" + 계좌관리검색식.Split('^')[5] + "^" + 계좌관리검색식.Split('^')[6] + "^" + 계좌관리검색식.Split('^')[7] + "^" + 계좌관리검색식.Split('^')[8] + "^" + 검색식;
                if (combobox.Name.Equals("combo_watch_condition_AA")) Properties.Settings.Default.와치검색식 = 검색식 + "^" + 와치검색식.Split('^')[1] + "^" + 와치검색식.Split('^')[2] + "^" + 와치검색식.Split('^')[3];
                if (combobox.Name.Equals("combo_watch_condition_BB")) Properties.Settings.Default.와치검색식 = 와치검색식.Split('^')[0] + "^" + 검색식 + "^" + 와치검색식.Split('^')[2] + "^" + 와치검색식.Split('^')[3];
                if (combobox.Name.Equals("combo_watch_condition_CC")) Properties.Settings.Default.와치검색식 = 와치검색식.Split('^')[0] + "^" + 와치검색식.Split('^')[1] + "^" + 검색식 + "^" + 와치검색식.Split('^')[3];
                if (combobox.Name.Equals("combo_watch_condition_DD")) Properties.Settings.Default.와치검색식 = 와치검색식.Split('^')[0] + "^" + 와치검색식.Split('^')[1] + "^" + 와치검색식.Split('^')[2] + "^" + 검색식;
                if (combobox.Name.Equals("CBB_관심검색식")) Properties.Settings.Default.CBB_관심검색식 = 검색식;
            }
        }


        public static void Condition_DataLoad(string 계좌번호) // 계좌 번호에 따른 조건식 불러오기
        {
            if (Form1.form1.CBB_관심검색식.Items.Contains(Properties.Settings.Default.CBB_관심검색식)) Form1.form1.CBB_관심검색식.SelectedItem = Properties.Settings.Default.CBB_관심검색식;
            else Form1.form1.CBB_관심검색식.SelectedIndex = -1;

            FileInfo File_Check = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Data\" + Form1.USER_ID + "__" + 계좌번호 + "__\\검색식\\사용검색식.txt");

            if (File_Check.Exists)
            {
                string path = System.Windows.Forms.Application.StartupPath + @"\Data\" + Form1.USER_ID + "__" + 계좌번호 + "__\\검색식\\사용검색식.txt";
                string OptionLists = File.ReadAllText(path);

                if (OptionLists.Contains(계좌번호)) // 조건식 저장값 불러오기 
                {
                    string[] 검색식 = OptionLists.Split(';');

                    Properties.Settings.Default.combo_new_condition_A = 검색식[2].Split('^')[1];
                    Properties.Settings.Default.combo_new_condition_B = 검색식[3].Split('^')[1];
                    Properties.Settings.Default.combo_new_condition_C = 검색식[4].Split('^')[1];

                    Properties.Settings.Default.combo_repeat_condition_A = 검색식[5].Split('^')[1];
                    Properties.Settings.Default.combo_repeat_condition_B = 검색식[6].Split('^')[1];
                    Properties.Settings.Default.combo_repeat_condition_C = 검색식[7].Split('^')[1];
                    Properties.Settings.Default.combo_repeat_condition_D = 검색식[8].Split('^')[1];
                    Properties.Settings.Default.combo_repeat_condition_E = 검색식[9].Split('^')[1];
                    Properties.Settings.Default.combo_repeat_condition_F = 검색식[10].Split('^')[1];
                    Properties.Settings.Default.combo_repeat_condition_G = 검색식[11].Split('^')[1];
                    Properties.Settings.Default.combo_repeat_condition_H = 검색식[12].Split('^')[1];
                    Properties.Settings.Default.combo_repeat_condition_I = 검색식[13].Split('^')[1];
                    Properties.Settings.Default.combo_repeat_condition_J = 검색식[14].Split('^')[1];
                    Properties.Settings.Default.combo_repeat_condition_K = 검색식[15].Split('^')[1];
                    Properties.Settings.Default.combo_repeat_condition_L = 검색식[16].Split('^')[1];
                    Properties.Settings.Default.combo_repeat_condition_M = 검색식[17].Split('^')[1];
                    Properties.Settings.Default.combo_repeat_condition_N = 검색식[18].Split('^')[1];
                    Properties.Settings.Default.combo_watch_condition_AA = 검색식[19].Split('^')[1];
                    Properties.Settings.Default.combo_watch_condition_BB = 검색식[20].Split('^')[1];
                    Properties.Settings.Default.combo_watch_condition_CC = 검색식[21].Split('^')[1];
                    Properties.Settings.Default.combo_watch_condition_DD = 검색식[22].Split('^')[1];
                    Properties.Settings.Default.combo_rebalance_condition_A = 검색식[23].Split('^')[1];
                    Properties.Settings.Default.combo_rebalance_condition_B = 검색식[24].Split('^')[1];
                    Properties.Settings.Default.combo_rebalance_condition_C = 검색식[25].Split('^')[1];
                    Properties.Settings.Default.combo_rebalance_condition_D = 검색식[26].Split('^')[1];
                    Properties.Settings.Default.combo_rebalance_condition_E = 검색식[27].Split('^')[1];
                    Properties.Settings.Default.combo_rebalance_condition_F = 검색식[28].Split('^')[1];
                    Properties.Settings.Default.combo_rebalance_condition_G = 검색식[29].Split('^')[1];
                    Properties.Settings.Default.CBB_Liquidation_condition_A = 검색식[30].Split('^')[1];
                    Properties.Settings.Default.CBB_Liquidation_condition_B = 검색식[31].Split('^')[1];
                    Properties.Settings.Default.CBB_Liquidation_condition_C = 검색식[32].Split('^')[1];

                    Form1.form1.combo_watch_condition_AA.Items.Add(Properties.Settings.Default.combo_watch_condition_AA); if (condition_check(Properties.Settings.Default.combo_watch_condition_AA)) { Form1.form1.combo_watch_condition_AA.Text = Properties.Settings.Default.combo_watch_condition_AA; } else { Properties.Settings.Default.combo_watch_condition_AA = ""; }
                    Form1.form1.combo_watch_condition_BB.Items.Add(Properties.Settings.Default.combo_watch_condition_BB); if (condition_check(Properties.Settings.Default.combo_watch_condition_BB)) { Form1.form1.combo_watch_condition_BB.Text = Properties.Settings.Default.combo_watch_condition_BB; } else { Properties.Settings.Default.combo_watch_condition_BB = ""; }
                    Form1.form1.combo_watch_condition_CC.Items.Add(Properties.Settings.Default.combo_watch_condition_CC); if (condition_check(Properties.Settings.Default.combo_watch_condition_CC)) { Form1.form1.combo_watch_condition_CC.Text = Properties.Settings.Default.combo_watch_condition_CC; } else { Properties.Settings.Default.combo_watch_condition_CC = ""; }
                    Form1.form1.combo_watch_condition_DD.Items.Add(Properties.Settings.Default.combo_watch_condition_DD); if (condition_check(Properties.Settings.Default.combo_watch_condition_DD)) { Form1.form1.combo_watch_condition_DD.Text = Properties.Settings.Default.combo_watch_condition_DD; } else { Properties.Settings.Default.combo_watch_condition_DD = ""; }

                    if (Properties.Settings.Default.CB_new_A) if (!condition_Run(Properties.Settings.Default.combo_new_condition_A, "신규_A")) { Properties.Settings.Default.combo_new_condition_A = ""; Properties.Settings.Default.CB_new_A = false; }
                    if (Properties.Settings.Default.CB_new_B) if (!condition_Run(Properties.Settings.Default.combo_new_condition_B, "신규_B")) { Properties.Settings.Default.combo_new_condition_B = ""; Properties.Settings.Default.CB_new_B = false; }
                    if (Properties.Settings.Default.CB_new_C) if (!condition_Run(Properties.Settings.Default.combo_new_condition_C, "신규_C")) { Properties.Settings.Default.combo_new_condition_C = ""; Properties.Settings.Default.CB_new_C = false; }

                    if (Properties.Settings.Default.combo_repeat_use_condition_A > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_A, "반복_A")) { Properties.Settings.Default.combo_repeat_condition_A = ""; Properties.Settings.Default.combo_repeat_use_condition_A = 0; Properties.Settings.Default.CB_repeat_use_A = false; }
                    if (Properties.Settings.Default.combo_repeat_use_condition_B > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_B, "반복_B")) { Properties.Settings.Default.combo_repeat_condition_B = ""; Properties.Settings.Default.combo_repeat_use_condition_B = 0; Properties.Settings.Default.CB_repeat_use_B = false; }
                    if (Properties.Settings.Default.combo_repeat_use_condition_C > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_C, "반복_C")) { Properties.Settings.Default.combo_repeat_condition_C = ""; Properties.Settings.Default.combo_repeat_use_condition_C = 0; Properties.Settings.Default.CB_repeat_use_C = false; }
                    if (Properties.Settings.Default.combo_repeat_use_condition_D > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_D, "반복_D")) { Properties.Settings.Default.combo_repeat_condition_D = ""; Properties.Settings.Default.combo_repeat_use_condition_D = 0; Properties.Settings.Default.CB_repeat_use_D = false; }
                    if (Properties.Settings.Default.combo_repeat_use_condition_E > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_E, "반복_E")) { Properties.Settings.Default.combo_repeat_condition_E = ""; Properties.Settings.Default.combo_repeat_use_condition_E = 0; Properties.Settings.Default.CB_repeat_use_E = false; }
                    if (Properties.Settings.Default.combo_repeat_use_condition_F > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_F, "반복_F")) { Properties.Settings.Default.combo_repeat_condition_F = ""; Properties.Settings.Default.combo_repeat_use_condition_F = 0; Properties.Settings.Default.CB_repeat_use_F = false; }
                    if (Properties.Settings.Default.combo_repeat_use_condition_G > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_G, "반복_G")) { Properties.Settings.Default.combo_repeat_condition_G = ""; Properties.Settings.Default.combo_repeat_use_condition_G = 0; Properties.Settings.Default.CB_repeat_use_G = false; }
                    if (Properties.Settings.Default.combo_repeat_use_condition_H > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_H, "반복_H")) { Properties.Settings.Default.combo_repeat_condition_H = ""; Properties.Settings.Default.combo_repeat_use_condition_H = 0; Properties.Settings.Default.CB_repeat_use_H = false; }
                    if (Properties.Settings.Default.combo_repeat_use_condition_I > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_I, "반복_I")) { Properties.Settings.Default.combo_repeat_condition_I = ""; Properties.Settings.Default.combo_repeat_use_condition_I = 0; Properties.Settings.Default.CB_repeat_use_I = false; }
                    if (Properties.Settings.Default.combo_repeat_use_condition_J > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_J, "반복_J")) { Properties.Settings.Default.combo_repeat_condition_J = ""; Properties.Settings.Default.combo_repeat_use_condition_J = 0; Properties.Settings.Default.CB_repeat_use_J = false; }
                    if (Properties.Settings.Default.combo_repeat_use_condition_K > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_K, "반복_K")) { Properties.Settings.Default.combo_repeat_condition_K = ""; Properties.Settings.Default.combo_repeat_use_condition_K = 0; Properties.Settings.Default.CB_repeat_use_K = false; }
                    if (Properties.Settings.Default.combo_repeat_use_condition_L > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_L, "반복_L")) { Properties.Settings.Default.combo_repeat_condition_L = ""; Properties.Settings.Default.combo_repeat_use_condition_L = 0; Properties.Settings.Default.CB_repeat_use_L = false; }
                    if (Properties.Settings.Default.combo_repeat_use_condition_M > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_M, "반복_M")) { Properties.Settings.Default.combo_repeat_condition_M = ""; Properties.Settings.Default.combo_repeat_use_condition_M = 0; Properties.Settings.Default.CB_repeat_use_M = false; }
                    if (Properties.Settings.Default.combo_repeat_use_condition_N > 0) if (!condition_Run(Properties.Settings.Default.combo_repeat_condition_N, "반복_N")) { Properties.Settings.Default.combo_repeat_condition_N = ""; Properties.Settings.Default.combo_repeat_use_condition_N = 0; Properties.Settings.Default.CB_repeat_use_N = false; }
                    if (Properties.Settings.Default.combo_rebalance_use_condition_A > 0) if (!condition_Run(Properties.Settings.Default.combo_rebalance_condition_A, "리밸_A")) { Properties.Settings.Default.combo_rebalance_condition_A = ""; Properties.Settings.Default.combo_rebalance_use_condition_A = 0; Properties.Settings.Default.CB_rebalance_A = false; }
                    if (Properties.Settings.Default.combo_rebalance_use_condition_B > 0) if (!condition_Run(Properties.Settings.Default.combo_rebalance_condition_B, "리밸_B")) { Properties.Settings.Default.combo_rebalance_condition_B = ""; Properties.Settings.Default.combo_rebalance_use_condition_B = 0; Properties.Settings.Default.CB_rebalance_B = false; }
                    if (Properties.Settings.Default.combo_rebalance_use_condition_C > 0) if (!condition_Run(Properties.Settings.Default.combo_rebalance_condition_C, "리밸_C")) { Properties.Settings.Default.combo_rebalance_condition_C = ""; Properties.Settings.Default.combo_rebalance_use_condition_C = 0; Properties.Settings.Default.CB_rebalance_C = false; }
                    if (Properties.Settings.Default.combo_rebalance_use_condition_D > 0) if (!condition_Run(Properties.Settings.Default.combo_rebalance_condition_D, "리밸_D")) { Properties.Settings.Default.combo_rebalance_condition_D = ""; Properties.Settings.Default.combo_rebalance_use_condition_D = 0; Properties.Settings.Default.CB_rebalance_D = false; }
                    if (Properties.Settings.Default.combo_rebalance_use_condition_E > 0) if (!condition_Run(Properties.Settings.Default.combo_rebalance_condition_E, "리밸_E")) { Properties.Settings.Default.combo_rebalance_condition_E = ""; Properties.Settings.Default.combo_rebalance_use_condition_E = 0; Properties.Settings.Default.CB_rebalance_E = false; }
                    if (Properties.Settings.Default.combo_rebalance_use_condition_F > 0) if (!condition_Run(Properties.Settings.Default.combo_rebalance_condition_F, "리밸_F")) { Properties.Settings.Default.combo_rebalance_condition_F = ""; Properties.Settings.Default.combo_rebalance_use_condition_F = 0; Properties.Settings.Default.CB_rebalance_F = false; }
                    if (Properties.Settings.Default.combo_rebalance_use_condition_G > 0) if (!condition_Run(Properties.Settings.Default.combo_rebalance_condition_G, "리밸_G")) { Properties.Settings.Default.combo_rebalance_condition_G = ""; Properties.Settings.Default.combo_rebalance_use_condition_G = 0; Properties.Settings.Default.CB_rebalance_G = false; }
                    if (Properties.Settings.Default.CBB_Liquidation_use_condition_A > 0) if (!condition_Run(Properties.Settings.Default.CBB_Liquidation_condition_A, "청산_A")) { Properties.Settings.Default.CBB_Liquidation_condition_A = ""; Properties.Settings.Default.CBB_Liquidation_use_condition_A = 0; Properties.Settings.Default.CB_Liquidation_A = false; }
                    if (Properties.Settings.Default.CBB_Liquidation_use_condition_B > 0) if (!condition_Run(Properties.Settings.Default.CBB_Liquidation_condition_B, "청산_B")) { Properties.Settings.Default.CBB_Liquidation_condition_B = ""; Properties.Settings.Default.CBB_Liquidation_use_condition_B = 0; Properties.Settings.Default.CB_Liquidation_B = false; }
                    if (Properties.Settings.Default.CBB_Liquidation_use_condition_C > 0) if (!condition_Run(Properties.Settings.Default.CBB_Liquidation_condition_C, "청산_C")) { Properties.Settings.Default.CBB_Liquidation_condition_C = ""; Properties.Settings.Default.CBB_Liquidation_use_condition_C = 0; Properties.Settings.Default.CB_Liquidation_C = false; }

                    bool condition_Run(string condtion, string position)
                    {
                        bool result = false;
                        Condition start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(condtion));
                        if (start_condition != null) { Start_Monitoring(start_condition, position); result = true; }
                        if (Properties.Settings.Default.CB_매수탐색A && Properties.Settings.Default.TB_매수탐색A.Equals(condtion)) result = true;
                        if (Properties.Settings.Default.CB_매수탐색B && Properties.Settings.Default.TB_매수탐색B.Equals(condtion)) result = true;
                        if (Properties.Settings.Default.CB_매도탐색 && Properties.Settings.Default.TB_매도탐색.Equals(condtion)) result = true;
                        return result;
                    }

                    bool condition_check(string condtion)
                    {
                        bool result = false;
                        Condition start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(condtion));
                        if (start_condition != null) { result = true; }
                        if (Properties.Settings.Default.CB_매수탐색A && Properties.Settings.Default.TB_매수탐색A.Equals(condtion)) result = true;
                        if (Properties.Settings.Default.CB_매수탐색B && Properties.Settings.Default.TB_매수탐색B.Equals(condtion)) result = true;
                        if (Properties.Settings.Default.CB_매도탐색 && Properties.Settings.Default.TB_매도탐색.Equals(condtion)) result = true;
                        return result;
                    }

                    Properties.Settings.Default.신규검색식 = Properties.Settings.Default.combo_new_condition_A + "^" + Properties.Settings.Default.combo_new_condition_B + "^" + Properties.Settings.Default.combo_new_condition_C;

                    Properties.Settings.Default.반복매매검색식 = Properties.Settings.Default.combo_repeat_condition_A + "^" + Properties.Settings.Default.combo_repeat_condition_B + "^" + Properties.Settings.Default.combo_repeat_condition_C + "^" +
                                                                Properties.Settings.Default.combo_repeat_condition_D + "^" + Properties.Settings.Default.combo_repeat_condition_E + "^" + Properties.Settings.Default.combo_repeat_condition_F + "^" +
                                                                Properties.Settings.Default.combo_repeat_condition_G + "^" + Properties.Settings.Default.combo_repeat_condition_H + "^" + Properties.Settings.Default.combo_repeat_condition_I + "^" +
                                                                Properties.Settings.Default.combo_repeat_condition_J + "^" + Properties.Settings.Default.combo_repeat_condition_K + "^" + Properties.Settings.Default.combo_repeat_condition_L + "^" +
                                                                Properties.Settings.Default.combo_repeat_condition_M + "^" + Properties.Settings.Default.combo_repeat_condition_N;

                    Properties.Settings.Default.계좌관리검색식 = Properties.Settings.Default.combo_rebalance_condition_A + "^" + Properties.Settings.Default.combo_rebalance_condition_B + "^" + Properties.Settings.Default.combo_rebalance_condition_C + "^" +
                                                                Properties.Settings.Default.combo_rebalance_condition_D + "^" + Properties.Settings.Default.combo_rebalance_condition_E + "^" + Properties.Settings.Default.combo_rebalance_condition_F + "^" +
                                                                Properties.Settings.Default.combo_rebalance_condition_G + "^" + Properties.Settings.Default.CBB_Liquidation_condition_A + "^" + Properties.Settings.Default.CBB_Liquidation_condition_B + "^" +
                                                                Properties.Settings.Default.CBB_Liquidation_condition_C;

                    Properties.Settings.Default.와치검색식 = Properties.Settings.Default.combo_watch_condition_AA + "^" + Properties.Settings.Default.combo_watch_condition_BB + "^" + Properties.Settings.Default.combo_watch_condition_CC + "^" +
                                                            Properties.Settings.Default.combo_watch_condition_DD;
                }
            }


            if (!Form1.로딩완료)
            {
                Tab_Watch.Watch_DataLoad();

                Form1.form1.CB_watch_use_A.Checked = Properties.Settings.Default.CB_watch_use_A;
                Form1.form1.CB_watch_use_B.Checked = Properties.Settings.Default.CB_watch_use_B;
                Form1.form1.CB_watch_use_C.Checked = Properties.Settings.Default.CB_watch_use_C;
                Form1.form1.CB_watch_use_D.Checked = Properties.Settings.Default.CB_watch_use_D;

                Tab_InterestGroup.관심실시간자동등록(false);

                Tab_InterestGroup.자동삭제실행();



                Console.WriteLine("1111111111 Condition_DataLoad ::" + Form1.condotionManager.Condotion_count());

                Form1.condotionManager.DequeueRun();

                Console.WriteLine("2222222222 Condition_DataLoad ::" + Form1.condotionManager.Condotion_count());

                if (Form1.매매시작.Equals(""))
                    Form1.form1.잔고_매매();
            }
        }

        public static bool Overlap_condition(string condition) // 검색식 위치 와 이름  저장 
        {
            bool result = false;

            List<string> 중복리스트 = Form1.form1.Overlap_condition_List;
            중복리스트.Clear();

            bool CBnew_A = Properties.Settings.Default.CB_new_A;
            bool CBnew_B = Properties.Settings.Default.CB_new_B;
            bool CBnew_C = Properties.Settings.Default.CB_new_C;

            if (Form1.FormBasic_Open)
            {
                CBnew_A = Form_Basic.form.CB_new_A.Checked;
                CBnew_B = Form_Basic.form.CB_new_B.Checked;
                CBnew_C = Form_Basic.form.CB_new_C.Checked;
            }

            if (CBnew_A) 중복리스트.Add(Properties.Settings.Default.combo_new_condition_A);
            if (CBnew_B) 중복리스트.Add(Properties.Settings.Default.combo_new_condition_B);
            if (CBnew_C) 중복리스트.Add(Properties.Settings.Default.combo_new_condition_C);
            if (Form1.form1.CB_watch_use_A.Checked) 중복리스트.Add(Properties.Settings.Default.combo_watch_condition_AA);
            if (Form1.form1.CB_watch_use_B.Checked) 중복리스트.Add(Properties.Settings.Default.combo_watch_condition_BB);
            if (Form1.form1.CB_watch_use_C.Checked) 중복리스트.Add(Properties.Settings.Default.combo_watch_condition_CC);
            if (Form1.form1.CB_watch_use_D.Checked) 중복리스트.Add(Properties.Settings.Default.combo_watch_condition_DD);

            int CBBrepeat_use_condition_A = Properties.Settings.Default.combo_repeat_use_condition_A;
            int CBBrepeat_use_condition_B = Properties.Settings.Default.combo_repeat_use_condition_B;
            int CBBrepeat_use_condition_C = Properties.Settings.Default.combo_repeat_use_condition_C;
            int CBBrepeat_use_condition_D = Properties.Settings.Default.combo_repeat_use_condition_D;
            int CBBrepeat_use_condition_E = Properties.Settings.Default.combo_repeat_use_condition_E;
            int CBBrepeat_use_condition_F = Properties.Settings.Default.combo_repeat_use_condition_F;
            int CBBrepeat_use_condition_G = Properties.Settings.Default.combo_repeat_use_condition_G;
            int CBBrepeat_use_condition_H = Properties.Settings.Default.combo_repeat_use_condition_H;
            int CBBrepeat_use_condition_I = Properties.Settings.Default.combo_repeat_use_condition_I;
            int CBBrepeat_use_condition_J = Properties.Settings.Default.combo_repeat_use_condition_J;
            int CBBrepeat_use_condition_K = Properties.Settings.Default.combo_repeat_use_condition_K;
            int CBBrepeat_use_condition_L = Properties.Settings.Default.combo_repeat_use_condition_L;
            int CBBrepeat_use_condition_M = Properties.Settings.Default.combo_repeat_use_condition_M;
            int CBBrepeat_use_condition_N = Properties.Settings.Default.combo_repeat_use_condition_N;

            if (Form1.FormRepeat_Open)
            {
                CBBrepeat_use_condition_A = Form_Repeat.form.combo_repeat_use_condition_A.SelectedIndex;
                CBBrepeat_use_condition_B = Form_Repeat.form.combo_repeat_use_condition_B.SelectedIndex;
                CBBrepeat_use_condition_C = Form_Repeat.form.combo_repeat_use_condition_C.SelectedIndex;
                CBBrepeat_use_condition_D = Form_Repeat.form.combo_repeat_use_condition_D.SelectedIndex;
                CBBrepeat_use_condition_E = Form_Repeat.form.combo_repeat_use_condition_E.SelectedIndex;
                CBBrepeat_use_condition_F = Form_Repeat.form.combo_repeat_use_condition_F.SelectedIndex;
                CBBrepeat_use_condition_G = Form_Repeat.form.combo_repeat_use_condition_G.SelectedIndex;
                CBBrepeat_use_condition_H = Form_Repeat.form.combo_repeat_use_condition_H.SelectedIndex;
                CBBrepeat_use_condition_I = Form_Repeat.form.combo_repeat_use_condition_I.SelectedIndex;
                CBBrepeat_use_condition_J = Form_Repeat.form.combo_repeat_use_condition_J.SelectedIndex;
                CBBrepeat_use_condition_K = Form_Repeat.form.combo_repeat_use_condition_K.SelectedIndex;
                CBBrepeat_use_condition_L = Form_Repeat.form.combo_repeat_use_condition_L.SelectedIndex;
                CBBrepeat_use_condition_M = Form_Repeat.form.combo_repeat_use_condition_M.SelectedIndex;
                CBBrepeat_use_condition_N = Form_Repeat.form.combo_repeat_use_condition_N.SelectedIndex;
            }

            if (CBBrepeat_use_condition_A > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_A);
            if (CBBrepeat_use_condition_B > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_B);
            if (CBBrepeat_use_condition_C > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_C);
            if (CBBrepeat_use_condition_D > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_D);
            if (CBBrepeat_use_condition_E > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_E);
            if (CBBrepeat_use_condition_F > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_F);
            if (CBBrepeat_use_condition_G > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_G);
            if (CBBrepeat_use_condition_H > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_H);
            if (CBBrepeat_use_condition_I > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_I);
            if (CBBrepeat_use_condition_J > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_J);
            if (CBBrepeat_use_condition_K > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_K);
            if (CBBrepeat_use_condition_L > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_L);
            if (CBBrepeat_use_condition_M > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_M);
            if (CBBrepeat_use_condition_N > 0) 중복리스트.Add(Properties.Settings.Default.combo_repeat_condition_N);

            int comborebalance_use_condition_A = Properties.Settings.Default.combo_rebalance_use_condition_A;
            int comborebalance_use_condition_B = Properties.Settings.Default.combo_rebalance_use_condition_B;
            int comborebalance_use_condition_C = Properties.Settings.Default.combo_rebalance_use_condition_C;
            int comborebalance_use_condition_D = Properties.Settings.Default.combo_rebalance_use_condition_D;
            int comborebalance_use_condition_E = Properties.Settings.Default.combo_rebalance_use_condition_E;
            int comborebalance_use_condition_F = Properties.Settings.Default.combo_rebalance_use_condition_F;
            int comborebalance_use_condition_G = Properties.Settings.Default.combo_rebalance_use_condition_G;

            int CBBLiquidation_use_condition_A = Properties.Settings.Default.CBB_Liquidation_use_condition_A;
            int CBBLiquidation_use_condition_B = Properties.Settings.Default.CBB_Liquidation_use_condition_B;
            int CBBLiquidation_use_condition_C = Properties.Settings.Default.CBB_Liquidation_use_condition_C;

            if (Form1.FormAccountManagement_Open)
            {
                comborebalance_use_condition_A = Form_AccountManagement.form.combo_rebalance_use_condition_A.SelectedIndex;
                comborebalance_use_condition_B = Form_AccountManagement.form.combo_rebalance_use_condition_B.SelectedIndex;
                comborebalance_use_condition_C = Form_AccountManagement.form.combo_rebalance_use_condition_C.SelectedIndex;
                comborebalance_use_condition_D = Form_AccountManagement.form.combo_rebalance_use_condition_D.SelectedIndex;
                comborebalance_use_condition_E = Form_AccountManagement.form.combo_rebalance_use_condition_E.SelectedIndex;
                comborebalance_use_condition_F = Form_AccountManagement.form.combo_rebalance_use_condition_F.SelectedIndex;
                comborebalance_use_condition_G = Form_AccountManagement.form.combo_rebalance_use_condition_G.SelectedIndex;

                CBBLiquidation_use_condition_A = Form_AccountManagement.form.CBB_Liquidation_use_condition_A.SelectedIndex;
                CBBLiquidation_use_condition_B = Form_AccountManagement.form.CBB_Liquidation_use_condition_B.SelectedIndex;
                CBBLiquidation_use_condition_C = Form_AccountManagement.form.CBB_Liquidation_use_condition_C.SelectedIndex;
            }

            if (comborebalance_use_condition_A > 0) 중복리스트.Add(Properties.Settings.Default.combo_rebalance_condition_A);
            if (comborebalance_use_condition_B > 0) 중복리스트.Add(Properties.Settings.Default.combo_rebalance_condition_B);
            if (comborebalance_use_condition_C > 0) 중복리스트.Add(Properties.Settings.Default.combo_rebalance_condition_C);
            if (comborebalance_use_condition_D > 0) 중복리스트.Add(Properties.Settings.Default.combo_rebalance_condition_D);
            if (comborebalance_use_condition_E > 0) 중복리스트.Add(Properties.Settings.Default.combo_rebalance_condition_E);
            if (comborebalance_use_condition_F > 0) 중복리스트.Add(Properties.Settings.Default.combo_rebalance_condition_F);
            if (comborebalance_use_condition_G > 0) 중복리스트.Add(Properties.Settings.Default.combo_rebalance_condition_G);

            if (CBBLiquidation_use_condition_A > 0) 중복리스트.Add(Properties.Settings.Default.CBB_Liquidation_condition_A);
            if (CBBLiquidation_use_condition_B > 0) 중복리스트.Add(Properties.Settings.Default.CBB_Liquidation_condition_B);
            if (CBBLiquidation_use_condition_C > 0) 중복리스트.Add(Properties.Settings.Default.CBB_Liquidation_condition_C);

            for (int i = 0; i < Form1.form1.Interest_condition_List.Count; i++)
            {
                중복리스트.Add(Form1.form1.Interest_condition_List[i]);
            }

            if (중복리스트.Contains(condition) && !Properties.Settings.Default.TB_매수탐색A.Equals(condition) && !Properties.Settings.Default.TB_매수탐색B.Equals(condition) && !Properties.Settings.Default.TB_매도탐색.Equals(condition)) result = true;

            return result;
        }

        public static void CB_condition_CheckedChanged(object sender) // 체크박스 와 콤보박스 사용 갯수 제한 
        {
            CheckBox CB = (sender as CheckBox);

            Condition start_condition = null;
            Condition stop_condition = null;

            string location = "신규_A";
            ComboBox combo = null;

            switch (CB.Name)
            {
                case "CB_new_A":
                    location = "신규_A";
                    combo = Form_Basic.form.combo_new_condition_A;
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.신규검색식.Split('^')[0]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_new_condition_A));
                    break;
                case "CB_new_B":
                    location = "신규_B";
                    combo = Form_Basic.form.combo_new_condition_B;
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.신규검색식.Split('^')[1]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_new_condition_B));
                    break;
                case "CB_new_C":
                    location = "신규_C";
                    combo = Form_Basic.form.combo_new_condition_C;
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.신규검색식.Split('^')[2]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_new_condition_C));
                    break;
                case "CB_watch_use_A":
                    location = "Watch_A";
                    combo = Form1.form1.combo_watch_condition_AA;
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.와치검색식.Split('^')[0]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_watch_condition_AA));
                    break;
                case "CB_watch_use_B":
                    location = "Watch_B";
                    combo = Form1.form1.combo_watch_condition_BB;
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.와치검색식.Split('^')[1]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_watch_condition_BB));
                    break;
                case "CB_watch_use_C":
                    location = "Watch_C";
                    combo = Form1.form1.combo_watch_condition_CC;
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.와치검색식.Split('^')[2]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_watch_condition_CC));
                    break;
                case "CB_watch_use_D":
                    location = "Watch_D";
                    combo = Form1.form1.combo_watch_condition_DD;
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.와치검색식.Split('^')[3]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_watch_condition_DD));
                    break;
            }

            if (CB.Checked)
            {
                void 대금탐색on()
                {
                    Form1.비프음("실행");
                    Form1.동작_Log("[검색식 가동확인] " + location + " - 검색식[ " + combo.Text + " ]이 실시간 감시를 시작 합니다.");
                    검색식위치저장(location, combo.Text);
                }
                void 대금탐색off()
                {
                    Form1.비프음("정지");
                    Form1.알림창("[ 검색식 가동확인 ]\n\n" + location + " - 검색식 [ " + combo.Text + " ]이 정지 되어 있습니다.", 5, false);
                    (sender as CheckBox).Checked = false;
                }

                if (combo.Text.Equals(Properties.Settings.Default.TB_매수탐색A))
                {
                    if (Properties.Settings.Default.CB_매수탐색A) 대금탐색on(); else 대금탐색off();
                }
                else if (combo.Text.Equals(Properties.Settings.Default.TB_매수탐색B))
                {
                    if (Properties.Settings.Default.CB_매수탐색B) 대금탐색on(); else 대금탐색off();
                }
                else if (combo.Text.Equals(Properties.Settings.Default.TB_매도탐색))
                {
                    if (Properties.Settings.Default.CB_매도탐색) 대금탐색on(); else 대금탐색off();
                }
                else
                {
                    if (Form1.Run_condition_List.Count < 10)
                    {
                        if (start_condition != null)
                        {
                            if (combo.Text.Equals(""))
                            {
                                Form1.AutoClosingAlram(location + " - 검색식이 비어 있습니다.", "검색식알림", 5, "동작");
                                (sender as CheckBox).Checked = false;
                            }
                            else
                            {
                                Start_Monitoring(start_condition, location);
                                검색식위치저장(location, combo.Text);
                            }
                        }
                        else
                        {
                            Form1.AutoClosingAlram(location + " - 검색식이 비어 있습니다.", "검색식알림", 5, "동작");
                            (sender as CheckBox).Checked = false;
                        }
                    }
                    else
                    {
                        if (!Form1.Run_condition_List.Contains(start_condition.name))
                        {
                            if (start_condition.name.Equals(""))
                                Form1.AutoClosingAlram(location + " - 검색식이 비어 있습니다.", "검색식알림", 5, "동작");
                            else
                                Form1.AutoClosingAlram("검색식은 10개 까지 사용할수 있습니다.", "검색식알림", 5, "동작");

                            (sender as CheckBox).Checked = false;
                        }
                        else
                        {
                            if (Form1.로딩완료) Form1.알림창("[ 검색식 가동중 ]\n\n검색식( " + start_condition.name + " ) 이 실시간 감시 중 입니다.", 5, false);
                            Form1.동작_Log("[검색식 가동중] 검색식( " + start_condition.name + " ) 이 실시간 감시 중 입니다.");
                        }
                    }
                }
            }
            else
            {
                if (combo.Text.Equals(Properties.Settings.Default.TB_매수탐색A) || combo.Text.Equals(Properties.Settings.Default.TB_매수탐색B) || combo.Text.Equals(Properties.Settings.Default.TB_매도탐색) || combo.Text.Equals(" "))
                {
                    if (combo.Text.Equals(" "))
                        Form1.비프음("언체크");
                    else
                    { Form1.동작_Log("[검색식 가동확인] " + location + "검색식 [ " + combo.Text + " ]이 실시간 감시를 해제 합니다."); Form1.비프음("정지"); }
                }
                else
                {
                    if (stop_condition != null)
                    {
                        Stop_Monitoring(stop_condition, location);  //검색식 정지 요청
                    }
                }
            }
        }

        public static void combo_use_condition_SelectedIndexChanged(object sender)
        {
            Condition start_condition = null;
            Condition stop_condition = null;

            string location = "반복_A";
            string condition_name = "";
            ComboBox ComboBox_use = sender as ComboBox;
            ComboBox ComboBox_condition = null;

            switch (ComboBox_use.Name)
            {
                case "combo_repeat_use_condition_A":
                    location = "반복_A";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[0];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[0]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_A));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_A.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_A;
                    Properties.Settings.Default.combo_repeat_use_condition_A = ComboBox_use.SelectedIndex;
                    break;
                case "combo_repeat_use_condition_B":
                    location = "반복_B";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[1];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[1]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_B));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_B.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_B;
                    Properties.Settings.Default.combo_repeat_use_condition_B = ComboBox_use.SelectedIndex;

                    break;
                case "combo_repeat_use_condition_C":
                    location = "반복_C";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[2];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[2]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_C));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_C.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_C;
                    Properties.Settings.Default.combo_repeat_use_condition_C = ComboBox_use.SelectedIndex;
                    break;
                case "combo_repeat_use_condition_D":
                    location = "반복_D";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[3];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[3]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_D));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_D.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_D;
                    Properties.Settings.Default.combo_repeat_use_condition_D = ComboBox_use.SelectedIndex;
                    break;
                case "combo_repeat_use_condition_E":
                    location = "반복_E";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[4];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[4]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_E));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_E.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_E;
                    Properties.Settings.Default.combo_repeat_use_condition_E = ComboBox_use.SelectedIndex;
                    break;
                case "combo_repeat_use_condition_F":
                    location = "반복_F";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[5];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[5]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_F));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_F.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_F;
                    Properties.Settings.Default.combo_repeat_use_condition_F = ComboBox_use.SelectedIndex;
                    break;
                case "combo_repeat_use_condition_G":
                    location = "반복_G";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[6];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[6]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_G));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_G.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_G;
                    Properties.Settings.Default.combo_repeat_use_condition_G = ComboBox_use.SelectedIndex;
                    break;
                case "combo_repeat_use_condition_H":
                    location = "반복_H";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[7];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[7]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_H));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_H.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_H;
                    Properties.Settings.Default.combo_repeat_use_condition_H = ComboBox_use.SelectedIndex;
                    break;
                case "combo_repeat_use_condition_I":
                    location = "반복_I";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[8];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[8]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_I));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_I.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_I;
                    Properties.Settings.Default.combo_repeat_use_condition_I = ComboBox_use.SelectedIndex;
                    break;
                case "combo_repeat_use_condition_J":
                    location = "반복_J";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[9];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[9]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_J));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_J.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_J;
                    Properties.Settings.Default.combo_repeat_use_condition_J = ComboBox_use.SelectedIndex;
                    break;
                case "combo_repeat_use_condition_K":
                    location = "반복_K";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[10];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[10]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_K));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_K.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_K;
                    Properties.Settings.Default.combo_repeat_use_condition_K = ComboBox_use.SelectedIndex;
                    break;
                case "combo_repeat_use_condition_L":
                    location = "반복_L";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[11];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[11]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_L));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_L.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_L;
                    Properties.Settings.Default.combo_repeat_use_condition_L = ComboBox_use.SelectedIndex;
                    break;
                case "combo_repeat_use_condition_M":
                    location = "반복_M";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[12];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[12]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_M));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_M.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_M;
                    Properties.Settings.Default.combo_repeat_use_condition_M = ComboBox_use.SelectedIndex;
                    break;
                case "combo_repeat_use_condition_N":
                    location = "반복_N";
                    condition_name = Properties.Settings.Default.반복매매검색식.Split('^')[13];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.반복매매검색식.Split('^')[13]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_repeat_condition_N));
                    if (Form1.로딩완료) Form_Repeat.form.CB_repeat_use_N.Checked = false;
                    ComboBox_condition = Form_Repeat.form.combo_repeat_condition_N;
                    Properties.Settings.Default.combo_repeat_use_condition_N = ComboBox_use.SelectedIndex;
                    break;
                case "combo_rebalance_use_condition_A":
                    location = "리밸_A";
                    condition_name = Properties.Settings.Default.계좌관리검색식.Split('^')[0];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.계좌관리검색식.Split('^')[0]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_rebalance_condition_A));
                    if (Form1.로딩완료) Form_AccountManagement.form.CB_rebalance_A.Checked = false;
                    ComboBox_condition = Form_AccountManagement.form.combo_rebalance_condition_A;
                    Properties.Settings.Default.combo_rebalance_use_condition_A = ComboBox_use.SelectedIndex;
                    break;
                case "combo_rebalance_use_condition_B":
                    location = "리밸_B";
                    condition_name = Properties.Settings.Default.계좌관리검색식.Split('^')[1];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.계좌관리검색식.Split('^')[1]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_rebalance_condition_B));
                    if (Form1.로딩완료) Form_AccountManagement.form.CB_rebalance_B.Checked = false;
                    ComboBox_condition = Form_AccountManagement.form.combo_rebalance_condition_B;
                    Properties.Settings.Default.combo_rebalance_use_condition_B = ComboBox_use.SelectedIndex;
                    break;
                case "combo_rebalance_use_condition_C":
                    location = "리밸_C";
                    condition_name = Properties.Settings.Default.계좌관리검색식.Split('^')[2];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.계좌관리검색식.Split('^')[2]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_rebalance_condition_C));
                    if (Form1.로딩완료) Form_AccountManagement.form.CB_rebalance_C.Checked = false;
                    ComboBox_condition = Form_AccountManagement.form.combo_rebalance_condition_C;
                    Properties.Settings.Default.combo_rebalance_use_condition_C = ComboBox_use.SelectedIndex;
                    break;
                case "combo_rebalance_use_condition_D":
                    location = "리밸_D";
                    condition_name = Properties.Settings.Default.계좌관리검색식.Split('^')[3];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.계좌관리검색식.Split('^')[3]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_rebalance_condition_D));
                    if (Form1.로딩완료) Form_AccountManagement.form.CB_rebalance_D.Checked = false;
                    ComboBox_condition = Form_AccountManagement.form.combo_rebalance_condition_D;
                    Properties.Settings.Default.combo_rebalance_use_condition_D = ComboBox_use.SelectedIndex;
                    break;
                case "combo_rebalance_use_condition_E":
                    location = "리밸_E";
                    condition_name = Properties.Settings.Default.계좌관리검색식.Split('^')[4];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.계좌관리검색식.Split('^')[4]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_rebalance_condition_E));
                    if (Form1.로딩완료) Form_AccountManagement.form.CB_rebalance_E.Checked = false;
                    ComboBox_condition = Form_AccountManagement.form.combo_rebalance_condition_E;
                    Properties.Settings.Default.combo_rebalance_use_condition_E = ComboBox_use.SelectedIndex;
                    break;
                case "combo_rebalance_use_condition_F":
                    location = "리밸_F";
                    condition_name = Properties.Settings.Default.계좌관리검색식.Split('^')[5];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.계좌관리검색식.Split('^')[5]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_rebalance_condition_F));
                    if (Form1.로딩완료) Form_AccountManagement.form.CB_rebalance_F.Checked = false;
                    ComboBox_condition = Form_AccountManagement.form.combo_rebalance_condition_F;
                    Properties.Settings.Default.combo_rebalance_use_condition_F = ComboBox_use.SelectedIndex;
                    break;
                case "combo_rebalance_use_condition_G":
                    location = "리밸_G";
                    condition_name = Properties.Settings.Default.계좌관리검색식.Split('^')[6];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.계좌관리검색식.Split('^')[6]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.combo_rebalance_condition_G));
                    if (Form1.로딩완료) Form_AccountManagement.form.CB_rebalance_G.Checked = false;
                    ComboBox_condition = Form_AccountManagement.form.combo_rebalance_condition_G;
                    Properties.Settings.Default.combo_rebalance_use_condition_G = ComboBox_use.SelectedIndex;
                    break;
                case "CBB_Liquidation_use_condition_A":
                    location = "청산_A";
                    condition_name = Properties.Settings.Default.계좌관리검색식.Split('^')[7];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.계좌관리검색식.Split('^')[7]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.CBB_Liquidation_condition_A));
                    if (Form1.로딩완료) Form_AccountManagement.form.CB_Liquidation_A.Checked = false;
                    ComboBox_condition = Form_AccountManagement.form.CBB_Liquidation_condition_A;
                    Properties.Settings.Default.CBB_Liquidation_use_condition_A = ComboBox_use.SelectedIndex;
                    break;
                case "CBB_Liquidation_use_condition_B":
                    location = "청산_B";
                    condition_name = Properties.Settings.Default.계좌관리검색식.Split('^')[8];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.계좌관리검색식.Split('^')[8]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.CBB_Liquidation_condition_B));
                    if (Form1.로딩완료) Form_AccountManagement.form.CB_Liquidation_B.Checked = false;
                    ComboBox_condition = Form_AccountManagement.form.CBB_Liquidation_condition_B;
                    Properties.Settings.Default.CBB_Liquidation_use_condition_B = ComboBox_use.SelectedIndex;
                    break;
                case "CBB_Liquidation_use_condition_C":
                    location = "청산_C";
                    condition_name = Properties.Settings.Default.계좌관리검색식.Split('^')[9];
                    start_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.계좌관리검색식.Split('^')[9]));
                    stop_condition = Form1.form1.ConditionList.Find(o => o.name.Equals(Properties.Settings.Default.CBB_Liquidation_condition_C));
                    if (Form1.로딩완료) Form_AccountManagement.form.CB_Liquidation_C.Checked = false;
                    ComboBox_condition = Form_AccountManagement.form.CBB_Liquidation_condition_C;
                    Properties.Settings.Default.CBB_Liquidation_use_condition_C = ComboBox_use.SelectedIndex;
                    break;
            }

            if (ComboBox_use.SelectedIndex > 0)
            {
                void 대금탐색on()
                {
                    Form1.비프음("실행");
                    Form1.동작_Log("검색식 [ " + condition_name + " ]이 실시간 감시를 시작 합니다.");
                    검색식위치저장(location, ComboBox_condition.Text);
                }
                void 대금탐색off()
                {
                    Form1.비프음("정지");
                    Form1.알림창("[ 검색식 가동확인 ]\n\n검색식 [ " + condition_name + " ]이 정지 되어 있습니다.", 5, false);
                    ComboBox_use.SelectedIndex = 0;

                }

                if (condition_name.Equals(Properties.Settings.Default.TB_매수탐색A))
                {
                    if (Properties.Settings.Default.CB_매수탐색A) 대금탐색on();
                    else 대금탐색off();
                }
                else if (condition_name.Equals(Properties.Settings.Default.TB_매수탐색B))
                {
                    if (Properties.Settings.Default.CB_매수탐색B) 대금탐색on();
                    else 대금탐색off();
                }
                else if (condition_name.Equals(Properties.Settings.Default.TB_매도탐색))
                {
                    if (Properties.Settings.Default.CB_매도탐색) 대금탐색on();
                    else 대금탐색off();
                }
                else
                {
                    if (Form1.Run_condition_List.Count < 10)
                    {
                        if (start_condition != null)
                        {
                            if (condition_name.Equals(""))
                            {
                                Form1.알림창("[ 검색식 가동확인 ]\n\n" + location + " - 검색식이 비어 있습니다.", 10, false);
                                ComboBox_use.SelectedIndex = 0;
                            }
                            else
                            {
                                Start_Monitoring(start_condition, location);
                                검색식위치저장(location, start_condition.name);
                            }
                        }
                        else
                        {
                            Form1.알림창("[ 검색식 가동확인 ]\n\n" + location + " - 검색식이 비어 있습니다.", 10, false);
                            ComboBox_use.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        if (start_condition == null)
                        {
                            Form1.알림창("[ 검색식 가동확인 ]\n\n" + location + " - 검색식이 비어 있습니다.", 10, false);
                            ComboBox_use.SelectedIndex = 0;
                        }
                        else
                        {
                            if (!Form1.Run_condition_List.Contains(start_condition.name))
                            {
                                if (start_condition.name.Equals(""))
                                    Form1.알림창("[ 검색식 가동확인 ]\n\n" + location + " - 검색식이 비어 있습니다.", 10, false);
                                else
                                    Form1.알림창("[ 검색식 가동확인 ]\n\n" + location + " - 검색식은 10개 까지 사용할수 있습니다.", 10, false);

                                ComboBox_use.SelectedIndex = 0;
                            }
                            else
                            {
                                if (Form1.로딩완료) Form1.알림창("[ 검색식 가동확인 ]\n\n" + location + " - 검색식[ " + start_condition.name + " ] 이 실시간 감시 중 입니다.", 5, false);
                                Form1.동작_Log("[검색식 가동확인] " + location + " - 검색식[ " + start_condition.name + " ] 이 실시간 감시 중 입니다.");
                            }
                        }
                    }
                }
            }
            else
            {
                if (condition_name.Equals(Properties.Settings.Default.TB_매수탐색A) || condition_name.Equals(Properties.Settings.Default.TB_매수탐색B) || condition_name.Equals(Properties.Settings.Default.TB_매도탐색))
                {
                    if (condition_name.Equals(""))
                        Form1.비프음("언체크");
                    else
                        Form1.동작_Log("[검색식 가동확인] " + location + " - 검색식 [ " + condition_name + " ]이 실시간 감시를 해제 합니다."); Form1.비프음("정지");

                    ComboBox_use.SelectedIndex = 0;
                }
                else
                {
                    if (stop_condition != null)
                    {
                        Stop_Monitoring(stop_condition, location);  //검색식 정지 요청
                    }
                }
            }

            if (ComboBox_use.SelectedIndex == 0) ComboBox_condition.SelectedIndex = 0;

        }

        public static void combo_condition_SelectedIndexChanged(object sender)
        {
            ComboBox combobox = sender as ComboBox;
            int index = combobox.SelectedIndex;
            string text = combobox.Text;

            switch (combobox.Name)
            {
                case "combo_new_condition_A":
                    if (!Properties.Settings.Default.신규검색식.Split('^')[0].Equals(Properties.Settings.Default.combo_new_condition_A) || (index == 0 && text.Equals("")))
                    {
                        Properties.Settings.Default.CB_new_A = false;
                        Form1.NewCatch_List_A.Clear();

                        Form_Basic.form.CB_new_A.Checked = false;
                    }
                    break;
                case "combo_new_condition_B":
                    if (!Properties.Settings.Default.신규검색식.Split('^')[1].Equals(Properties.Settings.Default.combo_new_condition_B) || (index == 0 && text.Equals("")))
                    {
                        Properties.Settings.Default.CB_new_B = false;
                        Form1.NewCatch_List_B.Clear();

                        Form_Basic.form.CB_new_B.Checked = false;
                    }
                    break;
                case "combo_new_condition_C":
                    if (!Properties.Settings.Default.신규검색식.Split('^')[2].Equals(Properties.Settings.Default.combo_new_condition_C) || (index == 0 && text.Equals("")))
                    {
                        Properties.Settings.Default.CB_new_C = false;
                        Form1.NewCatch_List_C.Clear();

                        Form_Basic.form.CB_new_C.Checked = false;
                    }
                    break;
                case "combo_repeat_condition_A":
                    if (Form_Repeat.form.combo_repeat_use_condition_A.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[0].Equals(Properties.Settings.Default.combo_repeat_condition_A) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_A.Checked = false;
                            Form1.Repeat_condition_List_A.Clear();

                            Properties.Settings.Default.combo_repeat_use_condition_A = 0;
                            Form_Repeat.form.combo_repeat_use_condition_A.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_repeat_condition_B":
                    if (Form_Repeat.form.combo_repeat_use_condition_B.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[1].Equals(Properties.Settings.Default.combo_repeat_condition_B) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_B.Checked = false;
                            Form1.Repeat_condition_List_B.Clear();

                            Properties.Settings.Default.combo_repeat_use_condition_B = 0;
                            Form_Repeat.form.combo_repeat_use_condition_B.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_repeat_condition_C":
                    if (Form_Repeat.form.combo_repeat_use_condition_C.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[2].Equals(Properties.Settings.Default.combo_repeat_condition_C) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_C.Checked = false;
                            Properties.Settings.Default.combo_repeat_use_condition_C = 0;
                            Form1.Repeat_condition_List_C.Clear();

                            Form_Repeat.form.combo_repeat_use_condition_C.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_repeat_condition_D":
                    if (Form_Repeat.form.combo_repeat_use_condition_D.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[3].Equals(Properties.Settings.Default.combo_repeat_condition_D) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_D.Checked = false;
                            Properties.Settings.Default.combo_repeat_use_condition_D = 0;
                            Form1.Repeat_condition_List_D.Clear();

                            Form_Repeat.form.combo_repeat_use_condition_D.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_repeat_condition_E":
                    if (Form_Repeat.form.combo_repeat_use_condition_E.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[4].Equals(Properties.Settings.Default.combo_repeat_condition_E) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_E.Checked = false;
                            Properties.Settings.Default.combo_repeat_use_condition_E = 0;
                            Form1.Repeat_condition_List_E.Clear();

                            Form_Repeat.form.combo_repeat_use_condition_E.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_repeat_condition_F":
                    if (Form_Repeat.form.combo_repeat_use_condition_F.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[5].Equals(Properties.Settings.Default.combo_repeat_condition_F) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_F.Checked = false;
                            Properties.Settings.Default.combo_repeat_use_condition_F = 0;
                            Form1.Repeat_condition_List_F.Clear();

                            Form_Repeat.form.combo_repeat_use_condition_F.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_repeat_condition_G":
                    if (Form_Repeat.form.combo_repeat_use_condition_G.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[6].Equals(Properties.Settings.Default.combo_repeat_condition_G) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_G.Checked = false;
                            Properties.Settings.Default.combo_repeat_use_condition_G = 0;
                            Form1.Repeat_condition_List_G.Clear();

                            Form_Repeat.form.combo_repeat_use_condition_G.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_repeat_condition_H":
                    if (Form_Repeat.form.combo_repeat_use_condition_H.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[7].Equals(Properties.Settings.Default.combo_repeat_condition_H) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_H.Checked = false;
                            Properties.Settings.Default.combo_repeat_use_condition_H = 0;
                            Form1.Repeat_condition_List_H.Clear();

                            Form_Repeat.form.combo_repeat_use_condition_H.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_repeat_condition_I":
                    if (Form_Repeat.form.combo_repeat_use_condition_I.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[8].Equals(Properties.Settings.Default.combo_repeat_condition_I) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_I.Checked = false;
                            Properties.Settings.Default.combo_repeat_use_condition_I = 0;
                            Form1.Repeat_condition_List_I.Clear();

                            Form_Repeat.form.combo_repeat_use_condition_I.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_repeat_condition_J":
                    if (Form_Repeat.form.combo_repeat_use_condition_J.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[9].Equals(Properties.Settings.Default.combo_repeat_condition_J) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_J.Checked = false;
                            Properties.Settings.Default.combo_repeat_use_condition_J = 0;
                            Form1.Repeat_condition_List_J.Clear();

                            Form_Repeat.form.combo_repeat_use_condition_J.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_repeat_condition_K":
                    if (Form_Repeat.form.combo_repeat_use_condition_K.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[10].Equals(Properties.Settings.Default.combo_repeat_condition_K) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_K.Checked = false;
                            Properties.Settings.Default.combo_repeat_use_condition_K = 0;
                            Form1.Repeat_condition_List_K.Clear();

                            Form_Repeat.form.combo_repeat_use_condition_K.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_repeat_condition_L":
                    if (Form_Repeat.form.combo_repeat_use_condition_L.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[11].Equals(Properties.Settings.Default.combo_repeat_condition_L) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_L.Checked = false;
                            Properties.Settings.Default.combo_repeat_use_condition_L = 0;
                            Form1.Repeat_condition_List_L.Clear();

                            Form_Repeat.form.combo_repeat_use_condition_L.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_repeat_condition_M":
                    if (Form_Repeat.form.combo_repeat_use_condition_M.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[12].Equals(Properties.Settings.Default.combo_repeat_condition_M) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_M.Checked = false;
                            Properties.Settings.Default.combo_repeat_use_condition_M = 0;
                            Form1.Repeat_condition_List_M.Clear();

                            Form_Repeat.form.combo_repeat_use_condition_M.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_repeat_condition_N":
                    if (Form_Repeat.form.combo_repeat_use_condition_N.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.반복매매검색식.Split('^')[13].Equals(Properties.Settings.Default.combo_repeat_condition_N) || (index == 0 && text.Equals("")))
                        {
                            Form_Repeat.form.CB_repeat_use_N.Checked = false;
                            Properties.Settings.Default.combo_repeat_use_condition_N = 0;
                            Form1.Repeat_condition_List_N.Clear();

                            Form_Repeat.form.combo_repeat_use_condition_N.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_rebalance_condition_A":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_A.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.계좌관리검색식.Split('^')[0].Equals(Properties.Settings.Default.combo_rebalance_condition_A) || (index == 0 && text.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_A.Checked = false;
                            Form1.Rebal_condition_List_A.Clear();

                            Properties.Settings.Default.combo_rebalance_use_condition_A = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_A.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_rebalance_condition_B":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_B.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.계좌관리검색식.Split('^')[1].Equals(Properties.Settings.Default.combo_rebalance_condition_B) || (index == 0 && text.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_B.Checked = false;
                            Form1.Rebal_condition_List_B.Clear();

                            Properties.Settings.Default.combo_rebalance_use_condition_B = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_B.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_rebalance_condition_C":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_C.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.계좌관리검색식.Split('^')[2].Equals(Properties.Settings.Default.combo_rebalance_condition_C) || (index == 0 && text.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_C.Checked = false;
                            Form1.Rebal_condition_List_C.Clear();

                            Properties.Settings.Default.combo_rebalance_use_condition_C = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_C.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_rebalance_condition_D":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_D.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.계좌관리검색식.Split('^')[3].Equals(Properties.Settings.Default.combo_rebalance_condition_D) || (index == 0 && text.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_D.Checked = false;
                            Form1.Rebal_condition_List_E.Clear();

                            Properties.Settings.Default.combo_rebalance_use_condition_D = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_D.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_rebalance_condition_E":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_E.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.계좌관리검색식.Split('^')[4].Equals(Properties.Settings.Default.combo_rebalance_condition_E) || (index == 0 && text.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_E.Checked = false;
                            Form1.Rebal_condition_List_E.Clear();

                            Properties.Settings.Default.combo_rebalance_use_condition_E = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_E.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_rebalance_condition_F":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_F.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.계좌관리검색식.Split('^')[5].Equals(Properties.Settings.Default.combo_rebalance_condition_F) || (index == 0 && text.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_F.Checked = false;
                            Form1.Rebal_condition_List_F.Clear();

                            Properties.Settings.Default.combo_rebalance_use_condition_F = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_F.SelectedIndex = 0;
                        }
                    }
                    break;
                case "combo_rebalance_condition_G":
                    if (Form_AccountManagement.form.combo_rebalance_use_condition_G.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.계좌관리검색식.Split('^')[6].Equals(Properties.Settings.Default.combo_rebalance_condition_G) || (index == 0 && text.Equals("")))
                        {
                            Form_AccountManagement.form.CB_rebalance_G.Checked = false;
                            Form1.Rebal_condition_List_G.Clear();

                            Properties.Settings.Default.combo_rebalance_use_condition_G = 0;
                            Form_AccountManagement.form.combo_rebalance_use_condition_G.SelectedIndex = 0;
                        }
                    }
                    break;

                case "CBB_Liquidation_condition_A":
                    if (Form_AccountManagement.form.CBB_Liquidation_use_condition_A.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.계좌관리검색식.Split('^')[7].Equals(Properties.Settings.Default.CBB_Liquidation_condition_A) || (index == 0 && text.Equals("")))
                        {
                            Form_AccountManagement.form.CB_Liquidation_A.Checked = false;
                            Form1.Liquidation_condition_List_A.Clear();

                            Properties.Settings.Default.CBB_Liquidation_use_condition_A = 0;
                            Form_AccountManagement.form.CBB_Liquidation_use_condition_A.SelectedIndex = 0;
                        }
                    }
                    break;
                case "CBB_Liquidation_condition_B":
                    if (Form_AccountManagement.form.CBB_Liquidation_use_condition_B.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.계좌관리검색식.Split('^')[8].Equals(Properties.Settings.Default.CBB_Liquidation_condition_B) || (index == 0 && text.Equals("")))
                        {
                            Form_AccountManagement.form.CB_Liquidation_B.Checked = false;
                            Form1.Liquidation_condition_List_B.Clear();

                            Properties.Settings.Default.CBB_Liquidation_use_condition_B = 0;
                            Form_AccountManagement.form.CBB_Liquidation_use_condition_B.SelectedIndex = 0;
                        }
                    }
                    break;
                case "CBB_Liquidation_condition_C":
                    if (Form_AccountManagement.form.CBB_Liquidation_use_condition_C.SelectedIndex != 0)
                    {
                        if (!Properties.Settings.Default.계좌관리검색식.Split('^')[9].Equals(Properties.Settings.Default.CBB_Liquidation_condition_C) || (index == 0 && text.Equals("")))
                        {
                            Form_AccountManagement.form.CB_Liquidation_C.Checked = false;
                            Form1.Liquidation_condition_List_C.Clear();

                            Properties.Settings.Default.CBB_Liquidation_use_condition_C = 0;
                            Form_AccountManagement.form.CBB_Liquidation_use_condition_C.SelectedIndex = 0;
                        }
                    }
                    break;

                case "combo_watch_condition_AA":
                    if (!Properties.Settings.Default.와치검색식.Split('^')[0].Equals(Properties.Settings.Default.combo_watch_condition_AA) || (index == 0 && text.Equals("")))
                    {
                        Form1.form1.CB_watch_use_A.Checked = false;
                    }
                    break;
                case "combo_watch_condition_BB":
                    if (!Properties.Settings.Default.와치검색식.Split('^')[1].Equals(Properties.Settings.Default.combo_watch_condition_BB) || (index == 0 && text.Equals("")))
                    {
                        Form1.form1.CB_watch_use_B.Checked = false;
                    }
                    break;
                case "combo_watch_condition_CC":
                    if (!Properties.Settings.Default.와치검색식.Split('^')[2].Equals(Properties.Settings.Default.combo_watch_condition_CC) || (index == 0 && text.Equals("")))
                    {
                        Form1.form1.CB_watch_use_C.Checked = false;
                    }
                    break;
                case "combo_watch_condition_DD":
                    if (!Properties.Settings.Default.와치검색식.Split('^')[3].Equals(Properties.Settings.Default.combo_watch_condition_DD) || (index == 0 && text.Equals("")))
                    {
                        Form1.form1.CB_watch_use_D.Checked = false;
                    }
                    break;
            }
        }

        public static void Start_Monitoring(Condition condition, string position)
        {
            Console.WriteLine("Start_Monitoring 위치: " + position + " 검색식: " + condition.name);


            Task JO_Manager = new Task(() =>
            {
                try
                {
                    Fail_condition fail = Form1.form1.fail_condition_List.Find(o => o.condition.Equals(condition.name));
                    if (fail == null)
                    {
                        if (!Form1.Run_condition_List.Contains(condition.name))
                        {
                            int result = result = Form1.form1.axKHOpenAPI1.SendCondition("1100", condition.name, condition.index, 1);

                            if (result == 1) // 성공
                            {
                                Form1.비프음("실행");

                                Form1.동작_Log(position + " => " + condition.name + " 을 감시를 시작합니다.");

                                Form1.Run_condition_List.Add(condition.name);

                                RunCondition Run = Form1.form1.search_condition_List.Find(o => o.condition.Equals(condition.name));
                                if (Run == null)
                                {
                                    RunCondition search_Add = new RunCondition(condition.name, 0);
                                    Form1.form1.search_condition_List.Add(search_Add);
                                }
                            }
                            else // 실패
                            {
                                Form1.Error_Log(position + "_ " + condition.name + " 실시간 감시 요청 실패 하였습니다.");
                                Form1.Error_Log("같은 조건식은 1분에 1회 실시간 감시 요청 할수 있습니다. " + position + " 체크박스 체크 해제후 다시 시도 하세요.");
                                Form1.AutoClosingAlram("같은 조건식은 1분에 1회 실시간 감시 요청 할수 있습니다. \n" + position + " 체크박스 체크 해제후 다시 시도 하세요.", "검색식 가동실패", 10, "동작");

                                Form1.비프음("언체크");

                                Fail_condition add = new Fail_condition(condition.name, 60);
                                Form1.form1.fail_condition_List.Add(add);

                                사용중지_(position);

                                Fail_condition fail_alram = Form1.form1.fail_condition_List.Find(o => o.condition.Equals(condition.name));
                                Form1.알림창(fail_alram.condition, fail_alram.count, true);
                            }
                        }
                        else
                        {
                            if (Form1.로딩완료) Form1.알림창("[ 검색식 가동중 ]\n\n검색식( " + condition.name + " ) 이 실시간 감시 중 입니다.", 5, false);
                            Form1.동작_Log("[검색식 가동중] 검색식( " + condition.name + " ) 이 실시간 감시 중 입니다.");
                        }
                    }
                    else // 60초 전에 감시 재요청 
                    {
                        사용중지_(position);
                        Form1.알림창(fail.condition, fail.count, true);
                    }
                }
                catch (Exception e)
                {
                    string message = e.Message.ToString();

                    Form1.알림창("[ 검색식 'start' 실패 ]\n\n검색식:" + condition.name + "\n\n검색식 'start' 실패 에러내역 :: " + message, 5, false);

                    Form1.Error_Log(" ");
                    Form1.Error_Log("[검색식 'start' 실패] 검색식:" + condition.name + " 검색식 'start' 실패 에러내역 :: " + message);
                    Form1.Error_Log(" ");

                    Form1.비프음("언체크");
                }
            });
            Form1.condotionManager.RequestTrData(JO_Manager); // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }


        public static void 사용중지_(string position)
        {
            if (Form1.FormBasic_Open)// Form 생성유무 확인
            {
                switch (position)
                {
                    case "신규_A": Form_Basic.form.CB_new_A.Checked = false; break;
                    case "신규_B": Form_Basic.form.CB_new_B.Checked = false; break;
                    case "신규_C": Form_Basic.form.CB_new_C.Checked = false; break;
                }
            }

            if (Form1.FormRepeat_Open)// Form 생성유무 확인
            {
                switch (position)
                {
                    case "반복_A": Form_Repeat.form.combo_repeat_use_condition_A.SelectedIndex = 0; break;
                    case "반복_B": Form_Repeat.form.combo_repeat_use_condition_B.SelectedIndex = 0; break;
                    case "반복_C": Form_Repeat.form.combo_repeat_use_condition_C.SelectedIndex = 0; break;
                    case "반복_D": Form_Repeat.form.combo_repeat_use_condition_D.SelectedIndex = 0; break;
                    case "반복_E": Form_Repeat.form.combo_repeat_use_condition_E.SelectedIndex = 0; break;
                    case "반복_F": Form_Repeat.form.combo_repeat_use_condition_F.SelectedIndex = 0; break;
                    case "반복_G": Form_Repeat.form.combo_repeat_use_condition_G.SelectedIndex = 0; break;
                    case "반복_H": Form_Repeat.form.combo_repeat_use_condition_H.SelectedIndex = 0; break;
                    case "반복_I": Form_Repeat.form.combo_repeat_use_condition_I.SelectedIndex = 0; break;
                    case "반복_J": Form_Repeat.form.combo_repeat_use_condition_J.SelectedIndex = 0; break;
                    case "반복_K": Form_Repeat.form.combo_repeat_use_condition_K.SelectedIndex = 0; break;
                    case "반복_L": Form_Repeat.form.combo_repeat_use_condition_L.SelectedIndex = 0; break;
                    case "반복_M": Form_Repeat.form.combo_repeat_use_condition_M.SelectedIndex = 0; break;
                    case "반복_N": Form_Repeat.form.combo_repeat_use_condition_N.SelectedIndex = 0; break;
                }
            }

            if (Form1.FormAccountManagement_Open)// Form 생성유무 확인
            {
                switch (position)
                {
                    case "리밸_A": Form_AccountManagement.form.combo_rebalance_use_condition_A.SelectedIndex = 0; break;
                    case "리밸_B": Form_AccountManagement.form.combo_rebalance_use_condition_B.SelectedIndex = 0; break;
                    case "리밸_C": Form_AccountManagement.form.combo_rebalance_use_condition_C.SelectedIndex = 0; break;
                    case "리밸_D": Form_AccountManagement.form.combo_rebalance_use_condition_D.SelectedIndex = 0; break;
                    case "리밸_E": Form_AccountManagement.form.combo_rebalance_use_condition_E.SelectedIndex = 0; break;
                    case "리밸_F": Form_AccountManagement.form.combo_rebalance_use_condition_F.SelectedIndex = 0; break;
                    case "리밸_G": Form_AccountManagement.form.combo_rebalance_use_condition_G.SelectedIndex = 0; break;
                    case "청산_A": Form_AccountManagement.form.CBB_Liquidation_use_condition_A.SelectedIndex = 0; break;
                    case "청산_B": Form_AccountManagement.form.CBB_Liquidation_use_condition_B.SelectedIndex = 0; break;
                    case "청산_C": Form_AccountManagement.form.CBB_Liquidation_use_condition_C.SelectedIndex = 0; break;
                }
            }

            switch (position)
            {
                case "신규_A": Properties.Settings.Default.CB_new_A = false; break;
                case "신규_B": Properties.Settings.Default.CB_new_B = false; break;
                case "신규_C": Properties.Settings.Default.CB_new_C = false; break;

                case "반복_A": Properties.Settings.Default.combo_repeat_use_condition_A = 0; break;
                case "반복_B": Properties.Settings.Default.combo_repeat_use_condition_B = 0; break;
                case "반복_C": Properties.Settings.Default.combo_repeat_use_condition_C = 0; break;
                case "반복_D": Properties.Settings.Default.combo_repeat_use_condition_D = 0; break;
                case "반복_E": Properties.Settings.Default.combo_repeat_use_condition_E = 0; break;
                case "반복_F": Properties.Settings.Default.combo_repeat_use_condition_F = 0; break;
                case "반복_G": Properties.Settings.Default.combo_repeat_use_condition_G = 0; break;
                case "반복_H": Properties.Settings.Default.combo_repeat_use_condition_H = 0; break;
                case "반복_I": Properties.Settings.Default.combo_repeat_use_condition_I = 0; break;
                case "반복_J": Properties.Settings.Default.combo_repeat_use_condition_J = 0; break;
                case "반복_K": Properties.Settings.Default.combo_repeat_use_condition_K = 0; break;
                case "반복_L": Properties.Settings.Default.combo_repeat_use_condition_L = 0; break;
                case "반복_M": Properties.Settings.Default.combo_repeat_use_condition_M = 0; break;
                case "반복_N": Properties.Settings.Default.combo_repeat_use_condition_N = 0; break;

                case "리밸_A": Properties.Settings.Default.combo_rebalance_use_condition_A = 0; break;
                case "리밸_B": Properties.Settings.Default.combo_rebalance_use_condition_B = 0; break;
                case "리밸_C": Properties.Settings.Default.combo_rebalance_use_condition_C = 0; break;
                case "리밸_D": Properties.Settings.Default.combo_rebalance_use_condition_D = 0; break;
                case "리밸_E": Properties.Settings.Default.combo_rebalance_use_condition_E = 0; break;
                case "리밸_F": Properties.Settings.Default.combo_rebalance_use_condition_F = 0; break;
                case "리밸_G": Properties.Settings.Default.combo_rebalance_use_condition_G = 0; break;

                case "청산_A": Properties.Settings.Default.CBB_Liquidation_use_condition_A = 0; break;
                case "청산_B": Properties.Settings.Default.CBB_Liquidation_use_condition_B = 0; break;
                case "청산_C": Properties.Settings.Default.CBB_Liquidation_use_condition_C = 0; break;


                case "Watch_A":
                    Form1.form1.CB_watch_use_A.Checked = false;
                    Properties.Settings.Default.CB_watch_use_A = false;
                    break;

                case "Watch_B":
                    Form1.form1.CB_watch_use_B.Checked = false;
                    Properties.Settings.Default.CB_watch_use_B = false;
                    break;

                case "Watch_C":
                    Form1.form1.CB_watch_use_C.Checked = false;
                    Properties.Settings.Default.CB_watch_use_C = false;
                    break;

                case "Watch_D":
                    Form1.form1.CB_watch_use_D.Checked = false;
                    Properties.Settings.Default.CB_watch_use_D = false;
                    break;
            }

        }


        public static void 검색식위치저장(string position, string 검색식)
        {
            switch (position)
            {
                case "신규_A": Properties.Settings.Default.combo_new_condition_A = 검색식; break;
                case "신규_B": Properties.Settings.Default.combo_new_condition_B = 검색식; break;
                case "신규_C": Properties.Settings.Default.combo_new_condition_C = 검색식; break;
                case "반복_A": Properties.Settings.Default.combo_repeat_condition_A = 검색식; break;
                case "반복_B": Properties.Settings.Default.combo_repeat_condition_B = 검색식; break;
                case "반복_C": Properties.Settings.Default.combo_repeat_condition_C = 검색식; break;
                case "반복_D": Properties.Settings.Default.combo_repeat_condition_D = 검색식; break;
                case "반복_E": Properties.Settings.Default.combo_repeat_condition_E = 검색식; break;
                case "반복_F": Properties.Settings.Default.combo_repeat_condition_F = 검색식; break;
                case "반복_G": Properties.Settings.Default.combo_repeat_condition_G = 검색식; break;
                case "반복_H": Properties.Settings.Default.combo_repeat_condition_H = 검색식; break;
                case "반복_I": Properties.Settings.Default.combo_repeat_condition_I = 검색식; break;
                case "반복_J": Properties.Settings.Default.combo_repeat_condition_J = 검색식; break;
                case "반복_K": Properties.Settings.Default.combo_repeat_condition_K = 검색식; break;
                case "반복_L": Properties.Settings.Default.combo_repeat_condition_L = 검색식; break;
                case "반복_M": Properties.Settings.Default.combo_repeat_condition_M = 검색식; break;
                case "반복_N": Properties.Settings.Default.combo_repeat_condition_N = 검색식; break;
                case "리밸_A": Properties.Settings.Default.combo_rebalance_condition_A = 검색식; break;
                case "리밸_B": Properties.Settings.Default.combo_rebalance_condition_B = 검색식; break;
                case "리밸_C": Properties.Settings.Default.combo_rebalance_condition_C = 검색식; break;
                case "리밸_D": Properties.Settings.Default.combo_rebalance_condition_D = 검색식; break;
                case "리밸_E": Properties.Settings.Default.combo_rebalance_condition_E = 검색식; break;
                case "리밸_F": Properties.Settings.Default.combo_rebalance_condition_F = 검색식; break;
                case "리밸_G": Properties.Settings.Default.combo_rebalance_condition_G = 검색식; break;
                case "청산_A": Properties.Settings.Default.CBB_Liquidation_condition_A = 검색식; break;
                case "청산_B": Properties.Settings.Default.CBB_Liquidation_condition_B = 검색식; break;
                case "청산_C": Properties.Settings.Default.CBB_Liquidation_condition_C = 검색식; break;
                case "Watch_A": Properties.Settings.Default.combo_watch_condition_AA = 검색식; break;
                case "Watch_B": Properties.Settings.Default.combo_watch_condition_BB = 검색식; break;
                case "Watch_C": Properties.Settings.Default.combo_watch_condition_CC = 검색식; break;
                case "Watch_D": Properties.Settings.Default.combo_watch_condition_DD = 검색식; break;
            }
        }


        public static void 검색식강제중지(string 검색식)
        {
            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                if (Properties.Settings.Default.combo_new_condition_A.Equals(검색식)) Stop_Monitoring(리턴(), "신규_A");
                if (Properties.Settings.Default.combo_new_condition_B.Equals(검색식)) Stop_Monitoring(리턴(), "신규_B");
                if (Properties.Settings.Default.combo_new_condition_C.Equals(검색식)) Stop_Monitoring(리턴(), "신규_C");

                if (Properties.Settings.Default.combo_repeat_condition_A.Equals(검색식)) Stop_Monitoring(리턴(), "반복_A");
                if (Properties.Settings.Default.combo_repeat_condition_B.Equals(검색식)) Stop_Monitoring(리턴(), "반복_B");
                if (Properties.Settings.Default.combo_repeat_condition_C.Equals(검색식)) Stop_Monitoring(리턴(), "반복_C");
                if (Properties.Settings.Default.combo_repeat_condition_D.Equals(검색식)) Stop_Monitoring(리턴(), "반복_D");
                if (Properties.Settings.Default.combo_repeat_condition_E.Equals(검색식)) Stop_Monitoring(리턴(), "반복_E");
                if (Properties.Settings.Default.combo_repeat_condition_F.Equals(검색식)) Stop_Monitoring(리턴(), "반복_F");
                if (Properties.Settings.Default.combo_repeat_condition_G.Equals(검색식)) Stop_Monitoring(리턴(), "반복_G");
                if (Properties.Settings.Default.combo_repeat_condition_H.Equals(검색식)) Stop_Monitoring(리턴(), "반복_H");
                if (Properties.Settings.Default.combo_repeat_condition_I.Equals(검색식)) Stop_Monitoring(리턴(), "반복_I");
                if (Properties.Settings.Default.combo_repeat_condition_J.Equals(검색식)) Stop_Monitoring(리턴(), "반복_J");
                if (Properties.Settings.Default.combo_repeat_condition_K.Equals(검색식)) Stop_Monitoring(리턴(), "반복_K");
                if (Properties.Settings.Default.combo_repeat_condition_L.Equals(검색식)) Stop_Monitoring(리턴(), "반복_L");
                if (Properties.Settings.Default.combo_repeat_condition_M.Equals(검색식)) Stop_Monitoring(리턴(), "반복_M");
                if (Properties.Settings.Default.combo_repeat_condition_N.Equals(검색식)) Stop_Monitoring(리턴(), "반복_N");

                if (Properties.Settings.Default.combo_rebalance_condition_A.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_A");
                if (Properties.Settings.Default.combo_rebalance_condition_B.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_B");
                if (Properties.Settings.Default.combo_rebalance_condition_C.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_C");
                if (Properties.Settings.Default.combo_rebalance_condition_D.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_D");
                if (Properties.Settings.Default.combo_rebalance_condition_E.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_E");
                if (Properties.Settings.Default.combo_rebalance_condition_F.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_F");
                if (Properties.Settings.Default.combo_rebalance_condition_G.Equals(검색식)) Stop_Monitoring(리턴(), "리밸_G");
                if (Properties.Settings.Default.CBB_Liquidation_condition_A.Equals(검색식)) Stop_Monitoring(리턴(), "청산_A");
                if (Properties.Settings.Default.CBB_Liquidation_condition_B.Equals(검색식)) Stop_Monitoring(리턴(), "청산_B");
                if (Properties.Settings.Default.CBB_Liquidation_condition_C.Equals(검색식)) Stop_Monitoring(리턴(), "청산_C");

                if (Properties.Settings.Default.combo_watch_condition_AA.Equals(검색식)) Stop_Monitoring(리턴(), "Watch_A");
                if (Properties.Settings.Default.combo_watch_condition_BB.Equals(검색식)) Stop_Monitoring(리턴(), "Watch_B");
                if (Properties.Settings.Default.combo_watch_condition_CC.Equals(검색식)) Stop_Monitoring(리턴(), "Watch_C");
                if (Properties.Settings.Default.combo_watch_condition_DD.Equals(검색식)) Stop_Monitoring(리턴(), "Watch_D");
            });

            Condition 리턴()
            {
                Condition result = Form1.form1.ConditionList.Find(o => o.name.Equals(검색식));
                return result;
            }
        }


        // 실시간 조건식 사용 해제
        public static void Stop_Monitoring(Condition condition, string position)
        {
            Task JO_Manager = new Task(() =>
            {
                Form1.form1.Invoke((MethodInvoker)delegate ()
                {
                    try
                    {
                        if (!Overlap_condition(condition.name))
                        {
                            Fail_condition fail = Form1.form1.fail_condition_List.Find(o => o.condition.Equals(condition.name));
                            if (fail == null)
                            {
                                if (Form1.Run_condition_List.Contains(condition.name))
                                {
                                    Form1.form1.axKHOpenAPI1.SendConditionStop("1100", condition.name, condition.index);

                                    Form1.동작_Log(position + " - " + condition.name + " 실시간 감시를 해제 합니다.");
                                    Form1.비프음("정지");
                                    Form1.Run_condition_List.Remove(condition.name);

                                    RunCondition search_Run = Form1.form1.search_condition_List.Find(o => o.condition.Equals(condition.name));
                                    if (search_Run != null)
                                    {
                                        Form1.form1.search_condition_List.Remove(search_Run);
                                    }
                                }

                                사용중지_(position);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Stop_Monitoring :: " + condition.name + "  검색식 중복입니다." + " position:: " + position);
                        }
                    }
                    catch (Exception e)
                    {
                        string message = e.Message.ToString();

                        Form1.알림창("[ 검색식 'Stop' 실패 ]\n\n검색식:" + condition.name + "\n\n검색식 'Stop' 에러내역 :: " + message, 5, false);

                        Form1.Error_Log(" ");
                        Form1.Error_Log("[검색식 'Stop' 실패] 검색식:" + condition.name + " 검색식 'Stop' 에러내역 :: " + message);
                        Form1.Error_Log(" ");

                        Form1.비프음("언체크");
                    }
                });
            });
            Form1.condotionManager.RequestTrData(JO_Manager);  // 생성된 Task 조스트 매니지먼트에 요청 등록. 
        }

        public static void 검색식사용제한()
        {
            Form1.form1.검색식_tick++;
            if (Form1.form1.검색식_tick >= 4)
            {
                Form1.form1.검색식_tick = 0;
                Form1.form1.Condition_Catch_List.Clear();
            }

            if (Form1.로딩완료 && (Form1.server_알림.Contains("마켓") || Form1.server_알림.Contains("동시")))
            {
                foreach (var item in Form1.form1.Condition_Catch_List.ToList())
                {
                    string 검색식 = item.Split('^')[1];
                    List<string> Group = Form1.form1.Condition_Catch_List.FindAll(o => o.Contains(검색식));

                    if (Group.Count > 100)
                    {
                        Form1.동작_Log("");
                        Form1.AutoClosingAlram(검색식 + " 식 사용 중지 됩니다. 지니_64가 원할히 가동되기 위해 1초당 100개 이상 실시간 검색되는 조건식은 사용할수 없습니다. 검색식 을 적절히 수정하여 사용하세요.", "검색식 사용제한", 1800, "동작");
                        Form1.동작_Log("");
                        Form1.Error_Log("");
                        Form1.Error_Log(검색식 + " 식이 사용 중지 됩니다. 지니_64가 원할히 가동되기 위해 1초당 100개 이상 실시간 검색되는 조건식은 사용할수 없습니다. 검색식 을 적절히 수정하여 사용하세요.");
                        Form1.Error_Log("");

                        검색식강제중지(검색식);

                        for (int n = 0; n < Group.Count; n++)
                        {
                            string Remove = Group[n];

                            Form1.form1.Condition_Catch_List.Remove(Remove);
                        }
                    }
                }
            }
        }

        public static void 검색식사용불가_강제정지()
        {
            if (Form1.신규조회_List.Count > 30)
            {
                string 검색식 = Form1.신규조회_List[0].검색식;
                string 위치 = "";

                List<신규조회> 조회개수_List = Form1.신규조회_List.FindAll(o => o.검색식.Equals(검색식));

                if (조회개수_List.Count > 30)
                {
                    if (검색식.Equals(Properties.Settings.Default.combo_new_condition_A) && Properties.Settings.Default.CB_new_A)
                    {
                        Stop_Monitoring(리턴(), "신규_A");
                        위치 = "신규_A";
                        알림();
                    }
                    if (검색식.Equals(Properties.Settings.Default.combo_new_condition_B) && Properties.Settings.Default.CB_new_B)
                    {
                        Stop_Monitoring(리턴(), "신규_B");
                        위치 = "신규_B";
                        알림();
                    }
                    if (검색식.Equals(Properties.Settings.Default.combo_new_condition_C) && Properties.Settings.Default.CB_new_C)
                    {
                        Stop_Monitoring(리턴(), "신규_C");
                        위치 = "신규_C";
                        알림();
                    }

                    void 알림()
                    {
                        Form1.AutoClosingAlram(위치 + " [" + 검색식 + "] 검색식을 강제 중지합니다. \n신규매수 검색식의 검색허용치는 초당 20개로 제한됩니다.", "검색허용치 초과", 1800, "동작");
                        Form1.Error_Log(위치 + " [" + 검색식 + "] 검색식을 강제 중지합니다. 신규매수 검색식의 검색허용치는 초당 20개로 제한됩니다.");
                    }
                }

                Condition 리턴()
                {
                    Condition result = Form1.form1.ConditionList.Find(o => o.name.Equals(검색식));
                    return result;
                }
            }

            if (Form1.form1.fail_condition_List.Count > 0)
            {
                foreach (var item in Form1.form1.fail_condition_List.ToList())
                {
                    if (item.count > 0)
                    {
                        item.count--;

                        if (item.count == 0)
                        {
                            Form1.form1.fail_condition_List.Remove(item);
                        }
                    }
                }
            }
        }


        public static bool 시장가대금탐색()
        {
            bool T = false;
            bool CBnew_A = Properties.Settings.Default.CB_new_A;
            bool CBnew_B = Properties.Settings.Default.CB_new_B;
            bool CBnew_C = Properties.Settings.Default.CB_new_C;

            if (Form1.FormBasic_Open)
            {
                CBnew_A = Form_Basic.form.CB_new_A.Checked;
                CBnew_B = Form_Basic.form.CB_new_B.Checked;
                CBnew_C = Form_Basic.form.CB_new_C.Checked;
            }

            bool CBrepeat_use_A = Properties.Settings.Default.CB_repeat_use_A;
            bool CBrepeat_use_B = Properties.Settings.Default.CB_repeat_use_B;
            bool CBrepeat_use_C = Properties.Settings.Default.CB_repeat_use_C;
            bool CBrepeat_use_D = Properties.Settings.Default.CB_repeat_use_D;
            bool CBrepeat_use_E = Properties.Settings.Default.CB_repeat_use_E;
            bool CBrepeat_use_F = Properties.Settings.Default.CB_repeat_use_F;
            bool CBrepeat_use_G = Properties.Settings.Default.CB_repeat_use_G;
            bool CBrepeat_use_H = Properties.Settings.Default.CB_repeat_use_H;
            bool CBrepeat_use_I = Properties.Settings.Default.CB_repeat_use_I;
            bool CBrepeat_use_J = Properties.Settings.Default.CB_repeat_use_J;
            bool CBrepeat_use_K = Properties.Settings.Default.CB_repeat_use_K;
            bool CBrepeat_use_L = Properties.Settings.Default.CB_repeat_use_L;
            bool CBrepeat_use_M = Properties.Settings.Default.CB_repeat_use_M;
            bool CBrepeat_use_N = Properties.Settings.Default.CB_repeat_use_N;

            int CBBrepeat_use_condition_A = Properties.Settings.Default.combo_repeat_use_condition_A;
            int CBBrepeat_use_condition_B = Properties.Settings.Default.combo_repeat_use_condition_B;
            int CBBrepeat_use_condition_C = Properties.Settings.Default.combo_repeat_use_condition_C;
            int CBBrepeat_use_condition_D = Properties.Settings.Default.combo_repeat_use_condition_D;
            int CBBrepeat_use_condition_E = Properties.Settings.Default.combo_repeat_use_condition_E;
            int CBBrepeat_use_condition_F = Properties.Settings.Default.combo_repeat_use_condition_F;
            int CBBrepeat_use_condition_G = Properties.Settings.Default.combo_repeat_use_condition_G;
            int CBBrepeat_use_condition_H = Properties.Settings.Default.combo_repeat_use_condition_H;
            int CBBrepeat_use_condition_I = Properties.Settings.Default.combo_repeat_use_condition_I;
            int CBBrepeat_use_condition_J = Properties.Settings.Default.combo_repeat_use_condition_J;
            int CBBrepeat_use_condition_K = Properties.Settings.Default.combo_repeat_use_condition_K;
            int CBBrepeat_use_condition_L = Properties.Settings.Default.combo_repeat_use_condition_L;
            int CBBrepeat_use_condition_M = Properties.Settings.Default.combo_repeat_use_condition_M;
            int CBBrepeat_use_condition_N = Properties.Settings.Default.combo_repeat_use_condition_N;

            if (Form1.FormRepeat_Open)
            {
                CBrepeat_use_A = Form_Repeat.form.CB_repeat_use_A.Checked;
                CBrepeat_use_B = Form_Repeat.form.CB_repeat_use_B.Checked;
                CBrepeat_use_C = Form_Repeat.form.CB_repeat_use_C.Checked;
                CBrepeat_use_D = Form_Repeat.form.CB_repeat_use_D.Checked;
                CBrepeat_use_E = Form_Repeat.form.CB_repeat_use_E.Checked;
                CBrepeat_use_F = Form_Repeat.form.CB_repeat_use_F.Checked;
                CBrepeat_use_G = Form_Repeat.form.CB_repeat_use_G.Checked;
                CBrepeat_use_H = Form_Repeat.form.CB_repeat_use_H.Checked;
                CBrepeat_use_I = Form_Repeat.form.CB_repeat_use_I.Checked;
                CBrepeat_use_J = Form_Repeat.form.CB_repeat_use_J.Checked;
                CBrepeat_use_K = Form_Repeat.form.CB_repeat_use_K.Checked;
                CBrepeat_use_L = Form_Repeat.form.CB_repeat_use_L.Checked;
                CBrepeat_use_M = Form_Repeat.form.CB_repeat_use_M.Checked;
                CBrepeat_use_N = Form_Repeat.form.CB_repeat_use_N.Checked;

                CBBrepeat_use_condition_A = Form_Repeat.form.combo_repeat_use_condition_A.SelectedIndex;
                CBBrepeat_use_condition_B = Form_Repeat.form.combo_repeat_use_condition_B.SelectedIndex;
                CBBrepeat_use_condition_C = Form_Repeat.form.combo_repeat_use_condition_C.SelectedIndex;
                CBBrepeat_use_condition_D = Form_Repeat.form.combo_repeat_use_condition_D.SelectedIndex;
                CBBrepeat_use_condition_E = Form_Repeat.form.combo_repeat_use_condition_E.SelectedIndex;
                CBBrepeat_use_condition_F = Form_Repeat.form.combo_repeat_use_condition_F.SelectedIndex;
                CBBrepeat_use_condition_G = Form_Repeat.form.combo_repeat_use_condition_G.SelectedIndex;
                CBBrepeat_use_condition_H = Form_Repeat.form.combo_repeat_use_condition_H.SelectedIndex;
                CBBrepeat_use_condition_I = Form_Repeat.form.combo_repeat_use_condition_I.SelectedIndex;
                CBBrepeat_use_condition_J = Form_Repeat.form.combo_repeat_use_condition_J.SelectedIndex;
                CBBrepeat_use_condition_K = Form_Repeat.form.combo_repeat_use_condition_K.SelectedIndex;
                CBBrepeat_use_condition_L = Form_Repeat.form.combo_repeat_use_condition_L.SelectedIndex;
                CBBrepeat_use_condition_M = Form_Repeat.form.combo_repeat_use_condition_M.SelectedIndex;
                CBBrepeat_use_condition_N = Form_Repeat.form.combo_repeat_use_condition_N.SelectedIndex;
            }

            bool CBrebalance_A = Properties.Settings.Default.CB_rebalance_A;
            bool CBrebalance_B = Properties.Settings.Default.CB_rebalance_B;
            bool CBrebalance_C = Properties.Settings.Default.CB_rebalance_C;
            bool CBrebalance_D = Properties.Settings.Default.CB_rebalance_D;
            bool CBrebalance_E = Properties.Settings.Default.CB_rebalance_E;
            bool CBrebalance_F = Properties.Settings.Default.CB_rebalance_F;
            bool CBrebalance_G = Properties.Settings.Default.CB_rebalance_G;

            int CBBrebalance_use_condition_A = Properties.Settings.Default.combo_rebalance_use_condition_A;
            int CBBrebalance_use_condition_B = Properties.Settings.Default.combo_rebalance_use_condition_B;
            int CBBrebalance_use_condition_C = Properties.Settings.Default.combo_rebalance_use_condition_C;
            int CBBrebalance_use_condition_D = Properties.Settings.Default.combo_rebalance_use_condition_D;
            int CBBrebalance_use_condition_E = Properties.Settings.Default.combo_rebalance_use_condition_E;
            int CBBrebalance_use_condition_F = Properties.Settings.Default.combo_rebalance_use_condition_F;
            int CBBrebalance_use_condition_G = Properties.Settings.Default.combo_rebalance_use_condition_G;

            bool CBLiquidation_A = Properties.Settings.Default.CB_Liquidation_A;
            bool CBLiquidation_B = Properties.Settings.Default.CB_Liquidation_B;
            bool CBLiquidation_C = Properties.Settings.Default.CB_Liquidation_C;

            int CBBLiquidation_use_condition_A = Properties.Settings.Default.CBB_Liquidation_use_condition_A;
            int CBBLiquidation_use_condition_B = Properties.Settings.Default.CBB_Liquidation_use_condition_B;
            int CBBLiquidation_use_condition_C = Properties.Settings.Default.CBB_Liquidation_use_condition_C;

            if (Form1.FormAccountManagement_Open)
            {
                CBrebalance_A = Form_AccountManagement.form.CB_rebalance_A.Checked;
                CBrebalance_B = Form_AccountManagement.form.CB_rebalance_B.Checked;
                CBrebalance_C = Form_AccountManagement.form.CB_rebalance_C.Checked;
                CBrebalance_D = Form_AccountManagement.form.CB_rebalance_D.Checked;
                CBrebalance_E = Form_AccountManagement.form.CB_rebalance_E.Checked;
                CBrebalance_F = Form_AccountManagement.form.CB_rebalance_F.Checked;
                CBrebalance_G = Form_AccountManagement.form.CB_rebalance_G.Checked;

                CBBrebalance_use_condition_A = Form_AccountManagement.form.combo_rebalance_use_condition_A.SelectedIndex;
                CBBrebalance_use_condition_B = Form_AccountManagement.form.combo_rebalance_use_condition_B.SelectedIndex;
                CBBrebalance_use_condition_C = Form_AccountManagement.form.combo_rebalance_use_condition_C.SelectedIndex;
                CBBrebalance_use_condition_D = Form_AccountManagement.form.combo_rebalance_use_condition_D.SelectedIndex;
                CBBrebalance_use_condition_E = Form_AccountManagement.form.combo_rebalance_use_condition_E.SelectedIndex;
                CBBrebalance_use_condition_F = Form_AccountManagement.form.combo_rebalance_use_condition_F.SelectedIndex;
                CBBrebalance_use_condition_G = Form_AccountManagement.form.combo_rebalance_use_condition_G.SelectedIndex;

                CBLiquidation_A = Form_AccountManagement.form.CB_Liquidation_A.Checked;
                CBLiquidation_B = Form_AccountManagement.form.CB_Liquidation_B.Checked;
                CBLiquidation_C = Form_AccountManagement.form.CB_Liquidation_C.Checked;

                CBBLiquidation_use_condition_A = Form_AccountManagement.form.CBB_Liquidation_use_condition_A.SelectedIndex;
                CBBLiquidation_use_condition_B = Form_AccountManagement.form.CBB_Liquidation_use_condition_B.SelectedIndex;
                CBBLiquidation_use_condition_C = Form_AccountManagement.form.CBB_Liquidation_use_condition_C.SelectedIndex;
            }

            if (CBnew_A) 검색식확인(Properties.Settings.Default.combo_new_condition_A);
            if (!T && CBnew_B) 검색식확인(Properties.Settings.Default.combo_new_condition_B);
            if (!T && CBnew_C) 검색식확인(Properties.Settings.Default.combo_new_condition_C);

            if (!T && CBrebalance_A && CBBrebalance_use_condition_A > 0) 검색식확인(Properties.Settings.Default.combo_rebalance_condition_A);
            if (!T && CBrebalance_B && CBBrebalance_use_condition_B > 0) 검색식확인(Properties.Settings.Default.combo_rebalance_condition_B);
            if (!T && CBrebalance_C && CBBrebalance_use_condition_C > 0) 검색식확인(Properties.Settings.Default.combo_rebalance_condition_C);
            if (!T && CBrebalance_D && CBBrebalance_use_condition_D > 0) 검색식확인(Properties.Settings.Default.combo_rebalance_condition_D);
            if (!T && CBrebalance_E && CBBrebalance_use_condition_E > 0) 검색식확인(Properties.Settings.Default.combo_rebalance_condition_E);
            if (!T && CBrebalance_F && CBBrebalance_use_condition_F > 0) 검색식확인(Properties.Settings.Default.combo_rebalance_condition_F);
            if (!T && CBrebalance_G && CBBrebalance_use_condition_G > 0) 검색식확인(Properties.Settings.Default.combo_rebalance_condition_G);

            if (!T && CBrepeat_use_A && CBBrepeat_use_condition_A > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_A);
            if (!T && CBrepeat_use_B && CBBrepeat_use_condition_B > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_B);
            if (!T && CBrepeat_use_C && CBBrepeat_use_condition_C > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_C);
            if (!T && CBrepeat_use_D && CBBrepeat_use_condition_D > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_D);
            if (!T && CBrepeat_use_E && CBBrepeat_use_condition_E > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_E);
            if (!T && CBrepeat_use_F && CBBrepeat_use_condition_F > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_F);
            if (!T && CBrepeat_use_G && CBBrepeat_use_condition_G > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_G);
            if (!T && CBrepeat_use_H && CBBrepeat_use_condition_H > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_H);
            if (!T && CBrepeat_use_I && CBBrepeat_use_condition_I > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_I);
            if (!T && CBrepeat_use_J && CBBrepeat_use_condition_J > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_J);
            if (!T && CBrepeat_use_K && CBBrepeat_use_condition_K > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_K);
            if (!T && CBrepeat_use_L && CBBrepeat_use_condition_L > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_L);
            if (!T && CBrepeat_use_M && CBBrepeat_use_condition_M > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_M);
            if (!T && CBrepeat_use_N && CBBrepeat_use_condition_N > 0) 검색식확인(Properties.Settings.Default.combo_repeat_condition_N);

            if (!T && Properties.Settings.Default.CB_watch_use_A) 검색식확인(Properties.Settings.Default.combo_watch_condition_AA);
            if (!T && Properties.Settings.Default.CB_watch_use_B) 검색식확인(Properties.Settings.Default.combo_watch_condition_BB);
            if (!T && Properties.Settings.Default.CB_watch_use_C) 검색식확인(Properties.Settings.Default.combo_watch_condition_CC);
            if (!T && Properties.Settings.Default.CB_watch_use_D) 검색식확인(Properties.Settings.Default.combo_watch_condition_DD);

            if (!T && CBLiquidation_A && CBBLiquidation_use_condition_A > 0) 검색식확인(Properties.Settings.Default.CBB_Liquidation_condition_A);
            if (!T && CBLiquidation_B && CBBLiquidation_use_condition_B > 0) 검색식확인(Properties.Settings.Default.CBB_Liquidation_condition_B);
            if (!T && CBLiquidation_C && CBBLiquidation_use_condition_C > 0) 검색식확인(Properties.Settings.Default.CBB_Liquidation_condition_C);

            void 검색식확인(string 검색식)
            {
                if (검색식.Equals(Properties.Settings.Default.TB_매수탐색A) || 검색식.Equals(Properties.Settings.Default.TB_매수탐색B) || 검색식.Equals(Properties.Settings.Default.TB_매도탐색))
                {
                    T = true;
                }
            }

            return T;
        }

        public static void 대금탐색취소(String 검색식)
        {
            if (Properties.Settings.Default.combo_new_condition_A.Equals(검색식)) Properties.Settings.Default.CB_new_A = false;
            if (Properties.Settings.Default.combo_new_condition_B.Equals(검색식)) Properties.Settings.Default.CB_new_B = false;
            if (Properties.Settings.Default.combo_new_condition_C.Equals(검색식)) Properties.Settings.Default.CB_new_C = false;

            if (Properties.Settings.Default.combo_watch_condition_AA.Equals(검색식)) Form1.form1.CB_watch_use_A.Checked = false;
            if (Properties.Settings.Default.combo_watch_condition_BB.Equals(검색식)) Form1.form1.CB_watch_use_B.Checked = false;
            if (Properties.Settings.Default.combo_watch_condition_CC.Equals(검색식)) Form1.form1.CB_watch_use_C.Checked = false;
            if (Properties.Settings.Default.combo_watch_condition_DD.Equals(검색식)) Form1.form1.CB_watch_use_D.Checked = false;

            if (Properties.Settings.Default.combo_repeat_condition_A.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_A = 0;
            if (Properties.Settings.Default.combo_repeat_condition_B.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_B = 0;
            if (Properties.Settings.Default.combo_repeat_condition_C.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_C = 0;
            if (Properties.Settings.Default.combo_repeat_condition_D.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_D = 0;
            if (Properties.Settings.Default.combo_repeat_condition_E.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_E = 0;
            if (Properties.Settings.Default.combo_repeat_condition_F.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_F = 0;
            if (Properties.Settings.Default.combo_repeat_condition_G.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_G = 0;
            if (Properties.Settings.Default.combo_repeat_condition_H.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_H = 0;
            if (Properties.Settings.Default.combo_repeat_condition_I.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_I = 0;
            if (Properties.Settings.Default.combo_repeat_condition_J.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_J = 0;
            if (Properties.Settings.Default.combo_repeat_condition_K.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_K = 0;
            if (Properties.Settings.Default.combo_repeat_condition_L.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_L = 0;
            if (Properties.Settings.Default.combo_repeat_condition_M.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_M = 0;
            if (Properties.Settings.Default.combo_repeat_condition_N.Equals(검색식)) Properties.Settings.Default.combo_repeat_use_condition_N = 0;

            if (Properties.Settings.Default.combo_rebalance_condition_A.Equals(검색식)) Properties.Settings.Default.combo_rebalance_use_condition_A = 0;
            if (Properties.Settings.Default.combo_rebalance_condition_B.Equals(검색식)) Properties.Settings.Default.combo_rebalance_use_condition_B = 0;
            if (Properties.Settings.Default.combo_rebalance_condition_C.Equals(검색식)) Properties.Settings.Default.combo_rebalance_use_condition_C = 0;
            if (Properties.Settings.Default.combo_rebalance_condition_D.Equals(검색식)) Properties.Settings.Default.combo_rebalance_use_condition_D = 0;
            if (Properties.Settings.Default.combo_rebalance_condition_E.Equals(검색식)) Properties.Settings.Default.combo_rebalance_use_condition_E = 0;
            if (Properties.Settings.Default.combo_rebalance_condition_F.Equals(검색식)) Properties.Settings.Default.combo_rebalance_use_condition_F = 0;
            if (Properties.Settings.Default.combo_rebalance_condition_G.Equals(검색식)) Properties.Settings.Default.combo_rebalance_use_condition_G = 0;
            if (Properties.Settings.Default.CBB_Liquidation_condition_A.Equals(검색식)) Properties.Settings.Default.CBB_Liquidation_use_condition_A = 0;
            if (Properties.Settings.Default.CBB_Liquidation_condition_B.Equals(검색식)) Properties.Settings.Default.CBB_Liquidation_use_condition_B = 0;
            if (Properties.Settings.Default.CBB_Liquidation_condition_C.Equals(검색식)) Properties.Settings.Default.CBB_Liquidation_use_condition_C = 0;
        }




    }
}
