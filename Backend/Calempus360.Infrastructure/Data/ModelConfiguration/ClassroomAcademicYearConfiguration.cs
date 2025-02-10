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
    internal class ClassroomAcademicYearConfiguration : IEntityTypeConfiguration<ClassroomAcademicYear>
    {
        public void Configure(EntityTypeBuilder<ClassroomAcademicYear> builder)
        {
            builder.HasKey(ca => new { ca.Classroom_Id, ca.Site_Id });
            builder.Property(ca => ca.AcademicYear_Id).IsRequired();
            builder.HasOne(ca => ca.Classroom).WithMany(ca => ca.AcademicYears).HasForeignKey(ca => ca.Classroom_Id);
        }
    }
}
