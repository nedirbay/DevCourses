namespace DevCourses.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string TargetUrl { get; set; } // Bildirime tıklayınca gidilecek sayfa

        // İlişkiler
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
