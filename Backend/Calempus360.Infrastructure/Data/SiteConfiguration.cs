using Calempus360_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Infrastructure.Data
{
    internal class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.HasKey(u => u.Site_Id);
            builder.Property(u => u.Site_Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Code).IsRequired();
            builder.Property(u => u.Phone).IsRequired();
            builder.Property(u => u.Address).IsRequired();
            builder.Property(u => u.CreatedAt).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(u => u.UpdatedAt).IsRequired().HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
            builder.HasOne(a => a.University).WithMany(u => u.Sites).HasForeignKey(z => z.University_Id);
        }
    }
}
