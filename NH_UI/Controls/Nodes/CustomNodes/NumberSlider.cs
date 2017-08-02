using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NH_UI.Modules;
using NH_VI.GraphLogic.Nodes;
using System.Windows.Controls;
using NH_VI.GraphLogic.Nodes.NumberNode;
using NH_VI.DataTypes.Abstract;

namespace NH_UI.Controls.Nodes.CustomNodes
{
    public class NumberSlider : CustomNodeView
    {
        public NumberSlider(ContextManager cm, ZoomBorder zb, INode bn) : base(cm, zb, bn)
        {
            var a = bn as NumberInputNode;
            a.Source.ConnectToData(new List<ExternalData>() {Dat });
           
            e.Value = (Dat.Value as ExternalNumber).value;
            e.ValueChanged += ChangeVal;
        }

        private void ChangeVal(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Dat.Value.RequestChange(e.NewValue);
        }

       

        private ExternalData Dat = new ExternalData(new ExternalNumber(0));

        private Slider e = new Slider()
        {
            Minimum = -10,
            Maximum = 10
        };
        public override UIElement Elem => e;
    }
}
