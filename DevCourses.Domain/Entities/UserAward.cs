namespace DevCourses.Domain.Entities
{
    public class UserAward
    {
        // İlişkiler (Composite Key)
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid AwardId { get; set; }
        public Award Award { get; set; }

        public DateTime EarnedAt { get; set; }
    }
}
