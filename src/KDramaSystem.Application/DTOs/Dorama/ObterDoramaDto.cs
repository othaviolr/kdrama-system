using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Application.DTOs.Dorama;

public class ObterDoramaDto
{
    public Guid DoramaId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? TituloOriginal { get; set; }
    public string Sinopse { get; set; } = string.Empty;
    public string CapaUrl { get; set; } = string.Empty;
    public int AnoLancamento { get; set; }
    public string PaisOrigem { get; set; } = string.Empty;
    public bool EmExibicao { get; set; }
    public PlataformaStreaming Plataforma { get; set; }

    public List<GeneroDto> Generos { get; set; } = new();
    public List<AtorDto> Atores { get; set; } = new();

    public class GeneroDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }

    public class AtorDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}