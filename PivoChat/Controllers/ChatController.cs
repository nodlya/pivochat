using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PivoChat.Database;
using PivoChat.Models;
using PivoChat.Requests;

namespace PivoChat.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    
    private readonly ChatContext _context;
    IHubContext<ChatHub> hubContext;

    public ChatController(ChatContext context,IHubContext<ChatHub> hubContext)
    {
        _context = context;
        this.hubContext = hubContext;
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
            return Ok(chatroom);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
    
    [HttpGet("{id}/messages")]  // GET /api/chat/124/messages
    public async Task<IActionResult> GetAllChatMessages([FromRoute]Guid id)
    {
        try
        {
            var chat = await _context.Chatroom.FindAsync(id);
            if(chat is null)
                return NotFound();

            var messages = _context.ChatMessages
                .Where(x => x.ChatroomId == chat.Id);
                
            messages = messages.Include(x => x.User);
            messages = messages.OrderBy(x => x.CreateDate);

            return Ok(messages);
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
            Chatroom chatroom = new Chatroom
            {
                Title = request.Title
            };
            
            ICollection<ChatRoomUsers> users = new List<ChatRoomUsers>(request.Users.Select(x => new ChatRoomUsers
            {
                UserId = x,
                ChatroomId = chatroom.Id
            }));
            
            chatroom.ChatRoomUsers = users;

            foreach (var user in users)
            {
                await hubContext.Groups.AddToGroupAsync(user.Id.ToString(), chatroom.Id.ToString());    
            }
            
            
            await _context.ChatRoomUsers.AddRangeAsync(users);
            await _context.Chatroom.AddAsync(chatroom);
            await _context.SaveChangesAsync();
            return Ok(chatroom);
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

            ChatRoomUsers chatRoomUsers = new ChatRoomUsers
            {
                ChatroomId = chatroom.Id,
                UserId = user.Id
            };
            chatroom.ChatRoomUsers.Add(chatRoomUsers);

            await _context.ChatRoomUsers.AddAsync(chatRoomUsers);
            _context.Chatroom.Update(chatroom);
            await _context.SaveChangesAsync();
            return Ok(chatRoomUsers);
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
            
            _context.Chatroom.Update(chatroom);
            await _context.SaveChangesAsync();
            return Ok(chatroom);
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
         
             _context.Chatroom.Update(chatroom!);
            await _context.SaveChangesAsync();
            return Ok(chatroom);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }		
}