using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.Enums;
using KDramaSystem.Domain.Services;

namespace KDramaSystem.Application.Services;

public class BadgeService : IBadgeService
{
    private readonly IProgressoTemporadaRepository _progressoRepository;
    private readonly IBadgeUsuarioRepository _badgeUsuarioRepository;
    private readonly ITemporadaRepository _temporadaRepository;

    public BadgeService(
        IProgressoTemporadaRepository progressoRepository,
        ITemporadaRepository temporadaRepository,
        IBadgeUsuarioRepository badgeUsuarioRepository)
    {
        _progressoRepository = progressoRepository;
        _temporadaRepository = temporadaRepository;
        _badgeUsuarioRepository = badgeUsuarioRepository;
    }

    public async Task<List<Badge>> ConcederBadgesProgressaoAsync(Guid usuarioId)
    {
        var progressoUsuario = await _progressoRepository.ObterPorUsuarioAsync(usuarioId);

        var progressoPorDorama = progressoUsuario
            .GroupBy(p => p.Temporada.DoramaId);

        int doramasConcluidos = 0;

        foreach (var grupo in progressoPorDorama)
        {
            var doramaId = grupo.Key;
            bool doramaFinalizado = true;

            foreach (var progressoTemporada in grupo)
            {
                var temporada = await _temporadaRepository.ObterPorIdAsync(progressoTemporada.TemporadaId);
                var totalEpisodios = temporada?.Episodios.Count ?? 0;

                if (totalEpisodios == 0 || grupo.Count(p => p.TemporadaId == progressoTemporada.TemporadaId) < totalEpisodios)
                {
                    doramaFinalizado = false;
                    break;
                }
            }

            if (doramaFinalizado)
                doramasConcluidos++;
        }

        var badgesProgressao = BadgeCatalogo.ObterTodas()
            .Where(b => b.Categoria == CategoriaBadge.Progressao)
            .OrderBy(b => b.Pontos)
            .ToList();

        var novasBadges = new List<Badge>();

        foreach (var badge in badgesProgressao)
        {
            var possuiBadge = await _badgeUsuarioRepository.UsuarioPossuiBadgeAsync(usuarioId, badge.Id);
            if (doramasConcluidos >= badge.Pontos && !possuiBadge)
            {
                await _badgeUsuarioRepository.AdicionarAsync(usuarioId, badge.Id);
                novasBadges.Add(badge);
            }
        }
        return novasBadges;
    }
}