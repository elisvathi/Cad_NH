using NH_VI.GraphLogic.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.GraphLogic
{
    public class NodesGraph
    {
        public delegate void GraphChanged();

        public delegate void NodeDelegate(INode n);
        public event NodeDelegate OnNodeAdded;
        public event NodeDelegate OnNodeRemoved;
        public delegate void ConnectorDelegate(Connector n);
        public event ConnectorDelegate OnConnectorAdded;
        public event ConnectorDelegate OnConnectorRemoved;
        public event GraphChanged OnGraphChanged;
        public List<INode> Nodes { get; set; } = new List<INode>();
        public void AddNode(INode n)
        {
            Nodes.Add(n);
            OnGraphChanged?.Invoke();
            OnNodeAdded?.Invoke(n);
            n.OnConnectorAdded += ConnectorAdded;
            n.OnConnectorRemoved += ConnectorRemoved;
            n.OnNodeRemoved += RemoveNode;
        }

        private void ConnectorRemoved(Connector n)
        {
            OnConnectorRemoved?.Invoke(n);
        }

        private void ConnectorAdded(Connector n)
        {
            OnConnectorAdded?.Invoke(n);
        }

        public void RemoveNode(INode n)
        {
            try
            {
                Nodes.Remove(n);
                OnGraphChanged?.Invoke();
                OnNodeRemoved?.Invoke(n);
                n.OnConnectorAdded -= ConnectorAdded;
                n.OnConnectorRemoved -= ConnectorRemoved;
                n.OnNodeRemoved -= RemoveNode;
            }
            catch
            {
                throw new Exception();
            }
        }
        public ISet<Connector> Connectors
        {
            get
            {
                var v = new HashSet<Connector>();
                foreach (var n in Nodes)
                {
                    foreach (var c in n.OutputSockets)
                    {
                        foreach (var con in c.Connectors)
                        {
                            v.Add(con);
                        }
                    }
                }
                return v;
            }
        }
    }
}
