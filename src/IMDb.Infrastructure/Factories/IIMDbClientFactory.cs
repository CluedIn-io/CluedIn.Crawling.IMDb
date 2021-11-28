using CluedIn.Crawling.IMDb.Core;

namespace CluedIn.Crawling.IMDb.Infrastructure.Factories
{
    public interface IIMDbClientFactory
    {
        IMDbClient CreateNew(IMDbCrawlJobData IMDbCrawlJobData);
    }
}
