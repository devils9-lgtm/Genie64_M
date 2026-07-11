using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace 지니64
{
    class TextValue
    {
        
        public static void TypingOnlyNumber(object sender, KeyPressEventArgs e, bool includePoint, bool includeMinus)
        {
            // ==========================================================
            // 💡 [1단계] 문자 필터링 (가장 가벼운 조건문으로 통합)
            // ==========================================================
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                if (includePoint && e.KeyChar == '.') { /* 통과 */ }
                else if (includeMinus && e.KeyChar == '-') { /* 통과 */ }
                else
                {
                    e.Handled = true; // 허용되지 않은 문자 즉시 차단
                    return;
                }
            }

            // ==========================================================
            // 💡 [2단계] 문맥 검사 (Reflection 제거 & TextBoxBase 통합!)
            // ==========================================================
            // [핵심] TextBox와 MaskedTextBox는 모두 TextBoxBase 소속이므로 한 번에 처리 가능!
            if (!(sender is TextBoxBase tbBase)) return;

            // --- 소수점(.) 로직 ---
            if (includePoint && e.KeyChar == '.')
            {
                // 전체 선택된 상태라면 덮어쓰기 허용
                bool isAllSelected = (tbBase.SelectionLength == tbBase.TextLength && tbBase.TextLength > 0);

                if (!isAllSelected)
                {
                    // Trim() 대신 TextLength 검사로 메모리 낭비 제로화, Contains로 가독성 향상
                    if (tbBase.TextLength == 0 || tbBase.Text.Contains('.'))
                    {
                        e.Handled = true;
                        return; // 차단 시 불필요한 아래 연산 스킵
                    }
                }
            }

            // --- 마이너스(-) 로직 ---
            if (includeMinus && e.KeyChar == '-')
            {
                bool isAllSelected = (tbBase.SelectionLength == tbBase.TextLength);

                if (!isAllSelected)
                {
                    // 1. 커서가 맨 앞(0)이 아닌 경우 차단 (숫자 중간에 '-' 입력 방지)
                    // 2. 이미 '-' 부호가 존재하는 경우 차단
                    if (tbBase.SelectionStart != 0 || tbBase.Text.Contains('-'))
                    {
                        e.Handled = true;
                    }
                }
            }
        }


        public static void TextBox_빨파검(object sender)
        {
            // 1. 방어막: 보낸 놈이 TextBox가 아니거나, 텍스트가 텅 비었으면 즉시 탈출!
            if (!(sender is TextBox TB) || string.IsNullOrEmpty(TB.Text)) return;

            Color 목표색상;

            // 2. 검색 최적화: Contains 전체검색 대신 -> StartsWith로 맨 앞 1글자만 0.001초 만에 확인
            if (TB.Text.StartsWith("-"))
            {
                목표색상 = Color.Blue;
            }
            // (혹시 소수점 "0.00"으로 찍히는 텍스트박스가 있을까 봐 안전장치 추가)
            else if (TB.Text.Equals("0") || TB.Text.Equals("0.00"))
            {
                목표색상 = Color.Black;
            }
            else
            {
                목표색상 = Color.Red;
            }

            // 3. ⭐️ UI 최적화의 핵심: 현재 색상이 '목표색상'과 다를 때만 붓을 들어서 칠함!
            if (TB.ForeColor != 목표색상)
            {
                TB.ForeColor = 목표색상;
            }
        }



        public static void 숫자콤마넣기_TextChanged(object sender)
        {
            // 1. 방어막: 텍스트박스가 아니거나 값이 비어있으면 즉시 탈출!
            if (!(sender is TextBox textbox) || string.IsNullOrEmpty(textbox.Text)) return;

            // 2. 가벼운 문자열 치환 (메모리 낭비 최소화)
            string text = textbox.Text.Replace(",", "").Replace("-", "");

            // 3. 무거운 Split 대신 IndexOf와 Substring 사용 (배열을 만들지 않아 초고속)
            int dotIndex = text.IndexOf('.');
            if (dotIndex >= 0)
            {
                text = text.Substring(0, dotIndex);
            }

            // 4. double(실수) 대신 long(정수) 사용 (CPU 연산 속도 대폭 상승)
            if (long.TryParse(text, out long num))
            {
                // 변환된 콤마 문자열을 먼저 변수에 담아둡니다.
                string formattedText = string.Format("{0:#,##0}", num);

                // ⭐️ 5. [핵심 최적화] 기존 텍스트와 새로 찍을 텍스트가 "다를 때만" 덮어씀!
                // (이 조건이 없으면 Text가 바뀔 때마다 TextChanged 이벤트가 무한 반복해서 실행됨)
                if (textbox.Text != formattedText)
                {
                    textbox.Text = formattedText;

                    // 커서를 뒤로 보내는 것도 진짜로 글자가 바뀌었을 때만 실행
                    textbox.SelectionStart = textbox.TextLength;
                    textbox.SelectionLength = 0;
                }
            }
        }

    
        public static void TextBox_양실수만(object sender)
        {
            // 1. 방어막: TextBox가 아니거나 텍스트가 텅 비었으면 즉시 탈출
            if (!(sender is TextBox tb) || string.IsNullOrEmpty(tb.Text)) return;

            string originalText = tb.Text;
            string cleanedText = originalText;

            // 2. 마이너스(-) 기호 완벽 제거 (StartsWith 대신 Replace로 혹시 모를 중간 부호도 싹 제거)
            if (cleanedText.Contains("-"))
            {
                cleanedText = cleanedText.Replace("-", "");
            }

            // 3. 무거운 Split 대신 IndexOf 활용 (배열을 만들지 않아 메모리 쓰레기 제로)
            int dotIndex = cleanedText.IndexOf('.');
            if (dotIndex >= 0)
            {
                // 소수점이 있으면 소수점 앞자리까지만 싹둑 자름
                cleanedText = cleanedText.Substring(0, dotIndex);
            }

            // 4. 불필요한 0 제거 (예: "01" -> "1", "00" -> "0")
            // long.TryParse를 쓰면 01을 1로 아주 빠르고 자연스럽게 바꿔줍니다.
            if (cleanedText.Length > 0 && long.TryParse(cleanedText, out long num))
            {
                cleanedText = num.ToString();
            }
            else
            {
                // 만약 "-" 나 "." 만 입력해서 글자가 다 날아갔다면 깡통으로 만듦
                cleanedText = "";
            }

            // ⭐️ 5. [핵심 최적화] 값이 진짜로 바뀌었을 때만 UI(텍스트박스) 업데이트!
            if (originalText != cleanedText)
            {
                tb.Text = cleanedText;

                // 텍스트를 강제로 바꿨으니 커서를 맨 뒤로 보내서 타자 치기 편하게 해줌
                tb.SelectionStart = tb.TextLength;
                tb.SelectionLength = 0;
            }
        }

       

        public static void TextBox_빨파검_소수2자리제한(object sender)
        {
            // 1. 방어막: TextBox가 아니거나 내용이 텅 비었으면 즉시 탈출 (오류 방지)
            if (!(sender is TextBox tb) || string.IsNullOrEmpty(tb.Text)) return;

            string currentText = tb.Text;
            Color 목표색상;

            // ==========================================================
            // 💡 [1단계] 색상 결정 및 최적화 업데이트
            // ==========================================================
            // Contains 전체 검색 대신 StartsWith로 첫 글자만 빛의 속도로 확인
            if (currentText.StartsWith("-"))
            {
                목표색상 = Color.Blue;
            }
            else if (currentText == "0" || currentText == "0.00") // 소수점 0.00일 때도 검은색 처리
            {
                목표색상 = Color.Black;
            }
            else
            {
                목표색상 = Color.Red;
            }

            // [최적화 핵심 1] 목표 색상과 현재 색상이 다를 때만 붓칠을 한다! (화면 깜빡임 방지)
            if (tb.ForeColor != 목표색상)
            {
                tb.ForeColor = 목표색상;
            }


            // ==========================================================
            // 💡 [2단계] 소수점 텍스트 변환 및 무한루프 방지
            // ==========================================================
            string newText = currentText;

            if (currentText.StartsWith("-"))
            {
                if (currentText.Length > 1)
                {
                    // 마이너스 기호를 떼고 숫자만 '글자변환'에 넣은 뒤 다시 합침
                    string numPart = currentText.Substring(1);
                    newText = "-" + 글자변환(numPart);
                }
            }
            else
            {
                newText = 글자변환(currentText);
            }

            // [최적화 핵심 2] 글자변환 결과가 현재 텍스트와 "진짜로 다를 때만" 덮어쓴다!
            // (이게 없으면 TextChanged 이벤트가 무한 반복되며 CPU를 잡아먹습니다)
            if (currentText != newText)
            {
                tb.Text = newText;

                // 커서를 맨 뒤로 보내는 것도 텍스트가 실제로 변경되었을 때만 실행
                tb.SelectionStart = tb.TextLength;
                tb.SelectionLength = 0;
            }
        }



        public static void TextBox_음수만입력_소수2자리제한(object sender)
        {
            // 1. 방어막: TextBox가 아니거나 내용이 비어있으면 즉시 탈출
            if (!(sender is TextBox tb) || string.IsNullOrEmpty(tb.Text)) return;

            string currentText = tb.Text;
            string newText = currentText;

            // ==========================================================
            // 💡 [1단계] 문자열 판별 및 글자 변환 (연산 압축)
            // ==========================================================
            if (!currentText.StartsWith("-"))
            {
                // 마이너스가 없으면: 앞에 강제로 '-'를 붙이고 숫자 부분 글자변환
                newText = "-" + 글자변환(currentText);
            }
            else
            {
                // 이미 마이너스가 있으면: '-' 뒤의 숫자만 떼서 글자변환 후 다시 조립
                if (currentText.Length > 1)
                {
                    string numPart = currentText.Substring(1);
                    newText = "-" + 글자변환(numPart);
                }
                // 만약 사용자가 "-" 딱 한 글자만 친 상태라면 굳이 변환 안 함
            }

            // ==========================================================
            // 💡 [2단계] 최적화 핵심: 실제 값이 변했을 때만 UI 업데이트!
            // ==========================================================
            // (이 조건문 하나가 저사양 PC의 텍스트박스 버벅임을 완전히 없애줍니다)
            if (currentText != newText)
            {
                tb.Text = newText;

                // 커서를 맨 뒤로 보내는 것도 텍스트가 진짜로 바뀌었을 때만 1번 실행!
                tb.SelectionStart = tb.TextLength;
                tb.SelectionLength = 0;
            }
        }


        public static void TextBox_소수자리제한(object sender)
        {
            // 1. 방어막: 텍스트박스가 아니거나 값이 비어있으면 0.001초 만에 탈출! (에러 원천 차단)
            if (!(sender is TextBox tb) || string.IsNullOrEmpty(tb.Text)) return;

            string currentText = tb.Text;
            string newText = currentText;

            // ==========================================================
            // 💡 [1단계] UI(화면)를 건드리지 않고 메모리 안에서 글자만 조립
            // ==========================================================
            if (currentText.StartsWith("-"))
            {
                if (currentText.Length > 1)
                {
                    // '-' 부호를 뺀 나머지 숫자만 글자변환에 넣고 다시 '-'를 붙임
                    string numPart = currentText.Substring(1);
                    newText = "-" + 글자변환(numPart);
                }
            }
            else
            {
                newText = 글자변환(currentText);
            }

            // ==========================================================
            // 💡 [2단계] 최적화 핵심: 조립된 결과가 현재 텍스트와 "다를 때만" 화면에 그린다!
            // ==========================================================
            // (이 한 줄이 TextChanged 이벤트 무한 반복과 화면 깜빡임을 완벽히 막아줍니다)
            if (currentText != newText)
            {
                tb.Text = newText;

                // 텍스트를 강제로 바꿨으니 타자 치기 편하게 커서를 맨 뒤로 보냄
                tb.SelectionStart = tb.TextLength;
                tb.SelectionLength = 0;
            }
        }


        public static void TextBox_양수소수자리제한(object sender)
        {
            // 1. 방어막: 텍스트박스가 아니거나 값이 비어있으면 0.001초 만에 탈출! (에러 방지)
            if (!(sender is TextBox tb) || string.IsNullOrEmpty(tb.Text)) return;

            string currentText = tb.Text;
            string newText = currentText;

            // ==========================================================
            // 💡 [1단계] 문자열 다듬기 (메모리에서 조용히 처리)
            // ==========================================================
            // "양수"만 허용하므로, 문자열 어디에든 마이너스(-)가 보이면 흔적도 없이 싹 제거합니다.
            if (newText.Contains("-"))
            {
                newText = newText.Replace("-", "");
            }

            // 마이너스를 지우고 남은 글자가 있다면 '글자변환' 로직을 태웁니다.
            // (쓸데없는 이중 if문 제거하여 속도 향상)
            if (newText.Length > 0)
            {
                newText = 글자변환(newText);
            }

            // ==========================================================
            // 💡 [2단계] 최적화 핵심: 최종 결과가 "다를 때만" 화면을 다시 그린다!
            // ==========================================================
            // (이 한 줄이 TextChanged 무한루프와 화면 껌뻑임을 완벽히 차단합니다)
            if (currentText != newText)
            {
                tb.Text = newText;

                // 값이 강제로 변경되었으므로, 타자 치기 편하게 커서를 맨 뒤로 옯겨줍니다.
                tb.SelectionStart = tb.TextLength;
                tb.SelectionLength = 0;
            }
        }


      

        public static string 글자변환(string text)
        {
            // 방어막: 빈 값이 들어오면 즉시 0 반환
            if (string.IsNullOrEmpty(text)) return "0";

            // 무거운 Split 대신 점(.)의 위치만 빛의 속도로 찾습니다! (메모리 낭비 제로)
            int dotIndex = text.IndexOf('.');

            // ==========================================================
            // 💡 1. 소수점이 없는 경우 (정수)
            // ==========================================================
            if (dotIndex < 0)
            {
                // int 대신 long을 써서 수백억, 수천억 단위의 큰 숫자도 에러 없이 완벽 소화!
                if (long.TryParse(text, out long num))
                {
                    return num.ToString(); // "01" -> "1" 처럼 앞의 쓸데없는 0 자동 제거
                }
                return text;
            }

            // ==========================================================
            // 💡 2. 소수점이 있는 경우
            // ==========================================================
            // 점을 기준으로 앞(정수)과 뒤(소수)를 가위로 싹둑 자릅니다.
            string intPart = text.Substring(0, dotIndex);
            string decPart = text.Substring(dotIndex + 1);

            // [버그 픽스] 소수점 뒤가 2자리를 '초과'하면 무조건 2자리까지만 자르도록 완벽 방어!
            if (decPart.Length > 2)
            {
                decPart = decPart.Substring(0, 2);
            }

            // [디테일 보정] 정수 부분에 불필요한 0이 붙어있다면 제거 (예: "01.23" -> "1.23")
            if (intPart.Length > 0 && long.TryParse(intPart, out long numInt))
            {
                intPart = numInt.ToString();
            }
            else if (intPart == "")
            {
                // 사용자가 냅다 점(.)부터 찍었을 때 (".5") -> "0.5"로 예쁘게 보정해줌
                intPart = "0";
            }

            // 가공된 정수와 소수를 합쳐서 반환!
            return intPart + "." + decPart;
        }

        public static string TextBox_0입력(int combobox, string text)
        {
            // 💡 임시 변수 할당 없이 조건에 따라 즉시 값을 던집니다! (초고속)
            return (combobox < 5) ? text : "0";
        }
    }
}
