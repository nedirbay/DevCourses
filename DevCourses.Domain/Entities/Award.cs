namespace DevCourses.Domain.Entities
{
    public class Award : BaseEntity
    {
        public string Title { get; set; } // "Seri Ustası"
        public string Description { get; set; } // "10 gün aralıksız çalıştın!"
        public string IconUrl { get; set; }
    }
}
