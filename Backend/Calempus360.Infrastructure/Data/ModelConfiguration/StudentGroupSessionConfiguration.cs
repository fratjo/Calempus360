using Calempus360.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calempus360.Infrastructure.Data.ModelConfiguration;

public class StudentGroupSessionConfiguration: IEntityTypeConfiguration<StudentGroupSession>
{
    public void Configure(EntityTypeBuilder<StudentGroupSession> builder)
    {
        builder.HasKey(sgs => new { sgs.StudentGroupId, sgs.SessionId });
        
        builder.Property(sgs => sgs.StudentGroupId).IsRequired();
        
        builder.Property(sgs => sgs.SessionId).IsRequired();
        
        builder.HasOne(sgs => sgs.StudentGroup)
            .WithMany(sg => sg.StudentGroupSessions)
            .HasForeignKey(sgs => sgs.StudentGroupId);
        
        builder.HasOne(sgs => sgs.Session)
            .WithMany(s => s.StudentGroupSessions)
            .HasForeignKey(sgs => sgs.SessionId);
    }
}