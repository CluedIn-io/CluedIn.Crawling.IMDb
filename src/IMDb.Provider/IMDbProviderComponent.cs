using System.Reflection;
using Castle.MicroKernel.Registration;
using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Core.Server;
using CluedIn.Crawling.IMDb.Core;
using CluedIn.Crawling.IMDb.Infrastructure.Installers;
using ComponentHost; // 
// 

namespace CluedIn.Crawling.Provider.IMDb
{
    [Component(IMDbConstants.ProviderName, "Providers", ComponentType.Service, ServerComponents.ProviderWebApi,
        Components.Server, Components.DataStores, Isolation = ComponentIsolation.NotIsolated)]
    public sealed class IMDbProviderComponent : ServiceApplicationComponent<IBusServer>
    {
        public IMDbProviderComponent(ComponentInfo componentInfo)
            : base(componentInfo)
        {
            // Dev. Note: Potential for compiler warning here ... CA2214: Do not call overridable methods in constructors
            //   this class has been sealed to prevent the CA2214 waring being raised by the compiler
            Container.Register(Component.For<IMDbProviderComponent>().Instance(this));
        }

        public override void Start()
        {
            Container.Install(new InstallComponents());
            var asm = Assembly.GetExecutingAssembly();
            Container.Register(Types.FromAssembly(asm).BasedOn<IProvider>().WithServiceFromInterface()
                .If(t => !t.IsAbstract).LifestyleSingleton());
            Container.Register(Types.FromAssembly(asm).BasedOn<IEntityActionBuilder>().WithServiceFromInterface()
                .If(t => !t.IsAbstract).LifestyleSingleton());


            State = ServiceState.Started;
        }

        public override void Stop()
        {
            if (State == ServiceState.Stopped)
            {
                return;
            }

            State = ServiceState.Stopped;
        }
    }
}
