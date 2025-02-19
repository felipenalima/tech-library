using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TechLibrary.Domain.Entities;

namespace TechLibrary.Infraestructure.Security.Tokens.Access;

public class JwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        };
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(securityToken);
    }

    private static SymmetricSecurityKey SecurityKey()
    {
        var signingkey = "uO6xe9RglLqbSu7BnZbvsyCraJz3ueWc"; // key example, choose yours, 32 characters

        var symmetricKey = Encoding.UTF8.GetBytes(signingkey);
        
        return new SymmetricSecurityKey(symmetricKey);
    }
}