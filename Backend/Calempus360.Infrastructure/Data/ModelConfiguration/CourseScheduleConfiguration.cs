using Calempus360.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Infrastructure.Data.ModelConfiguration
{
    internal class CourseScheduleConfiguration : IEntityTypeConfiguration<CourseSchedule>
    {
        public void Configure(EntityTypeBuilder<CourseSchedule> builder)
        {
            builder.HasKey(cs => cs.Schedule_Id);
            builder.Property(cs => cs.Schedule_Id).ValueGeneratedOnAdd();
            builder.Property(cs => cs.DayOfTheWeek).IsRequired();
            builder.Property(cs => cs.HourStart).IsRequired();
            builder.Property(cs => cs.HourEnd).IsRequired();
            builder.Property(cs => cs.CreatedAt).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(cs => cs.UpdatedAt).IsRequired().HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
        }
    }
}
