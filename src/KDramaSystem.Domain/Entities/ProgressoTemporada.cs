using KDramaSystem.Domain.ValueObjects;

namespace KDramaSystem.Domain.Entities
{
    public class ProgressoTemporada
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid TemporadaId { get; private set; }
        public int EpisodiosAssistidos { get; private set; }
        public StatusDorama Status { get; private set; }
        public DateTime DataAtualizacao { get; private set; }

        private ProgressoTemporada() { } 

        public ProgressoTemporada(Guid id, Guid usuarioId, Guid temporadaId, int episodiosAssistidos, StatusDorama status)
        {
            if (episodiosAssistidos < 0)
                throw new ArgumentException("Quantidade de episódios assistidos inválida.", nameof(episodiosAssistidos));

            Id = id;
            UsuarioId = usuarioId;
            TemporadaId = temporadaId;
            EpisodiosAssistidos = episodiosAssistidos;
            Status = status ?? throw new ArgumentNullException(nameof(status));
            DataAtualizacao = DateTime.UtcNow;
        }

        public void AtualizarProgresso(int novosEpisodiosAssistidos, int totalEpisodiosTemporada)
        {
            if (novosEpisodiosAssistidos < 0)
                throw new ArgumentException("Progresso inválido.", nameof(novosEpisodiosAssistidos));

            if (novosEpisodiosAssistidos > totalEpisodiosTemporada)
                throw new ArgumentException("Progresso não pode ser maior que o total de episódios da temporada.", nameof(novosEpisodiosAssistidos));

            EpisodiosAssistidos = novosEpisodiosAssistidos;

            if (novosEpisodiosAssistidos == totalEpisodiosTemporada)
                Status = new StatusDorama(Domain.Enums.StatusDoramaEnum.Concluido);

            DataAtualizacao = DateTime.UtcNow;
        }

        public void AtualizarStatus(StatusDorama novoStatus)
        {
            Status = novoStatus ?? throw new ArgumentNullException(nameof(novoStatus));
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}