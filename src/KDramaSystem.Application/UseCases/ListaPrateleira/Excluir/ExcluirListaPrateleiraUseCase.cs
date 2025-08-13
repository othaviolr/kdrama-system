using FluentValidation;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.ListaPrateleira.Excluir;

public class ExcluirListaPrateleiraUseCase
{
    private readonly IListaPrateleiraRepository _repository;
    private readonly IValidator<ExcluirListaPrateleiraRequest>? _validator;

    public ExcluirListaPrateleiraUseCase(IListaPrateleiraRepository repository, IValidator<ExcluirListaPrateleiraRequest>? validator = null)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task ExecuteAsync(ExcluirListaPrateleiraRequest request, CancellationToken cancellationToken = default)
    {
        if (_validator != null)
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var lista = await _repository.ObterPorIdAsync(request.ListaId, cancellationToken);
        if (lista == null || lista.UsuarioId != request.UsuarioId)
            throw new Exception("Lista não encontrada ou usuário não autorizado.");

        await _repository.RemoverAsync(request.ListaId, cancellationToken);
    }
}