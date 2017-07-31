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
using CadTest3.GraphLogic;

namespace NH_UI.Controls.Nodes
{
    /// <summary>
    /// Interaction logic for Socket.xaml
    /// </summary>
    public partial class InputSocketView : UserControl, ISocketView
    {
        public delegate void SocketDragStart(ISocketView sock);
        public event SocketDragStart OnSocketDragStarted;
        public InputSocket sock;
        private ContextManager manager;
        private MainCanvas BaseCanv => manager.ActiveKernel.Get<MainCanvas>();
        public InputSocketView(InputSocket s, ContextManager mg)
        {
            manager = mg;
            sock = s;
            InitializeComponent();
            Height = 50;
            Width = 100;
            TooltipDescription.DataContext = this;
            TooltipDescription.Text = InputInfo;
            sock.ParentNode.OnNodeDataChanged += UpdateInfo;
        }

        private void UpdateInfo(List<IData> dat)
        {
            TooltipDescription.Text = InputInfo;
        }

        private string InputInfo => sock.Data!=null?sock.Data.DataDescription:"Empty";

        public DraggableEllipse EllipseHandler => Handler;

        private void Handler_OnDragStarted()
        {
            OnSocketDragStarted?.Invoke(this);
        }

        private void Handler_OnDragReceived()
        {

        }

        private void Handler_OnDragFinished()
        {
            if (BaseCanv.IsTempActive && BaseCanv.tempSocket is OutputSocketView)
            {
                var sc = BaseCanv.tempSocket as OutputSocketView;
                if (sc.sock.ParentNode != sock.ParentNode)
                {
                    sock.ConnectTo(sc.sock,!BaseCanv.isShiftPressed);
                    BaseCanv.FinalazeTemp();
                }
            }
        }
    }
}
