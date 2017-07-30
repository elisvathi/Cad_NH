using NH_VI.GraphLogic.Nodes;
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

namespace NH_UI.Controls.Nodes
{
    /// <summary>
    /// Interaction logic for Socket.xaml
    /// </summary>
    public partial class InputSocketView : UserControl
    {
        public InputSocket sock;
        public InputSocketView(InputSocket s)
        {
            sock = s;
            InitializeComponent();
            Height = 50;
            Width = 100;
            TooltipDescription.DataContext = this;
            Handler.OnDragEnded += Handle_drag_finish;
            Handler.OnDragStarted += Hanle_drag_start;
            Handler.OnDropReceived += HandleDropReceived;
        }

        private void HandleDropReceived()
        {
            throw new NotImplementedException();
        }

        private void Hanle_drag_start()
        {
            throw new NotImplementedException();
        }

        private void Handle_drag_finish()
        {
            throw new NotImplementedException();
        }

        private string InputInfo => sock.Data.DataDescription;
    }
}
