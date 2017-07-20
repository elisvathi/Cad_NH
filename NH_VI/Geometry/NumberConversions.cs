using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.MathExtenions
{
   public static class NumberConversions
    {
        public static double ToDouble(this float val)
        {
            return (double)val;
        }
        public static float ToFloat(this double val)
        {
            return (float)val;
        }
        public static double ToRadians(this double val)
        {
            return val * Math.PI / 180;
        }
        public static double ToDegrees(this double val)
        {
            return val * (180 / Math.PI);
        }
        public static double Map(this double val, double x1, double y1, double x2, double y2) {
            var dif1 = y1 - x1;
            var rap = (val - x1) / dif1;
            var dif2 = y2 - x2;
            return rap * dif2 + x2;
        }
        public static double FixAngleRadians(this double ang) {
            var retVal = ang;
            var rot = Math.PI * 2;
            retVal %= rot;
            if (retVal < 0) { retVal = rot - Math.Abs(retVal); }
            return retVal;
        }
        public static double FixAngleDegrees(this double ang) {
            return FixAngleRadians(ang.ToRadians()).ToDegrees();
        }

    }
}
