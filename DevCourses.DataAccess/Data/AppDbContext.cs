using DevCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DevCourses.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        #region DbSet Tanımları
        // I. Kullanıcı ve Profil
        public DbSet<User> Users { get; set; }

        // II. Öğrenme İçeriği
        public DbSet<Language> Languages { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<VocabularyWord> VocabularyWords { get; set; }
        public DbSet<Conversation> Conversations { get; set; }

        // III. Kullanıcı İlerlemesi
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<UserVocabulary> UserVocabularies { get; set; }

        // IV. Oyunlaştırma
        public DbSet<League> Leagues { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<UserAward> UserAwards { get; set; }

        // V. Sosyal
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        // VI. Ek İçerik ve Teknik
        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Log> Logs { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ====================================================================
            // I. KULLANICI VE PROFİL YAPILANDIRMALARI
            // ====================================================================
            modelBuilder.Entity<User>(entity =>
            {
                // Benzersiz Alanlar (Unique Constraints)
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.UserName).IsUnique();

                // Gerekli Alanlar ve Uzunluk Kısıtlamaları
                entity.Property(u => u.FullName).IsRequired().HasMaxLength(150);
                entity.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
                entity.Property(u => u.PasswordHash).IsRequired();

                // İlişki: Bir kullanıcının bir ligi olabilir. Lig silinirse kullanıcı silinmez.
                entity.HasOne(u => u.CurrentLeague)
                      .WithMany() // League'den User'a direkt bir koleksiyon bağlantısı yok.
                      .HasForeignKey(u => u.CurrentLeagueId)
                      .OnDelete(DeleteBehavior.SetNull); // Lig silinirse, kullanıcının lig alanı null olur.
            });


            // ====================================================================
            // III. KULLANICI İLERLEMESİ (ARA TABLOLAR)
            // ====================================================================
            modelBuilder.Entity<UserProgress>(entity =>
            {
                // Composite Primary Key (Bileşik Anahtar)
                entity.HasKey(up => new { up.UserId, up.LessonId });

                // İlişki: Kullanıcı silinirse, ilerlemesi de silinir.
                entity.HasOne(up => up.User)
                      .WithMany(u => u.ProgressRecords)
                      .HasForeignKey(up => up.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserVocabulary>(entity =>
            {
                // Composite Primary Key
                entity.HasKey(uv => new { uv.UserId, uv.VocabularyWordId });

                // İlişki: Kullanıcı silinirse, kelime listesi de silinir.
                entity.HasOne(uv => uv.User)
                      .WithMany(u => u.Vocabulary)
                      .HasForeignKey(uv => uv.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ====================================================================
            // IV. OYUNLAŞTIRMA (ARA TABLO)
            // ====================================================================
            modelBuilder.Entity<UserAward>(entity =>
            {
                // Composite Primary Key
                entity.HasKey(ua => new { ua.UserId, ua.AwardId });

                // İlişki: Kullanıcı silinirse, kazandığı ödüller de silinir.
                entity.HasOne(ua => ua.User)
                      .WithMany(u => u.Awards)
                      .HasForeignKey(ua => ua.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ====================================================================
            // V. SOSYAL MODELLER (Karmaşık İlişkiler)
            // ====================================================================
            modelBuilder.Entity<Friendship>(entity =>
            {
                // Composite Primary Key
                entity.HasKey(f => new { f.RequesterId, f.ReceiverId });

                // İlişki: Arkadaşlık isteği gönderen kullanıcı (Requester)
                entity.HasOne(f => f.Requester)
                      .WithMany() // User sınıfında direkt bir 'GonderilenIstekler' koleksiyonu yok
                      .HasForeignKey(f => f.RequesterId)
                      .OnDelete(DeleteBehavior.Restrict); // Kullanıcı silinirse arkadaşlık kaydı silinemez, hata verir. Önce arkadaşlık silinmeli.

                // İlişki: Arkadaşlık isteği alan kullanıcı (Receiver)
                entity.HasOne(f => f.Receiver)
                      .WithMany()
                      .HasForeignKey(f => f.ReceiverId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                // İlişki: Gönderen
                entity.HasOne(m => m.Sender)
                      .WithMany()
                      .HasForeignKey(m => m.SenderId)
                      .OnDelete(DeleteBehavior.Restrict); // Kullanıcı silinirse mesajları kalmalı ki diğer kullanıcının sohbet geçmişi bozulmasın.

                // İlişki: Alan
                entity.HasOne(m => m.Receiver)
                      .WithMany()
                      .HasForeignKey(m => m.ReceiverId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ====================================================================
            // DİĞER HİYERARŞİK İLİŞKİLER (Cascade Delete)
            // Bir üst katman silindiğinde alt katmanların da silinmesi mantıklı olan yerler.
            // ====================================================================

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Language)
                .WithMany()
                .HasForeignKey(c => c.LanguageId)
                .OnDelete(DeleteBehavior.Restrict); // Dil silinirse, o dildeki kurslar silinmesin, hata versin.

            modelBuilder.Entity<Topic>()
                .HasOne(t => t.Course)
                .WithMany(c => c.Topics)
                .HasForeignKey(t => t.CourseId)
                .OnDelete(DeleteBehavior.Cascade); // Kurs silinirse, konuları da silinsin.

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Topic)
                .WithMany(t => t.Lessons)
                .HasForeignKey(l => l.TopicId)
                .OnDelete(DeleteBehavior.Cascade); // Konu silinirse, dersleri de silinsin.
        }

    }
}
