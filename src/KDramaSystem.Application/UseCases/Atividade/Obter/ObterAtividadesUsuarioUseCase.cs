using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Atividade.Obter;

public class ObterAtividadesUsuarioUseCase
{
    private readonly IAtividadeRepository _atividadeRepository;

    public ObterAtividadesUsuarioUseCase(IAtividadeRepository atividadeRepository)
    {
        _atividadeRepository = atividadeRepository;
    }

    public async Task<IEnumerable<ObterAtividadeDto>> ExecuteAsync(ObterAtividadesUsuarioRequest request)
    {
        var atividades = await _atividadeRepository.ObterPorUsuarioAsync(request.UsuarioId);

        return atividades.Select(a => new ObterAtividadeDto
        {
            Id = a.Id,
            Tipo = a.Tipo.ToString(),
            Descricao = GerarDescricao(a),
            Data = a.Data
        });
    }

    private string GerarDescricao(Domain.Entities.Atividade atividade)
    {
        return atividade.Tipo.Valor switch
        {
            KDramaSystem.Domain.Enums.TipoAtividadeEnum.AvaliouDorama => "Avaliou um dorama",
            KDramaSystem.Domain.Enums.TipoAtividadeEnum.AtualizouStatusDorama => "Atualizou status de um dorama",
            KDramaSystem.Domain.Enums.TipoAtividadeEnum.AtualizouProgresso => "Assistiu um episódio",
            KDramaSystem.Domain.Enums.TipoAtividadeEnum.CriouListaPrateleira => "Criou uma lista de prateleira",
            KDramaSystem.Domain.Enums.TipoAtividadeEnum.ComentouDorama => "Comentou em um dorama",
            KDramaSystem.Domain.Enums.TipoAtividadeEnum.SeguiuUsuario => "Começou a seguir um usuário",
            _ => "Atividade realizada"
        };
    }
}