using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.Geometry
{
   public class PCurve
    {
        public List<PVector> pts;
        public PCurve(List<PVector> points)
        {
            pts  = points;
        }


        public PVector GetPointAt(float t)
        {
            return GetFinal(pts, t);
        }
        public List<PVector> GetCurveData(int num)
        {
            return GetCurvePoints(pts, num);
        }
        public List<PVector> GetCurveData()
        {
            return GetCurveData(100);
        }


        public static PCurve GetNodeCurve(PVector pt1, PVector pt2)
        {
            var p1 = pt1.Copy();
            p1.Z = 0;
            var p2 = pt2.Copy();
            p2.Z = 0;
            var wdif = 200;
            PVector p1A = new PVector(p1.X + wdif, p1.Y);
            PVector p2A = new PVector(p2.X - wdif, p2.Y);
            var ls = new List<PVector>() { p1, p1A, p2A, p2 };
            return new PCurve(ls);

        }

        private static List<PVector> GetNext(List<PVector> points, float t)
        {
            var retVal = new List<PVector>();
            for(int i = 0;i< points.Count - 1; i++)
            {
                var p = PVector.Sub(points[i + 1], points[i]);
                p.Mult(t);
                p.Add(points[i]);
                retVal.Add(p);
            }
            return retVal;
        }
        private static PVector GetFinal(List<PVector> vec, float t)
        {
            var l = new List<PVector>(vec);
            while (l.Count > 1)
            {
                l = GetNext(l, t);
            }
            return l[0];
        }

        private static List<PVector> GetCurvePoints(List<PVector> cp, int steps)
        {
            List<PVector> retval = new List<PVector>();
            float st = 1.0F / (float)steps;
            for(int i =0;i< steps; i++)
            {
                retval.Add(GetFinal(cp, i * st));
            }
            return retval;
        }
    }
}
