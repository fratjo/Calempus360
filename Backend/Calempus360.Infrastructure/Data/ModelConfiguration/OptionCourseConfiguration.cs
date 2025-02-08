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
    internal class OptionCourseConfiguration : IEntityTypeConfiguration<OptionCourse>
    {
        public void Configure(EntityTypeBuilder<OptionCourse> builder)
        {
            builder.HasKey(oc => new {oc.Course_Id, oc.Option_Id});
            builder.Property(oc => oc.OptionGrade).IsRequired();
            builder.HasOne(oc => oc.Option).WithMany(oc => oc.OptionCourses).HasForeignKey(oc => oc.Option_Id);
            builder.HasOne(oc => oc.Course).WithMany(oc => oc.OptionsCourse).HasForeignKey(oc => oc.Course_Id);
        }
    }
}
