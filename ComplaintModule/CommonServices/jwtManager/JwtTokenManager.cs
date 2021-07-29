using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ApiServices.jwtManager
{
  public class JwtTokenManager : IJwtTokenManager
  {
    private readonly string _key;

    public JwtTokenManager(string key)
    {
      this._key = key;
    }

    public string GenerateToken(string username,string roles)
    {
      var jwtToken = new JwtSecurityTokenHandler();
      var tokenKey = Encoding.ASCII.GetBytes(this._key);
      var tokenDiscriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(new[]
        {
          new Claim(ClaimTypes.Name, username),
          new Claim(ClaimTypes.Role, roles)
        }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials =
          new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)

      };

      var token = jwtToken.CreateToken(tokenDiscriptor);

      return jwtToken.WriteToken(token);
    }


    
  }
}
