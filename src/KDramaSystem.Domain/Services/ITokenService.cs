namespace KDramaSystem.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GerarToken(Guid usuarioId, string nomeUsuario, string email);
    }
}