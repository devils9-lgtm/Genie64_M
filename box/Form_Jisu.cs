using System;
using System.Drawing;
using System.Windows.Forms;

namespace 지니_64.box
{
    public partial class Form_Jisu : UserControl
    {
        public static Form_Jisu form;
        public Form_Jisu()
        {
            form = this;
            InitializeComponent();
        }

        private void Form_Jisu_Load(object sender, EventArgs e)
        {
            Jisu_Form_Load();
        }

        public void Jisu_Form_Load()
        {
            Form1.음소거 = true;
            CB_kospi_new_stop.Checked = Properties.Settings.Default.CB_kospi_new_stop;
            CB_kospi_add_stop.Checked = Properties.Settings.Default.CB_kospi_add_stop;
            CB_kosdaq_new_stop.Checked = Properties.Settings.Default.CB_kosdaq_new_stop;
            CB_kosdaq_add_stop.Checked = Properties.Settings.Default.CB_kosdaq_add_stop;

            CB_use_kospi_min_03.Checked = Properties.Settings.Default.CB_use_kospi_min_03;
            CB_use_kospi_min_05.Checked = Properties.Settings.Default.CB_use_kospi_min_05;
            CB_use_kospi_min_10.Checked = Properties.Settings.Default.CB_use_kospi_min_10;
            CB_use_kospi_min_20.Checked = Properties.Settings.Default.CB_use_kospi_min_20;
            CB_use_kospi_min_30.Checked = Properties.Settings.Default.CB_use_kospi_min_30;
            CB_use_kospi_min_60.Checked = Properties.Settings.Default.CB_use_kospi_min_60;
            CB_use_kospi_day_03.Checked = Properties.Settings.Default.CB_use_kospi_day_03;
            CB_use_kospi_day_05.Checked = Properties.Settings.Default.CB_use_kospi_day_05;
            CB_use_kospi_day_10.Checked = Properties.Settings.Default.CB_use_kospi_day_10;
            CB_use_kospi_day_20.Checked = Properties.Settings.Default.CB_use_kospi_day_20;
            CB_use_kospi_day_40.Checked = Properties.Settings.Default.CB_use_kospi_day_40;
            CB_use_kospi_day_60.Checked = Properties.Settings.Default.CB_use_kospi_day_60;

            CB_UD_kospi_min_03.Checked = Properties.Settings.Default.CB_UD_kospi_min_03;
            CB_UD_kospi_min_05.Checked = Properties.Settings.Default.CB_UD_kospi_min_05;
            CB_UD_kospi_min_10.Checked = Properties.Settings.Default.CB_UD_kospi_min_10;
            CB_UD_kospi_min_20.Checked = Properties.Settings.Default.CB_UD_kospi_min_20;
            CB_UD_kospi_min_30.Checked = Properties.Settings.Default.CB_UD_kospi_min_30;
            CB_UD_kospi_min_60.Checked = Properties.Settings.Default.CB_UD_kospi_min_60;
            CB_UD_kospi_day_03.Checked = Properties.Settings.Default.CB_UD_kospi_day_03;
            CB_UD_kospi_day_05.Checked = Properties.Settings.Default.CB_UD_kospi_day_05;
            CB_UD_kospi_day_10.Checked = Properties.Settings.Default.CB_UD_kospi_day_10;
            CB_UD_kospi_day_20.Checked = Properties.Settings.Default.CB_UD_kospi_day_20;
            CB_UD_kospi_day_40.Checked = Properties.Settings.Default.CB_UD_kospi_day_40;
            CB_UD_kospi_day_60.Checked = Properties.Settings.Default.CB_UD_kospi_day_60;

            CB_use_kosdaq_min_03.Checked = Properties.Settings.Default.CB_use_kosdaq_min_03;
            CB_use_kosdaq_min_05.Checked = Properties.Settings.Default.CB_use_kosdaq_min_05;
            CB_use_kosdaq_min_10.Checked = Properties.Settings.Default.CB_use_kosdaq_min_10;
            CB_use_kosdaq_min_20.Checked = Properties.Settings.Default.CB_use_kosdaq_min_20;
            CB_use_kosdaq_min_30.Checked = Properties.Settings.Default.CB_use_kosdaq_min_30;
            CB_use_kosdaq_min_60.Checked = Properties.Settings.Default.CB_use_kosdaq_min_60;
            CB_use_kosdaq_day_03.Checked = Properties.Settings.Default.CB_use_kosdaq_day_03;
            CB_use_kosdaq_day_05.Checked = Properties.Settings.Default.CB_use_kosdaq_day_05;
            CB_use_kosdaq_day_10.Checked = Properties.Settings.Default.CB_use_kosdaq_day_10;
            CB_use_kosdaq_day_20.Checked = Properties.Settings.Default.CB_use_kosdaq_day_20;
            CB_use_kosdaq_day_40.Checked = Properties.Settings.Default.CB_use_kosdaq_day_40;
            CB_use_kosdaq_day_60.Checked = Properties.Settings.Default.CB_use_kosdaq_day_60;

            CB_UD_kosdaq_min_03.Checked = Properties.Settings.Default.CB_UD_kosdaq_min_03;
            CB_UD_kosdaq_min_05.Checked = Properties.Settings.Default.CB_UD_kosdaq_min_05;
            CB_UD_kosdaq_min_10.Checked = Properties.Settings.Default.CB_UD_kosdaq_min_10;
            CB_UD_kosdaq_min_20.Checked = Properties.Settings.Default.CB_UD_kosdaq_min_20;
            CB_UD_kosdaq_min_30.Checked = Properties.Settings.Default.CB_UD_kosdaq_min_30;
            CB_UD_kosdaq_min_60.Checked = Properties.Settings.Default.CB_UD_kosdaq_min_60;
            CB_UD_kosdaq_day_03.Checked = Properties.Settings.Default.CB_UD_kosdaq_day_03;
            CB_UD_kosdaq_day_05.Checked = Properties.Settings.Default.CB_UD_kosdaq_day_05;
            CB_UD_kosdaq_day_10.Checked = Properties.Settings.Default.CB_UD_kosdaq_day_10;
            CB_UD_kosdaq_day_20.Checked = Properties.Settings.Default.CB_UD_kosdaq_day_20;
            CB_UD_kosdaq_day_40.Checked = Properties.Settings.Default.CB_UD_kosdaq_day_40;
            CB_UD_kosdaq_day_60.Checked = Properties.Settings.Default.CB_UD_kosdaq_day_60;

            if (Properties.Settings.Default.CB_가이드매매) ControllerDisable.Form_Jisu_Disable();
            Form1.음소거 = Properties.Settings.Default.CB_음소거;
        }

