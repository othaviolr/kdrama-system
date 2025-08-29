using KDramaSystem.Application.UseCases.Usuario.Dtos;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.Usuario.ObterPerfilPublico;

public class ObterPerfilPublicoUseCase : IObterPerfilPublicoUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public ObterPerfilPublicoUseCase(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<PerfilPublicoDto?> ExecutarAsync(string nomeUsuario, Guid? usuarioLogadoId = null)
    {
        var usuario = await _usuarioRepository.ObterPorNomeUsuarioAsync(nomeUsuario);
        if (usuario == null) return null;

        return new PerfilPublicoDto
        {
            UsuarioId = usuario.Id,
            Nome = usuario.Nome,
            NomeUsuario = usuario.NomeUsuario,
            FotoUrl = usuario.FotoUrl,
            Bio = usuario.Bio,
            TotalSeguidores = usuario.Seguidores.Count,
            TotalSeguindo = usuario.Seguindo.Count,
            SegueUsuarioAtual = usuarioLogadoId.HasValue &&
                usuario.Seguidores.Any(s => s.SeguidorId == usuarioLogadoId.Value)
        };
    }
}