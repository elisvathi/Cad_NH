using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH_VI.GraphLogic.Operators;
using NH_VI.GraphLogic.Operators.NumberOperators.Series;

namespace NH_VI.GraphLogic.Nodes.NumberNode.NumberSeries
{
    public class CreateSeriesNode : AbstractNode
    {
        public CreateSeriesNode() : base(new CreateSeriesOperator())
        {
        }

        public override string Description { get; set; } = "Series";
    }
}
