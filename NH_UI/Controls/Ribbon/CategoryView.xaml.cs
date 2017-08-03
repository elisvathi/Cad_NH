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

namespace NH_UI.Controls.Ribbon
{
    /// <summary>
    /// Interaction logic for CategoryView.xaml
    /// </summary>
    public partial class CategoryView : UserControl
    {
        private ButtonCategory category;
        public CategoryView(ButtonCategory cat)
        {
            category = cat;
            InitializeComponent();
            BuildComponent();
          
            Background = Brushes.Purple;
        }
        public string CategoryName => category.CategoryName;
        private void BuildComponent()
        {
           for(int i =0;i< NumColumns; i++)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
               
            }
           for(int i = 0;i< NumColumns; i++)
            {
                var b = new ButtonsGrid(category.SubCategories[i]);
                Grid.SetColumn(b, i);
                MainGrid.Children.Add(b);
            }

        }

        public int NumColumns => category.SubCategories.Count;

    }
}
