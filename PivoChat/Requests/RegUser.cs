namespace PivoChat.Requests;

public class CreateUser
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}