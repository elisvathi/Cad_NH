using CadTest3.GraphLogic;
using NH_VI.GraphLogic.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NH_VI.GraphLogic.NodesGraph;

namespace NH_VI.GraphLogic.Nodes
{
    public delegate void NodeDataChanged(List<IData> dat);
    
   public interface INode
    {
        List<InputSocket> InputSockets { get; }
        List<OutputSocket> OutputSockets { get; }
        IOperator Operator { get; }
       event NodeDataChanged OnNodeDataChanged;
        string Description { get; set; }
        event ConnectorDelegate OnConnectorAdded;
        event ConnectorDelegate OnConnectorRemoved;
        void InvokeConnectorAdded(Connector con);
        void InvokeConnectorRemoved(Connector con);
        void Remove();
        event NodeDelegate OnNodeRemoved;
    }
}
