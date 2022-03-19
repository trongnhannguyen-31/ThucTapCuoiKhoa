using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.Constants
{
    public class Calculates
    {
        public static double ParseToBasicValue(double basicValue, double value, double quantity)
        {
            return (value / basicValue) * quantity;
        }
    }
}
