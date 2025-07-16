using System.Threading.Tasks;

namespace KDramaSystem.Application.UseCases.Usuario.Editar
{
    public interface IEditarPerfilUseCase
    {
        Task ExecutarAsync(EditarPerfilRequest request);
    }
}