namespace KDramaSystem.Domain.Entities
{
    public class Dorama
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public string? TituloOriginal { get; private set; }
        public string PaisOrigem { get; private set; }
        public int AnoLancamento { get; private set; }
        public bool EmExibicao { get; private set; }
        public string? Sinopse { get; private set; }

        private readonly List<Genero> _generos = new();
        public IReadOnlyCollection<Genero> Generos => _generos.AsReadOnly();

        private readonly List<Ator> _atores = new();
        public IReadOnlyCollection<Ator> Atores => _atores.AsReadOnly();

        private readonly List<Temporada> _temporadas = new();
        public IReadOnlyCollection<Temporada> Temporadas => _temporadas.AsReadOnly();

        public Dorama(Guid id, string titulo, string paisOrigem, int anoLancamento, bool emExibicao, string? sinopse = null, string? tituloOriginal = null)
        {
            if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título é obrigatório.");
            if (string.IsNullOrWhiteSpace(paisOrigem)) throw new ArgumentException("País de origem é obrigatório.");
            if (anoLancamento <= 1900 || anoLancamento > DateTime.UtcNow.Year + 1) 
                throw new ArgumentException("Ano de lançamento inválido.");

            Id = id;
            Titulo = titulo;
            TituloOriginal = tituloOriginal;
            PaisOrigem = paisOrigem;
            AnoLancamento = anoLancamento;
            EmExibicao = emExibicao;
            Sinopse = sinopse;
        }

        public void AdicionarGenero(Genero genero)
        {
            if (genero == null) throw new ArgumentNullException(nameof(genero));
            if (_generos.Any(g => g.Id == genero.Id)) return;
            _generos.Add(genero);
        }

        public void AdicionarAtor(Ator ator)
        {
            if (ator == null) throw new ArgumentNullException(nameof(ator));
            if (_atores.Any(a => a.Id == ator.Id)) return;
            _atores.Add(ator);
        }

        public void AdicionarTemporada(Temporada temporada)
        {
            if (temporada == null) throw new ArgumentNullException(nameof(temporada));
            if (_temporadas.Any(t => t.Id == temporada.Id)) return;
            _temporadas.Add(temporada);
        }

        public void MarcarComoEncerrado()
        {
            EmExibicao = false;
        }
    }
}