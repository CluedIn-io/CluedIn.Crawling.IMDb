using System.Collections.Generic;

namespace CluedIn.Crawling.IMDb.Core.Models
{
    public class NameBasicModel
    {
        /// <summary>
        /// nconst(string) - alphanumeric unique identifier of the name/person
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// primaryName(string)– name by which the person is most often credited
        /// </summary>
        public string PrimaryName { get; set; }

        /// <summary>
        /// birthYear – in YYYY format
        /// </summary>
        public string BirthYear { get; set; }

        /// <summary>
        /// deathYear – in YYYY format if applicable, else '\N'
        /// </summary>
        public string DeathYear { get; set; }

        /// <summary>
        /// primaryProfession(array of strings)– the top-3 professions of the person
        /// </summary>
        public List<string> PrimaryProfession { get; set; }

        /// <summary>
        /// knownForTitles(array of tconsts) – titles the person is known for
        /// </summary>
        public List<string> KnownForTitles { get; set; }
    }
}