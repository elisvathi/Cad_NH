using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NH_VI.Geometry
{
    public class PVector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public PVector(double x, double y, double z = 0)
        {
            X = x; Y = y; Z = z;
        }
        
        public PVector(System.Windows.Point p):this(p.X, p.Y)
        {
           
        }
        public Point GetPoint()
        {
            return new Point(X, Y);
        }
        public void Add(PVector v2)
        {
            X += v2.X;
            Y += v2.Y;
            Z += v2.Z;
        }

        public void Sub(PVector v2)
        {
            X -= v2.X;
            Y -= v2.Y;
            Z -= v2.Z;
        }



        public double Mag { get { return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)); } }

        public void Mult(double val)
        {
            X *= val;
            Y *= val;
            Z *= val;
        }
        public void SetMag(double val)
        {
            Mult(val / Mag);
        }

        public double Dot(PVector v)
        {
            return X * v.X + Y * v.Y + Z * v.Z;
        }
        public PVector Cross(PVector v)
        {
            var x = MatCalc(Y, Z, v.Y, v.Z);
            var y = MatCalc(X, Z, v.X, v.Z);
            var z = MatCalc(X, Y, v.X, v.Y);
            return new PVector(x, y, z);
        }
        public void Normalize()
        {
            SetMag(1);
        }
        public PVector ProjectTo(PVector vec)
        {
            var v = vec.Copy();
            v.Mult(Dot(vec));
            return v;
        }

        public void Rotate3D(PVector axis, double theta)
        {
            var vec = Rotate3D(this, axis, theta);
            X = vec.X;
            Y = vec.Y;
            Z = vec.Z;
        }

        public void RotateYPR(double yaw, double pitch, double roll)
        {
            var x = new PVector(1, 0, 0);
            var y = new PVector(0, 1, 0);
            var z = new PVector(0, 0, 1);
            Rotate3D(z, yaw);
            x.Rotate3D(z, yaw);
            y.Rotate3D(z, yaw);
            Rotate3D(x, pitch);
            y.Rotate3D(x, pitch);
            Rotate3D(y, roll);
        }

        public static PVector Rotate3D(PVector vector, PVector axis, double theta)
        {
            var vec = axis.Copy();
            vec.Normalize();
            double scalar_1 = (1 - Math.Cos(theta))*(Dot(vector, vec));
            double scalar_2 = Math.Cos(theta);
            double scalar_3 = Math.Sin(theta);
            PVector norm = vec.Copy();
            PVector tv = vector.Copy();
            PVector crs = Cross(norm, tv);
            norm.Mult(scalar_1);
            tv.Mult(scalar_2);
            crs.Mult(scalar_3);
            var retVal = norm.Copy();
            retVal.Add(tv);
            retVal.Add(crs);
            return retVal;
        }

        public static PVector Cross(PVector a, PVector b)
        {
            return a.Cross(b);
        }

        public static double Dot(PVector a, PVector b)
        {
            return a.Dot(b);
        }

        private double MatCalc(double x1, double x2, double y1, double y2)
        {
            return x1 * y2 - x2 * y1;
        }

        public PVector Copy()
        {
            return new PVector(X, Y, Z);
        }
        public static PVector operator +(PVector v1, PVector v2)
        {
            return PVector.Add(v1, v2);
        }

        public static PVector operator -(PVector v1, PVector v2)
        {
            return PVector.Sub(v1, v2);
        }

        public static PVector Add(PVector v1, PVector v2)
        {
            var v = v1.Copy();
            v1.Add(v2);
            return v1;
        }
        public static PVector Sub(PVector v1, PVector v2)
        {
            var v = v1.Copy();
            v.Sub(v2);
            return v;
        }
    }
}
