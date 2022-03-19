using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int zUser_Id { get; set; }
    }
}
