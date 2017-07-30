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
    /// Interaction logic for DraggableEllipse.xaml
    /// </summary>
    public partial class DraggableEllipse : UserControl
    {
        public delegate void ReceivedDrop();
        public event ReceivedDrop OnDropReceived;
        public delegate void EllipseDragstarted();
        public event EllipseDragstarted OnDragStarted;
        public event EllipseDragstarted OnDragEnded;
        public DraggableEllipse()
        {
            InitializeComponent();
        }

        private void Ellipse_Drop(object sender, DragEventArgs e)
        {
            if(e.OriginalSource is DraggableEllipse) { 
            OnDropReceived?.Invoke();
            }
        }

        private void Ellipse_DragEnter(object sender, DragEventArgs e)
        {
            if(e.OriginalSource is DraggableEllipse) { 
            shape.Fill = Brushes.Red;
            }
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
           if(e.LeftButton == MouseButtonState.Pressed)
            {
                OnDragStarted?.Invoke();
            }
        }

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton== MouseButtonState.Released)
            {
                OnDragEnded?.Invoke();
                
            }
        }
    }
}
