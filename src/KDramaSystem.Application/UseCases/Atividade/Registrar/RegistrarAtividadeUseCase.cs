using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Atividade.Registrar;

public class RegistrarAtividadeUseCase
{
    private readonly IAtividadeRepository _atividadeRepository;

    public RegistrarAtividadeUseCase(IAtividadeRepository atividadeRepository)
    {
        _atividadeRepository = atividadeRepository;
    }

    public async Task ExecuteAsync(RegistrarAtividadeRequest request)
    {
        var atividade = new Domain.Entities.Atividade(
            request.UsuarioId,
            request.Tipo,
            request.ReferenciaId
        );

        await _atividadeRepository.RegistrarAsync(atividade);
    }
}