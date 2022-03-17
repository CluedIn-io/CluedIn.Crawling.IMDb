using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Crawling;
using CluedIn.Crawling.IMDb.Core;
using CluedIn.Crawling.IMDb.Core.Models;
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


            yield return new Employee
            {
                FirstName = "Roman",
                LastName = "Klymenko",
                Company = "CluedIn"
            };

            yield return new Employee
            {
                FirstName = "Roman",
                LastName = "Klimenko",
                Company = "Sitecore"
            };

            yield return new Company
            {
                Name = "CluedIn"
            };

            yield return new Company
            {
                Name = "Sitecore"
            };
        }
    }
}
