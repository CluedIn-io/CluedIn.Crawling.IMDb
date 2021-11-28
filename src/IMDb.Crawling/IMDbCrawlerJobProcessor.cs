using CluedIn.Crawling;
using CluedIn.Crawling.IMDb.Core;

namespace CluedIn.Crawling.IMDb
{
    public class IMDbCrawlerJobProcessor : GenericCrawlerTemplateJobProcessor<IMDbCrawlJobData>
    {
        public IMDbCrawlerJobProcessor(IMDbCrawlerComponent component) : base(component)
        {
        }
    }
}
