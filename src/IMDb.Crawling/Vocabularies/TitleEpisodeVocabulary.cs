using CluedIn.Core.Data.Vocabularies;
using CluedIn.Crawling.IMDb.Core;

namespace CluedIn.Crawling.IMDb.Vocabularies
{
    public class TitleEpisodeVocabulary : SimpleVocabulary
    {
        public TitleEpisodeVocabulary()
        {
            VocabularyName = "IMDb Title Episode";
            KeyPrefix = "IMDb.title.episode";
            KeySeparator = ".";
            Grouping = IMDbConstants.EntityTypes.TitleEpisode;

            AddGroup("IMDb Title Crew", group =>
            {
                TitleId = group.Add(new VocabularyKey(nameof(TitleId), VocabularyKeyDataType.Identifier))
                    .WithDescription("tconst (string) - alphanumeric unique identifier of the title");
                ParentTitleId = group.Add(new VocabularyKey(nameof(ParentTitleId), VocabularyKeyDataType.Identifier))
                    .WithDescription("parentTconst (string) - alphanumeric identifier of the parent TV Series");
                SeasonNumber = group.Add(new VocabularyKey(nameof(SeasonNumber), VocabularyKeyDataType.Integer))
                    .WithDescription("seasonNumber (integer) – season number the episode belongs to");
                EpisodeNumber = group.Add(new VocabularyKey(nameof(EpisodeNumber), VocabularyKeyDataType.Integer))
                    .WithDescription("episodeNumber (integer) – episode number of the tconst in the TV series");
            });

            // TODO: Mappings
        }

        /// <summary>
        ///     tconst (string) - alphanumeric unique identifier of the title
        /// </summary>
        public VocabularyKey TitleId { get; private set; }

        /// <summary>
        ///     parentTconst (string) - alphanumeric identifier of the parent TV Series
        /// </summary>
        public VocabularyKey ParentTitleId { get; private set; }

        /// <summary>
        ///     seasonNumber (integer) – season number the episode belongs to
        /// </summary>
        public VocabularyKey SeasonNumber { get; private set; }

        /// <summary>
        ///     episodeNumber (integer) – episode number of the tconst in the TV series
        /// </summary>
        public VocabularyKey EpisodeNumber { get; private set; }
    }
}