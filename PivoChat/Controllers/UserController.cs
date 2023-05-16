using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PivoChat.Database;
using PivoChat.Requests;

namespace PivoChat.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ChatContext _context;

    public UserController(ChatContext context)
    {
        _context = context;
    }

    [HttpGet("{id}/chats")]  // GET /api/user/124/chats
    public async Task<IActionResult> GetAllUserChats([FromRoute]Guid id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if(user is null)
                return NotFound();
            
            var ChatroomsID = _context.ChatRoomUsers.Where(x => x.UserId == user.Id).ToList();
            var res = ChatroomsID.Select(x => _context.Chatroom.Find(x.ChatroomId)).ToList();
            
            return Ok(res);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
    [Authorize]
    [HttpGet("/users")]  // GET /api/users
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var res = _context.Users.ToList();
            if(res is null)
                return NotFound();

            return Ok(res);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
    
    [HttpGet("{id}")]   // GET /api/user/124
    public async Task<IActionResult> GetUser([FromRoute]Guid id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if(user is null)
                return NotFound();
            
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
    
    [HttpPatch("{id}")]   // Patch/api/user/124
    public async Task<IActionResult> UpdateUser([FromRoute]Guid id, [FromBody] UpdateUser request)
    {
        try
        {
            var user =await _context.Users.FindAsync(id);
            if(user is null)
                return NotFound();
            
            if (!string.IsNullOrWhiteSpace(request.Login))
                user.Login = request.Login!;
            if (!string.IsNullOrWhiteSpace(request.Name))
                user.Name = request.Name!;
            if (!string.IsNullOrWhiteSpace(request.Password))
                user.Password = request.Password!;

            _context.Users.Update(user!);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
    [HttpDelete("{id}")]   // Delete /api/user/124
    public async Task<IActionResult> SoftDeleteUser([FromRoute]Guid id)
    {
        try
        {
            var user =await _context.Users.FindAsync(id);
            if(user is null)
                return NotFound();

            user.isBan = true;
         
            _context.Users.Update(user!);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }		
}