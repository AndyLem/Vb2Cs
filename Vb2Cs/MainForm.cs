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
            StartConvertation();
        }

        private void StartConvertation()
        {
            //if (signatureRBtn.Checked)
            //    DoConvertSignature(textBox1.Text);
            //else if (dataAccessRBtn.Checked)
            //    DoConvertDataAccess(textBox1.Text);
            string src = textBox1.Text;
            string firstLine = src.Split((char)0x0D)[0];
            string funStr = DoConvertSignature(firstLine);
            string parStr = DoConvertDataAccess(src);

            StringBuilder sb = new StringBuilder();
            if (funStr != "")
            {
                sb.AppendLine(funStr);
                sb.AppendLine("{");
            }
            sb.AppendLine(parStr);
            if (funStr != "")
            {
                sb.AppendLine("}");
            }
            textBox2.Text = sb.ToString();
        }
        
        private string DoConvertDataAccess(string vbText)
        {
            string[] lines = vbText.Split((char)0x0D, (char)0x0A);
            List<string> parameters = new List<string>();
            bool hasRecordset = false;
            string command = string.Empty;
            List<string> vbCommentedCode = new List<string>();
            foreach (string lineItem in lines)
            {
                string line = lineItem.Trim();
                if (string.IsNullOrEmpty(line)) continue;
                if (line.IndexOf("CreateParameter") >= 0)
                {
                    int lastSpacePos = line.LastIndexOf(", ");
                    string lastParamName = line.Substring(lastSpacePos+2).TrimEnd(')');
                    parameters.Add(lastParamName);
                    vbCommentedCode.Add("// " + line);

                }
                else if (line.IndexOf("Recordset") >= 0)
                {
                    hasRecordset = true;
                    vbCommentedCode.Add("// " + line);
                }
                else if (line.IndexOf("CommandText") >= 0)
                {
                    Regex procNameEx = new Regex("(?<=\\\")[^\"]+?(?=\\\")");
                    command = procNameEx.Match(line).Value;
                    vbCommentedCode.Add("// " + line);
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("// Converted from VB:");
            foreach (string line in vbCommentedCode)
                sb.AppendLine(line);
            sb.AppendLine("\ttry");
            sb.AppendLine("\t{");
            
            if (hasRecordset)
            {
                sb.AppendLine("\t\tDataSet dsResult = DBMgr.Wf.ExecuteDataSet(");
                sb.AppendLine(string.Format("\t\t\t\"{0}\",", command));
                foreach (string par in parameters)
                    sb.AppendLine("\t\t\t" + par+",");
                sb.AppendLine("\t\t\t0);");
                sb.AppendLine("\t\treturn dsResult[0];");
            }
            else
            {
                sb.AppendLine("\t\tDBMgr.Wf.ExecuteNonQuery(");
                sb.Append(string.Format("\t\t\t\"{0}\"", command));


                foreach (string par in parameters)
                {
                    sb.AppendLine(",");
                    sb.Append("\t\t\t" + par); ;
                }
                sb.AppendLine(");");
                sb.AppendLine("\t\treturn 0;");
            }
            sb.AppendLine("\t}");
            sb.AppendLine("\tcatch (Exception ex)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tExceptionWrapper.TraceError(ex);");
            sb.AppendLine("\t\tthrow ex;");
            sb.AppendLine("\t}");
            return sb.ToString();
        }

        private string DoConvertSignature(string vbText)
        {
            if (vbText.IndexOf("Public Function") < 0) return "";

            string funcNameEx = "(?<=Public\\sFunction\\s)[^\"]+(?=\\()";
            string parNamesEx = "(?<=ByVal\\s)[^\"]+?(?=\\sAs)";
            string parTypesEx = "(?<=As\\s)[a-zA-Z]+?(?=\\,|\\)|\\s|\\=)";
            string funcTypeEx = "(?<=\\)\\sAs\\s)[^\"]+";
            string parDefValEx = "(?<=\\=\\s)[^\"]+";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("// " + vbText);
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
                sb.AppendLine();
                sb.Append("\t" + pt + " ");
                sb.Append(pn + defVal);

            }
            sb.Append(")");

            //sb.Replace((char)0x0D, ' ');
            //sb.Replace((char)0x0A, ' ');
            //sb.Replace("  ", " ");

            return sb.ToString();
        }
    }
}
