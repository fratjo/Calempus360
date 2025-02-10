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
    internal class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Group_Id);
            builder.Property(g => g.Group_Id).ValueGeneratedOnAdd();
            builder.Property(g => g.Code).IsRequired();
            builder.Property(g => g.NumberOfStudents).IsRequired();
            builder.Property(g => g.OptionGrade).IsRequired();
            builder.Property(g => g.AcademicYear_Id).IsRequired();
            builder.Property(g => g.CreatedAt).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(g => g.UpdatedAt).IsRequired().HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();

            builder.HasOne(g => g.MainSite).WithMany(s => s.Groups).HasForeignKey(g => g.Site_Id);
            builder.HasOne(g => g.Option).WithMany(o => o.Groups).HasForeignKey(g => g.Option_Id);
        }
    }
}
