namespace PivoChat.Models;

public class UserToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }

    public User User { get; set; } = new User();
}