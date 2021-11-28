using CluedIn.Core;
using CluedIn.Crawling;
using CluedIn.Crawling.IMDb.Core;
using ComponentHost;

namespace CluedIn.Crawling.IMDb
{
    [Component(IMDbConstants.CrawlerComponentName, "Crawlers", ComponentType.Service, Components.Server,
        Components.ContentExtractors, Isolation = ComponentIsolation.NotIsolated)]
    public class IMDbCrawlerComponent : CrawlerComponentBase
    {
        public IMDbCrawlerComponent([NotNull] ComponentInfo componentInfo)
            : base(componentInfo)
        {
        }
    }
}
