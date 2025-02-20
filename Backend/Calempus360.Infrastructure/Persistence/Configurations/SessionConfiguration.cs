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
    internal class SessionConfiguration : IEntityTypeConfiguration<SessionEntity>
    {
        public void Configure(EntityTypeBuilder<SessionEntity> builder)
        {
            builder.HasKey(s => s.SessionId);
            builder.Property(s => s.SessionId).HasDefaultValueSql("NEWID()");
            
            builder.Property(s => s.DatetimeStart).IsRequired();
            
            builder.Property(s => s.DatetimeEnd).IsRequired();
            
            builder.Property(s => s.ClassroomId).IsRequired();
            
            builder.Property(s => s.CourseId).IsRequired();

            builder.HasIndex(s => new { s.ClassroomId, s.DatetimeStart, s.DatetimeEnd }).IsUnique();
            
            builder.Property(s => s.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();
            
            builder
                .HasOne(s => s.ClassroomEntity)
                .WithMany(s => s.Sessions)
                .HasForeignKey(s => s.ClassroomId);
            
            builder
                .HasOne(s => s.CourseEntity)
                .WithMany(s => s.Sessions)
                .HasForeignKey(s => s.CourseId);
        }
    }
}
