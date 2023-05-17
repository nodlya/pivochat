using System.ComponentModel.DataAnnotations;

namespace PivoChat.Requests;

public class CreateMessage
{
    [Required]
    public string Text { get; set; }
        
    [Required]
    public Guid UserId { get; set; }
        
    [Required]
    public Guid ChatId { get; set; }
}