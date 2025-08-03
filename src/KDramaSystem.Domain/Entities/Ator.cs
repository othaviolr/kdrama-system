namespace KDramaSystem.Domain.Entities
{
    public class Ator
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string? NomeCompleto { get; private set; }
        public int? AnoNascimento { get; private set; }
        public decimal? Altura { get; private set; }
        public string? Pais { get; private set; }
        public string? Biografia { get; private set; }
        public string? FotoUrl { get; private set; }
        public string? Instagram { get; private set; }

        private readonly List<DoramaAtor> _doramas = new();
        public IReadOnlyCollection<DoramaAtor> Doramas => _doramas;

        public Ator(Guid id, string nome, string? nomeCompleto = null, int? anoNascimento = null, decimal? altura = null,
                    string? pais = null, string? biografia = null, string? fotoUrl = null, string? instagram = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");

            if (nome.Length < 2)
                throw new ArgumentException("Nome deve ter pelo menos 2 caracteres.");

            if (anoNascimento.HasValue && (anoNascimento < 1900 || anoNascimento > DateTime.UtcNow.Year))
                throw new ArgumentException("Ano de nascimento inválido.");

            if (altura.HasValue && altura <= 0)
                throw new ArgumentException("Altura deve ser um valor positivo.");

            Id = id;
            Nome = nome;
            NomeCompleto = nomeCompleto;
            AnoNascimento = anoNascimento;
            Altura = altura;
            Pais = pais;
            Biografia = biografia;
            FotoUrl = fotoUrl;
            Instagram = instagram;
        }

        public void AtualizarDados(string nome, string? nomeCompleto, int? anoNascimento, decimal? altura,
                                   string? pais, string? biografia, string? fotoUrl, string? instagram)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");

            if (nome.Length < 2)
                throw new ArgumentException("Nome deve ter pelo menos 2 caracteres.");

            if (anoNascimento.HasValue && (anoNascimento < 1900 || anoNascimento > DateTime.UtcNow.Year))
                throw new ArgumentException("Ano de nascimento inválido.");

            if (altura.HasValue && altura <= 0)
                throw new ArgumentException("Altura deve ser um valor positivo.");

            Nome = nome;
            NomeCompleto = nomeCompleto;
            AnoNascimento = anoNascimento;
            Altura = altura;
            Pais = pais;
            Biografia = biografia;
            FotoUrl = fotoUrl;
            Instagram = instagram;
        }

        public void AdicionarDorama(DoramaAtor doramaAtor)
        {
            if (doramaAtor == null)
                throw new ArgumentException("Associação com dorama inválida.");

            _doramas.Add(doramaAtor);
        }
    }
}