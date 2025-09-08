using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDramaSystem.Infrastructure.Configurations;

public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
{
    public void Configure(EntityTypeBuilder<Badge> builder)
    {
        builder.ToTable("Badges");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Descricao)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(b => b.Raridade)
            .IsRequired();

        builder.Property(b => b.Pontos)
            .IsRequired();

        builder.Property(b => b.Categoria)
            .IsRequired();

        builder.Property(b => b.Condicao)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(b => b.IconeUrl)
            .IsRequired()
            .HasMaxLength(300);
    }
}