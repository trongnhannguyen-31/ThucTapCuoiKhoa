using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices.Media.Models
{
    public class ImageResponse
    {
        public bool IsOk { get; set; }
        public string Message { get; set; }

        public ImageModel Image { get; set; }
    }
}
