using Microsoft.AspNetCore.Mvc;
using PivoChat.Database;
using PivoChat.Models;
using PivoChat.Requests;

namespace PivoChat.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    
    private readonly ChatContext _context;

    public ChatController(ChatContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]   // GET /api/chat/124
    public async Task<IActionResult> GetChat([FromRoute]Guid id)
    {
        try
        {
            var chatroom = await _context.Chatroom.FindAsync(id);
            if(chatroom is null)
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
   
    [HttpPost]   // POST /api/chat
    public async Task<IActionResult> CreateChat([FromBody] CreateChat request)
    { 
        try
        {
            ICollection<User> users = new List<User>(request.Users.Select(x => _context.Users.Find(x)));
            
            Chatroom chatroom = new Chatroom
            {
                Users = users,
                Title = request.Title
            };

            var res = await _context.Chatroom.AddAsync(chatroom);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
    
    [HttpPost("{id}/invite")]   // POST /api/chat/111/invite
    public async Task<IActionResult> InviteChat([FromRoute]Guid id, [FromBody] InviteUser request)
    { 
        try
        {
            var chatroom = await _context.Chatroom.FindAsync(id);
            var user = await _context.Users.FindAsync(request.userId);
            if (chatroom is null || user is null)
                return NotFound();
            
            chatroom.Users.Add(user);
            var res = await _context.Chatroom.AddAsync(chatroom);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }

    [HttpPatch("{id}")]   // PATCH /api/chat/124
    public async Task<IActionResult> UpdateChat([FromRoute]Guid id, [FromBody] UpdateChat request)
    {
        try
        {
            var chatroom = await _context.Chatroom.FindAsync(id);
            if(chatroom is null)
                return NotFound();

            if(string.IsNullOrWhiteSpace(request.Title))
                chatroom.Title = request.Title!;
            
            var res = _context.Chatroom.Update(chatroom);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
    [HttpDelete("{id}")]   // DELETE /api/chat/124
    public async Task<IActionResult> SoftDeleteChat([FromRoute] Guid id)
    {
        try
        {
            var chatroom =await _context.Chatroom.FindAsync(id);
            if(chatroom is null)
                return NotFound();

            chatroom.isDelete = true;
         
            var res = _context.Chatroom.Update(chatroom!);
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