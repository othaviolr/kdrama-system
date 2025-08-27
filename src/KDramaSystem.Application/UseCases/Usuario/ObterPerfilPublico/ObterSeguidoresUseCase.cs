using KDramaSystem.Application.DTOs.Usuario;
using KDramaSystem.Application.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.Usuario.ObterPerfilPublico;

public class ObterSeguidoresUseCase
{
    private readonly IUsuarioRelacionamentoRepository _relacionamentoRepository;

    public ObterSeguidoresUseCase(IUsuarioRelacionamentoRepository relacionamentoRepository)
    {
        _relacionamentoRepository = relacionamentoRepository;
    }

    public async Task<List<UsuarioResumoDto>> ExecutarAsync(Guid usuarioId)
    {
        var seguidores = await _relacionamentoRepository.ObterSeguidoresAsync(usuarioId);

        return seguidores.Select(s => new UsuarioResumoDto
        {
            UsuarioId = s.Id,
            Nome = s.Nome,
            FotoPerfilUrl = s.FotoUrl
        }).ToList();
    }
}