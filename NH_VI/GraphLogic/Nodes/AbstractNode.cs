using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;
using NH_VI.GraphLogic.Operators;
using static NH_VI.GraphLogic.NodesGraph;

namespace NH_VI.GraphLogic.Nodes
{
    public abstract class AbstractNode : INode
    {
        private IOperator _operator;
        public AbstractNode(IOperator op)
        {
            _operator = op;
            InputSockets = new List<InputSocket>();
            OutputSockets = new List<OutputSocket>();
            UpdateSockets();
        }

        private void UpdateSockets()
        {
           foreach(var t in _operator.InputTypes)
            {
                AddInputSocket(new InputSocket(t, this));
            }
           foreach(var t in _operator.OutputTypes)
            {
                AddOutputSocket(new OutputSocket(t, this));
            }
        }

        public List<InputSocket> InputSockets { get; private set; }

        public List<OutputSocket> OutputSockets { get; private set; }

        public IOperator Operator { get => _operator; }
        public abstract string Description { get; set; }

        private void AddInputSocket(InputSocket s)
        {
            s.OnDataChanged += Recalculate;
            InputSockets.Add(s);
        }
        private void AddOutputSocket(OutputSocket s)
        {
            OutputSockets.Add(s);
        }

        protected virtual void Recalculate(IData data)
        {
            var list = new List<IData>();
            foreach(var i in InputSockets)
            {
                list.Add(i.Data);
            }
          var output =   Operator.ProcessData(list);
            //OnNodeDataChanged?.Invoke(output);
            InvokeNodeChangedEvent(output);
        }
        protected virtual void InvokeNodeChangedEvent(List<IData> dat) {
            OnNodeDataChanged?.Invoke(dat);
        }

        public void InvokeConnectorAdded(Connector con)
        {
            OnConnectorAdded?.Invoke(con);
        }

        public void InvokeConnectorRemoved(Connector con)
        {
            OnConnectorRemoved?.Invoke(con);
        }

        public void Remove()
        {
            //foreach( var c in Connectors)
            //{
            //    c.Disconnect();
            //}
            for(int i = Connectors.Count() - 1; i >= 0; i--)
            {
                Connectors.ElementAt(i).Disconnect();
            }
            OnNodeRemoved?.Invoke(this);
        }

        private IEnumerable<Connector> Connectors
        {
            get
            {
                foreach(var inp in InputSockets) {
                    foreach (var c in inp.Connectors)
                    {
                        yield return c;
                    }
                }
                foreach (var outp in OutputSockets)
                {
                    foreach(var c in outp.Connectors)
                    {
                        yield return c;
                    }
                }
            }
        }

        public event NodeDataChanged OnNodeDataChanged;
        public event NodesGraph.ConnectorDelegate OnConnectorAdded;
        public event NodesGraph.ConnectorDelegate OnConnectorRemoved;
        public event NodeDelegate OnNodeRemoved;
    }
}
