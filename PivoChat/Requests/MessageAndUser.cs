using PivoChat.Models;

namespace PivoChat.Requests;

public class MessageAndUser
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public User User { get; set; }
    public Guid ChatroomId { get; set; }
    
}