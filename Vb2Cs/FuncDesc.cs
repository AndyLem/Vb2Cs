using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vb2Cs
{
    public class FuncDesc
    {
        protected List<string> _prefixes = new List<string>();
        protected string _subPrefix = "Public Sub";

        public class Parameter
        {
            public string Name = string.Empty;
            public string Type = string.Empty;
            public string DefaultValue = string.Empty;
            public bool HasDefaultValue = false;
            public bool Valid = false;
            public string CommentedSrc = string.Empty;
            public string Value = string.Empty;

            public Parameter(string paramString)
            {
                CommentedSrc = "// " + paramString;
                int asIndex = paramString.IndexOf("As");
                if (asIndex < 0)    // Это не сгнатура метода, а вызов, и передано значение параметра
                {
                    Value = paramString;
                    return;
                }
                

                string definition = paramString.Substring(0, asIndex).Trim();
                string typeAndDefault = paramString.Substring(asIndex + 2).Trim();

                int nameStartIndex = definition.LastIndexOf(' ');
                if (nameStartIndex == -1) 
                    nameStartIndex = 0;
                Name = definition.Substring(nameStartIndex).Trim();
                bool isArray = Name.IndexOf("()") >= 0;
                if (isArray)
                {
                    Name = Name.Replace("()", "");
                }

                int eqIndex = typeAndDefault.IndexOf('=');
                if (eqIndex < 0)
                {
                    Type = typeAndDefault;
                    HasDefaultValue = false;
                    DefaultValue = "";
                }
                else
                {
                    Type = typeAndDefault.Substring(0, eqIndex).Trim();
                    DefaultValue = typeAndDefault.Substring(eqIndex + 1).Trim();
                    HasDefaultValue = true;
                }
                Type = Transformer.TransformType(Type);
                if (isArray)
                    Type += "[]";
                Name = Transformer.TransformParam(Name);
                Valid = true;
            }

            public override string ToString()
            {
                if (Valid)
                    return string.Format("{0} {1}{2}", Type, Name, HasDefaultValue ? " = " + DefaultValue : "");
                else return "<<Error in parameter parsing>>";
            }
        }

        public string Name = string.Empty;
        public List<Parameter> Parameters = new List<Parameter>();
        public string ResultType = string.Empty;

        public string CommentedSrc = string.Empty;

        public bool Valid = false;

        public FuncDesc(string src)
        {
            InitPrefixes();
            src = src.Trim();
            CommentedSrc = "// " + src;

            int prefixIndex = -1, prefixEndIndex = -1;
            string currentPrefix = "";
            foreach (string prefix in _prefixes)
            {
                prefixIndex = src.IndexOf(prefix);
                if (prefixIndex >= 0)
                {
                    prefixEndIndex = prefixIndex + prefix.Length;
                    currentPrefix = prefix;
                    break;
                }
            }
            bool isSub = currentPrefix == "Sub";

            int leftBracketIndex = src.IndexOf("(");
            int rightBracketIndex = src.LastIndexOf(")");
            if (prefixIndex < 0) return;
            if (leftBracketIndex < 0) return;
            if (rightBracketIndex < 0) return;

            Name = src.Substring(prefixEndIndex, leftBracketIndex - prefixEndIndex).Trim();
            string paramsLine = src.Substring(leftBracketIndex + 1, rightBracketIndex - leftBracketIndex - 1).Trim();
            Parameters = ExtractParameters(paramsLine);
            if (!isSub)
            {
                int lastAsIndex = src.LastIndexOf("As");
                int lastBracketIndex = src.LastIndexOf(')');
                if ((lastAsIndex >= 0) && (lastBracketIndex < lastAsIndex))
                {
                    ResultType = src.Substring(lastAsIndex + 2).Trim();
                    ResultType = Transformer.TransformType(ResultType);
                }
                else
                {
                    ResultType = "__Type_not_found__";
                }
            }
            else ResultType = "void";

            Valid = true;
        }

        public static List<Parameter> ExtractParameters(string paramsLine)
        {
            List<Parameter> res = new List<Parameter>();
            var splittedParams = SplitParams(paramsLine);
            foreach (string singleParamString in splittedParams)
            {
                Parameter par = new Parameter(singleParamString);
                if (!Transformer.RemoveParam(par.Name))
                    res.Add(par);
            }
            return res;
        }

        private void InitPrefixes()
        {
            _prefixes.Add("Function");
            _prefixes.Add("Sub");
            _prefixes.Add("Get");
            _prefixes.Add("Let");
        }

        protected static List<string> SplitParams(string paramsLine)
        {
            List<string> splittedParams = new List<string>();
            int curPos = 0;
            int curParamStartPos = 0;
            int len = paramsLine.Length;
            int bracketLevel = 0;
            bool inQuotes = false;
            while (curPos < len)
            {
                switch (paramsLine[curPos])
                {
                    case '"':
                        inQuotes = !inQuotes;
                        curPos++;
                        continue;
                    case '(':
                        bracketLevel++;
                        curPos++;
                        continue;
                    case ')':
                        bracketLevel--;
                        curPos++;
                        continue;
                    case ',':
                        if (!inQuotes && (bracketLevel == 0))
                        {
                            string param = paramsLine.Substring(curParamStartPos, curPos - curParamStartPos);
                            splittedParams.Add(param.Trim());
                            curPos++;
                            curParamStartPos = curPos;
                            continue;
                        }
                        break;
                }
                curPos++;
            }
            if (curParamStartPos < curPos)
            {
                string param = paramsLine.Substring(curParamStartPos, curPos - curParamStartPos);
                splittedParams.Add(param.Trim());
            }
            return splittedParams;
        }

        public override string ToString()
        {
            return ToLine(true);
        }

        private string ToLine(bool startNewLines)
        {
            if (Valid)
                return string.Format("public static {0} {1}({2})", ResultType, Name, ParametersToString(startNewLines));
            else
                return "<<< Error in function parsing >>>";
        }

        private string ParametersToString(bool startNewLines)
        {
            StringBuilder sb = new StringBuilder();
            bool needComma = false;
            foreach (Parameter par in Parameters)
            {
                if (needComma) sb.Append(", ");
                if (startNewLines)
                {
                    sb.AppendLine();
                    sb.Append("\t");
                }
                sb.Append(par.ToString());
                needComma = true;
            }
            return sb.ToString();
        }

        public object ToSingleLine()
        {
            return ToLine(false);
        }
    }
}
