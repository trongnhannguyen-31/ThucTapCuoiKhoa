using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CongDongBau.Server.Web.Areas.Admin.Models.Import
{
    public class ImportPromotionModel : ImportExcelModel
    {
        public int? PartnerId { get; set; }
        public int? StoreId { get; set; }
        public int? HospitalId { get; set; }
    }
}