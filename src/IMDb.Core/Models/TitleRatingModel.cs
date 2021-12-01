namespace CluedIn.Crawling.IMDb.Core.Models
{
    public class TitleRatingModel
    {
        /// <summary>
        /// tconst(string) - alphanumeric identifier of episode
        /// </summary>
        public string TitleId { get; set; }


        /// <summary>
        /// averageRating – weighted average of all the individual user ratings
        /// </summary>
        public double AverageRating { get; set; }

        /// <summary>
        /// numVotes - number of votes the title has received
        /// </summary>
        public double NumVotes { get; set; }
    }
}