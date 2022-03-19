namespace Phoenix.Shared.Auth
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DeviceType { get; set; }
        public string DeviceId { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public string BinaryVersion { get; set; }
    }
}
