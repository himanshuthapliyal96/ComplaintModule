using System.Diagnostics.CodeAnalysis;
using Employee.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserApi.Configurations
{

  [ExcludeFromCodeCoverage]
  public static class Bootstrapper
  {
    public static void AddUserDatabase(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<UserDbContext>(optionsBuilder =>
          optionsBuilder.UseMySQL(configuration.GetSection("Connection").Value));
    }
  }
}
