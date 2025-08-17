using FluentValidation;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.ListaPrateleira.Criar;

public class CriarListaPrateleiraUseCase
{
    private readonly IListaPrateleiraRepository _repository;
    private readonly IValidator<CriarListaPrateleiraRequest> _validator;

    public CriarListaPrateleiraUseCase(IListaPrateleiraRepository repository, IValidator<CriarListaPrateleiraRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Domain.Entities.ListaPrateleira> ExecuteAsync(CriarListaPrateleiraRequest request, CancellationToken cancellationToken = default)
    {
        if (request.UsuarioId == Guid.Empty)
            throw new ArgumentException("UsuarioId inválido.", nameof(request.UsuarioId));

        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var lista = new Domain.Entities.ListaPrateleira(
            Guid.NewGuid(),
            request.UsuarioId,
            request.Nome,
            request.Privacidade,
            request.Descricao,
            request.ImagemCapaUrl
        );

        await _repository.AdicionarAsync(lista, cancellationToken);
        return lista;
    }
}