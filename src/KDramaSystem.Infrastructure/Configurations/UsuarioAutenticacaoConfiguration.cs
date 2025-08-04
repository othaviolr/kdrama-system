using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KDramaSystem.Domain.Entities;

public class UsuarioAutenticacaoConfiguration : IEntityTypeConfiguration<UsuarioAutenticacao>
{
    public void Configure(EntityTypeBuilder<UsuarioAutenticacao> builder)
    {
        builder.ToTable("UsuariosAutenticacao");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
               .IsRequired();

        builder.Property(u => u.SenhaHash)
               .IsRequired();

        builder.Property(u => u.UsuarioId)
               .IsRequired();
    }
}