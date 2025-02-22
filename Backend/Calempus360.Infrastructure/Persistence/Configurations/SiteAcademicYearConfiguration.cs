using Calempus360.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calempus360.Infrastructure.Data.ModelConfiguration
{
    internal class SiteAcademicYearConfiguration : IEntityTypeConfiguration<SiteAcademicYearEntity>
    {
        public void Configure(EntityTypeBuilder<SiteAcademicYearEntity> builder)
        {
            builder.HasKey(sa => new { sa.SiteId, sa.AcademicYearId });
            
            builder.Property(sa => sa.SiteId).IsRequired();
            
            builder.Property(sa => sa.AcademicYearId).IsRequired();
            
            builder
                .HasOne(sa => sa.SiteEntity)
                .WithMany(s => s.SiteAcademicYears)
                .HasForeignKey(sa => sa.SiteId);
            
            builder
                .HasOne(sa => sa.AcademicYearEntity)
                .WithMany(a => a.SiteAcademicYears)
                .HasForeignKey(sa => sa.AcademicYearId);
        }
    }
}
