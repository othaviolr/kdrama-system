using KDramaSystem.Domain.ValueObjects;

namespace KDramaSystem.Domain.Entities
{
    public class Comentario
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid? AvaliacaoId { get; private set; }
        public Guid? TemporadaId { get; private set; }
        public ComentarioValor Texto { get; private set; }
        public DateTime Data { get; private set; }

        private Comentario() { }

        public Comentario(Guid id, Guid usuarioId, ComentarioValor texto, Guid? avaliacaoId = null, Guid? temporadaId = null)
        {
            if (avaliacaoId is null && temporadaId is null)
                throw new ArgumentException("Comentário deve estar associado a uma avaliação ou temporada.");

            if (avaliacaoId is not null && temporadaId is not null)
                throw new ArgumentException("Comentário deve estar associado a apenas uma entidade.");

            Id = id;
            UsuarioId = usuarioId;
            Texto = texto ?? throw new ArgumentNullException(nameof(texto));
            AvaliacaoId = avaliacaoId;
            TemporadaId = temporadaId;
            Data = DateTime.UtcNow;
        }

        public void AtualizarTexto(ComentarioValor novoTexto)
        {
            Texto = novoTexto ?? throw new ArgumentNullException(nameof(novoTexto));
            Data = DateTime.UtcNow;
        }
    }
}