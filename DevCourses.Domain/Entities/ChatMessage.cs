namespace DevCourses.Domain.Entities
{
    public class ChatMessage : BaseEntity
    {
        public string Content { get; set; }
        public bool IsRead { get; set; }

        // İlişkiler
        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
