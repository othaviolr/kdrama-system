using KDramaSystem.Application.UseCases.Usuario.Dtos;

namespace KDramaSystem.Application.UseCases.Usuario.ObterPerfilPublico;

public interface IObterPerfilPublicoUseCase
{
    Task<PerfilPublicoDto?> ExecutarAsync(string nomeUsuario, Guid? usuarioLogadoId = null);
}