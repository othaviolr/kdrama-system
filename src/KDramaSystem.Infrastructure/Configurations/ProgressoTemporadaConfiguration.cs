using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDramaSystem.Infrastructure.Configurations;

public class ProgressoTemporadaConfiguration : IEntityTypeConfiguration<ProgressoTemporada>
{
    public void Configure(EntityTypeBuilder<ProgressoTemporada> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.UsuarioId).IsRequired();
        builder.Property(p => p.TemporadaId).IsRequired();
        builder.Property(p => p.EpisodiosAssistidos).IsRequired();
        builder.Property(p => p.DataAtualizacao).IsRequired();

        builder.OwnsOne(p => p.Status, status =>
        {
            status.Property(s => s.Valor)
                .HasColumnName("Status")
                .IsRequired();
        });
        builder.HasIndex(p => new { p.UsuarioId, p.TemporadaId }).IsUnique();
    }
}