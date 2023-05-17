using System.ComponentModel.DataAnnotations;

namespace PivoChat.Requests;

public class InviteUser
{
    [Required]
    public Guid userId { get; set; }
}