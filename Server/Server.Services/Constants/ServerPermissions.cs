using System.Collections.Generic;

namespace Phoenix.Server.Services.Constants
{
    public sealed class ServerPermissions
    {
        #region Admin

        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Typer = "Typer";
        public const string user = "user";

        //admin area permissions
        public const string AccessAdminPanel = "AccessAdminPanel";
        public const string ManageFalconUsers = "ManageFalconUsers";


        #endregion
        public static List<KeyValuePair<string, string>> DefaultClaims => new List<KeyValuePair<string, string>>()
        {
            //web admin
            new KeyValuePair<string,string>(Admin, AccessAdminPanel),
            new KeyValuePair<string,string>(Admin,ServerPermissions.ManageFalconUsers),

            new KeyValuePair<string,string>(Manager,ServerPermissions.AccessAdminPanel),
            
            new KeyValuePair<string,string>(Typer,ServerPermissions.AccessAdminPanel),
        };
    }
}

