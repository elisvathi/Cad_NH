using CadTest3.GraphLogic;
using NH_VI.GraphLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.DataTypes.Boolean
{
    public class PBoolean : AbstractData
    {
       public bool Value;
        public PBoolean(bool val)
        {
            Value = val;
        }
        public PBoolean() : this(false) { }
        public override IData Copy()
        {
            return new PBoolean(Value);
        }
        public override string ToString()
        {
            var p = Value ? "True" : "False";
            return "Boolean " +p;
        }
    }
}
