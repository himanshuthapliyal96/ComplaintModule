using System;
using ApiServices;
using ApiServices.jwtManager;
using Employee.Persistence;
using Microsoft.AspNetCore.Mvc;
using User.Core.Entity;
using UserApi.Services;

namespace UserApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LogInController : ControllerBase
  {

    private readonly IJwtTokenManager _tokenManager;
    private readonly UserService _userService;
    private readonly AuthenticateServicecs _authService;
    
    public LogInController(IJwtTokenManager tokenmanager,UserDbContext context )
    {
      _tokenManager = tokenmanager;
      _userService = new UserService(context);
      _authService = new AuthenticateServicecs(context);
    }

    /// <summary>
    /// Call for login to system
    /// </summary>
    /// <returns>Token and user logged in</returns>
    [HttpPost("login")]
    public IActionResult LogIn()
    {
      var re =  Request;
      var headers = re.Headers;
      string userId =string.Empty, password = string.Empty;
      if (headers.ContainsKey("UserId"))
      {
        userId = headers["UserId"];
      }

      if (headers.ContainsKey("password"))
      {
        password = headers["password"];
      }

      if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
      {
        return Ok(new Exception($"'username' or 'password' are missing in header."));
      }

      if (!_authService.AuthenticateUser(userId,password,out SecurityEntity user))
      {
        return Ok(new Exception("Invalid username/password."));
      }

      var token = _tokenManager.GenerateToken(userId,user.Role);

      return Ok(ApiResponse.GenerateResponse(token, _userService.GetUserEntity(userId)));
    }
  }
}
