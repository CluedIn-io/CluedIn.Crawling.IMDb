using CluedIn.Core.Crawling;
using CluedIn.Crawling;
using CluedIn.Crawling.IMDb.Infrastructure.Factories;
using Moq;
using Shouldly;
using Xunit;

namespace CluedIn.Crawling.IMDb.Unit.Test
{
    public class IMDbCrawlerBehaviour
    {
        private readonly ICrawlerDataGenerator _sut;

        public IMDbCrawlerBehaviour()
        {
            var nameClientFactory = new Mock<IIMDbClientFactory>();

            _sut = new IMDbCrawler(nameClientFactory.Object);
        }

        [Fact]
        public void GetDataReturnsData()
        {
            var jobData = new CrawlJobData();

            _sut.GetData(jobData)
                .ShouldNotBeNull();
        }
    }
}
