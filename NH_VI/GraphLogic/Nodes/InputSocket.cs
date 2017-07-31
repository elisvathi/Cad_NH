using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;

namespace NH_VI.GraphLogic.Nodes
{
    public class InputSocket : ISocket
    {
        public InputSocket(Type t, INode n)
        {
            _socketType = t;
            Connectors = new List<Connector>();
            parentNode = n;
        }
        private IData data;
        private Type _socketType;

        public IData Data { get => data; set => data = value; }

        public Type SocketType { get => _socketType; }

        private INode parentNode;
        public INode ParentNode { get => parentNode; }

        public List<Connector> Connectors { get; set; }

        public event DataChanged OnDataChanged;

        public void UpdateData(IData data)
        {
            OnDataChanged?.Invoke(data);
        }
        public void ConnectTo(OutputSocket s, bool repl = true)
        {
            var v = new Connector(s, this,repl);
        }
        public void Disconnect(Connector c)
        {
            c.Disconnect();
        }
        public void Disconnect(int n)
        {
            if(n>0&& n < Connectors.Count) { Connectors[n].Disconnect(); }
        }
        public void Disconnect(OutputSocket s)
        {
            var a = Connectors.FindAll((c) => c.Starting == s);
            foreach( var c in a)
            {
                c.Disconnect();
            }
        }
    }
}
