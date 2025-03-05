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
    internal class OptionCourseConfiguration : IEntityTypeConfiguration<OptionCourseEntity>
    {
        public void Configure(EntityTypeBuilder<OptionCourseEntity> builder)
        {
            builder.HasKey(oc => new
            {
                oc.CourseId, oc.OptionId, oc.AcademicYearId
            });
            
            builder.Property(oc => oc.CourseId).IsRequired();
            
            builder.Property(oc => oc.OptionId).IsRequired();
            
            builder.Property(oc => oc.AcademicYearId).IsRequired();
            
            builder.Property(oc => oc.OptionGrade).IsRequired();
            
            builder
                .HasOne(oc => oc.OptionEntity)
                .WithMany(oc => oc.OptionCourses)
                .HasForeignKey(oc => oc.OptionId).OnDelete(DeleteBehavior.Cascade);
            
            builder
                .HasOne(oc => oc.CourseEntity)
                .WithMany(oc => oc.OptionsCourses)
                .HasForeignKey(oc => oc.CourseId).OnDelete(DeleteBehavior.Cascade);
            
            builder
                .HasOne(oc => oc.AcademicYearEntity)
                .WithMany(oc => oc.OptionCourses)
                .HasForeignKey(oc => oc.AcademicYearId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
