using CluedIn.Core.Data.Vocabularies;
using CluedIn.Crawling.IMDb.Core;

namespace CluedIn.Crawling.IMDb.Vocabularies
{
    public class TitlePrincipalVocabulary : SimpleVocabulary
    {
        public TitlePrincipalVocabulary()
        {
            VocabularyName = "IMDb Title Principal";
            KeyPrefix = "IMDb.title.principal";
            KeySeparator = ".";
            Grouping = IMDbConstants.EntityTypes.TitlePrincipal;

            AddGroup("IMDb Title Principal", group =>
            {
                TitleId = group.Add(new VocabularyKey(nameof(TitleId), VocabularyKeyDataType.Identifier))
                    .WithDescription("tconst (string) - alphanumeric unique identifier of the title");
                Ordering = group.Add(new VocabularyKey(nameof(Ordering), VocabularyKeyDataType.Integer))
                    .WithDescription("ordering (integer) – a number to uniquely identify rows for a given titleId");
                PersonId = group.Add(new VocabularyKey(nameof(PersonId), VocabularyKeyDataType.Identifier))
                    .WithDescription("nconst (string) - alphanumeric unique identifier of the name/person");
                Category = group.Add(new VocabularyKey(nameof(Category)))
                    .WithDescription("category (string) - the category of job that person was in");
                Job = group.Add(new VocabularyKey(nameof(Job)))
                    .WithDescription("job (string) - the specific job title if applicable, else '\\N'");
                Characters = group.Add(new VocabularyKey(nameof(Characters)))
                    .WithDescription(
                        "characters (string) - the name of the character played if applicable, else '\\N'");
            });

            // TODO: Mappings
        }

        /// <summary>
        ///     tconst (string) - alphanumeric unique identifier of the title
        /// </summary>
        public VocabularyKey TitleId { get; private set; }

        /// <summary>
        ///     ordering (integer) – a number to uniquely identify rows for a given titleId
        /// </summary>
        public VocabularyKey Ordering { get; private set; }

        /// <summary>
        ///     nconst (string) - alphanumeric unique identifier of the name/person
        /// </summary>
        public VocabularyKey PersonId { get; private set; }

        /// <summary>
        ///     category (string) - the category of job that person was in
        /// </summary>
        public VocabularyKey Category { get; private set; }

        /// <summary>
        ///     job (string) - the specific job title if applicable, else '\N'
        /// </summary>
        public VocabularyKey Job { get; private set; }

        /// <summary>
        ///     characters (string) - the name of the character played if applicable, else '\N'
        /// </summary>
        public VocabularyKey Characters { get; private set; }
    }
}