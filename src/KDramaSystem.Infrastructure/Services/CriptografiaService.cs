using KDramaSystem.Domain.Interfaces.Services;
using BCrypt.Net;

namespace KDramaSystem.Infrastructure.Services
{
    public class CriptografiaService : ICriptografiaService
    {
        public string GerarHash(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool VerificarSenha(string senha, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }
    }
}