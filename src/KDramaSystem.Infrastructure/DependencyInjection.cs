using KDramaSystem.Application.Interfaces;
using KDramaSystem.Application.Interfaces.Repositories;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Domain.Interfaces.Services;
using KDramaSystem.Infrastructure.Persistence;
using KDramaSystem.Infrastructure.Repositories;
using KDramaSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KDramaSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var solutionRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".."));
            var dbPath = @"C:\Users\othav\source\repos\KDramaSystem\KDramaSystem\src\KDramaSystem.Infrastructure\kdrama.db";

            services.AddDbContext<KDramaDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAtorRepository, AtorRepository>();
            services.AddScoped<ITemporadaRepository, TemporadaRepository>();
            services.AddScoped<IEpisodioRepository, EpisodioRepository>();
            services.AddScoped<IUsuarioAutenticacaoRepository, UsuarioAutenticacaoRepository>();
            services.AddScoped<IUsuarioRelacionamentoRepository, UsuarioRelacionamentoRepository>();
            services.AddScoped<IDoramaRepository, DoramaRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();

            services.AddScoped<ICriptografiaService, CriptografiaService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUsuarioAutenticadoProvider, UsuarioAutenticadoProvider>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}