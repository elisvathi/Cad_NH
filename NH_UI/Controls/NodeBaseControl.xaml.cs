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

namespace NH_UI.Controls
{
    /// <summary>
    /// Interaction logic for NodeBaseControl.xaml
    /// </summary>
    public partial class NodeBaseControl : UserControl
    {
        public static readonly DependencyProperty NodeProp = DependencyProperty.Register(
            "BaseNode",
            typeof(INode),
            typeof(NodeBaseControl)
            );

        public INode BaseNode { get { return (INode)GetValue(NodeProp); } set { SetValue(NodeProp, value); Refresh(); } }

        private void Refresh()
        {
            //System.Diagnostics.Debug.WriteLine("NODENAME: " + BaseNode.Description);
            NameText.Text = BaseNode.Description;
        }

        public NodeBaseControl()
        {
            InitializeComponent();
            NameText.DataContext = BaseNode;
            this.DataContext = this;
            Width = 300;
            Height = 300;
        }

        bool drg = false;
        Point lastPos;
        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
          if(e.LeftButton == MouseButtonState.Pressed)
            {
                drg = true;
                lastPos = e.GetPosition(null);
                this.Cursor = Cursors.SizeAll;
            }
            else { drg = false; }
          
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (drg)
            {
                var lastXPos = Canvas.GetBottom(this);
                Canvas.SetLeft(this, Canvas.GetLeft(this) + e.GetPosition(null).X - lastPos.X);
                Canvas.SetTop(this, Canvas.GetTop(this) +  e.GetPosition(null).Y - lastPos.Y);
                lastPos = e.GetPosition(null);
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton== MouseButtonState.Released) { 
            if (drg) { drg = false; this.Cursor = Cursors.Arrow; }
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            UserControl_MouseMove(sender, e);
        }
    }
}
