using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.Vendor
{
    [Table("ImageRecords")]
    public class VendorPhoto
    {
        [Key]
        public int Id { get; set; }

        public string FileName { get; set; }
    }
}