using NH_VI.DataTypes.Boolean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.GraphLogic.Operators.BooleanOperators
{
  public static  class BooleanOps
    {
        public static PBoolean And(PBoolean b1, PBoolean b2)
        {
            return new PBoolean(b1.Value && b2.Value);
        }
        public static PBoolean Or(PBoolean b1, PBoolean b2)
        {
            return new PBoolean(b1.Value && b2.Value);
        }
        public static PBoolean Not(PBoolean b1)
        {
            return new PBoolean(!b1.Value);
        }
        public static PBoolean Nand(PBoolean b1, PBoolean b2)
        {
            return Not(And(b1, b2));
        }
        public static PBoolean Xor(PBoolean b1, PBoolean b2)
        {
            return And(Or(b1, b2), Nand(b1,b2));
        }
        public static PBoolean Nor(PBoolean b1, PBoolean b2)
        {
            return Not(Or(b1, b2));
        }
        public static PBoolean XNor(PBoolean b1, PBoolean b2)
        {
            return Not(Xor(b1, b2));
        }
    }
}
