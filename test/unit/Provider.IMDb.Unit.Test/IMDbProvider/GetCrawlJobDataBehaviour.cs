using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using CluedIn.Core.Providers;
using CluedIn.Crawling.IMDb.Core;
using Xunit;

namespace CluedIn.Crawling.Provider.IMDb.Unit.Test.IMDbProvider
{
    public class GetCrawlJobDataBehaviour : IMDbProviderTest
    {
        private readonly ProviderUpdateContext _context;

        public GetCrawlJobDataBehaviour()
        {
            _context = null;
        }

        [Theory]
        [InlineAutoData]
        public async Task ReturnsData(Dictionary<string, object> dictionary, Guid organizationId, Guid userId,
            Guid providerDefinitionId)
        {
            Assert.NotNull(
                await Sut.GetCrawlJobData(_context, dictionary, organizationId, userId, providerDefinitionId));
        }

        [Theory]
        [InlineAutoData]
        public async Task IMDbCrawlJobDataReturned(Dictionary<string, object> dictionary, Guid organizationId,
            Guid userId, Guid providerDefinitionId)
        {
            Assert.IsType<IMDbCrawlJobData>(
                await Sut.GetCrawlJobData(_context, dictionary, organizationId, userId, providerDefinitionId));
        }
    }
}
