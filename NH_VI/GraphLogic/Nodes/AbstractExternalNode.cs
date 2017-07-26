using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH_VI.GraphLogic.Operators;
using NH_VI.DataTypes.Abstract;
using CadTest3.GraphLogic;

namespace NH_VI.GraphLogic.Nodes
{
    public class AbstractExternalNode : AbstractNode
    {
        public AbstractExternalNode(ExternalOperator op) : base(op)
        {
            Source = new ExternalSocket(this);
        }
        private ExternalSocket source;

        public ExternalSocket Source { get => source; set { source = value; source.OnDataChanged += this.Recalculate; } }

        public override string Description { get; set; } = "External";

        public void ConnectToData(List<ExternalData> dat)
        {
            Source.ConnectToData(dat);
        }
        protected override void Recalculate(IData data)
        {
            //base.Recalculate(data);
            var output = Operator.ProcessData(new List<IData>() { Source.Data });
            InvokeNodeChangedEvent(output);
        }
    }
}
