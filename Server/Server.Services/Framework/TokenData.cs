using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.Framework
{
    public class TokenData
    {
        /// <summary>
        /// internal or external authenticate
        /// </summary>        
        public long UserId { get; set; }
       
        /// <summary>
        /// unit type: second
        /// </summary>
        public long LifeTime { get; set; }
    }
}
