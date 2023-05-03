namespace PivoChat.Requests;

public class CreateMessage
{
    public string Text { get; set; }
        
    public Guid UserId { get; set; }
        
    public Guid ChatId { get; set; }
}