using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Genero.Criar;

public class CriarGeneroUseCase
{
    private readonly IGeneroRepository _generoRepository;

    public CriarGeneroUseCase(IGeneroRepository generoRepository)
    {
        _generoRepository = generoRepository;
    }

    public async Task<Guid> ExecutarAsync(CriarGeneroRequest request)
    {
        var nomeExiste = await _generoRepository.ExisteComNomeAsync(request.Nome);
        if (nomeExiste)
            throw new Exception("Já existe um gênero com esse nome.");

        var genero = new KDramaSystem.Domain.Entities.Genero(Guid.NewGuid(), request.Nome);

        await _generoRepository.AdicionarAsync(genero);

        return genero.Id;
    }
}