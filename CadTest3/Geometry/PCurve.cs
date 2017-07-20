using Pen.Geometry.GeometryShapes;
using Pen.MathExtenions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Geometry
{
    public class PCurve : ICurve
    {

        List<PVector> _points;
        PointCollectionGeometry calculatedCurve;
        int precision = 100;
        float koef = 0.33F;
        int tightnesSteps = 1;
        private CurveType _cType;
        public CurveType CType { get { return _cType; } set { _cType = value; CalculateCurve(); } }

        public int Precision { get => precision; set { precision = value; CalculateCurve(); } }

        public float Koef { get => koef; set { koef = value; if (CType == CurveType.Normal) { CalculateCurve(); } } }

        public float Length => calculatedCurve != null ? calculatedCurve.Length.ToFloat() : 0;

        public List<PVector> RawCurveData => calculatedCurve.PointData;

        private void CalculateCurve()
        {
            switch (CType)
            {
                case CurveType.Loose:
                    {
                        calculatedCurve = new PointCollectionGeometry(GetCurveData(_points, precision * _points.Count));
                        break;
                    }

                case CurveType.Tight:
                    {
                        calculatedCurve = new PointCollectionGeometry(GetSecondCurveType(_points, precision));
                        break;
                    }
                case CurveType.Normal:
                 {
                        calculatedCurve = new PointCollectionGeometry(GetThirdCurveType(_points, precision, koef));
                        break;
                    }
                case CurveType.None:
                default:
                    {
                        calculatedCurve = new PointCollectionGeometry(_points);
                        break;
                    }
            }
        }


        public PCurve(List<PVector> pts, CurveType ct)
        {
            _points = pts;
            CType = ct;
        }

        public void AddControlPoint(PVector vec, int pos)
        {
            if (pos >= _points.Count || pos < 0)
            {
                _points.Add(vec);
            }
            else
            {
                _points.Insert(pos, vec);
            }
            CalculateCurve();
        }
        public void RemoveControlPoint(PVector vec)
        {
            if (_points.Contains(vec)) { _points.Remove(vec); CalculateCurve(); }

        }
        public void RemoveControlPoint(int ind)
        {
            if (ind >= 0 && ind < _points.Count)
            {
                RemoveControlPoint(_points[ind]);
            }
        }

        public PVector GetPointAtParameter(float t)
        {
            return calculatedCurve.PositionAt(t);
        }

        public PVector GetPointAtLength(float t)
        {
            return calculatedCurve.PositionAt(t / calculatedCurve.Length);
        }

        public List<PVector> DivideNumber(int n)
        {
            return calculatedCurve.GetDividePoints(n);
        }

        public List<PVector> DivideLength(float length)
        {

            int ndiv = (int)(Length / length);
            return calculatedCurve.GetDividePoints(ndiv);
        }

        public Tuple<PVector, double> ClosestPoint(PVector vec)
        {
            var dat = calculatedCurve.PointData;
            PVector cp = dat[0];
            double testLength = double.MaxValue;
            for(int i = 0;i< dat.Count - 1; i++)
            {
                PLine l = new PLine(dat[i], dat[i + 1]);
                PVector temp;
                double tempLen;
                if (l.ContainsClosestPoint(vec))
                {
                    temp = l.ClosesetPoint(vec);
                    tempLen = l.DistanceFromPoint(vec);
                }
                else
                {
                    var v1 = PVector.Sub(l.Start, vec);
                    var v2 = PVector.Sub(l.End, vec);
                    if (v1.Mag < v2.Mag) { temp = v1; tempLen = v1.Mag; } else { temp = v2; tempLen = v2.Mag; }

                }
                if (tempLen < testLength) { testLength = tempLen; cp = temp; }
                
            }
            return new Tuple<PVector, double>(cp, testLength);
        }
        public double DistanceFromPoint(PVector vec)
        {
            return ClosestPoint(vec).Item2;
        }

        public PVector PerpAtParameter(float param)
        {
            return PerpAtLength(param * Length);
        }

        public PVector PerpAtLength(float length)
        {
            float lim = 0.1F;
            PVector p1 = GetPointAtLength(length);
            PVector p2 = GetPointAtLength(length + lim);
            PVector vec = PVector.Sub(p2, p1);
            vec.Normalize();
            return vec;
        }

        #region Static Methods
        private static PVector GetLineParam(PLine l, float t)
        {
            return l.GetPointFromPosition(t);
        }
        private static List<PVector> GetNext(List<PVector> pts, float t)
        {
            var retVal = new List<PVector>();
            for (int i = 0; i < pts.Count - 1; i++)
            {
                var l = new PLine(pts[i], pts[i + 1]);
                retVal.Add(GetLineParam(l, t));
            }
            return retVal;
        }
        private static PVector GetFinal(List<PVector> vec, float t)
        {
            var g = vec;
            while (g.Count > 1)
            {
                g = GetNext(g, t);
            }
            return g[0];
        }
        private static List<PVector> GetCurveData(List<PVector> points, int steps)
        {
            var retVal = new List<PVector>();

            float stepSize = (float)1.0F / (float)steps;
            for (float i = 0; i <= 1; i+=stepSize)
            {
                retVal.Add(GetFinal(points, i));
            }

            return retVal;
        }
        private static List<PVector> GetSecondCurveType(List<PVector> points, int steps)
        {
            var retVal = new List<PVector>();

            int i = 0;
            while (i < points.Count - 2)
            {
                PVector p1, p2, p3;
                if (i == 0) { p1 = points[i]; }
                else
                {
                    var temp = PVector.Sub(points[i + 1], points[i]);
                    temp.Mult(0.5);
                    temp.Add(points[i]);
                    p1 = temp;
                }
                p2 = points[i + 1];
                var temp2 = PVector.Sub(points[i + 2], points[i + 1]);
                temp2.Mult(0.5);
                temp2.Add(points[i + 1]);
                p3 = temp2;
                var lst = new List<PVector>() { p1, p2, p3 };
                var curve = GetCurveData(lst, steps);
                if (i > 0) { curve.RemoveAt(0); }
                var rv = retVal.Concat(curve);
                retVal = rv.ToList();
            }
            retVal.Add(points.Last());
            return retVal;
        }
        private static List<PVector> GetThirdCurveType(List<PVector> points, int steps, float koef)
        {
            var newPoints = new List<PVector>();
            newPoints.Add(points[0]);
            for (int i = 1; i < points.Count - 1; i++)
            {
                PVector pt = points[i];
                PVector left = points[i - 1];
                PVector right = points[i + 1];
                PVector direction = PVector.Sub(right, left);
                double leftSide = PVector.Sub(pt, left).Mag;
                double rightSide = PVector.Sub(pt, right).Mag;
                leftSide *= koef;
                rightSide *= koef;
                var vl = direction.Copy();
                vl.SetMag(leftSide * -koef);
                var vr = direction.Copy();
                vr.SetMag(rightSide * koef);
                vl.Add(pt); vr.Add(pt);
                newPoints.Add(vl);
                newPoints.Add(pt);
                newPoints.Add(vr);
            }
            newPoints.Add(points.Last());
            return GetCurveData(newPoints, steps);
        }

     
        #endregion
    }
}
