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
            X = x;Y = y;Z = z;
        }
        public void Add(PVector v2)
        {
            X += v2.X;
            Y += v2.Y;
            Z += v2.Z;
        }
        public PVector Copy() {
            return new PVector(X, Y, Z);
        }
        public static PVector operator +(PVector v1, PVector v2)
        {
            return PVector.Add(v1, v2);
        }
        public static PVector Add(PVector v1, PVector v2) {
            var v = v1.Copy();
            v1.Add(v2);
            return v1;
        }
    }
}
