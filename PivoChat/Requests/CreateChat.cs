namespace PivoChat.Requests;

public class CreateChat
{
    public ICollection<String> Users { get; set; }
    public string Title { get; set; }
}