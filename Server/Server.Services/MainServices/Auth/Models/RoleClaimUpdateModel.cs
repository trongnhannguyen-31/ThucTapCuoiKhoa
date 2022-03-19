using System.Collections.Generic;

namespace Phoenix.Server.Services.MainServices.Auth.Models
{
    public class RoleClaimUpdateModel
    {
        public RoleClaimUpdateModel()
        {
            RoleClaims = new List<RoleClaim>();
        }
        public IList<RoleClaim> RoleClaims { get; set; }
    }
    public class RoleClaim
    {
        public string ClaimSystemName { get; set; }
        public string RoleSystemName { get; set; }
    }
}
