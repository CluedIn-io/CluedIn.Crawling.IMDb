namespace CluedIn.Crawling.IMDb.Core.Models
{
    public class TitleEpisodeModel
    {
        /// <summary>
        /// tconst(string) - alphanumeric identifier of episode
        /// </summary>
        public string TitleId { get; set; }


        /// <summary>
        /// parentTconst(string) - alphanumeric identifier of the parent TV Series
        /// </summary>
        public string ParentTitleId { get; set; }

        /// <summary>
        /// seasonNumber(integer) – season number the episode belongs to
        /// </summary>
        public int SeasonNumber { get; set; }

        /// <summary>
        /// episodeNumber(integer) – episode number of the tconst in the TV series
        /// </summary>
        public int EpisodeNumber { get; set; }
    }
}