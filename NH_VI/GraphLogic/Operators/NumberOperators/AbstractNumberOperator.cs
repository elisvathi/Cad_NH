using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;
using NH_VI.DataTypes.Numeric;

namespace NH_VI.GraphLogic.Operators
{
    public enum NumberOperationTypes
    {
        Add, Subtract, Multiply, Divide, Power, Equals, GreaterThan, SmallerThan, GreaterThanEquals, SmallerThanEquals
    }
    public abstract class AbstractNumberOperator : AbstractOperator
    {
        public override List<List<Type>> PossibleTypes => new List<List<Type>> { new List<Type>() { typeof(PNumber) }, new List<Type>() { typeof(PNumber) } };

        public override bool TreeOperator => false;
        public NumberOperationTypes OpType { get; set; } = NumberOperationTypes.Add;
        public override int MaxNumberOfSockets { get => 2; set { } }
        public override int MinimumNumberOfSockets { get => 2; set { } }
        public override int NumberOfOutputs { get => 1; set { } }

        protected override List<IData> OperateSimple(List<IData> dat)
        {
            var a = dat[0] as PNumber;
            var b = dat[1] as PNumber;
            var ret = Calculate(a.Value, b.Value);
            return new List<IData>() { ret };
        }
        protected abstract IData Calculate(double value1, double value2);
    }
}
