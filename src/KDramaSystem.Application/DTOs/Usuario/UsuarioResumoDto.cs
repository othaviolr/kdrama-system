namespace KDramaSystem.Application.DTOs.Usuario;

public class UsuarioResumoDto
{
    public Guid UsuarioId { get; set; }
    public string NomeUsuario { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string? FotoPerfilUrl { get; set; }
}