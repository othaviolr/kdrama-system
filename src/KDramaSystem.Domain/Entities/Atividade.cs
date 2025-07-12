using KDramaSystem.Domain.ValueObjetcs;

namespace KDramaSystem.Domain.Entities
{
    public class Atividade
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public TipoAtividade Tipo { get; private set; }
        public Guid ReferenciaId { get; private set; }
        public DateTime Data { get; private set; }

        private Atividade() { }

        public Atividade(Guid id, Guid usuarioId, TipoAtividade tipo, Guid referenciaId)
        {
            Id = id;
            UsuarioId = usuarioId;
            Tipo = tipo ?? throw new ArgumentNullException(nameof(tipo));
            ReferenciaId = referenciaId;
            Data = DateTime.UtcNow;
        }
    }
}