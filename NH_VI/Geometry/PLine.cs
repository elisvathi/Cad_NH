using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Geometry
{
    public class PLine
    {
        public PVector Start { get; set; }
        public PVector End { get; set; }
        public PLine(PVector s, PVector e)
        {
            Start = s;
            End = e;
        }
        public PLine(double x1, double y1, double x2, double y2)
        {
            Start = new PVector(x1, y1);
            End = new PVector(x2, y2);
        }
        public double Length
        {
            get { return PVector.DistanceBetween(Start, End); }
        }
        public PLine()
        {
        }
        public PVector FullVector { get { return PVector.Sub(End, Start); } }
        public PVector UnitVector { get { var a = FullVector.Copy(); a.SetMag(1); return a; } }

        public List<PVector> Divide(int num)
        {
            var retVal = new List<PVector>();
            var segmentLength = 1 / num;
            for (int i = 0; i < num + 1; i++)
            {
                var val = i * segmentLength;
                retVal.Add(GetPointFromPosition(i));
            }

            return retVal;
        }
        public PVector MidPoint
        {
            get
            {
                return GetPointFromPosition(0.5);
            }
        }
        public void ExtendEnd(double newLength)
        {
            Scale(Start.Copy(), newLength / Length);
        }
        public void ExtendStart(double newLength)
        {
            Scale(End.Copy(), newLength / Length);
        }
        public void ExtendBothSides(double newLength)
        {
            Scale(MidPoint.Copy(), newLength / Length);
        }
        public void Scale(PVector basePoint, double scaleValue)
        {
            PVector v1 = PVector.Sub(End, basePoint);
            PVector v2 = PVector.Sub(Start, basePoint);
            v1.Mult(scaleValue);
            v2.Mult(scaleValue);
            Start = PVector.Add(basePoint, v2);
            End = PVector.Add(basePoint, v1);
        }
        public void ScaleFromPositionOnLine(double position, double value)
        {

            Scale(GetPointFromPosition(position), value);
        }
        public PVector GetPointFromPosition(double value)
        {
            var vec = FullVector.Copy();
            vec.Mult(value);
            vec.Add(Start);
            return vec;
        }
        public void ScaleFromStart(double scaleValue)
        {
            ScaleFromPositionOnLine(0, scaleValue);
        }
        public void ScaleFromEnd(double scaleValue)
        {
            ScaleFromPositionOnLine(1, scaleValue);
        }
        public void ScaleFromMidPoint(double scaleValue)
        {
            ScaleFromPositionOnLine(0.5, scaleValue);
        }
        public double M { get { return FullVector.Y / FullVector.X; } }
        public double B { get { return Start.Y - M * Start.X; } }
        public PVector Intersection(PLine b)
        {
            if (M == b.M)
                return null;
            else
            {
                var x = (b.B - B) / (M - b.M);
                var y = (M * b.B - b.M * B) / (M - b.M);
                return new PVector(x, y);
            }
        }
        public PVector ClosesetPoint(PVector p)
        {
            var a = FullVector.Copy();
            a.RotateDegrees(90);
            a.Add(p);
            var newLine = new PLine(p, a);
            return Intersection(newLine);
        }

        public bool ContainsClosestPoint(PVector p)
        {
            var v = ClosesetPoint(p);
            return PVector.DistanceBetween(v, Start) <= Length && PVector.DistanceBetween(v, End) <= Length;
        }
        public double DistanceFromPoint(PVector p)
        {
            return PVector.DistanceBetween(p, ClosesetPoint(p));
        }

        public void RotateGeometry(PVector basePoint,double angle)
        {
            Start.RotateGeometry(basePoint, angle);
            End.RotateGeometry(basePoint, angle);
        }
        public void HeadingTo(PVector target, PVector basePoint, PVector refPoint)
        {
            var vec = PVector.Sub(basePoint, refPoint);
            var vecTarget = PVector.Sub(basePoint, target);
            var ang = PVector.AngleBetweenDegrees(vec, vecTarget);
            RotateGeometry(basePoint, ang);
        }
    }
}
