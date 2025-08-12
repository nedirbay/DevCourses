namespace DevCourses.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } // For uniq ID 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
