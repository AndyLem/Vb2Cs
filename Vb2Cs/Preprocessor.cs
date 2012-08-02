using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vb2Cs
{
    public static class Preprocessor
    {
        public static string ReplaceStrings(string src, string p, string p_2)
        {
            while (src.IndexOf(p) >= 0)
                src = src.Replace(p, p_2);
            return src;
        }

    }
}
