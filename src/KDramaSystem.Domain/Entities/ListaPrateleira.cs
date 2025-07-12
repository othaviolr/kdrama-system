namespace KDramaSystem.Domain.Entities
{
    public class ListaPrateleira
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public List<DoramaLista> Doramas { get; private set; } = new();
        public DateTime DataCriacao { get; private set; }

        private ListaPrateleira() { }

        public ListaPrateleira(Guid id, Guid usuarioId, string nome, string? descricao = null)
        {

            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome da lista é obrigatório");

            Id = id;
            UsuarioId = usuarioId;
            Nome = nome.Trim();
            Descricao = descricao?.Trim();
            DataCriacao = DateTime.UtcNow;
        }

        public void AdicionarDorama(Guid doramaId)
        {
            if (Doramas.Any(d => d.DoramaId == doramaId))
                return;

            Doramas.Add(new DoramaLista(Guid.NewGuid(), Id, doramaId));
        }

        public void RemoverDorama(Guid doramaId)
        {
            var item = Doramas.FirstOrDefault(d => d.DoramaId == doramaId);
            if (item is not null)
                Doramas.Remove(item);
        }

        public void AtualizarDescricao(string? novaDescricao)
        {
            Descricao = novaDescricao?.Trim();
        }

        public void AtualizarNome(string novoNome)
        {
            if (string.IsNullOrEmpty(novoNome))
                throw new ArgumentException("Nome da lista não pode ser vazia.");

            Nome = novoNome.Trim();
        }
    }
}