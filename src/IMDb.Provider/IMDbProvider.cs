using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using CluedIn.Core;
using CluedIn.Core.Configuration;
using CluedIn.Core.Crawling;
using CluedIn.Core.Data.Relational;
using CluedIn.Core.Providers;
using CluedIn.Core.Webhooks;
using CluedIn.Crawling.IMDb.Core;
using CluedIn.Crawling.IMDb.Infrastructure.Factories;
using CluedIn.Providers.Models;
using Newtonsoft.Json;

namespace CluedIn.Crawling.Provider.IMDb
{
    public class IMDbProvider : ProviderBase, IExtendedProviderMetadata
    {
        private readonly IIMDbClientFactory _IMDbClientFactory;

        public IMDbProvider([NotNull] ApplicationContext appContext, IIMDbClientFactory IMDbClientFactory)
            : base(appContext, IMDbConstants.CreateProviderMetadata())
        {
            _IMDbClientFactory = IMDbClientFactory;
        }

        public override bool ScheduleCrawlJobs { get; } = false;

        public string ServiceType { get; } = JsonConvert.SerializeObject(IMDbConstants.ServiceType);
        public string Aliases { get; } = JsonConvert.SerializeObject(IMDbConstants.Aliases);

        public string Details { get; set; } = IMDbConstants.Details;
        public string Category { get; set; } = IMDbConstants.Category;

        // TODO Please see https://cluedin-io.github.io/CluedIn.Documentation/docs/1-Integration/build-integration.html
        public string Icon => IMDbConstants.IconResourceName;
        public string Domain { get; } = IMDbConstants.Uri;
        public string About { get; } = IMDbConstants.CrawlerDescription;
        public AuthMethods AuthMethods { get; } = IMDbConstants.AuthMethods;
        public IEnumerable<Control> Properties => null;

        public Guide Guide { get; set; } = new Guide
        {
            Instructions = IMDbConstants.Instructions,
            Value = new List<string> {IMDbConstants.CrawlerDescription},
            Details = IMDbConstants.Details
        };

        public new IntegrationType Type { get; set; } = IMDbConstants.Type;

        public override async Task<CrawlJobData> GetCrawlJobData(
            ProviderUpdateContext context,
            IDictionary<string, object> configuration,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return await Task.FromResult(new IMDbCrawlJobData());
        }

        public override Task<bool> TestAuthentication(
            ProviderUpdateContext context,
            IDictionary<string, object> configuration,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId) =>
            Task.FromResult(true);

        public override Task<ExpectedStatistics> FetchUnSyncedEntityStatistics(ExecutionContext context,
            IDictionary<string, object> configuration, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            throw new NotImplementedException();
        }

        public override async Task<IDictionary<string, object>> GetHelperConfiguration(
            ProviderUpdateContext context,
            [NotNull] CrawlJobData jobData,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            if (jobData == null)
            {
                throw new ArgumentNullException(nameof(jobData));
            }

            return await Task.FromResult(new Dictionary<string, object>());
        }

        public override Task<IDictionary<string, object>> GetHelperConfiguration(
            ProviderUpdateContext context,
            CrawlJobData jobData,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId,
            string folderId)
        {
            throw new NotImplementedException();
        }

        public override async Task<AccountInformation> GetAccountInformation(ExecutionContext context,
            [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            if (jobData == null)
            {
                throw new ArgumentNullException(nameof(jobData));
            }

            if (!(jobData is IMDbCrawlJobData IMDbCrawlJobData))
            {
                throw new Exception("Wrong CrawlJobData type");
            }

            var client = _IMDbClientFactory.CreateNew(IMDbCrawlJobData);
            return await Task.FromResult(client.GetAccountInformation());
        }

        public override string Schedule(DateTimeOffset relativeDateTime, bool webHooksEnabled)
        {
            return $"{relativeDateTime.Minute} 0/23 * * *";
        }

        public override Task<IEnumerable<WebHookSignature>> CreateWebHook(ExecutionContext context,
            [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition,
            [NotNull] IDictionary<string, object> config)
        {
            if (jobData == null)
            {
                throw new ArgumentNullException(nameof(jobData));
            }

            if (webhookDefinition == null)
            {
                throw new ArgumentNullException(nameof(webhookDefinition));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            throw new NotImplementedException();
        }

        public override Task<IEnumerable<WebhookDefinition>> GetWebHooks(ExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData,
            [NotNull] IWebhookDefinition webhookDefinition)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> WebhookManagementEndpoints([NotNull] IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public override async Task<CrawlLimit> GetRemainingApiAllowance(ExecutionContext context,
            [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            if (jobData == null)
            {
                throw new ArgumentNullException(nameof(jobData));
            }


            //There is no limit set, so you can pull as often and as much as you want.
            return await Task.FromResult(new CrawlLimit(-1, TimeSpan.Zero));
        }
    }
}
