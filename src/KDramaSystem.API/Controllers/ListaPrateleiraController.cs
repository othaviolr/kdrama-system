using KDramaSystem.Application.DTOs.ListaPrateleira;
using KDramaSystem.Application.Interfaces;
using KDramaSystem.Application.UseCases.ListaPrateleira.Compartilhar;
using KDramaSystem.Application.UseCases.ListaPrateleira.Criar;
using KDramaSystem.Application.UseCases.ListaPrateleira.Editar;
using KDramaSystem.Application.UseCases.ListaPrateleira.Excluir;
using KDramaSystem.Application.UseCases.ListaPrateleira.Obter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers;

[ApiController]
[Route("api/listas-prateleira")]
[Authorize]
public class ListaPrateleiraController : ControllerBase
{
    private readonly CriarListaPrateleiraUseCase _criarUseCase;
    private readonly EditarListaPrateleiraUseCase _editarUseCase;
    private readonly ExcluirListaPrateleiraUseCase _excluirUseCase;
    private readonly ObterListaPrateleiraUseCase _obterUseCase;
    private readonly CompartilharListaPrateleiraUseCase _compartilharUseCase;
    private readonly IUsuarioAutenticadoProvider _usuarioAutenticadoProvider;

    public ListaPrateleiraController(
        CriarListaPrateleiraUseCase criarUseCase,
        EditarListaPrateleiraUseCase editarUseCase,
        ExcluirListaPrateleiraUseCase excluirUseCase,
        ObterListaPrateleiraUseCase obterUseCase,
        IUsuarioAutenticadoProvider usuarioAutenticadoProvider,
        CompartilharListaPrateleiraUseCase compartilharUseCase)
    {
        _criarUseCase = criarUseCase;
        _editarUseCase = editarUseCase;
        _excluirUseCase = excluirUseCase;
        _obterUseCase = obterUseCase;
        _usuarioAutenticadoProvider = usuarioAutenticadoProvider;
        _compartilharUseCase = compartilharUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarListaPrateleiraRequestDto dto)
    {
        var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

        var request = new CriarListaPrateleiraRequest
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            ImagemCapaUrl = dto.ImagemCapaUrl,
            Privacidade = dto.Privacidade,
            UsuarioId = usuarioId
        };

        var lista = await _criarUseCase.ExecuteAsync(request);

        return Ok(new CriarListaPrateleiraDto
        {
            Id = lista.Id,
            Nome = lista.Nome,
            Descricao = lista.Descricao,
            ImagemCapaUrl = lista.ImagemCapaUrl,
            ShareToken = lista.ShareToken,
            Privacidade = lista.Privacidade
        });
    }

    [HttpPut("{listaId:guid}")]
    public async Task<IActionResult> Editar(Guid listaId, [FromBody] EditarListaPrateleiraDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

            var request = new EditarListaPrateleiraRequest
            {
                ListaId = listaId,
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                ImagemCapaUrl = dto.ImagemCapaUrl,
                Privacidade = dto.Privacidade,
                UsuarioId = usuarioId
            };

            var lista = await _editarUseCase.ExecuteAsync(request);

            return Ok(new EditarListaPrateleiraDto
            {
                Id = lista.Id,
                Nome = lista.Nome,
                Descricao = lista.Descricao,
                ImagemCapaUrl = lista.ImagemCapaUrl
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = ex.Message });
        }
    }

    [HttpDelete("{listaId:guid}")]
    public async Task<IActionResult> Excluir(Guid listaId)
    {
        if (listaId == Guid.Empty)
            return BadRequest(new { erro = "ListaId inválido." });

        try
        {
            var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

            var request = new ExcluirListaPrateleiraRequest
            {
                ListaId = listaId,
                UsuarioId = usuarioId
            };

            await _excluirUseCase.ExecuteAsync(request);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = ex.Message });
        }
    }

    [HttpGet("{listaId:guid}")]
    public async Task<IActionResult> ObterPorId(Guid listaId)
    {
        if (listaId == Guid.Empty)
            return BadRequest(new { erro = "ListaId inválido." });

        try
        {
            var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

            var request = new ObterListaPrateleiraRequest
            {
                ListaId = listaId,
                UsuarioLogadoId = usuarioId
            };

            var listas = await _obterUseCase.ExecuteAsync(request);

            var listaDto = listas.Select(lista => new ObterListaPrateleiraDto
            {
                Id = lista.Id,
                Nome = lista.Nome,
                Descricao = lista.Descricao,
                ImagemCapaUrl = lista.ImagemCapaUrl,
                Privacidade = lista.Privacidade,
                ShareToken = lista.ShareToken,
                UsuarioId = lista.UsuarioId,
                DataCriacao = lista.DataCriacao,
                Doramas = lista.Doramas.Select(d => new DoramaListaDto
                {
                    DoramaId = d.DoramaId,
                    DataAdicao = d.DataAdicao
                }).ToList()
            }).FirstOrDefault();

            if (listaDto == null)
                return NotFound(new { erro = "Lista não encontrada." });

            return Ok(listaDto);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = ex.Message });
        }
    }

    [HttpGet("publicas")]
    public async Task<IActionResult> ObterPublicas()
    {
        try
        {
            var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

            var request = new ObterListaPrateleiraRequest
            {
                UsuarioLogadoId = usuarioId
            };

            var listas = await _obterUseCase.ExecuteAsync(request);

            var listasDto = listas.Select(lista => new ObterListaPrateleiraDto
            {
                Id = lista.Id,
                Nome = lista.Nome,
                Descricao = lista.Descricao,
                ImagemCapaUrl = lista.ImagemCapaUrl,
                Privacidade = lista.Privacidade,
                ShareToken = lista.ShareToken,
                UsuarioId = lista.UsuarioId,
                DataCriacao = lista.DataCriacao,
                Doramas = lista.Doramas.Select(d => new DoramaListaDto
                {
                    DoramaId = d.DoramaId,
                    DataAdicao = d.DataAdicao
                }).ToList()
            }).ToList();

            return Ok(listasDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = ex.Message });
        }
    }

    [HttpGet("minhas")]
    public async Task<IActionResult> ObterMinhas()
    {
        try
        {
            var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

            var request = new ObterListaPrateleiraRequest
            {
                UsuarioLogadoId = usuarioId,
                ApenasDoUsuario = true
            };

            var listas = await _obterUseCase.ExecuteAsync(request);

            var listasDto = listas.Select(lista => new ObterListaPrateleiraDto
            {
                Id = lista.Id,
                Nome = lista.Nome,
                Descricao = lista.Descricao,
                ImagemCapaUrl = lista.ImagemCapaUrl,
                Privacidade = lista.Privacidade,
                ShareToken = lista.ShareToken,
                UsuarioId = lista.UsuarioId,
                DataCriacao = lista.DataCriacao,
                Doramas = lista.Doramas.Select(d => new DoramaListaDto
                {
                    DoramaId = d.DoramaId,
                    DataAdicao = d.DataAdicao
                }).ToList()
            }).ToList();

            return Ok(listasDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = ex.Message });
        }
    }

    [HttpPost("{listaId:guid}/compartilhar")]
    public async Task<IActionResult> Compartilhar(Guid listaId)
    {
        try
        {
            var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

            var token = await _compartilharUseCase.ExecuteAsync(new CompartilharListaPrateleiraRequest
            {
                ListaId = listaId,
                UsuarioId = usuarioId
            });

            return Ok(new { shareToken = token });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = ex.Message });
        }
    }
}