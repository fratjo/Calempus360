using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Data.ModelConfiguration
{
    internal class ClassroomConfiguration : IEntityTypeConfiguration<ClassroomEntity>
    {
        public void Configure(EntityTypeBuilder<ClassroomEntity> builder)
        {
            builder.HasKey(c => c.ClassroomId);
            builder.Property(c => c.ClassroomId).HasDefaultValueSql("NEWID()");
            
            builder.Property(c => c.Name).IsRequired();
            
            builder.Property(c => c.Code).IsRequired();
            builder.HasIndex(c => c.Code).IsUnique();
            
            builder.Property(c => c.Capacity).IsRequired();
            
            builder.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            
            builder
                .HasOne(c => c.SiteEntity)
                .WithMany(c => c.Classrooms)
                .HasForeignKey(c => c.SiteId);
        }
    }
}
