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
        public List<INode> Nodes { get; set; } = new List<INode>();
        public void AddNode(INode n)
        {
            Nodes.Add(n);
        }
        public void RemoveNode(INode n)
        {
            try { Nodes.Remove(n); }
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
