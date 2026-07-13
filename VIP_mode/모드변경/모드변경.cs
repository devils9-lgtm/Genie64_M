using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 지니64.VIP_mode.모드변경
{
    internal class 모드변경
    {
        /// <summary>
        /// VIP 모드 로그인 시 잔고 데이터그리드뷰에 '증권사' 컬럼을 추가합니다.
        /// </summary>
        /// <param name="잔고데이터그리드뷰">Form에 있는 잔고 DataGridView 객체</param>
        public static void VIP증권사컬럼추가(DataGridView 잔고데이터그리드뷰)
        {
            // 🚀 만들어두신 완벽한 크로스 스레드 방어 유틸리티 사용
            Helper.안전한_UI_업데이트(잔고데이터그리드뷰, () =>
            {
                // 1. 방어 코드: 이미 '증권사' 컬럼이 만들어져 있다면 중복해서 만들지 않음
                if (잔고데이터그리드뷰.Columns.Contains("증권사"))
                {
                    return;
                }

                // 2. 새 텍스트 컬럼 생성 및 기본 설정
                DataGridViewTextBoxColumn brokerColumn = new DataGridViewTextBoxColumn();
                brokerColumn.Name = "증권사";
                brokerColumn.HeaderText = "증";
                brokerColumn.Width = 30;
                brokerColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // 3. 10번째 셀까지 고정(Frozen)되어 있으므로 새 컬럼도 고정 속성 부여
                brokerColumn.Frozen = true;

                // 4. 컬럼을 6번째 위치(인덱스 5)에 삽입
                if (잔고데이터그리드뷰.Columns.Count >= 5)
                {
                    잔고데이터그리드뷰.Columns.Insert(5, brokerColumn);
                }
                else
                {
                    잔고데이터그리드뷰.Columns.Add(brokerColumn);
                }
            });
        }
    }
}