using CluedIn.Core.Crawling;

namespace CluedIn.Crawling.IMDb.Core
{
    public class IMDbCrawlJobData : CrawlJobData
    {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
    }
}
