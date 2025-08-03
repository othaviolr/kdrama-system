using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Configurations;

public class EpisodioConfiguration : IEntityTypeConfiguration<Episodio>
{
    public void Configure(EntityTypeBuilder<Episodio> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.TemporadaId).IsRequired();

        builder.Property(e => e.Numero).IsRequired();

        builder.Property(e => e.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Sinopse)
            .HasMaxLength(1000);

        builder.Property(e => e.DuracaoMinutos).IsRequired();

        builder.Property(e => e.Tipo).IsRequired();
    }
}