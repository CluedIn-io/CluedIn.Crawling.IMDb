using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CluedIn.Core.Providers;
using CluedIn.Crawling.IMDb.Core;
using CluedIn.Crawling.IMDb.Core.Models;
using Microsoft.Extensions.Logging;

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

        private async Task DownloadFileAsync(string requestUri, string path)
        {
            await using Stream stream = await new HttpClient().GetStreamAsync(requestUri);
            await using FileStream fileStream
                = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, true);
            await stream.CopyToAsync(fileStream);
        }


        private async Task UnzipFileAsync(string zipPath, string outputPath)
        {
            await using FileStream compressedFileStream = File.Open(zipPath, FileMode.Open);
            await using FileStream outputFileStream = File.Create(outputPath);
            await using var decompressor = new GZipStream(compressedFileStream, CompressionMode.Decompress);
            decompressor.CopyTo(outputFileStream);
        }

        public IEnumerable<TitleAKAModel> GetTitleAKAs(IMDbCrawlJobData jobData)
        {
            var guid = Guid.NewGuid().ToString();
            var zipPath = $"title.akas.{guid}.tsv.gz";
            DownloadFileAsync("https://datasets.imdbws.com/title.akas.tsv.gz", zipPath)
                .GetAwaiter()
                .GetResult();

            var tsvPath = $"title.akas.{guid}.tsv";
            UnzipFileAsync(zipPath, tsvPath)
                .GetAwaiter()
                .GetResult();

            using StreamReader streamReader = new StreamReader(tsvPath);
            
            streamReader.ReadLine(); // skip the header

            while (!streamReader.EndOfStream)
            {
                TitleAKAModel titleAkaModel = null;
                
                try
                {
                    var columns = streamReader.ReadLine()?.Split("\t");
                    
                    if (columns == null)
                    {
                        continue;
                    }
                    
                    titleAkaModel = new TitleAKAModel
                    {
                        TitleId = columns[0],
                        Ordering = int.Parse(columns[1]),
                        Title = columns[2],
                        Region = columns[3],
                        Language = columns[4],
                        Types = columns[5].Split(",").ToList(),
                        Attributes = columns[6].Split(",").ToList(),
                        IsOriginalTitle = columns[7] == "1"
                    };
                }
                catch (Exception e)
                {
                    _log.LogError(e, e.Message);
                    continue;
                }
                
                yield return titleAkaModel;
            }
            
            File.Delete(zipPath);
            File.Delete(tsvPath);
        }
    }
}
