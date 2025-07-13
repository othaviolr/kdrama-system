namespace KDramaSystem.Application.UseCases.Usuario.Login
{
    public class LoginUsuarioCommand
    {
        public string Email { get; init; }
        public string Senha { get; init; }

        public LoginUsuarioCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}