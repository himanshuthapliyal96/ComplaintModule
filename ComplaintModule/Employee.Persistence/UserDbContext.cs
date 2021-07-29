using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using User.Core.Entity;

namespace Employee.Persistence
{
  public class UserDbContext:DbContext
  {
    private IConfiguration _configuration;
    public UserDbContext(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseMySQL(_configuration.GetSection("Connection").Value);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<UserEntity>(entity =>
      {
        entity.HasKey(e => e.ID);
      });

      modelBuilder.Entity<ComplaintEntity>(entity =>
      {
        entity.HasKey(e=>e.ComplaintId);
        entity.Property(e => e.ReporterId).IsRequired();
      });

      modelBuilder.Entity<SecurityEntity>(entity =>
      {
        entity.HasKey(e => e.ID);
        entity.Property(e => e.Password).IsRequired();
        entity.Property(e => e.Password).IsRequired();
      });
    }

    public DbSet<UserEntity> Employees { get; set; }

    public DbSet<ComplaintEntity> Complaints { get; set; }
    public DbSet<SecurityEntity> Security { get; set; }
  }
}
