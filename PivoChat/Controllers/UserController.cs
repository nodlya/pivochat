using Microsoft.AspNetCore.Mvc;
using PivoChat.Requests;

namespace PivoChat.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet("{id}")]   // GET /api/user/124
    public async Task<IActionResult> GetUser([FromRoute]string id)
    {
        //TODO вернуть пользователя по id;
        return Ok();
    }

    [HttpPost]   // Post/api/user
    public async Task<IActionResult> CreateUser([FromBody] CreateUser response)
    {
        //TODO создать пользователя;
        return Ok();
    }
    [HttpPatch("{id}")]   // Patch/api/user/124
    public async Task<IActionResult> UpdateUser([FromRoute]string id, [FromBody] UpdateUser response)
    {
        //TODO заменить пользователя;
        return Ok();
    }
    [HttpDelete("{id}")]   // Delete /api/user/124
    public async Task<IActionResult> SoftDeleteUser([FromRoute]string id)
    {
        //TODO пометить пользователя как удаленого;
        return Ok();
    }		
}