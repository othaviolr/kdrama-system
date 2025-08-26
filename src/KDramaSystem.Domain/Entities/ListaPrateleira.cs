using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Domain.Entities
{
    public class ListaPrateleira
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public string? ImagemCapaUrl { get; private set; }
        public ListaPrivacidade Privacidade { get; private set; }
        public string? ShareToken { get; private set; }
        public List<DoramaLista> Doramas { get; private set; } = new();
        public DateTime DataCriacao { get; private set; }

        private ListaPrateleira() { }

        public ListaPrateleira(Guid id, Guid usuarioId, string nome, ListaPrivacidade privacidade, string? descricao = null, string? imagemCapaUrl = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome da lista é obrigatório");

            Id = id;
            UsuarioId = usuarioId;
            Nome = nome.Trim();
            Descricao = descricao?.Trim();
            ImagemCapaUrl = imagemCapaUrl?.Trim();
            Privacidade = privacidade;
            DataCriacao = DateTime.UtcNow;

            if (privacidade == ListaPrivacidade.CompartilhadoLink)
                ShareToken = Guid.NewGuid().ToString("N");
        }

        public void AlterarPrivacidade(ListaPrivacidade novaPrivacidade)
        {
            Privacidade = novaPrivacidade;

            if (novaPrivacidade == ListaPrivacidade.CompartilhadoLink && string.IsNullOrEmpty(ShareToken))
                ShareToken = Guid.NewGuid().ToString("N");

            if (novaPrivacidade != ListaPrivacidade.CompartilhadoLink)
                ShareToken = null;
        }

        public void GerarShareToken()
        {
            if (string.IsNullOrEmpty(ShareToken))
                ShareToken = Guid.NewGuid().ToString("N");
        }

        public void AlterarImagemCapa(string? novaUrl)
        {
            ImagemCapaUrl = novaUrl?.Trim();
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
            if (string.IsNullOrWhiteSpace(novoNome))
                throw new ArgumentException("Nome da lista não pode ser vazio.");

            Nome = novoNome.Trim();
        }
    }
}