        public void SAVE_Jisu_avg_Checked()
        {
            Properties.Settings.Default.CB_kospi_new_stop = CB_kospi_new_stop.Checked;
            Properties.Settings.Default.CB_kospi_add_stop = CB_kospi_add_stop.Checked;
            Properties.Settings.Default.CB_kosdaq_new_stop = CB_kosdaq_new_stop.Checked;
            Properties.Settings.Default.CB_kosdaq_add_stop = CB_kosdaq_add_stop.Checked;

            Properties.Settings.Default.CB_use_kospi_min_03 = CB_use_kospi_min_03.Checked;
            Properties.Settings.Default.CB_use_kospi_min_05 = CB_use_kospi_min_05.Checked;
            Properties.Settings.Default.CB_use_kospi_min_10 = CB_use_kospi_min_10.Checked;
            Properties.Settings.Default.CB_use_kospi_min_20 = CB_use_kospi_min_20.Checked;
            Properties.Settings.Default.CB_use_kospi_min_30 = CB_use_kospi_min_30.Checked;
            Properties.Settings.Default.CB_use_kospi_min_60 = CB_use_kospi_min_60.Checked;
            Properties.Settings.Default.CB_use_kospi_day_03 = CB_use_kospi_day_03.Checked;
            Properties.Settings.Default.CB_use_kospi_day_05 = CB_use_kospi_day_05.Checked;
            Properties.Settings.Default.CB_use_kospi_day_10 = CB_use_kospi_day_10.Checked;
            Properties.Settings.Default.CB_use_kospi_day_20 = CB_use_kospi_day_20.Checked;
            Properties.Settings.Default.CB_use_kospi_day_40 = CB_use_kospi_day_40.Checked;
            Properties.Settings.Default.CB_use_kospi_day_60 = CB_use_kospi_day_60.Checked;

            Properties.Settings.Default.CB_UD_kospi_min_03 = CB_UD_kospi_min_03.Checked;
            Properties.Settings.Default.CB_UD_kospi_min_05 = CB_UD_kospi_min_05.Checked;
            Properties.Settings.Default.CB_UD_kospi_min_10 = CB_UD_kospi_min_10.Checked;
            Properties.Settings.Default.CB_UD_kospi_min_20 = CB_UD_kospi_min_20.Checked;
            Properties.Settings.Default.CB_UD_kospi_min_30 = CB_UD_kospi_min_30.Checked;
            Properties.Settings.Default.CB_UD_kospi_min_60 = CB_UD_kospi_min_60.Checked;
            Properties.Settings.Default.CB_UD_kospi_day_03 = CB_UD_kospi_day_03.Checked;
            Properties.Settings.Default.CB_UD_kospi_day_05 = CB_UD_kospi_day_05.Checked;
            Properties.Settings.Default.CB_UD_kospi_day_10 = CB_UD_kospi_day_10.Checked;
            Properties.Settings.Default.CB_UD_kospi_day_20 = CB_UD_kospi_day_20.Checked;
            Properties.Settings.Default.CB_UD_kospi_day_40 = CB_UD_kospi_day_40.Checked;
            Properties.Settings.Default.CB_UD_kospi_day_60 = CB_UD_kospi_day_60.Checked;

            Properties.Settings.Default.CB_use_kosdaq_min_03 = CB_use_kosdaq_min_03.Checked;
            Properties.Settings.Default.CB_use_kosdaq_min_05 = CB_use_kosdaq_min_05.Checked;
            Properties.Settings.Default.CB_use_kosdaq_min_10 = CB_use_kosdaq_min_10.Checked;
            Properties.Settings.Default.CB_use_kosdaq_min_20 = CB_use_kosdaq_min_20.Checked;
            Properties.Settings.Default.CB_use_kosdaq_min_30 = CB_use_kosdaq_min_30.Checked;
            Properties.Settings.Default.CB_use_kosdaq_min_60 = CB_use_kosdaq_min_60.Checked;
            Properties.Settings.Default.CB_use_kosdaq_day_03 = CB_use_kosdaq_day_03.Checked;
            Properties.Settings.Default.CB_use_kosdaq_day_05 = CB_use_kosdaq_day_05.Checked;
            Properties.Settings.Default.CB_use_kosdaq_day_10 = CB_use_kosdaq_day_10.Checked;
            Properties.Settings.Default.CB_use_kosdaq_day_20 = CB_use_kosdaq_day_20.Checked;
            Properties.Settings.Default.CB_use_kosdaq_day_40 = CB_use_kosdaq_day_40.Checked;
            Properties.Settings.Default.CB_use_kosdaq_day_60 = CB_use_kosdaq_day_60.Checked;

            Properties.Settings.Default.CB_UD_kosdaq_min_03 = CB_UD_kosdaq_min_03.Checked;
            Properties.Settings.Default.CB_UD_kosdaq_min_05 = CB_UD_kosdaq_min_05.Checked;
            Properties.Settings.Default.CB_UD_kosdaq_min_10 = CB_UD_kosdaq_min_10.Checked;
            Properties.Settings.Default.CB_UD_kosdaq_min_20 = CB_UD_kosdaq_min_20.Checked;
            Properties.Settings.Default.CB_UD_kosdaq_min_30 = CB_UD_kosdaq_min_30.Checked;
            Properties.Settings.Default.CB_UD_kosdaq_min_60 = CB_UD_kosdaq_min_60.Checked;
            Properties.Settings.Default.CB_UD_kosdaq_day_03 = CB_UD_kosdaq_day_03.Checked;
            Properties.Settings.Default.CB_UD_kosdaq_day_05 = CB_UD_kosdaq_day_05.Checked;
            Properties.Settings.Default.CB_UD_kosdaq_day_10 = CB_UD_kosdaq_day_10.Checked;
            Properties.Settings.Default.CB_UD_kosdaq_day_20 = CB_UD_kosdaq_day_20.Checked;
            Properties.Settings.Default.CB_UD_kosdaq_day_40 = CB_UD_kosdaq_day_40.Checked;
            Properties.Settings.Default.CB_UD_kosdaq_day_60 = CB_UD_kosdaq_day_60.Checked;

            Form1.AVG_jisu[0].new_stop = CB_kospi_new_stop.Checked;
            Form1.AVG_jisu[0].add_stop = CB_kospi_add_stop.Checked;
            Form1.AVG_jisu[0].use_min_03 = CB_use_kospi_min_03.Checked;
            Form1.AVG_jisu[0].use_min_05 = CB_use_kospi_min_05.Checked;
            Form1.AVG_jisu[0].use_min_10 = CB_use_kospi_min_10.Checked;
            Form1.AVG_jisu[0].use_min_20 = CB_use_kospi_min_20.Checked;
            Form1.AVG_jisu[0].use_min_30 = CB_use_kospi_min_30.Checked;
            Form1.AVG_jisu[0].use_min_60 = CB_use_kospi_min_60.Checked;
            Form1.AVG_jisu[0].use_day_03 = CB_use_kospi_day_03.Checked;
            Form1.AVG_jisu[0].use_day_05 = CB_use_kospi_day_05.Checked;
            Form1.AVG_jisu[0].use_day_10 = CB_use_kospi_day_10.Checked;
            Form1.AVG_jisu[0].use_day_20 = CB_use_kospi_day_20.Checked;
            Form1.AVG_jisu[0].use_day_40 = CB_use_kospi_day_40.Checked;
            Form1.AVG_jisu[0].use_day_60 = CB_use_kospi_day_60.Checked;
            Form1.AVG_jisu[0].UD_min_03 = CB_UD_kospi_min_03.Checked;
            Form1.AVG_jisu[0].UD_min_05 = CB_UD_kospi_min_05.Checked;
            Form1.AVG_jisu[0].UD_min_10 = CB_UD_kospi_min_10.Checked;
            Form1.AVG_jisu[0].UD_min_20 = CB_UD_kospi_min_20.Checked;
            Form1.AVG_jisu[0].UD_min_30 = CB_UD_kospi_min_30.Checked;
            Form1.AVG_jisu[0].UD_min_60 = CB_UD_kospi_min_60.Checked;
            Form1.AVG_jisu[0].UD_day_03 = CB_UD_kospi_day_03.Checked;
            Form1.AVG_jisu[0].UD_day_05 = CB_UD_kospi_day_05.Checked;
            Form1.AVG_jisu[0].UD_day_10 = CB_UD_kospi_day_10.Checked;
            Form1.AVG_jisu[0].UD_day_20 = CB_UD_kospi_day_20.Checked;
            Form1.AVG_jisu[0].UD_day_40 = CB_UD_kospi_day_40.Checked;
            Form1.AVG_jisu[0].UD_day_60 = CB_UD_kospi_day_60.Checked;

            Form1.AVG_jisu[1].new_stop = CB_kosdaq_new_stop.Checked;
            Form1.AVG_jisu[1].add_stop = CB_kosdaq_add_stop.Checked;
            Form1.AVG_jisu[1].use_min_03 = CB_use_kosdaq_min_03.Checked;
            Form1.AVG_jisu[1].use_min_05 = CB_use_kosdaq_min_05.Checked;
            Form1.AVG_jisu[1].use_min_10 = CB_use_kosdaq_min_10.Checked;
            Form1.AVG_jisu[1].use_min_20 = CB_use_kosdaq_min_20.Checked;
            Form1.AVG_jisu[1].use_min_30 = CB_use_kosdaq_min_30.Checked;
            Form1.AVG_jisu[1].use_min_60 = CB_use_kosdaq_min_60.Checked;
            Form1.AVG_jisu[1].use_day_03 = CB_use_kosdaq_day_03.Checked;
            Form1.AVG_jisu[1].use_day_05 = CB_use_kosdaq_day_05.Checked;
            Form1.AVG_jisu[1].use_day_10 = CB_use_kosdaq_day_10.Checked;
            Form1.AVG_jisu[1].use_day_20 = CB_use_kosdaq_day_20.Checked;
            Form1.AVG_jisu[1].use_day_40 = CB_use_kosdaq_day_40.Checked;
            Form1.AVG_jisu[1].use_day_60 = CB_use_kosdaq_day_60.Checked;
            Form1.AVG_jisu[1].UD_min_03 = CB_UD_kosdaq_min_03.Checked;
            Form1.AVG_jisu[1].UD_min_05 = CB_UD_kosdaq_min_05.Checked;
            Form1.AVG_jisu[1].UD_min_10 = CB_UD_kosdaq_min_10.Checked;
            Form1.AVG_jisu[1].UD_min_20 = CB_UD_kosdaq_min_20.Checked;
            Form1.AVG_jisu[1].UD_min_30 = CB_UD_kosdaq_min_30.Checked;
            Form1.AVG_jisu[1].UD_min_60 = CB_UD_kosdaq_min_60.Checked;
            Form1.AVG_jisu[1].UD_day_03 = CB_UD_kosdaq_day_03.Checked;
            Form1.AVG_jisu[1].UD_day_05 = CB_UD_kosdaq_day_05.Checked;
            Form1.AVG_jisu[1].UD_day_10 = CB_UD_kosdaq_day_10.Checked;
            Form1.AVG_jisu[1].UD_day_20 = CB_UD_kosdaq_day_20.Checked;
            Form1.AVG_jisu[1].UD_day_40 = CB_UD_kosdaq_day_40.Checked;
            Form1.AVG_jisu[1].UD_day_60 = CB_UD_kosdaq_day_60.Checked;
        }

