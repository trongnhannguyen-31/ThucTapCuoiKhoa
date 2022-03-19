using System.Collections.Generic;

namespace Phoenix.Shared.Core
{
    public class ListDto<T> 
    {
        public IList<T> Items { get; set; }
    }
}
