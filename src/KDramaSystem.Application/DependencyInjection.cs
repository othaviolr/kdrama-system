using KDramaSystem.Application.Interfaces;
using KDramaSystem.Application.UseCases.Usuario.Editar;
using KDramaSystem.Application.UseCases.Usuario.Login;
using KDramaSystem.Application.UseCases.Usuario.ObterPerfilCompleto;
using KDramaSystem.Application.UseCases.Usuario.Registrar;
using Microsoft.Extensions.DependencyInjection;

namespace KDramaSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IObterPerfilCompletoUseCase, ObterPerfilCompletoUseCase>();
            services.AddScoped<IEditarPerfilUseCase, EditarPerfilUseCase>();
            services.AddScoped<RegistrarUsuarioHandler>();
            services.AddScoped<LoginUsuarioHandler>();

            return services;
        }
    }
}