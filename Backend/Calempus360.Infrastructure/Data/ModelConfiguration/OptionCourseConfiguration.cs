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
    internal class OptionCourseConfiguration : IEntityTypeConfiguration<OptionCourse>
    {
        public void Configure(EntityTypeBuilder<OptionCourse> builder)
        {
            builder.HasKey(oc => new
            {
                oc.CourseId, oc.OptionId, oc.AcademicYearId
            });
            builder.Property(oc => oc.OptionGrade).IsRequired();
            builder
                .HasOne(oc => oc.Option)
                .WithMany(oc => oc.OptionCourses)
                .HasForeignKey(oc => oc.OptionId);
            builder
                .HasOne(oc => oc.Course)
                .WithMany(oc => oc.OptionsCourses)
                .HasForeignKey(oc => oc.CourseId);
            builder
                .HasOne(oc => oc.AcademicYear)
                .WithMany(oc => oc.OptionCourses)
                .HasForeignKey(oc => oc.AcademicYearId);
        }
    }
}
