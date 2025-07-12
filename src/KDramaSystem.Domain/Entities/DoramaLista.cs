namespace KDramaSystem.Domain.Entities
{
    public class DoramaLista
    {
        public Guid Id { get; private set; }
        public Guid ListaPrateleiraId { get; private set; }
        public Guid DoramaId { get; private set; }
        public DateTime DataAdicao { get; private set; }

        private DoramaLista() { }

        public DoramaLista(Guid id, Guid listaPrateleiraId, Guid doramaId)
        {
            Id = id;
            ListaPrateleiraId = listaPrateleiraId;
            DoramaId = doramaId;
            DataAdicao = DateTime.UtcNow;
        }
    }
}