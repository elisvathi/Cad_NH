using NH_UI.Modules;
using NH_VI.GraphLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Ninject;
using System.Windows.Media;

namespace NH_UI.Controls
{
    public class MainCanvas : Canvas
    {
        private NodesGraph _graph;
        private ContextManager _manager;
        public MainCanvas(NodesGraph graph, ContextManager manager)
        {
            _graph = graph;
            _graph.OnGraphChanged += RefreshNodeControls;
            _manager = manager;
            Background = Brushes.OrangeRed;
        }

        private void RefreshNodeControls()
        {
            Children.Clear();
            foreach (var n in _graph.Nodes)
            {

                var nc = _manager.ActiveKernel.Get<NodeBaseControl>();
                nc.BaseNode = n;
                
                this.Children.Add(nc);

            }
        }
    }
}
