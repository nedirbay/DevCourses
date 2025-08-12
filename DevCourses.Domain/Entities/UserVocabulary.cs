namespace DevCourses.Domain.Entities
{
    public class UserVocabulary
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid VocabularyWordId { get; set; }
        public VocabularyWord VocabularyWord { get; set; }

        public DateTime NextReviewDate { get; set; } // Bu kelimenin tekrar ne zaman sorulacağı
        public int ReviewStage { get; set; } // SRS'teki aşaması
    }
}
