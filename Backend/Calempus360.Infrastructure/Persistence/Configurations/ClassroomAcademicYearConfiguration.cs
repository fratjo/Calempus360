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
    internal class ClassroomAcademicYearConfiguration : IEntityTypeConfiguration<ClassroomAcademicYearEntity>
    {
        public void Configure(EntityTypeBuilder<ClassroomAcademicYearEntity> builder)
        {
            builder.HasKey(ca => new { ca.ClassroomId, ca.AcademicYearId});
            
            builder.Property(ca => ca.ClassroomId).IsRequired();
            
            builder.Property(ca => ca.AcademicYearId).IsRequired();
            
            builder
                .HasOne(ca => ca.ClassroomEntity)
                .WithMany(ca => ca.ClassroomAcademicYears)
                .HasForeignKey(ca => ca.ClassroomId);
            
            builder
                .HasOne(ca => ca.AcademicYearEntity)
                .WithMany(ca => ca.ClassroomAcademicYears)
                .HasForeignKey(ca => ca.AcademicYearId);
        }
    }
}
