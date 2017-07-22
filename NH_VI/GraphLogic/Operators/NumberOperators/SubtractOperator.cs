using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;
using NH_VI.DataTypes.Numeric;

namespace NH_VI.GraphLogic.Operators
{
    public class SubtractOperator : AbstractNumberOperator
    {
        protected override IData Calculate(double value1, double value2)
        {
            return new PNumber(value1- value2);
        }
    }
}
