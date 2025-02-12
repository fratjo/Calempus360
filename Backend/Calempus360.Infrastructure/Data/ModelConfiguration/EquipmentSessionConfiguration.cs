using Calempus360.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calempus360.Infrastructure.Data.ModelConfiguration;

internal class EquipmentSessionConfiguration: IEntityTypeConfiguration<EquipmentSession>
{
    public void Configure(EntityTypeBuilder<EquipmentSession> builder)
    {
        builder.HasKey(es => new { es.EquipmentId, es.SessionId });
        builder.HasOne(es => es.Equipment)
            .WithMany(e => e.EquipmentSessions)
            .HasForeignKey(es => es.EquipmentId);
        builder.HasOne(es => es.Session)
            .WithMany(s => s.EquipmentSessions)
            .HasForeignKey(es => es.SessionId);
    }
}