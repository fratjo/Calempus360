using Calempus360.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calempus360.Infrastructure.Persistence.Configurations
{
    internal class UniversityConfiguration : IEntityTypeConfiguration<UniversityEntity>
    {
        public void Configure(EntityTypeBuilder<UniversityEntity> builder)
        {
            builder.HasKey(u => u.UniversityId);
            builder.Property(u => u.UniversityId).HasDefaultValueSql("NEWID()");
            
            builder.Property(u => u.Name).IsRequired();
            builder.HasIndex(u => u.Name).IsUnique();
            
            builder.Property(u => u.Code).IsRequired();
            builder.HasIndex(u => u.Code).IsUnique();
            
            builder.Property(u => u.Phone).IsRequired();
            builder.HasIndex(u => u.Phone).IsUnique();
            
            builder.Property(u => u.Address).IsRequired();
            
            builder.Property(u => u.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            
            builder.Property(u => u.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
