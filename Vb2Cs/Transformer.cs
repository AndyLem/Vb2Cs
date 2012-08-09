using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vb2Cs
{
    public static class Transformer
    {
        private static Dictionary<string, string> _replaceTypes = new Dictionary<string, string>();
        private static Dictionary<string, string> _replaceParams = new Dictionary<string, string>();
        private static Dictionary<string, string> _convertTypes = new Dictionary<string, string>();
        private static List<string> _removeParams = new List<string>();

        private static string Transform(Dictionary<string, string> dict, string src)
        {
            if (dict.ContainsKey(src))
                return dict[src];
            return src;
        }

        public static string TransformType(string type)
        {
            return Transform(_replaceTypes, type);
        }

        public static string TransformParam(string param)
        {
            return Transform(_replaceParams, param);
        }

        public static bool RemoveParam(string param)
        {
            return _removeParams.Contains(param);
        }

        public static void Init()
        {
            _replaceTypes.Add("ADODB.Recordset", "DataTable");
            _replaceTypes.Add("Recordset", "DataTable");
            _replaceTypes.Add("Boolean", "bool");
            _replaceTypes.Add("String", "string");
            _replaceTypes.Add("Long", "long");
            _replaceTypes.Add("Byte", "byte");
            _replaceTypes.Add("Integer", "int");
            _replaceTypes.Add("Date", "DateTime");
            _replaceTypes.Add("Double", "double");

            _replaceParams.Add("Null", "null");

            _removeParams.Add("a_strConnectionString");
            _removeParams.Add("a_sConnectionString");
            _removeParams.Add("a_strConnection");
            _removeParams.Add("a_sConnection");

            _convertTypes.Add("int", "Int32");
            _convertTypes.Add("bool", "Boolean");
            _convertTypes.Add("long", "Int64");
            _convertTypes.Add("string", "String");
            _convertTypes.Add("double", "Double");
        }

        internal static string TransformTypeToConvert(string type)
        {
            return Transform(_convertTypes, type);
        }
    }
}
