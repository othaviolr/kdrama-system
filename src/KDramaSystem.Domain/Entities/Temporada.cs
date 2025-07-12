namespace KDramaSystem.Domain.Entities
{
    public class Temporada
    {
        public Guid Id { get; private set; }
        public Guid DoramaId { get; private set; }
        public int Numero { get; private set; }
        public string? Nome { get; private set; }
        public int AnoLancamento { get; private set; }
        public bool EmExibicao { get; private set; }
        public int NumeroEpisodios => _episodios.Count;
        public string? Sinopse { get; private set; }

        private readonly List<Episodio> _episodios = new();
        public IReadOnlyCollection<Episodio> Episodios => _episodios.AsReadOnly();

        public Temporada(Guid id, Guid doramaId, int numero, int anoLancamento, bool emExibicao, string? nome = null, string? sinopse = null)
        {
            if (numero <= 0) throw new ArgumentException("Número da temporada deve ser maior que zero.");
            if (anoLancamento <= 1900 || anoLancamento > DateTime.UtcNow.Year + 1)
                throw new ArgumentException("Ano de lançamento inválido.");

            Id = id;
            DoramaId = doramaId;
            Numero = numero;
            AnoLancamento = anoLancamento;
            EmExibicao = emExibicao;
            Nome = nome;
            Sinopse = sinopse;
        }

        public void AdicionarEpisodio(Episodio episodio)
        {
            if (episodio == null) throw new ArgumentNullException(nameof(episodio));
            if (_episodios.Any(e => e.Numero == episodio.Numero)) 
                throw new InvalidOperationException($"Episódio {episodio.Numero} já existe nesta temporada");

            _episodios.Add(episodio);
        }

        public void MarcarComoEncerrada()
        {
            EmExibicao = false;
        }
    }
}