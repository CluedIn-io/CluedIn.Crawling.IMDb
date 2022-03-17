using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.IMDb.Core.Models;

namespace CluedIn.Crawling.IMDb.ClueProducers
{
    public class EmployeeClueProducer : BaseClueProducer<Employee>
    {
        private readonly IClueFactory _factory;

        public EmployeeClueProducer([NotNull] IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(Employee input, Guid accountId)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Person, input.FirstName, accountId);

            var data = clue.Data.EntityData;
            var properties = data.Properties;

            data.Name = input.FirstName;

            data.Codes.Add(new EntityCode(EntityType.Person, "Test", input.FirstName));

            var vocabulary = CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson;

            _factory.CreateOutgoingEntityReference(clue, EntityType.Organization, EntityEdgeType.WorksFor, input, input.Company);


            properties[vocabulary.FirstName] = input.FirstName;
            properties[vocabulary.LastName] = input.LastName;

            if (data.OutgoingEdges.Count == 0) _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}