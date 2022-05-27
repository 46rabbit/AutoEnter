
namespace auto_Enter
{
    partial class NotifyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sender = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.msgContents = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sender
            // 
            this.sender.AutoSize = true;
            this.sender.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.sender.Location = new System.Drawing.Point(12, 30);
            this.sender.Name = "sender";
            this.sender.Size = new System.Drawing.Size(97, 19);
            this.sender.TabIndex = 0;
            this.sender.Text = "이선향 선생님";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(532, 19);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(493, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(39, 19);
            this.panel2.TabIndex = 0;
            // 
            // msgContents
            // 
            this.msgContents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.msgContents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.msgContents.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.msgContents.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.msgContents.Location = new System.Drawing.Point(16, 52);
            this.msgContents.Multiline = true;
            this.msgContents.Name = "msgContents";
            this.msgContents.Size = new System.Drawing.Size(504, 247);
            this.msgContents.TabIndex = 0;
            this.msgContents.Text = "사회적 거리두기 3단계에서 연무중 학생들의 의견을 수렴하여 본교 등교 원칙을 정하고자 합니다. 아래 내용을 잘 읽어 보시고, 의견을 표시해주고 \'" +
    "투표 완료\'라고 댓글을 달아주세요. ";
            this.msgContents.MouseHover += new System.EventHandler(this.msgContents_MouseHover);
            // 
            // NotifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 311);
            this.Controls.Add(this.msgContents);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sender);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NotifyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NotifyForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sender;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox msgContents;
    }
}