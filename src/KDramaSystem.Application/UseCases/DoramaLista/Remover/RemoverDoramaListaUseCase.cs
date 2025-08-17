using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.DoramaLista.Remover;

public class RemoverDoramaListaUseCase
{
    private readonly IDoramaListaRepository _repository;

    public RemoverDoramaListaUseCase(IDoramaListaRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Guid listaId, Guid doramaId)
    {
        if (listaId == Guid.Empty)
            throw new ArgumentException("Id da lista inválido.", nameof(listaId));

        if (doramaId == Guid.Empty)
            throw new ArgumentException("Id do dorama inválido.", nameof(doramaId));

        await _repository.RemoverAsync(listaId, doramaId);
    }
}