using System.Collections.Generic;
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

            foreach (var title in client.GetTitles(IMDbcrawlJobData))
            {
                yield return title;
            }
        }
    }
}
