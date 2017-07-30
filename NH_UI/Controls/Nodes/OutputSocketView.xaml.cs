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

namespace NH_UI.Controls.Nodes
{
    /// <summary>
    /// Interaction logic for OutputSocketView.xaml
    /// </summary>
    public partial class OutputSocketView : UserControl, ISocketView
    {
        public event SocketDragStart OnSocketDragStart;
        public OutputSocket sock;
        private ContextManager manager;
        private MainCanvas BaseCanv => manager.ActiveKernel.Get<MainCanvas>();
        public OutputSocketView(OutputSocket s , ContextManager mg)
        {
            manager = mg;
            sock = s;
            InitializeComponent();
            Height = 50;
            Width = 100;
            TooltipDescription.DataContext = this;
        }

        public DraggableEllipse EllipseHandler => Handler;

        private string OutputInfo => sock.Data.DataDescription;

        private void Handler_OnDragStarted()
        {
            OnSocketDragStart?.Invoke(this);
        }

        private void Handler_OnDragFinished()
        {
            if (BaseCanv.IsTempActive && BaseCanv.tempSocket is InputSocketView)
            {
                var sc = BaseCanv.tempSocket as InputSocketView;
                if (sc.sock.ParentNode != sock.ParentNode)
                {
                    sock.ConnectTo(sc.sock, !BaseCanv.isShiftPressed);
                    BaseCanv.FinalazeTemp();
                }
            }
        }
    }
}
