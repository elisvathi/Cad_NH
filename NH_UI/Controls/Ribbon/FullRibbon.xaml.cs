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
    /// Interaction logic for FullRibbon.xaml
    /// </summary>
    public partial class FullRibbon : UserControl
    {
        private NodeFactory factory;
        public FullRibbon(NodeFactory fact)
        {
            factory = fact;
            InitializeComponent();

            foreach (var cat in factory.Categories)
            {
                var item = new TabItem()
                {
                    Content = new CategoryView(cat),
                    Header = cat.CategoryName,
                   
                };
                tabContainer.Items.Add(item);
            }


        }
    }
}
