using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;

namespace NH_VI.GraphLogic.Nodes
{
    public class Connector
    {
        InputSocket ending;
        OutputSocket starting;

        public InputSocket Ending { get => ending; }
        public OutputSocket Starting { get => starting; }
        public event DataChanged OnDataChanged;

        public Connector(OutputSocket starting, InputSocket ending, bool replaceEnding = true)
        {
            Connect(starting, ending, replaceEnding);
        }

        void Connect(OutputSocket input, InputSocket output, bool replaceEnding = true)
        {
            starting = input;
            ending = output;
            starting.Connectors.Add(this);
            starting.OnDataChanged += UpdateData;
            if (replaceEnding)
            {
               //foreach(var c in ending.Connectors) { c.Disconnect(); }

               for (int i = ending.Connectors.Count-1; i >= 0; i--)
                {
                    ending.Connectors[i].Disconnect();
                }
                ending.Connectors.Add(this);
                this.OnDataChanged += ending.UpdateData;
            }
            ending.ParentNode.InvokeConnectorAdded(this);
            starting.ParentNode.InvokeConnectorAdded(this);
        }

        private void UpdateData(IData data)
        {
            OnDataChanged?.Invoke(data);
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
            if(!(obj is Connector)) { return false; }
            var a = obj as Connector;
            return ending == a.ending && starting == a.starting;
        }
    }
}
