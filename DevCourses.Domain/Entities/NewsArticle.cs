namespace DevCourses.Domain.Entities
{
    public class NewsArticle : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string SourceUrl { get; set; }

        // İlişkiler
        public Guid LanguageId { get; set; }
        public Language Language { get; set; }
        public LanguageLevel Level { get; set; }
    }
}
