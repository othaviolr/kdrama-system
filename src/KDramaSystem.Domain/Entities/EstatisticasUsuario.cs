namespace KDramaSystem.Domain.Entities
{
    public class EstatisticasUsuario
    {
        public Guid UsuarioId { get; set; }

        public int DoramasConcluidos { get; set; }
        public int TempoTotalAssistidoEmMinutos { get; set; }
        public int TotalAvaliacoes { get; set; }

        public List<BadgeConquista> Conquistas { get; set; } = new();

        public List<Badge> BadgesPendentes { get; set; } = new();

        public int Nivel { get; set; }

        public int PontosTotais { get; set; }
    }
}