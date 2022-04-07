using System;

namespace Phoenix.Shared.ImageRecord
{
    public class ImageRecordDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string RelativePath { get; set; }
        public string AbsolutePath { get; set; }
        public bool IsExternal { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsUsed { get; set; }
        public bool Deleted { get; set; }
    }
}
