using System.Collections.Generic;

namespace CluedIn.Crawling.IMDb.Core.Models
{
    public class TitleCrewModel
    {
        /// <summary>
        /// tconst(string) - alphanumeric unique identifier of the title
        /// </summary>
        public string TitleId { get; set; }

        /// <summary>
        /// directors(array of nconsts) - director(s) of the given title
        /// </summary>
        public List<string> Directors { get; set; }

        /// <summary>
        /// writers(array of nconsts) – writer(s) of the given title
        /// </summary>
        public List<string> Writers { get; set; }
    }
}