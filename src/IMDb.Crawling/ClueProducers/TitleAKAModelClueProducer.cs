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
    public class TitleAKAModelClueProducer : BaseClueProducer<TitleAKAModel>
    {
        private readonly IClueFactory _factory;

        public TitleAKAModelClueProducer([NotNull] IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(TitleAKAModel input, Guid accountId)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var clue = _factory.Create(IMDbConstants.EntityTypes.TitleAKA, input.TitleId, accountId);

            var data = clue.Data.EntityData;
            var properties = data.Properties;

            data.Name = input.Title;

            var titleVocabulary = IMDbVocabularyFactory.GetTitleAKAVocabulary();
            
            // titleId (string) - a tconst, an alphanumeric unique identifier of the title
            properties[titleVocabulary.TitleId] = input.TitleId;
            
            // (Title AKA)-[:AKA]->(Title)
            _factory.CreateOutgoingEntityReference(clue, IMDbConstants.EntityTypes.Title,
                IMDbConstants.EntityEdgeTypes.AKA, input, input.TitleId); 
            
            // ordering (integer) – a number to uniquely identify rows for a given titleId
            properties[titleVocabulary.Ordering] = input.Ordering.PrintIfAvailable();
            // title (string) – the localized title
            properties[titleVocabulary.Title] = input.Title;
            // region (string) - the region for this version of the title
            properties[titleVocabulary.Region] = input.Region;
            // language (string) - the language of the title
            properties[titleVocabulary.Language] = input.Language;
            // types (array) - Enumerated set of attributes for this alternative title. One or more of the following: "alternative", "dvd", "festival", "tv", "video", "working", "original", "imdbDisplay". New values may be added in the future without warning
            foreach (var type in input.Types)
            {
                data.Tags.Add(new Tag($"IMDb:Type:{type}"));
            }
            // attributes (array) - Additional terms to describe this alternative title, not enumerated
            foreach (var attribute in input.Attributes)
            {
                data.Tags.Add(new Tag($"IMDb:Attribute:{attribute}"));
            }
            // isOriginalTitle (boolean) – 0: not original title; 1: original title
            properties[titleVocabulary.IsOriginalTitle] = input.IsOriginalTitle.PrintIfAvailable();

            if (data.OutgoingEdges.Count == 0)
            {
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);
            }

            return clue;
        }
    }
}
