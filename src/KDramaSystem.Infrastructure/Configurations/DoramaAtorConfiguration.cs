using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDramaSystem.Infrastructure.Persistence.Configurations;

public class DoramaAtorConfiguration : IEntityTypeConfiguration<DoramaAtor>
{
    public void Configure(EntityTypeBuilder<DoramaAtor> builder)
    {
        builder.HasKey(da => new { da.DoramaId, da.AtorId });

        builder
            .HasOne(da => da.Dorama)
            .WithMany(d => d.Atores)
            .HasForeignKey(da => da.DoramaId);

        builder
            .HasOne(da => da.Ator)
            .WithMany(a => a.Doramas)
            .HasForeignKey(da => da.AtorId);

        builder.ToTable("DoramaAtores");
    }
}