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
            builder.HasKey(ca => new { ca.ClassroomId, ca.AcademicYearId});
            builder.Property(ca => ca.AcademicYearId).IsRequired();
            builder
                .HasOne(ca => ca.Classroom)
                .WithMany(ca => ca.ClassroomAcademicYears)
                .HasForeignKey(ca => ca.ClassroomId);
            builder
                .HasOne(ca => ca.AcademicYear)
                .WithMany(ca => ca.ClassroomAcademicYears)
                .HasForeignKey(ca => ca.AcademicYearId);
        }
    }
}
