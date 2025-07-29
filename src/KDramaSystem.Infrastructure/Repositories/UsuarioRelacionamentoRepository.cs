using KDramaSystem.Application.Interfaces.Repositories;
using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Infrastructure.Repositories;

public class UsuarioRelacionamentoRepository : IUsuarioRelacionamentoRepository
{
    private static readonly List<UsuarioRelacionamento> _relacionamentos = new();

    public Task<bool> ExisteRelacionamento(Guid seguidorId, Guid seguindoId)
    {
        var existe = _relacionamentos.Any(r => r.SeguidorId == seguidorId && r.SeguindoId == seguindoId);
        return Task.FromResult(existe);
    }

    public Task CriarAsync(UsuarioRelacionamento relacionamento)
    {
        _relacionamentos.Add(relacionamento);
        return Task.CompletedTask;
    }

    public Task RemoverAsync(Guid seguidorId, Guid seguindoId)
    {
        var existente = _relacionamentos.FirstOrDefault
            (r => r.SeguidorId == seguidorId && r.SeguindoId == seguindoId);

        if (existente is not null)
            _relacionamentos.Remove(existente);

        return Task.CompletedTask;
    }
}