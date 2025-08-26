using KDramaSystem.Application.UseCases.ListaPrateleira.Obter;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.ListaPrateleira.Compartilhar;

public class CompartilharListaPrateleiraRequest
{
    public Guid ListaId { get; set; }
    public Guid UsuarioId { get; set; }
}

public class CompartilharListaPrateleiraUseCase
{
    private readonly IListaPrateleiraRepository _repository;
    private readonly ObterListaPrateleiraUseCase _obterUseCase;

    public CompartilharListaPrateleiraUseCase(IListaPrateleiraRepository repository, ObterListaPrateleiraUseCase obterUseCase)
    {
        _repository = repository;
        _obterUseCase = obterUseCase;
    }

    public async Task<string> ExecuteAsync(CompartilharListaPrateleiraRequest request, CancellationToken cancellationToken = default)
    {
        var lista = (await _obterUseCase.ExecuteAsync(new ObterListaPrateleiraRequest
        {
            ListaId = request.ListaId,
            UsuarioLogadoId = request.UsuarioId
        })).FirstOrDefault();

        if (lista == null)
            throw new Exception("Lista não encontrada.");

        lista.GerarShareToken();
        await _repository.AtualizarAsync(lista, cancellationToken);

        return lista.ShareToken!;
    }
}