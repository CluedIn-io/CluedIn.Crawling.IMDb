using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.IMDb.Core.Models;

namespace CluedIn.Crawling.IMDb.ClueProducers
{
    public class CompanyClueProducer : BaseClueProducer<Company>
    {
        private readonly IClueFactory _factory;

        public CompanyClueProducer([NotNull] IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(Company input, Guid accountId)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Organization, input.Name, accountId);
            
            var data = clue.Data.EntityData;
            var properties = data.Properties;

            data.Codes.Add(new EntityCode(EntityType.Organization, "Test", input.Name));

            data.Name = input.Name;

            var vocabulary = CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization;

            properties[vocabulary.OrganizationName] = input.Name;

            if (data.OutgoingEdges.Count == 0) _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}