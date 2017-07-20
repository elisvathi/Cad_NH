using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Geometry.GeometryShapes
{
  public  class PointCollectionGeometry : IGeometricShape
    {
        private List<PVector> _pointData;
        public PointCollectionGeometry()
        {
            _pointData = new List<PVector>();
        }
        public PointCollectionGeometry(List<PVector> data)
        {
            _pointData = data;
        }
        public double Length
        {
            get
            {
                double retval = 0;
                for (int i = 0; i < _pointData.Count - 1; i++)
                {
                    retval += PVector.DistanceBetween(_pointData[i], _pointData[i + 1]);
                }
                return retval;
            }
        }
        public List<PVector> ControlPoints => new List<PVector>(_pointData);

        public List<PVector> PointData { get => _pointData;}

        public void AddStartPoint(PVector p)
        {
            _pointData.Add(p);
        }

        public void AddUpdatePoint(PVector p)
        {
            _pointData.Add(p);
        }

        public void FinalPoint(PVector p)
        {

        }

        public List<PVector> GetDividePoints(int n)
        {
            var dist = (Length/n)/Length ;
            var retVal = new List<PVector>();
            for (int i = 0; i < n; i++) {
                retVal.Add(PositionAt(i * dist));
            }
            retVal.Add(_pointData.Last());
            return retVal;
        }
        public PVector PositionAt(double val)
        {
            double value;
            if (val > 1) { value = 1; } else if (val < 0) { value = 0; } else { value = val; }
            var dist = Length*value;
            double actualDist = 0;
            int actualIndex = 0;
            while (actualDist <= dist && actualIndex < _pointData.Count)
            {
                if (actualDist + PVector.DistanceBetween(_pointData[actualIndex], _pointData[actualIndex + 1]) >= dist)
                {
                    var remainingValue = dist - actualDist;
                    var vec = PVector.Sub(_pointData[actualIndex + 1], _pointData[actualIndex]);
                    vec.SetMag(remainingValue);
                    vec.Add(_pointData[actualIndex]);
                    return vec;

                }
                else
                {
                    actualDist += PVector.DistanceBetween(_pointData[actualIndex], _pointData[actualIndex + 1]);
                    actualIndex++;
                }
            }
            return _pointData.Last();

        }
        public void UpdateWithControlPoints(List<PVector> data)
        {
            _pointData = new List<PVector>(data);
        }
    }
}
