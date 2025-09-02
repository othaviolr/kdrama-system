using KDramaSystem.Domain.Interfaces;
using static KDramaSystem.Application.DTOs.Dorama.ObterDoramaDto;

namespace KDramaSystem.Application.UseCases.Genero.Obter;

public class ObterGenerosUseCase
{
    private readonly IGeneroRepository _generoRepository;

    public ObterGenerosUseCase(IGeneroRepository generoRepository)
    {
        _generoRepository = generoRepository;
    }

    public async Task<List<GeneroDto>> ExecutarAsync()
    {
        var generos = await _generoRepository.ObterTodosAsync();
        return generos.Select(g => new GeneroDto
        {
            Id = g.Id,
            Nome = g.Nome
        }).ToList();
    }
}