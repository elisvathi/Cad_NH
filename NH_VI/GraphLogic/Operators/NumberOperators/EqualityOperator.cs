using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;
using NH_VI.DataTypes.Boolean;

namespace NH_VI.GraphLogic.Operators.NumberOperators
{
    public class EqualityOperator : AbstractNumberOperator
    {
        public override List<Type> OutputTypes => new List<Type>() { typeof(PBoolean) };
        protected override IData Calculate(double value1, double value2)
        {
            return new PBoolean(value1 == value2);
        }
    }
}
