using System.Collections.Generic;

namespace Phoenix.Server.Services.MainServices.Auth.Models
{
    public class RoleClaimModel
    {
        public RoleClaimModel()
        {
            Claim = new List<ClaimModel>();
        }
        public string SystemName { get; set; }
        public string Display { get; set; }
        public IList<ClaimModel> Claim { get; set; }
    }
    public class ClaimModel
    {
        public string SystemName { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}
