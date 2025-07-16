namespace KDramaSystem.Application.UseCases.Usuario.Editar
{
    public class EditarPerfilRequest
    {
        public string Nome { get; set; } = null!;
        public string NomeUsuario { get; set; } = null!;
        public string? FotoUrl { get; set; }
        public string? Bio { get; set; }
    }
}