using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDramaSystem.Infrastructure.Configurations;

public class DoramaListaConfiguration : IEntityTypeConfiguration<DoramaLista>
{
    public void Configure(EntityTypeBuilder<DoramaLista> builder)
    {
        builder.ToTable("DoramasListas");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.DoramaId)
            .IsRequired();

        builder.Property(d => d.ListaPrateleiraId)
            .IsRequired();

        builder.Property(d => d.DataAdicao)
            .IsRequired();
    }
}