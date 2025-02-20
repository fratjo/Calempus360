using Calempus360.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calempus360.Infrastructure.Data.ModelConfiguration;

internal class EquipmentSessionConfiguration: IEntityTypeConfiguration<EquipmentSessionEntity>
{
    public void Configure(EntityTypeBuilder<EquipmentSessionEntity> builder)
    {
        builder.HasKey(es => new { es.EquipmentId, es.SessionId });
        
        builder.Property(es => es.EquipmentId).IsRequired();
        
        builder.Property(es => es.SessionId).IsRequired();
        
        builder.HasOne(es => es.EquipmentEntity)
            .WithMany(e => e.EquipmentSessions)
            .HasForeignKey(es => es.EquipmentId);
        
        builder.HasOne(es => es.SessionEntity)
            .WithMany(s => s.EquipmentSessions)
            .HasForeignKey(es => es.SessionId);
    }
}