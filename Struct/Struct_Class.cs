namespace 지니_64
{
    public class NXT
    {
        public string code { get; set; }

        public NXT(string code)
        {
            this.code = code;
        }
    }

    public class AVG_price
    {
        public string code { get; set; }
        public double endprice { get; set; }
        public string date { get; set; }

        public AVG_price(string code, double endprice, string date)
        {
            this.code = code;
            this.endprice = endprice;
            this.date = date;
        }
    }

    public class AVG_jisu
    {
        public bool new_stop { get; set; }
        public bool add_stop { get; set; }
        public bool use_min_03 { get; set; }
        public bool use_min_05 { get; set; }
        public bool use_min_10 { get; set; }
        public bool use_min_20 { get; set; }
        public bool use_min_30 { get; set; }
        public bool use_min_60 { get; set; }
        public bool use_day_03 { get; set; }
        public bool use_day_05 { get; set; }
        public bool use_day_10 { get; set; }
        public bool use_day_20 { get; set; }
        public bool use_day_40 { get; set; }
        public bool use_day_60 { get; set; }
        public bool UD_min_03 { get; set; }
        public bool UD_min_05 { get; set; }
        public bool UD_min_10 { get; set; }
        public bool UD_min_20 { get; set; }
        public bool UD_min_30 { get; set; }
        public bool UD_min_60 { get; set; }
        public bool UD_day_03 { get; set; }
        public bool UD_day_05 { get; set; }
        public bool UD_day_10 { get; set; }
        public bool UD_day_20 { get; set; }
        public bool UD_day_40 { get; set; }
        public bool UD_day_60 { get; set; }
        public bool day_03 { get; set; }
        public bool day_05 { get; set; }
        public bool day_10 { get; set; }
        public bool day_20 { get; set; }
        public bool day_40 { get; set; }
        public bool day_60 { get; set; }
        public bool min_03 { get; set; }
        public bool min_05 { get; set; }
        public bool min_10 { get; set; }
        public bool min_20 { get; set; }
        public bool min_30 { get; set; }
        public bool min_60 { get; set; }
        public bool check_day_03 { get; set; }
        public bool check_day_05 { get; set; }
        public bool check_day_10 { get; set; }
        public bool check_day_20 { get; set; }
        public bool check_day_40 { get; set; }
        public bool check_day_60 { get; set; }
        public bool check_min_03 { get; set; }
        public bool check_min_05 { get; set; }
        public bool check_min_10 { get; set; }
        public bool check_min_20 { get; set; }
        public bool check_min_30 { get; set; }
        public bool check_min_60 { get; set; }

        public AVG_jisu(bool new_stop, bool add_stop,
                        bool use_min_03, bool use_min_05, bool use_min_10, bool use_min_20, bool use_min_30, bool use_min_60,
                        bool use_day_03, bool use_day_05, bool use_day_10, bool use_day_20, bool use_day_40, bool use_day_60,
                        bool UD_min_03, bool UD_min_05, bool UD_min_10, bool UD_min_20, bool UD_min_30, bool UD_min_60,
                        bool UD_day_03, bool UD_day_05, bool UD_day_10, bool UD_day_20, bool UD_day_40, bool UD_day_60,
                        bool min_03, bool min_05, bool min_10, bool min_20, bool min_30, bool min_60,
                        bool day_03, bool day_05, bool day_10, bool day_20, bool day_40, bool day_60,
                        bool check_min_03, bool check_min_05, bool check_min_10, bool check_min_20, bool check_min_30, bool check_min_60,
                        bool check_day_03, bool check_day_05, bool check_day_10, bool check_day_20, bool check_day_40, bool check_day_60)
        {
            this.new_stop = new_stop;
            this.add_stop = add_stop;
            this.use_min_03 = use_min_03;
            this.use_min_05 = use_min_05;
            this.use_min_10 = use_min_10;
            this.use_min_20 = use_min_20;
            this.use_min_30 = use_min_30;
            this.use_min_60 = use_min_60;
            this.use_day_03 = use_day_03;
            this.use_day_05 = use_day_05;
            this.use_day_10 = use_day_10;
            this.use_day_20 = use_day_20;
            this.use_day_40 = use_day_40;
            this.use_day_60 = use_day_60;
            this.UD_min_03 = UD_min_03;
            this.UD_min_05 = UD_min_05;
            this.UD_min_10 = UD_min_10;
            this.UD_min_20 = UD_min_20;
            this.UD_min_30 = UD_min_30;
            this.UD_min_60 = UD_min_60;
            this.UD_day_03 = UD_day_03;
            this.UD_day_05 = UD_day_05;
            this.UD_day_10 = UD_day_10;
            this.UD_day_20 = UD_day_20;
            this.UD_day_40 = UD_day_40;
            this.UD_day_60 = UD_day_60;
            this.min_03 = min_03;
            this.min_05 = min_05;
            this.min_10 = min_10;
            this.min_20 = min_20;
            this.min_30 = min_30;
            this.min_60 = min_60;
            this.day_03 = day_03;
            this.day_05 = day_05;
            this.day_10 = day_10;
            this.day_20 = day_20;
            this.day_40 = day_40;
            this.day_60 = day_60;
            this.check_min_03 = check_min_03;
            this.check_min_05 = check_min_05;
            this.check_min_10 = check_min_10;
            this.check_min_20 = check_min_20;
            this.check_min_30 = check_min_30;
            this.check_min_60 = check_min_60;
            this.check_day_03 = check_day_03;
            this.check_day_05 = check_day_05;
            this.check_day_10 = check_day_10;
            this.check_day_20 = check_day_20;
            this.check_day_40 = check_day_40;
            this.check_day_60 = check_day_60;
        }
    }



    public class ma
    {
        public string code { get; set; }

        public double repeat_ma1_A { get; set; }
        public double repeat_ma1_B { get; set; }
        public double repeat_ma1_C { get; set; }
        public double repeat_ma1_D { get; set; }
        public double repeat_ma1_E { get; set; }
        public double repeat_ma1_F { get; set; }
        public double repeat_ma1_G { get; set; }
        public double repeat_ma1_H { get; set; }
        public double repeat_ma1_I { get; set; }
        public double repeat_ma1_J { get; set; }
        public double repeat_ma1_K { get; set; }
        public double repeat_ma1_L { get; set; }
        public double repeat_ma1_M { get; set; }
        public double repeat_ma1_N { get; set; }
        public int repeat_ma1_value_A { get; set; }
        public int repeat_ma1_value_B { get; set; }
        public int repeat_ma1_value_C { get; set; }
        public int repeat_ma1_value_D { get; set; }
        public int repeat_ma1_value_E { get; set; }
        public int repeat_ma1_value_F { get; set; }
        public int repeat_ma1_value_G { get; set; }
        public int repeat_ma1_value_H { get; set; }
        public int repeat_ma1_value_I { get; set; }
        public int repeat_ma1_value_J { get; set; }
        public int repeat_ma1_value_K { get; set; }
        public int repeat_ma1_value_L { get; set; }
        public int repeat_ma1_value_M { get; set; }
        public int repeat_ma1_value_N { get; set; }

        public double repeat_ma2_A { get; set; }
        public double repeat_ma2_B { get; set; }
        public double repeat_ma2_C { get; set; }
        public double repeat_ma2_D { get; set; }
        public double repeat_ma2_E { get; set; }
        public double repeat_ma2_F { get; set; }
        public double repeat_ma2_G { get; set; }
        public double repeat_ma2_H { get; set; }
        public double repeat_ma2_I { get; set; }
        public double repeat_ma2_J { get; set; }
        public double repeat_ma2_K { get; set; }
        public double repeat_ma2_L { get; set; }
        public double repeat_ma2_M { get; set; }
        public double repeat_ma2_N { get; set; }
        public int repeat_ma2_value_A { get; set; }
        public int repeat_ma2_value_B { get; set; }
        public int repeat_ma2_value_C { get; set; }
        public int repeat_ma2_value_D { get; set; }
        public int repeat_ma2_value_E { get; set; }
        public int repeat_ma2_value_F { get; set; }
        public int repeat_ma2_value_G { get; set; }
        public int repeat_ma2_value_H { get; set; }
        public int repeat_ma2_value_I { get; set; }
        public int repeat_ma2_value_J { get; set; }
        public int repeat_ma2_value_K { get; set; }
        public int repeat_ma2_value_L { get; set; }
        public int repeat_ma2_value_M { get; set; }
        public int repeat_ma2_value_N { get; set; }

        public double Rebal_ma1_A { get; set; }
        public double Rebal_ma1_B { get; set; }
        public double Rebal_ma1_C { get; set; }
        public double Rebal_ma1_D { get; set; }
        public double Rebal_ma1_E { get; set; }
        public double Rebal_ma1_F { get; set; }
        public double Rebal_ma1_G { get; set; }
        public int Rebal_ma1_value_A { get; set; }
        public int Rebal_ma1_value_B { get; set; }
        public int Rebal_ma1_value_C { get; set; }
        public int Rebal_ma1_value_D { get; set; }
        public int Rebal_ma1_value_E { get; set; }
        public int Rebal_ma1_value_F { get; set; }
        public int Rebal_ma1_value_G { get; set; }
        public double Rebal_ma2_A { get; set; }
        public double Rebal_ma2_B { get; set; }
        public double Rebal_ma2_C { get; set; }
        public double Rebal_ma2_D { get; set; }
        public double Rebal_ma2_E { get; set; }
        public double Rebal_ma2_F { get; set; }
        public double Rebal_ma2_G { get; set; }
        public int Rebal_ma2_value_A { get; set; }
        public int Rebal_ma2_value_B { get; set; }
        public int Rebal_ma2_value_C { get; set; }
        public int Rebal_ma2_value_D { get; set; }
        public int Rebal_ma2_value_E { get; set; }
        public int Rebal_ma2_value_F { get; set; }
        public int Rebal_ma2_value_G { get; set; }

        public double Rebal_TS_ma_1차_A { get; set; }
        public double Rebal_TS_ma_1차_B { get; set; }
        public double Rebal_TS_ma_1차_C { get; set; }
        public double Rebal_TS_ma_1차_D { get; set; }
        public double Rebal_TS_ma_1차_E { get; set; }
        public double Rebal_TS_ma_1차_F { get; set; }
        public double Rebal_TS_ma_1차_G { get; set; }
        public int Rebal_TS_ma_1차_value_A { get; set; }
        public int Rebal_TS_ma_1차_value_B { get; set; }
        public int Rebal_TS_ma_1차_value_C { get; set; }
        public int Rebal_TS_ma_1차_value_D { get; set; }
        public int Rebal_TS_ma_1차_value_E { get; set; }
        public int Rebal_TS_ma_1차_value_F { get; set; }
        public int Rebal_TS_ma_1차_value_G { get; set; }

        public double Rebal_TS_ma_2차_A { get; set; }
        public double Rebal_TS_ma_2차_B { get; set; }
        public double Rebal_TS_ma_2차_C { get; set; }
        public double Rebal_TS_ma_2차_D { get; set; }
        public double Rebal_TS_ma_2차_E { get; set; }
        public double Rebal_TS_ma_2차_F { get; set; }
        public double Rebal_TS_ma_2차_G { get; set; }
        public int Rebal_TS_ma_2차_value_A { get; set; }
        public int Rebal_TS_ma_2차_value_B { get; set; }
        public int Rebal_TS_ma_2차_value_C { get; set; }
        public int Rebal_TS_ma_2차_value_D { get; set; }
        public int Rebal_TS_ma_2차_value_E { get; set; }
        public int Rebal_TS_ma_2차_value_F { get; set; }
        public int Rebal_TS_ma_2차_value_G { get; set; }

        public double Liquidation_ma_A { get; set; }
        public double Liquidation_ma_B { get; set; }
        public double Liquidation_ma_C { get; set; }
        public int Liquidation_ma_value_A { get; set; }
        public int Liquidation_ma_value_B { get; set; }
        public int Liquidation_ma_value_C { get; set; }
        public double Liquidation_TS_ma_A { get; set; }
        public double Liquidation_TS_ma_B { get; set; }
        public double Liquidation_TS_ma_C { get; set; }
        public int Liquidation_TS_ma_value_A { get; set; }
        public int Liquidation_TS_ma_value_B { get; set; }
        public int Liquidation_TS_ma_value_C { get; set; }

        public double 매매기간_TS_ma_A { get; set; }
        public double 매매기간_TS_ma_B { get; set; }
        public double 매매기간_TS_ma_C { get; set; }
        public double 매매기간_TS_ma_D { get; set; }
        public double 매매기간_TS_ma_E { get; set; }
        public double 매매기간_TS_ma_F { get; set; }
        public int 매매기간_TS_ma_value_A { get; set; }
        public int 매매기간_TS_ma_value_B { get; set; }
        public int 매매기간_TS_ma_value_C { get; set; }
        public int 매매기간_TS_ma_value_D { get; set; }
        public int 매매기간_TS_ma_value_E { get; set; }
        public int 매매기간_TS_ma_value_F { get; set; }


        public ma(string code,

                         double repeat_ma1_A, double repeat_ma1_B, double repeat_ma1_C, double repeat_ma1_D, double repeat_ma1_E, double repeat_ma1_F, double repeat_ma1_G, double repeat_ma1_H, double repeat_ma1_I, double repeat_ma1_J, double repeat_ma1_K, double repeat_ma1_L, double repeat_ma1_M, double repeat_ma1_N,
                         int repeat_ma1_value_A, int repeat_ma1_value_B, int repeat_ma1_value_C, int repeat_ma1_value_D, int repeat_ma1_value_E, int repeat_ma1_value_F, int repeat_ma1_value_G, int repeat_ma1_value_H, int repeat_ma1_value_I, int repeat_ma1_value_J, int repeat_ma1_value_K, int repeat_ma1_value_L, int repeat_ma1_value_M, int repeat_ma1_value_N,
                         double repeat_ma2_A, double repeat_ma2_B, double repeat_ma2_C, double repeat_ma2_D, double repeat_ma2_E, double repeat_ma2_F, double repeat_ma2_G, double repeat_ma2_H, double repeat_ma2_I, double repeat_ma2_J, double repeat_ma2_K, double repeat_ma2_L, double repeat_ma2_M, double repeat_ma2_N,
                         int repeat_ma2_value_A, int repeat_ma2_value_B, int repeat_ma2_value_C, int repeat_ma2_value_D, int repeat_ma2_value_E, int repeat_ma2_value_F, int repeat_ma2_value_G, int repeat_ma2_value_H, int repeat_ma2_value_I, int repeat_ma2_value_J, int repeat_ma2_value_K, int repeat_ma2_value_L, int repeat_ma2_value_M, int repeat_ma2_value_N,

                         double Rebal_ma1_A, double Rebal_ma1_B, double Rebal_ma1_C, double Rebal_ma1_D, double Rebal_ma1_E, double Rebal_ma1_F, double Rebal_ma1_G,
                         int Rebal_ma1_value_A, int Rebal_ma1_value_B, int Rebal_ma1_value_C, int Rebal_ma1_value_D, int Rebal_ma1_value_E, int Rebal_ma1_value_F, int Rebal_ma1_value_G,
                         double Rebal_ma2_A, double Rebal_ma2_B, double Rebal_ma2_C, double Rebal_ma2_D, double Rebal_ma2_E, double Rebal_ma2_F, double Rebal_ma2_G,
                         int Rebal_ma2_value_A, int Rebal_ma2_value_B, int Rebal_ma2_value_C, int Rebal_ma2_value_D, int Rebal_ma2_value_E, int Rebal_ma2_value_F, int Rebal_ma2_value_G,

                         double Rebal_TS_ma_1차_A, double Rebal_TS_ma_1차_B, double Rebal_TS_ma_1차_C, double Rebal_TS_ma_1차_D, double Rebal_TS_ma_1차_E, double Rebal_TS_ma_1차_F, double Rebal_TS_ma_1차_G,
                         int Rebal_TS_ma_1차_value_A, int Rebal_TS_ma_1차_value_B, int Rebal_TS_ma_1차_value_C, int Rebal_TS_ma_1차_value_D, int Rebal_TS_ma_1차_value_E, int Rebal_TS_ma_1차_value_F, int Rebal_TS_ma_1차_value_G,

                         double Rebal_TS_ma_2차_A, double Rebal_TS_ma_2차_B, double Rebal_TS_ma_2차_C, double Rebal_TS_ma_2차_D, double Rebal_TS_ma_2차_E, double Rebal_TS_ma_2차_F, double Rebal_TS_ma_2차_G,
                         int Rebal_TS_ma_2차_value_A, int Rebal_TS_ma_2차_value_B, int Rebal_TS_ma_2차_value_C, int Rebal_TS_ma_2차_value_D, int Rebal_TS_ma_2차_value_E, int Rebal_TS_ma_2차_value_F, int Rebal_TS_ma_2차_value_G,

                         double Liquidation_ma_A, double Liquidation_ma_B, double Liquidation_ma_C, double Liquidation_TS_ma_A, double Liquidation_TS_ma_B, double Liquidation_TS_ma_C,
                         int Liquidation_ma_value_A, int Liquidation_ma_value_B, int Liquidation_ma_value_C, int Liquidation_TS_ma_value_A, int Liquidation_TS_ma_value_B, int Liquidation_TS_ma_value_C,

                         double 매매기간_TS_ma_A, double 매매기간_TS_ma_B, double 매매기간_TS_ma_C, double 매매기간_TS_ma_D, double 매매기간_TS_ma_E, double 매매기간_TS_ma_F,
                         int 매매기간_TS_ma_value_A, int 매매기간_TS_ma_value_B, int 매매기간_TS_ma_value_C, int 매매기간_TS_ma_value_D, int 매매기간_TS_ma_value_E, int 매매기간_TS_ma_value_F
            )
        {
            this.code = code;

            this.repeat_ma1_A = repeat_ma1_A;
            this.repeat_ma1_B = repeat_ma1_B;
            this.repeat_ma1_C = repeat_ma1_C;
            this.repeat_ma1_D = repeat_ma1_D;
            this.repeat_ma1_E = repeat_ma1_E;
            this.repeat_ma1_F = repeat_ma1_F;
            this.repeat_ma1_G = repeat_ma1_G;
            this.repeat_ma1_H = repeat_ma1_H;
            this.repeat_ma1_I = repeat_ma1_I;
            this.repeat_ma1_J = repeat_ma1_J;
            this.repeat_ma1_K = repeat_ma1_K;
            this.repeat_ma1_L = repeat_ma1_L;
            this.repeat_ma1_M = repeat_ma1_M;
            this.repeat_ma1_N = repeat_ma1_N;

            this.repeat_ma1_value_A = repeat_ma1_value_A;
            this.repeat_ma1_value_B = repeat_ma1_value_B;
            this.repeat_ma1_value_C = repeat_ma1_value_C;
            this.repeat_ma1_value_D = repeat_ma1_value_D;
            this.repeat_ma1_value_E = repeat_ma1_value_E;
            this.repeat_ma1_value_F = repeat_ma1_value_F;
            this.repeat_ma1_value_G = repeat_ma1_value_G;
            this.repeat_ma1_value_H = repeat_ma1_value_H;
            this.repeat_ma1_value_I = repeat_ma1_value_I;
            this.repeat_ma1_value_J = repeat_ma1_value_J;
            this.repeat_ma1_value_K = repeat_ma1_value_K;
            this.repeat_ma1_value_L = repeat_ma1_value_L;
            this.repeat_ma1_value_M = repeat_ma1_value_M;
            this.repeat_ma1_value_N = repeat_ma1_value_N;

            this.repeat_ma2_A = repeat_ma2_A;
            this.repeat_ma2_B = repeat_ma2_B;
            this.repeat_ma2_C = repeat_ma2_C;
            this.repeat_ma2_D = repeat_ma2_D;
            this.repeat_ma2_E = repeat_ma2_E;
            this.repeat_ma2_F = repeat_ma2_F;
            this.repeat_ma2_G = repeat_ma2_G;
            this.repeat_ma2_H = repeat_ma2_H;
            this.repeat_ma2_I = repeat_ma2_I;
            this.repeat_ma2_J = repeat_ma2_J;
            this.repeat_ma2_K = repeat_ma2_K;
            this.repeat_ma2_L = repeat_ma2_L;
            this.repeat_ma2_M = repeat_ma2_M;
            this.repeat_ma2_N = repeat_ma2_N;

            this.repeat_ma2_value_A = repeat_ma2_value_A;
            this.repeat_ma2_value_B = repeat_ma2_value_B;
            this.repeat_ma2_value_C = repeat_ma2_value_C;
            this.repeat_ma2_value_D = repeat_ma2_value_D;
            this.repeat_ma2_value_E = repeat_ma2_value_E;
            this.repeat_ma2_value_F = repeat_ma2_value_F;
            this.repeat_ma2_value_G = repeat_ma2_value_G;
            this.repeat_ma2_value_H = repeat_ma2_value_H;
            this.repeat_ma2_value_I = repeat_ma2_value_I;
            this.repeat_ma2_value_J = repeat_ma2_value_J;
            this.repeat_ma2_value_K = repeat_ma2_value_K;
            this.repeat_ma2_value_L = repeat_ma2_value_L;
            this.repeat_ma2_value_M = repeat_ma2_value_M;
            this.repeat_ma2_value_N = repeat_ma2_value_N;

            this.Rebal_ma1_A = Rebal_ma1_A;
            this.Rebal_ma1_B = Rebal_ma1_B;
            this.Rebal_ma1_C = Rebal_ma1_C;
            this.Rebal_ma1_D = Rebal_ma1_D;
            this.Rebal_ma1_E = Rebal_ma1_E;
            this.Rebal_ma1_F = Rebal_ma1_F;
            this.Rebal_ma1_G = Rebal_ma1_G;

            this.Rebal_ma1_value_A = Rebal_ma1_value_A;
            this.Rebal_ma1_value_B = Rebal_ma1_value_B;
            this.Rebal_ma1_value_C = Rebal_ma1_value_C;
            this.Rebal_ma1_value_D = Rebal_ma1_value_D;
            this.Rebal_ma1_value_E = Rebal_ma1_value_E;
            this.Rebal_ma1_value_F = Rebal_ma1_value_F;
            this.Rebal_ma1_value_G = Rebal_ma1_value_G;

            this.Rebal_ma2_A = Rebal_ma2_A;
            this.Rebal_ma2_B = Rebal_ma2_B;
            this.Rebal_ma2_C = Rebal_ma2_C;
            this.Rebal_ma2_D = Rebal_ma2_D;
            this.Rebal_ma2_E = Rebal_ma2_E;
            this.Rebal_ma2_F = Rebal_ma2_F;
            this.Rebal_ma2_G = Rebal_ma2_G;

            this.Rebal_ma2_value_A = Rebal_ma2_value_A;
            this.Rebal_ma2_value_B = Rebal_ma2_value_B;
            this.Rebal_ma2_value_C = Rebal_ma2_value_C;
            this.Rebal_ma2_value_D = Rebal_ma2_value_D;
            this.Rebal_ma2_value_E = Rebal_ma2_value_E;
            this.Rebal_ma2_value_F = Rebal_ma2_value_F;
            this.Rebal_ma2_value_G = Rebal_ma2_value_G;

            this.Rebal_TS_ma_1차_A = Rebal_TS_ma_1차_A;
            this.Rebal_TS_ma_1차_B = Rebal_TS_ma_1차_B;
            this.Rebal_TS_ma_1차_C = Rebal_TS_ma_1차_C;
            this.Rebal_TS_ma_1차_D = Rebal_TS_ma_1차_D;
            this.Rebal_TS_ma_1차_E = Rebal_TS_ma_1차_E;
            this.Rebal_TS_ma_1차_F = Rebal_TS_ma_1차_F;
            this.Rebal_TS_ma_1차_G = Rebal_TS_ma_1차_G;

            this.Rebal_TS_ma_1차_value_A = Rebal_TS_ma_1차_value_A;
            this.Rebal_TS_ma_1차_value_B = Rebal_TS_ma_1차_value_B;
            this.Rebal_TS_ma_1차_value_C = Rebal_TS_ma_1차_value_C;
            this.Rebal_TS_ma_1차_value_D = Rebal_TS_ma_1차_value_D;
            this.Rebal_TS_ma_1차_value_E = Rebal_TS_ma_1차_value_E;
            this.Rebal_TS_ma_1차_value_F = Rebal_TS_ma_1차_value_F;
            this.Rebal_TS_ma_1차_value_G = Rebal_TS_ma_1차_value_G;

            this.Rebal_TS_ma_2차_A = Rebal_TS_ma_2차_A;
            this.Rebal_TS_ma_2차_B = Rebal_TS_ma_2차_B;
            this.Rebal_TS_ma_2차_C = Rebal_TS_ma_2차_C;
            this.Rebal_TS_ma_2차_D = Rebal_TS_ma_2차_D;
            this.Rebal_TS_ma_2차_E = Rebal_TS_ma_2차_E;
            this.Rebal_TS_ma_2차_F = Rebal_TS_ma_2차_F;
            this.Rebal_TS_ma_2차_G = Rebal_TS_ma_2차_G;

            this.Rebal_TS_ma_2차_value_A = Rebal_TS_ma_2차_value_A;
            this.Rebal_TS_ma_2차_value_B = Rebal_TS_ma_2차_value_B;
            this.Rebal_TS_ma_2차_value_C = Rebal_TS_ma_2차_value_C;
            this.Rebal_TS_ma_2차_value_D = Rebal_TS_ma_2차_value_D;
            this.Rebal_TS_ma_2차_value_E = Rebal_TS_ma_2차_value_E;
            this.Rebal_TS_ma_2차_value_F = Rebal_TS_ma_2차_value_F;
            this.Rebal_TS_ma_2차_value_G = Rebal_TS_ma_2차_value_G;

            this.Liquidation_ma_A = Liquidation_ma_A;
            this.Liquidation_ma_B = Liquidation_ma_B;
            this.Liquidation_ma_C = Liquidation_ma_C;

            this.Liquidation_ma_value_A = Liquidation_ma_value_A;
            this.Liquidation_ma_value_B = Liquidation_ma_value_B;
            this.Liquidation_ma_value_C = Liquidation_ma_value_C;

            this.Liquidation_TS_ma_A = Liquidation_TS_ma_A;
            this.Liquidation_TS_ma_B = Liquidation_TS_ma_B;
            this.Liquidation_TS_ma_C = Liquidation_TS_ma_C;

            this.Liquidation_TS_ma_value_A = Liquidation_TS_ma_value_A;
            this.Liquidation_TS_ma_value_B = Liquidation_TS_ma_value_B;
            this.Liquidation_TS_ma_value_C = Liquidation_TS_ma_value_C;

            this.매매기간_TS_ma_A = 매매기간_TS_ma_A;
            this.매매기간_TS_ma_B = 매매기간_TS_ma_B;
            this.매매기간_TS_ma_C = 매매기간_TS_ma_C;
            this.매매기간_TS_ma_D = 매매기간_TS_ma_D;
            this.매매기간_TS_ma_E = 매매기간_TS_ma_E;
            this.매매기간_TS_ma_F = 매매기간_TS_ma_F;

            this.매매기간_TS_ma_value_A = 매매기간_TS_ma_value_A;
            this.매매기간_TS_ma_value_B = 매매기간_TS_ma_value_B;
            this.매매기간_TS_ma_value_C = 매매기간_TS_ma_value_C;
            this.매매기간_TS_ma_value_D = 매매기간_TS_ma_value_D;
            this.매매기간_TS_ma_value_E = 매매기간_TS_ma_value_E;
            this.매매기간_TS_ma_value_F = 매매기간_TS_ma_value_F;
        }
    }
}
