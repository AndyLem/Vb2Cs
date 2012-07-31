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
        protected Dictionary<string, string> _replaceTypes = new Dictionary<string,string>();
        protected List<string> _removeParams = new List<string>();

        public MainForm()
        {
            InitializeComponent();
            _replaceTypes.Add("adodb.recordset", "DataTable");
            _removeParams.Add("a_sConnectionString");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DoConvert(textBox1.Text);
        }

        private void DoConvert(string vbText)
        {
            string funcNameEx = "(?<=Public\\sFunction\\s)[^\"]+(?=\\()";
            string parNamesEx = "(?<=ByVal\\s)[^\"]+?(?=\\sAs)";
            string parTypesEx = "(?<=As\\s)[a-zA-Z]+?(?=\\,|\\)|\\s|\\=)";
            string funcTypeEx = "(?<=\\)\\sAs\\s)[^\"]+";
            string parDefValEx = "(?<=\\=\\s)[^\"]+";

            StringBuilder sb = new StringBuilder();
            sb.Append("public static ");

            Regex fNameEx = new Regex(funcNameEx);
            string funcName = fNameEx.Match(vbText).Value;

            Regex fTypeEx = new Regex(funcTypeEx);
            string funcType = fTypeEx.Match(vbText).Value.ToLower();
            if (_replaceTypes.ContainsKey(funcType))
                funcType = _replaceTypes[funcType];


            sb.Append(funcType+" ");
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
                string pn = parNames[i];
                if (_removeParams.IndexOf(pn) >= 0)
                {
                    continue;
                }

                string pt = parTypes[i].ToLower();
                if (_replaceTypes.ContainsKey(pt))
                    pt = _replaceTypes[pt];

                string defVal = parDefEx.Match(pt).Value;
                if (defVal != "") defVal = " = " + defVal;

                if (i != 0)
                    sb.Append(", ");
                sb.Append(pt + " ");
                sb.Append(pn + defVal);

            }
            sb.Append(")");

            sb.Replace((char)0x0D, ' ');
            sb.Replace((char)0x0A, ' ');
            sb.Replace("  ", " ");


            textBox2.Text = sb.ToString();
        }
    }
}
