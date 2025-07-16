namespace KDramaSystem.Application.UseCases.Usuario.Dtos
{
    public class PerfilCompletoDto
    {
        public string Nome { get; set; } = null!;
        public string NomeUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FotoUrl { get; set; }
        public string? Bio { get; set; }
    }
}