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


        void Connect(OutputSocket input, InputSocket output, bool replaceEnding = true)
        {
            starting = input;
            ending = output;
            starting.Connectors.Add(this);
            starting.OnDataChanged += UpdateData;
            if (replaceEnding)
            {
               foreach(var c in ending.Connectors) { c.Disconnect(); }
                ending.Connectors.Add(this);
                this.OnDataChanged += ending.UpdateData;
            }
        }

        private void UpdateData(IData data)
        {
            OnDataChanged?.Invoke(data);
        }
        public void Disconnect()
        {
            OnDataChanged -= ending.UpdateData;
            starting.OnDataChanged -= UpdateData;
        }
    }
}
