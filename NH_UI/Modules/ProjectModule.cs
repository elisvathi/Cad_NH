using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NH_UI.Controls;
using NH_VI.GraphLogic;
using NH_UI.Factory;

namespace NH_UI.Modules
{
    class ProjectModule : Ninject.Modules.NinjectModule
    {
        private ContextManager _manager;
        public ProjectModule(ContextManager mg) {
            _manager = mg;
        }
        public override void Load()
        {
            Bind<ContextManager>().ToConstant(_manager).InSingletonScope();
            Bind<ZoomBorder>().ToSelf().InSingletonScope();
            Bind<MainCanvas>().ToSelf().InSingletonScope();
            Bind<NodesGraph>().ToSelf().InSingletonScope();

            Bind<NodeBaseControl>().ToSelf().InTransientScope();
           
            Bind<NodeFactory>().ToSelf().InSingletonScope();
            Bind(NodeTypes).ToSelf().InTransientScope();
        }
        Type[] NodeTypes => this.Kernel.Get<NodeFactory>().GetAvailableTypes;
    }
}
