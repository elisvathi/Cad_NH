using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;
using NH_VI.DataTypes.Boolean;

namespace NH_VI.GraphLogic.Operators.BooleanOperators
{
    public enum BoolOperationType
    {
        And, Or, Not, Xor, Nand, Non, XNor
    }
    public class BooleanOperator : AbstractOperator
    {
        public override List<Type> InputTypes => new List<Type>() {typeof(PBoolean), typeof(PBoolean) };
        public BooleanOperator(BoolOperationType t) {
            BType = t;
        }
        public BoolOperationType BType { get; set; } = BoolOperationType.And;
        public override bool TreeOperator => false;

        public override int MaxNumberOfSockets { get => BType==BoolOperationType.Not?1:2; set { } }
        public override int MinimumNumberOfSockets { get => BType == BoolOperationType.Not ? 1 : 2; set { } }
        public override int NumberOfOutputs { get => 1; set { } }

        public override List<Type> OutputTypes => new List<Type>() { typeof(PBoolean) };

        protected override List<IData> OperateSimple(List<IData> dat)
        {
            var retVal = new List<IData>();
            var d1 = dat[0] as PBoolean;
            var d2 = BType == BoolOperationType.Not ? null : dat[1] as PBoolean;
            switch (BType)
            {
                case BoolOperationType.And:
                    {
                        retVal.Add(BooleanOps.And(d1, d2));
                        break;
                    }
                case BoolOperationType.Nand:
                    {
                        retVal.Add(BooleanOps.Nand(d1, d2));
                        break;
                    }
                case BoolOperationType.Or:
                    {
                        retVal.Add(BooleanOps.Or(d1, d2));
                        break;
                    }
                case BoolOperationType.Not:
                    {
                        retVal.Add(BooleanOps.Not(d1));
                        break;
                    }
                case BoolOperationType.Xor:
                    {
                        retVal.Add(BooleanOps.Xor(d1, d2));
                        break;
                    }
                case BoolOperationType.XNor:
                    {
                        retVal.Add(BooleanOps.XNor(d1, d2));
                        break;
                    }
            }
            return retVal;
        }
    }
}
