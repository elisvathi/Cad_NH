using NH_UI.Controls;
using NH_UI.Modules;
using NH_VI.GraphLogic;
using NH_VI.GraphLogic.Nodes;
using NH_VI.GraphLogic.Nodes.NumberNode;
using System.Windows;

namespace NH_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ContextManager man;
        public MainWindow(ContextManager mg) { man = mg; }
        public INode FirstNode { get; set; } = new NumberInputNode();
        public INode SecondNode { get; set; } = new AddNode();

d        public ZoomBorder Zoomable { get }

        public NodesGraph Grp { get; }

        private void RefreshWindow()
        {
            gCont.Children.Add(Zoomable);
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Grp.AddNode(new AddNode());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Grp.AddNode(new NumberInputNode());
        }
    }
}