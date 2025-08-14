using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.DoramaLista.Adicionar;

public class AdicionarDoramaListaUseCase
{
    private readonly IDoramaListaRepository _repository;

    public AdicionarDoramaListaUseCase(IDoramaListaRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Guid listaId, Guid doramaId)
    {
        var item = new Domain.Entities.DoramaLista(Guid.NewGuid(), listaId, doramaId);
        await _repository.AdicionarAsync(item);
    }
}