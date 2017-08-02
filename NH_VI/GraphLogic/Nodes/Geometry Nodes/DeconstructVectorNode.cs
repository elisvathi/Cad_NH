using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH_VI.GraphLogic.Operators;
using NH_VI.GraphLogic.Operators.GeometryOperators;

namespace NH_VI.GraphLogic.Nodes.Geometry_Nodes
{
    public class DeconstructVectorNode : AbstractNode
    {
        public DeconstructVectorNode() : base(new DeconstructVectorOperation())
        {
        }

        public override string Description { get; set; } = "DeconVect";
    }
}
