using FluentValidation;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Ator.Excluir;

public class ExcluirAtorUseCase
{
    private readonly IAtorRepository _atorRepository;
    private readonly IValidator<ExcluirAtorRequest> _validator;

    public ExcluirAtorUseCase(IAtorRepository atorRepository, IValidator<ExcluirAtorRequest> validator)
    {
        _atorRepository = atorRepository;
        _validator = validator;
    }

    public async Task ExecutarAsync(ExcluirAtorRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new Exception(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

        var ator = await _atorRepository.ObterPorIdAsync(request.Id);
        if (ator == null)
            throw new Exception("Ator não encontrado.");

        await _atorRepository.ExcluirAsync(request.Id);
    }
}