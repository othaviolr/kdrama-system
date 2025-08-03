using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome).IsRequired();
        builder.Property(u => u.NomeUsuario).IsRequired();
        builder.Property(u => u.Email).IsRequired();
        builder.Property(u => u.FotoUrl);
        builder.Property(u => u.Bio);

        builder.Metadata.FindNavigation(nameof(Usuario.Seguidores))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Usuario.Seguindo))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Usuario.Avaliacoes))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Usuario.Progresso))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Usuario.Comentarios))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Usuario.Listas))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Usuario.Atividades))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}