using CluedIn.Core.Data.Vocabularies;
using CluedIn.Crawling.IMDb.Core;

namespace CluedIn.Crawling.IMDb.Vocabularies
{
    public class TitleBasicVocabulary : SimpleVocabulary
    {
        public TitleBasicVocabulary()
        {
            VocabularyName = "IMDb Title";
            KeyPrefix = "IMDb.title.basic";
            KeySeparator = ".";
            Grouping = IMDbConstants.EntityTypes.TitleBasic;

            AddGroup("IMDb Title", group =>
            {
                TitleId = group.Add(new VocabularyKey(nameof(TitleId), VocabularyKeyDataType.Identifier))
                    .WithDescription("tconst (string) - alphanumeric unique identifier of the title");
                TitleType = group.Add(new VocabularyKey(nameof(TitleType)))
                    .WithDescription(
                        "titleType (string) – the type/format of the title (e.g. movie, short, tvseries, tvepisode, video, etc)");
                PrimaryTitle = group.Add(new VocabularyKey(nameof(PrimaryTitle)))
                    .WithDescription(
                        "primaryTitle (string) – the more popular title / the title used by the filmmakers on promotional materials at the point of release");
                OriginalTitle = group.Add(new VocabularyKey(nameof(OriginalTitle)))
                    .WithDescription("originalTitle (string) - original title, in the original language");
                IsAdult = group.Add(new VocabularyKey(nameof(IsAdult), VocabularyKeyDataType.Boolean))
                    .WithDescription("isAdult (boolean) - 0: non-adult title; 1: adult title");
                StartYear = group.Add(new VocabularyKey(nameof(StartYear)))
                    .WithDescription(
                        "startYear (YYYY) – represents the release year of a title. In the case of TV Series, it is the series start year");
                EndYear = group.Add(new VocabularyKey(nameof(EndYear)))
                    .WithDescription("endYear (YYYY) – TV Series end year. ‘\\N’ for all other title types");
                RuntimeInMinutes = group.Add(new VocabularyKey(nameof(RuntimeInMinutes), VocabularyKeyDataType.Duration))
                    .WithDescription("runtimeMinutes – primary runtime of the title, in minutes");
            });

            AddGroup("IMDb Rating", group =>
            {
                AverageRating = group.Add(new VocabularyKey(nameof(AverageRating), VocabularyKeyDataType.Number))
                    .WithDescription("averageRating – weighted average of all the individual user ratings");
                NumVotes = group.Add(new VocabularyKey(nameof(NumVotes)))
                    .WithDescription("numVotes - number of votes the title has received");
            });

            // TODO: Mappings
        }

        /// <summary>
        ///     tconst (string) - alphanumeric unique identifier of the title
        /// </summary>
        public VocabularyKey TitleId { get; private set; }

        /// <summary>
        ///     titleType (string) – the type/format of the title (e.g. movie, short, tvseries, tvepisode, video, etc)
        /// </summary>
        public VocabularyKey TitleType { get; private set; }

        /// <summary>
        ///     primaryTitle (string) – the more popular title / the title used by the filmmakers on promotional materials at the
        ///     point of release
        /// </summary>
        public VocabularyKey PrimaryTitle { get; private set; }

        /// <summary>
        ///     originalTitle (string) - original title, in the original language
        /// </summary>
        public VocabularyKey OriginalTitle { get; private set; }

        /// <summary>
        ///     isAdult (boolean) - 0: non-adult title; 1: adult title
        /// </summary>
        public VocabularyKey IsAdult { get; private set; }

        /// <summary>
        ///     startYear (YYYY) – represents the release year of a title. In the case of TV Series, it is the series start year
        /// </summary>
        public VocabularyKey StartYear { get; private set; }

        /// <summary>
        ///     endYear (YYYY) – TV Series end year. ‘\N’ for all other title types
        /// </summary>
        public VocabularyKey EndYear { get; private set; }

        /// <summary>
        ///     runtimeMinutes – primary runtime of the title, in minutes
        /// </summary>
        public VocabularyKey RuntimeInMinutes { get; private set; }

        /// <summary>
        ///     averageRating – weighted average of all the individual user ratings
        /// </summary>
        public VocabularyKey AverageRating { get; private set; }

        /// <summary>
        ///     numVotes - number of votes the title has received
        /// </summary>
        public VocabularyKey NumVotes { get; private set; }
    }
}