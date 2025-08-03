using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Configurations;

public class DoramaAtorConfiguration : IEntityTypeConfiguration<DoramaAtor>
{
    public void Configure(EntityTypeBuilder<DoramaAtor> builder)
    {
        builder.HasKey(da => new { da.DoramaId, da.AtorId });

        builder.Property(da => da.DoramaId).IsRequired();
        builder.Property(da => da.AtorId).IsRequired();

        builder.HasOne(da => da.Dorama)
            .WithMany(d => d.Atores)
            .HasForeignKey(da => da.DoramaId);

        builder.HasOne(da => da.Ator)
            .WithMany(a => a.Doramas)
            .HasForeignKey(da => da.AtorId);
    }
}