namespace DevCourses.Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        // Which student registered?
        public Guid StudentId { get; set; }
        public User Student { get; set; }

        // Which course registered
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public double CompletionPercentage { get; set; } = 0; // Kurs ilerlemesi

    }
}
