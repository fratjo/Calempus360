using Calempus360.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Infrastructure.Data
{
    internal class DayWithoutCourseConfiguration : IEntityTypeConfiguration<DayWithoutCourse>
    {
        public void Configure(EntityTypeBuilder<DayWithoutCourse> builder)
        {
            builder.HasKey(d => d.DayWithoutCourse_Id);
            builder.Property(d => d.Name).IsRequired();
            builder.Property(d => d.Name).IsRequired();
            builder.Property(d => d.CreatedAt).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(d => d.UpdatedAt).IsRequired().HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
            builder.HasOne(a => a.AcademicYear).WithMany(d => d.DaysWithoutCourse).HasForeignKey(d => d.AcademicYear_Id);
        }
    }
    }
}
