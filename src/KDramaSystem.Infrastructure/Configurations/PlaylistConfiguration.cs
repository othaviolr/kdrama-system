using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDramaSystem.Infrastructure.Configurations;

public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable("Playlists");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .HasMaxLength(36);

        builder.Property(p => p.SpotifyPlaylistId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Url)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.ImagemUrl)
            .HasMaxLength(500);

        builder.Property(p => p.Dono)
            .HasMaxLength(200);

        builder.Property(p => p.TotalMusicas)
            .IsRequired();

        builder.Property(p => p.DoramaId)
            .IsRequired();

        builder.HasOne(p => p.Dorama)
            .WithMany(d => d.Playlists)
            .HasForeignKey(p => p.DoramaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}