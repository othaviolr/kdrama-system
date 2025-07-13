namespace KDramaSystem.Application.UseCases.Usuario.Dtos
{
    public class UsuarioAutenticadoDto
    {
        public Guid Id { get; }
        public string Nome { get; }
        public string NomeUsuario { get; }
        public string Email { get; }
        public string Token { get; }

        public UsuarioAutenticadoDto(Guid id, string nome, string nomeUsuario, string email, string token)
        {
            Id = id;
            Nome = nome;
            NomeUsuario = nomeUsuario;
            Email = email;
            Token = token;
        }
    }
}