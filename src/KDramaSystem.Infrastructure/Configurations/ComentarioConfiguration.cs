using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDramaSystem.Infrastructure.Configurations
{
    public class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.UsuarioId).IsRequired();

            builder.Property(c => c.AvaliacaoId).IsRequired(false);
            builder.Property(c => c.TemporadaId).IsRequired(false);

            builder.Property(c => c.Data).IsRequired();

            builder.OwnsOne(c => c.Texto, texto =>
            {
                texto.Property(t => t.Texto)
                     .HasColumnName("Texto")
                     .IsRequired()
                     .HasMaxLength(1000);
            });
        }
    }
}