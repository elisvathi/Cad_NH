using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadTest3.GraphLogic
{
  public  interface IData
    {
        DataTree Parent { get; set; }
        void AddTo(DataTree t);
        void AddTo(int index, DataTree t);
        IData RemoveFromParent();
        DataTree Encapsulate();
        IData Copy();
        int Index { get; }
        string DataDescription { get; }
        int TreeLevel { get; }
        void Replace(IData dat);
        int[] Path { get; }
    }
}
