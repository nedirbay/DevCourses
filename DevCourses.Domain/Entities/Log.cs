namespace DevCourses.Domain.Entities
{
    public class Log : BaseEntity
    {
        public string Level { get; set; } // "Info", "Warning", "Error"
        public string Message { get; set; }
        public string? StackTrace { get; set; } // Hata durumunda
    }
}
