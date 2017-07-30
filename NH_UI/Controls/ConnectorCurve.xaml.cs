using CadTest3.GraphLogic;
using NH_UI.Controls.Nodes;
using NH_UI.Modules;
using NH_VI.Geometry;
using NH_VI.GraphLogic.Nodes;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NH_UI.Controls
{
    public partial class ConnectorCurve : UserControl
    {
        public Connector con;
        private ContextManager manager;
        private MainCanvas Canvas => manager.ActiveKernel.Get<MainCanvas>();
        public Path GetPath
        {
            get
            {
                var p = new Path();
                p.Opacity = 0.7;
                p.Stroke = Stroke;
                p.StrokeThickness = IsSingle ? 3 : 7;
                p.StrokeDashArray = IsTree ? new DoubleCollection() { 3, 2 } : new DoubleCollection() { 1,0 };
                var geometry = new PathGeometry();
                p.Data = geometry;
                var pt = new PathFigure();
                geometry.Figures.Add(pt);
                
                var crv = PCurve.GetNodeCurve(new PVector(OutputPoint), new PVector(InputPoint));
                pt.StartPoint = crv.pts[0].GetPoint();
                pt.Segments.Add(new BezierSegment(crv.pts[1].GetPoint(), crv.pts[2].GetPoint(), crv.pts[3].GetPoint(), true));
                return p;
            }
        }
        private Brush Stroke
        {
            get
            {
                var b = Brushes.Black;
                //b.Opacity = 0.8;
                return b;
            }
        }
        private INode OutputFromNode => con.Starting.ParentNode;
        private INode InputToNode => con.Ending.ParentNode;
        private InputSocket InputToSocket => con.Ending;
        private OutputSocket OutputFromSocket => con.Starting;

        private InputSocketView InputToControl => Canvas.GetInputSocketControl(InputToSocket);
        private OutputSocketView OutputToControl => Canvas.GetOutputSocketControl(OutputFromSocket);

        private DraggableEllipse InputEllipse => InputToControl.Handler;
        private DraggableEllipse OutputEllipse => OutputToControl.Handler;

        private Point InputPoint => Canvas.PointFromScreen(InputEllipse.PointToScreen(new Point(InputEllipse.Width/2, InputEllipse.Height/2)));
        private Point OutputPoint => Canvas.PointFromScreen(OutputEllipse.PointToScreen(new Point(OutputEllipse.Width/2, OutputEllipse.Height/2)));

        private bool IsTree => (OutputFromSocket.Data is DataTree) ? (((OutputFromSocket.Data as DataTree).IsTree) ? true : false) : false;
        private bool IsList => (OutputFromSocket.Data is DataTree) ? (((OutputFromSocket.Data as DataTree).IsList) ? true : false) : false;
        private bool IsSingle => (OutputFromSocket.Data is DataTree) ? (((OutputFromSocket.Data as DataTree).IsSingle) ? true : false) : true;
        public Path path;
        public ConnectorCurve (ContextManager cm, Connector c )
        {
            InitializeComponent();
            manager = cm;
            con = c;
            path = GetPath;
            CrvCanv.Children.Add(path);
        }
        public void UpdatePath()
        {
            CrvCanv.Children.Clear();
            path = GetPath;
            CrvCanv.Children.Add(path);
        }
       
    }
}
