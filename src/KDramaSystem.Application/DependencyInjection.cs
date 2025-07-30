using KDramaSystem.Application.Interfaces;
using KDramaSystem.Application.UseCases.Dorama.Criar;
using KDramaSystem.Application.UseCases.Dorama.Excluir;
using KDramaSystem.Application.UseCases.Dorama.Obter;
using KDramaSystem.Application.UseCases.Usuario;
using KDramaSystem.Application.UseCases.Usuario.DeixarDeSeguir;
using KDramaSystem.Application.UseCases.Usuario.Deletar;
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
            services.AddScoped<IDeletarPerfilUseCase, DeletarPerfilUseCase>();
            services.AddScoped<SeguirUsuarioUseCase>();
            services.AddScoped<DeixarDeSeguirUsuarioUseCase>();
            services.AddScoped<CriarDoramaUseCase>();
            services.AddScoped<ExcluirDoramaUseCase>();
            services.AddScoped<ObterDoramaUseCase>();

            return services;
        }
    }
}