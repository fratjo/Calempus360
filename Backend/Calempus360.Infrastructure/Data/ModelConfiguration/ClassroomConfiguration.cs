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
    internal class ClassroomConfiguration : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.HasKey(c => c.ClassroomId);
            builder.Property(c => c.ClassroomId).HasDefaultValueSql("NEWID()");
            
            builder.Property(c => c.Name).IsRequired();
            
            builder.Property(c => c.Code).IsRequired();
            builder.HasIndex(c => c.Code).IsUnique();
            
            builder.Property(c => c.Capacity).IsRequired();
            
            builder.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            
            builder.Property(c => c.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
            
            builder
                .HasOne(c => c.Site)
                .WithMany(c => c.Classrooms)
                .HasForeignKey(c => c.SiteId);
        }
    }
}
