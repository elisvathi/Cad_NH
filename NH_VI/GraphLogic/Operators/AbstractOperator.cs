using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadTest3.GraphLogic;

namespace NH_VI.GraphLogic.Operators
{
    public enum DataGroupingModes
    {
        Shortest,
        Longest,
        CrossReference,
        Primary
    }
    public abstract class AbstractOperator : IOperator
    {
        public abstract List<List<Type>> PossibleTypes { get; }

        public abstract bool TreeOperator { get; }

        public DataGroupingModes GroupingMode { get; set; } = DataGroupingModes.Shortest;

       public virtual List<IData> ProcessData(List<IData> dat)
        {
            if (CheckData(dat)){ return GroupData(dat); } else
            {
                throw new Exception();
            }
        }
        protected virtual List<IData> GroupData(List<IData> dat)
        {
            return null;
        }

        protected abstract void OperateSimple(List<IData> dat);

        protected  bool CheckData(List<IData> dat) {
            var test1 = dat.Count >= MinimumNumberOfSockets && dat.Count<=MaxNumberOfSockets;
            var test2 = true;
            for(int i= 0; i< dat.Count;i++)
            {
                var tempTest = PossibleTypes[i].Contains(dat[i].GetType());
                var tempTest2 = dat[i] is DataTree && PossibleTypes[i].Contains((dat[i] as DataTree).DataType);
                var tempTest3 = TreeOperator && dat[i] is DataTree;
                var finalTest = tempTest || tempTest2 || tempTest3;
                if (!finalTest) { test2 = false; }
            }
            return test1 && test2;
            
        }
       
        public abstract int MaxNumberOfSockets { get; set; }
        public abstract int MinimumNumberOfSockets { get; set; }
    }
}
