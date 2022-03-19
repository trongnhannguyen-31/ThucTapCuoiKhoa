using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace CongDongBau.Server.Web.Areas.Admin.Models.Import
{
    public class ImportExcelModel
    {
        public string FileName { get; set; }
        public bool SaveToDb { get; set; }
    }
}