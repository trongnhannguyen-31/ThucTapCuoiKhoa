namespace Phoenix.Server.Web.Areas.Admin.Models.Account
{
    public class UserRuleModel
    {
        public int Id { get; set; }
        public string SystemName { get; set; }
        public string Role { get; set; }
        public string InvokeRule { get; set; }
    }
}