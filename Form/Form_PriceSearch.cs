using System;
using System.Windows.Forms;

namespace 지니64
{
    public partial class Form_PriceSearch : Form
    {
        public static Form_PriceSearch form;
        public Form_PriceSearch()
        {
            form = this;
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer |
                           ControlStyles.UserPaint |
                           ControlStyles.AllPaintingInWmPaint |
                           ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
        }

        public void Form_PriceSearch_Load()
        {
            Form1.음소거 = true;

            TB_Buy_A_탐색주가_1.Text = GenieConfig.TB_Buy_A_탐색주가_1.ToString("N0");
            TB_Buy_A_탐색주가_2.Text = GenieConfig.TB_Buy_A_탐색주가_2.ToString("N0");
            TB_Buy_A_탐색주가_3.Text = GenieConfig.TB_Buy_A_탐색주가_3.ToString("N0");
            TB_Buy_A_탐색주가_4.Text = GenieConfig.TB_Buy_A_탐색주가_4.ToString("N0");
            TB_Buy_A_탐색주가_5.Text = GenieConfig.TB_Buy_A_탐색주가_5.ToString("N0");
            TB_Buy_A_탐색주가_6.Text = GenieConfig.TB_Buy_A_탐색주가_6.ToString("N0");
            TB_Buy_B_탐색주가_1.Text = GenieConfig.TB_Buy_B_탐색주가_1.ToString("N0");
            TB_Buy_B_탐색주가_2.Text = GenieConfig.TB_Buy_B_탐색주가_2.ToString("N0");
            TB_Buy_B_탐색주가_3.Text = GenieConfig.TB_Buy_B_탐색주가_3.ToString("N0");
            TB_Buy_B_탐색주가_4.Text = GenieConfig.TB_Buy_B_탐색주가_4.ToString("N0");
            TB_Buy_B_탐색주가_5.Text = GenieConfig.TB_Buy_B_탐색주가_5.ToString("N0");
            TB_Buy_B_탐색주가_6.Text = GenieConfig.TB_Buy_B_탐색주가_6.ToString("N0");

            TB_Buy_A_탐색대금_1.Text = GenieConfig.TB_Buy_A_탐색대금_1.ToString();
            TB_Buy_A_탐색대금_2.Text = GenieConfig.TB_Buy_A_탐색대금_2.ToString();
            TB_Buy_A_탐색대금_3.Text = GenieConfig.TB_Buy_A_탐색대금_3.ToString();
            TB_Buy_A_탐색대금_4.Text = GenieConfig.TB_Buy_A_탐색대금_4.ToString();
            TB_Buy_A_탐색대금_5.Text = GenieConfig.TB_Buy_A_탐색대금_5.ToString();
            TB_Buy_A_탐색대금_6.Text = GenieConfig.TB_Buy_A_탐색대금_6.ToString();
            TB_Buy_B_탐색대금_1.Text = GenieConfig.TB_Buy_B_탐색대금_1.ToString();
            TB_Buy_B_탐색대금_2.Text = GenieConfig.TB_Buy_B_탐색대금_2.ToString();
            TB_Buy_B_탐색대금_3.Text = GenieConfig.TB_Buy_B_탐색대금_3.ToString();
            TB_Buy_B_탐색대금_4.Text = GenieConfig.TB_Buy_B_탐색대금_4.ToString();
            TB_Buy_B_탐색대금_5.Text = GenieConfig.TB_Buy_B_탐색대금_5.ToString();
            TB_Buy_B_탐색대금_6.Text = GenieConfig.TB_Buy_B_탐색대금_6.ToString();
            TB_Buy_A_탐색rate.Text = GenieConfig.TB_Buy_A_탐색rate.ToString();
            TB_Buy_B_탐색rate.Text = GenieConfig.TB_Buy_B_탐색rate.ToString();

            MTB_M_반복.Text = GenieConfig.MTB_M_반복.ToString();
            CBB_M_잔량.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_M_잔량);
            TB_M_매도호가별대금.Text = GenieConfig.TB_M_매도호가별대금.ToString();
            TB_M_매수호가별대금.Text = GenieConfig.TB_M_매수호가별대금.ToString();
            TB_M_매도호가합대금.Text = GenieConfig.TB_M_매도호가합대금.ToString();
            TB_M_매수호가합대금.Text = GenieConfig.TB_M_매수호가합대금.ToString();

            MTB_M_반복_2.Text = GenieConfig.MTB_M_반복_2.ToString();
            CBB_M_잔량_2.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_M_잔량_2);
            TB_M_매도호가별대금_2.Text = GenieConfig.TB_M_매도호가별대금_2.ToString();
            TB_M_매수호가별대금_2.Text = GenieConfig.TB_M_매수호가별대금_2.ToString();
            TB_M_매도호가합대금_2.Text = GenieConfig.TB_M_매도호가합대금_2.ToString();
            TB_M_매수호가합대금_2.Text = GenieConfig.TB_M_매수호가합대금_2.ToString();

