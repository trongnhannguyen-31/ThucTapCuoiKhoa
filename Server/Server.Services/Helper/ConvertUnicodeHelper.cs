using System.Text;
using System.Text.RegularExpressions;

namespace Phoenix.Server.Services.Helper
{
    public static class ConvertUnicodeHelper
    {
        public static string ConvertUnicode(string str)
        {
            var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            str = str.ToLower();
            str = Regex.Replace(str, @"\s+", " ");
            var temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
