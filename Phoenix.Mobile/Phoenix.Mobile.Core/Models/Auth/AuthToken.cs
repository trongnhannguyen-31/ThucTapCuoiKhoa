using System;

namespace Phoenix.Mobile.Core.Models.Auth
{
    public class AuthToken
    {
        public DateTimeOffset ExpiredAt { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }
        //customer
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}
