using Microsoft.VisualBasic;

namespace DevCourses.Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public string Title { get; set; }
        public LessonType Type { get; set; } // Kelime mi, Gramer mi, Konuşma mı?
        public int Order { get; set; }
        public int XpReward { get; set; } // Bu dersi bitirince kazanılacak puan

        // İlişkiler
        public Guid TopicId { get; set; }
        public Topic Topic { get; set; }
        public ICollection<VocabularyWord> VocabularyWords { get; set; } // Bu dersteki kelimeler
        public ICollection<Conversation> Conversations { get; set; } // Bu dersteki diyaloglar
    }
}
