using System;
using System.Windows.Forms;

namespace 지니_64.messgebox
{
    public partial class boxEX : UserControl
    {
        public static boxEX box;

        public boxEX()
        {
            box = this;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            close();
        }

        void close()
        {
            Form1.form1.Invoke((MethodInvoker)delegate ()
            {
                if (Form1.form1.Contains(box))
                {
                    Form1.form1.Controls.Remove(box);
                }
            });
        }

        private void boxEX_Click(object sender, EventArgs e)
        {
            box.BringToFront();
        }

        private void LB_text_Click(object sender, EventArgs e)
        {
            box.BringToFront();
        }
    }
}
