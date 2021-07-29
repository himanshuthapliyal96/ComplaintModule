using Employee.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Services;

namespace UserApi.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly UserService _userService;
    public UserController( UserDbContext context)
    {
      _userService = new UserService(context);
    }

    [HttpGet("AllAdmins")]
    public IActionResult GetAllAdminUsers()
    {
      return Ok(_userService.GetAdminUsers());
    }
  }
}
