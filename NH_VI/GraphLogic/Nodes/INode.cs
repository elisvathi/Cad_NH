using CadTest3.GraphLogic;
using NH_VI.GraphLogic.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.GraphLogic.Nodes
{
    public delegate void NodeDataChanged(List<IData> dat);
   public interface INode
    {
        List<InputSocket> InputSockets { get; }
        List<OutputSocket> OutputSockets { get; }
        IOperator Operator { get; }
       event NodeDataChanged OnNodeDataChanged;
    }
}
