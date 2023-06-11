using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PivoChat.Database;
using PivoChat.Models;
using PivoChat.Requests;
using PivoChat.Services;

namespace PivoChat.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    
    private readonly ChatContext _context;

    public AuthController(ChatContext context)
    {
        _context = context;
    }
    
    [HttpPost("reg")]   // Post/api/reg
    public async Task<IActionResult> Registration([FromBody] CreateUser request)
    {
        try
        {
            User user = new User
            {
                Name = request.Name, 
                Login = request.Login,
                Password = request.Password
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var jwt = JwtToken.GenerateToken(user);
            
            
            return Ok(new { Jwt = jwt, User = user });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
    
    [HttpPost("login")]   // Post/api/reg
    public async Task<IActionResult> Login([FromBody] AuthUser request)
    {
        try
        {
            var user = _context.Users.First(x=> x.Login==request.Login && x.Password == request.Password);
            if (user is null )
            {
                throw new Exception("Неверный логин или пароль");
            }
            var jwt = JwtToken.GenerateToken(user);
            
            return Ok(new { Jwt = jwt, User = user });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        try //todo доделать, как достать jwt и откуда
        {
            var tokenFromCookie = Request.Cookies["jwt"];
 
            Response.Cookies.Delete(tokenFromCookie);
            return Ok("Вы успешно вышли из учетной записи");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
}