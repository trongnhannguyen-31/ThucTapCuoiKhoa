using System;

namespace Phoenix.Server.Services.Helper
{
    public static class DateTimeExtensions
    {
        public static string ToVnDateTime(this DateTimeOffset date) => date.ToLocalTime().ToString("dd/MM/yyyy");
        public static string ToVnDateTime(this DateTime date) => date.ToString("dd/MM/yyyy");
    }
}
