using System;
using System.Windows.Forms;

namespace 지니_64
{
    public partial class Form_PriceSearch : Form
    {
        public static Form_PriceSearch form;
        public Form_PriceSearch()
        {
            form = this;
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void Form_PriceSearch_Load()
        {
            Form1.음소거 = true;

            TB_Buy_A_탐색주가_1.Text = Properties.Settings.Default.TB_Buy_A_탐색주가_1.ToString("N0");
            TB_Buy_A_탐색주가_2.Text = Properties.Settings.Default.TB_Buy_A_탐색주가_2.ToString("N0");
            TB_Buy_A_탐색주가_3.Text = Properties.Settings.Default.TB_Buy_A_탐색주가_3.ToString("N0");
            TB_Buy_A_탐색주가_4.Text = Properties.Settings.Default.TB_Buy_A_탐색주가_4.ToString("N0");
            TB_Buy_A_탐색주가_5.Text = Properties.Settings.Default.TB_Buy_A_탐색주가_5.ToString("N0");
            TB_Buy_A_탐색주가_6.Text = Properties.Settings.Default.TB_Buy_A_탐색주가_6.ToString("N0");
            TB_Buy_B_탐색주가_1.Text = Properties.Settings.Default.TB_Buy_B_탐색주가_1.ToString("N0");
            TB_Buy_B_탐색주가_2.Text = Properties.Settings.Default.TB_Buy_B_탐색주가_2.ToString("N0");
            TB_Buy_B_탐색주가_3.Text = Properties.Settings.Default.TB_Buy_B_탐색주가_3.ToString("N0");
            TB_Buy_B_탐색주가_4.Text = Properties.Settings.Default.TB_Buy_B_탐색주가_4.ToString("N0");
            TB_Buy_B_탐색주가_5.Text = Properties.Settings.Default.TB_Buy_B_탐색주가_5.ToString("N0");
            TB_Buy_B_탐색주가_6.Text = Properties.Settings.Default.TB_Buy_B_탐색주가_6.ToString("N0");

            TB_Buy_A_탐색대금_1.Text = Properties.Settings.Default.TB_Buy_A_탐색대금_1.ToString();
            TB_Buy_A_탐색대금_2.Text = Properties.Settings.Default.TB_Buy_A_탐색대금_2.ToString();
            TB_Buy_A_탐색대금_3.Text = Properties.Settings.Default.TB_Buy_A_탐색대금_3.ToString();
            TB_Buy_A_탐색대금_4.Text = Properties.Settings.Default.TB_Buy_A_탐색대금_4.ToString();
            TB_Buy_A_탐색대금_5.Text = Properties.Settings.Default.TB_Buy_A_탐색대금_5.ToString();
            TB_Buy_A_탐색대금_6.Text = Properties.Settings.Default.TB_Buy_A_탐색대금_6.ToString();
            TB_Buy_B_탐색대금_1.Text = Properties.Settings.Default.TB_Buy_B_탐색대금_1.ToString();
            TB_Buy_B_탐색대금_2.Text = Properties.Settings.Default.TB_Buy_B_탐색대금_2.ToString();
            TB_Buy_B_탐색대금_3.Text = Properties.Settings.Default.TB_Buy_B_탐색대금_3.ToString();
            TB_Buy_B_탐색대금_4.Text = Properties.Settings.Default.TB_Buy_B_탐색대금_4.ToString();
            TB_Buy_B_탐색대금_5.Text = Properties.Settings.Default.TB_Buy_B_탐색대금_5.ToString();
            TB_Buy_B_탐색대금_6.Text = Properties.Settings.Default.TB_Buy_B_탐색대금_6.ToString();
            TB_Buy_A_탐색rate.Text = Properties.Settings.Default.TB_Buy_A_탐색rate.ToString();
            TB_Buy_B_탐색rate.Text = Properties.Settings.Default.TB_Buy_B_탐색rate.ToString();

            MTB_M_반복.Text = Properties.Settings.Default.MTB_M_반복.ToString();
            CBB_M_잔량.SelectedIndex = Properties.Settings.Default.CBB_M_잔량;
            TB_M_매도호가별대금.Text = Properties.Settings.Default.TB_M_매도호가별대금.ToString();
            TB_M_매수호가별대금.Text = Properties.Settings.Default.TB_M_매수호가별대금.ToString();
            TB_M_매도호가합대금.Text = Properties.Settings.Default.TB_M_매도호가합대금.ToString();
            TB_M_매수호가합대금.Text = Properties.Settings.Default.TB_M_매수호가합대금.ToString();

            MTB_M_반복_2.Text = Properties.Settings.Default.MTB_M_반복_2.ToString();
            CBB_M_잔량_2.SelectedIndex = Properties.Settings.Default.CBB_M_잔량_2;
            TB_M_매도호가별대금_2.Text = Properties.Settings.Default.TB_M_매도호가별대금_2.ToString();
            TB_M_매수호가별대금_2.Text = Properties.Settings.Default.TB_M_매수호가별대금_2.ToString();
            TB_M_매도호가합대금_2.Text = Properties.Settings.Default.TB_M_매도호가합대금_2.ToString();
            TB_M_매수호가합대금_2.Text = Properties.Settings.Default.TB_M_매수호가합대금_2.ToString();

            CBB_Buy_A_분봉.SelectedIndex = Properties.Settings.Default.CBB_Buy_A_분봉;
            CBB_Buy_B_분봉.SelectedIndex = Properties.Settings.Default.CBB_Buy_B_분봉;
            CBB_Sell_탐색_분봉.SelectedIndex = Properties.Settings.Default.CBB_Sell_탐색_분봉;

            CBB_Buy_A_일봉.SelectedIndex = Properties.Settings.Default.CBB_Buy_A_일봉;
            CBB_Buy_B_일봉.SelectedIndex = Properties.Settings.Default.CBB_Buy_B_일봉;
            CBB_Sell_탐색_일봉.SelectedIndex = Properties.Settings.Default.CBB_Sell_탐색_일봉;

            TB_accumulate_Price.Text = Properties.Settings.Default.TB_accumulate_Price.ToString();

            TB_Sell_탐색주가_1.Text = Properties.Settings.Default.TB_Sell_탐색주가_1.ToString("N0");
            TB_Sell_탐색주가_3.Text = Properties.Settings.Default.TB_Sell_탐색주가_2.ToString("N0");
            TB_Sell_탐색주가_5.Text = Properties.Settings.Default.TB_Sell_탐색주가_3.ToString("N0");
            TB_Sell_탐색주가_2.Text = Properties.Settings.Default.TB_Sell_탐색주가_4.ToString("N0");
            TB_Sell_탐색주가_4.Text = Properties.Settings.Default.TB_Sell_탐색주가_5.ToString("N0");
            TB_Sell_탐색주가_6.Text = Properties.Settings.Default.TB_Sell_탐색주가_6.ToString("N0");
            TB_Buy_A_기준초.Text = Properties.Settings.Default.TB_Buy_A_기준초.ToString();
            TB_Buy_B_기준초.Text = Properties.Settings.Default.TB_Buy_B_기준초.ToString();
            TB_Sell_기준초.Text = Properties.Settings.Default.TB_Sell_기준초.ToString();

            TB_Sell_탐색대금_1.Text = Properties.Settings.Default.TB_Sell_탐색대금_1.ToString();
            TB_Sell_탐색대금_3.Text = Properties.Settings.Default.TB_Sell_탐색대금_2.ToString();
            TB_Sell_탐색대금_5.Text = Properties.Settings.Default.TB_Sell_탐색대금_3.ToString();
            TB_Sell_탐색대금_2.Text = Properties.Settings.Default.TB_Sell_탐색대금_4.ToString();
            TB_Sell_탐색대금_4.Text = Properties.Settings.Default.TB_Sell_탐색대금_5.ToString();
            TB_Sell_탐색대금_6.Text = Properties.Settings.Default.TB_Sell_탐색대금_6.ToString();
            TB_Sell_탐색rate.Text = Properties.Settings.Default.TB_Sell_탐색rate.ToString();

            TB_매수탐색A.Text = Properties.Settings.Default.TB_매수탐색A;
            TB_매수탐색B.Text = Properties.Settings.Default.TB_매수탐색B;
            TB_매도탐색.Text = Properties.Settings.Default.TB_매도탐색;

            CB_매수탐색A.Checked = Properties.Settings.Default.CB_매수탐색A;
            CB_매수탐색B.Checked = Properties.Settings.Default.CB_매수탐색B;
            CB_매도탐색.Checked = Properties.Settings.Default.CB_매도탐색;

            TB_Buy_상승카운터_A.Text = Properties.Settings.Default.TB_Buy_상승카운터_A.ToString();
            TB_Buy_상승카운터_B.Text = Properties.Settings.Default.TB_Buy_상승카운터_B.ToString();
            TB_Buy_하락카운터_A.Text = Properties.Settings.Default.TB_Buy_하락카운터_A.ToString();
            TB_Buy_하락카운터_B.Text = Properties.Settings.Default.TB_Buy_하락카운터_B.ToString();
            TB_Sell_상승카운터.Text = Properties.Settings.Default.TB_Sell_상승카운터.ToString();
            TB_Sell_하락카운터.Text = Properties.Settings.Default.TB_Sell_하락카운터.ToString();

            form.CB_Buy_상승옵션_A.Checked = Properties.Settings.Default.CB_Buy_상승옵션_A;
            form.CB_Buy_상승옵션_B.Checked = Properties.Settings.Default.CB_Buy_상승옵션_B;
            form.CB_Buy_하락옵션_A.Checked = Properties.Settings.Default.CB_Buy_하락옵션_A;
            form.CB_Buy_하락옵션_B.Checked = Properties.Settings.Default.CB_Buy_하락옵션_B;
            form.CB_Sell_상승옵션.Checked = Properties.Settings.Default.CB_Sell_상승옵션;
            form.CB_Sell_하락옵션.Checked = Properties.Settings.Default.CB_Sell_하락옵션;

            this.ActiveControl = CBB_Buy_A_일봉;
            Form1.음소거 = Properties.Settings.Default.CB_음소거;

            if (Properties.Settings.Default.CB_가이드매매) ControllerDisable.Form_PriceSearch_Disable();

        }
        public static void 대금탐색_저장()
        {
            try
            {
                if (form.TB_매수탐색A.Text.Length == 0) form.TB_매수탐색A.Text = "매수대금탐색A";
                if (form.TB_매수탐색B.Text.Length == 0) form.TB_매수탐색B.Text = "매수대금탐색B";
                if (form.TB_매도탐색.Text.Length == 0) form.TB_매도탐색.Text = "매도대금 탐색";

                Properties.Settings.Default.TB_매수탐색A = form.TB_매수탐색A.Text;
                Properties.Settings.Default.TB_매수탐색B = form.TB_매수탐색B.Text;
                Properties.Settings.Default.TB_매도탐색 = form.TB_매도탐색.Text;
            }
            catch (Exception e)
            {
                Console.WriteLine("대금탐색 / 대금탐색 탐색이름 오류 : " + e.Message); Form1.Error_Log("대금탐색 / 대금탐색 탐색이름 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_accumulate_Price.Text, out int accumulate_Price);
                if (accumulate_Price == 0) accumulate_Price = 1000;
                Properties.Settings.Default.TB_accumulate_Price = Math.Abs(accumulate_Price);
                form.TB_accumulate_Price.Text = Properties.Settings.Default.TB_accumulate_Price.ToString();

                int.TryParse(form.TB_Buy_A_기준초.Text, out int Buy_A_기준초);
                double.TryParse(form.TB_Buy_A_탐색rate.Text, out double Buy_A_탐색rate);
                int.TryParse(form.MTB_M_반복.Text, out int 반복);

                if (Buy_A_기준초 == 0) Buy_A_기준초 = 1;
                if (반복 < 2) 반복 = 5;

                Properties.Settings.Default.TB_Buy_A_기준초 = Math.Abs(Buy_A_기준초);
                Properties.Settings.Default.TB_Buy_A_탐색rate = Buy_A_탐색rate;
                Properties.Settings.Default.MTB_M_반복 = 반복;

                form.TB_Buy_A_기준초.Text = Properties.Settings.Default.TB_Buy_A_기준초.ToString();
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

                Properties.Settings.Default.TB_M_매도호가별대금 = Math.Abs(매도호가별대금);
                Properties.Settings.Default.TB_M_매수호가별대금 = Math.Abs(매수호가별대금);
                Properties.Settings.Default.TB_M_매도호가합대금 = Math.Abs(매도호가합대금);
                Properties.Settings.Default.TB_M_매수호가합대금 = Math.Abs(매수호가합대금);

                form.TB_M_매도호가별대금.Text = Properties.Settings.Default.TB_M_매도호가별대금.ToString();
                form.TB_M_매수호가별대금.Text = Properties.Settings.Default.TB_M_매수호가별대금.ToString();
                form.TB_M_매도호가합대금.Text = Properties.Settings.Default.TB_M_매도호가합대금.ToString();
                form.TB_M_매수호가합대금.Text = Properties.Settings.Default.TB_M_매수호가합대금.ToString();

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

                Properties.Settings.Default.TB_Buy_A_탐색주가_1 = Math.Abs(TB_Buy_A_탐색주가_1);
                Properties.Settings.Default.TB_Buy_A_탐색주가_2 = Math.Abs(TB_Buy_A_탐색주가_2);
                Properties.Settings.Default.TB_Buy_A_탐색주가_3 = Math.Abs(TB_Buy_A_탐색주가_3);
                Properties.Settings.Default.TB_Buy_A_탐색주가_4 = Math.Abs(TB_Buy_A_탐색주가_4);
                Properties.Settings.Default.TB_Buy_A_탐색주가_5 = Math.Abs(TB_Buy_A_탐색주가_5);
                Properties.Settings.Default.TB_Buy_A_탐색주가_6 = Math.Abs(TB_Buy_A_탐색주가_6);

                form.TB_Buy_A_탐색주가_1.Text = Properties.Settings.Default.TB_Buy_A_탐색주가_1.ToString();
                form.TB_Buy_A_탐색주가_2.Text = Properties.Settings.Default.TB_Buy_A_탐색주가_2.ToString();
                form.TB_Buy_A_탐색주가_3.Text = Properties.Settings.Default.TB_Buy_A_탐색주가_3.ToString();
                form.TB_Buy_A_탐색주가_4.Text = Properties.Settings.Default.TB_Buy_A_탐색주가_4.ToString();
                form.TB_Buy_A_탐색주가_5.Text = Properties.Settings.Default.TB_Buy_A_탐색주가_5.ToString();
                form.TB_Buy_A_탐색주가_6.Text = Properties.Settings.Default.TB_Buy_A_탐색주가_6.ToString();


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

                Properties.Settings.Default.TB_Buy_A_탐색대금_1 = Math.Abs(Buy_A_탐색대금_1);
                Properties.Settings.Default.TB_Buy_A_탐색대금_2 = Math.Abs(Buy_A_탐색대금_2);
                Properties.Settings.Default.TB_Buy_A_탐색대금_3 = Math.Abs(Buy_A_탐색대금_3);
                Properties.Settings.Default.TB_Buy_A_탐색대금_4 = Math.Abs(Buy_A_탐색대금_4);
                Properties.Settings.Default.TB_Buy_A_탐색대금_5 = Math.Abs(Buy_A_탐색대금_5);
                Properties.Settings.Default.TB_Buy_A_탐색대금_6 = Math.Abs(Buy_A_탐색대금_6);

                form.TB_Buy_A_탐색대금_1.Text = Properties.Settings.Default.TB_Buy_A_탐색대금_1.ToString();
                form.TB_Buy_A_탐색대금_2.Text = Properties.Settings.Default.TB_Buy_A_탐색대금_2.ToString();
                form.TB_Buy_A_탐색대금_3.Text = Properties.Settings.Default.TB_Buy_A_탐색대금_3.ToString();
                form.TB_Buy_A_탐색대금_4.Text = Properties.Settings.Default.TB_Buy_A_탐색대금_4.ToString();
                form.TB_Buy_A_탐색대금_5.Text = Properties.Settings.Default.TB_Buy_A_탐색대금_5.ToString();
                form.TB_Buy_A_탐색대금_6.Text = Properties.Settings.Default.TB_Buy_A_탐색대금_6.ToString();

                Properties.Settings.Default.CBB_M_잔량 = form.CBB_M_잔량.SelectedIndex;
                Properties.Settings.Default.CBB_Buy_A_분봉 = form.CBB_Buy_A_분봉.SelectedIndex;
                Properties.Settings.Default.CBB_Buy_A_일봉 = form.CBB_Buy_A_일봉.SelectedIndex;
            }
            catch (Exception e)
            {
                Console.WriteLine("대금탐색 / 매수 대금탐색_A 입력 오류 : " + e.Message); Form1.Error_Log("대금탐색 / 매수 대금탐색_A 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_Buy_B_기준초.Text, out int Buy_B_기준초);
                double.TryParse(form.TB_Buy_B_탐색rate.Text, out double Buy_B_탐색rate);
                int.TryParse(form.MTB_M_반복_2.Text, out int 반복_2);

                if (Buy_B_기준초 == 0) Buy_B_기준초 = 1;
                if (반복_2 < 2) 반복_2 = 5;

                Properties.Settings.Default.TB_Buy_B_기준초 = Math.Abs(Buy_B_기준초);
                Properties.Settings.Default.TB_Buy_B_탐색rate = Buy_B_탐색rate;
                Properties.Settings.Default.MTB_M_반복_2 = 반복_2;

                form.TB_Buy_B_기준초.Text = Properties.Settings.Default.TB_Buy_B_기준초.ToString();
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

                Properties.Settings.Default.TB_M_매수호가별대금_2 = Math.Abs(매수호가별대금_2);
                Properties.Settings.Default.TB_M_매도호가별대금_2 = Math.Abs(매도호가별대금_2);
                Properties.Settings.Default.TB_M_매도호가합대금_2 = Math.Abs(매도호가합대금_2);
                Properties.Settings.Default.TB_M_매수호가합대금_2 = Math.Abs(매수호가합대금_2);

                form.TB_M_매수호가별대금_2.Text = Properties.Settings.Default.TB_M_매수호가별대금_2.ToString();
                form.TB_M_매도호가별대금_2.Text = Properties.Settings.Default.TB_M_매도호가별대금_2.ToString();
                form.TB_M_매도호가합대금_2.Text = Properties.Settings.Default.TB_M_매도호가합대금_2.ToString();
                form.TB_M_매수호가합대금_2.Text = Properties.Settings.Default.TB_M_매수호가합대금_2.ToString();

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

                Properties.Settings.Default.TB_Buy_B_탐색주가_1 = Math.Abs(TB_Buy_B_탐색주가_1);
                Properties.Settings.Default.TB_Buy_B_탐색주가_2 = Math.Abs(TB_Buy_B_탐색주가_2);
                Properties.Settings.Default.TB_Buy_B_탐색주가_3 = Math.Abs(TB_Buy_B_탐색주가_3);
                Properties.Settings.Default.TB_Buy_B_탐색주가_4 = Math.Abs(TB_Buy_B_탐색주가_4);
                Properties.Settings.Default.TB_Buy_B_탐색주가_5 = Math.Abs(TB_Buy_B_탐색주가_5);
                Properties.Settings.Default.TB_Buy_B_탐색주가_6 = Math.Abs(TB_Buy_B_탐색주가_6);

                form.TB_Buy_B_탐색주가_1.Text = Properties.Settings.Default.TB_Buy_B_탐색주가_1.ToString();
                form.TB_Buy_B_탐색주가_2.Text = Properties.Settings.Default.TB_Buy_B_탐색주가_2.ToString();
                form.TB_Buy_B_탐색주가_3.Text = Properties.Settings.Default.TB_Buy_B_탐색주가_3.ToString();
                form.TB_Buy_B_탐색주가_4.Text = Properties.Settings.Default.TB_Buy_B_탐색주가_4.ToString();
                form.TB_Buy_B_탐색주가_5.Text = Properties.Settings.Default.TB_Buy_B_탐색주가_5.ToString();
                form.TB_Buy_B_탐색주가_6.Text = Properties.Settings.Default.TB_Buy_B_탐색주가_6.ToString();

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

                Properties.Settings.Default.TB_Buy_B_탐색대금_1 = Math.Abs(Buy_B_탐색대금_1);
                Properties.Settings.Default.TB_Buy_B_탐색대금_2 = Math.Abs(Buy_B_탐색대금_2);
                Properties.Settings.Default.TB_Buy_B_탐색대금_3 = Math.Abs(Buy_B_탐색대금_3);
                Properties.Settings.Default.TB_Buy_B_탐색대금_4 = Math.Abs(Buy_B_탐색대금_4);
                Properties.Settings.Default.TB_Buy_B_탐색대금_5 = Math.Abs(Buy_B_탐색대금_5);
                Properties.Settings.Default.TB_Buy_B_탐색대금_6 = Math.Abs(Buy_B_탐색대금_6);

                form.TB_Buy_B_탐색대금_1.Text = Properties.Settings.Default.TB_Buy_B_탐색대금_1.ToString();
                form.TB_Buy_B_탐색대금_2.Text = Properties.Settings.Default.TB_Buy_B_탐색대금_2.ToString();
                form.TB_Buy_B_탐색대금_3.Text = Properties.Settings.Default.TB_Buy_B_탐색대금_3.ToString();
                form.TB_Buy_B_탐색대금_4.Text = Properties.Settings.Default.TB_Buy_B_탐색대금_4.ToString();
                form.TB_Buy_B_탐색대금_5.Text = Properties.Settings.Default.TB_Buy_B_탐색대금_5.ToString();
                form.TB_Buy_B_탐색대금_6.Text = Properties.Settings.Default.TB_Buy_B_탐색대금_6.ToString();

                Properties.Settings.Default.CBB_M_잔량_2 = form.CBB_M_잔량_2.SelectedIndex;
                Properties.Settings.Default.CBB_Buy_B_분봉 = form.CBB_Buy_B_분봉.SelectedIndex;
                Properties.Settings.Default.CBB_Buy_B_일봉 = form.CBB_Buy_B_일봉.SelectedIndex;
            }
            catch (Exception e)
            {
                Console.WriteLine("대금탐색 / 매수 대금탐색_B 입력 오류 : " + e.Message); Form1.Error_Log("대금탐색 / 매수 대금탐색_B 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_Sell_기준초.Text, out int Sell_기준초);
                double.TryParse(form.TB_Sell_탐색rate.Text, out double Sell_탐색rate);

                if (Sell_기준초 == 0) Sell_기준초 = 1;

                Properties.Settings.Default.TB_Sell_기준초 = Math.Abs(Sell_기준초);
                Properties.Settings.Default.TB_Sell_탐색rate = Sell_탐색rate;

                form.TB_Sell_기준초.Text = Properties.Settings.Default.TB_Sell_기준초.ToString();
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

                Properties.Settings.Default.TB_Sell_탐색주가_1 = Math.Abs(TB_Sell_탐색주가_1);
                Properties.Settings.Default.TB_Sell_탐색주가_2 = Math.Abs(TB_Sell_탐색주가_2);
                Properties.Settings.Default.TB_Sell_탐색주가_3 = Math.Abs(TB_Sell_탐색주가_3);
                Properties.Settings.Default.TB_Sell_탐색주가_4 = Math.Abs(TB_Sell_탐색주가_4);
                Properties.Settings.Default.TB_Sell_탐색주가_5 = Math.Abs(TB_Sell_탐색주가_5);
                Properties.Settings.Default.TB_Sell_탐색주가_6 = Math.Abs(TB_Sell_탐색주가_6);

                form.TB_Sell_탐색주가_1.Text = Properties.Settings.Default.TB_Sell_탐색주가_1.ToString();
                form.TB_Sell_탐색주가_2.Text = Properties.Settings.Default.TB_Sell_탐색주가_2.ToString();
                form.TB_Sell_탐색주가_3.Text = Properties.Settings.Default.TB_Sell_탐색주가_3.ToString();
                form.TB_Sell_탐색주가_4.Text = Properties.Settings.Default.TB_Sell_탐색주가_4.ToString();
                form.TB_Sell_탐색주가_5.Text = Properties.Settings.Default.TB_Sell_탐색주가_5.ToString();
                form.TB_Sell_탐색주가_6.Text = Properties.Settings.Default.TB_Sell_탐색주가_6.ToString();

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

                Properties.Settings.Default.TB_Sell_탐색대금_1 = Math.Abs(Sell_탐색대금_1);
                Properties.Settings.Default.TB_Sell_탐색대금_2 = Math.Abs(Sell_탐색대금_2);
                Properties.Settings.Default.TB_Sell_탐색대금_3 = Math.Abs(Sell_탐색대금_3);
                Properties.Settings.Default.TB_Sell_탐색대금_4 = Math.Abs(Sell_탐색대금_4);
                Properties.Settings.Default.TB_Sell_탐색대금_5 = Math.Abs(Sell_탐색대금_5);
                Properties.Settings.Default.TB_Sell_탐색대금_6 = Math.Abs(Sell_탐색대금_6);

                form.TB_Sell_탐색대금_1.Text = Properties.Settings.Default.TB_Sell_탐색대금_1.ToString();
                form.TB_Sell_탐색대금_2.Text = Properties.Settings.Default.TB_Sell_탐색대금_2.ToString();
                form.TB_Sell_탐색대금_3.Text = Properties.Settings.Default.TB_Sell_탐색대금_3.ToString();
                form.TB_Sell_탐색대금_4.Text = Properties.Settings.Default.TB_Sell_탐색대금_4.ToString();
                form.TB_Sell_탐색대금_5.Text = Properties.Settings.Default.TB_Sell_탐색대금_5.ToString();
                form.TB_Sell_탐색대금_6.Text = Properties.Settings.Default.TB_Sell_탐색대금_6.ToString();

                Properties.Settings.Default.CBB_Sell_탐색_분봉 = form.CBB_Sell_탐색_분봉.SelectedIndex;
                Properties.Settings.Default.CBB_Sell_탐색_일봉 = form.CBB_Sell_탐색_일봉.SelectedIndex;
            }
            catch (Exception e)
            {
                Console.WriteLine("대금탐색 / 매도 대금탐색_B 입력 오류 : " + e.Message); Form1.Error_Log("대금탐색 / 매도 대금탐색_B 입력 오류 : " + e.Message);
            }

            try
            {
                int.TryParse(form.TB_Buy_상승카운터_A.Text, out int TB_Buy_상승카운터_A);
                int.TryParse(form.TB_Buy_상승카운터_B.Text, out int TB_Buy_상승카운터_B);
                int.TryParse(form.TB_Buy_하락카운터_A.Text, out int TB_Buy_하락카운터_A);
                int.TryParse(form.TB_Buy_하락카운터_B.Text, out int TB_Buy_하락카운터_B);
                int.TryParse(form.TB_Sell_상승카운터.Text, out int TB_Sell_상승카운터);
                int.TryParse(form.TB_Sell_하락카운터.Text, out int TB_Sell_하락카운터);

                Properties.Settings.Default.TB_Buy_상승카운터_A = Math.Abs(TB_Buy_상승카운터_A);
                Properties.Settings.Default.TB_Buy_상승카운터_B = Math.Abs(TB_Buy_상승카운터_B);
                Properties.Settings.Default.TB_Buy_하락카운터_A = Math.Abs(TB_Buy_하락카운터_A);
                Properties.Settings.Default.TB_Buy_하락카운터_B = Math.Abs(TB_Buy_하락카운터_B);
                Properties.Settings.Default.TB_Sell_상승카운터 = Math.Abs(TB_Sell_상승카운터);
                Properties.Settings.Default.TB_Sell_하락카운터 = Math.Abs(TB_Sell_하락카운터);

                form.TB_Buy_상승카운터_A.Text = Properties.Settings.Default.TB_Buy_상승카운터_A.ToString();
                form.TB_Buy_상승카운터_B.Text = Properties.Settings.Default.TB_Buy_상승카운터_B.ToString();
                form.TB_Buy_하락카운터_A.Text = Properties.Settings.Default.TB_Buy_하락카운터_A.ToString();
                form.TB_Buy_하락카운터_B.Text = Properties.Settings.Default.TB_Buy_하락카운터_B.ToString();
                form.TB_Sell_상승카운터.Text = Properties.Settings.Default.TB_Sell_상승카운터.ToString();
                form.TB_Sell_하락카운터.Text = Properties.Settings.Default.TB_Sell_하락카운터.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("대금탐색 입력 오류 : " + e.Message); Form1.Error_Log("대금탐색 입력 오류 : " + e.Message);
            }

            Properties.Settings.Default.CB_Buy_상승옵션_A = form.CB_Buy_상승옵션_A.Checked;
            Properties.Settings.Default.CB_Buy_상승옵션_B = form.CB_Buy_상승옵션_B.Checked;
            Properties.Settings.Default.CB_Buy_하락옵션_A = form.CB_Buy_하락옵션_A.Checked;
            Properties.Settings.Default.CB_Buy_하락옵션_B = form.CB_Buy_하락옵션_B.Checked;
            Properties.Settings.Default.CB_Sell_상승옵션 = form.CB_Sell_상승옵션.Checked;
            Properties.Settings.Default.CB_Sell_하락옵션 = form.CB_Sell_하락옵션.Checked;

        }

        private void Form_PriceSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Properties.Settings.Default.CB_레이아웃고정_대금탐색 = CB_레이아웃고정_대금탐색.Checked;
                Properties.Settings.Default.CB_대금탐색_시작위치저장 = form.CB_대금탐색_시작위치저장.Checked;
                if (Properties.Settings.Default.CB_대금탐색_시작위치저장) Properties.Settings.Default.WindowLocation = form.Location; LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
                LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
                Form1.form1.CB_대금탐색.Checked = false;
                Form1.FormPriceSearch_Open = false;

                e.Cancel = true;
                Hide();
            }
        }

        private void CB_레이아웃고정_대금탐색_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CB_레이아웃고정_대금탐색 = CB_레이아웃고정_대금탐색.Checked;

            if (!CB_레이아웃고정_대금탐색.Checked) LayoutChange.CBB_layout_SelectedIndex(-1);
            else LayoutChange.CBB_layout_SelectedIndex(Form1.form1.CBB_layout.SelectedIndex);
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
            CheckBox CB = (sender as CheckBox);
            string text = CB.Text.Substring(1);

            if (CB.Checked)
            {
                if (Form1.FormPriceSearch_Open) Form1.AutoClosingAlram("대금탐색은 종목이 많거나 복수로 사용하면 누락이 발생할수 있습니다.", "사용주의", 30, "동작");
                CB.Text = "■" + text;
            }
            else
            {
                CB.Text = "□" + text;
            }

            if (Form1.FormPriceSearch_Open)
            {
                Properties.Settings.Default.CB_매수탐색A = CB_매수탐색A.Checked;
                Properties.Settings.Default.CB_매수탐색B = CB_매수탐색B.Checked;
                Properties.Settings.Default.CB_매도탐색 = CB_매도탐색.Checked;

                if (!Properties.Settings.Default.CB_매수탐색A) Condition_Management.대금탐색취소(Properties.Settings.Default.TB_매수탐색A);
                if (!Properties.Settings.Default.CB_매수탐색B) Condition_Management.대금탐색취소(Properties.Settings.Default.TB_매수탐색B);
                if (!Properties.Settings.Default.CB_매도탐색) Condition_Management.대금탐색취소(Properties.Settings.Default.TB_매도탐색);
            }
        }

        private void CB_상승옵션_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);
            if (CB.Checked)
            {
                CB.Text = "틱 '이상' 상승 일때";
            }
            else
            {
                CB.Text = "틱 '이하' 상승 일때";
            }
        }


        private void CB_하락옵션_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CB = (sender as CheckBox);
            if (CB.Checked)
            {
                CB.Text = "틱 '이상' 하락 일때";
            }
            else
            {
                CB.Text = "틱 '이하' 하락 일때";
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

        private void _양수음수소수_키프레스(object sender, KeyPressEventArgs e) // 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, true); // textbox 에 양수, 음수 , 소수  숫자만 입력 받을수 있음 
        }

        private void _양수소수_키프레스(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, true, false); // textbox 에 양수 , 소수 숫자만 입력 받을수 있음
        }

        private void TextBox_양실수만(object sender, EventArgs e)
        {
            TextValue.TextBox_양실수만(sender);
        }

        private void _양수실수_키프레스(object sender, KeyPressEventArgs e)// 사용
        {
            TextValue.TypingOnlyNumber(sender, e, false, false); // textbox 에 양수 , 실수 숫자만 입력 받을수 있음
        }


    }
}
