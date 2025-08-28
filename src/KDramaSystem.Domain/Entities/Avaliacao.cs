using KDramaSystem.Domain.ValueObjects;
using KDramaSystem.Domain.ValueObjetcs;

namespace KDramaSystem.Domain.Entities
{
    public class Avaliacao
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid TemporadaId { get; private set; }
        public Temporada Temporada { get; private set; }
        public Nota Nota { get; private set; }
        public ComentarioValor? Comentario { get; private set; }
        public Guid? RecomendadoPorUsuarioId { get; private set; }
        public string? RecomendadoPorNomeLivre { get; private set; }
        public DateTime DataAvaliacao { get; private set; }

        public Usuario Usuario { get; private set; }

        private Avaliacao() { }

        public Avaliacao(Guid id, Guid usuarioId, Guid temporadaId, Nota nota, ComentarioValor? comentario,
                         Guid? recomendadoPorUsuarioId = null, string? recomendadoPorNomeLivre = null)
        {
            if (recomendadoPorUsuarioId is null && string.IsNullOrWhiteSpace(recomendadoPorNomeLivre) == false)
                recomendadoPorNomeLivre = recomendadoPorNomeLivre?.Trim();

            Id = id;
            UsuarioId = usuarioId;
            TemporadaId = temporadaId;
            Nota = nota ?? throw new ArgumentNullException(nameof(nota));
            Comentario = comentario;
            RecomendadoPorUsuarioId = recomendadoPorUsuarioId;
            RecomendadoPorNomeLivre = recomendadoPorNomeLivre;
            DataAvaliacao = DateTime.UtcNow;

            ValidarOrigemRecomendacao();
        }

        private void ValidarOrigemRecomendacao()
        {
            if (RecomendadoPorUsuarioId is null && string.IsNullOrWhiteSpace(RecomendadoPorNomeLivre))
                return;

            if (RecomendadoPorUsuarioId is not null && string.IsNullOrWhiteSpace(RecomendadoPorNomeLivre) == false)
                throw new InvalidOperationException("A recomendação deve ser por usuário ou por nome livre, não ambos.");
        }

        public void AtualizarNota(Nota novaNota)
        {
            Nota = novaNota ?? throw new ArgumentNullException(nameof(novaNota));
            DataAvaliacao = DateTime.UtcNow;
        }

        public void AtualizarComentario(ComentarioValor? novoComentario)
        {
            Comentario = novoComentario;
            DataAvaliacao = DateTime.UtcNow;
        }

        public void AtualizarRecomendacao(Guid? usuarioId, string? nomeLivre)
        {
            RecomendadoPorUsuarioId = usuarioId;
            RecomendadoPorNomeLivre = nomeLivre?.Trim();
            ValidarOrigemRecomendacao();
        }
    }
}