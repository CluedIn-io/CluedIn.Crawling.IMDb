using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Crawling.IMDb.Infrastructure.Factories;
using RestSharp;

namespace CluedIn.Crawling.IMDb.Infrastructure.Installers
{
    public class InstallComponents : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .AddFacilityIfNotExists<TypedFactoryFacility>()
                .Register(Component.For<IIMDbClientFactory>().AsFactory())
                .Register(Component.For<IMDbClient>().LifestyleTransient());

            if (!container.Kernel.HasComponent(typeof(IRestClient)) &&
                !container.Kernel.HasComponent(typeof(RestClient)))
            {
                container.Register(Component.For<IRestClient, RestClient>());
            }
        }
    }
}
