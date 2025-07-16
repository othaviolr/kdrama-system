using KDramaSystem.Application.UseCases.Usuario.Dtos;

namespace KDramaSystem.Application.UseCases.Usuario.ObterPerfilCompleto
{
    public interface IObterPerfilCompletoUseCase
    {
        Task<PerfilCompletoDto?> ExecutarAsync(ObterPerfilCompletoRequest request);
    }
}