namespace CluedIn.Crawling.IMDb.Vocabularies
{
    public static class IMDbVocabularyFactory
    {
        public static readonly NameBasicVocabulary NameBasicVocabulary = new NameBasicVocabulary();
        public static readonly TitleAKAVocabulary TitleAKAVocabulary = new TitleAKAVocabulary();
        public static readonly TitleBasicVocabulary TitleBasicVocabulary = new TitleBasicVocabulary();
        public static readonly TitleCrewVocabulary TitleCrewVocabulary = new TitleCrewVocabulary();
        public static readonly TitleEpisodeVocabulary TitleEpisodeVocabulary = new TitleEpisodeVocabulary();
        public static readonly TitlePrincipalVocabulary TitlePrincipalVocabulary = new TitlePrincipalVocabulary();
    }
}
