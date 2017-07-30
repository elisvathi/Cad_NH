using NH_UI.Controls.Nodes;
using NH_UI.Modules;
using NH_VI.GraphLogic.Nodes;
using Ninject;
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
using static NH_UI.Controls.Nodes.InputSocketView;

namespace NH_UI.Controls
{
    /// <summary>
    /// Interaction logic for NodeBaseControl.xaml
    /// </summary>
    public partial class NodeBaseControl : UserControl
    {


        public INode BaseNode { get; set; }

        private void Refresh()
        {
            //System.Diagnostics.Debug.WriteLine("NODENAME: " + BaseNode.Description);
            NameText.Text = BaseNode.Description;
        }
        private ZoomBorder zoomable;

        private int NumInput => BaseNode.InputSockets.Count;
        private int NumOutpu => BaseNode.OutputSockets.Count;
        ContextManager manager;
        private MainCanvas baseCanvas => manager.ActiveKernel.Get<MainCanvas>();
        public NodeBaseControl(ContextManager cm, ZoomBorder zb, INode bn)
        {
            manager = cm;
            InitializeComponent();
            BaseNode = bn;
            NameText.DataContext = BaseNode;
            this.DataContext = this;
            Width = 300;
            Height = Math.Max(NumInput, NumOutpu) * 100;
            Canvas.SetTop(this, 1);
            Canvas.SetLeft(this, 1);
            zoomable = zb;
            AdjustSockets();
        }

        private void AdjustSockets()
        {
            int i = 1;
            float h = Math.Max(NumInput, NumOutpu) * 100;
            float hinpu = h / NumInput;
            float hout = h / NumOutpu;
            foreach (var inS in BaseNode.InputSockets)
            {
                var sv = new InputSocketView(inS, manager);
                Canvas.SetLeft(sv, -25);
                Canvas.SetTop(sv, hinpu * i - hinpu / 2 - sv.Height / 2);
                sv.OnSocketDragStarted += SocketDrag;
                SocketsCanvas.Children.Add(sv);
                i++;
            }
            i = 1;
            foreach (var outS in BaseNode.OutputSockets)
            {
                var sv = new OutputSocketView(outS, manager);
                Canvas.SetRight(sv, -25);
                Canvas.SetTop(sv, hout * i - hout / 2 - sv.Height / 2);
                sv.OnSocketDragStart += SocketDrag;
                SocketsCanvas.Children.Add(sv);
                i++;
            }
        }
        public event SocketDragStart OnSocketDragStart;
        private void SocketDrag(ISocketView sock)
        {
            OnSocketDragStart?.Invoke(sock);
        }

        bool drg = false;
        Point lastPos;
        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                drg = true;
                lastPos = e.GetPosition(baseCanvas);
                this.Cursor = Cursors.SizeAll;
            }
            else { drg = false; }

        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (drg)
            {

                Canvas.SetLeft(this, (Canvas.GetLeft(this) + e.GetPosition(baseCanvas).X - lastPos.X));
                Canvas.SetTop(this, (Canvas.GetTop(this) + e.GetPosition(baseCanvas).Y - lastPos.Y));
                foreach (var crv in Connectors)
                {
                    var cc = baseCanvas.GetCurve(crv);
                        cc?.UpdatePath();
                    
                }
                lastPos = e.GetPosition(baseCanvas);
            }
        }


        private List<Connector> Connectors
        {
            get
            {
                var retVal = new List<Connector>();
                foreach(var s in BaseNode.InputSockets)
                {
                    foreach (var c in s.Connectors)
                    {
                        retVal.Add(c);
                    }
                }
                foreach (var s in BaseNode.OutputSockets)
                {
                    foreach(var c in s.Connectors)
                    {
                        retVal.Add(c);
                    }
                }
                return retVal;
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                if (drg) { drg = false; this.Cursor = Cursors.Arrow; }
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            UserControl_MouseMove(sender, e);
        }

        private IEnumerable<OutputSocketView> OutputSocketControls
        {
            get
            {
                return SocketsCanvas.Children.OfType<OutputSocketView>();
            }
        }
        private IEnumerable<InputSocketView> InputSocketControls => SocketsCanvas.Children.OfType<InputSocketView>();

        public OutputSocketView GetOutputSocketControl(OutputSocket soc)
        {
            var a = from b in OutputSocketControls
                    where b.sock == soc
                    select b;
            return a.First();
        }
        public InputSocketView GetInputSocketControl(InputSocket soc)
        {
            var a = from b in InputSocketControls
                    where b.sock == soc
                    select b;
            return a.First();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButton.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave_1(object sender, MouseEventArgs e)
        {
            CloseButton.Visibility = Visibility.Hidden;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            BaseNode.Remove();
        }
    }
}
