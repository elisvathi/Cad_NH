using NH_UI.Factory;
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
    /// Interaction logic for ButtonsGrid.xaml
    /// </summary>
    /// 
    public partial class ButtonsGrid : UserControl
    {
        const int numRows = 2;

        const double bottomHeight = 10;

        public SubCategory CommandList;
        public ButtonsGrid(SubCategory buttonList)
        {
            CommandList = buttonList;
            
            InitializeComponent();
            Width = ((Height - bottomHeight) / numRows) * NumColumns;
            PlaceButtons();
        }

        private int NumColumns
        {
            get
            {
                int retVal = 0;
                foreach (var v in CommandList.Buttons)
                {
                    if (v.isButton) { retVal++; }
                }
                return (int)(retVal / 2) + retVal % 2;
            }
        }

        private void PlaceButtons()
        {
           
            for (int i = 0; i < NumColumns; i++)
            {
                ButtonsGridCont.ColumnDefinitions.Add(new ColumnDefinition());
            }
            var l = new Label();
            var vbb = new Viewbox();
            var tb = new TextBlock();
            tb.Text = CommandList.Name;
            vbb.Child = tb;
            l.Content = vbb;
            Grid.SetRow(l, 2);
            Grid.SetColumnSpan(l, NumColumns);
            ButtonsGridCont.Children.Add(l);
            int gr = 0;
            int gc = 0;
            foreach (var c in CommandList.Buttons)
            {
                var b = new Button();
               
                b.Click += c.Clicked;
                var vb = new Viewbox();
                var t = new TextBlock()
                {
                    Text = c.GetDescription()
                };
                vb.Child = t;
                b.Content = vb;
                Grid.SetRow(b, gr);
                Grid.SetColumn(b, gc);
                ButtonsGridCont.Children.Add(b);
               
                gc++;
               
                if (gc >= NumColumns) { gc = 0; gr ++; }
                if (gr > 1) { gr = 0; }
            }
        }
    }
}
