using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH_VI.GraphLogic.Operators;
using NH_VI.GraphLogic.Operators.ExternalOperators;
using NH_VI.DataTypes.Numeric;

namespace NH_VI.GraphLogic.Nodes.NumberNode
{
    public class NumberInputNode : AbstractExternalNode
    {
        public NumberInputNode() : base(new ExternalNumberOperator())
        {
            Source.Data = new PNumber(15);
        }
        public override string Description { get; set; } = "Number";
    }
}
