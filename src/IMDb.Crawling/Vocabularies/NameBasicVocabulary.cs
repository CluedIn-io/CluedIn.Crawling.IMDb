using CluedIn.Core.Data.Vocabularies;
using CluedIn.Crawling.IMDb.Core;

namespace CluedIn.Crawling.IMDb.Vocabularies
{
    public class NameBasicVocabulary : SimpleVocabulary
    {
        public NameBasicVocabulary()
        {
            VocabularyName = "IMDb Name";
            KeyPrefix = "IMDb.name.basic";
            KeySeparator = ".";
            Grouping = IMDbConstants.EntityTypes.NameBasic;

            AddGroup("IMDb Name", group =>
            {
                PersonId = group.Add(new VocabularyKey(nameof(PersonId), VocabularyKeyDataType.Identifier))
                    .WithDescription("nconst(string) - alphanumeric unique identifier of the name/person");
                PrimaryName = group.Add(new VocabularyKey(nameof(PrimaryName), VocabularyKeyDataType.PersonName))
                    .WithDescription("primaryName(string)– name by which the person is most often credited");
                BirthYear = group.Add(new VocabularyKey(nameof(BirthYear), VocabularyKeyDataType.DateTime))
                    .WithDescription("birthYear – in YYYY format");
                DeathYear = group.Add(new VocabularyKey(nameof(DeathYear), VocabularyKeyDataType.DateTime))
                    .WithDescription("deathYear – in YYYY format if applicable, else '\\N'");
            });

            // TODO: Mappings
        }

        /// <summary>
        ///     nconst(string) - alphanumeric unique identifier of the name/person
        /// </summary>
        public VocabularyKey PersonId { get; private set; }

        /// <summary>
        ///     primaryName(string)– name by which the person is most often credited
        /// </summary>
        public VocabularyKey PrimaryName { get; private set; }

        /// <summary>
        ///     birthYear – in YYYY format
        /// </summary>
        public VocabularyKey BirthYear { get; private set; }

        /// <summary>
        ///     deathYear – in YYYY format if applicable, else '\N'
        /// </summary>
        public VocabularyKey DeathYear { get; private set; }
    }
}