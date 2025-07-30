using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Domain.Entities
{
    public class Dorama
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public string Titulo { get; private set; }
        public string? TituloOriginal { get; private set; }
        public string PaisOrigem { get; private set; }
        public int AnoLancamento { get; private set; }
        public bool EmExibicao { get; private set; }
        public string? Sinopse { get; private set; }
        public string ImagemCapaUrl { get; private set; }
        public PlataformaStreaming Plataforma { get; private set; }

        private List<Genero> _generos = new();
        public IReadOnlyCollection<Genero> Generos => _generos.AsReadOnly();

        private List<Ator> _atores = new();
        public IReadOnlyCollection<Ator> Atores => _atores.AsReadOnly();

        private List<Temporada> _temporadas = new();
        public IReadOnlyCollection<Temporada> Temporadas => _temporadas.AsReadOnly();

        public Dorama(Guid id, Guid usuarioId, string titulo, string paisOrigem, int anoLancamento, bool emExibicao, PlataformaStreaming plataforma, List<Genero> generos, string imagemCapaUrl, string? sinopse = null, string? tituloOriginal = null)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Título é obrigatório.");

            if (string.IsNullOrWhiteSpace(paisOrigem))
                throw new ArgumentException("País de origem é obrigatório.");

            if (anoLancamento <= 1900 || anoLancamento > DateTime.UtcNow.Year + 1)
                throw new ArgumentException("Ano de lançamento inválido.");

            if (generos == null || generos.Count == 0)
                throw new ArgumentException("Dorama deve ter ao menos um gênero.");

            if (string.IsNullOrWhiteSpace(imagemCapaUrl))
                throw new ArgumentException("Imagem de capa é obrigatória.");

            Id = id;
            UsuarioId = usuarioId;
            Titulo = titulo;
            TituloOriginal = tituloOriginal;
            PaisOrigem = paisOrigem;
            AnoLancamento = anoLancamento;
            EmExibicao = emExibicao;
            Plataforma = plataforma;
            Sinopse = sinopse;
            ImagemCapaUrl = imagemCapaUrl;
            _generos = new List<Genero>(generos);
        }

        public void EditarDados(string titulo, string? tituloOriginal, string paisOrigem, int anoLancamento, bool emExibicao, PlataformaStreaming plataforma, List<Genero> generos, string? sinopse, string imagemCapaUrl)
        {
            Titulo = titulo;
            TituloOriginal = tituloOriginal;
            PaisOrigem = paisOrigem;
            AnoLancamento = anoLancamento;
            EmExibicao = emExibicao;
            Plataforma = plataforma;
            Sinopse = sinopse;
            ImagemCapaUrl = imagemCapaUrl;

            _generos.Clear();
            _generos.AddRange(generos ?? new List<Genero>());
        }
        public void AtualizarSinopse(string? novaSinopse)
        {
            Sinopse = novaSinopse;
        }

        public void AlterarTituloOriginal(string? novoTituloOriginal)
        {
            TituloOriginal = novoTituloOriginal;
        }

        public void TrocarPlataforma(PlataformaStreaming novaPlataforma)
        {
            Plataforma = novaPlataforma;
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

        public void EditarInformacoes(string novoTitulo, string novoPaisOrigem, int novoAnoLancamento, bool emExibicao, PlataformaStreaming novaPlataforma, string novaImagemCapaUrl, string? novoTituloOriginal = null, string? novaSinopse = null)
        {
            if (string.IsNullOrWhiteSpace(novoTitulo))
                throw new ArgumentException("Título é obrigatório.");

            if (string.IsNullOrWhiteSpace(novoPaisOrigem))
                throw new ArgumentException("País de origem é obrigatório.");

            if (novoAnoLancamento <= 1900 || novoAnoLancamento > DateTime.UtcNow.Year + 1)
                throw new ArgumentException("Ano de lançamento inválido.");

            if (string.IsNullOrWhiteSpace(novaImagemCapaUrl))
                throw new ArgumentException("Imagem de capa é obrigatória.");

            Titulo = novoTitulo;
            PaisOrigem = novoPaisOrigem;
            AnoLancamento = novoAnoLancamento;
            EmExibicao = emExibicao;
            Plataforma = novaPlataforma;
            ImagemCapaUrl = novaImagemCapaUrl;
            TituloOriginal = novoTituloOriginal;
            Sinopse = novaSinopse;
        }
    }
}