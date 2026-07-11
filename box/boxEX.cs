using System;
using System.Windows.Forms;

namespace 지니64.messgebox
{
    public partial class boxEX : UserControl
    {
        public static boxEX box;

        public boxEX()
        {
            box = this;
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
          //  close();
        }

        void close()
        {
             Helper.안전한_UI_업데이트(Form1.form1, () =>
            {
                if (Form1.form1.Contains(box))
                {
                    Form1.form1.Controls.Remove(box);
                }
            });
        }

        private void boxEX_Click(object sender, EventArgs e)
        {
    //        box.BringToFront();
        }

        private void LB_text_Click(object sender, EventArgs e)
        {
        //    box.BringToFront();
        }
    }
}
