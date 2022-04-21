namespace Phoenix.Server.Web.Areas.Admin.Models.Account
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public bool? Active { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public bool Deleted { get; set; }
    }
}