using FluentValidation;
using KDramaSystem.Application.Interfaces.Services;
using KDramaSystem.Application.Services;
using KDramaSystem.Application.UseCases.Ator.Criar;
using KDramaSystem.Application.UseCases.Ator.Editar;
using KDramaSystem.Application.UseCases.Ator.Excluir;
using KDramaSystem.Application.UseCases.Ator.Obter;
using KDramaSystem.Application.UseCases.Avaliacao.Criar;
using KDramaSystem.Application.UseCases.Avaliacao.Editar;
using KDramaSystem.Application.UseCases.Avaliacao.Excluir;
using KDramaSystem.Application.UseCases.Avaliacao.Obter;
using KDramaSystem.Application.UseCases.Dorama;
using KDramaSystem.Application.UseCases.Dorama.Excluir;
using KDramaSystem.Application.UseCases.Dorama.Obter;
using KDramaSystem.Application.UseCases.DoramaLista.Adicionar;
using KDramaSystem.Application.UseCases.DoramaLista.Remover;
using KDramaSystem.Application.UseCases.Episodio.Criar;
using KDramaSystem.Application.UseCases.Episodio.Editar;
using KDramaSystem.Application.UseCases.Episodio.Excluir;
using KDramaSystem.Application.UseCases.Episodio.Obter;
using KDramaSystem.Application.UseCases.Genero.Criar;
using KDramaSystem.Application.UseCases.Genero.Editar;
using KDramaSystem.Application.UseCases.Genero.Excluir;
using KDramaSystem.Application.UseCases.Genero.Obter;
using KDramaSystem.Application.UseCases.ListaPrateleira.Criar;
using KDramaSystem.Application.UseCases.ListaPrateleira.Editar;
using KDramaSystem.Application.UseCases.ListaPrateleira.Excluir;
using KDramaSystem.Application.UseCases.ListaPrateleira.Obter;
using KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarProgresso;
using KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarStatus;
using KDramaSystem.Application.UseCases.ProgressoTemporada.ExcluirProgresso;
using KDramaSystem.Application.UseCases.Temporada.Criar;
using KDramaSystem.Application.UseCases.Temporada.Editar;
using KDramaSystem.Application.UseCases.Temporada.Excluir;
using KDramaSystem.Application.UseCases.Temporada.Obter;
using KDramaSystem.Application.UseCases.Usuario;
using KDramaSystem.Application.UseCases.Usuario.DeixarDeSeguir;
using KDramaSystem.Application.UseCases.Usuario.Deletar;
using KDramaSystem.Application.UseCases.Usuario.Editar;
using KDramaSystem.Application.UseCases.Usuario.Login;
using KDramaSystem.Application.UseCases.Usuario.ObterPerfilCompleto;
using KDramaSystem.Application.UseCases.Usuario.ObterPerfilPublico;
using KDramaSystem.Application.UseCases.Usuario.Registrar;
using Microsoft.Extensions.DependencyInjection;

namespace KDramaSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Usuario
        services.AddScoped<RegistrarUsuarioHandler>();
        services.AddScoped<LoginUsuarioHandler>();
        services.AddScoped<SeguirUsuarioUseCase>();
        services.AddScoped<DeixarDeSeguirUsuarioUseCase>();
        services.AddScoped<IObterPerfilPublicoUseCase, ObterPerfilPublicoUseCase>();
        services.AddScoped<IObterPerfilCompletoUseCase, ObterPerfilCompletoUseCase>();
        services.AddScoped<IEditarPerfilUseCase, EditarPerfilUseCase>();
        services.AddScoped<IDeletarPerfilUseCase, DeletarPerfilUseCase>();

        // Ator
        services.AddScoped<CriarAtorUseCase>();
        services.AddScoped<EditarAtorUseCase>();
        services.AddScoped<ExcluirAtorUseCase>();
        services.AddScoped<ObterAtorUseCase>();

        // Dorama
        services.AddScoped<CriarDoramaUseCase>();
        services.AddScoped<EditarDoramaUseCase>();
        services.AddScoped<ExcluirDoramaUseCase>();
        services.AddScoped<ObterDoramaUseCase>();
        services.AddScoped<ObterDoramaCompletoUseCase>();

        // Episodio
        services.AddScoped<CriarEpisodioUseCase>();
        services.AddScoped<EditarEpisodioUseCase>();
        services.AddScoped<ExcluirEpisodioUseCase>();
        services.AddScoped<ObterEpisodioPorIdUseCase>();

        // Genero
        services.AddScoped<CriarGeneroUseCase>();
        services.AddScoped<EditarGeneroUseCase>();
        services.AddScoped<ExcluirGeneroUseCase>();
        services.AddScoped<ObterGeneroPorIdUseCase>();

        // Temporada
        services.AddScoped<CriarTemporadaUseCase>();
        services.AddScoped<EditarTemporadaUseCase>();
        services.AddScoped<ExcluirTemporadaUseCase>();
        services.AddScoped<ObterTemporadaPorIdUseCase>();

        // ProgressoTemporada
        services.AddScoped<AtualizarProgressoTemporadaUseCase>();
        services.AddScoped<AtualizarStatusTemporadaUseCase>();
        services.AddScoped<ExcluirProgressoTemporadaUseCase>();

        // Avaliacao
        services.AddScoped<CriarAvaliacaoUseCase>();
        services.AddScoped<EditarAvaliacaoUseCase>();
        services.AddScoped<ExcluirAvaliacaoUseCase>();
        services.AddScoped<ObterAvaliacaoUseCase>();

        // Lista Prateleira
        services.AddScoped<CriarListaPrateleiraUseCase>();
        services.AddScoped<EditarListaPrateleiraUseCase>();
        services.AddScoped<ExcluirListaPrateleiraUseCase>();
        services.AddScoped<ObterListaPrateleiraUseCase>();

        // Dorama Lista
        services.AddScoped<AdicionarDoramaListaUseCase>();
        services.AddScoped<RemoverDoramaListaUseCase>();

        // Atividade
        services.AddScoped<IAtividadeService, AtividadeService>();

        services.AddValidatorsFromAssemblyContaining<CriarAtorValidator>();

        return services;
    }
}