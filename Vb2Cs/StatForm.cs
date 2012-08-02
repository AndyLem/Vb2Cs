using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Vb2Cs
{
    public partial class StatForm : Form
    {
        List<FuncDesc> _functions = new List<FuncDesc>();

        public StatForm()
        {
            InitializeComponent();
        }

        private void pasteBtn_Click(object sender, EventArgs e)
        {
            vbCodeBox.Text = Clipboard.GetText();
        }

        private void vbCodeBox_TextChanged(object sender, EventArgs e)
        {
            _functions.Clear();
            string src = vbCodeBox.Text.Trim();
            src = Preprocessor.ReplaceStrings(src, "_" + Environment.NewLine, "");
            src = Preprocessor.ReplaceStrings(src, "  ", " ");
            string[] lines = src.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                FuncDesc func = new FuncDesc(line);
                if (func.Valid)
                {
                    statBox.Items.Add(func.ToSingleLine());
                    _functions.Add(func);
                }
            }
        } 

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*.txt|*.txt";
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            StreamWriter fs = File.CreateText(dlg.FileName);
            foreach (FuncDesc func in _functions)
                fs.WriteLine(func.ToSingleLine());

            fs.Close();
        }
    }
}
