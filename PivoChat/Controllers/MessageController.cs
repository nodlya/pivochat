using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using PivoChat.Database;
using PivoChat.Models;
using PivoChat.Requests;

namespace PivoChat.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly ChatContext _context;
    IHubContext<ChatHub> hubContext;
    public MessageController(ChatContext context,IHubContext<ChatHub> hubContext)
    {
        _context = context;
        this.hubContext = hubContext;
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
            return Ok(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
    [HttpPost]   // POST /api/message
    public async Task<IActionResult> CreateChatMessage([FromBody] CreateMessage request)
    {
        try
        {
            Message message = new Message
            {
                Text = request.Text, 
                ChatroomId = request.ChatId,
                UserId = request.UserId
            };
            
            
            await hubContext.Clients.Group(request.ChatId.ToString()).SendAsync("SendMessage",message );

            await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();
           
            return Ok(message);
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

            if(!string.IsNullOrWhiteSpace(request.Text))
                message.Text = request.Text!;
            
            _context.ChatMessages.Update(message);
            await _context.SaveChangesAsync();
            return Ok(message);
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
            
            _context.ChatMessages.Remove(message);
            await _context.SaveChangesAsync();
            return Ok(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
}

