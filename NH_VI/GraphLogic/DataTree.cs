using NH_VI.GraphLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Type DataType { get; private set; }
        public Type NodeType => IsEmpty ? null : Data[0].GetType();
        public DataTree()
        {
            Data = new List<IData>();
            DataType = null;
        }
        public bool IsEmpty => Count == 0;

        public int Count => Data.Count;
        public DataTreeType TensorConfigType
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
        public bool IsList => TensorConfigType == DataTreeType.List;
        public bool IsTree => TensorConfigType == DataTreeType.Tree;
        public bool IsSingle => TensorConfigType == DataTreeType.Single;
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
                        if (inTens.DataType == DataType)
                        {
                            (t as DataTree).AddTo(this);
                        }
                    }
                    else
                    {
                        if (inTens.DataType == DataType)
                        {
                            for (int i = 0; i < Data.Count; i++)
                            {
                                Data[i].Encapsulate();
                            }
                            inTens.AddTo(this);
                        }
                    }
                }
                else
                {
                    if (t.GetType() == DataType)
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
        public void RemoveElement(int index)
        {
            Data[index].RemoveFromParent();
        }
        public override IData Copy()
        {
            var retVal = new DataTree();
            foreach( var t in Data)
            {
                t.Copy().AddTo(retVal);
            }
            return retVal;
        }
    }
}
