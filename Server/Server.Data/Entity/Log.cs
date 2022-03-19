using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Logs")]
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        [MaxLength(100)]
        public string IpAddress { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public LogLevel LogLevel { get; set; }
    }
    public enum LogLevel
    {
        Debug = 10,
        Information = 20,
        Warning = 30,
        Error = 40,
        Fatal = 50
    }
}
