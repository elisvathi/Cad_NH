using NH_UI.Controls.Nodes;
using NH_UI.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ninject;
using CadTest3.GraphLogic;
using NH_VI.Geometry;

namespace NH_UI.Controls
{
    /// <summary>
    /// Interaction logic for TemporaryCurve.xaml
    /// </summary>
    public partial class TemporaryCurve : UserControl
    {
        private ContextManager mang;
        private ISocketView sv;
        public TemporaryCurve(ISocketView sc, ContextManager mg)
        {
            InitializeComponent();
            mang = mg;
            sv = sc;
            SecondPoint = StartPoint;
        }
        private IData Data
        {
            get
            {
                IData retVal = null;
                if(sv is InputSocketView)
                {
                    retVal = (sv as InputSocketView).sock.Data;
                }else if(sv is OutputSocketView)
                {
                    retVal = (sv as OutputSocketView).sock.Data;
                }
                return retVal;
            }
        }

        public Path GetPath
        {
            get
            {
                var p = new Path();
                p.Opacity = 0.7;
                p.Stroke = Stroke;
                p.StrokeThickness = IsSingle ? 3 : 7;
                p.StrokeDashArray = IsTree ? new DoubleCollection() { 3, 2 } : new DoubleCollection() { 1, 0 };
                var geometry = new PathGeometry();
                p.Data = geometry;
                var pt = new PathFigure();
                geometry.Figures.Add(pt);

                var crv = PCurve.GetNodeCurve(new PVector((sv is OutputSocketView)?StartPoint:SecondPoint), new PVector((sv is OutputSocketView) ? SecondPoint : StartPoint));
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

        private bool IsTree => (Data is DataTree && (sv is OutputSocketView)) ? (((Data as DataTree).IsTree) ? true : false) : false;
        private bool IsList => (Data is DataTree && (sv is OutputSocketView)) ? (((Data as DataTree).IsList) ? true : false) : false;
        private bool IsSingle => (Data is DataTree && (sv is OutputSocketView)) ? (((Data as DataTree).IsSingle) ? true : false) : true;


        private MainCanvas BaseCanv => mang.ActiveKernel.Get<MainCanvas>();
        public void FinalizeSuccess() {
            CrvCanv.Children.Clear();
        }
       
        private Point StartPoint => BaseCanv.PointFromScreen(sv.EllipseHandler.PointToScreen(new Point(sv.EllipseHandler.Width / 2, sv.EllipseHandler.Height / 2)));
        private Point SecondPoint;
        public void UpdatePoint(Point pt)
        {
            SecondPoint = pt;
            UpdateCurve();
        }

        private void UpdateCurve()
        {
            CrvCanv.Children.Clear();
            CrvCanv.Children.Add(GetPath);
        }
    }
}
