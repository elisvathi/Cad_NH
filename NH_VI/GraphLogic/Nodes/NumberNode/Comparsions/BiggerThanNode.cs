using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH_VI.GraphLogic.Operators;
using NH_VI.GraphLogic.Operators.BooleanOperators;
using NH_VI.GraphLogic.Operators.NumberOperators;

namespace NH_VI.GraphLogic.Nodes.NumberNode.Comparsions
{
    public class BiggerThanNode : AbstractNode
    {
        public BiggerThanNode() : base(new GreaterThanOperator())
        {
        }

        public override string Description { get; set; } = ">";
    }
}
