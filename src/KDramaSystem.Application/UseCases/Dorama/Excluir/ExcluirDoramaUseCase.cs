using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Dorama.Excluir;

public class ExcluirDoramaUseCase
{
    private readonly IDoramaRepository _doramaRepository;
    public ExcluirDoramaUseCase(IDoramaRepository doramaRepository)
    {
        _doramaRepository = doramaRepository;
    }
    public async Task ExecutarAsync(ExcluirDoramaRequest request)
    {
        var dorama = await _doramaRepository.ObterPorIdAsync(request.Id);
        if (dorama == null)
        {
            throw new Exception("Dorama não encontrado.");
        }

        await _doramaRepository.ExcluirAsync(dorama.Id);
    }
}