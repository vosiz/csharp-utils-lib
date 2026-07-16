using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vosiz.Extends
{
    public static class DoubleExt
    {

        // Clamps the value between min and max
        public static double Clamp(this double value, double min, double max)
        {

            return Math.Min(Math.Max(value, min), max);
        }

        // Moves the value towards target by at most rate
        public static double Lerp(this double value, double target, double rate)
        {

            rate = Math.Abs(rate);

            if (target < value)
                return Math.Max(value - rate, target);
            else
                return Math.Min(value + rate, target);
        }
    }
}