            CBB_Buy_A_분봉.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Buy_A_분봉);
            CBB_Buy_B_분봉.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Buy_B_분봉);
            CBB_Sell_탐색_분봉.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Sell_탐색_분봉);

            CBB_Buy_A_일봉.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Buy_A_일봉);
            CBB_Buy_B_일봉.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Buy_B_일봉);
            CBB_Sell_탐색_일봉.SelectedIndex = GET.GenieCombobox(GenieConfig.CBB_Sell_탐색_일봉);

            TB_accumulate_Price.Text = GenieConfig.TB_accumulate_Price.ToString();

            TB_Sell_탐색주가_1.Text = GenieConfig.TB_Sell_탐색주가_1.ToString("N0");
            TB_Sell_탐색주가_3.Text = GenieConfig.TB_Sell_탐색주가_2.ToString("N0");
            TB_Sell_탐색주가_5.Text = GenieConfig.TB_Sell_탐색주가_3.ToString("N0");
            TB_Sell_탐색주가_2.Text = GenieConfig.TB_Sell_탐색주가_4.ToString("N0");
            TB_Sell_탐색주가_4.Text = GenieConfig.TB_Sell_탐색주가_5.ToString("N0");
            TB_Sell_탐색주가_6.Text = GenieConfig.TB_Sell_탐색주가_6.ToString("N0");
            TB_Buy_A_기준초.Text = GenieConfig.TB_Buy_A_기준초.ToString();
            TB_Buy_B_기준초.Text = GenieConfig.TB_Buy_B_기준초.ToString();
            TB_Sell_기준초.Text = GenieConfig.TB_Sell_기준초.ToString();

            Combo_Buy_A_초회.SelectedIndex = GET.GenieCombobox(GenieConfig.Combo_Buy_A_초회);
            Combo_Buy_B_초회.SelectedIndex = GET.GenieCombobox(GenieConfig.Combo_Buy_B_초회);
            combo_Sell_초회.SelectedIndex = GET.GenieCombobox(GenieConfig.combo_Sell_초회);

            TB_Sell_탐색대금_1.Text = GenieConfig.TB_Sell_탐색대금_1.ToString();
            TB_Sell_탐색대금_3.Text = GenieConfig.TB_Sell_탐색대금_2.ToString();
            TB_Sell_탐색대금_5.Text = GenieConfig.TB_Sell_탐색대금_3.ToString();
            TB_Sell_탐색대금_2.Text = GenieConfig.TB_Sell_탐색대금_4.ToString();
            TB_Sell_탐색대금_4.Text = GenieConfig.TB_Sell_탐색대금_5.ToString();
            TB_Sell_탐색대금_6.Text = GenieConfig.TB_Sell_탐색대금_6.ToString();
            TB_Sell_탐색rate.Text = GenieConfig.TB_Sell_탐색rate.ToString();

            CB_매수탐색A.Checked = GenieConfig.CB_매수탐색A;
            CB_매수탐색B.Checked = GenieConfig.CB_매수탐색B;
            CB_매도탐색.Checked = GenieConfig.CB_매도탐색;

            TB_Buy_상승카운터_A.Text = GenieConfig.TB_Buy_상승카운터_A.ToString();
            TB_Buy_상승카운터_B.Text = GenieConfig.TB_Buy_상승카운터_B.ToString();
            TB_Buy_하락카운터_A.Text = GenieConfig.TB_Buy_하락카운터_A.ToString();
            TB_Buy_하락카운터_B.Text = GenieConfig.TB_Buy_하락카운터_B.ToString();
            TB_Sell_상승카운터.Text = GenieConfig.TB_Sell_상승카운터.ToString();
            TB_Sell_하락카운터.Text = GenieConfig.TB_Sell_하락카운터.ToString();

            form.CB_Buy_상승옵션_A.Checked = GenieConfig.CB_Buy_상승옵션_A;
            form.CB_Buy_상승옵션_B.Checked = GenieConfig.CB_Buy_상승옵션_B;
            form.CB_Buy_하락옵션_A.Checked = GenieConfig.CB_Buy_하락옵션_A;
            form.CB_Buy_하락옵션_B.Checked = GenieConfig.CB_Buy_하락옵션_B;
            form.CB_Sell_상승옵션.Checked = GenieConfig.CB_Sell_상승옵션;
            form.CB_Sell_하락옵션.Checked = GenieConfig.CB_Sell_하락옵션;

            this.ActiveControl = CBB_Buy_A_일봉;
            Form1.음소거 = GenieConfig.CB_음소거;

            if (GenieConfig.CB_가이드매매) ControllerDisable.Form_PriceSearch_Disable();

        }
        public static void 대금탐색_저장()
        {
            // ---------------------------------------------------------
            // 가격/대금 탐색 설정 저장 (Setting.pricesearch 사용)
            // ---------------------------------------------------------

            try
            {
                GenieConfig.CB_매수탐색A = form.CB_매수탐색A.Checked;
                GenieConfig.CB_매수탐색B = form.CB_매수탐색B.Checked;
                GenieConfig.CB_매도탐색 = form.CB_매도탐색.Checked;

                con_add(form.CB_매수탐색A.Checked, "매수탐색_A");
                con_add(form.CB_매수탐색B.Checked, "매수탐색_B");
                con_add(form.CB_매도탐색.Checked, "매도탐색");
                if (Form1.내아이디) con_add(true, "랭킹분석");

                void con_add(bool checke, string name)
                {
                    if (checke)
                    {
                        Condition item = Form1.ConditionList.Find(o => o.name.Equals(name));
                        if (item == null) Form1.ConditionList.Add(new Condition("index", name));
                    }
                    else
                    {
                        Condition item = Form1.ConditionList.Find(o => o.name.Equals(name));
                        if (item != null) Form1.ConditionList.Remove(item);
                        Condition_Management.대금탐색취소(name);
                    }
                }
            }
            catch (Exception e)
            {
                 Form1.Console_print("대금탐색 / 입력 오류 : " + e.Message); Log.에러기록("대금탐색 / 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_accumulate_Price.Text, out int accumulate_Price);
                if (accumulate_Price == 0) accumulate_Price = 1000;
                GenieConfig.TB_accumulate_Price = Math.Abs(accumulate_Price);
                form.TB_accumulate_Price.Text = GenieConfig.TB_accumulate_Price.ToString();

                int.TryParse(form.TB_Buy_A_기준초.Text, out int Buy_A_기준초);
                double.TryParse(form.TB_Buy_A_탐색rate.Text, out double Buy_A_탐색rate);
                int.TryParse(form.MTB_M_반복.Text, out int 반복);

                if (Buy_A_기준초 == 0) Buy_A_기준초 = 1;
                if (반복 < 2) 반복 = 5;

                GenieConfig.TB_Buy_A_기준초 = Math.Abs(Buy_A_기준초);
                GenieConfig.TB_Buy_A_탐색rate = Buy_A_탐색rate;
                GenieConfig.MTB_M_반복 = 반복;

                form.TB_Buy_A_기준초.Text = GenieConfig.TB_Buy_A_기준초.ToString();
                form.TB_Buy_A_탐색rate.Text = Buy_A_탐색rate.ToString();
                form.MTB_M_반복.Text = 반복.ToString();

                double.TryParse(form.TB_M_매도호가별대금.Text, out double 매도호가별대금);
                double.TryParse(form.TB_M_매수호가별대금.Text, out double 매수호가별대금);
                double.TryParse(form.TB_M_매도호가합대금.Text, out double 매도호가합대금);
                double.TryParse(form.TB_M_매수호가합대금.Text, out double 매수호가합대금);

                if (매도호가별대금 == 0) 매도호가별대금 = 10;
                if (매수호가별대금 == 0) 매수호가별대금 = 10;
                if (매도호가합대금 == 0) 매도호가합대금 = 100;
                if (매수호가합대금 == 0) 매수호가합대금 = 100;

                GenieConfig.TB_M_매도호가별대금 = Math.Abs(매도호가별대금);
                GenieConfig.TB_M_매수호가별대금 = Math.Abs(매수호가별대금);
                GenieConfig.TB_M_매도호가합대금 = Math.Abs(매도호가합대금);
                GenieConfig.TB_M_매수호가합대금 = Math.Abs(매수호가합대금);

                form.TB_M_매도호가별대금.Text = GenieConfig.TB_M_매도호가별대금.ToString();
                form.TB_M_매수호가별대금.Text = GenieConfig.TB_M_매수호가별대금.ToString();
                form.TB_M_매도호가합대금.Text = GenieConfig.TB_M_매도호가합대금.ToString();
                form.TB_M_매수호가합대금.Text = GenieConfig.TB_M_매수호가합대금.ToString();

                int.TryParse(form.TB_Buy_A_탐색주가_1.Text.Replace(",", ""), out int TB_Buy_A_탐색주가_1);
                int.TryParse(form.TB_Buy_A_탐색주가_2.Text.Replace(",", ""), out int TB_Buy_A_탐색주가_2);
                int.TryParse(form.TB_Buy_A_탐색주가_3.Text.Replace(",", ""), out int TB_Buy_A_탐색주가_3);
                int.TryParse(form.TB_Buy_A_탐색주가_4.Text.Replace(",", ""), out int TB_Buy_A_탐색주가_4);
                int.TryParse(form.TB_Buy_A_탐색주가_5.Text.Replace(",", ""), out int TB_Buy_A_탐색주가_5);
                int.TryParse(form.TB_Buy_A_탐색주가_6.Text.Replace(",", ""), out int TB_Buy_A_탐색주가_6);

                if (TB_Buy_A_탐색주가_1 == 0) TB_Buy_A_탐색주가_1 = 1000;
                if (TB_Buy_A_탐색주가_2 == 0) TB_Buy_A_탐색주가_2 = 5000;
                if (TB_Buy_A_탐색주가_3 == 0) TB_Buy_A_탐색주가_3 = 10000;
                if (TB_Buy_A_탐색주가_4 == 0) TB_Buy_A_탐색주가_4 = 15000;
                if (TB_Buy_A_탐색주가_5 == 0) TB_Buy_A_탐색주가_5 = 20000;
                if (TB_Buy_A_탐색주가_6 == 0) TB_Buy_A_탐색주가_6 = 25000;

                GenieConfig.TB_Buy_A_탐색주가_1 = Math.Abs(TB_Buy_A_탐색주가_1);
                GenieConfig.TB_Buy_A_탐색주가_2 = Math.Abs(TB_Buy_A_탐색주가_2);
                GenieConfig.TB_Buy_A_탐색주가_3 = Math.Abs(TB_Buy_A_탐색주가_3);
                GenieConfig.TB_Buy_A_탐색주가_4 = Math.Abs(TB_Buy_A_탐색주가_4);
                GenieConfig.TB_Buy_A_탐색주가_5 = Math.Abs(TB_Buy_A_탐색주가_5);
                GenieConfig.TB_Buy_A_탐색주가_6 = Math.Abs(TB_Buy_A_탐색주가_6);

                form.TB_Buy_A_탐색주가_1.Text = GenieConfig.TB_Buy_A_탐색주가_1.ToString();
                form.TB_Buy_A_탐색주가_2.Text = GenieConfig.TB_Buy_A_탐색주가_2.ToString();
                form.TB_Buy_A_탐색주가_3.Text = GenieConfig.TB_Buy_A_탐색주가_3.ToString();
                form.TB_Buy_A_탐색주가_4.Text = GenieConfig.TB_Buy_A_탐색주가_4.ToString();
                form.TB_Buy_A_탐색주가_5.Text = GenieConfig.TB_Buy_A_탐색주가_5.ToString();
                form.TB_Buy_A_탐색주가_6.Text = GenieConfig.TB_Buy_A_탐색주가_6.ToString();


                double.TryParse(form.TB_Buy_A_탐색대금_1.Text, out double Buy_A_탐색대금_1);
                double.TryParse(form.TB_Buy_A_탐색대금_2.Text, out double Buy_A_탐색대금_2);
                double.TryParse(form.TB_Buy_A_탐색대금_3.Text, out double Buy_A_탐색대금_3);
                double.TryParse(form.TB_Buy_A_탐색대금_4.Text, out double Buy_A_탐색대금_4);
                double.TryParse(form.TB_Buy_A_탐색대금_5.Text, out double Buy_A_탐색대금_5);
                double.TryParse(form.TB_Buy_A_탐색대금_6.Text, out double Buy_A_탐색대금_6);

                if (Buy_A_탐색대금_1 == 0) Buy_A_탐색대금_1 = 1000;
                if (Buy_A_탐색대금_2 == 0) Buy_A_탐색대금_2 = 5000;
                if (Buy_A_탐색대금_3 == 0) Buy_A_탐색대금_3 = 10000;
                if (Buy_A_탐색대금_4 == 0) Buy_A_탐색대금_4 = 15000;
                if (Buy_A_탐색대금_5 == 0) Buy_A_탐색대금_5 = 20000;
                if (Buy_A_탐색대금_6 == 0) Buy_A_탐색대금_6 = 25000;

                GenieConfig.TB_Buy_A_탐색대금_1 = Math.Abs(Buy_A_탐색대금_1);
                GenieConfig.TB_Buy_A_탐색대금_2 = Math.Abs(Buy_A_탐색대금_2);
                GenieConfig.TB_Buy_A_탐색대금_3 = Math.Abs(Buy_A_탐색대금_3);
                GenieConfig.TB_Buy_A_탐색대금_4 = Math.Abs(Buy_A_탐색대금_4);
                GenieConfig.TB_Buy_A_탐색대금_5 = Math.Abs(Buy_A_탐색대금_5);
                GenieConfig.TB_Buy_A_탐색대금_6 = Math.Abs(Buy_A_탐색대금_6);

                form.TB_Buy_A_탐색대금_1.Text = GenieConfig.TB_Buy_A_탐색대금_1.ToString();
                form.TB_Buy_A_탐색대금_2.Text = GenieConfig.TB_Buy_A_탐색대금_2.ToString();
                form.TB_Buy_A_탐색대금_3.Text = GenieConfig.TB_Buy_A_탐색대금_3.ToString();
                form.TB_Buy_A_탐색대금_4.Text = GenieConfig.TB_Buy_A_탐색대금_4.ToString();
                form.TB_Buy_A_탐색대금_5.Text = GenieConfig.TB_Buy_A_탐색대금_5.ToString();
                form.TB_Buy_A_탐색대금_6.Text = GenieConfig.TB_Buy_A_탐색대금_6.ToString();

                GenieConfig.CBB_M_잔량 = GET.ComboBoxIndex(form.CBB_M_잔량);
                GenieConfig.CBB_Buy_A_분봉 = GET.ComboBoxIndex(form.CBB_Buy_A_분봉);
                GenieConfig.CBB_Buy_A_일봉 = GET.ComboBoxIndex(form.CBB_Buy_A_일봉);
            }
            catch (Exception e)
            {
                 Form1.Console_print("대금탐색 / 매수 대금탐색_A 입력 오류 : " + e.Message); Log.에러기록("대금탐색 / 매수 대금탐색_A 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_Buy_B_기준초.Text, out int Buy_B_기준초);
                double.TryParse(form.TB_Buy_B_탐색rate.Text, out double Buy_B_탐색rate);
                int.TryParse(form.MTB_M_반복_2.Text, out int 반복_2);

                if (Buy_B_기준초 == 0) Buy_B_기준초 = 1;
                if (반복_2 < 2) 반복_2 = 5;

                GenieConfig.TB_Buy_B_기준초 = Math.Abs(Buy_B_기준초);
                GenieConfig.TB_Buy_B_탐색rate = Buy_B_탐색rate;
                GenieConfig.MTB_M_반복_2 = 반복_2;

                form.TB_Buy_B_기준초.Text = GenieConfig.TB_Buy_B_기준초.ToString();
                form.TB_Buy_B_탐색rate.Text = Buy_B_탐색rate.ToString();
                form.MTB_M_반복_2.Text = 반복_2.ToString();

                double.TryParse(form.TB_M_매수호가별대금_2.Text, out double 매수호가별대금_2);
                double.TryParse(form.TB_M_매도호가별대금_2.Text, out double 매도호가별대금_2);
                double.TryParse(form.TB_M_매도호가합대금_2.Text, out double 매도호가합대금_2);
                double.TryParse(form.TB_M_매수호가합대금_2.Text, out double 매수호가합대금_2);

                if (매수호가별대금_2 == 0) 매수호가별대금_2 = 10;
                if (매도호가별대금_2 == 0) 매도호가별대금_2 = 10;
                if (매도호가합대금_2 == 0) 매도호가합대금_2 = 100;
                if (매수호가합대금_2 == 0) 매수호가합대금_2 = 100;

                GenieConfig.TB_M_매수호가별대금_2 = Math.Abs(매수호가별대금_2);
                GenieConfig.TB_M_매도호가별대금_2 = Math.Abs(매도호가별대금_2);
                GenieConfig.TB_M_매도호가합대금_2 = Math.Abs(매도호가합대금_2);
                GenieConfig.TB_M_매수호가합대금_2 = Math.Abs(매수호가합대금_2);

                form.TB_M_매수호가별대금_2.Text = GenieConfig.TB_M_매수호가별대금_2.ToString();
                form.TB_M_매도호가별대금_2.Text = GenieConfig.TB_M_매도호가별대금_2.ToString();
                form.TB_M_매도호가합대금_2.Text = GenieConfig.TB_M_매도호가합대금_2.ToString();
                form.TB_M_매수호가합대금_2.Text = GenieConfig.TB_M_매수호가합대금_2.ToString();

                int.TryParse(form.TB_Buy_B_탐색주가_1.Text.Replace(",", ""), out int TB_Buy_B_탐색주가_1);
                int.TryParse(form.TB_Buy_B_탐색주가_2.Text.Replace(",", ""), out int TB_Buy_B_탐색주가_2);
                int.TryParse(form.TB_Buy_B_탐색주가_3.Text.Replace(",", ""), out int TB_Buy_B_탐색주가_3);
                int.TryParse(form.TB_Buy_B_탐색주가_4.Text.Replace(",", ""), out int TB_Buy_B_탐색주가_4);
                int.TryParse(form.TB_Buy_B_탐색주가_5.Text.Replace(",", ""), out int TB_Buy_B_탐색주가_5);
                int.TryParse(form.TB_Buy_B_탐색주가_6.Text.Replace(",", ""), out int TB_Buy_B_탐색주가_6);

                if (TB_Buy_B_탐색주가_1 == 0) TB_Buy_B_탐색주가_1 = 1000;
                if (TB_Buy_B_탐색주가_2 == 0) TB_Buy_B_탐색주가_2 = 5000;
                if (TB_Buy_B_탐색주가_3 == 0) TB_Buy_B_탐색주가_3 = 10000;
                if (TB_Buy_B_탐색주가_4 == 0) TB_Buy_B_탐색주가_4 = 15000;
                if (TB_Buy_B_탐색주가_5 == 0) TB_Buy_B_탐색주가_5 = 20000;
                if (TB_Buy_B_탐색주가_6 == 0) TB_Buy_B_탐색주가_6 = 25000;

                GenieConfig.TB_Buy_B_탐색주가_1 = Math.Abs(TB_Buy_B_탐색주가_1);
                GenieConfig.TB_Buy_B_탐색주가_2 = Math.Abs(TB_Buy_B_탐색주가_2);
                GenieConfig.TB_Buy_B_탐색주가_3 = Math.Abs(TB_Buy_B_탐색주가_3);
                GenieConfig.TB_Buy_B_탐색주가_4 = Math.Abs(TB_Buy_B_탐색주가_4);
                GenieConfig.TB_Buy_B_탐색주가_5 = Math.Abs(TB_Buy_B_탐색주가_5);
                GenieConfig.TB_Buy_B_탐색주가_6 = Math.Abs(TB_Buy_B_탐색주가_6);

                form.TB_Buy_B_탐색주가_1.Text = GenieConfig.TB_Buy_B_탐색주가_1.ToString();
                form.TB_Buy_B_탐색주가_2.Text = GenieConfig.TB_Buy_B_탐색주가_2.ToString();
                form.TB_Buy_B_탐색주가_3.Text = GenieConfig.TB_Buy_B_탐색주가_3.ToString();
                form.TB_Buy_B_탐색주가_4.Text = GenieConfig.TB_Buy_B_탐색주가_4.ToString();
                form.TB_Buy_B_탐색주가_5.Text = GenieConfig.TB_Buy_B_탐색주가_5.ToString();
                form.TB_Buy_B_탐색주가_6.Text = GenieConfig.TB_Buy_B_탐색주가_6.ToString();

                double.TryParse(form.TB_Buy_B_탐색대금_1.Text, out double Buy_B_탐색대금_1);
                double.TryParse(form.TB_Buy_B_탐색대금_2.Text, out double Buy_B_탐색대금_2);
                double.TryParse(form.TB_Buy_B_탐색대금_3.Text, out double Buy_B_탐색대금_3);
                double.TryParse(form.TB_Buy_B_탐색대금_4.Text, out double Buy_B_탐색대금_4);
                double.TryParse(form.TB_Buy_B_탐색대금_5.Text, out double Buy_B_탐색대금_5);
                double.TryParse(form.TB_Buy_B_탐색대금_6.Text, out double Buy_B_탐색대금_6);

                if (Buy_B_탐색대금_1 == 0) Buy_B_탐색대금_1 = 1000;
                if (Buy_B_탐색대금_2 == 0) Buy_B_탐색대금_2 = 5000;
                if (Buy_B_탐색대금_3 == 0) Buy_B_탐색대금_3 = 10000;
                if (Buy_B_탐색대금_4 == 0) Buy_B_탐색대금_4 = 15000;
                if (Buy_B_탐색대금_5 == 0) Buy_B_탐색대금_5 = 20000;
                if (Buy_B_탐색대금_6 == 0) Buy_B_탐색대금_6 = 25000;

                GenieConfig.TB_Buy_B_탐색대금_1 = Math.Abs(Buy_B_탐색대금_1);
                GenieConfig.TB_Buy_B_탐색대금_2 = Math.Abs(Buy_B_탐색대금_2);
                GenieConfig.TB_Buy_B_탐색대금_3 = Math.Abs(Buy_B_탐색대금_3);
                GenieConfig.TB_Buy_B_탐색대금_4 = Math.Abs(Buy_B_탐색대금_4);
                GenieConfig.TB_Buy_B_탐색대금_5 = Math.Abs(Buy_B_탐색대금_5);
                GenieConfig.TB_Buy_B_탐색대금_6 = Math.Abs(Buy_B_탐색대금_6);

                form.TB_Buy_B_탐색대금_1.Text = GenieConfig.TB_Buy_B_탐색대금_1.ToString();
                form.TB_Buy_B_탐색대금_2.Text = GenieConfig.TB_Buy_B_탐색대금_2.ToString();
                form.TB_Buy_B_탐색대금_3.Text = GenieConfig.TB_Buy_B_탐색대금_3.ToString();
                form.TB_Buy_B_탐색대금_4.Text = GenieConfig.TB_Buy_B_탐색대금_4.ToString();
                form.TB_Buy_B_탐색대금_5.Text = GenieConfig.TB_Buy_B_탐색대금_5.ToString();
                form.TB_Buy_B_탐색대금_6.Text = GenieConfig.TB_Buy_B_탐색대금_6.ToString();

                GenieConfig.CBB_M_잔량_2 = GET.ComboBoxIndex(form.CBB_M_잔량_2);
                GenieConfig.CBB_Buy_B_분봉 = GET.ComboBoxIndex(form.CBB_Buy_B_분봉);
                GenieConfig.CBB_Buy_B_일봉 = GET.ComboBoxIndex(form.CBB_Buy_B_일봉);
            }
            catch (Exception e)
            {
                 Form1.Console_print("대금탐색 / 매수 대금탐색_B 입력 오류 : " + e.Message); Log.에러기록("대금탐색 / 매수 대금탐색_B 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_Sell_기준초.Text, out int Sell_기준초);
                double.TryParse(form.TB_Sell_탐색rate.Text, out double Sell_탐색rate);

                if (Sell_기준초 == 0) Sell_기준초 = 1;

                GenieConfig.TB_Sell_기준초 = Math.Abs(Sell_기준초);
                GenieConfig.TB_Sell_탐색rate = Sell_탐색rate;

                form.TB_Sell_기준초.Text = GenieConfig.TB_Sell_기준초.ToString();
                form.TB_Sell_탐색rate.Text = Sell_탐색rate.ToString();

                int.TryParse(form.TB_Sell_탐색주가_1.Text.Replace(",", ""), out int TB_Sell_탐색주가_1);
                int.TryParse(form.TB_Sell_탐색주가_2.Text.Replace(",", ""), out int TB_Sell_탐색주가_2);
                int.TryParse(form.TB_Sell_탐색주가_3.Text.Replace(",", ""), out int TB_Sell_탐색주가_3);
                int.TryParse(form.TB_Sell_탐색주가_4.Text.Replace(",", ""), out int TB_Sell_탐색주가_4);
                int.TryParse(form.TB_Sell_탐색주가_5.Text.Replace(",", ""), out int TB_Sell_탐색주가_5);
                int.TryParse(form.TB_Sell_탐색주가_6.Text.Replace(",", ""), out int TB_Sell_탐색주가_6);

                if (TB_Sell_탐색주가_1 == 0) TB_Sell_탐색주가_1 = 1000;
                if (TB_Sell_탐색주가_2 == 0) TB_Sell_탐색주가_2 = 5000;
                if (TB_Sell_탐색주가_3 == 0) TB_Sell_탐색주가_3 = 10000;
                if (TB_Sell_탐색주가_4 == 0) TB_Sell_탐색주가_4 = 15000;
                if (TB_Sell_탐색주가_5 == 0) TB_Sell_탐색주가_5 = 20000;
                if (TB_Sell_탐색주가_6 == 0) TB_Sell_탐색주가_6 = 25000;

                GenieConfig.TB_Sell_탐색주가_1 = Math.Abs(TB_Sell_탐색주가_1);
                GenieConfig.TB_Sell_탐색주가_2 = Math.Abs(TB_Sell_탐색주가_2);
                GenieConfig.TB_Sell_탐색주가_3 = Math.Abs(TB_Sell_탐색주가_3);
                GenieConfig.TB_Sell_탐색주가_4 = Math.Abs(TB_Sell_탐색주가_4);
                GenieConfig.TB_Sell_탐색주가_5 = Math.Abs(TB_Sell_탐색주가_5);
                GenieConfig.TB_Sell_탐색주가_6 = Math.Abs(TB_Sell_탐색주가_6);

                form.TB_Sell_탐색주가_1.Text = GenieConfig.TB_Sell_탐색주가_1.ToString();
                form.TB_Sell_탐색주가_2.Text = GenieConfig.TB_Sell_탐색주가_2.ToString();
                form.TB_Sell_탐색주가_3.Text = GenieConfig.TB_Sell_탐색주가_3.ToString();
                form.TB_Sell_탐색주가_4.Text = GenieConfig.TB_Sell_탐색주가_4.ToString();
                form.TB_Sell_탐색주가_5.Text = GenieConfig.TB_Sell_탐색주가_5.ToString();
                form.TB_Sell_탐색주가_6.Text = GenieConfig.TB_Sell_탐색주가_6.ToString();

                double.TryParse(form.TB_Sell_탐색대금_1.Text, out double Sell_탐색대금_1);
                double.TryParse(form.TB_Sell_탐색대금_2.Text, out double Sell_탐색대금_2);
                double.TryParse(form.TB_Sell_탐색대금_3.Text, out double Sell_탐색대금_3);
                double.TryParse(form.TB_Sell_탐색대금_4.Text, out double Sell_탐색대금_4);
                double.TryParse(form.TB_Sell_탐색대금_5.Text, out double Sell_탐색대금_5);
                double.TryParse(form.TB_Sell_탐색대금_6.Text, out double Sell_탐색대금_6);

                if (Sell_탐색대금_1 == 0) Sell_탐색대금_1 = 1000;
                if (Sell_탐색대금_2 == 0) Sell_탐색대금_2 = 5000;
                if (Sell_탐색대금_3 == 0) Sell_탐색대금_3 = 10000;
                if (Sell_탐색대금_4 == 0) Sell_탐색대금_4 = 15000;
                if (Sell_탐색대금_5 == 0) Sell_탐색대금_5 = 20000;
                if (Sell_탐색대금_6 == 0) Sell_탐색대금_6 = 25000;

                GenieConfig.TB_Sell_탐색대금_1 = Math.Abs(Sell_탐색대금_1);
                GenieConfig.TB_Sell_탐색대금_2 = Math.Abs(Sell_탐색대금_2);
                GenieConfig.TB_Sell_탐색대금_3 = Math.Abs(Sell_탐색대금_3);
                GenieConfig.TB_Sell_탐색대금_4 = Math.Abs(Sell_탐색대금_4);
                GenieConfig.TB_Sell_탐색대금_5 = Math.Abs(Sell_탐색대금_5);
                GenieConfig.TB_Sell_탐색대금_6 = Math.Abs(Sell_탐색대금_6);

                form.TB_Sell_탐색대금_1.Text = GenieConfig.TB_Sell_탐색대금_1.ToString();
                form.TB_Sell_탐색대금_2.Text = GenieConfig.TB_Sell_탐색대금_2.ToString();
                form.TB_Sell_탐색대금_3.Text = GenieConfig.TB_Sell_탐색대금_3.ToString();
                form.TB_Sell_탐색대금_4.Text = GenieConfig.TB_Sell_탐색대금_4.ToString();
                form.TB_Sell_탐색대금_5.Text = GenieConfig.TB_Sell_탐색대금_5.ToString();
                form.TB_Sell_탐색대금_6.Text = GenieConfig.TB_Sell_탐색대금_6.ToString();

                GenieConfig.CBB_Sell_탐색_분봉 = GET.ComboBoxIndex(form.CBB_Sell_탐색_분봉);
                GenieConfig.CBB_Sell_탐색_일봉 = GET.ComboBoxIndex(form.CBB_Sell_탐색_일봉);
            }
            catch (Exception e)
            {
                 Form1.Console_print("대금탐색 / 매도 대금탐색_B 입력 오류 : " + e.Message); Log.에러기록("대금탐색 / 매도 대금탐색_B 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_Buy_상승카운터_A.Text, out int TB_Buy_상승카운터_A);
                int.TryParse(form.TB_Buy_상승카운터_B.Text, out int TB_Buy_상승카운터_B);
                int.TryParse(form.TB_Buy_하락카운터_A.Text, out int TB_Buy_하락카운터_A);
                int.TryParse(form.TB_Buy_하락카운터_B.Text, out int TB_Buy_하락카운터_B);
                int.TryParse(form.TB_Sell_상승카운터.Text, out int TB_Sell_상승카운터);
                int.TryParse(form.TB_Sell_하락카운터.Text, out int TB_Sell_하락카운터);

                GenieConfig.TB_Buy_상승카운터_A = Math.Abs(TB_Buy_상승카운터_A);
                GenieConfig.TB_Buy_상승카운터_B = Math.Abs(TB_Buy_상승카운터_B);
                GenieConfig.TB_Buy_하락카운터_A = Math.Abs(TB_Buy_하락카운터_A);
                GenieConfig.TB_Buy_하락카운터_B = Math.Abs(TB_Buy_하락카운터_B);
                GenieConfig.TB_Sell_상승카운터 = Math.Abs(TB_Sell_상승카운터);
                GenieConfig.TB_Sell_하락카운터 = Math.Abs(TB_Sell_하락카운터);

                form.TB_Buy_상승카운터_A.Text = GenieConfig.TB_Buy_상승카운터_A.ToString();
                form.TB_Buy_상승카운터_B.Text = GenieConfig.TB_Buy_상승카운터_B.ToString();
                form.TB_Buy_하락카운터_A.Text = GenieConfig.TB_Buy_하락카운터_A.ToString();
                form.TB_Buy_하락카운터_B.Text = GenieConfig.TB_Buy_하락카운터_B.ToString();
                form.TB_Sell_상승카운터.Text = GenieConfig.TB_Sell_상승카운터.ToString();
                form.TB_Sell_하락카운터.Text = GenieConfig.TB_Sell_하락카운터.ToString();
            }
            catch (Exception e)
            {
                 Form1.Console_print("대금탐색 입력 오류 : " + e.Message); Log.에러기록("대금탐색 입력 오류 : " + e.Message);
            }

            GenieConfig.Combo_Buy_A_초회 = GET.ComboBoxIndex(form.Combo_Buy_A_초회);
            GenieConfig.Combo_Buy_B_초회 = GET.ComboBoxIndex(form.Combo_Buy_B_초회);
            GenieConfig.combo_Sell_초회 = GET.ComboBoxIndex(form.combo_Sell_초회);

            GenieConfig.CB_Buy_상승옵션_A = form.CB_Buy_상승옵션_A.Checked;
            GenieConfig.CB_Buy_상승옵션_B = form.CB_Buy_상승옵션_B.Checked;
            GenieConfig.CB_Buy_하락옵션_A = form.CB_Buy_하락옵션_A.Checked;
            GenieConfig.CB_Buy_하락옵션_B = form.CB_Buy_하락옵션_B.Checked;
            GenieConfig.CB_Sell_상승옵션 = form.CB_Sell_상승옵션.Checked;
            GenieConfig.CB_Sell_하락옵션 = form.CB_Sell_하락옵션.Checked;

        }

        private void Form_PriceSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                Form1.form1.CB_대금탐색.Checked = false;
            }
        }

        private void CB_레이아웃고정_대금탐색_CheckedChanged(object sender, EventArgs e)
        {
            GenieConfig.CB_레이아웃고정_대금탐색 = CB_레이아웃고정_대금탐색.Checked;

            if (!CB_레이아웃고정_대금탐색.Checked) LayoutChange.CBB_layout_SelectedIndex(-1);
            else LayoutChange.CBB_layout_SelectedIndex(GenieConfig.CBB_layout);
        }

        private void BT_대금탐색저장_Click(object sender, EventArgs e)
        {
            Form1.form1.Select();
            Form1.MBC_sender = (sender as Button).Name;
            Form1.중요메세지("대금탐색", "대금탐색 설정 을 저장 하시겠습니까?");
        }

        private void CB_매수탐색A_CheckedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            // 1. 철벽 방어막: 체크박스가 아니거나 텍스트가 비어있으면 즉시 탈출 (Substring 뻗음 방지)
            if (!(sender is CheckBox cb) || string.IsNullOrEmpty(cb.Text)) return;

            // 2. 알림창 로직 분리: 체크되었고 폼이 열려있을 때만 알림 발생
            if (cb.Checked && Form1.FormPriceSearch_Open)
            {
                Form1.AutoClosingAlram("대금탐색은 종목이 많거나 복수로 사용하면 누락이 발생할수 있습니다.", "사용주의", 30, "동작");
            }

            // 3. UI 렌더링 최적화: 글자가 1글자 이하라면 빈칸 처리하여 뻗음 방지, 다를 때만 덮어씀
            string remainText = cb.Text.Length > 1 ? cb.Text[1..] : "";
            string targetText = (cb.Checked ? "■" : "□") + remainText;

            if (cb.Text != targetText)
            {
                cb.Text = targetText;
            }
        }

        private void CB_상승옵션_CheckedChanged(object sender, EventArgs e)
        {
            // 1. 철벽 방어막 추가
            if (!(sender is CheckBox cb)) return;

            // 2. 삼항 연산자로 목표 텍스트 0.001초 만에 결정
            string targetText = cb.Checked ? "틱 '이상' 상승 일때" : "틱 '이하' 상승 일때";

            // 3. 현재 글자와 다를 때만 화면을 다시 그림
            if (cb.Text != targetText)
            {
                cb.Text = targetText;
            }
        }

        private void CB_하락옵션_CheckedChanged(object sender, EventArgs e)
        {
            // 1. 철벽 방어막 추가
            if (!(sender is CheckBox cb)) return;

            // 2. 삼항 연산자로 목표 텍스트 0.001초 만에 결정
            string targetText = cb.Checked ? "틱 '이상' 하락 일때" : "틱 '이하' 하락 일때";

            // 3. 현재 글자와 다를 때만 화면을 다시 그림
            if (cb.Text != targetText)
            {
                cb.Text = targetText;
            }
        }

        private void 버튼음_DropDownClosed(object sender, EventArgs e)
        {
            if (Form1.FormPriceSearch_Open) Form1.비프음("체크");
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

        private void TextBox_양수소수자리제한(object sender, EventArgs e) //textbox 의 색표시  사용
        {
            TextValue.TextBox_양수소수자리제한(sender);
        }

        private void 숫자콤마넣기_TextChanged(object sender, EventArgs e)
        {
            TextValue.숫자콤마넣기_TextChanged(sender);
        }

        private void 양수음수소수_키프레스_(object sender, KeyPressEventArgs e) // 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, true); // textbox 에 양수, 음수 , 소수  숫자만 입력 받을수 있음 
        }

        private void 양수소수_키프레스_(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, false); // textbox 에 양수 , 소수 숫자만 입력 받을수 있음
        }

        private void TextBox_양실수만(object sender, EventArgs e)
        {
            TextValue.TextBox_양실수만(sender);
        }

        private void 양수실수_키프레스_(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, false); // textbox 에 양수 , 실수 숫자만 입력 받을수 있음
        }


    }
}
