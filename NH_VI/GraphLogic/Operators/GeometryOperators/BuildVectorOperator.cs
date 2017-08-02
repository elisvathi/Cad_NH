using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;
using NH_VI.DataTypes.Numeric;
using NH_VI.Geometry;

namespace NH_VI.GraphLogic.Operators
{
    public class BuildVectorOperator : AbstractOperator
    {
        public override List<Type> InputTypes => new List<Type>() { typeof(PNumber), typeof(PNumber), typeof(PNumber) };

        public override List<Type> OutputTypes => new List<Type>() { typeof(PVector) };

        public override bool TreeOperator => false;

        public override int MaxNumberOfSockets { get; set; } = 3;
        public override int MinimumNumberOfSockets { get; set; } = 3;
        public override int NumberOfOutputs { get; set; } = 1;

        protected override List<IData> OperateSimple(List<IData> dat)
        {
            var x = (dat[0] as PNumber).Value;
            var y = (dat[1] as PNumber).Value;
            var z = (dat[2] as PNumber).Value;
            return new List<IData>() { new PVector(x, y, z) };
        }
    }
}
