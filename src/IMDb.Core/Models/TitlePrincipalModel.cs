namespace CluedIn.Crawling.IMDb.Core.Models
{
    public class TitlePrincipalModel
    {
        /// <summary>
        /// tconst(string) - alphanumeric identifier of episode
        /// </summary>
        public string TitleId { get; set; }


        /// <summary>
        /// ordering(integer) – a number to uniquely identify rows for a given titleId
        /// </summary>
        public int Ordering { get; set; }

        /// <summary>
        /// nconst(string) - alphanumeric unique identifier of the name/person
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// category(string) - the category of job that person was in
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// job(string) - the specific job title if applicable, else '\N'
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// characters(string) - the name of the character played if applicable, else '\N'
        /// </summary>
        public string Characters { get; set; }
    }
}