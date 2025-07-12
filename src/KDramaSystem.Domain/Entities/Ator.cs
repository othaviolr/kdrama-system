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

        public Ator(Guid id, string nome, string? nomeCompleto = null, int? anoNascimento = null, decimal? altura = null,
                    string? pais = null, string? biografia = null, string? fotoUrl = null, string? instagram = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");

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
    }
}