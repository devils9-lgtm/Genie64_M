
namespace 지니_64.box
{
    partial class MBC
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.LB_title = new System.Windows.Forms.Label();
            this.BT_적용 = new System.Windows.Forms.Button();
            this.LB_text = new System.Windows.Forms.Label();
            this.BT_취소 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LB_title
            // 
            this.LB_title.Font = new System.Drawing.Font("굴림", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_title.ForeColor = System.Drawing.Color.Red;
            this.LB_title.Location = new System.Drawing.Point(1, 2);
            this.LB_title.Name = "LB_title";
            this.LB_title.Size = new System.Drawing.Size(501, 17);
            this.LB_title.TabIndex = 3;
            this.LB_title.Text = "* 설정 알람 *";
            this.LB_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LB_title.Click += new System.EventHandler(this.LB_title_Click);
            // 
            // BT_적용
            // 
            this.BT_적용.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BT_적용.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BT_적용.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.BT_적용.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BT_적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_적용.Location = new System.Drawing.Point(147, 126);
            this.BT_적용.Name = "BT_적용";
            this.BT_적용.Size = new System.Drawing.Size(100, 25);
            this.BT_적용.TabIndex = 5;
            this.BT_적용.Text = "적용";
            this.BT_적용.UseVisualStyleBackColor = false;
            this.BT_적용.Click += new System.EventHandler(this.BT_적용_Click);
            // 
            // LB_text
            // 
            this.LB_text.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_text.Location = new System.Drawing.Point(1, 20);
            this.LB_text.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.LB_text.Name = "LB_text";
            this.LB_text.Size = new System.Drawing.Size(501, 104);
            this.LB_text.TabIndex = 4;
            this.LB_text.Text = "-";
            this.LB_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LB_text.Click += new System.EventHandler(this.LB_text_Click);
            // 
            // BT_취소
            // 
            this.BT_취소.BackColor = System.Drawing.SystemColors.Control;
            this.BT_취소.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BT_취소.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.BT_취소.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BT_취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_취소.Location = new System.Drawing.Point(254, 126);
            this.BT_취소.Name = "BT_취소";
            this.BT_취소.Size = new System.Drawing.Size(100, 25);
            this.BT_취소.TabIndex = 5;
            this.BT_취소.Text = "취소";
            this.BT_취소.UseVisualStyleBackColor = false;
            this.BT_취소.Click += new System.EventHandler(this.BT_취소_Click);
            // 
            // MBC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.LB_title);
            this.Controls.Add(this.BT_취소);
            this.Controls.Add(this.BT_적용);
            this.Controls.Add(this.LB_text);
            this.DoubleBuffered = true;
            this.Name = "MBC";
            this.Size = new System.Drawing.Size(503, 155);
            this.Load += new System.EventHandler(this.MBC_Load);
            this.Click += new System.EventHandler(this.MBC_Click);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button BT_적용;
        public System.Windows.Forms.Label LB_text;
        public System.Windows.Forms.Button BT_취소;
        public System.Windows.Forms.Label LB_title;
    }
}
