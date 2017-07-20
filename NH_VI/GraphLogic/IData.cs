using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadTest3.GraphLogic
{
  public  interface IData
    {
        DataTree Parent { get;  }
        void AddTo(DataTree t);
        void AddTo(DataTree t, int index);
        IData RemoveFromParent();
        DataTree Encapsulate();
        IData Copy();
        int Index { get; }
    }
}
