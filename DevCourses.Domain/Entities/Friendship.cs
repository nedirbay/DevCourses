namespace DevCourses.Domain.Entities
{
    public class Friendship
    {
        public Guid RequesterId { get; set; } // İstek gönderen kullanıcı
        public User Requester { get; set; }
        public Guid ReceiverId { get; set; } // İsteği alan kullanıcı
        public User Receiver { get; set; }

        public FriendshipStatus Status { get; set; }
        public DateTime RequestedAt { get; set; }
    }

    public enum FriendshipStatus
    {
        Unknown                
    }
}
