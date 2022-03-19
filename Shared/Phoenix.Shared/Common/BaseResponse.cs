using System.Collections.Generic;

namespace Phoenix.Shared.Common
{
    public class BaseResponse<T>
    {
        public List<T> Data { get; set; }
        public int DataCount { get; set; }
        public bool success { get; set; }
    }
}
