using Calempus360.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calempus360.Infrastructure.Data.ModelConfiguration
{
    internal class SiteAcademicYearConfiguration : IEntityTypeConfiguration<SiteAcademicYear>
    {
        public void Configure(EntityTypeBuilder<SiteAcademicYear> builder)
        {
            builder.HasKey(sa => new { sa.SiteId, sa.AcademicYearId });
            builder.Property(sa => sa.AcademicYearId).IsRequired();
            builder
                .HasOne(sa => sa.Site)
                .WithMany(s => s.SiteAcademicYears)
                .HasForeignKey(sa => sa.SiteId);
            builder
                .HasOne(sa => sa.AcademicYear)
                .WithMany(a => a.SiteAcademicYears)
                .HasForeignKey(sa => sa.AcademicYearId);
        }
    }
}
