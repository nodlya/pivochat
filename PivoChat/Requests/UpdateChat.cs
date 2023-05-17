using System.ComponentModel.DataAnnotations;

namespace PivoChat.Requests;

public class UpdateChat
{
    [Required]
    [MinLength(3)]
    public string Title { get; set; }
}