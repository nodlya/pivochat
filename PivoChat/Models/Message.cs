namespace PivoChat.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public User OwnerUser { get; set; }
    }

}
