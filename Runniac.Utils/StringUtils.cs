using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Runniac.Utils
{
    /// <summary>
    /// Provides string utility methods.
    /// </summary>
    public static class StringUtils
    {
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (c != '"' && c != '\\')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
