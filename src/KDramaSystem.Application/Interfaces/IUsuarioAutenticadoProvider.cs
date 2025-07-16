namespace KDramaSystem.Application.Interfaces
{
    public interface IUsuarioAutenticadoProvider
    {
        Guid ObterUsuarioId();
        string ObterEmail();
    }
}