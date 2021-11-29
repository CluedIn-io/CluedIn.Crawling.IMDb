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
    public class TitleRatingClueProducer : BaseClueProducer<TitleRatingModel>
    {
        private readonly IClueFactory _factory;

        public TitleRatingClueProducer([NotNull] IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(TitleRatingModel input, Guid accountId)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(IMDbConstants.EntityTypes.TitleBasic, input.TitleId, accountId);

            var data = clue.Data.EntityData;
            var properties = data.Properties;

            var titleBasicVocabulary = IMDbVocabularyFactory.TitleBasicVocabulary;

            // tconst (string) - alphanumeric unique identifier of the title
            properties[titleBasicVocabulary.TitleId] = input.TitleId;
            // averageRating – weighted average of all the individual user ratings
            properties[titleBasicVocabulary.AverageRating] = input.AverageRating.PrintIfAvailable();
            // numVotes - number of votes the title has received
            properties[titleBasicVocabulary.NumVotes] = input.NumVotes.PrintIfAvailable();

            if (data.OutgoingEdges.Count == 0) _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}