using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDramaSystem.Infrastructure.Mappings
{
    public class AvaliacaoConfiguration : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.UsuarioId).IsRequired();
            builder.Property(a => a.TemporadaId).IsRequired();
            builder.OwnsOne(a => a.Nota, nota =>
            {
                nota.Property(n => n.Valor).HasColumnName("Nota").IsRequired();
            });

            builder.OwnsOne(a => a.Comentario, comentario =>
            {
                comentario.Property(c => c.Texto)
                    .HasColumnName("Comentario")
                    .HasMaxLength(1000);
            });

            builder.Property(a => a.RecomendadoPorUsuarioId);
            builder.Property(a => a.RecomendadoPorNomeLivre).HasMaxLength(200);
            builder.Property(a => a.DataAvaliacao).IsRequired();
        }
    }
}