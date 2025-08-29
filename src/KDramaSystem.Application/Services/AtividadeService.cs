using KDramaSystem.Application.DTOs.Atividade;
using KDramaSystem.Application.Interfaces.Services;
using KDramaSystem.Domain.Enums;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.Services;

public class AtividadeService : IAtividadeService
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;
    private readonly IProgressoTemporadaRepository _progressoRepository;
    private readonly IListaPrateleiraRepository _listaPrateleiraRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITemporadaRepository _temporadaRepository;

    public AtividadeService(
        IAvaliacaoRepository avaliacaoRepository,
        IProgressoTemporadaRepository progressoRepository,
        IListaPrateleiraRepository listaPrateleiraRepository,
        IUsuarioRepository usuarioRepository,
        ITemporadaRepository temporadaRepository)
    {
        _avaliacaoRepository = avaliacaoRepository;
        _progressoRepository = progressoRepository;
        _listaPrateleiraRepository = listaPrateleiraRepository;
        _usuarioRepository = usuarioRepository;
        _temporadaRepository = temporadaRepository;
    }

    public async Task<List<AtividadeDto>> ObterFeedAsync(int quantidade)
    {
        var atividades = new List<AtividadeDto>();

        var todosProgressos = await _progressoRepository.ObterTodosAsync();
        foreach (var p in todosProgressos)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(p.UsuarioId);
            var temporada = await _temporadaRepository.ObterPorIdAsync(p.TemporadaId);

            atividades.Add(new AtividadeDto
            {
                UsuarioId = p.UsuarioId,
                UsuarioNome = usuario?.NomeUsuario ?? "Desconhecido",
                UsuarioAvatarUrl = usuario?.FotoUrl,
                TipoAtividade = TipoAtividadeEnum.ProgressoTemporada,
                DoramaId = temporada?.DoramaId ?? Guid.Empty,
                DoramaTitulo = temporada?.Dorama.Titulo ?? "Desconhecido",
                TemporadaNumero = temporada?.Numero,
                CriadoEm = p.DataAtualizacao
            });
        }

        var todasAvaliacoes = await _avaliacaoRepository.ObterTodasAsync();
        foreach (var a in todasAvaliacoes)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(a.UsuarioId);
            var temporada = await _temporadaRepository.ObterPorIdAsync(a.TemporadaId);

            atividades.Add(new AtividadeDto
            {
                UsuarioId = a.UsuarioId,
                UsuarioNome = usuario?.NomeUsuario ?? "Desconhecido",
                UsuarioAvatarUrl = usuario?.FotoUrl,
                TipoAtividade = TipoAtividadeEnum.Avaliacao,
                DoramaId = temporada?.DoramaId ?? Guid.Empty,
                DoramaTitulo = temporada?.Dorama.Titulo ?? "Desconhecido",
                TemporadaNumero = temporada?.Numero,
                Nota = a.Nota.Valor,
                Comentario = a.Comentario?.Texto,
                CriadoEm = a.DataAvaliacao
            });
        }

        var prateleirasPublicas = await _listaPrateleiraRepository.ObterPublicasAsync(Guid.Empty);
        foreach (var l in prateleirasPublicas)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(l.UsuarioId);

            atividades.Add(new AtividadeDto
            {
                UsuarioId = l.UsuarioId,
                UsuarioNome = usuario?.NomeUsuario ?? "Desconhecido",
                UsuarioAvatarUrl = usuario?.FotoUrl,
                TipoAtividade = TipoAtividadeEnum.Prateleira,
                PrateleiraId = l.Id,
                PrateleiraNome = l.Nome,
                CriadoEm = l.DataCriacao
            });
        }

        return atividades
            .OrderByDescending(a => a.CriadoEm)
            .Take(quantidade)
            .ToList();
    }

    public async Task<List<AtividadeDto>> ObterAtividadesUsuarioAsync(Guid usuarioId)
    {
        var atividades = new List<AtividadeDto>();
        var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);
        var nomeUsuario = usuario?.NomeUsuario ?? "Desconhecido";

        var progressos = await _progressoRepository.ObterPorUsuarioAsync(usuarioId);
        foreach (var p in progressos)
        {
            var temporada = await _temporadaRepository.ObterPorIdAsync(p.TemporadaId);

            atividades.Add(new AtividadeDto
            {
                UsuarioId = usuarioId,
                UsuarioNome = nomeUsuario,
                UsuarioAvatarUrl = usuario?.FotoUrl,
                TipoAtividade = TipoAtividadeEnum.ProgressoTemporada,
                DoramaId = temporada?.DoramaId ?? Guid.Empty,
                DoramaTitulo = temporada?.Dorama.Titulo ?? "Desconhecido",
                TemporadaNumero = temporada?.Numero,
                CriadoEm = p.DataAtualizacao
            });
        }

        var avaliacoes = await _avaliacaoRepository.ObterPorUsuarioAsync(usuarioId);
        foreach (var a in avaliacoes)
        {
            var temporada = await _temporadaRepository.ObterPorIdAsync(a.TemporadaId);

            atividades.Add(new AtividadeDto
            {
                UsuarioId = usuarioId,
                UsuarioNome = nomeUsuario,
                UsuarioAvatarUrl = usuario?.FotoUrl,
                TipoAtividade = TipoAtividadeEnum.Avaliacao,
                DoramaId = temporada?.DoramaId ?? Guid.Empty,
                DoramaTitulo = temporada?.Dorama.Titulo ?? "Desconhecido",
                TemporadaNumero = temporada?.Numero,
                Nota = a.Nota.Valor,
                Comentario = a.Comentario?.Texto,
                CriadoEm = a.DataAvaliacao
            });
        }

        var prateleiras = await _listaPrateleiraRepository.ObterPorUsuarioAsync(usuarioId);
        foreach (var l in prateleiras.Where(l => l.Privacidade == Domain.Enums.ListaPrivacidade.Publico))
        {
            atividades.Add(new AtividadeDto
            {
                UsuarioId = usuarioId,
                UsuarioNome = nomeUsuario,
                UsuarioAvatarUrl = usuario?.FotoUrl,
                TipoAtividade = TipoAtividadeEnum.Prateleira,
                PrateleiraId = l.Id,
                PrateleiraNome = l.Nome,
                CriadoEm = l.DataCriacao
            });
        }

        return atividades.OrderByDescending(a => a.CriadoEm).ToList();
    }
}