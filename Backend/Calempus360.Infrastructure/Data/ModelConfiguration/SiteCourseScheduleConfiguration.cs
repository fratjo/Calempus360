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
    internal class SiteCourseScheduleConfiguration : IEntityTypeConfiguration<SiteCourseSchedule>
    {
        public void Configure(EntityTypeBuilder<SiteCourseSchedule> builder)
        {
            builder.HasKey(scs => new { scs.Site_Id, scs.Schedule_Id });
            builder.Property(scs => scs.AcademicYear_Id).IsRequired();
            builder.HasOne(scs => scs.Schedule).WithMany(scs => scs.Sites).HasForeignKey(scs => scs.Schedule_Id);
            builder.HasOne(scs => scs.Site).WithMany(scs => scs.Schedules).HasForeignKey(scs => scs.Site_Id);
        }
    }
}
