using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Data.ModelConfiguration
{
    internal class SiteConfiguration : IEntityTypeConfiguration<SiteEntity>
    {
        public void Configure(EntityTypeBuilder<SiteEntity> builder)
        {
            builder.HasKey(u => u.SiteId);
            builder.Property(u => u.SiteId).HasDefaultValueSql("NEWID()");
            
            builder.Property(u => u.Name).IsRequired();
            
            builder.Property(u => u.Code).IsRequired();
            builder.HasIndex(u => u.Code).IsUnique();
            
            builder.Property(u => u.Phone).IsRequired();
            
            builder.Property(u => u.Address).IsRequired();
            
            builder.Property(u => u.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            
            builder.Property(u => u.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            
            builder.Property(u => u.UniversityId).IsRequired();
            
            builder
                .HasOne(s => s.UniversityEntity)
                .WithMany(u => u.Sites)
                .HasForeignKey(s => s.UniversityId).OnDelete(DeleteBehavior.ClientCascade);}
    }
}
