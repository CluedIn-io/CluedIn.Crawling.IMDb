using System;
using System.Linq;
using AutoFixture.Xunit2;
using CluedIn.Core.Crawling;
using CluedIn.Crawling.IMDb.Core;
using Shouldly;
using Xunit;

namespace CluedIn.Crawling.Provider.IMDb.Unit.Test.IMDbProvider
{
    public class GetHelperConfigurationBehaviour : IMDbProviderTest
    {
        private readonly CrawlJobData _jobData;

        public GetHelperConfigurationBehaviour()
        {
            _jobData = new IMDbCrawlJobData();
        }

        [Fact]
        public void Throws_ArgumentNullException_With_Null_CrawlJobData_Parameter()
        {
            var ex = Assert.Throws<AggregateException>(
                () => Sut.GetHelperConfiguration(null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid())
                    .Wait());

            ((ArgumentNullException)ex.InnerExceptions.Single())
                .ParamName
                .ShouldBe("jobData");
        }

        [Theory]
        [InlineAutoData]
        public void Returns_ValidDictionary_Instance(Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            Sut.GetHelperConfiguration(null, _jobData, organizationId, userId, providerDefinitionId)
                .Result
                .ShouldNotBeNull();
        }
    }
}
