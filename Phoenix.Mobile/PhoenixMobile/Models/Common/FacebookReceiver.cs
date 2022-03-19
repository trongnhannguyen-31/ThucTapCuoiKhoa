using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.Mobile.Models.Common
{
    public class FacebookReceiver
    {
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public string first_name { get; set; }
        public string email { get; set; }
        public PictureReceiver picture { get; set; }
    }
    public class PictureReceiver
    {
        public DataReceiver data { get; set; }
    }
    public class DataReceiver
    {
        public string Url { get; set; }
    }
}
