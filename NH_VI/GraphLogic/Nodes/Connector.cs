using CadTest3.GraphLogic;
using System.Collections.Generic;

namespace NH_VI.GraphLogic.Nodes
{
    public class Connector
    {
        private InputSocket ending;
        private OutputSocket starting;

        public InputSocket Ending { get => ending; }
        public OutputSocket Starting { get => starting; }

        public event DataChanged OnDataChanged;

        public Connector(OutputSocket starting, InputSocket ending, bool replaceEnding = true)
        {
            Connect(starting, ending, replaceEnding);
        }

        private void Connect(OutputSocket input, InputSocket output, bool replaceEnding = true)
        {
            ending = output;
            if (IsNotCircularReference(output.ParentNode))
            {
                starting = input;

                starting.Connectors.Add(this);
                starting.OnDataChanged += UpdateData;
                if (replaceEnding)
                {
                    //foreach(var c in ending.Connectors) { c.Disconnect(); }

                    for (int i = ending.Connectors.Count - 1; i >= 0; i--)
                    {
                        ending.Connectors[i].Disconnect();
                    }
                    ending.Connectors.Add(this);
                    this.OnDataChanged += ending.UpdateData;
                }
                starting.UpdateData(starting.Data);
                ending.ParentNode.InvokeConnectorAdded(this);
                starting.ParentNode.InvokeConnectorAdded(this);
            }
        }

        private void UpdateData(IData data)
        {
            OnDataChanged?.Invoke(data);
        }

        public bool IsNotCircularReference(INode s)
        {
            var val = true;
            foreach (var os in ending.ParentNode.OutputSockets)
            {
                foreach (var c in os.Connectors)
                {
                    val &= (c.ending.ParentNode != s);
                    if (val == false) return false;
                }
            }
            var lst = new List<Connector>();
            foreach (var v in ending.ParentNode.OutputSockets) { foreach (var c in v.Connectors) { lst.Add(c); } }
            foreach (var c in lst) { val &= c.IsNotCircularReference(s); }
            return true;
        }

        public void Disconnect()
        {
            starting.ParentNode.InvokeConnectorRemoved(this);
            ending.ParentNode.InvokeConnectorRemoved(this);
            starting.Connectors.Remove(this);
            ending.Connectors.Remove(this);

            OnDataChanged -= ending.UpdateData;
            starting.OnDataChanged -= UpdateData;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Connector)) { return false; }
            var a = obj as Connector;
            return ending == a.ending && starting == a.starting;
        }
    }
}