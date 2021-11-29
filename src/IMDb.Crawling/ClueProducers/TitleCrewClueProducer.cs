using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.IMDb.Core;
using CluedIn.Crawling.IMDb.Core.Models;
using CluedIn.Crawling.IMDb.Vocabularies;

namespace CluedIn.Crawling.IMDb.ClueProducers
{
    public class TitleCrewClueProducer : BaseClueProducer<TitleCrewModel>
    {
        private readonly IClueFactory _factory;

        public TitleCrewClueProducer([NotNull] IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(TitleCrewModel input, Guid accountId)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(IMDbConstants.EntityTypes.TitleCrew, input.TitleId, accountId);

            var data = clue.Data.EntityData;

            data.Name = input.TitleId;

            var properties = data.Properties;

            var titleBasicVocabulary = IMDbVocabularyFactory.TitleBasicVocabulary;

            // tconst (string) - alphanumeric unique identifier of the title
            properties[titleBasicVocabulary.TitleId] = input.TitleId;

            // directors(array of nconsts) -director(s) of the given title
            foreach (var director in input.Directors)
            {
                // (Name)-[:Director]->(Title)
                _factory.CreateIncomingEntityReference(clue, IMDbConstants.EntityTypes.NameBasic,
                    IMDbConstants.EntityEdgeTypes.DirectorOf, input, director); // TODO: check, if this is enough or we need to build an entity code
            }
            // writers(array of nconsts) – writer(s) of the given title
            foreach (var writer in input.Writers)
            {
                // (Name)-[:Writer]->(Title)
                _factory.CreateIncomingEntityReference(clue, IMDbConstants.EntityTypes.NameBasic,
                    IMDbConstants.EntityEdgeTypes.WriterOf, input, writer);
            }

            if (data.OutgoingEdges.Count == 0) _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}