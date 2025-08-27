using KDramaSystem.Application.DTOs.Usuario;
using KDramaSystem.Application.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.Usuario.ObterPerfilPublico;

public class ObterSeguindoUseCase
{
    private readonly IUsuarioRelacionamentoRepository _relacionamentoRepository;

    public ObterSeguindoUseCase(IUsuarioRelacionamentoRepository relacionamentoRepository)
    {
        _relacionamentoRepository = relacionamentoRepository;
    }

    public async Task<List<UsuarioResumoDto>> ExecutarAsync(Guid usuarioId)
    {
        var seguindo = await _relacionamentoRepository.ObterSeguindoAsync(usuarioId);

        return seguindo.Select(s => new UsuarioResumoDto
        {
            UsuarioId = s.Id,
            Nome = s.Nome,
            FotoPerfilUrl = s.FotoUrl
        }).ToList();
    }
}