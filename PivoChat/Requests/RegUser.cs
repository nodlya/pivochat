using System.ComponentModel.DataAnnotations;

namespace PivoChat.Requests;

public class CreateUser
{
    [Required]
    public string Name { get; set; }
    [Required]
    [MinLength(5)]
    public string Login { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}