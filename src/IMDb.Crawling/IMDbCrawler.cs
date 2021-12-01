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

            // 11.3M
            foreach (var nameBasicModel in client.GetNames(IMDbcrawlJobData))
            {
                yield return nameBasicModel;
            }

            // 8.4M
            foreach (var basicModel in client.GetTitleBasics(IMDbcrawlJobData).Take(10))
            {
                yield return basicModel;
            }

            // 8.4M
            foreach (var titleCrewModel in client.GetTitleCrew(IMDbcrawlJobData).Take(10))
            {
                yield return titleCrewModel;
            }

            // 29M
            foreach (var titleAkaModel in client.GetTitleAKAs(IMDbcrawlJobData).Take(10))
            {
                yield return titleAkaModel;
            }

            // 1.1M
            foreach (var titleRatingModel in client.GetTitleRatings(IMDbcrawlJobData).Take(10))
            {
                yield return titleRatingModel;
            }
        }
    }
}
