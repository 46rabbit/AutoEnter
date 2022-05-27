
namespace auto_Enter
{
    partial class writeTimeTable
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
            this.hourCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.minCombo = new System.Windows.Forms.ComboBox();
            this.classTime = new System.Windows.Forms.TextBox();
            this.restingTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.refreashBtn = new System.Windows.Forms.Button();
            this.timeTableShowView = new auto_Enter.ListViewWithReordering();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.addTableBtn = new System.Windows.Forms.Button();
            this.deleteTableBtn = new System.Windows.Forms.Button();
            this.presetListview = new auto_Enter.ListViewWithReordering();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.insertBtn = new System.Windows.Forms.Button();
            this.takeoutBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.fileName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // hourCombo
            // 
            this.hourCombo.FormattingEnabled = true;
            this.hourCombo.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.hourCombo.Location = new System.Drawing.Point(8, 43);
            this.hourCombo.Name = "hourCombo";
            this.hourCombo.Size = new System.Drawing.Size(70, 20);
            this.hourCombo.TabIndex = 3;
            this.hourCombo.Text = "01";
            this.hourCombo.SelectedIndexChanged += new System.EventHandler(this.timeVal_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Class start time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Time per class";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(357, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Resting time";
            // 
            // minCombo
            // 
            this.minCombo.FormattingEnabled = true;
            this.minCombo.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "56",
            "57",
            "58",
            "59"});
            this.minCombo.Location = new System.Drawing.Point(103, 43);
            this.minCombo.Name = "minCombo";
            this.minCombo.Size = new System.Drawing.Size(70, 20);
            this.minCombo.TabIndex = 9;
            this.minCombo.Text = "00";
            this.minCombo.SelectedIndexChanged += new System.EventHandler(this.timeVal_Changed);
            // 
            // classTime
            // 
            this.classTime.Location = new System.Drawing.Point(232, 42);
            this.classTime.Name = "classTime";
            this.classTime.Size = new System.Drawing.Size(100, 21);
            this.classTime.TabIndex = 10;
            this.classTime.TextChanged += new System.EventHandler(this.timeVal_Changed);
            // 
            // restingTime
            // 
            this.restingTime.Location = new System.Drawing.Point(359, 43);
            this.restingTime.Name = "restingTime";
            this.restingTime.Size = new System.Drawing.Size(100, 21);
            this.restingTime.TabIndex = 11;
            this.restingTime.TextChanged += new System.EventHandler(this.timeVal_Changed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(334, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "H";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(461, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "M";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "H";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(175, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "M";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.refreashBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.hourCombo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.minCombo);
            this.groupBox1.Controls.Add(this.restingTime);
            this.groupBox1.Controls.Add(this.classTime);
            this.groupBox1.Location = new System.Drawing.Point(12, 363);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(481, 106);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Time variables";
            // 
            // refreashBtn
            // 
            this.refreashBtn.Location = new System.Drawing.Point(8, 72);
            this.refreashBtn.Name = "refreashBtn";
            this.refreashBtn.Size = new System.Drawing.Size(75, 23);
            this.refreashBtn.TabIndex = 27;
            this.refreashBtn.Text = "Refresh";
            this.refreashBtn.UseVisualStyleBackColor = true;
            this.refreashBtn.Click += new System.EventHandler(this.refreashBtn_Click);
            // 
            // timeTableShowView
            // 
            this.timeTableShowView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.timeTableShowView.FullRowSelect = true;
            this.timeTableShowView.HideSelection = false;
            this.timeTableShowView.Location = new System.Drawing.Point(12, 12);
            this.timeTableShowView.Name = "timeTableShowView";
            this.timeTableShowView.Size = new System.Drawing.Size(481, 345);
            this.timeTableShowView.TabIndex = 17;
            this.timeTableShowView.UseCompatibleStateImageBehavior = false;
            this.timeTableShowView.View = System.Windows.Forms.View.Details;
            this.timeTableShowView.SizeChanged += new System.EventHandler(this.timeVal_Changed);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Subject";
            this.columnHeader1.Width = 83;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Link";
            this.columnHeader2.Width = 332;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Time";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 19;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(140, 39);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 21);
            this.textBox4.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "Subject name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(138, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 12);
            this.label9.TabIndex = 22;
            this.label9.Text = "Link";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.addTableBtn);
            this.groupBox2.Controls.Add(this.deleteTableBtn);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Location = new System.Drawing.Point(550, 363);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(354, 106);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Timetable Preset";
            // 
            // addTableBtn
            // 
            this.addTableBtn.Location = new System.Drawing.Point(248, 27);
            this.addTableBtn.Name = "addTableBtn";
            this.addTableBtn.Size = new System.Drawing.Size(100, 23);
            this.addTableBtn.TabIndex = 24;
            this.addTableBtn.Text = "Add";
            this.addTableBtn.UseVisualStyleBackColor = true;
            this.addTableBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // deleteTableBtn
            // 
            this.deleteTableBtn.Location = new System.Drawing.Point(248, 56);
            this.deleteTableBtn.Name = "deleteTableBtn";
            this.deleteTableBtn.Size = new System.Drawing.Size(100, 23);
            this.deleteTableBtn.TabIndex = 25;
            this.deleteTableBtn.Text = "Delete";
            this.deleteTableBtn.UseVisualStyleBackColor = true;
            this.deleteTableBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // presetListview
            // 
            this.presetListview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.presetListview.FullRowSelect = true;
            this.presetListview.HideSelection = false;
            this.presetListview.Location = new System.Drawing.Point(550, 12);
            this.presetListview.Name = "presetListview";
            this.presetListview.Size = new System.Drawing.Size(354, 345);
            this.presetListview.TabIndex = 24;
            this.presetListview.UseCompatibleStateImageBehavior = false;
            this.presetListview.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Subject";
            this.columnHeader4.Width = 54;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Link";
            this.columnHeader5.Width = 285;
            // 
            // insertBtn
            // 
            this.insertBtn.Location = new System.Drawing.Point(501, 146);
            this.insertBtn.Name = "insertBtn";
            this.insertBtn.Size = new System.Drawing.Size(40, 35);
            this.insertBtn.TabIndex = 25;
            this.insertBtn.Text = "<=";
            this.insertBtn.UseVisualStyleBackColor = true;
            this.insertBtn.Click += new System.EventHandler(this.insertBtn_Click);
            // 
            // takeoutBtn
            // 
            this.takeoutBtn.Location = new System.Drawing.Point(501, 187);
            this.takeoutBtn.Name = "takeoutBtn";
            this.takeoutBtn.Size = new System.Drawing.Size(40, 35);
            this.takeoutBtn.TabIndex = 26;
            this.takeoutBtn.Text = "=>";
            this.takeoutBtn.UseVisualStyleBackColor = true;
            this.takeoutBtn.Click += new System.EventHandler(this.takeoutBtn_Click);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(791, 475);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(113, 34);
            this.addBtn.TabIndex = 27;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click_1);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(791, 515);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(113, 34);
            this.cancelBtn.TabIndex = 28;
            this.cancelBtn.Text = "Cancle";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(636, 475);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(123, 12);
            this.label10.TabIndex = 27;
            this.label10.Text = "Timetable File Name";
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(638, 490);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(142, 21);
            this.fileName.TabIndex = 26;
            // 
            // writeTimeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 561);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.takeoutBtn);
            this.Controls.Add(this.insertBtn);
            this.Controls.Add(this.presetListview);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.timeTableShowView);
            this.Controls.Add(this.groupBox1);
            this.Name = "writeTimeTable";
            this.Text = "writeTimeTable";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox hourCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox minCombo;
        private System.Windows.Forms.TextBox classTime;
        private System.Windows.Forms.TextBox restingTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private ListViewWithReordering timeTableShowView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button addTableBtn;
        private System.Windows.Forms.Button deleteTableBtn;
        private ListViewWithReordering presetListview;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button insertBtn;
        private System.Windows.Forms.Button takeoutBtn;
        private System.Windows.Forms.Button refreashBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox fileName;
    }
}