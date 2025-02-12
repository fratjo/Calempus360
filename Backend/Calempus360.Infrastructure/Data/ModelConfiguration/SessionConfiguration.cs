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
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(s => s.SessionId);
            builder.Property(s => s.SessionId).ValueGeneratedOnAdd();
            builder.Property(s => s.DatetimeStart).IsRequired();
            builder.Property(s => s.DatetimeEnd).IsRequired();
            builder
                .HasOne(s => s.Classroom)
                .WithMany(s => s.Sessions)
                .HasForeignKey(s => s.ClassroomId);
            builder
                .HasOne(s => s.Course)
                .WithMany(s => s.Sessions)
                .HasForeignKey(s => s.CourseId);
        }
    }
}
