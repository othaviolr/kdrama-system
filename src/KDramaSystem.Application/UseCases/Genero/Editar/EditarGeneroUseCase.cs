using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Genero.Editar;

public class EditarGeneroUseCase
{
    private readonly IGeneroRepository _generoRepository;

    public EditarGeneroUseCase(IGeneroRepository generoRepository)
    {
        _generoRepository = generoRepository;
    }

    public async Task ExecutarAsync(EditarGeneroRequest request)
    {
        var genero = await _generoRepository.ObterPorIdAsync(request.Id);
        if (genero == null)
            throw new Exception("Gênero não encontrado.");

        var nomeExiste = await _generoRepository.ExisteComNomeAsync(request.Nome);
        if (nomeExiste && !string.Equals(genero.Nome, request.Nome, StringComparison.OrdinalIgnoreCase))
            throw new Exception("Já existe outro gênero com esse nome.");

        genero.AtualizarNome(request.Nome);

        await _generoRepository.AtualizarAsync(genero);
    }
}