using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("z_Users")]
    public class z_User
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string AvatarUrl { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public bool Active { get; set; }

        public string Role { get; set; }

        public bool Deleted { get; set; }
    }
}
