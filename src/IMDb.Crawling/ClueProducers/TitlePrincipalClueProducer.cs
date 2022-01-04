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
    /// <summary>
    /// TODO:
    /// </summary>
    public class TitlePrinciipalClueProducer : BaseClueProducer<TitlePrincipalModel>
    {
        private readonly IClueFactory _factory;

        public TitlePrinciipalClueProducer([NotNull] IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(TitlePrincipalModel input, Guid accountId)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Infrastructure.User, input.PersonId, accountId);

            var data = clue.Data.EntityData;
            var properties = data.Properties;

            var nameBasicVocabulary = IMDbVocabularyFactory.NameBasicVocabulary;

            _factory.CreateOutgoingEntityReference(clue, IMDbConstants.EntityTypes.TitleBasic,
                IMDbConstants.EntityEdgeTypes.PrincipalOf, input, input.TitleId.PrintIfAvailable());

            // nconst(string) - alphanumeric unique identifier of the name / person
            properties[nameBasicVocabulary.PersonId] = input.PersonId;
            _factory.CreateOutgoingEntityReference(clue, IMDbConstants.EntityTypes.TitleBasic,
                $"{IMDbConstants.EntityEdgeTypes.PrincipalOf}/Category/{input.Category}", input, input.TitleId.PrintIfAvailable());
            _factory.CreateOutgoingEntityReference(clue, IMDbConstants.EntityTypes.TitleBasic,
                $"{IMDbConstants.EntityEdgeTypes.PrincipalOf}/Job/{input.Job}", input, input.TitleId.PrintIfAvailable());
            if (data.OutgoingEdges.Count == 0) _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}