using System;
using System.Linq;
using ApiServices;
using Employee.Persistence;
using Microsoft.AspNetCore.Mvc;
using User.Core.Entity;

namespace UserApi.Services
{
  public class UserService
  {
    private UserDbContext _context;

    public UserService(UserDbContext context)
    {
      _context = context;
    }


    public UserEntity GetUserEntity(string userId)
    {
      return _context.Employees.FirstOrDefault(emp => emp.ID.ToString() == userId);
    }

    public object GetAdminUsers()
    {
      try
      {
        var data = _context.Security.Where(sec => sec.Role == Roles.Admin)
          .Join(
            _context.Employees,
            security => security.ID,
            employee => employee.ID,
            (security, employee) => new
            {
              Department = employee.DEPARTMENT,
              Name = employee.NAME,
              Id = employee.ID
            }
          ).ToList();
        return data;
      }
      catch (Exception ex)
      {
        throw new Exception($"Unable to get all admin users, Reason: {ex.Message}.");
      }
    
    }

     
  }
}
