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
        private INode _parentNode;
        public OutputSocket(Type t, INode n)
        {
            _socketType = t;
            _parentNode = n;
            Connectors = new List<Connector>();
            _parentNode.OnNodeDataChanged += NodeChanged;
        }

        private void NodeChanged(List<IData> dat)
        {
            Data = dat[Index];
            OnDataChanged?.Invoke(dat[Index]);
        }
        public int Index => _parentNode.OutputSockets.IndexOf(this);
        private IData data;
        private Type _socketType;

        public IData Data { get => data; set => data = value; }

        public Type SocketType { get => _socketType; }

        public INode ParentNode => _parentNode;

        public List<Connector> Connectors { get; set; }

        public event DataChanged OnDataChanged;
        public void Disconnect(Connector c)
        {
            c.Disconnect();
        }
        public void Disconnect(int n)
        {
            if (n > 0 && n < Connectors.Count) { Connectors[n].Disconnect(); }
        }
        public void Disconnect(InputSocket s)
        {
            var a = Connectors.FindAll((c) => c.Ending == s);
            foreach (var c in a)
            {
                c.Disconnect();
            }
        }
        public void ConnectTo(InputSocket s)
        {
            var v = new Connector(this, s);
        }
    }
}
