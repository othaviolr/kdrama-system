using KDramaSystem.Application.Interfaces;
using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Domain.Interfaces.Services;
using KDramaSystem.Infrastructure.Repositories;
using KDramaSystem.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KDramaSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            services.AddSingleton<IUsuarioAutenticacaoRepository, UsuarioAutenticacaoRepository>();

            services.AddScoped<ICriptografiaService, CriptografiaService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUsuarioAutenticadoProvider, UsuarioAutenticadoProvider>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}