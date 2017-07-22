using CadTest3.GraphLogic;
using NH_VI.DataTypes.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.GraphLogic.Nodes
{
    public class ExternalSocket : InputSocket
    {
        public ExternalSocket(INode n) : base(typeof(ExternalData), n)
        {
           
        }
        List<ExternalData> _data = new List<ExternalData>();

        public void  ConnectToData(List<ExternalData> dat)
        {
            _data.Clear();
            foreach(var v in dat) {
                _data.Add(v);
                v.OnExternalDataChanged += UpdateFromExternalData;
            }
            UpdateFromExternalData();
        }

        private void UpdateFromExternalData()
        {
            UpdateFromExternalData(null);
        }
        private void UpdateFromExternalData(ExternalData dat)
        {
            
            if (_data.Count == 1) {
               
                Data = _data[0];
                UpdateData(Data);
            } else
            {
                var t = new DataTree();
                foreach(var d in _data)
                {
                    t.AddElement(d);
                }
                Data = t;
                UpdateData(Data);
            }
        }
    }
}
