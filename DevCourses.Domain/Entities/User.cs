using Microsoft.AspNetCore.Identity;

namespace DevCourses.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public UserRole Role { get; set; } // Kullanıcı mı, Admin mi?

        // Öğrenme ve Oyunlaştırma Bilgileri
        public int ExperiencePoints { get; set; } // XP Puanı
        public int Streak { get; set; } // Kaç gündür aralıksız çalıştığı
        public DateTime LastActivityDate { get; set; }
        public int Gems { get; set; } // Sanal para birimi

        // İlişkiler
        public Guid? CurrentLeagueId { get; set; } // Mevcut ligi
        public League CurrentLeague { get; set; }

        public ICollection<UserProgress> ProgressRecords { get; set; }
        public ICollection<UserAward> Awards { get; set; }
        public ICollection<UserVocabulary> Vocabulary { get; set; }

    }

    public enum UserRole { Student, Admin, ContentCreator }

}
