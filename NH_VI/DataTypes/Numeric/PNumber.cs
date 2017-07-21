using CadTest3.GraphLogic;
using NH_VI.GraphLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.DataTypes.Numeric
{
    public class PNumber : AbstractData
    {
        public double Value;
        public PNumber():this(0) { }
        public PNumber(double d) { Value = d; }
        public override IData Copy()
        {
            return new PNumber(Value);
        }
        public override string ToString()
        {
            return "Number: " + Value;
        }
    }
}
