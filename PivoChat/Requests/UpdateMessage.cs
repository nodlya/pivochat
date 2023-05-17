using System.ComponentModel.DataAnnotations;

namespace PivoChat.Requests;

public class UpdateMessage
{
    [Required]
    public string Text { get; set; }
}