using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Geometry.GeometryShapes
{
    public enum CurveType {
        Loose,
        Tight,
        Normal,
        None
    }
   public interface ICurve
    {
        CurveType CType { get; set; }
        PVector GetPointAtParameter(float t);
        PVector GetPointAtLength(float t);
        List<PVector> DivideNumber(int n);
        List<PVector> DivideLength(float length);
        List<PVector> RawCurveData { get; }
        Tuple<PVector , double> ClosestPoint(PVector vec);
        PVector PerpAtParameter(float t);
        PVector PerpAtLength(float lengh);
        float Length { get; }
        double DistanceFromPoint(PVector vec);
    }
}
