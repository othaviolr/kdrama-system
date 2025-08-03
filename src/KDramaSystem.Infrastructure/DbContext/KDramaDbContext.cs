using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Persistence;

public class KDramaDbContext : DbContext
{
    public KDramaDbContext(DbContextOptions<KDramaDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Dorama> Doramas { get; set; }
    public DbSet<Temporada> Temporadas { get; set; }
    public DbSet<Episodio> Episodios { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<Ator> Atores { get; set; }
    public DbSet<DoramaAtor> DoramaAtores { get; set; }
    public DbSet<UsuarioRelacionamento> UsuarioRelacionamentos { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<ListaPrateleira> ListasPrateleira { get; set; }
    public DbSet<ProgressoTemporada> ProgressoTemporadas { get; set; }
    public DbSet<Atividade> Atividades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KDramaDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}