        public void Jisu_avg_Label_print()
        {
            if (Form1.FormJisu_Open)
            {
                AVG_jisu avg = Form1.AVG_jisu[0];
                label_kospi_min_03.Text = print(avg.min_03, label_kospi_min_03);
                label_kospi_min_05.Text = print(avg.min_05, label_kospi_min_05);
                label_kospi_min_10.Text = print(avg.min_10, label_kospi_min_10);
                label_kospi_min_20.Text = print(avg.min_20, label_kospi_min_20);
                label_kospi_min_30.Text = print(avg.min_30, label_kospi_min_30);
                label_kospi_min_60.Text = print(avg.min_60, label_kospi_min_60);
                label_kospi_day_03.Text = print(avg.day_03, label_kospi_day_03);
                label_kospi_day_05.Text = print(avg.day_05, label_kospi_day_05);
                label_kospi_day_10.Text = print(avg.day_10, label_kospi_day_10);
                label_kospi_day_20.Text = print(avg.day_20, label_kospi_day_20);
                label_kospi_day_40.Text = print(avg.day_40, label_kospi_day_40);
                label_kospi_day_60.Text = print(avg.day_60, label_kospi_day_60);

                avg = Form1.AVG_jisu[1];
                label_kosdaq_min_03.Text = print(avg.min_03, label_kosdaq_min_03);
                label_kosdaq_min_05.Text = print(avg.min_05, label_kosdaq_min_05);
                label_kosdaq_min_10.Text = print(avg.min_10, label_kosdaq_min_10);
                label_kosdaq_min_20.Text = print(avg.min_20, label_kosdaq_min_20);
                label_kosdaq_min_30.Text = print(avg.min_30, label_kosdaq_min_30);
                label_kosdaq_min_60.Text = print(avg.min_60, label_kosdaq_min_60);
                label_kosdaq_day_03.Text = print(avg.day_03, label_kosdaq_day_03);
                label_kosdaq_day_05.Text = print(avg.day_05, label_kosdaq_day_05);
                label_kosdaq_day_10.Text = print(avg.day_10, label_kosdaq_day_10);
                label_kosdaq_day_20.Text = print(avg.day_20, label_kosdaq_day_20);
                label_kosdaq_day_40.Text = print(avg.day_40, label_kosdaq_day_40);
                label_kosdaq_day_60.Text = print(avg.day_60, label_kosdaq_day_60);
            }

            string print(bool UD, Label label)
            {
                if (UD)
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

        private void CB_use_CheckedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            CheckBox CB = sender as CheckBox;
            if (CB.Checked)
            {
                CB.Text = "■" + CB.Text.Substring(1);
            }
            else
            {
                CB.Text = "□" + CB.Text.Substring(1);
            }
        }

        private void CB_UD_CheckedChanged(object sender, EventArgs e)
        {
            Form1.form1.체크박스_비프(sender);

            CheckBox CB = sender as CheckBox;
            if (CB.Checked)
            {
                CB.Text = "초과";
                CB.ForeColor = Color.Red;
            }
            else
            {
                CB.Text = "이하";
                CB.ForeColor = Color.Black;
            }
        }

        private void BT_설정저장_Click(object sender, EventArgs e)
        {
            Form1.form1.Form_Top_Most();
            Form1.MBC_sender = "BT_계좌설정저장";
            Form1.중요메세지("계좌설정", "계좌설정 저장 하시겠습니까?");
        }
    }
}
