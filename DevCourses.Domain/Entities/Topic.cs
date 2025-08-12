namespace DevCourses.Domain.Entities
{
    public class Topic :BaseEntity
    {
        public string Title { get; set; }
        public int Order { get; set; } // Konuların sıralaması

        // İlişkiler
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}
