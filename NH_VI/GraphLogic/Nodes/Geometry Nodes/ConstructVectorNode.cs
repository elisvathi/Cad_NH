using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH_VI.GraphLogic.Operators;

namespace NH_VI.GraphLogic.Nodes.Geometry_Nodes
{
    public class ConstructVectorNode : AbstractNode
    {
        public ConstructVectorNode() : base(new BuildVectorOperator())
        {
        }

        public override string Description { get; set; } = "VectorXYZ";
    }
}
