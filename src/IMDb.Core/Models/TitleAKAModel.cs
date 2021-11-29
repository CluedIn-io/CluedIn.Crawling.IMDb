using System.Collections.Generic;

namespace CluedIn.Crawling.IMDb.Core.Models
{
    public class TitleAKAModel
    {
        /// <summary>
        /// titleId (string) - a tconst, an alphanumeric unique identifier of the title
        /// </summary>
        public string TitleId { get; set; }
        
        /// <summary>
        /// ordering (integer) – a number to uniquely identify rows for a given titleId
        /// </summary>
        public int Ordering { get; set; }
        
        /// <summary>
        /// title (string) – the localized title
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// region (string) - the region for this version of the title
        /// </summary>
        public string Region { get; set; }
        
        /// <summary>
        /// language (string) - the language of the title
        /// </summary>
        public string Language { get; set; }
        
        /// <summary>
        /// types (array) - Enumerated set of attributes for this alternative title.
        /// One or more of the following:
        /// "alternative", "dvd", "festival", "tv", "video", "working", "original", "imdbDisplay".
        /// New values may be added in the future without warning
        /// </summary>
        public List<string> Types { get; set; }

        /// <summary>
        /// attributes (array) - Additional terms to describe this alternative title, not enumerated
        /// </summary>
        public List<string> Attributes { get; set; }
        
        /// <summary>
        /// isOriginalTitle (boolean) – 0: not original title; 1: original title
        /// </summary>
        public bool IsOriginalTitle { get; set; }
    }
}
