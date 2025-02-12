namespace ClickElf.Crafting
{
    partial class CraftingForm
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
            this.btnSettings = new System.Windows.Forms.Button();
            this.txtMacroBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStartPos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtM1Pos = new System.Windows.Forms.TextBox();
            this.txtM2Pos = new System.Windows.Forms.TextBox();
            this.txtM3Pos = new System.Windows.Forms.TextBox();
            this.lblM1Pos = new System.Windows.Forms.Label();
            this.lblM2Pos = new System.Windows.Forms.Label();
            this.lblM3Pos = new System.Windows.Forms.Label();
            this.lblM3WaitTime = new System.Windows.Forms.Label();
            this.lblM2WaitTime = new System.Windows.Forms.Label();
            this.lblM1WaitTime = new System.Windows.Forms.Label();
            this.txtM3WaitTime = new System.Windows.Forms.TextBox();
            this.txtM2WaitTime = new System.Windows.Forms.TextBox();
            this.txtM1WaitTime = new System.Windows.Forms.TextBox();
            this.txtAutoDelay = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(416, 405);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(93, 46);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.Text = "设置";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // txtMacroBox
            // 
            this.txtMacroBox.Location = new System.Drawing.Point(31, 71);
            this.txtMacroBox.Multiline = true;
            this.txtMacroBox.Name = "txtMacroBox";
            this.txtMacroBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMacroBox.Size = new System.Drawing.Size(478, 222);
            this.txtMacroBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "输入【开始制作】坐标：";
            // 
            // txtStartPos
            // 
            this.txtStartPos.Location = new System.Drawing.Point(185, 21);
            this.txtStartPos.Name = "txtStartPos";
            this.txtStartPos.Size = new System.Drawing.Size(324, 21);
            this.txtStartPos.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "在此处粘贴所有宏，两个宏间空一行";
            // 
            // txtM1Pos
            // 
            this.txtM1Pos.Location = new System.Drawing.Point(31, 320);
            this.txtM1Pos.Name = "txtM1Pos";
            this.txtM1Pos.Size = new System.Drawing.Size(145, 21);
            this.txtM1Pos.TabIndex = 5;
            // 
            // txtM2Pos
            // 
            this.txtM2Pos.Location = new System.Drawing.Point(198, 320);
            this.txtM2Pos.Name = "txtM2Pos";
            this.txtM2Pos.Size = new System.Drawing.Size(145, 21);
            this.txtM2Pos.TabIndex = 6;
            // 
            // txtM3Pos
            // 
            this.txtM3Pos.Location = new System.Drawing.Point(364, 320);
            this.txtM3Pos.Name = "txtM3Pos";
            this.txtM3Pos.Size = new System.Drawing.Size(145, 21);
            this.txtM3Pos.TabIndex = 7;
            // 
            // lblM1Pos
            // 
            this.lblM1Pos.AutoSize = true;
            this.lblM1Pos.Location = new System.Drawing.Point(29, 305);
            this.lblM1Pos.Name = "lblM1Pos";
            this.lblM1Pos.Size = new System.Drawing.Size(95, 12);
            this.lblM1Pos.TabIndex = 8;
            this.lblM1Pos.Text = "宏#1 热键栏坐标";
            // 
            // lblM2Pos
            // 
            this.lblM2Pos.AutoSize = true;
            this.lblM2Pos.Location = new System.Drawing.Point(196, 305);
            this.lblM2Pos.Name = "lblM2Pos";
            this.lblM2Pos.Size = new System.Drawing.Size(95, 12);
            this.lblM2Pos.TabIndex = 9;
            this.lblM2Pos.Text = "宏#2 热键栏坐标";
            // 
            // lblM3Pos
            // 
            this.lblM3Pos.AutoSize = true;
            this.lblM3Pos.Location = new System.Drawing.Point(362, 305);
            this.lblM3Pos.Name = "lblM3Pos";
            this.lblM3Pos.Size = new System.Drawing.Size(95, 12);
            this.lblM3Pos.TabIndex = 10;
            this.lblM3Pos.Text = "宏#3 热键栏坐标";
            // 
            // lblM3WaitTime
            // 
            this.lblM3WaitTime.AutoSize = true;
            this.lblM3WaitTime.Location = new System.Drawing.Point(362, 352);
            this.lblM3WaitTime.Name = "lblM3WaitTime";
            this.lblM3WaitTime.Size = new System.Drawing.Size(83, 12);
            this.lblM3WaitTime.TabIndex = 16;
            this.lblM3WaitTime.Text = "宏#3 等待时间";
            // 
            // lblM2WaitTime
            // 
            this.lblM2WaitTime.AutoSize = true;
            this.lblM2WaitTime.Location = new System.Drawing.Point(196, 352);
            this.lblM2WaitTime.Name = "lblM2WaitTime";
            this.lblM2WaitTime.Size = new System.Drawing.Size(83, 12);
            this.lblM2WaitTime.TabIndex = 15;
            this.lblM2WaitTime.Text = "宏#2 等待时间";
            // 
            // lblM1WaitTime
            // 
            this.lblM1WaitTime.AutoSize = true;
            this.lblM1WaitTime.Location = new System.Drawing.Point(29, 352);
            this.lblM1WaitTime.Name = "lblM1WaitTime";
            this.lblM1WaitTime.Size = new System.Drawing.Size(83, 12);
            this.lblM1WaitTime.TabIndex = 14;
            this.lblM1WaitTime.Text = "宏#1 等待时间";
            // 
            // txtM3WaitTime
            // 
            this.txtM3WaitTime.Location = new System.Drawing.Point(364, 367);
            this.txtM3WaitTime.Name = "txtM3WaitTime";
            this.txtM3WaitTime.Size = new System.Drawing.Size(145, 21);
            this.txtM3WaitTime.TabIndex = 13;
            // 
            // txtM2WaitTime
            // 
            this.txtM2WaitTime.Location = new System.Drawing.Point(198, 367);
            this.txtM2WaitTime.Name = "txtM2WaitTime";
            this.txtM2WaitTime.Size = new System.Drawing.Size(145, 21);
            this.txtM2WaitTime.TabIndex = 12;
            // 
            // txtM1WaitTime
            // 
            this.txtM1WaitTime.Location = new System.Drawing.Point(31, 367);
            this.txtM1WaitTime.Name = "txtM1WaitTime";
            this.txtM1WaitTime.Size = new System.Drawing.Size(145, 21);
            this.txtM1WaitTime.TabIndex = 11;
            // 
            // txtAutoDelay
            // 
            this.txtAutoDelay.Location = new System.Drawing.Point(31, 419);
            this.txtAutoDelay.Name = "txtAutoDelay";
            this.txtAutoDelay.Size = new System.Drawing.Size(248, 21);
            this.txtAutoDelay.TabIndex = 17;
            this.txtAutoDelay.Text = "25";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 404);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(263, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "自动延迟（ms，防止卡顿，留空或为0则不添加）";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(317, 405);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(93, 46);
            this.btnGenerate.TabIndex = 19;
            this.btnGenerate.Text = "生成脚本";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // CraftingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 469);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAutoDelay);
            this.Controls.Add(this.lblM3WaitTime);
            this.Controls.Add(this.lblM2WaitTime);
            this.Controls.Add(this.lblM1WaitTime);
            this.Controls.Add(this.txtM3WaitTime);
            this.Controls.Add(this.txtM2WaitTime);
            this.Controls.Add(this.txtM1WaitTime);
            this.Controls.Add(this.lblM3Pos);
            this.Controls.Add(this.lblM2Pos);
            this.Controls.Add(this.lblM1Pos);
            this.Controls.Add(this.txtM3Pos);
            this.Controls.Add(this.txtM2Pos);
            this.Controls.Add(this.txtM1Pos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStartPos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMacroBox);
            this.Controls.Add(this.btnSettings);
            this.Name = "CraftingForm";
            this.Text = "Crafting Helper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.TextBox txtMacroBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStartPos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtM1Pos;
        private System.Windows.Forms.TextBox txtM2Pos;
        private System.Windows.Forms.TextBox txtM3Pos;
        private System.Windows.Forms.Label lblM1Pos;
        private System.Windows.Forms.Label lblM2Pos;
        private System.Windows.Forms.Label lblM3Pos;
        private System.Windows.Forms.Label lblM3WaitTime;
        private System.Windows.Forms.Label lblM2WaitTime;
        private System.Windows.Forms.Label lblM1WaitTime;
        private System.Windows.Forms.TextBox txtM3WaitTime;
        private System.Windows.Forms.TextBox txtM2WaitTime;
        private System.Windows.Forms.TextBox txtM1WaitTime;
        private System.Windows.Forms.TextBox txtAutoDelay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGenerate;
    }
}