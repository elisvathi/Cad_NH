using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NH_UI.Modules;
using NH_VI.GraphLogic.Nodes;
using System.Windows.Controls;

namespace NH_UI.Controls.Nodes
{
    public abstract class CustomNodeView : NodeBaseControl
    {
        public CustomNodeView(ContextManager cm, ZoomBorder zb, INode bn) : base(cm, zb, bn)
        {
            base.NodeMainSpace.Children.Clear();
            
            NodeMainSpace.Children.Add(Elem);
        }

        public abstract UIElement Elem { get; }
    }
}
