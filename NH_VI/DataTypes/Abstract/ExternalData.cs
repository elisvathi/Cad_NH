using CadTest3.GraphLogic;
using NH_VI.GraphLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.DataTypes.Abstract
{
   public class ExternalData  : AbstractData
    {
        public delegate void ExternalDataChanged(ExternalData dat);
        public event ExternalDataChanged OnExternalDataChanged;
        public ExternalData(IExternal val) { Value = val;  Value.OnExternalChanged += ExternalChangeInvoker; }

        private void ExternalChangeInvoker(object val)
        {
            OnExternalDataChanged?.Invoke(this);
        }

        public override string ToString()
        {
            return "Exernal :" + value.ToString();
        }
        internal IExternal Value { get => value; set => this.value = value; }

        IExternal value;
        public override IData Copy()
        {
            return new ExternalData(value.Copy());
        }

     
    }
}
