using FluentValidation;
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

    public async Task<Domain.Entities.ListaPrateleira> ExecuteAsync(EditarListaPrateleiraRequest request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var lista = await _repository.ObterPorIdAsync(request.ListaId, cancellationToken);
        if (lista == null || lista.UsuarioId != request.UsuarioId)
            throw new Exception("Lista não encontrada ou usuário não autorizado.");

        if (!string.IsNullOrEmpty(request.Nome))
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