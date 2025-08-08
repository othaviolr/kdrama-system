namespace KDramaSystem.Application.UseCases.Usuario.Dtos;

public class PerfilPublicoDto
{
    public string Nome { get; set; } = null!;
    public string NomeUsuario { get; set; } = null!;
    public string? FotoUrl { get; set; }
    public string? Bio { get; set; }
    public int TotalSeguidores { get; set; }
    public int TotalSeguindo { get; set; }
    public bool SegueUsuarioAtual { get; set; }
}