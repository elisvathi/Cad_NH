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

        public DataGroupingModes GroupingMode { get; set; } = DataGroupingModes.CrossReference;

        public virtual List<IData> ProcessData(List<IData> dat)
        {
            if (CheckData(dat)) { return GroupData(dat); }
            else
            {
                throw new Exception();
            }
        }
        protected virtual List<IData> GroupData(List<IData> dat)
        {
            if (!TreeOperator)
            {
                if (dat.Count > 1)
                {

                    var b = false;
                    foreach (var d in dat) { if (d is DataTree) b = true; }
                    if (b)
                    {
                        var cop = new List<IData>(dat);
                        //foreach (var c in cop)
                        //{
                        //    if (!(c is DataTree)) { var t =  c.Encapsulate(); c = t; }
                        //}
                        for( int i = 0;i< cop.Count; i++)
                        {
                            if(!(cop[i]is DataTree)){ cop[i] = cop[i].Encapsulate(); }
                        }
                        DataTree.ConformTrees(cop.Cast<DataTree>().ToList(), out List<DataTree> conformedLists,GroupingMode, 0);
                        //DataTree.ConformTrees(cop[0] as DataTree, cop[1] as DataTree,GroupingMode, out DataTree f1, out DataTree f2, 0);
                        //var conformedLists = new List<DataTree>() { f1, f2 };
                        var skeleton = conformedLists[0].GetEmptyTree();
                       
                        List<IEnumerator<IData>> enums = new List<IEnumerator<IData>>();

                        foreach (var v in conformedLists)
                        {
                            enums.Add(v.GetChildIterator().GetEnumerator());
                        }

                        bool test = true;
                        var retVal = new List<IData>();
                        for(int  i = 0;i< NumberOfOutputs; i++) { retVal.Add(skeleton.Copy()); }
                        int counter = 0;
                        int numchilds = conformedLists[0].NumberChildEndings();
                        for (int j = 0; j < numchilds;j++)
                        {
                            counter++;
                            var calcList = new List<IData>();
                            List<int> path = new List<int>();
                            int firstTimeOnly = 0;
                            
                            foreach (var en in enums)
                            {

                                test = en.MoveNext();
                                var cur = en.Current;
                                if (firstTimeOnly == 0)
                                {
                                    path = cur.Path.ToList();
                                    firstTimeOnly++;
                                }
                                calcList.Add(en.Current);
                                

                            }
                           var temp =   OperateSimple(calcList);
                            
                            int i = 0;
                            foreach(var op in temp)
                            {
                                var newPath = new List<int>(path);
                                path.RemoveAt(path.Count - 1);
                                var d = (retVal[i] as DataTree).GetByPath(path.ToArray());
                                op.AddTo(d as DataTree);
                                i++;
                            }
                           
                        }

                        return retVal;
                    }
                    else
                    {
                        return OperateSimple(dat);
                    }
                    //DataTree.ConformTrees(dat.Cast(typeof(DataTree)), GroupingMode, out List<DataTree> tr, 0);
                }
                else
                {
                    return OperateSimple(dat);
                }
            }
            else { return OperateSimple(dat); }

        }

        protected abstract List<IData> OperateSimple(List<IData> dat);

        protected bool CheckData(List<IData> dat)
        {
            var test1 = dat.Count >= MinimumNumberOfSockets && dat.Count <= MaxNumberOfSockets;
            var test2 = true;
            for (int i = 0; i < dat.Count; i++)
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
        public abstract int NumberOfOutputs { get; set; }
    }
}
