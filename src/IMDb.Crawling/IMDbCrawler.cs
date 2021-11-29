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

            foreach (var nameBasicModel in client.GetNames(IMDbcrawlJobData))
            {
                yield return nameBasicModel;
            }

            foreach (var basicModel in client.GetTitleBasics(IMDbcrawlJobData))
            {
                yield return basicModel;
            }

            foreach (var titleAkaModel in client.GetTitleAKAs(IMDbcrawlJobData))
            {
                yield return titleAkaModel;
            }

            foreach (var titleCrewModel in client.GetTitleCrew(IMDbcrawlJobData))
            {
                yield return titleCrewModel;
            }

            foreach (var titleRatingModel in client.GetTitleRatings(IMDbcrawlJobData))
            {
                yield return titleRatingModel;
            }

            client.Cleanup();
        }
    }
}
