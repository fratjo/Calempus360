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
    internal class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
    {
        public void Configure(EntityTypeBuilder<StudentGroup> builder)
        {
            builder.HasKey(g => g.GroupId);
            builder.Property(g => g.GroupId).ValueGeneratedOnAdd();
            builder.Property(g => g.Code).IsRequired();
            builder.Property(g => g.NumberOfStudents).IsRequired();
            builder.Property(g => g.OptionGrade).IsRequired();
            builder.Property(g => g.AcademicYearId).IsRequired();
            builder.Property(g => g.CreatedAt).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(g => g.UpdatedAt).IsRequired().HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();

            builder
                .HasOne(g => g.Site)
                .WithMany(s => s.StudentGroups)
                .HasForeignKey(g => g.SiteId);
            builder
                .HasOne(g => g.Option)
                .WithMany(o => o.StudentGroups)
                .HasForeignKey(g => g.OptionId);
            builder
                .HasOne(g => g.AcademicYear)
                .WithMany(ay => ay.StudentGroups)
                .HasForeignKey(g => g.AcademicYearId);
        }
    }
}
