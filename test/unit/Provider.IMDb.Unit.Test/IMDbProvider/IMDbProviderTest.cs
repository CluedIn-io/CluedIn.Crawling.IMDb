using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Crawling.IMDb.Infrastructure.Factories;
using Moq;

namespace CluedIn.Crawling.Provider.IMDb.Unit.Test.IMDbProvider
{
    public abstract class IMDbProviderTest
    {
        protected readonly ProviderBase Sut;
        protected Mock<IWindsorContainer> Container;

        protected Mock<IIMDbClientFactory> NameClientFactory;

        protected IMDbProviderTest()
        {
            Container = new Mock<IWindsorContainer>();
            NameClientFactory = new Mock<IIMDbClientFactory>();
            var applicationContext = new ApplicationContext(Container.Object);
            Sut = new IMDb.IMDbProvider(applicationContext, NameClientFactory.Object);
        }
    }
}
