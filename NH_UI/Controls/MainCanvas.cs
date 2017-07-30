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
        private TemporaryCurve tempCurve;
        public ISocketView tempSocket;
        public bool IsTempActive;
        
        public MainCanvas(NodesGraph graph, ContextManager manager)
        {
            _graph = graph;
            //_graph.OnGraphChanged += RefreshNodeControls;
            _graph.OnConnectorAdded += ConnectorAdedHandler;
            _graph.OnConnectorRemoved += ConnectorRemovedHandler;
            _graph.OnNodeAdded += NodedAddedHandler;
            _graph.OnNodeRemoved += NodeRemovedHandler;
            _manager = manager;

            Background = Brushes.Wheat;
            
            var g = new Grid();
            Children.Add(g);
            NodesCanvas = new Canvas();
            ConnectionsCanvas = new Canvas();
            g.Children.Add(ConnectionsCanvas);
            g.Children.Add(NodesCanvas);
          MouseMove += MouseMoveHandler;
           MouseUp += MouseUpHandler;
            KeyDown += ShifHandler;
            KeyUp += ReleaseShiftHandler;
            
        }
        public bool isShiftPressed;
        private void ReleaseShiftHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                isShiftPressed = false;
            }
        }

        private void ShifHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                isShiftPressed = true;
            }
        }

        private void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            if( IsTempActive)
            {
                FinalazeTemp();
            }
        }

        internal void FinalazeTemp()
        {
            IsTempActive = false;
            tempCurve.FinalizeSuccess();
            ConnectionsCanvas.Children.Remove(tempCurve);
        }

        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && IsTempActive)
            {
                tempCurve.UpdatePoint(e.GetPosition(ConnectionsCanvas));
            }
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
                var nn = new NodeBaseControl(_manager, _manager.ActiveKernel.Get<ZoomBorder>(), n);
                NodesCanvas.Children.Add(nn);
                nn.OnSocketDragStart += SocketDragStarted;
            }
        }

        private void SocketDragStarted(ISocketView sock)
        {

            tempSocket = sock;
            IsTempActive = true;
            tempCurve = new TemporaryCurve(sock, _manager);
            ConnectionsCanvas.Children.Add(tempCurve);
        }

        private void ConnectorRemovedHandler(Connector n)
        {
            if (HasConnector(n))
            {
               
                ConnectionsCanvas.Children.Remove(GetCurve(n));
 
            }
        }

 

        private void ConnectorAdedHandler(Connector n)
        {

            if (!HasConnector(n))
            {
                var con = new ConnectorCurve(_manager, n);
               ConnectionsCanvas.Children.Add(con);

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
  
                    var nc = new NodeBaseControl(_manager, _manager.ActiveKernel.Get<ZoomBorder>(), n);

                    var al = AdornerLayer.GetAdornerLayer(this);
                    al.Add(new NodeAdorner(nc));
                    NodesCanvas.Children.Add(nc);
                }
            }

            for (int ind = ConnectionsCanvas.Children.Count-1; ind>=0;ind--)
            {
                if (ConnectionsCanvas.Children[i] is ConnectorCurve)
                {
                    var con = ConnectionsCanvas.Children[i] as ConnectorCurve;
                    if (!_graph.Connectors.Contains(con.con))
                    {
                      
                        ConnectionsCanvas.Children.Remove(con);
                    } 
                }
            }
            foreach (var c in _graph.Connectors)
            {
                if (!HasConnector(c))
                {
                   
                    ConnectionsCanvas.Children.Add(new ConnectorCurve(_manager, c));
                }
            }
        }

        public List<ConnectorCurve> Curves
        {
            get
            {
                var retVal = new List<ConnectorCurve>();
                foreach (var v in ConnectionsCanvas.Children)
                {
                    if (v is ConnectorCurve)
                    {
                        retVal.Add(v as ConnectorCurve);
                    }
                }
                return retVal;
            }
        }
        private bool HasNode(INode n)
        {
            return AddedNodes.Contains(n);
        }
        private bool HasConnector(Connector c)
        {
            return Connectors.Contains(c);
        }
        private List<Connector> Connectors
        {
            get
            {
                var retVal = new List<Connector>();
                foreach (var v in Curves)
                {
                   
                        retVal.Add(v.con);
                    
                }
                return retVal;
            }
        }
        public ConnectorCurve GetCurve(Connector c)
        {
            //var s = from a in ConnectionsCanvas.Children.OfType<ConnectorCurve>() where a.con.Equals(c) select a;
            //return s.First();
            var s = ConnectionsCanvas.Children;
            foreach ( var v in s)
            {
                if((v is ConnectorCurve))
                {
                    if((v as ConnectorCurve).con.Equals(c))
                    {
                        return v as ConnectorCurve;
                    }
                   
                }
            }
            return null;
        }
        public InputSocketView GetInputSocketControl(InputSocket sc)
        {
            var nd = GetControl(sc.ParentNode);
            return nd.GetInputSocketControl(sc);
        }
        public OutputSocketView GetOutputSocketControl(OutputSocket sc)
        {
            var nd = GetControl(sc.ParentNode);
            return nd.GetOutputSocketControl(sc);
        }


        private NodeBaseControl GetControl(INode n)
        {
            var a = from c in NodesCanvas.Children.OfType<NodeBaseControl>()
                    where c.BaseNode == n
                    select c;
            return a.Count()>0?a.First():null;
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
