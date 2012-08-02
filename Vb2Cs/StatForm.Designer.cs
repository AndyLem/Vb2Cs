namespace Vb2Cs
{
    partial class StatForm
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
            this.statBox = new System.Windows.Forms.ListBox();
            this.pasteBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.vbCodeBox = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // statBox
            // 
            this.statBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statBox.FormattingEnabled = true;
            this.statBox.HorizontalScrollbar = true;
            this.statBox.IntegralHeight = false;
            this.statBox.Location = new System.Drawing.Point(15, 207);
            this.statBox.Name = "statBox";
            this.statBox.Size = new System.Drawing.Size(766, 240);
            this.statBox.TabIndex = 10;
            // 
            // pasteBtn
            // 
            this.pasteBtn.Location = new System.Drawing.Point(39, 2);
            this.pasteBtn.Name = "pasteBtn";
            this.pasteBtn.Size = new System.Drawing.Size(75, 27);
            this.pasteBtn.TabIndex = 9;
            this.pasteBtn.Text = "Paste";
            this.pasteBtn.UseVisualStyleBackColor = true;
            this.pasteBtn.Click += new System.EventHandler(this.pasteBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "VB";
            // 
            // vbCodeBox
            // 
            this.vbCodeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vbCodeBox.Location = new System.Drawing.Point(12, 35);
            this.vbCodeBox.Multiline = true;
            this.vbCodeBox.Name = "vbCodeBox";
            this.vbCodeBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.vbCodeBox.Size = new System.Drawing.Size(769, 135);
            this.vbCodeBox.TabIndex = 7;
            this.vbCodeBox.WordWrap = false;
            this.vbCodeBox.TextChanged += new System.EventHandler(this.vbCodeBox_TextChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveBtn.Location = new System.Drawing.Point(15, 176);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 11;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // StatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 452);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.statBox);
            this.Controls.Add(this.pasteBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.vbCodeBox);
            this.Name = "StatForm";
            this.Text = "VB Code Statistics";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox statBox;
        private System.Windows.Forms.Button pasteBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox vbCodeBox;
        private System.Windows.Forms.Button saveBtn;
    }
}