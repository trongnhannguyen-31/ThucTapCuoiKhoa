using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.Mobile.Core.Framework
{
    public static class FileHelper
    {
        public static string GetFileType(this string path)
        {
            var dotIndex = path.LastIndexOf('.');
            if (dotIndex < 0 || dotIndex >= path.Length - 1) return "";
            return path.Substring(dotIndex + 1);
        }
        public static string GetFileName(this string path)
        {
            var splashIndex = path.LastIndexOf('/');
            if (splashIndex < 0 || splashIndex >= path.Length - 1) return path;
            return path.Substring(splashIndex + 1);
        }
    }
}
