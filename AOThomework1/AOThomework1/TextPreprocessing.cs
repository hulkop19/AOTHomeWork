using System;
using System.Collections.Generic;
using System.Text;

namespace AOThomework1
{
    public static class TextPreprocessing
    {
        public static List<string> GetTokens(string str)
        {
            var sb = new StringBuilder();
            foreach (var c in str)
            {
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                {
                    sb.Append(char.ToLower(c));
                }
            }

            return new List<string>(sb.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
