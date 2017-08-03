using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;
using NH_VI.Geometry;
using NH_VI.DataTypes.Numeric;

namespace NH_VI.GraphLogic.Operators.GeometryOperators
{
    public class DeconstructVectorOperation : AbstractOperator
    {
        public override List<Type> InputTypes => new List<Type>() { typeof(PVector) };

        public override List<Type> OutputTypes => new List<Type>() { typeof(PNumber), typeof(PNumber), typeof(PNumber) };

        public override bool TreeOperator => false;

        public override int MaxNumberOfSockets { get; set; } = 1;
        public override int MinimumNumberOfSockets { get; set; } = 1;
        public override int NumberOfOutputs { get; set; } = 3;

        protected override List<IData> OperateSimple(List<IData> dat)
        {
            if (dat.Count > 0) { 
            var v = dat[0] as PVector;
                if (v != null) {
            return new List<IData>() { new PNumber(v.X), new PNumber(v.Y), new PNumber(v.Z) };
                }
                else
                {
                    return new List<IData>();
                }
            }
            else
            {
                return new List<IData>();
            }
        }
    }
}
