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
       
        public delegate void EllipseDragActions();
        
        public event EllipseDragActions OnDragStarted;
        public event EllipseDragActions OnDragReceived;
        public event EllipseDragActions OnDragFinished;
        public DraggableEllipse()
        {
            InitializeComponent();
        }

        

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
           if(e.LeftButton == MouseButtonState.Pressed)
            {
                OnDragStarted?.Invoke();
            }
        }

        private void shape_MouseEnter(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed) {
            OnDragReceived?.Invoke();
                Entered = true;
            }
        }
        bool Entered = false;
        private void shape_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Entered)
            {
                OnDragFinished?.Invoke();
                Entered = false;
            }
        }
    }
}
