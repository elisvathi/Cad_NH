using NH_UI.Modules;
using NH_VI.GraphLogic;
using NH_VI.GraphLogic.Nodes;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NH_UI
{
    public class NodeAdder
    {

        public bool isButton;
        public Type nodeType;
        private ContextManager manager;
        public NodeAdder(Type n, ContextManager cm, bool visible = true)
        {
            manager = cm;
            isButton = visible;
            //if (n.GetInterfaces().Contains(typeof(INode)))
            //{
            //    nodeType = n;
            //}
            nodeType = n;
        }

        public INode GetNode()
        {
            return (INode)manager.ActiveKernel.GetService(nodeType);
        }

        public string GetDescription()
        {
            return GetNode().Description;
        }
        public void AddToGraph(NodesGraph g) {
            g.AddNode(GetNode());
        }
        private NodesGraph ActiveGraph => manager.ActiveKernel.Get<NodesGraph>();
        public void AddToGraph()
        {
            AddToGraph(ActiveGraph);
        }

        internal void Clicked(object sender, RoutedEventArgs e)
        {
            AddToGraph();
        }
    }
}
