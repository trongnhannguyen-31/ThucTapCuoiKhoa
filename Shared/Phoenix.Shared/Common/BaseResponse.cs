using System.Collections.Generic;

namespace Phoenix.Shared.Common
{
    public class BaseResponse<T>
    {
        public List<T> Data { get; set; }

        public T Record { get; set; }
        public int DataCount { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
