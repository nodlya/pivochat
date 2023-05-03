using Microsoft.AspNetCore.Mvc;
using PivoChat.Requests;

namespace PivoChat.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    [HttpGet("{id}")]   // GET /api/message/124
    public async Task<IActionResult> GetMessage([FromRoute]string id)
    {
        //TODO вернуть сообщение по id;
        return Ok();
    }
    [HttpPost]   // POST /api/message
    public async Task<IActionResult> CreateChatMessage([FromRoute]string id, [FromBody] CreateMessage request)
    {
        //TODO добавить сообщение в чат;
        return Ok();
    }
    
    [HttpPatch("{id}")]   // PATCH /api/chat/124/message
    public async Task<IActionResult> UpdateMessage([FromRoute]string id, [FromBody] UpdateMessage request)
    {
        //TODO обновить сообщение;
        return Ok();
    }
    [HttpDelete("{id}")]   // DELETE /api/message/124/message
    public async Task<IActionResult> DeleteMessage([FromRoute]string id)
    {
        //TODO удалить сообщение из чата;
        return Ok();
    }
}