using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.User
{
    public class UserDto
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
