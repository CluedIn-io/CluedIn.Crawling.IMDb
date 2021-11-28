namespace CluedIn.Crawling.IMDb.Vocabularies
{
    public static class IMDbVocabularyFactory
    {
        private static readonly TitleVocabulary TitleVocabulary;

        static IMDbVocabularyFactory()
        {
            TitleVocabulary = new TitleVocabulary();
        }

        public static TitleVocabulary GetTitleVocabulary()
        {
            return TitleVocabulary;
        }
    }
}
