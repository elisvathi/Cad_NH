using Pen.MathExtenions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Geometry
{
    public class PVector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public PVector(double x, double y)
        {
            X = x; Y = y;
        }
        public PVector(float x, float y)
        {
            X = x; Y = y;
        }
        public void Add(PVector v)
        {
            X += v.X;
            Y += v.Y;
        }
        public void Mult(double val)
        {
            X *= val;
            Y *= val;
        }
        public PVector Copy()
        {
            return new PVector(X, Y);
        }
        public void Sub(PVector v)
        {
            var vv = v.Copy();
            vv.Mult(-1);
            Add(vv);
        }
        public void Normalize()
        {
            SetMag(1);
        }
        public double Mag
        {
            get
            {
                return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            }
        }
        public void SetMag(double val)
        {
            Mult(val / Mag);
        }
        public double AngleRadians
        {
            get
            {
                var val =  Math.Atan(Y / X);
                
                if (X > 0)
                {
                    if (val <= 0) { return Math.PI / 2 - Math.Abs(val); } else { return val + Math.PI / 2; }
                }
                else if (X < 0)
                {
                    if (val >= 0) { return Math.PI * 2 - (Math.PI / 2 - val); } else { return Math.PI * 2 - (Math.PI / 2 + Math.Abs(val)); }
                }
                else { if (Y <= 0) { return 0; } else { return Math.PI; } }
                
            }
        }
       

        private double FixLargerAngle(double ang) {
            var val = ang;
            if (val > Math.PI * 2) { val %= Math.PI * 2; }
            return val;
        }
        private double FixNegativeAngle(double ang)
        {
            var a = FixLargerAngle(Math.Abs(ang));
            if (ang < 0) { a = Math.PI * 2 - a; return a; } else { return ang; }
        }
        public double AngleDegrees
        {
            get
            {
                return AngleRadians.ToDegrees();
            }
        }
        public void RotateRadians(double radians)
        {
            var ca = Math.Cos(radians);
            var sa = Math.Sin(radians);
            var tempX = ca * X - sa * Y;
            var tempY = sa * X + ca * Y;
            X = tempX;
            Y = tempY;
        }
        public void RotateDegrees(double degrees)
        {
            RotateRadians(degrees.ToRadians());
        }

        public double DistanceTo(PVector v)
        {
            var a = v.Copy();
            a.Sub(this);
            return a.Mag;
        }
        public double AngleWithRadians(PVector b)
        {
            return Math.Atan2(b.Y - Y, b.X - X);
        }
        public double AngleWithDegrees(PVector b)
        {
            return AngleWithRadians(b).ToDegrees();
        }

        public static PVector Sub(PVector a, PVector b)
        {
            var v1 = a.Copy();
            v1.Sub(b);
            return v1;
        }
        public static PVector Add(PVector a, PVector b)
        {
            var v1 = a.Copy();
            v1.Add(b);
            return v1;
        }
        public static double DistanceBetween(PVector a, PVector b)
        {
            return a.DistanceTo(b);
        }
        public static double AngleBetweenRadians(PVector a, PVector b)
        {
            return a.AngleWithRadians(b);
        }

        public static double AngleBetweenDegrees(PVector a, PVector b)
        {
            return a.AngleWithDegrees(b);
        }
        public static PVector  GetFromRotationRadians(PVector a, double radians)
        {
            var retVal = a.Copy();
            retVal.RotateRadians(radians);
            return retVal;
        }
        public static PVector GetFromRotationDegrees(PVector a, double degrees)
        {
            return PVector.GetFromRotationRadians(a, degrees.ToRadians());
        }
        public void DebugVector()
        {
            System.Diagnostics.Debug.WriteLine("VECOTR: X-> " + X+ "Y->" + Y);
        }
        public void RotateGeometry(PVector basePoint, double angleDegrees)
        {
            var vec = PVector.Sub(this, basePoint);
            vec.RotateDegrees(angleDegrees);
            vec.Add(basePoint);
            X = vec.X;
            Y = vec.Y;
        }
    }
}
