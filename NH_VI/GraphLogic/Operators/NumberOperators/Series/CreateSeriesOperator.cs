using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;
using NH_VI.DataTypes.Numeric;

namespace NH_VI.GraphLogic.Operators.NumberOperators.Series
{
    public class CreateSeriesOperator : AbstractOperator
    {
        public override List<Type> InputTypes => new List<Type>() { typeof(PNumber), typeof(PNumber), typeof(PNumber) };

        public override List<Type> OutputTypes => new List<Type>() { typeof(PNumber) };

        public override bool TreeOperator => false;

        public override int MaxNumberOfSockets { get; set; } = 3;
        public override int MinimumNumberOfSockets { get; set; } = 3;
        public override int NumberOfOutputs { get; set; } = 1;

        protected override List<IData> OperateSimple(List<IData> dat)
        {
            var start = (dat[0] as PNumber).Value;
            var step = (dat[1] as PNumber).Value;
            var num = (dat[2] as PNumber).Value;
            var retVal = new DataTree();
            for (int i = 0; i < num; i++) {
                retVal.AddElement(new PNumber(start + step * i));
            }
            return new List<IData>() { retVal };
        }
    }
}
