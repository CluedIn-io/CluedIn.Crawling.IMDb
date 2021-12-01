using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Providers;
using CluedIn.Crawling.IMDb.Core;
using CluedIn.Crawling.IMDb.Core.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CluedIn.Crawling.IMDb.Infrastructure
{
    public class IMDbClient
    {
        private readonly ILogger<IMDbClient> _log;

        public IMDbClient(ILogger<IMDbClient> log, IMDbCrawlJobData IMDbCrawlJobData)
        {
            if (IMDbCrawlJobData == null) throw new ArgumentNullException(nameof(IMDbCrawlJobData));

            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public AccountInformation GetAccountInformation()
        {
            //TODO - return some unique information about the remote data source
            // that uniquely identifies the account
            return new AccountInformation("", "");
        }

        private static List<string> ToList(BsonArray bsonArray)
        {
            return bsonArray
                .Select(x => x.AsString)
                .Where(x => x != "\\N")
                .ToList();
        }

        public IEnumerable<NameBasicModel> GetNames(IMDbCrawlJobData jobData)
        {
            // mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false
            _log.LogInformation($"Connection String: {jobData.ConnectionString}");
            //var client = new MongoClient(jobData.ConnectionString);

            MongoClient client;

            try
            {
                client = new MongoClient(
                    "mongodb://mongo:27017/?readPreference=primary&directConnection=true&ssl=false");
                _log.LogInformation("connected");
            }
            catch (Exception e)
            {
                _log.LogError(e, e.Message);
                throw;
            }



            var db = client.GetDatabase("imdb");

            _log.LogInformation($"DB: {db.DatabaseNamespace.DatabaseName}");

            var collection = db.GetCollection<BsonDocument>("name.basics");

            _log.LogInformation($"Collection: {collection.CollectionNamespace.CollectionName}");

            var filter = Builders<BsonDocument>.Filter.Eq("sample10", true);

            var cursor = collection.Find(filter).ToCursor().ToEnumerable();

            NameBasicModel model;

            foreach (var document in cursor)
            {
                try
                {
                    _log.LogInformation($"here");
                    model = new NameBasicModel();
                    model.PersonId = document["nconst"].AsString;
                    model.PrimaryName = document["primaryName"].AsString;
                    model.BirthYear = document["birthYear"].AsString;
                    model.DeathYear = document["deathYear"].AsString;
                    model.PrimaryProfession = ToList(document["primaryProfession"].AsBsonArray);
                    model.KnownForTitles = ToList(document["knownForTitles"].AsBsonArray);
                }
                catch (Exception e)
                {
                    _log.LogError(e, e.Message);
                    continue;
                }

                yield return model;
            }
        }

        public IEnumerable<TitleAKAModel> GetTitleAKAs(IMDbCrawlJobData jobData)
        {
            var client = new MongoClient(jobData.ConnectionString);

            var db = client.GetDatabase("imdb");

            var collection = db.GetCollection<BsonDocument>("title.akas");

            var filter = Builders<BsonDocument>.Filter.Eq("sample10", true);

            var cursor = collection.Find(filter).ToCursor().ToEnumerable();

            TitleAKAModel model;

            foreach (var document in cursor)
            {
                try
                {
                    model = new TitleAKAModel();
                    model.TitleId = document["titleId"].AsString;
                    model.Ordering = int.Parse(document["ordering"].AsString); // TODO:
                    model.Title = document["title"].AsString;
                    model.Region = document["region"].AsString;
                    model.Language = document["language"].AsString;
                    model.Types = ToList(document["types"].AsBsonArray);
                    model.Attributes = ToList(document["attributes"].AsBsonArray);
                    model.IsOriginalTitle = document["isOriginalTitle"].AsString == "1";
                }
                catch (Exception e)
                {
                    _log.LogError(e, e.Message);
                    continue;
                }

                yield return model;
            }
        }

        public IEnumerable<TitleBasicModel> GetTitleBasics(IMDbCrawlJobData jobData)
        {
            var client = new MongoClient(jobData.ConnectionString);

            var db = client.GetDatabase("imdb");

            var collection = db.GetCollection<BsonDocument>("title.basics");

            var filter = Builders<BsonDocument>.Filter.Eq("sample10", true);

            var cursor = collection.Find(filter).ToCursor().ToEnumerable();

            TitleBasicModel model;

            foreach (var document in cursor)
            {
                try
                {
                    model = new TitleBasicModel();
                    model.TitleId = document["tconst"].AsString;
                    model.TitleType = document["titleType"].AsString;
                    model.PrimaryTitle = document["primaryTitle"].AsString;
                    model.OriginalTitle = document["originalTitle"].AsString;
                    model.IsAdult = document["isAdult"].AsString == "1";
                    model.StartYear = document["startYear"].AsString;
                    model.EndYear = document["endYear"].AsString;
                    model.RuntimeMinutes = document["runtimeMinutes"].AsString;
                    model.Genres = ToList(document["genres"].AsBsonArray);
                }
                catch (Exception e)
                {
                    _log.LogError(e, e.Message);
                    continue;
                }

                yield return model;
            }
        }

        public IEnumerable<TitleCrewModel> GetTitleCrew(IMDbCrawlJobData jobData)
        {
            var client = new MongoClient(jobData.ConnectionString);

            var db = client.GetDatabase("imdb");

            var collection = db.GetCollection<BsonDocument>("title.crew");

            var filter = Builders<BsonDocument>.Filter.Eq("sample10", true);

            var cursor = collection.Find(filter).ToCursor().ToEnumerable();

            TitleCrewModel model;

            foreach (var document in cursor)
            {
                try
                {
                    model = new TitleCrewModel();
                    model.TitleId = document["tconst"].AsString;
                    model.Directors = ToList(document["directors"].AsBsonArray);
                    model.Writers = ToList(document["writers"].AsBsonArray);
                }
                catch (Exception e)
                {
                    _log.LogError(e, e.Message);
                    continue;
                }

                yield return model;
            }
        }

        public IEnumerable<TitleRatingModel> GetTitleRatings(IMDbCrawlJobData jobData)
        {
            var client = new MongoClient(jobData.ConnectionString);

            var db = client.GetDatabase("imdb");

            var collection = db.GetCollection<BsonDocument>("title.ratings");

            var filter = Builders<BsonDocument>.Filter.Eq("sample10", true);

            var cursor = collection.Find(filter).ToCursor().ToEnumerable();

            TitleRatingModel model;

            foreach (var document in cursor)
            {
                try
                {
                    model = new TitleRatingModel();
                    model.TitleId = document["tconst"].AsString;
                    model.AverageRating = document["averageRating"].AsDouble;
                    model.NumVotes = document["numVotes"].AsDouble;
                }
                catch (Exception e)
                {
                    _log.LogError(e, e.Message);
                    continue;
                }

                yield return model;
            }
        }
    }
}