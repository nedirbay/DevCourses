namespace DevCourses.Domain.Entities
{
    public class League : BaseEntity
    {
        public string Name { get; set; }
        public int MinExperiencePoints { get; set; } // Bu lige girmek için gereken minimum XP
        public string IconUrl { get; set; }
        public int Order { get; set; }
    }
}
