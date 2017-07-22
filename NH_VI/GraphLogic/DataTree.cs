using NH_VI.GraphLogic;
using NH_VI.GraphLogic.Operators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CadTest3.GraphLogic
{
    public enum DataTreeType
    {
        Single,
        List,
        Tree,
        Empty
    }

    public class DataTree : AbstractData
    {
        public List<IData> Data { get; }
        public Type DataType { get; set; }
        public Type NodeType => IsEmpty ? null : Data[0].GetType();

        public DataTree()
        {
            Data = new List<IData>();
            DataType = null;
        }

        public bool IsEmpty => Count == 0;

        public int Count => Data.Count;

        public DataTreeType TreeConfigType
        {
            get
            {
                if (NodeType == typeof(DataTree))
                {
                    return DataTreeType.Tree;
                }
                else
                {
                    if (Count == 1)
                    {
                        return DataTreeType.Single;
                    }
                    else if (Count > 1)
                    {
                        return DataTreeType.List;
                    }
                    else
                    {
                        return DataTreeType.Empty;
                    }
                }
            }
        }

        public bool IsList => TreeConfigType == DataTreeType.List;
        public bool IsTree => TreeConfigType == DataTreeType.Tree;
        public bool IsSingle => TreeConfigType == DataTreeType.Single;

        public IData this[int key]
        {
            get { return Data[key]; }
            set { Data[key] = value; }
        }

        public void AddElement(IData t)
        {
            if (IsEmpty)
            {
                if (t is DataTree) { DataType = (t as DataTree).DataType; } else { DataType = t.GetType(); }
                t.AddTo(this);
            }
            else
            {
                if (t is DataTree)
                {
                    var inTens = t as DataTree;
                    if (NodeType == typeof(DataTree))
                    {
                        if (inTens.DataType == DataType || DataType == null)
                        {
                            (t as DataTree).AddTo(this);
                        }
                    }
                    else
                    {
                        if (inTens.DataType == DataType || DataType == null)
                        {
                            GraftTree();
                            inTens.AddTo(this);
                        }
                    }
                }
                else
                {
                    var a = t.GetType();
                    var b = DataType;
                    if (t.GetType() == DataType || DataType == null)
                    {
                        if (NodeType == typeof(DataTree))
                        {
                            var temp = t.Encapsulate();
                            temp.AddTo(this);
                        }
                        else
                        {
                            t.AddTo(this);
                        }
                    }
                }
            }
        }

        public void RemoveElement(IData d)
        {
            d.RemoveFromParent();
        }

        private void ReplaceBranch(IData newBranch, int i)
        {
            Data[i].Replace(newBranch);
        }

        public void RemoveElement(int index)
        {
            Data[index].RemoveFromParent();
        }

        public override IData Copy()
        {
            var retVal = new DataTree();
            foreach (var t in Data)
            {
                if (t is DataTree) { (t as DataTree).Copy().AddTo(retVal); }
                else
                {
                    t.Copy().AddTo(retVal);
                }
            }
            retVal.Parent = Parent;
            retVal.DataType = DataType;
            return retVal;
        }

        public override string ToString()
        {
            if (IsTree)
            {
                return "Tree of " + DataType.Name + " with " + Count + " branches and depth of " + TreeDepth();
            }
            else if (IsList)
            {
                return "List of " + DataType.Name + " with " + Count + " elements.";
            }
            else if (IsSingle)
            {
                return "List of " + Data[0].GetType().Name + " with  1 element";
            }
            else
            {
                return "Empty";
            }
        }

        public override string DataDescription
        {
            get
            {
                var s = base.DataDescription;
                foreach (var d in Data)
                {
                    s += d.DataDescription;
                }
                return s;
            }
        }

        public IData GetByPath(int[] path)
        {
            if (path.Length == 0) { return this; }
            if (ContainsPath(path))
            {
                if (path.Length > 1)
                {
                    return (Data[path[0]] as DataTree).GetByPath(GetChildPath(path));
                }
                else
                {
                    return Data[path[0]];
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public static void ConformTrees(DataTree t1, DataTree t2, DataGroupingModes mode, out DataTree firstTree, out DataTree secondTree, int primary = 0)
        {
            int ind = -1;

            if (mode == DataGroupingModes.Shortest)
            {
                ind = t1.Count <= t2.Count ? 0 : 1;
            }
            else if (mode == DataGroupingModes.Longest)
            {
                ind = t1.Count >= t2.Count ? 0 : 1;
            }
            else if (mode == DataGroupingModes.Primary)
            {
                firstTree = primary == 0 ? t1 : t1.ConformTree(t2);
                secondTree = primary == 0 ? t2.ConformTree(t1) : t2;
                return;
            }

            if (ind != -1)
            {
                if (ind == 0) { firstTree = t1; secondTree = t2.ConformList(t1); } else { firstTree = t1.ConformList(t2); secondTree = t2; }
                bool test = ind == 0 ? firstTree.IsTree : secondTree.IsTree;
                if (test)
                {
                    if (ind == 0 && !secondTree.IsTree) { secondTree.GraftTree(); } else if (ind == 1 && !firstTree.IsTree) { firstTree.GraftTree(); }
                    for (int i = 0; i < firstTree.Count; i++)
                    {
                        ConformTrees(firstTree[i] as DataTree, secondTree[i] as DataTree, mode, out DataTree f1, out DataTree f2, primary);
                        firstTree.Data[i].Replace(f1);
                        secondTree.Data[i].Replace(f2);
                    }
                }
            }
            else
            {
                CrossRefTree(t1, t2, out firstTree, out secondTree);
            }
        }

        public static void ConformTrees(List<DataTree> trees, out List<DataTree> output, DataGroupingModes mode, int primaryIndex = 0)
        {
            output = new List<DataTree>();
            if (mode == DataGroupingModes.Primary)
            {
                output.Add(trees[primaryIndex]);
                for (int i = 0; i < trees.Count; i++)
                {
                    if (i != primaryIndex)
                    {
                        ConformTrees(trees[primaryIndex], trees[i], mode, out DataTree f1, out DataTree f2, 0);
                        //output.Add(f1);
                        output.Add(f2);
                    }
                }
            }
            else if (mode == DataGroupingModes.CrossReference)
            {
                CrossRefTree(trees, out output);

            }
            else
            {
                ConformListOfTreesLongestShortest(trees, out output, mode);
            }
        }

        public int NumberChildEndings()
        {
            if (!IsTree) { return Count; }
            else
            {
                int sum = 0;
                foreach (var d in Data)
                {
                    if (d is DataTree)
                    {
                        sum += (d as DataTree).NumberChildEndings();
                    }
                }
                return sum;
            }
        }

        public IEnumerable<IData> GetChildIterator()
        {
            if (!IsTree)
            {
                foreach (var d in Data)
                {
                    yield return d;
                }
            }
            else
            {
                foreach (var d in Data)
                {
                    if (d is DataTree)
                    {
                        foreach (var t in (d as DataTree).GetChildIterator())
                        {
                            yield return t;
                        }
                    }
                }
            }
        }
        public IEnumerable<DataTree> GetBranchesWithData()
        {
            if (!IsTree)
            {
                yield return this;
            }
            else
            {
                foreach (var d in Data)
                {
                    if (d is DataTree)
                    {
                        foreach (var t in (d as DataTree).GetBranchesWithData())
                        {
                            yield return t;
                        }
                    }
                }
            }
        }

        public DataTree GetEmptyTree()
        {
            var retVal = Copy() as DataTree;
            foreach (var v in retVal.GetBranchesWithData())
            {
                v.EmptyBranch();
            }
            return retVal;
        }
        public void EmptyBranch()
        {
            var c = Count;
            for (int i = 0; i < c; i++)
            {
                Data.Last().RemoveFromParent();
            }
        }

        private void GraftTree()
        {
            for (int i = 0; i < Data.Count; i++)
            {
                Data[i].Encapsulate();
            }
        }

        private DataTree ConformList(DataTree t)
        {
            return RequestNewSizeRepeatLast(t.Count);
        }

        private DataTree ConformTree(DataTree t)
        {
            var retVal = ConformList(t);
            if (t.IsTree)
            {
                retVal.GraftTree();
                for (int i = 0; i < retVal.Count; i++)
                {
                    retVal[i].Replace((retVal[i] as DataTree).ConformTree(t.Data[i] as DataTree));
                }
            }
            return retVal;
        }
        private static void ConformListOfTreesLongestShortest(List<DataTree> input, out List<DataTree> output, DataGroupingModes mode)
        {
            output = new List<DataTree>();
            int maxCount = -1;
            int maxIndex = -1;
            int minCount = int.MaxValue;
            int minIndex = -1;
            for (int i = 0; i < input.Count; i++) { if (input[i].Count > maxCount) { maxCount = input[i].Count; maxIndex = i; }; if (input[i].Count < minCount) { minCount = input[i].Count; minIndex = i; } }
            bool test = false;
            foreach (var l in input)
            {
                output.Add(l.RequestNewSizeRepeatLast(mode == DataGroupingModes.Longest ? maxCount : minCount));
                test |= l.IsTree;
            }
            if (test)
            {
                foreach (var l in output) { if (!(l.IsTree)) { l.GraftTree(); } }


                for (int i = 0; i < output[0].Count; i++)
                {
                    var newInput = new List<DataTree>();
                    foreach(var l in output)
                    {
                        newInput.Add(l.Data[i] as DataTree);
                    }
                    ConformListOfTreesLongestShortest(newInput, out List<DataTree> newOutput, mode);
                    for(int j = 0;j< newOutput.Count; j++)
                    {
                        output[j].ReplaceBranch(newOutput[j], i);
                    }
                }

            }


        }
        private static void CrossRefTree(List<DataTree> input, out List<DataTree> output)
        {
            output = new List<DataTree>();
            int counts = 1;
            foreach (var l in input) { counts *= l.Count > 0 ? l.Count : 1; }
            foreach (var l in input)
            {
                int repCount = 1;
                for (int i = input.IndexOf(l) + 1; i < input.Count; i++) { repCount *= input[i].Count; }
                output.Add(l.RequestCrossRefRepetition(counts / (l.Count > 0 ? l.Count : 1), repCount));
            }
            var test = false;
            foreach (var l in output) { if (l.IsTree) { test = true; } }
            if (test)
            {
                foreach (var e in output) { if (!(e.IsTree)) { e.GraftTree(); } }
                for (int i = 0; i < output[0].Count; i++)
                {
                    var newInput = new List<DataTree>();
                    foreach (var l in output) { newInput.Add(l.Data[i] as DataTree); }
                    CrossRefTree(newInput, out List<DataTree> newOutput);
                    for (int j = 0; j < output.Count; j++) { output[j].ReplaceBranch(newOutput[j], i); }
                }
            }
        }

        private static void CrossRefTree(DataTree t1, DataTree t2, out DataTree first, out DataTree second)
        {
            first = t1.RequestCrossRefRepetition(t2.Count, t2.Count);
            second = t2.RequestCrossRefRepetition(t1.Count, 1);

            if (first.IsTree || second.IsTree)
            {
                if (!first.IsTree) { first.GraftTree(); }
                if (!second.IsTree) { second.GraftTree(); }
                for (int i = 0; i < first.Count; i++)
                {
                    //int val = (first.Data[i] as DataTree).Count * (second.Data[i] as DataTree).Count;
                    //int c1 = (first.Data[i])
                    //first.Data[i].Replace((first.Data[i] as DataTree).RequestCrossRefRepetition(val, true));
                    //second.Data[i].Replace((second.Data[i] as DataTree).RequestCrossRefRepetition(val, false));
                    CrossRefTree(first.Data[i] as DataTree, second.Data[i] as DataTree, out DataTree f1, out DataTree f2);
                    first.ReplaceBranch(f1, i);
                    second.ReplaceBranch(f2, i);
                }
            }
        }

        private DataTree RequestCrossRefRepetition(int mult, int reps)
        {
            DataTree retVal = new DataTree();


            for (int i = 0; i < mult / reps; i++)
            {
                for (int j = 0; j < Data.Count; j++)
                {
                    for (int k = 0; k < reps; k++)
                    {
                        retVal.AddElement(Data[j].Copy());
                    }
                }
            }

            return retVal;
        }

        private DataTree RequestNewSizeRepeatLast(int size)
        {
            var retVal = Copy() as DataTree;
            if (size > Data.Count)
            {
                for (int i = Data.Count; i < size; i++)
                {
                    var newDat = retVal.Data.Last().Copy();
                    retVal.AddElement(newDat);
                }
            }
            else if (size < Data.Count)
            {
                for (int i = size; i < Count; i++)
                {
                    retVal.RemoveElement(retVal.Data.Last());
                }
            }
            return retVal;
        }

        private int TreeDepth()
        {
            if (!IsTree) { return 1; }
            else
            {
                List<int> depths = new List<int>();
                for (int i = 0; i < Count; i++)
                {
                    depths.Add(1 + (Data[i] as DataTree).TreeDepth());
                }
                return MaxInt(depths);
            }
        }

        private int MaxInt(List<int> depths)
        {
            int max = depths[0];
            for (int i = 1; i < depths.Count; i++)
            {
                if (depths[i] > max) { max = depths[i]; }
            }
            return max;
        }

        private bool ContainsPath(int[] path)
        {
            if (path.Length == 0) { return true; }
            var test = path[0] >= 0 && path[0] < Data.Count;
            if (!test) { return false; }
            var test2 = true;
            if (Data[path[0]] is DataTree && path.Length > 1)
            {
                var newPath = GetChildPath(path);
                test2 = (Data[path[0]] as DataTree).ContainsPath(newPath);
            }
            return test && test2;
        }

        private int[] GetChildPath(int[] path)
        {
            var l = new List<int>(path);
            l.RemoveAt(0);
            return l.ToArray();
        }
    }
}