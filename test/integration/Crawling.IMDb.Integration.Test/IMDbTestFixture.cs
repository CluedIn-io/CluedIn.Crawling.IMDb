using System;
using System.IO;
using System.Reflection;
using Castle.MicroKernel.Registration;
using CluedIn.Crawling.IMDb.Core;
using CluedIn.Crawling.IMDb.Vocabularies;
using CrawlerIntegrationTesting.Clues;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit.Abstractions;
using DebugCrawlerHost =
    CrawlerIntegrationTesting.CrawlerHost.DebugCrawlerHost<CluedIn.Crawling.IMDb.Core.IMDbCrawlJobData>;

namespace CluedIn.Crawling.IMDb.Integration.Test
{
    public class IMDbTestFixture
    {
        private readonly DebugCrawlerHost debugCrawlerHost;

        public IMDbTestFixture()
        {
            var executingFolder = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            debugCrawlerHost = new DebugCrawlerHost(executingFolder, IMDbConstants.ProviderName, c =>
            {
                c.Register(Component.For<ILogger>().UsingFactoryMethod(_ => NullLogger.Instance).LifestyleSingleton());
                c.Register(Component.For<ILoggerFactory>().UsingFactoryMethod(_ => NullLoggerFactory.Instance)
                    .LifestyleSingleton());
            });

            ClueStorage = new ClueStorage();

            Log = debugCrawlerHost.AppContext.Container.Resolve<ILogger<IMDbTestFixture>>();

            debugCrawlerHost.ProcessClue += ClueStorage.AddClue;

            var vocab = new TitleAKAVocabulary();

            debugCrawlerHost.Execute(IMDbConfiguration.Create(), IMDbConstants.ProviderId);
        }

        public ClueStorage ClueStorage { get; }

        public ILogger<IMDbTestFixture> Log { get; }

        public void PrintClues(ITestOutputHelper output)
        {
            foreach (var clue in ClueStorage.Clues)
            {
                output.WriteLine(clue.OriginEntityCode.ToString());
            }
        }

        public void PrintLogs(ITestOutputHelper output)
        {
            //TODO:
            //output.WriteLine(Log.PrintLogs());
        }

        public void Dispose()
        {
        }
    }
}
