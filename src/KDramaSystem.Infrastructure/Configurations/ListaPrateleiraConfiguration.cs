using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDramaSystem.Infrastructure.Configurations;

public class ListaPrateleiraConfiguration : IEntityTypeConfiguration<ListaPrateleira>
{
    public void Configure(EntityTypeBuilder<ListaPrateleira> builder)
    {
        builder.ToTable("ListasPrateleiras");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(l => l.Descricao)
            .HasMaxLength(1000);

        builder.Property(l => l.ImagemCapaUrl)
            .HasMaxLength(500);

        builder.Property(l => l.Privacidade)
            .IsRequired();

        builder.Property(l => l.ShareToken)
            .HasMaxLength(100);

        builder.Property(l => l.DataCriacao)
            .IsRequired();

        builder.HasMany(l => l.Doramas)
            .WithOne()
            .HasForeignKey(d => d.ListaPrateleiraId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}