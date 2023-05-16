using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PivoChat.Models;

namespace PivoChat.Services;

public static class JwtToken
{
    public static string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Name, user.Name)
        };
        var jwt = new JwtSecurityToken(
            issuer: "AuthOptions.ISSUER",
            audience: "AuthOptions.AUDIENCE",
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // время действия 2 минуты
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes("'very_secret_and_complex_key_12345'")), 
                SecurityAlgorithms.HmacSha256));
            
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

}