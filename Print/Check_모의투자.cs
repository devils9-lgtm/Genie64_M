using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64
{
    internal class Check_모의투자
    {
        public static void Print()
        {
            // ==========================================
            // 1. [데이터 세팅] 폼 생성 여부와 무관하게 메모리(GenieConfig) 값부터 먼저 수정합니다.
            // ==========================================
            if (GenieConfig.checkBox_Simulation)
            {
                GenieConfig.CB_신용주문_신규_A = false;
                GenieConfig.CB_신용주문_신규_B = false;
                GenieConfig.CB_신용주문_신규_C = false;

                GenieConfig.CB_NXT = false;
                GenieConfig.CB_NXT_매수금지 = false;
                GenieConfig.CB_NXT_손실제한 = false;

                GenieConfig.CB_중간가주문 = false;
                GenieConfig.CB_ETF매입비제외 = false;

                GenieConfig.CB_신용_주문사용 = false;
                GenieConfig.CB_신용_현금우선사용 = false;
                GenieConfig.CB_신용_신용우선사용 = false;
                GenieConfig.CB_신용_현신별도사용 = false;
                GenieConfig.TB_신용_신용증거금비율 = 100;
            }

            // ==========================================
            // 2. [기본매매 UI 세팅] 폼이 생성되어 있을 때만 접근 (Null 에러 방지)
            // ==========================================
            if (Form_Basic.form != null && !Form_Basic.form.IsDisposed)
            {
                if (GenieConfig.checkBox_Simulation)
                {
                    Form_Basic.form.CB_신용주문_신규_A.Enabled = false;
                    Form_Basic.form.CB_신용주문_신규_B.Enabled = false;
                    Form_Basic.form.CB_신용주문_신규_C.Enabled = false;
                }

                Form_Basic.form.CB_신용주문_신규_A.Checked = GenieConfig.CB_신용주문_신규_A;
                Form_Basic.form.CB_신용주문_신규_B.Checked = GenieConfig.CB_신용주문_신규_B;
                Form_Basic.form.CB_신용주문_신규_C.Checked = GenieConfig.CB_신용주문_신규_C;
            }

            // ==========================================
            // 3. [기능설정 UI 세팅] 폼이 생성되어 있을 때만 접근 (Null 에러 방지)
            // ==========================================
            if (Form_Function.form != null && !Form_Function.form.IsDisposed)
            {
                if (GenieConfig.checkBox_Simulation)
                {
                    Form_Function.form.CB_NXT.Enabled = false;
                    Form_Function.form.CB_NXT_매수금지.Enabled = false;
                    Form_Function.form.CB_NXT_손실제한.Enabled = false;

                    Form_Function.form.CB_중간가주문.Enabled = false;
                    Form_Function.form.CB_ETF매입비제외.Enabled = false;

                    Form_Function.form.CB_신용_주문사용.Enabled = false;
                    Form_Function.form.CB_신용_현금우선사용.Enabled = false;
                    Form_Function.form.CB_신용_신용우선사용.Enabled = false;
                    Form_Function.form.CB_신용_현신별도사용.Enabled = false;
                    Form_Function.form.TB_신용_신용증거금비율.Enabled = false;

                    Form_Function.form.label_신용.Enabled = false;
                    Form_Function.form.label_신용_1.Enabled = false;
                    Form_Function.form.label_신용_2.Enabled = false;

                    Form_Function.form.label_신용_1.Text = "모의투자";
                    Form_Function.form.label_신용_2.Text = "신용불가";

                    Form_Function.form.경고_1.Hide();
                    Form_Function.form.경고_2.Hide();
                    Form_Function.form.경고_3.Hide();
                    Form_Function.form.경고_4.Hide();
                    Form_Function.form.경고_5.Hide();
                    Form_Function.form.경고_6.Hide();
                    Form_Function.form.경고_7.Hide();
                }

                Form_Function.form.CB_NXT.Checked = GenieConfig.CB_NXT;
                Form_Function.form.CB_NXT_매수금지.Checked = GenieConfig.CB_NXT_매수금지;
                Form_Function.form.CB_NXT_손실제한.Checked = GenieConfig.CB_NXT_손실제한;

                Form_Function.form.CB_중간가주문.Checked = GenieConfig.CB_중간가주문;
                Form_Function.form.CB_ETF매입비제외.Checked = GenieConfig.CB_ETF매입비제외;

                Form_Function.form.CB_신용_주문사용.Checked = GenieConfig.CB_신용_주문사용;
                Form_Function.form.CB_신용_현금우선사용.Checked = GenieConfig.CB_신용_현금우선사용;
                Form_Function.form.CB_신용_신용우선사용.Checked = GenieConfig.CB_신용_신용우선사용;
                Form_Function.form.CB_신용_현신별도사용.Checked = GenieConfig.CB_신용_현신별도사용;
                Form_Function.form.TB_신용_신용증거금비율.Text = $"{GenieConfig.TB_신용_신용증거금비율} %";
            }
        }

    }
}


