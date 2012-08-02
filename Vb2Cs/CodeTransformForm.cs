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
    public partial class CodeTransformForm : Form
    {
        protected Dictionary<string, string> _replaceTypes = new Dictionary<string,string>();
        protected Dictionary<string, string> _replaceParams = new Dictionary<string, string>();
        protected List<string> _removeParams = new List<string>();

        public CodeTransformForm()
        {
            InitializeComponent();
            _replaceTypes.Add("adodb.recordset", "DataTable");
            _replaceTypes.Add("Boolean", "bool");
            _replaceParams.Add("Null", "null");
            _removeParams.Add("a_sConnectionString");
            Transformer.Init();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            StartConvertation_old();
            StartConvertation();
        }

        private void StartConvertation()
        {
            string src = vbCodeBox.Text.Trim();
            src = Preprocessor.ReplaceStrings(src, "_" + Environment.NewLine, "");
            src = Preprocessor.ReplaceStrings(src, "  ", " ");
            string[] lines = src.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            funcInfoBox.Items.Clear();
            if (lines.Length == 0) return;
            FuncDesc func = new FuncDesc(lines[0]);
            funcInfoBox.Items.Add(string.Format("Valid: '{0}'", func.Valid));
            funcInfoBox.Items.Add(string.Format("Name: '{0}'", func.Name));
            funcInfoBox.Items.Add(string.Format("Result type: '{0}'", func.ResultType));
            funcInfoBox.Items.Add(string.Format("Commented src: '{0}'", func.CommentedSrc));
            funcInfoBox.Items.Add(string.Format("Params: {0}", func.Parameters.Count));
            foreach (FuncDesc.Parameter par in func.Parameters)
            {
                if (par.Valid)
                    funcInfoBox.Items.Add(string.Format("\tName:'{0}', Type: '{1}', Default value: '{2}', Src: {3}", par.Name, par.Type, par.HasDefaultValue ? par.DefaultValue : "-", par.CommentedSrc));
                else
                    funcInfoBox.Items.Add(string.Format("Param is not Valid: {0}", par.CommentedSrc));
            }
            string codeConversionResult = DoConvertDataAccess(lines, func.Name);


            StringBuilder sb = new StringBuilder();
            if (func.Valid)
            {
                sb.AppendLine(func.ToString());
                sb.AppendLine("{");
                sb.AppendLine("\t" + func.CommentedSrc);
            }
            sb.Append(codeConversionResult);
            if (func.Valid)
                sb.AppendLine("}");

            csCodeBox.Text = sb.ToString();

            Clipboard.SetText(sb.ToString());
        }

        private void StartConvertation_old()
        {
            //if (signatureRBtn.Checked)
            //    DoConvertSignature(textBox1.Text);
            //else if (dataAccessRBtn.Checked)
            //    DoConvertDataAccess(textBox1.Text);
            string src = vbCodeBox.Text;
            src = Preprocessor.ReplaceStrings(src, "_" + Environment.NewLine, "");
            src = Preprocessor.ReplaceStrings(src, "  ", " ");


            string firstLine = src.Split((char)0x0D)[0];
            string funStr = DoConvertSignature_old(firstLine);
            string parStr = DoConvertDataAccess_old(src);

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
            csCodeBox.Text = sb.ToString();
        }

        private string DoConvertDataAccess_old(string vbText)
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
                else if (line.IndexOf("New ADODB.Recordset") >= 0)
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
                sb.AppendLine("\t\tDataSet dsResult = DBMngr.Wf.ExecuteDataSet(");
                sb.AppendLine(string.Format("\t\t\t\"{0}\",", command));
                foreach (string par in parameters)
                {
                    string p = par;
                    if (_replaceParams.ContainsKey(p))
                        p = _replaceParams[p];
                    sb.AppendLine("\t\t\t" + p + ",");
                }
                sb.AppendLine("\t\t\t0);");
                sb.AppendLine("\t\treturn dsResult.Tables[0];");
            }
            else
            {
                sb.AppendLine("\t\tDBMngr.Wf.ExecuteNonQuery(");
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

        private string DoConvertDataAccess(string[] vbTextLines, string funcName)
        {
            List<string> parameters = new List<string>();
            bool hasRecordset = false;
            string command = string.Empty;
            List<string> vbCommentedCode = new List<string>();
            foreach (string lineItem in vbTextLines)
            {
                string line = lineItem.Trim();
                if (string.IsNullOrEmpty(line)) continue;
                if (line.IndexOf("CreateParameter") >= 0)
                {
                    int lastSpacePos = line.LastIndexOf(", ");
                    string lastParamName = line.Substring(lastSpacePos + 2).TrimEnd(')');
                    parameters.Add(lastParamName);
                    vbCommentedCode.Add("// " + line);
                }
                else if (line.IndexOf("New ADODB.Recordset") >= 0)
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
            sb.AppendLine("\t// Converted from VB:");
            foreach (string line in vbCommentedCode)
                sb.AppendLine("\t"+line);
            sb.AppendLine("\ttry");
            sb.AppendLine("\t{");

            string dsName = "Result";
            if (funcName != "")
            {
                if (funcName.StartsWith("Get"))
                    dsName = funcName.Remove(0, 3);
                else dsName = funcName;
            }


            if (hasRecordset)
            {
                sb.AppendLine("\t\tDataSet ds" + dsName + " = DBMngr.Wf.ExecuteDataSet(");
                sb.AppendLine(string.Format("\t\t\t\"{0}\",", command));
                foreach (string par in parameters)
                {
                    string p = par;
                    if (_replaceParams.ContainsKey(p))
                        p = _replaceParams[p];
                    sb.AppendLine("\t\t\t" + p + ",");
                }
                sb.AppendLine("\t\t\t0);");
                sb.AppendLine("\t\treturn dsResult.Tables[0];");
            }
            else
            {
                sb.AppendLine("\t\tDBMngr.Wf.ExecuteNonQuery(");
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

        private string DoConvertSignature_old(string vbText)
        {
            if (vbText.IndexOf("Public Function") < 0) return "";

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
                sb.AppendLine();
                sb.Append("\t" + pt + " ");
                sb.Append(pn + defVal);

            }
            sb.AppendLine(")");
            sb.Append("// " + vbText);

            //sb.Replace((char)0x0D, ' ');
            //sb.Replace((char)0x0A, ' ');
            //sb.Replace("  ", " ");

            return sb.ToString();
        }

        private void copyBtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(csCodeBox.Text);
        }

        private void pasteBtn_Click(object sender, EventArgs e)
        {
            vbCodeBox.Text = Clipboard.GetText();
        }

        private void CodeTransformForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }
    }    
}
