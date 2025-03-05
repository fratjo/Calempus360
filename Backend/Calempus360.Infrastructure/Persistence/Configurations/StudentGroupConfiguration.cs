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
    internal class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroupEntity>
    {
        public void Configure(EntityTypeBuilder<StudentGroupEntity> builder)
        {
            builder.HasKey(g => g.StudentGroupId);
            builder.Property(g => g.StudentGroupId).HasDefaultValueSql("NEWID()");

            builder.Property(g => g.Code).IsRequired();
            builder.HasIndex(g => g.Code).IsUnique();
            
            builder.Property(g => g.NumberOfStudents).IsRequired();
            
            builder.Property(g => g.OptionGrade).IsRequired();
            
            builder.Property(g => g.AcademicYearId).IsRequired();
            
            builder.Property(g => g.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            
            builder.Property(g => g.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");

            builder
                .HasOne(g => g.SiteEntity)
                .WithMany(s => s.StudentGroups)
                .HasForeignKey(g => g.SiteId).OnDelete(DeleteBehavior.SetNull);
            
            builder
                .HasOne(g => g.OptionEntity)
                .WithMany(o => o.StudentGroups)
                .HasForeignKey(g => g.OptionId).OnDelete(DeleteBehavior.SetNull);
            
            builder
                .HasOne(g => g.AcademicYearEntity)
                .WithMany(ay => ay.StudentGroups)
                .HasForeignKey(g => g.AcademicYearId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
