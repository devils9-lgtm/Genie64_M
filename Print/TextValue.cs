using System.Drawing;
using System.Windows.Forms;

namespace 지니_64
{
    class TextValue
    {
        public static void TypingOnlyNumber(object sender, KeyPressEventArgs e, bool includePoint, bool includeMinus)
        {
            bool isValidInput = false;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                if (includePoint == true) { if (e.KeyChar == '.') isValidInput = true; }
                if (includeMinus == true) { if (e.KeyChar == '-') isValidInput = true; }

                if (isValidInput == false) e.Handled = true;
            }

            string type = sender.GetType().ToString();

            if (type.Contains("MaskedTextBox"))
            {
                if (includePoint == true)
                {
                    if (e.KeyChar == '.' && (string.IsNullOrEmpty((sender as MaskedTextBox).Text.Trim()) || (sender as MaskedTextBox).Text.IndexOf('.') > -1)) e.Handled = true;
                }
                if (includeMinus == true)
                {
                    if (e.KeyChar == '-' && (!string.IsNullOrEmpty((sender as MaskedTextBox).Text.Trim()) || (sender as MaskedTextBox).Text.IndexOf('-') > -1)) e.Handled = true;
                }
            }
            else
            {
                if (includePoint == true)
                {
                    if (e.KeyChar == '.' && (string.IsNullOrEmpty((sender as TextBox).Text.Trim()) || (sender as TextBox).Text.IndexOf('.') > -1)) e.Handled = true;
                }
                if (includeMinus == true)
                {
                    if (e.KeyChar == '-' && (!string.IsNullOrEmpty((sender as TextBox).Text.Trim()) || (sender as TextBox).Text.IndexOf('-') > -1)) e.Handled = true;
                }
            }
        }

        public static void TextBox_빨파검(object sender)
        {
            TextBox TB = sender as TextBox;

            if (TB.Text.Contains("-"))
            {
                TB.ForeColor = Color.Blue;
            }
            else
            {
                if (TB.Text.Equals("0"))
                {
                    TB.ForeColor = Color.Black;
                }
                else
                {
                    TB.ForeColor = Color.Red;
                }
            }
        }

        public static void 숫자콤마넣기_TextChanged(object sender)
        {
            TextBox textbox = sender as TextBox;
            string text = textbox.Text.Replace(",", "");
            text = text.Replace("-", "");
            if (text.Contains(".")) text = text.Split('.')[0];

            double num = 0;
            if (double.TryParse(text, out num))//숫자형태의 값일 때만 처리
            {
                textbox.Text = string.Format("{0:#,##0}", num);
                textbox.SelectionStart = textbox.TextLength;//커서를 항상 글자 제일 뒤로 위치시킴
                textbox.SelectionLength = 0;
            }
        }

        public static void TextBox_양실수만(object sender)
        {
            TextBox TB = sender as TextBox;
            string text = TB.Text;

            if (text.StartsWith("-"))
            {
                TB.Text = text.Substring(1);
            }
            else
            {
                if (text.Length > 1)
                {
                    if (text.Contains("."))
                    {
                        string[] 자리제한_배열 = text.Split('.');
                        int.TryParse(자리제한_배열[0], out int num_0);
                        TB.Text = num_0.ToString();
                    }
                    else
                    {
                        int.TryParse(text, out int num);
                        TB.Text = num.ToString();
                    }
                }

                TB.Select(TB.Text.Length, 0);
            }
        }

        public static void TextBox_빨파검_소수2자리제한(object sender)
        {
            TextBox TB = sender as TextBox;

            if (TB.Text.Contains("-"))
            {
                TB.ForeColor = Color.Blue;
            }
            else
            {
                if (TB.Text.Equals("0"))
                {
                    TB.ForeColor = Color.Black;
                }
                else
                {
                    TB.ForeColor = Color.Red;
                }
            }

            string text = TB.Text;
            if (text.StartsWith("-"))
            {
                if (text.Length > 1)
                {
                    text = text.Substring(1);
                    TB.Text = "-" + 글자변환(text);
                }
            }
            else
            {
                if (text.Length > 0)
                    TB.Text = 글자변환(text);
            }

            TB.Select(TB.Text.Length, 0);
        }

        public static void TextBox_음수만입력_소수2자리제한(object sender)
        {
            TextBox TB = sender as TextBox;

            if (!TB.Text.StartsWith("-"))
            {
                TB.Text = TB.Text.Insert(0, "-");
                TB.SelectionStart = TB.Text.Length;
            }
            else
            {
                string text = TB.Text;
                if (text.StartsWith("-"))
                {
                    if (text.Length > 1)
                    {
                        text = text.Substring(1);
                        TB.Text = "-" + 글자변환(text);
                    }
                }
                else
                {
                    if (text.Length > 0)
                        TB.Text = 글자변환(text);
                }

                TB.Select(TB.Text.Length, 0);
            }
        }

        public static void TextBox_소수자리제한(object sender)
        {
            TextBox TB = sender as TextBox;
            string text = TB.Text;
            if (text.StartsWith("-"))
            {
                if (text.Length > 1)
                {
                    text = text.Substring(1);
                    TB.Text = "-" + 글자변환(text);
                }
            }
            else
            {
                if (text.Length > 0)
                    TB.Text = 글자변환(text);
            }

            TB.Select(TB.Text.Length, 0);
        }

        public static void TextBox_양수소수자리제한(object sender)
        {

            TextBox TB = sender as TextBox;
            string text = TB.Text;

            if (text.StartsWith("-"))
            {
                TB.Text = text.Substring(1);

            }
            else
            {
                if (text.Length > 1)
                {
                    if (text.Length > 0)
                        TB.Text = 글자변환(text);
                }

                TB.Select(TB.Text.Length, 0);
            }
        }

        private static string 글자변환(string text)
        {
            string result = text;

            if (text.Contains("."))
            {
                string[] 자리제한_배열 = text.Split('.');

                if (자리제한_배열[1].Length > 0)
                {
                    if (자리제한_배열[1].Length == 3) 자리제한_배열[1] = 자리제한_배열[1].Substring(0, 2);
                    result = 자리제한_배열[0] + "." + 자리제한_배열[1].ToString();
                }
            }
            else
            {
                int.TryParse(text, out int num);
                result = num.ToString();
            }

            return result;
        }

        public static string TextBox_0입력(int combobox, string text)
        {
            string result = "0";

            if (combobox < 5)
            {
                result = text;
            }

            return result;
        }
    }
}
