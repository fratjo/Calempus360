using Calempus360.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Infrastructure.Data
{
    internal class UniversityConfiguration : IEntityTypeConfiguration<University>
    {
        public void Configure(EntityTypeBuilder<University> builder)
        {
            builder.HasKey(u => u.University_Id);
            builder.Property(u => u.University_Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Code).IsRequired();
            builder.Property(u => u.Phone).IsRequired();
            builder.Property(u => u.Address).IsRequired();
            builder.Property(u => u.CreatedAt).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(u => u.UpdatedAt).IsRequired().HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
        }
    }
}
