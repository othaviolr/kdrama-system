using KDramaSystem.Application.DTOs.Genero;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Genero.Obter;

public class ObterGeneroPorIdUseCase
{
    private readonly IGeneroRepository _generoRepository;

    public ObterGeneroPorIdUseCase(IGeneroRepository generoRepository)
    {
        _generoRepository = generoRepository;
    }

    public async Task<ObterGeneroDto?> ExecutarAsync(Guid generoId)
    {
        var genero = await _generoRepository.ObterPorIdAsync(generoId);
        if (genero == null) return null;

        return new ObterGeneroDto
        {
            Id = genero.Id,
            Nome = genero.Nome
        };
    }
}