namespace KDramaSystem.Domain.Interfaces.Services
{
    public interface ICriptografiaService
    {
        string GerarHash(string senha);
        bool VerificarSenha(string senha, string hash);
    }
}