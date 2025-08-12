namespace DevCourses.Domain.Entities
{
    public class Conversation : BaseEntity
    {
        public string Scenario { get; set; } // "Restoranda sipariş verme"
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }

    public enum LanguageLevel { A1, A2, B1, B2, C1, C2 }
    public enum LessonType { Vocabulary, Grammar, Listening, Reading, Speaking, Conversation, Test }
}
