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
    internal class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.HasKey(o => o.Option_Id);
            builder.Property(o => o.Option_Id).ValueGeneratedOnAdd();
            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.Code).IsRequired();
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.CreatedAt).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(o => o.UpdatedAt).IsRequired().HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
        }
    }
}
