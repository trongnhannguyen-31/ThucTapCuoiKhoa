namespace Phoenix.Shared.Auth
{
    public class ExternalTokenRequest
    {
        public string Provider { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string LastName { get; set; }
    }
}
