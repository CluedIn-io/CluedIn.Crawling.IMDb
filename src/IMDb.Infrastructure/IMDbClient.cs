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

        public IEnumerable<NameBasicModel> GetNames(IMDbCrawlJobData jobData)
        {
            var tsvPath = Path.Join("tsv", "name.basics.tsv");

            using StreamReader streamReader = new StreamReader(tsvPath);

            streamReader.ReadLine(); // skip the header

            while (!streamReader.EndOfStream)
            {
                NameBasicModel nameBasicModel;

                try
                {
                    var columns = streamReader.ReadLine()?.Split("\t");

                    if (columns == null)
                    {
                        continue;
                    }
                    
                    nameBasicModel = new NameBasicModel
                    {
                        PersonId = columns[0],
                        PrimaryName = columns[1],
                        BirthYear = columns[2],
                        DeathYear = columns[3],
                        PrimaryProfession = columns[4]
                            .Replace("[", string.Empty)
                            .Replace("]", string.Empty)
                            .Replace("\'", string.Empty)
                            .Split(",")
                            .Select(x => x.Trim())
                            .ToList(),
                        KnownForTitles = columns[5]
                            .Replace("[", string.Empty)
                            .Replace("]", string.Empty)
                            .Replace("\'", string.Empty)
                            .Split(",")
                            .Select(x => x.Trim())
                            .ToList(),
                    };
                }
                catch (Exception e)
                {
                    _log.LogError(e, e.Message);
                    continue;
                }

                yield return nameBasicModel;
            }
        }

        public IEnumerable<TitleAKAModel> GetTitleAKAs(IMDbCrawlJobData jobData)
        {
            var tsvPath = Path.Join("tsv", "title.akas.tsv");

            using StreamReader streamReader = new StreamReader(tsvPath);
            
            streamReader.ReadLine(); // skip the header

            while (!streamReader.EndOfStream)
            {
                TitleAKAModel titleAkaModel;
                
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
                        Types = columns[5]
                            .Replace("[", string.Empty)
                            .Replace("]", string.Empty)
                            .Replace("\'", string.Empty)
                            .Split(",")
                            .Select(x => x.Trim())
                            .ToList(),
                        Attributes = columns[6]
                            .Replace("[", string.Empty)
                            .Replace("]", string.Empty)
                            .Replace("\'", string.Empty)
                            .Split(",")
                            .Select(x => x.Trim())
                            .ToList(),
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
        }

        public IEnumerable<TitleBasicModel> GetTitleBasics(IMDbCrawlJobData jobData)
        {
            var tsvPath = Path.Join("tsv", "title.basics.tsv");

            using StreamReader streamReader = new StreamReader(tsvPath);

            streamReader.ReadLine(); // skip the header

            while (!streamReader.EndOfStream)
            {
                TitleBasicModel titleBasicModel;

                try
                {
                    var columns = streamReader.ReadLine()?.Split("\t");

                    if (columns == null)
                    {
                        continue;
                    }

                    titleBasicModel = new TitleBasicModel
                    {
                        TitleId = columns[0],
                        TitleType = columns[1],
                        PrimaryTitle = columns[2],
                        OriginalTitle = columns[3],
                        IsAdult = columns[4] == "1",
                        StartYear = columns[5],
                        EndYear = columns[6],
                        RuntimeMinutes = columns[7],
                        Genres = columns[8]
                            .Replace("[", string.Empty)
                            .Replace("]", string.Empty)
                            .Replace("\'", string.Empty)
                            .Split(",")
                            .Select(x => x.Trim())
                            .ToList(),
                    };
                }
                catch (Exception e)
                {
                    _log.LogError(e, e.Message);
                    continue;
                }

                yield return titleBasicModel;
            }
        }

        public IEnumerable<TitleCrewModel> GetTitleCrew(IMDbCrawlJobData jobData)
        {
            var tsvPath = Path.Join("tsv", "title.crew.tsv");

            using StreamReader streamReader = new StreamReader(tsvPath);

            streamReader.ReadLine(); // skip the header

            while (!streamReader.EndOfStream)
            {
                TitleCrewModel titleCrewModel;

                try
                {
                    var columns = streamReader.ReadLine()?.Split("\t");

                    if (columns == null)
                    {
                        continue;
                    }

                    titleCrewModel = new TitleCrewModel
                    {
                        TitleId = columns[0],
                        Directors = columns[1]
                            .Replace("[", string.Empty)
                            .Replace("]", string.Empty)
                            .Replace("\'", string.Empty)
                            .Split(",")
                            .Select(x => x.Trim())
                            .ToList(),
                        Writers = columns[2]
                            .Replace("[", string.Empty)
                            .Replace("]", string.Empty)
                            .Replace("\'", string.Empty)
                            .Split(",")
                            .Select(x => x.Trim())
                            .ToList(),
                    };
                }
                catch (Exception e)
                {
                    _log.LogError(e, e.Message);
                    continue;
                }

                yield return titleCrewModel;
            }
        }

        public IEnumerable<TitleRatingModel> GetTitleRatings(IMDbCrawlJobData jobData)
        {
            var tsvPath = Path.Join("tsv", "title.ratings.tsv");

            using StreamReader streamReader = new StreamReader(tsvPath);

            streamReader.ReadLine(); // skip the header

            while (!streamReader.EndOfStream)
            {
                TitleRatingModel titleRatingModel;

                try
                {
                    var columns = streamReader.ReadLine()?.Split("\t");

                    if (columns == null)
                    {
                        continue;
                    }

                    titleRatingModel = new TitleRatingModel
                    {
                        TitleId = columns[0],
                        AverageRating = double.Parse(columns[1]),
                        NumVotes = (int)double.Parse(columns[2]),
                    };
                }
                catch (Exception e)
                {
                    _log.LogError(e, e.Message);
                    continue;
                }

                yield return titleRatingModel;
            }
        }

        public IEnumerable<TitlePrincipalModel> GetTitlePrincipals(IMDbCrawlJobData jobData)
        {
            var tsvPath = Path.Join("tsv", "title.principals.tsv");

            using StreamReader streamReader = new StreamReader(tsvPath);

            streamReader.ReadLine(); // skip the header

            while (!streamReader.EndOfStream)
            {
                TitlePrincipalModel titlePrincipalModel;

                try
                {
                    var columns = streamReader.ReadLine()?.Split("\t");

                    if (columns == null)
                    {
                        continue;
                    }

                    titlePrincipalModel = new TitlePrincipalModel
                    {
                        TitleId = columns[0],
                        Ordering = int.Parse(columns[1]),
                        PersonId = columns[2],
                        Category = columns[3],
                        Job = columns[4],
                        Characters = columns[5],
                    };
                }
                catch (Exception e)
                {
                    _log.LogError(e, e.Message);
                    continue;
                }

                yield return titlePrincipalModel;
            }
        }
    }
}
