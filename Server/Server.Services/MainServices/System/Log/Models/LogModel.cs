using System;

namespace Phoenix.Server.Services.MainServices.System.Log.Models
{
    public class LogModel
    {
        public int Id { get; set; }

        public string ShortMessage { get; set; }

        public string FullMessage { get; set; }

        public string CreatedAt { get; set; }
    }
}
