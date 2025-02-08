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
    internal class Site_Academic_YearConfiguration : IEntityTypeConfiguration<Site_Academic_Year>
    {
        public void Configure(EntityTypeBuilder<Site_Academic_Year> builder)
        {
            builder.HasKey(sa => new { sa.Site_Id, sa.University_Id });
            builder.Property(sa => sa.AcademicYear_Id).IsRequired();
            builder.HasOne(sa => sa.Site).WithMany(s => s.Sites_Academic_Year).HasForeignKey(sa => sa.Site_Id);
        }
    }
}
