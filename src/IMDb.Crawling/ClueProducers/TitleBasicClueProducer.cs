using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.IMDb.Core;
using CluedIn.Crawling.IMDb.Core.Models;
using CluedIn.Crawling.IMDb.Vocabularies;

namespace CluedIn.Crawling.IMDb.ClueProducers
{
    public class TitleBasicClueProducer : BaseClueProducer<TitleBasicModel>
    {
        private readonly IClueFactory _factory;

        public TitleBasicClueProducer([NotNull] IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(TitleBasicModel input, Guid accountId)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(IMDbConstants.EntityTypes.TitleBasic, input.TitleId, accountId);

            var data = clue.Data.EntityData;
            var properties = data.Properties;

            data.Name = input.PrimaryTitle;

            var titleBasicVocabulary = IMDbVocabularyFactory.TitleBasicVocabulary;

            // tconst (string) - alphanumeric unique identifier of the title
            properties[titleBasicVocabulary.TitleId] = input.TitleId;
            // titleType (string) – the type/format of the title (e.g. movie, short, tvseries, tvepisode, video, etc)
            properties[titleBasicVocabulary.TitleType] = input.TitleType;
            // primaryTitle (string) – the more popular title / the title used by the filmmakers on promotional materials at the point of release
            properties[titleBasicVocabulary.PrimaryTitle] = input.PrimaryTitle;
            // originalTitle (string) - original title, in the original language
            properties[titleBasicVocabulary.OriginalTitle] = input.OriginalTitle;
            // isAdult (boolean) - 0: non-adult title; 1: adult title
            properties[titleBasicVocabulary.IsAdult] = input.IsAdult.PrintIfAvailable();
            // startYear (YYYY) – represents the release year of a title. In the case of TV Series, it is the series start year
            properties[titleBasicVocabulary.StartYear] = input.StartYear;
            // endYear (YYYY) – TV Series end year. ‘\N’ for all other title types
            properties[titleBasicVocabulary.EndYear] = input.EndYear;
            // runtimeMinutes – primary runtime of the title, in minutes
            properties[titleBasicVocabulary.RuntimeInMinutes] = input.RuntimeMinutes;
            // genres (string array) – includes up to three genres associated with the title
            foreach (var genre in input.Genres) data.Tags.Add(new Tag($"IMDb:Title:Genre:{genre}"));

            if (data.OutgoingEdges.Count == 0) _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}