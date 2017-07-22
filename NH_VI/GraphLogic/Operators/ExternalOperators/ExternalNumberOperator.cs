using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;
using NH_VI.DataTypes.Abstract;
using NH_VI.DataTypes.Numeric;

namespace NH_VI.GraphLogic.Operators.ExternalOperators
{
    public class ExternalNumberOperator : ExternalOperator
    {
       

        protected override IData ConvertExternal(IExternal v)
        {
            if (v is ExternalNumber)
            {
                return new PNumber((v as ExternalNumber).value);
            }
            else { throw new Exception(); }
            
        }
    }
}
