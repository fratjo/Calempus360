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
    internal class CourseConfiguration : IEntityTypeConfiguration<CourseEntity>
    {
        public void Configure(EntityTypeBuilder<CourseEntity> builder)
        {
            builder.HasKey(c => c.CourseId);
            builder.Property(c => c.CourseId).HasDefaultValueSql("NEWID()");
            
            builder.Property(c => c.Name).IsRequired();
            
            builder.Property(c => c.Code).IsRequired();
            builder.HasIndex(c => c.Code).IsUnique();
            
            builder.Property(c => c.Description).IsRequired();
            
            builder.Property(c => c.TotalHours).IsRequired();
            
            builder.Property(c => c.WeeklyHours).IsRequired();
            
            builder.Property(c => c.Semester).IsRequired();
            
            builder.Property(c => c.Credits).IsRequired();
            
            builder.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            
            builder.Property(c => c.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
