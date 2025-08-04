namespace KDramaSystem.Domain.Entities
{
    public class Genero
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        private Genero() { }

        public Genero(Guid id, string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do gênero é obrigatório.");

            Id = id;
            Nome = nome;
        }

        public void AtualizarNome(string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
                throw new ArgumentException("Novo nome do gênero é inválido.");

            Nome = novoNome;
        }
    }
}