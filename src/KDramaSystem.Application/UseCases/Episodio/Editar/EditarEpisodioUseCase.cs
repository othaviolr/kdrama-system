using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Episodio.Editar
{
    public class EditarEpisodioUseCase
    {
        private readonly IEpisodioRepository _episodioRepository;

        public EditarEpisodioUseCase(IEpisodioRepository episodioRepository)
        {
            _episodioRepository = episodioRepository;
        }

        public async Task ExecutarAsync(EditarEpisodioRequest request)
        {
            var episodio = await _episodioRepository.ObterPorIdAsync(request.Id);

            if (episodio == null)
                throw new Exception("Episódio não encontrado.");

            var episodiosNaTemporada = await _episodioRepository.ListarPorTemporadaAsync(episodio.TemporadaId);
            if (episodiosNaTemporada.Any(e => e.Numero == request.Numero && e.Id != request.Id))
                throw new Exception($"Já existe um episódio com o número {request.Numero} nessa temporada.");

            var episodioAtualizado = new KDramaSystem.Domain.Entities.Episodio(
                episodio.Id,
                episodio.TemporadaId,
                request.Numero,
                request.Titulo,
                request.DuracaoMinutos,
                request.Tipo,
                request.Sinopse
            );

            await _episodioRepository.AtualizarAsync(episodioAtualizado);
        }
    }
}