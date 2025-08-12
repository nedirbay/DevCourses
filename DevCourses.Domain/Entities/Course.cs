namespace DevCourses.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public LanguageLevel Level { get; set; } // A1, A2, B1...
        public string ImageUrl { get; set; }

        // İlişkiler
        public Guid LanguageId { get; set; }
        public Language Language { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}
