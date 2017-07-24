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

namespace NH_UI
{
    /// <summary>
    /// Interaction logic for NodeView.xaml
    /// </summary>
    public partial class NodeView : UserControl
    {
        public NodeView()
        {
            InitializeComponent();
        }

        private void CircleName_GotFocus(object sender, RoutedEventArgs e)
        {
            CircleName.Fill = Brushes.Red;
        }

        private void CircleName_LostFocus(object sender, RoutedEventArgs e)
        {
            CircleName.Fill = Brushes.Purple;
        }
    }
}
