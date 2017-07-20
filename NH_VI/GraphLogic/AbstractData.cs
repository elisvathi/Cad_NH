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
        public DataTree Parent { get; private set; } = null;

        public int Index => Parent!=null? Parent.Data.IndexOf(this) : -1;

        public void AddTo(DataTree t)
        {
            t.Data.Add(this);
            Parent = t;
        }

        public void AddTo(DataTree t, int index)
        {
            t.Data.Insert(index, t);
            Parent = t;
        }

        public abstract IData Copy();

        public DataTree Encapsulate()
        {
            var p = Parent;
            int ind = -1;
            if (p != null) { ind =Index; RemoveFromParent(); }
            var t = new DataTree();
            AddTo(t);
            if (p != null) { t.AddTo(p, ind); }
            return t;
        }

        public IData RemoveFromParent()
        {
            if (Parent != null) { Parent.Data.Remove(this); Parent = null; }

            return this;
        }

    }
}
