using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Dorama.Obter;

public class ObterDoramaUseCase
{
    private readonly IDoramaRepository _doramaRepository;
    public ObterDoramaUseCase(IDoramaRepository doramaRepository)
    {
        _doramaRepository = doramaRepository;
    }
    public async Task<KDramaSystem.Domain.Entities.Dorama> ExecutarAsync(ObterDoramaRequest request)
    {
        var dorama = await _doramaRepository.ObterPorIdAsync(request.Id);
        if (dorama == null)
        {
            throw new Exception("Dorama não encontrado.");
        }
        return dorama;
    }
}