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
using System.Windows.Documents;
using NH_UI.Controls.Nodes;
using NH_VI.GraphLogic.Nodes;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows;
using NH_VI.Geometry;

namespace NH_UI.Controls
{
    public class MainCanvas : Canvas
    {
        private NodesGraph _graph;
        private ContextManager _manager;
        private Canvas NodesCanvas;
        private Canvas ConnectionsCanvas;
        public MainCanvas(NodesGraph graph, ContextManager manager)
        {
            _graph = graph;
            //_graph.OnGraphChanged += RefreshNodeControls;
            _graph.OnConnectorAdded += ConnectorAdedHandler;
            _graph.OnConnectorRemoved += ConnectorRemovedHandler;
            _graph.OnNodeAdded += NodedAddedHandler;
            _graph.OnNodeRemoved += NodeRemovedHandler;
            _manager = manager;
           

            var p = new Path();
            var geom = new PathGeometry();
            p.Data = geom;
            var pt = new PathFigure();
            geom.Figures.Add(pt);
            pt.StartPoint = new System.Windows.Point(0,100);
            pt.Segments.Add(new BezierSegment(new System.Windows.Point(100, 100), new System.Windows.Point(100, 200), new System.Windows.Point(200, 200), true));
            
            p.Stroke = Brushes.Black;
            p.StrokeDashArray = new DoubleCollection() { 3, 2 };
            p.StrokeThickness = 7;
            p.StrokeDashCap = PenLineCap.Round;
            p.Opacity = 0.8;
            var g = new Grid();
            Children.Add(g);
            NodesCanvas = new Canvas();
            ConnectionsCanvas = new Canvas();
          
            g.Children.Add(NodesCanvas);
            g.Children.Add(ConnectionsCanvas);
            ConnectionsCanvas.Children.Add(p);
            
        }

        private void NodeRemovedHandler(INode n)
        {
            if (HasNode(n))
            {
                NodesCanvas.Children.Remove(GetControl(n));

            }
        }

        private void NodedAddedHandler(INode n)
        {
            if (!HasNode(n))
            {
                NodesCanvas.Children.Add(new NodeBaseControl(_manager, _manager.ActiveKernel.Get<ZoomBorder>(), n));

            }
        }

        private void ConnectorRemovedHandler(Connector n)
        {
            if (HasConnector(n))
            {
                var crv = from a in Curves where a.con == n select a;
                //ConnectionsCanvas.Children.Remove(crv.First().path);
                Curves.Remove(crv.First());
                RefreshCurves();
            }
        }

        public void RefreshCurves()
        {
            ConnectionsCanvas.Children.Clear();
            foreach(var v in Curves)
            {
                ConnectionsCanvas.Children.Add(v.path);
            }
        }

        private void ConnectorAdedHandler(Connector n)
        {

            if (!HasConnector(n))
            {
                var con = new ConnectorCurve(_manager, n);
                Curves.Add(con);

                //ConnectionsCanvas.Children.Add(con.path);
                RefreshCurves();
            }
        }

        private void RefreshNodeControls()
        {

            int i = NodesCanvas.Children.Count - 1;
            while (i >= 0) {
                if (NodesCanvas.Children[i] is NodeBaseControl)
                {
                    if (!(_graph.Nodes.Contains((NodesCanvas.Children[i] as NodeBaseControl).BaseNode)))
                    {
                        NodesCanvas.Children.RemoveAt(i);
                    }
                    i--;
                }
            }
            foreach (var n in _graph.Nodes)
            {


                if (!HasNode(n))
                {
                    //var nc = _manager.ActiveKernel.Get<NodeBaseControl>();
                    var nc = new NodeBaseControl(_manager, _manager.ActiveKernel.Get<ZoomBorder>(), n);

                    var al = AdornerLayer.GetAdornerLayer(this);
                    al.Add(new NodeAdorner(nc));
                    NodesCanvas.Children.Add(nc);
                }
            }

            for (int ind = Curves.Count; ind>=0;ind--)
            {
                var con = Curves[i];
                if (!_graph.Connectors.Contains(con.con))
                {
                    ConnectionsCanvas.Children.Remove(con.path);
                    Curves.RemoveAt(i);
                }
            }
            foreach (var c in _graph.Connectors)
            {
                if (!HasConnector(c))
                {
                    Curves.Add(new ConnectorCurve(_manager, c));
                    //var crv = new ConnectorCurve(_manager, c);
                    ConnectionsCanvas.Children.Add(Curves.Last().path);
                }
            }
        }

        public List<ConnectorCurve> Curves = new List<ConnectorCurve>();
        private bool HasNode(INode n)
        {
            return AddedNodes.Contains(n);
        }
        private bool HasConnector(Connector c)
        {
            return Connectors.Contains(c);
        }
        private IEnumerable<Connector> Connectors
        {
            get
            {
                foreach (var c in Curves)
                {
                    yield return c.con;
                }
            }
        }
        public ConnectorCurve GetCurve(Connector c)
        {
            var s = from a in Curves where a.con == c select a;
            return s.Count() > 0 ? s.First() : null ;
        }
        public IEnumerable<ConnectorCurve> GetCurvesForInputSocket(InputSocket s) {
            foreach (var con in s.Connectors)
            {
                var crv = GetCurve(con);
                if(crv != null) { yield return crv; }
               
            }
        }
        public IEnumerable<ConnectorCurve> GetCurvesForOutputSocket(OutputSocket s)
        {
            foreach(var con in s.Connectors)
            {
                var crv = GetCurve(con);
                if (crv != null) { yield return crv; }
            }
        }
        public IEnumerable<ConnectorCurve> GetNodeCurves(INode n)
        {
            foreach (var inp in n.InputSockets)
            {
               foreach (var crv in GetCurvesForInputSocket(inp)) { yield return crv; }
            }
            foreach(var outp in n.OutputSockets)
            {
                foreach(var crv in GetCurvesForOutputSocket(outp)) { yield return crv; }
            }
        }

        private NodeBaseControl GetControl(INode n)
        {
            var a = from c in NodesCanvas.Children.OfType<NodeBaseControl>()
                    where c.BaseNode == n
                    select c;
            return a.Count()>0?a.First():null;
        }
        
        public OutputSocketView GetOutputSocketControl(OutputSocket soc)
        {
            var a=  GetControl(soc.ParentNode);
            return a?.GetOutputSocketControl(soc);
        }
        public InputSocketView GetInputSocketControl(InputSocket soc)
        {
            var a = GetControl(soc.ParentNode);
            return a?.GetInputSocketControl(soc) ;
        }

        private List<INode> AddedNodes
        {
            get
            {
                var retVal = new List<INode>();
                foreach (var c in NodesCanvas.Children)
                {
                    if (c is NodeBaseControl)
                    {
                        retVal.Add((c as NodeBaseControl).BaseNode);
                    }
                }
                return retVal;
            }
        }
    }
}
