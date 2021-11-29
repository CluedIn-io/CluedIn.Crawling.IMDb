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
    public class TitleEpisodeClueProducer : BaseClueProducer<TitleEpisodeModel>
    {
        private readonly IClueFactory _factory;

        public TitleEpisodeClueProducer([NotNull] IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(TitleEpisodeModel input, Guid accountId)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(IMDbConstants.EntityTypes.TitleEpisode, input.TitleId, accountId);

            var data = clue.Data.EntityData;
            var properties = data.Properties;

            var titleEpisodeVocabulary = IMDbVocabularyFactory.TitleEpisodeVocabulary;

            // tconst (string) - alphanumeric unique identifier of the title
            properties[titleEpisodeVocabulary.TitleId] = input.TitleId;

            // parentTconst(string) -alphanumeric identifier of the parent TV Series
            properties[titleEpisodeVocabulary.ParentTitleId] = input.ParentTitleId;
            // seasonNumber(integer) – season number the episode belongs to
            properties[titleEpisodeVocabulary.SeasonNumber] = input.SeasonNumber.PrintIfAvailable();
            // episodeNumber(integer) – episode number of the tconst in the TV series
            properties[titleEpisodeVocabulary.EpisodeNumber] = input.EpisodeNumber.PrintIfAvailable();

            _factory.CreateOutgoingEntityReference(clue, IMDbConstants.EntityTypes.TitleBasic,
                IMDbConstants.EntityEdgeTypes.EpisodeOf, input, input.EpisodeNumber.PrintIfAvailable());

            if (data.OutgoingEdges.Count == 0) _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}