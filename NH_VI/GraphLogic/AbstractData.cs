using CadTest3.GraphLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.GraphLogic
{
    public abstract class AbstractData : IData
    {
        public DataTree Parent { get; set; } = null;

        public int Index => Parent != null ? Parent.Data.IndexOf(this) : -1;

        public virtual string DataDescription
        {
            get
            {
                var s = "";
                for (int i = 0; i < TreeLevel; i++)
                {
                    s += "     ";
                }
                s += Index == -1 ? "> " : Index + ". ";
                s += ToString();
                s += "\r\n";
                return s;
            }
        }

        public int TreeLevel => Parent == null ? 0 : Parent.TreeLevel + 1;

        public int[] Path
        {
            get
            {
                var retVal = new List<int>();
                if (Parent == null)
                {
                    return retVal.ToArray();
                }
                else
                {

                    retVal = Parent.Path.ToList();
                    retVal.Add(Index);
                }
                return retVal.ToArray();

            }
        }

        public void AddTo(DataTree t)
        {
            t.Data.Add(this);
            FixParentDataType(t);
            Parent = t;
        }

        public void AddTo(int index, DataTree t)
        {
            t.Data.Insert(index, this);
            FixParentDataType(t);
            Parent = t;

        }
        private void FixParentDataType(DataTree t)
        {
            if (t.DataType == null)
            {
                if(this is DataTree)
                {
                    t.DataType = (this as DataTree).DataType;
                }
                else
                {
                    t.DataType = this.GetType();
                }
            }

        }

        public abstract IData Copy();

        public DataTree Encapsulate()
        {
            var p = Parent;
            int ind = -1;
            if (p != null) { ind = Index; RemoveFromParent(); }
            var t = new DataTree();

            //AddTo(t);
            t.AddElement(this);
            if (p != null)
            {
                t.AddTo(ind, p);

            }
            return t;
        }
        public void Replace(IData d)
        {
            if (Parent != null)
            {
                var p = Parent;
                var ind = Index;
                RemoveFromParent();
                d.AddTo(ind, p);
            }
        }

        public IData RemoveFromParent()
        {
            if (Parent != null)
            {
                if (Parent.NumberChildEndings() == 1) { Parent.DataType = null; }
                Parent.Data.Remove(this);
                Parent = null;
            }

            return this;
        }

    }
}
