using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.DataTypes.Abstract
{
    public class ExternalNumber : IExternal
    {
        public double value;
        public ExternalNumber(double num)
        {
            value = num;
        }

        public event ExternalDelegate OnExternalChanged;

        public IExternal Copy()
        {
            return new ExternalNumber(value);
        }

        public void RequestChange(object val)
        {
            value = Convert.ToDouble(val);
            OnExternalChanged?.Invoke(val);
        }

        public override string ToString()
        {
            return "Double " + value;
        }
    }
}
