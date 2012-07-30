using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Vb2Cs
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DoConvert(textBox1.Text);
        }

        private void DoConvert(string vbText)
        {
            string funcNameEx = "(?<=Public\\sFunction\\s)[^\"]+(?=\\()";
            string parNamesEx = "(?<=ByVal\\s)[^\"]+?(?=\\sAs)";
            string parTypesEx = "(?<=As\\s)[^\"]+?(?=\\,|\\))";
            string funcTypeEx = "(?<=\\)\\sAs\\s)[^\"]+";
            string parDefValEx = "(?<=\\=\\s)[^\"]+";

            StringBuilder sb = new StringBuilder();
            sb.Append("public ");

            Regex fNameEx = new Regex(funcNameEx);
            string funcName = fNameEx.Match(vbText).Value;

            Regex fTypeEx = new Regex(funcTypeEx);
            string funcType = fTypeEx.Match(vbText).Value;

            sb.Append(funcType.ToLower()+" ");
            sb.Append(funcName+"(");

            Regex pTypesEx = new Regex(parTypesEx);
            var pTypesMatches = pTypesEx.Matches(vbText);

            List<string> parTypes = new List<string>();
            foreach (Match m in pTypesMatches)
                parTypes.Add(m.Value);


            Regex pNamesEx = new Regex(parNamesEx);
            var pNamesMatches = pNamesEx.Matches(vbText);

            List<string> parNames = new List<string>();
            foreach (Match m in pNamesMatches)
                parNames.Add(m.Value);

            Regex parDefEx = new Regex(parDefValEx);
            int parsCount = Math.Min(parNames.Count, parTypes.Count);
            for (int i = 0; i < parsCount; i++)
            {
                string pt = parTypes[i].ToLower();
                string pn = parNames[i];
                string defVal = parDefEx.Match(pt).Value;
                if (defVal != "") defVal = " = " + defVal;
                sb.Append(pt + " ");
                sb.Append(pn + defVal);
                if (i != parsCount - 1)
                    sb.Append(", ");
            }
            sb.Append(")");


            textBox2.Text = sb.ToString();
        }
    }
}
