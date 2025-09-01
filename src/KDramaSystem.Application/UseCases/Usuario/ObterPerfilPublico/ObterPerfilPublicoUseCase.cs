using KDramaSystem.Application.Interfaces.Repositories;
using KDramaSystem.Application.UseCases.Usuario.Dtos;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.Usuario.ObterPerfilPublico;

public class ObterPerfilPublicoUseCase : IObterPerfilPublicoUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUsuarioRelacionamentoRepository _relacionamentoRepository;

    public ObterPerfilPublicoUseCase(IUsuarioRepository usuarioRepository, IUsuarioRelacionamentoRepository relacionamentoRepository)
    {
        _usuarioRepository = usuarioRepository;
        _relacionamentoRepository = relacionamentoRepository;
    }

    public async Task<PerfilPublicoDto?> ExecutarAsync(string nomeUsuario, Guid? usuarioLogadoId = null)
    {
        if (string.IsNullOrWhiteSpace(nomeUsuario))
            throw new ArgumentException("Nome de usuário deve ser informado.", nameof(nomeUsuario));

        var usuario = await _usuarioRepository.ObterPorNomeUsuarioAsync(nomeUsuario);
        if (usuario == null) return null;

        var totalSeguidores = await _relacionamentoRepository.ObterSeguidoresAsync(usuario.Id);
        var totalSeguindo = await _relacionamentoRepository.ObterSeguindoAsync(usuario.Id);

        bool segueUsuarioAtual = false;
        if (usuarioLogadoId.HasValue)
            segueUsuarioAtual = await _relacionamentoRepository.ExisteRelacionamentoAsync(usuarioLogadoId.Value, usuario.Id);
        
        return new PerfilPublicoDto
        {
            UsuarioId = usuario.Id,
            Nome = usuario.Nome,
            NomeUsuario = usuario.NomeUsuario,
            FotoUrl = usuario.FotoUrl,
            Bio = usuario.Bio,
            TotalSeguidores = totalSeguidores.Count,
            TotalSeguindo = totalSeguindo.Count,
            SegueUsuarioAtual = segueUsuarioAtual
        };
    }
}