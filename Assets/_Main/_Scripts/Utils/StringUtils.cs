using System.Text.RegularExpressions;

namespace _Main._Scripts.Utils
{
    public static class StringUtils
    {
        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }
    }
}