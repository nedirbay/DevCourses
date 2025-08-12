namespace DevCourses.Domain.Entities
{
    public class VocabularyWord : BaseEntity
    {
        public string Text { get; set; } // "House"
        public string Translation { get; set; } // "Ev"
        public string? Phonetic { get; set; } // Fonetik okunuş
        public string? ExampleSentence { get; set; }
        public string? AudioUrl { get; set; } // Kelimenin sesli okunuşu

        // İlişkiler
        public Guid LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
