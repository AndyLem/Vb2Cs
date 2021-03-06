﻿namespace Vb2Cs
{
    partial class CodeTransformForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeTransformForm));
            this.vbCodeBox = new System.Windows.Forms.TextBox();
            this.csCodeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.copyBtn = new System.Windows.Forms.Button();
            this.pasteBtn = new System.Windows.Forms.Button();
            this.funcInfoBox = new System.Windows.Forms.ListBox();
            this.forceDataSetBox = new System.Windows.Forms.CheckBox();
            this.forceUseCmdBox = new System.Windows.Forms.CheckBox();
            this.wfBox = new System.Windows.Forms.RadioButton();
            this.realtyBox = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // vbCodeBox
            // 
            this.vbCodeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vbCodeBox.Location = new System.Drawing.Point(12, 43);
            this.vbCodeBox.Multiline = true;
            this.vbCodeBox.Name = "vbCodeBox";
            this.vbCodeBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.vbCodeBox.Size = new System.Drawing.Size(844, 90);
            this.vbCodeBox.TabIndex = 0;
            this.vbCodeBox.WordWrap = false;
            this.vbCodeBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // csCodeBox
            // 
            this.csCodeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.csCodeBox.Location = new System.Drawing.Point(15, 172);
            this.csCodeBox.Multiline = true;
            this.csCodeBox.Name = "csCodeBox";
            this.csCodeBox.ReadOnly = true;
            this.csCodeBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.csCodeBox.Size = new System.Drawing.Size(573, 266);
            this.csCodeBox.TabIndex = 1;
            this.csCodeBox.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "VB";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "C#";
            // 
            // copyBtn
            // 
            this.copyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.copyBtn.Location = new System.Drawing.Point(39, 139);
            this.copyBtn.Name = "copyBtn";
            this.copyBtn.Size = new System.Drawing.Size(75, 27);
            this.copyBtn.TabIndex = 4;
            this.copyBtn.Text = "Copy";
            this.copyBtn.UseVisualStyleBackColor = true;
            this.copyBtn.Click += new System.EventHandler(this.copyBtn_Click);
            // 
            // pasteBtn
            // 
            this.pasteBtn.Location = new System.Drawing.Point(39, 13);
            this.pasteBtn.Name = "pasteBtn";
            this.pasteBtn.Size = new System.Drawing.Size(75, 27);
            this.pasteBtn.TabIndex = 5;
            this.pasteBtn.Text = "Paste";
            this.pasteBtn.UseVisualStyleBackColor = true;
            this.pasteBtn.Click += new System.EventHandler(this.pasteBtn_Click);
            // 
            // funcInfoBox
            // 
            this.funcInfoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.funcInfoBox.FormattingEnabled = true;
            this.funcInfoBox.HorizontalScrollbar = true;
            this.funcInfoBox.IntegralHeight = false;
            this.funcInfoBox.Location = new System.Drawing.Point(594, 172);
            this.funcInfoBox.Name = "funcInfoBox";
            this.funcInfoBox.Size = new System.Drawing.Size(262, 271);
            this.funcInfoBox.TabIndex = 6;
            // 
            // forceDataSetBox
            // 
            this.forceDataSetBox.AutoSize = true;
            this.forceDataSetBox.Location = new System.Drawing.Point(120, 16);
            this.forceDataSetBox.Name = "forceDataSetBox";
            this.forceDataSetBox.Size = new System.Drawing.Size(170, 17);
            this.forceDataSetBox.TabIndex = 7;
            this.forceDataSetBox.Text = "Force ExecuteDataSet pattern";
            this.forceDataSetBox.UseVisualStyleBackColor = true;
            this.forceDataSetBox.CheckedChanged += new System.EventHandler(this.forceDataSetBox_CheckedChanged);
            // 
            // forceUseCmdBox
            // 
            this.forceUseCmdBox.AutoSize = true;
            this.forceUseCmdBox.Location = new System.Drawing.Point(296, 16);
            this.forceUseCmdBox.Name = "forceUseCmdBox";
            this.forceUseCmdBox.Size = new System.Drawing.Size(153, 17);
            this.forceUseCmdBox.TabIndex = 8;
            this.forceUseCmdBox.Text = "Force DbCommand pattern";
            this.forceUseCmdBox.UseVisualStyleBackColor = true;
            this.forceUseCmdBox.CheckedChanged += new System.EventHandler(this.forceUseCmdBox_CheckedChanged);
            // 
            // wfBox
            // 
            this.wfBox.AutoSize = true;
            this.wfBox.Checked = true;
            this.wfBox.Location = new System.Drawing.Point(503, 15);
            this.wfBox.Name = "wfBox";
            this.wfBox.Size = new System.Drawing.Size(39, 17);
            this.wfBox.TabIndex = 9;
            this.wfBox.TabStop = true;
            this.wfBox.Text = "Wf";
            this.wfBox.UseVisualStyleBackColor = true;
            this.wfBox.CheckedChanged += new System.EventHandler(this.wfBox_CheckedChanged);
            // 
            // realtyBox
            // 
            this.realtyBox.AutoSize = true;
            this.realtyBox.Location = new System.Drawing.Point(548, 15);
            this.realtyBox.Name = "realtyBox";
            this.realtyBox.Size = new System.Drawing.Size(55, 17);
            this.realtyBox.TabIndex = 10;
            this.realtyBox.Text = "Realty";
            this.realtyBox.UseVisualStyleBackColor = true;
            this.realtyBox.CheckedChanged += new System.EventHandler(this.realtyBox_CheckedChanged);
            // 
            // CodeTransformForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 450);
            this.Controls.Add(this.realtyBox);
            this.Controls.Add(this.wfBox);
            this.Controls.Add(this.forceUseCmdBox);
            this.Controls.Add(this.forceDataSetBox);
            this.Controls.Add(this.funcInfoBox);
            this.Controls.Add(this.pasteBtn);
            this.Controls.Add(this.copyBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.csCodeBox);
            this.Controls.Add(this.vbCodeBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CodeTransformForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visual Basic to C# Code Converter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CodeTransformForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox vbCodeBox;
        private System.Windows.Forms.TextBox csCodeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button copyBtn;
        private System.Windows.Forms.Button pasteBtn;
        private System.Windows.Forms.ListBox funcInfoBox;
        private System.Windows.Forms.CheckBox forceDataSetBox;
        private System.Windows.Forms.CheckBox forceUseCmdBox;
        private System.Windows.Forms.RadioButton wfBox;
        private System.Windows.Forms.RadioButton realtyBox;
    }
}

