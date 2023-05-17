using System.ComponentModel.DataAnnotations;

namespace PivoChat.Requests;

public class AuthUser
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
}