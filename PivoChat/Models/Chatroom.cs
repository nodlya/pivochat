namespace PivoChat.Models
{
    public class Chatroom
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool isDelete { get; set; } = false;
        
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
