using FluentValidation;
using FluentValidation.Results;
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
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }

        var lista = await _repository.ObterPorIdAsync(request.ListaId, cancellationToken);
        if (lista == null)
            throw new Exception("Lista não encontrada.");

        if (lista.UsuarioId != request.UsuarioId)
            throw new Exception("Usuário não autorizado.");

        await _repository.RemoverAsync(request.ListaId, cancellationToken);
    }
}