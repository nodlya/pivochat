namespace PivoChat.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<Chatroom> Chatroom { get; set; } = new List<Chatroom>();
    }

}
