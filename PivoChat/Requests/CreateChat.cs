namespace PivoChat.Requests;

public class CreateChat
{
    public ICollection<Guid> Users { get; set; }
    public string Title { get; set; }
}