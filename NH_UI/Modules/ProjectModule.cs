using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NH_UI.Controls;
using NH_VI.GraphLogic;

namespace NH_UI.Modules
{
    class ProjectModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<ZoomBorder>().ToSelf().InSingletonScope();
            Bind<MainCanvas>().ToSelf().InSingletonScope();
            Bind<NodesGraph>().ToSelf().InSingletonScope();
        }
    }
}
