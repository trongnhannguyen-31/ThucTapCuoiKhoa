namespace Phoenix.Server.Services.MainServices.Media.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string RelativePath { get; set; }
        public string AbsolutePath { get; set; }
        public bool IsExternal { get; set; }
        public string CreatedAt { get; set; }
        public bool IsUsed { get; set; }

        public bool ContinueEditing { get; set; }
    }
}
