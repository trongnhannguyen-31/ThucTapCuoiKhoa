using System;

namespace Phoenix.Server.Web.Areas.Admin.Models
{
    public class BaseModel : DateModel
    {
        public bool WhereCreatedAt { get; set; }
        public DateTime? fromDt { get; set; }
        public DateTime? toDt { get; set; }
        public bool WhereActiveDate { get; set; }
        public DateTime? ActivefromDt { get; set; }
        public DateTime? ActivetoDt { get; set; }
        public bool WhereExpireDate { get; set; }
        public DateTime? ExpirefromDt { get; set; }
        public DateTime? ExpiretoDt { get; set; }
    }
    public class DateModel
    {
        public DateModel()
        {
            DateFrom = DateTime.Now.ToString("dd/MM/yyyy");
            DateTo = DateTime.Now.ToString("dd/MM/yyyy");
        }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}