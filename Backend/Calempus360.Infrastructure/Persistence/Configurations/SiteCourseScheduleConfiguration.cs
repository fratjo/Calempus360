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
    internal class SiteCourseScheduleConfiguration : IEntityTypeConfiguration<SiteCourseScheduleEntity>
    {
        public void Configure(EntityTypeBuilder<SiteCourseScheduleEntity> builder)
        {
            builder.HasKey(scs => new
            {
                scs.SiteId, scs.ScheduleId, scs.AcademicYearId
            });
            
            builder.Property(scs => scs.ScheduleId).IsRequired();
            
            builder.Property(scs => scs.SiteId).IsRequired();
            
            builder.Property(scs => scs.AcademicYearId).IsRequired();
            
            builder
                .HasOne(scs => scs.CourseScheduleEntity)
                .WithMany(scs => scs.SitesCourseSchedules)
                .HasForeignKey(scs => scs.ScheduleId);
            
            builder
                .HasOne(scs => scs.SiteEntity)
                .WithMany(scs => scs.SiteCourseSchedules)
                .HasForeignKey(scs => scs.SiteId);
            
            builder
                .HasOne(scs => scs.AcademicYearEntity)
                .WithMany(scs => scs.SiteCourseSchedules)
                .HasForeignKey(scs => scs.AcademicYearId);
        }
    }
}
