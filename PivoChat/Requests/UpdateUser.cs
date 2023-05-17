using System.ComponentModel.DataAnnotations;

namespace PivoChat.Requests;

public class UpdateUser
{
    public string? Name { get; set; } = null;
    [MinLength(5)]
    public string? Login { get; set; } = null;
    [MinLength(8)]
    public string? Password { get; set; } = null;
}