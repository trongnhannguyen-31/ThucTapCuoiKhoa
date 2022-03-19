using System.Text;
using System.Text.RegularExpressions;

namespace Phoenix.Shared.Common
{
    public static class SharedValues
    {
        /// <summary>
        /// Dau ngan cach cac id trong 1 string
        /// </summary>
        public const char Delimiter = ';';
        public const string DelimiterString = ";";
        /// <summary>
        /// Dau ngan cach cac phan trong string Id
        /// </summary>
        public const char PartSeparator = '-';
        public const string PartSeparatorString = "-";
        /// <summary>
        /// Dau ngan cach cac phan trong string code
        /// </summary>
        public const char CodeSeparator = '.';
        public const string CodeSeparatorString = ".";
        /// <summary>
        /// format vietname datetime
        /// </summary>
        public const string VNFormat = "dd/MM/yyyy HH:mm";

    }
}
