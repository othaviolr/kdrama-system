using KDramaSystem.Domain.Enums;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.ListaPrateleira.Obter;

public class ObterListaPrateleiraUseCase
{
    private readonly IListaPrateleiraRepository _repository;

    public ObterListaPrateleiraUseCase(IListaPrateleiraRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Domain.Entities.ListaPrateleira>> ExecuteAsync(ObterListaPrateleiraRequest request, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(request.ShareToken))
        {
            var lista = await _repository.ObterPorTokenAsync(request.ShareToken, cancellationToken);
            if (lista == null)
                throw new Exception("Lista não encontrada ou não compartilhada.");

            return new[] { lista };
        }

        if (request.ListaId.HasValue)
        {
            var lista = await _repository.ObterPorIdAsync(request.ListaId.Value, cancellationToken);
            if (lista == null)
                throw new Exception("Lista não encontrada.");

            if (request.UsuarioLogadoId.HasValue
                && lista.UsuarioId != request.UsuarioLogadoId.Value
                && lista.Privacidade != ListaPrivacidade.Publico)
            {
                throw new Exception("Usuário não autorizado a acessar essa lista.");
            }

            return new[] { lista };
        }

        if (request.UsuarioId.HasValue)
        {
            var listas = await _repository.ObterPorUsuarioAsync(request.UsuarioId.Value, cancellationToken);
            return listas;
        }

        if (request.UsuarioLogadoId.HasValue && request.ApenasDoUsuario)
        {
            var listas = await _repository.ObterMinhasAsync(request.UsuarioLogadoId.Value, cancellationToken);
            return listas;
        }

        if (request.UsuarioLogadoId.HasValue)
        {
            var listas = await _repository.ObterPublicasAsync(request.UsuarioLogadoId.Value, cancellationToken);
            return listas;
        }

        return Enumerable.Empty<Domain.Entities.ListaPrateleira>();
    }
}