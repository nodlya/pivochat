namespace PivoChat.Models
{
    public class Chatroom
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool isDelete { get; set; } = false;
        
        public ICollection<ChatRoomUsers> ChatRoomUsers { get; set; } = new List<ChatRoomUsers>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
