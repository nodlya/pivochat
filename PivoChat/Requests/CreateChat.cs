using System.ComponentModel.DataAnnotations;

namespace PivoChat.Requests;

public class CreateChat
{
    [Required]
    public ICollection<Guid> Users { get; set; }
    [Required]
    [MinLength(3)]
    public string Title { get; set; }
}