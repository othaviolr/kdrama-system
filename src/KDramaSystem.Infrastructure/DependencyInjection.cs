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
            services.AddDbContext<KDramaDbContext>(options =>
            options.UseSqlite("Data Source=kdrama.db"));

            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            services.AddSingleton<IUsuarioAutenticacaoRepository, UsuarioAutenticacaoRepository>();
            services.AddSingleton<IUsuarioRelacionamentoRepository, UsuarioRelacionamentoRepository>();

            services.AddSingleton<IDoramaRepository, DoramaRepository>();
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