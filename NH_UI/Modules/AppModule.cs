using NH_UI.Factory;
using Ninject;
using Ninject.Modules;

namespace NH_UI.Modules
{
    public class AppModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IKernel>().ToConstant(KernelInstance).InSingletonScope().Named("AppKernel");
            Bind<ContextManager>().ToSelf().InSingletonScope();
            Bind<MainWindow>().ToSelf().InSingletonScope();
            Bind<App>().ToSelf().InSingletonScope();
            
        }
    }
}