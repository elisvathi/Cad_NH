using NH_UI.Controls;
using NH_UI.Modules;
using NH_VI.GraphLogic;
using NH_VI.GraphLogic.Nodes.Geometry_Nodes;
using NH_VI.GraphLogic.Nodes.NumberNode;
using NH_VI.GraphLogic.Nodes.NumberNode.NumberSeries;
using Ninject;
using System.Windows;
using System.Windows.Controls;

namespace NH_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ContextManager man;

        private ZoomBorder Zoomable => man.ActiveKernel.Get<ZoomBorder>();
        private MainCanvas Canvas => man.ActiveKernel.Get<MainCanvas>();
        public NodesGraph Grp => man.ActiveKernel.Get<NodesGraph>();

        private void RefreshWindow()
        {
            gContGrid.Children.Add(Zoomable);
            Grid.SetRow(Zoomable, 0);
        }

        public MainWindow(ContextManager mg)
        {
            man = mg;
            InitializeComponent();
            this.DataContext = this;
            RefreshWindow();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Grp.AddNode(new AddNode());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Grp.AddNode(new NumberInputNode());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Grp.AddNode(new ConstructVectorNode());
        }
    }
}