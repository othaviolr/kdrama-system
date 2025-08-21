using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Configurations;

public class TemporadaConfiguration : IEntityTypeConfiguration<Temporada>
{
    public void Configure(EntityTypeBuilder<Temporada> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.DoramaId).IsRequired();
        builder.Property(t => t.Numero).IsRequired();
        builder.Property(t => t.AnoLancamento).IsRequired();
        builder.Property(t => t.EmExibicao).IsRequired();
        builder.Property(t => t.Nome).HasMaxLength(200);
        builder.Property(t => t.Sinopse).HasMaxLength(1000);

        builder.Ignore(t => t.NumeroEpisodios);

        builder.HasMany(t => t.Episodios)
               .WithOne(e => e.Temporada)
               .HasForeignKey(e => e.TemporadaId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
               .FindNavigation(nameof(Temporada.Episodios))!
               .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}