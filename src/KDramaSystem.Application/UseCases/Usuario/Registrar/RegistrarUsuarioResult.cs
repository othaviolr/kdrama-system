namespace KDramaSystem.Application.UseCases.Usuario.Registrar
{
    public class RegistrarUsuarioResult
    {
        public Guid UsuarioId { get; init; }
        public string Nome { get; init; }
        public string NomeUsuario { get; init; }
        public string Email { get; init; }
        public string Token { get; init; }

        public RegistrarUsuarioResult(Guid usuarioId, string nome, string nomeUsuario, string email, string token)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            NomeUsuario = nomeUsuario;
            Email = email;
            Token = token;
        }
    }
}