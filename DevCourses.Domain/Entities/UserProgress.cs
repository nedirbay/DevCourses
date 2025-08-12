namespace DevCourses.Domain.Entities
{
    public class UserProgress : BaseEntity
    {
        // İlişkiler (Composite Key)
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public bool IsCompleted { get; set; }
        public int Score { get; set; } // Dersteki test puanı
    }
}
