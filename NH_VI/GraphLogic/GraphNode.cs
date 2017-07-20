using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadTest3.GraphLogic
{
   public class GraphNode
    {
        List<GraphNode> InputNodes { get; set; }
        List<GraphNode> OutputNodes { get; set; }
        List<List<Type>> PossibleInputTypes { get; set; }
        List<List<Type>> FinalOutputTypes { get; set; }
    }
}
