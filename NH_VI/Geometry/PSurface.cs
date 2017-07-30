using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.Geometry
{
  public  class PSurface
    {
        List<PCurve> crv;
        public PSurface(List<PCurve> curves)
        {
            crv = curves;
        }
        public PCurve GetCuve(float u)
        {
            List<PVector> retVal = new List<PVector>();
            for(int i = 0;i< crv.Count; i++)
            {
                retVal.Add(crv[i].GetPointAt(u));
            }
            return new PCurve(retVal);
        }
        public PVector GetPointAt(float u, float v)
        {
            return GetCuve(u).GetPointAt(v);
        }
    }
}
