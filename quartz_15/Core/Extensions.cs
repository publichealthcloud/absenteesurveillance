using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz
{
    public static class StringExtensions
    {
        public static string Shorten(this string input, int max, string append)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            
            return input.Length <= max ? input : (input.Substring(0, max) + append);
        }

        public static string PossessiveAdjective(this string s)
        {
            return (!string.IsNullOrWhiteSpace(s) && s.ToLower() == "female") ? "her" : "his";
        }
    }
}
