﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH_VI.GraphLogic.Operators;

namespace NH_VI.GraphLogic.Nodes.NumberNode
{
    public class AddNode : AbstractNode
    {
        public AddNode() : base(new AddOperator())
        {
        }

        public override string Description { get; set; } = "Add";
    }
}
