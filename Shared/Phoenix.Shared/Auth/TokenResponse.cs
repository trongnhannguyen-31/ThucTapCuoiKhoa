namespace Phoenix.Shared.Auth
{
    public class TokenResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// minutes
        /// </summary>
        public int ExpiresIn { get; set; }
        //public string RefreshToken { get; set; }
        public bool IsError { get; set; }
        public string ErrorDescription { get; set; }
        public string AccessToken { get; set; }
    }
}