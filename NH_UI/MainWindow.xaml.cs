using NH_UI.Controls;
using NH_VI.GraphLogic.Nodes;
using NH_VI.GraphLogic.Nodes.NumberNode;
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

namespace NH_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public INode FirstNode { get; set; } = new NumberInputNode();
        public INode SecondNode { get; set; } = new AddNode();

        private ZoomBorder _zoomb;
        [Inject]
        public ZoomBorder Zoomable { get=>_zoomb; set { _zoomb = value;  RefreshWindow(); } }

        private void RefreshWindow()
        {
           
            MainScrollView.Content = Zoomable;
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            first.BaseNode = FirstNode;
            second.BaseNode = SecondNode;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
           
        }

        private void Viewbox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //var vb = sender as Viewbox;
            //var sc = (ScaleTransform)vb.RenderTransform;
            //double zoom = e.Delta > 0 ? .2 : -.2;
            //sc.ScaleX += zoom;
            //sc.ScaleY += zoom;
            
        }
       
    }
}
