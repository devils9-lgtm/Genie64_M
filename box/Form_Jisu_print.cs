using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64.box
{
    public partial class Form_Jisu_print : UserControl
    {

        public static Form_Jisu_print form;

        public Form_Jisu_print()
        {
            form = this;
            InitializeComponent();
        }


        public void Jisu_avg_Label_print()
        {
            if (Form1.FormJisu_print_Open)
            {
                지수이평추세 avg = Form1.지수이평추세[0];
                label_kospi_min_03.Text = Print(avg.Min_추세_03, label_kospi_min_03);
                label_kospi_min_05.Text = Print(avg.Min_추세_05, label_kospi_min_05);
                label_kospi_min_10.Text = Print(avg.Min_추세_10, label_kospi_min_10);
                label_kospi_min_20.Text = Print(avg.Min_추세_20, label_kospi_min_20);
                label_kospi_min_30.Text = Print(avg.Min_추세_30, label_kospi_min_30);
                label_kospi_min_60.Text = Print(avg.Min_추세_60, label_kospi_min_60);
                label_kospi_day_03.Text = Print(avg.Day_추세_03, label_kospi_day_03);
                label_kospi_day_05.Text = Print(avg.Day_추세_05, label_kospi_day_05);
                label_kospi_day_10.Text = Print(avg.Day_추세_10, label_kospi_day_10);
                label_kospi_day_20.Text = Print(avg.Day_추세_20, label_kospi_day_20);
                label_kospi_day_40.Text = Print(avg.Day_추세_40, label_kospi_day_40);
                label_kospi_day_60.Text = Print(avg.Day_추세_60, label_kospi_day_60);

                avg = Form1.지수이평추세[1];
                label_kosdaq_min_03.Text = Print(avg.Min_추세_03, label_kosdaq_min_03);
                label_kosdaq_min_05.Text = Print(avg.Min_추세_05, label_kosdaq_min_05);
                label_kosdaq_min_10.Text = Print(avg.Min_추세_10, label_kosdaq_min_10);
                label_kosdaq_min_20.Text = Print(avg.Min_추세_20, label_kosdaq_min_20);
                label_kosdaq_min_30.Text = Print(avg.Min_추세_30, label_kosdaq_min_30);
                label_kosdaq_min_60.Text = Print(avg.Min_추세_60, label_kosdaq_min_60);
                label_kosdaq_day_03.Text = Print(avg.Day_추세_03, label_kosdaq_day_03);
                label_kosdaq_day_05.Text = Print(avg.Day_추세_05, label_kosdaq_day_05);
                label_kosdaq_day_10.Text = Print(avg.Day_추세_10, label_kosdaq_day_10);
                label_kosdaq_day_20.Text = Print(avg.Day_추세_20, label_kosdaq_day_20);
                label_kosdaq_day_40.Text = Print(avg.Day_추세_40, label_kosdaq_day_40);
                label_kosdaq_day_60.Text = Print(avg.Day_추세_60, label_kosdaq_day_60);
            }

            static string Print(bool 이평추세, Label label)
            {
                if (이평추세)
                {
                    label.ForeColor = Color.Red;
                    return "▲";
                }
                else
                {
                    label.ForeColor = Color.Blue;
                    return "▼";
                }
            }
        }
    }
}
