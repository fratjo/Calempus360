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
    internal class CourseScheduleConfiguration : IEntityTypeConfiguration<CourseScheduleEntity>
    {
        public void Configure(EntityTypeBuilder<CourseScheduleEntity> builder)
        {
            builder.HasKey(cs => cs.ScheduleId);
            builder.Property(cs => cs.ScheduleId).HasDefaultValueSql("NEWID()");

            builder.Property(cs => cs.DayOfTheWeek).IsRequired();

            builder.Property(cs => cs.HourStart).IsRequired().HasColumnType("TIME");
            
            builder.Property(cs => cs.HourEnd).IsRequired().HasColumnType("TIME");
            
            builder.HasIndex(cs => new { cs.DayOfTheWeek, cs.HourStart, cs.HourEnd }).IsUnique();
            
            builder.Property(cs => cs.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            
            builder.Property(cs => cs.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
