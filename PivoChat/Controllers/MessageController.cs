using Microsoft.AspNetCore.Mvc;
using PivoChat.Database;
using PivoChat.Models;
using PivoChat.Requests;

namespace PivoChat.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly ChatContext _context;

    public MessageController(ChatContext context)
    {
        _context = context;
    }

    
    [HttpGet("{id}")]   // GET /api/message/124
    public async Task<IActionResult> GetMessage([FromRoute]Guid id)
    {
        try
        {
            var message = await _context.ChatMessages.FindAsync(id);
            if(message is null)
                return NotFound();
            
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
    [HttpPost]   // POST /api/message
    public async Task<IActionResult> CreateChatMessage([FromRoute]Guid id, [FromBody] CreateMessage request)
    {
        try
        {
            Message message = new Message
            {
                Text = request.Text, 
                ChatroomId = request.ChatId,
                UserId = request.UserId
            };
        
            var res =await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
    
    [HttpPatch("{id}")]   // PATCH /api/chat/124/message
    public async Task<IActionResult> UpdateMessage([FromRoute]Guid id, [FromBody] UpdateMessage request)
    {
        try
        {
            var message = await _context.ChatMessages.FindAsync(id);
            if(message is null)
                return NotFound();

            if(string.IsNullOrWhiteSpace(request.Text))
                message.Text = request.Text!;
            
            var res = _context.ChatMessages.Update(message);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
    [HttpDelete("{id}")]   // DELETE /api/message/124/message
    public async Task<IActionResult> DeleteMessage([FromRoute]Guid id)
    {
        try
        {
            var message = await _context.ChatMessages.FindAsync(id);
            if(message is null)
                return NotFound();
            
            var res = _context.ChatMessages.Remove(message);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
}

