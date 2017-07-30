using NH_UI.Controls;
using NH_UI.Modules;
using NH_VI.GraphLogic;
using NH_VI.GraphLogic.Nodes;
using NH_VI.GraphLogic.Nodes.NumberNode;
using System.Windows;
using Ninject;
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
            var test1 = false;
            var test2 = false;
            foreach(var n in Grp.Nodes)
            {
                if (n is AddNode) { test1 = true; }
                if(n is NumberInputNode) { test2 = true; }
            }
            if (test1 && test2)
            {
                var an = Grp.Nodes.Find(x => x is AddNode);
                var nn = Grp.Nodes.Find(x => x is NumberInputNode);
                var ann = an as AddNode;
                var nnn = nn as NumberInputNode;
                var ot = nnn.OutputSockets[0];
                var inp = ann.InputSockets[0];
                ot.ConnectTo(inp);
            }
            
        }
    }
}