using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Genero.Excluir;

public class ExcluirGeneroUseCase
{
    private readonly IGeneroRepository _generoRepository;

    public ExcluirGeneroUseCase(IGeneroRepository generoRepository)
    {
        _generoRepository = generoRepository;
    }

    public async Task ExecutarAsync(ExcluirGeneroRequest request)
    {
        var genero = await _generoRepository.ObterPorIdAsync(request.Id);

        if (genero == null)
            throw new Exception("Gênero não encontrado.");

        await _generoRepository.RemoverAsync(request.Id);
    }
}