using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Configurations;

public class DoramaConfiguration : IEntityTypeConfiguration<Dorama>
{
    public void Configure(EntityTypeBuilder<Dorama> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Titulo).IsRequired();
        builder.Property(d => d.PaisOrigem).IsRequired();
        builder.Property(d => d.AnoLancamento).IsRequired();
        builder.Property(d => d.EmExibicao).IsRequired();
        builder.Property(d => d.Plataforma).IsRequired();
        builder.Property(d => d.ImagemCapaUrl).IsRequired();
        builder.Property(d => d.TituloOriginal);
        builder.Property(d => d.Sinopse);

        builder.Property(d => d.UsuarioId).IsRequired();

        builder.Metadata.FindNavigation(nameof(Dorama.Generos))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Dorama.Temporadas))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Dorama.Atores))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}