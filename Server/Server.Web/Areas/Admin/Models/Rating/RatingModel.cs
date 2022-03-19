using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.Rating
{
    public class RatingModel
    {
        public int Rate { get; set; }

        public string Comment { get; set; }

        public string Customer_Name { get; set; }
    }
}