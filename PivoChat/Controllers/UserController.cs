using Microsoft.AspNetCore.Mvc;
using PivoChat.Database;
using PivoChat.Models;
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

    /*
    [HttpGet("{id}")]  // GET /api/user/124/chats
    public async Task<IActionResult> GetAllUserChats([FromRoute]string id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if(user is null)
                return NotFound();

            var chats = user.Chatroom;
            return Ok(chats);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
    */
    
    [HttpGet("{id}")]   // GET /api/user/124
    public async Task<IActionResult> GetUser([FromRoute]Guid id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if(user is null)
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

    [HttpPost]   // Post/api/user
    public async Task<IActionResult> CreateUser([FromBody] CreateUser request)
    {
        try
        {
            User user = new User
            {
                Name = request.Name, 
                Login = request.Login
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok();
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
            
            if (string.IsNullOrWhiteSpace(request.Login))
                user!.Login = request.Login!;
            if (string.IsNullOrWhiteSpace(request.Name))
                user!.Name = request.Name!;

            var res = _context.Users.Update(user!);
            await _context.SaveChangesAsync();
            return Ok();
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
         
            var res = _context.Users.Update(user!);
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