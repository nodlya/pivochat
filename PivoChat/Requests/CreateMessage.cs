namespace PivoChat.Requests;

public class CreateMessage
{
    public string Text { get; set; }
        
    public string UserId { get; set; }
        
    public string ChatId { get; set; }
}