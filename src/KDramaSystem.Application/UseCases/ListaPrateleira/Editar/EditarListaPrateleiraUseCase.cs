using FluentValidation;
using FluentValidation.Results;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.ListaPrateleira.Editar;

public class EditarListaPrateleiraUseCase
{
    private readonly IListaPrateleiraRepository _repository;
    private readonly IValidator<EditarListaPrateleiraRequest> _validator;

    public EditarListaPrateleiraUseCase(IListaPrateleiraRepository repository, IValidator<EditarListaPrateleiraRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Domain.Entities.ListaPrateleira> ExecuteAsync(
        EditarListaPrateleiraRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var lista = await _repository.ObterPorIdAsync(request.ListaId, cancellationToken);
        if (lista == null)
            throw new Exception("Lista não encontrada.");

        if (lista.UsuarioId != request.UsuarioId)
            throw new Exception("Usuário não autorizado.");

        if (!string.IsNullOrWhiteSpace(request.Nome))
            lista.AtualizarNome(request.Nome);

        if (request.Descricao != null)
            lista.AtualizarDescricao(request.Descricao);

        if (request.ImagemCapaUrl != null)
            lista.AlterarImagemCapa(request.ImagemCapaUrl);

        if (request.Privacidade.HasValue)
            lista.AlterarPrivacidade(request.Privacidade.Value);

        await _repository.AtualizarAsync(lista, cancellationToken);
        return lista;
    }
}