namespace PivoChat.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public bool isBan { get; set; } = false;
        
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<ChatRoomUsers> ChatRoomUsers { get; set; } = new List<ChatRoomUsers>();
    }

}
