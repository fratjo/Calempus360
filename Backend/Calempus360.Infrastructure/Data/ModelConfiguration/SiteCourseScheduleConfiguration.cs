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
    internal class SiteCourseScheduleConfiguration : IEntityTypeConfiguration<SiteCourseSchedule>
    {
        public void Configure(EntityTypeBuilder<SiteCourseSchedule> builder)
        {
            builder.HasKey(scs => new
            {
                scs.SiteId, scs.ScheduleId, scs.AcademicYearId
            });
            
            builder.Property(scs => scs.ScheduleId).IsRequired();
            
            builder.Property(scs => scs.SiteId).IsRequired();
            
            builder.Property(scs => scs.AcademicYearId).IsRequired();
            
            builder
                .HasOne(scs => scs.CourseSchedule)
                .WithMany(scs => scs.SitesCourseSchedules)
                .HasForeignKey(scs => scs.ScheduleId);
            
            builder
                .HasOne(scs => scs.Site)
                .WithMany(scs => scs.SiteCourseSchedules)
                .HasForeignKey(scs => scs.SiteId);
            
            builder
                .HasOne(scs => scs.AcademicYear)
                .WithMany(scs => scs.SiteCourseSchedules)
                .HasForeignKey(scs => scs.AcademicYearId);
        }
    }
}
