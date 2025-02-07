using Calempus360_api.Models;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<University> Universities { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UniversityConfiguration());
    }
}