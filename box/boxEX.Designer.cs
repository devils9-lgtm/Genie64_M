
namespace 지니64.messgebox
{
    partial class boxEX
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
            this.알람 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.LB_text = new System.Windows.Forms.Label();
            this.LB_time = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // 알람
            // 
            this.알람.AutoSize = true;
            this.알람.Font = new System.Drawing.Font("굴림", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.알람.Location = new System.Drawing.Point(223, 2);
            this.알람.Name = "알람";
            this.알람.Size = new System.Drawing.Size(58, 17);
            this.알람.TabIndex = 0;
            this.알람.Text = "* 알람";
            this.알람.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveCaption;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(224, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "닫기";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // LB_text
            // 
            this.LB_text.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_text.Location = new System.Drawing.Point(1, 22);
            this.LB_text.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.LB_text.Name = "LB_text";
            this.LB_text.Size = new System.Drawing.Size(548, 85);
            this.LB_text.TabIndex = 0;
            this.LB_text.Text = "-\r\n-\r\n-\r\n-";
            this.LB_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LB_text.Click += new System.EventHandler(this.LB_text_Click);
            // 
            // LB_time
            // 
            this.LB_time.AutoSize = true;
            this.LB_time.Font = new System.Drawing.Font("굴림", 12.75F, System.Drawing.FontStyle.Bold);
            this.LB_time.Location = new System.Drawing.Point(285, 2);
            this.LB_time.Name = "LB_time";
            this.LB_time.Size = new System.Drawing.Size(26, 17);
            this.LB_time.TabIndex = 0;
            this.LB_time.Text = "10";
            this.LB_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boxEX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.LB_time);
            this.Controls.Add(this.알람);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LB_text);
            this.DoubleBuffered = true;
            this.Location = new System.Drawing.Point(600, 150);
            this.Name = "boxEX";
            this.Size = new System.Drawing.Size(550, 141);
            this.Click += new System.EventHandler(this.boxEX_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label 알람;
        public System.Windows.Forms.Label LB_text;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label LB_time;
    }
}
