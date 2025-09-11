using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.Services;

namespace KDramaSystem.Application.Services
{
    public class EstatisticasService
    {
        private readonly IProgressoTemporadaRepository _progressoRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IBadgeConquistaRepository _badgeConquistaRepository;
        private readonly IBadgeService _badgeService;

        public EstatisticasService(
            IProgressoTemporadaRepository progressoRepository,
            IAvaliacaoRepository avaliacaoRepository,
            IBadgeConquistaRepository badgeConquistaRepository,
            IBadgeService badgeService)
        {
            _progressoRepository = progressoRepository;
            _avaliacaoRepository = avaliacaoRepository;
            _badgeConquistaRepository = badgeConquistaRepository;
            _badgeService = badgeService;
        }

        public async Task<EstatisticasUsuario> ObterPorUsuarioAsync(Guid usuarioId)
        {
            var estatisticas = new EstatisticasUsuario
            {
                UsuarioId = usuarioId
            };

            estatisticas.DoramasConcluidos = await _progressoRepository
                .ContarDoramasConcluidosAsync(usuarioId);

            estatisticas.TempoTotalAssistidoEmMinutos = await _progressoRepository
                .SomarTempoAssistidoAsync(usuarioId);

            estatisticas.TotalAvaliacoes = await _avaliacaoRepository
                .ContarAvaliacoesAsync(usuarioId);

            await _badgeService.ConcederBadgesProgressaoAsync(usuarioId);

            estatisticas.Conquistas = await _badgeConquistaRepository
                .ObterPorUsuarioAsync(usuarioId);

            var todasBadges = BadgeCatalogo.ObterTodas();
            estatisticas.BadgesPendentes = todasBadges
                .Where(b => !estatisticas.Conquistas.Any(c => c.BadgeId == b.Id))
                .ToList();

            estatisticas.PontosTotais = estatisticas.Conquistas
                .Sum(c =>
                {
                    var badge = todasBadges.FirstOrDefault(b => b.Id == c.BadgeId);
                    return badge?.Pontos ?? 0;
                });

            estatisticas.Nivel = estatisticas.PontosTotais / 100 + 1;

            return estatisticas;
        }
    }
}