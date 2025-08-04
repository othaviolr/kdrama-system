using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDramaSystem.Infrastructure.Configurations;

public class UsuarioRelacionamentoConfiguration : IEntityTypeConfiguration<UsuarioRelacionamento>
{
    public void Configure(EntityTypeBuilder<UsuarioRelacionamento> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SeguidorId).IsRequired();
        builder.Property(x => x.SeguindoId).IsRequired();
        builder.Property(x => x.Data).IsRequired();

        builder.HasOne(x => x.Seguidor)
            .WithMany(x => x.Seguindo)
            .HasForeignKey(x => x.SeguidorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Seguindo)
            .WithMany(x => x.Seguidores)
            .HasForeignKey(x => x.SeguindoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.SeguidorId, x.SeguindoId }).IsUnique();
    }
}