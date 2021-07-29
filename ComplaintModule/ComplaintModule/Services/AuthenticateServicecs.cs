using System.Linq;
using Employee.Persistence;
using User.Core.Entity;

namespace UserApi.Services
{
  public class AuthenticateServicecs
  {
    private readonly UserDbContext _context;

    public AuthenticateServicecs(UserDbContext context)
    {
      _context = context;
    }

    public bool AuthenticateUser(string userId, string password,out SecurityEntity user)
    {
      var userEntities = _context.Security.FirstOrDefault(employee => employee.ID.ToString() == userId && employee.Password == password);
      if (userEntities ==null)
      {
        user = null;
        return false;
      }

      user = userEntities;
      return true;

    }
  }
}
