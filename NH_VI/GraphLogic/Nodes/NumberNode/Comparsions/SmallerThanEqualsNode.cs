﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH_VI.GraphLogic.Operators;
using NH_VI.GraphLogic.Operators.NumberOperators;

namespace NH_VI.GraphLogic.Nodes.NumberNode.Comparsions
{
    public class SmallerThanEqualsNode : AbstractNode
    {
        public SmallerThanEqualsNode() : base(new SmallerThanEquals())
        {
        }

        public override string Description { get; set; } = "<=";
    }
}
