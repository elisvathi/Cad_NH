using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH_VI.GraphLogic.Operators;
using NH_VI.GraphLogic.Operators.BooleanOperators;

namespace NH_VI.GraphLogic.Nodes.BooleanNodes
{
    public class AndNode : AbstractNode
    {
        public AndNode() : base(new BooleanOperator(BoolOperationType.And) )
        {
        }

        public override string Description { get; set; } = "And";
    }
    public class OrNode : AbstractNode
    {
        public OrNode() : base(new BooleanOperator(BoolOperationType.Or))
        {
        }

        public override string Description { get; set; } = "Or";
    }
    public class NotNode : AbstractNode
    {
        public NotNode() : base(new BooleanOperator(BoolOperationType.Not))
        {
        }

        public override string Description { get; set; } = "Not";
    }

    public class XorNode : AbstractNode
    {
        public XorNode() : base(new BooleanOperator(BoolOperationType.Xor))
        {
        }

        public override string Description { get; set; } = "Xor";
    }
    public class NandNode : AbstractNode
    {
        public NandNode() : base(new BooleanOperator(BoolOperationType.Nand))
        {
        }

        public override string Description { get; set; } = "Nand";
    }
    public class NorNode : AbstractNode
    {
        public NorNode() : base(new BooleanOperator(BoolOperationType.Nor))
        {
        }

        public override string Description { get; set; } = "Nor";
    }
    public class XNorNode : AbstractNode
    {
        public XNorNode() : base(new BooleanOperator(BoolOperationType.XNor))
        {
        }

        public override string Description { get; set; } = "XNor";
    }


}
