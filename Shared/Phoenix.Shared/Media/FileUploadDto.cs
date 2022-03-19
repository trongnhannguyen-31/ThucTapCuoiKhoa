namespace Phoenix.Shared.Media
{
    public class FileUploadDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] Content { get; set; }
    }
}
