using System.Collections.Generic;

namespace CluedIn.Crawling.IMDb.Core.Models
{
    public class TitleBasicModel
    {
        /// <summary>
        /// tconst(string) - alphanumeric unique identifier of the title
        /// </summary>
        public string TitleId { get; set; }

        /// <summary>
        /// titleType(string) – the type/format of the title(e.g.movie, short, tvseries, tvepisode, video, etc)
        /// </summary>
        public string TitleType { get; set; }

        /// <summary>
        /// primaryTitle(string) – the more popular title / the title used by the filmmakers on promotional materials at the point of release
        /// </summary>
        public string PrimaryTitle { get; set; }

        /// <summary>
        /// originalTitle(string) - original title, in the original language
        /// </summary>
        public string OriginalTitle { get; set; }

        /// <summary>
        /// isAdult(boolean) - 0: non-adult title; 1: adult title
        /// </summary>
        public bool IsAdult { get; set; }

        /// <summary>
        /// startYear(YYYY) – represents the release year of a title.In the case of TV Series, it is the series start year
        /// </summary>
        public string StartYear { get; set; }

        /// <summary>
        /// endYear(YYYY) – TV Series end year. ‘\N’ for all other title types
        /// </summary>
        public string EndYear { get; set; }

        /// <summary>
        /// runtimeMinutes – primary runtime of the title, in minutes
        /// </summary>
        public string RuntimeMinutes { get; set; }

        /// <summary>
        /// genres (string array) – includes up to three genres associated with the title
        /// </summary>
        public List<string> Genres { get; set; }
    }
}