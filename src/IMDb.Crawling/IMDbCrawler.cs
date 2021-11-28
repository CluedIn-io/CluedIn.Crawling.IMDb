using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Crawling;
using CluedIn.Crawling.IMDb.Core;
using CluedIn.Crawling.IMDb.Infrastructure.Factories;

namespace CluedIn.Crawling.IMDb
{
    public class IMDbCrawler : ICrawlerDataGenerator
    {
        private readonly IIMDbClientFactory _clientFactory;

        public IMDbCrawler(IIMDbClientFactory clientFactory)
        {
            this._clientFactory = clientFactory;
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            if (!(jobData is IMDbCrawlJobData IMDbcrawlJobData))
            {
                yield break;
            }

            var client = _clientFactory.CreateNew(IMDbcrawlJobData);

            foreach (var titleAkaModel in client.GetTitleAKAs(IMDbcrawlJobData).Take(100))
            {
                yield return titleAkaModel;
            }
        }
    }
}
