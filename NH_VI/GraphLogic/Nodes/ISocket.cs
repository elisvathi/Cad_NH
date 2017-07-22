using CadTest3.GraphLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.GraphLogic.Nodes
{
    public delegate void DataChanged(IData data);
    public interface ISocket
    {
        event DataChanged OnDataChanged;
        IData Data { get; set; }
        Type SocketType { get; }
        INode ParentNode { get; }
        List<Connector> Connectors { get; set; }
    }
}
