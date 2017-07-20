using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.Geometry
{
    public class PVector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public PVector(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
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
