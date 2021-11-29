using CluedIn.Core.Data.Vocabularies;
using CluedIn.Crawling.IMDb.Core;

namespace CluedIn.Crawling.IMDb.Vocabularies
{
    public class TitleCrewVocabulary : SimpleVocabulary
    {
        public TitleCrewVocabulary()
        {
            VocabularyName = "IMDb Title Crew";
            KeyPrefix = "IMDb.title.crew";
            KeySeparator = ".";
            Grouping = IMDbConstants.EntityTypes.TitleCrew;

            AddGroup("IMDb Title Crew", group =>
            {
                TitleId = group.Add(new VocabularyKey(nameof(TitleId), VocabularyKeyDataType.Identifier))
                    .WithDescription("tconst (string) - alphanumeric unique identifier of the title");
            });

            // TODO: Mappings
        }

        /// <summary>
        ///     tconst (string) - alphanumeric unique identifier of the title
        /// </summary>
        public VocabularyKey TitleId { get; private set; }
    }
}