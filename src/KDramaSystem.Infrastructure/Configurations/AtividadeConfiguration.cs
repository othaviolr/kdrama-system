using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDramaSystem.Infrastructure.Configurations
{
    public class AtividadeConfiguration : IEntityTypeConfiguration<Atividade>
    {
        public void Configure(EntityTypeBuilder<Atividade> builder)
        {
            builder.ToTable("Atividades");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.UsuarioId)
                .IsRequired();

            builder.Property(a => a.ReferenciaId)
                .IsRequired();

            builder.Property(a => a.Data)
                .IsRequired();

            builder.OwnsOne(a => a.Tipo, tipo =>
            {
                tipo.Property(t => t.Valor)
                    .HasColumnName("TipoAtividade")
                    .IsRequired();

                tipo.HasIndex(t => t.Valor);
            });
        }
    }
}