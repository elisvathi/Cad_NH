using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH_VI.GraphLogic.Operators;

namespace NH_VI.GraphLogic.Nodes.NumberNode
{
    public class MultiplyNode : AbstractNode
    {
        public MultiplyNode() : base(new MultiplyOperator())
        {
        }

        public override string Description { get; set; } = "Multiply";
    }
}
