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

            var clue = _factory.Create(IMDbConstants.EntityTypes.NameBasic, input.PersonId, accountId);

            var data = clue.Data.EntityData;
            var properties = data.Properties;

            var titlePrincipalVocabulary = IMDbVocabularyFactory.TitlePrincipalVocabulary;

            // tconst (string) - alphanumeric unique identifier of the title
            properties[titlePrincipalVocabulary.TitleId] = input.TitleId;

            _factory.CreateOutgoingEntityReference(clue, IMDbConstants.EntityTypes.TitleBasic,
                IMDbConstants.EntityEdgeTypes.PrincipalOf, input, input.TitleId.PrintIfAvailable());

            // ordering(integer) – a number to uniquely identify rows for a given titleId
            properties[titlePrincipalVocabulary.Ordering] = input.Ordering.PrintIfAvailable();
            // nconst(string) - alphanumeric unique identifier of the name / person
            properties[titlePrincipalVocabulary.PersonId] = input.PersonId;
            // category(string) - the category of job that person was in
            properties[titlePrincipalVocabulary.Category] = input.Category;
            // job(string) - the specific job title if applicable, else '\N'
            properties[titlePrincipalVocabulary.Job] = input.Job;
            // characters(string) - the name of the character played if applicable, else '\N'
            properties[titlePrincipalVocabulary.Characters] = input.Characters;

            if (data.OutgoingEdges.Count == 0) _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}