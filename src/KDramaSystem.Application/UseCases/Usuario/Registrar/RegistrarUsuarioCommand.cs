namespace KDramaSystem.Application.UseCases.Usuario.Registrar
{
    public class RegistrarUsuarioCommand
    {
        public string Nome { get; init; }
        public string NomeUsuario { get; init; }
        public string Email { get; init; }
        public string Senha { get; init; }

        public RegistrarUsuarioCommand(string nome, string nomeUsuario, string email, string senha)
        {
            Nome = nome;
            NomeUsuario = nomeUsuario;
            Email = email;
            Senha = senha;
        }
    }
}