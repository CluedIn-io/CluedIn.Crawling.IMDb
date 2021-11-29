using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.IMDb.Core;
using CluedIn.Crawling.IMDb.Core.Models;
using CluedIn.Crawling.IMDb.Vocabularies;

namespace CluedIn.Crawling.IMDb.ClueProducers
{
    public class NameBasicClueProducer : BaseClueProducer<NameBasicModel>
    {
        private readonly IClueFactory _factory;

        public NameBasicClueProducer([NotNull] IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(NameBasicModel input, Guid accountId)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var clue = _factory.Create(IMDbConstants.EntityTypes.NameBasic, input.PersonId, accountId);

            var data = clue.Data.EntityData;
            var properties = data.Properties;

            data.Name = input.PrimaryName;

            var nameBasicVocabulary = IMDbVocabularyFactory.NameBasicVocabulary;

            // nconst(string) - alphanumeric unique identifier of the name/person
            properties[nameBasicVocabulary.PersonId] = input.PersonId;
            // primaryName(string)– name by which the person is most often credited
            properties[nameBasicVocabulary.PrimaryName] = input.PrimaryName;
            // birthYear – in YYYY format
            properties[nameBasicVocabulary.BirthYear] = input.BirthYear;
            // deathYear – in YYYY format if applicable, else '\N'
            properties[nameBasicVocabulary.DeathYear] = input.DeathYear;
            // primaryProfession(array of strings)– the top-3 professions of the person
            foreach (var profession in input.PrimaryProfession)
            {
                data.Tags.Add(new Tag($"IMDb:Name:Profession:{profession}"));
            }
            // knownForTitles(array of tconsts) – titles the person is known for
            foreach (var title in input.KnownForTitles)
            {
                // (Name Basic)-[:KnownFor]->(Title)
                _factory.CreateOutgoingEntityReference(clue, IMDbConstants.EntityTypes.TitleBasic,
                    IMDbConstants.EntityEdgeTypes.KnownFor, input, title);
            }

            if (data.OutgoingEdges.Count == 0)
            {
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);
            }

            return clue;
        }
    }
}
