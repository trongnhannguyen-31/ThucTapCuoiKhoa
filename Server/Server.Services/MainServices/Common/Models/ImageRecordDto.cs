using System;

namespace Phoenix.Server.Services.MainServices.Common.Models
{
    public class ImageRecordDto
    {
        public string FileName { get; set; }      
        public string AbsolutePath { get; set; }       
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Code { get; set; }  
    }
}
