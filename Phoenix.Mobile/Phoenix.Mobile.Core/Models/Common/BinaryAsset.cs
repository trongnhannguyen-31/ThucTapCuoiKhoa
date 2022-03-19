using Phoenix.Shared.Media;

namespace Phoenix.Mobile.Core.Models.Common
{
    public class BinaryAsset : FileUploadDto
    {
        public string Guid { get; set; }
        public string Path { get; set; }
        public string Ext { get; set; }
        public bool HasImage { get; set; }
    }
}
