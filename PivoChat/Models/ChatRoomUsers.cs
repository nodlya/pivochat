namespace PivoChat.Models;

public class ChatRoomUsers
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
        
    public Guid ChatroomId { get; set; }
}