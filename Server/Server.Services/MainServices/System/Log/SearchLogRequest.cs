using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices.System.Log
{
    public class SearchLogRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string CampusCode { get; set; }
        public DateTimeOffset DateForm { get; set; }
        public DateTimeOffset DateTo { get; set; }
    }
}
