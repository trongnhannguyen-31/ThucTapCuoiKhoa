using Falcon.Web.Core.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Data.Entity
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int zUser_Id { get; set; }
        [ForeignKey("zUser_Id")]
        public virtual User zUser { get; set; }
    }
}
