using KDramaSystem.Application.DTOs.ListaPrateleira;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.ListaPrateleira.Obter;

public class ObterListasPorUsuarioUseCase
{
    private readonly IListaPrateleiraRepository _listaRepository;

    public ObterListasPorUsuarioUseCase(IListaPrateleiraRepository listaRepository)
    {
        _listaRepository = listaRepository;
    }

    public async Task<IEnumerable<ObterListaPrateleiraDto>> ExecutarAsync(Guid usuarioId)
    {
        var listas = await _listaRepository.ObterPorUsuarioAsync(usuarioId);

        return listas.Select(l => new ObterListaPrateleiraDto
        {
            Id = l.Id,
            Nome = l.Nome,
            Descricao = l.Descricao,
            ImagemCapaUrl = l.ImagemCapaUrl,
            Privacidade = l.Privacidade,
            ShareToken = l.ShareToken,
            UsuarioId = l.UsuarioId,
            DataCriacao = l.DataCriacao,
            Doramas = l.Doramas.Select(d => new DoramaListaDto
            {
                DoramaId = d.DoramaId,
                DataAdicao = d.DataAdicao
            }).ToList()
        });
    }
}