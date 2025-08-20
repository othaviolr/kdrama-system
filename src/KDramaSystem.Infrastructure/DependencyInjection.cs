using KDramaSystem.Application.Interfaces.Repositories;
using KDramaSystem.Application.Interfaces;
using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Domain.Interfaces.Services;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Infrastructure.Persistence;
using KDramaSystem.Infrastructure.Repositories;
using KDramaSystem.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<KDramaDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("KDramaDb"))
        );

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IAtorRepository, AtorRepository>();
        services.AddScoped<ITemporadaRepository, TemporadaRepository>();
        services.AddScoped<IEpisodioRepository, EpisodioRepository>();
        services.AddScoped<IUsuarioAutenticacaoRepository, UsuarioAutenticacaoRepository>();
        services.AddScoped<IUsuarioRelacionamentoRepository, UsuarioRelacionamentoRepository>();
        services.AddScoped<IDoramaRepository, DoramaRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGeneroRepository, GeneroRepository>();
        services.AddScoped<IProgressoTemporadaRepository, ProgressoTemporadaRepository>();
        services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
        services.AddScoped<IListaPrateleiraRepository, ListaPrateleiraRepository>();
        services.AddScoped<IDoramaListaRepository, DoramaListaRepository>();
        services.AddScoped<IAtividadeRepository, AtividadeRepository>();

        services.AddScoped<ICriptografiaService, CriptografiaService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUsuarioAutenticadoProvider, UsuarioAutenticadoProvider>();
        services.AddHttpContextAccessor();

        return services;
    }
}