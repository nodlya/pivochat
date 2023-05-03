using Microsoft.AspNetCore.Mvc;
using PivoChat.Requests;

namespace PivoChat.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    [HttpGet("{id}")]   // GET /api/chat/124
    public async Task<IActionResult> GetChat([FromRoute]string id)
    {
        //TODO вернуть чат по id;
        return Ok();
    }
   
    [HttpPost]   // POST /api/chat
    public async Task<IActionResult> CreateChat([FromBody] CreateChat response)
    {
        //TODO создать чата;
        return Ok();
    }
    [HttpPatch("{id}")]   // PATCH /api/chat/124
    public async Task<IActionResult> UpdateChat([FromRoute]string id, [FromBody] UpdateChat response)
    {
        //TODO обновить чат;
        return Ok();
    }
    [HttpDelete("{id}")]   // DELETE /api/chat/124
    public async Task<IActionResult> SoftDeleteChat([FromRoute] string id)
    {
        //TODO пометить чат как удаленный;
        return Ok();
    }		
}