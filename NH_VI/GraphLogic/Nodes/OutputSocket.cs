using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;

namespace NH_VI.GraphLogic.Nodes
{
    public class OutputSocket : ISocket
    {
        public OutputSocket(Type t)
        {
            _socketType = t;
            Connectors = new List<Connector>();
        }
        private IData data;
        private Type _socketType;

        public IData Data { get => data; set => data = value; }

        public Type SocketType { get => _socketType; }

        public INode ParentNode => throw new NotImplementedException();

        public List<Connector> Connectors { get; set; }

        public event DataChanged OnDataChanged;
    }
}
