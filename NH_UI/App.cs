using NH_UI.Modules;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NH_UI
{
   public partial class App : Application
    {
        public IKernel kern;

        [STAThread]
        public static void Main() {
           var kern = new StandardKernel(new AppModule());
            
            var app = kern.Get<App>();
            //app.kern = kern;
            app.Run(kern.Get<MainWindow>());
        }
    }
}
