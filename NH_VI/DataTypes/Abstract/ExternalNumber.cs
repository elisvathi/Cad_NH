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
        public IExternal Copy()
        {
            return new ExternalNumber(value);
        }
        public override string ToString()
        {
            return "Double " + value;
        }
    }
}
