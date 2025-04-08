using Calempus360.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calempus360.Infrastructure.Data.ModelConfiguration;

public class StudentGroupSessionConfiguration: IEntityTypeConfiguration<StudentGroupSessionEntity>
{
    public void Configure(EntityTypeBuilder<StudentGroupSessionEntity> builder)
    {
        builder.HasKey(sgs => new { sgs.StudentGroupId, sgs.SessionId });
        
        builder.Property(sgs => sgs.StudentGroupId).IsRequired();
        
        builder.Property(sgs => sgs.SessionId).IsRequired();
        
        builder.HasOne(sgs => sgs.StudentGroupEntity)
            .WithMany(sg => sg.StudentGroupSessions)
            .HasForeignKey(sgs => sgs.StudentGroupId).OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(sgs => sgs.SessionEntity)
            .WithMany(s => s.StudentGroupSessions)
            .HasForeignKey(sgs => sgs.SessionId).OnDelete(DeleteBehavior.Cascade);
    }
}