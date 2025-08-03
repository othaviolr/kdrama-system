using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Configurations;

public class AtorConfiguration : IEntityTypeConfiguration<Ator>
{
    public void Configure(EntityTypeBuilder<Ator> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(a => a.NomeCompleto)
            .HasMaxLength(250);

        builder.Property(a => a.AnoNascimento);

        builder.Property(a => a.Altura)
            .HasPrecision(5, 2);

        builder.Property(a => a.Pais)
            .HasMaxLength(100);

        builder.Property(a => a.Biografia)
            .HasMaxLength(2000);

        builder.Property(a => a.FotoUrl)
            .HasMaxLength(300);

        builder.Property(a => a.Instagram)
            .HasMaxLength(100);

        builder.Metadata.FindNavigation(nameof(Ator.Doramas))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}