namespace KDramaSystem.Application.DTOs.Usuario;

public class UsuarioResumoDto
{
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? FotoPerfilUrl { get; set; }
}