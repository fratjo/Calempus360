using Calempus360.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Infrastructure.Data.ModelConfiguration
{
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Course_Id);
            builder.Property(c => c.Course_Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Code).IsRequired();
            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.TotalHours).IsRequired();
            builder.Property(c => c.WeeklyHours).IsRequired();
            builder.Property(c => c.Semester).IsRequired();
            builder.Property(c => c.Credits).IsRequired();
            builder.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(c => c.UpdatedAt).IsRequired().HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
        }
    }
}
