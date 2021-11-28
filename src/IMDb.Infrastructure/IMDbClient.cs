using System;
using System.Collections.Generic;
using System.Net;
using CluedIn.Core.Providers;
using CluedIn.Crawling.IMDb.Core;
using CluedIn.Crawling.IMDb.Core.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace CluedIn.Crawling.IMDb.Infrastructure
{
    public class IMDbClient
    {
        private readonly ILogger<IMDbClient> _log;

        public IMDbClient(ILogger<IMDbClient> log, IMDbCrawlJobData IMDbCrawlJobData)
        {
            if (IMDbCrawlJobData == null)
            {
                throw new ArgumentNullException(nameof(IMDbCrawlJobData));
            }

            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public AccountInformation GetAccountInformation()
        {
            //TODO - return some unique information about the remote data source
            // that uniquely identifies the account
            return new AccountInformation("", "");
        }

        public IEnumerable<TitleModel> GetTitles(IMDbCrawlJobData jobData)
        {
            yield break;
        }
    }
}
