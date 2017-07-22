using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;
using NH_VI.DataTypes.Abstract;

namespace NH_VI.GraphLogic.Operators
{
    public abstract class ExternalOperator : AbstractOperator
    {
        public override List<Type> InputTypes => new List<Type>();

        public override List<Type> OutputTypes => new List<Type>() { typeof(IData) };

        public override bool TreeOperator => throw new NotImplementedException();

        public override int MaxNumberOfSockets { get => 0; set { } }
        public override int MinimumNumberOfSockets { get =>0;set { } }
        public override int NumberOfOutputs { get => 1;set { } }

        protected override bool CheckData(List<IData> dat)
        {
            bool test;
            if (dat !=null && dat.Count > 0) test = true;
            else test = false;
            return test;
        }
        protected override List<IData> OperateSimple(List<IData> dat)
        {
            var retVal = new List<IData>();
            foreach (var v in dat)
            {
                if (v is DataTree)
                {
                    var v1 = v.Copy();
                    foreach (var child in (v1 as DataTree).GetChildIterator())
                    {
                        child.Replace(ConvertExternal((child as ExternalData).Value));
                    }
                    retVal.Add(v1);
                }
                else if (v is ExternalData)
                {
                    retVal.Add(ConvertExternal((v as ExternalData).Value));
                }
            }
            return retVal;
        }

        protected abstract IData ConvertExternal(IExternal value);

        protected override List<IData> GroupData(List<IData> dat)
        {
            if (dat.Count == 1) { return OperateSimple(dat); } else
            {
                var t = new DataTree();
                foreach(var d in dat)
                {
                    t.AddElement(d);
                }
                return OperateSimple(new List<IData>() { t });
            }
        }

    }
}
