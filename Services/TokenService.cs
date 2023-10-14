using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shop.Models;

namespace Shop.Services {
  public static class TokenService {
    public static string GenerateToken(User user) {
      var tokenHandler = new JwtSecurityTokenHandler();
      var secretKey = Encoding.ASCII.GetBytes(Settings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor{
        Subject = new ClaimsIdentity(new Claim[]{
          new (ClaimTypes.Name, user.Username.ToString()),
          new (ClaimTypes.Name, user.Role.ToString())
        }),
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature),
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}