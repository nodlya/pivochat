namespace PivoChat.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public Guid ChatroomId { get; set; }
    }
}
