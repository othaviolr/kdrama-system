using FluentValidation;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Ator.Editar;

public class EditarAtorUseCase
{
    private readonly IAtorRepository _atorRepository;
    private readonly IValidator<EditarAtorRequest> _validator;

    public EditarAtorUseCase(IAtorRepository atorRepository, IValidator<EditarAtorRequest> validator)
    {
        _atorRepository = atorRepository;
        _validator = validator;
    }

    public async Task ExecutarAsync(EditarAtorRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new Exception(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

        var ator = await _atorRepository.ObterPorIdAsync(request.Id);
        if (ator == null)
            throw new Exception("Ator não encontrado.");

        var nomeDuplicado = await _atorRepository.ExisteComNomeAsync(request.Nome);
        if (nomeDuplicado && !string.Equals(ator.Nome, request.Nome, StringComparison.OrdinalIgnoreCase))
            throw new Exception("Já existe outro ator com esse nome.");

        ator.AtualizarDados(
            request.Nome,
            request.NomeCompleto,
            request.AnoNascimento,
            request.Altura,
            request.Pais,
            request.Biografia,
            request.FotoUrl,
            request.Instagram);

        await _atorRepository.AtualizarAsync(ator);
    }
}