using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH_VI.GraphLogic.Operators;

namespace NH_VI.GraphLogic.Nodes
{
    public abstract class AbstractNode : INode
    {
        public List<ISocket> InputSockets { get; }

        public List<ISocket> OutputSockets { get; }

        public abstract IOperator Operator { get; }
    }
}
