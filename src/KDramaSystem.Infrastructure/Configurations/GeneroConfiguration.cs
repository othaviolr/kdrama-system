using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Configurations;

public class GeneroConfiguration : IEntityTypeConfiguration<Genero>
{
    public void Configure(EntityTypeBuilder<Genero> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Nome)
            .IsRequired()
            .HasMaxLength(100);
    }
}