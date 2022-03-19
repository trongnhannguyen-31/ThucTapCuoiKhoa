using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.Helper
{
    public static class EnumerableExtensions
    {
        public static string GetDescription(this Enum value)
        {
            if (value == null) return string.Empty;
            // Get the Description attribute value for the enum value
            var fi = value.GetType().GetField(value.ToString());
            var attributes = fi.GetCustomAttribute(typeof(DescriptionAttribute), false) as DescriptionAttribute;
            return attributes?.Description ?? value.ToString();
        }
    }
}
