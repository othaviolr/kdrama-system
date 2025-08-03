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

        private readonly List<Genero> _generos = new();
        public IReadOnlyCollection<Genero> Generos => _generos.AsReadOnly();

        private readonly List<Temporada> _temporadas = new();
        public IReadOnlyCollection<Temporada> Temporadas => _temporadas.AsReadOnly();

        private readonly List<DoramaAtor> _atores = new();
        public IReadOnlyCollection<DoramaAtor> Atores => _atores.AsReadOnly();

        public Dorama(
            Guid id,
            Guid usuarioId,
            string titulo,
            string paisOrigem,
            int anoLancamento,
            bool emExibicao,
            PlataformaStreaming plataforma,
            List<Genero> generos,
            string imagemCapaUrl,
            string? sinopse = null,
            string? tituloOriginal = null)
        {
            ValidarDadosObrigatorios(titulo, paisOrigem, anoLancamento, imagemCapaUrl);

            if (generos == null || generos.Count == 0)
                throw new ArgumentException("Dorama deve ter ao menos um gênero.");

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
            _generos.AddRange(generos);
        }

        public void AtualizarInformacoes(
            string titulo,
            string? tituloOriginal,
            string paisOrigem,
            int anoLancamento,
            bool emExibicao,
            PlataformaStreaming plataforma,
            List<Genero> generos,
            string imagemCapaUrl,
            string? sinopse = null)
        {
            ValidarDadosObrigatorios(titulo, paisOrigem, anoLancamento, imagemCapaUrl);

            if (generos == null || generos.Count == 0)
                throw new ArgumentException("Dorama deve ter ao menos um gênero.");

            Titulo = titulo;
            TituloOriginal = tituloOriginal;
            PaisOrigem = paisOrigem;
            AnoLancamento = anoLancamento;
            EmExibicao = emExibicao;
            Plataforma = plataforma;
            Sinopse = sinopse;
            ImagemCapaUrl = imagemCapaUrl;

            _generos.Clear();
            _generos.AddRange(generos);
        }

        private void ValidarDadosObrigatorios(string titulo, string paisOrigem, int anoLancamento, string imagemCapaUrl)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Título é obrigatório.");

            if (string.IsNullOrWhiteSpace(paisOrigem))
                throw new ArgumentException("País de origem é obrigatório.");

            if (anoLancamento <= 1900 || anoLancamento > DateTime.UtcNow.Year + 1)
                throw new ArgumentException("Ano de lançamento inválido.");

            if (string.IsNullOrWhiteSpace(imagemCapaUrl))
                throw new ArgumentException("Imagem de capa é obrigatória.");
        }

        public void AtualizarSinopse(string? novaSinopse) => Sinopse = novaSinopse;

        public void AlterarTituloOriginal(string? novoTituloOriginal) => TituloOriginal = novoTituloOriginal;

        public void TrocarPlataforma(PlataformaStreaming novaPlataforma) => Plataforma = novaPlataforma;

        public void MarcarComoEncerrado() => EmExibicao = false;

        public void AdicionarGenero(Genero genero)
        {
            if (genero == null) throw new ArgumentNullException(nameof(genero));
            if (_generos.Any(g => g.Id == genero.Id)) return;
            _generos.Add(genero);
        }

        public void RemoverGenero(Guid generoId)
        {
            var genero = _generos.FirstOrDefault(g => g.Id == generoId);
            if (genero != null) _generos.Remove(genero);
        }

        public void AdicionarAtor(DoramaAtor doramaAtor)
        {
            if (doramaAtor == null) throw new ArgumentNullException(nameof(doramaAtor));
            if (_atores.Any(a => a.AtorId == doramaAtor.AtorId)) return;
            _atores.Add(doramaAtor);
        }

        public void RemoverAtor(Guid atorId)
        {
            var relacao = _atores.FirstOrDefault(a => a.AtorId == atorId);
            if (relacao != null) _atores.Remove(relacao);
        }

        public void AdicionarTemporada(Temporada temporada)
        {
            if (temporada == null) throw new ArgumentNullException(nameof(temporada));
            if (_temporadas.Any(t => t.Id == temporada.Id)) return;
            _temporadas.Add(temporada);
        }

        public void RemoverTemporada(Guid temporadaId)
        {
            var temporada = _temporadas.FirstOrDefault(t => t.Id == temporadaId);
            if (temporada != null) _temporadas.Remove(temporada);
        }
    }
}