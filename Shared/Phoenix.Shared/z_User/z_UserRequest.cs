namespace Phoenix.Shared.z_User
{
    public class z_UserRequest
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string AvatarUrl { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public bool Active { get; set; }

        public string Roles { get; set; }

        public bool Deleted { get; set; }
    }
}
