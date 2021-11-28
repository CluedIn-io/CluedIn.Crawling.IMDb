using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;
using CluedIn.Crawling.IMDb.Core;

namespace CluedIn.Crawling.IMDb.Vocabularies
{
    public class TitleAKAVocabulary : SimpleVocabulary
    {
        public TitleAKAVocabulary()
        {
            VocabularyName = "IMDb Title AKA";
            KeyPrefix = "IMDb.title.aka";
            KeySeparator = ".";
            Grouping = IMDbConstants.EntityTypes.TitleAKA;

            AddGroup("IMDb Title AKA", group =>
            {
                TitleId = group.Add(new VocabularyKey(nameof(TitleId), VocabularyKeyDataType.Identifier))
                    .WithDescription("titleId (string) - a tconst, an alphanumeric unique identifier of the title");
                Ordering = group.Add(new VocabularyKey(nameof(Ordering), VocabularyKeyDataType.Integer))
                    .WithDescription("ordering (integer) – a number to uniquely identify rows for a given titleId");
                Title = group.Add(new VocabularyKey(nameof(Title)))
                    .WithDescription("title (string) – the localized title");
                Region = group.Add(new VocabularyKey(nameof(Region)))
                    .WithDescription("region (string) - the region for this version of the title");
                Language = group.Add(new VocabularyKey(nameof(Language)))
                    .WithDescription("language (string) - the language of the title");
                IsOriginalTitle = group.Add(new VocabularyKey(nameof(IsOriginalTitle), VocabularyKeyDataType.Boolean))
                    .WithDescription("isOriginalTitle (boolean) – 0: not original title; 1: original title");
            });

            // TODO: Mappings
        }
        
        /// <summary>
        /// titleId (string) - a tconst, an alphanumeric unique identifier of the title
        /// </summary>
        public VocabularyKey TitleId { get; private set; }
        
        /// <summary>
        /// ordering (integer) – a number to uniquely identify rows for a given titleId
        /// </summary>
        public VocabularyKey Ordering { get; private set; }
        
        /// <summary>
        /// title (string) – the localized title
        /// </summary>
        public VocabularyKey Title { get; private set; }
        
        /// <summary>
        /// region (string) - the region for this version of the title
        /// </summary>
        public VocabularyKey Region { get; private set; }
        
        /// <summary>
        /// language (string) - the language of the title
        /// </summary>
        public VocabularyKey Language { get; private set; }
        
        /// <summary>
        /// isOriginalTitle (boolean) – 0: not original title; 1: original title
        /// </summary>
        public VocabularyKey IsOriginalTitle { get; private set; }
    }
}
