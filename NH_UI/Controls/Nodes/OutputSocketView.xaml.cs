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
    /// Interaction logic for OutputSocketView.xaml
    /// </summary>
    public partial class OutputSocketView : UserControl
    {
        public OutputSocket sock;
        public OutputSocketView(OutputSocket s)
        {
            sock = s;
            InitializeComponent();
            Height = 50;
            Width = 100;
            TooltipDescription.DataContext = this;
        }

        private string OutputInfo => sock.Data.DataDescription;

   
    }
}
