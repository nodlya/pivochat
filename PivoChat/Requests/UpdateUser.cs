namespace PivoChat.Requests;

public class UpdateUser
{
    public string? Name { get; set; } = null;
    public string? Login { get; set; } = null;
    public string? Password { get; set; } = null;
}