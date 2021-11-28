namespace CluedIn.Crawling.IMDb.Vocabularies
{
    public static class IMDbVocabularyFactory
    {
        private static readonly TitleAKAVocabulary TitleAkaVocabulary;

        static IMDbVocabularyFactory()
        {
            TitleAkaVocabulary = new TitleAKAVocabulary();
        }

        public static TitleAKAVocabulary GetTitleAKAVocabulary()
        {
            return TitleAkaVocabulary;
        }
    }
}
